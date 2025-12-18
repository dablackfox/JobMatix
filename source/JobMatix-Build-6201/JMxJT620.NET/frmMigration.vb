Option Strict Off
Option Explicit On
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports VB = Microsoft.VisualBasic

Public Class frmMigration

    '- Created 24-Feb-2015- 
    '-- Migrate JobMatix (RM) DB to JobMatix POS--
    '--  and Import current MYOB Staff, Stock, Suppliers and Customers-
    '-- (Including KEEPING Identities) --
    '-- So jobs data will be still be compatible with POS tables.

    '==
    '==  grh. JobMatix 3.1.3107.0801 ---  01-Aug-2015 ===
    '==   >>  Job Settings and log files now in CommonApplicationData--
    '==   >>   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)   
    '==   >>   gbLogMsg and glSaveTextFile wasn't/weren't reporting open error..
    '== 
    '==  grh. JobMatix 3.1.3107.803-  03-Aug-2015 ===
    '==   >>  Now Using .net 4.5.2
    '==
    '==  grh 3311.225= 25Feb2016=  
    '==      >> Now Uses clSystemInfo..
    '==
    '==
    '==  grh 3327.0117= 17Jan2017=  
    '==      >> Minor fixes to labesls, msgs....
    '==
    '==   v3.4.3401.0424 -- 24Apr2017= Extra fix-
    '==      -- frmMigration..  New db name (x_jmpos) set up is now fixed (user can't change)..     
    '==      -- Also- Delete DB X_jmpos if exists and user approves..
    '==
    '==
    '==    >> 3431.0712- 12-July-2018=
    '==       --  frmMigration.. After Restore of JobTracking DB, and before ALTER columns,
    '==                     Updgrade DB Compatibility_Level to 90 if below..
    '==
    '==
    '== -- Updated 3501.0814  14August2018=  
    '==     -- Fix clsJMxPOS31 to separate out function CreatePosTables into clsJMxCreatePOS...
    '== 
    '==  NEW BUILD-    4219 VERSION
    '==    Updated- 4219.1128 21-Nov-2019= 
    '==      --  Expand Explain text of Form Header..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Const k_VALIDNAMECHARS As String = " _0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    Private msValidChars As String

    Private mFrmParent As Form
    Private msSqlServerComputer As String = ""

    Private msSqlServer As String = ""
    Private msSqlVersion As String = ""
    Private mCnnSql As OleDbConnection
    Private mColSqlDBInfo As Collection

    Private msComputerName As String '--local machine--
    Private mbIsRunningOnServer As Boolean = False

    Private msJobMatixVersion As String
    Private mbIsAdmin As Boolean = False
    Private mColDBlist As Collection

    Private mbActivated As Boolean = False
    Private mbCancelled As Boolean = False

    Private msResult As String = ""

    Private msAppPath As String = ""
    Private msCreateLogPath As String = ""

    Private msMasterPath As String = ""
    Private msMSSQLServerDataDir As String = ""

    '--restore-
    Private msBackupFileTitle As String = ""
    Private msBackupFullPath As String = ""
    Private msBackupFullPath2 As String = ""

    Private msSqlBackupDBName As String = ""
    Private mDateBackupFinished As Date

    Private msBackupName As String
    Private msBackupServerName As String

    Private msShortName As String = ""
    Private msNewSqlDBName As String = ""

    Private msBackupFileServerLocalDir As String = ""
    Private msBackupFileServerNetworkDir As String = ""

    Private msBackupFileOurNetworkPath As String = ""

    '==3311.225= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    '==3311.225= Private mColSystemInfo As Collection      '--  holds COLLECTION system Info Table values at startup..
    '==3311.225= 
    Private mSysInfo1 As clsSystemInfo

    Private msBusinessShortName As String = ""
    '= = = = = = = = = =  =
    '-===FF->

    '--results-

    ReadOnly Property result() As String
        Get
            result = msResult
        End Get
    End Property '--path..-
    '= = = = = = = = = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = = = = = = =  =
    '-===FF->


    '--Constructor-
    '--Constructor-

    Public Sub New(ByRef frmParent As Form, _
                       ByVal bIsAdmin As Boolean, _
                       ByVal sSqlServerComputer As String, _
                        ByVal sSqlServer As String, _
                         ByRef cnnSql As OleDbConnection, _
                         ByVal sSqlVersion As String, _
                          ByVal sJobmatixVersion As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save inputs-
        mFrmParent = frmParent
        mbIsAdmin = bIsAdmin

        msSqlServerComputer = sSqlServerComputer
        msSqlServer = sSqlServer
        msSqlVersion = sSqlVersion
        mCnnSql = cnnSql

        msJobMatixVersion = sJobmatixVersion
        '= mColDBlist = colDBlist

    End Sub  '--new-
    '= = = = = = = = = == = 
    '-===FF->

    '-mbTableExists-

    Private Function mbTableExists(ByRef cnnSql As OleDbConnection, _
                                    ByVal strTableName As String) As Boolean
        Dim sSql, sErrorMsg As String
        Dim rdr1 As OleDbDataReader

        mbTableExists = False
        sSql = "SELECT * FROM sys.objects " & _
           "WHERE object_id = OBJECT_ID(N'[dbo].[" & strTableName & "]') AND type in (N'U')"
        sErrorMsg = ""
        If gbGetReader(mCnnSql, rdr1, sSql) Then  '--check if row exists..-
            If rdr1.HasRows Then '-table exists..-
                mbTableExists = True
            End If
            rdr1.Close()
        Else  '-get rdr error
            '--  GET error text !!--
            sErrorMsg = gsGetLastSqlErrorMessage()
        End If  '--get rdr--

        If sErrorMsg <> "" Then
            MsgBox("Error in reading sys.objects table.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If
    End Function  '-mbTableExists-
    '= = = = = = = = = = = = = ==== =
    '-===FF->

    '-- Add msg to txtReport.text--

    Private Function mbReport(ByVal sMsg As String) As Boolean

        txtReport.AppendText(sMsg & vbCrLf)
        txtReport.Focus()
        txtReport.SelectionStart = txtReport.TextLength
        txtReport.SelectionLength = 0
        '== txtReport.Select()

    End Function  '-report-
    '= = = = = = = = = = = = = = =


    Private Sub frmMigration_Load(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles MyBase.Load


        btnContinue.Enabled = False
        labGetName.Enabled = False
        Call CenterForm(Me)

        msComputerName = My.Computer.Name
        msValidChars = k_VALIDNAMECHARS
        txtReport.Text = ""
        labBackupSrc.Text = ""

        '-- migration Log..-
        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        '= msCreateLogPath = gsAppPath & "JTv31POS-Migration.log"
        msCreateLogPath = gsJobMatixLocalDataDir() & "\JobMatixPOS31-Migration.log"

        '= 3107.803= Kill(msCreateLogPath)
        If System.IO.File.Exists(msCreateLogPath) = True Then
            System.IO.File.Delete(msCreateLogPath)
            MsgBox("The old log: " & msCreateLogPath & vbCrLf & _
                             "    was deleted ok..", MsgBoxStyle.Information)
        End If
        '== mSysInfo1 = New clsSystemInfo(mCnnSql)
        Me.Text = "JobMatixPOS Migration- " & msJobMatixVersion

    End Sub  '--load-
    '= = = = = = = = = = =
    '-===FF->

    Private Sub frmMigration_Activated(ByVal sender As System.Object, _
                          ByVal e As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '--activated-
    '= = = = = = = = = = = =

    '-Shown-

    Private Sub frmMigration_Shown(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim rdr1 As OleDbDataReader
        Dim sMsg, sErrors As String
        Dim s1, s2 As String
        Dim intResult, intCount, ix As Integer

        '= If mbActivated Then Exit Sub
        '= mbActivated = True

        '-- get Filepaths for SQL Server data..-
        msMasterPath = ""
        Call gbLogMsg(gsRuntimeLogPath, "= = = =  = = = = =" & vbCrLf & "== MIGRATION is starting.." & vbCrLf)
        Call gbLogMsg(msCreateLogPath, "= = = =  = = = = =" & vbCrLf & "== MIGRATION is starting.." & vbCrLf)
        txtReport.Text = "MIGRATION is starting. Server is: " & msSqlServer & vbCrLf

        Try  '--main restore try..--
            intResult = glExecSP(mCnnSql, "sp_helpdb", "@dbname='master'", sErrors, rdr1)
            If intResult <> 0 Then
                MsgBox(sErrors, MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
            Else
                intCount = 0 '--count r'sets..-
                '--check dataReader (recordset)--
                If (rdr1 Is Nothing) Then
                    MsgBox("No data returned from Sql master Path Query..", MsgBoxStyle.Exclamation)
                    Me.Close()
                    Exit Sub
                End If
                labStatus.Text = "Checking Sql master Path Query result.." & vbCrLf
                While Not (rdr1 Is Nothing)
                    intCount += 1
                    If (intCount = 2) Then '--we want the 2nd rset..--
                        If rdr1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                            While (rdr1.Read) AndAlso (msMasterPath = "")
                                '--MsgBox "checking file path for master..-
                                Try
                                    '==s1 = Trim(rdr1.GetString("NAME")) '-- Rev-3101.919-- FOR ado.net..
                                    s1 = Trim(rdr1.Item("NAME")) '-- Rev-3103.225- Fixed..
                                    If (LCase(s1) = "master") Then
                                        Try
                                            '== s2 = Trim(rdr1.GetString("FILENAME")) '---- ditto, in case..-
                                            s2 = Trim(rdr1.Item("FILENAME")) '-- Rev-3103.225- Fixed..
                                            msMasterPath = s2
                                        Catch ex2 As Exception
                                            MsgBox("Error in reading master db filename." & vbCrLf & _
                                                                        ex2.Message, MsgBoxStyle.Exclamation)
                                            Me.Close()
                                            Exit Sub
                                        End Try
                                    End If '--master..--
                                Catch ex As Exception
                                    MsgBox("Error in reading master db-name." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                                    Me.Close()
                                    Exit Sub
                                End Try  '--NAME-
                            End While  '--read-
                        End If '- has rows--empty.-
                    End If '--2nd rset..-
                    If Not rdr1.NextResult Then Exit While '-done-
                End While
            End If '--ok-
            If (msMasterPath <> "") Then '--extract directory path..-
                ix = InStr(LCase(msMasterPath), "data\master")
                If (ix > 0) Then
                    msMSSQLServerDataDir = VB.Left(msMasterPath, ix + 4)
                End If
            End If
            '== MsgBox("TEST-INFO-" & vbCrLf & _
            '==         "SQL Server Master DB File Path is : " & vbCrLf & msMasterPath & vbCrLf & vbCrLf & _
            '==         "msSqlDataPath Is " & msMSSQLServerDataDir, vbInformation)
            Call mbReport("TEST-INFO-" & vbCrLf & _
                    "SQL Server Master DB File Path is : " & vbCrLf & msMasterPath & vbCrLf & vbCrLf & _
                    "msSqlDataPath Is " & msMSSQLServerDataDir)
            '----END of:  Locate DB files for Mster DB..
            '----END of:  Locate DB files for Mster DB..

            '-- IF NOT running on Server..
            '--  Check access to Users\public folder on Server..
            '-- We will need to copy the BAK file from the local path to the server public path -
            '--     Where the sql server can see it to RESTORE from..

            mbIsRunningOnServer = (LCase(msSqlServerComputer) = LCase(msComputerName))
            If Not mbIsRunningOnServer Then  '-running on client-
                '- check we can access Users\Public on server..
                s1 = "\\" & msSqlServerComputer & "\users\public"
                If My.Computer.FileSystem.DirectoryExists(s1) Then
                    msBackupFileServerNetworkDir = s1 & "\"   '-for us to copy to..
                    msBackupFileServerLocalDir = "c:\users\public\"  '-for sql server to see..
                Else
                    MsgBox("Unable to access the server path: " & s1 & vbCrLf & _
                             "You should run this Migration application on the Sql Server computer.", MsgBoxStyle.Exclamation)
                    Me.Close()
                    Exit Sub
                End If  '-exists
            Else  '-- we are on server-
                '-- use BAK path from openDlg..
            End If '-on server-

            Exit Sub

        Catch ex As Exception
            sMsg = "ERROR in Migration Activate Sub.." & vbCrLf & ex.Message & vbCrLf & _
                      "RESTORE closing.." & vbCrLf
            Call gbLogMsg(msCreateLogPath, sMsg)
            MsgBox(sMsg, MsgBoxStyle.Critical)
            Me.Close()
            Exit Sub
        End Try  '-- Outer main Try--

    End Sub '-SHOWN--activated --
    '= = = = = = = = = == == 
    '-===FF->

    '- get File to Restore..

    Private Sub btnBrowse_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim sStartPath, sSql As String
        Dim sName, s1 As String
        Dim MyResult As DialogResult
        Dim dataTable1 As DataTable

        msBackupFileTitle = "" '--"JobsDatabaseBackup_*"

        '--  get actual (Backup File) "RESTORE FROM" location from operator..--
        OpenDlg1.Title = "Jobs Database RESTORE- Select Backup file to reload onto Jobs DB.."
        '== dlg1.CancelError = True
        OpenDlg1.Filter = "SQL DB Backup Files (*.bak)|*.bak|All Files (*.*)|*.*"
        '== dlg1.flags = &H200000 Or &H4 Or &H800 Or &H1000
        OpenDlg1.InitialDirectory = sStartPath '--msAppPath
        OpenDlg1.FileName = sStartPath & msBackupFileTitle
        '== On Error Resume Next
        MyResult = OpenDlg1.ShowDialog
        '--check for cancel--
        '== If Err().Number <> 0 Then '--Cancelled==
        If MyResult <> System.Windows.Forms.DialogResult.OK Then     '= If Err().Number <> 0 Then '--Cancelled==
            '--set rs = nothing
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '==mlStaffTimeout = 0  '--start timing out..--
            Exit Sub
        End If
        '== On Error GoTo 0
        msBackupFullPath = OpenDlg1.FileName
        '== btnContinue.Enabled = True
        labBackupSrc.Text = msBackupFullPath
        Application.DoEvents()
        '== sTitle = dlg1.FileTitle '--bare filename..-
        msBackupFileTitle = Path.GetFileName(msBackupFullPath)

        '-- Check backup File Header and info..
        '-- FIRST.. If not running on server, then copy BAK file to server,
        '--   where Sql Server can see it..
        If Not mbIsRunningOnServer Then  '-running on client-
            '-- copy BAK to server..
            Try
                My.Computer.FileSystem.CopyFile(msBackupFullPath, _
                                                 msBackupFileServerNetworkDir & msBackupFileTitle, True)  '--overwrite.-
                msBackupFullPath2 = msBackupFileServerLocalDir & msBackupFileTitle
                s1 = msBackupFileServerNetworkDir & msBackupFileTitle
                msBackupFileOurNetworkPath = msBackupFileServerNetworkDir & msBackupFileTitle '-What we can see..
            Catch ex As Exception
                MsgBox("Unable to copy file to the server path: " & s1 & vbCrLf & ex.Message & vbCrLf & vbCrLf & _
                          "Try running this Migration application on the Sql Server computer.", MsgBoxStyle.Exclamation)
                '= Me.Close()
                Exit Sub
            End Try
        Else 'we are on server..  use orig. dlg path.
            msBackupFullPath2 = msBackupFullPath
            s1 = "[Local] " & msBackupFullPath
            msBackupFileOurNetworkPath = msBackupFullPath
        End If '-on server-
        Call mbReport(vbCrLf & "New DB files will now be RESTORED from: " & vbCrLf & s1 & vbCrLf)
        Call gbLogMsg(msCreateLogPath, vbCrLf & "New DB files will now be RESTORED from: " & vbCrLf & s1 & vbCrLf)

        '--  Get Header from Backup File..-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        sSql = "RESTORE HEADERONLY " & vbCrLf
        sSql = sSql & "FROM DISK = '" & msBackupFullPath2 & "'" & vbCrLf

        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Failed to get RESTORE HEADERONLY recordset.." & vbCrLf & _
                     "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else '--get first selected value.-....-
            If Not (dataTable1 Is Nothing) Then
                If (dataTable1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '= rs1.MoveFirst()
                    '-- capture BAK header flds..-
                    Dim dataRow1 As DataRow = dataTable1.Rows(0)  '--first row-
                    For Each column1 As DataColumn In dataTable1.Columns '= fld1 In rs1.Fields
                        sName = LCase(column1.ColumnName)
                        If Not IsDBNull(dataRow1.Item(sName)) Then
                            Select Case sName
                                Case "databasename" : msSqlBackupDBName = dataRow1.Item(sName)
                                Case "backupfinishdate" : mDateBackupFinished = CDate(dataRow1.Item(sName))
                                Case "backupname" : msBackupName = dataRow1.Item(sName)
                                Case "servername" : msBackupServerName = dataRow1.Item(sName)
                            End Select
                        End If '-null-
                    Next column1 '= fld1 '--fld1-
                End If '--bof-
                '== rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '== rs1 = Nothing
        If (msSqlBackupDBName = "") Then '-- no valid db name in Backup Header....--
            MsgBox("No valid db name in Backup Header..", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf (VB.Right(LCase(msSqlBackupDBName), 11) = "jobtracking") Then  '-ok-
            s1 = VB.Left(msSqlBackupDBName, (msSqlBackupDBName.Length - 11))  '-drop jt suffix.-
            If s1 = "" Then
                s1 = "Precise Pcs"
            ElseIf VB.Right(s1, 1) = "_" Then
                s1 = VB.Left(s1, (s1.Length - 1))  '-drop underscore suffix.-
            End If
            txtNewName.Text = s1
            msShortName = s1
            msNewSqlDBName = s1 & "_jmpos"
        Else  '--not job tracking..-
            MsgBox("The DB Name : '" & msSqlBackupDBName & "' in Backup Header " & _
                                "is not a valid JobTracking db name ..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        labGetName.Enabled = True
        txtNewName.Enabled = True
        btnContinue.Enabled = True

    End Sub  '-browse-
    '= = = = = = = = = = = = =
    '-===FF->

    Private Sub txtNewName_TextChanged(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles txtNewName.TextChanged

        btnContinue.Enabled = True
        txtNewName.Select()

    End Sub  '-new name-
    '= = = = = = = = = =  =

    '-- Validate Name..-
    Private Sub txtNewName_Validating(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                     Handles txtNewName.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim vName As Object
        Dim s1 As String
        Dim ok As Boolean
        Dim ix As Integer

        msShortName = Trim(txtNewName.Text)
        ok = True

        If (msShortName <> "") Then
            '-- check if alphanumeric.--)
            s1 = UCase(msShortName)
            For ix = 1 To Len(msShortName)
                If Not (InStr(msValidChars, Mid(s1, ix, 1)) > 0) Then
                    ok = False
                    Exit For
                End If
            Next ix
            If ok Then
                '-- replace all multiple spaces by single spaces..-
                While (InStr(msShortName, "  ") > 0)
                    msShortName = Replace(msShortName, "  ", " ") '--replace dbl space with single..--
                End While
                msShortName = Replace(msShortName, " ", "_") '--replace remaining spaces with u-score..--
                '== txtNewName.Text = msShortName & "_" & "jmpos"
            End If '--ok..-
        Else
            ok = False
            '== MsgBox("Must have Business Short Name..", MsgBoxStyle.Exclamation)
        End If '--null-
        If Not ok Then
            txtNewName.Text = ""
            keepfocus = True
            MsgBox("New DB Name is missing, or has invalid characters..", MsgBoxStyle.Exclamation)
        Else '--ok..-
            '==FrameLogin.Enabled = True
            labStatus.Text = ""
            msNewSqlDBName = msShortName & "_jmpos"
            btnContinue.Enabled = True
        End If
EventExitSub:
        eventArgs.Cancel = keepfocus
    End Sub '-- Short Name--
    '= = = = = = = = = = = = = = = =
    '-===FF->


    '- can Restore-

    Private Sub btnContinue_Click(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim sTargetFileFullPath, s1, s2, s3 As String
        Dim sNewTitle As String
        Dim sSql, sMoveSql, sFileMsg, sWithSql, sErrors As String
        Dim colFiles, colRecord As Collection
        Dim iPos, L1, intErrors As Integer
        Dim ok, bOk As Boolean
        Dim sPostCode, sABN, sErrorMsg As String

        btnBrowse.Enabled = False
        btnContinue.Enabled = False
        txtNewName.Enabled = True

        '--Check that BAK file is still there..
        '--  msBackupFullPath2 =
        If Not My.Computer.FileSystem.FileExists(msBackupFileOurNetworkPath) Then
            s1 = "The BAK source file '" & msBackupFileOurNetworkPath & "' has disappeared ! "
            Call gbLogMsg(msCreateLogPath, s1 & vbCrLf)
            MsgBox("We have a problem! " & vbCrLf & s1, MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '-- Check if Target DB exists..
        If gbExistsDatabase(mCnnSql, msNewSqlDBName) Then
            '--  only if DB exists.--
            Call gbLogMsg(msCreateLogPath, "MIGRATION RESTORE- confirming to overwrite DB " & msNewSqlDBName & ".." & vbCrLf)
            If Not MsgBox("CAUTION! " & vbCrLf & _
                           "You are about to OVERWRITE the migrated Database: '" & msNewSqlDBName & "'.." & vbCrLf & _
                   " FROM the backup file:" & vbCrLf & msBackupFullPath & vbCrLf & vbCrLf & _
                   "Is it ok to continue ??", _
                      MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                btnBrowse.Enabled = True
                Exit Sub
            Else  '-ok do it-
                MsgBox("NB: Make all other users are logged out of the database before continuing.", MsgBoxStyle.Exclamation)
                '=-3401.424=
                '-- DROP DB if exists..-
                Call gbLogMsg(msCreateLogPath, "MIGRATION RESTORE- Deleting Old DB " & msNewSqlDBName & ".." & vbCrLf)
                s1 = "DROP DATABASE " & msNewSqlDBName & vbCrLf
                bOk = gbExecuteCmd(mCnnSql, s1, L1, sErrorMsg)
                If bOk Then
                    Call gbLogMsg(msCreateLogPath, "-- Existing Jobs DB'" & msNewSqlDBName & "' DROPPED..")
                    MsgBox("-- Existing Jobs DB'" & msNewSqlDBName & "' DROPPED..", MsgBoxStyle.Information)
                Else
                    Call gbLogMsg(msCreateLogPath, "-- Failed to DROP Jobs DB'" & msNewSqlDBName & vbCrLf & sErrorMsg)
                    MsgBox("-- Failed to DROP Jobs DB'" & msNewSqlDBName & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                End If
            End If  '-msgbox-
        End If  '-exists.-

        '-- ok  Start Restore process..

        '- 1st.. Get File List from BAK file..

        '---  get Data file list from backup header..-
        '-- Replace File titles (x.mdf and x.ldf) with NEW DB name.
        Call gbLogMsg(msCreateLogPath, "MIGRATION- Getting DB FileList from: " & msBackupFileOurNetworkPath & vbCrLf)

        sSql = "RESTORE FILELISTONLY  " & vbCrLf
        sSql = sSql & "FROM DISK = '" & msBackupFullPath2 & "'" & vbCrLf
        If Not gbGetRecordCollection(mCnnSql, sSql, colFiles) Then
            MsgBox("This migration is not working..", MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Sub
        End If
        sMoveSql = "" '--to set file target directories..-
        sFileMsg = ""
        sWithSql = ""
        '-- Filelist recordset had a row for every file in backup set.-
        sTargetFileFullPath = "" '--remember target mdf file..-
        If (colFiles.Count() > 0) Then
            For Each colRecord In colFiles
                s3 = ""
                s1 = colRecord.Item("LogicalName")("value")
                s2 = colRecord.Item("PhysicalName")("value") '--Original full path..-
                sFileMsg = sFileMsg & "LogicalFile: '" & s1 & "' is from: " & s2 & vbCrLf '--for test..-
                iPos = InStrRev(s2, "\") '--we want file-title..--
                If (iPos <= 0) Then iPos = InStrRev(s2, ":") '--in case Drive only..--..--
                If (iPos > 0) Then s3 = Mid(s2, iPos + 1) '--  get file name from path..-
                '- now make new file titles for New DB Name..-
                sNewTitle = ""
                If (LCase(VB.Right(s2, 4)) = ".mdf") Then
                    sNewTitle = msNewSqlDBName & ".mdf"
                ElseIf (LCase(VB.Right(s2, 4)) = ".ldf") Then
                    sNewTitle = msNewSqlDBName & "_log.ldf"
                End If
                If (sNewTitle <> "") Then  'have name-
                    If sMoveSql <> "" Then sMoveSql = sMoveSql & ","
                    sMoveSql = sMoveSql & " MOVE '" & s1 & "' TO '" & msMSSQLServerDataDir & sNewTitle & "'"
                End If
                '=Migrate= If sMoveSql <> "" Then sMoveSql = sMoveSql & ","
                '=Migrate= sMoveSql = sMoveSql & " MOVE '" & s1 & "' TO '" & msMSSQLServerDataDir & s3 & "'"
                '-- save target file name for replace-test..-
                '== If (VB.Right(LCase(s2), 4) = ".mdf") And (sTargetFileFullPath = "") Then
                '== sTargetFileFullPath = msMSSQLServerDataDir & s3
                '== End If
            Next colRecord '--record.-
        End If '--count..-
        '- ok MOVE has been set to use name of new DB..
        '== MsgBox("Backup Set has original files: " & vbCrLf & sFileMsg & vbCrLf & vbCrLf & _
        '==          "New DB will have files MOVEd to new names: " & vbCrLf & sMoveSql, vbInformation)
        Call mbReport("Backup Set has original files: " & vbCrLf & sFileMsg & vbCrLf & vbCrLf & _
                  "New DB will have files MOVEd to new names: " & vbCrLf & sMoveSql)

        labStatus.Text = "Releasing DB from our connections.. "
        System.Windows.Forms.Application.DoEvents()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        sSql = "USE master  " & vbCrLf
        If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
            MsgBox(vbCrLf & "Restore.. Failed in USE for DATABASE 'master'  = =" & vbCrLf & _
                             "Error text: " & sErrors, MsgBoxStyle.Exclamation)
        End If

        sSql = "RESTORE DATABASE " & msNewSqlDBName & vbCrLf
        sSql = sSql & "FROM DISK = '" & msBackupFullPath2 & "'" & vbCrLf
        '--sSql = sSql + "WITH NAME = 'Full Backup of JobTracking DB on " + _
        ''--                  Format(Date, "ddmmmyyyy") + "-" + Format(Time, "hh:mm") + "'" + vbCrLf
        If sWithSql <> "" Then
            sSql = sSql & sWithSql
        End If
        If sMoveSql <> "" Then '--add move clauses..-
            If sWithSql <> "" Then
                sSql = sSql & "," '--MOVE is part of WTH list..-
            Else
                sSql = sSql & " WITH " & vbCrLf
            End If
            sSql = sSql & sMoveSql
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'If Not MsgBox("ONE LAST CHANCE! " & vbCrLf & " To NOT execute the  RESTORE SQL command:" & vbCrLf & vbCrLf & _
        '                sSql & vbCrLf & vbCrLf & "OK to Continue with RESTORE??", _
        '                    MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    Exit Sub
        'End If
        '== frmSplash.Labstatus.Text = "Attempting RESTORE of Jobs database from file: " & sFullPath
        Call gbLogMsg(msCreateLogPath, "= MIGRATION- Starting RESTORE- DB '" & msNewSqlDBName & "' from file:" & vbCrLf & _
                                                                                     msBackupFileOurNetworkPath & vbCrLf)
        Call mbReport("Starting RESTORE from file: " & vbCrLf & _
                          msBackupFileOurNetworkPath & vbCrLf & "SQL is: " & vbCrLf & sSql)
        labStatus.Text = "Starting RESTORE from file: " & vbCrLf & msBackupFullPath2
        System.Windows.Forms.Application.DoEvents()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            s1 = "Restore.. Failed in RESTORE for Jobs DATABASE: " & msNewSqlDBName & "'.." & vbCrLf & _
                             "Error was" & vbCrLf & sErrors & vbCrLf
            Call gbLogMsg(msCreateLogPath, s1)
            Call mbReport(s1)
            MsgBox(s1, MsgBoxStyle.Critical)
            '== Me.Close()
            Exit Sub
        Else '-ok-
        End If  '-exec-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        labStatus.Text = "Restore done.."
        System.Windows.Forms.Application.DoEvents()
        Call gbLogMsg(msCreateLogPath, "MIGRATION- RESTORE of Backup Jobs Database completed OK.." & vbCrLf)
        Call mbReport("MIGRATION- RESTORE of Backup Jobs Database completed OK.." & vbCrLf)

        '== make new DB current-
        Try
            mCnnSql.ChangeDatabase(msNewSqlDBName)
        Catch ex As Exception
            MsgBox("Failed to make POS database current..  " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        '-- Update compat-level if needed.
        '==    >> 3431.0712- 12-July-2018=
        '==       --  frmMigration.. After Restore of JobTracking DB, and before ALTER columns,
        '==                     Updgrade DB Compatibility_Level to 90 if below..
        Dim objResult As Object
        sSql = "SELECT compatibility_level  "
        sSql &= "FROM sys.databases WHERE name = '" & msNewSqlDBName & "';  "
        If gbGetSqlScalarValue(mCnnSql, sSql, objResult) Then
            If IsNumeric(objResult) Then
                If (CInt(objResult) < 90) Then
                    '- update to 90-
                    sSql = "ALTER DATABASE " & msNewSqlDBName
                    sSql &= "  SET COMPATIBILITY_LEVEL = 90; " & vbCrLf
                    If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
                        Call gbLogMsg(msCreateLogPath, "Migration.. Failed in SET DATABASE COMPATIBILITY_LEVEL 90  = =" & vbCrLf & _
                                        "Error text: " & sErrors & vbCrLf)
                        MsgBox("Migration.. Failed in SET DATABASE COMPATIBILITY_LEVEL 90  = =" & vbCrLf & _
                                         "Error text: " & sErrors, MsgBoxStyle.Exclamation)
                    Else  '-ok-
                        Call gbLogMsg(msCreateLogPath, "Info only- " & vbCrLf & _
                                        "The DATABASE COMPATIBILITY_LEVEL was updated-" & vbCrLf & _
                                        "  From " & CStr(objResult) & " to 90 (SqlServer 2005).." & vbCrLf)
                        MessageBox.Show("Info only- " & vbCrLf & _
                                        "The DATABASE COMPATIBILITY_LEVEL was updated- " & vbCrLf & _
                                        "  From " & CStr(objResult) & " to 90 (SqlServer 2005)..", _
                                        "JobMatixPOS Migration-", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If '-execute-
                End If  '-90=
            Else
                MsgBox("Found Invalid DB Compatibility Level: " & CStr(objResult), MsgBoxStyle.Exclamation)
                Call gbLogMsg(msCreateLogPath, "MIGRATION-   " & _
                              "Found Invalid DB Compatibility Level: " & CStr(objResult) & vbCrLf)
            End If  '-numeric-
        Else
            MsgBox("Failed to retrieve DB Compatibility Level.", MsgBoxStyle.Exclamation)
        End If '-get=

        '-- Next is to upgrade to POS by Adding POS tables to new Database.-

        '-- Add POS Tables..--
        '-- gbCreatePOSdbTables =
        '=3501.0814= Moveed create to new class.
        Dim JMx31POS As New JMxPOS330.clsJMxCreatePOS '= JMxPOS330.clsJMxPOS31

        '-- Add POS tables if not there..
        '-- (Since Build 3203.229 New DB's have POS tables Built-in.-)
        If mbTableExists(mCnnSql, "staff") AndAlso mbTableExists(mCnnSql, "customer") Then
            '-POS tables already created at setup..
        Else
            '-- must add POS tables.
            MsgBox("Adding schema info for POS tables.", MsgBoxStyle.Information)
            ok = JMx31POS.JMx31CreatePOS(mCnnSql, msNewSqlDBName, msCreateLogPath, intErrors)
            If ok Then
                '== sPOSmsg = vbCrLf & "(Including JobMatixPOS tables)" & vbCrLf
            Else  '--failed-
                Call gbLogMsg(msCreateLogPath, "** FAILED to create POS database : " & msNewSqlDBName & _
                                             " on server: " & msSqlServerComputer & vbCrLf & "   = = = = = = = = = = = =  ")
                MsgBox("Failed to create POS database TABLES:  " & msNewSqlDBName, MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Call gbLogMsg(msCreateLogPath, "MIGRATION- Adding POS Tables  to Database  completed OK.." & vbCrLf)
            Call mbReport("MIGRATION- Adding POS Table to Database completed OK.." & vbCrLf)
        End If  '--exists-

        '-- Update DB SystemInfo to change from RM to JobMatixPOS.
        Try
            mCnnSql.ChangeDatabase(msNewSqlDBName)
        Catch ex As Exception
            MsgBox("Failed to make POS database current..  " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        '-- build catalogue- for Import-
        '-- Load DB-Info for selected DB..--
        Call mbReport("-- Getting database schema for DB: " & vbCrLf & "   [" & msNewSqlDBName & "]..")
        labStatus.Text = "-- Getting database schema for DB: " & vbCrLf & "   [" & msNewSqlDBName & "].."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Dim sBuildSchemaLog As String
        If Not gbGetSqlSchemaInfo(mCnnSql, msNewSqlDBName, mColSqlDBInfo, sBuildSchemaLog) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call mbReport("--Error in getting Schema info. for DB: " & vbCrLf & "   [" & msNewSqlDBName & "]..")
            MsgBox("Error in getting Schema info...", MsgBoxStyle.Exclamation)
            Exit Sub
        Else  '-ok-
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '==3311-  now have databse.-
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '-- get Jobmatix System Info-
        '=331.225= If gbLoadsystemInfo(mCnnSql, mColSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        '--save stuff-
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME") '=mSdSystemInfo.Item("BUSINESSSHORTNAME")
        sPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        sABN = mSysInfo1.item("BUSINESSABN")

        '=331.225= End If
        Call mbReport(vbCrLf & "MIGRATION- Bus. short name is: " & msBusinessShortName)
        '-update-
        If Not mSysInfo1.UpdateSystemInfo(New Object() {"RetailHostName", "JobMatixPOS"}) Then
            MsgBox("Failed to update RetailHost Name in systemInfo table..", MsgBoxStyle.Exclamation)
        End If
        '-- Save original DB name for Licence..
        '-- msSqlBackupDBName-
        If Not mSysInfo1.UpdateSystemInfo(New Object() {"OriginalJobMatixDBName", msSqlBackupDBName}) Then
            MsgBox("Failed to update OriginalJobMatixDBName in systemInfo table..", MsgBoxStyle.Exclamation)
        End If

        '-- Before Import--  Expand Stock barcode in job tables--
        sSql = ""
        If mColSqlDBInfo.Contains("JobParts") Then  '-just in case..
            Dim colTable, colFields, colFieldx As Collection
            colTable = mColSqlDBInfo.Item("JobParts")
            colFields = colTable.Item("FIELDS")
            If colFields.Contains("RMBarcode") Then  '-expand to 4000 if not done..
                colFieldx = colFields.Item("RMBarcode")
                If (CInt(colFields.Item("RMBarcode")("CHAR_MAX_LENGTH")) < 40) Then '-need to expand-
                    sSql &= "ALTER TABLE dbo.JobParts ALTER COLUMN RMBarcode VARCHAR (40) NOT NULL; " & vbCrLf
                    sSql &= "ALTER TABLE dbo.QuoteJobParts ALTER COLUMN QuotePartBarcode VARCHAR (40) NOT NULL; " & vbCrLf
                    sSql &= "ALTER TABLE dbo.RAItems ALTER COLUMN RM_ItemBarcode VARCHAR (40) NOT NULL; " & vbCrLf
                End If
            End If  '-barcode-
            If (sSql <> "") Then '-something to do..
                If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
                    s1 = "ERROR: Failed to update JobParts Stock Barcode Columns widths.. " & vbCrLf & sErrors
                    Call gbLogMsg(msCreateLogPath, s1 & vbCrLf)
                    MsgBox(s1, MsgBoxStyle.Critical)
                Else '--ok-
                    s1 = "Note: The Jobs Table stock Barcode columns (JobParts etc)- " & vbCrLf & _
                           "Column widths have been expanded to 40 chars.." & vbCrLf
                    Call gbLogMsg(msCreateLogPath, s1 & vbCrLf)
                    MsgBox(s1, MsgBoxStyle.Information)
                End If '-exec-
            End If  '--sSql-
        End If  '-jobParts.-

        '- Next is to Import RM Tables Data..--
        labStatus.Text = "Ready to import RetailManager stock info.."

        labStatus.Text &= vbCrLf & "Importing Data.."
        '-- load import form-
        Call gbLogMsg(msCreateLogPath, "MIGRATION- Importing RM data into Database.." & vbCrLf)
        Dim frmImport1 As New frmImportRM(Me, msSqlServer, mCnnSql, msNewSqlDBName, mColSqlDBInfo, msJobMatixVersion)

        frmImport1.ShowDialog()
        If frmImport1.cancelled Then
            MsgBox("MIGRATION- Import cancelled..  Database will be deleted..", MsgBoxStyle.Exclamation)
        Else
            btnBrowse.Enabled = False
            Call gbLogMsg(msCreateLogPath, _
                          "MIGRATION- Importing RM Tables to Database completed OK.." & vbCrLf & _
                           "DBname: " & msNewSqlDBName & ";  ABN: " & sABN & ";  PostCode: " & sPostCode & " ==" & vbCrLf)
            labStatus.Text = "Import successful...." & vbCrLf & "Application needs to be re-started.."
            MsgBox("Import successful.. " & vbCrLf & "Application needs to be re-started.." & vbCrLf & vbCrLf & _
                      "Pls Note: Your new JobMatix+POS dbname is: '" & msNewSqlDBName & "'..", MsgBoxStyle.Information)
        End If

    End Sub '-continue-
    '= = = = = =  = ==  =

End Class '-frmMigration-
'= = = = = = = = = = = = = 

'== end form =