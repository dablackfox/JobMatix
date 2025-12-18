
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

Module modVersionOS
    '==
    '==  grh. JobMatixPOS 3.1.3101.0330 ---  30-Mar-2015 ===
    '==   >>  Started Module for OS version info... 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = =


    Const VER_NT_WORKSTATION As Integer = 1
    Const VER_NT_SERVER As Integer = 3
    Const VER_NT_DOMAIN_CONTROLLER As Integer = 2


    '-- Using the OSVERSIONINFO struct and corresponding signature:
    '-- Using the OSVERSIONINFO struct and corresponding signature:

    '--  OSVERSIONINFOEX  --
    '--  https://msdn.microsoft.com/en-us/library/windows/desktop/ms724833(v=vs.85).aspx  --

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
    Private Function GetVersionEx(ByRef osvi As OSVersionInfoEx) As Boolean
    End Function
    '= = = = = = = = = =
    '-===FF->

    '-- VB.NET definitions..--
    '-- VB.NET definitions..--

    '-- IS ADMIN --
    '-- IsUserAnAdmin --
    <DllImport("shell32.dll", EntryPoint:="IsUserAnAdmin")> _
    Public Function IsUserAnAdmin() As Boolean
    End Function


    '-  THANKS to Pinvoke.net

    '-- GetModuleHandle --
    '-- GetModuleHandle --

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
      Public Function GetModuleHandle(ByVal lpModuleName As String) As IntPtr
    End Function


    '-- GetProcAddress --
    '-- GetProcAddress --

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Ansi, ExactSpelling:=True)> _
      Public Function GetProcAddress(ByVal hModule As IntPtr, ByVal procName As String) As UIntPtr
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
      Public Function GetCurrentProcess() As SafeWaitHandle '==IntPtr
    End Function
    '= = = = = = = = = =
    '-===FF->


    '-- IsWow64Process --
    '-- IsWow64Process --

    '-- Determines whether the specified process is running under WOW64. 
    '-- Some WinAPI functions work differently when running through 
    '-- WOW64, so you will sometimes need to know if a process is under the thunking layer.

    <DllImport("Kernel32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)> _
       Public Function IsWow64Process( _
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

    Private Const CSIDL_WINDOWS As Int32 = &H24    '-- GetWindowsDirectory()
    Private Const CSIDL_SYSTEM As Int32 = &H25    '--// GetSystemDirectory()
    Private Const CSIDL_PROGRAM_FILES As Int32 = &H26    '--  C:\Program Files
    Private Const CSIDL_MYPICTURES As Int32 = &H27    '--  C:\Program Files\My Pictures
    Private Const CSIDL_PROFILE As Int32 = &H28    '--  USERPROFILE
    Private Const CSIDL_SYSTEMX86 As Int32 = &H29    '--  x86 system directory on RISC
    Private Const CSIDL_PROGRAM_FILESX86 As Int32 = &H2A    '--  x86 C:\Program Files on RISC
    Private Const CSIDL_PROGRAM_FILES_COMMON As Int32 = &H2B    '--  C:\Program Files\Common
    Private Const CSIDL_PROGRAM_FILES_COMMONX86 As Int32 = &H2C    '--  x86 Program Files\Common on RISC

    <DllImport("shell32.dll")> _
     Private Function SHGetFolderPath(ByVal hwndOwner As IntPtr, _
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


    <DllImport("shell32.dll")> _
    Private Function SHGetKnownFolderPath(<MarshalAs(UnmanagedType.LPStruct)> ByVal rfid As Guid, _
                                      ByVal dwFlags As UInt32, _
                                       ByVal hToken As IntPtr, _
                                         <MarshalAs(UnmanagedType.LPWStr)> ByRef pszPath As StringBuilder) As Int32
    End Function '--SHGetKnownFolderPath-- 
    '= = = = = = =  == = 
    '= = = = = = = = = =
    '-===FF->

    '= = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = =

    '== Private Const k_SFX_EXTRACT_PATH As String = "c:\zzzz-JobMatix3-xFiles"
    '-  WE use the WinRar SFX option "TempMode"-
    '--         which sets the setup working dir. to the temp dir..
    Const K_SAVESETTINGSPATH As String = "localJobSettings.txt"

    '--  Static vars..--
    Private mbActivated As Boolean = False

    Private mOSVersion As System.OperatingSystem  '== = Environment.OSVersion
    Private mOsvi2 As OSVersionInfoEx

    Private mColSqlServers As Collection
    Private mSdSettings As clsStrDictionary   '==  Scripting.Dictionary

    Private msJobMatixAppPath As String = ""

    Private folderBrowserDialog1 As FolderBrowserDialog
    '= = = = = = = = = = = =  = = = === 
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
    Public Function gbIsWow64() As Boolean

        Dim proc As Integer
        proc = GetProcAddress(GetModuleHandle("Kernel32.dll"), "IsWow64Process")

        If proc <= 0 Then
            '== txtSystemInfo.Text = txtSystemInfo.Text & "NO Dll entry point found for 'IsWow64Process'" & vbCrLf
            Return False
        End If
        'Dim processHandle As Long = GetProcessHandle(System.Diagnostics.Process.GetCurrentProcess().Id)
        Dim retVal As Boolean
        If IsWow64Process(GetCurrentProcess(), retVal) Then
            gbIsWow64 = retVal  '= Return retVal
        Else
            MsgBox("Error in call to 'IsWow64Process' system function. ", MsgBoxStyle.Exclamation)
            gbIsWow64 = False  '= Return False
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

    '- get OS version..-

    Public Function gbGetVersionOS(ByRef sVersionNo As String, _
                                    ByRef sVersionName As String, _
                                     ByRef sVersionSP As String) As Boolean

        gbGetVersionOS = False
        mOSVersion = Environment.OSVersion
        '== labOS.Text = mOSVersion.Version.ToString
        '== labOS.Text = labOS.Text & vbCrLf & mOSVersion.VersionString  '--includes Service Pack.-

        '-- Extended version info.--
        '-- Using the OSVERSIONINFO struct and corresponding signature:
        mOsvi2 = New OSVersionInfoEx()
        mOsvi2.dwOSVersionInfoSize = Marshal.SizeOf(mOsvi2)

        If GetVersionEx(mOsvi2) = 0 Then
            MsgBox("Failed to retrieve system Version info.", MsgBoxStyle.Exclamation)
        Else  '--ok-
            '--  get system version stuff from OSVersionInfoEx..
            Select Case mOsvi2.dwMajorVersion
                Case 5  '--xp/Server-2003.
                    Select Case mOsvi2.dwMinorVersion
                        Case 0
                            sVersionName = "Windows 2000."
                        Case 1
                            sVersionName = "Windows XP."
                        Case 2
                            If (mOsvi2.wProductType = VER_NT_WORKSTATION) Then
                                sVersionName = "Windows XP-64."
                            Else
                                sVersionName = "Windows Server 2003."
                            End If
                    End Select '-dwMinorVersion-
                Case 6  '--Vista/Win-7/Server-2008.
                    Select Case mOsvi2.dwMinorVersion
                        Case 0  '--Vista/Server-2008.
                            If (mOsvi2.wProductType = VER_NT_WORKSTATION) Then
                                sVersionName = "Windows Vista."
                            Else
                                sVersionName = "Windows Server 2008."
                            End If
                        Case 1
                            If (mOsvi2.wProductType = VER_NT_WORKSTATION) Then
                                sVersionName = "Windows 7."
                            Else
                                sVersionName = "Windows Server 2008-R2."
                            End If
                    End Select '-dwMinorVersion-
                Case Else
                    sVersionName = "Unknown or Windows 9x."
            End Select
            sVersionNo = mOSVersion.VersionString
            sVersionSP = mOsvi2.versionString
            gbGetVersionOS = True
        End If  '-get version..-

        '== Console.WriteLine( "Struct size:   {0}", osvi2.OSVersionInfoSize );
        '== labOS.Text = sVersionName & " - " & mOsvi2.versionString
        '== labOS.Text = labOS.Text & vbCrLf & " (" & mOSVersion.VersionString & ")"   '--includes Service Pack.-
        DoEvents()

    End Function  '--gbGetVersionOS-
    '= = = = = =  = = = = = = == =
    '-===FF->






End Module  '-- modVersionOS-

'== the end  ==
