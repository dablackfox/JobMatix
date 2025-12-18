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
'-- SqlClient needed for sqlBulkCopy class.--
Imports System.Data.SqlClient

Public Class frmImportRM

    '-  Import stock etc tables INTO POS DB from Retail manager..
    '-  Import stock etc tables INTO POS DB from Retail manager..
    '-  Import stock etc tables INTO POS DB from Retail manager..

    '==
    '==  JobMatix POS3--- 3103.303= 03-March-2015 ===
    '==   >> CREATE DB: 
    '==       Form to Import RetailManager Staff, Suppliers, Stock, Customers.
    '==
    '==  JobMatix POS 3.1.3107.801---  01-Aug-2015 ===
    '==   >>  All logs and localSettings now written to c:\programdata...
    '==            gsJobMatixLocalDataDir() -
    '==   >>  Set doNotEmailDocuments - 3107.801-  2015--
    '==
    '==  JobMatix POS 3.1.3107.912---  12-Sep-2015 ===
    '==   >>  Add code to import Purchase Orders..
    '==   MUST keep .Net 2.0 version of "interop.DAO.dll"  !!!!!!
    '==   MUST keep .Net 2.0 version of "interop.DAO.dll"
    '==   MUST keep .Net 2.0 version of "interop.DAO.dll"
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  grh. 3311.225- 25Feb2016- 
    '==         >>- FIX- Use clsLocalSettings.
    '==
    '==  grh. 3311.307- 07Mar2016- 
    '==         >>- Importing SerialAuditTrail- Import RM Sales-Cust Info also..
    '==  grh. 3311.324- 24Mar2016- 
    '==         >>- Standardise format for Imported SerialAuditTrail- RM Sales-Cust Info..
    '==
    '==  POS dll- 3301.606= 06June2016=
    '==      Updates for Stock (non-stockItem attribute-
    '==  
    '==     v3.3.3301.0119..  19-Jan-2017= ===
    '==       >> frmImport- (in POS333 Shell) Fix columns in Staff import (suburb/state)...
    '==
    '==     3411.0203=  03-Feb-2018= Fixes to Import...
    '==          -- frmIMPORT from RM- UPDATE columns, and Add Layby Qty to in-stock.. 
    '==
    '==
    '==    >> 3431.0712- 12-July-2018=
    '==       --  frmImport-  Drop attempt to import MYOB Stock Columns "cog_account", "income_account".. 
    ''=               (seems to have been dropped from later MYOB versions.)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    '-- IMPORTANT! for Jet/DAO360 - Target Platform must be X86 --
    '-- IMPORTANT! for Jet/DAO360 - Target Platform must be X86 --
    '-- IMPORTANT! for Jet/DAO360 - Target Platform must be X86 --
    '-- IMPORTANT! for Jet/DAO360 - Target Platform must be X86 --

    '- http://www.vbforums.com/showthread.php?572768-VB-NET-2005-Advanced-Compile-Options 
    '-- http://stackoverflow.com/questions/8259039/system-runtime-interopservices-comexception-0x80040154-microsoft-dao-3-6 


    '== Public Const K_SAVESETTINGSPATH As String = "localPOSSettings.txt"

    Private mFrmParent As Form
    Private msVersionPOS As String = ""

    Private mbIsInitialising As Boolean = True
    Private mbActivated As Boolean = False
    Private mbStartingUp As Boolean
    Private mbMainLoadDone As Boolean = False
    Private mbFormClosing As Boolean = False

    Private msServer As String = ""
    Public gbIsSqlServer As Boolean = False
    Public gbIsJetDB As Boolean = False
    '-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private msComputerName As String '--local machine--

    '--- Actual connections for POS ---

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  jobs DB info--
    Private mCnnSql As OleDbConnection '= ADODB.Connection '-- 

    '=3311.225= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    Private msLocalSettingsPath As String = ""
    '==3311.225=
    Private mLocalSettings1 As clsLocalSettings

    Private msSaveJetPath As String
    '= Private msSaveSqlServerName As String


    '-- Multi-Retail-Host--
    Private mRetailHost1 As _clsRetailHost

    Private msRetailHostname As String
    Private msProviderCode As String '--RM  or PBPOS or JMPOS..--
    Private msJetUid As String '--jet-
    Private msJetPwd As String '--jet-
    '-- save Jet Connection for extra Queries..
    Private mCnnJet As OleDbConnection '=RM Connection '-- 

    Private msAppPath As String
    Private msImportLogPath As String
 
    '= = = = = = = = = = = = = = = = = = = = = = = = =  = = = =

    '--Private mColDBMSJet As Collection  '--Retail manager DBMS--
    Private msJetDbName As String '--full MDB path---
    '== Private mColJetDBInfo As Collection  '--  RM DB info--
    '== Private mCnnJet As ADODB.connection                      '--
    '== Private msJetPathInfoKey As String '--  eg- "RM_JetPath_computername" --

    Private mbcancelled As Boolean = False
    Private msLastSqlErrorMessage As String = ""

    '= = = = = = = = = = = = = = = = = = = = = = = = = ==

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbcancelled
        End Get
    End Property
    '= = = = = = = = =  == == 
    '-===FF->


    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                    ByVal sServer As String, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                          ByVal sVersionPOS As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = FrmParent
        msServer = sServer

        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        msVersionPOS = sVersionPOS

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- Update one setting..--
    '----change the setting in the static var dictionary..--
    '----- Write the dictionary back to disk..--

    '=3311.225= Private Function mbSaveSetting(ByVal sKey As String, _
    '= 3311.225=                                 ByVal sValue As String) As Boolean
    '= 3311.225= Dim asKeys As ICollection
    '= 3311.225= Dim sKey1 As String
    '= 3311.225= Dim sPath As String
    '= 3311.225= Dim sNewFileData As String
    '= 3311.225= Dim ix, lResult As Integer
    '= 3311.225= '--if key exists..  remove it..--
    '= 3311.225=    sNewFileData = ""
    '= 3311.225=   sPath = gsLocalSettingsPath() '=msAppPath & K_SAVESETTINGSPATH
    '= 3311.225=   Try
    '= 3311.225=      If mSdSettings.Exists(UCase(sKey)) Then
    '= 3311.225=        mSdSettings.Remove((UCase(sKey)))
    '= 3311.225=        End If
    '= 3311.225=     Catch ex As Exception
    '= 3311.225=       MsgBox("Error in mbSaveSetting. Key:" & sKey & " (Remove).." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
    '= 3311.225=      Exit Function
    '= 3311.225=    End Try
    '= 3311.225= '-- add key and new value..--
    '= 3311.225=     Try
    '= 3311.225=         mSdSettings.Add(UCase(sKey), sValue)
    '= 3311.225=     Catch ex As Exception
    '= 3311.225=          MsgBox("Error in mbSaveSetting.  Key:" & sKey & " (Add).." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
    '= 3311.225=        Exit Function
    '= 3311.225=     End Try
    '= 3311.225= '--make string of key=value cr/lf key=value crlf etc --
    '= 3311.225= '--- over write file with new string of all settings..--
    '= 3311.225=     If mSdSettings.Count > 0 Then
    '= 3311.225=         asKeys = mSdSettings.Keys
    '= 3311.225=         For Each sKey1 In asKeys
    '= 3311.225=            sNewFileData = sNewFileData & sKey1 & "=" + mSdSettings.Item(sKey1).ToString + vbCrLf
    '= 3311.225=         Next
    '= 3311.225=         lResult = glSaveTextFile(sPath, sNewFileData) '-- As Long
    '= 3311.225=         If lResult <> 0 Then
    '= 3311.225=            MsgBox("Failed to save: " & sPath & vbCrLf & _
    '= 3311.225=                     ".. Error=" & lResult & " (" & ErrorToString(System.Math.Abs(lResult)) & ")")
    '= 3311.225=         End If
    '= 3311.225=     End If '--count..--
    '= 3311.225= End Function '--save setting.--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Add msg to txtReport.text--

    Private Function mbReport(ByVal sMsg As String) As Boolean

        txtReport.AppendText(sMsg & vbCrLf)
        txtReport.Focus()
        txtReport.SelectionStart = txtReport.TextLength
        txtReport.SelectionLength = 0
        '== txtReport.Select()

    End Function  '-report-
    '= = = = = = = = = = = = = = =

    '- clear all rows in POS Table-

    Private Function mbClearTable(ByRef cnnSql As OleDbConnection, _
                                    ByVal sTableName As String) As Boolean
        Dim sSql, sErrorMsg As String
        Dim intAffected As Integer

        mbClearTable = False
        sSql = "DELETE FROM dbo." & sTableName & ";"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(mCnnSql, sSql, intAffected, sErrorMsg) Then
            MsgBox("Failed to clear Table: " & sTableName & vbCrLf & "Error msg: " & vbCrLf & _
                                  vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '== cnnSqlClient.Close()
            Exit Function
        Else '-ok-
            mbClearTable = True
            txtReport.Text &= "Cleared Table: " & sTableName & "- " & intAffected & " items.." & vbCrLf

        End If '-execute.--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '--clear-
    '= = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get RM table rows..

    Private Function mbGetRetailManagerTable(ByVal sTableName As String, _
                                              ByVal sOrderColumn As String, _
                                               ByRef datatableRM As DataTable, _
                                               Optional ByVal sInputSql As String = "", _
                                                Optional ByVal sOrderCol2 As String = "") As Boolean
        Dim sSql As String
        Dim sMsg As String

        mbGetRetailManagerTable = False
        If (sInputSql = "") Then
            sSql = "SELECT * FROM " & sTableName & " ORDER by " & sOrderColumn & "; "
        Else
            sSql = sInputSql  '-use special query..
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJet, datatableRM, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sMsg = "Failed to get RM " & sTableName & " recordset.." & vbCrLf & "Error msg: " & vbCrLf & _
                                 vbCrLf & gsGetLastSqlErrorMessage()
            Call mbReport(sMsg)
            Call gbLogMsg(msImportLogPath, vbCrLf & sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '= cnnSqlClient.Close()
            Exit Function
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mbGetRetailManagerTable = True

    End Function '-mbGetRetailManagerTable-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get POS table rows..

    Private Function mbGetPOStable(ByVal sTableName As String, _
                                              ByVal sOrderColumn As String, _
                                               ByRef datatablePOS As DataTable) As Boolean
        Dim sSql As String
        Dim sMsg As String

        mbGetPOStable = False
        sSql = "SELECT * FROM " & sTableName & " ORDER by " & sOrderColumn & "; "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, datatablePOS, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sMsg = "Failed to get POS " & sTableName & " recordset.." & vbCrLf & "Error msg: " & vbCrLf & _
                                 vbCrLf & gsGetLastSqlErrorMessage()
            Call mbReport(sMsg)
            Call gbLogMsg(msImportLogPath, vbCrLf & sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '= cnnSqlClient.Close()
            Exit Function
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mbGetPOStable = True
    End Function  '--mbGetPOSTable-
    '= = = = = = = = = = = =  = ==  
    '-===FF->

    '-- Prepare category dataTables for BulkCopy..
    '--  Category1 (RM cat1), category2 (RM cat2), StockBrand (RM cat3)

    Private Function mbPrepareCatCopy(ByVal sSqlRM As String, _
                                            ByVal sColumnRM As String, _
                                             ByRef datatableRM As DataTable, _
                                              ByVal sSqlPOS As String, _
                                               ByVal sColumnPOS As String, _
                                                 ByRef datatablePOS As DataTable) As Boolean
        mbPrepareCatCopy = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '--get rm data--
        Call mbReport("Getting RM dt for sql: " & sSqlRM & "..")
        If Not gbGetDataTable(mCnnJet, datatableRM, sSqlRM) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("ERROR: Failed to get RM recordset.." & vbCrLf & "Error msg: " & vbCrLf & _
                                 vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        Call mbReport("Found " & datatableRM.Rows.Count & " RM records.")
        Call gbLogMsg(msImportLogPath, "Found " & datatableRM.Rows.Count & " RM  records.")
        '- get POS target table for schema..
        Call mbReport("Getting POS dt for sql: " & sSqlPOS & "..")
        If Not gbGetDataTable(mCnnSql, datatablePOS, sSqlPOS) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get POS recordset.." & vbCrLf & "Error msg: " & vbCrLf & _
                                 vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '= cnnSqlClient.Close()
            Exit Function
        End If

        datatablePOS.Rows.Clear()
        '-- fill target POS datatable form RM data.
        '--ie Translate dtRetailM rows (if any) data into dtPOS rows.
        Dim posRow1 As DataRow
        Dim ix As Integer = 0  '--count brand rows-
        If (Not (datatableRM Is Nothing)) AndAlso (datatableRM.Rows.Count > 0) Then
            Call mbReport("Translating RM records to POS data table.")
            For Each rmRow1 As DataRow In datatableRM.Rows
                '-- Fill all POS columns
                posRow1 = datatablePOS.NewRow()
                '-- for stockBrands, must supply ID also..-
                ix += 1
                If (InStr(LCase(sSqlPOS), "stockbrands") > 0) Then
                    posRow1.Item("brand_id") = ix
                Else  '-- cat1/cat have description.-
                    posRow1.Item("description") = ""
                End If
                posRow1.Item(sColumnPOS) = rmRow1.Item(sColumnRM)
                posRow1.Item("date_created") = DateTime.Today
                posRow1.Item("date_modified") = DateTime.Today

                datatablePOS.Rows.Add(posRow1)
            Next rmRow1
        End If '--RM nothing.-
        mbPrepareCatCopy = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function  '-mbPrepareCatDatatables-
    '= = = = = = = = = = = =  = = = = = = =   
    '-===FF->

    '-- Do Bulk  Copy --
    '-- Do Bulk  Copy --

    Private Function mbDoBulkCopy(ByRef bulkCopy1 As SqlBulkCopy, _
                                   ByRef dtPOS As DataTable, _
                                   ByRef sDestTableName As String) As Boolean
        Dim sMsg As String

        mbDoBulkCopy = False
        labProgress.Text = ""
        labTargetCount.Text = CStr(dtPOS.Rows.Count)
        sMsg = "Bulk copying " & dtPOS.Rows.Count & " " & sDestTableName & " rows to server."
        Call mbReport(sMsg)
        Call gbLogMsg(msImportLogPath, sMsg)
        bulkCopy1.DestinationTableName = "dbo." & sDestTableName  '= "dbo.staff"
        Try
            ' Write from the source to the destination.
            '= bulkCopy.WriteToServer(newProducts)
            bulkCopy1.WriteToServer(dtPOS)
            sMsg = "BulkCopy- all " & sDestTableName & " rows done.."
            Call mbReport(sMsg)
            Call gbLogMsg(msImportLogPath, sMsg)
        Catch ex As Exception
            sMsg = "ERROR in " & sDestTableName & " table BulkCopy.. " & vbCrLf & vbCrLf & _
                                                                   ex.Message
            Call gbLogMsg(msImportLogPath, sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        mbDoBulkCopy = True

    End Function  '-mbDoBulkCopy-
    '= = = = = = = = = = = =  = ==  
    '-===FF->

    '-- Connect to Retail Manager..--
    '-- Borrowed from MULTI-HOST version--

    '--IMORT:  sProviderCode is "RM" ONLY.. --

    Private Function mbRMConnect(Optional ByVal bNewPath As Boolean = False) As Boolean
        '==Dim sSettingsDB As String
        Dim sSettingsUid As String
        Dim sSettingsPwd As String
        Dim bUpdateJet, bConnected As Boolean
        Dim sSchema As String = ""

        '== bNewPath = False
        mbRMConnect = False

        '==msJetDbName = mSdSystemInfo.Item(UCase(msJetPathInfoKey))
        msJetDbName = mLocalSettings1.item("JETDBNAME") '= mSdSettings.Item("JETDBNAME")

        msJetUid = "admin"  '== mSdSystemInfo.Item(sSettingsUid)
        msJetPwd = ""   '== mSdSystemInfo.Item(sSettingsPwd)

        '--  can request browse for new DB..
        If bNewPath Then msJetDbName = "" '--force new path..-

        '--connect to jet db..--
        '==3083== labRetailHostPrompt.Visible = True
        bUpdateJet = False
        If (msJetDbName = "") Then bUpdateJet = True
        bConnected = False
        While Not bConnected
            If Not mRetailHost1.connect("", msJetDbName, msJetUid, msJetPwd, bNewPath, sSchema) Then
                If (MsgBox("No Retail Host connection.." & vbCrLf & _
                             "Do you want to retry", _
                              MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) <> MsgBoxResult.Yes) Then
                    Exit Function
                End If
            Else  '--ok-
                bConnected = True
                mCnnJet = mRetailHost1.connection  '-Save-
                Call mLocalSettings1.SaveSetting("JetDbName", msJetDbName)
                Call gbLogMsg(msImportLogPath, "= JobMatixPOS-Import- Connected to RM DB: " & msJetDbName)
                '- save sSchema text--
                Call glSaveTextFile(gsJobMatixLocalDataDir() & "\RetailM_schema.txt", sSchema)
            End If
        End While
        On Error GoTo 0
        '-- save jet parms in sysinfo..--
        '==If bUpdateJet Or bNewPath Then
        '==End If '--update jet..-
        labRMfilepath.Text = msJetDbName

        mbRMConnect = True
    End Function '--RM-connect..-
    '= = = = = = =
    '-===FF->

    '-- L o a d  --

    Private Sub frmImportRM_Load(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles MyBase.Load
        Dim s1 As String

        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        '= gsAppPath = sAppPath
        '= msAppPath = sAppPath
        msLocalSettingsPath = gsLocalJobsSettingsPath()
        '=3311.225=
        mLocalSettings1 = New clsLocalSettings(msLocalSettingsPath)

        '-  new log each month..-
        s1 = VB6.Format(CDate(DateTime.Today), "yyyy-mm-dd")

        Call gbLogMsg(gsRuntimeLogPath, "=== JobMatixPOSRM Import form is loading..")

        '= Call gbLoadSettings(gsAppPath & K_SAVESETTINGSPATH, mSdSettings)
        '= gsLocalSettingsPath() -
        '=3311.225= Call gbLoadSettings(gsLocalSettingsPath(), mSdSettings)

        '== msImportLogPath = msAppPath & "JMx-RM-Import-" & VB.Left(s1, 7) & ".log"
        msImportLogPath = gsJobMatixLocalDataDir() & "\JMx-RM-Import-" & VB.Left(s1, 7) & ".log"
        Call gbLogMsg(msImportLogPath, vbCrLf & "= JobMatixPOS- RM Import form is loading..")

        btnStart.Enabled = False
        btnBrowse.Enabled = False

        labTargetCount.Text = ""
        labProgress.Text = ""
        labRMfilepath.Text = ""

        txtReport.Text = ""
        '- position on top of calling form..
        If mFrmParent Is Nothing Then
            Call CenterForm(Me)
        Else
            Me.Left = mFrmParent.Left + 16
            Me.Top = mFrmParent.Top + 60
        End If
        labTargetDBname.Text = msSqlDbName
        labAction.Text = ""
  
        labVersion.Text = msVersionPOS

    End Sub  '-load-
    '= = = = = = = = = = = 

    Private Sub frmImportRM_Activated(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True

        '-- SET RETAIL MANAGER as Host Provider (Class)--
        msProviderCode = "RM"
        '= cmdPOS.Visible = False
        '= labStatus.Text = "Connecting to RetailManager database: " & msJetDbName
        '= LabQuotesHdr.Text = "RetailManager Quotes"
        System.Windows.Forms.Application.DoEvents()
        mRetailHost1 = New clsRetailHost

        If mbRMConnect() Then
            btnStart.Enabled = True
        End If

        btnBrowse.Enabled = True

        mbcancelled = True   '--must get to the end..-

    End Sub  '-activated-
    '= = = = = = = = = = = 
    '-===FF->

    '-browse-

    Private Sub btnBrowse_Click(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles btnBrowse.Click
        If mbRMConnect(True) Then
            btnStart.Enabled = True
        End If
    End Sub  '-browse--
    '= = = = = = = = = = = 

    '-- Bulk Copy event to notify rows copied..

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlRowsCopiedEventArgs)
        labProgress.Text = "" & args.RowsCopied.ToString()

    End Sub  '--rows copied-
    '= = = = = = = = = = = 
    '-===FF->

    '-- I M P O R T  Staff/Supplier/Stock/Goods Content into POS3 Tables..
    '-- I M P O R T  Staff/Supplier/Stock/Goods Content into POS3 Tables..

    Private Sub btnStart_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnStart.Click
        Dim sConnect, sSql, sSqlRM, sSqlPOS As String
        Dim sMsg, sErrorMsg, s1, s2 As String
        Dim sBarcode, sBarcode2 As String
        Dim dtRetailM, dtPOS, dtPOS_lines As DataTable
        Dim posRow1, rmRow1 As DataRow
        Dim cnnSqlClient As SqlConnection  '--Bulk Copy Connection.--
        Dim sqlTran1 As SqlTransaction  '-can be nothing-
        Dim ix, intStock_id, intLast_id, intSupplier_id, intAffected As Integer
        Dim bOk As Boolean
        Dim colUniqueKeys As Collection

        btnStart.Enabled = False
        btnBrowse.Enabled = False
        Kill(msImportLogPath)  '--starts new import log..-

        labProgress.Text = ""
        labTargetCount.Text = ""
        mbcancelled = True   '--must get to the end..-

        sConnect = "Persist Security Info=False;Integrated Security=SSPI; " & _
                         "Initial Catalog=" & msSqlDbName & "; server=" & msServer & "; "

        cnnSqlClient = New SqlConnection
        '==3072== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtReport.Text = "Sql bcp Connecting to Server: " & msServer & vbCrLf

        Try
            cnnSqlClient.ConnectionString = sConnect
            cnnSqlClient.Open()
            sMsg = "BulkCopy Connected ok to Sql Server.." & vbCrLf
            sMsg = sMsg & "   ConnectStr.=" & sConnect & vbCrLf
            '--msg = msg & "   Conn State= " + gsGetState(mCnnSql.State)
            '-test -
            '= MsgBox(sMsg, vbInformation, "RM Import")

        Catch ex As Exception
            sMsg = "Failed Connect to Sql Server.." & vbCrLf
            sMsg = sMsg & "Error: " & ex.Message & vbCrLf
            sMsg = sMsg & "connect string=<" & sConnect & ">"
            s2 = sMsg & vbCrLf '== & "SQL-Provider errors are:" & vbCrLf & s1
            '== If gbDebug Then MsgBox(s2, MsgBoxStyle.Critical, "Sql Connect..")
            msLastSqlErrorMessage = s2
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, s2 & vbCrLf & "-- end of error msg.--")
            End If '--log--
            Call gbLogMsg(msImportLogPath, s2 & vbCrLf & "-- end of error msg.--")
            Exit Sub
        End Try
        Call mbReport(sMsg)
        Call gbLogMsg(msImportLogPath, vbCrLf & sMsg)
        cnnSqlClient.ChangeDatabase(msSqlDbName)

        Dim bulkCopy1 As SqlBulkCopy = _
                      New SqlBulkCopy(cnnSqlClient, _
                           SqlBulkCopyOptions.KeepIdentity + SqlBulkCopyOptions.KeepNulls, sqlTran1)
        AddHandler bulkCopy1.SqlRowsCopied, AddressOf OnSqlRowsCopied
        bulkCopy1.NotifyAfter = 100
        '== bulkCopy1.DestinationTableName = "dbo.staff"

        '-- Clear all POS Tables upfront to avoid Reference issues.
        '=- Found 25 tables (+systemInfo): 
        '=  Cashup_Sessions Cashup_Shortages category1 category2 
        '=     Customer GoodsReceived GoodsReceivedLine Invoice InvoiceLine 
        '=       PaymentDetails PaymentDisbursements Payments 
        '=      PurchaseOrder PurchaseOrderLine SalesOrder SalesOrderLine SerialAudit SerialAuditTrail Staff 
        '=        Stock StockBrands StockTake StockTakeSerials Supplier SupplierCode  SystemInfo 

        '-  Order of deleting is important..
        bOk = mbClearTable(mCnnSql, "StockTakeItems")  '=3301.606=
        bOk = mbClearTable(mCnnSql, "StockTakeSerials")
        bOk = mbClearTable(mCnnSql, "StockTake")
        '== bOk = mbClearTable(mCnnSql, "SalesOrderLine")
        bOk = mbClearTable(mCnnSql, "SupplierReturnLine")  '=3411.0203=
        bOk = mbClearTable(mCnnSql, "SupplierReturns")

        bOk = mbClearTable(mCnnSql, "SubscriptionLine")  '=3411.0203=
        bOk = mbClearTable(mCnnSql, "SubscriptionInvoice")  '=3411.0203=
        bOk = mbClearTable(mCnnSql, "Subscription")  '=3411.0203=

        bOk = mbClearTable(mCnnSql, "LaybyLine")  '=3411.0203=
        bOk = mbClearTable(mCnnSql, "Layby")  '=3411.0203=

        bOk = mbClearTable(mCnnSql, "PaymentDisbursements")  '-restored 3301.705-
        bOk = mbClearTable(mCnnSql, "PaymentDetails")
        bOk = mbClearTable(mCnnSql, "Payments")
        bOk = mbClearTable(mCnnSql, "InvoiceLine")
        bOk = mbClearTable(mCnnSql, "Invoice")
        bOk = mbClearTable(mCnnSql, "Cashup_Shortages")
        bOk = mbClearTable(mCnnSql, "Cashup_Sessions")
        bOk = mbClearTable(mCnnSql, "SerialAuditTrail")
        bOk = mbClearTable(mCnnSql, "SerialAudit")
        bOk = mbClearTable(mCnnSql, "GoodsReceivedLine")
        bOk = mbClearTable(mCnnSql, "GoodsReceived")
        bOk = mbClearTable(mCnnSql, "PurchaseOrderLine")
        bOk = mbClearTable(mCnnSql, "PurchaseOrder")
        bOk = mbClearTable(mCnnSql, "SalesOrderLine")
        bOk = mbClearTable(mCnnSql, "SalesOrder")
        bOk = mbClearTable(mCnnSql, "SupplierCode")
        bOk = mbClearTable(mCnnSql, "stock")
        bOk = mbClearTable(mCnnSql, "category2")
        bOk = mbClearTable(mCnnSql, "category1")
        bOk = mbClearTable(mCnnSql, "StockBrands")
        bOk = mbClearTable(mCnnSql, "Customer")
        bOk = mbClearTable(mCnnSql, "Supplier")
        bOk = mbClearTable(mCnnSql, "staff")
        '== bOk = mbClearTable(mCnnSql, "staff")


        '-- NO.. make directory to export "csv" files into-
        '== sExportDir = msAppPath & "exportedData\"
        '= System.IO.Directory.CreateDirectory(sExportDir)

        '- Import RM Staff table..
        '- Import RM Staff table..
        '- Import RM Staff table..
        '--  KEEP IDENTITIES !! --

        '--1. Get all RM staff records.. -> dtRetailM
        '--2. Get empty POS Staff table -> dtPOS  (to structure d"tPOS)
        '--3.  Translate dtRetailM rows data into dtPOS rows.
        '--4.  BulkCopy Translated RM data (dtPOS) to Actual Staff Table..

        Call mbReport(vbCrLf & "Getting RM staff data..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM staff data..")
        '--1. Get all JET RM staff records.. -> dtRetailM
        If Not mbGetRetailManagerTable("staff", "staff_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " RM staff records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM Staff records.")

        '--2. Get Empty POS Staff records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type staff datatable--
        If Not mbGetPOStable("staff", "staff_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If

        '- testing..
        '== MsgBox("Found " & dtPOS.Rows.Count & " POS staff records.", MsgBoxStyle.Information)
        Call mbReport("Found " & dtPOS.Rows.Count & " POS STAFF records.")

        '-- WAS Emptied out dtPOS (DELETES.) -
        '--dtpos has structure we need..-
        '-- DELETE all current POS records.
        dtPOS.Rows.Clear()
        '--  WAS DONE above in actual DB table.
        '== bOk = mbClearTable(mCnnSql, "staff")

        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            Call mbReport("Translating RM staff records to POS  data table.")
            For Each rmRow1 In dtRetailM.Rows
                posRow1 = dtPOS.NewRow()
                posRow1.Item("staff_id") = rmRow1.Item("staff_id")
                posRow1.Item("barcode") = rmRow1.Item("barcode")
                posRow1.Item("lastName") = rmRow1.Item("surname")
                posRow1.Item("firstName") = rmRow1.Item("given_names")
                posRow1.Item("docket_name") = rmRow1.Item("docket_name")
                posRow1.Item("position") = rmRow1.Item("position")
                posRow1.Item("isAdministrator") = 0 '-false-
                posRow1.Item("inactive") = rmRow1.Item("inactive")
                posRow1.Item("dateOfBirth") = rmRow1.Item("dob")
                '-- combine address-
                s1 = rmRow1.Item("addr1")
                If (Not IsDBNull(rmRow1.Item("addr2"))) AndAlso (rmRow1.Item("addr2") <> "") Then
                    s1 &= vbCrLf & rmRow1.Item("addr2")
                End If
                If (Not IsDBNull(rmRow1.Item("addr3"))) AndAlso (rmRow1.Item("addr3") <> "") Then
                    s1 &= vbCrLf & rmRow1.Item("addr3")
                End If
                posRow1.Item("address") = s1
                posRow1.Item("suburb") = rmRow1.Item("suburb")
                posRow1.Item("state") = rmRow1.Item("state")
                posRow1.Item("postcode") = rmRow1.Item("postcode")
                posRow1.Item("homePhone") = rmRow1.Item("phone")
                posRow1.Item("mobile") = rmRow1.Item("mobile")
                posRow1.Item("emailAddress") = rmRow1.Item("email")
                posRow1.Item("status") = ""
                posRow1.Item("password") = ""
                posRow1.Item("passwordHint") = ""
                '== posRow1.Item("staffPicture") = DBNull
                posRow1.Item("date_created") = rmRow1.Item("date_modified")
                posRow1.Item("date_modified") = rmRow1.Item("date_modified")

                dtPOS.Rows.Add(posRow1)
            Next rmRow1  '-RM-

            '--4.  BulkCopy Translated RM data (dtPOS) to Actual Staff Table..
            '-- STAFF ready to copy.--
            If Not mbDoBulkCopy(bulkCopy1, dtPOS, "staff") Then
                cnnSqlClient.Close()
                Exit Sub
            End If  '-copy-
        End If '-RM Staff nothing.-
        Call gbLogMsg(msImportLogPath, "= Staff Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '- Import RM  C u s t o m e r  Table..
        '- Import RM  C u s t o m e r  Table..
        '- Import RM  C u s t o m e r  Table..

        '--  KEEP IDENTITIES !! --

        '--1. Get all RM CUSTOMER records.. -> dtRetailM
        '--2. Get empty POS Customer table -> dtPOS  (to structure dtPOS)
        '--3.  Translate dtRetailM Customer rows data into dtPOS Customer rows.
        '--4.  BulkCopy Translated RM data (dtPOS) to Actual Customer Table..

        Call mbReport(vbCrLf & "Getting RM Customer data..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM Customer data..")
        '--1. Get all JET RM  Customer records.. -> dtRetailM
        If Not mbGetRetailManagerTable("customer", "customer_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " RM Customer records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM Customer records.")

        '--2. Get Empty POS  Customer records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  Customer datatable--
        If Not mbGetPOStable("customer", "customer_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        Call mbReport("Found " & dtPOS.Rows.Count & " POS customer records.")
        dtPOS.Rows.Clear()  '-i case-
        '--  WAS DONE above in actual DB table.

        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            Call mbReport("Translating RM CUSTOMER records to POS  data table.")
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 27 POS c0lumns
                posRow1 = dtPOS.NewRow()
                posRow1.Item("customer_id") = rmRow1.Item("customer_id")
                posRow1.Item("barcode") = rmRow1.Item("barcode")
                posRow1.Item("companyName") = rmRow1.Item("company")
                posRow1.Item("lastName") = rmRow1.Item("surname")
                posRow1.Item("firstName") = rmRow1.Item("given_names")
                posRow1.Item("position") = rmRow1.Item("position")
                posRow1.Item("title") = rmRow1.Item("salutation") '-salutation-
                posRow1.Item("inactive") = rmRow1.Item("inactive")
                posRow1.Item("isAccountCust") = rmRow1.Item("account")

                '- rejig grade (int) to Char.-
                posRow1.Item("pricingGrade") = Chr((rmRow1.Item("grade") Mod 16) + 48)

                posRow1.Item("openedStaff_id") = rmRow1.Item("opened_id")
                posRow1.Item("openedStaffName") = ""
                posRow1.Item("creditLimit") = rmRow1.Item("limit")
                posRow1.Item("creditDays") = rmRow1.Item("days")

                '-- combine address-
                s1 = rmRow1.Item("addr1")
                If (Not IsDBNull(rmRow1.Item("addr2"))) AndAlso (rmRow1.Item("addr2") <> "") Then
                    s1 &= vbCrLf & rmRow1.Item("addr2")
                End If
                If (Not IsDBNull(rmRow1.Item("addr3"))) AndAlso (rmRow1.Item("addr3") <> "") Then
                    s1 &= vbCrLf & rmRow1.Item("addr3")
                End If
                posRow1.Item("address") = s1
                posRow1.Item("suburb") = rmRow1.Item("suburb")
                posRow1.Item("state") = rmRow1.Item("state")
                posRow1.Item("postcode") = rmRow1.Item("postcode")
                posRow1.Item("country") = rmRow1.Item("country")
                posRow1.Item("phone") = rmRow1.Item("phone")
                posRow1.Item("fax") = rmRow1.Item("fax")
                posRow1.Item("mobile") = rmRow1.Item("mobile")
                posRow1.Item("email") = rmRow1.Item("email")
                posRow1.Item("abn") = rmRow1.Item("abn")
                posRow1.Item("comments") = rmRow1.Item("notes") & vbCrLf & rmRow1.Item("comments")
                posRow1.Item("date_created") = rmRow1.Item("date_created")
                posRow1.Item("date_modified") = rmRow1.Item("date_modified")
                '- doNotEmailDocuments - 3107.801-  2015--
                posRow1.Item("doNotEmailDocuments") = 0   '-default- False. (0= can email ok)..
                dtPOS.Rows.Add(posRow1)
            Next rmRow1  '-RM-

            '--4.  BulkCopy Translated RM data (dtPOS) to Actual Customer Table..
            '-- Customer ready to copy.--
            If Not mbDoBulkCopy(bulkCopy1, dtPOS, "customer") Then
                cnnSqlClient.Close()
                Exit Sub
            End If  '-copy-
        End If '-RM CUSTOMER nothing.-
        Call gbLogMsg(msImportLogPath, "= Customer Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)
        '== labProgress.Text = ""
        '== labTargetCount.Text = ""

        '- Import RM Supplier table..
        '- Import RM Supplier table..
        '- Import RM Supplier table..
        '--  KEEP IDENTITIES !! --

        '--1. Get all RM Supplier records.. -> dtRetailM
        '--2. Get empty POS Supplier table -> dtPOS  (to structure d"tPOS)
        '--3.  Translate dtRetailM rows data into dtPOS rows.
        '--4.  BulkCopy Translated RM data (dtPOS) to Actual Supplier Table..

        Call mbReport(vbCrLf & "Getting RM Supplier data..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM Supplier data..")
        '--1. Get all JET RM  Customer records.. -> dtRetailM
        If Not mbGetRetailManagerTable("supplier", "supplier_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " RM Supplier records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM Supplier records.")

        '--2. Get Empty POS  Supplier records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  Supplier datatable--
        If Not mbGetPOStable("supplier", "supplier_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        Call mbReport("Found " & dtPOS.Rows.Count & " POS Supplier records.")
        dtPOS.Rows.Clear()  '-i case-
        '--  WAS DONE above in actual DB table.

        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            Call mbReport("Translating RM SUPPLIER records to POS data table.")
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 28 POS Supplier columns
                posRow1 = dtPOS.NewRow()
                posRow1.Item("supplier_id") = rmRow1.Item("supplier_id")
                posRow1.Item("barcode") = rmRow1.Item("barcode")
                posRow1.Item("supplierName") = rmRow1.Item("supplier")

                '- rejig grade (int) to Char.-
                posRow1.Item("grade") = CStr(Chr((rmRow1.Item("grade") Mod 16) + 48))
                posRow1.Item("inactive") = rmRow1.Item("inactive")
                posRow1.Item("contactName") = rmRow1.Item("main_contact")
                posRow1.Item("contactPosition") = rmRow1.Item("main_position")

                '-- combine address-
                s1 = rmRow1.Item("main_addr1")
                If (Not IsDBNull(rmRow1.Item("main_addr2"))) AndAlso (rmRow1.Item("main_addr2") <> "") Then
                    s1 &= vbCrLf & rmRow1.Item("main_addr2")
                End If
                If (Not IsDBNull(rmRow1.Item("main_addr3"))) AndAlso (rmRow1.Item("main_addr3") <> "") Then
                    s1 &= vbCrLf & rmRow1.Item("main_addr3")
                End If
                posRow1.Item("address") = s1
                posRow1.Item("suburb") = rmRow1.Item("main_suburb")
                posRow1.Item("state") = rmRow1.Item("main_state")
                posRow1.Item("postcode") = rmRow1.Item("main_postcode")
                posRow1.Item("country") = rmRow1.Item("main_country")
                posRow1.Item("phone") = rmRow1.Item("main_phone")
                posRow1.Item("fax") = rmRow1.Item("main_fax")
                '==posRow1.Item("mobile") = rmRow1.Item("mobile")
                posRow1.Item("emailAddress") = rmRow1.Item("main_email")
                '-webSiteURL-
                posRow1.Item("webSiteURL") = ""  '= rmRow1.Item("email")

                posRow1.Item("altContactName") = rmRow1.Item("other_contact")
                posRow1.Item("altContactPosition") = rmRow1.Item("other_position")
                posRow1.Item("altPhone") = rmRow1.Item("other_phone")
                posRow1.Item("altFax") = rmRow1.Item("other_fax")
                '==posRow1.Item("mobile") = rmRow1.Item("mobile")
                posRow1.Item("altEmail") = rmRow1.Item("other_email")

                posRow1.Item("freight_free") = rmRow1.Item("freight_free")
                posRow1.Item("reject_backorders") = rmRow1.Item("reject_backorders")
                posRow1.Item("deliveryDays") = rmRow1.Item("delivery_delay")

                posRow1.Item("abn") = rmRow1.Item("abn")
                posRow1.Item("comments") = "Migrated " & VB.Format(Today, "dd-MMM-yyyy") & " from Retail Manager.."
                posRow1.Item("date_created") = rmRow1.Item("date_modified")
                posRow1.Item("date_modified") = rmRow1.Item("date_modified")
                dtPOS.Rows.Add(posRow1)
            Next rmRow1  '-RM-

            '--4.  BulkCopy Translated RM data (dtPOS) to Actual supplier Table..
            '-- supplier ready to copy.--
            If Not mbDoBulkCopy(bulkCopy1, dtPOS, "supplier") Then
                cnnSqlClient.Close()
                Exit Sub
            End If  '-copy-
        End If '-RM Supplier nothing.-
        Call gbLogMsg(msImportLogPath, "= Supplier Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Stock categories..
        '-- Stock categories..

        '- Get categories in use in RM Stock, 
        '- and make POS table out of the values..

        '-- Category1 --
        '-- Category1 --
        Call mbReport(vbCrLf & "Getting RM Category-1 list..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM Cat1 data..")
        sSqlRM = "SELECT DISTINCT Cat1 FROM Stock ORDER BY Cat1; "
        sSqlPOS = "SELECT * FROM dbo.category1 ORDER BY cat1_key; "
        If Not mbPrepareCatCopy(sSqlRM, "cat1", dtRetailM, sSqlPOS, "cat1_key", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '-- now copy Cat1..
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "category1") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        Call gbLogMsg(msImportLogPath, "=Cat1 Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Category2 --
        '-- Category2 --
        Call mbReport(vbCrLf & "Getting RM Category-2 list..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM Cat2 data..")
        sSqlRM = "SELECT DISTINCT Cat2 FROM Stock ORDER BY Cat2; "
        sSqlPOS = "SELECT * FROM dbo.category2 ORDER BY cat2_key; "
        If Not mbPrepareCatCopy(sSqlRM, "cat2", dtRetailM, sSqlPOS, "cat2_key", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '-- now copy Cat2..
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "category2") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        Call gbLogMsg(msImportLogPath, "= Cat2 Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Brand (Category3) --
        Call mbReport(vbCrLf & "Getting RM Cat3 (stockBrands) data..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM Cat3 (brands) data..")
        sSqlRM = " SELECT DISTINCT cat3 FROM " & _
                   " (SELECT CategoryValues.description AS cat3,  cat1, cat2 " & _
                          " FROM stock " & _
                          " LEFT JOIN (CategorisedStock " & _
                          " LEFT JOIN CategoryValues ON ( CategoryValues.catValue_id= CategorisedStock.catValue_id) " & _
                          "   )     ON ((stock.stock_id=CategorisedStock.stock_id) AND (CategorisedStock.category_level=3)) ) "
        sSqlPOS = "SELECT * FROM dbo.stockBrands ORDER BY brand_id; "
        If Not mbPrepareCatCopy(sSqlRM, "cat3", dtRetailM, sSqlPOS, "brandName", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '-- now copy Cat2..
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "stockBrands") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        Call gbLogMsg(msImportLogPath, "= Stock Brands Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Now Import Stock Table..
        '-- Now Import Stock Table..
        '- Import RM STOCK table..
        '--  KEEP IDENTITIES !! --

        '--1. Get all RM STOCK records.. -> dtRetailM
        '--2. Get empty POS STOCK table -> dtPOS  (to structure d"tPOS)
        '--3.  Translate dtRetailM rows data into dtPOS rows.
        '--4.  BulkCopy Translated RM data (dtPOS) to Actual POS Stock Table..

        '-- ok.. get RM recordset (ex brands (cat3) to translate/copy..

        Call gbLogMsg(msImportLogPath, vbCrLf & "= Importing RM stock data...")
        Call mbReport(vbCrLf & "Getting RM stock data.. ")
        '--1. Get all JET RM  stock records.. -> dtRetailM
        If Not mbGetRetailManagerTable("stock", "stock_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " RM STOCK records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM Stock records.")
        '-test- show columns-
        s1 = ""
        For Each datacol1 As DataColumn In dtRetailM.Columns
            s1 = s1 & datacol1.ColumnName & "; "
        Next datacol1
        '== Call mbReport("RM Stock columns are: " & vbCrLf & s1 & vbCrLf)

        '--2. Get Empty POS stock records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  stock datatable--
        If Not mbGetPOStable("stock", "stock_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '= Call mbReport("Found " & dtPOS.Rows.Count & " POS stock records.")
        dtPOS.Rows.Clear()  '-in case-
        '--  WAS DONE above in actual DB table.

        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            Dim colRecord As Collection
            Dim sCat3 As String
            Dim intPass As Integer
            '=3411.0203=
            Dim intQtyInStock, intLaybyQty
            Dim bIsPackage As Boolean

            ix = 0    '--count rows.-
            colUniqueKeys = New Collection
            intLast_id = 0
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating Stock Table.."

            Call mbReport("Translating RM STOCK records to POS data table.")
            '-- DO 2 passes so we pick up ACTIVE records first..
            '--  These will be the valid ones for DUPs..
            For intPass = 1 To 2
                For Each rmRow1 In dtRetailM.Rows
                    If ((intPass = 1) And (Not rmRow1.Item("inactive"))) Or _
                             ((intPass = 2) And (rmRow1.Item("inactive"))) Then '--can have it.-
                        '-- Fill all 27 POS stock columns (ex Picture)-
                        posRow1 = dtPOS.NewRow()
                        intStock_id = rmRow1.Item("stock_id")
                        sBarcode = rmRow1.Item("barcode")
                        '=3411.0203= Include laby qty in the Stock count..
                        intQtyInStock = CInt(rmRow1.Item("quantity"))
                        intQtyInStock += CInt(rmRow1.Item("layby_qty"))
                        '=3411.0203= For packages- make Stock count =Zero=..
                        bIsPackage = (rmRow1.Item("package") <> 0)
                        If bIsPackage Then
                            intQtyInStock = 0
                        End If
                        If (intStock_id > 0) Then
                            '-- Special query to give us Cat3.--
                            If Not mRetailHost1.stockGetJobPartStockInfo("", intStock_id, colRecord) Then
                                sCat3 = "--"
                            Else  '-found-
                                sCat3 = colRecord.Item("cat3")("value")
                            End If
                            If colUniqueKeys.Contains(sBarcode) Then
                                Call mbReport("** DUPLICATE Barcode- id=" & intStock_id & "; barcode=" & sBarcode)
                                '== MsgBox("*** DUPLICATE Barcode- id=" & intStock_id & " barcode=" & sBarcode)
                                '=sBarcode2 = VB.Left("DUP" & CStr(ix + 1) & "-" & sBarcode, 15)  '--MODIFY-
                                sBarcode2 = sBarcode & "-DUP-" & CStr(ix + 1)  '--MODIFY-
                                Call gbLogMsg(msImportLogPath, _
                                         "** DUPLICATE Stock Barcode- id=" & intStock_id & "; barcode=" & sBarcode)
                            Else  '--ok-
                                sBarcode2 = sBarcode  '--ok to go-
                                colUniqueKeys.Add(sBarcode, sBarcode)  '--add to unique collection.-
                            End If  '-contains.-
                            posRow1.Item("stock_id") = intStock_id  '= rmRow1.Item("stock.stock_id")
                            posRow1.Item("barcode") = sBarcode2  '==rmRow1.Item("barcode")
                            posRow1.Item("inactive") = rmRow1.Item("inactive")
                            posRow1.Item("model_no") = ""
                            posRow1.Item("description") = rmRow1.Item("description")
                            posRow1.Item("sales_prompt") = rmRow1.Item("sales_prompt")
                            '=3301.606= posRow1.Item("isServiceItem") = 0
                            '=3301.606= posRow1.Item("isLabour") = 0   '-- can discover ??--
                            posRow1.Item("isNonStockItem") = rmRow1.Item("static_quantity")   '=3301.606= 
                            posRow1.Item("track_serial") = rmRow1.Item("track_serial")
                            posRow1.Item("allow_renaming") = rmRow1.Item("allow_renaming")
                            posRow1.Item("longDescription") = rmRow1.Item("longdesc")
                            posRow1.Item("cat1") = rmRow1.Item("cat1")
                            posRow1.Item("cat2") = rmRow1.Item("cat2")
                            '-- Special query gave us Cat3.--
                            posRow1.Item("BrandName") = sCat3
                            '==If Not IsDBNull(rmRow1.Item("cat3")) Then
                            '==posRow1.Item("BrandName") = rmRow1.Item("cat3")
                            '==Else
                            '==posRow1.Item("BrandName") = ""
                            '== End If
                            posRow1.Item("goods_taxCode") = rmRow1.Item("goods_tax")
                            posRow1.Item("costExTax") = rmRow1.Item("cost")
                            posRow1.Item("sales_taxCode") = rmRow1.Item("sales_tax")
                            posRow1.Item("sellExTax") = rmRow1.Item("sell")
                            '=3411.0203=
                            posRow1.Item("qtyInStock") = intQtyInStock '== CInt(rmRow1.Item("quantity"))
                            posRow1.Item("reOrderLevel") = CInt(rmRow1.Item("order_threshold"))
                            posRow1.Item("order_quantity") = CInt(rmRow1.Item("order_quantity"))
                            posRow1.Item("supplier_id") = rmRow1.Item("supplier_id")
                            posRow1.Item("freight") = rmRow1.Item("freight")
                            '-NB-3431.0712-
                            '=MYOB cols "cog_account" and "income_account"
                            '--  seem to have been dropped in later RM versions.
                            posRow1.Item("cost_account") = ""  '= rmRow1.Item("cog_account")
                            posRow1.Item("income_account") = ""  '=rmRow1.Item("income_account")
                            posRow1.Item("comments") = "Migrated " & VB.Format(Today, "dd-MMM-yyyy") & " from Retail Manager.."
                            '== posRow1.Item("productPicture") = null

                            posRow1.Item("date_created") = rmRow1.Item("date_created")
                            posRow1.Item("date_modified") = rmRow1.Item("date_modified")

                            dtPOS.Rows.Add(posRow1)
                            ix += 1
                            intLast_id = intStock_id  '-update-
                            labProgress.Text = CStr(ix)
                            DoEvents()
                        End If '--stock-id.
                    End If  '--pass-
                Next rmRow1
            Next intPass
        End If '-dtRetailM Is Nothing-
        '-- now copy stock..
        labAction.Text = "Bulk copying Stock Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "stock") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= Stock Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Import SupplierCode --
        '-- Import SupplierCode --
        '-- Import SupplierCode --

        '-- ok.. get RM recordset (SupplierCode) to translate/copy..

        Call mbReport(vbCrLf & "Getting RM SupplierCode data.. " & vbCrLf & sSql)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM SupplierCode data..")
        '--1. Get all JET RM  stock records.. -> dtRetailM
        If Not mbGetRetailManagerTable("SupplierCode", "supcode", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " RM SupplierCode records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM SupplierCode records.")

        '--2. Get Empty POS SupplierCode records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  SupplierCode datatable--
        If Not mbGetPOStable("SupplierCode", "SupCode", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '==Call mbReport("Found " & dtPOS.Rows.Count & " POS SupplierCode records.")
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            Dim sSupCode As String = ""
            Dim sSupCode2 As String = ""
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating SupplierCode Table.."
            Call mbReport("Translating RM SupplierCode records to POS data table.")
            ix = 0    '--count rows.-
            colUniqueKeys = New Collection
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 5 POS SupplierCode columns -
                posRow1 = dtPOS.NewRow()
                sSupCode = rmRow1.Item("supcode")
                intStock_id = rmRow1.Item("stock_id")
                intSupplier_id = rmRow1.Item("supplier_id")
                If colUniqueKeys.Contains(sSupCode) Then
                    Call mbReport("** DUPLICATE SupplierCode- " & vbCrLf & _
                                "Stock-id=" & intStock_id & ";  Supplier-id=" & intSupplier_id & "; supcode=" & sSupCode)
                    '== MsgBox("*** DUPLICATE Barcode- id=" & intStock_id & " barcode=" & sBarcode)
                    sSupCode2 = VB.Left("DUP" & CStr(ix + 1) & "-" & sSupCode, 15)  '--MODIFY-
                    Call gbLogMsg(msImportLogPath, _
                             "** DUPLICATE SupplierCode- " & vbCrLf & _
                                "Stock-id=" & intStock_id & ";  Supplier-id=" & intSupplier_id & "; supcode=" & sSupCode)
                Else  '--ok-
                    sSupCode2 = sSupCode  '--ok to go-
                    colUniqueKeys.Add(sSupCode, sSupCode)  '--add to unique collection.-
                End If
                posRow1.Item("supcode") = sSupCode2  '= rmRow1.Item("supcode")
                posRow1.Item("supplier_id") = rmRow1.Item("supplier_id")
                posRow1.Item("stock_id") = rmRow1.Item("stock_id")
                posRow1.Item("date_created") = rmRow1.Item("date_modified")
                posRow1.Item("date_modified") = rmRow1.Item("date_modified")
                dtPOS.Rows.Add(posRow1)
                ix += 1
                labProgress.Text = CStr(ix)
                DoEvents()
            Next rmRow1
        End If  '-Sup Code RM nothing--
        '-- now copy SupplierCode..
        labAction.Text = "Bulk copying SupplierCode Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "SupplierCode") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= SupplierCode Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Import PURCHASE ORDERS ---
        '-- Import PURCHASE ORDERS ---
        '-- Import PURCHASE ORDERS ---

        Dim colCompletedOrders As New Collection   '-- collect COMPLETED PO id's -
        Dim colOrdersReceiving As New Collection   '-- collect PO id's for Started Receiving -
        Dim colOrdersClosedForBO As New Collection   '-- collect PO id's for Closed for B/Orders -

        '-- First get RM recordset (GoodsReceived) to translate/copy..
        Call mbReport(vbCrLf & "Getting RetailM PurchaseOrder data.. " & vbCrLf & sSql)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RetailM PurchaseOrder data..")
        '--1. Get all JET RM PO records.. -> dtRetailM
        If Not mbGetRetailManagerTable("Orders", "order_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " PurchaseOrder records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RetailM PurchaseOrder records.")

        '--2. Get Empty POS GoodsReceived records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  GoodsReceived datatable--
        If Not mbGetPOStable("PurchaseOrder", "order_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '==Call mbReport("Found " & dtPOS.Rows.Count & " POS PO records.")
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating Orders Table.."
            Call mbReport("Translating RetailM Orders records to POS data table.")
            ix = 0    '--count rows.-
            '=colUniqueKeys = New Collection
            '= Dim date1 As Date
            '= Dim decEx, decTax, decInc As Decimal
            Dim intOrderId As Integer
            Dim sId As String
            For Each rmRow1 In dtRetailM.Rows
                '-  fill all PO cols this record..=
                Try
                    posRow1 = dtPOS.NewRow()
                    intOrderId = rmRow1.Item("order_id")
                    sId = Trim(CStr(intOrderId))
                    colCompletedOrders.Add(intOrderId, sId)  '--assume all completed to start with..
                    posRow1.Item("order_id") = intOrderId
                    posRow1.Item("revision") = rmRow1.Item("revision")

                    posRow1.Item("order_date") = rmRow1.Item("order_date")
                    posRow1.Item("due_date") = rmRow1.Item("due_date")
                    posRow1.Item("staff_id") = rmRow1.Item("staff_id")
                    posRow1.Item("supplier_id") = rmRow1.Item("supplier_id")
                    posRow1.Item("orderNoSuffix") = rmRow1.Item("order_suffix")
                    posRow1.Item("comments") = rmRow1.Item("comments")
                    posRow1.Item("delivery_address") = ""

                    posRow1.Item("isReceiving") = 0
                    posRow1.Item("isCompleted") = 0
                    posRow1.Item("isClosedForBackorders") = 0
                    posRow1.Item("isCancelled") = 0
                    posRow1.Item("comments") = rmRow1.Item("comments")
                    posRow1.Item("date_modified") = DateTime.Today
                    dtPOS.Rows.Add(posRow1)
                    ix += 1
                    labProgress.Text = CStr(ix)
                    DoEvents()

                Catch ex As Exception
                    s1 = "Failed to Translate  RetailM Orders row to POS.." & vbCrLf & ex.Message
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(msImportLogPath, s1 & vbCrLf & "= = = = = =" & vbCrLf)
                    cnnSqlClient.Close()
                    Exit Sub
                End Try
            Next rmRow1
        End If  '-RM nothing-

        '-  BFORE Bulk Copying PO- 
        '--    get PO Lines to separate dataTable-.--
        '--    Use  dtPOS_lines  for PO lines.--
        '-- Import PO Orders Lines ..-
        '-- Import PO Orders Lines ..-

        '-- First get RM recordset (PurchaseOrderLine) to translate/copy..
        Call mbReport(vbCrLf & "Getting RM (Purchase) OrderLine data.." & vbCrLf & sSql)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM (Purchase) OrdersLine data..")

        '--1. Get all JET RM  stock records.. -> dtRetailM
        '==  ORDER BY << order_id, stock_id >> so we can summarise received qty status-
        '==   for each PO order/stock-item..
        If Not mbGetRetailManagerTable("OrdersLine", "order_id", dtRetailM, , "stock_id") Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " OrdersLine records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM OrdersLine records.")

        '--2. Get Empty POS PurchaseOrderLine records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  PurchaseOrderLine datatable--
        If Not mbGetPOStable("PurchaseOrderLine", "line_id", dtPOS_lines) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '=Call mbReport("Found " & dtPOS.Rows.Count & " POS PurchaseOrderLine records.")
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        '--   NB- Each order item (stock item) can have multiple RM lines, according to 
        '--       How many GoodsReceived events were invoked to receive the total qty (if any)-
        '--       We need to summarise by stock_id for qty received (or back-ordered) -
        '--       And just transcribe ONE line to POS for each PO/stock-item-
        '--
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating OrdersLine Table.."
            Call mbReport("Translating RM OrdersLine records to POS data table.")
            ix = 0    '--count rows.-
            '= Dim decEx, decTax, decInc As Decimal -
            Dim intRx, intStatus, intQtyOrdered, intQtyReceived As Integer
            Dim intQtyBackOrdered, intQtyAbandoned As Integer
            Dim intPO_id, intLine_Id, intThisOrder_id, intThisStock_id, intThisQty As Integer
            Dim sId As String
            intQtyOrdered = 0 : intQtyReceived = 0
            intQtyBackOrdered = 0 : intQtyAbandoned = 0
            intThisOrder_id = -1 : intThisStock_id = -1
            For intRx = 0 To (dtRetailM.Rows.Count - 1)  '= Each rmRow1 In dtRetailM.Rows
                Try
                    rmRow1 = dtRetailM.Rows(intRx)
                    '- deal with this qty-
                    intThisOrder_id = rmRow1.Item("order_id")
                    intThisStock_id = rmRow1.Item("stock_id")
                    intStatus = CInt(rmRow1.Item("status"))
                    intThisQty = rmRow1.Item("quantity")
                    '- RECONSTRUCT original order qty-
                    If (intStatus = 0) Then
                        intQtyOrdered += intThisQty   '--should be the only line for this stock_id
                    ElseIf (intStatus < 0) Then      '- [-1] is Abandoned qty.
                        intQtyOrdered += intThisQty   '-(no B/O). May Not be the only line for this stock_id
                        intQtyAbandoned += intThisQty
                    ElseIf (intStatus = 1) Then
                        intQtyOrdered += intThisQty   '--B/Ordered amt. May Not be the only line for this stock_id
                        intQtyBackOrdered += intThisQty
                    ElseIf (intStatus = 2) Then
                        intQtyOrdered += intThisQty   '--A delivered amt. May Not be the only line for this stock_id
                        intQtyReceived += intThisQty   '--Received amt. May Not be the only line for this stock_id
                    End If  '-status-

                    '--look ahead to see if this is last row of table or -
                    '--  the last row of order/stock-id group-
                    If (intRx = (dtRetailM.Rows.Count - 1)) OrElse _
                           ((intThisOrder_id <> dtRetailM.Rows(intRx + 1).Item("order_id")) Or _
                              (intThisStock_id <> dtRetailM.Rows(intRx + 1).Item("stock_id"))) Then '-last of or last of group-

                        posRow1 = dtPOS_lines.NewRow()
                        intLine_Id = rmRow1.Item("line_id")
                        posRow1.Item("line_id") = intLine_Id
                        intPO_id = rmRow1.Item("order_id")
                        posRow1.Item("order_id") = intPO_id
                        posRow1.Item("supplier_id") = rmRow1.Item("supplier_id")
                        posRow1.Item("stock_id") = rmRow1.Item("stock_id")
                        posRow1.Item("supplierCode") = rmRow1.Item("supcode")
                        '-goods_taxCode
                        posRow1.Item("goods_taxCode") = rmRow1.Item("goods_tax")
                        posRow1.Item("cost_ex") = rmRow1.Item("cost_ex")
                        posRow1.Item("cost_inc") = rmRow1.Item("cost_inc")
                        '== intQtyOrdered = rmRow1.Item("quantity")
                        posRow1.Item("quantity") = intQtyOrdered
                        '==intQtyReceived = 0
                        '==If IsNumeric(rmRow1.Item("status")) Then
                        '== intQtyReceived = CInt(rmRow1.Item("status"))
                        '== End If
                        s1 = ""
                        If (intQtyBackOrdered <> 0) Then
                            s1 = "B/Order:" & CStr(intQtyBackOrdered)
                        ElseIf (intQtyAbandoned <> 0) Then
                            s1 = "Abandoned:" & CStr(intQtyAbandoned)
                        End If
                        posRow1.Item("status") = s1
                        posRow1.Item("qtyReceived") = intQtyReceived
                        posRow1.Item("goods_id") = rmRow1.Item("goods_id")
                        posRow1.Item("date_updated") = DateTime.Today

                        '- Update PO status-
                        sId = Trim(CStr(intPO_id))
                        If (intQtyReceived < intQtyOrdered) Then
                            '== REMOVE completed status-
                            If colCompletedOrders.Contains(sId) Then
                                colCompletedOrders.Remove(sId)
                            End If
                        End If
                        If (intQtyReceived > 0) Then  '-started receiving at least-
                            If Not colOrdersReceiving.Contains(sId) Then
                                colOrdersReceiving.Add(intPO_id, sId)
                            End If
                        End If
                        '- Flag B/orders closed if some abandoned-
                        If (intQtyAbandoned > 0) Then
                            If Not colOrdersClosedForBO.Contains(sId) Then
                                colOrdersClosedForBO.Add(intPO_id, sId)
                            End If
                        End If '-closed-

                        dtPOS_lines.Rows.Add(posRow1)
                        ix += 1
                        labProgress.Text = CStr(ix)
                        DoEvents()
                        '-- ready for next group-
                        intQtyOrdered = 0 : intQtyReceived = 0
                        intQtyBackOrdered = 0 : intQtyAbandoned = 0
                    Else '-more to come for this stock item-

                    End If  '-last-
                Catch ex As Exception
                    s1 = "Failed to Translate RM OrdersLine row to POS.." & vbCrLf & ex.Message
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(msImportLogPath, s1 & vbCrLf & "= = = = = =" & vbCrLf)
                    cnnSqlClient.Close()
                    Exit Sub
        End Try
            Next intRx '= rmRow1
        End If  '-RM nothing-

        '- NOW can Bulk Copy PO table..
        '-- now copy PO..
        labAction.Text = "Bulk copying PurchaseOrder Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "PurchaseOrder") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= PurchaseOrder Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        sSql = ""
        '-- AND NOW Fix up PO cols- isReceiving and isCompleted..
        '- colCompletedOrders shows which are completed-
        '-  colOrdersReceiving show which are receiving (even if completed)..
        '-- Make list of Completed PO nos..
        s1 = ""
        If (colCompletedOrders.Count > 0) Then
            For Each intId As Integer In colCompletedOrders
                If (s1 <> "") Then s1 &= ", "
                s1 &= CStr(intId)
            Next intId
            sSql = " UPDATE PurchaseOrder SET isCompleted=1 "
            sSql &= "   WHERE order_id in (" & s1 & "); " & vbCrLf
        End If  '-count-
        '-- Make list of Completed PO nos..
        s1 = ""
        If (colOrdersReceiving.Count > 0) Then
            For Each intId As Integer In colOrdersReceiving
                If (s1 <> "") Then s1 &= ", "
                s1 &= CStr(intId)
            Next intId
            sSql &= " UPDATE PurchaseOrder SET isReceiving=1 "
            sSql &= "   WHERE order_id in (" & s1 & "); " & vbCrLf
        End If  '-count-

        '- Update for Closed for b/o-
        s1 = ""
        If (colOrdersClosedForBO.Count > 0) Then
            For Each intId As Integer In colOrdersClosedForBO
                If (s1 <> "") Then s1 &= ", "
                s1 &= CStr(intId)
            Next intId
            sSql &= " UPDATE PurchaseOrder SET isClosedForBackorders=1 "
            sSql &= "   WHERE order_id in (" & s1 & "); " & vbCrLf
        End If  '-count-

        '- run the update-
        If (sSql <> "") Then
            If Not gbExecuteCmd(mCnnSql, sSql, intAffected, sErrorMsg) Then
                s1 = "Failed to update PO Completed Details." & vbCrLf & sErrorMsg
                Call gbLogMsg(msImportLogPath, s1 & vbCrLf)
                MsgBox(s1, MsgBoxStyle.Exclamation)
            Else  '-ok-
                s1 = "Updated OK PO Completed Details. " & vbCrLf & "- SQL was: " & vbCrLf & sSql & vbCrLf
                Call gbLogMsg(msImportLogPath, s1 & vbCrLf)
            End If '-- exec-
        End If  '--ssql-

        '-  and Bulk Copy PO LINES..
        '-  Bulk Copy PO table..
        '-- now copy PO..
        labAction.Text = "Bulk copying PurchaseOrderLine Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS_lines, "PurchaseOrderLine") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= PurchaseOrderLine Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- FINISHED Importing PURCHASE ORDERS ---


        '-- Import Goods Received Invoices..-
        '-- Import Goods Received Invoices..-
        '-- Import GoodsReceived Invoices..-
        sSql = ""
        '-- First get RM recordset (GoodsReceived) to translate/copy..
        Call mbReport(vbCrLf & "Getting RM GoodsReceived data.. " & vbCrLf & sSql)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM Goods data..")
        '--1. Get all JET RM  stock records.. -> dtRetailM
        If Not mbGetRetailManagerTable("Goods", "goods_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " Goods records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM Goods records.")

        '--2. Get Empty POS GoodsReceived records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  GoodsReceived datatable--
        If Not mbGetPOStable("GoodsReceived", "Goods_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '==Call mbReport("Found " & dtPOS.Rows.Count & " POS GoodsReceived records.")
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating GoodsReceived Table.."
            Call mbReport("Translating RM GoodsReceived records to POS data table.")
            ix = 0    '--count rows.-
            '=colUniqueKeys = New Collection
            Dim date1 As Date
            Dim decEx, decTax, decInc As Decimal
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 23 POS  GoodsReceived  columns -
                Try
                    posRow1 = dtPOS.NewRow()
                    posRow1.Item("goods_id") = rmRow1.Item("goods_id")
                    posRow1.Item("goods_date") = rmRow1.Item("goods_date")
                    posRow1.Item("staff_id") = rmRow1.Item("staff_id")
                    posRow1.Item("supplier_id") = rmRow1.Item("supplier_id")

                    posRow1.Item("invoice_no") = rmRow1.Item("invoice_no")
                    '- Some RM Invoice dates are crap--
                    If IsDate(rmRow1.Item("goods_date")) Then
                        date1 = rmRow1.Item("goods_date")
                        If (date1 < CDate("01-Jan-1900")) Then
                            date1 = CDate("01-Jan-1900")  '-make a valis date-
                        End If
                    Else
                        date1 = CDate("01-Jan-1900")  '-valid for sql-
                    End If '- ísdate-

                    posRow1.Item("invoice_date") = date1  '==rmRow1.Item("invoice_date")
                    posRow1.Item("orderNoSuffix") = VB.Left(rmRow1.Item("order_no"), 15)
                    posRow1.Item("order_id") = rmRow1.Item("order_id")

                    posRow1.Item("subtotal_ex") = rmRow1.Item("subtotal_ex")
                    posRow1.Item("subtotal_tax") = _
                         CDec(rmRow1.Item("subtotal_inc")) - CDec(rmRow1.Item("subtotal_ex"))
                    posRow1.Item("subtotal_inc") = rmRow1.Item("subtotal_inc")

                    decEx = CDec(rmRow1.Item("freight_ex"))
                    decInc = rmRow1.Item("freight_inc")
                    decTax = decInc - decEx
                    posRow1.Item("freight_ex") = decEx  '=rmRow1.Item("freight_ex")
                    posRow1.Item("freight_taxCode") = rmRow1.Item("freight_tax")
                    posRow1.Item("freight_tax") = decTax  '=rmRow1.Item("freight_tax")
                    posRow1.Item("freight_inc") = decInc
                    '-- Some guesswork here..--
                    If (decTax > 0) And (decEx > 0) Then
                        posRow1.Item("freight_taxPercentage") = (decTax / decEx) * 100
                    Else '-notax  --
                        posRow1.Item("freight_taxPercentage") = 0
                    End If

                    posRow1.Item("discount_nett") = 0
                    posRow1.Item("discount_tax") = 0
                    posRow1.Item("total_ex") = rmRow1.Item("total_ex")
                    posRow1.Item("total_tax") = CDec(rmRow1.Item("total_inc")) - CDec(rmRow1.Item("total_ex"))
                    posRow1.Item("total_inc") = rmRow1.Item("total_inc")
                    posRow1.Item("total_expected") = rmRow1.Item("expected")

                    posRow1.Item("comments") = rmRow1.Item("comments")
                    dtPOS.Rows.Add(posRow1)
                    ix += 1
                    labProgress.Text = CStr(ix)
                    DoEvents()
                Catch ex As Exception
                    s1 = "Failed to Translate RM Goods row to POS.." & vbCrLf & ex.Message
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(msImportLogPath, s1 & vbCrLf & "= = = = = =" & vbCrLf)
                    cnnSqlClient.Close()
                    Exit Sub
                End Try
            Next rmRow1
        End If '--GR nothing.-
        '-- now copy Goods..
        labAction.Text = "Bulk copying Goods Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "GoodsReceived") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= GoodsReceived Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- Import GoodsReceived Line (Invoices Lines)..-
        '-- Import GoodsReceived Line (Invoices Lines)..-
        '-- Import GoodsReceived Line (Invoices Lines)..-

        '-- First get RM recordset (GoodsLine) to translate/copy..
        Call mbReport(vbCrLf & "Getting RM GoodsLine data.." & vbCrLf & sSql)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM GoodsLine data..")
        '--1. Get all JET RM  stock records.. -> dtRetailM
        If Not mbGetRetailManagerTable("GoodsLine", "line_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " GoodsLine records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM GoodsLine records.")

        '--2. Get Empty POS GoodsReceivedLine records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  GoodsReceivedLine datatable--
        If Not mbGetPOStable("GoodsReceivedLine", "line_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '=Call mbReport("Found " & dtPOS.Rows.Count & " POS GoodsReceivedLine records.")
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating GoodsLine Table.."
            Call mbReport("Translating RM GoodsLine records to POS data table.")
            ix = 0    '--count rows.-
            Dim decEx, decTax, decInc As Decimal
            Dim intQty As Integer
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 13 POS  GoodsReceivedLine  columns -
                Try
                    posRow1 = dtPOS.NewRow()

                    posRow1.Item("line_id") = rmRow1.Item("line_id")
                    posRow1.Item("goods_id") = rmRow1.Item("goods_id")
                    posRow1.Item("stock_id") = rmRow1.Item("stock_id")
                    posRow1.Item("goods_taxCode") = rmRow1.Item("goods_tax")
                    decEx = rmRow1.Item("cost_ex")
                    posRow1.Item("cost_ex") = decEx
                    decTax = CDec(rmRow1.Item("cost_inc")) - CDec(rmRow1.Item("cost_ex"))
                    posRow1.Item("cost_tax") = decTax
                    decInc = rmRow1.Item("cost_inc")
                    posRow1.Item("cost_inc") = decInc

                    If (decEx > 0) Then
                        posRow1.Item("goods_taxPercentage") = ((decTax / decEx) * 100)
                    Else
                        posRow1.Item("goods_taxPercentage") = 0
                    End If
                    '==posRow1.Item("goods_taxPercentage") = IIf((decEx > 0), ((decTax / decEx) * 100), 0)
                    intQty = CInt(rmRow1.Item("quantity"))
                    posRow1.Item("quantity") = intQty
                    posRow1.Item("sell_ex") = CInt(rmRow1.Item("sell"))

                    posRow1.Item("total_ex") = decEx * intQty
                    posRow1.Item("total_tax") = decTax * intQty
                    posRow1.Item("total_inc") = decInc * intQty
                    dtPOS.Rows.Add(posRow1)
                    ix += 1
                    labProgress.Text = CStr(ix)
                    DoEvents()
                Catch ex As Exception
                    s1 = "Failed to Translate RM GoodsLine row to POS.." & vbCrLf & ex.Message
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(msImportLogPath, s1 & vbCrLf & "= = = = = =" & vbCrLf)
                    cnnSqlClient.Close()
                    Exit Sub
                End Try  '--goods line-
            Next rmRow1
        End If  '--goods Line is nothing-
        '-- now copy Goods..
        labAction.Text = "Bulk copying GoodsLine Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "GoodsReceivedLine") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= GoodsReceivedLine Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '-- NOW to Import all SERIALS known to RM. --

        '-- Start with SerialAuditTrail transaction Collection..-
        '--  Finds all serialAuditTrail transactions,
        '--     and collects serials/numbers from Joined SerialAudit Table..
        '--  generally, serials got into the system via GoodsRcvd, Refunds or SI..

        '==  grh. 3311.307- 07Mar2016- 
        '==    >>- Importing SerialAuditTrail- Import RM Sales-Cust Info also..
        '==
        '--  FIRST.  Just Import GR from initial RM Trail dataTable..
        '---  THEN-  Import Sales Trail records
        '-   from special Trail dataTable with Joined Docket and Customer..

        '-- First get RM recordset of ALL Trail records to translate/copy..
        sSql = " SELECT trail_id, stock_id, SerialAuditTrail.serialaudit_id, type, type_id, type_line_id, "
        sSql &= " original_id, trail_date, movement, "
        sSql &= "  SerialAudit.number AS SerialNo, SerialAudit.warranty_date, SerialAudit.date_created  "
        sSql &= "  FROM SerialAuditTrail "
        sSql &= "  LEFT OUTER JOIN SerialAudit "
        sSql &= "  ON (SerialAuditTrail.SerialAudit_Id=SerialAudit.SerialAudit_Id) "
        sSql &= "  ORDER BY SerialAuditTrail.serialaudit_id, trail_date; "
        '== sSql = sSql & "       WHERE  ((SerialAuditTrail.Type='GR') OR (SerialAuditTrail.Type='SI')) "

        Call mbReport(vbCrLf & "Getting RM SerialAudit-SerialAuditTrail data..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting SerialAudit-RM SerialAuditTrail data..")
        '--1. Get all JET RM  SerialAuditTrail-JOIN-SerialAudit records.. -> dtRetailM
        If Not mbGetRetailManagerTable("", "", dtRetailM, sSql) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " SerialAudit--SerialAuditTrail records." & vbCrLf)
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM SerialAuditTrail records.")

        '-- AND get a datatable of All SALES Trail records..
        Dim dtRetailM_Sales As DataTable
    
        sSql = " SELECT trail_id, stock_id, SerialAuditTrail.serialaudit_id, "
        sSql &= " SerialAuditTrail.type, type_id, type_line_id, "
        sSql &= " SerialAuditTrail.original_id, SerialAuditTrail.trail_date, movement, "
        sSql &= "  customer.barcode, customer.company,   "
        sSql &= "  (customer.given_names + ' ' + customer.surname) AS customer_name "
        sSql &= "  FROM SerialAuditTrail "
        sSql &= "  LEFT OUTER JOIN (docket "
        sSql &= "      LEFT OUTER JOIN customer "
        sSql &= "         ON (docket.customer_id=customer.customer_id))  "
        sSql &= "    ON (SerialAuditTrail.type_id=docket.docket_id) "
        sSql &= "  WHERE  ((SerialAuditTrail.Type='SA') OR (SerialAuditTrail.Type='IV')) "
        sSql &= "    ORDER BY SerialAuditTrail.serialaudit_id, trail_date; "
 
        Call mbReport(vbCrLf & "Getting RM SerialAuditTrail SALES data..")
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM SerialAuditTrail SALES data..")
        '--1. Get all JET RM  SerialAuditTrail-JOIN-SerialAudit records.. -> dtRetailM
        If Not mbGetRetailManagerTable("", "", dtRetailM_Sales, sSql) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM_Sales.Rows.Count & " SerialAuditTrail SALES records." & vbCrLf)
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM_Sales.Rows.Count & " RM SerialAuditTrail SALES records.")

        '-- Extract all serial audit records and Copy into SerialAudit Table-.
        '--  Then Extract all Trail (Non-sales) records and copy into POS SerialAuditTrail table.
        '--   AND Then Extract all Trail (SALES) records and copy into POS SerialAuditTrail table.

        '--2a. Get Empty POS SerialAudit records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  SerialAudit datatable--
        If Not mbGetPOStable("SerialAudit", "serial_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '- testing..
        '--2b.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            ix = 0    '--count rows.-
            Dim intSerial_id As Integer
            Dim bIsInStock As Boolean
            Dim sStatus, sTrailType As String
            Dim dateLastTrail As Date
            Dim intMovement As Integer
            intLast_id = 0
            '-- TESTING_ show all trail records..
            '= Call mbReport(vbCrLf & "== Found  " & dtRetailM.Rows.Count & " trail records: ")
            labAction.Text = "Extracting SerialAudit Records.."
            Call mbReport("Translating RM SerialAudit records to POS data table.")
            posRow1 = Nothing  '--must get a group first-
            For Each rmRow1 In dtRetailM.Rows
                '== Call mbReport("Id: " & rmRow1.Item("serialaudit_id") & "S/n: " & rmRow1.Item("serialNo") & _
                '=                   " Type: " & rmRow1.Item("type") & _
                '==                    " Date: " & Format(rmRow1.Item("trail_date"), "dd-MMM-yyyy") & " =")
                If (Trim(rmRow1.Item("serialNo")) <> "") Then  '-not blank- can have-
                    '=posRow1.Item("serial_id") = rmRow1.Item("serialAudit_id")

                    '--pick out the first of every serialAudit_id Trail Group-
                    '-- to insert the serialAudit record..
                    intSerial_id = rmRow1.Item("serialAudit_id")
                    bIsInStock = False
                    '== sStatus = "unknown"
                    If (intSerial_id > intLast_id) Then '-new- add last serial-
                        If Not (posRow1 Is Nothing) Then  '-have started-
                            posRow1.Item("isInStock") = bIsInStock
                            posRow1.Item("status") = sStatus
                            posRow1.Item("date_modified") = dateLastTrail  '= rmRow1.Item("Stock_id")
                            dtPOS.Rows.Add(posRow1)
                        End If
                        '- get details from first of group-
                        posRow1 = dtPOS.NewRow()
                        posRow1.Item("serial_id") = intSerial_id
                        posRow1.Item("serialNumber") = rmRow1.Item("serialNo")
                        posRow1.Item("stock_id") = rmRow1.Item("Stock_id")

                        posRow1.Item("warranty_date") = rmRow1.Item("warranty_date")
                        posRow1.Item("date_created") = rmRow1.Item("date_created")
                        '-- save date created in case trail-date null-
                        If Not IsDBNull(rmRow1.Item("date_created")) Then
                            dateLastTrail = rmRow1.Item("date_created")
                        Else
                            dateLastTrail = Today  '--instead of NULL-
                        End If
                    End If  '--new- add serial
                    sTrailType = UCase(rmRow1.Item("type"))  '-save-
                    If Not IsDBNull(rmRow1.Item("trail_date")) Then
                        dateLastTrail = rmRow1.Item("trail_date")  '-save-
                    End If
                    intMovement = rmRow1.Item("movement")       '--save as last-
                    '-- Work out if InStock and status as trail goes by..
                    If (sTrailType = "GR") Or (sTrailType = "SI") Or _
                           (((sTrailType = "SA") Or (sTrailType = "IV")) And (intMovement < 0)) Then '-received-
                        '- Goods in or Refund..-
                        bIsInStock = True
                        sStatus = "instock"
                    Else
                        bIsInStock = False
                        sStatus = "sold"
                        If (sTrailType = "RG") Then
                            sStatus = "returnedGoods"
                        End If '-rg-
                    End If  '-received-
                End If '-blank serial-
                intLast_id = intSerial_id  '--update last seen.- 
                ix += 1
                labProgress.Text = CStr(ix)
                DoEvents()
            Next rmRow1
            '-- add last row that we saved --
            If Not (posRow1 Is Nothing) Then  '-have started-
                posRow1.Item("isInStock") = bIsInStock
                posRow1.Item("status") = sStatus
                posRow1.Item("date_modified") = dateLastTrail  '= rmRow1.Item("Stock_id")
                dtPOS.Rows.Add(posRow1)
            End If
            '- ok- BulkCopy serials to table-
            '-- now copy SerialAudit..
            labAction.Text = "Bulk copying SerialAudit Table.."
            If Not mbDoBulkCopy(bulkCopy1, dtPOS, "SerialAudit") Then
                cnnSqlClient.Close()
                Exit Sub
            End If  '-copy-
            labAction.Text = "Done.."
            Call gbLogMsg(msImportLogPath, "== Done Importing SerialAudit records.." & vbCrLf & "= = = = = =" & vbCrLf)
            Call mbReport("= Done Importing SerialAudit records..." & vbCrLf)

            '-- Now add all trail records-
            '--
            '--2c. Get Empty POS SerialAuditTRail records -> dtPOS  (to structure dtPOS)-
            '-- Make POS-type  SerialAuditTrail datatable--
            If Not mbGetPOStable("SerialAuditTrail", "serialAudit_id", dtPOS) Then
                cnnSqlClient.Close()
                Exit Sub
            End If
            Dim intType_id, intType_line_id As Integer
            Dim sDetail As String

            '-- FIRST pick out and BULK COPY all NON-Sales trail records-

            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            ix = 0    '--count rows.-
            '= Call mbReport(vbCrLf & "== End of trail records == ")
            labAction.Text = "Translating SerialAuditTrail Table (GR trails).."
            Call mbReport(vbCrLf & "Translating RM SerialAuditTrail GR records to POS data table.")

            For Each rmRow1 In dtRetailM.Rows
                If (Trim(rmRow1.Item("serialNo")) <> "") And _
                            (Not IsDBNull(rmRow1.Item("trail_date"))) And _
                                 (UCase(rmRow1.Item("type")) <> "SA") And _
                                            (UCase(rmRow1.Item("type")) <> "IV") Then  '-not blank- can have- 
                    posRow1 = dtPOS.NewRow()
                    posRow1.Item("trail_id") = rmRow1.Item("trail_id")
                    posRow1.Item("stock_id") = rmRow1.Item("stock_id")
                    posRow1.Item("serialAudit_id") = rmRow1.Item("serialAudit_id")
                    posRow1.Item("original_id") = rmRow1.Item("original_id")

                    sTrailType = UCase(rmRow1.Item("type"))  '-save-
                    intType_id = UCase(rmRow1.Item("type_id"))  '-save-
                    intType_line_id = UCase(rmRow1.Item("type_line_id"))  '-save-
                    intMovement = rmRow1.Item("movement")

                    posRow1.Item("tran_type") = rmRow1.Item("type")
                    posRow1.Item("type_id") = rmRow1.Item("type_id")
                    posRow1.Item("type_line_id") = rmRow1.Item("type_line_id")
                    posRow1.Item("movement") = rmRow1.Item("movement")
                    posRow1.Item("trail_date") = rmRow1.Item("trail_date")  '-save- 

                    '-- compile RM trans-detail text..
                    sDetail = "RM Trans.Type=" & sTrailType & ";  "
                    If (sTrailType = "GR") Then
                        sDetail &= "GoodsReceived Inv #" & intType_id & "."
                        '-- we imported Goods, so we have these references
                        posRow1.Item("type_id") = rmRow1.Item("type_id")
                        posRow1.Item("type_line_id") = rmRow1.Item("type_line_id")
                    Else '-not Goods Rcvd.
                        '--RM Sales references not valid in POS
                        posRow1.Item("type_id") = -1       '= rmRow1.Item("type_id")
                        posRow1.Item("type_line_id") = -1  '= rmRow1.Item("type_line_id")
                        If (sTrailType = "SI") Then
                            sDetail &= " Input via StockInfo. Orig.RM ID: " & rmRow1.Item("original_id") & "."
                        ElseIf (sTrailType = "SA") Or (sTrailType = "IV") Then
                            '-- Sale or refund..-
                            '-Non-sales only= If (intMovement >= 0) Then
                            '-Non-sales only= sDetail &= "Sale- "
                            '-Non-sales only= Else
                            '-Non-sales only=   sDetail &= "Refund- "
                            '-Non-sales only= End If
                            '-Non-sales only= sDetail &= "RM Invoice No: " & intType_id & "."
                        ElseIf (sTrailType = "RG") Then
                            sDetail &= " Returned Goods- ID=" & intType_id & "."
                        End If
                    End If '-trail type-
                    '- is_RM_transaction
                    posRow1.Item("is_RM_transaction") = 1
                    posRow1.Item("RM_tr_detail") = sDetail
                    dtPOS.Rows.Add(posRow1)
                End If  '-blank
                ix += 1
                labProgress.Text = CStr(ix)
                DoEvents()
            Next rmRow1
            '- ok- BulkCopy serials to table-
            '-- now copy SerialAuditTrail..
            labAction.Text = "Bulk copying SerialAuditTrail (Non-sales) Table.."
            If Not mbDoBulkCopy(bulkCopy1, dtPOS, "SerialAuditTrail") Then
                cnnSqlClient.Close()
                Exit Sub
            End If  '-copy-
            labAction.Text = "Done.."
            Call gbLogMsg(msImportLogPath, "= SerialAuditTrail (non-sales) Import done." & vbCrLf & "= = = = = =" & vbCrLf)

            '-- NOW all Sales Trails.
            '-- NOW all Sales Trails.
            '-- Empty the POS table so as not to duplicate all the GR..
            dtPOS.Rows.Clear()

            If (Not (dtRetailM_Sales Is Nothing)) AndAlso (dtRetailM_Sales.Rows.Count > 0) Then
                labTargetCount.Text = CStr(dtRetailM_Sales.Rows.Count)
                labProgress.Text = "0"
                ix = 0    '--count rows.-
                '= Call mbReport(vbCrLf & "== End of trail records == ")
                labAction.Text = "Translating SerialAuditTrail Table (SA and IV trails).."
                Call mbReport(vbCrLf & "Translating RM SerialAuditTrail SALES records to POS data table.")

                For Each rmRow1 In dtRetailM_Sales.Rows
                    If (Not IsDBNull(rmRow1.Item("trail_date"))) And _
                                ((UCase(rmRow1.Item("type")) = "SA") Or _
                                       (UCase(rmRow1.Item("type")) = "IV")) Then  '-not blank- can have- 
                        posRow1 = dtPOS.NewRow()
                        posRow1.Item("trail_id") = rmRow1.Item("trail_id")
                        posRow1.Item("stock_id") = rmRow1.Item("stock_id")
                        posRow1.Item("serialAudit_id") = rmRow1.Item("serialAudit_id")
                        posRow1.Item("original_id") = rmRow1.Item("original_id")

                        sTrailType = UCase(rmRow1.Item("type"))  '-save-
                        intType_id = UCase(rmRow1.Item("type_id"))  '-save-
                        intType_line_id = UCase(rmRow1.Item("type_line_id"))  '-save-
                        intMovement = rmRow1.Item("movement")

                        posRow1.Item("tran_type") = rmRow1.Item("type")
                        '=posRow1.Item("type_id") = rmRow1.Item("type_id")
                        '=posRow1.Item("type_line_id") = rmRow1.Item("type_line_id")
                        '--RM Sales references not valid in POS
                        posRow1.Item("type_id") = -1       '= rmRow1.Item("type_id")
                        posRow1.Item("type_line_id") = -1  '= rmRow1.Item("type_line_id")
                        posRow1.Item("movement") = rmRow1.Item("movement")
                        posRow1.Item("trail_date") = rmRow1.Item("trail_date")  '-save- 

                        '-- compile RM trans-detail text..
                        sDetail = "RM_trans_type=" & sTrailType
                        If (intMovement >= 0) Then
                            sDetail &= " (Sale); "
                        Else
                            sDetail &= " (Refund); "
                        End If
                        '=Sales only= If (sTrailType = "GR") Then
                        '=Sales only= sDetail &= "GoodsReceived Inv #" & intType_id & "."
                        '=Sales only= '-- we imported Goods, so we have these references
                        '=Sales only= posRow1.Item("type_id") = rmRow1.Item("type_id")
                        '=Sales only= posRow1.Item("type_line_id") = rmRow1.Item("type_line_id")
                        '=Sales only= Else '-not Goods Rcvd.
                         '=Sales only= If (sTrailType = "SI") Then
                        '=Sales only= sDetail &= " Input via StockInfo. Orig.RM ID: " & rmRow1.Item("original_id") & "."
                        '=Sales only= ElseIf (sTrailType = "SA") Or (sTrailType = "IV") Then
                        '-- Sale or refund..-
                        sDetail &= "RM_sale_invoice_no=" & intType_id & "; " & _
                                   "Customer_barcode=" & rmRow1.Item("barcode") & "; " & _
                                   "Customer_company=" & rmRow1.Item("company") & "; " & _
                                     "Customer_name=" & rmRow1.Item("customer_name")
                        '=Sales only= ElseIf (sTrailType = "RG") Then
                        '=Sales only= sDetail &= " Returned Goods- ID=" & intType_id & "."
                        '=Sales only= End If
                        '=Sales only= End If '-trail type-
                        '- is_RM_transaction
                        posRow1.Item("is_RM_transaction") = 1
                        posRow1.Item("RM_tr_detail") = sDetail
                        dtPOS.Rows.Add(posRow1)
                    End If  '-blank
                    ix += 1
                    labProgress.Text = CStr(ix)
                    DoEvents()
                Next rmRow1
                '- ok- BulkCopy to table-
                '-- now copy SerialAuditTrail Sales (Adds to table)..
                labAction.Text = "Bulk copying SerialAuditTrail (SALES) Table.."
                If Not mbDoBulkCopy(bulkCopy1, dtPOS, "SerialAuditTrail") Then
                    cnnSqlClient.Close()
                    Exit Sub
                End If  '-copy-
                labAction.Text = "Done.."
                Call gbLogMsg(msImportLogPath, "= SerialAuditTrail (non-sales) Import done." & vbCrLf & "= = = = = =" & vbCrLf)

            End If  '--RM sales nothing-

        End If '-RM-nothing-

        '- Import Quotes --
        '- Import Quotes --
        '==sSql = "SELECT * FROM dbo.SalesOrder WHERE (transaction='QU') ORDER by SalesOrder_id; "

        '-- IMPORT ALL Sales Orders, (QU and SO) because we can't pick out QU DalesOrderLines..
        '-- IMPORT ALL Sales Orders, (QU and SO) because we can't pick out QU DalesOrderLines..

        '-- First get RM recordset (SalesOrder) to translate/copy..
        Call mbReport(vbCrLf & "Getting RM Quotes (SalesOrder) data.. " & vbCrLf)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM SalesOrder data..")
        '--1. Get all JET RM  SalesOrder records.. -> dtRetailM
        If Not mbGetRetailManagerTable("SalesOrder", "SalesOrder_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " SalesOrder QU/SO records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM SalesOrder records.")

        '--2. Get Empty POS SalesOrder records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  SalesOrder datatable--
        If Not mbGetPOStable("SalesOrder", "SalesOrder_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating SalesOrder Table.."
            Call mbReport("Translating RM SalesOrder records to POS data table.")
            ix = 0    '--count rows.-
            Dim decEx, decTax, decInc As Decimal
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 15 POS Quote (S/O) tablecolumns -
                Try
                    posRow1 = dtPOS.NewRow()
                    posRow1.Item("SalesOrder_id") = rmRow1.Item("SalesOrder_id")
                    posRow1.Item("SalesOrder_date") = rmRow1.Item("SalesOrder_date")
                    posRow1.Item("staff_id") = rmRow1.Item("staff_id")
                    posRow1.Item("customer_id") = rmRow1.Item("customer_id")
                    If UCase(rmRow1.Item("transaction") = "QU") Then
                        posRow1.Item("TransactionType") = "Quote"
                    Else
                        posRow1.Item("TransactionType") = rmRow1.Item("transaction") '-- eg SO..
                    End If
                    posRow1.Item("subtotal_tax") = 0  '
                    posRow1.Item("subtotal_inc") = rmRow1.Item("subtotal")
                    posRow1.Item("discount_nett") = rmRow1.Item("discount")
                    posRow1.Item("discount_tax") = 0
                    posRow1.Item("rounding") = rmRow1.Item("rounding")
                    decEx = rmRow1.Item("total_ex")
                    decInc = rmRow1.Item("total_inc")
                    decTax = decInc - decEx
                    posRow1.Item("total_ex") = rmRow1.Item("total_ex")
                    posRow1.Item("total_tax") = decTax
                    posRow1.Item("total_inc") = rmRow1.Item("total_inc")

                    posRow1.Item("deliveryInstructions") = ""
                    posRow1.Item("comments") = rmRow1.Item("comments")
                    dtPOS.Rows.Add(posRow1)
                    ix += 1
                    labProgress.Text = CStr(ix)
                    DoEvents()
                Catch ex As Exception
                    s1 = "Failed to Translate RM SalesOrder row to POS.." & vbCrLf & ex.Message
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(msImportLogPath, s1 & vbCrLf & "= = = = = =" & vbCrLf)
                    cnnSqlClient.Close()
                    Exit Sub
                End Try
            Next rmRow1
        End If  '-- rm nothing-
        '-- now copy SalesOrder..
        labAction.Text = "Bulk copying SalesOrder Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "SalesOrder") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= SalesOrder Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)

        '- More on-Importing Quotes --
        '--  SalesOrder Line-
        '-- First get RM recordset (SalesOrderLine) to translate/copy..
        Call mbReport(vbCrLf & "Getting RM Quotes (SalesOrderLine) data.. " & vbCrLf)
        Call gbLogMsg(msImportLogPath, vbCrLf & "Getting RM SalesOrderLine data..")
        '--1. Get all JET RM  SalesOrder records.. -> dtRetailM
        If Not mbGetRetailManagerTable("SalesOrderLine", "SalesOrder_id", dtRetailM) Then
            cnnSqlClient.Close()
            Exit Sub
        End If '--get
        '- testing..
        Call mbReport("Found " & dtRetailM.Rows.Count & " SalesOrderLine records.")
        Call gbLogMsg(msImportLogPath, "Found " & dtRetailM.Rows.Count & " RM SalesOrderLine records.")

        '--2. Get Empty POS SalesOrderLine records -> dtPOS  (to structure dtPOS)-
        '-- Make POS-type  SalesOrderLine datatable--
        If Not mbGetPOStable("SalesOrderLine", "SalesOrder_id", dtPOS) Then
            cnnSqlClient.Close()
            Exit Sub
        End If
        '--3.  Translate dtRetailM rows (if any) data into dtPOS rows.
        If (Not (dtRetailM Is Nothing)) AndAlso (dtRetailM.Rows.Count > 0) Then
            labTargetCount.Text = CStr(dtRetailM.Rows.Count)
            labProgress.Text = "0"
            labAction.Text = "Translating SalesOrderLine Table.."
            Call mbReport("Translating RM SalesOrderLine records to POS data table.")
            ix = 0    '--count rows.-
            Dim decEx, decTax, decInc As Decimal
            Dim intQuantity As Integer
            For Each rmRow1 In dtRetailM.Rows
                '-- Fill all 17 POS QuoteLine (S/O) tablecolumns -
                Try
                    posRow1 = dtPOS.NewRow()
                    posRow1.Item("line_id") = rmRow1.Item("line_id")
                    posRow1.Item("SalesOrder_id") = rmRow1.Item("SalesOrder_id")
                    posRow1.Item("stock_id") = rmRow1.Item("stock_id")
                    posRow1.Item("description") = ""

                    posRow1.Item("cost_ex") = rmRow1.Item("cost_ex")
                    posRow1.Item("cost_inc") = rmRow1.Item("cost_inc")
                    posRow1.Item("sell_ex") = rmRow1.Item("sell_ex")
                    posRow1.Item("sales_taxCode") = rmRow1.Item("sales_tax")
                    posRow1.Item("sales_taxPercentage") = 10
                    posRow1.Item("sell_inc") = rmRow1.Item("sell_inc")
                    decEx = rmRow1.Item("print_ex")
                    decInc = rmRow1.Item("print_inc")
                    decTax = decInc - decEx
                    posRow1.Item("sellActual_ex") = decEx  '= rmRow1.Item("print_ex")
                    posRow1.Item("sellActual_tax") = decTax
                    posRow1.Item("sellActual_inc") = decInc  '= rmRow1.Item("print_inc")
                    intQuantity = CInt(rmRow1.Item("quantity"))
                    posRow1.Item("quantity") = intQuantity
                    '-- we manufacture these..
                    posRow1.Item("total_ex") = decEx * intQuantity
                    posRow1.Item("total_tax") = decTax * intQuantity
                    posRow1.Item("total_inc") = decInc * intQuantity
                    dtPOS.Rows.Add(posRow1)
                    ix += 1
                    labProgress.Text = CStr(ix)
                    DoEvents()
                Catch ex As Exception
                    s1 = "Failed to Translate RM SalesOrderLine row to POS.." & vbCrLf & ex.Message
                    MsgBox(s1, MsgBoxStyle.Exclamation)
                    Call gbLogMsg(msImportLogPath, s1 & vbCrLf & "= = = = = =" & vbCrLf)
                    cnnSqlClient.Close()
                    Exit Sub
                End Try
            Next rmRow1
        End If '--RM nothing-
        '-- now copy SalesOrder..
        labAction.Text = "Bulk copying SalesOrderLine Table.."
        If Not mbDoBulkCopy(bulkCopy1, dtPOS, "SalesOrderLine") Then
            cnnSqlClient.Close()
            Exit Sub
        End If  '-copy-
        labAction.Text = "Done.."
        Call gbLogMsg(msImportLogPath, "= SalesOrderLine Table Import done." & vbCrLf & "= = = = = =" & vbCrLf)


        '-- all done.--
        cnnSqlClient.Close()
        mCnnJet.Close()
        mCnnSql.Close()
        mRetailHost1.closeConnection()
        Call mbReport("= JobMatixPOS-Import- All done.." & vbCrLf & _
                                                "= = = = = = = = = = = = = =" & vbCrLf)

        Call gbLogMsg(msImportLogPath, "= JobMatixPOS-Import- All done.." & vbCrLf & _
                                                "= = = = = = = = = = = = = =" & vbCrLf & vbCrLf)

        MsgBox("= JobMatixPOS- MYOB RM Import- All done.." & vbCrLf & _
                 "Log file is at: " & vbCrLf & msImportLogPath, MsgBoxStyle.Information)
        '--all good..--
        mbcancelled = False
        btnCancel.Text = "Close"
        '==Me.Hide()
        Exit Sub

    End Sub '-start import-
    '= = = = = = = = = = = = =  =
    '-===FF->

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
        Exit Sub
    End Sub '-cancel-
    '= = = = = = = = = = ==

End Class '-import-
'= = = = = = = = = = = == 
'== end form ==