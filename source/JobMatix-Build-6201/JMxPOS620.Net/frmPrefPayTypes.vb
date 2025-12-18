
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
'= Imports System.Reflection
'== Imports System.IO
'== Imports System.Drawing.Printing
Imports System.Windows.Forms.Application

Public Class frmPrefPayTypes
    Inherits Form

    '- Show INPUT Payment types list in Listbox and invite re-positioning of items...
    '--  Return re-ordered list..   or "cancelled"
    '= = =
    '==
    '= grh 01-July-2019-=
    '=
    '= = = = = = = = = = = = = = = = = = = = = ==  =

    '--Thanks to- "Save" at StackOverflow.

    '    https://stackoverflow.com/questions/4796109/how-to-move-item-in-listbox-up-and-down

    '-- public void MoveUp()
    '-- {
    '--     MoveItem(-1);
    '-- }

    '-- public void MoveDown()
    '-- {
    '--    MoveItem(1);
    '-- }

    '-- public void MoveItem(int direction)
    '-- {
    '--    // Checking selected item
    '--    if (listBox1.SelectedItem == null || listBox1.SelectedIndex < 0)
    '--        return; // No selected item - nothing to do

    '--    // Calculate new index using move direction
    '--    int newIndex = listBox1.SelectedIndex + direction;

    '--    // Checking bounds of the range
    '--    if (newIndex < 0 || newIndex >= listBox1.Items.Count)
    '--        return; // Index out of range - nothing to do

    '--    object selected = listBox1.SelectedItem;

    '--    // Removing removable element
    '--    listBox1.Items.Remove(selected);
    '--    // Insert it in new position
    '--    listBox1.Items.Insert(newIndex, selected);
    '--    // Restore selection
    '--    listBox1.SetSelected(newIndex, true);
    '--}
    '= = = = = = ==  == =  = = =
    '-===FF->


    Private mbLoadDone As Boolean = False

    '= paylist is semi-colon separated..
    Private mColOriginalPayList As Collection


    Private mbCancelled As Boolean = False
    Private mbSelectionChanged As Boolean = False

    '-result-
    Private mcolNewPayList As Collection
    '= = = = = = = = = = = = = = =  = =

    '-result-
    '-result-

    ReadOnly Property newPayList() As Collection
        Get
            newPayList = mcolNewPayList
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

    Public Sub New(ByRef colPayTypesList As Collection)

        ' This call is required by the designer.
        InitializeComponent()

        '- Add any initialization after the InitializeComponent() call.
        mColOriginalPayList = colPayTypesList

    End Sub  '-new-
    '= = = = = = == =  =


    '- Load=

    Private Sub frmPrefPayTypes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call CenterForm(Me)

        ListBox1.Items.Clear()
        Me.Text = "Payment Types- Preferred order."

        ''-- set up list box..
        If mColOriginalPayList IsNot Nothing Then
            For Each sType As String In mColOriginalPayList
                If (Trim(sType) <> "") Then
                    ListBox1.Items.Add(Trim(sType))
                End If
            Next sType
        End If  '-nothing-

    End Sub  '-load-
    '= = = = = = = = = =

    '-shown.

    Private Sub frmPrefPayTypes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        If (ListBox1.Items.Count < 1) Then
            mbCancelled = True

            MsgBox("No items to re-order !!", MsgBoxStyle.Exclamation)
            Me.Hide()
        End If

        mbLoadDone = True
    End Sub  '--shown.
    '= = = = = = = = === 
    '-===FF->

    '- Moving Items.

    Private Sub mbMoveItem(ByVal intDirection As Integer)
        Dim sItemSelected As String

        '--  Checking selected item
        If (ListBox1.SelectedItem Is Nothing) Or (ListBox1.SelectedIndex < 0) Then
            '--   return; // No selected item - nothing to do
            Exit Sub
        End If
        '--  Calculate new index using move direction
        Dim intNewIndex As Integer = ListBox1.SelectedIndex + intDirection

        '--Checking bounds of the range
        If (intNewIndex < 0) Or (intNewIndex >= ListBox1.Items.Count) Then
            '--  return; // Index out of range - nothing to do
            Exit Sub
        End If

        sItemSelected = ListBox1.SelectedItem

        '--  Removing removable element
        ListBox1.Items.Remove(sItemSelected)
        '--  Insert it in new position
        ListBox1.Items.Insert(intNewIndex, sItemSelected)
        '-- Restore selection
        ListBox1.SetSelected(intNewIndex, True)
        mbSelectionChanged = True

        '- save latest list order..
        mcolNewPayList = New Collection
        For Each sItem As String In ListBox1.Items
            mcolNewPayList.Add(sItem, sItem)  '-make key as well.  for caller.
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

    '-ok-

    Private Sub btnSave_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnSave.Click

        '= mbSelectedOK = True  '= for formClosing event-
        '- can't close form from here..  loses results.
        Me.Hide()
    End Sub '-ok-
    '= = = = = = = = = = =

    '-- cancel-

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnCancel.Click
        mbCancelled = True
        '- can't close form from here..  loses results.
        Me.Hide()

    End Sub  '-cancel-
    '= = = = = = = = = = = 
    '-===FF->

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmListSelect_FormClosing(ByVal eventSender As System.Object, _
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
                If Not mbSelectionChanged Then  '=OK for formClosing event-
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



    'Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    'End Sub


End Class '-frmPrefPayTypes-
'= = = = = = = =  = =