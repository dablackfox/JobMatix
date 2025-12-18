<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBrowsePOS
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        Me.mbIsInitialising = True
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.mbIsInitialising = False
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
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents LabFind As System.Windows.Forms.Label
    Public WithEvents Labwhere As System.Windows.Forms.Label
    Public WithEvents labTable As System.Windows.Forms.Label
    Public WithEvents LabRecCount As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdClearTextSearch = New System.Windows.Forms.Button()
        Me.cmdTextSearch = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.Labwhere = New System.Windows.Forms.Label()
        Me.labTable = New System.Windows.Forms.Label()
        Me.LabRecCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.grpBoxMainFooter = New System.Windows.Forms.GroupBox()
        Me.panelOK = New System.Windows.Forms.Panel()
        Me.panelEdit = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTextSearch = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxMainFooter.SuspendLayout()
        Me.panelOK.SuspendLayout()
        Me.panelEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdEdit
        '
        Me.cmdEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEdit.Location = New System.Drawing.Point(16, 15)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(76, 21)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.Text = "Edit"
        Me.ToolTip1.SetToolTip(Me.cmdEdit, "Edit selected record")
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'cmdNew
        '
        Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNew.Location = New System.Drawing.Point(16, 47)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(76, 21)
        Me.cmdNew.TabIndex = 2
        Me.cmdNew.Text = "New"
        Me.ToolTip1.SetToolTip(Me.cmdNew, "Create New Record")
        Me.cmdNew.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(228, 13)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 22)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "No selection..  Exit.")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(160, 13)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(57, 22)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Select Record and Exit")
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(358, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 32)
        Me.Label5.TabIndex = 90
        Me.Label5.Text = "Full Text Search and Filter"
        Me.ToolTip1.SetToolTip(Me.Label5, "Search/Filter text columns for text fragments.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  (No asterisks please..)")
        '
        'cmdClearTextSearch
        '
        Me.cmdClearTextSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearTextSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearTextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearTextSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearTextSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearTextSearch.Location = New System.Drawing.Point(617, 66)
        Me.cmdClearTextSearch.Name = "cmdClearTextSearch"
        Me.cmdClearTextSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearTextSearch.Size = New System.Drawing.Size(65, 23)
        Me.cmdClearTextSearch.TabIndex = 6
        Me.cmdClearTextSearch.Text = "X  Clear"
        Me.ToolTip1.SetToolTip(Me.cmdClearTextSearch, "Clear Search Text and refresh grid..")
        Me.cmdClearTextSearch.UseVisualStyleBackColor = False
        '
        'cmdTextSearch
        '
        Me.cmdTextSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdTextSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdTextSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTextSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTextSearch.Location = New System.Drawing.Point(539, 66)
        Me.cmdTextSearch.Name = "cmdTextSearch"
        Me.cmdTextSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTextSearch.Size = New System.Drawing.Size(61, 23)
        Me.cmdTextSearch.TabIndex = 5
        Me.cmdTextSearch.Text = "Search"
        Me.cmdTextSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTextSearch, "Search/Refresh customer list.")
        Me.cmdTextSearch.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 200
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.Lavender
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(186, 69)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(122, 19)
        Me.txtFind.TabIndex = 2
        Me.txtFind.Text = "txtFind"
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(15, 58)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(165, 31)
        Me.LabFind.TabIndex = 1
        Me.LabFind.Text = "Find xxx"
        '
        'Labwhere
        '
        Me.Labwhere.BackColor = System.Drawing.Color.Transparent
        Me.Labwhere.Cursor = System.Windows.Forms.Cursors.Default
        Me.Labwhere.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labwhere.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Labwhere.Location = New System.Drawing.Point(6, 16)
        Me.Labwhere.Name = "Labwhere"
        Me.Labwhere.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Labwhere.Size = New System.Drawing.Size(324, 24)
        Me.Labwhere.TabIndex = 6
        Me.Labwhere.Text = "labWhere"
        '
        'labTable
        '
        Me.labTable.BackColor = System.Drawing.Color.Transparent
        Me.labTable.Cursor = System.Windows.Forms.Cursors.Default
        Me.labTable.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.labTable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labTable.Location = New System.Drawing.Point(0, 1)
        Me.labTable.Name = "labTable"
        Me.labTable.Padding = New System.Windows.Forms.Padding(12, 7, 0, 0)
        Me.labTable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labTable.Size = New System.Drawing.Size(304, 26)
        Me.labTable.TabIndex = 0
        Me.labTable.Text = "labTable"
        '
        'LabRecCount
        '
        Me.LabRecCount.BackColor = System.Drawing.SystemColors.Control
        Me.LabRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRecCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRecCount.Location = New System.Drawing.Point(3, 16)
        Me.LabRecCount.Name = "LabRecCount"
        Me.LabRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRecCount.Size = New System.Drawing.Size(92, 18)
        Me.LabRecCount.TabIndex = 1
        Me.LabRecCount.Text = "labRecCount"
        Me.LabRecCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(94, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(63, 18)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "records"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'labStatus
        '
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.Color.Firebrick
        Me.labStatus.Location = New System.Drawing.Point(344, 2)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Size = New System.Drawing.Size(181, 15)
        Me.labStatus.TabIndex = 7
        Me.labStatus.Text = "labStatus"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridView1.Location = New System.Drawing.Point(9, 95)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 17
        Me.DataGridView1.RowTemplate.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(808, 346)
        Me.DataGridView1.StandardTab = True
        Me.DataGridView1.TabIndex = 3
        '
        'labVersion
        '
        Me.labVersion.AutoSize = True
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(7, 44)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(51, 12)
        Me.labVersion.TabIndex = 8
        Me.labVersion.Text = "labVersion"
        '
        'grpBoxMainFooter
        '
        Me.grpBoxMainFooter.Controls.Add(Me.panelOK)
        Me.grpBoxMainFooter.Controls.Add(Me.Labwhere)
        Me.grpBoxMainFooter.Controls.Add(Me.labVersion)
        Me.grpBoxMainFooter.Location = New System.Drawing.Point(9, 459)
        Me.grpBoxMainFooter.Name = "grpBoxMainFooter"
        Me.grpBoxMainFooter.Size = New System.Drawing.Size(808, 61)
        Me.grpBoxMainFooter.TabIndex = 9
        Me.grpBoxMainFooter.TabStop = False
        Me.grpBoxMainFooter.Text = "grpBoxMainFooter"
        '
        'panelOK
        '
        Me.panelOK.Controls.Add(Me.LabRecCount)
        Me.panelOK.Controls.Add(Me.Label2)
        Me.panelOK.Controls.Add(Me.cmdOk)
        Me.panelOK.Controls.Add(Me.cmdCancel)
        Me.panelOK.Location = New System.Drawing.Point(495, 15)
        Me.panelOK.Name = "panelOK"
        Me.panelOK.Size = New System.Drawing.Size(303, 41)
        Me.panelOK.TabIndex = 9
        '
        'panelEdit
        '
        Me.panelEdit.Controls.Add(Me.cmdNew)
        Me.panelEdit.Controls.Add(Me.cmdEdit)
        Me.panelEdit.Location = New System.Drawing.Point(717, 15)
        Me.panelEdit.Name = "panelEdit"
        Me.panelEdit.Size = New System.Drawing.Size(100, 74)
        Me.panelEdit.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Gainsboro
        Me.Label1.Location = New System.Drawing.Point(329, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 79)
        Me.Label1.TabIndex = 91
        '
        'txtTextSearch
        '
        Me.txtTextSearch.AcceptsReturn = True
        Me.txtTextSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTextSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTextSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTextSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTextSearch.Location = New System.Drawing.Point(361, 63)
        Me.txtTextSearch.MaxLength = 0
        Me.txtTextSearch.Name = "txtTextSearch"
        Me.txtTextSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTextSearch.Size = New System.Drawing.Size(164, 26)
        Me.txtTextSearch.TabIndex = 4
        Me.txtTextSearch.Text = "txtTextSearch"
        '
        'frmBrowsePOS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(829, 528)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdClearTextSearch)
        Me.Controls.Add(Me.cmdTextSearch)
        Me.Controls.Add(Me.txtTextSearch)
        Me.Controls.Add(Me.panelEdit)
        Me.Controls.Add(Me.grpBoxMainFooter)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtFind)
        Me.Controls.Add(Me.LabFind)
        Me.Controls.Add(Me.labTable)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(11, 30)
        Me.Name = "frmBrowsePOS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Browse Table"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxMainFooter.ResumeLayout(False)
        Me.grpBoxMainFooter.PerformLayout()
        Me.panelOK.ResumeLayout(False)
        Me.panelEdit.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents grpBoxMainFooter As System.Windows.Forms.GroupBox
    Friend WithEvents panelOK As System.Windows.Forms.Panel
    Friend WithEvents panelEdit As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmdClearTextSearch As System.Windows.Forms.Button
    Public WithEvents cmdTextSearch As System.Windows.Forms.Button
    Public WithEvents txtTextSearch As System.Windows.Forms.TextBox
#End Region
End Class