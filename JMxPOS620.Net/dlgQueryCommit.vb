
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports System.Windows.Forms

Public Class dlgQueryCommit

    '--  Query Commit (Sale or PO)..
    '--   With Email/print Options --

    '==
    '==  grh. JobMatix 3.1.3107.0831 ---  31-Aug-2015 ===
    '==   >>  PO/Sales Email/printing decision done in here.. 
    '==
    '== 
    '==  NEW POS Build..
    '==
    '==     v3.3.3311.0225..  26-Feb-2017= ===
    '==       >> SALE/Commit Confirmation.. Add printer combos, and pass selection onto ShowInvoice..
    '=          -- Also When called from Commit, 
    '==                 printing of Invoice or Receipt auto-proceeds without further user decision.
    '==
    '==     v3.4.3403.1031. 31-Oct-2017= ===
    '==       >> Form Closing..  Close anyway...
    '== 
    '==     v3.4.3411.0109. 09-Jan-2018= ===
    '==       >> Get and Show PDF printer if any.....
    '==
    '==     v3.4.3411.0422. 22-April-2018= ===
    '==       >> Exit Activated if activated....
    '==
    '==   Updated.- 3519.0221  Started 21-Feb-2019= 
    '==     -- Fixes to allow A4 Invoice printing on NON-account Sale... 
    '==           ie..   here User can choose A4 Invoice even for a Not-on-account Sale.
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

    Private Const k_invoicePrtSettingKey As String = "POS_InvoicePrinter"
    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"

    '= = = = = = = = =

    Private mbIsActivated As Boolean = True
    Private mbIsInitialising As Boolean = True

    Private mbDataChanged As Boolean = False

    Private mbCommitOK As Boolean = False

    '=3311.226=
    Private msInvoicePrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    '== Private msLabelPrinterName As String = ""
    Private msDefaultPrinterName As String = ""
    Private msPdfPrinterName As String = ""

    '== Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    '= Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    '- result of printer selections..
    ReadOnly Property selectedInvoicePrinter As String
        Get
            selectedInvoiceprinter = msInvoicePrinterName
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    ReadOnly Property selectedReceiptPrinter As String
        Get
            selectedReceiptPrinter = msReceiptPrinterName
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    '-A4InvoiceChecked-

    ReadOnly Property A4InvoiceChecked As Boolean
        Get
            A4InvoiceChecked = optConfirmA4.Checked
        End Get
    End Property  '-forced A4..
    '= = = = = = =  = = = = = = = = = = = = = = =
    '-===FF->

    Private Sub dlgQueryCommit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String

        '-preset cancel result-
        '==  NO   --  Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        '3311.226-- Load printer combos..
        Call CenterForm(Me)

        '- get printers collection and set up combos.
        cboInvoicePrinters.Items.Clear()
        cboReceiptPrinters.Items.Clear()

        '==3301.428= Local Settings-
        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)
        msPdfPrinterName = ""

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboInvoicePrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
                '- see below for PDF..
                '    If (InStr(LCase(sName), "adobe pdf") > 0) Then
                '        msPdfPrinterName = sName  '-save PDF printer name--
                '    End If
            Next sName

            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_invoicePrtSettingKey) AndAlso _
                     (mLocalSettings1.item(k_invoicePrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_invoicePrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboInvoicePrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboInvoicePrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboInvoicePrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            '-receipt-
            If mLocalSettings1.exists(k_receiptPrtSettingKey) AndAlso _
                    (mLocalSettings1.item(k_receiptPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_receiptPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it-
                    cboReceiptPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                End If
            Else '-no pref-
                If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query receipt prt.--
        End If '-getAvail.-  

        '=3411.0110=  Get PDF prefrred printer...
        '---(Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-get--
        '=3411.0113=
        labPdfPrinter.Text = msPdfPrinterName

        ''-- Either Invoice or receipt--
        'If (InStr(LCase(labDocType.Text), "receipt") > 0) Then  '-receipt
        '    cboInvoicePrinters.Enabled = False
        'Else '-printing invoice-
        '    cboReceiptPrinters.Enabled = False
        'End If
        panelDocOpts.Enabled = False

    End Sub  '-load-
    '= = = = = = = = = = = = 
    '-===FF->

    '-Activated-

    Private Sub dlgQueryCommit_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        If mbIsActivated Then Exit Sub
        mbIsActivated = True

    End Sub '-Activated-

    '--shown-

    Private Sub dlgQueryCommit_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'If mbIsActivated Then Exit Sub
        'mbIsActivated = True

        '-- set print options..
        '==
        '==   Updated.- 3519.0221  Started 21-Feb-2019= 
        '==     -- Fixes to allow A$ Invoice printing on NON-account Sale... 
        '==           ie..   here User can choose A4 Invoice even for a Not-on-account Sale.

        '-- Either Invoice or receipt--
        If (InStr(LCase(labDocType.Text), "receipt") > 0) Then  '-receipt-

            '-- Cash sale (not on account-) can now choose A4-
            optConfirmDocket.Checked = True
            cboInvoicePrinters.Enabled = False
            panelDocOpts.Enabled = True

            cboInvoicePrinters.Enabled = False
        Else '-printing invoice-
            '-  On Account-
            '-- Must be A4..
            cboReceiptPrinters.Enabled = False
            optConfirmA4.Checked = True
            panelDocOpts.Enabled = False
        End If
        mbIsInitialising = False

    End Sub  '-Shown-
    '= = = = = = = = = = = =  = = = = = =
    '-===FF->

    '--optConfirmA4_CheckedChanged-
    '-- Can only mean that cash-sale "receipt" can be A4 or not.

    Private Sub optConfirmA4_CheckedChanged(sender As Object, e As EventArgs) _
                                                    Handles optConfirmA4.CheckedChanged, optConfirmDocket.CheckedChanged
        Dim opt1 As RadioButton = CType(sender, RadioButton)
        If opt1.Checked Then
            If LCase(opt1.Name) = "optconfirma4" Then
                cboReceiptPrinters.Enabled = False
                cboInvoicePrinters.Enabled = True
            Else  '--wants docket.
                cboReceiptPrinters.Enabled = True
                cboInvoicePrinters.Enabled = False
            End If
        End If  '--checked-
    End Sub  '-optConfirmA4_CheckedChanged-
    '= = = = =  = = = = = = = = = = = = =
    '-===FF->

    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboInvoicePrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboInvoicePrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboInvoicePrinters.SelectedIndex >= 0) Then
            msInvoicePrinterName = cboInvoicePrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_invoicePrtSettingKey, msInvoicePrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msInvoicePrinterName) Then
                MsgBox("Failed to save invoice printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  InvoicePrinters-
    '= = = = = = = = = = = = =  =

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                           Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
                MsgBox("Failed to save Receipt printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '-ReceiptPrinters-
    '= = = = = = = = = = = = = = = = 
    '-===FF->


    Private Sub chkPrint_CheckedChanged(sender As Object, e As EventArgs) _
                                           Handles chkPrint.CheckedChanged, chkEmail.CheckedChanged
        mbDataChanged = True

    End Sub  '-chkPrint_CheckedChanged-
    '= = = = = = = = = = = = = = =  =



    Private Sub OK_Button_Click(ByVal sender As System.Object,
                                  ByVal e As System.EventArgs) Handles OK_Button.Click
        mbCommitOK = True
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub '=  Ok-
    '= = = = = = = = = =


    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub  '-cancel-
    '= = = = = = = = = = =
    '= = = = = = = = = = = = = 
    '-===FF->

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub dlgQueryCommit_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        '== Call gbLogMsg(gsRuntimeLogPath, "== JobMatixPOS Stock form is closing.." & vbCrLf & vbCrLf & _
        '==                                                  "= = = = = = = = = = = =" & vbCrLf & vbCrLf)
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                               System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                '-- confirm cancel--
                '-- confirm cancel--
                '==     v3.4.3403.1031. 31-Oct-2017= ===
                '==       >> Form Closing..  Close anyway...
                If mbDataChanged And (Not mbCommitOK) Then
                    'If (MsgBox("Abandon changes ", _
                    '         MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                    '    '== mbCancelled = True
                    '    intCancel = 0 '--let it go---
                    'Else  '-stay-
                    '    intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                    'End If
                    intCancel = 0 '--let it go---
                Else  '--not modified
                    intCancel = 0 '--let it go---
                End If  '--modified-
                '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--closing--
    '= = = = = = =  = = = = = = = == 



End Class  '-dlgQueryCommit-


'==  the end ==
