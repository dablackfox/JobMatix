Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb
Imports clsRAsMain33

Public Class frmRAs35Main


    '== grh 24-Jan-2017==

    '== This is the Main Form for the Open Source JobMatix. RAs.

    ' Copyright 2021 grhaas@outlook.com

    'Licensed under the Apache License, Version 2.0 (the "License");
    'you may Not use this file except In compliance With the License.
    'You may obtain a copy Of the License at

    '    http://www.apache.org/licenses/LICENSE-2.0

    'Unless required by applicable law Or agreed To In writing, software
    'distributed under the License Is distributed On an "AS IS" BASIS,
    'WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
    'See the License For the specific language governing permissions And
    'limitations under the License.

    '= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 


    '== Newly re-created RAsMain Form to recreate RAs as separate EXE..
    '-- Re-constituted MAIN from from JobMatix clsRAsMain. --

    '-- HISTORY-
    '==
    '==   grh- 3311.420=
    '==         >>   Exit Do (print loop) if print failed-
    '==         >>  make sure a printer is sekected if no previous..
    '==
    '==  -- 3327.0119- 19-Jan-2017-
    '==         >>-- Fixes ERROR in clsRAsMain33- SQL error when NO RAs on file. (empty list)..-- 
    '==         >>-- Fix Package Supplier Browse bug when no RA's..-- 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '== grh 24-Jan-2017==
    '== 
    '== Newly re-created RAsMain Form to recreate RAs as separate EXE..
    '-- Re-constituted MAIN from from JobMatix clsRAsMain. --
    '==
    '== grh 31-Jan-2017==
    '== 
    '== REVERSE Rehash Newly re-created RAsMain Form to recreate clsRAsMain RAs as separate DLL..
    '--    ( still Re-constituted MAIN from from JobMatix clsRAsMain. --)
    '==
    '==
    '== RAS34-EXE Cloned from POS340ex..
    '==   3411.0109  18Jan2018= 
    ''==   3411.012  22Jan2018=  Updates plus Icon
    '==
    '==
    '==  New version to go with JobMatix 35..
    '==
    '==   3501.0808  08-August-2018= 
    '==   -- Change Activated to Shown.....
    '==
    '==   4219.1124  24-Nov-2019= 
    '==   --  Import NEW dll JMxRetalHost FROM JobTracking......
    '==
    '==
    '= = = = = = = = == = = = = = = == = = = = = = = = ='= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (26-June-2021)
    '==   Target-New-Build-6201 --  (26-June-2021)
    '==   Target-New-Build-6201 --  (26-June-2021)
    '==
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 



    '-INPORT-

    'Const K_STATUS_GOODSSENT As String = "50-GoodsSentToSupplier"

    ''--  STUFF for TreeView Backcolour..--= = = = = = = = =
    ''--  STUFF for TreeView Backcolour..--= = = = = = = ==
    'Private Declare Function SendMessage Lib "user32" _
    '                      Alias "SendMessageA" (ByVal hwnd As Integer, _
    '                                            ByVal wMsg As Integer, _
    '                                             ByVal wParam As Integer, _
    '                                                 ByVal lParam As Integer) As Integer

    'Private Declare Function InvalidateRect Lib "user32" (ByVal hwnd As Integer, _
    '                                                     ByVal lpRect As Integer, _
    '                                                      ByVal bErase As Integer) As Integer

    'Private Declare Function UpdateWindow Lib "user32" (ByVal hwnd As Integer) As Integer

    'Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, _
    '                                                                           ByVal nIndex As Integer) As Integer

    'Private Declare Function SetWindowLong Lib "user32" _
    '                                         Alias "SetWindowLongA" (ByVal hwnd As Integer, _
    '                                                               ByVal nIndex As Integer, _
    '                                                                ByVal dwNewLong As Integer) As Integer

    'Private Const GWL_STYLE As Short = -16
    'Private Const TVM_SETBKCOLOR As Short = 4381
    'Private Const TVM_GETBKCOLOR As Short = 4383
    'Private Const TVS_HASLINES As Short = 2
    ''-- redraw--
    'Private Const WM_SETREDRAW As Integer = &HBS
    ''= = = = = = = = = = = = = = = = = = = = ==  =

    'Private Const K_SAVESETTINGSPATH As String = "localRAsSettings.txt"
    'Private Const k_RA_PrtSettingKey As String = "RA_PRTCOLOUR"


    'Const K_FINDACTIVEBG As Integer = &HC0FFFF
    'Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    'Const K_RTFLINEBREAK As String = "\par "
    'Const k_MAXPRINTCOLS As Short = 12 '--max columns we can print from grid..-
    ''= = = = = = = = =  = = = =
    Private mbIsInitialising As Boolean = True

    '-end import-

    Private mbActivated As Boolean = False
    Private mbStartupDone As Boolean = False

    '= Private mColQuote As Collection
    '= Private mColQuoteLines As Collection '--of collections (lines..)==

    Private mCnnSql As OleDbConnection '== ADODB.Connection
    '== Private mCnnShape As ADODB.Connection

    Private msServer As String
    Private msSqlVersion As String
    Private msInputDBNameJobs As String

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  jobs DB info--

    Dim msComputerName As String '--local machine--
    Dim msAppPath As String
    '====Private msSaveJetPath As String
    Private msInstallPath As String
    Private msJobMatixVersion As String = "JobMatix"

    '-- Results box position..-
    '= Private mlResultsTop As Integer = 84
    '= Private mlResultsLeft As Integer = 8

    Private mlFormDesignHeight As Integer = 760  '-- starting dimensions..-
    Private mlFormDesignWidth As Integer = 998   '-- starting dimensions..-

    '==IMPORT+ vars-

    'Private mLngSelectedRow As Integer '--selected browse row..-
    'Private mLngSelectedRowRAs As Integer '--selected browse row..-
    'Private mLngSelectedRowSupplier As Integer

    'Private mLngRAsTreeBGColour As Integer
    'Private mLngGridBGColourRAs As Integer

    Private msSettingsPath As String = ""
    '==3311.305=
    Private mLocalSettings1 As clsLocalSettings
    '=3311.305=
    Private mSysInfo1 As clsSystemInfo

    '--staff--
    Private mIntStaff_Id As Integer = -1
    Private msStaffBarcode As String = ""
    Private msStaffName As String = ""
    Private mlStaffTimeout As Integer = 1 '--Not used in this class now...--

    Private msCurrentUser As String
    Private mbIsSqlAdmin As Boolean

    '--  Jet-- RM --
    Private msJetPathInfoKey As String '--  eg- "RM_JetPath_computername" --

    Private msJetDbName As String
    Private msJetUid As String '--jet-
    Private msJetPwd As String '--jet-
    Private msSaveJetPath As String

    '-- Multi-Retail-Host--
    Private mRetailHost1 As _clsRetailHost
    Private msRetailHostname As String
    Private msProviderCode As String '--RM  or PBPOS..--

    Private mbIsPosOnly As Boolean = False  '-RAs34-

    '=3311.309= In-house RA's..=
    Private mLngRAsTreeBGColour As Integer
    Private mClsRAsMain1 As clsRAsMain33


    '= = = = = = = = = = = = = = = = = = = = = = = = =  =


    'Private mBrowseRAs1 As clsBrowse3 '== clsBrowse22
    'Private mBrowseSupplier1 As clsBrowse3 '== clsBrowse22

    'Private msWhereRAsExist As String = "" '--WHERE for suppliers browse..-

    'Private mColPrefsRAs As Collection '--  all statuses..-
    'Private mColPrefsSupplier As Collection '--  all statuses..-
    'Private mColSelectedSupplierRecord As Collection '--name/address+  all info..-
    'Private msSupplierName As String = ""
    'Private msSupplierAddressInfo As String = ""
    'Private msSupplierMainPhone As String = ""
    'Private msSupplierMainFax As String = ""
    'Private msSupplierMainEmail As String = ""

    ''---  printers..--

    'Private miPrtIndex As Short = -1
    'Private msDefaultPrinterName As String = ""
    ''== Private msColourPrtName As String = ""
    'Private msColourPrinterName As String = ""
    'Private msReceiptPrtName As String = ""
    'Private msLabelPrtName As String = ""

    'Private mButtonCurrentBrowseRAs As System.Windows.Forms.ToolStripButton
    'Private mButtonCurrentBrowseSupplier As System.Windows.Forms.ToolStripButton

    'Private mlRAId As Integer = -1

    'Private mDateOldest As Date
    ''= = = = = = = = = = = = = = =

    'Private msServiceChargeCat1 As String
    'Private msServiceChargeCat2 As String

    ''= = = = = = = = =  = = = = =

    ''--  Business Info-
    ''--  Business Info-
    'Private msBusinessABN As String
    Private msBusinessName As String
    'Private msBusinessAddress1 As String
    'Private msBusinessAddress2 As String
    'Private msBusinessShortName As String
    'Private msBusinessPhone As String
    'Private msBusinessPostCode As String
    'Private msBusinessState As String
    ''-- for printing..
    'Private mColBusiness As Collection

    'Private mdDateCreated As Date
    'Private msLicenceKey As String
    'Private mbLicenceOK As Boolean
    'Private msGSTPercentage As String
    'Private mCurGSTPercentage As Decimal

    'Private mImageUserLogo As Image
    ''--Barcodes..-
    'Private msItemBarcodeFontName As String
    'Private mlItemBarcodeFontSize As Integer

    'Private mbCancelled As Boolean
    'Private mbOK As Boolean

    'Private msLog As String = ""
    ''== = = = = = = = = =  = = = = == = =

    'Private mDataGridViewCellStyleHdr As DataGridViewCellStyle
    'Private mDataGridViewCellStyleData As DataGridViewCellStyle

    'Private mNodeActiveRoot, mNodeCompletedRoot As System.Windows.Forms.TreeNode
    ''== Private mNodeCancelledRoot As System.Windows.Forms.TreeNode

    '-- for RAs DLL-
    '== Private mClsRAsMain1 As JMxRAs340.clsRAsMain33
    Private msDefaultPrinterName As String = ""


    ''=END IMPORT= vars

    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Input Properties..--
    '-    From sub Main -- ===

    '--Properties as input parameters--

    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connectionsql() As OleDbConnection '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property
    '- - - - - - - - - -

    '--accept full table/cols description for browsed table--
    '--accept full table/cols description for browsed table--

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)

            mColSqlDBInfo = Value

        End Set
    End Property
    '- - - - - -
    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
            msInputDBNameJobs = Value
        End Set
    End Property
    '- = = = = = = = = = = = =  = == =  =

    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffBarcode() As String
        Set(ByVal Value As String)
            msStaffBarcode = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '-- Staff Id now comes from caller..--

    'WriteOnly Property StaffName() As String
    '    Set(ByVal Value As String)
    '        msStaffBarcode = Value
    '    End Set
    'End Property '--name--
    ''= = = = = = = =  = = =

    'WriteOnly Property StaffId() As Integer
    '    Set(ByVal Value As Integer)
    '        mIntStaff_Id = Value
    '    End Set
    'End Property '--id.-
    '= = = = = = = = = = = =  =
    '-===FF->

    '-- Connect to Retail Manager..--
    '-- MULTI-HOST version--
    '-- MULTI-HOST version--
    '-- MULTI-HOST version--

    '-- sProviderCode is "RM" or "QBPOS".. --

    Private Function mbRMConnect(ByVal sProviderCode As String, _
                                 Optional ByVal bNewPath As Boolean = False) As Boolean
        '==Dim sSettingsDB As String
        Dim sSettingsUid As String
        Dim sSettingsPwd As String
        Dim bUpdateJet, bConnected As Boolean
        '== Dim bNewPath As Boolean
        Dim strSchema As String

        '== bNewPath = False
        mbRMConnect = False

        '== msJetDbName = mSdSystemInfo.Item(UCase(msJetPathInfoKey))
        '== msJetUid = mSdSystemInfo.Item("RM_JETUSERNAME")
        '== msJetPwd = mSdSystemInfo.Item("RM_JETPASSWORD")

        If sProviderCode = "RM" Then
            msJetPathInfoKey = "RM_JetPath_" & msComputerName
            sSettingsUid = "RM_JETUSERNAME"
            sSettingsPwd = "RM_JETPASSWORD"
        ElseIf (sProviderCode = "QBPOS") Then
            msJetPathInfoKey = "QBPOS_JetPath_" & msComputerName
            sSettingsUid = "QBPOS_JETUSERNAME"
            sSettingsPwd = "QBPOS_JETPASSWORD"
            '==3083== labRetailHostPrompt.Text = "QuickbooksPOS Database:"
            '==3083== labRetailHostPrompt.BackColor = Color.Orange
        Else
            MsgBox("Invalid Provider code in RMConnect..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        msJetDbName = mSysInfo1.item(UCase(msJetPathInfoKey))
        msJetUid = mSysInfo1.item(sSettingsUid)
        msJetPwd = mSysInfo1.item(sSettingsPwd)

        '--  can request browse for new DB..
        If bNewPath Then msJetDbName = "" '--force new path..-

        '--connect to jet db..--
        '==3083== labRetailHostPrompt.Visible = True
        bUpdateJet = False
        If (msJetDbName = "") Then bUpdateJet = True

        bConnected = False
        While Not bConnected
            If Not mRetailHost1.connect("", msJetDbName, msJetUid, msJetPwd, bNewPath, strSchema) Then
                If (MsgBox("No Retail Host connection.." & vbCrLf & _
                             "Do you want to retry", _
                              MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) <> MsgBoxResult.Yes) Then
                    Exit Function
                End If
            Else  '--ok-
                bConnected = True
            End If
        End While
        Call glSaveTextFile(gsJobMatixLocalDataDir("JobMatix33") & "\RetailM_schema.txt", strSchema)
        On Error GoTo 0

        '-- save jet parms in sysinfo..--
        If bUpdateJet Or bNewPath Then
            If Not mSysInfo1.UpdateSystemInfo(New Object() {msJetPathInfoKey, msJetDbName, sSettingsUid, msJetUid, sSettingsPwd, msJetPwd}) Then
                MsgBox("Failed to update JET details in systemInfo table..", MsgBoxStyle.Critical)
            End If
        End If '--update jet..-
        txtJetDBName.Text = msJetDbName

        mbRMConnect = True
    End Function '--RM-connect..-
    '= = = = = = =
    '-===FF->

    '-- test sql server user condition.-
    '----  the SELECT statemen1 provided should return a single value.-

    Private Function mbTestSqlUser(ByRef cnnSQL As OleDbConnection, _
                                    ByVal strSelectQuery As String) As Boolean

        mbTestSqlUser = gbTestSqlUser(cnnSQL, strSelectQuery)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '-test--
    '= = = = = = = = = = =


    '-- lookup RM Staff to given BARCODE.--

    Private Function mbLookupStaff(ByRef sBarcode As String, _
                                    ByRef colFields As Collection) As Boolean
        '=== Dim colFld As Collection  '--"name"=, "value"-
        '== Dim fld1 As ADODB.Field
        Dim s1 As String

        mbLookupStaff = False

        '--staff Signon..--
        If mRetailHost1.staffGetStaffRecord(sBarcode, -1, colFields) Then '--found..--
            s1 = colFields.Item("docket_name")("value")
            '=== txtResults.Text = txtResults.Text & "-- Staff record: --" & vbCrLf & _
            ''===     "Staff-id: " & colRecord("staff_id")("value") & "; docket_name is: " & s1 & vbCrLf & _
            ''===     "Surname: " & colRecord("surname")("value") & vbCrLf & "= = = = = = " & vbCrLf
            '== MsgBox "Found: " & s1, vbInformation
            mbLookupStaff = True
        Else
            MsgBox("Staff code not found.", MsgBoxStyle.Exclamation)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

    End Function '--staff lookup..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- load--

    Private Sub frmRAs34Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim iPos, ix, L1, lngError As Integer
        Dim s1 As String
        Dim sName, sErrors As String
        '= Dim sCmdLine As String
        Dim v1 As Object
        mlFormDesignHeight = Me.Height
        mlFormDesignWidth = Me.Width
        Call CenterForm(Me)

        Try
            mbStartupDone = False
            msComputerName = System.Environment.MachineName
            '= gsErrorLogPath = gsJobMatixLocalDataDir("JobMatix34") & "\JobMatix34RAs-Runtime-" & VB.Left(s1, 7) & ".log"
            '= gsRuntimeLogPath = gsErrorLogPath  '--gsAppPath & "JTv3_Runtime.log"

            Call gbLogMsg(gsRuntimeLogPath, "=== JobMatixRAs Main form is loading..")

            msJobMatixVersion = "JobMatixRAs-  v" & CStr(My.Application.Info.Version.Major) & "." & _
                                      My.Application.Info.Version.Minor & "; Build: " & _
                                    My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
            labVersion.Text = msJobMatixVersion
            Me.Text = msJobMatixVersion

            LabToday.Text = VB.Format(CDate(DateTime.Today), "ddd dd-MMM-yyyy")
            LabServer.Text = msServer & vbCrLf & msSqlDbName

            '==3411.119== RAs own EXE...-
            '- Initialising RAs Main Controls..
            frameRAsTab.Text = ""
            frameRAsTree.Text = ""
            FrameBrowseRAs.Text = ""
            frameRA_suppliers.Text = ""
            txtRASearch.Text = ""

            '-- clear details..
            txtRACat1.Text = ""
            LabRA_id.Text = ""
            LabRAStatusFriendly.Text = ""
            txtRACreated.Text = ""
            txtRAUpdated.Text = ""
            txtRASerialNo.Text = ""
            txtRAProdBarcode.Text = ""
            txtRAProblem.Text = ""
            labRASupplier.Text = ""
            txtRASupplier.Text = ""
            txtRASupplierRANo.Text = ""
            txtFreightTrackNo.Text = ""
            txtRACustomerName.Text = ""
            txtRACustomerContact.Text = ""

            '==3311= SysInfo. use class instance.
            mSysInfo1 = New clsSystemInfo(mCnnSql)

            '= If Not mbLoadAllSystemInfo() Then
            '= End If
            If mSysInfo1.exists("RETAILHOSTNAME") Then
                msRetailHostname = mSysInfo1.item("RETAILHOSTNAME")
            End If
            If mSysInfo1.exists("is_pos_only") Then
                mbIsPosOnly = (LCase(VB.Left(mSysInfo1.item("is_pos_only"), 1)) = "y")
            End If
            msBusinessName = mSysInfo1.item("BUSINESSNAME")

            '--  check if we are sqlAdmin privileged..--
            mbIsSqlAdmin = mbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")
            LabAdmin.Text = IIf(mbIsSqlAdmin, "Admin.User", "")

            Exit Sub

        Catch ex As Exception
            MsgBox("ERROR in frmRAs35Main_Load- " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Me.Close()
        End Try  '-main try-

    End Sub  '-load-
    '= = = = = = = = = == = = =
    '-===FF->

    '--  A c t i v a t e d--

    Private Sub frmRAsMain_Activated(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        mbActivated = True
    End Sub  '-activated-
    '= = = = = = = = = = = ==  = 

    '--  WAS  A c t i v a t e d--
    '--  WAS  A c t i v a t e d--

    Private Sub frmRAsMain_Shown(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        Dim colRecord As Collection
        Dim pd1 As New PrintDocument()
        Dim s1 As String

        '= If mbActivated Then Exit Sub '- do all this once only-
        '= mbActivated = True

        '-- Default Retail Host..--
        If mbIsPosOnly Then
            msRetailHostname = "jobmatixpos"
            labRetailDB.Text = "JobMatix POS DB."
        ElseIf (msRetailHostname = "") Then
            msRetailHostname = "RetailManager" '--default..--
        End If

        If (LCase(msRetailHostname) = "retailmanager") Then
            '-- SET RETAIL MANAGER as Host Provider (Class)--
            msProviderCode = "RM"
            '== cmdPOS.Visible = False
            picPOS_logo.Visible = False  '- No POS..
            labStatus.Text = "Connecting to RetailManager database: " & msJetDbName
            System.Windows.Forms.Application.DoEvents()
            mRetailHost1 = New clsRetailHost
            If Not mbRMConnect("RM") Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("No RM conection..", MsgBoxStyle.Exclamation)
                mCnnSql.Close()
                Me.Close()
                Exit Sub
            End If
        ElseIf (LCase(msRetailHostname) = "jobmatixpos") Then  '--JM POS--
            labStatus.Text = "Setting Connection to JobMatix POS database.. "
            System.Windows.Forms.Application.DoEvents()
            msProviderCode = "JMPOS"
            '= txtJetDBName.Visible = False
            txtJetDBName.BackColor = Color.WhiteSmoke
            labRetailDB.Text = "-- JobMatix --"
            '= picPOS_logo.Left = txtJetDBName.Left
            '= picPOS_logo.Top = txtJetDBName.Top
            txtJetDBName.Text = msSqlDbName
            '== cmdPOS.Visible = True
            '--  create new JobMatixPOS interface class..
            mRetailHost1 = New clsRetailHostJMPOS

            '--Initialise:  Pass sql connection, dbname and colTables..
            Call mRetailHost1.SetConnection(msServer, mCnnSql, mColSqlDBInfo, msSqlDbName)
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Unknown Retail Host...", MsgBoxStyle.Exclamation)
            mCnnSql.Close()
            mRetailHost1.closeConnection()
            Me.Close() '==End '==Unload Me
            Exit Sub
        End If '-- host name=

        'mBrowseRAs1 = New clsBrowse3
        'mBrowseSupplier1 = New clsBrowse3
        'Dim sHostTableName As String
        'Call mRetailHost1.browseGetPrefColumns("supplier", sHostTableName, mColPrefsSupplier)

        '--  lookup staff name.--

        If mbLookupStaff(msStaffBarcode, colRecord) Then
            msStaffName = colRecord.Item("docket_name")("value")
            mIntStaff_Id = CInt(colRecord.Item("staff_id")("value"))
            txtStaff.Text = msStaffBarcode & "/" & msStaffName
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("No staff record for barcode: '" & msStaffBarcode & "'..", MsgBoxStyle.Exclamation)
            mCnnSql.Close()
            mRetailHost1.closeConnection()
            Me.Close() '==End '==Unload Me
            Exit Sub
        End If

        '==331- Load RAs Shipping-label printer Combo-
        cboRAs_A4Printers.Items.Clear()
        labStatus.Text = vbCrLf & "  Checking Default Printer.."
        msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
        '--  find name of default printer..--
        For ix = 0 To (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1)
            s1 = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(ix)
            '--  set printer selected so we can test....--
            pd1.PrinterSettings.PrinterName = s1
            If pd1.PrinterSettings.IsDefaultPrinter Then
                msDefaultPrinterName = s1
                '= Exit For
            End If
            '== ListBox1.Items.Add(pkInstalledPrinters)
            '== cboPrtSelect.Items.Add(pkInstalledPrinters)
            cboRAs_A4Printers.Items.Add(s1)
        Next ix
        '== L1 = Err().Number
        '== On Error GoTo activate_error
        If (msDefaultPrinterName = "") Then '== L1 <> 0 Then '--no printer..-
            MsgBox("Error in getting Default Printer.." & vbCrLf & _
                      "Printers may not be set up in Windows.." & vbCrLf & _
                         "RA printing may not be available..", MsgBoxStyle.Exclamation)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        LabBusiness.Text = msBusinessName

        labStatus.Text = "Initialising RA TreeView.." & vbCrLf
        '= Call mbInitialiseRAsTreeView(tvwRAs)

        labStatus.Text &= "Refreshing RA TreeView.."
        '= Call mbRefreshRAsTreeView(tvwRAs)


        '- start RAs main class instance.--
        '== mClsRAsMain1 = New JMxRAs34.clsRAsMain33
        mClsRAsMain1 = New clsRAsMain33(Me, Picture2.Image, labStatus, _
                                             msServer, labVersion.Text, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                               msStaffBarcode, True, mRetailHost1)
        '=3411.119=
        '-- Give clsRAsMain the staff.
        mClsRAsMain1.staffSignedOn(mIntStaff_Id, msStaffBarcode, msStaffName)

        labStatus.Text &= " Done.."
        DoEvents()  '--let it settle..

        labStatus.Text = " Startup Done.."
        TimerRAs.Enabled = True

        Picture2.Visible = False
        '== Dim ev1 As New TabControlEventArgs
        '== Call mClsRAsMain1.TabControlRAs_selected(TabControlRAs, ev1)
        TabControlRAs.TabPages("TabPageRAsTree").Select()

        mbStartupDone = True
        mbIsInitialising = False

    End Sub  '--SHOWN.. was activated-
    '= = = = = = = = = == = = =
    '-===FF->

    '--  form resized..--
    '--  form resized..--

    Private Sub frmJobMatix3_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then

            '--  cant make smaller than original..-
            If (Me.Height < mlFormDesignHeight) Then Me.Height = mlFormDesignHeight
            If (Me.Width < mlFormDesignWidth) Then Me.Width = mlFormDesignWidth
            '==cmdExit.Left = (Me.Width - cmdExit.Width - 25)

            '== frameRAsTab.SetBounds(mlResultsLeft, mlResultsTop, (Me.Width - 24), (Me.Height - mlResultsTop - 40))
            '= frameRAsTab.Width = TabPageRAs.Width - 6
            '= frameRAsTab.Height = TabPageRAs.Height - 37
            frameRAsTab.Width = Me.Width - 20
            panelHdr.Width = frameRAsTab.Width
            frameRAsTab.Height = Me.Height - panelHdr.Height - 83  '- 37
            '-- move Details Frame Horiz ONLY...--
            FrameRADetails.Left = frameRAsTab.Width - FrameRADetails.Width - 7
            FrameRADetails.Height = frameRAsTab.Height - 16

            LabShowSearchRAs.Width = frameRAsTab.Width - FrameRADetails.Width - 7
            labJobMatixRAs.Left = LabShowSearchRAs.Width + 7
            cmdNewRA.Left = frameRAsTab.Width - cmdNewRA.Width - 27
            '- TabControlRAs holds the rest..

            TabControlRAs.Height = frameRAsTab.Height - 17
            TabControlRAs.Width = frameRAsTab.Width - FrameRADetails.Width - 11

            frameRAsTree.Height = TabControlRAs.Height - 40  '=(frameRAsTab.Height - 40)
            frameRAsTree.Width = frameRAsTab.Width - FrameRADetails.Width - 17 '=(frameRAsTab.Width - FrameRADetails.Width - 11)
            tvwRAs.Height = (frameRAsTree.Height - 80)
            tvwRAs.Width = (frameRAsTree.Width - 16)
            LabTreeStatusRAs.Top = frameRAsTree.Height - LabTreeStatusRAs.Height - 3

            '==FrameBrowseRAs.Move 120, 480, (SSTabMain.Width - FrameRADetails.Width - 300), (SSTabMain.Height - 600)
            '==FrameBrowseRAs.Top = frameRAsTree.Top
            FrameBrowseRAs.Height = frameRAsTree.Height
            FrameBrowseRAs.Width = frameRAsTree.Width
            DataGridViewRAs.Height = (FrameBrowseRAs.Height - DataGridViewRAs.Top - 20)
            DataGridViewRAs.Width = (FrameBrowseRAs.Width - 20)
            labRecCountRAs.Top = FrameBrowseRAs.Height - 20

            '--suppliers-
            '= frameRA_suppliers.Top = frameRAsTree.Top
            frameRA_suppliers.Height = frameRAsTree.Height
            frameRA_suppliers.Width = frameRAsTree.Width

            dgvRA_suppliers.Height = frameRA_suppliers.Height - 140
            listViewSupplierRAs.Height = dgvRA_suppliers.Height '= frameRA_suppliers.Height - panelRA_supplierInfo.Height - 11
            Dim intX2 As Integer = (((frameRA_suppliers.Width \ 9) * 5) - 11) '==get 4/7ths of frame width for listView.
            listViewSupplierRAs.Width = intX2
            panelRA_supplierInfo.Width = intX2
            listViewSupplierRAs.Left = frameRA_suppliers.Width - intX2 - 11
            panelRA_supplierInfo.Left = listViewSupplierRAs.Left
            dgvRA_suppliers.Width = frameRA_suppliers.Width - intX2 - 27
            btnRAsUpdateGroupSent.Left = panelRA_supplierInfo.Width - btnRAsUpdateGroupSent.Width - 12

            labVersion.Top = Me.Height - 40

            System.Windows.Forms.Application.DoEvents()
        End If  '--min-
    End Sub  '-resize-
    '= = = = = = = = = ==  =
    '-===FF->

    '-- RA main form events.

    '- scraped out of JobMatixRAs34 Main..

    '== 3311.0118= 18Jan2018=
    '--    RAs Back AS EXE.. main Events..
    '--    RAs Back AS EXE.. main Events..

    '--TimerRAs_Tick-

    Private Sub TimerRAs_Tick(sender As Object, ev As EventArgs) Handles TimerRAs.Tick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.TimerRAs_Tick(sender, ev)

    End Sub  '--TimerRAs_Tick-
    '= = = = = = = = = == === 

    '-- RAs Tree and Grid..
    '-- RAs Tree and Grid..

    Private Sub tvwRAs_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                     Handles tvwRAs.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.tvwRAs_KeyPress(eventSender, eventArgs)
        Exit Sub

    End Sub '--keypress..-
    '= = = = = =  = = =

    Private Sub tvwRAs_DoubleClick(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles tvwRAs.DoubleClick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.tvwRAs_DoubleClick(eventSender, eventArgs)
        Exit Sub

    End Sub '--keypress..-
    '= = = = = =  = = =

    '-- TreeView Node Click--

    Private Sub tvwRAs_NodeClick(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) _
                                        Handles tvwRAs.NodeMouseClick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.tvwRAs_NodeClick(eventSender, eventArgs)

        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '--node click..
    '= = = = = = = = = = = =
    '-===FF->

    Private Sub cmdRefreshRAsTree_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles cmdRefreshRAsTree.Click

        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.cmdRefreshRAsTree_Click(eventSender, eventArgs)

    End Sub '--refresh.-
    '= = = = = = = = = =

    Private Sub OptRATreeSort_CheckedChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) _
                                                   Handles _OptRATreeSort_0.CheckedChanged, _
                                                   _OptRATreeSort_1.CheckedChanged, _
                                                   _OptRATreeSort_2.CheckedChanged, _
                                                   _OptRATreeSort_3.CheckedChanged
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.OptRATreeSort_CheckedChanged(eventSender, eventArgs)
        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '--sort-
    '= = = = = = = =
    '- end of tree stuff..
    '-===FF->


    '-- RAs Grid..  Mouse Activity..--
    '-- RAs Grid..  Mouse Activity..--

    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '-- set new sort column--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridViewRAs_Sorted(ByVal sender As Object, _
                                      ByVal ev As System.EventArgs) Handles DataGridViewRAs.Sorted

        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.dataGridViewRAs_Sorted(sender, ev)

    End Sub  '--sorted..-
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to SHOW--

    Private Sub DataGridViewRAs_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                                   Handles DataGridViewRAs.CellMouseClick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.DataGridViewRAs_CellMouseClickEvent(eventSender, eventArgs)

    End Sub '--click--
    '= = = = = = = = = =

    '-- Enter Row..  update RA detail..-

    '== THIS causes re-entrancy problems. ????  --

    Private Sub dataGridViewRAs_RowEnter(ByVal sender As Object, _
                                         ByVal ev As DataGridViewCellEventArgs) _
                                                      Handles DataGridViewRAs.RowEnter
        '= If mbIsInitialising Or mbStartingUp Then Exit Sub
        If mbIsInitialising Then Exit Sub

        Call mClsRAsMain1.dataGridViewRAs_RowEnter(sender, ev)
        '-thats all-

    End Sub  '--row enter-
    '= = = = = = = =  == = = =
    '-===FF->

    '--mouse activity---  select row to edit--

    Private Sub dataGridViewRAs_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                      Handles DataGridViewRAs.CellMouseDoubleClick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.DataGridViewRAs_CellMouseDblClickEvent(eventSender, eventArgs)

    End Sub '--DBL click--
    '= = = = = = = = = =

    '--key activity---  select row to edit--

    Private Sub DataGridViewRAs_KeyUp(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As KeyEventArgs) Handles DataGridViewRAs.KeyUp
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.DataGridViewRAs_KeyUp(eventSender, eventArgs)

    End Sub '--key-up--
    '= = = = = = = = = = =
    '-===FF->

    '-- RAs Browser.. txt FIND Activity.--
    '-- RAs Browser.. txt FIND Activity.--

    '--BROWSING RAs.. --

    '--key activity---  select row to edit--
    Private Sub txtFindRAs_KeyPress(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                Handles txtFindRAs.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindRAs_KeyPress(eventSender, eventArgs)

    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFindRAs_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) _
                                       Handles txtFindRAs.Enter
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindRAs_Enter(eventSender, eventArgs)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFindRAs_Leave(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) _
                                               Handles txtFindRAs.Leave
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindRAs_Leave(eventSender, eventArgs)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFindRAs.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFindRAs_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) _
                                           Handles txtFindRAs.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindRAs_TextChanged(eventSender, eventArgs)

        '= Call mBrowseRAs.Find(txtFindRAs)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = = = = = =
    '-===FF->


    '== RA's Grid/Search events..

    '-- ToolbarRAs for RA Browse..--
    '---- just sets up browser..

    Private Sub ToolbarRAsGrid_ButtonClick(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) _
                     Handles _ToolbarRAs_ButtonQueued.Click, _ToolbarRAs_ButtonRequested.Click, _
                                _ToolbarRAs_ButtonGranted.Click, _ToolbarRAs_ButtonShipped.Click, _
                                  _ToolbarRAs_ButtonCompleted.Click, _ToolbarRAs_ButtonAll_RAs.Click
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.ToolbarRAsGrid_ButtonClick(eventSender, eventArgs)

    End Sub '--toolbar RAs.--
    '= = = = = = = = =
    '-===FF->

    '-- RA Search..-

    Private Sub txtRASearch_Enter(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtRASearch.Enter
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtRASearch_Enter(eventSender, eventArgs)

        'txtRASearch.SelectionStart = 0
        'txtRASearch.SelectionLength = Len(txtRASearch.Text)
    End Sub '--got focus..-
    '= = = = = = = = = = =

    Private Sub txtRASearch_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                          Handles txtRASearch.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtRASearch_KeyPress(eventSender, eventArgs)

    End Sub '--keypress..-
    '= = = = = = = =

    '-- Clear--
    Private Sub cmdClearRASearch_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles cmdClearRASearch.Click
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.cmdClearRASearch_Click(eventSender, eventArgs)

    End Sub '--clear--
    '= = = = = = =
    '-===FF->

    '-- Search add some more crteria --
    '--  to the current Browse parameters..-

    Private Sub cmdRASearch_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdRASearch.Click
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.cmdRASearch_Click(eventSender, eventArgs)

    End Sub '--search..-
    '= = = = = = = = == =

    '-- done RA grid stuff..
    '-===FF->

    '=3311.313'-- RA Suppliers Data Grid.
    '--    R A  S U P P L I E R  --  BROWSING..
    '--    R A  S U P P L I E R  --  BROWSING..
    '--    R A  S U P P L I E R  --  BROWSING..

    '--  F l e x G r i d  E v e n t s..--
    '--  F l e x G r i d  E v e n t s..--
    '--mouse activity---  select col-headers--
    '-- set new sort column--

    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvRA_suppliers_Sorted(ByVal sender As Object, _
                                      ByVal ev As System.EventArgs) Handles dgvRA_suppliers.Sorted
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.dgvRA_suppliers_Sorted(sender, ev)

    End Sub
    '= = = = = = = = =  = = =
    '-===FF->


    '-- Enter Row..  update RA list for supplier...-

    '== THIS causes re-entrancy problems. ????  --

    Private Sub dgvRA_suppliers_RowEnter(ByVal sender As Object, _
                                         ByVal ev As DataGridViewCellEventArgs) _
                                                      Handles dgvRA_suppliers.RowEnter
        '= If mbIsInitialising Or mbStartingUp Then Exit Sub
        If mbIsInitialising Then Exit Sub

        Call mClsRAsMain1.dgvRA_suppliers_RowEnter(sender, ev)
        '-thats all-
    End Sub  '--row enter-
    '= = = = = = = =  == = = =

    '--mouse activity---  select row to SHOW--
    '--mouse activity---  select row to SHOW--

    Private Sub dgvRA_suppliers_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                     Handles dgvRA_suppliers.CellMouseClick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.dgvRA_suppliers_CellMouseClickEvent(eventSender, eventArgs)

    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to edit--

    Private Sub dgvRA_suppliers_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                        ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                                   Handles dgvRA_suppliers.CellMouseDoubleClick

        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.dgvRA_suppliers_CellMouseDblClickEvent(eventSender, eventArgs)

    End Sub '--dbl-click--
    '= = = = = = = = = =
    '-===FF->

    '--key activity---  select row to edit--
    '--  CATCH  << CTL-ENTER >>  ---

    Private Sub dgvRA_suppliers_KeyUp(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As KeyEventArgs) Handles dgvRA_suppliers.KeyUp

        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.dgvRA_suppliers_KeyUp(eventSender, eventArgs)

    End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

    '--BROWSING SUPPLIERS.. --

    '--SUPP key activity---  select row to edit--
    Private Sub txtFindSupplier_KeyPress(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                               Handles txtFindSupplier.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindSupplier_KeyPress(eventSender, eventArgs)

    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFindSupplier_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles txtFindSupplier.Enter
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindSupplier_Enter(eventSender, eventArgs)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFindSupplier_Leave(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles txtFindSupplier.Leave
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindSupplier_Leave(eventSender, eventArgs)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFindCust.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFindSupplier_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtFindSupplier.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.txtFindSupplier_TextChanged(eventSender, eventArgs)

    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    Private Sub chkShowGrantedRAsOnly_CheckedChanged(eventSender As Object, _
                                                  ev As EventArgs) Handles chkShowGrantedRAsOnly.CheckedChanged
        If mbIsInitialising Then Exit Sub
        '-- class instance may not be fully awake.
        If mClsRAsMain1 Is Nothing Then Exit Sub
        Call mClsRAsMain1.chkShowGrantedRAsOnly_CheckedChanged(eventSender, ev)

    End Sub  '-chkShowGrantedRAsOnly_CheckedChanged-
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Supplier.RA's  listview--
    '--  Show selected RA detail..--

    '--listViewJobs_Click--

    Private Sub listViewSupplierRAs_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles listViewSupplierRAs.Click
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.listViewSupplierRAs_Click(eventSender, eventArgs)

    End Sub '--listViewJobs_Click--
    '= = = = = = = =  = = = = = = = =

    '---If any item is  selected, then show RA details..--
    Private Sub listViewSupplierRAs_SelectedIndexChanged _
                           (ByVal eventSender As Object, ByVal ev As System.EventArgs) _
                                    Handles listViewSupplierRAs.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.listViewSupplierRAs_SelectedIndexChanged(eventSender, ev)

        'Dim listItems As ListView.SelectedListViewItemCollection = Me.listViewSupplierRAs.SelectedItems

        'If (listItems.Count > 0) Then  '--have selection..-
        '    '== Call mbLoadQuoteInfo()
        '    '== cmdBuildQuote.Enabled = True
        'End If
    End Sub  '-index changed.-
    '= = = = = = = = = = = = 

    Private Sub listViewSupplierRAs_dblClick(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles listViewSupplierRAs.DoubleClick
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.listViewSupplierRAs_dblClick(eventSender, eventArgs)

    End Sub '-dblClick
    '= = = = = = = = = 

    Private Sub chkSelectAllRAsGranted_CheckedChanged(eventSender As Object, _
                                                      eventArgs As EventArgs) _
                                               Handles chkSelectAllRAsGranted.CheckedChanged
        If mbIsInitialising Then Exit Sub
        If mClsRAsMain1 Is Nothing Then Exit Sub
        Call mClsRAsMain1.chkSelectAllRAsGranted_CheckedChanged(eventSender, eventArgs)

    End Sub  '-- chkSelectAllRAsGranted-
    '= = = = = = = = = = = = =  = = = = = 

    '- Print group Label and mark as sent..

    Private Sub btnRAsUpdateGroupSent_Click(eventSender As Object, _
                                              eventArgs As EventArgs) _
                                                Handles btnRAsUpdateGroupSent.Click
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.btnRAsUpdateGroupSent_Click(eventSender, eventArgs)

    End Sub  '-btnRAsUpdateGroupSent-
    '= = = = = = = = = = = = = = = = 

    Private Sub cboRAs_A4Printers_SelectedIndexChanged(eventSender As Object, EventArgs As EventArgs) _
                                                  Handles cboRAs_A4Printers.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        If mClsRAsMain1 Is Nothing Then Exit Sub
        Call mClsRAsMain1.cboRAs_A4Printers_SelectedIndexChanged(eventSender, EventArgs)

    End Sub  '-cboRAs_A4Printers-
    '= = = = = = = = = = = = = =

    '--  END OF  -  R A  S U P P L I E R  --  BROWSING..
    '--  END OF  -  R A  S U P P L I E R  --  BROWSING..
    '--  END OF  -  R A  S U P P L I E R  --  BROWSING..
    '= = = = = = = = = = = = = = = = = = = = == = = === 

    '-- done RA Suppliers..
    '-===FF->


    '== RAs Main TAB..

    '-- selected-

    Private Sub TabControlRAs_selected(sender As TabControl, ev As TabControlEventArgs) _
                                                                    Handles TabControlRAs.Selected
        If mbIsInitialising Then Exit Sub
        Call mClsRAsMain1.TabControlRAs_selected(sender, ev)

    End Sub  '--selected..
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--RA Tree-
    '--RAs Search Grid..- 
    '-- Suppliers-
    '= TabControl= Private Sub ToolStripButtonRAsTree_Click(sender As Object, ev As EventArgs)
    '= TabControl=     Call mClsRAsMain1.ToolStripButtonRAs_Click(sender, ev)
    '= TabControl= End Sub  '-ToolStripButtonRAsTree_Click-
    '= = = = = = = = = = = = = =  = = = = = = =
    '-===FF->

    '--  External calls--

    '-- New RA -

    Private Sub cmdNewRA_Click(sender As Object, ev As EventArgs) Handles cmdNewRA.Click
        If mbIsInitialising Then Exit Sub
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        Call mClsRAsMain1.cmdNewRA_Click(sender, ev)

        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub ''- New RA-
    '= = = = = == = = = 

    Private Sub btnRA_Attachments_Click(sender As Object, ev As EventArgs) Handles btnRA_attachments.Click
        If mbIsInitialising Then Exit Sub

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        Call mClsRAsMain1.btnRA_Attachments_Click(sender, ev)

        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '--attachments-
    '= = = = = === = = =

    Private Sub cmdViewRecordRAs_Click(sender As Object, ev As EventArgs) Handles cmdViewRecordRAs.Click
        If mbIsInitialising Then Exit Sub
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        Call mClsRAsMain1.cmdViewRecordRAs_Click(sender, ev)

        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '-View RA..-
    '= = = =  = = = =  = = = = = = 
    '-===FF->

    '-- Exit-

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Hide()
    End Sub
End Class  '- frmRAs34Main=
'= = = = = = = = = = = = = =

'== end form ==