Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class frmCustHistory
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. 
    '=Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = = = =
	
	'--  SHOW FULL Customer History.--
	'-----  Jobs and MYOB Sales..--
	
	'-- grh - =16-Mar-2010= 6:23pm == Started..-
	'-- grh - =02-Apr-2010= 6:21pm == Dockets are both "SA"  and "IV"..-
	'-- grh - =19-May-2010= 6:21pm == Caller can provide CustomerId.-
	'-- grh - =29-Jul-2010= 10:33am == F2 on Cust. must lookup RetailManager "customer" -(NOT Jobs !!!).-
	'-- grh - =03-Sep-2010= Show all Customer details incl EMAIL addr...-
	
	'--  J O B M A T I X --
	'-- grh - =10-Nov-2010= Customer Lookup use local browse object..-
	'-- grh - =12-Dec-2010= CustomerBarcode needed if called from Job context..-
	'-- grh - =05-Apr-2011= Unlock CustomerNo on getFocus (ie after browse)....-
	
	'-- grh - =08-Oct-2011= Use RetailHost...-

    '-- grh - =26-Nov-2011= UPGRADED to VB.NET....-
    '-- grh - =16-Dec-2011= Fixes to browse.....-

    '-- grh - =29-Feb-2012= Build 3013== Dropped MSHFlexGrid...-
    '--                     in favour of  dotNet DataGridView..
    '== 
    '-- grh - =08-Apr-2012= Build 3041.0== Dropped Pic Arrows....-
    '--    
    '==
    '== grh - =17-Apr-2012= Build 3043.0== Updating frame colours..-
    '==
    '== grh - =02-Jun-2012= Build 3059.1== For RM. Retrieve job list via "Customer_id"..-
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  16-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider needed for Jet OleDb driver).
    '==
    '==  NEW BUILD- 4219 VERSION
    '==    Updated- 4219.1122 22-Nov-2019= 
    '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
	Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--
	
    Private mbActive As Boolean = False  '-- stops activate being re-entered..-
	'== Private mbAmending As Boolean    '-- amend existing  job...-
	
	Private mlFormDesignHeight As Integer
	Private mlFormDesignWidth As Integer
	
    Private mlBrowseDesignTop As Integer = 72
    Private mlBrowseDesignLeft As Integer = 8
	
    Private mCnnJobs As OleDbConnection   '== ADODB.Connection '--SQL jobs connection --
	'== Private mCnnJet  As ADODB.connection    '--  Retail Manager Jet connection..--
	'== Private mColJetDBInfo As Collection
	Private mColSqlDBInfo As Collection
	
    Private msBusinessName As String = ""
	'-- new job data --
	'-- new job data --
    Private mlStaffId As Integer = -1 '--save staff-id--
    Private mlCustomerId As Integer = -1 '--save cust-id--

    Private msCustomerBarcode As String = "" '--main cust. identifier..-
	Private msStaffName As String
	Private msCustomerName As String
	Private msCustomerPhone As String
	Private msCustomerMobile As String
	Private msCustomerCompany As String
	
    Private mRsJobsList As DataTable  '== ADODB.Recordset
    Private mRsPurchasesList As DataTable  '==  ADODB.Recordset
	
    Private mlSortOrderPurchases As System.Windows.Forms.SortOrder  '== Integer
	Private mlSortKeyPurchases As Integer
	
    Private mlSortOrderJobs As System.Windows.Forms.SortOrder   '== Integer
	Private mlSortKeyJobs As Integer
	
	Private mlJobId As Integer
	Private mColJobFields As Collection
	
    '== Private mColPrefs As Collection
	Private mLngSelectedRow As Integer
	
	'-- MULTPLE HOST VERSION..--
    '=4219.1122= Private mRetailHost1 As _clsRetailHost
    Private mRetailHost1 As JMxRetailHost._clsRetailHost

	'== Private mBrowse1 As New clsBrowse22
	Private mBrowseHost As clsBrowse3 '== clsBrowse22    '-- clsBrowseHost
    '= = = = = = = = = = = = = = =

    '-- DataGridViewCellStyle--
    Private mDataGridViewCellStyleHdr As DataGridViewCellStyle
    Private mDataGridViewCellStyleData As DataGridViewCellStyle

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	
	'--  Input Properties..-
	'--  Input Properties..-
	
    WriteOnly Property retailHost() As JMxRetailHost._clsRetailHost
        Set(ByVal Value As JMxRetailHost._clsRetailHost)

            mRetailHost1 = Value

        End Set
    End Property '--host..--
	'= = = = =  = =  = = =
	
    WriteOnly Property connectionSql() As OleDbConnection   '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value

        End Set
    End Property '--cnn sql..--
	'= = = = = = =  = = = = =
	
	'== Property Let connectionJet(cnn1 As ADODB.connection)
	
	'==   Set mCnnJet = cnn1
	
	'== End Property  '--cnn jet..--
	'= = = = = = =  = = = = =
	
	WriteOnly Property dbInfoSql() As Collection
		Set(ByVal Value As Collection)
			
			mColSqlDBInfo = Value
			
		End Set
	End Property '--info sql jobs..--
	'= = = = = = =  = = = = =
	
	'== Property Let dbInfoJet(dbinfo As Collection)
	
	'==   Set mColJetDBInfo = dbinfo
	
	'== End Property  '--info jet..--
	'= = = = = = = = = = = = +
	
	WriteOnly Property BusinessName() As String
		Set(ByVal Value As String)
			
			msBusinessName = Value
		End Set
	End Property '--name-
	'= = = = = = = =  == =
	
    '-- Customer ID is preferred key where available..--
	
    WriteOnly Property CustomerId() As Integer
        Set(ByVal Value As Integer)

            mlCustomerId = Value
        End Set
    End Property  '--ID-
    '= = = = = = = =  == =

    '--  use Barcode when ni ID .-
    WriteOnly Property CustomerBarcode() As String
        Set(ByVal Value As String)

            msCustomerBarcode = Value
        End Set
    End Property '--barcode-
    '= = = = = = = =  == =
    '-===FF->

    '--  Refresh MYOB R-M Purchases List.--

    '=== Private Function mbRefreshPurchasesList(rs1 As ADODB.Recordset) As Boolean
    '===       Dim lngCount As Long
    '===       Dim sSql As String

    '===     mbRefreshPurchasesList = False
    '===     sSql = " SELECT docket.docket_id, docket_date, total_inc, "
    '===     sSql = sSql + "   Cat1, Cat2, Description, barcode,  docket.transaction,  "
    '===     sSql = sSql + "  DocketLine.stock_id, DocketLine.quantity, "
    '===     sSql = sSql + "      sell_inc, docket.customer_Id "
    '===     sSql = sSql + "  FROM (Docket "
    '===     sSql = sSql + "    LEFT JOIN (DocketLine "
    '===     sSql = sSql + "         LEFT JOIN Stock"
    '===     sSql = sSql + "             ON (stock.stock_id=DocketLine.stock_id) )"
    '===     sSql = sSql + "         ON  (DocketLine.docket_id=docket.docket_id) ) "
    '===     sSql = sSql + " WHERE (docket.customer_Id= " & CStr(mlCustomerId) & ") AND  "
    '===     sSql = sSql + "                        ((Transaction='SA') OR (Transaction='IV')) "
    '===     sSql = sSql + "   ORDER BY docket.docket_id DESC; "

    '===     Screen.MousePointer = vbHourglass
    '===     If gbGetRst(mCnnJet, rs1, sSql) Then  '--ok-
    '===         Screen.MousePointer = vbNormal
    '===         '--refresh listview..-
    '===         lngCount = mbLoadListView(rs1, ListViewPurchases)
    '===         LabPurchases.Caption = "Purchase History (" & lngCount & " Retail Manager Docket Lines)"
    '===         mbRefreshPurchasesList = True
    '===     Else '--failed..-
    '===         Screen.MousePointer = vbNormal
    '===         MsgBox "Failed to get Dockets recordset..", vbExclamation
    '===     End If

    '=== End Function  '--refresh..-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- ClearJobInfo --

    Private Function mbClearJobInfo() As Boolean

        txtGoods.Text = ""
        txtSymptoms.Text = ""
        txtWorkHistory.Text = ""
        LabWorkHistory.Text = ""

        ListViewParts.Items.Clear()

    End Function '---clear..-
    '= = = = = = = = = = = = = = =  =

    '-- Refresh Jobs List..-
    '-- Refresh Jobs List..-

    Private Function mbRefreshJobsList(ByRef rs1 As DataTable) As Boolean
        Dim lngCount As Integer
        Dim sWhere, sSql As String

        Call mbClearJobInfo()

        If (mlCustomerId >= 0) Then  '-have id..-
            sWhere = "WHERE (RMCustomer_Id= " & mlCustomerId & ") "
        Else  '--use barcode.-
            sWhere = "WHERE (CustomerBarcode= '" & msCustomerBarcode & "') "
        End If
        sSql = " SELECT Job_id, DateUpdated, TechStaffName AS Tech,  "
        sSql = sSql & "  JobStatus,  GoodsInCare, ProblemSymptoms, Priority "
        sSql = sSql & "  FROM Jobs  " & sWhere
        sSql = sSql & "   ORDER BY Job_Id DESC; "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If gbGetDataTable(mCnnJobs, rs1, sSql) Then '--ok-
            System.Windows.Forms.Cursor.Current = Cursors.Default
            '--refresh listview..-
            lngCount = gbLoadListView(rs1, ListViewJobs)
            LabJobHistory.Text = "Job Work History (" & lngCount & " Jobs)."
        Else '--failed..-
            System.Windows.Forms.Cursor.Current = Cursors.Default
            MsgBox("Failed to get Jobs recordset..", MsgBoxStyle.Exclamation)
        End If

    End Function '--refresh..-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-

    Private Function mbGetJobRecord(ByVal lngJobNo As Integer, ByRef ColJobFields As Collection) As Boolean

        Dim RsJob As DataTable '= ADODB.Recordset
        Dim sSql, sName As String
        '= Dim fld1 As ADODB.Field
        Dim colFld As Collection

        mbGetJobRecord = False
        sSql = "SELECT * from [jobs]  "
        sSql = sSql & " WHERE (job_id=" & CStr(lngJobNo) & ")  " & vbCrLf
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, RsJob, sSql) Then
            '==If bBeginTrans Then mCnnJobs.RollbackTrans   '-- Call mbRollbackTransaction
            '--If mbService Or mbDelivery Then mCnnJobs.RollbackTrans
            MsgBox("Failed to get JOB recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) Then
            If (RsJob.Rows.Count > 0) Then  '== Not (RsJob.BOF And RsJob.EOF) Then '--not empty-
                '== RsJob.MoveFirst()
                Dim datarow1 As DataRow = RsJob.Rows(0)  '-first row-
                '--return complete row..-
                For Each column1 As DataColumn In RsJob.Columns   '== Each fld1 In RsJob.Fields
                    colFld = New Collection
                    sName = column1.ColumnName
                    colFld.Add(LCase(sName), "name")
                    colFld.Add(datarow1.Item(sName), "value")
                    ColJobFields.Add(colFld, LCase(sName))
                Next column1 '= fld1
                mbGetJobRecord = True
            Else '--not found-
                MsgBox("Can't find Job Record for  JobNo: " & lngJobNo, MsgBoxStyle.Exclamation)
            End If '-empty-
            '== RsJob.Close()
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--  build listBox of PARTS added to this job so far..--
    '--  build listBox of PARTS added to this job so far..--

    Private Function mbShowAllParts() As Boolean
        Dim s1, sW As String
        Dim sShowCost As String
        '== Dim s3 As String
        Dim sBarcode As String
        Dim sSerialNo As String
        Dim sSql As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sStockId As String
        Dim sStaff As String
        Dim curCost, curTotalParts As Decimal
        Dim item1 As System.Windows.Forms.ListViewItem

        mbShowAllParts = False
        '==ListParts.Clear
        ListViewParts.Items.Clear()
        ListViewParts.Columns.Clear()
        ListViewParts.Items.Clear()
        ListViewParts.Columns.Add("", "Cat1", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 8))) '-- width..-
        ListViewParts.Columns.Add("", "Description", CInt(VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(ListViewParts.Width) \ 10) * 4))) '--40% of width..-
        ListViewParts.Columns.Add("", "Barcode", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 6)))
        ListViewParts.Columns.Add("", "Serial-No", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 5)))
        ListViewParts.Columns.Add("", "Wty", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 12)))
        ListViewParts.Columns.Add("", "StockId", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 6)))
        ListViewParts.Columns.Add("", "Sell-Price", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 6)))
        ListViewParts.Columns.Add("", "Tech.", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewParts.Width) \ 6)))

        curTotalParts = 0
        sSql = "Select * from [jobParts] WHERE (PartJob_id=" & CStr(mlJobId) & ")"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MsgBox("Failed to get JobPARTS recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--build list box list of PARTS so far..-
        If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
            '== If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
            For Each dataRow1 As DataRow In rs1.Rows
                '--add to list box for job..
                '-- RECORDSET is from JOBPARTS Table..--
                sW = " "
                If (UCase(dataRow1.Item("IsWarrantyPart")) = "Y") Then sW = "W"
                sStockId = Trim(CStr(dataRow1.Item("RMStock_id")))
                sBarcode = Trim(CStr(dataRow1.Item("RMBarcode")))
                sSerialNo = Trim(CStr(dataRow1.Item("PartSerialNumber")))
                curCost = CDec(dataRow1.Item("RMSell"))
                curTotalParts = curTotalParts + curCost
                sShowCost = FormatCurrency(curCost, 2)
                sStaff = VB.Left(dataRow1.Item("servicedByStaffName"), 8)
                '-- add to listview--
                s1 = Trim(dataRow1.Item("RMCat1")) '--1st column.-
                item1 = ListViewParts.Items.Add(s1)   '--1st column.-

                item1.SubItems.Add(Trim(dataRow1.Item("RMDescription"))) '--2nd column.-
                item1.SubItems.Add(sBarcode)
                item1.SubItems.Add(sSerialNo)
                item1.SubItems.Add(sW)
                item1.SubItems.Add(sStockId)
                item1.SubItems.Add(sShowCost)
                item1.SubItems.Add(sStaff)
            Next dataRow1

            '== While (Not rs1.EOF) '---And (cx < 100)
            '==   rs1.MoveNext()
            '==          End While '-eof-
            mbShowAllParts = True
            '== rs1.Close()
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '-- showParts.--
    '= = = = = = =  =  = =
    '-===FF->

    '-- Load Job  INFO FOR Selected R e c o r d..-

    Private Function mbShowJobInfo(ByVal lngJobId As Integer) As Boolean
        Dim s1 As String
        Dim sPriority As String

        '== txtGoods.Text = ""
        '== txtSymptoms.Text = ""
        '== txtWorkHistory.Text = ""
        '== LabWorkHistory.Caption = ""
        Call mbClearJobInfo()
        System.Windows.Forms.Application.DoEvents()
        If mbGetJobRecord(lngJobId, mColJobFields) Then
            mlJobId = lngJobId
            FrameJobDetails.Text = "Job No: " & lngJobId
            sPriority = UCase(mColJobFields.Item("priority")("value"))
            If sPriority = "Q" Then '--Quote..-
                txtGoods.Text = "QUOTATION"
            Else
                txtGoods.Text = mColJobFields.Item("GoodsInCare")("value") & vbCrLf & mColJobFields.Item("GoodsOther")("value")
                s1 = UCase(Trim(mColJobFields.Item("GoodsBrand")("value")))
                '--  in case of old-style newJob form..-
                If (s1 <> "") And (s1 <> "N/A") Then '--some old-style brand..-
                    txtGoods.Text = txtGoods.Text & vbCrLf & mColJobFields.Item("GoodsBrand")("value") & " (" & mColJobFields.Item("GoodsModel")("value") & ")"
                End If
                s1 = mColJobFields.Item("GoodsExtras")("value")
                If (s1 <> "") Then txtGoods.Text = txtGoods.Text & vbCrLf & "EXTRAS: " & s1

                s1 = mColJobFields.Item("ProblemShort")("value")
                If (s1 <> "N/A") And (s1 <> "") Then txtSymptoms.Text = s1 & vbCrLf
                txtSymptoms.Text = txtSymptoms.Text + mColJobFields.Item("ProblemSymptoms")("value")
                s1 = mColJobFields.Item("ProblemLong")("value")
                If s1 <> "" Then
                    txtSymptoms.Text = txtSymptoms.Text & vbCrLf & "NOTES:" & vbCrLf & s1
                End If
            End If '--quote..-
            System.Windows.Forms.Application.DoEvents()
            txtSymptoms.Text = txtSymptoms.Text & vbCrLf & "DIAGNOSIS:  " & mColJobFields.Item("Diagnosis")("value")
            '===txtWorkDetails.Text = mColJobFields("ServiceNotes")("value")
            LabWorkHistory.Text = "Work History  ( " & VB6.Format(CDec(mColJobFields.Item("TotalServiceTime")("value")), "##0.00") & " Hours)"
            txtWorkHistory.Text = mColJobFields.Item("ServiceNotes")("value")
            txtWorkHistory.SelectionStart = Len(txtWorkHistory.Text)
            txtWorkHistory.SelectionLength = 0
            System.Windows.Forms.Application.DoEvents()
            Call mbShowAllParts()
            System.Windows.Forms.Application.DoEvents()
        Else '--failed..-

        End If

    End Function '--show job..-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- lookup RM Customers to check Customer given long ID..--
    '-- lookup RM Customers, return record as collection of fld collections.--

    '=== Private Function XXX_mbLookupCustomerId(lngCustId As Long, _
    ''===                                      colFields As Collection) As Boolean
    '===       Dim colFld As Collection  '--"name"=, "value"-
    '===       Dim fld1 As ADODB.Field
    '===       Dim s1 As String
    '===       Dim sSql As String
    '===       Dim sName As String
    '===       Dim rs1 As ADODB.Recordset

    '===   XXX_mbLookupCustomerId = False
    '===   sSql = "Select * from [customer] WHERE (customer_id=" + CStr(lngCustId) + "); "
    '===   Screen.MousePointer = vbHourglass
    '===   If Not gbGetRst(mCnnJet, rs1, sSql) Then
    '===           MsgBox "Failed to get Customer recordset..", vbExclamation
    '===           Screen.MousePointer = vbDefault
    '===           Exit Function
    '===   End If
    '===   If Not (rs1 Is Nothing) Then
    '===          If Not (rs1.BOF And rs1.EOF) Then
    '===             rs1.MoveFirst
    '===             '--return complete row..-
    '===             Set colFields = New Collection
    '===             For Each fld1 In rs1.Fields
    '===                Set colFld = New Collection
    '===                colFld.Add LCase(fld1.Name), "name"
    '===                colFld.Add fld1.Value, "value"
    '===                colFields.Add colFld, LCase(fld1.Name)
    '===             Next fld1
    '===             XXX_mbLookupCustomerId = True
    '===          Else  '--not found-
    '===            MsgBox "No Customer record found for ID: " & lngCustId, vbExclamation
    '===          End If  '-eof-
    '===   End If  '--rs-
    '===   Screen.MousePointer = vbDefault '--in case Browser failed--
    '=== End Function  '--get customer-
    '= = = = = = =  =  = =

    '-- lookup RM Customers to check Customer given long ID..--
    '-- lookup RM Customers, return record as collection of fld collections.--

    '=== Private Function XXX_mbLookupCustomerBarcode(sBarcode As String, _
    ''===                                      colFields As Collection) As Boolean
    '===       Dim colFld As Collection  '--"name"=, "value"-
    '===       Dim fld1 As ADODB.Field
    '===       Dim s1 As String
    '===       Dim sSql As String
    '===       Dim sName As String
    '===       Dim rs1 As ADODB.Recordset

    '===   XXX_mbLookupCustomerBarcode = False
    '===   sSql = "Select * from [customer] WHERE barcode='" + sBarcode + "' "
    '===   Screen.MousePointer = vbHourglass
    '===   If Not gbGetRst(mCnnJet, rs1, sSql) Then
    '===           MsgBox "Failed to get Customer recordset..", vbExclamation
    '===           Screen.MousePointer = vbDefault
    '===           Exit Function
    '===   End If
    '===   If Not (rs1 Is Nothing) Then
    '===          If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst
    '===          If (Not rs1.EOF) Then   '---And (cx < 100)
    '===             '--return complete row..-
    '===             Set colFields = New Collection
    '===             For Each fld1 In rs1.Fields
    '===                Set colFld = New Collection
    '===                colFld.Add LCase(fld1.Name), "name"
    '===                colFld.Add fld1.Value, "value"
    '===                colFields.Add colFld, LCase(fld1.Name)
    '===             Next fld1
    '===             XXX_mbLookupCustomerBarcode = True
    '===          Else  '--not found-
    '===          End If  '-eof-
    '===   End If  '--rs-
    '===   Screen.MousePointer = vbDefault '--in case Browser failed--
    '=== End Function  '--get customer-
    '= = = = = = =  =  = =
    '-===FF->

    '--  set up customer details from customer record..--
    '--  set up customer details from customer record..--

    Private Function mbSetupCustomer(ByRef colFields As Collection) As Boolean
        Dim s1 As String
        Dim s2 As String
        Dim sName As String
        Dim s3, s4 As String
        Dim colPurchasesList As Collection
        Dim lngError As Integer

        On Error GoTo SetupCustomer_error
        FrameCust.Visible = True
        msCustomerName = ""
        msCustomerCompany = "" : s3 = "" : s4 = ""
        mlCustomerId = CInt(colFields.Item("customer_id")("value"))
        msCustomerBarcode = colFields.Item("barcode")("value")
        msCustomerCompany = colFields.Item("company")("value")
        If (msCustomerCompany = "") Then '--no company name..--
            msCustomerCompany = "--" '--"n/a" '==  don't want blank company..--
        End If
        sName = msCustomerCompany
        s3 = colFields.Item("given_names")("value")
        s4 = colFields.Item("surname")("value")
        msCustomerPhone = colFields.Item("phone")("value")
        If Len(msCustomerPhone) > 20 Then
            msCustomerPhone = VB.Left(Replace(msCustomerPhone, " ", ""), 20)
        End If
        msCustomerMobile = colFields.Item("mobile")("value")
        If Len(msCustomerMobile) > 20 Then
            msCustomerMobile = VB.Left(Replace(msCustomerMobile, " ", ""), 20)
        End If
        '-- save customer info..--
        '---msCustomerName = Left(s3 + " " + s4, 50)   '--max 50--
        msCustomerName = s4 '--max 50--SURNAME, GivenNames --
        If msCustomerName <> "" Then msCustomerName = msCustomerName & ", "
        txtCustName.Text = VB.Left(UCase(msCustomerName) & s3, 50) '--max 50--SURNAME, GivenNames --
        msCustomerName = VB.Left(msCustomerName & s3, 50) '--max 50--Surname, GivenNames --
        txtCustCompany.Text = msCustomerCompany
        txtCustPhone.Text = msCustomerPhone
        txtCustMobile.Text = msCustomerMobile
        txtCustFax.Text = colFields.Item("fax")("value")
        txtCustABN.Text = colFields.Item("abn")("value")

        '==-- 2468===
        s1 = colFields.Item("addr1")("value")
        s2 = colFields.Item("addr2")("value")
        s3 = colFields.Item("addr3")("value")
        txtCustAddress.Text = s1
        If (s2 <> "") Then txtCustAddress.Text = txtCustAddress.Text & vbCrLf & s2
        If (s3 <> "") Then txtCustAddress.Text = txtCustAddress.Text & vbCrLf & s3
        txtCustAddress.Text = txtCustAddress.Text & vbCrLf & _
                             colFields.Item("suburb")("value") & " " & colFields.Item("state")("value") & " " & _
                                                                                 colFields.Item("postcode")("value")
        s2 = colFields.Item("country")("value")
        If (s2 <> "") Then txtCustAddress.Text = txtCustAddress.Text & vbCrLf & s2
        txtCustEmail.Text = colFields.Item("email")("value")
        '==  NEW !!  ==
        '==  NEW !!  ==
        FrameCust.Text = " Customer_Id: " & mlCustomerId
        txtCustNo.Text = msCustomerBarcode

        '-- Get Purchases for this customer-Id..--
        '==  Call mbRefreshPurchasesList(mRsPurchasesList)

        If mRetailHost1.customerGetSalesHistory(msCustomerBarcode, mlCustomerId, colPurchasesList) Then
            '--ok-
            Call gbLoadListViewFromCollection(colPurchasesList, ListViewPurchases)
            LabPurchases.Text = "Purchase History (" & colPurchasesList.Count() & " Retail Docket Lines)"
        Else

        End If
        '--  refresh jobs list this customer--
        Call mbRefreshJobsList(mRsJobsList)

        ListViewJobs.Focus()
        colPurchasesList = Nothing
        mbSetupCustomer = True
        Exit Function

SetupCustomer_error:
        lngError = Err.Number()
        MsgBox("Runtime Error in JobMatix SetupCustomer.." & vbCrLf & _
                "Error is " & lngError & " = " & ErrorToString(lngError))

    End Function '--setup--
    '= = = = = = = = =  =
    '-===FF->

    '-- ORIGINAL L o a d---
    '-- L o a d---

    '== Private Sub mbOriginal_frmCustHistory_Load()
    Private Sub frmCustHistory_Load(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '== mbActive = False
        frameHistory.Text = ""

        '-- FORCE datagridView cell style to stick..-
        mDataGridViewCellStyleHdr = New DataGridViewCellStyle
        mDataGridViewCellStyleData = New DataGridViewCellStyle

        '--  dataGrids..  set header row styles..
        mDataGridViewCellStyleHdr.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleHdr.BackColor = System.Drawing.SystemColors.Control
        mDataGridViewCellStyleHdr.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleHdr.ForeColor = System.Drawing.SystemColors.WindowText
        mDataGridViewCellStyleHdr.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleHdr.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleHdr.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewHost.ColumnHeadersDefaultCellStyle = mDataGridViewCellStyleHdr
        Me.DataGridViewHost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize

        '--  dataGrids..  set DATA row styles..
        mDataGridViewCellStyleData.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleData.BackColor = System.Drawing.SystemColors.Window
        mDataGridViewCellStyleData.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleData.ForeColor = System.Drawing.SystemColors.ControlText
        mDataGridViewCellStyleData.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleData.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleData.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewHost.DefaultCellStyle = mDataGridViewCellStyleData

        labBusiness.Text = ""
        mlSortOrderJobs = System.Windows.Forms.SortOrder.Descending
        mlSortOrderPurchases = System.Windows.Forms.SortOrder.Descending

        mlSortKeyJobs = 0
        mlSortKeyPurchases = 0

        '== mlJobId = -1
        FrameJobDetails.Text = ""

        LabVersion.Text = "JobTracking- V:" & CStr(My.Application.Info.Version.Major) & "." & _
                         My.Application.Info.Version.Minor & " Build: " & _
                        My.Application.Info.Version.Build & ". Rev " & _
                         My.Application.Info.Version.Revision & "."

        FrameBrowse.Visible = False
        FrameBrowse.Text = "- Customer Lookup -"
        FrameBrowse.Top = FrameCust.Top
        FrameBrowse.Left = FrameCust.Left

        '--  prepare preferred cols..-
        '== mColPrefs = New Collection
        '== mColPrefs.Add("surname")
        '== mColPrefs.Add("given_names")
        '== mColPrefs.Add("company")
        '== mColPrefs.Add("phone")
        '== mColPrefs.Add("mobile")
        '== mColPrefs.Add("barcode")
        '== mColPrefs.Add("account")
        '== mColPrefs.Add("suburb")

        '== PicArrowUp.Visible = False
        '== PicArrowDown.Visible = False

        Call CenterForm(Me)

        '= Me.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + 600)

        Me.Left = My.Computer.Screen.Bounds.Width - Me.Width - 40

        mlFormDesignHeight = VB6.PixelsToTwipsY(Me.Height)
        mlFormDesignWidth = VB6.PixelsToTwipsX(Me.Width)

        '= mlBrowseDesignTop = VB6.PixelsToTwipsY(FrameBrowse.Top)
        '= mlBrowseDesignLeft = VB6.PixelsToTwipsX(FrameBrowse.Left)

    End Sub '--load..-
    '= = = = = = = = = = =
    '-===FF->

    '--EX Activate..-
    '--Activate..-

    '== 'UPGRADE_WARNING: Form event frmCustHistory.Activate has a new behavior. Click for more:
    '=====  'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '== Private Sub frmCustHistory_Activated(ByVal eventSender As System.Object, _
    '==                                         ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

    '== Private Sub frmCustHistory_Load(ByVal eventSender As System.Object, _
    '==                                ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    Private Sub frmCustHistory_Activated(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        Dim colRecord As Collection
        Dim bOK As Boolean = False

        If mbActive Then Exit Sub
        mbActive = True

        '== Call mbOriginal_frmCustHistory_Load()
        mBrowseHost = New clsBrowse3 '== clsBrowse22     '-- clsBrowseHost

        labBusiness.Text = msBusinessName
        If (mlCustomerId >= 0) Then  '--have customer ID..-
            bOK = mRetailHost1.customerGetCustomerRecord("", mlCustomerId, colRecord)
            If bOK Then
                Call mbSetupCustomer(colRecord)
            Else
                MsgBox("Failed to retrieve customer record (Cust_ID: " & mlCustomerId & ") ", MsgBoxStyle.Exclamation)
            End If
        ElseIf (msCustomerBarcode <> "") Then '--have customer..-
            '==If Not mbLookupCustomerId(mlCustomerId, colRecord) Then
            If Not mRetailHost1.customerGetCustomerRecord(msCustomerBarcode, -1, colRecord) Then '--found..--
                '== If Not mbLookupCustomerBarcode(msCustomerBarcode, colRecord) Then
                MsgBox("Failed to retrieve customer record (Barcode: " & msCustomerBarcode & ") ", MsgBoxStyle.Exclamation)
            Else '--ok--
                '--set up customer details.-
                Call mbSetupCustomer(colRecord)
            End If
        Else
            txtCustNo.Focus() '--get one..-
        End If
    End Sub '--Activate..-
    '= = = = = = = = = = =

    '--  resize..--

    'UPGRADE_WARNING: Event frmCustHistory.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub frmCustHistory_Resize(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            '--  cant make smaller than original..-
            If (VB6.PixelsToTwipsY(Me.Height) < mlFormDesignHeight) Then Me.Height = VB6.TwipsToPixelsY(mlFormDesignHeight)
            If (VB6.PixelsToTwipsX(Me.Width) < mlFormDesignWidth) Then Me.Width = VB6.TwipsToPixelsX(mlFormDesignWidth)

            '== FrameBrowse.SetBounds(mlBrowseDesignLeft, mlBrowseDesignTop, _
            FrameCust.SetBounds(8, 72, _
                       (Me.Width - FrameJobDetails.Width - 32), (Me.Height - FrameBrowse.Top - frameHistory.Height - 40))
            '== MSHFlexGridHost.Width = (FrameBrowse.Width - 16)
            '== MSHFlexGridHost.Height = (FrameBrowse.Height - MSHFlexGridHost.Top - 8)
            FrameBrowse.Width = FrameCust.Width
            FrameBrowse.Height = FrameCust.Height

            DataGridViewHost.Width = (FrameBrowse.Width - 16)
            DataGridViewHost.Height = (FrameBrowse.Height - DataGridViewHost.Top - 8)

            FrameJobDetails.Left = (FrameBrowse.Left + FrameBrowse.Width + 8)

            frameHistory.Top = (FrameBrowse.Top + FrameBrowse.Height + 8)
            frameHistory.Width = FrameBrowse.Width

            ListViewJobs.Width = frameHistory.Width - 16
            ListViewPurchases.Width = ListViewJobs.Width

        End If '--w/state--
    End Sub '--resize..-
    '= = = = = = = = = = =
    '-===FF->

    '-- Customer Name Entry/Lookup..--
    '-- Customer Name Entry/Lookup..--
    '-- Customer Name Entry/Lookup..--

    Private Sub txtCustNo_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles txtCustNo.Enter

        '--txtCustName.Text = ""
        '== txtCustNo.Text = "(Cust No.)"
        txtCustNo.SelectionStart = 0
        txtCustNo.SelectionLength = Len(txtCustNo.Text)
        txtCustNo.ReadOnly = False
        '== FrameBrowse.Visible = False
        '== FrameCust.Visible = True
    End Sub '--cust name got focus--
    '= = = =  = =  = =
    '-===FF->

    '--  GOT Function KEY..
    '--- check for F2 for cust Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Sub txtCustNo_KeyUp(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        Dim sHostTablename As String
        Dim lngControl As Integer
        Dim AltDown, ShiftDown, CtrlDown As Integer
        Dim colPrefs As Collection

        ShiftDown = (Shift And VB6.ShiftConstants.ShiftMask) > 0
        AltDown = (Shift And VB6.ShiftConstants.AltMask) > 0
        CtrlDown = (Shift And VB6.ShiftConstants.CtrlMask) > 0

        lngControl = (VB6.ShiftConstants.ShiftMask + VB6.ShiftConstants.AltMask + VB6.ShiftConstants.CtrlMask)
        '--sOrigKey = Trim(txtCustName.Text) '--save orig key--
        If (KeyCode = System.Windows.Forms.Keys.F2) And ((Shift And lngControl) = 0) Then '--lookup cust--
            txtCustNo.Text = ""
            FrameBrowse.Visible = True
            FrameCust.Visible = False
            mBrowseHost.connection = mRetailHost1.connection '== mCnnJet    '--job tracking sql connenction..-
            mBrowseHost.colTables = mRetailHost1.colTables '== mColJetDBInfo
            mBrowseHost.DBname = mRetailHost1.DBname '== ""   '--msSqlDBName
            '== mBrowseHost.tableName = "customer"

            '== mBrowseHost.retailHost = mRetailHost1
            '--  get table/prefs info for this host..--
            If Not mRetailHost1.browseGetPrefColumns("customer", sHostTablename, colPrefs) Then
                MsgBox("Can't translate table name to host table..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            mBrowseHost.tableName = sHostTablename

            mBrowseHost.IsSqlServer = mRetailHost1.IsSqlServer '== False '--JET/ R-M..--
            '== mBrowseHost.FlexGrid = MSHFlexGridHost
            mBrowseHost.DataGrid = DataGridViewHost

            '== mBrowseHost.ArrowUp = PicArrowUp
            '== mBrowseHost.ArrowDown = PicArrowDown
            '--  pass controls..--
            mBrowseHost.showRecCount = labRecCount '--updates rec. retrieval..
            mBrowseHost.showFind = LabFind '--updates Sort Column display..
            mBrowseHost.showTextFind = txtFind '--updates Sort Column display..
            '===sWhere = sWhereCond
            mBrowseHost.WhereCondition = "" '-- sWhere  '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
            mBrowseHost.PreferredColumns = colPrefs '== mColPrefs
            mBrowseHost.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
            FrameBrowse.Enabled = True

            '== MSHFlexGridHost.Clear()
            '== MSHFlexGridHost.ClearStructure()
            '== MSHFlexGridHost.Enabled = True

            txtCustNo.ReadOnly = True
            mLngSelectedRow = -1
            mBrowseHost.Activate() '-- go..--
            txtFind.Focus()
        End If '--F2--

    End Sub '--keyup-
    '= = = =  = =  = =
    '-===FF->

    '--got function key----
    '--- check for F2 for cust Lookup--
    '=======Private Sub txtCustNo_keyUp(KeyCode As Integer, Shift As Integer)

    Private Sub cmdFullBrowse_Click(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cmdFullBrowse.Click

        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        '--customer lookup (ext. browse)..---
        If mRetailHost1.customerLookup(colSelectedRow) Then '--found..--
            '==  Call mbSetupCustomer(colRecord)
            If (colSelectedRow.Count > 0) Then
                sName = CStr(colSelectedRow.Item(1)("name"))
                sValue = CStr(colSelectedRow.Item(1)("value"))
                If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
                    MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                Else '--ok--
                    '--set up customer details.-
                    Call mbSetupCustomer(colRecord)
                    FrameBrowse.Visible = False
                    FrameCust.Visible = True
                End If
            Else
                If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
            End If '--got row..--

        End If '--lookup.-
        '= FrameBrowse.Visible = False
    End Sub
    '= = = = = = = = = =
    '-===FF->

    '--Cust Losing focus..  fetch cust name--
    '--Cust Losing focus..  fetch cust name--
    '== Private Sub txtCustNo_Validating(ByVal eventSender As System.Object, _
    '==                                    ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
    '==                                            Handles txtCustNo.Validating
    '== Dim keepfocus As Boolean = EventArgs.Cancel
    '== Dim s1 As String
    '== Dim sName As String
    '== Dim colFields As Collection
    '== Dim colFld As Collection

    '==      s1 = txtCustNo.Text '--barcode--
    '==     sName = ""
    '==  '== If IsNumeric(s1) Then '--lookup-
    '==     If (s1 <> "") Then
    '== '--lookup RM cust table--
    '== '==If mbLookupCustomerBarcode(s1, colFields) Then
    '==         If mRetailHost1.customerGetCustomerRecord(s1, -1, colFields) Then '--found..--
    '==             Call mbSetupCustomer(colFields)
    '==             sName = msCustomerCompany
    '==         Else '--not found-
    '==             MsgBox("Customer Barcode: " & s1 & " not found..", MsgBoxStyle.Exclamation)
    '==         End If
    '==     ElseIf (mlCustomerId >= 0) Then  '--keep prev entry..--
    '==         sName = msCustomerCompany
    '==         txtCustCompany.Text = sName
    '==     Else
    '== '== MsgBox "cust ID must be numeric..", vbExclamation
    '==         sName = "" '--loop again..-
    '==     End If '--lookup-
    '== '--Wend  '--entry-
    '==     If sName = "" Then
    '== '== txtCustNo.Text = "(Cust No.)"
    '==         txtCustNo.SelectionStart = 0
    '==         txtCustNo.SelectionLength = Len(txtCustNo.Text)
    '==         keepfocus = True '--txtCustName.SetFocus
    '==     End If

    '== 'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '==     System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '== '--End If '--browsing
    '=     eventArgs.Cancel = keepfocus
    '== End Sub '--cust name validate--
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- ENTER KEY on Cust-name ---
    '-- ENTER KEY on Cust-name ---

    Private Sub txtCustNo_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustNo.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sName As String
        Dim colFields As Collection
        '== Dim colFld As Collection
        '== Dim s3, s4 As String
        '-If Not mbBrowsing Then  '--ok-
        s1 = txtCustNo.Text
        sName = ""
        If keyAscii = 13 Then '--enter-
            '== If IsNumeric(s1) Then '--lookup barcode---
            If (s1 <> "") Then
                '--lookup RM cust table--
                '==If mbLookupCustomerBarcode(s1, colFields) Then
                If mRetailHost1.customerGetCustomerRecord(s1, -1, colFields) Then '--found..--
                    Call mbSetupCustomer(colFields)
                    sName = msCustomerCompany
                    '===FrameGoods.Enabled = True
                    '===SSTab1.TabEnabled(k_SSTAB_GOODS) = True
                    '===cmdCustNext.Enabled = True
                Else '--not found-
                    MsgBox("Customer Barcode: " & s1 & " not found..", MsgBoxStyle.Exclamation)
                End If
            ElseIf (mlCustomerId >= 0) Then  '--keep prev entry..--
                sName = msCustomerCompany
                txtCustCompany.Text = sName
            Else
                '== MsgBox "cust ID must be numeric..", vbExclamation
                sName = "" '--loop again..-
            End If '--lookup-
            '--Wend  '--entry-
            If sName = "" Then
                '==txtCustNo.Text = "(Cust No.)"
                txtCustNo.SelectionStart = 0
                txtCustNo.SelectionLength = Len(txtCustNo.Text)
                '--KeepFocus = True '--txtCustName.SetFocus
            Else '--ok-
                '===optCustPriority(0).SetFocus   '--move on..--
            End If
            keyAscii = 0
        End If '--13-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '--End If '--browsing
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--cust name keypress--
    '= = = = = = = = = = =  =  = =  =
    '== end ENTER key --
    '-===FF->

    '-- Timer..--
    '--- Show Job Details..--

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer

        '--  update quote info display if selection has moved..--
        If ListViewJobs.Enabled Then '--looking up list ofjobs..-
            '===LabSpin.Caption = Mid(msPropChars, msPropPos, 1)
            '===msPropPos = msPropPos + 1
            '===If msPropPos > 8 Then msPropPos = 1
            '--update quote display..--
            System.Windows.Forms.Application.DoEvents()
            item1 = ListViewJobs.FocusedItem
            If (item1 Is Nothing) Then '--no selection..-
                Exit Sub
            Else
                lngJobId = CInt(item1.Text) '--1st column has to be job_id..--
                If lngJobId <> mlJobId Then '-- has changed..-
                    Call mbShowJobInfo(lngJobId)
                End If
            End If '--selected..-

        End If '--enables..-

    End Sub '--timer..-
    '= = = = = = = = =
    '-===FF->

    '--BROWSING CUSTOMERS.. --

    '--  F l e x G r i d  E v e n t s..--
    '--  F l e x G r i d  E v e n t s..--

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '-- set new sort column--

    '==FlexGrid== Private Sub MSHFlexgridHost_MouseUpEvent(ByVal eventSender As System.Object, _
    '==FlexGrid==                                     ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseUpEvent)

    '==FlexGrid== Dim lRow, lCol As Integer
    '==FlexGrid== Dim sName As String
    '==FlexGrid==     If eventArgs.button = 1 Then '--left --
    '==FlexGrid==         lRow = MSHFlexGridHost.MouseRow
    '==FlexGrid==         lCol = MSHFlexGridHost.MouseCol
    '==FlexGrid==         If (lRow > 0) And (MSHFlexGridHost.Rows > 1) Then '--NOT header row--
    '==FlexGrid== '=== STUFFS UP PgUp/Dn SCROLLING  ===  cmdViewRecord.SetFocus
    '==FlexGrid==         ElseIf lRow = 0 And (MSHFlexGridHost.Rows > 1) Then  '--in header row--
    '==FlexGrid== '--MsgBox "Left click on col :" & lCol
    '==FlexGrid==             sName = Trim(MSHFlexGridHost.get_TextMatrix(0, lCol)) '--get new column name--
    '==FlexGrid==             Call mBrowseHost.SortColumn(sName)
    '==FlexGrid==             txtFind.Focus()
    '==FlexGrid==         End If '--row 0--
    '==FlexGrid==     End If '--left--
    '==FlexGrid== End Sub '--mouse up--
    '= = = = = = = = = =
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridViewHost_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridViewHost.Sorted

        Dim sName As String
        '-- get new sort column..--

        Dim currentColumn As DataGridViewColumn = DataGridViewHost.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowseHost.SortColumn(sName)

        '==  Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.CurrentCell

    End Sub
    '= = = = = = = = =  = = =
    '-===FF->

    '--mouse activity---  select row to SHOW--
    '--mouse activity---  select row to SHOW--

    '==FlexGrid== Private Sub MSHFlexgridHost_ClickEvent(ByVal eventSender As System.Object, _
    '==FlexGrid==                                        ByVal eventArgs As System.EventArgs)
    '==FlexGrid== Dim lCol, lRow As Integer
    '==FlexGrid== Dim colRowValues As Collection
    '==FlexGrid== Dim colKeys As Collection

    '==FlexGrid==     lRow = MSHFlexGridHost.MouseRow
    '==FlexGrid==     lCol = MSHFlexGridHost.MouseCol
    '==FlexGrid==     If (lRow <= 0) Then '--in header row--
    '==FlexGrid==     Else
    '==FlexGrid== '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
    '==FlexGrid==         mLngSelectedRow = lRow
    '==FlexGrid==         Call mBrowseHost.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
    '==FlexGrid==         If Not (colKeys Is Nothing) Then
    '==FlexGrid== '==MsgBox "Nothing selected.", vbExclamation
    '==FlexGrid==             If colKeys.Count() > 0 Then '--we have selection..-
    '==FlexGrid== '-- Passing JOB-NO to get details....--
    '==FlexGrid== '==lngJobId = CLng(colKeys(1))
    '==FlexGrid== '===If (lngJobId <> mlJobId) Then  '--different panel or row..-
    '==FlexGrid== '======== Call mbShowJobInfo(lngJobId)
    '==FlexGrid== '=== End If
    '==FlexGrid==             End If '--keys.-
    '==FlexGrid==         End If '--nothing.-
    '==FlexGrid==     End If '--row--
    '==FlexGrid== End Sub '--click--
    '-===FF->
    '= = = = = = = = = =

    '--mouse activity---  select row to edit--

    '==FlexGrid== Private Sub MSHFlexgridHost_dblClick(ByVal eventSender As System.Object, _
    '==FlexGrid==                                      ByVal eventArgs As System.EventArgs)
    '==FlexGrid== Dim lngError As Integer
    '==FlexGrid== Dim lCol, lRow, lngId As Integer
    '==FlexGrid== Dim colKeys As Collection
    '==FlexGrid== Dim colSelectedRow As Collection
    '==FlexGrid== Dim colRecord As Collection
    '==FlexGrid== '== Dim sBarcode As String
    '==FlexGrid== Dim sName, sValue As String

    '==FlexGrid==     On Error GoTo MSHFlexgridHost_dblClick_error
    '==FlexGrid==     lRow = MSHFlexGridHost.MouseRow
    '==FlexGrid==     lCol = MSHFlexGridHost.MouseCol
    '==FlexGrid==     If (lRow > 0) Then '-- NOT in header row--
    '==FlexGrid== '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
    '==FlexGrid==         mLngSelectedRow = lRow
    '==FlexGrid==         Call mBrowseHost.SelectRecord(mLngSelectedRow, colKeys, colSelectedRow)

    '==FlexGrid==         If (colSelectedRow.Count > 0) Then
    '==FlexGrid==            sName = CStr(colSelectedRow.Item(1)("name"))
    '==FlexGrid==             sValue = CStr(colSelectedRow.Item(1)("value"))
    '==FlexGrid==             If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
    '==FlexGrid==                 MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
    '==FlexGrid==             Else '--ok--
    '==FlexGrid== '--set up customer details.-
    '==FlexGrid==                 Call mbSetupCustomer(colRecord)
    '==FlexGrid==                 FrameBrowse.Visible = False
    '==FlexGrid==             End If
    '==FlexGrid==         Else
    '==FlexGrid==             If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
    '==FlexGrid==         End If '--got row..--
    '==FlexGrid==     End If '--row--
    '==FlexGrid==     Exit Sub

    '==FlexGrid== MSHFlexgridHost_dblClick_error:
    '==FlexGrid==     lngError = Err.Number()
    '==FlexGrid==     MsgBox("Runtime Error in JobMatix MSHFlexgridHost_dblClick (" & lRow & "/" & lCol & ") sub.." & vbCrLf & _
    '==FlexGrid==             "Error is " & lngError & " = " & ErrorToString(lngError))

    '==FlexGrid== End Sub '--dbl-click--
    '= = = = = = = = = =

    '-- cell click.--
    '-- cell click.--

    Private Sub DataGridViewHost_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridViewHost.CellMouseClick
        Dim lRow, lCol As Integer
        Dim sName As String
        '==Dim i, j, k As Long

        If eventArgs.Button = 1 Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (DataGridViewHost.Rows.Count > 0) Then  '--selected a row.--

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

    Private Sub DataGridViewHost_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridViewHost.CellMouseDoubleClick
        Dim lRow As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            Call mBrowseHost.SelectRecord(mLngSelectedRow, colKeys, colSelectedRow)
            If (colSelectedRow.Count > 0) Then
                sName = CStr(colSelectedRow.Item(1)("name"))
                sValue = CStr(colSelectedRow.Item(1)("value"))
                If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
                    MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                Else '--ok--
                    '--set up customer details.-
                    Call mbSetupCustomer(colRecord)
                    FrameBrowse.Visible = False
                End If
            Else
                If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
            End If '--got row..--
        End If '--row--
    End Sub '--DBL click--
    '= = = = = = = = = =
    '-===FF->

    '--key activity---  select row to edit--

    '-- = REPLACE =   ?????--
    '-- = REPLACE =   ?????--
    '-- = REPLACE =   ?????--

    '==FlexGrid== Private Sub MSHFlexgridHost_KeyPressEvent(ByVal eventSender As System.Object, _
    '==FlexGrid==                              ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent)

    '==FlexGrid== Dim lCol, lRow, lngId As Integer
    '==FlexGrid== Dim colKeys As Collection
    '==FlexGrid== Dim colSelectedRow As Collection
    '==FlexGrid== Dim colRecord As Collection
    '==FlexGrid== '== Dim sBarcode As String
    '==FlexGrid== Dim sName, sValue As String

    '==FlexGrid==     lRow = MSHFlexGridHost.Row
    '==FlexGrid==     lCol = MSHFlexGridHost.Col

    '==FlexGrid== '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
    '==FlexGrid==     If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then
    '==FlexGrid==         If lRow <= 0 Then '--in header row--
    '==FlexGrid==             Beep()
    '==FlexGrid==         Else
    '==FlexGrid== '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
    '==FlexGrid==             mLngSelectedRow = lRow
    '==FlexGrid==             Call mBrowseHost.SelectRecord(mLngSelectedRow, colKeys, colSelectedRow)

    '==FlexGrid==             If colSelectedRow.Count() > 0 Then
    '==FlexGrid==                 sName = CStr(colSelectedRow.Item(1)("name"))
    '==FlexGrid==                 sValue = CStr(colSelectedRow.Item(1)("value"))
    '==FlexGrid==                 If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
    '==FlexGrid==                     MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
    '==FlexGrid==                 Else '--ok--
    '==FlexGrid== '--set up customer details.-
    '==FlexGrid==                     Call mbSetupCustomer(colRecord)
    '==FlexGrid==                     FrameBrowse.Visible = False
    '==FlexGrid==                 End If  '--get-
    '==FlexGrid==             Else
    '==FlexGrid==                 If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
    '==FlexGrid==             End If '--got row..--
    '==FlexGrid==         End If '--row--
    '==FlexGrid==         eventArgs.keyAscii = 0 '--processed--
    '==FlexGrid==     ElseIf eventArgs.keyAscii = System.Windows.Forms.Keys.Escape Then
    '==FlexGrid==         FrameBrowse.Visible = False
    '==FlexGrid==         FrameCust.Visible = True
    '==FlexGrid==     End If '--enter--

    '==FlexGrid== End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

    '-- CUSTOMER Browser.. txt FIND Activity.--
    '-- CUSTOMER Browser.. txt FIND Activity.--
    '--BROWSING CUSTOMERS.. --

    '--JOBS key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        '== lRow = MSHFlexGridHost.Row
        '== lCol = MSHFlexGridHost.Col
        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If (DataGridViewHost.SelectedRows.Count > 0) Then
                '--  use 1st selected row only.
                lRow = DataGridViewHost.SelectedRows(0).Cells(0).RowIndex
                If lRow >= 0 Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '== Call MSHFlexgridHost_KeyPressEvent(MSHFlexGridHost, _
                    '=                  New AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent(iKeyAscii))

                    Call mBrowseHost.SelectRecord(mLngSelectedRow, colKeys, colSelectedRow)
                    If (colSelectedRow.Count > 0) Then
                        sName = CStr(colSelectedRow.Item(1)("name"))
                        sValue = CStr(colSelectedRow.Item(1)("value"))
                        If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
                            MsgBox("Failed to retrieve customer record ( " & _
                                                                sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                        Else '--ok--
                            '--set up customer details.-
                            Call mbSetupCustomer(colRecord)
                            FrameBrowse.Visible = False
                        End If
                    Else
                        If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                    End If '--got row..--
                End If '--row--
            End If  '--sel.count-
            iKeyAscii = 0 '--processed--
        End If '--enter--
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowseHost.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->


    '-  PURCHASES-re-sort on selected column..-
    '--re-sort on selected column..-

    Private Sub listViewPurchases_ColumnClick(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.Windows.Forms.ColumnClickEventArgs) _
                                                           Handles ListViewPurchases.ColumnClick
        Dim lngNewKey As Integer
        Dim colHdr1 As System.Windows.Forms.ColumnHeader = ListViewPurchases.Columns(eventArgs.Column)

        lngNewKey = eventArgs.Column   '==colHdr1.Index - 1 '-- get zero index of column clicked..-
        '--MsgBox "Clicked col-no: " & lngNewKey
        With ListViewPurchases
            .Sort()
            'UPGRADE_ISSUE: MSComctlLib.ListView property ListViewPurchases.SortKey was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            '== .SortKey = lngNewKey '--change col--
            If lngNewKey = mlSortKeyPurchases Then '--same col clicked again..-
                'UPGRADE_ISSUE: MSComctlLib.ListView property ListViewPurchases.Sorted was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
                '== .Sorted = False
                If mlSortOrderPurchases = System.Windows.Forms.SortOrder.Ascending Then
                    mlSortOrderPurchases = System.Windows.Forms.SortOrder.Descending '--invert order..-
                Else
                    mlSortOrderPurchases = System.Windows.Forms.SortOrder.Ascending
                End If
                .Sort()
                'UPGRADE_ISSUE: MSComctlLib.ListView property ListViewPurchases.SortKey was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                '== .SortKey = lngNewKey '--change col--
                .Sorting = mlSortOrderPurchases
            Else '--changed col.--
                .Sorting = System.Windows.Forms.SortOrder.Descending '--start asc..-
                mlSortOrderPurchases = System.Windows.Forms.SortOrder.Descending '--remember.-
            End If
        End With
        mlSortKeyPurchases = lngNewKey '--remember current column.-
        ListViewPurchases.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        ListViewPurchases.ListViewItemSorter = New ListViewItemComparer2(eventArgs.Column, ListViewPurchases.Sorting)
    End Sub '--colClick..-
    '= = = = = = = = =  =

    '--re-sort JOBS on selected column..-

    Private Sub listViewJobs_ColumnClick(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.Windows.Forms.ColumnClickEventArgs) _
                                                    Handles ListViewJobs.ColumnClick
        Dim lngNewKey As Integer
        Dim colHdr1 As System.Windows.Forms.ColumnHeader = ListViewJobs.Columns(eventArgs.Column)

        lngNewKey = eventArgs.Column   '== colHdr1.Index - 1 '-- get zero index of column clicked..-
        '--MsgBox "Clicked col-no: " & lngNewKey
        With ListViewJobs
            .Sort()
            'UPGRADE_ISSUE: MSComctlLib.ListView property ListViewJobs.SortKey was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
            '=== .SortKey = lngNewKey '--change col--
            If lngNewKey = mlSortKeyJobs Then '--same col clicked again..-
                'UPGRADE_ISSUE: MSComctlLib.ListView property ListViewJobs.Sorted was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
                '== .Sorted = False
                If mlSortOrderJobs = System.Windows.Forms.SortOrder.Ascending Then
                    mlSortOrderJobs = System.Windows.Forms.SortOrder.Descending '--invert order..-
                Else
                    mlSortOrderJobs = System.Windows.Forms.SortOrder.Ascending
                End If
                .Sort()
                'UPGRADE_ISSUE: MSComctlLib.ListView property ListViewJobs.SortKey was not upgraded. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                '== .SortKey = lngNewKey '--change col--
                .Sorting = mlSortOrderJobs
            Else '--changed col.--
                .Sorting = System.Windows.Forms.SortOrder.Descending '--start asc..-
                mlSortOrderJobs = System.Windows.Forms.SortOrder.Descending '--remember.-
            End If
        End With
        mlSortKeyJobs = lngNewKey '--remember current column.-
        ListViewJobs.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        ListViewJobs.ListViewItemSorter = New ListViewItemComparer2(eventArgs.Column, ListViewJobs.Sorting)

    End Sub '--colClick..-
    '= = = = = = = = =  =
    '-===FF->

    '--listViewJobs_Click--

    Private Sub listViewJobs_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles ListViewJobs.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer

        '--  update quote info display if selection has moved..--
        item1 = ListViewJobs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            lngJobId = CInt(item1.Text) '--1st column has to be job_id..--
            If lngJobId <> mlJobId Then '-- has changed..-
                Call mbShowJobInfo(lngJobId)
            End If
        End If '--selected..-

    End Sub '--listViewJobs_Click--
    '= = = = = = = =  =

    '--Exit..--
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdClose.Click

        mBrowseHost = Nothing
        Me.Hide()
    End Sub '--exit-
    '= = = = = = = = = =

    '==== end form..===
End Class

' Implements the manual sorting of items by columns.
Class ListViewItemComparer2
    Implements IComparer
    Private col As Integer
    Private order As SortOrder

    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
        col = column
        Me.order = order
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
                        Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer = -1
        returnVal = [String].Compare(CType(x, _
                        ListViewItem).SubItems(col).Text, _
                        CType(y, ListViewItem).SubItems(col).Text)
        ' Determine whether the sort order is descending.
        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If

        Return returnVal
    End Function
End Class
'==  the end..==