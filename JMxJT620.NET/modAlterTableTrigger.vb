
Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
Imports VB = Microsoft.VisualBasic

Imports System.Data.OleDb
Imports System.Threading

Module modAlterTableTrigger

    '= --  ***20-may-2018= Script for Create TRIGGER for ALTER TABLE  ***
    '= --  Detects and does a  ROLLBACK for this specific ALTER command.
    '= --    VIZ- ALTER TABLE [dbo].[Jobs] alter column ProblemLong varchar (4000) ;
    '= --         OR ServiceNotes or SessionTimes or Notifications..
    '= USE BHC_JobTracking ;

    '== For JobMatix Build 3431.522--
    '==  This trigger is here to detect previous builds of JobMatix -
    '---      - from restoring old columns widths (4000) for the columns-
    '==     ProblemLong, ServiceNotes or SessionTimes or Notifications..
    '==     -- The trigger will abort ALTER TABLE Alter Column on the Jobs Table if  
    '==               The attempted new width is less than 5000..
    '==
    '= = = = = = = == = = = = = == = = = = = = = = = = = = = = = = = = = = = = = == = 

    Private Const k_trg_build_version_key As String = "trg_alter_table_build_version"

    '--  This must match SystemInfo Build no for the Trigger.
    '-- Update this for each change/update to the trigger.  (not for every new JobMatix build).
    Private mDecLatestBuild As Decimal = 3431.02  '-- Update this for each change/update to the trigger.
    '-- And each time the trigger is re-created, update the SystemInfo..
    '==  Trigger will be (re) Created if SystemInfo Trigger build is older..

    Private mSystemInfo1 As clsSystemInfo

    '= = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = = = = = = = == =


    Public Function gbCreateAlterTableTrigger(ByRef cnnSql As OleDb.OleDbConnection, _
                                                ByRef bTriggerWasUpdated As Boolean) As Boolean

        Dim sSqlTrg As String = ""
        Dim intAffected As Integer
        Dim sErrorMsg, s1, s2 As String
        Dim datatable1 As DataTable
        Dim bTriggerExists As Boolean = False
        Dim bTriggerUpdateNeeded As Boolean = False
        Dim decTriggerPreviousVersion As Decimal

        gbCreateAlterTableTrigger = False

        mSystemInfo1 = New clsSystemInfo(cnnSql)

        decTriggerPreviousVersion = -1

        If mSystemInfo1.contains(k_trg_build_version_key) Then
            s1 = mSystemInfo1.item(k_trg_build_version_key)
            If IsNumeric(s1) Then
                decTriggerPreviousVersion = CDec(s1)
                If (decTriggerPreviousVersion < mDecLatestBuild) Then
                    bTriggerUpdateNeeded = True   '--must create the trigger.
                End If
            End If
        Else  '- not there yet-
            bTriggerUpdateNeeded = True   '--must create the trigger.
        End If
        'sSqlTrg &= "IF EXISTS (SELECT * FROM sys.triggers " & vbCrLf
        'sSqlTrg &= "             WHERE [name] = 'trg_jobmatix_alter_table' AND [type] = 'TR') " & vbCrLf
        'sSqlTrg &= "BEGIN " & vbCrLf
        'sSqlTrg &= "       PRINT 'Yes Trigger exists..';" & vbCrLf
        'sSqlTrg &= "       DROP TRIGGER  [trg_jobmatix_alter_table] ON DATABASE ;  -- alter_table_log;  -- [dbo].[log];" & vbCrLf
        'sSqlTrg &= "   --       PRINT 'Trigger was dropped. ';" & vbCrLf
        'sSqlTrg &= "END " & vbCrLf
        'sSqlTrg &= "ELSE PRINT 'Trigger doesn''t exist..';" & vbCrLf

        sSqlTrg = "SELECT * FROM sys.triggers " & vbCrLf
        sSqlTrg &= "             WHERE ([name] = 'trg_jobmatix_alter_table') AND ([type] = 'TR') " & vbCrLf

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(cnnSql, datatable1, sSqlTrg) Then
            MessageBox.Show("ERROR- Failed to get Trigger defs. recordset.." & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf, _
                    "gbCreateAlterTableTrigger", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            bTriggerWasUpdated = False
            Exit Function
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '-- check if trigger exists..
        If (datatable1 IsNot Nothing) AndAlso (datatable1.Rows.Count > 0) Then
            bTriggerExists = True
        End If  '-nothing.-

        If (bTriggerExists) And (Not bTriggerUpdateNeeded) Then  '--all up to date..
            '-- nothing to do..
            gbCreateAlterTableTrigger = True
            bTriggerWasUpdated = False
            Exit Function
        End If  '-exists-

        '= --Now we NEED to (re) Create the Trigger-
        '--  First DROP the current one if exists..
        If (bTriggerExists) Then
            sSqlTrg = " DROP TRIGGER [trg_jobmatix_alter_table] ON DATABASE ; " & vbCrLf
            If gbExecuteCmd(cnnSql, sSqlTrg & vbCrLf, intAffected, sErrorMsg) Then
                '-ok-
            Else '--failed..-
                MessageBox.Show("ERROR- Failed to DROP old Trigger for ALTER TABLE.. " & vbCrLf & _
                                                 sErrorMsg & vbCrLf, "AlterTableTrigger", _
                                                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If  '-execute-
        End If  '-exists-

        '= --Now to (re)create the Trigger-
        sSqlTrg = " CREATE TRIGGER trg_jobmatix_alter_table " & vbCrLf
        sSqlTrg &= " ON database " & vbCrLf
        sSqlTrg &= " FOR ALTER_TABLE " & vbCrLf
        sSqlTrg &= " AS " & vbCrLf
        sSqlTrg &= "DECLARE @data XML; " & vbCrLf
        sSqlTrg &= "DECLARE @tsql_orig AS varchar(200); " & vbCrLf
        sSqlTrg &= "DECLARE @tsql AS varchar(200);" & vbCrLf
        sSqlTrg &= "DECLARE @rem AS varchar(200);" & vbCrLf
        sSqlTrg &= "DECLARE @tablename AS varchar(200);" & vbCrLf
        sSqlTrg &= "DECLARE @colname AS varchar(200);" & vbCrLf
        sSqlTrg &= "DECLARE @type AS varchar(200);" & vbCrLf
        sSqlTrg &= "DECLARE @resultdata AS varchar(200);" & vbCrLf
        sSqlTrg &= "DECLARE @errormsg AS varchar(500);" & vbCrLf
        sSqlTrg &= "DECLARE @ipos AS INTEGER;" & vbCrLf
        sSqlTrg &= "DECLARE @size AS INTEGER;" & vbCrLf

        sSqlTrg &= "SET @data = EVENTDATA(); " & vbCrLf
        sSqlTrg &= "SET @tsql_orig = @data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 'nvarchar(2000)') ;" & vbCrLf
        sSqlTrg &= "-- PRINT 'Original SQL is: ' + @tsql_orig " & vbCrLf
        sSqlTrg &= "SET @tsql_orig = UPPER(@tsql_orig) " & vbCrLf
        sSqlTrg &= "--  Parse for column name and varchar --" & vbCrLf
        sSqlTrg &= "--  first replace multiple spaces with one space. " & vbCrLf
        sSqlTrg &= "WHILE (CHARINDEX('  ', @tsql_orig) >0) " & vbCrLf
        sSqlTrg &= "  BEGIN " & vbCrLf
        sSqlTrg &= "      SET @tsql_orig=REPLACE (@tsql_orig, '  ',' '); " & vbCrLf
        sSqlTrg &= "  END; " & vbCrLf

        sSqlTrg &= "-- LOSE all the square brackets.. " & vbCrLf
        sSqlTrg &= "SET @tsql=REPLACE (@tsql_orig, '[',''); " & vbCrLf
        sSqlTrg &= "SET @tsql=REPLACE (@tsql, ']',''); " & vbCrLf
        sSqlTrg &= " " & vbCrLf
        sSqlTrg &= "--  Check for Jobs Table..   " & vbCrLf
        sSqlTrg &= "SET @ipos= CHARINDEX('ALTER TABLE', @tsql); " & vbCrLf
        sSqlTrg &= "IF (@ipos <=0) BEGIN  " & vbCrLf
        sSqlTrg &= "               PRINT 'Rem with ALTER table is: ' + @tsql " & vbCrLf
        sSqlTrg &= "               RETURN END   --  no alter table. no problem. " & vbCrLf
        sSqlTrg &= "-- Get to the table name. " & vbCrLf
        sSqlTrg &= "-- drop alter TABLE " & vbCrLf
        sSqlTrg &= "SET @tsql = RTRIM(LTRIM(SUBSTRING(@tsql ,@ipos+11, 100))) ;  --  Lose alter table.. " & vbCrLf
        sSqlTrg &= "-- Extract Table name.. " & vbCrLf
        sSqlTrg &= "SET @ipos= CHARINDEX(' ', @tsql);  -- look for end " & vbCrLf
        sSqlTrg &= "IF (@ipos<=0) BEGIN  " & vbCrLf
        sSqlTrg &= "               -- PRINT 'Rem with table is: ' + @tsql " & vbCrLf
        sSqlTrg &= "             RETURN END;   --  no sep.  so no varchar.. " & vbCrLf
        sSqlTrg &= "SET @tablename=SUBSTRING(@tsql ,1,@ipos-1); " & vbCrLf
        sSqlTrg &= "-- get remainder after table name- " & vbCrLf
        sSqlTrg &= "SET @tsql = RTRIM( LTRIM ( SUBSTRING(@tsql ,@ipos+1, 100))) ;  --  Lose table name.. " & vbCrLf
        sSqlTrg &= "-- PRINT 'Table Name is <<'  + @tablename + '>>';  -- TEST- " & vbCrLf
        sSqlTrg &= " " & vbCrLf
        sSqlTrg &= "IF LEFT(@tablename,4)= 'DBO.' " & vbCrLf
        sSqlTrg &= "   BEGIN SET @tablename= SUBSTRING(@tablename ,5,100); END " & vbCrLf
        sSqlTrg &= "-- PRINT 'Table Name is <<'  + @tablename + '>>';  -- TEST- " & vbCrLf
        sSqlTrg &= "IF (@tablename <> 'JOBS') BEGIN RETURN END;    -- We only want Jobs table. " & vbCrLf
        sSqlTrg &= " " & vbCrLf
        sSqlTrg &= "-- Now Check for column.. " & vbCrLf
        sSqlTrg &= "SET @ipos= CHARINDEX('ALTER COLUMN', @tsql); " & vbCrLf
        sSqlTrg &= "IF (@ipos >0) " & vbCrLf
        sSqlTrg &= "BEGIN " & vbCrLf
        sSqlTrg &= "   -- drop alter column " & vbCrLf
        sSqlTrg &= "   SET @rem= RTRIM( LTRIM ( SUBSTRING(@tsql ,@ipos+12, 100))) ;  --  want col.name. " & vbCrLf
        sSqlTrg &= "   -- SET @resultdata = LTRIM(@rem)  --  just to see it. " & vbCrLf
        sSqlTrg &= "   -- Extract col name.. " & vbCrLf
        sSqlTrg &= "   SET @ipos= CHARINDEX(' ', @rem); " & vbCrLf
        sSqlTrg &= "   IF (@ipos >0) " & vbCrLf
        sSqlTrg &= "      BEGIN " & vbCrLf
        sSqlTrg &= "        SET @colname=SUBSTRING(@rem ,1,@ipos-1); " & vbCrLf
        sSqlTrg &= "        SET @resultdata = @colname ;  --  just to see it. " & vbCrLf
        sSqlTrg &= "        -- PRINT 'ColName is <<'  + @colname + '>>';   -- TEST - " & vbCrLf
        sSqlTrg &= "        --  Only want certain columns- " & vbCrLf
        sSqlTrg &= "        IF (@colname<>'PROBLEMLONG') AND (@colname<>'SERVICENOTES') AND " & vbCrLf
        sSqlTrg &= "                 (@colname<>'SESSIONTIMES') AND (@colname<>'NOTIFICATIONS') " & vbCrLf
        sSqlTrg &= "             BEGIN  RETURN  END; " & vbCrLf
        sSqlTrg &= "        -- Isolate data type (ie varchar(n) " & vbCrLf
        sSqlTrg &= "        -- get remainder after column name- " & vbCrLf
        sSqlTrg &= "        SET @type  = RTRIM( LTRIM ( SUBSTRING(@rem ,@ipos+1, 100))) ;  --  Lose table name.. " & vbCrLf
        sSqlTrg &= "        -- PRINT 'ColName/Type are <<'  + @colname + '/' + @type + '>>';   -- TEST - " & vbCrLf
        sSqlTrg &= "        --  Drop all spaces- " & vbCrLf
        sSqlTrg &= "        SET @type=REPLACE (@type, ' ',''); " & vbCrLf
        sSqlTrg &= "        IF (LEFT(@type,7)='VARCHAR') -- now is : 'VARCHAR(n)' " & vbCrLf
        sSqlTrg &= "           BEGIN SET @type= SUBSTRING(@type ,8,100);  " & vbCrLf
        sSqlTrg &= "                 -- PRINT @type -- now is : '(n)' " & vbCrLf
        sSqlTrg &= "                 -- GET size from inseide brackets.. " & vbCrLf
        sSqlTrg &= "                 IF (LEFT(@type,1)='(') " & vbCrLf
        sSqlTrg &= "                   BEGIN " & vbCrLf
        sSqlTrg &= "                     SET @ipos= CHARINDEX(')', @type); " & vbCrLf
        sSqlTrg &= "                     IF (@ipos >2) " & vbCrLf
        sSqlTrg &= "                       BEGIN " & vbCrLf
        sSqlTrg &= "                         SET @type = RTRIM(LTRIM(SUBSTRING(@type ,2,@ipos-2))) ;  " & vbCrLf
        sSqlTrg &= "                          -- Now Lost brackets... " & vbCrLf
        sSqlTrg &= "                          -- PRINT @type " & vbCrLf
        sSqlTrg &= "                          IF ISNUMERIC(@type)=1 " & vbCrLf
        sSqlTrg &= "                            BEGIN " & vbCrLf
        sSqlTrg &= "                              -- PRINT 'And Is Numeric is true' " & vbCrLf
        sSqlTrg &= "                              SET @size= CAST(@type AS int); " & vbCrLf
        sSqlTrg &= "                              --  NOW the test..  Trying to set varchar(4000) is refused (old version). " & vbCrLf
        sSqlTrg &= "                              IF (@size<5000) AND (@size>0) " & vbCrLf
        sSqlTrg &= "                              BEGIN " & vbCrLf
        sSqlTrg &= "                                 SET @errormsg= 'ERROR: ' + CHAR(13) +" & _
                                                           "'This version of JobMatix is not current for this Database Version. '" & _
                                                           "+ CHAR(13) + ' It is recommended to use latest version..' + CHAR(13); " & vbCrLf
        sSqlTrg &= "                                 RAISERROR (@errormsg, 18,1); " & vbCrLf
        sSqlTrg &= "                                 ROLLBACK; " & vbCrLf
        sSqlTrg &= "                              END; " & vbCrLf
        sSqlTrg &= "                            END;  -- numeric- " & vbCrLf
        sSqlTrg &= "                       END; " & vbCrLf
        sSqlTrg &= "                   END; " & vbCrLf
        sSqlTrg &= "           END;  -- varchar- " & vbCrLf
        sSqlTrg &= "      END;  -- colname- " & vbCrLf
        sSqlTrg &= "   END;  -- -alter column- " & vbCrLf
        sSqlTrg &= " " & vbCrLf

        If gbExecuteCmd(cnnSql, sSqlTrg & vbCrLf, intAffected, sErrorMsg) Then
            gbCreateAlterTableTrigger = True
            bTriggerWasUpdated = True
            '== MsgBox "RE-lOADED function JT2_ChargeableHours ok..", vbInformation
        Else '--failed..-
            MessageBox.Show("ERROR- Failed to create Trigger for ALTER TABLE.. " & vbCrLf & _
                                             sErrorMsg & vbCrLf, "AlterTableTrigger", _
                                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Function
        End If

        '--ok-  Update systemInfo..
        If mSystemInfo1.UpdateSystemInfo(New Object() {k_trg_build_version_key, _
                                                           CStr(mDecLatestBuild)}) Then '--ok-
            '= MsgBox("Credentials were updated ok..", MsgBoxStyle.Information)
            gbCreateAlterTableTrigger = True
        End If '--update-

    End Function  '-create-
    '= = = = = = = = = == = == = 


End Module  '-modAlterTableTrigger-
'= = = = = = =  = = = == = = = =
