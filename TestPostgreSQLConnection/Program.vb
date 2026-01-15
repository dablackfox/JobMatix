Option Strict Off
Option Explicit On

Imports System
Imports System.Data
Imports Npgsql

'=========================================================================
'== PostgreSQL Connection Test Application
'== Tests basic connectivity to PostgreSQL Docker container
'== Created: 2026-01-15
'=========================================================================

Module TestPostgreSQLConnection

    Sub Main()
        Console.WriteLine("==============================================")
        Console.WriteLine("JobMatix PostgreSQL Connection Test")
        Console.WriteLine("==============================================")
        Console.WriteLine()

        ' Connection parameters
        Dim host As String = "localhost"
        Dim port As String = "5432"
        Dim database As String = "jobmatix_main"
        Dim username As String = "jobmatix_user"
        Dim password As String = "JobMatix2026!Dev"

        ' Build connection string
        Dim connString As String = String.Format( _
            "Host={0};Port={1};Database={2};Username={3};Password={4};Pooling=true;Maximum Pool Size=20;", _
            host, port, database, username, password)

        Console.WriteLine("Connection String:")
        Console.WriteLine(connString.Replace(password, "********"))
        Console.WriteLine()

        ' Test 1: Basic Connection
        Console.WriteLine("Test 1: Basic Connection")
        Console.WriteLine("------------------------")
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Console.WriteLine("✓ Connected successfully!")
                Console.WriteLine("  Database: " & conn.Database)
                Console.WriteLine("  PostgreSQL Version: " & conn.PostgreSqlVersion.ToString())
                Console.WriteLine("  Host: " & conn.Host)
                Console.WriteLine()
            End Using
        Catch ex As Exception
            Console.WriteLine("✗ Connection failed!")
            Console.WriteLine("  Error: " & ex.Message)
            Console.WriteLine()
            Console.ReadLine()
            Return
        End Try

        ' Test 2: Query Execution
        Console.WriteLine("Test 2: Query Execution (SELECT version())")
        Console.WriteLine("------------------------------------------")
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Using cmd As New NpgsqlCommand("SELECT version()", conn)
                    Dim version As String = cmd.ExecuteScalar().ToString()
                    Console.WriteLine("✓ Query executed successfully!")
                    Console.WriteLine("  " & version)
                    Console.WriteLine()
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("✗ Query failed!")
            Console.WriteLine("  Error: " & ex.Message)
            Console.WriteLine()
        End Try

        ' Test 3: Read from system_info table
        Console.WriteLine("Test 3: Read from system_info table")
        Console.WriteLine("------------------------------------")
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Using cmd As New NpgsqlCommand("SELECT * FROM system_info ORDER BY info_key", conn)
                    Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                        Console.WriteLine("✓ Table query successful!")
                        Console.WriteLine()
                        Console.WriteLine("  Key                  | Value")
                        Console.WriteLine("  " & New String("-"c, 70))
                        
                        While reader.Read()
                            Console.WriteLine("  {0,-20} | {1}", _
                                reader("info_key").ToString(), _
                                reader("info_value").ToString())
                        End While
                        Console.WriteLine()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("✗ Table read failed!")
            Console.WriteLine("  Error: " & ex.Message)
            Console.WriteLine()
        End Try

        ' Test 4: INSERT operation
        Console.WriteLine("Test 4: INSERT Operation")
        Console.WriteLine("------------------------")
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                
                Dim testKey As String = "test_connection_" & DateTime.Now.ToString("yyyyMMddHHmmss")
                Dim testValue As String = "Connection test successful at " & DateTime.Now.ToString()
                
                Dim sql As String = "INSERT INTO system_info (info_key, info_value) VALUES (@key, @value)"
                Using cmd As New NpgsqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", testKey)
                    cmd.Parameters.AddWithValue("@value", testValue)
                    
                    Dim rows As Integer = cmd.ExecuteNonQuery()
                    Console.WriteLine("✓ INSERT successful!")
                    Console.WriteLine("  Rows affected: " & rows)
                    Console.WriteLine("  Inserted key: " & testKey)
                    Console.WriteLine()
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("✗ INSERT failed!")
            Console.WriteLine("  Error: " & ex.Message)
            Console.WriteLine()
        End Try

        ' Test 5: UPDATE operation
        Console.WriteLine("Test 5: UPDATE Operation")
        Console.WriteLine("------------------------")
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                
                Dim sql As String = "UPDATE system_info SET info_value = @value, updated_at = CURRENT_TIMESTAMP WHERE info_key = @key"
                Using cmd As New NpgsqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@key", "migration_status")
                    cmd.Parameters.AddWithValue("@value", "connection_test_completed")
                    
                    Dim rows As Integer = cmd.ExecuteNonQuery()
                    Console.WriteLine("✓ UPDATE successful!")
                    Console.WriteLine("  Rows affected: " & rows)
                    Console.WriteLine()
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("✗ UPDATE failed!")
            Console.WriteLine("  Error: " & ex.Message)
            Console.WriteLine()
        End Try

        ' Test 6: Transaction
        Console.WriteLine("Test 6: Transaction (with ROLLBACK)")
        Console.WriteLine("------------------------------------")
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                
                Using trans As NpgsqlTransaction = conn.BeginTransaction()
                    Try
                        Dim sql As String = "INSERT INTO system_info (info_key, info_value) VALUES (@key, @value)"
                        Using cmd As New NpgsqlCommand(sql, conn, trans)
                            cmd.Parameters.AddWithValue("@key", "temp_transaction_test")
                            cmd.Parameters.AddWithValue("@value", "This will be rolled back")
                            cmd.ExecuteNonQuery()
                        End Using
                        
                        ' Rollback transaction
                        trans.Rollback()
                        Console.WriteLine("✓ Transaction and ROLLBACK successful!")
                        Console.WriteLine("  (Temporary data was not committed)")
                        Console.WriteLine()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine("✗ Transaction failed!")
            Console.WriteLine("  Error: " & ex.Message)
            Console.WriteLine()
        End Try

        ' Summary
        Console.WriteLine("==============================================")
        Console.WriteLine("All tests completed!")
        Console.WriteLine("==============================================")
        Console.WriteLine()
        Console.WriteLine("PostgreSQL connection is working correctly.")
        Console.WriteLine("Press any key to exit...")
        Console.ReadLine()
    End Sub

End Module
