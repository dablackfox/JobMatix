
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
'= Imports System.Reflection
'== Imports System.IO
'== Imports System.Drawing.Printing
Imports System.Windows.Forms.Application

Public Class frmShowCustomerTags
    Inherits System.Windows.Forms.Form

    Private Const k_MAX_TAG_CUST_COLUMN_SIZE As Integer = 2000

    '- Show Customer TAG list for selected customer in Listbox and 
    '--    invite adding, removing via item check boxes...
    '= = =
    '==
    '= grh 23-Jan-2020-=
    '=
    '= = = = = = = = = = = = = = = = = = = = = ==  =

    Private mCnnSql As OleDbConnection
    Private mIntCustomer_id As Integer = -1

    Private mbLoadDone As Boolean = False
    Private mClsTags1 As clsTags

    '= Ref list..  We chhose from here for each Cust...
    Private mColOriginalTagRefList As Collection

    '= Cust list..  Srating off on file for thid Cust...
    '= Private mColOriginalCustTagList As Collection

    Private mbCancelled As Boolean = False
    Private mbDataChanged As Boolean = False

    '- starting list..
    Private msOldCustTagList As String = ""
    Private mColOldCustTagList As Collection

    '-result-
    Private msNewCustTagList As String = ""
    Private mColNewCustTagList As Collection
    '= = = = = = = = = = = = = = =  = =

    '-- Input is customer_id
    WriteOnly Property customer_id As Integer
        Set(value As Integer)
            mIntCustomer_id = value
        End Set
    End Property  '-customer_id-
    '= = = = = = = = = = = = ==

    '--Results..

    '-- Updated Tags for this customer..  for display..

    ReadOnly Property UpdatedTags As Collection
        Get
            UpdatedTags = mColNewCustTagList
        End Get
    End Property

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

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '-===FF->

    '--L o a d --

    Private Sub frmShowCustomerTags_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '- Create Customer Column for Tags  if not existing..
        '== THIS has to go in the StartUp class  clsJMxPOS31..
        '-- ALSO--  Add column to Customer Table in modCreatePOSdb..

        '-- THIS to go to startup..
        '-- THIS to go to startup..
        '-- THIS to go to startup..

        'Dim sColDef, smsg As String
        'Dim bWasAdded As Boolean
        ''- Must have customer tags-
        'sMsg = ""
        'bWasAdded = False
        'sColDef = " VARCHAR(2000) NOT NULL DEFAULT '' "
        'If Not gbAddColumnToTable(mCnnSql, "Customer", "tags", sColDef, bWasAdded) Then
        '    MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        'Else  '-ok=
        '    If bWasAdded Then
        '        smsg &= "-- The new column 'tags' " & vbCrLf &
        '               "  was added (ok) to the Customer Table.." & vbCrLf
        '    End If
        'End If
        ''- Report new customer columns..
        'If smsg <> "" Then
        '    MsgBox("Table updated- " & vbCrLf & smsg, MsgBoxStyle.Information)
        'End If

        '-- END of  THIS to go to startup..
        '-- END of  THIS to go to startup..
        '-- END of  THIS to go to startup..

        btnSaveCustList.Enabled = False
        '-- get reference list, and load into chkListBox.

        mClsTags1 = New clsTags(mCnnSql)

        '= mClsTags1.GetCustTagRefList(mColOriginalTagRefList)

        checkedListRefTags.Items.Clear()
        Call CenterForm(Me)

    End Sub  '-load-
    '= = = = = = = = = = =
    '-===FF->

    '--Shown.

    Private Sub frmShowCustomerTags_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        If Not mClsTags1.GetCustTagRefList(mColOriginalTagRefList) Then
            '--No reference list..
            MsgBox("No Tag reference list found.. " & mIntCustomer_id, MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
            Exit Sub
        End If

        mColOldCustTagList = New Collection

        '- load reference list..
        For Each sTag As String In mColOriginalTagRefList
            checkedListRefTags.Items.Add(sTag)
        Next sTag

        '-- untick them all..
        If (checkedListRefTags.Items.Count > 0) Then
            For intIndex As Integer = 0 To (checkedListRefTags.Items.Count - 1)
                checkedListRefTags.SetItemChecked(intIndex, False)  '=(intIndex).checked = False
            Next intIndex
        Else
            '--No reference list..
            MsgBox("No Tag reference list found.. " & mIntCustomer_id, MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
            Exit Sub
        End If  '- count

        '-- Get current Cust tags and check them onto the Ref ListBox..
        Dim sBarcode As String
        Dim sSql, sName, sCompany As String
        Dim colResult, colRecord As Collection

        '--  get CUSTOMER record as collection for SELECT..--

        If mClsTags1.GetCustomerTags(mIntCustomer_id, sBarcode, sName, mColOldCustTagList) Then
            '-ok-
            txtCustName.Text = sName
            txtCustBarcode.Text = sBarcode
            txtCustomer_id.Text = "_Id= " & mIntCustomer_id
        Else
            MsgBox("Failed to get Customer tag list.." & vbCrLf & _
                                  """" & msOldCustTagList & """", MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
            Exit Sub
        End If
        '-ok=
        '= msOldCustTagList = Trim(colRecord.Item("Tags")("value"))

        '-- Check original cust tags onto ref list..

        'If (msOldCustTagList <> "") Then
        '    '- decode into a collection.-
        '    If mClsTags1.DecodeTagList(msOldCustTagList, mColOldCustTagList) Then
        '-- match text of each old tag to the REf. list..
        For Each sOldTag As String In mColOldCustTagList
            For intIndex As Integer = 0 To (checkedListRefTags.Items.Count - 1)
                '=checkedListRefTags.SetItemChecked(intIndex, False)  '=(intIndex).checked = False
                If LCase(checkedListRefTags.Items(intIndex)) = LCase(sOldTag) Then
                    '-found=
                    checkedListRefTags.SetItemChecked(intIndex, True)  '-flag as this cust..
                    Exit For  '-- exit inner for..
                End If
            Next intIndex
        Next sOldTag

        mbLoadDone = True

    End Sub  '-shown..
    '= = = = = = == = =  = 
    '-===FF->

    '-- checkedListRefTags_SelectedIndexChanged-

    Private Sub checkedListRefTags_SelectedIndexChanged(sender As Object, _
                                                         ev As EventArgs) Handles checkedListRefTags.SelectedIndexChanged

    End Sub  '-checkedListRefTags_SelectedIndexChanged
    '= = = = = = = = = = = = =  = = = = = = = = = = = =


    '-checkedListRefTags_ItemCheck=
    '-- A checkBox changed..

    Private Sub checkedListRefTags_ItemCheck(sender As Object, _
                                               ev As EventArgs) Handles checkedListRefTags.ItemCheck
        If Not mbLoadDone Then Exit Sub

        mbDataChanged = True
        btnSaveCustList.Enabled = True
    End Sub  '-checkedListRefTags_itemCheck
    '= = = = = = = = = = = = =  = = = = = = = = = = = =

    '-cancel-

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

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
    End Sub  '--cancel-
    '= = = = = = = = = = = = =
    '-===FF->

    '- Save --

    Private Sub btnSaveCustList_Click(sender As Object, e As EventArgs) Handles btnSaveCustList.Click
        Dim s1, sMsg, sErrorMsg As String

        '-- transfer checked items to the Result collection..
        mColNewCustTagList = New Collection

        For intIndex As Integer = 0 To (checkedListRefTags.Items.Count - 1)
            With checkedListRefTags
                If .GetItemCheckState(intIndex) = CheckState.Checked Then
                    mColNewCustTagList.Add(.Items(intIndex))
                End If
            End With
        Next intIndex

        '-- Make new string list of tags, and Update Customer Record (Tags)
        Dim sNewTagList As String = ""
        If Not mClsTags1.MakeTagList(mColNewCustTagList, sNewTagList) Then
            MsgBox("No new string list was created..", MsgBoxStyle.Exclamation)
        Else '--ok  
            '-update Cust record..
            '-- Check we don't bust the column.
            '-k_MAX_TAG_CUST_COLUMN_SIZE-
            s1 = msFixSqlStr(sNewTagList).Length
            If (s1 > k_MAX_TAG_CUST_COLUMN_SIZE) Then
                MsgBox("Too many Tags..  You'll have to drop some...", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            Dim sSql As String
            Dim sqlCmd1 As OleDbCommand
            Dim intAffected As Integer
 
            sSql = "UPDATE dbo.customer SET Tags='" & msFixSqlStr(sNewTagList) & "' "
            sSql &= "  WHERE (customer_id=" & CStr(mIntCustomer_id) & "); "
            Try
                sqlCmd1 = New OleDbCommand(sSql, mCnnSql)
                '= mCnnSql.ChangeDatabase(msSqlDbName)
                intAffected = sqlCmd1.ExecuteNonQuery()
                MsgBox("Tags were updated ok.." & vbCrLf & "  and " & intAffected & " row(s) affected..", MsgBoxStyle.Information)
            Catch ex As Exception
                mbCancelled = True
                sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
                sErrorMsg = "Saving Customer Tags: Error in Executing Sql: " & vbCrLf & _
                              sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                                   "--- end of error msg.--" & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sErrorMsg)
                MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            End Try
        End If
        mbDataChanged = False   '--for Form closing..
        Me.Hide()

    End Sub  '-btnSaveCustList_Click-
    '= = = = = =  = = = = = = = = == 
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


End Class  '-frmShowCustomerTags-
'= = = = = = = = = = = = = = =  =