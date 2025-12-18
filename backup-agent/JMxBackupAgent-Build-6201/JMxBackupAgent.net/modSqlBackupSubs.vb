
Imports System.Data
Imports System.Data.OleDb

Module modSqlBackupSubs

    '==  Background Scheduled task to backup JobMatix Database..

    '==     -- Console Application 3501.0821  21Aug2018=  ...
    '==
    '==    SQL Subs for Console task..
    '== 
    '=== = = = = = = = = = = = = = = = = ==  = = = = 


    '==  Background Scheduled task to backup JobMatix Database..

    '- Copyright 2021 grhaas@outlook.com

    '- Licensed under the Apache License, Version 2.0 (the "License");
    '- you may Not use this file except In compliance With the License.
    '- You may obtain a copy Of the License at

    '-    http://www.apache.org/licenses/LICENSE-2.0

    '- Unless required by applicable law Or agreed To In writing, software
    '- distributed under the License Is distributed On an "AS IS" BASIS,
    '- WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
    '- See the License For the specific language governing permissions And
    '- limitations under the License.

    '= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 



    Private msSqlVersion As String = ""
    Private mIntJobMatixDBid As Integer = -1

    Private mlngSqlMajorVersion As Integer = -1
    Private mbIsSqlAdmin As Boolean = False

    Private msLastSqlErrorMessage As String = ""

    '= = = = = = = = =  == =  = = = = = = ==  == =

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

        '== pm1 = cmd1.Parameters.Add("RETURN_VALUE", SqlDbType.Int)
        '=3103.0221 - Use OleDbType ==
        pm1 = cmd1.Parameters.Add("RETURN_VALUE", OleDbType.Integer)
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
                    '== pm1 = cmd1.Parameters.Add(sParamName, SqlDbType.NVarChar, 12)
                    '=3103.0221 - Use OleDbType ==
                    pm1 = cmd1.Parameters.Add(sParamName, OleDbType.LongVarWChar, 31)
                    pm1.Value = s1
                Else '--numeric-
                    '== pm1 = cmd1.Parameters.Add(sParamName, SqlDbType.Int)
                    '=3103.0221 - Use OleDbType ==
                    pm1 = cmd1.Parameters.Add(sParamName, OleDbType.Integer)
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

    '= 3103.0221 - Added on.. ==
    '-- cover for glExecSp --
    '--  Dump rdr into datatable for caller..-


    Public Function gbExecSp2(ByRef cnn1 As OleDbConnection, _
                                  ByVal sProcName As String, _
                                   ByVal sParms As String, _
                                    ByRef sError As String, _
                                     ByRef dtResults As DataTable) As Boolean

        Dim rdrResults As OleDbDataReader
        Dim intResult As Integer

        intResult = glExecSP(cnn1, sProcName, sParms, sError, rdrResults)
        If (intResult = 0) Then '-ok-
            dtResults = New DataTable
            dtResults.Load(rdrResults)
            gbExecSp2 = True '--blnSuccess = True
            rdrResults.Close()
        Else
            gbExecSp2 = False  '-- s/be -2..
            '== MsgBox(sError, MsgBoxStyle.Exclamation)
        End If

    End Function  '-gbExecSp2-
    '= = = = = = = = = = = = = = = = =
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
    '-===FF->

    '-- IF Current SERVER Instance Version-
    '--        is SQL-Server 2005 or later.--

    Public Function gbIsSqlServer2005Plus() As Boolean

        gbIsSqlServer2005Plus = (mlngSqlMajorVersion >= 9)  '-- 9=2005,  10=2008..--

    End Function '-2005Plus.-
    '= = = = = = = = = = = =  

    '--  2 0 0 8  ---

    '-- IF Current SERVER Instance Version-
    '--        is SQL-Server 2008 or later.--

    Public Function gbIsSqlServer2008Plus() As Boolean

        gbIsSqlServer2008Plus = (mlngSqlMajorVersion >= 10)  '-- 9=2005,  10=2008..--

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
            If (gsErrorLogPath() <> "") Then
                Call gbLogMsg(gsErrorLogPath, s2 & vbCrLf & "-- end of error msg.--")
            End If '--log--

        End Try
    End Function '--connect--
    '= = = = = = = = = = = = = = = = 
    '-===FF->


End Module '-modSqlBackupSubs-
'= = = = = = = = = = = = = = =
