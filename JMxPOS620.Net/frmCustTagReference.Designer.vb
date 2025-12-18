<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustTagReference
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustTagReference))
        Me.panelReference = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnAddToList = New System.Windows.Forms.Button()
        Me.labMainHdr = New System.Windows.Forms.Label()
        Me.txtNewTags = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnSaveRefList = New System.Windows.Forms.Button()
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.listRefTags = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelReference.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelReference
        '
        Me.panelReference.Controls.Add(Me.Label4)
        Me.panelReference.Controls.Add(Me.btnAddToList)
        Me.panelReference.Controls.Add(Me.labMainHdr)
        Me.panelReference.Controls.Add(Me.txtNewTags)
        Me.panelReference.Controls.Add(Me.Label3)
        Me.panelReference.Controls.Add(Me.btnCancel)
        Me.panelReference.Controls.Add(Me.Label2)
        Me.panelReference.Controls.Add(Me.btnMoveDown)
        Me.panelReference.Controls.Add(Me.btnMoveUp)
        Me.panelReference.Controls.Add(Me.btnSaveRefList)
        Me.panelReference.Controls.Add(Me.labHdr1)
        Me.panelReference.Controls.Add(Me.listRefTags)
        Me.panelReference.Location = New System.Drawing.Point(13, 41)
        Me.panelReference.Name = "panelReference"
        Me.panelReference.Size = New System.Drawing.Size(541, 517)
        Me.panelReference.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 401)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 14)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Add some new Tags."
        '
        'btnAddToList
        '
        Me.btnAddToList.Location = New System.Drawing.Point(293, 472)
        Me.btnAddToList.Name = "btnAddToList"
        Me.btnAddToList.Size = New System.Drawing.Size(57, 28)
        Me.btnAddToList.TabIndex = 2
        Me.btnAddToList.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.btnAddToList, "Add To Ref. List")
        Me.btnAddToList.UseVisualStyleBackColor = True
        '
        'labMainHdr
        '
        Me.labMainHdr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMainHdr.ForeColor = System.Drawing.Color.Gray
        Me.labMainHdr.Location = New System.Drawing.Point(385, 208)
        Me.labMainHdr.Name = "labMainHdr"
        Me.labMainHdr.Size = New System.Drawing.Size(140, 219)
        Me.labMainHdr.TabIndex = 12
        Me.labMainHdr.Text = resources.GetString("labMainHdr.Text")
        '
        'txtNewTags
        '
        Me.txtNewTags.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewTags.Location = New System.Drawing.Point(22, 420)
        Me.txtNewTags.MaxLength = 300
        Me.txtNewTags.Multiline = True
        Me.txtNewTags.Name = "txtNewTags"
        Me.txtNewTags.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNewTags.Size = New System.Drawing.Size(250, 80)
        Me.txtNewTags.TabIndex = 1
        Me.txtNewTags.Text = "txtNewTags"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Gainsboro
        Me.Label3.Location = New System.Drawing.Point(369, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 456)
        Me.Label3.TabIndex = 14
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(415, 31)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(57, 28)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(384, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 101)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "See Individual Customer Admin to view/edit Current List of Tags for any particula" & _
    "r Customer:.."
        '
        'btnMoveDown
        '
        Me.btnMoveDown.BackColor = System.Drawing.Color.Lavender
        Me.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMoveDown.Image = CType(resources.GetObject("btnMoveDown.Image"), System.Drawing.Image)
        Me.btnMoveDown.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMoveDown.Location = New System.Drawing.Point(293, 224)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(45, 50)
        Me.btnMoveDown.TabIndex = 4
        Me.btnMoveDown.Text = "Down"
        Me.btnMoveDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnMoveDown, "Move Item Down")
        Me.btnMoveDown.UseVisualStyleBackColor = False
        '
        'btnMoveUp
        '
        Me.btnMoveUp.BackColor = System.Drawing.Color.Lavender
        Me.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMoveUp.Image = CType(resources.GetObject("btnMoveUp.Image"), System.Drawing.Image)
        Me.btnMoveUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMoveUp.Location = New System.Drawing.Point(293, 151)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(45, 50)
        Me.btnMoveUp.TabIndex = 3
        Me.btnMoveUp.Text = "Up"
        Me.btnMoveUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnMoveUp, "Move Item Up")
        Me.btnMoveUp.UseVisualStyleBackColor = False
        '
        'btnSaveRefList
        '
        Me.btnSaveRefList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveRefList.Location = New System.Drawing.Point(293, 325)
        Me.btnSaveRefList.Name = "btnSaveRefList"
        Me.btnSaveRefList.Size = New System.Drawing.Size(57, 46)
        Me.btnSaveRefList.TabIndex = 5
        Me.btnSaveRefList.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveRefList, "Save Reference List")
        Me.btnSaveRefList.UseVisualStyleBackColor = True
        '
        'labHdr1
        '
        Me.labHdr1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(19, 21)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHdr1.Size = New System.Drawing.Size(253, 96)
        Me.labHdr1.TabIndex = 7
        Me.labHdr1.Text = "This is the reference list of pre-defined customer-related TAGS..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--Double Cl" & _
    "ick on an item to Edit it.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--Use the DEL key to delete an item." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--Press ""Save" & _
    """ when done.."
        '
        'listRefTags
        '
        Me.listRefTags.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listRefTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listRefTags.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listRefTags.FormattingEnabled = True
        Me.listRefTags.ItemHeight = 12
        Me.listRefTags.Location = New System.Drawing.Point(22, 129)
        Me.listRefTags.Name = "listRefTags"
        Me.listRefTags.Size = New System.Drawing.Size(235, 242)
        Me.listRefTags.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(266, 19)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Pre-defined Customer Reference Tags"
        '
        'frmCustTagReference
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(566, 570)
        Me.Controls.Add(Me.panelReference)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCustTagReference"
        Me.Text = "frmCustTagReference"
        Me.panelReference.ResumeLayout(False)
        Me.panelReference.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelReference As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSaveRefList As System.Windows.Forms.Button
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents listRefTags As System.Windows.Forms.ListBox
    Friend WithEvents labMainHdr As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNewTags As System.Windows.Forms.TextBox
    Friend WithEvents btnAddToList As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
