<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportRM
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.labRMfilepath = New System.Windows.Forms.Label()
        Me.labTargetCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labProgress = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.labTargetDBname = New System.Windows.Forms.Label()
        Me.labAction = New System.Windows.Forms.Label()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.txtReport = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(303, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Import MYOB Retail Manager Stock Info"
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnStart.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStart.Location = New System.Drawing.Point(558, 153)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(189, 30)
        Me.btnStart.TabIndex = 2
        Me.btnStart.Text = "Start the Import.."
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(420, 101)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(83, 26)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse ..."
        Me.ToolTip1.SetToolTip(Me.btnBrowse, "Browse to Retail Manager (recent.mdb) database ..")
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'labRMfilepath
        '
        Me.labRMfilepath.BackColor = System.Drawing.Color.LightGreen
        Me.labRMfilepath.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRMfilepath.Location = New System.Drawing.Point(24, 82)
        Me.labRMfilepath.Name = "labRMfilepath"
        Me.labRMfilepath.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labRMfilepath.Size = New System.Drawing.Size(374, 45)
        Me.labRMfilepath.TabIndex = 1
        Me.labRMfilepath.Text = "labRMfilepath"
        '
        'labTargetCount
        '
        Me.labTargetCount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labTargetCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.labTargetCount.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTargetCount.Location = New System.Drawing.Point(561, 278)
        Me.labTargetCount.Name = "labTargetCount"
        Me.labTargetCount.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labTargetCount.Size = New System.Drawing.Size(104, 29)
        Me.labTargetCount.TabIndex = 3
        Me.labTargetCount.Text = "labTargetCount"
        Me.labTargetCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(561, 261)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Target "
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(561, 326)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Copy Progress"
        '
        'labProgress
        '
        Me.labProgress.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labProgress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.labProgress.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labProgress.Location = New System.Drawing.Point(561, 343)
        Me.labProgress.Name = "labProgress"
        Me.labProgress.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labProgress.Size = New System.Drawing.Size(104, 29)
        Me.labProgress.TabIndex = 4
        Me.labProgress.Text = "labProgress"
        Me.labProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.Location = New System.Drawing.Point(660, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(374, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Import data from this RM Database:"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(558, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Target POS Database:"
        '
        'labTargetDBname
        '
        Me.labTargetDBname.BackColor = System.Drawing.Color.AliceBlue
        Me.labTargetDBname.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTargetDBname.Location = New System.Drawing.Point(558, 104)
        Me.labTargetDBname.Name = "labTargetDBname"
        Me.labTargetDBname.Size = New System.Drawing.Size(207, 30)
        Me.labTargetDBname.TabIndex = 11
        Me.labTargetDBname.Text = "labTargetDBname"
        '
        'labAction
        '
        Me.labAction.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labAction.Location = New System.Drawing.Point(562, 210)
        Me.labAction.Name = "labAction"
        Me.labAction.Size = New System.Drawing.Size(185, 38)
        Me.labAction.TabIndex = 12
        Me.labAction.Text = "labAction"
        '
        'labVersion
        '
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(25, 512)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(247, 17)
        Me.labVersion.TabIndex = 13
        Me.labVersion.Text = "labVersion"
        '
        'txtReport
        '
        Me.txtReport.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtReport.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReport.Location = New System.Drawing.Point(27, 130)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ReadOnly = True
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReport.Size = New System.Drawing.Size(489, 362)
        Me.txtReport.TabIndex = 1
        '
        'frmImportRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(777, 527)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.labAction)
        Me.Controls.Add(Me.labTargetDBname)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.labProgress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.labTargetCount)
        Me.Controls.Add(Me.labRMfilepath)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.txtReport)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmImportRM"
        Me.Text = "frmImportRM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents labRMfilepath As System.Windows.Forms.Label
    Friend WithEvents labTargetCount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents labProgress As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents labTargetDBname As System.Windows.Forms.Label
    Friend WithEvents labAction As System.Windows.Forms.Label
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
