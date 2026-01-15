Option Strict Off
Option Explicit On

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports Npgsql

'=========================================================================
'== modDatabaseAbstraction.vb
'== Database Abstraction Layer for MSSQL to PostgreSQL Migration
'== Created: 2026-01-15
'== 
'== Purpose: Provides unified interface for both OleDb (MSSQL) and 
'==          Npgsql (PostgreSQL) to allow gradual migration
'=========================================================================

Module modDatabaseAbstraction

    '== Database Type Enumeration
    Public Enum DatabaseType
        MSSQL = 1
        PostgreSQL = 2
    End Enum

    '== Current database type (can be set from config or runtime)
    Public gCurrentDatabaseType As DatabaseType = DatabaseType.MSSQL

    '== Connection Strings
    Public gMSSQLConnectionString As String = ""
    Public gPostgreSQLConnectionString As String = ""

    '=========================================================================
    '== UNIFIED CONNECTION INTERFACE
    '=========================================================================

    '== Get connection based on current database type
    Public Function GetDatabaseConnection() As IDbConnection
        Select Case gCurrentDatabaseType
            Case DatabaseType.MSSQL
                Return New OleDbConnection(gMSSQLConnectionString)
            Case DatabaseType.PostgreSQL
                Return New NpgsqlConnection(gPostgreSQLConnectionString)
            Case Else
                Throw New Exception("Unknown database type: " & gCurrentDatabaseType.ToString())
        End Select
    End Function

    '== Create command for current database
    Public Function CreateCommand(ByVal sql As String, ByVal conn As IDbConnection) As IDbCommand
        Select Case gCurrentDatabaseType
            Case DatabaseType.MSSQL
                Return New OleDbCommand(sql, DirectCast(conn, OleDbConnection))
            Case DatabaseType.PostgreSQL
                Return New NpgsqlCommand(sql, DirectCast(conn, NpgsqlConnection))
            Case Else
                Throw New Exception("Unknown database type")
        End Select
    End Function

    '== Create data adapter for current database
    Public Function CreateDataAdapter(ByVal sql As String, ByVal conn As IDbConnection) As IDbDataAdapter
        Select Case gCurrentDatabaseType
            Case DatabaseType.MSSQL
                Return New OleDbDataAdapter(sql, DirectCast(conn, OleDbConnection))
            Case DatabaseType.PostgreSQL
                Return New NpgsqlDataAdapter(sql, DirectCast(conn, NpgsqlConnection))
            Case Else
                Throw New Exception("Unknown database type")
        End Select
    End Function

    '== Create parameter for current database
    Public Function CreateParameter(ByVal paramName As String, ByVal value As Object) As IDbDataParameter
        Select Case gCurrentDatabaseType
            Case DatabaseType.MSSQL
                Return New OleDbParameter(paramName, value)
            Case DatabaseType.PostgreSQL
                Return New NpgsqlParameter(paramName, value)
            Case Else
                Throw New Exception("Unknown database type")
        End Select
    End Function

    '=========================================================================
    '== SQL SYNTAX CONVERSION
    '=========================================================================

    '== Convert SQL statement from MSSQL to PostgreSQL syntax
    Public Function ConvertSqlSyntax(ByVal mssqlSql As String) As String
        If gCurrentDatabaseType = DatabaseType.MSSQL Then
            ' No conversion needed
            Return mssqlSql
        End If

        Dim pgSql As String = mssqlSql

        ' Convert string concatenation: + to ||
        ' Note: This is a simple regex, may need refinement for complex cases
        ' pgSql = System.Text.RegularExpressions.Regex.Replace(pgSql, "'([^']*)'[\s]*\+[\s]*'([^']*)'", "'$1' || '$2'")

        ' Convert TOP n to LIMIT n
        pgSql = System.Text.RegularExpressions.Regex.Replace(pgSql, _
            "SELECT\s+TOP\s+(\d+)", "SELECT", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        ' Add LIMIT at the end (simple approach)
        If System.Text.RegularExpressions.Regex.IsMatch(mssqlSql, "SELECT\s+TOP\s+(\d+)", _
            System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
            Dim match As System.Text.RegularExpressions.Match = _
                System.Text.RegularExpressions.Regex.Match(mssqlSql, "TOP\s+(\d+)", _
                System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            If match.Success Then
                Dim limit As String = match.Groups(1).Value
                If Not pgSql.ToUpper().Contains("LIMIT") Then
                    pgSql &= " LIMIT " & limit
                End If
            End If
        End If

        ' Convert GETDATE() to NOW()
        pgSql = System.Text.RegularExpressions.Regex.Replace(pgSql, _
            "GETDATE\s*\(\s*\)", "NOW()", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Convert ISNULL to COALESCE
        pgSql = System.Text.RegularExpressions.Regex.Replace(pgSql, _
            "ISNULL\s*\(", "COALESCE(", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Convert table names to lowercase (PostgreSQL convention)
        ' Note: May need more sophisticated handling for quoted identifiers
        
        ' Convert dbo. schema prefix
        pgSql = System.Text.RegularExpressions.Regex.Replace(pgSql, _
            "\[?dbo\]?\.", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Return pgSql
    End Function

    '== Convert CREATE TABLE syntax
    Public Function ConvertCreateTableSyntax(ByVal mssqlCreateTable As String) As String
        If gCurrentDatabaseType = DatabaseType.MSSQL Then
            Return mssqlCreateTable
        End If

        Dim pgCreate As String = mssqlCreateTable

        ' Convert IDENTITY to SERIAL
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "INT\s+IDENTITY\s*\(\s*\d+\s*,\s*\d+\s*\)", _
            "SERIAL", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Convert data types
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "nvarchar\s*\(\s*max\s*\)", "TEXT", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "varchar\s*\(\s*max\s*\)", "TEXT", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "nvarchar", "VARCHAR", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "\bBIT\b", "BOOLEAN", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "\bMONEY\b", "DECIMAL(19,4)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "\bdatetime\b", "TIMESTAMP", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Remove CLUSTERED keyword
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "\bCLUSTERED\b", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Remove dbo. prefix
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "CREATE\s+TABLE\s+\[?dbo\]?\.", "CREATE TABLE ", _
            System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        ' Convert DEFAULT 0 for BIT to DEFAULT FALSE for BOOLEAN
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "BOOLEAN\s+NOT\s+NULL\s+DEFAULT\s+0", _
            "BOOLEAN NOT NULL DEFAULT FALSE", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        
        pgCreate = System.Text.RegularExpressions.Regex.Replace(pgCreate, _
            "BOOLEAN\s+NOT\s+NULL\s+DEFAULT\s+1", _
            "BOOLEAN NOT NULL DEFAULT TRUE", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

        Return pgCreate
    End Function

    '=========================================================================
    '== UNIFIED EXECUTION FUNCTIONS
    '=========================================================================

    '== Execute non-query (INSERT, UPDATE, DELETE)
    Public Function ExecuteNonQuery(ByVal sql As String, _
                                   ByRef rowsAffected As Integer, _
                                   ByRef errorMsg As String) As Boolean
        ExecuteNonQuery = False
        errorMsg = ""
        rowsAffected = 0

        Try
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                
                ' Convert SQL if needed
                Dim convertedSql As String = ConvertSqlSyntax(sql)
                
                Using cmd As IDbCommand = CreateCommand(convertedSql, conn)
                    rowsAffected = cmd.ExecuteNonQuery()
                End Using
                
                ExecuteNonQuery = True
            End Using
        Catch ex As Exception
            errorMsg = "Error executing SQL: " & vbCrLf & ex.Message & vbCrLf & _
                      "SQL was: " & vbCrLf & sql
        End Try
    End Function

    '== Execute scalar (return single value)
    Public Function ExecuteScalar(ByVal sql As String, _
                                 ByRef result As Object, _
                                 ByRef errorMsg As String) As Boolean
        ExecuteScalar = False
        errorMsg = ""
        result = Nothing

        Try
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                
                Dim convertedSql As String = ConvertSqlSyntax(sql)
                
                Using cmd As IDbCommand = CreateCommand(convertedSql, conn)
                    result = cmd.ExecuteScalar()
                End Using
                
                ExecuteScalar = True
            End Using
        Catch ex As Exception
            errorMsg = "Error executing scalar SQL: " & vbCrLf & ex.Message & vbCrLf & _
                      "SQL was: " & vbCrLf & sql
        End Try
    End Function

    '== Execute reader (return data reader)
    Public Function ExecuteReader(ByVal sql As String, _
                                 ByRef reader As IDataReader, _
                                 ByRef errorMsg As String) As Boolean
        ExecuteReader = False
        errorMsg = ""

        Try
            Dim conn As IDbConnection = GetDatabaseConnection()
            conn.Open()
            
            Dim convertedSql As String = ConvertSqlSyntax(sql)
            
            Dim cmd As IDbCommand = CreateCommand(convertedSql, conn)
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            
            ExecuteReader = True
        Catch ex As Exception
            errorMsg = "Error executing reader SQL: " & vbCrLf & ex.Message & vbCrLf & _
                      "SQL was: " & vbCrLf & sql
        End Try
    End Function

    '== Fill DataTable
    Public Function FillDataTable(ByVal sql As String, _
                                 ByRef dataTable As DataTable, _
                                 ByRef errorMsg As String) As Boolean
        FillDataTable = False
        errorMsg = ""

        Try
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                
                Dim convertedSql As String = ConvertSqlSyntax(sql)
                
                Dim adapter As IDbDataAdapter = CreateDataAdapter(convertedSql, conn)
                
                If dataTable Is Nothing Then
                    dataTable = New DataTable()
                End If
                
                adapter.Fill(dataTable)
                FillDataTable = True
            End Using
        Catch ex As Exception
            errorMsg = "Error filling DataTable: " & vbCrLf & ex.Message & vbCrLf & _
                      "SQL was: " & vbCrLf & sql
        End Try
    End Function

    '=========================================================================
    '== CONFIGURATION HELPERS
    '=========================================================================

    '== Initialize from connection strings
    Public Sub Initialize(ByVal dbType As DatabaseType, _
                         ByVal mssqlConnStr As String, _
                         ByVal pgConnStr As String)
        gCurrentDatabaseType = dbType
        gMSSQLConnectionString = mssqlConnStr
        gPostgreSQLConnectionString = pgConnStr
    End Sub

    '== Test connection
    Public Function TestConnection(ByRef errorMsg As String) As Boolean
        TestConnection = False
        errorMsg = ""

        Try
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                
                ' Test with a simple query
                Using cmd As IDbCommand = CreateCommand("SELECT 1", conn)
                    cmd.ExecuteScalar()
                End Using
                
                TestConnection = True
            End Using
        Catch ex As Exception
            errorMsg = "Connection test failed: " & vbCrLf & ex.Message
        End Try
    End Function

    '== Get database version
    Public Function GetDatabaseVersion(ByRef version As String, _
                                      ByRef errorMsg As String) As Boolean
        GetDatabaseVersion = False
        errorMsg = ""
        version = ""

        Try
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                
                Dim sql As String = ""
                Select Case gCurrentDatabaseType
                    Case DatabaseType.MSSQL
                        sql = "SELECT @@VERSION"
                    Case DatabaseType.PostgreSQL
                        sql = "SELECT version()"
                End Select
                
                Using cmd As IDbCommand = CreateCommand(sql, conn)
                    version = cmd.ExecuteScalar().ToString()
                End Using
                
                GetDatabaseVersion = True
            End Using
        Catch ex As Exception
            errorMsg = "Error getting database version: " & vbCrLf & ex.Message
        End Try
    End Function

End Module
'=========================================================================
'== End of modDatabaseAbstraction
'=========================================================================
