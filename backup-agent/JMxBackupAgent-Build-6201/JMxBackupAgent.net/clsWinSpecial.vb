
Option Strict Off
Option Explicit On
Imports System
Imports System.IO
Imports System.Collections
Imports System.Data.Sql
Imports System.Text
Imports VB = Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.Application
Imports Microsoft.Win32.SafeHandles

Public Class clsWinSpecial

    '-- class to Return Win Version 
    '==   and Win Special Folder Paths..

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


    '= grh Jobmatix 3107.803.. 03-Aug-2015==
    '==   Ripped out of Install (Setup.vb) programs==

    Const VER_NT_WORKSTATION As Integer = 1
    Const VER_NT_SERVER As Integer = 3
    Const VER_NT_DOMAIN_CONTROLLER As Integer = 2

    '-- Using the OSVERSIONINFO struct and corresponding signature:
    '-- Using the OSVERSIONINFO struct and corresponding signature:

    '--  OSVERSIONINFOEX  --

    '-- typedef struct _OSVERSIONINFOEX {
    '--   DWORD dwOSVersionInfoSize;
    '--   DWORD dwMajorVersion;
    '--   DWORD dwMinorVersion;
    '--   DWORD dwBuildNumber;
    '--   DWORD dwPlatformId;
    '--   TCHAR szCSDVersion[128];
    '--   WORD  wServicePackMajor;
    '--   WORD  wServicePackMinor;
    '--   WORD  wSuiteMask;
    '--   BYTE  wProductType;
    '--   BYTE  wReserved;
    '-- } OSVERSIONINFOEX, *POSVERSIONINFOEX, *LPOSVERSIONINFOEX;

    '-- VB Version..--
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure OSVersionInfoEx
        Public dwOSVersionInfoSize As Integer
        Public dwMajorVersion As Integer
        Public dwMinorVersion As Integer
        Public dwBuildNumber As Integer
        Public dwPlatformId As Integer
        '--  …
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=128)> _
        Public versionString As String
        '--
        Public wServicePackMajor As Short
        Public wServicePackMinor As Short
        Public wSuiteMask As Short
        Public wProductType As Byte
        Public wReserved As Byte
    End Structure '-- OSVersionInfoEx--


    '--  This must be used if OSVERSIONINFO is defined as a struct

    '- GetVersionEx --

    <DllImport("kernel32")> _
    Private Shared Function GetVersionEx(ByRef osvi As OSVersionInfoEx) As Boolean
    End Function
    '= = = = = = = = = =
    '-===FF->

    '-- VB.NET definitions..--
    '-- VB.NET definitions..--

    '-- IS ADMIN --
    '-- IsUserAnAdmin --
    <DllImport("shell32.dll", EntryPoint:="IsUserAnAdmin")> _
    Public Shared Function IsUserAnAdmin() As Boolean
    End Function


    '-  THANKS to Pinvoke.net

    '-- GetModuleHandle --
    '-- GetModuleHandle --

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function GetModuleHandle(ByVal lpModuleName As String) As IntPtr
    End Function


    '-- GetProcAddress --
    '-- GetProcAddress --

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True)> _
    Public Shared Function GetProcAddress(ByVal hModule As IntPtr, ByVal procName As String) As UIntPtr
    End Function

    '-- Notes: 
    '-- GetProcAddress only comes in an ANSI flavor, 
    '-- hence we help the runtime by telling it to always use ANSI when marshalling the 
    '-- string parameter. We also prevent the runtime looking for a non-existent GetProcAddressA, 
    '-- because the default for C# is to set ExactSpelling to false.

    '-- GetCurrentProcess -
    '-- GetCurrentProcess -
    '--     C# Signature:
    '--     [DllImport("kernel32.dll")]
    '--     static extern IntPtr GetCurrentProcess();
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function GetCurrentProcess() As safewaithandle '==IntPtr
    End Function
    '= = = = = = = = = =
    '-===FF->


    '-- IsWow64Process --
    '-- IsWow64Process --

    '-- Determines whether the specified process is running under WOW64. 
    '-- Some WinAPI functions work differently when running through 
    '-- WOW64, so you will sometimes need to know if a process is under the thunking layer.

    <DllImport("Kernel32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)> _
    Public Shared Function IsWow64Process( _
       ByVal hProcess As Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid, _
       ByRef wow64Process As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean

    End Function
    '= = = = = = = = = = = = = = = = = = = =  = = = = = 
    '-===FF->

    '--  Get Folder Paths..-
    '-- Get Folder Path stuff..---
    '-- Get Folder Path stuff..---
    '-- Get Folder Path stuff..---

    '--SHGetFolderPath--  For OS PRIOR to V6.0 (Vista)..
    '--- ShlObj.h --

    Private Const CSIDL_WINDOWS As Int32 = &H24    '-- GetWindowsDirectory()
    Private Const CSIDL_SYSTEM As Int32 = &H25    '--// GetSystemDirectory()
    Private Const CSIDL_PROGRAM_FILES As Int32 = &H26    '--  C:\Program Files
    Private Const CSIDL_MYPICTURES As Int32 = &H27    '--  C:\Program Files\My Pictures
    Private Const CSIDL_PROFILE As Int32 = &H28    '--  USERPROFILE
    Private Const CSIDL_SYSTEMX86 As Int32 = &H29    '--  x86 system directory on RISC
    Private Const CSIDL_PROGRAM_FILESX86 As Int32 = &H2A    '--  x86 C:\Program Files on RISC
    Private Const CSIDL_PROGRAM_FILES_COMMON As Int32 = &H2B    '--  C:\Program Files\Common
    Private Const CSIDL_PROGRAM_FILES_COMMONX86 As Int32 = &H2C    '--  x86 Program Files\Common on RISC

    '==3107.802=
    '== #define CSIDL_COMMON_APPDATA            0x0023        // All Users\Application Data
    Private Const CSIDL_COMMON_APPDATA As Int32 = &H23  '=  0x0023  // All Users\Application Data

    <DllImport("shell32.dll")> _
    Private Shared Function SHGetFolderPath(ByVal hwndOwner As IntPtr, _
                                               ByVal nFolder As Int32, ByVal hToken As IntPtr, _
                                                ByVal dwFlags As Int32, _
                                                ByVal pszPath As StringBuilder) As Int32
    End Function '--SHGetFolderPath-- 
    '= = = = = =  = = = =

    '--SHGetKnownFolderPath--  For OS V6.0 (Vista) and later..

    Private FOLDERID_Windows As New Guid("{F38BF404-1D43-42F2-9305-67DE0B28FC23}")
    Private FOLDERID_System As New Guid("{1AC14E77-02E7-4E5D-B744-2EB1AE5198B7}") '--System32.-

    Private FOLDERID_ProgramFiles As New Guid("{905e63b6-c1bf-494e-b29c-65b732d3d21a}")
    Private FOLDERID_ProgramFilesX86 As New Guid("{7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E}")
    '==3107.802=
    Private FOLDERID_ProgramData As New Guid("{62AB5D82-FDC1-4DC3-A9DD-070D1D495D97}")


    <DllImport("shell32.dll")> _
    Private Shared Function SHGetKnownFolderPath(<MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid, _
                                      ByVal dwFlags As UInt32, _
                                       ByVal hToken As IntPtr, _
                                         <MarshalAs(UnmanagedType.LPWStr)> ByRef pszPath As StringBuilder) As Int32
    End Function '--SHGetKnownFolderPath-- 
    '= = = = = = =  == = 
    '= = = = = = = = = =
    '-===FF->

    '= = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = =
  
    '== Private Const k_SFX_EXTRACT_PATH As String = "c:\zzzz-JobMatix3-xFiles"
    '-  WE use the WinRar SFX option "TempMode"-
    '--         which sets the setup working dir. to the temp dir..
    '== Const K_SAVESETTINGSPATH As String = "localJobSettings.txt"

    '--  Static vars..--
    Private mbActivated As Boolean = False
    Private mbIsWow64Process As Boolean = False
    Private mbIsAdminUser As Boolean = False

    Private mOSVersion As System.OperatingSystem  '== = Environment.OSVersion
    Private mOSvi2 As OSVersionInfoEx

    '== Private mColSqlServers As Collection
    '== Private mSdSettings As clsStrDictionary   '==  Scripting.Dictionary
    '== Private msJobMatixAppPath As String = ""
    '== Private folderBrowserDialog1 As FolderBrowserDialog

    '-- for new class-

    '-  mOSvi2 As OSVersionInfoEx
    Private msWindowsName As String = ""  '=  "Windows 2000" -
    '--   mOSvi2.versionString  -
    Private msOS_ServicePack As String = ""

    '- mOSVersion As System.OperatingSystem 
    Private mOSVersion_string As String = ""  '--includes Service Pack.-
    '--  eg: Microsoft Windows NT 5.1.2600.0 Service Pack 1
    Private msWindowsDir As String = ""
    Private msSystemDir As String = ""  '-Windows system..-

    '= labOS.Text = sVersionName & " - " & mOSvi2.versionString
    '= labOS.Text = labOS.Text & vbCrLf & " (" & mOSVersion.VersionString & ")"   '--includes Service Pack.-

    Private msProgramDir As String
    Private msAppDataDir As String

    '= = = = = = = = = = = =  = = = === 
    '= = = = = = = =  = = = =  = = = =
    '-===FF->

    '-- Notes:
    '-- "IsWow64Process" Requires Windows XP SP2, Windows Vista, Windows Server 2003 SP1 or Windows Server 2008
    '-- Use this instead of the processor architecture to determine if you are running on 64 bit.  
    '-- You can use GetProcAddess to determine if your OS supports it (XP SP2 or greater).

    '--  FROM MSDN  --
    '--   http://msdn.microsoft.com/en-us/library/ms684139(v=vs.85).aspx

    '-- For compatibility with operating systems that do not support this function, 
    '--   call GetProcAddress to detect whether IsWow64Process 
    '--    is implemented in Kernel32.dll. If GetProcAddress succeeds, it is safe to call this function. 
    '-- Otherwise, WOW64 is not present. 
    '-- Note that this technique is not a reliable way to detect whether the operating system 
    '--   is a 64-bit version of Windows because the 
    '--   Kernel32.dll in current versions of 32-bit Windows also contains this function.


    '--  SAMPLE --
    '-  Checks if the function exists on this OS, then calls it.
    Private Function IsWow64() As Boolean

        Dim proc As Integer
        proc = GetProcAddress(GetModuleHandle("Kernel32.dll"), "IsWow64Process")

        If proc <= 0 Then
            '=txtSystemInfo.Text = txtSystemInfo.Text & "NO Dll entry point found for 'IsWow64Process'" & vbCrLf
            MsgBox("NO Dll entry point found for 'IsWow64Process'", MsgBoxStyle.Exclamation)
            Return False
        End If
        'Dim processHandle As Long = GetProcessHandle(System.Diagnostics.Process.GetCurrentProcess().Id)
        Dim retVal As Boolean
        If IsWow64Process(GetCurrentProcess(), retVal) Then
            IsWow64 = retVal  '= Return retVal
        Else
            MsgBox("Error in call to 'IsWow64Process' system function. ", MsgBoxStyle.Exclamation)
            IsWow64 = False  '= Return False
        End If
    End Function  '--IsWow..
    '= = = = = = = = = = = = = = = = = 

    '-- Sample Code (VB.NET):
    '-- VB.NET:
    Public Function GetKnownFolderPath(ByVal folderGuid As Guid) As String
        Dim myPath As New StringBuilder(300)
        Dim retVal As String = String.Empty
        If SHGetKnownFolderPath(folderGuid, 0, Nothing, myPath) <> 0 Then
            Throw New ApplicationException("Can't get specified directory.")
        End If
        retVal = myPath.ToString
        Return retVal
    End Function
    '= = = = = = = = 
    '-- END SAMPLE --
    '= = = = = = = = = =
    '-===FF->


    '-Public properties.  Get System Information-..-
    '-Public properties. Get System Information-..-

    ReadOnly Property IsProcessWow64 As Boolean
        Get
            IsProcessWow64 = mbIsWow64Process
        End Get
    End Property  '-woe64-
    '= = = = = = = = = = = = = == == 

    ReadOnly Property IsAdminUser As Boolean
        Get
            IsAdminUser = mbIsAdminUser
        End Get
    End Property  '-wow64-
    '= = = = = = = = = = = = = == == 

    '-windows path-
    ReadOnly Property WindowsDir As String
        Get
            WindowsDir = msWindowsDir
        End Get
    End Property
    '= = = = = = = = = = = = = = =

    '-- Win System Path-
    ReadOnly Property SystemDir As String
        Get
            SystemDir = msSystemDir
        End Get
    End Property
    '= = = = = = = = = = = = = = =

    '-- prog path-
    ReadOnly Property ProgramDir As String
        Get
            ProgramDir = msProgramDir
        End Get
    End Property
    '= = = = = = = = = = = = = = =

    '-- App data path-
    ReadOnly Property AppDataDir As String
        Get
            AppDataDir = msAppDataDir
        End Get
    End Property
    '= = = = = = = = = = = = = = =

    '-- Win version-
    ReadOnly Property WindowsVersion As String
        Get
            WindowsVersion = msWindowsName & " " & msOS_ServicePack & vbCrLf & _
                                                                    mOSVersion_string
        End Get
    End Property
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-Public methods..-
    '-Public methods..-
    '-Public methods..-

    Public Sub New()
        Dim sVersionName As String  '--from mOSvi2 -eg "Windows 2000." -
        Dim ix As Integer
        Dim intCSIDL_program, intCSIDL_appdata As Int32
        Dim guidFOLDERID_program, guidFOLDERID_appData As Guid

        '-- set up system info for client queries..
        '-- set up system info for client queries..

        mbIsWow64Process = IsWow64()
        mbIsAdminUser = IsUserAnAdmin()

        '--  we are a 32-bit program..
        '--   So if we are wow64, then the OS is 64-bit..
        If mbIsWow64Process Then '=IsWow64() Then
            '= chkX64.Checked = True
            intCSIDL_program = CSIDL_PROGRAM_FILESX86
            guidFOLDERID_program = FOLDERID_ProgramFilesX86
            '== txtSystemInfo.Text = txtSystemInfo.Text & "Current process is Wow64.." & vbCrLf
        Else
            '== txtSystemInfo.Text = txtSystemInfo.Text & "Current process is NOT Wow64." & vbCrLf
            '= chkX64.Checked = False
            intCSIDL_program = CSIDL_PROGRAM_FILES
            guidFOLDERID_program = FOLDERID_ProgramFiles
        End If

        '--App Data path doesn't care about 64-bit-
        '--  Below Vista, or Vista and above..

        '-- Check OS first--

        mOSVersion = Environment.OSVersion
        '== labOS.Text = mOSVersion.Version.ToString
        '== labOS.Text = labOS.Text & vbCrLf & mOSVersion.VersionString  '--includes Service Pack.-

        '-- Extended version info.--
        '-- Using the OSVERSIONINFO struct and corresponding signature:

        mOSvi2 = New OSVersionInfoEx()
        mOSvi2.dwOSVersionInfoSize = Marshal.SizeOf(mOSvi2)

        '-- https://msdn.microsoft.com/en-us/library/windows/desktop/ms724833(v=vs.85).aspx 

        '--App Data path doesn't care about 64-bit-
        '--  Below Vista, or Vista and above..
        Dim sbProgFilesPath As New StringBuilder(400)
        Dim sbAppDataPath As New StringBuilder(400)
        Dim sbPathx As New StringBuilder(400)

        If (mOSvi2.dwMajorVersion < 6) Then  '-- below vista-
            intCSIDL_appdata = CSIDL_COMMON_APPDATA
            If SHGetFolderPath(Nothing, intCSIDL_appdata, Nothing, 0, sbAppDataPath) <> 0 Then
                Throw New ApplicationException("Can't get window's directory")
            End If
            msAppDataDir = sbAppDataPath.ToString
            '- program path-
            If SHGetFolderPath(Nothing, intCSIDL_program, Nothing, 0, sbProgFilesPath) <> 0 Then
                Throw New ApplicationException("Can't get window's directory")
            End If
            msProgramDir = sbProgFilesPath.ToString
            '-- windows-
            sbPathx = New StringBuilder(400)
            If SHGetFolderPath(Nothing, CSIDL_WINDOWS, Nothing, 0, sbPathx) <> 0 Then
                Throw New ApplicationException("Can't get window's directory")
            End If
            msWindowsDir = sbPathx.ToString
            '-- windows\system -
            sbPathx = New StringBuilder(400)
            If SHGetFolderPath(Nothing, CSIDL_SYSTEM, Nothing, 0, sbPathx) <> 0 Then
                Throw New ApplicationException("Can't get window's directory")
            End If
            msSystemDir = sbPathx.ToString
        Else '--vista or above-
            guidFOLDERID_appData = FOLDERID_ProgramData
            msAppDataDir = GetKnownFolderPath(guidFOLDERID_appData)
            msProgramDir = GetKnownFolderPath(guidFOLDERID_program)
            '-win-
            msWindowsDir = GetKnownFolderPath(FOLDERID_Windows)
            msProgramDir = GetKnownFolderPath(FOLDERID_System)
        End If  '-vista-

        '-- get OS description-
        If GetVersionEx(mOSvi2) = 0 Then
            MsgBox("Failed to retrieve system Version info.", MsgBoxStyle.Exclamation)
        Else  '--ok-
            '--  get system version stuff from OSVersionInfoEx..
            Select Case mOSvi2.dwMajorVersion
                Case 5  '--xp/Server-2003.
                    Select Case mOSvi2.dwMinorVersion
                        Case 0
                            sVersionName = "Windows 2000."
                        Case 1
                            sVersionName = "Windows XP."
                        Case 2
                            If (mOSvi2.wProductType = VER_NT_WORKSTATION) Then
                                sVersionName = "Windows XP-64."
                            Else
                                sVersionName = "Windows Server 2003."
                            End If
                    End Select '-dwMinorVersion-
                Case 6  '--Vista/Win-7/Server-2008.
                    Select Case mOSvi2.dwMinorVersion
                        Case 0  '--Vista/Server-2008.
                            If (mOSvi2.wProductType = VER_NT_WORKSTATION) Then
                                sVersionName = "Windows Vista."
                            Else
                                sVersionName = "Windows Server 2008."
                            End If
                        Case 1
                            If (mOSvi2.wProductType = VER_NT_WORKSTATION) Then
                                sVersionName = "Windows 7."
                            Else
                                sVersionName = "Windows Server 2008-R2."
                            End If
                    End Select '-dwMinorVersion-
                Case Else
                    sVersionName = "Unknown or Windows 9x."
            End Select
        End If  '-get version..-

        msWindowsName = sVersionName
        '= labOS.Text = sVersionName & " - " & mOSvi2.versionString
        '= labOS.Text = labOS.Text & vbCrLf & " (" & mOSVersion.VersionString & ")"   '--includes Service Pack.-

        msOS_ServicePack = mOSvi2.versionString   '-- service Pack-
        mOSVersion_string = mOSVersion.VersionString  '--also includes Service Pack.-



    End Sub  '-new-
    '= = = = = = = = = =


End Class  '-clsWinSpecial-
'= = = = = = = = = = = = = = 
