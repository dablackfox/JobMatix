Option Strict Off
Option Explicit On
Imports System
Imports VB = Microsoft.VisualBasic
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.Data.OleDb

Public Class clsStaffReminders

    '-- Class to run BG ON-SITE reminders SMS'..
    '==   -- WAS embedded in frmJobMatix33..

    '- B a c k g r o u n d  W o r k e r  Threads --

    '-- Staff reminders (eg ON-SITE Jobs)..-
    '--  Checks ALL jobs for ALL techs..
    '--  Could be restricted to server only..

    '--  This SMS support function runs in BG,
    '--    called from the BG SMS Reminder Worker Thread..

    '== grh 3311.508-  08May2016.-
    '-- uses BackgroundWorkerStaffReminders

    '- NB- Must make new SQL Connection each time it polls Database.
    '--  Else it's Transaction conficts with Main Thread..
    '==
    '==  -- 3311.731- 31July2016-
    '==      >>  Adding THREE new SMS Gateways.. "smsBroadcast" and "smsGlobal", "directSMS"..
    '==         Now has new systemInfo Key: 'smsGatewayHostName' ("smsBoss", "smsBroadcast" or "smsGlobal")
    '==      >>  Can't use retailHost1 here for staff, as has wrong sql connection for POS. staff.
    '==             So we pass in a list (collection) of staff records.
    '==
    '==
    '==  -- 3327.0119- 19-Jan-2017-
    '==         >>-- Fixes SQL ERROR in SMS update (INSERT not escaping apostrophes.)..-- 
    '==
    '==   v3.4.3403.0629 -- 29Jun2017= - FIX UP for release.
    '==         -- ADD Customer CustomerAddress/Phone to ON-SITE SMS..
    '==
    '==  NEW BUILD- 4219 VERSION
    '==    Updated- 4219.1122 22-Nov-2019= 
    '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
    '==
    '==
    '='= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = =


    Private Const k_OTHER_TYPE_SMS_SENT = "ONSITE_SMS_SENT"
    Private Const k_MAX_SMS_SIZE As Short = 140

    Private WithEvents BackgroundWorkerStaffReminders As System.ComponentModel.BackgroundWorker
    Private msComputerName As String '--local machine--

    '== Private mCnnSql As OleDbConnection  '-main thread connection 
    Private mOleDbCnn As OleDbConnection  '--extra cnn we do HERE for Transaction..
    Private msSqlDbName As String

    Private msSqlConnection As String = ""
    Private mbSqlServerConnectOK As Boolean = False

    '-- Multi-Retail-Host--
    '=4219.1122= Private mRetailHost1 As _clsRetailHost
    Private mRetailHost1 As JMxRetailHost._clsRetailHost  '= NOT REALLY NEEDED ?=

    Private mSysInfo1 As clsSystemInfo

    Private msSmsGatewayHostName As String = ""  '==3311.730=
    Private msSmsUsername, msSmsPassword As String
    Private ms_GOODS_ONSITE_JOB As String = ""  '-- "ON-SITE JOB;" -

    Private msReminderStatus As String = ""
    Private mLabReminderStatus As Label

    Private mIntMainSleepTime As Integer = 300   '-seconds-

    Private mColFullStaffList As Collection   '=3311.731= --for Reminder SMS..
    '--  updated from MainTask via the StaffMobiles propeerty..
    '==
    '= = = = = = == = = = = = = = = = = = = = = = =


    Public Sub New(ByVal strSqlConnection As String, _
                   ByVal sqlDbName As String, _
                    ByRef colFullStaffList As Collection, _
                     ByVal strOnsiteJobmarker As String, _
                      ByRef labReminderStatus As Label)
        MyBase.New()
        '= Class_Initialize_Renamed()

        msSqlConnection = strSqlConnection
        msSqlDbName = sqlDbName

        '=3311.731-= mRetailHost1 = retailHost1
        mColFullStaffList = colFullStaffList
        ms_GOODS_ONSITE_JOB = strOnsiteJobmarker
        mLabReminderStatus = labReminderStatus

        msComputerName = My.Computer.Name

        '-- Ready to go.
        BackgroundWorkerStaffReminders = New System.ComponentModel.BackgroundWorker
        '-- start BG thread..
        Me.BackgroundWorkerStaffReminders.RunWorkerAsync(mColFullStaffList)
        '-- DoWork will finiish init..

    End Sub '--New..--
    '= = = = = = = = = = = = = = = = = = = =

    Public WriteOnly Property colstaffmobiles As Collection
        Set(value As Collection)
            '=3311.731-= mRetailHost1 = retailHost1
            mColFullStaffList = value
        End Set
    End Property  '-staff mobiles-
    '= = = = = = = =  == = = = = =
    '-===FF->  
    '-- Updating Status om main form..

    Private Sub mUpdateReminderStatus(Optional ByVal sStatus As String = "", _
                                      Optional ByVal bStatusOK As Boolean = True)

        If mLabReminderStatus.InvokeRequired Then
            mLabReminderStatus.Invoke(New MethodInvoker(AddressOf mUpdateReminderStatus))
        Else  '-ok-
            If (sStatus <> "") Then
                mLabReminderStatus.Text = sStatus
            Else
                mLabReminderStatus.Text = msReminderStatus
            End If
            If Not bStatusOK Then
                mLabReminderStatus.ForeColor = Color.Red
            Else '-normal-
                mLabReminderStatus.ForeColor = Color.DarkGoldenrod
            End If
            DoEvents()
        End If  '-required-
        DoEvents()
    End Sub '-- update-
    '= = = = = = = = = = = = = = = = = = = =

    '-- Sql connect--

    Private Function mbSqlConnect() As Boolean

        Dim sTimeNow As String = Format(Now, "HH:mm:ss")
        mbSqlConnect = False
        If Not gbConnectSql(mOleDbCnn, msSqlConnection) Then
            msReminderStatus = sTimeNow & "ERROR. Failed SqlConnect."
            Call mUpdateReminderStatus()
            Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- ERROR. Failed SqlConnect.." & vbCrLf & _
                                                                    gsGetLastSqlErrorMessage() & vbCrLf)
        Else '-ok-
            Try
                Call mOleDbCnn.ChangeDatabase(msSqlDbName)
                mbSqlConnect = True
                msReminderStatus = sTimeNow & "- SQL Jobs DB Connected."
                Call mUpdateReminderStatus()
            Catch ex As Exception
                Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- ERROR. Failed ChangeDatabase.." & vbCrLf & _
                                                                                                  ex.Message & vbCrLf)
                Call mUpdateReminderStatus("FAILED Change DB.")
            End Try
        End If
    End Function  '--connect-
    '= = = = = = = = = = = === 
    '-===FF->  

    '- Send SMS and log event..

    Private Function mbSendSmsAndLog(ByVal intJob_id As Integer, _
                                      ByVal sNomTech As String, _
                                      ByVal sSmsType As String, _
                                      ByVal sMobile As String, _
                                      ByVal sMsgtext As String, _
                                      ByRef oleDbTransaction1 As OleDbTransaction) As Boolean

        Dim intAffected As Integer
        Dim sSql, s1, sErrorMsg As String

        mbSendSmsAndLog = False
        '-- Send the SMS..
        '-- SEND..--
        '== 3311.404==  Using InetSubs..
        Dim sResultInfo As String = ""
        '==3311.730=  variable Host gateway name.
        If Not gbSendSMS(msSmsGatewayHostName, msSmsUsername, msSmsPassword, sMsgtext, sMobile, sResultInfo) Then
            oleDbTransaction1.Rollback()
            Call gbLogMsg(gsRuntimeLogPath, _
                            "ERROR- Failed to send SMS to- " & sNomTech & vbCrLf & _
                              " via- " & msSmsGatewayHostName & vbCrLf & sResultInfo)
            '== MsgBox("ERROR- Failed to send SMS-" & vbCrLf & sResultInfo, MsgBoxStyle.Exclamation)
            mIntMainSleepTime = 1800  '-seconds= Long sleep now for 1/2 hour.
            Exit Function
        Else '-ok
            Call gbLogMsg(gsRuntimeLogPath, "Text Sent ok.." & vbCrLf & sResultInfo)
        End If  '- gbSend-
        mIntMainSleepTime = 300  '-seconds= OK..  normal sleeping interval.-

        '--IF SMS FAILS.  Log the ERROR and rollback-
        '--  oleDbTransaction1.Rollback()

        '-- sms done..
        '-- INSERT JobOther record..
        sSql = "INSERT INTO dbo.JobOtherDetails "
        sSql &= " (JobOther_JobId, JobOtherType, JobOtherStaffname, JobOtherBarcode, "
        sSql &= "   JobOtherTextData1, JobOtherTextData2) "
        sSql &= " VALUES (" & CStr(intJob_id) & ", '" & k_OTHER_TYPE_SMS_SENT & "'"
        sSql &= ", '" & gsFixSqlStr(sNomTech) & "'"
        sSql &= ", '" & gsFixSqlStr(msComputerName) & "'"
        sSql &= ", '" & gsFixSqlStr(sSmsType) & "'"
        sSql &= ", 'To: " & sMobile & "-  " & gsFixSqlStr(sMsgtext) & "'"
        sSql &= " );"
        If gbExecuteSql(mOleDbCnn, sSql, True, oleDbTransaction1, intAffected, sErrorMsg) Then
            '-- ok.. COMMIT Transaction..
            oleDbTransaction1.Commit()
            Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Staff WAKEUP SMS logged.." & vbCrLf)
            '== Thread.Sleep(3000)
            mbSendSmsAndLog = True
        Else  '-failed-
            mIntMainSleepTime = 3600  '-seconds= Long sleep now for 1 hour.
            Call gbLogMsg(gsRuntimeLogPath, "ON-SITE SMS Sql INSERT failed.." & vbCrLf & _
                             sErrorMsg)
            '- was rolled back by Exec..
        End If   '- exec.

    End Function  '-mbSendSmsAndLog-
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->  

    '-- SMS Reminder Worker Thread-
    '-    Hang around all day reminding staff of stuff. ..--
    '--  Started from Form Activated event routine..
    '-   This event handler is where the actual BG work is done.
    '-- ev ARGUMENT is collection of staff mobiles-

    Private Sub BackgroundWorkerStaffReminders_DoWork(sender As Object, ev As DoWorkEventArgs) _
                                                       Handles BackgroundWorkerStaffReminders.DoWork
        Dim sSql, s1, sHH, sMM As String
        Dim datatable1 As DataTable
        '-- wakeup Time..
        Dim dtWakeUpTime As DateTime = DateAdd(DateInterval.Hour, 7, Today)  '-- default 0700 hours.
        Dim intReminderMins As Integer = 60  '-- mins before job is due.. Defaults 60 mins.. 
        Dim sTimeNow, sValue, sTimePromised As String
        Dim intWakeUpCount, intReminderCount As Integer

        '--  THIS PROBABLY won't be updated--
        '--  User may have to exit and restart JonMatix.--
        Dim bEnableSMSReminders As Boolean = False

        Dim colFullStaffList As Collection = DirectCast(ev.Argument, Collection)  '=3311.731= --for Reminder SMS..


        '-- Startup stuff first--
        Call gbLogMsg(gsRuntimeLogPath, "ON-SITE- Staff Reminder Thread started.." & vbCrLf)
        Thread.Sleep(30000)  '--30 secs-

        '-- make initial connection- for sysInfo
        '-- get sysInfo..
        mbSqlServerConnectOK = False
        '=mbSqlServerConnectOK = gbConnectSql(mOleDbCnn, msSqlConnection)
        If Not mbSqlConnect() Then
            Exit Sub  '--end it-
        Else
            mbSqlServerConnectOK = True
        End If

        mSysInfo1 = New clsSystemInfo(mOleDbCnn)

        '-- get sysInfo parameters. (User Biz Must define WakeUp Time)-
        If mSysInfo1.exists("OnSiteSmsWakeUpTime") Then
            s1 = Replace(mSysInfo1.item("OnSiteSmsWakeUpTime"), ":", "")  '-strip colon if any.
            If (Len(s1) = 4) AndAlso IsNumeric(s1) AndAlso _
                            (CInt(s1) <= 1200) AndAlso (CInt(Mid(s1, 3)) <= 59) Then
                '--make wakeup time today..
                dtWakeUpTime = DateAdd(DateInterval.Minute, _
                                       (CInt(VB.Left(s1, 2)) * 60) + CInt(Mid(s1, 3)), Today)
            End If
        End If '-exists-
        If mSysInfo1.exists("OnSiteSmsReminderMinsBefore") Then
            s1 = mSysInfo1.item("OnSiteSmsReminderMinsBefore")
            If IsNumeric(s1) AndAlso (CInt(s1) <= 480) Then  '-less than 8 hours-
                intReminderMins = CInt(s1)
            End If
        End If  '--mins-
        '--save SMS settings.--
        '==3311.728= Must find chosen Gateway Host-
        msReminderStatus = sTimeNow & " Getting SMS Gateway.."
        Call mUpdateReminderStatus()
        Thread.Sleep(3000)  '--3 secs.

        If mSysInfo1.exists("SmsGatewayHostName") AndAlso _
         (mSysInfo1.item("SmsGatewayHostName") <> "") Then
            msSmsGatewayHostName = mSysInfo1.item("SmsGatewayHostName")
        Else   '--selection not defined yet.. must use ORIGINAL-
            msSmsGatewayHostName = "smsBoss"
        End If
        msReminderStatus = sTimeNow & " SMS Gateway is: " & msSmsGatewayHostName
        Call mUpdateReminderStatus()
        '-- log the startup and parms..
        Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- BG task starting.." & vbCrLf & _
                                      "Using WakeUp time as: " & Format(dtWakeUpTime, "dd-MMM-yyyy hh:mm tt ") & _
                                          vbCrLf & "Using ReminderBefore Mins as : " & intReminderMins & vbCrLf & _
                                        " SMS Gateway is: " & msSmsGatewayHostName)
        Thread.Sleep(3000)  '--3 secs.

        '-- credentials-
        For Each sKey1 As String In mSysInfo1.keys  '= col1 In mColSystemInfo
            sValue = Trim(mSysInfo1.item(sKey1))
            If (LCase(sKey1) = "smscentralgatewayusername") Then
                msSmsUsername = sValue '= Trim(col1.Item("systemvalue"))
            ElseIf (LCase(sKey1) = "smscentralgatewaypassword") Then
                msSmsPassword = sValue  '= Trim(col1.Item("systemvalue"))
            End If '--gateway..-
        Next sKey1  '= col1 '--col1-

        If mSysInfo1.exists("EnableOnSiteSmsReminders") AndAlso _
             UCase(mSysInfo1.item("EnableOnSiteSmsReminders")) = "Y" Then
            bEnableSMSReminders = True
            msReminderStatus = sTimeNow & " SMS Reminders Enabled.."
            Call mUpdateReminderStatus()
        Else
            bEnableSMSReminders = False
            msReminderStatus = sTimeNow & " SMS Reminders Disabled.."
            Call mUpdateReminderStatus()
        End If '-exists-

        '-- close sql connection.  re-open when needed..
        mOleDbCnn.Close()

        Dim oleDbTransaction1 As OleDbTransaction
        Dim intJob_id, intAffected, intPos As Integer
        Dim sNomTech, sMobile, sMsgtext, sErrorMsg, sCustomer As String
        Dim sCustomerAddress, sCustomerPhone As String
        Dim datePromised As DateTime
        Dim colStaffInfo As Collection
        Dim dtSmsSent As DataTable
        Dim bSmsOk As Boolean = True

        mIntMainSleepTime = 300  '-seconds=
        '-- Main loop- all day..
        While True
            '-- sleep until wakeup time..
            sTimeNow = Format(Now, "HH:mm:ss")
            If Not bEnableSMSReminders Then  '=3311.409= -not enabled- 
                '--   MAY NOT CHANGE without a restart !!
                Thread.Sleep(300000)  '--5 mins.
            ElseIf (Now < dtWakeUpTime) Then
                msReminderStatus = sTimeNow & " Still asleep."
                Call mUpdateReminderStatus()
                Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- Sleeping until WakeUp time.." & vbCrLf)
                Thread.Sleep(300000)  '--5 mins.
            Else  '--ok, we can start work..
                msReminderStatus = sTimeNow & " Thread Started.."
                Call mUpdateReminderStatus()
                '== Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- Checking Jobs.." & vbCrLf)
                '-- get a list of ALL (still QUEUED) ONSITE Jobs Promised Today..
                '-- FOR each Job-
                '--  BEGIN transaction.
                '---   Read JobOtherDetails (Type="ONSITE-SMS-SENT") for THIS Job WITH TABLOCK
                '--           Where dateCreated=Today
                '--    IF SMS needed then SEND SMS..
                '--        and INSERT record (Type="ONSITE-SMS-SENT") for THIS Job..
                '--        COMMIT TRANSaction.
                '--    else rollback..
                '-- NEXT JOB.

                sSql = " SELECT job_id, CustomerName, DateCreated, DatePromised, DateUpdated, NominatedTech, JobStatus, Priority, "
                sSql &= "  GoodsOther, CustomerPhone, CustomerMobile, CustomerCompany "
                sSql &= "  FROM dbo.Jobs "
                sSql &= " WHERE (LEFT(JobStatus,2)<='10') " '-not started.-
                sSql &= "    AND (UPPER(GoodsInCare) = '" & ms_GOODS_ONSITE_JOB & "') "
                sSql &= "    AND (DATEDIFF(day, getdate(), DatePromised)=0) "  '-today-
                If Not mbSqlConnect() Then
                    '- no connection
                    Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- No Sql Connection.." & vbCrLf)
                    Thread.Sleep(600000)  '--10 mins.
                ElseIf gbGetDataTable(mOleDbCnn, datatable1, sSql) Then
                    If Not (datatable1 Is Nothing) Then
                        '=Call gbLogMsg(gsRuntimeLogPath, _
                        '=             "ON-SITE Reminders- We found " & datatable1.Rows.Count & "  Onsite Jobs for today..")
                        If (datatable1.Rows.Count > 0) Then
                            For Each dtRow1 As DataRow In datatable1.Rows  '--Each Job..
                                intJob_id = dtRow1.Item("job_id")
                                sNomTech = dtRow1.Item("NominatedTech")
                                datePromised = dtRow1.Item("DatePromised")
                                sTimePromised = Format(datePromised, "hh:mm tt.")
                                sCustomer = Replace(Trim(dtRow1.Item("CustomerCompany")), "--", "")
                                If (sCustomer <> "") Then
                                    sCustomer &= ": "
                                End If
                                sCustomer &= dtRow1.Item("CustomerName")
                                '=3403- Get Customer ON-SITE Address AND PHONE---
                                s1 = dtRow1.Item("GoodsOther")
                                sCustomerAddress = "Unknown Address. "
                                intPos = InStr(UCase(s1), "ON-SITE ADDRESS:")
                                If (intPos > 0) Then
                                    sCustomerAddress = Replace(Trim(Mid(s1, intPos + 16)), vbCrLf, " ")
                                End If
                                sCustomerPhone = Trim(dtRow1.Item("CustomerPhone"))
                                s1 = Trim(dtRow1.Item("CustomerMobile"))
                                If (s1 <> "") Then '-have mobile-
                                    If (sCustomerPhone <> "") Then
                                        sCustomerPhone &= "/"
                                    End If
                                    sCustomerPhone &= s1
                                End If
                                '-- GET Tech Mobile for SMS if needed..
                                sMobile = ""
                                '-- Only have Tech Docket_name to look up.. 
                                msReminderStatus = sTimeNow & " Getting Staff details for: " & sNomTech
                                Call mUpdateReminderStatus()
                                If (Not (colFullStaffList Is Nothing)) Then
                                    '- mRetailHost1.staffGetStaffRecordEx("", -1, sNomTech, colStaffInfo) Then
                                    '3311.731= -search full staff list for nomTech docket_name-
                                    For Each colStaffInfo In colFullStaffList
                                        If LCase(colStaffInfo.Item("docket_name")("value")) = LCase(sNomTech) Then
                                            sMobile = Replace(colStaffInfo.Item("mobile")("value"), " ", "")
                                            Exit For
                                        End If
                                    Next colStaffInfo
                                    '-- Send WakeUp SMS if not yet sent,
                                    '--    ELSE send Reminder-before if not yet sent..
                                    '== sMobile = Replace(colStaffInfo.Item("mobile")("value"), " ", "")
                                    '-- check valid mobile-
                                    If (sMobile <> "") AndAlso IsNumeric(sMobile) Then '-ok-
                                        '-- LOCK the table while we're looking at it..
                                        oleDbTransaction1 = mOleDbCnn.BeginTransaction
                                        '- get last SMS sent for this Job today, if any..- 
                                        sSql = "SELECT top 1 * FROM dbo.JobOtherDetails WITH (TABLOCKX) "
                                        sSql &= " WHERE (JobOtherType='" & k_OTHER_TYPE_SMS_SENT & "') "  '-SMS type of Other-
                                        sSql &= "  AND (JobOther_JobId=" & CStr(intJob_id) & ") "  '-writtentoday-
                                        sSql &= "  AND (DATEDIFF(day,CURRENT_TIMESTAMP, JobOtherDateCreated)=0) "  '-writtentoday-
                                        sSql &= " ORDER BY JobOtherDateCreated DESC; "
                                        If gbGetDataTableEx(mOleDbCnn, dtSmsSent, sSql, oleDbTransaction1) AndAlso _
                                                           (Not (dtSmsSent Is Nothing)) Then
                                            '-- datatable returned-
                                            If (dtSmsSent.Rows.Count > 0) Then
                                                '-- get last sms sent for this job..
                                                Dim dtRowSms1 As DataRow = dtSmsSent.Rows(0)
                                                s1 = LCase(dtRowSms1.Item("JobOtherTextData1"))  '-- get sms type-
                                                If (s1 = "wakeup") Then
                                                    '-- Last SMS was WakeUp..
                                                    '-- send reminder if time has come..
                                                    If (DateDiff(DateInterval.Minute, Now, datePromised) < intReminderMins) Then
                                                        '-- SEND SMS, INSERT JobOther record..
                                                        '--  and COMMIT the Transaction to release the Table..
                                                        sMsgtext = "For " & sNomTech & _
                                                              "- ON-SITE Job " & intJob_id & ". at " & sTimePromised & _
                                                                   "; " & sCustomer & "; Ph: " & sCustomerPhone & _
                                                                     "; At: " & sCustomerAddress & ".."
                                                        '-k_MAX_SMS_SIZE -
                                                        sMsgtext = VB.Left(sMsgtext, k_MAX_SMS_SIZE)
                                                        Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- Sending msg: " & vbCrLf & _
                                                                      "To:" & sMobile & "- " & sMsgtext & ".")
                                                        bSmsOk = mbSendSmsAndLog(intJob_id, sNomTech, _
                                                                               "REMINDER", sMobile, sMsgtext, oleDbTransaction1)
                                                        If bSmsOk Then
                                                            '= intSentCount += 1
                                                        End If
                                                    Else
                                                        '-- nothing to do- Release Table.
                                                        oleDbTransaction1.Rollback()
                                                    End If  '--datediff.
                                                Else '--last SMS was reminder-
                                                    '-- nothing to do- Release Table.
                                                    oleDbTransaction1.Rollback()
                                                End If  '-wakeup/reminder-
                                            Else '-Empty- no sms sent so far today..
                                                '-- SEND WAKEUP SMS and Log it..--
                                                'sMsgtext = "Hi " & sNomTech & _
                                                '            ", You have Onsite Job today at " & sTimePromised & _
                                                '                        ". (No." & intJob_id & ": " & sCustomer & _
                                                '                             "). Pls check the JobMatix On-Site Jobs Panel.."
                                                sMsgtext = "For " & sNomTech & _
                                                           "- ON-SITE Job " & intJob_id & ". at " & sTimePromised & _
                                                              "; " & sCustomer & "; Ph: " & sCustomerPhone & _
                                                              "; At: " & sCustomerAddress & ".."
                                                '-k_MAX_SMS_SIZE -
                                                sMsgtext = VB.Left(sMsgtext, k_MAX_SMS_SIZE)

                                                Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- Sending msg: " & vbCrLf & _
                                                               "To:" & sMobile & "- " & sMsgtext & ".")
                                                bSmsOk = mbSendSmsAndLog(intJob_id, sNomTech, _
                                                                         "WAKEUP", sMobile, sMsgtext, oleDbTransaction1)
                                                If bSmsOk Then
                                                    '= intSentCount += 1
                                                End If  '-sent-
                                            End If  '-sms count-
                                        Else '- failed to get JobOther SMS r/set-
                                            '-- release
                                            oleDbTransaction1.Rollback()
                                        End If  '- get JobOther SMS.
                                    Else  '-bad mobile-
                                        Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- BAD Mobile for " & sNomTech & " !!!")
                                    End If  '-mobile ok.
                                Else  '- NO tech record found.
                                    '-- can't do anything with this job..
                                    msReminderStatus = sTimeNow & " ERROR- No Staff details for: " & sNomTech
                                    Call mUpdateReminderStatus()
                                    Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- ERROR: No Staff details for: " & sNomTech)
                                End If  '--get staff-
                            Next dtRow1 '-job-
                        End If  '--count-
                        Thread.Sleep(3000)
                    Else
                        Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders- No Jobs table returned !!!")
                    End If
                    '--  Do SQL count of all Wakeups/reminders SMS sent today..
                    sSql = "SELECT count (*) FROM dbo.JobOtherDetails  "
                    sSql &= " WHERE (JobOtherType='" & k_OTHER_TYPE_SMS_SENT & "') "  '-SMS type of Other-
                    sSql &= "  AND (DATEDIFF(day,CURRENT_TIMESTAMP, JobOtherDateCreated)=0) "  '-writtentoday-
                    sSql &= "  AND (jobOtherTextdata1='WAKEUP');"
                    If gbGetSqlScalarIntegerValue(mOleDbCnn, sSql, intWakeUpCount) Then
                    End If
                    sSql = "SELECT count (*) FROM dbo.JobOtherDetails  "
                    sSql &= " WHERE (JobOtherType='" & k_OTHER_TYPE_SMS_SENT & "') "  '-SMS type of Other-
                    sSql &= "  AND (DATEDIFF(day,CURRENT_TIMESTAMP, JobOtherDateCreated)=0) "  '-writtentoday-
                    sSql &= "  AND (jobOtherTextdata1='REMINDER');"
                    If gbGetSqlScalarIntegerValue(mOleDbCnn, sSql, intReminderCount) Then
                    End If

                    sTimeNow = Format(Now, "HH:mm.")
                    msReminderStatus = sTimeNow & " =" & intWakeUpCount & "." & intReminderCount & " OnSite Sms sent today."
                    If Not bSmsOk Then
                        msReminderStatus &= vbCrLf & " ERRORS occurred- see Runtime Log."
                    End If
                    Call mUpdateReminderStatus(bSmsOk)
                    '-close connection-
                    mOleDbCnn.Close()

                Else
                    Call gbLogMsg(gsRuntimeLogPath, "ON-SITE Reminders-ERROR- Failed to get JOBs data.." & vbCrLf & _
                                        gsGetLastSqlErrorMessage()) '= & vbCrLf & "Job Record may be in use.."
                    Thread.Sleep(30000)
                End If  '--connect- get-
            End If  '-wake up time-
            '== Call gbLogMsg(gsRuntimeLogPath, "ON-SITE SMS reminder is sleeping..")
            DoEvents()

            '-- Eventually, this sleep should be about 5 mins..
            Thread.Sleep(mIntMainSleepTime * 1000)  '-msecs.
        End While  '--all day-

    End Sub  '- StaffReminders_DoWork--
    '= = = = = = = = = = = = = = = = =  =

End Class  '-clsStaffReminders=
'= = = = = = = = = = = = == = 

'== the end= 
