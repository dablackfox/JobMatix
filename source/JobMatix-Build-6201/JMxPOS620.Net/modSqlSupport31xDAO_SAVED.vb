Option Strict Off
Option Explicit On
Imports System.io
Imports System.Collections
Imports System.Data.Sql
Imports VB6 = Microsoft.VisualBasic
'= Imports System.Data.sqlclient
Imports System.Data.OleDb

Module modSqlSupport

    '-- sql subs for ADO.net--
    '--  grh POS 24-Feb-2014--

    '-- PO3- Ugrade-- 12-Sep-2014 --
    '--  NOW using System.Data.OleDb
    '--  To be connection compatible with JobMatix v3.1 and Jet/OLEDB..-
    '==
    '= = = =  = = = = = = ==  = = = == 

    '-- ADODB data Types--
    '-- ADODB data Types--

    Public Const ADODB_DataTypeEnum_adBigInt = 20
    Public Const ADODB_DataTypeEnum_adBinary = 128
    Public Const ADODB_DataTypeEnum_adBoolean = 11
    Public Const ADODB_DataTypeEnum_adChar = 129
    Public Const ADODB_DataTypeEnum_adDBTimeStamp = 135
    Public Const ADODB_DataTypeEnum_adNumeric = 131
    Public Const ADODB_DataTypeEnum_adDouble = 5
    Public Const ADODB_DataTypeEnum_adVarBinary = 204
    Public Const ADODB_DataTypeEnum_adInteger = 3
    Public Const ADODB_DataTypeEnum_adCurrency = 6
    Public Const ADODB_DataTypeEnum_adWChar = 130
    Public Const ADODB_DataTypeEnum_adSingle = 4
    Public Const ADODB_DataTypeEnum_adSmallInt = 2
    Public Const ADODB_DataTypeEnum_adVariant = 12
    Public Const ADODB_DataTypeEnum_adGUID = 72

    '--these for matching DAO--
    Public Const ADODB_DataTypeEnum_adUnsignedTinyInt = 17
    Public Const ADODB_DataTypeEnum_adVarChar = 200
    Public Const ADODB_DataTypeEnum_adDate = 7
    Public Const ADODB_DataTypeEnum_adDecimal = 14
    Public Const ADODB_DataTypeEnum_adLongVarBinary = 205
    Public Const ADODB_DataTypeEnum_adLongVarWChar = 203
    Public Const ADODB_DataTypeEnum_adVarWChar = 202
    Public Const ADODB_DataTypeEnum_adLongVarChar = 201
    Public Const ADODB_DataTypeEnum_adTinyInt = 16

    Public Const ADODB_DataTypeEnum_adUnsignedBigInt = 21
    Public Const ADODB_DataTypeEnum_adUnsignedInt = 19
    Public Const ADODB_DataTypeEnum_adUnsignedSmallInt = 18


    '= = = = = = = = = = = =
    '== grh 12-Feb-2013= Build-3072/3073= --
    Private msLastSqlErrorMessage As String = ""


    '-- SET THESE at START UP ----
    '-- SET THESE at START UP ----
    '-- SET THESE at START UP ----
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = -1
    Private mIntJobMatixDBid As Integer = -1

    Private mbIsSqlAdmin As Boolean = False

    '= = = = = = = = = = = = = = =  == =
    '-===FF->

    '---- all EX  xbsWizard subs---


    '-- 3072=  gsGetLastError msg --

    Public Function gsGetLastSqlErrorMessage() As String

        gsGetLastSqlErrorMessage = msLastSqlErrorMessage
    End Function '--gsGetLast-
    '= = = = = = = = = = = =


    '-- conversions --
    '--  clean up sql string data ..--
    Public Function gsFixSqlStr(ByRef sInstr As String) As String

        gsFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '-===FF->

    '--   S q l V e r s i o n --

    Public Sub gbSetupSqlVersion(ByRef cnnSql As OleDbConnection)
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim dataTable1 As New DataTable
        Dim iPos, lngResult As Integer
        Dim sErrors As String

        '--  G e t S q l S e r v e r V e r s i o n --
        msSqlVersion = "0.0.0.0"
        lngResult = glExecSP(cnnSql, "xp_msver", "", sErrors, rs1)
        If lngResult <> 0 Then '--failed.-
            MsgBox("Failed to get SQL Version.." & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
        Else '--ok.-
            If Not (rs1 Is Nothing) Then
                Try
                    dataTable1.Load(rs1)
                    If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then '--ie.. not empty..-
                        '==If rs1.BOF Then rs1.MoveFirst() '-- MUST do it this way for r/sets from execSP..-
                        For Each datarow1 As DataRow In dataTable1.Rows
                            If (LCase(datarow1.Item("name")) = "productversion") Then
                                msSqlVersion = datarow1.Item("Character_Value")
                                Exit For
                            End If  '--found name-
                        Next datarow1
                    End If '--empty.-
                Catch ex As Exception
                    MsgBox("Failed to load SQL Version dataTable.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If '--nothing..-
        End If '-result.--
        '-- extract Major version-
        mlngSqlMajorVersion = 8 '-- Rev-2912.. sql 2000..-
        iPos = InStr(msSqlVersion, ".") '--find major version.-
        If (iPos > 1) Then
            mlngSqlMajorVersion = CInt(Left(msSqlVersion, iPos - 1))
        End If
    End Sub  '-- gsSqlVersion--
    '= = = = = = = = = = = = = = =  =


    '--   S q l V e r s i o n --
    Public Sub gbSetSqlVersion(ByVal sVersion As String)
        Dim iPos As Integer

        msSqlVersion = sVersion
        iPos = InStr(msSqlVersion, ".") '--find major version.-
        mlngSqlMajorVersion = 8 '-- Rev-2912.. sql 2000..-
        If (iPos > 1) Then
            mlngSqlMajorVersion = CInt(Left(msSqlVersion, iPos - 1))
        End If

    End Sub  '-- gsSqlVersion--
    '= = = = = = = = = = = = = = =  =

    Public Function gsGetSqlVersion() As String

        gsGetSqlVersion = msSqlVersion

    End Function  '-- gsSqlVersion--
    '= = = = = = = = = = = = = = =  =

    '--  set/get our db_id..
    '--  set/get our db_id..
    '--  set/get our db_id..
    Public Sub gSetJobMatixDBid(ByVal intId As Integer)

        mIntJobMatixDBid = intId

    End Sub '-gbIntJobMatixDBid.-
    '= = = = = = = = = = =

    Public Function gbIntJobMatixDBid() As Integer

        gbIntJobMatixDBid = mIntJobMatixDBid

    End Function '-gbIntJobMatixDBid.-
    '= = = = = = = = = = =


    '-- IF Current SERVER Instance Version-
    '--        is SQL-Server 2005 or later.--

    Public Function gbIsSqlServer2005Plus() As Boolean

        gbIsSqlServer2005Plus = (mlngSqlMajorVersion >= 9)  '-- 9=2005,  10=2008..--

    End Function '-2005Plus.-
    '= = = = = = = = = = = =  

  '-===FF->

    '-- SET Current User if SQL Admin.--
    '-- SET Current User if SQL Admin.--

    Public Sub gbSetIsSqlAdmin(ByVal bIsAdmin As Boolean)

        mbIsSqlAdmin = bIsAdmin
    End Sub '- set admin.-
    '= = = = = = = = = = =
    '-- IF Current User is SQL Admin.--
    '-- IF Current User is SQL Admin.--

    Public Function gbIsSqlAdmin() As Boolean

        gbIsSqlAdmin = mbIsSqlAdmin
    End Function '-admin.-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--convert numeric data for sorted display..-

    '--  This must have ADO-Type input only !!--

    Public Function gsFormat(ByRef v1 As Object, _
                             ByVal intADO_Type As Integer, _
                              ByVal lSize As Integer) As String
        Dim sResult As String
        Dim sType As String '--sql type--

        sResult = ""
        If Not IsDBNull(v1) Then
            sResult = CStr(v1) '--for strings..-
            sType = UCase(gsGetSqlType(intADO_Type, lSize))
            If (sType = "MONEY") Or (sType = "SMALLMONEY") Then '--currency..-
                If IsNumeric(v1) Then   '=3059.1=
                    sResult = New String(" ", 9)
                    sResult = RSet(FormatCurrency(v1, 2), Len(sResult))
                End If  '--is numeric-
            ElseIf gbIsNumericType(sType) Then
                If IsNumeric(v1) Then   '=3059.1=
                    '== sResult = New String(" ", 5)
                    '= sResult = RSet(VB6.Format(v1, "    0"), Len(sResult))
                    sResult = v1.ToString
                End If  '--is numeric v1-
            ElseIf gbIsDate(sType) Then
                If IsDate(v1) Then   '=3059.1=
                    sResult = Format(CDate(v1), "yyyy-MM-dd")
                End If  '--is date v1-
            End If '--type..-
        End If '--null..-
        gsFormat = sResult

    End Function '--convert--
    '-===FF->


    '--determine if date type--

    Public Function gbIsDate(ByVal sSqlType As String) As Boolean

        Dim s1 As String
        s1 = UCase(sSqlType)
        If (s1 = "DATETIME") Then
            gbIsDate = True
        Else
            gbIsDate = False
        End If

    End Function '--isdate--
    '= = = = =  = = =  = =

    '--determine if text type--

    Public Function gbIsText(ByVal sSqlType As String) As Boolean

        Dim s1 As String
        Dim ix As Integer

        s1 = UCase(sSqlType)
        '--drop length (LL) if appended--
        ix = InStr(s1, "(")
        If (ix > 1) Then s1 = Left(s1, ix - 1) '--drop parenthesised length--

        If (s1 = "CHAR") Or (s1 = "NCHAR") Or (s1 = "VARCHAR") Or _
                                 (s1 = "NVARCHAR") Or (s1 = "TEXT") Or (s1 = "NTEXT") Then
            gbIsText = True
        Else
            gbIsText = False
        End If

    End Function '--isText--
    '= = = = =  = = =  = =

    '--determine if NUMERIC sql type--

    Public Function gbIsNumericType(ByVal sSqlType As String) As Boolean
        Dim pos As Short
        Dim s1 As String
        pos = InStr(1, sSqlType, " ") '--find end of actual tyoe--
        s1 = UCase(sSqlType)
        If (pos > 1) Then s1 = UCase(Left(sSqlType, pos - 1)) '--drop IDENTITY-
        If (s1 = "INT") Or (s1 = "BIGINT") Or (s1 = "DECIMAL") Or _
                         (s1 = "SMALLINT") Or (s1 = "TINYINT") Or _
                              (s1 = "BIT") Or (s1 = "FLOAT") Or (s1 = "REAL") Or _
                                   (s1 = "MONEY") Or (s1 = "SAMLLMONEY") Or (s1 = "NUMERIC") Then
            gbIsNumericType = True
        Else
            gbIsNumericType = False
        End If
    End Function '--isNumeric--
    '= = = = =  = = =  = =
    '-===FF->

    '=
    '-- convert ADO type to SQL type--
    Public Function gsGetSqlType(ByVal intType As Integer, _
                                   ByVal lSize As Integer) As String
        Dim j, i, k As Integer
        Dim sT As String

        sT = "varChar (16)" '--default--
        Select Case intType
            Case ADODB_DataTypeEnum_adBigInt : sT = "BIGINT"
            Case ADODB_DataTypeEnum_adBinary : sT = "BINARY"
            Case ADODB_DataTypeEnum_adBoolean : sT = "BIT"
            Case ADODB_DataTypeEnum_adChar : sT = "CHAR(" & Trim(CStr(lSize)) & ")"
            Case ADODB_DataTypeEnum_adCurrency : sT = "MONEY"
            Case ADODB_DataTypeEnum_adDate : sT = "datetime"
            Case ADODB_DataTypeEnum_adDBTimeStamp : sT = "DATETIME"
            Case ADODB_DataTypeEnum_adDecimal : sT = "DECIMAL"
            Case ADODB_DataTypeEnum_adDouble : sT = "FLOAT"
            Case ADODB_DataTypeEnum_adGUID : sT = "UNIQUEIDENTIFIER"
            Case ADODB_DataTypeEnum_adInteger : sT = "INT"
            Case ADODB_DataTypeEnum_adLongVarBinary : sT = "VARBINARY" '--if L>8000 then use IMAGE --
            Case ADODB_DataTypeEnum_adLongVarChar : sT = "TEXT"
            Case ADODB_DataTypeEnum_adLongVarWChar : sT = "NTEXT" '--dbase memo--
            Case ADODB_DataTypeEnum_adNumeric : sT = "NUMERIC"
            Case ADODB_DataTypeEnum_adSingle : sT = "REAL"
            Case ADODB_DataTypeEnum_adSmallInt : sT = "SMALLINT"
            Case ADODB_DataTypeEnum_adTinyInt : sT = "TINYINT"
                '--unsigned not defined in sql-
            Case ADODB_DataTypeEnum_adUnsignedBigInt : sT = "BIGINT"
            Case ADODB_DataTypeEnum_adUnsignedInt : sT = "INT"
            Case ADODB_DataTypeEnum_adUnsignedSmallInt : sT = "SMALLINT"
            Case ADODB_DataTypeEnum_adUnsignedTinyInt : sT = "TINYINT"
                '----
            Case ADODB_DataTypeEnum_adVariant : sT = "SQL_VARIANT"
                '--Case adBinary:     sT = "TIMESTAMP"         '--???--
                '--Case adVarBinary: sT = "TINYINT"
            Case ADODB_DataTypeEnum_adVarBinary : sT = "VARBINARY"
            Case ADODB_DataTypeEnum_adVarChar : sT = "VARCHAR(" & Trim(CStr(lSize)) & ")"
            Case ADODB_DataTypeEnum_adVarWChar : sT = "nvarChar(" & Trim(CStr(lSize)) & ")"
            Case ADODB_DataTypeEnum_adWChar : sT = "NCHAR"
            Case Else
        End Select
        gsGetSqlType = sT

    End Function '--sql-type--
    '= = = =  = = = =
    '-===FF->

    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--
    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--
    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--

    '=POS31= Public Function glTypeDAOtoADO(ByRef vType As Object) As Integer
    '=POS31= Dim lngADO As Integer

    '=POS31=   Select Case vType
    '=POS31=      Case DAO.DataTypeEnum.dbBigInt : lngADO = ADODB_DataTypeEnum_adBigInt
    '=POS31=      Case DAO.DataTypeEnum.dbBinary : lngADO = ADODB_DataTypeEnum_adBinary
    '=POS31=      Case DAO.DataTypeEnum.dbBoolean : lngADO = ADODB_DataTypeEnum_adBoolean
    '=POS31=      Case DAO.DataTypeEnum.dbByte : lngADO = ADODB_DataTypeEnum_adUnsignedTinyInt
    '=POS31=      Case DAO.DataTypeEnum.dbChar : lngADO = ADODB_DataTypeEnum_adVarChar
    '=POS31=      Case DAO.DataTypeEnum.dbCurrency : lngADO = ADODB_DataTypeEnum_adCurrency
    '=POS31=      Case DAO.DataTypeEnum.dbDate : lngADO = ADODB_DataTypeEnum_adDate
    '=POS31= '-------Case db:      lngADO = adDBTimeStamp: sT = "DATETIME"
    '=POS31=      Case DAO.DataTypeEnum.dbDecimal : lngADO = ADODB_DataTypeEnum_adDecimal
    '=POS31=      Case DAO.DataTypeEnum.dbDouble : lngADO = ADODB_DataTypeEnum_adDouble
    '=POS31=      Case DAO.DataTypeEnum.dbFloat : lngADO = ADODB_DataTypeEnum_adDouble
    '=POS31=      Case DAO.DataTypeEnum.dbGUID : lngADO = ADODB_DataTypeEnum_adGUID
    '=POS31=      Case DAO.DataTypeEnum.dbInteger : lngADO = ADODB_DataTypeEnum_adInteger
    '=POS31=      Case DAO.DataTypeEnum.dbLong : lngADO = ADODB_DataTypeEnum_adInteger
    '=POS31=      Case DAO.DataTypeEnum.dbLongBinary : lngADO = ADODB_DataTypeEnum_adLongVarBinary
    '=POS31= '----Case db:      lngADO = adLongVarChar: sT = "TEXT"
    '=POS31=      Case DAO.DataTypeEnum.dbMemo : lngADO = ADODB_DataTypeEnum_adLongVarWChar
    '=POS31=      Case DAO.DataTypeEnum.dbNumeric : lngADO = ADODB_DataTypeEnum_adDouble
    '=POS31=      Case DAO.DataTypeEnum.dbSingle : lngADO = ADODB_DataTypeEnum_adSingle
    '=POS31=      Case DAO.DataTypeEnum.dbText : lngADO = ADODB_DataTypeEnum_adVarWChar
    '=POS31=      Case DAO.DataTypeEnum.dbTime : lngADO = ADODB_DataTypeEnum_adDate
    '=POS31=      Case DAO.DataTypeEnum.dbTimeStamp : lngADO = ADODB_DataTypeEnum_adDate
    '=POS31=      Case DAO.DataTypeEnum.dbVarBinary : lngADO = ADODB_DataTypeEnum_adVarBinary

    '=POS31=      Case Else : lngADO = ADODB_DataTypeEnum_adVarChar
    '=POS31=    End Select

    '=POS31=    glTypeDAOtoADO = lngADO
    '=POS31= End Function '--  glTypeDAOtoADO --
    '= = = =  = = = =
    '-===FF->

    '-- -Convert sql type_name to ADO type==
    '-- -Convert sql type_name to ADO type==

    Public Function giGetADOdataType(ByVal sSqlType As String) As Short

        Dim intADO As Short

        intADO = 128      '== ADODB.DataTypeEnum.adBinary '--default-
        Select Case UCase(sSqlType)
            Case "BIGINT" : intADO = ADODB_DataTypeEnum_adBigInt
            Case "BINARY" : intADO = ADODB_DataTypeEnum_adBinary
            Case "BIT" : intADO = ADODB_DataTypeEnum_adBoolean
            Case "CHAR" : intADO = ADODB_DataTypeEnum_adChar
            Case "DATETIME" : intADO = ADODB_DataTypeEnum_adDBTimeStamp
            Case "DECIMAL" : intADO = ADODB_DataTypeEnum_adNumeric
            Case "FLOAT" : intADO = ADODB_DataTypeEnum_adDouble
            Case "IMAGE" : intADO = ADODB_DataTypeEnum_adVarBinary
            Case "INT" : intADO = ADODB_DataTypeEnum_adInteger
            Case "MONEY" : intADO = ADODB_DataTypeEnum_adCurrency
            Case "NCHAR" : intADO = ADODB_DataTypeEnum_adWChar
            Case "NTEXT" : intADO = ADODB_DataTypeEnum_adWChar
            Case "NUMERIC" : intADO = ADODB_DataTypeEnum_adNumeric
            Case "NVARCHAR" : intADO = ADODB_DataTypeEnum_adWChar
            Case "REAL" : intADO = ADODB_DataTypeEnum_adSingle
                '--Case "SMALLDATETIME":   intADO = adTimeStamp
            Case "SMALLINT" : intADO = ADODB_DataTypeEnum_adSmallInt
            Case "SMALLMONEY" : intADO = ADODB_DataTypeEnum_adCurrency
            Case "SQL_VARIANT" : intADO = ADODB_DataTypeEnum_adVariant
            Case "SYSNAME" : intADO = ADODB_DataTypeEnum_adWChar
            Case "TEXT" : intADO = ADODB_DataTypeEnum_adChar
            Case "TIMESTAMP" : intADO = ADODB_DataTypeEnum_adBinary
            Case "TINYINT" : intADO = ADODB_DataTypeEnum_adVarBinary
            Case "UNIQUEIDENTIFIER" : intADO = ADODB_DataTypeEnum_adGUID
            Case "VARBINARY" : intADO = ADODB_DataTypeEnum_adVarBinary
            Case "VARCHAR" : intADO = ADODB_DataTypeEnum_adChar

        End Select '--sqltype--

        giGetADOdataType = intADO

    End Function '--getADO--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- -Convert .Net DataType type_name to ADO, sql types==

    Public Function gbConvertDotNetDataType(ByRef column1 As DataColumn, _
                                             ByRef intADO_type As Integer, _
                                             ByRef sSqlType As String) As Boolean
        gbConvertDotNetDataType = False
        Try
            With column1
                If .DataType Is GetType(System.Int32) Then
                    sSqlType = "INT"
                    '== intADO_type = 3
                ElseIf .DataType Is GetType(System.Int64) Then
                    sSqlType = "SMALLINT"
                ElseIf .DataType Is GetType(System.Int16) Then
                    sSqlType = "BIGINT"
                ElseIf .DataType Is GetType(System.Decimal) Then
                    sSqlType = "MONEY"
                ElseIf .DataType Is GetType(System.Byte) Then
                    sSqlType = "BINARY"
                ElseIf .DataType Is GetType(System.Int16) Then
                    sSqlType = "SMALLINT"
                ElseIf .DataType Is GetType(System.DateTime) Then
                    sSqlType = "DATETIME"
                ElseIf .DataType Is GetType(System.String) Then
                    sSqlType = "NVARCHAR"
                Else
                    sSqlType = "sql_variant"
                End If
                '--etc-

            End With '= column1
            intADO_type = giGetADOdataType(sSqlType)

            gbConvertDotNetDataType = True
        Catch ex As Exception
            MsgBox("ERROR in 'gbConvertDotNetDataType' function" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-gbConvertDotNetDataType-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->


    '--TEXT SEARCH request-- ==13-Jun-2010==
    '--TEXT SEARCH request--
    '-- "asColumns" is an array of strings (column names..) --

    Public Function gbMakeTextSearchSql(ByVal sArgText As String, _
                                         ByRef asColumns As Object) As String
        Dim asArgs As Object
        Dim sSql As String
        Dim s1, s2 As String
        Dim cx, ix As Integer

        gbMakeTextSearchSql = ""

        '-- build query to srch all text cols..--
        '=======sSearchArg = Trim(txtSearch.Text)    '==UCase(request.Form("selTextSearchArg"))
        sSql = "" '---"SELECT * FROM " + msProductTableName + " WHERE "
        s1 = "" : s2 = "" '--result-
        '-- arg can be multiple tokens..--
        asArgs = Split(Trim(sArgText))
        '====Set table1 = mColSqlDBInfo("JOBS")   '--- mCat1.Tables(msProductTableName)
        cx = 0

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        If (Not IsNothing(asColumns)) And (Not IsNothing(asArgs)) Then '--we have some text cols..--
            For ix = 0 To UBound(asArgs) '--for each arg fragment..--
                If (ix > 0) Then sSql = sSql & " AND "
                s1 = "" '-- sub-clause for this Arg..
                If (UBound(asColumns) > 0) Then s1 = s1 & "(" '-- for multiple columns..--
                For cx = 0 To UBound(asColumns) '---Each column1 In table1.Columns
                    s2 = asColumns(cx)
                    '---If gbIsText(gsGetSqlType(column1.Type, column1.DefinedSize)) Then  '-- is text col..-
                    If (cx > 0) Then s1 = s1 & " OR "
                    '-- all cols are in OR relation with this Arg...--
                    s1 = s1 & "(" & s2 & " LIKE '%" & gsFixSqlStr(CStr(asArgs(ix))) & "%' )"
                Next cx '--column-
                If (UBound(asColumns) > 0) Then s1 = s1 & ")"
                sSql = sSql & s1
            Next ix
        Else '--no text cols..  no search..
        End If '--text--
        gbMakeTextSearchSql = sSql
        '--add srch args if any..-

    End Function '--make srch..-
    '= = = = = = = =
    '-===FF->

    '==    =3083.210= Add function 'gbSQL_Enumerate_Main' ----
    '==  DISCOVER SQL serverinstances..--
    '==  DISCOVER SQL serverinstances..--

    '-- Dim dataTable As System.Data.DataTable = instance.GetDataSources()
    '-- The table returned from the method call contains the following columns,
    '--         all of which contain string values:

    '-- Column  Description  
    '-- ServerName 
    '--  Name of the server.

    '-- InstanceName 
    '--  Name of the server instance.  
    '--         Blank if the server is running as the default instance.

    '-- IsClustered 
    '--  Indicates whether the server is part of a cluster.

    '-- Version 
    '--  Version of the server 
    '--         (8.00.x for SQL Server 2000, and 9.00.x for SQL Server 2005).

    Public Function gbSQL_Enumerate_Main(ByRef ColSqlServers As Collection) As Boolean
        ' Retrieve the enumerator instance and then the data.
        Dim enumInstance1 As SqlDataSourceEnumerator = SqlDataSourceEnumerator.Instance
        Dim table1 As System.Data.DataTable
        Dim colServer As Collection
        Dim rx As Integer = 0
        Dim s1, s2, s3 As String
        '== ' Display the contents of the table.
        '== DisplayData(table)
        gbSQL_Enumerate_Main = False
        ColSqlServers = New Collection

        Try
            table1 = enumInstance1.GetDataSources()
            For Each row As DataRow In table1.Rows
                s1 = IIf((Not VB6.IsDBNull(row("ServerName"))), row("ServerName"), "")
                s2 = IIf((Not VB6.IsDBNull(row("InstanceName"))), row("InstanceName"), "")
                s3 = IIf((Not VB6.IsDBNull(row("Version"))), row("Version"), "")
                '=colServer.Add(row("InstanceName"), "InstanceName")
                If (s1 <> "") And (Not IsDBNull(row("Version"))) Then  '-has version-
                    rx += 1  '--count rows..-
                    colServer = New Collection
                    colServer.Add(s1, "ServerName")
                    colServer.Add(s2, "InstanceName")
                    colServer.Add(s3, "Version")
                    ColSqlServers.Add(colServer)
                    '== txtSystemInfo.Text = txtSystemInfo.Text & rx & ": "
                    '== For Each col As DataColumn In table.Columns
                    '=    Console.WriteLine("{0} = {1}", col.ColumnName, row(col))
                    '==   txtSystemInfo.Text = txtSystemInfo.Text & col.ColumnName & "=" & row(col) & "; "
                    '==   txtSystemInfo.SelectionStart = txtSystemInfo.TextLength
                    '==   txtSystemInfo.SelectionLength = 0
                    '== Next '--col-
                    '== txtSystemInfo.Text = txtSystemInfo.Text & vbCrLf
                End If '-has version-
            Next '--row-
            gbSQL_Enumerate_Main = True

        Catch ex As Exception
            '--error-
            MsgBox("Error searching SQL instances.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
        End Try
        '== Console.WriteLine("Press any key to continue.")
        '== Console.ReadKey()
    End Function  '-- SQL_Enumerate-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '= = = =  c o n n e c t = = = =
    '= = = =  c o n n e c t = = = =

    '== Public Function gbConnectSql(ByRef cnnSQL As SqlConnection, _
    '==                            ByVal sConnect As String) As Boolean

    Public Function gbConnectSql(ByRef cnnSQL As OleDbConnection, _
                                   ByVal sConnect As String) As Boolean

        Dim s1, s2, msg As String

        msLastSqlErrorMessage = ""
        gbConnectSql = False
        If (cnnSQL Is Nothing) Then cnnSQL = New OleDbConnection
        '==3072== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '==On Error Resume Next
        Try
            cnnSQL.ConnectionString = sConnect
            cnnSQL.Open()
            msg = "Connected ok to database.." & vbCrLf
            msg = msg & "   ConnectStr.=" & sConnect & vbCrLf
            '--msg = msg & "   Conn State= " + gsGetState(mCnnSql.State)
            '--MsgBox msg, vbInformation, "xbsWizard Main"
            gbConnectSql = True
            '--MsgBox msg   '--gAdvise msg
            '--txtMessages.Text = txtMessages.Text + vbCrLf + msg + vbCrLf

        Catch ex As Exception
            msg = "Failed Connect to Sql Server.." & vbCrLf
            msg = msg & "Error: " & ex.Message & vbCrLf
            msg = msg & "connect string=<" & sConnect & ">"
            s2 = msg & vbCrLf '== & "SQL-Provider errors are:" & vbCrLf & s1
            '== If gbDebug Then MsgBox(s2, MsgBoxStyle.Critical, "Sql Connect..")
            msLastSqlErrorMessage = s2
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, s2 & vbCrLf & "-- end of error msg.--")
            End If '--log--

        End Try
    End Function '--connect--
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Get SQL Select ANY Value Type (No TRANSACTION current).
    '--==  -- (cmd.getScalar)--
    '-- http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.executescalar(v=vs.90).ASPX

    Public Function gbGetSqlScalarValue(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSqlSelect As String, _
                                           ByRef objResult As Object) As Boolean
        Dim sqlCmd1 As OleDbCommand  '== SqlCommand
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbGetSqlScalarValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSqlSelect, cnnSql)  '== SqlCommand(sSqlSelect, cnnSql)
            objResult = sqlCmd1.ExecuteScalar
            gbGetSqlScalarValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSqlSelect & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarValue-
    '= = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Get Select INTEGER Value (No TRANSACTION current).
    '--==  -- (cmd.getScalar)--

    Public Function gbGetSqlScalarIntegerValue(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSql As String, _
                                           ByRef intResult As Integer) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbGetSqlScalarIntegerValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)  '= SqlCommand(sSql, cnnSql)
            intResult = sqlCmd1.ExecuteScalar
            gbGetSqlScalarIntegerValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Get Select INTEGER Value DURING SQL TRANSACTION.-
    '--==  -- (cmd.getScalar)--
    '--  IF in Transaction, RollBack in the event of failure..-

    Public Function gbGetSqlScalarIntegerValue_Trans(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSql As String, _
                                          ByVal bIsTransaction As Boolean, _
                                           ByRef sqlTran1 As OleDbTransaction, _
                                           ByRef intResult As Integer) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbGetSqlScalarIntegerValue_Trans = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)  '=SqlCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intResult = sqlCmd1.ExecuteScalar
            gbGetSqlScalarIntegerValue_Trans = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue_TRANS-
    '= = = = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Execute Command -
    '---  (DOES NOT start another transaction..)--

    Public Function gbExecuteCmd(ByRef cnnUserDB As OleDbConnection, _
                                      ByVal sSql As String, _
                                        ByRef lAffected As Integer, _
                                         ByRef sErrorMsg As String) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String
        Dim lRecordsAff As Integer
        Dim lError As Integer

        '== On Error GoTo GetRst_Error
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql

        msLastSqlErrorMessage = ""
        '---DEFAULT timeout is 30 secs--
        sErrorMsg = ""
        Try
            lRecordsAff = cmd1.ExecuteNonQuery
            lAffected = lRecordsAff '--return result--
            gbExecuteCmd = True
        Catch ex As Exception
            lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbExecuteCmd:  Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            gbExecuteCmd = False
        End Try

    End Function  '-gbExecuteCmd-
    '= = = = = = = = = = = = = = = =
    '-== = =
    '-===FF->

    '-- NEW- Execute SQL Command..--
    '-- Ex POS3- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Public Function gbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            gbExecuteSql = True   '--ok--
            '== MsgBox("Sql exec ok. " & intAffected & " records affected..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbExecuteSql: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-gbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '---EXEC stored procedure--
    '---  ***  NOTE: The Command object is not safe for scripting. !!--
    '-------see ADO cmd object doco--
    '-=Exec Stored procedure==

    Public Function glExecSP(ByRef cnn1 As OleDbConnection, _
                              ByVal sProcName As String, _
                               ByVal sParms As String, _
                                ByRef sError As String, _
                                ByRef rsResults As OleDbDataReader) As Integer
        Dim lResult As Integer
        '==dim rs as ADODB.recordset
        Dim cmd1 As OleDbCommand
        '==dim sError as string, msg as string
        Dim s1, s2 As String
        Dim lErrCode, iCount As Integer
        Dim iPos As Short
        Dim pm1 As OleDbParameter
        Dim sParamList As String
        Dim sParamName, sParam, sParamValue As String

        msLastSqlErrorMessage = ""
        cmd1 = New OleDbCommand
        '--call stored procedure to disable site/content--
        cmd1.CommandTimeout = 60
        cmd1.CommandText = sProcName '---"sp_ECB_SiteInjunction"   '--sp name--
        cmd1.CommandType = CommandType.StoredProcedure '== ADODB.CommandTypeEnum.adCmdStoredProc
        '= cmd1.NamedParameters = True

        '==FIRST add return code parameter====
        '== pm1 = cmd1.CreateParameter("Return", ADODB.DataTypeEnum.adInteger, _
        '==                                      ADODB.ParameterDirectionEnum.adParamReturnValue, , 0)
        '== cmd1.Parameters.Append(pm1)

        pm1 = cmd1.Parameters.Add("RETURN_VALUE", SqlDbType.Int)
        pm1.Direction = ParameterDirection.ReturnValue
        pm1.Value = 0

        '--do input parameters--
        sParamList = sParms : iCount = 0
        '-- list is like << x=a & y=b etc >>  ==
        '------Note: String arg values must be in SINGLE quotes.===
        '==DO NOT alter case of field args !!  ===
        While (Len(sParamList) > 0)
            iPos = InStr(sParamList, "&")
            If (iPos = 0) Then '--last-
                sParam = Trim(sParamList)
                sParamList = ""
            Else
                sParam = Trim(Left(sParamList, iPos - 1))
                sParamList = Trim(Mid(sParamList, iPos + 1))
            End If
            iPos = InStr(sParam, "=") '--split lhs/rhs--
            If (iPos = 0) Then
                sParamName = Trim(sParam) : sParamValue = ""
                '==Call oHTTPRequest.AddPostData(sGetParamName)
            Else
                sParamName = Trim(Left(sParam, iPos - 1))
                sParamValue = Trim(Mid(sParam, iPos + 1))
                '==Call oHTTPRequest.AddPostData(sGetParamName, sGetParamValue)
            End If
            '==Call oFormParser.setInputData(sParamName, sParamValue)
            If Len(sParamName) > 0 Then
                iCount = iCount + 1
                If (Left(sParamValue, 1) = "'") Then '--char parm--
                    s1 = Mid(sParamValue, 2, Len(sParamValue) - 2) '--drop quotes==
                    pm1 = cmd1.Parameters.Add(sParamName, SqlDbType.NVarChar, 12)
                    pm1.Value = s1
                Else '--numeric-
                    pm1 = cmd1.Parameters.Add(sParamName, SqlDbType.Int)
                    If IsNumeric(sParamValue) Then
                        pm1.Value = CInt(sParamValue)
                    Else
                        pm1.Value = 0
                    End If
                End If
            End If '--len--
        End While '--getParam-
        lResult = 0
        '--set connection AFTER creating parms == (stops auto-refresh)==
        '== cmd1.ActiveConnection = cnn1
        cmd1.Connection = cnn1
        Try
            rsResults = cmd1.ExecuteReader
            lResult = CInt(cmd1.Parameters(0).Value) '===("Return"))
            glExecSP = 0
        Catch ex As Exception
            lResult = CInt(cmd1.Parameters(0).Value) '===("Return"))
            sError = "Failed Executing " & sProcName & vbCrLf & _
                         "Parms: " & sParms & vbCrLf & _
                            "Error Msg is :" & vbCrLf & ex.Message & vbCrLf & "--  end of msg.--" & vbCrLf
            lResult = -lErrCode
            Call gbLogMsg(gsErrorLogPath, sError)
            msLastSqlErrorMessage = sError
            '==gosub buildErrorResult
            glExecSP = -2 '== lResult
        End Try
        cmd1 = Nothing
    End Function '--execSP-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '--get list of User-defined types--

    Public Function gbGetUserTypes(ByRef cnn1 As OleDbConnection, _
                                     ByVal sDBName As String, _
                                      ByRef colTypeList As Collection) As Boolean
        Dim j, i, k As Integer
        Dim s1, s2 As String
        Dim sList, sErrors As String
        Dim rs As OleDbDataReader '== ADODB.Recordset
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbGetUserTypes = False
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, k, sErrors) Then
            If gbDebug Then MsgBox("GetUserTypes: Failed sql: " & s1)
            msLastSqlErrorMessage = "GetUserTypes: Failed sql: " & s1
        End If
        sList = "Found User-types: " & vbCrLf
        lResult = glExecSP(cnn1, "sp_help", "", sErrors, rs)
        If (lResult <> 0) Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            '--check recordset--
            colTypeList = New Collection
            While Not (rs Is Nothing)
                If rs.HasRows Then
                    '--if not empty, current pos is First record--(seeADO)_-
                    While rs.Read  '== Not rs.EOF
                        If (LCase(rs.GetName(0)) = "type_name") Then '--r/set for user types--
                            s1 = rs.Item("type_name") '--get user type name--
                            s2 = rs.Item("storage_type") '--underlying SQL-server type name-
                            colTypeList.Add(s2, UCase(s1)) '--user type is Key--
                            sList = sList & s1 & "(" & s2 & ")" & vbCrLf
                        Else  '--not user-types rset..
                            Exit While
                        End If
                        '== rs.MoveNext()
                    End While
                    gbGetUserTypes = True
                    If gbDebug Then MsgBox(sList, MsgBoxStyle.Information) '--test--
                    '== End If '--not empty-
                End If  '--has rows..
                If Not rs.NextResult() Then Exit While '==rs = rs.NextRecordset
            End While  '--nothing
        End If '--ok--
        If Not (rs Is Nothing) Then rs.Close()
        rs = Nothing
    End Function '--getUserTypes--
    '= = = =  = = = =
    '= = = =  = = = =
    '-===FF->

    '-- SQL functions==11June2001==
    '---==Get result Set ==
    '--Caller supplies full select string--

    Public Function gbGetReader(ByRef cnnUserDB As OleDbConnection, _
                               ByRef rdr1 As OleDbDataReader, _
                                ByVal sSql As String, _
                                  Optional ByVal blnReadWrite As Boolean = False) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String

        '== On Error GoTo GetRst_Error
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql
        Try
            rdr1 = cmd1.ExecuteReader
            If (Not (rdr1 Is Nothing)) Then  '--ok-
                gbGetReader = True '--blnSuccess = True
            Else  '--not open.-
                sMsg = " Error in 'gbGetRst' (Get Recordset):" & vbCrLf & _
                           "ERROR: Recordset not available, or is closed.." & vbCrLf
                sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sMsg)
                '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = sMsg
                gbGetReader = False
            End If  '--open-

        Catch ex As Exception
            sMsg = " Error executing sql cmd in 'gbGetReader' (Get Recordset):" & vbCrLf & _
                      "ERROR text: " & vbCrLf & ex.Message & vbCrLf
            sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, sMsg)
            End If '--log--
            '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            gbGetReader = False
        End Try
        Exit Function

    End Function  '--gbGetReader--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '==  24-March-2014  ----
    '---  Get result Set  INTO DataTable
    '==  NEW for JobMatix POS.. ==
    '--Caller supplies full select string--

    Public Function gbGetDataTable(ByRef cnnUserDB As OleDbConnection, _
                                     ByRef dataTable1 As DataTable, _
                                      ByVal sSql As String) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String
        Dim rdr1 As OleDbDataReader

        gbGetDataTable = False
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql
        Try
            rdr1 = cmd1.ExecuteReader
            If (Not (rdr1 Is Nothing)) Then  '--ok-
                dataTable1 = New DataTable
                dataTable1.Load(rdr1)
                gbGetDataTable = True '--blnSuccess = True
                rdr1.Close()
            Else  '--not open.-
                sMsg = " Error in 'gbGetDataTable' (Get Recordset):" & vbCrLf & _
                           "ERROR: Recordset not available, or is closed.." & vbCrLf
                sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sMsg)
                '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = sMsg
                gbGetDataTable = False
            End If  '--open-

        Catch ex As Exception
            sMsg = " Error executing sql cmd in 'gbGetDataTable' (Get Recordset):" & vbCrLf & _
                      "ERROR text: " & vbCrLf & ex.Message & vbCrLf
            sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, sMsg)
            End If '--log--
            '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            gbGetDataTable = False
        End Try
        If (Not (rdr1 Is Nothing)) Then
            rdr1.Close()
        End If
        Exit Function

    End Function  '--gbGet datatable--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--
    '--- REURNS False only if ERRORS..
    '--   Value of NOTHING is a valid return result--

    Public Function gbGetSelectValueEx(ByRef cnnSql As OleDbConnection, _
                                              ByVal sSql As String, _
                                             ByRef vResult As Object) As Boolean
        Dim rs1 As DataTable  '== ADODB.Recordset
        Dim sErrorMsg As String

        gbGetSelectValueEx = False
        vResult = Nothing     '--valid return result--
        Try
            If Not gbGetDataTable(cnnSql, rs1, sSql) Then
                sErrorMsg = "DB ERROR-  Function 'gbGetSelectValueEx'" & vbCrLf & _
                             " Failed to get SELECT recordset " & "for SQL:" & vbCrLf & sSql & vbCrLf & _
                       "Error text: " & vbCrLf & gsGetLastSqlErrorMessage()
                Call gbLogMsg(gsErrorLogPath, sErrorMsg & vbCrLf & "-- end of error msg.--")
            Else '--get first selected value.-....-
                If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                    '==If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '==rs1.MoveFirst()
                    Dim datarow1 As DataRow = rs1.Rows(0)  '--first row-
                    If Not IsDBNull(datarow1.Item(0)) Then
                        vResult = datarow1.Item(0)
                    End If '--null.-
                    gbGetSelectValueEx = True '--got something..-
                    '== End If '--bof-
                    '==rs1.Close()
                End If '--nothing 
            End If '--get-
        Catch ex As Exception
            sErrorMsg = "EXCEPTION ERROR-  Function 'gbGetSelectValueEx'" & vbCrLf & _
               " Failed to get SELECT recordset " & "for SQL:" & vbCrLf & sSql & vbCrLf & _
               "Error text: " & vbCrLf & ex.Message
            msLastSqlErrorMessage = sErrorMsg
            Call gbLogMsg(gsErrorLogPath, sErrorMsg & vbCrLf & "-- end of error msg.--")
        End Try
        rs1 = Nothing
    End Function '--getSelect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--

    Public Function gbGetSelectValue(ByRef cnnSql As OleDbConnection, _
                                       ByVal sSql As String, _
                                         ByRef vResult As Object) As Boolean
        Dim rs1 As OleDbDataReader '= ADODB.Recordset
        Dim sErrorMsg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        gbGetSelectValue = False
        Try
            If Not gbGetReader(cnnSql, rs1, sSql) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                sErrorMsg = "Failed to get SELECT recordset.." & vbCrLf & _
                       "Error text: " & vbCrLf & gsGetLastSqlErrorMessage()
                msLastSqlErrorMessage = sErrorMsg
                Exit Function
            Else '--get first selected value.-....-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                If Not (rs1 Is Nothing) Then
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                        '== rs1.MoveFirst()
                        If rs1.Read Then
                            If Not IsDBNull(rs1.Item(0)) Then
                                vResult = rs1.Item(0)
                            End If '--null.-
                        End If  '--read-
                    End If '--bof-
                    gbGetSelectValue = True '--got something..-
                    rs1.Close()
                Else
                    msLastSqlErrorMessage = "No dataReader returned from SELECT Query !!"
                End If '--nothing
            End If '--get-
        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sErrorMsg = "== ERROR in mbGetSelectValue.." & vbCrLf & ex.Message & vbCrLf
            msLastSqlErrorMessage = sErrorMsg
            Call gbLogMsg(gsRuntimeLogPath, sErrorMsg)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not (rs1 Is Nothing) Then rs1.Close()
            Exit Function
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- set current database..--
    '-  !! MUST retry because for non-admin user -
    '----  it doesn't stick the first time..--

    Public Function gbSetCurrentDatabase(ByRef cnnSQL As OleDbConnection, _
                                         ByVal sDBName As String) As Boolean

        Dim sSql, sErrors, sMsg As String
        Dim sCurrentDB As String = ""
        Dim lngStart, L1 As Integer
        Dim intUseCount As Integer = 0
        Dim v1 As Object = Nothing

        Try  '--main try-
            gbSetCurrentDatabase = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Do
                Try
                    cnnSQL.ChangeDatabase(sDBName)
                Catch ex As Exception
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Call gbLogMsg(gsErrorLogPath, "= Failed in Change-DATABASE: " & sDBName & vbCrLf & _
                                                 ex.Message & vbCrLf & "-- end of error msg.--")
                    MsgBox("= Failed in USE for DATABASE: " & sDBName & " = =" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    Exit Function '-- False..-
                End Try
                intUseCount += 1
                '--  check USE result for current db..-
                '== If gbGetSelectValueEx(cnnSQL, "SELECT DB_NAME() AS current_db_name;", v1) Then
                '==   sCurrentDB = CStr(v1)
                '== End If
                sCurrentDB = cnnSQL.Database
                If (LCase(sCurrentDB) <> LCase(sDBName)) Then
                    lngStart = CInt(VB6.Timer()) '--PAUSE.. starting seconds.-
                    While (CInt(VB6.Timer()) < lngStart + 2)
                        System.Windows.Forms.Application.DoEvents()
                    End While
                End If '-not db.-
            Loop Until (LCase(sCurrentDB) = LCase(sDBName)) Or (intUseCount >= 5)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '-- check result..-
            If (LCase(sCurrentDB) = LCase(sDBName)) Then gbSetCurrentDatabase = True
            Exit Function
        Catch ex As Exception
            sMsg = "== ERROR in gbSetCurrentDatabase function.." & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            Exit Function
        End Try  '--main try-

    End Function '-SetCurrentDatabase-
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- test sql server user condition.-
    '----  the SELECT statement provided should return a single value.-

    Public Function gbTestSqlUser(ByRef cnnSQL As OleDbConnection, _
                            ByVal strSelectQuery As String) As Boolean

        Dim rs1 As OleDbDataReader  '-ADODB.Recordset
        Dim vResult As Object
        Dim sMsg As String

        Try
            gbTestSqlUser = False
            If Not gbGetReader(cnnSQL, rs1, strSelectQuery) Then
                MsgBox("Failed to get SELECT recordset for query: <" & strSelectQuery & ">.." & vbCrLf & _
                        "Error text: " & gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Else '--get first selected value.-....-
                If Not (rs1 Is Nothing) Then
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                        '== If rs1.BOF Then rs1.MoveFirst()
                        If rs1.Read Then
                            If Not IsDBNull(rs1(0)) Then  '=  (rs1.Fields(0).Value) Then
                                vResult = rs1(0)  '== rs1.Fields(0).Value
                                If CShort(vResult) = 1 Then gbTestSqlUser = True '--got "1"..-
                            End If '--null.-
                        End If
                    End If '---empty-
                End If '--nothing..-
                rs1.Close()
            End If '--get rst.-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Catch ex As Exception
            sMsg = "== ERROR in mbTestSqlUser function.." & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not (rs1 Is Nothing) Then rs1.Close()
            Exit Function
        End Try
    End Function '-testSql--
    '= = = = = = = = = = =
    '-===FF->

    '-- Get DB_ID --
    '--  Returns false if no such database..
    '-- Result returned in intDb_id --

    Public Function gbGetDB_ID(ByRef cnnSql As OleDbConnection, _
                        ByVal strDatabaseName As String, _
                                ByRef intDb_id As Integer) As Boolean
        Dim sSql As String
        Dim sqlCmd1 As OleDbCommand
        '= Dim intResult As Integer
        Dim objResult As Object

        gbGetDB_ID = False
        intDb_id = -1
        If strDatabaseName = "" Then Exit Function
        sSql = "SELECT DB_ID('" & strDatabaseName & "') AS DB_ID; "
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            '==intResult = CInt(sqlCmd1.ExecuteScalar)
            objResult = sqlCmd1.ExecuteScalar
            If Not IsDBNull(objResult) Then
                intDb_id = CInt(objResult)
            End If
            gbGetDB_ID = True  '=no error--
        Catch ex As Exception
            msLastSqlErrorMessage = "gbGetDB_ID:  Error in Sql ExecuteScalar cmd. " & vbCrLf & ex.Message
        End Try
        '==If gbGetSelectValueEx(cnnSql, sSql, vResult) Then  '--not error-
        '==If Not (vResult Is Nothing) Then
        '==intDb_id = CInt(vResult)
        '==End If
        '==gbGetDB_ID = True  '=no error--
        '==End If
    End Function  '--gbGetDB_ID--
    '= = = = = = = = = = = = = =

    '--exists database--
    '-- get list of databases and check if arg db is included--
    '==3073.309==   use DB_ID --

    Public Function gbExistsDatabase(ByRef cnn1 As OleDbConnection, _
                                     ByVal strDatabaseName As String) As Boolean
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbExistsDatabase = False
        If gbGetDB_ID(cnn1, strDatabaseName, lResult) Then  '--NO error.--
            If (lResult > 0) Then    '--valid Db_id..-
                gbExistsDatabase = True
            End If
        Else  '--error--
            MsgBox("DB Error- 'gbExistsDatabase'.." & vbCrLf & _
                        msLastSqlErrorMessage, MsgBoxStyle.Exclamation)
        End If
    End Function '--exists--
    '= = = =  = = = =
    '= = = = = = = = =
    '-===FF->

    '--  SQL server 2000.--

    '--get list of existing databases--
    '--get list of existing databases--

    Public Function gbGetDatabases(ByRef cnn1 As OleDbConnection, _
                                   ByRef colDBlist As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader '== ADODB.Recordset
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbGetDatabases = False
        lResult = glExecSP(cnn1, "sp_databases", "", sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox("gbGetDatabases-" & vbCrLf & sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = sErrors
        Else
            '--check recordset--
            colDBlist = New Collection
            If Not (rs Is Nothing) Then
                '--if not empty, current pos is First record--(seeADO)_-
                If rs.HasRows Then
                    '==If rs.BOF And (Not rs.EOF) Then rs.MoveFirst() '--shouldn't be necessary--
                    While rs.Read  '== Not rs.EOF
                        '--If UCase$(rs("DATABASE_NAME")) = UCase$(sDBName) Then
                        s1 = rs.Item("DATABASE_NAME")
                        colDBlist.Add(s1) '--rs("DATABASE_NAME")
                        '==rs.MoveNext()
                    End While
                End If  '--has rows-
                gbGetDatabases = True
                '== rs.NextResult() '==rs = rs.NextRecordset
            End If  '--nothing-
            If Not (rs Is Nothing) Then rs.Close()
        End If '--ok--
        rs = Nothing
    End Function '--getDBlist--
    '= = = = = = = = =
    '-===FF->

    '--  SQL Server 2005 Plus..
    '--  SQL Server 2005 Plus..

    '--get list of existing databases--
    '--get list of existing databases--

    Public Function gbGetDatabasesSQL2005(ByRef cnn1 As OleDbConnection, _
                                      ByRef colDBlist As Collection) As Boolean
        Dim sSql, s1 As String
        Dim rs1 As OleDbDataReader '== ADODB.Recordset

        msLastSqlErrorMessage = ""
        gbGetDatabasesSQL2005 = False
        sSql = "SELECT name, database_id, owner_sid FROM sys.databases; "
        If Not gbGetReader(cnn1, rs1, sSql) Then
            '== MsgBox("Failed to retrieve list of databases..", MsgBoxStyle.Exclamation)
            s1 = "Failed to retrieve list of databases.." & vbCrLf & _
                              "Error msg: " & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf
            If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = s1
        Else
            '--check recordset--
            colDBlist = New Collection
            If Not (rs1 Is Nothing) Then
                '--if not empty, current pos is First record--(seeADO)_-
                If rs1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then '-- not empty
                    '==rs1.MoveFirst() '--shouldn't be necessary--
                    While rs1.Read  '== Not rs1.EOF
                        s1 = rs1.Item("NAME")
                        colDBlist.Add(s1) '--rs("DATABASE_NAME")
                        '==rs1.MoveNext()
                    End While
                    gbGetDatabasesSQL2005 = True
                End If '--empty.-
                '== Call rs1.NextResult()   '=rs1 = rs1.NextRecordset
            End If  '--nothing-
        End If '--ok--
        If Not (rs1 Is Nothing) Then rs1.Close()
        rs1 = Nothing
    End Function '--getDBlist--
    '= = = = = = = = = = = = =
    '-===FF->

    '- V3.1  -- with POS support--
    '- V3.1  -- with POS support--
    '--  SQL server 2000/2005 Plus.--

    '--get list of existing Jobmatix databases--
    '--  Filter out system DB's..
    '--  Filter for "jobtracking" and "_jmpos" DB's --

    Public Function gbGetJobmatixDatabases(ByRef cnnSql As OleDbConnection, _
                                           ByRef colAllJobsDBs As Collection, _
                                           ByRef colUserJobsDBs As Collection) As Boolean
        Dim bOk As Boolean
        Dim col1, colMyList As Collection
        Dim s1, s2, sName As String

        gbGetJobmatixDatabases = False
        If gbIsSqlServer2005Plus() Then '-- 9=2005,  10=2008..--
            bOk = gbGetDatabasesSQL2005(cnnSql, colMyList)
        Else '--  <"9".. assume sql Server 2000..---
            '--get list of db's for this sql server 2000--
            '-- IN SQL-2000, this only works for PUBLIC..--
            bOk = gbGetDatabases(cnnSql, colMyList)
        End If '--2005..--

        If bOk Then
            colAllJobsDBs = New Collection  '-- all regardless if user has access..
            colUserJobsDBs = New Collection  '-- Those the user has access to..
            For Each sName In colMyList
                '= sName = CStr(vName) '--??-
                s2 = LCase(sName)
                If (s2 <> "master") And (s2 <> "model") And (s2 <> "msdb") And (s2 <> "tempdb") Then '-ok-
                    colAllJobsDBs.Add(sName, sName)  '--name is both Key and data.-
                    '== sMsg = sMsg & sName & vbCrLf
                    '-- collect jobtracking db's..-
                    If (InStr(s2, "jobtracking") > 0) Or (InStr(s2, "_jmpos") > 0) Then
                        '--check user has access to this DB..--
                        '--  User DB collection is Collection of Collections.
                        bOk = gbTestSqlUser(cnnSql, "SELECT HAS_DBACCESS ('" & sName & "'); ")
                        If mbIsSqlAdmin Or ((Not mbIsSqlAdmin) And bOk) Then
                            col1 = New Collection
                            col1.Add(sName, "dbname")
                            colUserJobsDBs.Add(col1, sName)
                        End If '--admin..-
                    End If
                End If '--not master-
            Next sName '--each name-
            gbGetJobmatixDatabases = True
        End If  '--ok-

    End Function  '-gbGetJobmatixDatabases-
    '= = = = = = = = = =  = = = = = = == =
    '-===FF->

    '--  get recordset as collection for SELECT..--

    Public Function gbGetRecordCollection(ByRef cnnSQL As OleDbConnection, _
                                            ByVal sSql As String, _
                                             ByRef colResult As Collection) As Boolean
        Dim rs1 As OleDbDataReader '= ADODB.Recordset
        Dim sName, sErrorMsg As String
        Dim col1 As Collection
        Dim colRow As Collection
        '== Dim fld1 As ADODB.Field
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim column1 As DataColumn

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        gbGetRecordCollection = False
        If Not gbGetReader(cnnSQL, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset.." & vbCrLf & _
             "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If Not (rs1 Is Nothing) Then
                If rs1.HasRows Then  '=  Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    dataTable1 = New DataTable
                    '--  Get data "recordset" to internal table..
                    dataTable1.Load(rs1)
                    '= dataTable1.TableName = msTableName
                    colResult = New Collection
                    '== rs1.MoveFirst()
                    For Each row1 In dataTable1.Rows
                        colRow = New Collection
                        For Each column1 In dataTable1.Columns '==  For i = 0 To rs.Fields.Count - 1
                            sName = column1.ColumnName   '== rs.Fields(i).Name
                            col1 = New Collection
                            col1.Add(sName, "name")
                            col1.Add(row1.Item(sName), "value")
                            colRow.Add(col1, LCase(sName))
                        Next column1
                        colResult.Add(colRow)
                    Next row1
                    gbGetRecordCollection = True '--got something..-
                End If '--EMPTY. bof-
                rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSelect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--exists user--
    '-- get list of users and check if arg User is included--

    Public Function gbExistsLogin(ByRef cnn1 As OleDbConnection, _
                             ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader '= ADODB.Recordset
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbExistsLogin = False
        lResult = glExecSP(cnn1, "sp_helplogins", "", sErrors, rs)
        If (lResult <> 0) Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = sErrors
        Else
            s1 = "User Logins are: "
            '--check recordset--
            If Not (rs Is Nothing) Then
                '--If rs.RecordCount > 0 Then
                If rs.HasRows Then  '= rs.BOF And (Not rs.EOF) Then rs.MoveFirst()
                    While rs.Read '== Not rs.EOF
                        '==  s1 = s1 + rs.Fields("LoginName").Value + ";  "
                        '--MsgBox "checking db exists: " & rs("DATABASE_NAME")
                        If UCase(rs.Item("LoginName")) = UCase(sLoginName) Then
                            gbExistsLogin = True
                        End If
                        '== rs.MoveNext()
                    End While
                    rs.NextResult()  '= = rs.NextRecordset
                End If  '--has roes.-
                rs.Close()
            End If  '= While  '-nothing-
            '--MsgBox s1   '--testing--
        End If '--ok-
        rs = Nothing
    End Function '--exists user--
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--DROP login..--

    '--drop sql login--
    Public Function gbDropLogin(ByRef cnn1 As OleDbConnection, _
                             ByVal sLoginName As String) As Boolean

        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '== ADODB.Recordset
        Dim lResult As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        sParms = "@loginame='" & sLoginName & "' "
        gbDropLogin = False
        lResult = glExecSP(cnn1, "sp_droplogin", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sErrors
        Else
            gbDropLogin = True
        End If
        rs = Nothing
    End Function '--drop login--
    '= = = = = = = = = = =

    '--add sql login--
    Public Function gbAddLogin(ByRef cnn1 As OleDbConnection, _
                               ByVal sLoginName As String, _
                                ByRef sPassword As String, _
                                   ByRef sDefDB As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '=ADODB.Recordset
        Dim lResult As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        sParms = "@loginame='" & sLoginName & "' & @passwd='" & sPassword & _
                                                         "' & @defdb='" & sDefDB & "'"
        gbAddLogin = False
        lResult = glExecSP(cnn1, "sp_addlogin", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = sErrors
        Else
            gbAddLogin = True
        End If
        rs = Nothing
    End Function '--add login--
    '= = = = = = = = = = =
    '-===FF->

    '---  Add Windows NT user 'domain\user' to SQL logins.--
    '---  Add Windows NT user 'domain\user' to SQL logins.--

    Public Function gbGrantLogin(ByRef cnn1 As OleDbConnection, _
                               ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader   '=ADODB.Recordset
        Dim lResult As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        sParms = "@loginame='" & sLoginName & "' "
        gbGrantLogin = False
        lResult = glExecSP(cnn1, "sp_grantlogin", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sErrors
        Else
            gbGrantLogin = True
        End If
        rs = Nothing
    End Function '--GRANT login--
    '= = = = = = = = = = =
    '-===FF->

    '==3069=- Check for a specific permission existing...-
    '===     Return the StateDEscr. (ie GRANT, DENY, REVOKE, GRANT_WITH_GRANT_OPTION..)--
    '-- USE master
    '-- SELECT PL.name as grantee_name, 
    '--        PM.state_desc, 
    '--        PM.permission_name 
    '-- FROM
    '--   sys.server_permissions AS PM 
    '-- JOIN sys.server_principals AS PL 
    '--   ON  PM.grantee_principal_id = PL.principal_id
    '-- WHERE permission_name = 'VIEW Server State' ;
    '-- GO
    '== Sql Server 2005 or later ONLY..--

    Public Function gbPermissionExists(ByRef cnn1 As OleDbConnection, _
                                        ByVal strGranteeName As String, _
                                         ByVal strPermission As String, _
                                         ByRef strStateDescr As String) As Boolean
        Dim sSql, s1, sErrors, sMsg As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim lResult, L1 As Integer

        msLastSqlErrorMessage = ""
        gbPermissionExists = False
        strStateDescr = ""
        Try
            If Not gbIsSqlServer2005Plus() Then Exit Function
            '--USE--
            s1 = " USE master; "
            If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
                If gbDebug Then MsgBox("gbPermissionExists- Failed sql: " & s1)
                msLastSqlErrorMessage = "gbPermissionExists- Failed sql: " & s1
            End If
            sSql = "  SELECT PL.name as grantee_name, " & _
                   "       PM.state_desc, " & _
                   "       PM.permission_name " & _
                   "    FROM  sys.server_permissions AS PM" & _
                   "     JOIN sys.server_principals AS PL " & _
                   "        ON  PM.grantee_principal_id = PL.principal_id " & _
                   "    WHERE (PM.permission_name ='" & strPermission & "') AND " & _
                   "           (PL.name ='" & strGranteeName & "'); "

            If Not gbGetDataTable(cnn1, rs1, sSql) Then
                If gbDebug Then MsgBox("gbPermissionExists-" & vbCrLf & _
                         "Failed to retrieve list of Permissions..", MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & _
                                  "gbPermissionExists-" & vbCrLf & _
                                      "Failed to retrieve list of Permissions.."
            Else
                '--check recordset--
                If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                    '--if not empty, current pos is First record--(seeADO)_-
                    '==If Not (rs1.BOF And rs1.EOF) Then '-- not empty
                    '== rs1.MoveFirst() '--shouldn't be necessary--
                    Dim datarow1 As DataRow = rs1.Rows(0)  '--first row-
                    '--  Any match is good--
                    strStateDescr = CStr(datarow1.Item("state_desc"))
                    gbPermissionExists = True
                    '== End If '--empty.-
                End If  '--nothing- 
            End If '--ok--
            rs1 = Nothing
            Exit Function
        Catch ex As Exception
            sMsg = "ERROR in gbPermissionExists function.." & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            Exit Function
        End Try
    End Function  '--gbPermissionExists-
    '= = = = = = = = = = = =
    '-===FF->

    '==3069=- Check for a specific permission being GRANTED...-
    '--     strGranteeName if the users Login name. ---
    Public Function gbIsPermissionGranted(ByRef cnn1 As OleDbConnection, _
                                        ByVal strGranteeName As String, _
                                         ByVal strPermission As String) As Boolean
        Dim sStateDescr As String = ""

        msLastSqlErrorMessage = ""
        gbIsPermissionGranted = False
        If Not gbIsSqlServer2005Plus() Then
            If (UCase(strPermission) = "VIEW SERVER STATE") Then  '--this permitted in sql-2000..
                gbIsPermissionGranted = True
            End If
        Else  '--2005 and later..  we can ask..-
            If gbPermissionExists(cnn1, strGranteeName, strPermission, sStateDescr) Then
                If (UCase(Left(sStateDescr, 5)) = "GRANT") Then
                    gbIsPermissionGranted = True
                End If
            End If
        End If  '--2005-
    End Function  '--gbPermissionGranted-
    '= = = = = = = = = = = =

    '--  GRANT permission for  "VIEW SERVER STATE"  --

    Public Function gbGrantVWSSPermission(ByRef cnn1 As OleDbConnection, _
                                         ByVal strGranteeName As String) As Boolean

        Dim sSql, s1, sErrors As String
        Dim L1 As Integer
        '==Dim v1 As Object

        gbGrantVWSSPermission = False
        msLastSqlErrorMessage = ""
        '--USE--
        '== sSql = "USE master; "
        Try
            cnn1.ChangeDatabase("master")
        Catch ex As Exception
            s1 = "gbGrantVWSSPermission:  Failed USE master sql: " & vbCrLf & ex.Message
            If gbDebug Then MsgBox(s1 & vbCrLf & sSql)
            msLastSqlErrorMessage = s1
        End Try
        '== If Not gbSetCurrentDatabase(cnn1, "master") Then
        '= s1 = "gbGrantVWSSPermission:  Failed USE master sql: "
        '= If gbDebug Then MsgBox(s1 & vbCrLf & sSql)
        '= msLastSqlErrorMessage = s1
        '= End If
        '-- TESTING-  check USE result for current db..-
        '== If gbGetSelectValue(cnn1, "SELECT DB_NAME() AS current_db_name;", v1) Then
        '== sCurrentDB = CStr(v1)
        '== MsgBox("Current DB is: " & sCurrentDB)
        '== Else
        '== MsgBox("Failed: Get current db..")
        '== End If

        sSql = "GRANT VIEW SERVER STATE TO [" & strGranteeName & "]; "
        If gbExecuteCmd(cnn1, sSql, L1, sErrors) Then
            gbGrantVWSSPermission = True
        Else
            If gbDebug Then MsgBox("gbGrantVWSSPermission:  Failed sql: " & vbCrLf & sSql)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & _
                                 "gbGrantVWSSPermission:  Failed sql: " & vbCrLf & sSql
        End If
    End Function  '-GrantVWSSPermission-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Grant DB Access -- (replaces addDBUser..)-
    '-- Grant DB Access -- (replaces addDBUser..)-

    Public Function gbGrantDBAccess(ByRef cnn1 As OleDbConnection, _
                                  ByVal sDBName As String, _
                                   ByVal sLoginName As String) As Boolean
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader   '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbGrantDBAccess- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbGrantDBAccess- Failed sql: " & s1
        End If

        sParms = "@loginame='" & sLoginName & "' & @name_in_db='" & sLoginName & "'"
        gbGrantDBAccess = False
        lResult = glExecSP(cnn1, "sp_grantdbaccess", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbGrantDBAccess = True
        End If
        rs = Nothing
    End Function '--add user--
    '= = = = = = = = = = =

    '--add Role..--
    '--add Role..--
    Public Function gbAddRoleMember(ByRef cnn1 As OleDbConnection, _
                                      ByVal sDBName As String, _
                                       ByVal sLoginName As String, _
                                         ByVal sRoleName As String) As Boolean
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbAddRoleMember- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbAddRoleMember- Failed sql: " & s1
        End If
        sParms = "@rolename='" & sRoleName & "' & @membername='" & sLoginName & "'"
        gbAddRoleMember = False
        lResult = glExecSP(cnn1, "sp_addrolemember", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbAddRoleMember = True
        End If
        rs = Nothing
    End Function '--add user--
    '= = = = = = = = = = =
    '-===FF->

    '--add DB user=--
    Public Function gbAddDBuser(ByRef cnn1 As OleDbConnection, _
                               ByVal sDBName As String, _
                               ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '== ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbAddDBuser- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbAddDBuser- Failed sql: " & s1
        End If
        sParms = "@loginame='" & sLoginName & "' & @name_in_db='" & sLoginName & "'"
        gbAddDBuser = False
        lResult = glExecSP(cnn1, "sp_adduser", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbAddDBuser = True
        End If
        rs = Nothing
    End Function '--add user--
    '= = = = = = = = = = =

    '--  Drop User account in DB..--
    '--  Drop User account in DB..--
    '-- g b D r o p D B u s e r ---

    Public Function gbDropDBuser(ByRef cnn1 As OleDbConnection, _
                                         ByVal sDBName As String, _
                                          ByVal sNameInDB As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '==  ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbDropDBuser- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbDropDBuser- Failed sql: " & s1
        End If

        sParms = "@name_in_db='" & sNameInDB & "'"
        gbDropDBuser = False
        lResult = glExecSP(cnn1, "sp_revokedbaccess", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox("Failed to drop DB user: " & sNameInDB & vbCrLf & _
                                        sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbDropDBuser = True
        End If
        rs = Nothing
    End Function '--drop user--
    '= = = = = = = = = = =
    '-===FF->

    '-- Change DB Owner..--
    '-- Change DB Owner..--
    Public Function gbChangeDBOwner(ByRef cnn1 As OleDbConnection, _
                                  ByVal sDBName As String, _
                                   ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '==  As ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbChangeDBOwner- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbChangeDBOwner- Failed sql: " & s1
        End If

        sParms = "@loginame='" & sLoginName & "'  "
        gbChangeDBOwner = False
        lResult = glExecSP(cnn1, "sp_changedbowner", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbChangeDBOwner = True
        End If
        rs = Nothing
    End Function '--change owner--
    '= = = = = = = = = = =
    '= = = = = = = = = = =
    '-===FF->

    '--  sp_helpuser to get JobTrackin DB users..--
    '---  via gbGetUsers (cnn, dbname, colUsers)  ----
    '--- RETURNS Collection of User collections..-
    '---  Each User collection has items with keys: LOGINNAME ans USERNAME -
    '----    As per "sp_helpuser" recordset..--

    '--NB: 17July2011--  JobMatix Rev-2912 ++  ==
    '---- SQL Server 2008.. "sp_helpuser"  RETURNS "RoleName" instead of "GroupName"--
    '------ We MUST detect this and still return "GroupName" to caller..---

    Public Function mbGetUsersEx_SQL2008(ByRef cnn1 As OleDbConnection, _
                                       ByVal sDBName As String, _
                                       ByRef colUserNames As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim colUser1 As Collection
        Dim vGroup As Object

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("mbGetUsersEx_SQL2008- Failed sql: " & s1)
            msLastSqlErrorMessage = "mbGetUsersEx_SQL2008- Failed sql: " & s1
        End If

        mbGetUsersEx_SQL2008 = False
        lResult = glExecSP(cnn1, "sp_helpuser", "", sErrors, rs1)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else '--  check rs1 for users.--
            colUserNames = New Collection
            '--check recordset--
            If Not (rs1 Is Nothing) Then
                '--If rs.RecordCount > 0 Then
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                While rs1.Read   '== Not rs1.EOF
                    If Not IsDBNull(rs1.Item("UserName")) Then
                        colUser1 = New Collection
                        colUser1.Add(CStr(rs1.Item("UserName")), "USERNAME")
                        If Not IsDBNull(rs1.Item("LoginName")) Then
                            colUser1.Add(CStr(rs1.Item("LoginName")), "LOGINNAME")
                        Else '--no login..-
                            colUser1.Add("null", "LOGINNAME")
                        End If '--login.-
                        '--SQL2008-- has "RoleName"-- column..--
                        On Error Resume Next
                        vGroup = rs1.Item("GroupName")
                        If (Err.Number <> 0) Then '--No group.. is 2008 or later..
                            On Error Resume Next
                            vGroup = rs1.Item("RoleName")
                            If (Err.Number <> 0) Then '--no column..-
                                vGroup = System.DBNull.Value '==colUser1.Add "null", "GROUPNAME"
                            Else '--role ok..--'--2008 or later..
                            End If '--rolename--
                        Else '--group ok..-
                        End If '-group..-
                        On Error GoTo 0
                        If Not IsDBNull(vGroup) Then
                            colUser1.Add(vGroup, "GROUPNAME")
                        Else '--null.. no group..-
                            colUser1.Add("null", "GROUPNAME")
                        End If '--Group.-

                        colUserNames.Add(colUser1) '--
                    End If
                    '==rs1.MoveNext()
                End While  '--read-
                rs1.Close()
            End If '--nothing..-
            mbGetUsersEx_SQL2008 = True
        End If
        rs1 = Nothing
    End Function '--get users..-
    '== = = = = = = = = = = =
    '-===FF->

    '--  sp_helpuser to get JobTrackin DB users..--
    '---  via gbGetUsers (cnn, dbname, colUsers)  ----
    '--- RETURNS Collection of User collections..-
    '---  Each User collection has items with keys: LOGINNAME ans USERNAME -
    '----    As per "sp_helpuser" recordset..--

    Public Function gbGetUsersEx(ByRef cnn1 As OleDbConnection, _
                                 ByVal sDBName As String, _
                                 ByRef colUserNames As Collection) As Boolean


        gbGetUsersEx = mbGetUsersEx_SQL2008(cnn1, sDBName, colUserNames)

        '=== Set rs1 = Nothing
    End Function '--get users..-
    '== = = = = = = = = = = =

    '--=3073.311= 11-Mar-2013=
    '-- check user access to DB..--
    '--  LoginName muse be valid login..
    '--   Return false if DB error..

    Public Function gbCheckUserAccess(ByRef cnnSql As OleDbConnection, _
                                    ByVal sDBName As String, _
                                  ByVal sLoginName As String, _
                                   ByRef bHasAccess As Boolean, _
                                    ByRef bIsDbOwner As Boolean) As Boolean
        Dim col1 As Collection
        Dim colUserNames As Collection

        bHasAccess = False
        bIsDbOwner = False
        gbCheckUserAccess = False
        If gbGetUsersEx(cnnSql, sDBName, colUserNames) Then
            gbCheckUserAccess = True
            For Each col1 In colUserNames
                If LCase(col1.Item("LoginName")) = LCase(sLoginName) Then '--login exists in DB, so check if has owner rights..-
                    If (LCase(col1.Item("UserName")) = LCase(sLoginName)) Then
                        '--login has same name as user in DB..--
                        bHasAccess = True '--don't need grantdbaccess.
                        '--  has security account.. Check if owner..
                        If (LCase(col1.Item("GroupName")) = "db_owner") Then
                            '--login has db-owner role.--
                            bIsDbOwner = True
                        End If  '--group-
                    End If  '--same name=
                End If '--login has alias in db..-
            Next col1 '--col1-
        End If '--get users.-
    End Function  '--gbCheckUserAccess-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '---  Upgrade column width..--
    '---  Upgrade column width..--
    '-----  VARCHAR columns only !!!  --

    Public Function gbExpandColumn(ByRef cnnSQL As OleDbConnection, _
                                    ByVal sTable As String, _
                                    ByVal sColumn As String, _
                                     ByVal lngNewWidth As Integer) As Boolean
        Dim sSql, s1 As String
        Dim sErrorMsg As String
        Dim lngaffected As Integer

        msLastSqlErrorMessage = ""
        gbExpandColumn = False
        sSql = "ALTER TABLE [" & sTable & "] ALTER COLUMN " & sColumn & _
                                                  " VARCHAR (" & CStr(lngNewWidth) & ") NOT NULL;  "
        If Not gbExecuteCmd(cnnSQL, sSql, lngaffected, sErrorMsg) Then
            s1 = "ERROR: Failed to expand Column '" & sColumn & _
                                     "' in Table '" & sTable & "' " & vbCrLf & sErrorMsg
            If gbDebug Then MsgBox(s1, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = s1
        Else '--ok-
            If gbDebug Then MsgBox("OK.. Column '" & sColumn & "' in Table '" & sTable & _
                     "' " & vbCrLf & "  has been expanded to " & lngNewWidth & " chars.." & vbCrLf & _
                                "    --> " & lngaffected & " rows affected.", MsgBoxStyle.Information)
            gbExpandColumn = True
        End If
    End Function '--expand..--
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- 3.1.3101-- Check VWSS permissions..-
    '-- 3.1.3101-- Check VWSS permissions..-

    '== 3069==
    '==  For SQL SERVER 2005 +++..  To enable normal user to see all users with "sp_who"..
    '== A. IF WE are sqlAdmin THEN
    '==     AFTER user selecting DB-  
    '==         >> MAKE SURE all non-admin Users of this DB are GRANTED VIEW SERVER STATE permission..
    '== B. IF WE are NOT sqlAdmin THEN
    '==      AFTER user selecting DB-  
    '==         >> Check if current Users of this DB has VIEW SERVER STATE permission..
    '==              If not, advise operator to run JobMatix with Admin login to update perms..
    '==                      Then EXIT..
    '= = == = = = == = == = = = = == === == 

    Public Function gbCheckVWSSpermissions(ByRef cnnSQL As OleDbConnection, _
                                           ByVal bIsSqlAdmin As Boolean, _
                                            ByVal sSqlDbName As String, _
                                             ByVal sCurrentUserNT As String) As Boolean

        Dim sOrphanList, sList1, sMsg As String
        Dim sUserNameInDB, sUserLogin
        Dim col1, colUsers As Collection

        gbCheckVWSSpermissions = False
        If bIsSqlAdmin Then
            '--check/update all JobMatix user accounts in (DB " sDBNameJobs")  --
            '--  USER name may not have an actual Login  !!
            '--    (Can be orphaned due to RESTORE..)--
            Call gbLogMsg(gsRuntimeLogPath, "-- SqlAdmin: Checking user permissions for VWSS.. " & vbCrLf)

            If gbGetUsersEx(cnnSQL, sSqlDbName, colUsers) Then
                If (Not (colUsers Is Nothing)) AndAlso (colUsers.Count > 0) Then
                    sOrphanList = ""
                    sList1 = ""
                    sMsg = "NOTE: Some users in the Database: '" & sSqlDbName & "'" & vbCrLf & _
                            " need additional SQL permissions, and have now been upgraded.." & vbCrLf & _
                            "These are ( UserNames [LoginName] ): " & vbCrLf
                    '-- NEED master databse..--
                    '== frmSplash1.Labstatus.Text = "-- Setting MASTER database as current.."
                    If Not gbSetCurrentDatabase(cnnSQL, "master") Then
                        MsgBox("Couldn't set MASTER DB!", MsgBoxStyle.Exclamation)
                    End If
                    For Each col1 In colUsers
                        sUserLogin = col1.Item("LOGINNAME")
                        sUserNameInDB = col1.Item("USERNAME")
                        If (LCase(sUserNameInDB) <> "dbo") And (LCase(sUserNameInDB) <> "guest") And _
                                 (LCase(sUserNameInDB) <> "information_schema") And _
                                  (LCase(sUserNameInDB) <> "sys") And (LCase(sUserNameInDB) <> "sa") Then
                            If (LCase(sUserLogin) <> "null") And _
                                                  (LCase(sUserLogin) <> LCase(sCurrentUserNT)) Then  '--has login.. and NOT me.-
                                '--  Filter out Admin users..  They have permission anyway..-
                                '-- 3077- Don't filter..  CHECK ALL users--
                                '=3077= If Not mbTestSqlUser(cnnSQL, "SELECT IS_SRVROLEMEMBER ('sysadmin','" & sUserLogin & "'); ") Then
                                '--    -- 3077- all users--  (was:  NOT an admin user.) 
                                '-- ALL UERS-  Check if Login has VIEW SERVER STATE permission..-
                                If Not gbIsPermissionGranted(cnnSQL, sUserLogin, "VIEW SERVER STATE") Then
                                    '--Not prev. Granted. Must GRANT now..-
                                    If Not gbGrantVWSSPermission(cnnSQL, sUserLogin) Then
                                        MsgBox("ERROR: Startup permission check-" & vbCrLf & _
                                             "failed to GRANT VWSS to: " & sUserLogin & vbCrLf & _
                                            "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                                    Else  '--ok.
                                        sList1 = sList1 & sUserNameInDB & "  [" & sUserLogin & "]" & vbCrLf
                                    End If  '--vwss
                                End If  '--prev granted-
                                '=3077= End If  '--admin user.-
                            Else  '--orphan.-
                                sOrphanList = sOrphanList & sUserNameInDB & vbCrLf
                            End If  '--orphan.--
                        End If  '--dbo-
                    Next col1 '--col1-
                    If (sList1 <> "") Then
                        Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & sList1 & vbCrLf)
                        MsgBox(sMsg & vbCrLf & sList1, MsgBoxStyle.Information)
                    End If
                    '--show orphans, if any..-
                    If (sOrphanList <> "") Then
                        Call gbLogMsg(gsRuntimeLogPath, "-- Discovered orphaned users: " & vbCrLf & sOrphanList & vbCrLf)
                    End If  '--have orphans..-
                    gbCheckVWSSpermissions = True
                Else
                    MsgBox("ERROR: Startup permission check-" & vbCrLf & _
                              "No users found in DB: " & sSqlDbName & " !", MsgBoxStyle.Exclamation)
                    Exit Function  '--the end..-
                End If '--nothing..
            Else
                MsgBox("ERROR in Startup permission check-" & vbCrLf & _
                         "Failed to get users to show.." & vbCrLf & _
                              "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                Exit Function  '--the end..-
            End If
        Else  '--normal user.-
            If Not gbIsPermissionGranted(cnnSQL, sCurrentUserNT, "VIEW SERVER STATE") Then
                MsgBox("User permissions need to be upgraded.." & vbCrLf & _
                          "Start up JobMatix again as SQL Admin user to get users upgraded..", MsgBoxStyle.Exclamation)
                Exit Function  '--the end..-
            Else  '-ok-
                gbCheckVWSSpermissions = True
            End If
        End If  '-admin-

    End Function   '-gbCheckVWSSpermissions-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->



    '--  add Windows User Logon to SQL logins..-
    '--  add Windows User Logon to SQL logins..-

    '=-- 1. Create SQL Login if it doesn't exist..
    '=-- 2. Add security account to our DB. (gbGrantDBAccess)..
    '=-- 3. Add db_owner role to the security account in our DB. (gbAddRoleMember)..
    '=-- 4. Add VIEW SERVER STATE permission the new Login.. (for "who_using")..

    '==  Returns TRUE if login exists or was Granted OK..--

    Public Function gbAddNewUser(ByRef cnnSql As OleDbConnection, _
                                    ByVal sSqlDBName As String, _
                                     ByVal sLoginName As String, _
                                      ByRef sResult As String, _
                                      ByRef sErrorResult As String) As Boolean
        Dim bHasAccess, bIsDbOwner As Boolean

        gbAddNewUser = False
        sResult = ""
        sErrorResult = ""
        If Not gbExistsLogin(cnnSql, sLoginName) Then '--does not exist..
            '--no login yet..
            '====If Not gbAddLogin(mCnnSql, sLoginName, sPwd, msSqlDBName) Then
            If Not gbGrantLogin(cnnSql, sLoginName) Then
                sErrorResult = "'gbAddNewUser'- Failed to add new Login '" & sLoginName & "' .." & _
                                vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
                Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
                Exit Function
            Else '-Granted ok..-
                sResult = sResult & "== SQL Login: '" & sLoginName & "' has been granted OK..." & vbCrLf & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sResult & vbCrLf & "-- end INFO msg --" & vbCrLf)
            End If '--added..-
        Else
            If gbDebug Then MsgBox("The SQL login name '" & sLoginName & "' already exists.." & vbCrLf & _
                        "Will now check DB access rights..", MsgBoxStyle.Information)
            sResult = "The SQL login name '" & sLoginName & "' already exists.." & vbCrLf & _
                        "Now checking DB access rights.." & vbCrLf & vbCrLf
        End If '-doesn't exist..-
        bHasAccess = False
        bIsDbOwner = False
        '--
        '==3073.311= 11-Mar2013=
        If Not gbCheckUserAccess(cnnSql, sSqlDBName, sLoginName, bHasAccess, bIsDbOwner) Then
            sErrorResult = "'gbAddNewUser'- ERROR- Failed to get DB user list.." & _
                        vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
            Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
            If gbDebug Then MsgBox(sErrorResult, MsgBoxStyle.Exclamation)
            Exit Function
        End If '--check-
        '---If login doesn't exist IN DB, or exists but has no ownership in this DB..--
        '-- Must add user to DB before adding owner role..--
        If Not bHasAccess Then
            If Not gbGrantDBAccess(cnnSql, sSqlDBName, sLoginName) Then
                '= bOk = False
                sErrorResult = sErrorResult & _
                      "ERROR- Failed to GRANT access to user '" & sLoginName & "' .." & vbCrLf & _
                              vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
                Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
            Else '--granted ok-
                sResult = sResult & "== DB Access to '" & sSqlDBName & _
                                          "' Granted OK to: " & sLoginName & vbCrLf & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sResult & vbCrLf & "-- end INFO msg --" & vbCrLf)
            End If '--grant..-
        Else  '--has access
            sResult = sResult & "== " & sLoginName & _
                         " Already has DB Access to '" & sSqlDBName & vbCrLf & vbCrLf
        End If '--has access..-
        If Not bIsDbOwner Then
            If Not gbAddRoleMember(cnnSql, sSqlDBName, sLoginName, "db_owner") Then
                sErrorResult = sErrorResult & _
                            "ERROR- Failed to add RoleMember (db_owner) to new user '" & sLoginName & _
                          "' .." & vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
                Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
            Else '--ok-
                sResult = sResult & _
                       "== 'DB_OWNER' Role was added OK for User '" & sLoginName & _
                                                "'  in DB: '" & sSqlDBName & "'.." & vbCrLf & vbCrLf
            End If '--add role..-
        Else  '-- HAS db_owner role..
            sResult = sResult & "== " & sLoginName & _
                          " Already is member of db_owner group in DB '" & sSqlDBName & vbCrLf & vbCrLf
        End If
        '== 3071== 
        '== SQL-2005 Plus- User NEEDS VIEW SERVER STATE permission..--
        If gbIsSqlServer2005Plus() Then
            If Not gbGrantVWSSPermission(cnnSql, sLoginName) Then
                sErrorResult = sErrorResult & _
                     "ERROR: Failed to GRANT VIEW SERVER STATE to: " & sLoginName & vbCrLf
            Else  '--ok.
                sResult = sResult & _
                   "== Granted 'VIEW SERVER STATE' permission OK for login '" & sLoginName & ".." & vbCrLf & vbCrLf
            End If  '--vwss-
        End If  '--2005-
        gbAddNewUser = True

    End Function  '-- gbAddNewUser--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Who Using this DB.. --
    '--  Who Using this DB.. --
    '-- call sp_who to get list of current users of DBname --
    '-- Rev-2912.. 21-Jul-2011==  ONLY worry about tasks that are "runnable"..-
    '-- Rev-3069.. 16-Oct-2011==  Worry about all tasks that are NOT "dormant"..-
    '--  ALSO-=3071=  
    '==     For SQL Server 2005 and later..  Use "sys.sysprocesses" system view..

    Public Function gbWhoUsing(ByRef cnn1 As OleDbConnection, _
                              ByVal sDBName As String, _
                               ByRef colWhichUsers As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim sSql, s1, sErrors As String
        Dim sLoginName, sHostName, sStatus As String
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim colUser As Collection

        msLastSqlErrorMessage = ""
        '--USE--
        '== s1 = " USE master " '-- + sDBName
        '==3069=  If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then MsgBox("Failed sql: " & s1)
        gbWhoUsing = False
        If gbIsSqlServer2005Plus() Then
            If gbIntJobMatixDBid() <= 0 Then Exit Function '-- NO DB ID was saved !!-
            sSql = "SELECT loginame, dbid, hostname, nt_domain, status " & vbCrLf & _
                   "FROM sys.sysprocesses; "
            If Not gbGetReader(cnn1, rs1, sSql) Then  '= gbGetRst(cnn1, rs1, sSql) Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                      "Error msg: " & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf
                If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = s1
            Else
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '== If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '= Not rs1.EOF
                            sStatus = ""
                            If (Not IsDBNull(rs1.Item("status"))) Then
                                sStatus = CStr(rs1.Item("status"))
                            End If
                            If (Not IsDBNull(rs1.Item("loginame"))) And (Not IsDBNull(rs1.Item("dbid"))) Then
                                '== colUser = New Collection '--each user is collection..-
                                sLoginName = CStr(rs1.Item("loginame"))
                                If (CInt(rs1.Item("dbid")) = gbIntJobMatixDBid()) And _
                                                                        (sStatus <> "dormant") Then  '--our DB--
                                    colUser = New Collection '--each user is collection..-
                                    colUser.Add(sLoginName, "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    colWhichUsers.Add(colUser)
                                End If
                            End If  '--null login.-
                            '== rs1.MoveNext()
                        End While  '-read-
                    End If  '--empty- 
                    rs1.Close()
                    gbWhoUsing = True
                End If  '--nothing..-
            End If  '--get rset-
        Else  '-sql server 2000 --
            lResult = glExecSP(cnn1, "sp_who", "", sErrors, rs1)
            If lResult <> 0 Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                       "Error msg: " & vbCrLf & sErrors & vbCrLf
                If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = s1
                '==MsgBox(sErrors, MsgBoxStyle.Critical)
            Else '--  check rs1 for users.--
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '= If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '== Not rs1.EOF
                            If Not IsDBNull(rs1.Item("dbname")) Then
                                '=3069.0= If (UCase(rs1.Fields("dbname").Value) = UCase(sDBName)) And _
                                '=3069.0=               (LCase(rs1.Fields("status").Value) = "runnable") Then '-- this one is using....--
                                If (UCase(rs1.Item("dbname")) = UCase(sDBName)) And _
                                           (LCase(rs1.Item("status")) <> "dormant") And _
                                                 (LCase(rs1.Item("status")) <> "background") Then '-- this one is using..--
                                    colUser = New Collection '--each user is collection..-
                                    '--MsgBox "Found user: " + rs1("LoginName")
                                    colUser.Add(CStr(rs1.Item("loginame")), "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    colWhichUsers.Add(colUser)
                                End If '--this db--
                            End If '--isnull-
                            '== rs1.MoveNext()
                        End While
                    End If  '--empty..-
                    rs1.Close()
                    gbWhoUsing = True
                End If '--nothing..-
            End If '--exec ok-
        End If  '--sql 2005--
        rs1 = Nothing
        colUser = Nothing
    End Function '--WhoUsing..-
    '= = = = = = = = = =

End Module
