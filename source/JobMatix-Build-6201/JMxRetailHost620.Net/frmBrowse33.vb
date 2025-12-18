Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class frmBrowse
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
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  14-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb 
    '==           (dropped sqlClient).. (For Jet OleDb driver).
    '==
    '==   >>  NOW calls clsBrowse3 for SQL retrieval and Grid loading..
    '== 
    '==  grh. JobMatix 3.1.3107.803-  03-Aug-2015 ===
    '==   >>  Now Using .net 4.5.2
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  grh.  3311.303- 03Mar2016-
    '==
    '==    >> New version frmBrowse33 to include Full text Search-
    '==               AND use clsBrowse33.vb (class is still "clsBrowse3")..
    '==
    '==     NB: Can be called for Jet Lookup (from claRetalHost)..
    '==     NB: Can be called for Jet Lookup (from claRetalHost)..
    '==
    '==  NEW BUILD-
    '==
    '==    >> 3431.0427=  27/28-April-2018..
    '==         -- FIX ALL FORMS to replace "msgbox"  with .Net "MessageBox"..
    '==         -- FIX ALL FORMS to MOVE "Activated" event stuff to "Shown" event..
    '== = = = = = = = = =
    '==
    '==  NEW BUILD-
    '==
    '== -- POS Updated 3501.1103  01/02/03-Nov-2018=  
    '==     -- Fixing Brower FORM.. Get latest frmBrowse33 (class frmBrowse) From JobTracking.
    '==            and update to catch ENTER key to Return currently selected row....
    '==
    '==
    '== -- Updated 3501.1106  06/08-Nov-2018=  
    '==     -- IMPORT latest browser form frmBrowse33 and  clsBrowse34 class from POS latest.
    '==                AND tidy up clsBrowse34 to work properly with JobTracking Aliased Pref.Cols.
    '==                   AND Fix frmBrowse33 to take account of Aliased flds for the Search-columns array.
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
      '--      -- ALSO- Update frmBrowse33 to accept User's Select-Sql..  
    '==            ALSO check that frmBrowse33 cancels out on ESCape pressed.
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= Const k_version As String = "frmBrowse33-Vers:=03-Mar-2016=12:00pm=="
	
	'--Const ki_maxChildButtons = 6
	Const K_FINDACTIVEBG As Integer = &HC0FFFF
	Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    Private mbIsInitialising As Boolean  '--  SEE myBase.New()  --
    Private mbActivated As Boolean = False

    Private mbStartupDone As Boolean = False
	Private mlFormDesignHeight As Integer
	Private mlFormDesignWidth As Integer '--save starting dimensions..-
	
	
    Private mColTables As Collection
    Private msDBname As String
    Private mCnnSql As OleDbConnection  '== ADODB.Connection

    Private mbIsSqlServer As Boolean

    Private mColPreferredColumns As Collection '--show these first..--
    Private mbShowPreferredColumnsOnly As Boolean = True   '--default..-
    Private mbNoFormCentering As Boolean = False
    Private msInitialFocusColumn As String = ""

    Private mColOriginalColumns As Collection
	
    '==3101= Private WithEvents mRst As ADODB.Recordset

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
	
    '=3311.303= Private masOrders() As String '--list of index names--
    Private msWhere As String = "" '-MUST START with Empty string. (NOT nothing)-
    '- -WHERE condition for current browse--
	
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
    '== Private mColChildTables As Collection
	'= = = = = = = = = = = = = =
    Private mbClosingDown As Boolean = False
    '=3311.303=  Private mlFetchComplete As Integer
    '=3311.303=  Private msFetchError As String
	Private mlRecCount As Integer '--ocunt of recs in rset..--
	
	Private mColKeyValues As Collection '--PKEYS of selected record-
	Private mColRowValues As Collection '--selected grid row-
    Private msTitle As String = ""
	'= = = = =
	
    Private mlGridLeft As Integer = 240 '== = VB6.PixelsToTwipsX(MSHFlexgrid1.Left)
    Private mlGridTop As Integer = 900  '==  = VB6.PixelsToTwipsY(MSHFlexgrid1.Top) '--save grid pos..--

	Private mColPrimaryKeyGridCols As New Collection '--saved by getRecordset..-
    Private mlFindTimer As Integer = -1 '--  settling time to drop CR from scanner..-
    '= = = = = = = = = = = = = = = =  = =

    '--   for datagrid--
    '==3101= Private mOleDbDataAdapter1 As OleDbDataAdapter
    '==3101= mDataSet1 As DataSet

    Private mCurrentGridColumn As DataGridViewColumn

    '==3101= Browser object-
    Private mBrowse3 As clsBrowse3 '== clsBrowse22  '== clsBrowseHost
    Private mLngSelectedRow As Integer = -1

    Private masSearchColumns() As String '--list of index names--

    '== 3519.0317=
    Private msUserSelectList As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = =

    '--properties as input parameters--
    '--properties as input parameters--
    WriteOnly Property connection() As OleDbConnection  '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)

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
			
			mcolPreferredColumns = Value
		End Set
	End Property '--prefs..-
	'= = = = =  =  = =  = =
	
	WriteOnly Property ShowPreferredColumnsOnly() As Boolean
		Set(ByVal Value As Boolean)
			
			mbShowPreferredColumnsOnly = Value
		End Set
	End Property '--preferred..=
	'= = = = = = = = = = = = =
	
	WriteOnly Property NoFormCentering() As Boolean
		Set(ByVal Value As Boolean)
			
			mbNoFormCentering = Value
		End Set
	End Property '--no center..-
	'= = = = = = = =  ==
	
	WriteOnly Property InitialFocusColumn() As String
		Set(ByVal Value As String)
            msInitialFocusColumn = Value
			
		End Set
	End Property '--initial focus..--
	'= = = = = = = = = = = = =
	
	'==  results.
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

    '== 3519.0317=
    '--  USER can provide SELECT list..-
    WriteOnly Property UserSelectList() As String
        Set(ByVal Value As String)

            msUserSelectList = Value
        End Set
    End Property '--select..-
    '= = = = = = = =  = = = =
    '= = = = = = = = = = = =
    '-===FF->

       '--l o a d STUFF  goes to InitializeComponent.. ===----
    '--l o a d----
    '--l o a d----
    '--l o a d STUFF  goes to InitializeComponent.. ===----
    '--activate--
    '--activate--
    '--activate stuff is now the  LOAD EVENT..--
    '--activate stuff is now the  LOAD EVENT..--

    '--  USER PROPERTIES have alreday been set  !!  ==
    '--  USER PROPERTIES have alreday been set  !!  ==
    '--  USER PROPERTIES have alreday been set  !!  ==

    Private Sub frmBrowse_Load(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim L1 As Integer
        '== Dim colTable As Collection
        '== Dim colTableInfo As Collection
        Dim idx, cx, tx, intPos As Integer
        Dim colP1 As Collection
        Dim s1, s2 As String
        Dim colFieldx As Collection

        If mbStartupDone Then Exit Sub

        cmdOk.Enabled = False

        '--  stuff from original LOAD..--
        mlFormDesignHeight = Me.Height '--save starting dimensions..-
        mlFormDesignWidth = Me.Width '--save starting dimensions..-

        mColKeyValues = New Collection
        mColRowValues = New Collection

        mlRecCount = 0
        mbOrderAsc = True '--default is ASC..-
        mbOrder2Asc = True
        mlFindTimer = -1 '-inactive.-

        '-- END stuff from original LOAD..--

        txtFind.Text = ""
        txtTextSearch.Text = ""
        s1 = "=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & ", Build: " & _
                                My.Application.Info.Version.Build & ", Rev: " & My.Application.Info.Version.Revision & "="
        If (msTitle = "") Then
            Me.Text = "Browsing Table:  " & msTableName & "  (JobMatix:  " & s1 & ")."
        Else
            Me.Text = msTitle & "  (JobMatix:  " & s1 & ")."
        End If

        If (msDBname = "") And mbIsSqlServer Then
            MsgBox("DB name empty in frmBrowse startup..", MsgBoxStyle.Critical)
            Me.Hide()
            Exit Sub
        End If
        If mColTables.Count() <= 0 Then
            MsgBox("Tables collection empty in frmBrowse startup..", MsgBoxStyle.Critical)
            '--Me.Hide
            Exit Sub
        End If
        If Not mbNoFormCentering Then Call CenterForm(Me)
        If Not gbDebug Then Labwhere.Visible = False

        '--set up stuff for current table--
        '-Set mColTable = mColTables(idx)
        mColTable = mColTables.Item(msTableName)
        '--Set mColTableInfo = mColTable(1)
        mColFields = mColTable.Item("FIELDS")
        mColPrimaryKeys = mColTable.Item("PRIMARYKEYS")
        mColOtherIndexes = mColTable.Item("OTHERINDEXES")
        '--msTableName = mColTable("TABLENAME")
         tx = 0 '--count tables in browse list--
        '--For idx = 1 To mColTables.count
        '--   Set colTable = mColTables(idx)

        '--cmdNew.Enabled = False
        '--cmdBrowse.Enabled = False
        txtFind.Enabled = False

        '--finish table setup/load rset etc==
        msCurrentOrder = ""
        txtFind.Text = ""
        tx = -1
        '--cboOrder.Clear
        '--cboOrder.Enabled = True
        txtFind.Enabled = True

        Dim sPrefFields As String = ""
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

        '== 3311.303=
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

    End Sub '--L o a d --
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--activate--
    '--activate--
    '--activate stuff is now the  LOAD EVENT..--
    '--activate stuff is now the  LOAD EVENT..--

    '--  USER PROPERTIES have alreday been set  !!  ==
    '--  USER PROPERTIES have alreday been set  !!  ==
    '--  USER PROPERTIES have alreday been set  !!  ==
    Private Sub frmBrowse_Activated(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '-activated-
    '= = = = = = = = = = = =  =

    '-3431.0428-Shown replaces Activated-

    Private Sub frmBrowse_Shown(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        '= If mbActivated Then Exit Sub
        '= mbActivated = True

        '--cmdBrowse.Enabled = True
        txtFind.Enabled = True
        '-- frame1.Caption = ""   '--" Table: '" + msTableName + "' "
        '= labTable.Text = msTitle
        labTable.Text = "Browsing: '" & msTableName & "' table."

        '== start Browser class--
        mBrowse3 = New clsBrowse3

        mBrowse3.connection = mCnnSql '--job tracking sql OR JET connection..-
        mBrowse3.colTables = mColTables
        mBrowse3.DBname = msDBname
        mBrowse3.tableName = msTableName  '== "jobs"
        mBrowse3.IsSqlServer = mbIsSqlServer  '= Is Jet if false.
        '== mBrowseJobs.FlexGrid = MSHFlexGridJobs
        mBrowse3.DataGrid = DataGridView1

        '--  pass controls..--
        mBrowse3.showRecCount = LabRecCount '--updates rec. retrieval..
        mBrowse3.showFind = LabFind '--updates Sort Column display..
        mBrowse3.showTextFind = txtFind '--updates Sort Column display..
        '==End If
        '--- set WHERE condition for jobStatus..--
        '= sWhere = sWhereCond
        mBrowse3.WhereCondition = msWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        mBrowse3.PreferredColumns = mColPreferredColumns
        mBrowse3.ShowPreferredColumnsOnly = mbShowPreferredColumnsOnly '= True
        '= FrameBrowse.Enabled = True
        DataGridView1.Enabled = True
        '= LabTitle.Text = msTitle
        '= mLngSelectedRow = -1
        '=3519.0317=
        mBrowse3.UserSelectList = msUserSelectList  '-- if offered..
        Try
            mBrowse3.Activate() '-- go..--
        Catch ex As Exception
            MsgBox("Error starting Browser class.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        mbStartupDone = True
        '=txtFind.Focus()
        txtTextSearch.Select()  '==3311.304-
    End Sub '-SHOWN- 
    '= = = = = = = = = = == = 
    '-===FF->

    '--  form resized..--
    'UPGRADE_WARNING: Event frmBrowse.Resize may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub frmBrowse_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        '--  cant make smaller than original..-
        If (Me.Height < mlFormDesignHeight) Then Me.Height = mlFormDesignHeight
        If (Me.Width < mlFormDesignWidth) Then Me.Width = mlFormDesignWidth
        '--resize results list box..--
        DataGridView1.Width = (Me.Width - 40)
        DataGridView1.Height = (Me.Height - DataGridView1.Top - 90)

        cmdOk.Top = (Me.Height - 80) '= DataGridView1.Top + DataGridView1.Height + 13
        cmdCancel.Top = cmdOk.Top
        LabRecCount.Top = cmdOk.Top
        Label2.Top = cmdOk.Top
        Labwhere.Top = (DataGridView1.Top + DataGridView1.Height + 6)
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
                DataGridView1.Select()   '--focus on grid.
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
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--txtFind..  TRAP CR and discard.. (For barcode scanner..)
    '--txtFind..  TRAP CR and discard.. (For barcode scanner..)

    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter..--
            If (mlFindTimer >= 0) Then '--  still settling--
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
                                         ByVal eventArgs As System.EventArgs) _
                                            Handles txtFind.TextChanged
        Dim sMsg As String

        If Not mbStartupDone Then Exit Sub

        Try
            Call mBrowse3.Find(txtFind)
            mlFindTimer = 0   '-start timer.
        Catch ex As Exception
            sMsg = "Runtime error in frmBrowse DatagridView txtFind_change method:" & vbCrLf & _
                   vbCrLf & vbCrLf & "ERROR: " & ex.Message & vbCrLf & "..."
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            If (gsErrorLogPath <> "") Then
                If Not gbLogMsg(gsErrorLogPath, sMsg) Then MsgBox("Error log failed..", MsgBoxStyle.Exclamation)
            End If '--log--

        End Try
    End Sub '--txtFind change--
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  select col-headers--
    '-- set new sort column--
    '  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridView1_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridView1.Sorted

        Dim sName As String
        '-- get new sort column..--
        If Not mbStartupDone Then Exit Sub

        Dim currentColumn As DataGridViewColumn = DataGridView1.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        '= Call mbSortColumn(sName)
        Call mBrowse3.SortColumn(sName)

    End Sub
    '= = = = = = = = =  = = =
    '-===FF->

    '=POS- 3501.1103- Catch Enter Key to Complete the Browse.
    '=POS- 3501.1103- Catch Enter Key to Complete the Browse.
    ' PreviewKeyDown is where you preview the key.
    ' Do not put any logic here, instead use the
    ' KeyDown event after setting IsInputKey to true.

    Private Sub DataGridView1_PreviewKeyDown(ByVal sender As Object, _
                                               ByVal e As PreviewKeyDownEventArgs) _
                                             Handles DataGridView1.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Enter, Keys.Escape, Keys.Up    '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =

    '--ACTUAL Data GridView Control keyDown --
    '--- check for Fx  etc-

    Private Sub DataGridView1_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        Dim intGridCol As Integer = DataGridView1.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        Dim intGridRow As Integer = DataGridView1.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (KeyCode = System.Windows.Forms.Keys.F5) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--F5.. go back to new barcode entry.--
            'If intGridCol = k_GRIDCOL_SERIALNOSREQUIRED Then
            '    '-- exit grid, back to new item..
            '    eventArgs.Handled = True
            '    txtStockItemBarcode.Select()
            'End If  '-gridcol-
        ElseIf (KeyCode = System.Windows.Forms.Keys.Enter) Then
            'If (intGridCol = k_GRIDCOL_SERIALNOSREQUIRED) Or _
            '                  (mbIsPurchaseOrder And (intGridCol = k_GRIDCOL_EXTENSION)) Then
            '    '-- exit grid, back to new item..
            '    eventArgs.Handled = True
            '    txtStockItemBarcode.Select()
            'End If  '-gridcol-
            Call cmdOk_Click(DataGridView1, New System.EventArgs())
            eventArgs.Handled = True
        End If  '--keycode-
    End Sub  '-dgvGoodsItems_KeyDown-
    '= = = = = = = =  = = == = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub DataGridView1_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseClick
        Dim lRow, lCol As Integer
        Dim sName As String
        '==Dim i, j, k As Long

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (DataGridView1.Rows.Count > 0) Then  '--selected a row.--

                cmdOk.Enabled = True
            End If

        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    Private Sub DataGridView1_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseDoubleClick
        Dim lRow, lCol As Integer

        lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If lRow < 0 Then '--in header row--
        Else
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            '== Call mSelectRecord(lRow)
            mLngSelectedRow = lRow
            Call mBrowse3.SelectRecord(mLngSelectedRow, mColKeyValues, mColRowValues)
            Call cmdExit_Click()
        End If '--row--

    End Sub '--click--
    '= = = = = = = = = = = = = = = = = =

    '-- selection changed-   Allow OK..

    Private Sub DataGridView1_SelectionChanged(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As EventArgs) _
                                                            Handles DataGridView1.SelectionChanged
        If (DataGridView1.SelectedRows.Count > 0) Then
            cmdOk.Enabled = True
        Else
            cmdOk.Enabled = False
        End If
    End Sub  '-- selection changed-
    '= = = =  = = = = = =
    '-===FF->

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
        sWhere = msWhere  '-get initial condition if any -
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
        mBrowse3.WhereCondition = sWhere '--load search condition..--
        mBrowse3.refresh()

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

    '-- ok --
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        Dim intRow, intCol As Integer
        '= lRow = DataGridView1.CurrentCell.RowIndex   '==  MSHFlexgrid1.Row
        '= lCol = DataGridView1.CurrentCell.ColumnIndex   '==  MSHFlexgrid1.Col
        '= re-done 3501.1103=
        intRow = -1
        If (DataGridView1.SelectedRows.Count > 0) Then
            intRow = DataGridView1.SelectedRows(0).Index
        End If

        If (intRow >= 0) Then '--ok row--
            '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
            '= Call mSelectRecord(lRow)
            mLngSelectedRow = intRow
            Call mBrowse3.SelectRecord(mLngSelectedRow, mColKeyValues, mColRowValues)

            Call cmdExit_Click()
        End If '--row--
    End Sub '--ok --
    '= = = = = = = = =  =

    '--exit--

    Private Sub cmdExit_Click()

        '== ?? == MSHFlexgrid1.Enabled = False
        '== ?? == MSHFlexgrid1.Visible = False
        mbClosingDown = True

        mbStartupDone = False '--force activate withot re-load--
        Me.Hide()

    End Sub '--exit--
    '= = = = = = = = =
    '= = = = =  =

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

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
        'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
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