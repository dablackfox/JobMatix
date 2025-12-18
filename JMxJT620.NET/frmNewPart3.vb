Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmNewPart
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = = = = =
	
	'-- Form to look up RetailManager stock table..--
	'---- to find Barcode.. --
	
	'--- grh = 28Apr2009== Updated SELECT to JOIN categorisedStock etc to get at CAT#..--
	'--- grh = 12May2009== Strip leading zeroes from BARCODE and allow alphanumerics..--
	'--- grh = 05Jun2009== Form position set by caller...--
	'--- grh = 21Jul2009== Sell price is EX GST..--
	'--- grh = 16Nov2009== Add combo Box for Quantity.---
	'--- grh = 11Jan2010== Add text box for SerialNo.-.---
	'--- grh = 10-Jun-2010== Check that SerialNo exists in MYOB...-.---
	'--- grh = 28-Jun-2010== trim txtPartNo before comparing with MYOB...---
	'--- grh = 29-Jul-2010== Allow F2 Stock Lookup for parts..---
	'-----     ----      Also: Filter for SERVICE items For SERVICE CHARGE items lookup..--
	'--- grh = 05-Aug-2010== Fix form caption for SERVICE.---
	'--- grh = 20-Aug-2010== Fix WHERE condition for F2 on Parts AFTER SERVICE.---
	'--- grh = 23-Dec-2010== SERVICE Charge can have variable Qty..---
	'--- grh  30-Jan-2011= For Cat3, JOIN CategorisedStock ON CategorisedStock.category_level=3 (NOT Cat_id)..-
	'---  V2.2  --
	'--- grh  14-May-2011= Lookup Stock..  can click scanLabel OR use F2..-
	'--- grh  17-Jun-2011= Catch MYOB "allow_renaming" field on looked up part...-
	'----    ---  IF true, then allow user to change description/price..--
	'---   ALSO, now shows AND returns sell price incl GST..-
	'--  Rev-2916--
	'--- grh  05-Aug-2011= Scanning barcode..  ensure is SERVICE item if SERVICE was requested...-
	
	'-- JobMatixV3-- Rev-3010--  MultipleRetailHost version..
	'--- grh  02-Nov-2011=  also prepare for vb.net..
	
    '--- grh  26-Nov-2011=  UPGRADED to vb.net..

    '== grh= 07-Mar-2012== Build 3031==
    '--       Stock Browser now in-house.. ( clsBrowse3 )--
    '--      AND re-position form according to mandate.. 
    '--      AND:  14Mar2012.. Fix stock lookup for QBPOS stock browsing..--
    '== 
    '== grh= 28-Apr-2012== Build 3047.1==
    '--       Fixes to sequencing of textBox's Focus..--
    '== 
    '== grh= 20-May-2013== Build 3077.520==
    '--       >>  ALLOW leading zeroes on PRODUCT BARCODE..--
    '==
    '== grh= 11-Jun-2013== Build 3077.611==
    '==        >>  'chkKeepScannedLeadZeroes'
    '==                (to disable stripping of leading zeroes and spaces..)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '== grh= 13-Oct-2014== Build 3.1.3101.1013==
    '==
    '==      >>  Updates to support JobMatixPOS..
    '==      >>  Lookup Serial must now send Stock_id as well as SerialNo.
    '==
    '== grh= 24-Oct-2014== Build 3.1.3101.1024==
    '==       >>  Add Full text Srch for Stock Browser.
    '==
    '==  3311.302- 02-04 March 2016-
    '==
    '==    >> (mbGetSerialInfo) - Show (popup) SALE/Customer Info (if any) for SerialInfo.
    '==    >>  Catch ENTER key on text search text..
    '==    >>  Lookup button now pulls up Full Stock Browse form..
    '==  3311.422- 22 Apr 2016-
    '==     Drop empty msgBox on Serial-sold report (when not sold)-
    '==
    '==  3327.0117- 17-Jan-2017=
    '==     Fix to getting quantity combo value-
    '==
    '==   >> 3431.0427=  27-Apr-2018= ..
    '==        -- 3431.0427= FIX ALL FORMS to replace "msgbox"  with .Net "MessageBox.Show"..
    '==             and Add "Me" to ShowDialog calls..
    '==         -- FIX ALL FORMS to move "Activated" event stuff to "Shown" event..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    Private mColFields As Collection '-- stock record for caller..--
    Private mIntStock_id As Integer = -1
    Private msSerialNo As String = ""

    Private msServiceChargeCat1 As String = ""
    Private msServiceChargeCat2 As String = ""
	
    Private mbCancelled As Boolean = False
    Private mbServiceChargeRequested As Boolean = False

    Private mbCanTrackSerials As Boolean = False
	
	Private mColItemFields As Collection '-serial no. lookup..-
 
    Private mCurGST As Decimal = 0 '--GST as percentage..-
	
    Private mRetailHost1 As _clsRetailHost

    Private mBrowse1 As clsBrowse3 '== clsBrowse22
    Private mLngSelectedRow As Integer = -1

    Private msStockTableColumnNameCat1 As String = ""
    Private msStockTableColumnNameCat2 As String = ""

    Private mDataGridViewCellStyleHdr As DataGridViewCellStyle
    Private mDataGridViewCellStyleData As DataGridViewCellStyle

    '--  mandated location..
    Private mIntFormTop As Integer = -1
    Private mIntFormLeft As Integer = -1

    '=3311.304=-- Serial No reporting.
    Private msSerialReport As String = ""

	'= = = = = = = = = = =
	
	WriteOnly Property retailHost() As _clsRetailHost
		Set(ByVal Value As _clsRetailHost)
			
			mRetailHost1 = Value
		End Set
	End Property '--host..--
	'= = = = =  = =  = = =
	
	'== Property Let connectionJet(cnn1 As ADODB.connection)
	'==   Set mCnnJet = cnn1
	'== End Property  '--cnn jet..--
	'= = = = = = =  = = = = =
	
	'== Property Let dbInfoJet(dbinfo As Collection)
	'==     Set mColJetDBInfo = dbinfo
	'== End Property  '--info jet..--
	'= = = = = = = = = = = =
	
	WriteOnly Property ServiceChargeRequested() As Boolean
		Set(ByVal Value As Boolean)
			mbServiceChargeRequested = Value
		End Set
	End Property '--service--
	'= = = =  = = = = = = =
	
	WriteOnly Property ServiceChargeCat1() As String
		Set(ByVal Value As String)
			msServiceChargeCat1 = Value
		End Set
	End Property
	'= = = = = = =
	
	WriteOnly Property ServiceChargeCat2() As String
		Set(ByVal Value As String)
			msServiceChargeCat2 = Value
		End Set
	End Property
    '= = = = = = =

    '--Mandated location..-
    WriteOnly Property MandatedFormTop() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormTop = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =
    WriteOnly Property MandatedFormLeft() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormLeft = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =


    '--  GST--
	
	WriteOnly Property GST_Percentage() As Decimal
		Set(ByVal Value As Decimal)
			
			mCurGST = Value
		End Set
	End Property '--GST..-
	'= = = = = = = = = = =
	
	'==  result is keyed fldset of selected record--
	ReadOnly Property selectedRecord() As Collection
		Get
			
			selectedRecord = mColFields
			
		End Get
	End Property '-- get record--
	'= = = = = = = =  = =  ==
	
	'==SERIAL No.result--
	
	ReadOnly Property SerialNo() As String
		Get
			
			SerialNo = Trim(txtSerialNo.Text)
		End Get
    End Property '-- get serial--
	'= = = = = = = =  = =  ==
	
    '==QUANTITY result--
    '-- Changing tex manually seems to kill selected index.
	
	ReadOnly Property quantity() As Integer
		Get
			Dim L1 As Integer
			
            If (cboQty.SelectedIndex >= 0) Then
                quantity = CInt(VB6.GetItemString(cboQty, cboQty.SelectedIndex))
                quantity = CInt(cboQty.Text)
            Else
                '= MsgBox("TESTING: cbo index is: " & cboQty.SelectedIndex, MsgBoxStyle.Information)  '--TEST-
                quantity = CInt(Trim(cboQty.Text))
            End If
        End Get
    End Property '-- get quantity--
	'= = = = = = = =  = =  ==
	
	'==warranty result--
	
	ReadOnly Property warranty() As Boolean
		Get
            warranty = (chkWarranty.CheckState = 1) '--if true--
        End Get
    End Property '-- get warranty--
	'= = = = = = = =  = =  ==
	
	'==  result if cancelled--
	ReadOnly Property cancelled() As Boolean
		Get
            cancelled = mbCancelled
        End Get
	End Property '-- get cancelled--
	'= = = = = = = =  = =  ==
	'-===FF->

	Private Function mbItemIsServiceCharge(ByRef colItem As Collection) As Boolean
		Dim sCat1 As String
		Dim sCat2 As String
		
		mbItemIsServiceCharge = False
        sCat1 = colItem.Item("cat1")("value")
        sCat2 = colItem.Item("cat2")("value")
		
		If (msServiceChargeCat1 <> "") Then
			If LCase(sCat1) = LCase(msServiceChargeCat1) Then
				If (msServiceChargeCat2 <> "") Then
					If LCase(sCat2) = LCase(msServiceChargeCat2) Then
						mbItemIsServiceCharge = True
					End If
				Else '--cat1 only..-
					mbItemIsServiceCharge = True
				End If
			End If
		End If
	End Function '--IsServiceCharge--
	'= = = = = = = = = = = = = ==
	'-===FF->
	
	'--  lookup --
	
	'-- trap ENTER key on part no..--
	'--  lookup barcode--
	Private Function mbLookupProduct() As Boolean
		
        Dim s1 As String
        Dim lngError As Integer
		Dim sName, sSql As String
		Dim col1 As Collection
		Dim sDescr, sCat3 As String
		Dim sLongDescr As String
		Dim sSpecialPrice As String
		Dim curCost As Decimal
		Dim curCostInclGST As Decimal
		
        mbLookupProduct = False
        On Error GoTo LookupProduct_Error
        '-- strip leading zeroes..--
        '==  NO! Build-3077:  ALLOW LEADING ZEROES..
        '==YES- Build-3077.610:  Strip LEADING ZEROES Unless "noStrip" checked..
        If Not chkKeepScannedLeadZeroes.Checked Then
            While (VB.Left(txtPartNo.Text, 1) = "0") Or (VB.Left(txtPartNo.Text, 1) = " ")
                '==    While (VB.Left(txtPartNo.Text, 1) = " ")
                txtPartNo.Text = Mid(txtPartNo.Text, 2)
            End While
        End If  '--checked..-

        s1 = Trim(txtPartNo.Text)
        If (s1 = "") Then Exit Function

        sName = ""
        LabDescription.Text = ""
        '===LabEnterSerialNo.Caption = ""
        txtSerialNo.Enabled = False
        '--lookup RM stock table--
        '-- NEED special JOINs to get at CAT3..---
         If mRetailHost1.stockGetJobPartStockInfo(s1, -1, mColFields) Then
            '==       Call mbShowRecord("-SPECIAL Stock record--", colRecord)
            '--=Rev:2908=  get originals for three special fields..-
            sDescr = Trim(mColFields.Item("originaldescription")("value"))
            sLongDescr = Trim(mColFields.Item("longdesc")("value"))
            curCost = CDec(mColFields.Item("OriginalSell")("value"))

            '-- and create covers to replicate proper RM column names..--
            '--  recreate "sell" still ex GST..--
            col1 = New Collection
            col1.Add("sell", "name")
            col1.Add(curCost, "value")
            If Not mColFields.Contains("sell") Then  '-JonMatixPOS collects "OriginalSell" AND "sell"--
                '-- Actually 2 versions of the same column..-
                mColFields.Add(col1, "sell") '-- for consistency..-
            End If
            '-- AND CREATE "SellInclGst"..
            '-- NOW add GST..--
            curCostInclGST = curCost + ((curCost * mCurGST) / 100)

            '--  confirm part..--
            '--MsgBox "Description: " + sDescr + vbCrLf + _
            '"Cost:  " + CStr(curCost) + vbCrLf, vbInformation
            sCat3 = Trim(mColFields.Item("cat3")("value"))
            LabDescription.Text = "(" & sCat3 & ") " & sDescr & vbCrLf & "Price:  " & FormatCurrency(curCost, 2) & " (Ex GST.)"
            LabDescription.Enabled = True
            LabVerify.Enabled = True
            txtDescription.Text = sDescr
            txtSellInclGST.Text = FormatCurrency(curCostInclGST, 2)
            '-- for Gavin..-
            sSpecialPrice = "No"
            ChkAllowRenaming.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-
            If (mColFields.Item("allow_renaming")("value") = True) Then
                sSpecialPrice = "Yes"
                ChkAllowRenaming.CheckState = System.Windows.Forms.CheckState.Checked '--checked..-
                LabChangePrice.Visible = True
                txtDescription.ReadOnly = False
                txtDescription.TabStop = True
                txtSellInclGST.Enabled = True
                txtSellInclGST.ReadOnly = False
                '--   add marker to LongDescr. for Delivery printout..-
                '==  NO MARKER NEEDED ==  sLongDescr = sLongDescr & ";;;&&&HasSpecialSellingPrice;"
            Else '--no special price..--
                LabChangePrice.Visible = False
                txtDescription.ReadOnly = True
                txtDescription.TabStop = False
                txtSellInclGST.ReadOnly = True
                txtSellInclGST.Enabled = False
            End If
            '== NO MORE==   set up final Long Descr. value..-
            '== Set col1 = New Collection
            '== col1.Add "LongDesc", "name"
            '== col1.Add sLongDescr, "value"
            '== mColFields.Add col1, "LongDesc" '-- for eventual sql INSERT..-

            '--  Set up SpacialPrice ("allow_renaming")..--
            col1 = New Collection
            col1.Add("SpecialPrice", "name")
            col1.Add(sSpecialPrice, "value")
            mColFields.Add(col1, "SpecialPrice") '-- for parts listview...-

            cmdOk.Enabled = True
            '== txtSerialNo.Enabled = True
            txtSerialNo.Text = ""
            cboQty.Enabled = True
            '==LabEnterSerialNo.Caption = "Enter Serial-No (if any) of new Part.."
            '== If Not mbServiceChargeRequested Then LabEnterSerialNo.Visible = True
            mbLookupProduct = True

            '===txtSerialNo.SetFocus
        Else
            MessageBox.Show(Me, "No stock record..", "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If '--lookup..-
        Exit Function

LookupProduct_Error:
        lngError = Err.Number()
        MessageBox.Show(Me, "Runtime Error in JobMatix  NewPart: LookupProduct.." & vbCrLf & _
                "Error is " & lngError & " = " & ErrorToString(lngError), _
                "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Function '--  lookup.--
	'= = = = = = = = = = = = =
    '-===FF->

    '-CheckSerialNumber-

    Private Function mbCheckSerialNumber(ByVal sSerialNo As String, _
                                           ByVal intStock_id As Integer) As Boolean
        Dim s1, s2 As String
        Dim sItemBarcode As String
        Dim colSerialsResult, col1 As Collection
        Dim sHasBeenSold, sNotInStock As String  '--messages.-

        mbCheckSerialNumber = False
        '--  Lookup SerialNo in SerialAudit table..--
        '---- SerialAudit links SerailNo to SerialAuditTrail, which --
        '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--
        sHasBeenSold = ""
        sNotInStock = ""
        msSerialReport = ""
        '==3101.1024=  GetSerialInfo NOW returns a collection of records..
        If mRetailHost1.serialGetSerialInfo(intStock_id, sSerialNo, colSerialsResult) _
                                                      AndAlso (colSerialsResult.Count > 0) Then
            mColItemFields = colSerialsResult.Item(1)   '--first record-
            sItemBarcode = mColItemFields.Item("barcode")("value")
            If UCase(sItemBarcode) <> UCase(Trim(txtPartNo.Text)) Then '--wrong part type.-
                MessageBox.Show(Me, "The SerialNo: " & sSerialNo & " on file " & vbCrLf & _
                       " is related to the product barcode: '" & sItemBarcode & "'.." & vbCrLf & vbCrLf & _
                        "  (not the barcode: '" & txtPartNo.Text & "' that you have entered..)" & vbCrLf & _
                          "Either the Product Barcode or the item SerialNo needs to be changed..", _
                         "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else '--ok-
                '--now check that this SerialNo is actually still in stock..--
                '=3311-302-
                '=3311.302=--  First- Show Sale info if any..
                Dim colSaleInfo As Collection
                If mColItemFields.Contains("item_sale_info") Then
                    colSaleInfo = mColItemFields.Item("item_sale_info")
                    If (colSaleInfo.Count > 0) Then  '-have some-
                        s1 = "NB: This Item seems to have been sold. Sale details are:" & vbCrLf
                        For Each col1 In colSaleInfo
                            s2 = LCase(col1.Item("name"))
                            s1 &= s2 & " :  "
                            If (s2 = "sale_date") Then
                                s1 &= VB.Format(CDate(col1.Item("value")), "dd-MMM-yyyy") & vbCrLf
                            Else
                                s1 &= col1.Item("value") & vbCrLf
                            End If
                        Next col1
                        sHasBeenSold = s1 '= SAVE-  MsgBox(s1, MsgBoxStyle.Exclamation)
                    End If  '-count-
                End If '-sale info--
                '=3311.302--- resume flow--
                s1 = UCase(mColItemFields.Item("InStock")("value"))
                If (s1 = "YES") Then
                    '= mbCheckSerialNumber = True
                Else '--not found.--
                    sNotInStock = "Note: The SerialNo: " & sSerialNo & " is valid OK in RetailManager," & vbCrLf & _
                               "but does NOT appear to be in stock (in the table of Stocked-Serials).." & vbCrLf & vbCrLf & _
                               "Make sure this is the correct part.."
                End If '--lookup stock..-
                msSerialReport = sNotInStock
                If (msSerialReport <> "") Then msSerialReport &= vbCrLf & vbCrLf
                '=3311.422= drop crlf-
                msSerialReport &= sHasBeenSold '==& vbCrLf
                If (msSerialReport <> "") Then msSerialReport &= vbCrLf '==3311.422=
                mbCheckSerialNumber = True
            End If '--barcode..-
        Else '--not found..-
            MessageBox.Show(Me, "Can't find that item SerialNo in RetailManager Database..", _
                            "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If '-Lookup SerialAudit-
    End Function '--CheckSerial--
    '= = = = = = = = = = = = = = = =
    '-===FF->


    Private Function msMakeStockFilter() As String
        Dim sWhere As String = ""

        If mbServiceChargeRequested Then
            If (msStockTableColumnNameCat1 <> "") AndAlso (msServiceChargeCat1 <> "") Then
                sWhere = " (" & msStockTableColumnNameCat1 & "='" & msServiceChargeCat1 & "') "
                If (msStockTableColumnNameCat2 <> "") AndAlso (msServiceChargeCat2 <> "") Then
                    sWhere = sWhere & " AND (" & msStockTableColumnNameCat2 & "='" & msServiceChargeCat2 & "') "
                End If
            End If
        Else  '--stock part.-
            If (msStockTableColumnNameCat1 <> "") AndAlso (msServiceChargeCat1 <> "") Then '--can filter..-
                sWhere = " (" & msStockTableColumnNameCat1 & "<>'" & msServiceChargeCat1 & "') "
            End If
        End If  '--requested-
        msMakeStockFilter = sWhere
    End Function  '--msMakeStockFilter-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  GET READY for Function KEY..
    '--- INITIALISE Stock Browser.for STOCK Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(Optional ByVal sSrchWhereCond As String = "") As Boolean

        Dim colPrefs As Collection
        Dim sHostTablename As String
        Dim sWhere As String

        mBrowse1 = New clsBrowse3 '== clsBrowse22
        '-- show full frame..-
        '== FrameBrowse.Height = (LabDescription.Top - FrameBrowse.Top - 4)
        '== DataGridView1.Height = (FrameBrowse.Height - DataGridView1.Top - 8)

        mBrowse1.connection = mRetailHost1.connection
        mBrowse1.colTables = mRetailHost1.colTables
        mBrowse1.IsSqlServer = mRetailHost1.IsSqlServer
        mBrowse1.DBname = mRetailHost1.DBname

        '--  get table/prefs info for this host..--
        If Not mRetailHost1.browseGetPrefColumns("stock", sHostTablename, colPrefs) Then
            MessageBox.Show(Me, "Can't translate table name to host table..", _
                            "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        mBrowse1.tableName = sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = DataGridView1

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        If (sSrchWhereCond <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= sSrchWhereCond
        End If
        mBrowse1.WhereCondition = sWhere
        mBrowse1.PreferredColumns = colPrefs '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        FrameBrowse.Enabled = True

        mLngSelectedRow = -1
        mBrowse1.Activate() '-- go..--

        '== txtFind.Focus()
        txtStockSearch.Select()  '=3311.302=-- For Stewart..

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  shrink and disable the Grid.--

    Private Function mbHideStockGrid(Optional ByVal intRowSelected As Integer = -1) As Boolean
        Dim ix As Integer
        Dim colHdr1 As DataGridViewColumnHeaderCell

        '-- if row selected, leave it as the visible one.--
        If (intRowSelected >= 0) Then  '--have row..-
            Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.Rows(intRowSelected).Cells(0)
        End If
        FrameBrowse.Height = 139  '== (LabDescription.Top - FrameBrowse.Top - 4)
        DataGridView1.Height = (FrameBrowse.Height - DataGridView1.Top - 8)
        '= LabOk.Top = FrameBrowse.Height - 27
        cmdSelect.Top = FrameBrowse.Height - 27 '= LabOk.Top
        labRecCount.Top = cmdSelect.Top

        For ix = 0 To (DataGridView1.ColumnCount - 1)
            colHdr1 = DataGridView1.Columns(ix).HeaderCell
            If colHdr1.Style.BackColor = Color.Yellow Then
                colHdr1.Style.BackColor = Color.LightGray
            End If
        Next ix
        labRecCount.BackColor = Color.Transparent

        FrameBrowse.Enabled = False
        DataGridView1.Enabled = False
        cmdLookup.Enabled = True

    End Function  '--hide.-
    '= = = =  = =  = =
    '-===FF->

    '-- ORIGINAL load--
	
    Private Sub mbOriginal_frmNewPart_Load()
        Dim ix As Short

        LabDescription.Enabled = False
        LabDescription.Text = ""
        LabVerify.Enabled = False
        cmdOk.Enabled = False
        txtPartNo.Text = ""
        '== mbCancelled = False
        '--Me.Top = ((Screen.Height \ 3) * 2)

        If (mIntFormTop >= 0) Then
            Me.Top = mIntFormTop
        End If
        If (mIntFormLeft >= 0) Then
            Me.Left = mIntFormLeft
        End If

        cboQty.Items.Clear()
        For ix = 1 To 11
            cboQty.Items.Add(VB6.Format(ix, "#0"))
        Next ix
        cboQty.SelectedIndex = 0 '--show qty "1"--
        cboQty.Enabled = False
        txtSerialNo.Enabled = False
        LabEnterSerialNo.Visible = False
        LabEnterSerialNo.Text = ""
        txtSerialNo.Text = ""

        '== Now IS Used !!  -- cmdLookup.Enabled = False '-not used..-
        '== cmdLookup.Visible = False
        LabScanProduct.Enabled = False

        LabVerify.Text = "Verify that this is the correct part. " & _
                         "Enter Serial-No if available..  Select Qty, and press Finish to complete. " & vbCrLf & vbCrLf & _
                         " = MYOB Users Note: the Serial-No and the Product-Barcode must match in the MYOB Database.."

        '--  If use can change price when part added..--
        '== mCurGST = 1# '--default..-

        ChkAllowRenaming.Enabled = False
        ChkAllowRenaming.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-

        FrameBrowse.Text = "Stock Browser"
        LabFind.Text = ""
        txtStockSearch.Text = ""

        FrameBrowse.Enabled = False

        With DataGridView1.RowTemplate
            .DefaultCellStyle.BackColor = Color.LightGray
            .Height = 18
            .MinimumHeight = 18
        End With

        '-- FORCE datagridView cell style to stick..-
        mDataGridViewCellStyleHdr = New DataGridViewCellStyle
        mDataGridViewCellStyleData = New DataGridViewCellStyle

        mDataGridViewCellStyleHdr.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleHdr.BackColor = System.Drawing.SystemColors.Control
        mDataGridViewCellStyleHdr.Font = _
             New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleHdr.ForeColor = System.Drawing.SystemColors.WindowText
        mDataGridViewCellStyleHdr.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleHdr.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleHdr.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = mDataGridViewCellStyleHdr
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize

        mDataGridViewCellStyleData.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleData.BackColor = System.Drawing.SystemColors.Window
        mDataGridViewCellStyleData.Font = _
            New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleData.ForeColor = System.Drawing.SystemColors.ControlText
        mDataGridViewCellStyleData.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        mDataGridViewCellStyleData.SelectionForeColor = System.Drawing.SystemColors.MenuText
        mDataGridViewCellStyleData.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = mDataGridViewCellStyleData

        cmdLookup.CausesValidation = False

        DataGridView1.Enabled = False
    End Sub '-- load--
	'= = = = = = = = =

    '--  EX Activate..--

    '== 'UPGRADE_WARNING: Form event frmNewPart.Activate has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '== Private Sub frmNewPart_Activated(ByVal eventSender As System.Object, _
    '==                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

    Private Sub frmNewPart_Load(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Call mbOriginal_frmNewPart_Load()
        mbCanTrackSerials = mRetailHost1.CanTrackSerials
        '-- get Cat1/2 column names..-

        msStockTableColumnNameCat1 = mRetailHost1.StockTableColumnNameCat1
        msStockTableColumnNameCat2 = mRetailHost1.StockTableColumnNameCat2

        If mbServiceChargeRequested Then
            LabHdr1.Text = "Add SERVICE Item to Job."
            If (msServiceChargeCat1 <> "") Then
                '==mbServiceChargeRequested = True
                chkWarranty.Enabled = False
                '==LabEnterSerialNo.Enabled = False
                LabEnterSerialNo.Visible = False
                LabVerify.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
                LabVerify.Text = vbCrLf & vbCrLf & _
                     "  Verify that this is the correct Service Charge Item, and click on Finish.."
                '== Me.Text = "Add SERVICE Charge from stock items.."
            Else
                MessageBox.Show(Me, "No Service Charge Stock Category has been defined in System Info.." & vbCrLf & _
                               "  (See JobMatix Setup..)", "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                mbCancelled = True
                Me.Hide()
            End If
        Else  '-not service..-
            LabHdr1.Text = "Add Stock Item to Job."
            LabEnterSerialNo.Text = "Serial-No (if any) of new Part.."
            LabEnterSerialNo.Visible = True
        End If
        '--  in any case..
        If Not mbCanTrackSerials Then
            LabEnterSerialNo.Visible = False
        End If

        '== On DEMAND.. ---Call mbInitialiseBrowse()

        LabScanProduct.Enabled = True
        txtPartNo.Focus()

    End Sub '--activate--
	'= = = = = = = = = = = =
	'-===FF->
	
	'--Product barccode--
    '--Product barccode--
    '-- Got Focus..--
	
    Private Sub txtPartNo_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles txtPartNo.Enter
        LabScanProduct.Enabled = True

        Call mbHideStockGrid()

        txtPartNo.SelectionStart = 0
        txtPartNo.SelectionLength = Len(txtPartNo.Text)
        cboQty.Enabled = False
        LabChangePrice.Visible = False

        txtSerialNo.Enabled = False

    End Sub '--focus..-
	'= = = = = =  = == =
	'-===FF->
	
	'--Product barccode--
	
	'--  F2 was pressed..  Browse for stock code..--
    Private Function mbBrowseStockTableDialog() As Boolean
        '== Dim i, j, k, cx, fx As Long
        '== Dim s1 As String, s2 As String
        '--Dim lngActualSize As Long
        Dim lngId As Integer
        '== Dim colKeys As Collection
        '== Dim colPrefs As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode As String
        '== Dim sWhere As String
        Dim colRecord As Collection  '--full  record..-

        mbBrowseStockTableDialog = False
        '== sWhere = ""
        '--retrieve selected record and fill in cust details..--
        If mRetailHost1.stockLookup(mbServiceChargeRequested, msServiceChargeCat1, msServiceChargeCat2, colSelectedRow) Then
            '--  that gets full normal stock record .--
            If colSelectedRow Is Nothing Then
                '====Me.Hide
                MessageBox.Show(Me, "Nothing selected..", "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            Else '--selection made..-
                '-- get barcode..--
                If (colSelectedRow.Count() > 0) Then
                    '== sBarcode = colSelectedRow.Item("barcode")("value")
                    '== lngId = CInt(colSelectedRow.Item("stock_id")("value"))
                    '--  TRanslate native browser row and get equiv. RM collection-
                    If Not mRetailHost1.stockGetStockRecordEx(colSelectedRow, colRecord) Then
                        MessageBox.Show(Me, "No record for Product selected.. ", _
                                              "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If
                    sBarcode = colRecord.Item("barcode")("value")
                    lngId = CInt(colRecord.Item("stock_id")("value"))
                    If sBarcode = "" Then
                        MessageBox.Show(Me, "Product-Id: " & lngId & " has no barcode..", _
                                          "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If

                    txtPartNo.Text = sBarcode
                    If mbLookupProduct() Then
                        '--ONLY if part (no serial for SERVICE charge..--
                        If mbServiceChargeRequested Or (Not mbCanTrackSerials) Then
                            LabScanProduct.Enabled = False
                            LabEnterSerialNo.Visible = False
                            txtSerialNo.Enabled = False
                            System.Windows.Forms.Application.DoEvents()
                            cmdOk.Focus()
                            '====cboQty.Enabled = False
                        ElseIf mbCanTrackSerials Then
                            LabScanProduct.Enabled = False
                            LabEnterSerialNo.Visible = True
                            '== LabEnterSerialNo.Text = "Enter Serial-No (if any) of new Part.."
                            txtSerialNo.Enabled = True
                            System.Windows.Forms.Application.DoEvents()
                            txtSerialNo.Focus()
                        Else
                            LabEnterSerialNo.Visible = False
                        End If
                        mbBrowseStockTableDialog = True
                    End If '--lookup..
                Else
                    If gbDebug Then MessageBox.Show(Me, "No selection made..", _
                                                "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '===Me.Hide
                    Exit Function
                End If '--got row..--
            End If '--nothing..-

        End If '--lookup..-
        System.Windows.Forms.Application.DoEvents()
        '==Set colPrefs = Nothing
    End Function '--browse..-
    '= = = = = =  = == =
    '-===FF->

    '--  F2 was pressed..  Browse for stock code..--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--

    Private Function mbBrowseStockTable(Optional ByRef sSrchWhereCond As String = "") As Boolean
        Dim sWhere As String = ""

        LabDescription.Enabled = False
        cmdLookup.Enabled = False
        cmdOk.Enabled = False
        '==FrameProduct.Visible = False
        FrameBrowse.Visible = True
        FrameBrowse.Enabled = True
        DataGridView1.Enabled = True

        FrameBrowse.Height = cmdOk.Top - FrameBrowse.Top - 16  '== 60
        DataGridView1.Height = (FrameBrowse.Height - DataGridView1.Top - 40)
        '= LabOk.Top = FrameBrowse.Height - 27
        cmdSelect.Top = FrameBrowse.Height - 27 '= LabOk.Top
        labRecCount.Top = cmdSelect.Top

        If (mBrowse1 Is Nothing) Then
            Call mbInitialiseBrowse()
        Else
            sWhere = msMakeStockFilter()  '--service or not..-
            '-- add srch args..
            If (sSrchWhereCond <> "") Then
                If sWhere <> "" Then
                    sWhere &= "AND "
                End If
                sWhere &= sSrchWhereCond
            End If
            mBrowse1.WhereCondition = sWhere '-- sWhere -
            mBrowse1.refresh()
        End If
        '= txtFind.Focus()
        txtStockSearch.Select()  '-for stewart-

        System.Windows.Forms.Application.DoEvents()
    End Function  ''--mbBrowseStockTable--
    '= = = = = =  = == =

    '--  get selected stock record.---

    Private Function mbSelectStockRow(ByVal lngRow As Integer) As Boolean

        Dim sBarcode As String
        Dim lngId, ix As Integer
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim colKeys As Collection

        Call mBrowse1.SelectRecord(lngRow, colKeys, colSelectedRow)
        If (colSelectedRow.Count > 0) Then
            '--    14Mar2012.. Fix stock lookup for QBPOS stock browsing..--
            '-- get reworked (RM) stock record
            If mRetailHost1.stockGetStockRecordEx(colSelectedRow, colRecord) Then
                sBarcode = colRecord.Item("barcode")("value")
                lngId = CInt(colRecord.Item("stock_id")("value"))
                If sBarcode = "" Then
                    MessageBox.Show(Me, "Product-Id: " & lngId & " has no barcode.." & vbCrLf & vbCrLf & _
                             "  Note:  JobMatix can process only those products " & vbCrLf & _
                                " with non-null values assigned in the barcode column..", _
                                  "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Function
                End If
                txtPartNo.Text = sBarcode
                If mbLookupProduct() Then
                    '--ONLY if part (no serial for SERVICE charge..--
                    If mbServiceChargeRequested Or (Not mbCanTrackSerials) Then
                        LabScanProduct.Enabled = False
                        LabEnterSerialNo.Visible = False
                        txtSerialNo.Enabled = False
                        System.Windows.Forms.Application.DoEvents()
                        cmdOk.Enabled = True
                        cmdOk.Focus()
                        '====cboQty.Enabled = False
                    ElseIf mbCanTrackSerials Then
                        LabScanProduct.Enabled = False
                        LabEnterSerialNo.Visible = True
                        '== LabEnterSerialNo.Text = "Enter Serial-No (if any) of new Part.."
                        txtSerialNo.Enabled = True
                        System.Windows.Forms.Application.DoEvents()
                        txtSerialNo.Focus()
                    Else
                        LabEnterSerialNo.Visible = False
                    End If
                End If '--lookup..

                Call mbHideStockGrid(lngRow)
            End If  '--recordEx..-
        End If  '--sel row--
    End Function  '--mbSelectStockRow-
    '= = = = = = = = = = 
    '-===FF->

    '--Product barccode--

    '-- PreviewKeyDown is where you preview the key.
    '-- Do not put any logic here, instead use the
    '-- KeyDown event after setting IsInputKey to true.

    Private Sub txtPartNo_PreviewKeyDown(ByVal sender As Object, _
                                          ByVal e As PreviewKeyDownEventArgs) Handles txtPartNo.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Escape
                e.IsInputKey = True
        End Select
    End Sub  '--PreviewKeyDown-
    '= = = = = = = = =  = = = = =  == 

    '--got function key----
    '--- check for F2 for STOCK Lookup--

    Private Sub txtPartNo_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        '== Dim cx, j, i, k, fx As Integer
        '== Dim s1, s2 As String
        '--Dim lngActualSize As Long
        Dim lngControl, lngId As Integer
        Dim AltDown, ShiftDown, CtrlDown As Integer
  
        ShiftDown = (Shift And VB6.ShiftConstants.ShiftMask) > 0
        AltDown = (Shift And VB6.ShiftConstants.AltMask) > 0
        CtrlDown = (Shift And VB6.ShiftConstants.CtrlMask) > 0
        lngControl = (VB6.ShiftConstants.ShiftMask + VB6.ShiftConstants.AltMask + VB6.ShiftConstants.CtrlMask)
        If (KeyCode = System.Windows.Forms.Keys.F2) And ((Shift And lngControl) = 0) Then '--lookup cust--
            '--If ShiftDown And CtrlDown And AltDown Then  Txt = "SHIFT+CTRL+ALT+F2."
            Call mbBrowseStockTable()
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Escape) Then
            If txtPartNo.Text = "" Then  '--exit
                mbCancelled = True
                Me.Hide()
                Exit Sub
            End If  '--no data-
            eventArgs.Handled = True
        End If '--F2--
    End Sub '--keyup-
    '= = = = = = = = = = = =

    '--  same as F2..  Lookup stock..--

    Private Sub txtPartNo_DoubleClick(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles txtPartNo.DoubleClick
        Call mbBrowseStockTable()
    End Sub
    '= = = = = = = = = =

    '--  same as F2..  Lookup stock..--
    '==3311.303= Now use Browse Form-

    Private Sub cmdLookup_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles cmdLookup.Click
        '== Call mbBrowseStockTable()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        Call mbBrowseStockTableDialog()  '--using frmBrowse via clRetailHost-
        System.Windows.Forms.Application.DoEvents()
    End Sub  '--cmdLookup_Click--
    '= = = = =  = = = = = = = = 
    '-===FF->

    '--Product barccode--
    '--Product barccode--

    'UPGRADE_WARNING: Event txtPartNo.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        LabDescription.Text = ""

        cmdOk.Enabled = False
        cboQty.Enabled = False
        '==If mbServiceChargeRequested Then
        txtSerialNo.Text = ""
        txtSerialNo.Enabled = False
        '== ElseIf mbCanTrackSerials Then
        '== txtSerialNo.Enabled = True '--ready for TAB key..-
        '== Else
        '== txtSerialNo.Enabled = False
        '== End If

        LabEnterSerialNo.Visible = False
        txtDescription.Text = ""
        txtSellInclGST.Text = ""

        chkWarranty.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-
        cboQty.SelectedIndex = 0 '-- 1--

    End Sub '--barcode change--
    '= = = = = = = = = = =

    '-- "ENTER" key on PartNo )barcode..)..-
    Private Sub txtPartNo_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartNo.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            If mbLookupProduct() Then
                If mbServiceChargeRequested Then
                    DataGridView1.ClearSelection()
                    System.Windows.Forms.Application.DoEvents()
                    If Not mbItemIsServiceCharge(mColFields) Then
                        MessageBox.Show(Me, "That item is not a service charge.", _
                                              "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else '--ok--
                        LabEnterSerialNo.Visible = False
                        txtSerialNo.Enabled = False
                        '===cboQty.Enabled = False
                        cmdOk.Enabled = True
                        cmdOk.Focus()
                    End If '--is service.-
                ElseIf mbCanTrackSerials Then
                    '-- must be stock part..
                    If mbItemIsServiceCharge(mColFields) Then
                        MessageBox.Show(Me, "That item is not a Stock Part.", _
                                                "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf mbCanTrackSerials Then '--ok--
                        LabEnterSerialNo.Visible = True
                        '== LabEnterSerialNo.Text = "Enter Serial-No (if any) of new Part.."
                        txtSerialNo.Enabled = True
                        System.Windows.Forms.Application.DoEvents()
                        txtSerialNo.Focus()
                    End If
                Else
                    LabEnterSerialNo.Visible = False
                End If
            End If '--lookup..-
            keyAscii = 0 '--processed..-
            '== ElseIf keyAscii = Keys.Escape Then   '==27 Then '--ESC-
            '== keyAscii = 0 '--processed..-
            '== If txtPartNo.Text = "" Then  '--exit
            '== mbCancelled = True
            '== Me.Hide()
            '== Exit Sub
            '== End If  '--no data-
         End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--PartNo_KeyPress--
    '= = = = = = = = = = =
    '= = = = =  = = = = = = = = 
    '-===FF->

    '--  validate txtPartNo..--

    '== Private Sub txtPartNo_Validating(ByVal eventSender As System.Object, _
    '==                            ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartNo.Validating
    '== Dim keepfocus As Boolean = EventArgs.Cancel

    '==     If FrameBrowse.Enabled Then Exit Sub '--browseing--
    '==     If Not mbLookupProduct() Then
    '==         keepfocus = True
    '==     Else '--ok..-
    '==         If mbServiceChargeRequested Then
    '==             If Not mbItemIsServiceCharge(mColFields) Then
    '==                 keepfocus = True
    '==                 MsgBox("That item is not a service charge.", MsgBoxStyle.Exclamation)
    '==             Else '--ok--
    '==                 txtSerialNo.Enabled = False
    '== '===cboQty.Enabled = False
    '==                 cmdOk.Enabled = True
    '==                 cmdOk.Focus()
    '==             End If
    '==         Else '--part requested..-
    '== '-- must be stock part..
    '==             If mbItemIsServiceCharge(mColFields) Then
    '==                 keepfocus = True
    '==                 MsgBox("That item is not a Stock Part.", MsgBoxStyle.Exclamation)
    '==             Else '--ok--
    '==                 txtSerialNo.Focus()
    '==             End If
    '==         End If
    '==     End If
    '==     eventArgs.Cancel = keepfocus
    '== End Sub '--validate..-
    '= = = = = = = = = =
    '-===FF->

    '--BROWSING STOCK.. --

    '--  D at a  G r i d  E v e n t s..--
    '--  D at a  G r i d  E v e n t s..--

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridView1_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridView1.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = DataGridView1.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub DataGridView1_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseClick
        Dim lRow, lCol As Integer
        '== Dim sName As String
        '==Dim i, j, k As Long

        If eventArgs.Button = 1 Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (DataGridView1.Rows.Count > 0) Then  '--selected a row.--

                '== cmdOk.Enabled = True
                mLngSelectedRow = lRow
                '== Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)

            End If
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    '== Private Sub MSHFlexgrid1_dblClick(ByVal eventSender As System.Object, _
    '==                                        ByVal eventArgs As System.EventArgs)
    Private Sub DataGridView1_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseDoubleClick
        Dim lRow As Integer

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            '--  get stock id and start edit.--
            '== Call mbSelectEditItem(lRow)
            Call mbSelectStockRow(mLngSelectedRow)

        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '-- STOCK Browser.. txt FIND Activity.--
    '-- STOCK Browser.. txt FIND Activity.--
    '--BROWSING STOCK.. --

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        '= lRow = MSHFlexGrid1.Row
        '= lCol = MSHFlexGrid1.Col

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If DataGridView1.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = DataGridView1.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '==Call MSHFlexgrid1_KeyPressEvent(MSHFlexGrid1, _
                    '==                New AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent(iKeyAscii))
                    '== Call mbSelectEditItem(lRow)
                    Call mbSelectStockRow(mLngSelectedRow)

                End If '--row--
                iKeyAscii = 0 '--processed--
            End If '--enter--

            eventArgs.KeyChar = Chr(iKeyAscii)
            If iKeyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End If  '--count-
    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '--  OK..  selected browser row..

    Private Sub cmdSelect_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles cmdSelect.Click

        '== Dim lCol, lngId As Integer
        '-  get selected row..--
        Dim lngRowNo As Integer

        '==Call MSHFlexgrid1_dblClick(MSHFlexGrid1, New System.EventArgs())
        If (DataGridView1.SelectedRows.Count > 0) Then
            '--  use 1st selected row only.
            lngRowNo = DataGridView1.SelectedRows(0).Cells(0).RowIndex
            If (lngRowNo >= 0) Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRow = lngRowNo
                Call mbSelectStockRow(mLngSelectedRow)
            Else  '--no row-
                If gbDebug Then MessageBox.Show(Me, "No selection made..", _
                               "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If '--got row..--
        End If  '-- sel-count.-
    End Sub  '-cmdSelect-
    '= = = = = = = = = =

    '-- Stock Browser..  Full text Search..--
    '-- Stock Browser..  Full text Search..--

    Private Sub cmdStockSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdStockSearch.Click
        Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-

        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
         '--  now in the Interface..--
        asColumns = mRetailHost1.stockSearchColumns()

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtStockSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        Call mbBrowseStockTable(sWhere)

    End Sub '-cmdStockSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearStockSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearStockSearch.Click
        txtStockSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdStockSearch_Click(cmdStockSearch, New System.EventArgs())

    End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =

    '=3311.304=
    '-- "ENTER" key on search text.-

    Private Sub txtStockSearch_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                    Handles txtStockSearch.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdStockSearch_Click(cmdStockSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '-srch text enter-
    '= = = = = = = == = = = = = = 

    '==  END of stock BROWSING..--
    '==  END of stock BROWSING..--
    '-===FF->

     '-- SERIAL-No--
    '-- SERIAL-No--
    Private Sub txtSerialNo_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles txtSerialNo.Enter
        txtSerialNo.SelectionStart = 0
        txtSerialNo.SelectionLength = Len(txtSerialNo.Text)
    End Sub '--focus..-
    '= = = = = = = = = =

    'UPGRADE_WARNING: Event txtSerialNo.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtSerialNo_TextChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles txtSerialNo.TextChanged

        If Trim(txtSerialNo.Text) <> "" Then
            cboQty.Enabled = False
            cboQty.SelectedIndex = 0 '--qty of 1 only if serno entered.-
        End If
    End Sub '--Serial No change--
    '= = = = = = = = = = = = = =

    '-- "ENTER" key on Serial No..-
    Private Sub txtSerialNo_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                 Handles txtSerialNo.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        msSerialNo = Trim(txtSerialNo.Text)
        If keyAscii = 13 Then '--enter-
            If Trim(txtSerialNo.Text) <> "" Then
                mIntStock_id = CInt(mColFields.Item("stock_id")("value"))
                If mbCheckSerialNumber(msSerialNo, mIntStock_id) Then '--ok-
                    chkWarranty.Focus()
                Else '--not valid..-
                    txtSerialNo.SelectionStart = 0
                    txtSerialNo.SelectionLength = Len(txtSerialNo.Text)
                End If
            Else '--blank serial..-
                '== 3047.1- Can BE Warranty without Ser-No..   cmdOk.Focus()
                chkWarranty.Focus()
            End If
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--SerialNo_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    '-- validating..-

    Private Sub txtSerialNo_Validating(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                             Handles txtSerialNo.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel

        msSerialNo = Trim(txtSerialNo.Text)
        If (msSerialNo <> "") Then
            mIntStock_id = CInt(mColFields.Item("stock_id")("value"))
            If mbCheckSerialNumber(msSerialNo, mIntStock_id) Then '--ok-
                '==chkWarranty.SetFocus
            Else '--not valid..-
                keepfocus = True
            End If
        Else '--blank serial..-
        End If
        eventArgs.Cancel = keepfocus
    End Sub '--validate..-
    '= = = = = = = = = =

    '- Serial No validated-
    '= Show serial report info if any.

    Private Sub txtSerialNo_Validated(ByVal sender As Object, _
                                         ByVal e As System.EventArgs) Handles txtSerialNo.Validated
        ' If all conditions have been met, clear the error provider of errors.
        '== errorProvider1.SetError(textBox1, "")
        If (msSerialReport <> "") Then
            MessageBox.Show(Me, msSerialReport, "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub  '- validated-
    '= = = = = = = = = = = == ===  =
    '-===FF->

    '-- "ENTER" key on Warranty Checkbox....-
    Private Sub chkWarranty_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                 Handles chkWarranty.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If (keyAscii = 13) Then '--enter-

            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            chkWarranty.Parent.SelectNextControl(ActiveControl, True, True, True, False)

            '-- "SelectNextControl" should achieve this..--
            '== If (txtDescription.Enabled And txtDescription.TabStop) Then
            '==    txtDescription.Focus()
            '== ElseIf cboQty.Enabled Then
            '==    cboQty.Focus()
            '== ElseIf cmdOk.Enabled Then
            '==    cmdOk.Focus()
            '== End If

            keyAscii = 0  '--handled.-
        End If
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--chkWarranty keypress..-
    '= = = = = = = = = =

    '-- "ENTER" key on txtDescription...-
    Private Sub txtDescription_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                 Handles txtDescription.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txtDescription.Parent.SelectNextControl(ActiveControl, True, True, True, False)

            '-- "SelectNextControl" should achieve this..--
            '== If txtSellInclGST.Enabled Then
            '== txtSellInclGST.Focus()
            '== ElseIf cboQty.Enabled Then
            '== cboQty.Focus()
            '== ElseIf cmdOk.Enabled Then
            '== cmdOk.Focus()
            '== End If
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--SerialNo_KeyPress--
    '= = = = = = = = = = =
    '-===FF->

    Private Sub txtSellInclGST_Enter(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles txtSellInclGST.Enter

        txtSellInclGST.SelectionStart = 0
        txtSellInclGST.SelectionLength = Len(txtSellInclGST.Text)
    End Sub '--gotfocus.--
    '= = = = = = = = = = = =

    '-- Sell price.. --

    'UPGRADE_WARNING: Event txtSellInclGST.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtSellInclGST_TextChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles txtSellInclGST.TextChanged
        Dim s1 As String

        s1 = Trim(txtSellInclGST.Text)
        If (s1 = "") Then Exit Sub
        If Not IsNumeric(s1) Then
            MessageBox.Show(Me, "Sell price is Numeric field only..", _
                           "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSellInclGST.SelectionStart = 0
            txtSellInclGST.SelectionLength = Len(txtSellInclGST.Text)
        End If '--numeric..-

    End Sub '-- sell.-
    '= = = = = = = = =
    '-===FF->

    '-- "ENTER" key on sell price...-
    Private Sub txtSellInclGST_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                 Handles txtSellInclGST.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            txtSellInclGST.Parent.SelectNextControl(ActiveControl, True, True, True, False)

            '-- "SelectNextControl" should achieve this..--
            '== If cboQty.Enabled Then
            '== cboQty.Focus()
            '== ElseIf cmdOk.Enabled Then
            '== cmdOk.Focus()
            '== End If
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--SerialNo_KeyPress--
    '= = = = = = = = = = =

    '--validate.--
    Private Sub txtSellInclGST_Validating(ByVal eventSender As System.Object, _
                                                 ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                                    Handles txtSellInclGST.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String

        s1 = Trim(txtSellInclGST.Text)
        If (s1 = "") Then

        ElseIf (Not IsNumeric(s1)) Then
            MessageBox.Show(Me, "Sell price is Numeric field only..", _
                                    "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSellInclGST.SelectionStart = 0
            txtSellInclGST.SelectionLength = Len(txtSellInclGST.Text)
            keepfocus = True
        End If '--numeric..-

        eventArgs.Cancel = keepfocus
    End Sub '--sell-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '- Text changed..  check for numeric.

    Private mbComboEntered As Boolean = False

    Private Sub cboQty_TextChanged(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) _
                                                              Handles cboQty.TextChanged
        If Not IsNumeric(Trim(cboQty.Text)) Then
            If mbComboEntered Then
                Exit Sub
            End If
            mbComboEntered = True
            MessageBox.Show(Me, "Qty must be a number only", _
                                  "JobMatix NewPart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboQty.Text = ""
            mbComboEntered = False
        End If
    End Sub  '-TextChanged-
    '= = = = == = = = === 

    '-- "ENTER" key on cboQty...-
    Private Sub cboQty_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                 Handles cboQty.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        If keyAscii = 13 Then '--enter-
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            '-- NB "SelectNextControl" starts from CURRENT CONTROL --
            cboQty.Parent.SelectNextControl(ActiveControl, True, True, True, False)

            '-- "SelectNextControl" should achieve this..--
            '== If cmdOk.Enabled Then
            '= cmdOk.Focus()
            '== End If
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--cboQty--
    '= = = = = = = = = = = 
    '-===FF->

    '-- Finish.. ok--
    '-- Finish.. ok--

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        Dim ans As Short
        Dim curCostInclGST As Decimal
        Dim col1 As Collection

        ans = MsgBoxResult.Yes
        '-- set up fld collection for caller--
        If (quantity > 1) Then '-confirm-
            ans = MessageBox.Show(Me, "You have specified a qty of " & quantity & " for this part.." & vbCrLf & _
                        "Please confirm this is OK..", _
                        "JobMatix NewPart", _
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        End If '--qty..-
        '--exit--
        If ans = MsgBoxResult.Yes Then
            '-- set up final cover values for descr., price..-

            col1 = New Collection
            col1.Add("Description", "name")
            col1.Add(txtDescription.Text, "value")
            mColFields.Add(col1, "Description") '-- for eventual sql INSERT..-

            curCostInclGST = CDec(txtSellInclGST.Text)
            col1 = New Collection
            col1.Add("SellInclGST", "name")
            col1.Add(curCostInclGST, "value")
            mColFields.Add(col1, "SellInclGST") '-- for consistency..-

            Me.Hide()
        End If
    End Sub '--ok--
    '= = = = = = = = = =
    '-===FF->

    '--cancel job..--
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        mbCancelled = True
        Me.Hide()
        Exit Sub

    End Sub '--cancel--
    '= = = = = =  = = =

    '--uses QueryUnload--
    '--  NB- Controlled programmed exit (via Hide)-
    '--    Will only hit this when frmMaint (FormOwner) closes the form.

    Private Sub frmNewPart_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                  Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim ans As Short

        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                                               System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                                  System.Windows.Forms.CloseReason.FormOwnerClosing  '==, vbFormCode
                '= See NB  above- mbCancelled = True
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                '--Clicked form Close Control Box Button--
                ans = MsgBoxResult.Yes
                If (txtPartNo.Text <> "") Then
                    ans = MessageBox.Show(Me, "Abandon this new part..?", _
                                          "JobMatix NewPart", _
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If (ans <> MsgBoxResult.Yes) Then
                        intCancel = 1 '--cant close yet--
                    Else '--yes--
                        mbCancelled = True
                        intCancel = 0 '--let it go--
                    End If
                Else  '- no part no-
                    mbCancelled = True
                    intCancel = 0 '--let it go--
                End If '-enabled..-
            Case Else
                '== mbCancelled = True
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--queryUnload--
    '= = = = = = = = = =  =


End Class  '--frmNewPart-
'== end form ==
