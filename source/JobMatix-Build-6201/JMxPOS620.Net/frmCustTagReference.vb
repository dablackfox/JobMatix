
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
'= Imports System.Reflection
'== Imports System.IO
'== Imports System.Drawing.Printing
Imports System.Windows.Forms.Application


Public Class frmCustTagReference

    '- Show Customer TAG types list in Listbox and 
    '--    invite adding, removing and re-positioning of items...
    '--  Update re-ordered list..   or "cancelled"
    '= = =
    '==
    '= grh 23-Jan-2020-=
    '=
    '= = = = = = = = = = = = = = = = = = = = = ==  =

    Private Const k_MAX_TAGSIZE As Integer = 30

    Private mCnnSql As OleDbConnection

    Private mbLoadDone As Boolean = False
    Private mClsTags1 As clsTags

    '= paylist is semi-colon separated..
    Private mColOriginalTagRefList As Collection


    Private mbCancelled As Boolean = False
    Private mbDataChanged As Boolean = False

    '-result-
    Private mcolNewTagList As Collection
    '= = = = = = = = = = = = = = =  = =

    '-result-
    '-result-

    ReadOnly Property newPayList() As Collection
        Get
            newPayList = mcolNewTagList
        End Get
    End Property '-selectedRow-
    '= = = = = =  = = = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property '-cancelled-
    '= = = = = =  = = = = = = = =
    '-===FF->

    '-- new--constructor..

    Public Sub New(ByRef cnn1 As OleDbConnection)

        ' This call is required by the designer.
        InitializeComponent()

        '- Add any initialization after the InitializeComponent() call.
        '= mColOriginalTagList = colPayTypesList
        mCnnSql = cnn1

    End Sub  '-new-
    '= = = = = = == =  =

    '-- Load --

    Private Sub frmTags_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call CenterForm(Me)
        '-- get CUST tag reference list, and load into ref ListBox.

        mClsTags1 = New clsTags(mCnnSql)
        '-mColOriginalTagRefList=
        txtNewTags.Text = ""
        Me.Text = "Customer Tags Reference"

    End Sub  '-load-
    '= = = = =  ==  = =

    '-shown..

    Private Sub frmTags_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        listRefTags.Items.Clear()

        If mClsTags1.GetCustTagRefList(mColOriginalTagRefList) Then
            '--load to list box-
            For Each sTag As String In mColOriginalTagRefList
                listRefTags.Items.Add(sTag)
            Next
        Else  '- no tags defined yet...-
            mColOriginalTagRefList = New Collection
            MsgBox("No Cust. tags defined yet.", MsgBoxStyle.Information)
        End If
        mbLoadDone = True

    End Sub  '-shown.
    '= = = = = = = = = = =  =
    '-===FF->

    '- Moving Items.

    Private Sub mbMoveItem(ByVal intDirection As Integer)
        Dim sItemSelected As String

        '--  Checking selected item
        If (listRefTags.SelectedItem Is Nothing) Or (listRefTags.SelectedIndex < 0) Then
            '--   return; // No selected item - nothing to do
            Exit Sub
        End If
        '--  Calculate new index using move direction
        Dim intNewIndex As Integer = listRefTags.SelectedIndex + intDirection

        '--Checking bounds of the range
        If (intNewIndex < 0) Or (intNewIndex >= listRefTags.Items.Count) Then
            '--  return; // Index out of range - nothing to do
            Exit Sub
        End If

        sItemSelected = listRefTags.SelectedItem

        '--  Removing removable element
        listRefTags.Items.Remove(sItemSelected)
        '--  Insert it in new position
        listRefTags.Items.Insert(intNewIndex, sItemSelected)
        '-- Restore selection
        listRefTags.SetSelected(intNewIndex, True)
        mbDataChanged = True

        '- save latest list order..
        mcolNewTagList = New Collection
        For Each sItem As String In listRefTags.Items
            mcolNewTagList.Add(sItem, sItem)  '-make key as well.  for caller.
        Next sItem

    End Sub  '-move item-
    '= = = = = = = = = == = = =

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click

        If Not mbLoadDone Then Exit Sub
        Call mbMoveItem(-1)
    End Sub  '-move up-
    '= = = = = = = = = = = 

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click

        If Not mbLoadDone Then Exit Sub
        Call mbMoveItem(1)

    End Sub  '-move down..
    '= = = = = = = = = = = == = =
    '-===FF->

    '--List Box ==

    '-- make Delete Key Available.

    Private Sub listRefTags_PreviewKeyDown(sender As Object, _
                                        eventArgs As PreviewKeyDownEventArgs) Handles listRefTags.PreviewKeyDown
        Select Case eventArgs.KeyCode
            Case Keys.Delete
                eventArgs.IsInputKey = True
            Case Else
        End Select
    End Sub  '-listRefTags_PreviewKeyDown=
    '= = = = = = = = = = =  = = = = = = =

    '-  catch delete key..

    Private Sub listRefTags_KeyDown(sender As Object, _
                                        eventArgs As KeyEventArgs) Handles listRefTags.KeyDown

        Select Case eventArgs.KeyCode
            Case Keys.Delete
                '== eventArgs.IsInputKey = True
                '- If An item is selected, then confirm deletion..
                '--  Checking selected item
                If (listRefTags.SelectedItem Is Nothing) OrElse (listRefTags.SelectedIndex < 0) Then
                    '--   return; // No selected item - nothing to do
                    Exit Sub
                End If
                '-- Delete this item  ?
                If (MessageBox.Show("Delete '" & listRefTags.SelectedItem & "' ?", "Deleting..", _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                          MessageBoxDefaultButton.Button1) <> Windows.Forms.DialogResult.Yes) Then
                    Exit Sub
                End If  '-yes-
                '-- delete selected item..
                Try
                    listRefTags.Items.Remove(listRefTags.SelectedItem)
                    mbDataChanged = True
                Catch ex As Exception
                    MsgBox("Failed to remove Item." & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            Case Else
        End Select
    End Sub  '-listRefTags_KeyDown-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--DoubleClick=  to Edit item..

    Private Sub listRefTags_DoubleClick(sender As Object, _
                                                  ev As EventArgs) Handles listRefTags.DoubleClick
        Dim sNewItem As String

        If (listRefTags.SelectedItem Is Nothing) Or (listRefTags.SelectedIndex < 0) Then
            Exit Sub
        End If
        '--  Save index for replacing..
        Dim intNewIndex As Integer = listRefTags.SelectedIndex 
        Dim sItemSelected As String = listRefTags.SelectedItem

        sNewItem = InputBox("Edit the Tag text and press OK.", "", sItemSelected)
        If sNewItem = "" Then
            Exit Sub  '--cancelled.
        End If
        '-- replace with new version..
        '--  Removing old element
        listRefTags.Items.RemoveAt(intNewIndex)
        '--  Insert it in new position
        listRefTags.Items.Insert(intNewIndex, sNewItem)
        '-- Restore selection
        listRefTags.SetSelected(intNewIndex, True)
        mbDataChanged = True

    End Sub '-listRefTags_DoubleClick-
    '= = = = = = = = =  = = = = = == =

    '-listRefTags_SelectedIndexChanged-

    Private Sub listRefTags_SelectedIndexChanged(sender As Object, _
                                                  ev As EventArgs) Handles listRefTags.SelectedIndexChanged
    End Sub  '-listRefTags_SelectedIndexChanged-
    '= = = = = = = = ==  == =  = =

    '-- new tags text box..
    '-txtNewTags_TextChanged-

    Private Sub txtNewTags_TextChanged(sender As Object, e As EventArgs) Handles txtNewTags.TextChanged

        If Trim(txtNewTags.Text) <> "" Then
            btnAddToList.Enabled = True
        Else
            btnAddToList.Enabled = False
        End If

    End Sub  '-txtNewTags_TextChanged-
    '= = = = = =  = = = = = = = = = = 
    '-===FF->
    '-- validate-

    Private Sub txtNewTags_validating(sender As Object, _
                                      ev As System.ComponentModel.CancelEventArgs) Handles txtNewTags.Validating
        '-- check it doesn't contain "[,]" or comma..
        Dim sData As String = txtNewTags.Text

        If (InStr(sData, "[") > 0) Or (InStr(sData, "]") > 0) Or _
                                              (InStr(sData, ",") > 0) Or _
                                              (InStr(sData, "'") > 0) Or (InStr(sData, """") > 0) Then
            ev.Cancel = True
            MsgBox("Tags can't contain the chars ""["", ""]"", commas or quotes..")
        End If  '- instr-

    End Sub  '-validating
    '= = = = = =  = = = = = = = = = = 
    '-===FF->

    '--btnAddToList-

    Private Sub btnAddToList_Click(sender As Object, e As EventArgs) Handles btnAddToList.Click

        '== btnAddToList-

        If (Trim(txtNewTags.Text) <> "") Then
            Dim sItemList() As String = Split(Trim(txtNewTags.Text), vbCrLf)
            Dim bTooLong As Boolean = False

            If (sItemList IsNot Nothing) Then
                For Each sTag As String In sItemList
                    If Trim(sTag).Length > k_MAX_TAGSIZE Then
                        bTooLong = True
                    End If
                    If Trim(sTag) <> "" Then
                        listRefTags.Items.Add(VB.Left(Trim(sTag), k_MAX_TAGSIZE))
                        mbDataChanged = True
                    End If
                Next sTag
                If bTooLong Then
                    MsgBox("Note- Items may have been truncated if more than " & k_MAX_TAGSIZE & " chars..", MsgBoxStyle.Information)
                End If
                btnAddToList.Enabled = False
                txtNewTags.Text = ""
            End If  '--nothing-
        End If
    End Sub
    '= = = = = = = = = = = = = == 
    '-===FF->

    '-ok-  S a v e.

    Private Sub btnSave_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnSaveRefList.Click

        Dim colNewTagList As Collection
        If (listRefTags.Items.Count > 0) Then
            colNewTagList = New Collection
            For Each sTag As String In listRefTags.Items
                colNewTagList.Add(sTag)
            Next sTag
            If colNewTagList.Count > 0 Then
                If mClsTags1.SaveCustTagRefList(colNewTagList) Then
                    MsgBox("New List was saved..", MsgBoxStyle.Information)
                Else
                    MsgBox("ERROR- Failed to save new list..", MsgBoxStyle.Exclamation)
                End If
                mbDataChanged = False   '--for Form closing..
            End If
        Else
            MsgBox("No items to save..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '- can't close form from here..  loses results.
        Me.Hide()
    End Sub '-ok-
    '= = = = = = = = = = = = = = = = =

    '-- cancel-

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnCancel.Click
        If Not mbDataChanged Then  '=OK for formClosing event-
            '= intCancel = 0 '--OK.  let it go---
            mbCancelled = True
            Me.Hide()
        Else  '- changes..
            If MsgBox("Abandon Changes ? ", _
                        MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                mbCancelled = True
                mbDataChanged = False   '--for Form closing..
                Me.Hide()
            Else '--stay here-
                '--cant close yet--'--was mistake..  keep going..
            End If  '-yes/no-
        End If  '-selected
    End Sub  '-cancel-
    '= = = = = = = = = = = 
    '-===FF->

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmTags_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        '== MsgBox("test- Have FormClosing Event..", MsgBoxStyle.Information)
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                               System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
                '== MsgBox("test- Have Form Owner Closing Event..", MsgBoxStyle.Information)
                '= mbCancelled = True
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                '- clicked the X..
                If Not mbDataChanged Then  '=OK for formClosing event-
                    intCancel = 0 '--OK.  let it go---
                Else  '- NOT from OK buton..
                    If MsgBox("Abandon Changes ? ", _
                                MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        mbCancelled = True
                        intCancel = 0 '--let it go---
                    Else '--stay here-
                        intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                    End If  '-yes/no-
                End If  '-selected
            Case Else
                '=MsgBox("test- Have Case Else Closing Event..", MsgBoxStyle.Information)
                '= HIDING the from gives this Close Reason.-
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel

    End Sub  '-closing-
    '= = = = = = = = = = = = =


End Class '-- frmTags'-
'= = = = = == = = = = = = = = == = 