Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel

Imports System.Data
Imports System.Data.OleDb

'= Imports JMxJT420
Imports JobMatix3


Module modJT420Main

    '-- Startup Sub Main..

    '---for JobMatix V35 exe..--
    '-- Find and connect Sql Server..
    '--  Then call JobMatix DLL Main form.. ..=

    '==
    '==
    '== Created 3500.0610  10June2018=  ...
    '==
    '== -- Updated 3501.0724  24July-2018= ...
    '==           3501.0724= --  NewInJobMatix now loaded from Local Dir at runtime...
    '==
    '== -- Updated 4201.0627  27-June-2019= ...
    '==         For New version 4.2 of POS--...
    '==
    '=== = = = = = = = = = = = = = = = = ==  = = = = 

    '- JET DAO needs this..
    '== Friend DAODBEngine_definst As New DAO.DBEngine

    '= = = = = =

    '= Const K_SAVESETTINGSPATH As String = "localEmailSettings.txt"

    Private mbIsInitialising As Boolean = False
    Private mbStartingUp As Boolean

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

    Private msVersionPos As String
    '-- v3.1--
    '-- v3.1--
    '-- v3.1--

    '-- wait form--
    Private mFormWait1 As frmWait

    Private mbConnected As Boolean = False
    Private mbLicenceOK As Boolean = False
    Private mbIsCreateMode As Boolean = False

    Private msInputDBNameJobs As String = ""
    Private msStaffBarcode As String = ""
    Private msLog As String = ""

    Private msSqlVersion As String = ""
    Private mIntJobMatixDBid As Integer = -1

    Private mBackgroundWorkerGetSchema As BackgroundWorker
    Private mbSqlServerGetSchemaOK As Boolean = False

    Private msBuildSchemaLog As String = ""

    Private msJobmatixAppName As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->


    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String, _
                      Optional ByVal sHeader As String = "JobMatix Launch Exe")

        mFormWait1 = New frmWait
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.labHdr.Text = sHeader
        mFormWait1.waitTitle = msVersionPos
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
    '-===FF->

    '= 3203.105=
    '-- Make Create SQL for JobAttachments and RA_Attachments Tables.

    '-- FOR RAs34.EXE-  
    '-- This stolen from JobMatix modCreateJobs3

    'Const K_width_party_info As Integer = 500
    'Const K_width_staff_name As Integer = 50
    'Const K_width_comments As Integer = 1000

    ''= = = = = = = = = = = = =
    '-===FF->


    '-- lookup RM Staff to given BARCODE.--

    'Private Function mbLookupStaff(ByRef sBarcode As String, _
    '                                ByRef colRecord As Collection) As Boolean
    '    '= Dim sBarcode As String
    '    Dim sSql, s1 As String
    '    Dim colResult As Collection

    '    mbLookupStaff = False
    '    '--staff Signon..--
    '    '== Must be JobMatix POS lookup..

    '    sSql = "SELECT * FROM [staff] WHERE (barcode='" & sBarcode & "');"
    '    If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
    '                           (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
    '        '--have a row..-
    '        colRecord = colResult.Item(1)
    '        '=msSaleStaffbarcode = sBarcode
    '        mbLookupStaff = True
    '    Else '--not found..-
    '        '= EventArgs.Cancel = True
    '        MsgBox("No Staff found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
    '    End If  '-get--

    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

    'End Function '--staff lookup..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '- Check if TableExists..
    '-  IN CURRENT DATABASE --

    'Private Function mbDoesTableExist(ByRef cnnSql As OleDbConnection, _
    '                                    ByVal sTableName As String) As Boolean
    '    Dim rdr1 As OleDbDataReader
    '    mbDoesTableExist = False
    '    '--  IF table does not exist !!--
    '    '-- The following example checks for the existence of a specified table
    '    '--     by verifying that the table has an object ID. 
    '    Dim sSql As String = "SELECT * FROM sys.objects " & _
    '                           "WHERE object_id = OBJECT_ID(N'[dbo].[" & sTableName & "]') AND type in (N'U')"
    '    If gbGetReader(mCnnSql, rdr1, sSql) Then  '--check if row exists..-
    '        If rdr1.HasRows Then '-table exists..-
    '            mbDoesTableExist = True
    '        Else  '--doesn't exist.. must create.
    '            mbDoesTableExist = False
    '        End If
    '        rdr1.Close()
    '    Else  '-get rdr error
    '        '--  GET error text !!--
    '        MsgBox("Error in reading sys.objects table.." & vbCrLf & _
    '                              gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
    '    End If  '--get--

    'End Function '-- exists table.-
    '= = = = = = = = = = = == = = =  =
    '-===FF->


    '-- SQL Connect--
    Private Function mbSqlConnect(ByVal sServer As String) As Boolean
        Dim sConnect, sMsg As String

        mbSqlConnect = False
        sConnect = "Provider=SQLOLEDB; Server=" & msServer & _
                   "; Trusted_Connection=true; Integrated Security=SSPI; ConnectionTimeout=10; "
        Call mWaitFormOn("Connecting to Sql Server: " & vbCrLf & msServer & "..")
        If gbConnectSql(mCnnSql, sConnect) Then
            Call mWaitFormOff()
            '==bConnected = True '== bLoggedOn = True
            msSqlConnectString = sConnect
            mbSqlConnect = True
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call mWaitFormOff()
            sMsg = "Login to Sql-Server '" & msServer & "' has failed." & vbCrLf & _
                  "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf & _
             "Check that your Windows Logon has been added to SQL-Server logins.."

            Call gbLogMsg(gsRuntimeLogPath, "=== ERROR in SQL Connect in JobMatix JMxJT420ex- [Sub] Main Function.." & _
                          vbCrLf & vbCrLf & sMsg)

            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Function
        End If '--connected-
    End Function '--mbSqlConnect-
    '= = = = = = = = = = = = = = =

    '- Background worker thread to Sql server DB schema..--
    '--  Started from Form Load event routine..
    Private Sub backgroundWorkerGetSchema_DoWork(ByVal sender As Object, _
                                              ByVal e As DoWorkEventArgs)

        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        mbSqlServerGetSchemaOK = gbGetSqlSchemaInfo(mCnnSql, msSqlDbName, mColSqlDBInfo, msBuildSchemaLog)

    End Sub  '- backgroundWorkerGetSchema_DoWork--
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  "sub" Main  --
    '--  "sub" Main  --
    '--  "sub" Main  --

    Public Function Main(ByVal cmdArgs() As String) As Integer

        Dim s1, s2, sList, asTokens() As String
        Dim sCmdLine, sErrors As String
        Dim ix, returnValue, intTop, intLeft As Integer
        Dim v1 As Object
        Dim sSchemaInfo As String
        Dim bAdminTest As Boolean = False

        '- See if there are any arguments.
        sList = ""
        intTop = -1 : intLeft = -1
        If cmdArgs.Length > 0 Then
            For argNum As Integer = 0 To UBound(cmdArgs, 1)
                '- Insert code to examine cmdArgs(argNum) and take
                '- appropriate action based on its value.
                sList &= vbCrLf & cmdArgs(argNum)
            Next argNum
        End If
        '= MsgBox("The JobMatix RAs31 Main procedure is starting up.." & vbCrLf & "Cmd args are: " & vbCrLf & sList)
        msVersionPos = "JMxJT420ex Main v:" & CStr(My.Application.Info.Version.Major) & "." & _
                                         My.Application.Info.Version.Minor & ", Build: " & _
                                             My.Application.Info.Version.Build & "." & _
                                                 My.Application.Info.Version.Revision
        '-- setup stuff..--
        '=3501.0616=
        Dim sAppPath As String = gsAppPath()
        msAppPath = sAppPath
        '- SEE BLELOW- 
        '- msJobmatixAppName = gsAppName()

        '-  new log each month..-
        '= s1 = VB.Format(CDate(DateTime.Today), "yyyy-MM-dd")

        '-- NOW are public FUNCTIONS
        '== gsErrorLogPath = gsJobMatixLocalDataDir("JobMatix35") & "\JmxPOS350-Runtime-" & VB.Left(s1, 7) & ".log"
        '== gsRuntimeLogPath = gsErrorLogPath  '--gsAppPath & "JTv3_Runtime.log"

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

        msStaffBarcode = ""
        '--  cmd line must have STAFF barcode..--
        'If gbGetCmd(sCmdLine, "StaffBarcode", s1) Then
        '    If (s1 <> "") Then
        '        msStaffBarcode = s1
        '        msLog = msLog & "Input StaffBarcode: " & s1 & vbCrLf
        '    End If
        'End If
        'If (msStaffBarcode = "") Then
        '    MsgBox("Staff barcode must be supplied..", MsgBoxStyle.Exclamation)
        '    End '=Unload Me
        '    Exit Function
        'End If
        If gbGetCmd(sCmdLine, "AdminTest", s1) Then
            '=If (UCase(s1) = "NO") Then
            bAdminTest = True
            '= End If
        End If

        '- check parms..
        If (msServer = "") Then
            MsgBox("No server name supplied for JobTracking app..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (msInputDBNameJobs = "") Then
            MsgBox("No Database name supplied for JobTracking app..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        If (msJobmatixAppName = "") Then
            MsgBox("No JobMatix App name supplied for JobTracking app..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        '=3501.0616-- MUST DO THIS FIRST !!-
        Call gSubSetAppName(msJobmatixAppName)

        Call gbLogMsg(gsRuntimeLogPath, " == " & msVersionPos & " [Sub] Main Function is starting..")


        '-- Connect to Sql Server..
        mCnnSql = New OleDbConnection  '= ADODB.Connection
        '== mCnnSql.ConnectionTimeout = 10 '--seconds..-
        While Not mbConnected
            mbConnected = mbSqlConnect(msServer)
            If Not mbConnected Then
                If Not MsgBox("No Sql connection.  Retry ? ", _
                            MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    Return -1  '=Exit Function '--ends-
                End If
            End If '--connected-
            '-- go around again..  UNTIL cancelled or ok..
        End While  '-Not bConnected-

        '-- test-
        '== MsgBox("Connected ok to :" & msServer, MsgBoxStyle.Information)

        '-- get server/instance split.

        asTokens = Split(msServer, "\")
        If (asTokens.Length > 0) Then
            msSqlServerComputer = asTokens(0)
        End If
        If (asTokens.Length > 1) Then
            msSqlServerInstance = asTokens(1)
        End If

        Call gbSetupSqlVersion(mCnnSql)
        msSqlVersion = gsGetSqlVersion()

        '--  check if we are sqlAdmin privileged..--
        mbIsSqlAdmin = gbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")
        '-- save as global--
        Call gbSetIsSqlAdmin(mbIsSqlAdmin)
        Call gbLogMsg(gsRuntimeLogPath, "JobTracking Sub Main- Logged in OK to SQL Server Instance:  " & msServer & vbCrLf & _
                                          "SQL-Server Version: " & msSqlVersion & vbCrLf)
        msCurrentUserName = gsGetCurrentUser()

        '-- USE --
        msSqlDbName = msInputDBNameJobs

        '-- USE Jobs DB and check result..-
        If Not gbSetCurrentDatabase(mCnnSql, msSqlDbName) Then
            '== If (LCase(sCurrentDB) <> LCase(sDBnameJobs)) Then
            MsgBox("= Failed to set current Database for: " & msSqlDbName & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsRuntimeLogPath, "JobTracking Sub Main- " & vbCrLf & _
                                            "= Failed to set current Database for: " & msSqlDbName & vbCrLf & _
                                               "Jobmatix is closing down..")
            '== Me.Close()
            Exit Function '-- False..-
        End If
        '--  USE must have worked..-
        '-- SAVE id for who_using.. mIntJobMatixDBid --
        If gbGetSelectValueEx(mCnnSql, "SELECT DB_ID() AS current_db_id;", v1) Then
            If Not (v1 Is Nothing) Then
                mIntJobMatixDBid = CLng(v1)
            End If
        End If  '--get-
        Call gSetJobMatixDBid(mIntJobMatixDBid)
        Call gbLogMsg(gsRuntimeLogPath, "JobTracking Sub Main- = OK.. current Database is set to: [" & msSqlDbName & "]..")

        '-- Connect and DB choice done..
        '-- Connect and DB choice done..

        '--  Get DB schema info..-- 
        '-- Load DB-Info for selected DB..--
        Call mWaitFormOn("-- Getting database schema for DB: " & vbCrLf & "   [" & msSqlDbName & "]..")
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '-- BG Worker to get schema.--
        mBackgroundWorkerGetSchema = New BackgroundWorker

        AddHandler mBackgroundWorkerGetSchema.DoWork, AddressOf backgroundWorkerGetSchema_DoWork

        mbSqlServerGetSchemaOK = False
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
            msBuildSchemaLog &= " *** ERROR- FAILED to build  SQL catalogue for DB: " & msSqlDbName & " ==" & vbCrLf
            MsgBox(" *** ERROR- FAILED to build  SQL catalogue for DB: " & msSqlDbName & " ==" & vbCrLf)
            '= Me.Close()
            Exit Function '-- False..-
        End If '--ok--

        sSchemaInfo = gsShowSqlSchema(msSqlDbName, mColSqlDBInfo) '--display all schema info-- 
        sSchemaInfo &= vbCrLf & "= = = Log Saved: " & _
                                            VB.Format(Now, "dd-MMM-yy hh:mm") & " = = =" & vbCrLf
        Call glSaveTextFile(gsJobMatixLocalDataDir("JobMatix42") & "\" & msSqlDbName & "_JT420Schema.txt", _
                                   msBuildSchemaLog & vbCrLf & "= = = = =" & vbCrLf & vbCrLf & sSchemaInfo)
        '-- Connect and DB Startup COMPLETED..

        '-- ok to go.-
        '-- load and call the POS Main Form..

        '== MsgBox("JobMatixRAs31 is showing RAs Main form.", MsgBoxStyle.Exclamation)
        Call gbLogMsg(gsRuntimeLogPath, "JMxJT420ex is showing JobTracking Main form..  ")

        Dim frmJobMatixMain1 As frmJobMatix42Main
        Try
            '--  load POS Main form and show it..
            frmJobMatixMain1 = New JobMatix3.frmJobMatix42Main(mCnnSql, _
                                                               msServer, _
                                                               msSqlServerComputer, _
                                                               msSqlServerInstance, _
                                                               msSqlConnectString, msSqlDbName, _
                                                               msJobmatixAppName)
            Try
                frmJobMatixMain1.ShowDialog()
            Catch ex As Exception
                MsgBox("JMxJT420ex- Error in showing JobTracking Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)

            End Try
        Catch ex As Exception
            MsgBox("JMxJT420ex- Error in loading JobTracking Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '--show-

        '- Insert call to appropriate starting place in your code.
        '- On return, assign appropriate value to returnValue.
        '- 0 usually means successful completion.
        '== MsgBox("The RAs31 application is terminating with error level " & CStr(returnValue) & ".")
        Call gbLogMsg(gsRuntimeLogPath, "JobMatixJT420ex is exiting with error level " _
                                             & CStr(returnValue) & "." & vbCrLf & _
                                             "= = = = = = = = = = = = = = = =" & vbCrLf & _
                                             "= = = = = = = = = = = = = = = =" & vbCrLf & _
                                             "= = = = = = = = = = = = = = = =" & vbCrLf)
        Return returnValue

    End Function  '--main--
    '= = = = = = = = = = = = = = = = = = =


End Module '--mod JT420Main-
'= = = = = = = = = = = = = = = == = = =

'== the end =
