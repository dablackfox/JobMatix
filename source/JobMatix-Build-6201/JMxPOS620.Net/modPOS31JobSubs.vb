Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb

Module modPOS31JobSubs

    '==
    '==  grh JobMatixPOS 3.1.3103.0104 -  04-Jan-2015
    '==       >> Subs for Job Info for POS Job Delivery etc...
    '==
    '==     3401.0318- Tidy up Delivering Jobs..-
    '==              >>  Add function gbGetPriorityInfo to get Labour rates.
    '= = = =  = = = = = = = = = = = = = = == = =  = = = = == = = = = = = =  =

    '--  compute labour chargeable hours to date..-

    Public Function gCurComputeChargeableHours(ByVal sSessionTimes As String) As Decimal
        Dim sRem As String
        Dim sName, s1 As String
        Dim sDate As String
        Dim sTimeSpent As String
        Dim iPos2, iPos, iPos3 As Integer
        Dim bChargeable As Boolean
        Dim curResult As Decimal

        '-- Dissect accumulated session string..--
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

    '-- For POS Job Delivery.
    '--  build collection of PARTS added to this job so far..--
 
    Public Function gbCollectAllJobParts(ByRef cnnJobs As OleDbConnection, _
                                       ByVal lngJobId As Integer, _
                                       ByRef colJobItems As Collection) As Boolean

        Dim sW As String
        '== Dim s2 As String
        '== Dim sShowNewCost, sShowCost As String
        '== Dim s3 As String
        Dim sBarcode As String
        Dim sSerialNo As String
        Dim sSql, sSqlCosts As String
        '== Dim sdCostRows As clsStrDictionary  '==  Scripting.Dictionary
        '== Dim sdAllowRenamingRows As clsStrDictionary  '==   Scripting.Dictionary
        Dim rs1 As DataTable '= ADODB.Recordset
        Dim sStockId, sPartId As String
        Dim sStaff As String
        '= Dim col1 As Collection
        Dim colItem As Collection
        '= Dim colRecord As Collection
        '== Dim colRecordList As Collection
        '== Dim sKey As String
        '== Dim v1 As Object

        gbCollectAllJobParts = False
        colJobItems = New Collection

        '--  RE-CODE in MAINT after call..--
        '--  RE-CODE in MAINT after call..--
        '=======  !!!   If bQuotation Then Set ListViewParts.SmallIcons = ImageList1   '--for quoted parts..-

        sSql = "Select * from [jobParts] WHERE (PartJob_id=" & CStr(lngJobId) & ")"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(cnnJobs, rs1, sSql) Then
            MsgBox("Failed to get JobPARTS recordset.." & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & _
                                        vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--build list box list of PARTS so far..-
        If Not (rs1 Is Nothing) Then
            sSqlCosts = ""
            If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                '--  get dictionary of latest sell prices for relevant parts..-
                '--   FIRST- get all stock nos in this job..-
                '-- now load parts..-
                '== rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to collection for job..
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
                    '==sStaff = Space(8)
                    sStaff = Left(dataRow1.Item("servicedByStaffName"), 8)

                    '-- Collect all column items..--
                    colItem = New Collection
                    colItem.Add(sPartId, "part_id")
                    colItem.Add(sStockId, "stock_id")
                    colItem.Add(Trim(dataRow1.Item("RMCat1")), "Cat1")
                    colItem.Add(Trim(dataRow1.Item("RMCat2")), "Cat2")
                    colItem.Add(Trim(dataRow1.Item("RMDescription")), "Description")
                    '== colItem.Add(sShowCost, "Orig_SellPrice")
                    '== colItem.Add(sShowNewCost, "Upd_SellPrice")
                    '= colItem.Add(lngAllowRenaming, "allow_renaming")
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
            gbCollectAllJobParts = True
            '== rs1.Close()
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
        '= sdCostRows = Nothing
    End Function '-- showParts.--
    '= = = = = = =  =  = =
    '-===FF->

    '=3401.318 18Mar2017=  --gbGetPriorityInfo--
    '-- Mangled POS version 
    '- INPUT is Priority and If OnSite..-
    '-- Result id Stock Barcode for Labour..
    '--    But if no barcode thewnResult is Labour Price and Description.)
    '-- NB:  Returns original rate info if no barcodes setup..eg.  "DESCRIPTIONPRIORITY1"-

    Public Function gbGetPriorityInfoPOS(ByRef cnnJobs As OleDbConnection, _
                                           ByVal sPriority As String, _
                                            ByVal bIsOnSite As Boolean, _
                                             ByRef sBarcode As String, _
                                             ByRef curHourlyRateInc As Decimal, _
                                             ByRef strDescription As String) As Boolean
        Dim sKey, sDescrKey As String
        Dim lngCount As Integer
        Dim s1, sGSTPercentage As String
        '== Dim sBarcode As String
        Dim sDescr As String
        Dim decGST = 10.0
        Dim decSellEx As Decimal

        gbGetPriorityInfoPOS = False
        '= bUsingOldRateInfo = False
        sBarcode = ""
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
                gbGetPriorityInfoPOS = True
                'Dim colRecord As Collection
                'If Not clsRetailHost1.stockGetStockRecord(sBarcode, -1, colRecord) Then
                '    MsgBox("No Stock record for barcode:  " & sBarcode, MsgBoxStyle.Exclamation)
                '    Exit Function
                'Else '-ok-
                '    '-- get sell price and add GST to show Labour Rate.
                '    sDescr = colRecord.Item("description")("value")
                '    If (Left(sDescr, 1) <> sPriority) Then
                '        sDescr = sPriority & ". " & sDescr  '--auto number if needed..--
                '    End If
                '    strDescription = sDescr
                '    decSellEx = CDec(colRecord.Item("sell")("value"))  '-- Sell is EX GST-
                '    curHourlyRateInc = decSellEx + ((decSellEx * decGST) / 100)
                '    gbGetPriorityInfoPOS = True
                'End If
            Else '-returns original..  barcode. not set up..
                sKey = "LabourHourlyRatePriority" & sPriority     '- in case no barcode setup done..
                sDescrKey = "DESCRIPTIONPRIORITY" & sPriority
                If SysInfo1.exists(sKey) AndAlso (SysInfo1.item(sKey) <> "") Then
                    curHourlyRateInc = CDec(SysInfo1.item(sKey))
                    strDescription = SysInfo1.item(sDescrKey)
                    sBarcode = ""   '= NO barcode- bUsingOldRateInfo = True
                    gbGetPriorityInfoPOS = True
                End If
            End If  '- barcode exists-
        Catch ex As Exception
            MsgBox("Runtime Error in gbGetPriorityInfo function.." & vbCrLf & _
                      "Error is " & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
    End Function  '- gbGetPriorityInfo-
    '= = = = = = = = = = = = = = = = = = = = 

End Module '-modPOS31JobSubs-
'= = = = = = = = = = = = = =
