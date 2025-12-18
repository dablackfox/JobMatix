Option Strict Off
Option Explicit On
Imports System.Text
Imports System.Runtime.InteropServices

Module ModMD5
	'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = =  = =  = =
	'-- MD5 --
	'-- provides interface to Win32 MD5 Hash function..--
	'-- grh- 13-Jul-2010--  fixed hash data call..--(message is in bytes..)--

    '-- grh- 08-Dec-2011-- JobMatix3-  Fixes to VB.NET version..  DROP all "varPtr" usage--

    '== grh- 30-Jun-2012-- JobMatix3- Build 3061.2  Fixes to call to GetHashParam..
    '==     Use Struct EMULATING a byte array to get result. (Stringbuilder class has NULL termin.)..        --


    <StructLayout(LayoutKind.Explicit, Size:=16)> _
    Public Structure resultMD5
        <FieldOffset(0)> Public byte_0 As Byte
        <FieldOffset(1)> Public byte_1 As Byte
        <FieldOffset(2)> Public byte_2 As Byte
        <FieldOffset(3)> Public byte_3 As Byte
        <FieldOffset(4)> Public byte_4 As Byte
        <FieldOffset(5)> Public byte_5 As Byte
        <FieldOffset(6)> Public byte_6 As Byte
        <FieldOffset(7)> Public byte_7 As Byte
        <FieldOffset(8)> Public byte_8 As Byte
        <FieldOffset(9)> Public byte_9 As Byte
        <FieldOffset(10)> Public byte_10 As Byte
        <FieldOffset(11)> Public byte_11 As Byte
        <FieldOffset(12)> Public byte_12 As Byte
        <FieldOffset(13)> Public byte_13 As Byte
        <FieldOffset(14)> Public byte_14 As Byte
        <FieldOffset(15)> Public byte_15 As Byte
    End Structure  '--md5-
    '= = = = = = = = = = = = =

    '-----
	'--win32 API's--
	'--win32 API's--
	'--win32 API's--
	
	'-- WINBASEAPI DWORD WINAPI GetLastError( VOID );
	Declare Function GetLastError Lib "Kernel32.dll" () As Integer
	
	'= = = =  =
	'--- logoff function ---
	'---LIBRARY DEF IS:
	'---     BOOL WINAPI ExitWindowsEx( UINT uFlags,DWORD dwReason);
	Declare Function ExitWindowsEx Lib "user32.dll" (ByVal uFlags As Short, ByVal dwReason As Integer) As Short '--BOOL-
	'== = = = = = = ==
	
	
	'-- Win32 MD5 Hash functions..--
	'-- Win32 MD5 Hash functions..--
	'-- Win32 MD5 Hash functions..--
	
	Public Const CALG_MD5 As Integer = 32768 + 3 '-- &H8003
	Public Const PROV_RSA_FULL As Short = 1
	Public Const CRYPT_VERIFYCONTEXT As Integer = &HF0000000
	Public Const HP_HASHVAL As Short = &H2s
	Public Const MD5LEN As Short = 16
	'---#define MS_ENHANCED_PROV_A       "Microsoft Enhanced Cryptographic Provider v1.0"
	Public Const MS_ENHANCED_PROV_A As String = "Microsoft Enhanced Cryptographic Provider v1.0"
	
	
	'-- AcquireContext..--
	'-- AcquireContext..--
	
	'-- C++ --
	'-- BOOL WINAPI CryptAcquireContext(
	'--   __out  HCRYPTPROV *phProv,
	'--   __in   LPCTSTR pszContainer,
	'--   __in   LPCTSTR pszProvider,
	'--   __in   DWORD dwProvType,
	'--   __in   DWORD dwFlags
	'--  );
    Declare Function CryptAcquireContext Lib "advapi32.dll" _
                              Alias "CryptAcquireContextA" _
                                   (ByRef lphndProv As Integer, _
                                     ByVal lpContainer As Integer, _
                                       ByVal lpzProvider As String, _
                                                          ByVal lngProvType As Integer, _
                                                               ByVal lngFlags As Integer) As Integer '--ret BOOL-
	'= = = = = = = = = =
	'= = = = = = = = = =
	
	'-- Create Hash Object.----
	'-- Create Hash Object.----
	
	'-- C++ --
	'-- BOOL WINAPI CryptCreateHash(
	'--   __in   HCRYPTPROV hProv,
	'--   __in   ALG_ID Algid,
	'--   __in   HCRYPTKEY hKey,
	'--   __in   DWORD dwFlags,
	'--   __out  HCRYPTHASH *phHash
	'-- );
    Declare Function CryptCreateHash Lib "advapi32.dll" (ByVal hndProv As Integer, _
                                                         ByVal intAlgId As Integer, _
                                                             ByVal hndKey As Integer, _
                                                              ByVal lngFlags As Integer, _
                                                                ByRef lphHash As Integer) As Integer '--ret BOOL-
	'--- "lphHash" is ptr to handle which will be passed back as result..==
	'= = = = = = = = = =  =
	'= = = = = = = = = =  =
	
	'--  Add some data to Hash..-
	'--  Add some data to Hash..-
	
	'-- C++ --
	'-- BOOL WINAPI CryptHashData(
	'--   __in  HCRYPTHASH hHash,
	'--   __in  BYTE *pbData,
	'--   __in  DWORD dwDataLen,
	'--   __in  DWORD dwFlags
	'-- );
	
    Declare Function CryptHashData Lib "advapi32.dll" _
                                       (ByVal hHash As Integer, _
                      <MarshalAs(UnmanagedType.LPStr)> ByVal pbData As StringBuilder, _
                                                         ByVal lngDataLen As Integer, _
                                                            ByVal lngFlags As Integer) As Integer '--ret BOOL-
	'--- "hHash" is actual hash-obj handle passed back as CREATE result..==
	'---- pbData is ptr to byte array data to add to hash arg.-
	'= = = = = = = = = =  =
	'= = = = = = = = = =  =
	
	'--- GET final hash RWSULT..-
	'--- GET final hash RWSULT..-
	
	'-- C++ --
	'-- BOOL WINAPI CryptGetHashParam(
	'--   __in     HCRYPTHASH hHash,
	'--   __in     DWORD dwParam,
	'--   __out    BYTE *pbData,
	'--   __inout  DWORD *pdwDataLen,
	'--   __in     DWORD dwFlags
	'-- );
	
    '== Declare Function CryptGetHashParam Lib "advapi32.dll" _
    '==                                      (ByVal hHash As Integer, _
    '==                                        ByVal lngParam As Integer, _
    '==                                 <MarshalAs(UnmanagedType.LPStr)> ByVal pbData As StringBuilder, _
    '==                                           ByRef pDataLen As Integer, _
    '==                                             ByVal lngFlags As Integer) As Integer '--ret BOOL-

    '==  30Jun2012==
    Declare Function CryptGetHashParam Lib "advapi32.dll" _
                                         (ByVal hHash As Integer, _
                                           ByVal lngParam As Integer, _
                                           ByRef pbdata As resultMD5, _
                                              ByRef pDataLen As Integer, _
                                                ByVal lngFlags As Integer) As Integer '--ret BOOL-
    '--- "hHash" is actual hash-obj handle passed back as CREATE result..==
    '---- pbData is ptr to byte array data for hash result.-
    '---- "pDataLen" is pointer to DWORD (long) to receive actual size..-
    '= = = = = = = = = =  =
    '= = = = = = = = = =  =

    '-- DESTROY HASH Object..--
    '-- DESTROY HASH Object..--

    '-- C++ --
    '-- BOOL WINAPI CryptDestroyHash(
    '--   __in  HCRYPTHASH hHash
    '-- );
	Declare Function CryptDestroyHash Lib "advapi32.dll" (ByVal hHash As Integer) As Integer '--ret BOOL-
	'= = = = = = = = = =  =
	'= = = = = = = = = =  =
	
	'-- ReleaseContext --
	'-- ReleaseContext --
	
	'-- C++ --
	'-- BOOL WINAPI CryptReleaseContext(
	'--   __in  HCRYPTPROV hProv,
	'--   __in  DWORD dwFlags
	'-- );
    Declare Function CryptReleaseContext Lib "advapi32.dll" (ByVal hndProv As Integer, _
                                                             ByVal lngFlags As Integer) As Integer '--ret BOOL-
	'= = = = = = = = = =  =
	'= = = = = = = = = =  =
	
	
	'-- GET MD5 HASH of message.--
	'-- GET MD5 HASH of message.--
	
    Public Function gbGetMD5Hash(ByVal sMessageData As String, _
                                  ByRef byteResult() As Byte) As Boolean
        Dim lngProvHandle As Integer
        Dim lngHashHandle As Integer
        Dim kx, ix, lngCount As Integer
        Dim s1 As String
        Dim sProvider As String
        '== Dim byteMessage() As Byte
        '== Dim strProvider As StringBuilder
        Dim strSource As StringBuilder
        '==Dim strResult As StringBuilder
        Dim result1 As resultMD5

        gbGetMD5Hash = False
        sProvider = MS_ENHANCED_PROV_A & Chr(0)
        '== strProvider = New StringBuilder(MS_ENHANCED_PROV_A & Chr(0))
        If CryptAcquireContext(lngProvHandle, 0, sProvider, PROV_RSA_FULL, CRYPT_VERIFYCONTEXT) <> 0 Then '-ok-
            If CryptCreateHash(lngProvHandle, CALG_MD5, 0, 0, lngHashHandle) <> 0 Then '--created..-
                '== ReDim byteMessage(Len(sMessageData) - 1)
                '== '--re-do msg as array of bytes..-
                '== For ix = 0 To (Len(sMessageData) - 1)
                '== byteMessage(ix) = CByte(Asc(Mid(sMessageData, ix + 1, 1)))
                '== Next ix

                '--re-do msg as stringBuilder--
                strSource = New StringBuilder(sMessageData)

                '== If CryptHashData(lngHashHandle, byteMessage(0), Len(sMessageData), 0) <> 0 Then '--pk..-
                If CryptHashData(lngHashHandle, strSource, Len(sMessageData), 0) <> 0 Then '--pk..-
                    ReDim byteResult(MD5LEN - 1)
                    lngCount = MD5LEN
                    '-- set up result receiving string..--
                    '== strResult = New StringBuilder("", MD5LEN)
                    '== If CryptGetHashParam(lngHashHandle, HP_HASHVAL, strResult, lngCount, 0) <> 0 Then '--ok..-
                    If CryptGetHashParam(lngHashHandle, HP_HASHVAL, result1, lngCount, 0) <> 0 Then '--ok..-
                        gbGetMD5Hash = True
                        s1 = ""  '--test-
                        '--  convert result string bacak to caller's array..
                        byteResult(0) = result1.byte_0
                        byteResult(1) = result1.byte_1
                        byteResult(2) = result1.byte_2
                        byteResult(3) = result1.byte_3
                        byteResult(4) = result1.byte_4
                        byteResult(5) = result1.byte_5
                        byteResult(6) = result1.byte_6
                        byteResult(7) = result1.byte_7
                        byteResult(8) = result1.byte_8
                        byteResult(9) = result1.byte_9
                        byteResult(10) = result1.byte_10
                        byteResult(11) = result1.byte_11
                        byteResult(12) = result1.byte_12
                        byteResult(13) = result1.byte_13
                        byteResult(14) = result1.byte_14
                        byteResult(15) = result1.byte_15
                        For ix = 0 To (MD5LEN - 1)
                            '== byteResult(ix) = Asc(strResult.Chars(ix))
                            '== s1 = s1 & " " & Hex(Asc(strResult.Chars(ix)))   '--  THIS SHOWS CORRCT RESULT..-
                            s1 = s1 & " " & Hex(Asc(byteResult(ix)))   '--  THIS SHOWS CORRCT RESULT..-
                        Next ix
                        '== MsgBox("MD5 String result (" & lngCount & ") is: " & vbCrLf & s1, MsgBoxStyle.Information) '--TEST-
                    Else '--failed to get result..
                        MsgBox("MD5 Failed in CryptGetHashParam..  Error is: " & GetLastError(), MsgBoxStyle.Critical)
                    End If '--result.-
                Else '--failed to hash..-
                    MsgBox("MD5 Failed in CrypttHashData..  Error is: " & GetLastError(), MsgBoxStyle.Critical)

                End If '--hashData..-
                Call CryptDestroyHash(lngHashHandle) '--done..-
            Else '--failed to create hash..-
                MsgBox("MD5 Failed in CryptCreateHash..  Error is: " & GetLastError(), MsgBoxStyle.Critical)
            End If '--create hash..-
            Call CryptReleaseContext(lngProvHandle, 0) '--done.-
        Else '--failed to acquire context..-
            MsgBox("MD5 Failed in CryptAcquireContext..  Error is: " & GetLastError(), MsgBoxStyle.Critical)
        End If '--acquire.--

    End Function '-- get hash..--
	'= = = = = = = = =  = = ==
	
	'=== the end.. ===
End Module