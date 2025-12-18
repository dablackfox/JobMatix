
Imports System.IO

Public Class clsCompareFileInfo
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim File1 As FileInfo
        Dim File2 As FileInfo

        File1 = DirectCast(x, FileInfo)
        File2 = DirectCast(y, FileInfo)
        Compare = DateTime.Compare(File1.LastWriteTime, File2.LastWriteTime)
    End Function
End Class  '-clsCompareFileInfo-
'= = = = = = = = = = = = = ==  =
