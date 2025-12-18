
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class frmNewJobBase
    Inherits System.Windows.Forms.Form

    '-- Created 08-Dec-2019= 
    '--     for JobMatix42 4219.!215..
    '--  Hosts Child UserControl "ucChildNewJob"..

    '= = = = = = = =  = = = == = = = = = = = == = = = = = =

    Const k_statusWaitListed As String = "05-WaitListed"
    Const k_statusCreated As String = "10-Created"
    Const k_statusStarted As String = "30-Started"
    Const k_statusCancelled As String = "97-Cancelled"

    '-- Tabs--
    Const k_SSTAB_CUSTOMER As Short = 0
    Const k_SSTAB_GOODS As Short = 1
    '========Const k_SSTAB_USERS = 2
    Const k_SSTAB_PROBLEM As Short = 2

    '====Const k_maxGoodsInCare = 3   '-- max index no of TXT items..
    Const k_maxExtrasInCare As Short = 4 '-- no of checkboxes--
    Const k_maxUserNames As Short = 3 '--  no of usernames possible--
    '---Const k_maxProblems = 10       '-- no of checkboxes--
    Const k_TABPRINTPRIORITY As Short = 26
    Const k_TABPRINTBRAND As Short = 34
    Const k_TABPRINTMODEL As Short = 52
    '= = = = = = = = = = = = =  =
    Const k_PRINT_AGREEMENT As Short = 0
    Const k_PRINT_RECEIPT As Short = 1
    Const k_PRINT_LABEL As Short = 2

    Const K_GOODS_ONSITEJOB As String = "ON-SITE JOB;"

    '= = = = = = = = = = = =

    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer

    Private msAppPath As String
    Private mbActive As Boolean = False  '-- stops activate being re-entered..-
    Private mbFormStarting As Boolean = True  '-- for premature firing of events....-

    Private mbAmending As Boolean = False  '-- amend existing  job...-
    Private mbIsBooking As Boolean = False '--NOW DECIDED HERE..  Form called up by BOOKING command..--
    Private mbIsCheckIn As Boolean = False '-- Form called up by CHECK-In command..--
    Private mbIsOnSiteJob As Boolean = False '-- Form called up by New OnSite Job command..--

    Private mCnnJobs As OleDbConnection  '== ADODB.Connection '--SQL jobs connection --
    Private mTransactionUpdateJob As OleDbTransaction
    Private mTransactionInsertJob As OleDbTransaction

    '== Private mCnnJet  As ADODB.connection    '--  Retail Manager Jet connection..--
    Private mRetailHost1 As _clsRetailHost

    '== Private mColJetDBInfo As Collection
    Private mColSqlDBInfo As Collection

    Private msLocalSettingsPath As String = ""
    Private mLocalSettings1 As clsLocalSettings  '==3311.225=

    '-- new job data --
    '-- new job data --
    Private mlStaffId As Integer = -1 '--save staff-id--
    Private msStaffName As String = ""

    Private mlJobId As Integer = -1 '--AMEND JOBno, OR (Created) Identity retrieved..-
    Private mColJobFields As Collection '--Retrieved Existing Job Record to AMEND..--

    Private mlCustomerId As Integer = -1 '--save cust-id--
    Private msCustomerBarcode As String = "" '--main cust. identifier..-
    Private msCustomerCompany As String = ""
    Private msCustomerName As String = ""
    '==3083=
    Private msCustomerAddress As String = ""

    Private msCustomerPhone As String = ""
    Private msCustomerMobile As String = ""
    '== 3057.1 ==
    Private mColCustomerJobsGoods As Collection
    Private mColCustomerPrevGoodsFlat As Collection  '--cooresponds to combo box..-

    Private msPriority As String = ""
    Private msPriorityText As String = ""
    Private mlPriorityColour As Integer
    '=3203.119=
    Private mColorPriorityFG As Color = Color.Black

    '--Private maiGoods() As Integer   '-- checked states of Goods list box..--
    Private mlGoodsCount As Integer = -1 '--count of goods in txt array..-
    Private msGoodsInCare As String = ""
    '=3501.0814=  Remember orig. in case amended..
    Private msOriginalGoodsInCare As String = ""
    Private mbGoodsOtherChanged As Boolean = False

    Private mColInitialGoodsInCare As Collection

    Private msPrintGoodsInCare As String = ""
    Private miGoodsIndex As Short
    '--Private msOtherGoods As String
    '--Private mbBrandClicked As Boolean   '--otherwise txt in textbox..--

    Private msExtrasInCare As String = ""

    '===  REPLACED by PROBLEM-SHORT--   Private msProblems As String
    Private msProblemShort As String = ""
    Private msProblemDetails As String = ""
    '== Private maiSymptoms() As Short '-- checked states of symptoms list box..--
    '--  (Now upgraded to CheckListBox.. )

    Private msSymptoms As String = "" '-- final accum..-

    Private mbBrowsing As Boolean = False
    Private mbCreated As Boolean = False '--record has been created..--

    '--  printers--
    '== Private mPrtColour As Printer
    Private msColourPrinterName As String = ""
    Private msDefaultPrinterName As String = ""

    '== Private mPrtReceipt As Printer
    Private msReceiptPrinterName As String = ""

    '== Private mPrtLabel As Printer
    Private msLabelPrinterName As String = ""
    '==Private mlProgress As Long  '--progress value..-
    '= = = = =  = =  = = = = =

    Private mbLicenceOK As Boolean = False
    'Private msABN As String = ""
    'Private msBusinessName As String
    'Private msBusinessShortName As String
    'Private msBusinessAddress1 As String
    'Private msBusinessAddress2 As String
    'Private msBusinessState As String
    'Private msBusinessPostCode As String
    'Private msBusinessPhone As String
    'Private msBusinessEmail As String = ""

    '== Private msNewJobDocketFootnote As String = ""
    Private msNewJobDocketFootnote As String = "We will notify you when job is ready..  Payable on collection.."
    '= = = = = = = = = = = = = =

    Private mCurLabourHourlyRate As Decimal = 1
 
    Private mCurLabourMinCharge As Decimal = 1
    Private mCurNotificationCostLimit As Decimal = 1
    '= = = = = = = = = = = = =
     Private mColPriorities As Collection

    '= = = = = = = = = = = = =
    '-- Label printing..--
    Private msLabelBarcodeFontName As String = ""
    Private mlLabelBarcodeFontSize As Integer = 9
    '-- Job Label Dimensions..-
    '===Private mcurJobLabelPrintDepth As Currency  '-- label actual depth mm.. (max 1 decimal)--
    '===Private mcurJobLabelGapDepth As Currency   '-- label GAP depth mm.. (max 1 decimal.)--

    '= = = = = = = = = = = = = = = =  =
    'Private mlCmdSymptomLeft As Integer '-- save orig. pos.--
    'Private mlCmdSymptomTop As Integer '-- save orig. pos.--
    ''= = = = = = = = = = = = = = = = =

    'Private msJobOriginalStatus As String = ""
    ''--original Job Record dateTime-
    'Private mDateOriginalTimeStamp As DateTime

    '==  NEW FOR GOODS..==
    '==  NEW FOR GOODS..==
    Private mColGoodsTypes As Collection
    Private mColBrands As Collection

    Private mbCustomerRefreshed As Boolean = False
    Private mbDataChanged As Boolean = False
    '=3519.0414-
    Private mbInvalidPasswordDataOnStartup As Boolean = False

    Private mbLabelsPrinted As Boolean = False

    Private msTermsText As String = ""
    Private msServiceChargesInfoText As String = ""   '--3041.1==

    '==3083== OnSite--
    Private msDatePromised As String = ""
    Private msTimepromised As String = ""
    '=3403.604=  SAVE Original date promised.
    Private mDatePromisedOriginal As Date = DateTime.MinValue

    '==3083.717= Labels
    Private mbUserChangedNumberOfLabels As Boolean = False
    Private mbNumUpDownLabels_Updating As Boolean = False

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Private mClsSystemInfo As clsSystemInfo

    Private mIntPreviousJobNo As Integer = -1
    '-- drawing experiments..-
    '== Private mTabArea As Rectangle
    '== Private mTabTextArea As RectangleF


    '==  A t t a c h m e n t s -
    '==  A t t a c h m e n t s -
    '==  A t t a c h m e n t s -

    Private mClsAttachments1 As clsAttachments

    '-- NEW File to be Attached (For NEW RA PIC)..
    Private mByteNewFile As Byte()
    Private msNewFileFullPath As String = ""
    Private msNewFileFileTitle As String = ""
    Private msNewFileFormat As String = ""

    Private mIntDoc_id As Integer = -1    '--Current Main attachment image if any..
    Private mbMainImageToBeUpdated As Boolean = False

    '=3431.0505--EXCHANGE Calendar updates-
    '--  This is passed back to Main Form if Calendar updated was queued.
    Private msExchangeCalendarUpdateXmlFileName As String = ""

    '=3431.0527=
    Private mIntMaxTxtProblem As Integer = 4000

    '=3501.1105=
    Private mbByPassLaptopChargerCheck As Boolean = False

    '-frm Base-
    '-save child form for resizing..
    Private mUcChild1 As ucChildNewJob

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    WriteOnly Property connectionSql() As OleDbConnection  '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    '===  NOW MUST GO VIA RetailHost CLASS..--
    WriteOnly Property retailHost() As _clsRetailHost
        Set(ByVal Value As _clsRetailHost)

            mRetailHost1 = Value
        End Set
    End Property '-host-
    '= = = = = = = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)

            mColSqlDBInfo = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    '== Property Let dbInfoJet(dbinfo As Collection)

    '==   Set mColJetDBInfo = dbinfo

    '== End Property  '--info jet..--
    '= = = = = = = = = = = =

    '--  Customer Details for New Job..--

    WriteOnly Property CustomerId() As Integer
        Set(ByVal Value As Integer)

            mlCustomerId = Value
        End Set
    End Property '--CustomerId-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerBarcode() As String
        Set(ByVal Value As String)

            msCustomerBarcode = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerCompany() As String
        Set(ByVal Value As String)

            msCustomerCompany = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerName() As String
        Set(ByVal Value As String)
            msCustomerName = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerPhone() As String
        Set(ByVal Value As String)

            msCustomerPhone = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =
    WriteOnly Property CustomerMobile() As String
        Set(ByVal Value As String)

            msCustomerMobile = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerJobsGoods() As Collection
        Set(ByVal Value As Collection)

            mColCustomerJobsGoods = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    '--licensing..--

    WriteOnly Property LicenceOK() As Boolean
        Set(ByVal Value As Boolean)
            mbLicenceOK = Value
        End Set
    End Property '--licence..-
    '= = = = = = = = = = = =

    WriteOnly Property LabourHourlyRate() As Decimal
        Set(ByVal Value As Decimal)
            mCurLabourHourlyRate = Value
        End Set
    End Property '--rate.--
    '= = = = = = = =  = = =

    WriteOnly Property LabourMinCharge() As Decimal
        Set(ByVal Value As Decimal)

            mCurLabourMinCharge = Value
        End Set
    End Property '-min.--
    '= = = = = = = =  = = =

    WriteOnly Property NotificationCostLimit() As Decimal
        Set(ByVal Value As Decimal)

            mCurNotificationCostLimit = Value
        End Set
    End Property '--limit.--
    '= = = = = = = =  = = =

    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffId() As Integer
        Set(ByVal Value As Integer)

            mlStaffId = Value
        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '--  job No. comes if this is AMENDING new Job..-

    WriteOnly Property JobId() As Integer
        Set(ByVal Value As Integer)

            mlJobId = Value
        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '-- Booking or Real Job..--
    '==3203.123=  NOE DECIDED HERE..
    '== WriteOnly Property IsBooking() As Boolean
    '==     Set(ByVal Value As Boolean)
    '==         mbIsBooking = Value
    '==     End Set
    '== End Property '--licence..-
    '= = = = = = = = = = = =

    WriteOnly Property IsCheckIn() As Boolean
        Set(ByVal Value As Boolean)
            mbIsCheckIn = Value
        End Set
    End Property '-- IsCheckIn--
    '= = = = = = = = =  = = = =

    WriteOnly Property IsOnSiteJob() As Boolean
        Set(ByVal Value As Boolean)
            mbIsOnSiteJob = Value
        End Set
    End Property '-- IsCheckIn--
    '= = = = = = = = =  = = = =

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Object
        Set(ByVal Value As Object)

            picUserLogo.Image = Value
        End Set
    End Property '--logo..--
    '= = = = = = = =  ==

    '=3431.0505=
    '-- -results-
    '-- msExchangeCalendarUpdateXmlFileName-

    ReadOnly Property ExchangeCalendarUpdateXmlFileName As String
        Get
            ExchangeCalendarUpdateXmlFileName = msExchangeCalendarUpdateXmlFileName
        End Get
    End Property  '--msExchangeCalendarUpdateXmlFileName-
    '= = = = = = = = = = = = = == = = = = = = = == = = =

    '-- E n d properties..-
    '-- E n d properties..-
    '-===FF->

    '==-- Child uses Delegate to signal child closed to Main Parent.....

    Public Sub subChildReport(ByVal strChildName As String, _
                              ByVal strEvent As String, _
                               ByVal sText As String)

        '= If LCase(strEvent) = "formclosed" Then
        'For Each tabPageChild1 As TabPage In Me.TabControlMain.TabPages
        '    If (Not (tabPageChild1 Is Nothing)) AndAlso _
        '               (LCase(tabPageChild1.Name) = LCase(strChildName)) Then
        '        If LCase(strEvent) = "formclosed" Then
        '            '-Child form has closed..
        '            '-- find tab page and delete it..
        '            tabPageChild1.Dispose()
        '            Exit For
        '        End If  '--closed-
        '    ElseIf LCase(strEvent) = "updatetabtext" Then
        '        If (sText <> "") Then
        '            tabPageChild1.Text = sText
        '        End If
        '    End If  '-nothing-
        'Next  '-page-
        '= End If  '-closed-

    End Sub  '--ChildReport-
    '= = = = = = =  =  = =

    '==-- Child uses EVENT Delegate to signal child closed to Main Parent..
    '-- Child has this event definition..
    '--   Public Event PosChildClosing(ByVal strChildName As String)

    '-- Dispose of Tab Page and Child Control..
    '-- Dispose of Tab Page and Child..

    Private Sub posChild_Closing(ByVal strChildName As String)

        RemoveHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing

        '-return result to caller..

        '= mbIsCancelled = mUcChild1.wasCancelled
        '= msSelectedCustomerBarcode = mUcChild1.selectedBarcode
        msExchangeCalendarUpdateXmlFileName = mUcChild1.ExchangeCalendarUpdateXmlFileName

        Me.Hide()

    End Sub  '- posChild_Closing-
    '= = = = = = = = = = = === = = =

    '==--Child uses Delegate to signal child STAFF SIGNED ON to Main Parent.....

    Public Sub subChildSignedOn(ByVal intStaffid As Integer, _
                                ByVal strStaffBarcode As String, _
                                 ByVal strStaffName As String)
        '--save as main signon-
        '= mIntStaff_id = intStaffid
        '= msStaffBarcode = strStaffBarcode
        msStaffName = strStaffName

    End Sub '' child signed on..-
    '= = = = = = = == = = = = = = = = = = = =
    '-===FF->


    '-- load-

    Private Sub frmNewJobBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call CenterForm(Me)
        grpBoxMain.Text = ""
        Me.Text = gsGetAppName() & "-  New/Amend Job."

    End Sub  '--load.
    '= = = = = = = = = = = =

    '--Activated-

    Private Sub frmCustomer_Activated(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Activated
        '-- do sub at startup only..
        If mbActive Then Exit Sub
        mbActive = True

    End Sub  '--activated-

    '--Shown-

    Private Sub frmNewJobBase_Shown(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Shown

        '=4201.0530==
        '--  Load Child UserControl..

        Dim ucChild1 As ucChildNewJob

        '- make as static first, first, for resizing.
        mUcChild1 = New ucChildNewJob

        '-save- for re-size..
        ucChild1 = mUcChild1

        ucChild1.StaffId = mlStaffId
        ucChild1.StaffName = msStaffName
        '= ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnJobs '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.retailHost = mRetailHost1

        ucChild1.dbInfoSql = mColSqlDBInfo
        ucChild1.LabourMinCharge = mCurLabourMinCharge
        ucChild1.NotificationCostLimit = mCurNotificationCostLimit
        '==3072== frmNewJob3A.NewJobDocketFootnote = msNewJobDocketFootnote
 
        ucChild1.LicenceOK = mbLicenceOK
        ucChild1.StaffId = mlStaffId
        ucChild1.StaffName = msStaffName
        '-- Passing JOB-NO Indicate RE-EDIT existing Agreement..--

        ucChild1.JobId = mlJobId '==== CLng(colKeys(1))
        ucChild1.UserLogo = picUserLogo.Image '--pass logo..-

        '=If bCheckingIn Then ucChild1.IsCheckIn = True
        ucChild1.IsCheckIn = mbIsCheckIn
        ucChild1.IsOnSiteJob = mbIsOnSiteJob

        ucChild1.CustomerBarcode = msCustomerBarcode
        ucChild1.CustomerId = mlCustomerId
        ucChild1.CustomerCompany = msCustomerCompany
        ucChild1.CustomerName = msCustomerName
        ucChild1.CustomerPhone = msCustomerPhone
        ucChild1.CustomerMobile = msCustomerMobile

        If mColCustomerJobsGoods IsNot Nothing Then
            ucChild1.CustomerJobsGoods = mColCustomerJobsGoods
        End If  '--nothing-

        '==Call mbAddNewChild2(ucChild1, "ucChildCustomer", "Customers", tabPageChild1)

        ucChild1.Name = "ucChildNewJob1" '= strFormClassName & "_" & CStr(mIntChildCount)
        ucChild1.Text = ucChild1.Name
        ucChild1.Dock = DockStyle.Fill
        ucChild1.AutoSize = False
        '=ucChild1.Dock = DockStyle.Fill
        ucChild1.AutoSize = False

        ucChild1.Parent = grpBoxMain
        grpBoxMain.Controls.Add(ucChild1)

        ucChild1.delReport = AddressOf Me.subChildReport

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing


        'delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
        'Call delResized(grpBoxMain.Width - 7, grpBoxMain.Height - 7)
        'delResized = Nothing

        DoEvents()

    End Sub  '-- SHOWN --
    '= = = = = = = = = = = = = =  
    '-===FF->

    '-- Mdi Main form resized..--
    '--  form resized..--
    '-- DELEGATE for Resizing Child..
    Public Delegate Sub SubFormResized(ByVal intParentWidth As Integer, _
                                        ByVal intParentHeight As Integer)
    '-- This is instantiated below.
    Public delResized As SubFormResized '--    = AddressOf frmPosMainMdi.subChildReport

    '- Form re-sized..

    Private Sub frmNewJobbase_Resize(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If (Me.WindowState = System.Windows.Forms.FormWindowState.Minimized) Then
            Exit Sub
        End If

        '--  can't make smaller than original..-
        '-- IS FIXED-
        'If (Me.Height < mIntFormDesignHeight) Then
        '    Me.Height = mIntFormDesignHeight
        'End If
        'If (Me.Width < mIntFormDesignWidth) Then
        '    Me.Width = mIntFormDesignWidth
        'End If


        '-- resize main box and top panel-
        'panelBanner.Width = Me.Width - 11
        'grpBoxMain.Width = panelBanner.Width
        'grpBoxMain.Height = Me.Height - 93

        '= labDLLversion.Top = grpBoxMain.Top + grpBoxMain.Height + 27
        DoEvents()  '--time to adjust contents.

        '= btnExit.Left = btnOK.Left + 240
        '= btnExit.Top = btnOK.Top
        If (mUcChild1 IsNot Nothing) Then
            Dim ucChild1 As ucChildNewJob = mUcChild1
            delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
            '= Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
            Call delResized(grpBoxMain.Width - 7, grpBoxMain.Height - 7)
            delResized = Nothing
        End If  '-nothing.-

        DoEvents()

    End Sub   '-resize-
    '= = = = = = = = = = = = = =  
    '-===FF->




    '-- DELEGATE for CLOSING Child..
    '= Public Delegate Sub SubFormCloseRequest()
    Public Delegate Function SubFormCloseRequest() As Boolean

    '-- This is instantiated below.
    Public delCloseRequest As SubFormCloseRequest '--    = AddressOf frmPosMainMdi.subChildReport

    '-- Mouse on "X" on child Tab..


    Private Sub frmNewJob3_FormClosing(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim sMsg As String
        Dim bCanCloseOk As Boolean = False

        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                               System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing

                '= send close cmd..
                delCloseRequest = New SubFormCloseRequest(AddressOf mUcChild1.SubFormCloseRequest)
                'Call delCloseRequest()
                '= If mbCloseSelectedChild(tabPage1, sName) Then
                bCanCloseOk = delCloseRequest.Invoke
                '= MsgBox("bCanCloseOk is: " & bCanCloseOk, MsgBoxStyle.Information)
                If bCanCloseOk Then
                    '-remove close event handler.
                    RemoveHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing
                    intCancel = 0 '--let it go--
                Else
                    '-can't close
                    intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                End If
                'If (msModifiedControls <> "") Then
                '    If (MsgBox("Abandon changes ?", _
                '         MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                '        intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                '        Exit Sub
                '    End If
                'End If
                'intCancel = 0 '--let it go---
                ''==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel

    End Sub  '-closing-
    '= = = = =====  =


End Class  '-frmNewJobBase-

'= =end form = = = = =  = = =
