Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Data
Imports System.Windows.Forms.Application
'== Imports system.data.sqlclient
Imports System.Data.OleDb
Imports System.Threading

Public Class frmEdit

    '-- Created for JoMatix POS..  24Mar2014..
    '--  Offspring of sqlExplore frmMaint2 (Edit Section).. 

    '--  Called from frmBrowse33 to edit selected row..

    '==  JobMatix POS---  24-Mar-2013=
    '==   >>  Using ADO.net 
    '==
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==  grh. JobMatix 3.1.3101.1207 ---  07-Dec-2014 ===
    '==   >>  Staff_id ans StaffName as input... 
    '==          and ADD special code for Customer Table.. 
    '==                (no update allowed for isAccountCust).
    '==                (and insert openedStaff_id for new cust.)
    '==
    '==   grh- JobMatix POS3 3.1.3103.0205-
    '==      >>  Fix Commit for Image parameter.. ( must be "? " )..
    '==
    '==
    '==   grh- JobMatix POS3 3.1.3103.0216-
    '==      >>  Fix Min dates....
    '==            '-DTPickerModel.MinDate = CDate("1/1/1753")   '--sql server min. date.
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >> Now for .Net 4.5.2- 
    '==
    '==     v3.3.3301.816..  16-August-2016= === (Now .Net 3.5--)
    '==       >> Fixing bug in Hiding form after error.- 
    '==             (Must call Me.DialogResult = DialogResult.Cancel BEFORE Hide..)
    '==       >> Input dll versionPOS..
    '==
    '==   Updated.-grh 3519.0310  Started 08-March-2019= 
    '==      >> Supplier Barcode now Auto-generated !!! (via Fixes to frmEdit)
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 
    '==
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020 +)
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (240-Nov-2020)
    '==
    '==
    '==   -- frmEdit..  Add Staff barcode to list of Autogen fields....
    '==            ALSO-  Update "date_modified" column when updating table that has it..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Const k_maxTextSize = 65500

    Private msVersionPOS As String = ""

    Private mbIsInitialising As Boolean  '--  SEE myBase.New()  --
    Private mbActivated As Boolean = False
    Private mbStartingUp As Boolean = True

    Private mbStartupDone As Boolean = False
    Private mlFormDesignHeight As Integer
    Private mlFormDesignWidth As Integer '--save starting dimensions..-

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private mColTables As Collection
    Private msDBname As String
    Private mCnnSql As OleDbConnection '= SqlConnection '-- ADODB.Connection
    '== Private mbIsSqlServer As Boolean

    Private mbNoFormCentering As Boolean = False
    '== Private msInitialFocusColumn As String = ""

    '==Private WithEvents mRst As ADODB.Recordset
    Private mSqlRdr1 As OleDbDataReader
    Private mDataSet1 As DataSet

    Private miTableIndexes() As Integer

    '--stuff from current browse table--

    '== Private msTableName As String
    Private miListIndex As Integer

    Private miCurrentTableIndex As Short

    Private mColTable As Collection '--current table--
    Private mColTableInfo As Collection
    Private mColFields As Collection '--fields for current table--
    Private mColPrimaryKeys As Collection
    Private mColForeignKeys As Collection
    Private mColOtherIndexes As Collection
    Private mColCurrentCols As Collection '--current RS/grid col names in order left->right--

    Private msWhere As String '--WHERE condition for current browse--

    Private miCurrentColCount As Short '--no of columns in current grid--
    Private msCurrentPrimaryKeyName As String = "" '--column name current oreder--

    Private mColPrimaryKeyValues As Collection '--for current record--
    Private mColChildTables As Collection
    '= = = = = = = = = = = = = =
    Private mbClosingDown As Boolean = False
 
    '= Private mColKeyValues As Collection '--PKEYS of selected record-
    '==Private mColRowValues As Collection '--selected grid row-
    Private msTitle As String
    '= = = = =

    Private mlGridLeft As Integer = 240
    Private mlGridTop As Integer = 900  '==  =--save grid pos..--

    Private mColPrimaryKeyGridCols As New Collection '--saved by getRecordset..-
    Private mlFindTimer As Integer = -1 '--  settling time to drop CR from scanner..-
    '= = = = = = = = = = = = = = = =  = =

    '-- to replace VB6 control array..
    Private mColTextControls As Collection
    '-- Each item is identified by ColumnName-
    '---    and is itself a collection of fldControl (ref), sqltype.
    '----- 
    '= = = = = = = = = = = = = = = = = = = = = = =

    '-- EDIT stuff ---
    '-- EDIT stuff ---
    '=Private mbNewRecord As Boolean = False
    Private mbNewRow As Boolean = False   '----new record being added--
    Private mbIsCustomerTable As Boolean = False   '----new AccountCust being added--
    Private mbIsAccountCustomer As Boolean = False   '---- AccountCust being added or updated--

    Private mColPreferredColumnsOriginal As Collection '--show these first..--
    Private mColPreferredColumns As Collection '--show these first..--
    Private mbShowPreferredColumnsOnly As Boolean = False

    '== Private mRstEdit As adodb.Recordset  '--WAS  mRst  !!  ====
    Private mDataTableEdit As DataTable  '--WAS  mRst  !!  ====

    '== Private mRsFn As adodb.Recordset     '--to read Fn descriptors--
    Private mDataTableFn As DataTable  '==adodb.Recordset     '--to read Fn descriptors--

    '--stuff for current edit table--
    '--Dim miListIndex As Long

    Private msEditTableName As String
    Private mColRecordKeyValues As Collection

    Private msBaseSelect As String
    Private msWhereThisRecord As String         '--WHERE clause for current record--
    Private mlFrameLeft, mlFrameTop As Integer  '---ref base for absolute pos0--
    Private miImageCount As Integer

    '= = = = = =
    Private mColEditFields As Collection    '--fields for current table (complete row)--
    '----Each fld is a collection (key=fldname) and has data items with keys:
    '------ TYPE, MAXSIZE,VALUE, FOREIGNTABLE, FOREIGNCOLUMN,JOINEDVALUE--
    '= = = = = = = = = = = = = =

    Private mlTxtBgColor As System.Drawing.Color   '== Integer
    Private miCurrentTxtIndex As Integer
    Private mbCompleted As Boolean   '--wait flag--
    Private mbModified As Boolean = False    '--row modified flag--
    Private mbUpdated As Boolean
    Private mbEditActive As Boolean   '----
 
    '--array of fld mod flages--
    Private mabFldModified() As Boolean  '--base of 1--  FIX !! -- NOW ZERO !!-
    Private mTxtPrimaryKey As TextBox    '-- ref to PKEY textbox.-

    Private mColRowImages As Collection
    '== Private mbDTPicker1_ValueChanged As Boolean = False

    Private mbCancelled As Boolean = False

    '==   Updated.-grh 3519.0310.. 10-March-2019= 
    '==      >> Supplier Barcode now Auto-generated !!! (via Fixes to frmEdit)
    '--  ONLY ONE AUTOGEN FLD allowed per table.

    '-if Not empty, holds fld Name to AutoGen (eg 'barcode')
    Private msAutoGenFieldName As String = ""  '-if Not empty, holds fld Name to AutoGen (eg 'barcode')
    Private msAutoGenFieldSqlType As String = ""
    Private mColAutoGenFieldx As Collection

    '-New barcode..
    Private msSelectedSupplierBarcode As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = =

    '-- end  EDIT stuff ---
    '-- end  EDIT stuff ---


    '--properties as input parameters--
    '--properties as input parameters--

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


    WriteOnly Property connection() As OleDbConnection  '= SqlConnection  '== ADODB.Connection
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
            msEditTableName = Value
        End Set
    End Property
    '-- - - -  - -

    '--  We only need primary key values..-

    '== WriteOnly Property WhereCondition() As String
    '==     Set(ByVal Value As String)
    '==         msWhere = Value
    '==         msWhereThisRecord = Value   '--POS--
    '==     End Set
    '== End Property
    '- - - - - -

    '--accept list of preferred col-names.---
    WriteOnly Property PreferredColumns() As Collection
        Set(ByVal Value As Collection)
            mColPreferredColumnsOriginal = Value
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

    '--  EDIT record..  Key collection.-
    WriteOnly Property selectedKey() As Collection
        Set(ByVal value As Collection)
            mColRecordKeyValues = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = = 

    '-- is new record to create..
    WriteOnly Property newRecord() As Boolean
        Set(ByVal value As Boolean)
            mbNewRow = value
        End Set
    End Property
    '= = = = = = = = = = = = = = =

    '==  results.
    '==  results.
    '==  results.
 
    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = =  = = = 

    '=3519.0311= 
    '-- - result of selection..
    ReadOnly Property selectedBarcode As String
        Get
            selectedBarcode = msSelectedSupplierBarcode
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    '==ReadOnly Property selectedKey() As Collection
    '==    Get
    '==        selectedKey = mColKeyValues
    '==    End Get
    '==End Property '-- get key--
    '= = = = = = = =  = =  ==

    '== SECOND result is collection of flds (name/value) of selected grid row--
    '== ReadOnly Property selectedRow() As Collection
    '==     Get
    '==         selectedRow = mColRowValues
    '==     End Get
    '== End Property '-- get key--
    '= = = = = = = =  = =  ==
    '= = = end properties = = = = = = = =
    '-===FF->

    '==   Updated.-grh 3519.0310  Started 08-March-2019= 
    '==      >> Supplier Barcode now Auto-generated !!! (via Fixes to frmEdit)
    '-- FOR now, this fn defines which fields.

    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
    '==   -- frmEdit..  Add Staff barcode to list of Autogen fields....

    Private Function mbIsAutoGenField(ByVal sTableName As String,
                                        ByVal sFieldName As String) As Boolean
        mbIsAutoGenField = False
        'If ((LCase(sTableName) = "customer") Or (LCase(sTableName) = "supplier") And
        '                                                   LCase(sFieldName) = "barcode") Then
        If ((LCase(sTableName) = "customer") Or (LCase(sTableName) = "supplier") Or
                         (LCase(sTableName) = "staff")) And (LCase(sFieldName) = "barcode") Then
            mbIsAutoGenField = True
        End If
    End Function '-=mbIsAutoGenField-
    '= = = = = = = = = = = = = == = = ==


    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '= = = = = = = = = = = =
    '-===FF->

    ''' <summary>
    ''' Converts the Image File to array of Bytes
    ''' Thanks to:
    '''     http://www.codeproject.com/Articles/31921/Convert-Image-File-to-Bytes-and-Back  
    ''' </summary>
    ''' <param name="ImageFilePath">The path of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function mabConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '= = = = = = = = = = = =
    '-===FF->
    '-- Browse Jobs or Parts table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    '--  colPrefs not used here..  we don't have these for Foreign-Key Tables.
    Private Function mbBrowseTable(ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                       ByVal sTableName As String, _
                                       Optional ByVal intBrowseTop As Integer = -1, _
                                        Optional ByVal intBrowseLeft As Integer = -1) As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColTables
        frmBrowse1.DBname = msDBname
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        '= frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        frmBrowse1.HideEditButtons = True   '-- this is jsut fkey Lookup..
        '-- set postion-
        If (intBrowseTop >= 0) Then frmBrowse1.MandatedFormTop = intBrowseTop
        If (intBrowseLeft >= 0) Then frmBrowse1.MandatedFormLeft = intBrowseLeft

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = =
    '-===FF->

    '-- Data Changed.--
    '--  Check if all completed..-
    Private Function msCheckRequiredFields() As String
        Dim colTextField As Collection
        Dim txtData As TextBox
        Dim DTPickerData As DateTimePicker
        Dim sFldName, sNameCol As String
        Dim sSqlType As String
        Dim sFieldsList As String = ""
        Dim colFieldx As Collection
        Dim bDisabledDate As Boolean = False

        msCheckRequiredFields = ""

        sNameCol = mColTables.Item(msEditTableName)("NAMECOLUMN") '--from orig. defs.-
        For Each colTextField In mColTextControls
            txtData = colTextField("txtData")
            sFldName = colTextField("name")
            sSqlType = colTextField("sqlType")
            colFieldx = mColEditFields(sFldName)
            If gbIsDate(sSqlType) Then
                DTPickerData = colFieldx("DTPickerData")
                If Not DTPickerData.Enabled Then
                    bDisabledDate = True
                End If
            End If
            '-- Note that datetime txt boxes are loaded only from datePicker-
            '----  and should always be valid if not empty..-
            '== If (Not colFieldx.Item("ISIDENTITY")) And (Not bDisabledDate) And _
            '==          (Not colFieldx.Item("ISIMAGECOLUMN")) Then  '--not mand.-
            '== If Trim(txtData.Text) = "" Then  '--incomplete..-
            '==    sFieldsList &= sFldName & "; "
            '== End If
            '= End If
            '=3519.0310-  bypass autoGen.
            If (msAutoGenFieldName <> "") AndAlso _
                           ((LCase(sFldName) = LCase(msAutoGenFieldName))) Then  '-- bypass-
                '-- is auto..  no check.
            Else '-check-
                '--  CHECK for mandatory fields-
                If (LCase(sFldName) = "barcode") Or (LCase(sFldName) = LCase(sNameCol)) Or _
                        colFieldx.Item("ISFOREIGNKEY") Or _
                        (colFieldx.Item("ISPRIMARYKEY") And (Not colFieldx.Item("ISIDENTITY"))) Then
                    If Trim(txtData.Text) = "" Then  '--incomplete..-
                        sFieldsList &= sFldName & "; "
                    End If
                End If
            End If  '-auto-
        Next colTextField
        txtUnfinished.Text = sFieldsList

        If sFieldsList = "" Then
            cmdCancel.Text = "Cancel"
            If Not mbNewRow Then
                cmdSave.Enabled = True
                cmdSaveExit.Enabled = False   '--"Exit"--
            Else
                cmdSaveExit.Enabled = True
            End If
        End If
        msCheckRequiredFields = sFieldsList

    End Function  '--required-
    '= = = = = = ='= = = = = = = =

    '-- Data Changed.--
    '--  Check if all completed..-
    Private Function mbDataChanged(ByVal intIndex As Integer) As Boolean
        mbModified = True
        mabFldModified(intIndex) = True

        Call msCheckRequiredFields()

    End Function  '--changed-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--Edit routines --- Ex vb6 sqlExplore - Upgraded here..
    '--Edit routines ---
    '--Edit routines ---

    '--Build WHERE condition for current record--
    '--build update/insert SET list of Flds/Values--
    Private Function msMakeWhereCondition() As String
        Dim i, j, k, fx As Integer
        Dim s1 As String, s2 As String
        Dim colFx As Collection
        Dim sFldName As String
        Dim sSqlType As String
        s1 = ""
        If mColPrimaryKeys.Count > 0 Then
            For i = 1 To mColPrimaryKeys.Count
                If (i > 1) Then s1 = s1 + " and "
                sFldName = UCase$(mColPrimaryKeys(i))
                '--get sql data type--
                colFx = mColFields(sFldName)
                sSqlType = colFx("TYPE_NAME")
                s2 = msFixSqlStr(mColRecordKeyValues(i))
                '--NOTE !!  don't use single for NUMERIC flds..----
                If gbIsText(sSqlType) Or gbIsDate(sSqlType) Then s2 = "'" + s2 + "'"
                s1 = s1 + " ([" + msEditTableName + "]." + sFldName + "=" & s2 + ") "
            Next i
        End If
        msMakeWhereCondition = s1

    End Function  '--where--
    '= = = = = = = = = = = =
    '-===FF->

    '--build update/insert SET list of Flds/Values--
    Private Function mbMakeSetList(ByRef imageParameters() As OleDbParameter, _
                                     ByRef strSetValues As String) As Boolean
        Dim i, j, k, fx As Integer
        Dim s1 As String, s2 As String
        '==Dim colFldx As Collection
        Dim sFldName As String
        Dim colMainFieldStuff As Collection  '--From original input coll.-
        Dim sSqlType, sDefaultValue As String
        '--Dim lngActualSize As Long
        Dim lngMaxsize As Integer
        Dim sUpdate As String, sFldList As String
        Dim sValueList As String
        Dim sFldData As String
        Dim txtData As TextBox
        Dim DTPickerData As DateTimePicker
        Dim parameter1 As OleDbParameter
        Dim bIsIdentity, bIsAutoGen As Boolean
        Dim bDisabledDate As Boolean = False

        s2 = ""
        mbMakeSetList = False
        fx = 0
        sUpdate = "" : sFldList = "" : sValueList = ""
        Try
            For Each sFldName In mColPreferredColumns  '== fx = 1 To mColFields.Count
                '==  sFldName = mColEditFields.Item(fx)("NAME")
                '--long text flds are limired BY US to 64k bytes--
                '--lngActualSize = mRst.Fields(sFldName).ActualSize
                '-- get orig fld defs..-
                colMainFieldStuff = mColFields(sFldName)
                fx += 1
                txtData = mColEditFields.Item(sFldName)("txtData")   '--get ref to textbox.
                sSqlType = UCase$(mColEditFields.Item(sFldName)("TYPE_NAME"))
                lngMaxsize = mColEditFields.Item(sFldName)("MAXSIZE")
                bIsIdentity = False
                If mColEditFields.Item(sFldName)("ISIDENTITY") Then
                    bIsIdentity = True
                End If
                '=3519.0310-
                bIsAutoGen = False
                If (msAutoGenFieldName <> "") AndAlso (LCase(msAutoGenFieldName) = LCase(sFldName)) Then
                    bIsAutoGen = True
                End If
                sDefaultValue = colMainFieldStuff("COLUMN_DEFAULT")
                '--bypass date fields with DEFAULT.--(ie DateCreated).
                If gbIsDate(sSqlType) Then
                    DTPickerData = mColEditFields.Item(sFldName)("DTPickerData")
                    If Not DTPickerData.Enabled Then
                        bDisabledDate = True
                    End If
                End If
                If gbIsDate(sSqlType) And bDisabledDate Then  '--date_modified etc..-
                    '-- don't write out this fld.
                Else '--ok.. can do this field
                    '--Check if data modified--
                    If mabFldModified(fx - 1) Or mbNewRow Then
                        If (UCase(sSqlType) = "IMAGE") Or _
                             (UCase(sSqlType) = "BINARY") Or (UCase(sSqlType) = "VARBINARY") Then
                            '--can't use strings.--
                            If mColRowImages.Contains(sFldName) Then
                                '-- add column as parameter-
                                If Not mbNewRow Then  '-update-
                                    If sUpdate <> "" Then
                                        sUpdate = sUpdate & ", "
                                    End If
                                    sUpdate = sUpdate & sFldName & "= ?"  '= & sFldName
                                Else  '--insert-
                                    If (sFldList <> "") Then
                                        sFldList = sFldList & ", "
                                        sValueList = sValueList + ", "
                                    End If
                                    sFldList = sFldList & sFldName
                                    sValueList = sValueList & " ? "  '= & sFldName
                                End If
                                '-- BUILD SQL cmd parameter for image byte[]...
                                parameter1 = New OleDbParameter("@" & sFldName, SqlDbType.VarBinary)
                                parameter1.Value = mColRowImages(sFldName)
                                k = imageParameters.Length + 1
                                ReDim Preserve imageParameters(k - 1)
                                imageParameters(k - 1) = parameter1
                            End If
                        Else  '--not IMAGE-
                            If gbIsDate(sSqlType) Then
                                sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                            ElseIf gbIsText(sSqlType) Then
                                sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                            Else  '--numeric-
                                sFldData = msFixSqlStr(txtData.Text)
                            End If
                            If Not mbNewRow Then  '--update--
                                If Not mColEditFields.Item(sFldName)("ISPRIMARYKEY") Then  '--can't update PKEY..-
                                    If sUpdate <> "" Then
                                        sUpdate = sUpdate & ", "
                                    End If
                                    sUpdate = sUpdate + sFldName + "=" + sFldData
                                End If
                            Else   '--INSERT--  NEW--
                                If (txtData.Text <> "") Or bIsAutoGen Then  '--don't include if no text-(except for AutoGen)
                                    '--don't include ident flds-- 
                                    If (Not bIsIdentity) And (Not (InStr(1, UCase$(sSqlType), "IDENTITY") > 0)) Then
                                        If sFldList <> "" Then
                                            sFldList = sFldList + ", "
                                            sValueList = sValueList + ", "
                                        End If
                                        sFldList &= sFldName
                                        If bIsAutoGen Then  '--add PlaceHolder for new AutoGen fld data.
                                            If gbIsNumericType(sSqlType) Then
                                                sValueList &= "%%%Auto_" & LCase(msAutoGenFieldName)
                                            Else  '-not numeric-
                                                sValueList &= "'%%%Auto_" & LCase(msAutoGenFieldName) & "'"
                                            End If
                                        Else '-- normal-
                                            sValueList &= sFldData
                                        End If
                                    End If  '--ID-
                                End If  '--empty-
                            End If  '--new-
                        End If  '--image-
                    End If  '--modified--
                End If '--default date.-
            Next sFldName  '= fx
            If Not mbNewRow Then  '--update--
                strSetValues = sUpdate
            Else
                If sFldList <> "" Then '--something--
                    strSetValues = "(" + sFldList + ")  VALUES (" + sValueList + ")"
                End If
            End If
            mbMakeSetList = True
        Catch ex As Exception
            mbMakeSetList = False
            MsgBox("Error in mbMakeSetList.." & vbCrLf & "Current Field: " & sFldName & vbCrLf & _
                                ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function  '--make list--
    '= = = = =  = = =  = =
    '-===FF->

    '---  Get Foreign Descr fld---  ( for Jet)---
    '---  Get Foreign Descr fld---

    '== No Jet= Private Function msGetForeignDescriptor(ByVal fx As Integer, _
    '== No Jet=                                          ByVal vKeyValue As Object) As String
    '== No Jet= Dim i, j, k As Integer
    '== No Jet= Dim s1, s2, sSql As String
    '== No Jet= Dim colFx As Collection
    '== No Jet= Dim sKeyFldName As String, sTable As String
    '== No Jet= Dim sSqlType As String, sResult As String
    '== No Jet= Dim sDisplayFld As String

    '== No Jet= msGetForeignDescriptor = ""
    '== No Jet= sResult = ""
    '== No Jet= '--get sql data type--
    '== No Jet= sSqlType = mColFields(fx)("TYPE_NAME")   '--from original info col..-
    '== No Jet= colFx = mColEditFields(fx)    '--get info this fld-
    '== No Jet= sTable = colFx("FOREIGNTABLE")
    '== No Jet= sKeyFldName = colFx("FOREIGNKEYFIELD")
    '== No Jet= sDisplayFld = colFx("FOREIGNDISPLAYFIELD")
    '== No Jet= s2 = msFixSqlStr(CStr(vKeyValue))
    '== No Jet= '--NOTE !!  don't use single for NUMERIC flds..----
    '== No Jet= If gbIsText(sSqlType) Or gbIsDate(sSqlType) Then s2 = "'" + s2 + "'"
    '== No Jet= sSql = "SELECT " + sDisplayFld + " FROM " + sTable + " WHERE  ([" + sTable + "]." + sKeyFldName + "=" & s2 + ") "
    '== No Jet= If gbDebug Then MsgBox("Getting record set for:" + vbCrLf + msBaseSelect)
    '== No Jet= If Not gbGetRst(mCnnSql, mRsFn, sSql) Then
    '== No Jet= MsgBox("Failed to get Display Fld recordset..")
    '== No Jet= Exit Function
    '== No Jet= Else   '--ok--
    '== No Jet= End If
    '== No Jet=     s1 = ""
    '== No Jet= '--get descr record field--
    '== No Jet=     If Not (mRsFn.BOF And mRsFn.EOF) Then  '--ok--
    '== No Jet=         If mRsFn.BOF And (Not mRsFn.EOF) Then mRsFn.MoveFirst()
    '== No Jet= '--scan recordset as debig--
    '== No Jet= '--For fx = 0 To (mRst.Fields.Count - 1)
    '== No Jet=         If Not IsNull(mRsFn.Fields(sDisplayFld).Value) Then
    '== No Jet=             sResult = CStr(mRsFn.Fields(sDisplayFld).Value)
    '== No Jet=         End If
    '== No Jet= '--Next fx
    '== No Jet=         If gbDebug Then MsgBox("Fielddata is :" + vbCrLf + s1)
    '== No Jet=     End If  '--not bof-
    '== No Jet=     msGetForeignDescriptor = sResult
    '== No Jet= End Function  '--get Fn--
    '= = = = = = = = = = = =
    '-===FF->

    '--  == E D I T  ==
    '---     retrieve current record and fill edit form--
    '- - - - - -

    Private Function mbRefresh() As Boolean
        Dim i, j, k, cx, fx As Integer
        Dim s1 As String, s2, sData As String
        Dim colFldx As Collection
        Dim sColName As String, sFldName As String
        Dim sSqlType As String
        Dim lngActualSize As Integer
        Dim lngResult, lngOffset As Integer
        Dim sPath As String
        Dim sAlias As String, sDisplay As String  '--foreign descriptor--
        Dim v1 As Object
        Dim yBinaryData() As Byte
        Dim yB2() As Byte
        Dim row1 As DataRow
        Dim column1 As DataColumn
        Dim txtData As TextBox
        Dim chkData As CheckBox
        Dim labJoin As Label
        Dim DTPickerData As DateTimePicker
        '== Dim cmdData As Button
        Dim bIsImage, bIsYesNo As Boolean
        Dim image1 As Image

        '--debug--
        If gbDebug Then MsgBox("Getting record set for:" + vbCrLf + msBaseSelect)

        If Not gbGetDataTable(mCnnSql, mDataTableEdit, msBaseSelect & msWhereThisRecord) Then
            '= gbGetRst(mCnnSql, mRstEdit, msBaseSelect + msWhereThisRecord) Then
            MsgBox("Failed to get recordset..")
            mbRefresh = False '--Me.Hide
            Exit Function
        Else   '--ok--
            mbEditActive = False
        End If
        s1 = ""
        '--fill edit form with record fields--
        If (mDataTableEdit.Rows.Count > 0) Then '= Not (mRstEdit.BOF And mRstEdit.EOF) Then  '--ok, not empty--
            '== mRstEdit.MoveFirst()
            '--scan recordset as debug--
            fx = 0
            row1 = mDataTableEdit.Rows(0)
            For Each column1 In mDataTableEdit.Columns
                sColName = column1.ColumnName
                s2 = ""
                If mColEditFields.Contains(sColName) Then
                    sSqlType = UCase$(mColEditFields.Item(sColName)("TYPE_NAME"))
                    If Not IsDBNull(row1.Item(sColName)) Then
                        If (UCase$(sSqlType) = "IMAGE") Or (UCase(sSqlType) = "BINARY") Or (UCase(sSqlType) = "VARBINARY") Then
                            s2 = "IMAGE"
                        Else
                            s2 = CStr(row1.Item(sColName))
                        End If
                        s1 = s1 & sColName & "=" + s2 + vbCrLf
                        '==If Not (column1.DataType Is GetType(Byte)) Then
                        '=End If
                    Else
                        s1 = s1 & sColName & " is null" + vbCrLf
                    End If  '--null-
                End If
              Next  '--column--
            '== For fx = 0 To (mRstEdit.Fields.Count - 1)
            '== sColName = mRstEdit.Fields(fx).Name
            '== If Not IsNull(mRstEdit.Fields(fx).Value) Then
            '== s2 = ""
            '== If (mRstEdit.Fields(fx).Type <> adBinary) And _
            '==                  (mRstEdit.Fields(fx).Type <> adVarBinary) Then _
            '== s2 = CStr(mRstEdit.Fields(fx).Value)
            '== s1 = s1 + sColName + "=" + s2 + vbCrLf
            '== '--load textbox with value--
            '== Else
            '== s1 = s1 + mRstEdit.Fields(fx).Name + " is null" + vbCrLf
            '== '--clear textbox--
            '== End If '--null-
            '== Next fx

            '-- DEBUG -
            '=== MsgBox("Refresh Fielddata is :" + vbCrLf + s1)
            fx = 0
            mColRowImages = New Collection  '--save all images for the current row--
            '--  (Collection of  Byte arrays (key= fldName)

            '--now loop thru our collection and pick out r/set values by name--
            For Each sFldName In mColPreferredColumns  '= fx = 1 To mColFields.Count
                s2 = "" : sAlias = "" : sDisplay = ""
                sData = ""
                fx += 1
                cx = fx - 1  '--columns start fron zero--
                '== sFldName = mColEditFields.Item(fx)("NAME")
                '--long text flds are limited BY US to 64k bytes--
                txtData = mColEditFields.Item(sFldName)("txtData")   '--get ref to textbox.
                labJoin = mColEditFields.Item(sFldName)("labJoin")   '--get ref to textbox.
                sSqlType = UCase$(mColEditFields.Item(sFldName)("TYPE_NAME"))
                bIsImage = False
                bIsYesNo = False
                picImage1.Image = Nothing
                If (UCase$(sSqlType) = "IMAGE") Or (UCase(sSqlType) = "BINARY") Or (UCase(sSqlType) = "VARBINARY") Then
                    bIsImage = True
                ElseIf (UCase$(sSqlType) = "BIT") Then
                    bIsYesNo = True
                    chkData = mColEditFields.Item(sFldName)("chkData")
                End If
                If Not IsDBNull(row1.Item(sFldName)) Then
                    If ((sSqlType = "TEXT") Or (sSqlType = "NTEXT")) Then
                        sData = row1.Item(sFldName).ToString
                        lngActualSize = sData.Length   '== mRstEdit.Fields(sFldName).ActualSize
                        If (lngActualSize > k_maxTextSize) Then
                            MsgBox("Warning.. memo fld: '" & sFldName & "' is too long to edit..", vbExclamation)
                            '== txtData.Text = VB.Left(CStr(mRstEdit.Fields(sFldName).Value), 16)
                            txtData.Text = VB.Left(sData, 16)
                            txtData.Enabled = False
                        Else
                            txtData.Text = sData
                        End If
                    ElseIf bIsImage Then  '--save--
                        '== ReDim yBinaryData(0 To lngActualSize - 1)  '== As Byte
                        yBinaryData = row1.Item(sFldName) '==mRstEdit.Fields(sFldName).Value
                        '== '--NORTHWIND db photo has 78-byte header--
                        '== lngOffset = 0
                        '= If (UCase$(msDBname) = "NORTHWIND") Then lngOffset = 78
                        '== ReDim yB2(0 To (lngActualSize - lngOffset - 1))  '== As Byte
                        '== '--drop northwind header--  SLOW !! ====
                        '== For i = 0 To (lngActualSize - lngOffset - 1)
                        '== yB2(i) = yBinaryData(i + lngOffset)
                        '== Next i
                        Try
                            '--- load picture from byte array..-
                            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(yBinaryData)
                            image1 = System.Drawing.Image.FromStream(ms)
                            picImage1.Image = image1
                            ms.Close()
                        Catch ex As Exception
                            MsgBox("Failed to load image from table: " & msEditTableName & vbCrLf & _
                                              "Error: " & ex.Message)
                        End Try
                        '--  save the byte array in static image collection..
                        mColRowImages.Add(yBinaryData, sFldName)
                        txtData.ReadOnly = True  '--must use cmd button to change image..-
                    ElseIf gbIsDate(sSqlType) Then
                        DTPickerData = mColEditFields.Item(sFldName)("DTPickerData")   '--get ref to textbox.
                        '--load date-time picker- DTPickerData -
                        DTPickerData.Value = CDate(row1.Item(sFldName))
                        sData = row1.Item(sFldName).ToString
                        s2 = sData  '= CStr(mRstEdit.Fields(sFldName).Value)
                    ElseIf bIsYesNo Then  '== datacolumn is BOOLEAN-
                        sData = CStr(row1.Item(sFldName))
                        If row1.Item(sFldName) = True Then  '== IsNumeric(sData) AndAlso (CInt(sData) = 1) Then
                            chkData.Checked = True
                        Else
                            chkData.Checked = False
                        End If
                        s2 = sData  '= CStr(mRstEdit.Fields(sFldName).Value)
                    Else  '--regular stuff.--
                        sData = row1.Item(sFldName).ToString
                        s2 = sData  '= CStr(mRstEdit.Fields(sFldName).Value)
                    End If  '--binary--
                    '--get foreign descr fld if foreign key--
                    If mColEditFields.Item(sFldName)("ISFOREIGNKEY") Then  '--load display for joined data-
                        '== If mbIsSqlServer Then  ''-we have joined everything in one SELECT--
                        sAlias = mColEditFields.Item(sFldName)("FOREIGNDISPLAYALIAS")
                        If Not IsDBNull(row1.Item(sAlias)) Then  '=IsNull(mRstEdit.Fields(sAlias).Value) Then _
                            sDisplay = row1.Item(sAlias)  '== mRstEdit.Fields(sAlias).Value
                            '== NO Jet in POS !! --
                            '== Else  '--Jet..  we must read each Fn display col separately..  !!!!--
                            '==    sDisplay = msGetForeignDescriptor(fx, mRstEdit.Fields(sFldName).Value)
                            '== End If  '--Jet-
                        End If  '-null-
                    End If  '--foreign--
                Else  '--null-
                    txtData.Text = "(null)"
                End If
                '==lngActualSize = mRstEdit.Fields(sFldName).ActualSize
                txtData.Text = sData
                labJoin.Text = sDisplay
                '== LabJoin.ToolTipText = sDisplay
            Next sFldName  '= fx
        Else  '--empty-
            MsgBox("Error..  no data retrieved for this record.." + vbCrLf + _
                                                       msBaseSelect, vbCritical)
            mbEditActive = True
            '--mbClosing = True
            '--Unload Me
        End If  '--refresh--
        mbEditActive = True
        mbModified = False
        For i = 0 To UBound(mabFldModified)  '--fixed.-POS-
            mabFldModified(i) = False '--RE initialise--
        Next i
        mbRefresh = True
        cmdSave.Enabled = False
        cmdSaveExit.Enabled = False   '--record is clean--
        '==cmdCancel.Text = "Close"

    End Function  '--refresh--
    '= = = = = = = = = =
    '-===FF->

    '-- update database for this row--
    Private Function mbUpdateRecord() As Boolean
        Dim ix, fx As Integer
        Dim s1 As String, s2 As String
        '--Dim colFldx As Collection
        '--Dim sColName As String, sFldName As String
        '--Dim sSqlType As String
        '--Dim lngActualSize As Long
        '== Dim lngMaxsize, lngAffected, lngError As Integer
        Dim sUpdate As String
        '== Dim imageParameters() As SqlParameter
        Dim imageParameters() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
        Dim cmd1 As OleDbCommand

        s2 = ""
        mbUpdateRecord = False
        If mbMakeSetList(imageParameters, sUpdate) Then
            '--build  sql update statement--
            '---include only modified fields--
            '= sUpdate = msMakeSetList()  '--build fld/vale list--
            If (Len(sUpdate) > 0) Or (imageParameters.Length > 0) Then '--some to do--

                '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                '==   Update "date_modified" column when updating table that has it..
                '-- date updated..
                If mColFields.Contains("date_modified") Then
                    If (sUpdate <> "") Then
                        sUpdate = sUpdate & ", "
                    End If
                    sUpdate &= "date_modified= CURRENT_TIMESTAMP "
                End If  '-'-- date updated..-
                '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)


                sUpdate = "UPDATE [" & msEditTableName + "]  SET " & sUpdate & msWhereThisRecord
                '--                "  Where: " + msRecordKeyColumn + "='" + msRecordKeyValue + "'"
                Try
                    cmd1 = New OleDbCommand(sUpdate, mCnnSql)
                    If (imageParameters.Length > 0) Then
                        For ix = 0 To (imageParameters.Length - 1)
                            cmd1.Parameters.Add(imageParameters(ix))
                        Next
                    End If
                    cmd1.ExecuteNonQuery()
                    '-ok-
                    mbUpdateRecord = True

                Catch ex As Exception
                    MsgBox("Sql Error in UPDATE record: " & vbCrLf & "SQL: " &
                                sUpdate & vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
                End Try
            Else  '--no change--
                mbUpdateRecord = True
            End If  '--update--
            mbModified = False
            mbUpdated = True
        End If
        '--Call mbRefresh
        '--if ok-

    End Function  '--update-
    '= = = = = = = = = =
    '= = = = = = = = = = = =
    '-===FF->

    '--Add new record--
    '-- update database for this row--
    Private Function mbAddNewRecord() As Boolean
        Dim i, j, k, ix, fx As Integer
        Dim s1 As String, s2 As String
        '== Dim colFldx As Collection
        Dim sInsert, sSql, sInsert2 As String
        '== Dim lngAffected, lngError As Integer
        Dim sFldData, sFldName As String
        Dim imageParameters() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
        Dim cmd1 As OleDbCommand
        '=Dim sqlRdr1 As SqlDataReader
        Dim v2 As Object
        Dim intID, intNewRecordId As Integer
        Dim txtData As TextBox
        Dim bIsAutoGen As Boolean

        mbAddNewRecord = False
        If mbMakeSetList(imageParameters, sInsert) Then
            '=sInsert = msMakeSetList()  '--build fld/vaintid=cint(lue list--
            If (sInsert <> "") Then  '== And (imageParameters.Length > 0) Then '--something--

                '--insert new record--
                '==   Updated.-grh 3519.0310.. 10-March-2019= 

                '==  Supplier Barcode now Auto-generated !!! (via Fixes to frmEdit)
                '--  ONLY ONE AUTOGEN FLD allowed per table.
                '--  Stolen from frmCustomer (Admin)..
                '==3519.0310=  Separate operation if AutoGen barcode is included..
                bIsAutoGen = False
                If (msAutoGenFieldName <> "") Then  '--have an autoGen barcode..
                    bIsAutoGen = True
                    Dim sqlTransaction1 As OleDbTransaction
                    '= Dim myProcId As Integer
                    Dim txtAutoGen As TextBox = mColAutoGenFieldx.Item("txtData")
                    '= myProcId = Process.GetCurrentProcess.Id
                    '-- do autogen loop,  then INSERT..

                    Dim intCount As Integer = 5 '--retry times..-
                    '= Dim intAffected As Integer
                    Dim bCompletedOk As Boolean = False
                    Dim sSqlInsert As String
                    '= Dim sFields2, sValues2 As String
                    Dim dataTable1 As DataTable
                    '== sSqlInsert = "INSERT INTO [customer] (" + sFldList + ")  VALUES (" + sValueList + ");"
                    '==     -- 3501.0920-  Improve AUTO-barcode for NEW Customer...
                    '==     -- 3501.0920-  Improve AUTO-barcode for NEW Customer...
                    '- try insert/update x times
                    While (intCount > 0) And (Not bCompletedOk)
                        intCount -= 1
                        '== MsgBox("SQL Insert cmd is : " & vbCrLf & sSql, MsgBoxStyle.Information)
                        Try
                            '--  add next attempt at predicting ID, for barcode.. 

                            '--  Must be GLOBAL next IDENT..
                            sqlTransaction1 = mCnnSql.BeginTransaction
                            '-- dummy SELECT to Lock the Table with HINT..
                            sSql = "SELECT TOP (1) " & msAutoGenFieldName & " FROM dbo." & msEditTableName & _
                                                                                     " WITH (SERIALIZABLE) ;"
                            If Not gbGetDataTableEx(mCnnSql, dataTable1, sSql, sqlTransaction1) Then
                                '- was rolled back-
                                MsgBox("Sql Error in DUMMY SELECT " & msEditTableName & "  record: " & vbCrLf & _
                                       gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                                Exit While
                            End If
                            '- ok get last IDENT.
                            '--  Must be GLOBAL next IDENT..
                            sSql = "SELECT CAST(IDENT_CURRENT ('dbo." & msEditTableName & "') AS int) ;"
                            If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                                If intID >= 0 Then
                                    txtAutoGen.Text = Trim(CStr(intID + 1))
                                Else
                                    MsgBox("Couldn't get valid NEXT barcode value.", MsgBoxStyle.Exclamation)
                                    sqlTransaction1.Rollback()
                                    Exit While
                                End If
                            Else
                                MsgBox("Failed to retrieve CURRENT " & msEditTableName & " No..", MsgBoxStyle.Exclamation)
                                '-was rolled back-
                                Exit While
                            End If
                            '= sInsert2 = Replace(sInsert, "%%%Auto_" & LCase(msAutoGenFieldName), txtAutoGen.Text)
                            sInsert2 = sInsert
                            sInsert2 = Replace(sInsert2, "%%%Auto_" & LCase(msAutoGenFieldName), txtAutoGen.Text)
                            sSqlInsert = "INSERT INTO [" + msEditTableName + "] " + sInsert2
                            ''-- Now do the insert..
                            cmd1 = New OleDbCommand(sSqlInsert, mCnnSql, sqlTransaction1)
                            If (imageParameters.Length > 0) Then
                                For ix = 0 To (imageParameters.Length - 1)
                                    cmd1.Parameters.Add(imageParameters(ix))
                                Next
                            End If
                            Try
                                cmd1.ExecuteNonQuery()
                                '-was good.. should commit now to bed in new ids.
                                Try
                                    sqlTransaction1.Commit()
                                Catch ex As Exception
                                    MsgBox("Sql Error in COMMIT " & msEditTableName & " record.." & vbCrLf & _
                                             ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
                                    Exit While '= Sub
                                End Try
                                '-  Retrieve customer_id:  (IDENTITY of Customer record written.)-
                                '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.customer') AS int);"
                                sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                                If gbGetSqlScalarIntegerValue(mCnnSql, sSql, intID) Then
                                    intNewRecordId = intID
                                    '-- update invoice display later..-
                                Else
                                    '-- rollback was done..
                                    MsgBox("Failed to retrieve latest Record Id..", MsgBoxStyle.Exclamation)
                                    Exit While '= Sub
                                End If
                                '--worked-
                                bCompletedOk = True
                            Catch ex As Exception
                                sqlTransaction1.Rollback()
                                MsgBox("Sql Error in INSERT " & msEditTableName & " record: " & vbCrLf & _
                                              ex.Message & vbCrLf & vbCrLf & _
                                              "NB. AutoGen barcode must be UNIQUE and may have clashed.." & vbCrLf & _
                                                "We will Retry the Commit to overcome the problem." & vbCrLf & _
                                                "SQL Command was: " & vbCrLf & _
                                                   sSqlInsert & vbCrLf, MsgBoxStyle.Exclamation)
                                Thread.Sleep(1000)  '-msecs
                            End Try
                        Catch ex As Exception
                            sqlTransaction1.Rollback()
                            '-- Assume failed because Is was already in use as a barcode.
                            MsgBox("Error INSERTing " & msEditTableName & " record: " & vbCrLf & _
                                          ex.Message & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
                            Exit While
                        End Try  '--cmd 1=
                    End While  '-completed-
                    If Not bCompletedOk Then
                        Exit Function
                    Else '--added ok.
                        msSelectedSupplierBarcode = txtAutoGen.Text
                        mbCancelled = False
                        '- now back to caller with new barcode.
                        Me.Hide()
                        Exit Function
                    End If
                Else  '- no auto gen.
                    sInsert = "INSERT INTO [" + msEditTableName + "] " + sInsert
                    Try
                        cmd1 = New OleDbCommand(sInsert, mCnnSql)
                        If (imageParameters.Length > 0) Then
                            For ix = 0 To (imageParameters.Length - 1)
                                cmd1.Parameters.Add(imageParameters(ix))
                            Next
                        End If
                        cmd1.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox("Sql Error in INSERT record: " & vbCrLf & "SQL Command was: " & _
                                     sInsert & vbCrLf & ex.Message & vbCrLf & vbCrLf & _
                                     "Note that barcodes must be UNIQUE..", MsgBoxStyle.Exclamation)
                        Exit Function
                    End Try
                End If  '-auto/no auto
                '-- insert done.
                Try
                    '--insert done..-
                    mbAddNewRecord = True

                    '--get last IDENTITY (allocated order_id)..--
                    sFldName = mColPrimaryKeys(1)
                    If mColEditFields(sFldName)("ISIDENTITY") Then  '--need to get the autonumber-
                        sSql = "SELECT IDENT_CURRENT ('dbo." & msEditTableName & "')"
                        If gbGetSelectValue(mCnnSql, sSql, v2) AndAlso (CInt(v2) > 0) Then
                            intID = CInt(v2)
                            txtData = mColEditFields(sFldName)("txtData")  '--pont to PKEY textbox-
                            txtData.Text = CStr(intID)
                        End If
                    End If
                    '--Build record key collection for next refresh--
                    mColRecordKeyValues = New Collection
                    For Each sFldName In mColPreferredColumns  '== fx = 1 To mColFields.Count
                        If mColEditFields(sFldName)("ISPRIMARYKEY") Then '=== mColFields(fx)("ISPRIMARYKEY") Then
                            txtData = mColEditFields.Item(sFldName)("txtData")   '--get ref to textbox.
                            If mColEditFields(sFldName)("ISIDENTITY") Then
                                '--need to get the autonumber-
                                sFldData = CStr(intID) '= txtData(fx - 1).Text
                            Else  '--not autonumber-
                                sFldData = txtData.Text
                            End If
                            mColRecordKeyValues.Add(sFldData)
                            Exit For
                        End If
                    Next sFldName  '== fx
                    s1 = msMakeWhereCondition()
                    msWhereThisRecord = " WHERE " + s1
                    MsgBox("New record has been added to table:" + vbCrLf + "     '" + msEditTableName + "'")
                    mbNewRow = False
                Catch ex As Exception
                    MsgBox("Sql Error in getting INSERT record ID.. " & vbCrLf & "SQL Command was: " & _
                                 sInsert & vbCrLf & ex.Message & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
                End Try
            Else
                MsgBox("Nothing to create..", MsgBoxStyle.Exclamation)
            End If  '--have stuff--
        End If  '-make-
    End Function '--add-
    '= = = = = = = = = =
    '-===FF->

    '- edit-Activate--

    '--E d i t  A c t i v a t e--
    '--E d i t  A c t i v a t e--
    '--E d i t  A c t i v a t e--
    '-- a c t i v a t e  FROM Edit FORM ----
    '--build our local fld def collection--
    '---and control arrays for for edit screen--

    Private Sub Edit_Activate()

        Dim colTable As Collection
        Dim colTableInfo As Collection
        Dim colFieldx As Collection
        Dim colTx As Collection   '--scratch--
        Dim colFx As Collection   '--scratch--
        Dim i, j, k, idx, fx, tx, cx As Integer
        Dim lVpos, intNextVpos, lHpos, lVsep As Integer
        Dim lVBottom As Integer
        Dim lMaxSize, lMaxTxtWidth, intPrevTextHeight As Integer
        Dim lStartRhs, intRhsTop As Integer
        Dim lBoxWidth, lEditWidth As Integer
        Dim sName As String, sFldName As String
        Dim s1 As String, s2 As String, s3 As String
        Dim sJoin As String, sFKeyInfo As String
        Dim sFtableName As String, sTableAlias As String, sAlias As String
        Dim sSqlType, sDefaultValue As String
        Dim bIsYesNo As Boolean
        Dim bSingleColumn As Boolean = False
        Dim bKeyfld, bIsIdentity, bIsImage, bIsBigText As Boolean
        Dim bLeftColFull, bRightColFull As Boolean
        Dim colTextField As Collection
        '-- Edit controls proxies..
        Dim txtData As TextBox
        Dim labFldCap, labJoin As Label
        Dim cmdData As Button  '--Edit--
        Dim DTPickerData As DateTimePicker
        Dim chkData As CheckBox

        mbEditActive = False
        '== ???  =    mbNewRow = False
        '==If (mColRecordKeyValues.Count <= 0) Then mbNewRow = True '--no key.. start new record--
        If msDBname = "" Then
            MsgBox("DB name empty in frmEdit startup..", vbCritical)
            '=====Me.Hide
            Exit Sub
        End If
        If mColTables.Count <= 0 Then
            MsgBox("Tables collection empty in frmEdit startup..", vbCritical)
            '======Me.Hide
            Exit Sub
        End If
        tx = 0 '--count tables in browse list--
        For idx = 1 To mColTables.Count
            colTable = mColTables(idx)
            '--extract table info for TABLE being edited--
            If UCase$(colTable.Item("TABLENAME")) = UCase$(msEditTableName) Then
                mColTable = mColTables(idx)
                '--Set mColTableInfo = colTable(1)
                mColFields = mColTable("FIELDS")
                mColPrimaryKeys = mColTable("PRIMARYKEYS")
                mColForeignKeys = mColTable("FOREIGNKEYS")
                mColOtherIndexes = mColTable("OTHERINDEXES")
                Exit For
            End If
            tx = tx + 1
            '--ReDim Preserve miTableIndexes(1 To tx)
            '--miTableIndexes(tx) = idx '--save list of browse table indexes--
        Next idx
        '--build edit fld list--
        mColTextControls = New Collection

        mColEditFields = New Collection
        mColRowImages = New Collection

        '-- USE   existing mColFields---
        '--USE  OTHERindexes to identify displyCol in foreign table !!--
        sJoin = ""
        '--build control array of text boxes--
        lVpos = txtModelData.Top   '= txtData(0).Top  
        intNextVpos = txtModelData.Top   '= txtData(0).Top  
        '== intTextHeight = txtModelData.Height   '== txtData(0).Height   '--LabFldCap(0).Height
        lVsep = ((txtModelData.Height \ 3) * 2) - 3  '--vertical gap--60%--
        '--lStartRhs = 0  '---LabFldCap(0).Left
        lBoxWidth = panelEdit.Width  '== frameEdit.Width  
        lVBottom = txtModelData.Top    '= txtData(0).Top     '--top of lowest txt box--
        bRightColFull = True '==  Box(0) no more--  False '---Box(0) will be already in place--
        intPrevTextHeight = 0
        '--lEditWidth = frmImage.Left - 200  '--usable width is at left of image column--
        lEditWidth = lBoxWidth - 60   '==400  '--usable full width if no image column--
        '== lStartRhs = LabFldCap(0).Left + (lEditWidth \ 2) + 100 '-now always the start of the RH col--
        lStartRhs = labModelCap.Left + (lEditWidth \ 2) + 8 '-now always the start of the RH col--
        msBaseSelect = "SELECT "
        '--count images --
        miImageCount = 0
        picImage1.Enabled = False
        '== picImage1.Visible = False
        For Each sFldName In mColPreferredColumns
            If mColFields.Contains(sFldName) Then
                sSqlType = mColFields.Item(sFldName)("TYPE_NAME")  '--sql native type--
                If (UCase$(sSqlType) = "IMAGE") Or _
                              (UCase(sSqlType) = "BINARY") Or (UCase(sSqlType) = "VARBINARY") Then
                    miImageCount = miImageCount + 1
                    picImage1.Enabled = True
                    '== picImage1.Visible = True
                End If
            End If
        Next  '--fld-
        intRhsTop = labModelCap.Top

        '-- always have pic frame visible..
        '== If (miImageCount > 0) Then  '--make space for image--
        intRhsTop = picImage1.Top + picImage1.Height + 7
        '== Else
        '== picImage1.Visible = False
        '= End If
        '--test-
        '= MsgBox("TEST msg- Setting up " & mColPreferredColumns.Count & " fields..", MsgBoxStyle.Information)
        '== If (mColPreferredColumns.Count <= 13) Then
        bSingleColumn = True
        '== End If

        mlTxtBgColor = txtModelData.BackColor  '== txtData(0).BackColor '--save normal backcolour--
        sFKeyInfo = "Foreign Key Tracking.." + vbCrLf '--debug--

        '--build local edit fld collection--
        '---Build textbox control array--
        '-- We are only ever interested in the "preferred"fields..-
        idx = 0
        For Each sFldName In mColPreferredColumns  '= idx = 1 To mColFields.Count
            '== sFldName = mColFields.Item(idx)("NAME")
            colFieldx = New Collection
            If mColFields.Contains(sFldName) Then
                idx += 1
                If (idx > 7) Then
                    bSingleColumn = False
                End If
                colFieldx.Add(mColFields.Item(sFldName)("TYPE"), "TYPE")
                colFieldx.Add(mColFields.Item(sFldName)("NAME"), "NAME")
                sSqlType = mColFields.Item(sFldName)("TYPE_NAME")  '--sql native type--
                colFieldx.Add(sSqlType, "TYPE_NAME")           '--we  need this too--
                lMaxSize = mColFields.Item(sFldName)("CHAR_MAX_LENGTH")
                If (lMaxSize = 0) Then
                    lMaxSize = 16 '--int,date etc--
                ElseIf (lMaxSize = -1) AndAlso gbIsText(sSqlType) Then  '--(max) relpalces ntext.
                    '-- (was fixed 3101.2107)--
                    lMaxSize = 64000
                End If
                sDefaultValue = mColFields.Item(sFldName)("COLUMN_DEFAULT")
                bIsImage = False
                bIsBigText = False
                bIsYesNo = False
                If (UCase$(sSqlType) = "IMAGE") Or (UCase(sSqlType) = "BINARY") Or (UCase(sSqlType) = "VARBINARY") Then
                    bIsImage = True
                ElseIf (UCase$(sSqlType) = "TEXT") Or (UCase$(sSqlType) = "NTEXT") Or _
                                             (gbIsText(sSqlType) And (lMaxSize > 255)) Then
                    bIsBigText = True
                ElseIf (UCase$(sSqlType) = "BIT") Then
                    bIsYesNo = True
                End If
                colFieldx.Add(bIsImage, "ISIMAGECOLUMN")           '--we  need this too--
                colFieldx.Add(bIsBigText, "ISBIGTEXT")           '--we  need this too--
                '--add to select field list--
                If (idx > 1) Then msBaseSelect = msBaseSelect & ", "
                msBaseSelect = msBaseSelect & "[" + msEditTableName & "]." + "[" & sFldName + "]"

                '--check for foreign key--
                '--On Error Resume Next
                '--s1 = mColFields.item(idx)("FOREIGNTABLE")
                '--If Err() = 0 Then '--yes-
                If mColFields.Item(sFldName)("ISFOREIGNKEY") Then
                    s1 = mColFields.Item(sFldName)("FOREIGNTABLE")
                    sFtableName = s1
                    sTableAlias = s1 & "_alias" & CStr(idx) '--in case self-reference same table--
                    colFieldx.Add(sFtableName, "FOREIGNTABLE")
                    s2 = mColFields.Item(sFldName)("FOREIGNFIELD")
                    colFieldx.Add(s2, "FOREIGNKEYFIELD")
                    '----GET display column name/size from foreign table defs--
                    colTx = mColTables(sFtableName)  '--access table referred to by foreign key--
                    colFx = colTx("FIELDS")
                    '--colFieldx.Add colTx("NAMECOLUMN"), "FOREIGNNAMECOLUMN"    '--remember descriptor col.for F/Table--
                    '--find key column and pick next char-type fld as display--
                    '--s3 = "":
                    k = 0 '--marker--
                    s3 = colTx("NAMECOLUMN")   '--get descr col if set--
                    sFKeyInfo = sFKeyInfo + "Checking Foreign key on col no. " & idx & sFldName + vbCrLf
                    For i = 1 To colFx.Count
                        If UCase$(colFx(i)("NAME")) = UCase$(s2) Then '--found--
                            k = i
                            Exit For
                        End If
                    Next i
                    If (k = 0) Or (s3 = "") Then
                        colFieldx.Add(False, "ISFOREIGNKEY")  '--no such key fld, or no display col.--
                        sFKeyInfo = sFKeyInfo + "  ==> Failed to add foreign key descr. for Table:" & _
                                                                                  sFtableName + "/" + s2 + vbCrLf
                    Else  '--ok--add to select statement--and add to JOIN clause--
                        '--build joined fld name for rs--  add to select field list--
                        colFieldx.Add(True, "ISFOREIGNKEY")     '--foreign key ref is valid--
                        colFieldx.Add(s3, "FOREIGNDISPLAYFIELD")
                        sAlias = sTableAlias + "_" + s3
                        colFieldx.Add(sAlias, "FOREIGNDISPLAYALIAS") '--use this to get from r/s--
                        '== If mbIsSqlServer Then
                        '== NO Jet !!-
                        msBaseSelect = msBaseSelect + ",[" + sTableAlias + "]." + s3 + _
                                        " AS [" + sAlias + "]"
                        '== End If  '--sql-
                        '--add join clause--
                        sJoin = sJoin + " LEFT JOIN [" + sFtableName + "] AS [" + sTableAlias + _
                                "] ON ([" + msEditTableName + "]." + sFldName + _
                                                         "= [" + sTableAlias + "]." + s2 + ")"
                        lMaxSize = 8 '-- !!! -- SHOULD be enough for foreign key  CHECK later !!!==
                    End If
                Else  '--not a foreign key--
                    colFieldx.Add(False, "ISFOREIGNKEY")
                    '--check if self referencing---ie refers to primary key of base table--
                    '---this can happen with references to parents--
                End If

                On Error GoTo 0
                colFieldx.Add(lMaxSize, "MAXSIZE")
                cx = idx - 1 '--control indexes start from 0--

                '== If idx = 1 Then '--first txt box exists..--
                '--  NO.. We need to create all..==

                '-- Create controls for this field..-
                labFldCap = New Label
                labFldCap.Font = labModelCap.Font
                labFldCap.BackColor = labModelCap.BackColor
                labFldCap.Name = "labFldCap_" & sFldName
                labFldCap.AutoSize = False
                labFldCap.Height = labModelCap.Height
                labFldCap.Width = labModelCap.Width
                '--use col name as caption--
                labFldCap.Text = Replace(sFldName, "_", " ")  '--(cx)- caption-

                txtData = New TextBox
                txtData.Font = txtModelData.Font
                txtData.BackColor = txtModelData.BackColor
                txtData.Name = "txtData_" & sFldName
                txtData.Height = txtModelData.Height
                If bIsBigText Then
                    txtData.Height = txtModelData.Height * 5
                    txtData.Multiline = True
                    txtData.ScrollBars = ScrollBars.Vertical
                End If
                txtData.Width = txtModelData.Width
                txtData.BorderStyle = txtModelData.BorderStyle
                txtData.Tag = CStr(idx - 1)   '--so we can retrieve as array index-
                AddHandler txtData.Enter, AddressOf txtData_Enter
                AddHandler txtData.TextChanged, AddressOf txtData_TextChanged
                AddHandler txtData.Validating, AddressOf txtData_Validating

                labJoin = New Label
                labJoin.Font = labModelJoin.Font
                labJoin.BackColor = labModelJoin.BackColor
                labJoin.Name = "labJoin_" & sFldName
                labJoin.AutoSize = False
                labJoin.Height = labModelJoin.Height
                labJoin.Width = labModelJoin.Width

                '--add a cmd button for each fld for possible memo-edit, picture browse, calendar etc.--
                cmdData = New Button
                cmdData.Font = cmdModelData.Font
                cmdData.BackColor = cmdModelData.BackColor
                cmdData.Name = "cmdData_" & sFldName
                cmdData.Width = cmdModelData.Width
                cmdData.Height = cmdModelData.Height
                cmdData.Text = cmdModelData.Text
                AddHandler cmdData.Click, AddressOf cmdData_Click

                DTPickerData = Nothing
                chkData = Nothing
                If gbIsDate(sSqlType) Then
                    '--  add DT picker--
                    DTPickerData = New DateTimePicker
                    DTPickerData.Name = "DTPickerData_" & sFldName
                    DTPickerData.Font = DTPickerModel.Font
                    DTPickerData.Width = 100
                    DTPickerData.Format = DateTimePickerFormat.Short
                    DTPickerData.Height = DTPickerModel.Height
                    DTPickerData.MinDate = DTPickerModel.MinDate
                    DTPickerData.MaxDate = DTPickerModel.MaxDate
                    DTPickerData.ShowUpDown = True
                    AddHandler DTPickerData.ValueChanged, AddressOf DTPickerData_ValueChanged
                ElseIf bIsYesNo Then
                    chkData = New CheckBox
                    chkData.Name = "chkData_" & sFldName
                    chkData.Text = labFldCap.Text
                    chkData.Font = chkModel.Font
                    chkData.Width = labFldCap.Width
                    chkData.Height = chkModel.Height
                    AddHandler chkData.CheckStateChanged, AddressOf chkData_CheckedChanged
                End If

                '-- Position new controls..-
                '--set box on next "line"--
                lVpos = intNextVpos
                '--move to rhs for 2nd half--
                If idx = 1 Then  '--1st fld- always alone on the line..
                    lHpos = labModelCap.Left  '= labFldCap(0).Left   '--use lhs--
                    intNextVpos = lVpos + txtData.Height + lVsep '--vpos next line--
                    bRightColFull = True   '--always alone..
                Else  '--not 1st-
                    '--FKEY starts new line-- Also long text (or Memo-bigText)
                    If colFieldx("ISFOREIGNKEY") Or bSingleColumn Then
                        If Not bRightColFull Then
                            lVpos += intPrevTextHeight + lVsep '--vpos next line--
                        End If
                        intNextVpos = lVpos + txtData.Height + lVsep '--set up next line--
                        lHpos = labModelCap.Left  '= labFldCap(0).Left
                        bRightColFull = True '---fills line..-
                    ElseIf bRightColFull Or (lVpos < intRhsTop) Then  '--don't write over image..
                        If Not bRightColFull Then
                            lVpos += intPrevTextHeight + lVsep '--vpos to next line--
                        End If
                        lHpos = labModelCap.Left  '= labFldCap(0).Left   '--use lhs--
                        bRightColFull = False      '--rhs will be vacant--
                        '== intNextVpos = lVpos + txtData.Height + lVsep '--set up next line--
                        '==ElseIf Not bLeftColFull Then
                        '==     lVpos = lVpos + lVsep '--vpos next line--
                        '==     lHpos = LabFldCap(0).Left
                    ElseIf Not bRightColFull Then  '--  use current rhs --
                        lHpos = lStartRhs
                        bRightColFull = True
                        intNextVpos = lVpos + intPrevTextHeight + lVsep '--set up next line--
                    Else  '--start at lvpos..-
                        bRightColFull = False
                    End If  '--fkey--
                End If
                intPrevTextHeight = txtData.Height
                labFldCap.Top = lVpos  '=labFldCap(cx).Top = lVpos
                txtData.Top = lVpos      '==  txtData(cx).Top = lVpos
                labJoin.Top = lVpos      '==  labJoin(cx).Top = lVpos
                cmdData.Top = lVpos - (cmdData.Height - txtData.Height) '==align bottoms-

                labFldCap.Left = lHpos  '== labFldCap(cx).Left = lHpos   '-- LabFldCap(0).Left + lStartRhs
                '== txtData(cx).Left = lHpos + labFldCap(cx).Width   '--- txtData(0).Left + lStartRhs
                txtData.Left = lHpos + labFldCap.Width + 3  '--- txtData(0).Left + lStartRhs
                '--labJoin(idx - 1).Left = labJoin(0).Left + lStartRhs
                '== End If   '--first--

                txtData.TabIndex = (idx - 1) * 4 + 11 '--start tabindex from 11--
                cmdData.TabIndex = (idx - 1) * 4 + 12 '--start tabindex from 11--
                If Not (DTPickerData Is Nothing) Then
                    DTPickerData.TabIndex = (idx - 1) * 4 + 13 '--start tabindex from 11--
                End If
                If Not (chkData Is Nothing) Then
                    chkData.TabIndex = (idx - 1) * 4 + 14 '--start tabindex from 11--
                End If
                '--C A U T I O N ----
                '----text and ntext flds can't go into a standard textbox--
                '----- needs multi-line edit box--
                If lMaxSize > k_maxTextSize Then
                    txtData.MaxLength = k_maxTextSize  '=(cx)-
                Else
                    txtData.MaxLength = lMaxSize   '-(cx)-- ie. here..  --
                End If
                '--load default value later if any..
                txtData.Text = "" '--(cx)-  "(" + CStr(idx) + ")" + "txtData"
                '--set width of edit box--
                lMaxTxtWidth = (lEditWidth \ 2) - (labFldCap.Width) - 20  '--(cx)-300
                labJoin.Visible = False
                cmdData.Visible = False
                If Not DTPickerData Is Nothing Then  '--exists-
                    DTPickerData.Visible = False
                End If
                If Not chkData Is Nothing Then  '--exists-
                    chkData.Visible = False
                End If
                '-- ok.. sort out the controls..
                '--  depending on data type..-
                If bIsImage Then  '==(UCase$(sSqlType) = "IMAGE") Then
                    txtData.Width = 10  '== txtData(cx).Width = 600
                    '--txtData(cx).BackColor = kl_TxtLockedColor   '--vbGrayText
                    ToolTip1.SetToolTip(cmdData, "Browse for new image file..")
                    cmdData.Visible = True
                    cmdData.Enabled = True
                    cmdData.Text = "Browse.."
                    cmdData.Left = txtData.Left + txtData.Width + 13
                    '== ElseIf (UCase$(sSqlType) = "TEXT") Or (UCase$(sSqlType) = "NTEXT") Or _
                    '=                               (gbIsText(sSqlType) And (lMaxSize > 255)) Then
                ElseIf bIsBigText Then
                    txtData.Width = lMaxTxtWidth '= (lMaxTxtWidth - cmdData.Width - 3)  '--cx-50
                    '== ToolTip1.SetToolTip(cmdData, "Edit memo text..")
                    cmdData.Visible = False '== True
                    '== cmdData.Enabled = True
                    '== cmdData.Left = txtData.Left + txtData.Width + 13
                ElseIf colFieldx("ISFOREIGNKEY") Then
                    txtData.Width = 100  '== txtData(cx).Width = 600
                    txtData.ReadOnly = True   '--must lookup..-
                    labJoin.Left = txtData.Left + txtData.Width + 3  '--(cx)-100
                    '--use remaining width for joined data--
                    '-- full width now ==  LabJoin(cx).Width = (lEditWidth \ 2) - (LabFldCap(cx).Width) - txtData(cx).Width - 300
                    k = lEditWidth
                    If (lVpos < intRhsTop) Or bSingleColumn Then  '==(miImageCount > 0) And =
                        k = k - picImage1.Width   '--don't overwrite image column..
                    End If
                    labJoin.Width = k - (labFldCap.Width) - txtData.Width - cmdData.Width - 60  '-(cx)-300-
                    ToolTip1.SetToolTip(cmdData, "Lookup..")
                    cmdData.Visible = True
                    cmdData.Enabled = True
                    cmdData.Text = "Lookup.."
                    cmdData.Left = txtData.Left + txtData.Width + labJoin.Width + 7
                    labJoin.Visible = True
                ElseIf gbIsDate(sSqlType) Then
                    txtData.Width = 100
                    '== cmdData.Left = txtData.Left + txtData.Width + 13
                    '== cmdData.Visible = True
                    DTPickerData.Visible = True
                    DTPickerData.Top = txtData.Top
                    DTPickerData.Left = txtData.Left + txtData.Width + 7
                    txtData.ReadOnly = True  '--must use date picker--
                    txtData.TabStop = False   '--must use DT picker.-
                    If (LCase(sFldName) = "date_modified") Or (LCase(sFldName) = "date_created") Or _
                                       (LCase(sFldName) = "date_updated") Or (LCase(sFldName) = "dateupdated") Then
                        '== cmdData.Enabled = False
                        '== cmdData.Text = ""
                        DTPickerData.Enabled = False
                    Else  '-not default now-
                        ToolTip1.SetToolTip(cmdData, "Change Date data..")
                        '= cmdData.Enabled = True
                        DTPickerData.Enabled = True
                    End If
                ElseIf bIsYesNo Then
                    txtData.Width = 40
                    chkData.Left = txtData.Left + txtData.Width + 7
                    chkData.Top = txtData.Top
                    chkData.Visible = True
                    txtData.ReadOnly = True   '--must lookup..-
                    txtData.TabStop = False   '--must use checkbox.-
                    '== chkData.Checked = False
                    If mbNewRow Then
                        txtData.Text = "0"   '-set default.--UNCHECKED-
                    End If
                ElseIf gbIsText(sSqlType) And (lMaxSize > 55) Then
                    '--not memo..  long textfield.
                    k = lEditWidth
                    If (lVpos < intRhsTop) Then  '==(miImageCount > 0) And =
                        k = k - picImage1.Width   '--don't overwrite image..
                    End If
                    txtData.Width = k - (labFldCap.Width) - 40
                Else  '--varchar, money etc..-
                    txtData.Width = lEditWidth - (labFldCap.Width) - 20
                    '==  txtData.Width = lMaxSize * 10  '== txtData(cx).Width = lMaxSize * 140
                End If
                '== If (txtData.Width < 40) Then txtData.Width = 40 '-(cx)-min size--600
                '--truncate if too long--
                If (labFldCap.Width + txtData.Width) > (lEditWidth \ 2) Then
                    txtData.Width = (lEditWidth \ 2) - (labFldCap.Width) - 13
                End If
                '--make capt/textbox visible--
                labFldCap.Visible = True  '--(cx)-
                txtData.Visible = True     '--(cx)-
                '== txtData.Locked = False      '--(cx)-
                txtData.BackColor = mlTxtBgColor   '---because base box (0) could have changed-
                '--If (UCase$(sSqlType) = "IMAGE") Then txtData(cx).Locked = True
                '--can't update primary key flds-  --or any IDENTITY field--
                '--If (UCase$(sFldName) = UCase$(msRecordKeyColumn)) Or
                bKeyfld = False
                If mColPrimaryKeys.Count > 0 Then
                    For i = 1 To mColPrimaryKeys.Count
                        If UCase$(sFldName) = UCase$(mColPrimaryKeys(i)) Then
                            bKeyfld = True
                        End If
                    Next i
                End If
                bIsIdentity = False
                If bKeyfld Then
                    '==If mbNewRow Then --
                    labFldCap.BackColor = Color.LavenderBlush '==&HFFC0FF '--vbColorMauve-??-
                    labFldCap.Text = "*" & labFldCap.Text
                    colFieldx.Add(True, "ISPRIMARYKEY")
                    mTxtPrimaryKey = txtData
                    bIsIdentity = mColFields.Item(sFldName)("ISIDENTITY")
                    '-- set up for refresh.
                    msWhereThisRecord = " WHERE (" & sFldName & "='&&&KEYVALUE'  )"
                    If gbIsNumericType(sSqlType) Then
                        msWhereThisRecord = " WHERE (" & sFldName & "=&&&KEYVALUE  )"
                    End If
                Else
                    colFieldx.Add(False, "ISPRIMARYKEY")
                End If
                '--lock image type and pkey (edit mode)--
                If (bKeyfld And (Not mbNewRow)) Or bIsImage Or bIsIdentity Or _
                                         (InStr(1, UCase$(sSqlType), "IDENTITY") > 0) Then
                    txtData.ReadOnly = True   '=  txtData(cx).Locked = True
                    txtData.TabStop = False
                    txtData.BackColor = Color.Gainsboro   '== kl_TxtLockedColor  '--vbGrayText '--  ??--
                End If
                If bIsIdentity Then
                    colFieldx.Add(True, "ISIDENTITY")
                Else
                    colFieldx.Add(False, "ISIDENTITY")
                End If

                colFieldx.Add(sDefaultValue, "COLUMN_DEFAULT")
                '== labJoin.Visible = True  '-(cx)-
                '== If Not colFieldx("ISFOREIGNKEY") Then
                '== labJoin.Visible = False '--no foreign data--
                '== End If
                '= cmdData.Left = labJoin.Left  '--(cx)-
                '== cmdData.Visible = False
                '== cmdData.Enabled = False
                '== If bIsImage Then
                '==End If
                '--add to local collection--
                '== ReDim Preserve mabFldModified(1 To cx + 1)
                '== mabFldModified(cx + 1) = False '--initialise--
                '--update lowest box--
                If (lVBottom < txtData.Top) Then lVBottom = txtData.Top '-(cx)-top of lowest txt box--

                '-- All done-
                '-- add textbox controls ref to our editing field info..
                colFieldx.Add(txtData, "txtData")
                colFieldx.Add(cmdData, "cmdData")
                colFieldx.Add(labJoin, "labJoin")
                If Not DTPickerData Is Nothing Then
                    colFieldx.Add(DTPickerData, "DTPickerData")
                End If
                If Not chkData Is Nothing Then
                    colFieldx.Add(chkData, "chkData")
                End If
                mColEditFields.Add(colFieldx, sFldName)

                '-- add controls to panel collection..
                panelEdit.Controls.Add(labFldCap)
                panelEdit.Controls.Add(txtData)
                panelEdit.Controls.Add(labJoin)
                panelEdit.Controls.Add(cmdData)
                If Not DTPickerData Is Nothing Then
                    panelEdit.Controls.Add(DTPickerData)
                End If
                If Not chkData Is Nothing Then
                    panelEdit.Controls.Add(chkData)
                End If

                '-- add the text control to our OWN control array.
                colTextField = New Collection
                colTextField.Add(txtData, "txtData")
                colTextField.Add(sFldName, "name")
                colTextField.Add(sSqlType, "sqltype")
                mColTextControls.Add(colTextField, sFldName)
                '==   Updated.-grh 3519.0310.. 10-March-2019= 
                '==      >> Supplier Barcode now Auto-generated !!! (via Fixes to frmEdit)
                '-- IF this the AutoGen field, Save it for New REcord.. 
                If mbIsAutoGenField(msEditTableName, sFldName) Then
                    msAutoGenFieldName = sFldName
                    msAutoGenFieldSqlType = sSqlType
                    mColAutoGenFieldx = colFieldx
                    txtData.ReadOnly = True   '=  txtData(cx).Locked = True
                    txtData.TabStop = False
                    txtData.BackColor = Color.Gainsboro   '== kl_TxtLockedColor  '--vbGrayText '--  ??--
                    labFldCap.BackColor = Color.LavenderBlush '==&HFFC0FF '--vbColorMauve-??-
                    labFldCap.Text = "*" & labFldCap.Text
                End If  '-autogen.
            End If  '--contains.-
        Next sFldName  '= idx  '--fld--

        ReDim mabFldModified(mColEditFields.Count - 1)
        '-- REFRESH will init array.--
        '== For i = 0 To UBound(mabFldModified)  '--fixed.-POS-
        '==    mabFldModified(i) = False '--RE initialise--
        '== Next i

        '-- DELETE Model controls..--
        panelEdit.Controls.Remove(labModelCap)
        panelEdit.Controls.Remove(txtModelData)
        panelEdit.Controls.Remove(labModelJoin)
        panelEdit.Controls.Remove(cmdModelData)
        panelEdit.Controls.Remove(DTPickerModel)
        panelEdit.Controls.Remove(chkModel)

        '--Me.Caption = "Table: " + colTable.item("TABLENAME") + _
        '--                             "  Where: " + msRecordKeyColumn + "=" + msRecordKeyValue
        '==Me.Caption = k_version + "  --  Table: '" + colTable.item("TABLENAME") + "'"
        msBaseSelect = msBaseSelect + "  FROM [" + msEditTableName + "]  "
        '--Note: Jet doesn't seem to handle Table-name aliases...
        '-----  so don't use use joins to get foreign descriptors..
        '------   issues extra SELECTs after main row is read in..
        '== If mbIsSqlServer Then 
        '= NO Jet here any more..--
        msBaseSelect = msBaseSelect + sJoin

        '--msBaseSelect = msBaseSelect + "  WHERE (" + _
        '--              msEditTableName + "." + msRecordKeyColumn + "='" + msRecordKeyValue + "')"
        '== s1 = ""
        '----Get primarykey fld type, and --
        '----build WHERE clause with correct value syntax--

        '=== NO record YET !!!--
        '=== If Not mbNewRow Then  '--we have key values--
        '===    s1 = msMakeWhereCondition()
        '===    msWhereThisRecord = " WHERE " + s1
        '=== End If  '--new--

        '--adjust depth of form--
        '==  ??? ==
        lVpos = lVBottom + lVsep + 200 '---(lVsep * 2) '--vpos next line--
        '==frameEdit.Height = lVpos + cmdSave.Height + 150

        '== cmdSave.Top = lVpos
        '==cmdCancel.Top = cmdSave.Top
        '== cmdSaveExit.Top = cmdSave.Top
        '--move buttons if covered by Image frame--
        If picImage1.Enabled And _
                     (lVpos < (picImage1.Top + picImage1.Height + 200)) Then '--move buttons to lhs--
            '== cmdSave.Left = txtData(0).Left
            '== cmdCancel.Left = cmdSave.Left + 1440
            '== cmdSaveExit.Left = cmdCancel.Left + 1440
        End If
        '--cmdSave.Enabled = False
        '--cmdSaveExit.Enabled = False
 
        If gbDebug Then MsgBox(sFKeyInfo, vbInformation) '--tracking F-Keys-

    End Sub  '--Edit -activate--
    '= = = = = = = = = = = =

    '--end Edit routines --
    '--end Edit routines --
    '--end Edit routines --
    '--end Edit routines --
    '-===FF->

    '-- L o a d  -
    '-- L o a d  -

    Private Sub frmEdit_Load(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles MyBase.Load
        '== Dim colTableInfo As Collection

        cmdSave.Enabled = False

        '--  stuff from original LOAD..--
        mlFormDesignHeight = Me.Height '--save starting dimensions..-
        mlFormDesignWidth = Me.Width '--save starting dimensions..-

        '== mColKeyValues = New Collection
        '== mColRowValues = New Collection

        '= Call CenterForm(Me)

        '== mlGridLeft = VB6.PixelsToTwipsX(MSHFlexgrid1.Left)
        '== mlGridTop = VB6.PixelsToTwipsY(MSHFlexgrid1.Top) '--save grid pos..--
        mlFindTimer = -1 '-inactive.-

        '-- END stuff from original LOAD..--

        grpBoxMainFooter.Text = ""
        '== txtBigText.Text = ""
        '== txtBigText.Visible = False
        DTPickerModel.MinDate = CDate("1/1/1753")   '--sql server min. date..

        '-TESTING-
        '= MsgBox("TEST hiding.", MsgBoxStyle.Information)
        '= Me.DialogResult = DialogResult.Cancel
        labStatus.Text = ""

    End Sub  '--load --
    '= = = = = = = =  = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Activated--
    Private Sub frmEdit_Activated(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '--acivated-
    '= = = = = = = = = = =


    Private Sub frmEdit_Shown(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim idx, cx, tx As Integer
        Dim colP1 As Collection
        Dim s1, sMsg As String
        Dim vFldName As Object
        '== Dim colFieldx As Collection
        Dim sControlName, sColumnName As String
        Dim chk1 As CheckBox
        Dim txt1, txt2 As TextBox
        Dim cmd1 As Button

        'If mbActivated Then Exit Sub
        'mbActivated = True

        '='-TESTING-
        '=MsgBox("TEST hiding.", MsgBoxStyle.Information)
        '=Me.DialogResult = DialogResult.Cancel
        '=Me.Hide()
        '='=MsgBox("Form Now hidden..", MsgBoxStyle.Information)
        '=Exit Sub
        '='- end test-

        If (mColPreferredColumnsOriginal Is Nothing) OrElse (mColPreferredColumnsOriginal.Count <= 0) Then
            MsgBox("Error- No definition supplied for which Preferred Record columns to add/edit..", MsgBoxStyle.Exclamation)
            mbcancelled = True
            Me.DialogResult = DialogResult.Cancel
            Me.Hide()
            Exit Sub
        End If
        If (msDBname = "") Then  '== And mbIsSqlServer Then
            MsgBox("DB name empty in frmMaint startup..", MsgBoxStyle.Critical)
            Me.DialogResult = DialogResult.Cancel
            Me.Hide()
            Exit Sub
        End If
        If mColTables.Count() <= 0 Then
            MsgBox("Tables collection empty in frmEDit startup..", MsgBoxStyle.Critical)
            Me.DialogResult = DialogResult.Cancel
            Me.Hide()
            Exit Sub
        End If

        's1 = "=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & ", Build: " & _
        '                 My.Application.Info.Version.Build & ", Rev: " & My.Application.Info.Version.Revision & "="
        Me.Text = "Editing Table:  " & msEditTableName & ". " & msVersionPOS   '== & "  (JobMatixPOS:  " & s1 & ")."

        '= labVersion.Text = "JobMatixPOS: " & s1

        If Not mbNoFormCentering Then Call CenterForm(Me)

        '--hide all childTable buttons to start--
        '--For i = 1 To ki_maxChildButtons
        '--      cmdchildTable(i - 1).Visible = False
        '--Next i
        '--miListIndex = miCurrentTableIndex
        '--idx = miListIndex
        '--set up stuff for current table--
        '-Set mColTable = mColTables(idx)
        mColTable = mColTables.Item(msEditTableName)
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
                    If UCase(msEditTableName) = UCase(colP1.Item(idx)) Then
                        mColChildTables.Add(mColTables.Item(cx)("TABLENAME"))
                        Exit For '--one is enough--
                    End If
                Next idx
            End If '--parents-
        Next cx
        tx = 0 '--count tables in browse list--
        '--For idx = 1 To mColTables.count
        msCurrentPrimaryKeyName = ""

        If mColPrimaryKeys.Count() > 0 Then
            msCurrentPrimaryKeyName = mColPrimaryKeys.Item(1)
            '=Else '--use col-0 --
            '==    s1 = mColFields.Item(1)("NAME")
        End If
        '== msCurrentPrimaryKeyName = s1

        '--  Copy mColPreferredColumnsOriginal to
        '--     mColPreferredColumns with primary key fld the first item..
        If (msCurrentPrimaryKeyName = "") Then  '--no PKEY-
            mColPreferredColumns = mColPreferredColumnsOriginal
        Else
            mColPreferredColumns = New Collection
            '--two passes of collection to put PKEY up front--
            For idx = 1 To 2
                For Each vFldName In mColPreferredColumnsOriginal
                    If idx = 1 Then  '--first pass- Grab PKEY--
                        If UCase(CStr(vFldName)) = UCase(msCurrentPrimaryKeyName) Then
                            mColPreferredColumns.Add(msCurrentPrimaryKeyName)
                        End If
                    Else  '--second pass- All others..
                        If UCase(CStr(vFldName)) <> UCase(msCurrentPrimaryKeyName) Then  '--all oters-
                            mColPreferredColumns.Add(CStr(vFldName))
                        End If
                    End If  '-idx=1-
                Next  '--fld
            Next  '- ídx
        End If

        labTitle.Text = msTitle
        If msTitle = "" Then labTitle.Text = "Editing: '" & msEditTableName & "' table."

        Call Edit_Activate()  '--Build all edit controls..
        '-done-

        '== 3101.1207= Special for Customer Table.=
        mbIsAccountCustomer = False
        mbIsCustomerTable = (LCase(msEditTableName) = "customer")
        If mbNewRow AndAlso mbIsCustomerTable Then
            sMsg = "IMPORTANT:" & vbCrLf & _
                     "Is this NEW Customer to be " & vbCrLf & _
                     "  an Account (debtors ledger) customer or not ?" & vbCrLf & _
                     " (Answering 'No' will create a normal (cash) Customer.)" & vbCrLf & _
                   vbCrLf & "  NB: Can NOT be changed later !!" & vbCrLf
            If MsgBox(sMsg, MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                mbIsAccountCustomer = True
            End If '-yes-
        End If '-new-

        '-- S P E C I A L for Customer Table-
        '-- S P E C I A L for Customer Table-

        '-IF customer table, then set some fields as read-only..
        '--  ie. isAccountCust, openedStaff_id, openedStaffName.
        If mbIsCustomerTable Then
            For Each control1 As Control In panelEdit.Controls
                sControlName = LCase(control1.Name)
                '-- get new data for update-
                sColumnName = control1.Tag
                If (sControlName = "chkdata_isaccountcust") Then
                    chk1 = CType(control1, CheckBox)
                    txt1 = panelEdit.Controls("txtdata_isaccountcust")  '--holds DB col. value.
                    txt2 = panelEdit.Controls("txtdata_creditlimit")  '--holds DB col. value.
                    chk1.Enabled = False
                    '= If Not mbIsAccountCustomer Then
                    '= txt2.Enabled = False  '-no credit limit..-
                    '= End If
                    If mbNewRow Then  '-set bit-
                        If mbIsAccountCustomer Then '-only know this FOR NEW record.-
                            chk1.Checked = True
                            txt1.Text = "1"
                        Else  '-no account-
                            chk1.Checked = False
                            txt1.Text = "0"
                            txt2.Enabled = False  '-no credit limit..-
                        End If
                    End If '-new-
                ElseIf (sControlName = "txtdata_openedstaff_id") Then
                    txt1 = CType(control1, TextBox)
                    txt1.ReadOnly = True
                    If mbNewRow Then
                        txt1.Text = CStr(mIntStaff_id)
                    End If
                    Try
                        cmd1 = panelEdit.Controls("cmddata_openedstaff_id")
                        cmd1.Visible = False
                    Catch ex As Exception
                        MsgBox("No cmd button for openedStaff_id.", MsgBoxStyle.Information)
                    End Try
                ElseIf (sControlName = "txtdata_openedstaffname") Then
                    txt1 = CType(control1, TextBox)
                    txt1.ReadOnly = True
                    If mbNewRow Then
                        txt1.Text = msStaffName
                    End If
                End If
            Next control1
        End If  '-customer table.-


        '==Call CenterForm(Me)--

        '-- position defined by frmBrowse caller..

        '-- EX Edit_Activate --
        mbModified = False   '--row flag--
        mbUpdated = False
        '== mlFrameTop = Me.Top + frameEdit.Top
        '== mlFrameLeft = Me.Left + frameEdit.Left
        '--retrieve full current record--
        If Not mbNewRow Then
            '-- record key was supplied..--
            If (Not mColRecordKeyValues Is Nothing) AndAlso (mColRecordKeyValues.Count > 0) Then
                s1 = CStr(mColRecordKeyValues(1))
                msWhereThisRecord = Replace(msWhereThisRecord, "&&&KEYVALUE", s1)
            End If
            '== MsgBox("Will edit record: " & msWhereThisRecord, MsgBoxStyle.Information)
            cmdSaveExit.Text = "Exit"
            cmdSaveExit.Enabled = True '--not used with maint2--
            cmdSave.Enabled = False
            Call mbRefresh()  '--get/show record--  --
            '-- S P E C I A L for Customer Table-
            '-IF customer table, then set some fields as read-only..
            '--  ie. isAccountCust, openedStaff_id, openedStaffName.
            If mbIsCustomerTable Then
                chk1 = panelEdit.Controls("chkdata_isaccountcust")  '--holds DB col. value.
                txt2 = panelEdit.Controls("txtdata_creditlimit")  '--holds DB col. value.
                mbIsAccountCustomer = chk1.Checked
                If Not mbIsAccountCustomer Then
                    txt2.Enabled = False  '-no credit limit..-
                End If
            End If '-cust-
        Else
            '-- new row..--
            cmdSave.Visible = False
            Call msCheckRequiredFields()   '--set list of needed.
            mbEditActive = True
            cmdSaveExit.Enabled = False '--not used with maint2--
        End If '-new/old-
        '==cmdCancel.Enabled = False
        mbStartingUp = False

    End Sub  '--Shown --
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Text box events..--
    '-- Handles All Data text boxes..--
    '-- Handles All Data text boxes..--


    Private Sub txtData_Enter(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) '== Handles txtData.Enter

        '--get ref to which textbox.--

    End Sub '--text Enter-
    '= = = = = = = = = = = = 

    '-- Handles All Data text boxes..--
    '-- Handles All Data text boxes..--


    Private Sub txtData_Validating(ByVal sender As Object, _
                                     ByVal e As System.ComponentModel.CancelEventArgs)  '== Handles textData.Validating

        Dim txtData As TextBox = CType(sender, System.Windows.Forms.TextBox)
        Dim sName = txtData.Name
        Dim sFldName As String
        Dim colField As Collection
        Dim sSqlType As String
        Dim errorMsg As String = ""

        '--get ref to which txt.--
        If InStr(LCase(sName), "txtdata_") > 0 Then  '--valid-
            sFldName = Mid(sName, 9)
        Else '--invalid-
            Exit Sub
        End If
        If txtData.ReadOnly Then
            Exit Sub  '-let it go-
        End If
        '--  get info this column.--
        colField = mColEditFields(sFldName)
        sSqlType = colField.Item("TYPE_NAME")
        If (Trim(txtData.Text) <> "") Then
            If gbIsNumericType(sSqlType) Then
                If (Not IsNumeric(txtData.Text)) Then
                    errorMsg = "Field is for NUMERIC data only."
                End If
            End If
        End If  '--empty-

        If errorMsg <> "" Then  '--error
            e.Cancel = True
            MsgBox(errorMsg, MsgBoxStyle.Exclamation)
        End If

        '-- sample --
        '== If Not ValidEmailAddress(textBox1.Text, errorMsg) Then
        '==   ' Cancel the event and select the text to be corrected by the user.
        '==   e.Cancel = True
        '===   textBox1.Select(0, textBox1.Text.Length)
        '===   ' Set the ErrorProvider error with the text to display. 
        '===   Me.errorProvider1.SetError(textBox1, errorMsg)
        '== End If
    End Sub  '--txtData_Validating--
    '= = = = = = = = = = = = = = = 

    '-- Handles All Data text boxes..--

    Private Sub txtData_TextChanged(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) '== Handles txtModelData.TextChanged
        Dim txtData As TextBox = CType(sender, System.Windows.Forms.TextBox)
        Dim sName = txtData.Name
        Dim sFldName As String
        Dim colField As Collection
        Dim sSqlType As String
        Dim intIndex As Integer
        Dim font1 As New Font(txtData.Font, FontStyle.Bold)

        If mbStartingUp Then Exit Sub
        '--get ref to which textbox.--
        '--get ref to which txt.--
        If InStr(LCase(sName), "txtdata_") > 0 Then  '--valid-
            sFldName = Mid(sName, 9)
        Else '--invalid-
            Exit Sub
        End If
        '--  get info this column.--
        colField = mColEditFields(sFldName)
        sSqlType = colField.Item("TYPE_NAME")
        intIndex = CInt(txtData.Tag)   '--  get txt control "index".

        '--  set correct "mabFldModified" value..
        Call mbDataChanged(intIndex)
        txtData.Font = font1  '--bold --
        DoEvents()

    End Sub '--text changed.-
    '= = = = = = = = = = = = 
    '-===FF->

    '-- date picker-

    Private Sub DTPickerData_ValueChanged(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs)   '= Handles DTPickerModel.ValueChanged

        Dim DTPickerData As DateTimePicker = CType(sender, System.Windows.Forms.DateTimePicker)
        Dim sName = DTPickerData.Name
        Dim sFldName As String
        Dim colField As Collection
        Dim sSqlType As String
        Dim txtData As TextBox
        Dim intIndex As Integer

        If mbStartingUp Then Exit Sub
        '--save current value in text field.-
        '--get ref to which picker.--
        If InStr(LCase(sName), "dtpickerdata_") > 0 Then  '--valid-
            sFldName = Mid(sName, 14)
        Else '--invalid-
            Exit Sub
        End If
        '--  get info this column.--
        colField = mColEditFields(sFldName)
        sSqlType = colField.Item("TYPE_NAME")

        '= MsgBox("Field: " & sFldName & vbCrLf & _
        '==          "Data type is: " & sSqlType, MsgBoxStyle.Information)
        txtData = colField.Item("txtData")
        intIndex = CInt(txtData.Tag)   '--  get txt control "index".
        txtData.Text = Format(DTPickerData.Value, "dd-MMM-yyyy hh:mm")  '=CStr(DTPickerData.Value)
        Call mbDataChanged(intIndex)

    End Sub '--DTPicker--
    '= = = = =  = = == =  =

    '--  Yes/no checkbox-

    Private Sub chkData_CheckedChanged(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs)  '==Handles chkModel.CheckedChanged
        Dim chkData As CheckBox = CType(sender, System.Windows.Forms.CheckBox)
        Dim sName = chkData.Name
        Dim sFldName As String
        Dim colField As Collection
        Dim sSqlType As String
        Dim txtData As TextBox
        Dim intIndex As Integer

        '--get ref to which cmd button.--
        If mbStartingUp Then Exit Sub
        If InStr(LCase(sName), "chkdata_") > 0 Then  '--valid-
            sFldName = Mid(sName, 9)
        Else '--invalid-
            Exit Sub
        End If
        '--  get info this column.--
        colField = mColEditFields(sFldName)
        sSqlType = colField.Item("TYPE_NAME")

        '== MsgBox("Field: " & sFldName & vbCrLf & _
        '=          "Data type is: " & sSqlType, MsgBoxStyle.Information)
        txtData = colField.Item("txtData")
        intIndex = CInt(txtData.Tag)   '--  get txt control "index".
        '-- save current value (0/1) in txtData..
        If chkData.Checked Then
            txtData.Text = "1"
        Else
            txtData.Text = "0"
        End If
        Call mbDataChanged(intIndex)
    End Sub  '--chkModel-
    '= = = = = = = = = = = = = 
    '-===FF->

    '-- cmdData ---
    '-- Handles All cmdData buttons..--

    Private Sub cmdData_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) '== Handles cmdData--

        Dim cmdData As Button = CType(sender, System.Windows.Forms.Button)
        Dim sName = cmdData.Name
        Dim sFldName As String
        Dim colField As Collection
        Dim sSqlType As String
        Dim txtData As TextBox
        Dim col1 As Collection
        Dim intIndex As Integer
        '-- image stuff-
        Dim sTitle, sStartPath, sFullPath As String
        Dim MyResult As System.Windows.Forms.DialogResult
        Dim byteImage1 As Byte()
        Dim image1 As Image
        '== Dim frmEditItem1 As frmEditItem
        '-- for FKEY browser..
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sFKeyTable As String
        Dim sFKeyField, sFKeyDisplayField As String
        Dim labJoin As Label

        '--get ref to which cmd button.--
        If InStr(LCase(sName), "cmddata_") > 0 Then  '--valid-
            sFldName = Mid(sName, 9)
        Else '--invalid-
            Exit Sub
        End If
        '--  get info this column.--
        colField = mColEditFields(sFldName)
        sSqlType = colField.Item("TYPE_NAME")

        '== MsgBox("Field: " & sFldName & vbCrLf & _
        '==          "Data type is: " & sSqlType, MsgBoxStyle.Information)
        txtData = colField.Item("txtData")
        intIndex = CInt(txtData.Tag)   '--  get txt control "index".

        If colField.Item("ISIMAGECOLUMN") Then
            '--  image column..--
            sTitle = ""
            sStartPath = ""
            '--  get actual (image File) location from operator..--
            OpenDlg1.Title = msEditTableName & ":  Select Image file for this record.."
            OpenDlg1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.ICO)|*.BMP;*.JPG;*.GIF;*.PNG;*.ICO|All files (*.*)|*.* "
            '= "SQL DB Backup Files (*.png)|*.jpg|All Files (*.*)|*.*"
            OpenDlg1.InitialDirectory = sStartPath '--msAppPath
            OpenDlg1.FileName = sStartPath & sTitle
            MyResult = OpenDlg1.ShowDialog
            '--check for cancel--
            If (MyResult <> System.Windows.Forms.DialogResult.OK) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            '== On Error GoTo 0
            sFullPath = OpenDlg1.FileName
            Try
                '--load image bytes..--
                byteImage1 = mabConvertImageFiletoBytes(sFullPath)
            Catch ex As Exception
                MsgBox("Failed to load image data from File: " & sFullPath & vbCrLf & _
                                   "Error: " & ex.Message)
                Exit Sub
            End Try
            '-- save image data for this columns-
            If mColRowImages.Contains(sFldName) Then
                mColRowImages.Remove(sFldName)
            End If
            mColRowImages.Add(byteImage1, sFldName)

            '--- load picture from byte array..-
            Try
                Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(byteImage1)
                image1 = System.Drawing.Image.FromStream(ms)
                picImage1.Image = image1
                ms.Close()
                Call mbDataChanged(intIndex)
            Catch ex As Exception
                MsgBox("Failed to load image from MemStream: " & msEditTableName & vbCrLf & _
                                  "Error: " & ex.Message)
                Exit Sub
            End Try

            '= ElseIf (UCase$(sSqlType) = "TEXT") Or (UCase$(sSqlType) = "NTEXT") Or _
            '==                            (gbIsText(sSqlType) And (lMaxSize > 255)) Then
        ElseIf colField.Item("ISBIGTEXT") Then
            '--  show multiline text box..
            '-- txtBigText --

        ElseIf gbIsDate(sSqlType) Then
            DoEvents()
            '-- not used--

        ElseIf colField("ISFOREIGNKEY") Then
            '--  call browser..-
            '-- get foreign-key table name..
            sFKeyTable = colField("FOREIGNTABLE")
            sFKeyField = colField("FOREIGNKEYFIELD")
            sFKeyDisplayField = colField("FOREIGNDISPLAYFIELD")

            If Not mbBrowseTable("Lookup " & sFKeyTable, "", _
                                   colKeys, colSelectedRow, sFKeyTable, _
                                   Me.Top + panelEdit.Top + txtData.Top + 17, Me.Left + panelEdit.Left + txtData.Left) Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--ok. selected..-
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    '== MsgBox("Selected : " & colKeys(1))
                    txtData.Text = colKeys(1)  '--save fkey as data..
                    labJoin = colField.Item("labJoin")
                    If colSelectedRow.Contains(sFKeyDisplayField) Then
                        col1 = colSelectedRow.Item(sFKeyDisplayField)
                        labJoin.Text = col1.Item("value")
                    Else
                        MsgBox("No value in selected row for: " & sFKeyDisplayField, MsgBoxStyle.Information)
                    End If
                    Call mbDataChanged(intIndex)
                End If
            End If  '--browse.-
        End If  '--is image/FKEY column etc..-

    End Sub '--click.-
    '= = = = = = = = = = = = 
    '-===FF->


    '-- Save/Exit stuff..
    '-- Save/Exit stuff..

    '--Save --
    '--Save --

    Private Sub cmdSave_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles cmdSave.Click
        If mbUpdateRecord() Then
            cmdSave.Enabled = False
            cmdCancel.Visible = False
            cmdSaveExit.Text = "Exit"
            cmdSaveExit.Enabled = True
        End If

    End Sub  '--Save --
    '= = = = = = = = = =  = == = 

    '--SaveExit-- or EXit.-
    Private Sub cmdSaveExit_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles cmdSaveExit.Click
        If mbNewRow Then
            If mbAddNewRecord() Then
                Me.Hide()
            End If
        Else
            Me.Hide()
        End If

    End Sub  '--SaveExit--
    '= = = = = = = = = = = = 

    '--cancel--

    Private Sub cmdCancel_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles cmdCancel.Click

        If mbModified Then
            If MsgBox("Abandon changes ", _
                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Me.Hide()
            End If
        End If
    End Sub  '--cancel--
    '= = = = = = = = = = = = == 

End Class

'== end form ===