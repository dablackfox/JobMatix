Option Explicit On
Option Strict Off

Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class clsDgvGoods
    Inherits DataGridView

    '- grh- 16-June-2017..-

    '-- datagridview sub-class to enable editing of all columns.
    '- we want to capture the ENTER key and turn it into RightArrow, so user can edit along the row.

    '==  https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.processdatagridviewkey(v=vs.90).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-2

    Private _lastEditableColumn As Integer = 5  '= let ENTER go through on this column..

    '-- AND From shapedPanel and CodeGuru-
    <Browsable(True)> _
    Public Property lastEditableColumn() As Integer
        Get
            Return _lastEditableColumn
        End Get
        Set(ByVal Value As Integer)
            If (Value >= 0) AndAlso (Value < Me.ColumnCount) Then
                _lastEditableColumn = Value
            End If
            '= Invalidate()
        End Set
    End Property  '-lastEditableColumn-
    '= = = = = = = = = = = = = = = =  == = =  = =

    '- ProcessDialogKey- 
    '- ProcessDialogKey- 

    <System.Security.Permissions.UIPermission( _
        System.Security.Permissions.SecurityAction.LinkDemand, _
        Window:=System.Security.Permissions.UIPermissionWindow.AllWindows)> _
    Protected Overrides Function ProcessDialogKey( _
        ByVal keyData As Keys) As Boolean

        ' Extract the key code from the key value.  
        Dim key As Keys = keyData And Keys.KeyCode

        ' Handle the ENTER key as if it were a RIGHT ARROW key.  
        If key = Keys.Enter Then
            '-Check if last columns..
            If (Me.CurrentCell.ColumnIndex >= 0) AndAlso (Me.CurrentCell.ColumnIndex < _lastEditableColumn) Then
                '- not last.. so convert ENTER to RightArrow to keeep moving right.
                Return Me.ProcessRightKey(keyData)
            End If
        End If
        '- else- let system have it.-
        '-else- Let DataGridView  handle the ENTER key.
        Return MyBase.ProcessDialogKey(keyData)

    End Function  '-ProcessDialogKey--
    '= = = = = = = = = = = = = = = =  == = =  = =

    '-ProcessDataGridViewKey-
    '-ProcessDataGridViewKey-

    <System.Security.Permissions.SecurityPermission( _
        System.Security.Permissions.SecurityAction.LinkDemand, Flags:= _
        System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)> _
    Protected Overrides Function ProcessDataGridViewKey( _
        ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean

        ' Handle the ENTER key as if it were a RIGHT ARROW key.  
        If e.KeyCode = Keys.Enter Then
            '-Check if last columns..
            If (Me.CurrentCell.ColumnIndex >= 0) AndAlso (Me.CurrentCell.ColumnIndex < _lastEditableColumn) Then
                '- not last.. so convert ENTER to RightArrow to keeep moving right.
                Return Me.ProcessRightKey(e.KeyData)
            End If
            '= Return Me.ProcessRightKey(e.KeyData)
        End If
        '-else- Let DataGridView  handle the ENTER key.
        Return MyBase.ProcessDataGridViewKey(e)

    End Function '-ProcessDataGridViewKey-
    '= = = = = = = =  = = = = = = = = ==  =

End Class '-clsDgvGoods-
'= = = = = = = = = == = =  = 

'== the end-
