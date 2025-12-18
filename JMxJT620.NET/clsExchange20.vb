
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.Exchange.WebServices.Data

Public Class clsExchange20

    '-- grh 29-May-2017--
    '--  Class to cover/handle Exchange Server Appointments..

    '-- grh 08-May-2018--
    '--  Updated to retry connection..

    '--3431.0707-- grh 07-July-2018--
    '--  Updated- Add function to query Connection result..
    '==
    '== -- Updated 3501.1001  01-Oct-2018=  
    '==     -- Fix to clsExchange20 (GetAppointmentsForDate) to check for NOTHING returned....
    '==
    '= = = = = = = = = = = = = = = = = = = = =

    Private msUserLogin As String = ""
    Private msUserPassword As String = ""

    Private mExchangeService1 As ExchangeService
    Private mbServiceOK As Boolean = False

    '--result of Find..
    Private mAppointmentsResults As FindItemsResults(Of Appointment)

    Private mListAppointments As List(Of Appointment)

    Private mColLastFoundAppointments As Collection

    Private msLastExchangeError As String = ""

    '= = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    Private Function CertificateValidationCallBack(sender As Object, _
                                                  certificate As System.Security.Cryptography.X509Certificates.X509Certificate, _
                                                   chain As System.Security.Cryptography.X509Certificates.X509Chain, _
                                                     sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean

        '-- // If the certificate is a valid, signed certificate, return true.
        If (sslPolicyErrors = System.Net.Security.SslPolicyErrors.None) Then

            Return True
        End If
        '-- // If there are errors in the certificate chain, look at each error to determine the cause.
        If ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) <> 0) Then

            If (Not IsDBNull(chain)) AndAlso (Not IsDBNull(chain.ChainStatus)) Then
                '=  (chain <> null And chain.ChainStatus <> null) Then

                For Each status As System.Security.Cryptography.X509Certificates.X509ChainStatus In chain.ChainStatus
                    If ((certificate.Subject = certificate.Issuer) And
                       (status.Status = System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot)) Then
                        '-// Self-signed certificates with an untrusted root are valid. 
                        Continue For
                    Else
                        '=  {
                        If (status.Status <> System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError) Then
                            '- // If there are any other errors in the certificate chain, the certificate is invalid,
                            '- // so the method returns false.
                            Return False
                        End If
                    End If
                Next status
                '--// When processing reaches this line, the only errors in the certificate chain are 
                '--// untrusted root errors for self-signed certificates. These certificates are valid
                '-- // for default Exchange server installations, so return true.
                Return True
            Else
                '-- // In all other cases, return false.
                Return False
            End If
        End If  '-chain null-

    End Function  '- CertificateValidationCallBack-
    '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->

    Private Function RedirectionUrlValidationCallback(redirectionUrl As String) As Boolean
        '-{
        '-// The default for the validation callback is to reject the URL.
        Dim result As Boolean = False

        Dim redirectionUri = New Uri(redirectionUrl)

        '- // Validate the contents of the redirection URL. In this simple validation
        '= // callback, the redirection URL is considered valid if it is using HTTPS
        '- // to encrypt the authentication credentials. 
        If (LCase(redirectionUri.Scheme) = "https") Then
            result = True
        End If
        Return result
        '-}
    End Function '- re-dir-callback-
    '= = = = = = == = = =  = = = = = == = = =  =
    '-===FF->


    '-- Constructor- I n i t ----

    Public Sub New(ByVal strUserLogin As String, _
                   ByVal strPassword As String)
        MyBase.New()
        msLastExchangeError = ""
        mbServiceOK = False
        Dim intRetries As Integer = 2
        ServicePointManager.ServerCertificateValidationCallback = _
                   New RemoteCertificateValidationCallback(AddressOf CertificateValidationCallBack)

        '= -save-  Base Mailbox credentials..

        msUserLogin = strUserLogin
        msUserPassword = strPassword

        mExchangeService1 = New ExchangeService(ExchangeVersion.Exchange2007_SP1)
        mExchangeService1.Credentials = New WebCredentials(msUserLogin, msUserPassword)

        mExchangeService1.TraceEnabled = True
        mExchangeService1.TraceFlags = TraceFlags.All
        While (intRetries > 0) And (Not mbServiceOK)
            Try
                mExchangeService1.AutodiscoverUrl(strUserLogin, AddressOf RedirectionUrlValidationCallback)
                mbServiceOK = True
                msLastExchangeError = ""
            Catch ex As Exception
                '= MsgBox("AutodiscoverUrl FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                msLastExchangeError = "AutodiscoverUrl FAILED-" & vbCrLf & ex.Message
                intRetries -= 1
            End Try
        End While

    End Sub '--init..--
    '= = = = = = = = = = = = =

    '-Get result of connect.-

    Public Function ExchangeServiceOk() As Boolean

        ExchangeServiceOk = mbServiceOK

    End Function  '--GetLastExchangeError-
    '= = = = = = = = = = = = = = = = = = ==

    '-GetLastExchangeError-

    Public Function GetLastExchangeError() As String

        GetLastExchangeError = msLastExchangeError

    End Function  '--GetLastExchangeError-
    '= = = = = = = = = = = = = = = = = = ==
    '-===FF->

    Public Function SendEmail(ByVal strRecipient As String, _
                               ByVal strSubject As String, _
                               ByVal strText As String) As Boolean

        SendEmail = False
        msLastExchangeError = ""
        If Not mbServiceOK Then
            '=MsgBox("No Service connection.", MsgBoxStyle.Exclamation)
            msLastExchangeError = "No Service connection."
            Exit Function
        End If

        Dim email1 = New EmailMessage(mExchangeService1)
        '      EmailMessage email = new EmailMessage(service);
        'email.ToRecipients.Add("martin@precisepcs.com");
        'email.ToRecipients.Add("grhaas@outlook.com");
        'email.Subject = "Hello-Exchange-World";
        'email.Body = new MessageBody("Hi Martin.. This is the first (second) email I've sent by using the EWS Managed API");
        'email.Send();

        '- email1.ToRecipients.Add("martin@precisepcs.com");
        email1.ToRecipients.Add(strRecipient)
        email1.Subject = strSubject
        email1.Body = New MessageBody(strText)
        Try
            email1.Send()
            SendEmail = True
        Catch ex As Exception
            '= MsgBox("Send Email FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            msLastExchangeError = "Send Email FAILED-" & vbCrLf & ex.Message
        End Try

    End Function  '-send email-
    '= = = = = = = = = = = = = === =
    '-===FF->

    '== Make Appointment.
    '-- Return the Id in "strAppointmentCalUid"..

    Public Function MakeAppointment(ByVal strSubject As String, _
                                    ByVal strBody As String, _
                                    ByVal dateStart As Date, _
                                    ByVal dateEnd As Date, _
                                     ByVal strLocation As String, _
                                     ByRef strAppointmentCalUid As String) As Boolean
        MakeAppointment = False
        strAppointmentCalUid = ""
        msLastExchangeError = ""

        If Not mbServiceOK Then
            '= MsgBox("No ExchangeService connection.", MsgBoxStyle.Exclamation)
            msLastExchangeError = "No ExchangeService connection."
            Exit Function
        End If

        Dim appointment1 = New Appointment(mExchangeService1)
        '--// Set the properties on the appointment object to create the appointment.
        appointment1.Subject = strSubject
        appointment1.Body = strBody
        appointment1.Start = dateStart
        appointment1.End = dateEnd
        appointment1.Location = strLocation

        '= appointment.ReminderDueBy = DateTime.Now;
        '--- Save the appointment to instance's calendar.
        Try
            appointment1.Save(SendInvitationsMode.SendToNone)
            '--   Verify that the appointment was created by using the appointment's item ID.
            '- AND save the Appointment.ICalUid property (system.string).
            Try
                Dim app2 As Appointment = Appointment.Bind(mExchangeService1, appointment1.Id, _
                                                             New PropertySet(BasePropertySet.FirstClassProperties))
                strAppointmentCalUid = app2.ICalUid
                MakeAppointment = True
            Catch ex As Exception
                '=MsgBox("Getting ID of new Appointment FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                msLastExchangeError = "Getting ID of new Appointment FAILED-" & vbCrLf & ex.Message
            End Try

        Catch ex As Exception
            '= MsgBox("Save Appointment FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            msLastExchangeError = "Save Appointment FAILED-" & vbCrLf & ex.Message
            Exit Function
        End Try

        '== https://msdn.microsoft.com/en-us/library/office/dn495611(v=exchg.150).aspx  

        '-- Appointment appointment = new Appointment(service);
        '// Set the properties on the appointment object to create the appointment.
        'appointment.Subject = "Tennis lesson";
        'appointment.Body = "Focus on backhand this week.";
        'appointment.Start = DateTime.Now.AddDays(2);
        'appointment.End = appointment.Start.AddHours(1);
        'appointment.Location = "Tennis club";
        'appointment.ReminderDueBy = DateTime.Now;
        '// Save the appointment to your calendar.
        'appointment.Save(SendInvitationsMode.SendToNone);
        '// Verify that the appointment was created by using the appointment's item ID.
        'Item item = Item.Bind(service, appointment.Id, new PropertySet(ItemSchema.Subject));
        'Console.WriteLine("\nAppointment created: " + item.Subject + "\n");

    End Function  '== Make Appointment.
    '= = = = = = = = = = = = = === =
    '-===FF->

    '== GET list of appointments Appointment for period.
    '--  Caller n=must do this before deciding which on to update or delete...

    Public Function GetAppointmentsForDate(ByVal dateStart As Date, _
                                            ByVal dateEnd As Date, _
                                            ByRef colAppointments As Collection) As Boolean
        Const NUM_APPTS As Integer = 10
        Dim sList As String

        GetAppointmentsForDate = False
        msLastExchangeError = ""

        If Not mbServiceOK Then
            '= MsgBox("No ExchangeService connection.", MsgBoxStyle.Exclamation)
            msLastExchangeError = "No ExchangeService connection."
            Exit Function
        End If

        '- const int NUM_APPTS = 5;
        '- // Initialize the calendar folder object with only the folder ID. 
        '- CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());

        '- // Set the start and end time and number of appointments to retrieve.
        '- CalendarView cView = new CalendarView(startDate, endDate, NUM_APPTS);

        '- // Limit the properties returned to the appointment's subject, start time, and end time.
        '-  cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

        '- Initialize the calendar folder object with only the folder ID. 
        Dim calendar As CalendarFolder
        Try
            calendar = CalendarFolder.Bind(mExchangeService1, WellKnownFolderName.Calendar, New PropertySet())
        Catch ex As Exception
            '= MsgBox("Get Appointments List- Failed to bind to calendar.." & vbCrLf & _
            '=                                        ex.Message, MsgBoxStyle.Exclamation)
            msLastExchangeError = "Get Appointments List- Failed to bind to calendar.." & vbCrLf & _
                                                    ex.Message
            Exit Function
        End Try
        '- check for nothing.
        '==
        '== -- Updated 3501.1001  01-Oct-2018=  
        '==     -- Fix to clsExchange20 (GetAppointmentsForDate) to check for NOTHING returned....
        '==
        If calendar Is Nothing Then
            msLastExchangeError = "Get Appointments List- No calendar object returned.." & vbCrLf 
            Exit Function
        End If

        '- Set the start and end time and number of appointments to retrieve.
        Dim cView As CalendarView = New CalendarView(dateStart, dateEnd, NUM_APPTS)
        '- Limit the properties returned to the appointment's subject, start time, and end time.
        cView.PropertySet = New PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End)

        '=  // Retrieve a collection of appointments by using the calendar view.
        '=    FindItemsResults<Appointment> appointments = calendar.FindAppointments(cView);

        '=    Console.WriteLine("\nThe first " + NUM_APPTS + " appointments on your calendar from " + startDate.Date.ToShortDateString() + 
        '=                " to " + endDate.Date.ToShortDateString() + " are: \n");
        '  foreach (Appointment a in appointments)
        '{
        '    Console.Write("Subject: " + a.Subject.ToString() + " ");
        '    Console.Write("Start: " + a.Start.ToString() + " ");
        '    Console.Write("End: " + a.End.ToString());
        '    Console.WriteLine();
        '}

        '= Retrieve a collection of appointments by using the calendar view.
        Try
            '= mListAppointments = calendar.FindAppointments(cView)
            mAppointmentsResults = calendar.FindAppointments(cView)
            mListAppointments = mAppointmentsResults.ToList()
        Catch ex As Exception
            '= MsgBox("Get Appointments List FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            msLastExchangeError = "Get Appointments List FAILED-" & vbCrLf & ex.Message
            Exit Function
        End Try
        sList = ""
        colAppointments = New Collection  '-Co of subjects for caller-
        mColLastFoundAppointments = New Collection  '--to save-

        Dim idx As Integer = 0
        Dim col1 As Collection
        '--get list and collection-.
        For Each appt1 As Appointment In mListAppointments
            idx += 1
            col1 = New Collection
            col1.Add(appt1.Subject, "appt_subject")
            col1.Add(appt1.Start, "appt_start")
            col1.Add(appt1.End, "appt_end")
            '= col1.Add(appt1, "this_appt")
            col1.Add(appt1.Id.ToString, "appt_item_id")
            col1.Add(CStr(idx), "idx")
            colAppointments.Add(col1, CStr(idx))
            '-- collect id's for us so we can identify appt for update or deletion.
            mColLastFoundAppointments.Add(appt1, CStr(idx))
            sList &= appt1.Subject.ToString & vbCrLf & appt1.Id.ToString & vbCrLf & vbCrLf
        Next appt1
        '--test-
        GetAppointmentsForDate = True
        'MsgBox("The first " & mListAppointments.Count & _
        '              " appointments on your calendar from " & Format(dateStart, "dd-MMM-yyyy") & _
        '                " to " & Format(dateEnd, "dd-MMM-yyyy") & " are: " & vbCrLf & sList, MsgBoxStyle.Information)

    End Function  '-GetAppointmentsForDate-
    '= = = = = = = = = = = = = = = = = = =  = 
    '= = = = = = = = = = = = = === =
    '-===FF->

    '-- Update Appointment- (subject and date)-

    '== Caller MUST HAVE DONE GET list of Appointments for target period.
    '--   before deciding which on to update or delete...
    '--  "strIdx" identifies appointment in the previously retrieved Collection.

    Public Function UpdateAppointment(ByVal strIdx As String, _
                                       ByVal strNewSubject As String, _
                                       ByVal dateNewStart As DateTime, _
                                       ByVal dateNewEnd As DateTime) As Boolean
        Dim appointment1 As Appointment

        msLastExchangeError = ""

        UpdateAppointment = False
        If Not mbServiceOK Then
            msLastExchangeError = "No ExchangeService connection."
            Exit Function
        End If

        If (strIdx = "") OrElse (Not mColLastFoundAppointments.Contains(strIdx)) Then
            '= MsgBox("Invalid appointment Index for Update.", MsgBoxStyle.Exclamation)
            msLastExchangeError = "Invalid appointment Index for Update."
            Exit Function
        End If

        '--    https://msdn.microsoft.com/en-us/library/office/dn495610(v=exchg.150).aspx
        '-- //- C# Conditional operator-  (cf IIF() )	x ? y : z 	Evaluates to y if x is true, z if x is false
        '// Instantiate an appointment object by binding to it by using the ItemId.
        '// As a best practice, limit the properties returned to only the ones you need.
        'Appointment appointment =
        '- // Appointment.Bind(service, appointmentId, _
        '-- //     new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End));

        'string oldSubject = appointment.Subject;
        '// Update properties on the appointment with a new subject, start time, and end time.
        'appointment.Subject = appointment.Subject + " moved one hour later and to the day after " + appointment.Start.DayOfWeek + "!";
        'appointment.Start.AddHours(25);
        'appointment.End.AddHours(25);
        '// Unless explicitly specified, the default is to use SendToAllAndSaveCopy.
        '// This can convert an appointment into a meeting. To avoid this,
        '// explicitly set SendToNone on non-meetings.
        'SendInvitationsOrCancellationsMode mode = appointment.IsMeeting ? 
        '    SendInvitationsOrCancellationsMode.SendToAllAndSaveCopy : SendInvitationsOrCancellationsMode.SendToNone;
        '// Send the update request to the Exchange server.
        'appointment.Update(ConflictResolutionMode.AlwaysOverwrite, mode);
        '// Verify the update.
        'Console.WriteLine("Subject for the appointment was \"" + oldSubject + "\". The new subject is \"" + appointment.Subject + "\"");
        '//= = = = = = = = = == = =

        '--  Instantiate an appointment object by binding to it by using the ItemId.
        '--  As a best practice, limit the properties returned to only the ones you need.
        Dim appt_this As Appointment = mListAppointments(CInt(strIdx) - 1)
        Dim strThisId As String = appt_this.Id.ToString  '= mColLastFoundAppointments.Item(strIdx)("appt_item_id")
        Try
            appointment1 = Appointment.Bind(mExchangeService1, appt_this.Id, _
                                            New PropertySet(AppointmentSchema.Subject, _
                                                   AppointmentSchema.StartTimeZone, AppointmentSchema.Start, _
                                                   AppointmentSchema.End))
            appointment1.Subject = strNewSubject
            appointment1.Start = dateNewStart
            appointment1.StartTimeZone = TimeZoneInfo.Local
            appointment1.End = dateNewEnd
            '-- Unless explicitly specified, the default is to use SendToAllAndSaveCopy.
            '-- This can convert an appointment into a meeting. To avoid this,
            '-- explicitly set SendToNone on non-meetings.
            Dim mode As SendInvitationsOrCancellationsMode = SendInvitationsOrCancellationsMode.SendToNone
            Try
                '-- Send the update request to the Exchange server.
                appointment1.Update(ConflictResolutionMode.AlwaysOverwrite, mode)
                UpdateAppointment = True
            Catch ex As Exception
                '= MsgBox("Update operation for Appointment UPDATE has FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                msLastExchangeError = "Update operation for Appointment UPDATE has FAILED-" & vbCrLf & ex.Message
            End Try
        Catch ex As Exception
            '= MsgBox("BINDING to Appointment for UPDATE has FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            msLastExchangeError = "BINDING to Appointment for UPDATE has FAILED-" & vbCrLf & ex.Message
            Exit Function
        End Try  '-bind-

    End Function '--update-
    '= = = = = = = = = = = = = === =
    '-===FF->


    '-- Delete Appointment-

    '== Caller MUST HAVE DONE GET list of Appointments for target period.
    '--   before deciding which on to update or delete...
    '--  "strIdx" identifies appointment in the previously retrieved Collection.

    Public Function DeleteAppointment(ByVal strIdx As String) As Boolean
        Dim appointment1 As Appointment
        msLastExchangeError = ""

        DeleteAppointment = False
        If Not mbServiceOK Then
            msLastExchangeError = "No ExchangeService connection."
            Exit Function
        End If

        If (strIdx = "") OrElse (Not mColLastFoundAppointments.Contains(strIdx)) Then
            '= MsgBox("Invalid appointment Index for Delete.", MsgBoxStyle.Exclamation)
            msLastExchangeError = "Invalid appointment Index for Delete."
            Exit Function
        End If

        '--  // Instantiate an appointment object by binding to it by using the ItemId.
        '// As a best practice, limit the properties returned to only the ones you need.
        'Appointment appointment = Appointment.Bind(service, appointmentId, new PropertySet());

        '// Delete the appointment. Note that the item ID will change when the item is moved to the Deleted Items folder.
        'appointment.Delete(DeleteMode.MoveToDeletedItems);

        '--  Instantiate an appointment object by binding to it by using the ItemId.
        '--  As a best practice, limit the properties returned to only the ones you need.
        Dim appt_this As Appointment = mListAppointments(CInt(strIdx) - 1)
        Dim strThisId As String = appt_this.Id.ToString  '= mColLastFoundAppointments.Item(strIdx)("appt_item_id")
        Try
            appointment1 = Appointment.Bind(mExchangeService1, appt_this.Id)
            '- check bind-
            If (appointment1.Id.ToString <> strThisId) Then
                '= MsgBox("Deleting Appt- Wrong ID returned in Bind.", MsgBoxStyle.Exclamation)
                msLastExchangeError = "Deleting Appt- Wrong ID returned in Bind."
                Exit Function
            End If
            '-- Delete the appointment. Note that the item ID will change when the item is moved to the Deleted Items folder.
            Try
                appointment1.Delete(DeleteMode.MoveToDeletedItems)
                DeleteAppointment = True
            Catch ex As Exception
                '=MsgBox("DELETING Appointment has FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                msLastExchangeError = "DELETING Appointment has FAILED-" & vbCrLf & ex.Message
                Exit Function
            End Try

        Catch ex As Exception
            '= MsgBox("BINDING to Appointment to delete has FAILED-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            msLastExchangeError = "BINDING to Appointment to delete has FAILED-" & vbCrLf & ex.Message
            Exit Function
        End Try

    End Function  '-DeleteAppointment-
    '= = = = = = = = = ==  == = = = = =



End Class  '--clsExchange20-
'= = = = = == = == == = == 

'== the end ==
