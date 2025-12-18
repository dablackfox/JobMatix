
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports System.Windows.Forms
'= Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports System.Data
Imports System.Data.OleDb
Imports System.Security.AccessControl
Imports System.Diagnostics
Imports System.Security
Imports System.Threading

Module modBackupMain

    '==
    '==  Background Scheduled task to backup JobMatix Database..
    '==  Background Scheduled task to backup JobMatix Database..

    '== This is the Startup routine for the Open Source JobMatix Backup Agent...
    '== This is the Startup routine for the Open Source JobMatix Backup Agent...

    '- Copyright 2021 grhaas@outlook.com

    '- Licensed under the Apache License, Version 2.0 (the "License");
    '- you may Not use this file except In compliance With the License.
    '- You may obtain a copy Of the License at

    '-    http://www.apache.org/licenses/LICENSE-2.0

    '- Unless required by applicable law Or agreed To In writing, software
    '- distributed under the License Is distributed On an "AS IS" BASIS,
    '- WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
    '- See the License For the specific language governing permissions And
    '- limitations under the License.

    '= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 

    '==
    '==  Background Scheduled task to backup JobMatix Database..
    '==     -- Console Application 3501.0821  21Aug2018=  ...
    '==
    '==   -- Updated 3501.1007  --07-Oct-2018=
    '==
    '==   -- Updated 3501.1010  --10-Oct-2018=
    '==        -- Backup is now written to local Server Path, and then copied to NetworkTargetPath.
    '==
    '=== = = = = = = = = = = = = = = = = ==  = = = = 

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (03-July-2021)
    '==   Target-New-Build-6201 --  (03-July-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Private msVersionMain As String = ""
    Private msJobmatixAppName = "JMxBackupAgent"

    Private mbIsInitialising As Boolean = False
    Private mbStartingUp As Boolean
    Private mbIsTesting As Boolean = False   '-- allows the console.readline cmds to work.

    Private msServer As String = ""
    Private mCnnSql As OleDbConnection
    Private mbIsSqlAdmin As Boolean = False
    Private msSqlDbName As String = ""
    Private mColSqlDBInfo As Collection

    Private msSqlConnectString As String = ""

    '-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    Private msComputerName As String '--local machine--
    Private msAppPath As String

    Private mbConnected As Boolean = False
    Private mbLicenceOK As Boolean = False
    Private mbIsCreateMode As Boolean = False

    Private msInputDBNameJobs As String = ""
    Private msStaffBarcode As String = ""
    Private msLog As String = ""

    Private msSqlVersion As String = ""
    Private mIntJobMatixDBid As Integer = -1

    Private msBuildSchemaLog As String = ""
    Private clsWinInfo1 As clsWinSpecial

    Private msLastSqlErrorMessage As String = ""
    Private mlngSqlMajorVersion As Integer = -1

    Private msBackupTargetLocalPath As String = ""  '-local to server-
    Private msBackupTargetNetworkPath As String = ""  '-local to server-

    Private mEventLog1 As EventLog
    Private msOurSourceName As String = "JobMatixDbBackup"  '-for event log..-

    Private mIntBackupDaysToKeep As Integer = 7  '--default keep for 7 days..
    Private mIntBackupsTodayToKeep As Integer = 3  '--default keep last 3 for today...

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 
    '-===FF->

    '--showMsg-

    Private Sub showMsg(ByVal sMsg As String, _
                        Optional ByVal bWriteEventLog As Boolean = False, _
                        Optional ByVal bIsError As Boolean = False)

        System.Console.WriteLine(sMsg)

        '--TESTING-
        If bWriteEventLog Then
            If bIsError Then
                mEventLog1.WriteEntry(sMsg, EventLogEntryType.Error)
            Else
                mEventLog1.WriteEntry(sMsg, EventLogEntryType.Information)
            End If
        End If

    End Sub  '-showMsg-
    '= = = = = = = = = = = =  = = = = == =


    '==   Adds an ACL entry on the specified file for the specified account. 
    Private Function AddFileSecurity(ByVal strFileName As String, _
                         ByVal account As String, _
                          ByVal rights As FileSystemRights, _
                           ByVal controlType As AccessControlType) As Boolean

        ' Get a FileSecurity object that represents the  
        ' current security settings. 
        Dim fSecurity As FileSecurity = File.GetAccessControl(strFileName)

        ' Add the FileSystemAccessRule to the security settings.  
        Dim accessRule As FileSystemAccessRule = _
            New FileSystemAccessRule(account, rights, controlType)
        fSecurity.AddAccessRule(accessRule)

        Try
            ' Set the new access settings.
            File.SetAccessControl(strFileName, fSecurity)
            AddFileSecurity = True
        Catch ex As Exception
            '= MsgBox("Failed to Update ACL for File: " & vbCrLf & strFileName & vbCrLf & vbCrLf & ex.Message)
            showMsg("Failed to Update ACL for File: " & vbCrLf & strFileName & vbCrLf & vbCrLf & ex.Message)
            AddFileSecurity = False
        End Try

    End Function  '-AddFileSecurity-
    '= = = = = = = = = = = = = = = = = = = = = = =

    '-- Assembly name..
    Public Function gsAssemblyName() As String

        '=3501.0611-  MUST get actual assembly ie DLL..
        Dim assemblyThis As Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim assName As AssemblyName
        assName = assemblyThis.GetName
        '= modFileSupport33-
        gsAssemblyName = Replace(Replace(assName.Name, ".exe", ""), ".dll", "")  '-replace in case.

    End Function  '-assembly name-
    '= = = = = = = == = = = = =  =
    '-===FF->

    '-[Create]-Return JobMatix Local data DIR.-

    Public Function gsJobMatixLocalDataDir(Optional ByVal strAppName As String = "") As String

        gsJobMatixLocalDataDir = ""
        '-- get programData dir.
        Dim s1 As String = clsWinInfo1.AppDataDir
        Dim sUser As String = Replace(System.Environment.UserName, " ", "_")
        Dim sUserDir As String
        Dim sProgramName As String = strAppName
        If (sProgramName = "") Then
            sProgramName = gsGetAppName()
            If (sProgramName = "") Then
                If (gsAssemblyName() <> "") Then
                    sProgramName = gsAssemblyName()
                Else '-default-
                    sProgramName = msJobmatixAppName  '= gK_APP_NAME
                End If
            End If
        End If '-omitted-
        If (Right(s1, 1) <> "\") Then
            s1 &= "\"
        End If
        Dim sJobMatixDir As String = s1 & sProgramName  '=  "JobMatix32"
        '- make Jobmatix sub Dir-
        If Not Directory.Exists(sJobMatixDir) Then
            '--make it-
            Try
                Directory.CreateDirectory(sJobMatixDir)
            Catch ex As Exception
                'MsgBox("Failed to create directory: " & vbCrLf & _
                '              sJobMatixDir & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                showMsg("!! Failed to create directory: " & vbCrLf & _
                              sJobMatixDir & vbCrLf & ex.Message)
                Exit Function
            End Try
        End If  '-exists-
        '- make Users sub Dir-
        sUserDir = sJobMatixDir & "\" & sUser
        If Not Directory.Exists(sUserDir) Then
            '--make it-
            Try
                Directory.CreateDirectory(sUserDir)
            Catch ex As Exception
                'MsgBox("Failed to create User Data directory: " & vbCrLf & _
                '              sUserDir & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                showMsg("Failed to create User Data directory: " & vbCrLf & _
                                  sUserDir & vbCrLf & ex.Message)
                Exit Function
            End Try
        End If  '-exists-

        gsJobMatixLocalDataDir = sUserDir  '== s1 & "JobMatix31"

    End Function '-- gsJobMatixLocalDataDir --
    '= = = = =  = = = = == = = = = = = =
    '-===FF->


    '--gsAppPath-

    Public Function gsAppPath() As String
        gsAppPath = My.Application.Info.DirectoryPath

    End Function '-gsAppPath-
    '= = = = = = = == = = = = = 

    '-- main line must SET appName.. (comes from highest level assembly..

    Public Sub gSubSetAppName(ByVal strAppname As String)
        '-save-
        msJobMatixAppName = strAppname

    End Sub  '- gSubSetAppName-
    '= = = = = = =  = = = = = =

    '-- Return pre-set app name.  eg. JobMatix35  ===

    Public Function gsGetAppName() As String
        gsGetAppName = msJobMatixAppName

    End Function '-gsGetAppName-
    '= = = = = = = == = = = = = 

    '---   Make ErrorLogPath and RuntimeLogPath into FUNCTIONS !!

    Public Function gsErrorLogPath() As String

        '-  new log each month..-
        Dim s1 As String = Format(CDate(DateTime.Today), "yyyy-MM-dd")

        gsErrorLogPath = gsJobMatixLocalDataDir(gsGetAppName) & "\" & gsGetAppName() & "-Runtime-" & VB.Left(s1, 7) & ".log"

    End Function '-gsErrorLogPath
    '= = = = = = ==  = = = = = == = = = = =

    Public Function gsRuntimeLogPath() As String

        gsRuntimeLogPath = gsErrorLogPath()
    End Function '-gsRuntimeLogPath
    '= = = = = = ==  = = = = = == = = = = =
    '-===FF->

    '-- SQL Connect--

    Private Function mbSqlConnect(ByVal sServer As String) As Boolean
        Dim sConnect, sMsg As String

        mbSqlConnect = False
        sConnect = "Provider=SQLOLEDB; Server=" & msServer & _
                   "; Trusted_Connection=true; Integrated Security=SSPI; ConnectionTimeout=10; "
        showMsg("Wait- Connecting to Sql Server: [" & msServer & "]..")
        If gbConnectSql(mCnnSql, sConnect) Then
            '= Call mWaitFormOff()
            '==bConnected = True '== bLoggedOn = True
            msSqlConnectString = sConnect
            mbSqlConnect = True
        Else
            sMsg = "Login to Sql-Server: ['" & msServer & "'] has failed." & vbCrLf & _
                  "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf & _
             "Check that your Windows Logon has been added to SQL-Server logins.."

            Call gbLogMsg(gsRuntimeLogPath, "=== ERROR in SQL Connect in JobMatix DbBackup- [Sub] Main Function.." & _
                          vbCrLf & vbCrLf & sMsg)
            showMsg("=== ERROR in SQL Connect in JobMatix DbBackup- [Sub] Main Function.." & _
                          vbCrLf & vbCrLf & sMsg, True, True)
            Exit Function
        End If '--connected-
    End Function '--mbSqlConnect-
    '= = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '==Log msg to log file--
    '== 3107.806= Re-write for .Net 4.5-
    '==  --  and Optionally Add Security ==

    Public Function gbLogMsg(ByVal sLogPath As String, _
                                 ByVal strLogMsg As String, _
                          Optional ByVal bMakePublic As Boolean = True) As Boolean

        Dim sw1 As StreamWriter
        Dim s1, sData As String

        gbLogMsg = False
        If (sLogPath = "") Then Exit Function
        '--- !!  ADD date/time to msg..--
        '                                                     My.Application.Info.AssemblyName & s1 & vbCrLf & strLogMsg
        '= 3501.0615=
        s1 = " =V" & msVersionMain & "="
        sData = vbCrLf & VB.Format(Today, "dd-MMM-yyyy") & " = " & VB.Format(TimeOfDay, "hh:mm:ss = ") & _
                                                             gsAssemblyName() & s1 & vbCrLf & strLogMsg
        '== 3107.806= Re-write for .Net 4.5-
        If Not File.Exists(sLogPath) Then  '-must create-
            '-- Create the file.  
            Try
                sw1 = File.CreateText(sLogPath)
                '-- add data msg-
                sw1.WriteLine(sData)
                sw1.Close()
                sw1.Dispose()
                gbLogMsg = True
            Catch ex As Exception
                'MsgBox("Failed to Create Text to File: " & vbCrLf & sLogPath & vbCrLf & _
                '                                               vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                showMsg("Failed to Create Text to File: " & vbCrLf & sLogPath & vbCrLf & _
                                               vbCrLf & ex.Message)
                Exit Function
            End Try
            '-- make public for everyone..--
            If bMakePublic Then
                If AddFileSecurity(sLogPath, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow) Then
                    '-ok-
                Else
                    gbLogMsg = False
                    Exit Function
                End If
            End If  '-public-
        Else    '-file exists--
            ' Append msg to the file.
            Try
                sw1 = File.AppendText(sLogPath)
                sw1.WriteLine(sData)
                sw1.Close()
                sw1.Dispose()
                gbLogMsg = True
            Catch ex As Exception
                'MsgBox("Failed to append Text to File: " & vbCrLf & sLogPath & vbCrLf & _
                '                                            vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                showMsg("Failed to append Text to File: " & vbCrLf & sLogPath & vbCrLf & _
                                                vbCrLf & ex.Message)
            End Try
        End If  '-exists-
    End Function '--log msg--
    '= = = = = = = = = = = = = =  =
    '-===FF->

    '-----look in cmd line for "/cmd"---
    '---- extract command line parameter---
    '------return as sParm---

    Public Function gbGetCmd(ByVal sCmdLine As String, _
                               ByVal sCmd As String, _
                               ByRef sParm As String) As Boolean
        Dim k, i, j, px As Integer
        Dim s1, s2 As String

        sParm = "" : s2 = ""
        gbGetCmd = False
        k = InStr(1, UCase(sCmdLine), "/" & UCase(sCmd)) '--locate cms--
        If (k > 0) Then '--found--
            s1 = Mid(sCmdLine, k + Len(sCmd) + 1) '--get tail after cmd--
            If s1 = "" Then '--no more--
                gbGetCmd = True
            Else '--some tail-
                If (Left(s1, 1) <> " ") And (Left(s1, 1) <> "=") Then Exit Function '--bad terminator-
                s1 = Trim(s1) '--drop leading spaces-
                If (Left(s1, 1) = "=") Then '--w
                    s1 = Mid(s1, 2) '---drop--
                    s1 = Trim(s1) '--and drop leading spaces-
                End If
                i = 1 '--scan for end--
                gbGetCmd = True
                If (Left(s1, 1) = """") Then '--quoted parm--
                    s1 = Mid(s1, 2) '---drop 1st quote--
                    While (i < Len(s1)) And (Mid(s1, i, 1) <> """") '--find closing quote-
                        i = i + 1
                    End While
                    sParm = Left(s1, i - 1)
                Else
                    '--While (i < Len(s1)) And ((Mid$(s1, i, 1) = " ") Or (Mid$(s1, i, 1) = "="))
                    '--  i = i + 1
                    '--Wend
                    If (i <= Len(s1)) Then
                        s2 = Mid(s1, i) '--should/be start of parm--
                        If (Len(s2) < 1) Or (Left(s2, 1) = "/") Then Exit Function '--no parm--
                        '--must be  term by space===
                        i = InStr(1, s2, " ") '--scan past space --
                        If (i > 1) Then
                            sParm = Left(s2, i - 1) '--get parm excl space--
                        Else
                            sParm = s2
                        End If
                    End If
                End If '--quoted-
            End If '--some tail-
        End If

    End Function '--get cmd--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '--===FF->

    '-- get sql scalar value..

    Private Function mbGetSelectValueEx(ByRef cnnSql As OleDbConnection, _
                                              ByVal sSql As String, _
                                             ByRef vResult As Object) As Boolean
        Dim cmd1 As New OleDbCommand

        mbGetSelectValueEx = False

        cmd1.Connection = cnnSql
        cmd1.CommandText = sSql
        cmd1.CommandType = CommandType.Text

        Try
            vResult = cmd1.ExecuteScalar
            If Not vResult Is Nothing Then
                mbGetSelectValueEx = True
            Else
                Console.WriteLine("NOTHING result returned from Sql Execute Scalar.." & vbCrLf)
            End If
        Catch ex As Exception
            Console.WriteLine("Error in Sql Execute Scalar:" & vbCrLf & ex.Message)
        End Try

    End Function  '-mbGetSelectValueEx-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '--===FF->


    '== M a i n-  
    '== M a i n-  

    Public Function Main(ByVal cmdArgs() As String) As Integer

        Dim s1, s2, sList, sMsg, asTokens() As String
        Dim sCmdLine, sErrors As String
        Dim ix, returnValue As Integer
        Dim v1 As Object
        '= Dim sSchemaInfo As String
        Dim bAdminTest As Boolean = False
        Dim bRunningOnServer As Boolean = False

        clsWinInfo1 = New clsWinSpecial
        msComputerName = My.Computer.Name

        '- See if there are any arguments.
        sList = ""
        If (cmdArgs.Length > 0) Then
            For argNum As Integer = 0 To UBound(cmdArgs, 1)
                '- Insert code to examine cmdArgs(argNum) and take
                '- appropriate action based on its value.
                sList &= vbCrLf & cmdArgs(argNum)
            Next argNum
        End If
        msVersionMain = "JmxBackup Main v:" & CStr(My.Application.Info.Version.Major) & "." & _
                                         My.Application.Info.Version.Minor & ", Build: " & _
                                             My.Application.Info.Version.Build & "." & _
                                                 My.Application.Info.Version.Revision
        showMsg(msVersionMain)
        '-  No Cmdline parms.. show parms help..
 
        If (cmdArgs.Length <= 0) Then
            Console.WriteLine()
            Console.WriteLine( _
                   "  JobMatix BACKUP Agent- " & vbCrLf & _
                   "  Vers: " & msVersionMain & vbCrLf & _
                   "  Program will call Sql Script to create DB Backup File on the 'BackupLocalPath' path " & vbCrLf & _
                   "     that is accessibly by Sql Server. ie: c:\users\public\..  " & vbCrLf & _
                   "     Program will then copy the file to the TargetNetwork Path, and delete local copy. " & vbCrLf & vbCrLf & _
                   "  Allowed CmdLine arguments are: " & vbCrLf & _
                   "      /Server= SqlServerName (ie. machine\instance)" & vbCrLf & _
                   "      /JT_DBname= databaseName (ie. JobMatix db name)" & vbCrLf & _
                   "      /BackupLocalPath= Server Temp. Local FileSystemPath " & vbCrLf & _
                   "            (ie. directory local to SQL server-  eg  c:\users\public\.. )" & vbCrLf & vbCrLf & _
                   "      /BackupTargetNetworkPath= Target NetworkPath " & vbCrLf & _
                   "           (ie. Final Target directory with network share name)" & vbCrLf & _
                   "    -- OPTIONAL: " & vbCrLf & _
                   "      /BackupDaysToKeep= NoOfDays (ie. will be deleted after this many days.)" & vbCrLf & _
                   "      /BackupsTodayToKeep= NoOfBackup Files (ie. Max. to keep for any day)" & vbCrLf & _
                   "      /Testing= YES (ie. Needs Console interaction to continue.)" & vbCrLf & vbCrLf & _
                   "   NOTES:  " & vbCrLf & _
                   "      1. Program should be launched ON SERVER machine at regular intervals (eg by Task Scheduler)" & vbCrLf & _
                   "            It will purge expired Backup files, Create a new DB Local Backup file, " & _
                   "            copy it to the 'BackupTargetNetworkPath' Dest., delete the local file and exit" & vbCrLf & _
                   "      2. Program writes progress messages to the Console.. " & vbCrLf & _
                   "                 Waits for reply ONLY if testing=yes.." & vbCrLf & _
                   "      3. If the Program is launched with no CmdLine arguments, " & vbCrLf & _
                   "             it will write out this help notice and exit." & vbCrLf & _
                   "      4. Program needs to be launched also every day at close of busimess." & vbCrLf & _
                   "      5. Program can be launched on SQL Server machine ONLY.." & vbCrLf & _
                   "           The argument  /BackupTargetNetworkPath is the Final Dest (ie the Cloud). " & vbCrLf & _
                   "      6   NB:  The argument  /BackupTargetNetworkPath must NOT be the same as 'BackupLocalPath' . " & vbCrLf & _
                   "              The backuo will FAIL if this is the case..' . " & vbCrLf & _
                   "      7. FATAL ERRORS- An ERROR message is written to the Windows (System) Event Log..." & vbCrLf & _
                   "           The SOURCE on the Event Log msg will be shown as '" & msOurSourceName & "'. " & vbCrLf & _
                   "      == the end ==" & vbCrLf)
            Console.WriteLine()
            Console.ReadLine()
            Console.ReadLine()
            Return 0
        Else  '--have args-
            Console.WriteLine()
            Console.WriteLine("The JobMatix BACKUP Main procedure is starting up.." & vbCrLf & _
                              "Cmd args are: " & vbCrLf & sList)
        End If '-args-
        '-- testing..
        If mbIsTesting Then
            Console.WriteLine("Press ENTER to continue")
            Console.ReadLine()
        End If

        Call gbLogMsg(gsRuntimeLogPath, vbCrLf & " JobMatix DbBackup- started..")

        '= Console.ReadLine()
        '-- setup stuff..--
        '=3501.0616=
        Dim sAppPath As String = gsAppPath()
        msAppPath = sAppPath

        '-- get cmd line parms..
        sCmdLine = Trim(VB.Command())
        If gbGetCmd(sCmdLine, "Server", s1) Then
            If (s1 <> "") Then
                msServer = s1
                msLog = msLog & "Server: " & s1 & vbCrLf
            End If
        End If
        If gbGetCmd(sCmdLine, "JT_DBName", s1) Then
            If (s1 <> "") Then
                msInputDBNameJobs = s1
                msLog = msLog & "Input JT_DBName: " & s1 & vbCrLf
            End If
        End If

        If gbGetCmd(sCmdLine, "JobmatixAppName", s1) Then
            If (s1 <> "") Then
                msJobmatixAppName = s1
                msLog = msLog & "msJobmatixAppName: " & s1 & vbCrLf
            End If
        End If
        '- msBackupTargetPath-
        If gbGetCmd(sCmdLine, "BackupLocalPath", s1) Then
            If (s1 <> "") Then
                msBackupTargetLocalPath = s1
                msLog = msLog & "BackupTargetPath: " & s1 & vbCrLf
            End If
        End If '--path-
        '- msBackupTargetNetworkPath-
        If gbGetCmd(sCmdLine, "BackupTargetNetworkPath", s1) Then
            If (s1 <> "") Then
                msBackupTargetNetworkPath = s1
                msLog = msLog & "BackupTargetNetworkPath: " & s1 & vbCrLf
            End If
        End If '--path-

        '--  Optional parms..
        '=  mIntBackupDaysToKeep As Integer = 7  '--default keep for 7 days..
        '=  mIntBackupsTodayToKeep As Integer = 3  '--default keep last 3 for today...
        If gbGetCmd(sCmdLine, "BackupDaysToKeep", s1) Then
            If (s1 <> "") AndAlso IsNumeric(s1) AndAlso _
                                   (CInt(s1) > 1) AndAlso (CInt(s1) <= 30) Then
                mIntBackupDaysToKeep = CInt(s1)
                msLog = msLog & "BackupDaysToKeep: " & s1 & vbCrLf
            End If
        End If '--days-
        If gbGetCmd(sCmdLine, "BackupsTodayToKeep", s1) Then
            If (s1 <> "") AndAlso IsNumeric(s1) AndAlso _
                                   (CInt(s1) > 1) AndAlso (CInt(s1) <= 30) Then
                mIntBackupsTodayToKeep = CInt(s1)
                msLog = msLog & "BackupsTodayToKeep: " & s1 & vbCrLf
            End If
        End If '--todays-

        '-- TESTING switch for the Console.readline's 
        If gbGetCmd(sCmdLine, "Testing", s1) Then
            If (s1 <> "") AndAlso _
                        ((UCase(s1) = "Y") Or (UCase(s1) = "YES")) Then
                mbIsTesting = True
                msLog = msLog & "Testing: " & s1 & vbCrLf
            End If
        End If '--todays-

        msStaffBarcode = ""

        '-- Open up the Event Log..
        '-- Open up the Event Log..
        '-- Open up the Event Log..

        '= When writing to an event log, you must pass the machine name where 
        '= the log resides.  Here the MachineName Property of the Environment class 
        '= is used to determine the name of the local machine.  Assuming you have 
        '= the appropriate permissions, it is also easy to write to event logs on 
        '= other machines.

        'Check if the Source exists 
        If Not EventLog.SourceExists(msOurSourceName, System.Environment.MachineName) Then
            Dim mySourceData = New EventSourceCreationData(msOurSourceName, "System")
            mySourceData.MachineName = "."  '-// Default to the local computer.
            Try
                System.Diagnostics.EventLog.CreateEventSource(mySourceData)
            Catch ex As Exception
                Call gbLogMsg(gsRuntimeLogPath, "ERROR- Failed to Create EventLog Source- " & vbCrLf & _
                                                  ex.Message & vbCrLf)
                showMsg("ERROR- Failed to Create EventLog Source- " & vbCrLf & _
                                                  ex.Message & vbCrLf & "-- And NO backup was done!", True, True)
                Return -1
            End Try
            '-ok-
            Thread.Sleep(5000)  '--slepp 5 second to ley source get registered..
        End If

        '- make us a log object 
        mEventLog1 = New EventLog("System", System.Environment.MachineName, msOurSourceName)

        'Writing to system log, in the similar way you can write to other 
        'logs that you have appropriate permissions to write to
        mEventLog1.WriteEntry("-- JobTracking Backup Agent has started..", EventLogEntryType.Information, CInt(10001))
        '- close log on exit..
        '= mEventLog1.Close()

        '- check parms..
        Dim sParmsErrors As String = ""

        If (msServer = "") Then
            '= Call gbLogMsg(gsRuntimeLogPath, "No server name supplied for JobTracking Backup app...")
            '= showMsg("No server name supplied for JobTracking Backup app..", True)
            sParmsErrors &= "-- No Sql Server name supplied for JobTracking Backup Agent..." & vbCrLf
            '= Return -1
        End If
        If (msInputDBNameJobs = "") Then
            '= Call gbLogMsg(gsRuntimeLogPath, "No JT_DBName name supplied for JobTracking Backup app...")
            '= showMsg("No Database name supplied for JobTracking app..", True)
            sParmsErrors &= "-- No DATABASE name supplied for JobTracking Backup Agent..." & vbCrLf
            '= Return -2
        End If
        If (msJobmatixAppName = "") Then
            '-- USE default ()
            msJobmatixAppName = "JMxBackupAgent"
        End If

        '- msBackupTargetPath-
        If (msBackupTargetLocalPath = "") Then
            sParmsErrors &= "-- No JobMatix Backup LocalPath supplied for Backup Agent..." & vbCrLf
        End If

        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        Call gSubSetAppName(msJobmatixAppName)

        Call gbLogMsg(gsRuntimeLogPath, " == " & msVersionMain & " [Sub] Main Function is starting..")

        '-- Check DB name must be _JobTracking or _jmpos..
        If (InStr(LCase(msInputDBNameJobs), "jobtracking") <= 0) And _
                                (InStr(LCase(msInputDBNameJobs), "jmpos") <= 0) Then
            '--not a valid JobMatix DB name.
            sParmsErrors &= "-- INVALID DATABASE name supplied for JobTracking Backup Agent..." & vbCrLf
        End If
        '-  show cmd line errors..
        If (sParmsErrors <> "") Then
            sMsg = "ERRORS in backup Agent input parameters.  " & vbCrLf & _
                                                    sParmsErrors & vbCrLf & "* No backup was done.."
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            showMsg(sMsg, True, True)
            mEventLog1.Close()
            Return -1  '=Exit Function '--ends-
        End If

        If (VB.Right(msBackupTargetLocalPath, 1) <> "\") Then
            msBackupTargetLocalPath &= "\"
        End If
        If (msBackupTargetNetworkPath <> "") Then
            If (VB.Right(msBackupTargetNetworkPath, 1) <> "\") Then
                msBackupTargetNetworkPath &= "\"
            End If
        End If

        '-- checks done..  set up server machine name..
        '-- get server/instance split.
        asTokens = Split(msServer, "\")
        If (asTokens.Length > 0) Then
            msSqlServerComputer = asTokens(0)
        End If
        If (asTokens.Length > 1) Then
            msSqlServerInstance = asTokens(1)
        End If
        bRunningOnServer = (UCase(msSqlServerComputer) = UCase(msComputerName))

        Console.WriteLine("Server Machine Name is: " & msSqlServerComputer)
        Console.WriteLine("Machine we are running on is: " & msComputerName)

        '-- DEPENDING if we're running on Server..
        If bRunningOnServer Then
            If (msBackupTargetNetworkPath = "") Then
                '-  ERROR--
                '-- must have Target network path as final Dest. of the Backup/.
                sMsg = "ERROR- We must have a Target network path as final Dest. of the Backup.. " & vbCrLf & _
                       "   This is missing.." & vbCrLf & _
                    "  * No backup was done.."
                Call gbLogMsg(gsRuntimeLogPath, sMsg)
                showMsg(sMsg, True, True)
                mEventLog1.Close()
                Return -1  '=Exit Function '--ends-
            End If
        Else
            '-NOT running on server.
            sMsg = "ERROR- This program can only be run on the SQL Server machine.. " & vbCrLf & _
                   "   It can not be run from a network Client machine.." & vbCrLf & _
                "  * No backup was done.."
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            showMsg(sMsg, True, True)
            mEventLog1.Close()
            Return -1  '=Exit Function '--ends-
        End If '- on server-
        Console.WriteLine("Backup Network path is: " & msBackupTargetNetworkPath)

        '-- Connect to Sql Server..
        mCnnSql = New OleDbConnection  '= ADODB.Connection
        '== mCnnSql.ConnectionTimeout = 10 '--seconds..-
        While Not mbConnected
            mbConnected = mbSqlConnect(msServer)
            If Not mbConnected Then
                 Call gbLogMsg(gsRuntimeLogPath, " == No Sql connection. ")
                showMsg("ERROR- No Sql connection.  ", True, True)
                mEventLog1.Close()
                Return -2  '=Exit Function '--ends-
            End If '--connected-
            '-- go around again..  UNTIL cancelled or ok..
        End While  '-Not bConnected-

        '-- test-
        '== MsgBox("Connected ok to :" & msServer, MsgBoxStyle.Information)

        Call gbSetupSqlVersion(mCnnSql)
        msSqlVersion = gsGetSqlVersion()

        '--  check if we are sqlAdmin privileged..--
        '= mbIsSqlAdmin = gbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")

        If mbGetSelectValueEx(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin');", v1) Then
            mbIsSqlAdmin = (CInt(v1) = 1)
        End If
        '-- save as global--
        Call gbSetIsSqlAdmin(mbIsSqlAdmin)
        Call gbLogMsg(gsRuntimeLogPath, "Jobmatix backup Agent- Sub Main-" & vbCrLf & _
                      " Logged in OK to SQL Server Instance:  " & msServer & vbCrLf & _
                                          "SQL-Server Version: " & msSqlVersion & vbCrLf)
        msCurrentUserName = System.Environment.UserName

        '-- USE --
        msSqlDbName = msInputDBNameJobs

        '-- USE Jobs DB and check result..-
        Try
            mCnnSql.ChangeDatabase(msSqlDbName)
        Catch ex As Exception
            showMsg("= Failed to set current Database for: " & msSqlDbName & vbCrLf & _
                                            vbCrLf & ex.Message, True, True)
            mEventLog1.Close()
            Return -5
        End Try
        '--  USE must have worked..-
        '= If gbGetSelectValueEx(mCnnSql, "SELECT DB_ID() AS current_db_id;", v1) Then
        '-- SAVE id for who_using.. mIntJobMatixDBid --
        If mbGetSelectValueEx(mCnnSql, "SELECT DB_ID() AS current_db_id;", v1) Then
            If Not (v1 Is Nothing) Then
                mIntJobMatixDBid = CLng(v1)
            End If
        End If  '--get-
        Call gSetJobMatixDBid(mIntJobMatixDBid)
        Call gbLogMsg(gsRuntimeLogPath, "JobTracking Sub Main- " & vbCrLf & _
                        "= OK.. current Database is set to: [" & msSqlDbName & "]..")

        '-- Connect and DB choice done..
        '-- Connect and DB choice done..
        '-- NO NEED to  Get DB schema info..-- 

        '-- USE MASTER DB for Backup..-
        Try
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            mCnnSql.ChangeDatabase("MASTER")
        Catch ex As Exception
            gbLogMsg(gsRuntimeLogPath, "= Failed to set current Database for MASTER.. " & vbCrLf & _
                                            vbCrLf & ex.Message)
            showMsg("ERROR= Failed to set current Database for MASTER.. " & vbCrLf & _
                                            vbCrLf & ex.Message, True, True)
        End Try

        sMsg = vbCrLf & "Jobmatix Backup Agent Connected ok- " & vbCrLf & _
                        "   SqlServer: " & msServer & "; " & vbCrLf & _
                        "   Database: " & msSqlDbName & vbCrLf & _
                        "   Backup Local Path: " & msBackupTargetLocalPath & vbCrLf & _
                        "   Backup Target Network Path: " & msBackupTargetNetworkPath & vbCrLf & _
                        "   BackupDaysToKeep: " & mIntBackupDaysToKeep & vbCrLf & _
                        "   BackupsTodayToKeep: " & mIntBackupsTodayToKeep & vbCrLf & "= = =" & vbCrLf
        Call gbLogMsg(gsRuntimeLogPath, sMsg)
        showMsg(sMsg)

        Console.WriteLine("Ready to purge old backups.")  '-TEST-
        '=Console.ReadLine()  '-TEST-

        '--  Check for old backup files to be purged..
        Dim directory1 As DirectoryInfo
        Dim aFiles() As FileInfo
        Dim sBakFileFullPath, sTitle, sSql As String
        Dim dtFileDate As DateTime

        Try
            directory1 = New DirectoryInfo(msBackupTargetNetworkPath)
        Catch ex As Exception
            showMsg("ERROR- Jobmatix Backup Agent- " & vbCrLf & _
                      " Failed to get Backup Path Dir.Info.." & vbCrLf & ex.Message, True, True)
            mEventLog1.Close()
            Return -8
        End Try
        '--Get list of backup files.
        aFiles = directory1.GetFiles("*.bak")

        '-- SORT by date modified..
        '==  http://geekswithblogs.net/ntsmith/archive/2006/08/17/88250.aspx
        '==

        '==   Target-New-Build-6201 --  (03-July-2021)
        '==  Don't sort empty array.
        If aFiles.Length > 0 Then
            Array.Sort(aFiles, New clsCompareFileInfo)
        End If
        '== END  Target-New-Build-6201 --  (03-July-2021)

        '-- First- delete any file older than mintBackupDaysToKeep.

        For Each file1 As FileInfo In aFiles
            sBakFileFullPath = msBackupTargetNetworkPath & file1.Name
            sTitle = file1.Name
            dtFileDate = File.GetLastWriteTime(sBakFileFullPath)

            '-- Check Filename for V22..--
            If (Not (InStr(LCase(sTitle), "jobtracking-v22") > 0)) And _
                      (Not (InStr(LCase(sTitle), "jmpos-v22") > 0)) Then
                'If (MsgBox("Warning: " & vbCrLf & "   The selected backup file:  '" & sTitle & "' " & vbCrLf & _
                '          "   may not be a Version 2.2 JobTracking backup.." & vbCrLf & vbCrLf & _
                '            "Do you want to continue and RESTORE this file ??", _
                '                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                '    Exit Function
                '== End If '--msgbox-
                '-ignore file--
            Else  '- ok-
                If DateDiff(DateInterval.Day, dtFileDate, Today) > mIntBackupDaysToKeep Then
                    '-- expired, so we can delete it now..
                    Try
                        File.Delete(sBakFileFullPath)
                        Console.WriteLine("Deleted expired backup: " & vbCrLf & sBakFileFullPath & vbCrLf)  '-TEST-
                    Catch ex As Exception
                        sMsg = "** ERROR- Failed to DELETE old Backup file: " & vbCrLf & _
                            "     " & sBakFileFullPath & vbCrLf & ex.Message & vbCrLf & _
                                 vbCrLf & "Another task may be accessing the file.."
                        Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
                        showMsg(sMsg, True, True)
                    End Try
                End If
            End If '--instr-
        Next file1

        '-- Now delete all but the last x backups done today.
        '-- Now delete all but the last x backups done today.
        '-- Now delete all but the last x backups done today.
        Console.WriteLine("Purging today's old backups.")  '-TEST-

        '--Get NEW list of REMAINING backup files.        
        Try
            directory1 = New DirectoryInfo(msBackupTargetNetworkPath)
        Catch ex As Exception
            showMsg("ERROR- Jobmatix Backup Agent- " & vbCrLf & _
                      " Failed to get Backup Path Dir.Info.." & vbCrLf & ex.Message, True, True)
            mEventLog1.Close()
            Return -8
        End Try

        aFiles = directory1.GetFiles("*.bak")
        Console.WriteLine("Found " & aFiles.Length & " old backups.")  '==   Target-New-Build-6201 

        '-- SORT AGAIN by date modified..
        '==

        '==   Target-New-Build-6201 --  (03-July-2021)
        '==  Don't sort empty array.
        If aFiles.Length > 0 Then
            '=Array.Sort(aFiles, New clsCompareFileInfo)
            Array.Sort(aFiles, New clsCompareFileInfo)
        End If
        '== END  Target-New-Build-6201 --  (03-July-2021)

        Dim listFilesToday As New List(Of FileInfo)
        Dim fileX As FileInfo
        '= Dim intDays As Integer

        For Each file1 As FileInfo In aFiles
            sBakFileFullPath = msBackupTargetNetworkPath & file1.Name
            dtFileDate = File.GetLastWriteTime(sBakFileFullPath)
            '= intDays = DateDiff(DateInterval.Day, dtFileDate, Today)
            '-test-
            'Console.WriteLine(file1.Name & "; Date: " & Format(dtFileDate, "dd-MMM-yyyy HH:mm") & "; intdays=" & intDays)
            'Console.ReadLine()
            If (dtFileDate.Day = DateTime.Today.Day) And _
                        (dtFileDate.Month = DateTime.Today.Month) And (dtFileDate.Year = DateTime.Today.Year) Then
                '-was written today..
                '- add to today's list
                listFilesToday.Add(file1)
            End If
        Next file1
        '-- purge any of today's more then mIntBackupsTodayToKeep --
        While (listFilesToday.Count >= mIntBackupsTodayToKeep)
            fileX = listFilesToday(0)  '-get oldest and delete it... 
            sBakFileFullPath = fileX.FullName
            Try
                File.Delete(sBakFileFullPath)
                sMsg = "Purging backups Today- Deleted excess backup: " & vbCrLf & sBakFileFullPath & vbCrLf
                Console.WriteLine(sMsg)
                Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
            Catch ex As Exception
                sMsg = "** ERROR- Failed to DELETE old Backup file: " & vbCrLf & _
                    "     " & sBakFileFullPath & vbCrLf & ex.Message & vbCrLf & _
                         vbCrLf & "Another task may be accessing the file.."
                Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
                showMsg(sMsg, True, True)
            End Try
            '-drop it out of the list. 
            listFilesToday.RemoveAt(0)
        End While

        '== ok.. Ready to Start the backup..
        '== 
        Console.WriteLine("Ready to start the next Backup..")  '-TEST-
        If mbIsTesting Then
            Console.WriteLine("Press ENTER to continue with the Backup..")  '-TEST-
            Console.WriteLine("Press Ctl-C to abort...")   '-TEST-
            Console.ReadLine()
        End If
        Console.WriteLine("Starting the Backup now..")  '-TEST-

        Dim cmd1 As New OleDbCommand
        '- make bak file name-
        sTitle = "JobsDB-" & msSqlDbName & "-V22-Backup_" & Format(CDate(DateTime.Today), "ddMMMyyyy") & "-" & _
                                                                                           Format(TimeOfDay, "HHmm") & ".bak"
        mCnnSql.ChangeDatabase(msSqlDbName)
        sSql = "BACKUP DATABASE " & msSqlDbName & vbCrLf
        sSql = sSql & "TO DISK = '" & msBackupTargetLocalPath & sTitle & "'" & vbCrLf
        sSql = sSql & "WITH NAME = 'Full Backup of JobTracking DB on " & _
                      Format(CDate(DateTime.Today), "ddMMMyyyy") & "-" & Format(TimeOfDay, "HH:mm") & "'" & vbCrLf
        cmd1.CommandText = sSql
        cmd1.Connection = mCnnSql
        Try
            cmd1.ExecuteNonQuery()
        Catch ex As Exception
            sMsg = "ERROR- Failed in Backup of jobs DATABASE: " & msSqlDbName & "'.." & vbCrLf & _
                                        "TO '" & msBackupTargetLocalPath & sTitle & "'.." & vbCrLf & vbCrLf & _
                                                 "Error info was:" & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            showMsg(sMsg & vbCrLf & "No backup was done..", True, True)
            mEventLog1.Close()
            Return -9
        End Try
        'If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
        '    MsgBox("Backup.. Failed in Backup of jobs DATABASE: " & msSqlDbName & "'.." & vbCrLf & _
        '                                "TO '" & msBackupTargetPath & sTitle & "'.." & vbCrLf & vbCrLf & _
        '                                         "Error info was:" & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Critical)
        'End If
        '-- all done-
        sMsg = "OK- Backup File of jobMatix DATABASE: " & msSqlDbName & "'.." & vbCrLf & _
                            " was created on Local Server Path: '" & msBackupTargetLocalPath & sTitle & "'.." & _
                              vbCrLf & vbCrLf & "=  Now we will copy .BAK file to Dest. NetworkPath = =" & vbCrLf
        Call gbLogMsg(gsRuntimeLogPath, sMsg)
        Console.WriteLine(sMsg)

        '-- NOW to copy the created backup file to the Dest. Network Path..
        Dim sSqlCopySourcePath As String = msBackupTargetLocalPath & sTitle
        Dim sDestPath As String = msBackupTargetNetworkPath & sTitle
        Try
            File.Copy(sSqlCopySourcePath, sDestPath, True)
        Catch ex As Exception
            sMsg = "ERROR- Copy aborted, or Error in copying file.." & vbCrLf & _
                                                   "Source path: '" & sSqlCopySourcePath & "'" & vbCrLf & _
                                                   "Dest. path:  '" & sDestPath & "'" & vbCrLf & _
                                "(Note: Destination path may be unreachable from Server, " & vbCrLf & _
                                "     or forbidden to the JobMatix Agent process.)" & vbCrLf & vbCrLf & _
                                "ERROR Text provided was:" & vbCrLf & _
                                ex.ToString & vbCrLf & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            showMsg(sMsg & vbCrLf & "Backup was not copied to Dest..", True, True)
            mEventLog1.Close()
            Return -11
        End Try
        sMsg = "Backup file was copied ok to: " & vbCrLf & "  [" & sDestPath & "]" & vbCrLf
        Call gbLogMsg(gsRuntimeLogPath, sMsg)
        Console.WriteLine(sMsg)
        '-- now to Delete the temp copy.
        '--ok- can delete the source copy.
        Try
            File.Delete(sSqlCopySourcePath)
            Console.WriteLine("Backup local file copy was deleted ok.. " & vbCrLf)
        Catch ex As Exception
            sMsg = "ERROR- Failed to Delete Local backup Copy.-" & vbCrLf & _
                "Error text was: " & vbCrLf & _
                         ex.ToString & vbCrLf & vbCrLf & _
                         "Backup was done, but local copy not deleted." & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            showMsg(sMsg & vbCrLf & "Backup was NOT copied to Dest..", True, True)
        End Try

        Call gbLogMsg(gsRuntimeLogPath, "JobMatix Backup Agent is exiting." & vbCrLf & _
                                                              "== all done ====" & vbCrLf)
        '- close log on exit..
        mEventLog1.Close()
        Console.WriteLine("JobMatix Backup Agent is exiting..")

        'For testing=
        If mbIsTesting Then
            Console.WriteLine("Press ENTER to exit..")
            Console.ReadLine()
        End If
        Return 0

    End Function  '-main-
    '= = = = = = = = = = = =

End Module  '-modBackupMain-
'= = = = = = =  = = = == = =

'== the end ===
