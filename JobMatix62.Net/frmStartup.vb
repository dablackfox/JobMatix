
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.Data.OleDb

Public Class frmStartup

    '-- Startup Options..


    '== grh 18-June-2021-  This is the form for the Open Source JobMatix Startup Options screen...

    ' Copyright 2021 grhaas@outlook.com

    'Licensed under the Apache License, Version 2.0 (the "License");
    'you may Not use this file except In compliance With the License.
    'You may obtain a copy Of the License at

    '    http://www.apache.org/licenses/LICENSE-2.0

    'Unless required by applicable law Or agreed To In writing, software
    'distributed under the License Is distributed On an "AS IS" BASIS,
    'WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
    'See the License For the specific language governing permissions And
    'limitations under the License.

    '= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 




    '== 3311.509-  Tidying up..

    '==3357.0206- 06Feb2017= Fix Form centering.

    '==
    '==  >> 3411.0214=  14-Feb-2018= Updated design...
    '==          -- frmStartup- Added Tab Control for New/Old users... 
    '== 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (29-June-2021)
    '==   Target-New-Build-6201 --  (29-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Private mFrmParent As Form

    Private msSqlServerComputer As String = ""

    Private msSqlServer As String = ""
    Private msSqlVersion As String = ""

    Private msComputerName As String '--local machine--

    Private mbRunningOnServer As Boolean = False

    Private msJobMatixVersion As String
    Private mbIsAdmin As Boolean = False
    Private mColDBlist As Collection

    Private mbActivated As Boolean = False
    Private mbCancelled As Boolean = False

    Private msResult As String = ""

    '--results-

    ReadOnly Property result() As String
        Get
            result = msResult
        End Get
    End Property '--path..-
    '= = = = = = = = = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = = = = = = =  =
    '-===FF->


    '--Constructor-
    '--Constructor-

    Public Sub New(ByRef frmParent As Form, _
                       ByVal bIsAdmin As Boolean, _
                       ByVal sSqlServerComputer As String, _
                        ByVal sSqlServer As String, _
                         ByVal sSqlVersion As String, _
                          ByVal sJobmatixVersion As String, _
                           ByRef colDBlist As Collection)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save inputs-
        mFrmParent = frmParent
        mbIsAdmin = bIsAdmin

        msSqlServerComputer = sSqlServerComputer
        msSqlServer = sSqlServer
        msSqlVersion = sSqlVersion

        msJobMatixVersion = sJobmatixVersion
        mColDBlist = colDBlist

    End Sub  '--new-
    '= = = = = = = = = == = 

    '-- load-

    Private Sub frmStart_Load(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "JobMatix Startup Options.-"
        grpBoxDBs.Text = "Available Databases"
        grpBoxAdminExisting.Text = "Restore/Migrate Jobmatix"
        grpBoxAdminNew.Text = ""

        Me.Text = "JobMatix Startup"
        msComputerName = My.Computer.Name

        Call CenterForm(Me)
        '- position on top of calling form..
        'If mFrmParent Is Nothing Then
        '    Call CenterForm(Me)
        'Else
        '    Me.Left = mFrmParent.Left + 216
        '    Me.Top = mFrmParent.Top + 150
        'End If
        '= labStaffName.Text = msStaffName
        labSqlVersion.Text = "Sql Server: " & msSqlServer & vbCrLf & "Version: " & msSqlVersion

        labJobmatixVersion.Text = msJobMatixVersion

        mbRunningOnServer = (UCase(msSqlServerComputer) = UCase(msComputerName))

        ListDBs.Items.Clear()
        grpBoxAdminNew.Enabled = True
        If mbIsAdmin Then
            grpBoxAdminExisting.Enabled = True
            If mbRunningOnServer Then
                btnRestore.Enabled = True
                btnMigrate.Enabled = True
            Else
                btnRestore.Enabled = False
                '= TEMP- ALLOW to test on CLIENT- btnMigrate.Enabled = False
            End If
            labChoose.Text = "Choose from: " & vbCrLf & _
                            " = Select a JobMatix database to open; " & vbCrLf & _
                            " = Create a new JobMatix database; " & vbCrLf & _
                            " = Restore from a JobMatix Database Backup; " & vbCrLf & _
                            "    or Migrate JobMatix from MYOB RM to JobMatixPOS."
        Else
            grpBoxAdminExisting.Enabled = False
            labChoose.Text = "Choose from: " & vbCrLf & _
                             " = Select a JobMatix database to open; "
        End If
        btnSelectDB.Enabled = False

    End Sub  '-load-
    '= = = = = = = = = = =  =

    Private Sub frmStart_Activated(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True

        If mColDBlist Is Nothing Then
            MsgBox("Invalid DB list..", MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
            Exit Sub
        End If

        '-load listbox.-
        If mColDBlist.Count > 0 Then
            Dim sName As String
            Dim col1 As Collection
            For Each col1 In mColDBlist
                sName = col1.Item("dbname")
                ListDBs.Items.Add(sName)
            Next
            ListDBs.SelectedIndex = -1
            grpBoxDBs.Enabled = True
            TabControl1.SelectedIndex = 1   '--existing..
        Else
            grpBoxDBs.Enabled = False
            TabControl1.SelectedIndex = 0   '--New-
        End If

    End Sub  '-Activated-
    '= = = = = = = = = = = 
    '-===FF->

    Private Sub ListDBs_SelectedIndexChanged(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles ListDBs.SelectedIndexChanged

        If ListDBs.SelectedIndex >= 0 Then  '--selected-
            btnSelectDB.Enabled = True
        Else
            btnSelectDB.Enabled = False
        End If
    End Sub  '-ListDBs_SelectedIndexChanged-
    '= = = = = = = = = = = = =  = = == ==

    '-- DoubleClick to select-
    '-- DoubleClick to select-

    Private Sub ListDBs_DoubleClick(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles ListDBs.DoubleClick
        If ListDBs.SelectedIndex >= 0 Then  '--selected-
            msResult = ListDBs.SelectedItem
            Me.Hide()
        End If
    End Sub  '- DoubleClick-
    '= = = = = = = = = = = =  = 

    '-Select-
    '-Select-

    Private Sub btnSelectDB_Click(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles btnSelectDB.Click

        If ListDBs.SelectedIndex >= 0 Then  '--selected-
            msResult = ListDBs.SelectedItem
            Me.Hide()
        End If

    End Sub  '-Select-
    '= = = = = = = = = =

    '-Create-
    '-Create-

    Private Sub btnCreateRM_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles btnCreateRM.Click
        msResult = "CREATE-DB-RM"
        Me.Hide()
    End Sub  '-Create-
    '= = = = = = = = = 

    Private Sub btnCreatePOS_Click(sender As Object, e As EventArgs) Handles btnCreatePOS.Click

        msResult = "CREATE-DB-JMXPOS"
        Me.Hide()
    End Sub  '-create POS-
    '= = = = == = === =  = == =

    '--Restore-
    '--Restore-
    Private Sub btnRestore_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) Handles btnRestore.Click
        msResult = "RESTORE-DB"
        Me.Hide()

    End Sub '--Restore-
    '= = = =  = = = = 

    '--btnMigrate-

    Private Sub btnMigrate_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnMigrate.Click

        msResult = "MIGRATE-DB"
        Me.Hide()

    End Sub  '--btnMigrate-
    '= = = = = = = = = = = = = = =

    '-===FF->

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles btnCancel.Click
        mbCancelled = True
        Me.Hide()

    End Sub  '-cancel-
    '= = = = = = = = = = = = = 

    '--= = = u n l o a d = = = = = = =

    Private Sub frmStartup_FormClosing(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                         Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        '--MsgBox "frmMaint UNload event..'"  '-debug--
        '--If Not gbclosingDown Then
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                              System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                            System.Windows.Forms.CloseReason.FormOwnerClosing '==  NOT FOR vb.net.., vbFormCode
                mbCancelled = True
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                mbCancelled = True
                intCancel = 0 '--let it go--

        End Select

    End Sub  '--closing-
    '= = = = = =  == = = 


End Class '-frmStartup--
'= = = = = = = = = = = = = = = =  =