Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Data
Imports System.Windows.Forms.Application
'== Imports system.data.sqlclient
Imports system.data.OleDb

Public Class frmGoodsSerials

    '--Goods Received.--

    '-- show/edit list of serials for current stock Item.
    '--  Check DB serials for Duplicate items..-

    '--  NB:  No DB update here..  this is on COMMIT for Goods Received..-
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '== -- Updated 3501.1107  07-Nov-2018=  
    '==      Form cancel button.
    '==
    '== = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== UPDATES to Build 4284.1124  
    '== UPDATES to Build 4284.1124  
    '== UPDATES to Build 4284.1124  
    '==
    '==   Target-New-Build-4287 --  (30-Jan-2021)
    '==
    '==  Goods Received Serials entry...  (Stewart 27-Jan-2021-)
    '==    The system won't let you put in a Serial that's already in the system, 
    '==    Or that you've already entered for that Invoice line.. 
    '==    But it will Let you enter a Serial that was entered In a previous line In that invoice.. 
    '==      (Does this mean you are trying To enter the same product twice In the one invoice ?)
    '== For the next release-  make sure that serial no's are unique over the whole Invoice, 
    '==    And still also not on file already in the system. 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==


    Const k_GRIDCOL_SERIAL As Short = 0

    Private mbActivated As Boolean = False
    '- - - - -
    Private mbIsInitialising As Boolean = False
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private mbCancelled As Boolean = False

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '== SqlConnection '--

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private mIntForm_top As Integer = -1
    Private mIntForm_left As Integer = -1

    '== Private msSerialList As String = ""  '--input/output
    Private mColSerials As Collection   '--input/output

    '==   Target-New-Build-4287 --  
    '==   Target-New-Build-4287 --  
    Private mColOtherSerialsThisInvoice As Collection
    '== END  Target-New-Build-4287 --  
    '== END  Target-New-Build-4287 --  


    Private msStockBarcode As String = ""      '--input-
    Private msStockDescription As String = ""  '--input-
    Private mIntStock_id As Integer = -1       '--input-
    Private mIntQuantity As Integer = 0       '--input-

    Private mbModified As Boolean = False
    Private mbNewSerial As Boolean = False

    Private msSerialsOnFile As String = ";"  '--tracks serials actually on file-
    '= Private msSerialsInGrid = ";"           '--tracks serials currently in dataGridView-

    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Staff Name/Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffId() As Integer
        Set(ByVal Value As Integer)

            mIntStaff_id = Value
        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '-- Sql Connection Info from Sub Main..-
    '-- Sql Connection Info from Sub Main..-

    WriteOnly Property connectionSql() As OleDbConnection   '== SqlConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)
            mColSqlDBInfo = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
        End Set
    End Property  '--dbname--
    '= = = = = = = = = = = = = = = = = = = = =

    WriteOnly Property form_top() As Integer
        Set(ByVal value As Integer)
            mIntForm_top = value
        End Set
    End Property  '--top-
    '= = = = = = = = = = = = = 

    WriteOnly Property form_left() As Integer
        Set(ByVal value As Integer)
            mIntForm_left = value
        End Set
    End Property  '--left-
    '= = = = = = = = = = = = = 

    WriteOnly Property stockBarcode() As String
        Set(ByVal Value As String)
            msStockBarcode = Value
        End Set
    End Property  '--barcode--
    '= = = = = = = = = = = = = = = = = = = = =

    WriteOnly Property stock_id() As Integer
        Set(ByVal Value As Integer)
            mIntStock_id = Value
        End Set
    End Property  '--id--
    '= = = = = = = = = = = = = = = = = = = = =

    WriteOnly Property stockDescription() As String
        Set(ByVal Value As String)
            msStockDescription = Value
        End Set
    End Property  '--descr--
    '= = = = = = = = = = = = = = = = = = = = =

    WriteOnly Property quantity() As Integer
        Set(ByVal Value As Integer)
            mIntQuantity = Value
        End Set
    End Property  '--id--
    '= = = = = = = = = = = = = = = = = = = = =

    '== '-- serial nos..
    '== Property serialList() As String
    '==     Get
    '==         serialList = msSerialList
    '==     End Get
    '==     Set(ByVal value As String)
    '==         msSerialList = value
    '==     End Set
    '== End Property
    '= = = = = =  = = = = = = =

    '-- serial nos.. Input/output.

    Property colSerials() As Collection
        Get
            colSerials = mColSerials
        End Get
        Set(ByVal value As Collection)
            mColSerials = value
        End Set
    End Property
    '= = = = = =  = = = = = = =

    '==   Target-New-Build-4287 --  
    '==   Target-New-Build-4287 --  

    WriteOnly Property colOtherSerialsThisInvoice() As Collection
        Set(ByVal value As Collection)
            mColOtherSerialsThisInvoice = value
        End Set
    End Property
    '= = = = = =  = = = = = = =
    '== END  Target-New-Build-4287 --  
    '== END  Target-New-Build-4287 --  


    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = = = = =
    '-===FF->


    '-- update result-
    '-- repack grid items into collection for caller-

    Private Function mbUpdateSerialList() As Boolean
        Dim sSerial As String

        '--prepare result list for caller-

        mColSerials = New Collection
        If (dgvSerials.Rows.Count > 0) Then
            For Each gridRow1 As DataGridViewRow In dgvSerials.Rows
                If (Trim(gridRow1.Cells(k_GRIDCOL_SERIAL).Value) <> "") Then  '--not empty-
                    sSerial = Trim(gridRow1.Cells(k_GRIDCOL_SERIAL).Value)
                    mColSerials.Add(sSerial)
                End If
            Next gridRow1
        End If  '--count-

    End Function  '--update-
    '= = = = =  = = = = = = ==

    '--load-
    '--load-

    Private Sub frmGoodsSerials_Load(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles MyBase.Load

        labIntro.Text = "Scan or Enter or fix all " & mIntQuantity & _
                             " serial number(s) being received for this Goods stock item/line.." & vbCrLf & _
                          "NB: Serial numbers must be unique for any given stock id/barcode."

        labMsg.Text = ""
        dgvSerials.Rows.Clear()

        labShowCurrent.Text = "Item Serials currently on File in System:"

    End Sub  '--load-
    '= = = = = = = = = = 

    '--Activated-
    '--Activated-

    Private Sub frmGoodsSerials_Activated(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub '-- do once only..--
        mbActivated = True

    End Sub '--Activated-
    '= = = = = = = = = =

    '-- S h o w n..-

    Private Sub frmGoodsSerials_Shown(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim gridRow1 As DataGridViewRow
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim intCount, iPos, ix As Integer
        '==Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim sSql, s1, sBarcode, sErrorMsg As String

        '== If mbActivated Then Exit Sub '-- do once only..--
        '== mbActivated = True
        If (mIntForm_top = -1) Or (mIntForm_left = -1) Then
            Call CenterForm(Me)
        Else
            '-- caller provided-
            Me.Top = mIntForm_top
            Me.Left = mIntForm_left
        End If

        Me.Update()
        Application.DoEvents()   '--let form show..--
        '-load stock info-
        labStockId.Text = CStr(mIntStock_id)
        labStockBarcode.Text = msStockBarcode
        labStockDescription.Text = msStockDescription
        labQty.Text = CStr(mIntQuantity)
        DoEvents()

        '-- check inputs--
        If (mIntStock_id <= 0) Or (msStockBarcode = "") Or (msStockDescription = "") Or (mIntQuantity <= 0) Then
            MsgBox("Serial No's form has invalid or insufficient input..", MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Close()
            Exit Sub
        End If
        '--  check/split input serials list -
        '== Dim strInputSerials() As String = {}
        '== strInputSerials = Split(msSerialList, vbCrLf)
        '== If (mIntQuantity < strInputSerials.Length) Then
        '== End If

        '-- check input collection..-
        If (Not (mColSerials Is Nothing)) AndAlso (mColSerials.Count > 0) Then
            If (mColSerials.Count > mIntQuantity) Then
                MsgBox("Serial No's input list more than 'qty' value..", MsgBoxStyle.Exclamation)
                mbCancelled = True
                Me.Close()
                Exit Sub
            End If  '--count-
        End If  '--nothing-

        '-- Add inputs as rows to Grid..
        '-- build/make no of Grid Rows = "quantity"--
        '-- ie Add a row to the grid for each serial..
        For intCount = 1 To mIntQuantity
            gridRow1 = New DataGridViewRow  '--prepare datagrid report row..
            dgvSerials.Rows.Add(gridRow1)
            '--  Number the rows..
            dgvSerials.Rows(intCount - 1).HeaderCell.Value = intCount.ToString  '== CStr(rx + 1)
        Next intCount
        '-- update grid with input list if any..-
        If (Not (mColSerials Is Nothing)) AndAlso (mColSerials.Count > 0) Then
            ix = 0
            For Each s1 In colSerials
                '-- load serial no into grid row.
                dgvSerials.Rows(ix).Cells(0).Value = s1
                ix += 1
            Next
        End If  '-nothing-
        '-- retrieve all serials currently on file for this stock item.. 
        sSql = "SELECT * FROM serialAudit WHERE (stock_id= " & CStr(mIntStock_id) & ") ;"
        '-- get the recordset (datatable)..
        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in getting recordset for serialAudit table.." & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
            Exit Sub
        Else
            If Not (dataTable1 Is Nothing) Then
                If (dataTable1.Rows.Count > 0) Then
                    '-- make a list of distinct serial nos. found...
                    intCount = 0
                    txtSerialsOnFile.Text = ""
                    For Each row1 In dataTable1.Rows
                        If Trim(txtSerialsOnFile.Text) <> "" Then
                            txtSerialsOnFile.Text &= vbCrLf
                        End If
                        txtSerialsOnFile.Text &= row1.Item("SerialNumber")
                        msSerialsOnFile &= LCase(row1.Item("SerialNumber")) & ";"
                    Next row1
                    labShowCurrent.Text = "Item Serials (" & dataTable1.Rows.Count & ") currently on File in the System:"
                Else  '=If (dataTable1.Rows.Count > 0) Then
                    '--  no serials on record.
                    labShowCurrent.Text = "NO Serials currently on File in System.."
                End If '--nothing-
            Else  '-nothing.
                labShowCurrent.Text = "ERROR- NO Serials Dataset was returned..."
            End If  '-nothing-
        End If  '--get table--
    End Sub  '--Activated-
    '= = = = = = = = = = == =
    '-===FF->

    '-- DataGridView Goods Serials-

    '-- Textbox control has been activated on a cell.-
    '--  set event handlers to deal with the textbox..
    '-- to catch keypress... AND set data Modified state..

    Private Sub dgvSerials_EditingControlShowing(ByVal sender As Object, _
                                                    ByVal e As DataGridViewEditingControlShowingEventArgs) _
                                              Handles dgvSerials.EditingControlShowing

        Dim text1 As TextBox = CType(e.Control, TextBox)
        If (text1 IsNot Nothing) Then
            '-- Remove an existing event-handler, if present, to avoid 
            '-- adding multiple handlers when the editing control is reused.
            RemoveHandler text1.KeyDown, _
                New KeyEventHandler(AddressOf dgvSerials_KeyDown)
        End If
        '-- Add the event handler. 
        AddHandler text1.KeyDown, _
             New KeyEventHandler(AddressOf dgvSerials_KeyDown)
    End Sub  '--EditingControlShowing-
    '= = = = = = =  = = = == = = = 

    '-dgvSerials_KeyDown-
    '-- Grid TEXTBOX- Catch editing activity --
    '--- AND set data Modified state--

    Private Sub dgvSerials_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) '= Handles txtPartNo.KeyDown

        mbModified = True

    End Sub '-dgvSerials_KeyDown-
    '= = = = = = = = = = = =
    '-===FF->

    '-- catch SERIAL No scanned entry--
    '--- C e l l  V a l i d a t i n g--=  
    '==
    '==   Target-New-Build-4287 --  
    '==      (28-Jan-2021)
    '==
    '==  Goods Received Serials entry...  (Stewart 27-Jan-2021-)
    '== For the next release-  make sure that serial no's are unique over the whole Invoice, 
    '==    And still also not on file already in the system. 
    '==


    Private Sub dgvSerials_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs) _
                                                  Handles dgvSerials.CellValidating
        Dim lRow, lCol, ix As Integer
        Dim s1, strBarcode, sSerialNo, sQty As String
        Dim sSql As String
        '== Dim dataTable1 As DataTable
        Dim gridRow1 As DataGridViewRow

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        Me.dgvSerials.Rows(ev.RowIndex).ErrorText = ""
        Me.dgvSerials.Rows(ev.RowIndex).Cells(lCol).ErrorText = ""
        If (lRow >= 0) And (dgvSerials.Rows.Count > 0) Then  '--selected a row.--
            If (lCol = k_GRIDCOL_SERIAL) Then  '--serial-
                sSerialNo = Trim(ev.FormattedValue.ToString)
                '-- look up barcode and get stock item info..
                If (sSerialNo <> "") Then  '--have serial-
                    '--lookup serial-
                    If (InStr(msSerialsOnFile, LCase(sSerialNo) & ";") > 0) Then
                        ev.Cancel = True
                        Me.dgvSerials.Rows(ev.RowIndex).ErrorText = "Serial No is already on File!" & sSerialNo
                        Me.dgvSerials.Rows(ev.RowIndex).Cells(lCol).ErrorText =
                                                            "Serial No is already on File! " & sSerialNo
                        MsgBox("Serial No '" & sSerialNo & "' is already on File in system!", MsgBoxStyle.Exclamation)
                        mbModified = True
                        Exit Sub
                    Else '-not on file-
                        '-- check for duplicates in grid..
                        For ix = 0 To (dgvSerials.Rows.Count - 1)
                            gridRow1 = dgvSerials.Rows(ix)
                            If (ix <> ev.RowIndex) And
                                 LCase((gridRow1.Cells(k_GRIDCOL_SERIAL).Value) = LCase(sSerialNo)) Then
                                ev.Cancel = True
                                MsgBox("Serial No '" & sSerialNo & "' is already in this lidtGrid!", MsgBoxStyle.Exclamation)
                                mbModified = True
                                Exit Sub
                            End If
                        Next ix

                        '==   Target-New-Build-4287 --  
                        '==   Target-New-Build-4287 --  
                        '--not duplicated in local grid..
                        '-- check for duplicates in Main Invoice grid..
                        If (mColOtherSerialsThisInvoice IsNot Nothing) Then
                            Dim colOther As Collection
                            For Each colInvoiceLineInfo As Collection In mColOtherSerialsThisInvoice
                                colOther = colInvoiceLineInfo.Item("SerialsList")
                                s1 = colInvoiceLineInfo.Item("LineNo")
                                For Each sOtherSerial As String In colOther
                                    If (LCase(sOtherSerial) = LCase(sSerialNo)) Then
                                        ev.Cancel = True
                                        MsgBox("Error- There is already a SerialNo '" & sSerialNo & "'" & vbCrLf &
                                                 " entered in Line-No " & s1 & " of this Invoice. ", MsgBoxStyle.Exclamation)
                                        mbModified = True
                                        Exit Sub
                                    End If
                                Next sOtherSerial
                            Next colInvoiceLineInfo
                        End If  '--nothing.
                        '== END Target-New-Build-4287 --  
                        '== END Target-New-Build-4287 --  


                    End If  '-not on file-
                End If  '--have serial-
                mbModified = True    '--assume modified..--
            End If  '--col-
        End If  '--row--
    End Sub  '--cell validating.--
    '= = = = = = = = = = == =
    '-===FF->


    '-- Save/Exit stuff..
    '-- Save/Exit stuff..

    '--OK-  SaveExit-- or Exit.-

    Private Sub cmdSaveExit_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles cmdSaveExit.Click
        If mbModified Then
            Call mbUpdateSerialList()
            Me.Hide()
        Else
            Me.Hide()
        End If

    End Sub  '--SaveExit--
    '= = = = = = = = = = = = 

    '--cancel--

    Private Sub cmdCancel_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles cmdCancel.Click

        If mbModified Then
            If (MsgBox("Abandon changes ", _
                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                mbCancelled = True
                Me.Hide()
            Else  '-stay-
                Exit Sub
            End If
        Else  '--not modified
            mbCancelled = True
            Me.Hide()
        End If  '--modified-
    End Sub  '--cancel--
    '= = = = = = = = = = = = == 

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmGoodsSerials_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                               System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                If mbModified Then
                    If MsgBox("Abandon changes ", _
                           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        mbCancelled = True
                        intCancel = 0 '--let it go---
                    Else  '-stay-
                        intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                        eventArgs.Cancel = intCancel
                        Exit Sub
                    End If
                Else  '--not modified
                    mbCancelled = True
                    intCancel = 0 '--let it go---
                    '== Me.Hide()
                End If  '--modified-
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--closing--
    '= = = = = = =  = = = = = = = == 

    Private Sub labMsg_Click(sender As Object, e As EventArgs) Handles labMsg.Click

    End Sub
End Class '-frmGoodsSerials-
'= = = = = = = = = = == =