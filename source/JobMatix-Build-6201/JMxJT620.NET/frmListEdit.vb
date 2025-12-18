Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class frmListEdit
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	
	'-- JobTracking..--
	'--Form to hold LIST edit box--
	'-----Started 04-July-2009--
	'-----  Changed to ListView control.. 06-Jan-2010--
	'--grh- =15-Jul-2010-- Prohibit changing State or Postcode..-
	'--grh- =31-Jul-2010-- WARNING only on changing Postcode..-
	'--grh- =05-Aug-2010-- Fix..  allow setting rhs to be blank...-
	'--grh- =07-Sep-2010-- Fix..  Ambiguity in Exit/Cancel.-..-
	'--grh- =19-Sep-2010-- Fix..  Don't rebuild listview hdrs each edit.-..-
	'--grh- =06-Jan-2011-- Form now resizable...-
	'--grh- =03-Aug-2011-- Rev-2916--  Edit Textbox now multiline for SystemInfo- (SMS's, footnotes etc)...-
	'---     AND- More nuumeric checks..

    '--grh- =25-Aug-2011-- V3.0--  UPGRADED to VB.NET...-
    '--grh- =22-Mar-2012-- V3.0.3031--  Tidying up.....-
    '--grh- =11-Apr-2012-- V3.0.3041--  Tidying up..  Delete CDBL Upgrade junk....-
    '==
    '== grh- =19-Jul-2012-- V3.0.3067.0-  
    '==  >> Add link to help file....-
    '==  >> For Ref tables (Not sysInfo) SORT listview on Descr. col..
    '==  >> Rationalise buttons (form's default ok in code, Delete Finish button..)
    '==
    '== grh- =09-Mar-2013-- V3.0.3073.309-  
    '==  >> "Cancelled" is always false..- (Cancelled not relevant for ListEDit)..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  18-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


	Private msOrigData As String
	Private msFieldName As String
	
    Private mbCancelled As Boolean = False
    Private mbStartupDone As Boolean = False
    Private mbActivated As Boolean = False

    Private mbDataChanged As Boolean = False  '--29/12/2006--
    Private mbEditing As Boolean = False
	
    Private msTableName As String = ""
    Private msPKeyName As String = ""
    Private mCnnSql As OleDbConnection  '== ADODB.Connection '-- DB connection..-
	
    Private msIdColName As String = ""
    Private msDescrColName As String = ""
    Private mbDeletionsPermitted As Boolean = False
    Private mbInsertPermitted As Boolean = False

    Private mlMaxTextLength As Integer = 24

	'--Private mColOriginalList As Collection
	'--Private mColNewList As Collection
	Private mlFormDesignHeight As Integer '-- starting dimensions..-
    Private mlFormDesignWidth As Integer '-- starting dimensions..-

    Private mLngNoCols, mLngSortCol As Integer
	'= = =  = = = =
	
	
	'--load table name value--
	WriteOnly Property tableName() As String
		Set(ByVal Value As String)
			msTableName = Value
			'-txtMemo.Text = sText
		End Set
	End Property
	'= = = = = = = = = = =
	
	'--load PKEY name value--
	WriteOnly Property PrimaryKeyColName() As String
		Set(ByVal Value As String)
			msPKeyName = Value
			
		End Set
	End Property
	'= = = = = = = = = = =
	
	'--gives us original list--
	'--Property Let OriginalList(Col1 As Collection)
	'-    Set mColOriginalList = Col1
	'-txtMemo.Text = sText
	'--End Property
	'= = = = = = = = = = =
	
	'--  set max text width.--
	WriteOnly Property maxLength() As Integer
		Set(ByVal Value As Integer)
            'UPGRADE_WARNING: TextBox property txtEntry.maxLength has a new behavior. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            txtEntry.MaxLength = Value  '-in VB.NET:  NO longer useful..===
            mlMaxTextLength = Value
		End Set
	End Property '--max..-
	'= = = = = = = =
	
    WriteOnly Property connection() As OleDbConnection  '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)
            mCnnSql = Value

        End Set
    End Property '--connection.-
	'= = = = =  = = = =  = =
	
	'--  name of ID. column to save for update..-
	WriteOnly Property IdColumn() As String
		Set(ByVal Value As String)
			
			msIdColName = Value
		End Set
	End Property '--descr.-
	'= = = = = = = = = =  =
	
	'--  name of descr. column to edit..-
	WriteOnly Property DescrColumn() As String
		Set(ByVal Value As String)
			
			msDescrColName = Value
		End Set
	End Property '--descr.-
	'= = = = = = = = = =  =
	
	WriteOnly Property deletionsOK() As Boolean
		Set(ByVal Value As Boolean)
			
			mbDeletionsPermitted = Value
		End Set
	End Property '--deleteok--
	'= = = = = = = = = = =  =
	
	'--SET cancelled value-- '--let can.--
	'= = = = = = = = = = =
	
	'--sends back updated list--
	'--Property Get UpdatedList() As Collection
	'--    Set UpdatedList = mColNewList
	'-txtMemo.Text = sText
	'--End Property
	'= = = = = = = = = = =
	
	'--get cancelled value--
	Property cancelled() As Boolean
		Get
            cancelled = False    '=3073.309==  mbCancelled
		End Get
		Set(ByVal Value As Boolean)
			
			mbCancelled = Value
		End Set
	End Property
	'= = = = = = = = = = =
	'-===FF->
	
	'--  clean up sql string data ..--
	Private Function msFixSqlStr(ByRef sInstr As String) As String
		
		msFixSqlStr = Replace(sInstr, "'", "''")
    End Function '--fixSql-
	'= = = = = = = = = = = =

	'--convert numeric data for sorted display..-
	
	Private Function msFormat(ByVal v1 As Object, ByVal vType As Object, ByVal lSize As Integer) As String
        '==3067.0== Dim sResult As String
        '==3067.0== Dim sType As String '--sql type--

        msFormat = gsFormat(v1, vType, lSize)
        '==3067.0== sResult = CStr(v1) '--for strings..-
        '==3067.0== sType = UCase(gsGetSqlType(vType, lSize))
        '==3067.0== If (sType = "MONEY") Or (sType = "SAMLLMONEY") Then '--currency..-
        '==3067.0==    sResult = New String(" ", 9)
        '==3067.0==    sResult = RSet(FormatCurrency(v1, 2), Len(sResult))
        '==3067.0== ElseIf gbIsNumericType(sType) Then 
        '==3067.0==    sResult = New String(" ", 5)
        '==3067.0==    sResult = RSet(VB6.Format(v1, "####0"), Len(sResult))
        '==3067.0== ElseIf gbIsDate(sType) Then 
        '==3067.0==    sResult = VB6.Format(CDate(v1), "yyyy-mm-dd")
        '==3067.0== End If
        '==3067.0== msFormat = sResult
    End Function '--convert--
	'= = = = = = = = = = =  =
	'-===FF->

    '--  refresh listbox from table..--
	'--  refresh listbox from table..--
	
	Private Function mbRefresh(Optional ByVal bFirstTime As Boolean = False) As Boolean
		Dim sSql As String
        Dim s1, sSqlType As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        '== Dim fldx As ADODB.Field
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intADO_Type, intSize As Integer
		
		mbRefresh = False
		sSql = "Select * from [" & msTableName & "] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get [" & msTableName & "] recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Exit Function
        Else
            '-- create column headers...--
            ListView1.Items.Clear()
            If bFirstTime Then ListView1.Columns.Clear()
            Me.ListView1.ListViewItemSorter = Nothing  '--disable sorting until loaded..-

            '--ok.. build list box for ListEdit...-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not (rs1 Is Nothing) Then
                If bFirstTime Then '-- build headers once only..--
                    mLngNoCols = 0
                    For Each column1 As DataColumn In rs1.Columns   '== Each fldx In rs1.Fields '--get primary key col..-
                        s1 = "" & CStr(column1.ColumnName)
                        If LCase(s1) = LCase(msPKeyName) Then
                            mLngNoCols = mLngNoCols + 1
                            ListView1.Columns.Add("", s1, CInt(ListView1.Width) \ 5) '--20%--
                            Exit For
                        End If
                    Next column1  '--fldx  --
                    '-- get data col..--
                    For Each column1 As DataColumn In rs1.Columns   '==  fldx In rs1.Fields '--get descr. col..-
                        s1 = "" & CStr(column1.ColumnName)
                        If LCase(s1) = LCase(msDescrColName) Then
                            ListView1.Columns.Add("", s1, CInt((ListView1.Width \ 5) * 4)) '--80%.-
                            mLngSortCol = mLngNoCols
                            mLngNoCols = mLngNoCols + 1
                            Exit For
                        End If
                    Next column1 '--fldx  --
                End If '--first time.--
                '--Set colOriginal = New Collection
                '--mCnnSql.BeginTrans
                '--bCommitOk = False
                '== If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    If (msIdColName <> "") Then '--auto-ident PKEY col..- 
                        s1 = RSet(CStr(dataRow1.Item(msPKeyName)), 7) '--right-just id.. --
                    Else '--alpha--
                        s1 = dataRow1.Item(msPKeyName)  '--key col.-
                        '==item1.Text = rs1.Fields(msPKeyName).Value '--key col.-
                    End If
                    item1 = ListView1.Items.Add(s1)  '==item1.Text = s1
                    '-- format second column (data)-
                    gbConvertDotNetDataType(rs1.Columns(1), intADO_Type, sSqlType)
                    s1 = Trim(gsFormat(dataRow1.Item(msDescrColName), intADO_Type, intSize))

                    '==s1 = msFormat(dataRow1.Item(msDescrColName), _
                    '==                 dataRow1.Item(msDescrColName).Type, dataRow1.Item(msDescrColName).DefinedSize)
                    item1.SubItems.Add(s1)
                Next dataRow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==  rs1.MoveNext()
                '== End While '-eof-
                mbRefresh = True
                '== rs1.Close()
                If (msIdColName <> "") Then '--auto-ident PKEY col..-
                    '--  needs special sort compare object..
                    ' Set the ListViewItemSorter property to a new ListViewItemComparer 
                    ' object. Setting this property immediately sorts the 
                    ' ListView using the ListViewItemComparer object.
                    '== MsgBox("Sorting on col: " & mLngSortCol, MsgBoxStyle.Information)
                    Me.ListView1.ListViewItemSorter = New ListViewItemComparer(mLngSortCol)  '--Zero is FIRST SUB item..--
                Else '--char key.-
                    '== ListView1.SortKey = 0 '--1st col.-
                    '--  EASY to sort on first col..
                    ListView1.Sorting = SortOrder.Ascending
                End If
            End If '--rs nothing-
        End If '--get rs-
        ListView1.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--refresh..-
	'= = = = = = = = = = =  =
	'-===FF->
	
	'-- Load..  --
    '-- Load..  --
    '--- HEY  !  USER Properties have been set BEFORE Load event..
    '---     SO DO NOT CHANGE THEM HERE..
	
    Private Sub mbOriginal_frmListEdit_Load()
        Dim s1 As String
    
        '== mbCancelled = False
        '== mbStartupDone = False

        ListView1.Items.Clear()
        txtEntry.Text = ""
  
        '== mbDeletionsPermitted = False
        '== mbInsertPermitted = False

        '= mbDataChanged = False '--Clearing txtEntry set this..
        '== msPKeyName = ""
        '== msIdColName = "" '--insert avail only for ID col AUTO-IDENT--

        mlFormDesignHeight = VB6.PixelsToTwipsY(Me.Height) '--save starting dimensions..-
        mlFormDesignWidth = VB6.PixelsToTwipsX(Me.Width) '--save starting dimensions..-

        '== 3067.0 ==
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-ListEdit.htm")
        End If

    End Sub '--load--
	'= = = = = = = = = =  =
	'-===FF->
	
    '== 'UPGRADE_WARNING: Form event frmListEdit.Activate has a new behavior. Click for more:
    '===  'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    '== Private Sub frmListEdit_Activated(ByVal eventSender As System.Object, _
    '==                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

    Private Sub frmListEdit_Load(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        If mbStartupDone Then Exit Sub

        mbStartupDone = True
        Call mbOriginal_frmListEdit_Load()

        If msPKeyName = "" Then
            MsgBox("No primary-key name set.", MsgBoxStyle.Exclamation)
            Me.Hide()
        End If
        Me.Text = "  Editing Table:  '" & msTableName & "'"
        LabTable.Text = msTableName
        LabHelp1.Text = "Important Note:" & vbCrLf & _
                        "This is the complete list of items for the selected table. " & _
                         "Individual changes are updated as they are committed [OK], so the list on view will always " & _
                           "represent the contents of the table in the Jobs database.."

        mbEditing = False
        cmdOk.Enabled = False

    End Sub '--load--
	'= = = = = = = = =
    '-===FF->

    '--A c t i v a t e d-

    Private Sub frmListEdit_Activated(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True

        '== NB- IF  msIdColName is set, then PKEY is numeric, AUTO-IDENT col..--
        '==   ELSE PKEY  is Char-type fld, and INSERTS are not allowed..
        If (msIdColName <> "") Then '--auto-ident PKEY col..-
            If (LCase(msPKeyName) <> LCase(msIdColName)) Then
                MsgBox("Primary-key name is not comsistent with ID-col name.", MsgBoxStyle.Exclamation)
                mbCancelled = True
                Me.Hide()
                Exit Sub
            End If
            mbInsertPermitted = True '--insert avail only for ID col AUTO-IDENT--
        End If
        '--MsgBox "Memo Activate.."
        '---txtMemo.Text = msOrigData

        '--set up list..-
        If Not mbRefresh(True) Then
            Me.Hide()
            Exit Sub
        End If

        If Not mbDeletionsPermitted Then cmdRemove.Enabled = False
        LabAction.Text = "New Entry:"
        'UPGRADE_WARNING: TextBox property txtEntry.maxLength has a new behavior. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        LabRuler.Text = VB.Left(LabRuler.Text, txtEntry.MaxLength)

        '--systemInfo table has multiline text..
        If (LCase(msTableName) = "systeminfo") Then
            '==txtEntry.MultiLine = True  '--READ-ONLY at runtime..-
            '==txtEntry.ScrollBars = 2  '--vertical..- '--READ-ONLY at runtime..-
            txtEntry.Height = VB6.TwipsToPixelsY(1275)
            'UPGRADE_WARNING: TextBox property txtEntry.maxLength has a new behavior. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            mlMaxTextLength = 3000
            txtEntry.MaxLength = mlMaxTextLength
            VB6.SetDefault(cmdOk, False) '--Enter for CR..-- Need multiline.
            txtEntry.AcceptsReturn = True
        Else '-- user table..-
            txtEntry.Height = VB6.TwipsToPixelsY(315)
            VB6.SetDefault(cmdOk, True) '--Enter for CR..-- NO multiline.
            txtEntry.AcceptsReturn = False
            '== txtEntry.maxLength = 50  '--see our input property..
        End If '--systeminfo..-

        If (ListView1.Items.Count > 0) Then
            'UPGRADE_WARNING: Lower bound of collection ListView1.ListItems has changed from 1 to 0. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
            ListView1.FocusedItem = ListView1.Items.Item(0) '--first item..-
            cmdOk.Enabled = False
            cmdAbort.Enabled = False
            ListView1.Focus()
            LabRuler.Visible = False
        Else
            txtEntry.Focus()
        End If
        mbDataChanged = False
    End Sub '--activated.-
    '= = = = = = = = = =  =
    '-===FF->

    '--  form resized..--
    '--  form resized..--

    'UPGRADE_WARNING: Event frmListEdit.Resize may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub frmListEdit_Resize(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        '== Dim ix As Integer
        '== Dim intTabNo As Short
        '== Dim index As Short

        If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            '--  cant make smaller than original..-
            If (VB6.PixelsToTwipsY(Me.Height) < mlFormDesignHeight) Then Me.Height = VB6.TwipsToPixelsY(mlFormDesignHeight)
            If (VB6.PixelsToTwipsX(Me.Width) < mlFormDesignWidth) Then Me.Width = VB6.TwipsToPixelsX(mlFormDesignWidth)

            cmdEdit.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - 1485)
            cmdRemove.Left = cmdEdit.Left
            cmdCancel.Left = cmdEdit.Left
            '== cmdFinish.Left = cmdCancel.Left

            ListView1.Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(Me.Height) \ 9) * 4)
            ListView1.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - VB6.PixelsToTwipsX(ListView1.Left) - 1830)

            '-- vertical..--
            '== cmdFinish.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - 1700)
            cmdCancel.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - 1000)
            labHelp2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - 840)

            LabAction.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(ListView1.Top) + VB6.PixelsToTwipsY(ListView1.Height) + 180)
            txtEntry.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(LabAction.Top) + 300)
            LabRuler.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(txtEntry.Top) + 345)
            If (LCase(msTableName) = "systeminfo") Then
                txtEntry.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - VB6.PixelsToTwipsY(txtEntry.Top) - 1000)
            End If
            '==LabRuler.Top = Me.Height - 2070
            '===txtEntry.Top = Me.Height - 2430
            cmdOk.Top = txtEntry.Top
            cmdAbort.Top = txtEntry.Top

            '===LabAction.Top = Me.Height - 2670

        End If '--minimised..-
    End Sub '-resize..-
    '= = = = = = = = = =
    '-===FF->


    Private Sub listView1_ColumnClick(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.ColumnClickEventArgs) _
                                                 Handles ListView1.ColumnClick
        Dim lngNewKey As Integer
        '== Dim colHdr1 As System.Windows.Forms.ColumnHeader = ListView1.Columns(eventArgs.Column)

        '== lngNewKey = colHdr1.Index - 1 '-- get zero index of column clicked..-
        'UPGRADE_ISSUE: MSComctlLib.ListView property ListView1.SortKey was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        '== ListView1.SortKey = lngNewKey
        ' Set the ListViewItemSorter property to a new ListViewItemComparer 
        ' object. Setting this property immediately sorts the 
        ' ListView using the ListViewItemComparer object.
        '= MsgBox("Sorting on col: " & eventArgs.Column, MsgBoxStyle.Information)
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(eventArgs.Column)

    End Sub '--hdr click..-
    '= = = = = = = = = = =

    '--   txtEntry..--
    Private Sub txtEntry_Enter(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles txtEntry.Enter

        If mbEditing Or mbInsertPermitted Then
            txtEntry.ReadOnly = False
            cmdOk.Enabled = True
            cmdAbort.Enabled = True
            cmdRemove.Enabled = False
            cmdEdit.Enabled = False
            txtEntry.SelectionStart = 0
            txtEntry.SelectionLength = Len(txtEntry.Text)
        Else
            txtEntry.ReadOnly = True
        End If

    End Sub '--got focus..--
    '= = = = = = = = = ==

    '--   txtEntry..--
    'UPGRADE_WARNING: Event txtEntry.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtEntry_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles txtEntry.TextChanged

        mbDataChanged = True

        '-- TRUNCATE if needed..--
        If (Len(txtEntry.Text) > mlMaxTextLength) Then
            Beep()
            txtEntry.Text = VB.Left(txtEntry.Text, mlMaxTextLength)
        End If
        '== cmdFinish.Enabled = False
        cmdCancel.Enabled = True

    End Sub '--change..-
    '= = = = = = = = =  =
    '-===FF->

    '--ok--
    '--  editing item complete..-

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        '== Dim s1 As String
        Dim sSql As String
        Dim sDescr, sKey As String
        Dim sErrorMsg As String
        '== Dim lngId As Integer
        Dim lngaffected As Integer
        Dim bOk As Boolean
        Dim item1 As System.Windows.Forms.ListViewItem

        '--if EDIT, then UPDATE selected item..
        '--  else add as new item..--
        '===If (txtEntry.Text <> "") Then
        sDescr = Trim(txtEntry.Text)
        item1 = ListView1.FocusedItem
        '==If Not item1 Is Nothing Then
        If mbEditing And Not (item1 Is Nothing) Then '--    (ListView1.ListIndex >= 0) Then
            '-- UPDATE and Refresh..--
            '==lngId = ListView1.ItemData(ListView1.ListIndex)
            sKey = Trim(item1.Text) '--ist col..-

            '--  DIFFERENT if PKEY is numeric or Alpha  !!!  ---

            sSql = "UPDATE [" & msTableName & "] SET " & msDescrColName & "='" & msFixSqlStr(sDescr) & "' "
            If (msIdColName <> "") Then '--numeric key.-
                sSql = sSql & "  WHERE (" & msIdColName & "=" & sKey & "); " '-- CStr(lngId) + "); "
            Else '--char key.-
                sSql = sSql & "  WHERE (" & msPKeyName & "='" & sKey & "'); "
            End If '--numeric.-
            '-- Confirm updating systemInfo..--
            bOk = True
            If (LCase(msTableName) = "systeminfo") Then
                If (LCase(sKey) = "labourhourlyrate") Or (LCase(sKey) = "labourmincharge") Or _
                        (LCase(sKey) = "labourhourlyratepriority1") Or (LCase(sKey) = "labourhourlyratepriority2") Or _
                          (LCase(sKey) = "labourhourlyratepriority3") Or (LCase(sKey) = "servicenotificationcostlimit") Then
                    If Not IsNumeric(sDescr) Then
                        MsgBox("Cost Values must be numeric {eg $$$.cc..", MsgBoxStyle.Exclamation)
                        bOk = False
                    End If
                End If
                If bOk Then
                    If (MsgBox("OK to change SystemInfo '" & sKey & "'  to:" & vbCrLf & vbCrLf & _
                               IIf((sDescr = ""), "(BLANK)", sDescr) + vbCrLf, _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, _
                                                                     "System Settings") <> MsgBoxResult.Yes) Then
                        bOk = False
                    End If
                End If
            End If
            If bOk Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                bOk = gbExecuteCmd(mCnnSql, sSql, lngaffected, sErrorMsg)
                System.Windows.Forms.Cursor.Current = Cursors.Default
                If Not bOk Then MsgBox("Failed to UPDATE item in DB.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            End If
            '--List1.RemoveItem (List1.ListIndex)
            '--DoEvents
            '--List1.AddItem txtEntry.Text
        ElseIf mbInsertPermitted Then
            '--new entry--
            '--  INSERT -- and refresh..-
            sSql = "INSERT INTO [" & msTableName & "] (" & msDescrColName & ") VALUES ('" & sDescr & "' ) " & vbCrLf
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            bOk = gbExecuteCmd(mCnnSql, sSql, lngaffected, sErrorMsg)
            System.Windows.Forms.Cursor.Current = Cursors.Default
            If Not bOk Then MsgBox("Failed to INSERT item in DB.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)

            '--List1.AddItem txtEntry.Text
        End If '-edit.--
        Call mbRefresh()
        txtEntry.Text = ""
        '==End If  '--nothing.-
        '===End If  '--text-

        cmdOk.Enabled = False
        cmdAbort.Enabled = False
        If mbDeletionsPermitted Then cmdRemove.Enabled = True
        cmdEdit.Enabled = True
        mbDataChanged = False '--clean again..-
        '= cmdFinish.Enabled = True
        '== cmdCancel.Enabled = False

        ListView1.Enabled = True
        ListView1.Focus()

    End Sub '--ok..--
    '= = = = = = = = =  =
    '-===FF->

    '--  editing item aborted....-

    Private Sub cmdAbort_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdAbort.Click

        txtEntry.Text = ""
        cmdOk.Enabled = False
        cmdAbort.Enabled = False
        mbDataChanged = False '--clean again..-

        ListView1.Enabled = True
        ListView1.Focus()

    End Sub '--ok..--
    '= = = = = = = = =  =


    '-- cmd E D I T  item.. --

    Private Sub cmdEdit_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdEdit.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim sKey As String

        item1 = ListView1.FocusedItem
        If Not (item1 Is Nothing) Then
            '--If ListView1.ListIndex >= 0 Then '-- item is selected..--
            sKey = item1.Text
            If (LCase(msTableName) = "systeminfo") Then
                If (LCase(sKey) = "businesspostcode") Then
                    If MsgBox("CAUTION! The JobTracking Licence Key is related to your business Postcode." & vbCrLf & _
                                  "If you change it you will need to apply for a replacement Licence Key.." & vbCrLf & vbCrLf & _
                                    "Are you sure you want to change the business Postcode?", _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                        mbEditing = False
                        Exit Sub
                    End If '--postcode.-
                ElseIf (LCase(sKey) = "businessusername") Or (LCase(sKey) = "businessabn") Or _
                          (LCase(sKey) = "businessshortname") Or (LCase(sKey) = "businessstate") Or _
                                    (LCase(sKey) = "jt2securityid") Or (LCase(VB.Left(sKey, 10)) = "rm_jetpath") Then
                    MsgBox("Can't change that value..", MsgBoxStyle.Exclamation)
                    mbEditing = False
                    Exit Sub
                End If
            End If '--sysinfo.-
            'UPGRADE_WARNING: Lower bound of collection item1 has changed from 1 to 0. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
            txtEntry.Text = item1.SubItems(1).Text '--descr. text..   '-- Trim(ListView1.List(ListView1.ListIndex))
            'UPGRADE_WARNING: TextBox property txtEntry.maxLength has a new behavior. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            LabAction.Text = "Edit " & sKey & ": (Max. " & txtEntry.MaxLength & " chars.)"
            mbEditing = True
            ListView1.Enabled = False
            txtEntry.Focus()
            cmdCancel.Enabled = True
        End If '--selected..-

    End Sub '--edit.--
    '= = = = = = = = =
    '-===FF->

    '-- Remove an item..--
    Private Sub cmdRemove_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdRemove.Click
        Dim s1 As String
        Dim sKey As String
        Dim sSql As String
        Dim sErrorMsg As String
        '==Dim lngId As Long
        Dim lngaffected As Integer
        Dim bOk As Boolean
        Dim item1 As System.Windows.Forms.ListViewItem

        item1 = ListView1.FocusedItem
        '===If ListView1.ListIndex >= 0 Then '-- item is selected..--
        If Not (item1 Is Nothing) Then
            sKey = Trim(item1.Text) '-- ListView1.List(ListView1.ListIndex)
            'UPGRADE_WARNING: Lower bound of collection item1 has changed from 1 to 0. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
            s1 = item1.SubItems(1).Text '--value..-
            If MsgBox("Remove the item:" & vbCrLf & sKey & "= " & s1 & vbCrLf & vbCrLf & "From the table?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then

                '-- get id and DELETE from table..--
                ListView1.Enabled = False
                '==lngId = ListView1.ItemData(ListView1.ListIndex)
                '--delete 1 rowl-
                If msIdColName <> "" Then ''-numeric..-
                    sSql = "DELETE FROM [" & msTableName & "]  WHERE (" & msIdColName & "=" & sKey & "); "
                Else '--char key..-
                    sSql = "DELETE FROM [" & msTableName & "]  WHERE (" & msPKeyName & "='" & sKey & "'); "
                End If
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                bOk = gbExecuteCmd(mCnnSql, sSql, lngaffected, sErrorMsg)
                System.Windows.Forms.Cursor.Current = Cursors.Default
                If Not bOk Then MsgBox("Failed to DELETE item from DB.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                '--refresh the list..--
                Call mbRefresh()

                '-mbDataChanged = True
            End If '--ask-
        End If '--selected..-
        ListView1.Enabled = True
        ListView1.Focus()
    End Sub '--remove--
    '= = = = = = = =  =
    '-===FF->

    Private Sub listView1_Enter(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles ListView1.Enter

        If mbDeletionsPermitted Then
            cmdRemove.Enabled = True
        Else
            cmdRemove.Enabled = False
        End If
        '--cmdRemove.Enabled = True
        cmdEdit.Enabled = True
        LabAction.Text = ""
        If mbInsertPermitted Then LabAction.Text = "New Entry:"
        mbEditing = False
    End Sub '--focus.-
    '= = = = = = = = = = =

    '-- ENTER on ListBox..-
    Private Sub listView1_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then
            '--If ListView1.ListIndex >= 0 Then '-- item is selected..--
            keyAscii = 0 '--done..-
            Call cmdEdit_Click(cmdEdit, New System.EventArgs())
            '--End If '--selected..-
        End If '--13-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--enter--
    '= = = = = =  = = = =
    '-===FF->

    '-- Exit-  cancel form..-
    '-- Exit-  cancel form..-

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dim ans As Short

        If Not mbDataChanged Then '--exit..-
            mbCancelled = True
            Me.Hide()
            Exit Sub
        End If
        '--ask if we want to cancel--

        ans = MsgBox("Cancel this entry (abandons last edit..) ?", _
                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, "Memo entry")
        If ans = MsgBoxResult.Yes Then
            '--set cancel flag--
            mbCancelled = True
            '--mbStartupDone = False
            '--mbClosingDown = True

            Me.Hide()
        End If

    End Sub '--Exit/cancel--
    '= = = = = =  ==  = =  =

    '==3067.0== Private Sub cmdFinish_Click(ByVal eventSender As System.Object, _
    '==3067.0==                               ByVal eventArgs As System.EventArgs)

    '==3067.0== Dim ans As Short

    '==3067.0==     If Not mbDataChanged Then '--same as cancel..-
    '==3067.0==         Me.Hide()
    '==3067.0==         Exit Sub
    '==3067.0==     End If
    '==3067.0==     ans = MsgBox("Save this new edit and exit..?", _
    '==3067.0==                      MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, "Memo entry")
    '==3067.0==     If ans = MsgBoxResult.Yes Then
    '==3067.0==         Me.Hide()
    '==3067.0==     End If

    '==3067.0==     End Sub '--ok--
    '= = = = = =  = = = =  =
    '-===FF->

    '= = = u n l o a d = = = = = = =

    Private Sub frmListEdit_FormClosed(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '--MsgBox "frmMemo UNload event..'"  '-debug--
        '--If Not mbClosingDown Then
        '--     MsgBox "Please use OK/Cancel buttons to exit form..", vbInformation, "Memo.."
        '--     intCancel = 1  '--cant close yet--
        '--End If
    End Sub '--unload--

    '= = = = = = =
    '--= = = u n l o a d = = = = = = =

    Private Sub frmListEdit_FormClosing(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        '--MsgBox "frmMaint UNload event..'"  '-debug--
        '--If Not gbclosingDown Then
        'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                               System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                           System.Windows.Forms.CloseReason.FormOwnerClosing   '==, vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                '--Call cmdExit_Click
                If mbDataChanged Then
                    MsgBox("Changes have been made...", MsgBoxStyle.Information, "List Edit.")
                    intCancel = 1 '--cant close yet--
                Else
                    intCancel = 0 '--let it go--
                End If
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--queryUnload--
    '= = = = = = = = = = =

    '= = = end my form ===
End Class

'==  listView column comparartor..-

' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer

    Private col As Integer

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
       Implements IComparer.Compare
        Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
    End Function
End Class

'=== totally the end ===
