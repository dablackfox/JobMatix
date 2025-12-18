Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
Imports VB = Microsoft.VisualBasic
'==Imports System.Data
'= Imports System.Data.OleDb
Imports System.IO
Imports System.Security.AccessControl
Imports System.Text

Public Class clsLocalSettings

    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==
    '==    >> All Local settings work now via THIS Class. ..
    '==
    '==  >> 3301.429= 29Apr2016-  (POS dll).
    '==           To ensure keys NOT case-sensitive-  Drop "mSdSettings As clsStrDictionary"..
    '==             (No need now for UCASE keys..)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

    Private mColSettings As Collection
    '==3301.429= Private mSdSettings As clsStrDictionary
    Private msFullLocalPath As String = ""

    Private msLastFileErrorMessage As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '== 3107.805== 
    '== here in 331.214--
    '==   Adds an ACL entry on the specified file for the specified account. 
    Private Function AddFileSecurity(ByVal strFileName As String, _
                         ByVal account As String, _
                          ByVal rights As FileSystemRights, _
                           ByVal controlType As AccessControlType) As Boolean

        ' Get a FileSecurity object that represents the  
        ' current security settings. 
        Dim fSecurity As FileSecurity = File.GetAccessControl(strFileName)

        ' Add the FileSystemAccessRule to the security settings.  
        Dim accessRule As FileSystemAccessRule = _
            New FileSystemAccessRule(account, rights, controlType)

        fSecurity.AddAccessRule(accessRule)
        Try
            ' Set the new access settings.
            File.SetAccessControl(strFileName, fSecurity)
            AddFileSecurity = True
        Catch ex As Exception
            MsgBox("Failed to Update ACL for File: " & vbCrLf & strFileName & vbCrLf & vbCrLf & ex.Message)
            AddFileSecurity = False
        End Try
    End Function  '-AddFileSecurity-
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--create/write TEXT file==
    '--create/write file==
    '== 3107.806= Re-written for .Net. ==
    '== 3107.806= Optionally Add Security ==

    Private Function mIntSaveTextFile(ByRef sFileSavePath As String, _
                                      ByVal sNewFileData As String, _
                                      Optional ByVal bMakePublic As Boolean = True) As Integer
        Dim ok As Boolean
        Dim lNumberOfBytesRead As Integer
        Dim lTotalSize As Integer
        Dim lTimeout As Integer
        Dim intFileHandle As Integer
        Dim iResult, j, i, k, lPos As Integer
        Dim s1, s2, sMsg As String

        msLastFileErrorMessage = ""
        mIntSaveTextFile = -1
        '-- WriteAllText overwrites any existing file..-
        Try
            File.WriteAllText(sFileSavePath, sNewFileData)
        Catch ex As Exception
            sMsg = "Failed to save Text File: " & vbCrLf & sFileSavePath & vbCrLf & vbCrLf & ex.Message
            MsgBox(sMsg)
            msLastFileErrorMessage = sMsg
            mIntSaveTextFile = -2  '= ex.HResult
            Exit Function
        End Try
        '-- make public for everyone..--
        If bMakePublic Then
            If AddFileSecurity(sFileSavePath, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow) Then
                mIntSaveTextFile = 0
            End If
        End If  '-public-
    End Function '--save text--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  LOCAL Settings file functions..--
    '--  LOCAL Settings file functions..--

    '--load local settings.. eg printer names, sql serrvername..
    Private Function mbLoadSettings() As Boolean
        Dim handle_Renamed As Short
        Dim lResult, k2 As Integer
        Dim sPath As String
        Dim sLine As String
        Dim sKey, sValue As String
        Dim col1 As Collection

        sPath = msFullLocalPath  '== gsLocalJobsSettingsPath()  '= msAppPath & K_SAVESETTINGSPATH
        '==3301.429= mSdSettings = New clsStrDictionary  '== Scripting.Dictionary '--  holds local job settings..
        mColSettings = New Collection
        '--check if exists..--
        If (Dir(sPath) <> "") Then '--exists-
            '--lResult = openByte(sPath, "RS", handle)
            handle_Renamed = FreeFile()
            On Error Resume Next
            FileOpen(handle_Renamed, sPath, OpenMode.Input)
            lResult = Err().Number
            If (lResult <> 0) Then
                MsgBox("Can't open settings file.." & vbCrLf & _
                       sPath & vbCrLf & "Error is: " & lResult & _
                             "=" & ErrorToString(lResult), MsgBoxStyle.Critical)
            Else '--ok-
                '--read all lines into dictionary (collection)..--
                While Not EOF(handle_Renamed)
                    sLine = Trim(LineInput(handle_Renamed))
                    '--sep lhs/rhs--
                    sValue = "" : sKey = ""
                    '- must be valid key-
                    If (InStr("_abcdefghijklmnopqrstuvwxyz0123456789", LCase(VB.Left(sLine, 1))) > 0) Then  '-ok-
                        k2 = InStr(1, sLine, "=")
                        If (k2 > 1) Then '--we have lhs/rhs
                            sKey = Trim(VB.Left(sLine, k2 - 1))
                            '=UCase(Trim(VB.Left(sLine, k2 - 1))) '--get name--
                            sValue = Trim(Mid(sLine, k2 + 1)) '--get value--
                            '--colItems.Add sValue, sName                 '--name is key--
                        Else
                            sKey = Trim(sLine) '= UCase(Trim(sLine)) '--  no rhs-
                        End If
                        If sKey <> "" Then
                            '==3301.429= mSdSettings.Add(sKey, sValue)
                            col1 = New Collection
                            col1.Add(sKey, "key")
                            col1.Add(sValue, "value")
                            mColSettings.Add(col1, sKey)  '-load collection also-
                        End If
                    End If  '-key ok-
                End While '--lines-
                mbLoadSettings = True
                FileClose(handle_Renamed)
            End If '--open..-
        End If '--exists--
    End Function '--load settings..--
    '= = = = = =  =  == = = == ==
    '-===FF->

    '-refresh-

    Private Function mbRefresh() As Boolean

        mbRefresh = False
        If mbLoadSettings() Then
            mbRefresh = True
        End If
    End Function  '--refresh--
    '= = = = = = = = = = = 

    '-- Constructor- I n i t ----
    '-  Must have full Local path incl. file-name

    Public Sub New(ByVal strSettingsFullPath As String)
        MyBase.New()

        '-path includes file-name
        msFullLocalPath = strSettingsFullPath

        Call mbRefresh()   '--load current system info..--
    End Sub '--init..--
    '= = = = = = = = = = = = =

    '-- P u b l i c  M e t h o d s  
    '-- P u b l i c  M e t h o d s  
    '-- P u b l i c  M e t h o d s  

    '-- E x i s t s ----
    '-- Key is NOT case sensitive..

    Public Function exists(ByVal sKey As String) As Boolean
        '==exists = mSdSettings.Exists(sKey)
        exists = mColSettings.Contains(sKey)

    End Function  '--exists--
    '= = = = = = = = = = = =

    '--  refresh all --
    '--  reload variables.--
    Public Function refreshAll() As Boolean
        refreshAll = mbRefresh()
    End Function  '--exists--
    '= = = = = = = = = = = =
    '-===FF->

    '-- Update one setting..--
    '----change the setting in the static var dictionary..--
    '----- Write the dictionary back to disk..--
    '== Fixed 3311.327 to return correct result.

    Public Function SaveSetting(ByVal sKey As String, _
                                 ByVal sValue As String) As Boolean
        Dim col1 As Collection '= Dim asKeys As ICollection
        Dim sKey1 As String
        Dim sPath As String
        Dim sNewFileData As String
        Dim ix, lResult As Integer

        SaveSetting = False
        '--if key exists..  remove it..--
        sNewFileData = ""
        sPath = msFullLocalPath  '= gsLocalJobsSettingsPath()  '=msAppPath & K_SAVESETTINGSPATH
        Call mbRefresh()  '==3301.429 POS=
        '== If mSdSettings.Exists(UCase(sKey)) Then mSdSettings.Remove((UCase(sKey)))
        If mColSettings.Contains(sKey) Then
            mColSettings.Remove(sKey)  '--removes sub-collection.-
        End If
        '-- add key and new value..--
        '== mSdSettings.Add(UCase(sKey), sValue)
        col1 = New Collection
        col1.Add(sKey, "key")
        col1.Add(sValue, "value")
        mColSettings.Add(col1, sKey)  '-load collection also-
        '== mColSettings.Add(sValue, sKey)
        '--make string of key=value cr/lf key=value crlf etc --
        '--- over write file with new string of all settings..--
        If (mColSettings.Count > 0) Then  '= mSdSettings.Count > 0 Then
            '==asKeys = mSdSettings.Keys
            Try
                For ix = 1 To mColSettings.Count '== Each col1 In mColSettings '= sKey1 In asKeys
                    '== sNewFileData = sNewFileData & sKey1 & "=" + mSdSettings.Item(sKey1).ToString + vbCrLf
                    col1 = mColSettings(ix)
                    sNewFileData &= col1.Item("key") & "=" & col1.Item("value") & vbCrLf
                Next ix '=col1
                sNewFileData &= ";;-- the end --"
            Catch ex As Exception
                MsgBox("Save-setting-  Error in building new LocalSettings file data.." & _
                                                       vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Exit Function
            End Try
            lResult = mIntSaveTextFile(sPath, sNewFileData) '-- As Long
            If (lResult <> 0) Then
                MsgBox("Failed to save: " & sPath & vbCrLf, MsgBoxStyle.Exclamation)
            Else  '-ok-
                SaveSetting = True
            End If
        End If '--count..--
    End Function '--save setting.--
    '- - - -  - - - -  --
    '-===FF->

    '-- Item- Get setting for a particular Key..
    '== .. eg printer names, sql serrvername..

    Public Function item(ByVal strSettingKey As String) As String
        item = ""
        '-- get settings collection-
        If mbRefresh() Then
            If mColSettings.Contains(strSettingKey) Then
                item = mColSettings.Item(strSettingKey)("value")
            End If  '-contains-
        End If
    End Function '--item..--
    '= = = = = =  =  == = = == ==

    '--QUERY local settings for a particular Key..
    '== .. eg printer names, sql serrvername..

    Public Function queryLocalSetting(ByVal strSettingKey As String, _
                                           ByRef strSettingValue As String) As Boolean
        queryLocalSetting = False
        '-- get settings collection-
        If mbRefresh() Then
            If mColSettings.Contains(strSettingKey) Then
                strSettingValue = mColSettings.Item(strSettingKey)("value")
                queryLocalSetting = True
            End If  '-contains-
        End If
    End Function '--Query settings..--
    '= = = = = =  =  == = = == ==


End Class  '-clsLocalSettings-
'= = = = = = = = = = = = === 
