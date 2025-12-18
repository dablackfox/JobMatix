
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Reflection
Imports System.IO
Imports System.ComponentModel
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports system.data.OleDb

Public Class clsJMxPOS31

    '-- POS 31 DLL ..--

    '== grh 15-June-2021-  
    '==   This Is the Starup/Licence Check Class for the Open Source JobMatix..

    ' Copyright © 2021 grhaas@outlook.com

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


    '-- All Sales + Goods + Stock Functionality
    '--    Now packaged as DLL..
    '--    User calls Public Methods with LIVE connection Info..-
    '==
    '==  JobMatix POS3- DLL --  25-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb..
    '==
    '==  grh. JobMatix 3.1.3107.0801 ---  01-Aug-2015 ===
    '==   >>  POSSettings file now in CommonApplicationData--
    '==   >>   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)   
    '==
    '==  grh. DLL JMxPOS330 3.3.3307.0203 ---  03-Feb-2017 ===
    '==   >> Add function "GoodsReturned" to update POS Returns/Stock/SerialAudit tables for
    '==         Goods Sent (Returns)- 
    '==             (instantiated and called from JobMatix RA.s).
    '==
    '==     3401.0314- LICENCE CHECK function moved to this class..--
    '==                '-- Must use the New Constructor with Connection stuff.
    '==
    '==     3401.0317- Startup Till Check function moved to this class..--
    '==                '-- Must use New with Connection stuff.
    '==    v.3401.0414- 14Apr2017=
    '==          >> Fixes to find/use correct (Client) ComputerName in case of THIN CLIENT.-..-
    '==
    '==
    '==     3403.1010- 07/10-Oct-2017-
    '==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
    '==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
    '==               NB: (Table "DocArchive" to be DROPPED..)
    '==      --  HERE (in initialise constr.) we must check/setup email-Q server path.
    '==     3403.1028 -- Account Payments can now have discount given on invoices..  
    '==      -- PaymentDisbursment now has TranCode columns ("payment" or "discount".
    '==      -- Payment has new column:  discountGivenOnPayment MONEY NOT NULL DEFAULT 0,"
    '==     -- 3411.1125 -- Add TABLES for Subscriptions..
    '==     -- 3411.0127  27Jan2018= ..
    '==           Public Function GetDllVersion() As String
    '==     -- 3411.0224  24Feb2018= ..
    '==           Updates to Subscriptions schemas..
    '==
    '==     -- 3411.0423  23April2018= ..
    '==           Fix to GoodsReturned crash. (Collection index)..
    '==
    '==    3501.0717 17July2018-
    '==        -- Option to change Till now only avail on Startup...
    '==
    '==
    '== -- Updated 3501.0809  09August2018=  
    '==     -- Use Combined File/Sql subs module...
    '==       Incl.  Fixes to modAllFileAndSqlSubs to Get correct appname for LocalDataDir..
    '= = = = = = = = ==  =
    '==
    '== -- Updated 3501.0814  14August2018=  
    '==     -- Fix clsJMxPOS31 to separate CreatePosTables into clsJMxCreatePOS...
    '== -- Updated 3501.0930  30-Sep-2018=  
    '==     -- Fix startup to use "ServerShareNetworkPath" as as base of POS_EMAIL_QUEUE...
    '==
    '== -- Updated 3501.1029  29-Oct-2018=  
    '==     -- Startup Till Check- DROP Offer to change Till.....
    '==
    '==   Updated.- 3519.0404  Started 30-March-2019= 
    '==    -- TRIAL PERIOD Extended to ninety days for everyone...
    '==         AND Unlicenced Users restricted to ONE user, EXCEPT for Precise (still THREE users.) 
    '==  
    '==  NEW VERSION+
    '==
    '==    -- 4201.0717.  17-July-2019-
    '==       -- SupplierCode Table-  DROP Primary Key (not needed), expand supCode col. to 40 chars..
    '==       -- PurchaseOrderLine Table-  Expand supplierCode col. to 40 chars..
    '==
    '== = = =
    '==
    '== NEW revision-
    '==
    '==    -- 4201.0929.  Started 19-September-2019-
    '==        -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"
    '==
    '== = = = = = = = = = = = =
    '=
    '== For Build 4221.
    '==   3.  Tags- 25-Jan-2020.. For Build 4221..
    '==         -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
    '== 
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = 
    '==
    '==  Target is new Build 4251..  06-June=2020..
    '==  Target is new Build 4251..
    '==  
    '==   THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==       --  Involves creating a REFUND DETAILS EXTRA Option (radioButton) to be able to refund same types as Payments..
    '==                 ie dropdown including ZipPay, bank Deposit etc..
    '==             NOTE- Sales form will keep Refund Options Frame for continuity of process,
    '==                   allowing Refunds to Cash, CreditNote or EftPos as always..
    '==                BUT an extra Option (OTHER- Choose From List) will allow user to see a DropDown Combo
    '==                      of remaining keys from master List so User can choose ONE only..
    '==                ALSO TWO new columns to be added to Payments Table-  
    '==                      viz- "RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).
    '==                         to Cash/CreditNote/EftPosDr/EftPosCr already recorded in Payment record.
    '==
    '= = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = 


    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (15-June-2021)
    '==   Target-New-Build-6201 --  (15-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '==  FOR LicenceKey- ComputePosKey now points to dummy in clsKeygen42 in DLL JMxKeyGen420_OS
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    '==   Updated.- 3519.0404..
    Private Const k_POS_MAX_TRIALDAYS As Int32 = 90

    Private mCnnSql As OleDbConnection  '=  ADODB.Connection
    Private mbIsSqlAdmin As Boolean = False

    Private msServer As String = ""
  
    Private msSqlVersion As String = ""
    Private msInputDBNameJobs As String = ""

    '== Private msDBNameJobs As String

    Private msSqlDbName As String = ""
    Private mColSqlDBInfo As Collection '--  jobs DB info--
    '= Private mIntStaff_id As Integer = -1
    '= Private msStaffName As String = ""
    Private msRuntimeLogPath As String = ""

    Private msMachineName As String = "" '--local machine--
    Private msComputerName As String = "" '--client or Fat machine--
    Private mbIsThinClient As Boolean = False

    Private msAppPath As String = ""

    Private msDllversion As String = ""

    Private mSysInfo1 As clsSystemInfo

    '-- Licence Check..

    '--  L i c e n c e --
    '--  L i c e n c e --

    Private msBusinessABN As String = ""
    Private msBusinessShortName As String = ""
    Private msBusinessState As String = ""
    Private msBusinessPostCode As String = ""

    Private mdDateCreated As Date
    Private msLicenceKey As String = ""
    '== Private msLicenceKeyLevel2 As String = ""
    Private mbIsFullLicence As Boolean = False
    '== 3072/3 == Private mbIsThreeUserLicence As Boolean = False
    Private mbLicenceOK As Boolean = False
    Private mIntMaxUsersPermitted As Integer = 0   '--none--

    Private msJMPOS33_SecurityIdOriginal As String = ""
    Private msJMPOS33_SecurityId As String = ""
    Private mIntDatabaseDays As Integer = -1

    '=3403.1010=-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    '= Private msEmailQueueSharePath As String = ""

    '==  Target is new Build 4251..  06-June=2020..
    '==  Target is new Build 4251..  06-June=2020..
    '==  Target is new Build 4251..  06-June=2020..
    Private mbUpgradeWasRefused As Boolean = False

    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '==  Target is new Build 4251..  06-June=2020..
    '==  Target is new Build 4251..  06-June=2020..

    '--  Signal upgrade refused..
    Public ReadOnly Property UpgradeWasRefused As Boolean
        Get
            UpgradeWasRefused = mbUpgradeWasRefused
        End Get
    End Property  '-UpgradeWasRefused-
    '= = = = = = = = = = = = == = = 


    '-- anonymous constructor-

    'Public Sub New()

    'End Sub '- new-1=
    '= = = = = = = = = = = = = = = = = = = = = = = = =

    '-- second constructor- (for Returns-)

    Public Sub New(ByRef cnnSql As OleDbConnection, _
                     ByVal sSqlServerName As String, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                          ByVal strLogPath As String)

        mCnnSql = cnnSql
        msServer = sSqlServerName
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        msRuntimeLogPath = strLogPath
        '-- initialise..--
        '- Get actual machine running this app process. (NOT the remote client).
        msMachineName = My.Computer.Name  '- for actual machine running this app process. (NOT the remote client).

        '--  get thin client if any=
        ' get the workstation name...
        mbIsThinClient = False
        msComputerName = Environment.GetEnvironmentVariable("clientname")
        ' if not a thin client, previous step returns nothing,
        ' this will get the name of a fat client...
        If (msComputerName IsNot Nothing) AndAlso (msComputerName <> "") Then
            mbIsThinClient = True
        Else '-no "client"  is Fat..
            '= machinename = Environment.GetEnvironmentVariable("computername")
            msComputerName = My.Computer.Name
        End If

        '--- Separate the "SQL-Server\InstanceName" bits-- if needed..
        If (msServer = "\") Then
            msSqlServerComputer = msComputerName
        Else
            Dim kx As Integer = InStr(msServer, "\")
            If (kx > 0) Then '-have instance..--
                msSqlServerComputer = VB.Left(msServer, kx - 1)
                msSqlServerInstance = Mid(msServer, kx + 1)
                If msSqlServerComputer = "" Then '--local instance..
                    msSqlServerComputer = msComputerName
                End If
            Else '--default instance,..-
                msSqlServerComputer = msServer '--no instance name included..-
            End If
        End If '--ni name-

    End Sub '- new-2=
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function msGetDllversion() As String
        Dim assemblyThis As Assembly
        Dim assName As AssemblyName
        Dim s1, sVersion As String

        'msAppPath = My.Application.Info.DirectoryPath
        'If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        'gsAppPath = msAppPath

        '=3501.0809=
        Dim sAppPath As String = gsAppPath()
        If VB.Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
        msAppPath = sAppPath

        '-  new log each month..-
        '= s1 = VB.Format(Now, "yyyy-MM-dd")
        '= gsErrorLogPath = gsJobMatixLocalDataDir("JobMatix34") & "\JMxPOS340-Runtime-" & VB.Left(s1, 7) & ".log"
        '= gsRuntimeLogPath = gsErrorLogPath()  '--gsAppPath & "JTv3_Runtime.log"

        assemblyThis = System.Reflection.Assembly.GetExecutingAssembly()
        assName = assemblyThis.GetName
        With assName.Version
            sVersion = CStr(.Major) & "." & CStr(.Minor) & "." & CStr(.Build) & "." & CStr(.Revision)
        End With

        msGetDllversion = sVersion
    End Function  '--get version-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecuteSql = True   '--ok--
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
            Call gbLogMsg(msRuntimeLogPath, sErrorMsg)
            '= msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '==  Target is new Build 4251..  06-June=2020..
    '==  Target is new Build 4251..  06-June=2020..

    Private Function mbDoesColumnExist(ByVal sTableName As String, _
                                         ByVal sColumnName As String, _
                                         ByRef bColumnExists As Boolean) As Boolean
        Dim rdr1 As OleDbDataReader
        Dim sSql, sErrorMsg As String

        mbDoesColumnExist = False
        bColumnExists = False

        sSql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS " & vbCrLf &
                    " WHERE (TABLE_NAME = '" & sTableName & "') " & _
                        " AND (COLUMN_NAME = '" & sColumnName & "')" & vbCrLf

        If gbGetReader(mCnnSql, rdr1, sSql) Then  '--check if row exists..-
            mbDoesColumnExist = True  '-rdr is ok-
            If rdr1.HasRows Then '-column already exists..-
                bColumnExists = True
                rdr1.Close()
                Exit Function
            Else  '--doesn't exist.. 
                rdr1.Close()
            End If  '-has rows-
        Else  '-rdr error-
            '--  GET error text !!--
            MsgBox("mbDoesColumnExist: Error in reading INFORMATION_SCHEMA.COLUMNS.." & vbCrLf & _
                                  gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
        End If  '-get

    End Function  '- mbDoesColumnExist() -
    '= = = = = = = = = = = = = = = = = = = = == 
    '-===FF->

    '-- Public methods..
    '-- Public methods..

    Public Function GetDllVersion() As String

        GetDllVersion = msDllversion

    End Function  '-get dll version-
    '= = = = = = = = = = = = =  = = =

    '-- Startup- Check we have Till assigned..
    '==3501.0717 -- Option to change Till now only avail on Startup...

    Public Function StartupTillCheck(ByRef strOurTillId As String) As Boolean

        '--  Look up SystemInfo Till Assignment for this computer. (msComputerName)
        '-- Sysinfo key is like "CashDrawer_[ Computer ]=X" ( where "computer"=ComputerName)..
        '--         AND Where "X"  can be ["A".."Z" ) ie Till assigned to this computer..

        '==
        '== -- Updated 3501.1029  29-Oct-2018=  
        '==     -- Startup Till Check- DROP Offer to change Till.....
        '== 

        '= Dim sCurrentCashDrawer As String = ""
        Dim sTillId As String

        StartupTillCheck = False
        If Not gbGetCashDrawer(mCnnSql, msComputerName, sTillId) Then
            '=== mTxtSaleCustBarcode.Text = "NO TILL!"
            '==  mTxtSaleCustBarcode.ReadOnly = True
        Else  '--ok-
            '= sCurrentCashDrawer = sTillId
            strOurTillId = sTillId
            'If MessageBox.Show("Your PC is currently assigned to Till- " & gsGetCurrentCashDrawer() & vbCrLf & _
            '                   " Do you want to change to a different Till ?",
            '                   "JobMatixPOS Startup", _
            '                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
            '                           MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            '    '-No-- so no change-
            '    StartupTillCheck = True
            '    MessageBox.Show("ok.. You are now assigned to Till- " & gsGetCurrentCashDrawer() & vbCrLf,
            '                   "JobMatixPOS Startup", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    '== Exit Function
            'Else  '-yes
            '    '=  Wants to change Till..
            '    StartupTillCheck = True
            '    Dim sNewCashDrawerId As String = ""
            '    If gbChangeCashDrawer(mCnnSql, _
            '                                 msComputerName, _
            '                                   sNewCashDrawerId) Then
            '        strOurTillId = sNewCashDrawerId
            '    Else
            '        MessageBox.Show("ERROR- Failed to change currently assigned to Till-", _
            '                  "JobMatixPOS Startup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    End If  '-change-
            'End If  '-yes
            '-No more offer to change-
            StartupTillCheck = True
        End If  '-get-
    End Function  '- StartupTillCheck-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Public methods..
    '-- POS DLL Startup.. Lots of checks....
    '-- POS DLL Startup.. Lots of checks....
    '-- POS DLL Startup.. Lots of checks....

    '-- Licence Check..
    '-- Result ok can continue..

    Public Function LicenceCheckPOS(ByRef bIsEvaluating As Boolean) As Boolean
        Dim sMsg, s1, s2, s3 As String

        LicenceCheckPOS = False
        bIsEvaluating = False

        '-- set up log path.. (in ModFileSuport)..
        If msDllversion = "" Then
            msDllversion = msGetDllversion()
        End If
        '- Check that Connection has been set..
        If mCnnSql Is Nothing Then
            MsgBox("No SQL Connection for Licence Check.", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        sMsg = "JmxPOS340 Startup Licence Check started." & vbCrLf
        sMsg &= "==  Host Machine Name is: " & msMachineName & ";  " & vbCrLf
        If mbIsThinClient Then
            sMsg &= "==  Thin-Client Computer is: " & msComputerName & ";  "
        Else
            sMsg &= "==  Fat-Client Computer is: " & msComputerName & ";  "
        End If
        sMsg &= vbCrLf & "== POS DLL version is: " & msDllversion & ";  " & vbCrLf

        Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf)


        '=3403.919= Check/Set user Admin status--
        '--  check if we are sqlAdmin privileged..--
        mbIsSqlAdmin = gbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")
        '-- save as global--
        Call gbSetIsSqlAdmin(mbIsSqlAdmin)
        '== then can check.. = If Not gbIsSqlAdmin() Then ==

        '- test= 
        '== MsgBox(sMsg, MsgBoxStyle.Information)

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")

        '==
        '==     v3.3.3301.1112..  12-Nov-2016= ===
        '==       >> Add Licence Checking (cloning POS licence off Jobmatix)..-
        mbIsFullLicence = False
        mbLicenceOK = False

        msLicenceKey = mSysInfo1.item("POS33_LICENCEKEY")

        If mSysInfo1.itemDateCreated("JMPOS33_SECURITYID", mdDateCreated) Then
            '-ok-
            msJMPOS33_SecurityIdOriginal = mSysInfo1.item("JMPOS33_SECURITYID")
            '=MsgBox("TEST- Found JT2SECURITYID= " & msJT2SecurityIdOriginal & vbCrLf & _
            '=          "Date Created=" & VB.Format(mdDateCreated, "dd-MMM-yyyy HH:mm "))
        End If '-itemdate
        '==

        '-- C O M P U T E   L I C E N C E ----

        '= Dim asSalt(5) As String
        Dim sComputedKey As String
        '= Dim sComputedPreciseShortKey As String
        Dim sComputedKeyLevel2 As String
        Dim sComputedKeyThreeUser As String
        Dim sComputedKeyTwoUser As String
        Dim sComputedKeySingleUser As String
        Dim bIsLevel2Licence As Boolean = False

        '= Dim binMD5Digest() As Byte
        '= Dim sMsg, sId, sIdLevel2, sIdThreeUser As String
        '= Dim sIdTwoUser, sIdSingleUser As String


        '-- C O M P U T E Possible  L I C E N C E S ----
        '-- C O M P U T E Possible  L I C E N C E S ----

        '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
        '==
        '==   Target-New-Build-6201 --  (15-June-2021)
        '==   Target-New-Build-6201 --  (15-June-2021)
        '==
        '==  For JobMatix62Main- OPEN SOURCE version...
        '==  For JobMatix62Main- OPEN SOURCE version...
        '==
        '==  FOR LicenceKey- ComputePosKey now points to dummy in clsKeygen42 in DLL JMxKeyGen420_OS
        '==
        '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


        Dim clsKeyGen1 As JMxKeyGen420.clsKeyGen42
        Dim bIsPosSys As Boolean = True

        clsKeyGen1 = New JMxKeyGen420.clsKeyGen42
        '-- NOW used class for compute..

        If Not clsKeyGen1.ComputePosKey(msBusinessABN, msBusinessPostCode, msBusinessShortName,
                                       msSqlDbName, bIsPosSys, sComputedKey,
                                      sComputedKeyLevel2, sComputedKeyThreeUser, sComputedKeyTwoUser, sComputedKeySingleUser) Then
            MsgBox("Failed to get Licence Keys..", MsgBoxStyle.Exclamation)
            'Else
            '    '-- ok--
            '    '=gbComputePosKey = True
            'End If  '-get keys.
            'If Not gbComputePosKey(msBusinessABN, msBusinessPostCode, msBusinessShortName, msSqlDbName, _
            '                              sComputedKey, sComputedKeyLevel2, _
            '                                  sComputedKeyThreeUser, _
            '                                   sComputedKeyTwoUser, _
            '                                      sComputedKeySingleUser) Then
            '    MsgBox("Failed to Compute valid POS Licence key", MsgBoxStyle.Exclamation)

        Else  '-computed ok..
            sMsg = "Level-1 Computed FULL key is: " & vbCrLf & sComputedKey & vbCrLf & vbCrLf &
                 "Level-2 Computed key is: " & vbCrLf & sComputedKeyLevel2 & vbCrLf & vbCrLf &
                    "ThreeUser Computed key is: " & vbCrLf & sComputedKeyThreeUser & vbCrLf & vbCrLf &
                    "TwoUser Computed key is: " & vbCrLf & sComputedKeyTwoUser & vbCrLf & vbCrLf &
                     "Single-User Computed key is: " & vbCrLf & sComputedKeySingleUser & vbCrLf
            '=-- TEST- 
            '== MsgBox(sMsg, MsgBoxStyle.Information)

            '- Check Site POS Licence if any..
            '- Check Site POS Licence if any..
        End If  '-compute-

        '== END  Target-New-Build-6201 --  (15-June-2021)
        '== END  Target-New-Build-6201 --  (15-June-2021)


        '--   L I C E N C E  C H E C K ----
        '--   L I C E N C E  C H E C K ----
        '--   L I C E N C E  C H E C K ----

        '--  GET DateCreated of "BusinessABN" info row..-
        '----- and check licence date and key..--
        '-------  note SERVER Name was used in PRECISE Licence build..====
        '-- RECREATE securityId and dateCreated from ABN Date in systemInfo.-
        Dim dateX As DateTime
        msJMPOS33_SecurityId = gsMakeSecurityId(mCnnSql, dateX) '--re-computed (POS) for checking.. 
        '-- -- From[modCreateJobs3]-] --

        s3 = "Re-computed SecurityId is: " & msJMPOS33_SecurityId & vbCrLf & _
                "DateCreated is: " & Format(mdDateCreated, "dd-mmm-yyyy, hh:mm:ss")
        '== If gbDebug Then
        '== MsgBox(s3, MsgBoxStyle.Information)
        '== End If
        '-- NB: if (msJT2SecurityId <> msJT2SecurityIdOriginal) then TAMPERING has neen done !!..--

        If (msLicenceKey <> "") Then '--have licence..  check it..-
            '== Target-New-Build-6201 --  (15-June-2021)
            'mbLicenceOK = gbCheckLicenceKey(msLicenceKey,
            '                               sComputedKey, sComputedKeyLevel2,
            '                                sComputedKeyThreeUser, sComputedKeyTwoUser,
            '                                  sComputedKeySingleUser,
            '                                    bIsLevel2Licence, mIntMaxUsersPermitted)
            mbLicenceOK = clsKeyGen1.CheckLicenceKey(msLicenceKey,
                                           sComputedKey, sComputedKeyLevel2,
                                            sComputedKeyThreeUser, sComputedKeyTwoUser,
                                              sComputedKeySingleUser,
                                                bIsLevel2Licence, mIntMaxUsersPermitted)
            '== END  Target-New-Build-6201 --  (15-June-2021)
            If Not mbLicenceOK Then
                MsgBox("The POS Licence key on file is not valid for this installation.." & vbCrLf &
                                       "Please enter a valid licence key..", MsgBoxStyle.Information)
                msLicenceKey = ""
                mIntMaxUsersPermitted = 0
            Else  '--ok-
                If Not bIsLevel2Licence Then mbIsFullLicence = True
            End If  '-ok-
        End If  '--no licence..-

        '==3083==mlDatabaseDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, mdDateCreated, CDate(DateTime.Today))
        mIntDatabaseDays = gIntDateDiffDays(mdDateCreated, DateTime.Today)
        '- Enter licence if none yet..-

        '== Target-New-Build-6201 --  (15-June-2021)
        If (msLicenceKey = "") And (Not clsKeyGen1.LicenceRequired) Then '--no licence..-
            '--not required--  Open Source version..
            mbLicenceOK = True
            mbIsFullLicence = True
            mIntMaxUsersPermitted = -1  '-unlimited-
            bIsEvaluating = False  '= True
            MsgBox("Open Source Version-  No EUL needed.", MsgBoxStyle.Information)
            '== END  Target-New-Build-6201 --  (15-June-2021)

        ElseIf (msLicenceKey = "") Then '--no licence..- But Is Required..
            Dim intCount, intDaysRem As Integer
            Dim ok As Boolean
            ok = False
            '= 3519.0404=NOW 90..  
            '--   intDaysRem = (60 - mIntDatabaseDays)
            intDaysRem = (k_POS_MAX_TRIALDAYS - mIntDatabaseDays)
            If (intDaysRem > 0) Then
                If (MsgBox("There are " & CStr(intDaysRem) & " days left for this JobMatix-POS evaluation.." & vbCrLf & _
                             "Do you have a valid JobMatix-POS Licence Key to enter? ", _
                             MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                    mbLicenceOK = True '--bypass licence for now..-
                    mbIsFullLicence = True  '==3067.0== --trial has full functions..--
                    mIntMaxUsersPermitted = -1  '-unlimited-
                    ok = True
                    bIsEvaluating = True
                End If
            Else '--expired..-
                MsgBox("The POS Evaluation period has expired..", MsgBoxStyle.Exclamation)
            End If
            intCount = 0
            While (Not ok) And (intCount < 3)
                '====Else   '--expired..--
                s1 = InputBox("Please enter a valid POS Licence Key:", "JobMatix POS Licence.")
                s1 = Replace(s1, " ", "") '--delete blanks..--
                If s1 = "" Then '--nag..-
                    intCount += 1
                    If (intCount >= 3) Then mbLicenceOK = False
                Else '--check licence..--
                    mbLicenceOK = clsKeyGen1.CheckLicenceKey(s1, sComputedKey, sComputedKeyLevel2,
                                                      sComputedKeyThreeUser, sComputedKeyTwoUser,
                                                        sComputedKeySingleUser,
                                                          bIsLevel2Licence, mIntMaxUsersPermitted)
                    If mbLicenceOK Then
                        msLicenceKey = s1
                        If bIsLevel2Licence Then
                            ok = True
                        Else  '--full licence-
                            mbIsFullLicence = True
                            ok = True
                        End If   '--level2--
                        '--   UPDATE systemInfo..-
                        If Not mSysInfo1.UpdateSystemInfo(New Object() {"POS33_LICENCEKEY", msLicenceKey}) Then
                            MsgBox("Failed to update POS LicenceKey details in systemInfo table..", MsgBoxStyle.Critical)
                        End If
                    Else  '--invalid..--
                        MsgBox("POS Key does not seem to be valid for this installation..", MsgBoxStyle.Exclamation)
                        intCount += 1
                        If (intCount >= 3) Then mbLicenceOK = False
                    End If
                End If '--empty-
            End While '--OK..----End If  '--days..-
        End If '--no licence..-

        '-- NB: if (msJT2SecurityId <> msJT2SecurityIdOriginal) then TAMPERING has neen done !!..--
        If (msJMPOS33_SecurityId <> msJMPOS33_SecurityIdOriginal) Then
            '--EXCLUDES PRECISE..  -- TAMPERING has neen done !!..--
            MsgBox("Note: Important POS licence information has been changed.." & vbCrLf & _
                        "POS Licence can not be validated.", MsgBoxStyle.Exclamation)
            mbLicenceOK = False
        End If

        Call gbLogMsg(gsRuntimeLogPath, "JmxPOS340 Startup Licence Check done." & vbCrLf & _
                                 "Ok=" & mbLicenceOK.ToString & ";  Evaluating=" & bIsEvaluating.ToString & "." & vbCrLf)

        '== end of licence stuff..
        '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

        '=3403.516=  CHECK if layby Tables need to be added..

        If Not gbDoesTableExist(mCnnSql, "layby") Then
            '-- must add-
            Dim ok As Boolean
            Dim asChanges(1) As String '--update list..-
            Dim intErrorCount As Integer

            ok = gbCreatePOSLaybyTables(mCnnSql, msSqlDbName, msRuntimeLogPath, intErrorCount)
            If ok Then
                '-- updated build no..
                Try
                    asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
                    asChanges(1) = Trim(msDllversion)
                    If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                        MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
                    Else
                    End If
                Catch ex As Exception
                    MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
                MsgBox("Layby Table definitions were added (ok) to your database..", MsgBoxStyle.Information)
            End If  '-ok
        End If '-- exists.

        '=3403.522=  CHECK if layby_id column is in Invoice Table.. or needs to be added..
        Dim bWasAdded As Boolean = False
        Dim sColDef As String = " INT NOT NULL DEFAULT -1 "
        If Not gbAddColumnToTable(mCnnSql, "invoice", "delivered_layby_id", sColDef, bWasAdded) Then
            MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        Else  '-ok=
            If bWasAdded Then
                MsgBox("The new column 'delivered_Layby_id' was added (ok) to the Invoice Table..", MsgBoxStyle.Information)
            End If
        End If

        '=3403/1010=  DROP DocArchive Table if exists.. (is obsolete).
        If gbDoesTableExist(mCnnSql, "DocArchive") Then
            Dim sSql As String = "DROP TABLE DocArchive ;"
            If mbExecuteSql(mCnnSql, sSql, False, Nothing) Then
                MsgBox("DROP TABLE DocArchive; was executed ok..", MsgBoxStyle.Information)
            Else
                MsgBox("ERROR-  FAILED to DROP DocArchive Table..", MsgBoxStyle.Exclamation)
            End If
        End If  '-exists-
        '=ALSO DROP sTable = "paymentRefundDetails" '---..
        '--  (Now ALL refunds are handles by vash refund or Credit Note)..
        If gbDoesTableExist(mCnnSql, "paymentRefundDetails") Then
            Dim sSql As String = "DROP TABLE paymentRefundDetails ;"
            If mbExecuteSql(mCnnSql, sSql, False, Nothing) Then
                MsgBox("DROP TABLE paymentRefundDetails; was executed ok..", MsgBoxStyle.Information)
            Else
                MsgBox("ERROR-  FAILED to DROP paymentRefundDetails Table..", MsgBoxStyle.Exclamation)
            End If
        End If  '-exists-

        '==  3403.1028 -- Account Payments can now have discount given on invoices..  
        '==      -- PaymentDisbursment now has TranCode columns ("payment" or "discount").
        '---  Check/add column if needed..
        bWasAdded = False
        sColDef = "  nvarChar(15) NOT NULL DEFAULT 'payment' "
        If Not gbAddColumnToTable(mCnnSql, "PaymentDisbursements", "tranCode", sColDef, bWasAdded) Then
            MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        Else  '-ok=
            If bWasAdded Then
                MsgBox("The new column 'tranCode' " & vbCrLf &
                       "  was added (ok) to the PaymentDisbursements Table..", MsgBoxStyle.Information)
            End If
        End If
        '== 3403.1030 -- Payments has new columns:  1. discountGivenOnPayment MONEY NOT NULL DEFAULT 0,"
        sMsg = ""
        bWasAdded = False
        sColDef = "  MONEY NOT NULL DEFAULT 0 "
        If Not gbAddColumnToTable(mCnnSql, "Payments", "discountGivenOnPayment", sColDef, bWasAdded) Then
            MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        Else  '-ok=
            If bWasAdded Then
                'MsgBox("The new column 'discountGivenOnPayment' " & vbCrLf &
                '       "  was added (ok) to the Payments Table..", MsgBoxStyle.Information)
                sMsg &= "-- The new column 'discountGivenOnPayment' " & vbCrLf &
                       "  was added (ok) to the Payments Table.." & vbCrLf
            End If
        End If
        '-- Reversal only for Debtors Payments.
        '= sCreateSql &= "  isReversal bit NOT NULL DEFAULT 0,"
        '= sCreateSql &= "  originalPayment_id INT NOT NULL DEFAULT -1, "
        bWasAdded = False
        sColDef = " bit NOT NULL DEFAULT 0 "
        If Not gbAddColumnToTable(mCnnSql, "Payments", "isReversal", sColDef, bWasAdded) Then
            MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        Else  '-ok=
            If bWasAdded Then
                sMsg &= "-- The new column 'isReversal' " & vbCrLf &
                       "  was added (ok) to the Payments Table.." & vbCrLf
            End If
        End If
        bWasAdded = False
        sColDef = "  INT NOT NULL DEFAULT -1 "
        If Not gbAddColumnToTable(mCnnSql, "Payments", "originalPayment_id", sColDef, bWasAdded) Then
            MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        Else  '-ok=
            If bWasAdded Then
                sMsg &= "-- The new column 'originalPayment_id' " & vbCrLf &
                       "  was added (ok) to the Payments Table.." & vbCrLf
            End If
        End If
        '- Report new payment columns..
        If sMsg <> "" Then
            MsgBox("Table updated- " & vbCrLf & sMsg, MsgBoxStyle.Information)
        End If

        '=3403/1010=  Check EmailQueue Server Path if exists.. (else create).
        '-- msSqlServerComputer -- s/be start of share..
        Dim sServerShareLocalPath As String = ""
        Dim sServerShareNetworkPath As String = ""
        Dim sEmailQueueSharePath As String = ""

        '=3403.1009- Server Share Path for Email Queue.
        Dim sNewQueuePath = ""  '= "users\public\JobMatixPOS-EmailQueue"

        '=3501.0930= -"ServerShareNetworkPath"-
        If mSysInfo1.contains("ServerShareLocalPath") Then
            sServerShareLocalPath = mSysInfo1.item("ServerShareLocalPath")
        End If
        If mSysInfo1.contains("ServerShareNetworkPath") Then
            sServerShareNetworkPath = mSysInfo1.item("ServerShareNetworkPath")
        End If
        If (sServerShareLocalPath = "") Then
            '-default-
            sServerShareLocalPath = "c:\users\public"
            sServerShareNetworkPath = "\\" & msSqlServerComputer & "\users\public"
            '- update settings for these values
            '-update-
            Dim asChanges(3) As String
            asChanges(0) = "ServerShareLocalPath"  '= "POS_sell_margin"
            asChanges(1) = Trim(sServerShareLocalPath)
            asChanges(2) = "ServerShareNetworkPath"  '= "POS_sell_margin"
            asChanges(3) = Trim(sServerShareNetworkPath)
            If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                MsgBox("Startup- Couldn't save " & "ServerShareLocalPath" & " setting.", MsgBoxStyle.Exclamation)
            Else  '-ok-
            End If '-update-
        End If
        '-- check paths and update email q. path.
        '-- WE might be on client, so check Network path..
        If (Not Directory.Exists(sServerShareNetworkPath)) Then
            '-ok-
            '-missing.
            MsgBox("Error- The Server Local/Network Share path Folders: " & vbCrLf & _
                     "'" & sServerShareLocalPath & " | " & sServerShareNetworkPath & "'" & _
                      "    are missing or wrong.." & vbCrLf & _
                     "This must be fixed, or the Share paths changed, " & vbCrLf & _
                     " before DB backups can be done, or invoices can be emailed out." & _
                       vbCrLf & vbCrLf & "See JobMatixPOS Setup Form to fix this..", MsgBoxStyle.Exclamation)
        Else  '-ok-
            '- update email queue path if needed-
            sNewQueuePath = sServerShareNetworkPath & "\JobMatix34MailQueue"
            If Not Directory.Exists(sNewQueuePath) Then
                Dim info1 As DirectoryInfo  '= = Directory.CreateDirectory(sNewQueuePath)
                Try
                    info1 = Directory.CreateDirectory(sNewQueuePath)
                    MsgBox("The EmailQueue Share path Folder " & "'" & sNewQueuePath & vbCrLf & _
                     " was created (ok) on the server..", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Error- Failed to Create EmailQueue Share path Folder: " & vbCrLf & _
                      "'" & sNewQueuePath & "' on the server.." & vbCrLf & _
                      "This needs to be fixed before invoices can be emailed out.", MsgBoxStyle.Exclamation)
                End Try  '-create-
            End If
            '- update settings.
            If mSysInfo1.contains("POS_EMAILQUEUE_SHAREPATH") Then
                sEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")
            End If
            If (sEmailQueueSharePath = "") Or (LCase(sEmailQueueSharePath) <> LCase(sNewQueuePath)) Then
                '-update-
                Dim asChanges(1) As String
                asChanges(0) = "POS_EMAILQUEUE_SHAREPATH"  '= "POS_sell_margin"
                asChanges(1) = Trim(sNewQueuePath)
                If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                    MsgBox("Couldn't save " & "POS_EMAILQUEUE_SHAREPATH" & " setting.", MsgBoxStyle.Exclamation)
                Else  '-ok-
                End If '-update-
            End If  '-path=""-
        End If '-old path exists.

        ''-obsolete-
        'If (Len(sEmailQueueSharePath) <= 2) OrElse (VB.Left(sEmailQueueSharePath, 2) <> "\\") Then
        '    '-  no path, or invalid path.. must create-
        '    sNewQueuePath = sServerShareNetworkPath   '= "\\" & msSqlServerComputer & "\users\public"
        '    '- check this path.-
        'Else '-ok have path-
        '    '-check if server is current..
        '    Dim sOldServer, sOldTail As String
        '    Dim kx As Integer = InStr(3, sEmailQueueSharePath, "\")
        '    If (kx > 3) Then '-have tail..--
        '        sOldServer = Trim(Mid(sEmailQueueSharePath, 3, kx - 3))
        '        sOldTail = Mid(sEmailQueueSharePath, kx + 1)
        '        If LCase(sOldServer) <> LCase(msSqlServerComputer) Then
        '            '- server was changed via RESTORE ? --
        '            sNewQueuePath = "\\" & msSqlServerComputer & "\users\public"
        '        Else  '-server still same..
        '            '--   so check Q. path is still ok.
        '            If Directory.Exists(sEmailQueueSharePath) Then
        '                '-ok-
        '            Else  '-missing.
        '                MsgBox("Error- The EmailQueue Share path Folder: " & vbCrLf & _
        '                         "'" & sEmailQueueSharePath & "' Folder has gone missing." & vbCrLf & _
        '                         "This must be fixed, or the Share path changed, before invoices can be emailed out.", MsgBoxStyle.Exclamation)
        '            End If '-old path exists.
        '        End If '-changed-
        '    Else '-wrong-
        '        MsgBox("Invalid EmailQueue Share path '" & _
        '               sEmailQueueSharePath & "' in settings.", MsgBoxStyle.Exclamation)
        '    End If
        'End If '-have path.-
        ''- re-create if needed..
        'If (sNewQueuePath <> "") Then  '-- we have  "\\" & msSqlServerComputer & "\users\public" --
        '    If Directory.Exists(sNewQueuePath) Then
        '        '== MsgBox("Testing. found path '" & sNewQueuePath & "' is ok..", MsgBoxStyle.Information)
        '        '--add JobMatix Sub-dir ..
        '        sNewQueuePath &= "\JobMatix34MailQueue"
        '        '- Create if it doesn't exist.
        '        Dim asChanges(1) As String
        '        Try
        '            Dim info1 As DirectoryInfo = Directory.CreateDirectory(sNewQueuePath)
        '            '-ok- Update sysinfo for updated path.
        '            MsgBox("The EmailQueue Share path Folder " & "'" & sNewQueuePath & vbCrLf & _
        '                                 " was created (ok) on the server..", MsgBoxStyle.Information)
        '            asChanges(0) = "POS_EMAILQUEUE_SHAREPATH"  '= "POS_sell_margin"
        '            asChanges(1) = Trim(sNewQueuePath)
        '            If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
        '                MsgBox("Couldn't save " & "POS_EMAILQUEUE_SHAREPATH" & " setting.", MsgBoxStyle.Exclamation)
        '            End If
        '        Catch ex As Exception
        '            MsgBox("Error- Failed to Create EmailQueue Share path Folder: " & vbCrLf & _
        '                      "'" & sNewQueuePath & "' on the server.." & vbCrLf & _
        '                      "This needs to be fixed before invoices can be emailed out.", MsgBoxStyle.Exclamation)
        '        End Try  '-create-
        '    Else  '-no path..
        '        '=3411.0112= --error-  Server SHARE not accessible.
        '        MsgBox("Error- The EmailQueue Public Folder: " & vbCrLf & _
        '                "'" & sNewQueuePath & "' Folder does not exist." & vbCrLf & _
        '                    "This must be fixed before invoices can be emailed out." & vbCrLf & _
        '                    vbCrLf & "  (and JobMatixPOS will need to be re-started..)", MsgBoxStyle.Exclamation)
        '    End If  '-exists-
        'End If  '-new path-

        '==  3411.1125 -- Add TABLES for Subscriptions..
        If Not gbDoesTableExist(mCnnSql, "Subscription") Then
            '-- must add-
            Dim ok As Boolean
            Dim asChanges(1) As String '--update list..-
            Dim intErrorCount As Integer

            ok = gbCreatePOSSubscriptionTables(mCnnSql, msSqlDbName, msRuntimeLogPath, intErrorCount)
            If ok Then
                '-- updated build no..
                Try
                    asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
                    asChanges(1) = Trim(msDllversion)
                    If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                        MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
                    Else
                    End If
                Catch ex As Exception
                    MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
                MsgBox("Subscription Table definitions were added (ok) to your database..", MsgBoxStyle.Information)
            End If  '-ok
        Else
            '-exists.. check new columns.
            '==  3411.0224  24Feb2018= ..
            '==           Updates to Subscriptions schemas..
            sMsg = ""
            bWasAdded = False
            sColDef = " datetime NULL "
            If Not gbAddColumnToTable(mCnnSql, "Subscription", "termination_date", sColDef, bWasAdded) Then
                MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
            Else  '-ok=
                If bWasAdded Then
                    sMsg &= "-- The new column 'termination_date' " & vbCrLf &
                           "  was added (ok) to the Subscription Table.." & vbCrLf
                End If
            End If
            '-sellActual_inc MONEY NOT NULL DEFAULT 0 =
            bWasAdded = False
            sColDef = " MONEY NOT NULL DEFAULT 0 "
            If Not gbAddColumnToTable(mCnnSql, "SubscriptionLine", "sellActual_inc", sColDef, bWasAdded) Then
                MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
            Else  '-ok=
                If bWasAdded Then
                    sMsg &= "-- The new column 'sellActual_inc' " & vbCrLf &
                           "  was added (ok) to the SubscriptionLine Table.." & vbCrLf
                End If
            End If

            '==42.0929= 
            '==
            '==    -- 4201.0929.  Started 19-September-2019-
            '==        -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"

            bWasAdded = False
            sColDef = " BIT NOT NULL DEFAULT 0  "
            If Not gbAddColumnToTable(mCnnSql, "Subscription", "OkToEmailInvoices", sColDef, bWasAdded) Then
                MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
            Else  '-ok=
                If bWasAdded Then
                    sMsg &= "-- The new column 'OkToEmailInvoices' " & vbCrLf &
                           "  was added (ok) to the Subscription Table.." & vbCrLf
                End If
            End If  '--add-
            '= = = = end this column..-

            '- Report new payment columns..
            If sMsg <> "" Then
                MsgBox("Table updated- " & vbCrLf & sMsg, MsgBoxStyle.Information)
            End If
        End If '-- exists.

        '=17-July-2019-
        '=4201.0717=
        '-- Drop PKEY for SupplierCode Table and Expand SupCode Column..

        Dim sSupCodeSql As String
        Dim dtSupCode As DataTable
        Dim sPkeyConstaintName

        sSupCodeSql = "SELECT name FROM sysobjects" & _
                           " WHERE (xtype = 'PK') AND parent_obj = OBJECT_ID(N'SupplierCode')"
        If gbGetDataTable(mCnnSql, dtSupCode, sSupCodeSql) Then
            If (dtSupCode IsNot Nothing) Then
                '- if has rows, then EXISTS..
                If (dtSupCode.Rows.Count > 0) Then
                    '--exists.. we need to drop it..
                    sPkeyConstaintName = dtSupCode.Rows(0).Item("name")
                    '= MsgBox("The primary key constraint: " & sPkeyConstaintName & " will be dropped..", MsgBoxStyle.Information)

                    '-- DROP the PKEY as it is not useful, and expand the column.. in tables supplierCode & PO Line..
                    sSupCodeSql = "ALTER TABLE dbo.SupplierCode DROP CONSTRAINT " & sPkeyConstaintName & " ;" & vbCrLf
                    sSupCodeSql &= "ALTER TABLE dbo.SupplierCode ALTER COLUMN supcode nvarchar(40) NOT NULL; " & vbCrLf
                    sSupCodeSql &= "ALTER TABLE dbo.PurchaseOrderLine ALTER COLUMN SupplierCode nvarchar(40) NOT NULL; "

                    '= MsgBox("sql is: " & vbCrLf & sSupCodeSql, MsgBoxStyle.Information)

                    '-- ask user consent to upgrade..
                    If MessageBox.Show("Please Note- To continue with this version of JobMatixPOS, " & _
                                       "  there needs to be a DB schema upgrade-" & vbCrLf & _
                                       "  ie. The SupplierCode columns need to be expanded to 40-chars.." & vbCrLf & vbCrLf & _
                                       "Please make sure that the JobMatix database has been backed up, and " & vbCrLf & _
                                        "  that all other JobMatix users have been closed down." & vbCrLf & vbCrLf & _
                                        " Is it OK to continue now with the update ?", "JobMatixPOS DB Update", _
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                        '= Application.Exit()
                        mbUpgradeWasRefused = True
                        Exit Function  '--stop it falling through.
                    End If

                    '-- ok to go on and do it..
                    If Not mbExecuteSql(mCnnSql, sSupCodeSql, False, Nothing) Then
                        MsgBox("Update has failed..", MsgBoxStyle.Exclamation)
                        Application.Exit()
                    End If
                    '-- update the build no..
                    Dim asChanges(1) As String '--update list..-
                    Try
                        asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
                        asChanges(1) = Trim(msDllversion)
                        If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                            MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
                        Else
                        End If
                    Catch ex As Exception
                        MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    End Try
                    MsgBox("OK.. Schema Update has succeeded..", MsgBoxStyle.Information)
                    '-- exists
                Else
                    '- doesn't exist-
                    '-- nothing to do now.  Job was done before..
                    '= MsgBox("No primary Key constraint found. ", MsgBoxStyle.Information)
                End If  '-count=
            Else '-nothing-
                '-error-
                MsgBox("ERROR checking PKEY for SupplierCode Table..  NOTHING returned..", MsgBoxStyle.Exclamation)
            End If  '-nothing.
        Else
            '-get failed..
            MsgBox("ERROR checking PKEY for SupplierCode  Table.." & vbCrLf & _
                             gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        End If  '-get-

        '=
        '== For Build 4221.
        '==   3.  Tags- 25-Jan-2020.. For Build 4221..
        '== -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
        '== -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
        '== -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
        '== 
        '-- THIS to go to startup..
        '-- THIS to go to startup..

        '= Dim sColDef, smsg As String
        '= Dim bWasAdded As Boolean
        '- Must have customer tags-
        sMsg = ""
        bWasAdded = False
        sColDef = " VARCHAR(2000) NOT NULL DEFAULT '' "
        If Not gbAddColumnToTable(mCnnSql, "Customer", "Tags", sColDef, bWasAdded) Then
            MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
        Else  '-ok=
            If bWasAdded Then
                sMsg &= "-- The new column 'Tags' " & vbCrLf &
                       "  was added (ok) to the Customer Table.." & vbCrLf
            End If
        End If
        '- Report new customer columns..
        If sMsg <> "" Then
            MsgBox("Table updated- " & vbCrLf & sMsg, MsgBoxStyle.Information)
        End If

        '-- END of  "THIS to go to startup"..

        '= 06-June-2020=
        '==  Target is new Build 4251..
        '==  Target is new Build 4251..
        '==  
        '==   THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
        '==       --  Involves creating a REFUND DETAILS EXTRA Option (radioButton) to be able to refund same types as Payments..
        '==                 ie dropdown including ZipPay, bank Deposit etc..
        '==             NOTE- Sales form will keep Refund Options Frame for continuity of process,
        '==                   allowing Refunds to Cash, CreditNote or EftPos as always..
        '==                BUT an extra Option (OTHER- Choose From List) will allow user to see a DropDown Combo
        '==                      of remaining keys from master List so User can choose ONE only..
        '==                ALSO TWO new columns to be added to Payments Table-  
        '==                      viz- "RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).
        '==                         to Cash/CreditNote/EftPosDr/EftPosCr already recorded in Payment record.
        '==

        '--  ALL THIS to go to startup..
        '-- THIS to go to startup..
        '-- THIS to go to startup..  in LicenceCheckPOS..

        '==  Add Columns to Payments Table.
        '--    RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).

        Dim bColumnExists As Boolean = False
        Dim sTableName As String = "Payments"
        Dim sColumnName1 As String = "RefundOtherDetailAmount"
        Dim sColumnName2 As String = "RefundOtherDetailKey"

        If Not mbDoesColumnExist(sTableName, sColumnName1, bColumnExists) Then
            MsgBox("Error in checking column name." & vbCrLf, MsgBoxStyle.Exclamation)
        Else ''-ok-
            If Not bColumnExists Then
                '-- needs updating.
                '-- ask user consent to upgrade..
                If MessageBox.Show("Please Note- To continue with this version of JobMatixPOS, " & _
                                   "  there needs to be a DB schema upgrade-" & vbCrLf & _
                                   "  ie. New columns need to be added to the Payments Table." & vbCrLf & _
                                   "  So that extended Refund Types can be accommodated.." & vbCrLf & vbCrLf & _
                                   "Please make sure that the JobMatix database has been backed up, and " & vbCrLf & _
                                    "  that all other JobMatix users have been closed down." & vbCrLf & vbCrLf & _
                                    " Is it OK to continue now with the update ?", "JobMatixPOS DB Update", _
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                    '==Application.Exit()  DOES weird stuff !!
                    mbUpgradeWasRefused = True
                    Exit Function  '--stop it falling through.
                End If

                '--ok  do it..
                sMsg = ""
                bWasAdded = False
                sColDef = " MONEY NOT NULL DEFAULT 0 "
                If Not gbAddColumnToTable(mCnnSql, "Payments", "RefundOtherDetailAmount", sColDef, bWasAdded) Then
                    MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
                Else  '-ok=
                    If bWasAdded Then
                        sMsg &= "-- The new column 'RefundOtherDetailAmount' " & vbCrLf &
                               "  was added (ok) to the Payments Table.." & vbCrLf
                    End If
                End If
                '-RefundOtherDetailKey-
                bWasAdded = False
                sColDef = " NVARCHAR(32) NOT NULL DEFAULT '' "
                If Not gbAddColumnToTable(mCnnSql, "Payments", "RefundOtherDetailKey", sColDef, bWasAdded) Then
                    MsgBox("Error in 'gbAddColumnToTable'." & vbCrLf, MsgBoxStyle.Exclamation)
                Else  '-ok=
                    If bWasAdded Then
                        sMsg &= "-- The new column 'RefundOtherDetailKey' " & vbCrLf &
                               "  was added (ok) to the Payments Table.." & vbCrLf
                    End If
                End If
                '- Report new Payment cols..
                If sMsg <> "" Then
                    MsgBox("Table updated- " & vbCrLf & sMsg, MsgBoxStyle.Information)
                End If
            Else  '-exists-
                '-ok-
            End If  '-bColumnExists-
        End If  '--does exist

        '-- END of  "THIS to go to startup"..
        '== END OF Target is new Build 4251..


        mbUpgradeWasRefused = False

        LicenceCheckPOS = mbLicenceOK

    End Function  '-LicenceCheckPOS=
    '= = = = = = = = = = = = = = = = =

    '-- get max users..  Must follow Licence Check..

    Public Function GetMaxPosUsers() As Integer
        GetMaxPosUsers = mIntMaxUsersPermitted

    End Function  '-get max=
    '= = = = = = = = = = = = = = = = = == = = = = = ==  
    '-===FF->

    '-- Public methods..
    '-- Public methods..

    '--  Start  up POS Main from POS Shell or from Jobmatix --
    '--  This call redundant..  
    '-      frmPOS3Main stuff will be incorp. into JobMatix main form.-

    '== Public Function JMx31ShowPOS(ByRef cnnSql As OleDbConnection, _
    '==                              ByVal sSqlServerName As String, _
    '==                               ByVal sSqlDbName As String, _
    '==                               ByRef colSqlDBInfo As Collection, _
    '==                                ByVal intStaff_id As Integer, _
    '==                                 ByVal strStaffName As String) As Boolean
    '== Dim s1, sApppath As String
    '== Dim frmPOS3Main1 As frmPOS3Main
    '== Dim sVersion As String

    '==     sVersion = msGetDllversion()  '--set app path and get version..
    '== '-  new log each month..-
    '==     s1 = VB.Format(Now, "yyyy-MM-dd")
    '==     Call gbLogMsg(gsRuntimeLogPath, "JMx31ShowPOS is starting." & vbCrLf & "DLL Version: " & sVersion)
    '==     MsgBox("Hello from JMx31ShowPOS.." & vbCrLf & " Version: " & sVersion, MsgBoxStyle.Information)
    '==    Try
    '== '--  load POS3  Main form and show it..
    '==         frmPOS3Main1 = New frmPOS3Main
    '==     Catch ex As Exception
    '==         MsgBox("Error in loading POS Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '==         Exit Function
    '==     End Try
    '== '--show-
    '==     Try
    '==         frmPOS3Main1.connectionSql = cnnSql
    '==         frmPOS3Main1.SqlServer = msServer
    '==         frmPOS3Main1.DBname = sSqlDbName
    '==         frmPOS3Main1.dbInfoSql = colSqlDBInfo
    '==         frmPOS3Main1.StaffId = intStaff_id
    '==        frmPOS3Main1.StaffName = strStaffName
    '==         frmPOS3Main1.dllVersion = sVersion
    '==         frmPOS3Main1.ShowDialog()
    '==     Catch ex As Exception
    '==         MsgBox("Error in showing POS Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '==     End Try
    '== End Function  '- JMx31ShowPOS-
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Create POS tables in the JobMatix database. --

    'Public Function JMx31CreatePOS(ByRef cnnSql As OleDbConnection, _
    '                               ByVal sSqlDbName As String, _
    '                                 ByRef sCreateLog As String, _
    '                                 ByRef intErrorCount As Integer) As Boolean
    '    Dim ok As Boolean
    '    Dim sVersion As String
    '    Dim asChanges(1) As String '--update list..-

    '    mSysInfo1 = New clsSystemInfo(cnnSql)

    '    sVersion = msGetDllversion()  '--set app path and get version..
    '    Call gbLogMsg(gsRuntimeLogPath, "JMx31CreatePOS is starting." & vbCrLf & "DLL Version: " & sVersion)
    '    Call gbLogMsg(sCreateLog, "JMx31CreatePOS is starting." & vbCrLf & "DLL Version: " & sVersion)

    '    '-- call modCreate..--

    '    '-- gbCreatePOSdbTables =
    '    ok = gbCreatePOSdbTables(cnnSql, sSqlDbName, sCreateLog, intErrorCount)

    '    '==3403.429-- 29Apr2017=
    '    '=- = Create LayBy Tables for POS database = = =

    '    If ok Then
    '        ok = gbCreatePOSLaybyTables(cnnSql, sSqlDbName, sCreateLog, intErrorCount)
    '        If ok Then
    '            '-- updated build no..
    '            Try
    '                asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
    '                asChanges(1) = Trim(sVersion)
    '                If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
    '                    MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
    '                Else
    '                End If
    '            Catch ex As Exception
    '                MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '            End Try
    '        End If
    '    End If

    '    '==  3411.1125 -- Add TABLES for Subscriptions..
    '    If ok Then
    '        ok = gbCreatePOSSubscriptionTables(cnnSql, sSqlDbName, sCreateLog, intErrorCount)
    '        If ok Then
    '            '-- updated build no..
    '            Try
    '                asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
    '                asChanges(1) = Trim(sVersion)
    '                If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
    '                    MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
    '                Else
    '                End If
    '            Catch ex As Exception
    '                MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '            End Try
    '        End If
    '    End If

    '    JMx31CreatePOS = ok

    'End Function '--JMx31CreatePOS--
    '= = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--GoodsReturned-
    '-  Must follow "new-2" to have set up connection/DB info..

    '==  (instantiated and called from JobMatix RA.s).

    '-- INSERT base record into the SupplierReturns Table, 
    '--  a record for each RA item into the SupplierReturnsLine Table 
    '--    and update stock and Serial Info as reqd for each RA item ..

    '- colRA_items is a collection of RA-item- Each Item is a collection of RA info--

    '-Info keys for each RA Item in collection correspond to the JobMatix RAItems Table cols.:-
    '-- eg:
    '--  serialAudit_id int NOT NULL DEFAULT -1,"
    '--  ra_id int NOT NULL DEFAULT -1,"     '=Jobmatix RA-id-
    '--  RA_SupplierRMA_No nvarChar(60) NOT NULL DEFAULT '', "
    '--  RM_stockId int NOT NULL REFERENCES Stock(stock_id),"
    '--  RA_SerialNumber nvarChar(40) NOT NULL DEFAULT '',"
    '--  RM_invoiceNo nvarChar(20) NOT NULL DEFAULT '',"
    '--  RM_ItemBarcode nvarChar(40) NOT NULL DEFAULT '',  "
    '--  RM_ItemDescription nvarChar(40) NOT NULL DEFAULT '', "
    '--  quantity int NOT NULL DEFAULT 0,"
    '--  RA_Symptoms nvarChar(511) NOT NULL DEFAULT '', "
    '--  RA_RMA_RequestNotes nvarChar(2040) NOT NULL DEFAULT '', "

    '-- NB: We MUST execute "sRA_Udate_Sql" as part of the overall TRANSACTION-
    '--  (This updates the RA Item with Goods Sent Status etc)..

    Public Function POS_GoodsReturned(ByVal intStaff_id As Integer, _
                                     ByVal sStaffName As String, _
                                      ByVal intSupplier_id As Integer, _
                                       ByVal sComments As String, _
                                       ByRef colRA_items As Collection, _
                                        ByVal sRA_Udate_Sql As String) As Boolean
        Dim sResultText As String = ""

        POS_GoodsReturned = False
        If mCnnSql Is Nothing Then
            MsgBox("-POS_GoodsReturned Failed " & vbCrLf & _
                      "No valid SQL Connection.." & " .. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        Dim sysInfo1 As New clsSystemInfo(mCnnSql)

        If (intSupplier_id <= 0) Or ((colRA_items Is Nothing) OrElse (colRA_items.Count <= 0)) Then
            MsgBox("-POS_GoodsReturned Failed " & vbCrLf & _
                      "No valid supplier or RA info was offered.." & " .. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '-TEST-
        '== MsgBox("POS- Updating Returns for " & colRA_items.Count & "  items..", MsgBoxStyle.Information)

        '-- ok- we should have some items..
        Dim oleDbTran1 As OleDbTransaction
        Try  '-main try-
            Dim sSql, s1, sGoodsTaxcode As String  '= = ""
            Dim sFields, sValues As String
            Dim intCount As Integer
            Dim intReturn_id, intReturnLine_id, intID As Integer
            Dim intRA_id, intSerial_id, intStock_id, intQty As Integer
            Dim colItemCosts, colItem As Collection
            Dim decTotal_ex, decTotal_inc, decCostExTax, decCostIncTax As Decimal
            Dim decGST_rate As Decimal = 10D    '--default. value-  get from setup.
            Dim datatable1 As DataTable
            Dim datarow1 As DataRow
            Dim sSerialNo, sSupplierRMA, sHistory As String

            If sysInfo1.contains("GSTPercentage") Then
                s1 = sysInfo1.item("GSTPercentage")
                If IsNumeric(s1) Then
                    decGST_rate = CDec(s1)
                End If
            End If

            sResultText = ""
            decTotal_ex = 0
            decTotal_inc = 0
            colItemCosts = New Collection

            '-- compute all RA items costs first, so we can cost main Return Record..
            '--   Collect all item costs for ReturnLines lines--

            For Each colRA1 As Collection In colRA_items
                intStock_id = colRA1.Item("RM_stockId")
                intRA_id = colRA1.Item("RA_id")
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [stock] WHERE (stock_id=" & CStr(intStock_id) & ");"
                If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
                                       (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then
                    datarow1 = datatable1.Rows(0)
                    sGoodsTaxcode = UCase(datarow1.Item("goods_taxcode"))
                    '-costExTax-
                    decCostExTax = CDec(datarow1.Item("costExTax"))  '--rrp ex tax-
                    '-- Must add TAX..
                    decCostIncTax = decCostExTax
                    If sGoodsTaxcode = "GST" Then
                        decCostIncTax = decCostExTax + (decCostExTax * decGST_rate / 100)
                    End If
                    colItem = New Collection
                    colItem.Add(sGoodsTaxcode, "goods_taxcode")
                    colItem.Add(decCostExTax, "cost_ex")
                    colItem.Add(decCostIncTax, "cost_inc")
                    colItemCosts.Add(colItem, CStr(intRA_id))
                    '-- add to totals-
                    decTotal_ex += decCostExTax
                    decTotal_inc += decCostIncTax
                Else  '-no record-
                    MsgBox("-POS_GoodsReturned Failed " & vbCrLf & _
                                 "No stock record for stock_id: " & intStock_id & " .. ", MsgBoxStyle.Exclamation)
                    Exit Function
                End If '-get-
            Next colRA1

            '-- ok.. START transaction-
            oleDbTran1 = mCnnSql.BeginTransaction

            '-1.- Execute RAs update-
            If Not mbExecuteSql(mCnnSql, sRA_Udate_Sql, True, oleDbTran1) Then
                MsgBox("-POS_GoodsReturned- RA-Update FAILED-  See Runtime Log...")
                Exit Function
            End If  '--exec invoice-
            sResultText &= "-- POS RA Update: " & vbCrLf & sRA_Udate_Sql & vbCrLf & _
                                "    was Completed ok.." & vbCrLf & vbCrLf

            '-2.- Insert Returns base record..
            sSql = "INSERT INTO dbo.SupplierReturns ("
            sSql &= "  staff_id, staff_name, supplier_id, total_ex, total_inc, comments) "
            sValues = "VALUES ( " & CStr(intStaff_id) & ", '" & gsFixSqlStr(sStaffName) & "', " & _
                          CStr(intSupplier_id) & ", " & CStr(decTotal_ex) & ", " & CStr(decTotal_inc) & ", " & _
                          " '" & gsFixSqlStr(sComments) & "' );"
            If Not mbExecuteSql(mCnnSql, sSql & sValues, True, oleDbTran1) Then
                MsgBox("Inserting SupplierReturns record has FAILED.." & vbCrLf & _
                         "Progress text was: " & sResultText & vbCrLf, MsgBoxStyle.Exclamation)
                Exit Function
            End If  '--exec invoice-

            '- 3. Retrieve Return No. (IDENTITY of REturn record written.)-
            sSql = "SELECT CAST(IDENT_CURRENT ('dbo.SupplierReturns') AS int);"
            If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, oleDbTran1, intID) Then
                intReturn_id = intID
            Else
                MsgBox("Failed to retrieve latest Return No..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            sResultText &= "-- SupplierReturns record: " & intReturn_id & " inserted ok.." & vbCrLf

            '-4.-  Insert ReturnLines.. one for each RA sent back..
            '-- for each RA, Update stock ans serial audit.
            sSql = ""
            intCount = 0
            For Each colRA1 As Collection In colRA_items
                intRA_id = colRA1.Item("RA_id")
                intStock_id = colRA1.Item("RM_stockId")
                intSerial_id = colRA1.Item("RM_serialAudit_id")
                sSupplierRMA = colRA1.Item("RA_supplierRMA_no")
                '= 3411.0423-  fixes.
                colItem = colItemCosts.Item(CStr(intRA_id))

                decCostExTax = colItem.Item("cost_ex")
                decCostIncTax = colItem.Item("cost_inc")
                sGoodsTaxcode = colItem.Item("goods_taxcode")

                '-4a.-  Insert ReturnLine..
                sSql = "INSERT INTO dbo.SupplierReturnLine "
                sSql &= " (return_id, stock_id, serialAudit_id, "
                sSql &= "  serialNumber, invoice_no, ra_id, supplier_RMA_no, "
                sSql &= "  barcode, description, quantity, symptoms, request_notes,  "
                sSql &= "  goods_taxCode, cost_ex, cost_inc ) "

                sSql &= "VALUES ( " & CStr(intReturn_id) & ", " & CStr(intStock_id) & ", " & CStr(intSerial_id) & ", "
                sSql &= " '" & gsFixSqlStr(colRA1.Item("RA_serialNumber")) & "', "
                sSql &= " '" & gsFixSqlStr(colRA1.Item("RM_invoiceNo")) & "', "
                sSql &= CStr(intRA_id) & ", "
                sSql &= " '" & gsFixSqlStr(sSupplierRMA) & "', "
                sSql &= " '" & gsFixSqlStr(colRA1.Item("RM_ItemBarcode")) & "', "
                sSql &= " '" & gsFixSqlStr(colRA1.Item("RM_ItemDescription")) & "', "
                sSql &= CStr(colRA1.Item("quantity")) & ", "
                sSql &= " '" & gsFixSqlStr(colRA1.Item("RA_symptoms")) & "', "
                sSql &= " '" & gsFixSqlStr(colRA1.Item("RA_RMA_RequestNotes")) & "', "
                sSql &= " '" & gsFixSqlStr(sGoodsTaxcode) & "', "
                sSql &= CStr(decCostExTax) & ", "
                sSql &= CStr(decCostIncTax) & " "
                sSql &= "  ); " & vbCrLf
                '-- insert this row..-
                If Not mbExecuteSql(mCnnSql, sSql, True, oleDbTran1) Then
                    MsgBox("Insert SupplierReturnLine Failed..", MsgBoxStyle.Exclamation)
                    Exit Function
                End If  '--exec invoice LINE-
                '-get ID of last line inserted.. (For Serial-Audit-Trail).
                sSql = "SELECT CAST(IDENT_CURRENT ('dbo.SupplierReturnLine') AS int);"
                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, oleDbTran1, intID) Then
                    intReturnLine_id = intID  '-- this goes into serialAuditTrail below..-
                Else
                    MsgBox("Failed to read latest RETURN LINE-ID No..", MsgBoxStyle.Exclamation)
                    Exit Function
                End If

                '-4b. next- update stock count-
                intQty = CInt(colRA1.Item("quantity"))
                If (intQty > 0) Then
                    '- update stock count.
                    sSql = "UPDATE dbo.stock SET "
                    sSql &= " qtyInStock=(qtyInStock -" & CStr(intQty) & ")"
                    sSql &= "  WHERE (stock_id = " & CStr(intStock_id) & " );"
                    If Not mbExecuteSql(mCnnSql, sSql, True, oleDbTran1) Then
                        MsgBox("Update RETURNS Stock Failed..")
                        Exit Function
                    End If  '--exec Update stock qty-
                End If
                '-stock done-
                intCount += 1
                sResultText &= "-- SupplierReturns Line: #" & intCount & " inserted ok.." & vbCrLf & _
                                 "  and Stock updated. "
                '--   IF SERIALized ->  UPDATE SerialAudit status -> SOLD. -
                '--            And ->  INSERT SerialAuditTrail record.. (SALE/REFUND (INSTOCK)).-
                intSerial_id = IIf(IsNumeric(colRA1.Item("RM_serialAudit_id")), CInt(colRA1.Item("RM_serialAudit_id")), -1)
                sSerialNo = Trim(colRA1.Item("RA_serialNumber"))
                If (intSerial_id > 0) Then
                    '-4c.  update serialAudit record...
                    sSql = "UPDATE dbo.serialAudit  "
                    sSql &= "SET status='RETURNED', isInStock=0 "
                    sSql &= "  WHERE (serial_id=" & CStr(intSerial_id) & ");"
                    If Not mbExecuteSql(mCnnSql, sSql, True, oleDbTran1) Then
                        MsgBox("Update RETURN SerialAudit Failed..", MsgBoxStyle.Exclamation)
                        Exit Function
                    End If  '--exec Update serial status-

                    '-4d.   add Trail record "return"..
                    '--ok.. insert SerialAuditTrail record.-
                    '- This is the "transaction" trail for this serial.-
                    sHistory = "Jobmatix-RA #" & CStr(intRA_id) & "; SupplierRMA: " & sSupplierRMA & ".."
                    sSql = "INSERT INTO dbo.SerialAuditTrail ("
                    sSql &= " stock_id, SerialAudit_id, "
                    sSql &= "  tran_type, type_id, type_line_id, movement, RM_tr_detail "
                    sSql &= ") "
                    sSql &= "VALUES ( "
                    sSql &= CStr(intStock_id) & ", " & CStr(intSerial_id) & ", "
                    sSql &= "'return', "
                    sSql &= CStr(intReturn_id) & ", " & CStr(intReturnLine_id) & ", -1, "
                    sSql &= " '" & gsFixSqlStr(sHistory) & "' "
                    sSql &= "); "
                    '-- insert this TRAIL rec..-
                    If Not mbExecuteSql(mCnnSql, sSql, True, oleDbTran1) Then
                        MsgBox("Saving Serial Audit TRAIL record FAILED..", MsgBoxStyle.Exclamation)
                        Exit Function
                    End If  '--exec serial-
                    sResultText &= " And SerialAudit Id: " & intSerial_id & " Updated ok.."
                End If  '-serial-
                sResultText &= vbCrLf & vbCrLf
            Next colRA1
            '-- COMMIT the lot..

            '- 5.  Commit TRANSACTION.-
            Try
                oleDbTran1.Commit()
                Call gbLogMsg(msRuntimeLogPath, sResultText & vbCrLf & _
                                                 "-POS_GoodsReturned- Transaction committed ok.." & vbCrLf)
                MsgBox(sResultText & vbCrLf & "-POS_GoodsReturned- " & vbCrLf & _
                                        " Transaction committed ok..", MsgBoxStyle.Information)
            Catch ex As Exception
                oleDbTran1.Rollback()
                MsgBox(sResultText & vbCrLf & "Transaction commit FAILED.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Exit Function
            End Try  '-commit-
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            POS_GoodsReturned = True
            Exit Function

        Catch ex As Exception
            oleDbTran1.Rollback()
            MsgBox("-POS_GoodsReturned function Failed " & vbCrLf & ex.Message & vbCrLf & _
                      "Result text: " & sResultText & " .. ", MsgBoxStyle.Exclamation)
            Call gbLogMsg(msRuntimeLogPath, "-POS_GoodsReturned updating Failed " & vbCrLf & ex.Message & " .. ")

        End Try  '-main try-

    End Function '-pos_GoodsReturned-
    '= = = = = = = = = = = = = ==  = = = = =
End Class  '=clsJMxPOS31-
'= = = = = = = =  = = = ==  =
'-===FF->

'--clsJMxCreatePOS -
'--clsJMxCreatePOS -
'--clsJMxCreatePOS -

Public Class clsJMxCreatePOS

    Private mSysInfo1 As clsSystemInfo
    Private msAppPath As String = ""

    Private Function msGetDllversion() As String
        Dim assemblyThis As Assembly
        Dim assName As AssemblyName
        Dim s1, sVersion As String

        'msAppPath = My.Application.Info.DirectoryPath
        'If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        'gsAppPath = msAppPath

        '=3501.0809=
        Dim sAppPath As String = gsAppPath()
        If VB.Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
        msAppPath = sAppPath

        '-  new log each month..-
        '= s1 = VB.Format(Now, "yyyy-MM-dd")
        '= gsErrorLogPath = gsJobMatixLocalDataDir("JobMatix34") & "\JMxPOS340-Runtime-" & VB.Left(s1, 7) & ".log"
        '= gsRuntimeLogPath = gsErrorLogPath()  '--gsAppPath & "JTv3_Runtime.log"

        assemblyThis = System.Reflection.Assembly.GetExecutingAssembly()
        assName = assemblyThis.GetName
        With assName.Version
            sVersion = CStr(.Major) & "." & CStr(.Minor) & "." & CStr(.Build) & "." & CStr(.Revision)
        End With

        msGetDllversion = sVersion
    End Function  '--get version-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    Public Function JMx31CreatePOS(ByRef cnnSql As OleDbConnection, _
                               ByVal sSqlDbName As String, _
                                 ByRef sCreateLog As String, _
                                 ByRef intErrorCount As Integer) As Boolean
        Dim ok As Boolean
        Dim sVersion As String
        Dim asChanges(1) As String '--update list..-

        mSysInfo1 = New clsSystemInfo(cnnSql)

        sVersion = msGetDllversion()  '--set app path and get version..
        Call gbLogMsg(gsRuntimeLogPath, "JMx31CreatePOS is starting." & vbCrLf & "DLL Version: " & sVersion)
        Call gbLogMsg(sCreateLog, "JMx31CreatePOS is starting." & vbCrLf & "DLL Version: " & sVersion)

        '-- call modCreate..--

        '-- gbCreatePOSdbTables =
        ok = gbCreatePOSdbTables(cnnSql, sSqlDbName, sCreateLog, intErrorCount)

        '==3403.429-- 29Apr2017=
        '=- = Create LayBy Tables for POS database = = =

        If ok Then
            ok = gbCreatePOSLaybyTables(cnnSql, sSqlDbName, sCreateLog, intErrorCount)
            If ok Then
                '-- updated build no..
                Try
                    asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
                    asChanges(1) = Trim(sVersion)
                    If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                        MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
                    Else
                    End If
                Catch ex As Exception
                    MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If
        End If

        '==  3411.1125 -- Add TABLES for Subscriptions..
        If ok Then
            ok = gbCreatePOSSubscriptionTables(cnnSql, sSqlDbName, sCreateLog, intErrorCount)
            If ok Then
                '-- updated build no..
                Try
                    asChanges(0) = "POS_program_version"  '= "POS_sell_margin"
                    asChanges(1) = Trim(sVersion)
                    If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                        MsgBox("Couldn't save " & "POS_program_version" & " setting.", MsgBoxStyle.Exclamation)
                    Else
                    End If
                Catch ex As Exception
                    MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
            End If
        End If

        JMx31CreatePOS = ok

    End Function '--JMx31CreatePOS--


End Class

'== the end ==
