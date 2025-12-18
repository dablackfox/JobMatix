Option Strict Off
Option Explicit On

Friend Class frmLogin
    Inherits System.Windows.Forms.Form

    '-- == grh- =23-Nov-2011==  UPGRADE.. vb.net version..

    '== grh 3031.7= 27Mar2012 ==
    '--- Servertext box now NOT multiline.-
    '---    (Fixes problem when pressing ENTER on server.)..--
    '==
    '== grh 3072/3== 16Feb2013 ==
    '==   >>  Set Cancelled for Form Control X exit..
    '==
    '== grh 3083.210== 10Feb2014 ==  
    '==     ALWAYS Search for SQL servers.. 
    '==       WE are here if requested or if no server name supplied.
    '== 
    '==  grh. JobMatix 3.1.3107.803-  03-Aug-2015 ===
    '==   >>  Now Using .net 4.5.2
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = 

    '--SQL Login form--
    Private mbActivated As Boolean = False
    Private mbSetupDone As Boolean = False

    Private msUserName As String = ""
    Private msPassword As String = ""

    Private msComputerName As String = ""
    Private msServer As String = ""
    Private mbSql As Boolean = False
    Private mbJet As Boolean = False

    Private mbWindowsAuth As Boolean = False  '--IsWindowsAuthentication--

    Private mbCancelled As Boolean = True

    Private mColSQLServerInstances As Collection

    '--Dim mbCreateMode As Boolean
    '= = = = = = = =  = = =  ==

    WriteOnly Property ComputerName() As String
        Set(ByVal Value As String)
            msComputerName = Value
            txtServer.Text = Value
        End Set
    End Property '--computername-
    '= = == = = = = = =

    WriteOnly Property IsSqlServer() As Boolean
        Set(ByVal Value As Boolean)
            mbSql = Value
        End Set
    End Property '--computername-
    '= = = = = = = = = = =

    WriteOnly Property IsWindowsAuthentication() As Boolean
        Set(ByVal Value As Boolean)
            mbWindowsAuth = Value
        End Set
    End Property '--computername-
    '= = = = = = = = = = =


    WriteOnly Property IsJet() As Boolean
        Set(ByVal Value As Boolean)
            mbJet = Value
        End Set
    End Property '--computername-
    '= = = = = = = = = = =

    '--  set up initial user details..--
    WriteOnly Property LastUserName() As String
        Set(ByVal Value As String)
            msUserName = Value
        End Set
    End Property '--last user.--
    '= = = = = = = = = =  = = =

    WriteOnly Property LastPassword() As String
        Set(ByVal Value As String)
            msPassword = Value
        End Set
    End Property '--last pw.--
    '= = = = = = = = = =  = = =
    '= = = = = = = = = = = = = = = = = =

    '== Rseults..==

    ReadOnly Property server() As String
        Get
            server = txtServer.Text
        End Get
    End Property '--server--
    '= = = = = = = = =

    ReadOnly Property username() As String
        Get
            username = txtUserName.Text
        End Get
    End Property '--user--

    ReadOnly Property password() As String
        Get
            password = txtPassword.Text
        End Get
    End Property '--pwd--
    '= = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = = = = =
    ReadOnly Property Createmode() As Boolean
        Get
            Createmode = False '--chkCreate.Value
        End Get
    End Property '--create-
    '= = = = = = = = = =
    '-===FF->

    '--  This stuff has to go into the constructor (  sub New()  ) in the designer code.-
    '--  This stuff has to go into the constructor (  sub New()  ) in the designer code.-

    '--   BUT NOW we can initialse the vars when they are DECLAREd.. !!!!!!  --

    '--   BUT NOW we can initialse the vars when they are DECLAREd.. !!!!!!  --
    '--   BUT NOW we can initialse the vars when they are DECLAREd.. !!!!!!  --

    '== Private Sub frmLogin_Load(ByVal eventSender As System.Object, _
    '==                               ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    '==     mbJet = False
    '==     mbSql = False
    '==     msComputerName = ""
    '==     txtServer.Width = VB6.TwipsToPixelsX(2925)
    '==     cmdBrowse.Visible = False
    '==     mbCancelled = False
    '==     msUserName = ""
    '==     msPassword = ""

    '==     mbWindowsAuth = False
    '==     LabTitle.Text = ""

    '== End Sub '--load-
    '= = = = = = = = =
    '-===FF->

    '-search sql-
    '-search sql-

    Private Function mbSearchSQL() As Boolean
        Dim ok As Boolean = False
        Dim col1 As Collection

        mbSearchSQL = False
        mbSetupDone = False
        cmdBrowse.Enabled = False
        labStatus.Text = "Please wait-  Searching for SQL Server instances.   This could take a minute or two.."
        Application.DoEvents()
        cmdOK.Enabled = False
        '== picWorking.Visible = True
        '==  Working gif doesn't get updated..
        Application.DoEvents()
        ok = gbSQL_Enumerate_Main(mColSQLServerInstances)
        labStatus.Text = ""
        labStatus.ForeColor = Color.Black
        '==picWorking.Visible = False
        Application.DoEvents()
        If Not ok Then
            MsgBox("Search Failed", MsgBoxStyle.Critical)
            Exit Function
        End If
        If (Not (mColSQLServerInstances Is Nothing)) AndAlso (mColSQLServerInstances.Count > 0) Then
            If (mColSQLServerInstances.Count = 1) Then  '--only one..--
                col1 = mColSQLServerInstances(1)
                txtServer.Enabled = True
                txtServer.Text = col1("ServerName") & "\" & col1("InstanceName")
                Application.DoEvents()
                labStatus.Text = vbCrLf & "Found one SQL server.."
                MsgBox("Found one SQL server..", MsgBoxStyle.Information)
                cmdOK.Enabled = True
                cmdOK.Select()
            Else  '--choose--
                listServers.Items.Clear()
                For Each col1 In mColSQLServerInstances
                    listServers.Items.Add(col1("ServerName") & "\" & col1("InstanceName"))
                Next
                labServerSearch.Visible = False
                listServers.Visible = True
                listServers.SelectedIndex = -1  '= 0
                MsgBox("Found " & mColSQLServerInstances.Count & " SQL servers..", MsgBoxStyle.Information)
                labStatus.Text = "Please choose a server instance."
                If (msComputerName = "") Then
                    labStatus.Text &= "- Try typing in a server name if your server is not listed."
                End If
                Application.DoEvents()
                cmdBrowse.Visible = True
                cmdBrowse.Enabled = True
                '== If (msComputerName <> "") Then
                txtServer.Enabled = True
                cmdOK.Enabled = True
                listServers.Select()
                '== End If
                mbSetupDone = True
            End If
        ElseIf (msComputerName = "") Then '-- no servers..--
            labStatus.Text = "No SQL server found.."
            labStatus.Text &= vbCrLf & "  You can try typing in a server name to connect.."
            Application.DoEvents()
            MsgBox("No SQL server found..", MsgBoxStyle.Exclamation)
            '==mbCancelled = True
            '==Me.Close()
            '== Exit Function
            cmdBrowse.Visible = True
            cmdBrowse.Enabled = True
            txtServer.Enabled = True
            txtServer.Focus()
        Else  '-- supplied name not found.
            labStatus.Text = ""
            Application.DoEvents()
            MsgBox("SQL server: '" & msComputerName & "' was not found..", MsgBoxStyle.Exclamation)
            cmdBrowse.Visible = True
            cmdBrowse.Enabled = True
            cmdOK.Enabled = True
            txtServer.Enabled = True
            txtServer.Focus()
        End If
    End Function  '-search sql-
    '= = = = = = = = =
    '-===FF->

    '== Private Sub frmLogin_Activated(ByVal eventSender As System.Object, _
    '==                                   ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
  
    Private Sub frmLogin_Load(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '== cmdBrowse.Visible = False
        labStatus.Text = ""
         listServers.Visible = False
        '= picWorking.Visible = False
        listServers.Top = labServerSearch.Top
        listServers.Left = labServerSearch.Left
        listServers.Width = labServerSearch.Width
        listServers.Height = labServerSearch.Height

        labServerSearch.Text = "You can search for Sql Server instances on the local network. " & _
            "However, a server instance can only be discovered if the Sql Browser service is running on that machine."

        labServerSearch.Visible = False
    End Sub  '--load-
    '= = = = = = = = = 

    '--  What was VB6 LOAD EVENT stuff NOW has to go
    '---       into the constructor (  sub New()  ) in the designer code.-
    '--  VB6 Activate EVENT needs to be the vb.net LOAD  event..
    '--   BUT NOW we can initialse the vars when they are DECLAREd.. !!!!!!  --
    '--   BUT NOW we can initialse the vars when they are DECLAREd.. !!!!!!  --

    Private Sub frmLogin_Activated(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Dim ok As Boolean = False
        '==Dim col1 As Collection

        Application.DoEvents()
        If mbActivated Then Exit Sub
        mbActivated = True

        txtUserName.Text = msUserName
        txtPassword.Text = msPassword
        If mbJet Then
            cmdBrowse.Visible = True
            labServer.Text = "MS-Access DB FileName"
            Me.Text = "JobMatix:  Login to Jet (Access) Database.."
            LabTitle.Text = "Login to Jet (Access) Database.."
            If msUserName = "" Then
                txtUserName.Text = "admin"
                txtPassword.Text = ""
            End If
            txtServer.Width = 435
            cmdBrowse.Left = (txtServer.Left + txtServer.Width - cmdBrowse.Width)
            txtServer.Focus()
        ElseIf Not mbSql Then  '--odbc--
            labServer.Text = "Data Source Name"
            Me.Text = "JobMatix:   ODBC Data Source"
            LabTitle.Text = "ODBC Data Source"
            txtServer.Focus()
        Else '--sql-
            labServerSearch.Visible = True
            Application.DoEvents()
            Me.Text = "JobMatix:  Sql Server Login.."
            labServer.Text = "SQL Server:"
            LabTitle.Text = "Sql Server Login"
            txtServer.Text = msComputerName   '--from caller.
            '== txtServer.Enabled = False
            If mbWindowsAuth Then '-- no user name needed..--
                txtUserName.Enabled = False
                txtPassword.Enabled = False
                labUsername.Enabled = False
                labPassword.Enabled = False
            End If
            '- if no server supplied, then search..--
            '== Search anyway..--
            cmdBrowse.Text = "Search.."
            If (msComputerName = "") Then
                labStatus.Text = "No previous SQL server.."
                labStatus.Text &= vbCrLf & "You can type in a server\instance name to connect to, "
                labStatus.Text &= vbCrLf & "  or Search for available network servers.."
                cmdBrowse.Focus()
            Else
                cmdOK.Select()
            End If
        End If  '--jet/sql--
        '==  Set cancelled since we're not catching Form X-exit event..
        '== NO..  Control Box gone..-  mbCancelled = True
    End Sub '--activated-
    '= = = = = = = = =
    '-===FF->

    Private Sub listServers_SelectedIndexChanged(ByVal sender As System.Object, _
                                                 ByVal e As System.EventArgs) _
                                                           Handles listServers.SelectedIndexChanged
        Dim sDescr As String
        If Not mbSetupDone Then Exit Sub '--premature--
        '-- get selection..--
        If (listServers.SelectedIndex >= 0) Then
            '--get selected priority and update Job..--
            sDescr = listServers.Text '--selected itm.--
            txtServer.Text = sDescr
            txtServer.Enabled = True
            labStatus.Text = ""
            cmdOK.Enabled = True
            cmdOK.Select()
        End If
    End Sub  '--SelectedIndexChanged--
    '= = = = = = = = = = = = = = = = 

    '====Browse for Jet file --
    '== Or Search again for SQL servers..--

    '== common dialog box to get JET file path ==
    '== common dialog box to get JET file path ==

    Private Sub cmdBrowse_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdBrowse.Click
        Dim dlg1Open1 As OpenFileDialog
        Dim MyResult As System.Windows.Forms.DialogResult
        Dim ok As Boolean

        If mbJet Then
            dlg1Open1 = New OpenFileDialog()
            dlg1Open1.Title = "Jet Database selection- Pls Select MDB file.."
            dlg1Open1.Filter = "Jet (MS Access) DB Files (*.mdb)|*.mdb|All Files (*.*)|*.*"
            dlg1Open1.InitialDirectory = My.Application.Info.DirectoryPath '--msAppPath
            '==On Error Resume Next
            MyResult = dlg1Open1.ShowDialog()
            '--check for cancel--
            If MyResult <> System.Windows.Forms.DialogResult.OK Then     '= If Err().Number <> 0 Then '--Cancelled==
                System.Windows.Forms.Cursor.Current = Cursors.Default
                Exit Sub
            End If
            On Error GoTo 0
            txtServer.Text = dlg1Open1.FileName

            txtServer.SelectionStart = Len(txtServer.Text)
            txtServer.SelectionLength = 0
            txtServer.Focus()

        ElseIf mbSql Then  '--search again..-
            ok = mbSearchSQL()
        End If

    End Sub '--browse--
    '= = = = = = = = = 
    '-===FF->

    '-- ENTER on server name..-
    '-- PreviewKeyDown is where you preview the key.
    '-- Do not put any logic here, instead use the
    '-- KeyDown event after setting IsInputKey to true.

    Private Sub txtServer_PreviewKeyDown(ByVal sender As Object, _
                                          ByVal e As PreviewKeyDownEventArgs) Handles txtServer.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Enter
                e.IsInputKey = True
        End Select
    End Sub  '--PreviewKeyDown-
    '= = = = = = = = =  = = = = =  == 

    '-- "ENTER" key on PartNo )barcode..)..-
    Private Sub txtServer_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServer.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            If (Trim(txtServer.Text) <> "") Then
                cmdOK.Focus()
                keyAscii = 0 '--processed..-
            End If
            '== ElseIf keyAscii = Keys.Escape Then   '==27 Then '--ESC-
            '== keyAscii = 0 '--processed..-
            '== If txtPartNo.Text = "" Then  '--exit
            '== mbCancelled = True
            '== Me.Hide()
            '== Exit Sub
            '== End If  '--no data-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--PartNo_KeyPress--
    '= = = = = = = = = = =
    '= = = = =  = = = = = = = = 
    '-===FF->

    'UPGRADE_WARNING: Event txtUserName.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtUserName_TextChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles txtUserName.TextChanged
        If UCase(txtUserName.Text) = "SA" Then
            '--chkCreate.Enabled = True
            '--chkCreate.Visible = True

        Else
            '--chkCreate.Enabled = False
            '--chkCreate.Visible = False

        End If '--sa--

    End Sub
    '= = = = = = = = = = = = =

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        'set the global var to false
        'to denote a failed login
        '--LoginSucceeded = False
        mbCancelled = True
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        'check for correct password
        '--If txtPassword = "password" Then
        'place code to here to pass the
        'success to the calling sub
        'setting a global var is the easiest
        '--LoginSucceeded = True
        mbCancelled = False
        Me.Close()
        '--Else
        '--    MsgBox "Invalid Password, try again!", , "Login"
        '--    txtPassword.SetFocus
        '--    SendKeys "{Home}+{End}"
        '--End If
    End Sub '--ok--
    '=== end form==


End Class