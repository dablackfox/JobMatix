Option Strict Off
Option Explicit On
Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net.Security
Imports System.Threading
Imports System.ComponentModel
Imports System.Text
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb

Friend Class frmNotifyCust
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = = =
	
	'--NOTIFY.. REv-2776..-
	'---  with SMS functionality..--
    '---- grh-- 03-Oct-2010-==== New Form for SMS Notification..--
	'---- grh-- 04-Oct-2010-==== Build into Ver: 2478...--
	'---- grh-- 15-Oct-2010-==== Ver: 2482.. Confirm New password...--
	'---- grh-- 19-Oct-2010-==== Ver: 2485.. Fix  "Sent OK" message..-
	'---- grh--15-Nov-2010-==== JOBMATIX -- Ver: 2785.. Fix  re-vamp form...-
	'---- grh--12-Jan-2011-==== Ver: 2796.. Fix SQL Update for Quotes...-
	'---- grh--03-Apr-2011-==== Ver: 2804.. New SMS Gateway:  "www.smsboss.com.au"--...-
	'---- grh--09-Aug-2011-==== Ver: 2918/2919.. Send Final Notify text back to caller.-
	
    '-- grh--=26-Nov-2011-==== VB.NET UPGRADED version..-
    '-- grh--=28-Dec-2011-==== inet class converted to WebRequest.-
    '-- grh--=03-Mar-2012-====Scripting.Dictionary converted to: clsStrDictionary..-

    '-- grh--=03-Apr-2012-====Clear some frame captions...-

    '== 13-Apr-2012 = Build-3043.0 ==
    '==    DROP all Reference-Update functions..--
    '==    Add EMAIL function..

    '== 20-Apr-2012 = Build-3043.1 ==
    '==    Fix for empty SMTP settings... 
    '= = = = = = = = = = = = = = = = = = = = = = = = =

    '== 
    '==  grh. JobMatix 3.1.3101 ---  18-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '== 
    '==  grh. JobMatix 3.1.3101.1029 ---  catch Error in SMS POST ===
    '==
    '==  grh. JobMatix 3.1.3107.518 --- Notifications column now 4000. ===
    '==
    '==  grh JobMatix 3.1.3107.1013-  13-Oct-2015
    '==    >> For Gavin (LE Charlestown). Notify Cust. (SMS) 
    '==            Allow user to insert Cust Name in SMS.. (keywords &&FIRSTNAME, &&LASTNAME)
    '==    >> Accept collection of RM Cust Info as input..
    '==
    '==   grh 3107.1213-  13-Dec-2015=
    '==       >>   Fix to set smskeys combo selectedIndex to -1 (Not selected)... 
    '==                 So User is forced to select a message. This ensures SMS Injection.
    '==
    '==   grh 3311.225-  25-Feb-2016=
    '==       >>  Moved over to clsSystemInfo.. 
    '==
    '==   grh 3311.330-  30-Mar-2016=
    '==       >>  Email- max text length fixed from 144 to 4000.. 
    '==
    '==  -- 3311.728- 28July2016-
    '==         >>  Adding TWO new SMS Gateways.. "smsBroadcast" and "smsGlobal"..
    '==             Now has new systemInfo Key: 'smsGatewayHostName' ("smsBoss", "smsBroadcast" or "smsGlobal")
    '==
    '==   v3.4.3403.0531 -- 31may2017= x-
    '==         -- Fixes to (frmNotifyCust) SendMail to capture InvalidCertificate Callback...
    '==
    '==   3411.0113- 13Jan2018=
    '==    -- Re-instate the Update Job.. for SMS SEnt ok..
    '==
    '==  DLL version of JobTracking.
    '==    3501.0610 -- Some event form references removed For JobTracking now being a DLL..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    Private Structure xxxx_URL
        Dim Scheme As String
        Dim Host As String
        Dim Port As Integer
        Dim URI As String
        Dim Query As String
    End Structure ''-url-
    '== = = = = = = = = = = = = =

    Private mbIsInitialising As Boolean = False

    Private mbActivated As Boolean = False

    Private mCnnJobs As OleDbConnection '==  ADODB.Connection
    Private msBusinessShortname As String = ""

    Private msSmsGatewayAddress As String = ""
    Private msSmsGatewayHostName As String = ""
    Private msSmsOriginalUsername As String = ""
    Private msSmsOriginalPassword As String = ""
	
    Private mbNotifyCancelled As Boolean = False
    Private mbNotifiedOK As Boolean = False
	
    '== Private mSdSystemInfo As clsStrDictionary '== Scripting.Dictionary
    '== Private mColSystemInfo As Collection
    '==3311.225= 
    Private mSysInfo1 As clsSystemInfo

    Private mlJobId As Integer = -1
    Private msCustomerName As String = ""
    Private msCustomerEmail As String = ""

    '-= 3107.1013=
    Private mColRMCustomerDetails As Collection
    Private msCustomerFirstName As String = ""
    Private msCustomerLastName As String = ""
    Private msCustomerSalutation As String = ""


	'==Private msCustomerPhone As String
	'==Private msCustomerMobile As String
	
    Private msStaffName As String = ""
    '===Private msReasonText As String

    '==3043.0= 
    Private msGatewayURL As String = ""
    Private msSmsUsername As String = ""
    Private msSmsPassword As String = ""
	
	Private masSMSTexts() As String '--reference texts correspond to combo keys..--
	
	'--HTTP--
    '== Private inet1 As New inetcafe '--http inetcafe class..

    '== Private inet1 As New inetWebRequest '--OUR http .NET class..

    Private mbConnected As Boolean = False
    Private mbError As Boolean = False
    Private mStrDataReceived As String = ""
	
    Private mbPasswordChanged As Boolean = False
    Private mbSmsOnly As Boolean = False
	'= = = = = = = = = = = = = = = = = =
	
    Private msFinalText As String = ""
    '= = = = = = = = = = = =  = = = =

    '--SAVE original SMTP Mail SystemInfo Settings..
    Private msSMTPHostName As String = ""
    Private mIntSMTPHostPort As Integer = -1
    Private mbSMTPHostUsesSSL As Boolean = False

    Private msSMTPUsername As String = ""
    Private msSMTPPassword As String = ""

    Private Shared mbSendingMail As Boolean = False
    Private Shared mbMailSent As Boolean = False
    Private Shared mbMailCancelled As Boolean = False

    Private Shared WithEvents mSmtpClient1 As SmtpClient
    Private Shared mMessage As MailMessage

    Private mMailFrom As MailAddress
    Private Shared msSMTPResultMsg As String = ""

    '= = = = = = = = = = = = = =  = = = = = = = = = 

    '--  properties.--
	
    WriteOnly Property connection() As OleDbConnection '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--connection..--
	'= = = = = = = ===
	
	WriteOnly Property JobId() As Integer
		Set(ByVal Value As Integer)
			
			mlJobId = Value
        End Set
	End Property '---job..-
	'= = = = = = = = = = =
	
	WriteOnly Property Previous() As String
		Set(ByVal Value As String)
			
			txtNotifications.Text = Value
			
		End Set
	End Property '--previous.-
	'= = = = = =  =  == = = == ==
	
	'--  text from job status..- (ready-to-start OR completed ).-
	WriteOnly Property Reason() As String
		Set(ByVal Value As String)
			
			txtReason.Text = Value
			
		End Set
	End Property '--item text.-
	'= = = = = =  =  == = = == ==
	
	WriteOnly Property StaffName() As String
		Set(ByVal Value As String)
			
			msStaffName = Value
			
		End Set
	End Property '--staff.-
	'= = = = = = = = = = = == ==
	
	WriteOnly Property CustomerName() As String
		Set(ByVal Value As String)
			
			msCustomerName = Value
			
		End Set
	End Property '--previous.-
	'= = = = = = = = = = = == ==
	
	WriteOnly Property CustomerPhone() As String
		Set(ByVal Value As String)
			
			txtCustomerPhone.Text = Value
			
		End Set
	End Property '--previous.-
	'= = = = = = = = = = = == ==
	
	WriteOnly Property CustomerMobile() As String
		Set(ByVal Value As String)
			
			txtCustomerMobile.Text = Value
			
		End Set
	End Property '--previous.-
	'= = = = = = = = = = = == ==

    WriteOnly Property CustomerEmail() As String
        Set(ByVal Value As String)

            msCustomerEmail = Value
            txtEmailTo.Text = msCustomerEmail

        End Set
    End Property '--previous.-
    '= = = = = = = = = = = == ==

	WriteOnly Property SmsOnly() As Boolean
		Set(ByVal Value As Boolean)
			
			mbSmsOnly = Value
		End Set
	End Property '--mbSmsOnly--
	'= = = = = =  = = = == =
	
	'===Property Let SmsGatewayURL(s1 As String)
	
	'===   txtGatewayURL.Text = s1
	
	'===End Property  '--previous.-
	'= = = = = = = = = = = == ==
    WriteOnly Property RMCustomerDetails() As Collection
        Set(ByVal Value As Collection)
            mColRMCustomerDetails = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

	'-- results..--
	'-- results..--
	
	ReadOnly Property cancelled() As Boolean
		Get
			
			cancelled = mbNotifyCancelled
			
		End Get
	End Property '--mbNotifyCancelled --
	'= = = = = = = = = = = = = = =
	
	ReadOnly Property FinalText() As String
		Get
			
			FinalText = msFinalText
			
		End Get
	End Property '--final text..-
    '= = = = = = = = = = =  =
    '-===FF->
	
	'-- conversions --
	'--  clean up sql string data ..--
	Private Function msFixSqlStr(ByRef sInstr As String) As String
		
		msFixSqlStr = Replace(sInstr, "'", "''")
		
	End Function '--fixSql-
	'= = = = = = = = = = = =
	'-===FF->
	
	'-- returns type URL from a string--
	'-- returns type URL from a string--
	
    Private Function ExtractUrl(ByVal strURL As String) As xxxx_URL
        Dim intPos1 As Short
        Dim intPos2 As Short

        Dim retURL As xxxx_URL

        '-- 1 look for a scheme it ends with ://
        intPos1 = InStr(strURL, "://")

        If intPos1 > 0 Then
            retURL.Scheme = Mid(strURL, 1, intPos1 - 1)
            strURL = Mid(strURL, intPos1 + 3)
        End If

        '-- 2 look for a port
        intPos1 = InStr(strURL, ":")
        intPos2 = InStr(strURL, "/")

        If intPos1 > 0 And intPos1 < intPos2 Then
            '-- a port is specified
            retURL.Host = Mid(strURL, 1, intPos1 - 1)

            If (IsNumeric(Mid(strURL, intPos1 + 1, intPos2 - intPos1 - 1))) Then
                retURL.Port = CShort(Mid(strURL, intPos1 + 1, intPos2 - intPos1 - 1))
            End If
        ElseIf intPos2 > 0 Then
            retURL.Host = Mid(strURL, 1, intPos2 - 1)
        Else
            retURL.Host = strURL
            retURL.URI = "/"

            ExtractUrl = retURL
            Exit Function
        End If

        strURL = Mid(strURL, intPos2)

        '-- find a question mark ?
        intPos1 = InStr(strURL, "?")

        If intPos1 > 0 Then
            retURL.URI = Mid(strURL, 1, intPos1 - 1)
            retURL.Query = Mid(strURL, intPos1 + 1)
        Else
            retURL.URI = strURL
        End If
        ExtractUrl = retURL
    End Function '--extract--
	'= = = = = = = = = = =  =
	'-===FF->
	
	'--- url encodes a string..--
	'--- url encodes a string..--
	
    Private Function URLEncode(ByVal str_Renamed As String) As String
        Dim intLen As Short
        Dim x As Short
        Dim curChar As Integer
        Dim newStr As String

        intLen = Len(str_Renamed)
        newStr = ""

        '-- encode anything which is not a letter or number
        For x = 1 To intLen
            curChar = Asc(Mid(str_Renamed, x, 1))
            If curChar = 32 Then
                '-- we can use a + sign for a space--
                newStr = newStr & "+"
            ElseIf (curChar < 48 Or curChar > 57) And (curChar < 65 Or curChar > 90) And (curChar < 97 Or curChar > 122) Then
                newStr = newStr & "%" & Hex(curChar)
            Else
                newStr = newStr & Chr(curChar)
            End If
        Next x
        URLEncode = newStr
    End Function '--encode.-
	'= = = = =  = = = = = =
	
	'-- decodes a url encoded string--
    'UPGRADE_NOTE: str was upgraded to str_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'

    Private Function UrlDecode(ByVal str_Renamed As String) As String
        Dim intLen As Short
        Dim x As Short
        Dim curChar As New VB6.FixedLengthString(1)
        Dim strCode As New VB6.FixedLengthString(2)

        Dim newStr As String

        intLen = Len(str_Renamed)
        newStr = ""
        For x = 1 To intLen
            curChar.Value = Mid(str_Renamed, x, 1)

            If curChar.Value = "%" Then
                strCode.Value = "&h" & Mid(str_Renamed, x + 1, 2)
                If IsNumeric(strCode.Value) Then
                    curChar.Value = Chr(Int(CDbl(strCode.Value)))
                Else
                    curChar.Value = ""
                End If
                x = x + 2
            End If
            newStr = newStr & curChar.Value
        Next x

        UrlDecode = newStr
    End Function '--decode..--
	'= = = = = = =  = =  = =
	'-===FF->
	
	Private Function mbDisablePhoneNotify() As Integer
		
		FramePhoneResult.Enabled = False
		OptNotified(0).Enabled = False
		OptNotified(1).Enabled = False
		OptNotified(2).Enabled = False
		cmdOK.Enabled = False
		
	End Function '--disable phone..-
	'= = = = = = = = = = = = ==
	
	
	Private Function mbEnablePhoneNotify() As Integer
		
		FramePhoneResult.Enabled = True
		OptNotified(0).Enabled = True
		OptNotified(1).Enabled = True
		OptNotified(2).Enabled = True
		cmdOK.Enabled = True
		
	End Function '--disable phone..-
	'= = = = = = = = = = = = ==
	'-===FF->
	
	'--mbDisableSMS--
	
	Private Function mbDisableSMS() As Boolean
		
		cboSmsKeys.Enabled = False
        cmdSend.Enabled = False
		
        '== txtNewKey.Text = ""
        '== txtNewKey.Enabled = False

		txtSMS.Text = ""
		txtSMS.Enabled = False
		ChkJobNo.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-
		ChkJobNo.Enabled = False
		
		FrameSMS.Enabled = False
		
	End Function '--disable..-
    '= = = = = = = = = = = =

    '-- insert Cust. names in place of tokens.
    '-- case NOT sensitive..

    Private Function msInsertCustNames(ByVal strMsgText As String) As String
        Dim sResult As String
        sResult = Replace(strMsgText, "&&TITLE", msCustomerSalutation, , , CompareMethod.Text)
        sResult = Replace(sResult, "&&firstname", msCustomerFirstName, , , CompareMethod.Text)
        sResult = Replace(sResult, "&&lastname", msCustomerLastName, , , CompareMethod.Text)

        msInsertCustNames = sResult

    End Function  '--insert--
    '= = = = = = = = = = = = = = = =
	'-===FF->

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

	'-- load systemInfo settings..--
	'-- load systemInfo settings..--
	'---  send back a collection of collections (rows..)--
	
    '=3311.225= Private Function mbLoadsystemInfo(ByRef cnnSQL As OleDbConnection, _
    '=3311.225=                                   ByRef colInfo As Collection, _
    '=3311.225=                                    ByRef sdSystemInfo As clsStrDictionary) As Boolean

    '=3311.225=     mbLoadsystemInfo = gbLoadsystemInfo(cnnSQL, colInfo, sdSystemInfo)

    '=3311.225=      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '=3311.225= End Function '--load info..--.-
	'= = = = = =
	'-===FF->
	
	'-- Refresh..--
	
	Private Function mbRefreshSMS() As Boolean
        '= Dim col1 As Collection
		Dim lngCount As Integer
		
		mbRefreshSMS = False
		
		'--load combo of SMS keys..-
		cboSmsKeys.Items.Clear()
		Erase masSMSTexts
		lngCount = 0
        For Each sKey1 As String In mSysInfo1.keys  '= col1 In mColSystemInfo
            If LCase(VB.Left(sKey1, 8)) = "smstext_" Then '--standard message text..-
                cboSmsKeys.Items.Add(Mid(sKey1, 9))
                ReDim Preserve masSMSTexts(lngCount)
                masSMSTexts(lngCount) = mSysInfo1.item(sKey1)  '= col1.Item("systemvalue")
                lngCount = lngCount + 1
            End If
        Next sKey1  '= col1 '--col1..-
		If lngCount > 0 Then
            '== cmdDelete.Enabled = True
            '== cmdUpdate.Enabled = True
		Else
            '== cmdDelete.Enabled = False
            '== cmdUpdate.Enabled = False
		End If
		mbRefreshSMS = True
		
	End Function '--refresh.-
	'= = = = = = =  =  = =
    '-===FF->

    '-- Refresh all settings..--

    Private Function mbRefreshAll() As Boolean
        '= Dim col1 As Collection
        Dim s1, sValue As String

        mbRefreshAll = False
        '-- get systemInfo stuff..--
        '--  refresh SMS messages..
        If Not mbRefreshSMS() Then
            MsgBox("No SMS info..", MsgBoxStyle.Exclamation)
            '==Me.Hide
            '==Exit Sub
        End If

        '--save settings.--
        '==3311.728= Must find chosen Gateway Host-

        If mSysInfo1.exists("SmsGatewayHostName") AndAlso _
         (mSysInfo1.item("SmsGatewayHostName") <> "") Then
            msSmsGatewayHostName = mSysInfo1.item("SmsGatewayHostName")
        Else   '--selection not defined yet.. must use ORIGINAL-
            msSmsGatewayHostName = "smsBoss"
        End If

        For Each sKey1 As String In mSysInfo1.keys  '= col1 In mColSystemInfo
            sValue = Trim(mSysInfo1.item(sKey1))
            If LCase(sKey1) = "smscentralgatewayurl" Then
                '--save gateway..--
                '= s1 = sValue '= Trim(col1.Item("systemvalue"))
                '= If (s1 <> "") Then msGatewayURL = s1
            ElseIf (LCase(sKey1) = "smscentralgatewayusername") Then
                msSmsUsername = sValue '= Trim(col1.Item("systemvalue"))
            ElseIf (LCase(sKey1) = "smscentralgatewaypassword") Then
                msSmsPassword = sValue  '= Trim(col1.Item("systemvalue"))
            End If '--gateaway..-
        Next sKey1  '= col1 '--col1-
        '==3043.0= txtSmsPassword(0).Enabled = True

        '--  load SMTP Mail SystemInfo Settings..
        '--  SMTPServer     --
        '--  SMTPUsername   --
        '--  SMTPPassword   --
        For Each sKey1 As String In mSysInfo1.keys  '= col1 In mColSystemInfo
            sValue = Trim(mSysInfo1.item(sKey1))
            If LCase(sKey1) = "smtphostname" Then '--standard message text..-
                msSMTPHostName = sValue  '= col1.Item("systemvalue")
            ElseIf LCase(sKey1) = "smtphostport" Then '--standard message text..-
                mIntSMTPHostPort = CInt(sValue)
            ElseIf LCase(sKey1) = "smtphostusesssl" Then '--standard message text..-
                mbSMTPHostUsesSSL = IIf(UCase(VB.Left(sValue, 1)) = "Y", True, False)
            ElseIf LCase(sKey1) = "smtpusername" Then '--standard message text..-
                msSMTPUsername = sValue  '= col1.Item("systemvalue")
            ElseIf LCase(sKey1) = "smtppassword" Then '--standard message text..-
                msSMTPPassword = sValue  '=  col1.Item("systemvalue")
            ElseIf LCase(sKey1) = "businessshortname" Then '--standard message text..-
                msBusinessShortname = Replace(sValue, "_", "")
            End If
        Next sKey1 '=col1 '--col1..-
        '== txtEmailFrom.Text = mMailFrom.DisplayName & "<" & mMailFrom.Address & ">"

        labSMTPHost.Text = msSMTPHostName & ":" & mIntSMTPHostPort
        If mbSMTPHostUsesSSL Then
            labSMTPHost.Text = labSMTPHost.Text & vbCrLf & "(Using SSL)"
        End If

        labEmailStatus.Text = ""

        If (msSMTPHostName = "") Or (mIntSMTPHostPort <= 0) Or (msSMTPUsername = "") Then
            cmdSendEmail.Visible = False
            labEmailStatus.Text = "SMTP/Email Details are not complete.."
        Else  '-ok.-
            '==   v3.4.3403.0531 -- 31may2017= x-
            '==         -- Fixes to (frmNotifyCust) SendMail to capture InvalidCertificate Callback...

            '-- setup Callback..
            ServicePointManager.ServerCertificateValidationCallback = _
                                New RemoteCertificateValidationCallback(AddressOf My_CertificateValidationCallBack)

            '- now we can create client..
            mSmtpClient1 = New SmtpClient(msSMTPHostName, mIntSMTPHostPort)
            '--username-
            mMailFrom = New MailAddress(msSMTPUsername, msBusinessShortname)
            txtEmailFrom.Text = msBusinessShortname & "  <" & msSMTPUsername & ">"
        End If
        mbRefreshAll = True
    End Function  '--mbRefreshAll--
    '= = = = = = =  = = = = = = = =
    '-===FF->

    '---  Update Job record to add Notification text..-
    '---  Update Job record to add Notification text..-

    Private Function mbUpdateNotification(ByRef sText As String, ByRef lngJobId As Integer) As Boolean
        Dim L1 As Integer
        Dim sItem As String
        Dim sSql As String
        Dim sErrorMsg As String

        mbUpdateNotification = False

        sItem = VB6.Format(Today, "dd-mmm-yy") & ", " & VB6.Format(TimeOfDay, "hh:mm") & "; " & _
                                                              msStaffName & "; (" & txtReason.Text & ") " & vbCrLf & sText
        sSql = "UPDATE [jobs] set Notifications=RIGHT((Notifications + '" & msFixSqlStr(sItem) & "'+ CHAR(13)+ CHAR(10) ),4000) "
        sSql = sSql & " WHERE (job_id=" & CStr(lngJobId) & ") "

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(mCnnJobs, sSql, L1, sErrorMsg) Then
            MsgBox("Failed to update DB JOB Notification details.." & "SQL was:" & vbCrLf & _
                                                          sSql & vbCrLf & vbCrLf & sErrorMsg & vbCrLf, MsgBoxStyle.Critical)
        Else '--ok--
            If (L1 > 0) Then
                If gbDebug Then
                    MsgBox("Job Notifications were updated OK.. " & vbCrLf & "SQL was:" & vbCrLf & _
                                                       sSql & vbCrLf & "(" & L1 & " rows affected.)", MsgBoxStyle.Information)
                End If
                txtNotifications.Text = txtNotifications.Text & sItem '--REv-2476--
                msFinalText = sItem '--for caller...-
                mbUpdateNotification = True
            Else
                MsgBox("No records were updated ..", MsgBoxStyle.Exclamation)
            End If
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--update..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- ORIGINAL Load --
    '-- Load --

    Private Sub mbOriginal_frmNotifyCust_Load()

        '== mbNotifyCancelled = False
        '== mbNotifiedOK = False
        '== mlJobId = -1

        cboSmsKeys.Items.Clear()
        cboSmsKeys.Enabled = False

        '==3043.0= cmdAdd.Enabled = False
        cmdSend.Enabled = False
        '== mbConnected = False

        '==ChkJobNo.Value = 0    '--unchecked..-

        '== txtGatewayURL.Text = ""
        '== txtCustomerPhone.Text = ""
        '== txtCustomerMobile.Text = ""
        mStrDataReceived = ""

        '== msSmsOriginalUsername = ""
        '== msSmsOriginalPassword = ""

        '-- SET default gateway..--
        '==3043.0= txtGatewayURL.Text = "http://www.smsboss.com.au/api/sms.asmx/SendSMS"
        '==3043.0= txtSmsUsername.Text = "" '--- "accounts@precisepcs.com"  '--PRECISE TEST.. Default..-
        '==3043.0= txtSmsPassword(0).Text = "" '--- "sjb61sjb"
        '==3043.0= txtSmsPassword(1).Text = "" '--- Confirm..-
        '== mbPasswordChanged = False

        msGatewayURL = "http://www.smsboss.com.au/api/sms.asmx/SendSMS"
        msSmsUsername = "" '--- "accounts@precisepcs.com"  '--PRECISE TEST.. Default..-
        msSmsPassword = "" '--- "sjb61sjb"


        '== txtReason.Text = ""
        '== mbSmsOnly = False
        Call mbDisablePhoneNotify()
        '== FrameNotification.Text = ""

        FramePhoneResult.Text = ""
        frameEmail.Text = ""
        labEmailStatus.Text = ""
        cmdSendEmail.Enabled = False
        cmdCancelEmail.Enabled = False

        labEmailTimer.Text = ""

        Call CenterForm(Me)
    End Sub '--load --
    '= = = = = = = = == =
    '-===FF->

    '-- Load-  EX Activate..-

    Private Sub frmNotifyCust_Load(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '= If mbActivated Then Exit Sub
        '= mbActivated = True
 
        '3311.225=- load system info stuff.
        mSysInfo1 = New clsSystemInfo(mCnnJobs)

        Call mbOriginal_frmNotifyCust_Load()
        Me.Text = "Customer Notification.."
        If gbDebug Then
            LabStatus.Visible = True
        Else
            LabStatus.Visible = False
        End If

        LabHdr.Text = msCustomerName & vbCrLf & " (Job No: " & mlJobId & ").  "

        Call mbDisableSMS()

        Call mbRefreshAll()

        '==3043.0= txtSmsPassword(0).Enabled = False '--lockout change event..-
        '==3043.0= txtSmsPassword(1).Visible = False
        '==3043.0= LabConfirm.Visible = False

        LabStatus.Text = ""
        '-- get systemInfo stuff..--

        '==  enable PHONE.. -- '==3043.0= 
        '--  (was option button..)--
        Call mbEnablePhoneNotify()

        '==  enable SMS.. -- '==3043.0= 
        '--  (was option button..)--
        FrameSMS.Enabled = True
        cboSmsKeys.SelectedIndex = -1
        '==cmdOK.Enabled = False
        txtSMS.Text = ""
        txtSMS.Enabled = True
        If (cboSmsKeys.Items.Count > 0) Then
            cboSmsKeys.Enabled = True
            cboSmsKeys.SelectedIndex = -1  '== 3107.1213= Force user to select.  Was 0 ==
        Else
            cboSmsKeys.Enabled = False
            '== cmdUpdate.Enabled = False
            '== cmdDelete.Enabled = False
        End If
        '== txtNewKey.Text = ""
        '== txtNewKey.Enabled = True
        ChkJobNo.CheckState = System.Windows.Forms.CheckState.Checked '--checked..-
        ChkJobNo.Enabled = True
        txtSmsDest.Text = txtCustomerMobile.Text '--default..--
        '--  done enable..--

        If mbSmsOnly Then '-- only sms..--
            OptNotified(0).Enabled = False
            OptNotified(1).Enabled = False
            OptNotified(2).Enabled = False
            '====Call OptNotified_Click(3)
            If cboSmsKeys.Enabled Then
                cboSmsKeys.Focus()
            Else
                '==3043.0=     txtNewKey.Focus()
            End If 'cbo..-
        Else '--all notify..-
            '==OptNotified(0).SetFocus
        End If

        If gbDebug Then
            MsgBox("SMS Gateway: " & msGatewayURL & vbCrLf & _
                   "SMS Username: " & msSmsUsername & vbCrLf & _
                   "SMS Password: " & msSmsPassword, MsgBoxStyle.Information)
        End If

        txtEmailSubject.Text = txtReason.Text
        txtEmailText.Text = txtReason.Text

        txtReason.SelectionStart = txtReason.TextLength
        txtReason.SelectionLength = 0
        txtReason.Focus()

        '-= 3107.1013=
        '--  Ssetup customer first/last names for Gavin-
        If Not (mColRMCustomerDetails Is Nothing) Then
            If mColRMCustomerDetails.Contains("given_names") Then
                msCustomerFirstName = mColRMCustomerDetails.Item("given_names")("value")
            End If
            If mColRMCustomerDetails.Contains("surname") Then
                msCustomerLastName = mColRMCustomerDetails.Item("surname")("value")
            End If
            If mColRMCustomerDetails.Contains("salutation") Then
                msCustomerSalutation = mColRMCustomerDetails.Item("salutation")("value")
            End If
            '- show details.
            LabHdr.Text &= "(" & msCustomerSalutation & " " & msCustomerFirstName & " " & msCustomerLastName & ")"
        End If  '-details-

    End Sub '--LOAD --
    '= = = = = = = = == =
    '-===FF->

    '--PHONE options click..--

    'UPGRADE_WARNING: Event OptNotified.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub OptNotified_CheckedChanged(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles OptNotified.CheckedChanged
        If mbIsInitialising Then Exit Sub
        If eventSender.Checked Then
            Dim index As Short = OptNotified.GetIndex(eventSender)
            '==3043.0=         If (index = 3) Then '--SMS--
            '==3043.0=             FrameSMS.Enabled = True
            '==3043.0=             cboSmsKeys.SelectedIndex = -1
            '==3043.0=             cmdOK.Enabled = False
            '==3043.0=             txtSMS.Text = ""
            '==3043.0=             txtSMS.Enabled = True
            '==3043.0=             If (cboSmsKeys.Items.Count > 0) Then
            '==3043.0=                 cboSmsKeys.Enabled = True
            '==3043.0=                 cboSmsKeys.SelectedIndex = 0
            '==3043.0=             Else
            '==3043.0=                 cboSmsKeys.Enabled = False
            '==3043.0= '== cmdUpdate.Enabled = False
            '==3043.0= '== cmdDelete.Enabled = False
            '==3043.0=             End If
            '==3043.0= '== txtNewKey.Text = ""
            '==3043.0= '== txtNewKey.Enabled = True
            '==3043.0=             ChkJobNo.CheckState = System.Windows.Forms.CheckState.Checked '--checked..-
            '==3043.0=             ChkJobNo.Enabled = True
            '==3043.0=             txtSmsDest.Text = txtCustomerMobile.Text '--default..--
            '==3043.0=         Else '--rang..-
            '==3043.0=             Call mbDisableSMS()
            '==3043.0=             cmdOK.Enabled = True
            '==3043.0=         End If
            cmdClose.Enabled = False
        End If
    End Sub '--opt..--
    '= = = = = = = = = = = =
    '-===FF->

    '--  Rang-- OK..--
    '--  Rang-- OK..--
    '--   =3043.0=-  JUST save phone result..-

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        Dim ix, index As Integer
        Dim sItem As String

        index = -1
        sItem = ""
        '-- Get message..  --
        For ix = 0 To 2
            If OptNotified(ix).Checked = True Then
                sItem = OptNotified(ix).Text & vbCrLf
                index = ix
            End If
        Next ix
        '-- UPDATE Job..  --
        If (index >= 0) Then
            If mbUpdateNotification(sItem, mlJobId) Then
                '=== MsgBox "DB Updated ok..", vbInformation
            Else
                MsgBox("Job not updated..", MsgBoxStyle.Exclamation)
            End If
            cmdOK.Enabled = False
            cmdClose.Enabled = True
        End If
        '-- and Exit..--
        '== Me.Hide()
    End Sub '-ok-
    '= = = = = = = = =
    '-===FF->

    '-- SMS --
    '-- SMS --
    '-- SMS --

    Private Sub cboSmsKeys_Enter(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs)

        '== txtNewKey.Text = ""
        '== cmdAdd.Enabled = False
        '==3043.0 ==  cmdSend.Enabled = True

    End Sub '--got focus.-
    '= = = = = = = = = = = =

    'UPGRADE_WARNING: Event cboSmsKeys.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboSmsKeys_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                 ByVal eventArgs As System.EventArgs) _
                                Handles cboSmsKeys.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        With cboSmsKeys
            If (.SelectedIndex >= 0) Then '--have selection..-
                txtSMS.Text = masSMSTexts(.SelectedIndex)
                txtSMS.Enabled = True

                '=3107.1013= -- replace Name tokens if any..
                txtSMS.Text = msInsertCustNames(txtSMS.Text)

                '== cmdUpdate.Enabled = True
                '== cmdDelete.Enabled = True
                cmdSend.Enabled = True
                cmdSend.Focus()
                ChkJobNo.Enabled = True
                cmdClose.Enabled = False
            End If

        End With
    End Sub '--changed.-
    '= = = = = = =  = = = = =
    '-===FF->


    '-- Edit Reference table..--
    '-- Edit Reference table..--

    Private Sub cmdEditReference_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles cmdEditReference.Click

        Dim frmSMSUpdate1 As New frmSMSUpdate

        If mbIsInitialising Then Exit Sub
        frmSMSUpdate1.connection = mCnnJobs
        frmSMSUpdate1.ShowDialog()
        frmSMSUpdate1.Close()
        frmSMSUpdate1.Dispose()

        Call mbRefreshAll()

    End Sub  '--EditReference--
    '= = = = = = = = = = = = = 
    '-===FF->

    '-- SMS  S E N D---
    '-- SMS  S E N D---
    '== 3311.404==  Using InetSubs..
    '== 3311.404==  Using InetSubs..

    Private Sub cmdSend_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdSend.Click
        Dim sJobText As String
        Dim sMessageSent As String
        Dim sHttpText As String
        Dim sHttpTextMessage As String
        Dim sHeaders As String
        Dim strURL As String
        Dim iPos2, ix, iPos, lngResult As Integer
        '= Dim strStatusLine As String
        '==3311.405=Dim inet1 As inetWebRequest '--OUR http .NET class..

        Dim strResultStatus As String
        Dim strPostException, strResultText As String
        '===Dim strUsername As String, strPassword As String
        '==3311.405= Dim DestUrl As URL
        '==3311.405= Dim sXmlStatus As String
        '==3311.405= Dim sXmlErrorMsg As String

        '==3311.728- Check Gateway.

        If (Trim(msSmsUsername) = "") Or (Trim(msSmsPassword) = "") Then
            MsgBox("Note: The Gateway Username or Password " & _
                                   " are not valid values..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If (Trim(txtSmsDest.Text) = "") Then
            MsgBox("Note: The Destination phone field " & _
                                   " must be completed with non-blank values..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '--  add JobNo if wanted..--
        sJobText = ""
        '===strUsername = "test@smscentral.com.au"
        '===strPassword = "2P@ssword"
        If (mlJobId > 0) And (ChkJobNo.CheckState = 1) Then
            sJobText = "- JobNo: " & mlJobId & "."
        End If
        '-- IN CASE Name tokens entered manually..
        '=3107.1013= -- replace Name tokens if any..
        txtSMS.Text = msInsertCustNames(txtSMS.Text)

        If MsgBox("OK to send the SMS text:" & vbCrLf & vbCrLf & "<< " & txtSMS.Text & sJobText & " >>" & vbCrLf & _
                       vbCrLf & "To: " & vbCrLf & msCustomerName & vbCrLf & _
                             "Mobile No: " & txtSmsDest.Text & "..?" & vbCrLf & _
                             "   (via Gateway: '" & msSmsGatewayHostName & "')", _
                                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            '-- SEND..--
            '== 3311.404==  Using InetSubs..
            Dim sResultInfo As String = ""
            If Not gbSendSMS(msSmsGatewayHostName, msSmsUsername, msSmsPassword, _
                                txtSMS.Text & sJobText, txtSmsDest.Text, sResultInfo, LabStatus) Then
                MsgBox("failed to send SMS-" & vbCrLf & sResultInfo, MsgBoxStyle.Exclamation)
            Else
                '--ok.. Re-instate the Update Job..
                '= 3411.0113- 13Jan2018=
                If mbUpdateNotification("-SMS Sent: " & txtSMS.Text & sJobText & vbCrLf, mlJobId) Then
                    MsgBox("SMS was sent OK.. " & vbCrLf & _
                                         "  and DB Job Record was updated ok..", MsgBoxStyle.Information)
                Else
                    MsgBox("Job not updated..", MsgBoxStyle.Exclamation)
                End If
                MsgBox("Text Sent ok.." & vbCrLf & sResultInfo, vbInformation)
            End If  '- gbSend-
            '== The rest is now GONE (redundant).

        End If '--yes.-
        cmdSend.Enabled = False
        cmdClose.Enabled = True
        cmdCancel.Enabled = False
    End Sub '-SMS Send-
    '= = = = = = = = =
    '-===FF->

    '--  E m a i l --
    '--  E m a i l --

    Private Sub txtEmailTo_TextChanged(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles txtEmailTo.TextChanged

        If mbIsInitialising Then Exit Sub
        If (InStr(txtEmailTo.Text, "@") > 1) And (txtEmailText.Text <> "") Then

            cmdSendEmail.Enabled = True
        End If
    End Sub  '-txtEmailTo-
    '== = = = = = = = = = = =

    Private Sub txtEmailText_TextChanged(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles txtEmailText.TextChanged

        If mbIsInitialising Then Exit Sub
        If (InStr(txtEmailTo.Text, "@") > 1) Then

            cmdSendEmail.Enabled = True
        End If
    End Sub  '--txtEmailText-
    '== = = = = = = = = = = =
    '-===FF->

    '-- S e n d  E m a i l --
    '-- S e n d  E m a i l --

    '-- Email Completion Callback..--

    Public Shared Sub SendEmailCompletedCallback(ByVal sender As Object, _
                                                  ByVal e As AsyncCompletedEventArgs) Handles mSmtpClient1.SendCompleted

        ' Get the unique identifier for this asynchronous operation.
        Dim token As String = CStr(e.UserState)
        Dim sMsg As String = "Email completion event." & vbCrLf & vbCrLf

        If e.Cancelled Then
            '== Console.WriteLine("[{0}] Send canceled.", token)
            sMsg = sMsg & "Message cancelled."
            mbMailCancelled = True
        Else
            If e.Error IsNot Nothing Then
                '= Console.WriteLine("[{0}] {1}", token, e.Error.ToString())
                sMsg = sMsg & "Error in Send:" & e.Error.ToString()
                '= MsgBox(sMsg, MsgBoxStyle.Information)  '--TEST-
            Else
                '== Console.WriteLine("Message sent.")
                mbMailSent = True
                sMsg = sMsg & "Message sent."
            End If
        End If
        '== frmNotifyCust.labEmailStatus.Text = sMsg

        '== MsgBox(sMsg, MsgBoxStyle.Information)  '--TEST-

        msSMTPResultMsg = sMsg   '--send back..-

        mbSendingMail = False

        '-- These form references removed For JobTracking now being a DLL..
        'frmNotifyCust.cmdCancelEmail.Enabled = False
        'frmNotifyCust.cmdClose.Enabled = True
        'frmNotifyCust.txtEmailTo.Enabled = True
        'frmNotifyCust.txtEmailSubject.Enabled = True
        'frmNotifyCust.txtEmailText.Enabled = True

        ' Clean up.
        mMessage.Dispose()

    End Sub '--SendCompletedCallback-
    '= = = = = 

    Private Sub cmdCancelEmail_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles cmdCancelEmail.Click

        labEmailStatus.Text = "Cancelling send.."
        DoEvents()
        mSmtpClient1.SendAsyncCancel()
        ' Clean up.
        '== mMessage.Dispose()
    End Sub  '--cancel..-
    '= = = = = = = = = = = = = =    

    '- send email..

    Private Sub cmdSendEmail_Click(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles cmdSendEmail.Click

        Dim intError As Integer = 0
        Dim intStart, intLast As Integer
        Dim sUpdateText As String = ""
        '==    Dim client As New SmtpClient(msSMTPServer)     '==  (args(0))

        ' Specify the e-mail sender.
        ' Create a mailing address that includes a UTF8 character
        ' in the display name.
        '== Dim [from] As New MailAddress(msSMTPUsername, msBusinessShortname)
        If (txtEmailTo.Text = "") OrElse (InStr(txtEmailTo.Text, "@") < 1) Then
            MsgBox("No valid destination user name..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If (Trim(txtEmailSubject.Text) = "") Or (Trim(txtEmailText.Text) = "") Then
            MsgBox("Msg Subject or msg Text must not be empty..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '==  mMailFrom = New MailAddress(msSMTPUsername, msBusinessShortname)
        ' Set destinations for the e-mail message.
        Dim [to] As New MailAddress(txtEmailTo.Text) '==("ben@contoso.com")
        sUpdateText = "To: " & txtEmailTo.Text & vbCrLf
        ' Specify the message content.
        '== Dim message As New MailMessage([from], [to])
        Dim smtpCred As NetworkCredential

        smtpCred = New NetworkCredential(msSMTPUsername, msSMTPPassword)
        mSmtpClient1.Credentials = smtpCred
        mSmtpClient1.Port = mIntSMTPHostPort
        mSmtpClient1.EnableSsl = mbSMTPHostUsesSSL

        mMessage = New MailMessage(mMailFrom, [to])
        mMessage.Body = txtEmailText.Text  '== "This is a test e-mail message sent by an application. "

        '== ' Include some non-ASCII characters in body and subject.
        '== Dim someArrows As New String(New Char() {ChrW(&H2190), ChrW(&H2191), ChrW(&H2192), ChrW(&H2193)})
        '= message.Body += Environment.NewLine & someArrows

        mMessage.BodyEncoding = Encoding.UTF8
        mMessage.Subject = txtEmailSubject.Text  '== "test message 1" & someArrows
        mMessage.SubjectEncoding = Encoding.UTF8
        sUpdateText = sUpdateText & "Subject: " & txtEmailSubject.Text & vbCrLf
        sUpdateText = sUpdateText & "Message: " & txtEmailText.Text

        ' Set the method that is called back when the send operation ends.
        '== AddHandler mSmtpClient1.SendCompleted, AddressOf SendEmailCompletedCallback

        ' The userState can be any object that allows your callback 
        ' method to identify this send operation.
        ' For this example, the userToken is a string constant.
        Dim userState As String = "test message1"

        labEmailStatus.Text = "Preparing to send message.."
        msSMTPResultMsg = ""
        cmdCancel.Enabled = False
        cmdClose.Enabled = False
        mbSendingMail = False
        mbMailSent = False
        mbMailCancelled = False
        '== On Error GoTo cmdSendEmail_Error

        Try
            mSmtpClient1.SendAsync(mMessage, userState)
        Catch ex As Exception
            MsgBox("Error:  Can't initiate Send" & vbCrLf & ex.Message)
            cmdClose.Enabled = True
            ' Clean up.
            mMessage.Dispose()
            txtEmailTo.Enabled = True
            txtEmailSubject.Enabled = True
            txtEmailText.Enabled = True
            Exit Sub
        End Try
        '==Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.")

        '--  started ok..--
        labEmailStatus.Text = "Found host: " & msSMTPHostName & vbCrLf & _
                                   "Send message started..." & vbCrLf & vbCrLf & _
                                     " Press Cancel Email to cancel.."
        cmdSendEmail.Enabled = False
        cmdCancelEmail.Enabled = True
        mbSendingMail = True
        txtEmailTo.Enabled = False
        txtEmailSubject.Enabled = False
        txtEmailText.Enabled = False
        cmdCancelEmail.Focus()

        '== WAIT 60 secs= --
        intStart = CInt(VB.Timer()) '--starting seconds.-
        intLast = CInt(VB.Timer()) '--starting seconds.-
        While mbSendingMail And (CInt(VB.Timer()) < intStart + 60)  '--  VB.Timer returns a DOUBLE..-
            '--  integral part is seconds..
            If (intLast <> CInt(VB.Timer())) Then  '--update display every second..-
                labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
                intLast = CInt(VB.Timer()) '--starting seconds.-
            End If
            System.Windows.Forms.Application.DoEvents()
        End While
        labEmailTimer.Text = ""

        cmdCancelEmail.Enabled = False
        If mbSendingMail Then  '--timed out..-
            mSmtpClient1.SendAsyncCancel()
            labEmailStatus.Text = "Send operation timed out.."
            MsgBox("Send operation timed out.." & vbCrLf & vbCrLf & _
                         "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
        ElseIf mbMailSent Then
            '--completion event..-
            '==MsgBox(msSMTPResultMsg, MsgBoxStyle.Information)
            labEmailStatus.Text = msSMTPResultMsg
            '--  update DB..-
            '-- UPDATE Job..  --
            If mbUpdateNotification("-EMAIL Sent: " & vbCrLf & sUpdateText & vbCrLf, mlJobId) Then
                MsgBox("Email was sent OK.. " & vbCrLf & _
                                     "  and DB Job Record was updated ok..", MsgBoxStyle.Information)
            Else
                MsgBox("Job record not updated..", MsgBoxStyle.Exclamation)
            End If
        ElseIf mbMailCancelled Then
            labEmailStatus.Text = "Message cancelled."
        Else    '-error-
            MsgBox(msSMTPResultMsg & vbCrLf & vbCrLf & _
                         "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
            labEmailStatus.Text = "Error in Send."
        End If

        '--update status in case completion couldn't access it..
        txtEmailTo.Enabled = True
        txtEmailSubject.Enabled = True
        txtEmailText.Enabled = True

        '--  can try again..
        cmdSendEmail.Enabled = True
        cmdClose.Enabled = True
        cmdCancel.Enabled = False

        '==  Dim answer As String = Console.ReadLine()
        ' If the user canceled the send, and mail hasn't been sent yet,
        ' then cancel the pending operation.
        '== If answer.StartsWith("c") AndAlso mbMailSent = False Then
        '== client.SendAsyncCancel()
        '== End If
        Exit Sub
        '== Console.WriteLine("Goodbye.")

        '== ERROR goto  NOT USED.-- 
cmdSendEmail_Error:
        intError = Err().Number
        MsgBox("!! Error:  Can't initiate Send.." & vbCrLf & _
                         "Error code: " & intError & " = " & ErrorToString(intError), MsgBoxStyle.Exclamation)
        cmdClose.Enabled = True
        ' Clean up.
        mMessage.Dispose()
        txtEmailTo.Enabled = True
        txtEmailSubject.Enabled = True
        txtEmailText.Enabled = True
        Exit Sub
    End Sub  '--send email..-
    '= = = = = = = = =
    '-===FF->

    '-- Cancel..-

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        mbNotifyCancelled = True
        Me.Close()


    End Sub
    '= = = = = = = = = =

    '=== end form =====

    Private Sub cmdClose_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

End Class
'=== end form =====
'=== end form =====

'-- Web interface to replace INETCAF3.cls..---
'-- Web interface to replace INETCAF3.cls..---
'-- Web interface to replace INETCAF3.cls..---

'-- Derived  FROM msdn Web class examples..--

'=3311.404= -- GONE from here.. See seoarate class file. inetWebRequest.vb -
'=3311.404= -- GONE from here.. See seoarate class file.
'=3311.404=-- GONE from here.. See seoarate class file.
'=3311.404=-- GONE from here.. See seoarate class file.

Public Class xxxxxxxx_inetWebRequest

    Private mbResponseOpen As Boolean = False

    Private msAuthenticateUserId As String = ""
    Private msAuthenticatePassword As String = ""

    Private msRemoteHost As String
    Private msPostURL As String = ""
    Private msHeaderContentType As String = ""

    Private mPostRequest1 As WebRequest
    Private mResponse1 As WebResponse

    Private msLastExceptionText As String = ""

    '--  property parameters..--
    '--  property parameters..--

    WriteOnly Property authenticateUserId() As String
        Set(ByVal Value As String)
            msAuthenticateUserId = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property authenticatePassword() As String
        Set(ByVal Value As String)
            msAuthenticatePassword = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property remoteHost() As String
        Set(ByVal Value As String)
            msRemoteHost = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property postURL() As String
        Set(ByVal Value As String)
            msPostURL = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property ContentType() As String
        Set(ByVal Value As String)
            msHeaderContentType = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    ReadOnly Property LastExceptionText() As String
        Get
            LastExceptionText = msLastExceptionText
        End Get
    End Property '-- get cancelled--
    '= = = = = = = =  = =  ==

    '-- init --
    '-- init --

    Public Sub New()
        MyBase.New()
        '==  Class_Initialize_Renamed()
    End Sub '--initialise..-
    '= = = = = = = = = = = = =

    '--  Public functions..-
    '--  Public functions..-
    '--  Public functions..-

    Public Function inetOpen() As Integer

        inetOpen = -1  '--assume fails..

        Try
            mPostRequest1 = WebRequest.Create(msPostURL)
            Exit Try
        Catch wex As Exception
            msLastExceptionText = wex.Message
            Exit Function
        End Try

        inetOpen = 0  '-ok..-

    End Function  '--open--
    '= = = = = = = = = = = = = = =

    '-  NOT NEEDED..--

    '== Public Function hostConnect() As Integer

    '==     hostConnect = 0  '--ok--
    '== End Function   '--connect-
    '= = = = = = = = = = = = = = 


    '-- PostData  -

    Public Function PostData(ByVal sHttpText As String, _
                                  ByRef strResultText As String) As Integer

        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(sHttpText)
        Dim dataStream As Stream '== = request.GetRequestStream()

        PostData = -1
        msLastExceptionText = ""

        '-- Set the Method property of the request to POST.
        mPostRequest1.Method = "POST"
        mPostRequest1.ContentType = msHeaderContentType
        Try
            dataStream = mPostRequest1.GetRequestStream()
        Catch wex1 As Exception
            msLastExceptionText = "Error in Post fn (GetRequestStream)" & vbCrLf & wex1.Message
            Exit Function
        End Try
        '-- Write the data to the request stream.
        Try
            dataStream.Write(byteArray, 0, byteArray.Length)
        Catch wex2 As Exception
            msLastExceptionText = "Error in Post fn (dataStream.Write)" & vbCrLf & wex2.Message
            Exit Function
        End Try
        '-- Close the Stream object.
        dataStream.Close()
        mbResponseOpen = True
        '--ok..  do it.
        Try
            mResponse1 = mPostRequest1.GetResponse()
            Exit Try
        Catch wex3 As Exception
            msLastExceptionText = "Error in Post fn (GetResponse)" & vbCrLf & wex3.Message
            Exit Function
        End Try

        '-- Get the stream containing content returned by the server.
        dataStream = mResponse1.GetResponseStream()
        '-- Open the stream using a StreamReader for easy access.
        Dim reader As New StreamReader(dataStream)
        '-- Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()

        '-- Display the content.
        '== Console.WriteLine(responseFromServer)
        strResultText = responseFromServer

        ' Clean up the streams.
        reader.Close()
        dataStream.Close()
        '== mResponse1.Close()
        PostData = 0  '--ok.-

    End Function  '--PostData-
    '= = = = = = = = = = = = = 

    '--Q u e r i e s..--
    '--Q u e r i e s..--
    '--Q u e r i e s..--

    '--queryRequestHeaders-
    Public Function queryRequestHeaders() As String

        queryRequestHeaders = ""

    End Function  '--querySRequestHeaders--
    '= = = = = = = = = = = = = = = =


    Public Function queryStatusCode() As String

        queryStatusCode = ""
        If Not (mResponse1 Is Nothing) Then
            queryStatusCode = CType(mResponse1, HttpWebResponse).StatusCode
        End If

    End Function  '--queryStatusCode--
    '= = = = = = = = = = = = = = = =

    '--queryStatusText--
    Public Function queryStatusText() As String

        queryStatusText = "--"
        If Not (mResponse1 Is Nothing) Then
            queryStatusText = CType(mResponse1, HttpWebResponse).StatusDescription
        End If
    End Function  '--queryStatusText--
    '= = = = = = = = = = = = = = = =

    '= '--hostDisConnect-
    '== Public Function hostDisConnect() As Integer

    '==     hostDisConnect = 0  '--ok--
    '== End Function   '--disconnect-
    '= = = = = = = = = = = = = = 

    '--  Close.--
    Public Function inetClose() As Integer

        If mbResponseOpen Then mResponse1.Close()

        inetClose = 0  '--ok--
    End Function   '--close-
    '= = = = = = = = = = = = = = 



    '== '==  Original example..--
    '== Public Shared Sub Main_renamed()
    '== ' Create a request using a URL that can receive a post. 
    '= =Dim request As WebRequest = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ")
    '== ' Set the Method property of the request to POST.
    '==     request.Method = "POST"
    '== ' Create POST data and convert it to a byte array.
    '== Dim postData As String = "This is a test that posts this string to a Web server."
    '== Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
    '== ' Set the ContentType property of the WebRequest.
    '==     request.ContentType = "application/x-www-form-urlencoded"
    '== ' Set the ContentLength property of the WebRequest.
    '==     request.ContentLength = byteArray.Length
    '== ' Get the request stream.
    '== Dim dataStream As Stream = request.GetRequestStream()
    '== ' Write the data to the request stream.
    '==     dataStream.Write(byteArray, 0, byteArray.Length)
    '== ' Close the Stream object.
    '==     dataStream.Close()
    '== ' Get the response.
    '== Dim response As WebResponse = request.GetResponse()
    '== ' Display the status.
    '==     Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
    '== ' Get the stream containing content returned by the server.
    '==     dataStream = response.GetResponseStream()
    '== ' Open the stream using a StreamReader for easy access.
    '== Dim reader As New StreamReader(dataStream)
    '== ' Read the content.
    '== Dim responseFromServer As String = reader.ReadToEnd()
    '== ' Display the content.
    '==     Console.WriteLine(responseFromServer)
    '== ' Clean up the streams.
    '==     reader.Close()
    '==     dataStream.Close()
    '==     response.Close()
    '== End Sub
End Class


'==  the end ===