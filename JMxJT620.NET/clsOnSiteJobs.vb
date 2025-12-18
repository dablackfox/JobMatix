Option Explicit On
Imports System.Data
Imports System.data.OleDb
Imports VB = Microsoft.VisualBasic

Public Class clsOnSiteJobs

    '--= JobMatix V3.0.3083.308.===
    '-- Class to show OnSite Jobs in DataGridView..

    '==
    '==  grh =V3.0.3083.308= Started 08-Mar-2014=
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  grh. JobMatix 3.1 ---  16-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==
    '==  grh.  3431.0705 --  05-July-2018=
    '==       --  On-site Jobs Panel.. Order Jobs Date DESCENDING....
    '==                  (Time in Day still ASCENDING..)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '--  Checklist FlexGrid columns..-
    '--  col-0 is fixed row no..-
    Private Const k_GRIDCOL_DATETIME As Short = 0
    Private Const k_GRIDCOL_CUSTOMER As Short = 1
    Private Const k_GRIDCOL_TECH As Short = 2
    Private Const k_GRIDCOL_JOBNO As Short = 3
    Private Const k_GRIDCOL_STATUS As Short = 4

    Private Const K_GOODS_ONSITEJOB = "ON-SITE JOB;"

    '= = = = = = = = = = = = = = = = = = = = = = = 

    Private mbStartupDone As Boolean

    Private msDBname As String
    Private mCnnSql As OleDbConnection   '== ADODB.Connection
    Private msTableName As String

    Private msInitialOrder1 As String '-- caller provides..  "
    Private msInitialOrder2 As String '-- caller provides..  "

    '== Private WithEvents mRst As ADODB.Recordset
    Private WithEvents mRst As DataTable

    Private msWhere As String '--WHERE condition for current browse--

    Private miCurrentColCount As Short '--no of columns in current grid--
    Private msCurrentPrimaryKeyName As String '--column name current oreder--

    '--  current order--
    Private msCurrentOrder As String '--column name current MAJOR order--
   
    Private msCurrentSearch As String '--current find string--

    Private mColPrimaryKeyValues As Collection '--for current record--
    '--Private mColChildTables As Collection
    '= = = = = = = = = = = = = =
    Private mbClosingDown As Boolean
    '== Private mlFetchComplete As Integer
    '== Private msFetchError As String
    Private mlRecCount As Integer '--ocunt of recs in rset..--

    '=== Private mColKeyValues As Collection  '--PKEYS of selected record-
    '=== Private mColRowValues As Collection  '--selected grid row-
    '=== Private msTitle As String
    '= = = = =

    '--Private mlGridLeft, mlGridTop As Long  '--save grid pos..--
    Private mColPrimaryKeyGridCols As New Collection '--saved by getRecordset..-
    '= = = = = = = = = = = = = = = =  = =

    '-- flexGrid is located in caller's form..-
    '=    Private MSHFlexGrid1 As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid

    '-- DataGridView is located in caller's form..-
    Private mDataGridView1 As DataGridView

    Private LabRecCount As System.Windows.Forms.Label '-- so is this.-

    Private msLastErrorMsg As String = ""
    '= = = = = = = = = = = = = = = = == =


    '--Properties as input parameters--
    '--Properties as input parameters--
    '--Properties as input parameters--

    WriteOnly Property connection() As OleDbConnection   '==ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property
    '- - - - - - - - - -
    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msDBname = Value
        End Set
    End Property
    '- - - - - -

    '--  pointer to DataGridView..--
    WriteOnly Property DataGrid() As DataGridView
        Set(ByVal Value As DataGridView)

            mDataGridView1 = Value
        End Set
    End Property '--grid..-
    '= = = = = = = = = = = = = =

    '--  pointer to reccount label..--
    WriteOnly Property showRecCount() As System.Windows.Forms.Label
        Set(ByVal Value As System.Windows.Forms.Label)

            LabRecCount = Value
        End Set
    End Property '--grid..-
    '= = = = = = = = = = = = = =

    '--  result --
    ReadOnly Property LastErrorMsg() As String
        Get
            LastErrorMsg = msLastErrorMsg
        End Get
    End Property
    '= = = = =
    '-===FF->

    '--- g e t R e c o r d s e t--
    '--- g e t R e c o r d s e t--

    Private Function mbGetRecordset() As Boolean
        Dim lCount, j, i, k, ix As Integer
        Dim sMsg As String
        Dim colKey As Collection
        Dim v1 As Object
        Dim sSql, sOrder As String
        Dim lBG As Integer '--save colour--

        '--get key info from current table coll.--
        mbGetRecordset = False
        '--add primary key fields--
        lCount = 0 '--count flds selected--

        '-- get on-site jobs not delivered or cancelled..
        '==       --  On-site Jobs Panel.. Order Jobs Date DESCENDING....
        '==             (Time in Day still ASCENDING..)
        sSql = "SELECT *,   "
        '-- Extracted the dd/mm/yy and re-converts to datetime at midnight..
        sSql &= " cast( CONVERT(VARCHAR, DatePromised, 106) + ' 00:00:00 ' AS datetime) AS DayPromised, "
        '-- Extracted the HH:mm and re-converts to datetime with zero day..
        sSql &= "  CONVERT(VARCHAR, DatePromised, 108) AS TimePromised  "
        'sSql &= " CAST( '01/01/1900 ' + STR(DATEPART(hour,DatePromised),2) + ':' + " & _
        '                                  "  STR(DATEPART(minute,DatePromised),2) AS datetime) AS TimePromised  "
        sSql &= " FROM [Jobs] "
        sSql &= " WHERE ((UPPER(GoodsInCare)='" & K_GOODS_ONSITEJOB & "') AND (LEFT(JobStatus,2)<'70')) "
        '-- Sort order--
        '= sSql &= " ORDER BY [DatePromised] DESC; "
        sSql &= " ORDER BY [DayPromised] DESC, [TimePromised] ASC; "

        '--On Error Resume Next
        '== On Error GoTo mbGetRecordset_error
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '== mRst.Open(sSql, mCnnSql, ADODB.CursorTypeEnum.adOpenStatic, _
        '==                                        ADODB.LockTypeEnum.adLockReadOnly, ADODB.ExecuteOptionEnum.adAsyncFetch)
        '== On Error GoTo 0
        If Not gbGetDataTable(mCnnSql, mRst, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            sMsg = " 'ON_SITE Class: mbGetDataTable' Error- SQL:" & vbCrLf & sSql & vbCrLf & vbCrLf & _
                          "ERROR: " & gsGetLastSqlErrorMessage() & vbCrLf & _
                                     "===== end error msg =====" & vbCrLf
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, sMsg)
            End If '--log--
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastErrorMsg = sMsg
            mbGetRecordset = False '--blnSuccess = False
            mRst = Nothing '--GetResultset = Nothing
        Else  '--ok-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            mbGetRecordset = True
        End If

    End Function '-get record set--
    '= = = = =
    '-===FF->

    '--Get and load new r/set--
    '--Get and load new r/set--

    Private Function mbReload() As Boolean

        Dim iPos, ix, cx, rx, intLines As Integer
        Dim intDays As Integer
        Dim lngRecCount As Integer = 0
        Dim sDateKey, sDate, sTime, sCustomer, sTech, sJobNo, sJobStatus As String
        Dim s2, s1, s3 As String
        '==Dim intRowCount, rx As Integer
        Dim row1 As DataGridViewRow
        '=3431.0705=  Dim sDateLastKey As String = "00000000"  '--YYYYMMdd --
        Dim sDateLastKey As String = "99990000"  '--YYYYMMdd --  NOW DESCENDING order..
        Dim colJobs, col1 As Collection
        Dim styleHdr As DataGridViewCellStyle
        Dim datePromised As DateTime

        mbReload = False
        msLastErrorMsg = ""
        Try
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If mbGetRecordset() Then
                '==If mlRecCount > 0 Then
                LabRecCount.BackColor = System.Drawing.Color.Magenta '--loading grid--
                System.Windows.Forms.Application.DoEvents()
                '-- load grid.-
                If (mRst.Rows.Count <= 0) Then  '== (mRst.BOF And mRst.EOF) Then '--empty-
                    lngRecCount = 0
                Else '--move pointer to set up reccount
                    '== mRst.MoveLast() '--RuntimeError if r/set empty !!--
                    '= mRst.MoveFirst()
                    lngRecCount = mRst.Rows.Count '= mRst.RecordCount
                    '--rsVar = rs.GetString(adClipString)  '--rs.RecordCount)--
                End If '--empty--
                '== MsgBox("Found:   " & lngRecCount & " records.", MsgBoxStyle.Information)
                '--  make all columns  NOT sortable.--
                For cx = 0 To (mDataGridView1.Columns.Count - 1)
                    mDataGridView1.Columns(cx).SortMode = DataGridViewColumnSortMode.NotSortable
                Next cx
                '-- build rows..-
                With mDataGridView1.RowTemplate
                    .DefaultCellStyle.BackColor = Color.Bisque
                    .Height = 21
                    .MinimumHeight = 20
                End With
                rx = 0
                '--  Build job collection with discreet YYYYDDD date keys..
                '-- so we can compare and clearly separate jobs into day groups..
                colJobs = New Collection
                If (lngRecCount > 0) Then
                    For Each dataRow1 As DataRow In mRst.Rows
                        col1 = New Collection
                        intLines = 0
                        datePromised = CDate(dataRow1.Item("DatePromised"))
                        '=--  build a row collection ..--
                        col1.Add(datePromised, "datepromised")
                        sDateKey = VB6.Format(CDate(dataRow1.Item("DatePromised")), "yyyy")
                        sDateKey &= VB6.Format(CDate(dataRow1.Item("DatePromised")), "MMdd")
                        col1.Add(sDateKey, "datekey")

                        sCustomer = CStr(dataRow1.Item("CustomerName"))
                        If (sCustomer <> "") Then
                            intLines += 1
                            sCustomer &= vbCrLf
                        End If
                        If (CStr(dataRow1.Item("CustomerCompany")) <> "") And _
                                            (CStr(dataRow1.Item("CustomerCompany")) <> "--") Then
                            sCustomer &= CStr(dataRow1.Item("CustomerCompany"))
                            intLines += 1
                        End If
                        col1.Add(sCustomer, "customer")
                        col1.Add(intLines, "custlines")

                        sDate = VB6.Format(CDate(dataRow1.Item("DatePromised")), "dddd") & vbCrLf & _
                                            VB6.Format(CDate(dataRow1.Item("DatePromised")), "    dd-MMM-yyyy") & vbCrLf
                        col1.Add(sDate, "date")

                        sTime = VB6.Format(CDate(dataRow1.Item("DatePromised")), "hh:mm")
                        col1.Add(sTime, "time")

                        sTech = dataRow1.Item("NominatedTech")
                        col1.Add(sTech, "tech")
                        sJobNo = CStr(dataRow1.Item("job_id"))
                        col1.Add(sJobNo, "jobno")
                        sJobStatus = dataRow1.Item("jobStatus")
                        col1.Add(sJobStatus, "jobstatus")
                        '-- add to jobs collection.-
                        colJobs.Add(col1)
                        rx += 1
                    Next dataRow1
                    '== mRst.MoveFirst()
                    '== While Not mRst.EOF
                    '==   mRst.MoveNext()
                    '== rx += 1
                    '== End While
                End If  '--lngRecCount--
                '-- ok. build grid rows..
                '--  add a "header" row for each separate day..
                '--  clear grid..
                mDataGridView1.Rows.Clear()
                '--  create lngRecCount new rows.
                '== intRowCount = lngRecCount
                rx = 0
                For Each col1 In colJobs
                    row1 = New DataGridViewRow
                    mDataGridView1.Rows.Add(row1)
                    '-- cells are created with the row..
                    '=--  just update the cell values..--
                    sDateKey = col1.Item("datekey")
                    sDate = col1.Item("date")
                    datePromised = CDate(col1.Item("datepromised"))
                    '=3431.0705= Now is DESCENDING.-
                    If (sDateKey < sDateLastKey) Then  '--new day-- '=  (sDateKey > sDateLastKey) Then  '--new day--
                        '--  use this row as header, and add another for the data-
                        '==row1.Height = 47  '--Day hdr is deeper..--
                        styleHdr = New DataGridViewCellStyle
                        styleHdr.BackColor = Color.LavenderBlush
                        styleHdr.Font = New Font(mDataGridView1.Font, FontStyle.Bold)
                        styleHdr.ForeColor = Color.DarkGray  '--overdue--
                        intDays = gIntDateDiffDays(DateTime.Today, datePromised)
                        If (intDays >= 0) Then '--not overdue-
                            styleHdr.ForeColor = Color.Black
                        Else
                            '== MsgBox("overdue..", MsgBoxStyle.Information)
                        End If
                        With mDataGridView1
                            .Rows(rx).Cells(k_GRIDCOL_DATETIME).Value = sDate
                            '-- colour this header row..--
                            .Rows(rx).Cells(k_GRIDCOL_DATETIME).Style = styleHdr
                            '=.Font = New Font(mDataGridView1.Font, FontStyle.Bold)
                            .Rows(rx).Cells(k_GRIDCOL_CUSTOMER).Value = ""
                            .Rows(rx).Cells(k_GRIDCOL_CUSTOMER).Style = styleHdr
                            .Rows(rx).Cells(k_GRIDCOL_TECH).Value = ""
                            .Rows(rx).Cells(k_GRIDCOL_TECH).Style = styleHdr
                            .Rows(rx).Cells(k_GRIDCOL_JOBNO).Value = ""
                            .Rows(rx).Cells(k_GRIDCOL_JOBNO).Style = styleHdr
                            .Rows(rx).Cells(k_GRIDCOL_STATUS).Value = ""  '== CStr(intDays)  '--TEMP= DEBUG..-
                            .Rows(rx).Cells(k_GRIDCOL_STATUS).Style = styleHdr
                            '--Day hdr is deeper..--
                            .Rows(rx).Height = 35  '-- after cell data inserted, row is unshared.

                            sDateLastKey = sDateKey   '-- save current key.-
                            row1 = New DataGridViewRow
                            mDataGridView1.Rows.Add(row1)
                            rx += 1  '--header row=
                        End With
                    End If
                    '--  now do job row..  row has been added..
                    sTime = col1.Item("time")
                    sCustomer = col1.Item("customer")
                    intLines = CInt(col1.Item("custlines"))
                    sTech = col1.Item("tech")
                    sJobNo = col1.Item("jobno")
                    sJobStatus = col1.Item("jobstatus")
                    With mDataGridView1
                        .Rows(rx).Cells(k_GRIDCOL_DATETIME).Value = sTime
                        .Rows(rx).Cells(k_GRIDCOL_CUSTOMER).Value = sCustomer
                        .Rows(rx).Cells(k_GRIDCOL_TECH).Value = sTech
                        .Rows(rx).Cells(k_GRIDCOL_JOBNO).Value = sJobNo
                        .Rows(rx).Cells(k_GRIDCOL_STATUS).Value = sJobStatus
                        .Rows(rx).Height = 27  '-- after cell data inserted, row is unshared.
                        If intLines > 1 Then
                            .Rows(rx).Height = 37  '-- after cell data inserted, row is unshared.
                        End If
                        .Rows(rx).Height = 27  '-- after cell data inserted, row is unshared.
                    End With
                    rx += 1  '--data row added.=
                Next  '--col1--
                mbReload = True
                '==End If  '--count-
                '-- lighter colour for date column..-
                '==mDataGridView1.Columns(k_GRIDCOL_DATETIME).DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
                '== mDataGridView1.Columns(k_GRIDCOL_JOBNO).DefaultCellStyle.BackColor = Color.Gray

                '--If mRst.State = adStateOpen Then mRst.Close
            Else '--sql error-
                '== MSHFlexGrid1.Enabled = False
            End If '--get.-
        Catch ex As Exception
            MsgBox("Error in ON-SITE mbReload function." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '--Labwhere.BackColor = vbYellow  '--progress--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '==LabRecCount.BackColor = System.Drawing.ColorTranslator.FromOle(lBG) '-- vbGreen  '--progress--
        LabRecCount.BackColor = System.Drawing.Color.Transparent
        col1 = Nothing
        colJobs = Nothing
        '--- Set mRst = Nothing
    End Function '--reload--
    '= = = = = = = = = = = =
    '-===FF->

    '--  p u b l i c  --
    '--  p u b l i c  --
    '--  p u b l i c  --

    Public Sub New()
        MyBase.New()
        '==  Class_Initialize_Renamed()
        '-- stuff from frmBrowse_Load()  ..--
        msDBname = ""
        msLastErrorMsg = ""
        mbStartupDone = False
        mbClosingDown = False
        '-- frame1.Caption = ""
        '--Frame2.Caption = ""
        msCurrentSearch = ""
        msCurrentOrder = "" '-Major..-
     
        msTableName = ""
        '=== Me.Caption = k_version
        '--cboOrder.Enabled = False  '--no order
        msWhere = ""
 
        mlRecCount = 0

    End Sub '--init..--
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Public Methods..--
    '-- Public Methods..--
    '-- Public Methods..--

    '-- A c t i v a t e  - Launches first R/set and grid-load after setting up SELECT list..-
    '-- A c t i v a t e  - Launches first R/set and grid-load after setting up SELECT list..-
    '-- A c t i v a t e  - Launches first R/set and grid-load..-

    Public Function Activate() As Boolean

        Dim s1 As String

        '==If mbStartupDone Then Exit Sub
        '====== txtFind.Text = ""
        mbStartupDone = True
        With My.Application.Info
            s1 = "=V" & .Version.Major & "." & .Version.Minor & "Build:" & _
                        .Version.Build & ", Rev: " & .Version.Revision & "="
        End With

        On Error Resume Next
        If Not (mRst Is Nothing) Then
            '== mRst.Close()
            mRst = Nothing
        End If
        On Error GoTo 0

        '--Get rset and Build Grid rows...
        If Not mbReload() Then
            Activate = False
        Else
            Activate = True
        End If

    End Function  '--activate--
    '= = = = =  = = = == = =  =
    '-===FF->

    '--- Refresh browse using current SELECT list and WHERE Cond.. etc.--
    '--- Refresh browse using current SELECT list and WHERE Cond.. etc.--

    Public Function refresh() As Boolean
        Dim intRow, intSelRow, intCol As Integer
        Dim sMsg As String

        refresh = False
        On Error GoTo refresh_error
        If Not mbStartupDone Then Exit Function '--Must Activate at least once..-
        '--Get new mRst recordset to implement current SELECT and sort cols--

        intSelRow = -1
        '-  save position..-
        If (mDataGridView1.Rows.Count > 0) Then '==3053.0 ==
            intRow = mDataGridView1.FirstDisplayedCell.RowIndex
            intCol = mDataGridView1.FirstDisplayedCell.ColumnIndex
            '--  save selection..--
            If (mDataGridView1.SelectedRows.Count > 0) Then
                '--  use 1st selected row only.
                intSelRow = mDataGridView1.SelectedRows(0).Cells(0).RowIndex
            End If
        End If  '--rows..-
        If mbReload() Then
            On Error GoTo refresh_error
            '-  restore..-
            If (mDataGridView1.Rows.Count > 0) Then '==3053.0 ==
                mDataGridView1.FirstDisplayedCell = mDataGridView1.Rows(intRow).Cells(intCol)  '--keep col-0 on show..-
                If (intSelRow >= 0) Then  '--have selection.-
                    mDataGridView1.Rows(intSelRow).Selected = True
                End If
            End If  '--rows..-
            System.Windows.Forms.Application.DoEvents()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            refresh = True
        End If
        Exit Function

refresh_error:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        sMsg = " Runtime error in ON-SITE Browse-refresh:" & vbCrLf & vbCrLf & _
                   "ERROR: " & Str(Err.Number) & "==" & Err.Description & vbCrLf
        MsgBox(sMsg, MsgBoxStyle.Exclamation)
        If (gsErrorLogPath <> "") Then
            If Not gbLogMsg(gsErrorLogPath, sMsg) Then MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
        End If '--log--
    End Function  '--refresh--
    '= = = = =  = = = == = =  =
    '-===FF->


    '---select record--
    '---select record--

    Public Sub SelectRecord(ByVal lngRow As Integer, _
                              ByRef ColKeyValues As Collection, _
                               ByRef colRowValues As Collection)
        Dim cx As Integer
        Dim colFld As Collection
        Dim v1 As Object
        Dim sName As String

        '--setup key-values col. of current row for retrieval of correct full record--
        '=== Set mColKeyValues = New Collection
        ColKeyValues = New Collection
        colRowValues = New Collection
        If Not mbStartupDone Then Exit Sub '--too early..-
        If mDataGridView1.RowCount <= 0 Then
            '--10Nov2011--
            MsgBox("OnSite Data Grid is empty..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '-- ON-SITE:  Job_id is primary key--
        cx = 3
        ColKeyValues.Add(mDataGridView1.Rows(lngRow).Cells(cx).Value) '--save key value for row..-

        '== For Each v1 In mColPrimaryKeys
        '== '--find column name--
        '== For cx = 0 To mDataGridView1.Columns.Count - 1   '===   (MSHFlexGrid1.get_Cols() - 1)
        '== If LCase(CStr(v1)) = LCase(mDataGridView1.Columns(cx).HeaderCell.Value) Then
        '== ColKeyValues.Add(mDataGridView1.Rows(lngRow).Cells(cx).Value) '--save key value for row..-
        '== Exit For
        '== End If '--found-
        '== Next cx
        '== Next v1

        '-- send back complete row  from grid..--
        '=== Set mColRowValues = New Collection
        For cx = 0 To (mDataGridView1.Columns.Count - 1)   '===  (MSHFlexGrid1.get_Cols() - 1)
            colFld = New Collection
            sName = mDataGridView1.Columns(cx).Name
            colFld.Add(LCase(sName), "name") '--col hdr..-
            colFld.Add(mDataGridView1.Rows(lngRow).Cells(cx).Value, "value") '--data..-
            colRowValues.Add(colFld, LCase(sName))
        Next cx

    End Sub '--select--
    '= = = = = = = = = =
    '-===FF->


    Protected Overrides Sub Finalize()
        '== Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub '--init..--
    '= = = = = = = = = = = = =


End Class
