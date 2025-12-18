Option Strict Off
Option Explicit On
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application

Public Module modBackupRestore35

    '==
    '==    3501.0713 13-July-2018= (Updates from 3431.0712- )
    '==       -- 3501.0714  Separate out backup/Restore into separate Module.
    '==
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==      -- From POS Main- Drop "labStatus" from Backup call..
    '==
    '==  NEW DLL- 4219 VERSION
    '==    -- 4219.1125 25-Nov-2019= 
    '==      -- Moved this  module  "modBackupRestore35" as Public
    '==             to  HERE into JMxRetailHost.dll so EVERyONE can use it..
    '==      --  ALSO- Update RESTORE with this FIX from the JobTracking Version
    '==                -- Updated 3501.1105  05-Nov-2018=  
    '==                -- (a)  DB RESTORE-  make sure both db-names "_jobtracking" and "_Jmpos" are accepted.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Private mFormWait1 As frmWait
    Private msVersionPOS As String = ""
    Private mFrmParent As Form

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = msVersionPOS
        mFormWait1.labHdr.Text = "Debtors Statements-"
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.Show(mFrmParent)
        System.Windows.Forms.Application.DoEvents()
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

    '--  get recordset as collection for SELECT..--

    Private Function mbGetRecordCollection(ByRef cnnSQL As OleDbConnection, _
                                            ByVal sSql As String, _
                                             ByRef colResult As Collection) As Boolean
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sName As String
        Dim col1 As Collection
        Dim colRow As Collection
        '== Dim fld1 As ADODB.Field

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetRecordCollection = False
        If Not gbGetDataTable(cnnSQL, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset.." & vbCrLf & _
             "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If Not (rs1 Is Nothing) Then
                colResult = New Collection
                If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '= rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        colRow = New Collection
                        For Each column1 As DataColumn In rs1.Columns '== fld1 In rs1.Fields
                            col1 = New Collection
                            sName = column1.ColumnName
                            col1.Add(sName, "name")
                            col1.Add(dataRow1.Item(sName), "value")
                            colRow.Add(col1, LCase(sName))
                        Next column1 '= fld1 '=col1
                        colResult.Add(colRow)
                    Next dataRow1
                    '== While Not rs1.EOF
                    '==   rs1.MoveNext()
                    '== End While '--eof.-
                    mbGetRecordCollection = True '--got something..-
                End If '--EMPTY. bof-
                '== rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- RESTORE -- (ex modJobsMain--
    '-- R e s t o r e  JOBS DB --
    '-- R e s t o r e  JOBS DB --

    '--  use FILE-OPEN dialogue to get backed-up File-spec. to restore.--
    '------  show file date/time to confirm restore..

    Public Function gbRestoreJobsDatabase(ByRef cnnSQL As OleDbConnection, _
                                          ByVal sCurrentUserNT As String, _
                                            ByRef dlg1 As OpenFileDialog, _
                                             ByVal sStartPath As String, _
                                               ByRef labStatus As System.Windows.Forms.Label, _
                                                  ByRef sRestoredSqlDBname As String) As Boolean

        Dim sSql, sName As String
        Dim s1, s2, s3, sErrors As String
        Dim lCount, lResult, IntJobMatixDBid As Integer
        Dim kx, ix, fx, iPos, L1 As Integer
        Dim sMoveSql, sWithSql As String
        Dim sMasterPath, sMSSQLServerDataDir As String
        Dim sTitle, sFullPath As String
        Dim sTargetFileFullPath As String
        Dim sMsg, sFileMsg, sInfoMsg As String
        Dim sBackupName, sSqlDBName As String
        Dim sBackupServerName As String
        Dim dateBackupFinished As Date
        Dim rdr1 As OleDbDataReader  '= ADODB.Recordset
        Dim dataTable1 As DataTable
        '== Dim fld1 As ADODB.Field
        '--Dim colSystemInfo As Collection
        Dim colRecord As Collection
        Dim col1 As Collection
        Dim colFiles As Collection
        Dim colWhichUsers As Collection
        Dim bOk, bStillWaiting As Boolean
        Dim MyResult As System.Windows.Forms.DialogResult

        sTitle = "" '--"JobsDatabaseBackup_*"
        sStartPath = ""
        gbRestoreJobsDatabase = False
        sBackupServerName = ""
        sMSSQLServerDataDir = ""
        sSqlDBName = ""
        '--  Get target MSSQL data dir on this Server.--  start with SQLServer-2000--

        '-- REv-2912 ++   -- Get target MSSQL data dir on this Server.--  USE MASTER DB files location..--
        '--  dev stuff.--
        '----  Locate DB files for Mster DB..
        '----  Locate DB files for Mster DB..
        '=====txtResults.Text = txtResults.Text & vbCrLf & "Checking master DB file path.." & vbCrLf
        sMasterPath = ""
        Call gbLogMsg(gsRuntimeLogPath, "RESTORE is starting.." & vbCrLf)

        Try  '--main restore try..--
            lResult = glExecSP(cnnSQL, "sp_helpdb", "@dbname='master'", sErrors, rdr1)
            If lResult <> 0 Then
                MsgBox(sErrors, MsgBoxStyle.Exclamation)
            Else
                lCount = 0 '--count r'sets..-
                '--check dataReader (recordset)--
                While Not (rdr1 Is Nothing)
                    lCount = lCount + 1
                    If (lCount = 2) Then '--we want the 2nd rset..--
                        If rdr1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                            While (rdr1.Read) AndAlso (sMasterPath = "")
                                '--MsgBox "checking file path for master..-
                                Try
                                    '==s1 = Trim(rdr1.GetString("NAME")) '-- Rev-3101.919-- FOR ado.net..
                                    s1 = Trim(rdr1.Item("NAME")) '-- Rev-3103.225- Fixed..
                                    If (LCase(s1) = "master") Then
                                        Try
                                            '== s2 = Trim(rdr1.GetString("FILENAME")) '---- ditto, in case..-
                                            s2 = Trim(rdr1.Item("FILENAME")) '-- Rev-3103.225- Fixed..
                                            sMasterPath = s2
                                        Catch ex2 As Exception
                                            '== 3.1.3103.225 =
                                            MsgBox("Error in reading master db filename." & vbCrLf & _
                                                                       ex2.Message, MsgBoxStyle.Exclamation)
                                            '=Me.Close()
                                            Exit Function
                                        End Try
                                    End If '--master..--
                                Catch ex As Exception
                                    '== 3.1.3103.225 =
                                    MsgBox("Error in reading master db-name." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                                    '= Me.Close()
                                    Exit Function
                                End Try  '--NAME-
                            End While  '--read-
                        End If '- has rows--empty.-
                    End If '--2nd rset..-
                    '= rs1 = rs1.NextRecordset
                    If Not rdr1.NextResult Then Exit While '-done-
                End While
            End If '--ok-
            If (sMasterPath <> "") Then '--extract directory path..-
                ix = InStr(LCase(sMasterPath), "data\master")
                If (ix > 0) Then
                    sMSSQLServerDataDir = Left(sMasterPath, ix + 4)
                End If
            End If
            '===MsgBox "sMasterPath is : " & vbCrLf & sMasterPath & vbCrLf & vbCrLf & _
            ''===         "sSqlDataPath Is " & sMSSQLServerDataDir, vbInformation
            '----END of:  Locate DB files for Mster DB..
            '----END of:  Locate DB files for Mster DB..

            '--  get actual (Backup File) "RESTORE FROM" location from operator..--
            dlg1.Title = "Jobs Database RESTORE- Select Backup file to reload onto Jobs DB.."
            dlg1.Filter = "SQL DB Backup Files (*.bak)|*.bak|All Files (*.*)|*.*"
            dlg1.InitialDirectory = sStartPath '--msAppPath
            dlg1.FileName = sStartPath & sTitle
            '== On Error Resume Next
            MyResult = dlg1.ShowDialog
            '--check for cancel--
            '== If Err().Number <> 0 Then '--Cancelled==
            If MyResult <> System.Windows.Forms.DialogResult.OK Then     '= If Err().Number <> 0 Then '--Cancelled==
                '--set rs = nothing
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '==mlStaffTimeout = 0  '--start timing out..--
                Exit Function
            End If
            '== On Error GoTo 0
            sFullPath = dlg1.FileName

            '== sTitle = dlg1.FileTitle '--bare filename..-
            sTitle = Path.GetFileName(sFullPath)
            If Not gbExecuteCmd(cnnSQL, "USE master  " & vbCrLf, L1, sErrors) Then
                MsgBox(vbCrLf & "Backup.. Failed in USE for DATABASE 'master'  = =" & vbCrLf & _
                       "Error text: " & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
            End If
            '--  Get Header from Backup File..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            sSql = "RESTORE HEADERONLY " & vbCrLf
            sSql = sSql & "FROM DISK = '" & sFullPath & "'" & vbCrLf

            If Not gbGetDataTable(cnnSQL, dataTable1, sSql) Then
                MsgBox("Failed to get RESTORE HEADERONLY recordset.." & vbCrLf & _
                         "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
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
                                    Case "databasename" : sSqlDBName = dataRow1.Item(sName)
                                    Case "backupfinishdate" : dateBackupFinished = CDate(dataRow1.Item(sName))
                                    Case "backupname" : sBackupName = dataRow1.Item(sName)
                                    Case "servername" : sBackupServerName = dataRow1.Item(sName)
                                End Select
                            End If '-null-
                        Next column1 '= fld1 '--fld1-
                    End If '--bof-
                    '== rs1.Close()
                End If '--nothing
            End If '--get-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '== rs1 = Nothing
            If sSqlDBName = "" Then '-- no valid db name in Backup Header....--
                MsgBox("No valid db name in Backup Header..", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            '---  get Data file list from backup header..-
            sSql = "RESTORE  FILELISTONLY  " & vbCrLf
            sSql = sSql & "FROM DISK = '" & sFullPath & "'" & vbCrLf
            If Not mbGetRecordCollection(cnnSQL, sSql, colFiles) Then Exit Function
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
                    If sMoveSql <> "" Then sMoveSql = sMoveSql & ","
                    sMoveSql = sMoveSql & " MOVE '" & s1 & "' TO '" & sMSSQLServerDataDir & s3 & "'"
                    '-- save target file name for replace-test..-
                    If (Right(LCase(s2), 4) = ".mdf") And (sTargetFileFullPath = "") Then
                        sTargetFileFullPath = sMSSQLServerDataDir & s3
                    End If
                Next colRecord '--record.-
            End If '--count..-
            '===MsgBox "Backup Set has source files: " + vbCrLf + sMsg, vbInformation
            '--  show DB header..-
            '==MsgBox "DB Backup Dataset: " + vbCrLf + sFullPath + vbCrLf + vbCrLf + _
            '==                 "DataSet Name=" + sBackupName + vbCrLf + "Database Name=" + sSqlDBName, vbInformation
            sInfoMsg = "RESTORE JobTracking database: " & vbCrLf & _
                   "  '" & sSqlDBName & "'" & vbCrLf & vbCrLf & _
                   "Backup Info: " & sBackupName & vbCrLf & vbCrLf & _
                   "From Server: " & sBackupServerName & vbCrLf & vbCrLf & _
                   "From SQL Files: " & vbCrLf & sFileMsg & vbCrLf & vbCrLf & _
                   "Please close JobTracking application on all other client workstations.." & vbCrLf & _
                   vbCrLf & vbCrLf & "OK to RESTORE Jobs DB FROM the backup file:" & vbCrLf & _
                                             sFullPath & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, "RESTORE- confirming.." & vbCrLf & sInfoMsg & vbCrLf)
            If Not MsgBox(sInfoMsg, _
                  MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '==mlStaffTimeout = 0  '--start timing out..--
                Exit Function
            End If
            '-- Check Filename for V22..--

            'If Not (InStr(LCase(sTitle), "jobtracking-v22") > 0) Then
            '    If (MsgBox("Warning: " & vbCrLf & "   The selected backup file:  '" & sTitle & "' " & vbCrLf & _
            '              "   may not be a Version 2.2 JobTracking backup.." & vbCrLf & vbCrLf & _
            '                "Do you want to continue and RESTORE this file ??", _
            '                     MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
            '        Exit Function
            '    End If '--msgbox-
            'End If '--instr-

            '==
            '==  NEW DLL- 4219 VERSION
            '==    -- 4219.1122 22-Nov-2019= 
            '==      -- Moved this  module  "modBackupRestore35" as Public
            '==             to  HERE into JMxRetailHost.dll so EVERyONE can use it..
            '==      --  ALSO- Update RESTORE with this FIX from the JobTracking Version
            '==                -- Updated 3501.1105  05-Nov-2018=  
            '==                -- (a)  DB RESTORE-  make sure both db-names "_jobtracking" and "_Jmpos" are accepted.
            If (InStr(LCase(sTitle), "jobtracking-v22") > 0) Or _
                            (InStr(LCase(sTitle), "jmpos-v22") > 0) Then
                '-ok=
            Else
                If (MsgBox("Warning: " & vbCrLf & "   The selected backup file:  '" & sTitle & "' " & vbCrLf & _
                          "   may not be a Version 2.2 JobTracking backup.." & vbCrLf & vbCrLf & _
                            "Do you want to continue and RESTORE this file ??", _
                                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                    Exit Function
                End If '--msgbox-
            End If '--instr-
            '-- Continue..-

            '-- Continue..-
            '==frmSplash.Labstatus.Text = "Releasing DB from our connections.. "
            '-- Release JobTracking DB..-
            labStatus.Text = "Releasing DB from our connections.. "
            System.Windows.Forms.Application.DoEvents()

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            sSql = "USE master  " & vbCrLf
            If Not gbExecuteCmd(cnnSQL, sSql, L1, sErrors) Then
                MsgBox(vbCrLf & "Backup.. Failed in USE for DATABASE 'master'  = =" & vbCrLf & _
                                 "Error text: " & sErrors, MsgBoxStyle.Exclamation)
            End If

            '== frmSplash.Labstatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '--light yellow-
            labStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '--light yellow-
            '== NOT in vb.net..= frmSplash.labStatus.BackStyle = 1   '--opaque..-

            sSql = "RESTORE DATABASE " & sSqlDBName & vbCrLf
            sSql = sSql & "FROM DISK = '" & sFullPath & "'" & vbCrLf
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
            Call gbLogMsg(gsRuntimeLogPath, "RESTORE- checking if DB " & sSqlDBName & " exists.." & vbCrLf)
            '== 3073.309==  wait only if DB curently exists.. 
            If gbExistsDatabase(cnnSQL, sSqlDBName) Then
                '--  only if DB exists.--
                Call gbLogMsg(gsRuntimeLogPath, "RESTORE- confirming to overwrite DB " & sSqlDBName & ".." & vbCrLf)
                If Not MsgBox("CAUTION! " & vbCrLf & "You are about to RESTORE the Jobs Database" & vbCrLf & _
                       " FROM the backup file:" & vbCrLf & sFullPath & vbCrLf & vbCrLf & _
                       "To a prior state as at the date/time shown in the selected backup file name.." & vbCrLf & vbCrLf & _
                       "Subsequent changes will be lost.." & vbCrLf & _
                       "Last Chance! Do you really want to continue ??", _
                          MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    '==mlStaffTimeout = 0  '--start timing out..--
                    Exit Function
                End If
                '-- no turning back..--
                '-- SET UP DB-ID for "whoUsing" -- in "mIntJobMatixDBid"..
                If gbGetDB_ID(cnnSQL, sSqlDBName, lResult) Then  '--NO error.--
                    If (lResult > 0) Then    '--valid Db_id..-
                        IntJobMatixDBid = lResult
                        Call gSetJobMatixDBid(IntJobMatixDBid)
                    End If
                Else  '--error--
                    MsgBox("DB Error- 'RESTORE'.." & vbCrLf & _
                                gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                End If
                bStillWaiting = True
                '== frmSplash.Labstatus.Text = "Waiting for EXCLUSIVE access to Jobs database.."
                labStatus.Text = "Waiting for EXCLUSIVE access to Jobs database.."
                System.Windows.Forms.Application.DoEvents()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If Not gbWhoUsing(cnnSQL, sSqlDBName, colWhichUsers) Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    s1 = "Failed to get user list.  Sql was 'exec sp_who'..  " & vbCrLf & _
                                "Error text: " & vbCrLf & gsGetLastSqlErrorMessage()
                    Call gbLogMsg(gsRuntimeLogPath, "RESTORE- DB " & sSqlDBName & ".." & vbCrLf & s1 & vbCrLf)
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                Else '--ok
                    While bStillWaiting
                        If (colWhichUsers.Count() <= 0) Then '--no users..
                            Call gbLogMsg(gsRuntimeLogPath, "RESTORE- DB '" & sSqlDBName & "'.." & vbCrLf & _
                                                              "No logged-in users to wait for.." & vbCrLf)
                            bStillWaiting = False '--break out..-
                        Else '--show Users..
                            sMsg = ""
                            For Each col1 In colWhichUsers
                                sMsg = sMsg & col1.Item("LOGINAME") & "  (" & Trim(col1.Item("HOSTNAME")) & ") " & vbCrLf
                            Next col1 '--col1-
                            Call gbLogMsg(gsRuntimeLogPath, "RESTORE- DB '" & sSqlDBName & "'.." & vbCrLf & _
                                                    "Waiting for logged-in users:" & vbCrLf & sMsg & vbCrLf)
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            If Not MsgBox("Attention! " & vbCrLf & "RESTORE needs EXCLUSIVE access to Jobs database.." & vbCrLf & _
                                              vbCrLf & "Some DB users are still logged in... viz:" & vbCrLf & sMsg & vbCrLf & vbCrLf & _
                                                "You must close these JobTracking clients to be able to continue.." & vbCrLf & vbCrLf & _
                                                    "Do want to continue with RESTORE ??", _
                                            MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                Exit Function
                            Else '--continue--
                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                                If Not gbWhoUsing(cnnSQL, sSqlDBName, colWhichUsers) Then
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                    MsgBox("Failed to get user list.  Sql was 'exec sp_who'..  " & vbCrLf & _
                                          "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                                    bStillWaiting = False
                                End If
                            End If '--yes..-
                        End If '-count-
                    End While '--waiting..-
                End If '--who..--
                '-- END of  only if DB exists.--
            Else  '-no exist--
                Call gbLogMsg(gsRuntimeLogPath, "RESTORE- No DB found for name: " & vbCrLf & _
                                                                     sSqlDBName & ".." & vbCrLf)
                If Not MsgBox("CAUTION! No current database was found.." & vbCrLf & _
                               "However, You are about to execute RESTORE SQL:" & vbCrLf & vbCrLf & _
                             sSql & vbCrLf & vbCrLf & "Do you want to continue ??", _
                             MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Exit Function
                End If
            End If  '--exists

            '--check if REPLACE needed and consentd..--
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            s1 = Dir(sTargetFileFullPath)
            If (s1 <> "") Then '--exists.-
                If MsgBox("Important Note: The database files like: " & vbCrLf & _
                            "   '" & sTargetFileFullPath & "'" & vbCrLf & _
                            "already exist on this server.." & vbCrLf & vbCrLf & _
                            "This RESTORE will overwrite these database files." & vbCrLf & vbCrLf & _
                            "Do you want to continue and RESTORE the database ?", _
                            MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    Exit Function
                Else '--continue..-
                    sSql = sSql & ", REPLACE " & vbCrLf
                End If '--yes..-
                '--else  doesn't exist..-
            End If
            If Not MsgBox("ONE LAST CHANCE! " & vbCrLf & " To NOT execute the  RESTORE SQL command:" & vbCrLf & vbCrLf & _
                           sSql & vbCrLf & vbCrLf & "OK to Continue with RESTORE??", _
                               MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Exit Function
            End If
            '== frmSplash.Labstatus.Text = "Attempting RESTORE of Jobs database from file: " & sFullPath
            Call gbLogMsg(gsRuntimeLogPath, "Starting RESTORE- DB '" & sSqlDBName & "' from file:" & vbCrLf & _
                                                                                         sFullPath & vbCrLf)
            labStatus.Text = "Staring RESTORE from file: " & vbCrLf & sFullPath
            System.Windows.Forms.Application.DoEvents()

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbExecuteCmd(cnnSQL, sSql, L1, sErrors) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                s1 = "Backup/Restore.. Failed in RESTORE for Jobs DATABASE: " & sSqlDBName & "'.." & vbCrLf & _
                                 "Error was" & vbCrLf & sErrors & vbCrLf
                Call gbLogMsg(gsRuntimeLogPath, s1)
                MsgBox(s1, MsgBoxStyle.Critical)
            Else '--ok--
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                gbRestoreJobsDatabase = True
                labStatus.Text = "Restore done.." & vbCrLf & "Adding your login to SQL Server.."
                System.Windows.Forms.Application.DoEvents()
                Call gbLogMsg(gsRuntimeLogPath, "RESTORE of Jobs Database completed OK.." & vbCrLf)
                '-- MAKE user login name--
                '== s1 = msSqlServerComputerName & "\" & msCurrentUserName
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                bOk = gbAddNewUser(cnnSQL, sSqlDBName, sCurrentUserNT, sInfoMsg, sErrors)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                If bOk Then
                    MsgBox(sInfoMsg, MsgBoxStyle.Information)
                Else  '--failed..
                    MsgBox(sErrors, MsgBoxStyle.Exclamation)
                End If

                MsgBox("RESTORE of Jobs Database completed OK.." & vbCrLf & vbCrLf & _
                 "Administrator should check that Windows User Accounts can access the Database..", MsgBoxStyle.Information)
                gbRestoreJobsDatabase = True
                sRestoredSqlDBname = sSqlDBName '-- send back..-
            End If
            '===mCnnSql.Close
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '==frmSplash.Labstatus.Text = ""
            Exit Function

        Catch ex As Exception
            sMsg = "ERROR in Main RESTORE function.." & vbCrLf & ex.Message & vbCrLf & _
                      "RESTORE closing.." & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            MsgBox(sMsg, MsgBoxStyle.Critical)
            Exit Function
        End Try  '-- Outer main Try--

    End Function '--restore--
    '= = = = = = = = =  = = = = = = = = = = = = = = =
    '-===FF->

    '=3401.415= -- BACKUP -- (ex frmJobmatixMain)--
    '-- B a c k u p   JOBS DB --
    '-- B a c k u p   JOBS DB --
    '-- B a c k u p   JobMatix POS DB --
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==      -- From POS Main- Drop "labStatus" from Backup call..
    '==  NEW DLL- 4219 VERSION
    '==    -- 4219.1125 25-Nov-2019= 
    '==        -- Drop Input parm SysInfo1..  Use SysInfo in this dll..

    Public Function gbBackupJobsDatabase(ByRef frmMain As Form, _
                                          ByVal sSqlServerComputer As String, _
                                          ByRef cnnSQL As OleDbConnection, _
                                          ByVal sMachineName As String, _
                                          ByVal sComputerName As String, _
                                          ByVal sJobmatixAppName As String, _
                                          ByVal sCurrentUserNT As String, _
                                          ByVal sSqlDbName As String, _
                                            ByRef dlg1Save As SaveFileDialog, _
                                                  ByRef sFinalBackupPath As String) As Boolean
        Dim sSql As String
        Dim sStartPath As String
        Dim sErrors, sMsg, sValue As String
        Dim iPos, L1 As Integer
        Dim bRunningOnServer As Boolean
        Dim sSelectedFullPath As String
        Dim sServerLocalPath As String '--needed if backing up from client..-
        Dim sServerNetworkPath As String '--needed if backing up from client..-
        Dim sTitle As String
        Dim sSqlBackupPath As String
        Dim sSqlCopySourcePath As String = ""
        '= Dim sdSysInfo As clsStrDictionary  '==  Scripting.Dictionary
        '= Dim colSystemInfo As Collection
        '= Dim col1 As Collection
        Dim MyResult As System.Windows.Forms.DialogResult
        Dim SysInfo1 As clsSystemInfo

        gbBackupJobsDatabase = False
        sFinalBackupPath = ""
        mFrmParent = frmMain
        SysInfo1 = New clsSystemInfo(cnnSQL)

        '=3401.417= bRunningOnServer = (UCase(sSqlServerComputer) = UCase(sComputerName))
        '- For RDS, thin client can thinks it is on server.
        bRunningOnServer = (UCase(sSqlServerComputer) = UCase(sMachineName))
        sTitle = "JobsDB-" & sSqlDbName & "-V22-Backup_" & Format(CDate(DateTime.Today), "ddMMMyyyy") & "-" & _
                                                                                   Format(TimeOfDay, "HHmm") & ".bak"
        '= sStartPath = gsJobMatixLocalDataDir("JobMatix34") '= msAppPath
        '=4201.1110=
        sStartPath = gsJobMatixLocalDataDir(sJobmatixAppName) '= msAppPath
        sServerLocalPath = ""
        sServerNetworkPath = ""
        '-- Get last backup file path from systemInfo..--
        '==If gbLoadsystemInfo(mCnnSql, colSystemInfo, sdSysInfo) Then '-- get RetailManager Jet path.--
        For Each sKey1 As String In SysInfo1.keys '== col1 In colSystemInfo
            sValue = Trim(SysInfo1.item(sKey1))
            If sKey1 = "LASTBACKUPFULLPATH" Then
                sStartPath = sValue '= col1.Item("systemvalue")
                '--Exit For
            ElseIf sKey1 = "SERVERSHARELOCALPATH" Then  '--in case NOT running on server..-
                sServerLocalPath = sValue '= col1.Item("systemvalue")
                '= 3077.602= Don't add "\" to null psth..
                If (sServerLocalPath <> "") AndAlso (VB.Right(sServerLocalPath, 1) <> "\") Then
                    sServerLocalPath = sServerLocalPath & "\"
                End If
            ElseIf sKey1 = "SERVERSHARENETWORKPATH" Then
                sServerNetworkPath = sValue  '=col1.Item("systemvalue")
                '= 3077.602= Don't add "\" to null psth..
                If (sServerNetworkPath <> "") AndAlso (VB.Right(sServerNetworkPath, 1) <> "\") Then
                    sServerNetworkPath = sServerNetworkPath & "\"
                End If
            End If '--key.-
        Next sKey1 '= col1
        '==End If
        If Not bRunningOnServer Then '--must have network paths.-
            If (sServerLocalPath = "") Or (sServerNetworkPath = "") Then
                '===MsgBox "No Server network path defined in JobTracking SystemInfo..", vbExclamation
                MsgBox("Note: DB Backup must go via a Server drive/folder to get to your media," & vbCrLf & _
                        "and the Server paths: " & vbCrLf & _
                        "   'ServerShareLocalPath' and 'ServerShareNetworkPath' " & vbCrLf & vbCrLf & _
                        "must be defined in the JobTracking SystemInfo table" & vbCrLf & _
                         "before a DB Backup to a network client can be done..", MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '= mlStaffTimeout = 0 '--start timing out..--
                Exit Function
            End If
        End If
        If (sStartPath = "") Then
            sStartPath = gsJobMatixLocalDataDir("JobMatix34") '= msAppPath
        Else '--extract dir from fullpath..-
            iPos = InStrRev(sStartPath, "\")
            If (iPos > 0) Then sStartPath = VB.Left(sStartPath, iPos - 1)
        End If

        '--  get FINAL actual SAVE location from operator..--
        dlg1Save.Title = "Jobs Database Backup- Pls Select target directory.."
        dlg1Save.Filter = "SQL DB Backup Files (*.bak)|*.bak|All Files (*.*)|*.*"

        '-- 3077.601  ALWAYS use SharePath if exists...-
        '---- first create backup on share path, and then SAVE it to users save loc.--
        '= 3077.602=If bRunningOnServer Then
        dlg1Save.InitialDirectory = sStartPath
        '= 3077.602=End If '--msAppPath
        '--  else
        dlg1Save.FileName = sTitle
        '== On Error Resume Next

        '--  Show Dialog --
        MyResult = dlg1Save.ShowDialog()
        '--check for cancel--
        '== If Err().Number <> 0 Then '--Cancelled==
        If (MyResult <> System.Windows.Forms.DialogResult.OK) Then     '= If Err().Number <> 0 Then '--Cancelled==
            '--set rs = nothing
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '= mlStaffTimeout = 0 '--start timing out..--
            Exit Function
        End If
        '== On Error GoTo 0
        sSelectedFullPath = dlg1Save.FileName
        If bRunningOnServer And (sServerLocalPath = "") Then
            sSqlBackupPath = sSelectedFullPath '--on server- No local path.. use user selected target..-
        Else '--running on client.. OR on Server with local path. (as intermediate)-
            sSqlBackupPath = sServerLocalPath & sTitle '-- Backup path must be local to server.--
        End If
        If Not MsgBox("OK to BACKUP Jobs database TO the backup file:" & vbCrLf & sSelectedFullPath & vbCrLf, _
                             MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '= mlStaffTimeout = 0 '--start timing out..--
            Exit Function
        End If
        '== sSql = "USE " & msSqlDbName & vbCrLf
        '== If Not gbExecuteTrans(mCnnSql, sSql) Then
        '==   MsgBox(vbCrLf & "Backup DB.. Failed in USE for DATABASE: " & msSqlDbName & " = =")
        '== End If
        cnnSQL.ChangeDatabase(sSqlDbName)
        sSql = "BACKUP DATABASE " & sSqlDbName & vbCrLf
        sSql = sSql & "TO DISK = '" & sSqlBackupPath & "'" & vbCrLf
        sSql = sSql & "WITH NAME = 'Full Backup of JobTracking DB on " & _
                      Format(CDate(DateTime.Today), "ddMMMyyyy") & "-" & Format(TimeOfDay, "HH:mm") & "'" & vbCrLf

        '= labStatus.Text = " Backing up Jobs database to: " & sSelectedFullPath
        '=4201.1110..
        Call mWaitFormOn("Pls Wait.  Backing up database to:" & vbCrLf & sSelectedFullPath)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Application.DoEvents()
        If Not gbExecuteCmd(cnnSQL, sSql, L1, sErrors) Then
            Call mWaitFormOff()
            MsgBox("Backup.. Failed in Backup of jobs DATABASE: " & sSqlDbName & "'.." & vbCrLf & _
                                        "TO '" & sSqlBackupPath & "'.." & vbCrLf & vbCrLf & _
                                                 "Error info was:" & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Critical)
        Else '--ok--
            Call mWaitFormOff()
            If bRunningOnServer And (sServerLocalPath = "") Then
                Call gbLogMsg(gsRuntimeLogPath, vbCrLf & "** Server-side Backup completed ok.." & vbCrLf)
                MsgBox("Server-side Backup completed ok to the File Path:" & vbCrLf & _
                                                     "'" & sSelectedFullPath & "'..", MsgBoxStyle.Information)
                gbBackupJobsDatabase = True
                sFinalBackupPath = sSelectedFullPath
                '--save backup file path in systemInfo..--
                If Not SysInfo1.UpdateSystemInfo(New Object() {"LastBackupFullPath", sSelectedFullPath}) Then
                    MsgBox("Failed to update BACKUP path details in systemInfo table..", MsgBoxStyle.Critical)
                End If '--update-
            Else '--on client.. OR Server with local path-  copy to final target dir..--
                sMsg = "Backup file Created ok.. " & vbCrLf & "About to Copy file: " & vbCrLf & _
                                  sServerNetworkPath & sTitle & vbCrLf & " to: " & vbCrLf & sSelectedFullPath
                If bRunningOnServer Then
                    sSqlCopySourcePath = sSqlBackupPath
                    sMsg = "Backup file Created ok on server.. " & vbCrLf & "About to Copy file: " & vbCrLf & _
                                       sSqlBackupPath & vbCrLf & vbCrLf & " to: " & vbCrLf & sSelectedFullPath
                Else  '--from client..
                    sSqlCopySourcePath = sServerNetworkPath & sTitle
                End If
                MsgBox(sMsg, MsgBoxStyle.Information)

                '==3077.621= USE "My.Computer.FileSystem.CopyFile" for file copy..
                Do
                    Try
                        My.Computer.FileSystem.CopyFile(sSqlCopySourcePath, sSelectedFullPath, _
                                                 FileIO.UIOption.AllDialogs, FileIO.UICancelOption.ThrowException)
                        '--save backup file path in systemInfo..--
                        If Not SysInfo1.UpdateSystemInfo(New Object() {"LastBackupFullPath", sSelectedFullPath}) Then
                            frmMain.BringToFront()
                            MsgBox("Failed to update BACKUP path details in systemInfo table..", MsgBoxStyle.Critical)
                            Exit Do
                        Else  '- sysinfo update ok-
                            gbBackupJobsDatabase = True
                            sFinalBackupPath = sSelectedFullPath
                            Call gbLogMsg(gsRuntimeLogPath, vbCrLf & _
                                                       "** Backup and file-copy completed ok.." & vbCrLf & _
                                                       "Copied from: '" & sSqlCopySourcePath & "'" & vbCrLf & _
                                                       "Copied to:  '" & sSelectedFullPath & "'" & vbCrLf)
                            frmMain.BringToFront()
                            'MsgBox("Backup and file-copy completed ok.." & vbCrLf & _
                            '                           "Src path: '" & sSqlCopySourcePath & "'" & vbCrLf & _
                            '                           "Dest path:  '" & sSelectedFullPath & "'", MsgBoxStyle.Information)
                            Dim sDeleteMsg As String
                            sDeleteMsg = "Backup and file-copy completed ok.." & vbCrLf & _
                                                       "Src path: '" & sSqlCopySourcePath & "'" & vbCrLf & _
                                                       "Dest path:  '" & sSelectedFullPath & "'" & vbCrLf & vbCrLf
                            '=3403.909==  DELETE source copy if ok with user..
                            If (MsgBox(sDeleteMsg + "OK to delete the source copy now?", _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                                '--ok- can delete the source copy.
                                Try
                                    My.Computer.FileSystem.DeleteFile(sSqlCopySourcePath, _
                                                   FileIO.UIOption.OnlyErrorDialogs, _
                                                   FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.ThrowException)
                                Catch ex As Exception
                                    MsgBox("Delete Failed- Error text was: " & vbCrLf & _
                                                 ex.ToString & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
                                End Try
                            End If  '-yes-
                            Exit Do
                        End If '--update-
                    Catch ex As Exception
                        Call gbLogMsg(gsRuntimeLogPath, "Copy aborted, or Error in copying file.." & vbCrLf & _
                                                   "Source: '" & sSqlCopySourcePath & "'" & vbCrLf & _
                                                   "Dest. :  '" & sSelectedFullPath & "'" & vbCrLf & vbCrLf & _
                                "Error text was: " & vbCrLf & ex.ToString & vbCrLf & vbCrLf)
                        frmMain.BringToFront()
                        MyResult = MsgBox("Copy aborted, or Error in copying file.." & vbCrLf & _
                                                   "Source path: '" & sSqlCopySourcePath & "'" & vbCrLf & _
                                                   "Dest. path:  '" & sSelectedFullPath & "'" & vbCrLf & _
                                "(Note: Destination path may be unreachable from Server, " & vbCrLf & _
                                "     or forbidden to the Jobmatix process.)" & vbCrLf & vbCrLf & _
                                "ERROR Text provided was:" & vbCrLf & _
                                ex.ToString & vbCrLf & vbCrLf, MsgBoxStyle.RetryCancel)
                        If MyResult = System.Windows.Forms.DialogResult.Cancel Then Exit Do
                    End Try
                Loop  '--break when done or aborted
            End If '--on server.-
        End If '--execute..--

    End Function  '-gbBackupJobsDatabase-
    '= = = = = = = = = == = = = = = = = = = =


End Module  '-modBackupRestore35-
'= = = = = = = = = = =  = = = = = =
