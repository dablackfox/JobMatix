<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTransLookup
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtStaffBarcode = New System.Windows.Forms.TextBox()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.labSaleStaff = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.txtItemBarcode = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCustBarcode = New System.Windows.Forms.TextBox()
        Me.LabSaleCust = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.grpBoxDatePicker = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.panelPeriodFromTo = New System.Windows.Forms.Panel()
        Me.DTPickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.DTPickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.panelPeriodOpts = New System.Windows.Forms.Panel()
        Me.optPeriodAny = New System.Windows.Forms.RadioButton()
        Me.optPeriod12Months = New System.Windows.Forms.RadioButton()
        Me.optperiodThisMonth = New System.Windows.Forms.RadioButton()
        Me.optPeriodToday = New System.Windows.Forms.RadioButton()
        Me.cboLookupType = New System.Windows.Forms.ComboBox()
        Me.btnClearFilter = New System.Windows.Forms.Button()
        Me.labLookupType = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.frameBrowse = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmdClearTranSearch = New System.Windows.Forms.Button()
        Me.cmdTranSearch = New System.Windows.Forms.Button()
        Me.txtTranSearch = New System.Windows.Forms.TextBox()
        Me.dgvResultList = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRefresh2 = New System.Windows.Forms.Button()
        Me.panelBanner.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpBoxDatePicker.SuspendLayout()
        Me.panelPeriodFromTo.SuspendLayout()
        Me.panelPeriodOpts.SuspendLayout()
        Me.frameBrowse.SuspendLayout()
        CType(Me.dgvResultList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(6, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 44)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Transaction Lookup"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.GhostWhite
        Me.panelBanner.Controls.Add(Me.btnRefresh2)
        Me.panelBanner.Controls.Add(Me.Panel1)
        Me.panelBanner.Controls.Add(Me.btnExit)
        Me.panelBanner.Controls.Add(Me.grpBoxDatePicker)
        Me.panelBanner.Controls.Add(Me.Label1)
        Me.panelBanner.Controls.Add(Me.cboLookupType)
        Me.panelBanner.Controls.Add(Me.btnClearFilter)
        Me.panelBanner.Controls.Add(Me.labLookupType)
        Me.panelBanner.Controls.Add(Me.Label2)
        Me.panelBanner.Location = New System.Drawing.Point(0, 3)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(977, 145)
        Me.panelBanner.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtStaffBarcode)
        Me.Panel1.Controls.Add(Me.txtItemDescription)
        Me.Panel1.Controls.Add(Me.labSaleStaff)
        Me.Panel1.Controls.Add(Me.txtCustName)
        Me.Panel1.Controls.Add(Me.labStaffName)
        Me.Panel1.Controls.Add(Me.txtItemBarcode)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtCustBarcode)
        Me.Panel1.Controls.Add(Me.LabSaleCust)
        Me.Panel1.Location = New System.Drawing.Point(566, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(335, 126)
        Me.Panel1.TabIndex = 66
        '
        'txtStaffBarcode
        '
        Me.txtStaffBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaffBarcode.Location = New System.Drawing.Point(74, 3)
        Me.txtStaffBarcode.Name = "txtStaffBarcode"
        Me.txtStaffBarcode.Size = New System.Drawing.Size(55, 24)
        Me.txtStaffBarcode.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtStaffBarcode, "Staff Barcode..")
        '
        'txtItemDescription
        '
        Me.txtItemDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemDescription.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDescription.Location = New System.Drawing.Point(173, 53)
        Me.txtItemDescription.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtItemDescription.MaxLength = 60
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.ReadOnly = True
        Me.txtItemDescription.Size = New System.Drawing.Size(152, 17)
        Me.txtItemDescription.TabIndex = 60
        Me.txtItemDescription.TabStop = False
        Me.txtItemDescription.Text = "txtItemDescription"
        '
        'labSaleStaff
        '
        Me.labSaleStaff.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSaleStaff.ForeColor = System.Drawing.Color.Blue
        Me.labSaleStaff.Location = New System.Drawing.Point(7, 9)
        Me.labSaleStaff.Name = "labSaleStaff"
        Me.labSaleStaff.Size = New System.Drawing.Size(48, 13)
        Me.labSaleStaff.TabIndex = 53
        Me.labSaleStaff.Text = "Staff:"
        Me.labSaleStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCustName
        '
        Me.txtCustName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(10, 79)
        Me.txtCustName.Multiline = True
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.Size = New System.Drawing.Size(162, 36)
        Me.txtCustName.TabIndex = 65
        Me.txtCustName.Text = "txtCustName"
        '
        'labStaffName
        '
        Me.labStaffName.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.labStaffName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStaffName.Location = New System.Drawing.Point(7, 30)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(109, 18)
        Me.labStaffName.TabIndex = 55
        Me.labStaffName.Text = "labSaleStaffName"
        '
        'txtItemBarcode
        '
        Me.txtItemBarcode.BackColor = System.Drawing.Color.White
        Me.txtItemBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemBarcode.Location = New System.Drawing.Point(173, 25)
        Me.txtItemBarcode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtItemBarcode.MaxLength = 40
        Me.txtItemBarcode.Name = "txtItemBarcode"
        Me.txtItemBarcode.Size = New System.Drawing.Size(152, 24)
        Me.txtItemBarcode.TabIndex = 6
        Me.txtItemBarcode.Text = "txtItemBarcode"
        Me.ToolTip1.SetToolTip(Me.txtItemBarcode, "Stock Barcode (F2)")
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Lavender
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(250, 79)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 36)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Refresh Grid"
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh Grid")
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label3.Location = New System.Drawing.Point(170, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Stock Barcode:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.Label3, "Enter customer barcode, ot press ""Lookup""..")
        '
        'txtCustBarcode
        '
        Me.txtCustBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustBarcode.Location = New System.Drawing.Point(74, 52)
        Me.txtCustBarcode.Name = "txtCustBarcode"
        Me.txtCustBarcode.Size = New System.Drawing.Size(55, 24)
        Me.txtCustBarcode.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtCustBarcode, "Customer Barcode-   " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  F2 to Search." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'LabSaleCust
        '
        Me.LabSaleCust.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSaleCust.ForeColor = System.Drawing.Color.MediumBlue
        Me.LabSaleCust.Location = New System.Drawing.Point(7, 57)
        Me.LabSaleCust.Name = "LabSaleCust"
        Me.LabSaleCust.Size = New System.Drawing.Size(65, 13)
        Me.LabSaleCust.TabIndex = 56
        Me.LabSaleCust.Text = "Customer:"
        Me.LabSaleCust.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.LabSaleCust, "Enter customer barcode, ot press ""Lookup""..")
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExit.Location = New System.Drawing.Point(921, 8)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(47, 37)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'grpBoxDatePicker
        '
        Me.grpBoxDatePicker.Controls.Add(Me.Label4)
        Me.grpBoxDatePicker.Controls.Add(Me.panelPeriodFromTo)
        Me.grpBoxDatePicker.Controls.Add(Me.panelPeriodOpts)
        Me.grpBoxDatePicker.Location = New System.Drawing.Point(250, 4)
        Me.grpBoxDatePicker.Name = "grpBoxDatePicker"
        Me.grpBoxDatePicker.Size = New System.Drawing.Size(310, 134)
        Me.grpBoxDatePicker.TabIndex = 3
        Me.grpBoxDatePicker.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Search Period"
        '
        'panelPeriodFromTo
        '
        Me.panelPeriodFromTo.Controls.Add(Me.DTPickerFrom)
        Me.panelPeriodFromTo.Controls.Add(Me.DTPickerTo)
        Me.panelPeriodFromTo.Controls.Add(Me.Label7)
        Me.panelPeriodFromTo.Controls.Add(Me.Label8)
        Me.panelPeriodFromTo.Location = New System.Drawing.Point(153, 30)
        Me.panelPeriodFromTo.Name = "panelPeriodFromTo"
        Me.panelPeriodFromTo.Size = New System.Drawing.Size(145, 95)
        Me.panelPeriodFromTo.TabIndex = 21
        '
        'DTPickerFrom
        '
        Me.DTPickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerFrom.Location = New System.Drawing.Point(48, 17)
        Me.DTPickerFrom.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DTPickerFrom.Name = "DTPickerFrom"
        Me.DTPickerFrom.Size = New System.Drawing.Size(88, 21)
        Me.DTPickerFrom.TabIndex = 5
        '
        'DTPickerTo
        '
        Me.DTPickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerTo.Location = New System.Drawing.Point(48, 49)
        Me.DTPickerTo.Name = "DTPickerTo"
        Me.DTPickerTo.Size = New System.Drawing.Size(88, 21)
        Me.DTPickerTo.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(6, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 35)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Period From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(26, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "To"
        '
        'panelPeriodOpts
        '
        Me.panelPeriodOpts.Controls.Add(Me.optPeriodAny)
        Me.panelPeriodOpts.Controls.Add(Me.optPeriod12Months)
        Me.panelPeriodOpts.Controls.Add(Me.optperiodThisMonth)
        Me.panelPeriodOpts.Controls.Add(Me.optPeriodToday)
        Me.panelPeriodOpts.Location = New System.Drawing.Point(6, 30)
        Me.panelPeriodOpts.Name = "panelPeriodOpts"
        Me.panelPeriodOpts.Size = New System.Drawing.Size(141, 95)
        Me.panelPeriodOpts.TabIndex = 20
        '
        'optPeriodAny
        '
        Me.optPeriodAny.Location = New System.Drawing.Point(8, 13)
        Me.optPeriodAny.Name = "optPeriodAny"
        Me.optPeriodAny.Size = New System.Drawing.Size(49, 31)
        Me.optPeriodAny.TabIndex = 0
        Me.optPeriodAny.TabStop = True
        Me.optPeriodAny.Text = "Any Date"
        Me.optPeriodAny.UseVisualStyleBackColor = True
        '
        'optPeriod12Months
        '
        Me.optPeriod12Months.Location = New System.Drawing.Point(75, 49)
        Me.optPeriod12Months.Name = "optPeriod12Months"
        Me.optPeriod12Months.Size = New System.Drawing.Size(63, 34)
        Me.optPeriod12Months.TabIndex = 3
        Me.optPeriod12Months.TabStop = True
        Me.optPeriod12Months.Text = "Last 12 Months"
        Me.optPeriod12Months.UseVisualStyleBackColor = True
        '
        'optperiodThisMonth
        '
        Me.optperiodThisMonth.Location = New System.Drawing.Point(8, 49)
        Me.optperiodThisMonth.Name = "optperiodThisMonth"
        Me.optperiodThisMonth.Size = New System.Drawing.Size(56, 34)
        Me.optperiodThisMonth.TabIndex = 2
        Me.optperiodThisMonth.TabStop = True
        Me.optperiodThisMonth.Text = "This Month"
        Me.optperiodThisMonth.UseVisualStyleBackColor = True
        '
        'optPeriodToday
        '
        Me.optPeriodToday.Location = New System.Drawing.Point(73, 13)
        Me.optPeriodToday.Name = "optPeriodToday"
        Me.optPeriodToday.Size = New System.Drawing.Size(56, 34)
        Me.optPeriodToday.TabIndex = 1
        Me.optPeriodToday.TabStop = True
        Me.optPeriodToday.Text = "Today Only"
        Me.optPeriodToday.UseVisualStyleBackColor = True
        '
        'cboLookupType
        '
        Me.cboLookupType.BackColor = System.Drawing.Color.GhostWhite
        Me.cboLookupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLookupType.Font = New System.Drawing.Font("Lucida Sans Unicode", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLookupType.FormattingEnabled = True
        Me.cboLookupType.Location = New System.Drawing.Point(10, 79)
        Me.cboLookupType.Name = "cboLookupType"
        Me.cboLookupType.Size = New System.Drawing.Size(119, 24)
        Me.cboLookupType.TabIndex = 0
        '
        'btnClearFilter
        '
        Me.btnClearFilter.BackColor = System.Drawing.Color.Lavender
        Me.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearFilter.Location = New System.Drawing.Point(160, 44)
        Me.btnClearFilter.Name = "btnClearFilter"
        Me.btnClearFilter.Size = New System.Drawing.Size(74, 22)
        Me.btnClearFilter.TabIndex = 1
        Me.btnClearFilter.Text = "Clear All"
        Me.btnClearFilter.UseVisualStyleBackColor = False
        '
        'labLookupType
        '
        Me.labLookupType.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labLookupType.Location = New System.Drawing.Point(7, 60)
        Me.labLookupType.Name = "labLookupType"
        Me.labLookupType.Size = New System.Drawing.Size(122, 16)
        Me.labLookupType.TabIndex = 14
        Me.labLookupType.Text = "Transaction Type"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Lavender
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(157, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 33)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Select Filter Conditions"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frameBrowse
        '
        Me.frameBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameBrowse.CausesValidation = False
        Me.frameBrowse.Controls.Add(Me.Label22)
        Me.frameBrowse.Controls.Add(Me.Label21)
        Me.frameBrowse.Controls.Add(Me.cmdClearTranSearch)
        Me.frameBrowse.Controls.Add(Me.cmdTranSearch)
        Me.frameBrowse.Controls.Add(Me.txtTranSearch)
        Me.frameBrowse.Controls.Add(Me.dgvResultList)
        Me.frameBrowse.Controls.Add(Me.txtFind)
        Me.frameBrowse.Controls.Add(Me.labRecCount)
        Me.frameBrowse.Controls.Add(Me.LabFind)
        Me.frameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBrowse.Location = New System.Drawing.Point(3, 151)
        Me.frameBrowse.Name = "frameBrowse"
        Me.frameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBrowse.Size = New System.Drawing.Size(974, 453)
        Me.frameBrowse.TabIndex = 8
        Me.frameBrowse.TabStop = False
        Me.frameBrowse.Text = "FrameBrowse"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(854, 18)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(83, 15)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "Records found."
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(240, 19)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(121, 12)
        Me.Label21.TabIndex = 81
        Me.Label21.Text = "Full Text Filter (Srch):"
        '
        'cmdClearTranSearch
        '
        Me.cmdClearTranSearch.BackColor = System.Drawing.Color.LavenderBlush
        Me.cmdClearTranSearch.CausesValidation = False
        Me.cmdClearTranSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearTranSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearTranSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearTranSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearTranSearch.Location = New System.Drawing.Point(708, 36)
        Me.cmdClearTranSearch.Name = "cmdClearTranSearch"
        Me.cmdClearTranSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearTranSearch.Size = New System.Drawing.Size(63, 23)
        Me.cmdClearTranSearch.TabIndex = 80
        Me.cmdClearTranSearch.Text = "Clear"
        Me.cmdClearTranSearch.UseVisualStyleBackColor = False
        '
        'cmdTranSearch
        '
        Me.cmdTranSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.cmdTranSearch.CausesValidation = False
        Me.cmdTranSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTranSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdTranSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTranSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTranSearch.Location = New System.Drawing.Point(628, 36)
        Me.cmdTranSearch.Name = "cmdTranSearch"
        Me.cmdTranSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTranSearch.Size = New System.Drawing.Size(63, 23)
        Me.cmdTranSearch.TabIndex = 79
        Me.cmdTranSearch.Text = "Search"
        Me.cmdTranSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdTranSearch.UseVisualStyleBackColor = False
        '
        'txtTranSearch
        '
        Me.txtTranSearch.AcceptsReturn = True
        Me.txtTranSearch.BackColor = System.Drawing.Color.White
        Me.txtTranSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTranSearch.CausesValidation = False
        Me.txtTranSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTranSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTranSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTranSearch.Location = New System.Drawing.Point(239, 36)
        Me.txtTranSearch.MaxLength = 0
        Me.txtTranSearch.Name = "txtTranSearch"
        Me.txtTranSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTranSearch.Size = New System.Drawing.Size(290, 26)
        Me.txtTranSearch.TabIndex = 78
        Me.txtTranSearch.Text = "txtTranSearch"
        '
        'dgvResultList
        '
        Me.dgvResultList.AllowUserToAddRows = False
        Me.dgvResultList.AllowUserToDeleteRows = False
        Me.dgvResultList.BackgroundColor = System.Drawing.Color.White
        Me.dgvResultList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvResultList.ColumnHeadersHeight = 18
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvResultList.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvResultList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvResultList.Location = New System.Drawing.Point(6, 69)
        Me.dgvResultList.MultiSelect = False
        Me.dgvResultList.Name = "dgvResultList"
        Me.dgvResultList.ReadOnly = True
        Me.dgvResultList.RowHeadersWidth = 17
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvResultList.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvResultList.RowTemplate.Height = 19
        Me.dgvResultList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvResultList.Size = New System.Drawing.Size(952, 378)
        Me.dgvResultList.StandardTab = True
        Me.dgvResultList.TabIndex = 4
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.Gainsboro
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.CausesValidation = False
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(11, 44)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(168, 19)
        Me.txtFind.TabIndex = 2
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(804, 18)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(44, 17)
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
        Me.LabFind.Location = New System.Drawing.Point(10, 16)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(167, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'btnRefresh2
        '
        Me.btnRefresh2.BackColor = System.Drawing.Color.Lavender
        Me.btnRefresh2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh2.Location = New System.Drawing.Point(159, 91)
        Me.btnRefresh2.Name = "btnRefresh2"
        Me.btnRefresh2.Size = New System.Drawing.Size(75, 36)
        Me.btnRefresh2.TabIndex = 67
        Me.btnRefresh2.Text = "Refresh Grid"
        Me.ToolTip1.SetToolTip(Me.btnRefresh2, "Refresh Grid")
        Me.btnRefresh2.UseVisualStyleBackColor = False
        '
        'ucTransLookup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.frameBrowse)
        Me.Controls.Add(Me.panelBanner)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucTransLookup"
        Me.Size = New System.Drawing.Size(983, 630)
        Me.panelBanner.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpBoxDatePicker.ResumeLayout(False)
        Me.grpBoxDatePicker.PerformLayout()
        Me.panelPeriodFromTo.ResumeLayout(False)
        Me.panelPeriodOpts.ResumeLayout(False)
        Me.frameBrowse.ResumeLayout(False)
        Me.frameBrowse.PerformLayout()
        CType(Me.dgvResultList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panelBanner As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Public WithEvents frameBrowse As System.Windows.Forms.GroupBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents cmdClearTranSearch As System.Windows.Forms.Button
    Public WithEvents cmdTranSearch As System.Windows.Forms.Button
    Public WithEvents txtTranSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgvResultList As System.Windows.Forms.DataGridView
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Public WithEvents LabFind As System.Windows.Forms.Label
    Friend WithEvents labLookupType As System.Windows.Forms.Label
    Friend WithEvents cboLookupType As System.Windows.Forms.ComboBox
    Friend WithEvents txtStaffBarcode As System.Windows.Forms.TextBox
    Friend WithEvents labSaleStaff As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents LabSaleCust As System.Windows.Forms.Label
    Friend WithEvents txtCustBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtItemBarcode As System.Windows.Forms.TextBox
    Friend WithEvents txtItemDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClearFilter As System.Windows.Forms.Button
    Friend WithEvents grpBoxDatePicker As System.Windows.Forms.GroupBox
    Friend WithEvents panelPeriodOpts As System.Windows.Forms.Panel
    Friend WithEvents optPeriod12Months As System.Windows.Forms.RadioButton
    Friend WithEvents optperiodThisMonth As System.Windows.Forms.RadioButton
    Friend WithEvents optPeriodToday As System.Windows.Forms.RadioButton
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents panelPeriodFromTo As System.Windows.Forms.Panel
    Friend WithEvents DTPickerFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPickerTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents optPeriodAny As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh2 As System.Windows.Forms.Button

End Class
