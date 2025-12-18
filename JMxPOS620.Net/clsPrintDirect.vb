Option Explicit On
Option Strict Off

Imports System
Imports System.Runtime.InteropServices    
Imports System.ComponentModel
Imports vb = Microsoft.VisualBasic

Public Class clsPrintDirect

    '-- class to print Directly to the printer. (NOT using .Net PrintDocument)..
    '-- grh 28-March-2018=
    ''-- Help From- 
    '-- http://www.vbforums.com/showthread.php?830329-Print-on-Network-Printer-winspool-API  

    Public Sub New()
    End Sub  '--new-
    ' = = = = == =

    '-- printStringDirect--

    Public Function PrintStringDirect(ByVal strDocName As String, _
                                      ByVal strPrinterName As String, _
                                        ByVal byteData() As Byte) As Boolean
        Dim printerHandle, pUnmanagedData As IntPtr
        Dim bReturn, bSuccess As Boolean
        Dim lpcWritten As Int32
        Dim LenString, level As Integer
        '=  Dim sWrittenData As String
        Dim MyDocInfo As NativeMethods.DOC_INFO_1A

        PrintStringDirect = False

        level = 1
        bReturn = NativeMethods.OpenPrinter(strPrinterName, printerHandle, IntPtr.Zero)
        If (Not bReturn) Then
            MsgBox("The Printer Name " & strPrinterName & " is not recognized.", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '-- printer open--
        MyDocInfo.pDocName = strDocName
        MyDocInfo.pOutputFile = vbNullString
        MyDocInfo.pDataType = vbNullString  '-- "RAW" & Chr(0) GIVED error 1804 (invalid datatype).
        If ((NativeMethods.StartDocPrinter(printerHandle, 1, MyDocInfo)) <> 0) Then
            '-pUnmanagegBytes points to unmanaged data to print.-
            Dim byteManagedData As Byte() = byteData
            '- get unm. memory and move data-
            Try
                pUnmanagedData = Marshal.AllocCoTaskMem(byteManagedData.Length)
                Marshal.Copy(byteManagedData, 0, pUnmanagedData, byteManagedData.Length)
            Catch ex As Exception
                MsgBox("PrintStringDirect function: " & vbCrLf & _
                        " Failed to copy print data to unmanaged memory." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Exit Function
            End Try
            '-- start printer-
            ' set by default failure printing
            bSuccess = False
            If (NativeMethods.StartPagePrinter(printerHandle)) Then
                '=ok=
                If NativeMethods.WritePrinter(printerHandle, pUnmanagedData, _
                                                         byteManagedData.Length, lpcWritten) Then
                    bSuccess = True
                End If '-written-
                NativeMethods.EndPagePrinter(printerHandle)
            End If
            Marshal.FreeCoTaskMem(pUnmanagedData)
            NativeMethods.EndDocPrinter(printerHandle)
            If bSuccess Then
                Interaction.Beep()
                PrintStringDirect = True
            Else
                '-- get last error-
                MsgBox("Win32 Error in printing-  Code is: " & Marshal.GetLastWin32Error())
            End If
        Else
            MsgBox("PrintStringDirect function: " & vbCrLf & _
                   " Failed Win32 StartDocPrinter." & vbCrLf, MsgBoxStyle.Exclamation)
            '-- get last error-
            MsgBox("Win32 Error in printing-  Code is: " & Marshal.GetLastWin32Error())
            '=Exit Function
        End If
        NativeMethods.ClosePrinter(printerHandle)

    End Function  '-printStringDirect-
    '= = = = = = = = = = == = = =  == =
    '-===FF->

    '-Send CashDrawer Open Command-

    Public Function SendCashDrawerOpenCommand(ByVal sPrinterName As String, _
                                                 ByVal sCmdText As String, _
                                                 Optional ByVal bTesting As Boolean = False) As Boolean
        Dim byteData() As Byte = {}
        Dim sHex, s1, s2, s3 As String
        Dim sCodes() As String
        Dim sPrintData As String = ""

        SendCashDrawerOpenCommand = False
        If (sCmdText = "") Or (sPrinterName = "") Then
            Exit Function
        End If
        '-- Command text is in Display Mode. (is ESC p NULL etc)
        '--  Translate into actual ESC stuff.
        s1 = sCmdText
        '-- check for codes..
        If (s1 <> "") Then
            If (vb.Left(UCase(s1), 3)) = "ESC" Then
                '-codes
                sCodes = Split(s1, " ")
                If (sCodes.Length > 0) Then
                    For Each s2 In sCodes
                        If UCase(s2) = "ESC" Then
                            sPrintData &= Chr(27)
                        ElseIf UCase(s2) = "NULL" Then
                            sPrintData &= Chr(0)
                        ElseIf UCase(s2) = "SOH" Then
                            sPrintData &= Chr(1)
                        ElseIf UCase(s2) = "ENQ" Then
                            sPrintData &= Chr(5)
                        ElseIf (s2 = " ") Then '-drop extra spaces=-
                        ElseIf UCase(s2) = "SPACE" Then
                            sPrintData &= Chr(32)
                        Else
                            sPrintData &= s2  '- include everything else.
                        End If
                    Next s2
                Else
                    MsgBox("Nothing entered..", MsgBoxStyle.Exclamation)
                End If
            Else  '-just text-
                sPrintData = s1
            End If
        End If  '-testdata-
        '-send to printer..
        If (sPrintData <> "") And (sPrinterName <> "") Then
            '= use this- 
            byteData = System.Text.Encoding.UTF8.GetBytes(sPrintData)
            '= this gives 2 bytes per char.  byteData = System.Text.Encoding.Unicode.GetBytes(sTestData)
            '- show byte data..  in Hex..
            sHex = ""  '= "Byte Count: " & byteData.Length & ".." & vbCrLf
            For Each byte1 As Byte In byteData
                If sHex <> "" Then sHex &= ", "
                sHex &= Conversion.Hex(byte1)
            Next '-byte1-
            If bTesting Then
                MsgBox("Testing Cash Drawer- Sending bytes: " & vbCrLf & _
                           "Byte Count: " & byteData.Length & ".." & vbCrLf & sHex, MsgBoxStyle.Information)
            End If
            If PrintStringDirect("Till Open Cmd.", sPrinterName, byteData) Then
                SendCashDrawerOpenCommand = True
            End If
        End If
    End Function  '-mbSendCashDrawerOpenCommand=
    '= = = = = = = = == = = = = = = = = = = = =

End Class  '-clsPrintDirect-
'= = = = = = = == = = = = = = = = =
'-===FF->

'-- Beep--

Public NotInheritable Class Interaction

    Private Sub New()
    End Sub

    ' Callers require Unmanaged permission         
    Public Shared Sub Beep()
        ' No need to demand a permission as callers of Interaction.Beep                      
        ' will require UnmanagedCode permission                      
        If Not NativeMethods.MessageBeep(-1) Then
            Throw New Win32Exception()
        End If
    End Sub  '--beep-
End Class  '-Interaction-
'= = = = = = = = = = = = 

'--  Special class to contain p-Invoke Win32 stuff..
'--  Special class to contain p-Invoke Win32 stuff..
'--  Special class to contain p-Invoke Win32 stuff..
'-https://msdn.microsoft.com/en-us/library/ms182161(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1 

Friend NotInheritable Class NativeMethods

    Private Sub New()
    End Sub  '-new-

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Friend Shared Function MessageBeep(ByVal uType As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    '-- printer Functions..=
    '<StructLayout(LayoutKind.Sequential)> _
    '     Public Structure DOCINFO<MarshalA(UnmanagedType.LPWStr)> 
    '     Public pDocName As String<MarshalAs(UnmanagedType.LPWStr)> 
    '     Public pOutputFile As String<MarshalAs(UnmanagedType.LPWStr)> 
    '    Public pDataType As String
    'End Structure 'DOCINFO

    '- https://www.pinvoke.net/default.aspx/Structures/DOCINFO.html?diff=y 

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Public Structure DOC_INFO_1A
        <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
    End Structure

    Public Declare Function ClosePrinter Lib "winspool.drv" (ByVal hPrinter As IntPtr) As Boolean
    Public Declare Function EndDocPrinter Lib "winspool.drv" (ByVal hPrinter As IntPtr) As Boolean
    Public Declare Function EndPagePrinter Lib "winspool.drv" (ByVal hPrinter As IntPtr) As Boolean
    Public Declare Function OpenPrinter Lib "winspool.drv" _
                  Alias "OpenPrinterA" (ByVal src As String, _
                                        ByRef hPrinter As IntPtr, _
                                        ByVal pd As IntPtr) As Boolean
    Public Declare Function StartDocPrinter Lib "winspool.drv" _
                   Alias "StartDocPrinterA" (ByVal hPrinter As IntPtr, _
                                             Level As Integer, _
                                             ByRef pDocInfo As DOC_INFO_1A) As Integer
    Public Declare Function StartPagePrinter Lib "winspool.drv" (ByVal hPrinter As IntPtr) As Boolean
    Public Declare Function WritePrinter Lib "winspool.drv" (ByVal hPrinter As IntPtr, _
                                                             ByVal pBytes As IntPtr, _
                                                             ByVal dwCount As Int32, ByRef dwWritten As Int32) As Boolean
    Public Declare Function GetLastError Lib "kernel32" () As Int32


End Class  '_Nativemethods--
'= = = = = = = = = ==  =

'== the end==
