<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreditNotesReport
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
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.cboReportPrinters = New System.Windows.Forms.ComboBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.chkCreditNotesAllCust = New System.Windows.Forms.CheckBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.btnCreditNotesCustLookup = New System.Windows.Forms.Button()
        Me.chkCreditNotesOutstOnly = New System.Windows.Forms.CheckBox()
        Me.labCreditNotesCustName = New System.Windows.Forms.Label()
        Me.btnCreditNotesReport = New System.Windows.Forms.Button()
        Me.shapedPanelCreditNotes = New JMxPOS330.ShapedPanel()
        Me.shapedPanelCreditNotes.SuspendLayout()
        Me.SuspendLayout()
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(70, 20)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(212, 25)
        Me.labHdr1.TabIndex = 0
        Me.labHdr1.Text = "Credit Notes- History Report"
        '
        'cboReportPrinters
        '
        Me.cboReportPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboReportPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReportPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReportPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReportPrinters.FormattingEnabled = True
        Me.cboReportPrinters.Location = New System.Drawing.Point(30, 255)
        Me.cboReportPrinters.Name = "cboReportPrinters"
        Me.cboReportPrinters.Size = New System.Drawing.Size(211, 20)
        Me.cboReportPrinters.TabIndex = 4
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(27, 236)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(160, 13)
        Me.Label52.TabIndex = 57
        Me.Label52.Text = "Report Printer :"
        '
        'chkCreditNotesAllCust
        '
        Me.chkCreditNotesAllCust.BackColor = System.Drawing.Color.MistyRose
        Me.chkCreditNotesAllCust.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreditNotesAllCust.Location = New System.Drawing.Point(42, 106)
        Me.chkCreditNotesAllCust.Name = "chkCreditNotesAllCust"
        Me.chkCreditNotesAllCust.Size = New System.Drawing.Size(107, 26)
        Me.chkCreditNotesAllCust.TabIndex = 1
        Me.chkCreditNotesAllCust.Text = "All Customers"
        Me.chkCreditNotesAllCust.UseVisualStyleBackColor = False
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(39, 60)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(100, 31)
        Me.Label48.TabIndex = 10
        Me.Label48.Text = "Choose Customer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Optional)"
        '
        'btnCreditNotesCustLookup
        '
        Me.btnCreditNotesCustLookup.BackColor = System.Drawing.Color.MistyRose
        Me.btnCreditNotesCustLookup.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreditNotesCustLookup.Location = New System.Drawing.Point(145, 67)
        Me.btnCreditNotesCustLookup.Name = "btnCreditNotesCustLookup"
        Me.btnCreditNotesCustLookup.Size = New System.Drawing.Size(42, 24)
        Me.btnCreditNotesCustLookup.TabIndex = 0
        Me.btnCreditNotesCustLookup.Text = ">>"
        Me.btnCreditNotesCustLookup.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnCreditNotesCustLookup.UseVisualStyleBackColor = False
        '
        'chkCreditNotesOutstOnly
        '
        Me.chkCreditNotesOutstOnly.BackColor = System.Drawing.Color.MistyRose
        Me.chkCreditNotesOutstOnly.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreditNotesOutstOnly.Location = New System.Drawing.Point(32, 179)
        Me.chkCreditNotesOutstOnly.Name = "chkCreditNotesOutstOnly"
        Me.chkCreditNotesOutstOnly.Size = New System.Drawing.Size(107, 37)
        Me.chkCreditNotesOutstOnly.TabIndex = 2
        Me.chkCreditNotesOutstOnly.Text = "Show Outst.Only"
        Me.chkCreditNotesOutstOnly.UseVisualStyleBackColor = False
        '
        'labCreditNotesCustName
        '
        Me.labCreditNotesCustName.BackColor = System.Drawing.Color.Snow
        Me.labCreditNotesCustName.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCreditNotesCustName.Location = New System.Drawing.Point(29, 150)
        Me.labCreditNotesCustName.Name = "labCreditNotesCustName"
        Me.labCreditNotesCustName.Size = New System.Drawing.Size(237, 15)
        Me.labCreditNotesCustName.TabIndex = 7
        Me.labCreditNotesCustName.Text = "labCreditNotesCustName"
        '
        'btnCreditNotesReport
        '
        Me.btnCreditNotesReport.BackColor = System.Drawing.Color.MistyRose
        Me.btnCreditNotesReport.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreditNotesReport.Location = New System.Drawing.Point(277, 251)
        Me.btnCreditNotesReport.Name = "btnCreditNotesReport"
        Me.btnCreditNotesReport.Size = New System.Drawing.Size(112, 24)
        Me.btnCreditNotesReport.TabIndex = 3
        Me.btnCreditNotesReport.Text = "Show Report"
        Me.btnCreditNotesReport.UseVisualStyleBackColor = False
        '
        'shapedPanelCreditNotes
        '
        Me.shapedPanelCreditNotes.BackColor = System.Drawing.Color.MistyRose
        Me.shapedPanelCreditNotes.BorderColor = System.Drawing.Color.Thistle
        Me.shapedPanelCreditNotes.Controls.Add(Me.cboReportPrinters)
        Me.shapedPanelCreditNotes.Controls.Add(Me.Label48)
        Me.shapedPanelCreditNotes.Controls.Add(Me.Label52)
        Me.shapedPanelCreditNotes.Controls.Add(Me.btnCreditNotesReport)
        Me.shapedPanelCreditNotes.Controls.Add(Me.chkCreditNotesAllCust)
        Me.shapedPanelCreditNotes.Controls.Add(Me.labCreditNotesCustName)
        Me.shapedPanelCreditNotes.Controls.Add(Me.chkCreditNotesOutstOnly)
        Me.shapedPanelCreditNotes.Controls.Add(Me.btnCreditNotesCustLookup)
        Me.shapedPanelCreditNotes.Edge = 40
        Me.shapedPanelCreditNotes.Location = New System.Drawing.Point(32, 61)
        Me.shapedPanelCreditNotes.Name = "shapedPanelCreditNotes"
        Me.shapedPanelCreditNotes.Size = New System.Drawing.Size(459, 331)
        Me.shapedPanelCreditNotes.TabIndex = 5
        '
        'frmCreditNotesReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(527, 418)
        Me.Controls.Add(Me.shapedPanelCreditNotes)
        Me.Controls.Add(Me.labHdr1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCreditNotesReport"
        Me.Text = "frmCreditNotesReport"
        Me.shapedPanelCreditNotes.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents cboReportPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents chkCreditNotesAllCust As System.Windows.Forms.CheckBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents btnCreditNotesCustLookup As System.Windows.Forms.Button
    Friend WithEvents chkCreditNotesOutstOnly As System.Windows.Forms.CheckBox
    Friend WithEvents labCreditNotesCustName As System.Windows.Forms.Label
    Friend WithEvents btnCreditNotesReport As System.Windows.Forms.Button
    Friend WithEvents shapedPanelCreditNotes As JMxPOS330.ShapedPanel
End Class
