Imports System
Imports System.Windows.Forms
Imports System.Data
Imports Npgsql

Public Class FormMain
    Inherits Form
    
    Private btnTest As Button
    Private txtOutput As TextBox
    Private lblStatus As Label
    Private menuStrip As MenuStrip
    Private statusStrip As StatusStrip
    
    Public Sub New()
        InitializeComponent()
        DatabaseConfig.LoadConfiguration()
    End Sub
    
    Private Sub InitializeComponent()
        Me.Text = "JobMatix POS 8.0 - .NET 8"
        Me.Size = New Drawing.Size(1024, 768)
        Me.StartPosition = FormStartPosition.CenterScreen
        
        ' Menu Strip
        menuStrip = New MenuStrip()
        Dim fileMenu As New ToolStripMenuItem("&File")
        fileMenu.DropDownItems.Add("&Exit", Nothing, AddressOf OnExit)
        
        Dim dbMenu As New ToolStripMenuItem("&Database")
        dbMenu.DropDownItems.Add("Test &Connection", Nothing, AddressOf TestConnection)
        dbMenu.DropDownItems.Add("View &Stock", Nothing, AddressOf ViewStock)
        
        menuStrip.Items.Add(fileMenu)
        menuStrip.Items.Add(dbMenu)
        
        ' Status Strip
        statusStrip = New StatusStrip()
        lblStatus = New ToolStripStatusLabel("Ready")
        statusStrip.Items.Add(lblStatus)
        
        ' Output TextBox
        txtOutput = New TextBox()
        txtOutput.Multiline = True
        txtOutput.ScrollBars = ScrollBars.Both
        txtOutput.Dock = DockStyle.Fill
        txtOutput.Font = New Drawing.Font("Consolas", 10)
        
        ' Add controls
        Me.Controls.Add(txtOutput)
        Me.Controls.Add(menuStrip)
        Me.Controls.Add(statusStrip)
        Me.MainMenuStrip = menuStrip
    End Sub
    
    Private Sub OnExit(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub
    
    Private Sub TestConnection(sender As Object, e As EventArgs)
        Try
            UpdateStatus("Testing connection...")
            txtOutput.AppendText("==================================" & Environment.NewLine)
            txtOutput.AppendText("Testing PostgreSQL Connection" & Environment.NewLine)
            txtOutput.AppendText("==================================" & Environment.NewLine)
            
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                txtOutput.AppendText($"✓ Connected to: {conn.Database}" & Environment.NewLine)
                
                ' Test system info
                Using cmd As IDbCommand = conn.CreateCommand()
                    cmd.CommandText = "SELECT info_key, info_value FROM systeminfo ORDER BY info_key"
                    Using reader As IDataReader = cmd.ExecuteReader()
                        txtOutput.AppendText(Environment.NewLine & "System Information:" & Environment.NewLine)
                        While reader.Read()
                            txtOutput.AppendText($"  {reader("info_key")} = {reader("info_value")}" & Environment.NewLine)
                        End While
                    End Using
                End Using
                
                UpdateStatus("Connection successful!")
            End Using
        Catch ex As Exception
            txtOutput.AppendText($"✗ Error: {ex.Message}" & Environment.NewLine)
            UpdateStatus("Connection failed!")
        End Try
    End Sub
    
    Private Sub ViewStock(sender As Object, e As EventArgs)
        Try
            UpdateStatus("Loading stock...")
            txtOutput.AppendText("==================================" & Environment.NewLine)
            txtOutput.AppendText("Stock Items" & Environment.NewLine)
            txtOutput.AppendText("==================================" & Environment.NewLine)
            
            Using conn As IDbConnection = GetDatabaseConnection()
                conn.Open()
                
                Using cmd As IDbCommand = conn.CreateCommand()
                    cmd.CommandText = "SELECT stock_id, stock_code, stock_description, stock_qty, sell_price FROM stock ORDER BY stock_code LIMIT 20"
                    Using reader As IDataReader = cmd.ExecuteReader()
                        Dim count As Integer = 0
                        While reader.Read()
                            count += 1
                            txtOutput.AppendText($"{count}. [{reader("stock_code")}] {reader("stock_description")} - Qty: {reader("stock_qty")}, Price: ${reader("sell_price")}" & Environment.NewLine)
                        End While
                        txtOutput.AppendText(Environment.NewLine & $"Total: {count} items" & Environment.NewLine)
                    End Using
                End Using
                
                UpdateStatus($"Loaded stock items")
            End Using
        Catch ex As Exception
            txtOutput.AppendText($"✗ Error: {ex.Message}" & Environment.NewLine)
            UpdateStatus("Failed to load stock!")
        End Try
    End Sub
    
    Private Sub UpdateStatus(message As String)
        lblStatus.Text = message
        Application.DoEvents()
    End Sub
End Class
