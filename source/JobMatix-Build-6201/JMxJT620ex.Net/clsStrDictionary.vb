'== Public Class clsStrDictionary--

'--  Created 02-Mar-2012--
'-----  To Replace Scripting.Dictionary COM component..-

'--  NB: BASE Class dictaes that Keys are CASE-SENSITIVE  !!  --

Imports System
Imports System.Collections

Public Class clsStrDictionary
    Inherits DictionaryBase

    Private Const K_MAX_lENGTH_KEY As Integer = 255
    Private Const K_MAX_lENGTH_VALUE As Integer = 4000  '--don't want to exceed SystemInfo Value column size.--

    Default Public Property Item(ByVal key As String) As String
        Get
            Return CType(Dictionary(key), String)
        End Get
        Set(ByVal value As String)
            Dictionary(key) = value
        End Set
    End Property  '--Item--
    '= = = = = = = = = =  ==

    Public ReadOnly Property Keys() As ICollection
        Get
            Return Dictionary.Keys
        End Get
    End Property  '--keys--
    '= = = = = = = = = = = = = = = = = = 

    Public ReadOnly Property Values() As ICollection
        Get
            Return Dictionary.Values
        End Get
    End Property  '--Values--
    '= = = = = = = = = = = = =

    Public Sub Add(ByVal key As String, ByVal value As String)
        Dictionary.Add(key, value)
    End Sub '--Add--
    '= = = = = = = = = = 

    Public Function Contains(ByVal key As String) As Boolean
        Return Dictionary.Contains(key)
    End Function '--  Contains--
    '= = = = = = = = = = = = = = 

    Public Function Exists(ByVal key As String) As Boolean
        Return Dictionary.Contains(key)
    End Function '--  Exists--
    '= = = = = = = = = = = = = = 

    Public Sub Remove(ByVal key As String)
        Dictionary.Remove(key)
    End Sub '--Remove--
    '= = = = = = = = = = = = = = =  = = = =

    Protected Overrides Sub OnInsert(ByVal key As Object, ByVal value As Object)
        If Not GetType(System.String).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type String.", "key")
        Else
            Dim strKey As String = CType(key, String)
            If (strKey.Length > K_MAX_lENGTH_KEY) Then
                Throw New ArgumentException("key must be no more than " & K_MAX_lENGTH_KEY & " characters in length.", "key")
            End If
        End If
        If Not GetType(System.String).IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type String.", "value")
        Else
            Dim strValue As String = CType(value, String)
            If (strValue.Length > K_MAX_lENGTH_VALUE) Then
                Throw New ArgumentException("value must be no more than " & K_MAX_lENGTH_VALUE & " characters in length.", "value")
            End If
        End If
    End Sub 'OnInsert

    Protected Overrides Sub OnRemove(ByVal key As Object, ByVal value As Object)
        If Not GetType(System.String).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type String.", "key")
        Else
            Dim strKey As String = CType(key, String)
            If (strKey.Length > K_MAX_lENGTH_KEY) Then
                Throw New ArgumentException("key must be no more than " & K_MAX_lENGTH_KEY & " characters in length.", "key")
            End If
        End If
    End Sub 'OnRemove

    Protected Overrides Sub OnSet(ByVal key As Object, ByVal oldValue As Object, ByVal newValue As Object)
        If Not GetType(System.String).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type String.", "key")
        Else
            Dim strKey As String = CType(key, String)
            If (strKey.Length > K_MAX_lENGTH_KEY) Then
                Throw New ArgumentException("key must be no more than " & K_MAX_lENGTH_KEY & " characters in length.", "key")
            End If
        End If
        If Not GetType(System.String).IsAssignableFrom(newValue.GetType()) Then
            Throw New ArgumentException("newValue must be of type String.", "newValue")
        Else
            Dim strValue As String = CType(newValue, String)
            If (strValue.Length > K_MAX_lENGTH_VALUE) Then
                Throw New ArgumentException("newValue must be no more than " & K_MAX_lENGTH_VALUE & _
                                                                                       " characters in length.", "newValue")
            End If
        End If
    End Sub 'OnSet

    Protected Overrides Sub OnValidate(ByVal key As Object, ByVal value As Object)
        If Not GetType(System.String).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type String.", "key")
        Else
            Dim strKey As String = CType(key, String)
            If (strKey.Length > K_MAX_lENGTH_KEY) Then
                Throw New ArgumentException("key must be no more than " & K_MAX_lENGTH_KEY & " characters in length.", "key")
            End If
        End If
        If Not GetType(System.String).IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type String.", "value")
        Else
            Dim strValue As String = CType(value, String)
            If (strValue.Length > K_MAX_lENGTH_VALUE) Then
                Throw New ArgumentException("value must be no more than " & K_MAX_lENGTH_VALUE & " characters in length.", "value")
            End If
        End If
    End Sub 'OnValidate 

End Class '--clsStrDictionary--
'= = = = = = = = = = = = = = 

'==Samples== Public Class SamplesDictionaryBase

'==Samples== Public Shared Sub Main()

'==Samples== ' Creates and initializes a new DictionaryBase.
'==Samples== Dim mySSC As New ShortStringDictionary()

'==Samples== ' Adds elements to the collection.
'==Samples==     mySSC.Add("One", "a")
'==Samples==     mySSC.Add("Two", "ab")
'==Samples==     mySSC.Add("Three", "abc")
'==Samples==     mySSC.Add("Four", "abcd")
'==Samples==     mySSC.Add("Five", "abcde")

'==Samples== ' Display the contents of the collection using For Each. This is the preferred method.
'==Samples==     Console.WriteLine("Contents of the collection (using For Each):")
'==Samples==     PrintKeysAndValues1(mySSC)

'==Samples== ' Display the contents of the collection using the enumerator.
'==Samples==     Console.WriteLine("Contents of the collection (using enumerator):")
'==Samples==     PrintKeysAndValues2(mySSC)

'==Samples== ' Display the contents of the collection using the Keys property and the Item property.
'==Samples==     Console.WriteLine("Initial contents of the collection (using Keys and Item):")
'==Samples==     PrintKeysAndValues3(mySSC)

'==Samples== ' Tries to add a value that is too long.
'==Samples==     Try
'==Samples==         mySSC.Add("Ten", "abcdefghij")
'==Samples==     Catch e As ArgumentException
'==Samples==         Console.WriteLine(e.ToString())
'==Samples==     End Try

'==Samples== ' Tries to add a key that is too long.
'==Samples==     Try
'==Samples==         mySSC.Add("Eleven", "ijk")
'==Samples==     Catch e As ArgumentException
'==Samples==         Console.WriteLine(e.ToString())
'==Samples==     End Try

'==Samples==     Console.WriteLine()

'==Samples== ' Searches the collection with Contains.
'==Samples==     Console.WriteLine("Contains ""Three"": {0}", mySSC.Contains("Three"))
'==Samples==     Console.WriteLine("Contains ""Twelve"": {0}", mySSC.Contains("Twelve"))
'==Samples==     Console.WriteLine()

'==Samples== ' Removes an element from the collection.
'==Samples==     mySSC.Remove("Two")

'==Samples== ' Displays the contents of the collection.
'==Samples==     Console.WriteLine("After removing ""Two"":")
'==Samples==     PrintKeysAndValues1(mySSC)

'==Samples== End Sub 'Main


'==Samples== ' Uses the For Each statement which hides the complexity of the enumerator.
'==Samples== ' NOTE: The For Each statement is the preferred way of enumerating the contents of a collection.
'==Samples== Public Shared Sub PrintKeysAndValues1(ByVal myCol As ShortStringDictionary)
'==Samples== Dim myDE As DictionaryEntry
'==Samples==     For Each myDE In myCol
'==Samples==         Console.WriteLine("   {0,-5} : {1}", myDE.Key, myDE.Value)
'==Samples==     Next myDE
'==Samples==     Console.WriteLine()
'==Samples== End Sub 'PrintKeysAndValues1


'==Samples== ' Uses the enumerator. 
'==Samples== ' NOTE: The For Each statement is the preferred way of enumerating the contents of a collection.
'==Samples== Public Shared Sub PrintKeysAndValues2(ByVal myCol As ShortStringDictionary)
'==Samples== Dim myDE As DictionaryEntry
'==Samples== Dim myEnumerator As System.Collections.IEnumerator = myCol.GetEnumerator()
'==Samples==     While myEnumerator.MoveNext()
'==Samples==         If Not (myEnumerator.Current Is Nothing) Then
'==Samples==             myDE = CType(myEnumerator.Current, DictionaryEntry)
'==Samples==             Console.WriteLine("   {0,-5} : {1}", myDE.Key, myDE.Value)
'==Samples==         End If
'==Samples==     End While
'==Samples==     Console.WriteLine()
'==Samples== End Sub 'PrintKeysAndValues2


'==Samples== ' Uses the Keys property and the Item property.
'==Samples== Public Shared Sub PrintKeysAndValues3(ByVal myCol As ShortStringDictionary)
'==Samples== Dim myKeys As ICollection = myCol.Keys
'==Samples== Dim k As String
'==Samples==     For Each k In myKeys
'==Samples==         Console.WriteLine("   {0,-5} : {1}", k, myCol(k))
'==Samples==     Next k
'==Samples==     Console.WriteLine()
'==Samples== End Sub 'PrintKeysAndValues3

'==Samples== End Class '--sample- 


'This code produces the following output.
'
'Contents of the collection (using For Each):
'   Three : abc
'   Five  : abcde
'   Two   : ab
'   One   : a
'   Four  : abcd
'
'Contents of the collection (using enumerator):
'   Three : abc
'   Five  : abcde
'   Two   : ab
'   One   : a
'   Four  : abcd
'
'Initial contents of the collection (using Keys and Item):
'   Three : abc
'   Five  : abcde
'   Two   : ab
'   One   : a
'   Four  : abcd
'
'System.ArgumentException: value must be no more than 5 characters in length.
'Parameter name: value
'   at ShortStringDictionary.OnValidate(Object key, Object value)
'   at System.Collections.DictionaryBase.System.Collections.IDictionary.Add(Object key, Object value)
'   at SamplesDictionaryBase.Main()
'System.ArgumentException: key must be no more than 5 characters in length.
'Parameter name: key
'   at ShortStringDictionary.OnValidate(Object key, Object value)
'   at System.Collections.DictionaryBase.System.Collections.IDictionary.Add(Object key, Object value)
'   at SamplesDictionaryBase.Main()
'
'Contains "Three": True
'Contains "Twelve": False
'
'After removing "Two":
'   Three : abc
'   Five  : abcde
'   One   : a
'   Four  : abcd

'== End Class
