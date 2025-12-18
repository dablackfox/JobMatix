Imports System.Windows.Forms


Friend Module modMyMsgBox

    '-- grh 11-Sep-2020..
    '==
    '-- Implements new function myMessageBox()  -

    Friend Function myMessageBox(ByVal strMessage As String, _
                                 Optional ByVal strHeader As String = "New message", _
                                 Optional ByVal strTitle As String = "") As DialogResult
        Dim dlgMsg As Form
        Dim txtMsg As TextBox
        Dim labHdr As Label
        Dim btnOK As Button
        Dim btnCancel As Button

        Dim sText() As String '--message


        sText = Split(strMessage, vbCrLf)

        '- Form-
        dlgMsg = New Form
        dlgMsg.FormBorderStyle = FormBorderStyle.FixedDialog
        '= dlgMsg.Height = 600
        '= dlgMsg.Width = 400
        '= dlgMsg.BackColor = Color.White

        '-label-
        labHdr = New Label
        With labHdr
            .Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, _
                                                             System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            .Top = 10
            .Left = 20
            .BackColor = Color.LightGoldenrodYellow
            .Name = "labHdr"
            .Width = 430
            .Height = 20
            .AutoSize = False
        End With

        '--text box-
        txtMsg = New TextBox
        txtMsg.Height = 320
        txtMsg.Width = 430
        txtMsg.BackColor = Color.WhiteSmoke
        txtMsg.ReadOnly = True
        txtMsg.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, _
                                                  System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        txtMsg.Top = 40
        txtMsg.Left = 20
        txtMsg.Multiline = True
        txtMsg.TabStop = False
        txtMsg.BorderStyle = BorderStyle.None
        If (sText.Length > 6) Then
            txtMsg.ScrollBars = ScrollBars.Vertical
        Else
            txtMsg.ScrollBars = ScrollBars.None
        End If

        '--button-
        btnOK = New Button
        With btnOK
            '=.Location = New System.Drawing.Point(362, 500)
            .Name = "btnOK"
            .Size = New System.Drawing.Size(90, 28)
            .TabIndex = 4
            .Text = "-OK-"
            '= .UseVisualStyleBackColor = True
            .DialogResult = DialogResult.OK
            .FlatStyle = FlatStyle.Flat
            .BackColor = Color.WhiteSmoke
        End With  '-button-

        '-not visible..  just for esc ?
        btnCancel = New Button
        '=btnCancel.Visible = False
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.BackColor = Color.White  '-- not visible.
        btnCancel.ForeColor = Color.Black
        btnCancel.FlatStyle = FlatStyle.Flat

        btnCancel.Width = 20
        btnCancel.Height = 28

        '-- Form-
        With dlgMsg
            .AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            .AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            .BackColor = System.Drawing.Color.White
            .ClientSize = New System.Drawing.Size(470, 440)
            .MaximizeBox = False

            .Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, _
                                                            System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            .Name = "dlgMsg"
            .Controls.Add(labHdr)
            .Controls.Add(txtMsg)
            .Controls.Add(btnOK)
            .Controls.Add(btnCancel)

            .CancelButton = btnCancel
            .Text = strTitle
 
        End With

        labHdr.Text = strHeader
        btnOK.Left = dlgMsg.Width - btnOK.Width - 80
        btnOK.Top = txtMsg.Top + txtMsg.Height + 26

        btnCancel.Left = btnOK.Left + btnOK.Width + 10
        btnCancel.Top = btnOK.Top
        btnCancel.Text = "X"

        '- show message..
        txtMsg.Text = ""
        For Each s1 As String In sText
            txtMsg.Text &= s1 & vbCrLf
        Next

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler dlgMsg.Load, AddressOf dlgMsg_Load

        '= Call CenterForm(dlgMsg)
        myMessageBox = dlgMsg.ShowDialog()

        dlgMsg.Dispose()

    End Function '-myMessageBox-
    '= = = == =  = = == = == = = = =

    '-- Load event..-

    Private Sub dlgMsg_Load(sender As Object, ev As EventArgs)
        Dim dlgMsg As Form = CType(sender, Form)

        '-- center form..
        dlgMsg.Top = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - dlgMsg.Height) \ 2)
        dlgMsg.Left = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - dlgMsg.Width) \ 2)


    End Sub  '-load-
    '= = = == = = = = = 


End Module  '-modMyMsgBox-
'= = == = = = = = = = = = = =
