
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports system.data.OleDb
Imports System.ComponentModel

Public Class frmStockLabels

    '==
    '==  grh. JobMatix 3.1.3103.0112 ---  12-Jan-2015 ===
    '==   >>  Label printing.... 
    '==     from Admin or from GoodsReceived..
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '== = =
    '==     v3.4.3403.521..  21-May-2017= ===
    '==       >>  Fix to Lookup Columns list...
    '==
    '== = =
    '==     v3.5.3501.0913.. 13-Sep-2018= ===
    '==       >>  Fix to Cancel/print Buttons (Set causesValidation off)....
    '==       >> Change Activated Event to "Shown"  to stop duplication of form being re-shown... 
    '==
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==
    '==   Updated.- 3519.0207 07-Feb-2019= 
    '==     -- Fixes to frmStocklabels to show correct RRP (Not sell_ex)-
    '==            AND implement rrp rounding.
    '==
    '==   Updated.- 3519.0227  Started 26-Feb-2019= 
    '==     -- Update to frmStockLabels to Fix Rounding. (Must decimal.round to 2 decimals before our rounding.).
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  Target is new Build 4233..
    '==  Target is new Build 4233..
    '==
    '==  Target is new Build 4233.0421.
    '==   
    '==   -- frmStockLabels-  Re-design input b/code, qty as textbox line,   
    '==             to avoid editing in grid and crashing on qty.
    '==
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==
    '==
    '==   6. FIXED-  In frmStockLabels, make numericUp/down control NOT readOnly, so user can enter a Number.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = = = = = == = = 
    '==
    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..
    '==
    '==   28-June-2020..
    '==   Stock Labels- bug reported- user is trapped in loop if Invalid barcode entered (Stewart 28/6/2020)..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==
    '== A.  POS System  30-Jul-2020
    '==
    '== (c) Stock Labels (Stewart 17Jul2020).. 
    '==    --  When focussing  on Qty Up/Down, select text content for easy over-writing.. 
    '==         (a TextBox will replace the NumericUpDown control.)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==

    '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
    '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
    '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
    '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
    '-- MUST fix==
    Private Const k_labelPrtSettingKey As String = "POS_LABELPRINTER"

    '-- GoodsReceived DataGridView columns.--
    Private Const k_GRIDCOL_BARCODE As Short = 0
    Private Const k_GRIDCOL_QTY As Short = 1
    Private Const k_GRIDCOL_CAT1 As Short = 2
    Private Const k_GRIDCOL_CAT2 As Short = 3
    Private Const k_GRIDCOL_DESCRIPTION As Short = 4
    Private Const k_GRIDCOL_RRP As Short = 5
    Private Const k_GRIDCOL_STOCK_ID As Short = 6  '--hidden-

    Private mbIsInitialising As Boolean = True

    Private mFrmParent As Form
    Private mbActivated As Boolean = False   '-to activate once only.-

    Private msServer As String = ""
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = 0

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    '--inputs--

    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--
    '= Private mlJobId As Integer = -1
    Private mColPrefsCustomer As Collection
    Private mImageUserLogo As Image

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private mColInputStockItems As Collection '-from Goods Received.-

    Private msLabelPrinterName As String = ""
    '== Private msLabelPrinterName As String = ""
    Private msDefaultPrinterName As String = ""

    '==3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msBusinessABN As String
    Private msBusinessName As String
    Private msBarcodeFontName As String = "IDAutomationHC39M" '-default-
    Private mIntBarcodeFontSize As Integer = 7  '-default-

    Private mDecSell_margin As Decimal = 10D
    Private mDecGST_rate As Decimal = 10D    '--temp. get from setup.

    Private mbIsPrinted As Boolean = False

    Private mColPrefsStock As Collection

    '= = = = = = = = = = = = = = = = = = = = = 

    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                         ByVal sVersionPOS As String, _
                          ByRef imageUserLogo As Image, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String, _
                               ByRef colInputStockItems As Collection)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = FrmParent

        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        msVersionPOS = sVersionPOS

        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName
        mColInputStockItems = colInputStockItems

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '- IMPORTED from GoodsREceived..

    Private Function mDecGetRoundingAmount(ByVal decAmountToBeRounded As Decimal) As Decimal
        '-  Compute Rounding..--
        Dim decRounding As Decimal = 0
        Dim intCentsRounding As Integer = 0
        Dim intCents1 As Integer
        intCents1 = (decAmountToBeRounded * 100) Mod 10  '--get original cents.
        Select Case intCents1
            Case 1, 6 : intCentsRounding = -1
            Case 2, 7 : intCentsRounding = -2
            Case 3, 8 : intCentsRounding = 2
            Case 4, 9 : intCentsRounding = 1
        End Select
        decRounding = (intCentsRounding / 100)   '== make 0.0d  --
        mDecGetRoundingAmount = decRounding

    End Function  '-mIntGetRoundingCents-
    '= = = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- Browse Selected table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByVal sTableName As String, _
                                      ByRef colPrefs As Collection, _
                                       ByRef sTitle As String, _
                                        ByRef sWhere As String, _
                                         ByRef colKeys As Collection, _
                                           ByRef colSelectedRow As Collection) As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = =
    '-===FF->

    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition)..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseAndSearchTable(ByRef colPrefs As Collection, _
                                           ByRef sTitle As String, _
                                            ByRef sWhere As String, _
                                            ByRef colKeys As Collection, _
                                            ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Supplier") As Boolean
        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --

        mbBrowseAndSearchTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        'If bHideEditButtons Then  '=3403.715- Default has changed-
        '    frmBrowse1.lookupSelection = True
        '    frmBrowse1.HideEditButtons = True
        'Else '--need to edit..
        '    frmBrowse1.lookupSelection = False
        '    frmBrowse1.HideEditButtons = False
        'End If
        'frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()


    End Function  '-mbBrowseAndSearchTable-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->


    '-  load stock info grid row..--

    Private Function mbSetupStockItem(ByRef dataTable1 As DataTable, _
                                           ByVal intGridRow As Integer, _
                                           Optional ByVal intQty As Integer = 1) As Boolean
        Dim row1 As DataRow
        Dim intStock_id As Integer
        Dim sBarcode, sSalesTaxCode As String
        Dim decSellExTax, decSellTaxAmount As Decimal
        Dim decSellIncTax As Decimal '=="RRP"-
        Dim decItemQty As Decimal = 1   '--default qty--

        Dim yBinaryData() As Byte
        Dim image1 As Image

        row1 = dataTable1.Rows(0)
        '= Call mbClearItemEntry()
        intStock_id = row1.Item("stock_id")
        '- check if already set up..=
        If CInt(dgvStockItems.Rows(intGridRow).Cells(k_GRIDCOL_STOCK_ID).Value) = intStock_id Then
            Exit Function  '--don't over-write existing info..-
        End If

        sBarcode = row1.Item("barcode")
        '= sGoodsTaxcode = UCase(row1.Item("Goods_taxcode"))
        sSalesTaxCode = UCase(row1.Item("Sales_taxcode"))

        decSellExTax = CDec(row1.Item("sellExTax"))  '--rrp ex tax-

        '-- get GST rate for this tax code..
        '-mDecGST_rate-
        '- compute rrp incl. -
        decSellTaxAmount = ((decSellExTax * mDecGST_rate / 100))
        decSellIncTax = decSellExTax + decSellTaxAmount

        '==
        '==   Updated.- 3519.0207 07-Feb-2019= 
        '==     -- Fixes to frmStocklabels to show correct RRP (Not sell_ex)-
        '-  Compute Rounding..--
        'Dim decRounding As Decimal = 0
        'Dim intCentsRounding As Integer = 0
        'Dim intCents1 As Integer
        'intCents1 = (decSellIncTax * 100) Mod 10  '--get original cents.
        'Select Case intCents1
        '    Case 1, 6 : intCentsRounding = -1
        '    Case 2, 7 : intCentsRounding = -2
        '    Case 3, 8 : intCentsRounding = 2
        '    Case 4, 9 : intCentsRounding = 1
        'End Select
        'decRounding = (intCentsRounding / 100)   '== make 0.0d  --
        'decSellIncTax += decRounding

        '=3519.0227= ROUNDING.
        '--   MUST round to 2 decimals first.
        decSellIncTax = Decimal.Round(decSellIncTax, 2)
        decSellIncTax += mDecGetRoundingAmount(decSellIncTax)

        '- show values-
        '=dgv1.Rows(rx).HeaderCell.Value = (rx + 1).ToString  '== CStr(rx + 1)

        With dgvStockItems.Rows(intGridRow)
            '= .HeaderCell.Value = (intGridRow + 1).ToString  '--number the rows.-
            .Cells(k_GRIDCOL_BARCODE).Value = row1.Item("barcode")
            .Cells(k_GRIDCOL_CAT1).Value = row1.Item("cat1")
            .Cells(k_GRIDCOL_CAT2).Value = row1.Item("cat2")
            .Cells(k_GRIDCOL_DESCRIPTION).Value = row1.Item("description")
            '==3519.0207  .Cells(k_GRIDCOL_RRP).Value = FormatCurrency(decSellExTax, 2)
            .Cells(k_GRIDCOL_RRP).Value = FormatCurrency(decSellIncTax, 2)
            .Cells(k_GRIDCOL_QTY).Value = "1"

            '--  NEW for 4233---
            '--  NEW for 4233---
            '==  Target is new Build 4233..
            '==  Target is new Build 4233..
            '==  Target is new Build 4233..
            .Cells(k_GRIDCOL_QTY).Value = CStr(intQty)  '= CStr(UpDownQty.Value)

            '-- Hidden--
            .Cells(k_GRIDCOL_STOCK_ID).Value = CStr(row1.Item("stock_id"))

            '-- get tax, price and picture.--
            '== txtItemTax.Text = row1.Item("sales_TaxCode")
        End With  '--dgvSaleItems.Rows-
        btnPrint.Enabled = True
        DoEvents()
        mbIsPrinted = False
    End Function  '--SetupGoodsStockItem-
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

    '-- load -

    Private Sub frmStockLabels_Load(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Load
        '=3301.428= Dim colSystemInfo As Collection
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String

        btnPrint.Enabled = False
        dgvStockItems.Rows.Clear()

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3301.428=If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        '-msBarcodeFontName 
        msBarcodeFontName = mSysInfo1.item("POS_BARCODEFONTNAME")
        s1 = mSysInfo1.item("POS_BARCODEFONTSIZE")
        If IsNumeric(s1) Then
            mIntBarcodeFontSize = CInt(s1)
        End If
        If mSysInfo1.contains("POS_SELL_MARGIN") Then
            s1 = mSysInfo1.item("POS_SELL_MARGIN")
            If IsNumeric(s1) Then
                mDecSell_margin = CDec(s1)
            End If
        End If
        If mSysInfo1.contains("GSTPercentage") Then
            s1 = mSysInfo1.item("GSTPercentage")
            '-mdecGST_percentage
            If IsNumeric(s1) Then
                mDecGST_rate = CDec(s1)
            End If
        End If
        '=3301.428= End If  '-load sys info--

        '==3301.428= Local Settings-
        msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '- get printers collection and set up combos.
        cboLabelPrinters.Items.Clear()

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboLabelPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
            '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
            '==  hey!  mLocalSettings1.EXISTS key is case Sensitive--
            If mLocalSettings1.exists(k_labelPrtSettingKey) AndAlso _
                    (mLocalSettings1.item(k_labelPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_labelPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_labelPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboLabelPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboLabelPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboLabelPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
        End If '-getAvail.-  

        '- position on top of calling form..
        'If mFrmParent Is Nothing Then
        Call CenterForm(Me)
        'Else
        '    Me.Left = mFrmParent.Left + 16
        '    Me.Top = mFrmParent.Top + 33
        'End If

        '--  NEW for 4233---
        '--  NEW for 4233---
        '==  Target is new Build 4233..
        '==  Target is new Build 4233..
        '==  Target is new Build 4233..

        mColPrefsStock = New Collection
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("cat1")   '--fkey-
        mColPrefsStock.Add("cat2")   '-fkey-
        mColPrefsStock.Add("description")
        '= colPrefsStock.Add("productPicture")
        mColPrefsStock.Add("stock_id")
        mColPrefsStock.Add("isNonStockItem")
        mColPrefsStock.Add("track_serial")
        mColPrefsStock.Add("inactive")

        txtStockItemBarcode.Text = ""
        txtStockItemDescription.Text = ""
        btnStockLineOk.Enabled = False

        Me.Text = "Printing Barcodes..  " & msVersionPOS

    End Sub  '-load -
    '= = = = = = = = = = = = =
    '-===FF->

    '== A c t i v a t e d  ==
    Private Sub frmStockLabels_Activated(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '--activated.
    '= = = = = = = =  == = 

    Private Sub frmStockLabels_Shown(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim s1, sSql, sBarcode, sQty As String
        Dim intQty, intRow As Integer
        Dim dataTable1 As DataTable
        Dim gridRow1 As DataGridViewRow

        '-- Load grid from GoodsReceived..  If Any..
        '-- input is collection of collection ( barcode, qty) --
        intRow = 0
        If (Not (mColInputStockItems Is Nothing)) AndAlso (mColInputStockItems.Count > 0) Then
            For Each col1 As Collection In mColInputStockItems
                sBarcode = col1.Item("barcode")
                sQty = col1.Item("qty")
                '-- lookup stock info (descr., RRP) etc--
                If (sBarcode <> "") Then  '--have barcode-
                    '--lookup barcode-
                    sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
                    If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                                           (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                        '-- add row to grid..
                        gridRow1 = New DataGridViewRow
                        dgvStockItems.Rows.Add(gridRow1)
                        '--have a row..-
                        Call mbSetupStockItem(dataTable1, intRow)
                    Else '--not found..-
                        MsgBox("No Stock record found for barcode: '" & sBarcode & "' !" & _
                                 vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    End If  '-get--
                End If  '--have barcode-
                '--insert qty..-
                dgvStockItems.Rows(intRow).Cells(k_GRIDCOL_QTY).Value = sQty
                intRow += 1
            Next col1
            mbIsPrinted = False
        End If  '-items-
        '== labHelp.Text = "Scan stock barcodes to print (F2 to lookup)."

        '--  NEW for 4233---
        '==  Target is new Build 4233..
        '==  Target is new Build 4233..
        '==  Target is new Build 4233..

        txtStockItemBarcode.Select()
        mbIsInitialising = False

    End Sub  '-SHOWN -
    '= = = = = = = = = = = 

    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cbolabelPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboLabelPrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboLabelPrinters.SelectedIndex >= 0) Then
            msLabelPrinterName = cboLabelPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_labelPrtSettingKey, msLabelPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_labelPrtSettingKey, msLabelPrinterName) Then
                MsgBox("Failed to save Label printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  labelPrinters-
    '= = = = = = = = = = = = =  =


    '-- ALL THIS FROM GoodsReceived...
    '-- ALL THIS FROM GoodsReceived...


    '--  Barcode Entry and Lookup..
    '--  Barcode Entry and Lookup..
    '--  Barcode Entry and Lookup..


    Private Sub txtStockItemBarcode_TextChanged(sender As Object, _
                                                 ev As EventArgs) Handles txtStockItemBarcode.TextChanged
        If mbIsInitialising Then Exit Sub

    End Sub  '-txtStockItemBarcode_TextChanged-
    '= = = = = =  = = = = = = = = = = = = 


    '-- Textbox Enter control for Item barcode.

    Private Sub txtStockItemBarcode_Enter(sender As Object, _
                                           ev As System.EventArgs) Handles txtStockItemBarcode.Enter
        If mbIsInitialising Then Exit Sub

        '= txtStockItemSupplierCode.Text = ""

        If txtStockItemBarcode.Text = "" Then
            txtStockItemBarcode.Text = "Barcode"
        End If
        txtStockItemBarcode.SelectionStart = 0
        txtStockItemBarcode.SelectionLength = Len(txtStockItemBarcode.Text)

        btnStockLineOk.Enabled = False

    End Sub '-txtSaleItemBarcode_Enter-
    '= = = = = = = = = = = = = = = = = ==

    '==-- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....

    Private Sub txtStockItemBarcode_Click(sender As Object, _
                                          ev As System.EventArgs) Handles txtStockItemBarcode.Click

        If mbIsInitialising Then Exit Sub
        txtStockItemBarcode.SelectionStart = 0
        txtStockItemBarcode.SelectionLength = Len(txtStockItemBarcode.Text)

    End Sub  '-txtSaleItemBarcode_Click-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Static dataTable with current stock Item.
    Private mDataTableStockSelected As DataTable

    '-- Stock barcode validation.
    '-- Returns false if canceleld or invalid..
    '==
    '==   Updated.- 3519.0207 07-Feb-2019= 
    '==     -- Fixes to GoodsReceived to strip leading zeroes from scanned barcode id=f necessary.-
    '==

    Private Function mbStockBarcodeValidate(ByVal sInputBarcode As String, _
                                            ByRef sFinalBarcode As String) As Boolean
        Dim s1, sBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        Dim bOk As Boolean = False
        Dim bCancelled As Boolean = False
        Dim intStock_id As Integer
        Dim bHasLeadZeroes As Boolean = False
        '= Dim bDoneLookUp As Boolean = False

        sBarcode = sInputBarcode
        mbStockBarcodeValidate = False
        bHasLeadZeroes = (VB.Left(sInputBarcode, 1) = "0")
        sBarcode = sInputBarcode
        '-- start with stripping leading zeroes..
        While (VB.Left(sBarcode, 1) = "0")
            sBarcode = Mid(sBarcode, 2)
        End While
        '--lookup barcode-
        '=3301.816== Give user the chance to update stock table-
        Dim result1 As MsgBoxResult = MsgBoxResult.Yes
        '= Dim frmStock1 As frmStock
        Do
            'sSql = "SELECT *, supcode FROM dbo.stock "
            'sSql &= "  LEFT OUTER JOIN dbo.SupplierCode AS SC "
            'sSql &= "      ON (SC.supplier_id=" & CStr(mIntSupplier_id) & ") "
            'sSql &= "           AND (SC.stock_id=stock.stock_id) "
            sSql = "SELECT * FROM dbo.stock "
            sSql &= "WHERE  (barcode='" & sBarcode & "');"

            If gbGetDataTable(mCnnSql, mDataTableStockSelected, sSql) AndAlso _
                 (Not (mDataTableStockSelected Is Nothing)) AndAlso (mDataTableStockSelected.Rows.Count > 0) Then
                '- FOUND- have a row..-
                '- validated can finish..
                bCancelled = False   '=ev.Cancel = False   '--ok to go-
                result1 = MsgBoxResult.No  '--And drop out of loop..
            Else '--not found..-
                If bHasLeadZeroes And (VB.Left(sBarcode, 1) <> "0") Then  '-failed stripped version
                    sBarcode = sInputBarcode   '--try original unstripped..
                    result1 = MsgBoxResult.Yes  '-for retry.
                Else  '- no lead zeroes, or failed raw version.
                    'result1 = MsgBox("No Stock record found for barcode: '" & sBarcode & "' !" & vbCrLf & _
                    '                "Do you want to add this stock item?", _
                    '                MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question)
                    'If (result1 = MsgBoxResult.Yes) Then '- wants to add item.
                    '    '--call stock admin-
                    'Else  '-no- so -post error and exit 

                    bCancelled = True   '= ev.Cancel = True

                    '==  Target-New-Build-4253..
                    '==  Target-New-Build-4253..
                    result1 = MsgBoxResult.No  '--And drop out of loop..

                    MsgBox("No Stock record found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                    'End If
                End If  '--zeroes-
            End If  '-get--
        Loop Until (result1 <> MsgBoxResult.Yes)
        If Not bCancelled Then
            sFinalBarcode = sBarcode
            mbStockBarcodeValidate = True
        End If
    End Function  '== barcode validate-
    '= = = = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- Stock Item Search (F2)..--

    '-- TEXT BOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Private Sub txtStockItemBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtStockItemBarcode.KeyDown
        If mbIsInitialising Then Exit Sub

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode, sBarcode0, sSql, s1, sErrorMsg As String
        Dim intStock_id As Integer
        '= Dim gridrow1 As DataGridViewRow
        Dim colPrefsStock As Collection = mColPrefsStock  '=3301.816=

        intStock_id = -1
        '= msSupplierName = ""
        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup stock--
            If Not mbBrowseAndSearchTable(colPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "stock") Then
                '= mbBrowseTable("Stock", colPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, True) Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else '-ok-
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    '== MsgBox("Selected : " & colKeys(1))
                    intStock_id = CInt(colKeys(1))  '--save pkey as data..
                    If colSelectedRow.Contains("barcode") Then
                        sBarcode0 = colSelectedRow.Item("barcode")("value")
                    Else
                        MsgBox("No value in selected row for: barcode..  ", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                    sBarcode = Replace(sBarcode0, "'", "")  ''--strip quotes-
                    If (sBarcode <> "") Then
                        '-- get selected stock details..-
                        '== sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
                        'sSql = "SELECT *, supcode FROM dbo.stock "
                        'sSql &= "  LEFT OUTER JOIN dbo.SupplierCode AS SC "
                        'sSql &= "      ON (SC.supplier_id=" & CStr(mIntSupplier_id) & ") "
                        'sSql &= "           AND (SC.stock_id=stock.stock_id) "
                        sSql = "SELECT * FROM dbo.stock "
                        sSql &= "WHERE  (barcode='" & sBarcode & "');"
                        If gbGetDataTable(mCnnSql, mDataTableStockSelected, sSql) Then
                            If (Not (mDataTableStockSelected Is Nothing)) AndAlso (mDataTableStockSelected.Rows.Count > 0) Then
                                '-- stuff selected barcode into grid's  textbox..
                                txtBarcode.Text = mDataTableStockSelected.Rows(0).Item("barcode")
                                '-- show description.. allow OK..
                                txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                                btnStockLineOk.Enabled = True
                                '= labHelp2.Text = ""
                                DoEvents()
                                '= btnStockLineOk.Select()

                                '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
                                '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
                                '==UpDownQty.Select()
                                txtQuantity.Select()
                                '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)

                            Else  '=If (dataTable1.Rows.Count <= 0) Then
                                MsgBox("No Stock data row returned for barcode: " & sBarcode & _
                                            vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                            End If  '-nothing-
                        Else
                            sErrorMsg = gsGetLastSqlErrorMessage()
                            MsgBox("ERROR: No Stock datatable returned for barcode: " & sBarcode & _
                                      vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                            '== End If
                        End If '--get table-
                    Else
                        MsgBox("ERROR: Invalid stock barcode: <<" & sBarcode0 & ">>", MsgBoxStyle.Exclamation)
                    End If  '--barcode-
                End If  '--keys-
            End If '-browse-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F5) And _
                          ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '-ADD NEW stock (Autogen barcode.)--
            '--call stock admin-
            '-ADD NEW stock (Autogen barcode.)--
        ElseIf (KeyCode = System.Windows.Forms.Keys.F10) And _
                          ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--goto commit--
            'If btnGoodsCommit.Enabled Then
            '    btnGoodsCommit.Select()
            'End If
        End If  '-keycode.
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Handle ENTER for all Line Item textboxes..
    '--   txtSaleItemBarcode-  Enter Pressed --

    Private Sub txtStockItemBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtStockItemBarcode.KeyPress
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1, sBarcode, sFinalBarcode, sSq As String

        If (keyAscii = 13) Then '--enter-
            '--  If this is barcode, then check if valid etc...-
            If (LCase(textBox1.Name) = "txtstockitembarcode") Then
                keyAscii = 0
                eventArgs.Handled = True
                '-- go to next fld-
                '--  Just use validating--
                '= controlParent.SelectNextControl(textBox1, True, True, True, True)

                SendKeys.Send("{TAB}")
                'If LCase(sBarcode) <> "barcode" And (sBarcode <> "") Then '-have a barcode-
                '    '--  Must validate here--
                '    If Not mbStockBarcodeValidate(sBarcode) Then
                '        MsgBox("ERROR: Invalid stock barcode: <<" & sBarcode & ">>", MsgBoxStyle.Exclamation)
                '    Else '--ok-
                '        txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                '        btnStockLineOk.Enabled = True
                '        btnStockLineOk.Select()
                '    End If
                'Else  '--empty-
                '    '- no commit for view PO..-
                '    If (mbIsPurchaseOrder And mbIsNewPO) And btnGoodsCommit.Enabled Then
                '        btnGoodsCommit.Select()
                '    ElseIf (Not mbIsPurchaseOrder) And panelGoodsFooter.Enabled Then
                '        txtTotalExpected.Select()
                '    End If
                'End If  '-have barcode-
                '= SendKeys.Send("{TAB}")
            End If  '-barcode-
        End If  '-13-
    End Sub  '-txtSaleItemBarcode_KeyPress-
    '= = = = = = = =  = = = = = = = = == == 
    '-===FF->

    '-- Handle Validating for Line Item barcode..

    Private Sub txtSaleItemBarcode_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs) _
                                                 Handles txtStockItemBarcode.Validating
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sBarcode, sFinalBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        Dim bOk As Boolean = False
        Dim intStock_id As Integer

        sError = ""
        '--  If this is barcode, then check if valid etc...-
        If (LCase(textBox1.Name) = "txtstockitembarcode") Then
            '--ENTER or TAB was pressed...
            sBarcode = sData
            If LCase(sBarcode) <> "barcode" And (sBarcode <> "") Then '-have a barcode-
                If Not mbStockBarcodeValidate(sBarcode, sFinalBarcode) Then
                    ev.Cancel = True
                    MsgBox("ERROR: Invalid stock barcode: <<" & sBarcode & ">>", MsgBoxStyle.Exclamation)
                Else '--ok-
                    btnStockLineOk.Enabled = True
                    textBox1.Text = sFinalBarcode  '- in case striiped.
                    '= btnStockLineOk.Select()
                End If
            Else  '--empty-  
                '- validated can move it on.
                '=txtTotalExpected.Select()
            End If  '-have barcode- 
        End If  '--barcode-

    End Sub  '--txtSaleItemBarcode_Validating-
    '= = = = = = = = = = = = = = = = = = = == 

    '==3307.0218 =
    '-- Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..

    Private Sub txtStockItemBarcode_Validated(ByVal sender As System.Object, _
                                              ByVal ev As System.EventArgs) _
                                                 Handles txtStockItemBarcode.Validated
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sBarcode As String

        If mbIsInitialising Then Exit Sub
        sBarcode = sData
        If LCase(sBarcode) = "barcode" Or (sBarcode = "") Then '-empty barcode-
            'If (mbIsPurchaseOrder And mbIsNewPO) And btnGoodsCommit.Enabled Then
            '    btnGoodsCommit.Select()
            'ElseIf (Not mbIsPurchaseOrder) And panelGoodsFooter.Enabled Then
            '    txtTotalExpected.Select()
            'End If
        Else  '-have barcode-
            If (mDataTableStockSelected IsNot Nothing) Then
                txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                '== NEW STUFF-
                '==    -- 4201.0519.  Purchase Orders to have txtStockItemSupplierCode textbox, 
                '==          and update of SupplierCode Table for New SupplierCode.....
               '= btnStockLineOk.Select()
                '= UpDownQty.Select()
            End If
        End If  '-barcode-

    End Sub  '--txtSaleItemBarcode_Validated-
    '= = = = = = = = = = = = = = = = = = = ==  = = = = = = 
    '-===FF->

    '-- Catch ENTER on UpDownQty..
    '-- Catch ENTER on UpDownQty..

    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)

    'Private Sub upDownQty_KeyPress(ByVal eventSender As System.Object, _
    '                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
    '                                 Handles UpDownQty.KeyPress
    '    If mbIsInitialising Then Exit Sub
    '    Dim keyAscii As Short = Asc(eventArgs.KeyChar)

    '    If (keyAscii = 13) Then '--enter-
    '        keyAscii = 0
    '        eventArgs.Handled = True
    '        '-- go to next fld-
    '        '--  Just use validating--
    '        '= controlParent.SelectNextControl(textBox1, True, True, True, True)

    '        SendKeys.Send("{TAB}")
    '        '= SendKeys.Send("{TAB}")
    '        '= End If  '-barcode-
    '    End If  '-13-
    'End Sub  '-upDownQty_KeyPress-
    '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)
    '= = = = = == = = = = = == == 
    '-===FF->


    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)

    '-- validate txtQuantity

    '-- txtQuantity_Enter-
    '-- txtQuantity_Enter-
    '-- txtQuantity_Enter-

    Private Sub txtQuantity_Enter(sender As Object, _
                                           ev As System.EventArgs) Handles txtQuantity.Enter
        If mbIsInitialising Then Exit Sub

        If txtQuantity.Text = "" Then
            txtQuantity.Text = "Qty"
        End If
        txtQuantity.SelectionStart = 0
        txtQuantity.SelectionLength = Len(txtQuantity.Text)

    End Sub  '- txtQuantity_Enter-
    '= = = = == = = = = = = 

    '== txtQuantity_TextChanged

    Private Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged

    End Sub  '-txtQuantity_TextChanged-
    '= = = = = = = =  = = = = == = = =  =

    '--txtQuantity_KeyPress.

    Private Sub txtQuantity_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                 Handles txtQuantity.KeyPress
        If mbIsInitialising Then Exit Sub
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If (keyAscii = 13) Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            '-- go to next fld-
            '--  Just use validating--
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '--txtQuantity_KeyPress-
    '= = = = = = = = = = = =  == = = 

    '-txtQuantity_Validating=

    Private Sub txtQuantity_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs) Handles txtQuantity.Validating
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim intQty As Integer

        '-- must be numeric and <=100..
        If Integer.TryParse(sData, intQty) Then
            If intQty > 100 Then
                ev.Cancel = True
                MsgBox("ERROR: Invalid Quantity..  must be <= 100..", MsgBoxStyle.Exclamation)
            End If
        Else
            ev.Cancel = True
            MsgBox("ERROR: Invalid Quantity..  must be an integer..", MsgBoxStyle.Exclamation)
        End If
    End Sub  '-txtQuantity_Validating=
    '= = = = = = = = = = = = = = = ==  =
    '== ÉND  Target-New-Build-4259 -- (Started 17-Jul-2020)
    '== ÉND  Target-New-Build-4259 -- (Started 17-Jul-2020)
    '-===FF->


    '-btnStockLineOk_Click-

    Private Sub btnStockLineOk_Click(sender As Object, _
                                       e As EventArgs) Handles btnStockLineOk.Click
        Dim sBarcode, sOriginalSupcode, sNewSupcode, s1 As String
        Dim intNewCol As Integer

        sBarcode = mDataTableStockSelected.Rows(0).Item("barcode")
        sOriginalSupcode = ""
        'If Not IsDBNull(mDataTableStockSelected.Rows(0).Item("supcode")) Then
        '    sOriginalSupcode = mDataTableStockSelected.Rows(0).Item("supcode")
        'End If

        '-- Add a  grid row.-
        Dim intGridRx As Integer = dgvStockItems.Rows.Add()
        dgvStockItems.Rows(intGridRx).Cells(k_GRIDCOL_BARCODE).Value = sBarcode
        DoEvents()

        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '= Call mbSetupStockItem(mDataTableStockSelected, intGridRx, CInt(UpDownQty.Value))
        Call mbSetupStockItem(mDataTableStockSelected, intGridRx, CInt(txtQuantity.Text))
        '== ÉND  Target-New-Build-4259 -- (Started 17-Jul-2020)


        btnStockLineOk.Enabled = False
        txtStockItemBarcode.Text = ""
        txtStockItemDescription.Text = ""

        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '= UpDownQty.Value = "1"
        txtQuantity.Text = "1"
        '== ÉND  Target-New-Build-4259 -- (Started 17-Jul-2020)

        txtStockItemBarcode.Select()

    End Sub  '-btnStockLineOk_Click-
    '= = = = = = = = = = = = = = =  =

    '--END OF Barcode stuff from GOODS RECEIUVED..
    '--END OF Barcode stuff from GOODS RECEIUVED..
    '--END OF Barcode stuff from GOODS RECEIUVED..
    '-===FF->

    '-- GRID events.-

    '- Row Enter-
    'Private Sub dgvStockItems_RowEnter(ByVal sender As Object, _
    '                                     ByVal ev As DataGridViewCellEventArgs) _
    '                                         Handles dgvStockItems.RowEnter
    '    '- go to Barcode column first.-
    '    '= crashes (reentrany error)- dgvStockItems.CurrentCell = Me.dgvStockItems(ev.RowIndex, k_GRIDCOL_BARCODE)
    'End Sub  '-row-enter-
    ''= = = = = =  = = = = == = =  =


    '-- DataGridView Goods-
    '-== F2 Lookup Stock --

    '-- Textbox control has been activated on a cell.-
    '--  set event handlers to deal with the textbox..
    '-- to catch keypress...
    '= = = = = = = = = = = = = = = =
    '-===FF->


    Private Sub btnPrint_Click(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim col1, colLabels As Collection
        Dim intCount, rx As Integer
        Dim clsPrint1 As New clsPrintSaleDocs
        '-"IDAutomationHC39M"-
        Dim sFontName As String = msBarcodeFontName
        Dim intFontSize As Integer = mIntBarcodeFontSize

        If (sFontName = "") Then
            sFontName = "IDAutomationHC39M"  '-default-
            intFontSize = 8
        End If
        If (dgvStockItems.Rows.Count > 0) Then
            colLabels = New Collection
            '-- make label print collection-
            rx = 0
            For Each row1 As DataGridViewRow In dgvStockItems.Rows
                If (Trim(row1.Cells(k_GRIDCOL_BARCODE).Value) <> "") Then
                    intCount = CInt(row1.Cells(k_GRIDCOL_QTY).Value)
                    If (intCount > 0) Then
                        For ix As Integer = 1 To intCount
                            col1 = New Collection
                            '== col1.Add("barcode", "name")
                            col1.Add(row1.Cells(k_GRIDCOL_BARCODE).Value, "barcode")
                            col1.Add(row1.Cells(k_GRIDCOL_DESCRIPTION).Value, "description")
                            '- txtSellIncTax.Text-
                            col1.Add(row1.Cells(k_GRIDCOL_RRP).Value, "price")
                            colLabels.Add(col1, CStr(rx) & "_" & CStr(ix))
                            rx += 1
                        Next ix
                    End If '-count-
                End If
            Next row1
            If clsPrint1.PrintStockLabels(colLabels, msBusinessName, _
                                              msLabelPrinterName, sFontName, intFontSize) Then
                mbIsPrinted = True
            End If
        End If  '-count-
    End Sub '-btnPrint-
    '= = = = = = = = = = 
    '-===FF->

    '-Cancel-

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles btnCancel.Click

        If (Not (mColInputStockItems Is Nothing)) AndAlso _
                         (mColInputStockItems.Count > 0) AndAlso (Not mbIsPrinted) Then
            If (MsgBox("Labels not yet printed.  Sure you want to cancel?", _
                           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                Exit Sub
            Else '-exit-
                Me.Hide()
                Exit Sub
            End If
        Else  '-not goods received..
            Me.Hide()
            Exit Sub
        End If
    End Sub  '--cancel-
    '= = = = = = =  = == = 


End Class  '--frmStockLabels-
'= = = = = = = = = = = = = =

'== end form ==