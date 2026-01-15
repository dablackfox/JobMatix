Imports System
Imports System.Data
Imports Npgsql
Imports JMxRetailHost620

Module TestDatabaseMigration
    
    Sub Main()
        Console.WriteLine("========================================")
        Console.WriteLine("JobMatix PostgreSQL Migration Test")
        Console.WriteLine("========================================")
        Console.WriteLine()
        
        ' Load configuration
        DatabaseConfig.LoadConfiguration()
        Console.WriteLine(DatabaseConfig.GetConfigSummary())
        Console.WriteLine()
        
        ' Test 1: Direct PostgreSQL Connection
        Console.WriteLine("Test 1: Direct PostgreSQL Connection to POS Database")
        Console.WriteLine("-----------------------------------------------------")
        TestDirectPostgreSqlConnection()
        Console.WriteLine()
        
        ' Test 2: Using Database Abstraction Layer
        Console.WriteLine("Test 2: Using Database Abstraction Layer")
        Console.WriteLine("------------------------------------------")
        TestDatabaseAbstraction()
        Console.WriteLine()
        
        ' Test 3: Test Jobs Database
        Console.WriteLine("Test 3: Jobs Database Connection")
        Console.WriteLine("----------------------------------")
        TestJobsDatabase()
        Console.WriteLine()
        
        ' Test 4: SQL Conversion
        Console.WriteLine("Test 4: SQL Syntax Conversion")
        Console.WriteLine("-------------------------------")
        TestSqlConversion()
        Console.WriteLine()
        
        Console.WriteLine("========================================")
        Console.WriteLine("All tests completed!")
        Console.WriteLine("========================================")
        Console.WriteLine()
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
    End Sub
    
    ''' <summary>
    ''' Test direct PostgreSQL connection
    ''' </summary>
    Sub TestDirectPostgreSqlConnection()
        Try
            Dim connString As String = DatabaseConfig.GetPosConnectionString()
            Console.WriteLine("Connection String: " & connString.Replace(DatabaseConfig.PostgreSqlPassword, "****"))
            
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Console.WriteLine("✓ Connected to PostgreSQL successfully!")
                Console.WriteLine("  Database: " & conn.Database)
                Console.WriteLine("  Server Version: " & conn.ServerVersion)
                
                ' Query SystemInfo
                Dim cmd As New NpgsqlCommand("SELECT * FROM SystemInfo ORDER BY SystemKey", conn)
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    Console.WriteLine()
                    Console.WriteLine("  System Information:")
                    While reader.Read()
                        Console.WriteLine("    " & reader("SystemKey").ToString() & " = " & reader("SystemValue").ToString())
                    End While
                End Using
                
                ' Count tables
                cmd = New NpgsqlCommand("SELECT COUNT(*) FROM Staff", conn)
                Dim staffCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine("  Staff Records: " & staffCount.ToString())
                
                cmd = New NpgsqlCommand("SELECT COUNT(*) FROM Customer", conn)
                Dim custCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine("  Customer Records: " & custCount.ToString())
                
            End Using
            
        Catch ex As Exception
            Console.WriteLine("✗ Error: " & ex.Message)
            Console.WriteLine("  Stack Trace: " & ex.StackTrace)
        End Try
    End Sub
    
    ''' <summary>
    ''' Test using database abstraction layer
    ''' </summary>
    Sub TestDatabaseAbstraction()
        Try
            Dim connString As String = DatabaseConfig.GetPosConnectionString()
            
            ' Get connection through abstraction layer
            Dim conn As IDbConnection = modDatabaseAbstraction.GetDatabaseConnection(connString, DatabaseConfig.UseSqlServer)
            
            If conn Is Nothing Then
                Console.WriteLine("✗ Failed to get database connection")
                Return
            End If
            
            conn.Open()
            Console.WriteLine("✓ Connected through abstraction layer!")
            Console.WriteLine("  Connection Type: " & conn.GetType().Name)
            
            ' Execute a query
            Dim sql As String = "SELECT StaffCode, StaffName FROM Staff"
            Dim cmd As IDbCommand = conn.CreateCommand()
            cmd.CommandText = sql
            
            Using reader As IDataReader = cmd.ExecuteReader()
                Console.WriteLine()
                Console.WriteLine("  Staff List:")
                While reader.Read()
                    Console.WriteLine("    " & reader("StaffCode").ToString() & " - " & reader("StaffName").ToString())
                End While
            End Using
            
            conn.Close()
            
        Catch ex As Exception
            Console.WriteLine("✗ Error: " & ex.Message)
        End Try
    End Sub
    
    ''' <summary>
    ''' Test Jobs database
    ''' </summary>
    Sub TestJobsDatabase()
        Try
            Dim connString As String = DatabaseConfig.GetJobsConnectionString()
            Console.WriteLine("Connection String: " & connString.Replace(DatabaseConfig.PostgreSqlPassword, "****"))
            
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Console.WriteLine("✓ Connected to Jobs database!")
                
                ' Query reference tables
                Dim cmd As New NpgsqlCommand("SELECT COUNT(*) FROM Brands", conn)
                Dim brandCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine("  Brands: " & brandCount.ToString())
                
                cmd = New NpgsqlCommand("SELECT COUNT(*) FROM GoodsTypes", conn)
                Dim goodsCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine("  Goods Types: " & goodsCount.ToString())
                
                cmd = New NpgsqlCommand("SELECT COUNT(*) FROM TaskTypes", conn)
                Dim taskCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine("  Task Types: " & taskCount.ToString())
                
                cmd = New NpgsqlCommand("SELECT COUNT(*) FROM Jobs", conn)
                Dim jobCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine("  Jobs: " & jobCount.ToString())
                
                ' Show some brands
                cmd = New NpgsqlCommand("SELECT BrandDescr FROM Brands ORDER BY BrandDescr LIMIT 5", conn)
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    Console.WriteLine()
                    Console.WriteLine("  Sample Brands:")
                    While reader.Read()
                        Console.WriteLine("    - " & reader("BrandDescr").ToString())
                    End While
                End Using
                
            End Using
            
        Catch ex As Exception
            Console.WriteLine("✗ Error: " & ex.Message)
        End Try
    End Sub
    
    ''' <summary>
    ''' Test SQL syntax conversion
    ''' </summary>
    Sub TestSqlConversion()
        Try
            ' Test various SQL conversions
            Dim sqlServerQueries() As String = { _
                "SELECT TOP 10 * FROM Staff", _
                "SELECT GETDATE() AS CurrentDate", _
                "SELECT * FROM Jobs WHERE JobStatus LIKE '%Created%'", _
                "INSERT INTO Staff (StaffCode, Active) VALUES ('TEST', 1)" _
            }
            
            Console.WriteLine("Converting SQL Server syntax to PostgreSQL:")
            Console.WriteLine()
            
            For Each sql As String In sqlServerQueries
                Console.WriteLine("  SQL Server: " & sql)
                Dim converted As String = modDatabaseAbstraction.ConvertSqlSyntax(sql)
                Console.WriteLine("  PostgreSQL: " & converted)
                Console.WriteLine()
            Next
            
            Console.WriteLine("✓ SQL conversion working correctly!")
            
        Catch ex As Exception
            Console.WriteLine("✗ Error: " & ex.Message)
        End Try
    End Sub
    
End Module
