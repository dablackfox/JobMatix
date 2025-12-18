Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Diagnostics
'--

Public Class clsAttachments

    '==
    '==   grh 3203.1227-  27-Dec-2015=
    '==     >> Started-  New CLASS to manage all Attachments for Jobs/RA's .- 
    '==                (Pics, Docs, PDF's .)
    '==
    '==   grh 3203.1230-  30-Dec-2015=
    '==     >>  New Sub can be called wthout user controls.CLASS
    '==            (to manage INSERT for New Job or RA..).- 
    '==
    '==   grh 3203.102-  02-Jan-2016=
    '==      >> New method- UpdateAttachment--
    '==     >> 09Jan2016-  Separate Attachments Tables for JOBS ans RAs..
    '==
    '==  =NEW VERSION=
    '==   grh 3311.227-  27-Feb-2016=
    '==      >> Fixes, and ADD MS-WORD and Excel Document types.--
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== Public Const K_SAVESETTINGSPATH As String = "localPOSSettings.txt"
    '-- columns widths..
    Const K_width_party_info As Integer = 500
    Const K_width_staff_name As Integer = 50
    Const K_width_comments As Integer = 1000


    Private mFrmParent As Form
    Private msJobMatixVersion As String = ""

    Private mbIsInitialising As Boolean = True
    '== Private mbActivated As Boolean = False
    Private mbStartingUp As Boolean

    '== Private msServer As String = ""
    '== '-- now split server/instance..--
    '== Private msSqlServerComputer As String = ""
    '= Private msSqlServerInstance As String = ""
    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private msComputerName As String '--local machine--

    '--- Actual connections ---
    Private mCnnSql As OleDbConnection '= ADODB.Connection '-- 
    '= Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  jobs DB info--

    Private msOwnerApp As String = ""
    Private msTableName As String = ""   '= Job_Attachments or RA_Attachments.. 
    Private msEntity_id_column_name As String = ""

    Private mIntEntityId As Integer = -1  '-Job_id or RA_id..

    Private msPartyInfo As String = ""
    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private msAppPath As String
    Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '= Private msListSql As String

    '= = = = CONTROLS on Parent Form.. = = = 
    '= = = = CONTROLS on Parent Form.. = = = 

    Private mOpenDlg1 As OpenFileDialog
    Private mGrpBoxAddNew As GroupBox
    Private mBtnBrowse As Button
    Private mTxtNewFileName As TextBox
    Private mTxtNewComment As TextBox
    Private mBtnSaveAttachment As Button
    Private mLabHelp As Label
    Private mLvwDocs As ListView
    Private mGrpBoxItem As GroupBox
    Private mPicProduct As PictureBox
    Private mPicPDF As PictureBox
    '==3311- add .docs and .xl. 
    Private mPicMsWord As PictureBox
    Private mPicMsExcel As PictureBox
    Private mBtnViewDoc As Button
    Private mBtnDelete As Button
    Private mTxtComments As TextBox

    '-- done controls..--


    '-- NEW File to be Attached..

    Private mByteNewFile As Byte()
    Private msNewFileFullPath As String = ""
    Private msNewFileFileTitle As String = ""
    Private msNewFileFormat As String = ""
    '== Private mIntNewFileSize As Integer = 0

    '- current selection-
    Private mIntDoc_id As Integer = -1
    Private mByteRetrievedFile As Byte()
    Private msCurrentFileTitle As String = ""
    Private mCurrentBinaryData As Byte()
    Private msCurrentFileSuffix As String = ""

    '= = = = = = = = = = = = = = = = = = = = = = = = =  = = = =

    Private mbcancelled As Boolean = False
    Private msLastSqlErrorMessage As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==

    '--=3119.1222=
    '=  Context menu for Pasting- attachment file name-
    '--  Popup menu for Right click on txt File name..-
    Private mContextMenuPasteFileName As ContextMenu
    Private WithEvents mnuPasteFileName As New MenuItem("Paste File")
    Private WithEvents mnuPasteFileSep1 As New MenuItem("-")
    Private WithEvents mnuPasteFileSep2 As New MenuItem("-")

    '-- Dummy to disable default menu-
    '-- LEAVE empty.-
    Private mContextMenuDummy As New ContextMenu

    '= = = = = = =

    '== Last Insert..
    Private mIntRecordsAffected As Integer = -1

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbcancelled
        End Get
    End Property
    '= = = = = = = = =  == == 
    '-===FF->

    ''' <summary>
    ''' Converts the DATA File to array of Bytes
    ''' Thanks to:
    '''     http://www.codeproject.com/Articles/31921/Convert-Image-File-to-Bytes-and-Back  
    ''' </summary>
    ''' <param name="ImageFilePath">The path of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mabConvertDataFiletoBytes(ByVal DataFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing

        If String.IsNullOrEmpty(DataFilePath) = True Then
            Throw New ArgumentNullException("Data File Name Cannot be Null or Empty", "DataFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(DataFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(DataFilePath, IO.FileMode.Open, IO.FileAccess.Read)
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

    '-- NOT NEEDED ???  ==

    ''' <summary>
    ''' Converts the Image File to array of Bytes
    ''' Thanks to:
    '''     http://www.codeproject.com/Articles/31921/Convert-Image-File-to-Bytes-and-Back  
    ''' </summary>
    ''' <param name="ImageFilePath">The path of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function mabConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
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

    '= 3311.227==
    '- g b I s I m a g e F i l e -

    '- NOW is HERE ..(ex modFileSupport),

    '- Can supply full title or just the Extension..

    Public Function mbIsImageFile(ByVal sFileTitle As String) As Boolean
        Dim listTypes As List(Of String) = New List(Of String) From {"BMP", "JPG", "GIF", "PNG", "ICO"}
        Dim sSuffix As String
        Dim intPos1 As Integer

        mbIsImageFile = False
        '- check format..-
        intPos1 = InStrRev(sFileTitle, ".")
        If (intPos1 > 0) Then '--found-
            sSuffix = Mid(sFileTitle, intPos1 + 1)
        Else
            sSuffix = sFileTitle  '--we just got the extension.
        End If
        If listTypes.Contains(UCase(sSuffix)) Then
            mbIsImageFile = True
        End If

    End Function '-is image-
    '= = = = = = = = =  = = == 

    Private Function mbIsOfficeDocumentFile(ByVal sFileTitle As String) As Boolean
        Dim listTypes As List(Of String) = New List(Of String) From {"PDF", "DOC", "DOCX", "XLS", "XLSX", "RTF", "TXT"}
        Dim sSuffix As String
        Dim intPos1 As Integer

        mbIsOfficeDocumentFile = False
        '- check format..-
        intPos1 = InStrRev(sFileTitle, ".")
        If (intPos1 > 0) Then '--found-
            sSuffix = Mid(sFileTitle, intPos1 + 1)
        Else
            sSuffix = sFileTitle  '--we just got the extension.
        End If

        If listTypes.Contains(UCase(sSuffix)) Then
            mbIsOfficeDocumentFile = True
        End If
    End Function '-is image-
    '= = = = = = = = =  = = == 
    '-===FF->

    '-===FF->


    '---subroutine create table --
    '-- =3203.118=  WITH TRANSACTION=

    Private Function mbDb_createTableEx(ByRef cnnSQL As OleDbConnection, _
                                      ByVal sTableName As String, _
                                       ByVal sCreateSql As String, _
                                        ByVal bIsTransaction As Boolean, _
                                         ByRef sqlTran1 As OleDbTransaction, _
                                           ByVal sCreateLogPath As String, _
                                            ByRef intSqlErrors As Integer) As Boolean
        Dim sErrorMsg As String
        Dim sRollback As String = ""
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer

        mbDb_createTableEx = False
        Call gbLogMsg(sCreateLogPath, "Creating SQL Table:  '" & sTableName & "'.." & vbCrLf & _
                                                                         "SQL is:  " & sCreateSql)
        Try
            sqlCmd1 = New OleDbCommand(sCreateSql, cnnSQL)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intAffected = sqlCmd1.ExecuteNonQuery()
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  created ok..")
            mbDb_createTableEx = True
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sErrorMsg = "ERROR in CreateTable: " & ex.Message & vbCrLf & "==" & vbCrLf & _
                                 "Sql was: " & vbCrLf & sCreateSql & vbCrLf & vbCrLf & sRollback & _
                                  "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            intSqlErrors += 1
        End Try
    End Function '--create table..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '---subroutine to create INDEX --
    '---subroutine to create INDEX --

    Private Function mbDb_createIndexEx(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sTableName As String, _
                                        ByVal bIsTransaction As Boolean, _
                                         ByRef sqlTran1 As OleDbTransaction, _
                                         ByVal fx As Integer, _
                                           ByVal sFldList As String, _
                                           ByVal sCreateLogPath As String, _
                                                ByRef intSqlErrors As Integer) As Boolean
        Dim s1 As String
        Dim sSql, sErrorMsg As String
        Dim sRollback As String = ""
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer

        mbDb_createIndexEx = False
        s1 = sTableName & "_IDX" & Trim(CStr(fx))
        sSql = " CREATE INDEX " & s1 & " ON " & sTableName & " (" & sFldList & ")"
        sSql = sSql & "  WITH FILLFACTOR=80 "
        Call gbLogMsg(sCreateLogPath, " -- Creating SQL Index:  " & vbCrLf & sSql)

        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSQL)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intAffected = sqlCmd1.ExecuteNonQuery()
            Call gbLogMsg(sCreateLogPath, "  ==  Table: " & sTableName & " -- INDEX: " & s1 & " created ok..")
            mbDb_createIndexEx = True
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sErrorMsg = "ERROR in CreateTable: " & ex.Message & vbCrLf & "==" & vbCrLf & _
                                  "Sql was: " & vbCrLf & sSql & vbCrLf & vbCrLf & sRollback & _
                                   "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
            intSqlErrors += 1
        End Try
    End Function '--create index-
    '= = = = = = = = = = = =
    '-===FF->

    '---subroutine create table --
    '---subroutine create table --
    Private Function mbDb_createTable(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sTableName As String, _
                                       ByVal sCreate As String, _
                                       ByVal sCreateLogPath As String, _
                                       ByRef iSqlErrors As Integer) As Boolean
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim sErrorMsg As String

        Call gbLogMsg(sCreateLogPath, "Creating SQL Table:  '" & sTableName & "'..")
        Call gbLogMsg(sCreateLogPath, "SQL is:  " & sCreate)
        '==gAdvise "Creating SQL table '" + sTableName + "'.."
        bOk = gbExecuteCmd(cnnSQL, sCreate, L1, sErrorMsg)
        If Not bOk Then
            Call gbLogMsg(sCreateLogPath, "  Failed." & vbCrLf)
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            iSqlErrors = iSqlErrors + 1
            '== gbCreateJobsDB = False
        Else '--ok--  add privileges--
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  created ok..")
        End If '--create ok-
        '==Return
        mbDb_createTable = bOk
    End Function '--create table..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '---subroutine to create INDEX --
    '---subroutine to create INDEX --

    Private Function mbDb_createIndex(ByRef cnnSQL As OleDbConnection, _
                                      ByVal sTableName As String, _
                                       ByVal fx As Integer, _
                                        ByVal sFldList As String, _
                                         ByVal sCreateLogPath As String, _
                                         ByRef iSqlErrors As Integer) As Boolean
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim s1 As String
        Dim sErrorMsg As String
        Dim sSql As String
        s1 = sTableName & "_IDX" & Trim(CStr(fx))
        sSql = " CREATE INDEX " & s1 & " ON " & sTableName & " (" & sFldList & ")"
        sSql = sSql & "  WITH FILLFACTOR=80 "
        Call gbLogMsg(sCreateLogPath, " -- Creating SQL Index:  " & vbCrLf & sSql)
        bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        If Not bOk Then
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
            MsgBox("Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
            iSqlErrors = iSqlErrors + 1
        Else
            Call gbLogMsg(sCreateLogPath, "  ==  Table: " & sTableName & " -- INDEX: " & s1 & " created ok..")
        End If '--ok-
        '==Return
        mbDb_createIndex = bOk
    End Function '--create index-
    '= = = = = = = = = = = =
    '-===FF->

    '- Check if TableExists..

    Private Function mbDoesAttachmentTableExist(ByVal sOwnerApp As String) As Boolean
        Dim rdr1 As OleDbDataReader
        Dim sTableName As String

        mbDoesAttachmentTableExist = False
        Select Case UCase(sOwnerApp)
            Case "JOB" : sTableName = "Job_Attachments"
            Case "RA" : sTableName = "RA_Attachments"
            Case Else
                MsgBox("Error: Invalid owner App in in 'mbDoesAttachmentTableExist'..", MsgBoxStyle.Exclamation)
                Exit Function
        End Select
        '--  IF table does not exist !!--
        '-- The following example checks for the existence of a specified table
        '--     by verifying that the table has an object ID. 
        Dim sSql As String = "SELECT * FROM sys.objects " & _
                               "WHERE object_id = OBJECT_ID(N'[dbo].[" & sTableName & "]') AND type in (N'U')"
        If gbGetReader(mCnnSql, rdr1, sSql) Then  '--check if row exists..-
            If rdr1.HasRows Then '-table exists..-
                mbDoesAttachmentTableExist = True
            Else  '--doesn't exist.. must create.
                mbDoesAttachmentTableExist = False
            End If
            rdr1.Close()
        Else  '-get rdr error
            '--  GET error text !!--
            MsgBox("Error in reading sys.objects table.." & vbCrLf & _
                                  gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
        End If  '--get--

    End Function '-- exists table.-
    '= = = = = = = = = = = == = = =  =
    '-===FF->

    '-- Save PDF/doc in docs table-

    Private Function mbSaveDocumentToDB(ByVal strFileFullPath As String, _
                                         ByVal strFileTitle As String, _
                                         ByVal strFileFormat As String, _
                                          ByVal bIsImage As Boolean, _
                                            ByVal intEntity_id As Integer, _
                                             ByVal strPartyInfo As String, _
                                              ByVal strComments As String, _
                                               Optional ByVal bIsTransaction As Boolean = False, _
                                                Optional ByRef sqlTran1 As OleDbTransaction = Nothing) As Boolean

        Dim byteFileData As Byte()
        Dim intFileSize, intIsImage As Integer
        Dim sFldList As String = ""
        Dim sValueList As String = ""
        Dim sUpdate As String = ""
        Dim sSqlDataType, sFldData, sSql As String
        '== Dim ix, intAffected, intID, intStock_id As Integer
        Dim imageParameters1() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
        Dim parameter1 As OleDbParameter
        Dim sqlCmd1 As OleDbCommand

        mbSaveDocumentToDB = False
        intIsImage = IIf(bIsImage, 1, 0)
        Try
            '--load File bytes..--
            byteFileData = mabConvertDataFiletoBytes(strFileFullPath)
        Catch ex As Exception
            MsgBox("Failed to load Doc File data from File: " & strFileFullPath & vbCrLf & _
                               "Error: " & ex.Message)
            Exit Function
        End Try
        intFileSize = byteFileData.Length

        '-- INSERT doc row into doc archive Table..
 
        sFldList &= msEntity_id_column_name & ", doc_party_info, doc_staff_id, doc_staff_name, "
        sFldList &= " doc_file_format, doc_file_title, doc_file_is_image, doc_file_size, doc_file_comments "

        sValueList = CStr(intEntity_id) & ", "
        sValueList &= "'" & gsFixSqlStr(strPartyInfo) & "', "
        sValueList &= CStr(mIntStaff_id) & ", "
        sValueList &= "'" & gsFixSqlStr(msStaffName) & "', "
        sValueList &= "'" & gsFixSqlStr(strFileFormat) & "', "
        sValueList &= "'" & gsFixSqlStr(strFileTitle) & "', "
        sValueList &= CStr(intIsImage) & ", "
        sValueList &= CStr(intFileSize) & ", "
        sValueList &= "'" & gsFixSqlStr(strComments) & "' "

        '--VARBINARY column- can't use strings.--
        '--  make SQL cmd parameter..-
        '-- BUILD SQL cmd parameter for image byte[]...
        If Not byteFileData Is Nothing Then
            If (sFldList <> "") Then
                sFldList = sFldList & ", "
                sValueList = sValueList & ", "
            End If
            sFldList = sFldList & "doc_file_content"
            sValueList = sValueList & " ? "
            parameter1 = New OleDbParameter("@" & "doc_file_content", SqlDbType.VarBinary)
            parameter1.Value = byteFileData '= mColRowImages(sFldName)
            Dim k As Integer = imageParameters1.Length + 1
            ReDim Preserve imageParameters1(k - 1)
            imageParameters1(k - 1) = parameter1
        End If  '--nothing=                
        sSql = "INSERT INTO dbo." & msTableName & " (" + sFldList + ")  VALUES (" + sValueList + ");"
        '== MsgBox("SQL Insert cmd is : " & vbCrLf & sSql, MsgBoxStyle.Information)
        Try
            sqlCmd1 = New OleDbCommand(sSql, mCnnSql)
            If (imageParameters1.Length > 0) Then
                For ix As Integer = 0 To (imageParameters1.Length - 1)
                    sqlCmd1.Parameters.Add(imageParameters1(ix))
                Next
            End If
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            mIntRecordsAffected = sqlCmd1.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Sql Error in INSERT document record: " & vbCrLf & "SQL Command was: " & _
                          sSql & vbCrLf & ex.Message & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        mbSaveDocumentToDB = True

    End Function  '-save doc-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->


    '-- Browse for New pdf/picture to Attach--

    Private Function mbOpenFileBrowse(ByRef sFullPath As String, _
                                       ByRef sFileTitle As String, _
                                       ByRef byteFile1 As Byte()) As Boolean

        Dim sTitle, sStartPath As String
        Dim MyResult As System.Windows.Forms.DialogResult

        mbOpenFileBrowse = False
        '--  image column..--
        sTitle = ""
        sStartPath = ""
        '--  get actual (image File) location from operator..--
        mOpenDlg1.Title = "Select Image or PDF/Doc/Xls file.."
        mOpenDlg1.Filter = _
               "Image/PDF-Doc Files(*.BMP;*.JPG;*.GIF;*.PNG;*.ICO;*.PDF;*.doc;*.docx;*.xls;*xlsx;*.rtf;*.txt)|" & _
                             "*.BMP;*.JPG;*.GIF;*.PNG;*.ICO;*.PDF;*.doc;*.docx;*.xls;*xlsx;*.rtf;*.txt|All files (*.*)|*.* "
        '= "SQL DB Backup Files (*.png)|*.jpg|All Files (*.*)|*.*"
        mOpenDlg1.InitialDirectory = sStartPath '--msAppPath
        mOpenDlg1.FileName = sStartPath & sTitle
        MyResult = mOpenDlg1.ShowDialog
        '--check for cancel--
        If (MyResult <> System.Windows.Forms.DialogResult.OK) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '== On Error GoTo 0
        sFullPath = mOpenDlg1.FileName   '--has path-
        sFileTitle = mOpenDlg1.SafeFileName  '--No path-
        Try
            '--load image bytes..--
            byteFile1 = mabConvertDataFiletoBytes(sFullPath)
        Catch ex As Exception
            MsgBox("Failed to load byte data from File: " & sFullPath & vbCrLf & _
                               "Error: " & ex.Message)
            Exit Function
        End Try
        '-- save image data for this columns-
        '==    mByteImage1 = byteImage1
        mbOpenFileBrowse = True

    End Function  '--pic--
    '= = = = = = = = = = = = 
    '-===FF->

    '-- get filename from clipboard..--

    Private Function mbGetFileFromClipboard(ByRef strFileFullPath As String, _
                                             ByRef strFileTitle As String, _
                                             ByRef strFormat As String) As Boolean
        Dim data_object As Object = Clipboard.GetDataObject()
        mbGetFileFromClipboard = False

        '-- check if files are there..
        If (data_object.GetDataPresent(DataFormats.FileDrop)) Then
            Dim files() As String = data_object.GetData(DataFormats.FileDrop)
            If (files.Length > 0) Then
                '-- just check the first..
                Dim sName As String = files(0)
                If (System.IO.Directory.Exists(sName)) Then
                    MsgBox("Can't attach a directory !! ", MsgBoxStyle.Exclamation)
                    Exit Function
                Else '-ok-
                    '== MsgBox("File Name is:" & vbCrLf & sName, MsgBoxStyle.Information)
                    '-- save the File name..
                    strFileFullPath = sName
                    Dim intPos As Integer = InStrRev(sName, "\")
                    If intPos > 0 Then
                        strFileTitle = Mid(sName, intPos + 1)
                    Else  '--no slash-
                        strFileTitle = sName
                    End If '-pos-
                    '-- get suffix--
                    '- check format..-
                    Dim s1 As String
                    intPos = InStrRev(strFileTitle, ".")
                    If (intPos > 0) Then '--found-
                        s1 = Mid(strFileTitle, intPos + 1)
                        If (Not mbIsImageFile(s1)) And (Not mbIsOfficeDocumentFile(s1)) Then '= (UCase(s1) <> "PDF") Then
                            MsgBox("Invalid File Type (not Image or PDF/doc/xls).." & _
                                         vbCrLf & vbCrLf & strFileTitle, MsgBoxStyle.Exclamation)
                            Exit Function
                        End If
                        strFormat = s1
                        mbGetFileFromClipboard = True
                    Else '--invalid -
                        MsgBox("Invalid File Type (no suffix).." & _
                                         vbCrLf & vbCrLf & strFileTitle, MsgBoxStyle.Exclamation)
                        Exit Function
                    End If
                End If  '--exists-
            End If  '-length-
        End If  '--file drop-
    End Function  '-get file-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- refresh attachments listView..

    Private Function mbGetAttachments(ByVal intEntityId As Integer, _
                                         ByRef datatable1 As DataTable) As Boolean
        Dim s1, sListSql As String

        mbGetAttachments = False
        '= mLvwDocs.Enabled = False

        sListSql = "SELECT * FROM dbo." & msTableName & "  "
        sListSql &= "WHERE (" & msEntity_id_column_name & " =" & CStr(intEntityId) & ")"
        sListSql &= " ORDER BY doc_id; "

        If Not gbGetDataTable(mCnnSql, datatable1, sListSql) Then
            MsgBox("Failed to get Attachments recordset.." & vbCrLf & _
                    "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else  '-ok-
        End If  '--get-
        '== mLvwDocs.Enabled = True
        mbGetAttachments = True

    End Function  '-refresh-
    '= = = = = = = = = = = = 
    '-===FF->

    '-- refresh attachments listView..

    Private Function mbRefreshAttachments(ByVal intEntityId As Integer) As Boolean
        Dim datatable1 As DataTable
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim s1 As String

        mbRefreshAttachments = False
        mLvwDocs.Enabled = False

        If Not mbGetAttachments(intEntityId, datatable1) Then '= gbGetDataTable(mCnnSql, datatable1, msListSql) Then
            MsgBox("Failed to get Attachments for Entity: " & intEntityId, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else  '-ok-
            mLvwDocs.Items.Clear()
            If datatable1 Is Nothing Then
                MsgBox("ERROR- No Attachments Table returned..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            If (datatable1.Rows.Count <= 0) Then
                '== MsgBox("No Attachments on file yet..", MsgBoxStyle.Information)
                '== Exit Function
            Else  '--have some-
                Dim dblSize As Double
                For Each datarow1 As DataRow In datatable1.Rows
                    s1 = Trim(datarow1.Item("doc_id")) '--1st column.-
                    item1 = mLvwDocs.Items.Add(s1)   '--1st column.-
                    item1.SubItems.Add(Format(datarow1.Item("doc_date_inserted"), "dd-MMM-yyyy")) '--2nd column.-
                    item1.SubItems.Add(Trim(datarow1.Item("doc_file_title")))
                    dblSize = datarow1.Item("doc_file_size") / 1024
                    item1.SubItems.Add(Format(dblSize, "  ,   , 00.00") & " KB")
                    item1.SubItems.Add(Trim(datarow1.Item("doc_staff_name")))
                Next  '-datarow-
            End If  '-count-
        End If  '--get-
        mLvwDocs.Enabled = True
        mbRefreshAttachments = True

    End Function  '-refresh-
    '= = = = = = = = = = = = 
    '-===FF->

    '--sub new-
    '--sub new-

    '- This overload used for callers with no form controls..
    '-- Class Instance must be either JOB or RA (sOwnerApp)--

    '-- Used also by Jobmatix Main startup to test if tables exist..

    Public Sub New(ByRef FrmParent As Form, _
                     ByRef cnnSql As OleDbConnection, _
                      ByVal sOwnerApp As String, _
                       ByRef openDlg1 As OpenFileDialog)
        '--init-  save -
        mFrmParent = FrmParent
        mCnnSql = cnnSql
        msOwnerApp = sOwnerApp   '-  "JOB" or "RA" ----
        mOpenDlg1 = openDlg1
        If (UCase(msOwnerApp) = "JOB") Then
            msTableName = "Job_Attachments"
            msEntity_id_column_name = "doc_job_id"
        ElseIf (UCase(msOwnerApp) = "RA") Then
            msTableName = "RA_Attachments"
            msEntity_id_column_name = "doc_ra_id"
        Else
            msTableName = "-ERROR-"
            Throw New ArgumentNullException("ERROR- Invalid App-type", "New")
            Exit Sub
        End If

    End Sub  '--New naked-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '- This overload used for callers WITH form controls..
    '-- Class Instance must be either JOB or RA (sOwnerApp)--

    '-- NOT bHasUserControls then USER can INSERT only..
    '-- Table must exist already..

    Public Sub New(ByRef FrmParent As Form, _
                     ByRef cnnSql As OleDbConnection, _
                        ByRef colSqlDBInfo As Collection, _
                          ByVal sOwnerApp As String, _
                           ByVal intEntityId As Integer, _
                            ByVal strPartyInfo As String, _
                             ByVal intStaff_id As Integer, _
                              ByVal strStaffName As String, _
                              ByRef openDlg1 As OpenFileDialog)

        Dim bHasUserControls As Boolean = True

        '--init-  save -
        mFrmParent = FrmParent
        msOwnerApp = sOwnerApp   '-  "JOB" or "RA" ----

        mCnnSql = cnnSql
        '== msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        '= msJobMatixVersion = sVersion

        mIntEntityId = intEntityId  '- Job_id or RA_id
        msPartyInfo = VB.Left(strPartyInfo, K_width_party_info)
        msStaffName = VB.Left(strStaffName, K_width_staff_name)
        mIntStaff_id = intStaff_id
        mOpenDlg1 = openDlg1

        If (UCase(msOwnerApp) = "JOB") Then
            msTableName = "Job_Attachments"
            msEntity_id_column_name = "doc_job_id"
        ElseIf (UCase(msOwnerApp) = "RA") Then
            msTableName = "RA_Attachments"
            msEntity_id_column_name = "doc_ra_id"
        Else
            msTableName = "-ERROR-"
        End If

        If bHasUserControls Then
            '-- Find all needed Controls on  Attachments Form..
            '--   and save as Module vars..
            Dim controlsArray() As Control

            '-  ERROR if table doesn't exist..
            If Not mbDoesAttachmentTableExist(sOwnerApp) Then
                Throw New ArgumentNullException("ERROR- No Attachments Table", "New")
                Exit Sub
            End If

            '- get group Boxes first.
            controlsArray = mFrmParent.Controls.Find("grpBoxAddNew", True)  '--searches all children.. 
            If (controlsArray.Length > 0) Then mGrpBoxAddNew = controlsArray(0)

            For Each Control1 As Control In mGrpBoxAddNew.Controls
                Select Case LCase(Control1.Name)
                    '= Case "grpboxaddnew" : mGrpBoxAddNew = Control1
                    Case "btnbrowse" : mBtnBrowse = Control1
                    Case "txtnewfilename" : mTxtNewFileName = Control1
                    Case "txtnewcomment" : mTxtNewComment = Control1
                    Case "btnsaveattachment" : mBtnSaveAttachment = Control1
                    Case "labhelp" : mLabHelp = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next  '--control1-

            '- get group Boxes first.
            controlsArray = mFrmParent.Controls.Find("grpboxitem", True)  '--searches all children.. 
            If (controlsArray.Length > 0) Then mGrpBoxItem = controlsArray(0)

            For Each Control1 As Control In mGrpBoxItem.Controls
                Select Case LCase(Control1.Name)
                    Case "lvwdocs"
                        mLvwDocs = Control1
                        '== mLvwDocs.Columns["doc_id"].TextAlign = HorizontalAlignment.Right;
                        '= Case "grpboxitem" : mGrpBoxItem = Control1
                    Case "picproduct" : mPicProduct = Control1
                    Case "picpdf" : mPicPDF = Control1
                        '=3311.227= add Office docs. 
                    Case "picmsword" : mPicMsWord = Control1
                    Case "picmsexcel" : mPicMsExcel = Control1
                    Case "btnviewdoc" : mBtnViewDoc = Control1
                    Case "btndelete" : mBtnDelete = Control1
                    Case "txtcomments" : mTxtComments = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next  '--control1-

            '-- Initialising User Controls Stuff..=
            Try
                mTxtNewComment.MaxLength = K_width_comments
                mTxtNewComment.Text = ""
                '==labNewFileTitle.Text = ""
                mTxtNewFileName.Text = ""
                mGrpBoxItem.Text = ""

                mLvwDocs.Enabled = False
                mGrpBoxAddNew.Enabled = False

                mPicPDF.Top = mPicProduct.Top
                mPicPDF.Left = mPicProduct.Left + 16
                mPicPDF.Visible = False
                '=3311.227=
                mPicMsWord.Top = mPicPDF.Top
                mPicMsWord.Left = mPicPDF.Left
                mPicMsWord.Visible = False
                mPicMsExcel.Top = mPicPDF.Top
                mPicMsExcel.Left = mPicPDF.Left
                mPicMsExcel.Visible = False

                mLabHelp.Text = "To add an Attachment, browse to the PDF/Doc/Xls or Image file to be attached, and Open it. " & vbCrLf & _
                                 " (Or Copy and paste onto this Form using Ctl-V..) " & vbCrLf & _
                                  "Then Enter some comment, and Press Save.." & vbCrLf
            Catch ex As Exception
                '= MsgBox("ERROR initialising controls.." & vbCrLf & ex.Message)
                Throw New ArgumentNullException("NEW ERROR- initialising controls.", "New clsAttachments")
                Exit Sub
            End Try

            Select Case UCase(msOwnerApp)
                Case "JOB"
                    mGrpBoxItem.Text = "Attachments on File for Job: " & mIntEntityId
                Case "RA"
                    mGrpBoxItem.Text = "Attachments on File for RA: " & mIntEntityId
                Case Else
                    MsgBox("ERROR- Invalid Application Type..", MsgBoxStyle.Exclamation)
                    Throw New ArgumentNullException("ERROR- Invalid Application Type: " & msOwnerApp, "clsAttachments")
                    '= Exit Sub
            End Select  '-owner-

            '- load listView with docs for this Job or RA if any..
            If Not mbRefreshAttachments(mIntEntityId) Then
                Throw New ArgumentNullException("Failed to refresh Attachments", "New")
            End If
            mGrpBoxAddNew.Enabled = True

            mTxtNewFileName.Text = "Paste New Attachment " & vbCrLf & "    Here.."
        End If  '-has controls--

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '= P u b l i c  M e t h o d s --
    '= P u b l i c  M e t h o d s --

    '-- NO USER CONTROLS needed for these..
    '-- NO USER CONTROLS needed for these..

    '= 3311.227==
    '- g b I s I m a g e F i l e -

    '- Can supply full title or just the Extension..

    Public Function IsImageFile(ByVal sFileTitle As String) As Boolean
        '-  Is now here now in this class.-
        IsImageFile = mbIsImageFile(sFileTitle)
    End Function  '- gbIsImageFile-
    '= = = = = = = = = = = = = = = = =

    Public Function IsOfficeDocumentFile(ByVal sFileTitle As String) As Boolean
        IsOfficeDocumentFile = mbIsOfficeDocumentFile(sFileTitle)
    End Function
    '= = = = = = = = = = == = =
    '-===FF->

    '- Check if table exists yet..--

    Public Function DoesAttachmentTableExist(sOwnerApp) As Boolean


        DoesAttachmentTableExist = mbDoesAttachmentTableExist(sOwnerApp)

    End Function '-exists ?-
    '= = = = = = = = = = = = = =

    '- CREATE table now..--

    '-- MUST ALSO BE in CreateJobs  Module.    !!!!!!

    Public Function CreateAttachmentTable(ByVal strAppType As String, _
                                          ByVal bIsTransaction As Boolean, _
                                            ByRef sqlTran1 As OleDbTransaction) As Boolean

        '-- FROM Attachments Form Activated-
        Dim sTableName, sSql, sCreateSql, sFldList, sErrorMsg As String
        Dim iSqlErrors, intAffected As Integer
        Dim bDoCreate As Boolean = True
        Dim sCreateLogPath As String = gsErrorLogPath
        Dim bOk As Boolean = True
        Dim rdr1 As OleDbDataReader
        Dim datatable1 As DataTable

        CreateAttachmentTable = False
        If (UCase(strAppType) <> "JOB") And (UCase(strAppType) <> "RA") Then
            Exit Function
        End If

        If (UCase(strAppType) = "JOB") Then
            sTableName = "Job_Attachments"
        Else
            sTableName = "RA_Attachments"
        End If
        '-- IF table exists.DROP it..-
        '-- The following example checks for the existence of a specified table
        '--     by verifying that the table has an object ID. 
        Dim bTableExists As Boolean = False
        sSql = "SELECT * FROM sys.objects " & _
                   "WHERE object_id = OBJECT_ID(N'[dbo].[" & sTableName & "]') AND type in (N'U')"
        sErrorMsg = ""
        If bIsTransaction Then
            If gbGetDataTableEx(mCnnSql, datatable1, sSql, sqlTran1) Then
                If ((Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0))) Then
                    bTableExists = True
                End If
            Else  '--error-
                sErrorMsg = gsGetLastSqlErrorMessage()
            End If
        Else '-not in trans.-
            If gbGetReader(mCnnSql, rdr1, sSql) Then  '--check if row exists..-
                If rdr1.HasRows Then '-table exists..-
                    bTableExists = True
                End If
                rdr1.Close()
            Else  '-get rdr error
                '--  GET error text !!--
                sErrorMsg = gsGetLastSqlErrorMessage()
            End If  '--get rdr--
        End If
        If sErrorMsg <> "" Then
            MsgBox("Error in reading sys.objects table.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If
        '-- DROP if exists..-
        If bTableExists Then
            sSql = "DROP TABLE dbo." & sTableName & ";"
            If Not gbExecuteSql(mCnnSql, sSql, True, sqlTran1, intAffected, sErrorMsg) Then
                MsgBox("Failed to DROP Attachment Table." & vbCrLf & vbCrLf & _
                                             sErrorMsg, MsgBoxStyle.Exclamation)
                Exit Function
            End If
        End If '-existed.

        '-- OK.  Create the Attachment Table.
        sCreateSql = gsMakeAttachmentsScript(strAppType)
        '-- Now to create ----
        bOk = mbDb_createTableEx(mCnnSql, sTableName, sCreateSql, bIsTransaction, sqlTran1, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            '- Roll back done..
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreateAttachmentTables- Table " & sTableName & " was created ok.. ")

        '-- must have indexes on App entity id ..
        '--add other indexes----
        Dim fx As Integer
        fx = 1 '---index no...
        sFldList = " doc_ra_id"
        If (UCase(strAppType) = "JOB") Then
            sFldList = " doc_job_id"
        End If
        bOk = mbDb_createIndexEx(mCnnSql, sTableName, True, sqlTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        CreateAttachmentTable = bOk

    End Function '-create..-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '- This used ONLY for Inserting Doc from NewRA and New Job..
    '--  Can be inside a TRANSACTION..---

    Public Function InsertNewAttachment(ByVal strFileFullPath As String, _
                                        ByVal strFileTitle As String, _
                                         ByVal strFileFormat As String, _
                                           ByVal intEntity_id As Integer, _
                                            ByVal strPartyInfo As String, _
                                             ByVal strComments As String, _
                                              ByVal bIsTransaction As Boolean, _
                                               ByRef sqlTran1 As OleDbTransaction, _
                                                ByRef intAffected As Integer) As Boolean

        '-- save to Database..
        Dim bIsImage As Boolean = mbIsImageFile(strFileFormat)

        InsertNewAttachment = mbSaveDocumentToDB(strFileFullPath, strFileTitle, _
                                   strFileFormat, bIsImage, intEntity_id, strPartyInfo, strComments, _
                                                                                bIsTransaction, sqlTran1)
        intAffected = mIntRecordsAffected
    End Function  '-Insert-
    '= = = = = = = = = = = = = = = = = = = = = = =

    '-- Browse for New pdf/picture to Attach--
    '- This used ONLY for Browsing for new PIC from NewRA and New Job..
    '--  File names and Byte array of file are returned as side effects.

    Public Function OpenFileBrowse(ByRef sFileFullPath As String, _
                                      ByRef sFileTitle As String, _
                                       ByRef byteFile1 As Byte()) As Boolean
        OpenFileBrowse = mbOpenFileBrowse(sFileFullPath, sFileTitle, byteFile1)

    End Function  '-open browse.
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '- Get First image on File this Job or RA..-

    Public Function GetFirstImage(ByVal intEntityId As Integer, _
                                  ByRef intDocId As Integer, _
                                  ByRef sFileTitle As String, _
                                   ByRef byteImageBytes() As Byte) As Boolean
        Dim datatable1 As DataTable
        Dim s1, sTitle, sFormat As String
        '== MsgBox("GetFirstImage- Started.. ")

        GetFirstImage = False

        If Not mbGetAttachments(intEntityId, datatable1) Then
            MsgBox("Failed to get Attachments for Entity: " & intEntityId, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else  '-ok-
            '-- look for first image.-
            '-- datatable is in order of doc_id (ie history)..
            '==MsgBox("GetFirstImage- Got list.. ")
            If (datatable1.Rows.Count > 0) Then
                For ix As Integer = 0 To (datatable1.Rows.Count - 1)
                    sTitle = datatable1.Rows(ix).Item("doc_file_title")
                    sFormat = datatable1.Rows(ix).Item("doc_file_format")
                    If mbIsImageFile(UCase(sFormat)) Then  '-take it-
                        intDocId = datatable1.Rows(ix).Item("doc_id")
                        sFileTitle = sTitle
                        byteImageBytes = datatable1.Rows(ix).Item("doc_file_content")
                        GetFirstImage = True
                        Exit For
                    End If
                Next ix
            End If  '-count-
        End If '-get table-
    End Function '-getFirstImage-
    '= = = = = = = = = = = = =  = =
    '-===FF->

    '- Get ALL images on File this Job or RA..-
    '--  Function returns count of images.
    '--   "intMaxToReturn"  is limit of callers space..
    '--      (also try ByRef aByteImageBytes()() As Byte ) 

    Public Function GetSelectedImages(ByVal intEntityId As Integer, _
                                  ByRef listImageIDs As List(Of Integer), _
                                  ByRef asFileTitles() As String, _
                                  ByRef listImagesUser As List(Of Image)) As Integer
        Dim datatable1 As DataTable
        Dim s1, sTitle, sFormat As String
        Dim intCount As Integer = 0
        Dim image1 As Image
        Dim ms As System.IO.MemoryStream
        Dim bytearray1() As Byte
        Dim asTitles() As String = {}
        Dim listImages1 As New List(Of Image)
        Dim bGetAll As Boolean = (listImageIDs.Count <= 0)
        Dim intDoc_id As Integer

        GetSelectedImages = -1   '-- if failed--
        If Not mbGetAttachments(intEntityId, datatable1) Then
            MsgBox("Failed to get Attachments for Entity: " & intEntityId, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else  '-ok-
            '-- datatable is in order of doc_id (ie history)..
            '==asFileTitle.Initialize()
            listImages1.Clear()
            If (datatable1.Rows.Count > 0) Then
                For ix As Integer = 0 To (datatable1.Rows.Count - 1)
                    intDoc_id = datatable1.Rows(ix).Item("doc_id")
                    sTitle = datatable1.Rows(ix).Item("doc_file_title")
                    sFormat = datatable1.Rows(ix).Item("doc_file_format")
                    If mbIsImageFile(UCase(sFormat)) And _
                           (bGetAll Or listImageIDs.Contains(intDoc_id)) Then  '-take it-  if wanted..
                        ReDim Preserve asTitles(intCount)     '= = sTitle
                        bytearray1 = datatable1.Rows(ix).Item("doc_file_content")
                        ms = New System.IO.MemoryStream(bytearray1)
                        image1 = System.Drawing.Image.FromStream(ms)
                        listImages1.Add(image1)
                        '== mPicProduct.Image = image1
                        ms.Close()
                        intCount += 1
                    End If
                Next ix
            End If  '-count-
            asFileTitles = asTitles
            listImagesUser = listImages1
            GetSelectedImages = intCount
        End If '-get table-
    End Function  '--get all images.-
    '= = = = = = = = = = = = =  = =
    '-===FF->

    '-- Update an attachment..
    '-   Update  FileTitle, FileFormat and byte() CONTENT..-

    '--  Can be inside a TRANSACTION..---

    Public Function UpdateAttachment(ByVal intDoc_id As Integer, _
                                      ByVal strFileFullPath As String, _
                                       ByVal strFileTitle As String, _
                                       ByVal strFileFormat As String, _
                                       ByVal strStaffName As String, _
                                         ByRef byteImageBytes() As Byte, _
                                           ByVal bIsTransaction As Boolean, _
                                            ByRef sqlTran1 As OleDbTransaction, _
                                            ByRef intAffected As Integer) As Boolean

        Dim sUpdate As String
        Dim imageParameters1() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
        Dim parameter1 As OleDbParameter
        Dim sqlCmd1 As OleDbCommand

        UpdateAttachment = False
        '-- Update Database via doc_id...
        If (intDoc_id <= 0) Or (strFileFullPath = "") Or (byteImageBytes Is Nothing) Then
            MsgBox("ERROR- invalid parameters to update Attachment.", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        sUpdate = "doc_file_title='" & gsFixSqlStr(strFileTitle) & "', "
        sUpdate &= "doc_file_format='" & gsFixSqlStr(strFileFormat) & "', "
        sUpdate &= "doc_file_comments= doc_file_comments + CHAR(13) + CHAR(10) + " & _
                            "'Updated by: " & gsFixSqlStr(strStaffName) & " on " & _
                                 VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy HH:mm") & ".', "
        sUpdate &= "doc_file_content= ?"  '=oleDB =parameter ref..-
        '-- BUILD SQL cmd parameter for image byte[]...
        '=If Not mByteImage1 Is Nothing Then
        parameter1 = New OleDbParameter("@" & "doc_file_content", SqlDbType.VarBinary)
        parameter1.Value = byteImageBytes  '= mColRowImages(sFldName)
        Dim k As Integer = imageParameters1.Length + 1
        ReDim Preserve imageParameters1(k - 1)
        imageParameters1(k - 1) = parameter1
        '=End If  '--nothing=
        sUpdate = "UPDATE dbo." & msTableName & " SET " & sUpdate & _
              " WHERE (doc_id=" & CStr(intDoc_id) & ");"
        Try
            sqlCmd1 = New OleDbCommand(sUpdate, mCnnSql)
            If (imageParameters1.Length > 0) Then
                For ix As Integer = 0 To (imageParameters1.Length - 1)
                    sqlCmd1.Parameters.Add(imageParameters1(ix))
                Next
            End If
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intAffected = sqlCmd1.ExecuteNonQuery()
            MsgBox("Done- " & intAffected & " record(s) were updated..", MsgBoxStyle.Information)
            If (intAffected > 0) Then
                UpdateAttachment = True
            End If
        Catch ex As Exception
            MsgBox("Sql Error in UPDATE document record: " & vbCrLf & "SQL Command was: " & _
                          sUpdate & vbCrLf & ex.Message & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
    End Function  '--Update-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- E V E N T S --
    '- These catched events transferred from User Form.--
    '- FOR users with a Form:

    '== ALL ACTUAL CONTROL EVENTS occur on the FORM-- 

    '== ALL ACTUAL CONTROL EVENTS occur on the FORM, 
    '--    and are passed here to the CLASS for processing.-- 

    '--PREVIEW KeyPress..-
    '-- Catch  ctl-V- to Paste.

    Public Sub frmAttachments_KeyDown(sender As Object, _
                                  eventArgs As System.Windows.Forms.KeyEventArgs)  '= Handles MyBase.KeyDown
        Dim sFileFullpath, sFileTitle, sFormat As String

        If (eventArgs.Control And (eventArgs.KeyCode = Keys.V)) Then
            If mbGetFileFromClipboard(sFileFullpath, sFileTitle, sFormat) Then
                '-- Show File name-
                msNewFileFullPath = sFileFullpath
                msNewFileFileTitle = sFileTitle
                msNewFileFormat = sFormat
                '-- set up for comments and save..
                '== labNewFileTitle.Text = msNewFileFileTitle
                mTxtNewFileName.Text = msNewFileFullPath
                mBtnSaveAttachment.Enabled = False
                mTxtNewComment.Enabled = True
            End If  '--get-

            eventArgs.Handled = True
        End If '--ctl-V-

    End Sub  '-- keydown-
    '= = = = = = = = = = = =
    '-===FF->

    '--=3119.1222=  PASTE-FILE Context menu stuff--
    '--=3119.1222=  PASTE-FILE Context menu stuff--

    '-- menu click-

    Public Sub mnuPasteFileName_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) '= Handles mnuPasteFileName.Click
        Dim sFileFullpath, sFileTitle, sFormat As String

        If mbGetFileFromClipboard(sFileFullpath, sFileTitle, sFormat) Then
            '-- Show File name-
            msNewFileFullPath = sFileFullpath
            msNewFileFileTitle = sFileTitle
            msNewFileFormat = sFormat
            '-- set up for comments and save..
            '== labNewFileTitle.Text = msNewFileFileTitle
            mTxtNewFileName.Text = msNewFileFullPath
            mBtnSaveAttachment.Enabled = False
            mTxtNewComment.Enabled = True
            mTxtNewComment.Text = sFileTitle
            mTxtNewComment.SelectionStart = 0
            mTxtNewComment.SelectionLength = mTxtNewComment.Text.Length

            '== mTxtNewComment.Enabled = True
        End If  '--get-
    End Sub  '-mnuPasteFileName- click-
    '= = = = = = = = = = = = = = = =  = = = = = = =
    '-===FF->

    '-  MOUSE ACTION- txtNewFileName-

    '== NOT THIS ONE==
    '--  has to run on the FORM.-

    '== Private Sub txtNewFileName_MouseUp(sender As Object, _
    '==                                      ev As MouseEventArgs) Handles txtNewFileName.MouseUp
    '== Dim sFileFullpath, sFileTitle, sFormat As String

    '== ' If the right mouse button was clicked and released, 
    '== ' display the shortcut menu assigned to the txt.  
    '==     If ev.Button = MouseButtons.Right Then
    '==         If mbGetFileFromClipboard(sFileFullpath, sFileTitle, sFormat) Then
    '==             mnuPasteFileName.Enabled = True
    '==         Else  '-nothing on clipboard-
    '==             mnuPasteFileName.Enabled = False
    '==         End If '-get
    '== '--show menu.. user must ckick.. 
    '==         mContextMenuPasteFileName.Show(txtNewFileName, New Point(ev.X, ev.Y))
    '==     End If
    '== End Sub  '-txtNewFile mouse up-
    '= = = = = = = = = == = = = = = = = = 
    '-===FF->

    '-- A d d Attachment--
    '-- A d d Attachment--

    Public Sub btnBrowse_Click(sender As Object, _
                                ev As EventArgs)   '== Handles btnBrowse.Click
        Dim intPos1 As Integer
        Dim s1 As String

        mBtnBrowse.Enabled = False
        mTxtNewComment.Enabled = False
        mTxtNewComment.Text = ""
        mBtnSaveAttachment.Enabled = False

        If Not mbOpenFileBrowse(msNewFileFullPath, msNewFileFileTitle, mByteNewFile) Then
            mBtnBrowse.Enabled = True   '--can have another go..-
            Exit Sub
        End If  '--open-

        msNewFileFormat = ""
        '- check format..-
        intPos1 = InStrRev(msNewFileFileTitle, ".")

        If (intPos1 > 0) Then '--found-
            s1 = Mid(msNewFileFileTitle, intPos1 + 1)
            If (Not mbIsImageFile(s1)) And (Not mbIsOfficeDocumentFile(s1)) Then  '= (UCase(s1) <> "PDF") Then
                MsgBox("Invalid File Type (not Image or PDF)..", MsgBoxStyle.Exclamation)
                mBtnBrowse.Enabled = True   '--can have another go..-
                Exit Sub
            End If
            msNewFileFormat = s1
        Else '--invalid -
            MsgBox("Invalid File Type (no suffix)..", MsgBoxStyle.Exclamation)
            mBtnBrowse.Enabled = True   '--can have another go..-
            Exit Sub
        End If
        '-- set up for comments and save..
        '== labNewFileTitle.Text = msNewFileFileTitle
        mTxtNewFileName.Text = msNewFileFullPath
        mBtnSaveAttachment.Enabled = False
        mTxtNewComment.Enabled = True

        mBtnBrowse.Enabled = True   '--can have another go..-

    End Sub  '-browse.-
    '= = = = = = = = = = = = 

    Public Sub txtNewComment_TextChanged(sender As Object, _
                                              ev As EventArgs) '== Handles txtNewComment.TextChanged

        If mTxtNewComment.Enabled Then
            mBtnSaveAttachment.Enabled = True
        End If

    End Sub  '--new comment-
    '= = = = = = = = = = == = = 

    '-- save new File to DB..

    Public Sub btnSaveAttachment_Click(sender As Object, ev As EventArgs)   '== Handles btnSave.Click


        If (msNewFileFileTitle = "") Then
            Exit Sub
        End If
        '-- save to Database..
        '-- NO TRANSACTION.--
        Dim bIsImage As Boolean = mbIsImageFile(msNewFileFormat)

        If Not mbSaveDocumentToDB(msNewFileFullPath, msNewFileFileTitle, _
                                   msNewFileFormat, bIsImage, mIntEntityId, msPartyInfo, mTxtNewComment.Text) Then

        End If  '-save-

        msNewFileFileTitle = ""   '- so we don't repeat it..
        mBtnSaveAttachment.Enabled = False
        mTxtNewFileName.Text = "Paste New Attachment " & vbCrLf & "    Here.."

        Call mbRefreshAttachments(mIntEntityId)

    End Sub  'save new-
    '= = = = = = = = = =
    '-===FF->

    '-- view current doc..
    '-- Doc has been selected from listView..

    Public Sub btnViewDoc_Click(sender As Object, e As EventArgs)  '= Handles btnViewDoc.Click

        If (msCurrentFileTitle = "") Or (msCurrentFileSuffix = "") Or _
                                           (mCurrentBinaryData Is Nothing) Then
            MsgBox("Nothing selected..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '-- data dir this user..
        Dim sTempPath As String = gsJobMatixLocalDataDir()
        If (VB.Right(sTempPath, 1) <> "\") Then
            sTempPath &= "\"
        End If
        '-- make user temp directory to dump file to..
        sTempPath &= "Temp"
        '- make Users Temp sub Dir-
        If Not Directory.Exists(sTempPath) Then
            '--make it-
            Try
                Directory.CreateDirectory(sTempPath)
            Catch ex As Exception
                MsgBox("Failed to create User Data directory: " & vbCrLf & _
                              sTempPath & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                Exit Sub
            End Try
        End If  '-exists-
        '-- Now dump file to disk dir.
        sTempPath &= "\" & msCurrentFileTitle

        '- System.IO.File.WriteAllBytes  -
        '-    File.WriteAllBytes(string path, byte[] bytes)
        Try
            File.WriteAllBytes(sTempPath, mCurrentBinaryData)
        Catch ex As Exception
            MsgBox("Failed to write File:" & vbCrLf & _
                sTempPath & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        '-- launch process to view file (eg Adobe Reader)..
        If mbIsImageFile(msCurrentFileSuffix) Then
            Try
                Process.Start("IExplore.exe", sTempPath)
            Catch ex As Exception
                MsgBox("Error starting IE.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        ElseIf mbIsOfficeDocumentFile(UCase(msCurrentFileSuffix)) Then   '-doc file 
            '==If (UCase(msCurrentFileSuffix) = "PDF") Then
            Try
                Process.Start(sTempPath)
            Catch ex As Exception
                MsgBox("Error starting PDF Reader.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        Else  '-nothing-
            MsgBox("Unknown File Type.." & vbCrLf & sTempPath, MsgBoxStyle.Exclamation)
        End If

    End Sub  '--  view -
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Delete -

    Public Sub btnDelete_Click(sender As Object, ev As EventArgs) '== Handles btnDelete.Click

        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intDocId, intAffected As Integer
        Dim sFileName, sSql, sErrorMsg As String
        '--  update quote info display if selection has moved..--
        item1 = mLvwDocs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            MsgBox("Nothing selected..", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            intDocId = CInt(item1.Text) '--1st column has to be job_id..--
            If (intDocId > 0) Then
                sFileName = item1.SubItems(2).Text
                If MsgBox("Sure you want to remove the Attachment file: " & vbCrLf & sFileName & vbCrLf & _
                             "  From this " & msOwnerApp & "  ? ", _
                            MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    '-- delete this row..
                    sSql = "DELETE FROM dbo." & msTableName
                    sSql &= "  WHERE (doc_id=" & CStr(intDocId) & ");"
                    '== sSql &= "  AND (doc_app_entity_id=" & CStr(mIntEntityId) & ");"
                    If Not gbExecuteCmd(mCnnSql, sSql, intAffected, sErrorMsg) Then
                        MsgBox("ERROR deleting attachment row..  Error is:" & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                    Else     '-ok-
                        If (intAffected > 0) Then
                            MsgBox("(" & intAffected & ") Attachment deleted ok.", MsgBoxStyle.Information)
                        Else
                            MsgBox("(" & intAffected & ") Attachments were deleted.", MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
                If Not mbRefreshAttachments(mIntEntityId) Then
                    Throw New ArgumentNullException("Failed to refresh Attachments", "New")
                End If
            Else
                MsgBox("No Document selected..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If '--selected..-
    End Sub  '--delete-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- list vew=  selection changed..--

    Public Sub lvwDocs_SelectedIndexChanged(sender As Object, e As EventArgs) '== Handles lvwDocs.SelectedIndexChanged
        Dim byteFile1 As Byte()
        Dim image1 As Image
        Dim intDoc_id As Integer
        Dim sSql As String
        Dim dataTable1 As DataTable

        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer

        mBtnViewDoc.Enabled = False
        msCurrentFileTitle = ""

        '--  update info display if selection has moved..--
        item1 = mLvwDocs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            intDoc_id = CInt(item1.Text) '--1st column has to be job_id..--
            If (intDoc_id <> mIntDoc_id) Then '-- has changed..-
            End If
        End If '--selected..-

        '--save selection-
        mIntDoc_id = intDoc_id

        '-- Retrieve Attachment Bytestream..
        sSql = "SELECT * FROM dbo." & msTableName & "  WHERE (doc_id=" & CStr(intDoc_id) & ");"
        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Failed to get Attachment record.." & vbCrLf & _
               "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If (dataTable1 Is Nothing) Then
            MsgBox("ERROR- NULL Attachment recordset returned..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If (dataTable1.Rows.Count <= 0) Then
            MsgBox("Attachments NOT FOUND..", MsgBoxStyle.Exclamation)
            Exit Sub
        Else  '--have some-

        End If  '-count-

        Dim datarow1 As DataRow = dataTable1.Rows(0)
        msCurrentFileTitle = datarow1.Item("doc_file_title")
        mTxtComments.Text = msCurrentFileTitle & vbCrLf & datarow1.Item("doc_file_comments")
        mPicProduct.Image = Nothing
        mPicProduct.Visible = False
        mPicPDF.Visible = False
        mPicMsWord.Visible = False
        mPicMsExcel.Visible = False

        If Not IsDBNull(datarow1.Item("doc_file_content")) Then
            mCurrentBinaryData = datarow1.Item("doc_file_content")
            Dim sSuffix As String = datarow1.Item("doc_file_format")
            Dim ms As System.IO.MemoryStream
            Try
                '== IF current selection is Image..
                '--- load picture from byte array..-
                If mbIsImageFile(sSuffix) Then
                    ms = New System.IO.MemoryStream(mCurrentBinaryData)
                    image1 = System.Drawing.Image.FromStream(ms)
                    mPicProduct.Visible = True
                    mPicProduct.Image = image1
                    ms.Close()  '-close stream-
                ElseIf (InStr(UCase(sSuffix), "PDF") > 0) Then
                    '-- get PDF ICON if is PDF..-
                    mPicPDF.Visible = True
                ElseIf (InStr(UCase(sSuffix), "DOC") > 0) Then
                    '-- get doc ICON if is PDF..-
                    mPicMsWord.Visible = True
                ElseIf (InStr(UCase(sSuffix), "XL") > 0) Then
                    '-- get PDF ICON if is PDF..-
                    mPicMsExcel.Visible = True
                End If  '-image-
            Catch ex As Exception
                MsgBox("ERROR in loading image data into picture box... " & vbCrLf & _
                                  "Error: " & ex.Message)
            End Try
            mBtnViewDoc.Enabled = True
            msCurrentFileSuffix = sSuffix
        Else  '-is null
            msCurrentFileTitle = ""
            msCurrentFileSuffix = ""
        End If  '--null pic--

    End Sub '-SelectedIndexChanged-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--listViewDocs_Click--
    Public Sub lvwDocs_DblClick(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) '== Handles lvwDocs.DoubleClick
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intDocId As Integer
        Dim ev As New EventArgs

        '--  update quote info display if selection has moved..--
        item1 = mLvwDocs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            intDocId = CInt(item1.Text) '--1st column has to be job_id..--
            If intDocId > 0 Then
                Call btnViewDoc_Click(mLvwDocs, ev)
            End If
        End If '--selected..-

    End Sub '--listView_dblClick--
    '= = = = = = = =  =



End Class  '--clsAttachments-
'= = = = = = = = = = = = =  = =
'==  The End ====
