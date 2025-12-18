Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class frmSMSUpdate
	Inherits System.Windows.Forms.Form
	
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = =
	
	'--  Update standard SMS message and settings..-
	'--  Update standard SMS message and settings..-
	
    '-- grh- 13-Aug-2011== JobMatix Updates to Rev-2919 --

    '== grh= 03-Mar-2012- Build 3031.==
    '------         "clsStrDictionary" REPLACES "Scripting.Dictionary"  --

    '== grh= 13-Apr-2012- Build 3043.0.==
    '------   Separate texrbox for NEW SMS mesage..-
    '---      New fields to capture/commit  SMTP server/username/password for Email messages..
    '---   19-Apr-2012--  Fixes..  
    '==
    '==  30-Apr-2012-- 3047.1 =
    '--   Admin only for Gateway and SMTP settings..
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '==  grh. JobMatix 3.1 ---  19-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped ADODB and sqlClient)..
    '==
    '==   grh JobMatix33.. 25Feb2016=
    '==      3311.225=  Now Using  mSysInfo1 As clsSystemInfo
    '==             >> ON-SITE SMS reminders sent.
    '==
    '==  grh 3311.423-
    '==        >>- On-Site SMS Reminder Tinmes..
    '==              "OnSiteSmsWakeUpTime" and  "OnSiteSmsReminderMinsBefore"
    '==
    '==  -- 3311.730- 30July2016-
    '==         >>  Adding THREE new SMS Gateways.. "smsBroadcast", "smsGlobal", "directSMS"..
    '==            (New radio buttons for User to Choose gateway..  and
    '==                new systemInfo Key: 'smsGatewayHostName' -
    '==                        ("smsBoss", "smsBroadcast" or "smsGlobal" or "directSMS")
    '==
    '==   v3.4.3403.0531 -- 31may2017= x-
    '==         -- ADD to (frmSMSUpdate) panel for EXCHANGE-SERVER EWS mailbox and password...
    '==         --    (ALSO ON-SITE Jobs can post appointment to Exchange Calendar if defined.)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Private mbIsInitialising As Boolean = False
    Private mbActivated As Boolean = False

    Private mbFormLoading As Boolean = False

    Private mCnnJobs As OleDbConnection  '== ADODB.Connection

    '=3311.727 --
    Private msSmsGatewayOriginalHostName As String = ""
    Private msSmsGatewayHostName As String = ""

	Private msSmsGatewayAddress As String
	Private msSmsOriginalUsername As String
	Private msSmsOriginalPassword As String
	
    '==3311.225= Private mSdSystemInfo As clsStrDictionary    '== Scripting.Dictionary
    '==3311.225= Private mColSystemInfo As Collection
    '==3311.225= 
    Private mSysInfo1 As clsSystemInfo

	Private msStaffName As String
	'===Private msReasonText As String
	
	Private masSMSTexts() As String '--reference texts correspond to combo keys..--

	Private mbConnected As Boolean
	Private mbError As Boolean
	
    Private mbSmsOnly As Boolean
	'= = = = = = = = = = = = = = = = = =

    '--SAVE original SMTP Mail SystemInfo Settings..
    Private msSMTPHostName As String = ""
    Private mIntSMTPHostPort As Integer = 25    '--default..-

    Private msSMTPUsername As String = ""
    Private msSMTPPassword As String = ""

    Private mbSMSPasswordChanged As Boolean
    Private mbSMSDataChanged As Boolean = False
    Private mbSMTPDataChanged As Boolean = False
    Private mbExchangeDataChanged As Boolean = False   '-  EXCHANGE-SERVER EWS -

    '-  EXCHANGE-SERVER EWS mailbox and password...
    '-  EXCHANGE-SERVER EWS mailbox and password...
    Private msExchangeMailbox As String = ""
    Private msExchangePassword As String = ""


    '= = = = = = = = = = = =  = = = =
	
    WriteOnly Property connection() As OleDbConnection  '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--connection..--
	'= = = = = = = ===
	'-- results..--
	'-- results..--
	'= = = = = = = = = = = = = = =
	'-===FF->
	
	'-- conversions --
	'--  clean up sql string data ..--
	Private Function msFixSqlStr(ByRef sInstr As String) As String
		
		msFixSqlStr = Replace(sInstr, "'", "''")
		
	End Function '--fixSql-
	'= = = = = = = = = = = =
	'-===FF->
	
	'-- Refresh..--
	
	Private Function mbRefreshSMS() As Boolean
        '=Dim col1 As Collection
        Dim sKey1 As String
		Dim lngCount As Integer
		
		mbRefreshSMS = False
		
		'--load combo of SMS keys..-
		cboSmsKeys.Items.Clear()
		Erase masSMSTexts
		lngCount = 0
        Dim myKeys As ICollection = mSysInfo1.keys
        '== For Each col1 In mColSystemInfo
        '== If LCase(VB.Left(col1.Item("systemkey"), 8)) = "smstext_" Then '--standard message text..-
        '== cboSmsKeys.Items.Add(Mid(col1.Item("systemkey"), 9))
        '== ReDim Preserve masSMSTexts(lngCount)
        '== masSMSTexts(lngCount) = col1.Item("systemvalue")
        '== lngCount = lngCount + 1
        '== End If
        '== Next col1 '--col1..-
        For Each sKey1 In myKeys
            If LCase(VB.Left(sKey1, 8)) = "smstext_" Then '--standard message text..-
                cboSmsKeys.Items.Add(Mid(sKey1, 9))
                ReDim Preserve masSMSTexts(lngCount)
                masSMSTexts(lngCount) = mSysInfo1.item(sKey1)
                lngCount = lngCount + 1
            End If
        Next sKey1
        If lngCount > 0 Then
            cmdDelete.Enabled = True
            cmdUpdate.Enabled = True
        Else
            cmdDelete.Enabled = False
            cmdUpdate.Enabled = False
        End If
        mbRefreshSMS = True

    End Function '--refresh.-
	'= = = = = = =  =  = =
	'-===FF->
	
	'--  load --
	'--  load --
	
    Private Sub frmSMSUpdate_Load(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim s1, sItem As String

        mbFormLoading = True

        mSysInfo1 = New clsSystemInfo(mCnnJobs)

        cboSmsKeys.Items.Clear()

        txtSmsUsername.Enabled = False
        txtSmsPassword(0).Enabled = False '--lockout change event..-
        txtSmsPassword(1).Visible = False
        LabConfirm.Visible = False
        '===cboSmsKeys.Enabled = False
        '==cmdAdd.Enabled = False
        cmdFinish.Enabled = False
        mbConnected = False

        txtGatewayURL.Text = ""

        msSmsOriginalUsername = ""
        msSmsOriginalPassword = ""

        '==3311.727= Can choose Gateway Host-
        '==3311.727= Can choose Gateway Host-

        If mSysInfo1.exists("SmsGatewayHostName") AndAlso _
         (mSysInfo1.item("SmsGatewayHostName") <> "") Then
            '--was already defined-
            sItem = mSysInfo1.item("SmsGatewayHostName")
            msSmsGatewayOriginalHostName = sItem
            msSmsGatewayHostName = sItem
            Select Case LCase(sItem)
                Case "smsboss"
                    optSmsGatewayBoss.Checked = True
                    txtGatewayURL.Text = k_SMS_GATEWAY_URL_BOSS
                Case "smsbroadcast"
                    optSmsGatewayBroadcast.Checked = True
                    txtGatewayURL.Text = k_SMS_GATEWAY_URL_BROADCAST
                Case "smsglobal"
                    optSmsGatewayGlobal.Checked = True
                    txtGatewayURL.Text = k_SMS_GATEWAY_URL_GLOBAL
                Case "directsms"
                    optSmsGatewayDirect.Checked = True
                    txtGatewayURL.Text = k_SMS_GATEWAY_URL_DIRECTSMS
                Case Else
            End Select
        Else '--not defined yet..-
            '-- SET default gateway..--
            txtGatewayURL.Text = k_SMS_GATEWAY_URL_BOSS '--  "http://www.smsboss.com.au/api/sms.asmx/SendSMS"
            optSmsGatewayBoss.Checked = True
            msSmsGatewayHostName = "smsBoss"
        End If

        txtSmsUsername.Text = "" '--- "accounts@precisepcs.com"  '--PRECISE TEST.. Default..-
        txtSmsPassword(0).Text = "" '--- "sjb61sjb"
        txtSmsPassword(1).Text = "" '--- Confirm..-
        mbSMSPasswordChanged = False

        txtSMTPPassword2.Visible = False
        labSMTPConfirm.Visible = False

        txtExchangeMailbox.Text = ""
        txtExchangePassword1.Text = ""
        txtExchangePassword2.Text = ""

        txtExchangePassword2.Visible = False
        labExchangeConfirm.Visible = False

        mbSMTPDataChanged = False

        '==txtReason.Text = ""
        mbSmsOnly = True '-- False
        '===Call mbDisablePhoneNotify
        frameSMSGateway.Text = ""
        frameSMTPSettings.Text = ""

        labAdminOnly.Visible = False
        labAdminOnly2.Visible = False

        cmdClose.Text = "Exit"
        Call CenterForm(Me)

        '==3311.423-
        '--ON-SITE SMS reminders..
        btnOnSiteSave.Enabled = False

        '--load on-site combos-
        cboOnSiteWakeUp.Items.Clear()
        cboOnSiteWakeUp.Items.Add("0600")
        cboOnSiteWakeUp.Items.Add("0630")
        cboOnSiteWakeUp.Items.Add("0700")
        cboOnSiteWakeUp.Items.Add("0730")
        cboOnSiteWakeUp.Items.Add("0800")
        cboOnSiteWakeUp.Items.Add("0830")
        cboOnSiteWakeUp.Items.Add("0900")
        cboOnSiteWakeUp.Items.Add("0930")

        cboOnSiteMinsBefore.Items.Clear()
        cboOnSiteMinsBefore.Items.Add("030")
        cboOnSiteMinsBefore.Items.Add("060")
        cboOnSiteMinsBefore.Items.Add("090")
        cboOnSiteMinsBefore.Items.Add("120")

        '-- set up current values if any..-
        If mSysInfo1.exists("OnSiteSmsWakeUpTime") AndAlso _
                 (mSysInfo1.item("OnSiteSmsWakeUpTime") <> "") Then
            sItem = mSysInfo1.item("OnSiteSmsWakeUpTime")
            For Each s1 In cboOnSiteWakeUp.Items
                If s1 = sItem Then
                    cboOnSiteWakeUp.SelectedItem = s1
                    Exit For
                End If
            Next s1
        Else '- no def.
            cboOnSiteWakeUp.SelectedIndex = 0
        End If
        '-- and for Mins before-
        If mSysInfo1.exists("OnSiteSmsReminderMinsBefore") AndAlso _
                 (mSysInfo1.item("OnSiteSmsReminderMinsBefore") <> "") Then
            sItem = mSysInfo1.item("OnSiteSmsReminderMinsBefore")
            For Each s1 In cboOnSiteMinsBefore.Items
                If s1 = sItem Then
                    cboOnSiteMinsBefore.SelectedItem = s1
                    Exit For
                End If
            Next s1
        Else '- no def.
            cboOnSiteMinsBefore.SelectedIndex = 0
        End If

        btnOnSiteSave.Enabled = False
        grpBoxReminders.Text = ""

    End Sub '--load--
    '= = = = = = = = = = = = = = = = = = = = = =
	'-===FF->
	
	'-- Activate..-
	'-- Activate..-
	
    Private Sub frmSMSUpdate_Activated(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        '= Dim col1 As Collection
        Dim s1 As String

        If mbActivated Then Exit Sub
        mbActivated = True

        Me.Text = "JobMatix- Updating SMS settings- "
        Me.Text &= " V:" & CStr(My.Application.Info.Version.Major) & "." & _
                          My.Application.Info.Version.Minor & "." & _
                              My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "."
        '-- get systemInfo stuff..--
        '== If Not gbLoadsystemInfo(mCnnJobs, mColSystemInfo, mSdSystemInfo) Then
        '==  MsgBox("No System Info..", MsgBoxStyle.Exclamation)
        '==  Me.Hide()
        '== Exit Sub
        '== End If
        If Not mbRefreshSMS() Then
            MsgBox("No SMS info..", MsgBoxStyle.Exclamation)
            '==Me.Hide
            '==Exit Sub
        End If
        '== 3047.1--
        If Not gbIsSqlAdmin() Then
            frameSMSGateway.Enabled = False
            frameSMTPSettings.Enabled = False
            grpBoxExchange.Enabled = False
            labAdminOnly.Visible = True
            labAdminOnly2.Visible = True
        End If
        '--save settings.--
        For Each sKey As String In mSysInfo1.keys  '== col1 In mColSystemInfo
            If LCase(sKey) = "smscentralgatewayurl" Then
                '--save gateway..--
                s1 = Trim(mSysInfo1.item(sKey))
                If (s1 <> "") Then txtGatewayURL.Text = s1
            ElseIf (LCase(sKey) = "smscentralgatewayusername") Then
                txtSmsUsername.Text = Trim(mSysInfo1.item(sKey))
                msSmsOriginalUsername = txtSmsUsername.Text '--save..=
            ElseIf (LCase(sKey) = "smscentralgatewaypassword") Then
                txtSmsPassword(0).Text = Trim(mSysInfo1.item(sKey))
                msSmsOriginalPassword = txtSmsPassword(0).Text
            End If '--gateaway..-
        Next sKey '--col1-

        '--  load SMTP Mail SystemInfo Settings..
        '--  SMTPServer     --
        '--  SMTPUsername   --
        '--  SMTPPassword   --
        chkHostUsesSSL.Checked = False
        Dim svalue As String
        For Each sKey As String In mSysInfo1.keys  '== col1 In mColSystemInfo
            svalue = Trim(mSysInfo1.item(sKey))
            If LCase(sKey) = "smtphostname" Then '--server..-
                msSMTPHostName = svalue
                txtSMTPServer.Text = msSMTPHostName
            ElseIf LCase(sKey) = "smtphostport" Then '--port.-
                mIntSMTPHostPort = svalue
            ElseIf LCase(sKey) = "smtphostusesssl" Then '--server..-
                If (VB.Left(UCase(svalue), 1) = "Y") Then
                    chkHostUsesSSL.Checked = True
                End If
            ElseIf LCase(sKey) = "smtpusername" Then '--user..-
                msSMTPUsername = svalue
                txtSMTPUsername.Text = msSMTPUsername
            ElseIf LCase(sKey) = "smtppassword" Then '--password..-
                msSMTPPassword = svalue
                txtSMTPPassword1.Text = msSMTPPassword
            ElseIf LCase(sKey) = "exchange_mailbox_user" Then '--user..-
                '- EXCHANGE-
                msExchangeMailbox = svalue
                txtExchangeMailbox.Text = msExchangeMailbox
            ElseIf LCase(sKey) = "exchange_mailbox_password" Then '--user..-
                '- EXCHANGE-
                msExchangePassword = svalue
                txtExchangeMailbox.Text = msExchangePassword

            End If
        Next sKey '--col1..-

        txtSMTPHostPort.Text = CStr(mIntSMTPHostPort)
        TabControl1.SelectedIndex = 0   '--show SMS messages page..--

        txtSmsUsername.Enabled = True
        txtSmsPassword(0).Enabled = True

        If cboSmsKeys.Enabled Then
            cboSmsKeys.Focus()
        Else
            txtNewKey.Focus()
        End If 'cbo..-

        cmdFinish.Enabled = False '--was set on by change event.-
        mbSMSPasswordChanged = False '--   ditto..-
        mbSMSDataChanged = False
        mbSMTPDataChanged = False '--   ditto..-

        mbFormLoading = False

    End Sub '--activate--
	'= = = = = = = = == =
	'-===FF->
	
	
    Private Sub cboSmsKeys_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cboSmsKeys.Enter

        txtNewKey.Text = ""
        cmdAdd.Enabled = False
        '==cmdFinish.Enabled = True

    End Sub '--got focus.-
	'= = = = = = = = = = = =
	
    'UPGRADE_WARNING: Event cboSmsKeys.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboSmsKeys_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                    ByVal eventArgs As System.EventArgs) _
                                           Handles cboSmsKeys.SelectedIndexChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        With cboSmsKeys
            If .SelectedIndex >= 0 Then '--have selection..-
                txtSMS.Text = masSMSTexts(.SelectedIndex)
                txtSMS.Enabled = True

                '==ChkJobNo.Value = 0    '--unchecked..-
                cmdUpdate.Enabled = True
                cmdDelete.Enabled = True
                '==cmdFinish.Enabled = True
                '==cmdFinish.SetFocus
                '===ChkJobNo.Enabled = True
            End If

        End With
    End Sub '--selected.-
	'= = = = = = =  = = = = =
    '-===FF->

	'--  new sms msg key..-
	
    Private Sub txtNewKey_Enter(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtNewKey.Enter
        txtNewKey.SelectionStart = 0
        txtNewKey.SelectionLength = Len(txtNewKey.Text)

        txtSMS.Text = ""
        cmdFinish.Enabled = False
        cmdAdd.Enabled = True
        '==ChkJobNo.Enabled = False

    End Sub '--got focus.-
	'= = = = = = = = = = = =
	
    'UPGRADE_WARNING: Event txtNewKey.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtNewKey_TextChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles txtNewKey.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        '--fix key for double blanks..--
        While InStr(txtNewKey.Text, "  ") > 0
            txtNewKey.Text = Replace(txtNewKey.Text, "  ", " ")
        End While
        '--replace blanks with u/scores..--
        txtNewKey.Text = Replace(txtNewKey.Text, " ", "_")
        txtNewKey.SelectionStart = Len(txtNewKey.Text)

    End Sub '--change..-
	'= = = = = = = = = = = =
	'-===FF->
	
	'-- delete msg from sysinfo list..-
	
    Private Sub cmdDelete_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        Dim sKey As String
        Dim ix, index, L1 As Integer
        Dim sSql As String
        Dim sErrorMsg As String

        If (cboSmsKeys.SelectedIndex >= 0) Then
            sKey = "smsText_" & VB6.GetItemString(cboSmsKeys, cboSmsKeys.SelectedIndex)
            sSql = " DELETE FROM [SystemInfo] WHERE (SystemKey='" & sKey & "');  "
            If MsgBox("OK to delete the Sms Key: " & vbCrLf & sKey & vbCrLf & "From SystemInfo table?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question, "Deleting msg: " & sKey) = MsgBoxResult.Yes Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If Not gbExecuteCmd(mCnnJobs, sSql, L1, sErrorMsg) Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("Failed to Delete sms key.." & "SQL was:" & vbCrLf & sSql & vbCrLf & vbCrLf & sErrorMsg & vbCrLf, MsgBoxStyle.Critical)
                Else '--ok--
                    If (L1 > 0) Then
                        '==If gbDebug Then
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        MsgBox("SmS Key Deleted OK.. " & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & "(" & L1 & " rows affected.)", MsgBoxStyle.Information)
                    Else
                        MsgBox("No records were deleted ..", MsgBoxStyle.Exclamation)
                    End If
                End If '--execute..-
                '-- REFRESH systemInfo stuff..--
                '= If Not gbLoadsystemInfo(mCnnJobs, mColSystemInfo, mSdSystemInfo) Then
                '== MsgBox("No systemInfo found..", MsgBoxStyle.Exclamation)
                '= End If
                Call mbRefreshSMS()
                txtNewKey.Text = ""
                '==ChkJobNo.Enabled = True
            End If '--yes.-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If '--listindex..-
    End Sub '--delete..-
	'= = = = = = = = = =
	
	'-- Update msg..-
	
    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        Dim sKey As String

        If txtSMS.Text = "" Then
            MsgBox("No Text content has been entered for this message key..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '--ok ..  get key and update text to systeminfo..-
        If (cboSmsKeys.SelectedIndex >= 0) Then
            sKey = VB6.GetItemString(cboSmsKeys, cboSmsKeys.SelectedIndex)
            If mSysInfo1.UpdateSystemInfo(New Object() {"smsText_" & sKey, txtSMS.Text}) Then '--ok-
                cmdAdd.Enabled = False
                '==cmdFinish.Enabled = True
                '-- REFRESH systemInfo stuff..--
                '= If Not gbLoadsystemInfo(mCnnJobs, mColSystemInfo, mSdSystemInfo) Then
                '== MsgBox("No systemInfo found..", MsgBoxStyle.Exclamation)
                '== End If
                Call mbRefreshSMS()
                txtNewKey.Text = ""
                '==ChkJobNo.Enabled = True
            Else '--failed..-
                MsgBox("Couldn't update SystemInfo table..", MsgBoxStyle.Exclamation)
            End If '-update.-
        End If '--index=
    End Sub '--update..-
	'= = = = = = =  = = ==
	
	'--  A D D ---
	'--  A D D ---
	
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, _
                            ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        If txtNewSMS.Text = "" Then
            MsgBox("No Text content has been entered for this message key..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '--ok ..  add key and text to systeminfo..-
        If mSysInfo1.UpdateSystemInfo(New Object() {"smsText_" & txtNewKey.Text, txtNewSMS.Text}) Then '--ok-
            cmdAdd.Enabled = False
            '==cmdFinish.Enabled = True
            '-- REFRESH systemInfo stuff..--
            '= If Not gbLoadsystemInfo(mCnnJobs, mColSystemInfo, mSdSystemInfo) Then
            '= MsgBox("No systemInfo found..", MsgBoxStyle.Exclamation)
            '= End If
            Call mbRefreshSMS()
            txtNewKey.Text = ""
            '==ChkJobNo.Enabled = True
        Else '--failed..-
            MsgBox("Couldn't update SystemInfo table..", MsgBoxStyle.Exclamation)
        End If

    End Sub '-ok-
	'= = = = = = = = =
    '-===FF->

    '==3311.423-
    '--ON-SITE SMS reminders..


    Private Sub cboOnSiteWakeUp_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                       Handles cboOnSiteWakeUp.SelectedIndexChanged
        btnOnSiteSave.Enabled = True

    End Sub  '-cboOnSiteWakeUp-
    '= = = = = = = = = = = = = = = = 

    Private Sub cboOnSiteMinsBefore_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                      Handles cboOnSiteMinsBefore.SelectedIndexChanged
        btnOnSiteSave.Enabled = True

    End Sub  '--cboOnSiteMinsBefore-
    '= = = = = = = = = = = = = = = = =

    Private Sub btnOnSiteSave_Click(sender As Object, ev As EventArgs) Handles btnOnSiteSave.Click

        '-- save both times..-
        If Not mSysInfo1.UpdateSystemInfo(New Object() {"OnSiteSmsWakeUpTime", cboOnSiteWakeUp.SelectedItem}) Then '--ok-
            MsgBox("ERROR- Failed to update Wakeup time setting.")
        End If  '--update-

        If Not mSysInfo1.UpdateSystemInfo(New Object() {"OnSiteSmsReminderMinsBefore", cboOnSiteMinsBefore.SelectedItem}) Then '--ok-
            MsgBox("ERROR- Failed to update Wakeup time setting.")
        End If  '--update-
        btnOnSiteSave.Enabled = False

    End Sub '-btnOnSiteSave-
    '= = = = = = = = = = =  =
    '-===FF->

    '--  SMS Gateway credentials..--
    '--  SMS Gateway credentials..--

    '=3311.727= - optSmsGatewayBoss_CheckedChanged-

    Private Sub optSmsGatewayBoss_CheckedChanged(sender As Object, e As EventArgs) _
                                                  Handles optSmsGatewayBoss.CheckedChanged, _
                                                  optSmsGatewayBroadcast.CheckedChanged, _
                                                  optSmsGatewayGlobal.CheckedChanged, _
                                                  optSmsGatewayDirect.CheckedChanged
        '-- updating- msSmsGatewayHostName -
        If mbIsInitialising Or mbFormLoading Then Exit Sub

        If optSmsGatewayBoss.Checked Then
            msSmsGatewayHostName = "smsBoss"
            txtGatewayURL.Text = k_SMS_GATEWAY_URL_BOSS
        ElseIf optSmsGatewayBroadcast.Checked Then
            msSmsGatewayHostName = "smsBroadcast"
            txtGatewayURL.Text = k_SMS_GATEWAY_URL_BROADCAST
        ElseIf optSmsGatewayGlobal.Checked Then
            msSmsGatewayHostName = "smsGlobal"
            txtGatewayURL.Text = k_SMS_GATEWAY_URL_GLOBAL
        ElseIf optSmsGatewayDirect.Checked Then
            msSmsGatewayHostName = "directSMS"
            txtGatewayURL.Text = k_SMS_GATEWAY_URL_DIRECTSMS
        End If
        cmdClose.Text = "Cancel"
        mbSMSDataChanged = True
        cmdFinish.Enabled = True

    End Sub '--optSmsGatewayBoss_CheckedChanged-
    '= = = = = = = = = = = =  = = = = = = === =

	
    'UPGRADE_WARNING: Event txtSmsUsername.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtSmsUsername_TextChanged(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs)

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        '===MsgBox "Username changed..", vbExclamation
        cmdClose.Text = "Cancel"
        mbSMSDataChanged = True
        cmdFinish.Enabled = True

    End Sub '--user..-
	'= = = = = = = = =
	
    '-- SMS Password..--
	
    'UPGRADE_WARNING: Event txtSmsPassword.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtSmsPassword_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtSmsPassword.TextChanged
        Dim index As Short = txtSmsPassword.GetIndex(eventSender)

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbSMSDataChanged = True
        mbSMSPasswordChanged = True
        cmdClose.Text = "Cancel"
        If (index = 0) And txtSmsPassword(0).Enabled Then '--ist box..-
            txtSmsPassword(1).Visible = True
            LabConfirm.Visible = True
        ElseIf (index = 1) Then
            If (txtSmsPassword(0).Text = txtSmsPassword(1).Text) Then '--ok..-
                cmdFinish.Enabled = True
            Else
                cmdFinish.Enabled = False
            End If
        End If '--1st..-

    End Sub '--change..-
	'= = = = = = = = =
	'-===FF->

    '--  SMTP Mail Server stuff..--
    '--  SMTP Mail Server stuff..--
    '--  SMTP Mail Server stuff..--

    Private Sub txtSMTPServer_TextChanged(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles txtSMTPServer.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbSMTPDataChanged = True
        cmdFinish.Enabled = True
        cmdClose.Text = "Cancel"
    End Sub  '--txtSMTPServer_TextChanged-
    '= = = = = = = = = = = = = = = = = = =

    '--  Port-no..-

    Private Sub txtSMTPHostPort_TextChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles txtSMTPHostPort.TextChanged
        Dim s1 As String = Trim(txtSMTPHostPort.Text)

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        If (s1 <> "") Then
            If Not IsNumeric(s1) Then
                '== MsgBox("SMTP host port-no. must be numeric.", MsgBoxStyle.Exclamation)
            End If
        End If
        mbSMTPDataChanged = True
        cmdClose.Text = "Cancel"
    End Sub '--port-
    '= = = = = = = = = = 

    Private Sub txtSMTPHostPort_validating(ByVal sender As System.Object, _
                                               ByVal e As System.ComponentModel.CancelEventArgs) _
                                                    Handles txtSMTPHostPort.Validating
        Dim s1 As String = Trim(txtSMTPHostPort.Text)

        If (s1 <> "") Then
            If Not IsNumeric(s1) Then
                MsgBox("SMTP host port-no. must be numeric.", MsgBoxStyle.Exclamation)
                e.Cancel = True    '--keep focus..--
            Else
                cmdFinish.Enabled = True
            End If
        Else  '--no entry..-
            txtSMTPHostPort.Text = "25"  '--default..
            cmdFinish.Enabled = True
        End If
    End Sub  '--port validate-
    '= = = = = = = = = = = = = = 

    Private Sub chkHostUsesSSL_CheckedChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles chkHostUsesSSL.CheckedChanged

        mbSMTPDataChanged = True
        cmdClose.Text = "Cancel"
        cmdFinish.Enabled = True
    End Sub  '--chkHostUsesSSL-
    '= = = = = = = = = = = = = 
    '-===FF->

    Private Sub txtSMTPUsername_TextChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles txtSMTPUsername.TextChanged

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbSMTPDataChanged = True
        cmdClose.Text = "Cancel"
    End Sub  '--txtSMTPUsername_TextChanged-
    '= = = = = = = = = = == = = = = = = = =

    Private Sub txtSMTPPassword1_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtSMTPPassword1.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        txtSMTPPassword2.Visible = True
        labSMTPConfirm.Visible = True

        If (txtSMTPPassword1.Text = txtSMTPPassword2.Text) Then '--ok..-
            cmdFinish.Enabled = True
        Else
            cmdFinish.Enabled = False
        End If
        mbSMTPDataChanged = True
        cmdClose.Text = "Cancel"
    End Sub  '--txtSMTPPassword1_TextChanged--
    '= = = = = = = = = = = = = = = = = = = =

    Private Sub txtSMTPPassword2_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtSMTPPassword2.TextChanged

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        If (txtSMTPPassword1.Text = txtSMTPPassword2.Text) Then '--ok..-
            cmdFinish.Enabled = True
        Else
            cmdFinish.Enabled = False
        End If

        mbSMTPDataChanged = True
    End Sub  '--txtSMTPPassword1_TextChanged--
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- EXCHANGE Credentials..

    '-txtExchangeMailbox-

    Private Sub txtExchangeMailbox_TextChanged(sender As Object, e As EventArgs) _
                                                 Handles txtExchangeMailbox.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbExchangeDataChanged = True
        cmdClose.Text = "Cancel"

    End Sub '-txtExchangeMailbox-
    '= = = = = = == = = == ==== =

    '-txtExchangePassword1-

    Private Sub txtExchangePassword1_TextChanged(sender As Object, e As EventArgs) _
                                                    Handles txtExchangePassword1.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbExchangeDataChanged = True

        txtExchangePassword2.Visible = True
        labExchangeConfirm.Visible = True

        If (txtExchangePassword1.Text = txtExchangePassword2.Text) Then '--ok..-
            btnSaveExchange.Enabled = True
        Else
            btnSaveExchange.Enabled = False
        End If
        cmdClose.Text = "Cancel"

    End Sub  '-txtExchangePassword1-
    '= = = = = = = = = = = == = =  =

    '-txtExchangePassword2-

    Private Sub txtExchangePassword2_TextChanged(sender As Object, e As EventArgs) _
                                                         Handles txtExchangePassword2.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbExchangeDataChanged = True

        If (txtExchangePassword1.Text = txtExchangePassword2.Text) Then '--ok..-
            btnSaveExchange.Enabled = True
        Else
            btnSaveExchange.Enabled = False
        End If
        cmdClose.Text = "Cancel"

    End Sub  '-txtExchangePassword2-
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- F i n i s h --
    '-- F i n i s h --

    Private Sub cmdFinish_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) _
                                             Handles cmdFinish.Click
        Dim sSSL As String
        If mbSMSPasswordChanged Then
            If (Trim(txtSmsPassword(0).Text) <> Trim(txtSmsPassword(1).Text)) Then
                MsgBox("SMS Gateway Password and confirmation fields are not identical..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        If Trim(msSMTPPassword) <> Trim(txtSMTPPassword1.Text) Then  '--has changed..--
            If (Trim(txtSMTPPassword1.Text) <> Trim(txtSMTPPassword2.Text)) Then
                MsgBox(" SMTP (Mail) Password and confirmation fields are not identical..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        '=3311-727= -- update SMS gateway if changed..--
        Dim sUpdateMsg As String = ""
        '--   SMS- ASK first..--
        If (LCase(msSmsGatewayOriginalHostName) <> Trim(LCase(msSmsGatewayHostName)))  Then '--changed..-
            If MsgBox("The SMS Gateway HOST has been changed.." & vbCrLf & _
                   "Do you want to update the JobMatix database " & vbCrLf & _
                        "  to save this new value ?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If mSysInfo1.UpdateSystemInfo(New Object() {"SmsGatewayHostName", Trim(msSmsGatewayHostName)}) Then '--ok-
                    MsgBox("Gateway HOST name was updated ok..", MsgBoxStyle.Information)
                End If '--update-
            Else  '--no-
                '== Exit Sub
            End If '--yes.
        End If '--changed..-

        '-- update SMS Credentials if changed..--
        '--   SMS- ASK first..--
        If (LCase(msSmsOriginalUsername) <> Trim(LCase(txtSmsUsername.Text))) Or _
                                                        (msSmsOriginalPassword <> Trim(txtSmsPassword(0).Text)) Then '--changed..-
            '==TabControl1.SelectedIndex = 1  '--SMS--
            If MsgBox("The SMS Gateway credentials have been changed.." & vbCrLf & _
                   "Do you want to update the JobMatix database " & vbCrLf & _
                        "  to save these new values?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If mSysInfo1.UpdateSystemInfo(New Object() {"smsCentralGatewayUsername", Trim(txtSmsUsername.Text), _
                                                         "smsCentralGatewayPassword", Trim(txtSmsPassword(0).Text)}) Then '--ok-
                    MsgBox("Credentials were updated ok..", MsgBoxStyle.Information)
                End If '--update-
            Else  '--no-
                '== Exit Sub
            End If '--yes.
        End If '--changed..-

        '--  Update SMTP stuff if needed..-
        If mbSMTPDataChanged Then
            '== TabControl1.SelectedIndex = 2  '--SMTP--
            sSSL = "N"
            If chkHostUsesSSL.Checked Then sSSL = "Y"
            If MsgBox("The SMTP (Mail) Server credentials have been changed.." & vbCrLf & _
                   "Do you want to update the JobMatix database " & vbCrLf & _
                        "  to save these new values?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If mSysInfo1.UpdateSystemInfo(New Object() _
                              {"smtpHostName", Trim(txtSMTPServer.Text), _
                              "smtpHostPort", Trim(txtSMTPHostPort.Text), _
                              "smtpHostUsesSSL", sSSL, _
                              "smtpUsername", Trim(txtSMTPUsername.Text), _
                             "smtpPassword", Trim(txtSMTPPassword1.Text)}) Then '--ok-
                    MsgBox("SMTP Credentials were updated ok..", MsgBoxStyle.Information)
                End If '--update-
            Else  '--no-
                '== Exit Sub
            End If '--yes.
        End If '--changed..-
        Me.Hide()
    End Sub '--finish..-
    '= = = = = = = = = = = =
    '-===FF->

    '--E x c h a n g e   F i n i s h --
    '--E x c h a n g e   F i n i s h --
    '--E x c h a n g e   F i n i s h --

    '-btnSaveExchange- 
    Private Sub btnSaveExchange_Click(sender As Object, e As EventArgs) Handles btnSaveExchange.Click

        '--  Update SMTP stuff if needed..-
        If mbExchangeDataChanged Then
            If MsgBox("The Exchange (MailBox) credentials have been changed.." & vbCrLf & _
                   "Do you want to update the JobMatix database " & vbCrLf & _
                        "  to save these new values?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If mSysInfo1.UpdateSystemInfo(New Object() _
                              {"exchange_mailbox_user", Trim(txtExchangeMailbox.Text), _
                             "exchange_mailbox_password", Trim(txtExchangePassword1.Text)}) Then '--ok-
                    MsgBox("EXCHANGE Credentials were updated ok..", MsgBoxStyle.Information)
                End If '--update-
            Else  '--no-
                '== Exit Sub
            End If '--yes.
        End If '--changed..-
        Me.Hide()

    End Sub  '-btnSaveExchange-
    '= = = = == =  = == == = ==== 

    '-- Cancel..-

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdClose.Click

        If mbSMTPDataChanged Or mbSMSDataChanged Or mbExchangeDataChanged Then

            If MsgBox("Discard changes?", _
                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Me.Hide()
            End If
        Else
            Me.Hide()
        End If
    End Sub
    '= = = = = = = = = =


    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmSMSUpdate_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                 Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim sMsg As String

        sMsg = "Discard changes ?"
        'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                             System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                   System.Windows.Forms.CloseReason.FormOwnerClosing  '==, vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                If mbSMTPDataChanged Or mbSMSDataChanged Then
                    '-- confirm if job is to be abandoned..--
                    If MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        intCancel = 0 '--let it go--
                    Else
                        intCancel = 1 '--cant close yet--'--was mistake..  keep going..
                    End If '--yes.--
                Else '--no changes..-
                    intCancel = 0 '--let it go--
                End If
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--unload--
    '= = = = = = = = = = = =
    '= = = = = = = =  = = =
    '=== end form..==

 
End Class