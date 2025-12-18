Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class clsSystemInfo

    '==JobMatix Build 3072==
    '--= grh 08-Feb-2013==
    '==   Class created for NewJob form support..=
    '== 
    '==  grh. JobMatix 3.1.3101 ---  19-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==
    '==    >> All systemInfo work now via THIS Class clsSystemInfo ..
    '==
    '==    -3401.0417 17-April-2017=
    '==      >> Fix to update DateUpdated when updating..-
    '==
    '==  NEW DLL- 4219 VERSION
    '==    -- 4219.1125 25-Nov-2019= 
    '==        -- Now is Friend SysInfo for internal use in this dll..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    Private mColSystemInfo As Collection
    Private mSdSystemInfo As clsStrDictionary
    Private mCnnSql As OleDbConnection  '== ADODB.Connection

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- load systemInfo settings..--
    '---  send back a collection of collections (rows..)--

    Private Function mbLoadsystemInfo(ByRef colInfo As Collection, _
                                          ByRef sdSystemInfo As clsStrDictionary) As Boolean
        Dim sSql As String
        Dim sKey, sValue As String
        Dim rs1 As DataTable  '== ADODB.Recordset
        Dim col1 As Collection
        '== Dim date1, date2 As Date

        mbLoadsystemInfo = False
        sSql = "Select * from [systemInfo] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get systemInfo recordset.." & vbCrLf & _
            "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--build dictionary of sysinfo items....-
            colInfo = New Collection '--  holds system settings..
            sdSystemInfo = New clsStrDictionary  '== Scripting.Dictionary
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '= If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add item....
                    col1 = New Collection
                    sKey = UCase(Trim(dataRow1.Item("systemKey")))
                    col1.Add(sKey, "systemkey")
                    col1.Add(Trim(dataRow1.Item("systemValue")), "systemvalue")
                    col1.Add(CDate(dataRow1.Item("dateCreated")), "datecreated")
                    col1.Add(CDate(dataRow1.Item("dateUpdated")), "dateupdated")
                    If (sKey <> "") Then
                        colInfo.Add(col1, sKey)
                    End If
                    '-- build Dictionary also..--
                    sdSystemInfo.Add(sKey, Trim(dataRow1.Item("systemValue")))
                Next dataRow1
                mbLoadsystemInfo = True
            End If '--rs nothing/empty=-
        End If '--get rs-
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--load info..--.-
    '= = = = = =
    '-===FF->

    Private Function mbRefresh() As Boolean

        mbRefresh = False

        If mbLoadsystemInfo(mColSystemInfo, mSdSystemInfo) Then
            mbRefresh = True
        End If
    End Function  '--refresh--
    '= = = = = = = = = = = 

    '-- Constructor- I n i t ----

    Public Sub New(ByRef cnn1 As OleDbConnection)
        MyBase.New()

        mCnnSql = cnn1  '-save-

        Call mbRefresh()   '--load current system info..--
    End Sub '--init..--
    '= = = = = = = = = = = = =
    '-===FF->

    '- P u b l i c  M e t h o d s -
    '- P u b l i c  M e t h o d s -

    '-- E x i s t s ----
    '-- Key is NOT case sensitive..

    Public Function exists(ByVal sKey As String) As Boolean

        Call mbRefresh()
        exists = mColSystemInfo.Contains(sKey)

    End Function  '--exists--
    '= = = = = = = = = = = =

    '--  refresh all --
    '--  reload variables.--
    Public Function refreshAll() As Boolean
        refreshAll = mbRefresh()
    End Function  '--exists--
    '= = = = = = = = = = = =
    '-===FF->

    '- Contains-

    Public Function contains(ByVal sKey As String) As Boolean
        Call mbRefresh()
        contains = mColSystemInfo.Contains(sKey)

    End Function  '-contains-
    '= = = = = = = = == = = = = =

    '--  retrieve item --
    '-- Key is NOT case sensitive..

    Public Function item(ByVal sKey As String) As String
        Dim strResult As String = ""
        Dim col1 As Collection
        Call mbRefresh()
        If mColSystemInfo.Contains(sKey) Then
            Try
                col1 = mColSystemInfo.Item(sKey)
                strResult = CStr(col1.Item("systemValue"))
            Catch ex As Exception
                MsgBox("clsSystemInfo: Error retrieving collection item: " & sKey & vbCrLf & _
                       ex.Message, MsgBoxStyle.Exclamation) '
                strResult = ""
            End Try
        End If  '-contains.-
        item = strResult
    End Function  '--item--
    '= = = = = = = = = = = = = = = = = = =

    '--  retrieve item Date Created--
    '-- Key is NOT case sensitive..

    Public Function itemDateCreated(ByVal sKey As String, _
                                    ByRef dateCreated As Date) As Boolean
        Dim col1 As Collection
        Call mbRefresh()
        itemDateCreated = False
        If mColSystemInfo.Contains(sKey) Then
            Try
                col1 = mColSystemInfo.Item(sKey)
                dateCreated = col1.Item("dateCreated")
                itemDateCreated = True
            Catch ex As Exception
                MsgBox("clsSystemInfo: Error retrieving collection item date: " & sKey & vbCrLf & _
                       ex.Message, MsgBoxStyle.Exclamation) '
            End Try
        End If  '-contains.-
    End Function  '--item date--
    '= = = = = = = = = = = = = = = = = = =

    '- get all keys..-

    Public Function keys() As ICollection

        Call mbRefresh()
        keys = mSdSystemInfo.Keys
    End Function  '-keys-
    '= = = = = = = = = = = =
    '-===FF->

    '- Update some items.-

    '-- vaInfo is array of (key,value,key,value...)
    '---- if key exists then UPDATE, else INSERT..--

    Public Function UpdateSystemInfo(ByRef vaInfo As Object) As Boolean
        Dim sSql As String
        Dim sKey, sValue As String
        Dim L1, ix As Integer
        Dim sErrors As String
        '== Dim transaction1 As SqlTransaction2

        UpdateSystemInfo = False
        sSql = " BEGIN TRANSACTION " & vbCrLf
        ix = 0
        While ix <= UBound(vaInfo)
            sKey = vaInfo(ix)
            sValue = gsFixSqlStr(CStr(vaInfo(ix + 1)))
            sSql = sSql & "IF EXISTS (SELECT * FROM [SystemInfo] WHERE (SystemKey='" & sKey & "'))" & vbCrLf
            sSql = sSql & "  UPDATE [SystemInfo] SET " & _
                                     " systemValue='" & sValue & "', " & vbCrLf & _
                                     " DateUpdated= CURRENT_TIMESTAMP " & vbCrLf & _
                          " WHERE (SystemKey='" & sKey & "')" & vbCrLf
            sSql = sSql & "ELSE INSERT INTO [SystemInfo] (SystemKey,systemValue) " & _
                                    " VALUES ('" & sKey & "', '" & sValue & "') " & vbCrLf
            ix = ix + 2
        End While '--ix--
        sSql = sSql & " COMMIT TRANSACTION "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
            MsgBox("!! Failed in UPDATE jobTracking systemInfo.." & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Critical)
        Else '--ok--
            UpdateSystemInfo = True
        End If
        Call mbRefresh()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '--update-
    '= = = = = = =  =  = =


End Class  '--systemInfo--
