Option Strict Off
Option Explicit On

Imports System
Imports System.Collections
Imports System.Data
Imports Npgsql

'=========================================================================
'== modPostgreSqlSupport.vb
'== PostgreSQL-specific database functions
'== Created: 2026-01-15
'==
'== Purpose: Provides PostgreSQL versions of core SQL functions from
'==          modAllFileAndSqlSubs.vb. These mirror the OleDb functions
'==          but use Npgsql for PostgreSQL connectivity.
'=========================================================================

Module modPostgreSqlSupport

    '== Last error message
    Public msLastPgErrorMessage As String = ""

    '=========================================================================
    '== CONNECTION
    '=========================================================================

    '== Connect to PostgreSQL database
    Public Function gbConnectPostgreSql(ByRef cnnPG As NpgsqlConnection, _
                                       ByVal sConnect As String) As Boolean

        Dim msg, s2 As String

        msLastPgErrorMessage = ""
        gbConnectPostgreSql = False
        If (cnnPG Is Nothing) Then cnnPG = New NpgsqlConnection

        Try
            cnnPG.ConnectionString = sConnect
            cnnPG.Open()
            msg = "Connected ok to PostgreSQL database.." & vbCrLf
            msg = msg & "   ConnectStr.=" & sConnect & vbCrLf
            msg = msg & "   PostgreSQL Version: " & cnnPG.PostgreSqlVersion.ToString() & vbCrLf
            gbConnectPostgreSql = True

        Catch ex As Exception
            msg = "Failed Connect to PostgreSQL Server.." & vbCrLf
            msg = msg & "Error: " & ex.Message & vbCrLf
            msg = msg & "connect string=<" & sConnect & ">"
            s2 = msg & vbCrLf
            msLastPgErrorMessage = s2
            If (gsErrorLogPath() <> "") Then
                Call gbLogMsg(gsErrorLogPath, s2 & vbCrLf & "-- end of error msg.--")
            End If

        End Try
    End Function

    '=========================================================================
    '== EXECUTE COMMANDS
    '=========================================================================

    '== Execute non-query command (INSERT, UPDATE, DELETE)
    Public Function gbExecutePostgreSqlCmd(ByRef cnnPG As NpgsqlConnection, _
                                          ByVal sSql As String, _
                                          ByRef lAffected As Integer, _
                                          ByRef sErrorMsg As String) As Boolean
        Dim cmd1 As NpgsqlCommand
        Dim sMsg As String

        msLastPgErrorMessage = ""
        gbExecutePostgreSqlCmd = False
        lAffected = 0

        Try
            cmd1 = New NpgsqlCommand(sSql, cnnPG)
            cmd1.CommandTimeout = 60
            lAffected = cmd1.ExecuteNonQuery()
            gbExecutePostgreSqlCmd = True

        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbExecutePostgreSqlCmd: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                      "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastPgErrorMessage = sErrorMsg
        End Try

    End Function

    '=========================================================================
    '== SCALAR QUERIES
    '=========================================================================

    '== Get scalar value (any type)
    Public Function gbGetPostgreSqlScalarValue(ByRef cnnPG As NpgsqlConnection, _
                                              ByVal sSqlSelect As String, _
                                              ByRef objResult As Object) As Boolean
        Dim cmd1 As NpgsqlCommand
        Dim sMsg, sErrorMsg As String

        gbGetPostgreSqlScalarValue = False
        msLastPgErrorMessage = ""

        Try
            cmd1 = New NpgsqlCommand(sSqlSelect, cnnPG)
            objResult = cmd1.ExecuteScalar
            gbGetPostgreSqlScalarValue = True

        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbGetPostgreSqlScalarValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSqlSelect & vbCrLf & _
                      "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastPgErrorMessage = sErrorMsg
        End Try
    End Function

    '== Get scalar INTEGER value
    Public Function gbGetPostgreSqlScalarIntegerValue(ByRef cnnPG As NpgsqlConnection, _
                                                     ByVal sSql As String, _
                                                     ByRef intResult As Integer) As Boolean
        Dim cmd1 As NpgsqlCommand
        Dim sMsg, sErrorMsg As String

        gbGetPostgreSqlScalarIntegerValue = False
        msLastPgErrorMessage = ""

        Try
            cmd1 = New NpgsqlCommand(sSql, cnnPG)
            intResult = CInt(cmd1.ExecuteScalar())
            gbGetPostgreSqlScalarIntegerValue = True

        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbGetPostgreSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                      "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastPgErrorMessage = sErrorMsg
        End Try
    End Function

    '== Get scalar STRING value
    Public Function gbGetPostgreSqlScalarStringValue(ByRef cnnPG As NpgsqlConnection, _
                                                    ByVal sSql As String, _
                                                    ByRef sResult As String) As Boolean
        Dim cmd1 As NpgsqlCommand
        Dim sMsg, sErrorMsg As String
        Dim objResult As Object

        gbGetPostgreSqlScalarStringValue = False
        msLastPgErrorMessage = ""
        sResult = ""

        Try
            cmd1 = New NpgsqlCommand(sSql, cnnPG)
            objResult = cmd1.ExecuteScalar()
            If objResult IsNot Nothing Then
                sResult = objResult.ToString()
            End If
            gbGetPostgreSqlScalarStringValue = True

        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbGetPostgreSqlScalarStringValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                      "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastPgErrorMessage = sErrorMsg
        End Try
    End Function

    '=========================================================================
    '== DATA READERS
    '=========================================================================

    '== Get data reader
    Public Function gbGetPostgreSqlReader(ByRef cnnPG As NpgsqlConnection, _
                                         ByRef rdr1 As NpgsqlDataReader, _
                                         ByVal sSql As String) As Boolean
        Dim cmd1 As NpgsqlCommand
        Dim sMsg, sErrorMsg As String

        gbGetPostgreSqlReader = False
        msLastPgErrorMessage = ""

        Try
            cmd1 = New NpgsqlCommand(sSql, cnnPG)
            rdr1 = cmd1.ExecuteReader()
            gbGetPostgreSqlReader = True

        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbGetPostgreSqlReader: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                      "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastPgErrorMessage = sErrorMsg
        End Try
    End Function

    '=========================================================================
    '== DATABASE MANAGEMENT
    '=========================================================================

    '== Set current database
    Public Function gbSetPostgreSqlCurrentDatabase(ByRef cnnPG As NpgsqlConnection, _
                                                  ByVal sDBName As String) As Boolean
        Dim sErrors, sMsg As String

        Try
            gbSetPostgreSqlCurrentDatabase = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            ' In PostgreSQL, we need to reconnect with the new database
            Dim originalConnString As String = cnnPG.ConnectionString
            Dim builder As New NpgsqlConnectionStringBuilder(originalConnString)
            builder.Database = sDBName

            ' Close current connection
            If cnnPG.State = ConnectionState.Open Then
                cnnPG.Close()
            End If

            ' Reconnect with new database
            cnnPG.ConnectionString = builder.ToString()
            cnnPG.Open()

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            gbSetPostgreSqlCurrentDatabase = True

        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(gsErrorLogPath, "= Failed in Change-DATABASE: " & sDBName & vbCrLf & _
                                         ex.Message & vbCrLf & "-- end of error msg.--")
            MsgBox("= Failed to switch to DATABASE: " & sDBName & " = =" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function

    '== Test if table exists
    Public Function gbPostgreSqlTableExists(ByRef cnnPG As NpgsqlConnection, _
                                           ByVal sTableName As String) As Boolean
        Dim sSql As String
        Dim rdr1 As NpgsqlDataReader = Nothing
        Dim iCount As Integer = 0

        gbPostgreSqlTableExists = False

        Try
            ' In PostgreSQL, table names are case-insensitive unless quoted
            ' Check in information_schema
            sSql = "SELECT COUNT(*) FROM information_schema.tables " & _
                  "WHERE table_schema = 'public' AND " & _
                  "LOWER(table_name) = LOWER('" & sTableName & "')"

            Dim objResult As Object = Nothing
            If gbGetPostgreSqlScalarValue(cnnPG, sSql, objResult) Then
                iCount = CInt(objResult)
                If iCount > 0 Then
                    gbPostgreSqlTableExists = True
                End If
            End If

        Catch ex As Exception
            Call gbLogMsg(gsErrorLogPath, "Error checking if table exists: " & sTableName & vbCrLf & _
                                         ex.Message)
        End Try
    End Function

    '=========================================================================
    '== TRANSACTIONS
    '=========================================================================

    '== Execute command with transaction
    Public Function gbExecutePostgreSqlCmdTrans(ByRef cnnPG As NpgsqlConnection, _
                                               ByVal sSql As String, _
                                               ByRef lAffected As Integer, _
                                               ByRef sErrorMsg As String, _
                                               ByRef trans As NpgsqlTransaction) As Boolean
        Dim cmd1 As NpgsqlCommand
        Dim sMsg As String

        msLastPgErrorMessage = ""
        gbExecutePostgreSqlCmdTrans = False
        lAffected = 0

        Try
            cmd1 = New NpgsqlCommand(sSql, cnnPG, trans)
            cmd1.CommandTimeout = 60
            lAffected = cmd1.ExecuteNonQuery()
            gbExecutePostgreSqlCmdTrans = True

        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbExecutePostgreSqlCmdTrans: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                      "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastPgErrorMessage = sErrorMsg
        End Try

    End Function

    '=========================================================================
    '== HELPER FUNCTIONS
    '=========================================================================

    '== Get last error message
    Public Function gsGetLastPostgreSqlErrorMessage() As String
        gsGetLastPostgreSqlErrorMessage = msLastPgErrorMessage
    End Function

    '== Clear last error
    Public Sub ClearLastPostgreSqlError()
        msLastPgErrorMessage = ""
    End Sub

End Module
'=========================================================================
'== End of modPostgreSqlSupport
'=========================================================================
