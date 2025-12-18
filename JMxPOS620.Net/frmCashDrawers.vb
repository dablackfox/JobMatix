Option Explicit On
Option Strict Off

Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Windows.Forms
Imports vb = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Math


Public Class frmCashDrawers

    '==
    '==     3411.0324- 24/31-March-2018-
    '==      -- POS Setup/Options. Set Cash Drawer printer Connections....
    '==      -- Add clsPrintDirect tp print the CashDrawer codes via Win32 printing....
    '==
    '==     3501.1024- 24-Oct-2018-
    '==      -- Fix staffname on label...
    '== 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Private mbIsActivated As Boolean = False

    Private mbIsInitialising As Boolean = True
    Private mbFormLoading As Boolean = False
    Private mbActivated As Boolean = False

    Private mFrmParent As Form   '-- for locating Me..

    Private msServer As String = ""
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = 0

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    '= Private msLastSqlErrorMessage As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    '= Private mlJobId As Integer = -1

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private mImageUserLogo As Image

    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msDefaultPrinterName As String = ""
    '= Private msReportprinterName As String = ""
    Private msReceiptPrinterName As String = ""

    Private msBusinessName As String = ""
    Private mbIsTestMode As Boolean = False

    Private msCurrentTill As String = ""  '-A..H--
    Private msTillPrinterNameKey As String = ""
    Private msTillOpenCodeKey As String = ""

    Private mClsPrintDirect1 As clsPrintDirect

    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function msGetTillPrinterNameKey(ByVal sCurrentTill As String) As String
        Dim sTillPrinterNameKey As String
        sTillPrinterNameKey = "POS_TillOpenPrinterName_Till_" & sCurrentTill

        msGetTillPrinterNameKey = sTillPrinterNameKey

    End Function '-msGetTillPrinterName=
    '= = = = = = = = = = = = = == = = = =

    Private Function msGetTillOpenCodeKey(ByVal sCurrentTill As String) As String
        Dim sTillOpenCodeKey As String
        sTillOpenCodeKey = "POS_TillOpenCode_Till_" & sCurrentTill

        msGetTillOpenCodeKey = sTillOpenCodeKey

    End Function '-msGetTillPrinterName=
    '= = = = = = = = = = = = = == = = = =

    '-- save systemInfo value..

    Private Function mbSaveSystemValue(ByVal sValue As String, _
                                         ByVal sSystemkey As String, _
                                          Optional ByVal bNotifyOK As Boolean = False) As Boolean

        Dim asChanges(1) As String '--update list..-
        mbSaveSystemValue = False
        If (Trim(sValue) <> "") Then  '= AndAlso IsNumeric(txt1.Text) Then
            Try
                asChanges(0) = sSystemkey  '= "POS_sell_margin"
                asChanges(1) = Trim(sValue)
                If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                    MsgBox("Couldn't save " & sSystemkey & " setting.", MsgBoxStyle.Exclamation)
                Else
                    mbSaveSystemValue = True
                    If bNotifyOK Then MsgBox("Settings saved ok..", MsgBoxStyle.Information)
                End If
            Catch ex As Exception
                MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Function '-mbSaveSystemValue-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-mbSendCashDrawerOpenCommand-

    Private Function mbSendCashDrawerOpenCommand(ByVal sPrinterName As String, _
                                                 ByVal sCmdText As String, _
                                                 Optional ByVal bTesting As Boolean = False) As Boolean

        mbSendCashDrawerOpenCommand = mClsPrintDirect1.SendCashDrawerOpenCommand(sPrinterName, sCmdText, bTesting)

        'Dim byteData() As Byte = {}
        'Dim sHex, s1, s2, s3 As String
        'Dim sCodes() As String
        'Dim sPrintData As String = ""

        'mbSendCashDrawerOpenCommand = False
        'If (sCmdText = "") Or (sPrinterName = "") Then
        '    Exit Function
        'End If
        ''-- Command text is in Display Mode. (is ESC p NULL etc)
        ''--  Translate into actual ESC stuff.
        's1 = sCmdText
        ''-- check for codes..
        'If (s1 <> "") Then
        '    If (vb.Left(UCase(s1), 3)) = "ESC" Then
        '        '-codes
        '        sCodes = Split(s1, " ")
        '        If (sCodes.Length > 0) Then
        '            For Each s2 In sCodes
        '                If UCase(s2) = "ESC" Then
        '                    sPrintData &= Chr(27)
        '                ElseIf UCase(s2) = "NULL" Then
        '                    sPrintData &= Chr(0)
        '                ElseIf UCase(s2) = "SOH" Then
        '                    sPrintData &= Chr(1)
        '                ElseIf UCase(s2) = "ENQ" Then
        '                    sPrintData &= Chr(5)
        '                ElseIf (s2 = " ") Then '-drop extra spaces=-
        '                ElseIf UCase(s2) = "SPACE" Then
        '                    sPrintData &= Chr(32)
        '                Else
        '                    sPrintData &= s2  '- include everything else.
        '                End If
        '            Next s2
        '        Else
        '            MsgBox("Nothing entered..", MsgBoxStyle.Exclamation)
        '        End If
        '    Else  '-just text-
        '        sPrintData = s1
        '    End If
        'End If  '-testdata-
        ''-send to printer..
        'If (sPrintData <> "") And (sPrinterName <> "") Then
        '    '= use this- 
        '    byteData = System.Text.Encoding.UTF8.GetBytes(sPrintData)
        '    '= this gives 2 bytes per char.  byteData = System.Text.Encoding.Unicode.GetBytes(sTestData)
        '    '- show byte data..  in Hex..
        '    sHex = ""  '= "Byte Count: " & byteData.Length & ".." & vbCrLf
        '    For Each byte1 As Byte In byteData
        '        If sHex <> "" Then sHex &= ", "
        '        sHex &= Conversion.Hex(byte1)
        '    Next '-byte1-
        '    If bTesting Then
        '        MsgBox("Testing Cash Drawer- Sending bytes: " & vbCrLf & _
        '                   "Byte Count: " & byteData.Length & ".." & vbCrLf & sHex, MsgBoxStyle.Information)
        '    End If
        '    If clsPrintDirect1.PrintStringDirect("Till Open Cmd.", sPrinterName, byteData) Then
        '        mbSendCashDrawerOpenCommand = True
        '    End If
        'End If
    End Function  '-mbSendCashDrawerOpenCommand=
    '= = = = = = = = == = = = = = = = = = = = =
    '-===FF->

    '--Constructor-
    '--Constructor-

    Public Sub New(ByRef frmParent As Form, _
                     ByVal sServer As String, _
                      ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                       ByRef colSqlDBInfo As Collection, _
                       ByVal sVersionPOS As String, _
                       ByRef imageUserLogo As Image, _
                        ByVal intStaff_id As Integer, _
                         ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = frmParent
        msServer = sServer
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        msVersionPOS = sVersionPOS
        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

        msComputerName = My.Computer.Name

        '= All this has to be here to service Public call to Open cash Drawer.

        '-- get system Info table data.-
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        '-CashDrawer print direct
        mClsPrintDirect1 = New clsPrintDirect


    End Sub  '--new --
    '= = =  = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- Public function to kick Open Cash Drawer for Indicated TILL..

    '-OpenCashDrawer-

    'Public Function OpenCashDrawer(ByVal strTillId As String) As Boolean
    '    Dim sTillPrinterNameInfoKey, sTillOpenCodeInfoKey As String
    '    Dim sPrinterName As String = ""
    '    Dim sEscapeCodes As String = ""

    '    If (mClsPrintDirect1 Is Nothing) Or (mSysInfo1 Is Nothing) Then
    '        MsgBox("Error- Class not initialised...", MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If
    '    OpenCashDrawer = False
    '    If (InStr("ABCDEFGH", UCase(strTillId)) <= 0) Then
    '        MsgBox("Error- " & strTillId & " is invalid Till Id..", MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If

    '    '==  get Printer and ESCape codes for this Till..
    '    sTillPrinterNameInfoKey = msGetTillPrinterNameKey(UCase(strTillId))
    '    sTillOpenCodeInfoKey = msGetTillOpenCodeKey(UCase(strTillId))

    '    If mSysInfo1.exists(sTillPrinterNameInfoKey) AndAlso _
    '                (Trim(mSysInfo1.item(sTillPrinterNameInfoKey)) <> "") Then
    '        sPrinterName = Trim(mSysInfo1.item(msTillPrinterNameKey))
    '        If mSysInfo1.exists(sTillOpenCodeInfoKey) AndAlso _
    '              (Trim(mSysInfo1.item(msTillOpenCodeKey)) <> "") Then
    '            sEscapeCodes = Trim(mSysInfo1.item(sTillOpenCodeInfoKey))
    '        End If
    '    End If  '-exists-
    '    If (sPrinterName = "") Or (sEscapeCodes = "") Then
    '        MsgBox("No Cash-Drawer printer is set up for- " & strTillId, MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If
    '    '-- ok.. send ESCape codes to printer to Open Drawer..
    '    '-- send routine has to decode into actual ESC stuff.

    '    If Not mbSendCashDrawerOpenCommand(sPrinterName, sEscapeCodes) Then
    '        MsgBox("Open drawer Failed ! ", MsgBoxStyle.Exclamation)
    '    End If

    'End Function  '-OpenCashDrawer-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '- refresh Printer Combo--

    Private Function mbRefreshPrinters() As Boolean

        '= Dim pd1 As New PrintDocument()
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim sName As String

        msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
        cboReceiptPrinters.Items.Clear()
        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= "JobMatix33" default.
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboReceiptPrinters.Items.Add(sName)
            Next sName
            If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
        End If '-getAvail.-  

    End Function  '-mbRefreshPrinters-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--Load-

    Private Sub frmCashDrawers_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        grpBoxTillTrigger.Text = ""
        grpBoxTillTrigger.Enabled = False
        labSelectedTill.Text = ""

        Call mbRefreshPrinters()
        cboReceiptPrinters.Enabled = False
        '= cboTillSelect.SelectedIndex = -1

        btnTillTest.Enabled = False
        btnTillSave.Enabled = False

        '= All this has to be IN CONSTRUCTOR above to service Public call to Open cash Drawer.
        '-- get system Info table data.-
        'mSysInfo1 = New clsSystemInfo(mCnnSql)
        ''-CashDrawer print direct
        'clsPrintDirect1 = New clsPrintDirect
        labStaffName.Text = msStaffName   '=3501.1024=

        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        Call CenterForm(Me)

    End Sub  '-load-
    '= = = = = = = = = = = =

    '-activated-

    Private Sub frmCashDrawers_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        If mbIsActivated Then Exit Sub
        mbIsActivated = True

        '=cboTillSelect.Enabled = True
        mbIsInitialising = False

        '= cboTillSelect.Select()
        '=txtTillTrigger.Select()

    End Sub  '-activated-
    '= = = = = = = = = = = =
    '-===FF->

    '-cboTillSelect_SelectedIndexChanged-

    'Private Sub cboTillSelect_SelectedIndexChanged(sender As Object, e As EventArgs) _
    '                                                  Handles cboTillSelect.SelectedIndexChanged
    '    '=Dim strTillId As String '-- [A..H]..
    '    Dim strPrtName, strOpenCode As String

    '    If mbIsInitialising Then Exit Sub
    '    btnTillTest.Enabled = False
    '    btnTillSave.Enabled = False
    '    grpBoxTillTrigger.Enabled = False
    '    '-  get systemInfo for this Till-
    '    '== POS_TillOpenCode_Till_X --
    '    '== POS_TillOpenPrinterName_Till_X --
    '    If (cboTillSelect.SelectedIndex >= 0) Then
    '        msCurrentTill = UCase(vb.Right(cboTillSelect.SelectedItem, 1))
    '        msTillPrinterNameKey = msGetTillPrinterNameKey(msCurrentTill)  '="POS_TillOpenPrinterName_Till_" & msCurrentTill
    '        msTillOpenCodeKey = msGetTillOpenCodeKey(msCurrentTill)  '= "POS_TillOpenCode_Till_" & msCurrentTill
    '        If mSysInfo1.exists(msTillPrinterNameKey) AndAlso _
    '              (Trim(mSysInfo1.item(msTillPrinterNameKey)) <> "") Then
    '            strPrtName = Trim(mSysInfo1.item(msTillPrinterNameKey))
    '            cboReceiptPrinters.SelectedItem = strPrtName
    '            txtTillTrigger.Enabled = True
    '            grpBoxTillTrigger.Enabled = True
    '            If mSysInfo1.exists(msTillOpenCodeKey) AndAlso _
    '                   (Trim(mSysInfo1.item(msTillOpenCodeKey)) <> "") Then
    '                strOpenCode = Trim(mSysInfo1.item(msTillOpenCodeKey))
    '                txtTillTrigger.Text = strOpenCode & " "  '--restore trailing space in textbox.
    '            Else  '-no code saved-
    '                Call btnRestoreDefault_Click(btnRestoreDefault, New System.EventArgs)
    '            End If
    '            cboReceiptPrinters.Enabled = True
    '            txtTillTrigger.SelectionStart = txtTillTrigger.TextLength
    '            btnTillTest.Enabled = True
    '        Else
    '            MsgBox(msCurrentTill & vbCrLf & _
    '                   " Please choose the Docket printer that connects Till_" & msCurrentTill, MsgBoxStyle.Information)
    '            cboReceiptPrinters.Enabled = True
    '            cboReceiptPrinters.SelectedIndex = -1
    '            cboReceiptPrinters.Select()
    '        End If '- exists.

    '    End If  '-index.-
    'End Sub '-cboTillSelect_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--optTill_A_CheckedChanged-

    Private Sub optTill_A_CheckedChanged(sender As Object, e As EventArgs) _
                                               Handles optTill_A.CheckedChanged, _
                                               optTill_B.CheckedChanged, optTill_C.CheckedChanged, _
                                               optTill_D.CheckedChanged, _
                                               optTill_E.CheckedChanged, optTill_F.CheckedChanged, _
                                               optTill_G.CheckedChanged, optTill_H.CheckedChanged
        Dim opt1 As RadioButton = CType(sender, RadioButton)
        Dim radioNameSelected As String = CType(sender, RadioButton).Name
        Dim strPrtName, strOpenCode As String

        btnTillTest.Enabled = False
        btnTillSave.Enabled = False
        grpBoxTillTrigger.Enabled = False

        '= 3411.0311 =
        If opt1.Checked Then
            msCurrentTill = UCase(vb.Right(radioNameSelected, 1))
            If (InStr("ABCDEFGH", msCurrentTill) > 0) Then  '-Ok-
                msTillPrinterNameKey = msGetTillPrinterNameKey(msCurrentTill)  '="POS_TillOpenPrinterName_Till_" & msCurrentTill
                msTillOpenCodeKey = msGetTillOpenCodeKey(msCurrentTill)  '= "POS_TillOpenCode_Till_" & msCurrentTill
                labSelectedTill.Text = "Till-  " & msCurrentTill
                If mSysInfo1.exists(msTillPrinterNameKey) AndAlso _
                      (Trim(mSysInfo1.item(msTillPrinterNameKey)) <> "") Then
                    strPrtName = Trim(mSysInfo1.item(msTillPrinterNameKey))
                    cboReceiptPrinters.SelectedItem = strPrtName
                    txtTillTrigger.Enabled = True
                    grpBoxTillTrigger.Enabled = True
                    If mSysInfo1.exists(msTillOpenCodeKey) AndAlso _
                           (Trim(mSysInfo1.item(msTillOpenCodeKey)) <> "") Then
                        strOpenCode = Trim(mSysInfo1.item(msTillOpenCodeKey))
                        txtTillTrigger.Text = strOpenCode & " "  '--restore trailing space in textbox.
                    Else  '-no code saved-
                        Call btnRestoreDefault_Click(btnRestoreDefault, New System.EventArgs)
                    End If
                    cboReceiptPrinters.Enabled = True
                    txtTillTrigger.SelectionStart = txtTillTrigger.TextLength
                    btnTillTest.Enabled = True
                Else
                    MsgBox("Setting up Till-  " & msCurrentTill & vbCrLf & _
                           " Please choose the Docket printer that connects Till- " & msCurrentTill, MsgBoxStyle.Information)
                    cboReceiptPrinters.Enabled = True
                    cboReceiptPrinters.SelectedIndex = -1
                    cboReceiptPrinters.Select()
                End If '- exists.
            End If
        End If  '--checked-

    End Sub  '-optTill_A_CheckedChanged-
    '= = = = = = = = = = = = = === = = 
    '-===FF->

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                 ByVal e As System.EventArgs) _
                                                    Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbIsInitialising Then Exit Sub
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            'If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
            '    '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
            '    MsgBox("Failed to save Receipt printer setting.", MsgBoxStyle.Information)
            'End If
            txtTillTrigger.Enabled = True
            grpBoxTillTrigger.Enabled = True
            Call btnRestoreDefault_Click(btnRestoreDefault, New System.EventArgs)

        End If '-index-
    End Sub '-ReceiptPrinters-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Till Trigger Events..-
    '-- Till Trigger Events..-
    '-- Till Trigger Events..-

    Private Const k_DEFAULT_TRIGGER As String = "ESC p NULL ESC @"
    '-- Note- This is how it is stored in the SystemInfo Table..
    '--  It is translated just prior to sending to printer/till.

    '-- txtTillTrigger TESTING--
    '-- Private mColTriggerSequence As New Collection-
    '--  NO NEED to translate and collect..  just store the string.
    '--   We will translate just before sending to printer.

    '= 3411.0313=
    '-- catch ESCAPE for Sale form Cancel function..

    '- PreviewKeyDown is where you preview the key.
    '- Do not put any logic here, instead use the
    '- KeyDown event after setting IsInputKey to true.
    Private Sub txtTillTrigger_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) _
                                            Handles txtTillTrigger.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Escape, Keys.Down, Keys.Up, Keys.Left, Keys.Right, Keys.Tab, Keys.Delete
                e.IsInputKey = True
        End Select
    End Sub  '-Me.PreviewKeyDown-
    '= = = = = = = = =  = = = = = =
    '-===FF->

    '-- FORM- Key Down..-
    '-- catch Null and ESCape...-
    '--  If Backspace, then drop item to the left....

    Private Sub txtTillTrigger_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                               Handles txtTillTrigger.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim text1 As TextBox = CType(eventSender, TextBox)
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim intInsertPos As Integer = text1.SelectionStart
        Dim intPos As Integer

        If (KeyCode = System.Windows.Forms.Keys.D0) And _
                        ((Not eventArgs.Shift) And (eventArgs.Control)) Then '--Ctl-Zero--
            '--Ctl-Zero-- --> NULL
            text1.Text = txtTillTrigger.Text.Insert(intInsertPos, "NULL" & " ")  '--always add trailing space..
            text1.SelectionStart = intInsertPos + 5
            '= mColTriggerSequence.Add(Chr(0))
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Escape) And _
                    ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--ESCAPE: cancel--
            text1.Text = text1.Text.Insert(intInsertPos, "ESC" & " ")
            text1.SelectionStart = intInsertPos + 4
            '= mColTriggerSequence.Add(Chr(27))
            eventArgs.Handled = True

        ElseIf (KeyCode = System.Windows.Forms.Keys.Back) And _
              ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--Backspace..  Drop last item..
            '-- delete backwards to prev. space.
            '-- let it go through to KeyPress..
        ElseIf (KeyCode = System.Windows.Forms.Keys.Space) And _
              ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--Space.. 
            text1.Text = text1.Text.Insert(intInsertPos, "SPACE" & " ")
            text1.SelectionStart = intInsertPos + 6
            '= mColTriggerSequence.Add(Chr(27))
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Right) Or (KeyCode = System.Windows.Forms.Keys.Down) Then
            If (intInsertPos < text1.TextLength) AndAlso _
                      (text1.Text.Substring(intInsertPos, 1) <> " ") Then '-can go right..
                intPos = intInsertPos
                '- go right to next space.
                While (intPos < text1.TextLength) And (text1.Text.Substring(intPos, 1) <> " ")
                    intPos += 1
                End While
                If (intPos < text1.TextLength) Then
                    intPos += 1   '-get past space delim.
                End If
                text1.SelectionStart = intPos
            End If
            eventArgs.Handled = True  '- we take it out of the pipeline anyway..

        ElseIf (KeyCode = System.Windows.Forms.Keys.Left) Or (KeyCode = System.Windows.Forms.Keys.Up) Then
            If (intInsertPos > 0) Then '-can go left..
                '--Back up to point to next token to the left.
                intPos = intInsertPos
                If (text1.Text.Substring(intPos - 1, 1) = " ") Then
                    intPos -= 1
                End If
                '--Now pointing to trailing space..
                intPos -= 1  '-point to last char. of token.
                '- back up to next space on left..
                While (intPos >= 0) AndAlso (text1.Text.Substring(intPos, 1) <> " ")
                    intPos -= 1
                End While
                text1.SelectionStart = intPos + 1
            End If
            eventArgs.Handled = True  '- we take it out anyway..
        ElseIf (KeyCode = System.Windows.Forms.Keys.Delete) Then
            '--DELete-  takes out next token to the right..
            intPos = intInsertPos
            If (intPos < text1.TextLength) Then
                '- go right to next space.
                While (intPos < text1.TextLength) And (text1.Text.Substring(intPos, 1) <> " ")
                    intPos += 1
                End While
                If (intPos < text1.TextLength) Then
                    intPos += 1   '-get past space delim.
                End If
                '- Cut the token out..
                text1.Text = text1.Text.Remove(intInsertPos, (intPos - intInsertPos))
                '= Insert Point doesn't move..  
                text1.SelectionStart = intInsertPos
            End If
            eventArgs.Handled = True  '- we take it out anyway..
        ElseIf (KeyCode = System.Windows.Forms.Keys.Tab) Then
            eventArgs.Handled = True  '- we take it out anyway..
        Else
            '-- let keyPress pick up ascii char..
        End If  '--null/ESCAPE-
    End Sub '--keyDown..-
    '= = = = = = = = = = == =
    '-===FF->

    '-txtTillTrigger_KeyPress-
    '--  If Backspace, then drop item to the left....

    Private Sub txtTillTrigger_KeyPress(sender As Object, ev As System.Windows.Forms.KeyPressEventArgs) _
                                                                    Handles txtTillTrigger.KeyPress

        Dim text1 As TextBox = CType(sender, TextBox)
        Dim intInsertPos As Integer = text1.SelectionStart
        Dim sData As String = Trim(text1.Text)
        Dim keyAscii As Short = Asc(ev.KeyChar)
        Dim s1 As String
        Dim intPos As Integer

        If (keyAscii = 13) Then '--enter-
            '- done-
            '= MsgBox("all done.", MsgBoxStyle.Information)
            If txtTillTrigger.TextLength > 4 Then
                btnTriggerOk.Select()
            End If
            ev.Handled = True
        ElseIf (keyAscii = 27) Then  '-ESCAPE shouldn't be here..
            '-- ignore it-
            ev.Handled = True
        ElseIf (keyAscii = 32) Then  '-SPACE crept through..
            '--Already dealt with- ignore it-
            ev.Handled = True
        ElseIf (keyAscii = 127) Then  '-DELete..
            '-- Delete the next character to right ?-
            '-- NO.. just ignore it.
            ev.Handled = True
        ElseIf (keyAscii = 8) Then  '-backspace-
            '-- backup over last group.
            intPos = intInsertPos - 1  '---zero-based-  point to trailing space.
            If (text1.TextLength > 1) And _
                      ((intPos >= 0) AndAlso (text1.Text.Substring(intPos, 1) = " ")) Then
                text1.Text = text1.Text.Remove(intPos, 1)  '-remove trailing space..
                intPos -= 1
                text1.SelectionStart = intPos
                While (intPos >= 0) AndAlso (text1.Text.Substring(intPos, 1) <> " ")
                    text1.Text = text1.Text.Remove(intPos, 1)
                    intPos -= 1
                End While
                text1.SelectionStart = intPos + 1
            End If  '-length-
            ev.Handled = True
        Else '-- add to list
            text1.Text = text1.Text.Insert(intInsertPos, ev.KeyChar & " ")
            text1.SelectionStart = intInsertPos + 2
            '= mColTriggerSequence.Add(ev.KeyChar)
            ev.Handled = True
        End If
    End Sub '-- txtTillTrigger_KeyPress
    '= = = = = =  = = = = = = = = = = = = =
    '-===FF->

    '-- Click on text--
    '-- Adjust selection point to start of token..

    Private Sub txtTillTrigger_click(sender As Object, ev As EventArgs) _
                                         Handles txtTillTrigger.Click
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim intInsertPos As Integer = text1.SelectionStart
        Dim sData As String = Trim(text1.Text)
        Dim intPos As Integer

        If (intInsertPos <= 0) Then Exit Sub '- ok if at start.
        If (text1.TextLength <= 0) Then Exit Sub

        text1.SelectionLength = 0  '--kill any selection.
        intPos = intInsertPos
        If (text1.Text.Substring(intPos - 1, 1) = " ") Then
            Exit Sub   '-ok if we are situated after trailing space.
        End If

        '--we must be in the middle of a token.
        '- back up to next space on left..
        While (intPos >= 0) AndAlso (text1.Text.Substring(intPos, 1) <> " ")
            intPos -= 1
        End While
        text1.SelectionStart = intPos + 1

    End Sub '-txtTillTrigger- -
    '= = = = = = = = = =  = 

    '-txtTillTrigger_TextChanged-

    Private Sub txtTillTrigger_TextChanged(sender As Object, e As EventArgs) Handles txtTillTrigger.TextChanged
        If mbIsInitialising Then Exit Sub
        If Trim(txtTillTrigger.Text) <> "" Then
            btnTriggerOk.Enabled = True
        End If
    End Sub   '-txtTillTrigger_TextChanged-
    '= = = = = = = = = = = = = = = == = = 
    '-===FF->

    '--Trigger code buttons.

    Private Sub btnRestoreDefault_Click(sender As Object, e As EventArgs) Handles btnRestoreDefault.Click

        txtTillTrigger.Text = k_DEFAULT_TRIGGER
        If (vb.Right(txtTillTrigger.Text, 1) <> " ") Then
            txtTillTrigger.Text &= " "
        End If
        txtTillTrigger.SelectionStart = txtTillTrigger.TextLength
        txtTillTrigger.Select()
    End Sub  '-restore-
    '= = = = = = = = = = = = = = =

    Private Sub btnTriggerClear_Click(sender As Object, e As EventArgs) Handles btnTriggerClear.Click

        txtTillTrigger.Text = ""
        txtTillTrigger.SelectionStart = 0
        txtTillTrigger.Select()
    End Sub  '--clear-
    '= = = = = = = = = =

    '--btnTriggerOk-

    Private Sub btnTriggerOk_Click(sender As Object, e As EventArgs) Handles btnTriggerOk.Click

        btnTillTest.Enabled = True
        btnTillSave.Enabled = True
    End Sub  '-btnTriggerOk-
    '= = = = = = = = = = = = =

    '--END of Till Trigger Events--
    '--END of Till Trigger Events--
    '--END of Till Trigger Events--
    '-===FF->


    Private Sub btnTillTest_Click(sender As Object, e As EventArgs) Handles btnTillTest.Click

        '-- send routine has to decode into actual ESC stuff.

        If Not mbSendCashDrawerOpenCommand(msReceiptPrinterName, txtTillTrigger.Text, True) Then
            MsgBox("Test Failed ! ", MsgBoxStyle.Exclamation)
        End If
    End Sub  '-test==
    '= = = = = = = = = == = =

    '-- save setting for current till..

    Private Sub btnTillSave_Click(sender As Object, e As EventArgs) Handles btnTillSave.Click
        Dim strPrtName, strOpenCode As String

        If (msTillPrinterNameKey <> "") And (msCurrentTill <> "") Then  '-ok-
            strOpenCode = Trim(txtTillTrigger.Text)
            strPrtName = Trim(cboReceiptPrinters.SelectedItem)
            If (strPrtName <> "") And (strOpenCode <> "") Then
                '--save these values..
                If mbSaveSystemValue(strPrtName, msTillPrinterNameKey) AndAlso _
                           mbSaveSystemValue(strOpenCode, msTillOpenCodeKey) Then
                    MsgBox("Settings for Till-" & msCurrentTill & " saved ok..", MsgBoxStyle.Information)
                End If
            End If
        End If  '-key-
    End Sub  '-save --
    '= = = = = = = = = = = = = = = = =

    Private Sub btnRefreshPrt_Click(sender As Object, e As EventArgs) Handles btnRefreshPrt.Click
        Call mbRefreshPrinters()
    End Sub  '-btnRefreshPrt-
    '= = = = = = = = = == = = = = =


End Class  '-frmCashDrawers-
'= = = = == = = = = =  = ==  =

'== end form=