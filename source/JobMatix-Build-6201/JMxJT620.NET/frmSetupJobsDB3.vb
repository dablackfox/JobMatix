Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Public Class frmSetupJobsDB
    Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
    '= = = = = = = = = = =

    '--Form to CREATE New JobTracking Business DB..--

    '=== Updated:  15-Oct-2009===3:28pm===
    '=== Updated:  25-Jan-2010===Added HourlyRate and Min.Charge..===
    '=== Updated:  03-Feb-2010===Added Underscore to DB Name..===
    '=== Updated:  13-Mar-2010===Added "ItemBarcodeFontName" Key to SysInfo collection...===
    '=== Updated:  30-Mar-2010===Made Three labour rates for Three Job Priorities ...===
    '=== Updated:  04-May-2010===REv:2422- Windows Authentication-
    '--- ----      ---   No more SQL UserName/Password..=
    '=== Updated:  27-May-2010===REv:2436- Don't create SecurityId here..-
    '=== Updated:  15-Jul-2010===Rev:2455- Now must have postcode and State...-
    '=== Updated:  28-Jul-2010===Rev:2456- "ItemBarcodeFontSize" added...-
    '=== Updated:  29-Aug-2010===Rev:2464- "GSTPercentage" added...-

    '=== Updated:  30-Mar-2011===Rev:2804- "QuoteChassisCat1/2" for systemInfo..--
    '=== Updated:  03-Apr-2011===Rev:2804- "smsCentralGatewayURL" etc. for systemInfo..--
    '=== Updated:  17-Jun-2011===Rev:2908-  Allow Duplicate shortname to continue and re-cretae database...--

    '== 11-Aug-2011-  NEW VERSION to Create DB setup ..--
    '---  AND to Change SystemInfo config. values..--
    '-----  NOTE.  Separate FORM for SMS setup Updates..

    '== 19-Nov-2011- Rev 3013-  NEW VERSION to UPGRADE to vb.net ..--
    '----  NO MORE GOSUBs...  ---

    '== 03-Dec-2011- Rev 3023-  VERSION Prep. to UPGRADE to vb.net ..--
    '==       Add Option buttons to identify RetailHost system..-
    '--      AND add some code to check text lengths ( no max length in vb.net.)--

    '== 14-Dec-2011- Rev 3023- VB.NET VERSION Prep..--
    '==  03-Mar-2012- Rev 3031---    "clsStrDictionary"  REPLACES  "Scripting.Dictionary"--
    '==  05-Mar-2012==  mbIsInitialising =
    '== 
    '== 12-Apr-2012- Build= 3043.0- 
    '==   Add new userText(3):  "ServiceChargesInfoText" ==
    '==
    '== 30-Apr-2012- Build= 3047.1- 
    '==  Catch all ENTER keys on input controls, and
    '--   Use "SelectNextControl" to navigate forwards..--
    '==
    '== 01Jul2012== 
    '==  3063.0 hyphen NOT allowed in shortname (or DB-name)...  
    '==
    '== 23/27-Jul-2012 =3067.0== 
    '==   >>  Add helpProvider-
    '==   >> For now-  disable Quickbooks option..
    '==   >>  Catch error on Font Dialog..
    '==
    '== 18-May-2013 =3077.0== 
    '==     Fix Version text..
    '= = = = = = = = = = = = = = = = = = = = = = = = = = 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = 
    '== 
    '==  grh. JobMatix 3.1 ---  19-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped ADODB and sqlClient)..
    '==
    '== 
    '==  grh. JobMatix 3.1.3101.927 ---  27-Sep-2014 ===
    '==        >>  Add JobMatixPOS as alt. Retail Host.
    '==
    '==  grh. JobMatix 3.1.3107.518 ---  18-May-2015 ===
    '==        >>  WARNING: JobMatixPOS as alt. Retail Host still ALPHA version.
    '==
    '==  grh. JobMatix 3.1.3107.707 ---  07-Jul-2015 ===
    '==        >>  Font changes on form.
    '==
    '==  grh. JobMatix 3.1.3107.801 ---  01-Aug-2015 ===
    '==        >>  Log files to CommonData.
    '==        >>  Fix ABN non-numeric bug..
    '== 
    '==  grh. JobMatix 3.1.3107.803-  03-Aug-2015 ===
    '==   >>  Now Using .net 4.5.2
    '== 
    '== NEW VERSION-
    '==  grh. JobMatix 3.2.3203.105-  05-Jan-2016 ===
    '==   >>  Back to Using .net 3.5.
    '==   >>  Decouple ESC from Cancel Button..
    '==   >>  3203.117 Add Email text box to Business Info....
    '== 
    '==  >> 11Feb2016-  3203.211- 
    '==         -- ADD Checkbox [Do not enforce MinCharge].
    '==                ( Default is unchecked =No)
    '= = = 
    '==       >>3203.218=  Fix storing ENFORCE_MINIMUM_RESULT:
    '==          Was wrong way around..
    '==
    '= = = NEW VERSION  JobMatix33--
    '==       >> 3311.225=  UpdateSystemInfo now in here.:
    '==       >> 3311.328=  Caller decides if RM or POS..
    '==                      (Input property).
    '==        >>   '=3311.329=  New Labour Rates Stuff.
    '==--         Labour Rates now in MYOB/POS (Service Cat.)..
    '==--          WE keep track of the stock barcodes..
    '==            (SystemInfo:  'labourRateP1RetailBarcode'  etc.)
    '==--             (Not available for Create..  Just when updating..)
    '==                                      (Because of RetailHost).
    '==        >>   '=3311.509= 09May2016-
    '==--         Add option for New JobMatixPOS creators
    '==                   to IMPORT all POS data from RetailManager..
    '==             Then do the IMPORT..
    '==
    '==   v3.4.3401.0424 -- 24Apr2017= Extra fix-
    '==         -- frmSetupJobsDB3..-  New MYOB-RM users must have "-jobtracking" DB name..
    '==                  AND NO POS tables will be included in DB..
    '==
    '==   >> 3411.0217=  17-Feb-2018= 
    '==          -- 3411.0217-Create new DB (Setup) - ADD TypeOfBusiness" to setup..
    '==               AND Make it optional to set up the Goods/Tasks etc as per computer Shop.... 
    '== 
    '==   >> 3431.0527- 27May2018-
    '==      -- To Fix Amend crash when no LabourRates/Descriptions set up
    '==                           Revert to legacy decsriptors...
    '==      --  Avtivated -->  SHOWN..
    '==
    '= = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = = = = = = = = =
    '==
    '==  DLL version of JobTracking. 10-June-2018=
    '==    JMxJT350.dll 
    '==   3501.0610 -- -  
    '==      Main form now called from the Base EXE JMxJT350Ex..
    '==       --  Removed all "End" statements.....
    '==
    '== -- Updated 3501.0814  14August2018=  
    '==     -- Fix clsJMxPOS31 to separate ot function CreatePosTables into clsJMxCreatePOS...
    '== 
    '== -- Updated 4219.1129  29-November-2019=  
    '==      --  Fixes to Setup form to fix visibility of labels on Backup Path.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '==
    '== Target-build-4267  (Started 29-Sep-2020)
    '== Target-build-4267  (Started 29-Sep-2020)
    '== Target-build-4267  (Started 29-Sep-2020)
    '==
    '==  --Update Warning message on Form to Build 4267 (Beta)..  
     '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== in 3063.0 hyphen NOT allowd.  
    '==    Const k_VALIDNAMECHARS As String = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-"
    Const k_VALIDNAMECHARS As String = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    Private Const k_RM_NAME As String = "retailmanager"
    '== Private Const k_QBPOS_NAME As String = "quickbookspos"
    Private Const k_JMPOS_NAME As String = "jobmatixpos"

    Private mbIsInitialising As Boolean  '--  SEE public Sub New()  --

    Private msComputerName As String = ""
    Private msServer As String = ""

    Private msSqlServerComputer As String = ""
    Private mbRunningOnServer As Boolean = False
    Private mbCreateNewDB As Boolean = False
    Private mbIsCreatingPOSDB As Boolean = False

    Private mbCancelled As Boolean = False
    Private mbStarted As Boolean = False

    Private msValidChars As String

    Private mCnnSql As OleDbConnection '== ADODB.Connection
    Private mColDBNames As Collection '--Existing JobTracking DB's--

    Private msBusinessFullName As String
    Private msBusinessShortName As String
    Private msBusinessABN As String
    Private msSqlDbName As String = ""
    '==Private msJobsUserName As String

    Private msOriginalLicence As String = ""

    Private msAppPath As String

    Private mSdSystemInfo As New clsStrDictionary  '== Scripting.Dictionary
    Private mColSystemInfo As Collection
    Private mDateCreated As Date
    Private mbDataChanged As Boolean = False

    '--  updates..
    Private mSdUpdatedInfo As New clsStrDictionary  '==  Scripting.Dictionary

    Private msRetailHostName As String = ""

    '==3311.329-
    '-- Multi-Retail-Host--
    '--  Not available for Create..  Just when updating..
    Private mRetailHost1 As _clsRetailHost

    '= = = = = = = =  = = =  == = = = = = 

    '-- Inputs..--
    '-- Inputs..--
    WriteOnly Property sqlConnection() As OleDbConnection '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--connection..-
    '= = = = = = = = = = =  =

    WriteOnly Property DatabaseName() As String
        Set(ByVal Value As String)

            msSqlDbName = Value
        End Set
    End Property '--db..--
    '= = = = = = = = = = =


    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)

            '== msSqlServerComputer = Value '--save..-
            msServer = Value '== 3311.509=

            labServer.Text = msServer  '= Value '==msSqlServerComputer
        End Set
    End Property '--server..-
    '= = = = = = = = = = =

    WriteOnly Property ServerComputerName() As String
        Set(ByVal Value As String)

            msSqlServerComputer = Value '--save..-
            '== labServer.Text = msSqlServerComputer
        End Set
    End Property '--server..-
    '= = = = = = = = = = =

    WriteOnly Property DBNames() As Collection
        Set(ByVal Value As Collection)
            mColDBNames = Value

        End Set
    End Property '--DBNames--
    '= = = = = = = = = = = =

    WriteOnly Property CreateNewDB() As Boolean
        Set(ByVal Value As Boolean)

            mbCreateNewDB = Value
        End Set
    End Property '--create--
    '= = = = = = = = = = = = =

    WriteOnly Property NewDBIsPOS() As Boolean
        Set(ByVal Value As Boolean)

            mbIsCreatingPOSDB = Value
        End Set
    End Property '--create--
    '= = = = = = = = = = = = =

    '== Results..==

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = = = = =

    '==3311.329-  For Stock Labour Rates..--

    WriteOnly Property retailHost() As _clsRetailHost
        Set(ByVal Value As _clsRetailHost)

            mRetailHost1 = Value
        End Set
    End Property '--host..--
    '= = = = =  = =  = = =

    '-===FF->

    '-- Is RetailManager--

    Private Function mbIsRetailManager() As Boolean

        mbIsRetailManager = (LCase(msRetailHostName) = k_RM_NAME)
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 

    '== Private Function mbIsQBPOS() As Boolean
    '==                          mbIsQBPOS = (LCase(msRetailHostName) = k_QBPOS_NAME)
    '==End Function  '- mbIsQB--
    '= = = = = = = = = = = = = = = = == 

    Private Function mbIsJMPOS() As Boolean

        mbIsJMPOS = (LCase(msRetailHostName) = k_JMPOS_NAME)
    End Function  '- mbIsJobMatixPOS--
    '= = = = = = = = = = = = = = = = == 
    '-===FF->

    Private Sub mbSetupRM()

        _txtServiceCategory_0.MaxLength = 6
        _txtServiceCategory_1.Enabled = True
        labServiceCatPrompt0.Text = "Cat1:"
        labServiceCatPrompt1.Text = "Cat2:"

        _txtChassisCat_0.MaxLength = 6
        _txtChassisCat_1.Enabled = True
        labChassisCatPrompt0.Text = "Cat1"
        labChassisCatPrompt1.Text = "Cat2"

        LabHelpServiceCat.Text = "Any MYOB RetailManager Stock item with these " & _
                     "Cat1/Cat2 values will be defined as " & "a Service Charge forJobMatix job tracking.."
        LabChassisCats.Enabled = True

    End Sub  '--setup RM-
    '= = = = = = = = = = = = = 

    Private Sub mbSetupJMPOS()

        _txtServiceCategory_0.MaxLength = 6
        _txtServiceCategory_1.Enabled = True
        labServiceCatPrompt0.Text = "Cat1:"
        labServiceCatPrompt1.Text = "Cat2:"

        _txtChassisCat_0.MaxLength = 6
        _txtChassisCat_1.Enabled = True
        labChassisCatPrompt0.Text = "Cat1"
        labChassisCatPrompt1.Text = "Cat2"

        LabHelpServiceCat.Text = "Any JobMatixPOS Stock item with these " & _
                     "Cat1/Cat2 values will be defined as " & "a Service Charge forJobMatix job tracking.."
        LabChassisCats.Enabled = True

    End Sub  '--setup RM-
    '= = = = = = = = = = = = = 

    'Private Sub mbSetupQBPOS()

    '    _txtServiceCategory_0.MaxLength = 50
    '    _txtServiceCategory_1.Enabled = False
    '    labServiceCatPrompt0.Text = "Inventory Type:"
    '    labServiceCatPrompt1.Text = ""

    '    '== _txtChassisCat_0.MaxLength = 50
    '    _txtChassisCat_0.Enabled = False
    '    _txtChassisCat_1.Enabled = False
    '    labChassisCatPrompt0.Text = ""  '== "Inventory Type:"
    '    labChassisCatPrompt1.Text = ""

    '    LabHelpServiceCat.Text = "Any Quickbooks POS Stock item with this " & _
    '                 "Inv.Type value will be defined as " & "a Service Charge forJobMatix job tracking.."

    '    LabHelpChassis.Text = ""
    '    LabChassisCats.Enabled = False
    'End Sub  '--setup qbpos-
    '= = = = = = = = = = = = = 
    '-===FF->

    '-- load systemInfo settings..--
    '---  send back a collection of collections (rows..)--

    Private Function mbLoadsystemInfo(ByRef cnnSQL As OleDbConnection, _
                                      ByRef colInfo As Collection, _
                                          ByRef sdSystemInfo As clsStrDictionary) As Boolean
        Dim sSql As String
        Dim sKey, sValue As String
        Dim rs1 As DataTable  '== ADODB.Recordset
        Dim col1 As Collection
        Dim date1, date2 As Date

        mbLoadsystemInfo = False
        sSql = "Select * from [systemInfo] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(cnnSQL, rs1, sSql) Then
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
                    If sKey <> "" Then colInfo.Add(col1, sKey)
                    '-- build Dictionay also..--
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
    '-- vaInfo is array of (key,value,key,value...)
    '---- if key exists then UPDATE, else INSERT..--

    Private Function mbUpdateSystemInfo(ByRef cnnSQL As OleDbConnection, _
                                         ByRef vaInfo As Object) As Boolean
        Dim sSql As String
        Dim sKey, sValue As String
        Dim L1, ix As Integer
        Dim sErrors As String
        '== Dim transaction1 As SqlTransaction2

        mbUpdateSystemInfo = False
        sSql = " BEGIN TRANSACTION " & vbCrLf
        ix = 0
        While ix <= UBound(vaInfo)
            sKey = vaInfo(ix)
            sValue = gsFixSqlStr(CStr(vaInfo(ix + 1)))
            sSql = sSql & "IF EXISTS (SELECT * FROM [SystemInfo] WHERE (SystemKey='" & sKey & "'))" & vbCrLf
            sSql = sSql & "    UPDATE [SystemInfo] SET systemValue='" & sValue & "' " & vbCrLf & _
                          " WHERE (SystemKey='" & sKey & "')" & vbCrLf
            sSql = sSql & "ELSE INSERT INTO [SystemInfo] (SystemKey,systemValue) " & _
                                    " VALUES ('" & sKey & "', '" & sValue & "') " & vbCrLf
            ix = ix + 2
        End While '--ix--
        sSql = sSql & " COMMIT TRANSACTION "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(cnnSQL, sSql, L1, sErrors) Then
            MsgBox("!! Failed in UPDATE jobTracking systemInfo.." & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Critical)
        Else '--ok--
            mbUpdateSystemInfo = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '--update-
    '= = = = = = =  =  = =
    '-===FF->


    '--  vb.net TEXTBOX doesn't recognise maxlength..--

    Private Sub mbCheckMaxText(ByRef txt1 As System.Windows.Forms.TextBox, _
                                ByVal lngMaxLength As Integer)

        If mbIsInitialising Then Exit Sub
        If (Len(Trim(txt1.Text)) > lngMaxLength) Then
            txt1.Text = VB.Left(Trim(txt1.Text), lngMaxLength)
        End If
    End Sub '--CheckMaxText-
    '= = = = = = = = = = = = =

    '--  save changed text into dictionary of updates..-

    Private Function mbSaveUpdatedText(ByRef txt1 As System.Windows.Forms.TextBox) As Boolean

        If mbIsInitialising Then Exit Function

        Dim lngError As Integer
        mbSaveUpdatedText = False

        On Error GoTo SaveUpdated_error
        mSdUpdatedInfo(txt1.Tag) = txt1.Text
        mbDataChanged = True
        If Not mbCreateNewDB Then cmdOK.Enabled = True '--can SAVE if updating..--
        mbSaveUpdatedText = True
        Exit Function

SaveUpdated_error:
        lngError = Err().Number
        MsgBox("Runtime error in 'mbSaveUpdatedText'.. (" & txt1.Tag & ")" & vbCrLf & _
                 "Error is: " & lngError & ":  " & ErrorToString(lngError) & vbCrLf & vbCrLf & _
                                             "Field won't be updated..", MsgBoxStyle.Exclamation)
    End Function '--updated..-
    '= = = = = = = = = =  ==
    '-===FF->

    '--  make ARRAY of system changes..--
    '--   and make list of system changes for display..--

    Private Function msGetChanges(ByRef asChanges() As String) As String
        Dim vKeys, vKey1 As Object
        Dim sKey As String
        Dim sValue As String
        Dim sMsgChanges As String
        Dim ix As Integer

        sMsgChanges = ""
        If Not mbCreateNewDB Then '-- updating..-
            If (mSdUpdatedInfo.Count > 0) Then
                vKeys = mSdUpdatedInfo.Keys
                Erase asChanges
                ix = 0
                '== For ix = 0 To (mSdUpdatedInfo.Count - 1)
                For Each vKey1 In vKeys
                    sKey = CStr(vKey1)   '== vKeys(ix)
                    sValue = mSdUpdatedInfo(sKey)
                    ReDim Preserve asChanges((ix * 2) + 1)
                    asChanges(ix * 2) = sKey
                    asChanges((ix * 2) + 1) = sValue
                    sMsgChanges = sMsgChanges & vbCrLf & _
                             sKey & " : " & " '" & IIf((Len(sValue) <= 64), sValue, VB.Left(sValue, 64)) & "'"
                    ix += 1
                Next vKey1
                '==Next ix
            End If '--count..-
        End If
        msGetChanges = sMsgChanges
    End Function '--changes..--
    '= = = = = = = = = = = = =
    '-===FF->

    '-- L o a d --

    Private Sub frmSetupJobsDB_Load(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim s1 As String

        '== mbDataChanged = False
        '== msComputerName = ""
        msBusinessShortName = ""
        frameBusiness.Text = ""
        frameABN.Text = "Database"
        frameABN.Enabled = False

        '= msRetailHostName = ""
        optRetailHost(0).Enabled = False '--"RetailManager".-
        optRetailHost(1).Enabled = False '-- "JobMatixPOS".-

        '--  backup path is for server only.-
        panelBackupPaths.Enabled = False  '=frameBackupPath.Enabled = False
        frameBackupPath.Text = ""
        txtServerBackupFolder(0).Enabled = False
        txtServerBackupFolder(1).Enabled = False
        cmdBrowsePath.Enabled = False

        frameLabour.Text = ""
        frameCategories.Text = ""
        frameTexts.Text = ""

        '=3203.211=
        chkNoEnforceMinCharge.Checked = True
        chkNoEnforceMinCharge.Enabled = False

        '== mbRunningOnServer = False

        '= mbCancelled = False
        '== mbCreateNewDB = False
        '--msOldUserName = ""
        '--msPassword = ""
        Call CenterForm(Me)
        '==txtUserName.Text = ""
        '==txtPassword(0).Text = ""

        msValidChars = k_VALIDNAMECHARS
        '== cmdOK.Enabled = False
        '== mbStarted = False

        cboState.Items.Clear()
        cboState.Items.Add("NSW")
        cboState.Items.Add("QLD")
        cboState.Items.Add("SA")
        cboState.Items.Add("TAS")
        cboState.Items.Add("VIC")
        cboState.Items.Add("WA")
        cboState.Items.Add("ACT")
        cboState.Items.Add("NT")
        cboState.SelectedIndex = -1 '--none..--
        txtState.Visible = False
        txtState.Left = cboState.Left

        txtGSTPercentage.Text = "10.00"

        LabWarnPostcode.Text = "Important: Make sure that the State/PostCode entries are correct. " & _
                        "These will form part of your JobMatix database identity, and cannot be changed later.."

        ToolTip1.SetToolTip(labRetailHost, "This is a mandatory selection. JobMatix needs a Retail Host database " & _
                                     vbCrLf & " to get information on customers, stock and suppliers.." & vbCrLf & _
                                      vbCrLf & "This selection is stored in your JobMatix database, " & _
                                        "and can't be changed later..")

        labStatus.Text = "Enter Business details for creating new JobMatix instance (DB)."

        LabHelpBarcode.Text = "Stock Barcodes will be printed on the " & _
                "Job completion Service Record for easy scanning into " & "the RetailManager sale transaction screen.."

        LabHelpServiceCat.Text = "Any MYOB RetailManager Stock item with these " & _
                             "Cat1/Cat2 values will be defined as " & "a Service Charge forJobMatix job tracking.."

        LabHelpChassis.Text = "For ""Build from Quotation"" jobs, " & _
                              "any MYOB RetailManager Stock item with these " & _
                                 "Cat1/Cat2 values will be defined as a ""Chassis"" or " & _
                                    "foundation component for JobMatix System building."

        LabHelpBackupPath.Text = "Client-side backup of JobMatix Database:" & vbCrLf & _
                                 "When backing-up the JobMatix database from a client computer, " & _
                                   "the SQL Server Backup function needs to write the backup file to " & _
                                     "a folder path accessible from the server machine.."
        '--  System Keys..--
        '--  System Keys..--
        '--  setup TextBox Tags with SystemKey values..
        '-----  so we know what to update in the SystemInfo table..--

        txtFullName.Tag = "BusinessName"
        txtAddress1.Tag = "BusinessAddress1"
        txtAddress2.Tag = "BusinessAddress2"
        '==cboState.Tag = "BusinessState"

        '==txtPostCode.Tag = "BusinessPostCode"  '--won't be updating this..-
        txtPhone.Tag = "BusinessPhone"
        txtEmail.Tag = "BusinessEmail"  '=3203.117=

        txtUserLicence.Tag = "LicenceKey"

        txtGSTPercentage.Tag = "GSTPercentage"
        txtLabourRates(0).Tag = "LabourHourlyRatePriority1"
        txtLabourRates(1).Tag = "LabourHourlyRatePriority2"
        txtLabourRates(2).Tag = "LabourHourlyRatePriority3"
        txtLabourRates(3).Tag = "LabourMinCharge"
        txtLabourRates(4).Tag = "ServiceNotificationCostLimit"

        '==3311.329=  New-  LabourRate Stock barcodes..
        txtLabourP1Barcode.Tag = "LabourRateP1RetailBarcode"
        txtLabourP2Barcode.Tag = "LabourRateP2RetailBarcode"
        txtLabourP3Barcode.Tag = "LabourRateP3RetailBarcode"
        txtLabourOnSiteP1Barcode.Tag = "LabourRateOnSiteP1RetailBarcode"
        txtLabourOnSiteP2Barcode.Tag = "LabourRateOnSiteP2RetailBarcode"
        txtLabourOnSiteP3Barcode.Tag = "LabourRateOnSiteP3RetailBarcode"

        txtPriority(0).Tag = "DescriptionPriority1"
        txtPriority(1).Tag = "DescriptionPriority2"
        txtPriority(2).Tag = "DescriptionPriority3"

        txtServiceCategory(0).Tag = "StockServiceChargeCat1"
        txtServiceCategory(1).Tag = "StockServiceChargeCat2"

        txtChassisCat(0).Tag = "QuoteChassisCat1"
        txtChassisCat(1).Tag = "QuoteChassisCat2"

        txtUserTexts(0).Tag = "TermsAndConditions"
        txtUserTexts(1).Tag = "NewJobDocketFootnote"
        txtUserTexts(2).Tag = "DeliveryDocketFootnote"
        '--ServiceChargesInfoText-
        txtUserTexts(3).Tag = "ServiceChargesInfoText"

        txtBarcodeFontName.Tag = "ItemBarcodeFontName"
        txtBarcodeFontSize.Tag = "ItemBarcodeFontSize"

        txtServerBackupFolder(0).Tag = "ServerShareLocalPath"
        txtServerBackupFolder(1).Tag = "ServerShareNetworkPath"

        '== 3067.0 ==
        '== s1 = Dir(msAppPath & "JobMatix3.chm")
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-SetupJobs.htm")
        End If

        '=3311.329=  New Labour Rates Stuff.
        '--  Labour Rates now in MYOB/POS..
        '--  WE keep stock barcodes..
        panelOldLabourRates.Enabled = False

        txtLabourP1Barcode.Text = ""
        txtLabourP2Barcode.Text = ""
        txtLabourP3Barcode.Text = ""
        txtLabourOnSiteP1Barcode.Text = ""
        txtLabourOnSiteP2Barcode.Text = ""
        txtLabourOnSiteP3Barcode.Text = ""

        txtLabourP1Barcode.ReadOnly = True  '-Forces browse..
        txtLabourP2Barcode.ReadOnly = True  '-Forces browse..
        txtLabourP3Barcode.ReadOnly = True  '-Forces browse..
        txtLabourOnSiteP1Barcode.ReadOnly = True  '-Forces browse..
        txtLabourOnSiteP2Barcode.ReadOnly = True  '-Forces browse..
        txtLabourOnSiteP3Barcode.ReadOnly = True  '-Forces browse..

        labP1Description.Text = ""
        labP2Description.Text = ""
        labP3Description.Text = ""
        labOnSiteP1Description.Text = ""
        labOnSiteP2Description.Text = ""
        labOnSiteP3Description.Text = ""

        labP1Rate.Text = ""
        labP2Rate.Text = ""
        labP3Rate.Text = ""
        labOnSiteP1Rate.Text = ""
        labOnSiteP2Rate.Text = ""
        labOnSiteP3Rate.Text = ""

        panelHourlyRateDefsWorkshop.Enabled = False   '--can re-enable for Updating (not create)

        '--  NOW all done..---
        Me.mbIsInitialising = False

    End Sub '--load-
    '= = = = = = = = =
    '-===FF->

    '--  A c t i v a t e --
    '--  A c t i v a t e --

    'UPGRADE_WARNING: Form event frmSetupJobsDB.Activate has a new behavior. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'

    Private Sub frmSetupJobsDB_Activated(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbStarted Then Exit Sub
        mbStarted = True

    End Sub  '-activated.
    '= = = = = = = = = = = == = =

    Private Sub frmSetupJobsDB_Shown(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        Dim col1 As Collection
        Dim s1, sVersion As String
        Dim L1 As Integer

        '= If mbStarted Then Exit Sub
        '= mbStarted = True
        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"

        sVersion = CStr(My.Application.Info.Version.Major) & "." & _
                               My.Application.Info.Version.Minor & ". Build " & _
                                My.Application.Info.Version.Build & "." & _
                                    My.Application.Info.Version.Revision & "."
        LabVersion.Text = "JobMatix- V:" & sVersion

        '--Find local ComputerName as server default--
        L1 = gsRegQueryValue(HKEY_LOCAL_MACHINE, _
                             "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName", "ComputerName", s1)
        If L1 = 0 Then
            '--MsgBox "Local ComputerName is :" + vbCrLf + s1
            msComputerName = s1
        Else
            MsgBox("Can't find computer name..  Reg error: " & L1)
        End If
        '--load login form and get login--
        '---Also gives us Create Mode if trye--
        Me.Text = "JobMatix Setup-  V:" & sVersion & "     (This W/s: " & msComputerName & ")"
        mbRunningOnServer = (UCase(msSqlServerComputer) = UCase(msComputerName))

        If mbRunningOnServer Then
            panelBackupPaths.Enabled = True  '=frameBackupPath.Enabled = True
            txtServerBackupFolder(0).Enabled = True
            txtServerBackupFolder(0).ReadOnly = True '--force user to browse..--
            txtServerBackupFolder(1).Enabled = True
            cmdBrowsePath.Enabled = True
        End If
        SSTab1.SelectedIndex = 0 '--  always start on front panel..--

        If mbCreateNewDB Then '--CREATE--
            optRetailHost(0).Enabled = True
            '==3067.0== optRetailHost(1).Enabled = True
            '==3067.0== For now-  disable Quickbooks option..

            '-3411.0217=
            optBusinessTypeOther.Checked = True

            '-- 3101-  opt(1) is now JobMatixPOS.--
            '==3311.328= _optRetailHost_1.Enabled = True
            optRetailHost(0).Checked = False  '--RM--
            optRetailHost(1).Checked = False  '--JMPOS--

            '==3311.328=  caller has chosen RM or POS--
            _optRetailHost_0.Enabled = False
            _optRetailHost_1.Enabled = False
            If mbIsCreatingPOSDB Then
                optRetailHost(1).Checked = True
                msRetailHostName = "JobMatixPOS"
                Call mbSetupJMPOS()
            Else
                optRetailHost(0).Checked = True  '-RM..
                msRetailHostName = "RetailManager"
                Call mbSetupRM()
            End If
            labHostName.Text = msRetailHostName
            frameCategories.Enabled = True

            '- end new choice.

            labHdr2.Text = "Creating New Database"
            LabHdr3.Text = "JobMatix DB Business Settings"
            LabHdr3.ForeColor = System.Drawing.ColorTranslator.FromOle(&HFF0000) '--blue.-

            txtPriority(0).Text = "1. Normal Speed"
            txtPriority(1).Text = "2. Light Speed"
            txtPriority(2).Text = "3. Ludicrous Speed"

            '=3431.0527= 27May2018=
            txtLabourRates(0).Text = "1.00"
            txtLabourRates(1).Text = "1.00"
            txtLabourRates(2).Text = "1.00"
            txtLabourRates(3).Text = "1.00"
            txtLabourRates(4).Text = "1.00"

            txtServiceCategory(0).Text = "SERVCE"
            txtServiceCategory(1).Text = ""
            txtChassisCat(0).Text = "MOTHER"
            txtChassisCat(0).Text = ""

            txtBarcodeFontSize.Text = "9" '--default.-

            '--  get standard terms and conditions..--
            txtUserTexts(0).Text = gsGetTermsAndConditions()
            txtUserTexts(1).Text = " We will contact you when Job is ready.. " & _
                                     "Please note that the account is payable on collection." '--new job..-
            txtUserTexts(2).Text = "We will cover all work on this service for 30 days from date of invoice.." '--del. docket.-
            txtUserTexts(3).Text = ""  '--servicecharges info text.
            labStatus.Text = "Setting up business details.."
            '== txtFullName.Focus()

        Else '--  updating SystemInfo..-

            '-3411.0217=
            optBusinessTypeOther.Enabled = False
            optBusinessTypeComputer.Enabled = False

            labRetailHost.Enabled = False

            txtNewDBName.Text = msSqlDbName '--do this BEFORE ShortName..
            txtNewDBName.ReadOnly = True

            cmdOK.Text = "Save..."
            LabDatabaseName.Text = "JobMatix Database Name:"
            '--retrieve SystemInfo and load text boxes..--
            labHdr2.Text = "Updating JobMatix Setup Information.."
            labStatus.Text = "Certain Business details can not be changed..."
            LabWarnPostcode.Text = "State and Postcode can't be changed."
            labShortName.Text = vbCrLf & "Business Short-name"

            txtShortName.ReadOnly = True
            txtPostCode.ReadOnly = True
            txtShortName.ReadOnly = True
            txtABN.ReadOnly = True

            cboState.Visible = False

            txtState.Visible = True
            txtState.ReadOnly = True
            txtUserLicence.ReadOnly = False
            frameABN.Enabled = True

            frameCategories.Enabled = True

            '-- get current system info..-
            '==3311.329=
            If Not (mRetailHost1 Is Nothing) Then
                panelHourlyRateDefsWorkshop.Enabled = True  '- now have retail host..
            End If  '- nothing-

            If mbLoadsystemInfo(mCnnSql, mColSystemInfo, mSdSystemInfo) Then '-- get all system info..--
                For Each col1 In mColSystemInfo
                    If col1.Item("systemkey") = "JT2SECURITYID" Then '--user created at CREATE time..
                        '==msJT2SecurityIdOriginal = col1("systemvalue")
                        mDateCreated = CDate(col1.Item("datecreated"))
                    ElseIf col1.Item("systemkey") = "BUSINESSNAME" Then
                        txtFullName.Text = col1.Item("SystemValue")
                    ElseIf col1.Item("systemkey") = "BUSINESSSHORTNAME" Then
                        txtShortName.Text = col1.Item("SystemValue")
                    ElseIf col1.Item("systemkey") = "BUSINESSPOSTCODE" Then
                        txtPostCode.Text = col1.Item("SystemValue")
                    ElseIf col1.Item("systemkey") = "BUSINESSSTATE" Then
                        txtState.Text = col1.Item("SystemValue")
                    ElseIf col1.Item("systemkey") = "BUSINESSABN" Then
                        txtABN.Text = col1.Item("SystemValue")
                    End If '--key..-
                Next col1 '--col1-
            Else '-no info..-
                Me.Close()
                Exit Sub '=End
            End If

            '--  load current info..-
            '--   There is no "Exists" method for collections..  !!!
            If mSdSystemInfo.Exists("RETAILHOSTNAME") Then _
                               msRetailHostName = CStr(mColSystemInfo.Item("RetailHostName")("SystemValue"))

            '== txtFullName.Text = mColSystemInfo("BusinessName")("SystemValue")
            If mSdSystemInfo.Exists("BUSINESSADDRESS1") Then _
                                                     txtAddress1.Text = mColSystemInfo.Item("BusinessAddress1")("SystemValue")
            If mSdSystemInfo.Exists("BUSINESSADDRESS2") Then _
                                                 txtAddress2.Text = mColSystemInfo.Item("BusinessAddress2")("SystemValue")
            '===  FIX  !!  cboState.Tag = "BusinessState"

            '==txtPostCode.Tag = "BusinessPostCode"  '--won't be updating this..-
            If mSdSystemInfo.Exists("BUSINESSPHONE") Then _
                                                   txtPhone.Text = mColSystemInfo.Item("BusinessPhone")("SystemValue")
            '=3203.117=
            If mSdSystemInfo.Exists("BUSINESSEMAIL") Then _
                                                   txtEmail.Text = mColSystemInfo.Item("BusinessEmail")("SystemValue")

            If mSdSystemInfo.Exists("LICENCEKEY") Then
                txtUserLicence.Text = mColSystemInfo.Item("LicenceKey")("SystemValue")
                msOriginalLicence = txtUserLicence.Text '---save..-
            End If
            If mSdSystemInfo.Exists("GSTPERCENTAGE") Then _
                             txtGSTPercentage.Text = mColSystemInfo.Item("GSTPercentage")("SystemValue")

            '==3311.329=  labour Rate Barcodes..
            If mSdSystemInfo.Exists("LABOURRATEP1RETAILBARCODE") Then _
                          txtLabourP1Barcode.Text = mColSystemInfo.Item("LabourRateP1RetailBarcode")("SystemValue")
            If mSdSystemInfo.Exists("LABOURRATEP2RETAILBARCODE") Then _
                          txtLabourP2Barcode.Text = mColSystemInfo.Item("LabourRateP2RetailBarcode")("SystemValue")
            If mSdSystemInfo.Exists("LABOURRATEP3RETAILBARCODE") Then _
                          txtLabourP3Barcode.Text = mColSystemInfo.Item("LabourRateP3RetailBarcode")("SystemValue")
            If mSdSystemInfo.Exists("LABOURRATEONSITEP1RETAILBARCODE") Then _
                          txtLabourOnSiteP1Barcode.Text = mColSystemInfo.Item("LabourRateOnSiteP1RetailBarcode")("SystemValue")
            If mSdSystemInfo.Exists("LABOURRATEONSITEP2RETAILBARCODE") Then _
                          txtLabourOnSiteP2Barcode.Text = mColSystemInfo.Item("LabourRateOnSiteP2RetailBarcode")("SystemValue")
            If mSdSystemInfo.Exists("LABOURRATEONSITEP3RETAILBARCODE") Then _
                          txtLabourOnSiteP3Barcode.Text = mColSystemInfo.Item("LabourRateOnSiteP3RetailBarcode")("SystemValue")
            '-- done-

            If mSdSystemInfo.Exists("LABOURHOURLYRATEPRIORITY1") Then _
                             txtLabourRates(0).Text = mColSystemInfo.Item("LabourHourlyRatePriority1")("SystemValue")
            If mSdSystemInfo.Exists("LABOURHOURLYRATEPRIORITY2") Then _
                                  txtLabourRates(1).Text = mColSystemInfo.Item("LabourHourlyRatePriority2")("SystemValue")
            If mSdSystemInfo.Exists("LABOURHOURLYRATEPRIORITY3") Then _
                                  txtLabourRates(2).Text = mColSystemInfo.Item("LabourHourlyRatePriority3")("SystemValue")
            If mSdSystemInfo.Exists("LABOURMINCHARGE") Then _
                                                txtLabourRates(3).Text = mColSystemInfo.Item("LabourMinCharge")("SystemValue")
            '=3203.211-  ENFORCE-
            chkNoEnforceMinCharge.Checked = False
            If mSdSystemInfo.Exists("ENFORCE_MINIMUM_CHARGE") Then
                If (UCase(mColSystemInfo.Item("Enforce_Minimum_Charge")("SystemValue")) = "Y") Then
                    chkNoEnforceMinCharge.Checked = True
                End If
            End If  '-enforce..-

            If mSdSystemInfo.Exists("SERVICENOTIFICATIONCOSTLIMIT") Then _
                                   txtLabourRates(4).Text = mColSystemInfo.Item("ServiceNotificationCostLimit")("SystemValue")

            If mSdSystemInfo.Exists("DESCRIPTIONPRIORITY1") Then _
                                            txtPriority(0).Text = mColSystemInfo.Item("DescriptionPriority1")("SystemValue")
            If mSdSystemInfo.Exists("DESCRIPTIONPRIORITY2") Then _
                                               txtPriority(1).Text = mColSystemInfo.Item("DescriptionPriority2")("SystemValue")
            If mSdSystemInfo.Exists("DESCRIPTIONPRIORITY3") Then _
                                             txtPriority(2).Text = mColSystemInfo.Item("DescriptionPriority3")("SystemValue")

            If mSdSystemInfo.Exists("STOCKSERVICECHARGECAT1") Then _
                                   txtServiceCategory(0).Text = mColSystemInfo.Item("StockServiceChargeCat1")("SystemValue")
            If mSdSystemInfo.Exists("STOCKSERVICECHARGECAT2") Then _
                                   txtServiceCategory(1).Text = mColSystemInfo.Item("StockServiceChargeCat2")("SystemValue")

            If mSdSystemInfo.Exists("QUOTECHASSISCAT1") Then _
                                              txtChassisCat(0).Text = mColSystemInfo.Item("QuoteChassisCat1")("SystemValue")
            If mSdSystemInfo.Exists("QUOTECHASSISCAT2") Then _
                                               txtChassisCat(1).Text = mColSystemInfo.Item("QuoteChassisCat2")("SystemValue")

            If mSdSystemInfo.Exists("TERMSANDCONDITIONS") Then _
                                            txtUserTexts(0).Text = mColSystemInfo.Item("TermsAndConditions")("SystemValue")
            If mSdSystemInfo.Exists("NEWJOBDOCKETFOOTNOTE") Then _
                                             txtUserTexts(1).Text = mColSystemInfo.Item("NewJobDocketFootnote")("SystemValue")
            If mSdSystemInfo.Exists("DELIVERYDOCKETFOOTNOTE") Then _
                                           txtUserTexts(2).Text = mColSystemInfo.Item("DeliveryDocketFootnote")("SystemValue")
            If mSdSystemInfo.Exists("SERVICECHARGESINFOTEXT") Then _
                                           txtUserTexts(3).Text = mColSystemInfo.Item("ServiceChargesInfoText")("SystemValue")

            If mSdSystemInfo.Exists("ITEMBARCODEFONTNAME") Then _
                                           txtBarcodeFontName.Text = mColSystemInfo.Item("ItemBarcodeFontName")("SystemValue")
            If mSdSystemInfo.Exists("ITEMBARCODEFONTSIZE") Then _
                                            txtBarcodeFontSize.Text = mColSystemInfo.Item("ItemBarcodeFontSize")("SystemValue")

            If mSdSystemInfo.Exists("SERVERSHARELOCALPATH") Then _
                                     txtServerBackupFolder(0).Text = mColSystemInfo.Item("ServerShareLocalPath")("SystemValue")
            If mSdSystemInfo.Exists("SERVERSHARENETWORKPATH") Then _
            txtServerBackupFolder(1).Text = mColSystemInfo.Item("ServerShareNetworkPath")("SystemValue")

        End If '-new/update..-
        '--  clear the update list.. --
        '----- (stuff was added by all the text box loading..)
        mSdUpdatedInfo = New clsStrDictionary   '== Scripting.Dictionary

        '--  NB-- Pre-Existing (V2) databases may not have "RetailHost" systemInfo entry..
        '--  WE ASSUME they are RetailManager sites..--
        If Not mbCreateNewDB Then '--is update--
            '-- force retailmanager if not specified..
            If (msRetailHostName = "") Then  '--Must be JobMatix 3.0 DB ==
                msRetailHostName = k_RM_NAME
                '--  add to info..-
                mSdUpdatedInfo("RETAILHOSTNAME") = msRetailHostName
                optRetailHost(0).Checked = True '--RM--
                mbDataChanged = True
                MsgBox("Please note:" & vbCrLf & "No Retail-host system was set up in this DB.." & vbCrLf & _
                            "The setup will be changed to the default of MYOB ""RetailManager""...", MsgBoxStyle.Information)
            Else '- is set.  set option for display..--
                If (LCase(msRetailHostName) = k_RM_NAME) Then
                    optRetailHost(0).Checked = True
                ElseIf (LCase(msRetailHostName) = k_JMPOS_NAME) Then  '=3101- Now JobMatixPOS=
                    optRetailHost(1).Checked = True
                End If
            End If '-host-
            If mbIsJMPOS() Then
                Call mbSetupJMPOS()
            Else
                Call mbSetupRM()
            End If
            labHostName.Text = msRetailHostName
            txtFullName.SelectionStart = txtFullName.TextLength
            txtFullName.SelectionLength = 0
        End If '--update.-
        cmdOK.Enabled = False '--was enable by all the text box loading..
        mbDataChanged = False
        cmdRestoreTerms.Enabled = False
        txtFullName.Focus()

    End Sub '--SHOWN.  -Activate-
    '= = = = = = = = =
    '-===FF->

    '--  N E W  DB Stuff only..-
    '--  N E W  DB Stuff only..-
    '--  new DB Stuff only..-

    '--validate postcode.--
    Private Sub cboState_Validating(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                  Handles cboState.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel

        If cboState.SelectedIndex < 0 Then '--nothing selected..-
            keepfocus = True
        End If
        eventArgs.Cancel = keepfocus
    End Sub '--state..--
    '= = = = = = = = = =

    '-- Postcode..-

    'UPGRADE_WARNING: Event txtPostCode.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtPostCode_TextChanged(ByVal eventSender As System.Object, _
                                                        ByVal eventArgs As System.EventArgs) Handles txtPostCode.TextChanged

        If mbIsInitialising Then Exit Sub

        Call mbCheckMaxText(txtPostCode, 4)
        txtPostCode.Text = Trim(txtPostCode.Text)
    End Sub '-- change..-
    '= = = = = = = =  = ==

    '--validate postcode.--
    Private Sub txtPostCode_Validating(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPostCode.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String

        If Not mbCreateNewDB Then
            GoTo EventExitSub
        Else '--new-
            s1 = txtPostCode.Text
            If (Len(s1) < 3) Then
                MsgBox("Post Code is too short..", MsgBoxStyle.Exclamation)
                keepfocus = True
            ElseIf (Not IsNumeric(s1)) Then
                MsgBox("Post Code must be numeric only..", MsgBoxStyle.Exclamation)
                keepfocus = True
            Else '--ok.-
                '=3101= Wait for RetailHost Selection-  
                '==3311.328=  can go now..=
                frameABN.Enabled = True
            End If
        End If '-new-

EventExitSub:
        eventArgs.Cancel = keepfocus
    End Sub '--postcode.-
    '= = = = = =  = = = = = =  =

    '-- - select RetailHost_click ==

    'UPGRADE_WARNING: Event optRetailHost.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub optRetailHost_CheckedChanged(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs)
        '=3311.328= Handles optRetailHost.CheckedChanged

        If mbIsInitialising Then Exit Sub
        If Not mbCreateNewDB Then
            Exit Sub
        End If
        '-- new-
        labPOSWarning.Enabled = True
        If eventSender.Checked Then
            Dim index As Short = optRetailHost.GetIndex(eventSender)

            msRetailHostName = optRetailHost(index).Tag
            frameCategories.Enabled = True
        End If
        If mbIsJMPOS() Then
            Call mbSetupJMPOS()
        Else
            labPOSWarning.Enabled = False
            Call mbSetupRM()
        End If
        labHostName.Text = msRetailHostName
        frameABN.Enabled = True

    End Sub '--RetailHost_click--
    '-===FF->

    '-- Business Short Name..-
    'UPGRADE_WARNING: Event txtShortName.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtShortName_TextChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles txtShortName.TextChanged
        Dim L1 As Integer

        If mbIsInitialising Then Exit Sub
        If Not mbCreateNewDB Then Exit Sub
        Call mbCheckMaxText(txtShortName, 24)

        txtNewDBName.Text = ""

        '--  Spaces permitted..  they will be replaced by UNDERSCOREs to create DB-Name..-

        If (msBusinessABN = txtABN.Text) Then
            If gbCheckValidABN(msBusinessABN, L1) Then
                '==FrameLogin.Enabled = True    '--can jump over..-
            End If
        End If

    End Sub '--change..-
    '= = = = = = = = = =

    '-- Business Short Name..-
    Private Sub txtShortName_Validating(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                     Handles txtShortName.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim vName As Object
        Dim s1, sSuffix As String
        Dim ok As Boolean
        Dim ix As Integer

        '--txtShortName.Text = Replace(txtShortName.Text, " ", "") '--delete blanks..-
        '--check against list of current jobTracking DB's--
        If Not mbCreateNewDB Then GoTo EventExitSub
        msBusinessShortName = Trim(txtShortName.Text)
        ok = True

        If (msBusinessShortName <> "") Then
            '-- check if alphanumeric.--)
            s1 = UCase(msBusinessShortName)
            For ix = 1 To Len(msBusinessShortName)
                If Not (InStr(msValidChars, Mid(s1, ix, 1)) > 0) Then
                    ok = False
                    Exit For
                End If
            Next ix
            If ok Then
                '-- replace all multiple spaces by single spaces..-
                While (InStr(msBusinessShortName, "  ") > 0)
                    msBusinessShortName = Replace(msBusinessShortName, "  ", " ") '--replace dbl space with single..--
                End While
                msBusinessShortName = Replace(msBusinessShortName, " ", "_") '--replace remaining spaces with u-score..--
                If mbIsJMPOS() Then
                    sSuffix = "_jmpos"
                    txtNewDBName.Text = msBusinessShortName & "_" & "jmpos"
                Else
                    sSuffix = "_JobTracking"
                    txtNewDBName.Text = msBusinessShortName & "_" & "JobTracking"
                End If
                For Each vName In mColDBNames
                    If LCase(vName) = LCase(msBusinessShortName) & sSuffix Then '= "_jmpos" Then
                        If MsgBox("That ShortName already exists as the Database: " & vbCrLf & _
                                   Trim(vName) & ".." & vbCrLf & "If you continue, the database will be deleted " & vbCrLf & _
                                " and replaced by the new database." & vbCrLf & vbCrLf & _
                                "Do you want to continue with this ShortName? ", _
                               MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                            ok = False '--keep focus..-
                            msBusinessShortName = ""
                        End If
                        Exit For
                    End If
                Next vName '--name--
            End If '--ok..-
        Else
            ok = False
            '== MsgBox("Must have Business Short Name..", MsgBoxStyle.Exclamation)
        End If '--null-
        If Not ok Then
            txtNewDBName.Text = ""
            keepfocus = True
            MsgBox("Business Short Name is missing, or has invalid characters..", MsgBoxStyle.Exclamation)
        Else '--ok..-
            '==FrameLogin.Enabled = True
            labStatus.Text = ""
        End If
EventExitSub:
        eventArgs.Cancel = keepfocus
    End Sub '-- Short Name--
    '= = = = = = = = =
    '-===FF->

    '--  ABN --
    '--  ABN --

    'UPGRADE_WARNING: Event txtABN.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtABN_TextChanged(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles txtABN.TextChanged
        Dim L1 As Integer

        If mbIsInitialising Then Exit Sub
        '-- check ABN.-
        If Not mbCreateNewDB Then Exit Sub

        Call mbCheckMaxText(txtABN, 24)
        msBusinessABN = Replace(txtABN.Text, " ", "")
        '== If Not gbCheckValidABN(msBusinessABN, L1) Then
        '= Else '--ok-- 
        '= cmdOK.Enabled = True
        '= End If '--check--
    End Sub '-- abn--
    '= = = = = = = = =

    '-- check ABN.-

    Private Sub txtABN_Validating(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtABN.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim L1 As Integer

        If Not mbCreateNewDB Then GoTo EventExitSub
        msBusinessABN = Replace(txtABN.Text, " ", "")
        If Not IsNumeric(msBusinessABN) Then
            MsgBox("ABN must be numeric only..", MsgBoxStyle.Exclamation)
            keepfocus = True
        Else '-num ok-
            If Not gbCheckValidABN(msBusinessABN, L1) Then
                MsgBox("ABN supplied is not a valid Australian Business Number..", MsgBoxStyle.Exclamation)
                '==FrameLogin.Enabled = False
                keepfocus = True
            Else '--ok--
                cmdOK.Enabled = True
            End If '--check--
        End If

EventExitSub:
        eventArgs.Cancel = keepfocus
    End Sub '-- abn--
    '= = = = = = = = =
    '-===FF->
    '--  U p d a t e s --
    '--  U p d a t e s --

    '--  This stuff can be updated..-
    '--  This stuff can be updated..-

    'UPGRADE_WARNING: Event txtFullName.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFullName_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles txtFullName.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtFullName, 50)
        '==mSdUpdatedInfo(txtFullName.Tag) = txtFullName.Text
        Call mbSaveUpdatedText(txtFullName)
    End Sub '--name..-
    '= = = = = = = =

    'UPGRADE_WARNING: Event txtAddress1.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtAddress1_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtAddress1.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtAddress1, 50)
        Call mbSaveUpdatedText(txtAddress1)
    End Sub '--address..-
    '= = = = = = = =

    Private Sub txtAddress2_TextChanged(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles txtAddress2.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtAddress2, 50)
        Call mbSaveUpdatedText(txtAddress2)
    End Sub '--address..-
    '= = = = = = = =

    Private Sub txtPhone_TextChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles txtPhone.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtPhone, 24)
        Call mbSaveUpdatedText(txtPhone)
    End Sub '--Phone..-
    '= = = = = = = =

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtEmail, 127)
        Call mbSaveUpdatedText(txtEmail)

    End Sub  '-email-
    '= = = = = = = = = = =  =
    '-===FF->

    '-- First Panel..- "ENTER" key on Name and address...-

    Private Sub txtFullName_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                              Handles txtFullName.KeyPress, txtAddress1.KeyPress, txtAddress2.KeyPress, _
                                                         txtPostCode.KeyPress, txtPhone.KeyPress, txtEmail.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txt1 As TextBox = CType(eventSender, TextBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '== MsgBox("ENTER key on : " & txt1.Name, MsgBoxStyle.Information)
            txt1.Parent.SelectNextControl(ActiveControl, True, True, True, False)

            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--FullName_KeyPress--
    '= = = = = = = = = = = = = =  =

    '-- "ENTER" key on cbostate...-

    Private Sub cboState_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                    Handles cboState.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim cbo1 As ComboBox = CType(eventSender, ComboBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            cbo1.Parent.SelectNextControl(ActiveControl, True, True, True, False)
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--SerialNo_KeyPress--
    '= = = = = = = = = = =

    '-- "ENTER" key on Short Name..

    Private Sub txtShortName_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
            Handles txtShortName.KeyPress, txtABN.KeyPress, txtNewDBName.KeyPress, txtUserLicence.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txt1 As TextBox = CType(eventSender, TextBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txt1.Parent.SelectNextControl(ActiveControl, True, True, True, False)

            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--SerialNo_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    'UPGRADE_WARNING: Event txtUserLicence.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtUserLicence_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtUserLicence.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtUserLicence, 32)
        '===mSdUpdatedInfo(txtUserLicence.Tag) = txtUserLicence.Text
        Call mbSaveUpdatedText(txtUserLicence)

    End Sub '--licence..-
    '= = = = = = = = = =
    '-===FF->

    'UPGRADE_WARNING: Event txtGSTPercentage.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtGSTPercentage_TextChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles txtGSTPercentage.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtGSTPercentage, 6)
        '===mSdUpdatedInfo(txtGSTPercentage.Tag) = txtGSTPercentage.Text
        Call mbSaveUpdatedText(txtGSTPercentage)

    End Sub '-gst change..-
    '= = = = = = = = =  = =

    '-- validate GST..--
    Private Sub txtGSTPercentage_Validating(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                        Handles txtGSTPercentage.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String

        s1 = txtGSTPercentage.Text
        If (Len(s1) < 1) Then
            MsgBox("GST Percentage is too short..", MsgBoxStyle.Exclamation)
            keepfocus = True
        ElseIf (Not IsNumeric(s1)) Then
            MsgBox("GST Percentage must be numeric only..", MsgBoxStyle.Exclamation)
            keepfocus = True

        Else '--ok.-
        End If

        eventArgs.Cancel = keepfocus
    End Sub '--GST Percentage.-
    '= = = = = =  = = = = = =  =
    '-===FF->

    '-- Browse for stock Labour Rates..-

    Private Sub btnP1Browse_Click(sender As Object, ev As EventArgs) _
                                                Handles btnP1Browse.Click, _
                                                       btnP2Browse.Click, _
                                                       btnP3Browse.Click, _
                                                          btnOnSiteP1Browse.Click, _
                                                          btnOnSiteP2Browse.Click, _
                                                          btnOnSiteP3Browse.Click

        Dim buttonX As Button = CType(sender, Button)
        Dim sName As String = buttonX.Name

        If (_txtServiceCategory_0.Text) = "" Then
            MsgBox("Service Charge Categories 1/2 must be set up first.." & vbCrLf & _
                     " See previous panel (stock cat. defs)..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim sServiceChargeCat1 As String = Trim(_txtServiceCategory_0.Text)
        Dim sServiceChargeCat2 As String = Trim(_txtServiceCategory_1.Text)
        Dim sBarcode, sDescription, sSell As String
        Dim colSelectedRecord As Collection
        '--browse for labour rates in stock table-
        '--  MUST have RetailHost  interface..
        If Not (mRetailHost1 Is Nothing) Then
            '--retrieve selected record and fill in cust details..--
            If mRetailHost1.stockLookup(True, sServiceChargeCat1, sServiceChargeCat2, colSelectedRecord) Then

                '--  that gets full normal stock record .--
                If colSelectedRecord Is Nothing Then
                    '====Me.Hide
                    MsgBox("Nothing selected..", MsgBoxStyle.Exclamation)
                    Exit Sub
                Else '--ok. selection made..-
                    '-- get barcode..--
                    If (colSelectedRecord.Count() > 0) Then
                        sBarcode = colSelectedRecord.Item("barcode")("value")
                        sDescription = colSelectedRecord.Item("description")("value")
                        sSell = FormatCurrency(colSelectedRecord.Item("sell")("value"), 2)  '-- Sell is EX GST-
                        '-- set up correct priority rate..
                        Select Case LCase(sName)
                            Case "btnp1browse"
                                txtLabourP1Barcode.Text = sBarcode
                                labP1Description.Text = sDescription
                                labP1Rate.Text = sSell
                                Call mbSaveUpdatedText(txtLabourP1Barcode)
                            Case "btnp2browse"
                                txtLabourP2Barcode.Text = sBarcode
                                labP2Description.Text = sDescription
                                labP2Rate.Text = sSell
                                Call mbSaveUpdatedText(txtLabourP2Barcode)
                            Case "btnp3browse"
                                txtLabourP3Barcode.Text = sBarcode
                                labP3Description.Text = sDescription
                                labP3Rate.Text = sSell
                                Call mbSaveUpdatedText(txtLabourP3Barcode)
                            Case "btnonsitep1browse"   '-onSite Priority-1.-
                                txtLabourOnSiteP1Barcode.Text = sBarcode
                                labOnSiteP1Description.Text = sDescription
                                labOnSiteP1Rate.Text = sSell
                                Call mbSaveUpdatedText(txtLabourOnSiteP1Barcode)
                            Case "btnonsitep2browse"   '-onSite Priority-1.-
                                txtLabourOnSiteP2Barcode.Text = sBarcode
                                labOnSiteP2Description.Text = sDescription
                                labOnSiteP2Rate.Text = sSell
                                Call mbSaveUpdatedText(txtLabourOnSiteP2Barcode)
                            Case "btnonsitep3browse"   '-onSite Priority-1.-
                                txtLabourOnSiteP3Barcode.Text = sBarcode
                                labOnSiteP3Description.Text = sDescription
                                labOnSiteP3Rate.Text = sSell
                                Call mbSaveUpdatedText(txtLabourOnSiteP3Barcode)
                            Case Else
                                Exit Sub
                        End Select
                    End If  '-count-
                End If '-no record.
            Else '--lookup cancelled..
                Exit Sub
            End If  '--lookup-
        End If  '-nothing- 
    End Sub  '-btnP1Browse_Click-
    '= = = = = =  = = = = = =  =
    '-===FF->

    '-- Hourly Rate (0) and Minimum Charge (1) --
    '== 3311.329==
    '--  Now used only for _txtLabourRates_3 (MinCharge) and _txtLabourRates_4 (Limit)

    Private Sub txtLabourRates_TextChanged(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles txtLabourRates.TextChanged
        Dim s1 As String

        If mbIsInitialising Then Exit Sub
        Dim index As Short = txtLabourRates.GetIndex(eventSender)

        '==3311.329=
        If (index < 3) Then Exit Sub '--Rates P1,P2,P3 are vdecrecated-

        Call mbCheckMaxText(txtLabourRates(index), 7)

        s1 = txtLabourRates(index).Text
        If Not IsNumeric(s1) Then
            MsgBox("Numeric field only..", MsgBoxStyle.Exclamation)
            txtLabourRates(index).SelectionStart = 0
            txtLabourRates(index).SelectionLength = Len(txtLabourRates(index).Text)
        Else '--ok.-
            '--  save update..-
            '==mSdUpdatedInfo(txtLabourRates(index).Tag) = txtLabourRates(index).Text
            Call mbSaveUpdatedText(txtLabourRates(index))
            If index = 3 Then '-min charge-
                chkNoEnforceMinCharge.Enabled = True
            End If
        End If '--numeric..-

    End Sub '-- Rates.-
    '= = = = = = = = =

    '--validate.--
    Private Sub txtLabourRates_Validating(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                    Handles txtLabourRates.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim index As Short = txtLabourRates.GetIndex(eventSender)
        Dim s1 As String

        '==3311.329=
        If (index < 3) Then Exit Sub '--Rates P1,P2,P3 are vdecrecated-

        s1 = txtLabourRates(index).Text
        If Not IsNumeric(s1) Then
            MsgBox("Numeric field only..", MsgBoxStyle.Exclamation)
            txtLabourRates(index).SelectionStart = 0
            txtLabourRates(index).SelectionLength = Len(txtLabourRates(index).Text)
            keepfocus = True
        End If '--numeric..-
        eventArgs.Cancel = keepfocus
    End Sub '-- Rates.-
    '= = = = = = = = =

    '=3203.211-  -- Do not Enforce Min Charge.

    Private Sub chkNoEnforceMinCharge_CheckedChanged(sender As Object, e As EventArgs) _
                                                          Handles chkNoEnforceMinCharge.CheckedChanged
        mbDataChanged = True
        '==mSdUpdatedInfo(txt1.Tag) = txt1.Text
        '-- Add to list of changes..
        '== WRONG= mSdUpdatedInfo("ENFORCE_MINIMUM_CHARGE") = IIf(chkNoEnforceMinCharge.Checked, "Y", "N")
        '= 3203.218=  This is right:
        mSdUpdatedInfo("ENFORCE_MINIMUM_CHARGE") = IIf(chkNoEnforceMinCharge.Checked, "N", "Y")

        If Not mbCreateNewDB Then cmdOK.Enabled = True '--can SAVE if updating..--

    End Sub '-- chkNoEnforceMinCharge-
    '= = = = = = =  = = = = = = = = = = = = =
    '= = = = = =  = = = = = =  =
    '-===FF->

    '--priority descr.--
    '--priority descr.--

    'UPGRADE_WARNING: Event txtPriority.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtPriority_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtPriority.TextChanged

        If mbIsInitialising Then Exit Sub
        Dim index As Short = txtPriority.GetIndex(eventSender)

        Call mbCheckMaxText(txtPriority(index), 24)
        '===mSdUpdatedInfo(txtPriority(index).Tag) = txtPriority(index).Text
        Call mbSaveUpdatedText(txtPriority(index))

    End Sub '--priority descr.--
    '= = = = =  = = = = = = = = = = 
    '-===FF->

    '-- GST/LabourRates Panel..- "ENTER" key...-

    Private Sub txtGSTPanel_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
         Handles txtGSTPercentage.KeyPress, _txtLabourRates_0.KeyPress, _txtLabourRates_1.KeyPress, _
                   _txtLabourRates_2.KeyPress, _txtLabourRates_3.KeyPress, _txtLabourRates_4.KeyPress, _
                                       _txtPriority_0.KeyPress, _txtPriority_1.KeyPress, _txtPriority_2.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txt1 As TextBox = CType(eventSender, TextBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txt1.Parent.SelectNextControl(ActiveControl, True, True, True, False)
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--SerialNo_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    '--Categories..--
    '--Categories..--
    '--  NB: Quickbooks Categories max 50..--

    'UPGRADE_WARNING: Event txtServiceCategory.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtServiceCategory_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles txtServiceCategory.TextChanged

        If mbIsInitialising Then Exit Sub

        Dim index As Short = txtServiceCategory.GetIndex(eventSender)

        Call mbCheckMaxText(txtServiceCategory(index), 50)
        '== mSdUpdatedInfo(txtServiceCategory(index).Tag) = txtServiceCategory(index).Text
        Call mbSaveUpdatedText(txtServiceCategory(index))
    End Sub '--service cat..-
    '= = = = = = = = = =

    'UPGRADE_WARNING: Event txtChassisCat.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtChassisCat_TextChanged(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles txtChassisCat.TextChanged
        If mbIsInitialising Then Exit Sub
        Dim index As Short = txtChassisCat.GetIndex(eventSender)

        Call mbCheckMaxText(txtChassisCat(index), 50)
        '=== mSdUpdatedInfo(txtChassisCat(index).Tag) = txtChassisCat(index).Text
        Call mbSaveUpdatedText(txtChassisCat(index))
    End Sub '--chassis cat..-
    '= = = = =  =

    '--STOCK CATEGORIES Panel..- "ENTER" key...-

    Private Sub txtServiceCatPanel_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                              Handles _txtServiceCategory_0.KeyPress, _txtServiceCategory_1.KeyPress, _
                                                        _txtChassisCat_0.KeyPress, _txtChassisCat_1.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txt1 As TextBox = CType(eventSender, TextBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txt1.Parent.SelectNextControl(ActiveControl, True, True, True, False)
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--service Cat_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    '-- U s e r  T e x t s--
    '-- U s e r  T e x t s--

    'UPGRADE_WARNING: Event txtUserTexts.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtUserTexts_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtUserTexts.TextChanged
        If mbIsInitialising Then Exit Sub
        Dim index As Short = txtUserTexts.GetIndex(eventSender)
        Dim lngMaxLength As Integer

        lngMaxLength = 2000 '-- not terms..-
        If (index = 0) Then lngMaxLength = 2400 '--is terms..-

        Call mbCheckMaxText(txtUserTexts(index), lngMaxLength)

        '==  mSdUpdatedInfo(txtUserTexts(index).Tag) = txtUserTexts(index).Text
        Call mbSaveUpdatedText(txtUserTexts(index))
        If (index = 0) Then '--terms were changed..-
            cmdRestoreTerms.Enabled = True
        End If
    End Sub '--txtUserTexts--
    '= = = = = = = = =

    '--  restore standard terms..

    Private Sub cmdRestoreTerms_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdRestoreTerms.Click

        txtUserTexts(0).Text = gsGetTermsAndConditions()
        '==mSdUpdatedInfo(txtUserTexts(0).Tag) = txtUserTexts(0).Text
        Call mbSaveUpdatedText(txtUserTexts(0))
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    'UPGRADE_WARNING: Event txtBarcodeFontName.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtBarcodeFontName_TextChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) Handles txtBarcodeFontName.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtBarcodeFontName, 50)
        Call mbSaveUpdatedText(txtBarcodeFontName)
    End Sub
    '= = = = = = = = = =

    'UPGRADE_WARNING: Event txtBarcodeFontSize.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtBarcodeFontSize_TextChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles txtBarcodeFontSize.TextChanged

        If mbIsInitialising Then Exit Sub
        Call mbCheckMaxText(txtBarcodeFontSize, 3)

        Call mbSaveUpdatedText(txtBarcodeFontSize)
    End Sub
    '= = = = =  = = = =

    '--  Browse for Barcode FONT..--
    '--  Browse for Barcode FONT..--

    '--   SHOW CommonDialog for FONTS..

    Private Sub cmdBarcodeFont_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdBarcodeFont.Click
        '== Dim dlgFont1 As New FontDialog()
        Dim MyResult As System.Windows.Forms.DialogResult

        '-- Display the Font dialog box
        Try
            MyResult = dlg1Font.ShowDialog()
        Catch ex As Exception
            MsgBox("Can't load that Font!" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        If MyResult <> System.Windows.Forms.DialogResult.OK Then     '= If Err().Number <> 0 Then '--Cancelled==
            System.Windows.Forms.Cursor.Current = Cursors.Default
            Exit Sub
        End If

        txtBarcodeFontName.Text = dlg1Font.Font.Name
        txtBarcodeFontSize.Text = CStr(dlg1Font.Font.Size)

        Exit Sub

    End Sub '--barcode.-
    '= = = = = = = = = = = = =
    '-===FF->

    '--USER TEXTS Panel..- "ENTER" key...-

    Private Sub txtUserTextPanel_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                              Handles _txtUserTexts_0.KeyPress, _txtUserTexts_1.KeyPress, _
                                            _txtUserTexts_2.KeyPress, _txtUserTexts_3.KeyPress, _
                                                        txtBarcodeFontName.KeyPress, txtBarcodeFontSize.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txt1 As TextBox = CType(eventSender, TextBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txt1.Parent.SelectNextControl(ActiveControl, True, True, True, False)
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--usertext_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    '--B a c k u p path..
    '--B a c k u p path..
    '--  browse for LOCAL backup path..

    Private Sub cmdBrowsePath_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles cmdBrowsePath.Click
        Dim sPath As String
        Dim s1 As String
        Dim frmDirSearch1 As New frmDirSearch

        frmDirSearch1.FilePattern = "*.bak"
        frmDirSearch1.Top = (Me.Top + cmdBrowsePath.Top)
        frmDirSearch1.Left = (Me.Left + SSTab1.Left + (SSTab1.Width \ 2))
        frmDirSearch1.Text = "Searching for Backup Folder.."

        sPath = ""
        frmDirSearch1.ShowDialog()
        If frmDirSearch1.cancelled Then
            s1 = "Search Cancelled."
        Else
            sPath = frmDirSearch1.path
            s1 = "Path is: " & vbCrLf & frmDirSearch1.path
        End If
        frmDirSearch1.Close()
        MsgBox(s1, MsgBoxStyle.Information) '--testing..--

        If (sPath <> "") Then
            txtServerBackupFolder(0).Text = sPath
            '===mSdUpdatedInfo(txtServerBackupFolder(0).Tag) = sPath

            Call mbSaveUpdatedText(txtServerBackupFolder(0))
            txtServerBackupFolder(1).Focus()
        End If

    End Sub '--browse dir.-
    '= = = = = =  = = = = ==

    '--  check/save Backup share path..--

    Private Sub txtServerBackupFolder_Enter(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) Handles txtServerBackupFolder.Enter
        Dim index As Short = txtServerBackupFolder.GetIndex(eventSender)

        If index = 1 Then '--share path..--
            If CBool(Trim(CStr(txtServerBackupFolder(0).Text = ""))) Then
                MsgBox("Please set the local backup path first..", MsgBoxStyle.Exclamation)
                cmdBrowsePath.Focus()
                Exit Sub
            ElseIf CBool(Trim(CStr(txtServerBackupFolder(1).Text = ""))) Then
                txtServerBackupFolder(1).Text = "\\" & msComputerName & "\"
            Else
            End If
            txtServerBackupFolder(1).SelectionStart = 0
            txtServerBackupFolder(1).SelectionLength = Len(txtServerBackupFolder(1).Text)

        End If '--share--

    End Sub '--backup got focus..--
    '= = = = = = = = = = = = = =

    'UPGRADE_WARNING: Event txtServerBackupFolder.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtServerBackupFolder_TextChanged(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.EventArgs) _
                                                               Handles txtServerBackupFolder.TextChanged
        If mbIsInitialising Then Exit Sub
        Dim index As Short = txtServerBackupFolder.GetIndex(eventSender)

        '==  mSdUpdatedInfo(txtServerBackupFolder(1).Tag) = txtServerBackupFolder(1).Text
        Call mbSaveUpdatedText(txtServerBackupFolder(1))

    End Sub '--changee..--
    '= = = = = = = = = = = = =

    Private Sub txtServerBackupFolder_Validating(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                          Handles txtServerBackupFolder.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim index As Short = txtServerBackupFolder.GetIndex(eventSender)
        If index = 1 Then '--share path..--

            '--check SHARE path exists ???   --

        End If '--share..--
        eventArgs.Cancel = keepfocus
    End Sub '--validate..-
    '= = = = = = = = = = = =
    '-===FF->

    '--ServerBackup Panel..- "ENTER" key...-

    Private Sub txtServerBackupPanel_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                              Handles _txtServerBackupFolder_0.KeyPress, _txtServerBackupFolder_1.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txt1 As TextBox = CType(eventSender, TextBox)

        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txt1.Parent.SelectNextControl(ActiveControl, True, True, True, False)
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--usertext_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    '-- OK to CREATE  (or SAVE)..--
    '--  SUBROUTINE to add info item..--

    Private Sub mbCmdOK_AddInfoKey(ByVal sKey As String, _
                                    ByVal sValue As String, _
                                      ByRef colBusinessDetails As Collection)
        Dim col1 As Collection
        col1 = New Collection
        col1.Add(sValue, "value")
        col1.Add(sKey, "name")
        colBusinessDetails.Add(col1, sKey)
        '==Return
        col1 = Nothing
    End Sub '--AddInfoKey-
    '= = = = = = = = = = =

    '-- OK to CREATE  (or SAVE)..--
    '-- OK to CREATE  (or SAVE)..--

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        Dim s1, sErrorMsg As String
        Dim sKey, sValue As String
        Dim sCreateLogPath As String
        Dim sPOSmsg As String = ""
        Dim sDay, sSeconds As String
        Dim sState, sPostCode As String
        Dim sJT2SecurityId As String
        Dim dateCreated As DateTime
        Dim ok As Boolean
        Dim lErrors, L1 As Integer
        Dim col1 As Collection
        Dim colBusinessDetails As Collection
        Dim asChanges() As String '--update list..-

        If (txtFullName.Text = "") Or (txtAddress1.Text = "") Or (txtAddress2.Text = "") Then
            MsgBox("Business Name/Address details are not complete..", MsgBoxStyle.Exclamation)
            SSTab1.SelectedIndex = 0
            txtFullName.Focus()
            Exit Sub
        End If

        '=3203.117= Check Email.
        If Not (InStr(txtEmail.Text, "@") > 1) Then
            MsgBox("Business Email Address is not valid..", MsgBoxStyle.Exclamation)
            SSTab1.SelectedIndex = 0
            txtEmail.Focus()
            Exit Sub
        End If

        If (txtBarcodeFontName.Text <> "") And (txtBarcodeFontSize.Text = "") Then
            MsgBox("You must specify the Barcode font-size..", MsgBoxStyle.Exclamation)
            SSTab1.SelectedIndex = 3
            txtBarcodeFontSize.Focus()
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If Not mbCreateNewDB Then '-- updating..-
            '--  collect updates for confirmation..--
            If MsgBox("Save these changes: " & vbCrLf & vbCrLf & msGetChanges(asChanges), _
                           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                '--save changes..--
                If Not mbUpdateSystemInfo(mCnnSql, asChanges) Then
                    MsgBox("Couldn't save settings.", MsgBoxStyle.Exclamation)
                End If
            End If
            Me.Hide()
        Else '-- CREATE..--
            mbCancelled = False
            '-- New suffix "jmpos" for v3.1.= created DB's  ..-
            '==3401.424-  Keep JobTracking for RM users.
            If mbIsJMPOS() Then
                msSqlDbName = msBusinessShortName & "_" & "jmpos" '= "JobTracking"
            Else
                msSqlDbName = msBusinessShortName & "_" & "JobTracking"
            End If
            '--create security id.--
            sDay = VB6.Format(DatePart(Microsoft.VisualBasic.DateInterval.DayOfYear, Today), "##0") '--Today as day of year..
            sDay = VB6.Format(Len(sDay), "0") & sDay
            '===sSeconds = Trim(CStr(CLng(Timer)))
            '===sJT2SecurityId = sDay + sSeconds
            sState = Trim(VB6.GetItemString(cboState, cboState.SelectedIndex))
            sPostCode = Trim(txtPostCode.Text)

            If (msRetailHostName = "") Then
                MsgBox("You must select which Retail Management System " & vbCrLf & _
                                                   "  is in operation in this business..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            '--  final chance to change..-
            s1 = "You are about to create a new JobMatix database with the following key details:" & vbCrLf & vbCrLf
            s1 = s1 & "BusinessShortName:  " & msBusinessShortName & vbCrLf
            s1 = s1 & "ABN:     " & msBusinessABN & vbCrLf
            s1 = s1 & "State: " & sState & vbCrLf
            s1 = s1 & "PostCode: " & sPostCode & vbCrLf & vbCrLf
            s1 = s1 & "Note that these details will identify your database and cannot be changed.." & vbCrLf & vbCrLf
            If MsgBox(s1 & "Do you want to continue and create this new database?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                txtPostCode.Focus()
                Exit Sub
            End If
            '-- CREATE database.--
            '-- CREATE database.--
            '-- build biz details collection..-
            colBusinessDetails = New Collection
            sKey = "BusinessName" : sValue = Trim(txtFullName.Text)
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "BusinessAddress1" : sValue = Trim(txtAddress1.Text)
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "BusinessAddress2" : sValue = Trim(txtAddress2.Text)
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "BusinessState" : sValue = sState
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "BusinessPostCode" : sValue = sPostCode
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "BusinessPhone" : sValue = Trim(txtPhone.Text)
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)
            '=3203.117=
            sKey = "BusinessEmail" : sValue = Trim(txtEmail.Text)
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "GSTPercentage" : sValue = Trim(txtGSTPercentage.Text)
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "BusinessShortName" : sValue = msBusinessShortName
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "LabourHourlyRatePriority1" : sValue = txtLabourRates(0).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "LabourHourlyRatePriority2" : sValue = txtLabourRates(1).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "LabourHourlyRatePriority3" : sValue = txtLabourRates(2).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "LabourMinCharge" : sValue = txtLabourRates(3).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            '=3203.211=
            '== WRONG= sKey = "Enforce_Minimum_Charge" : sValue = IIf(chkNoEnforceMinCharge.Checked, "Y", "N")
            '=3203.218=
            '-- This is right..-
            sKey = "Enforce_Minimum_Charge" : sValue = IIf(chkNoEnforceMinCharge.Checked, "N", "Y")
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)
            '----------

            sKey = "ServiceNotificationCostLimit" : sValue = txtLabourRates(4).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)
            '---------
            sKey = "DescriptionPriority1" : sValue = txtPriority(0).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "DescriptionPriority2" : sValue = txtPriority(1).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "DescriptionPriority3" : sValue = txtPriority(2).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            '==QUOTECHASSISCAT1-
            sKey = "QuoteChassisCat1" : sValue = txtChassisCat(0).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "QuoteChassisCat2" : sValue = txtChassisCat(1).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "ItemBarcodeFontName" : sValue = txtBarcodeFontName.Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "ItemBarcodeFontSize" : sValue = txtBarcodeFontSize.Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)
            '==  29Jul2010==
            sKey = "StockServiceChargeCat1" : sValue = txtServiceCategory(0).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "StockServiceChargeCat2" : sValue = txtServiceCategory(1).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            '--  add updated terms and conditions..--
            sKey = "TermsAndConditions" : sValue = txtUserTexts(0).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "NewJobDocketFootnote" : sValue = txtUserTexts(1).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "DeliveryDocketFootnote" : sValue = txtUserTexts(2).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "ServiceChargesInfoText" : sValue = txtUserTexts(3).Text
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            '--server backup path..--
            sKey = "ServerShareLocalPath" : sValue = ""
            '==== GoSub cmdOK_AddInfoKey
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "ServerShareNetworkPath" : sValue = ""
            '==== GoSub cmdOK_AddInfoKey
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)
            '--==03Apr2011==
            sKey = "smsCentralGatewayURL"
            sValue = "http://www.smsboss.com.au/api/sms.asmx/SendSMS"
            '==== GoSub cmdOK_AddInfoKey
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "smsCentralGatewayUsername" : sValue = ""
            '==== GoSub cmdOK_AddInfoKey
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            sKey = "smsCentralGatewayPassword" : sValue = ""
            '==== GoSub cmdOK_AddInfoKey
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)

            '--  NEW.-- Retail Host name-
            Call mbCmdOK_AddInfoKey("RetailHostName", msRetailHostName, colBusinessDetails)

            '- New 3411.0217= 
            '--  Add Type of business.
            sKey = "TypeOfBusiness"
            sValue = "Other"
            If optBusinessTypeComputer.Checked Then
                sValue = "Computer"
            End If
            Call mbCmdOK_AddInfoKey(sKey, sValue, colBusinessDetails)
            '--done that biz type=

            Dim bDeleteDB As Boolean = False
            '=3107.801=
            sCreateLogPath = gsJobMatixLocalDataDir() & "\" & msBusinessShortName & "_CreateJobsDB.log" '-- log path for create..--
            '-- If log exists..  kill it..--
            If (Dir(sCreateLogPath) <> "") Then Kill(sCreateLogPath)
            Call gbLogMsg(sCreateLogPath, "JobMatix is creating database : " & msSqlDbName & vbCrLf & _
                                 "  on Server: " & msSqlServerComputer & "', " & ";  BusinessShortName=" & msBusinessShortName & _
                               vbCrLf & ";  ABN: " & msBusinessABN & ";  State: " & sState & ";  PostCode: " & sPostCode & " ===")
            labStatus.Text = "JobMatix is creating database : " & msSqlDbName
            ok = gbCreateJobsDB(mCnnSql, msSqlDbName, _
                         msBusinessABN, colBusinessDetails, "", "", sCreateLogPath, lErrors, dateCreated, sJT2SecurityId)
            If ok Then
                Call gbLogMsg(sCreateLogPath, "OK.. Database '" & msSqlDbName & "' was created on server: " & msSqlServerComputer & _
                                                                              vbCrLf & "   = = = = = = = = = = = = = = = = =  ")
                '-- 3101-- 27Sep2014-
                '=3401.424=  ONLY if POS system..
                If mbIsJMPOS() Then
                    '-- Add POS Tables..--
                    '-- gbCreatePOSdbTables =
                    '=3501.0814=  Fn moved to different class.
                    Dim JMx31POS As New JMxPOS330.clsJMxCreatePOS  '= JMxPOS330.clsJMxPOS31
                    ok = JMx31POS.JMx31CreatePOS(mCnnSql, msSqlDbName, sCreateLogPath, lErrors)
                    If ok Then
                        sPOSmsg = vbCrLf & "(Including JobMatixPOS tables)" & vbCrLf
                    Else  '--failed-
                        Call gbLogMsg(sCreateLogPath, "** FAILED to create POS database : " & msSqlDbName & _
                                                     " on server: " & msSqlServerComputer & vbCrLf & "   = = = = = = = = = = = =  ")
                        MsgBox("Failed to create POS database TABLES:  " & msSqlDbName, MsgBoxStyle.Exclamation)
                    End If
                End If '-pos-
            Else '--failed-
                Call gbLogMsg(sCreateLogPath, "** FAILED to create database : " & msSqlDbName & _
                                            " on server: " & msSqlServerComputer & vbCrLf & "   = = = = == = = = = = =  ")
                MsgBox("Failed to create Jobs database:  " & msSqlDbName, MsgBoxStyle.Exclamation)
            End If '--created ok--
            If ok Then
                Call gbLogMsg(sCreateLogPath, "= = =  JobMatix31 DB build completed ok.. DETAILS FOLLOW:  =====" & vbCrLf & _
                                            " == Date: " & VB6.Format(dateCreated, "dd-mmm-yyyy, ") & _
                                            VB6.Format(dateCreated, "hh:mm:ss") & ",  DBName= " & msSqlDbName & _
                                            ",  ABN= " & msBusinessABN & ", SecurityId=" & sJT2SecurityId & ".." & vbCrLf & _
                                                    "= = = = The End = = = = = ")
                MsgBox("JobMatix database:" & msSqlDbName & sPOSmsg & " has been created ok.." & vbCrLf & vbCrLf & _
                           "Please Note: " & vbCrLf & " A CREATE log has been created in the local directory..   ie.:" & vbCrLf & _
                             sCreateLogPath & vbCrLf & _
                             " This log must be preserved for issuance of user licence.." & vbCrLf & vbCrLf & _
                                                     "After Importing (if any), restart the application..", MsgBoxStyle.Information)
                '=3311.509=  Can IMPORT here if POS system..
                If mbIsJMPOS() Then
                    If MsgBox("Important Note: " & vbCrLf & _
                              "If you are migrating from MYOB RetailManager to JobMatxPOS, " & vbCrLf & _
                              "you can import all current Staff, Customers, Suppliers and Stock data (not Sales) " & vbCrLf & _
                              "from your latest MYOB RetailManager 'recent.mdb' database into JobMatix.." & vbCrLf & vbCrLf & _
                              "This is the only chance for you to do this, now at the POS database startup." & vbCrLf & _
                              "Do you want to Import the data from RetailManager ?", _
                               MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        '--Import is a go..-
                        labStatus.Text = "-- WAIT- Getting schema for DB: [" & msSqlDbName & "].."
                        System.Windows.Forms.Application.DoEvents()
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                        '-- Now.. get sql schema for the DB...-
                        Dim ColSqlDBInfo As Collection
                        If Not gbGetSqlSchemaInfo(mCnnSql, msSqlDbName, ColSqlDBInfo, True) Then
                            MsgBox("Failed to create Jobs database:  " & msSqlDbName & vbCrLf & _
                                   "You will have to re-do this Create. ", MsgBoxStyle.Exclamation)
                            bDeleteDB = True
                        Else  '-ok-
                            '-- do Import-
                            labStatus.Text = "-- IMPORTING RM data into: [" & msSqlDbName & "].."
                            '-- load import form-
                            Dim frmImport1 As New frmImportRM(Me, msServer, mCnnSql, msSqlDbName, ColSqlDBInfo, LabVersion.Text)
                            frmImport1.ShowDialog()
                            If frmImport1.cancelled Then
                                MsgBox("Import cancelled..  Database will be deleted..", MsgBoxStyle.Exclamation)
                                bDeleteDB = True
                            Else
                                MsgBox("Import successful.. " & vbCrLf & "Application needs to be re-started..", MsgBoxStyle.Information)
                            End If
                        End If  '--get schema-
                    Else '-no import-
                        MsgBox("ok.. Application needs to be re-started.", MsgBoxStyle.Information)
                    End If '-yes-import- 
                Else  '-not jmpos-
                    MsgBox("ok.. Application needs to be re-started.", MsgBoxStyle.Information)
                End If  '=jmpos-
            Else  '-failed-
                '-- delete failed DB.--
                bDeleteDB = True
            End If
            If bDeleteDB Then
                s1 = "USE MASTER " & vbCrLf
                Call gbExecuteCmd(mCnnSql, s1, L1, sErrorMsg)
                Call gbLogMsg(sCreateLogPath, "Deleting failed database:  '" & msSqlDbName & "'.." & vbCrLf & "== = = =  = = =")
                s1 = "DROP DATABASE " & msSqlDbName & vbCrLf
                If gbExecuteCmd(mCnnSql, s1, L1, sErrorMsg) Then
                    MsgBox("The Failed new DB was deleted.-", MsgBoxStyle.Information)
                End If
            End If

            Me.Hide()
            '==End If
        End If '-Create/Update..-
        Exit Sub

    End Sub '--ok--
    '= = = = = = = =
    '-===FF->

    '-- cancel..-

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Dim sMsg As String
        Dim vKeys As Object
        Dim sKey As String
        Dim sChanges As String
        Dim asChanges() As String
        Dim ix As Integer

        sChanges = ""
        If Not mbCreateNewDB Then '-- updating..-
            sChanges = msGetChanges(asChanges)
            If (sChanges <> "") Then
                sMsg = "Discard these changes ?" & vbCrLf & vbCrLf & sChanges
            Else
                mbCancelled = True
                Me.Hide()
                Exit Sub
            End If
        Else '--create..
            sMsg = "Abandon this CREATE operation ?"
        End If
        Dim result As MsgBoxResult = MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo)
        If result <> MsgBoxResult.Yes Then
            Exit Sub   '--stay here..
        Else
            mbCancelled = True
            Me.Hide()
            Exit Sub
        End If
    End Sub '--cancel..-
    '= = = = = = = = =

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmSetupJobsDB_FormClosing(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim sMsg As String

        If ((Not mbCreateNewDB) And (mSdUpdatedInfo.Count <= 0)) Or (mbCreateNewDB And (Not mbDataChanged)) Then '--updates empty..
            intCancel = 0 '--let it go--
        Else
            sMsg = "Abandon this create ?"
            If Not mbCreateNewDB Then sMsg = "Abandon changes ?"
            Select Case intMode
                Case System.Windows.Forms.CloseReason.WindowsShutDown, System.Windows.Forms.CloseReason.TaskManagerClosing, System.Windows.Forms.CloseReason.FormOwnerClosing '== not in .NET --, vbFormCode
                    intCancel = 0 '--let it go--
                Case System.Windows.Forms.CloseReason.UserClosing
                    '-- confirm if form is to be abandoned..--
                    If MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        intCancel = 0 '--let it go--
                    Else
                        intCancel = 1 '--cant close yet--'--was mistake..  keep going..
                    End If '--yes.--
                Case Else
                    intCancel = 0 '--let it go--
            End Select '--mode--
        End If '--exit.-
        eventArgs.Cancel = intCancel
    End Sub '--unload--
    '= = = = = = = = = = = =

    '=== end form==

End Class
'= = = = = = = =  ==