Option Strict Off
Option Explicit On
Imports System.Data.OleDb

Module modJetLogin
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	
	'= = = = =  = ==
	
	
	'--  Jet Login for Retail Host class..--
	'----  updated 24-Oct-2011= for VB.NET smoothness..--
    '----  updated 29-Mar-2012= for DB open error message upgrades..--

    '== grh 3077-  19May2013..-  Tidied up 'gbGetJet360DbInfo' --
    '==
    '--  JobMatix v3.1.3101 =PO3- Ugrade-- 16-Sep-2014 --
    '--  NOW using System.Data.OleDb
    '--  To be connection compatible with JobMatix v3.1 and Jet/OLEDB..-
    '==
    '==  grh. JobMatix 3.1.3101.1110 ---  10-Nov-2014 ===
    '==   >>  Move all DAO functions to in here . 
    '==         so sqlSupport is free of Jet/DAO links.
    '==
    '= = = =  = = = = = = = = = = = == = = = = = = = = = = = = = = = = 

	
	'--get ADO  errors--
	'--get ADO errors--
	
    '==3101= Private Function msGetErrors(ByRef cnn As ADODB.Connection, ByRef lErr As Integer) As String
    '==3101= Dim errs1 As ADODB.Errors
    '==3101= Dim k As Short
    '==3101= Dim sResult As String
    '==3101= Dim objErr As ADODB.Error

    '==3101= errs1 = cnn.Errors
    '==3101= sResult = "" : k = 0
    '==3101= If lErr <> 0 Then sResult = "Error No: " & lErr & " " & ErrorToString(lErr) & vbCrLf
    '==3101= For	Each objErr In errs1
    '==3101= If k = 0 Then
    '==3101= sResult = sResult & "== SQL Errors are: ==" & vbCrLf
    '==3101= k = 1
    '==3101= End If
    '==3101= sResult = sResult & "SQLState:" & objErr.SQLState & ", NativeError:" & objErr.NativeError & " " & objErr.Description & vbCrLf
    '==3101= Next objErr '-err-
    '==3101= errs1 = Nothing
    '==3101= objErr = Nothing
    '==3101= msGetErrors = sResult
    '==3101= End Function '--geterrors-
    '= = = = = = = = = = = = =
    '-===FF->


    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--
    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--
    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--

    Private Function mIntTypeDAOtoADO(ByRef vType As Object) As Integer
        Dim lngADO As Integer

        Select Case vType
            Case DAO.DataTypeEnum.dbBigInt : lngADO = ADODB_DataTypeEnum_adBigInt
            Case DAO.DataTypeEnum.dbBinary : lngADO = ADODB_DataTypeEnum_adBinary
            Case DAO.DataTypeEnum.dbBoolean : lngADO = ADODB_DataTypeEnum_adBoolean
            Case DAO.DataTypeEnum.dbByte : lngADO = ADODB_DataTypeEnum_adUnsignedTinyInt
            Case DAO.DataTypeEnum.dbChar : lngADO = ADODB_DataTypeEnum_adVarChar
            Case DAO.DataTypeEnum.dbCurrency : lngADO = ADODB_DataTypeEnum_adCurrency
            Case DAO.DataTypeEnum.dbDate : lngADO = ADODB_DataTypeEnum_adDate
                '-------Case db:      lngADO = adDBTimeStamp: sT = "DATETIME"
            Case DAO.DataTypeEnum.dbDecimal : lngADO = ADODB_DataTypeEnum_adDecimal
            Case DAO.DataTypeEnum.dbDouble : lngADO = ADODB_DataTypeEnum_adDouble
            Case DAO.DataTypeEnum.dbFloat : lngADO = ADODB_DataTypeEnum_adDouble
            Case DAO.DataTypeEnum.dbGUID : lngADO = ADODB_DataTypeEnum_adGUID
            Case DAO.DataTypeEnum.dbInteger : lngADO = ADODB_DataTypeEnum_adInteger
            Case DAO.DataTypeEnum.dbLong : lngADO = ADODB_DataTypeEnum_adInteger
            Case DAO.DataTypeEnum.dbLongBinary : lngADO = ADODB_DataTypeEnum_adLongVarBinary
                '----Case db:      lngADO = adLongVarChar: sT = "TEXT"
            Case DAO.DataTypeEnum.dbMemo : lngADO = ADODB_DataTypeEnum_adLongVarWChar
            Case DAO.DataTypeEnum.dbNumeric : lngADO = ADODB_DataTypeEnum_adDouble
            Case DAO.DataTypeEnum.dbSingle : lngADO = ADODB_DataTypeEnum_adSingle
            Case DAO.DataTypeEnum.dbText : lngADO = ADODB_DataTypeEnum_adVarWChar
            Case DAO.DataTypeEnum.dbTime : lngADO = ADODB_DataTypeEnum_adDate
            Case DAO.DataTypeEnum.dbTimeStamp : lngADO = ADODB_DataTypeEnum_adDate
            Case DAO.DataTypeEnum.dbVarBinary : lngADO = ADODB_DataTypeEnum_adVarBinary

            Case Else : lngADO = ADODB_DataTypeEnum_adVarChar
        End Select

        mIntTypeDAOtoADO = lngADO
    End Function '--  glTypeDAOtoADO --
    '= = = =  = = = =
    '-===FF->

    '= = = =  c o n n e c t = = = =

    Private Function mbConnectSql(ByRef cnnSQL As OleDbConnection, _
                                  ByVal sConnect As String, _
                                   ByRef sErrorMsg As String) As Boolean
        Dim ans As Integer
        Dim L1 As Integer
        Dim s1, s2 As String

        mbConnectSql = False
        If (cnnSQL Is Nothing) Then cnnSQL = New OleDbConnection
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '= On Error Resume Next
        '= cnnSQL.Open(sConnect)
        '== ans = Err().Number
        Try
            cnnSQL.ConnectionString = sConnect
            cnnSQL.Open()
            mbConnectSql = True

        Catch ex As Exception
            sErrorMsg = "Failed Connect to Jet database...." & vbCrLf
            sErrorMsg = sErrorMsg & "Error: " & ex.Message & vbCrLf
            sErrorMsg = sErrorMsg & "connect string was: <" & sConnect & ">" & vbCrLf
            '==s2 = sErrorMsg & vbCrLf
            '== If gbDebug Then MsgBox(s2, MsgBoxStyle.Critical, "Sql Connect..")
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, sErrorMsg & vbCrLf & "-- end of error msg.--")
            End If '--log--

        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '--connect--
    '- - - - - - - - -
    '-===FF->


    '= =Display sql database schema= = = ==  =
    '= =Display sql database schema= = = ==  =
    '--  from our internal collections--

    '== V3.1.3101- oleDb Public Function gsShowSqlSchema(ByRef cnnSQL As ADODB.Connection, _
    '==                                  ByRef sSqlDBName As String, _
    '==                                    ByRef colSqlTables As Collection) As String


    Public Function gsShowJetSchema(ByRef sSqlDBName As String, _
                                       ByRef colSqlTables As Collection) As String

        Dim i, j As Integer
        Dim isx As Integer
        '== Dim bOk As Boolean
        '= Dim L1 As Long
        Dim s1 As String
        Dim sList, sTableName As String
        '== Dim colTblList As Collection
        Dim colTable As Collection
        Dim colFieldx As Collection '-- 1 field--
        Dim colFields As Collection
        Dim colTableInfo As Collection
        Dim colPrimaryKey As Collection
        Dim colForeignKeys As Collection
        Dim colOtherIndexes As Collection
        Dim colParents As Collection
        Dim v1 As Object
        '== Dim rs As ADODB.Recordset
        '= Dim lResult As Long
        Dim sLogMsg As String

        '--If Not gbGetDatabaseInfo(cnnSQL, sSqlDBname, colSqlTables) Then Exit Sub
        gsShowJetSchema = ""
        sLogMsg = vbCrLf & "==Showing sql catalogue for:" & vbCrLf & "DB: '" & sSqlDBName & "' == ." & vbCrLf & vbCrLf
        '--ok.. show collection--

        '---catalogue has been loaded..  now show all schemas--
        '----incl- (primary keys, and foreign keys)--
        '== gAdvise " == Displaying catalogue for DB: " + sSqlDBname
        For isx = 1 To colSqlTables.Count()
            colTable = colSqlTables.Item(isx)
            sTableName = colTable.Item("TABLENAME")
            colTableInfo = colTable.Item("SOURCEINFO")
            colFields = colTable.Item("FIELDS")
            colPrimaryKey = colTable.Item("PRIMARYKEYS")
            '--Set colPrimaryKey = colTable(3)
            colForeignKeys = colTable.Item("FOREIGNKEYS")
            colOtherIndexes = colTable.Item("OTHERINDEXES")
            colParents = colTable.Item("PARENTS")

            '--sTableName = colTableInfo.item("TABLENAME")
            sLogMsg = sLogMsg & vbCrLf & "-- TABLE '" & sTableName & "' has columns:" & vbCrLf
            sList = ""
            If colFields.Count() > 0 Then
                For Each colFieldx In colFields
                    If sList <> "" Then sList = sList & vbCrLf
                    sList = sList & colFieldx.Item("NAME") + ";  "
                    If colFieldx.Item("ISAUTOINCR") Then
                        sList = sList & " AUTOINCR "
                    End If
                    sList = sList & "[Fld-Type:" & CStr(colFieldx.Item("TYPE"))
                    sList = sList & "; DefSize: " & CStr(colFieldx.Item("DEFINEDSIZE")) & "];"
                    sList = sList & "  SqlType:" & colFieldx.Item("TYPE_NAME") & "; "
                    If Not colFieldx.Item("ISNULLABLE") Then
                        sList = sList & "NOT NULL "
                    Else
                        '--sList = sList & "(null) "
                    End If
                    If colFieldx.Item("ISFOREIGNKEY") Then
                        sList = sList & " FOREIGNKEY "
                    End If
                    '--DEBUG--
                    '--If UCase$(sTableName) = "MASTERS" And colFieldx("NAME") = "MASTOWNER" Then
                    '--  s1 = "Allitems: "
                    '--  For i = 1 To colFieldx.count
                    '--   v1 = colFieldx(i)
                    '--   s1 = s1 + CStr(v1) + "  "
                    '--  Next i
                    '--  On Error Resume Next
                    '--  s2 = colFieldx.item("FOREIGNTABLE")   '--if exists--
                    '--  k = Err()
                    '--  DoEvents
                    '--End If
                    '--END DEBUG--
                    '--On Error Resume Next
                    '--s1 = colFieldx.item("FOREIGNTABLE")   '--if exists--
                    '--If Err() = 0 Then sList = sList + " (REF " + s1 + ") "   '--ok if exists--
                    '--On Error GoTo 0
                    If colFieldx.Item("ISFOREIGNKEY") Then '--if exists--
                        s1 = colFieldx.Item("FOREIGNTABLE") '--if exists--
                        sList = sList & " REF " & s1 & "; "
                    End If '--foreign-
                Next colFieldx
            End If '--count--
            sLogMsg = sLogMsg & sList & vbCrLf '--gAdvise sList
            sList = "       Primary Key fields: "
            If (colPrimaryKey.Count() > 0) Then
                For Each v1 In colPrimaryKey
                    sList = sList + v1 + " "
                Next v1
            End If '--count--
            sLogMsg = sLogMsg & sList & vbCrLf '--gAdvise sList
            '--show foreign keys..--
            sList = " Foreign Key fields: " & vbCrLf
            If colForeignKeys.Count() > 0 Then
                For Each colFieldx In colForeignKeys
                    s1 = colFieldx.Item("LOCALFIELD")
                    sList = sList & colFieldx.Item("LOCALFIELD") & _
                          " REF " & colFieldx.Item("FOREIGNTABLE") & "," + colFieldx.Item("FOREIGNFIELD") & vbCrLf
                Next colFieldx
            End If '--count--
            sLogMsg = sLogMsg & sList & vbCrLf '--gAdvise sList
            '--show other indexes--
            sList = ""
            If colOtherIndexes.Count() > 0 Then
                sList = "-- Other indexes on: " & vbCrLf
                For i = 1 To colOtherIndexes.Count()
                    colFieldx = colOtherIndexes.Item(i)
                    sList = sList & VB6.Format(i, "00") & ". "
                    If colFieldx.Count() > 0 Then
                        For j = 1 To colFieldx.Count()
                            sList = sList & " " + colFieldx.Item(j)
                        Next j
                    End If '-count--
                    sList = sList & vbCrLf
                Next i
            End If '--other--
            If sList <> "" Then sLogMsg = sLogMsg & sList '-- gAdvise sList
            sList = ""
            If colParents.Count() > 0 Then
                For i = 1 To colParents.Count()
                    If i > 1 Then sList = sList & ", "
                    sList = sList + colParents.Item(i)
                Next i
                sLogMsg = sLogMsg & "  Parent tables are: " & sList & vbCrLf
            End If '--parents-
            '--show selected "name" column..
            sLogMsg = sLogMsg & "Name column is: " + colTable.Item("NAMECOLUMN") + vbCrLf
        Next isx '--table--
        '==sLogMsg = sLogMsg & "=== All Done == " & vbCrLf & vbCrLf
        sLogMsg = vbCrLf & sLogMsg & "==Completed showing sql catalogue for:" & vbCrLf & _
                    "DB: '" & sSqlDBName & "' == ." & vbCrLf & "=== === === ===" & vbCrLf

        gsShowJetSchema = sLogMsg '--result.-
    End Function '--show--
    '= = = = = = = = =
    '--Note:  for "getDbInfo, see "InfoSchema.bas" ..
    '-===FF->

    '--  DAO-based function to get schema for Jet 3.51 (Access'97) DB ----
    '--  DAO-based function to get schema for Jet 3.51 (Access'97) DB ----
    '--  DAO-based function to get schema for Jet 3.51 (Access'97) DB ----

    '= =build Jet 3.51 database schema= = = ==  =
    '= =build Jet 3.51 database schema= = = ==  =

    '---grh-- ==19-April-2009== -DAO for (Jet 3.51) DB.. ==
    '---grh-- ==04-Mar-2010== -Fix problem showing AccessVersion.. now on LOG... ==
    '-----     also..  add "ORDINALPOSITION" to field collection.. ===
    '---grh-- ==30-Jan-2011== Ver: 2802 -Use "required" field property to determin nullablity. ==

    '---grh-- ==02-Oct-2011== RENAMED Jet360 for QB-POS needing DAO 3.6 (Access 2000 format).. ==
    '---grh-- ==19-May-2013== Build 3077- Tidying up... ==

    Public Function gbGetJet360DbInfo(ByRef cnnSQL As OleDbConnection, _
                                      ByRef sJetDBname As String, _
                                       ByRef sJetUid As String, _
                                        ByRef sJetPwd As String, _
                                          ByRef colSqlTables As Collection, _
                                             ByRef sLog As String) As Boolean

        Dim kx, tx, k, fx, px As Integer
        '==Dim bOk As Boolean
        Dim L1, intSize As Integer
        Dim s2, s1, Msg, s3 As String
        Dim sCreate, sNull As String
        Dim sKeyField As String
        Dim sConnect, sAppPath As String
        Dim sList, sTableName, sNameList As String
        Dim sFtable, sLocalFld As String
        Dim sForeignFld As String
        Dim colParents As Collection
        Dim colTblList As Collection
        Dim colTable As Collection
        Dim colFieldx As Collection '-- 1 field--
        Dim colFields As Collection
        Dim colTableInfo As Collection
        Dim colPrimaryKey As Collection
        Dim colForeignKeys As Collection
        Dim colOtherIndexes As Collection
        Dim v1, v2 As Object
        Dim colUserTypes As Collection
        '--DAO stuff--
        Dim db1 As DAO.Database
        Dim table1 As DAO.TableDef
        Dim field1 As DAO.Field
        Dim index1 As DAO.Index
        Dim relation1 As DAO.Relation
        Dim sDbVersion As String
        Dim sAccessVersion As String
        Dim sNameCol As String

        gbGetJet360DbInfo = False
        sLog = ""
        On Error Resume Next
        db1 = DAODBEngine_definst.Workspaces(0).OpenDatabase(sJetDBname, False, True, "MS Access; PWD=" & sJetPwd)
        L1 = Err().Number
        If L1 <> 0 Then
            MsgBox("Runtime Error in GetJet360DbInfo openDatabase function.." & vbCrLf & _
                                                        "Error is " & L1 & " = " & ErrorToString(L1))
            Exit Function
        End If
        sDbVersion = db1.Version
        sAccessVersion = db1.Properties("AccessVersion").Value
        '==MsgBox "Access version is: " + db1.Properties("AccessVersion")
        sLog = sLog & "DB File: " & sJetDBname & vbCrLf & "Jet Version is: " & sDbVersion & ".." & vbCrLf
        sLog = sLog & "MS Access version: " + db1.Properties("AccessVersion").Value & ".." & vbCrLf
        colTblList = New Collection
        sList = ""

        '--get all tables--
        sLog = sLog & "= = = = = = = = " & vbCrLf & "= = = = = = = = " & vbCrLf & vbCrLf '--separator--
        sLog = sLog & "GetJet360DBInfo: Reading DAO JET tables info for DB: " & sJetDBname & vbCrLf
        sLog = sLog & "MS-Access version is: " & sAccessVersion & vbCrLf & vbCrLf

        On Error GoTo GetJet351Info_error
        For Each table1 In db1.TableDefs
            '--If UCase(table1.  .Type) = "TABLE" Then
            s1 = table1.Name
            colTblList.Add(s1)
            sList = sList & " TABLE: " & s1 & "  "
            '--End If   '--table--
            '--colTblList.Add s1
        Next table1 '--table-
        '--MsgBox "== GetJet351DBInfo: .. DATABASE: " + sJetDBname + vbCrLf + _
        ''--         " - Found " & colTblList.Count & " tables:  " + sList + vbCrLf + "- - - -"
        sLog = sLog & vbCrLf & "== GetJet360DBInfo: .. DATABASE: " & sJetDBname & vbCrLf & _
                                       " - Found " & colTblList.Count() & " tables:  " & sList & vbCrLf & "- - - -"
        On Error GoTo GetJet351Info_error2
        '--BUILD our catalogue for current database--
        '--for each table..  build complete description of columns/keys--
        colSqlTables = New Collection
        If (colTblList.Count() <= 0) Then
            '--MsgBox "No tables visible in DB: " + sSqlDBname + "  for current user..", vbExclamation
            sLog = sLog & "No tables visible in DB: " & sJetDBname & "  for current user.."
            Exit Function
        End If
        '--If gbVerbose Then gAdvise vbCrLf + "=== Reading sql columns info for: "
        For tx = 1 To colTblList.Count()
            sTableName = colTblList.Item(tx)
            If (UCase(Left(sTableName, 4)) <> "MSYS") Then '--Access System table ??--
                colTable = New Collection
                colTableInfo = New Collection
                colTable.Add(sTableName, "TABLENAME")
                '--colTableInfo.add sTableName, "TABLENAME"
                colFields = New Collection
                colPrimaryKey = New Collection
                colForeignKeys = New Collection
                colOtherIndexes = New Collection

                '--get all columns--
                table1 = db1.TableDefs(sTableName)

                '--each table..  get column info=-
                sList = "Table " & sTableName & " has columns:" & vbCrLf
                sNameList = "" '--names this table--

                For Each field1 In table1.Fields
                    colFieldx = New Collection
                    s1 = field1.Name '--s1 = rs("column_name")
                    colFieldx.Add(s1, "NAME")
                    sList = sList & s1 & ", "
                    sNameList = sNameList & UCase(s1) & "  "

                    '--MsgBox "Table " + sTableName + " has columns:" + vbCrLf + sNameList
                    '--colFieldx.add rs("data_type"), "TYPE"
                    L1 = field1.OrdinalPosition
                    colFieldx.Add(L1, "ORDINALPOSITION")

                    intSize = field1.Size '-- col1.DefinedSize
                    '---If Not IsNull(rs("char_octet_length")) Then _
                    ''---      L1 = rs("char_octet_length")
                    colFieldx.Add(intSize, "DEFINEDSIZE")

                    '==  THIS CRASHES.. ==>    k = glTypeDAOtoADO(field1.Type) '--get DAO data type--
                    L1 = field1.Type
                    k = mIntTypeDAOtoADO(L1) '--get DAO data type--
                    colFieldx.Add(k, "TYPE") '--ADO data type--
                    s2 = gsGetSqlType(k, intSize) '--3083= FIX= get equiv. sql type--
                    colFieldx.Add(s2, "TYPE_NAME")

                    '-- QBPOS multi host stuff..--02-Oct-2011==
                    If (field1.Attributes And DAO.FieldAttributeEnum.dbAutoIncrField) = 0 Then '--NOT AutoIncr..-
                        colFieldx.Add(False, "ISAUTOINCR") '--null not allowed in this column--
                    Else
                        colFieldx.Add(True, "ISAUTOINCR") '--null IS allowed in this column--
                    End If

                    '==ans = field1.Attributes  '-- And adColNullable '---- CInt(rs("nullable"))   '--smallint ---
                    '==If ans = 0 Then
                    If field1.Required Then
                        colFieldx.Add(False, "ISNULLABLE") '--null not allowed in this column--
                    Else
                        colFieldx.Add(True, "ISNULLABLE") '--null IS allowed in this column--
                    End If
                    colFieldx.Add(False, "ISPRIMARYKEY") '--changed below if true--
                    colFieldx.Add(False, "ISFOREIGNKEY") '--changed below if true--
                    '----colFieldx.add False, "ISFOREIGNKEY"    '--assume not foreign key for now--
                    colFields.Add(colFieldx, UCase(s1)) '--key is fldName--

                Next field1 '--field---

                sLog &= sList & "==" & vbCrLf '--show cols this table--
                '--MsgBox sList + "==" + vbCrLf   '--DEBUG  ==   show cols this table--

                '== gLogMsg(sList & vbCrLf)
                sList = " Foreign Keys are: " & vbCrLf
                On Error GoTo GetJet351Info_error2

                '==  Get foreign keys  loop==

                sList = " Foreign RELATIONS are: " & vbCrLf
                '== On Error GoTo GetJet351Info_error2
                '--For Each key1 In table1.Keys

                '-- SHOW all foreign relations to this table.--
                For Each relation1 In db1.Relations
                    '--If key1.Type = adKeyForeign Then
                    If UCase(relation1.Table) = UCase(sTableName) Then '--this table..--
                        '--get foreign keys--
                        '----Set colForeignKeys = New Collection
                        '--gLogMsg " =Foreign key: " + key1.Name
                        L1 = relation1.Attributes
                        sList = sList & " --Relation: '" & relation1.Name & "' Attr." & L1 & " on cols : " & vbCrLf
                        s2 = relation1.ForeignTable '--- key1.RelatedTable
                        kx = 0 : s1 = ""
                        If relation1.Fields.Count > 0 Then
                            For Each field1 In relation1.Fields '--only saves the FIRST one !!!!
                                kx = kx + 1 '--count keys--
                                If kx = 1 Then
                                    s1 = field1.Name '--rs("fkcolumn_name")
                                    s3 = field1.ForeignName '--- keycol1.RelatedColumn
                                End If
                                sList = sList & "       " & s1 & " (REF " & s2 & ", " & s3 & ") " & vbCrLf
                            Next field1 '--col--
                        Else '--no cols..
                            sList = sList & " NO COLUMN INFO !!..    " & vbCrLf
                        End If
                    End If '--this table..--
                Next relation1 '---relation..--

                sLog &= sList

                '--get OtherIndexes--
                colFieldx = Nothing
                '--gLogMsg "Reading Other index info for table: " + sTableName
                sList = " == Indexes : "
                s1 = "" '--idx name--
                On Error GoTo GetJet351Info_error3

                For Each index1 In table1.Indexes
                    '--k = rs("type")
                    '--pick out type-3 (other)--
                    s2 = CStr(index1.Name) '--rs("index_name")
                    '--MsgBox "Table : " + sTableName + "..  found index: " + s2
                    If index1.Primary Then '-- .PrimaryKey Then    '--Jet 3.51-- save primary key==
                        sList = sList & vbCrLf & "PKEY Index '" & s2 & "' on: "
                        On Error Resume Next
                        field1 = index1.Fields(0)
                        L1 = Err().Number
                        '==On Error GoTo 0
                        On Error GoTo GetJet351Info_error3
                        If L1 <> 0 Then
                            MsgBox("Error in getting PKEY index columns.." & vbCrLf & _
                                           "Error is " & L1 & " = " & ErrorToString(L1), MsgBoxStyle.Exclamation)
                        Else '--ok-
                            s1 = CStr(field1.Name)
                            colPrimaryKey.Add(s1)
                            colFields.Item(UCase(s1)).Remove("ISPRIMARYKEY") '--remove/add to change--
                            colFields.Item(UCase(s1)).Add(True, "ISPRIMARYKEY") '--remove/add to change--
                            sList = sList & " " & s1
                        End If '--error-
                    Else '----If Not index1.PrimaryKey Then    '--k = 3 Then
                        sList = sList & vbCrLf & "Other Index '" & s2 & "' on: "
                        kx = 0
                        colFieldx = New Collection
                        For Each field1 In index1.Fields
                            kx = kx + 1
                            s3 = CStr(field1.Name) '--save all cols--
                            '--End If
                            '--s1 = s2 '--save last index name--
                            colFieldx.Add(s3)
                            sList = sList & s3 & " "
                        Next field1 '--col1-
                        colOtherIndexes.Add(colFieldx) '--save this index--
                    End If '-not primary---3--
                    '--colOtherIndexes.add colFieldx
                Next index1 '--index1--
                sLog &= sList
                '--MsgBox "Table : " + sTableName + vbCrLf + sList

                '-- set up mainParent for Assoc tables--
                '--  ie where primary key is also foreign key (dependent)-
                '--The first foreign key/Primary key points to the parent table--
                sList = "Parent Table(s) are: "
                colParents = New Collection
                If (colPrimaryKey.Count() > 0) And (colForeignKeys.Count() > 0) Then
                    For px = 1 To colPrimaryKey.Count() '--check all primary key cols--
                        s1 = UCase(colPrimaryKey.Item(px))
                        '--  For fx = 1 To colForeignKeys.Count
                        For Each colFieldx In colForeignKeys
                            '--Set colFieldx = colForeignKeys(fx)
                            If UCase(colFieldx.Item("LOCALFIELD")) = s1 Then
                                s2 = colFieldx.Item("FOREIGNTABLE") '--another parent for our table-
                                colParents.Add(s2) '---add parent to coll.-
                                sList = sList & s2 & "; "
                                Exit For '--inner-fx--(First parent is enough)-
                            End If
                        Next colFieldx '--fx
                    Next px '--next primary key col--
                End If
                sLog &= sList & vbCrLf & "= = = = = = = = = = = " & vbCrLf
                '--If (colParents.count > 0) Then Call gLogMsg(sList)
                '-----If colFieldx.count > 0 Then colOtherIndexes.add colFieldx   '--save last index-

                '--search fld list for best name column..--
                sNameCol = ""
                For Each colFieldx In colFields
                    If gbIsText(colFieldx.Item("TYPE_NAME")) Then
                        s1 = colFieldx.Item("NAME") '--get this column name..-
                        If (InStr(UCase(s1), "COMPANY") > 0) Then
                            sNameCol = s1
                        ElseIf (InStr(UCase(s1), "SURNAME") > 0) Then
                            sNameCol = s1
                        ElseIf (InStr(UCase(s1), "NAME") > 0) Then
                            sNameCol = s1
                        ElseIf (InStr(UCase(s1), "DESCR") > 0) Then
                            sNameCol = s1
                        End If '--instr-
                    End If '--text--
                    If sNameCol <> "" Then Exit For '--found something..-
                Next colFieldx '--flds-
                '--save stuff for this table--
                colTable.Add(colTableInfo, "SOURCEINFO")
                colTable.Add(colParents, "PARENTS")
                colTable.Add(colFields, "FIELDS")
                colTable.Add(colPrimaryKey, "PRIMARYKEYS") '-- #3--
                '--4th entry is FOREIGN KEY collection--
                colTable.Add(colForeignKeys, "FOREIGNKEYS") '-- #4--  WILL BE EMPTY at this point..--
                colTable.Add(colOtherIndexes, "OTHERINDEXES") '-- #5--
                colTable.Add(sNameCol, "NAMECOLUMN") '--#6--
                colTable.Add(sAccessVersion, "ACCESSVERSION") '--#7--

                colSqlTables.Add(colTable, UCase(sTableName)) '--save all stuff this table..--
            End If '--system table..-
        Next tx '--table--

        '- Foreign Keys..--
        '-- Note on relations: foreign keys are attached to the real foreign (Primary) table--
        '-- scan all relations--
        '--  invert each relation to attach it to dependent table..-
        '==  Get foreign keys  loop==

        sList = "==All Foreign Keys are: " & vbCrLf
        On Error GoTo GetJet351Info_error2
        '--For Each key1 In table1.Keys

        '-- check all foreign relations .--
        For Each relation1 In db1.Relations
            L1 = relation1.Attributes
            sFtable = relation1.Table '--actual primary table..--
            sTableName = relation1.ForeignTable '--- This is Table with actual foreign key..
            kx = 0 : s1 = ""
            sList = sList & " -- In Table: '" & sTableName & " cols : " & vbCrLf
            If relation1.Fields.Count > 0 Then
                For Each field1 In relation1.Fields '--only saves the FIRST one !!!!
                    kx = kx + 1 '--count keys--
                    If kx = 1 Then
                        sForeignFld = field1.Name '--looking back from dep. table..--
                        sLocalFld = field1.ForeignName '--- col in dep table that has fkey..--
                    End If
                    sList = sList & "       " & sLocalFld & " (REF " & sFtable & ", Fld: " & sForeignFld & ") " & vbCrLf
                Next field1 '--col--
                '--s2 = rs("pktable_name"): s3 = rs("pkcolumn_name")
                '---MsgBox "Found Foreign key:" + vbCrLf + "Table: " + sTableName + vbCrLf + sList

                '--add to key collection for DEP table..-
                colFieldx = New Collection
                colFieldx.Add(sLocalFld, "LOCALFIELD")
                colFieldx.Add(sFtable, "FOREIGNTABLE")
                colFieldx.Add(sForeignFld, "FOREIGNFIELD")
                colForeignKeys.Add(colFieldx) '--add to fkey collection this table--

                colSqlTables.Item(UCase(sTableName))("FOREIGNKEYS").Add(colFieldx)
                '--flag (xref) this as foreign key in relevant col. of DEPendent table--
                '-change flag, add fkey ref in f/key column--
                If sLocalFld <> "" Then '--have cols--
                    colSqlTables.Item(UCase(sTableName))("FIELDS")(UCase(sLocalFld)).Remove("ISFOREIGNKEY") '--remove/add to change--
                    colSqlTables.Item(UCase(sTableName))("FIELDS")(UCase(sLocalFld)).Add(True, "ISFOREIGNKEY") '--remove/add to change--
                    colSqlTables.Item(UCase(sTableName))("FIELDS")(UCase(sLocalFld)).Add(sFtable, "FOREIGNTABLE")
                    colSqlTables.Item(UCase(sTableName))("FIELDS")(UCase(sLocalFld)).Add(sForeignFld, "FOREIGNFIELD")
                End If '--
                '--End If  '--foreign-
                '--End If  '--this table..--
            Else '--no cols..
                sList = sList & " NO COLUMN INFO !!..    " & vbCrLf
            End If

        Next relation1 '---relation..--

        sLog &= sList

        gbGetJet360DbInfo = True
        db1.Close()
        db1 = Nothing

        sLog = sLog & "= = =  Build DAO Jet 3.60 catalogue done.. = = = = " & vbCrLf

        Exit Function

GetJet351Info_error:
        L1 = Err().Number
        MsgBox("Runtime Error in Stage-1 of gbGetJet360DbInfo function.." & vbCrLf & _
                                           "Error is " & L1 & " = " & ErrorToString(L1), MsgBoxStyle.Exclamation)
        Exit Function
GetJet351Info_error2:
        L1 = Err().Number
        MsgBox("Runtime ERROR in Stage-2 of gbGetJet360DbInfo function.." & vbCrLf & _
                                            "Error is " & L1 & " = " & ErrorToString(L1), MsgBoxStyle.Exclamation)
        Exit Function

GetJet351Info_error3:
        L1 = Err().Number
        MsgBox("Runtime Error in Stage-THREE of gbGetJet360DbInfo function.." & vbCrLf & _
                                       "Error is " & L1 & " = " & ErrorToString(L1), MsgBoxStyle.Exclamation)
        Exit Function

    End Function '--getJet351Info--
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '--LOGON/Connect to Retail Manager DB  (Jet) ----
    '--LOGON/Connect to Retail Manager DB  (Jet) ----
    '--LOGON/Connect to Retail Manager DB  (Jet) ----
    '--Jet --
    '--Jet --
    Public Function gbLoginJet(ByRef cnnSQL As OleDbConnection, _
                                ByRef sJetDBname As String, _
                                  ByRef colDBInfo As Collection, _
                                   ByRef sUid As String, _
                                     ByRef sPwd As String, _
                                      ByRef sConnectLog As String) As Boolean

        Dim L1, lCount, lngCnn As Integer
        Dim s2, s1, s3 As String
        Dim sSql As String '--, sJetName As String
        Dim sCnnKey As String
        Dim sName As String
        Dim vName As Object
        Dim bLoggedOn As Boolean
        Dim col1 As Collection
        Dim sConnect, sErrorMsg As String
        Dim bOk As Boolean
        Dim dbJet As DAO.Database '--  to test connection..--
        Dim frmLogin1 As frmLogin

        gbLoginJet = False
        On Error GoTo LoginJet_error

        frmLogin1 = New frmLogin
        frmLogin1.ComputerName = sJetDBname '--default=
        frmLogin1.IsSqlServer = False
        frmLogin1.IsJet = True
        lCount = 0
        bLoggedOn = False

        '-- must be able to set properties BEFORE connecting..-
        cnnSQL = New OleDbConnection
        While (lCount < 5) And (Not bLoggedOn)
            '--frmJobs.labStatus.Caption = ""
            If (sJetDBname = "") Or (sUid = "") Then '--show login form..--
                frmLogin1.ShowDialog()
                If Not frmLogin1.cancelled Then '--ok---
                    '--get data from login form--
                    sJetDBname = frmLogin1.server
                    sUid = frmLogin1.username
                    sPwd = frmLogin1.password
                    '--try connect--
                Else '--cancelled.--
                    sJetDBname = ""
                    lCount = 5 '--exit logon-
                End If '--cancelled--
            End If '--show form..--
            If sJetDBname <> "" Then '--ok-
                '-- test connection with DAO first..--
                '--  NB: for QBPOS.. using "Database Password"  --==01-Oct-2011==
                '-----  KB 209953 ---
                On Error Resume Next
                dbJet = DAODBEngine_definst.Workspaces(0).OpenDatabase(sJetDBname, False, True, "MS Access; PWD=" & sPwd)
                L1 = Err().Number
                If L1 <> 0 Then
                    MsgBox("Error in JetLogon DAO OpenDatabase function.." & vbCrLf & _
                            "   File path was: [" & sJetDBname & "].. " & vbCrLf & _
                            "Error is " & L1 & " = " & ErrorToString(L1))
                    Exit Function
                Else '--ok--
                    '--MsgBox "DB: " + sJetDBname + vbCrLf + "Connected ok with DAO..", vbInformation
                    dbJet.Close()
                End If
                On Error GoTo 0 '== LogonJet_error
                '-- ok connect with oledb..--
                '-->>  this for user-level security..--
                '===  sConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sJetDBname
                '===  sConnect = sConnect + "; Password=" + sPwd + "; User ID=" + sUid

                '---==01-Oct-2011==  this for Database Password security (QBPOS AND RM)....--
                '==sConnect = sConnect + "; Jet OLEDB: Database Password=" + msPwd + "; "
                '==  sConnect = sConnect + " Password=" + msPwd + "; "
                '= cnnSQL.Provider = "Microsoft.Jet.OLEDB.4.0"
                '==cnnSQL.Properties("Jet OLEDB:Database Password").Value = sPwd
                sConnect = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & sJetDBname & "; "
                sConnect &= "Jet OLEDB:Database Password=" & sPwd & "; "
                If mbConnectSql(cnnSQL, sConnect, sErrorMsg) Then
                    bLoggedOn = True
                    '--MsgBox "OLEDB Jet 3.51 Connected ok..", vbInformation
                Else
                    MsgBox("OLEDB Jet 4.0 Login to dataSource has failed." & vbCrLf & sErrorMsg & vbCrLf & _
                            "--  File path was: [" & sJetDBname & "].. " & vbCrLf & vbCrLf & _
                             "Check credentials/permissions before trying again..", MsgBoxStyle.Exclamation)
                    lCount = lCount + 1
                    sUid = "" '--force logon screen..--
                End If '--connected-
            End If '--have server-
            '--Else  '--cancelled
            '--End If  '--not cancelled--
        End While '--logged on-
        '--frmJobs.labStatus.Caption = ""
        If bLoggedOn Then
            sJetDBname = frmLogin1.server
            s1 = LCase(sJetDBname)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '---  use DAO to get schema..--
            bOk = gbGetJet360DbInfo(cnnSQL, sJetDBname, sUid, sPwd, colDBInfo, sConnectLog)
            If bOk Then
                If gbDebug Then MsgBox("Loaded info for DATABASE:  " & sName, MsgBoxStyle.Information)
                '--colDBs.Add colDBInfo, LCase$(sName)
            Else
                MsgBox(" *** ERROR- FAILED to build Jet DB catalogue ==" & vbCrLf & "==" & vbCrLf)
                '--Exit For
            End If '--ok--
            If bOk Then
                '--MsgBox "Jet 3.51 logon completed ok....", vbInformation
                gbLoginJet = True
            End If '--ok-
        Else '--no logon-
            cnnSQL = Nothing
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        dbJet = Nothing

        frmLogin1.Close()
        col1 = Nothing
        Exit Function

LoginJet_error:
        L1 = Err().Number
        MsgBox("VB Runtime Error in 'LoginJet' function.." & vbCrLf & "Error is " & L1 & " = " & ErrorToString(L1))
        Exit Function

    End Function '--logon Jet-
    '= = = = = = = =

    '== end module..==
End Module