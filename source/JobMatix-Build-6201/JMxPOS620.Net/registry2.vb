Option Strict Off
Option Explicit On
Module registry2
	
	'--  How To Use the Registry API to Save and Retrieve Setting==
	'-- This article was previously published under Q145679
	
	Public Const REG_SZ As Integer = 1
	Public Const REG_DWORD As Integer = 4
	
	Public Const HKEY_CLASSES_ROOT As Integer = &H80000000
	Public Const HKEY_CURRENT_USER As Integer = &H80000001
	Public Const HKEY_LOCAL_MACHINE As Integer = &H80000002
	Public Const HKEY_USERS As Integer = &H80000003
	
	Public Const ERROR_NONE As Short = 0
	Public Const ERROR_BADDB As Short = 1
	Public Const ERROR_BADKEY As Short = 2
	Public Const ERROR_CANTOPEN As Short = 3
	Public Const ERROR_CANTREAD As Short = 4
	Public Const ERROR_CANTWRITE As Short = 5
	Public Const ERROR_OUTOFMEMORY As Short = 6
	Public Const ERROR_ARENA_TRASHED As Short = 7
	Public Const ERROR_ACCESS_DENIED As Short = 8
	Public Const ERROR_INVALID_PARAMETERS As Short = 87
	Public Const ERROR_NO_MORE_ITEMS As Short = 259
	
	Public Const KEY_QUERY_VALUE As Short = &H1s
	Public Const KEY_SET_VALUE As Short = &H2s
	Public Const KEY_ALL_ACCESS As Short = &H3Fs
	
	Public Const REG_OPTION_NON_VOLATILE As Short = 0
	
	Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Integer) As Integer
	Declare Function RegCreateKeyEx Lib "advapi32.dll"  Alias "RegCreateKeyExA"(ByVal hKey As Integer, ByVal lpSubKey As String, ByVal Reserved As Integer, ByVal lpClass As String, ByVal dwOptions As Integer, ByVal samDesired As Integer, ByVal lpSecurityAttributes As Integer, ByRef phkResult As Integer, ByRef lpdwDisposition As Integer) As Integer
	Declare Function RegOpenKeyEx Lib "advapi32.dll"  Alias "RegOpenKeyExA"(ByVal hKey As Integer, ByVal lpSubKey As String, ByVal ulOptions As Integer, ByVal samDesired As Integer, ByRef phkResult As Integer) As Integer
	Declare Function RegQueryValueExString Lib "advapi32.dll"  Alias "RegQueryValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal lpData As String, ByRef lpcbData As Integer) As Integer
	Declare Function RegQueryValueExLong Lib "advapi32.dll"  Alias "RegQueryValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByRef lpData As Integer, ByRef lpcbData As Integer) As Integer
	Declare Function RegQueryValueExNULL Lib "advapi32.dll"  Alias "RegQueryValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal lpData As Integer, ByRef lpcbData As Integer) As Integer
	Declare Function RegSetValueExString Lib "advapi32.dll"  Alias "RegSetValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByVal lpValue As String, ByVal cbData As Integer) As Integer
	Declare Function RegSetValueExLong Lib "advapi32.dll"  Alias "RegSetValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByRef lpValue As Integer, ByVal cbData As Integer) As Integer
	
	'= = = = = = =  =
	
	'--wrapper functions---
	Private Function SetValueEx(ByVal hKey As Integer, ByRef sValueName As String, ByRef lType As Integer, ByRef vValue As Object) As Integer
		Dim lValue As Integer
		Dim sValue As String
		Select Case lType
			Case REG_SZ
				'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				sValue = vValue & Chr(0)
				SetValueEx = RegSetValueExString(hKey, sValueName, 0, lType, sValue, Len(sValue))
			Case REG_DWORD
				'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				lValue = vValue
				SetValueEx = RegSetValueExLong(hKey, sValueName, 0, lType, lValue, 4)
		End Select
	End Function '-- set--
	'= = = = = = = = = = = =
	
	Private Function QueryValueEx(ByVal lhKey As Integer, ByVal szValueName As String, ByRef vValue As Object) As Integer
		Dim cch As Integer
		Dim lrc As Integer
		Dim lType As Integer
		Dim lValue As Integer
		Dim sValue As String
		
		On Error GoTo QueryValueExError
		
		' Determine the size and type of data to be read
		lrc = RegQueryValueExNULL(lhKey, szValueName, 0, lType, 0, cch)
		If lrc <> ERROR_NONE Then Error(5)
		
		Select Case lType
			' For strings
			Case REG_SZ
				sValue = New String(Chr(0), cch)
				
				lrc = RegQueryValueExString(lhKey, szValueName, 0, lType, sValue, cch)
				If lrc = ERROR_NONE Then
					'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					vValue = Left(sValue, cch - 1)
				Else
					'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					vValue = Nothing
				End If
				' For DWORDS
			Case REG_DWORD
				lrc = RegQueryValueExLong(lhKey, szValueName, 0, lType, lValue, cch)
				'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If lrc = ERROR_NONE Then vValue = lValue
			Case Else
				'all other data types not supported
				lrc = -1
		End Select
		
QueryValueExExit: 
		QueryValueEx = lrc
		Exit Function
		
QueryValueExError: 
		Resume QueryValueExExit
	End Function '--Query--
	'= = = = = = = = = = = = = = = =
	
	'=== Creating a New Key:  ===
	
	'--Creating a new key is as simple as using the following procedure.
	'-- CreateNewKey takes the name of the key to create, and the constant representing the
	'----predefined key to create the key under.
	'-- The call to RegCreateKeyEx doesn't take advantage of the security mechanisms allowed,
	'------ but could be modified to do so.
	'--A discussion of Registry security is outside the scope of this article.
	
	'--eg., Calling CreateNewKey like this:
	'---     CreateNewKey "TestKey\SubKey1\SubKey2", HKEY_LOCAL_MACHINE
	'----will create three-nested keys beginning with TestKey immediately under HKEY_CURRENT_USER,
	'-----SubKey1 subordinate to TestKey, and SubKey3 under SubKey2.
	
	Public Sub gRegCreateNewKey(ByRef sNewKeyName As String, ByRef lPredefinedRootKey As Integer)
		Dim hNewKey As Integer 'handle to the new key
		Dim lRetVal As Integer 'result of the RegCreateKeyEx function
		
		lRetVal = RegCreateKeyEx(lPredefinedRootKey, sNewKeyName, 0, vbNullString, REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, 0, hNewKey, lRetVal)
		RegCloseKey(hNewKey)
	End Sub '--create--
	'= = = = = = = = = = =
	
	'=== Setting/Modifying a Value: ==
	
	'--Creating and setting a value of a specified key can be accomplished with the following short procedure.
	'--- SetKeyValue takes the key that the value will be associated with, the name of the value,
	'----------the setting of the value, and the type of the value
	'----------- (the SetValueEx function only supports REG_SZ and REG_DWORD, but this can be modified if necessary).
	'---------Specifying a new value for an existing sValueName will modify the current setting of that value.
	'-- A call of:
	'------   SetKeyValue "TestKey\SubKey1", "StringValue", "Hello", REG_SZ
	'-----will create a value of type REG_SZ called "StringValue" with the setting of "Hello."
	'---------This value will be associated with the key SubKey1 of "TestKey."
	
	Public Sub gRegSetKeyValue(ByRef lPredefinedRootKey As Integer, ByRef sKeyName As String, ByRef sValueName As String, ByRef vValueSetting As Object, ByRef lValueType As Integer)
		Dim lRetVal As Integer 'result of the SetValueEx function
		Dim hKey As Integer 'handle of open key
		
		'open the specified key
		'--lRetVal = RegOpenKeyEx(HKEY_CURRENT_USER, sKeyName, 0, _
		''--                          KEY_SET_VALUE, hKey)
		lRetVal = RegOpenKeyEx(lPredefinedRootKey, sKeyName, 0, KEY_SET_VALUE, hKey)
		lRetVal = SetValueEx(hKey, sValueName, lValueType, vValueSetting)
		RegCloseKey(hKey)
	End Sub '--set key--
	'= = = = = = = = = = =  =
	
	'===  Querying a Value:  ==
	
	'--The next procedure can be used to ascertain the setting of an existing value.
	'----QueryValue takes the name of the key and the name of a value associated with that key
	'------- and displays a message box with the corresponding value.
	'------It uses a call to the QueryValueEx wrapper function defined below   (above ?)--,
	'-------that only supports REG_SZ and REG_DWORD types.
	'== With this procedure, a call of:
	'-----<<   QueryValue "TestKey\SubKey1", "StringValue"  >>
	'----will display a message box with the current setting of the "StringValue" value,
	'----and assumes that "StringValue" exists in the "TestKey\SubKey1" key.
	
	'--If the Value that you query does not exist then QueryValue will return an error code of 2 - 'ERROR_BADKEY'.
	
	Public Function gsRegQueryValue(ByRef lPredefinedRootKey As Integer, ByRef sKeyName As String, ByRef sValueName As String, ByRef vValue As Object) As Integer
		Dim lRetVal As Integer 'result of the API functions
		Dim hKey As Integer 'handle of opened key
		'--Dim vValue As Variant      'setting of queried value
		
		lRetVal = RegOpenKeyEx(lPredefinedRootKey, sKeyName, 0, KEY_QUERY_VALUE, hKey)
		If lRetVal = 0 Then '--ok--
			lRetVal = QueryValueEx(hKey, sValueName, vValue)
			If (lRetVal <> 0) Then MsgBox("Failed to get Reg.Value:" & vbCrLf & sValueName)
			RegCloseKey(hKey)
		Else
			MsgBox("Failed to open Reg.key:" & vbCrLf & sKeyName)
		End If
		gsRegQueryValue = lRetVal '-vValue
	End Function '--query--
	'= = = = = = = = = =
	
	'===  Querying a KEY:  ==  NO msgBox !! ==
	'===   grh -- 13Dec2009===
	
	'--If the Value that you query does not exist then QueryValue will return an error code of 2 - 'ERROR_BADKEY'.
	
	Public Function gsRegQueryValue2(ByRef lPredefinedRootKey As Integer, ByRef sKeyName As String, ByRef sValueName As String, ByRef vValue As Object) As Integer
		Dim lRetVal As Integer 'result of the API functions
		Dim hKey As Integer 'handle of opened key
		'--Dim vValue As Variant      'setting of queried value
		
		lRetVal = RegOpenKeyEx(lPredefinedRootKey, sKeyName, 0, KEY_QUERY_VALUE, hKey)
		If lRetVal = 0 Then '--ok--
			lRetVal = QueryValueEx(hKey, sValueName, vValue)
			'==If (lRetVal <> 0) Then _
			''==        MsgBox "Failed to get Reg.Value:" + vbCrLf + sValueName
			RegCloseKey(hKey)
		Else
			'---MsgBox "Failed to open Reg.key:" + vbCrLf + sKeyName
		End If
		gsRegQueryValue2 = lRetVal '-vValue
	End Function '--query--
	'= = = = = = = = = =
	
	
	'==end reg stuff===
End Module