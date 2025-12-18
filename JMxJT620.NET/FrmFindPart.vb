Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb
Imports VB = Microsoft.VisualBasic
Imports System.ComponentModel

Friend Class FrmFindPart
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = =  =
	
	'-- grh= 22-Feb-2010= ----
	'----    show  Job parts for selected Barcode-
	'--       and selected period.--
	'-- grh= 13-Jun-2010= ---- Add ListViw of All R-M Serials..--
	'-- grh= 11-Aug-2010= --- R-M Serials-  look for "SI" codes also..--
	'-- grh= 31-Jan-2011= --- Parts Search-  Add Customer, ServicedByStaffName to search columns....--
    '-- grh= 12-Nov-2011= --- mRetailHost1...--

    '-- grh= 08-Dec-2011= --- VB.NET version..--
    '-- grh= 07-Jan-2012= --- Fixes..--
    '== grh=  23Apr2012=  Fix colours..
    '==
    '==  "=V:3.0.3061.0= Built:07Jun2012= Fix listView index crash in Parts Search...=" & _
    '==
    '==  "=V:3.0.3077   = Built:14May 2013= SerialSearch: pass status/cancel...=" & _
    '= = = = = = = = = = = = = = = = = = = = = = = == = 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  15-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb 
    '==           (dropped sqlClient).. (For Jet OleDb driver).
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  grh. JobMatix 3.4.3431.0515 ---  15-May-2018 ===
    '==   >> Extra Input properties so we can show Job..
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Private mbActivated As Boolean = False
    Private mbCancelled As Boolean = False
	
    Private mCnnJobs As OleDbConnection   '= ADODB.Connection
    Private mRstParts As DataTable  '= ADODB.Recordset
	
	'== Private mCnnJet As ADODB.connection
	Private mRetailHost1 As _clsRetailHost
	
	'= Private mRstSerials As ADODB.Recordset
	'== Private mRstStockedSerials As ADODB.Recordset  '--in stock ony..-
	
    Private mlFormDesignHeight As Integer = Me.Height '--save==
    Private mlFormDesignWidth As Integer = Me.Width
    Private mlListViewLeft As Integer  '== = ListView1.Left
    Private mlListViewTop As Integer '=== ListView1.Top
	
	Private msSqlWhereSearch As String '--barcode/text srch.-
	'====Private msSqlWhereStatus As String   '--status or date arg..-
	Private msDateStart As String
	Private msDateEnd As String
	Private mDateOldest As Date
	
    Private mbSerialAudit As Boolean = False  '--looking at RM Serial Audit Table.--
    Private mbSerialCancel As Boolean = False  '--Cancel clicked for RM Serial Audit Table.--

    '==
    '==  grh. JobMatix 3.4.3431.0515 ---  15-May-2018 ===
    '==   >> Extra Input properties so we can show Job..
    Private mColSqlDBInfo As Collection '--  jobs DB info--

    Private mIntStaff_id As Integer = -1
    Private msStaffBarcode As String = ""
    Private msStaffName As String = ""

    '= = = = = = = = = = = = = ===
	
    WriteOnly Property sqlConnection() As OleDbConnection   '=ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '==connection..-
	'= = = = = = = = = = =

	WriteOnly Property retailHost() As _clsRetailHost
		Set(ByVal Value As _clsRetailHost)
			
			mRetailHost1 = Value
		End Set
	End Property '-host..-
	'= = = = = = = = = = = = = = = = =
	
	
	WriteOnly Property SerialAudit() As Boolean
		Set(ByVal Value As Boolean)
			
			mbSerialAudit = Value
			If Value Then '--lose jobs frame..-
				'==LabNoRecords.Top = FrameJobParts.Top
				'==mlListViewLeft = FrameJobParts.Left
				'===mlListViewTop = FrameJobParts.Top + 300
				FrameJobParts.Visible = False
				FrameSerialAudit.Top = FrameJobParts.Top
			End If
			
		End Set
	End Property '--serials..-
    '= = = = = = = = = =  =
    '==
    '==  grh. JobMatix 3.4.3431.0515 ---  15-May-2018 ===
    '==   >> Extra Input properties so we can show Job..

    WriteOnly Property ColSqlDBInfo As Collection
        Set(value As Collection)
            mColSqlDBInfo = value
        End Set
    End Property '-mColSqlDBInfo-
    '= = = = = = = = = = = = == == == = 

    WriteOnly Property Staff_id As Integer
        Set(value As Integer)
            mIntStaff_id = value
        End Set
    End Property  '-staff_id.
    '= = = = = = = = = = = == = =  = ==

    WriteOnly Property StaffBarcode As String
        Set(value As String)
            msStaffBarcode = value
        End Set
    End Property  '--staff barcode-
    '= = = = = = = = = = = = = = =
	
    WriteOnly Property StaffName As String
        Set(value As String)
            msStaffName = value
        End Set
    End Property  '--staffname-
    '= = = = = = = = = = = = = = =
    '-===FF->

	'--convert numeric data for sorted display..-
	
    Private Function msFormat(ByVal v1 As Object, _
                               ByVal intADO_Type As Integer, _
                                ByVal lSize As Integer) As String

        msFormat = gsFormat(v1, intADO_Type, lSize)


    End Function '--convert--
    '= = = = = = = = = = = = == = = = = =

	
	'--  get value of 1st rst item for SELECT..--
	
    Private Function mbGetSelectValue(ByVal sSql As String, _
                                      ByRef vResult As Object) As Boolean
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sErrorMsg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetSelectValue = False
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If Not (rs1 Is Nothing) Then
                If (rs1.Rows.Count > 0) Then '== Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '= rs1.MoveFirst()
                    Dim datarow1 As DataRow = rs1.Rows(0)  '--first row--
                    If Not IsDBNull(datarow1.Item(0)) Then
                        vResult = datarow1.Item(0)
                        mbGetSelectValue = True '--got something..-
                    End If '--null.-
                End If '--bof-
                '== rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElect..-
	'= = = = = = = = = = = = = =
	'-===FF->
	
	'-- Load RM Quotes listView from recordset..--
	'-- Load RM Quotes listView from recordset..--
	
    Private Function mbLoadListView(ByRef rs1 As DataTable, _
                                     ByRef ListView1 As System.Windows.Forms.ListView) As Boolean

        Dim s2, s1, s3, sSqlType As String
        '== Dim fldx As ADODB.Field
        Dim lngNoCols, cx, iq, ix, lngQty As Integer
        Dim lCount, intADO_Type, intSize As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim v1 As Object

        lngNoCols = 0
        lCount = 0
        '-- create column headers...--
        ListView1.Items.Clear()
        ListView1.Columns.Clear()
        For Each column1 As DataColumn In rs1.Columns   '== fldx In rs1.Fields
            lngNoCols = lngNoCols + 1
            s1 = "" & column1.ColumnName  '== CStr(fldx.Name)
            ListView1.Columns.Add(s1) '--, ListView1.Width \ 8
        Next column1  '= fldx '--fldx  --
        '--MsgBox "Headers loaded...", vbInformation
        LabNoRecords.Text = ""
        '--fill list box with record fields--
        If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ok.. not empty--
            '= rs1.MoveFirst()
            '== rs1.MoveLast()
            LabNoRecords.Text = "= FOUND: " & rs1.Rows.Count & " Part(s).."
            '== rs1.MoveFirst()
            ListView1.Items.Clear()
            lCount = 0
            '--scan recordset and load--
            For Each datarow1 As DataRow In rs1.Rows
                '---load current item--
                '== s1 = Trim(msFormat(rs1.Fields(0).Value, rs1.Fields(0).Type, rs1.Fields(0).DefinedSize))
                cx = 0  '=column index-
                '- get 1st field.-
                v1 = datarow1.Item(0)
                intSize = rs1.Columns(cx).MaxLength
                '--call global to convert Column-datatype to ADO Type and SqlType..--
                gbConvertDotNetDataType(rs1.Columns(cx), intADO_Type, sSqlType)
                s1 = Trim(msFormat(v1, intADO_Type, intSize))
                '-don't show if 1st col blank or dot..-  IE. BLANK SERIAL-NO..-
                If ((s1 <> "") And (s1 <> ".") And (s1 <> ",") And (s1 <> "-")) Then
                    item1 = ListView1.Items.Add(s1)
                    '==  item1.Text = s1
                    For cx = 1 To rs1.Columns.Count - 1  '= lngNoCols - 1 '--remainder of flds..-
                        '--s1 = CStr(mRstQuote(ix).Value)
                        '= s1 = msFormat(rs1.Fields(ix).Value, rs1.Fields(ix).Type, rs1.Fields(ix).DefinedSize)
                        v1 = datarow1.Item(cx)
                        intSize = rs1.Columns(cx).MaxLength
                        gbConvertDotNetDataType(rs1.Columns(cx), intADO_Type, sSqlType)
                        s1 = Trim(msFormat(v1, intADO_Type, intSize))
                        item1.SubItems.Add(s1)
                    Next cx  '= ix
                    lCount = lCount + 1
                    item1.Tag = CStr(lCount) '--ID of this part item..-
                End If '--s1-
            Next datarow1

            '== While (Not rs1.EOF)
            '==    rs1.MoveNext()
            '== End While '--eof
            '==MsgBox "Processed "
            '--Set ListView1.SelectedItem = ListView1.ListItems(1)
            '--show list--
            '--MsgBox s1   '--show last--
        Else
            MsgBox("No items to show...", MsgBoxStyle.Exclamation)
        End If '--not empty--
        mbLoadListView = True
    End Function '--load list..-
	'= = = = = = = = = = = = =
    '-===FF->

    '--  NEW VERSION --
  
    '-- Get list of R-M Serial Audit Table..-
    '-- Get list of R-M Serial Audit Table..-
	
	Private Function mbRefreshSerials() As Boolean
        '= Dim lngError As Integer
		Dim sSearchArg As String
        '== Dim asColumns As Object
        Dim colSerialsList As Collection
        Dim strErrorReport As String = ""
		
        mbRefreshSerials = False
        mbSerialCancel = False
		sSearchArg = Trim(txtSerialArg.Text)
		LabStatus.Text = "Getting Serial-Audit list.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Not mRetailHost1.serialGetAllSerials(sSearchArg, _
                      LabStatus, mbSerialCancel, colSerialsList, strErrorReport) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get serials list.." & _
                         vbCrLf & vbCrLf & strErrorReport, MsgBoxStyle.Exclamation)
        ElseIf mbSerialCancel Then
            LabStatus.Text = "Cancelled."
        Else
            '--ok-
            LabStatus.Text = "Loading ListView with: " & colSerialsList.Count() & " records.."
            System.Windows.Forms.Application.DoEvents()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Call gbLoadListViewFromCollection(colSerialsList, ListView1)
            LabStatus.Text = "Done: " & colSerialsList.Count() & " records.."
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            mbRefreshSerials = True
        End If
        LabStatus.BackColor = Color.LightGoldenrodYellow
        Exit Function
		
RefreshSerials_Error: 
        Dim lngError As Integer = Err.Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in  'mbRefreshSerials' function.." & vbCrLf & _
                "Error: " & lngError & ":  " & ErrorToString(lngError) & vbCrLf & _
                    "Serial lookup is/may be incomplete..", MsgBoxStyle.Exclamation)
		
        colSerialsList = Nothing
		
	End Function '--find serial..-
	'= = = = = = = = = = ==
	'= = = = = = = = = = = =
	'-===FF->

	'-- Get list of job parts..-
	'-- Get list of job parts..-
	
	Private Function mbRefreshParts() As Boolean
		Dim lCount, j, i, k, ix As Integer
		'--Dim lngOrigRow As Long
		Dim asColumns As Object
		Dim v1 As Object
		Dim sSql As String
		Dim sArg As String
		
		'--lngOrigRow = 0
		msSqlWhereSearch = ""
		sArg = Trim(txtArg.Text)
		'===If OptSrchArg(0).Value = True Then   '--Barcode.-
		'===    msSqlWhereSearch = " (RMBarcode='" + txtArg.Text + "') "
		'===ElseIf OptSrchArg(1).Value = True Then   '--Descr.-
		'===     msSqlWhereSearch = " ((RMCat1 LIKE '" + txtArg.Text + "') OR " + _
		''===                            " (RMDescription LIKE '" + txtArg.Text + "')) "
		'===End If  '--arg.-
        asColumns = New Object() {"PartSerialNumber", "RMBarcode", "RMCat1", "RMDescription", _
                                                     "CustomerCompany", "CustomerName", "ServicedByStaffName"}
		msSqlWhereSearch = gbMakeTextSearchSql(sArg, asColumns)
		
		sSql = " SELECT RMBarcode AS Barcode, RMCat1, RMDescription, PartSerialNumber AS SerialNo,   "
		sSql = sSql & " PartJob_id as JobNo, Jobs.JobStatus, JobParts.DateCreated, RMStock_Id AS Stock_id, "
        sSql = sSql & " Customer=(CASE CustomerCompany WHEN 'n/a' THEN '' WHEN '--' THEN '' " & _
                                                    "            ELSE (CustomerCompany +' + ') END + CustomerName), "
		sSql = sSql & " RMSell,  ServicedByStaffName "
		sSql = sSql & " FROM JobParts "
		sSql = sSql & "     LEFT JOIN Jobs ON (Jobs.Job_id=JobParts.PartJob_id)  "
		sSql = sSql & " WHERE  "
		If (Len(txtArg.Text) > 0) Then
			sSql = sSql & msSqlWhereSearch & " AND "
		End If
		'==If chkCurrent.Value = 1 Then '--use current status.-
		If OptStatus(0).Checked = True Then
			sSql = sSql & " ((LEFT(Jobs.jobStatus,2)>='30') AND (LEFT(Jobs.jobStatus,2)<'70'))" '-- current jobs..--
		ElseIf OptStatus(2).Checked = True Then  '--use dates..
			sSql = sSql & " (LEFT(Jobs.jobStatus,2)>='70') AND " '-- delivered jobs..--
			sSql = sSql & "  (JobParts.DateCreated>= '" & msDateStart & "') AND "
			sSql = sSql & "     (JobParts.DateCreated<= '" & msDateEnd & "') "
		Else '--all delivered..-
			sSql = sSql & " (LEFT(Jobs.jobStatus,2)>='70') "
		End If
		'===sSql = sSql + " WHERE (RMBarcode='" + txtBarcode.Text + "') AND   "
		'===sSql = sSql + "     (JobParts.DateCreated>= '" + LabDateStart + "') AND "
		'===sSql = sSql + "     (JobParts.DateCreated<= '" + LabDateEnd + "') "
		sSql = sSql & " ORDER BY PartJob_id "
		'====MsgBox "SQL is: " + vbCrLf + sSql
		System.Windows.Forms.Application.DoEvents()
		mbRefreshParts = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, mRstParts, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Parts recordset.." & vbCrLf & sSql, MsgBoxStyle.Critical)
            mbCancelled = True
            '--Me.Hide
            mRstParts = Nothing
            Exit Function
            '-Exit Function
        Else '--ok--
            Call mbLoadListView(mRstParts, ListView1)
            '==  "=V:3.0.3061.0= Built:07Jun2012= Fix index crash in Parts Search form...=" & _
            If (ListView1.Items.Count > 0) Then ListView1.FocusedItem = ListView1.Items.Item(0)
            mbRefreshParts = True
        End If '-got rs--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Function '--refresh..--
	'= = = = = = = = = = = =
	'-===FF->

	
    Private Sub FrmFindPart_Load(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        '= mbActivated = False
        '== mbCancelled = False
        '====MonthView1.Enabled = False
        '===LabSelectPeriod.Visible = False
        '--  cant make smaller than original..-
        mlFormDesignHeight = VB6.PixelsToTwipsY(Me.Height) '--save==
        mlFormDesignWidth = VB6.PixelsToTwipsX(Me.Width)

        '== mlListViewLeft = VB6.PixelsToTwipsX(ListView1.Left)
        '== mlListViewTop = VB6.PixelsToTwipsY(ListView1.Top)
        LabNoRecords.Text = ""
        Call CenterForm(Me)
        '===LabPrompt.Caption = ""

        msSqlWhereSearch = ""
        '==msSqlWhereStatus = ""
        '==LabQuery.Caption = ""
        LabPeriod.Text = ""
        '== mbSerialAudit = False
        LabStatus.Text = ""
        frameListView.Text = ""

    End Sub '--load--
	'= = = = = = = =
    '-===FF->

	'--Activate..-
	
    'UPGRADE_WARNING: Form event FrmFindPart.Activate has a new behavior. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'

    Private Sub FrmFindPart_Activated(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '-bActivated-
    '= = = = = = = = = = = = =

    '-3431.0515-  Now is SHOWN.

    Private Sub FrmFindPart_Shown(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        Dim sSql As String
        Dim v1 As Object
        Dim dateX As Date
        Dim lCount As Integer

        'If mbActivated Then Exit Sub
        'mbActivated = True

        If mbSerialAudit Then '--looking at RM serials..--
            FrameJobParts.Visible = False
            Me.Text = "JobTracking- Retail Serial Audit Table"
            LabHdr.Text = "  Retail (POS)- " & vbCrLf & "  Serial-Audit Table"
            ListView1.BackColor = System.Drawing.ColorTranslator.FromOle(&H71B33C) '--medium sea green..-
            FrameSerialAudit.Text = "- Serial Audit List -"
            FrameSerialAudit.BackColor = System.Drawing.ColorTranslator.FromOle(&H71B33C) '--medium sea green..- &H8CE6F0
            '== Call mbRefreshSerials()
            cmdSerialSrch.Focus()
            cmdSerialCancel.BackColor = Color.WhiteSmoke

        Else '--job parts..-
            FrameSerialAudit.Visible = False
            FrameJobParts.Text = ""
            mDateOldest = (DateAdd(Microsoft.VisualBasic.DateInterval.Year, -1, Today)) '--default to 1 year ago..-
            sSql = "SELECT MIN(DateCreated) as OldestJobDate FROM [JobParts]; "
            If mbGetSelectValue(sSql, v1) Then
                mDateOldest = CDate(v1)
            Else
                MsgBox("No Job date info found..", MsgBoxStyle.Exclamation)
            End If '--get-

            '--set up months combo..-
            cboMonths.Items.Clear()
            lCount = 12 '-maxx 12 months offered..-
            dateX = Today
            While (lCount > 0) And (dateX >= mDateOldest)
                cboMonths.Items.Add(VB6.Format(dateX, "yyyy-mmm"))
                dateX = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, dateX) '--go back a week at a time..-
                lCount = lCount - 1
            End While
            '--  show last 6 weeks..--
            '===msDateStart = Format(DateAdd("m", -1, Date), "yyyy-mmm-dd")
            '===msDateEnd = Format(Date, "yyyy-mmm-dd")

            '==DTPicker1.Value = CDate(msDateStart)
            '==DTPicker2.Value = CDate(msDateEnd)
            '====chkCurrent.Value = 1  '--current..-
            OptStatus(0).Checked = True '--current..-
            Call mbRefreshParts()
            txtArg.Focus()
        End If '--job parts..-
    End Sub '--Shown..   -activate..-
    '= = = = =  = = = = =
    '-===FF->

    '--  form resized..--
    'UPGRADE_WARNING: Event FrmFindPart.Resize may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub FrmFindPart_Resize(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            '--  cant make smaller than original..-
            If (VB6.PixelsToTwipsY(Me.Height) < mlFormDesignHeight) Then Me.Height = VB6.TwipsToPixelsY(mlFormDesignHeight)
            If (VB6.PixelsToTwipsX(Me.Width) < mlFormDesignWidth) Then Me.Width = VB6.TwipsToPixelsX(mlFormDesignWidth)

            '--resize results list box..--
            '== ListView1.SetBounds(8, 240, (Me.Width - 40), (Me.Height - 272))
            FrameJobParts.Width = (Me.Width - 24)
            frameListView.Width = FrameJobParts.Width
            frameListView.Height = (Me.Height - frameListView.Top - 16)

            '= ListView1.SetBounds(8, 240, (Me.Width - 40), (Me.Height - 272))
            ListView1.Width = frameListView.Width - 12
            ListView1.Height = frameListView.Height - ListView1.Top - 24

            System.Windows.Forms.Application.DoEvents()
        End If '--minimized..-

    End Sub '--resize..-
    '= = = = = = = = = =
    '-===FF->

    '-- listView..

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub '-SelectedIndexChanged-
    '= = = = = = = = = =

    '--listView1_DoubleClick--

    Private Sub listView1_DoubleClick(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles ListView1.DoubleClick
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer

        '--  Show Job View --..--
        item1 = ListView1.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            If IsNumeric(item1.SubItems(4).Text) AndAlso (CInt(item1.SubItems(4).Text) > 0) Then
                lngJobId = CInt(item1.SubItems(4).Text) '--5th column has to be job_id..--

                '--view job..
                Dim frmJobMaint3A As frmJobMaint3
                frmJobMaint3A = New frmJobMaint3

                '-- call Job Edit with selected jobno..--
                frmJobMaint3A.JobNo = lngJobId
                frmJobMaint3A.connectionSql = mCnnJobs
                '== frmJobMaint2.connectionJet = mCnnJet
                frmJobMaint3A.retailHost = mRetailHost1

                frmJobMaint3A.dbInfoSql = mColSqlDBInfo
                '== frmJobMaint2.dbInfoJet = mColJetDBInfo

                '-- V I E W  O N L Y --
                frmJobMaint3A.ServiceUpdate = False '-- NOT service type--
                frmJobMaint3A.DeliveryUpdate = False '--NOT delivery type--
                '== frmJobMaint3A.NotifyUpdate = False

                frmJobMaint3A.StaffId = mIntStaff_id
                frmJobMaint3A.StaffName = msStaffName
                frmJobMaint3A.StaffBarcode = msStaffBarcode

                frmJobMaint3A.ShowDialog(Me)
                frmJobMaint3A.Close()
            End If  '-numeric..

            'If lngJobId <> mlJobId Then '-- has changed..-
            '    Call mbShowJobInfo(lngJobId)
            'End If
        End If '--selected..-

    End Sub '--listView1_DblClick--
    '= = = = = = = =  = = = = = = =
    '-===FF->

    '--Serial Audit Search..--
    '--Serial Audit Search..--
    '--Serial Audit Search..--

    Private Sub cmdSerialClear_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles cmdSerialClear.Click
        txtSerialArg.Text = ""
    End Sub '--clear..==
    '= = = = = = = = = = =

    Private Sub cmdSerialSrch_Click(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cmdSerialSrch.Click
        cmdSerialSrch.Enabled = False
        Call mbRefreshSerials()
        cmdSerialSrch.Enabled = True
    End Sub '--srch..-
    '= = = = = = = = = =


    Private Sub txtSerialArg_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSerialArg.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter--
            If Trim(txtSerialArg.Text) <> "" Then
                Call mbRefreshSerials()
            End If
            keyAscii = 0 '--processed..-
        End If '--enter-

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--enter..-
    '= = = = = = = = =
    Private Sub cmdSerialCancel_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles cmdSerialCancel.Click
        mbSerialCancel = True
    End Sub  '--cancel-
    '= = = = = = = = = =
    '-===FF->


    '-- Job Parts.. Barcode/Text srch--
    '-- Barcode/Text srch--
    '-- Barcode/Text srch--

    Private Sub txtArg_Enter(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles txtArg.Enter

        txtArg.SelectionStart = 0
        txtArg.SelectionLength = Len(txtArg.Text)

    End Sub '--gotfocus..-
    '= = = = = = = = = =

    Private Sub txtArg_KeyPress(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtArg.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter--

            If Trim(txtArg.Text) <> "" Then
                '====LabSelectPeriod.Visible = True
                '===MonthView1.Enabled = True
                '===MonthView1.SetFocus
                Call mbRefreshParts()
            End If
            keyAscii = 0 '--processed..-
        End If '--enter-

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--enter..-
    '= = = = = = = = =

    Private Sub cmdJobPartClear_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles cmdJobPartClear.Click
        txtArg.Text = ""
    End Sub '--clear..--
    '= = = = = = = = =  =
    '-===FF->

    '--OptSrchArg--
    '--OptSrchArg--

    '===Private Sub OptSrchArg_Click(Index As Integer)

    '===   If OptSrchArg(0).Value = True Then   '--Barcode.-
    '===     LabPrompt.Caption = "Scan or Enter Item Barcode.."
    '===msSqlWhereSearch = " (RMBarcode='" + txtArg.Text + "') "

    '===   ElseIf OptSrchArg(1).Value = True Then   '--Descr.-
    '===      LabPrompt.Caption = "Description. (Use % for Wildcard..)" '--OptSrchArg--
    '==msSqlWhereSearch = " (RMCat1 LIKE '" + txtArg.Text + "') OR " + _
    ''==                     " (RMDescription LIKE '" + txtArg.Text + "') "
    '===   End If  '--arg.-
    '==   txtArg.SetFocus
    '===End Sub  '--OptSrchArg--
    '= = = = = = = ==

    '--  choose status or date..--

    '==Private Sub chkCurrent_Click()

    '==   If (chkCurrent.Value = 1) Then '--checked..-
    '--current jobs.. no dates..-
    '==DTPicker1.Enabled = False
    '==DTPicker2.Enabled = False
    '==      LabPeriod.Caption = ""
    '==      cboMonths.Enabled = False
    '==   Else
    '==DTPicker1.Enabled = True
    '==DTPicker2.Enabled = True
    '==      cboMonths.Enabled = True

    '==   End If  '--checked..-
    '==End Sub  '--chkCurrent..-
    '= = = = = = = = =

    'UPGRADE_WARNING: Event OptStatus.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim index As Short = OptStatus.GetIndex(eventSender)

            If OptStatus(2).Checked = True Then '--months,..-
                cboMonths.Enabled = True
            Else
                LabPeriod.Text = ""
                cboMonths.Enabled = False

            End If
        End If
    End Sub '--status.-
    '= = = = = = = = =

    '--months..-
    '--months..-

    'UPGRADE_WARNING: Event cboMonths.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboMonths_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles cboMonths.SelectedIndexChanged
        Dim sYear, s1, sMonth As String
        Dim ix, intMonth As Integer
        Dim date1, date2 As Date

        If cboMonths.SelectedIndex >= 0 Then
            s1 = Trim(VB6.GetItemString(cboMonths, cboMonths.SelectedIndex)) '--  get-- "yyyy-mmm"  --
            ix = InStr(s1, "-")
            If (ix > 1) Then
                sYear = Trim(VB.Left(s1, ix - 1))
                sMonth = Trim(Mid(s1, ix + 1)) '--  get MMM --
            End If
            '-- get month no..-
            intMonth = DatePart(Microsoft.VisualBasic.DateInterval.Month, CDate(s1 & "-28")) '--any day..
            If (intMonth >= 1) And (intMonth <= 12) Then
                date1 = DateSerial(CShort(sYear), intMonth, 1) '--start of selected month..-
                date2 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, date1)) '--day before 1st day of following month.-
                '--sWhere = " WHERE (YEAR(Jobs.DateCreated)= " + sYear + ") AND (MONTH(Jobs.DateCreated)= " & intMonth & ")  "
                '====GoSub ReportSetupWhereCondition
                msDateStart = VB6.Format(date1, "dd-mmm-yyyy") & " 00:00 " '-min-
                msDateEnd = VB6.Format(date2, "dd-mmm-yyyy") & " 23:59" '--max.--

                LabPeriod.Text = " Sel'd. Mnth: " & msDateStart & " to " & msDateEnd & ".."
            End If
        End If '--index.-
    End Sub '--months..-
    '= = = = = = = = = =
    '-===FF->

    '--Refresh..-
    '--Refresh..-

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        '===If Trim(txtArg.Text) <> "" Then
        '==msDateStart = Format(DTPicker1.Value, "yyyy-mmm-dd")
        '==msDateEnd = Format(DTPicker2.Value, "yyyy-mmm-dd")

        Call mbRefreshParts()
        '===End If

    End Sub '--Refresh..-
    '= = = = = = = = = =

    '--exit--

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdExit.Click

        mCnnJobs = Nothing
        mRstParts = Nothing

        '== Set mCnnJet = Nothing
        '== Set mRstSerials = Nothing
        '== Set mRstStockedSerials = Nothing '--in stock ony..-
        Me.Hide()
    End Sub
    '= = = = = = = = =

    '=== end form ====
    '=== end form ====

    '= = = = = = =  = = = = = == == =

End Class  '-FrmFindPart-
'= = = = = = = = = = = = = = 