Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
'= Imports System.Reflection
'== Imports System.IO
'== Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports system.data.OleDb

Public Class frmListSelect

    '- Show Datatable in ListView and invite selction..
    '--  Return dt Row No..   or "cancelled"

    '==
    '==  grh JobMatixPOS 3.1.3101.1102 -
    '==       >> Implementing form.
    '==
    '==   3403.1104 04-Nov-2017=
    '==     >>-- formatting money amounts in listview..
    '==
    '=    3519.0211-  11-Feb-2019=
    '=      Check data Columns for null. (ie dates.)
    '==
    '= = = =  = = = = = = = = = = = = = =  == = =  = = = = == = = =

    Private mbLoadDone As Boolean = False

    Private mbCancelled As Boolean = False
    Private mbSelectedOK As Boolean = False

    Private mDataTable1 As DataTable

    Private mIntSelectedRow As Integer = -1

    Private mIntFormDesignHeight As Integer = -1
    Private mIntFormDesignWidth As Integer = -1

    '=3403.1105 --
    Private mColSqlHeaderTypes As Collection  '--helps with formatting.

    '= = = = = =  = = = = = = = = = = ==  =

    WriteOnly Property hdrText() As String
        Set(ByVal value As String)
            labHdr1.Text = value
        End Set
    End Property

    WriteOnly Property inData() As DataTable
        Set(ByVal value As DataTable)
            mDataTable1 = value
        End Set
    End Property '-indata-
    '= = = = = = = = = = = = 

    '=3403.1105 --
    '==  mColSqlHeaderTypes As Collection
    WriteOnly Property SqlHeaderTypes As Collection
        Set(value As Collection)
            mColSqlHeaderTypes = value
        End Set
    End Property '-types-
    '= = == = = = = = = =  === =

    '-result-
    '-result-

    ReadOnly Property selectedRow() As Integer
        Get
            selectedRow = mIntSelectedRow
        End Get
    End Property '-selectedRow-
    '= = = = = =  = = = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property '-cancelled-
    '= = = = = =  = = = = = = = =

    '-- Load  listView from recordset..--
    '== v3.1.3101 -- ADO.net oleDb=

    Private Function mbLoadListView(ByRef rs1 As DataTable, _
                                    ByRef ListView1 As System.Windows.Forms.ListView) As Integer

        Dim s1, sName As String
        Dim dataCol1 As DataColumn '--fldx As ADODB.Field
        Dim lngNoCols, ix As Integer
        Dim lCount As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim Header1 As ColumnHeader

        lngNoCols = 0
        lCount = 0
        mbLoadListView = -1
        '-- create column headers...--
        ListView1.Items.Clear()
        ListView1.Columns.Clear()
        For Each dataCol1 In rs1.Columns   '== fldx In rs1.Fields
            lngNoCols = lngNoCols + 1
            s1 = "" & dataCol1.ColumnName  '== CStr(fldx.Name)
            Header1 = ListView1.Columns.Add("", s1, CInt((ListView1.Width) \ 5))
            If (dataCol1.DataType Is System.Type.GetType("System.Decimal")) Or _
                    (dataCol1.DataType Is System.Type.GetType("System.Int32")) Then
                Header1.TextAlign = HorizontalAlignment.Right
                Header1.Width = 70
            End If
        Next dataCol1  '=fldx '--fldx  --
        '--MsgBox "Headers loaded...", vbInformation

        '--fill list box with record fields--
        If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ok.. not empty--
            '==rs1.MoveFirst()
            ListView1.Items.Clear() : lCount = 0
            '--scan recordset and load--
            For Each dataRow1 As DataRow In rs1.Rows
                '---load current item--
                For ix = 0 To (lngNoCols - 1) '--ALL flds..-
                    '=3519.0211- Column might be null. (ie dates.)
                    '=3519.0211- Column might be null. (ie dates.)
                    s1 = ""  '-default-
                    If Not IsDBNull(dataRow1.Item(ix)) Then
                        s1 = dataRow1.Item(ix).ToString
                        '==   3403.1104 04-Nov-2017=
                        sName = rs1.Columns(ix).ColumnName
                        'If rs1.Columns(ix).DataType Is System.Type.GetType("System.Decimal") Then
                        '    s1 = RSet(FormatCurrency(dataRow1.Item(ix), 2), 11)
                        'End If
                        If (rs1.Columns(ix).DataType Is System.Type.GetType("System.Boolean")) Then
                            If dataRow1.Item(ix) Then
                                s1 = "Yes"
                            Else
                                s1 = "N"
                            End If
                        ElseIf (rs1.Columns(ix).DataType Is System.Type.GetType("System.DateTime")) Then
                            s1 = Format(dataRow1.Item(ix), "dd-MMM-yyyy  HH:mm")
                        ElseIf (mColSqlHeaderTypes IsNot Nothing) AndAlso mColSqlHeaderTypes.Contains(sName) Then
                            '-- get sql type this column.
                            If InStr(LCase(mColSqlHeaderTypes.Item(sName)), "money") > 0 Then
                                s1 = RSet(FormatCurrency(dataRow1.Item(ix), 2), 11)
                            End If
                        End If
                    End If  '--null-
                    If ix = 0 Then
                        item1 = ListView1.Items.Add(s1)  '--First col does CREATE..-
                    Else
                        item1.SubItems.Add(s1)
                    End If
                Next ix
                lCount = lCount + 1
                item1.Tag = CStr(lCount) '--ID of this part item..-
            Next dataRow1
        Else
            '==  MsgBox sEmptyMsg, vbInformation
        End If '--not empty--
        mbLoadListView = lCount
    End Function '--load list..-
    '= = = = = = =  =
    '-===FF->

    '-- Load Event-

    Private Sub frmListSelect_Load(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Load

        If mDataTable1 Is Nothing Then
            MsgBox("No input data table to show ", MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
        End If
        btnOK.Enabled = False

        mIntFormDesignHeight = Me.Height '--save starting dimensions..-
        mIntFormDesignWidth = Me.Width '--save starting dimensions..-

        Call CenterForm(Me)

        '-- load listview from dataTable.-

        Call mbLoadListView(mDataTable1, ListView1)

        mbLoadDone = True

    End Sub  '-load-
    '= = = = = = =  = = = =
    '-- form resized --

    Private Sub frmListSelect_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If (Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized) Then

            '--  cant make smaller than original..-
            If (Me.Height < mIntFormDesignHeight) Then Me.Height = mIntFormDesignHeight
            If (Me.Width < mIntFormDesignWidth) Then Me.Width = mIntFormDesignWidth

            ListView1.Width = Me.Width - 80
            ListView1.Height = Me.Height - 240

            btnOK.Top = ListView1.Top + ListView1.Height + 20
            btnOK.Left = Me.Width - 180

            btnCancel.Top = btnOK.Top
            btnCancel.Left = Me.Width - 100

        End If  '-window--

    End Sub  '-resize- 
    '= = = = = = =  =
    '-===FF->


    '-- Sel. index changed --

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, _
                                                  ByVal e As System.EventArgs) _
                                                    Handles ListView1.SelectedIndexChanged
        If Not mbLoadDone Then Exit Sub

        If Not ListView1.SelectedIndices Is Nothing Then
            If (ListView1.SelectedIndices.Count > 0) Then
                mIntSelectedRow = ListView1.SelectedIndices.Item(0)
                btnOK.Enabled = True
            End If
        End If  '--nothing-

    End Sub '-- Sel. index changed --
    '= = = = = = = = = = = = = = = = = =

    '--  Listview Clicking -

    '--listViewJobs_Click--

    Private Sub listView1_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles ListView1.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer

        '--  update quote info display if selection has moved..--
        item1 = ListView1.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            mIntSelectedRow = item1.Index
            btnOK.Enabled = True
            '=btnOK.Select()
        End If '--selected..-
    End Sub  '-click-
    '= = = = = = = = = = = =  = =

    Private Sub listView1_dblClick(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles ListView1.DoubleClick
        Dim item1 As System.Windows.Forms.ListViewItem
        item1 = ListView1.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            '== Call cmdViewRecord_Click()
            '- Do ok..
            If Not ListView1.SelectedIndices Is Nothing Then
                If (ListView1.SelectedIndices.Count > 0) Then
                    mIntSelectedRow = ListView1.SelectedIndices.Item(0)
                    mbSelectedOK = True  '= for formClosing event-
                    '- can't close form from here..  loses results.
                    Me.Hide()
                    Exit Sub
                End If
            End If  '--nothing-
        End If
    End Sub '-dbl click-
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-ok-

    Private Sub btnOK_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnOK.Click

        mbSelectedOK = True  '= for formClosing event-
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
                If mbSelectedOK Then  '=OK for formClosing event-
                    intCancel = 0 '--OK.  let it go---
                Else  '- NOT from OK buton..
                    If MsgBox("Abandon Selection ? ", _
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

End Class  '-frmListSelect-

'== end form ==