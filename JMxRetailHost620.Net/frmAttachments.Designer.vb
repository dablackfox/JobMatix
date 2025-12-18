<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttachments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttachments))
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.labHdr2 = New System.Windows.Forms.Label()
        Me.labEntityId = New System.Windows.Forms.Label()
        Me.labHdrInfo = New System.Windows.Forms.Label()
        Me.picProduct = New System.Windows.Forms.PictureBox()
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.lvwDocs = New System.Windows.Forms.ListView()
        Me.doc_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_date_created = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_file_title = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_file_size = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_staff = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.labPartyLab = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnViewDoc = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSaveAttachment = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtNewComment = New System.Windows.Forms.TextBox()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.grpBoxItem = New System.Windows.Forms.GroupBox()
        Me.picMsExcel = New System.Windows.Forms.PictureBox()
        Me.picMsWord = New System.Windows.Forms.PictureBox()
        Me.picPDF = New System.Windows.Forms.PictureBox()
        Me.grpBoxAddNew = New System.Windows.Forms.GroupBox()
        Me.txtNewFileName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labHelp = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelBanner = New System.Windows.Forms.Panel()
        CType(Me.picProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxItem.SuspendLayout()
        CType(Me.picMsExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMsWord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxAddNew.SuspendLayout()
        Me.SuspendLayout()
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(28, 28)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(206, 31)
        Me.labHdr1.TabIndex = 0
        Me.labHdr1.Text = "Attachments"
        '
        'labVersion
        '
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(30, 564)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(191, 12)
        Me.labVersion.TabIndex = 1
        Me.labVersion.Text = "labVersion"
        '
        'labHdr2
        '
        Me.labHdr2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr2.Location = New System.Drawing.Point(65, 62)
        Me.labHdr2.Name = "labHdr2"
        Me.labHdr2.Size = New System.Drawing.Size(74, 23)
        Me.labHdr2.TabIndex = 2
        Me.labHdr2.Text = "Job No:"
        '
        'labEntityId
        '
        Me.labEntityId.Font = New System.Drawing.Font("Courier New", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labEntityId.Location = New System.Drawing.Point(145, 62)
        Me.labEntityId.Name = "labEntityId"
        Me.labEntityId.Size = New System.Drawing.Size(89, 23)
        Me.labEntityId.TabIndex = 3
        Me.labEntityId.Text = "labEntityId"
        '
        'labHdrInfo
        '
        Me.labHdrInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.labHdrInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrInfo.Location = New System.Drawing.Point(275, 51)
        Me.labHdrInfo.Name = "labHdrInfo"
        Me.labHdrInfo.Size = New System.Drawing.Size(247, 48)
        Me.labHdrInfo.TabIndex = 4
        Me.labHdrInfo.Text = "labHdrInfo"
        '
        'picProduct
        '
        Me.picProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picProduct.Location = New System.Drawing.Point(137, 171)
        Me.picProduct.Name = "picProduct"
        Me.picProduct.Size = New System.Drawing.Size(92, 92)
        Me.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picProduct.TabIndex = 13
        Me.picProduct.TabStop = False
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'lvwDocs
        '
        Me.lvwDocs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lvwDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.doc_id, Me.doc_date_created, Me.doc_file_title, Me.doc_file_size, Me.doc_staff})
        Me.lvwDocs.Font = New System.Drawing.Font("Lucida Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDocs.FullRowSelect = True
        Me.lvwDocs.GridLines = True
        Me.lvwDocs.HideSelection = False
        Me.lvwDocs.Location = New System.Drawing.Point(6, 24)
        Me.lvwDocs.MultiSelect = False
        Me.lvwDocs.Name = "lvwDocs"
        Me.lvwDocs.Size = New System.Drawing.Size(626, 129)
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
        Me.doc_date_created.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.doc_file_size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.doc_file_size.Width = 90
        '
        'doc_staff
        '
        Me.doc_staff.Text = "Staff"
        Me.doc_staff.Width = 100
        '
        'labPartyLab
        '
        Me.labPartyLab.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPartyLab.Location = New System.Drawing.Point(276, 36)
        Me.labPartyLab.Name = "labPartyLab"
        Me.labPartyLab.Size = New System.Drawing.Size(219, 15)
        Me.labPartyLab.TabIndex = 16
        Me.labPartyLab.Text = "labPartyLab"
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(555, 238)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(53, 23)
        Me.btnDelete.TabIndex = 16
        Me.btnDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this document permantly. File System to View..")
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnViewDoc
        '
        Me.btnViewDoc.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnViewDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewDoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewDoc.Location = New System.Drawing.Point(555, 175)
        Me.btnViewDoc.Name = "btnViewDoc"
        Me.btnViewDoc.Size = New System.Drawing.Size(53, 23)
        Me.btnViewDoc.TabIndex = 15
        Me.btnViewDoc.Text = "View"
        Me.ToolTip1.SetToolTip(Me.btnViewDoc, "Launches app to View Doc..")
        Me.btnViewDoc.UseVisualStyleBackColor = False
        '
        'btnSaveAttachment
        '
        Me.btnSaveAttachment.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAttachment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAttachment.Location = New System.Drawing.Point(533, 115)
        Me.btnSaveAttachment.Name = "btnSaveAttachment"
        Me.btnSaveAttachment.Size = New System.Drawing.Size(57, 26)
        Me.btnSaveAttachment.TabIndex = 2
        Me.btnSaveAttachment.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveAttachment, "Save to Database")
        Me.btnSaveAttachment.UseVisualStyleBackColor = False
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(533, 31)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(57, 26)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.ToolTip1.SetToolTip(Me.btnBrowse, "Browse for new File to Attach")
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'txtNewComment
        '
        Me.txtNewComment.BackColor = System.Drawing.Color.White
        Me.txtNewComment.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewComment.Location = New System.Drawing.Point(191, 68)
        Me.txtNewComment.Multiline = True
        Me.txtNewComment.Name = "txtNewComment"
        Me.txtNewComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNewComment.Size = New System.Drawing.Size(312, 73)
        Me.txtNewComment.TabIndex = 1
        Me.txtNewComment.Text = "txtNewComment"
        Me.ToolTip1.SetToolTip(Me.txtNewComment, "ust Have Comment for New Attachment")
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(245, 175)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(292, 88)
        Me.txtComments.TabIndex = 17
        Me.txtComments.Text = "txtComments"
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
        Me.grpBoxItem.Location = New System.Drawing.Point(33, 268)
        Me.grpBoxItem.Name = "grpBoxItem"
        Me.grpBoxItem.Size = New System.Drawing.Size(638, 288)
        Me.grpBoxItem.TabIndex = 18
        Me.grpBoxItem.TabStop = False
        Me.grpBoxItem.Text = "grpBoxItem"
        '
        'picMsExcel
        '
        Me.picMsExcel.Image = CType(resources.GetObject("picMsExcel.Image"), System.Drawing.Image)
        Me.picMsExcel.Location = New System.Drawing.Point(20, 226)
        Me.picMsExcel.Name = "picMsExcel"
        Me.picMsExcel.Size = New System.Drawing.Size(51, 53)
        Me.picMsExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMsExcel.TabIndex = 27
        Me.picMsExcel.TabStop = False
        '
        'picMsWord
        '
        Me.picMsWord.Image = CType(resources.GetObject("picMsWord.Image"), System.Drawing.Image)
        Me.picMsWord.Location = New System.Drawing.Point(82, 174)
        Me.picMsWord.Name = "picMsWord"
        Me.picMsWord.Size = New System.Drawing.Size(43, 42)
        Me.picMsWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMsWord.TabIndex = 26
        Me.picMsWord.TabStop = False
        '
        'picPDF
        '
        Me.picPDF.Image = CType(resources.GetObject("picPDF.Image"), System.Drawing.Image)
        Me.picPDF.Location = New System.Drawing.Point(17, 171)
        Me.picPDF.Name = "picPDF"
        Me.picPDF.Size = New System.Drawing.Size(48, 41)
        Me.picPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPDF.TabIndex = 23
        Me.picPDF.TabStop = False
        '
        'grpBoxAddNew
        '
        Me.grpBoxAddNew.Controls.Add(Me.txtNewFileName)
        Me.grpBoxAddNew.Controls.Add(Me.Label3)
        Me.grpBoxAddNew.Controls.Add(Me.labHelp)
        Me.grpBoxAddNew.Controls.Add(Me.Label2)
        Me.grpBoxAddNew.Controls.Add(Me.btnSaveAttachment)
        Me.grpBoxAddNew.Controls.Add(Me.txtNewComment)
        Me.grpBoxAddNew.Controls.Add(Me.btnBrowse)
        Me.grpBoxAddNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxAddNew.Location = New System.Drawing.Point(32, 98)
        Me.grpBoxAddNew.Name = "grpBoxAddNew"
        Me.grpBoxAddNew.Size = New System.Drawing.Size(639, 157)
        Me.grpBoxAddNew.TabIndex = 19
        Me.grpBoxAddNew.TabStop = False
        Me.grpBoxAddNew.Text = "Add New Attachment"
        '
        'txtNewFileName
        '
        Me.txtNewFileName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNewFileName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewFileName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewFileName.Location = New System.Drawing.Point(191, 31)
        Me.txtNewFileName.Multiline = True
        Me.txtNewFileName.Name = "txtNewFileName"
        Me.txtNewFileName.ReadOnly = True
        Me.txtNewFileName.Size = New System.Drawing.Size(312, 31)
        Me.txtNewFileName.TabIndex = 23
        Me.txtNewFileName.Text = "txtNewFileName"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(191, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "File to attach:"
        '
        'labHelp
        '
        Me.labHelp.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelp.Location = New System.Drawing.Point(14, 22)
        Me.labHelp.Name = "labHelp"
        Me.labHelp.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHelp.Size = New System.Drawing.Size(146, 119)
        Me.labHelp.TabIndex = 20
        Me.labHelp.Text = "To add an Attachment,, browse to the file to be attached, and Open.  Then Enter s" & _
    "ome comment, and Press Save.."
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(530, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 42)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Must Have a Comment for New Doc."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Location = New System.Drawing.Point(618, 25)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(53, 26)
        Me.btnExit.TabIndex = 21
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 19)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "For-"
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.LightSteelBlue
        Me.panelBanner.Location = New System.Drawing.Point(0, 0)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(698, 21)
        Me.panelBanner.TabIndex = 23
        '
        'frmAttachments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(698, 577)
        Me.Controls.Add(Me.panelBanner)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grpBoxAddNew)
        Me.Controls.Add(Me.grpBoxItem)
        Me.Controls.Add(Me.labPartyLab)
        Me.Controls.Add(Me.labHdrInfo)
        Me.Controls.Add(Me.labEntityId)
        Me.Controls.Add(Me.labHdr2)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.labHdr1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmAttachments"
        Me.Text = "frmAttachments"
        CType(Me.picProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxItem.ResumeLayout(False)
        Me.grpBoxItem.PerformLayout()
        CType(Me.picMsExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMsWord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxAddNew.ResumeLayout(False)
        Me.grpBoxAddNew.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents labHdr2 As System.Windows.Forms.Label
    Friend WithEvents labEntityId As System.Windows.Forms.Label
    Friend WithEvents labHdrInfo As System.Windows.Forms.Label
    Friend WithEvents picProduct As System.Windows.Forms.PictureBox
    Friend WithEvents openDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lvwDocs As System.Windows.Forms.ListView
    Friend WithEvents doc_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents doc_date_created As System.Windows.Forms.ColumnHeader
    Friend WithEvents doc_file_title As System.Windows.Forms.ColumnHeader
    Friend WithEvents labPartyLab As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnViewDoc As System.Windows.Forms.Button
    Friend WithEvents doc_staff As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents grpBoxItem As System.Windows.Forms.GroupBox
    Friend WithEvents grpBoxAddNew As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSaveAttachment As System.Windows.Forms.Button
    Friend WithEvents txtNewComment As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents labHelp As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents picPDF As System.Windows.Forms.PictureBox
    Friend WithEvents txtNewFileName As System.Windows.Forms.TextBox
    Friend WithEvents doc_file_size As System.Windows.Forms.ColumnHeader
    Friend WithEvents picMsExcel As System.Windows.Forms.PictureBox
    Friend WithEvents picMsWord As System.Windows.Forms.PictureBox
    Friend WithEvents panelBanner As System.Windows.Forms.Panel
End Class
