
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Collections.Generic

Public Class clsTags

    '-- get/save collection of TAG reference list..

    '==
    '= grh 23-Jan-2020-=
    '=
    '= = = = = = = = = = = = = = = = = = = = = ==  =

    '-sysingo key for TAGS ref list.

    Private Const k_CustTagsReferenceList As String = "POS_CUST_TAGS_REF_LIST"

    '-- STORED Tags list are always stored in square brackets separated by commas..
    '---  ie  in the Form:  [tag1],[tag2], etc 

    ' --- When needed,  they are passed to caller as a collection of naked TAG strings.

    Private mCnnSql As OleDbConnection
    Private mSysInfo1 As clsSystemInfo

    Private mbSystemReady As Boolean = False

    '= = = = = = =  = = = = ==  = ==== === == 

    '- mbDecodeTagList-
    '-- Reurns a collection.

    Private Function mbDecodeTagList(ByVal sTagList As String, _
                                     ByRef colTagList As Collection) As Boolean
        mbDecodeTagList = False

        If sTagList Is Nothing OrElse sTagList = "" Then
            Exit Function
        End If
        colTagList = New Collection
        Dim sTags() As String = Split(sTagList, ",")

        For Each sItem As String In sTags
            sItem = Trim(sItem)
            If VB.Left(sItem, 1) = "[" Then
                sItem = Trim(sItem.Substring(1))  '-start from second Char..
            End If
            If VB.Right(sItem, 1) = "]" Then
                sItem = Trim(Left(sItem, sItem.Length - 1))  '-start from second Char from right...
            End If
            colTagList.Add(sItem)
        Next sItem
        mbDecodeTagList = True
    End Function  '- mbDecodeTagList-
    '= = = = = = = = == = = = = = = = = = = = =

    '-- Make comma-separated Tag List..
    '--  ie Dump collection into string list of Tags...
    '-mbMakeTagList-

    Private Function mbMakeTagList(ByRef colTagList As Collection, _
                                   ByRef sTagList As String) As Boolean

        mbMakeTagList = False
        If colTagList Is Nothing Then
            Exit Function
        End If
        Dim sNewList As String = ""

        For Each sTag As String In colTagList
            If sNewList <> "" Then
                sNewList &= ","
            End If
            sNewList &= "[" & sTag & "]"
        Next sTag
        sTagList = sNewList
        mbMakeTagList = True
    End Function  '-mbMakeTagList-
    '= = = =  = = = = = = = = 
    '-===FF->

    '=4221 - Check if Column Exists..

    Private Function mbDoesTableColumnExist(ByRef cnnSql As OleDbConnection, _
                                         ByVal sTableName As String, _
                                         ByVal sColumnName As String) As Boolean
        Dim rdr1 As OleDbDataReader
        Dim sSql, sErrorMsg As String
        Dim intAffected As Integer

        mbDoesTableColumnExist = False
        sSql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS " & vbCrLf &
                            " WHERE (TABLE_NAME = '" & sTableName & "') " & _
                                " AND (COLUMN_NAME = '" & sColumnName & "')" & vbCrLf
        If gbGetReader(cnnSql, rdr1, sSql) Then  '--check if row exists..-
            If rdr1.HasRows Then '-column already exists..-
                mbDoesTableColumnExist = True  '- is ok-
                rdr1.Close()
                Exit Function
            Else  '--doesn't exist.. must create.
                rdr1.Close()
            End If '-rdr-
        Else  '-get rdr error
            '--  GET error text !!--
            MsgBox("mbDoesTableColumnExist: Error in reading INFORMATION_SCHEMA.COLUMNS.." & vbCrLf & _
                                  gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
        End If  '--get--
    End Function '-gbDoesTableColumnExist
    '= = = = = = = = = = = = = = = = =

    '-- new--constructor..
    '-- new--constructor..
    '-- new--constructor..

    Public Sub New(ByRef cnn1 As OleDbConnection)

        MyBase.New()
        mCnnSql = cnn1
        Try
            mSysInfo1 = New clsSystemInfo(mCnnSql)
        Catch ex As Exception
            MsgBox(" Error in new Tags class.. Failed to create SystemInfo class.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try
        '-- Check that Tags column has been created in Customer Table..
        '-- In case we came from JobTracking before running latest POS.
        If Not mbDoesTableColumnExist(mCnnSql, "Customer", "Tags") Then
            MsgBox(" Error in new Tags class.. No Tags column in Customer Table.." & vbCrLf & _
                       "POS system needs to run first in Build 4221 or later.", MsgBoxStyle.Exclamation)
        Else  '- ok-
            mbSystemReady = True
        End If  '-exists-
    End Sub  '-new-
    '= = = = = = == =  =
    '-===FF->

    '-- P u b l i c --
    '-- P u b l i c --
    '-- P u b l i c --

    '-- get current list of defined Cust tags..-

    Public Function GetCustTagRefList(ByRef colRefTags As Collection) As Boolean

        GetCustTagRefList = False
        If Not mbSystemReady Then
            MsgBox(" Error in Tags class.. Not Initialised..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        Call mSysInfo1.refreshAll()

        If mSysInfo1.contains(k_CustTagsReferenceList) Then
            colRefTags = New Collection

            Dim sItem As String
            Dim sTagList As String = mSysInfo1.item(k_CustTagsReferenceList)
            '-- comma separated..
            'Dim sTags() As String = Split(sTagList, ",")
            If mbDecodeTagList(sTagList, colRefTags) Then
                GetCustTagRefList = True
            End If
        End If
    End Function  '-GetTagList-
    '= = = = = = = === = =  = =
    '-===FF->

    '-- Save new CUST. list (complete..)

    '== USES Public Function UpdateSystemInfo(ByRef vaInfo As Object) As Boolean =

    Public Function SaveCustTagRefList(ByRef colNewList As Collection) As Boolean
        Dim sNewList As String = ""
        SaveCustTagRefList = False

        If Not mbSystemReady Then
            MsgBox(" Error in Tags class.. Not Initialised..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If colNewList Is Nothing Then
            Exit Function
        End If
        Try
            If mbMakeTagList(colNewList, sNewList) Then
                '-  update..
                Dim asItems() As String = {k_CustTagsReferenceList, sNewList}
                If Not mSysInfo1.UpdateSystemInfo(asItems) Then
                    MsgBox("Failed Saving new Tag list.", MsgBoxStyle.Exclamation)
                Else '-ok-
                    SaveCustTagRefList = True
                End If
            End If
        Catch ex As Exception
            MsgBox("Failed making new Tag list." & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function  '-SaveTagList-
    '= = = = = = = = ==  ==  == 

    '-- Decode, encode..

    Public Function MakeTagList(ByRef colTagList As Collection, _
                                   ByRef sNewList As String) As Boolean
        MakeTagList = False
        If Not mbSystemReady Then
            MsgBox(" Error in Tags class.. Not Initialised..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        MakeTagList = mbMakeTagList(colTagList, sNewList)

    End Function  '-make string list=
    '= = = = = = = = = = = = = =

    '-- String list to collection..

    Public Function DecodeTagList(ByVal sTagList As String, _
                                     ByRef colTagList As Collection) As Boolean
        DecodeTagList = False
        If Not mbSystemReady Then
            MsgBox(" Error in Tags class.. Not Initialised..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        DecodeTagList = mbDecodeTagList(sTagList, colTagList)

    End Function  '-decode.
    '= = = = = = = = = = == =  =
    '-===FF->

    '-- get collection of current tags for a Customer...-

    Public Function GetCustomerTags(ByVal intCustomer_id As Integer, _
                                    ByRef sCustBarcode As String, _
                                    ByRef sCustName As String, _
                                     ByRef colCustomerTags As Collection) As Boolean
        Dim sOldCustTagList As String
        Dim sSql, sName, sCompany As String
        Dim colResult, colRecord As Collection

        GetCustomerTags = False
        If Not mbSystemReady Then
            MsgBox(" Error in Tags class.. Not Initialised..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '--  get CUSTOMER record as collection for SELECT..--
        sSql = "SELECT * FROM [customer] WHERE (customer_id=" & CStr(intCustomer_id) & ");"
        If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                               (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
            '--have a row..-
            colRecord = colResult.Item(1)
            sCustBarcode = colRecord.Item("barcode")("value")
            sName = colRecord.Item("firstName")("value") & " " & colRecord.Item("lastName")("value")
            sCompany = Trim(colRecord.Item("companyName")("value"))
            sCustName = IIf((sCompany <> ""), sCompany & vbCrLf & sName, sName)
        Else '--not found..-
            MsgBox("No Customer found for ID: " & intCustomer_id, MsgBoxStyle.Exclamation)
             Exit Function
        End If  '-get--
        '-ok=
        sOldCustTagList = Trim(colRecord.Item("Tags")("value"))
        '-- send back tags for this customer...
        If (sOldCustTagList <> "") Then
            '- decode into a collection.-
            If DecodeTagList(sOldCustTagList, colCustomerTags) Then
                GetCustomerTags = True
            Else
                MsgBox("Failed decode Customer the tag list.." & vbCrLf & _
                     """" & sOldCustTagList & """", MsgBoxStyle.Exclamation)
            End If
        Else
            '--empty list.
            colCustomerTags = New Collection
            GetCustomerTags = True   '--empty is ok..
        End If

    End Function  '- GetCustomerTags-
    '= = = = = = = = = = = = = = = = = =


End Class '= clsTags..
'= = == = = = = = = = = 

