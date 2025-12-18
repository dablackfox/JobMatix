Option Strict Off
Option Explicit On
Friend Class frmPrtSelect
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	
	'== PrtSselect ==
	'== PrtSselect ==
	
	'== Form to select a printer..
    '---JobTracking--24April2009===

    '-- grh ==23-Nov-2011==  UPGRADED  vb.net Version..--
    '-- grh ==21-Apr-2012==  RE-vamp to show all three printers...--

    Private mbActivated As Boolean = False
    Private mbCancelled As Boolean = False

    Private msSelectedName As String = ""
    Private msReceiptSelectedName As String = ""
    Private msLabelSelectedName As String = ""


    'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
    '== Private mPrinter1 As Printer

	Private msCaption As String
	'= = = = = =  = = =  ==
	
	WriteOnly Property msg() As String
		Set(ByVal Value As String)
			
			msCaption = Value
		End Set
	End Property '--caption..--
    '= = = = = = = = = = = = =  =

    '--  Starting values..--

    WriteOnly Property OriginalPrinterName() As String
        Set(ByVal Value As String)

            txtColourPrinter.Text = Value
        End Set
    End Property '--caption..--
    '= = = = = = = = = = = = =  =

    WriteOnly Property OriginalReceiptPrinterName() As String
        Set(ByVal Value As String)

            txtReceiptPrinter.Text = Value
        End Set
    End Property '--caption..--
    '= = = = = = = = = = = = =  =
    WriteOnly Property OriginalLabelPrinterName() As String
        Set(ByVal Value As String)

            txtLabelPrinter.Text = Value
        End Set
    End Property '--caption..--
    '= = = = = = = = = = = = =  =

    '--  Results..-

    '--  return A4 Document printer Name..-
    ReadOnly Property PrinterName() As String
        Get
            PrinterName = msSelectedName
        End Get
    End Property
	'= = = = = = = =  = = =  =

    '--  return Receipt printer Name..-
    ReadOnly Property ReceiptPrinterName() As String
        Get
            ReceiptPrinterName = msReceiptSelectedName
        End Get
    End Property
    '= = = = = = = =  = = =  =

    '--  return Receipt printer Name..-
    ReadOnly Property LabelPrinterName() As String
        Get
            LabelPrinterName = msLabelSelectedName
        End Get
    End Property
    '= = = = = = = =  = = =  =

    'UPGRADE_ISSUE: VB.Printer type was not upgraded. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
    'UPGRADE_ISSUE: VB.Printer type was not upgraded. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
    '== ReadOnly Property selectedPrinter() As Object
    '== 	Get

    '== 		selectedPrinter = mPrinter1

    '== 	End Get
    '== End Property
    '= = = = = = = =  = = =  =


    ReadOnly Property cancelled() As Boolean
        Get

            cancelled = mbCancelled

        End Get
    End Property
    '= = = = = = = =  = = =  =


    '-- EX: load --
    '-- EX: load --

    '== Private Sub mbOriginal_frmPrtSelect_Load()
    Private Sub frmPrtSelect_Load(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
        '== Dim prtx As Printer
        '== Dim sName As String
        Dim i As Integer
        Dim pkInstalledPrinters As String

        '==mbCancelled = False
        cmdOk.Enabled = False
        '--load combo with printeres..--
        cboPrtSelect.Items.Clear()
        cboReceiptPrtSelect.Items.Clear()
        cboLabelPrtSelect.Items.Clear()

        'UPGRADE_ISSUE: Printers object was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
        '== For Each prtx In Printers
        '== 'UPGRADE_ISSUE: Printer property prtx.DeviceName was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
        '== cboPrtSelect.Items.Add(prtx.DeviceName) '--DriverName
        '== Next prtx '--prtx.--

        For i = 0 To (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1)
            pkInstalledPrinters = System.Drawing.Printing.PrinterSettings. _
                                                     InstalledPrinters.Item(i)
            '== ListBox1.Items.Add(pkInstalledPrinters)
            cboPrtSelect.Items.Add(pkInstalledPrinters)
            cboReceiptPrtSelect.Items.Add(pkInstalledPrinters)
            cboLabelPrtSelect.Items.Add(pkInstalledPrinters)
        Next

        cboPrtSelect.Visible = True
        cboReceiptPrtSelect.Visible = True
        cboLabelPrtSelect.Visible = True
        '== msCaption = "Select Printer from list.."
        Me.Text = "JobMatix Printer Assignment"

        Call CenterForm(Me)
        System.Windows.Forms.Application.DoEvents()

    End Sub '-- load--
    '= = = = = = = = =  =

    '== 'UPGRADE_WARNING: Form event frmPrtSelect.Activate has a new behavior. Click for more: 
    '==   'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'

    '== Private Sub frmPrtSelect_Activated(ByVal eventSender As System.Object, _
    '==   ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

    '==Private Sub frmPrtSelect_Load(ByVal eventSender As System.Object, _
    '==                                  ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    Private Sub frmPrtSelect_Activated(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        '== Call mbOriginal_frmPrtSelect_Load()
        If mbActivated Then Exit Sub
        mbActivated = True

        '== LabMsg.Text = msCaption

        If cboPrtSelect.Items.Count > 0 Then
            cboPrtSelect.SelectedIndex = -1 '-- set up empty item..-
            cboReceiptPrtSelect.SelectedIndex = -1 '-- set up empty item..-
            cboLabelPrtSelect.SelectedIndex = -1 '-- set up empty item..-
        Else
            MsgBox("No printers are defined..", MsgBoxStyle.Exclamation)
            mbCancelled = True
            Me.Hide()
        End If
    End Sub '--activate--
    '= = = = = = = = = ==
    '-===FF->

    '--PRT COMBO..  click on prt name..--
    '----  select printer for "miPrtIndex" function..--

    'UPGRADE_WARNING: Event cboPrtSelect.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboPrtSelect_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                       ByVal eventArgs As System.EventArgs) _
                                                           Handles cboPrtSelect.SelectedIndexChanged
        'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
        '== Dim prtx As Printer

        With cboPrtSelect
            msSelectedName = VB6.GetItemString(cboPrtSelect, .SelectedIndex)
        End With
        txtColourPrinter.Text = msSelectedName
        '-- find selected printer..

        '-- find selected printer..
        'UPGRADE_ISSUE: Printers object was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
        '== For Each prtx In Printers
        '== 'UPGRADE_ISSUE: Printer property prtx.DeviceName was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
        '== If UCase(prtx.DeviceName) = UCase(msSelectedName) Then '--found..-
        '== mPrinter1 = prtx
        '== Exit For
        '== End If
        '== Next prtx '--prtx.--

        cmdOk.Enabled = True
        '--cboPrtSelect.Visible = False

    End Sub '--cbo click.-
    '= = = = = = = = = ==

    Private Sub cboReceiptPrtSelect_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                         ByVal eventArgs As System.EventArgs) _
                                                         Handles cboReceiptPrtSelect.SelectedIndexChanged
        With cboReceiptPrtSelect
            msReceiptSelectedName = VB6.GetItemString(cboReceiptPrtSelect, .SelectedIndex)
        End With
        txtReceiptPrinter.Text = msReceiptSelectedName

        '-- find selected printer..
        cmdOk.Enabled = True
    End Sub '--cbo click.-
    '= = = = = = = = = ==

    Private Sub cboLabelPrtSelect_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                         ByVal eventArgs As System.EventArgs) _
                                                         Handles cboLabelPrtSelect.SelectedIndexChanged
        With cboLabelPrtSelect
            msLabelSelectedName = VB6.GetItemString(cboLabelPrtSelect, .SelectedIndex)
        End With
        txtLabelPrinter.Text = msLabelSelectedName

        '-- find selected printer..
        cmdOk.Enabled = True
    End Sub '--cbo click.-
    '= = = = = = = = = ==
    '-===FF->

    '-- cmds..--

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        Me.Hide()
    End Sub '--ok..--
    '= = = = =  = =  = = =


    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                            ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        mbCancelled = True
        Me.Hide()

    End Sub '--cancel--
    '= = = = =  = =  = = =
    '= = = = =  = =  = = =

    '=== end form..==
End Class