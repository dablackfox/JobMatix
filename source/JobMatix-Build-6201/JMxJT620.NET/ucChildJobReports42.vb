Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data.Sql
Imports System.Data.OleDb

Public Class ucChildJobReports42
    Inherits System.Windows.Forms.UserControl   '=  .Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'


    '--grh--06-Apr-2011== Created as separate EXE for JobMatix2 Reports..==
    '--grh--19-Apr-2011== Rev-102== added staff Summary report...==
    '--grh--17-May-2011== Rev-113== Query Unload..==

    '--grh--23-Feb-2012=12:50pm = Rev-203== Jobs Reports..  add DaysInCustody to SQL ..==
    '--grh--02-Aug-2012= 6:58pm = Rev-211== NEW- Timesheet Report ..==
    '==
    '--grh--09-Aug-2012= 1:37pm = Rev-217== Fixes to Timesheet Report ..==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '==  NEW VERSION- Upgraded to vb.net PLUS .Net 3.5=

    '== --grh--28-Aug-2016= 1:37pm = 
    '==     Also moved to ADO.net (oleDb)..==
    '==
    '==
    '== --grh- v3.3.3323.0620=
    '==             -20-June-2018= 1:37pm =  Updates for Precise-=
    '==     --  Add DatePicker selection controls to select any date period. 
    '==     -- Completed Jobs Report-  Ignore whether Job delivered or not.-==
    '==            AND if chkExcludeNilLabourJobs.Checked then ignore jobs with no ChargeableTime
    '==     -- Caller must send in SIX labour rates on Cmd Line parms...-==
    '==        ie. HrlyRateP1,  HrlyRateP2,  HrlyRateP3,  HrlyRateOnSiteP1,  HrlyRateOnSiteP2,  HrlyRateOnSiteP3
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = =

    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '== Target-Build-4284  (Started 01-Nov-2020)
    '== Target-Build-4284  (Started 01-Nov-2020)
    '==
    '==    DEV- Bring JobReports into JobTracking Main as a UserControl..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= = = = = = = = = = = =
    '=3301.828= Const K_SAVESETTINGSPATH As String = "localJobSettings.txt"
    Private Const k_PrtSettingKey As String = "PRTCOLOUR"

    Const K_RTFLINEBREAK As String = "\par "


    Const K_MAXREPORTSTATUS As Short = 1 '--  0..1 --  (2 buttons)..--

    Const K_MAXREPORTOPTION As Short = 3 '--  0..3 --  (4 buttons)..--
    Const K_MAXMONTHOPTION As Short = 4 '--  0..4 --  (5 buttons)..--

    '-- Report Types--  ==17Apr2010==
    '--- Restores optReport Radio Buttons..-
    Const K_MAXREPORTTYPE As Short = 5 '--  0..5 --  (6 buttons)..--

    Const K_JOBS_REPORT As Short = 0 '--  0..3 --  (there were 4 buttons)..--
    Const K_PARTS_REPORT As Short = 1 '--  0..3 --  (there were 4 buttons)..--
    '==Const K_CUST_REPORT = 2 '--  NOT USED.--
    Const K_STAFF_REPORT As Short = 2 '--  0..3 --  (there were 4 buttons)..--
    Const K_TIMESHEET_REPORT As Short = 3

    '-- Report Job cboStatus--
    '==Const K_STATUS_QUEUEDJOBS = 0  '--waiting jobs..-
    Const K_STATUS_CURRENTJOBS As Short = 0 '--current jobs..-
    Const K_STATUS_COMPLETED_JOBS As Short = 1
    Const K_STATUS_DELIVEREDJOBS As Short = 2
    Const K_STATUS_ALL_JOBS As Short = 2
    '==Const K_STATUS_COMPLETEDJOBS = 2   '--overlaps Current and Completed..-

    '-- Report cboPeriod LISTINDEX..-
    '--Const K_REPORT_CURRENTJOBS = 0  '--current jobs..-
    Const K_REPORT_TODAY As Short = 0
    Const K_REPORT_CURRENTWEEK As Short = 1
    Const K_REPORT_LASTWEEK As Short = 2
    Const K_REPORT_CURRENTMONTH As Short = 3 '--current month..-
    Const K_REPORT_LASTMONTH As Short = 4 '--LAST MONTH..-
    Const K_REPORT_LAST12MONTHS As Short = 5 '--LAST 12 MONTHs..-
    Const K_REPORT_SELECT As Short = 6 '--Select month from combo..-
    Const K_REPORT_PICK_PERIOD As Short = 7 '--Select month from combo..-

    Const k_MAXPRINTCOLS As Short = 12 '--max columns we can print from grid..-
    '= = = = = = = = =  = = = =
    Private Const K_GOODS_ONSITEJOB = "ON-SITE JOB;"   '--3083.312--


    Private mbStartupDone As Boolean
    Private mColQuote As Collection
    Private mColQuoteLines As Collection '--of collections (lines..)==

    Private mCnnSql As OleDbConnection   '= ADODB.Connection
    Private mCnnShape As OleDbConnection   '=  ADODB.Connection
    Private msServer As String
    Private msSqlPwd As String
    Private msSqlUid As String

    Private msSqlVersion As String = ""
    Private msInputDBName As String = "" '=Build-4284
    Private msDBnameJobs As String = ""   '-Build-4284

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    Private msProgramVersion As String
    Private msLocalDataDir As String = ""

    '==3301.828=
    '== Private mSdSettings As Scripting.Dictionary
    '==3301.828=
    Private msSettingsPath As String = ""
    '==3301.828=
    Private mLocalSettings1 As clsLocalSettings

    Private mlStaffId As Integer
    Private mlSalesOrderId As Integer

    Private msCurrentUser As String
    Private mbIsSqlAdmin As Boolean

    Private miReportJobStatus As Short
    Private mDateOldest As Date
    Private miReportType As Short
    Private mlPeriodIndex As Integer

    Private mbShowDetailLines As Boolean
    '= = = = = = = = = = = = = = =

    Private msBusinessName As String
    Private msServiceChargeCat1 As String
    Private msServiceChargeCat2 As String

    '==3301.828= Private mSdSystemInfo As Scripting.Dictionary
    '==3301.828= Private mColSystemInfo As Collection
    '==3301.828=
    Private mSysInfo1 As clsSystemInfo

    Private mCurLabourHourlyRateP1 As Decimal = 1.0
    Private mCurLabourHourlyRateP2 As Decimal = 1.0
    Private mCurLabourHourlyRateP3 As Decimal = 1.0
    '-3323.0621=
    Private mCurLabourHourlyRateOnSiteP1 As Decimal = 1.0
    Private mCurLabourHourlyRateOnSiteP2 As Decimal = 1.0
    Private mCurLabourHourlyRateOnSiteP3 As Decimal = 1.0
    '-3323.0621=
    Private mbUseLegacyLabourRates As Boolean = False

    Private mCurLabourMinCharge As Decimal

    Private msDescriptionPriority1 As String
    Private msDescriptionPriority2 As String
    Private msDescriptionPriority3 As String
    '= = = = = = = = =  = = = = =

    '--Barcodes..-
    Private msItemBarcodeFontName As String
    Private mIntItemBarcodeFontSize As Integer

    Private mbCancelled As Boolean
    Private mbOK As Boolean

    '- printer-
    '----------
    Private msDefaultPrinterName As String = ""
    Private msSelectedPrinterName As String = ""  '-- "Adobe PDF"

    '--msColourPrinterName -
    '== = = = = = = = = =  = = = = == = =

    '-- Result of Report Query scraped in to recursive collection 
    '=   of rows and child chapters FROM the datareader query rsult.
    Private mColQueryResults As Collection

    '-- Collection at top (root) level points to a (first) chapter coll. which has two Items- keys are:
    '--   1. "ChapterName" (Current table name)-
    '--   2. "Rows"  (collection of row data).
    '--     Each "Row" item in [Rows] is a collection of two items:
    '--            (i) "RowData" is a collection of "field" collections: ("name=","value="..
    '--            (ii) "ChildChapter" (if not nothing) represents the child "recordset"
    '--                                   related to this row. (its mother).
    '--                "ChildChapter" will be collection with two Items as per BASE above.
    '--                     (Recursion starts hete).
    '--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '== Target-Build-4284  (Started 01-Nov-2020)
    '== Target-Build-4284  (Started 01-Nov-2020)

    Private msInputCmdline As String = ""
    Private mClsRetailHost1 As _clsRetailHost

    WriteOnly Property InputCmdline() As String
        Set(ByVal Value As String)
            msInputCmdline = Value
        End Set
    End Property '--sInputCmdline-
    '= = = = = = 

    WriteOnly Property Sql_Connection() As OleDbConnection
        Set(ByVal Value As OleDbConnection)
            mCnnSql = Value
        End Set
    End Property '--sInputCmdline-
    '= = = = = =  = = = = = = =

    WriteOnly Property sqlDbname() As String
        Set(ByVal Value As String)
            msInputDBName = Value
            msDBnameJobs = Value
        End Set
    End Property '--sInputCmdline-
    '= = = = = = 

    '-- clsRetailHost1 As _clsRetailHost--

    WriteOnly Property clsOurRetailHost As _clsRetailHost
        Set(ByVal Value As _clsRetailHost)
            mClsRetailHost1 = Value
        End Set
    End Property '--_clsRetailHost-
    '= = = = = = = = = == = = = == = = = 

    '-- These were originally sent from Main program via the Cmd Line..

    Private Function mbGetLatestlabourRates() As Boolean
        Dim sDescr As String
        Dim bIsOnSite As Boolean = False

        Try
            '-- get current rates..
            Call gbGetPriorityInfo(mCnnSql, "1", bIsOnSite, mClsRetailHost1, mCurLabourHourlyRateP1, sDescr)
            Call gbGetPriorityInfo(mCnnSql, "2", bIsOnSite, mClsRetailHost1, mCurLabourHourlyRateP2, sDescr)
            Call gbGetPriorityInfo(mCnnSql, "3", bIsOnSite, mClsRetailHost1, mCurLabourHourlyRateP3, sDescr)
            bIsOnSite = True
            Call gbGetPriorityInfo(mCnnSql, "1", bIsOnSite, mClsRetailHost1, mCurLabourHourlyRateOnSiteP1, sDescr)
            Call gbGetPriorityInfo(mCnnSql, "2", bIsOnSite, mClsRetailHost1, mCurLabourHourlyRateOnSiteP2, sDescr)
            Call gbGetPriorityInfo(mCnnSql, "3", bIsOnSite, mClsRetailHost1, mCurLabourHourlyRateOnSiteP3, sDescr)

            mbGetLatestlabourRates = True

            'MsgBox("TESTING- latest labour rates are-" & vbCrLf &
            '             FormatCurrency(mCurLabourHourlyRateP1, 2) & vbCrLf &
            '             FormatCurrency(mCurLabourHourlyRateP2, 2) & vbCrLf &
            '             FormatCurrency(mCurLabourHourlyRateP3, 2) & vbCrLf &
            '              FormatCurrency(mCurLabourHourlyRateOnSiteP1, 2) & vbCrLf &
            '              FormatCurrency(mCurLabourHourlyRateOnSiteP2, 2) & vbCrLf &
            '              FormatCurrency(mCurLabourHourlyRateOnSiteP3, 2) & vbCrLf, MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox("Failed to get latest labour rates-" & vbCrLf &
                         ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function '-mbGetLatestlabourRates-
    '= = = = = = = = = = = == == = = = =  =

    '== END Target-Build-4284  (Started 01-Nov-2020)
    '== END Target-Build-4284  (Started 01-Nov-2020)
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '--get day of week--
    Private Function msDayOfWeek(ByRef date1 As Date) As String
        Dim sDay As String

        Select Case DatePart(Microsoft.VisualBasic.DateInterval.Weekday, date1)
            Case 1 : sDay = "Sunday"
            Case 2 : sDay = "Monday"
            Case 3 : sDay = "Tuesday"
            Case 4 : sDay = "Wednesday"
            Case 5 : sDay = "Thursday"
            Case 6 : sDay = "Friday"
            Case 7 : sDay = "Saturday"
        End Select

        msDayOfWeek = sDay

    End Function '--day--
    '= = = = = = =  =  = =

    '-- set up Data1/Date2 WHERE SQL condition..-
    '-- based on the Form's  datePickers controls From/To..

    'Private Function msReportSetupWhereCondition(ByVal strDateColumn As String, _
    '                                             ByRef sWhere As String) As String
    '    Dim sDate1, sDate2 As String

    '    '-- format dates for SQL..-
    '    sdate1 = Format(DTPickerFrom.Value, "dd-MMM-yyyy") & " 00:00"  '-min-
    '    sDate2 = Format(DTPickerTo.Value, "dd-MMM-yyyy") & " 23:59"  '--max.--
    '    If (sWhere = "") Then
    '        sWhere = " WHERE "
    '    Else
    '        sWhere = sWhere & " AND "
    '    End If
    '    sWhere = sWhere & " ((" & strDateColumn & ">='" & sDate1 & "') AND (" & strDateColumn & "<='" & sDate2 & "')) "

    '    msReportPeriod = "From: " & sDate1 & "  To: " & sDate2
    '    msReportSetupWhereCondition = sWhere

    'End Function  '--SetupWhereCondition-
    '= = = =  = =  == = = = = = =  = = =
    '-===FF->


    '--mbComputeLabourCost=

    Private Function mbComputeLabourCost(ByVal strPriority As String,
                                        ByVal strHours As String,
                                        ByRef decCost As Decimal) As Boolean
        Dim decHours, decRate As Decimal

        mbComputeLabourCost = False
        decCost = 0
        If IsNumeric(strHours) Then
            Try
                decHours = CDec(strHours)
                Select Case VB.Left(strPriority, 1)
                    Case "1", "H", "Q"
                        decRate = mCurLabourHourlyRateP1
                    Case "2"
                        decRate = mCurLabourHourlyRateP2
                    Case "3"
                        decRate = mCurLabourHourlyRateP3
                    Case Else
                        decRate = mCurLabourHourlyRateP1
                End Select
                decCost = decHours * decRate
                mbComputeLabourCost = True
            Catch ex As Exception
                MsgBox("ERROR in reports mbComputeLabourCost-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If  '-numeric-

    End Function '-mbComputeLabourCost-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  LOCAL Settings file functions..--
    '--  LOCAL Settings file functions..--


    '-- Update one setting..--
    '----change the setting in the static var dictionary..--
    '----- Write the dictionary back to disk..--

    Private Function mbSaveSetting(ByRef sKey As String, ByRef sValue As String) As Boolean
        'Dim asKeys As Object
        'Dim sKey1 As String
        'Dim sPath As String
        'Dim sNewFileData As String
        'Dim ix, lResult As Integer
        '--if key exists..  remove it..--

        If Not mLocalSettings1.SaveSetting(sKey, sValue) Then

        End If



    End Function '--save setting.--
    '- - - -  - - - -  --
    '-===FF->

    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-

    'Private Function mbGetJobTrackingRecord(ByVal sSql As String, ByRef ColJobFields As Collection) As Boolean
    '	Dim RsJob As ADODB.Recordset
    '	Dim fld1 As ADODB.Field
    '	Dim colFld As Collection

    '	mbGetJobTrackingRecord = False
    '	'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '	System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '	If Not gbGetRst(mCnnSql, RsJob, sSql) Then
    '		MsgBox("Failed to get JOB recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
    '		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '		'--Me.Hide
    '		Exit Function
    '	End If
    '	'--txtMessages.Text = ""
    '	ColJobFields = New Collection
    '	If Not (RsJob Is Nothing) Then
    '		If RsJob.BOF And (Not RsJob.EOF) Then
    '			RsJob.MoveFirst()
    '		End If
    '		If (Not RsJob.EOF) Then '---And (cx < 100)
    '			'--return complete row..-
    '			For	Each fld1 In RsJob.Fields
    '				colFld = New Collection
    '				colFld.Add(LCase(fld1.Name), "name")
    '				colFld.Add(fld1.Value, "value")
    '				ColJobFields.Add(colFld, LCase(fld1.Name))
    '			Next fld1
    '			mbGetJobTrackingRecord = True
    '		Else '--not found-
    '		End If '-eof-
    '		RsJob.Close()
    '	End If '--rs-
    '	'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '	System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '	'UPGRADE_NOTE: Object RsJob may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '	RsJob = Nothing
    'End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '=3301.828=
    '-- L o a d  ANY JobTracking R e c o r d..-

    Private Function mbGetJobTrackingRecord(ByVal sSql As String,
                                         ByRef colJobFields As Collection) As Boolean
        Dim RsJob As DataTable '== ADODB.Recordset
        '--Dim fld1 As ADODB.Field
        Dim dataCol1 As DataColumn
        Dim sName As String
        Dim colFld As Collection

        mbGetJobTrackingRecord = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, RsJob, sSql) Then
            MsgBox("Failed to get JOB recordset.." & vbCrLf &
                    gsGetLastSqlErrorMessage() & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        colJobFields = New Collection
        If (Not (RsJob Is Nothing)) AndAlso (RsJob.Rows.Count > 0) Then
            '--return complete row..-
            Dim datarow1 As DataRow = RsJob.Rows(0)  '-first row.-
            For Each dataCol1 In RsJob.Columns  '= fld1 In RsJob.Fields
                colFld = New Collection
                sName = dataCol1.ColumnName
                colFld.Add(sName, "name")
                colFld.Add(datarow1.Item(sName), "value")
                colJobFields.Add(colFld, sName)
            Next dataCol1 '-fld1
            mbGetJobTrackingRecord = True
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--

    '==3301.828=
    '--  get value of 1st rst item for SELECT..--

    Private Function mbGetSelectValue(ByVal sSql As String, ByRef vResult As Object) As Boolean
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sErrorMsg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetSelectValue = False
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '== If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                '== rs1.MoveFirst()
                Dim datarow1 As DataRow = rs1.Rows(0)  '--1st row-
                If Not IsDBNull(datarow1.Item(0)) Then
                    vResult = datarow1.Item(0)
                    mbGetSelectValue = True '--got something..-
                End If '--null.-
                '== End If '--bof-
                '= rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSelect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  compute labour chargeable hours to date..-

    Public Function gCurComputeChargeableHours(ByVal sSessionTimes As String) As Decimal
        Dim sRem As String
        Dim sName, s1 As String
        Dim sDate As String
        Dim sTimeSpent As String
        Dim iPos2, iPos, iPos3 As Integer
        Dim bChargeable As Boolean
        Dim curResult As Decimal

        '-- Diseect accumulated session string..--
        curResult = 0
        sRem = Trim(sSessionTimes) '--get all sessions TO DATE this job..-
        While (sRem <> "")
            sName = "" : sDate = "" : sTimeSpent = ""
            bChargeable = True
            iPos = InStr(sRem, vbCrLf)
            If iPos > 0 Then
                s1 = Trim(VB.Left(sRem, iPos - 1))
                sRem = Trim(Mid(sRem, iPos + 2)) '--drop cf/lf-
            Else
                s1 = Trim(sRem) : sRem = "" '--last session..-
            End If '--ipos.-
            '--dissect session..-
            If s1 <> "" Then
                iPos = InStr(s1, ":")
                If (iPos > 1) Then
                    sDate = Trim(VB.Left(s1, iPos - 1))
                    If Not IsDate(sDate) Then
                        '--sDate = sDateCompleted  '--in case of bad stuff.--
                    Else
                        sDate = VB6.Format(CDate(sDate), "dd-mmm-yyyy") '--reformat..--
                    End If
                    s1 = Trim(Mid(s1, iPos + 1))
                    iPos2 = InStr(s1, "+")
                    If (iPos2 > 0) Then
                        sName = Trim(VB.Left(s1, iPos2 - 1))
                        If sName = "" Then sName = "YY_UNKNOWN"
                        sTimeSpent = UCase(Trim(Mid(s1, iPos2 + 1)))
                        iPos3 = InStr(sTimeSpent, "-NC") '--17Apr2010--
                        If (iPos3 > 0) Then '--no charge-
                            bChargeable = False
                            sTimeSpent = Replace(sTimeSpent, "-NC", "") '-get rid of marker..-
                        End If
                    End If
                End If
            End If '--s1
            '====sTest = sTest + sDate + "," + sName + "," + sTimeSpent + "; "
            '--create session record.-
            If bChargeable And (sName <> "") And (sDate <> "") And IsNumeric(sTimeSpent) Then '-- sTimeSpent = ""
                curResult = curResult + CDec(sTimeSpent)
            End If
        End While '--sessions.=
        gCurComputeChargeableHours = curResult
    End Function '--chargeable hours.-
    '= = = = = = = = = == =
    '-===FF->

    '-- SET-UP SQL FUNCTION to compute labour chargeable hours to date..-
    '-- SET-UP SQL FUNCTION to compute labour chargeable hours to date..-
    '----create function to evaluate all sessions lines..--

    Private Function mbSetupSqlChargeableHoursFunction() As Boolean
        Dim sSql As String
        Dim lngaffected As Integer
        Dim sErrorMsg As String

        mbSetupSqlChargeableHoursFunction = False
        '-- so we we can summarise for job..--
        '--  First, DROP it in case it's still there..-
        sSql = " DROP FUNCTION JT2_ChargeableHours "
        If gbExecuteCmd(mCnnShape, sSql & vbCrLf, lngaffected, sErrorMsg) Then
            If gbDebug Then MsgBox("DROPPED function JT2_ChargeableHours ok..", MsgBoxStyle.Information)
        Else
            If gbDebug Then MsgBox("FAILED to DROP function JT2_ChargeableHours..", MsgBoxStyle.Exclamation)
        End If
        '--  build function.--
        sSql = " /*---Function to dissect and sum all chargeable session-line times in one job..--*/ " & vbCrLf
        sSql = sSql & " CREATE FUNCTION JT2_ChargeableHours (@sSessionTimes varchar(2000)) "
        sSql = sSql & " RETURNS Decimal(7,2) "
        sSql = sSql & " AS "
        sSql = sSql & " BEGIN "
        sSql = sSql & "      DECLARE @sRem varchar(2000), @s1 varchar(2000), @s2 varchar(2000) "
        sSql = sSql & "      DECLARE @sTimeSpent varchar(100)  "
        sSql = sSql & "      DECLARE @nResult decimal(7,2) "
        sSql = sSql & "      DECLARE @iPos int, @iPos2 int "
        sSql = sSql & "   SET @sRem=@sSessionTimes "
        sSql = sSql & "   SET @nResult=0  "
        sSql = sSql & "   WHILE (LEN(@sRem) >0)    "
        sSql = sSql & "     BEGIN  "
        sSql = sSql & "       /*-- get next session..  sessions are separated by CRLF --*/ "
        sSql = sSql & "       SET @iPos=CHARINDEX(CHAR(13)+CHAR(10), @sRem)  "
        sSql = sSql & "       IF (@iPos > 0) BEGIN "
        sSql = sSql & "                   SET @s1 = LTRIM(RTRIM(LEFT(@sRem, @iPos - 1)))  "
        sSql = sSql & "                    /*--drop cf/lf- and SAVE Remainder.. -- */      "
        '== Target-Build-4284  (Started 01-Nov-2020)-  USE SUBSTRING instead of RIGHT..
        '== sSql = sSql & "                   SET @sRem = RTRIM(LTRIM(RIGHT(@sRem, (LEN(@sRem)-@iPos-1) ) )) "
        sSql = sSql & "                   SET @sRem = RTRIM(LTRIM(SUBSTRING(@sRem, @iPos+1, (LEN(@sRem)-@iPos) ) )) "
        sSql = sSql & "                   END  "
        sSql = sSql & "        ELSE BEGIN SET @s1 = LTRIM(RTRIM(@sRem)) "
        sSql = sSql & "                  SET @sRem = ''  END  /*--last session..-*/  "
        sSql = sSql & "      /*--dissect session in s1 ..-*/ "
        sSql = sSql & "      IF (LEN(@s1)>0)  "
        sSql = sSql & "        BEGIN   "
        sSql = sSql & "          SET @iPos = CHARINDEX(':', @s1)  "
        sSql = sSql & "          IF (@iPos > 0)  "
        sSql = sSql & "             BEGIN "
        sSql = sSql & "               SET @iPos2 = CHARINDEX('+', @s1, @iPos+1) /*-get past date to name-delim..-*/  "
        sSql = sSql & "               IF (@iPos2 > 0)  "
        sSql = sSql & "                  BEGIN "
        '== Target-Build-4284  (Started 01-Nov-2020)-  USE SUBSTRING instead of RIGHT..
        '= sSql = sSql & "                     SET @sTimeSpent = UPPER(LTRIM(RTRIM(RIGHT(@s1, (LEN(@s1)-@iPos2-1) )))) "
        sSql = sSql & "                     SET @sTimeSpent = UPPER(LTRIM(RTRIM(SUBSTRING(@s1,@iPos2+1, (LEN(@s1)-@iPos2) )))) "
        sSql = sSql & "                     IF (CHARINDEX('-NC', @sTimeSpent) <=0)   /*- if not No-charge.--*/   "
        sSql = sSql & "                        BEGIN SET @nResult =@nResult + CAST(@sTimeSpent AS decimal(7,2)) END "
        sSql = sSql & "                  END   /*-- start of time..--*/  "
        sSql = sSql & "             END    /*-- found colon..--*/ "
        '== Target-Build-4284  (Started 01-Nov-2020)-  Don't change result...
        '== sSql = sSql & "           ELSE  SET @nResult= @nResult -20   /*- no colon.-*/"
        sSql = sSql & "         END  /* - s1 -*/  "
        sSql = sSql & "     END   /*-- While --*/  "
        sSql = sSql & "   RETURN @nResult  "
        sSql = sSql & " END  /*-- AS function. --*/ "

        If gbExecuteCmd(mCnnShape, sSql & vbCrLf, lngaffected, sErrorMsg) Then
            mbSetupSqlChargeableHoursFunction = True
            '== MsgBox "RE-lOADED function JT2_ChargeableHours ok..", vbInformation
        Else '--failed..-
            MsgBox("Failed to load SQL FUNCTION JT2_ChargeableHours..  " & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
        End If

    End Function '--SetupSql--
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '==3301.828=
    '- ReportSetupWhereCondition --

    Private Function mbReportSetupWhereCondition(ByVal date1 As Date,
                                                  ByVal date2 As Date,
                                                  ByVal sDateColumn As String,
                                                  ByRef sDate1 As String,
                                                  ByRef sDate2 As String,
                                                  ByRef sWhere As String) As Boolean
        '-- format dates for SQL..-
        sDate1 = VB6.Format(date1, "dd-mmm-yyyy") & " 00:00" '-min-
        sDate2 = VB6.Format(date2, "dd-mmm-yyyy") & " 23:59" '--max.--
        If sWhere = "" Then
            sWhere = " WHERE "
        Else
            sWhere = sWhere & " AND "
        End If
        sWhere &= " ((Jobs." & sDateColumn & ">='" & sDate1 & "') AND (Jobs." & sDateColumn & "<='" & sDate2 & "')) "
        'UPGRADE_WARNING: Return has a new behavior. Click for more: 
        '== ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        '=Return
    End Function '--parts report..-
    '= = = = = = = = = = = = = = ==
    '-===FF->


    '-- parts report..--
    '-- parts report..--
    '-- parts report..--

    Private Sub mbReports_Execute(ByVal idxReport As Short,
                                  ByVal bShowDetail As Boolean,
                                  ByVal bPreviewOnly As Boolean)
        Dim fx, idx, ix, rx, lngParentRows, intCount As Integer
        Dim sWhereWarranty As String
        Dim sShapeSql As String
        Dim sCostSelectSql As String
        Dim sBarcodeSql As String
        Dim sBarcodeFont As String
        Dim sWhere As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim s2, s1, s3, s4, s5 As String
        Dim sYear, sMonth As String
        Dim strLabourRates As String
        Dim intMonth As Short
        Dim intYear As Short
        Dim intDayofWeek As Short
        Dim bCurrent As Boolean
        Dim sDateColumn As String
        Dim date1, date2 As Date
        Dim sDate1, sDate2, sDateCompleted As String
        Dim sReportPeriod As String
        Dim sQueryResult As String
        Dim curLabourRatePriority1 As Decimal
        Dim curChargeableTime As Decimal
        Dim curLabourCharge As Decimal
        '=3301.828 Dim report1 As Object
        '=3301.828 Dim rptStaff As Object

        sWhereWarranty = "" '=== " AND (IsWarrantyPart = 'Y') "
        sDateCompleted = ""
        sBarcodeFont = msItemBarcodeFontName '== "IDAutomationHC39M"
        '==If (ChkWarranty.Value <> 1) Then sWhereWarranty = ""
        date2 = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -2, Today)
        date1 = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -3, Today)


        '== Target-Build-4284  (Started 01-Nov-2020)
        '== Target-Build-4284  (Started 01-Nov-2020)
        Call mbGetLatestlabourRates()
        '== END Target-Build-4284  (Started 01-Nov-2020)
        '== END Target-Build-4284  (Started 01-Nov-2020)


        strLabourRates = "Labour Rates: P1=" & FormatCurrency(mCurLabourHourlyRateP1, 2)
        strLabourRates = strLabourRates & "; P2=" & FormatCurrency(mCurLabourHourlyRateP2, 2)
        strLabourRates = strLabourRates & "; P3=" & FormatCurrency(mCurLabourHourlyRateP3, 2)

        '--TESTING.---
        '--TESTING.---
        '===msServiceChargeCat1 = "SERVCE"  '--TESTING.---
        '===msServiceChargeCat2 = ""   '-- "SERVCE"        '--TESTING.---
        '-- END TESTING.---

        sDateColumn = "DateUpdated"
        curLabourRatePriority1 = 99.0# '--testing..--

        '-- set up SQL to separate RMSell (irem cost) into SERVICE and Parts..--
        '---- Relies on definitions for  "msServiceChargeCat1" and "msServiceChargeCat2"..--

        '--Set up the default column values for No Service-Cat defined..-
        sCostSelectSql = " ServiceCharge=0, PartCost=RMSell, PrtService='-', PrtPart=CAST(RMSell AS char(10)) "

        '-- First add currency columns for computing totals..--
        If (msServiceChargeCat1 <> "") Then
            '===If (msServiceChargeCat2 <> "") Then    '--Cat1 and cat2 defined..--
            sCostSelectSql = " ServiceCharge= (CASE "
            sCostSelectSql = sCostSelectSql & " WHEN ((RMCat1='" & msServiceChargeCat1 & "') "
            sCostSelectSql = sCostSelectSql & IIf((msServiceChargeCat2 <> ""), "  AND (RMCat2='" & msServiceChargeCat2 & "')) ", ")")
            sCostSelectSql = sCostSelectSql & " THEN RMSell ELSE 0 END), "
            '-- Part cost is the inverse..--
            sCostSelectSql = sCostSelectSql & " PartCost= (CASE "
            sCostSelectSql = sCostSelectSql & " WHEN ((RMCat1='" & msServiceChargeCat1 & "') "
            sCostSelectSql = sCostSelectSql & IIf((msServiceChargeCat2 <> ""), "  AND (RMCat2='" & msServiceChargeCat2 & "')) ", ")")
            sCostSelectSql = sCostSelectSql & "  THEN 0 ELSE RMSell END), "
        End If '--service..--
        '-- Now add Char. formatted currency columns for printing the detail cost..--
        If (msServiceChargeCat1 <> "") Then
            sCostSelectSql = sCostSelectSql & " PrtService= (CASE "
            sCostSelectSql = sCostSelectSql & " WHEN ((RMCat1='" & msServiceChargeCat1 & "') "
            sCostSelectSql = sCostSelectSql & IIf((msServiceChargeCat2 <> ""), "  AND (RMCat2='" & msServiceChargeCat2 & "')) ", ")")
            sCostSelectSql = sCostSelectSql & " THEN CAST(RMSell AS char(10)) ELSE '-' END), "
            '-- Part cost is the inverse..--
            sCostSelectSql = sCostSelectSql & " PrtPart= (CASE "
            sCostSelectSql = sCostSelectSql & " WHEN ((RMCat1='" & msServiceChargeCat1 & "') "
            sCostSelectSql = sCostSelectSql & IIf((msServiceChargeCat2 <> ""), "  AND (RMCat2='" & msServiceChargeCat2 & "')) ", ")")
            sCostSelectSql = sCostSelectSql & "  THEN '-' ELSE CAST(RMSell AS char(10)) END) "
        End If '--service..--

        '--prod stuff..-
        '==MsgBox "sCostSelectSql is: " & vbCrLf & sCostSelectSql, vbInformation

        '---MsgBox "Option " & index & " clicked..", vbInformation
        '--labStatus.Visible = False
        LabReportName.Visible = True
        '--mlStaffTimeout = -1  '--SUSPEND timing out..--
        bCurrent = False
        LabReportName.Text = "" : sWhere = ""
        intDayofWeek = (DatePart(Microsoft.VisualBasic.DateInterval.Weekday, Today) - 1) '--This Week: gives 0 for Sun., 1 for Monday..
        If intDayofWeek <= 0 Then intDayofWeek = 7 '-- and now 7 for Sunday..-
        '-- set up report JOBSTATUS condition..-
        '===If miReportJobStatus = K_STATUS_QUEUEDJOBS Then  '--waiting jobs..  date irrelevant..-
        '===    sWhere = " WHERE (LEFT(Jobs.jobStatus,2)<'30')" '-- queued jobs..--
        '===    LabReportName.Caption = " Queued Jobs only.."
        '===ElseIf miReportJobStatus = K_STATUS_CURRENTJOBS Then '--WIP -current jobs..  date irrelevant..-
        If miReportJobStatus = K_STATUS_CURRENTJOBS Then '--WIP -current jobs..  date irrelevant..-
            sWhere = " WHERE ((LEFT(Jobs.jobStatus,2)>='05') AND (LEFT(Jobs.jobStatus,2)<'70'))" '-- current jobs..--
            bCurrent = True
            LabReportName.Text = " Current Jobs only.."
        ElseIf miReportJobStatus = K_STATUS_COMPLETED_JOBS Then '--COMPLETED- DELIVERED or not 
            '--      ..= 3323.0620- Completion date is NOW relevent..   NOT relevant..-
            '===     sWhere = " WHERE ((LEFT(Jobs.jobStatus,2)>='50') AND (LEFT(Jobs.jobStatus,2)<='70')) "  '--not cancelled jobs..-
            sWhere = " WHERE ((LEFT(Jobs.jobStatus,2)>='50') AND (LEFT(Jobs.jobStatus,2)<='70') " &
                                                       " AND (DateCompleted IS NOT NULL)  ) "  '--del or not delivered jobs..-
            '===3323.0620=     '-- jobs completed: ComplDate must be in in PERIOD..--
            sDateColumn = "DateCompleted"
            LabReportName.Text = "Jobs Completed: "
        ElseIf miReportJobStatus = K_STATUS_DELIVEREDJOBS Then  '--WIP -current jobs..  date irrelevant..-
            sWhere = " WHERE (LEFT(Jobs.jobStatus,2)='70') " '-- jobs DELIVERED:  DeliveryDate must be in in PERIOD....--
            sDateColumn = "DateDelivered"
            LabReportName.Text = "Delivered Jobs: "
        Else '==ALL- Timesheets..-
            sWhere = "" '-- all jobs..  depends on date updated...--
            sDateColumn = "DateUpdated"
            LabReportName.Text = "Jobs Updated: "

        End If '-status..-

        '-- setup period if used..-
        '==If (miReportJobStatus = K_STATUS_COMPLETEDJOBS) Or ----
        If (miReportJobStatus = K_STATUS_DELIVEREDJOBS) Or (mlPeriodIndex >= 0) Then '--need period..-
            idx = cboPeriod.SelectedIndex
            If (idx = K_REPORT_TODAY) Then
                date1 = Today '-- from today..
                date2 = Today '-- end date is today.-
                '=3301.828= GoSub ReportSetupWhereCondition
                '=3301.828=
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text = LabReportName.Text & "Today:  " & sDate1 & ".."
            ElseIf (idx = K_REPORT_CURRENTWEEK) Then
                date1 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -(intDayofWeek - 1), Today) '-- gives start date this week..
                date2 = Today '-- end date is today.-
                '=3301.828= GoSub ReportSetupWhereCondition
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text = LabReportName.Text & "This week:  " & sDate1 & "  to " & sDate2 & ".."
            ElseIf (idx = K_REPORT_LASTWEEK) Then
                date1 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -(intDayofWeek - 1) - 7, Today) '-- gives start date this week minus 7 days..
                date2 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 6, date1) '--end day of last week..
                '=3301.828= GoSub ReportSetupWhereCondition
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text = LabReportName.Text & "Last Week:  " & sDate1 & "  to " & sDate2 & ".."
            ElseIf (idx = K_REPORT_CURRENTMONTH) Then
                '--sWhere = " WHERE (YEAR(Jobs.DateCreated)= YEAR(CURRENT_TIMESTAMP)) AND (MONTH(Jobs.DateCreated)= MONTH(CURRENT_TIMESTAMP))  "
                date1 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -(VB.Day(Today) - 1), Today) '--start at 1st day of this month..-
                date2 = Today '-- end date is today.-
                '=3301.828= GoSub ReportSetupWhereCondition
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text = LabReportName.Text & " This Month:  " & sDate1 & "  to " & sDate2 & ".."
                '--ElseIf OptMonth(K_REPORT_LASTMONTH).Value = True Then
            ElseIf (idx = K_REPORT_LASTMONTH) Then
                intYear = Year(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, Today)) '--go back one month--
                intMonth = Month(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, Today)) '--go back one month--
                date1 = DateSerial(intYear, intMonth, 1) '--start of selected month..-
                date2 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, date1)) '--day before 1st day of following month.-
                '=3301.828= GoSub ReportSetupWhereCondition
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text = LabReportName.Text & "Last Month: " & sDate1 & " to " & sDate2 & ".."
            ElseIf (idx = K_REPORT_LAST12MONTHS) Then
                '-- sWhere = " WHERE (Jobs.DateCreated> DATEADD(mm,-12,CURRENT_TIMESTAMP)) "
                s1 = Mid(VB6.Format(Today, "dd-mmm-yyyy"), 4, 3) '--to get MMM..-
                intYear = Year(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -12, Today)) '--go back 12 months--
                date1 = CDate("01-" & s1 & "-" & VB6.Format(intYear, "0000")) '--make "01-MMM-yyyy"  12 months ago.-
                date2 = Today '--end date is today..-
                '=3301.828= GoSub ReportSetupWhereCondition
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text = LabReportName.Text & "12 Mths:  " & sDate1 & "  to " & sDate2 & ".."
            ElseIf (idx = K_REPORT_SELECT) Then  '--Month cbo selected.--
                If CboMonths.SelectedIndex >= 0 Then
                    s1 = Trim(VB6.GetItemString(CboMonths, CboMonths.SelectedIndex)) '--  get-- "yyyy-mmm"  --
                    ix = InStr(s1, "-")
                    If (ix > 1) Then
                        sYear = Trim(VB.Left(s1, ix - 1))
                        sMonth = Trim(Mid(s1, ix + 1)) '--  get MMM --
                    End If
                End If
                '-- get month no..-
                intMonth = DatePart(Microsoft.VisualBasic.DateInterval.Month, CDate(s1 & "-28")) '--any day..
                If (intMonth >= 1) And (intMonth <= 12) Then
                    date1 = DateSerial(CShort(sYear), intMonth, 1) '--start of selected month..-
                    date2 = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, date1)) '--day before 1st day of following month.-
                    '--sWhere = " WHERE (YEAR(Jobs.DateCreated)= " + sYear + ") AND (MONTH(Jobs.DateCreated)= " & intMonth & ")  "
                    '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="C5A1A479-AB8B-4D40-AAF4-DB19A2E5E77F"'
                    '=3301.828= GoSub ReportSetupWhereCondition
                    Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                    LabReportName.Text &= "For Month: " & sDate1 & " to " & sDate2 & ".."
                End If
            ElseIf (idx = K_REPORT_PICK_PERIOD) Then  '-date pickers..
                date1 = DTPickerFrom.Value
                date2 = DTPickerTo.Value
                Call mbReportSetupWhereCondition(date1, date2, sDateColumn, sDate1, sDate2, sWhere)
                LabReportName.Text &= "For Period: " & sDate1 & " to " & sDate2 & ".."

            Else '--invalid period..-
                MsgBox("No period selected", MsgBoxStyle.Exclamation)
                Exit Sub
            End If '--select period..-
        End If '-completed/delivered-period..-

        '----If Not (chkCurrentJobs.Value = 1) Then sWhere = ""
        '--ListResults.Clear
        '====MSHFlexGrid1.Clear   '--empty grid..-
        sShapeSql = ""
        sReportPeriod = LabReportName.Text
        '-- Report Type--
        Dim dsReport As DataSet
        '==  https://msdn.microsoft.com/en-us/library/bh8kx08z(v=vs.90).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-3
        '== Dim adapterReport As OleDbDataAdapter
        '= New OleDbDataAdapter( _
        '  "SHAPE {SELECT CustomerID, CompanyName FROM Customers} " & _
        '  "APPEND ({SELECT CustomerID, OrderID FROM Orders} AS Orders " & _
        '  "RELATE CustomerID TO CustomerID)", connection)

        Dim cmd1 As OleDbCommand
        Dim rdr1 As OleDbDataReader
        Dim strQueryResult As String = ""                   '-- PREVIEW/PRINT --
        Dim clsReportPrint1 As clsReportPrinter
        '= clsReportPrint1 = New clsReportPrinter
        Dim colReportLines As Collection

        '-- Process reports-
        If idxReport = 0 Then '-- Jobs/Parts..--
            '-- Jobs/Parts..--
            '-- Jobs/Parts..--
            '--load code for sql function 'JT2_ChargeableHours'..--
            Call mbSetupSqlChargeableHoursFunction()
            LabReportName.Text = " Loading Jobs Report Form.."
            System.Windows.Forms.Application.DoEvents()
            '==Load rptJobs
            If (Not bShowDetail) Then '--wants summary.-
                '=3301.828= report1 = New rptJobsSum
            Else '--detail-
                '=3301.828= report1 = New rptJobs
            End If '-summary.-

            '= ONSITE JOBS are-  (UPPER(GoodsInCare) = '" & K_GOODS_ONSITEJOB & "') 

            '-- Check for chkExcludeNilLabourJobs --
            If chkExcludeNilLabourJobs.Checked Then
                sWhere &= " AND (dbo.JT2_ChargeableHours(SessionTimes) >0) "
            End If  '-exclude-

            '===LabReportName.Caption = "Jobs/Parts List- " & LabReportName.Caption
            sShapeSql = "SHAPE {SELECT job_id AS JobNo, DateCreated,JobStatus, Priority, DateCompleted, DateDelivered, "
            '-- LatestDate.--
            sShapeSql &= " GoodsIncare, ISNULL(DateDelivered, DateUpdated) AS LastUpdate, "
            sShapeSql &= " LastDateCaption=(CASE WHEN DateDelivered IS NULL THEN 'Updated:' ELSE 'Delivered:' END), "
            '-- Days In custody.--
            sShapeSql &= " DATEDIFF(day,dateCreated, ISNULL(DateDelivered, CURRENT_TIMESTAMP)) AS DaysInCustody, "
            '----
            sShapeSql &= " Customer=(CASE CustomerCompany WHEN 'n/a' THEN CustomerName "
            sShapeSql &= "                                WHEN '--'  THEN CustomerName "
            sShapeSql &= "                                WHEN ''    THEN CustomerName "
            sShapeSql &= "      ELSE CustomerCompany  END ), "
            sShapeSql &= " CustomerBarcode AS CustBarcode, TotalServiceTime AS TotalTime, SessionTimes, "
            sShapeSql &= " ChargeableTime=dbo.JT2_ChargeableHours(SessionTimes), "
            sShapeSql &= "  LabourCharge= (CASE Priority "
            sShapeSql &= "    WHEN '3' THEN (dbo.JT2_ChargeableHours(SessionTimes) *" & CStr(mCurLabourHourlyRateP3) & ") "
            sShapeSql &= "    WHEN '2' THEN (dbo.JT2_ChargeableHours(SessionTimes) *" & CStr(mCurLabourHourlyRateP2) & ") "
            sShapeSql &= "    ELSE (dbo.JT2_ChargeableHours(SessionTimes) *" & CStr(mCurLabourHourlyRateP1) & ") END) "
            sShapeSql &= " FROM Jobs " & sWhere & " ORDER BY job_id ASC } "
            sShapeSql &= " APPEND ({SELECT RMCat1, RMdescription,RMBarcode, PartJob_id  AS JobNo, " & sCostSelectSql
            sShapeSql &= "  FROM [jobParts] ORDER BY RMCat1, RMdescription } "
            sShapeSql &= " AS chapParts "
            sShapeSql &= " RELATE JobNo TO JobNo )"

            '===MsgBox "sShapeSql is: " & vbCrLf & sShapeSql, vbInformation
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '==3301.828-  
            '= dsReport = New DataSet
            '-- start retrieval-
            Try
                '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
                cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
                rdr1 = cmd1.ExecuteReader
            Catch ex As Exception
                MsgBox("Error getting Jobs recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End Try
            '--get parent/child tables into the dataset--
            '=  intCount = adapterReport.Fill(dsReport, "Jobs")
            If (Not (rdr1 Is Nothing)) Then  '= gbGetRst(mCnnShape, rs1, sShapeSql) Then
                If Not (rdr1.HasRows) Then  '= (rs1.BOF And rs1.EOF) Then '--not empty..-
                    MsgBox("No rows to show..", MsgBoxStyle.Information)
                    rdr1.Close()   '=rs1 = Nothing
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                Else '--not empty..-
                    'rs1.MoveFirst()
                    'rs1.MoveLast()
                    '== lngParentRows = rs1.Rows.Count '= rs1.RecordCount
                    '-- extract chargeable sessions and compute labour cost..
                    '----  each job..==
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '--Bind the recordset to the report..-
                    LabReportName.Text = " Displaying Jobs Report Form.."
                    System.Windows.Forms.Application.DoEvents()
                    '                  report1.DataSource = rs1
                    '                  report1.DataMember = rs1.DataMember
                    ''==Public Function gCurComputeChargeableHours(ByVal sSessionTimes As String) As Currency
                    ''-- Update the lblRecCount control on the report (Page Footer Section) to show RecordCount
                    '                  report1.Sections("Section5").Controls("lblRecCount").Caption = rs1.RecordCount & " Jobs: "
                    'System.Windows.Forms.Application.DoEvents()
                    ''-- SHOW REPORT..==
                    '                  report1.Show(VB6.FormShowConstants.Modal)

                    strQueryResult = gbShowQueryResults(rdr1, "Jobs", mColQueryResults)
                    '--TEMP-  DUMP into text box..
                    txtTestResults.Text = strQueryResult

                    LabReportName.Text = "Query  done.."

                    '--load report info and show--
                    clsReportPrint1 = New clsReportPrinter
                    '- clsReportPrint1.BarcodeFontName = msItemBarcodeFontName
                    '- clsReportPrint1.BarcodeFontSize = mIntItemBarcodeFontSize

                    '-  define tab columns- (ofsets from our marg)-
                    Const k_TAB_JOB As Integer = 0 : Const k_WIDTH_JOB As Integer = 60
                    Const k_TAB_JOB_STATUS As Integer = 60
                    Const k_TAB_CAT1 As Integer = 80
                    Const k_TAB_DESCR As Integer = 160
                    Const k_TAB_BARCODE As Integer = 450
                    Const k_TAB_SELL_S As Integer = 560 : Const k_WIDTH_SELL As Integer = 100
                    Const k_TAB_SELL_P As Integer = 640

                    '-- Totals-
                    Dim decServiceItemSell As Decimal = 0  '-"ServiceCharge"-
                    Dim decPartItemSell As Decimal = 0    '-"PartCost"-
                    Dim decJobHours, decJobLabourCost As Decimal  '-"ChargeableTime", "LabourCharge"-
                    Dim intJobItemCount As Integer = 0

                    Dim decJobTotalServiceItemSell As Decimal = 0
                    Dim decJobTotalPartItemSell As Decimal = 0

                    Dim decFinalTotalJobHours As Decimal = 0
                    Dim decFinalTotalLabourCost As Decimal = 0
                    Dim intFinalTotalItemCount As Integer = 0
                    Dim decFinalTotalServiceItemSell As Decimal = 0
                    Dim decFinalTotalPartItemSell As Decimal = 0

                    colReportLines = New Collection
                    '- DIG into colQueryResults and build lines..
                    '--ok. Markup and Print Real Query Results-
                    Dim colAllJobs As Collection = mColQueryResults.Item(1)
                    Dim colJobRows As Collection = colAllJobs.Item("rows")
                    Dim colJobRowData As Collection
                    '-
                    Dim colAllParts As Collection '=== mColQueryResults.Item(1)
                    Dim colPartRows As Collection   '== colAllParts.Item("rows")
                    Dim colPartRowData As Collection

                    Dim sCat1, sDescr, sStatus, sBarcode, sJobNo, sCustomer, sSell_S, sSell_P As String
                    Dim sDelivered, sPriority As String
                    Dim decLabourRate As Decimal
                    Dim bIsOnSiteJob As Boolean

                    '--Dig into all levels-
                    For Each colJob As Collection In colJobRows  '-each Job group==
                        colJobRowData = colJob("rowdata")
                        '- show Job Info-
                        sDelivered = ""
                        sDateCompleted = ""
                        sCustomer = colJobRowData.Item("Customer")("value")  '--save- for next line-
                        sJobNo = Format(CInt(colJobRowData.Item("JobNo")("value")), " ##,000")
                        sStatus = colJobRowData.Item("JobStatus")("value")  '--save- for next line-
                        '=3323.0621=
                        bIsOnSiteJob = (UCase(colJobRowData.Item("GoodsIncare")("value")) = K_GOODS_ONSITEJOB)
                        sPriority = (UCase(colJobRowData.Item("Priority")("value")) = K_GOODS_ONSITEJOB)

                        If (VB.Left(sStatus, 2) >= "70") AndAlso
                               (Not (IsDBNull(colJobRowData.Item("DateDelivered")("value")))) Then
                            sDelivered = " (Delivered)"
                        End If
                        If bIsOnSiteJob Then
                            sDelivered = " (ON-SITE Job)"
                        End If
                        If (VB.Left(sStatus, 2) >= "50") AndAlso
                                            (Not (IsDBNull(colJobRowData.Item("DateCompleted")("value")))) Then
                            sDateCompleted = Format(colJobRowData.Item("DateCompleted")("value"), "dd-MMM-yyyy")
                            sStatus = " Completed on: " & sDateCompleted
                        End If '-completed-
                        '==  decJobHours, decJobLabourCost As Decimal  '-"ChargeableTime", "LabourCharge"-
                        decJobHours = CDec(colJobRowData.Item("ChargeableTime")("value"))
                        decFinalTotalJobHours += decJobHours

                        '==3323.0621-  Re-do Labour Charge, as we now have ONSITE rates..
                        '==3323.0621-  Re-do Labour Charge, as we now have ONSITE rates..
                        '==3323.0621-  Re-do Labour Charge, as we now have ONSITE rates..
                        '=decJobLabourCost = colJobRowData.Item("LabourCharge")("value")
                        Select Case sPriority
                            Case "3"
                                decLabourRate = mCurLabourHourlyRateP3
                                If bIsOnSiteJob Then
                                    decLabourRate = mCurLabourHourlyRateOnSiteP3
                                End If
                            Case "2"
                                decLabourRate = mCurLabourHourlyRateP2
                                If bIsOnSiteJob Then
                                    decLabourRate = mCurLabourHourlyRateOnSiteP2
                                End If
                            Case Else
                                decLabourRate = mCurLabourHourlyRateP1
                                If bIsOnSiteJob Then
                                    decLabourRate = mCurLabourHourlyRateOnSiteP1
                                End If
                        End Select
                        decJobLabourCost = decLabourRate * decJobHours
                        decFinalTotalLabourCost += decJobLabourCost

                        s1 = "<textline>"
                        s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold""  align=""right""  "
                        s1 &= " width = """ & k_WIDTH_JOB & """  >" & sJobNo & "</txt>"
                        '-cust-
                        s1 &= "<txt TAB=""" & k_TAB_JOB_STATUS & """  fontStyle=""bold""  >" &
                                                          sStatus & " --" & sCustomer & sDelivered & "</txt>"
                        s1 &= "</textline>"
                        colReportLines.Add(s1)

                        '-- get parts this job..
                        colAllParts = colJob.Item("rowchild")(1)
                        colPartRows = colAllParts.Item("rows")
                        decJobTotalServiceItemSell = 0
                        decJobTotalPartItemSell = 0
                        intJobItemCount = 0
                        For Each colPart As Collection In colPartRows
                            colPartRowData = colPart("rowdata")
                            '- show Part Info-
                            sCat1 = colPartRowData.Item("RMcat1")("value")
                            sDescr = colPartRowData.Item("RMdescription")("value")
                            sBarcode = colPartRowData.Item("RMbarcode")("value")
                            decServiceItemSell = CDec(colPartRowData.Item("ServiceCharge")("value"))
                            decJobTotalServiceItemSell += decServiceItemSell
                            decFinalTotalServiceItemSell += decServiceItemSell
                            sSell_S = colPartRowData.Item("PrtService")("value")  '-PrtService 
                            '- FormatCurrency(decServiceItemSell, 2)
                            decPartItemSell = CDec(colPartRowData.Item("PartCost")("value"))
                            decJobTotalPartItemSell += decPartItemSell
                            decFinalTotalPartItemSell += decPartItemSell
                            sSell_P = colPartRowData.Item("PrtPart")("value")   '-PrtPart 
                            '-FormatCurrency(decPartItemSell, 2)
                            s1 = "<textline>"
                            '-part info-
                            s1 &= "<txt TAB=""" & k_TAB_CAT1 & """  >" & sCat1 & "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_DESCR & """  >" & sDescr & "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_BARCODE & """  >" & sBarcode & "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_SELL_S & """   align=""right"" " &
                                            "  width = """ & k_WIDTH_SELL & """    >" & sSell_S & "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_SELL_P & """   align=""right"" " &
                                            "  width = """ & k_WIDTH_SELL & """    >" & sSell_P & "</txt>"
                            s1 &= "</textline>"
                            colReportLines.Add(s1)
                            intJobItemCount += 1
                            intFinalTotalItemCount += 1
                        Next colPart

                        '- total sell this job.
                        sSell_S = FormatCurrency(decJobTotalServiceItemSell, 2)
                        sSell_P = FormatCurrency(decJobTotalPartItemSell, 2)
                        s1 = "<textline>"
                        s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >" &
                                "Totals this Job: " & "-   Labour Hours: " & Format(decJobHours, "  0.00") &
                                "-   Labour Cost: " & Format(decJobLabourCost, "  0.00") &
                                  "        (" & intJobItemCount & " items): "
                        s1 &= "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_SELL_S & """  fontStyle=""bold""  align=""right"" "
                        s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_S & "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_SELL_P & """  fontStyle=""bold""  align=""right"" "
                        s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_P & "</txt>"
                        s1 &= "</textline>"
                        colReportLines.Add(s1)
                        s1 = "<drawline fontstyle=""bold"" />"
                        colReportLines.Add(s1)
                    Next colJob

                    '- Final totals.
                    sSell_S = FormatCurrency(decFinalTotalServiceItemSell, 2)
                    sSell_P = FormatCurrency(decFinalTotalPartItemSell, 2)
                    s1 = "<textline>"
                    s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >" &
                            "FINAL TOTALS-  " & colJobRows.Count & " Jobs:     Labour Hours: " &
                                                       Format(decFinalTotalJobHours, "  0.00") &
                            "-   Labour Cost: " & Format(decFinalTotalLabourCost, "  0.00") &
                              "         (Total" & intFinalTotalItemCount & " items): "
                    s1 &= "</txt>"
                    s1 &= "<txt TAB=""" & k_TAB_SELL_S & """  fontStyle=""bold""  align=""right"" "
                    s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_S & "</txt>"
                    s1 &= "<txt TAB=""" & k_TAB_SELL_P & """  fontStyle=""bold""  align=""right"" "
                    s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_P & "</txt>"
                    s1 &= "</textline>"
                    colReportLines.Add(s1)
                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)

                    Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msProgramVersion, colReportLines, msSelectedPrinterName,
                                                              msBusinessName, "Jobs/Job Cost Report",
                                                               Color.DarkMagenta,
                                                               "Shows Jobs with Labour and Stock items " &
                                                               "assigned to Jobs " & LabReportStatus.Text & vbCrLf &
                                                               strLabourRates, sReportPeriod,
                                                         " Job- -Status- -Customer-    Item Description" & Space(24) &
                                                                   " Stock barcode.  Service-Items   Stock-Items  ")

                    System.Windows.Forms.Application.DoEvents()
                End If '-bof. empty-
                '==rs1.Close
            Else
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get SHAPEd recordset..", MsgBoxStyle.Exclamation)
            End If '--nothing- get r-set..--
            LabReportName.Text = " Ready..."
            '== Unload(report1)
            System.Windows.Forms.Application.DoEvents()
        ElseIf idxReport = 1 Then
            '-- Parts/Jobs..--
            LabReportName.Text = " Loading Parts Report Form.."
            System.Windows.Forms.Application.DoEvents()
            '==Load rptPartsJobs
            If (Not bShowDetail) Then '--wants summary.-
                '=3301.828= report1 = New rptPartsSum
            Else '--detail-
                '=3301.828= report1 = New rptPartsJobs
            End If '-summary.-
            '--- Now with CAT1 from v1.4 ==
            '--  ok..  now get r/set for report..-
            sBarcodeSql = " RMBarcode "
            If sBarcodeFont <> "" Then '--using barcode..
                sBarcodeSql = " RMBarcode= '*'+RMBarcode+'*' " '--add asterisks for b/c font..-
            End If
            '--  need to make a temp table of DISTINCTIVE stock id's in job-parts..--
            sShapeSql = " SELECT DISTINCT  RMStock_id AS RMTempStockId, RMCat1 as TempCat1, " & sBarcodeSql
            sShapeSql = sShapeSql & ",  RMDescription AS RMTempDescr "
            sShapeSql = sShapeSql & "  FROM [jobParts] WHERE PartJob_id IN "
            sShapeSql = sShapeSql & " (SELECT Job_id FROM [Jobs] " & sWhere & ") "
            sShapeSql = sShapeSql & sWhereWarranty & "  ORDER BY RMTempStockId; "

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If gbGetDataTable(mCnnShape, rs1, sShapeSql) Then '= gbGetRst(mCnnShape, rs1, sShapeSql) Then
                '-- scan parent recordset..
                '---- build array of counts of rows in child rset..--
                If (rs1 Is Nothing) Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If '--nothing-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                If Not gbMakeTempTable(mCnnShape, "#TempJobStock", rs1, "RMTempStockId") Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("Failed to make temp report table..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If '--get rst..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            '===sShapeSql = "SHAPE {SELECT DISTINCT  RMStock_id, " & sBarcodeSql & _
            ''===                "  FROM [jobParts] WHERE PartJob_id IN " + _
            ''===                " (SELECT Job_id FROM [Jobs] " + sWhere + ") " + _
            ''===                   sWhereWarranty + " ORDER BY RMStock_id } "
            sShapeSql = "SHAPE {SELECT  RMTempStockId, TempCat1 AS Cat1, RMBarcode, RMTempDescr  "
            sShapeSql = sShapeSql & "  FROM #TempJobStock  "
            sShapeSql = sShapeSql & "     ORDER BY TempCat1, RMTempDescr } "
            sShapeSql = sShapeSql & "APPEND ({ SELECT JP.RMCat1, JP.RMDescription, JP.PartSerialNumber, JP.RMSell, "
            sShapeSql = sShapeSql & "PartJob_id AS JobNo, JP.RMStock_id, Jobs.DateCreated, Jobs.JobStatus, "
            sShapeSql = sShapeSql & " Wrnty=(CASE IsWarrantyPart WHEN 'Y' THEN 'Yes' ELSE '' END), "
            sShapeSql = sShapeSql & " Customer=(CASE jobs.CustomerCompany WHEN 'n/a' THEN CustomerName  "
            sShapeSql = sShapeSql & "                                  WHEN '--' THEN CustomerName "
            sShapeSql = sShapeSql & "                                  WHEN ''   THEN CustomerName "
            sShapeSql = sShapeSql & "            ELSE jobs.CustomerCompany  END ),"
            sShapeSql = sShapeSql & " jobs.CustomerBarcode AS CustBarcode, Jobs.job_id  "
            sShapeSql = sShapeSql & " FROM JobParts JP JOIN Jobs ON (PartJob_id = Job_id) " & sWhere & sWhereWarranty
            sShapeSql = sShapeSql & "  ORDER BY JobNo ASC} "
            sShapeSql = sShapeSql & "AS chapParts "
            sShapeSql = sShapeSql & "RELATE RMTempStockId to RMStock_id )"

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '-- start retrieval-
            Try
                '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
                cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
                rdr1 = cmd1.ExecuteReader
            Catch ex As Exception
                MsgBox("Error getting Parts/Jobs recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End Try
            If (Not (rdr1 Is Nothing)) Then  '= gbGetRst(mCnnShape, rs1, sShapeSql) Then
                '-- scan parent recordset..
                '---- build array of counts of rows in child rset..--
                'If (rs1 Is Nothing) Then
                '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '    Exit Sub
                'End If '--nothing-
                '--  check supports..-
                '-- If rs1.Supports(adAddNew) Then MsgBox "AddNew OK." Else MsgBox "No AddNew allowed.."
                '--  save all fld names..--
                'For fx = 1 To rs1.Fields.Count
                '    If rs1.Fields(fx - 1).Type <> ADODB.DataTypeEnum.adChapter Then '--don't save chapter cols..-
                '    End If '--chap.-
                'Next fx
                If (Not rdr1.HasRows) Then '= (rs1.BOF And rs1.EOF) Then '--not empty..-
                    MsgBox("No rows to show..", MsgBoxStyle.Information)
                    '= rs1 = Nothing
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                Else '--not empty..-
                    '= lngParentRows = rs1.RecordCount
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '--Bind the recordset to the report..-
                    '= report1.DataSource = rs1
                    '== report1.DataMember = rs1.DataMember
                    '-- set Barcode font.-
                    If (sBarcodeFont <> "") Then
                        '== report1.Sections("Section6").Controls("txtItemBarcode").Font.Name = sBarcodeFont '-- "IDAutomationHC39M"
                        '-- Update the lblRecCount control on the report (Page Footer Section) to show RecordCount
                    End If
                    '== report1.Sections("Section5").Controls("lblRecCount").Caption = rs1.RecordCount & " Stock items listed.."
                    System.Windows.Forms.Application.DoEvents()

                    '-- SHOW REPORT..==
                    '-- SHOW REPORT..==
                    '== report1.Show(VB6.FormShowConstants.Modal)

                    strQueryResult = gbShowQueryResults(rdr1, "#TempJobStock", mColQueryResults)
                    '--TEMP-  DUMP into text box..
                    txtTestResults.Text = strQueryResult

                    LabReportName.Text = "Query done.."
                    '--load report info and show--
                    clsReportPrint1 = New clsReportPrinter
                    clsReportPrint1.BarcodeFontName = msItemBarcodeFontName
                    clsReportPrint1.BarcodeFontSize = mIntItemBarcodeFontSize

                    '-  define tab columns- (ofsets from our marg-
                    Const k_TAB_CAT1 As Integer = 0
                    Const k_TAB_DESCR As Integer = 60
                    Const k_TAB_STOCK_ID As Integer = 460
                    Const k_TAB_BARCODE As Integer = 520
                    Const k_TAB_JOB As Integer = 280 : Const k_WIDTH_JOB As Integer = 60
                    Const k_TAB_CUST As Integer = 360   '= : Const k_WIDTH_HRS_CHG As Integer = 72
                    '= Const k_TAB_QTY As Integer = 432 : Const k_WIDTH_QTY As Integer = 100
                    Const k_TAB_SELL As Integer = 560 : Const k_WIDTH_SELL As Integer = 120

                    Dim decItemSell As Decimal = 0
                    Dim decItemTotalSell As Decimal = 0
                    Dim decFinalTotalSell As Decimal = 0

                    colReportLines = New Collection

                    '--ok. Markup and Print Real Query Results-
                    Dim colAllParts As Collection = mColQueryResults.Item(1)
                    Dim colPartRows As Collection = colAllParts.Item("rows")
                    Dim colPartRowData As Collection
                    Dim colAllJobs, colJobRows As Collection
                    'MsgBox("TEST- base collection Name='" & _
                    '            colAllParts.Item("ChapterName") & "' Count=" & colAllParts.Count & vbCrLf & _
                    '            "colPartRows count=" & colPartRows.Count, MsgBoxStyle.Information)
                    '-- each day has a row-
                    Dim colJob, colJobRowData As Collection
                    '- Jobs in day.
                    '= Dim colAllSessions, colSessionRows, colSession, colSessionData As Collection
                    Dim sCat1, sDescr, sStockId, sBarcode, sJobNo, sCustomer, sSell As String
                    '--Dig into all levels-
                    For Each colPart As Collection In colPartRows  '-each PART group==
                        '-restart tech totals-
                        decItemTotalSell = 0
                        colPartRowData = colPart.Item("rowdata")
                        sCat1 = colPartRowData.Item("cat1")("value")
                        '-RMTempDescr, RMBarcode --
                        sDescr = colPartRowData.Item("RMTempDescr")("value")
                        sStockId = CStr(colPartRowData.Item("RMTempStockId")("value"))
                        sBarcode = colPartRowData.Item("RMBarcode")("value")
                        s1 = "<textline><txt fontstyle=""bold"">" & sCat1 & "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_DESCR & """ fontStyle=""bold"" >" & sDescr & "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_STOCK_ID & """ fontStyle=""bold"" >" & sStockId & "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_BARCODE & """ fontStyle=""barcode"" >" & sBarcode & "</txt>"
                        s1 &= "</textline>"
                        colReportLines.Add(s1)

                        '--  get all Jobs that have this part..
                        '-- Now show all Jobs and sesssions for day..
                        '=colRoot = colDay.Item("rowdata")
                        colAllJobs = colPart.Item("rowchild")(1)
                        colJobRows = colAllJobs.Item("rows")
                        decItemTotalSell = 0
                        For Each colJob In colJobRows
                            colJobRowData = colJob("rowdata")
                            '- show Job Info-
                            sCustomer = colJobRowData.Item("Customer")("value")  '--save- for next line-
                            sJobNo = Format(CInt(colJobRowData.Item("JobNo")("value")), " ##,000")
                            decItemSell = CDec(colJobRowData.Item("RMSell")("value"))
                            decItemTotalSell += decItemSell
                            decFinalTotalSell += decItemSell
                            sSell = FormatCurrency(decItemSell, 2)
                            s1 = "<textline>"
                            s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold""  align=""right""  "
                            s1 &= " width = """ & k_WIDTH_JOB & """  >" & sJobNo & "</txt>"
                            '-cust-
                            s1 &= "<txt TAB=""" & k_TAB_CUST & """  >" & sCustomer & "</txt>"
                            '-sell-
                            s1 &= "<txt TAB=""" & k_TAB_SELL & """  align=""right""  "
                            s1 &= " width = """ & k_WIDTH_SELL & """  >" & sSell & "</txt>"
                            s1 &= "</textline>"
                            If bShowDetail Then
                                colReportLines.Add(s1)
                            End If
                        Next colJob
                        '- show total sell for part. (all jobs).
                        sSell = FormatCurrency(decItemTotalSell, 2)
                        s1 = "<textline>"
                        s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >Total (" & colJobRows.Count & " items): "
                        s1 &= "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_SELL & """  fontStyle=""bold""  align=""right"" "
                        s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell & "</txt>"
                        s1 &= "</textline>"
                        colReportLines.Add(s1)

                        s1 = "<drawline fontstyle=""bold"" />"
                        colReportLines.Add(s1)
                    Next colPart

                    '- show FINAL total sell for all parts. (all jobs).
                    sSell = FormatCurrency(decFinalTotalSell, 2)
                    s1 = "<textline>"
                    s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >Final Total All Jobs: "
                    s1 &= "</txt>"
                    s1 &= "<txt TAB=""" & k_TAB_SELL & """  fontStyle=""bold"" "
                    s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell & "</txt>"
                    s1 &= "</textline>"
                    colReportLines.Add(s1)

                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)

                    '- DO the REPORT--
                    Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msProgramVersion, colReportLines, msSelectedPrinterName,
                                         msBusinessName, "Stock/Jobs Report",
                                          Color.Firebrick,
                                          "Shows all Stock Parts " &
                                          "attached to Jobs " & LabReportStatus.Text, sReportPeriod,
                    "-Cat1-    -Description- " & Space(26) & "  -JobNo-Customer-             -Stock-Id/Barcode-       -Sell-")

                    System.Windows.Forms.Application.DoEvents()
                End If '-bof. empty-
                '==rs1.Close
            Else '-nothing- 
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get SHAPEd recordset..", MsgBoxStyle.Exclamation)
            End If '--get r-set.. --
            '== Unload(report1)
        ElseIf idxReport = 2 Then  '-- staff/Jobs..--
            '--load code for sql function 'JT2_ChargeableHours'..--
            Call mbSetupSqlChargeableHoursFunction()
            '--load code for sql function 'JT2_ChargeableHours'..--
            '-- Staff/Jobs/Parts..--
            '--  can be detail report or Summary..--
            LabReportName.Text = " Loading Staff Report Form.."
            System.Windows.Forms.Application.DoEvents()
            '==If (chkStaffSummary.Value = 1) Then '--wants summary.-
            If (Not bShowDetail) Then '--wants summary.-
                '=3301.828= rptStaff = New rptStaffSum
            Else
                '=3301.828= rptStaff = New rptStaffJobs
            End If '-summary.-
            '===Load rptStaffJobs
            '===LabReportName.Caption = "Jobs/Parts List- " & LabReportName.Caption
            sShapeSql = " SHAPE {SELECT DISTINCT "
            sShapeSql = sShapeSql & "  StaffName =(CASE NominatedTech "
            sShapeSql = sShapeSql & "             WHEN 'n/a' THEN TechStaffName  "
            sShapeSql = sShapeSql & "             WHEN ''    THEN TechStaffName ELSE NominatedTech END) "
            sShapeSql = sShapeSql & " FROM Jobs " & sWhere & " ORDER BY StaffName } "
            sShapeSql = sShapeSql & " APPEND ("
            '--inner shape--
            sShapeSql = sShapeSql & " ( SHAPE {SELECT job_id AS JobNo, DateUpdated,JobStatus, DateCompleted, Priority, GoodsInCare, "
            sShapeSql = sShapeSql & "  StaffName =(CASE NominatedTech "
            sShapeSql = sShapeSql & "             WHEN 'n/a' THEN TechStaffName  "
            sShapeSql = sShapeSql & "             WHEN ''    THEN TechStaffName ELSE NominatedTech END), "
            sShapeSql = sShapeSql & "    Customer=(CASE CustomerCompany WHEN 'n/a' THEN CustomerName "
            sShapeSql = sShapeSql & "                                   WHEN '--'  THEN CustomerName "
            sShapeSql = sShapeSql & "                                   WHEN ''    THEN CustomerName "
            sShapeSql = sShapeSql & "              ELSE CustomerCompany  END ), "
            sShapeSql = sShapeSql & "   CustomerBarcode AS CustBarcode, TotalServiceTime AS TotalTime, SessionTimes, "
            sShapeSql = sShapeSql & "   ChargeableTime=dbo.JT2_ChargeableHours(SessionTimes), "
            '===sShapeSql = sShapeSql & "   LabourCharge=(dbo.JT2_ChargeableHours(SessionTimes) *100)  "
            sShapeSql = sShapeSql & "  LabourCharge= (CASE Priority "
            sShapeSql = sShapeSql & "    WHEN '3' THEN (dbo.JT2_ChargeableHours(SessionTimes) *" & CStr(mCurLabourHourlyRateP3) & ") "
            sShapeSql = sShapeSql & "    WHEN '2' THEN (dbo.JT2_ChargeableHours(SessionTimes) *" & CStr(mCurLabourHourlyRateP2) & ") "
            sShapeSql = sShapeSql & "    ELSE (dbo.JT2_ChargeableHours(SessionTimes) *" & CStr(mCurLabourHourlyRateP1) & ") END) "

            sShapeSql = sShapeSql & " FROM Jobs  " & sWhere & "  ORDER BY job_id ASC } "
            sShapeSql = sShapeSql & " APPEND ({SELECT RMCat1, RMdescription,RMBarcode, PartJob_id  AS JobNo, " & sCostSelectSql
            sShapeSql = sShapeSql & "  FROM [jobParts] ORDER BY RMCat1, RMdescription } "
            sShapeSql = sShapeSql & " AS chapParts "
            sShapeSql = sShapeSql & " RELATE JobNo TO JobNo ) )" '--end of inner shape/append..-
            '--end of inner shape--
            sShapeSql = sShapeSql & " AS chapJobs "
            sShapeSql = sShapeSql & " RELATE StaffName TO StaffName ) " '--end of outer append..-

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '-- start retrieval-
            Try
                '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
                cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
                rdr1 = cmd1.ExecuteReader
            Catch ex As Exception
                MsgBox("Error getting Staff/Jobs recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End Try

            If (Not (rdr1 Is Nothing)) Then  '=  gbGetRst(mCnnShape, rs1, sShapeSql) Then
                '-- scan parent recordset..
                '---- build array of counts of rows in child rset..--
                'If (rs1 Is Nothing) Then
                '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '    Exit Sub
                'End If '--nothing-
                ''--  check supports..-
                '-- If rs1.Supports(adAddNew) Then MsgBox "AddNew OK." Else MsgBox "No AddNew allowed.."
                '--  save all fld names..--
                'For fx = 1 To rs1.Fields.Count
                '    If rs1.Fields(fx - 1).Type <> ADODB.DataTypeEnum.adChapter Then '--don't save chapter cols..-
                '    End If '--chap.-
                'Next fx
                If (Not rdr1.HasRows) Then '=  (rs1.BOF And rs1.EOF) Then '--not empty..-
                    MsgBox("No rows to show..", MsgBoxStyle.Information)
                    '= rs1 = Nothing
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                Else '--not empty..-
                    '= rs1.MoveFirst()
                    '= rs1.MoveLast()
                    '= lngParentRows = rs1.RecordCount
                    'If (lngParentRows <= 0) Then
                    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '    Exit Sub
                    'Else '--has parent rows..-
                    '    '--DISCONNECT the r/set.-
                    '= rs1.ActiveConnection = Nothing

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    'System.Windows.Forms.Application.DoEvents()
                    ''-- SHOW REPORT..==
                    '                  rptStaff.Show(VB6.FormShowConstants.Modal)
                    strQueryResult = gbShowQueryResults(rdr1, "Jobs", mColQueryResults)
                    '--TEMP-  DUMP into text box..
                    txtTestResults.Text = strQueryResult

                    '=LabReportName.Text = " done.."
                    LabReportName.Text = "Query  done.."

                    '--load report info and show--
                    clsReportPrint1 = New clsReportPrinter

                    '-- Totals -
                    Dim decTechTotalJobHours As Decimal = 0
                    Dim decTechTotalLabourCost As Decimal = 0
                    Dim intTechTotalJobCount As Integer = 0
                    Dim decTechTotalServiceItemSell As Decimal = 0
                    Dim decTechTotalPartItemSell As Decimal = 0

                    '--Job Totals-
                    Dim decServiceItemSell As Decimal = 0  '-"ServiceCharge"-
                    Dim decPartItemSell As Decimal = 0    '-"PartCost"-
                    Dim decJobHours, decJobLabourCost As Decimal  '-"ChargeableTime", "LabourCharge"-
                    Dim intJobItemCount As Integer = 0

                    Dim decJobTotalServiceItemSell As Decimal = 0
                    Dim decJobTotalPartItemSell As Decimal = 0

                    Dim decFinalTotalJobHours As Decimal = 0
                    Dim decFinalTotalLabourCost As Decimal = 0
                    Dim intFinalTotalJobCount As Integer = 0
                    Dim decFinalTotalServiceItemSell As Decimal = 0
                    Dim decFinalTotalPartItemSell As Decimal = 0
                    '-
                    Dim decThisHoursChg As Decimal
                    Dim decThisHoursNC As Decimal
                    Dim decThisCost As Decimal

                    '-- PREVIEW/PRINT --
                    '-  define tab columns-
                    Const k_TAB_TECH As Integer = 0
                    '-  define tab columns- (ofsets from our marg)-
                    Const k_TAB_JOB As Integer = 100 : Const k_WIDTH_JOB As Integer = 60
                    Const k_TAB_JOB_STATUS As Integer = 200
                    Const k_TAB_CAT1 As Integer = 80
                    Const k_TAB_DESCR As Integer = 160
                    Const k_TAB_BARCODE As Integer = 450
                    Const k_TAB_SELL_S As Integer = 560 : Const k_WIDTH_SELL As Integer = 100
                    Const k_TAB_SELL_P As Integer = 640

                    colReportLines = New Collection

                    '--ok. Markup and Print Real Query Results-
                    Dim colAllTechs As Collection = mColQueryResults.Item(1)
                    Dim colTechRows As Collection = colAllTechs.Item("rows")

                    '-- each tech has a row-
                    Dim colRowData As Collection
                    '-- each day has a row-
                    Dim colDayRowData As Collection
                    '- Jobs in day.
                    Dim colAllJobs, colJobRows, colJob, colJobRowData As Collection
                    Dim colAllParts As Collection '=== mColQueryResults.Item(1)
                    Dim colPartRows As Collection   '== colAllParts.Item("rows")
                    Dim colPartRowData As Collection
                    '--Dig into all levels-
                    '--Dig into all levels-
                    '--Dig into all levels-
                    Dim sStaffname, sCat1, sDescr, sStatus, sBarcode, sJobNo, sCustomer, sSell_S, sSell_P As String
                    Dim sPriority, sStatusMsg As String
                    Dim bIsOnSiteJob As Boolean
                    Dim decLabourRate As Decimal

                    For Each colTech As Collection In colTechRows  '-each tech group==
                        '-restart tech totals-
                        decTechTotalJobHours = 0
                        intTechTotalJobCount = 0
                        decTechTotalLabourCost = 0
                        decTechTotalServiceItemSell = 0
                        decTechTotalPartItemSell = 0

                        colRowData = colTech.Item("rowdata")
                        sStaffname = colRowData.Item("staffName")("value")
                        '=s2 = colRowData.Item("techName")("value")
                        s1 = "<textline><txt fontstyle=""big"">" & sStaffname & "</txt></textline>"
                        colReportLines.Add(s1)
                        '- get all JOBS this tech..
                        colAllJobs = colTech.Item("rowchild")(1)
                        colJobRows = colAllJobs.Item("rows")

                        '--Dig into all Jobs-
                        For Each colJob In colJobRows  '-each Job group==
                            colJobRowData = colJob("rowdata")
                            '- show Job Info-
                            sCustomer = colJobRowData.Item("Customer")("value")  '--save- for next line-
                            sJobNo = Format(CInt(colJobRowData.Item("JobNo")("value")), " ##,000")
                            sStatus = colJobRowData.Item("JobStatus")("value")  '--save- for next line-
                            sStatusMsg = sStatus
                            If (VB.Left(sStatus, 2) >= "50") AndAlso
                                                (Not (IsDBNull(colJobRowData.Item("DateCompleted")("value")))) Then
                                sDateCompleted = Format(colJobRowData.Item("DateCompleted")("value"), "dd-MMM-yyyy")
                                sStatusMsg = "Completed on: " & sDateCompleted
                            End If '-completed-
                            '=3323.0621=
                            bIsOnSiteJob = (UCase(colJobRowData.Item("GoodsIncare")("value")) = K_GOODS_ONSITEJOB)
                            sPriority = colJobRowData.Item("Priority")("value")

                            '==  decJobHours, decJobLabourCost As Decimal  '-"ChargeableTime", "LabourCharge"-
                            decJobHours = colJobRowData.Item("ChargeableTime")("value")
                            decTechTotalJobHours += decJobHours
                            decFinalTotalJobHours += decJobHours

                            '==3323.0621- RE-calc.. labour charge..
                            '=  ---   decJobLabourCost = colJobRowData.Item("LabourCharge")("value")
                            '==3323.0621-  Re-do Labour Charge, as we now have ONSITE rates..
                            '==3323.0621-  Re-do Labour Charge, as we now have ONSITE rates..
                            '=decJobLabourCost = colJobRowData.Item("LabourCharge")("value")
                            Select Case sPriority
                                Case "3"
                                    decLabourRate = mCurLabourHourlyRateP3
                                    If bIsOnSiteJob Then
                                        decLabourRate = mCurLabourHourlyRateOnSiteP3
                                    End If
                                Case "2"
                                    decLabourRate = mCurLabourHourlyRateP2
                                    If bIsOnSiteJob Then
                                        decLabourRate = mCurLabourHourlyRateOnSiteP2
                                    End If
                                Case Else
                                    decLabourRate = mCurLabourHourlyRateP1
                                    If bIsOnSiteJob Then
                                        decLabourRate = mCurLabourHourlyRateOnSiteP1
                                    End If
                            End Select
                            decJobLabourCost = decLabourRate * decJobHours

                            decTechTotalLabourCost += decJobLabourCost
                            decFinalTotalLabourCost += decJobLabourCost
                            intTechTotalJobCount += 1

                            s1 = "<textline><txt>" & sStaffname & ": Job #</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold""  align=""right""  "
                            s1 &= " width = """ & k_WIDTH_JOB & """  >" & sJobNo & "</txt>"
                            '-cust-
                            s1 &= "<txt TAB=""" & k_TAB_JOB_STATUS & """  fontStyle=""bold""  >" &
                                                              sStatusMsg & "  " & sCustomer & "</txt>"
                            s1 &= "</textline>"
                            colReportLines.Add(s1)

                            '-- get parts this job..
                            colAllParts = colJob.Item("rowchild")(1)
                            colPartRows = colAllParts.Item("rows")
                            decJobTotalServiceItemSell = 0
                            decJobTotalPartItemSell = 0
                            intJobItemCount = 0
                            For Each colPart As Collection In colPartRows
                                colPartRowData = colPart("rowdata")
                                '- show Part Info-
                                sCat1 = colPartRowData.Item("RMcat1")("value")
                                sDescr = colPartRowData.Item("RMdescription")("value")
                                sBarcode = colPartRowData.Item("RMbarcode")("value")
                                decServiceItemSell = CDec(colPartRowData.Item("ServiceCharge")("value"))
                                decJobTotalServiceItemSell += decServiceItemSell
                                decTechTotalServiceItemSell += decServiceItemSell
                                decFinalTotalServiceItemSell += decServiceItemSell
                                sSell_S = colPartRowData.Item("PrtService")("value")  '-PrtService 
                                '- FormatCurrency(decServiceItemSell, 2)
                                decPartItemSell = CDec(colPartRowData.Item("PartCost")("value"))
                                decJobTotalPartItemSell += decPartItemSell
                                decTechTotalPartItemSell += decPartItemSell
                                decFinalTotalPartItemSell += decPartItemSell
                                sSell_P = colPartRowData.Item("PrtPart")("value")   '-PrtPart 
                                '-FormatCurrency(decPartItemSell, 2)
                                s1 = "<textline>"
                                '-part info-
                                s1 &= "<txt TAB=""" & k_TAB_CAT1 & """  >" & sCat1 & "</txt>"
                                s1 &= "<txt TAB=""" & k_TAB_DESCR & """  >" & sDescr & "</txt>"
                                s1 &= "<txt TAB=""" & k_TAB_BARCODE & """  >" & sBarcode & "</txt>"
                                s1 &= "<txt TAB=""" & k_TAB_SELL_S & """   align=""right"" " &
                                                "  width = """ & k_WIDTH_SELL & """    >" & sSell_S & "</txt>"
                                s1 &= "<txt TAB=""" & k_TAB_SELL_P & """   align=""right"" " &
                                                "  width = """ & k_WIDTH_SELL & """    >" & sSell_P & "</txt>"
                                s1 &= "</textline>"
                                colReportLines.Add(s1)
                                intJobItemCount += 1
                                '=ntFinalTotalItemCount += 1
                            Next colPart

                            '- total sell this job.
                            sSell_S = FormatCurrency(decJobTotalServiceItemSell, 2)
                            sSell_P = FormatCurrency(decJobTotalPartItemSell, 2)
                            s1 = "<textline>"
                            s1 &= "<txt fontStyle=""bold"" >" &
                                    "Total for Job: " & sJobNo & "-   Labour Hours: " & Format(decJobHours, "  0.00") &
                                    "-   Labour Cost: " & Format(decJobLabourCost, "  0.00") &
                                      " (" & intJobItemCount & " items): "
                            s1 &= "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_SELL_S & """  fontStyle=""bold""  align=""right"" "
                            s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_S & "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_SELL_P & """  fontStyle=""bold""  align=""right"" "
                            s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_P & "</txt>"
                            s1 &= "</textline>"
                            colReportLines.Add(s1)
                            s1 = "<drawline fontstyle=""bold"" />"
                            colReportLines.Add(s1)
                        Next colJob

                        '-- Tech totals.-
                        sSell_S = FormatCurrency(decTechTotalServiceItemSell, 2)
                        sSell_P = FormatCurrency(decTechTotalPartItemSell, 2)
                        s1 = "<textline><txt fontStyle=""bold"" >Total for TECH: " & sStaffname & " </txt> "
                        s1 = "</textline>"
                        s1 = "<textline>"
                        s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >" &
                                "Total for TECH " & sStaffname & ": " & intTechTotalJobCount & " Jobs;  Labour Hrs: " &
                                 Format(decTechTotalJobHours, "  0.00") &
                                "-   Labour Cost: " & Format(decTechTotalLabourCost, "  0.00")
                        s1 &= "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_SELL_S & """  fontStyle=""bold""  align=""right"" "
                        s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_S & "</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_SELL_P & """  fontStyle=""bold""  align=""right"" "
                        s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_P & "</txt>"
                        s1 &= "</textline>"
                        colReportLines.Add(s1)
                        s1 = "<drawline fontstyle=""bold"" />"
                        colReportLines.Add(s1)
                        s1 = "<drawline fontstyle=""bold"" />"
                        colReportLines.Add(s1)
                    Next colTech
                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)

                    '-- all techs done.
                    '-- print Final Totals-
                    sSell_S = FormatCurrency(decFinalTotalServiceItemSell, 2)
                    sSell_P = FormatCurrency(decFinalTotalPartItemSell, 2)
                    '-print this TECH total hours/cost-
                    s1 = "<textline>"
                    s1 &= "<txt fontStyle=""big"" >FINAL Total All Techs, All days:</txt>"
                    s1 = "</textline>"
                    s1 = "<textline>"
                    s1 &= "<txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >" &
                           "FINAL TOTALS-  " & intFinalTotalJobCount & " Jobs:   Labour Hours: " & Format(decFinalTotalJobHours, "  0.00") &
                           "-   Labour Cost: " & Format(decFinalTotalLabourCost, "  0.00")
                    s1 &= "</txt>"
                    s1 &= "<txt TAB=""" & k_TAB_SELL_S & """  fontStyle=""bold""  align=""right"" "
                    s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_S & "</txt>"
                    s1 &= "<txt TAB=""" & k_TAB_SELL_P & """  fontStyle=""bold""  align=""right"" "
                    s1 &= "  width = """ & k_WIDTH_SELL & """   >" & sSell_P & "</txt>"
                    s1 &= "</textline>"
                    colReportLines.Add(s1)
                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)
                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)

                    '= Dim clsReportPrint1 = New clsReportPrinter
                    Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msProgramVersion, colReportLines, msSelectedPrinterName,
                                                             msBusinessName, "Staff Jobs and Job Cost Report",
                                                              Color.DarkOliveGreen,
                                                              "Shows (for each Tech) the Jobs/Parts " &
                                                              "logged " & LabReportStatus.Text & vbCrLf &
                                                              strLabourRates, sReportPeriod,
                                                        "Techname -Job-  -Customer-    Item Description" & Space(24) &
                                                                             " Stock barcode.  Service-Items   Stock-Items  ")

                    '= End If '--parent rows...-
                End If '-bof. empty-
            Else
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get Staff/Jobs SHAPEd recordset..", MsgBoxStyle.Exclamation)
            End If '--get r-set..--
            'UPGRADE_ISSUE: Unload rptStaff was not upgraded. Click for more: 
            '== ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="875EBAD7-D704-4539-9969-BC7DBDAA62A2"'
            '== Unload(rptStaff)
            '-- Daily timesheets...--
        ElseIf idxReport = 3 Then  '-- Daily timesheets...--
            LabReportName.Text = " Loading Staff Timesheets data.."
            If gbQueryWorkSessions(mCnnSql, mCnnShape, date1, date2,
                                      mCurLabourHourlyRateP1, mCurLabourHourlyRateP2, mCurLabourHourlyRateP3,
                                      msDescriptionPriority1, msDescriptionPriority2, msDescriptionPriority3, sShapeSql) Then '--ok- Got SQL--
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                '-- start retrieval-
                Try
                    '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
                    cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
                    rdr1 = cmd1.ExecuteReader
                Catch ex As Exception
                    MsgBox("Error getting Parts/Jobs recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End Try

                If (Not (rdr1 Is Nothing)) Then  '=  gbGetRst(mCnnShape, rs1, sShapeSql) Then
                Else
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("Failed to get Timesheets SHAPEd recordset..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If '--get rs-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                'If gbDebug Then
                '    '--  show rs1 results.-
                '    sQueryResult = gbShowQueryResults(rdr1)
                '    '--  copy text result to clopboard.--
                '    My.Computer.Clipboard.Clear()
                '    My.Computer.Clipboard.SetText(sQueryResult)
                '    MsgBox("Staff Timesheets:" & vbCrLf & "  Results are on the clipboard..", MsgBoxStyle.Information)
                'End If '--debug-
                If (Not rdr1.HasRows) Then '=   (rs1.BOF And rs1.EOF) Then '-- empty..-
                    MsgBox("No rows to show..", MsgBoxStyle.Information)
                    '= rs1 = Nothing
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Sub
                Else '--not empty..-
                    'rs1.MoveFirst()
                    'rs1.MoveLast()
                    '= lngParentRows = rs1.RecordCount
                    'report1 = New rptTimesheets
                    '                  report1.Sections("Report_Header").Controls("labCompany").Caption = msBusinessName '== "Precise PCs ."
                    '                  report1.Sections("Page_Header").Controls("labReportStatus").Caption = sReportPeriod
                    '                  report1.Sections("Page_Footer").Controls("labVersion").Caption = "JobMatix JobReports Ver: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
                    '                  report1.Sections("Page_Footer").Controls("labRates").Caption = strLabourRates

                    '--DISCONNECT the r/set.-
                    '= rs1.ActiveConnection = Nothing
                    '== report1.DataSource = rs1
                    '== report1.DataMember = rs1.DataMember

                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '--Bind the recordset to the report..-
                    '-- Update the lblRecCount control on the report (Page Footer Section) to show RecordCount
                    '== rptStaff.Sections("Section5").Controls("lblRecCount").Caption = rs1.RecordCount & " staff listed"
                    System.Windows.Forms.Application.DoEvents()
                    '-- SHOW REPORT..==
                    '== report1.Show(VB6.FormShowConstants.Modal)

                    strQueryResult = gbShowQueryResults(rdr1, "TechSessions", mColQueryResults)
                    '--TEMP-  DUMP into text box..
                    txtTestResults.Text = strQueryResult
                    LabReportName.Text = " done.."
                    clsReportPrint1 = New clsReportPrinter

                    '-- Totals -
                    Dim decDayTotalHoursCharged As Decimal = 0
                    Dim decDayTotalHoursNC As Decimal = 0
                    Dim decDayTotalCost As Decimal = 0
                    Dim decTechTotalHoursCharged As Decimal = 0
                    Dim decTechTotalHoursNC As Decimal = 0
                    Dim decTechTotalCost As Decimal = 0
                    Dim decFinalTotalHoursCharged As Decimal = 0
                    Dim decFinalTotalHoursNC As Decimal = 0
                    Dim decFinalTotalCost As Decimal = 0
                    '-
                    Dim decThisHoursChg As Decimal
                    Dim decThisHoursNC As Decimal
                    Dim decThisCost As Decimal

                    '-- PREVIEW/PRINT --
                    '-  define tab columns-
                    Const k_TAB_TECH As Integer = 0
                    Const k_TAB_DAY As Integer = 60
                    Const k_TAB_DATE As Integer = 140
                    Const k_TAB_JOB As Integer = 180
                    Const k_TAB_STATUS As Integer = 280
                    Const k_TAB_HRS_CHG As Integer = 360 : Const k_WIDTH_HRS_CHG As Integer = 72
                    Const k_TAB_HRS_COST As Integer = 432 : Const k_WIDTH_HRS_COST As Integer = 100
                    Const k_TAB_HRS_NC As Integer = 532 : Const k_WIDTH_HRS_NC As Integer = 100

                    colReportLines = New Collection

                    '--ok. Markup and Print Real Query Results-
                    Dim colAllTechs As Collection = mColQueryResults.Item(1)
                    Dim colTechRows As Collection = colAllTechs.Item("rows")
                    Dim colAllDays, colDayRows As Collection
                    'MsgBox("TEST- base collection Name='" & _
                    '            colAllTechs.Item("ChapterName") & "' Count=" & colAllTechs.Count & vbCrLf & _
                    '            "colTechRows count=" & colTechRows.Count, MsgBoxStyle.Information)
                    '-- each tech has a row-
                    Dim colRowData As Collection
                    '-- each day has a row-
                    Dim colDayRowData As Collection
                    '- Jobs in day.
                    Dim colAllJobs, colJobRows, colJob, colJobRowData As Collection
                    Dim colAllSessions, colSessionRows, colSession, colSessionData As Collection
                    Dim sCustomer, sPriority, sCost As String
                    '--Dig into all levels-
                    For Each colTech As Collection In colTechRows  '-each tech group==
                        '-restart tech totals-
                        decTechTotalHoursCharged = 0
                        decTechTotalHoursNC = 0
                        decTechTotalCost = 0
                        colRowData = colTech.Item("rowdata")
                        s2 = colRowData.Item("techName")("value")
                        s1 = "<textline><txt fontstyle=""bold"">" & s2 & "</txt></textline>"
                        colReportLines.Add(s1)
                        '- get all days this tech..
                        colAllDays = colTech.Item("rowchild")(1)
                        colDayRows = colAllDays.Item("rows")
                        For Each colDay As Collection In colDayRows
                            '-restart day totals-
                            decDayTotalHoursCharged = 0
                            decDayTotalHoursNC = 0
                            decDayTotalCost = 0
                            colDayRowData = colDay.Item("rowdata")
                            s2 = colDayRowData.Item("dayNumber")("value") & ": " &
                                      colDayRowData.Item("SessionDayOfWeek")("value")
                            s3 = colDayRowData.Item("SessionDate")("value")
                            s1 = "<textline><txt TAB=""" & k_TAB_DAY & """>" & s2 & "</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_DATE & """>" & s3 & "</txt></textline>"
                            colReportLines.Add(s1)
                            '-- Now show all Jobs and sesssions for day..
                            '=colRoot = colDay.Item("rowdata")
                            colAllJobs = colDay.Item("rowchild")(1)
                            colJobRows = colAllJobs.Item("rows")
                            For Each colJob In colJobRows
                                colJobRowData = colJob("rowdata")
                                '- show Job Info-
                                sCustomer = colJobRowData.Item("Customer")("value")  '--save- for next line-
                                s2 = "Job # " & colJobRowData.Item("job_id")("value") & ": "
                                s3 = colJobRowData.Item("JobStatus")("value")
                                sPriority = colJobRowData.Item("JobPriority")("value")
                                s4 = colJobRowData.Item("PriorityDescr")("value")
                                s1 = "<textline><txt TAB=""" & k_TAB_JOB & """ fontStyle=""bold"" >" & s2 & "</txt>"
                                s1 &= "<txt  TAB=""" & k_TAB_STATUS & """>" & s3 & "</txt>"  '-status-
                                s1 &= "<txt TAB=""" & k_TAB_HRS_CHG & """>" & s4 & "</txt>"  '-priority-
                                s1 &= "</textline>"
                                colReportLines.Add(s1)
                                s1 = "<textline><txt TAB=""" & k_TAB_JOB & """>" & sCustomer & "</txt>"
                                s1 &= "</textline>"
                                colReportLines.Add(s1)

                                '-- get sessions hours for this Job-
                                colAllSessions = colJob.Item("rowchild")(1)
                                colSessionRows = colAllSessions.Item("rows")
                                For Each colSession In colSessionRows
                                    colSessionData = colSession("rowdata")
                                    s2 = colSessionData.Item("hours_chg")("value")
                                    s4 = colSessionData.Item("hours_nc")("value")
                                    sCost = "--"
                                    If IsNumeric(s2) Then
                                        Call mbComputeLabourCost(sPriority, s2, decThisCost)
                                        sCost = FormatCurrency(decThisCost, 2)
                                        decThisHoursChg = CDec(s2)
                                        '-update totals-
                                        decDayTotalHoursCharged += decThisHoursChg
                                        decTechTotalHoursCharged += decThisHoursChg
                                        decFinalTotalHoursCharged += decThisHoursChg
                                        '- update costss-
                                        decDayTotalCost += decThisCost
                                        decTechTotalCost += decThisCost
                                        decFinalTotalCost += decThisCost
                                    End If
                                    If IsNumeric(s4) Then
                                        decThisHoursNC = CDec(s4)
                                        '-update totals-
                                        decDayTotalHoursNC += decThisHoursNC
                                        decTechTotalHoursNC += decThisHoursNC
                                        decFinalTotalHoursNC += decThisHoursNC
                                    End If
                                    '-print this session hours/cost-
                                    s1 = "<textline>"
                                    s1 &= "<txt TAB=""" & k_TAB_HRS_CHG & """ align=""right"" "
                                    s1 &= " width = """ & k_WIDTH_HRS_CHG & """ >" & s2 & "</txt>"  '-hours chg.-
                                    s1 &= "<txt TAB=""" & k_TAB_HRS_COST & """ align=""right"" "
                                    s1 &= " width = """ & k_WIDTH_HRS_COST & """ >" & sCost & "</txt>"  '-cost.-
                                    s1 &= "<txt TAB=""" & k_TAB_HRS_NC & """ align=""right"" "
                                    s1 &= " width = """ & k_WIDTH_HRS_NC & """ >" & s4 & "</txt>"  '--hours nc-
                                    s1 &= "</textline>"
                                    colReportLines.Add(s1)
                                Next colSession
                                '-- end of job-
                            Next colJob

                            '- End of day.
                            '-print totals for day..-
                            s2 = Format(decDayTotalHoursCharged, "   0.00")
                            sCost = FormatCurrency(decDayTotalCost, 2)
                            s4 = Format(decDayTotalHoursNC, "   0.00")
                            '-print this day total hours/cost-
                            s1 = "<textline>"
                            s1 &= "<txt TAB=""" & k_TAB_DAY & """ fontStyle=""bold"" >Day Total:</txt>"
                            s1 &= "<txt TAB=""" & k_TAB_HRS_CHG & """ align=""right""  fontstyle=""bold""   "
                            s1 &= " width = """ & k_WIDTH_HRS_CHG & """ >" & s2 & "</txt>"  '-hours chg.-
                            s1 &= "<txt TAB=""" & k_TAB_HRS_COST & """ align=""right""  fontstyle=""bold""   "
                            s1 &= " width = """ & k_WIDTH_HRS_COST & """ >" & sCost & "</txt>"  '-cost.-
                            s1 &= "<txt TAB=""" & k_TAB_HRS_NC & """ align=""right""  fontstyle=""bold""   "
                            s1 &= " width = """ & k_WIDTH_HRS_NC & """ >" & s4 & "</txt>"  '--hours nc-
                            s1 &= "</textline>"
                            colReportLines.Add(s1)
                            s1 = "<drawline fontstyle=""bold""  TAB=""" & k_TAB_DAY & """ />"
                            colReportLines.Add(s1)
                        Next colDay

                        '-- end of this tech-
                        '-- print totals for tech-
                        s2 = Format(decTechTotalHoursCharged, "   0.00")
                        sCost = FormatCurrency(decTechTotalCost, 2)
                        s4 = Format(decTechTotalHoursNC, "   0.00")
                        '-print this TECH total hours/cost-
                        s1 = "<textline>"
                        s1 &= "<txt TAB=""" & k_TAB_TECH & """ fontStyle=""bold"" >Tech Total All days:</txt>"
                        s1 &= "<txt TAB=""" & k_TAB_HRS_CHG & """ align=""right""  fontstyle=""bold""   "
                        s1 &= " width = """ & k_WIDTH_HRS_CHG & """ >" & s2 & "</txt>"  '-hours chg.-
                        s1 &= "<txt TAB=""" & k_TAB_HRS_COST & """ align=""right""  fontstyle=""bold""   "
                        s1 &= " width = """ & k_WIDTH_HRS_COST & """ >" & sCost & "</txt>"  '-cost.-
                        s1 &= "<txt TAB=""" & k_TAB_HRS_NC & """ align=""right""  fontstyle=""bold""   "
                        s1 &= " width = """ & k_WIDTH_HRS_NC & """ >" & s4 & "</txt>"  '--hours nc-
                        s1 &= "</textline>"
                        colReportLines.Add(s1)

                        s1 = "<drawline fontstyle=""bold"" />"
                        colReportLines.Add(s1)
                    Next colTech
                    '-- alltechs done.
                    '-- print Final Totals-
                    s2 = Format(decFinalTotalHoursCharged, "   0.00")
                    sCost = FormatCurrency(decFinalTotalCost, 2)
                    s4 = Format(decFinalTotalHoursNC, "   0.00")
                    '-print this TECH total hours/cost-
                    s1 = "<textline>"
                    s1 &= "<txt TAB=""" & k_TAB_TECH & """ fontStyle=""bold"" >FINAL Total All Techs, All days:</txt>"
                    s1 &= "<txt TAB=""" & k_TAB_HRS_CHG & """ align=""right""  fontstyle=""bold""   "
                    s1 &= " width = """ & k_WIDTH_HRS_CHG & """ >" & s2 & "</txt>"  '-hours chg.-
                    s1 &= "<txt TAB=""" & k_TAB_HRS_COST & """ align=""right""  fontstyle=""bold""   "
                    s1 &= " width = """ & k_WIDTH_HRS_COST & """ >" & sCost & "</txt>"  '-cost.-
                    s1 &= "<txt TAB=""" & k_TAB_HRS_NC & """ align=""right""  fontstyle=""bold""   "
                    s1 &= " width = """ & k_WIDTH_HRS_NC & """ >" & s4 & "</txt>"  '--hours nc-
                    s1 &= "</textline>"
                    colReportLines.Add(s1)
                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)

                    '= Dim clsReportPrint1 = New clsReportPrinter
                    Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msProgramVersion, colReportLines, msSelectedPrinterName,
                                                             msBusinessName, "Staff Daily Worksheets",
                                                              Color.DarkMagenta,
                                                              "Shows Day by Day all Tech session times " &
                                                              "logged to Jobs " & LabReportStatus.Text & vbCrLf &
                                                              strLabourRates, sReportPeriod,
                                        "Tech Name Sess.day/date   Job/Customer " & Space(24) & " Hours Chg.    Cost     Hours N/C.")
                End If '-bof. empty- 
            End If '--query sql..- 

        End If '--report type.-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        LabReportName.Text = " Ready..  Select Report Type and Job Status.."
        '== report1 = Nothing
        Exit Sub

        'ReportSetupWhereCondition: 
        '		'-- format dates for SQL..-
        '		sDate1 = VB6.Format(date1, "dd-mmm-yyyy") & " 00:00" '-min-
        '		sDate2 = VB6.Format(date2, "dd-mmm-yyyy") & " 23:59" '--max.--
        '		If sWhere = "" Then
        '			sWhere = " WHERE "
        '		Else
        '			sWhere = sWhere & " AND "
        '		End If
        '		sWhere = sWhere & " ((Jobs." & sDateColumn & ">='" & sDate1 & "') AND (Jobs." & sDateColumn & "<='" & sDate2 & "')) "
        '        Return
    End Sub '--parts report..-
    '= = = = = = = = = = = =
    '-===FF->

    '= = L o a d = = =  = = = = = =
    '= = L o a d = = =  = = = = = =
    '= = L o a d = = =  = = = = = =

    Private Sub frmJobReports_Load(ByVal eventSender As System.Object,
                                   ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim ix As Integer
        Dim s1 As String

        mbStartupDone = False
        cmdRefresh.Enabled = False
        cmdPrint.Enabled = False

        FrameReportType.Enabled = False
        For ix = 0 To K_MAXREPORTTYPE
            optReport(ix).Enabled = False
            optReport(ix).Checked = False
        Next ix
        ChkWarranty.Enabled = False
        FrameStatus.Text = "Select Job Status"
        'For ix = 0 To K_MAXREPORTSTATUS
        '    OptJobStatus(ix).Checked = False
        '    OptJobStatus(ix).Enabled = False
        'Next ix
        '==FrameStatus.Visible = False
        LabDBName.Text = ""
        LabReportStatus.Text = ""
        LabHdr1.Text = ""

        FrameStatus.Enabled = False
        FramePeriod.Enabled = False
        LabSelectPeriod.Enabled = False
        panelDtPickers.Enabled = False
        labPickDates.Visible = False

        LabInclude.Enabled = False

        CboMonths.Items.Clear()
        CboMonths.Enabled = False
        LabSelectMonth.Enabled = False
        mlPeriodIndex = -1

        cboPeriod.Items.Clear()
        cboPeriod.Items.Add("Today")
        cboPeriod.Items.Add("This Week")
        cboPeriod.Items.Add("Last Week")
        cboPeriod.Items.Add("This Month")
        cboPeriod.Items.Add("Last Month")
        cboPeriod.Items.Add("Last 12 Months")
        cboPeriod.Items.Add("Select Month")
        cboPeriod.Items.Add("Select Period")
        cboPeriod.Enabled = False
        msBusinessName = "Business Name."

        msServiceChargeCat1 = "SERVCE" '--JobMatix default..-
        msServiceChargeCat2 = ""

        '-- SHOW startup.--
        '===Call MAIN_Startup

        mCurLabourHourlyRateP1 = 0
        mCurLabourHourlyRateP2 = 0
        mCurLabourHourlyRateP3 = 0
        mCurLabourMinCharge = 0

        mIntItemBarcodeFontSize = 9 '--default..--
        msItemBarcodeFontName = ""

        '===MsgBox "frmTestLookup loaded..", vbInformation
        msServer = ""

        '== Target-Build-4284  (Started 01-Nov-2020)
        '= msInputDBName = ""
        '= msDBnameJobs = ""
        '== END Target-Build-4284  (Started 01-Nov-2020)


        '= cboDatabases.Visible = False

        '= cmdOK.Visible = False
        '= cmdCancel.Visible = False
        '== LabChoose.Visible = False

        '==chkStaffSummary.Value = 0   '--unchecked..-
        '==chkStaffSummary.Enabled = False
        mbShowDetailLines = False

        '== cmdExit.Enabled = False

        msSettingsPath = gsLocalJobsSettingsPath("JobMatix33") '= msAppPath & K_SAVESETTINGSPATH

        '==3301.828= Load up Settings.
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)
        'If mLocalSettings1.queryLocalSetting("sqlserver", s1) Then
        '    msServer = s1
        'End If
        msProgramVersion = "JobReports33- v" & CStr(My.Application.Info.Version.Major) & "." &
                             My.Application.Info.Version.Minor & "; Build: " &
                                   My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
        Me.Text = msProgramVersion

        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        '=3301.1028= get printers.
        cboPrinters.Items.Clear()
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName As String In colPrinters
                cboPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.queryLocalSetting(k_PrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then
                        cboPrinters.SelectedItem = msDefaultPrinterName
                    End If
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then
                    cboPrinters.SelectedItem = msDefaultPrinterName
                End If
            End If  '-query- 
            msSelectedPrinterName = cboPrinters.SelectedItem
        End If '-getAvail.-  

        '== Target-Build-4284  (Started 01-Nov-2020)
        '==    DEV- Bring JobReports into JobTracking Main as a UserControl..
        grpBoxReportsMain.Text = ""
        Call frmJobReports_Shown()



    End Sub '--load..-
    '= = = = = = = = = = = =
    '-===FF->

    '-- A c t i v a t e --
    '-- A c t i v a t e --
    '-- A c t i v a t e --

    'Private Sub frmJobReports_Activated(ByVal eventSender As System.Object,
    '                                ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

    'End Sub  '-activated..
    '= = = = = = = = = = = =

    '=3323= - Now is Shown..=

    '== Target-Build-4284  (Started 01-Nov-2020)
    '==    DEV- Bring JobReports into JobTracking Main as a UserControl..

    '- Now called from Load Event..

    Private Sub frmJobReports_Shown()  '=(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Shown
        '= Dim bUpdateJet As Boolean
        '= Dim ans As Short
        Dim sCmdLine As String
        Dim sConnect As String
        '= Dim bLoggedOn As Boolean
        '= Dim bOk As Boolean
        Dim lCount, ix, L1, lngStart As Integer
        Dim s1, s2 As String
        '= Dim sName As String
        Dim sErrors As String
        Dim sMsg As String
        Dim sSql As String
        Dim v1, vName As Object
        Dim dateX As Date
        '= Dim p1 As ADODB.Property
        '= Dim col1 As Collection
        '= Dim colRecord As Collection
        '= Dim colMyList As Collection
        '= Dim colJobsDBs As Collection

        If mbStartupDone Then Exit Sub
        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"

        '== Target-Build-4284  (Started 01-Nov-2020)
        '= msDBnameJobs = ""
        '== END Target-Build-4284  (Started 01-Nov-2020)

        '- gsJobMatixLocalDataDir-
        msLocalDataDir = gsJobMatixLocalDataDir()
        If VB.Right(msLocalDataDir, 1) <> "\" Then msLocalDataDir &= "\"

        '== Target-Build-4284  gsErrorLogPath = msLocalDataDir & "Report_Errors.txt" '--- msAppPath & "Report_Errors.txt"

        Me.Text = "JobMatix JobReports Ver: " & My.Application.Info.Version.Major & "." &
                      My.Application.Info.Version.Build & "." &
                          My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision

        '--msJetDbName = "\\uki-sunrise\precise-July-2009\recent.mdb"
        '== Call CenterForm(Me)

        '== Target-Build-4284  (Started 01-Nov-2020)
        '== Target-Build-4284  (Started 01-Nov-2020)

        '== Build-4284--    sCmdLine = Trim(VB.Command())
        sCmdLine = msInputCmdline
        mbUseLegacyLabourRates = False
        '== END Target-Build-4284  (Started 01-Nov-2020)
        '== END Target-Build-4284  (Started 01-Nov-2020)



        gbDebug = False
        '== If InStr(1, UCase$(sCmdLine), "/D") > 0 Then gbDebug = True
        If gbGetCmd(sCmdLine, "debug", s1) Then gbDebug = True

        If gbGetCmd(sCmdLine, "Server", s1) Then
            If (s1 <> "") Then
                msServer = s1
            End If
        End If

        '== Target-Build-4284  (Started 01-Nov-2020)
        '== Target-Build-4284  (Started 01-Nov-2020)
        'If gbGetCmd(sCmdLine, "DBName", s1) Then
        '    If (s1 <> "") Then
        '        msInputDBName = s1
        '    End If
        'End If
        '== END  Target-Build-4284  (Started 31-Oct-2020)

        '-- mbUseLegacyLabourRates-
        '==3323.0621=
        '--  User should send labour rates om cmd line..
        '==     -- Caller must send in SIX labour rates on Cmd Line parms...-==
        '==        ie. HrlyRateP1,  HrlyRateP2,  HrlyRateP3,  HrlyRateOnSiteP1,  HrlyRateOnSiteP2,  HrlyRateOnSiteP3

        mbUseLegacyLabourRates = False
        If gbGetCmd(sCmdLine, "HrlyRateP1", s1) Then
            If (s1 <> "") AndAlso IsNumeric(s1) Then
                mCurLabourHourlyRateP1 = CDec(s1)
                '-Use P1 rates for all other rates not specified.
                mCurLabourHourlyRateP2 = mCurLabourHourlyRateP1
                mCurLabourHourlyRateP3 = mCurLabourHourlyRateP1
                mCurLabourHourlyRateOnSiteP1 = mCurLabourHourlyRateP1
                mCurLabourHourlyRateOnSiteP2 = mCurLabourHourlyRateP1
                mCurLabourHourlyRateOnSiteP3 = mCurLabourHourlyRateP1

                '-  now get P2 from the cmd line if any..
                If gbGetCmd(sCmdLine, "HrlyRateP2", s1) Then
                    If (s1 <> "") AndAlso IsNumeric(s1) Then
                        mCurLabourHourlyRateP2 = CDec(s1)
                    End If '-s1-
                End If '-HrlyRateP2-
                '-  now get P3 from the cmd line if any..
                If gbGetCmd(sCmdLine, "HrlyRateP3", s1) Then
                    If (s1 <> "") AndAlso IsNumeric(s1) Then
                        mCurLabourHourlyRateP3 = CDec(s1)
                    End If '-s1-
                End If '-HrlyRateP3-
                '-  now get OnSite P1 from the cmd line if any..
                If gbGetCmd(sCmdLine, "HrlyRateOnSiteP1", s1) Then
                    If (s1 <> "") AndAlso IsNumeric(s1) Then
                        mCurLabourHourlyRateOnSiteP1 = CDec(s1)
                    End If '-s1-
                End If '-HrlyRateP1-
                '-  now get OnSite P2 from the cmd line if any..
                mCurLabourHourlyRateOnSiteP2 = mCurLabourHourlyRateP3
                If gbGetCmd(sCmdLine, "HrlyRateonsiteP2", s1) Then
                    If (s1 <> "") AndAlso IsNumeric(s1) Then
                        mCurLabourHourlyRateOnSiteP2 = CDec(s1)
                    End If '-s1-
                End If '-HrlyRateP2-
                '-  now get onsite P3 from the cmd line if any..
                mCurLabourHourlyRateOnSiteP3 = mCurLabourHourlyRateP3
                If gbGetCmd(sCmdLine, "HrlyRateOnSiteP3", s1) Then
                    If (s1 <> "") AndAlso IsNumeric(s1) Then
                        mCurLabourHourlyRateOnSiteP3 = CDec(s1)
                    End If '-s1-
                End If '-HrlyRateP3-
            End If '-s1.. HrlyRateP1-
        Else
            mbUseLegacyLabourRates = True   '--use old rates from systemInfo if any.. (See BELOW)
        End If '-HrlyRateP1

        '--Find local ComputerName as server default--
        'L1 = gsRegQueryValue(HKEY_LOCAL_MACHINE, "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName", "ComputerName", s1)
        'If L1 = 0 Then
        '    '--MsgBox "Local ComputerName is :" + vbCrLf + s1
        '    msComputerName = s1
        'Else
        '    MsgBox("Can't find computer name..  Reg error: " & L1)
        'End If
        msComputerName = My.Computer.Name
        msCurrentUser = gsCurrentUser()

        '==MsgBox "Current User is:  " & msComputerName & "\" & msCurrentUser
        '==  if no cmd line server, then check local settings..-
        If (msServer = "") Then
            '== msServer = mSdSettings.Item("SQLSERVER")
            If mLocalSettings1.queryLocalSetting("sqlserver", s1) Then
                msServer = s1
            End If
        End If
        If (msServer = "") Then
            MsgBox("No server name supplied.", MsgBoxStyle.Exclamation)
            Me.Hide()
            '== Target-Build-4284 End
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '-- Using Windows Authentication..--
        '-- Using Windows Authentication..--
        '-- Using Windows Authentication..--
        msSqlUid = ""
        msSqlPwd = ""

        '--TESTING Appp Path..-




        '== Target-Build-4284  (Started 31-Oct-2020)
        '== Target-Build-4284  (Started 31-Oct-2020)
        '==    DEV- Bring JobReports into JobTracking Main as a UserControl..
        '=--  Sql oledb Connection is now supplied by Caller from Maim..
        '=--  Sql oledb Connection is now supplied by Caller from Maim..

        'LabReportName.Text = "Logging on to SQL Server: " & msServer
        'System.Windows.Forms.Application.DoEvents()
        ''--  Connect to Jobs..-
        'bLoggedOn = False
        'ans = MsgBoxResult.Yes
        'mCnnSql = New OleDbConnection '=ADODB.Connection
        'sConnect = "Provider=SQLOLEDB; Server=" & msServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        'While (Not bLoggedOn) And (ans = MsgBoxResult.Yes)
        '    If gbConnectSql(mCnnSql, sConnect) Then
        '        bLoggedOn = True
        '        '==sSqlServer = sServer
        '    Else
        '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '= vbNormal
        '        ans = MsgBox("Login to Sql-Server '" & msServer & "' has failed." & vbCrLf & "Check credentials..  Retry???", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question)
        '    End If '--connected-
        'End While '--bLoggedOn-
        ''-- show ole db cnn properties..--
        's1 = vbCrLf
        'If Not bLoggedOn Then
        '    Me.Close()
        '    Exit Sub
        'End If
        ''==  End
        'LabReportName.Text = "Sql Login done.."
        'System.Windows.Forms.Application.DoEvents()
        ''--  get sql version..-
        'Call gbSetupSqlVersion(mCnnSql)
        'Call gbSetSqlVersion(msSqlVersion) '== msSqlVersion = gsGetSqlServerVersion(mCnnSql)

        '==END Target-Build-4284  (Started 31-Oct-2020)
        '==END Target-Build-4284  (Started 31-Oct-2020)






        '--  Discover if we are an SQL-Amin user..--
        '--  Discover if we are an SQL-Amin user..--
        mbIsSqlAdmin = False
        sMsg = "" '-debug logininfo..-
        '-- "xp_logininfo" should fail if we are not ADMIN..--
        LabReportName.Text = "SQL Server: " & msServer & "  Version: " & msSqlVersion & "-  " & "Checking user privileges.."
        '--  check if we are sqlAdmin privileged..--
        mbIsSqlAdmin = gbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")
        LabADmin.Text = IIf(mbIsSqlAdmin, "Admin.User", "")





        '== Target-Build-4284  (Started 01-Nov-2020)
        '== Target-Build-4284  (Started 01-Nov-2020)
        '===--  DB name is now supplied as INPUT..

        'If (msInputDBName = "") Then
        '    LabReportName.Text = "SQL Server: " & msServer & ";  Ver: " & msSqlVersion & "-  " & "Getting avail. database names.."
        '    System.Windows.Forms.Application.DoEvents()
        '    '-- find db names for this user..--
        '    '--  Get list of databases we can see.--
        '    If (VB.Left(msSqlVersion, 1) >= "9") Then '-- sql server 2005 or later..--
        '        bOk = gbGetDatabasesSQL2005(mCnnSql, colMyList)
        '    Else '--  <"9".. assume sql Server 2000..---
        '        '--get list of db's for this sql server--
        '        '-- IN SQL-2000, this only works for PUBLIC..--
        '        bOk = gbGetDatabases(mCnnSql, colMyList)
        '    End If '--2005..--
        '    If Not bOk Then
        '        MsgBox("Unable to retrieve DB list..", MsgBoxStyle.Critical, "JobTracking, Startup.")
        '        Me.Close()
        '        Exit Sub '-- End
        '        '==ElseIf gbDebug Then
        '        '==        MsgBox "got SQL DB collection ok..", vbInformation
        '    End If '-ok--
        '    LabReportName.Text = " done.."

        '    '-- collect all DB's..
        '    LabReportName.Text = "SQL Server: " & msServer & " Version: " & msSqlVersion & "-  " & "Checking database names.."
        '    '==Set colDBs = New Collection  '--collect all db info collections-
        '    '==Set colList2 = New Collection '--count filtered list--
        '    colJobsDBs = New Collection '--count jobTracking DBs--
        '    s1 = LCase(msServer)
        '    '--frmJobs.labStatus.BackColor = &HC0FFFF     '--light yellow-
        '    sMsg = "Server: '" & msServer & "'.. Found databases:" & vbCrLf
        '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    For Each vName In colMyList
        '        sName = CStr(vName) '--??-
        '        s2 = LCase(sName)
        '        If (s2 <> "master") And (s2 <> "model") And (s2 <> "msdb") And (s2 <> "tempdb") Then '-ok-
        '            '==colList2.Add sName
        '            sMsg = sMsg & sName & vbCrLf
        '            '-- collect jobtracking db's..-
        '            If (InStr(s2, "jobtracking") > 0) Then
        '                '--check user has access to this DB..--
        '                If mbIsSqlAdmin Or ((Not mbIsSqlAdmin) And gbTestSqlUser(mCnnSql, "SELECT HAS_DBACCESS ('" & sName & "'); ")) Then
        '                    col1 = New Collection
        '                    col1.Add(sName, "dbname")
        '                    '==== col1.Add colDBInfo, "dbinfo"
        '                    colJobsDBs.Add(col1, sName)
        '                    msDBnameJobs = sName '--save last in case only one..--
        '                End If '--admin..-
        '                '==== Set colDBInfoJobs = colDBInfo   '--save last in case only one..--
        '            End If
        '        End If '--not master-
        '    Next vName '--each name-
        'End If '-input dbname..-
        ''--TEST--
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default  '= vbNormal
        ''==MsgBox sMsg, vbInformation  '--TESTING..--

        ''-- IF input dbname supplied, or 1 DB, then go ahead into application..
        ''----  If no input dbname and multiple jobs db's, we show combo box..--
        ''====  If (LCase(sSqlUid) = "sa") Then  '--NO MORE SQL Athentication..-
        'If (msInputDBName <> "") Then '--input db specified..
        '    msDBnameJobs = msInputDBName
        'ElseIf (colJobsDBs.Count() > 1) Then
        '    cboDatabases.Items.Clear()
        '    For Each col1 In colJobsDBs
        '        cboDatabases.Items.Add(col1.Item("dbname"))
        '    Next col1
        '    cboDatabases.Visible = True
        '    cmdOK.Visible = True
        '    cmdCancel.Visible = True
        '    LabChoose.Visible = True
        '    '==LabFunctions.Enabled = False
        '    If (cboDatabases.Items.Count > 0) Then
        '        cboDatabases.SelectedIndex = 0 '--show first..--
        '        cmdOK.Focus()
        '    Else
        '        '--nothing to choose..--
        '        MsgBox("No JobTracking databases found..", MsgBoxStyle.Exclamation)
        '        Me.Close()
        '        Exit Sub
        '    End If
        '    mbCancelled = False
        '    mbOK = False
        '    '-- Wait for some user input on splash form..-
        '    '--  timeout after 10 minutes..-
        '    LabReportName.Text = "SQL Server: " & msServer & "; Version: " & msSqlVersion & "-  " & "Waiting for User Selection.."
        '    System.Windows.Forms.Application.DoEvents()
        '    lngStart = CInt(VB.Timer()) '--starting seconds.-
        '    While Not (mbOK Or mbCancelled) And (CInt(VB.Timer()) < lngStart + 600)
        '        System.Windows.Forms.Application.DoEvents()
        '    End While
        '    '--take action chosen..-
        '    If mbCancelled Then
        '        Me.Close()
        '        Exit Sub '-- End
        '    ElseIf mbOK Then  '--OK--
        '        If cboDatabases.SelectedIndex >= 0 Then '--chosen..-
        '            With cboDatabases
        '                msDBnameJobs = VB6.GetItemString(cboDatabases, .SelectedIndex)
        '            End With
        '        Else
        '            MsgBox("No valid choice..", MsgBoxStyle.Exclamation)
        '            Me.Close()
        '            '== Target-Build-4284 End
        '        End If
        '    Else '--timed out..-
        '        Me.Close()
        '        Exit Sub '-- End
        '    End If '--cmd--
        'Else '--Not Admin and  1 db only--
        '    '--name was saved..--
        'End If '--choose..--
        'If msDBnameJobs = "" Then
        '    MsgBox("Can't find any Jobs database for user: '" & msCurrentUser & "'..", MsgBoxStyle.Exclamation)
        '    Me.Close()
        '    Exit Sub '-- End
        'End If

        '== END Target-Build-4284  (Started 01-Nov-2020)
        '== END Target-Build-4284  (Started 01-Nov-2020)






        LabDBName.Text = msDBnameJobs
        '-- Load DB-Info for selected DB..--
        sSql = "USE " & msDBnameJobs & vbCrLf
        '-- set up db..--
        If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
            MsgBox("Failed USE for DB: '" & msDBnameJobs & "'.." & vbCrLf & sErrors & vbCrLf &
                                          "User may not have access..", MsgBoxStyle.Exclamation)
            '= Me.Close()
            '= Exit Sub
        End If '-- USE..--
        '= cmdExit.Enabled = True
        '= cboDatabases.Enabled = False
        '= cmdOK.Enabled = False
        '= VB6.SetCancel(cmdCancel, False)
        '= cmdCancel.Enabled = False
        '= VB6.SetCancel(cmdExit, True)
        '= LabChoose.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '-- setup sql SHAPE connection for reports..--
        '-- setup sql SHAPE connection for reports..--
        '-- setup sql SHAPE connection for reports..--
        '-- setup sql SHAPE connection for reports..--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mCnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & msServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & msServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(mCnnShape, sConnect) Then
            '--FrameReport.Enabled = True   '--show report options frame..--
            '--FrameStatus.Enabled = True
        Else
            MsgBox("Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
            '====FrameReport.Enabled = False
            '= Me.Close()
            Exit Sub
            '== Target-Build-4284 End
        End If '--connected-
        If Not gbExecuteCmd(mCnnShape, "USE " & msDBnameJobs & vbCrLf, L1, sErrors) Then
            MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" & msDBnameJobs & "'.." &
                                                                  vbCrLf & sErrors, MsgBoxStyle.Exclamation)
            mCnnShape.Close()
            '= Me.Close()
            Exit Sub
        End If '--use-
        '= mCnnShape.CommandTimeout = 10 '-- 10 sec cmd timeout..-
        '= mCnnShape.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '-- REPORTS.. find oldest job date..-
        mDateOldest = (DateAdd(Microsoft.VisualBasic.DateInterval.Year, -1, Today)) '--default to 1 year ago..-
        sSql = "SELECT MIN(DateCreated) as OldestJobDate FROM [Jobs]; "
        If mbGetSelectValue(sSql, v1) Then
            mDateOldest = CDate(v1)
        Else
            MsgBox("No Job date info found..", MsgBoxStyle.Exclamation)
        End If '--get-


        CboMonths.Items.Clear()
        lCount = 24 '-maxx 24 months offered..-
        dateX = Today
        While (lCount > 0) And (dateX >= mDateOldest)
            CboMonths.Items.Add(VB6.Format(dateX, "yyyy-mmm"))
            dateX = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, dateX) '--go back a week at a time..-
            lCount = lCount - 1
        End While

        'If Not gbLoadsystemInfo(mCnnSql, mColSystemInfo, mSdSystemInfo) Then
        '	MsgBox("No System Info..", MsgBoxStyle.Exclamation)
        'End If
        '==3301.828= SysInfo. use class instance.
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '--set up service charge definitions..--
        '--- Don't destroy default if not defined in systemInfo..
        s1 = mSysInfo1.item("STOCKSERVICECHARGECAT1")
        If (s1 <> "") Then
            msServiceChargeCat1 = s1
            msServiceChargeCat2 = mSysInfo1.item("STOCKSERVICECHARGECAT2")
        End If
        '=3323.0621=
        If mbUseLegacyLabourRates Then  '-NO rates were input..get old rates if any.
            If mSysInfo1.exists("LABOURHOURLYRATEPRIORITY1") Then
                If (mSysInfo1.item("LABOURHOURLYRATEPRIORITY1") <> "") Then
                    mCurLabourHourlyRateP1 = CDec(mSysInfo1.item("LABOURHOURLYRATEPRIORITY1"))
                    mCurLabourHourlyRateOnSiteP1 = mCurLabourHourlyRateP1
                End If
            End If
            If mSysInfo1.exists("LABOURHOURLYRATEPRIORITY2") Then
                If (mSysInfo1.item("LABOURHOURLYRATEPRIORITY2") <> "") Then
                    mCurLabourHourlyRateP2 = CDec(mSysInfo1.item("LABOURHOURLYRATEPRIORITY2"))
                    mCurLabourHourlyRateOnSiteP2 = mCurLabourHourlyRateP2
                End If
            End If
            If mSysInfo1.exists("LABOURHOURLYRATEPRIORITY3") Then
                If (mSysInfo1.item("LABOURHOURLYRATEPRIORITY3") <> "") Then
                    mCurLabourHourlyRateP3 = CDec(mSysInfo1.item("LABOURHOURLYRATEPRIORITY3"))
                    mCurLabourHourlyRateOnSiteP3 = mCurLabourHourlyRateP3
                End If
            End If
        End If  '-legacy rates-
        If mSysInfo1.exists("LABOURMINCHARGE") Then
            If (mSysInfo1.item("LABOURMINCHARGE") <> "") Then
                mCurLabourMinCharge = CDec(mSysInfo1.item("LABOURMINCHARGE"))
            End If
        End If

        '--  get descriptors..-
        msDescriptionPriority1 = mSysInfo1.item("DESCRIPTIONPRIORITY1")
        msDescriptionPriority2 = mSysInfo1.item("DESCRIPTIONPRIORITY2")
        msDescriptionPriority3 = mSysInfo1.item("DESCRIPTIONPRIORITY3")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")

        msItemBarcodeFontName = mSysInfo1.item("ITEMBARCODEFONTNAME")
        s1 = mSysInfo1.item("ITEMBARCODEFONTSIZE")
        If IsNumeric(s1) Then
            L1 = CInt(s1)
            If (L1 > 3) And (L1 < 36) Then
                mIntItemBarcodeFontSize = L1
            End If
        End If
        '==System.Windows.Forms.Cursor.Current = vbNormal
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FrameReportType.Enabled = True
        For ix = 0 To K_MAXREPORTTYPE
            optReport(ix).Enabled = True
        Next ix
        '= LabChoose.Enabled = True
        LabReportName.Text = " Ready..  Select Report Type and Job Status.."

        '==Call optReport_Click(0)  '--show all jobs--
        '= optReport(6).Checked = True
        mbStartupDone = True

        '==OptJobStatus(0).SetFocus
        '- TEST OFF-
        txtTestResults.Visible = False

        _optReport_0.Checked = True

    End Sub '--shown.--
    '= = = = = = = = = = = =
    '-===FF->

    '--  choose database.---

    Private Sub cmdOk_Click(ByVal eventSender As System.Object,
                               ByVal eventArgs As System.EventArgs)
        mbOK = True
    End Sub '--ok..-
    '= = = = = = = = = =

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        mbCancelled = True
    End Sub '--cancel.-
    '= = = = = = = = = = =
    '-===FF->

    '-- Choose Report..-
    '-- Choose Report..-

    'UPGRADE_WARNING: Event optReport.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub optReport_CheckedChanged(ByVal eventSender As System.Object,
                                         ByVal eventArgs As System.EventArgs) Handles optReport.CheckedChanged
        If eventSender.Checked Then
            Dim idxReport As Short = optReport.GetIndex(eventSender)
            Dim ix As Integer
            Dim optSelected As RadioButton = CType(eventSender, RadioButton)

            If optSelected.Checked Then
                miReportJobStatus = 0
                mbShowDetailLines = False
                If (idxReport Mod 2) = 1 Then mbShowDetailLines = True '--odd--
                'For ix = 0 To K_MAXREPORTSTATUS
                '    OptJobStatus(ix).Checked = False '--ckear all status buttons..
                'Next ix
                optJobStatusCurrent.Checked = False
                optJobStatusCompleted.Checked = False
                optJobStatusDelivered.Checked = False

                optJobStatusCurrent.Enabled = False
                optJobStatusCompleted.Enabled = False
                optJobStatusDelivered.Enabled = False
                chkExcludeNilLabourJobs.Checked = False
                chkExcludeNilLabourJobs.Enabled = False

                'FramePeriod.Enabled = False
                cboPeriod.SelectedIndex = -1 '--no selection..-
                CboMonths.SelectedIndex = -1
                mlPeriodIndex = -1
                FrameStatus.Enabled = False
                LabInclude.Enabled = False
                'OptJobStatus(0).Enabled = False
                'OptJobStatus(1).Enabled = False
                LabReportStatus.Text = ""
                panelDtPickers.Enabled = False

                '==chkStaffSummary.Value = 0   '--unchecked..-
                Select Case idxReport
                    Case 0, 1
                        miReportType = K_JOBS_REPORT '-- J o b s --
                        '==FrameStatus.Visible = True
                        FrameStatus.Enabled = True
                        LabInclude.Enabled = True
                        'OptJobStatus(0).Enabled = True
                        'OptJobStatus(1).Enabled = True
                        optJobStatusCurrent.Enabled = True
                        optJobStatusCompleted.Enabled = True
                        optJobStatusDelivered.Enabled = True

                        'For ix = 0 To K_MAXREPORTSTATUS
                        '    OptJobStatus(ix).Enabled = True
                        'Next ix
                        '==chkStaffSummary.Enabled = False
                        LabInclude.Enabled = True
                        ChkWarranty.Enabled = False
                        If mbShowDetailLines Then
                            LabHdr1.Text = "Jobs and Stock Items Detailed Report.."
                        Else
                            LabHdr1.Text = "Jobs Summary Report.."
                        End If
                    Case 2, 3
                        miReportType = K_PARTS_REPORT '--  P a r t s --
                        '==FrameStatus.Visible = True
                        FrameStatus.Enabled = True
                        LabInclude.Enabled = True
                        'OptJobStatus(0).Enabled = True
                        'OptJobStatus(1).Enabled = True
                        'For ix = 0 To K_MAXREPORTSTATUS
                        '    OptJobStatus(ix).Enabled = True
                        'Next ix
                        optJobStatusCurrent.Enabled = True
                        optJobStatusCompleted.Enabled = True
                        optJobStatusDelivered.Enabled = True

                        LabInclude.Enabled = True
                        ChkWarranty.Enabled = True
                        '==chkStaffSummary.Enabled = False
                        '===LabHdr1.Caption = "Stock Parts in Jobs Report.."
                        If mbShowDetailLines Then
                            LabHdr1.Text = "Stock Parts in Jobs Detailed Report.."
                        Else
                            LabHdr1.Text = "Stock Parts in Jobs Summary Report.."
                        End If
                    Case 4, 5
                        '-- S t a f f ---
                        miReportType = K_STAFF_REPORT '-- S t a f f ---
                        'For ix = 0 To K_MAXREPORTSTATUS
                        '    OptJobStatus(ix).Enabled = False
                        'Next ix
                        ChkWarranty.Enabled = False
                        LabInclude.Enabled = False

                        '= miReportJobStatus = K_STATUS_DELIVEREDJOBS '== K_STATUS_COMPLETEDJOBS
                        '=3323.0621= Only completed works..
                        miReportJobStatus = K_STATUS_COMPLETED_JOBS
                        '===LabHdr1.Caption = "Staff Hours and Jobs Report.."
                        If mbShowDetailLines Then
                            LabHdr1.Text = "Staff Hours and Jobs Detailed Report.."
                        Else
                            LabHdr1.Text = "Staff Hours and Jobs Summary Report.."
                        End If
                        FramePeriod.Enabled = True
                        cboPeriod.Enabled = True
                        cboPeriod.BackColor = System.Drawing.ColorTranslator.FromOle(&H80000005) '--whiteish..-
                        LabReportStatus.Text = "Completed Jobs Only.."
                        '===chkStaffSummary.Enabled = True
                        cboPeriod.Focus()
                    Case 6
                        '-- Daily Timesheets ---
                        '--  Any Job Updated in period..
                        miReportType = K_TIMESHEET_REPORT '-- Timesheets ---
                        miReportJobStatus = K_STATUS_ALL_JOBS

                        LabHdr1.Text = "Staff Hours and Jobs Summary Report.."
                        FramePeriod.Enabled = True
                        cboPeriod.Enabled = True
                        cboPeriod.BackColor = System.Drawing.ColorTranslator.FromOle(&H80000005) '--whiteish..-
                        LabHdr1.Text = "Daily Staff Jobs Timesheets Report.."
                        LabReportStatus.Text = "All Staff/Job sessions for selected Period.."
                        '===chkStaffSummary.Enabled = True
                        cboPeriod.Focus()

                    Case Else
                End Select
                cmdRefresh.Enabled = False
                cmdPrint.Enabled = False
                '== labChooseReport.Enabled = False

            End If  '--checked-


            System.Windows.Forms.Application.DoEvents()
        End If
    End Sub '--cmd Reports..-
    '= = = = = =
    '-===FF->

    '-- Select Job status.--
    '-- Select Job status.--

    'UPGRADE_WARNING: Event optJobStatus.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    '-optJobStatusCurrent_CheckedChanged=

    Private Sub optJobStatusCurrent_CheckedChanged(sender As Object,
                                                   ev As EventArgs) _
                                               Handles optJobStatusCurrent.CheckedChanged,
                                               optJobStatusCompleted.CheckedChanged,
                                               optJobStatusDelivered.CheckedChanged
        Dim Index As Short = -1
        Dim optSender As RadioButton  '--  = CType(sender, RadioButton)
        optSender = CType(sender, RadioButton)
        Dim sName As String = LCase(optSender.Name)

        If Not mbStartupDone Then Exit Sub '--was clicked in load event..-
        '-- who was clicked-
        If optSender.Checked Then  '-save it-
            LabReportName.Text = ""
            CboMonths.Visible = False
            LabSelectMonth.Visible = False
            cmdRefresh.Enabled = False
            cmdPrint.Enabled = False
            chkExcludeNilLabourJobs.Enabled = False

            Select Case sName
                Case "optjobstatuscurrent"
                    Index = K_STATUS_CURRENTJOBS
                    cboPeriod.Enabled = False
                    CboMonths.Enabled = False
                    LabSelectPeriod.Enabled = False
                    cboPeriod.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0) '--greyish..-
                    LabReportStatus.Text = "Jobs on Bench or Awaitng Delivery.."
                    LabReportName.Text = "Current Jobs only.."
                    cmdRefresh.Enabled = True
                    cmdPrint.Enabled = True
                    cmdRefresh.Focus()

                Case "optjobstatuscompleted"
                    Index = K_STATUS_COMPLETED_JOBS
                    '= cboPeriod.Enabled = False
                    CboMonths.Enabled = False
                    FramePeriod.Enabled = True
                    LabSelectPeriod.Enabled = True
                    cboPeriod.Enabled = True
                    cboPeriod.BackColor = System.Drawing.ColorTranslator.FromOle(&H80000005) '--whiteish..-

                    '= LabSelectPeriod.Enabled = False
                    '= cboPeriod.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0) '--greyish..-
                    LabReportStatus.Text = "Completed Jobs (Delivered or not).."
                    LabReportName.Text = "Completed Jobs only.."
                    chkExcludeNilLabourJobs.Enabled = True
                    '= cmdRefresh.Enabled = True
                    '= cmdPrint.Enabled = True
                    '= cmdRefresh.Focus()
                    cboPeriod.Focus()

                Case "optjobstatusdelivered"
                    Index = K_STATUS_DELIVEREDJOBS
                    FramePeriod.Enabled = True
                    LabSelectPeriod.Enabled = True
                    cboPeriod.Enabled = True
                    cboPeriod.BackColor = System.Drawing.ColorTranslator.FromOle(&H80000005) '--whiteish..-
                    '--cboPeriod.ListIndex = 0   '--show 1st item..-
                    LabReportStatus.Text = "Jobs Delivered in Selected Period.."
                    cboPeriod.Focus()

                Case Else

            End Select
        End If
        miReportJobStatus = Index '--save--

    End Sub  '-optJobStatusCurrent_CheckedChanged-
    '= = = = = =  = = = = = = = = = = = = = = = =
    '-===FF->

    '-- select a month..-
    'UPGRADE_WARNING: Event cboMonths.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboMonths_SelectedIndexChanged(ByVal eventSender As System.Object,
                                                ByVal eventArgs As System.EventArgs) Handles CboMonths.SelectedIndexChanged

        '==OptReport(0).Value = True  '-- start with jobs report..-
        '==OptReport(0).SetFocus
        LabReportName.Text = VB6.GetItemString(CboMonths, CboMonths.SelectedIndex)
        mlPeriodIndex = cboPeriod.SelectedIndex
        cmdRefresh.Enabled = True
        cmdPrint.Enabled = True
        cmdRefresh.Focus()
    End Sub '--months.--
    '= = = = = = = = =

    '-- select a period..-
    'UPGRADE_WARNING: Event cboPeriod.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboPeriod_SelectedIndexChanged(ByVal eventSender As System.Object,
                                                ByVal eventArgs As System.EventArgs) Handles cboPeriod.SelectedIndexChanged

        Dim rx As Short
        Dim sSelectedItem As String

        If (cboPeriod.SelectedIndex >= 0) Then '--selected..-
            '===For rx = 0 To K_MAXREPORTOPTION: OptReport(rx).Value = False: Next rx   '-- no report..-
            mlPeriodIndex = -1
            sSelectedItem = LCase(VB6.GetItemString(cboPeriod, cboPeriod.SelectedIndex))

            If (InStr(sSelectedItem, "select month") > 0) Then
                mlPeriodIndex = cboPeriod.SelectedIndex
                CboMonths.Visible = True
                LabSelectMonth.Visible = True
                LabSelectMonth.Enabled = True
                CboMonths.SelectedIndex = -1
                CboMonths.Enabled = True
                CboMonths.BackColor = System.Drawing.ColorTranslator.FromOle(&H80000005) '--whiteish..-
                CboMonths.Focus()
            ElseIf (InStr(sSelectedItem, "select period") > 0) Then
                '=3323.0620=
                mlPeriodIndex = cboPeriod.SelectedIndex
                panelDtPickers.Enabled = True
                labPickDates.Visible = True
                cmdRefresh.Enabled = True
                cmdPrint.Enabled = True

            Else '--selected pre-defined period..-
                mlPeriodIndex = cboPeriod.SelectedIndex
                CboMonths.Enabled = False
                CboMonths.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
                LabSelectMonth.Enabled = False
                '--OptReport(0).Value = True  '-- start with jobs report..-
                '--OptReport(0).SetFocus
                LabReportName.Text = VB6.GetItemString(cboPeriod, cboPeriod.SelectedIndex)
                cmdRefresh.Enabled = True
                cmdPrint.Enabled = True
                cmdRefresh.Focus()
            End If '--instr-
        End If '--selected-
    End Sub '--period.--
    '= = = = = = = =
    '-===FF->


    '--Refresh Report..-

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object,
                                  ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        Dim Index As Short

        '-- Do it..--
        '- PREVIEW only-
        Call mbReports_Execute(miReportType, mbShowDetailLines, True)

        labChooseReport.Enabled = True


    End Sub '--refresh-
    '= = = = = = = = =  =

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

        '--PRINT-  (NOT preview)-

        Call mbReports_Execute(miReportType, mbShowDetailLines, False)

        labChooseReport.Enabled = True

    End Sub
    '= = = = = = = =
    '-===FF->

    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboPrinters_SelectedIndexChanged(ByVal sender As System.Object,
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboPrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboPrinters.SelectedIndex >= 0) Then
            msSelectedPrinterName = cboPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_PrtSettingKey, msSelectedPrinterName) Then
                MsgBox("Failed to save RA Colour (A4) printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboPrinters-
    '= = = = = = = = = = = = =  =


    'UPGRADE_WARNING: Event ChkWarranty.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub ChkWarranty_CheckStateChanged(ByVal eventSender As System.Object,
                                              ByVal eventArgs As System.EventArgs) Handles ChkWarranty.CheckStateChanged
        Dim ix As Integer

        '==For ix = 0 To K_MAXREPORTOPTION
        '==OptReport(ix).Value = False      '--clear all reports.--
        '--CmdCopy.Enabled = False
        '==Next ix
    End Sub '--warranty..--
    '= = = = = = = = = =
    '-===FF->
    '= = = = = = = = =  = =

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        '====mCnnJet.Close

        '== Target-Build-4284  (Started 01-Nov-2020)
        '== Target-Build-4284  (Started 01-Nov-2020)
        'mCnnSql.Close()
        'mCnnShape.Close()
        'mCnnSql = Nothing
        'mCnnShape = Nothing
        '== END Target-Build-4284  (Started 01-Nov-2020)
        '== END Target-Build-4284  (Started 01-Nov-2020)


        '==Me.Hide
        '= Me.Close()

    End Sub '--exit--
    '= = = = = = = =


    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    'Private Sub frmJobReports_FormClosing(ByVal eventSender As System.Object,
    '                                      ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    Dim intCancel As Boolean = eventArgs.Cancel
    '    Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
    '    Dim ix As Integer

    '    'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
    '    '= ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'

    '    Select Case intMode
    '        Case System.Windows.Forms.CloseReason.FormOwnerClosing  '=, vbFormCode
    '            intCancel = 0 '--let it go--
    '        Case System.Windows.Forms.CloseReason.WindowsShutDown, System.Windows.Forms.CloseReason.TaskManagerClosing
    '            '= mCnnSql.Close()
    '            '= mCnnShape.Close()
    '            intCancel = 0 '--let it go--
    '        Case System.Windows.Forms.CloseReason.UserClosing
    '            '= mCnnSql.Close()
    '            '= mCnnShape.Close()
    '            '==For ix = 0 To K_MAXJOBTABS
    '            '===Unload Me
    '            intCancel = 0 '--let it go--
    '            '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '            '--intCancel = 0 '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = intCancel
    'End Sub '--unload--
    '= = = = = = = = = = = = = =


End Class  '-frmJobReports-
'= = = = = = = = = = =  == = = 
'== end form. ==
