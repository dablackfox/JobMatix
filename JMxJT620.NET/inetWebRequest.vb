Option Strict Off
Option Explicit On
Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
Imports System.Text
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application
'= Imports System.Data
'= Imports System.Data.OleDb

Public Class inetWebRequest


    '-- Web interface to replace INETCAF3.cls..---
    '-- Web interface to replace INETCAF3.cls..---
    '-- Web interface to replace INETCAF3.cls..---

    '-- Derived  FROM msdn Web class examples..--

    '==  grh 3311.404. made into separate class file -
    '==            For new Public SMS functions.
    '==  
    '= = = = = = = = = = = = = = = = = = = = = = = = = = =


    Private mbResponseOpen As Boolean = False

    Private msAuthenticateUserId As String = ""
    Private msAuthenticatePassword As String = ""

    Private msRemoteHost As String
    Private msPostURL As String = ""
    Private msHeaderContentType As String = ""

    Private mPostRequest1 As WebRequest
    Private mResponse1 As WebResponse

    Private msLastExceptionText As String = ""

    '--  property parameters..--
    '--  property parameters..--

    WriteOnly Property authenticateUserId() As String
        Set(ByVal Value As String)
            msAuthenticateUserId = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property authenticatePassword() As String
        Set(ByVal Value As String)
            msAuthenticatePassword = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property remoteHost() As String
        Set(ByVal Value As String)
            msRemoteHost = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property postURL() As String
        Set(ByVal Value As String)
            msPostURL = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    WriteOnly Property ContentType() As String
        Set(ByVal Value As String)
            msHeaderContentType = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = 

    ReadOnly Property LastExceptionText() As String
        Get
            LastExceptionText = msLastExceptionText
        End Get
    End Property '-- get cancelled--
    '= = = = = = = =  = =  ==

    '-- init --
    '-- init --

    Public Sub New()
        MyBase.New()
        '==  Class_Initialize_Renamed()
    End Sub '--initialise..-
    '= = = = = = = = = = = = =

    '--  Public functions..-
    '--  Public functions..-
    '--  Public functions..-

    Public Function inetOpen() As Integer

        inetOpen = -1  '--assume fails..

        Try
            mPostRequest1 = WebRequest.Create(msPostURL)
            Exit Try
        Catch wex As Exception
            msLastExceptionText = wex.Message
            Exit Function
        End Try

        inetOpen = 0  '-ok..-

    End Function  '--open--
    '= = = = = = = = = = = = = = =

    '-  NOT NEEDED..--

    '== Public Function hostConnect() As Integer

    '==     hostConnect = 0  '--ok--
    '== End Function   '--connect-
    '= = = = = = = = = = = = = = 


    '-- PostData  -

    Public Function PostData(ByVal sHttpText As String, _
                                  ByRef strResultText As String) As Integer

        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(sHttpText)
        Dim dataStream As Stream '== = request.GetRequestStream()

        PostData = -1
        msLastExceptionText = ""

        '-- Set the Method property of the request to POST.
        mPostRequest1.Method = "POST"
        mPostRequest1.ContentType = msHeaderContentType
        Try
            dataStream = mPostRequest1.GetRequestStream()
        Catch wex1 As Exception
            msLastExceptionText = "Error in Post fn (GetRequestStream)" & vbCrLf & wex1.Message
            Exit Function
        End Try
        '-- Write the data to the request stream.
        Try
            dataStream.Write(byteArray, 0, byteArray.Length)
        Catch wex2 As Exception
            msLastExceptionText = "Error in Post fn (dataStream.Write)" & vbCrLf & wex2.Message
            Exit Function
        End Try
        '-- Close the Stream object.
        dataStream.Close()
        mbResponseOpen = True
        '--ok..  do it.
        Try
            mResponse1 = mPostRequest1.GetResponse()
            Exit Try
        Catch wex3 As Exception
            msLastExceptionText = "Error in Post fn (GetResponse)" & vbCrLf & wex3.Message
            Exit Function
        End Try

        '-- Get the stream containing content returned by the server.
        dataStream = mResponse1.GetResponseStream()
        '-- Open the stream using a StreamReader for easy access.
        Dim reader As New StreamReader(dataStream)
        '-- Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()

        '-- Display the content.
        '== Console.WriteLine(responseFromServer)
        strResultText = responseFromServer

        ' Clean up the streams.
        reader.Close()
        dataStream.Close()
        '== mResponse1.Close()
        PostData = 0  '--ok.-

    End Function  '--PostData-
    '= = = = = = = = = = = = = 

    '--Q u e r i e s..--
    '--Q u e r i e s..--
    '--Q u e r i e s..--

    '--queryRequestHeaders-
    Public Function queryRequestHeaders() As String

        queryRequestHeaders = ""

    End Function  '--querySRequestHeaders--
    '= = = = = = = = = = = = = = = =


    Public Function queryStatusCode() As String

        queryStatusCode = ""
        If Not (mResponse1 Is Nothing) Then
            queryStatusCode = CType(mResponse1, HttpWebResponse).StatusCode
        End If

    End Function  '--queryStatusCode--
    '= = = = = = = = = = = = = = = =

    '--queryStatusText--
    Public Function queryStatusText() As String

        queryStatusText = "--"
        If Not (mResponse1 Is Nothing) Then
            queryStatusText = CType(mResponse1, HttpWebResponse).StatusDescription
        End If
    End Function  '--queryStatusText--
    '= = = = = = = = = = = = = = = =

    '= '--hostDisConnect-
    '== Public Function hostDisConnect() As Integer

    '==     hostDisConnect = 0  '--ok--
    '== End Function   '--disconnect-
    '= = = = = = = = = = = = = = 

    '--  Close.--
    Public Function inetClose() As Integer

        If mbResponseOpen Then mResponse1.Close()

        inetClose = 0  '--ok--
    End Function   '--close-
    '= = = = = = = = = = = = = = 



    '== '==  Original example..--
    '== Public Shared Sub Main_renamed()
    '== ' Create a request using a URL that can receive a post. 
    '= =Dim request As WebRequest = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ")
    '== ' Set the Method property of the request to POST.
    '==     request.Method = "POST"
    '== ' Create POST data and convert it to a byte array.
    '== Dim postData As String = "This is a test that posts this string to a Web server."
    '== Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
    '== ' Set the ContentType property of the WebRequest.
    '==     request.ContentType = "application/x-www-form-urlencoded"
    '== ' Set the ContentLength property of the WebRequest.
    '==     request.ContentLength = byteArray.Length
    '== ' Get the request stream.
    '== Dim dataStream As Stream = request.GetRequestStream()
    '== ' Write the data to the request stream.
    '==     dataStream.Write(byteArray, 0, byteArray.Length)
    '== ' Close the Stream object.
    '==     dataStream.Close()
    '== ' Get the response.
    '== Dim response As WebResponse = request.GetResponse()
    '== ' Display the status.
    '==     Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
    '== ' Get the stream containing content returned by the server.
    '==     dataStream = response.GetResponseStream()
    '== ' Open the stream using a StreamReader for easy access.
    '== Dim reader As New StreamReader(dataStream)
    '== ' Read the content.
    '== Dim responseFromServer As String = reader.ReadToEnd()
    '== ' Display the content.
    '==     Console.WriteLine(responseFromServer)
    '== ' Clean up the streams.
    '==     reader.Close()
    '==     dataStream.Close()
    '==     response.Close()
    '== End Sub


End Class  '-inetWebRequest
