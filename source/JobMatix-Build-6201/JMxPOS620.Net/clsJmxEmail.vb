Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb

Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net.Security
Imports System.ComponentModel
Imports System.Text

Public Class clsJmxEmail

    '- Class to send an email..

    '== JobMatixPOS v3411.1229 = 29-DEc-2017=

    '-- INPUT Mail settings from JobMatix..-
    Private mColMailSettings As Collection

    '--  Callers textbox controls..
    Private mLabEmailStatus As Label

    '--SAVE original SMTP Mail SystemInfo Settings..
    Private msSMTPHostName As String = ""
    Private mIntSMTPHostPort As Integer = -1
    Private mbSMTPHostUsesSSL As Boolean = False

    Private msSMTPUsername As String = ""
    Private msSMTPPassword As String = ""

    Private msBusinessShortname As String = ""

    Private Shared mbSendingMail As Boolean = False
    Private Shared mbMailSent As Boolean = False
    Private Shared mbMailCancelled As Boolean = False
    Private Shared msSendErrorText As String = ""

    Private Shared WithEvents mSmtpClient1 As SmtpClient
    Private Shared mMessage As MailMessage

    Private mMailFrom As MailAddress
    Private Shared msSMTPResultMsg As String = ""
    '= = = = = = = = = = = = = =  = = = = = = = = = 
    '= = = = = = = = = = = = = = = = = = = = = = = = ==  == = = 
    '-===FF->

    '-CertificateValidationCallBack-

    Private Function My_CertificateValidationCallBack(sender As Object, _
                                                certificate As System.Security.Cryptography.X509Certificates.X509Certificate, _
                                                 chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                                                   sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean

        '-- // If the certificate is a valid, signed certificate, return true.
        If (sslPolicyErrors = System.Net.Security.SslPolicyErrors.None) Then

            Return True
        End If
        '-- // If there are errors in the certificate chain, look at each error to determine the cause.
        If ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) <> 0) Then

            If (Not IsDBNull(chain)) AndAlso (Not IsDBNull(chain.ChainStatus)) Then
                '=  (chain <> null And chain.ChainStatus <> null) Then

                For Each status As System.Security.Cryptography.X509Certificates.X509ChainStatus In chain.ChainStatus
                    If ((certificate.Subject = certificate.Issuer) And
                       (status.Status = System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot)) Then
                        '-// Self-signed certificates with an untrusted root are valid. 
                        Continue For
                    Else
                        '=  {
                        If (status.Status <> System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError) Then
                            '- // If there are any other errors in the certificate chain, the certificate is invalid,
                            '- // so the method returns false.
                            Return False
                        End If
                    End If
                Next status
                '--// When processing reaches this line, the only errors in the certificate chain are 
                '--// untrusted root errors for self-signed certificates. These certificates are valid
                '-- // for default Exchange server installations, so return true.
                '=-TEST =
                '== MsgBox("We are accepting a self-signed Server Certificate.", MsgBoxStyle.Exclamation)
                Return True
            Else
                '-- // In all other cases, return false.
                Return False
            End If
        End If  '-chain null-

    End Function  '- CertificateValidationCallBack-
    '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->

    '- add msg to status
    Private Sub mSubUpdateStatus(ByVal sMsg As String, _
                                 Optional ByVal bClearFirst As Boolean = False)

        If mLabEmailStatus IsNot Nothing Then

            mLabEmailStatus.Text &= sMsg & vbCrLf

        End If  '-nothing-

    End Sub  '-update-
    '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->

    '- P u b l i c  M e t h o d s -
    '- P u b l i c  M e t h o d s -

    '-- Constructor- I n i t ----

    Public Sub New(ByRef colMailSettings As Collection, _
                    ByRef labEmailStatus As Label)


        MyBase.New()

        mColMailSettings = colMailSettings  '-save-

        '--  load SMTP Mail SystemInfo Settings..
        '--  SMTPServer     --
        '--  SMTPUsername   --
        '--  SMTPPassword   --

        '-- save individual settings..
        msSMTPHostName = mColMailSettings.Item("smtphostname")
        mIntSMTPHostPort = mColMailSettings.Item("smtphostport")
        mbSMTPHostUsesSSL = mColMailSettings.Item("smtphostusesssl")
        msSMTPUsername = mColMailSettings.Item("smtpusername")
        msSMTPPassword = mColMailSettings.Item("smtpPassword")
        msBusinessShortname = mColMailSettings.Item("businessshortname")

        '-controls 
        mLabEmailStatus = labEmailStatus


        '-- setup Callback..
        ServicePointManager.ServerCertificateValidationCallback = _
                            New RemoteCertificateValidationCallback(AddressOf My_CertificateValidationCallBack)

        '- now we can create client..
        mSmtpClient1 = New SmtpClient(msSMTPHostName, mIntSMTPHostPort)
        '--username-
        mMailFrom = New MailAddress(msSMTPUsername, msBusinessShortname)
        '= txtEmailFrom.Text = msBusinessShortname & "  <" & msSMTPUsername & ">"


    End Sub '--init..--
    '= = = = = = = = = = = = =
    '-===FF->

    '- Send Email that is provided in parms...

    Public Function SendEmail(ByVal strTxtEmailTo As String, _
                                 ByVal strTxtEmailSubject As String, _
                                 ByVal strTxtEmailtext As String, _
                                 ByVal strAttachmentFullPath As String) As Boolean

        Dim intError As Integer = 0
        Dim intStart, intLast As Integer
        Dim sUpdateText As String = ""
        '==    Dim client As New SmtpClient(msSMTPServer)     '==  (args(0))

        SendEmail = False
        ' Specify the e-mail sender.
        ' Create a mailing address that includes a UTF8 character
        ' in the display name.
        '== Dim [from] As New MailAddress(msSMTPUsername, msBusinessShortname)
        If (strTxtEmailTo = "") OrElse (InStr(strTxtEmailTo, "@") < 1) Then
            MsgBox("No valid destination user name..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (Trim(strTxtEmailSubject) = "") Or (Trim(strTxtEmailtext) = "") Then
            MsgBox("Msg Subject or msg Text must not be empty..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '==  mMailFrom = New MailAddress(msSMTPUsername, msBusinessShortname)
        ' Set destinations for the e-mail message.
        Dim [to] As New MailAddress(strTxtEmailTo) '==("ben@contoso.com")
        sUpdateText = "To: " & strTxtEmailTo & vbCrLf
        ' Specify the message content.
        '== Dim message As New MailMessage([from], [to])
        Dim smtpCred As NetworkCredential

        smtpCred = New NetworkCredential(msSMTPUsername, msSMTPPassword)
        mSmtpClient1.Credentials = smtpCred
        mSmtpClient1.Port = mIntSMTPHostPort
        mSmtpClient1.EnableSsl = mbSMTPHostUsesSSL

        mMessage = New MailMessage(mMailFrom, [to])
        mMessage.Body = strTxtEmailtext  '== "This is a test e-mail message sent by an application. "

        '== ' Include some non-ASCII characters in body and subject.
        '== Dim someArrows As New String(New Char() {ChrW(&H2190), ChrW(&H2191), ChrW(&H2192), ChrW(&H2193)})
        '= message.Body += Environment.NewLine & someArrows

        mMessage.BodyEncoding = Encoding.UTF8
        mMessage.Subject = strTxtEmailSubject  '== "test message 1" & someArrows
        mMessage.SubjectEncoding = Encoding.UTF8
        sUpdateText = sUpdateText & "Subject: " & strTxtEmailSubject & vbCrLf
        sUpdateText = sUpdateText & "Message: " & strTxtEmailtext

        '-add attachment if any..
        If strAttachmentFullPath <> "" Then
            Dim attach1 As New Attachment(strAttachmentFullPath)
            mMessage.Attachments.Add(attach1)
        End If
 
        ' Set the method that is called back when the send operation ends.
        '== AddHandler mSmtpClient1.SendCompleted, AddressOf SendEmailCompletedCallback

        ' The userState can be any object that allows your callback 
        ' method to identify this send operation.
        ' For this example, the userToken is a string constant.
        Dim userState As String = "test message1"

        mLabEmailStatus.Text = "Preparing to send message.."
        msSMTPResultMsg = ""
        '= cmdCancel.Enabled = False
        '= cmdClose.Enabled = False
        mbSendingMail = False
        mbMailSent = False
        mbMailCancelled = False
        msSendErrorText = ""
        '== On Error GoTo cmdSendEmail_Error

        Try
            mSmtpClient1.SendAsync(mMessage, userState)
        Catch ex As Exception
            MsgBox("Error:  Can't initiate Send" & vbCrLf & ex.Message)
            '= cmdClose.Enabled = True
            ' Clean up.
            mMessage.Dispose()
            Exit Function
        End Try
        '==Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.")

        '--  started ok..--
        mLabEmailStatus.Text = "Found host: " & msSMTPHostName & vbCrLf & _
                                   "Send message started..." & vbCrLf & vbCrLf & _
                                     " Press Cancel Email to cancel.."
        mbSendingMail = True
        SendEmail = True


        '== User can WAIT 60 secs= --


        'intStart = CInt(VB.Timer()) '--starting seconds.-
        'intLast = CInt(VB.Timer()) '--starting seconds.-
        'While mbSendingMail And (CInt(VB.Timer()) < intStart + 60)  '--  VB.Timer returns a DOUBLE..-
        '    '--  integral part is seconds..
        '    If (intLast <> CInt(VB.Timer())) Then  '--update display every second..-
        '        '= labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
        '        Call mSubUpdateStatus("Wait: " & (60 - (intLast - intStart)))  '= labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
        '        intLast = CInt(VB.Timer()) '--starting seconds.-
        '    End If
        '    System.Windows.Forms.Application.DoEvents()
        'End While
        'Call mSubUpdateStatus("", True)  '- and clear.'=mlabEmailTimer.Text = ""

        ''= cmdCancelEmail.Enabled = False
        'If mbSendingMail Then  '--timed out..-
        '    mSmtpClient1.SendAsyncCancel()
        '    mLabEmailStatus.Text = "Send operation timed out.."
        '    MsgBox("Send operation timed out.." & vbCrLf & vbCrLf & _
        '                 "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
        'ElseIf mbMailSent Then
        '    '--completion event..-
        '    '==MsgBox(msSMTPResultMsg, MsgBoxStyle.Information)
        '    mLabEmailStatus.Text = msSMTPResultMsg
        '    mLabEmailStatus.Text &= "Message cancelled."
        'Else    '-error-
        '    MsgBox(msSMTPResultMsg & vbCrLf & vbCrLf & _
        '                 "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
        '    mLabEmailStatus.Text = "Error in Send."
        'End If

        '==  Dim answer As String = Console.ReadLine()
        ' If the user canceled the send, and mail hasn't been sent yet,
        ' then cancel the pending operation.
        '== If answer.StartsWith("c") AndAlso mbMailSent = False Then
        '== client.SendAsyncCancel()
        '== End If
        Exit Function
        '== ERROR goto  NOT USED.-- 
        'cmdSendEmail_Error:
        '        intError = Err().Number
        '        MsgBox("!! Error:  Can't initiate Send.." & vbCrLf & _
        '                         "Error code: " & intError & " = " & ErrorToString(intError), MsgBoxStyle.Exclamation)
        '        '= cmdClose.Enabled = True
        '        ' Clean up.
        '        mMessage.Dispose()
        '        Exit Function

    End Function  '- mbSendEmail-
    '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->

    '-- S e n d  E m a i l --
    '-- S e n d  E m a i l --

    '-- Email Completion Callback..--

    Public Shared Sub SendEmailCompletedCallback(ByVal sender As Object, _
                                                  ByVal ev As AsyncCompletedEventArgs) _
                                                   Handles mSmtpClient1.SendCompleted

        ' Get the unique identifier for this asynchronous operation.
        Dim token As String = CStr(ev.UserState)
        Dim sMsg As String = "Email completion event." & vbCrLf & vbCrLf

        If ev.Cancelled Then
            '== Console.WriteLine("[{0}] Send canceled.", token)
            sMsg = sMsg & "Message cancelled."
            mbMailCancelled = True
        Else
            If ev.Error IsNot Nothing Then
                '= Console.WriteLine("[{0}] {1}", token, e.Error.ToString())
                sMsg = sMsg & "Error in Send:" & vbCrLf & ev.Error.ToString()
                '= MsgBox(sMsg, MsgBoxStyle.Information)  '--TEST-
                msSendErrorText = sMsg
            Else
                '== Console.WriteLine("Message sent.")
                mbMailSent = True
                sMsg = sMsg & "Message sent."
                msSendErrorText = ""
            End If
        End If
        '== frmNotifyCust.labEmailStatus.Text = sMsg

        '== MsgBox(sMsg, MsgBoxStyle.Information)  '--TEST-

        msSMTPResultMsg = sMsg   '--send back..-

        mbSendingMail = False

        ' Clean up.
        mMessage.Dispose()

    End Sub '--SendCompletedCallback-
    '= = = = = 

    '-- function to test completion..

    Public Function SendCompleted(ByRef sErrorText As String) As Boolean

        If (Not mbSendingMail) Then  '-finished mbMailSent Then
            sErrorText = msSendErrorText  '== empty if no error..
            SendCompleted = True
        Else  '-still sending-
            SendCompleted = False
        End If
 
    End Function  '- SendCompleted-
    '= = = = = = = = = = = = = = = == 

    '== Cancel=

    Public Sub cancelEmail()

        'Private Sub cmdCancelEmail_Click(ByVal sender As System.Object, _
        '                                    ByVal e As System.EventArgs) Handles cmdCancelEmail.Click

        Call mSubUpdateStatus("Cancelling send..") '= labEmailStatus.Text = "Cancelling send.."
        DoEvents()
        mSmtpClient1.SendAsyncCancel()
        ' Clean up.
        '== mMessage.Dispose()
    End Sub  '--cancel..-
    '= = = = = = = = = = = = = =    


End Class  '--clsJmxEmail-
'= = = = = = = = = = == =

'== end class=
