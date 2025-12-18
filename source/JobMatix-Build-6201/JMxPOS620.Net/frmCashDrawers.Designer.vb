<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashDrawers
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
        Me.ShapedPanelMain = New JMxPOS330.ShapedPanel()
        Me.panelTillSelect = New System.Windows.Forms.Panel()
        Me.labSelectedTill = New System.Windows.Forms.Label()
        Me.panelSelectTill = New System.Windows.Forms.Panel()
        Me.optTill_H = New System.Windows.Forms.RadioButton()
        Me.optTill_G = New System.Windows.Forms.RadioButton()
        Me.optTill_F = New System.Windows.Forms.RadioButton()
        Me.optTill_E = New System.Windows.Forms.RadioButton()
        Me.optTill_D = New System.Windows.Forms.RadioButton()
        Me.optTill_C = New System.Windows.Forms.RadioButton()
        Me.optTill_B = New System.Windows.Forms.RadioButton()
        Me.optTill_A = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnRefreshPrt = New System.Windows.Forms.Button()
        Me.btnTillTest = New System.Windows.Forms.Button()
        Me.btnTillSave = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpBoxTillTrigger = New System.Windows.Forms.GroupBox()
        Me.btnTriggerClear = New System.Windows.Forms.Button()
        Me.btnTriggerOk = New System.Windows.Forms.Button()
        Me.btnRestoreDefault = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTillTrigger = New System.Windows.Forms.TextBox()
        Me.labTillExplain = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ShapedPanelMain.SuspendLayout()
        Me.panelTillSelect.SuspendLayout()
        Me.panelSelectTill.SuspendLayout()
        Me.grpBoxTillTrigger.SuspendLayout()
        Me.SuspendLayout()
        '
        'ShapedPanelMain
        '
        Me.ShapedPanelMain.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.ShapedPanelMain.BorderColor = System.Drawing.Color.Khaki
        Me.ShapedPanelMain.Controls.Add(Me.panelTillSelect)
        Me.ShapedPanelMain.Controls.Add(Me.Label1)
        Me.ShapedPanelMain.Controls.Add(Me.btnExit)
        Me.ShapedPanelMain.Controls.Add(Me.labStaffName)
        Me.ShapedPanelMain.Controls.Add(Me.Label13)
        Me.ShapedPanelMain.Controls.Add(Me.Label5)
        Me.ShapedPanelMain.Edge = 20
        Me.ShapedPanelMain.Location = New System.Drawing.Point(12, 12)
        Me.ShapedPanelMain.Name = "ShapedPanelMain"
        Me.ShapedPanelMain.Size = New System.Drawing.Size(684, 499)
        Me.ShapedPanelMain.TabIndex = 9
        '
        'panelTillSelect
        '
        Me.panelTillSelect.Controls.Add(Me.labSelectedTill)
        Me.panelTillSelect.Controls.Add(Me.panelSelectTill)
        Me.panelTillSelect.Controls.Add(Me.btnRefreshPrt)
        Me.panelTillSelect.Controls.Add(Me.btnTillTest)
        Me.panelTillSelect.Controls.Add(Me.btnTillSave)
        Me.panelTillSelect.Controls.Add(Me.Label2)
        Me.panelTillSelect.Controls.Add(Me.grpBoxTillTrigger)
        Me.panelTillSelect.Controls.Add(Me.cboReceiptPrinters)
        Me.panelTillSelect.Controls.Add(Me.Label53)
        Me.panelTillSelect.Location = New System.Drawing.Point(13, 87)
        Me.panelTillSelect.Name = "panelTillSelect"
        Me.panelTillSelect.Size = New System.Drawing.Size(663, 402)
        Me.panelTillSelect.TabIndex = 64
        '
        'labSelectedTill
        '
        Me.labSelectedTill.Font = New System.Drawing.Font("Lucida Sans", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSelectedTill.Location = New System.Drawing.Point(147, 140)
        Me.labSelectedTill.Name = "labSelectedTill"
        Me.labSelectedTill.Size = New System.Drawing.Size(118, 22)
        Me.labSelectedTill.TabIndex = 64
        Me.labSelectedTill.Text = "labSelectedTill"
        '
        'panelSelectTill
        '
        Me.panelSelectTill.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelSelectTill.Controls.Add(Me.optTill_H)
        Me.panelSelectTill.Controls.Add(Me.optTill_G)
        Me.panelSelectTill.Controls.Add(Me.optTill_F)
        Me.panelSelectTill.Controls.Add(Me.optTill_E)
        Me.panelSelectTill.Controls.Add(Me.optTill_D)
        Me.panelSelectTill.Controls.Add(Me.optTill_C)
        Me.panelSelectTill.Controls.Add(Me.optTill_B)
        Me.panelSelectTill.Controls.Add(Me.optTill_A)
        Me.panelSelectTill.Controls.Add(Me.Label4)
        Me.panelSelectTill.Location = New System.Drawing.Point(22, 105)
        Me.panelSelectTill.Name = "panelSelectTill"
        Me.panelSelectTill.Size = New System.Drawing.Size(101, 269)
        Me.panelSelectTill.TabIndex = 0
        '
        'optTill_H
        '
        Me.optTill_H.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_H.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_H.Location = New System.Drawing.Point(13, 225)
        Me.optTill_H.Name = "optTill_H"
        Me.optTill_H.Size = New System.Drawing.Size(76, 20)
        Me.optTill_H.TabIndex = 74
        Me.optTill_H.TabStop = True
        Me.optTill_H.Text = "Till  H"
        Me.optTill_H.UseVisualStyleBackColor = False
        '
        'optTill_G
        '
        Me.optTill_G.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_G.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_G.Location = New System.Drawing.Point(13, 198)
        Me.optTill_G.Name = "optTill_G"
        Me.optTill_G.Size = New System.Drawing.Size(76, 20)
        Me.optTill_G.TabIndex = 73
        Me.optTill_G.TabStop = True
        Me.optTill_G.Text = "Till  G"
        Me.optTill_G.UseVisualStyleBackColor = False
        '
        'optTill_F
        '
        Me.optTill_F.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_F.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_F.Location = New System.Drawing.Point(13, 171)
        Me.optTill_F.Name = "optTill_F"
        Me.optTill_F.Size = New System.Drawing.Size(76, 20)
        Me.optTill_F.TabIndex = 72
        Me.optTill_F.TabStop = True
        Me.optTill_F.Text = "Till  F"
        Me.optTill_F.UseVisualStyleBackColor = False
        '
        'optTill_E
        '
        Me.optTill_E.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_E.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_E.Location = New System.Drawing.Point(13, 144)
        Me.optTill_E.Name = "optTill_E"
        Me.optTill_E.Size = New System.Drawing.Size(76, 20)
        Me.optTill_E.TabIndex = 71
        Me.optTill_E.TabStop = True
        Me.optTill_E.Text = "Till  E"
        Me.optTill_E.UseVisualStyleBackColor = False
        '
        'optTill_D
        '
        Me.optTill_D.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_D.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_D.Location = New System.Drawing.Point(13, 117)
        Me.optTill_D.Name = "optTill_D"
        Me.optTill_D.Size = New System.Drawing.Size(76, 20)
        Me.optTill_D.TabIndex = 70
        Me.optTill_D.TabStop = True
        Me.optTill_D.Text = "Till  D"
        Me.optTill_D.UseVisualStyleBackColor = False
        '
        'optTill_C
        '
        Me.optTill_C.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_C.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_C.Location = New System.Drawing.Point(13, 90)
        Me.optTill_C.Name = "optTill_C"
        Me.optTill_C.Size = New System.Drawing.Size(76, 20)
        Me.optTill_C.TabIndex = 69
        Me.optTill_C.TabStop = True
        Me.optTill_C.Text = "Till  C"
        Me.optTill_C.UseVisualStyleBackColor = False
        '
        'optTill_B
        '
        Me.optTill_B.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_B.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_B.Location = New System.Drawing.Point(13, 63)
        Me.optTill_B.Name = "optTill_B"
        Me.optTill_B.Size = New System.Drawing.Size(76, 20)
        Me.optTill_B.TabIndex = 68
        Me.optTill_B.TabStop = True
        Me.optTill_B.Text = "Till  B"
        Me.optTill_B.UseVisualStyleBackColor = False
        '
        'optTill_A
        '
        Me.optTill_A.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optTill_A.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTill_A.Location = New System.Drawing.Point(13, 36)
        Me.optTill_A.Name = "optTill_A"
        Me.optTill_A.Size = New System.Drawing.Size(76, 20)
        Me.optTill_A.TabIndex = 0
        Me.optTill_A.TabStop = True
        Me.optTill_A.Text = "Till  A"
        Me.optTill_A.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 16)
        Me.Label4.TabIndex = 67
        Me.Label4.Text = "Select Till:"
        '
        'btnRefreshPrt
        '
        Me.btnRefreshPrt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRefreshPrt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshPrt.Location = New System.Drawing.Point(460, 105)
        Me.btnRefreshPrt.Name = "btnRefreshPrt"
        Me.btnRefreshPrt.Size = New System.Drawing.Size(74, 29)
        Me.btnRefreshPrt.TabIndex = 2
        Me.btnRefreshPrt.Text = "Refresh"
        Me.ToolTip1.SetToolTip(Me.btnRefreshPrt, "Refresh Printer List..")
        Me.btnRefreshPrt.UseVisualStyleBackColor = False
        '
        'btnTillTest
        '
        Me.btnTillTest.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnTillTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTillTest.Location = New System.Drawing.Point(567, 273)
        Me.btnTillTest.Name = "btnTillTest"
        Me.btnTillTest.Size = New System.Drawing.Size(74, 29)
        Me.btnTillTest.TabIndex = 4
        Me.btnTillTest.Text = "Test"
        Me.ToolTip1.SetToolTip(Me.btnTillTest, "test This Till..")
        Me.btnTillTest.UseVisualStyleBackColor = False
        '
        'btnTillSave
        '
        Me.btnTillSave.BackColor = System.Drawing.Color.Chartreuse
        Me.btnTillSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTillSave.Location = New System.Drawing.Point(567, 345)
        Me.btnTillSave.Name = "btnTillSave"
        Me.btnTillSave.Size = New System.Drawing.Size(74, 29)
        Me.btnTillSave.TabIndex = 5
        Me.btnTillSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnTillSave, "Save This Till..")
        Me.btnTillSave.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(286, 86)
        Me.Label2.TabIndex = 63
        Me.Label2.Text = "Select Till (Cash Drawer) and select the docket printer to which it is attached.." & _
    "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Then Check/change/Confirm the Open-Drawer code sequence.."
        '
        'grpBoxTillTrigger
        '
        Me.grpBoxTillTrigger.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxTillTrigger.Controls.Add(Me.btnTriggerClear)
        Me.grpBoxTillTrigger.Controls.Add(Me.btnTriggerOk)
        Me.grpBoxTillTrigger.Controls.Add(Me.btnRestoreDefault)
        Me.grpBoxTillTrigger.Controls.Add(Me.Label3)
        Me.grpBoxTillTrigger.Controls.Add(Me.txtTillTrigger)
        Me.grpBoxTillTrigger.Controls.Add(Me.labTillExplain)
        Me.grpBoxTillTrigger.Location = New System.Drawing.Point(140, 170)
        Me.grpBoxTillTrigger.Name = "grpBoxTillTrigger"
        Me.grpBoxTillTrigger.Size = New System.Drawing.Size(394, 204)
        Me.grpBoxTillTrigger.TabIndex = 3
        Me.grpBoxTillTrigger.TabStop = False
        Me.grpBoxTillTrigger.Text = "grpBoxTillTrigger"
        '
        'btnTriggerClear
        '
        Me.btnTriggerClear.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnTriggerClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTriggerClear.Location = New System.Drawing.Point(324, 46)
        Me.btnTriggerClear.Name = "btnTriggerClear"
        Me.btnTriggerClear.Size = New System.Drawing.Size(53, 25)
        Me.btnTriggerClear.TabIndex = 2
        Me.btnTriggerClear.Text = "Clear"
        Me.btnTriggerClear.UseVisualStyleBackColor = False
        '
        'btnTriggerOk
        '
        Me.btnTriggerOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnTriggerOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTriggerOk.Location = New System.Drawing.Point(324, 164)
        Me.btnTriggerOk.Name = "btnTriggerOk"
        Me.btnTriggerOk.Size = New System.Drawing.Size(53, 25)
        Me.btnTriggerOk.TabIndex = 3
        Me.btnTriggerOk.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.btnTriggerOk, "Confirms text sequence entered..")
        Me.btnTriggerOk.UseVisualStyleBackColor = False
        '
        'btnRestoreDefault
        '
        Me.btnRestoreDefault.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRestoreDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestoreDefault.Location = New System.Drawing.Point(202, 147)
        Me.btnRestoreDefault.Name = "btnRestoreDefault"
        Me.btnRestoreDefault.Size = New System.Drawing.Size(57, 42)
        Me.btnRestoreDefault.TabIndex = 1
        Me.btnRestoreDefault.Text = "Restore Default"
        Me.btnRestoreDefault.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(188, 78)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "NB: The original MYOB Retail Manager DEFAULT sequence for most docket printers is" & _
    ":" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<<  ESC p NULL ESC @  >>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  (Angle Brackets not includedt ).." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtTillTrigger
        '
        Me.txtTillTrigger.Location = New System.Drawing.Point(6, 86)
        Me.txtTillTrigger.MaxLength = 60
        Me.txtTillTrigger.Name = "txtTillTrigger"
        Me.txtTillTrigger.Size = New System.Drawing.Size(336, 21)
        Me.txtTillTrigger.TabIndex = 0
        '
        'labTillExplain
        '
        Me.labTillExplain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTillExplain.Location = New System.Drawing.Point(8, 15)
        Me.labTillExplain.Name = "labTillExplain"
        Me.labTillExplain.Size = New System.Drawing.Size(293, 67)
        Me.labTillExplain.TabIndex = 0
        Me.labTillExplain.Text = "Enter (via the keyboard) the code sequence to send to the Till-connected printer " & _
    "to open the Cash Drawer..  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use the Escape key to enter ESC, Ctl-0 for a NULL.." & _
    ""
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(314, 142)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(220, 20)
        Me.cboReceiptPrinters.TabIndex = 1
        '
        'Label53
        '
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(311, 103)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(107, 31)
        Me.Label53.TabIndex = 61
        Me.Label53.Text = "Attached to this Docket Printer:"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(351, 17)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "Set up Cash Drawer printer Connections for all Tills."
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExit.CausesValidation = False
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Location = New System.Drawing.Point(580, 20)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(74, 27)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'labStaffName
        '
        Me.labStaffName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStaffName.Location = New System.Drawing.Point(422, 39)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(125, 13)
        Me.labStaffName.TabIndex = 6
        Me.labStaffName.Text = "labStaffName"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(422, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 22)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Staff: "
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label5.Location = New System.Drawing.Point(25, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(379, 29)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "POS Setup Cash Drawer Connections" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'frmCashDrawers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(704, 523)
        Me.Controls.Add(Me.ShapedPanelMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCashDrawers"
        Me.Text = "frmCashDrawers"
        Me.ShapedPanelMain.ResumeLayout(False)
        Me.panelTillSelect.ResumeLayout(False)
        Me.panelSelectTill.ResumeLayout(False)
        Me.grpBoxTillTrigger.ResumeLayout(False)
        Me.grpBoxTillTrigger.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ShapedPanelMain As JMxPOS330.ShapedPanel
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grpBoxTillTrigger As System.Windows.Forms.GroupBox
    Friend WithEvents btnTriggerClear As System.Windows.Forms.Button
    Friend WithEvents btnTriggerOk As System.Windows.Forms.Button
    Friend WithEvents btnRestoreDefault As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTillTrigger As System.Windows.Forms.TextBox
    Friend WithEvents labTillExplain As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panelTillSelect As System.Windows.Forms.Panel
    Friend WithEvents btnTillSave As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnTillTest As System.Windows.Forms.Button
    Friend WithEvents btnRefreshPrt As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents panelSelectTill As System.Windows.Forms.Panel
    Friend WithEvents optTill_H As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_G As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_F As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_E As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_D As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_C As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_B As System.Windows.Forms.RadioButton
    Friend WithEvents optTill_A As System.Windows.Forms.RadioButton
    Friend WithEvents labSelectedTill As System.Windows.Forms.Label
End Class
