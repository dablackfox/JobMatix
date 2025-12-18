Imports System.Windows.Forms

Public Class dlgNoShow

    '==--3083.315..--
    '--   Extend for use as About Version box with RTF text box....
    '==--3311.831..--
    '--   Show Version in form caption...
    '==
    '==  NEW REVISION  4219.1214 VERSION started 08-Dec-2019=
    '==    Updated- 4219.1216 08-Dec-2019= 
    '==      --  Updates to dlgNoShow for AppName..  Can be called from POS also..
    '==
    '= = = = = = = = = = == = = = = = = = = = = = = = = = = = = = ==

    Private mbActivated As Boolean = False
    Private mbIsRichText As Boolean = False
    Private mbHideDoNotShowControl As Boolean = False

    Private msMessage As String = "--"
    Private msSubTitle As String = ""
    Private msMainTitle As String = ""

    Private mbDoNotShow As Boolean = False
    Private mbCanViewWhatsNew As Boolean = False

    Private msJobMatixVersion As String = ""
    '= = = = = = = = = = = = = = = = = = =


    WriteOnly Property Message() As String
        Set(ByVal Value As String)
            '= labMessage.Text = Value
            msMessage = Value
        End Set
    End Property '--message-
    '= = == = = = = = = = = =

    WriteOnly Property SubHdr() As String
        Set(ByVal Value As String)
            '= labMessage.Text = Value
            msSubTitle = Value
        End Set
    End Property '--message-
    '= = == = = = = = = = = =
    '=3103=
    WriteOnly Property MainTitle() As String
        Set(ByVal Value As String)
            '= labMessage.Text = Value
            msMainTitle = Value
        End Set
    End Property '--message-
    '= = == = = = = = = = = =

    '==3083=
    WriteOnly Property isRichText() As Boolean
        Set(ByVal value As Boolean)
            mbIsRichText = value
        End Set
    End Property
    '= = = = = = = = = = = = ==

    WriteOnly Property HideDoNotShowControl() As Boolean
        Set(ByVal value As Boolean)
            mbHideDoNotShowControl = value
        End Set
    End Property
    '= = = = = = = = = = = = = =  =

    WriteOnly Property CanViewWhatsNew() As Boolean
        Set(ByVal value As Boolean)
            mbCanViewWhatsNew = value
        End Set
    End Property  '--  can view -
    '= = = = = = = = =  = = = == =

    '-- result.--

    ReadOnly Property DoNotShow() As Boolean
        Get
            DoNotShow = mbDoNotShow
        End Get
    End Property
    '= = = = = = = = = = = = = = = = = ==  

    '-- l o a d --

    Private Sub dlgNoShow_Load(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles MyBase.Load
        '== labMessage.Text = msMessage
        '== labSubTitle.Text = msSubTitle
        RichTextInfo.Visible = False
        WebBrowser1.Visible = False
        labViewWhatsNew.Visible = False

        msJobMatixVersion = gsGetAppName() & " Info-  v" & CStr(My.Application.Info.Version.Major) & "." & _
                            My.Application.Info.Version.Minor & "; Build: " & _
                              My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
        Me.Text = gsGetAppName() & " Info- " '= msJobMatixVersion

    End Sub  '--  load.-
    '= = = = = = = = = =

    '-- Activated--

    Private Sub dlgNoShow_Activated(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub '-- Activated--
    '= = = = = = = = = = = = = =

    '=4219.1216
    '- -- Shown..

    Private Sub dlgNoShow_Shown(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles MyBase.Shown

        'If mbActivated Then Exit Sub
        'mbActivated = True
        labSubTitle.Text = msSubTitle
        If mbHideDoNotShowControl Then
            chkDoNotShow.Visible = False
        End If
        If mbCanViewWhatsNew Then
            labViewWhatsNew.Visible = True
        End If
        If mbIsRichText Then
            Me.Width = 620  '=590
            Me.Height = 700
            Me.BackColor = Color.WhiteSmoke    '=  LightGoldenrodYellow
            labMessage.Visible = False
            labSubTitle.Visible = False
            labMainTitle.Text = msMainTitle  '="About JobMatix Build 3083"
            '==RichTextInfo.Top = labMessage.Top
            '== RichTextInfo.Height = 360
            '== RichTextInfo.Rtf = msMessage
            '== RichTextInfo.Visible = True
            WebBrowser1.Top = labSubTitle.Top   '== labMessage.Top
            WebBrowser1.Height = 550  '=360
            WebBrowser1.Width = Me.Width - 32
            WebBrowser1.DocumentText = msMessage
            WebBrowser1.Visible = True
            OK_Button.Top = Me.Height - 65
            OK_Button.Left = Me.Width - OK_Button.Width - 40
            chkDoNotShow.Top = Me.Height - 65
            labViewWhatsNew.Top = chkDoNotShow.Top
            Call CenterForm(Me)      '-- center with new size..-

        Else  '--normal-
            labMessage.Text = msMessage
            labMessage.Height = 180
            '== WebBrowser1.DocumentText = _
            '== "<html><body>Please enter your name:<br/>" & _
            '== "<input type='text' name='userName'/><br/>" & _
            '== "<a href='http://www.microsoft.com'>continue</a>" & _
            '== "</body></html>"
            Call CenterForm(Me)
        End If
    End Sub  '--  SHOW_ was Activated.-
    '= = = = = = = = = = = = = = = = = =


    Private Sub OK_Button_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        WebBrowser1.Dispose()
        Me.Hide()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        WebBrowser1.Dispose()
        Me.Hide()
    End Sub

    Private Sub chkDoNotShow_CheckedChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles chkDoNotShow.CheckedChanged
        mbDoNotShow = chkDoNotShow.Checked
    End Sub

End Class
