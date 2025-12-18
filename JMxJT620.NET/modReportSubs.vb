Option Strict Off
Option Explicit On
Imports System.Data.Sql
Imports System.Data.OleDb

Module modReportSubs
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'=== = = = ==  =
	
	'==  grh= 02-aug-2012==
	'==    Ver: 2.1.211  ==
	'==      >> "gbQueryWorkSummary" resurrected for Daily Timesheets..
	'==
	'==  grh= 08-aug-2012==
	'==    Ver: 2.1.217  ==
	'==     FIX "gbQueryWorkSummary" --  (was selecting "Completed" Jobs only !!--
	'= =
	'= = = = = = = = = = = = = = = = = =
    '==  NEW VERSION- Upgraded to .Net 3.5=

    '== --grh--28-Aug-2016= 1:37pm = 
    '==     Also upgraded to ADO.net (oleDb)..==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

	
	'==Public gsErrorLogPath As String
	
	'--  get Current User ---
	
    Private Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, _
                                                                                  ByRef nSize As Integer) As Integer
	'= = = = = = = = = = =
	
	Private msResults As String
	
	'--  clean up sql string data ..--
	Private Function msFixSqlStr(ByRef sInstr As String) As String
		
		msFixSqlStr = Replace(sInstr, "'", "''")
		
	End Function '--fixSql-
	'= = = = = = = = = = = =
	
	'--get day of week--
	Private Function msDayOfWeek(ByRef date1 As Date) As String
		Dim sDay As String
		
		Select Case DatePart(Microsoft.VisualBasic.DateInterval.WeekDay, date1)
			Case 1 : sDay = "Sunday"
			Case 2 : sDay = "Monday"
			Case 3 : sDay = "Tuesday"
			Case 4 : sDay = "Wednesday"
			Case 5 : sDay = "Thursday"
			Case 6 : sDay = "Friday"
			Case 7 : sDay = "Saturday"
		End Select
		
		msDayOfWeek = sDay
	End Function '--day--
	'= = = = = = =  =  = =
    '-===FF->

    '-- -Convert .Net DataType type_name to ADO, sql types==

    Private Function mbConverColumnDataType(ByRef column1 As DataColumn, _
                                             ByRef intADO_type As Integer, _
                                             ByRef sSqlType As String) As Boolean
        mbConverColumnDataType = False
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
                    sSqlType = "NVARCHAR" & "(" & Trim(.MaxLength) & ")"
                Else
                    sSqlType = "sql_variant"
                End If
                '--etc-

            End With '= column1
            intADO_type = giGetADOdataType(sSqlType)

            mbConverColumnDataType = True
        Catch ex As Exception
            MsgBox("ERROR in 'gbConvertDotNetDataType' function" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-mbConverColumnDataType-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- -Convert .Net DataType type_name to ADO, sql types==
    '-- Input v1 is .net nativw value from oleDbdataReader row..-

    Public Function gsConvertDotNetDataType(ByVal v1 As Object, _
                                             ByRef intADO_type As Integer) As String
        Dim sSqlType As String
        gsConvertDotNetDataType = ""
        Try
            If TypeOf (v1) Is System.Int32 Then
                sSqlType = "INT"
                '== intADO_type = 3
            ElseIf TypeOf (v1) Is System.Int64 Then
                sSqlType = "SMALLINT"
            ElseIf TypeOf (v1) Is System.Int16 Then
                sSqlType = "BIGINT"
            ElseIf TypeOf (v1) Is System.Decimal Then
                sSqlType = "MONEY"
            ElseIf TypeOf (v1) Is System.Byte Then
                sSqlType = "BINARY"
            ElseIf TypeOf (v1) Is System.Int16 Then
                sSqlType = "SMALLINT"
            ElseIf TypeOf (v1) Is System.DateTime Then
                sSqlType = "DATETIME"
            ElseIf TypeOf (v1) Is System.String Then
                sSqlType = "NVARCHAR"
            Else
                sSqlType = "sql_variant"
            End If
            '--etc-
            intADO_type = giGetADOdataType(sSqlType)
            gsConvertDotNetDataType = sSqlType
        Catch ex As Exception
            MsgBox("ERROR in 'gsConvertDotNetDataType' function" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-gbConvertDotNetDataType-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->

	
	'--recursive..  calls itself for each child rset..-
	'--recursive..  calls itself for each child rset..-
	
    '--  NEEDS Fixed-pitch Font to view in Notepad..

    '=3301.829  !!  THIS is too much ties to ADODB recordsets and typea..
    '=3301.829  !!  THIS is too much ties to ADODB recordsets and typea..
    '=3301.829  !!  THIS is too much ties to ADODB recordsets and typea..
    '=3301.829  !!  THIS is too much ties to ADODB recordsets and typea..

    '    Private Function mbAccumulateQueryResults(ByRef intDepth As Short, _
    '                                              ByRef rs1 As ADODB.Recordset, _
    '                                              ByVal sChapterName As String, _
    '                                              ByVal sParentColNames As String, _
    '                                              ByRef lngColumnPos As Integer, _
    '                                              ByRef sResults As String) As Boolean


    '        Dim s2, s1, s3 As String
    '        Dim sHdr, sDataLine As String
    '        Dim sItem, sUline As String
    '        Dim fldx As ADODB.Field
    '        Dim sName As String
    '        Dim v1 As Object
    '        Dim sChapter As String
    '        Dim cx, lngResult As Integer
    '        Dim L1, lngSize, fx As Integer
    '        '== Dim lngColumnPos As Long
    '        Dim rs2 As ADODB.Recordset
    '        Dim intNewDepth As Short
    '        Dim sLastKey As String
    '        Dim sOurColNames As String

    '        On Error GoTo showQuery_error

    '        sHdr = "==" : sUline = "--" : sLastKey = ""
    '        '== lngColumnPos = (intDepth * 30) + 1  '--move to right for each child..-
    '        intNewDepth = intDepth + 1
    '        cx = 0 '--count rows..-
    '        sOurColNames = ";"

    '        If Not (rs1 Is Nothing) Then
    '            If Not (rs1.BOF And rs1.EOF) Then '--not empty.--
    '                rs1.MoveFirst()
    '                '--ListResults.Clear
    '                '--ListResults.AddItem "=== Querying: " + sDBname + " ===="
    '                While (Not rs1.EOF) '---And (cx < 100)
    '                    '---1st record..  build hdr..--
    '                    If (cx = 0) Then '--get all names..-
    '                        fx = 0
    '                        For Each fldx In rs1.Fields
    '                            sName = fldx.Name
    '                            sOurColNames = sOurColNames & LCase(sName) & ";" '- info for child --
    '                            fx = fx + 1
    '                            '--  ignore any cols that parent already has displayed...-
    '                            If (fldx.Type <> ADODB.DataTypeEnum.adChapter) And (InStr(sParentColNames, LCase(sName) & ";") <= 0) Then
    '                                If (fx > 1) Then
    '                                    sHdr = sHdr & "| "
    '                                    sUline = sUline & "| "
    '                                End If
    '                                lngSize = fldx.DefinedSize '== (fldx.DefinedSize \ 2)
    '                                '--  set up column print col.width..-
    '                                If lngSize > 24 Then lngSize = 24
    '                                If (fldx.Type = ADODB.DataTypeEnum.adCurrency) Or (fldx.Type = ADODB.DataTypeEnum.adDBTimeStamp) Then
    '                                    lngSize = 14
    '                                ElseIf gbIsNumericType(gsGetSqlType(fldx.Type, lngSize)) Then
    '                                    lngSize = 12
    '                                End If
    '                                '==If UCase(Left(sName, 10)) = "SERVICEDBY" Then lngSize = 10 '--shorten hdr..-
    '                                s1 = Space(lngSize)
    '                                s1 = LSet(sName, Len(s1))
    '                                sHdr = sHdr & s1
    '                                s1 = Space(lngSize)
    '                                s1 = LSet(New String("-", Len(sName)), Len(s1))
    '                                sUline = sUline & s1
    '                            End If '--chapter-
    '                        Next fldx '--fld-
    '                        '--txtMessages.Text = txtMessages.Text + vbCrLf
    '                    End If '-- cx=0===get-names--
    '                    If (cx Mod 20) = 0 Then '--show hdr line..-
    '                        '--ListResults.AddItem sHdr
    '                        sResults = sResults & vbCrLf & Space(lngColumnPos + 1) & sHdr & vbCrLf '--for clipboard..--
    '                        '--ListResults.AddItem sUline
    '                        sResults = sResults & Space(lngColumnPos + 1) & sUline & vbCrLf
    '                    End If
    '                    '--  show data line..--
    '                    sDataLine = "--"
    '                    fx = 0
    '                    For Each fldx In rs1.Fields
    '                        fx = fx + 1
    '                        sName = fldx.Name
    '                        If (fldx.Type = ADODB.DataTypeEnum.adChapter) Then
    '                            rs2 = fldx.Value
    '                            sChapter = sName
    '                        ElseIf (InStr(sParentColNames, LCase(sName) & ";") <= 0) Then  '--normal column..-
    '                            If (fx > 1) Then
    '                                sDataLine = sDataLine & "| "
    '                            End If
    '                            'UPGRADE_WARNING: Couldn't resolve default property of object v1. Click for more: 
    '      '=  ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '                            v1 = fldx.Value
    '                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 
    '      '=  ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    '                            If (IsDBNull(v1)) Then
    '                                sItem = "-"
    '                            Else
    '                                sItem = CStr(v1)
    '                            End If '-- in case str tyoe--
    '                            lngSize = fldx.DefinedSize '== (fldx.DefinedSize \ 2)
    '                            '== If UCase(Left(sName, 10)) = "SERVICEDBY" Then lngSize = 10 '--shorten hdr..-
    '                            '--  set up column print col.width..-
    '                            If lngSize > 24 Then lngSize = 24
    '                            s1 = Space(lngSize)
    '                            If (fldx.Type = ADODB.DataTypeEnum.adCurrency) Then
    '                                lngSize = 14
    '                                s1 = Space(lngSize)
    '                                sItem = FormatCurrency(v1, 2)
    '                                s1 = RSet(sItem, Len(s1))
    '                            ElseIf (fldx.Type = ADODB.DataTypeEnum.adDBTimeStamp) Then
    '                                lngSize = 14
    '                                s1 = Space(lngSize)
    '                                sItem = FormatDateTime(v1, DateFormat.ShortDate)
    '                                s1 = RSet(sItem, Len(s1))
    '                            ElseIf gbIsNumericType(gsGetSqlType(fldx.Type, lngSize)) Then
    '                                lngSize = 12
    '                                s1 = Space(lngSize)
    '                                s1 = RSet(sItem, Len(s1))
    '                            Else
    '                                s1 = LSet(sItem, Len(s1))
    '                            End If
    '                            sDataLine = sDataLine & s1 '== + "| "
    '                        Else '--dups parent col..  ignore.
    '                        End If '--chapter--
    '                        '---txtMessages.Text = txtMessages.Text + CStr(v1)
    '                    Next fldx '--fld-
    '                    '--ListResults.AddItem sDataLine
    '                    '== s1 = UCase(CStr(rs1.Fields(0).Value))
    '                    '== If s1 <> sLastKey Then sResults = sResults + vbCrLf    '--space between groups..-
    '                    '== sLastKey = s1
    '                    sResults = sResults & Space(lngColumnPos + 1) & sDataLine '== child may start alongside. & vbCrLf
    '                    If Not (rs2 Is Nothing) Then '--output all child records..-
    '                        '== If rs2.BOF And (Not rs2.EOF) Then rs2.MoveFirst
    '                        '== While (Not rs2.EOF)
    '                        '==   s1 = Space(Len(sDataLine)) '--to add child records..--
    '                        '==   For Each fldx In rs2.Fields
    '                        '==       v1 = fldx.Value
    '                        '==       If (IsNull(v1)) Then sItem = "-" Else sItem = CStr(v1) '-- in case str tyoe--
    '                        '==       s1 = s1 + fldx.Name + "=" + sItem + "; "
    '                        '==   Next  '--fldx-
    '                        '==   msResults = msResults + s1 + vbCrLf
    '                        '==   rs2.MoveNext
    '                        '== Wend

    '                        '--  Call Ourselves  --
    '                        '--    to children of this parent line..--
    '                        Call mbAccumulateQueryResults(intNewDepth, rs2, sChapter, sOurColNames, lngColumnPos + Len(sUline) + 1, sResults)
    '                    Else
    '                        sResults = sResults & vbCrLf '--no child..
    '                    End If
    '                    'UPGRADE_NOTE: Object rs2 may not be destroyed until it is garbage collected. Click for more: 
    '                  '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '                    rs2 = Nothing
    '                    '--txtMessages.Text = txtMessages.Text + "= = = = = = " + vbCrLf
    '                    cx = cx + 1
    '                    rs1.MoveNext() '--next fkey row--
    '                End While
    '            End If '--not empty..-
    '            '--ListResults.AddItem ""   '--blank line..--
    '            '--ListResults.AddItem "== the end == [" + CStr(cx) + " record(s)]"
    '            '== sResults = sResults + vbCrLf
    '            If gbDebug Then
    '                sResults = sResults & "= Exiting L" & intDepth & ": " & sChapterName & " ==  [ " & CStr(cx) & " record(s)]" & vbCrLf
    '            ElseIf intDepth = 0 Then
    '                sResults = sResults & vbCrLf & "== the end =="
    '            End If

    '            '--ListResults.Enabled = True
    '            '--CmdCopy.Enabled = True
    '            '--Set rs = rs.NextRecordset

    '        End If '--nothing--
    '        Exit Function

    'showQuery_error:
    '        lngResult = Err().Number
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '        MsgBox("Error in mbAccumulateQueryResults.." & vbCrLf & _
    '                   "Error=" & lngResult & "(" & ErrorToString(lngResult) & ").", MsgBoxStyle.Exclamation)

    '    End Function '--results.=
    '= = = = = = = = =
    '-===FF->

    '=3301.830--  New version for oleDbDataReader--

    '--recursive..  calls itself for each child rset..-
    '--recursive..  calls itself for each child rset..-

    Private Function mbAccumulateQueryResults(ByRef intDepth As Short, _
                                              ByRef rdr1 As OleDbDataReader, _
                                              ByVal sChapterName As String, _
                                              ByVal sParentColNames As String, _
                                              ByRef lngColumnPos As Integer, _
                                              ByRef colResults As Collection, _
                                              ByRef sResults As String) As Boolean

        Dim s2, s1, s3 As String
        Dim sHdr, sDataLine As String
        Dim sUline As String
        '= Dim fldx As ADODB.Field
        Dim sName, sSqlType As String
        Dim intADO_type As Integer
        Dim v1 As Object
        Dim sChapter As String
        '= Dim cx, lngResult As Integer
        Dim lngSize, intFldWidth, rowx As Integer
        '== Dim lngColumnPos As Long
        '= Dim rs2 As ADODB.Recordset
        Dim rdr2 As OleDbDataReader
        Dim intNewDepth As Short
        Dim sLastKey As String
        Dim sOurColNames As String
        Dim colThisChapter, colRowChild As Collection
        Dim colThisRowset, colThisRow, colThisRowData, colField As Collection

        mbAccumulateQueryResults = False

        Try  '-main try-
            sHdr = "==" : sUline = "--" : sLastKey = ""
            '== lngColumnPos = (intDepth * 30) + 1  '--move to right for each child..-
            intNewDepth = intDepth + 1
            rowx = 0 '--count rows..-
            sOurColNames = ";"
            colThisChapter = New Collection
            colThisChapter.Add(sChapterName, "ChapterName")
            colThisRowset = New Collection

            If Not (rdr1 Is Nothing) Then '-ok-
                If rdr1.HasRows Then
                    Do While rdr1.Read
                        colThisRow = New Collection
                        colThisRowData = New Collection
                        colRowChild = Nothing
                        sDataLine = ""
                        For ifx As Integer = 0 To (rdr1.FieldCount - 1)
                            sName = rdr1.GetName(ifx) '= fldx.Name
                            v1 = rdr1.Item(ifx)
                            sSqlType = gsConvertDotNetDataType(rdr1.Item(ifx), intADO_type)
                            intFldWidth = 12    '-Temp=
                            If gbIsNumericType(sSqlType) Then
                                intFldWidth = 8
                            End If
                            If (rowx = 0) Then '--get all col. names-
                                '--  ignore any cols that parent already has displayed...-
                                If (InStr(sParentColNames, LCase(sName) & ";") <= 0) Then
                                    sOurColNames = sOurColNames & LCase(sName) & ";" '- info for child --
                                    '= s1 = Space(lngSize)
                                    s1 = LSet(sName & "(" & sSqlType & ")", intFldWidth)
                                    If (ifx > 0) Then '-not 1st-
                                        sHdr = sHdr & " | "
                                        sUline = sUline & " | "
                                    End If
                                    sHdr = sHdr & s1
                                    '=s1 = Space(lngSize)
                                    s1 = LSet(New String("-", Len(sName)), intFldWidth)
                                    sUline = sUline & s1
                                End If  '- not in parent-
                            End If   '- rowx-
                            '=LogText(Space(lngLevel * 3) & dr.GetName(i) & vbTab)
                            '- Looking for FieldType of System.Data.IDataReader
                            If TypeOf rdr1(ifx) Is IDataReader Then
                                rdr2 = rdr1.GetValue(ifx)
                                sChapter = sName
                            Else  '- NOT child-chapter- 
                                '- get name/data-
                                '--  ignore any cols that parent already has displayed...-
                                If (InStr(sParentColNames, LCase(sName) & ";") <= 0) Then
                                    '- TEMP conversion-
                                    s1 = v1.ToString   '= rdr1.GetString(ifx)
                                    If (ifx > 0) Then '-not 1st-
                                        sDataLine &= " | "
                                    End If
                                    sDataLine &= LSet(s1, intFldWidth)
                                End If '-parent name-
                                '-- add to fld collection this row-
                                colField = New Collection
                                colField.Add(sName, "name")
                                colField.Add(v1, "value")
                                colThisRowData.Add(colField, sName)
                            End If  '-rdr type-
                        Next ifx
                        '-- done all flds in this row.
                        If (rowx = 0) Then '--Show col. names-
                            sResults = sResults & Space(lngColumnPos + 1) & sHdr '== child may start alongside. & vbCrLf
                            sResults &= vbCrLf '-dataline will follow, and must be UNDER this hdr.
                        End If
                        rowx += 1   '-track rows.-
                        '-- add dataline to rsult-
                        sResults = sResults & Space(lngColumnPos + 1) & sDataLine '== ??? child may start alongside. & vbCrLf

                        If Not (rdr2 Is Nothing) Then '-have child chapter-
                            '= ListChapteredFields(drOrders, lngLevel + 1)
                            '--  Call Ourselves  --
                            '--    to save children of this parent line..--
                            sResults &= vbCrLf
                            colRowChild = New Collection
                            Call mbAccumulateQueryResults(intNewDepth, rdr2, sChapter, _
                                                          sOurColNames, lngColumnPos + Len(sHdr) + 1, colRowChild, sResults)
                        Else '-no child chapter in this row-
                            sResults &= vbCrLf
                        End If
                        colThisRow.Add(colThisRowData, "rowdata")
                        colThisRow.Add(colRowChild, "rowchild")
                        colThisRowset.Add(colThisRow)
                    Loop  '-While rdr1.Read-
                    rdr1.Close()
                End If  '-rdr1.HasRows-
            End If '-nothing-
            colThisChapter.Add(colThisRowset, "Rows")
            colResults.Add(colThisChapter, sChapterName)
            If gbDebug Then
                'sResults = sResults & "= Exiting L" & intDepth & ": " & _
                '            sChapterName & " ==  [ " & CStr(rowx) & " record(s)]" & vbCrLf
            ElseIf intDepth = 0 Then
                sResults = sResults & vbCrLf & "== the end =="
            End If

            Exit Function   '--done-

        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error in mbAccumulateQueryResults.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try  '-main try-

    End Function  '-AccumulateQuery-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  get Current User ---
    '--  get Current User ---

    Public Function gsCurrentUser() As String
        '*********************************************************
        '* Function to get the current logged on user in windows *
        '*********************************************************

        Dim strBuff As New VB6.FixedLengthString(255)
        Dim X As Integer

        gsCurrentUser = ""
        X = GetUserName(strBuff.Value, Len(strBuff.Value) - 1)
        If X > 0 Then
            'Look for Null Character, usually included
            X = InStr(strBuff.Value, vbNullChar)
            'Trim off buffered spaces too
            If X > 0 Then
                gsCurrentUser = Left(strBuff.Value, X - 1) 'UCase is optional ;)
            Else
                gsCurrentUser = Left(strBuff.Value, X)
            End If
        End If

    End Function
    '= = = = = =  == = =
    '-===FF->


    '-- load systemInfo settings..--
    '-- load systemInfo settings..--
    '---  send back a collection of collections (rows..)--

    'Public Function gbLoadsystemInfo(ByRef cnnSQL As ADODB.Connection, ByRef colInfo As Collection, ByRef sdSystemInfo As Scripting.Dictionary) As Boolean
    '	Dim sSql As String
    '	Dim sKey, sValue As String
    '	Dim rs1 As ADODB.Recordset
    '	Dim col1 As Collection
    '	Dim date1, date2 As Date

    '	gbLoadsystemInfo = False
    '	sSql = "Select * from [systemInfo] "
    '	'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '	System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '	If Not gbGetRst(cnnSQL, rs1, sSql) Then
    '		MsgBox("Failed to get systemInfo recordset..", MsgBoxStyle.Exclamation)
    '		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '	Else '--build dictionary of sysinfo items....-
    '		colInfo = New Collection '--  holds system settings..
    '		sdSystemInfo = New Scripting.Dictionary
    '		If Not (rs1 Is Nothing) Then
    '			If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
    '			While (Not rs1.EOF) '---
    '				'--add item....
    '				col1 = New Collection
    '				sKey = UCase(Trim(rs1.Fields("systemKey").Value))
    '				col1.Add(sKey, "systemkey")
    '				col1.Add(Trim(rs1.Fields("systemValue").Value), "systemvalue")
    '				col1.Add(CDate(rs1.Fields("dateCreated").Value), "datecreated")
    '				col1.Add(CDate(rs1.Fields("dateUpdated").Value), "dateupdated")
    '				If sKey <> "" Then colInfo.Add(col1, sKey)
    '				'-- build Dictionay also..--
    '				sdSystemInfo.Add(sKey, Trim(rs1.Fields("systemValue").Value))
    '				rs1.MoveNext()
    '			End While '-eof-
    '			rs1.Close()
    '			gbLoadsystemInfo = True
    '		End If '--rs nothing=-
    '	End If '--get rs-
    '	'UPGRADE_NOTE: Object rs1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '	rs1 = Nothing
    '	'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '	System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    'End Function '--load info..--.-
    '= = = = = =
    '-===FF->


    '-- Get SQL-Server Version..-
    '-- Get SQL-Server Version..-

    'Public Function gsGetSqlServerVersion(ByRef cnnSQL As ADODB.Connection) As String
    '	Dim lngResult As Integer
    '	Dim sErrors As String
    '	Dim rs1 As ADODB.Recordset

    '	gsGetSqlServerVersion = ""
    '	'==msSqlVersion = ""
    '	lngResult = glExecSP(cnnSQL, "xp_msver", "", sErrors, rs1)
    '	If lngResult <> 0 Then '--failed.-
    '		MsgBox("Failed to get SQL Version.." & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
    '	Else '--ok.-
    '		If Not (rs1 Is Nothing) Then
    '			If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
    '				If rs1.BOF Then rs1.MoveFirst() '-- MUST do it this way for r/sets from execSP..-
    '				While Not rs1.EOF
    '					'== "name" is the second column in the msver recordset..--
    '					If (LCase(rs1.Fields("name").Value) = "productversion") Then
    '						gsGetSqlServerVersion = rs1.Fields("Character_Value").Value
    '					End If
    '					rs1.MoveNext()
    '				End While
    '			End If '--empty.-
    '		End If '--nothing..-
    '	End If '-result.--

    'End Function '--sql version.-
    '= = = = = = = =
    '-===FF->


    '-- test sql server user condition.-
    '----  the SELECT statemen1 provided should return a single value.-

    'Public Function gbTestSqlUser(ByRef cnnSQL As ADODB.Connection, ByVal strSelectQuery As String) As Boolean

    '	Dim rs1 As ADODB.Recordset
    '	Dim vResult As Object

    '	gbTestSqlUser = False
    '	If Not gbGetRst(cnnSQL, rs1, strSelectQuery) Then
    '		MsgBox("Failed to get SELECT recordset for query: <" & strSelectQuery & ">..", MsgBoxStyle.Exclamation)
    '		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '	Else '--get first selected value.-....-
    '		If Not (rs1 Is Nothing) Then
    '			If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
    '				If rs1.BOF Then rs1.MoveFirst()
    '				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    '				If Not IsDbNull(rs1.Fields(0).Value) Then
    '					'UPGRADE_WARNING: Couldn't resolve default property of object vResult. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '					vResult = rs1.Fields(0).Value
    '					'UPGRADE_WARNING: Couldn't resolve default property of object vResult. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '					If CShort(vResult) = 1 Then
    '						gbTestSqlUser = True '--got "1"..-
    '					End If
    '				End If '--null.-
    '			End If '---empty-
    '		End If '--nothing..-
    '	End If '--get rst.-
    '	'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '	System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    'End Function '-test--
    '= = = = = = = = = = =
    '-===FF->

    '-- Create and load Temp Table..--
    '-- Create and load Temp Table..--
    '---- "bDropDuplicates"  refers to the first column of the r/set..-
    '- ByRef rs1 As ADODB.Recordset NOW is datatable..

    Public Function gbMakeTempTable(ByRef cnnSQL As OleDbConnection, _
                                    ByVal sTableName As String, _
                                    ByRef rs1 As DataTable, _
                                    Optional ByVal strDropDuplicatesColName As String = "") As Boolean
        Dim bDropDuplicates As Boolean
        Dim sSql As String
        Dim column1 As DataColumn '= ADODB.Field
        Dim sErrorMsg As String
        Dim sFields As String
        Dim sType, sSqlType, sFldValue As String
        Dim sPrevious As String
        Dim sValues As String
        Dim sDupValue As String
        Dim bIncludeRow As Boolean
        Dim sTest As String
        Dim sResults As String
        Dim sDuplicates As String
        Dim iPos, ix, lngRecCount As Integer
        Dim lngTotalLines, lngBatchCount, lngaffected As Integer
        Dim intADO_type As Integer

        gbMakeTempTable = False
        bDropDuplicates = (Len(strDropDuplicatesColName) > 0)
        If rs1 Is Nothing Then Exit Function
        '==If (rs1.Fields.Count <= 0) Then Exit Function
        If (rs1.Columns.Count <= 0) Then Exit Function
        '--create temp table from supplied recordset..--
        '--  First, DROP it in case it's still there..-
        sSql = " DROP TABLE " & sTableName & "; "
        Call gbExecuteCmd(cnnSQL, sSql & vbCrLf, lngaffected, sErrorMsg)
        sSql = " CREATE TABLE " & sTableName & " (Temp_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, " & vbCrLf
        '-- reproduce rs1 columns into temp table..--
        For Each column1 In rs1.Columns '= rs1.Fields
            '== sSql = sSql & field1.Name & " " & gsGetSqlType(field1.Type, field1.DefinedSize) & ", "
            Call mbConverColumnDataType(column1, intADO_type, sSqlType)
            sSql = sSql & column1.ColumnName & " " & sSqlType & ", "
        Next column1 '--field..-..
        sSql = sSql & " DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '== MsgBox("TEMP Create SQL is:" & vbCrLf & sSql, vbInformation)  '--T E S T ..--
        sPrevious = ""
        sDuplicates = ""
        If Not gbExecuteCmd(cnnSQL, sSql & vbCrLf, lngaffected, sErrorMsg) Then
            MsgBox(vbCrLf & "Failed SQL CREATE Temp table: " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '-ok-
            '--insert all lines--
            If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                sSql = "" : sResults = "Temp table inserts:" & vbCrLf
                lngTotalLines = 0
                lngBatchCount = 0
                lngRecCount = 0
                'rs1.MoveFirst()
                'rs1.MoveLast()
                ''===MsgBox "r/set has " & rs1.RecordCount & " rows.."
                'rs1.MoveFirst()
                '=====For Each col1 In colReportLines
                For Each row1 As DataRow In rs1.Rows  '=While Not rs1.EOF
                    '--build next insert..--
                    sFields = ""
                    sValues = ""
                    sDupValue = ""
                    bIncludeRow = True '--assume not duplicate..
                    For colx As Integer = 0 To rs1.Columns.Count - 1
                        '=For Each column1 In rs1.Columns '=  field1 In rs1.Fields
                        column1 = rs1.Columns(colx)
                        If sFields <> "" Then
                            sFields = sFields & ", "
                            sValues = sValues & ", "
                        End If
                        sFields = sFields & column1.ColumnName  '= field1.Name
                        '== sType = gsGetSqlType(field1.Type, field1.DefinedSize)
                        Call gbConvertDotNetDataType(column1, intADO_type, sType)
                        iPos = InStr(sType, "(")
                        If (iPos > 0) Then sType = Trim(Left(sType, iPos - 1)) '--drop size.-
                        If gbIsNumericType(sType) Then
                            sValues = sValues & CStr(row1.Item(colx))
                        Else '--needs quotes..--
                            sValues = sValues & "'" & CStr(row1.Item(colx)) & "'"
                        End If
                        If bDropDuplicates Then '--test if this is the dup test column..
                            If LCase(column1.ColumnName) = LCase(strDropDuplicatesColName) Then '--this column..-
                                If CStr(row1.Item(colx)) = sPrevious Then bIncludeRow = False '--drop dup.
                                sDupValue = CStr(row1.Item(colx)) '--save-
                            End If
                        End If
                    Next colx '=column1 '=field1 '--field..-
                    lngRecCount = lngRecCount + 1 '--rset records..-
                    If bIncludeRow Then
                        sPrevious = sDupValue '--save key of last item written out..-
                        sSql = sSql & " INSERT INTO " & sTableName & " ( " & sFields & ") VALUES (" & sValues & ") " & vbCrLf
                        lngBatchCount = lngBatchCount + 1
                        lngTotalLines = lngTotalLines + 1
                        If (lngBatchCount >= 50) Then
                            '===  OR ((lngBatchCount <= 50) And (lngTotalLines >= rs1.RecordCount)) Then
                            '--send every 50, or last batch..-
                            'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 
                            '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
                            '== GoSub MakeTempInsertBatch
                            sTest = "Inserting " & lngBatchCount & " rows..  sql is:" & vbCrLf & sSql '--test--
                            sResults = sResults & sTest & vbCrLf & "== end batch ==  " & vbCrLf
                            If Not gbExecuteCmd(cnnSQL, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                                MsgBox(vbCrLf & "Failed SQL INSERTs into Temp table: " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                Exit Function
                            End If '--insert ok-
                            sSql = ""
                            lngBatchCount = 0
                            '= If (lngBatchCount <> 0) Then Exit Function '--failed..-
                        End If '--batch--
                    Else '--dropped..-
                        sDuplicates = sDuplicates & sDupValue & ";" & vbCrLf
                    End If '--include-
                    '== rs1.MoveNext()
                Next row1 '=End While '--eof..-
                If (lngBatchCount > 0) Then '--some left..-
                    '= GoSub MakeTempInsertBatch
                    sTest = "Inserting " & lngBatchCount & " rows..  sql is:" & vbCrLf & sSql '--test--
                    sResults = sResults & sTest & vbCrLf & "== end batch ==  " & vbCrLf
                    If Not gbExecuteCmd(cnnSQL, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                        MsgBox(vbCrLf & "Failed SQL INSERTs into Temp table: " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Exit Function
                    End If '--insert ok-
                    sSql = ""
                    lngBatchCount = 0
                End If
                '====Next '--col1
                '-- insert FINAL TOTAL ROW.. --
                '-- now read temp file as report..-
                '--TEST..-   check recordset..--
                '--sSql = " SELECT * FROM #WorkSessions "
                '--If Not gbGetRst(mCnnShape, rs1, sSql) Then _
                ''--     MsgBox "Failed to get TEST Sessions recordset..", vbExclamation
                '--Call mbShowQueryResults(rs1, msSqlDBName)
                '--Call CmdCopy_Click
                '--MsgBox "Check clipboard for all session rows.."
                '-- END TEST.--
            End If '--not empty..-
            '-- TEST---
            '===Call glSaveTextFile("JobReports_TempSql.txt", sResults & vbCrLf & _
            ''===                     "Duplicates dropped: " & sDuplicates & vbCrLf & _
            ''===                          "= = = Inserted: " & lngTotalLines & " rows = = =")
            '-- END TEST.--
            gbMakeTempTable = True
        End If '--create..-
        Exit Function

        '--send every 50, or last batch..-
        'MakeTempInsertBatch:
        '        sTest = "Inserting " & lngBatchCount & " rows..  sql is:" & vbCrLf & sSql '--test--
        '        sResults = sResults & sTest & vbCrLf & "== end batch ==  " & vbCrLf
        '        If Not gbExecuteCmd(cnnSQL, sSql & vbCrLf, lngaffected, sErrorMsg) Then
        '            MsgBox(vbCrLf & "Failed SQL INSERTs into Temp table: " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '            '==    Exit Function
        '        End If '--insert ok-
        '        sSql = ""
        '        lngBatchCount = 0
        '        Return
    End Function '-bMakeTempTable-
    '= = = = = = = = = = = = = = =
    '-===FF->


    '--Show Hierarchical rset result.--
    '--Show Hierarchical rset result.--

    '== Public Function gbShowQueryResults(ByRef rs1 As ADODB.Recordset) As String

    Public Function gbShowQueryResults(ByRef rdr1 As OleDbDataReader, _
                                       ByVal strBaseTableName As String, _
                                       ByRef colResults As Collection) As String
        Dim intDepth As Short '--recursion depth
        Dim lngColPos As Integer
        Dim sResults As String

        intDepth = 0
        lngColPos = 1
        sResults = vbCrLf & "=== Hierarchical Query result.. ====" & vbCrLf

        '=  WRITE something for oleDb dataReaders-
        colResults = New Collection
        Call mbAccumulateQueryResults(intDepth, rdr1, strBaseTableName, "", lngColPos, colResults, sResults)

        gbShowQueryResults = sResults

    End Function '-show-
    '= = = = = = = = = = = = = = =
    '-===FF->


    '--Get A N Y JOB  u p d a t e d  in selected Period ..
    '--Get A N Y JOB  u p d a t e d  in selected Period ..
    '--Get A N Y JOB  u p d a t e d  in selected Period ..
    '-- Staff Timesheets...-

    '--Jobs Work summary query script..--
    '-- "dateStart" and "dateEnd" (incl) define reporting period..--

    Public Function gbQueryWorkSessions(ByRef cnnSQL As OleDbConnection, _
                                        ByRef cnnShape As OleDbConnection, _
                                        ByVal dateStart As Date, _
                                        ByVal dateEnd As Date, _
                                        ByVal curLabourHourlyRateP1 As Decimal, _
                                        ByVal curLabourHourlyRateP2 As Decimal, _
                                          ByVal curLabourHourlyRateP3 As Decimal, _
                                          ByVal sDescriptionPriority1 As String, _
                                          ByVal sDescriptionPriority2 As String, _
                                          ByVal sDescriptionPriority3 As String, _
                                             ByRef sShapeSql As String) As Boolean
        Dim sSql, s1 As String
        Dim sErrorMsg As String
        Dim sTotalSql As String
        Dim sRem As String
        Dim sWhere As String
        Dim sTimeSpent As String
        Dim sTimeSpentNC As String
        '== Dim sDate As String
        Dim sSessionDate As String
        Dim sSessionDayOfWeek As String
        Dim sDate1, sDate2 As String
        Dim sJobId As String
        Dim sDateCreated As String
        '== Dim sDateCompleted As String
        Dim sDateUpdated As String '--job record..-
        Dim sCustomer As String
        Dim sJobStatus As String
        Dim sJobPriority As String
        Dim sPriorityDescr As String
        Dim sTotalTime As String
        Dim sTest As String
        Dim rdr1 As OleDbDataReader  '= rs1 As New ADODB.Recordset
        Dim dataTable1 As DataTable
        '= Dim fldx As ADODB.Field
        Dim sName As String
        Dim v1 As Object
        Dim lngDayNumber As Integer
        Dim dSessionDate As Date
        Dim iPos3, iPos, cx, iPos2, lngJobs As Integer
        Dim lngRecordsAff, lngError As Integer
        Dim intSelYear, intSelMonth, intHistoryDays As Integer
        Dim lngTotalLines, lngBatchCount, lngaffected As Integer
        Dim col1 As Collection
        Dim colReportLines As Collection
        Dim cur1 As Decimal
        Dim curLabourRate As Decimal '--Rate for this Job..-

        Dim curSessionCost As Decimal '--Compute session charge..-
        Dim curJobTimeSpent As Decimal '--Accum total job/session time..-
        Dim curJobTimeSpentNC As Decimal '--Accum total NoCharge job/session time..-
        Dim curTotalTime As Decimal '--total session time..-
        Dim curTotalTime_nc As Decimal '--total NC session time..-

        Dim cmd1 As OleDbCommand

        '= On Error GoTo QuerySessions_error
        Try  '-main try-
            intHistoryDays = 31 '--TESTING..  get full month--
            gbQueryWorkSessions = False
            intSelMonth = Month(Today) : intSelYear = Year(Today) '--  temp..  choose current month..-

            '--mlStaffTimeout = -1  '--SUSPEND timing out..--
            sDate1 = VB6.Format(dateStart, "dd-mmm-yyyy") & " 00:00"
            sDate2 = VB6.Format(dateEnd, "dd-mmm-yyyy") & " 23:59"
            '==Ver: 2.1.217  ==  sWhere = " WHERE (LEFT(Jobs.jobStatus,2)>='50') "
            '==Ver: 2.1.217  == sWhere = sWhere + " AND (Jobs.DateUpdated IS NOT NULL) "

            sWhere = " WHERE (Jobs.DateUpdated IS NOT NULL) "
            '==Ver: 2.1.217  == sWhere = sWhere & " AND ((Jobs.DateUpdated>='" + sDate1 + "') AND (Jobs.DateUpdated<='" + sDate2 + "')) "
            '== JOB may have been updated again AFTER dateEnd..
            '--  WE STILL wnat it..
            sWhere = sWhere & " AND (Jobs.DateUpdated>='" & sDate1 & "') "

            '--Choose completed jobs (incl delivered..)..--
            '--sWhere = " WHERE ((LEFT(jobStatus,2)>='50') ) "
            '--   " AND (YEAR(DateCompleted)=" & CStr(intSelYear) & ") " &  "AND (MONTH(DateCompleted)=" & CStr(intSelMonth) & "))"
            '--If intHistoryDays >= 0 Then  '--specify some days.
            '--   sWhere = sWhere & " AND (DATEDIFF(day,DateCompleted,CURRENT_TIMESTAMP)<=" & CStr(intHistoryDays) & ")"
            '--End If

            sSql = " SELECT job_id, DateCreated, DateUpdated, JobStatus, Priority, " & _
                     " Customer=(CASE CustomerCompany WHEN 'n/a' THEN ''  WHEN '--' THEN '' " & _
                       "  ELSE (CustomerCompany +' + ') END + CustomerName), " & _
                        " CustomerBarcode AS CustBarcode, TotalServiceTime, SessionTimes, TechStaffName " & _
                          " FROM Jobs " & sWhere & " ORDER BY job_id DESC "
            lngJobs = 0
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            System.Windows.Forms.Application.DoEvents()
            '--MsgBox "Getting jobs recordset..  sql is:" + vbCrLf + sSql, vbInformation '--test--
            If Not gbGetDataTable(cnnSQL, dataTable1, sSql) Then '= gbGetRst(cnnSQL, rs1, sSql) Then
                MsgBox("Failed to get Jobs recordset..", MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Else '--build session records....-
                colReportLines = New Collection '--  all session lines..-
                If Not (dataTable1 Is Nothing) Then
                    If dataTable1.Rows.Count <= 0 Then '= rs1.BOF And rs1.EOF Then
                        MsgBox("No UPDATED jobs found for the period..", MsgBoxStyle.Information)
                    Else
                        '= rs1.MoveFirst()
                    End If '--empty-
                    sTest = "Found Sessions: " & vbCrLf
                    For Each datarow1 As DataRow In dataTable1.Rows '= While (Not rs1.EOF) '---
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 
                        '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        If Not IsDBNull(datarow1.Item("DateUpdated")) Then '--ok-
                            '--Save Job Info, and unpack sessions for each job...
                            sJobId = CStr(datarow1.Item("job_id"))   '= CStr(rs1.Fields("job_id").Value)
                            '--sDateCompleted = ""    '-- iis(IsNull(rs1("DateCompleted")), "", CStr(rs1("DateCompleted")))
                            '--If Not IsNull(rs1("DateCompleted")) Then sDateCompleted = CStr(rs1("DateCompleted"))
                            sDateCreated = VB6.Format(datarow1.Item("DateCreated"), "dd-mmm-yyyy ttttt")
                            '==sDateCompleted = Format(rs1("DateCompleted"), "dd-mmm-yyyy ttttt")
                            sDateUpdated = VB6.Format(CDate(datarow1.Item("DateUpdated")), "dd-mmm-yyyy ttttt")
                            sCustomer = Left(datarow1.Item("Customer"), 32)
                            sJobStatus = Left(datarow1.Item("JobStatus"), 16)
                            sJobPriority = Left(datarow1.Item("Priority"), 1)
                            Select Case sJobPriority
                                Case "3"
                                    sPriorityDescr = sDescriptionPriority3
                                    curLabourRate = curLabourHourlyRateP3
                                Case "2"
                                    sPriorityDescr = sDescriptionPriority2
                                    curLabourRate = curLabourHourlyRateP2
                                Case Else
                                    sPriorityDescr = sDescriptionPriority1
                                    curLabourRate = curLabourHourlyRateP1
                            End Select
                            sTotalTime = CStr(datarow1.Item("TotalServiceTime"))
                            curJobTimeSpent = 0 '--add up sessions this job..-
                            curJobTimeSpentNC = 0 '--add up NC sessions this job..-
                            sRem = Trim(datarow1.Item("SessionTimes")) '--get all sessions this job..-
                            While (sRem <> "")
                                sName = "" : sSessionDate = "" : sTimeSpent = "" : sTimeSpentNC = ""
                                iPos = InStr(sRem, vbCrLf)
                                If iPos > 0 Then
                                    s1 = Trim(Left(sRem, iPos - 1))
                                    sRem = Trim(Mid(sRem, iPos + 2)) '--drop cf/lf-
                                Else
                                    s1 = Trim(sRem) : sRem = "" '--last session..-
                                End If '--ipos.-
                                '--dissect THIS session..-
                                If s1 <> "" Then
                                    iPos = InStr(s1, ":")
                                    If (iPos > 1) Then
                                        sSessionDate = Trim(Left(s1, iPos - 1))
                                        If Not IsDate(sSessionDate) Then
                                            sSessionDate = sDateUpdated '--in case of bad stuff.--
                                            dSessionDate = CDate(sDateUpdated)
                                        Else
                                            dSessionDate = CDate(sSessionDate)
                                            sSessionDate = VB6.Format(CDate(sSessionDate), "dd-mmm-yyyy") '--reformat..--
                                        End If
                                        'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 
                                        '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
                                        lngDayNumber = DateDiff(Microsoft.VisualBasic.DateInterval.Day, dateStart, dSessionDate) + 1 '-- day-1 is first day.-
                                        s1 = Trim(Mid(s1, iPos + 1))
                                        iPos2 = InStr(s1, "+")
                                        If (iPos2 > 0) Then
                                            sName = Trim(Left(s1, iPos2 - 1))
                                            If sName = "" Then sName = "YY_UNKNOWN"
                                            sName = Left(sName, 24)
                                            sTimeSpent = Trim(Mid(s1, iPos2 + 1))
                                            '--chargeable/NonCh.
                                            iPos3 = InStr(sTimeSpent, "-NC") '--17Apr2010--
                                            If (iPos3 > 0) Then '--no charge-
                                                '==bChargeable = False
                                                sTimeSpentNC = Replace(sTimeSpent, "-NC", "") '-get rid of marker..-
                                                sTimeSpent = "0" '--- no chargeable time..-
                                            Else '--chargeable..-
                                                sTimeSpentNC = "0"
                                            End If
                                        End If
                                    End If
                                End If '--s1
                                sTest = sTest & sSessionDate & "," & sName & "," & sTimeSpent & "; "
                                '--create session record.-

                                '== Include this session ONLY IF session date is in OUR select Period..==
                                '==  Filter out sessions from non-wanted periods.

                                If (sName <> "") And (sSessionDate <> "") And IsNumeric(sTimeSpent) And (dSessionDate >= dateStart) And (dSessionDate <= dateEnd) Then '-- sTimeSpent = ""
                                    sSessionDayOfWeek = msDayOfWeek(dSessionDate)
                                    '- compute session charge..
                                    curSessionCost = CDbl(sTimeSpent) * curLabourRate
                                    'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 
                                    '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
                                    '== GoSub QueryWork_MakeSessionLine '--make col1.-
                                    col1 = New Collection
                                    col1.Add(sJobId, "job_id")
                                    col1.Add(CStr(lngDayNumber), "daynumber")
                                    col1.Add(sDateCreated, "datecreated")
                                    col1.Add(sDateUpdated, "dateupdated")
                                    col1.Add(sCustomer, "customer")
                                    col1.Add(sJobStatus, "jobstatus")
                                    col1.Add(sJobPriority, "jobpriority")
                                    col1.Add(sPriorityDescr, "prioritydescr")
                                    col1.Add(sTotalTime, "totalservicetime")
                                    col1.Add(sName, "sessiontechname")
                                    col1.Add(sSessionDate, "sessiondate")
                                    col1.Add(sSessionDayOfWeek, "sessiondayofweek")
                                    col1.Add(sTimeSpent, "sessiontimespent")
                                    col1.Add(sTimeSpentNC, "sessiontimespent_nc")
                                    col1.Add(FormatCurrency(curSessionCost, 2), "sessioncost")
                                    colReportLines.Add(col1)
                                    curJobTimeSpent = curJobTimeSpent + CDec(sTimeSpent) '--accum session times this job..-
                                    curJobTimeSpentNC = curJobTimeSpentNC + CDec(sTimeSpentNC) '--accum NC session times this job..-
                                End If
                            End While '--sessions.=
                            '-- check for missing sessions !!! --
                            '--TotalTime includes NC sessions.-==30-Mar-2011==Rev2084==
                            '==Ver: 2.1.217  cur1 = CCur(sTotalTime) - curJobTimeSpent - curJobTimeSpentNC
                            '==Ver: 2.1.217  If (cur1 <> 0) Then '--session gap !!--
                            '==Ver: 2.1.217      sTimeSpent = Format(cur1, "##0.00") '--make dummy missing session
                            '==Ver: 2.1.217      sName = "YY_MISSING"
                            '==Ver: 2.1.217      sSessionDate = sDateUpdated
                            '==Ver: 2.1.217      GoSub QueryWork_MakeSessionLine  '--make col1.-
                            '==Ver: 2.1.217      colReportLines.Add col1
                            '==Ver: 2.1.217  End If  '--gap-
                            lngJobs = lngJobs + 1
                        End If '--isnull-
                        '= rs1.MoveNext()
                    Next datarow1  '=End While '-eof-
                    '= rs1.Close()
                    If gbDebug Then MsgBox(sTest, MsgBoxStyle.Information)
                    If colReportLines.Count() > 0 Then
                        '--create temp table for all sessions lines..--
                        '-- so we we can summarise by TechStaff..--
                        '--  First, DROP it in case it's still there..-
                        sSql = " DROP TABLE #WorkSessions "
                        '==v2.1.217=  Call gbExecuteCmd(cnnShape, sSql + vbCrLf, lngaffected, sErrorMsg)
                        'On Error Resume Next
                        'cnnShape.Execute(sSql, lngRecordsAff, ADODB.ExecuteOptionEnum.adExecuteNoRecords) '==v2.1.217=
                        'lngError = Err.Number
                        If Not gbExecuteCmd(cnnShape, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                            '--  ignore any DROP error..
                            If gbDebug Then
                                MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
                                Call gbLogMsg(gsErrorLogPath, sErrorMsg)
                            End If
                        End If
                        'If gbDebug Then
                        '    If lngError <> 0 Then '--NOT ok--
                        '        s1 = "ERROR: " & CStr(lngError) & "==" & Err.Description & vbCrLf & "=="
                        '        sErrorMsg = "Debug QueryWorkSessions:  Error in Executing Sql: " & vbCrLf & _
                        '                                         s1 & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf
                        '        MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
                        '        Call gbLogMsg(gsErrorLogPath, sErrorMsg)
                        '    End If
                        'End If '--debug-
                        'On Error GoTo QuerySessions_error

                        '-- Re-create temp table..--
                        sSql = " CREATE TABLE #WorkSessions (Session_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
                        sSql = sSql & " DayNumber int, Job_id int, DateUpdated datetime, DateCreated datetime, "
                        sSql = sSql & " JobStatus varchar(16), "
                        sSql = sSql & " JobPriority varchar(1), "
                        sSql = sSql & " PriorityDescr varchar(50), "
                        sSql = sSql & " Customer varchar(50), "
                        sSql = sSql & " TotalServiceTime decimal(6,2), "
                        sSql = sSql & " SessionTechname varchar(32), "
                        sSql = sSql & " SessionDate varchar(32), "
                        sSql = sSql & " SessionDayOfWeek varchar(16), "
                        sSql = sSql & " SessionTimespent decimal(6,2), "
                        sSql = sSql & " SessionTimespent_nc decimal(6,2), "
                        sSql = sSql & " SessionCost money )"

                        If Not gbExecuteCmd(cnnShape, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                            MsgBox(vbCrLf & "Failed SQL CREATE Temp table: " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        Else '-ok-
                            '--insert all session lines--
                            sSql = ""
                            curTotalTime = 0
                            curTotalTime_nc = 0
                            lngBatchCount = 0 '-- send small batches..-
                            lngTotalLines = 0
                            For Each col1 In colReportLines
                                sSql = sSql & " INSERT INTO #WorkSessions " & "(DayNumber, Job_id, DateUpdated, DateCreated, JobStatus,JobPriority, PriorityDescr, " & " Customer, TotalServiceTime, " & " SessionTechname, SessionDate, SessionDayOfWeek, SessionTimespent, SessionTimespent_nc, SessionCost ) "
                                'UPGRADE_WARNING: Couldn't resolve default property of object col1(). Click for more: 
                                '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                                sSql = sSql & " VALUES ( " & col1.Item("daynumber") & ", "
                                sSql = sSql & col1.Item("job_id") & ", "
                                sSql = sSql & "'" & col1.Item("dateupdated") & "', "
                                sSql = sSql & "'" & col1.Item("datecreated") & "', "

                                sSql = sSql & " '" & CStr(col1.Item("jobstatus")) & "', "
                                sSql = sSql & " '" & CStr(col1.Item("jobpriority")) & "', "
                                sSql = sSql & " '" & CStr(col1.Item("prioritydescr")) & "', "
                                '==sSql = sSql + " '" + col1("dateupdated") + "', "
                                sSql = sSql & " '" & msFixSqlStr(CStr(col1.Item("customer"))) & "', "
                                sSql = sSql & CStr(col1.Item("totalservicetime")) & ", "
                                sSql = sSql & "'" & CStr(col1.Item("sessiontechname")) & "', "
                                sSql = sSql & "'" & CStr(col1.Item("sessiondate")) & "', "
                                sSql = sSql & "'" & CStr(col1.Item("sessiondayofweek")) & "', "
                                sSql = sSql & CStr(col1.Item("sessiontimespent")) & ", "
                                sSql = sSql & CStr(col1.Item("sessiontimespent_nc")) & ", "
                                sSql = sSql & CStr(col1.Item("sessioncost")) & "  ); " & vbCrLf
                                '== If gbDebug Then MsgBox "Insert SQL is: " & vbCrLf & sSql, vbInformation

                                curTotalTime = curTotalTime + CDec(col1.Item("sessiontimespent"))
                                curTotalTime_nc = curTotalTime_nc + CDec(col1.Item("sessiontimespent_nc"))
                                lngBatchCount = lngBatchCount + 1
                                lngTotalLines = lngTotalLines + 1
                                If (lngBatchCount >= 50) Or ((lngBatchCount <= 50) And (lngTotalLines >= colReportLines.Count())) Then
                                    '--send every 50, or last batch..-
                                    sTest = "Inserting " & lngJobs & " jobs.. " & lngBatchCount & " rows..  sql is:" & vbCrLf & sSql '--test--
                                    msResults = sTest & vbCrLf & "== the end ==  " & vbCrLf
                                    If gbDebug Then
                                        '= Call CmdCopy_Click
                                        My.Computer.Clipboard.Clear() ' Clear Clipboard.
                                        My.Computer.Clipboard.SetText(msResults) '-- Put result text on Clipboard.
                                        MsgBox("Check clipboard for all INSERT BATCH SQL..")
                                    End If '--test-
                                    '-- END TEST.--
                                    If Not gbExecuteCmd(cnnShape, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                                        MsgBox(vbCrLf & "Failed SQL INSERTs into Temp table: " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                        Exit Function
                                    End If '--insert ok-
                                    sSql = "" : lngBatchCount = 0
                                End If '--batch
                            Next col1 '--col1
                            '-- insert FINAL TOTAL ROW.. --
                            '== sSql = " INSERT INTO #WorkSessions " + _
                            ''==       "(Job_id, SessionTechname,SessionDate,SessionTimespent, SessionTimespent_nc ) " + _
                            ''==        " VALUES (0, 'ZZ_FINALTOTAL',CURRENT_TIMESTAMP, " + CStr(curTotalTime) + ", " + CStr(curTotalTime_nc) + "); "
                            '--MsgBox "Inserting " & lngJobs & " jobs.. " & _
                            ''--                   colReportLines.Count & " rows..  sql is:" + vbCrLf + sSql, vbInformation '--test--
                            '--insert the batch..--
                            '== If Not gbExecuteCmd(cnnShape, sSql + vbCrLf, lngaffected, sErrorMsg) Then
                            '==          Screen.MousePointer = vbDefault
                            '==           MsgBox vbCrLf + "Failed SQL INSERT FINAL TOTAL into Temp table: " + vbCrLf + sErrorMsg, vbCritical
                            '== Else '--ok-
                            '== End If  '-inserted.-
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            '-- now read temp file as report..-
                            If gbDebug Then
                                '--TEST..-   check recordset..--
                                sSql = " SELECT * FROM #WorkSessions "
                                'If Not gbGetRst(cnnShape, rs1, sSql) Then
                                '    MsgBox("Failed to get TEST Sessions recordset..", MsgBoxStyle.Exclamation)
                                'End If
                                cmd1 = New OleDbCommand(sSql, cnnShape)
                                Dim colResults As Collection
                                colResults = New Collection
                                Try
                                    rdr1 = cmd1.ExecuteReader
                                    sRem = gbShowQueryResults(rdr1, "#WorkSessions", colResults)
                                    My.Computer.Clipboard.Clear() ' Clear Clipboard.
                                    My.Computer.Clipboard.SetText(sRem) '-- Put result text on Clipboard.
                                    MsgBox("Check clipboard for all session rows..")
                                Catch ex As Exception
                                    MsgBox("Failed to get TEST Sessions recordset..", MsgBoxStyle.Exclamation)
                                End Try
                                '-- END TEST.--
                            End If '--debug-
                            '--MsgBox "OK..  inserted " & lngaffected & " session lines..", vbInformation
                            '--  shape..--

                            '== Ver: 2.1.211  ==
                            '==  Timesheets- New Hierarchy-
                            '--  BEWARE the CURLY brackets around the SELECT statements..-
                            '--  BEWARE the CURLY brackets around the SELECT statements..-
                            '--  BEWARE the CURLY brackets around the SELECT statements..-
                            sSql = " SHAPE {SELECT DISTINCT SessionTechName AS TechName FROM #WorkSessions ORDER BY TechName} "
                            sSql = sSql & " APPEND ( " '--days-
                            sSql = sSql & "     (SHAPE {SELECT DISTINCT DayNumber,SessionDayOfWeek, SessionTechName AS TechName, SessionDate "
                            sSql = sSql & "                      FROM #WorkSessions ORDER BY DayNumber } "
                            sSql = sSql & "        APPEND ( " '--Jobs--
                            sSql = sSql & "           (SHAPE { SELECT DISTINCT  Job_Id, JobStatus, JobPriority, PriorityDescr, "
                            sSql = sSql & "                          SessionTechName AS TechName, DayNumber, Customer "
                            sSql = sSql & "                       FROM #WorkSessions  ORDER BY Job_id } "
                            sSql = sSql & "               APPEND ( "
                            sSql = sSql & "      {SELECT Job_id, SessionTechName AS TechName, DayNumber, SessionDayOfWeek, "
                            sSql = sSql & "                     sessiontimespent AS Hours_chg, sessiontimespent_nc AS Hours_nc, SessionCost "
                            sSql = sSql & "                       FROM #WorkSessions  } "
                            sSql = sSql & "               AS chapSessions  RELATE  " '--append sessions.-
                            sSql = sSql & "                  TechName TO TechName, DayNumber TO DayNumber, Job_id to Job_id )) "
                            sSql = sSql & "        AS chapJobs  RELATE TechName TO TechName, DayNumber TO DayNumber ) " '--Append-Jobs--
                            sSql = sSql & "     )" '-append-days-
                            sSql = sSql & " AS chapDays RELATE TechName TO TechName) "
                            sSql = sSql & " "
                            '--   Call mbAllJobPartsQuery(sSql)
                            sShapeSql = sSql '--pass back sql for query..-
                            gbQueryWorkSessions = True
                            '-- set col widths.-
                            '--End If
                        End If '--create..-
                    Else '--no lines-
                        MsgBox("No Session records to show..", MsgBoxStyle.Exclamation)
                    End If '-reportlines.-
                    '--mbLoadsystemInfo = True
                End If '--rs-
            End If '--get rs-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '= rs1 = Nothing
            '---mlStaffTimeout = 0  '--start timing out..--
            Exit Function

            'QueryWork_MakeSessionLine:
            '        col1 = New Collection
            '        col1.Add(sJobId, "job_id")
            '        col1.Add(CStr(lngDayNumber), "daynumber")
            '        col1.Add(sDateCreated, "datecreated")
            '        col1.Add(sDateUpdated, "dateupdated")
            '        col1.Add(sCustomer, "customer")
            '        col1.Add(sJobStatus, "jobstatus")
            '        col1.Add(sJobPriority, "jobpriority")
            '        col1.Add(sPriorityDescr, "prioritydescr")
            '        col1.Add(sTotalTime, "totalservicetime")
            '        col1.Add(sName, "sessiontechname")
            '        col1.Add(sSessionDate, "sessiondate")
            '        col1.Add(sSessionDayOfWeek, "sessiondayofweek")
            '        col1.Add(sTimeSpent, "sessiontimespent")
            '        col1.Add(sTimeSpentNC, "sessiontimespent_nc")
            '        col1.Add(FormatCurrency(curSessionCost, 2), "sessioncost")
            '        Return
        Catch ex As Exception
            MsgBox("Error in gbQueryWorkSessions.." & vbCrLf & _
                       ex.Message, MsgBoxStyle.Exclamation)
        End Try '-main try-

        'QuerySessions_error:
        '        lngError = Err().Number
        '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '        MsgBox("Error in gbQueryWorkSessions.." & vbCrLf & _
        '                  "Error=" & lngError & "(" & ErrorToString(lngError) & ").", MsgBoxStyle.Exclamation)

    End Function '--timesheet query..-
    '= = = = = = = = = = = = = =
    '-===FF->


    '=== end module--
End Module