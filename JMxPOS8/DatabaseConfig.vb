Imports System
Imports System.Configuration
Imports System.IO

' =====================================================================
' DatabaseConfig - Centralized Database Configuration Module
' =====================================================================
' This module provides centralized configuration for database connections.
' Set UseSqlServer = False to use PostgreSQL
' Set UseSqlServer = True to use Microsoft SQL Server (original)
' =====================================================================

Public Module DatabaseConfig

    ' =====================================================================
    ' CONFIGURATION FLAGS
    ' =====================================================================
    
    ''' <summary>
    ''' Set to False to use PostgreSQL, True to use SQL Server
    ''' </summary>
    Public UseSqlServer As Boolean = False
    
    ''' <summary>
    ''' Automatically convert SQL syntax when using PostgreSQL
    ''' </summary>
    Public AutoConvertSql As Boolean = True
    
    ' =====================================================================
    ' SQL SERVER CONFIGURATION (Original)
    ' =====================================================================
    
    Public SqlServerHost As String = ".\SQLEXPRESS"
    Public SqlServerUser As String = ""  ' Empty for Windows Authentication
    Public SqlServerPassword As String = ""
    Public SqlServerMainDb As String = "JobMatix"
    Public SqlServerJobsDb As String = "Jobs"
    Public SqlServerPosDb As String = "POSdb"
    
    ' =====================================================================
    ' POSTGRESQL CONFIGURATION (New)
    ' =====================================================================
    
    Public PostgreSqlHost As String = "localhost"
    Public PostgreSqlPort As Integer = 5432
    Public PostgreSqlUser As String = "jobmatix_user"
    Public PostgreSqlPassword As String = "JobMatix2026!Dev"
    Public PostgreSqlMainDb As String = "jobmatix_main"
    Public PostgreSqlJobsDb As String = "jobmatix_jobs"
    Public PostgreSqlPosDb As String = "jobmatix_pos"
    Public PostgreSqlBackupDb As String = "jobmatix_backup"
    
    ' =====================================================================
    ' CONNECTION STRING BUILDERS
    ' =====================================================================
    
    ''' <summary>
    ''' Gets the connection string for the main database
    ''' </summary>
    Public Function GetMainConnectionString() As String
        If UseSqlServer Then
            Return GetSqlServerConnectionString(SqlServerMainDb)
        Else
            Return GetPostgreSqlConnectionString(PostgreSqlMainDb)
        End If
    End Function
    
    ''' <summary>
    ''' Gets the connection string for the Jobs database
    ''' </summary>
    Public Function GetJobsConnectionString() As String
        If UseSqlServer Then
            Return GetSqlServerConnectionString(SqlServerJobsDb)
        Else
            Return GetPostgreSqlConnectionString(PostgreSqlJobsDb)
        End If
    End Function
    
    ''' <summary>
    ''' Gets the connection string for the POS database
    ''' </summary>
    Public Function GetPosConnectionString() As String
        If UseSqlServer Then
            Return GetSqlServerConnectionString(SqlServerPosDb)
        Else
            Return GetPostgreSqlConnectionString(PostgreSqlPosDb)
        End If
    End Function
    
    ''' <summary>
    ''' Gets a SQL Server connection string for the specified database
    ''' </summary>
    Private Function GetSqlServerConnectionString(ByVal databaseName As String) As String
        Dim connStr As String = "Provider=SQLOLEDB; Server=" & SqlServerHost & "; "
        
        If String.IsNullOrEmpty(SqlServerUser) Then
            ' Windows Authentication
            connStr &= "Trusted_Connection=true; Integrated Security=SSPI; "
        Else
            ' SQL Server Authentication
            connStr &= "User ID=" & SqlServerUser & "; Password=" & SqlServerPassword & "; "
        End If
        
        connStr &= "Initial Catalog=" & databaseName & ";"
        
        Return connStr
    End Function
    
    ''' <summary>
    ''' Gets a PostgreSQL connection string for the specified database
    ''' </summary>
    Private Function GetPostgreSqlConnectionString(ByVal databaseName As String) As String
        Return String.Format( _
            "Host={0};Port={1};Database={2};Username={3};Password={4};", _
            PostgreSqlHost, _
            PostgreSqlPort, _
            databaseName, _
            PostgreSqlUser, _
            PostgreSqlPassword)
    End Function
    
    ''' <summary>
    ''' Load configuration from environment variables or config file
    ''' Call this at application startup
    ''' </summary>
    Public Sub LoadConfiguration()
        ' Try to load from environment variables first
        Dim envDbType As String = Environment.GetEnvironmentVariable("JOBMATIX_DB_TYPE")
        
        If Not String.IsNullOrEmpty(envDbType) Then
            UseSqlServer = (envDbType.ToUpper() = "SQLSERVER")
        End If
        
        ' PostgreSQL connection settings from environment
        Dim envPgHost As String = Environment.GetEnvironmentVariable("JOBMATIX_PG_HOST")
        If Not String.IsNullOrEmpty(envPgHost) Then PostgreSqlHost = envPgHost
        
        Dim envPgPort As String = Environment.GetEnvironmentVariable("JOBMATIX_PG_PORT")
        If Not String.IsNullOrEmpty(envPgPort) Then PostgreSqlPort = Integer.Parse(envPgPort)
        
        Dim envPgUser As String = Environment.GetEnvironmentVariable("JOBMATIX_PG_USER")
        If Not String.IsNullOrEmpty(envPgUser) Then PostgreSqlUser = envPgUser
        
        Dim envPgPass As String = Environment.GetEnvironmentVariable("JOBMATIX_PG_PASSWORD")
        If Not String.IsNullOrEmpty(envPgPass) Then PostgreSqlPassword = envPgPass
        
        ' Try to load from .env file if it exists
        LoadFromEnvFile()
    End Sub
    
    ''' <summary>
    ''' Load configuration from .env file
    ''' </summary>
    Private Sub LoadFromEnvFile()
        Try
            Dim envPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env")
            
            If File.Exists(envPath) Then
                Dim lines() As String = File.ReadAllLines(envPath)
                
                For Each line As String In lines
                    If String.IsNullOrWhiteSpace(line) OrElse line.Trim().StartsWith("#") Then
                        Continue For
                    End If
                    
                    Dim parts() As String = line.Split("="c)
                    If parts.Length = 2 Then
                        Dim key As String = parts(0).Trim()
                        Dim value As String = parts(1).Trim()
                        
                        Select Case key
                            Case "JOBMATIX_DB_TYPE"
                                UseSqlServer = (value.ToUpper() = "SQLSERVER")
                            Case "JOBMATIX_PG_HOST"
                                PostgreSqlHost = value
                            Case "JOBMATIX_PG_PORT"
                                PostgreSqlPort = Integer.Parse(value)
                            Case "JOBMATIX_PG_USER"
                                PostgreSqlUser = value
                            Case "JOBMATIX_PG_PASSWORD"
                                PostgreSqlPassword = value
                            Case "JOBMATIX_PG_DB_MAIN"
                                PostgreSqlMainDb = value
                            Case "JOBMATIX_PG_DB_JOBS"
                                PostgreSqlJobsDb = value
                            Case "JOBMATIX_PG_DB_POS"
                                PostgreSqlPosDb = value
                        End Select
                    End If
                Next
            End If
        Catch ex As Exception
            ' Silently fail if .env file doesn't exist or can't be read
            ' Will use default values
        End Try
    End Sub
    
    ''' <summary>
    ''' Gets the current database type as a string
    ''' </summary>
    Public Function GetDatabaseType() As String
        Return If(UseSqlServer, "SQL Server", "PostgreSQL")
    End Function
    
    ''' <summary>
    ''' Displays current configuration (for debugging)
    ''' </summary>
    Public Function GetConfigSummary() As String
        Dim summary As String = "Database Configuration:" & vbCrLf
        summary &= "----------------------" & vbCrLf
        summary &= "Database Type: " & GetDatabaseType() & vbCrLf
        summary &= "Auto Convert SQL: " & AutoConvertSql.ToString() & vbCrLf
        
        If UseSqlServer Then
            summary &= "SQL Server Host: " & SqlServerHost & vbCrLf
            summary &= "Main Database: " & SqlServerMainDb & vbCrLf
            summary &= "Jobs Database: " & SqlServerJobsDb & vbCrLf
            summary &= "POS Database: " & SqlServerPosDb & vbCrLf
        Else
            summary &= "PostgreSQL Host: " & PostgreSqlHost & vbCrLf
            summary &= "PostgreSQL Port: " & PostgreSqlPort.ToString() & vbCrLf
            summary &= "PostgreSQL User: " & PostgreSqlUser & vbCrLf
            summary &= "Main Database: " & PostgreSqlMainDb & vbCrLf
            summary &= "Jobs Database: " & PostgreSqlJobsDb & vbCrLf
            summary &= "POS Database: " & PostgreSqlPosDb & vbCrLf
        End If
        
        Return summary
    End Function

End Module
