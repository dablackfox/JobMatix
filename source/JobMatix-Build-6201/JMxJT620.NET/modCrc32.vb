Option Strict Off
Option Explicit On
Option Compare Text
Module modCrc32
	
	'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = =  =
	
	'---CRC32 and ABN check-digit routines..--
	'---CRC32 and ABN check-digit routines..--
	
	'---This STATIC array variable declared for Crc32Table--
	Private Crc32Table(255) As Integer
	'= = = = =
	
	'-- ABN validity check..--
	'-- ABN validity check..--
	'--courtesy ato.gov ...
	'-- gh- 11-July-2010-  Returns Weighted Sum for user..--
	
	'-- http://www.ato.gov.au/businesses/content.asp?doc=/content/13187.htm&pc=001/003/021/001/008&mnu=610&mfp=001/003&st=&cy=1
	
	Public Function gbCheckValidABN(ByVal sInputABN As String, ByRef lngSum As Integer) As Boolean
		Dim sABN As String
		'UPGRADE_WARNING: Lower bound of array aiABN was changed from 1 to 0. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		Dim aiABN(11) As Short '-- abn digits..--
		Dim avWeights() As Object '-- check weights..--
		Dim ix, sx As Integer
		
		gbCheckValidABN = False
		lngSum = 0
		sABN = Replace(sInputABN, " ", "") '--strip..--
		If (Len(sABN) <> 11) Then Exit Function
		For ix = 1 To 11
			aiABN(ix) = CShort(Mid(sABN, ix, 1))
		Next ix
		aiABN(1) = aiABN(1) - 1 '--subtract 1 from high-order digit..-
		
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		avWeights = New Object(){10, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19}
		
		'--compute products..-
		For ix = 1 To 11
			'UPGRADE_WARNING: Couldn't resolve default property of object avWeights(ix - 1). Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			lngSum = lngSum + (aiABN(ix) * avWeights(ix - 1))
		Next ix
		If ((lngSum Mod 89) = 0) Then gbCheckValidABN = True
		
	End Function '--abn.--
	'= = = = = = = =  = =
	
	
	'---  CRC32 --
	'---  CRC32 --
	'---  CRC32 --
	'-- downloaded from--
	'--- http://www.freevbcode.com/ShowCode.Asp?ID=655
	'---  CRC32 --
	'---  CRC32 --
	'- - - - - -
	'----- DECLARE static table (above).. --
	'------Then all we have to do is write public functions like
	'-----  these...
	
	Public Function InitCrc32(Optional ByVal Seed As Integer = &HEDB88320, Optional ByVal Precondition As Integer = &HFFFFFFFF) As Integer
		
		'----// Declare counter variable iBytes,
		'--- counter variable iBits,
		'--- value variables lCrc32 and lTempCrc32
		
		Dim iBytes, iBits As Short
		Dim lCrc32 As Integer
		Dim lTempCrc32 As Integer
		
		'// Turn on error trapping
		On Error Resume Next
		
		'// Iterate 256 times
		For iBytes = 0 To 255
			
			'// Initiate lCrc32 to counter variable
			lCrc32 = iBytes
			
			'// Now iterate through each bit in counter byte
			For iBits = 0 To 7
				'// Right shift unsigned long 1 bit
				lTempCrc32 = lCrc32 And &HFFFFFFFE
				lTempCrc32 = lTempCrc32 \ &H2s
				lTempCrc32 = lTempCrc32 And &H7FFFFFFF
				
				'// Now check if temporary is less than zero and then
				'mix Crc32 checksum with Seed value
				If (lCrc32 And &H1s) <> 0 Then
					lCrc32 = lTempCrc32 Xor Seed
				Else
					lCrc32 = lTempCrc32
				End If
			Next 
			
			'// Put Crc32 checksum value in the holding array
			Crc32Table(iBytes) = lCrc32
		Next 
		
		'// After this is done, set function value to the
		'precondition value
		InitCrc32 = Precondition
		
	End Function '--init crc--
	'= = = = = = = = = =  =
	
	'--// The function above is the initializing function, now
	'---we have to write the computation function
	
	Public Function AddCrc32(ByVal Item As String, ByVal Crc32 As Integer) As Integer
		
		'// Declare following variables
		Dim bCharValue As Byte
		Dim iCounter As Short
		Dim lIndex As Integer
		Dim lAccValue, lTableValue As Integer
		
		'// Turn on error trapping
		On Error Resume Next
		
		'// Iterate through the string that is to be checksum-computed
		For iCounter = 1 To Len(Item)
			
			'// Get ASCII value for the current character
			bCharValue = Asc(Mid(Item, iCounter, 1))
			
			'// Right shift an Unsigned Long 8 bits
			lAccValue = Crc32 And &HFFFFFF00
			lAccValue = lAccValue \ &H100s
			lAccValue = lAccValue And &HFFFFFF
			
			'// Now select the right adding value from the
			'holding table
			lIndex = Crc32 And &HFFs
			lIndex = lIndex Xor bCharValue
			lTableValue = Crc32Table(lIndex)
			
			'// Then mix new Crc32 value with previous
			'accumulated Crc32 value
			Crc32 = lAccValue Xor lTableValue
		Next 
		
		'// Set function value the the new Crc32 checksum
		AddCrc32 = Crc32
		
	End Function '--addcrc--
	'= = = = = = = = = = = =
	
	'//---- At last, we have to write a function so that we
	'---------can get the Crc32 checksum value at any time
	
	Public Function GetCrc32(ByVal Crc32 As Integer) As Integer
		'// Turn on error trapping
		On Error Resume Next
		
		'// Set function to the current Crc32 value
		GetCrc32 = Crc32 Xor &HFFFFFFFF
		
	End Function '--getcrc--
	'= = = = = = =
	
	'--make crc32 hash key--
	'--make crc32 hash key--
	'--make crc32 hash key--
	
	Public Function glMakeHashKey(ByVal sData As String) As Integer
		Dim lngResult As Integer
		Dim lngCrc32Value As Integer
		
		On Error Resume Next
		lngCrc32Value = InitCrc32() '--could supply seed..--
		lngCrc32Value = AddCrc32(sData, lngCrc32Value) '--compute--
		lngResult = GetCrc32(lngCrc32Value)
		
		On Error GoTo 0
		glMakeHashKey = lngResult
		
	End Function '--make key-
	'= = = = = = = = = = =
	
	'-- make jobtracking key..--
	'-- make jobtracking key..--
	'-- make jobtracking key..--
	
	'---  Concat ABN, Server, UserName and RH n digits of ABN..
	'---- - - -  Where "n" if length of username..--
	Public Function gsMakeJobTrackingKey(ByVal sABN As String, ByVal sServer As String, ByVal sUserName As String) As String
		Dim sKeyArg As String
		Dim sABN2 As String
		Dim lngResult As Integer
		
		lngResult = 0
		sABN2 = Replace(sABN, " ", "") '--remove blanks.-
		
		sKeyArg = sABN2 & LCase(sServer) & LCase(sUserName) & Right(sABN2, Len(sUserName))
		If Len(sUserName) > 0 Then
			If gbDebug Then MsgBox("Debug.. calling makeHashKey with arg:" & vbCrLf & sKeyArg)
			lngResult = glMakeHashKey(sKeyArg)
		Else
			'===MsgBox "Can't make Jobtracking key..  invalid UserName..", vbCritical
		End If
		
		gsMakeJobTrackingKey = UCase(Hex(lngResult))
		
	End Function '--jobtracking--
	'= = = = = = = =  =
	
	'-- BUILD-2436 == New Version using SecurityId..-
	'- NOTE: DBName is substituted for Server..
	'----    and SecurityId is substituted for UserName.--
	
	'---  Concat ABN, DBName, SecurityId and RH n digits of ABN..
	'---- - - -  Where "n" is value of last (RH) Digit of SecurityId..--
	Public Function gsMakeJobTrackingKeyV2(ByVal sABN As String, ByVal sDBName As String, ByVal sJT2SecurityId As String) As String
		Dim sKeyArg As String
		Dim sABN2 As String
		Dim lngResult As Integer
		Dim intExtras As Short
		
		lngResult = 0
		gsMakeJobTrackingKeyV2 = ""
		sABN2 = Replace(sABN, " ", "") '--remove blanks.-
		intExtras = 0
		If IsNumeric(Right(sJT2SecurityId, 1)) Then
			intExtras = CShort(Right(sJT2SecurityId, 1))
		End If
		If intExtras <= 0 Then intExtras = 1 '-- 03July2010==
		sKeyArg = sABN2 & LCase(sDBName) & LCase(sJT2SecurityId) & Right(sABN2, intExtras)
		If (Len(sJT2SecurityId) > 0) Then
			If gbDebug Then MsgBox("Debug.. KeyV2 calling makeHashKey with arg:" & vbCrLf & sKeyArg)
			lngResult = glMakeHashKey(sKeyArg)
			gsMakeJobTrackingKeyV2 = UCase(Hex(lngResult))
		Else
			'===MsgBox "Can't make Jobtracking key..  invalid UserName..", vbCritical
		End If
		
	End Function '--jobtracking--
	'= = = = = = = =  =
	
	'--sub "main" disabled--grh--
	
	'// And for testing the routines above...
	'--Public Sub Main()
	
	'--Dim lCrc32Value As Long
	
	'--On Error Resume Next
	'--lCrc32Value = InitCrc32()
	'--lCrc32Value = AddCrc32("This is the original message!", _
	''--   lCrc32Value)
	'--Debug.Print Hex$(GetCrc32(lCrc32Value))
	
	'--End Sub
	
	'= = =end download ===
End Module