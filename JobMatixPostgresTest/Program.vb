Imports System
Imports Npgsql

Module Program
    Private TestInsertedId As Integer = -1

    Sub Main(args As String())
        Console.WriteLine("=========================================")
        Console.WriteLine("JobMatix PostgreSQL Connection Test")
        Console.WriteLine("Running on .NET 8 (Linux)")
        Console.WriteLine("=========================================")
        Console.WriteLine()

        ' Connection string from .env configuration
        Dim connString As String = "Host=localhost;Port=5432;Database=jobmatix_pos;Username=jobmatix_user;Password=JobMatix2026!Dev;"

        Console.WriteLine("Test 1: Connection Test")
        Console.WriteLine("------------------------")
        TestConnection(connString)
        Console.WriteLine()

        Console.WriteLine("Test 2: Query System Info")
        Console.WriteLine("--------------------------")
        TestSystemInfo(connString)
        Console.WriteLine()

        Console.WriteLine("Test 3: Query Staff Table")
        Console.WriteLine("--------------------------")
        TestStaffQuery(connString)
        Console.WriteLine()

        Console.WriteLine("Test 4: Test Jobs Database")
        Console.WriteLine("---------------------------")
        TestJobsDatabase()
        Console.WriteLine()

        Console.WriteLine("Test 5: INSERT Test (Staff)")
        Console.WriteLine("----------------------------")
        TestInsert(connString)
        Console.WriteLine()

        Console.WriteLine("Test 6: UPDATE Test")
        Console.WriteLine("--------------------")
        TestUpdate(connString)
        Console.WriteLine()

        Console.WriteLine("Test 7: DELETE Test")
        Console.WriteLine("--------------------")
        TestDelete(connString)
        Console.WriteLine()

        Console.WriteLine("=========================================")
        Console.WriteLine("All PostgreSQL tests completed!")
        Console.WriteLine("=========================================")
    End Sub

    Sub TestConnection(connString As String)
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Console.WriteLine("✓ Connected successfully!")
                Console.WriteLine($"  Server version: {conn.ServerVersion}")
                Console.WriteLine($"  Database: {conn.Database}")
                Console.WriteLine($"  Host: {conn.Host}")
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Connection failed: {ex.Message}")
        End Try
    End Sub

    Sub TestSystemInfo(connString As String)
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Dim cmd As New NpgsqlCommand("SELECT info_key, info_value FROM systeminfo ORDER BY info_key", conn)
                
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    Console.WriteLine("System Information:")
                    While reader.Read()
                        Console.WriteLine($"  {reader("info_key")} = {reader("info_value")}")
                    End While
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Query failed: {ex.Message}")
        End Try
    End Sub

    Sub TestStaffQuery(connString As String)
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Dim cmd As New NpgsqlCommand("SELECT staff_id, barcode, firstname, lastname, inactive FROM staff LIMIT 5", conn)
                
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    Console.WriteLine("Staff Records:")
                    While reader.Read()
                        Console.WriteLine($"  {reader("barcode")} - {reader("firstname")} {reader("lastname")} (Active: {Not CBool(reader("inactive"))})")
                    End While
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Query failed: {ex.Message}")
        End Try
    End Sub

    Sub TestJobsDatabase()
        Dim connString As String = "Host=localhost;Port=5432;Database=jobmatix_jobs;Username=jobmatix_user;Password=JobMatix2026!Dev;"
        
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                Console.WriteLine("✓ Connected to Jobs database!")
                
                ' Check table counts
                Dim cmd As New NpgsqlCommand("
                    SELECT 
                        (SELECT COUNT(*) FROM Brands) as brands,
                        (SELECT COUNT(*) FROM GoodsTypes) as goods_types,
                        (SELECT COUNT(*) FROM TaskTypes) as task_types,
                        (SELECT COUNT(*) FROM Symptoms) as symptoms,
                        (SELECT COUNT(*) FROM Jobs) as jobs
                ", conn)
                
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Console.WriteLine($"  Brands: {reader("brands")}")
                        Console.WriteLine($"  Goods Types: {reader("goods_types")}")
                        Console.WriteLine($"  Task Types: {reader("task_types")}")
                        Console.WriteLine($"  Symptoms: {reader("symptoms")}")
                        Console.WriteLine($"  Jobs: {reader("jobs")}")
                    End If
                End Using
                
                ' Show some brands
                cmd = New NpgsqlCommand("SELECT BrandDescr FROM Brands ORDER BY BrandDescr LIMIT 5", conn)
                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    Console.WriteLine(vbCrLf & "  Sample Brands:")
                    While reader.Read()
                        Console.WriteLine($"    - {reader("BrandDescr")}")
                    End While
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Jobs database test failed: {ex.Message}")
        End Try
    End Sub

    Sub TestInsert(connString As String)
        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                
                ' Insert a test staff member
                Dim cmd As New NpgsqlCommand("
                    INSERT INTO staff (barcode, firstname, lastname, docket_name, position, isadministrator, inactive, dateofbirth, date_created)
                    VALUES (@code, @firstname, @lastname, @docket, @position, @admin, @inactive, @dob, @created)
                    RETURNING staff_id
                ", conn)
                
                cmd.Parameters.AddWithValue("code", "TEST001")
                cmd.Parameters.AddWithValue("firstname", "Test")
                cmd.Parameters.AddWithValue("lastname", "User")
                cmd.Parameters.AddWithValue("docket", "Test User")
                cmd.Parameters.AddWithValue("position", "Tester")
                cmd.Parameters.AddWithValue("admin", False)
                cmd.Parameters.AddWithValue("inactive", False)
                cmd.Parameters.AddWithValue("dob", New DateTime(1990, 1, 1))
                cmd.Parameters.AddWithValue("created", DateTime.Now)
                
                Dim newId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Console.WriteLine($"✓ Inserted test staff member with ID: {newId}")
                
                ' Store for cleanup
                TestInsertedId = newId
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Insert failed: {ex.Message}")
        End Try
    End Sub

    Sub TestUpdate(connString As String)
        If TestInsertedId = -1 Then
            Console.WriteLine("⚠ Skipping - no test record to update")
            Return
        End If

        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                
                Dim cmd As New NpgsqlCommand("
                    UPDATE staff 
                    SET firstname = @name, lastname = @lastname, date_modified = @modified
                    WHERE staff_id = @id
                ", conn)
                
                cmd.Parameters.AddWithValue("name", "Test (Updated)")
                cmd.Parameters.AddWithValue("lastname", "User (Updated)")
                cmd.Parameters.AddWithValue("modified", DateTime.Now)
                cmd.Parameters.AddWithValue("id", TestInsertedId)
                
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Console.WriteLine($"✓ Updated {rowsAffected} row(s)")
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Update failed: {ex.Message}")
        End Try
    End Sub

    Sub TestDelete(connString As String)
        If TestInsertedId = -1 Then
            Console.WriteLine("⚠ Skipping - no test record to delete")
            Return
        End If

        Try
            Using conn As New NpgsqlConnection(connString)
                conn.Open()
                
                Dim cmd As New NpgsqlCommand("DELETE FROM staff WHERE staff_id = @id", conn)
                cmd.Parameters.AddWithValue("id", TestInsertedId)
                
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Console.WriteLine($"✓ Deleted {rowsAffected} row(s)")
                Console.WriteLine("  Test cleanup complete")
            End Using
        Catch ex As Exception
            Console.WriteLine($"✗ Delete failed: {ex.Message}")
        End Try
    End Sub
End Module
