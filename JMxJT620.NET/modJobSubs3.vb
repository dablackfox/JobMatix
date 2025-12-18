Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb

Module modJobSubs3
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	
	'-- Jobs-related functions..---
	'---- created for Jobmatix.. 19-Oct-2010==
	
	'--grh 30-Mar-2011= Rev-2084== Fix function to load Priority Decsriptors count..-
	'--grh 06Aug-2011= Rev-2918==  function to return constant TermsAndConditions.-
	
	'--grh 11-Oct-2011= Rev-3010==  functions to load listview...-
	
    '--grh 23-Nov-2011= Rev-3013 ++==  UPGRADED vb.net version.....-

    '==grh= 03-Mar-2012= ver:3031==  "clsStrDictionary" REPLACES "Scripting.Dictionary"--

    '==grh= 02-Jun-2012= ver:3059.1== Fix "gsFormat"to protect against non-conforming num/date data.--
    '===

    '==grh= 10-Mar-2014= ver:3083.310== DateDiff Function to bypass legacy VB6 DateDiff. etc.--
    '===
    '= = = = = = = = = == = = = =  == == = = 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  19-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==   grh JobMatix33.. 25Feb2016=
    '==      3311.225=  Now Using  mSysInfo1 As clsSystemInfo
    '==
    '==      3311.330=  All LabourRates to be gotten from RetailHost using Priority/Onsite
    '==                   Add function gbGetPriorityInfo..
    '==
    '==  -- 3311.0817- 17Aug2016-
    '==         >>-- 3311.0817-   Fixed 'gbReformatJobCustomerName' for updating in TRANSACTION !
    '==
    '==  -- 3403.0607- 07Jun2017-
    '==       >> Update "gbShowAllParts" to include "cost" (orig cost_ex) Cost  Price in collection.-
    '==
    '==   v3.4.3403.0909 -- 09Sep2017= - 
    '==      -- 'gbShowAllParts' Fix Second Hand parts price in total.
    '==            Don't use stock price if zero, as this could be second-hand part
    '==                   (priced when put into job.. 
    '==
    '==   v3.4.3431.0527 -- 27May2018= - 
    '==      -- "gbGetPriorityInfo" No key, or blank Descr.  Send back temp Description.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '== 3083.310== DateDiff Function to bypass legacy VB6 DateDiff. etc.--

    Public Function gIntDateDiffDays(ByVal date1 As DateTime, _
                                     ByVal date2 As DateTime) As Integer
        Dim timespan1, timespan2 As TimeSpan

        Try
            timespan1 = date1.Subtract(DateTime.MinValue)  '-- interval from beginning of time..
            timespan2 = date2.Subtract(DateTime.MinValue)
            gIntDateDiffDays = timespan2.Days - timespan1.Days

        Catch ex As Exception
            MsgBox("Error in 'gIntDateDiffDays' function.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            gIntDateDiffDays = 0
        End Try

    End Function  '--gIntDateDiffDays-
    '= = = = = = = = = == =
    '-===FF->


    Public Function gsGetTermsAndConditions() As String
        Dim sTerms As String

        sTerms = vbCrLf
        sTerms = sTerms & "1. The goods listed above are tendered for service and are left "
        sTerms = sTerms & "at the Customer's sole risk."
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "2. Toshiba warranty covers hardware faults only.  If no fault is "
        sTerms = sTerms & "found, or if the fault relates to software, then our normal "
        sTerms = sTerms & "charges will apply."
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "3. If any goods, or faulty components forming part of the goods, "
        sTerms = sTerms & "is not under warranty, or where there is a modification to any "
        sTerms = sTerms & "component of the goods, or if the warranty has been voided by "
        sTerms = sTerms & "the Customer, then the minimum service charge will apply, even "
        sTerms = sTerms & "if the fault is not rectified."
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "4. No work further to the above service will be performed "
        sTerms = sTerms & "without the prior consent of the Customer.  If authorised, "
        sTerms = sTerms & "charges will at the current hourly rate relevant to the agreed "
        sTerms = sTerms & "priority level plus cost of parts, or for an agreed fixed "
        sTerms = sTerms & "service fee.  Goods presented for repair may be replaced by "
        sTerms = sTerms & "refurbished goods of the same type rather than being repaired.  "
        sTerms = sTerms & "Refurbished parts may be used to repair the goods."
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "5. While &&BizShortName makes every effort to look after goods "
        sTerms = sTerms & "under our care, no responsibility can be taken for any goods "
        sTerms = sTerms & "lost, damaged or stolen whilst on our premises (monitored alarm "
        sTerms = sTerms & "and patrolled). &&BizShortName endeavours to provide the best "
        sTerms = sTerms & "protection for your data wherever possible. However, the nature "
        sTerms = sTerms & "of that same data makes it impossible to guarantee that no data "
        sTerms = sTerms & "will be lost through circumstances beyond our control. This "
        sTerms = sTerms & "being the case &&BizShortName will not accept liability for any "
        sTerms = sTerms & "data loss whatsoever."
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "6. Please note that all faults due to software errors, viruses and trojans "
        sTerms = sTerms & "are specifically not covered under warranty, and that a minimum service charge "
        sTerms = sTerms & "will apply, even if the fault can not be rectified.. "
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "7. You agree to make full payment on collection of the goods, and confirm "
        sTerms = sTerms & "that you will collect the goods within 30 days of the notice of completion. "
        sTerms = sTerms & "In the event that you do not collect the Goods within this time frame, you "
        sTerms = sTerms & "agree to pay any disposal and/or recovery charges.  Unclaimed goods will be "
        sTerms = sTerms & "disposed of after six months after completion of assessment or completion of "
        sTerms = sTerms & "entire job."
        sTerms = sTerms & vbCrLf & vbCrLf
        sTerms = sTerms & "8. By signing this form you (the signatory) accept these terms and conditions "
        sTerms = sTerms & "and hereby agree to exempt &&BizShortName (including all individuals "
        sTerms = sTerms & "associated with &&BizShortName) from prosecution for any consequential or "
        sTerms = sTerms & "inconsequential loss or damage to goods or data."

        gsGetTermsAndConditions = sTerms

    End Function '--TermsAndConditions--
    '= = = = = = = = = == =
    '-===FF->

    '--NB: v3.1.3101 -- ADO.net oleDb= 
    '=   gsFormat MOVED to modSqlSupport--

    '-- Load  listView from recordset..--
    '-- Load  listView from recordset..--
    '== v3.1.3101 -- ADO.net oleDb=

    Public Function gbLoadListView(ByRef rs1 As DataTable, _
                                    ByRef ListView1 As System.Windows.Forms.ListView) As Integer

        Dim s1 As String
        Dim dataCol1 As DataColumn '--fldx As ADODB.Field
        Dim lngNoCols, ix As Integer
        Dim lCount As Integer
        Dim item1 As System.Windows.Forms.ListViewItem

        lngNoCols = 0
        lCount = 0
        gbLoadListView = -1
        '-- create column headers...--
        ListView1.Items.Clear()
        ListView1.Columns.Clear()
        For Each dataCol1 In rs1.Columns   '== fldx In rs1.Fields
            lngNoCols = lngNoCols + 1
            s1 = "" & dataCol1.ColumnName  '== CStr(fldx.Name)
            ListView1.Columns.Add("", s1, CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListView1.Width) \ 7)))
        Next dataCol1  '=fldx '--fldx  --
        '--MsgBox "Headers loaded...", vbInformation

        '--fill list box with record fields--
        If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ok.. not empty--
            '==rs1.MoveFirst()
            ListView1.Items.Clear() : lCount = 0
            '--scan recordset and load--
            For Each dataRow1 As DataRow In rs1.Rows
                '---load current item--
                For ix = 0 To (lngNoCols - 1) '--ALL flds..-
                    '==ADO.net oleDb=  s1 = gsFormat(rs1.Fields(ix).Value, rs1.Fields(ix).Type, rs1.Fields(ix).DefinedSize)
                    s1 = dataRow1.Item(ix).ToString
                    If ix = 0 Then
                        item1 = ListView1.Items.Add(s1)  '--First col does CREATE..-
                    Else
                        item1.SubItems.Add(s1)
                    End If
                Next ix
                lCount = lCount + 1
                item1.Tag = CStr(lCount) '--ID of this part item..-
            Next dataRow1
            '== While (Not rs1.EOF)
            '== rs1.MoveNext()
            '== End While '--eof
        Else
            '==  MsgBox sEmptyMsg, vbInformation
        End If '--not empty--
        gbLoadListView = lCount
    End Function '--load list..-
    '= = = = = = =  =
    '-===FF->

    '-- Load  listView from collection..--
    '-- Load RM Quotes listView from recordset..--

    Public Function gbLoadListViewFromCollection(ByRef colList As Collection, _
                                                  ByRef ListView1 As System.Windows.Forms.ListView) As Boolean

        Dim s1 As String
        Dim colFldx As Collection
        Dim colRecord As Collection
        Dim lngNoCols, ix As Integer
        Dim lCount As Integer
        Dim lngType, lngSize As Integer
        Dim v1 As Object
        Dim item1 As System.Windows.Forms.ListViewItem

        lngNoCols = 0
        lCount = 0
        '-- create column headers...--
        ListView1.Items.Clear()
        ListView1.Columns.Clear()

        If (colList.Count() > 0) Then '--ok.. not empty--
            colRecord = colList.Item(1) '--  get first reord..-
            '--  get all column names..--
            For Each colFldx In colRecord
                lngNoCols = lngNoCols + 1
                s1 = "" & CStr(colFldx.Item("Name"))
                ListView1.Columns.Add(s1) '--, ListView1.Width \ 8
            Next colFldx '--fldx  --
        End If '--count.-
        '--MsgBox "Headers loaded...", vbInformation

        '--fill list box with record fields--
        If (colList.Count() > 0) Then '--ok.. not empty--
            '==rs1.MoveFirst
            ListView1.Items.Clear() : lCount = 0
            '--scan recordset and load--
            '== While (Not rs1.EOF)
            For Each colRecord In colList
                '---load current item--
                'UPGRADE_ISSUE: MSComctlLib.ListItems method ListItems.Add was not upgraded. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
                '== item1 = ListView1.Items.Add()
                '==item1.Text = s1
                '==For ix = 1 To lngNoCols - 1 '--remainder of flds..-
                ix = 0
                For Each colFldx In colRecord
                    v1 = colFldx.Item("value")
                    lngType = CInt(colFldx.Item("type"))
                    lngSize = CInt(colFldx.Item("definedSize"))
                    s1 = gsFormat(v1, lngType, lngSize)
                    If ix = 0 Then
                        '== item1.Text = s1
                        item1 = ListView1.Items.Add(s1)
                    Else
                        item1.SubItems.Add(s1)
                    End If
                    ix = ix + 1
                Next colFldx '== ix
                lCount = lCount + 1
                item1.Tag = CStr(lCount) '--ID of this part item..-
                '==== rs1.MoveNext
            Next colRecord '==Wend   '--eof
            '--Set ListView1.SelectedItem = ListView1.ListItems(1)
            '--show list--
            '--MsgBox s1   '--show last--
        Else
            If gbDebug Then MsgBox("No items to show...", MsgBoxStyle.Exclamation)
        End If '--not empty--
        gbLoadListViewFromCollection = True
    End Function '--load list..-
    '= = = = = = =  =
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
                s1 = Trim(Left(sRem, iPos - 1))
                sRem = Trim(Mid(sRem, iPos + 2)) '--drop cf/lf-
            Else
                s1 = Trim(sRem) : sRem = "" '--last session..-
            End If '--ipos.-
            '--dissect session..-
            If s1 <> "" Then
                iPos = InStr(s1, ":")
                If (iPos > 1) Then
                    sDate = Trim(Left(s1, iPos - 1))
                    If Not IsDate(sDate) Then
                        '--sDate = sDateCompleted  '--in case of bad stuff.--
                    Else
                        sDate = VB6.Format(CDate(sDate), "dd-mmm-yyyy") '--reformat..--
                    End If
                    s1 = Trim(Mid(s1, iPos + 1))
                    iPos2 = InStr(s1, "+")
                    If (iPos2 > 0) Then
                        sName = Trim(Left(s1, iPos2 - 1))
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


    '--wait form--

    '---  wait form---
    '===Public Sub FormWaitOn(message As String)

    '===Load FrmWait
    '===FrmWait.message = message
    '--Forms!frmWait.Caption = k_version
    '--frmWizard.txtMessage = frmWizard.txtMessage + message
    '===FrmWait.Show
    '===Screen.MousePointer = 11  '--HOURGLASS

    '===DoEvents
    '===End Sub
    '= = = = = = = = = = = = = = = = =


    '===Public Sub FormWaitOff()
    '--Forms!switchboard.message = ""

    '===FrmWait.Hide
    '--frmWait.MousePointer = 0  '--DEFAULT
    '===Screen.MousePointer = Default
    '--DoCmd.Close acForm, "frmWait"
    '===Unload FrmWait
    '===End Sub
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  THIS MAY NOT BE USED AT ALL..---
    '--  THIS MAY NOT BE USED AT ALL..---
    '--  THIS MAY NOT BE USED AT ALL..---

    '--  get RM recordset as Dictionary object.-
    '----- UCASE 1st col is key, 2nd col makes itemData..--

    '=3101= Public Function gbRMGetJetRSetAsDictionary(ByRef cnnJet As ADODB.Connection, _
    '=3101=                                                ByVal sSql As String, _
    '=3101=                                                 ByRef sdRows As clsStrDictionary) As Boolean
    '=3101= Dim sKey As String
    '=3101= Dim rs1 As ADODB.Recordset
    '=3101= Dim v1 As Object

    '=3101= gbRMGetJetRSetAsDictionary = False
    '=3101= '--sSql = "Select * from [staff] WHERE staff_id=" + CStr(lngStaffId)
    '=3101=     System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '=3101=     If Not gbGetRst(cnnJet, rs1, sSql) Then
    '=3101=         MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
    '=3101=         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '=3101=         Exit Function
    '=3101=     End If
    '=3101= '--txtMessages.Text = ""
    '=3101=     If Not (rs1 Is Nothing) Then
    '=3101=         sdRows = New clsStrDictionary  '== Scripting.Dictionary
    '=3101=         If Not (rs1.BOF And rs1.EOF) Then '--not empty-
    '=3101=             rs1.MoveFirst()
    '=3101=             While (Not rs1.EOF)
    '=3101= '--return 1st 2 cols each row..-
    '=3101=                 sKey = rs1.Fields(0).Value
    '=3101=                 v1 = rs1.Fields(1).Value
    '=3101=                 If Not sdRows.Exists(UCase(sKey)) Then '--can add.--
    '=3101=                     sdRows.Add(UCase(sKey), v1)
    '=3101=                 End If
    '=3101=                 rs1.MoveNext()
    '=3101=             End While '-eof-
    '=3101=         End If '==empty.-
    '=3101=         gbRMGetJetRSetAsDictionary = True
    '=3101=     End If '--rs-
    '=3101=     rs1 = Nothing
    '=3101=     System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
    '=3101= End Function '--get rs..-
    '= = = = = = = = = = = = = =
    '-===FF->


    '-- Caller's SCRATCHPAD for Changes..-
    '=== Private mlTempPartsCount As Long   '--array count..-
    '====Private malTempPartsIndexes() As Long   '-- -1=Deleted, 0=NewRecord, 1..n=ExistingTableRecordId  --
    '-------                                      (for NewRecord, collection is in mavTempNewData at same index..)-
    '====Private mavTempNewParts() As Variant    '-- FldCollection  if this PART has been added, or Nothing.-
    '====Private maCurTempPartsCosts() As Currency   '-- cost of parts in parts listbox..--
    '====Private mabTempPartsCostUpdated() As Boolean '-- cost of part has changed since PartRecord last written...--
    '==                                            '--  has to be updated in table..--
    '-- END of  SCRATCHPAD for Changes..-
    '= = = = = = = = = = =  =  = =  =


    '--  build listBox of PARTS added to this job so far..--
    '--  build listBox of PARTS added to this job so far..--
    '==== Public Function gbShowAllParts(bUpdatePrices As Boolean, _
    ''====                                bQuotation As Boolean, _
    ''====                                cnnJobs As ADODB.connection, _
    ''====                                cnnJet As ADODB.connection, _
    ''====                                ByVal curGST As Currency, _
    ''====                                ByVal lngJobId As Long, _
    ''====                                  ListViewParts As ListView, _
    ''====                                  ByRef lngTempPartsCount As Long, _
    ''====                                    aLngTempPartsIndexes() As Long, _
    ''====                                           avTempNewParts() As Variant, _
    ''====                                        aCurTempPartsCosts() As Currency, _
    ''====                                     abTempPartsCostUpdated() As Boolean, _
    ''====                                                ByRef curParts As Currency) As Boolean
    '-===FF->


    '------  NO..  DO NOT   also build arrays for Temp Part SCRATCHPAD..---
    '--------Ver: 2788-  JUST pass collection of current items with updated priceses..==

    Public Function gbShowAllParts(ByRef cnnJobs As OleDbConnection, _
                                    ByRef retailHost1 As _clsRetailHost, _
                                     ByVal curGST As Decimal, _
                                      ByVal lngJobId As Integer, _
                                       ByRef colJobItems As Collection, _
                                       ByRef curOrigPartsTotal As Decimal, _
                                        ByRef curPartsTotal As Decimal) As Boolean

        Dim sW As String
        '== Dim s2 As String
        Dim sShowNewCost, sShowCost As String
        '== Dim s3 As String
        Dim sBarcode As String
        Dim sSerialNo As String
        Dim sSql, sSqlCosts As String
        Dim sdCostRows As clsStrDictionary  '==  Scripting.Dictionary
        Dim sdAllowRenamingRows As clsStrDictionary  '==   Scripting.Dictionary
        Dim rs1 As DataTable '= ADODB.Recordset
        Dim sStockId, sPartId As String
        Dim sStaff As String
        Dim colItem As Collection
        Dim curCost As Decimal
        Dim curTotalOriginal As Decimal '--original prices..
        Dim curTotalCurrent As Decimal '-- at current prices..-
        Dim curNewCost As Decimal
        Dim lngAllowRenaming As Integer
        Dim bUpdatedCost As Boolean
        Dim col1 As Collection
        Dim colStockList As Collection
        Dim colRecord As Collection
        Dim colRecordList As Collection
        Dim sKey As String
        Dim v1 As Object

        gbShowAllParts = False
        colJobItems = New Collection

        '--  RE-CODE in MAINT after call..--
        '--  RE-CODE in MAINT after call..--
        '=======  !!!   If bQuotation Then Set ListViewParts.SmallIcons = ImageList1   '--for quoted parts..-

        curTotalOriginal = 0
        curTotalCurrent = 0
        sSql = "Select * from [jobParts] WHERE (PartJob_id=" & CStr(lngJobId) & ")"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(cnnJobs, rs1, sSql) Then
            MsgBox("Failed to get JobPARTS recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--build list box list of PARTS so far..-
        If Not (rs1 Is Nothing) Then
            sSqlCosts = ""
            If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                '--  get dictionary of latest sell prices for relevant parts..-
                '--   FIRST- get all stock nos in this job..-
                colStockList = New Collection
                '== rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    colStockList.Add(Trim(CStr(dataRow1.Item("RMstock_id"))))
                Next dataRow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==   rs1.MoveNext()
                '== End While
                '--  get current cost and "allow_renaming" values for selected parts..-
                sdCostRows = New clsStrDictionary  '==   Scripting.Dictionary
                sdAllowRenamingRows = New clsStrDictionary  '==   Scripting.Dictionary
                If retailHost1.stockGetStockList(colStockList, colRecordList) Then
                    For Each colRecord In colRecordList
                        sKey = Trim(CStr(colRecord.Item("Stock_id")("value")))
                        v1 = colRecord.Item("sell")("value")
                        If Not sdCostRows.Exists(UCase(sKey)) Then '--can add.--
                            sdCostRows.Add(UCase(sKey), v1)
                        End If
                        v1 = colRecord.Item("allow_renaming")("value")
                        If Not sdAllowRenamingRows.Exists(UCase(sKey)) Then '--can add.--
                            sdAllowRenamingRows.Add(UCase(sKey), v1)
                        End If
                    Next colRecord '--record.-
                End If
                '-- now load parts..-
                '== rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to list box for job..
                    '-- RECORDSET is from JOBPARTS Table..--
                    sW = " "
                    If (UCase(dataRow1.Item("IsWarrantyPart")) = "Y") Then sW = "W"
                    '==s1 = Space(4)
                    sPartId = Trim(CStr(dataRow1.Item("part_id")))
                    sStockId = Trim(CStr(dataRow1.Item("RMStock_id")))
                    sBarcode = Trim(CStr(dataRow1.Item("RMBarcode")))
                    sSerialNo = Trim(CStr(dataRow1.Item("PartSerialNumber")))
                    sStaff = Trim(dataRow1.Item("servicedByStaffName"))
                    '==s2 = Space(28)
                    '==LSet s2 = Trim(Left(rs1("RMDescription"), 28))
                    curCost = CDec(dataRow1.Item("RMSell"))
                    '--  check latest cost..-
                    bUpdatedCost = False
                    curOrigPartsTotal = curOrigPartsTotal + curCost
                    '===If bUpdatePrices Then
                    '=3403.909= Don't use Stock price if not GT zero.--
                    If sdCostRows.Exists(sStockId) AndAlso (CDec(sdCostRows(sStockId)) > 0) Then
                        curNewCost = CDec(sdCostRows(sStockId))
                        curNewCost = curNewCost + ((curNewCost * curGST) / 100)
                    Else
                        curNewCost = curCost '--??--
                    End If
                    lngAllowRenaming = 0
                    If sdAllowRenamingRows.Exists(sStockId) Then '--have a value--
                        If CBool(sdAllowRenamingRows(sStockId)) = True Then lngAllowRenaming = 1
                    End If
                    '===End If  '--update..-
                    curTotalCurrent = curTotalCurrent + curNewCost
                    '-- (in Main.) mark new cost with ASTERISK..--
                    sShowCost = FormatCurrency(curCost, 2)
                    sShowNewCost = FormatCurrency(curNewCost, 2)
                    '==sStaff = Space(8)
                    sStaff = Left(dataRow1.Item("servicedByStaffName"), 8)

                    '-- Collect all column items..--
                    colItem = New Collection
                    colItem.Add(sPartId, "part_id")
                    colItem.Add(sStockId, "stock_id")
                    colItem.Add(Trim(dataRow1.Item("RMCat1")), "Cat1")
                    colItem.Add(Trim(dataRow1.Item("RMCat2")), "Cat2")
                    colItem.Add(Trim(dataRow1.Item("RMDescription")), "Description")
                    '=- 3403.607-  Include cost Price (ex).
                    colItem.Add(dataRow1.Item("RMCost"), "cost")
                    colItem.Add(sShowCost, "Orig_SellPrice")
                    colItem.Add(sShowNewCost, "Upd_SellPrice")
                    colItem.Add(lngAllowRenaming, "allow_renaming")
                    colItem.Add(sBarcode, "Barcode")
                    colItem.Add(sSerialNo, "SerialNo")
                    colItem.Add(sW, "Wty")
                    colItem.Add(sStaff, "servicedByStaffName")
                    '--  add item to list..--
                    colJobItems.Add(colItem)
                Next dataRow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==  rs1.MoveNext()
                '= End While '-eof-
            End If '--empty..-
            gbShowAllParts = True
            '== rs1.Close()
        End If '--rs-
        '===curParts = curTotalParts
        curOrigPartsTotal = curOrigPartsTotal
        curPartsTotal = curTotalCurrent
        '--  RE-CODE in MAINT after call..--
        '--  RE-CODE in MAINT after call..--
        '=======  !!!  Call mbShowFullCost  !!!   =====

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
        sdCostRows = Nothing
    End Function '-- showParts.--
    '= = = = = = =  =  = =
    '-===FF->

    '--Rev-2788 ==   dissect GoodsInCare  DB string..--
    '----  return as collection of item collections..==

    Public Function gbDecodeGoodsIncare(ByRef sInitialGoodsinCare As String, _
                                         ByRef colGoodsInCare As Collection) As Boolean
        Dim L1, ix, iPos, kx, lngCount As Integer
        Dim va1 As Object
        Dim s1 As String
        Dim sRem As String
        Dim asGoodsItems() As String
        Dim sType, sBrand As String
        Dim sModel, sSerial As String
        '==Dim item1 As ListItem
        Dim colItem1 As Collection

        gbDecodeGoodsIncare = False
        On Error GoTo DecodeGoodsIncare_Error
        Erase asGoodsItems
        lngCount = 0
        colGoodsInCare = New Collection
        If (sInitialGoodsinCare <> "") Then '-have some..-
            '--parse text str for CR/LF to separate goods items..
            sRem = sInitialGoodsinCare
            iPos = InStr(sRem, vbCrLf)
            While (iPos > 0) Or (sRem <> "")
                'UPGRADE_WARNING: Lower bound of array asGoodsItems was changed from 1 to 0. Click for more: 
                'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                ReDim Preserve asGoodsItems(lngCount)
                If iPos > 0 Then
                    asGoodsItems(lngCount) = Left(sRem, iPos - 1)
                    sRem = Mid(sRem, iPos + 2)
                    iPos = InStr(sRem, vbCrLf)
                Else
                    asGoodsItems(lngCount) = sRem '--last-
                    sRem = ""
                End If
                lngCount = lngCount + 1
            End While
            '--  parse each goods item into type/brand/model/serialNO..
            If (lngCount > 0) Then
                For ix = 0 To (lngCount - 1)
                    sType = "" : sBrand = ""
                    sModel = "" : sSerial = ""
                    sRem = asGoodsItems(ix)
                    If InStr(sRem, vbTab) > 0 Then '--using tabs..-
                        va1 = Split(sRem, vbTab) '-- see if tab delimited..-
                        For kx = 0 To UBound(va1)
                            Select Case kx
                                Case 0
                                    sType = va1(kx)
                                Case 1
                                    sBrand = va1(kx)
                                Case 2
                                    sModel = va1(kx)
                                Case 3
                                    sSerial = va1(kx)
                            End Select
                        Next kx
                    Else '--old style.. << TYPE (Brand,Model) SerialNo: SERIALNO >>
                        iPos = InStrRev(LCase(sRem), "serialno:")
                        If (iPos > 0) Then
                            sSerial = Trim(Mid(sRem, iPos + 9))
                            sRem = Trim(Left(sRem, iPos - 1))
                        End If
                        If (Right(sRem, 1) = ")") Then
                            sRem = Trim(Left(sRem, Len(sRem) - 1)) '--drop right bracket.-
                            iPos = InStrRev(sRem, ",")
                            If (iPos > 0) Then '==extract model..-
                                sModel = Trim(Mid(sRem, iPos + 1))
                                sRem = Trim(Left(sRem, iPos - 1))
                            End If
                            iPos = InStrRev(sRem, "(") '--opening for brand,model--
                            If iPos > 0 Then
                                sBrand = Trim(Mid(sRem, iPos + 1))
                                sType = Trim(Left(sRem, iPos - 1))
                            Else '--no "("--
                                sType = Trim(sRem)
                            End If
                        Else '-no brand or model..-
                            sType = Trim(sRem)
                        End If
                    End If '--style..--
                    colItem1 = New Collection
                    colItem1.Add(sType, "Type")
                    colItem1.Add(sBrand, "Brand")
                    colItem1.Add(sModel, "Model")
                    colItem1.Add(sSerial, "SerialNo")
                    colGoodsInCare.Add(colItem1)
                    '--  load into listView..-
                    '-- add new row to listview--
                    '===Set item1 = ListViewGoods.ListItems.Add
                    '===item1.Text = sType  '--1st column.-
                    '===item1.ListSubItems.Add , , sBrand   '-- sBrand
                    '===item1.ListSubItems.Add , , sModel   '-sModel
                    '===item1.ListSubItems.Add , , sSerial   '--sSerialNo
                Next ix
            End If
        End If '--have goods..
        gbDecodeGoodsIncare = True

        Exit Function

DecodeGoodsIncare_Error:
        L1 = Err().Number
        MsgBox("Runtime Error in DecodeGoods function.." & vbCrLf & "Error is " & L1 & " = " & ErrorToString(L1))
    End Function '--decode..--
    '= = = = = = =  =  = =
    '-===FF->

    '--Rev-2788 ==   ENCODE GoodsInCare  DB string..--
    '----  FROM collection of item collections..==

    Public Function gbEncodeGoodsIncare(ByRef colGoods As Collection, _
                                         ByRef sResultGoods As String, _
                                           ByRef sDisplayGoods As String) As Boolean
        Const k_TABPRINTBRAND As Short = 34
        Const k_TABPRINTMODEL As Short = 52

        Dim sResult As String
        Dim L1 As Integer
        Dim sDisplayResult As String
        Dim colItem1 As Collection

        gbEncodeGoodsIncare = False
        sResult = ""
        sDisplayResult = ""
        If Not (colGoods Is Nothing) Then
            For Each colItem1 In colGoods
                If (sResult <> "") Then sResult = sResult & vbCrLf
                If (sDisplayResult <> "") Then sDisplayResult = sDisplayResult & vbCrLf
                sResult = sResult & colItem1.Item("Type") & vbTab & colItem1.Item("Brand") & vbTab & _
                                                             colItem1.Item("Model") & vbTab & colItem1.Item("SerialNo")
                '===     msPrintGoodsInCare = msPrintGoodsInCare + "-" + item1.Text + _
                ''===                           vbTab + Format(k_TABPRINTBRAND, "000") + " (" + item1.SubItems(1) + ", " + _
                ''===                                       vbTab + Format(k_TABPRINTMODEL, "000") + item1.SubItems(2) + ")"
                sDisplayResult = sDisplayResult & colItem1.Item("Type") & vbTab & _
                                    VB6.Format(k_TABPRINTBRAND, "000") & " (" + colItem1.Item("Brand") & ", " & vbTab & _
                                      VB6.Format(k_TABPRINTMODEL, "000") & colItem1.Item("Model") & ") " & _
                                                                                     "S/n:" & colItem1.Item("SerialNo")
            Next colItem1 '--item1..-
        End If '--nothing..-
        sResultGoods = sResult
        sDisplayGoods = sDisplayResult
        Exit Function

EncodeGoodsIncare_Error:
        L1 = Err().Number
        MsgBox("Runtime Error in ENCODE Goods function.." & vbCrLf & "Error is " & L1 & " = " & ErrorToString(L1))
    End Function '--encode..-
    '= = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '=3311.330---gbGetPriorityInfo--

    '- INPUT is Priority and If OnSite..-
    '-- Result is Labour Price and Description.
    '-- NB:  Returns original rate info if no barcodes setup..eg.  "DESCRIPTIONPRIORITY1"-

    Public Function gbGetPriorityInfo(ByRef cnnJobs As OleDbConnection, _
                                      ByVal sPriority As String, _
                                        ByVal bIsOnSite As Boolean, _
                                        ByRef clsRetailHost1 As _clsRetailHost, _
                                          ByRef curHourlyRateInc As Decimal, _
                                          ByRef strDescription As String) As Boolean
        Dim sKey, sDescrKey As String
        Dim lngCount As Integer
        Dim s1, sGSTPercentage As String
        Dim sBarcode As String
        Dim sDescr As String
        Dim decGST = 10.0
        Dim decSellEx As Decimal

        gbGetPriorityInfo = False
        Try
            '-- get correct priority key and info..
            Dim SysInfo1 As New clsSystemInfo(cnnJobs)
            sGSTPercentage = SysInfo1.item("GSTPERCENTAGE")
            If IsNumeric(sGSTPercentage) Then decGST = CDec(sGSTPercentage)
            If bIsOnSite Then
                If (sPriority = "1") Or (sPriority = "H") Then
                    sKey = "LabourRateOnSiteP1RetailBarcode"
                ElseIf (sPriority = "2") Then
                    sKey = "LabourRateOnSiteP2RetailBarcode"
                ElseIf (sPriority = "3") Then
                    sKey = "LabourRateOnSiteP3RetailBarcode"
                Else
                    MsgBox("Invalid onsite priority", MsgBoxStyle.Exclamation)
                    Exit Function
                End If
            Else  '-workshop-
                If (sPriority = "1") Or (sPriority = "H") Or (sPriority = "Q") Then
                    sKey = "LabourRateP1RetailBarcode"
                 ElseIf (sPriority = "2") Or (sPriority = "B") Then
                    sKey = "LabourRateP2RetailBarcode"
                ElseIf (sPriority = "3") Then
                    sKey = "LabourRateP3RetailBarcode"
                Else
                    MsgBox("Invalid priority", MsgBoxStyle.Exclamation)
                    Exit Function
                End If
            End If
            '-- get MYOB/POS stock barcode.-
            If SysInfo1.exists(sKey) AndAlso (SysInfo1.item(sKey) <> "") Then
                '-ok-
                sBarcode = SysInfo1.item(sKey)
                Dim colRecord As Collection
                If Not clsRetailHost1.stockGetStockRecord(sBarcode, -1, colRecord) Then
                    MsgBox("No Stock record for barcode:  " & sBarcode, MsgBoxStyle.Exclamation)
                    Exit Function
                Else '-ok-
                    '-- get sell price and add GST to show Labour Rate.
                    sDescr = colRecord.Item("description")("value")
                    If (Left(sDescr, 1) <> sPriority) Then
                        sDescr = sPriority & ". " & sDescr  '--auto number if needed..--
                    End If
                    strDescription = sDescr
                    decSellEx = CDec(colRecord.Item("sell")("value"))  '-- Sell is EX GST-
                    curHourlyRateInc = decSellEx + ((decSellEx * decGST) / 100)
                    gbGetPriorityInfo = True
                End If
            Else '-returns original..  barcode. not set up..
                sKey = "LabourHourlyRatePriority" & sPriority     '- in case no barcode setup done..
                sDescrKey = "DESCRIPTIONPRIORITY" & sPriority
                If SysInfo1.exists(sKey) AndAlso (SysInfo1.item(sKey) <> "") Then
                    curHourlyRateInc = CDec(SysInfo1.item(sKey))
                    strDescription = SysInfo1.item(sDescrKey)
                    '== MsgBox("Please Note: No Stock barcode for Labour Priority:  " & sPriority & vbCrLf & _
                    '==        " can be found in the Jobmatix settings.." & vbCrLf & _
                    '==        " Original JobMatix rates will be used.." & vbCrLf & vbCrLf & _
                    '==        "To stop seeing this message, " & vbCrLf & _
                    '==       "  set up all the labour rates in the Retail Stock System, " & vbCrLf & _
                    '==       " and then set up the barcodes in the Jobmatx Setup Screen.", MsgBoxStyle.Exclamation)
                    gbGetPriorityInfo = True
                Else
                    '=3431.0527=  No key, or blank Descr.
                    strDescription = sPriority & ". Temp Priority Descr. " & sPriority
                    curHourlyRateInc = 1.0
                    gbGetPriorityInfo = True
                End If
            End If  '- barcode exists-
        Catch ex As Exception
            MsgBox("Runtime Error in gbGetPriorityInfo function.." & vbCrLf & _
                      "Error is " & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
    End Function  '- gbGetPriorityInfo-
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '==3311.331- -- NEW get priority Descriptors..--
    '==3311.331--   NEW  get priority Descriptors..--

    Public Function gbGetPriorityDescriptorsEx(ByRef cnnJobs As OleDbConnection, _
                                               ByVal bIsOnSite As Boolean, _
                                               ByRef clsRetailHost1 As _clsRetailHost, _
                                                  ByRef colPriorities As Collection) As Boolean
        Dim decRate As Decimal
        Dim sDescr As String

        gbGetPriorityDescriptorsEx = False
        colPriorities = New Collection
        For intPriority As Integer = 1 To 3
            If gbGetPriorityInfo(cnnJobs, Trim(CStr(intPriority)), bIsOnSite, clsRetailHost1, decRate, sDescr) Then
                colPriorities.Add(sDescr)
            Else
                Exit Function
            End If
        Next intPriority
        gbGetPriorityDescriptorsEx = True
    End Function
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  get priority Descriptors..--
    '--  get priority Descriptors..--

    '==3311.330- This Is now OBSOLETE..--

    Public Function xxxx_gbGetPriorityDescriptors(ByRef cnnJobs As OleDbConnection, _
                                             ByRef colPriorities As Collection) As Boolean
        '= Dim sdSystemInfo As clsStrDictionary   '== Scripting.Dictionary
        '== Dim colSystemInfo As Collection
        '= Dim colInfo As Collection
        Dim sKey As String
        Dim lngCount As Integer
        Dim s1 As String
        '== Dim sPriority As String
        Dim sDescr As String
        '=3311-225-
        Dim SysInfo1 As New clsSystemInfo(cnnJobs)

        xxxx_gbGetPriorityDescriptors = False
        '== If gbLoadsystemInfo(cnnJobs, colSystemInfo, sdSystemInfo) Then
        colPriorities = New Collection
        lngCount = 0
        For Each sKey In SysInfo1.keys '= colInfo In colSystemInfo
            '=sKey = colInfo.Item("SystemKey")
            sDescr = SysInfo1.item(sKey)  '= colInfo.Item("SystemValue")
            s1 = Mid(sKey, 20, 1)
            If LCase(Left(sKey, 19)) = "descriptionpriority" Then
                If IsNumeric(s1) And (lngCount < 10) Then '--  Can have [0..9] max..--
                    If (Left(sDescr, 1) <> s1) Then sDescr = sDescr & s1 & ". " '--auto number if needed..--
                    colPriorities.Add(sDescr)
                    lngCount = lngCount + 1
                End If '--numeric-
            End If
        Next sKey '= colInfo '--colinfo=
        xxxx_gbGetPriorityDescriptors = True
        '= End If '--load
        '== sdSystemInfo = Nothing
        '== colSystemInfo = Nothing
        SysInfo1 = Nothing

    End Function '--get priorities..-
    '= = = = == =  == = = = = = = =  =

    '--convert variant array into collection -

    Public Function gbMakeCollection(ByRef array1 As Object, ByRef colResult As Collection) As Boolean
        Dim ix, lngLower As Integer
        Dim lngError As Integer

        gbMakeCollection = False
        On Error Resume Next
        lngLower = LBound(array1)
        lngError = Err().Number
        On Error GoTo 0
        If lngError <> 0 Then '--error-
            MsgBox("No array to convert..", MsgBoxStyle.Information)
            Exit Function
        End If '--error..-
        colResult = New Collection
        For ix = lngLower To UBound(array1)
            colResult.Add(array1(ix))
        Next ix

        gbMakeCollection = True

    End Function '-end make.--
    '= = = = = = = = = = = = =
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Precise PCs only..--
    '-- fix up CustomerName (Surname, giennames)--
    '-- << ,SPACE >> in CustomerName indicates crrect fromatting..-

    Public Function gbReformatJobCustomerName(ByRef cnnJobs As OleDbConnection) As Boolean

        Dim rsJobs As DataTable  '= ADODB.Recordset
        Dim sSql, sErrorMsg As String
        Dim s2, s1, s3 As String
        Dim lngaffected As Integer
        Dim lngCount, lngId, lngBatchCount As Integer
        Dim ix, iPos, lngRecCount As Integer
        Dim transaction1 As OleDbTransaction

        gbReformatJobCustomerName = False
        '--  find all jobs with multiple tokens in customerName, but NO << ,SPACE >>.-
        sSql = "SELECT Job_id, CustomerName from [jobs] " & _
               " WHERE (LEN(CustomerName)>3) AND " & _
                  "(PATINDEX('% %',RTRIM(LTRIM(CustomerName)))>1) AND (PATINDEX('%, %',CustomerName)<=0); " & vbCrLf
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(cnnJobs, rsJobs, sSql) Then
            MsgBox("Failed to get JOBs recordset.." & vbCrLf & _
                    "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '-- get reccount..-
        If Not (rsJobs Is Nothing) Then
            '== If (rsJobs.BOF And rsJobs.EOF) Then '--empty-
            '==   lngRecCount = 0
            '== Else '--move pointer to set up reccount
            '= rsJobs.MoveLast() '--RuntimeError if r/set empty !!--
            '== rsJobs.MoveFirst()
            '== lngRecCount = rsJobs.RecordCount
            '== '--rsVar = rs.GetString(adClipString)  '--rs.RecordCount)--
            '== End If '--empty--
            lngRecCount = rsJobs.Rows.Count
            If (lngRecCount > 0) Then '--some unformatted names exist..-
                '-- confirm reformating..-
                If MsgBox("There are " & lngRecCount & " Customer names needing reformatting.." & vbCrLf & _
                             " Do you want to continue to reformat these records ?" & vbCrLf, _
                                  MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    '== rsJobs.MoveFirst()
                    lngCount = 0 '--count updates..-
                    lngBatchCount = 0 '--count updates..-
                    sSql = "" '--accumulate updates..-
                    '= cnnJobs.BeginTrans()
                    transaction1 = cnnJobs.BeginTransaction
                    For Each dataRow1 As DataRow In rsJobs.Rows
                        lngId = CInt(dataRow1.Item("Job_id"))
                        s1 = CStr(dataRow1.Item("CustomerName")) '--get rs original-
                        iPos = InStr(s1, ", ") '-- already reformatted..-
                        If (iPos > 1) Then '--yes..-
                            '--s3 = UCase(Left(s1, iPos + 1)) + Mid(s1, iPos + 2)  '--just UCASE the Surname..-
                        Else '-- REFORMAT!! original is "given names ..  surname"
                            iPos = InStrRev(s1, " ") '-- END-token s/be surname..-
                            If (iPos > 0) And (iPos < Len(s1)) Then '--something-
                                s3 = Mid(s1, iPos + 1) '--surname from end..-
                                s2 = Left(s1, iPos - 1) '--geven names at left..-
                                If s2 <> "" Then s3 = s3 & ", " & s2 '--surname, givenNames..=
                            Else '-take all as surname-
                                s3 = (s1)
                            End If
                            '-- REFORMAT this job-cust.--
                            lngCount = lngCount + 1
                            lngBatchCount = lngBatchCount + 1
                            sSql = sSql & " UPDATE [Jobs] SET CustomerName='" & s3 & "' " & _
                                                                 " WHERE (Job_id=" & CStr(lngId) & "); " & vbCrLf
                            '-- Send 20 at a time.--
                            If (lngBatchCount >= 20) Then
                                '== GoSub ReformatUpdateBatch
                                MsgBox("Updating " & lngBatchCount & " rows..  sql is:" & vbCrLf & _
                                                                     sSql & vbCrLf, MsgBoxStyle.Information) '--test--
                                '=3311.817= Fixed for Transaction1..-
                                If Not gbExecuteSql(cnnJobs, sSql & vbCrLf, True, transaction1, lngaffected, sErrorMsg) Then
                                    '=If Not gbExecuteCmd(cnnJobs, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                                    '=done= transaction1.Rollback()
                                    MsgBox(vbCrLf & "Error! ROLLBACK was Executed !!" & vbCrLf & _
                                                  "Failed CustName SQL UPDATES to Jobs table: " & vbCrLf & _
                                                        sErrorMsg & vbCrLf & "Sql was:" & sSql & vbCrLf, MsgBoxStyle.Critical)
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                                    Exit Function
                                End If '--insert ok-
                                sSql = "" : lngBatchCount = 0

                            End If '--batch-
                        End If '-pos/ was reformatted..-
                    Next dataRow1
                    '== While (Not rsJobs.EOF)
                    '==   rsJobs.MoveNext()
                    '== End While
                    If (lngBatchCount > 0) Then '--part batch left.-
                        '== GoSub ReformatUpdateBatch
                        MsgBox("Updating " & lngBatchCount & " rows..  sql is:" & vbCrLf _
                                                     & sSql & vbCrLf, MsgBoxStyle.Information) '--test--
                        '=3311.817= Fixed for Transaction1..-
                        If Not gbExecuteSql(cnnJobs, sSql & vbCrLf, True, transaction1, lngaffected, sErrorMsg) Then
                            '=If Not gbExecuteCmd(cnnJobs, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                            '= cnnJobs.RollbackTrans()
                            '=3311.817= DONE- transaction1.Rollback()
                            MsgBox(vbCrLf & "Error! ROLLBACK was Executed !!" & vbCrLf & _
                                         "Failed CustName SQL UPDATES to Jobs table: " & vbCrLf & sErrorMsg & vbCrLf & _
                                                                          "Sql was:" & sSql & vbCrLf, MsgBoxStyle.Critical)
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            Exit Function
                        End If '--insert ok-
                        sSql = "" : lngBatchCount = 0
                    End If '--batch-
                    '== cnnJobs.CommitTrans()
                    transaction1.Commit()
                    MsgBox("OK.. WE have updated " & lngCount & " rows..", MsgBoxStyle.Information) '--test--
                End If '-yes..-
                gbReformatJobCustomerName = True
            End If '--reccount.-
            '== rsJobs.Close()
        End If '-- rs nothing..-
        rsJobs = Nothing
        Exit Function

    End Function '-reformat..-
    '= = = = = = =  =  = =

    '==  the end ==
End Module

'=== end subs.==