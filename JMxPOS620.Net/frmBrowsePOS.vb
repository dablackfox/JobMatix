Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Data
Imports System.Windows.Forms.Application
Imports System.Reflection
'== Imports system.data.sqlclient
Imports System.Data.OleDb

Friend Class frmBrowsePOS
    Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
    '= = = = = = = =

    '--BROWSE form-  JobTracking v1.4 ===-

    '--grh--=14-Oct-2007=1040== fix LoadGrid for empty r/set--
    '--grh--=19-Jun-2008== Add IsSqlServer flag to condition "CONVERT" for dates--
    '--grh--=23-Jun-2008== Bracket all field-names in SELECT statement-
    '---   Note: some fld names seem to be reserved words..  eg "position"..--
    '--grh--=30-Jun-2008== for JET: Convert dates in Grid to "yyyy-mm-dd"  ---

    '--grh--=07-Mar-2009== Dev from Original Maint. for JobTracking  ---
    '--grh--=14-Sep-2009== SORT- always reload from DB-table.
    '------     Use SQL-server to sort on 2-cols. ---
    '--grh--=20-Jan-2010== v1.4== "OK" can't be Default ENTER because Scanner sends CR on txtFind--
    '-----         This triggers completion as soon as scanner is finished sending...
    '-----         Need to capture/discard CR arriving in txtFind before settling time..
    '------          and respond to CR in Grid..
    '--grh--=11-May-2010== v1.4== "Fix Apostrophe crash in FIND text..--

    '--  J O B M A T I X ---
    '--grh--=11-Nov-2010== v2.1= Allow caller to position form...--
    '--grh--=30-Mar-2011== v2.1.2804= Allow caller to decide Focus Column....--

    '--grh--=24-Oct-2011== v3.0.3010= REplace GOSUBs for VB.NET compliance...--
    '--grh--=23-Nov-2011== v3.0.3010= UPGRADE.  VB.NET version...--

    '--grh--=30-Nov-2011== v3.0.3021=  VB.NET version...--
    '---   Fixed searching on numeric column..

    '--grh--=19-Dec-2011== v3.0.3023=  Allow Field Aliasing on user colprefs...--

    '--grh--=15-Feb-2012== v3.0.3031=  Replace MSHFlexGrid with dotNet "DataGridView" control...--
    '--grh--=15-Mar-2012== v3.0.3031=  Fix "Find" for numeric column...--
    '== 
    '--grh--=08-Apr-2012== v3.0.3041.0= 
    '--- SET Selected-row in Find..- 
    '==
    '--grh--=11-Apr-2012== v3.0.3041.1= 
    '--- SET Selected-row in Find TO last row if argument out-of-bounds..- 
    '==   also..  save/restore 1st visible column..
    '= 
    '== grh--=23-Apr-2012== v3.0.3047.0= 
    '--  Fix colours and focus..
    '==
    '== 3073.310= 10-Mar-2013=
    '==   >>  getRecordset.. Put Brackets [] around ORDR BY field-names. 
    '==
    '==  JobMatix POS---  22-Mar-2014=
    '==   >>  Using ADO.net 
    '==
    '=== = = = = = = = = = = = = = = = = 
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==
    '==  grh. JobMatix 3.1.3101.1207 ---  07-Dec-2014 ===
    '==   >>  Staff_id ans StaffName as input... 
    '==          and pass them on to Edit Form..
    '==
    '==
    '==  grh. JobMatix 3.1.3101.1221 ---  21-Dec-2014 ===
    '==   >>  FrmParent is input for positioning... 
    '==
    '==  grh. JobMatix 3.1.3103.0216 ---  16-Feb-2015 ===
    '==   >> "mbLookupSelection"- 
    '==            dblClick can return selection or Edit selected item if admin... 
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >> Now for .Net 4.5.2- 
    '==
    '==  grh. JobMatixPOS 3.3.3301.711 ---  11-July-2016= ===
    '==   >> Now for .Net 3.5- 
    '==   >>  Tidy up recCount.
    '==
    '==  grh. JobMatixPOS 3.3.3301.816 ---  16-Aug-2016= ===
    '==   >> Input version POS.- 
    '==
    '==
    '==  grh. JobMatixPOS 3.3.3307.816 ---  02-Feb-2017= ===
    '==   >> Can Input User Select List...- 
    '==
    '==     3403.715- 15July2017-
    '==      -- Make- mbLookupSelection and mbHideEditButtons Default to TRUE..
    '==
    '==   grh. Updated 3519.0322  22-March-2019-
    '==     -- Add full text search functions (Cloned from frmBrowse33..)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 

    '= Const k_version As String = "frmBrowsePOS3-Vers:=05-Aug-2015=8:17pm=="

    '--Const ki_maxChildButtons = 6
    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    Private mFrmParent As Form
    Private msVersionPOS As String = ""

    Private mbIsInitialising As Boolean  '--  SEE myBase.New()  --
    Private mbActivated As Boolean = False
    Private mbCancelled As Boolean = False

    Private mbStartupDone As Boolean = False
    Private mlFormDesignHeight As Integer
    Private mlFormDesignWidth As Integer '--save starting dimensions..-

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private mColTables As Collection
    Private msDBname As String
    Private mCnnSql As OleDbConnection   '= SqlConnection '-- ADODB.Connection
    Private mbIsSqlServer As Boolean

    Private mColPreferredColumns As Collection '--show these first..--
    Private mbShowPreferredColumnsOnly As Boolean = False
    Private msUserSelectList As String = ""

    Private mbLookupSelection As Boolean = True  '==False
    '-False means Admin dblClick goes to edit selected item.-
    '-- TRUE means dblClick returne selected item to caller.

    '== Private mbNoFormCentering As Boolean = False
    Private mbHideEditButtons As Boolean = True  '=False
    Private mIntFormTop As Integer = -1
    Private mIntFormleft As Integer = -1

    Private msInitialFocusColumn As String = ""

    '==Private WithEvents mRst As ADODB.Recordset
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

    Private masOrders() As String '--list of index names--
    Private msWhere As String '--WHERE condition for current browse--
    Private msOriginalWhere As String '--ORIGINAL WHERE condition for current browse--

    Private miCurrentColCount As Short '--no of columns in current grid--
    Private msCurrentPrimaryKeyName As String = "" '--column name current oreder--

    '--  current oreder--
    Private msCurrentOrder As String = "" '--column name current MAJOR order--
    Private msCurrentOrder2 As String = "" '--column name current Minor order--
    Private miCurrentOrderColNo As Short '--column no current oreder--
    Private miCurrentOrderColNo2 As Short '--column no current Minor Order--
    Private mbOrderAsc As Boolean = True '-- current order id True=ASCENDING, False=DESC..-
    Private mbOrder2Asc As Boolean = True '-- current order id-2 True=ASCENDING, False=DESC..-

    Private msCurrentSearch As String = "" '--current find string--

    Private mColPrimaryKeyValues As Collection '--for current record--
    Private mColChildTables As Collection
    '= = = = = = = = = = = = = =
    Private mbClosingDown As Boolean = False
    Private mlFetchComplete As Integer
    Private msFetchError As String
    Private mlRecCount As Integer '--ocunt of recs in rset..--

    Private mColKeyValues As Collection '--PKEYS of selected record-
    Private mColRowValues As Collection '--selected grid row-
    Private msTitle As String
    '= = = = =

    Private mlGridLeft As Integer = 240 '== = VB6.PixelsToTwipsX(MSHFlexgrid1.Left)
    Private mlGridTop As Integer = 900  '==  = VB6.PixelsToTwipsY(MSHFlexgrid1.Top) '--save grid pos..--

    Private mColPrimaryKeyGridCols As New Collection '--saved by getRecordset..-
    Private mlFindTimer As Integer = -1 '--  settling time to drop CR from scanner..-
    '= = = = = = = = = = = = = = = =  = =

    '--   for datagrid--
    '==Private mOleDbDataAdapter1 As OleDbDataAdapter
    '= Private mSqlDbDataAdapter1 As SqlDataAdapter
    Private mDataSet1 As DataSet

    Private mCurrentGridColumn As DataGridViewColumn

    '=3519.0322=- for text srch
    Private masSearchColumns() As String '--list of index names--
    Private mColOriginalColumns As Collection

    '= = = = = = = = = = = = = = = = = = =


    '--properties as input parameters--
    '--properties as input parameters--
    WriteOnly Property FrmParent() As Form
        Set(ByVal value As Form)
            mFrmParent = value
        End Set
    End Property  '= parent form=
    '= = = = =  = = = = = = = ==  == 

    '-version-
    WriteOnly Property versionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
            labVersion.Text = msVersionPOS
        End Set
    End Property  '--version--
    '= = = = = = = = = = = = = = = = = = == 


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

    WriteOnly Property connection() As OleDbConnection   '=  SqlConnection  '== ADODB.Connection
        Set(ByVal Value As OleDbConnection) '==  ADODB.Connection)

            mCnnSql = Value
        End Set
    End Property
    '- - - - - - - - - -

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
    WriteOnly Property Title() As String
        Set(ByVal Value As String)
            '--miCurrentTableIndex = Index
            msTitle = Value
        End Set
    End Property
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
            msOriginalWhere = msWhere
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


    WriteOnly Property ShowPreferredColumnsOnly() As Boolean
        Set(ByVal Value As Boolean)

            mbShowPreferredColumnsOnly = Value
        End Set
    End Property '--preferred..=
    '= = = = = = = = = = = = = = = = =  =

    WriteOnly Property HideEditButtons() As Boolean
        Set(ByVal value As Boolean)
            mbHideEditButtons = value
        End Set
    End Property  '--buttons..--
    '= = = = = = =  = = = = = = =

    '--Mandated location..-
    WriteOnly Property MandatedFormTop() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormTop = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =
    WriteOnly Property MandatedFormLeft() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormLeft = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =


    '--useless..  callers properties are set AFTER load.
    '==WriteOnly Property NoFormCentering() As Boolean
    '==	Set(ByVal Value As Boolean)
    '==		mbNoFormCentering = Value
    '==	End Set
    '==End Property '--no center..-
    '= = = = = = = =  ==

    WriteOnly Property InitialFocusColumn() As String
        Set(ByVal Value As String)
            msInitialFocusColumn = Value

        End Set
    End Property '--initial focus..--
    '= = = = = = = = = = = = =

    '-- called from function other than Admin-
    '--  Dbl Click returne selection..

    WriteOnly Property lookupSelection() As Boolean
        Set(ByVal value As Boolean)
            mbLookupSelection = value
        End Set
    End Property  '-is lookup-
    '= = = = = = = = = = = = = = = = 


    '==  results.
    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property  '--cancelled--
    '= = = = = = = = = = = = = = = = = == 

    '==  result is PKEYS  keyset of selected record--
    ReadOnly Property selectedKey() As Collection
        Get
            selectedKey = mColKeyValues
        End Get
    End Property '-- get key--
    '= = = = = = = =  = =  ==

    '== SECOND result is collection of flds (name/value) of selected grid row--
    ReadOnly Property selectedRow() As Collection
        Get
            selectedRow = mColRowValues
        End Get
    End Property '-- get key--
    '= = = = = = = =  = =  ==


    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '= = = = = = = = = = = =
    '-===FF->

    Private Function msGetDllversion() As String
        Dim assemblyThis As Assembly
        Dim assName As AssemblyName
        Dim s1, sVersion As String

        assemblyThis = System.Reflection.Assembly.GetExecutingAssembly()
        assName = assemblyThis.GetName
        With assName.Version
            sVersion = CStr(.Major) & "." & CStr(.Minor) & "." & CStr(.Build) & "." & Format(.Revision, "0000")
        End With

        msGetDllversion = sVersion
    End Function  '--get version-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  replacing GOSUB--
    '--GoSub getRec_addColumn--

    '-- NEW VERSION with Field Aliases..--
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
            lCount = lCount + 1
            '--  CHECK for ALIAS..-
            s1 = UCase(Trim(sFldName))
            iPos = InStr(s1, " AS ")
            If (iPos > 0) Then  '--have alias..-
                sNativeFldName = Trim(VB.Left(Trim(sFldName), (iPos - 1)))
                sAlias = Trim(Mid(Trim(sFldName), (iPos + 4)))
                sFldList = sFldList & UCase(sNativeFldName) & "/" '--build list for dups check--
                bIsLiteral = (IsNumeric(sNativeFldName)) Or (VB.Left(sNativeFldName, 1) = "'")
            Else  '--native only..-
                sNativeFldName = Trim(sFldName)
                sFldList = sFldList & UCase(sFldName) & "/" '--build list for dups check--
            End If

            If bIsLiteral Then
                If (sFields <> "") Then sFields = sFields & ", "
                sFields = sFields & sFldName  '-- apply as requested.-
            Else
                If Not mColFields.Contains(UCase(sNativeFldName)) Then  '--no such field..
                    MsgBox("No such column as: " & sNativeFldName & " in table: " & msTableName & vbCrLf & _
                                             "Field name will be ignored..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If (sFields <> "") Then sFields = sFields & ", "
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
    '= = = = = = = = = = = =
    '-===FF->

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
        '= Dim lBG As Integer '--save colour--
        '== Dim colField As Collection '--info for 1 field--
        Dim sType, sErrorMsg As String
        Dim cmd1 As New OleDbCommand '=SqlCommand
        '= Dim sqlResult As IAsyncResult

        '--get key info from current table coll.--
        sFields = ""
        sFldList = "/"
        sLastFld = ""
        mbGetRecordset = False
        '--add primary key fields--
        lCount = 0 '--count flds selected--
        '--do user select FIRST..--
        If (msUserSelectList <> "") Then '--caller provides select list..-
            sSql = msUserSelectList
        Else
            '--do preferred columns next..--
            If Not (mColPreferredColumns Is Nothing) Then '--do preferred cols first..-
                For Each v1 In mColPreferredColumns
                    sFldName = CStr(v1)
                    Call mGetRec_addColumn(sFldName, lCount, sFldList, sFields, sLastFld) '==GoSub getRec_addColumn
                Next v1 '--pref. s1--
            End If '--preferred..--
            If mColPrimaryKeys.Count() > 0 Then
                '---If sOrder = "" Then sOrder = mColPrimaryKeys(1)
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
            End If
            If Not mbShowPreferredColumnsOnly Then '--  more columns..-
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
                            Call mGetRec_addColumn(sFldName, lCount, sFldList, sFields, sLastFld)
                            '== GoSub getRec_addColumn
                        End If '--lcount--
                    Next i
                    '-End If  '--lastfld--
                End If '--count..-
            End If '-- preferred only..--
            sSql = "SELECT " & sFields
        End If  '-user select-

        '- user can JOIN- (if FROM already there, don't add it...)-
        If (InStr(UCase(sSql), " FROM ") <= 0) Then  '-no FROM, add.
            sSql = sSql & " FROM [" & msTableName & "] "
        End If
        Labwhere.Text = ""
        If (msWhere <> "") Then Labwhere.Text = "  Where " & msWhere
        '== sSql = "SELECT " & sFields & " FROM [" & msTableName & "] " '--+ " WHERE " + msWhere
        If (msWhere <> "") Then sSql = sSql & " WHERE " & msWhere

        '-- Sort order--
        sOrder = ""
        If (msCurrentOrder <> "") Then
            '== UPGRADE ERROR= sOrder = CStr(CDbl(" ORDER BY " & msCurrentOrder) + IIf(mbOrderAsc, " ASC", " DESC"))
            sOrder = " ORDER BY [" & msCurrentOrder & IIf(mbOrderAsc, "] ASC", "] DESC")
            If (msCurrentOrder2 <> "") Then
                '== sOrder = CStr(CDbl(sOrder & ", " & msCurrentOrder2) + IIf(mbOrder2Asc, " ASC", " DESC"))
                sOrder = sOrder + ", [" & msCurrentOrder2 & IIf(mbOrder2Asc, "] ASC", "] DESC")
            End If '--order2
        End If '--Orders--
        sSql = sSql & sOrder
        Labwhere.Text = Labwhere.Text & vbCrLf & sOrder
        mlFetchComplete = -1 '--not ready-
        '--mRst.Properties("Initial Fetch Size") = 0       '--ensure progress event--
        '--mRst.Properties("Background Fetch Size") = 15  '--default--

        '= mRst.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        '--lBG = Labwhere.BackColor       '--save--
        '--Labwhere.BackColor = vbYellow  '--progress--
        '--On Error Resume Next
        '== On Error GoTo mbGetRecordset_error
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '== mRst.Open(sSql, mCnnSql, ADODB.CursorTypeEnum.adOpenStatic, _
        '==                  ADODB.LockTypeEnum.adLockReadOnly, ADODB.ExecuteOptionEnum.adAsyncFetch)

        '=  msLastSqlErrorMessage = ""
        cmd1.Connection = mCnnSql
        cmd1.CommandText = sSql
        sErrorMsg = ""
        Try
            '== OLEDB = No Asynch.==
            '== OLEDB = sqlResult = cmd1.BeginExecuteReader()
            '== OLEDB = ' Although it is not necessary, the following procedure
            '== OLEDB = ' displays a counter in the console window, indicating that 
            '== OLEDB = ' the main thread is not blocked while awaiting the command 
            '== OLEDB = ' results.
            '== OLEDB = Dim count As Integer
            '== OLEDB = While Not sqlResult.IsCompleted
            '== OLEDB = count += 1
            '== OLEDB = '== Console.WriteLine("Waiting ({0})", count)
            '== OLEDB = labStatus.Text = "Waiting " & count
            '== OLEDB = DoEvents()
            '== OLEDB = ' Wait for 1/10 second, so the counter
            '== OLEDB = ' does not consume all available resources 
            '== OLEDB = ' on the main thread.
            '== OLEDB = Threading.Thread.Sleep(100)
            '== OLEDB = End While
            '== OLEDB = System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            '== OLEDB = ' Once the IAsyncResult object signals that it is done
            '== OLEDB = ' waiting for results, you can retrieve the results.
            '== OLEDB = mSqlRdr1 = cmd1.EndExecuteReader(sqlResult)
            '== OLEDB = labStatus.Text = "Fetch Completed."

            mSqlRdr1 = cmd1.ExecuteReader()
            labStatus.Text = "Fetch Completed."

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

        '--wait for completion event--
        '== On Error GoTo 0

        '== While (mlFetchComplete < 0) '--Wait for fetchComplete event..--
        '== System.Windows.Forms.Application.DoEvents()
        '== End While '--fetch--
        '--If Not gbGetResultset(mCnnSql, msTableName, rs1, sFields, msWhere, sOrder) Then
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If (sErrorMsg <> "") Then  '==mlFetchComplete <> 0 Then
            MsgBox("Failed to get recordset for table:  '" & msTableName & "'" & vbCrLf & _
                                             sErrorMsg, MsgBoxStyle.Exclamation)
            If (gsErrorLogPath <> "") Then
                If Not gbLogMsg(gsErrorLogPath, sErrorMsg) Then
                    MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
                End If
            End If '--log--
            '--Me.Hide
            '--Exit Function
        Else '--ok--
            '--MsgBox "ok.. got " & rs1.RecordCount & " records.."
            mbGetRecordset = True
            LabFind.Text = "Find: " & vbCrLf & "(" & LCase(msCurrentOrder) & ")" '--show key order--
            System.Windows.Forms.Application.DoEvents()
        End If
        '--Labwhere.BackColor = lBG '--restore--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '==On Error GoTo 0

        Exit Function

    End Function '-get record set--
    '= = = = =
    '-===FF->

    '--load data flexgrid--
    '--  NOW DataGridView --

    '== Private Function mbLoadGrid(ByRef rs As ADODB.Recordset) As Boolean
    Private Function mbLoadGrid(ByRef sqlRdr1 As OleDbDataReader) As Boolean

        Dim rx, L1, i, k, cx As Integer
        '== Dim rsVar As Object
        Dim sName As String
        Dim lCharWidth As Integer '--total no chars to show--
        Dim lTwipWidth As Integer '--grid size in twips--
        Dim lTwipsPerChar As Integer '-- one char size in twips--(from point size)-
        Dim lMaxColSize As Integer
        Dim laWidths() As Integer
        Dim lngRecCount As Integer
        Dim d1 As Date
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim column1 As DataColumn

        '== Dim rsTemp As ADODB.Recordset
        '== mOleDbDataAdapter1 = New OleDbDataAdapter

        dataTable1 = New DataTable
        '--  Get data "recordset" to internal table..
        dataTable1.Load(sqlRdr1)
        dataTable1.TableName = msTableName
        mDataSet1 = New DataSet
        mDataSet1.Tables.Add(dataTable1)
        '= mOleDbDataAdapter1.Fill(mDataSet1, rsTemp, msTableName)

        If Not mbStartupDone Then '--First time..--
            '==?== MSHFlexgrid1.RowHeightMin = 300 '--about 2 lines..-
            '--clear current col collection-
            mColCurrentCols = New Collection

            lMaxColSize = lTwipWidth \ 3 '--limit any col..-- to 33 %--
            Erase laWidths
            '==?== MSHFlexgrid1.set_Cols(0, rs.Fields.Count)

            miCurrentColCount = dataTable1.Columns.Count   '== rs.Fields.Count
            '--Allocate col widrhs in proportion to their char width.--
            '--compute width of all flds by their max sizes--
            lCharWidth = 0
            For Each column1 In dataTable1.Columns '==  For i = 0 To rs.Fields.Count - 1
                sName = column1.ColumnName   '== rs.Fields(i).Name
                k = column1.MaxLength  '== rs.Fields(i).DefinedSize
                If column1.DataType Is GetType(Int32) Then  '=gbIsNumericType(gsGetSqlType((rs.Fields(i).Type), k)) Then
                    k = 8 '--initial size for numerics.-
                Else
                    k = (k \ 2) '--allocate 50 percent of max size--
                End If
                k = IIf(k > 20, 20, k) '-allow initial max 24 per fld--
                If (LCase(sName) = "surname") Or (LCase(sName) = "given_names") Then
                    k = 24
                ElseIf (LCase(sName) = "description") Then
                    k = 50
                End If
                If k < Len(sName) Then k = Len(sName) + 1 '--col name must fit..--
                If k < 5 Then k = 5 '--min--
                k = k + 3 '--allow for sort arrow..-
                lCharWidth = lCharWidth + k
                ReDim Preserve laWidths(i)
                laWidths(i) = k '--save all col char widths--
            Next '=i
            '--L1 = (lTwipWidth \ lCharWidth) '--twips ALLOCATED per char--
            L1 = lTwipsPerChar '-- ALLOCATE width needed per char--
            If L1 <= 0 Then L1 = 110 '--eg 9-twipsPerPoint-
            '-- Set column names in the grid
            For Each column1 In dataTable1.Columns '== = 0 To rs.Fields.Count - 1
                k = L1 * laWidths(i) '--get twips per char-Times chars--
                If k > lMaxColSize Then k = lMaxColSize
                sName = column1.ColumnName   '=rs.Fields(i).Name
                '--set col width--
                '==?== MSHFlexgrid1.set_ColWidth(i, 0, k) '--set width--
                mColCurrentCols.Add(sName)
                '--set current order col to Italic--
                If UCase(sName) = UCase(msCurrentOrder) Then
                    miCurrentOrderColNo = i
                End If
            Next '=i '--field-
        End If '--FIRST time..-
        '== end setup==

        If dataTable1.Rows.Count <= 0 Then  '=(rs.BOF And rs.EOF) Then '--empty-
            lngRecCount = 0
        Else '--move pointer to set up reccount
            '==rs.MoveLast() '--RuntimeError if r/set empty !!--
            '= rs.MoveFirst()
            lngRecCount = dataTable1.Rows.Count  '== rs.RecordCount
            '--rsVar = rs.GetString(adClipString)  '--rs.RecordCount)--
        End If '--empty--
        '-- Set range of cells in the grid
        '---  and load all data from recordset--
        '==3301.711=
        LabRecCount.Text = CStr(lngRecCount)  '==3301.711=
        '== If (lngRecCount > 0) Then
        '--LOAD all data rows-- Can't take CR's--
        '==?== MSHFlexgrid1.Recordset = rs

        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridView1.ColumnHeadersHeight = 29  '--increase header height.

        '--  lOAD the grid..--
        DataGridView1.DataSource = mDataSet1
        DataGridView1.DataMember = msTableName

        '--  make all columns  AUTOMATIC  sort.--
        For cx = 0 To DataGridView1.Columns.Count - 1   '= (MSHFlexgrid1.get_Cols() - 1)
            DataGridView1.Columns(cx).SortMode = DataGridViewColumnSortMode.Automatic
        Next cx

        '--  set selected hdr colour..-
        Dim colHdr1 As DataGridViewColumnHeaderCell
        colHdr1 = DataGridView1.Columns(miCurrentOrderColNo).HeaderCell
        colHdr1.Style.BackColor = Color.Yellow
        '== MUST set new FONT object..-  colHdr1.Style.Font.Italic = True

        '--set current col hdr--
        '-- now AUTO..   no NEED --
        '== If mbOrderAsc Then
        '=-------------- MSHFlexgrid1.CellPicture = PicArrowUp.Image '-- ASC--
        '== colHdr1.SortGlyphDirection = SortOrder.Ascending
        '== Else
        '=---------------- MSHFlexgrid1.CellPicture = PicArrowDown.Image '-- DESC.-
        '== colHdr1.SortGlyphDirection = SortOrder.Descending
        '== End If

        '--- for Jet DB's, we must convert all DATE columns to yyyy-mm-dd  --
        '----  since we could not use CONVERT in the SELECT statement--

        '== NO Jet Now..==
        '== NO Jet Now..==

        '--  MUST sort the first time..--
        DataGridView1.Sort(DataGridView1.Columns(miCurrentOrderColNo), System.ComponentModel.ListSortDirection.Ascending)

        '--MSHFlexgrid1.Visible = True
        '== End If '--not empty-
        '== ?? =  MSHFlexgrid1.Redraw = True '--ok show changes..-
        LabFind.Text = "Searching: " & msCurrentOrder & ".." '--show key order--
        txtFind.Enabled = True '--allow change event-
        '== SEE reload..-- txtFind.Focus()

        mDataSet1.Dispose()
        '==rsTemp = Nothing
        System.Windows.Forms.Application.DoEvents()
    End Function '--load grid--
    '= = = = =  =
    '-===FF->

    '--  sort order Changed..--

    '--  A u t o sort  --  THIS NOW CALLED AFTER the SORT  --
    '--               ie from the DataGridView.sorted event. 
    '--  Just to record, and to change header colour..--
    '--  sName is header text of NEWly-clicked column--

    Private Function mbSortColumn(ByRef sName As String) As Boolean
        Dim ix, lCol As Integer
        Dim colHdr1 As DataGridViewColumnHeaderCell

        mbSortColumn = True
        lCol = 0
        '--  find col-no..  sName is col.hdr.-
        For ix = 0 To DataGridView1.Columns.Count - 1   '= (MSHFlexgrid1.get_Cols() - 1)
            If LCase(sName) = LCase(DataGridView1.Columns(ix).HeaderCell.Value) Then
                '== LCase(Trim(MSHFlexgrid1.get_TextMatrix(0, ix))) Then
                lCol = ix
                Exit For
            End If
        Next ix
        txtFind.Enabled = False '--stop change event-

        '--set old sort col to normal--
        '--set old sort col to normal--

        colHdr1 = DataGridView1.Columns(miCurrentOrderColNo).HeaderCell
        '====  colHdr1.SortGlyphDirection = SortOrder.None   '--clear arrow.-

        '--demote old order to Minor--
        If LCase(sName) <> LCase(msCurrentOrder) Then '--new column..-
            msCurrentOrder2 = msCurrentOrder
            mbOrder2Asc = mbOrderAsc
            colHdr1.Style.BackColor = Color.LightGray     '--  SHOULD be from SAVED original colour..--
        End If

        '-- Set range of SORT ROWS as full grid--
        '--different col- or same col but was DESC..  then make ASC..--
        If (UCase(sName) <> UCase(msCurrentOrder)) Then '--new col.-
            '--set new order--
            msCurrentOrder = sName '--masOrders(lCol)
            miCurrentOrderColNo = lCol
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
        '== Call mbReload(True)


        '--  set new column hdr colour..-
        colHdr1 = DataGridView1.Columns(miCurrentOrderColNo).HeaderCell
        colHdr1.Style.BackColor = Color.Yellow

        System.Windows.Forms.Application.DoEvents()

        txtFind.Text = "" '--clear for new sort column-

        '--set current col hdr--
        '=== If mbOrderAsc Then
        '===     Set MSHFlexgrid1.CellPicture = PicArrowUp.Picture '-- ASC--
        '=== Else
        '===       Set MSHFlexgrid1.CellPicture = PicArrowDown.Picture '-- DESC.-
        '=== End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        LabFind.Text = "Find in: " & "'" & msCurrentOrder & "'" '--show key order--
        txtFind.Enabled = True '--allow change event-
        txtFind.Focus()

    End Function '--sort..-
    '= = = = = = = = =
    '-===FF->

    '--Get and load new r/set--

    Private Function mbReload() As Boolean

        Dim lBG As Integer

        mbReload = False
        On Error Resume Next
        If Not (mSqlRdr1 Is Nothing) Then
            mSqlRdr1.Close()
            mSqlRdr1 = Nothing
        End If
        On Error GoTo 0
        lBG = System.Drawing.ColorTranslator.ToOle(LabRecCount.BackColor) '--save--

        '--Labwhere.BackColor = vbYellow  '--progress--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If mbGetRecordset() Then
            '==If mlRecCount > 0 Then
            LabRecCount.BackColor = System.Drawing.Color.Magenta '--loading grid--
            System.Windows.Forms.Application.DoEvents()
            Call mbLoadGrid(mSqlRdr1)
            mbReload = True
            txtFind.Enabled = True '--allow change event-
            '--txtFind.SetFocus
            mlFindTimer = -1 '-inactive.-

            '--select 1st data row..-
            If DataGridView1.Rows.Count > 0 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(0)
                txtFind.Focus()
            Else '--empty..-
                MsgBox("No records..", MsgBoxStyle.Information)
            End If
            '==  mRst.Close()
            '=End If '--count-
            '--If mRst.State = adStateOpen Then mRst.Close
        Else '--error..-
        End If '--get.-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        LabRecCount.BackColor = Color.Transparent    '=System.Drawing.Color.Lime '--progress--
        '--- Set mRst = Nothing
    End Function '--reload--
    '= = = = = = = = = = = =
    '-===FF->

    '---select record--

    Private Sub mSelectRecord(ByVal lngRow As Integer)

        Dim sName As String
        Dim cx As Integer
        Dim colFld As Collection
        Dim v1 As Object

        '--setup key-values col. of current row for retrieval of correct full record--
        mColKeyValues = New Collection
        For Each v1 In mColPrimaryKeys
            '--find column name--
            For cx = 0 To DataGridView1.Columns.Count - 1   '===  (MSHFlexgrid1.get_Cols() - 1)
                If LCase(CStr(v1)) = LCase(DataGridView1.Columns(cx).HeaderCell.Value) Then
                    '== LCase(MSHFlexgrid1.get_TextMatrix(0, cx)) Then
                    '--found this key col.-
                    '===  mColKeyValues.Add(MSHFlexgrid1.get_TextMatrix(lngRow, cx)) '--save key value for row..-
                    mColKeyValues.Add(DataGridView1.Rows(lngRow).Cells(cx).Value) '--save key value for row..-
                    Exit For
                End If '--found-
            Next cx
        Next v1
        '-- send back complete row  from grid..--
        mColRowValues = New Collection
        For cx = 0 To DataGridView1.Columns.Count - 1   '=== (MSHFlexgrid1.get_Cols() - 1)
            colFld = New Collection
            '== colFld.Add(LCase(MSHFlexgrid1.get_TextMatrix(0, cx)), "name") '--col hdr..-
            sName = DataGridView1.Columns(cx).HeaderCell.Value
            colFld.Add(LCase(sName), "name") '--col hdr..-

            '== colFld.Add(MSHFlexgrid1.get_TextMatrix(lngRow, cx), "value") '--col hdr..-
            colFld.Add(DataGridView1.Rows(lngRow).Cells(cx).Value, "value") '--col hdr..-
            mColRowValues.Add(colFld, LCase(sName))
        Next cx

    End Sub '--select--
    '= = = = = = = = = = = = = =
    '-===FF->

    Private Function mbEditRecord(ByVal intRow As Integer)
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--POS sql connenction..-
        frmEdit1.colTables = mColTables
        frmEdit1.DBname = msDBname
        frmEdit1.tableName = msTableName
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = False
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName

        '--- set PKEY WHERE condition for edit.--
        frmEdit1.selectedKey = mColKeyValues
        frmEdit1.PreferredColumns = mColPreferredColumns
        frmEdit1.Title = "Editing " & msTableName & " record"

        frmEdit1.versionPOS = msVersionPOS

        frmEdit1.ShowDialog()

        frmEdit1.Close()
        '--get new r/set, load grid--
        If mbReload() Then

        End If

    End Function  '--edit-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--l o a d STUFF  goes to InitializeComponent.. ===----
    '--l o a d----
    '--l o a d----
    '--l o a d STUFF  goes to InitializeComponent.. ===----
    '--l o a d STUFF  goes to InitializeComponent.. ===----

    '== Private Sub frmBrowse_Load(ByVal eventSender As System.Object, _
    '==                                 ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    '== Dim L1 As Integer

    '--reset stuff--
    '--gbclosingDown = False
    '---clear combo box ---
    '==     msDBname = ""
    '==     txtFind.Enabled = False
    '==     txtFind.Text = ""
    '==     mbStartupDone = False
    '==     mbClosingDown = False
    '==     mlFormDesignHeight = VB6.PixelsToTwipsY(Me.Height) '--save starting dimensions..-
    '==     mlFormDesignWidth = VB6.PixelsToTwipsX(Me.Width) '--save starting dimensions..-

    '== '-- frame1.Caption = ""
    '== '--Frame2.Caption = ""
    '==     msCurrentSearch = ""
    '==     msCurrentOrder = "" '-Major..-
    '==     msCurrentOrder2 = "" '--Minor--

    '==     mbInitialFocusColumn = ""

    '== '---MSHFlexgrid1.Enabled = False
    '==     L1 = MSHFlexgrid1.get_RowHeight(0) \ 2 '-- to add 50% --
    '==     MSHFlexgrid1.set_RowHeight(0, MSHFlexgrid1.get_RowHeight(0) + L1) '--deeper hdrs..-

    '==     msTableName = ""
    '==     Me.Text = k_version
    '== '--cboOrder.Enabled = False  '--no order
    '==     msWhere = ""
    '==     mColKeyValues = New Collection
    '==     mColRowValues = New Collection
    '==     cmdOk.Enabled = False
    '== '======mnuRecord.Enabled = False
    '==     msTitle = ""

    '==     mlGridLeft = VB6.PixelsToTwipsX(MSHFlexgrid1.Left)
    '==     mlGridTop = VB6.PixelsToTwipsY(MSHFlexgrid1.Top) '--save grid pos..--
    '==     mlRecCount = 0
    '==     mbOrderAsc = True '--default is ASC..-
    '==     mbOrder2Asc = True
    '==     mlFindTimer = -1 '-inactive.-
    '==     mbShowPreferredColumnsOnly = False
    '==     mbNoFormCentering = False '-- default is to centre form..

    '== End Sub '--load--
    '- - - - - - - -  -

    '--l o a d STUFF  goes to InitializeComponent.. ===----
    '--l o a d STUFF  goes to InitializeComponent.. ===----
    '--activate--
    '--activate--
    '--activate stuff is now the  LOAD EVENT..--
    '--activate stuff is now the  LOAD EVENT..--
    '--activate stuff is now the  LOAD EVENT..--

    'UPGRADE_WARNING: Form event frmBrowse.Activate has a new behavior. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '== Private Sub frmBrowse_Activated(ByVal eventSender As System.Object, _
    '==                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

    '--  USER PROPERTIES not yet set  !!  ==
    '--  USER PROPERTIES not yet set  !!  ==
    '--  USER PROPERTIES not yet set  !!  ==

    Private Sub frmBrowse_Load(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim L1, intPos As Integer
        '== Dim colTable As Collection
        '== Dim colTableInfo As Collection
        Dim idx, cx, tx As Integer
        Dim colP1 As Collection
        Dim s1, s2 As String
        Dim colFieldx As Collection

        If mbStartupDone Then Exit Sub

        cmdOk.Enabled = False
        grpBoxMainFooter.Text = ""

        '--  stuff from original LOAD..--
        mlFormDesignHeight = Me.Height '--save starting dimensions..-
        mlFormDesignWidth = Me.Width '--save starting dimensions..-

        mColKeyValues = New Collection
        mColRowValues = New Collection

        Call CenterForm(Me)

        '== mlGridLeft = VB6.PixelsToTwipsX(MSHFlexgrid1.Left)
        '== mlGridTop = VB6.PixelsToTwipsY(MSHFlexgrid1.Top) '--save grid pos..--
        mlRecCount = 0
        mbOrderAsc = True '--default is ASC..-
        mbOrder2Asc = True
        mlFindTimer = -1 '-inactive.-

        '-- END stuff from original LOAD..--

        txtFind.Text = ""
        '--cboTables.Clear
        '--cboOrder.Clear
        '--build cbo list of valid tables--
        '---make combo box visible and add list-
        '=s1 = "=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & ", Build: " & _
        '=                        My.Application.Info.Version.Build & ", Rev: " & My.Application.Info.Version.Revision & "="
        Me.Text = "Browsing Table:  " & msTableName & ".  " & msVersionPOS    '= & "  (JobMatix:  " & s1 & ")."

        If (msDBname = "") And mbIsSqlServer Then
            MsgBox("DB name empty in frmMaint startup..", MsgBoxStyle.Critical)
            Me.Hide()
            Exit Sub
        End If
        If mColTables.Count() <= 0 Then
            MsgBox("Tables collection empty in frmMaint startup..", MsgBoxStyle.Critical)
            '--Me.Hide
            Exit Sub
        End If
        '=== If Not mbNoFormCentering Then Call CenterForm(Me)

        '--user may overrise..-
        Call CenterForm(Me)
        If Not gbDebug Then Labwhere.Visible = False

        '--hide all childTable buttons to start--
        '--For i = 1 To ki_maxChildButtons
        '--      cmdchildTable(i - 1).Visible = False
        '--Next i
        '--miListIndex = miCurrentTableIndex
        '--idx = miListIndex
        '--set up stuff for current table--
        '-Set mColTable = mColTables(idx)
        mColTable = mColTables.Item(msTableName)
        '--Set mColTableInfo = mColTable(1)
        mColFields = mColTable.Item("FIELDS")
        mColPrimaryKeys = mColTable.Item("PRIMARYKEYS")
        mColOtherIndexes = mColTable.Item("OTHERINDEXES")
        '--msTableName = mColTable("TABLENAME")
        '--make a list of our chilren--
        '--find all tables with out table as parent--
        mColChildTables = New Collection
        For cx = 1 To mColTables.Count()
            If (mColTables.Item(cx)("PARENTS").Count > 0) Then '--this tables has parents--
                colP1 = mColTables.Item(cx)("PARENTS")
                For idx = 1 To colP1.Count() '--check if this is our child--
                    If UCase(msTableName) = UCase(colP1.Item(idx)) Then
                        mColChildTables.Add(mColTables.Item(cx)("TABLENAME"))
                        Exit For '--one is enough--
                    End If
                Next idx
            End If '--parents-
        Next cx
        tx = 0 '--count tables in browse list--
        '--For idx = 1 To mColTables.count
        '--   Set colTable = mColTables(idx)
        '--Set colTableInfo = colTable(1)
        '--add table name to combo list--

        cmdNew.Enabled = False
        cmdEdit.Enabled = False
        '--cmdBrowse.Enabled = False
        txtFind.Enabled = False

        '--finish table setup/load rset etc==
        msCurrentOrder = ""
        txtFind.Text = ""
        tx = -1
        '--cboOrder.Clear
        Erase masOrders '--clear--
        If mColPrimaryKeys.Count() > 0 Then
            s1 = mColPrimaryKeys.Item(1)
        Else '--use col-0 --
            s1 = mColFields.Item(1)("NAME")
        End If
        msCurrentPrimaryKeyName = s1
        tx = tx + 1
        ReDim Preserve masOrders(tx)
        masOrders(tx) = s1
        '--set up other indexes--
        If mColOtherIndexes.Count() > 0 Then
            For idx = 1 To mColOtherIndexes.Count()
                colFieldx = mColOtherIndexes.Item(idx)
                If colFieldx.Count() > 0 Then
                    '--cboOrder.AddItem colfieldx.item(1)
                    tx = tx + 1
                    ReDim Preserve masOrders(tx)
                    masOrders(tx) = colFieldx.Item(1) '--use 1st col of index only--
                End If
            Next idx
        End If
        If tx >= 0 Then '--cboOrder.ListCount > 0 Then
            '--cboOrder.Enabled = True
            txtFind.Enabled = True
            msCurrentOrder = masOrders(0) '--cboOrder.List(0)  '--start with prim.--
            '--cboOrder.ListIndex = 0             '--select first item--
        Else
            '--cboOrder.Enabled = False  '--no order
            txtFind.Enabled = False
        End If '--count--

        Dim sPrefFields As String = ""  '=3519.0322=
        '-- order to preferred col if defined..-
        '-- order to preferred col if defined..-
        If Not (mColPreferredColumns Is Nothing) Then
            If mColPreferredColumns.Count() > 0 Then
                msCurrentOrder = mColPreferredColumns.Item(1) '--first pref.col.-
                If mColPreferredColumns.Count() > 1 Then
                    msCurrentOrder2 = mColPreferredColumns.Item(2) '--second (Minor) pref.col.-
                End If '--count..-
            End If '-->0-
            '-make searchable list-
            For Each s1 In mColPreferredColumns
                sPrefFields &= LCase(s1) & ";"
            Next s1
        End If '--nothing..-

        '--USER can override initial order..-
        If (msInitialFocusColumn <> "") Then '--overrides initial order..-
            msCurrentOrder = msInitialFocusColumn
        End If
        '----mnuAddNew.Caption = "Add New " + msTableName + " record"

        '- position us on top of calling form..
        If mFrmParent Is Nothing Then
            Call CenterForm(Me)
        Else
            Me.Left = mFrmParent.Left + 16
            Me.Top = mFrmParent.Top + 50
        End If

        '--cmdBrowse.Enabled = True
        txtFind.Enabled = True
        '-- frame1.Caption = ""   '--" Table: '" + msTableName + "' "
        labTable.Text = msTitle
        If msTitle = "" Then labTable.Text = "Selecting from: '" & msTableName & "' table."

        labVersion.Text = "POS dll Version: " & msGetDllversion()        '== 3311.303=

        '=3519.0322=
        '=3519.0322=
        '=3501.1106-  !! RE-DO a new list of DB cols EX the aliases..
        '-  Build DB column name collection for column SEARCHes..
        '--  Analyse and deconstruct Prefs/aliases..
        mColOriginalColumns = New Collection
        '=mColPreferredColumnsActual = New Collection
        If Not (mColPreferredColumns Is Nothing) Then
            For Each sPrefEntry As String In mColPreferredColumns
                intPos = InStr(LCase(sPrefEntry), " as ")
                If (intPos > 0) Then
                    s1 = Trim(VB.Left(sPrefEntry, intPos - 1))  '-orig native col.-
                    s2 = Trim(Mid(sPrefEntry, intPos + 4))  '-alias-
                    mColOriginalColumns.Add(s1, s2)  '- key is alias, -returns value native-
                    '= mColPreferredColumnsActual.Add(s2)  '--save alias as actual grid col.name
                Else  '-not aliased.
                    mColOriginalColumns.Add(Trim(sPrefEntry), Trim(sPrefEntry))  '--returns self-
                    '= mColPreferredColumnsActual.Add(Trim(sPrefEntry), Trim(sPrefEntry))
                End If
            Next  '-colPrefEntry-
        End If  '-nothing-

        '=3519.0322=
        '--  Collect Text Search Columns.
        masSearchColumns = {} '--list of index names--
        Dim sName, sType, sList As String
        Dim intCount As Integer = 0
        sList = ""
        '=3501.1106-  !! RE-DO a new list of DB cols EX the aliases..
        If mColOriginalColumns.Count > 0 Then
            '-- get data type of col. and add to srch list if text..
            For Each sName In mColOriginalColumns
                If mColFields.Contains(sName) Then
                    colFieldx = mColFields.Item(sName)
                    sType = colFieldx.Item("TYPE_NAME")
                    If gbIsText(sType) Then
                        ReDim Preserve masSearchColumns(intCount)
                        masSearchColumns(intCount) = sName
                        intCount += 1
                    End If
                End If  '-contains-
            Next sName
        Else  '- no prefs.  Look in colTables.
            For Each colFieldx In mColFields
                sName = colFieldx.Item("NAME")
                sType = colFieldx.Item("TYPE_NAME")
                '- pref flds override, if any-
                If (sPrefFields = "") OrElse _
                               ((sPrefFields <> "") AndAlso (InStr(sPrefFields, LCase(sName) & ";") > 0)) Then
                    If gbIsText(sType) Then
                        ReDim Preserve masSearchColumns(intCount)
                        masSearchColumns(intCount) = sName
                        intCount += 1
                    End If
                End If  '-prefs-
            Next colFieldx
        End If  '-count-
        '--TEST show list-
        For intCount = 0 To masSearchColumns.Length - 1
            sList &= masSearchColumns(intCount) & "; "  '-- for test msg-
        Next intCount
        '== MsgBox("TEST-Search Columns are:" & vbCrLf & sList)
        '-- collect cols done..--
        txtTextSearch.Text = ""

    End Sub '--load--
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-Activated-
    Private Sub frmBrowse_Activated(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '--activated-
    '= = = = = = = == == 

    '--shown-

    Private Sub frmBrowse_Shown(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        'If mbActivated Then Exit Sub
        'mbActivated = True

        If mbHideEditButtons Then
            cmdEdit.Visible = False
            cmdNew.Visible = False
        Else  '-- edit visible- don;t need ok-
            cmdOk.Visible = False
        End If
        If mIntFormTop >= 0 Then
            Me.Top = mIntFormTop
        End If
        If mIntFormleft >= 0 Then
            Me.Top = mIntFormleft
        End If

        If mbReload() Then '--get new r/set, load grid--
            '=== mbOrderAsc = False
            '=== Call mbSortColumn(msCurrentOrder)  '--set up selected col. for ASC.-
            mbStartupDone = True '--1st time is done..-
            '= txtFind.Focus()
        Else '--no recs-
            MsgBox("No records..", MsgBoxStyle.Exclamation)
            '== Me.Hide()
            '== Exit Sub
        End If
        cmdNew.Enabled = True
        If DataGridView1.SelectedRows.Count > 0 Then
            cmdEdit.Enabled = True
        End If
        '=3519.0322=
        txtTextSearch.Select()

    End Sub  '--shown--
    '= = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--  form resized..--
    'UPGRADE_WARNING: Event frmBrowse.Resize may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub frmBrowse_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        '--  cant make smaller than original..-
        If (Me.Height < mlFormDesignHeight) Then Me.Height = mlFormDesignHeight
        If (Me.Width < mlFormDesignWidth) Then Me.Width = mlFormDesignWidth
        labTable.Width = Me.Width

        '--resize grid..--
        DataGridView1.Width = Me.Width - 40  '== (Me.Width - 40)
        DataGridView1.Height = (Me.Height - DataGridView1.Top - grpBoxMainFooter.Height - 50)
        panelEdit.Left = Me.Width - panelEdit.Width - 36

        grpBoxMainFooter.Top = Me.Height - grpBoxMainFooter.Height - 46
        grpBoxMainFooter.Width = DataGridView1.Width - 3
        panelOK.Left = grpBoxMainFooter.Width - panelOK.Width

        '==cmdOk.Top = DataGridView1.Top + DataGridView1.Height + 13
        '== cmdCancel.Top = cmdOk.Top
        '==LabRecCount.Top = cmdOk.Top
        '==Label2.Top = cmdOk.Top
        '== Labwhere.Top = (DataGridView1.Top + DataGridView1.Height + 8)

    End Sub '--resize..-
    '= = = = = = = = = = =

    '-- 200ms timer to discard CR from Scanner --
    '----- Discard all CR coming within 2600ms of last data char..--

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles Timer1.Tick

        If (mlFindTimer >= 0) Then '--active.--
            mlFindTimer = mlFindTimer + 200
            If (mlFindTimer >= 2600) Then '--can accept cr now..-
                mlFindTimer = -1 '--timeed out..--
            End If

        End If '--active..--
    End Sub '--timer..-
    '= = = = = = =  =
    '-===FF->

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles txtFind.Enter

        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        '= LabFind.Font. = VB.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        '= LabFind.Font.Bold = False   '== = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--txtFind..  TRAP CR and discard.. (For barcode scanner..)
    '--txtFind..  TRAP CR and discard.. (For barcode scanner..)

    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter..--
            If mlFindTimer >= 0 Then '--  still settling--
                '--discard..  probably from scanner..--
            Else '--timer over..  can accept CR from opo..
                Call cmdOk_Click(cmdOk, New System.EventArgs())
            End If
            keyAscii = 0 '--processed.-
        End If
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = = = = = =
    '= = = = = = = = = =  =
    '-===FF->

    '--change in find argument--
    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Dim s1, sGrid, sMsg As String
        Dim colField As Collection
        Dim sType, sArg As String
        Dim sSample As String
        Dim iArgLen, selRow As Integer
        Dim intResult As Integer
        Dim intOrigCol As Integer = 0
        Dim rx, rowPos, colPos As Integer
        Dim double1, double2 As Double

        '--ignore if in init..---
        If mbIsInitialising Then Exit Sub
        On Error GoTo txtFind_Error
        If Not mbStartupDone Then Exit Sub

        '-  save position..-
        intOrigCol = DataGridView1.FirstDisplayedCell.ColumnIndex

        If (DataGridView1.Rows.Count <= 0) Then Exit Sub
        '-- strip leading zeroes..--
        '--  ?? ONLY for Barcodes and SerialNos..--
        While (VB.Left(txtFind.Text, 1) = "0")
            txtFind.Text = Mid(txtFind.Text, 2)
        End While
        '--find value--  save row no--
        s1 = UCase(txtFind.Text)

        '==MsgBox("Arg is :" & s1)

        If txtFind.Enabled And ((msCurrentSearch <> "") And _
                                   (msCurrentSearch <> s1)) Or ((msCurrentSearch = "") And (s1 <> "")) Then
            '--do it now if rs active--
            '=== If Not (mRst Is Nothing) Then
            '===   If Not (mRst.BOF And mRst.EOF) Then  '--not empty-
            colField = mColFields.Item(UCase(msCurrentOrder)) '--get this field stuff-
            sType = LCase(colField.Item("TYPE_NAME")) '--get sql type--
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '==Now Manual Search= If gbIsNumericType(sType) Then
            '==Now Manual Search= If Trim(s1) = "" Then s1 = "0"
            '==Now Manual Search= If IsNumeric(s1) And (Len(s1) <= 7) Then
            '==Now Manual Search= '== msCurrentSearch = s1
            '==Now Manual Search= '== sArg = msCurrentOrder & ">=" & msCurrentSearch '--no quotes--
            '==Now Manual Search= msCurrentSearch = s1
            '==Now Manual Search= If mbOrderAsc Then
            '==Now Manual Search= sArg = msCurrentOrder & ">=" & msCurrentSearch '--no quotes--
            '==Now Manual Search= Else '--DESCENDING..-
            '==Now Manual Search= sArg = msCurrentOrder & "<=" & msCurrentSearch '--no quotes--
            '==Now Manual Search= End If
            '==Now Manual Search= Else
            '==Now Manual Search= MsgBox("Search column can have numeric values only.." & vbCrLf & _
            '==Now Manual Search=                           "   (max seven digits)..", MsgBoxStyle.Exclamation)
            '==Now Manual Search= System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '==Now Manual Search= Exit Sub
            '==Now Manual Search= End If
            '==Now Manual Search= Else
            '====     msCurrentSearch = msFixSqlStr(s1) '--eg O'Brien..--
            '==Now Manual Search= msCurrentSearch = s1
            '==Now Manual Search= If mbOrderAsc Then
            '==Now Manual Search= sArg = msCurrentOrder & ">='" & msCurrentSearch & "'"
            '==Now Manual Search= Else
            '==Now Manual Search= sArg = msCurrentOrder & "<='" & msCurrentSearch & "'"
            '==Now Manual Search= End If
            '==Now Manual Search= End If '--numeric--

            '--  miCurrentOrderColNo is current column..-
            '--- mbOrderAsc  (true or false) shows ASC/DESC currently...--

            '--grh--=14-Sep-2009== SORT- always reload from DB-table.
            '--grh--=14-Sep-2009== SORT- always reload from DB-table.

            msCurrentSearch = s1
            iArgLen = Len(msCurrentSearch)
            selRow = -1

            '--  AUTO SORTING..  RecordSet is now UNSORTED..-
            '--  DO manual FIND here..--

            '===       mRst.MoveFirst()
            '===       mRst.Find(sArg) '--position to find arg--
            '===       If mRst.EOF Then mRst.MoveLast()
            '===       If mRst.AbsolutePosition > 1 Then '--  AbsolutePosition is [1..noRows]..--
            '==                     MSHFlexgrid1.TopRow = mRst.AbsolutePosition - 1 '--move grid--
            '===          rowPos = mRst.AbsolutePosition - 1 '--move grid--
            '===       Else
            '==                       MSHFlexgrid1.TopRow = mRst.AbsolutePosition '--move grid--
            '===          rowPos = mRst.AbsolutePosition '--move grid--
            '===       End If

            colPos = miCurrentOrderColNo
            '== rowPos = 0  '-- in case --
            rowPos = -1  '-- in case out of bounds--
            '-- M a n u a l Search --
            '-- M a n u a l Search --
            '== If DataGridView1.Rows.Count > 2 Then
            For rx = 0 To (DataGridView1.Rows.Count - 1)
                If gbIsNumericType(sType) Then
                    '--  compare complete args..--
                    sSample = DataGridView1.Rows(rx).Cells(colPos).Value.ToString()
                    If (IsNumeric(sSample) And IsNumeric(msCurrentSearch)) Then
                        double1 = CDbl(sSample)
                        double2 = CDbl(msCurrentSearch)
                        If mbOrderAsc Then '= (DataGridView1.SortOrder = SortOrder.Ascending) Then
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
                Else  '--alpha- 
                    sSample = VB.Left(DataGridView1.Rows(rx).Cells(colPos).Value.ToString(), iArgLen)
                    intResult = System.String.Compare(sSample, msCurrentSearch, True)
                    If mbOrderAsc Then '=  (DataGridView1.SortOrder = SortOrder.Ascending) Then
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
                End If  '-numeric
            Next  '--rx..-
            '== End If  '--row count..-

            '-- In case out of bounds--
            If (rowPos < 0) Then
                If mbOrderAsc Then '=  (DataGridView1.SortOrder = SortOrder.Ascending) Then
                    rowPos = DataGridView1.Rows.Count - 1
                Else  '-desc..
                    rowPos = 0    '--set on 1st row..
                End If
            End If

            '--  set new visible pos..-
            DataGridView1.FirstDisplayedCell = DataGridView1.Rows(rowPos).Cells(intOrigCol)  '--keep orig-col on show..-
            '== DataGridView1.CurrentCell = DataGridView1.Rows(rowPos).Cells(0)
            DataGridView1.CurrentCell = DataGridView1.Rows(rowPos).Cells(miCurrentOrderColNo)

            '-- enable OK if match..-
            '== sGrid = VB.Left(DataGridView1.Rows(rowPos).Cells(colPos).Value, iArgLen)
            '= If (intResult = 0) Then 
            cmdOk.Enabled = True

            If LCase(VB.Left(sGrid, iArgLen)) = LCase(msCurrentSearch) Then cmdOk.Focus() '=  .Enabled = True
            mlFindTimer = 0 '--restart timer.-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '===   End If   '--not empty-
            '=== End If  '--nothing--
        End If '--txtFind.-
        '--position grid to row--
        Exit Sub

txtFind_Error:
        sMsg = "Runtime error in frmBrowse DatagridView txtFind_change method:" & vbCrLf & _
               vbCrLf & vbCrLf & "ERROR: " & Str(Err.Number) & "==" & Err.Description & vbCrLf & "..."
        MsgBox(sMsg, MsgBoxStyle.Exclamation)
        If (gsErrorLogPath() <> "") Then
            If Not gbLogMsg(gsErrorLogPath, sMsg) Then MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
        End If '--log--
    End Sub '--txtFind change--
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  select col-headers--
    '-- set new sort column--
    '== Private Sub MSHFlexgrid1_MouseUpEvent(ByVal eventSender As System.Object, _
    '==                                        ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseUpEvent)
    '== Dim lRow, lCol As Integer
    '== Dim sName As String
    '== '==Dim i, j, k As Long

    '==     If eventArgs.button = 1 Then '--left --
    '==         lRow = MSHFlexgrid1.MouseRow
    '==         lCol = MSHFlexgrid1.MouseCol
    '==         If (lRow > 0) And (MSHFlexgrid1.Rows > 1) Then '--NOT header row--
    '==             cmdOk.Enabled = True
    '==         ElseIf lRow = 0 And (MSHFlexgrid1.Rows > 1) Then  '--in header row--
    '== '--MsgBox "Left click on col :" & lCol
    '==             sName = Trim(MSHFlexgrid1.get_TextMatrix(0, lCol)) '--get new column name--
    '== '--If UCase$(sName) <> UCase$(msCurrentOrder) Then '--different col-
    '== '--because we do reload.. NEED TO  check if column is indexed--
    '==             Call mbSortColumn(sName)
    '==          End If '--row 0--
    '==     End If '--left--
    '== End Sub '--mouse up--
    '= = = = = = = = = =

    '--  AUTO SORT-- This event still happens..--

    '=== Private Sub dataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, _
    '===                                                  ByVal EventArgs As DataGridViewCellMouseEventArgs) _
    '===                                                     Handles DataGridView1.ColumnHeaderMouseClick
    '=== Dim lRow, lCol As Integer
    '=== Dim sName As String
    '=== '== MsgBox("clicked on a header..", MsgBoxStyle.Information)

    '=== '== If EventArgs.Button = 1 Then '--left --
    '===     lCol = EventArgs.ColumnIndex
    '=== '==lRow = EventArgs.RowIndex
    '===     sName = Trim(DataGridView1.Columns(lCol).HeaderCell.Value)
    '=== '== MsgBox("clicked on hdr: " & sName, MsgBoxStyle.Information)

    '=== '== Call mbSortColumn(sName)
    '=== '== End If  '--left.-

    '=== End Sub  '--header click.-
    '= = = = = = =  = = = = =

    '  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridView1_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridView1.Sorted

        Dim sName As String
        '-- get new sort column..--
        If Not mbStartupDone Then Exit Sub

        Dim currentColumn As DataGridViewColumn = DataGridView1.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mbSortColumn(sName)

        '==  Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.CurrentCell

    End Sub
    '= = = = = = = = =  = = =
    '-===FF->

    '=-3403.711=
    '- row enter-.-

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, _
                            ByVal ev As DataGridViewCellEventArgs) _
                                     Handles DataGridView1.RowEnter
        Dim intRowIndex = ev.RowIndex
        If (intRowIndex >= 0) And (DataGridView1.Rows.Count > 0) Then  '- case grid is loading.-
            If cmdEdit.Visible Then cmdEdit.Enabled = True
            cmdOk.Enabled = True
        End If

    End Sub  '- - row enter-
    '= = = = = = = = = = =

    '-- cell click.--
    '-- cell click.--

    Private Sub DataGridView1_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseClick
        Dim lRow, lCol As Integer
        Dim sName As String

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (DataGridView1.Rows.Count > 0) Then  '--selected a row.--
                If cmdEdit.Visible Then cmdEdit.Enabled = True
                cmdOk.Enabled = True
            End If

        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    '== Private Sub MSHFlexgrid1_dblClick(ByVal eventSender As System.Object, _
    '==                                        ByVal eventArgs As System.EventArgs)
    Private Sub DataGridView1_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseDoubleClick
        Dim lRow, lCol As Integer

        lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow < 0) Then '--in header row--
        Else
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            Call mSelectRecord(lRow)
            If mbLookupSelection Then  '--caller wants selection result-
                Call cmdExit_Click()
            Else  '-admin-
                '-- edit item..-
                Call mbEditRecord(lRow)
            End If  '--lookup-
        End If '--row--

    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    Private Sub cmdEdit_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles cmdEdit.Click
        Dim lRow As Integer
        '== Dim frmEdit1 As frmEdit

        lRow = DataGridView1.CurrentCell.RowIndex   '==  MSHFlexgrid1.Row

        If (lRow >= 0) Then '--ok row--
            '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol

            '-- set up row collections..-
            Call mSelectRecord(lRow)
            Call mbEditRecord(lRow)
        End If '--row--

    End Sub  '--edit --
    '= = = = = = = = = 
    '-===FF->

    '--Add new record.
    '--Add new record.

    Private Sub cmdNew_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles cmdNew.Click
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--job tracking sql connenction..-
        frmEdit1.colTables = mColTables
        frmEdit1.DBname = msDBname
        frmEdit1.tableName = msTableName '--"jobs"
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = True
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName
        frmEdit1.versionPOS = msVersionPOS

        '--- set WHERE condition for jobStatus..--
        frmEdit1.PreferredColumns = mColPreferredColumns
        frmEdit1.Title = "Add New " & msTableName & " record"

        frmEdit1.ShowDialog()

        frmEdit1.Close()
        '--get new r/set, load grid--
        If mbReload() Then

        End If

    End Sub  '--new--
    '= = = = = = = = = =
    '-===FF->

    '==
    '==   grh. Updated 3519.0322  22-March-2019-
    '==     -- Add full text search functions (Cloned from frmBrowse33..)
    '==

    '=3311.303-- Full Text Search..--

    Private Sub txtTextSearch_TextChanged(sender As Object, e As EventArgs) Handles txtTextSearch.TextChanged

    End Sub
    '= = = = = =

    '-- "ENTER" key on search text.-

    Private Sub txtTextSearch_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                    Handles txtTextSearch.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdTextSearch_Click(cmdTextSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub '-srch text enter-
    '= = = = = = = == = = = = = = 
    '-===FF->

    '-- Search..-

    Private Sub cmdTextSearch_Click(sender As Object, e As EventArgs) _
                                               Handles cmdTextSearch.Click
        Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-
        sWhere = msOriginalWhere  '-get initial condition if any -
        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        '--  now in the Interface..--
        asColumns = masSearchColumns  '= mRetailHost1.stockSearchColumns()

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtTextSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        '= Call mbBrowseStockTable(sWhere)
        '=3311.303=
        '-TEST-
        '= MsgBox("Srch Where Condition is : " & vbCrLf & sWhere)
        '= mBrowse3.WhereCondition = sWhere '--load search condition..--
        '= mBrowse3.refresh()

        msWhere = sWhere '--set up original + new ?
        '-Now to refresh=
        If mbReload() Then '--get new r/set, load grid--
            '=== mbOrderAsc = False
            '=== Call mbSortColumn(msCurrentOrder)  '--set up selected col. for ASC.-
            '= mbStartupDone = True '--1st time is done..-
            '= txtFind.Focus()
        Else '--no recs-
            MsgBox("No records..", MsgBoxStyle.Exclamation)
            '== Me.Hide()
            '== Exit Sub
        End If
        cmdNew.Enabled = True
        If DataGridView1.SelectedRows.Count > 0 Then
            cmdEdit.Enabled = True
        End If

        '-  added 3501.1103 - Nov2018-
        If (DataGridView1.Rows.Count > 0) Then  '0have roes..
            '-- select the top one after a search
            DataGridView1.Rows(0).Selected = True
            DataGridView1.Select()
        End If  '-count-

    End Sub  '-- Search-
    '= = = = = = = = = = = = = = == = =

    '- Clear-

    Private Sub cmdClearTextSearch_Click(sender As Object, e As EventArgs) _
                                                 Handles cmdClearTextSearch.Click
        txtTextSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdTextSearch_Click(cmdTextSearch, New System.EventArgs())

    End Sub
    '= = = = = = = = = = = = = = = = = =
    '-===FF->



    '= = = = = = = = = =
    '-===FF->


    '==  TO FIX == !!!  --
    '==  TO FIX == !!!  --
    '==  TO FIX == !!!  --


    '----key activity---  select row to edit--
    '----key activity---  select row to edit--

    '==TO FIX== Private Sub MSHFlexgrid1_KeyPressEvent(ByVal eventSender As System.Object, _
    '==TO FIX==                                ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent)

    '==TO FIX== Dim lRow As Integer = 1, lCol As Integer = 1

    '==  TO FIX ==   lRow = MSHFlexgrid1.Row
    '==  TO FIX == lCol = MSHFlexgrid1.Col

    '==TO FIX== '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
    '==TO FIX==     If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then
    '==TO FIX==         If lRow = 0 Then '--in header row--
    '==TO FIX==             Beep()
    '==TO FIX==         Else
    '==TO FIX== '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
    '==TO FIX==             Call mSelectRecord(lRow)
    '==TO FIX==             Call cmdExit_Click()
    '==TO FIX==         End If '--row--
    '==TO FIX==         eventArgs.keyAscii = 0 '--processed.-
    '==TO FIX==     End If '--enter--

    '==TO FIX== End Sub '--click--
    '= = = = = = = = = = = 
    '-===FF->


    '-- ok --
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        Dim lRow, lCol As Integer

        lRow = DataGridView1.CurrentCell.RowIndex   '==  MSHFlexgrid1.Row
        lCol = DataGridView1.CurrentCell.ColumnIndex   '==  MSHFlexgrid1.Col

        If (lRow >= 0) Then '--ok row--
            '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
            Call mSelectRecord(lRow)
            Call cmdExit_Click()
        End If '--row--

    End Sub '--ok --
    '= = = = = = = = =  =

    '--exit--

    Private Sub cmdExit_Click()

        If Not (mSqlRdr1 Is Nothing) Then
            On Error Resume Next
            '= If mRst.State = ADODB.ObjectStateEnum.adStateOpen Then mRst.Close()
            '--mRst.Close
            mSqlRdr1.Close()
            mSqlRdr1 = Nothing
        End If
        On Error GoTo 0
        mbClosingDown = True

        mbStartupDone = False '--force activate withot re-load--
        Me.Hide()

    End Sub '--exit--
    '= = = = = = = = =
    '= = = = =  =

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        mbCancelled = True
        Call cmdExit_Click()

    End Sub '--close--
    '= = = = =  =
    Private Sub mnuClose_Click()

        Call cmdExit_Click()

    End Sub '--close--
    '= = = = =  =


    '--= = = u n l o a d = = = = = = =
    Private Sub frmBrowse_FormClosing(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                   Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        '--MsgBox "frmMaint UNload event..'"  '-debug--
        '--If Not gbclosingDown Then
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                                 System.Windows.Forms.CloseReason.FormOwnerClosing  '==, vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                Call cmdExit_Click()
                '--If Not mbClosingDown Then
                '--  MsgBox "Please use Exit/Cancel buttons to exit form..", _
                ''--                                             vbInformation, "Maint."
                '--  intCancel = 1  '--cant close yet--
                '---End If
                intCancel = 0 '--let it go--
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--unload--

    '= = end Browse form= = =


End Class