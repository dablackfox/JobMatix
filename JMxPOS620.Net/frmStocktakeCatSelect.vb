Option Strict Off
Option Explicit On


Public Class frmStocktakeCatSelect


    '==
    '==     v3.3.3301.813..  13-August-2016= ===
    '==       >> helper form for Stocktake- 
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = =

    Private mFrmParent As Form
    Private mColCategoriesTree As Collection
    Private msDllVersion As String

    Private msCurrentCat1 As String = ""
    Private mColCurrentCat2 As Collection
    Private msCurrentCat2List As String

    Private mbCancelled As Boolean = False

    '==  RESULTS ==

    ReadOnly Property isCancelled As Boolean
        Get
            isCancelled = mbCancelled
        End Get
    End Property  '-cancelled-
    '= = = = = = = = = =  = = = =

    ReadOnly Property currentCat1 As String
        Get
            currentCat1 = msCurrentCat1
        End Get
    End Property  '-currentCat1=
    '= = = = = = = = = = = = = = = = = = =

    ReadOnly Property colCurrentCat2 As Collection
        Get
            colCurrentCat2 = mColCurrentCat2
        End Get
    End Property  '-colCurrentCat2-
    '= = = = = = = = = = = = = = = = = =  = = =

    ReadOnly Property currentCat2List As String
        Get
            currentCat2List = msCurrentCat2List
        End Get
    End Property  '-currentCat1=
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                        ByRef colCategoriesTree As Collection, _
                        ByVal strDllVersion As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = FrmParent
        mColCategoriesTree = colCategoriesTree

        msDllVersion = strDllVersion

    End Sub  '-new-
    '= = = = = = = = = = ==  = ==


    Private Sub frmStocktakeCatSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim col1, col2 As Collection

        btnCatSelectOk.Enabled = False
        Me.Text = "Selecting Stocktake Categories.. " & msDllVersion
        '== MsgBox("Selecting Stocktake Categories.. " & msDllVersion)

        '-- Populate cat1 combo..
        panelCatSelection.Visible = True
        panelCatSelection.Enabled = True
        '-- load cat1-
        cboSelectCat1.Items.Clear()
        listSelectCat2.Items.Clear()

        For Each col1 In mColCategoriesTree
            '= MsgBox(col1.Item(1))
            cboSelectCat1.Items.Add(col1.Item("cat1name"))
            col2 = col1.Item("cat2children")  '-test=
        Next col1
        cboSelectCat1.SelectedIndex = -1

        Call CenterForm(Me)

    End Sub  '-load-
    '= = = = = =  ==  = = =

    '-activated-

    Private Sub frmStocktakeCatSelect_activated(sender As Object, e As EventArgs) Handles MyBase.Activated


        cboSelectCat1.Select()

    End Sub  '-activated-
    '= = = = = = = = = = = = 
    '-===FF->

    '-- cboSelectCat1_SelectedIndexChanged-

    Private Sub cboSelectCat1_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                       Handles cboSelectCat1.SelectedIndexChanged
        Dim colCat2List As Collection

        '-- reload Cat2  list..-
        listSelectCat2.Items.Clear()

        If (cboSelectCat1.SelectedIndex >= 0) Then
            Dim sCat1 As String = cboSelectCat1.SelectedItem
            If mColCategoriesTree.Contains(sCat1) Then
                colCat2List = mColCategoriesTree(sCat1)("cat2Children")
                For Each s1 As String In colCat2List
                    listSelectCat2.Items.Add(s1)
                Next '-s1-
                listSelectCat2.Enabled = True
            Else
                '--not in collection-
                MsgBox("Error..  can't match that. ", MsgBoxStyle.Exclamation)

            End If  '-contains-

        End If  '-index-

    End Sub  '-cboSelectCat1-
    '= = = = = = = = = = = = = = = =  

    '-- Cat2-

    Private Sub listSelectCat2_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                     Handles listSelectCat2.SelectedIndexChanged
        btnCatSelectOk.Enabled = True

    End Sub '-listSelectCat2-
    '= = = = = = = = = = = = = ==  = =

    '-  Check all-

    Private Sub chkCat2All_CheckedChanged(sender As Object, ev As EventArgs) _
                                               Handles chkCat2All.CheckedChanged
        Dim ix As Integer

        If (listSelectCat2.Items.Count > 0) Then  '--have some-
            If chkCat2All.Checked Then
                '- check all-
                For ix = 0 To (listSelectCat2.Items.Count - 1)
                    listSelectCat2.SetItemChecked(ix, True)
                Next '-ix-
                btnCatSelectOk.Enabled = True
            Else  '- uncheck all-
                For ix = 0 To (listSelectCat2.Items.Count - 1)
                    listSelectCat2.SetItemChecked(ix, False)
                Next '-ix-
                btnCatSelectOk.Enabled = False
            End If
        End If  '-count-
    End Sub '-select all-
    '= = = = = = = =  = = = = = ==
    '-===FF->

    '-- Select OK--
    Private Sub btnCatSelectOk_Click(sender As Object, ev As EventArgs) _
                                        Handles btnCatSelectOk.Click
        '- can go soon-
        '-- get Cat2 checked items..
        '-test-
        Dim s1 As String = cboSelectCat1.SelectedItem
        msCurrentCat1 = s1
        msCurrentCat2List = ""
        mColCurrentCat2 = New Collection
        panelCatSelection.Enabled = False

        '-- save list of cat2 selections..
        For Each s1 In listSelectCat2.CheckedItems
            mColCurrentCat2.Add(Trim(s1), Trim(s1))  '--maust have same val for key.. (so Contains works..)
            msCurrentCat2List &= s1 & ";  "
        Next

        '-TEST-
        '= MsgBox("StockTaking: " & vbCrLf & "Cat1= " & msCurrentCat1 & vbCrLf & _
        '==              " Cat2 are: " & msCurrentCat2List)
        'grpBoxType.Enabled = False

        ''-- load  UNCOUNTED  Stock table items selected (cat1/cat2)..
        ''--  After 1st Item scanned..
        ''-- now can go..

        'grpBoxScanAuto.Enabled = True
        'grpBoxManual.Enabled = True
        'TabControlMain.Enabled = True
        'txtScanBarcode.Enabled = True
        'txtScanBarcodeManual.Enabled = True
        'labPartialSelection.Text = "Cat1: " & msCurrentCat1 & vbCrLf & "Cat2: " & msCurrentCat2List
        'labPartialSelection.Visible = True

        'MsgBox("OK.. We are stocktaking: " & vbCrLf & _
        '       "Cat1: " & msCurrentCat1 & vbCrLf & _
        '       "cat2: " & msCurrentCat2List & vbCrLf & vbCrLf & _
        '       "So Scan first the item to initialise this Stocktake..", MsgBoxStyle.Information)
        ''=  txtScanBarcode.Select()

        Me.Hide()

    End Sub '==  ok-
    '= = = = = = = = = = = = 

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        mbCancelled = True
        Me.Hide()

    End Sub
End Class  '-frmStocktakeCatSelect-

'= = =the end ==