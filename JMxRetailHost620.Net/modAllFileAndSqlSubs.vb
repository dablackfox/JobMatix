
Option Strict Off
Option Explicit On
Imports System
Imports System.Collections
Imports System.Data.Sql
Imports VB6 = Microsoft.VisualBasic
Imports System.IO
Imports System.Security.AccessControl
Imports System.Text
'= Imports System.Data.sqlclient
Imports System.Data.OleDb
'== Imports VB = Microsoft.VisualBasic.Compatibility.VB6
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading

Public Module modAllFileAndSqlSubs

    '== This Is to standardise interface to Files and Sql...

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

    '== grh JobMatix v3.5.3501.0615
    '==   -- 15-June-2018=
    '==     --  Created New Module "modAllFileAndSqlSubs"..
    '==           to combine All fileSupport and sql support Functions.
    '==        (Takes all content from and obsoletes modFileSupport33, modSqlSupport31, and modSqlInfoSchema31.)
    '==
    '== V3.5.3501.0615--
    '=   -- FIXES to COMBINED MODULE--
    '==
    '== V3.5.3501.0615--
    '==   -- SqlInoSchema..  Cut out excessive log reporting on Table info..
    '==
    '==    3501.0806 06-August-2018= 
    '==       -- Speeding up getSchema at startup.-
    '==
    '== -- Updated 3501.1223  23-Dec-2018=  
    '==     --  Add RETRY to gbLogMsg Append Writeln....
    '==
    '==  NEW DLL- 4219 VERSION
    '==    Created- 4219.1122 22-Nov-2019= 
    '==      --  Move Retail Host Interfaces and classes to HERE in JMxRetailHost.dll..
    '==      --  Make "modAllFileAndSqlSubs" Public HERE in JMxRetailHost.dll so EVERyONE can use it..
    '==
    '==      --  The Functions gsAssembly..." stuff to be private only, as users of this DLL have 
    '==              make their own arrangements about assembly infos..
    '==
    '==
    '=== = = = = = = = = = = == = = = = = = = = = = = = = = = = = = == =  = = = = = == = = = = = = = = = = = = =

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (16-June-2021)
    '==   Target-New-Build-6201 --  (16-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '= Module modFileSupport
    '= Module modFileSupport
    '= Module modFileSupport

    '== START of IMHERTTED FILE SUPPORT stuff.. --
    '== START of IMHERTTED FILE SUPPORT stuff.. --
    '== START of IMHERTTED FILE SUPPORT stuff.. --

    '= = = xbs subs = =

    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
    '---grh-- ==11-May-2005== -Fixed execSP -====
    '-----
    '---grh-- ==15-june-2005== -Fixed tablename (don't ucase)-====
    '---grh-----06-Sep-2005==  -Added file open and read dbf header info--
    '---grh-----15-Sep-2005==  -fix sql type_name in getSqlInfo--
    '-----------------------==    -Convert sql type_name to ADO type==
    '---grh-----22-Sep-2005==  -function to Get cmdline parm-
    '---grh-----02-Oct-2005==  -function to make/Save binary file-
    '---grh-----31-Oct-2005==  -ExecuteTrans--Return Errno when fails-
    '---grh-----24-Nov-2005==  -Log data--
    '---grh-----25-Nov-2005==  -GetInfo..  set up Parent collection for table--
    '---grh-----21-Dec-2005==  -IsDate, IsText..--
    '---grh-----27-Dec-2005==  -getUserTypes..--

    '== r e b o r n  from xbssubs = = --
    '----- to separate SQL-subs (from xbs stuff) as general subs for Explorer--
    '-- - - AND to make xbs conversion as sub-function of Explorer-- (see xbswiz.bas ) ==

    '---grh-----08-Oct-2007==  -create sqlSubs as Clone of xbsSubs..--
    '---grh-----19-Jun-2008==  -for Jet--add ADOX catalogue (gbGetJetDbInfo)..--

    '---grh-----01-Apr-2009==  -for JobTracking.  SQL and Jet-- Login screens only if no info available..--
    '---grh-----01-Jul-2009==  -for JobTracking.  Jet DB does not get loaded into global list of connections....--
    '---grh-----04-Sep-2009==  -for JobTracking.  Add "gbGrantDBAccess" and "gbAddRoleMember"...--
    '---grh-----17-Sep-2009==  -for JobTracking.  REVAMP "getUsers" into "getUsersEx"...--
    '---grh-----15-Oct-2009==  -for JobTracking-V2.  Drop sqllogin (see "modJobsSubs")....--
    '---grh-----11-Dec-2009==  -for JobTracking.  Update "getUsersEx"... Add GroupName col. to result Collection.--
    '---grh-----20-Feb-2009==  -for JobTracking.  Jet Connect for ADO/OLEDB..  Use Jet 4.0.--
    '----     ---     Gives better result retrieving RA Supplier Invoices..-- (BIG JOIN..)==
    '---grh-----13-Mar-2010==  -for JobTrackingV2..  gbExecuteCmd logs errors...
    '---grh-----14-Jun-2010==  -for JobTrackingV2.. gbMakeTextSearchSql to build multi-column text search..
    '---grh-----03-Sep-2010==  -for JobTrackingV2..  Fix "gbMakeTextSearchSql" for embedded quotes..
    '---grh-----21-Jul-2011==  -for Jobmatix 2912. and SQL-Server-2008.  Extend getUsersEx..
    '---   ALSO:       ---"who_using" only needs to worry about "runnable" tasks..--

    '---grh- ==23-Nov-2011== -for Jobmatix 3013++.   UPGRADED VERSION for vb.net..
    '---grh- ==25-Mar-2012== -for Jobmatix 3031.3.   Fixes for Message logging....
    '==
    '---grh- ==11-Oct-2012== -for Jobmatix 3069.0   Fixes for "who_using"....
    '== grh  = 13-Oct-2012== 
    '==  Build-3069.0/3071.0= -- Check for a specific permission..-
    '==     and GRANT permission for  "VIEW SERVER STATE"  --
    '==  Also- 3071.0=  Upgrade sp_whoUsing" for SQL Server 2005..
    '--
    '==
    '== grh 12-Feb-2013= Build-3072/3073= --
    '==  modSqlSubs3 ==  
    '==   >> Big cleanup..  REMOVE all UI stuff (eg DoEvents, Mousepointers, MsgBox's etc)..-
    '==         and set up Function to RETRIEVE LAST error message.  --
    '==   >> Remove function "gbLogonJet"  
    '--        SEE gbJetLogin  in modJetLogin.vb..  ===
    '==
    '==  
    '== grh 09-Mar-2013= Build-3073.309= --
    '==  modSqlSubs3 ==  
    '--      exists database--  Use DB_ID to check existence..
    '==
    '==    =3073.311= Add function 'gbCheckUserAccess' ----
    '==         and add Function 'gbAddNewUser'
    '==
    '==    =3077.519= Add function 'gbSetCurrentDatabase' ----
    '==         And function 'gbGrantVWSSPermission' now calls 'gbSetCurrentDatabase'..
    '==
    '==
    '==    =3083.210= Add function 'gbSQL_Enumerate_Main' ----
    '==         for sql login if no Server supplied.
    '==
    '==
    '==  grh= 16Feb2014--
    '--  JobMatixPOSadmin Build 1.0.1001.0
    '==       Now is FILE FUNCTIONS ONLY in this Module..
    '==
    '==  grh= 16Feb2014--
    '==   Use system.environment for GetUserName..    
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = ==  
    '== 
    '==  grh. JobMatix 3.1 ---  13-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb 
    '==          (dropped sqlClient).. (For Jet OleDb driver).
    '==
    '==  grh. JobMatix 3.1.3102.1005 ---  05-Oct-2014 ===
    '==       >> Fixes to showSqlInfo..
    '==
    '==  grh. JobMatix 3.1.3101.1110 ---  10-Nov-2014 ===
    '==   >>  Add function to get Avail printers. 
    '==
    '==  grh. JobMatix 3.1.3107.0801 ---  01-Aug-2015 ===
    '==   >>  JOBS Settings file now in CommonApplicationData--
    '==   >>   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)   
    '==   >>   gbLogMsg and glSaveTextFile wasn't/weren't reporting open error..
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >>  Get the POSSettings dir from clsWinSpecial--
    '==   >>  Move up to .Net 4.5.2 and
    '==               add "everyone" ACL to Log/txt files. --
    '==   >>  THIS module file to be used for POS system ALSO !!!!!
    '==   >>  Re-do "gbLogMsg", "glSaveTextFile" and "glLoadDataFile" for ACL and updated file handling !!!
    '==
    '==   grh 3107.1213-  13-Dec-2015=
    '==          Fix to gsJobMatixLocalDataDir()  to ADD CurrentUser as SubDirectory.- 
    '==                       (needed for Gavin for Terminal Services aka RDS operation.)
    '==
    '==  VERSION 3.2==
    '==     grh  3203.102==
    '==         >>Public Function gbIsImageFile(ByVal sSuffix As String) As Boolean
    '==         >> 3203.105- Upgrade Data Dir. to "\JobMatix32\"..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  VERSION 3.3==
    '==  grh. 3311.224= 24Feb2016- 
    '==      >>  Add Parm "appName" to gsJobmatixLocalDataDir...
    '==      >>  Settings and systemInfo functions obsoleted....
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Public Const gK_APP_NAME As String = "JobMatix00"

    Public Const gK_POS_SETTINGS_PATH As String = "localPOSSettings.txt"
    Public Const gK_JOBS_SETTINGSPATH As String = "localJOBSettings.txt"

    Public Const gK_SETTING_PRTCOLOUR As String = "PRTCOLOUR"
    Public Const gK_SETTING_PRTRECEIPT As String = "PRTRECEIPT"
    Public Const gK_SETTING_PRTLABEL As String = "PRTLABEL"

    '--P u b l i c  variables--
    '--P u b l i c  variables--

    Public giSelectTimeout As Short
    '==Public giSelectTimeout   As Integer
    Public gbDebug As Boolean
    Public gbDevel As Boolean
    Public gbVerbose As Boolean
    Public gbclosingDown As Short

    '--create stuff--
    '--Public gsDBUserName As String

    Public giSQLerrors As Short

    Public gsFldSep As String '--export fld sep.--
    Public gsRowTerm As String '--export row terminator

    '== 3101= Public gsLogData As String '--accumulate log lines--

    '== V3.5.3501.0615--
    '=   -- FIXES to COMBINED MODULE--

    '---   Make ErrorLogPath and RuntimeLogPath into FUNCTIONS !!
    '== Public gsErrorLogPath As String
    '= Public gsRuntimeLogPath As String
    '-- and Assembly name-
    '= Public gsAssemblyName As String = ""

    '= Public gsAppPath As String
    '==  Public gsAppName As String
    '= = = = = = = = = = = = = = =

    Private msLastFileErrorMessage As String = ""

    Private clsWinInfo1 As New clsWinSpecial
    Private msJobMatixAppName As String = ""
    Private msJobMatixAppVersion As String = ""
    '= = = = = = = = = = = = = = = = = = = = = =


    '--  get Current User ---
    '--  get Current User ---
    '== Private Declare Function GetUserName Lib "advapi32.dll" _
    '==                      Alias "GetUserNameA" (ByVal lpBuffer As StringBuilder, _
    '==                                                ByRef nSize As Integer) As Integer

    '-- The HTML Help..--
    '-- The HTML Help..  --

    '-- The HTML Help constants are defined as follows:
    Const HH_DISPLAY_TOPIC As Short = &H0S
    Const HH_SET_WIN_TYPE As Short = &H4S
    Const HH_GET_WIN_TYPE As Short = &H5S
    Const HH_GET_WIN_HANDLE As Short = &H6S
    Const HH_DISPLAY_TEXT_POPUP As Short = &HES ' Display string resource ID or
    ' text in a pop-up window.
    Const HH_HELP_CONTEXT As Short = &HFS ' Display mapped numeric value in
    ' dwData.
    Const HH_TP_HELP_CONTEXTMENU As Short = &H10S ' Text pop-up help, similar to
    ' WinHelp's HELP_CONTEXTMENU.
    Const HH_TP_HELP_WM_HELP As Short = &H11S ' text pop-up help, similar to
    ' WinHelp's HELP_WM_HELP.

    '-- The HtmlHelp() function is declared as follows:
    Declare Function HtmlHelp Lib "hhctrl.ocx" _
                     Alias "HtmlHelpA" (ByVal hwndCaller As Integer, _
                                             ByVal pszFile As String, _
                                                ByVal uCommand As Integer, _
                                        ByVal dwData As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = === 
    '-===FF->

    '== V3.5.3501.0615--
    '=   -- FIXES to COMBINED MODULE--

    '==  4219.1128=  The Functions gsAssembly..." stuff to be private only, as users of this DLL have 
    '==              make their own arrangements about assembly infos..
    '==
    '==

    '-- Assembly name..
    '=Public Function gsAssemblyName() As String
    Private Function msAssemblyName() As String

        '=3501.0611-  MUST get actual assembly ie DLL..
        Dim assemblyThis As Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim assName As AssemblyName
        assName = assemblyThis.GetName
        '= modFileSupport33-
        msAssemblyName = Replace(Replace(assName.Name, ".exe", ""), ".dll", "")  '-replace in case.
        'With assName.Version
        '    msJobMatixVersion = gsAssemblyName & " v:" & CStr(.Major) & "." & CStr(.Minor) & ". Build: " & _
        '                          CStr(.Build) & "." & CStr(.Revision)
        'End With
    End Function  '-assembly name-
    '= = = = = = = == = = = = =  =

    '-- Assembly Version..
    '= Public Function gsAssemblyVersion() As String
    Private Function msAssemblyVersion() As String
        Dim sAssemblyName As String

        '=3501.0611-  MUST get actual assembly ie DLL..
        Dim assemblyThis As Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim assName As AssemblyName
        assName = assemblyThis.GetName
        '= modFileSupport33-
        sAssemblyName = Replace(Replace(assName.Name, ".exe", ""), ".dll", "")  '-replace in case.
        With assName.Version
            msAssemblyVersion = "v:" & CStr(.Major) & "." & CStr(.Minor) & ". Build: " & _
                                                               CStr(.Build) & "." & CStr(.Revision)
        End With
    End Function  '-assembly name-
    '= = = = = = = == = = = = =  =
    '-===FF->

    '--gsAppPath-

    Public Function gsAppPath() As String
        gsAppPath = My.Application.Info.DirectoryPath

    End Function '-gsAppPath-
    '= = = = = = = == = = = = = 

    '-- main line must SET appName.. (comes from highest level assembly..

    Public Sub gSubSetAppName(ByVal strAppname As String)
        '-save-
        msJobMatixAppName = strAppname

    End Sub  '- gSubSetAppName-
    '= = = = = = =  = = = = = =

    '-- Return pre-set app name.  eg. JobMatix35  ===

    Public Function gsGetAppName() As String
        gsGetAppName = msJobMatixAppName

    End Function '-gsGetAppName-
    '= = = = = = = == = = = = = 

    '==4219.1128-

    '--Application version..

    Public Sub gSubSetAppVersion(ByVal strAppVersion As String)
        '-save-
        msJobMatixAppVersion = strAppVersion

    End Sub  '- gSubSetAppversion-
    '= = = = = = =  = = = = = =

    '-- Return pre-set app name.  eg. JobMatix35  ===

    Public Function gsGetAppVersion() As String
        gsGetAppVersion = msJobMatixAppVersion

    End Function '-gsGetAppVersion-
    '= = = = = = = == = = = = =  =
    '-===FF->

    '---   Make ErrorLogPath and RuntimeLogPath into FUNCTIONS !!

    Public Function gsErrorLogPath() As String

        '-  new log each month..-
        Dim s1 As String = Format(CDate(DateTime.Today), "yyyy-MM-dd")

        gsErrorLogPath = gsJobMatixLocalDataDir(gsGetAppName) & "\" & gsGetAppName() & "-Runtime-" & VB6.Left(s1, 7) & ".log"

    End Function '-gsErrorLogPath
    '= = = = = = ==  = = = = = == = = = = =

    Public Function gsRuntimeLogPath() As String

        gsRuntimeLogPath = gsErrorLogPath()
    End Function '-gsRuntimeLogPath
    '= = = = = = ==  = = = = = == = = = = =
    '-===FF->

    '= 3203.117==
    '- Warranty Colour..

    Public Function gIntUnderWarrantyColour() As Integer

        gIntUnderWarrantyColour = RGB(148, 0, 211)   '--dark violet.

    End Function  '-Warranty colour-
    '= = = = = = = = = = = = = == = =

    '= 3203.117==
    '- Pr-1 Colour..

    Public Function gIntPriority1Colour() As Integer

        gIntPriority1Colour = RGB(&H87, &HCE, &HFA)   '--light sky blue. --
    End Function  '-Normal colour-
    '= = = = = = = = = = = = = == = =
    '= 3203.117==
    '- Pr-2 Colour..

    Public Function gIntPriority2Colour() As Integer

        gIntPriority2Colour = RGB(255, 192, 203) '--pink (255,192,203) (-#FFC0CB)-
    End Function  '-P2 colour-
    '= = = = = = = = = = = = = == = =

    '= 3203.117==
    '- Pr-3 Colour..

    Public Function gIntPriority3Colour() As Integer

        '=3203.121=  gIntPriority3Colour = RGB(&HFFS, &H0S, &HFFS) '--magenta --
        gIntPriority3Colour = RGB(255, 20, 147) '--deep pink-  #ff1493- --

    End Function  '-P3 colour-
    '= = = = = = = = = = = = = == = =

    '- Quote  RGB(255, 255, 0) '--orange.- '= mlPriorityColour

    Public Function gIntPriorityQuoteColour() As Integer

        gIntPriorityQuoteColour = RGB(255, 255, 0)  '-orange-

    End Function  '-Quote colour-
    '= = = = = = = = = = = = = == = =

    '-ON-SITE Job..  GoldenRod BG.--

    Public Function gIntPriorityOnSiteJobColour() As Integer

        gIntPriorityOnSiteJobColour = RGB(218, 165, 32)  '-GoldenRod-

    End Function  'On-Site colour-
    '= = = = = = = = = = = = = == = = = = = = = = = = = = = = = = = 
    '-===FF->

    '= 3203.102==
    '- g b I s I m a g e F i l e -

    '- Can supply full title or just the Extension..

    '=3311= GONE to clsAttachments.-

    'Public Function OBSOLETE_gbIsImageFile(ByVal sFileTitle As String) As Boolean
    '    Dim listTypes As New List(Of String)   '=== {"BMP", "JPG", "GIF", "PNG", "ICO"}
    '    Dim sSuffix As String
    '    Dim intPos1 As Integer

    '    OBSOLETE_gbIsImageFile = False
    '    '- check format..-
    '    intPos1 = InStrRev(sFileTitle, ".")
    '    If (intPos1 > 0) Then '--found-
    '        sSuffix = Mid(sFileTitle, intPos1 + 1)
    '    Else
    '        sSuffix = sFileTitle  '--we just got the extension.
    '    End If
    '    '--
    '    listTypes.Add("BMP")
    '    listTypes.Add("JPG")
    '    listTypes.Add("GIF")
    '    listTypes.Add("PNG")
    '    listTypes.Add("ICO")
    '    If listTypes.Contains(UCase(sSuffix)) Then
    '        OBSOLETE_gbIsImageFile = True
    '    End If
    'End Function '-is image-
    '= = = = = = = = =  = = == 
    '-===FF->

    '- EX POS dll-
    '- settings file path..--
    '==  3107.1213-  13-Dec-2015=
    '==          Fix to ADD CurrentUser as SubDirectory.- 
    '==                       (needed for Gavin for Terminal Services aka RDS operation.)
    '==
    '-[Create]-Return JobMatix Local data DIR.-

    Public Function gsJobMatixLocalDataDir(Optional ByVal strAppName As String = "") As String

        gsJobMatixLocalDataDir = ""
        '-- get programData dir.
        Dim s1 As String = clsWinInfo1.AppDataDir
        Dim sUser As String = Replace(gsGetCurrentUser(), " ", "_")
        Dim sUserDir As String
        Dim sProgramName As String = strAppName
        If (sProgramName = "") Then
            sProgramName = gsGetAppName()
            If (sProgramName = "") Then
                If (msAssemblyName() <> "") Then
                    sProgramName = msAssemblyName()
                Else '-default-
                    sProgramName = gK_APP_NAME
                End If
            End If
        End If '-omitted-
        If (Right(s1, 1) <> "\") Then
            s1 &= "\"
        End If
        Dim sJobMatixDir As String = s1 & sProgramName  '=  "JobMatix32"
        '- make Jobmatix sub Dir-
        If Not Directory.Exists(sJobMatixDir) Then
            '--make it-
            Try
                Directory.CreateDirectory(sJobMatixDir)
            Catch ex As Exception
                MsgBox("Failed to create directory: " & vbCrLf & _
                              sJobMatixDir & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                Exit Function
            End Try
        End If  '-exists-
        '- make Users sub Dir-
        sUserDir = sJobMatixDir & "\" & sUser
        If Not Directory.Exists(sUserDir) Then
            '--make it-
            Try
                Directory.CreateDirectory(sUserDir)
            Catch ex As Exception
                MsgBox("Failed to create User Data directory: " & vbCrLf & _
                              sUserDir & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                Exit Function
            End Try
        End If  '-exists-

        gsJobMatixLocalDataDir = sUserDir  '== s1 & "JobMatix31"

    End Function '-- gsJobMatixLocalDataDir --
    '= = = = =  = = = = == = = = = = = =
    '-===FF->

    '--Return Local JOBS SettingsPath-

    Public Function gsLocalJobsSettingsPath(Optional ByVal strAppName As String = "") As String
        Dim sName As String = strAppName
        'If (sName = "") Then
        '    If (msAssemblyName() <> "") Then
        '        sName = msAssemblyName()
        '    Else '-default-
        '        sName = gK_APP_NAME
        '    End If
        'End If '-omitted-
        gsLocalJobsSettingsPath = gsJobMatixLocalDataDir(sName) & "\" & gK_JOBS_SETTINGSPATH

    End Function '-- gsLocalSettingsPath --
    '= = = = =  = = = = == = = = = = = = = 

    '--Return Local POS SettingsPath-

    Public Function gsLocalSettingsPath(Optional ByVal strAppName As String = "") As String
        Dim sName As String = strAppName
        'If (sName = "") Then
        '    If (msAssemblyName() <> "") Then
        '        sName = msAssemblyName()
        '    Else '-default-
        '        sName = gK_APP_NAME
        '    End If
        'End If '-omitted-
        gsLocalSettingsPath = gsJobMatixLocalDataDir(sName) & "\" & gK_POS_SETTINGS_PATH

    End Function '-- gsLocalSettingsPath --
    '= = = = =  = = = = == = = = = = = = = 
    '-===FF->

    '== 3107.805== 
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

    '-- Byte Array to Hex..-
    '--   Bytes T o H e x  string --

    Public Function gsBinToHex(ByRef sInstr() As Byte) As String
        Dim j, i, k As Integer
        Dim s1, sResult As String

        sResult = ""
        '--If Len(sInstr) > 0 Then
        For i = 1 To UBound(sInstr) + 1
            s1 = Hex(sInstr(i - 1))
            If (Len(s1) Mod 2) <> 0 Then s1 = "0" & s1 '--pad odd length-
            sResult = sResult & s1
        Next i
        '--End If
        gsBinToHex = sResult
    End Function '--to hex--
    '= = = = = = =
    '= = = = = = = = = = = =
    '-===FF->

    '-- Procedure to call HTML Help..--
    '-- Procedure to call HTML Help..--

    '-- HTML Help file launched in response to a button click: --

    Public Sub HH_DISPLAY_Click(ByRef hwnd As Integer, ByVal strTopicName As String)
        '--hWnd is a Long defined elsewhere to be the window handle
        '--that will be the parent to the help window.
        Dim hwndHelp As Integer
        '--The return value is the window handle of the created help window.
        '== hwndHelp = HtmlHelp(hwnd, "JT2-UserGuide.chm", HH_DISPLAY_TOPIC, VarPtr(strTopicName))
        hwndHelp = HtmlHelp(hwnd, "JT2-UserGuide.chm::/" & strTopicName, HH_DISPLAY_TOPIC, 0)

    End Sub '--help-
    '= = = = = = = = = = = = =  = = = =

    '== 3067.0==
    '-- Set/Get HelpFile Name --

    Private msHelpFileName As String = ""

    '-- Set HelpFile Name --
    Public Function gbSetHelpFileName(ByVal sFileName As String) As Boolean

        msHelpFileName = sFileName
    End Function  '--Set==

    '--Retrieve--

    Public Function gsGetHelpFileName() As String
        gsGetHelpFileName = msHelpFileName
    End Function  '--Get==
    '= = = = = = = = = = = = = =
    '-===FF->


    '--  get Current User ---
    '--  get Current User ---
    '*********************************************************
    '* Function to get the current logged on user in windows *
    '*********************************************************

    '-- Reworked to use StringBuilder Class
    '--  for fixed-size win32 buffer.

    Public Function gsGetCurrentUser() As String

        gsGetCurrentUser = System.Environment.UserName

    End Function '--get user--
    '= = = = = = = = = = = = = =
    '-===FF->
    '- printers - 3101.1110=
    '-gbGetAavailablePrinters-
    '-- Returns VB collection of names of avail printers.
    '--  and (Name of Default printer, or ""..
    '--  and (1-based) index of Default printer, or -1..

    Public Function gbGetAvailablePrinters(ByRef colPrinters As Collection, _
                                            ByRef sDefaultPrinterName As String, _
                                              ByRef intDefaultPrinterIndex As Integer) As Boolean
        Dim ix, intPrtCount, intDefault As Integer
        Dim s1 As String
        Dim pd1 As New PrintDocument()

        gbGetAvailablePrinters = False
        intDefaultPrinterIndex = -1
        sDefaultPrinterName = ""  '== Printer.DeviceName-
        intDefault = -1
        '-- get printers and find name of default printer..--
        Try
            intPrtCount = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count
            colPrinters = New Collection
            If (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0) Then
                For ix = 0 To (intPrtCount - 1)
                    s1 = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(ix)
                    '--  set printer selected so we can test....--
                    pd1.PrinterSettings.PrinterName = s1
                    If pd1.PrinterSettings.IsDefaultPrinter Then
                        sDefaultPrinterName = s1
                        '==Exit For
                        intDefault = ix
                    End If
                    colPrinters.Add(s1, s1)  '- same key and data..
                Next ix
                If (sDefaultPrinterName = "") Then '== L1 <> 0 Then '--no printer..-
                    MsgBox("Error in getting Default Printer.." & vbCrLf & _
                               "Printers may not be set up in Windows.." & vbCrLf & _
                                 "POS printing may not be available..", MsgBoxStyle.Exclamation)
                End If
                If (intDefault >= 0) Then '-have default-
                    intDefaultPrinterIndex = intDefault + 1  '-for vb collection-
                End If
                gbGetAvailablePrinters = True
            End If
        Catch ex As Exception
            MsgBox("ERROR in getting system Printer collection." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-gbGetAavailablePrinters-
    '= = = = = = = = = = = = = = = = =  = =
    '-===FF->

    '--  LOCAL Settings file functions..--
    '--  LOCAL Settings file functions..--

    '-- load settings into a collection if collections
    '--   (Each sub collection contains key and value items..)-

    'Public Function OBSOLETE_gbLoadLocalSettings(ByVal sPath As String, _
    '                                   ByRef colSettings As Collection) As Boolean
    '= = = = =  = = = = = = = = = = = = = =

    '--QUERY local settings for a particular Key..
    '== .. eg printer names, sql serrvername..

    'Public Function OBSOLETE_gbQueryLocalSetting(ByVal sPath As String, _
    '                                     ByVal strSettingKey As String, _
    '                                       ByRef strSettingValue As String) As Boolean
    '= = = = = =  =  == = = == ==

    '-- Save a local Setting.
    '--  Delete current setting if exists..-
    '--  Add new setting, and re-write file..-

    'Public Function OBSOLETE_gbSaveLocalSetting(ByVal sPath As String, _
    '                                    ByVal strSettingKey As String, _
    '                                      ByVal strSettingValue As String) As Boolean
    '= = = = = =  == = =  = = == =  ==

    '-- ORIGINAL - LOCAL Settings file functions..--

    '--load local settings.. eg printer names, sql serrvername..
    '== Public Function gbLoadSettings(ByRef sPath As String, _
    '==                                 ByRef sdSettings As Scripting.Dictionary) As Boolean

    'Public Function OBSOLETE_gbLoadSettings(ByVal sPath As String, _
    '                                ByRef sdSettings As clsStrDictionary) As Boolean
    '= = = = = =  =  == = = == ==

    '-- load systemInfo settings..--
    '-- load systemInfo settings..--
    '---  send back a collection of collections (rows..)--

    'Public Function OBSOLETE_gbLoadsystemInfo(ByRef cnnSQL As OleDbConnection, _
    '                                  ByRef colInfo As Collection, _
    '                                      ByRef sdSystemInfo As clsStrDictionary) As Boolean
    '= = = = = =
    '-- vaInfo is array of (key,value,key,value...)
    '---- if key exists then UPDATE, else INSERT..--

    'Public Function OBSOLETE_gbUpdateSystemInfo(ByRef cnnSQL As OleDbConnection, _
    '                                     ByRef vaInfo As Object) As Boolean
    '= = = = = = =  =  = =
    '-===FF->


    '-- get full INSTALL path of JobTracking2.exe from Registry..-

    Public Function gsGetInstallAppPath(ByVal sAppname As String) As String
        Dim L1 As Integer
        Dim s1 As String

        gsGetInstallAppPath = ""
        '--Find install path..--
        '---  Use empty ValueName to get the default value incl filename..
        L1 = gsRegQueryValue2(HKEY_LOCAL_MACHINE, "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" & sAppname, "", s1)
        If L1 = 0 Then
            '--MsgBox "Local ComputerName is :" + vbCrLf + s1
            gsGetInstallAppPath = s1
        Else
            If gbDebug Then MsgBox("Can't find Installed App Path in registry for JobTracking2.  Reg error: " & L1)
        End If
    End Function '--install path.-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- find full path of supplied file name.=-
    '-- either in local app Directory..
    '---  or in install dir. path..

    Public Function gsFindFullPath(ByVal sAppname As String, ByVal sFileName As String) As String
        Dim sAppPath As String
        Dim sPath As String
        Dim s1 As String

        sAppPath = My.Application.Info.DirectoryPath
        If Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
        gsFindFullPath = sFileName '--in case --
        sPath = sAppPath & sFileName
        If (Dir(sPath) <> "") Then '--found.
            gsFindFullPath = sPath
        Else
            '--  look for errant file ein the App Installed Path..--
            s1 = gsGetInstallAppPath(sAppname) '--get jobtracking install path.
            If (s1 <> "") Then '-something..-
                If Right(LCase(s1), Len(sAppname)) = LCase(sAppname) Then
                    sPath = Left(s1, Len(s1) - Len(sAppname)) & sFileName
                    If Dir(sPath) <> "" Then '--found.
                        gsFindFullPath = sPath
                    End If
                End If
            End If
        End If
    End Function '--find path..-
    '= = = = = = = = = = = = = = =

    '-- find full path of supplied file name.=-
    '-- either in local app Directory..
    '---  or in install dir. path..

    Public Function gsFindFullPathEx(ByVal sAppname As String, _
                                    ByVal sFileName As String, ByRef sFullPath As String) As Boolean
        Dim sPath As String
        Dim s1 As String
        Dim sAppPath As String

        gsFindFullPathEx = False '=sFileName  '--in ace --
        sAppPath = My.Application.Info.DirectoryPath
        If Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
        sFullPath = ""
        sPath = sAppPath & sFileName
        If Dir(sPath) <> "" Then '--found.
            sFullPath = sPath
            gsFindFullPathEx = True
            '==msFindFullPath = sPath
        Else
            s1 = gsGetInstallAppPath(sAppname) '--get jobtracking install path.
            If (s1 <> "") Then '-something..-
                If Right(LCase(s1), 16) = "jobtracking2.exe" Then
                    sPath = Left(s1, Len(s1) - 16) & sFileName
                    If Dir(sPath) <> "" Then '--found.
                        sFullPath = sPath
                        gsFindFullPathEx = True
                    End If
                End If
            End If
        End If
    End Function '--find path..-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--parse list of items==
    '--unpack UNQUOTED list-==

    Public Function giParseList(ByVal sInList As String, _
                                ByRef colItems As Collection) As Short
        Dim j, i, k As Integer
        Dim fx As Short
        Dim sList As String
        Dim s1 As String

        '--dissect comma sep. list sList--> array 'asItems'- count= fx-----
        '--Erase asItems:
        fx = 0
        giParseList = 0
        colItems = New Collection
        sList = sInList
        If Len(sList) <= 0 Then Exit Function
        If (Left(sList, 1) = "(") Then sList = Mid(sList, 2) '--drop brackets--
        If (Right(sList, 1) = ")") Then sList = Left(sList, Len(sList) - 1)
        '--unseparate items from commas--
        k = InStr(1, sList, ",")
        While (k > 0) Or (Len(sList) > 0)
            If k > 0 Then
                s1 = Trim(Left(sList, k - 1))
                sList = Trim(Mid(sList, k + 1)) '--remainder--
            Else
                s1 = Trim(sList) : sList = "" '--last--
            End If
            '--if len(s1) >0 then
            fx = fx + 1
            '--ReDim Preserve asItems(1 To fx)
            '--asItems(fx) = s1
            colItems.Add(s1)
            '--end if
            If k > 0 Then k = InStr(1, sList, ",") '--keep looking--
        End While
        giParseList = fx
    End Function '--parse--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-----look in cmd line for "/cmd"---
    '---- extract command line parameter---
    '------return as sParm---

    Public Function gbGetCmd(ByVal sCmdLine As String, _
                               ByVal sCmd As String, _
                               ByRef sParm As String) As Boolean
        Dim k, i, j, px As Integer
        Dim s1, s2 As String

        sParm = "" : s2 = ""
        gbGetCmd = False
        k = InStr(1, UCase(sCmdLine), "/" & UCase(sCmd)) '--locate cms--
        If (k > 0) Then '--found--
            s1 = Mid(sCmdLine, k + Len(sCmd) + 1) '--get tail after cmd--
            If s1 = "" Then '--no more--
                gbGetCmd = True
            Else '--some tail-
                If (Left(s1, 1) <> " ") And (Left(s1, 1) <> "=") Then Exit Function '--bad terminator-
                s1 = Trim(s1) '--drop leading spaces-
                If (Left(s1, 1) = "=") Then '--w
                    s1 = Mid(s1, 2) '---drop--
                    s1 = Trim(s1) '--and drop leading spaces-
                End If
                i = 1 '--scan for end--
                gbGetCmd = True
                If (Left(s1, 1) = """") Then '--quoted parm--
                    s1 = Mid(s1, 2) '---drop 1st quote--
                    While (i < Len(s1)) And (Mid(s1, i, 1) <> """") '--find closing quote-
                        i = i + 1
                    End While
                    sParm = Left(s1, i - 1)
                Else
                    '--While (i < Len(s1)) And ((Mid$(s1, i, 1) = " ") Or (Mid$(s1, i, 1) = "="))
                    '--  i = i + 1
                    '--Wend
                    If (i <= Len(s1)) Then
                        s2 = Mid(s1, i) '--should/be start of parm--
                        If (Len(s2) < 1) Or (Left(s2, 1) = "/") Then Exit Function '--no parm--
                        '--must be  term by space===
                        i = InStr(1, s2, " ") '--scan past space --
                        If (i > 1) Then
                            sParm = Left(s2, i - 1) '--get parm excl space--
                        Else
                            sParm = s2
                        End If
                    End If
                End If '--quoted-
            End If '--some tail-
        End If

    End Function '--get cmd--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '--===FF->

    '- - - -log message in text file--

    '=3101= Public Sub gLogMsg(ByVal sMsg As String)
    '=3101=     gsLogData = gsLogData & sMsg & vbCrLf
    '=3101=     If gbVerbose Then
    '=3101=     End If
    '=3101= End Sub '--logmsg--
    '= = = = = = = =

    '- - - -show message in main forms text--

    '=3101= Public Sub gAdvise(ByVal sMsg As String)
    '=3101=     Call gLogMsg(sMsg) '--add to log--
    '=3101= End Sub
    '= = = = = = = =

    '-- CenterForm  --
    '-- CenterForm  --

    Public Sub CenterForm(ByVal xForm As System.Windows.Forms.Form)
        With xForm
            If .WindowState = System.Windows.Forms.FormWindowState.Normal Then
                .Top = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - .Height) \ 2)
                .Left = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - .Width) \ 2)
            End If
        End With
    End Sub
    '= = = = = = = =  = = = =  = = = =
    '= = = = = = = =  = = = =  = = = =
    '-===FF->

    '==Log msg to log file--
    '== 3107.806= Re-write for .Net 4.5-
    '==  --  and Optionally Add Security ==

    '== -- Updated 3501.1223  23-Dec-2018=  
    '==     --  Add RETRY to gbLogMsg Append Writeln....


    Public Function gbLogMsg(ByVal sLogPath As String, _
                                 ByVal strLogMsg As String, _
                          Optional ByVal bMakePublic As Boolean = True) As Boolean

        Dim sw1 As StreamWriter
        Dim s1, sData As String

        gbLogMsg = False
        If (sLogPath = "") Then Exit Function
        '--- !!  ADD date/time to msg..--
        's1 = " =V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & _
        '             "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "="
        'sData = vbCrLf & VB6.Format(Today, "dd-MMM-yyyy") & " = " & VB6.Format(TimeOfDay, "hh:mm:ss = ") & _
        '                                                     My.Application.Info.AssemblyName & s1 & vbCrLf & strLogMsg
        '= 3501.0615=
        s1 = " =" & gsGetAppVersion() & "."
        sData = vbCrLf & VB6.Format(Today, "dd-MMM-yyyy") & " = " & VB6.Format(TimeOfDay, "hh:mm:ss = ") & _
                                                             gsGetAppName() & s1 & vbCrLf & strLogMsg
        '== 3107.806= Re-write for .Net 4.5-
        If Not File.Exists(sLogPath) Then  '-must create-
            '-- Create the file.  
            Try
                sw1 = File.CreateText(sLogPath)
                '-- add data msg-
                sw1.WriteLine(sData)
                sw1.Close()
                sw1.Dispose()
                gbLogMsg = True
            Catch ex As Exception
                MsgBox("Failed to Create Text to File: " & vbCrLf & sLogPath & vbCrLf & _
                                                               vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Exit Function
            End Try
            '-- make public for everyone..--
            If bMakePublic Then
                If AddFileSecurity(sLogPath, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow) Then
                    '-ok-
                Else
                    gbLogMsg = False
                    Exit Function
                End If
            End If  '-public-
        Else    '-file exists--
            ' Append msg to the file.
            Dim bDoneOk As Boolean = False
            Dim intRetry As Integer = 2
            While (Not bDoneOk) And (intRetry > 0)
                Try
                    sw1 = File.AppendText(sLogPath)
                    sw1.WriteLine(sData)
                    sw1.Close()
                    sw1.Dispose()
                    gbLogMsg = True
                    bDoneOk = True
                Catch ex As Exception
                    intRetry -= 1
                    If intRetry <= 0 Then
                        MsgBox("Failed to append Text to File: " & vbCrLf & sLogPath & vbCrLf & _
                                                                    vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    Else  '-can retry-
                        If (Not (sw1 Is Nothing)) Then
                            sw1.Close()
                        End If
                        Thread.Sleep(500)  '--millisecs.
                    End If
                End Try
            End While  '-retry-
        End If  '-exists-
    End Function '--log msg--
    '= = = = = = = = = = = = = =  =
    '-===FF->

    '---- o p e n B y t e--------
    '---- o p e n B y t e--------

    'Public Function OBSOLETE_openByte(ByVal fpath As String, _
    '                         ByVal xShare As String, _
    '                          ByRef handle As Short) As Short

    '- = = = = = = = = = = = = = = = = = = = *-

    '-*--setAddress+ readByte..----*-

    'Public Function OBSOLETE_readPos(ByRef handle As Short, _
    '                            ByRef pos As Integer, _
    '                              ByRef buf As String, _
    '                              ByRef SIZE As Integer) As Short '-ok-y/n-
    '- = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = *-

    '-- load file data  --

    'Public Function OBSOLETE_glLoadDataFile(ByVal strFilePath As String, _
    '                                  ByRef sFileData As String) As Integer


    '= = = =  = = = = =
    '-===FF->

    '--create/write TEXT file==
    '--create/write file==
    '== 3107.806= Re-written for .Net. ==
    '== 3107.806= Optionally Add Security ==

    Public Function glSaveTextFile(ByRef sFileSavePath As String, _
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
        glSaveTextFile = -1
        '-- WriteAllText overwrites any existing file..-
        Try
            File.WriteAllText(sFileSavePath, sNewFileData)
        Catch ex As Exception
            sMsg = "Failed to save Text File: " & vbCrLf & sFileSavePath & vbCrLf & vbCrLf & ex.Message
            MsgBox(sMsg)
            msLastFileErrorMessage = sMsg
            glSaveTextFile = -2  '= ex.HResult
            Exit Function
        End Try
        '-- make public for everyone..--
        If bMakePublic Then
            If AddFileSecurity(sFileSavePath, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow) Then
                glSaveTextFile = 0
            End If
        End If  '-public-
    End Function '--save text--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '--create/write BINARY DATA to file==
    '--create/write file==

    'Public Function OBSOLETE_glSaveFile(ByRef sFileSavePath As String, _
    '                               ByRef yBinData() As Byte) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '= =Display sql database schema= = = ==  =
    '= =Display sql database schema= = = ==  =
    '--  from our internal collections--

    '==3101= Public Function gbMain_showSqlSchema(ByRef sSqlDBName As String, _
    '==3101=                                       ByRef colSqlTables As Collection) As Boolean

    Public Function gsShowSqlSchema(ByRef sSqlDBName As String, _
                                         ByRef colSqlTables As Collection) As String

        Dim k, i, j, isx As Integer
        Dim s1, sInfo As String
        '==Dim sKeyField As String
        Dim sList, sTableName, sSqlType As String
        '== Dim colTblList As Collection
        Dim colTable As Collection
        Dim colFieldx As Collection '-- 1 field--
        Dim colFields As Collection
        Dim colTableInfo As Collection
        Dim colPrimaryKey As Collection
        Dim colForeignKeys As Collection
        Dim colOtherIndexes As Collection
        Dim colParents As Collection
        Dim v1 As Object
        '== Dim rs As ADODB.Recordset
        Dim lResult As Integer

        '--ok.. show collection--

        gsShowSqlSchema = ""
        '---catalogue has been loaded..  now show all schemas--
        '----incl- (primary keys, and foreign keys)--
        sInfo = vbCrLf & "= = gbMain_showSqlSchema= =" & vbCrLf & " = Displaying catalogue for DB: " & sSqlDBName
        For isx = 1 To colSqlTables.Count()
            colTable = colSqlTables.Item(isx)
            sTableName = colTable.Item("TABLENAME")
            colTableInfo = colTable.Item("SOURCEINFO")
            colFields = colTable.Item("FIELDS")
            colPrimaryKey = colTable.Item("PRIMARYKEYS")
            '--Set colPrimaryKey = colTable(3)
            colForeignKeys = colTable.Item("FOREIGNKEYS")
            colOtherIndexes = colTable.Item("OTHERINDEXES")
            colParents = colTable.Item("PARENTS")

            '--sTableName = colTableInfo.item("TABLENAME")
            sInfo &= vbCrLf & "== TABLE:  '" & sTableName & "' has columns: " & vbCrLf
            sList = ""
            If colFields.Count() > 0 Then
                For Each colFieldx In colFields
                    If sList <> "" Then sList = sList & vbCrLf
                    sList &= colFieldx.Item("NAME") & "; [Type:" + CStr(colFieldx.Item("TYPE"))
                    sList &= "] DefSize:" & CStr(colFieldx.Item("DEFINEDSIZE")) & "; "  '--octet_length-
                    sSqlType = colFieldx.Item("TYPE_NAME")
                    If InStr(LCase(sSqlType), "varchar") > 0 Then
                        sSqlType &= "(" & CStr(colFieldx.Item("CHAR_MAX_LENGTH")) & ")"
                    End If
                    sList &= " sqlType:" & sSqlType & "; "
                    If Not colFieldx.Item("ISNULLABLE") Then
                        sList &= "NOT NULL"
                    End If
                    s1 = colFieldx.Item("COLUMN_DEFAULT")
                    If (s1 <> "") Then
                        sList &= " DEFAULT " & s1
                    End If
                    If colFieldx.Item("ISIDENTITY") Then sList &= " IDENTITY "
                    If colFieldx.Item("ISFOREIGNKEY") Then sList &= " FKEY: "
                    If colFieldx.Item("ISFOREIGNKEY") Then '--if exists--
                        s1 = colFieldx.Item("FOREIGNTABLE") '--if exists--
                        sList = sList & " REF " & s1 & "."
                    End If '--foreign-
                Next colFieldx
            End If '--count--
            sInfo &= vbCrLf & sList & vbCrLf '--gAdvise sList
            sList = "-- Primary Key fields: "
            If (colPrimaryKey.Count() > 0) Then
                For Each v1 In colPrimaryKey
                    sList = sList + v1 + "; "
                Next v1
            End If '--count--
            sInfo &= sList & vbCrLf '--gAdvise sList
            '--show foreign keys..--
            sList = "-- Foreign Key fields: "
            If colForeignKeys.Count() > 0 Then
                For Each colFieldx In colForeignKeys
                    s1 = colFieldx.Item("LOCALFIELD")
                    sList &= colFieldx.Item("LOCALFIELD") & _
                            " REF " & colFieldx.Item("FOREIGNTABLE") & "," & colFieldx.Item("FOREIGNFIELD") & vbCrLf
                Next colFieldx
            End If '--count--
            sInfo &= sList '--gAdvise sList
            '--show other indexes--
            sList = ""
            If colOtherIndexes.Count() > 0 Then
                sList = "-- OTHER indexes on : " & vbCrLf
                For i = 1 To colOtherIndexes.Count()
                    colFieldx = colOtherIndexes.Item(i)
                    sList = sList & VB6.Format(i, "00") & ". "
                    If colFieldx.Count() > 0 Then
                        For j = 1 To colFieldx.Count()
                            sList = sList & " " + colFieldx.Item(j)
                        Next j
                    End If '-count--
                    sList = sList & vbCrLf
                Next i
            End If '--other--
            sInfo &= sList '-- gAdvise sList
            sList = ""
            If colParents.Count() > 0 Then
                For i = 1 To colParents.Count()
                    If i > 1 Then sList = sList & ", "
                    sList = sList + colParents.Item(i)
                Next i
                sInfo &= "  Parent tables are: " & sList
            End If '--parents-
            '--show selected "name" column..
            sInfo &= "-- Name column is: " + colTable.Item("NAMECOLUMN") & vbCrLf
        Next isx '--table--
        sInfo &= "=== All Done == " & vbCrLf & vbCrLf
        sInfo &= "==  sql catalogue show all done ==  (see log file)."

        gsShowSqlSchema = sInfo

    End Function '--show--
    '= = = = = = = = =
    '--Note:  for "getDbInfo, see "InfoSchema.bas" ..

    '= = = = = = = =
    '= = =  the end =  =

    '= = =end subs= =
    '= End Module  
    '== END OF FILE SUPPORT stuff.. --
    '== END OF FILE SUPPORT stuff.. --
    '== END OF FILE SUPPORT stuff.. --

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '-- Sql subs..

    '== Module modSqlSupport
    '== Module modSqlSupport
    '== Module modSqlSupport

    '== START of IMHERTTED SQL SUPPORT stuff.. --
    '== START of IMHERTTED SQL SUPPORT stuff.. --
    '== START of IMHERTTED SQL SUPPORT stuff.. --
    '== START of IMHERTTED SQL SUPPORT stuff.. --


    '-- sql subs for ADO.net--
    '--  grh POS 24-Feb-2014--

    '-- PO3- Ugrade-- 12-Sep-2014 --
    '--  NOW using System.Data.OleDb
    '--  To be connection compatible with JobMatix v3.1 and Jet/OLEDB..-
    '==
    '==  grh. JobMatix 3.1.3101.1110 ---  10-Nov-2014 ===
    '==   >>  Move DAO functions to "modJetLogin" . 
    '==         so this module is free of Jet/DAO links.
    '==
    '==  grh. JobMatix 3.1.3103.0205 ---  03Feb2005 ===
    '==   >>  -gbIsSqlServer2008Plus()
    '==   >>  WhoUsing now uses "sp_who" for sql-2000 AND sql-2005-
    '==
    '==  grh. JobMatix 3.1.3103.0221 ---  21Feb2005 ===
    '==   >>  -glExecSp -  Must use oleDb parameter value types (NOT sql)-
    '==
    '= = = =  = = = = = = = =  = = = = = = = = = =  = = = = = = = =

    '-- ADODB data Types--
    '-- ADODB data Types--

    Public Const ADODB_DataTypeEnum_adBigInt = 20
    Public Const ADODB_DataTypeEnum_adBinary = 128
    Public Const ADODB_DataTypeEnum_adBoolean = 11
    Public Const ADODB_DataTypeEnum_adChar = 129
    Public Const ADODB_DataTypeEnum_adDBTimeStamp = 135
    Public Const ADODB_DataTypeEnum_adNumeric = 131
    Public Const ADODB_DataTypeEnum_adDouble = 5
    Public Const ADODB_DataTypeEnum_adVarBinary = 204
    Public Const ADODB_DataTypeEnum_adInteger = 3
    Public Const ADODB_DataTypeEnum_adCurrency = 6
    Public Const ADODB_DataTypeEnum_adWChar = 130
    Public Const ADODB_DataTypeEnum_adSingle = 4
    Public Const ADODB_DataTypeEnum_adSmallInt = 2
    Public Const ADODB_DataTypeEnum_adVariant = 12
    Public Const ADODB_DataTypeEnum_adGUID = 72

    '--these for matching DAO--
    Public Const ADODB_DataTypeEnum_adUnsignedTinyInt = 17
    Public Const ADODB_DataTypeEnum_adVarChar = 200
    Public Const ADODB_DataTypeEnum_adDate = 7
    Public Const ADODB_DataTypeEnum_adDecimal = 14
    Public Const ADODB_DataTypeEnum_adLongVarBinary = 205
    Public Const ADODB_DataTypeEnum_adLongVarWChar = 203
    Public Const ADODB_DataTypeEnum_adVarWChar = 202
    Public Const ADODB_DataTypeEnum_adLongVarChar = 201
    Public Const ADODB_DataTypeEnum_adTinyInt = 16

    Public Const ADODB_DataTypeEnum_adUnsignedBigInt = 21
    Public Const ADODB_DataTypeEnum_adUnsignedInt = 19
    Public Const ADODB_DataTypeEnum_adUnsignedSmallInt = 18


    '= = = = = = = = = = = =
    '== grh 12-Feb-2013= Build-3072/3073= --
    Private msLastSqlErrorMessage As String = ""


    '-- SET THESE at START UP ----
    '-- SET THESE at START UP ----
    '-- SET THESE at START UP ----
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = -1
    Private mIntJobMatixDBid As Integer = -1

    Private mbIsSqlAdmin As Boolean = False

    '= = = = = = = = = = = = = = =  == =
    '-===FF->

    '---- all EX  xbsWizard subs---


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

    '--convert numeric data for sorted display..-

    '--  This must have ADO-Type input only !!--

    Public Function gsFormat(ByRef v1 As Object, _
                             ByVal intADO_Type As Integer, _
                              ByVal lSize As Integer) As String
        Dim sResult As String
        Dim sType As String '--sql type--

        sResult = ""
        If Not IsDBNull(v1) Then
            sResult = CStr(v1) '--for strings..-
            sType = UCase(gsGetSqlType(intADO_Type, lSize))
            If (sType = "MONEY") Or (sType = "SMALLMONEY") Then '--currency..-
                If IsNumeric(v1) Then   '=3059.1=
                    sResult = New String(" ", 9)
                    sResult = RSet(FormatCurrency(v1, 2), Len(sResult))
                End If  '--is numeric-
            ElseIf gbIsNumericType(sType) Then
                If IsNumeric(v1) Then   '=3059.1=
                    '== sResult = New String(" ", 5)
                    '= sResult = RSet(VB6.Format(v1, "    0"), Len(sResult))
                    sResult = v1.ToString
                End If  '--is numeric v1-
            ElseIf gbIsDate(sType) Then
                If IsDate(v1) Then   '=3059.1=
                    sResult = Format(CDate(v1), "yyyy-MM-dd")
                End If  '--is date v1-
            End If '--type..-
        End If '--null..-
        gsFormat = sResult

    End Function '--convert--
    '-===FF->


    '--determine if date type--

    Public Function gbIsDate(ByVal sSqlType As String) As Boolean

        Dim s1 As String
        s1 = UCase(sSqlType)
        If (s1 = "DATETIME") Then
            gbIsDate = True
        Else
            gbIsDate = False
        End If

    End Function '--isdate--
    '= = = = =  = = =  = =

    '--determine if text type--

    Public Function gbIsText(ByVal sSqlType As String) As Boolean

        Dim s1 As String
        Dim ix As Integer

        s1 = UCase(sSqlType)
        '--drop length (LL) if appended--
        ix = InStr(s1, "(")
        If (ix > 1) Then s1 = Left(s1, ix - 1) '--drop parenthesised length--

        If (s1 = "CHAR") Or (s1 = "NCHAR") Or (s1 = "VARCHAR") Or _
                                 (s1 = "NVARCHAR") Or (s1 = "TEXT") Or (s1 = "NTEXT") Then
            gbIsText = True
        Else
            gbIsText = False
        End If

    End Function '--isText--
    '= = = = =  = = =  = =

    '--determine if NUMERIC sql type--

    Public Function gbIsNumericType(ByVal sSqlType As String) As Boolean
        Dim pos As Short
        Dim s1 As String
        pos = InStr(1, sSqlType, " ") '--find end of actual tyoe--
        s1 = UCase(sSqlType)
        If (pos > 1) Then s1 = UCase(Left(sSqlType, pos - 1)) '--drop IDENTITY-
        If (s1 = "INT") Or (s1 = "BIGINT") Or (s1 = "DECIMAL") Or _
                         (s1 = "SMALLINT") Or (s1 = "TINYINT") Or _
                              (s1 = "BIT") Or (s1 = "FLOAT") Or (s1 = "REAL") Or _
                                   (s1 = "MONEY") Or (s1 = "SAMLLMONEY") Or (s1 = "NUMERIC") Then
            gbIsNumericType = True
        Else
            gbIsNumericType = False
        End If
    End Function '--isNumeric--
    '= = = = =  = = =  = =
    '-===FF->

    '=
    '-- convert ADO type to SQL type--
    Public Function gsGetSqlType(ByVal intType As Integer, _
                                   ByVal lSize As Integer) As String
        Dim j, i, k As Integer
        Dim sT As String

        sT = "varChar (16)" '--default--
        Select Case intType
            Case ADODB_DataTypeEnum_adBigInt : sT = "BIGINT"
            Case ADODB_DataTypeEnum_adBinary : sT = "BINARY"
            Case ADODB_DataTypeEnum_adBoolean : sT = "BIT"
            Case ADODB_DataTypeEnum_adChar : sT = "CHAR(" & Trim(CStr(lSize)) & ")"
            Case ADODB_DataTypeEnum_adCurrency : sT = "MONEY"
            Case ADODB_DataTypeEnum_adDate : sT = "datetime"
            Case ADODB_DataTypeEnum_adDBTimeStamp : sT = "DATETIME"
            Case ADODB_DataTypeEnum_adDecimal : sT = "DECIMAL"
            Case ADODB_DataTypeEnum_adDouble : sT = "FLOAT"
            Case ADODB_DataTypeEnum_adGUID : sT = "UNIQUEIDENTIFIER"
            Case ADODB_DataTypeEnum_adInteger : sT = "INT"
            Case ADODB_DataTypeEnum_adLongVarBinary : sT = "VARBINARY" '--if L>8000 then use IMAGE --
            Case ADODB_DataTypeEnum_adLongVarChar : sT = "TEXT"
            Case ADODB_DataTypeEnum_adLongVarWChar : sT = "NTEXT" '--dbase memo--
            Case ADODB_DataTypeEnum_adNumeric : sT = "NUMERIC"
            Case ADODB_DataTypeEnum_adSingle : sT = "REAL"
            Case ADODB_DataTypeEnum_adSmallInt : sT = "SMALLINT"
            Case ADODB_DataTypeEnum_adTinyInt : sT = "TINYINT"
                '--unsigned not defined in sql-
            Case ADODB_DataTypeEnum_adUnsignedBigInt : sT = "BIGINT"
            Case ADODB_DataTypeEnum_adUnsignedInt : sT = "INT"
            Case ADODB_DataTypeEnum_adUnsignedSmallInt : sT = "SMALLINT"
            Case ADODB_DataTypeEnum_adUnsignedTinyInt : sT = "TINYINT"
                '----
            Case ADODB_DataTypeEnum_adVariant : sT = "SQL_VARIANT"
                '--Case adBinary:     sT = "TIMESTAMP"         '--???--
                '--Case adVarBinary: sT = "TINYINT"
            Case ADODB_DataTypeEnum_adVarBinary : sT = "VARBINARY"
            Case ADODB_DataTypeEnum_adVarChar : sT = "VARCHAR(" & Trim(CStr(lSize)) & ")"
            Case ADODB_DataTypeEnum_adVarWChar : sT = "nvarChar(" & Trim(CStr(lSize)) & ")"
            Case ADODB_DataTypeEnum_adWChar : sT = "NCHAR"
            Case Else
        End Select
        gsGetSqlType = sT

    End Function '--sql-type--
    '= = = =  = = = =
    '-===FF->

    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--
    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--
    '-- glTypeDAOtoADO  --Convert DAO type to ADO type..--

    '=3101.1110= Public Function glTypeDAOtoADO(ByRef vType As Object) As Integer
    '=3101.1110=    Dim lngADO As Integer
    '=3101.1110=     Select Case vType
    '=3101.1110=         Case DAO.DataTypeEnum.dbBigInt : lngADO = ADODB_DataTypeEnum_adBigInt
    '=3101.1110=         Case DAO.DataTypeEnum.dbBinary : lngADO = ADODB_DataTypeEnum_adBinary
    '=3101.1110=         Case DAO.DataTypeEnum.dbBoolean : lngADO = ADODB_DataTypeEnum_adBoolean
    '=3101.1110=         Case DAO.DataTypeEnum.dbByte : lngADO = ADODB_DataTypeEnum_adUnsignedTinyInt
    '=3101.1110=         Case DAO.DataTypeEnum.dbChar : lngADO = ADODB_DataTypeEnum_adVarChar
    '=3101.1110=         Case DAO.DataTypeEnum.dbCurrency : lngADO = ADODB_DataTypeEnum_adCurrency
    '=3101.1110=         Case DAO.DataTypeEnum.dbDate : lngADO = ADODB_DataTypeEnum_adDate
    '=3101.1110= '-------Case db:      lngADO = adDBTimeStamp: sT = "DATETIME"
    '=3101.1110=         Case DAO.DataTypeEnum.dbDecimal : lngADO = ADODB_DataTypeEnum_adDecimal
    '=3101.1110=         Case DAO.DataTypeEnum.dbDouble : lngADO = ADODB_DataTypeEnum_adDouble
    '=3101.1110=         Case DAO.DataTypeEnum.dbFloat : lngADO = ADODB_DataTypeEnum_adDouble
    '=3101.1110=         Case DAO.DataTypeEnum.dbGUID : lngADO = ADODB_DataTypeEnum_adGUID
    '=3101.1110=         Case DAO.DataTypeEnum.dbInteger : lngADO = ADODB_DataTypeEnum_adInteger
    '=3101.1110=         Case DAO.DataTypeEnum.dbLong : lngADO = ADODB_DataTypeEnum_adInteger
    '=3101.1110=         Case DAO.DataTypeEnum.dbLongBinary : lngADO = ADODB_DataTypeEnum_adLongVarBinary
    '=3101.1110= '----Case db:      lngADO = adLongVarChar: sT = "TEXT"
    '=3101.1110=         Case DAO.DataTypeEnum.dbMemo : lngADO = ADODB_DataTypeEnum_adLongVarWChar
    '=3101.1110=         Case DAO.DataTypeEnum.dbNumeric : lngADO = ADODB_DataTypeEnum_adDouble
    '=3101.1110=         Case DAO.DataTypeEnum.dbSingle : lngADO = ADODB_DataTypeEnum_adSingle
    '=3101.1110=         Case DAO.DataTypeEnum.dbText : lngADO = ADODB_DataTypeEnum_adVarWChar
    '=3101.1110=         Case DAO.DataTypeEnum.dbTime : lngADO = ADODB_DataTypeEnum_adDate
    '=3101.1110=         Case DAO.DataTypeEnum.dbTimeStamp : lngADO = ADODB_DataTypeEnum_adDate
    '=3101.1110=         Case DAO.DataTypeEnum.dbVarBinary : lngADO = ADODB_DataTypeEnum_adVarBinary
    '=3101.1110=         Case Else : lngADO = ADODB_DataTypeEnum_adVarChar
    '=3101.1110=     End Select
    '=3101.1110=     glTypeDAOtoADO = lngADO
    '=3101.1110= End Function '--  glTypeDAOtoADO --
    '= = = =  = = = =
    '-===FF->

    '-- -Convert sql type_name to ADO type==
    '-- -Convert sql type_name to ADO type==

    Public Function giGetADOdataType(ByVal sSqlType As String) As Short

        Dim intADO As Short

        intADO = 128      '== ADODB.DataTypeEnum.adBinary '--default-
        Select Case UCase(sSqlType)
            Case "BIGINT" : intADO = ADODB_DataTypeEnum_adBigInt
            Case "BINARY" : intADO = ADODB_DataTypeEnum_adBinary
            Case "BIT" : intADO = ADODB_DataTypeEnum_adBoolean
            Case "CHAR" : intADO = ADODB_DataTypeEnum_adChar
            Case "DATETIME" : intADO = ADODB_DataTypeEnum_adDBTimeStamp
            Case "DECIMAL" : intADO = ADODB_DataTypeEnum_adNumeric
            Case "FLOAT" : intADO = ADODB_DataTypeEnum_adDouble
            Case "IMAGE" : intADO = ADODB_DataTypeEnum_adVarBinary
            Case "INT" : intADO = ADODB_DataTypeEnum_adInteger
            Case "MONEY" : intADO = ADODB_DataTypeEnum_adCurrency
            Case "NCHAR" : intADO = ADODB_DataTypeEnum_adWChar
            Case "NTEXT" : intADO = ADODB_DataTypeEnum_adWChar
            Case "NUMERIC" : intADO = ADODB_DataTypeEnum_adNumeric
            Case "NVARCHAR" : intADO = ADODB_DataTypeEnum_adWChar
            Case "REAL" : intADO = ADODB_DataTypeEnum_adSingle
                '--Case "SMALLDATETIME":   intADO = adTimeStamp
            Case "SMALLINT" : intADO = ADODB_DataTypeEnum_adSmallInt
            Case "SMALLMONEY" : intADO = ADODB_DataTypeEnum_adCurrency
            Case "SQL_VARIANT" : intADO = ADODB_DataTypeEnum_adVariant
            Case "SYSNAME" : intADO = ADODB_DataTypeEnum_adWChar
            Case "TEXT" : intADO = ADODB_DataTypeEnum_adChar
            Case "TIMESTAMP" : intADO = ADODB_DataTypeEnum_adBinary
            Case "TINYINT" : intADO = ADODB_DataTypeEnum_adVarBinary
            Case "UNIQUEIDENTIFIER" : intADO = ADODB_DataTypeEnum_adGUID
            Case "VARBINARY" : intADO = ADODB_DataTypeEnum_adVarBinary
            Case "VARCHAR" : intADO = ADODB_DataTypeEnum_adChar

        End Select '--sqltype--

        giGetADOdataType = intADO

    End Function '--getADO--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- -Convert .Net DataType type_name to ADO, sql types==

    Public Function gbConvertDotNetDataType(ByRef column1 As DataColumn, _
                                             ByRef intADO_type As Integer, _
                                             ByRef sSqlType As String) As Boolean
        gbConvertDotNetDataType = False
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
                    sSqlType = "NVARCHAR"
                Else
                    sSqlType = "sql_variant"
                End If
                '--etc-

            End With '= column1
            intADO_type = giGetADOdataType(sSqlType)

            gbConvertDotNetDataType = True
        Catch ex As Exception
            MsgBox("ERROR in 'gbConvertDotNetDataType' function" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-gbConvertDotNetDataType-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->


    '--TEXT SEARCH request-- ==13-Jun-2010==
    '--TEXT SEARCH request--
    '-- "asColumns" is an array of strings (column names..) --

    Public Function gbMakeTextSearchSql(ByVal sArgText As String, _
                                         ByRef asColumns As Object) As String
        Dim asArgs As Object
        Dim sSql As String
        Dim s1, s2 As String
        Dim cx, ix As Integer

        gbMakeTextSearchSql = ""
        '=3101.1026=
        If (Trim(sArgText) = "") Then Exit Function

        '-- build query to srch all text cols..--
        '=======sSearchArg = Trim(txtSearch.Text)    '==UCase(request.Form("selTextSearchArg"))
        sSql = "" '---"SELECT * FROM " + msProductTableName + " WHERE "
        s1 = "" : s2 = "" '--result-
        '-- arg can be multiple tokens..--
        asArgs = Split(Trim(sArgText))
        '====Set table1 = mColSqlDBInfo("JOBS")   '--- mCat1.Tables(msProductTableName)
        cx = 0

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        If (Not IsNothing(asColumns)) And (Not IsNothing(asArgs)) Then '--we have some text cols..--
            For ix = 0 To UBound(asArgs) '--for each arg fragment..--
                If (ix > 0) Then sSql = sSql & " AND "
                s1 = "" '-- sub-clause for this Arg..
                If (UBound(asColumns) > 0) Then s1 = s1 & "(" '-- for multiple columns..--
                For cx = 0 To UBound(asColumns) '---Each column1 In table1.Columns
                    s2 = asColumns(cx)
                    '---If gbIsText(gsGetSqlType(column1.Type, column1.DefinedSize)) Then  '-- is text col..-
                    If (cx > 0) Then s1 = s1 & " OR "
                    '-- all cols are in OR relation with this Arg...--
                    s1 = s1 & "(" & s2 & " LIKE '%" & gsFixSqlStr(CStr(asArgs(ix))) & "%' )"
                Next cx '--column-
                If (UBound(asColumns) > 0) Then s1 = s1 & ")"
                sSql = sSql & s1
            Next ix
        Else '--no text cols..  no search..
        End If '--text--
        gbMakeTextSearchSql = sSql
        '--add srch args if any..-

    End Function '--make srch..-
    '= = = = = = = =
    '-===FF->

    '==    =3083.210= Add function 'gbSQL_Enumerate_Main' ----
    '==  DISCOVER SQL serverinstances..--
    '==  DISCOVER SQL serverinstances..--

    '-- Dim dataTable As System.Data.DataTable = instance.GetDataSources()
    '-- The table returned from the method call contains the following columns,
    '--         all of which contain string values:

    '-- Column  Description  
    '-- ServerName 
    '--  Name of the server.

    '-- InstanceName 
    '--  Name of the server instance.  
    '--         Blank if the server is running as the default instance.

    '-- IsClustered 
    '--  Indicates whether the server is part of a cluster.

    '-- Version 
    '--  Version of the server 
    '--         (8.00.x for SQL Server 2000, and 9.00.x for SQL Server 2005).

    Public Function gbSQL_Enumerate_Main(ByRef ColSqlServers As Collection) As Boolean
        ' Retrieve the enumerator instance and then the data.
        Dim enumInstance1 As SqlDataSourceEnumerator = SqlDataSourceEnumerator.Instance
        Dim table1 As System.Data.DataTable
        Dim colServer As Collection
        Dim rx As Integer = 0
        Dim s1, s2, s3 As String
        '== ' Display the contents of the table.
        '== DisplayData(table)
        gbSQL_Enumerate_Main = False
        ColSqlServers = New Collection

        Try
            table1 = enumInstance1.GetDataSources()
            For Each row As DataRow In table1.Rows
                s1 = IIf((Not VB6.IsDBNull(row("ServerName"))), row("ServerName"), "")
                s2 = IIf((Not VB6.IsDBNull(row("InstanceName"))), row("InstanceName"), "")
                s3 = IIf((Not VB6.IsDBNull(row("Version"))), row("Version"), "")
                '=colServer.Add(row("InstanceName"), "InstanceName")
                If (s1 <> "") And (Not IsDBNull(row("Version"))) Then  '-has version-
                    rx += 1  '--count rows..-
                    colServer = New Collection
                    colServer.Add(s1, "ServerName")
                    colServer.Add(s2, "InstanceName")
                    colServer.Add(s3, "Version")
                    ColSqlServers.Add(colServer)
                    '== txtSystemInfo.Text = txtSystemInfo.Text & rx & ": "
                    '== For Each col As DataColumn In table.Columns
                    '=    Console.WriteLine("{0} = {1}", col.ColumnName, row(col))
                    '==   txtSystemInfo.Text = txtSystemInfo.Text & col.ColumnName & "=" & row(col) & "; "
                    '==   txtSystemInfo.SelectionStart = txtSystemInfo.TextLength
                    '==   txtSystemInfo.SelectionLength = 0
                    '== Next '--col-
                    '== txtSystemInfo.Text = txtSystemInfo.Text & vbCrLf
                End If '-has version-
            Next '--row-
            gbSQL_Enumerate_Main = True

        Catch ex As Exception
            '--error-
            MsgBox("Error searching SQL instances.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
        End Try
        '== Console.WriteLine("Press any key to continue.")
        '== Console.ReadKey()
    End Function  '-- SQL_Enumerate-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '= = = =  c o n n e c t = = = =
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

    '-- Get SQL Select ANY Value Type (No TRANSACTION current).
    '--==  -- (cmd.getScalar)--
    '-- http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.executescalar(v=vs.90).ASPX

    Public Function gbGetSqlScalarValue(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSqlSelect As String, _
                                           ByRef objResult As Object) As Boolean
        Dim sqlCmd1 As OleDbCommand  '== SqlCommand
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbGetSqlScalarValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSqlSelect, cnnSql)  '== SqlCommand(sSqlSelect, cnnSql)
            objResult = sqlCmd1.ExecuteScalar
            gbGetSqlScalarValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSqlSelect & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarValue-
    '= = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Get Select INTEGER Value (No TRANSACTION current).
    '--==  -- (cmd.getScalar)--

    Public Function gbGetSqlScalarIntegerValue(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSql As String, _
                                           ByRef intResult As Integer) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbGetSqlScalarIntegerValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)  '= SqlCommand(sSql, cnnSql)
            intResult = sqlCmd1.ExecuteScalar
            gbGetSqlScalarIntegerValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Get Select INTEGER Value DURING SQL TRANSACTION.-
    '--==  -- (cmd.getScalar)--
    '--  IF in Transaction, RollBack in the event of failure..-

    Public Function gbGetSqlScalarIntegerValue_Trans(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSql As String, _
                                          ByVal bIsTransaction As Boolean, _
                                           ByRef sqlTran1 As OleDbTransaction, _
                                           ByRef intResult As Integer) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        gbGetSqlScalarIntegerValue_Trans = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)  '=SqlCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intResult = sqlCmd1.ExecuteScalar
            gbGetSqlScalarIntegerValue_Trans = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntegerValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue_TRANS-
    '= = = = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Execute Command -
    '---  (DOES NOT start another transaction..)--

    Public Function gbExecuteCmd(ByRef cnnUserDB As OleDbConnection, _
                                      ByVal sSql As String, _
                                        ByRef lAffected As Integer, _
                                         ByRef sErrorMsg As String) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String
        Dim lRecordsAff As Integer
        Dim lError As Integer

        '== On Error GoTo GetRst_Error
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql

        msLastSqlErrorMessage = ""
        '---DEFAULT timeout is 30 secs--
        sErrorMsg = ""
        Try
            lRecordsAff = cmd1.ExecuteNonQuery
            lAffected = lRecordsAff '--return result--
            gbExecuteCmd = True
        Catch ex As Exception
            lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbExecuteCmd:  Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            gbExecuteCmd = False
        End Try

    End Function  '-gbExecuteCmd-
    '= = = = = = = = = = = = = = = =
    '-== = =
    '-===FF->

    '-- NEW- Execute SQL Command..--
    '-- Ex POS3- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Public Function gbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction, _
                                       ByRef intAffected As Integer, _
                                         ByRef sErrorMsg As String) As Boolean
        Dim sqlCmd1 As OleDbCommand
        '= Dim intAffected As Integer
        Dim sMsg As String
        Dim sRollback As String = ""

        gbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            gbExecuteSql = True   '--ok--
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
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            '== MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-gbExecuteSql-
    '= = = = = = = = = = = = = = = = =
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


    '--get list of User-defined types--

    Public Function gbGetUserTypes(ByRef cnn1 As OleDbConnection, _
                                     ByVal sDBName As String, _
                                      ByRef colTypeList As Collection) As Boolean
        Dim j, i, k As Integer
        Dim s1, s2 As String
        Dim sList, sErrors As String
        Dim rs As OleDbDataReader '== ADODB.Recordset
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbGetUserTypes = False
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, k, sErrors) Then
            If gbDebug Then MsgBox("GetUserTypes: Failed sql: " & s1)
            msLastSqlErrorMessage = "GetUserTypes: Failed sql: " & s1
        End If
        sList = "Found User-types: " & vbCrLf
        lResult = glExecSP(cnn1, "sp_help", "", sErrors, rs)
        If (lResult <> 0) Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            '--check recordset--
            colTypeList = New Collection
            While Not (rs Is Nothing)
                If rs.HasRows Then
                    '--if not empty, current pos is First record--(seeADO)_-
                    While rs.Read  '== Not rs.EOF
                        If (LCase(rs.GetName(0)) = "type_name") Then '--r/set for user types--
                            s1 = rs.Item("type_name") '--get user type name--
                            s2 = rs.Item("storage_type") '--underlying SQL-server type name-
                            colTypeList.Add(s2, UCase(s1)) '--user type is Key--
                            sList = sList & s1 & "(" & s2 & ")" & vbCrLf
                        Else  '--not user-types rset..
                            Exit While
                        End If
                        '== rs.MoveNext()
                    End While
                    gbGetUserTypes = True
                    If gbDebug Then MsgBox(sList, MsgBoxStyle.Information) '--test--
                    '== End If '--not empty-
                End If  '--has rows..
                If Not rs.NextResult() Then Exit While '==rs = rs.NextRecordset
            End While  '--nothing
        End If '--ok--
        If Not (rs Is Nothing) Then rs.Close()
        rs = Nothing
    End Function '--getUserTypes--
    '= = = =  = = = =
    '= = = =  = = = =
    '-===FF->

    '-- SQL functions==11June2001==
    '---==Get result Set ==
    '--Caller supplies full select string--

    Public Function gbGetReader(ByRef cnnUserDB As OleDbConnection, _
                               ByRef rdr1 As OleDbDataReader, _
                                ByVal sSql As String, _
                                  Optional ByVal blnReadWrite As Boolean = False) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String

        '== On Error GoTo GetRst_Error
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql
        Try
            rdr1 = cmd1.ExecuteReader
            If (Not (rdr1 Is Nothing)) Then  '--ok-
                gbGetReader = True '--blnSuccess = True
            Else  '--not open.-
                sMsg = " Error in 'gbGetRst' (Get Recordset):" & vbCrLf & _
                           "ERROR: Recordset not available, or is closed.." & vbCrLf
                sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sMsg)
                '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = sMsg
                gbGetReader = False
            End If  '--open-

        Catch ex As Exception
            sMsg = " Error executing sql cmd in 'gbGetReader' (Get Recordset):" & vbCrLf & _
                      "ERROR text: " & vbCrLf & ex.Message & vbCrLf
            sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
            If (gsErrorLogPath() <> "") Then
                Call gbLogMsg(gsErrorLogPath, sMsg)
            End If '--log--
            '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            gbGetReader = False
        End Try
        Exit Function

    End Function  '--gbGetReader--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '---  Get result Set  INTO DataTable
    '==  NEW for JobMatix POS.. ==
    '--Caller supplies full select string--
    '--  TRANSACTION is current..-

    Public Function gbGetDataTableEx(ByRef cnnUserDB As OleDbConnection, _
                                     ByRef dataTable1 As DataTable, _
                                      ByVal sSql As String, _
                                       ByRef sqlTran1 As OleDbTransaction) As Boolean

        Dim cmd1 As New OleDbCommand
        Dim sMsg As String
        Dim rdr1 As OleDbDataReader

        gbGetDataTableEx = False
        msLastSqlErrorMessage = ""
        If sqlTran1 Is Nothing Then
            msLastSqlErrorMessage = "Transaction object is missing.."
            MsgBox("Transaction object is missing..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql
        cmd1.Transaction = sqlTran1
        Try
            rdr1 = cmd1.ExecuteReader
            If (Not (rdr1 Is Nothing)) Then  '--ok-
                dataTable1 = New DataTable
                dataTable1.Load(rdr1)
                gbGetDataTableEx = True '--blnSuccess = True
                rdr1.Close()
            Else  '--not open.-
                sqlTran1.Rollback()
                sMsg = " Error in 'gbGetDataTable' (Get Recordset):" & vbCrLf & _
                           "ERROR: Recordset not available, or is closed.." & vbCrLf
                sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & _
                              vbCrLf & "RollBack was executed.." & vbCrLf & _
                             "--- end error msg --" & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sMsg)
                '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = sMsg
                gbGetDataTableEx = False
            End If  '--open-

        Catch ex As Exception
            sqlTran1.Rollback()
            sMsg = " Error executing sql cmd in 'gbGetDataTable' (Get Recordset):" & vbCrLf & _
                      "ERROR text: " & vbCrLf & ex.Message & vbCrLf
            sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & _
                              vbCrLf & "RollBack was executed.." & vbCrLf & _
                              "--- end error msg --" & vbCrLf
            If (gsErrorLogPath() <> "") Then
                Call gbLogMsg(gsErrorLogPath, sMsg)
            End If '--log--
            '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            gbGetDataTableEx = False
        End Try
        If (Not (rdr1 Is Nothing)) Then
            rdr1.Close()
        End If
        Exit Function
    End Function  '-gbGetDataTableEx-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '==  24-March-2014  ----
    '---  Get result Set  INTO DataTable
    '==  NEW for JobMatix POS.. ==
    '--Caller supplies full select string--

    Public Function gbGetDataTable(ByRef cnnUserDB As OleDbConnection, _
                                     ByRef dataTable1 As DataTable, _
                                      ByVal sSql As String) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String
        Dim rdr1 As OleDbDataReader

        gbGetDataTable = False
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql
        Try
            rdr1 = cmd1.ExecuteReader
            If (Not (rdr1 Is Nothing)) Then  '--ok-
                dataTable1 = New DataTable
                dataTable1.Load(rdr1)
                gbGetDataTable = True '--blnSuccess = True
                rdr1.Close()
            Else  '--not open.-
                sMsg = " Error in 'gbGetDataTable' (Get Recordset):" & vbCrLf & _
                           "ERROR: Recordset not available, or is closed.." & vbCrLf
                sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sMsg)
                '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = sMsg
                gbGetDataTable = False
            End If  '--open-

        Catch ex As Exception
            sMsg = " Error executing sql cmd in 'gbGetDataTable' (Get Recordset):" & vbCrLf & _
                      "ERROR text: " & vbCrLf & ex.Message & vbCrLf
            sMsg = sMsg & "SQL Was: " & vbCrLf & sSql & vbCrLf & "--- end error msg --" & vbCrLf
            If (gsErrorLogPath() <> "") Then
                Call gbLogMsg(gsErrorLogPath, sMsg)
            End If '--log--
            '== If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            gbGetDataTable = False
        End Try
        If (Not (rdr1 Is Nothing)) Then
            rdr1.Close()
        End If
        Exit Function

    End Function  '--gbGet datatable--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--
    '--- REURNS False only if ERRORS..
    '--   Value of NOTHING is a valid return result--

    Public Function gbGetSelectValueEx(ByRef cnnSql As OleDbConnection, _
                                              ByVal sSql As String, _
                                             ByRef vResult As Object) As Boolean
        Dim rs1 As DataTable  '== ADODB.Recordset
        Dim sErrorMsg As String

        gbGetSelectValueEx = False
        vResult = Nothing     '--valid return result--
        Try
            If Not gbGetDataTable(cnnSql, rs1, sSql) Then
                sErrorMsg = "DB ERROR-  Function 'gbGetSelectValueEx'" & vbCrLf & _
                             " Failed to get SELECT recordset " & "for SQL:" & vbCrLf & sSql & vbCrLf & _
                       "Error text: " & vbCrLf & gsGetLastSqlErrorMessage()
                Call gbLogMsg(gsErrorLogPath, sErrorMsg & vbCrLf & "-- end of error msg.--")
            Else '--get first selected value.-....-
                If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                    '==If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '==rs1.MoveFirst()
                    Dim datarow1 As DataRow = rs1.Rows(0)  '--first row-
                    If Not IsDBNull(datarow1.Item(0)) Then
                        vResult = datarow1.Item(0)
                    End If '--null.-
                    gbGetSelectValueEx = True '--got something..-
                    '== End If '--bof-
                    '==rs1.Close()
                End If '--nothing 
            End If '--get-
        Catch ex As Exception
            sErrorMsg = "EXCEPTION ERROR-  Function 'gbGetSelectValueEx'" & vbCrLf & _
               " Failed to get SELECT recordset " & "for SQL:" & vbCrLf & sSql & vbCrLf & _
               "Error text: " & vbCrLf & ex.Message
            msLastSqlErrorMessage = sErrorMsg
            Call gbLogMsg(gsErrorLogPath, sErrorMsg & vbCrLf & "-- end of error msg.--")
        End Try
        rs1 = Nothing
    End Function '--getSelect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--

    Public Function gbGetSelectValue(ByRef cnnSql As OleDbConnection, _
                                       ByVal sSql As String, _
                                         ByRef vResult As Object) As Boolean
        Dim rs1 As OleDbDataReader '= ADODB.Recordset
        Dim sErrorMsg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        gbGetSelectValue = False
        Try
            If Not gbGetReader(cnnSql, rs1, sSql) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                sErrorMsg = "Failed to get SELECT recordset.." & vbCrLf & _
                       "Error text: " & vbCrLf & gsGetLastSqlErrorMessage()
                msLastSqlErrorMessage = sErrorMsg
                Exit Function
            Else '--get first selected value.-....-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                If Not (rs1 Is Nothing) Then
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                        '== rs1.MoveFirst()
                        If rs1.Read Then
                            If Not IsDBNull(rs1.Item(0)) Then
                                vResult = rs1.Item(0)
                            End If '--null.-
                        End If  '--read-
                    End If '--bof-
                    gbGetSelectValue = True '--got something..-
                    rs1.Close()
                Else
                    msLastSqlErrorMessage = "No dataReader returned from SELECT Query !!"
                End If '--nothing
            End If '--get-
        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sErrorMsg = "== ERROR in mbGetSelectValue.." & vbCrLf & ex.Message & vbCrLf
            msLastSqlErrorMessage = sErrorMsg
            Call gbLogMsg(gsRuntimeLogPath, sErrorMsg)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not (rs1 Is Nothing) Then rs1.Close()
            Exit Function
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- set current database..--
    '-  !! MUST retry because for non-admin user -
    '----  it doesn't stick the first time..--

    Public Function gbSetCurrentDatabase(ByRef cnnSQL As OleDbConnection, _
                                         ByVal sDBName As String) As Boolean

        Dim sSql, sErrors, sMsg As String
        Dim sCurrentDB As String = ""
        Dim lngStart, L1 As Integer
        Dim intUseCount As Integer = 0
        Dim v1 As Object = Nothing

        Try  '--main try-
            gbSetCurrentDatabase = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Do
                Try
                    cnnSQL.ChangeDatabase(sDBName)
                Catch ex As Exception
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Call gbLogMsg(gsErrorLogPath, "= Failed in Change-DATABASE: " & sDBName & vbCrLf & _
                                                 ex.Message & vbCrLf & "-- end of error msg.--")
                    MsgBox("= Failed in USE for DATABASE: " & sDBName & " = =" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    Exit Function '-- False..-
                End Try
                intUseCount += 1
                '--  check USE result for current db..-
                '== If gbGetSelectValueEx(cnnSQL, "SELECT DB_NAME() AS current_db_name;", v1) Then
                '==   sCurrentDB = CStr(v1)
                '== End If
                sCurrentDB = cnnSQL.Database
                If (LCase(sCurrentDB) <> LCase(sDBName)) Then
                    lngStart = CInt(VB6.Timer()) '--PAUSE.. starting seconds.-
                    While (CInt(VB6.Timer()) < lngStart + 2)
                        System.Windows.Forms.Application.DoEvents()
                    End While
                End If '-not db.-
            Loop Until (LCase(sCurrentDB) = LCase(sDBName)) Or (intUseCount >= 5)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '-- check result..-
            If (LCase(sCurrentDB) = LCase(sDBName)) Then gbSetCurrentDatabase = True
            Exit Function
        Catch ex As Exception
            sMsg = "== ERROR in gbSetCurrentDatabase function.." & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            Exit Function
        End Try  '--main try-

    End Function '-SetCurrentDatabase-
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- test sql server user condition.-
    '----  the SELECT statement provided should return a single value.-

    Public Function gbTestSqlUser(ByRef cnnSQL As OleDbConnection, _
                            ByVal strSelectQuery As String) As Boolean

        Dim rs1 As OleDbDataReader  '-ADODB.Recordset
        Dim vResult As Object
        Dim sMsg As String

        Try
            gbTestSqlUser = False
            If Not gbGetReader(cnnSQL, rs1, strSelectQuery) Then
                MsgBox("Failed to get SELECT recordset for query: <" & strSelectQuery & ">.." & vbCrLf & _
                        "Error text: " & gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Else '--get first selected value.-....-
                If Not (rs1 Is Nothing) Then
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                        '== If rs1.BOF Then rs1.MoveFirst()
                        If rs1.Read Then
                            If Not IsDBNull(rs1(0)) Then  '=  (rs1.Fields(0).Value) Then
                                vResult = rs1(0)  '== rs1.Fields(0).Value
                                If CShort(vResult) = 1 Then gbTestSqlUser = True '--got "1"..-
                            End If '--null.-
                        End If
                    End If '---empty-
                End If '--nothing..-
                rs1.Close()
            End If '--get rst.-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Catch ex As Exception
            sMsg = "== ERROR in mbTestSqlUser function.." & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not (rs1 Is Nothing) Then rs1.Close()
            Exit Function
        End Try
    End Function '-testSql--
    '= = = = = = = = = = =
    '-===FF->

    '-- Get DB_ID --
    '--  Returns false if no such database..
    '-- Result returned in intDb_id --

    Public Function gbGetDB_ID(ByRef cnnSql As OleDbConnection, _
                        ByVal strDatabaseName As String, _
                                ByRef intDb_id As Integer) As Boolean
        Dim sSql As String
        Dim sqlCmd1 As OleDbCommand
        '= Dim intResult As Integer
        Dim objResult As Object

        gbGetDB_ID = False
        intDb_id = -1
        If strDatabaseName = "" Then Exit Function
        sSql = "SELECT DB_ID('" & strDatabaseName & "') AS DB_ID; "
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            '==intResult = CInt(sqlCmd1.ExecuteScalar)
            objResult = sqlCmd1.ExecuteScalar
            If Not IsDBNull(objResult) Then
                intDb_id = CInt(objResult)
            End If
            gbGetDB_ID = True  '=no error--
        Catch ex As Exception
            msLastSqlErrorMessage = "gbGetDB_ID:  Error in Sql ExecuteScalar cmd. " & vbCrLf & ex.Message
        End Try
        '==If gbGetSelectValueEx(cnnSql, sSql, vResult) Then  '--not error-
        '==If Not (vResult Is Nothing) Then
        '==intDb_id = CInt(vResult)
        '==End If
        '==gbGetDB_ID = True  '=no error--
        '==End If
    End Function  '--gbGetDB_ID--
    '= = = = = = = = = = = = = =

    '--exists database--
    '-- get list of databases and check if arg db is included--
    '==3073.309==   use DB_ID --

    Public Function gbExistsDatabase(ByRef cnn1 As OleDbConnection, _
                                     ByVal strDatabaseName As String) As Boolean
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbExistsDatabase = False
        If gbGetDB_ID(cnn1, strDatabaseName, lResult) Then  '--NO error.--
            If (lResult > 0) Then    '--valid Db_id..-
                gbExistsDatabase = True
            End If
        Else  '--error--
            MsgBox("DB Error- 'gbExistsDatabase'.." & vbCrLf & _
                        msLastSqlErrorMessage, MsgBoxStyle.Exclamation)
        End If
    End Function '--exists--
    '= = = =  = = = =
    '= = = = = = = = =
    '-===FF->

    '--  SQL server 2000.--

    '--get list of existing databases--
    '--get list of existing databases--

    Public Function gbGetDatabases(ByRef cnn1 As OleDbConnection, _
                                   ByRef colDBlist As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader '== ADODB.Recordset
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbGetDatabases = False
        lResult = glExecSP(cnn1, "sp_databases", "", sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox("gbGetDatabases-" & vbCrLf & sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = sErrors
        Else
            '--check recordset--
            colDBlist = New Collection
            If Not (rs Is Nothing) Then
                '--if not empty, current pos is First record--(seeADO)_-
                If rs.HasRows Then
                    '==If rs.BOF And (Not rs.EOF) Then rs.MoveFirst() '--shouldn't be necessary--
                    While rs.Read  '== Not rs.EOF
                        '--If UCase$(rs("DATABASE_NAME")) = UCase$(sDBName) Then
                        s1 = rs.Item("DATABASE_NAME")
                        colDBlist.Add(s1) '--rs("DATABASE_NAME")
                        '==rs.MoveNext()
                    End While
                End If  '--has rows-
                gbGetDatabases = True
                '== rs.NextResult() '==rs = rs.NextRecordset
            End If  '--nothing-
            If Not (rs Is Nothing) Then rs.Close()
        End If '--ok--
        rs = Nothing
    End Function '--getDBlist--
    '= = = = = = = = =
    '-===FF->

    '--  SQL Server 2005 Plus..
    '--  SQL Server 2005 Plus..

    '--get list of existing databases--
    '--get list of existing databases--

    Public Function gbGetDatabasesSQL2005(ByRef cnn1 As OleDbConnection, _
                                      ByRef colDBlist As Collection) As Boolean
        Dim sSql, s1 As String
        Dim rs1 As OleDbDataReader '== ADODB.Recordset

        msLastSqlErrorMessage = ""
        gbGetDatabasesSQL2005 = False
        sSql = "SELECT name, database_id, owner_sid FROM sys.databases; "
        If Not gbGetReader(cnn1, rs1, sSql) Then
            '== MsgBox("Failed to retrieve list of databases..", MsgBoxStyle.Exclamation)
            s1 = "Failed to retrieve list of databases.." & vbCrLf & _
                              "Error msg: " & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf
            If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = s1
        Else
            '--check recordset--
            colDBlist = New Collection
            If Not (rs1 Is Nothing) Then
                '--if not empty, current pos is First record--(seeADO)_-
                If rs1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then '-- not empty
                    '==rs1.MoveFirst() '--shouldn't be necessary--
                    While rs1.Read  '== Not rs1.EOF
                        s1 = rs1.Item("NAME")
                        colDBlist.Add(s1) '--rs("DATABASE_NAME")
                        '==rs1.MoveNext()
                    End While
                    gbGetDatabasesSQL2005 = True
                End If '--empty.-
                '== Call rs1.NextResult()   '=rs1 = rs1.NextRecordset
            End If  '--nothing-
        End If '--ok--
        If Not (rs1 Is Nothing) Then rs1.Close()
        rs1 = Nothing
    End Function '--getDBlist--
    '= = = = = = = = = = = = =
    '-===FF->

    '- V3.1  -- with POS support--
    '- V3.1  -- with POS support--
    '--  SQL server 2000/2005 Plus.--

    '--get list of existing Jobmatix databases--
    '--  Filter out system DB's..
    '--  Filter for "jobtracking" and "_jmpos" DB's --

    Public Function gbGetJobmatixDatabases(ByRef cnnSql As OleDbConnection, _
                                           ByRef colAllJobsDBs As Collection, _
                                           ByRef colUserJobsDBs As Collection) As Boolean
        Dim bOk As Boolean
        Dim col1, colMyList As Collection
        Dim s1, s2, sName As String

        gbGetJobmatixDatabases = False
        If gbIsSqlServer2005Plus() Then '-- 9=2005,  10=2008..--
            bOk = gbGetDatabasesSQL2005(cnnSql, colMyList)
        Else '--  <"9".. assume sql Server 2000..---
            '--get list of db's for this sql server 2000--
            '-- IN SQL-2000, this only works for PUBLIC..--
            bOk = gbGetDatabases(cnnSql, colMyList)
        End If '--2005..--

        If bOk Then
            colAllJobsDBs = New Collection  '-- all regardless if user has access..
            colUserJobsDBs = New Collection  '-- Those the user has access to..
            For Each sName In colMyList
                '= sName = CStr(vName) '--??-
                s2 = LCase(sName)
                If (s2 <> "master") And (s2 <> "model") And (s2 <> "msdb") And (s2 <> "tempdb") Then '-ok-
                    colAllJobsDBs.Add(sName, sName)  '--name is both Key and data.-
                    '== sMsg = sMsg & sName & vbCrLf
                    '-- collect jobtracking db's..-
                    If (InStr(s2, "jobtracking") > 0) Or (InStr(s2, "_jmpos") > 0) Then
                        '--check user has access to this DB..--
                        '--  User DB collection is Collection of Collections.
                        bOk = gbTestSqlUser(cnnSql, "SELECT HAS_DBACCESS ('" & sName & "'); ")
                        If mbIsSqlAdmin Or ((Not mbIsSqlAdmin) And bOk) Then
                            col1 = New Collection
                            col1.Add(sName, "dbname")
                            colUserJobsDBs.Add(col1, sName)
                        End If '--admin..-
                    End If
                End If '--not master-
            Next sName '--each name-
            gbGetJobmatixDatabases = True
        End If  '--ok-

    End Function  '-gbGetJobmatixDatabases-
    '= = = = = = = = = =  = = = = = = == =
    '-===FF->

    '--  get recordset as collection for SELECT..--

    Public Function gbGetRecordCollection(ByRef cnnSQL As OleDbConnection, _
                                            ByVal sSql As String, _
                                             ByRef colResult As Collection) As Boolean
        Dim rs1 As OleDbDataReader '= ADODB.Recordset
        Dim sName, sErrorMsg As String
        Dim col1 As Collection
        Dim colRow As Collection
        '== Dim fld1 As ADODB.Field
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim column1 As DataColumn

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        gbGetRecordCollection = False
        If Not gbGetReader(cnnSQL, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset.." & vbCrLf & _
             "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If Not (rs1 Is Nothing) Then
                If rs1.HasRows Then  '=  Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    dataTable1 = New DataTable
                    '--  Get data "recordset" to internal table..
                    dataTable1.Load(rs1)
                    '= dataTable1.TableName = msTableName
                    colResult = New Collection
                    '== rs1.MoveFirst()
                    For Each row1 In dataTable1.Rows
                        colRow = New Collection
                        For Each column1 In dataTable1.Columns '==  For i = 0 To rs.Fields.Count - 1
                            sName = column1.ColumnName   '== rs.Fields(i).Name
                            col1 = New Collection
                            col1.Add(sName, "name")
                            col1.Add(row1.Item(sName), "value")
                            colRow.Add(col1, LCase(sName))
                        Next column1
                        colResult.Add(colRow)
                    Next row1
                    gbGetRecordCollection = True '--got something..-
                End If '--EMPTY. bof-
                rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSelect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--exists user--
    '-- get list of users and check if arg User is included--

    Public Function gbExistsLogin(ByRef cnn1 As OleDbConnection, _
                             ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader '= ADODB.Recordset
        Dim lResult As Integer

        msLastSqlErrorMessage = ""
        gbExistsLogin = False
        lResult = glExecSP(cnn1, "sp_helplogins", "", sErrors, rs)
        If (lResult <> 0) Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = sErrors
        Else
            s1 = "User Logins are: "
            '--check recordset--
            If Not (rs Is Nothing) Then
                '--If rs.RecordCount > 0 Then
                If rs.HasRows Then  '= rs.BOF And (Not rs.EOF) Then rs.MoveFirst()
                    While rs.Read '== Not rs.EOF
                        '==  s1 = s1 + rs.Fields("LoginName").Value + ";  "
                        '--MsgBox "checking db exists: " & rs("DATABASE_NAME")
                        If UCase(rs.Item("LoginName")) = UCase(sLoginName) Then
                            gbExistsLogin = True
                        End If
                        '== rs.MoveNext()
                    End While
                    rs.NextResult()  '= = rs.NextRecordset
                End If  '--has roes.-
                rs.Close()
            End If  '= While  '-nothing-
            '--MsgBox s1   '--testing--
        End If '--ok-
        rs = Nothing
    End Function '--exists user--
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--DROP login..--

    '--drop sql login--
    Public Function gbDropLogin(ByRef cnn1 As OleDbConnection, _
                             ByVal sLoginName As String) As Boolean

        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '== ADODB.Recordset
        Dim lResult As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        sParms = "@loginame='" & sLoginName & "' "
        gbDropLogin = False
        lResult = glExecSP(cnn1, "sp_droplogin", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sErrors
        Else
            gbDropLogin = True
        End If
        rs = Nothing
    End Function '--drop login--
    '= = = = = = = = = = =

    '--add sql login--
    Public Function gbAddLogin(ByRef cnn1 As OleDbConnection, _
                               ByVal sLoginName As String, _
                                ByRef sPassword As String, _
                                   ByRef sDefDB As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '=ADODB.Recordset
        Dim lResult As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        sParms = "@loginame='" & sLoginName & "' & @passwd='" & sPassword & _
                                                         "' & @defdb='" & sDefDB & "'"
        gbAddLogin = False
        lResult = glExecSP(cnn1, "sp_addlogin", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = sErrors
        Else
            gbAddLogin = True
        End If
        rs = Nothing
    End Function '--add login--
    '= = = = = = = = = = =
    '-===FF->

    '---  Add Windows NT user 'domain\user' to SQL logins.--
    '---  Add Windows NT user 'domain\user' to SQL logins.--

    Public Function gbGrantLogin(ByRef cnn1 As OleDbConnection, _
                               ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader   '=ADODB.Recordset
        Dim lResult As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        sParms = "@loginame='" & sLoginName & "' "
        gbGrantLogin = False
        lResult = glExecSP(cnn1, "sp_grantlogin", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sErrors
        Else
            gbGrantLogin = True
        End If
        rs = Nothing
    End Function '--GRANT login--
    '= = = = = = = = = = =
    '-===FF->

    '==3069=- Check for a specific permission existing...-
    '===     Return the StateDEscr. (ie GRANT, DENY, REVOKE, GRANT_WITH_GRANT_OPTION..)--
    '-- USE master
    '-- SELECT PL.name as grantee_name, 
    '--        PM.state_desc, 
    '--        PM.permission_name 
    '-- FROM
    '--   sys.server_permissions AS PM 
    '-- JOIN sys.server_principals AS PL 
    '--   ON  PM.grantee_principal_id = PL.principal_id
    '-- WHERE permission_name = 'VIEW Server State' ;
    '-- GO
    '== Sql Server 2005 or later ONLY..--

    Public Function gbPermissionExists(ByRef cnn1 As OleDbConnection, _
                                        ByVal strGranteeName As String, _
                                         ByVal strPermission As String, _
                                         ByRef strStateDescr As String) As Boolean
        Dim sSql, s1, sErrors, sMsg As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim lResult, L1 As Integer

        msLastSqlErrorMessage = ""
        gbPermissionExists = False
        strStateDescr = ""
        Try
            If Not gbIsSqlServer2005Plus() Then Exit Function
            '--USE--
            s1 = " USE master; "
            If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
                If gbDebug Then MsgBox("gbPermissionExists- Failed sql: " & s1)
                msLastSqlErrorMessage = "gbPermissionExists- Failed sql: " & s1
            End If
            sSql = "  SELECT PL.name as grantee_name, " & _
                   "       PM.state_desc, " & _
                   "       PM.permission_name " & _
                   "    FROM  sys.server_permissions AS PM" & _
                   "     JOIN sys.server_principals AS PL " & _
                   "        ON  PM.grantee_principal_id = PL.principal_id " & _
                   "    WHERE (PM.permission_name ='" & strPermission & "') AND " & _
                   "           (PL.name ='" & strGranteeName & "'); "

            If Not gbGetDataTable(cnn1, rs1, sSql) Then
                If gbDebug Then MsgBox("gbPermissionExists-" & vbCrLf & _
                         "Failed to retrieve list of Permissions..", MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & _
                                  "gbPermissionExists-" & vbCrLf & _
                                      "Failed to retrieve list of Permissions.."
            Else
                '--check recordset--
                If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                    '--if not empty, current pos is First record--(seeADO)_-
                    '==If Not (rs1.BOF And rs1.EOF) Then '-- not empty
                    '== rs1.MoveFirst() '--shouldn't be necessary--
                    Dim datarow1 As DataRow = rs1.Rows(0)  '--first row-
                    '--  Any match is good--
                    strStateDescr = CStr(datarow1.Item("state_desc"))
                    gbPermissionExists = True
                    '== End If '--empty.-
                End If  '--nothing- 
            End If '--ok--
            rs1 = Nothing
            Exit Function
        Catch ex As Exception
            sMsg = "ERROR in gbPermissionExists function.." & vbCrLf & ex.Message & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            If gbDebug Then MsgBox(sMsg, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = sMsg
            Exit Function
        End Try
    End Function  '--gbPermissionExists-
    '= = = = = = = = = = = =
    '-===FF->

    '==3069=- Check for a specific permission being GRANTED...-
    '--     strGranteeName if the users Login name. ---
    Public Function gbIsPermissionGranted(ByRef cnn1 As OleDbConnection, _
                                        ByVal strGranteeName As String, _
                                         ByVal strPermission As String) As Boolean
        Dim sStateDescr As String = ""

        msLastSqlErrorMessage = ""
        gbIsPermissionGranted = False
        If Not gbIsSqlServer2005Plus() Then
            If (UCase(strPermission) = "VIEW SERVER STATE") Then  '--this permitted in sql-2000..
                gbIsPermissionGranted = True
            End If
        Else  '--2005 and later..  we can ask..-
            If gbPermissionExists(cnn1, strGranteeName, strPermission, sStateDescr) Then
                If (UCase(Left(sStateDescr, 5)) = "GRANT") Then
                    gbIsPermissionGranted = True
                End If
            End If
        End If  '--2005-
    End Function  '--gbPermissionGranted-
    '= = = = = = = = = = = =

    '--  GRANT permission for  "VIEW SERVER STATE"  --

    Public Function gbGrantVWSSPermission(ByRef cnn1 As OleDbConnection, _
                                         ByVal strGranteeName As String) As Boolean

        Dim sSql, s1, sErrors As String
        Dim L1 As Integer
        '==Dim v1 As Object

        gbGrantVWSSPermission = False
        msLastSqlErrorMessage = ""
        '--USE--
        '== sSql = "USE master; "
        Try
            cnn1.ChangeDatabase("master")
        Catch ex As Exception
            s1 = "gbGrantVWSSPermission:  Failed USE master sql: " & vbCrLf & ex.Message
            If gbDebug Then MsgBox(s1 & vbCrLf & sSql)
            msLastSqlErrorMessage = s1
        End Try
        '== If Not gbSetCurrentDatabase(cnn1, "master") Then
        '= s1 = "gbGrantVWSSPermission:  Failed USE master sql: "
        '= If gbDebug Then MsgBox(s1 & vbCrLf & sSql)
        '= msLastSqlErrorMessage = s1
        '= End If
        '-- TESTING-  check USE result for current db..-
        '== If gbGetSelectValue(cnn1, "SELECT DB_NAME() AS current_db_name;", v1) Then
        '== sCurrentDB = CStr(v1)
        '== MsgBox("Current DB is: " & sCurrentDB)
        '== Else
        '== MsgBox("Failed: Get current db..")
        '== End If

        sSql = "GRANT VIEW SERVER STATE TO [" & strGranteeName & "]; "
        If gbExecuteCmd(cnn1, sSql, L1, sErrors) Then
            gbGrantVWSSPermission = True
        Else
            If gbDebug Then MsgBox("gbGrantVWSSPermission:  Failed sql: " & vbCrLf & sSql)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & _
                                 "gbGrantVWSSPermission:  Failed sql: " & vbCrLf & sSql
        End If
    End Function  '-GrantVWSSPermission-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Grant DB Access -- (replaces addDBUser..)-
    '-- Grant DB Access -- (replaces addDBUser..)-

    Public Function gbGrantDBAccess(ByRef cnn1 As OleDbConnection, _
                                  ByVal sDBName As String, _
                                   ByVal sLoginName As String) As Boolean
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader   '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbGrantDBAccess- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbGrantDBAccess- Failed sql: " & s1
        End If

        sParms = "@loginame='" & sLoginName & "' & @name_in_db='" & sLoginName & "'"
        gbGrantDBAccess = False
        lResult = glExecSP(cnn1, "sp_grantdbaccess", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbGrantDBAccess = True
        End If
        rs = Nothing
    End Function '--add user--
    '= = = = = = = = = = =

    '--add Role..--
    '--add Role..--
    Public Function gbAddRoleMember(ByRef cnn1 As OleDbConnection, _
                                      ByVal sDBName As String, _
                                       ByVal sLoginName As String, _
                                         ByVal sRoleName As String) As Boolean
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbAddRoleMember- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbAddRoleMember- Failed sql: " & s1
        End If
        sParms = "@rolename='" & sRoleName & "' & @membername='" & sLoginName & "'"
        gbAddRoleMember = False
        lResult = glExecSP(cnn1, "sp_addrolemember", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbAddRoleMember = True
        End If
        rs = Nothing
    End Function '--add user--
    '= = = = = = = = = = =
    '-===FF->

    '--add DB user=--
    Public Function gbAddDBuser(ByRef cnn1 As OleDbConnection, _
                               ByVal sDBName As String, _
                               ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '== ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbAddDBuser- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbAddDBuser- Failed sql: " & s1
        End If
        sParms = "@loginame='" & sLoginName & "' & @name_in_db='" & sLoginName & "'"
        gbAddDBuser = False
        lResult = glExecSP(cnn1, "sp_adduser", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbAddDBuser = True
        End If
        rs = Nothing
    End Function '--add user--
    '= = = = = = = = = = =

    '--  Drop User account in DB..--
    '--  Drop User account in DB..--
    '-- g b D r o p D B u s e r ---

    Public Function gbDropDBuser(ByRef cnn1 As OleDbConnection, _
                                         ByVal sDBName As String, _
                                          ByVal sNameInDB As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '==  ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbDropDBuser- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbDropDBuser- Failed sql: " & s1
        End If

        sParms = "@name_in_db='" & sNameInDB & "'"
        gbDropDBuser = False
        lResult = glExecSP(cnn1, "sp_revokedbaccess", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox("Failed to drop DB user: " & sNameInDB & vbCrLf & _
                                        sErrors, MsgBoxStyle.Exclamation)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbDropDBuser = True
        End If
        rs = Nothing
    End Function '--drop user--
    '= = = = = = = = = = =
    '-===FF->

    '-- Change DB Owner..--
    '-- Change DB Owner..--
    Public Function gbChangeDBOwner(ByRef cnn1 As OleDbConnection, _
                                  ByVal sDBName As String, _
                                   ByVal sLoginName As String) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs As OleDbDataReader  '==  As ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim sParms As String

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("gbChangeDBOwner- Failed sql: " & s1)
            msLastSqlErrorMessage = "gbChangeDBOwner- Failed sql: " & s1
        End If

        sParms = "@loginame='" & sLoginName & "'  "
        gbChangeDBOwner = False
        lResult = glExecSP(cnn1, "sp_changedbowner", sParms, sErrors, rs)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else
            gbChangeDBOwner = True
        End If
        rs = Nothing
    End Function '--change owner--
    '= = = = = = = = = = =
    '= = = = = = = = = = =
    '-===FF->

    '--  sp_helpuser to get JobTrackin DB users..--
    '---  via gbGetUsers (cnn, dbname, colUsers)  ----
    '--- RETURNS Collection of User collections..-
    '---  Each User collection has items with keys: LOGINNAME ans USERNAME -
    '----    As per "sp_helpuser" recordset..--

    '--NB: 17July2011--  JobMatix Rev-2912 ++  ==
    '---- SQL Server 2008.. "sp_helpuser"  RETURNS "RoleName" instead of "GroupName"--
    '------ We MUST detect this and still return "GroupName" to caller..---

    Public Function mbGetUsersEx_SQL2008(ByRef cnn1 As OleDbConnection, _
                                       ByVal sDBName As String, _
                                       ByRef colUserNames As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim s1, sErrors As String
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim colUser1 As Collection
        Dim vGroup As Object

        msLastSqlErrorMessage = ""
        '--USE--
        s1 = " USE " & sDBName
        If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then
            If gbDebug Then MsgBox("mbGetUsersEx_SQL2008- Failed sql: " & s1)
            msLastSqlErrorMessage = "mbGetUsersEx_SQL2008- Failed sql: " & s1
        End If

        mbGetUsersEx_SQL2008 = False
        lResult = glExecSP(cnn1, "sp_helpuser", "", sErrors, rs1)
        If lResult <> 0 Then
            If gbDebug Then MsgBox(sErrors, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = msLastSqlErrorMessage & vbCrLf & sErrors
        Else '--  check rs1 for users.--
            colUserNames = New Collection
            '--check recordset--
            If Not (rs1 Is Nothing) Then
                '--If rs.RecordCount > 0 Then
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                While rs1.Read   '== Not rs1.EOF
                    If Not IsDBNull(rs1.Item("UserName")) Then
                        colUser1 = New Collection
                        colUser1.Add(CStr(rs1.Item("UserName")), "USERNAME")
                        If Not IsDBNull(rs1.Item("LoginName")) Then
                            colUser1.Add(CStr(rs1.Item("LoginName")), "LOGINNAME")
                        Else '--no login..-
                            colUser1.Add("null", "LOGINNAME")
                        End If '--login.-
                        '--SQL2008-- has "RoleName"-- column..--
                        On Error Resume Next
                        vGroup = rs1.Item("GroupName")
                        If (Err.Number <> 0) Then '--No group.. is 2008 or later..
                            On Error Resume Next
                            vGroup = rs1.Item("RoleName")
                            If (Err.Number <> 0) Then '--no column..-
                                vGroup = System.DBNull.Value '==colUser1.Add "null", "GROUPNAME"
                            Else '--role ok..--'--2008 or later..
                            End If '--rolename--
                        Else '--group ok..-
                        End If '-group..-
                        On Error GoTo 0
                        If Not IsDBNull(vGroup) Then
                            colUser1.Add(vGroup, "GROUPNAME")
                        Else '--null.. no group..-
                            colUser1.Add("null", "GROUPNAME")
                        End If '--Group.-

                        colUserNames.Add(colUser1) '--
                    End If
                    '==rs1.MoveNext()
                End While  '--read-
                rs1.Close()
            End If '--nothing..-
            mbGetUsersEx_SQL2008 = True
        End If
        rs1 = Nothing
    End Function '--get users..-
    '== = = = = = = = = = = =
    '-===FF->

    '--  sp_helpuser to get JobTrackin DB users..--
    '---  via gbGetUsers (cnn, dbname, colUsers)  ----
    '--- RETURNS Collection of User collections..-
    '---  Each User collection has items with keys: LOGINNAME ans USERNAME -
    '----    As per "sp_helpuser" recordset..--

    Public Function gbGetUsersEx(ByRef cnn1 As OleDbConnection, _
                                 ByVal sDBName As String, _
                                 ByRef colUserNames As Collection) As Boolean


        gbGetUsersEx = mbGetUsersEx_SQL2008(cnn1, sDBName, colUserNames)

        '=== Set rs1 = Nothing
    End Function '--get users..-
    '== = = = = = = = = = = =

    '--=3073.311= 11-Mar-2013=
    '-- check user access to DB..--
    '--  LoginName muse be valid login..
    '--   Return false if DB error..

    Public Function gbCheckUserAccess(ByRef cnnSql As OleDbConnection, _
                                    ByVal sDBName As String, _
                                  ByVal sLoginName As String, _
                                   ByRef bHasAccess As Boolean, _
                                    ByRef bIsDbOwner As Boolean) As Boolean
        Dim col1 As Collection
        Dim colUserNames As Collection

        bHasAccess = False
        bIsDbOwner = False
        gbCheckUserAccess = False
        If gbGetUsersEx(cnnSql, sDBName, colUserNames) Then
            gbCheckUserAccess = True
            For Each col1 In colUserNames
                If LCase(col1.Item("LoginName")) = LCase(sLoginName) Then '--login exists in DB, so check if has owner rights..-
                    If (LCase(col1.Item("UserName")) = LCase(sLoginName)) Then
                        '--login has same name as user in DB..--
                        bHasAccess = True '--don't need grantdbaccess.
                        '--  has security account.. Check if owner..
                        If (LCase(col1.Item("GroupName")) = "db_owner") Then
                            '--login has db-owner role.--
                            bIsDbOwner = True
                        End If  '--group-
                    End If  '--same name=
                End If '--login has alias in db..-
            Next col1 '--col1-
        End If '--get users.-
    End Function  '--gbCheckUserAccess-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '---  Upgrade column width..--
    '---  Upgrade column width..--
    '-----  VARCHAR columns only !!!  --

    Public Function gbExpandColumn(ByRef cnnSQL As OleDbConnection, _
                                    ByVal sTable As String, _
                                    ByVal sColumn As String, _
                                     ByVal lngNewWidth As Integer) As Boolean
        Dim sSql, s1 As String
        Dim sErrorMsg As String
        Dim lngaffected As Integer

        msLastSqlErrorMessage = ""
        gbExpandColumn = False
        sSql = "ALTER TABLE [" & sTable & "] ALTER COLUMN " & sColumn & _
                                                  " VARCHAR (" & CStr(lngNewWidth) & ") NOT NULL;  "
        If Not gbExecuteCmd(cnnSQL, sSql, lngaffected, sErrorMsg) Then
            s1 = "ERROR: Failed to expand Column '" & sColumn & _
                                     "' in Table '" & sTable & "' " & vbCrLf & sErrorMsg
            If gbDebug Then MsgBox(s1, MsgBoxStyle.Critical)
            msLastSqlErrorMessage = s1
        Else '--ok-
            If gbDebug Then MsgBox("OK.. Column '" & sColumn & "' in Table '" & sTable & _
                     "' " & vbCrLf & "  has been expanded to " & lngNewWidth & " chars.." & vbCrLf & _
                                "    --> " & lngaffected & " rows affected.", MsgBoxStyle.Information)
            gbExpandColumn = True
        End If
    End Function '--expand..--
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- 3.1.3101-- Check VWSS permissions..-
    '-- 3.1.3101-- Check VWSS permissions..-

    '== 3069==
    '==  For SQL SERVER 2005 +++..  To enable normal user to see all users with "sp_who"..
    '== A. IF WE are sqlAdmin THEN
    '==     AFTER user selecting DB-  
    '==         >> MAKE SURE all non-admin Users of this DB are GRANTED VIEW SERVER STATE permission..
    '== B. IF WE are NOT sqlAdmin THEN
    '==      AFTER user selecting DB-  
    '==         >> Check if current Users of this DB has VIEW SERVER STATE permission..
    '==              If not, advise operator to run JobMatix with Admin login to update perms..
    '==                      Then EXIT..
    '= = == = = = == = == = = = = == === == 

    Public Function gbCheckVWSSpermissions(ByRef cnnSQL As OleDbConnection, _
                                           ByVal bIsSqlAdmin As Boolean, _
                                            ByVal sSqlDbName As String, _
                                             ByVal sCurrentUserNT As String) As Boolean

        Dim sOrphanList, sList1, sMsg As String
        Dim sUserNameInDB, sUserLogin
        Dim col1, colUsers As Collection

        gbCheckVWSSpermissions = False
        If bIsSqlAdmin Then
            '--check/update all JobMatix user accounts in (DB " sDBNameJobs")  --
            '--  USER name may not have an actual Login  !!
            '--    (Can be orphaned due to RESTORE..)--
            Call gbLogMsg(gsRuntimeLogPath, "-- SqlAdmin: Checking user permissions for VWSS.. " & vbCrLf)

            If gbGetUsersEx(cnnSQL, sSqlDbName, colUsers) Then
                If (Not (colUsers Is Nothing)) AndAlso (colUsers.Count > 0) Then
                    sOrphanList = ""
                    sList1 = ""
                    sMsg = "NOTE: Some users in the Database: '" & sSqlDbName & "'" & vbCrLf & _
                            " need additional SQL permissions, and have now been upgraded.." & vbCrLf & _
                            "These are ( UserNames [LoginName] ): " & vbCrLf
                    '-- NEED master databse..--
                    '== frmSplash1.Labstatus.Text = "-- Setting MASTER database as current.."
                    If Not gbSetCurrentDatabase(cnnSQL, "master") Then
                        MsgBox("Couldn't set MASTER DB!", MsgBoxStyle.Exclamation)
                    End If
                    For Each col1 In colUsers
                        sUserLogin = col1.Item("LOGINNAME")
                        sUserNameInDB = col1.Item("USERNAME")
                        If (LCase(sUserNameInDB) <> "dbo") And (LCase(sUserNameInDB) <> "guest") And _
                                 (LCase(sUserNameInDB) <> "information_schema") And _
                                  (LCase(sUserNameInDB) <> "sys") And (LCase(sUserNameInDB) <> "sa") Then
                            If (LCase(sUserLogin) <> "null") And _
                                                  (LCase(sUserLogin) <> LCase(sCurrentUserNT)) Then  '--has login.. and NOT me.-
                                '--  Filter out Admin users..  They have permission anyway..-
                                '-- 3077- Don't filter..  CHECK ALL users--
                                '=3077= If Not mbTestSqlUser(cnnSQL, "SELECT IS_SRVROLEMEMBER ('sysadmin','" & sUserLogin & "'); ") Then
                                '--    -- 3077- all users--  (was:  NOT an admin user.) 
                                '-- ALL UERS-  Check if Login has VIEW SERVER STATE permission..-
                                If Not gbIsPermissionGranted(cnnSQL, sUserLogin, "VIEW SERVER STATE") Then
                                    '--Not prev. Granted. Must GRANT now..-
                                    If Not gbGrantVWSSPermission(cnnSQL, sUserLogin) Then
                                        MsgBox("ERROR: Startup permission check-" & vbCrLf & _
                                             "failed to GRANT VWSS to: " & sUserLogin & vbCrLf & _
                                            "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                                    Else  '--ok.
                                        sList1 = sList1 & sUserNameInDB & "  [" & sUserLogin & "]" & vbCrLf
                                    End If  '--vwss
                                End If  '--prev granted-
                                '=3077= End If  '--admin user.-
                            Else  '--orphan.-
                                sOrphanList = sOrphanList & sUserNameInDB & vbCrLf
                            End If  '--orphan.--
                        End If  '--dbo-
                    Next col1 '--col1-
                    If (sList1 <> "") Then
                        Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & sList1 & vbCrLf)
                        MsgBox(sMsg & vbCrLf & sList1, MsgBoxStyle.Information)
                    End If
                    '--show orphans, if any..-
                    If (sOrphanList <> "") Then
                        Call gbLogMsg(gsRuntimeLogPath, "-- Discovered orphaned users: " & vbCrLf & sOrphanList & vbCrLf)
                    End If  '--have orphans..-
                    gbCheckVWSSpermissions = True
                Else
                    MsgBox("ERROR: Startup permission check-" & vbCrLf & _
                              "No users found in DB: " & sSqlDbName & " !", MsgBoxStyle.Exclamation)
                    Exit Function  '--the end..-
                End If '--nothing..
            Else
                MsgBox("ERROR in Startup permission check-" & vbCrLf & _
                         "Failed to get users to show.." & vbCrLf & _
                              "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                Exit Function  '--the end..-
            End If
        Else  '--normal user.-
            If Not gbIsPermissionGranted(cnnSQL, sCurrentUserNT, "VIEW SERVER STATE") Then
                MsgBox("User permissions need to be upgraded.." & vbCrLf & _
                          "Start up JobMatix again as SQL Admin user to get users upgraded..", MsgBoxStyle.Exclamation)
                Exit Function  '--the end..-
            Else  '-ok-
                gbCheckVWSSpermissions = True
            End If
        End If  '-admin-

    End Function   '-gbCheckVWSSpermissions-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->



    '--  add Windows User Logon to SQL logins..-
    '--  add Windows User Logon to SQL logins..-

    '=-- 1. Create SQL Login if it doesn't exist..
    '=-- 2. Add security account to our DB. (gbGrantDBAccess)..
    '=-- 3. Add db_owner role to the security account in our DB. (gbAddRoleMember)..
    '=-- 4. Add VIEW SERVER STATE permission the new Login.. (for "who_using")..

    '==  Returns TRUE if login exists or was Granted OK..--

    Public Function gbAddNewUser(ByRef cnnSql As OleDbConnection, _
                                    ByVal sSqlDBName As String, _
                                     ByVal sLoginName As String, _
                                      ByRef sResult As String, _
                                      ByRef sErrorResult As String) As Boolean
        Dim bHasAccess, bIsDbOwner As Boolean

        gbAddNewUser = False
        sResult = ""
        sErrorResult = ""
        If Not gbExistsLogin(cnnSql, sLoginName) Then '--does not exist..
            '--no login yet..
            '====If Not gbAddLogin(mCnnSql, sLoginName, sPwd, msSqlDBName) Then
            If Not gbGrantLogin(cnnSql, sLoginName) Then
                sErrorResult = "'gbAddNewUser'- Failed to add new Login '" & sLoginName & "' .." & _
                                vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
                Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
                Exit Function
            Else '-Granted ok..-
                sResult = sResult & "== SQL Login: '" & sLoginName & "' has been granted OK..." & vbCrLf & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sResult & vbCrLf & "-- end INFO msg --" & vbCrLf)
            End If '--added..-
        Else
            If gbDebug Then MsgBox("The SQL login name '" & sLoginName & "' already exists.." & vbCrLf & _
                        "Will now check DB access rights..", MsgBoxStyle.Information)
            sResult = "The SQL login name '" & sLoginName & "' already exists.." & vbCrLf & _
                        "Now checking DB access rights.." & vbCrLf & vbCrLf
        End If '-doesn't exist..-
        bHasAccess = False
        bIsDbOwner = False
        '--
        '==3073.311= 11-Mar2013=
        If Not gbCheckUserAccess(cnnSql, sSqlDBName, sLoginName, bHasAccess, bIsDbOwner) Then
            sErrorResult = "'gbAddNewUser'- ERROR- Failed to get DB user list.." & _
                        vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
            Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
            If gbDebug Then MsgBox(sErrorResult, MsgBoxStyle.Exclamation)
            Exit Function
        End If '--check-
        '---If login doesn't exist IN DB, or exists but has no ownership in this DB..--
        '-- Must add user to DB before adding owner role..--
        If Not bHasAccess Then
            If Not gbGrantDBAccess(cnnSql, sSqlDBName, sLoginName) Then
                '= bOk = False
                sErrorResult = sErrorResult & _
                      "ERROR- Failed to GRANT access to user '" & sLoginName & "' .." & vbCrLf & _
                              vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
                Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
            Else '--granted ok-
                sResult = sResult & "== DB Access to '" & sSqlDBName & _
                                          "' Granted OK to: " & sLoginName & vbCrLf & vbCrLf
                Call gbLogMsg(gsErrorLogPath, sResult & vbCrLf & "-- end INFO msg --" & vbCrLf)
            End If '--grant..-
        Else  '--has access
            sResult = sResult & "== " & sLoginName & _
                         " Already has DB Access to '" & sSqlDBName & vbCrLf & vbCrLf
        End If '--has access..-
        If Not bIsDbOwner Then
            If Not gbAddRoleMember(cnnSql, sSqlDBName, sLoginName, "db_owner") Then
                sErrorResult = sErrorResult & _
                            "ERROR- Failed to add RoleMember (db_owner) to new user '" & sLoginName & _
                          "' .." & vbCrLf & vbCrLf & "Last error was: " & vbCrLf & msLastSqlErrorMessage
                Call gbLogMsg(gsErrorLogPath, sErrorResult & vbCrLf & "-- end error msg --" & vbCrLf)
            Else '--ok-
                sResult = sResult & _
                       "== 'DB_OWNER' Role was added OK for User '" & sLoginName & _
                                                "'  in DB: '" & sSqlDBName & "'.." & vbCrLf & vbCrLf
            End If '--add role..-
        Else  '-- HAS db_owner role..
            sResult = sResult & "== " & sLoginName & _
                          " Already is member of db_owner group in DB '" & sSqlDBName & vbCrLf & vbCrLf
        End If
        '== 3071== 
        '== SQL-2005 Plus- User NEEDS VIEW SERVER STATE permission..--
        If gbIsSqlServer2005Plus() Then
            If Not gbGrantVWSSPermission(cnnSql, sLoginName) Then
                sErrorResult = sErrorResult & _
                     "ERROR: Failed to GRANT VIEW SERVER STATE to: " & sLoginName & vbCrLf
            Else  '--ok.
                sResult = sResult & _
                   "== Granted 'VIEW SERVER STATE' permission OK for login '" & sLoginName & ".." & vbCrLf & vbCrLf
            End If  '--vwss-
        End If  '--2005-
        gbAddNewUser = True

    End Function  '-- gbAddNewUser--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Who Using this DB.. --
    '--  Who Using this DB.. --
    '-- call sp_who to get list of current users of DBname --
    '-- Rev-2912.. 21-Jul-2011==  ONLY worry about tasks that are "runnable"..-
    '-- Rev-3069.. 16-Oct-2011==  Worry about all tasks that are NOT "dormant"..-
    '--  ALSO-=3071=  
    '==     For SQL Server 2005 and later..  Use "sys.sysprocesses" system view..
    '==
    '--  NEW--=3103.0203=  
    '==     For SQL Server 2008 and later ONLY..  Use "sys.sysprocesses" system view..
    '==   NB: SQL Server 2005 with oledb has problem with "sys.sysprocesses" system view..
    '==          ("Protocol Error in TDS data stream" )
    '== = =

    Public Function gbWhoUsing(ByRef cnn1 As OleDbConnection, _
                              ByVal sDBName As String, _
                               ByRef colWhichUsers As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim sSql, s1, sErrors As String
        Dim sLoginName, sHostName, sStatus As String
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim colUser As Collection

        msLastSqlErrorMessage = ""
        '--USE--
        '== s1 = " USE master " '-- + sDBName
        '==3069=  If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then MsgBox("Failed sql: " & s1)
        gbWhoUsing = False
        If gbIsSqlServer2008Plus() Then  '=If gbIsSqlServer2005Plus() Then
            If gbIntJobMatixDBid() <= 0 Then Exit Function '-- NO DB ID was saved !!-
            sSql = "SELECT loginame, dbid, hostname, nt_domain, status " & vbCrLf & _
                   "FROM sys.sysprocesses; "
            If Not gbGetReader(cnn1, rs1, sSql) Then  '= gbGetRst(cnn1, rs1, sSql) Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                      "Error msg: " & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf
                If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = s1
            Else
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '== If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '= Not rs1.EOF
                            sStatus = ""
                            If (Not IsDBNull(rs1.Item("status"))) Then
                                sStatus = CStr(rs1.Item("status"))
                            End If
                            If (Not IsDBNull(rs1.Item("loginame"))) And (Not IsDBNull(rs1.Item("dbid"))) Then
                                '== colUser = New Collection '--each user is collection..-
                                sLoginName = CStr(rs1.Item("loginame"))
                                If (CInt(rs1.Item("dbid")) = gbIntJobMatixDBid()) And _
                                                                        (sStatus <> "dormant") Then  '--our DB--
                                    colUser = New Collection '--each user is collection..-
                                    colUser.Add(sLoginName, "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    colWhichUsers.Add(colUser)
                                End If
                            End If  '--null login.-
                            '== rs1.MoveNext()
                        End While  '-read-
                    End If  '--empty- 
                    rs1.Close()
                    gbWhoUsing = True
                End If  '--nothing..-
            End If  '--get rset-
        Else  '-sql server 20002005 --
            lResult = glExecSP(cnn1, "sp_who", "", sErrors, rs1)
            If lResult <> 0 Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                       "Error msg: " & vbCrLf & sErrors & vbCrLf
                If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                msLastSqlErrorMessage = s1
                '==MsgBox(sErrors, MsgBoxStyle.Critical)
            Else '--  check rs1 for users.--
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '= If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '== Not rs1.EOF
                            If Not IsDBNull(rs1.Item("dbname")) Then
                                '=3069.0= If (UCase(rs1.Fields("dbname").Value) = UCase(sDBName)) And _
                                '=3069.0=               (LCase(rs1.Fields("status").Value) = "runnable") Then '-- this one is using....--
                                If (UCase(rs1.Item("dbname")) = UCase(sDBName)) And _
                                           (LCase(rs1.Item("status")) <> "dormant") And _
                                                 (LCase(rs1.Item("status")) <> "background") Then '-- this one is using..--
                                    colUser = New Collection '--each user is collection..-
                                    '--MsgBox "Found user: " + rs1("LoginName")
                                    colUser.Add(CStr(rs1.Item("loginame")), "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    colWhichUsers.Add(colUser)
                                End If '--this db--
                            End If '--isnull-
                            '== rs1.MoveNext()
                        End While
                    End If  '--empty..-
                    rs1.Close()
                    gbWhoUsing = True
                End If '--nothing..-
            End If '--exec ok-
        End If  '--sql 2005--
        rs1 = Nothing
        colUser = Nothing
    End Function '--WhoUsing..-
    '= = = = = = = = = =

    '== End Module  Sql Subs..

    '== END of IMHERTTED SQL SUPPORT stuff.. --
    '== END of IMHERTTED SQL SUPPORT stuff.. --
    '== END of IMHERTTED SQL SUPPORT stuff.. --
    '== END of IMHERTTED SQL SUPPORT stuff.. --
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- SqlSchema stuff.

    '== START of IMHERTTED SQL SCHEMA stuff.. --
    '== START of IMHERTTED SQL SCHEMA stuff.. --
    '== START of IMHERTTED SQL SCHEMA stuff.. --
    '== START of IMHERTTED SQL SCHEMA stuff.. --

    '== Module sqlInfoSchema

    '---Get DB Schema (catalogue) Info for selected database---
    '---Get DB Schema (catalogue) Info for selected database---

    '---Get DB Schema (catalogue) Info for selected database---
    '---    -uses system stored procedures to build our 'catalogue'===
    '---- 11-June-2008--
    '----NOW uses sql-92 Standard INFORMATION_SCHEMA Views --

    '--= NB = For jobTracking Version-2...--
    '----- =26-Oct-2009=  SQL Schemas..  Retrieve and collect ForeignKey Constraint name.--
    '-------    SO THAT we can drop FKEY constarine by name if we need to..--
    '==
    '==  grh Build 3077.519= 19May2013==
    '==     >> Dropped 'gbGetJet351DbInfo' and 'gbGetJetDbInfo'  --
    '==           (See 'modJetLogin' for function 'gbGetJet360DbInfo'  --
    '==
    '==  grh JobMatixPOS Build 1001.225= 25Feb2014==
    '==     >> Dropped ADO..  now using ADO.net sql classes  --
    '==
    '==  grh JobMatixPOS Build 1001.328/330= 28Mar2014==
    '==     >> Add ISIDENTITY to Column properties.  --
    '==     >> Add COLUMN_DEFAULT value to Column properties.  --
    '==
    '==
    '==  JobMatix v3.1.3101.916---  16-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==      >> -- SQL SERVER ONLY --
    '==      >> -- SQL SERVER ONLY --
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==  
    '==  Combined Module.
    '==
    '==    3501.0806 06-August-2018= 
    '==       -- Speeding up getSchema at startup.-
    '==       -- Qualify all GetReaders with TableName..
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =



    '- --catalog/schema-- - - - -
    '--list of tables to be collected/exported--

    '--Each collecction item (Table)- is itself a collection of collections--
    '------------of stuff for that table--
    '--Each table collection has Key with value of "TABLENAME" and holds:
    '----= Item TABLENAME:  String containing tablename;--
    '----= Item PARENTS: Collection of parent table names.--(Parent shares primary key with self, --
    '-----                              which is a foreign key-column in self table..---
    '----= Item SOURCEINFO: Collection of strings with keys:
    '---                ----------- sourceDBFname, selectFields,selectCondition
    '--- = Item DBFFIELDLIST: Collection of field info collections from DBF header-
    '--  = Item FIELDS: Jet field collection for the table:
    '--     --- Each item (key=FieldName) is itself a collection of attributes for 1 fld:
    '--     ---   (NAME,TYPE, (sql)TYPE_NAME, DEFINEDSIZE, ISNULLABLE,ISPRIMARYKEY, ISFOREIGNKEY
    '----   ---                             [, FOREIGNTABLE,FOREIGNFIELD] (if Foreign Key) )---
    '--  = Item PRIMARYKEYS: collection of strings:  PRIMARY KEY fld-list for the table:
    '--     -----   Collection of (string) field names composing the PRIM-KEY.
    '--  = Item FOREIGNKEYS:  Collection of Foreign-key (relations) for the table:
    '--     ------   Each relation is a collection of strings WITH KEYS:
    '--      ------       (LOCALFIELD, FOREIGNTABLE,FOREIGNFIELD, FK_CONSTRAINT_NAME)
    '--- = Item OTHERINDEXES: Collection of OtherIndexes on table:
    '-----            Each item in this collection is an index, and is itself
    '-----               is a collection of string-names of columns composing the index-
    '------
    '----= Item NAMECOLUMN: String containing name of column in this table that --
    '-----               best represents the name of the row owner..
    '-----                  eg. customername for Customer Table..
    '= = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = =

    '= = = = = = = = = = = = = = =  '---- (built from sql system info)---

    '--SQL SERVER ONLY --
    '--SQL SERVER --
    '--SQL SERVER --

    '===  R E A D   T H I S  ===
    '===  R E A D   T H I S  ===
    '===  R E A D   T H I S  ===
    '===  R E A D   T H I S  ===
    '===  R E A D   T H I S  ===

    '===---ADOX used only for OLEDB Jet 4.0 (MS-Access 2000 db's)..--
    '===--- -HEY! OLEDB ADOX NOT to be used to get schema for Jet 3.51 (MS-Access '97 db's)..--
    '===------          KEY object does not return column info..---
    '===-----  -HEY! USE  DAO 3.51 to get schema info for Jet 3.51 (MS-Access '97 db's)..--


    '-NOTE on --getting Foreign Key info--
    '--- script courtesy of Ron Talmage--
    '--Microsoft SQL Server Professional and Pinnacle Publishing --
    '-----  http://msdn.microsoft.com/en-us/library/aa175805(SQL.80).aspx ==

    '--This query takes the initial information for the FK constraint from KEY_COLUMN_USAGE
    '----- and joins it with the primary key or uniqueness constraint on the referenced table.
    '-- Since the number of columns must be the same between the FK and the referenced key,
    '--- and their order must be the same, the query can join the second reference to
    '------KEY_COLUMN_USAGE on the ORDINAL_POSITION, as you can see in the last row.
    '---Now you have all the important information about a FK constraint that was missing
    '---- from the initial REFERENTIAL_CONSTRAINTS view.
    '-- By the way, in the preceding query, the 'FK_CONSTRAINT_NAME' style of
    '---- naming columns explicitly distinguishes the FK constraint information from that
    '---- of the referenced (primary key or uniqueness constraint) table.


    '---sSql=   "SELECT
    '---           KCU1.CONSTRAINT_NAME AS 'FK_CONSTRAINT_NAME'
    '---         , KCU1.TABLE_NAME AS 'FK_TABLE_NAME'
    '---         , KCU1.COLUMN_NAME AS 'FK_COLUMN_NAME'
    '---         , KCU1.ORDINAL_POSITION AS 'FK_ORDINAL_POSITION'
    '---         , KCU2.CONSTRAINT_NAME AS 'UQ_CONSTRAINT_NAME'
    '---         , KCU2.TABLE_NAME AS 'UQ_TABLE_NAME'
    '---         , KCU2.COLUMN_NAME AS 'UQ_COLUMN_NAME'
    '---         , KCU2.ORDINAL_POSITION AS 'UQ_ORDINAL_POSITION'
    '---      FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC
    '---      JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1
    '---      ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG
    '---         AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA
    '---         AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
    '---     JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2
    '---      ON KCU2.CONSTRAINT_CATALOG =
    '---      RC.UNIQUE_CONSTRAINT_CATALOG
    '---         AND KCU2.CONSTRAINT_SCHEMA =
    '---      RC.UNIQUE_CONSTRAINT_SCHEMA
    '---         AND KCU2.CONSTRAINT_NAME =
    '---      RC.UNIQUE_CONSTRAINT_NAME
    '---         AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION "

    '---

    '--Note: "USE" statement not permitted--
    '----Current DB must be already selected before calling.--

    '= 3101= Public Function gbGetSchemaInfo(ByRef cnnSQL As OleDbConnection, _
    '= 3101=                              ByRef sSqlDBName As String, _
    '= 3101=                              ByRef colSqlTables As Collection, _
    '= 3101=                              Optional ByVal bIsSqlServer As Boolean = False) As Boolean

    Public Function gbGetSqlSchemaInfo(ByRef cnnSQL As OleDbConnection, _
                                     ByVal sSqlDBName As String, _
                                     ByRef colSqlTables As Collection, _
                                     ByRef strLog As String) As Boolean

        Dim k, tx, isx, fx, px As Integer
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim msg, s1, s2 As String
        Dim s3, s4 As String
        Dim sSysType As String
        Dim sLastKey As String
        Dim sList, sTableName, sNameList As String
        Dim sErrorMsg, sSection, sErrors As String
        Dim sSql As String
        Dim colParents As Collection
        Dim colTblList As Collection
        Dim colTable As Collection
        Dim colFieldx As Collection '-- 1 field--
        Dim colFields As Collection
        Dim colTableInfo As Collection
        Dim colPrimaryKey As Collection
        Dim colForeignKeys As Collection
        Dim colOtherIndexes As Collection
        Dim v1, v2 As Object
        Dim rs As OleDbDataReader  '= ADODB.Recordset
        Dim dtColumns, dtIndexes As DataTable '= SqlDataReader
        Dim dtPrKeyCols, dtFnKeyCols As DataTable
        Dim row1 As DataRow
        '= Dim rsPrKeyCols As OleDbDataReader  '=  ADODB.Recordset
        '= Dim rsFnKeyCols As OleDbDataReader  '=  ADODB.Recordset
        Dim lResult As Integer
        Dim colUserTypes As Collection
        Dim sPrKeysSql, sColsSql, sColsSql2, sFnKeysSql As String
        Dim sIndexesSql, sNameCol As String
        Dim bIsSqlServer As Boolean = True

        gbGetSqlSchemaInfo = False
        '--get user-defined types--
        bOk = gbGetUserTypes(cnnSQL, sSqlDBName, colUserTypes)

        colTblList = New Collection
        '-----s1 = "USE " + sSqlDBname + vbCrLf
        '-----bOk = gbExecuteTrans(cnnSQL, s1)
        sList = ""
        '--get all tables--
        strLog = vbCrLf & "= = = = Sql Schema Info = = = = = = = " & vbCrLf & vbCrLf '--separator--
        strLog &= "Starting gbGetSchemaInfo:" & vbCrLf & " Reading sql  tables info for DB: " & sSqlDBName
        '==MsgBox("GetDBInfo: Reading sql  tables info for DB: " & sSqlDBName & "." & vbCrLf, MsgBoxStyle.Information)

        sSql = "SELECT table_name FROM INFORMATION_SCHEMA.TABLES " & " WHERE (table_type='BASE TABLE') "
        sSql &= " ORDER BY table_name;"
        If Not gbGetReader(cnnSQL, rs, sSql) Then
            MsgBox("Error in getting tables schema recordset.. " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        End If
        '--check recordset- get list of tables=-
        While Not (rs Is Nothing)
            '== If rs.BOF And (Not rs.EOF) Then rs.MoveFirst()
            While rs.Read  '= Not rs.EOF
                '--If UCase$(rs("table_type")) = "TABLE" Then
                '--Set colTable = New Collection
                s1 = rs.Item("TABLE_NAME")
                sList = sList & s1 & " "
                colTblList.Add(s1)
                '--End If
                '== rs.MoveNext()
            End While
            If Not rs.NextResult() Then Exit While '==rs = rs.NextRecordset
        End While '--nothing--
        If Not (rs Is Nothing) Then rs.Close()
        strLog &= "== GetDBInfo: .. DATABASE: " & sSqlDBName & vbCrLf & _
                           " - Found " & colTblList.Count() & " tables:  " & sList & vbCrLf & "- - - -"
        Call gbLogMsg(gsRuntimeLogPath, "== GetSqlSchemaInfo: .. DATABASE: " & sSqlDBName & vbCrLf & _
                          " - Found " & colTblList.Count() & " tables:  " & sList & vbCrLf & "- - - -")
        '--BUILD our catalogue for this database--
        '---first, get ALL column info for all tables (NOW INDIVIDUALLY)--
        sColsSql = "SELECT *, COLUMNPROPERTY(OBJECT_ID(ISC.Table_name), ISC.Column_name, 'ISIDENTITY') AS IsIdentity " & _
                          " FROM INFORMATION_SCHEMA.COLUMNS AS ISC"
        '=3501.0806= -- Qualify all GetReaders with TableName.. 
        '==                  (Name will be available when call is made below in Table Loop..)
        '= NO!  sColsSql &= " WHERE (ISC.Table_name= '$$tablename' ) "
        '-If Not gbGet Rst(cnnSQL, rsColumns, sSql) Then Exit Function '--msg was displayed..

        '---and get ALL PKEY column info for all tables--
        sPrKeysSql = "SELECT tc.table_name, kcu.constraint_name, kcu.column_name, " & _
                          "   tc.constraint_type, kcu.ordinal_position  " & _
                          " FROM INFORMATION_SCHEMA.key_column_usage AS kcu " & _
                          "   LEFT JOIN INFORMATION_SCHEMA.table_constraints AS tc " & _
                          "     ON (tc.constraint_name= kcu.constraint_name) " & _
                          "  WHERE (tc.constraint_type='PRIMARY KEY')  " & _
                          "  ORDER BY kcu.constraint_name,kcu.column_name,kcu.ordinal_position  "

        '--If Not gbGetRst(cnnSQL, rsPrKeyCols, sSql) Then Exit Function '--msg was displayed..

        '--AND get all Foreign Key cols---
        sFnKeysSql = "SELECT " & " KCU1.CONSTRAINT_NAME AS 'FK_CONSTRAINT_NAME', " & _
                          " KCU1.TABLE_NAME  AS 'FK_TABLE_NAME', " & _
                          " KCU1.COLUMN_NAME AS 'FK_COLUMN_NAME', " & _
                          " KCU1.ORDINAL_POSITION AS 'FK_ORDINAL_POSITION', " & _
                          " KCU2.CONSTRAINT_NAME  AS 'UQ_CONSTRAINT_NAME', " & _
                          " KCU2.TABLE_NAME  AS 'UQ_TABLE_NAME', " & _
                          " KCU2.COLUMN_NAME AS 'UQ_COLUMN_NAME', " & _
                          " KCU2.ORDINAL_POSITION AS 'UQ_ORDINAL_POSITION' " & _
                          " FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC " & _
                          "  JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 " & _
                          "    ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG " & _
                          "    AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA " & _
                          "    AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME " & _
                          "  JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 " & _
                          "    ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG " & _
                          "     AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA " & _
                          "     AND KCU2.CONSTRAINT_NAME =   RC.UNIQUE_CONSTRAINT_NAME " & _
                          "     AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION "

        '--If Not gbGetRst(cnnSQL, rsFnKeyCols, sSql) Then Exit Function '--msg was displayed..
        sIndexesSql = "SELECT "
        sIndexesSql &= " TableName = t.name, "
        sIndexesSql &= "      IndexName = ind.name, "
        sIndexesSql &= "      IndexId = ind.index_id, "
        sIndexesSql &= "      ColumnId = ic.index_column_id, "
        sIndexesSql &= "      ColumnName = col.name, "
        sIndexesSql &= "      ind.*, ic.*, col.* "
        sIndexesSql &= " FROM  sys.indexes ind "
        sIndexesSql &= " INNER JOIN "
        sIndexesSql &= "      sys.index_columns ic ON (ind.object_id = ic.object_id) and (ind.index_id = ic.index_id) "
        sIndexesSql &= " INNER JOIN "
        sIndexesSql &= "      sys.columns col ON (ic.object_id = col.object_id) and (ic.column_id = col.column_id) "
        sIndexesSql &= " INNER JOIN "
        sIndexesSql &= "      sys.tables t ON (ind.object_id = t.object_id) "
        '= sIndexesSql &= " WHERE (t.name='" & sTableName & "') "
        sIndexesSql &= "    WHERE (ind.is_primary_key = 0) "
        '== sIndexesSql &= "      AND ind.is_unique = 0 "
        '== sIndexesSql &= "      AND ind.is_unique_constraint = 0 "
        sIndexesSql &= "      AND t.is_ms_shipped = 0 "
        sIndexesSql &= " ORDER BY "
        sIndexesSql &= "      col.name, ind.name, ind.index_id, ic.index_column_id "

        '=3501.0807=  GET All columns for all tables..
        If Not gbGetDataTable(cnnSQL, dtColumns, sColsSql) Then
            s1 = "Error in getting columns recordset for all tables.. " & vbCrLf & _
                                           gsGetLastSqlErrorMessage()
            Call gbLogMsg(gsRuntimeLogPath, s1 & "= = = = =  ==" & vbCrLf)
            MsgBox(s1, MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        End If
        '=3501.0807=  GET All PKEYS for all tables..
        If Not gbGetDataTable(cnnSQL, dtPrKeyCols, sPrKeysSql) Then
            s1 = "Error in getting KEYS recordset for all table PKEYS.. " & vbCrLf & _
                                           gsGetLastSqlErrorMessage()
            Call gbLogMsg(gsRuntimeLogPath, s1 & "= = = = =  ==" & vbCrLf)
            MsgBox(s1, MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        End If
        '=3501.0807=  GET All FOREIGN KEYS for all tables..
        If Not gbGetDataTable(cnnSQL, dtFnKeyCols, sFnKeysSql) Then
            s1 = "Error in getting FKEY recordset for all tables.." & vbCrLf & _
                                            gsGetLastSqlErrorMessage()

            Call gbLogMsg(gsRuntimeLogPath, s1 & "= = = = =  ==" & vbCrLf)
            MsgBox(s1, MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        End If
        '== INDEXES --
        If Not gbGetDataTable(cnnSQL, dtIndexes, sIndexesSql) Then
            s1 = "Error in getting Indexes recordset for all tables.. " & sTableName & vbCrLf & _
                                           gsGetLastSqlErrorMessage()
            Call gbLogMsg(gsRuntimeLogPath, s1 & "= = = = =  ==" & vbCrLf)
            MsgBox(s1, MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        End If

        '--Now.. for each table..  build complete description of columns/keys--
        colSqlTables = New Collection
        If (colTblList.Count() > 0) Then
            '--If gbVerbose Then gAdvise vbCrLf + "=== Reading sql columns info for: "
            For tx = 1 To colTblList.Count()
                sTableName = colTblList.Item(tx)
                colTable = New Collection
                colTableInfo = New Collection
                colTable.Add(sTableName, "TABLENAME")
                '--colTableInfo.add sTableName, "TABLENAME"
                colFields = New Collection
                colPrimaryKey = New Collection
                colForeignKeys = New Collection
                colOtherIndexes = New Collection
                '--get all columns--

                '== Call gbLogMsg(gsRuntimeLogPath, "=== Reading sql COLUMN info for table: " & sTableName & "." & vbCrLf)
                '=3501.0806=  Insert TableName..
                '= NO !  sColsSql2 = Replace(sColsSql, "$$tablename", sTableName)
                '--refresh recordset--
                'If Not gbGetDataTable(cnnSQL, dtColumns, sColsSql2) Then
                '    MsgBox("Error in getting columns recordset for table: " & sTableName & vbCrLf & _
                '                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '    Exit Function '--msg was displayed..
                'End If
                ''--check recordset from the top for this table--
                '----- get column info=-
                sList = "Table " & sTableName & " has columns:" & vbCrLf
                sNameList = "" '--names this table--
                '==3501.0615-- strLog &= vbCrLf & "Reading Column info for table: " & sTableName
                If Not (dtColumns Is Nothing) Then
                    '== If Not rsColumns.BOF And (Not rsColumns.EOF) Then rsColumns.MoveFirst()
                    For Each row1 In dtColumns.Rows
                        If UCase(sTableName) = UCase(row1.Item("table_name")) Then '--this table-
                            colFieldx = New Collection
                            s1 = row1.Item("column_name")
                            colFieldx.Add(s1, "NAME")
                            sList = sList & s1 & ", "
                            sNameList = sNameList & UCase(s1) & "  "
                            '--colFieldx.add rsColumns("data_type"), "TYPE"
                            L1 = 0
                            If Not IsDBNull(row1.Item("character_octet_length")) Then
                                L1 = row1.Item("character_octet_length")
                            End If
                            colFieldx.Add(L1, "DEFINEDSIZE")
                            L1 = 0
                            If Not IsDBNull(row1.Item("character_maximum_length")) Then
                                L1 = row1.Item("character_maximum_length")
                            End If
                            colFieldx.Add(L1, "CHAR_MAX_LENGTH")

                            sSysType = row1.Item("data_type") '--"system" data type !!---
                            colFieldx.Add(sSysType, "TYPE_NAME") '--use system type as sql type--

                            k = giGetADOdataType(sSysType)
                            '-- !!  CHECK if systype is sql-92 type !!!!!!  ===
                            colFieldx.Add(k, "TYPE") '--ADO data type--
                            If (UCase(row1.Item("is_nullable")) <> "YES") Then '--smallint ---
                                colFieldx.Add(False, "ISNULLABLE") '--null not allowed in this column--
                            Else
                                colFieldx.Add(True, "ISNULLABLE") '--null IS allowed in this column--
                            End If
                            '-- save DEFAULT value--
                            s2 = ""
                            If Not IsDBNull(row1.Item("COLUMN_DEFAULT")) Then
                                s2 = row1.Item("COLUMN_DEFAULT")
                            End If
                            colFieldx.Add(s2, "COLUMN_DEFAULT") '--changed below if true--

                            colFieldx.Add(False, "ISPRIMARYKEY") '--changed below if true--
                            colFieldx.Add(False, "ISFOREIGNKEY") '--changed below if true--
                            '----colFieldx.add False, "ISFOREIGNKEY"    '--assume not foreign key for now--

                            If (row1.Item("isIdentity") > 0) Then
                                colFieldx.Add(True, "ISIDENTITY") '--IS identity..---
                            Else
                                colFieldx.Add(False, "ISIDENTITY") '--not identity col.
                            End If
                            colFields.Add(colFieldx, UCase(s1)) '--key is fldName--
                            '--debug--
                            '--Set colFieldx = colFields(UCase$(s1))
                            '--v1 = colFieldx("TYPE")
                            '--v2 = colFieldx("DEFINEDSIZE")
                        End If '--this table-

                    Next  '--row1.-
                    '==While rsColumns.Read   '== Not rsColumns.EOF
                    '== '== rsColumns.MoveNext()
                    '== End While
                    '==If Not rsColumns.NextResult() Then Exit While '=  rsColumns = rsColumns.NextRecordset
                End If  '=While '--nothing--
                '==If Not (dtColumns Is Nothing) Then dtColumns.Close()
                '--gLogMsg sList + "==" + vbCrLf  '--show cols this table--
                '--DEBUG--
                '--Set colFieldx = colFields("CODENAME")
                '--sList = colFieldx("NAME") + " (" + CStr(colFieldx.count) + " itms) "
                '--sList = sList + "  (size " + CStr(colFieldx("DEFINEDSIZE")) + ") "
                '--sList = sList + "  (type:" + CStr(colFieldx.item("TYPE")) + ") "
                '--DEBUG--

                '--get primary key cols--
                '==3501.0615-- strLog &= "Reading sql primary key info for table: " & sTableName
                '== Call gbLogMsg(gsRuntimeLogPath, "=== Reading sql PRIMARY KEY info for table: " & sTableName & "." & vbCrLf)
                '--get recordset--
                'If Not gbGetReader(cnnSQL, rsPrKeyCols, sPrKeysSql) Then
                '    MsgBox("Error in getting keys recordset for table: " & sTableName & vbCrLf & _
                '                                    gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '    Exit Function '--msg was displayed..
                'End If
                sList = "  Primary key cols : "
                If Not (dtPrKeyCols Is Nothing) Then
                    '== If rsPrKeyCols.BOF And (Not rsPrKeyCols.EOF) Then rsPrKeyCols.MoveFirst()
                    '=While rsPrKeyCols.Read  '== Not rsPrKeyCols.EOF
                    For Each row1 In dtPrKeyCols.Rows
                        If UCase(row1.Item("table_name")) = UCase(sTableName) Then '--this table-
                            s1 = row1.Item("column_name")
                            colPrimaryKey.Add(s1)
                            colFields.Item(UCase(s1)).Remove("ISPRIMARYKEY") '--remove/add to change--
                            colFields.Item(UCase(s1)).Add(True, "ISPRIMARYKEY") '--remove/add to change--
                            sList = sList & s1 & " (" + row1.Item("constraint_name") & ")  "
                        End If '--this table-
                    Next row1
                    '== rsPrKeyCols.MoveNext()
                    '=End While '--eof-
                    '= If Not rsPrKeyCols.NextResult() Then Exit While '== rsPrKeyCols = rsPrKeyCols.NextRecordset
                End If  '--nothing--
                '= If Not (rsPrKeyCols Is Nothing) Then rsPrKeyCols.Close()
                '--gLogMsg sList + vbCrLf
                '--get foreign keys--
                '----Set colForeignKeys = New Collection
                '==3501.0615-- strLog &= "Reading sql Foreign key info for table: " & sTableName
                '= Call gbLogMsg(gsRuntimeLogPath, "=== Reading sql Foreign key info for table: " & sTableName & "." & vbCrLf)
                sList = "  FOREIGN key cols : " & vbCrLf
                '--Get foreign keys for current table--
                '--Note: we only use 1st col of each key..
                '======  multiple col.support still to come ..!!--
                '--refresh recordset--
                'If Not gbGetReader(cnnSQL, rsFnKeyCols, sFnKeysSql) Then
                '    MsgBox("Error in getting FKEY recordset for table: " & sTableName & vbCrLf & _
                '                                    gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '    Exit Function '--msg was displayed..
                'End If
                sLastKey = ""
                If Not (dtFnKeyCols Is Nothing) Then
                    '== If rsFnKeyCols.BOF And (Not rsFnKeyCols.EOF) Then rsFnKeyCols.MoveFirst()
                    '=While rsFnKeyCols.Read  '== Not rsFnKeyCols.EOF
                    For Each row1 In dtFnKeyCols.Rows
                        If UCase(row1.Item("fk_table_name")) = UCase(sTableName) Then '--this table-
                            s4 = row1.Item("fk_constraint_name")
                            If (s4 <> sLastKey) Then '--start new constraint--use 1st col-
                                sLastKey = s4 '--ignores subseq cols this constraint-
                                s1 = row1.Item("fk_column_name")
                                s2 = row1.Item("uq_table_name")
                                s3 = row1.Item("uq_column_name")
                                s4 = row1.Item("FK_CONSTRAINT_NAME")
                                sList = sList & s1 & " (REF " & s2 & ", " & s3 & ") " & vbCrLf
                                '--add to key collection-
                                colFieldx = New Collection
                                colFieldx.Add(s1, "LOCALFIELD")
                                colFieldx.Add(s2, "FOREIGNTABLE")
                                colFieldx.Add(s3, "FOREIGNFIELD")
                                colFieldx.Add(s4, "FK_CONSTRAINT_NAME")
                                colForeignKeys.Add(colFieldx) '--add to fkey collection this table--
                                '--flag (xref) this as foreign key in relevant col. of current table--
                                '-change flag, add fkey ref in f/key column--
                                colFields.Item(UCase(s1)).Remove("ISFOREIGNKEY") '--remove/add to change--
                                colFields.Item(UCase(s1)).Add(True, "ISFOREIGNKEY") '--remove/add to change--
                                colFields.Item(UCase(s1)).Add(s2, "FOREIGNTABLE")
                                colFields.Item(UCase(s1)).Add(s3, "FOREIGNFIELD")
                            End If '--new--
                        End If '--this table--
                    Next row1
                    '== rsFnKeyCols.MoveNext() '--next fkey row--
                    '=End While '--eof--
                    '= If Not rsFnKeyCols.NextResult() Then Exit While '=  rsFnKeyCols = rsFnKeyCols.NextRecordset
                End If '--nothing--
                '= If Not (rsFnKeyCols Is Nothing) Then rsFnKeyCols.Close()
                '==3501.0615-- strLog &= sList

                '--get OtherIndexes--
                '-- !!  ONLY for sqlServer ---- AND only 2005Plus --
                If bIsSqlServer AndAlso gbIsSqlServer2005Plus() Then '--can use sys.indexes-
                    colFieldx = Nothing
                    '==3501.0615-- strLog &= "Reading Other-index info for table: " & sTableName
                    '= Call gbLogMsg(gsRuntimeLogPath, "=== Reading sql OTHER-INDEX info for table: " & sTableName & "." & vbCrLf)
                    '==  JobMatix POS3---  10-Sep-2014 ===
                    '-- can't use "sp_statistics" -- min OleDb connection..
                    '- indexes courtesy: 
                    '=  http://stackoverflow.com/questions/765867/list-of-all-index-index-columns-in-sql-server-db

                    'sIndexesSql = "SELECT "
                    'sIndexesSql &= " TableName = t.name, "
                    'sIndexesSql &= "      IndexName = ind.name, "
                    'sIndexesSql &= "      IndexId = ind.index_id, "
                    'sIndexesSql &= "      ColumnId = ic.index_column_id, "
                    'sIndexesSql &= "      ColumnName = col.name, "
                    'sIndexesSql &= "      ind.*, ic.*, col.* "
                    'sIndexesSql &= " FROM  sys.indexes ind "
                    'sIndexesSql &= " INNER JOIN "
                    'sIndexesSql &= "      sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id "
                    'sIndexesSql &= " INNER JOIN "
                    'sIndexesSql &= "      sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id "
                    'sIndexesSql &= " INNER JOIN "
                    'sIndexesSql &= "      sys.tables t ON ind.object_id = t.object_id "
                    'sIndexesSql &= " WHERE (t.name='" & sTableName & "') "
                    'sIndexesSql &= "      AND ind.is_primary_key = 0 "
                    ''== sIndexesSql &= "      AND ind.is_unique = 0 "
                    ''== sIndexesSql &= "      AND ind.is_unique_constraint = 0 "
                    'sIndexesSql &= "      AND t.is_ms_shipped = 0 "
                    'sIndexesSql &= " ORDER BY "
                    'sIndexesSql &= "      col.name, ind.name, ind.index_id, ic.index_column_id "
                    '-- get table-
                    sList = " ==  Other Indexes : "
                    'If Not gbGetDataTable(cnnSQL, dtIndexes, sIndexesSql) Then
                    '    MsgBox("Error in getting Indexes recordset for table: " & sTableName & vbCrLf & _
                    '                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    '    Exit Function '--msg was displayed..
                    'End If
                    If (Not (dtIndexes Is Nothing)) AndAlso (dtIndexes.Rows.Count > 0) Then
                        For Each row1 In dtIndexes.Rows
                            If UCase(sTableName) = UCase(row1.Item("TableName")) Then '--this table-
                                colFieldx = New Collection
                                s2 = row1.Item("IndexName")
                                s3 = row1.Item("ColumnName")
                                sList &= vbCrLf & "   Index " & s2 & " on: " & s3
                                colFieldx.Add(s3)
                                colOtherIndexes.Add(colFieldx)
                            End If  '-table-
                        Next row1
                    Else
                        '==3501.0615-- strLog &= "No Other-index found for table: " & sTableName
                    End If  '--nothing-
                End If '--sql server--

                '-- set up mainParent for Assoc tables--
                '--  ie where primary key is also foreign key (dependent)-
                '--The first foreign key/Promary key point to the parent table--
                sList = "Parent Table(s) are: "
                colParents = New Collection
                If (colPrimaryKey.Count() > 0) And (colForeignKeys.Count() > 0) Then
                    For px = 1 To colPrimaryKey.Count() '--check all primary key cols--
                        s1 = UCase(colPrimaryKey.Item(px))
                        For fx = 1 To colForeignKeys.Count()
                            colFieldx = colForeignKeys.Item(fx)
                            If UCase(colFieldx.Item("LOCALFIELD")) = s1 Then
                                s2 = colFieldx.Item("FOREIGNTABLE") '--another parent for our table-
                                colParents.Add(s2) '---add parent to coll.-
                                sList = sList & s2 & "; "
                                Exit For '--inner-fx--(First parent is enough)-
                            End If
                        Next fx
                    Next px '--next primary key col--
                End If
                '--If (colParents.count > 0) Then Call gLogMsg(sList)
                '-----If colFieldx.count > 0 Then colOtherIndexes.add colFieldx   '--save last index-
                '--search fld list for best name column..--
                sNameCol = ""
                For Each colFieldx In colFields
                    If gbIsText(colFieldx.Item("TYPE_NAME")) Then
                        s1 = colFieldx.Item("NAME") '--get this column name..-
                        L1 = CLng(colFieldx.Item("CHAR_MAX_LENGTH")) '--get this column text size-
                        If (InStr(UCase(s1), "COMPANY") > 0) Then
                            sNameCol = s1
                        ElseIf (InStr(UCase(s1), "SURNAME") > 0) Then
                            sNameCol = s1
                        ElseIf (InStr(UCase(s1), "NAME") > 0) Then
                            sNameCol = s1
                        ElseIf (InStr(UCase(s1), "DESCR") > 0) Then
                            sNameCol = s1
                        End If '--instr=
                    End If '--text fld-
                    If (sNameCol <> "") Then Exit For '--found something..-
                Next colFieldx '--flds-
                If (sNameCol = "") Then  '--nothing found..
                    '--retry with text size..
                    '--  fudge to pick up supplier name-
                    For Each colFieldx In colFields
                        If gbIsText(colFieldx.Item("TYPE_NAME")) Then
                            s1 = colFieldx.Item("NAME") '--get this column name..-
                            L1 = CLng(colFieldx.Item("CHAR_MAX_LENGTH")) '--get this column text size-
                            If (L1 > 24) Then '--take it..--
                                sNameCol = s1
                                Exit For
                            End If
                        End If
                    Next colFieldx '--flds-
                End If
                '--save stuff for this table--
                colTable.Add(colTableInfo, "SOURCEINFO")
                colTable.Add(colParents, "PARENTS")
                colTable.Add(colFields, "FIELDS")
                colTable.Add(colPrimaryKey, "PRIMARYKEYS") '-- #3--
                '--4th entry is FOREIGN KEY collection--
                colTable.Add(colForeignKeys, "FOREIGNKEYS") '-- #4--
                colTable.Add(colOtherIndexes, "OTHERINDEXES") '-- #5--
                colTable.Add(sNameCol, "NAMECOLUMN") '--#6--

                colSqlTables.Add(colTable, UCase(sTableName)) '--save all stuff this table..--
            Next tx '--table--
            gbGetSqlSchemaInfo = True

            strLog &= "= gbGetSchemaInfo== Finished building catalogue for: " & sSqlDBName & " == " & vbCrLf & _
                            "= = = = = = = = =  = = = =" & vbCrLf
        Else
            '--MsgBox "No tables visible in DB: " + sSqlDBname + "  for current user..", vbExclamation
            strLog &= "= gbGetSchemaInfo= No tables visible in DB: " & sSqlDBName & "  for current user.." & vbCrLf
        End If '--table count--

        rs = Nothing
        dtColumns = Nothing
        dtPrKeyCols = Nothing
        dtFnKeyCols = Nothing
    End Function '--getInfo--
    '= = = = = = = = = = = = = = =


    '-- JobMatixPOS-   NO MORE JET --
    '-- JobMatixPOS-   NO MORE JET --
    '-- JobMatixPOS-   NO MORE JET --
    '-- JobMatixPOS-   NO MORE JET --

    '== END of IMHERTTED SQL SCHEMA stuff.. --
    '== END of IMHERTTED SQL SCHEMA stuff.. --
    '== END of IMHERTTED SQL SCHEMA stuff.. --
    '== END of IMHERTTED SQL SCHEMA stuff.. --

    '= = =  the end =  =
    '= = = = end of module==
    '= End Module  == Sql Schema..



End Module  '-modAllFileAndSqlSubs-
'= = = = = = = = = = = = = = = == =

'== the end ==
