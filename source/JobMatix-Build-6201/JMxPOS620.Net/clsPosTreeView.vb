
Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Windows.Forms


Public Class clsPosTreeView
    Inherits System.Windows.Forms.TreeView

    Private Const TVM_SETEXTENDEDSTYLE As Integer = &H1100 + 44
    Private Const TVS_EX_DOUBLEBUFFER As Integer = &H4
    Dim currentNode As TreeNode
    Public Hot_Tracking_Node As TreeNode
    Private Declare Function SendMessageA Lib "User32" (ByVal hWnd As IntPtr, _
                                                        ByVal m As Integer, _
                                                        ByVal wParam As IntPtr, _
                                                        ByVal lParam As IntPtr) As IntPtr

    Public Sub New()
        MyBase.New()
    End Sub
    '= = = == =  ==

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        SendMessageA(Me.Handle, TVM_SETEXTENDEDSTYLE, _
                         CType(TVS_EX_DOUBLEBUFFER, IntPtr), _
                             CType(TVS_EX_DOUBLEBUFFER, IntPtr))
        MyBase.OnHandleCreated(e)
    End Sub
    '= = = = = ==  =

    'Private Sub TreeView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    '    currentNode = Me.GetNodeAt(e.X, e.Y)
    '    If currentNode IsNot Hot_Tracking_Node Then
    '        Hot_Tracking_Node = currentNode
    '        Me.Refresh()
    '    End If
    'End Sub
    '= = = ==  = =

    'Private Sub treeview1_drawnode(ByVal sender As Object, ByVal e As DrawTreeNodeEventArgs) Handles Me.DrawNode

    '    If e.Node.Equals(Hot_Tracking_Node) Then
    '        e.Graphics.FillRectangle(Brushes.Green, e.Bounds)
    '    End If
    '    e.Graphics.DrawRectangle(Pens.Red, e.Bounds)
    '    e.Graphics.DrawString(e.Node.Text, Me.Font, Brushes.Black, e.Bounds.Location)
    'End Sub


End Class  '-clsPosTreeView-
'= = = = = = = = = = == = 
