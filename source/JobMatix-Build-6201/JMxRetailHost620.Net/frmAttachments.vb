Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Diagnostics
'--
Public Class frmAttachments

    '==
    '==   grh 3119.1216-  16-Dec-2015=
    '==     >> Started-  New form to manage all Attachments for Jobs/RA's .- 
    '==                (Pics, Docs, PDF's .)
    '==
    '==   grh 3203.1225-  25-Dec-2015=
    '==
    '==     >> Stripped to bone and exported main logic to "clsAttachments".- 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  =NEW VERSION=
    '==   grh 3311.227-  27-Feb-2016=
    '==      >> Fixes, and ADD MS-WORD and Excel Document types.--
    '==      >>  RAs Colour- rgb(255,96,48) -- #FF6030 ==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== Public Const K_SAVESETTINGSPATH As String = "localPOSSettings.txt"
    '-- columns widths..
    Const K_width_party_info As Integer = 500
    Const K_width_staff_name As Integer = 50
    Const K_width_comments As Integer = 1000


    Private mFrmParent As Form
    Private msJobMatixVersion As String = ""

    Private mbIsInitialising As Boolean = True
    Private mbActivated As Boolean = False
    Private mbStartingUp As Boolean
    Private mbMainLoadDone As Boolean = False
    Private mbFormClosing As Boolean = False

    '== Private msServer As String = ""
    '-- now split server/instance..--
    '== Private msSqlServerComputer As String = ""
    '== Private msSqlServerInstance As String = ""
    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private msComputerName As String '--local machine--

    '--- Actual connections ---

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  jobs DB info--
    Private mCnnSql As OleDbConnection '= ADODB.Connection '-- 

    Private msOwnerApp As String = ""
    Private mIntEntityId As Integer = -1  '-Job_id or RA_id..

    Private msPartyInfo As String = ""
    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private msAppPath As String
    Private mSdSettings As clsStrDictionary '--  holds local job settings..


    '- TO GO !!!--
    '== Private msListSql As String

    '-- NEW File to be Attached..

    '== Private mByteNewFile As Byte()
    '== Private msNewFileFullPath As String = ""
    '== Private msNewFileFileTitle As String = ""
    '== Private msNewFileFormat As String = ""

    '- current selection-
    '== Private mIntDoc_id As Integer = -1
    '== Private mByteRetrievedFile As Byte()
    '== Private msCurrentFileTitle As String = ""
    '== Private mCurrentBinaryData As Byte()
    '== Private msCurrentFileSuffix As String = ""

    '= = = = = = = = = = = = = = = = = = = = = = = = =  = = = =

    Private mbcancelled As Boolean = False
    Private msLastSqlErrorMessage As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==

    '--=3203.1222=
    '=  Context menu for Pasting- attachment file name-
    '--  Popup menu for Right click on txt File name..-
    Private mContextMenuPasteFileName As ContextMenu
    Private WithEvents mnuPasteFileName As New MenuItem("Paste File")
    Private WithEvents mnuPasteFileSep1 As New MenuItem("-")
    Private WithEvents mnuPasteFileSep2 As New MenuItem("-")

    '-- Dummy to disable default menu-
    '-- LEAVE empty.-
    Private mContextMenuDummy As New ContextMenu

    '== clsAttachments -

    Private mClsAttachments1 As clsAttachments

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbcancelled
        End Get
    End Property
    '= = = = = = = = =  == == 
    '-===FF->

    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                          ByVal sOwnerApp As String, _
                           ByVal intEntityId As Integer, _
                            ByVal strPartyInfo As String, _
                             ByVal intStaff_id As Integer, _
                              ByVal strStaffName As String, _
                              ByVal strJobMatixVersion As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = FrmParent
        msOwnerApp = sOwnerApp   '-  "JOB" or "RA" ----

        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        '= msJobMatixVersion = sVersion

        mIntEntityId = intEntityId  '- Job_id or RA_id
        msPartyInfo = VB.Left(strPartyInfo, K_width_party_info)
        msStaffName = VB.Left(strStaffName, K_width_staff_name)
        mIntStaff_id = intStaff_id

        labVersion.Text = strJobMatixVersion

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- load --
    '= 3119.1216=
    '- http://stackoverflow.com/questions/5952006/how-to-check-if-table-exist-and-if-it-doesnt-exist-create-table-in-sql-server-20

    Private Sub frmAttachments_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.Text = "JobMatix Ver: " & My.Application.Info.Version.Major & "." & _
           My.Application.Info.Version.Minor & ". Build: " & _
            My.Application.Info.Version.Build & "." & _
               My.Application.Info.Version.Revision & " - Processing Attachments.."


        '= labOnFileLab.Text = ""
        labHdr2.Text = ""
        labHdrInfo.Text = msPartyInfo
        labPartyLab.Text = ""
        labEntityId.Text = CStr(mIntEntityId)
        '== txtNewComment.MaxLength = K_width_comments
        '== txtNewComment.Text = ""
        '== txtNewFileName.Text = ""
        '== grpBoxItem.Text = ""

        '== lvwDocs.Enabled = False
        '== grpBoxAddNew.Enabled = False

        '= picPDF.Top = picProduct.Top
        '== picPDF.Left = picProduct.Left + 16
        '== picPDF.Visible = False

        '== labHelp.Text = "To add an Attachment, browse to the PDF or Image file to be attached, and Open it. " & vbCrLf & _
        '==                  " (Or Copy and paste onto this Form using Ctl-V..) " & vbCrLf & _
        '==                  "Then Enter some comment, and Press Save.." & vbCrLf
        '= msListSql = "SELECT * FROM dbo.Attachments "
        '== msListSql &= "WHERE (doc_app_type='" & msOwnerApp & "') AND (doc_app_entity_id=" & mIntEntityId & "); "

        Select Case UCase(msOwnerApp)
            Case "JOB"
                '= grpBoxItem.Text = "Attachments on File for Job: " & mIntEntityId
                labHdr1.Text = "Job Attachments "
                labHdr2.Text = "Job No:"
                labPartyLab.Text = "Customer Info:"
            Case "RA"
                labHdr1.Text = "RA Attachments "
                '== grpBoxItem.Text = "Attachments on File for RA: " & mIntEntityId
                labHdr2.Text = "RA No:"
                labPartyLab.Text = "Supplier Info:"
                '= msListSql &= "WHERE (doc_app_type='RA'); "
                panelBanner.BackColor = Color.FromArgb(255, 96, 48)
            Case Else
                MsgBox("ERROR- Invalid Application Type..", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
        End Select

        Me.KeyPreview = True  '-To catch Ctl-V (Pasting File)..

        '--=3119.1222=
        '=  Context menu for pasting file Name--
        '--  Popup menu for Right click on txt File name..-
        mContextMenuPasteFileName = New ContextMenu
        mnuPasteFileName.Name = "mnuPasteFileName"
        '== mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileSep1)
        mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileName)
        '== mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileSep2)

        '--  done that menu.--

        '-- disable default menu.
        txtNewFileName.ContextMenu = mContextMenuDummy

        '= 3203.114-  fix Attachments listView=
        Me.lvwDocs.Columns(0).TextAlign = HorizontalAlignment.Right
        Me.lvwDocs.Columns(1).TextAlign = HorizontalAlignment.Right
        Me.lvwDocs.Columns(3).TextAlign = HorizontalAlignment.Right

        Call CenterForm(Me)

    End Sub  '--load --
    '= = = = = = == = == = =  =
    '= = = = = = = = = = = =
    '-===FF->


    Private Sub frmAttachments_Activated(sender As Object, _
                                          ev As EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

        '-- Make new inst. of clsAttachments.
        Try
            mClsAttachments1 = New clsAttachments(Me, mCnnSql, mColSqlDBInfo, msOwnerApp, _
                                         mIntEntityId, msPartyInfo, mIntStaff_id, msStaffName, openDlg1)
        Catch ex As Exception
            MsgBox("ERROR creating new clsAttachments." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
 
        mbIsInitialising = False

    End Sub  '--activated-
    '= = = = = = = = = = = =
    '-===FF->

    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 
    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 
    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 

    '--PREVIEW KeyPress..-
    '-- Catch  ctl-V- to Paste.

    Private Sub frmAttachments_KeyDown(sender As Object, _
                                  eventArgs As System.Windows.Forms.KeyEventArgs) _
                                      Handles MyBase.KeyDown
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.frmAttachments_KeyDown(sender, eventArgs)
        Exit Sub

    End Sub  '-- keydown-
    '= = = = = = = = = = = =
    '-===FF->

    '--=3119.1222=  PAST-FILE Context menu stuff--
    '--=3119.1222=  PAST-FILE Context menu stuff--

    '-- menu click-

    Public Sub mnuPasteFileName_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles mnuPasteFileName.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.mnuPasteFileName_Click(eventSender, eventArgs)
        Exit Sub

    End Sub  '-mnuPasteFileName- click-
    '= = = = = = = = = = = = = = = =  = = = = = = =
    '-===FF->

    '-  MOUSE ACTION- txtNewFileName-

    '-- HANDLED HERE !!!
    '-- HANDLED HERE !!!
    '-- HANDLED HERE !!!

    Private Sub txtNewFileName_MouseUp(sender As Object, _
                                         ev As MouseEventArgs) Handles txtNewFileName.MouseUp
  
        '== Dim sFileFullpath, sFileTitle, sFormat As String
        If mbIsInitialising Then Exit Sub
        Dim data_object As Object = Clipboard.GetDataObject()

        ' If the right mouse button was clicked and released, 
        ' display the shortcut menu assigned to the txt.  
        If ev.Button = MouseButtons.Right Then
            '== If mbGetFileFromClipboard(sFileFullpath, sFileTitle, sFormat) Then
            If (data_object.GetDataPresent(DataFormats.FileDrop)) Then
                mnuPasteFileName.Enabled = True
            Else  '-nothing on clipboard-
                mnuPasteFileName.Enabled = False
            End If '-get
            '--show menu.. user must ckick.. 
            mContextMenuPasteFileName.Show(txtNewFileName, New Point(ev.X, ev.Y))
        End If
    End Sub  '-txtNewFile mouse up-
    '= = = = = = = = = == = = = = = = = = 
    '-===FF->

    '-- A d d Attachment--
    '-- A d d Attachment--

    Private Sub btnBrowse_Click(sender As Object, _
                                ev As EventArgs) Handles btnBrowse.Click

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnBrowse_Click(sender, ev)
        Exit Sub

    End Sub  '-browse.-
    '= = = = = = = = = = = = 

    Private Sub txtNewComment_TextChanged(sender As Object, _
                                              ev As EventArgs) Handles txtNewComment.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.txtNewComment_TextChanged(sender, ev)
        Exit Sub

    End Sub  '--new comment-
    '= = = = = = = = = = == = = 

    '-- save new File to DB..

    Private Sub btnSave_Click(sender As Object, ev As EventArgs) Handles btnSaveAttachment.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnSaveAttachment_Click(sender, ev)
        Exit Sub


    End Sub  'save new-
    '= = = = = = = = = =
    '-===FF->

    '-- view current doc..
    '-- Doc has been selected from listView..

    Private Sub btnViewDoc_Click(sender As Object, ev As EventArgs) Handles btnViewDoc.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnViewDoc_Click(sender, ev)
        Exit Sub

    End Sub  '--  view -
    '= = = = = = = = = = = = = = = = =

    '-- Delete -

    Private Sub btnDelete_Click(sender As Object, ev As EventArgs) Handles btnDelete.Click

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnDelete_Click(sender, ev)
        Exit Sub

    End Sub  '--delete-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- list vew=  selection changed..--

    Private Sub lvwDocs_SelectedIndexChanged(sender As Object, _
                                              ev As EventArgs) Handles lvwDocs.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.lvwDocs_SelectedIndexChanged(sender, ev)
        Exit Sub

    End Sub '-SelectedIndexChanged-
    '= = = = = = = = = = = = = = = 

    '--listViewDocs_DblClick--

    Private Sub lvwDocs_DblClick(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles lvwDocs.DoubleClick

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.lvwDocs_DblClick(eventSender, eventArgs)
        Exit Sub

    End Sub '--listView_dblClick--
    '= = = = = = = =  =


    '--Exit-

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click


        Me.Close()

    End Sub  '--exit--
    '= = = = = = = =  == = = = 


End Class  '--frmAttachments-
'= = = = = = = = = = = = = = =
'==
'== end form ==
'= = = = == = =  ==== = == = =