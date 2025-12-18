Option Strict Off
Option Explicit On
Imports System.data.OleDb
Imports VB = Microsoft.VisualBasic
Imports System.Threading

Friend Class clsBrowse3

    '-== "clsBrowse" Created  =29-Oct-2009==
    '-== "clsBrowse" Created  =29-Oct-2009==
    '-== "clsBrowse" Created  =29-Oct-2009==

    '--- Class to emulate frmBrowse  --
    '---  so that main browser controls (eg FlexGrid) --
    '----  can stay in view on JobTracking Main form..


    '-- EX  Maint form---31-May-2005==
    '-----------------------------

    '--grh--=14-Oct-2007=1040== fix LoadGrid for empty r/set--
    '--grh--=19-Jun-2008== Add IsSqlServer flag to condition "CONVERT" for dates--
    '--grh--=23-Jun-2008== Bracket all field-names in SELECT statement-
    '---   Note: some fld names seem to be reserved words..  eg "position"..--
    '--grh--=30-Jun-2008== for JET: Convert dates in Grid to "yyyy-mm-dd"  ---

    '--grh--=07-Mar-2009== Dev from Original Maint. for JobTracking  ---
    '--grh--=14-Sep-2009== SORT- always reload from DB-table.
    '------     Use SQL-server to sort on 2-cols. ---
    '--grh--=28-Mar-2010==LoadGrid..  Colour rows of priority jobs..-.
    '--grh--=27Jul2010== "Refresh" method to refresh browse using current SELECT list and WHERE Cond.. etc.--
    '--grh--=11Aug2010==  LoadGrid.. Kill msgBox "No records.."--
    '--grh--=29Aug2010==  Find:  Fix Numeric argument for ASC/DESC..--
    '--grh--=03Sep2010==  Reload-  disable Grid after SQL error..-
    '--grh--=25-Oct-2010==  Terminate-  remove all grid commands....-

    '--grh--=30-Oct-2010==  NEW VERSION for JobMatix2 ---  remove priority colours..-
    '--grh--=15-Dec-2010==  Caller can provide select list and Initial Order..-

    '--grh--=24-Oct-2011== v3.0.3010= Replace GOSUBs for VB.NET compliance...--

    '--grh--=23-Nov-2011== v3.0.3010= UPGRADED to VB.NET version...--
    '--grh--=27-Feb-2012== v3.0.3031= Dropped MSHFlexGrid- 
    '---         now using VB.NET DataGridView...--
    '--grh--=15-Mar-2012== v3.0.3031= Show sort col-name in Find-lab..- 
    '----   AND..  fix "Find" on numeric column.-
    '== 
    '--grh--=08-Apr-2012== v3.0.3041.0= 
    '--- SET Selected-row in Find..- 
    '==

    '--grh--=11-Apr-2012== v3.0.3041.1= 
    '--- SET Selected-row in Find TO last row if argument out-of-bounds..- 
    '==
    '==3053= 16May2012=
    '--  Clear Grid when no data..
    '==
    '==3072/3= 18-Feb-2013=
    '--  mRst.Open Error or FetchComplete Error:
    '--    Raise runtime error in Activate and Refresh..
    '--    Mainline can try to re-connect.
    '==
    '== 3073.310= 10-Mar-2013=
    '==   >>  getRecordset.. Put Brackets [] around ORDR BY field-names. 
    '==
    '== 3083.314= 14-Mar-2014=
    '==   >>  Find:  Don't srch empty grid.. 
    '==   >> 3083.321  Disallow FIND for date columns..
    '==       And fix  left over yellow Column headers..
    '==   >> 3083.402  InitialOrderIsDescending -- FORCES DESC... on 1st column..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '==  grh. JobMatix 3.1 ---  13-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb 
    '==           (dropped sqlClient).. (For Jet OleDb driver).
    '==
    '==  =3101.928-  Check msCurrentOrder for Alias..--
    '==  =3101.929-  SORT-  Translate Sort Col. back to original.-
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '==  grh. JobMatix/POS 3.2 ---  06Feb2016===
    '==        Now clsBrowse32 --
    '==   >>  ADD 3rd User Initial Sort Field.. 
    '==   >>  And add some try-catches around reload..
    '==   >>   And user can ADD new Row..==
    '==           --NO too messy with BOUND grid.. Add to DB table and USE Refresh..
    '==
    '==  JmxPOS330.dll  VERSION- 3.3.3301.510  10May2016-
    '==     >> User can include FROM, JOIN...
    '==
    '==
    '==  JmxPOS330.dll   >> 3411.1209 
    '==      -- File clsBrowse32.vb renamed to clsBrowse34.vb..
    '==      -- actual class name renamed to "clsBrowse3" (for JobMatix JobTracking.)
    '==      -- New Property colColumnWidthWeights (collection) 
    '==             added to give user Fill Weight control with UserSElectList.
    '==
    '==   >> 3411.0304=  04-Mar-2018=
    '==      -- More Fixes Browsing Sort order..
    '==
    '== -- Updated 3501.1102  02-Nov-2018=  
    '==     -- Fixing Col. Sorting disfunctions Crash in FindSerials browsing....
    '==            THIS is actually a problem with clsBrowse34..  When user supplies the SQl SELECT script,
    '==               The ColTables collection passed in does not necessarily contain Table Columns info
    '==                for JOINED tables that may be included in the Users SELECT script.. 
    '==                  So references to these for Data tyoes will cause runtime errors..
    '==          SOLUTION is to not reference colTables when user supplies SQL script.
    '==          References to Column data types AFTER to fetch can refer to the 
    '==                  Column Type in the Datatable fetched.  
    '== -- Updated 3501.1105  06-Nov-2018=  
    '==     -- Now In JobTracking.Fixing Reload Crashes...
    '==                AND tidy up clsBrowse34 to work properly with JobTracking Aliased Pref.Cols.
    '==                 And avoid use of "mColTables" after recordset is fetched.
    '==     -- 3501.1107- Now In POS.Fixing Activated Crashes...
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    '= Const k_version As String = "clsBrowse34-V3.5.3501.1107=="

    '--Const ki_maxChildButtons = 6
    Const k_rstOpenFailure = 1023     '== for Raise error-

    Private mbStartupDone As Boolean

    Private mColTables As Collection
    Private msDBname As String
    Private mCnnSql As OleDbConnection  '== ADODB.Connection

    Private mbIsSqlServer As Boolean
    Private mColPreferredColumns As Collection '--show these first..--
    Private mColOriginalColumns As Collection '--translate back to native cols...--
    '=3501.1106=
    Private mColPreferredColumnsActual As Collection '--PrefCols after Aliasing
    '==           ie these will be the same names as in the CurrentDataTable 
    '--                and in the Grid Col.names..--

    Private mbShowPreferredColumnsOnly As Boolean
    Private msUserSelectList As String '-- caller provides: "SELECT  [list] "

    Private msInitialOrder1 As String '-- caller provides..  "
    Private msInitialOrder2 As String '-- caller provides..  "
    Private msInitialOrder3 As String '-- 3201.206=  caller provides..  "

    '==3311.1209=
    Private mColColumnWidthWeights As Collection

    '==3083==
    Private mbInitialOrderIsDescending As Boolean = False

    '== Private WithEvents mRst As ADODB.Recordset
    Private mSqlRdr1 As OleDbDataReader  '== SqlDataReader


    Private miTableIndexes() As Integer

    '--stuff for current browse table--
    '--stuff for current browse table--
    '--stuff for current browse table--

    Private msTableName As String
    Private miListIndex As Integer

    Private miCurrentTableIndex As Short


    Private mColTable As Collection '--current table--
    Private mColTableInfo As Collection
    Private mColFields As Collection '--fields for current table--
    Private mColPrimaryKeys As Collection
    Private mColOtherIndexes As Collection
    Private mColCurrentCols As Collection '--current RS/grid col names in order left->right--

    '====Private masOrders() As String       '--list of index names--
    Private msWhere As String '--WHERE condition for current browse--
    Private msFinalWhere As String

    Private miCurrentColCount As Short '--no of columns in current grid--
    Private msCurrentPrimaryKeyName As String '--column name current oreder--

    '--  current oreder--
    Private msCurrentOrder As String '--column name current MAJOR order--
    '== useless.
    '= Private msCurrentOrder2 As String '--column name current Minor order--
    '= Private msCurrentOrder3 As String '--column name current Minor-Minor order--

    Private miCurrentOrderColNo As Short '--column no current oreder--
    Private miCurrentOrderColNo2 As Short '--column no current Minor Order--
    Private miCurrentOrderColNo3 As Short '--column no current Minor-Minor Order--

    Private mbOrderAsc As Boolean '-- current order id True=ASCENDING, False=DESC..-
    Private mbOrder2Asc As Boolean '-- current order id-2 True=ASCENDING, False=DESC..-

    Private msCurrentSearch As String '--current find string--

    Private mColPrimaryKeyValues As Collection '--for current record--
    '--Private mColChildTables As Collection
    '= = = = = = = = = = = = = =
    Private mbClosingDown As Boolean
    Private mlFetchComplete As Integer
    Private msFetchError As String

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
    '===Private LabWhere As Label    '-- so is this.-
    Private LabFind As System.Windows.Forms.Label '--and this..-
    '== Private PicArrowUp As System.Windows.Forms.PictureBox
    '== Private PicArrowDown As System.Windows.Forms.PictureBox
    Private txtFind As System.Windows.Forms.TextBox

    Private mlPriorityColNo As Integer
    Private mlJobReturnedColNo As Integer
    '= = = = = = = = = = = = = = = = == =

    '--   for datagrid--
    Private mDataTableCurrent As DataTable
    Private mOleDbDataAdapter1 As OleDbDataAdapter
    Private mDataSet1 As DataSet
    '= = = = = = = = = = = = = = = = == =


    '--Properties as input parameters--
    '--Properties as input parameters--
    '--Properties as input parameters--

    WriteOnly Property connection() As OleDbConnection '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property
    '- - - - - - - - - -

    '--accept full table/cols description for browsed table--
    '--accept full table/cols description for browsed table--

    WriteOnly Property colTables() As Collection
        Set(ByVal Value As Collection)

            mColTables = Value

        End Set
    End Property
    '- - - - - -
    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msDBname = Value
        End Set
    End Property
    '- - - - - -
    '=== Property Let Title(sName As String)
    '--miCurrentTableIndex = Index
    '===      msTitle = sName
    '=== End Property
    '= = = = = = = =  =

    WriteOnly Property tableName() As String
        Set(ByVal Value As String)
            '--miCurrentTableIndex = Index
            msTableName = Value
        End Set
    End Property
    '-- - - -  - -
    WriteOnly Property WhereCondition() As String
        Set(ByVal Value As String)
            msWhere = Value
        End Set
    End Property
    '- - - - - -

    WriteOnly Property IsSqlServer() As Boolean
        Set(ByVal Value As Boolean)
            mbIsSqlServer = Value
        End Set
    End Property
    '- - - - - -
    '--accept list of preferred col-names.---
    WriteOnly Property PreferredColumns() As Collection
        Set(ByVal Value As Collection)

            mColPreferredColumns = Value
        End Set
    End Property '--prefs..-
    '= = = = =  =  = =  = =

    '--  USER can provide SELECT list..-
    WriteOnly Property UserSelectList() As String
        Set(ByVal Value As String)

            msUserSelectList = Value
        End Set
    End Property '--select..-
    '= = = = = = = =  = = = =
    WriteOnly Property InitialOrder1() As String
        Set(ByVal Value As String)

            msInitialOrder1 = Value
        End Set
    End Property '--select..-
    '= = = = = = = =  = = = =
    WriteOnly Property InitialOrder2() As String
        Set(ByVal Value As String)

            msInitialOrder2 = Value
        End Set
    End Property '--select..-
    '= = = = = = = =  = = = =
    WriteOnly Property InitialOrder3() As String
        Set(ByVal Value As String)

            msInitialOrder3 = Value
        End Set
    End Property '--select..-
    '= = = = = = = =  = = = =


    WriteOnly Property ShowPreferredColumnsOnly() As Boolean
        Set(ByVal Value As Boolean)

            mbShowPreferredColumnsOnly = Value
        End Set
    End Property '--preferred..=
    '= = = = = = = = = = = = == = 

    '==3083 InitialOrderIsDescending--
    WriteOnly Property InitialOrderIsDescending() As Boolean
        Set(ByVal value As Boolean)
            mbInitialOrderIsDescending = value
        End Set
    End Property  '--InitialOrderIsDescending-
    '= = = = = = = = = = = = = = = =  == =
    '= = = = = = = = = = = = =

    '--  pointer to FlexGrid..--
    '== WriteOnly Property FlexGrid() As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid
    '== 	Set(ByVal Value As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid)
    '= 		MSHFlexGrid1 = Value
    '== 	End Set
    '== End Property '--grid..-
    '= = = = = = = = = = = = = =

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

    '--  pointer to "Where" label..--
    '===Property Let showWhere(Label1 As Label)

    '===  Set LabWhere = Label1

    '===End Property  '--grid..-
    '= = = = = = = = = = = = = =

    '--  pointer to "Find" label..--
    WriteOnly Property showFind() As System.Windows.Forms.Label
        Set(ByVal Value As System.Windows.Forms.Label)

            LabFind = Value
        End Set
    End Property '--grid..-
    '= = = = = = = = = = = = = =

    '--  pointer to "Find" txt..--
    WriteOnly Property showTextFind() As System.Windows.Forms.TextBox
        Set(ByVal Value As System.Windows.Forms.TextBox)

            txtFind = Value
        End Set
    End Property '--grid..-
    '= = = = = = = = = = = = = =

    '==3311.1209=
    '--   Private mColColumnWidthWeights As Collection

    '--accept list of preferred col-width weights.---
    WriteOnly Property colColumnWidthWeights() As Collection
        Set(ByVal Value As Collection)

            mColColumnWidthWeights = Value
        End Set
    End Property '--widths...-
    '= = = = =  =  = =  = = = = = = = = = = == 

    '--  pointer to "arrows"..--
    '== WriteOnly Property ArrowUp() As System.Windows.Forms.PictureBox
    '==     Set(ByVal Value As System.Windows.Forms.PictureBox)

    '==         PicArrowUp = Value
    '==     End Set
    '== End Property '--grid..-
    '= = = = = = = = = = = = = =
    '--  pointer to "arrows"..--
    '== WriteOnly Property ArrowDown() As System.Windows.Forms.PictureBox
    '==     Set(ByVal Value As System.Windows.Forms.PictureBox)

    '==         PicArrowDown = Value
    '==     End Set
    '== End Property '--grid..-
    '= = = = = = = = = = = = = =

    '-- R e s u l t s ---
    '-- R e s u l t s ---
    ReadOnly Property FinalWhere() As String
        Get

            FinalWhere = msFinalWhere
        End Get
    End Property '--where..-
    '= = = = = = = = = = =

    '==  result is PKEYS  keyset of selected record--
    '=== Property Get selectedKey() As Collection

    '===   Set selectedKey = mColKeyValues

    '== End Property  '-- get key--
    '= = = = = = = =  = =  ==

    '== SECOND result is collection of flds (name/value) of selected grid row--
    '=== Property Get selectedRow() As Collection

    '===  Set selectedRow = mColRowValues

    '=== End Property  '-- get key--
    '= = = = = = = =  = =  ==
    '= = = = =
    '-===FF->

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '-===FF->

    '--  replacing GOSUB--
    '--  replacing GOSUB--
    '--  replacing GOSUB--

    '--GoSub getRec_addColumn--

    Private Sub mGetRec_addColumn(ByVal sFldName As String, _
                                     ByRef lCount As Integer, _
                                       ByRef sFldList As String, _
                                         ByRef sFields As String, _
                                           ByRef sLastFld As String)
        Dim iPos As Integer
        Dim colField As Collection
        Dim s1, sNativeFldName As String
        Dim sAlias As String = ""
        Dim sType As String
        Dim bIsLiteral As Boolean = False

        If Not (InStr(1, sFldList, "/" & UCase(sFldName) & "/") > 0) Then '--not there-
            If (sFields <> "") Then sFields = sFields & ", "
            lCount = lCount + 1
            '--  CHECK for ALIAS..-
            s1 = UCase(Trim(sFldName))
            iPos = InStr(s1, " AS ")
            If (iPos > 0) Then  '--have alias..-
                sNativeFldName = Trim(Left(Trim(sFldName), (iPos - 1)))
                sAlias = Trim(Mid(Trim(sFldName), (iPos + 4)))
                sFldList = sFldList & UCase(sNativeFldName) & "/" '--build list for dups check--
                bIsLiteral = (IsNumeric(sNativeFldName)) Or (Left(sNativeFldName, 1) = "'")
            Else  '--native only..-
                sNativeFldName = Trim(sFldName)
                sFldList = sFldList & UCase(sFldName) & "/" '--build list for dups check--
            End If

            If bIsLiteral Then
                sFields = sFields & sFldName  '-- apply as requested.-
            Else
                colField = mColFields.Item(UCase(sNativeFldName)) '--get this field stuff-
                sType = LCase(colField.Item("TYPE_NAME")) '--get sql type--
                '--dates must be reformatted YYYY-MM-DD for col. sorting in grid-
                '---NB !  --  Note: CONVERT not allowed for Jet Acces 97 db's.. ----
                If mbIsSqlServer And (sType = "datetime") Then
                    sFields = sFields & _
                             " CONVERT(varchar," & sNativeFldName & ",120) AS " & IIf((sAlias = ""), sNativeFldName, sAlias)
                    '--dateType-> format select-
                Else
                    sFields = sFields & "[" & sNativeFldName & "]" & _
                                                 IIf((sAlias = ""), "", " AS " & sAlias) '--not date- normal select-
                End If
                sLastFld = sFldName
            End If  '--literal--
        End If '--not there-
        '== Return
    End Sub '--addColumn--
    '= = = = = = = = = = = =
    '-===FF->

    '--- g e t R e c o r d s e t--
    '--- g e t R e c o r d s e t--

    '-----Private Function mbGetRecordset(Optional sOrder As String = "") As Boolean
    Private Function mbGetRecordset() As Boolean
        Dim lCount, j, i, k, ix As Integer
        Dim sFields, s1 As String
        Dim sFldName As String
        Dim colKey As Collection
        Dim v1 As Object
        Dim sFldList As String '--tests for duplicates--
        Dim sLastFld As String
        Dim sSql, sOrder As String
        Dim lBG As Integer '--save colour--
        Dim colField As Collection '--info for 1 field--
        Dim sType, sMsg, sErrorMsg As String
        Dim cmd1 As New OleDbCommand '=SqlCommand

        '--get key info from current table coll.--
        sFields = ""
        sFldList = "/"
        sLastFld = ""
        mbGetRecordset = False
        '--add primary key fields--
        lCount = 0 '--count flds selected--
        '--do preferred columns FIRST..--
        If (msUserSelectList <> "") Then '--caller provides select list..-
            sSql = msUserSelectList
        Else
            '--no user select list..  so build it..-
            If Not (mColPreferredColumns Is Nothing) Then '--do preferred cols first..-
                For Each v1 In mColPreferredColumns
                    sFldName = CStr(v1)
                    Call mGetRec_addColumn(sFldName, lCount, sFldList, sFields, sLastFld) '==GoSub getRec_addColumn
                Next v1 '--pref. s1--
            End If '--preferred..--
            If mColPrimaryKeys.Count() > 0 Then
                For i = 1 To mColPrimaryKeys.Count()
                    s1 = mColPrimaryKeys.Item(i)
                    '--add to select list if not already there--
                    If Not (InStr(1, sFldList, "/" & UCase(s1) & "/") > 0) Then '--not there-
                        If sFields <> "" Then sFields = sFields & ", "
                        sFields = sFields & "[" & s1 & "]" '--protect col names.--
                        sFldList = sFldList & UCase(s1) & "/" '--build list for dups check--
                        lCount = lCount + 1
                        sLastFld = s1
                    End If '--instr--
                Next i
            End If '-primary keys..-
            If Not mbShowPreferredColumnsOnly Then '--can show all more columns..--
                '--add name col if exists--
                s1 = mColTable.Item("NAMECOLUMN")
                If s1 <> "" Then
                    If Not (InStr(1, sFldList, "/" & UCase(s1) & "/") > 0) Then '--not there-
                        If sFields <> "" Then sFields = sFields & ", "
                        sFields = sFields & "[" & s1 & "]" '--protect col names.--
                        sFldList = sFldList & UCase(s1) & "/" '--build list for dups check--
                        lCount = lCount + 1
                        sLastFld = s1
                    End If '--instr--
                End If '-name-
                '--add other keys as extra browse cols--
                If mColOtherIndexes.Count() > 0 Then
                    For i = 1 To mColOtherIndexes.Count()
                        colKey = mColOtherIndexes.Item(i)
                        For Each v1 In colKey
                            sFldName = CStr(v1)
                            Call mGetRec_addColumn(sFldName, lCount, sFldList, sFields, sLastFld) '==GoSub getRec_addColumn
                        Next v1
                    Next i
                End If '--otherIndexes--
                '--select all fields if no cols <=5----
                k = 0 '--switch--
                '--IF not enough display cols-- select more--
                If (lCount < 7) Then '--add some more-
                    '-If sLastFld <> "" Then
                    For i = 1 To mColFields.Count()
                        sFldName = mColFields.Item(i)("NAME")
                        '-If k <> 0 Then '--started--
                        If lCount < 6 Then
                            Call mGetRec_addColumn(sFldName, lCount, sFldList, sFields, sLastFld) '==GoSub getRec_addColumn
                        End If '--lcount--
                    Next i
                    '-End If  '--lastfld--
                End If '--count-
            End If '--preferred.-
            sSql = "SELECT " & sFields
        End If '--user select.-
        '- user can JOIN- (if FROM already there, don't add it...)-
        If (InStr(UCase(sSql), " FROM ") <= 0) Then  '-no FROM, add.
            sSql = sSql & " FROM [" & msTableName & "] "
        End If

        If (msWhere <> "") Then msFinalWhere = "  Where " & msWhere
        If (msWhere <> "") Then sSql = sSql & " WHERE " & msWhere
        '-- Sort order--
        sOrder = ""
        If (msCurrentOrder <> "") Then
            sOrder = " ORDER BY [" & msCurrentOrder & IIf(mbOrderAsc, "] ASC", "] DESC")
            'If (msCurrentOrder2 <> "") Then
            '    sOrder = sOrder + ", [" + msCurrentOrder2 + IIf(mbOrder2Asc, "] ASC", "] DESC")
            '    If (msCurrentOrder3 <> "") Then
            '        sOrder = sOrder + ", [" + msCurrentOrder3 + IIf(mbOrder2Asc, "] ASC", "] DESC")
            '    End If  '-order-3-
            'End If '--order2
        End If '--Orders--
        sSql = sSql & sOrder
        '===LabWhere.Caption = LabWhere.Caption + vbCrLf + sOrder
        msFinalWhere = msFinalWhere & vbCrLf & sOrder
        mlFetchComplete = -1 '--not ready-
        '--lBG = Labwhere.BackColor       '--save--
        '--Labwhere.BackColor = vbYellow  '--progress--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        cmd1.Connection = mCnnSql
        cmd1.CommandText = sSql
        sErrorMsg = ""
        Try
            mSqlRdr1 = cmd1.ExecuteReader()
            '==   DisplayResults(reader)

        Catch ex As OleDbException  '=SqlException
            '= Console.WriteLine("Error ({0}): {1}", ex.Number, ex.Message)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sErrorMsg = " 'mbGetRecordset': SQL error in:" & vbCrLf & sSql & vbCrLf & vbCrLf & _
                           "ERROR: " & CStr(ex.ErrorCode) & "==" & ex.Message & vbCrLf
            '==MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        Catch ex As InvalidOperationException
            '== Console.WriteLine("Error: {0}", ex.Message)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sErrorMsg = " 'mbGetRecordset': InvalidOperationException in:" & vbCrLf & sSql & vbCrLf & vbCrLf & _
                           "ERROR: " & "==" & ex.Message & vbCrLf
        Catch ex As Exception
            ' You might want to pass these errors
            ' back out to the caller.
            '= Console.WriteLine("Error: {0}", ex.Message)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sErrorMsg = " 'mbGetRecordset': Exception Error in:" & vbCrLf & sSql & vbCrLf & vbCrLf & _
                           "ERROR: " & "==" & ex.Message & vbCrLf
        End Try

        If (sErrorMsg <> "") Then  '==mlFetchComplete <> 0 Then
            MsgBox("Failed to get recordset for table:  '" & msTableName & "'" & vbCrLf & _
                                             sErrorMsg, MsgBoxStyle.Exclamation)
            If (gsErrorLogPath <> "") Then
                If Not gbLogMsg(gsErrorLogPath, sErrorMsg) Then
                    MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
                End If
            End If '--log--
            mbGetRecordset = False '--blnSuccess = False
            '--Me.Hide
            '--Exit Function
        Else '--ok--
            '--MsgBox "ok.. got " & rs1.RecordCount & " records.."
            mbGetRecordset = True
            LabFind.Text = "Find: " & vbCrLf & "(" & LCase(msCurrentOrder) & ")" '--show key order--
            System.Windows.Forms.Application.DoEvents()
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '== On Error GoTo 0
        Exit Function

    End Function '-get record set--
    '= = = = =
    '-===FF->

    '--load dataGridView-- 
    '--load dataGridView--

    '= Private Function mbLoadGrid(ByRef rs As ADODB.Recordset) As Boolean
    Private Function mbLoadGrid(ByRef sqlRdr1 As OleDbDataReader) As Boolean

        Dim rx, L1, intColIndex, k, cx As Integer
        '= Dim colField As Collection
        '= Dim sType, sName As String
        Dim sName As String
        Dim s1 As String
        Dim lCharWidth As Integer '--total no chars to show--
        '= Dim lTwipWidth As Integer '--grid size in twips--
        Dim lTwipsPerChar As Integer '-- one char size in twips--(from point size)-
        Dim lMaxColSize As Integer
        Dim laWidths() As Integer
        Dim lngRecCount As Integer
        Dim d1 As Date
        '== Dim rsTemp As ADODB.Recordset
        '= Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim column1 As DataColumn

        '==If mbStartupDone Then  '--we've been here before--
        '==Else '--first time.-
        mbLoadGrid = False

        mDataTableCurrent = New DataTable
        '--  Get data "recordset" to internal table..
        Try
            mDataTableCurrent.Load(sqlRdr1)
        Catch ex As Exception
            MsgBox("Error in Loading Grid Table from sqlRdr ." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        Application.DoEvents()
        mDataTableCurrent.TableName = msTableName

        '== This dataset is no longer used.
        mDataSet1 = New DataSet
        mDataSet1.Tables.Add(mDataTableCurrent)
        Application.DoEvents()

        miCurrentColCount = mDataTableCurrent.Columns.Count   '==  rs.Fields.Count

        '--clear current col collection-
        mColCurrentCols = New Collection

        Erase laWidths
        '--Allocate col widrhs in proportion to their char width.--
        '--compute width of all flds by their max sizes--

        '=3311.1209=  --NB THESE widths are not used any more !!!!
        '=3311.1209=  --NB THESE widths are not used any more !!!!
        '=3311.1209=  --NB THESE widths are not used any more !!!!

        lCharWidth = 0
        intColIndex = 0
        For Each column1 In mDataTableCurrent.Columns '==  i = 0 To rs.Fields.Count - 1
            sName = column1.ColumnName   '==  rs.Fields(i).Name
            If (LCase(sName) = "priority") Then
                mlPriorityColNo = intColIndex '--save "priority" ColNo.-
            ElseIf (LCase(sName) = "jobreturned") Then
                mlJobReturnedColNo = intColIndex '--save "priority" ColNo.-
            End If
            k = k = column1.MaxLength  '==  rs.Fields(i).DefinedSize
            If column1.DataType Is GetType(Int32) Then  '= gbIsNumericType(gsGetSqlType((rs.Fields(i).Type), k)) Then
                k = 8 '--initial size for numerics.-
            Else
                k = (k \ 2) '--allocate 50 percent of max size--
            End If
            k = IIf(k > 20, 20, k) '-allow initial max 24 per fld--
            If (LCase(sName) = "surname") Or (LCase(sName) = "given_names") Then
                k = 14
            End If
            If k < Len(sName) Then
                k = Len(sName) + 1 '--col name must fit..--
            End If
            If k < 5 Then
                k = 5 '--min--
            End If
            k = k + 3 '--allow for sort arrow..-
            lCharWidth = lCharWidth + k
            ReDim Preserve laWidths(intColIndex)
            laWidths(intColIndex) = k '--save all col char widths--
            intColIndex += 1
        Next column1  '= i
        L1 = lTwipsPerChar '-- ALLOCATE width needed per char--
        If (L1 <= 0) Then L1 = 110 '--eg 9-twipsPerPoint-
        '-- Set column names in the grid
        intColIndex = 0
        For Each column1 In mDataTableCurrent.Columns '==  i = 0 To rs.Fields.Count - 1
            k = L1 * laWidths(intColIndex) '--get twips per char-Times chars--
            If (k > lMaxColSize) Then k = lMaxColSize
            sName = column1.ColumnName   '= rs.Fields(i).Name
            If (LCase(msTableName) = "jobs") Then '--set some col widts..-
                If (LCase(sName) = "job_id") Then
                    k = 900
                ElseIf (LCase(sName) = "priority") Then
                    k = 600
                End If
            End If
            '--set col width--
            mColCurrentCols.Add(sName)
            '--set current order col to Italic--
            If UCase(sName) = UCase(msCurrentOrder) Then
                miCurrentOrderColNo = intColIndex
            End If
            intColIndex += 1
        Next column1 '= i '--field-
        '==End If  '--FIRST time..-
        '== end setup==

        If (mDataTableCurrent.Rows.Count <= 0) Then  '=(rs.BOF And rs.EOF) Then '--empty-
            lngRecCount = 0
        Else '--move pointer to set up reccount
            lngRecCount = mDataTableCurrent.Rows.Count  '== rs.RecordCount
        End If '--empty--
        LabRecCount.Text = CStr(lngRecCount)  '=3101.1025=

        '-- Set range of cells in the grid
        '---  and load all data from recordset--
        '--MSHFlexgrid1.Row = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '=3501.1108=
        '= mDataGridView1.Rows.Clear()
        '= mDataGridView1.Columns.Clear()  '--CRASHES-

        mDataGridView1.ClearSelection()
        '==
        mDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        mDataGridView1.ColumnHeadersHeight = 29  '--increase header height.

        '==3311.1209= FIRST- SET Fill Mode- Apply width weights if any supplied..
        If mColColumnWidthWeights IsNot Nothing Then
            mDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If  '-nothing-

        mDataGridView1.EndEdit()   '--was suggested to stop index exception..

        '--  lOAD the grid..--
        '--  lOAD the grid..--
        '-- 3411.1225=  lOAD the grid (1)..--
        Try
            mDataGridView1.DataSource = mDataTableCurrent '=3501.1108=  mDataSet1
            Application.DoEvents()
        Catch ex As Exception
            MsgBox("Unexpected Exception in loading data grid (step-1): " & mDataGridView1.Name & ".." & vbCrLf & _
                   ex.Message & vbCrLf & vbCrLf & "-- Processing will continue..", MsgBoxStyle.Exclamation)
            '==Exit Function
        End Try
        '--  lOAD the grid (2)..--
        '-- THIS shouldn't be needed
        '--    as there's ONLY ONE table in the dataset..
        '=  https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.datamember%28v=vs.110%29.aspx
        '-- But we still seem to have to do it..
        '-- TEST msg-
        '== MsgBox("mDataset1 has " & mDataSet1.Tables.Count & " tables..", MsgBoxStyle.Information)
        '--Wait for dataset to load..
        'Dim intWaitCount As Integer = 30
        'While (mDataSet1.Tables.Count <= 0) And (intWaitCount > 0)
        '    Thread.Sleep(100)
        '    intWaitCount -= 1
        'End While

        '-test-
        '= MsgBox("Testing- clsBrowse34- DataGrid has " & mDataGridView1.Rows.Count & " rows..", MsgBoxStyle.Information)

        '--  lOAD the grid (3)..--
        '--  lOAD the grid (3)..--
        'Try
        '    mDataGridView1.DataMember = msTableName
        'Catch ex As Exception
        '    MsgBox("Unexpected Exception in loading data grid (step-2): " & mDataGridView1.Name & ".." & vbCrLf & _
        '           ex.Message & vbCrLf & vbCrLf & "-- Processing will continue..", MsgBoxStyle.Exclamation)
        '    '==Exit Function
        'End Try
        ''==3311.1209=-- NOW- SET- Apply width weights if any supplied..
        Try
            If (mColColumnWidthWeights IsNot Nothing) Then
                '==3311.1209= Apply width weights from supplied collection..
                Dim intWt As Integer
                For cx = 0 To (mDataGridView1.Columns.Count - 1)
                    If ((cx + 1) <= mColColumnWidthWeights.Count) Then  '-we have a weight.-
                        If IsNumeric(mColColumnWidthWeights.Item(cx + 1)) Then
                            intWt = CInt(mColColumnWidthWeights.Item(cx + 1))
                            mDataGridView1.Columns(cx).FillWeight = intWt
                        End If  '-numeric-
                    End If
                Next cx
            End If  '-nothing-
        Catch ex As Exception
            MsgBox("ERROR in setting data grid column widths..." & vbCrLf & _
                   ex.Message & vbCrLf & vbCrLf & "processing will continue..", MsgBoxStyle.Exclamation)
        End Try
 
        '--  make all columns  AUTOMATIC  sort.--
        For cx = 0 To mDataGridView1.Columns.Count - 1   '= (MSHFlexgrid1.get_Cols() - 1)
            mDataGridView1.Columns(cx).SortMode = DataGridViewColumnSortMode.Automatic
        Next cx

        System.Windows.Forms.Application.DoEvents()
        '-- set default cell BG colour..-
        If (lngRecCount > 0) Then
            '--- for Jet DB's, we must convert all DATE columns to yyyy-mm-dd  --
            '----  since we could not use CONVERT in the SELECT statement--
            If Not mbIsSqlServer Then '--assume Jet-
                For cx = 0 To (mDataTableCurrent.Columns.Count - 1)  '= rs.Fields.Count - 1
                    '==L1 = rs.Fields(cx).Type '--get ADO data type this column--
                    sName = mDataTableCurrent.Columns(cx).ColumnName  '=rs.Fields(cx).Name
                    '--MsgBox "TESTING: col:" + sName + " Type = " & L1, vbInformation
                    If mDataTableCurrent.Columns(cx).DataType Is GetType(DateAndTime) Then
                        '--convert all row-cells for this column--
                        Dim datarow1 As DataRow
                        For rx = 0 To (lngRecCount - 1)  '-tracking down the grid.--
                            datarow1 = mDataTableCurrent.Rows(rx)
                            d1 = DateValue(datarow1.Item(sName)) '--get rs original-
                            mDataGridView1.Rows(rx).Cells(cx).Value = Format(d1, "yyyy-MM-dd")
                        Next rx
                    End If  '--datetime-
                Next cx '--column-
            End If '--jet--
            '-- ====== FIX Customer names.--
 
            '--  MUST sort the first time..--
            Try
                If mbOrderAsc Then  '==3083.402..  check order..
                    mDataGridView1.Sort(mDataGridView1.Columns(miCurrentOrderColNo), _
                                                                  System.ComponentModel.ListSortDirection.Ascending)
                Else
                    mDataGridView1.Sort(mDataGridView1.Columns(miCurrentOrderColNo), _
                                                                  System.ComponentModel.ListSortDirection.Descending)
                End If
            Catch ex As Exception
                MsgBox("Load-Grid- Error sorting Grid first time." & vbCrLf & ex.Message & vbCrLf & _
                       vbCrLf & "Processing will contine..", MsgBoxStyle.Information)
            End Try
            '-- Disallow FIND for date columns..-
            '=3501.1102= -- USE DataTable..  NOT colFields..
            '= colField = mColFields.Item(UCase(msCurrentOrder)) '--get this field stuff-
            '= sType = LCase(colField.Item("TYPE_NAME")) '--get sql type--
            Dim bIsDateType As Boolean = False
            If mDataTableCurrent.Columns.Contains(msCurrentOrder) Then '= mColFields.Contains(UCase(msCurrentOrder)) Then
                If mDataTableCurrent.Columns(msCurrentOrder).DataType Is GetType(DateTime) Then
                    bIsDateType = True
                End If  '-type-
                If bIsDateType Then  '= gbIsDate(sType) Then
                    LabFind.Text = ""
                    txtFind.Enabled = False
                    mDataGridView1.Select()
                Else  '--text/number.
                    LabFind.Text = "Find in: " & "'" + msCurrentOrder + "'"  '--show key order--
                    txtFind.Enabled = True '--allow change event-
                    txtFind.Focus()
                End If
            End If  '-contains.
        Else '--empty..-
            LabFind.Text = ""
            txtFind.Enabled = False
            If gbDebug Then MsgBox("No records for this selection..", MsgBoxStyle.Information)
        End If '--not empty-
        mbLoadGrid = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '==txtFind.SetFocus

        System.Windows.Forms.Application.DoEvents()
    End Function '--load grid--
    '= = = = =  =
    '-===FF->

    '--Get and load new r/set--
    '--Get and load new r/set--

    Private Function mbReload(Optional ByVal bWriteGrid As Boolean = True) As Boolean

        Dim lBG As Integer
        '=Dim sName As String
        Dim bOk, bOk2 As Boolean

        mbReload = False
        Try
            If Not (mSqlRdr1 Is Nothing) Then
                mSqlRdr1.Close()
                mSqlRdr1 = Nothing
            End If
            lBG = System.Drawing.ColorTranslator.ToOle(LabRecCount.BackColor) '--save--
            '--Labwhere.BackColor = vbYellow  '--progress--
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '- get recordset=
            Try
                bOk = mbGetRecordset()
            Catch ex As Exception
                MsgBox("Error in clsBrowse Reload function." & vbCrLf & _
                       "FAILED to get Recordset for table " & msTableName & ".." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
            If bOk Then '=mbGetRecordset() Then
                LabRecCount.BackColor = System.Drawing.Color.Magenta '--loading grid--
                System.Windows.Forms.Application.DoEvents()
                bOk2 = False
                If bWriteGrid Then
                    Try
                        bOk2 = mbLoadGrid(mSqlRdr1)
                    Catch ex As Exception
                        MsgBox("Reload- Runtime Error in loading Data Grid. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    End Try
                    If Not bOk2 Then
                        MsgBox("Error in loading Data Grid ", MsgBoxStyle.Exclamation)
                    Else '-ok-
                        mbReload = True
                    End If  '-load grid-
                Else
                    mbReload = True
                End If
                '== txtFind.Enabled = True '--allow change event-
                '--txtFind.SetFocus
            Else '--sql error-
                MsgBox("Failed to get Recordset for data grid.", MsgBoxStyle.Exclamation)
            End If '--get.-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            LabRecCount.BackColor = System.Drawing.ColorTranslator.FromOle(lBG) '-- vbGreen  '--progress--

        Catch ex As Exception
            MsgBox("Error in clsBrowse Reload function." & vbCrLf & _
                   "For table " & msTableName & ".." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function '--reload--
    '= = = = = = = = = = = =
    '-===FF->

    '-- initialize --
    '-- initialize --

    'UPGRADE_NOTE: class_initialize was upgraded to class_initialize_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()

        '-- stuff from frmBrowse_Load()  ..--
        msDBname = ""
        '====== txtFind.Enabled = False
        '====== txtFind.Text = ""
        mbStartupDone = False
        mbClosingDown = False
        '-- frame1.Caption = ""
        '--Frame2.Caption = ""
        msCurrentSearch = ""
        msCurrentOrder = "" '-Major..-
        '= msCurrentOrder2 = "" '--Minor--

        msTableName = ""
        '=== Me.Caption = k_version
        '--cboOrder.Enabled = False  '--no order
        msWhere = ""
        '== Set mColKeyValues = New Collection
        '== Set mColRowValues = New Collection
        '====  cmdOk.Enabled = False
        '== msTitle = ""

        '==mlGridLeft = MSHFlexGrid1.Left
        '==mlGridTop = MSHFlexGrid1.Top     '--save grid pos..--
        mlRecCount = 0
        mbOrderAsc = True '--default is ASC..-
        mbOrder2Asc = True

        mlPriorityColNo = -1 '--none.-
        mlJobReturnedColNo = -1 '--none.-
        msFinalWhere = ""
        mbShowPreferredColumnsOnly = False
        msUserSelectList = ""
        msInitialOrder1 = "" '-- caller provides..  "
        msInitialOrder2 = "" '-- caller provides..  "
        msInitialOrder3 = "" '-- caller provides..  "

    End Sub  '--init-
    '= = = = = = = = = = 

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub '--init..--
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Public Methods..--
    '-- Public Methods..--
    '-- Public Methods..--

    '-- A c t i v a t e  - Launches first R/set and grid-load after setting up SELECT list..-
    '-- A c t i v a t e  - Launches first R/set and grid-load after setting up SELECT list..-
    '-- A c t i v a t e  - Launches first R/set and grid-load..-

    Public Sub Activate()
        Dim idx, intPos As Integer
        Dim sPrefEntry As String
        Dim s1, s2 As String

        '==If mbStartupDone Then Exit Sub
        '====== txtFind.Text = ""
        Try
            mbStartupDone = True
            s1 = "=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "Build:" & _
                                           My.Application.Info.Version.Build & ", Rev: " & My.Application.Info.Version.Revision & "="
            If (msDBname = "") And mbIsSqlServer Then
                MsgBox("DB name empty in frmMaint startup..", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If mColTables Is Nothing Then
                MsgBox("DB Table info not setup for clsMaint startup..", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If mColTables.Count() <= 0 Then
                MsgBox("Tables collection empty in frmMaint startup..", MsgBoxStyle.Critical)
                '--Me.Hide
                Exit Sub
            End If
            '-  Build column name translate table.  for SORT column issues..
            '--  Analyse and deconstruct Prefs/aliases..
            mColOriginalColumns = New Collection
            mColPreferredColumnsActual = New Collection
            If Not (mColPreferredColumns Is Nothing) Then
                For Each sPrefEntry In mColPreferredColumns
                    intPos = InStr(LCase(sPrefEntry), " as ")
                    If (intPos > 0) Then
                        s1 = Trim(Left(sPrefEntry, intPos - 1))  '-orig native col.-
                        s2 = Trim(Mid(sPrefEntry, intPos + 4))  '-alias-
                        mColOriginalColumns.Add(s1, s2)  '- key is alias, -returns value native-
                        mColPreferredColumnsActual.Add(s2, s2)  '--save alias as actual grid col.name
                    Else  '-not aliased.
                        mColOriginalColumns.Add(Trim(sPrefEntry), Trim(sPrefEntry))  '--returns self-
                        mColPreferredColumnsActual.Add(Trim(sPrefEntry), Trim(sPrefEntry))
                    End If
                Next  '-colPrefEntry-
            End If  '-nothing-

            '--set up stuff for current table--
            '-Set mColTable = mColTables(idx)
            mColTable = mColTables.Item(msTableName)
            '--Set mColTableInfo = mColTable(1)
            mColFields = mColTable.Item("FIELDS")
            mColPrimaryKeys = mColTable.Item("PRIMARYKEYS")
            mColOtherIndexes = mColTable.Item("OTHERINDEXES")

            '--finish table setup/load rset etc==
            msCurrentOrder = ""
            If mColPrimaryKeys.Count() > 0 Then
                s1 = mColPrimaryKeys.Item(1)
            Else '--use col-0 --
                s1 = mColFields.Item(1)("NAME")
            End If
            msCurrentPrimaryKeyName = s1
            If (Not (mColPreferredColumns Is Nothing)) AndAlso (mColPreferredColumns.Count > 0) Then
                If (msInitialOrder1 <> "") Then '--was provided..-
                    If mColPreferredColumnsActual.Contains(msInitialOrder1) Then
                        msCurrentOrder = msInitialOrder1
                    Else  '--wrong by caller.  should have used alias.
                        msCurrentOrder = mColPreferredColumnsActual.Item(1)
                    End If
                Else  '-no initial order provided.
                    msCurrentOrder = mColPreferredColumnsActual.Item(1)
                End If '--order1- 
            Else  '-no prefs.
                If (msInitialOrder1 <> "") Then '--was provided..-
                    msCurrentOrder = msInitialOrder1
                End If  '-provided.
            End If  '-oref nothing-
            '-  There can be No initial order. (ok)

            '== 3083.402..  user can force DESC..
            mbOrderAsc = True  '- Activate- initial default.
            If mbInitialOrderIsDescending Then mbOrderAsc = False '--make DESC..

            '-3101.928- Check for Alias in Sort columns..
            '-- no- see above..
            'intPos = InStr(LCase(msCurrentOrder), " as ")
            'If intPos > 0 Then
            '    msCurrentOrder = Trim(Left(msCurrentOrder, intPos - 1))  '--For sql  sort. OrigCol.Name-
            'End If
            'intPos = InStr(LCase(msCurrentOrder2), " as ")
            'If (intPos > 0) Then
            '    msCurrentOrder2 = Trim(Left(msCurrentOrder2, intPos - 1))  '--For sql  sort. OrigCol.Name-
            'End If

            '- Go.
            If mbReload(True) Then '--get new r/set, load grid--
                mbStartupDone = True '--1st time is done..-
                '== txtFind.Focus()
            Else '--no recs-
                If Not (mSqlRdr1 Is Nothing) Then
                    mSqlRdr1.Close()
                    mSqlRdr1 = Nothing
                End If
                MsgBox("SQL Recordset Open ERROR:" & vbCrLf & _
                      "No records due to SQL error.." & vbCrLf & vbCrLf & _
                      "(Possible Server Connection failure.)", MsgBoxStyle.Exclamation, "Browse Activate")
                Err.Raise(vbObjectError + 512 + k_rstOpenFailure, "clsBrowse32-Activate", _
                                  "Recordset Open Failure.")
                Exit Sub
            End If  '-reload-

            '=3411.0304=
            '=3501.1108= 
            mDataGridView1.ClearSelection()

            If Not (mSqlRdr1 Is Nothing) Then
                mSqlRdr1.Close()
                mSqlRdr1 = Nothing
            End If
        Catch ex As Exception
            MsgBox("POS Browser- Runtime Error in clBrowse3. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Err.Raise(vbObjectError + 512 + k_rstOpenFailure, "clsBrowse32-Activate", _
                              "Activate Failure.")
        End Try
    End Sub '--activate--
    '= = = = = = = = = = = = = =
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
        '=3501.1103-  DROP all this sruff about saving position, as
        '==    A different recordset woll have come back.
        'If (mDataGridView1.Rows.Count > 0) Then '==3053.0 ==
        '    intRow = mDataGridView1.FirstDisplayedCell.RowIndex
        '    intCol = mDataGridView1.FirstDisplayedCell.ColumnIndex
        '    '--  save selection..--
        '    If (mDataGridView1.SelectedRows.Count > 0) Then
        '        '--  use 1st selected row only.
        '        intSelRow = mDataGridView1.SelectedRows(0).Cells(0).RowIndex
        '    End If
        'End If  '--rows..-
        'On Error Resume Next
        If Not mbReload(True) Then
            MsgBox("SQL Recordset Open ERROR:" & vbCrLf & _
                  "No records due to SQL error.." & vbCrLf & vbCrLf & _
                  "(Possible Server Connection failure.)", MsgBoxStyle.Exclamation, "Browse Refresh")
            '==== Me.Hide
            If Not (mSqlRdr1 Is Nothing) Then
                mSqlRdr1.Close()
                mSqlRdr1 = Nothing
            End If
            Err.Raise(vbObjectError + 512 + k_rstOpenFailure, "clsBrowse-Refresh", _
                               "Recordset Open Failure.")
            Exit Function
        End If  '--reload-
        On Error GoTo refresh_error
        '-  restore..-
        '=3501.1103-  DROP all this sruff about saving position, as
        '==    A different recordset woll have come back.
        'If (mDataGridView1.Rows.Count > 0) Then '==3053.0 ==
        '    mDataGridView1.FirstDisplayedCell = mDataGridView1.Rows(intRow).Cells(intCol)  '--keep col-0 on show..-
        '    If intSelRow > 0 Then  '--have slection.-
        '        mDataGridView1.Rows(intSelRow).Selected = True
        '    End If
        'End If  '--rows..-
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        refresh = True
        Exit Function

refresh_error:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        sMsg = " Runtime error in clsBrowse-refresh:" & vbCrLf & vbCrLf & _
                   "ERROR: " & Str(Err.Number) & "==" & Err.Description & vbCrLf
        MsgBox(sMsg, MsgBoxStyle.Exclamation)
        If (gsErrorLogPath <> "") Then
            If Not gbLogMsg(gsErrorLogPath, sMsg) Then MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
        End If '--log--
    End Function '--refresh..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '== 3201.209 - ADD new Row..==
    '-- Is Bound, so add to the dataTable underneath..
    '-  Collection must conform to DataTable columns..

    '== Public Function AddNewRow(colNewRowData As Collection) As Boolean
    '== Dim col1 As Collection
    '== Dim sColName, sColData As String
    '==         AddNewRow = False
    '==     If Not (colNewRowData Is Nothing) Then
    '==         Try
    '== Dim dtGrid As DataTable = mDataSet1.Tables(msTableName)
    '== Dim datarowNew As DataRow = dtGrid.NewRow
    '==             For Each col1 In colNewRowData
    '==                 sColName = col1("name")
    '==                 sColData = col1("value")
    '==                 datarowNew(sColName) = sColData
    '==             Next col1
    '==             dtGrid.Rows.Add(datarowNew)
    '==             dtGrid.AcceptChanges()
    '==             AddNewRow = True
    '==         Catch ex As Exception
    '==             MsgBox("Error adding new row to data Grid." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '==         End Try
    '==     End If  '-- nothing-
    '==     End Function '-AddNewRow-
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--- Change Sort Column or Order..--
    '--- Change Sort Column or Order..--

    '--  A u t o sort  --  THIS NOW CALLED AFTER the SORT  --
    '--               ie from the DataGridView.sorted event. 
    '--  Just to record, and to change header colour..--
    '--  sName is header text of NEWly-clicked column--

    Public Function SortColumn(ByVal sNameClicked As String) As Boolean
        Dim ix, intNewSortCol As Integer
        Dim colHdr1 As DataGridViewColumnHeaderCell
        '= Dim colField As Collection
        '= Dim sType, sOriginalColName As String
        '= Dim sOriginalColName As String

        SortColumn = True
        intNewSortCol = 0

        '==3083=  Make all col hdrs gray..
        For ix = 0 To (mDataGridView1.Columns.Count - 1)
            colHdr1 = mDataGridView1.Columns(ix).HeaderCell
            colHdr1.Style.BackColor = Color.LightGray     '--  SHOULD be from SAVED original colour..--
        Next

        '--  find col-no..  sName is col.hdr.-
        For ix = 0 To mDataGridView1.Columns.Count - 1   '=  (MSHFlexGrid1.get_Cols() - 1)
            If LCase(sNameClicked) = LCase(mDataGridView1.Columns(ix).HeaderCell.Value) Then
                '=LCase(Trim(MSHFlexGrid1.get_TextMatrix(0, ix))) Then
                intNewSortCol = ix
                Exit For
            End If
        Next ix
        '- get hdr of prev. active col.
        colHdr1 = mDataGridView1.Columns(miCurrentOrderColNo).HeaderCell
        '--translate-
        'If mColOriginalColumns.Contains(sNameClicked) Then
        '    sOriginalColName = mColOriginalColumns.Item(sNameClicked)
        'Else
        '    sOriginalColName = sNameClicked
        'End If
        ''= sOriginalColName=mretailhost1. ?? 

        '--demote old order to Minor--
        '- msCurrentOrder is prev. active order.
        If LCase(sNameClicked) <> LCase(msCurrentOrder) Then
            '= LCase(sOriginalColName) <> LCase(msCurrentOrder) Then '--new column..-
            '= msCurrentOrder2 = msCurrentOrder
            mbOrder2Asc = mbOrderAsc
            colHdr1.Style.BackColor = Color.LightGray     '--  SHOULD be from SAVED original colour..--
        End If

        '-- Set range of SORT ROWS as full grid--
        '--different col- or same col but was DESC..  then make ASC..--
        If (UCase(sNameClicked) <> UCase(msCurrentOrder)) Then '= (UCase(sOriginalColName) <> UCase(msCurrentOrder)) Then '--new col.-
            '--set new order--
            msCurrentOrder = sNameClicked '= sOriginalColName '--masOrders(lCol)
            miCurrentOrderColNo = intNewSortCol
            '--MSHFlexgrid1.Sort = flexSortGenericAscending '--SORT--
            mbOrderAsc = True
        Else '--same Major col.- FLIP..-
            If (Not mbOrderAsc) Then
                mbOrderAsc = True
            Else '--was ASC.-
                '--MSHFlexgrid1.Sort = flexSortGenericDescending '--SORT--
                mbOrderAsc = False '-make DESC..-
            End If
        End If '-new order.-
        '--Get new mRst recordset to implement new sort cols--

        '==   NO MORE RELOAD..  GRID does sorting..--
        '==    Call mbReload(True)

        '--  set new column hdr colour..-
        colHdr1 = mDataGridView1.Columns(miCurrentOrderColNo).HeaderCell
        colHdr1.Style.BackColor = Color.Yellow

        System.Windows.Forms.Application.DoEvents()

        txtFind.Text = "" '--clear for new sort column-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '-- Clear the selection, because the selected row index won't 
        '--     be pointing to the same actual record..
        '=3411.0304=
        mDataGridView1.ClearSelection()

        '=3311.510=  Disallow FIND for date columns..- 
        '--  (ONLY look at BASE table-) eg Serials grid has a JOIN.-
        '=3501.1102= -- USE DataTable..  NOT colFields..
        Dim bIsDateType As Boolean = False
        If mDataTableCurrent.Columns.Contains(msCurrentOrder) Then '= mColFields.Contains(UCase(msCurrentOrder)) Then
            '= colField = mColFields.Item(UCase(msCurrentOrder)) '--get this field stuff-
            '= sType = LCase(colField.Item("TYPE_NAME")) '--get sql type--
            If mDataTableCurrent.Columns(msCurrentOrder).DataType Is GetType(DateTime) Then
                bIsDateType = True
            End If  '-type-
            If bIsDateType Then  '= gbIsDate(sType) Then
                LabFind.Text = ""
                txtFind.Enabled = False
                mDataGridView1.Select()
            Else  '--text/number.
                LabFind.Text = "Find in: " & "'" + msCurrentOrder + "'"  '--show key order--
                txtFind.Enabled = True '--allow change event-
                txtFind.Focus()
            End If
        End If
    End Function '--sort..-
    '= = = = = = = = =
    '-===FF->

    '---select record--
    '---select record--

    Public Sub SelectRecord(ByVal lngRow As Integer, _
                              ByRef ColKeyValues As Collection, _
                               ByRef colRowValues As Collection)

        '==Dim sName As String
        Dim cx As Integer
        Dim colFld As Collection
        Dim v1 As Object
        Dim sName As String

        '--setup key-values col. of current row for retrieval of correct full record--
        '=== Set mColKeyValues = New Collection
        ColKeyValues = New Collection
        colRowValues = New Collection
        If Not mbStartupDone Then Exit Sub '--too early..-
        '== If Trim(MSHFlexGrid1.get_TextMatrix(0, cx)) = "" Then
        If mDataGridView1.RowCount <= 0 Then
            '--10Nov2011--
            MsgBox("Data Grid is empty..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        For Each v1 In mColPrimaryKeys
            '--find column name--
            For cx = 0 To mDataGridView1.Columns.Count - 1   '===   (MSHFlexGrid1.get_Cols() - 1)
                If LCase(CStr(v1)) = LCase(mDataGridView1.Columns(cx).HeaderCell.Value) Then
                    '== LCase(MSHFlexGrid1.get_TextMatrix(0, cx)) Then '--found this key col.-
                    '==ColKeyValues.Add(MSHFlexGrid1.get_TextMatrix(lngRow, cx)) '--save key value for row..-
                    ColKeyValues.Add(mDataGridView1.Rows(lngRow).Cells(cx).Value) '--save key value for row..-
                    Exit For
                End If '--found-
            Next cx
        Next v1
        '-- send back complete row  from grid..--
        '=== Set mColRowValues = New Collection
        For cx = 0 To mDataGridView1.Columns.Count - 1   '===  (MSHFlexGrid1.get_Cols() - 1)
            colFld = New Collection

            '== colFld.Add(LCase(MSHFlexGrid1.get_TextMatrix(0, cx)), "name") '--col hdr..-
            sName = mDataGridView1.Columns(cx).HeaderCell.Value
            colFld.Add(LCase(sName), "name") '--col hdr..-

            '== colFld.Add(MSHFlexGrid1.get_TextMatrix(lngRow, cx), "value") '--col hdr..-
            colFld.Add(mDataGridView1.Rows(lngRow).Cells(cx).Value, "value") '--data..-
            '= colRowValues.Add(colFld, LCase(MSHFlexGrid1.get_TextMatrix(0, cx)))
            colRowValues.Add(colFld, LCase(sName))
        Next cx

    End Sub '--select--
    '= = = = = = = = = =
    '-===FF->

    '--change in find argument--
    '--  FIND row corresponding to find argument..-
    '-- --Position grid to row--
    '-- NB:  DataGridView version.. R/set closed..
    '---- we now do MANUAL Find.--

    Public Sub Find(ByRef txtFind As System.Windows.Forms.TextBox)

        Dim s1, sGrid, sMsg As String
        '= Dim colField As Collection
        '= Dim sType, sArg As String
        Dim sArg As String
        Dim sSample As String
        Dim iArgLen, selRow As Integer
        Dim intResult As Integer
        Dim intOrigCol, rx, rowPos, colPos As Integer
        Dim double1, double2 As Double

        On Error GoTo Find_Error
        If (mDataGridView1.Rows.Count <= 0) Then
            MsgBox("No rows to search..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '-  save position..-
        intOrigCol = mDataGridView1.FirstDisplayedCell.ColumnIndex

        s1 = UCase(txtFind.Text)
        If txtFind.Enabled And ((msCurrentSearch <> "") And (msCurrentSearch <> s1)) Or _
                                                                  ((msCurrentSearch = "") And (s1 <> "")) Then
            '--do it now if rs active--
            '= colField = mColFields.Item(UCase(msCurrentOrder)) '--get this field stuff-
            '= sType = LCase(colField.Item("TYPE_NAME")) '--get sql type--
            Dim bIsNumericType As Boolean = False
            If mDataTableCurrent.Columns(msCurrentOrder).DataType Is GetType(Integer) Or _
                  mDataTableCurrent.Columns(msCurrentOrder).DataType Is GetType(Decimal) Or _
                     mDataTableCurrent.Columns(msCurrentOrder).DataType Is GetType(Long) Then
                bIsNumericType = True
            End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '--  SEARCH FlexGrid..  NOT recordset,  since we may have changed the data.--
            '--  miCurrentOrderColNo is current column..-
            '--- mbOrderAsc  (true or false) shows ASC/DESC currently...--

            msCurrentSearch = s1
            iArgLen = Len(msCurrentSearch)
            selRow = -1

            '--  AUTO SORTING..  RecordSet is now UNSORTED..-
            '--  DO manual FIND here..--

            '--  AUTO SORTING..  RecordSet is now UNSORTED..-
            '--  DO manual FIND here..--

            colPos = miCurrentOrderColNo
            rowPos = -1  '-- in case out of bounds--
            '-- M a n u a l Search --
            '-- M a n u a l Search --
            '== If (mDataGridView1.Rows.Count > 1) Then
            For rx = 0 To (mDataGridView1.Rows.Count - 1)
                If bIsNumericType Then '= gbIsNumericType(sType) Then
                    '--  compare complete args..--
                    sSample = mDataGridView1.Rows(rx).Cells(colPos).Value.ToString()
                    If (IsNumeric(sSample) And IsNumeric(msCurrentSearch)) Then
                        double1 = CDbl(sSample)
                        double2 = CDbl(msCurrentSearch)
                        If (mDataGridView1.SortOrder = SortOrder.Ascending) Then
                            If (double1 >= double2) Then '-- cell is GT or equal to Arg. found, going up.
                                rowPos = rx
                                Exit For
                            End If
                        Else  '-desc..-
                            If (double1 <= double2) Then  '--Cell LE to ARG.. found, going DOWN..
                                rowPos = rx
                                Exit For
                            End If
                        End If  '-asc-
                    Else '--can't compare..-
                        rowPos = rx
                        Exit For
                    End If
                Else  '--alpha--
                    sSample = VB.Left(mDataGridView1.Rows(rx).Cells(colPos).Value.ToString(), iArgLen)
                    intResult = System.String.Compare(sSample, msCurrentSearch, True)
                    If (mDataGridView1.SortOrder = SortOrder.Ascending) Then
                        If (intResult >= 0) Then '-- cell is GT or equal to Arg. found, going up.
                            rowPos = rx
                            Exit For
                        End If
                    Else  '--desc--
                        If (intResult <= 0) Then  '--Cell LE to ARG.. found, going DOWN..
                            rowPos = rx
                            Exit For
                        End If
                    End If
                End If  '--numeric..-
            Next  '--rx..-
            '== Else  '--1 row.-
            '==     rowPos = 0
            '== End If  '--row count..-

            '-- In case out of bounds--
            If (rowPos < 0) Then
                If (mDataGridView1.SortOrder = SortOrder.Ascending) Then
                    rowPos = mDataGridView1.Rows.Count - 1
                Else  '-desc..
                    rowPos = 0    '--set on 1sr row..
                End If
            End If

            '--  set new visible pos..-
            '--   keep orig 1st col on show..-
            mDataGridView1.FirstDisplayedCell = mDataGridView1.Rows(rowPos).Cells(intOrigCol)

            '== MSHFlexGrid1.Row = mRst.AbsolutePosition
            '== MSHFlexGrid1.RowSel = MSHFlexGrid1.Row
            '== MSHFlexGrid1.Col = 0
            '== MSHFlexGrid1.ColSel = MSHFlexGrid1.get_Cols() - 1
            mDataGridView1.CurrentCell = mDataGridView1.Rows(rowPos).Cells(miCurrentOrderColNo)

            '-- enable OK if match..-
            '= sGrid = Left(MSHFlexGrid1.get_TextMatrix(mRst.AbsolutePosition, miCurrentOrderColNo), iArgLen)
            sGrid = VB.Left(mDataGridView1.Rows(rowPos).Cells(colPos).Value, iArgLen)
            '============  If LCase(Left(sGrid, iArgLen)) = LCase(msCurrentSearch) Then cmdOK.Enabled = True

            '-- This for frmBrowse only..--
            '--  << If (intResult = 0) Then cmdOK.Enabled = True  >>

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If '-enabled..-
        '--position grid to row--
        '== End If '--nothing..-
        Exit Sub
Find_Error:
        sMsg = "Runtime error in DatagridView Find method:" & vbCrLf & _
               vbCrLf & vbCrLf & "ERROR: " & Str(Err.Number) & "==" & Err.Description & vbCrLf & "..."
        MsgBox(sMsg, MsgBoxStyle.Exclamation)
        If (gsErrorLogPath() <> "") Then
            If Not gbLogMsg(gsErrorLogPath, sMsg) Then MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
        End If '--log--

    End Sub '--change--
    '= = = = = = = = = = = =
    '-===FF->

    '-- Terminate..-
    '-- Terminate..-

    'UPGRADE_NOTE: class_terminate was upgraded to class_terminate_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()

        '==  MsgBox "Browse class Terminate..", vbInformation
        '-- end of browse..-
        '====  rev-2777 25Oct2010==  MSHFlexGrid1.Clear
        '===MSHFlexGrid1.Row = 0
        '===MSHFlexGrid1.RowSel = 0
        '===MSHFlexGrid1.Col = miCurrentOrderColNo
        '===MSHFlexGrid1.CellFontItalic = False
        '===MSHFlexGrid1.CellBackColor = &H8000000F   '--vbGrey
        '===Set MSHFlexGrid1.CellPicture = Nothing

    End Sub
    '= = = = = = = = = = 

    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub '--init..--
    '= = = = = = = = = = = = =

    '== end class ===
End Class