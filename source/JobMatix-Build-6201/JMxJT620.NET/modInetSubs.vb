Option Strict Off
Option Explicit On
Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
Imports System.Text
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb

Module modInetSubs

    '== grh 04-April-2016-  3311.404 
    '==      Functions for Sending SMS's and Emails..
    '==
    '==  MOSTLY dug out of 'frmNotifyCust22'--
    '==
    '==
    '==  -- 3311.730- 30July2016-
    '==         >>  Adding THREE new SMS Gateways.. "smsBroadcast", "smsGlobal"  and "directSMS"..
    '==             Now has new systemInfo Key: 
    '==                  'smsGatewayName' ("smsBoss", "smsBroadcast" or "smsGlobal" or "directSMS")
    '==
    '= = = = = = = = == = = = =  == = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '- NOT USED !!   sysInfo1 must be an active instance..
    '--  Gives info on SMS providers (servers)
    '-     and SMPT Server and credentials.

    '==  -- 3311.727- 27July2016-
    '--  DEFINE ALL Supported SMS gateWay URL's     HERE..-
    '--  DEFINE ALL Supported SMS gateWay URL's     HERE..-
    '--  DEFINE ALL Supported SMS gateWay URL's     HERE..-

    Public Const k_SMS_GATEWAY_URL_BOSS As String = "http://www.smsboss.com.au/api/sms.asmx/SendSMS"
    Public Const k_SMS_GATEWAY_URL_BROADCAST As String = "https://api.smsbroadcast.com.au/api-adv.php"
    Public Const k_SMS_GATEWAY_URL_GLOBAL As String = "https://www.smsglobal.com/http-api.php"
    Public Const k_SMS_GATEWAY_URL_DIRECTSMS As String = "https://api.directsms.com.au/s3/http/send_message"
    '-- end of sms constants-

    '- For parsing full URL..

    Private Structure URL
        Dim Scheme As String
        Dim Host As String
        Dim Port As Integer
        Dim URI As String
        Dim Query As String
    End Structure ''-url-
    '== = = = = = = = = = = = = =

    '= Private mSysInfo1 As clsSystemInfo

    Private msGatewayURL As String = ""
    Private msSmsUsername As String = ""
    Private msSmsPassword As String = ""

    '-- returns type URL from a string--
    '-- returns type URL from a string--

    Private Function ExtractUrl(ByVal strURL As String) As URL
        Dim intPos1 As Short
        Dim intPos2 As Short

        Dim retURL As URL

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
    '-===FF->

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

    '-- P u b l i c  Functions-
    '-- P u b l i c  Functions-
    '-- P u b l i c  Functions-

    '-g b S e n d S M S-

    '==3311.728=  EXTRA gateways supported..
    '--  First parameters is gateWay name-
    '--   ("smsBoss", "smsBroadcast" or "smsGlobal").

    Public Function gbSendSMS(ByVal strGatewayName As String, _
                              ByVal strGatewayUsername As String, _
                              ByVal strGatewayPassword As String, _
                                ByVal strText As String, _
                                 ByVal strSmsDest1 As String, _
                                    ByRef strUserResultInfo As String, _
                                    Optional ByRef labStatus As Label = Nothing) As Boolean
        '=Dim s1, svalue As String
        Dim strSmsDest As String, sMessageSent As String
        Dim sHttpText, sQueryString As String
        Dim sHttpTextMessage As String
        Dim sHeaders As String
        Dim strURL As String
        Dim iPos2, ix, iPos, lngResult As Integer
        Dim inet1 As inetWebRequest '--OUR http .NET class..

        Dim strResultStatus As String
        Dim strPostException, strResultText As String
        '===Dim strUsername As String, strPassword As String
        Dim DestUrl As URL
        Dim sXmlStatus As String
        Dim sXmlErrorMsg As String

        gbSendSMS = False
        '== mSysInfo1 = New clsSystemInfo(mCnnJobs)

        msGatewayURL = "http://www.smsboss.com.au/api/sms.asmx/SendSMS"
        msSmsUsername = "" '--- "accounts@precisepcs.com"  '--PRECISE TEST.. Default..-
        msSmsPassword = "" '--- "sjb61sjb"

        '=3311.409=  User must provide credentials..=
        msSmsUsername = strGatewayUsername
        msSmsPassword = strGatewayPassword

        '==3311.728=  EXTRA gateways supported..
        '-- All Gateway Host type discovered here from supplied URL.
        Select Case LCase(strGatewayName)
            Case "smsboss"
                msGatewayURL = k_SMS_GATEWAY_URL_BOSS
            Case "smsbroadcast"
                msGatewayURL = k_SMS_GATEWAY_URL_BROADCAST
            Case "smsglobal"
                msGatewayURL = k_SMS_GATEWAY_URL_GLOBAL
            Case "directsms"
                msGatewayURL = k_SMS_GATEWAY_URL_DIRECTSMS
            Case Else
                strUserResultInfo = "SMS Send not possible-  UNKNOWN Host not supported."
                Exit Function
        End Select

        '-- check some parameters..
        If (Trim(msSmsUsername) = "") Or (Trim(msSmsPassword) = "") Then
            '= MsgBox("Note: The Gateway Username or Password " & _
            '=                        " are not valid values..", MsgBoxStyle.Exclamation)
            strUserResultInfo = "Note: The Gateway Username or Password " & _
                                   " are not valid values.."
            Exit Function
        End If
        If (Trim(strSmsDest1) = "") Then
            '= MsgBox("Note: The Destination phone number " & _
            '=                        " must be non-blank..", MsgBoxStyle.Exclamation)
            strUserResultInfo = "Note: The Destination phone number must be non-blank.."
            Exit Function
        End If
        '-- fix dest no..
        strSmsDest = Replace(strSmsDest1, " ", "")
        If Left(strSmsDest, 1) = "0" Then
            strSmsDest = "61" & Mid(strSmsDest, 2)
        End If

        '-- SEND..--
        inet1 = New inetWebRequest '--OUR http .NET class..
        sMessageSent = strText  '= txtSMS.Text & sJobText
        sHttpTextMessage = URLEncode(sMessageSent)
        '==3043.0= strURL = txtGatewayURL.Text
        strURL = msGatewayURL

        '--  Build POST Content as Content-Type: "application/x-www-form-urlencoded"  --
        '-- smsGobal-
        '== https://www.smsglobal.com/http-api.php?action=sendsms&user=testuser&password=secret&&from=Test&to=61447100250&text=Hello%20world
        '=3311.728  ? DEPENDS on HOST-

        sQueryString = ""  '--used for smsBroadcast.
        sHttpText = ""  '-- POST text content.-
        If (LCase(strGatewayName) = "smsglobal") Then
            sQueryString = "?action=sendsms"
            sQueryString &= "&user=" & URLEncode(msSmsUsername)
            sQueryString &= "&password=" & URLEncode(msSmsPassword)
            sQueryString &= "&from=JobMatix"  '= & strGatewayName
            sQueryString &= "&to=" & strSmsDest
            sQueryString &= "&text=" & sHttpTextMessage
        ElseIf (LCase(strGatewayName) = "smsboss") Then '-boss -
            sHttpText = "username=" & URLEncode(msSmsUsername)
            sHttpText &= "&password=" & URLEncode(msSmsPassword)
            sHttpText &= "&from=JobMatix"  '= & strGatewayName
            sHttpText &= "&to=" & strSmsDest
            sHttpText &= "&message=" & sHttpTextMessage
        ElseIf (LCase(strGatewayName) = "smsbroadcast") Then ' broadcast-
            sQueryString = "?username=" & URLEncode(msSmsUsername)
            sQueryString &= "&password=" & URLEncode(msSmsPassword)
            sQueryString &= "&from=JobMatix"   '= & strGatewayName
            sQueryString &= "&to=" & strSmsDest
            sQueryString &= "&message=" & sHttpTextMessage
        ElseIf (LCase(strGatewayName) = "directsms") Then ' direct-
            sQueryString = "?type=1-way"
            sQueryString &= "&username=" & URLEncode(msSmsUsername)
            sQueryString &= "&password=" & URLEncode(msSmsPassword)
            sQueryString &= "&senderid=JobMatix"   '= & strGatewayName
            sQueryString &= "&to=" & strSmsDest
            sQueryString &= "&message=" & sHttpTextMessage
        End If

        '-- extract the URL using a helper function-
        DestUrl = ExtractUrl(strURL)
        If DestUrl.Host = vbNullString Then
            '=MsgBox("Invalid Host", MsgBoxStyle.Critical, "ERROR")
            strUserResultInfo = "Invalid Host in provider URL!"
            Exit Function
        End If
        '--Fix URL-- drop any supplied query.--
        DestUrl.Query = ""
        DestUrl.Scheme = "http"
        sHeaders = "Content-Type: application/x-www-form-urlencoded" & vbCrLf & "Host: " & DestUrl.Host & vbCrLf

        '--  set full URL, incl scheme..
        inet1.postURL = strURL & sQueryString
        inet1.ContentType = "application/x-www-form-urlencoded"

        '--open inet class-
        lngResult = inet1.inetOpen
        If (lngResult <> 0) Then
            '= MsgBox("Failed to open inet class.." & vbCrLf & _
            '=           "URL: " & strURL & vbCrLf & "Error=" & lngResult, MsgBoxStyle.Exclamation)
            strUserResultInfo = "Failed to open inet class.." & vbCrLf & _
                      "URL: " & strURL & vbCrLf & "Error=" & lngResult
            Exit Function
        End If  '-result-
        If (Not (labStatus Is Nothing)) Then labStatus.Text = "Sending to: " & strURL & ".." & vbCrLf
        '==3043.0= inet1.authenticateUserId = txtSmsUsername.Text '--just in case..
        '==3043.0= inet1.authenticatePassword = txtSmsPassword(0).Text
        inet1.authenticateUserId = msSmsUsername '--just in case..
        inet1.authenticatePassword = msSmsPassword

        strUserResultInfo = ""
        '--  do it..-
        lngResult = inet1.PostData(sHttpText, strResultText)
        If (lngResult < 0) Then
            '==3101.1029= catch any exceptions..-
            strPostException = inet1.LastExceptionText
            If (strPostException <> "") Then
                '== MsgBox(strPostException, MsgBoxStyle.Exclamation)
                strUserResultInfo &= "Post Exception was: " & strPostException & vbCrLf
            End If

            strResultStatus = VB.Left(inet1.queryStatusCode, 3)
            '== MsgBox("Failed to Send to inet..." & vbCrLf & "Error=" & lngResult & vbCrLf & _
            '==          "Response: " & strResultStatus & " = " & inet1.queryStatusText & vbCrLf & _
            '==                                "ResultText: " & strResultText, MsgBoxStyle.Exclamation)
            '== MsgBox("Request Failed- Request headers were: " & vbCrLf & _
            '==                       inet1.queryRequestHeaders, MsgBoxStyle.Exclamation)
            strUserResultInfo &= "Post Result was: " & vbCrLf & _
                             "Failed to Send to inet..." & vbCrLf & "Error=" & lngResult & vbCrLf & _
                            "Response: " & strResultStatus & " = " & inet1.queryStatusText & vbCrLf & _
                                           "ResultText: " & strResultText
            strUserResultInfo &= vbCrLf & "Request Failed- Request headers were: " & vbCrLf & _
                                 inet1.queryRequestHeaders
            If (Not (labStatus Is Nothing)) Then labStatus.Text = labStatus.Text & _
                               "Response: " & strResultStatus & " = " & inet1.queryStatusText & vbCrLf & _
                                                                             "ResultText: " & strResultText
            '== Call inet1.hostDisConnect()
            Call inet1.inetClose()
            '-failed-
            Exit Function
        Else
            '-- IF SENT OK..-
            sXmlStatus = ""
            sXmlErrorMsg = ""
            strResultStatus = VB.Left(inet1.queryStatusCode, 3)
            If strResultStatus = "200" Then '--ok-
                '-- got some valid answer.. Parse XML to get result..--
                If (Not (labStatus Is Nothing)) Then labStatus.Text = labStatus.Text & _
                                                            strResultStatus & " = " & inet1.queryStatusText & vbCrLf & _
                                                                   "ResultText: '" & strResultText & "'"
                '== MsgBox("DEBUG Info: " & vbCrLf & _
                '==                     "SMS gateway Result: 200 OK..   Request headers were: " & vbCrLf & _
                '==                                                                inet1.queryRequestHeaders)
                '== MsgBox("DEBUG Info: " & vbCrLf & "SMS gateway Result: 200 OK..    " & vbCrLf & _
                '==              "Received response: " & strResultStatus & " = " & inet1.queryStatusText & vbCrLf & _
                '==                      "ResultText: " & vbCrLf & strResultText & vbCrLf, MsgBoxStyle.Information)
                '=3311.728= 
                '-- CHECK Response According to Host-
                If (LCase(strGatewayName) = "smsboss") Then
                    '-- XML response.
                    '-- parse XML..-
                    iPos = InStr(LCase(strResultText), "<smsresponse")
                    If (iPos > 0) Then
                        iPos2 = InStr(iPos + 12, LCase(strResultText), "<status>")
                        If (iPos2 > 0) Then '--extract status..-
                            ix = InStr(iPos2 + 8, LCase(strResultText), "</status>")
                            If (ix > iPos2 + 8) Then sXmlStatus = Mid(strResultText, iPos2 + 8, ix - iPos2 - 8)
                        End If '--ipos2--
                        iPos2 = InStr(iPos + 12, LCase(strResultText), "<errormessage")
                        If (iPos2 > 0) Then '--extract status..-
                            ix = InStr(iPos2 + 14, LCase(strResultText), "</errormessage")
                            If (ix > iPos2 + 14) Then sXmlErrorMsg = Mid(strResultText, iPos2 + 14, ix - iPos2 - 14)
                        End If '--ipos2--
                    End If '--response..-
                    '== If gbDebug Then MsgBox("Result status from host is: '" & sXmlStatus & "'" & vbCrLf & _
                    '==          "Result ErrorMsg from host is: '" & sXmlErrorMsg & "'" & vbCrLf, MsgBoxStyle.Information)
                    If (InStr(LCase(sXmlStatus), "true") > 0) Then '--SUCCESS..-
                        '= MsgBox("SMS was sent OK.. " & vbCrLf, MsgBoxStyle.Information)
                        strUserResultInfo = "SMS was sent OK.. " & vbCrLf
                        gbSendSMS = True
                    Else  '- no true-
                        '= MsgBox("SMS Send failed.  Error msg was: " & vbCrLf & sXmlErrorMsg, MsgBoxStyle.Exclamation)
                        strUserResultInfo = "SMS Send failed.  Error msg was: " & vbCrLf & sXmlErrorMsg
                    End If '--xml status..-
                ElseIf (LCase(strGatewayName) = "smsbroadcast") Or (LCase(strGatewayName) = "smsglobal") Then
                    '-- parse smsbroadcast response..-
                    iPos = InStr(LCase(strResultText), "ok:")
                    If (iPos > 0) Then
                        strUserResultInfo = "SMS was sent OK.. " & vbCrLf & strResultText
                        gbSendSMS = True
                    Else
                        strUserResultInfo = "SMS Send failed.  Error msg was: " & vbCrLf & strResultText
                    End If '-ipos-
                ElseIf (LCase(strGatewayName) = "directsms") Then
                    '-- parse DIRECTsms response..-
                    iPos = InStr(LCase(strResultText), "id:")
                    If (iPos > 0) Then
                        strUserResultInfo = "SMS was sent OK.. " & vbCrLf & strResultText
                        gbSendSMS = True
                    Else
                        strUserResultInfo = "SMS Send failed.  Error msg was: " & vbCrLf & strResultText
                    End If '-ipos-
                Else
                    strUserResultInfo = "SMS Send failed.  UNKNOWN Host not supported."
                End If  '-gateway name-
            Else '--not 200..-
                '= MsgBox("SMS Send failed. " & vbCrLf & "Gateway Result Status was: " & vbCrLf & _
                '=                                                                strResultStatus, MsgBoxStyle.Exclamation)
                strUserResultInfo = "SMS Send failed. " & vbCrLf & "Gateway Result Status was: " & vbCrLf & strResultStatus
            End If
            '-- and Exit..--
            '= Call inet1.hostDisConnect()
            Call inet1.inetClose()
            inet1 = Nothing
        End If '-- sent..
    End Function '-send SMS-
    '= = = = = = = = = = = = ==

End Module '-modInetSubs..
'= = = = = = == = = = ==== 
