<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMigration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMigration))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labGetName = New System.Windows.Forms.Label()
        Me.txtNewName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.labBrowse = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.labBackupSrc = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.txtReport = New System.Windows.Forms.TextBox()
        Me.labExplain = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "JobMatix POS Migration."
        '
        'labGetName
        '
        Me.labGetName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labGetName.ForeColor = System.Drawing.Color.Firebrick
        Me.labGetName.Location = New System.Drawing.Point(76, 337)
        Me.labGetName.Name = "labGetName"
        Me.labGetName.Size = New System.Drawing.Size(277, 47)
        Me.labGetName.TabIndex = 1
        Me.labGetName.Text = "Check the name for the new database (NB- This based on the original JobTracking d" & _
    "atabase and can't be changed) .."
        '
        'txtNewName
        '
        Me.txtNewName.BackColor = System.Drawing.Color.Lavender
        Me.txtNewName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewName.Location = New System.Drawing.Point(79, 387)
        Me.txtNewName.Name = "txtNewName"
        Me.txtNewName.ReadOnly = True
        Me.txtNewName.Size = New System.Drawing.Size(274, 14)
        Me.txtNewName.TabIndex = 2
        Me.txtNewName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(357, 387)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "_jmpos"
        '
        'btnContinue
        '
        Me.btnContinue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.Location = New System.Drawing.Point(475, 376)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(83, 27)
        Me.btnContinue.TabIndex = 4
        Me.btnContinue.Text = "Continue"
        Me.btnContinue.UseVisualStyleBackColor = True
        '
        'labBrowse
        '
        Me.labBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labBrowse.ForeColor = System.Drawing.Color.Firebrick
        Me.labBrowse.Location = New System.Drawing.Point(240, 242)
        Me.labBrowse.Name = "labBrowse"
        Me.labBrowse.Size = New System.Drawing.Size(234, 31)
        Me.labBrowse.TabIndex = 5
        Me.labBrowse.Text = "Please browse to a copy of the latest JobMatix JobTracking backup (.BAK) file.."
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(481, 244)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(77, 27)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'labBackupSrc
        '
        Me.labBackupSrc.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labBackupSrc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labBackupSrc.Location = New System.Drawing.Point(27, 275)
        Me.labBackupSrc.Name = "labBackupSrc"
        Me.labBackupSrc.Size = New System.Drawing.Size(531, 48)
        Me.labBackupSrc.TabIndex = 1
        Me.labBackupSrc.Text = "labBackupSrc"
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.labStatus.Location = New System.Drawing.Point(27, 570)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labStatus.Size = New System.Drawing.Size(531, 45)
        Me.labStatus.TabIndex = 8
        Me.labStatus.Text = "labStatus"
        '
        'OpenDlg1
        '
        Me.OpenDlg1.FileName = "OpenFileDialog1"
        '
        'txtReport
        '
        Me.txtReport.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReport.Location = New System.Drawing.Point(30, 412)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ReadOnly = True
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReport.Size = New System.Drawing.Size(528, 149)
        Me.txtReport.TabIndex = 9
        '
        'labExplain
        '
        Me.labExplain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labExplain.ForeColor = System.Drawing.Color.DimGray
        Me.labExplain.Location = New System.Drawing.Point(27, 39)
        Me.labExplain.Name = "labExplain"
        Me.labExplain.Size = New System.Drawing.Size(531, 192)
        Me.labExplain.TabIndex = 10
        Me.labExplain.Text = resources.GetString("labExplain.Text")
        '
        'frmMigration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(609, 623)
        Me.Controls.Add(Me.labExplain)
        Me.Controls.Add(Me.txtReport)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.labBackupSrc)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.labBrowse)
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNewName)
        Me.Controls.Add(Me.labGetName)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMigration"
        Me.Text = "frmMigration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labGetName As System.Windows.Forms.Label
    Friend WithEvents txtNewName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents labBrowse As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents labBackupSrc As System.Windows.Forms.Label
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents OpenDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
    Friend WithEvents labExplain As System.Windows.Forms.Label
End Class
