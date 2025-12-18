Option Strict Off
Option Explicit On
Public Class frmWait

    '== 3107.1213 = 13Dec2015=  Updated form size..
    '== 3311.128 = 28Feb2016.  Updated version..

    '== 6201.0609 = 09June2021.  Open Source version..

    Private mbActivated As Boolean = False

    '--  catch and signal ESC.-
    Private mbLoginRequested As Boolean = False

    Private msTitle As String = ""

    '-- input-
    WriteOnly Property waitTitle As String
        Set(value As String)
            msTitle = value
        End Set
    End Property  '-title
    '= = = = = = = = = = = = = = = = = = = =


    '--  results.--
    ReadOnly Property loginRequested() As Boolean
        Get
            loginRequested = mbLoginRequested
        End Get
    End Property '--Get cancelled--
    '= = = =  = =  = = = = = = = = =

    '-- CenterForm  --
    '-- CenterForm  --

    Private Sub CenterForm(ByVal xForm As System.Windows.Forms.Form)
        With xForm
            If .WindowState = System.Windows.Forms.FormWindowState.Normal Then
                .Top = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - .Height) \ 2)
                .Left = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - .Width) \ 2)
            End If
        End With
    End Sub
    '= = = = = = = =  = = = =  = = = =
    '= = = = = = = =  = = = =  = = = =
    '-===FF->


    '- Load--

    Private Sub frmWait_Load(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) Handles MyBase.Load
        Call CenterForm(Me)
        If (msTitle <> "") Then
            Me.Text = msTitle
        Else  '-default-
            Me.Text = "JobMatix62"
        End If
        '== labHdr.Text = "JobMatix"

        Me.KeyPreview = True

    End Sub  '--load-
    '= = = = = = = = =  =

    '- activate--
    Private Sub frmWait_Activated(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True


    End Sub  '--load-
    '= = = = = = = = =  =


    '-- KeyPress..-
    Private Sub frmWait_KeyPress(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                        Handles MyBase.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 27 Then '--ESC-
            mbLoginRequested = True
            keyAscii = 0
            '= Labstatus.Text = ""
            Me.KeyPreview = False
        End If

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = = = = == =

End Class