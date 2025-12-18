Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application

Public Class frmGetPrinter


    '= POS 3403.511.-- 
    '-- To Select a printer-
    '-- Show relevant combo and return selected printer name.
    Private Const k_colourPrtSettingKey As String = "POS_InvoicePrinter"
    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"
    Private Const k_labelPrtSettingKey As String = "POS_LabelPrinter"

    Private mbFormStarting As Boolean = True

    '--  printers--
    '== Private mPrtColour As Printer
    Private msColourPrinterName As String = ""
    Private msDefaultPrinterName As String = ""

    Private msReceiptPrinterName As String = ""
    Private msLabelPrinterName As String = ""
    '= = = = =  = =  = = = = =
    Private msLocalSettingsPath As String = ""
    Private mLocalSettings1 As clsLocalSettings  '==3311.225=
    '= = = = =  = = = = = = = = = = = =

    '==3083.717= Labels
    Private mbUserChangedNumberOfLabels As Boolean = False
    Private mbNumUpDownLabels_Updating As Boolean = False
    '= = = = ===== = = = = = = = = = = = = = = = = = = = = =

    '-Inputs-
    Private mbGetSettingOnly As Boolean = False  '--Just get current setting and return-
    '-- Input selection.. "colour", "receipt" or "label".
    Private msWhichPrinter As String = ""
    Private mIntNumberOfLabels As Integer = 1

    '-outputs-
    Private mbCancelled As Boolean = False
    Private msSelectedPrinterName As String = ""
    '= = = = = = = = = = = = = = = = = = = = =  =
    '-===FF->

    '-- INPUT-

    WriteOnly Property GetSettingOnly() As Boolean  '-(no show.)
        Set(ByVal value As Boolean)
            mbGetSettingOnly = value
        End Set
    End Property
    '= = = = = = = = = = = = = == = = =

    WriteOnly Property WhichPrinter() As String
        Set(ByVal Value As String)
            msWhichPrinter = Value
        End Set
    End Property '--colour.--
    '= = = = = = = =  = = = 

    WriteOnly Property RequestedNumberOfLabels() As Integer
        Set(ByVal value As Integer)
            mIntNumberOfLabels = value
        End Set
    End Property '-no.labels to start-
    '= = = = = = = = = = = = = = = = = =

    '-Result-

    ReadOnly Property SelectedPrinterName() As String
        Get
            SelectedPrinterName = msSelectedPrinterName
        End Get
    End Property '-selectedRow-
    '= = = = = =  = = = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property '-cancelled-
    '= = = = = =  = = = = = = = =

    ReadOnly Property NumberOfLabels() As Integer
        Get
            NumberOfLabels = CInt(NumUpDownLabels.Value)
        End Get
    End Property '-cancelled-
    '= = = = = = = = = = = = = = = = =  = = = = = = = ==
    '-===FF->

    '-- Load-

    Private Sub frmGetPrinter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim intDefaultPrinterIndex As Integer
        Dim colPrinters As Collection
        Dim s1, sName As String

        '-- Combo for Colour printer..
        '=3203.106= get printers.
        cboColourPrinters.Items.Clear()   '--Colour printer..
        cboReceiptPrinters.Items.Clear()
        cboLabelPrinters.Items.Clear()

        msLocalSettingsPath = gsLocalJobsSettingsPath()
        mLocalSettings1 = New clsLocalSettings(msLocalSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
        Else
            '--  load all combos with printers list.
            For Each sName In colPrinters
                cboColourPrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
                cboLabelPrinters.Items.Add(sName)
            Next sName
            '-- A. check local settings (prefs) for COLOUR printer..
            If mLocalSettings1.queryLocalSetting(k_colourPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboColourPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboColourPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboColourPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            msColourPrinterName = cboColourPrinters.SelectedItem

            '-- B. check local settings (prefs) for RECEIPT printer..
            If mLocalSettings1.queryLocalSetting(k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--pref. defined, so set it- 
                    cboReceiptPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem

            '-- C. check local settings (prefs) for LABEL printer..
            If mLocalSettings1.queryLocalSetting(k_labelPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--pref. defined, so set it- 
                    cboLabelPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboLabelPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboLabelPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            msLabelPrinterName = cboLabelPrinters.SelectedItem

        End If '-getAvail.-  

        '=3083.717=
        mbNumUpDownLabels_Updating = True   '--"disable" while we change it.
        NumUpDownLabels.Value = mIntNumberOfLabels
        mbNumUpDownLabels_Updating = False   '-- free..

        mbUserChangedNumberOfLabels = False

        '-- Disable ALL..  then enable the Requested Combo.

        panelPrtColour.Visible = False
        panelPrtReceipt.Visible = False
        panelPrtLabel.Visible = False

        '-- Input selection.. "colour", "receipt" or "label".
        Select Case LCase(msWhichPrinter)
            Case "colour"
                panelPrtColour.Visible = True
                labHdr1.Text = "Select Colour printer"
            Case "receipt"
                panelPrtReceipt.Top = panelPrtColour.Top
                panelPrtReceipt.Height = panelPrtColour.Height
                panelPrtReceipt.Visible = True
                labHdr1.Text = "Select Receipt printer"
            Case "label"
                panelPrtLabel.Top = panelPrtColour.Top
                panelPrtLabel.Height = panelPrtColour.Height
                panelPrtLabel.Visible = True
                labHdr1.Text = "Select Label printer"
            Case Else
                mbCancelled = True
                MsgBox("'GetPrinter'- No valid printer type provided.", MsgBoxStyle.Exclamation)
                Me.Hide()
        End Select

        Call CenterForm(Me)

        mbFormStarting = False

    End Sub  '--load-
    '= = = = ===== = ===== =
    '-===FF->

    '=3203.106= get printer sel..
    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboColourPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboColourPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbFormStarting Then Exit Sub
        If (cboColourPrinters.SelectedIndex >= 0) Then
            msColourPrinterName = cboColourPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_colourPrtSettingKey, msColourPrinterName) Then
                MsgBox("Failed to save COLOUR printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboColourPrinters-
    '= = = = = = = = = = = = = = = = = =

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                     ByVal e As System.EventArgs) _
                                                     Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbFormStarting Then Exit Sub
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
                MsgBox("Failed to save RECEIPT printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboReceiptPrinters-
    '= = = = = = = = = = = == = = = = 

    Private Sub cboLabelPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) _
                                                      Handles cboLabelPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbFormStarting Then Exit Sub
        If (cboLabelPrinters.SelectedIndex >= 0) Then
            msLabelPrinterName = cboLabelPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_labelPrtSettingKey, msLabelPrinterName) Then
                MsgBox("Failed to save Label printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboLabelPrinters-
    '= = = = = = = = = = = = =  =
    '-===FF->

    '-NumUpDownLabels_ValueChanged-

    Private Sub NumUpDownLabels_ValueChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles NumUpDownLabels.ValueChanged

        '==MsgBox("NumUpDownLabels_ValueChanged event fired..", MsgBoxStyle.Information) '--TEST -

        If mbNumUpDownLabels_Updating Or mbFormStarting Then Exit Sub
        '- Was changed by user.
        '== MsgBox("NumUpDownLabels_ValueChanged by user..", MsgBoxStyle.Information) '--TEST -
        mbUserChangedNumberOfLabels = True
    End Sub '--NumUpDownLabels_ValueChanged-
    '= = = = = = = = = = = = = = = = = =

    '--  replace blanks with zero.-
    '-   (bug in control.)
    Private Sub NumUpDownLabels_Validating(ByVal sender As Object, _
                                           ByVal e As System.ComponentModel.CancelEventArgs) _
                                              Handles NumUpDownLabels.Validating
        If NumUpDownLabels.Text = "" Then
            NumUpDownLabels.Value = 0
        End If
    End Sub  '--NumUpDownLabels_Validating-
    '= = = = = = = = = = = = = = = = = = = =

    '-  Exiting -

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        '-- Input selection.. "colour", "receipt" or "label".
        Select Case LCase(msWhichPrinter)
            Case "colour"
                msSelectedPrinterName = msColourPrinterName
            Case "receipt"
                msSelectedPrinterName = msReceiptPrinterName
            Case "label"
                msSelectedPrinterName = msLabelPrinterName
            Case Else
                msSelectedPrinterName = "ERROR No Selection"
        End Select
        Me.Hide()
    End Sub '-pk-
    '= = = = = = = = = = 

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        mbCancelled = True
        Me.Hide()
    End Sub  '-cancel=
    '= = = = = = = = = = = == 

End Class  '-frmGetPrinter-
'= = = = = = = = = = = = == 