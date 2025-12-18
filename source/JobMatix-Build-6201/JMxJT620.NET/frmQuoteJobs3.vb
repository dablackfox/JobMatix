Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class frmQuoteJobs
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	
	'--  J o b T r a c k i n g -----
	'--  J o b T r a c k i n g -----
	'--  J o b T r a c k i n g -----
	
	'-- Form to lookup MYOB R-M Quote and Create New Jobs--
	
	'= = = = = = =
	
	'==== grh ==25-Mar-2010==  Add code to Print Job Form for Each Job Created..-
	'==== grh ==31-Mar-2010==  Add COMBO box (Cat1/Cat2 from Stock) to allow change to Chassis base component...-
	'==== grh ==19-May-2010==  Lookup barcode for NominTech....-
	'=====        Fix Customer Company..--
	'==== grh ==27-May-2010==  DROP "QUOTE" text from NominTech when Inserting NEW JOb.....-
	'==== grh ==30-Jun-2010==  Fix/Load comments (gotFocus) for current job.....-
	'==== grh ==04-Sep-2010==  Enable Reload/Amend/Reprint....-
	'==== grh ==06-Sep-2010==  Use preferred Printer (not Default.)..-
	'====        ------------ AND Show jobs list (if any) during quote browse..-
	
	'== JobMatix V3.==
    '==== grh ==09-Nov-2011==  Re-vamp printing for clsPrintDocs...-

    '==== grh ==08-DEc-2011==  VB.NET version..-
    '==== grh ==21-Dec-2011==  Dropping modelChecklist..-
    '--             (Users must use ServiceModelChecklists..--)
    '--             (Caller must supply Customer_id, Name..--)

    '== JobMatix V30.3037.0.==
    '==== grh ==01-Apr-2012==  Re-vamp..  Quote (Order) is NOW SELECTED-
    '==           in JobMatix main screen and comes in here as input parameter..-
    '==
    '== grh 23Jul2012= 3067.0 ==
    '==   >> Add help provider..
    '==   >> Refresh quote info when Chassis defs changed.
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== 
    '==  grh. JobMatix 3.1.3101 ---  18-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '== 
    '==  grh. JobMatix 3.1.3103 ---  23-Jan-2015 ===
    '==   >>  made txtNoJobs -> READONLY- 
    '==
    '==  grh. JobMatix 3.1.3107.611 ---  11-Jun-2015 ===
    '==        >>  Fix Save error-  Must include Transaction object in Execute..
    '==
    '==  grh. JobMatix 3.1.3107.707 ---  07Jul-2015 ===
    '==        >>  Tidying up MYOB/POS texts..
    '==
    '==   grh Jobmatix 3107.1012- 12-Oct-2015-
    '==        >> frmQuoteJobs.  Adding CheckBox + code 
    '==               to allow user to build ONE job even if no motherboard.
    '==
    '==   grh 3501.0617 --  
    '==       --  frmQuoteJob3:  Move Activated to Shown..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Const k_version As String = "=CreateQuoteJobs==Form Ver:12-Oct-2015==02:48pm="
	
	Const k_statusCancelled As String = "97-Cancelled"
	Const k_MAXJOBS As Short = 15
	'--Public gbclosingDown As Boolean  '--ex xbsSubs--
	
	'--Dim msAppPath As String
	'--Dim mColTables As Collection
	
    Private msDBname As String = ""
    Private mCnnJobs As OleDbConnection '== ADODB.Connection
	'== Private mCnnJet As ADODB.connection
	
	Private mRetailHost1 As _clsRetailHost

    Private mlOrderId As Integer = -1
    Private mColSelectedRow As Collection  '--selected quote row/data..

	'--Dim msTableName As String
	Private msRMSelect As String
	
    Private mbCancelled As Boolean = False
    Private mbStartupDone As Boolean = False
    Private mbAmending As Boolean = False  '--amending existing job-set..
    Private mbDataChanged As Boolean = False  '--for amend..-
	
	'--save result--
    Private msNewKey As String = ""
    Private msNewDisplay As String = ""
    Private mbClosingDown As Boolean = False  '--ex xbsSubs--
	

    '==3037= Private mlGridLeft, mlGridTop As Integer
    Private mlSortKey As Integer '--col index for sort..-
	Private mlSortOrder As Integer '-asc/desc-
	
	'= = = = = = =
	Private msBusinessName As String
	Private msBusinessShortName As String
	Private msBusinessAddress1 As String
	Private msBusinessAddress2 As String
	
	
	Private msDefaultChassisCat1 As String '--cat1 value for basic part..  ie CASE or motherboard..-
	Private msDefaultChassisCat2 As String
	Private msChassisCat1 As String '--cat1 value for basic part..  ie CASE or motherboard..-
	Private msChassisCat2 As String
	
	Private mlQuoteChassisQty As Integer
	Private mlNoChassisTypes As Integer
	Private msStaffName As String
	Private mlStaffId As Integer
	
	'-- current Quote.-
	'== Private mRstQuote As ADODB.Recordset
	Private mColQuoteRecords As Collection
	
	
    '== Private mRsItems As ADODB.Recordset  '--RS items for current quote.-
	
	Private mColQuoteItems As Collection '--items for current quote as COLLECTION.-
    Private malItemJobNo() As Integer '-- ItemNo gives array index ->assigned job seq.no.
    Private mbCanBuild As Boolean = False
	
	Private mlNoJobs As Integer '--number of jobs to be created for this quote..-
	Private msCustomerBarcode As String
	Private mlCustomerId As Integer
	Private msCustomerName As String
	Private msCustomerCompany As String
	Private msCustomerPhone As String
	Private msCustomerMobile As String
	Private msTotalValue As String
	Private mCurTotalValue As Decimal
	'= = = = = = = = = = = = = = =
	Private masNominTech() As String '--Nomin Tech for each Job..-
	Private masComments() As String '-Special Comments for each Job..-
	Private malJobIds() As Integer '-- save created jobnos..-
	Private mColPrintJobParts As Collection '--collection of jobs (key=CreatedJobNo)..
    '-----                  ====   Each "job" is collection of print lines of parts for that job..-

    '== Private mColModelChecklist As Collection '-DISCONTINUED in vb.net version.-
	'= = = = = = = = = = = =  = = =
	
	'--  Define MODEL stock requirements for one system...--
	Private mlMaxModelParts As Integer
    Private malModelStockQty(,) As Integer '--Slot for every distinct stock ID in quote. ->[stock-id,QtyNeeded]
	'--- ie r/set rows in SalesOrderline..--  EXCLUDES CHASSIS Parts..-
	
	Private msChassisStockIds As String '-- holds DISTINCT chassis stock_Id's (trimmed) found in Quote Items..-
	'--  "/id1/id2/....."  ----
	Private mlJobNoBackColour As Integer
	Private mlCurrentJobNo As Integer
	'= = = = = = = = = = = = = = =
	Private msPropChars As String
	Private msPropPos As Short
	'= = = = = = = = = = = =  =
	Private mDateJobCreated As Date
	Private msJobCreatedBy As String
    Private msFullJobList As String '--for printout..-

    '--  Input Customer to look for.-
    '==3037= Private msCustomerSearchName As String = ""
    '==3037= Private mlCustomerSearchId As Integer = -1

	'--  printers--
    Private msColourPrtName As String = ""
	'= = = = = = = = = =  = = = ==
	
	'--properties as input parameters--
	
	'== Property Let RMconnection(conn1 As ADODB.connection)
	
	'==     Set mCnnJet = conn1
	'== End Property
	'- - - - - -
	
	WriteOnly Property retailHost() As _clsRetailHost
		Set(ByVal Value As _clsRetailHost)
			
			mRetailHost1 = Value
		End Set
	End Property '-host..-
	'= = = = = = = = = = = = = = = = =

    WriteOnly Property JobsConnection() As OleDbConnection '== ADODB.Connection ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property
	'- - - - - -
	
	WriteOnly Property DBname() As String
		Set(ByVal Value As String)
			msDBname = Value
		End Set
	End Property
    '= = = = =  = = =  = =

    WriteOnly Property OrderId() As Integer
        Set(ByVal Value As Integer)
            '==3037= mlCustomerSearchId = Value
            mlOrderId = Value
        End Set
    End Property '--staff--
    '= = = = = = =  = =  = =

    '-- Selected quote row..
    WriteOnly Property ColSelectedRow() As Collection
        Set(ByVal Value As Collection)
            mColSelectedRow = Value
        End Set
    End Property  '--selected rwo.--
    '= = = = = = = = = = = = == = 
	
	WriteOnly Property BusinessName() As String
		Set(ByVal Value As String)
			msBusinessName = Value
		End Set
	End Property
	'= = = = =  = = =  = =
	
	WriteOnly Property BusinessAddress1() As String
		Set(ByVal Value As String)
			msBusinessAddress1 = Value
		End Set
	End Property
	'= = = = =  = = =  = =
	
	WriteOnly Property BusinessAddress2() As String
		Set(ByVal Value As String)
			msBusinessAddress2 = Value
		End Set
	End Property
	'= = = = =  = = =  = =
	
	'--  identify chassis compnent ie case..-
	WriteOnly Property ChassisCat1() As String
		Set(ByVal Value As String)
			msChassisCat1 = Value
		End Set
	End Property '--cat1--
	'= = = = = = = = = =
	
	WriteOnly Property ChassisCat2() As String
		Set(ByVal Value As String)
			msChassisCat2 = Value
		End Set
	End Property '--cat2--
	'= = = = = = =  = =  = =
	
    '-- Staff Name/ID..-
	WriteOnly Property StaffName() As String
		Set(ByVal Value As String)
			msStaffName = Value
		End Set
	End Property '--staff--
	'= = = = = = =  = =  = =
	WriteOnly Property StaffId() As Integer
		Set(ByVal Value As Integer)
			mlStaffId = Value
		End Set
	End Property '--staff--
	'= = = = = = =  = =  = =

    '-- Staff Name/ID..-
    WriteOnly Property CustomerName() As String
        Set(ByVal Value As String)
            '==3037= msCustomerSearchName = Value
            msCustomerName = Value
        End Set
    End Property '--cust--
    '= = = = = = =  = =  = =
    WriteOnly Property CustomerId() As Integer
        Set(ByVal Value As Integer)
            '==3037= mlCustomerSearchId = Value
            mlCustomerId = Value
        End Set
    End Property '--cust--
    '= = = = = = =  = =  = =

    '--  printers..--

    WriteOnly Property ColourPrtName() As String
        Set(ByVal Value As String)

            msColourPrtName = Value
        End Set
    End Property '--prtColour
    '= = = = = = = = = = =  =  = =  =


    '-- end properties..--
    '-- end properties..--
    '-- end properties..--
    '= = = = = = = = = = =
    '-===FF->

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =


    '--convert numeric data for sorted display..-

    Private Function msFormat(ByVal v1 As Object, ByVal vType As Object, ByVal lSize As Integer) As String
        Dim sResult As String
        Dim sType As String '--sql type--

        sResult = CStr(v1) '--for strings..-
        sType = UCase(gsGetSqlType(vType, lSize))
        If (sType = "MONEY") Or (sType = "SAMLLMONEY") Then '--currency..-
            sResult = New String(" ", 9)
            sResult = RSet(FormatCurrency(v1, 2), Len(sResult))
        ElseIf gbIsNumericType(sType) Then
            sResult = New String(" ", 5)
            sResult = RSet(VB6.Format(v1, "####0"), Len(sResult))

        ElseIf gbIsDate(sType) Then
            sResult = VB6.Format(CDate(v1), "yyyy-mm-dd")
        End If
        msFormat = sResult

    End Function '--convert--
    '-===FF->

    '--  get value of 1st rst column for SELECT..--

    Private Function mbGetSelectValue(ByVal sSql As String, ByRef colResult As Collection) As Boolean
        Dim rs1 As DataTable  '= ADODB.Recordset
        '= Dim sErrorMsg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetSelectValue = False
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If Not (rs1 Is Nothing) Then
                colResult = New Collection
                If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '= rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        If Not IsDBNull(dataRow1.Item(0)) Then
                            colResult.Add(dataRow1.Item(0))
                        End If '--null.-
                    Next dataRow1
                    '== While Not rs1.EOF
                    '==   rs1.MoveNext()
                    '== End While '--eof..-
                    mbGetSelectValue = True '--got something..-
                End If '--bof/empty-
                '== rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction, _
                                      ByRef sLastSqlErrorMessage As String) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecuteSql = True   '--ok--
            '== MsgBox("Sql exec ok. " & intAffected & " records affected..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbExecuteSql: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            sLastSqlErrorMessage = sErrorMsg
            '== MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Get list of Jobs for given Quote.--

    Private Function mbGetQuoteJobsList(ByRef lngOrderId As Integer, ByRef colResult As Collection, ByRef sFullJobList As String, ByRef sWhereList As String) As Boolean
        Dim sSql As String
        Dim v1 As Object
        mbGetQuoteJobsList = False
        sSql = "SELECT DISTINCT QuotePart_JobId,QuotePart_OrderId, Jobs.JobStatus  "
        sSql = sSql & " FROM [QuoteJobParts] LEFT JOIN [Jobs] ON (Jobs.Job_Id=QuoteJobParts.QuotePart_JobId)  "
        sSql = sSql & " WHERE (QuotePart_OrderId= " & CStr(lngOrderId) & ") AND (LEFT(Jobs.JobStatus,2) <'90'); "
        If mbGetSelectValue(sSql, colResult) Then
            sFullJobList = ""
            sWhereList = ""
            For Each v1 In colResult
                If (sFullJobList <> "") Then sFullJobList = sFullJobList & ", "
                msFullJobList = msFullJobList & CStr(v1)
                '-- Build Wherelist to get Statuses for these jobs..--
                If (sWhereList <> "") Then sWhereList = sWhereList & " OR "
                sWhereList = sWhereList & " (Jobs.Job_Id= " & CStr(v1) & ") "
            Next v1
            sFullJobList = sFullJobList & "."
            mbGetQuoteJobsList = True
        End If '--select..-
    End Function '--get jobs..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Load cboChassis with DISTINCT Cat1/Cat2 Combinations..-
    '-- Load cboChassis with DISTINCT Cat1/Cat2 Combinations..-

    Private Function mbLoadChassisCombo() As Boolean
        Dim sSql As String
        Dim s1 As String
        Dim i, j, k, lCount, ix As Integer
        Dim lngCurrentChassis As Integer
        '== Dim rs1 As ADODB.Recordset
        Dim colDistinctCat1Cat2 As Collection
        Dim colItem As Collection
        Dim v1 As Object
        Dim sCat1, sCat2 As String

        mbLoadChassisCombo = False
        lngCurrentChassis = -1
        '== sSql = "SELECT DISTINCT Cat1,Cat2 FROM Stock ORDER BY Cat1,Cat2; "
        '== Screen.MousePointer = vbHourglass
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '==If Not gbGetRst(mCnnJet, rs1, sSql) Then
        If Not mRetailHost1.stockGetDistinctCategoryList(colDistinctCat1Cat2) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Cat1/Cat2 recordset.." + vbCrLf, MsgBoxStyle.Exclamation)
            '==rs1 = Nothing
            Exit Function
        Else   '--ok--
            '--fill COMBO box with record fields--
            '== If Not (rs1.BOF And rs1.EOF) Then   '--ok.. not empty--
            If Not (colDistinctCat1Cat2 Is Nothing) Then   '--ok.. not empty--
                '== rs1.MoveFirst()
                lCount = 0
                cboChassis.Items.Clear()
                '--scan recordset and load--
                '== While (Not rs1.EOF)
                For Each colItem In colDistinctCat1Cat2
                    '---load current item--
                    sCat1 = colItem("cat1")("Value")   '== rs1("cat1").Value
                    sCat2 = colItem("cat2")("Value")   '==  rs1("cat2").Value
                    s1 = Space(16)
                    Mid(s1, 1, 6) = sCat1
                    Mid(s1, 9, 6) = sCat2
                    cboChassis.Items.Add(s1)
                    '-- check if this is same as initial setup..-
                    If (LCase(sCat1) = LCase(msChassisCat1)) And (LCase(sCat2) = LCase(msChassisCat2)) Then
                        lngCurrentChassis = lCount '--to set listindex..-
                    End If
                    lCount = lCount + 1
                    '== rs1.MoveNext()
                Next colItem
                '== End While   '--eof
            Else
                MsgBox("No Cat1/Cat2 items to show...", vbExclamation)
            End If  '--not empty--
            '== cboChassis.ListIndex = lngCurrentChassis
            cboChassis.SelectedIndex = lngCurrentChassis
            mbLoadChassisCombo = True
        End If  '-got rs--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function  '--LoadChassisCombo--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-

    Private Function mbGetJobRecord(ByVal lngJobNo As Integer, ByRef ColJobFields As Collection) As Boolean

        Dim RsJob As DataTable  '= ADODB.Recordset
        Dim sSql, sName As String
        '== Dim fld1 As ADODB.Field
        Dim colFld As Collection

        mbGetJobRecord = False
        sSql = "SELECT * from [jobs]  "
        sSql = sSql & " WHERE (job_id=" & CStr(lngJobNo) & ")  " & vbCrLf
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, RsJob, sSql) Then
            MsgBox("Failed to get JOB recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) And (RsJob.Rows.Count > 0) Then
            '== If RsJob.BOF And (Not RsJob.EOF) Then
            '== RsJob.MoveFirst()
            '= End If
            Dim dataRow1 As DataRow = RsJob.Rows(0)  '==first row-
            '== If (Not RsJob.EOF) Then '---And (cx < 100)
            '--return complete row..-
            For Each column1 As DataColumn In RsJob.Columns '== fld1 In RsJob.Fields
                colFld = New Collection
                sName = column1.ColumnName
                colFld.Add(LCase(sName), "name")
                colFld.Add(dataRow1.Item(sName), "value")
                ColJobFields.Add(colFld, LCase(sName))
            Next column1 '=fld1
            mbGetJobRecord = True
            '== Else '--not found-
            '== End If '-eof-
            '= RsJob.Close()
        End If '--rs-
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--- SHOW parts in listViewQuote..  ie all POOL parts..-

    Private Function mbShowQuoteParts() As Boolean
        '== Dim kx, ix, vx As Integer
        Dim itemNo, lngCount As Integer
        Dim colItem As Collection
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim curTotalCost As Decimal

        ListViewQuote.Items.Clear()
        curTotalCost = 0
        lngCount = 0
        '--browse all items and pick out those assigned to POOL..  ie free..-
        For itemNo = 0 To UBound(malItemJobNo)
            If (malItemJobNo(itemNo) = 0) Then '--belongs to POOL..-
                colItem = mColQuoteItems.Item(CStr(itemNo + 1))  '-- key is 1-based index..-
                item1 = ListViewQuote.Items.Add(colItem.Item("cat1")("value"))
                '== item1.Text = colItem.Item("cat1")("value")
                item1.SubItems.Add(colItem.Item("cat2")("value"))
                item1.SubItems.Add(colItem.Item("description")("value"))
                item1.SubItems.Add(colItem.Item("barcode")("value"))
                item1.SubItems.Add(colItem.Item("OrderQty")("value"))
                item1.SubItems.Add(colItem.Item("sell_inc")("value"))
                item1.SubItems.Add(colItem.Item("stock_id")("value"))
                curTotalCost = curTotalCost + CDec(colItem.Item("sell_inc")("value"))
                item1.SubItems.Add(FormatCurrency(colItem.Item("sell_inc")("value"), 2))
                item1.Tag = CStr(itemNo) & "/" & CStr(colItem.Item("stock_id")("value"))
                lngCount = lngCount + 1
            End If '--belongs..-
        Next itemNo
        LabPoolInfo.Text = lngCount & " Parts.." & " Total: " & FormatCurrency(curTotalCost, 2)

    End Function '--show pool..--
    '= = = = = = = = = = = = =

    '-- show parts in listViewJob..--
    Private Function mbShowJobParts(ByVal lngJobSeqNo As Integer) As Boolean
        Dim ix As Integer
        Dim itemNo, lngCount As Integer
        Dim colItem As Collection
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim curTotalCost As Decimal

        ListViewJob.Items.Clear()
        For ix = 0 To mlNoJobs - 1
            LabJobSeq(ix).BackColor = System.Drawing.ColorTranslator.FromOle(mlJobNoBackColour) '--all grey..-
        Next ix
        curTotalCost = 0
        lngCount = 0
        '--browse all items and pick out those assigned to this job.-
        For itemNo = 0 To UBound(malItemJobNo)
            If lngJobSeqNo = malItemJobNo(itemNo) Then '--belongs..-
                colItem = mColQuoteItems.Item(CStr(itemNo + 1))  '--  key is 1-based index.-
                'UPGRADE_ISSUE: MSComctlLib.ListItems method ListViewJob.ListItems.Add was not upgraded. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                item1 = ListViewJob.Items.Add(colItem.Item("cat1")("value"))
                '== item1.Text = colItem.Item("cat1")("value")
                item1.SubItems.Add(colItem.Item("description")("value"))
                item1.SubItems.Add(colItem.Item("barcode")("value"))
                curTotalCost = curTotalCost + CDec(colItem.Item("sell_inc")("value"))
                item1.SubItems.Add(FormatCurrency(colItem.Item("sell_inc")("value"), 2))
                item1.Tag = CStr(itemNo)
                lngCount = lngCount + 1
            End If '--belongs..-
        Next itemNo
        LabJobInfo.Text = lngCount & " Items. Total: " & FormatCurrency(curTotalCost, 2)
        If (malJobIds(lngJobSeqNo - 1) > 0) Then '--have actual jobno.-
            LabJobInfo.Text = LabJobInfo.Text & "  =JobId: " & malJobIds(lngJobSeqNo - 1) & "="
        End If
        LabJobSeq(lngJobSeqNo - 1).BackColor = System.Drawing.Color.White

        mlCurrentJobNo = lngJobSeqNo '--set current job no..-
        txtNominTech.Enabled = False '--prevent change event..-
        txtComments.Enabled = False
        txtNominTech.Text = masNominTech(lngJobSeqNo - 1)
        txtComments.Text = masComments(lngJobSeqNo - 1)
        txtNominTech.Enabled = True
        txtComments.Enabled = True
        If (mlCurrentJobNo = 1) Then
            cmdCopyComment.Enabled = True
        Else
            cmdCopyComment.Enabled = False
        End If
    End Function '--show job..-
    '= = = = = = =  =
    '-===FF->

    '--  get all HEADER info for selected quote..-
    '--- '==3037=  FROM input Quote info..-

    Private Function mbLoadQuoteHeaderInfo() As Boolean
        Dim ix As Integer
        Dim sMsg As String
        Dim s2, s1 As String
        '==3037= Dim item1 As System.Windows.Forms.ListViewItem
        Dim col1 As Collection

        '==3037= item1 = ListViewSalesOrders.FocusedItem
        '==3037= If (item1 Is Nothing) Then '--no selection..-
        '==3037=     MsgBox("No item is selected..", MsgBoxStyle.Exclamation)
        '==3037=     Exit Function
        '==3037= Else
        '==3037= '- return collection of flds selected item..--
        '==3037= mColSelectedRow = New Collection
        '==3037= If ListViewSalesOrders.Columns.Count > 0 Then
        '==3037= For ix = 0 To (ListViewSalesOrders.Columns.Count - 1)
        '==3037=    col1 = New Collection
        '==3037=    If ix = 0 Then '--first col.-
        '==3037=       s1 = ListViewSalesOrders.Columns.Item(0).Text
        '==3037=       s2 = item1.Text
        '==3037=    Else
        '==3037=       s2 = item1.SubItems(ix).Text
        '==3037=       s1 = ListViewSalesOrders.Columns.Item(ix).Text
        '==3037=    End If '--first.-
        '==3037=    col1.Add(s1, "name")
        '==3037=    col1.Add(s2, "value")
        '==3037=    mColSelectedRow.Add(col1, LCase(s1)) '--add column to result..-
        '==3037= Next ix
        '==3037= End If '--count.-
        '==3037= End If
        '--mbStartupDone = False
        '--clear cancel f;ag--

        sMsg = ""
        '--mlOrderId = -1
        msCustomerBarcode = ""
        mCurTotalValue = 0
        msTotalValue = ""
        mlNoJobs = 0
        mlQuoteChassisQty = 0 '--total of m/boards in quote..-
        mlNoChassisTypes = 0

        msCustomerName = mColSelectedRow.Item("surname")("value") + ", " + mColSelectedRow.Item("given_names")("value")
        If Trim(msCustomerName) = "," Then
            msCustomerName = ""
        End If
        msCustomerCompany = mColSelectedRow.Item("company")("value")
        '--If Not bCancelled Then   '--selected--
        '--show selected row..--
        For Each col1 In mColSelectedRow
            sMsg = sMsg + col1.Item("name") & " = " & col1.Item("value") & vbCrLf
            If (LCase(col1.Item("name")) = "order_id") Then
                mlOrderId = CInt(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custid") Then
                mlCustomerId = CInt(col1.Item("value"))
                '--  save cust no also..--
            ElseIf (LCase(col1.Item("name")) = "custbarcode") Then
                msCustomerBarcode = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custcompany") Then
                msCustomerCompany = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custphone") Then
                msCustomerPhone = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custmobile") Then
                msCustomerMobile = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "total_inc") Then
                mCurTotalValue = CDec(col1.Item("value"))
            End If
        Next col1 '--col1-

        '--End If
        msTotalValue = VB6.Format(mCurTotalValue, "    $0.00")
        labCustSearch.Text = msCustomerName & vbCrLf & msCustomerCompany
        LabOrderDetail.Text = "OrderNo: " & mlOrderId & vbCrLf & _
                            "Customer: " & msCustomerName & vbCrLf & "Company: " & msCustomerCompany & vbCrLf & _
                                 "Phone: " & msCustomerPhone & " - " & msCustomerMobile & vbCrLf
        LabOrderDetail.Text = LabOrderDetail.Text & "Total Value: " & msTotalValue & vbCrLf
        For ix = 0 To k_MAXJOBS - 1
            LabJobSeq(ix).Visible = False
            LabJobSeq(ix).BackColor = System.Drawing.ColorTranslator.FromOle(mlJobNoBackColour) '--all grey..-
            LabJobSeq(ix).Enabled = False
        Next ix
    End Function '--mbLoadQuoteHeaderInfo..-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Set up jobs..-
    Private Function mbSetUpQuoteJobs(ByRef colQuoteLines As Collection, _
                                        ByVal intNoJobs As Integer)
        Dim ix, iq, lngCount, lngQuotePartsCount As Integer
        Dim s1 As String
        Dim lngStockId As Integer
        Dim lngQty As Integer


        '-- To Build Model.. Initialise.--
        Erase malModelStockQty
        mlMaxModelParts = 0
        '===  mRsItems.MoveFirst
        '--scan recordset and load Jobs MODEL..---
        '===  While (Not mRsItems.EOF)

        '--  Iterate over BASE recordset list.--
        '----  1 record per stock type..-
        For Each colItem As Collection In colQuoteLines
            lngStockId = CInt(colItem.Item("stock_id")("Value"))
            s1 = "/" & Trim(CStr(lngStockId)) & "/" '--  to identify if Chassis-
            lngQty = CInt(colItem.Item("OrderQty")("value")) '==  CLng(mRsItems("OrderQty").Value)  '--parts listview.-
            If InStr(msChassisStockIds, s1) > 0 Then '-this is a Chassis part..-
            ElseIf (lngQty > 0) Then  '--ONLY sub-parts go into model..-
                '-- build model info this part..-
                mlMaxModelParts = mlMaxModelParts + 1
                'UPGRADE_WARNING: Lower bound of array malModelStockQty was changed from 1,1 to 0,0. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                ReDim Preserve malModelStockQty(1, mlMaxModelParts - 1)
                malModelStockQty(0, mlMaxModelParts - 1) = lngStockId '== CLng(mRsItems("stock_id").Value)
                malModelStockQty(1, mlMaxModelParts - 1) = lngQty \ intNoJobs '--items per system this stock_id
            End If '--chassis
            '===   mRsItems.MoveNext
            '===   Wend  '--eof-
        Next colItem
        '==done=
        Erase masNominTech
        Erase masComments
        Erase malJobIds
        '== 'UPGRADE_WARNING: Lower bound of array masNominTech was changed from 1 to 0. Click for more: '
        '==ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
        ReDim masNominTech(intNoJobs - 1)
        ReDim malJobIds(intNoJobs - 1)
        For ix = 0 To (intNoJobs - 1)
            masNominTech(ix) = "" '--init..-
            malJobIds(ix) = -1 '--initialise.-
        Next ix
        LabOrderDetail.Text = LabOrderDetail.Text & "Number of Jobs: " & intNoJobs & vbCrLf
        LabOrderDetail.Text = LabOrderDetail.Text & "Total no. of Parts: " & lngQuotePartsCount & vbCrLf
        LabStatus.Text = "OK to build.."
        LabStatus.BackColor = System.Drawing.Color.Lime
        cmdQuoteOK.Enabled = True
        cmdCopyComment.Enabled = False
        System.Windows.Forms.Application.DoEvents()

    End Function  '-mbSetUpQuoteJobs -
    '= = = = = = = = = = = =
    '-===FF->

    '--  get all HDR/DETAIL info for selected quote..-
    '--  get all HDR/DETAIL info for selected quote..-

    Private Function mbLoadQuoteInfo() As Boolean
        Dim ix, iq, lngCount As Integer
        Dim lngChassisQty, lngNoCols As Integer
        Dim lngType, lngSize As Integer
        '= Dim lngStockId As Integer
        Dim lngQuotePartsCount As Integer
        Dim lngQty As Integer
        '== Dim sSql, sMsg As String
        Dim s1 As String
        Dim sWhereList As String
        '--Dim sQtyField As String
        Dim item1 As System.Windows.Forms.ListViewItem
        '== Dim col1 As Collection
        Dim colItem As Collection
        '== Dim ColQuote As Collection
        Dim colResult As Collection
        Dim colQuoteLines As Collection
        '==Dim fldx As ADODB.Field
        Dim colFldx As Collection
        Dim v1 As Object

        mbLoadQuoteInfo = False
        lngQuotePartsCount = 0
        cmdSave.Enabled = False
        '== cmdReprint.Enabled = True

        Call mbLoadQuoteHeaderInfo() '--load hdr stuff.-
        ListViewQuote.Items.Clear()

        '--retrieve all parts/items for this quote..-
        If Not mRetailHost1.quoteGetStocklist(mlOrderId, colQuoteLines, mColQuoteItems) Then
            MsgBox("No Quote stock items returned..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (mColQuoteItems Is Nothing) Then
            MsgBox("No Quote items collection returned..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        lngNoCols = 0
        '==  DONE  === (retailHost)   Set mColQuoteItems = New Collection '--  build static collection all items..--

        '-- NB: mColQuoteItems is the expanded list..
        '--- "colQuoteLines" is the base collection of the SalesLines recordset..-

        '-- set up listview..--
        LabPoolInfo.Text = ""

        '--  Build column headers..-
        If (mColQuoteItems.Count() > 0) Then '--have some records.-
            ListViewQuote.Columns.Clear()
            colItem = mColQuoteItems.Item(1) '--get 1st record..-
            '= For Each fldx In mRsItems.Fields
            For Each colFldx In colItem
                lngNoCols = lngNoCols + 1
                '== s1 = "" & CStr(fldx.Name)
                s1 = Trim(CStr(colFldx.Item("name")))
                If LCase(s1) = "description" Then
                    ListViewQuote.Columns.Add("", s1, (CInt(ListViewQuote.Width) \ 4))
                Else
                    ListViewQuote.Columns.Add("", s1, (CInt(ListViewQuote.Width) \ 8))
                End If
                '--If LCase(s1) = "orderqty" Then sQtyField = s1
            Next colFldx '--fldx  --
        End If '--count..-

        '-- Load main items collection and left listView..--
        '-- discover no of jobs (individual systems..) in this quote.-
        '--  save Chassis or m'board qty if defined..-
        '--If msChassisCat1 <> "" Then
        msChassisStockIds = "/" '--saves distinct ids.-
        lngCount = 0
        '== If Not (mRsItems.BOF And mRsItems.EOF) Then   '--ok.. not empty--
        If (mColQuoteItems.Count() > 0) Then '--have some records.-
            '==    mRsItems.MoveFirst
            '--scan recordset and load--
            '==    While (Not mRsItems.EOF)

            '--  NOTE..  collection now include MULTIPLE items-
            '-- according to OrderQty..-

            '-- 1st Pass through items to get Chassis ino..--
            For Each colItem In mColQuoteItems
                lngChassisQty = 0 '--temp store.-
                If msChassisCat1 <> "" Then
                    '===  If InStr(LCase(mRsItems("cat1")), LCase(msChassisCat1)) > 0 Then '--cat1 fits..-
                     If InStr(LCase(colItem.Item("cat1")("value")), LCase(msChassisCat1)) > 0 Then '--cat1 fits..-
                        If (msChassisCat2 = "") Then '--enough id..-
                            '== lngChassisQty = CLng(mRsItems("OrderQty"))  '-- ie., no of m/boards = the no of jobs..-
                            lngChassisQty = CInt(colItem.Item("OrderQty")("value")) '-- ie., no of m/boards = the no of jobs..-
                        Else '--cat2 must fit..-
                            '==  If InStr(LCase(mRsItems("cat2")), LCase(msChassisCat2)) > 0 Then '--cat2 also fits..-
                            If InStr(LCase(colItem.Item("cat2")("value")), LCase(msChassisCat2)) > 0 Then '--cat2 also fits..-
                                lngChassisQty = CInt(colItem.Item("OrderQty")("value")) '== CLng(mRsItems("OrderQty"))
                            End If
                        End If
                    End If '--cat1..-
                End If '--chassis1--
                If (lngChassisQty > 0) Then '--found a m/board..-
                    mlQuoteChassisQty = mlQuoteChassisQty + 1  '== lngChassisQty '--count ALL m/board instances..-
                    '--  add to stock-id list if nor alreday seen..
                    s1 = "/" & Trim(CStr(colItem.Item("stock_id")("Value"))) & "/"
                    If (InStr(msChassisStockIds, s1) <= 0) Then  '--Not found.. is new type--
                        mlNoChassisTypes = mlNoChassisTypes + 1 '--count m/board TYPES (ie stock types.-)..-
                        '==  count every chassis as a job..-
                        '==If mlNoJobs > 0 Then '--too many m'board types..-
                        '==   mlNoJobs = -1
                        '==Else '-ok-
                        '===   msChassisStockIds = msChassisStockIds & Trim(CStr(mRsItems("stock_id").Value)) & "/"
                        msChassisStockIds = msChassisStockIds & Trim(CStr(colItem.Item("stock_id")("Value"))) & "/"
                    End If
                    mlNoJobs = mlNoJobs + 1  '==lngChassisQty
                    '==End If
                End If
            Next colItem  '--ist pass.

            '--  2nd pass to load listview.-
            For Each colItem In mColQuoteItems
                '--  load current item into listView..-
                lngQty = 1 '--assume no qty fld..--
                lngQty = CInt(colItem.Item("OrderQty")("value")) '== CLng(mRsItems("OrderQty").Value)  '--parts listview.-
                '--  !!!!  NB-  NEED multiple instances of item as per OrderQty--
                '----  So it can correspond to ListView..--
                '-- Make a main collection item for each instance of part..--
                '== If (lngQty > 0) Then
                lngQuotePartsCount = lngQuotePartsCount + 1  '= + lngQty
                lngCount = lngCount + 1
                 ix = 0
                For Each colFldx In colItem
                    v1 = colFldx.Item("value")
                    lngType = CInt(colFldx.Item("type"))
                    lngSize = CInt(colFldx.Item("definedSize"))
                    s1 = msFormat(v1, lngType, lngSize)
                    If ix = 0 Then
                        '== item1.Text = s1
                        item1 = ListViewQuote.Items.Add(s1)
                    Else
                        item1.SubItems.Add(s1)
                    End If
                    ix = ix + 1
                Next colFldx '== ix
                item1.Tag = CStr(lngCount - 1) & "/" & Trim(CStr(colItem.Item("stock_id")("Value")))
 
                'UPGRADE_WARNING: Lower bound of array malItemJobNo was changed from 1 to 0. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                ReDim Preserve malItemJobNo(lngCount - 1) '-- set up location array for item/jobno..-
                 '== End If '--qty>0 --
                '==  mRsItems.MoveNext
                '==  Wend  '--eof-
            Next colItem  '--2nd pass.-
        Else '--no records..-
            MsgBox("No parts defined for this quote..", MsgBoxStyle.Exclamation)
            '--FrameBuild.Enabled = False
            '--cmdLookup.Enabled = True
            Exit Function
        End If '--empty.-
        '--End If  '--chassis--
        mlNoJobs = mlQuoteChassisQty '--total of m/boards in quote..-
        If (mlNoJobs <= 0) Or chkBuildOneJobAnyway.Enabled Then '--can't do it..-
            If chkBuildOneJobAnyway.Enabled Then  '--been here..-
                If chkBuildOneJobAnyway.Checked Then
                    mlNoJobs = 1
                    Call mbSetUpQuoteJobs(colQuoteLines, mlNoJobs)
                    mbLoadQuoteInfo = True  '-- can go..
                End If
            Else  '-first time.-
                cmdQuoteOK.Enabled = False
                mbLoadQuoteInfo = False
                LabStatus.Text = "Can't build.."
                LabStatus.BackColor = System.Drawing.Color.Red

                chkBuildOneJobAnyway.Enabled = True
                '-- just WAIT for CHECK to check..-
            End If '--  enabled-

        Else '--ok--
            '--  NOW we know how many jobs there will be..
            chkBuildOneJobAnyway.Font = New Font("Tahoma", 8, FontStyle.Regular)
            Call mbSetUpQuoteJobs(colQuoteLines, mlNoJobs)
            mbLoadQuoteInfo = True
        End If '--no of jobs..-
        '-- Check for Jobs already created.--
        If mbGetQuoteJobsList(mlOrderId, colResult, msFullJobList, sWhereList) Then '--has jobs..-
            LabStatus.Text = "Has Jobs: " & msFullJobList
            LabStatus.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, &HB6S, &HC1S)) '--light pink..
            System.Windows.Forms.Application.DoEvents()
        End If
    End Function '--load quote..-
    '= = = = = = =  =
    '-===FF->

    '-- A M E N D I N G - reload Existing Jobs for selected Quote ==
    '--  A M E N D I N G - reload Existing Jobs for selected Quote ==

    Private Function mbReloadQuoteJobs(ByRef colJobIds As Collection) As Boolean
        Dim v1 As Object
        Dim sSql As String
        Dim ix, lngJobId As Integer
        Dim lngCount, lngJobSequenceNo As Integer
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim colItem As Collection
        Dim colField As Collection
        Dim colJobParts As Collection
        Dim ColJobFields As Collection
        Dim curCost As Decimal
        Dim curTotalParts As Decimal
        Dim sCat1, sCat2 As String
        Dim sDescr, sLine As String
        Dim sStockId As String
        Dim sBarcode As String
        Dim sShowCost As String
        Dim sOrderQty As String

        mbReloadQuoteJobs = False
        If (colJobIds.Count() < 1) Then
            MsgBox("No Jobs found..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        lngJobSequenceNo = 0  '--is 1-based..--
        lngCount = 0 '--all items..-
        Call mbLoadQuoteHeaderInfo() '--load hdr stuff.-
        mColQuoteItems = New Collection '--  build static collection all items..--
        mlNoJobs = colJobIds.Count()
        '-- set up stuff..--
        LabPoolInfo.Text = ""
        ListViewQuote.Items.Clear()
        ListViewQuote.Columns.Clear()
        ListViewQuote.Enabled = False
        '-- Initialise..--

        Erase masNominTech
        Erase masComments
        Erase malJobIds
        '== 'UPGRADE_WARNING: Lower bound of array masNominTech was changed from 1 to 0. Click for more: 
        '== 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
        ReDim masNominTech(mlNoJobs - 1)
        ReDim masComments(mlNoJobs - 1)
        '== 'UPGRADE_WARNING: Lower bound of array malJobIds was changed from 1 to 0. Click for more: 
        '== 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
        ReDim malJobIds(mlNoJobs - 1)
        For ix = 0 To (mlNoJobs - 1)
            masNominTech(ix) = "" '--init..-
            masComments(ix) = "" '--init..-
            malJobIds(ix) = -1 '--initialise.-
        Next ix

        LabStatus.Text = "Amending Jobs: " & msFullJobList & ".."
        LabStatus.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, &HC0S, &HCBS)) '--pink ---   vbGreen
        cmdQuoteOK.Enabled = False
        cmdCopyComment.Enabled = False
        System.Windows.Forms.Application.DoEvents()
        mColPrintJobParts = New Collection

        '--RE-CREATE list of all Quote PARTS for ALL Jobs in Quote..-
        '--load all parts..--
        For Each v1 In colJobIds
            lngJobSequenceNo = lngJobSequenceNo + 1
            lngJobId = CInt(v1)
            curTotalParts = 0
            '--  get job record..--
            If mbGetJobRecord(lngJobId, ColJobFields) Then
                '--  save Tech name and Comments.--
                masNominTech(lngJobSequenceNo - 1) = ColJobFields.Item("NominatedTech")("value")
                masComments(lngJobSequenceNo - 1) = ColJobFields.Item("Diagnosis")("value")
                If (lngJobSequenceNo = 1) Then '--save date from 1st job..-
                    mDateJobCreated = CDate(ColJobFields.Item("DateCreated")("value"))
                    msJobCreatedBy = ColJobFields.Item("RcvdStaffName")("value")
                End If
            End If
            malJobIds(lngJobSequenceNo - 1) = lngJobId '--save jobnos..-
            colJobParts = New Collection '-Collect print items this Job..--

            '--  get parts this job..--
            sSql = "SELECT * from [QuoteJobParts] WHERE (QuotePart_JobId=" & CStr(lngJobId) & ")"
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
                MsgBox("Failed to get QUOTED JobPARTS recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '--RE-CREATE list of all Quote PARTS this Job..-
            If (Not (rs1 Is Nothing)) And (rs1.Rows.Count > 0) Then
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to collection for job..
                    '-- RECORDSET is from QUOTED JOBPARTS Table..--
                    lngCount = lngCount + 1
                    colItem = New Collection
                    '--field by field.--
                    colField = New Collection
                    colField.Add("Stock_Id", "name")
                    sStockId = Trim(CStr(dataRow1.Item("QuotePart_StockId")))
                    colField.Add(sStockId, "value")
                    colItem.Add(colField, "Stock_Id")
                    '--field by field.--
                    colField = New Collection
                    colField.Add("Barcode", "name")
                    sBarcode = Trim(CStr(dataRow1.Item("QuotePartBarcode")))
                    colField.Add(sBarcode, "value")
                    colItem.Add(colField, "Barcode")
                    '--cost
                    curCost = CDec(dataRow1.Item("QuotePart_Sell_inc"))
                    curTotalParts = curTotalParts + curCost
                    '==sShowCost = Space(10)
                    sShowCost = FormatCurrency(curCost, 2)
                    colField = New Collection
                    colField.Add("Sell_inc", "name")
                    colField.Add(sShowCost, "value")
                    colItem.Add(colField, "Sell_inc")
                    '--field by field.--
                    colField = New Collection
                    colField.Add("Cat1", "name")
                    sCat1 = Trim(CStr(dataRow1.Item("QuotePartCat1")))
                    colField.Add(sCat1, "value")
                    colItem.Add(colField, "Cat1")
                    '--field by field.--
                    colField = New Collection
                    colField.Add("Cat2", "name")
                    sCat2 = Trim(CStr(dataRow1.Item("QuotePartCat2")))
                    colField.Add(sCat2, "value")
                    colItem.Add(colField, "Cat2")
                    '--field by field.--
                    colField = New Collection
                    colField.Add("Description", "name")
                    sDescr = Trim(CStr(dataRow1.Item("QuotePartDescription")))
                    colField.Add(sDescr, "value")
                    colItem.Add(colField, "Description")
                    '==  NOT NEEDED  ==  colItem.Add lngCount, "itemNo"

                    '-- Add this job part to Quote parts collection.--
                    mColQuoteItems.Add(colItem, CStr(lngCount)) '--item no (1-based) is key in main collection.-
                    ReDim Preserve malItemJobNo(lngCount - 1) '-- set up array location (zero-based)  for item/jobno..-
                    '-- WE ALREADY KNOW the JobSeqNo for this item..--
                    malItemJobNo(lngCount - 1) = lngJobSequenceNo
                    '-- set up Print lines parts collection..--
                    '-- build print line for assigned part..--
                    sLine = Space(44)
                    Mid(sLine, 1, 6) = sCat1
                    Mid(sLine, 8, 6) = sCat2
                    Mid(sLine, 15, 15) = sBarcode
                    Mid(sLine, 32, 8) = "[" & sStockId & "]"
                    colJobParts.Add(sLine & vbCrLf & sDescr)
                Next dataRow1

                '== While (Not rs1.EOF) '---And (cx < 100)
                '==   rs1.MoveNext()
                '== End While '-eof-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '== rs1.Close()
            End If '--rs nothing-
            mColPrintJobParts.Add(colJobParts, CStr(lngJobId)) '--save this JOB parts list for printing.
        Next v1 '--job..-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        LabOrderDetail.Text = LabOrderDetail.Text & "Number of Jobs: " & mlNoJobs & vbCrLf
        LabOrderDetail.Text = LabOrderDetail.Text & "Total no. of Parts: " & lngCount & vbCrLf

        mbReloadQuoteJobs = True

    End Function '--reload
    '= = = = = = = =
    '-===FF->

    '-- NEW !!  Print the Quote Job form.--
    '-- NEW !!  Print the Quote Job form.--

    Private Function mbPrintQuoteJobForm(ByVal lngJobSequence As Integer, _
                                                 ByVal lngJobNo As Integer) As Integer

        Dim prtDocs1 As clsPrintDocs
        Dim lngError As Integer
        '== Dim kx, iPos, ix, lResult As Integer
        '== Dim s1, s2 As String
        Dim sDateText As String
        Dim sDateText2 As String
        Dim sText As String
        '== Dim sFullPath As String
        '== Dim sUsernames As String
        Dim colBusiness As Collection
        Dim colCustomer As Collection
        Dim colJobParts As Collection
        Dim v1 As Object

        On Error GoTo PrintQuoteJob_Error
        prtDocs1 = New clsPrintDocs
        prtDocs1.TicketDate = VB6.Format(Today, "mmm-yy") '--"Sep-09"

        sDateText = "Created By: " '--stay on this line..-
        sDateText2 = ""
        If mbAmending Then
            sDateText = sDateText & msJobCreatedBy & ".  " & VB6.Format(mDateJobCreated, "dd-mmm-yyyy, hh:mm ")
        Else '--creating today...-
            sDateText = sDateText & msStaffName & ".  " & VB6.Format(Today, "dd-mmm-yyyy, ") & VB6.Format(TimeOfDay, "hh:mm")
        End If
        '===Printer.Print msStaffName; ".  "; s1
        If mbAmending Then
            sDateText2 = "Updated By: " '--stay on this line..-
            sDateText2 = sDateText2 & msStaffName & ".  " & _
                               VB6.Format(Today, "dd-mmm-yyyy, ") & VB6.Format(TimeOfDay, "hh:mm")
        End If

        prtDocs1.PrtSelectedPrinterName = msColourPrtName  '== mPrtColour

        prtDocs1.Version = LabVersion.Text
        prtDocs1.UserLogo = Me.Picture1.Image
        prtDocs1.JobNo = lngJobNo
        prtDocs1.QuoteOrderId = mlOrderId

        '==   sHdr1 = "(#" & lngJobSequence & " of " & mlNoJobs & ")"
        prtDocs1.QuoteJobSequence = lngJobSequence
        prtDocs1.QuoteNumberOfJobs = mlNoJobs

        prtDocs1.HeaderDate = sDateText '== s2 & msStaffName & Space(12) & s1
        prtDocs1.HeaderDate2 = sDateText2 '--if updated--

        prtDocs1.PriorityColour = RGB(255, 255, 0) '--orange.- '= mlPriorityColour

        '--load business info..-
        colBusiness = New Collection
        colBusiness.Add(msBusinessName, "BusinessName")
        colBusiness.Add(msBusinessShortName, "BusinessShortName")
        colBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        colBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        colBusiness.Add("", "BusinessState")
        colBusiness.Add("", "BusinessPostcode")
        prtDocs1.Business = colBusiness

        '--load cust info..-
        colCustomer = New Collection
        colCustomer.Add(msCustomerBarcode, "CustomerBarcode")
        colCustomer.Add(msCustomerName, "CustomerName")
        colCustomer.Add(msCustomerCompany, "CustomerCompany")
        colCustomer.Add(msCustomerPhone, "CustomerPhone")
        colCustomer.Add(msCustomerMobile, "CustomerMobile")
        colCustomer.Add("", "CustomerPriorityText")
        colCustomer.Add(masNominTech(lngJobSequence - 1), "CustomerTechName")
        prtDocs1.Customer = colCustomer

        '--  model checklist..-
        sText = vbCrLf & "Note: Model Checklists specifically for Quote-Jobs" & vbCrLf & _
                            " have been discontinued in Version 3 of Jobmatix." & vbCrLf & vbCrLf & _
                           " Service checklists can be set up for any Service Charge item."
        '== For Each v1 In mColModelChecklist
        '== sText = sText & v1 & vbCrLf
        '== Next v1
        prtDocs1.ModelChecklist = sText

        '-- Job Items Required..-
        colJobParts = mColPrintJobParts.Item(CStr(lngJobNo)) '--parts list this job..-
        sText = "<b>Job Items Required" & vbCrLf & vbCrLf
        sText = sText & "<ul>Cat1   Cat2   Barcode       [StockId] " & vbCrLf & vbCrLf
        For Each v1 In colJobParts
            sText = sText & v1 & vbCrLf & vbCrLf
        Next v1

        prtDocs1.JobItemsRequired = sText

        '--  special instructions..--
        prtDocs1.QuoteInstructions = masComments(lngJobSequence - 1)
        prtDocs1.QuoteFullJobList = msFullJobList

        '-- go..--
        mbPrintQuoteJobForm = prtDocs1.PrintQuoteJob

        prtDocs1 = Nothing

        '==Printer.EndDoc
        '-- end print --
        Exit Function

PrintQuoteJob_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in mbPrintQuoteJobForm." & vbCrLf & _
        "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbPrintQuoteJobForm = -lngError

    End Function '--print quote..--
    '= = = = = = = =
    '-===FF->

    '-- l o a d -----
    '-- l o a d -----

    Private Sub frmQuoteJobs_Load(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim ix As Integer
        Dim s1 As String

        '--gbclosingDown = False
        msRMSelect = ""

        LabHdr1.Text = ""
        LabHdr2.Text = ""
        labCustSearch.Text = ""
        cmdQuoteOK.Enabled = False
        '-- save prior to activate..-
        chkBuildOneJobAnyway.Enabled = False

        '==3037= mlOrderId = -1

        mlSortKey = -1 '-- no sort clicked..-
        mlSortOrder = System.Windows.Forms.SortOrder.Ascending '--or  lvwDescending--
        '== LabOrderDetail.Text = ""
        '== LabOrderDetail.Text = vbCrLf & "Looking for quotes for customer:" & vbCrLf & msCustomerSearchName
        LabOrderDetail.Text = vbCrLf & "Buiding quote for customer:" & vbCrLf & msCustomerName

        FrameBuild.Enabled = False
        FrameBuild.Text = ""
        FrameQuote.Text = ""
        mlNoJobs = 0

        msChassisCat1 = ""
        msChassisCat2 = ""
        mlQuoteChassisQty = 0
        mlNoChassisTypes = 0

        mlJobNoBackColour = System.Drawing.ColorTranslator.ToOle(LabJobSeq(0).BackColor) '--save for restoring.--

        For ix = 1 To k_MAXJOBS - 1
            LabJobSeq(ix).Visible = False
        Next ix
        cmdAddAll.Enabled = False
        cmdAdd.Enabled = False
        cmdRemove.Enabled = False
        cmdSave.Enabled = False
        cmdReprint.Enabled = False

        LabPoolInfo.Text = ""
        txtNominTech.Enabled = False
        msPropChars = "|/-\|/-\"
        msPropPos = 1
        Call CenterForm(Me)
        cboChassis.Items.Clear()
        cmdCopyComment.Enabled = False

        LabVersion.Text = "JobMatix- V:" & CStr(My.Application.Info.Version.Major) & "." & _
                       My.Application.Info.Version.Minor & _
                      ". Build:" & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "="
        '== mbAmending = False
        '== mbDataChanged = False
        mDateJobCreated = Today
        msJobCreatedBy = ""
        msFullJobList = ""

         '==  MsgBox("Form_Load completed..", MsgBoxStyle.Information)

        '== 3067.0 ==
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-QuoteJobs.htm")
        End If

    End Sub '--load--
    '= = = = = = = = = =  =

    '--- A c t i v a t e --
    '--- A c t i v a t e --

    '==    3501.0617 --  
    '==       --  frmQuoteJob3:  Move Activated to Shown..
    '==

    Private Sub frmQuoteJobs_Activated(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbStartupDone Then Exit Sub
        mbStartupDone = True

    End Sub  '--activated-
    '= = = = = = = = = = === 

    '-- Shown-

    Private Sub frmQuoteJobs_Shown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        '== Dim lCount, j, i, k, ix As Integer
        Dim lngStart As Integer
        Dim lngCount As Integer
        Dim sMsg As String

        'If mbStartupDone Then Exit Sub
        'mbStartupDone = True

        '==MsgBox("Form_Activated entered..", MsgBoxStyle.Information)
        If (mlOrderId <= 0) Then
            MsgBox("No valid Quote/Order No. selected.", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub
        End If

        '--  settle down the form..
        lngStart = CInt(VB.Timer()) '--starting seconds.-
        While CInt(VB.Timer()) < lngStart + 2
            System.Windows.Forms.Application.DoEvents()
        End While

        Me.Text = k_version
        LabHdr1.Text = "Create Jobs from MYOB/POS Retail Quotation.."
        LabHdr2.Text = vbCrLf
        LabHdr2.Text = LabHdr2.Text & "Inspect the Retail/POS Quote (Sales Order), and provisionally"
        LabHdr2.Text = LabHdr2.Text & " assign Quoted items to as many Jobs as are indicated by the Quote.."
        '--- Call CenterForm(Me)

        '--  check quote selected row info..
        If mColSelectedRow Is Nothing Then
            MsgBox("No Quote/Order info supplied.", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub
        End If

        '== If (mlCustomerSearchId < 0) Then
        If (mlCustomerId < 0) Then
            MsgBox("No customer selected..", MsgBoxStyle.Exclamation)
            Me.Hide()
            Exit Sub
        End If

        '--  show customer info..
        Call mbLoadQuoteHeaderInfo()

        '-Load Model Checklist for printing..-
        '-- GET MODEL list..-
        lngCount = 0
        sMsg = "Note: " & vbCrLf & vbCrLf & _
                      "The JobMatix setup values for 'QuoteChassisCat1' and  'QuoteChassisCat2'" & vbCrLf & _
                       " are incomplete.  These values set the base (foundation) component " & vbCrLf & _
                       " on which a complete system can be built." & vbCrLf & vbCrLf & _
                           "Default values of 'MOTHER' (Cat1) and 'SK' [****] (Cat2) will be used.."
        ToolTip1.SetToolTip(cmdRestoreChassisDefs, _
                      "Restore setup values for 'QuoteChassisCat1' and  'QuoteChassisCat2'")

        If (msChassisCat1 = "") Or (msChassisCat2 = "") Then
            ToolTip1.SetToolTip(cmdRestoreChassisDefs, sMsg)
            msChassisCat1 = "MOTHER"
            msChassisCat2 = "SK"
            MsgBox(sMsg)
        End If '--cat1-
        '--  save original as Default..-
        msDefaultChassisCat1 = msChassisCat1
        msDefaultChassisCat2 = msChassisCat2
        txtChassisCat1.Text = msChassisCat1
        txtChassisCat2.Text = msChassisCat2
        LabSplitMsg.Text = LabSplitMsg.Text & vbCrLf & vbCrLf & "Defaults: " & msChassisCat1 & "/" & msChassisCat2
        '== msRMSelect = "SELECT SalesOrder_id AS Order_id, SalesOrder_date AS Order_date, " + _
        ''==       " customer.surname, customer.given_names, customer.company, customer.barcode AS CustBarcode,  " + _
        ''==             "SalesOrder.Total_inc,SalesOrder.customer_id as CustId, " + _
        ''==              "customer.phone AS CustPhone, customer.mobile AS CustMobile, SalesOrder.Status AS OrderStatus, " + _
        ''==             " SalesOrder.transaction AS Trans " + _
        ''==       " FROM SalesOrder  LEFT JOIN Customer ON (SalesOrder.customer_id=Customer.Customer_id) " + _
        ''==        " WHERE (SalesOrder.transaction = 'QU') "

        '--MsgBox "Lookup Load form completed..", vbInformation
        '-- LabHdr2.Caption = msSelect
        '== rs1 = Nothing
        '--set up chassis combo..-
        cboChassis.Enabled = False

        '--  THIS VERY SLOW  !!!  ..--
        Call mbLoadChassisCombo()

        cboChassis.Enabled = True

        '--load quotes..-
        '== Set mRstQuote = New ADODB.Recordset
        '== If Not mbRefreshQuotes Then  '--failed..
        '==3037= labCustSearch.Text = msCustomerSearchName
        labCustSearch.Text = msCustomerName
        System.Windows.Forms.Application.DoEvents()
        '==3037= LabOrderDetail.Text = vbCrLf & "Looking for quotes for customer:" & vbCrLf & msCustomerSearchName
        LabOrderDetail.Text = vbCrLf & "quote for customer:" & vbCrLf & msCustomerName
        System.Windows.Forms.Application.DoEvents()

        FrameBuild.Visible = False

        '==3037= cmdLookup.Enabled = False
        '==3037= chkAllQuotes.Enabled = True

        cmdAddAll.Visible = False
        cmdAdd.Visible = False
        cmdRemove.Visible = False

        '==3037= ListViewSalesOrders.Focus()
        mbCanBuild = mbLoadQuoteInfo()

        Me.Text = "JobMatix31:  Creating Quote Jobs"

        '--End If  '--startup-
    End Sub '--activate--
    '= = = = = = = = =
    '-===FF->

    '--  form resized..--
    'UPGRADE_WARNING: Event frmQuoteJobs.Resize may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub frmQuoteJobs_Resize(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) _
                                                               Handles MyBase.Resize
        Dim lngHt As Integer

        '==3037= lngHt = VB6.PixelsToTwipsY(ListViewSalesOrders.Height) '--save--
        '--resize results list box..--
        '---ListResults.Move mlResultsLeft, mlResultsTop, (Me.Width - 500), (Me.Height - mlResultsTop - 1300)
        '=== ListViewSalesOrders.Move mlGridLeft, mlGridTop, (Me.Width - 8200), lngHt    '--keep current height..-
        '=== FrameBuild.Width = ListViewSalesOrders.Width
        '====  ListViewJob.Width = FrameBuild.Width - 240
        '===cmdSave.Top = Me.Height - 1000
        '===cmdCancel.Top = cmdSave.Top
        '----txtResults.Top = cmdSave.Top
    End Sub '--resize..-
    '= = = = = = = = = = =

    '-- Chassis Selection clicked..
    '-- Chassis Selection clicked..

    'UPGRADE_WARNING: Event cboChassis.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboChassis_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As System.EventArgs) Handles cboChassis.SelectedIndexChanged
        Dim s1 As String

        With cboChassis
            If .SelectedIndex >= 0 Then '--selected..-
                s1 = VB6.GetItemString(cboChassis, .SelectedIndex)
                msChassisCat1 = Trim(VB.Left(s1, 6))
                msChassisCat2 = Trim(Mid(s1, 7))
            End If '--selected.-
        End With '--chassis..-
        txtChassisCat1.Text = msChassisCat1
        txtChassisCat2.Text = msChassisCat2

        '==3037=  ????  =

        '==3037= ListViewSalesOrders.Focus()
        '==3037= Call listViewSalesOrders_Click(ListViewSalesOrders, New System.EventArgs())

        '==3067.0== Start again..
        Call mbLoadQuoteInfo()

    End Sub '--chassis-
    '= = = = = = = = = = =

    '-- Restore defaults Chassis defs..

    Private Sub cmdRestoreChassisDefs_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles cmdRestoreChassisDefs.Click

        msChassisCat1 = msDefaultChassisCat1
        msChassisCat2 = msDefaultChassisCat2
        '--show--
        txtChassisCat1.Text = msChassisCat1
        txtChassisCat2.Text = msChassisCat2

        cboChassis.SelectedIndex = -1 '--now have default..-

        '==3067.0== Start again..
        Call mbLoadQuoteInfo()

    End Sub '--restore--
    '= = = = = = = = = =
    '-===FF->

    '-- chkBuildOneJobAnyway-

    Private Sub chkBuildOneJobAnyway_CheckedChanged(sender As Object, ev As EventArgs) _
                                                   Handles chkBuildOneJobAnyway.CheckedChanged
        If chkBuildOneJobAnyway.Checked Then
            cmdQuoteOK.Enabled = True
        End If

    End Sub '-build one job-
    '= = = = = = = = = = = = = = = = = =  = 

    '-- OK -- SELECT Quote..--
    '-- Setup selected Quote for building--
    '-- Setup selected Quote for building--

    Private Sub cmdQuoteOk_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdQuoteOK.Click

        Dim ix, lngCount As Integer
        Dim lngNoCols As Integer
        '== Dim lngQty As Integer
        Dim lngaffected As Integer
        '== Dim ans As Short
        Dim sSql As String
        Dim s2, s1, s3 As String
        Dim sErrors As String
        '== Dim v1 As Object
        '==Dim sJobList As String
        Dim sWhereList As String
        Dim ColQuote As Collection
        '== Dim fldx As ADODB.Field
        Dim colResult As Collection
        Dim colResult2 As Collection

        '--ans = MsgBox("Save this new text and exit..?", _
        ''--                        vbYesNo + vbQuestion, "Memo entry")
        s2 = "" : s3 = ""
        txtNoJobs.Text = ""
        '--idx = ListFn.ListIndex
        '--mlNoJobs = 0
        lngCount = 0 '--count and number items..-
        lngNoCols = 0
        LabJobInfo.Text = ""
        mbAmending = False
        '--NB! - check that this quote not already allocated to Jobs NOT Cancelled...--
        If (mlOrderId > 0) Then '--lookup this quote order no in jobQuoteParts table.--
            If mbGetQuoteJobsList(mlOrderId, colResult, msFullJobList, sWhereList) Then '--has jobs..-
                If MsgBox("JobTracking already has NON-CANCELLED JobNo(s):  " & vbCrLf & msFullJobList & vbCrLf & _
                           " with this Quote/Order no." & vbCrLf & vbCrLf & _
                             "Do you want to amend, re-do or re-print this quote?", _
                                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    FrameBuild.Enabled = False
                    Exit Sub '----
                Else '-yes..  amend.-
                    '--reload jobs..--
                    If MsgBox("Note that parts/items can not be re-assigned between jobs.." & vbCrLf & vbCrLf & _
                               "Do you want to CANCEL the jobs: " & vbCrLf & msFullJobList & vbCrLf & _
                                 "  and completely re-do this Quote ?" & vbCrLf & vbCrLf & _
                                 "  (To KEEP and amend/print existing Jobs, Press 'NO'..)", _
                                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        '--cancel the jobs in the list..  FIRST Check statuses.-
                        sSql = "SELECT Jobs.JobStatus FROM [Jobs] WHERE (LEFT(Jobs.JobStatus,2) >'10') AND (" & sWhereList & ");"
                        s1 = "ARE YOU SURE you want to CANCEL the jobs: " & vbCrLf & msFullJobList & vbCrLf & _
                                                                                        "  and completely re-do this Quote ?"
                        If mbGetSelectValue(sSql, colResult2) Then
                            s1 = s1 & vbCrLf & vbCrLf & "NB:  AT LEAST " & colResult2.Count() & " Job(s) ARE ALREADY STARTED !"
                        End If '-GetSelect-
                        If MsgBox(s1 & vbCrLf, _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                            sSql = "UPDATE [Jobs] SET JobStatus= '" & k_statusCancelled & "'"
                            sSql = sSql & ", TechStaffName= '" & msFixSqlStr(msStaffName) & "'"
                            sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
                            sSql = sSql & " WHERE ( " & sWhereList & ");"
                            If MsgBox("OK to EXECUTE the SQL: " & vbCrLf & sSql & vbCrLf, _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                                If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                    MsgBox("Failed to update JOB Statuses.." & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Exclamation)
                                Else '--ok--
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                    MsgBox("OK.. " & lngaffected & " Jobs were cancelled.. " & vbCrLf & _
                                                                    "SQL was:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Information)
                                End If '--execute..-
                            Else '-squibbed..-
                            End If '--yes, execute.-
                            FrameBuild.Enabled = False
                            Exit Sub '----
                        Else '--no..  give up..
                            FrameBuild.Enabled = False
                            Exit Sub
                        End If '-yes- cancel..-
                    Else '-NO- don't cancel-  JUST AMEND..-
                        mbAmending = True
                        If Not mbReloadQuoteJobs(colResult) Then
                            MsgBox("Couldn't reload jobs..", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If '--reload--
                    End If '--cancel..-
                    '===Exit Sub
                End If '--yes..-
            Else '-- no previous..-
                Erase malItemJobNo '--where is each item..-
                If Not mbLoadQuoteInfo() Then
                    FrameBuild.Enabled = False
                    '==3037= cmdLookup.Enabled = True
                    Exit Sub '---
                End If
            End If '--select-
        End If
        '--  ok.--
        txtNoJobs.Text = CStr(mlNoJobs)

        '===ListViewSalesOrders.Height = 1000
        '==3037= ListViewSalesOrders.Enabled = False
        '==3037= cmdLookup.Enabled = True

        cmdQuoteOK.Enabled = False
        cboChassis.Enabled = False
        cmdRestoreChassisDefs.Enabled = False

        cmdAddAll.Visible = True
        cmdAdd.Visible = True
        cmdRemove.Visible = True

        cmdAddAll.Enabled = False
        FrameBuild.Enabled = True
        FrameBuild.Visible = True
        mbCancelled = False
        '--set up listView to show jobs..
        ListViewJob.Items.Clear()
        ListViewJob.Columns.Clear()
        ListViewJob.Columns.Add("", "Cat1", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewJob.Width) \ 8)))
        ListViewJob.Columns.Add("", "Description", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewJob.Width) \ 3)))
        ListViewJob.Columns.Add("", "barcode", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewJob.Width) \ 6)))
        ListViewJob.Columns.Add("", "Sell_inc", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewJob.Width) \ 6)))

        '-- load parts list for quote..-
        '=== Call mbLoadPartsListView(mRsItems, ListViewQuote)
        If mbAmending Then
            For ix = 0 To mlNoJobs - 1
                LabJobSeq(ix).Visible = True
                LabJobSeq(ix).Enabled = True
            Next ix
            cmdReprint.Enabled = True
            cmdSave.Text = "Save (Updates)"
            Call LabJobSeq_Click(LabJobSeq.Item(0), New System.EventArgs()) '--show first job..--
        Else '--new deal..-
            cmdReprint.Enabled = False
            cmdSave.Text = "Save (Create)"
            If (mlNoChassisTypes <= 0) Then '--can't tell how to allocate systems..--
                MsgBox("Unable to identify the basic chassis component (ie. cat1='" & msChassisCat1 & "') in this quote.." & _
                                         vbCrLf & "Allocation of parts to jobs must be done manually.", MsgBoxStyle.Exclamation)
                '== Exit Sub
            ElseIf (mlNoChassisTypes > 1) Then  '--can't tell how to allocate systems..--
                MsgBox("Please note:" & vbCrLf & "This quote includes mixed system types.." & vbCrLf & _
                      "Auto-Allocation of parts to jobs will involve some arbitrary choices," & vbCrLf & _
                                   " and the result may need to be adjusted manually.", MsgBoxStyle.Information)
            End If '--Else
            '--If mlNoJobs = 0 Then mlNoJobs = 1
            cmdAddAll.Enabled = True
            If (mlNoJobs > 0) Then '--show allowable jobs..-
                For ix = 0 To mlNoJobs - 1
                    LabJobSeq(ix).Visible = True
                    LabJobSeq(ix).Enabled = True
                    'UPGRADE_WARNING: Lower bound of array masNominTech was changed from 1 to 0. Click for more: 
                    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                    ReDim Preserve masNominTech(ix)
                    masNominTech(ix) = ""
                    'UPGRADE_WARNING: Lower bound of array masComments was changed from 1 to 0. Click for more: 
                    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                    ReDim Preserve masComments(ix)
                    masComments(ix) = ""
                Next ix
            End If '--jobs..-

            Call mbShowQuoteParts() '--should be Full..-
            '-- allocate all parts to "mlNoJobs" jobs..-
            LabStatus.Text = " Building.."
            LabStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0F0F0) '--vbYellow
            cmdAddAll.Enabled = True
            chkBuildOneJobAnyway.Enabled = False
            cmdQuoteOK.Enabled = False

        End If '--amending..-
        colResult = Nothing
        ColQuote = Nothing

    End Sub '--ok--
    '= = = = = = = = = = =
    '-===FF->

    '-- Select job to view..-
    '-- Select job to view..-

    Private Sub LabJobSeq_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles LabJobSeq.Click
        Dim index As Short = LabJobSeq.GetIndex(eventSender)

        Call mbShowJobParts(index + 1)

    End Sub '--jobseq..-
    '= = = = = = = = = = = =
    '-===FF->

    '-- tech name--
    '-- tech name--
    'UPGRADE_WARNING: Event txtNominTech.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtNominTech_TextChanged(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles txtNominTech.TextChanged

        '==mbDataChanged = True

    End Sub '--nomin tech.--
    '= = = = = = = = = = = = =

    '-- nominated tech/staff--
    '-- nominated tech/staff--
    Private Sub txtNominTech_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles txtNominTech.Enter

        txtNominTech.SelectionStart = 0
        txtNominTech.SelectionLength = Len(txtNominTech.Text)
    End Sub '--gotfocus.-
    '= = = = = = = = = = =

    '-- PreviewKeyDown is where you preview the key.
    '-- Do not put any logic here, instead use the
    '-- KeyDown event after setting IsInputKey to true.

    Private Sub txtNomTechBarcode_PreviewKeyDown(ByVal sender As Object, _
                                          ByVal e As PreviewKeyDownEventArgs) _
                                                Handles txtNomTechBarcode.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Escape, Keys.Return
                e.IsInputKey = True
                If gbDebug Then MsgBox("Key is: " & Asc(e.KeyCode))
        End Select
    End Sub  '--PreviewKeyDown-
    '= = = = = = = = =  = = = = =  == 

    '--Enter..-
    Private Sub txtNomTechBarcode_KeyPress(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                            Handles txtNomTechBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim colFields As Collection
        Dim bOk As Boolean

        bOk = True
        If keyAscii = 13 Then '--enter-
            If mlCurrentJobNo > 0 Then
                s1 = Trim(txtNomTechBarcode.Text)
                If (s1 <> "") Then '--empty is ok..--
                    '==If IsNumeric(s1) Then  '--number must be id..-
                    '==If mbLookupStaff(s1, colFields) Then    '--ok-
                    If mRetailHost1.staffGetStaffRecord(s1, -1, colFields) Then '--found..--
                        txtNominTech.Text = colFields.Item("docket_name")("value")
                        masNominTech(mlCurrentJobNo - 1) = Trim(txtNominTech.Text)
                        If mbAmending Then
                            mbDataChanged = True
                            cmdSave.Enabled = True
                        End If
                    Else
                        bOk = False
                        MsgBox("Invalid staff-id..", MsgBoxStyle.Exclamation)
                    End If '--lookup.-
                    '==Else  '--alpha..  accept anything..-
                    '==       masNominTech(mlCurrentJobNo) = Trim(txtNominTech.Text)
                    '==End If  '--numeric.-
                End If '--empty-
                '====If bOk Then cmdCheckGoods.SetFocus   '-cmdfinish.SetFocus
            End If '--jobno.-
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--enter-
    '= = = =  = = =

    Private Sub txtNomTechBarcode_Validating(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                             Handles txtNomTechBarcode.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String
        Dim colFields As Collection

        s1 = Trim(txtNomTechBarcode.Text)
        If (s1 <> "") Then '--empty is ok..--
            '==If IsNumeric(s1) Then  '--number must be id..-
            '==If mbLookupStaff(CLng(s1), colFields) Then    '--ok-
            If mRetailHost1.staffGetStaffRecord(s1, -1, colFields) Then '--found..--
                txtNominTech.Text = colFields.Item("docket_name")("value")
                masNominTech(mlCurrentJobNo - 1) = Trim(txtNominTech.Text)
            Else
                MsgBox("Invalid staff-id..", MsgBoxStyle.Exclamation)
                keepfocus = True
            End If
            '==End If  '--numeric.-
        End If '--empty-

        eventArgs.Cancel = keepfocus
    End Sub '--tech..--
    '= = = = = = = = = =  =
    '-===FF->

    '-- Job comments--
    '-- Job comments--

    Private Sub txtComments_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles txtComments.Enter

        If mlCurrentJobNo > 0 Then
            txtComments.Text = masComments(mlCurrentJobNo - 1)
            If (mlCurrentJobNo = 1) Then
                cmdCopyComment.Enabled = True
            Else
                cmdCopyComment.Enabled = False
            End If
        End If
    End Sub '--nomin tech.--
    '= = = = = = = = = = = = =

    '-- Job comments--

    'UPGRADE_WARNING: Event txtComments.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtComments_TextChanged(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles txtComments.TextChanged

        If (mlCurrentJobNo > 0) Then
            masComments(mlCurrentJobNo - 1) = Trim(txtComments.Text)
            If mbAmending And txtComments.Enabled Then
                mbDataChanged = True
                cmdSave.Enabled = True
            End If
        End If
    End Sub '--nomin tech.--
    '= = = = = = = = = = = = =

    Private Sub txtComments_Leave(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles txtComments.Leave
        '=====cmdCopyComment.Enabled = False
    End Sub
    '= = = ======

    '--  copy comment from Job-1 to all Jobs..--

    Private Sub cmdCopyComment_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles cmdCopyComment.Click
        Dim ix As Integer

        If (mlCurrentJobNo = 1) And (mlNoJobs > 1) Then
            masComments(0) = Trim(txtComments.Text)
            For ix = 1 To (mlNoJobs - 1)
                masComments(ix) = masComments(0)
            Next ix
        End If
    End Sub '--copy..--
    '= = = = = =  = = = =
    '-===FF->

    '--Moving parts back and forth.-
    '--Moving parts back and forth.-
    '--Moving parts back and forth.-

    '-- select job part --
    '-- select job part --

    'UPGRADE_ISSUE: MSComctlLib.ListView event listViewJob.itemClick was not upgraded. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
    '== Private Sub listViewJob_itemClick(ByVal item1 As System.Windows.Forms.ListViewItem)

    Private Sub listViewJob_Click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles ListViewJob.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        item1 = ListViewJob.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            If Not mbAmending Then cmdRemove.Enabled = True
            cmdAdd.Enabled = False
            cmdAddAll.Enabled = False
        End If  '--nothing.-
    End Sub '--listViewJobs_itemClick(--
    '= = = = = = = = = = = = = = =

    '--  select quote part..-
    '--  select quote part..-
    'UPGRADE_ISSUE: MSComctlLib.ListView event listViewQuote.itemClick was not upgraded. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
    '== Private Sub listViewQuote_itemClick(ByVal item1 As System.Windows.Forms.ListViewItem)

    Private Sub listViewQuote_Click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles ListViewQuote.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        item1 = ListViewQuote.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            cmdRemove.Enabled = False
            cmdAdd.Enabled = True
            cmdAddAll.Enabled = False
        End If
    End Sub '--listViewQuote_itemClick(--
    '= = = = = = = = = = = = = = =

    '--  Move from Left to Right.-
    '----  ie Allocate part from POOL into current job..-
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim iPos, idx, itemNo As Integer
        Dim s1 As String

        If (mlCurrentJobNo > 0) And (ListViewQuote.Items.Count > 0) Then '--must have src and a destination..-
            For idx = 0 To (ListViewQuote.Items.Count - 1)
                'UPGRADE_WARNING: Lower bound of collection ListViewQuote.ListItems has changed from 1 to 0. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                item1 = ListViewQuote.Items.Item(idx)
                If item1.Selected Then '--ok.-
                    s1 = item1.Tag '--itemNo/stockId
                    iPos = InStr(s1, "/")
                    If iPos > 1 Then
                        itemNo = CInt(VB.Left(s1, iPos - 1))
                        If (itemNo >= 0) And (itemNo <= UBound(malItemJobNo)) Then
                            malItemJobNo(itemNo) = mlCurrentJobNo '--allocate to current job.-
                        End If '--item.-
                    End If '--iPos..-
                End If '--selected..-
            Next idx '--item..-
            Call mbShowJobParts(mlCurrentJobNo) '--rebuild this job view..  plus item1-
            '-- rebuild pool view.-
            Call mbShowQuoteParts()
            cmdAdd.Enabled = False
            If (ListViewQuote.Items.Count <= 0) Then '--pool empty..-
                cmdSave.Enabled = True
            End If
        ElseIf (mlCurrentJobNo <= 0) Then  '--must a destination..-
            MsgBox("No Job Sequence No. is selected!", MsgBoxStyle.Exclamation)
        End If '--current job..-
    End Sub '--add--
    '= = = = = = = = = = = =

    '--Move from right to left..-
    '----  Take from Job and put back into pool..-

    Private Sub cmdRemove_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdRemove.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim idx, itemNo As Integer

        If (mlCurrentJobNo > 0) And (ListViewJob.Items.Count > 0) Then '--just checking..-
            For idx = 0 To (ListViewJob.Items.Count - 1)
                'UPGRADE_WARNING: Lower bound of collection ListViewJob.ListItems has changed from 1 to 0. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                item1 = ListViewJob.Items.Item(idx)
                If item1.Selected Then '--ok.-
                    itemNo = CInt(item1.Tag)
                    If (itemNo >= 0) And (itemNo <= UBound(malItemJobNo)) Then
                        malItemJobNo(itemNo) = 0 '--free-- ie., back in pool..-
                    End If
                End If '--selected..-
            Next idx '--item..-
            Call mbShowJobParts(mlCurrentJobNo) '--rebuild this job view..  EX item1-
            '-- rebuild pool view.-
            Call mbShowQuoteParts()
            If (ListViewJob.Items.Count > 0) Then
                cmdRemove.Enabled = True
            Else
                cmdRemove.Enabled = False
            End If
            cmdSave.Enabled = False
        End If '--current job..-
    End Sub '--add--
    '= = = = = = = = = = = =
    '-===FF->

    '--  allocate all parts..-
    '--  allocate all parts..-

    Private Sub cmdAddAll_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdAddAll.Click
        Dim s1 As String
        Dim sStockId As String
        Dim iPos As Integer
        Dim mx, ix, vx As Integer
        '== Dim colItem As Collection
        Dim colJobItems As Collection
        Dim v1 As Object
        '== Dim col1 As Collection
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngQtyReq, lngStockId, lngQtyCount As Integer
        Dim lngResult, lngJobNo As Integer

        If mlNoJobs <= 0 Then Exit Sub
        For ix = 0 To mlNoJobs - 1
            LabJobSeq(ix).BackColor = System.Drawing.ColorTranslator.FromOle(mlJobNoBackColour) '--all grey..-
            LabJobSeq(ix).Visible = True
            LabJobSeq(ix).Enabled = True
        Next ix
        '--  allocate stuff to each job in turn.-
        For lngJobNo = 1 To mlNoJobs
            colJobItems = New Collection '--collect itemnos.--
            '-- FIRST find a Chassis part for this Job..-
            '--   Browse remaining Quote parts list, and grab First item that is a Chassis stock item..-
            lngResult = 0 '--means fail..-
            For vx = 0 To (ListViewQuote.Items.Count - 1) '--srch for 1 item..-
                item1 = ListViewQuote.Items.Item(vx)
                s1 = item1.Tag
                iPos = InStr(s1, "/") '--separates itemno/stockid..-
                If iPos > 0 Then
                    sStockId = "/" & Trim(Mid(s1, iPos + 1)) & "/"
                    If InStr(msChassisStockIds, sStockId) > 0 Then '--found a chassis..-
                        '============== If CLng(Mid(s1, iPos + 1)) = lngStockId Then '--found one..--
                        lngResult = 1 '--found-
                        colJobItems.Add(VB.Left(s1, iPos - 1)) '--collect this Chassis item-no. for this Job..-
                        ListViewQuote.Items.RemoveAt((vx)) '-- gone from pool.-
                        Exit For '-- vx.. start new sweep (loop) for more of this part if needed..-
                    End If
                End If '--ipos-
            Next vx
            '-- NOW, Using model of required sub-parts for 1 system,  ---
            '-- browse remaining Quote parts list, and grab (OrderQty \ noJobs) instances of ..-
            '-----  of each different part..-
            For mx = 0 To (mlMaxModelParts - 1) '--each required stock no..-
                lngStockId = malModelStockQty(0, mx)
                lngQtyReq = malModelStockQty(1, mx)
                lngQtyCount = 0
                '--  find lngQtyReq stock items in lvw and grab them..==
                Do  '-- restart srch for each instance..-
                    lngResult = 0 '--means fail..-
                    For vx = 0 To (ListViewQuote.Items.Count - 1) '--srch for 1 item..-
                        item1 = ListViewQuote.Items.Item(vx)
                        s1 = item1.Tag
                        iPos = InStr(s1, "/") '--separates itemno/stockid..-
                        If iPos > 0 Then
                            If CInt(Mid(s1, iPos + 1)) = lngStockId Then '--found one..--
                                lngResult = 1 '--found-
                                colJobItems.Add(VB.Left(s1, iPos - 1)) '--collect itemnos.-
                                lngQtyCount = lngQtyCount + 1
                                ListViewQuote.Items.RemoveAt((vx))
                                Exit For '-- vx.. start new sweep (loop) for more of this part if needed..-
                            End If
                        End If
                    Next vx
                Loop Until (lngQtyCount >= lngQtyReq) Or (lngResult = 0) '--until we have enough or we don't find any..-
            Next mx '--required model item..-
            '--  update item list with these parts..-

            '--MsgBox "JobNo: " & mlCurrentJobNo & "..  Found: " & colJobItems.Count & " parts instances..", vbInformation
            '--  assign these parts to current job..--
            If colJobItems.Count() > 0 Then
                For Each v1 In colJobItems
                    If (CInt(v1) >= 0) And (CInt(v1) <= UBound(malItemJobNo)) Then
                        malItemJobNo(CInt(v1)) = lngJobNo '--this part to current job..-
                    End If
                Next v1
            End If '--count.-
        Next lngJobNo '--jobno..-
        mlCurrentJobNo = 1
        '-- ok... show first job..-
        cmdAddAll.Enabled = False
        Call mbShowJobParts(1)
        Call mbShowQuoteParts() '--should be empty..-
        If (ListViewQuote.Items.Count <= 0) Then '--pool empty..-
            cmdSave.Enabled = True
        End If
    End Sub '--add all--
    '= = = = = = = = =
    '= = = = = =  = = = =  =
    '-===FF->

    '--Print all jobs..--
    '--Print all jobs..--

    Private Sub cmdReprint_Click(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cmdReprint.Click
        Dim jx, lngJobNo As Integer

        If (mlNoJobs > 0) Then
            '== Printer = mPrtColour '-- set main printer--
            For jx = 1 To mlNoJobs    '--sequence no.-
                lngJobNo = malJobIds(jx - 1)
                Call mbPrintQuoteJobForm(jx, lngJobNo)
                '== If (jx < mlNoJobs) Then Printer.NewPage()
            Next jx
            '== Printer.EndDoc()
            cmdReprint.Text = "Re-print"
            MsgBox("Check printer for completion.. " & vbCrLf & _
                        mlNoJobs & " Job Forms were sent to: " & vbCrLf & vbCrLf & _
                                                    msColourPrtName & "..", MsgBoxStyle.Information)
        End If '--no of jobs..-
    End Sub '--print--
    '= = = = = =  = = = =  =
    '-===FF->

    '-- Save..--  Create all jobs.. and Exit--
    '-- Save..--  Create all jobs..--

    Private Sub cmdSave_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        Dim s1 As String
        Dim sSql As String
        Dim sErrorMsg As String
        Dim sJobsCreated As String
        Dim jx, ix, lngaffected As Integer
        Dim lngTotalParts As Integer
        Dim lngCurrentJobNo As Integer
        Dim bCancelled As Boolean
        Dim rs1 As DataTable '= ADODB.Recordset
        Dim itemNo, lngCount As Integer
        Dim colItem As Collection
        '-- item flds..-
        Dim sStockId, sBarcode As String
        Dim sCat1, sCat2 As String
        Dim sDescription As String
        Dim sLine As String
        Dim colJobParts As Collection
        Dim transaction1 As OleDbTransaction

        lngTotalParts = 0
        sJobsCreated = ""
        If mbAmending Then '--Updating..--
            cmdSave.Enabled = False
            cmdCancel.Enabled = False
            If mbDataChanged Then '--update tech/comments..--
                sSql = ""
                For jx = 0 To (mlNoJobs - 1) '--sequence nos..--
                    lngCurrentJobNo = malJobIds(jx)
                    sSql = sSql & " UPDATE [Jobs] SET "
                    sSql = sSql & "   NominatedTech= '" & msFixSqlStr(masNominTech(jx)) & "' "
                    sSql = sSql & ", diagnosis= '" & msFixSqlStr(masComments(jx)) & "' "
                    sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
                    sSql = sSql & " WHERE (job_id=" & CStr(lngCurrentJobNo) & ") "
                Next jx
                '--  mass update as transaction..-
                '== mCnnJobs.BeginTrans()
                transaction1 = mCnnJobs.BeginTransaction
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrorMsg) Then
                    '== mCnnJobs.RollbackTrans()
                    transaction1.Rollback()
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("Failed to update DB Quote JOB details.." & vbCrLf & sErrorMsg & vbCrLf, MsgBoxStyle.Critical)
                Else '--update ok--
                    '== mCnnJobs.CommitTrans()
                    transaction1.Commit()
                    '===If gbDebug Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    s1 = "OK.. " & mlNoJobs & "  Job Records were updated.. " & vbCrLf
                    If gbDebug Then s1 = s1 & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf
                    MsgBox(s1, MsgBoxStyle.Information)
                End If '--update..-
                cmdCancel.Text = "Exit"
                VB6.SetCancel(cmdCancel, False)
                cmdCancel.Enabled = True
                mbDataChanged = False
                Exit Sub
            End If '--data changed.-
        ElseIf (mlNoJobs > 0) Then  '--new quote jobs to save..-
            cmdCancel.Enabled = False
            cmdSave.Enabled = False
            mColPrintJobParts = New Collection
            '== mCnnJobs.BeginTrans()
            transaction1 = mCnnJobs.BeginTransaction
            '-- INSERT Job, and Insert parts..-
            bCancelled = False
            jx = 0
            While (jx <= (mlNoJobs - 1)) And (Not bCancelled)
                '== jx = jx + 1
                '--  10 fields..--
                sSql = "INSERT INTO Jobs " & _
                     " (CustomerBarcode, RMCustomer_Id, CustomerCompany, CustomerName, CustomerPhone,CustomerMobile,Priority, " & _
                     "NominatedTech, RcvdStaffName, RcvdRMStaff_Id, Diagnosis ) " & _
                     " VALUES  ('" & msCustomerBarcode & "', " & CStr(mlCustomerId) & ", " & _
                               "'" & msFixSqlStr(msCustomerCompany) & "', " & "'" & msFixSqlStr(msCustomerName) & "', " & _
                               "'" & msFixSqlStr(msCustomerPhone) & "', '" & msFixSqlStr(msCustomerMobile) & "', 'Q', " & _
                               "'" & msFixSqlStr(masNominTech(jx)) & "', " & "'" & msFixSqlStr(msStaffName) & "', " & _
                                                            CStr(mlStaffId) & ", " & " '" & msFixSqlStr(masComments(jx)) & "' )"
                '==3107.611=  If gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrorMsg) Then '--ok
                If mbExecuteSql(mCnnJobs, sSql, True, transaction1, sErrorMsg) Then '--ok
                    '--get last IDENTITY (allocated order_id)..--
                    sSql = "SELECT CAST(IDENT_CURRENT ('dbo.jobs') AS int)"
                    '==3107.611=  If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
                    If Not gbGetSqlScalarIntegerValue_Trans(mCnnJobs, sSql, True, transaction1, lngCurrentJobNo) Then
                        '--MsgBox sErrorMsg, vbCritical
                        '--mCnnJobs.RollbackTrans
                        MsgBox("Failed to retrieve ID id of new job record.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                        bCancelled = True
                        '--Response.End
                    Else '--ok-- Have it now..   WAS- SHOULD be on 1st row...
                        '==If (Not (rs1 Is Nothing)) Then  '= (rs1.State = ADODB.ObjectStateEnum.adStateOpen) Then
                        '= If (rs1.Rows.Count > 0) Then '= Not (rs1.BOF And rs1.EOF) Then '--not empty--
                        '--must be on 1st row..--get number-
                        '== Dim dataRow1 As DataRow = rs1.Rows(0)  '--1sr row-
                        '== lngCurrentJobNo = CInt(dataRow1.Item(0)) '--get 1st value--
                        malJobIds(jx) = lngCurrentJobNo '--save jobnos..-
                        '-- INSERT all parts for this job..-
                        sSql = ""
                        lngCount = 0
                        sJobsCreated = sJobsCreated & lngCurrentJobNo & "; "
                        '--browse all items and pick out those assigned to this job.-
                        colJobParts = New Collection
                        For itemNo = 0 To UBound(malItemJobNo)
                            If (malItemJobNo(itemNo) = jx + 1) Then '--SeqNo fits. belongs to current job....-
                                colItem = mColQuoteItems.Item(CStr(itemNo + 1))  '--KEY is 1-based index..
                                sStockId = CStr(colItem.Item("stock_id")("value"))
                                sBarcode = colItem.Item("barcode")("value")
                                sCat1 = CStr(colItem.Item("cat1")("value"))
                                sCat2 = CStr(colItem.Item("cat2")("value"))
                                sDescription = CStr(colItem.Item("description")("value"))
                                sSql = sSql & "INSERT INTO [QuoteJobParts] "
                                sSql = sSql & " (QuotePart_JobId, QuotePart_OrderId, QuotePart_StockId, QuotePartDescription, "
                                sSql = sSql & "  QuotePartBarcode, QuotePartCat1, QuotePartCat2, QuotePart_Sell_inc, "
                                sSql = sSql & "   QuotePart_OrderQty ) "
                                sSql = sSql & " VALUES ( " & CStr(lngCurrentJobNo) & ", " & CStr(mlOrderId) & ", "
                                sSql = sSql & sStockId & ", "
                                sSql = sSql & "'" & msFixSqlStr(sDescription) & "', "
                                sSql = sSql & "'" & sBarcode & "', "
                                sSql = sSql & "'" & msFixSqlStr(sCat1) & "', "
                                sSql = sSql & "'" & msFixSqlStr(sCat2) & "', "
                                sSql = sSql & VB6.Format(colItem.Item("sell_inc")("value"), "######0.00") & ", "
                                sSql = sSql & CStr(colItem.Item("OrderQty")("value")) & "); " & vbCrLf
                                lngCount = lngCount + 1
                                '-- build print line for assigned part..--
                                sLine = Space(44)
                                Mid(sLine, 1, 6) = sCat1
                                Mid(sLine, 8, 6) = sCat2
                                Mid(sLine, 15, 15) = sBarcode
                                Mid(sLine, 32, 8) = "[" & sStockId & "]"
                                colJobParts.Add(sLine & vbCrLf & sDescription)
                            End If '--belongs..-
                        Next itemNo
                        If lngCount > 0 Then '--have items for this job..-
                            '--  ok, do mass insert..--
                            '==3107.611=   If gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrorMsg) Then '--ok
                            If mbExecuteSql(mCnnJobs, sSql, True, transaction1, sErrorMsg) Then '--ok
                                lngTotalParts = lngTotalParts + lngCount
                                mColPrintJobParts.Add(colJobParts, CStr(lngCurrentJobNo)) '--save this parts list for printing.
                            Else '-insert failed--
                                MsgBox("Failed to insert quote job PARTS records.." & vbCrLf & _
                                                                              sErrorMsg, MsgBoxStyle.Critical)
                                bCancelled = True
                            End If '-- PARTS execute--
                        Else '--no parts..-
                            MsgBox("Allocation error.. Job Seq." & jx & " Has no PARTS allocated.." & vbCrLf & _
                                       "Jobs Created for this quote will be backed out..", MsgBoxStyle.Exclamation)
                            bCancelled = True '--rollback.-
                        End If '--count..-
                        '= End If '- IDENT empty--
                        '-mCnnJobs.CommitTrans
                        '= rs1.Close()
                        '==End If '--open-
                    End If '--get Scalar  =rst--
                Else '-insert failed--
                    '--mCnnJobs.RollbackTrans
                    MsgBox("Failed to insert new job record.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                    bCancelled = True
                End If '--execute--
                jx = jx + 1
            End While '-- jx..  all jobs..-
            '-- Done all jobs..  now commit..-
            If Not bCancelled Then
                '== mCnnJobs.CommitTrans()
                transaction1.Commit()
                MsgBox("OK..  Inserted " & lngTotalParts & " parts for " & mlNoJobs & _
                             " Jobs.." & "Created Job Nos are: " & sJobsCreated, MsgBoxStyle.Information)
                '-- Now print a form for each job..-
                cmdCancel.Text = "Exit"
                VB6.SetCancel(cmdCancel, False)
                cmdCancel.Enabled = True
                '-- build job list..-
                msFullJobList = ""
                For jx = 0 To (mlNoJobs - 1)
                    If (msFullJobList <> "") Then msFullJobList = msFullJobList & ", "
                    msFullJobList = msFullJobList & malJobIds(jx)
                Next jx
                msFullJobList = msFullJobList & "."
                Call cmdReprint_Click(cmdReprint, New System.EventArgs())
                cmdReprint.Enabled = True
            Else
                '= mCnnJobs.RollbackTrans()
                transaction1.Rollback()
                MsgBox("Operations for this quote have been backed out..", MsgBoxStyle.Exclamation)
            End If
        End If '-some jobs-
        rs1 = Nothing
        '==mRstQuote.Close
        '==Set mRstQuote = Nothing
        '==Me.Hide
    End Sub '-save.-
    '= = = = = =  = = = =  =
    '-===FF->

    '-- Cancel.-

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dim ans As Short
        '--ask if want to cancel--
        ans = MsgBoxResult.Yes
        If mbAmending Then
            If mbDataChanged Then
                ans = MsgBox("Abandon changes ?", _
                           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question)
            End If
        ElseIf FrameBuild.Enabled And (LCase(cmdCancel.Text) = "cancel") Then
            ans = MsgBox("Abandon this quote/build..?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question)
            If ans = MsgBoxResult.Yes Then
                '--set cancel flag--
                mbCancelled = True
            End If
        End If '-enabled..-

        If ans = MsgBoxResult.Yes Then
            '--set cancel flag--
            '==mbCancelled = True
            '--mbStartupDone = False
            mbStartupDone = False
            FrameBuild.Enabled = False    '--get through query unload..-
            '== mRstQuote.Close
            '== Set mRstQuote = Nothing
            mbClosingDown = True
            Me.Hide()
        End If '--yes..-
    End Sub '--cancel--
    '= = = = = =  ==  = =  =

    '= = = u n l o a d = = = = = = =
    Private Sub frmQuoteJobs_FormClosed(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '--MsgBox "frmMemo UNload event..'"  '-debug--
        '--If Not mbClosingDown Then
        '--     MsgBox "Please use OK/Cancel buttons to exit form..", vbInformation, "Lookup.."
        '--     intCancel = 1  '--cant close yet--
        '--End If
    End Sub '--unload--
    '= = = = = = =

    '--uses QueryUnload--
    Private Sub frmQuoteJobs_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                  Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim ans As Short

        'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                                           System.Windows.Forms.CloseReason.FormOwnerClosing  '==, vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                '--Call cmdExit_Click
                ans = MsgBoxResult.Yes
                If FrameBuild.Enabled And (LCase(cmdCancel.Text) = "cancel") Then
                    ans = MsgBox("Abandon this quote/build..?", _
                             MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, "Memo entry")
                End If '-enabled..-
                If ans <> MsgBoxResult.Yes Then
                    intCancel = 1 '--cant close yet--
                Else '--yes--
                    intCancel = 0 '--let it go--
                End If
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--queryUnload--
    '= = = = = = = = = =  =

    '=== end main form ==

End Class


' Implements the manual sorting of items by columns.
Class ListViewItemComparerQ2
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
'==  the end (2)..==