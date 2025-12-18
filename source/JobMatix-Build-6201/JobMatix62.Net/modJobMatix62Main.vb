Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System
Imports System.Data
Imports System.Data.OleDb

Module modJobMatix62Main

    '-- Startup Sub Main..

    '===

    '== This is the Startup routine for the Open Source JobMatix..
    '== This is the Startup routine for the Open Source JobMatix..
    '== This is the Startup routine for the Open Source JobMatix..

    ' Copyright 2021 grhaas@outlook.com

    'Licensed under the Apache License, Version 2.0 (the "License");
    'you may Not use this file except In compliance With the License.
    'You may obtain a copy Of the License at

    '    http://www.apache.org/licenses/LICENSE-2.0

    'Unless required by applicable law Or agreed To In writing, software
    'distributed under the License Is distributed On an "AS IS" BASIS,
    'WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
    'See the License For the specific language governing permissions And
    'limitations under the License.

    '= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 

    '==
    '== Created 3501.0606  06Jun2018=  for JobMatix35 etc..
    '==    --  to Startup Launching POS system which can then call JobTracking exe. 
    '==
    '= = = = = = = = ==  =
    '-- Startup Sub Main.. For all JobMatix functions..
    '== 
    '-- Find and connect Sql Server..
    '--  Then call POS Main form.. ..=

    '--  If no POS tables in selected DB, then LAUNCH JobTracking..
    '==
    '==
    '== >> 3501.0731  31-July-2018=  Updated for JobMatix35 etc..
    '==    --  For POS updates.. 
    '==    --  Remember last App started up (POS or JobTr), and prompt for change.
    '==           If no change, startup as per last time.
    '==
    '== >> 4201.0627  27-June-2019=  Updated for JobMatix42 etc..
    '==    --  For POS updates.. 
    '==
    '=== = = = = = = = = = = = = = = = = ==  = = = = = = = = = = = = = = = = = = =

    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==
    '==
    '==  C. For JobMatix42Main-  Incorporate  Setup..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '=3411.1221=
    '-SetForegroundWindow-  To switch to POS process..
    Declare Function SetForegroundWindow Lib "user32.dll" (ByVal hwnd As Integer) As Integer


    Private mbIsInitialising As Boolean = False
    Private mbStartingUp As Boolean

    Private msServer As String = ""
    Private msSqlConnectString As String = ""
    Private mCnnSql As OleDbConnection
    Private mbIsSqlAdmin As Boolean = False
    Private msSqlDbName As String = ""
    Private mColSqlDBInfo As Collection

    '-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    Private msJobmatixAppName As String = ""

    Private msVersion As String
    '-- v3.1--
    '-- v3.1--
    '-- v3.1--

    '-- wait form--
    Private mFormWait1 As frmWait

    Private mbConnected As Boolean = False
    'Private mbLicenceOK As Boolean = False
    'Private mbIsCreateMode As Boolean = False

    'Private msInputDBNameJobs As String = ""
    'Private msStaffBarcode As String = ""
    'Private msLog As String = ""

    Private msSqlVersion As String = ""
    Private mIntJobMatixDBid As Integer = -1

    '-- Background Worker results..-
    '-- Background Worker results..-

    Private mbSqlServerSearchOk As Boolean = False
    Private WithEvents mBackgroundWorkerSearch As BackgroundWorker
    Private mColSQLServerInstances As Collection = Nothing

    Private WithEvents mBackgroundWorkerGetSchema As BackgroundWorker
    Private mbSqlServerGetSchemaOK As Boolean = False
    Private msSqlServerGetSchemaError As String = ""

    Private msBuildSchemaLog As String = ""

    Private msSettingsPath As String = ""
    '==3311.224=
    Private mLocalSettings1 As clsLocalSettings

    Private mbInstallingJobMatixPOS As Boolean = False
    Private mbIsInstalling As Boolean = False

    Private msJobMatixVersion As String = ""

    Private mSysInfo1 As clsSystemInfo
    Private msOriginalJobMatixDBName As String = ""
    Private msRetailHostname As String = ""

    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '= = = = = = = = = = = = = = = = == 
    '-===FF->


    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String,
                      Optional ByVal sHeader As String = "JobMatix62 Exe")

        mFormWait1 = New frmWait
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.labHdr.Text = sHeader
        mFormWait1.waitTitle = "JobMatix starting.."
        mFormWait1.Show()
        DoEvents()
    End Sub '- mWaitFormOn-
    '-= = = = =  = = = = = =

    '-- kill (hide) wait form--
    Private Sub mWaitFormOff()

        mFormWait1.Hide()
        mFormWait1.Close()
        mFormWait1.Dispose()
        DoEvents()
    End Sub  '--wait--
    '= = = = = = = = = = = = = = = = =

    '-- Is RetailManager--

    Private Function mbIsRetailManager() As Boolean

        mbIsRetailManager = (LCase(msRetailHostname) = "retailmanager")
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 

    '-- IsIsJobmatixPOS--

    Private Function mbIsJobmatixPOS() As Boolean

        mbIsJobmatixPOS = (LCase(msRetailHostname) = "jobmatixpos")
    End Function  '- mbIsJobmatixPOS--
    '= = = = = = = = = = = = = = = = == 
    '-===FF->

    '-- SQL Connect--
    Private Function mbSqlConnect(ByVal sServer As String) As Boolean
        Dim sConnect, sMsg As String

        mbSqlConnect = False
        sConnect = "Provider=SQLOLEDB; Server=" & msServer &
                   "; Trusted_Connection=true; Integrated Security=SSPI; ConnectionTimeout=10; "
        msSqlConnectString = sConnect

        Call mWaitFormOn("Connecting to Sql Server: " & vbCrLf & msServer & "..")
        If gbConnectSql(mCnnSql, sConnect) Then
            Call mWaitFormOff()
            '==bConnected = True '== bLoggedOn = True
            mbSqlConnect = True
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call mWaitFormOff()
            sMsg = "Login to Sql-Server '" & msServer & "' has failed." & vbCrLf &
                  "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf &
             "Check that your Windows Logon has been added to SQL-Server logins.."

            Call gbLogMsg(gsRuntimeLogPath, "=== ERROR in SQL Connect in JobMatix [Sub] Main Function.." &
                          vbCrLf & vbCrLf & sMsg)

            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Function
        End If '--connected-
    End Function '--mbSqlConnect-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '- B a c k g r o u n d  W o r k e r  Threads --
    '- B a c k g r o u n d  W o r k e r  Threads --


    '- Background worker thread to look for Sql servers..--
    '--  Started from Form Load event routine..
    '-   This event handler is where the actual BG work is done.
    '-- http://msdn.microsoft.com/en-us/library/System.ComponentModel.BackgroundWorker(v=vs.80).aspx  

    Private Sub backgroundWorkerSearch_DoWork(ByVal sender As Object,
                                               ByVal e As DoWorkEventArgs) _
                                              Handles mBackgroundWorkerSearch.DoWork

        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        ' Assign the result of the computation
        ' to the Result property of the DoWorkEventArgs
        ' object. This is will be available to the 
        ' RunWorkerCompleted eventhandler.
        '== e.Result = ComputeFibonacci(e.Argument, worker, e)

        mbSqlServerSearchOk = gbSQL_Enumerate_Main(mColSQLServerInstances)

    End Sub '- backgroundWorkerSearch_DoWork
    '= = = = = = = = = = = = = = = = = = = =  =


    '=- Background worker thread to Sql server DB schema..--
    '= --  Started from Form Load event routine..

    Private Sub backgroundWorkerGetSchema_DoWork(ByVal sender As Object,
                                              ByVal e As DoWorkEventArgs) _
                                              Handles mBackgroundWorkerGetSchema.DoWork

        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        '-msSqlServerGetSchemaError-
        Try
            If gbGetSqlSchemaInfo(mCnnSql, msSqlDbName, mColSqlDBInfo, msBuildSchemaLog) Then
                mbSqlServerGetSchemaOK = True
            End If
        Catch ex As Exception
            msSqlServerGetSchemaError = ex.Message
        End Try

    End Sub  '- backgroundWorkerGetSchema_DoWork--
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function mbFindSQlServer(ByRef sServerInstance As String,
                                     ByRef sServerMachine As String,
                                     ByRef sSqlInstance As String) As Boolean
        Dim ix As Integer
        Dim col1 As Collection
        Dim sMsg, s1 As String

        mbFindSQlServer = False
        '-- Now.. Find an Sql Server on the network..-
        Call mWaitFormOn("Pls Wait.  Checking for Sql Servers.." & vbCrLf &
                           "   This might take a moment.")
        '-- search..-
        mbSqlServerSearchOk = False
        '==  bOk = gbSQL_Enumerate_Main(mColSQLServerInstances)
        '-- Start the Sql Search operation in the background.
        mBackgroundWorkerSearch.RunWorkerAsync()
        '-- wait for completion-
        '-- ie Wait for the BackgroundWorker to finish the Search.
        While mBackgroundWorkerSearch.IsBusy
            '- Keep UI messages moving, so the WAIT form remains 
            '-     responsive during the asynchronous operation.
            Application.DoEvents()
        End While

        Call mWaitFormOff()  '--hide wait form-
        Application.DoEvents()
        If Not mbSqlServerSearchOk Then
            MsgBox("ERROR- Sql Server Search Failed !!!", MsgBoxStyle.Exclamation)
            '==  Me.Close()
            Exit Function
        End If
        If (Not (mColSQLServerInstances Is Nothing)) AndAlso (mColSQLServerInstances.Count > 0) Then
            '- CHOOSE a server and do the CONNECT..--
            If (mColSQLServerInstances.Count = 1) Then  '--only one..-
                col1 = mColSQLServerInstances(1)
                '== msServer = col1("ServerName") & "\" & col1("InstanceName")
                '= 3107.922- Fixed..
                sServerInstance = col1("ServerName") & "\" & col1("InstanceName")
                sServerMachine = col1.Item("ServerName")
                sSqlInstance = col1.Item("InstanceName")
                mbFindSQlServer = True
                Application.DoEvents()
                '== MsgBox("Found one SQL server..", MsgBoxStyle.Information)
            Else  '--more than 1..  must choose..
                '== MsgBox("Found " & mColSQLServerInstances.Count & " SQL servers..", MsgBoxStyle.Information) 'TEST-
                sMsg = ""
                ix = 0
                For Each col1 In mColSQLServerInstances
                    ix += 1
                    sMsg &= vbCrLf & CStr(ix) & ". " & col1("ServerName") & "\" & col1("InstanceName")
                Next
                sMsg = "Found " & mColSQLServerInstances.Count & " SQL servers.." & vbCrLf &
                                     "Please choose a server instance." & vbCrLf & vbCrLf & sMsg
                '-- Choose.--
                s1 = InputBox(sMsg, "JobMatixPOS Sql Servers")
                If (s1 <> "") AndAlso IsNumeric(s1) AndAlso
                             (CInt(s1) > 0) AndAlso (CInt(s1) <= mColSQLServerInstances.Count) Then
                    col1 = mColSQLServerInstances(CInt(s1))
                    sServerInstance = col1("ServerName") & "\" & col1("InstanceName")
                    mbFindSQlServer = True
                ElseIf (s1 = "") Then
                    '= Me.Close()
                    Exit Function
                End If
                '-ok-
                sServerMachine = col1.Item("ServerName")
                sSqlInstance = col1.Item("InstanceName")
            End If '-only one-
            '-- connect to selected sql server
        Else  '--no servers.
        End If  '-found server..
    End Function  '-- mbFindSQlServer-
    '= = = = = =  = = = = = = = = =  
    '-===FF->

    '-- SELECT DB  -

    '--SelectDatabase--
    '--  Get all accessible POS DB's, and select one..

    Private Function mbSelectDatabase(ByRef colDBs As Collection,
                                        ByVal bIsAdmin As Boolean,
                                        ByVal bIsInstalling As Boolean,
                                        ByVal bInstallingJobMatixPOS As Boolean,
                                         ByRef sSelectedDBName As String) As Boolean
        Dim col1 As Collection
        Dim colList2 As New Collection
        Dim vName As Object
        Dim s1, s2, sMsg, sName As String
        Dim sList As String = ""
        Dim intCount As Integer = 0
        Dim sDBnamePOS As String = ""
        Dim bOK As Boolean

        mbSelectDatabase = False
        sSelectedDBName = ""
        If (colDBs Is Nothing) OrElse (colDBs.Count <= 0) Then
            '=3103.123=
            If Not bIsAdmin Then
                Exit Function
            End If
        End If
        If Not (colDBs Is Nothing) Then
            For Each col1 In colDBs
                intCount += 1
                sName = col1.Item("dbname")
                sList &= CStr(intCount) & ".  " & sName & vbCrLf
                colList2.Add(sName)
            Next
        End If
        If bIsAdmin Then  '--add CREATE/RESTORE-
            '== NOW HAVE START FORM with Buttons.
        End If
        '=3107.707== Bypass Start Form if INITIAL install..-
        '=3411.0214-  ALWAYS Show Startup Form if we got here...

        Dim sStartResult As String
        '= If (colDBs.Count <= 0) And bIsInstalling Then
        'sStartResult = "CREATE-DB-RM"
        'If bInstallingJobMatixPOS Then
        '    sStartResult = "CREATE-DB-JMXPOS"  '--setup user was chooser..
        'End If
        '=Else  '--normal-
        '-- Show startup form and get result..
        Dim frmStart1 As New frmStartup(Nothing, bIsAdmin, msSqlServerComputer, msServer, msSqlVersion, msJobMatixVersion, colDBs)
        frmStart1.ShowDialog()
        If frmStart1.cancelled Then
            '= Me.Close()
            Exit Function
        End If
        sStartResult = frmStart1.result
        '== End If
        If (sStartResult <> "") Then  '=(frmStart1.result <> "") Then
            s2 = sStartResult '= frmStart1.result
            '-- DO Create /Restore Here..--
            '=3311.328--  Can be CREATE-DB-RM or CREATE-DB-POS
            If UCase(VB.Left(s2, 9)) = "CREATE-DB" Then
                '-- call Create-
                '--  CREATE..--
                '--  NEW 2921==

                '-- TEMP out-
                Dim frmSetupJobsDB1 As New frmSetupJobsDB
                frmSetupJobsDB1.CreateNewDB = True
                '=3311.328=  Tell DB setup if RM or POS..-
                If sStartResult = "CREATE-DB-JMXPOS" Then
                    frmSetupJobsDB1.NewDBIsPOS = True    '--else is RM..-
                End If
                frmSetupJobsDB1.sqlConnection = mCnnSql
                frmSetupJobsDB1.SqlServer = msServer
                frmSetupJobsDB1.ServerComputerName = msSqlServerComputer
                '==frmSetupJobsDB.DatabaseName = msSqlDbName
                frmSetupJobsDB1.DBNames = colList2 '--send list of all DB's-
                frmSetupJobsDB1.ShowDialog()
                frmSetupJobsDB1.Close()

                '= Me.Close()
                Exit Function
            ElseIf UCase(s2) = "RESTORE-DB" Then
                '-- call Restore-
                Call mWaitFormOn("Starting Restore..", "RESTORE database")
                If Not gbRestoreJobsDatabase(mCnnSql, msCurrentUserNT, (mFormWait1.OpenDlg1),
                            msAppPath, (mFormWait1.labHdr), msSqlDbName) Then
                    Call mWaitFormOff()
                    Call gbLogMsg(gsRuntimeLogPath, "NO RESTORE done.." & vbCrLf)
                    MsgBox("No restore done..", MsgBoxStyle.Exclamation)
                Else  '--ok-
                    Call mWaitFormOff()
                    MsgBox("Restore Completed.." & vbCrLf & vbCrLf &
                          "  Application must be re-started", MsgBoxStyle.Information)
                End If '-restore-

                '= Me.Close()
                Exit Function
            ElseIf UCase(s2) = "MIGRATE-DB" Then
                '-- call Upgrade/Migrate--

                '-- to Restore DB, Add POS tables, and Import RM data..
                '-- Show Migration form and get result..

                '- TEMP out-
                Dim frmMigrate1 As New frmMigration(New Form, bIsAdmin,
                                                msSqlServerComputer, msServer, mCnnSql, msSqlVersion, msJobMatixVersion)
                frmMigrate1.ShowDialog()

                '= Me.Close()
                Exit Function

            Else '--DB selected-
                sDBnamePOS = s2
                sSelectedDBName = s2
                mbSelectDatabase = True
            End If
        End If  '-s1-
    End Function  '-select-
    '= = = = = = = = = = = = =
    '-===FF->


    '-- Flag..-
    Private mbLoginRequested As Boolean = False

    '--  "sub" Main  --
    '--  "sub" Main  --
    '--  "sub" Main  --

    '==   Target-New-Build-6201 --  (22-October-2020)

    '==    If /Setup in command line, then call setup..
    '==    If /Setup in command line, then call setup..
    '==    If /Setup in command line, then call setup..


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cmdArgs"></param>
    ''' <returns></returns>
    Public Function Main(ByVal cmdArgs() As String) As Integer
        Dim s1, s2, sList As String
        Dim sAppPath, sCmdLine, sErrors As String
        Dim ix, lngStart As Integer
        Dim returnValue As Integer = 0
        Dim bConnected As Boolean = False

        '=  Dim ix, lngStart As Integer
        Dim iPos As Short
        '= Dim sShowMsgKey, sHdr, sMsgText As String
        Dim sMsgText, sSchemaInfo As String
        Dim bShow As Boolean = False
        '= Dim bConnected As Boolean = False
        Dim bESCapeRequested As Boolean
        '= Dim rs1 As DataTable   '== ADODB.Recordset
        Dim colAllJobsDBs, colUserJobsDBs As Collection
        Dim v1 As Object
        Dim asTokens() As String
        Dim sLastAppStartup As String = ""
        Dim sNextAppStartUp As String = ""
        Dim bSilentRunning As Boolean = False
        Dim bIsSetup As Boolean = False

        returnValue = 0
        mBackgroundWorkerSearch = New BackgroundWorker
        '--  Look for an Sql Server..--
        msServer = ""
        '-- get previous server if any..

        Try  '--Main-
            msSettingsPath = gsLocalJobsSettingsPath("JobMatix62") '= msAppPath & K_SAVESETTINGSPATH
            '==3311.214= Load up Settings.
            '--  check local settings for sql server name..
            mLocalSettings1 = New clsLocalSettings(msSettingsPath)
            If mLocalSettings1.queryLocalSetting("sqlserver", s1) Then
                '= MsgBox("TEST- Found setting sqlserver: " & s1, MsgBoxStyle.Information)
                msServer = s1
            End If
            If mLocalSettings1.queryLocalSetting("LastAppStartup", s1) Then
                sLastAppStartup = s1
            End If
            sNextAppStartUp = sLastAppStartup  '--default if no change.

            msCurrentUserName = gsGetCurrentUser()
            mbLoginRequested = False

            '==    If /Setup in command line, then call setup..

            '==  For JobMatix62Main- OPEN SOURCE version...
            '==  For JobMatix62Main- OPEN SOURCE version...
            '--  SETUP has been removed..  Not included in O/S Version..
            '--  SETUP has been removed..  Not included in O/S Version..

            '- See if there are any arguments.
            '-- WinRar SFX will pass cmdline arg << -sp:quiet >> 
            '--      to us here as ":quiet"..
            'sList = ""
            'If cmdArgs.Length > 0 Then
            '    For argNum As Integer = 0 To UBound(cmdArgs, 1)
            '        '- Insert code to examine cmdArgs(argNum) and take
            '        '- appropriate action based on its value.
            '        s1 = Trim(cmdArgs(argNum))
            '        If VB.Left(s1, 1) = ":" Then
            '            s1 = Trim(Mid(s1, 2))  '--drop colon.
            '        End If
            '        sList &= vbCrLf & s1
            '        If LCase(s1) = "/quiet" Or LCase(s1) = "quiet" Then
            '            bSilentRunning = True
            '        End If
            '        If LCase(s1) = "/setup" Or LCase(s1) = "setup" Then
            '            bIsSetup = True
            '        End If
            '    Next argNum
            'End If

            ''--  Check environment variables for "quiet"..
            ''--   The Install SFX can be launched with "sp:quiet"
            'Dim sCmds() As String

            'If bIsSetup Then  '-we were launched via the SFX-INSTALL.. 
            '    Try
            '        '=  try get the environment variable if exists.
            '        '--We can pick up original QUIET cmd if needed.
            '        '-- This gives us the original SFX launch command line..
            '        sCmdLine = Environment.GetEnvironmentVariable("sfxcmd")
            '        If (sCmdLine IsNot Nothing) Then
            '            sCmds = Split(LCase(sCmdLine), " ")
            '            '-- 1st cmd is the sfx exe..
            '            If (sCmds.Length > 1) Then
            '                For Each s1 In sCmds
            '                    If InStr(s1, "quiet") > 0 Then
            '                        bSilentRunning = True
            '                        Exit For
            '                    End If
            '                Next s1
            '            End If
            '        End If
            '    Catch ex As Exception
            '        MsgBox("No env. variables..", MsgBoxStyle.Information)
            '    End Try
            '    Dim intRcode As Integer
            '    '-- Launch Install..
            '    '== MsgBox("Launching install..", MsgBoxStyle.Information)  '-test..

            '    intRcode = POS_Setup_Main(bSilentRunning)
            '    Return intRcode
            '    '--Install finished or aborted..
            'End If  'setup-

            '-- END- SETUP has been removed..  Not included in O/S Version..
            '-- END- SETUP has been removed..  Not included in O/S Version..

            '== NOT INSTALLING..-
            '-- get on with JobMatix running.

            '= Me.KeyPreview = True
            Call mWaitFormOn("Press ESC for SQL Server Search.", "JobMatix is starting..")
            DoEvents()
            lngStart = CInt(VB.Timer()) '--starting seconds.-
            While (CInt(VB.Timer()) <= lngStart + 3)
                System.Windows.Forms.Application.DoEvents()
            End While
            '==bLoginRequested = frmSplash1.loginRequested '--get request if any..-
            mbLoginRequested = mFormWait1.loginRequested
            Call mWaitFormOff()

            '- Connect-

            mCnnSql = New OleDbConnection  '= ADODB.Connection
            '== mCnnSql.ConnectionTimeout = 10 '--seconds..-
            Dim bRetrying As Boolean = False
            While Not bConnected
                If (msServer = "") Or mbLoginRequested Then  '-- no server defined..
                    '- go search network.
                    If mbFindSQlServer(s1, msSqlServerComputer, msSqlServerInstance) Then
                        msServer = s1
                    Else  '-nothing-
                        MsgBox("No SQL server found..", MsgBoxStyle.Information)
                        msServer = ""  '--force input box name entry.
                    End If
                End If
                If (msServer = "") Or bRetrying Then  '--still no server defined.. get name.
                    '==   MsgBox("No SQL server found..", MsgBoxStyle.Exclamation)
                    '=3107.902= --Give Option to eneter a server name..
                    msServer = Trim(InputBox("No SQL server connected.." & vbCrLf &
                                  "Enter 'Server\Instance' name if available..", "Sql Server connect-", msServer))
                    If (VB.Left(msServer, 2) = "\\") Then
                        msServer = VB.Mid(msServer, 3)  '--drop  the double slash..
                    End If
                    If (msServer = "") Then '-no name-
                        Call gbLogMsg(gsRuntimeLogPath, "No Sql server available.. Exiting JobMatix." & vbCrLf)
                        '= Me.Close()
                        returnValue = 1011
                        Exit Try '= Function
                    End If
                End If
                '-- Found Server- try connect-
                bConnected = mbSqlConnect(msServer)
                bRetrying = True   '--in case failed.-
            End While  '-Not bConnected-

            If Not bConnected Then
                returnValue = 1012
                Exit Try
            End If
            Call gbSetupSqlVersion(mCnnSql)
            msSqlVersion = gsGetSqlVersion()

            '- save SQLSERVER in Settings ?-
            '= Call mbSaveSetting("SQLSERVER", msServer)
            Call mLocalSettings1.SaveSetting("SQLSERVER", msServer)
            '-- spti server/instance..
            asTokens = Split(msServer, "\")
            If (asTokens.Length > 0) Then
                msSqlServerComputer = asTokens(0)
            End If
            If (asTokens.Length > 1) Then
                msSqlServerInstance = asTokens(1)
            End If
            msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName

            '-- Choose DB or Create/Restore..

            mbIsInstalling = False  '=3107.706=
            '-- setup.. user chooses..
            mbInstallingJobMatixPOS = False   '=3311.328-
            '== End If
            sCmdLine = Trim(VB.Command())
            gbVerbose = False
            If InStr(1, UCase(sCmdLine), "/V") > 0 Then gbVerbose = True
            gbDebug = False
            If InStr(1, UCase(sCmdLine), "/SUPERADMIN") > 0 Then gbDebug = True
            gbDevel = gbDebug
            '=3107.706=
            If gbGetCmd(sCmdLine, "IsInstalling", s1) Then
                mbIsInstalling = True
                '=3311.328-  Setup sends choice- RM/JobMatixPOS-
                If gbGetCmd(sCmdLine, "InstallJobMatixPOS", s1) Then
                    mbInstallingJobMatixPOS = True
                End If
            End If

            '=3501.0616=
            sAppPath = gsAppPath()
            If VB.Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
            msAppPath = sAppPath

            '-  new log each month..-

            '=3501.0616-- MUST DO THIS !!-
            msJobmatixAppName = My.Application.Info.AssemblyName
            Call gSubSetAppName(msJobmatixAppName)

            Call gbLogMsg(gsRuntimeLogPath, "=== " & msJobmatixAppName & "- Sub main is starting..")
            With My.Application.Info.Version
                msJobMatixVersion = msJobmatixAppName & "-  v" & CStr(.Major) & "." &
                                                 .Minor & ". Build: " & .Build & "." & .Revision
            End With
            '== gsAssemblyName = Replace(My.Application.Info.AssemblyName, ".exe", "")  '-replace in case.
            '= Must do this.
            Call gSubSetAppVersion(msJobMatixVersion)

            Try  '=main2-
                msSqlVersion = gsGetSqlVersion()
                '--  check if we are sqlAdmin privileged..--
                mbIsSqlAdmin = gbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")
                '-- save as global--
                Call gbSetIsSqlAdmin(mbIsSqlAdmin)
                Call gbLogMsg(gsRuntimeLogPath, msJobmatixAppName & "- Sub main-" & vbCrLf &
                              "Logged in OK to SQL Server Instance:  " & msServer & vbCrLf &
                                                  "SQL-Server Version: " & msSqlVersion & ".." & vbCrLf)

                '-- SUB MAIN Logged on ok--

                '--  NOW find databases.-- and select..
                sMsgText = "-- Server is: " & msServer & vbCrLf
                sMsgText &= "-- Checking for JobMatix databases.. " & vbCrLf
                If mbIsSqlAdmin Then
                    sMsgText &= vbCrLf & "  (Press ESC for DB-Admin functions..)"
                End If  '-admin-
                Call mWaitFormOn(sMsgText, msJobMatixVersion)
                lngStart = CInt(VB.Timer()) '-- TEST starting seconds.-
                If Not gbGetJobmatixDatabases(mCnnSql, colAllJobsDBs, colUserJobsDBs) Then
                    Call mWaitFormOff()
                    MsgBox("getting database list has failed." & vbCrLf &
                               "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf &
                          "Please check that your Windows Logon has been added to SQL-Server logins..",
                           MsgBoxStyle.Exclamation)
                    returnValue = 1004
                    '= Me.Close()
                    Exit Try  '= Function
                End If '--get..-

                '- TESTING-  give 2 secs-
                If mbIsSqlAdmin Then
                    While (CInt(VB.Timer()) <= lngStart + 3)
                        System.Windows.Forms.Application.DoEvents()
                    End While
                End If

                bESCapeRequested = mFormWait1.loginRequested
                Call mWaitFormOff()
                '--testing-
                '== MsgBox("Found " & colUserJobsDBs.Count & "  databases", MsgBoxStyle.Information)

                '-- Choose database..-
                msSqlDbName = ""
                '-- No dbs in list.. if Admin, Offer CREATE/RESTORE and Wait..
                '--                  Else msg: Must be admin for Create.
                If (colUserJobsDBs.Count <= 0) Then  '- no DB-
                    If mbIsSqlAdmin Then
                        If Not mbIsInstalling Then
                            MsgBox("No JobMatix database could be found at the SQL Server: " & msServer & vbCrLf &
                                      " Select from Create or Restore a database..", MsgBoxStyle.Information)
                        End If
                        If mbSelectDatabase(colUserJobsDBs, mbIsSqlAdmin, mbIsInstalling, mbInstallingJobMatixPOS, s1) Then
                            msSqlDbName = s1
                        Else  '-nothing -(could be Upgrade/migration)- will exit- 
                            returnValue = 1005
                            Exit Try '= Function
                        End If  '-select -
                    Else  '--not admin-
                        MsgBox("No JobMatix database could be found at Server: " & msServer & vbCrLf &
                                "For NT user " & msCurrentUserNT & vbCrLf &
                                "Check that your Windows Logon has been added to SQL-Server logins.", MsgBoxStyle.Exclamation)
                        returnValue = 1006
                        '= Me.Close()
                        Exit Try  '= Function
                    End If '-admin-
                ElseIf (colUserJobsDBs.Count = 1) Then  '- ONE DB-
                    '-- IF 1 DB, NOT Admin-login,  then go ahead into application..
                    '-- IF 1 DB, plus Admin-login,  If ESC pressed then Show Create/Restore + DB list (1).
                    '--                             Else Continue to open the only database..
                    If mbIsSqlAdmin Then
                        If bESCapeRequested Then
                            '-- show DB name and CREATE/RESTORE options.
                            If mbSelectDatabase(colUserJobsDBs, mbIsSqlAdmin, mbIsInstalling, mbInstallingJobMatixPOS, s1) Then
                                msSqlDbName = s1
                            Else  '-nothing -- will exit- 
                                returnValue = 1005
                                Exit Try '= Function
                            End If '-select-
                        Else  '-- no ESC request..  GO ahead with the ONLY DB-
                            msSqlDbName = colUserJobsDBs(1)("dbname")
                        End If  '-- ÉSC-
                    Else  '-- not admin..  GO ahead with the ONLY DB-
                        msSqlDbName = colUserJobsDBs(1)("dbname")
                    End If '--admin
                Else  '--Multiple DBs--
                    '-- If >1 jobs db's-- Admin-login, we show DB list..--
                    '--  Admin- show DB LIST and CREATE/RESTORE options.
                    '--   Not Admin- show DB LIST Only.
                    If mbSelectDatabase(colUserJobsDBs, mbIsSqlAdmin, mbIsInstalling, mbInstallingJobMatixPOS, s1) Then
                        msSqlDbName = s1
                    Else  '-nothing -- will exit- 
                        returnValue = 1005
                        Exit Try '= Function
                    End If '-select-
                End If  '--DB count-

                If (msSqlDbName = "") Then
                    MsgBox("Can't find any JobTracking database for user: '" &
                           msCurrentUserName & "'.." & vbCrLf & vbCrLf &
                           "(JobMatix Admin user may need to add '" & msCurrentUserName &
                           "' to JobMatix users..)", MsgBoxStyle.Exclamation)
                    returnValue = 1007
                    '= Me.Close()
                    Exit Try  '=Function
                End If

                '-- Now set current database..--
                '-  !! MUST retry because for non-admin user -
                '----  it doesn't stick the first time..--

                '-- USE Jobs DB and check result..-
                If Not gbSetCurrentDatabase(mCnnSql, msSqlDbName) Then
                    '== If (LCase(sCurrentDB) <> LCase(sDBnameJobs)) Then
                    MsgBox("= Failed to set current Database for: " & msSqlDbName & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(gsRuntimeLogPath, "= Failed to set current Database for: " & msSqlDbName & vbCrLf &
                                                       "Jobmatix is closing down..")
                    returnValue = 1008
                    '= Me.Close()
                    Exit Try  '=Function '-- False..-
                End If
                '--  USE must have worked..-
                '-- SAVE id for who_using.. mIntJobMatixDBid --
                If gbGetSelectValueEx(mCnnSql, "SELECT DB_ID() AS current_db_id;", v1) Then
                    If Not (v1 Is Nothing) Then
                        mIntJobMatixDBid = CLng(v1)
                    End If
                End If  '--get-
                Call gSetJobMatixDBid(mIntJobMatixDBid)
                Call gbLogMsg(gsRuntimeLogPath, "= OK.. current Database is set to: [" & msSqlDbName & "].." & vbCrLf &
                                                   "  == and it took " & msSqlDbName & " USE attempts..")
                '-- Connect and DB choice done..

                Call gbLogMsg(gsRuntimeLogPath, "-- SqlAdmin: Checking user permissions for VWSS.. " & vbCrLf)
                '--  CHECK VSS PERMISSIONS--
                If Not gbCheckVWSSpermissions(mCnnSql, mbIsSqlAdmin, msSqlDbName, msCurrentUserNT) Then
                    returnValue = 1009
                    '= Me.Close()
                    Exit Try '= Function '-- False..-
                End If  '--check-
                Call gbLogMsg(gsRuntimeLogPath, "-- SqlAdmin: Done Checking permissions.. " & vbCrLf)

                '-  GET Sql Schema..--
                '-- AGAIN..  Must USE Jobs DB and check result..-
                If Not gbSetCurrentDatabase(mCnnSql, msSqlDbName) Then
                    '== If (LCase(sCurrentDB) <> LCase(sDBnameJobs)) Then
                    MsgBox("= Failed to set current Database for: " & msSqlDbName & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(gsRuntimeLogPath, "= Failed to set current Database for: " & msSqlDbName & vbCrLf &
                                                       "Jobmatix is closing down..")
                    returnValue = 1010
                    '= Me.Close()
                    Exit Try '= Function '-- False..-
                End If
                '--  USE must have worked..-

                '--ok. Now can Get DB schema info..-- 
                '-- Load DB-Info for selected DB..--
                Call mWaitFormOn("-- Server: " & msServer & vbCrLf & vbCrLf &
                                "-- Getting schema  " & vbCrLf & " For DB '" & msSqlDbName & "'..", msJobMatixVersion)
                System.Windows.Forms.Application.DoEvents()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

                '-- BG Worker to get schema.--
                mBackgroundWorkerGetSchema = New BackgroundWorker
                mbSqlServerGetSchemaOK = False
                msSqlServerGetSchemaError = ""
                '-- start worker thread-
                '-- Start the Sql GetSchema operation in the background.
                mBackgroundWorkerGetSchema.RunWorkerAsync()
                '-- wait for completion-
                '-- ie Wait for the BackgroundWorker to finish the Search.
                While mBackgroundWorkerGetSchema.IsBusy
                    '- Keep UI messages moving, so the WAIT form remains 
                    '-     responsive during the asynchronous operation.
                    Application.DoEvents()
                End While

                If mbSqlServerGetSchemaOK Then  '= gbGetSchemaInfo(mCnnSql, msSqlDbName, mColSqlDBInfo, True) Then 
                    Call mWaitFormOff()
                    msBuildSchemaLog &= vbCrLf & "= loaded info for DATABASE:  " & msSqlDbName & "  = = = =" & vbCrLf
                    '==colDBs.Add colDBInfo, LCase$(sName)
                Else
                    Call mWaitFormOff()
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    msBuildSchemaLog &= " *** ERROR- FAILED to build  SQL catalogue for DB: " & msSqlDbName & vbCrLf &
                                         msSqlServerGetSchemaError & vbCrLf & " =="
                    MessageBox.Show(" *** ERROR- FAILED to build  SQL catalogue for DB: " & msSqlDbName & " ==" & vbCrLf &
                                        msSqlServerGetSchemaError & vbCrLf &
                                                        " ==", "JobMatix Get Schema..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    returnValue = 1011
                    '= Me.Close()
                    Exit Try  '= Function '-- False..-
                End If '--ok--
                '-- ok to go.-

                sSchemaInfo = gsShowSqlSchema(msSqlDbName, mColSqlDBInfo) '--display all schema info-- 
                sSchemaInfo &= vbCrLf & "= = = Log Saved: " &
                                                    VB.Format(Now, "dd-MMM-yy hh:mm") & " = = =" & vbCrLf
                Call glSaveTextFile(gsJobMatixLocalDataDir("JobMatix62") & "\" & msSqlDbName & "_schema.txt",
                                           msBuildSchemaLog & vbCrLf & "= = = = =" & vbCrLf & vbCrLf & sSchemaInfo)
                '-- Connect and DB Startup COMPLETED..

                If (returnValue <> 0) Then
                    Exit Try
                End If

            Catch ex As Exception
                MsgBox("Error in JobMatix Sub Main2 function.." & vbCrLf & ex.Message)
                '==Me.Close()
                returnValue = 1003
                '=End
            End Try  '--main2-

            If (returnValue <> 0) Then
                Exit Try
            End If
            '=3501.0613=  now the main stuff..
            '=3501.0613=  now the main stuff..

            mSysInfo1 = New clsSystemInfo(mCnnSql)
            '==  If No POS tables (Legacy JobTracking) then launch JobTracking, and exit..

            If mSysInfo1.exists("RETAILHOSTNAME") Then
                msRetailHostname = mSysInfo1.item("RETAILHOSTNAME")
            End If
            '== msOriginalJobMatixDBName -
            '=3103.305==
            If mSysInfo1.exists("ORIGINALJOBMATIXDBNAME") Then
                msOriginalJobMatixDBName = mSysInfo1.item("ORIGINALJOBMATIXDBNAME")
            End If
            '-- Default Retail Host..--
            If (msRetailHostname = "") Then
                msRetailHostname = "RetailManager" '--default..--
            End If

            '= If POS db..  Give choice of start up..
            Dim bIsJobMatixPOS As Boolean = False
            Dim sMsgWhich As String = "Press ESC to start up with "
            Dim bNewStartupRequested As Boolean = False

            If (LCase(msRetailHostname) = "jobmatixpos") And
                        mColSqlDBInfo.Contains("stock") Then  '-just in case..  '--JM POS--
                bIsJobMatixPOS = True
            End If

            If bIsJobMatixPOS Then  '--choose startup-  POS or JobTracking.
                If (UCase(sLastAppStartup) = "POS") Or (sLastAppStartup = "") Then
                    sMsgWhich &= "JobTracking.."
                    sNextAppStartUp = "JobTracking"
                Else  '-JobTracking or none was last used..
                    sMsgWhich &= "JobMatixPOS.."
                    sNextAppStartUp = "POS"
                End If
                '-- show and pause..
                Call mWaitFormOn(sMsgWhich, "JobMatix starting up..")
                DoEvents()
                lngStart = CInt(VB.Timer()) '--starting seconds.-
                While (CInt(VB.Timer()) <= lngStart + 3)
                    System.Windows.Forms.Application.DoEvents()
                End While
                bNewStartupRequested = mFormWait1.loginRequested
                Call mWaitFormOff()

                If bNewStartupRequested Then  '-swap from last start.
                    '- save new choice..
                    Call mLocalSettings1.SaveSetting("LastAppStartup", sNextAppStartUp)
                Else  '- no change-
                    sNextAppStartUp = sLastAppStartup
                End If
            End If  '-bIsJobMatixPOS-

            If bIsJobMatixPOS And ((sNextAppStartUp = "POS") Or (sNextAppStartUp = "")) Then  '-just in case..  '--JM POS--

                Dim frmPOS3Main1 As frmPosMainMdi  '=frmPOS34Main

                If (Application.OpenForms.Count > 0) Then
                    For Each frm1 As Form In Application.OpenForms
                        If frm1.Name = "frmPosMainMdi" Then
                            MsgBox("POS Form is already open..", MsgBoxStyle.Information)
                            Exit Function
                        End If
                    Next frm1
                End If '-count=
                Try
                    '--  load POS3  Main form and show it..
                    frmPOS3Main1 = New frmPosMainMdi  '=frmPOS34Main
                Catch ex As Exception
                    MsgBox("Error in loading POS Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    Exit Function
                End Try
                '--show-
                Try
                    frmPOS3Main1.connectionSql = mCnnSql
                    frmPOS3Main1.SqlServer = msServer
                    frmPOS3Main1.DBname = msSqlDbName
                    frmPOS3Main1.dbInfoSql = mColSqlDBInfo
                    'frmPOS3Main1.StaffId = mIntStaffId
                    'frmPOS3Main1.StaffName = msStaffName
                    ''-msStaffBarcode-
                    'frmPOS3Main1.StaffBarcode = msStaffBarcode  '-03-Jan-2018-
                    'frmPOS3Main1.mainVersionPOS = msVersionPOS
                    frmPOS3Main1.JobmatixAppName = msJobmatixAppName
                    '= Mdi Main is MODAL now-
                    frmPOS3Main1.ShowDialog()

                Catch ex As Exception
                    MsgBox("Error in showing POS Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            Else   '-RM- OR POS, but wants JobTracking startup
                '-- NO POS..  Must be legacy JobTracking with RetalManager.
                '==  JobTrackin Requested OR
                '--      (Legacy JobTracking)  launch JobTracking, and exit..

                Call gbLogMsg(gsRuntimeLogPath, "modJobMatix62Main is executing JobTracking exe..  ")

                Dim intResult As Integer  '- Double
                Dim sJT350Name, sJT350Path, sJT350Args As String

                '==   Target-New-Build-6201 --  (16-June-2021)
                '== sJT350Name = "JmxJT420ex"
                sJT350Name = "JMxJT620ex"
                '== END  Target-New-Build-6201 --  (16-June-2021)

                sJT350Path = msAppPath & sJT350Name & ".exe"
                sJT350Args = " /server=" & msServer &
                               " /JT_dbname=" & msSqlDbName & " /JobMatixAppName=" & msJobmatixAppName
                '-- check if process already running..
                '==
                '== >> 3501.0916  16-Sepg-2018=  Updated for JobMatix35 etc..
                '==    --  Allow Multiple instances of JobTracking to be started from Main.... 
                '==

                Dim ap As Process() = Process.GetProcessesByName(sJT350Name)
                'If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
                '    '-- is alive.. so switch to it..
                '    Dim intId As IntPtr = ap(0).MainWindowHandle
                '    '-found-  Make it active-
                '    intResult = SetForegroundWindow(intId)
                'Else '-not there.. so can launch..-
                Try
                    Process.Start(sJT350Path, sJT350Args)
                Catch ex As Exception
                    MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sJT350Path & vbCrLf & vbCrLf &
                           "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
                End Try
                'End If  '-nothing-
                Exit Try  '= Function
            End If  '-pos-

        Catch ex As Exception
            returnValue = 1002
            MessageBox.Show("Error Connecting to Sql Server, " & vbCrLf &
                            "in JobMatix62 Sub-Main startup." & vbCrLf & ex.Message,
                              "JobMatix Sub Main", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try  '-main-

        '- Insert call to appropriate starting place in your code.
        '- On return, assign appropriate value to returnValue.
        '- 0 usually means successful completion.
        '== MsgBox("The RAs31 application is terminating with error level " & CStr(returnValue) & ".")
        Call gbLogMsg(gsRuntimeLogPath, "JobMatix62Main is exiting with error/info level " _
                                             & CStr(returnValue) & "." & vbCrLf & "= = = = = = = =" & vbCrLf)
        Return returnValue

    End Function  '-Main-
    '= = = = = = = = = = = =



End Module '--modJobMatix62Main-

'== the end =
