Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb

Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net.Security
Imports System.ComponentModel
Imports System.Text
Imports System.Xml
Imports System.Threading

Public Class frmEmailMain

    '==
    '== grh RAs v3.4.3401.0923  23Sep2017-
    '==      >> Cloned and Re-build as POS Emailer..
    '==
    '==   >> 3411.0106 -- 06-Jan-2018== .
    '==                -- Add CustomerName to XML... 
    '==   >> 3411.0127 -- 27-Jan-2018== .
    '==                -- Tidy up..  Add Checkbox to Confirm all SEND's ... 
    '==
    '==   >> 3411.0307=  07-Mar-2018=
    '==        --  Email Agent-   "Automate" Sending Emails.
    '==            ie Add Checkboxes to each row to select which to send..  
    '==                   Add Form button- "Send All Selected", and Pause..
    '==
    '==
    '==   >> 3431.0622=  22-June-2018=
    '==    -- Catch content erors on Email XML Files for reserved chars (eg ampersand,<, > )...
    '==
    '==
    '==   Updated.- 3519.0319  Started 14-March-2019= 
    '==    --  Email Queue. Put Try/catch around the SendAll loop to catch error..
    '==
    '= = = = = = = = = =
    '==
    '==   New Build 4233.0421   19-April-2020..
    '==    
    '==   1.  frmEmailmain- disable the event "dgvEmailList_SelectionChanged" because it is causing the crash that
    '==                      comes after sending a single email because the grid is empty.
    '-- IF Grid is empty, then getting CURRENT CELL crashes with NULL Object ref.--
    '-- IF Grid is empty, then getting CURRENT CELL crashes with NULL Object ref.--
    '=  CRASHES if Grid is empty.>>  Dim intRowIndex = dgvEmailList.CurrentCell.RowIndex
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    ''= = = = = = = = =  = = = =
    Private mbIsInitialising As Boolean = True

    '-end import-

    Private mbActivated As Boolean = False
    Private mbStartupDone As Boolean = False

    '= Private mColQuote As Collection
    '= Private mColQuoteLines As Collection '--of collections (lines..)==

    Private mCnnSql As OleDbConnection '== ADODB.Connection
    '== Private mCnnShape As ADODB.Connection

    Private msServer As String
    Private msSqlVersion As String
    Private msInputDBName As String

    Private mbIsAdminTest As Boolean = False

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  jobs DB info--

    Dim msComputerName As String '--local machine--
    Dim msAppPath As String
    '====Private msSaveJetPath As String
    Private msInstallPath As String
    Private msJobMatixVersion As String = "JobMatix"

    Private msDllversion As String = ""

    Private msDocTableName As String
    '-- Results box position..-
    '= Private mlResultsTop As Integer = 84
    '= Private mlResultsLeft As Integer = 8

    Private mlFormDesignHeight As Integer = 760  '-- starting dimensions..-
    Private mlFormDesignWidth As Integer = 998   '-- starting dimensions..-

    '==IMPORT+ vars-

    Private msBusinessShortname As String = ""

    'Private mLngSelectedRow As Integer '--selected browse row..-
    'Private mLngSelectedRowRAs As Integer '--selected browse row..-
    'Private mLngSelectedRowSupplier As Integer

    'Private mLngRAsTreeBGColour As Integer
    'Private mLngGridBGColourRAs As Integer

    Private msSettingsPath As String = ""
    '==3311.305=
    Private mLocalSettings1 As clsLocalSettings
    '=3311.305=
    Private mSysInfo1 As clsSystemInfo

    '--staff--
    Private mIntStaff_Id As Integer = -1
    Private msStaffBarcode As String = ""
    Private msStaffName As String = ""
    Private mlStaffTimeout As Integer = 1 '--Not used in this class now...--

    Private msCurrentUser As String
    Private mbIsSqlAdmin As Boolean


    Private mbIsPosOnly As Boolean = False  '-RAs34-

    Private msDefaultPrinterName As String = ""
    Private msEmailQueueSharePath As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = =  =

    '-- Mail stuff from JobMatix..-

    '--SAVE original SMTP Mail SystemInfo Settings..
    Private msSMTPHostName As String = ""
    Private mIntSMTPHostPort As Integer = -1
    Private mbSMTPHostUsesSSL As Boolean = False

    Private msSMTPUsername As String = ""
    Private msSMTPPassword As String = ""

    'Private Shared mbSendingMail As Boolean = False
    'Private Shared mbMailSent As Boolean = False
    '=Private Shared mbMailCancelled As Boolean = False

    '= Private Shared WithEvents mSmtpClient1 As SmtpClient
    '= Private Shared mMessage As MailMessage

    '= Private mMailFrom As MailAddress
    '= Private Shared msSMTPResultMsg As String = ""
    '= = = = = = = = = = = = = =  = = = = = = = = = 

    Private mIntSelectedRow As Integer = -1
    '- current selection-
    'Private mIntDoc_id As Integer = -1
    'Private mByteRetrievedFile As Byte()
    'Private msCurrentFileTitle As String = ""
    'Private mCurrentBinaryData As Byte()
    'Private msCurrentFileSuffix As String = ""

    '= = = = = = = = = = = = = = = = = = = = = = = = =  = = = =
    Private mbGridIsRefreshing As Boolean = False

    '-- EMAILER-
    Private clsJmxEmail1 As clsJmxEmail

    '- Robotics..
    Private mbPauseTheSending As Boolean = False

    '= = = = = = = = = = = = = = = = = = = = = = = = ==  == = = 
    '-===FF->

    '--  Input Properties..--
    '-    From sub Main -- ===

    '--Properties as input parameters--


    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connection() As OleDbConnection '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property
    '- - - - - - - - - -

    '--accept full table/cols description for browsed table--
    '--accept full table/cols description for browsed table--

    WriteOnly Property colTables() As Collection
        Set(ByVal Value As Collection)

            mColSqlDBInfo = Value

        End Set
    End Property
    '- - - - - -
    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
            msInputDBName = Value
        End Set
    End Property
    '- = = = = = = = = = = = =  = == =  =

    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffBarcode() As String
        Set(ByVal Value As String)
            msStaffBarcode = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '-admin for test-
    WriteOnly Property IsAdminTest As Boolean
        Set(value As Boolean)
            mbIsAdminTest = value
        End Set
    End Property  '-test-
    '= = = = = = = = = = = = = = = = = = =

    WriteOnly Property DllVersion As String
        Set(value As String)
            msDllversion = value
        End Set
    End Property
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- Add msg to txtReport.text--

    Private Function mbReport(ByVal sMsg As String) As Boolean

        txtReport.AppendText(sMsg & vbCrLf)
        txtReport.Focus()
        txtReport.SelectionStart = txtReport.TextLength
        txtReport.SelectionLength = 0
        '== txtReport.Select()

    End Function  '-report-
    '= = = = = = = = = = = = = = =


    '-- lookup RM Staff to given BARCODE.--

    Private Function mbLookupStaff(ByRef sBarcode As String, _
                                    ByRef colRecord As Collection) As Boolean
        '= Dim sBarcode As String
        Dim sSql, s1 As String
        Dim colResult As Collection

        mbLookupStaff = False
        '--staff Signon..--
        '== Must be JobMatix POS lookup..

        sSql = "SELECT * FROM [staff] WHERE (barcode='" & sBarcode & "');"
        If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                               (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
            '--have a row..-
            colRecord = colResult.Item(1)
            '=msSaleStaffbarcode = sBarcode
            mbLookupStaff = True
        Else '--not found..-
            '= EventArgs.Cancel = True
            MsgBox("No Staff found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
        End If  '-get--

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

    End Function '--staff lookup..--
    '= = = = = = = = = = = = = =
    '-===FF->

    ''' <summary>
    ''' Converts the DATA File to array of Bytes
    ''' Thanks to:
    '''     http://www.codeproject.com/Articles/31921/Convert-Image-File-to-Bytes-and-Back  
    ''' </summary>
    ''' <param name="ImageFilePath">The path of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function gabConvertDataFiletoBytes(ByVal DataFilePath As String) As Byte()
    '    Dim _tempByte() As Byte = Nothing
    '    If String.IsNullOrEmpty(DataFilePath) = True Then
    '        Throw New ArgumentNullException("Data File Name Cannot be Null or Empty", "DataFilePath")
    '        Return Nothing
    '    End If
    '    Try
    '        Dim _fileInfo As New IO.FileInfo(DataFilePath)
    '        Dim _NumBytes As Long = _fileInfo.Length
    '        Dim _FStream As New IO.FileStream(DataFilePath, IO.FileMode.Open, IO.FileAccess.Read)
    '        Dim _BinaryReader As New IO.BinaryReader(_FStream)
    '        _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
    '        _fileInfo = Nothing
    '        _NumBytes = 0
    '        _FStream.Close()
    '        _FStream.Dispose()
    '        _BinaryReader.Close()
    '        Return _tempByte
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    '= = = = = = = = = = = =
    '-===FF->

    '-CertificateValidationCallBack-

    'Private Function My_CertificateValidationCallBack(sender As Object, _
    '                                            certificate As System.Security.Cryptography.X509Certificates.X509Certificate, _
    '                                             chain As System.Security.Cryptography.X509Certificates.X509Chain, _
    '                                               sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean

    '    '-- // If the certificate is a valid, signed certificate, return true.
    '    If (sslPolicyErrors = System.Net.Security.SslPolicyErrors.None) Then

    '        Return True
    '    End If
    '    '-- // If there are errors in the certificate chain, look at each error to determine the cause.
    '    If ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) <> 0) Then

    '        If (Not IsDBNull(chain)) AndAlso (Not IsDBNull(chain.ChainStatus)) Then
    '            '=  (chain <> null And chain.ChainStatus <> null) Then

    '            For Each status As System.Security.Cryptography.X509Certificates.X509ChainStatus In chain.ChainStatus
    '                If ((certificate.Subject = certificate.Issuer) And
    '                   (status.Status = System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot)) Then
    '                    '-// Self-signed certificates with an untrusted root are valid. 
    '                    Continue For
    '                Else
    '                    '=  {
    '                    If (status.Status <> System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError) Then
    '                        '- // If there are any other errors in the certificate chain, the certificate is invalid,
    '                        '- // so the method returns false.
    '                        Return False
    '                    End If
    '                End If
    '            Next status
    '            '--// When processing reaches this line, the only errors in the certificate chain are 
    '            '--// untrusted root errors for self-signed certificates. These certificates are valid
    '            '-- // for default Exchange server installations, so return true.
    '            '=-TEST =
    '            '== MsgBox("We are accepting a self-signed Server Certificate.", MsgBoxStyle.Exclamation)
    '            Return True
    '        Else
    '            '-- // In all other cases, return false.
    '            Return False
    '        End If
    '    End If  '-chain null-

    'End Function  '- CertificateValidationCallBack-
    '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->

     '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->

    '-- Refresh all settings..--
    '-- COLLECT Mail settings for EMAILER...-
    Private mColMailSettings As Collection


    Private Function mbRefreshAllSMTPSettings() As Boolean
        '= Dim col1 As Collection
        Dim s1, sValue As String

        mbRefreshAllSMTPSettings = False
        '-- get systemInfo stuff..--
        '--  refresh SMS messages..
        '--save settings.--
        '==3311.728= Must find chosen Gateway Host-

        '--  load SMTP Mail SystemInfo Settings..
        '--  SMTPServer     --
        '--  SMTPUsername   --
        '--  SMTPPassword   --
        msSMTPHostName = ""
        mIntSMTPHostPort = -1

        For Each sKey1 As String In mSysInfo1.keys  '= col1 In mColSystemInfo
            sValue = Trim(mSysInfo1.item(sKey1))
            If LCase(sKey1) = "smtphostname" Then '--standard message text..-
                msSMTPHostName = sValue  '= col1.Item("systemvalue")
            ElseIf LCase(sKey1) = "smtphostport" Then '--standard message text..-
                mIntSMTPHostPort = IIf(IsNumeric(sValue), CInt(sValue), -1)
            ElseIf LCase(sKey1) = "smtphostusesssl" Then '--standard message text..-
                mbSMTPHostUsesSSL = IIf(UCase(VB.Left(sValue, 1)) = "Y", True, False)
            ElseIf LCase(sKey1) = "smtpusername" Then '--standard message text..-
                msSMTPUsername = sValue  '= col1.Item("systemvalue")
            ElseIf LCase(sKey1) = "smtppassword" Then '--standard message text..-
                msSMTPPassword = sValue  '=  col1.Item("systemvalue")
            ElseIf LCase(sKey1) = "businessshortname" Then '--standard message text..-
                msBusinessShortname = Replace(sValue, "_", "")
            End If
        Next sKey1 '=col1 '--col1..-
        '== txtEmailFrom.Text = mMailFrom.DisplayName & "<" & mMailFrom.Address & ">"
        '-- make settings collection  for emailer..
        mColMailSettings = New Collection
        mColMailSettings.Add(msSMTPHostName, "smtphostname")
        mColMailSettings.Add(mIntSMTPHostPort, "smtphostport")
        mColMailSettings.Add(mbSMTPHostUsesSSL, "smtphostusesssl")
        mColMailSettings.Add(msSMTPUsername, "smtpusername")
        mColMailSettings.Add(msSMTPPassword, "smtppassword")
        mColMailSettings.Add(msBusinessShortname, "businessshortname")


        labSMTPHost.Text = msSMTPHostName & ":" & mIntSMTPHostPort
        If mbSMTPHostUsesSSL Then
            labSMTPHost.Text = labSMTPHost.Text & vbCrLf & "(Using SSL)"
        End If

        '=labStatus.Text = ""
        Call mbReport("Refeshing SMTP Settings..")

        If (msSMTPHostName = "") Or (mIntSMTPHostPort <= 0) Or (msSMTPUsername = "") Then
            '= cmdSendEmail.Visible = False
            Call mbReport(vbCrLf & "ERROR- SMTP/Email Details are not complete..")
        Else  '-ok.-
            '==   v3.4.3403.0531 -- 31may2017= x-
            '==         -- Fixes to (frmNotifyCust) SendMail to capture InvalidCertificate Callback...

            ''-- setup Callback..
            'ServicePointManager.ServerCertificateValidationCallback = _
            '                    New RemoteCertificateValidationCallback(AddressOf My_CertificateValidationCallBack)

            ''- now we can create client..
            'mSmtpClient1 = New SmtpClient(msSMTPHostName, mIntSMTPHostPort)
            ''--username-
            'mMailFrom = New MailAddress(msSMTPUsername, msBusinessShortname)
            'txtEmailFrom.Text = msBusinessShortname & "  <" & msSMTPUsername & ">"
        End If

        '-- get email standard texts..

        mbRefreshAllSMTPSettings = True
    End Function  '--mbRefreshAll--
    '= = = = = = =  = = = = = = = =
    '-===FF->

    '--mbRefreshEmailGrid-
    '-- Each PDF to be emailed is covered by an XML file with Email-Info--
    '-- eg.

    '= <Pos-doc-descriptor> 
    '=    <doc-email-to> sandra@pinnata.com.au
    '=    </doc-email-to> 

    '=    <doc-subject> Sale- invoice No:4  Dated :25-Dec-2017
    '=    </doc-subject> 

    '=    <doc-emailtext> Re:Sale- invoice No:4  Dated :25-Dec-2017
    '=        Dear Sandra Watson
    '=        Please find attached your invoice as per above.
    '=        Thank You..
    '=        JMx Clearwater Pty Ltd
    '=   </doc-emailtext> 

    '=   <doc-file-title> Invoice-4_Cust-3.pdf
    '=   </doc-file-title> 

    '= </Pos-doc-descriptor> 

    Private mbLoadingGrid As Boolean = False

    Private Function mbRefreshEmailGrid() As Boolean
        Dim directory1 As DirectoryInfo
        Dim aFiles() As FileInfo
        Dim xmlReader1 As XmlReader
        Dim dtFileDate As DateTime
        Dim sXmlFileFullPath As String
        '-xml stuff-
        Dim settings As New XmlReaderSettings()
        Dim sRecipient, sSubject, sEmailText As String
        Dim sTargetName As String  '-customer name..
        Dim sAttachmentFileTitle As String
        Dim sTextMsg, sName, sContent As String
        Dim bStartedOk As Boolean
        Dim gridRow1 As DataGridViewRow
        Dim rx As Integer

        mbRefreshEmailGrid = False
        mbLoadingGrid = True
        DoEvents()

        settings.ConformanceLevel = ConformanceLevel.Fragment
        settings.IgnoreWhitespace = True
        settings.IgnoreComments = True
        Try
            directory1 = New DirectoryInfo(msEmailQueueSharePath)
        Catch ex As Exception
            MsgBox("Failed to get EmailQueueSharePath Info..", MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        '--het list of files.
        aFiles = directory1.GetFiles("*.xml")

        '-- SORT by date modified..
        '== I’ve been forced in to using vb.net for a windows service project which scans a folder of xml 
        '== files, they need to be processed in order of the files modified date & time.  Directory.GetFiles() 
        '== returns an array of filenames in alphabetic order, I could find precious little information on the 
        '== net so I thought I’d share what I came up with :-
        '==  http://geekswithblogs.net/ntsmith/archive/2006/08/17/88250.aspx
        Array.Sort(aFiles, New clsCompareFileInfo)

        'For Each fl As FileInfo In aFiles
        '    MsgBox(fl.FullName.ToString() & "; " & fl.LastWriteTime.ToString, MsgBoxStyle.Information)
        'Next

        '-- Ckear the Grid..
        dgvEmailList.Rows.Clear()

        For Each file1 As FileInfo In aFiles
            sXmlFileFullPath = msEmailQueueSharePath & "\" & file1.Name
            dtFileDate = File.GetLastWriteTime(sXmlFileFullPath)

            '= MsgBox("Found: " & sFileFullPath & vbCrLf & _
            '=                  "Last Modified: " & Format(dtFileDate, "dd-MMM-yyyy hh:mm tt"), MsgBoxStyle.Information)

            '-- Get email info from xml file and load to Grid..
            '-- don't process if truncated-
            If (file1.Length <= 0) Then
                Continue For  '--got to next-
            End If

            Try
                xmlReader1 = XmlReader.Create(sXmlFileFullPath, settings)
            Catch ex As Exception
                MsgBox("Failed to Create XML reader for file- " & vbCrLf & _
                         "'" & sXmlFileFullPath & "' " & vbCrLf & ex.Message & vbCrLf & _
                       vbCrLf & "Make sure the Emailer is not running on another system..", MsgBoxStyle.Exclamation)
                Continue For '= Exit Function
            End Try
            sTextMsg = ""
            sRecipient = "" : sSubject = "" : sEmailText = ""
            sTargetName = ""
            bStartedOk = False
            Try
                While xmlReader1.Read()
                    If xmlReader1.IsStartElement() Then
                        If xmlReader1.IsEmptyElement Then
                            sTextMsg &= xmlReader1.Name & vbCrLf '= Console.WriteLine("<{0}/>", xmlReader1.Name)
                        Else
                            sName = xmlReader1.Name
                            sTextMsg &= sName & vbCrLf '= Console.Write("<{0}> ", xmlReader1.Name)
                            If (LCase(sName) = "pos-doc-descriptor") Then
                                bStartedOk = True   ''-- read to first inside.
                            ElseIf bStartedOk Then  '-into the subs..
                                xmlReader1.Read() ' Read the start tag.
                                If xmlReader1.IsStartElement() Then ' Handle nested elements.
                                    sTextMsg &= xmlReader1.Name & vbCrLf '=   Console.Write(vbCr + vbLf + "<{0}>", xmlReader1.Name)
                                End If
                                '-- Read the text content of the element.
                                sContent = Trim(xmlReader1.ReadString)  '= & vbCrLf
                                sTextMsg &= sContent & vbCrLf  '= Console.WriteLine(xmlReader1.ReadString()) 
                                Select Case LCase(sName)
                                    Case "doc-email-to-name"
                                        sTargetName = sContent
                                    Case "doc-email-to-address"
                                        sRecipient = sContent
                                    Case "doc-subject"
                                        sSubject = sContent
                                    Case "doc-emailtext"
                                        sEmailText = sContent
                                    Case "doc-file-title"
                                        sAttachmentFileTitle = sContent
                                    Case Else
                                End Select
                            Else  '-wrong-  no opening tag.

                            End If
                        End If  '-empty-
                    End If  '-start element=
                End While  '-read-
            Catch ex As Exception
                MessageBox.Show("ERROR- XML reader reports an error for file- " & vbCrLf & _
                         "'" & sXmlFileFullPath & "' " & vbCrLf & ex.Message & vbCrLf & _
                              vbCrLf & "This xml file will be renamed with error suffix..", _
                               "Emailer..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                xmlReader1.Close()
                Try
                    My.Computer.FileSystem.RenameFile(sXmlFileFullPath, file1.Name & ".Error")
                Catch ex2 As Exception
                    MessageBox.Show("ERROR- FAILED to rename file- " & vbCrLf & _
                             "'" & sXmlFileFullPath & "' " & vbCrLf & ex2.Message & vbCrLf & _
                                  vbCrLf & "This xml file should be deleted by operator..", _
                                   "Emailer..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
                Continue For '= Exit Function
            End Try
            xmlReader1.Close()
            'MsgBox("Test XML text is :" & vbCrLf & sTextMsg, MsgBoxStyle.Information)
            'MsgBox("Recipient is :" & sRecipient & vbCrLf & _
            '        "Subject is: " & sSubject & vbCrLf & _
            '        "Emailtext is: " & sEmailText & vbCrLf & _
            '        "Attachment is: " & sAttachmentFileTitle & vbCrLf, MsgBoxStyle.Information)
            '-- Add to Grid..
            gridRow1 = New DataGridViewRow
            dgvEmailList.Rows.Add(gridRow1)
            rx = dgvEmailList.Rows.Count - 1  '--last row -
            With dgvEmailList.Rows(rx)
                .Height = 60
                .MinimumHeight = 60
                .Cells("doc_email_target").Value = sTargetName & vbCrLf & sRecipient
                .Cells("doc_date_created").Value = Format(dtFileDate, "dd-MMM-yyyy")
                .Cells("doc_subject").Value = sSubject
                .Cells("doc_email_text").Value = sEmailText
                .Cells("doc_file_title").Value = sAttachmentFileTitle
                .Cells("xml_file_path").Value = sXmlFileFullPath
                .Cells("doc_recipient").Value = sRecipient  '-email adrress bare..
                .Cells("doc_markToSend").Value = False
                .Cells("doc_markToSend").ReadOnly = False
            End With  '-dgvEmailList-
        Next file1

        dgvEmailList.ClearSelection()
        mbLoadingGrid = False
        DoEvents()

        '= xmlReader1.dispose()
        mbRefreshEmailGrid = True

    End Function  '-mbRefreshEmailGrid-
    '= = = = = = =  = = = = = = = =
    '-===FF->

    '--  NB:  Input properties are now available from sub Main... 
    '--    Load Event fires only when caller issues "Show" for the form..--

    Private Sub frmEmailMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim iPos, ix, L1, lngError As Integer
        Dim s1 As String
        Dim sName, sErrors As String
        '= Dim sCmdLine As String
        Dim v1 As Object

        mlFormDesignHeight = Me.Height
        mlFormDesignWidth = Me.Width
        Call CenterForm(Me)
        '= panelEmailDetail.Enabled = False
        btnRefresh.Enabled = False
        chkConfirmSends.Checked = True
        chkConfirmSends.Enabled = False

        btnSendAllMarked.Enabled = False
        btnCancelSend.Enabled = False
        btnPauseSending.Enabled = False

        '= cmdSendEmail.Enabled = False
        msDocTableName = "DocArchive"
        If Not mbIsAdminTest Then
            '= btnFindSave.Visible = False
        End If
        txtReport.Text = ""

        Try
            mbStartupDone = False
            msComputerName = System.Environment.MachineName

            '= gsErrorLogPath = gsJobMatixLocalDataDir("JobMatix34") & "\JobMatix34RAs-Runtime-" & VB.Left(s1, 7) & ".log"
            '= gsRuntimeLogPath = gsErrorLogPath  '--gsAppPath & "JTv3_Runtime.log"

            '== Call gbLogMsg(gsRuntimeLogPath, "=== JobMatix34Email Main form is loading..")

            msJobMatixVersion = "JobMatixPOSex34-  v" & CStr(My.Application.Info.Version.Major) & "." & _
                                      My.Application.Info.Version.Minor & "; Build: " & _
                                    My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
            labVersion.Text = msDllversion '= msJobMatixVersion
            Me.Text = msJobMatixVersion

            LabToday.Text = VB6.Format(CDate(DateTime.Today), "ddd dd-mmm-yyyy")
            LabServer.Text = msServer & vbCrLf & msSqlDbName

            grpBoxMain.Text = ""
            labEmailTimer.Text = ""

            '==3311= SysInfo. use class instance.
            mSysInfo1 = New clsSystemInfo(mCnnSql)

            '=3403.1009- Server Share Path for Email Queue.
            msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")
            txtQueuePath.Text = msEmailQueueSharePath

            If mSysInfo1.exists("is_pos_only") Then
                mbIsPosOnly = (LCase(VB.Left(mSysInfo1.item("is_pos_only"), 1)) = "y")
            End If

            Exit Sub

        Catch ex As Exception
            MsgBox("ERROR in frmRAs34Main_Load- " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Me.Close()
        End Try  '-main try-

    End Sub  '-load-
    '= = = = = = = = = === == 
    '-===FF->

    '--Activated-

    Private Sub frmEmailMain_Activated(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Dim colRecord As Collection
        Dim pd1 As New PrintDocument()
        Dim s1 As String

        If mbActivated Then Exit Sub
        mbActivated = True
        '--  lookup staff name.--

        If mbLookupStaff(msStaffBarcode, colRecord) Then
            msStaffName = colRecord.Item("docket_name")("value")
            mIntStaff_Id = CInt(colRecord.Item("staff_id")("value"))
            txtStaff.Text = msStaffBarcode & "/" & msStaffName
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("No staff record for barcode: '" & msStaffBarcode & "'..", MsgBoxStyle.Exclamation)
            mCnnSql.Close()
            '== mRetailHost1.closeConnection()
            Me.Close() '==End '==Unload Me
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '= LabBusiness.Text = msBusinessName

        Call mbReport(" Done..")
        DoEvents()  '--let it settle..

        Call mbRefreshAllSMTPSettings()
        '- check settings..-
        If (msSMTPHostName = "") Or (mIntSMTPHostPort <= 0) Or (msSMTPUsername = "") Then
            '= cmdSendEmail.Visible = False
            MsgBox("Note- The SMTP/Email Setup Details are not complete.." & vbCrLf & _
                     "Go to the POS Setup Form and complete the SMTP Mail setup.." & vbCrLf & vbCrLf & _
                      "Then you can re-run this form..", MsgBoxStyle.Information)
            Me.Close()
            Exit Sub
        End If

        '-- check Queue path..
        If (msEmailQueueSharePath = "") Then
            MsgBox("EmailQueueSharePath not defined in POS setup.", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub
        End If
        '--POS--  Make MAIL class instance..
        '=Private clsJmxEmail1 As clsJmxEmail

        labStatus.Text = ""  '-- keep this for the Email class.--

        clsJmxEmail1 = New clsJmxEmail(mColMailSettings, labStatus)

        Call mbReport(labStatus.Text & vbCrLf & " Startup Done..")
        btnCancelSend.Enabled = False
        btnRefresh.Enabled = True

        If Not mbRefreshEmailGrid() Then
            Me.Close()
            Exit Sub
        End If
        mbStartupDone = True
        mbIsInitialising = False
    End Sub  '-activated=
    '= = = = = = = = = == =

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Call mbRefreshEmailGrid()

    End Sub  '-Refresh--
    '= = = = = = = = =  =
    '-===FF->

    '-- SEND Selected eMail..--

    Private Function mbSendSelectedEmail(ByVal intGridRowIndex As Integer, _
                                          ByVal bSendingOneOnly As Boolean, _
                                          ByRef bWasDeleted As Boolean) As Boolean


        Dim sTarget, sRecipient, sSubject, sEmailText, sXmlFilePath As String
        Dim sFileTitle, sAttachmentPath As String
        Dim sColumnName As String
        Dim msgBoxResult1 As MsgBoxResult

        mbSendSelectedEmail = False
        bWasDeleted = False

        '- get email meta stuff..
        With dgvEmailList.Rows(intGridRowIndex)
            sTarget = .Cells("doc_email_target").Value  '--Name + email address.
            sRecipient = .Cells("doc_recipient").Value
            sSubject = .Cells("doc_subject").Value
            sEmailText = .Cells("doc_email_text").Value
            sFileTitle = Trim(.Cells("doc_file_title").Value)
            '= .Cells("xml_file_path").Value = sXmlFileFullPath
            sXmlFilePath = Trim(.Cells("xml_file_path").Value)
        End With '- dgvEmailList-
        sAttachmentPath = ""
        '-- Attachment (pdf )..
        If (sFileTitle <> "") Then
            sAttachmentPath = msEmailQueueSharePath & "\" & sFileTitle
        End If

        If chkConfirmSends.Checked AndAlso bSendingOneOnly Then  '-confirm..
            If (MsgBox("OK to send this Email: " & vbCrLf & sSubject & _
                         vbCrLf & vbCrLf & "To: " & vbCrLf & _
                           sTarget & vbCrLf & vbCrLf & _
                            "  Click Yes to continue SEND..", _
                     MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                Exit Function
            End If  '-yes-
        End If '-confirm..
        Call mbReport("Sending to: " & sTarget & vbCrLf & sSubject)
        '-- Lock the XML file to serialise against another Emailer task..
        '-- When email sent, truncate it and delete it..
        Try '-fsXml-
            Using fsXml As FileStream = File.Open(sXmlFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                mbMailCancelled = False
                '-- send and wait for completion..
                If Not clsJmxEmail1.SendEmail(sRecipient, sSubject, sEmailText, sAttachmentPath) Then
                    MsgBox("Failed to initiate Email sending..", MsgBoxStyle.Exclamation)
                    mbSendMailButtonLocked = False
                    Exit Function
                End If

                '- started ok..  Wait for completion.
                btnCancelSend.Enabled = True

                Dim intStart, intLast As Integer
                Dim sErrorText As String = ""

                '== WAIT 60 secs= --
                intStart = CInt(VB.Timer()) '--starting seconds.-
                intLast = CInt(VB.Timer()) '--starting seconds.-
                While (Not clsJmxEmail1.SendCompleted(sErrorText)) And _
                                         (CInt(VB.Timer()) < intStart + 60)  '--  VB.Timer returns a DOUBLE..-
                    '--  integral part is seconds..
                    If (intLast <> CInt(VB.Timer())) Then  '--update display every second..-
                        '= labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
                        labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
                        intLast = CInt(VB.Timer()) '--starting seconds.-
                    End If
                    System.Windows.Forms.Application.DoEvents()
                End While  '-completed..
                '=Call mSubUpdateStatus("", True)  '- and clear.'=mlabEmailTimer.Text = ""

                '= cmdCancelEmail.Enabled = False
                If (Not clsJmxEmail1.SendCompleted(sErrorText)) Then  '--timed out..-
                    '= mSmtpClient1.SendAsyncCancel()
                    clsJmxEmail1.cancelEmail()

                    '==mbMailCancelled = True
                    Call mbReport("Send operation timed out..")
                    MsgBox("Send operation timed out.." & vbCrLf & vbCrLf & _
                                 "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
                    fsXml.Close()
                Else  '-completed-
                    If mbMailCancelled Then  '== sErrorText <> "" Then
                        '--manual cancel...-
                        '==MsgBox(msSMTPResultMsg, MsgBoxStyle.Information)
                        '= labStatus.Text = msSMTPResultMsg
                        Call mbReport(" ** Send Message was cancelled.")
                    ElseIf (sErrorText <> "") Then    '-error-
                        MsgBox("ERROR in Send: " & vbCrLf & sErrorText & vbCrLf & vbCrLf & _
                                     "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
                        Call mbReport("** Error in Send.")
                        fsXml.Close()
                    Else '--ok-- Sent-
                        mbSendSelectedEmail = True
                        '= msgBoxResult1 =MsgBoxResult.Yes   '-default..-
                        If (Not chkDeleteWhenSent.Checked) AndAlso _
                              (MsgBox("Email was sent ok.." & vbCrLf & _
                                  "  Click Yes to drop it off the Queue.", _
                                  MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                            fsXml.Close()
                            Call mbReport("-- Sent, but not deleted.." & vbCrLf & "= = =")
                        Else  '-yes-
                            '-- delete PDF and XML files..
                            fsXml.SetLength(0)  '--kill the content before we close and remove.
                            fsXml.Close()
                            Try
                                File.Delete(sXmlFilePath)
                            Catch ex As Exception
                                MsgBox("Failed to DELETE Email Xml file.." & vbCrLf & ex.Message & vbCrLf & _
                                         vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
                            End Try
                            '-- delete PDF and XML files..
                            Try
                                File.Delete(sAttachmentPath)
                            Catch ex As Exception
                                MsgBox("Failed to DELETE PDF attachment file.." & vbCrLf & ex.Message & vbCrLf & _
                                         vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
                            End Try
                            '= 3411.0309-
                            '--  Delete grid row..
                            Try
                                dgvEmailList.Rows.RemoveAt(intGridRowIndex)
                                bWasDeleted = True
                            Catch ex As Exception
                                MsgBox("Failed to DELETE Grod Row.." & vbCrLf & ex.Message & vbCrLf & _
                                          vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
                            End Try
                            dgvEmailList.ClearSelection()
                            Call mbReport("-- Send done.." & vbCrLf & "= = =")
                        End If '-yes/no-
                    End If  '-cancelled/ok-
                End If  '-complete-
            End Using  '-fsxml-
            '= mbSendMailButtonLocked = False
        Catch ex As Exception
            MsgBox("Failed to get Excl. open on Email Xml file.." & vbCrLf & ex.Message & vbCrLf & _
                     vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
            mbSendMailButtonLocked = False
            Exit Function
        End Try  '-fsXml-

    End Function  '-mbSendSelectedEmail
    '= = = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- SEND Mail Column (Button)--
    '-- SEND Mail Column (Button)--

    Private mbSendMailButtonLocked As Boolean = False  '-serialises entry..

    Private Shared mbMailCancelled As Boolean = False
    '-dgvEmailList_CellContentClick-

    Private Sub dgvEmailList_CellContentClick(sender As Object, _
                                                ByVal cellEvent As DataGridViewCellEventArgs) _
                                                             Handles dgvEmailList.CellContentClick
        '= Dim sTarget, sRecipient, sSubject, sEmailText As String
        Dim sFileTitle, sAttachmentPath, sXmlFilePath As String
        Dim sColumnName As String

        If ((TypeOf dgvEmailList.Columns(cellEvent.ColumnIndex) Is DataGridViewButtonColumn) _
            Or (TypeOf dgvEmailList.Columns(cellEvent.ColumnIndex) Is DataGridViewCheckBoxColumn)) _
                                                           AndAlso Not cellEvent.RowIndex = -1 Then
            '--ok..  check its the Send email "button..
        Else
            Exit Sub
        End If     '= End If  '--not header-
        sColumnName = LCase(dgvEmailList.Columns(cellEvent.ColumnIndex).Name)

        If (sColumnName = "doc_send") Or (sColumnName = "view_doc") _
                                 Or (sColumnName = "delete_email") Or (sColumnName = "doc_marktosend") Then
            '-ok=
        Else  '-wrong button..
            MsgBox("Unknown button..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '-Send now-
        '-lock-
        mbSendMailButtonLocked = True

        '- get email meta stuff..
        With dgvEmailList.Rows(cellEvent.RowIndex)
            '= sTarget = .Cells("doc_email_target").Value  '--Name + email address.
            '= sRecipient = .Cells("doc_recipient").Value
            '= sSubject = .Cells("doc_subject").Value
            '= sEmailText = .Cells("doc_email_text").Value
            sFileTitle = Trim(.Cells("doc_file_title").Value)
            '= .Cells("xml_file_path").Value = sXmlFileFullPath
            sXmlFilePath = Trim(.Cells("xml_file_path").Value)
        End With '- dgvEmailList-
        sAttachmentPath = ""
        '-- Attachment (pdf )..
        If (sFileTitle <> "") Then
            sAttachmentPath = msEmailQueueSharePath & "\" & sFileTitle
        End If

        '-- Check which column got clicked..
        If (sColumnName = "view_doc") Then
            If (sAttachmentPath = "") Then
                MsgBox("Failed to find attachment Path..", MsgBoxStyle.Exclamation)
                mbSendMailButtonLocked = False
                Exit Sub
            Else
                Try
                    Process.Start(sAttachmentPath)
                Catch ex As Exception
                    MsgBox("Error starting PDF Reader.." & vbCrLf & _
                           " For file name: " & sAttachmentPath & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
                mbSendMailButtonLocked = False
                Exit Sub
            End If  '-view-
        ElseIf (sColumnName = "delete_email") Then
            If (MsgBox("Email will be DELETED and not sent.. " & vbCrLf & _
                       "Do you want to delete it forever ?" & vbCrLf & vbCrLf & _
           "  Click Yes to Delete and drop it off the Queue.", _
           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                mbSendMailButtonLocked = False
                Exit Sub
            Else  '--delete-
                Try
                    File.Delete(sXmlFilePath)
                Catch ex As Exception
                    MsgBox("Failed to DELETE Email Xml file.." & vbCrLf & ex.Message & vbCrLf & _
                             vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
                End Try
                '-- delete PDF and XML files..
                Try
                    File.Delete(sAttachmentPath)
                Catch ex As Exception
                    MsgBox("Failed to DELETE PDF attachment file.." & vbCrLf & ex.Message & vbCrLf & _
                             vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
                End Try
                Call mbReport("-- Email Deleted..")
                '= 3411.0309-
                '--  Delete grid row..
                Try
                    dgvEmailList.Rows.RemoveAt(cellEvent.RowIndex)
                Catch ex As Exception
                    MsgBox("Failed to DELETE Grod Row.." & vbCrLf & ex.Message & vbCrLf & _
                              vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
                End Try
                dgvEmailList.ClearSelection()
                '= Call mbRefreshEmailGrid()
                mbSendMailButtonLocked = False
                Exit Sub
            End If  '-delete=
        ElseIf (sColumnName = "doc_marktosend") Then
            ''-- if any checked.. enable send buttons.
            'If (dgvEmailList.Rows.Count > 0) Then
            '    Dim intCountChecked As Integer = 0
            '    For Each rowx As DataGridViewRow In dgvEmailList.Rows
            '        '-- keep going..
            '        If rowx.Cells("doc_markToSend").Value = True Then
            '            '-- count--
            '            intCountChecked += 1
            '            '--  confirm first..
            '        End If  '-marked-
            '    Next rowx
            '    If (intCountChecked > 0) Then
            '        btnSendAllMarked.Enabled = True
            '    Else
            '        btnSendAllMarked.Enabled = False
            '    End If
            'End If '-count-
            Exit Sub  '--see below-
        End If  '-name-

        '-- S E N D the  E M A I L --
        Dim bWasDeleted As Boolean
        '--  confirm first..
        If Not mbSendSelectedEmail(cellEvent.RowIndex, True, bWasDeleted) Then  '-sending one only..
            MsgBox("Send not done..", MsgBoxStyle.Exclamation)
        End If
        If Not bWasDeleted Then
            '--unmark..
            dgvEmailList.Rows(cellEvent.RowIndex).Cells("doc_markToSend").Value = False
        End If
        'If chkConfirmSends.Checked Then  '-confirm..
        '    If (MsgBox("OK to send this Email: " & vbCrLf & sSubject & _
        '                 vbCrLf & vbCrLf & "To: " & vbCrLf & _
        '                   sTarget & vbCrLf & vbCrLf & _
        '                    "  Click Yes to continue SEND..", _
        '             MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
        '        Exit Sub
        '    End If  '-yes-
        'End If '-confirm..

        ''-- Lock the XML file to serialise against another Emailer task..
        ''-- When email sent, trucate it and delete it..
        'Try '-fsXml-
        '    Using fsXml As FileStream = File.Open(sXmlFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
        '        mbMailCancelled = False
        '        '-- send and wait for completion..
        '        If Not clsJmxEmail1.SendEmail(sRecipient, sSubject, sEmailText, sAttachmentPath) Then
        '            MsgBox("Failed to initiate Email sending..", MsgBoxStyle.Exclamation)
        '            mbSendMailButtonLocked = False
        '            Exit Sub
        '        End If

        '        '- started ok..  Wait for completion.
        '        btnCancelSend.Enabled = True

        '        Dim intStart, intLast As Integer
        '        Dim sErrorText As String = ""

        '        '== WAIT 60 secs= --
        '        intStart = CInt(VB.Timer()) '--starting seconds.-
        '        intLast = CInt(VB.Timer()) '--starting seconds.-
        '        While (Not clsJmxEmail1.SendCompleted(sErrorText)) And _
        '                                 (CInt(VB.Timer()) < intStart + 60)  '--  VB.Timer returns a DOUBLE..-
        '            '--  integral part is seconds..
        '            If (intLast <> CInt(VB.Timer())) Then  '--update display every second..-
        '                '= labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
        '                labEmailTimer.Text = "Wait: " & (60 - (intLast - intStart))
        '                intLast = CInt(VB.Timer()) '--starting seconds.-
        '            End If
        '            System.Windows.Forms.Application.DoEvents()
        '        End While  '-completed..
        '        '=Call mSubUpdateStatus("", True)  '- and clear.'=mlabEmailTimer.Text = ""

        '        '= cmdCancelEmail.Enabled = False
        '        If (Not clsJmxEmail1.SendCompleted(sErrorText)) Then  '--timed out..-
        '            '= mSmtpClient1.SendAsyncCancel()
        '            clsJmxEmail1.cancelEmail()

        '            '==mbMailCancelled = True
        '            labStatus.Text = "Send operation timed out.."
        '            MsgBox("Send operation timed out.." & vbCrLf & vbCrLf & _
        '                         "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
        '            fsXml.Close()
        '        Else  '-completed-
        '            If mbMailCancelled Then  '== sErrorText <> "" Then
        '                '--manual cancel...-
        '                '==MsgBox(msSMTPResultMsg, MsgBoxStyle.Information)
        '                '= labStatus.Text = msSMTPResultMsg
        '                labStatus.Text &= " ** Send Message was cancelled."
        '            ElseIf (sErrorText <> "") Then    '-error-
        '                MsgBox("ERROR in Send: " & vbCrLf & sErrorText & vbCrLf & vbCrLf & _
        '                             "  Check Settings and credentials before re-trying..", MsgBoxStyle.Exclamation)
        '                labStatus.Text &= "** Error in Send."
        '                fsXml.Close()
        '            Else '--ok-- Sent-
        '                If (MsgBox("Email was sent ok.." & vbCrLf & _
        '                          "  Click Yes to drop it off the Queue.", _
        '                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
        '                    fsXml.Close()
        '                    labStatus.Text &= "-- Sent, but not deleted.."
        '                Else  '-yes-
        '                    '-- delete PDF and XML files..
        '                    fsXml.SetLength(0)  '--kill the content before we close and remove.
        '                    fsXml.Close()
        '                    Try
        '                        File.Delete(sXmlFilePath)
        '                    Catch ex As Exception
        '                        MsgBox("Failed to DELETE Email Xml file.." & vbCrLf & ex.Message & vbCrLf & _
        '                                 vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
        '                    End Try
        '                    '-- delete PDF and XML files..
        '                    Try
        '                        File.Delete(sAttachmentPath)
        '                    Catch ex As Exception
        '                        MsgBox("Failed to DELETE PDF attachment file.." & vbCrLf & ex.Message & vbCrLf & _
        '                                 vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
        '                    End Try
        '                    labStatus.Text &= "-- All done.."
        '                End If '-yes/no-
        '            End If  '-cancelled/ok-
        '        End If  '-complete-
        '    End Using  '-fsxml-
        '    '= mbSendMailButtonLocked = False
        'Catch ex As Exception
        '    MsgBox("Failed to get Excl. open on Email Xml file.." & vbCrLf & ex.Message & vbCrLf & _
        '             vbCrLf & "Another system may be running Emailer task..", MsgBoxStyle.Exclamation)
        '    mbSendMailButtonLocked = False
        '    Exit Sub
        'End Try  '-fsXml-
        '-done-
        mbSendMailButtonLocked = False
        '- grid row was deleted..
        '= Call mbRefreshEmailGrid()

    End Sub  '-dgvEmailList_CellContentClick-
    '= = = = = = =  = = = = = = = = == =
    '-===FF->

    '--  Capture DGV ChkBox col. CheckChanged Event--
    '-- https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.currentcelldirtystatechanged(v=vs.110).aspx

    '-- This event handler manually raises the CellValueChanged event 
    '-- by calling the CommitEdit method. 
    Sub dgvEmailList_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal cellEvent As EventArgs) _
                                                  Handles dgvEmailList.CurrentCellDirtyStateChanged
        If mbLoadingGrid Then
            Exit Sub
        End If
        If Not (TypeOf dgvEmailList.CurrentCell Is DataGridViewCheckBoxCell) Then
            Exit Sub
        End If     '= End If  '--not header-
        If dgvEmailList.IsCurrentCellDirty Then
            dgvEmailList.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub  '-CurrentCellDirtyStateChanged-
    '= = = =

    '-- If a check box cell is clicked, this event handler gets it..

    Public Sub dgvEmailList_CellValueChanged(ByVal sender As Object, _
                                              ByVal cellEvent As DataGridViewCellEventArgs) _
                                                     Handles dgvEmailList.CellValueChanged
        Dim sColumnName As String
        If mbLoadingGrid Then
            Exit Sub
        End If
        If (TypeOf dgvEmailList.Columns(cellEvent.ColumnIndex) Is DataGridViewCheckBoxColumn) _
                                                 AndAlso Not cellEvent.RowIndex = -1 Then
        Else
            Exit Sub
        End If     '= End If  '--not header-
        sColumnName = LCase(dgvEmailList.Columns(cellEvent.ColumnIndex).Name)
        '-test-
        '==MsgBox("Cell value Changed for: " & sColumnName, MsgBoxStyle.Information)

        If sColumnName = "doc_marktosend" Then
            Dim intCountChecked As Integer = 0
            Dim checkCell As DataGridViewCheckBoxCell
            '== buttonCell.Enabled = Not CType(checkCell.Value, [Boolean])
            '-- check all checkBox cells.
            '-- if any checked.. enable send buttons.
            If (dgvEmailList.Rows.Count > 0) Then
                For Each rowx As DataGridViewRow In dgvEmailList.Rows
                    '-- keep going..
                    checkCell = CType(rowx.Cells("doc_marktosend"), DataGridViewCheckBoxCell)

                    If CType(checkCell.Value, [Boolean]) Then '= rowx.Cells("doc_markToSend").Value = True Then
                        '-- count--
                        intCountChecked += 1
                        '--  confirm first..
                    End If  '-marked-
                Next rowx
                If (intCountChecked > 0) Then
                    btnSendAllMarked.Enabled = True
                Else
                    btnSendAllMarked.Enabled = False
                End If
            End If '-count-
            '--dgvEmailList.Invalidate()
            btnSendAllMarked.Text = "Send " & intCountChecked & " items Marked"
        End If  '-name-
    End Sub  '--cell value changed..
    '= = = = = = =  = = = = = = = = == =
    '-===FF->

    '- doc row selected.-

    Private Sub dgvEmailList_SelectionChanged(ByVal sender As Object, _
                                   ByVal e As EventArgs) Handles dgvEmailList.SelectionChanged
        Dim sSql As String
        Dim datatable1 As DataTable

        '--4223.0421.. IF Grid is empty, then getting CURRENT CELL crashes with NULL Object ref.--
        '=  CRASHES if Grid is empty.>>  Dim intRowIndex = dgvEmailList.CurrentCell.RowIndex

        '=  REMOVED  to fix crash ..  Dim intRowIndex = dgvEmailList.CurrentCell.RowIndex

        If mbIsInitialising Then Exit Sub
        If mbGridIsRefreshing Then Exit Sub

        '= mCurrentBinaryData = Nothing
        If (dgvEmailList.Rows.Count <= 0) Then
            Exit Sub  '--empty.. might be rebuilding-
        End If
        If (dgvEmailList.SelectedRows.Count > 0) Then
            '=  Dim intRowIndex = dgvEmailList.CurrentCell.RowIndex
        End If '-count-
    End Sub  '-StockList_SelectionChanged-
    '== = = = =  == = = = = = = = = =  =
    '-===FF->

    '-Pause-

    Private Sub btnPauseSending_Click(sender As Object, e As EventArgs) Handles btnPauseSending.Click
        mbPauseTheSending = True
    End Sub  '-Pause-
    '= = = = = = = = = = =

    '-- Send all..-
    '-- Just pick the top row each time..
    '-- Repeat the WHILE loop until an empty pass breaks us out of the Do Loop..
    '==
    '==   Updated.- 3519.0319  Started 14-March-2019= 
    '==    --  Email Queue. Put Try/catch around the SendAll loop to catch error..
    '==    

    Private Sub btnSendAllSelected_Click(sender As Object, e As EventArgs) Handles btnSendAllMarked.Click
        Dim rowx As DataGridViewRow
        Dim bNoMoreToSend, bWasDeleted As Boolean
        Dim intRowIndex As Integer
        Dim sLastRecipient As String

        mbPauseTheSending = False
        btnPauseSending.Enabled = True
        Try
            Do
                bNoMoreToSend = True
                intRowIndex = 0   '--start from the top again..
                While (dgvEmailList.Rows.Count > 0) And (intRowIndex < dgvEmailList.Rows.Count)
                    If mbPauseTheSending Then
                        Exit Do
                    End If
                    rowx = dgvEmailList.Rows(intRowIndex)
                    '-- keep going..
                    If rowx.Cells("doc_markToSend").Value = True Then
                        '-- S E N D the  E M A I L --
                        bNoMoreToSend = False   '--found some.  Force another DO pass.
                        sLastRecipient = rowx.Cells("doc_email_target").Value
                        If Not mbSendSelectedEmail(rowx.Index, False, bWasDeleted) Then  '- NOT sending one only..
                            '-- Failed..
                            rowx.Cells("doc_markToSend").Value = False
                            MsgBox("Send not done..", MsgBoxStyle.Exclamation)
                            Exit Do  '--stop everything..
                        Else  '-sent ok
                            If Not bWasDeleted Then
                                '-- Sent, but not deleted..
                                '--unmark..  So we don't try and send it again..
                                rowx.Cells("doc_markToSend").Value = False
                                intRowIndex += 1  '--look at nxt one if any.
                            Else  '-was sent and deleted.
                                Exit While  '--start again from the top.
                            End If
                        End If
                    Else  '--not marked-
                        intRowIndex += 1  '--look at nxt one if any.
                    End If  '-marked-
                    DoEvents()
                End While
                DoEvents()
            Loop Until bNoMoreToSend
            MsgBox("All done..." & vbCrLf & vbCrLf & _
                      "Last email was to Recipient: " & sLastRecipient & ".." & vbCrLf, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Possible Error in Email sending loop." & vbCrLf & ex.Message & vbCrLf & vbCrLf & _
                      "Last email was to Recipient: " & sLastRecipient & ".." & vbCrLf & _
                       "Please check that all Emails were sent..", MsgBoxStyle.Exclamation)
        End Try

        mbPauseTheSending = False
        btnPauseSending.Enabled = False
        btnSendAllMarked.Text = "Send Items Marked.."

    End Sub  '-send all-
    '== = = = =  == = = = = = = = = =  =
    '-===FF->

    '-btnSelectAll-
    '-- Check all emails as SEND now..

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click

        If (dgvEmailList.Rows.Count > 0) Then
            For Each rowx As DataGridViewRow In dgvEmailList.Rows
                rowx.Cells("doc_markToSend").Value = True
            Next rowx
        End If '-count-
        btnSendAllMarked.Enabled = True

    End Sub  '-btnSelectAll--
    '= = = = = = = = = = = = = 

    '--btnUnselectAll-

    Private Sub btnUnselectAll_Click(sender As Object, e As EventArgs) Handles btnUnselectAll.Click
        If (dgvEmailList.Rows.Count > 0) Then
            For Each rowx As DataGridViewRow In dgvEmailList.Rows
                rowx.Cells("doc_markToSend").Value = False
            Next rowx
        End If '-count-

    End Sub  '-btnUnselectAll-
    '== = = = =  == = = = = = = = = =  =
    '-===FF->


    Private Sub cmdCancelEmail_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles btnCancelSend.Click

        Call mbReport("Cancelling send..")
        DoEvents()
        '=mSmtpClient1.SendAsyncCancel()

        mbPauseTheSending = True  '-tell I Robot..
        clsJmxEmail1.cancelEmail()

        mbMailCancelled = True
    End Sub  '--cancel..-
    '= = = = = = = = = = = = = =    

    '-- Cancel..-

    'Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
    '                             ByVal eventArgs As System.EventArgs)

    '    '= mbNotifyCancelled = True
    '    Me.Close()
    'End Sub
    '= = = = = = = = = =


    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub



End Class  '-frmEmailMain-
'= = = = = = = = = = == == =
'= = = = = = = = = = = = = =
'-===FF->

'==  http://geekswithblogs.net/ntsmith/archive/2006/08/17/88250.aspx


Public Class clsCompareFileInfo
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim File1 As FileInfo
        Dim File2 As FileInfo

        File1 = DirectCast(x, FileInfo)
        File2 = DirectCast(y, FileInfo)
        Compare = DateTime.Compare(File1.LastWriteTime, File2.LastWriteTime)
    End Function
End Class '-clsCompareFileInfo-
'= = = = = = == = = 


'== end form ==