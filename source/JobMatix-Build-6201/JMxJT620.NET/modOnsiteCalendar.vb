Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
Imports VB = Microsoft.VisualBasic

Module modOnsiteCalendar

    '-- grh 04-Jun-2017-
    '-- Module for ON-SITE Exchange Calendar Appointments.
    '==
    '==  -- 3431.0505- 05-May-2018-
    '==        New Job/Amend- Send Exchange calendar update to DISK BG Queue..
    '==      Add BG Exchange Update BackgroundWorker now does handling cal to this. Exchange Work..
    '--       SO NO UI things here..  (Mo message Boxes !! No CURSOR stuff!!) 
    '==               And Send back error text.
    '==
    '==  -- 3431.0707- 07-July-2018-
    '==       Query result of New connection.
    '==
    '== -- Updated 3501.1105  05-Nov-2018=  
    '==       New Parameter (Duration) for the Exchange module.
    '==
    '== = = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==   Target-New-Build-4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build-4262 -- 18-Aug-2020--
    '==
    '==      --  From Main Form- BG Worker Exchange201-.   see modOnsiteCalendar.vb   
    '==             ie We need to put in more try/catch code into the Function mbFindJobAppointment  
    '==                to catch Karens's string crash (Martin email 12-Aug-2020/)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '== Target-Build-4284  (Re-Started 23-Nov-2020)
    '== Target-Build-4284  (Re-Started 23-Nov-2020)
    '== Target-Build-4284  (Re-Started 23-Nov-2020)
    '==
    '==  -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==

    Private mClsExchange201 As clsExchange20

    Private msExchangeUserLogin As String = ""
    Private msExchangeUserPassword As String = ""

    '= Private mbActivated As Boolean = False
    Private mClsSystemInfo1 As clsSystemInfo

    Private msExchangeLastErrorMsg As String = ""

    '= = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    '- Return the idx if Job Found.-
    '-- Look through the appointments (subjects) collection -
    '--     for subject that has "[#JobMatixJobNo: #nnn]" in the subject --

    '==   Target-New-Build-4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build-4262 -- 18-Aug-2020--


    'Private Function mbFindJobAppointment(ByRef colAllAppts As Collection, _
    '                                       ByVal intInputJobNo As Integer, _
    '                                       ByRef strIdxFound As String) As Boolean

    '    Dim sSubject, s1, s2, s3, s4, sRem, sIdx As String
    '    Dim intPos, intPos2, intPos3, intJobNo As Integer

    '    mbFindJobAppointment = False

    '    '-- Look for subject that has "[#JobMatixJobNo: #nnn]" in the subject --
    '    strIdxFound = ""
    '    intJobNo = -1
    '    For Each col1 As Collection In colAllAppts
    '        If (strIdxFound = "") Then  '--keep looking-
    '            sSubject = col1.Item("appt_subject")
    '            sIdx = col1.Item("idx")
    '            sRem = LCase(sSubject)
    '            While (Len(sRem) > 0)
    '                intPos = InStr(sRem, "[")
    '                If (intPos > 0) Then
    '                    s1 = Trim(Mid(sRem, intPos + 1))  '-get what follows-
    '                    sRem = s1  '-- in case this is not the full identifier we want.
    '                    '-- scan for closing "]"
    '                    intPos2 = InStr(s1, "]")
    '                    If (intPos2 > 0) Then
    '                        s2 = Trim(Mid(s1, 1, (intPos2 - 1)))
    '                        '-test-
    '                        '== MsgBox("Found: '" & s2 & "'")
    '                        s2 = Replace(s2, " ", "") '--delete all spaces.-
    '                        If (s2.Substring(0, 15) = "#jobmatixjobno:") Then
    '                            s3 = Mid(s2, 16)  '--drop that tag-
    '                            If ((s3.Substring(0, 1) = "#") And (s3.Length > 1)) AndAlso _
    '                                                               IsNumeric(Mid(s3, 2)) Then  '-get jobno-
    '                                intJobNo = CInt(Mid(s3, 2))
    '                                sRem = ""  '--all done this subject-
    '                                If intJobNo = intInputJobNo Then
    '                                    strIdxFound = sIdx
    '                                    mbFindJobAppointment = True
    '                                    '= Exit For
    '                                End If
    '                            End If
    '                        End If '-jobmatix-
    '                    End If  '-intpos2-
    '                Else  '-not found-
    '                    sRem = ""
    '                End If  '-intPos-
    '            End While  'sRem-
    '        End If  '-found-
    '    Next col1
    '    '= MsgBox("JobNo is: " & intJobNo & ";   Idx is:" & sIdxFound, MsgBoxStyle.Information)

    'End Function  '-find Job-
    '= = = = = =  = = = = = = =

    '- Return the idx if Job Found.-
    '-- Look through the appointments (subjects) collection -
    '--     for subject that has "[#JobMatixJobNo: #nnn]" in the subject --

    '==   Target-New-Build-4262 -- 18-Aug-2020--
    '==      -- e We need to put in more try/catch code into the Function mbFindJobAppointment  
    '==                to catch Karens's string crash (Martin email 12-Aug-2020/)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =

    Private Function mbFindJobAppointment(ByRef colAllAppts As Collection, _
                                           ByVal intInputJobNo As Integer, _
                                           ByRef strIdxFound As String) As Boolean

        Dim sSubject, s1, s2, s3, s4, sRem, sIdx As String
        Dim intPos, intPos2, intPos3, intJobNo As Integer

        mbFindJobAppointment = False

        '-- Look for subject that has "[#JobMatixJobNo: #nnn]" in the subject --
        strIdxFound = ""
        intJobNo = -1
        For Each col1 As Collection In colAllAppts
            If (strIdxFound = "") Then  '--keep looking-
                sSubject = col1.Item("appt_subject")
                sIdx = col1.Item("idx")
                sRem = LCase(sSubject)
                '==   Target-New-Build-4262 -- 18-Aug-2020--
                Try
                    While (Len(sRem) > 0)
                        intPos = InStr(sRem, "[")
                        If (intPos > 0) Then
                            s1 = Trim(Mid(sRem, intPos + 1))  '-get what follows-
                            sRem = s1  '-- in case this is not the full identifier we want.
                            '-- scan for closing "]"
                            intPos2 = InStr(s1, "]")
                            If (intPos2 > 0) Then
                                s2 = Trim(Mid(s1, 1, (intPos2 - 1)))
                                '-test-
                                '== MsgBox("Found: '" & s2 & "'")
                                s2 = Replace(s2, " ", "") '--delete all spaces.-
                                If (s2.Substring(0, 15) = "#jobmatixjobno:") Then
                                    s3 = Mid(s2, 16)  '--drop that tag-
                                    If ((s3.Substring(0, 1) = "#") And (s3.Length > 1)) AndAlso _
                                                                       IsNumeric(Mid(s3, 2)) Then  '-get jobno-
                                        intJobNo = CInt(Mid(s3, 2))
                                        sRem = ""  '--all done this subject-
                                        If intJobNo = intInputJobNo Then
                                            strIdxFound = sIdx
                                            mbFindJobAppointment = True
                                            '= Exit For
                                        End If
                                    End If
                                Else  '-not found-
                                    '==   Target-New-Build-4262 -- 18-Aug-2020--
                                    sRem = "" '-exit while..New-Build-4262
                                End If '-jobmatix-
                            Else  '-not found-
                                '==   Target-New-Build-4262 -- 18-Aug-2020--
                                sRem = "" '-exit while..New-Build-4262
                            End If  '-intpos2-
                        Else  '-not found-
                            sRem = "" '-exit while..New-Build-4262
                        End If  '-intPos-
                    End While  '-sRem-
                Catch ex As Exception
                    msExchangeLastErrorMsg = "Search for Job Appointmnt Failed in 'mbFindJobAppointment'.." & vbCrLf & _
                             ex.Message & vbCrLf
                    sRem = ""
                    Exit Function
                End Try
                '==END   Target-New-Build-4262 -- 18-Aug-2020--
            End If  '-found-
        Next col1
        '= MsgBox("JobNo is: " & intJobNo & ";   Idx is:" & sIdxFound, MsgBoxStyle.Information)

    End Function  '-find Job-
    '= = = = = =  = = = = = = =


    'Private Function mbExchangeInitialise()

    '    If mClsExchange201 Is Nothing Then
    '        '= txtCollection.Text = vbCrLf & "Linking to Exchange Service..."
    '        Application.DoEvents()

    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '        mClsExchange201 = New clsExchange20(msExchangeUserLogin, msExchangeUserPassword)
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    '        '= txtCollection.Text &= vbCrLf & " ok.."
    '    End If  '-nothing-

    'End Function  '--init-
    '= = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    '-- Add new Appointment.

    Private Function mbAddNewAppointment(ByVal intJobNo As Integer, _
                                          ByVal strSubject As String, _
                                          ByVal strBody As String, _
                                           ByVal strLocation As String, _
                                           ByVal datePromised As Date, _
                                            ByVal decDuration As Decimal) As Boolean
        Dim s1, s2, s3, sIdxFound, sAppId As String

        mbAddNewAppointment = False
        Dim sJobMatixMark As String = "[#JobMatixJobNo: #" & CStr(intJobNo) & "]"

        '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Not mClsExchange201.MakeAppointment(strSubject & "  " & sJobMatixMark, strBody, _
                                 datePromised, datePromised.AddHours(CInt(decDuration)), strLocation, sAppId) Then
            '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'MsgBox("Make New Appointmnt Failed.." & vbCrLf & _
            '         mClsExchange201.GetLastExchangeError, MsgBoxStyle.Exclamation)
            msExchangeLastErrorMsg = "Make New Appointmnt Failed.." & vbCrLf & _
                     mClsExchange201.GetLastExchangeError & vbCrLf
        Else
            '= strActionTaken = "Added"
            '==MsgBox("Make Appt done ok." & vbCrLf & "Appt.Id is: " & sAppId, MsgBoxStyle.Information)
            mbAddNewAppointment = True
        End If  '--make-
        '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function  '-mbAddNewAppointment
    '= = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    '-- ADD or UPDATE-
    '-- Update Appointment Date if exists, else update the DateStart..

    Private Function mbUpdateAppointment(ByVal intJobNo As Integer, _
                                          ByVal datePromisedOld As Date, _
                                          ByVal datePromisedNew As Date, _
                                            ByVal decDuration As Decimal, _
                                              ByRef strActionTaken As String) As Boolean
        Dim colAllAppts As Collection
        Dim sSubject, s1, s2, s3, sIdxFound, sAppId As String
        '= Dim intJobNo As Integer
        Dim date1, dateSearch, dateStart, dateEnd As Date

        mbUpdateAppointment = False
        strActionTaken = ""
        '-- make srch start just after midnight a month before ols ate.
        date1 = datePromisedOld.AddMonths(-1)
        dateSearch = New DateTime(date1.Year, date1.Month, date1.Day, _
                                  0, 1, 0, 0)
        '== Call mbExchangeInitialise()

        '= System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not mClsExchange201.GetAppointmentsForDate(dateSearch, dateSearch.AddMonths(3), colAllAppts) Then
            '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'MsgBox("get Appts list Failed.." & vbCrLf & _
            '                 mClsExchange201.GetLastExchangeError, MsgBoxStyle.Exclamation)
            msExchangeLastErrorMsg = "Get Appts list Failed.." & vbCrLf & _
                             mClsExchange201.GetLastExchangeError & vbCrLf
            Exit Function
        End If

        If (colAllAppts IsNot Nothing) Then  '= AndAlso (colAllAppts.Count > 1) Then
            '- test- show all-
            'txtCollection.Text = "Found these-" & vbCrLf & vbCrLf
            'For Each col1 As Collection In colAllAppts
            '    txtCollection.Text &= col1.Item("idx") & ": " & col1.Item("appt_subject") & vbCrLf
            '    txtCollection.Text &= "StartDate: " & CStr(col1.Item("appt_start")) & vbCrLf
            '    txtCollection.Text &= col1.Item("appt_item_id") & vbCrLf & vbCrLf
            'Next col1
            '== END test- 

            If mbFindJobAppointment(colAllAppts, intJobNo, sIdxFound) Then
                sSubject = colAllAppts.Item(sIdxFound)("appt_subject")
                dateStart = colAllAppts.Item(sIdxFound)("appt_start")
                dateEnd = colAllAppts.Item(sIdxFound)("appt_end")
                '-test-
                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                'If MsgBox("Found JobNo: " & intJobNo & ";   Idx is:" & sIdxFound & vbCrLf & _
                '          "  Start Date: " & CStr(dateStart) & _
                '                   "So- ok to UPDATE: " & sSubject & vbCrLf & _
                '                   " to " & CStr(datePromisedNew) & " ??", _
                '       MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes Then
                '    Exit Function
                'End If
                '-- ok- can now update.
                If Not mClsExchange201.UpdateAppointment(sIdxFound, _
                                         sSubject & " (Updated: " & Format(Now, "dd-MMM-yyyy") & ";)", _
                                         datePromisedNew, datePromisedNew.AddHours(CInt(decDuration))) Then
                    '= System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    'MsgBox("Appointment Date Update failed.." & vbCrLf & _
                    '         mClsExchange201.GetLastExchangeError, MsgBoxStyle.Exclamation)
                    msExchangeLastErrorMsg = "Appointment Date Update failed.." & vbCrLf & _
                                                               mClsExchange201.GetLastExchangeError() & vbCrLf
                Else '-ok-
                    '-  check if updated to new date.
                    strActionTaken = "Updated"
                    '= MsgBox("Date Update OK..", MsgBoxStyle.Information)
                    mbUpdateAppointment = True
                End If  '-update-
            Else  '--NOT found- must add-
                '-BUT not here-  No ActionTaken means NOT FOUND-
                mbUpdateAppointment = True
            End If '-find-
        End If  '--appts nothing-
        '= System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function  '-mbUpdateAppointment-
    '= = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    '== Target-Build-4284  (Started 04-Nov-2020)
    '== Target-Build-4284  (Started 04-Nov-2020)
    '==
    '==  -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..

    Private Function mbCancelAppointment(ByVal intJobNo As Integer,
                                           ByVal datePromisedOld As Date,
                                           ByVal datePromisedNew As Date,
                                                 ByRef strActionTaken As String) As Boolean

        Dim colAllAppts As Collection
        Dim sSubject, s1, s2, s3, sIdxFound, sAppId As String
        '= Dim intJobNo As Integer
        Dim date1, dateSearch, dateStart, dateEnd As Date

        mbCancelAppointment = False
        strActionTaken = ""
        '-- make srch start just after midnight a month before ols ate.
        date1 = datePromisedOld.AddMonths(-1)
        dateSearch = New DateTime(date1.Year, date1.Month, date1.Day,
                                  0, 1, 0, 0)
        If Not mClsExchange201.GetAppointmentsForDate(dateSearch, dateSearch.AddMonths(3), colAllAppts) Then
            '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            'MsgBox("get Appts list Failed.." & vbCrLf & _
            '                 mClsExchange201.GetLastExchangeError, MsgBoxStyle.Exclamation)
            msExchangeLastErrorMsg = "Get Appts list Failed.." & vbCrLf &
                             mClsExchange201.GetLastExchangeError & vbCrLf
            Exit Function
        End If
        If (colAllAppts IsNot Nothing) Then  '= AndAlso (colAllAppts.Count > 1) Then

            If mbFindJobAppointment(colAllAppts, intJobNo, sIdxFound) Then
                '-test-
                'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                'If MsgBox("Found JobNo: " & intJobNo & ";   Idx is:" & sIdxFound & vbCrLf & _
                '          "  Start Date: " & CStr(dateStart) & _
                '                   "So- ok to UPDATE: " & sSubject & vbCrLf & _
                '                   " to " & CStr(datePromisedNew) & " ??", _
                '       MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes Then
                '    Exit Function
                'End If
                '-- ok- can now Cancel.
                If Not mClsExchange201.DeleteAppointment(sIdxFound) Then
                    '= System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    'MsgBox("Appointment Date Update failed.." & vbCrLf & _
                    '         mClsExchange201.GetLastExchangeError, MsgBoxStyle.Exclamation)
                    msExchangeLastErrorMsg = "Appointment Delete failed.." & vbCrLf &
                                                               mClsExchange201.GetLastExchangeError() & vbCrLf
                Else '-ok-
                    '-  done.
                    strActionTaken = "Cancelled"
                    '= MsgBox("Date Update OK..", MsgBoxStyle.Information)
                    mbCancelAppointment = True
                End If  '-update-
            Else  '-not found.-
                '-BUT   No ActionTaken means NOT FOUND-
                mbCancelAppointment = True
            End If  '--Find-
        End If  '--All Appts.-
    End Function  '-mbCancelAppointment-
    '= = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    '- Add or Update ON-SITE Appointment-
    '-- 
    '--   "bIsNewJob" Means ADD new, else Find and update..
    '==
    '== -- Updated 3501.1105  05-Nov-2018=  
    '==       New Parameter (Duration) for the Exchange module.

    '==
    '== Target-Build-4284  (Started 23-Nov-2020)
    '== Target-Build-4284  (Started 23-Nov-2020)
    '==
    '==  -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..
    '==     Update Function has extra parameter: strRequestType..
    '--      RequestType can be "NewJob", "UpdateJob", or "CancelJob"..
    '==     "bIsNewJob" is now just local..
    '==

    Public Function gbUpdateOnsiteCalendar(ByVal intJobNo As Integer,
                                           ByVal strNominatedTech As String,
                                            ByVal strBody As String,
                                           ByVal strCustomer As String,
                                           ByVal strLocation As String,
                                            ByVal datePromisedOld As Date,
                                            ByVal datePromisedNew As Date,
                                            ByVal decDuration As Decimal,
                                            ByRef clsSystemInfo1 As clsSystemInfo,
                                             ByVal strRequestType As String) As Boolean

        Dim s1, sIdxFound, strActionTaken, sSubject As String
        '=Dim date1, dateSrch As Date
        Dim sOldExchangeUserLogin As String = msExchangeUserLogin
        Dim sOldExchangeUserPassword As String = msExchangeUserPassword
        '== Target-Build-4284  (Started 23-Nov-2020)
        Dim bIsNewJob As Boolean = (LCase(strRequestType) = "newjob")

        gbUpdateOnsiteCalendar = False
        msExchangeLastErrorMsg = ""

        mClsSystemInfo1 = clsSystemInfo1  '--save-
        '-- Load credentials.-
        If mClsSystemInfo1.exists("EXCHANGE_MAILBOX_USER") Then
            msExchangeUserLogin = mClsSystemInfo1.item("EXCHANGE_MAILBOX_USER")
        End If

        If mClsSystemInfo1.exists("EXCHANGE_MAILBOX_PASSWORD") Then
            msExchangeUserPassword = mClsSystemInfo1.item("EXCHANGE_MAILBOX_PASSWORD")
        End If
        '=Call mbExchangeInitialise()
        '-- set up exchange class-
        '==
        '==  -- 3431.0707- 07-July-2018-
        '==       Query result of New connection.
        '==
        '--  This will last for the duration..
        If mClsExchange201 Is Nothing Then
            mClsExchange201 = New clsExchange20(msExchangeUserLogin, msExchangeUserPassword)
            If mClsExchange201 IsNot Nothing Then  '-created-
                If Not mClsExchange201.ExchangeServiceOk Then
                    msExchangeLastErrorMsg = "Exchange Connection Failed." & vbCrLf &
                                              mClsExchange201.GetLastExchangeError
                    Exit Function
                End If  '-ok-
            Else '-create failed.
                msExchangeLastErrorMsg = "Failed to Create MS Exchange Service." & vbCrLf &
                                          mClsExchange201.GetLastExchangeError
                Exit Function
            End If  '-- nothing-
        Else  '--service exists-
            '-- still have a service..
            '-  If credentials have changed, we must start a new one..
            If (sOldExchangeUserLogin <> msExchangeUserLogin) Or
                     (sOldExchangeUserPassword <> msExchangeUserPassword) Then
                mClsExchange201 = Nothing
                mClsExchange201 = New clsExchange20(msExchangeUserLogin, msExchangeUserPassword)
                If (mClsExchange201 Is Nothing) OrElse (Not mClsExchange201.ExchangeServiceOk) Then
                    msExchangeLastErrorMsg = "Exchange Connection Failed." & vbCrLf &
                                              mClsExchange201.GetLastExchangeError
                    Exit Function
                End If  '-ok-
            End If  '-credentials-
        End If  '-nothing-
        '-ok-
        sSubject = "ON-SITE Job No." & CStr(intJobNo) & ";  Tech: " & strNominatedTech & ";  Customer: " & strCustomer & "; "

        If bIsNewJob Then '- chkIsNewJob.Checked Then
            '- ADD new Appointment-
            If Not mbAddNewAppointment(intJobNo, sSubject, strBody, strLocation, datePromisedNew, decDuration) Then
                '== MsgBox("Add Appointment Failed.", MsgBoxStyle.Exclamation)
                msExchangeLastErrorMsg = "Add Appointment Failed." & vbCrLf & msExchangeLastErrorMsg
            Else  '-ok=
                gbUpdateOnsiteCalendar = True
                '= MsgBox("New Appointment added to calendar ok..", MsgBoxStyle.Exclamation)
                msExchangeLastErrorMsg = "New Appointment added to calendar ok.."
            End If
        ElseIf (LCase(strRequestType) = "updatejob") Then '-- is update- '-- is update-
            If Not mbUpdateAppointment(intJobNo,
                                       datePromisedOld,
                                       datePromisedNew, decDuration,
                                           strActionTaken) Then
                '== MsgBox("ERROR in calendar Update.", MsgBoxStyle.Exclamation)
                msExchangeLastErrorMsg &= "ERROR in calendar Update.."
            Else  '-no error-
                If (strActionTaken = "") Then
                    '--not found-
                    'MsgBox("Appointment for JobNo : " & intJobNo & " was not found.." & vbCrLf & _
                    '       "A new appointment will be added.", MsgBoxStyle.Exclamation)
                    msExchangeLastErrorMsg &= "Appointment for JobNo : " & intJobNo & " was not found.." & vbCrLf &
                                                    "A new appointment will be added."
                    '-- MUST add Job to calendar as it seems to have gone..
                    '--add=
                    If Not mbAddNewAppointment(intJobNo, sSubject, strBody, strLocation, datePromisedNew, decDuration) Then
                        '= MsgBox("Add Appointment Failed.", MsgBoxStyle.Exclamation)
                        msExchangeLastErrorMsg = "Add Appointment Failed." & vbCrLf & msExchangeLastErrorMsg
                    Else  '-ok=
                        gbUpdateOnsiteCalendar = True
                        '== MsgBox("New Appointment added to calendar ok..", MsgBoxStyle.Exclamation)
                        msExchangeLastErrorMsg &= vbCrLf & "= New Appointment added to calendar ok.."
                    End If  '--add-
                Else
                    gbUpdateOnsiteCalendar = True
                    '= MsgBox("Appointment was : " & strActionTaken)
                    msExchangeLastErrorMsg &= "Appointment was : " & vbCrLf & strActionTaken & vbCrLf
                End If  '-action-
            End If
        ElseIf (LCase(strRequestType) = "canceljob") Then '-- is cancel-
            '-- call our mbCancelAppointment with JobNo...
            If Not mbCancelAppointment(intJobNo,
                                       datePromisedOld,
                                       datePromisedNew,
                                           strActionTaken) Then
                msExchangeLastErrorMsg &= "ERROR in calendar Cancel function..."
            Else  '-no error-
                If (strActionTaken = "") Then
                    msExchangeLastErrorMsg &= "Appointment for JobNo : " & intJobNo & " was not found.." & vbCrLf &
                                                    "No Cancel was done..."
                Else  '-ok=
                    gbUpdateOnsiteCalendar = True
                    '= MsgBox("Appointment was : " & strActionTaken)
                    msExchangeLastErrorMsg &= "Appointment was : " & vbCrLf & strActionTaken & vbCrLf
                End If
            End If  '-cancel-
        Else  '--nothing-
            msExchangeLastErrorMsg = "Invalid Appointment Request: '" & strRequestType & "'.."
        End If  '-new/update-
    End Function '-gbUpdateOnsiteCalendar-
    '= = = = = = = = = = = ==  == = = = = 

    '- sGetLastExchangeErrorMsg-

    Public Function gsGetLastExchangeErrorMsg() As String
        gsGetLastExchangeErrorMsg = msExchangeLastErrorMsg
    End Function
    '= = = = = = = = = = = = = = = = = = = == = = = = =


End Module  '-modOnsiteCalendar-
'= = = = = = = =  = = = == == =
