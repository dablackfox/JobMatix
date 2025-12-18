Option Strict Off
Option Explicit On
Friend Class frmDirSearch
	Inherits System.Windows.Forms.Form
	'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = =
	
	
	'--  directory Search.--
	'-----  FROM MSDN "WinSeek"  Sample application..-
	
	'--  FORM needs:
	
	'-- Drive list box     Name drvList
	'-- Directory list box Name dirList
	'-- File list box      Name filList   Pattern  *.*
	'-- List box           Name lstFoundFiles
	
	'-- First command button   Name: cmdSearch,  Caption: &Search,  Default: True
	'-- Second command button  Name: cmdExit,  Caption  E&xit
	
	
	Private mbCancelled As Boolean
	Private mbCurrentPath As String
	
	
	WriteOnly Property FilePattern() As String
		Set(ByVal Value As String)
			
			txtSearch.Text = Value
			filList.Pattern = Value
		End Set
	End Property
	'= = = = = = = = = =
	
	ReadOnly Property path() As String
		Get
			
			path = mbCurrentPath
		End Get
	End Property '--path..-
	'= = = = = = = = = = = = =
	
	ReadOnly Property cancelled() As Boolean
		Get
			
			cancelled = mbCancelled
		End Get
	End Property
	'= = = = = = = = = =  =
	
	'-- L o a d --
	'-- L o a d --
	
	Private Sub frmDirSearch_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		mbCurrentPath = ""
		mbCancelled = False
		
	End Sub '--load ..--
	'= = = = = = = = = =
	
	'UPGRADE_WARNING: Form event frmDirSearch.Activate has a new behavior. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmDirSearch_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		
		txtDirPath.Text = dirList.Path
		dirList.Focus()
		
	End Sub '--activate..
	'= = = = = = = = =  =
	
	'--srch Arg.-
	
	'UPGRADE_WARNING: Event txtSearch.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtSearch_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSearch.TextChanged
		
		filList.Pattern = txtSearch.Text
		
	End Sub '--srch-
	'= = = = = = = = = = = = = =
	
	'-- The Drive List Box's Change Event
	'---When the user clicks an item in the drive list box,
	'----its Change event is generated. The drvList_Change event procedure is invoked,
	'-----and the following code is run:
	
	Private Sub drvList_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles drvList.SelectedIndexChanged
		On Error GoTo DriveHandler
		'-- If new drive was selected, the Dir1 box
		'-- updates its display.
		dirList.Path = drvList.Drive
		txtDirPath.Text = dirList.Path
		Exit Sub
		'-- If there is an error, reset drvList.Drive with the
		'-- drive from dirList.Path.
DriveHandler: 
		drvList.Drive = dirList.Path
		Exit Sub
	End Sub
	'= = = = = =
	
	
	'--- The Directory List Box's Change Event
	'---If the user double-clicks an item in the directory list box,
	'-----or if the Path property of dirList is changed in code
	'----  (as in the drvList_Change procedure),
	'------the dirList_Change event is initiated.
	'-----The following code responds to that event:
	
	Private Sub dirList_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dirList.Change
		'-- Update file list box to synchronize with the
		'-- directory list box.
		filList.path = dirList.Path
		txtDirPath.Text = dirList.Path
	End Sub
	'= = = = = = =
	'--This event procedure assigns the Path property of the dirList box
	'----to the Path property of the filList box.
	'---This causes a PathChange event in the filList list box, which is redrawn;
	'---you don't need to add code to the filList_PathChange procedure,
	'-----because in this application, the event chain ends in the filList list box.
	
	'-- catch ENTER key on dirList.. --
	
	Private Sub dirList_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles dirList.KeyPress
		Dim keyAscii As Short = Asc(eventArgs.KeyChar)
		
		If keyAscii = 13 Then '--enter --
			'==If (dirList.ListIndex >= 0) Then  '--leaf is sekected..-
			dirList.Path = dirList.DirList(dirList.DirListIndex) '--simulate dbl-click.-
			'==End If
			keyAscii = 0 '--done..-
		End If
		
		eventArgs.KeyChar = Chr(keyAscii)
		If keyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub '--enter.-
	
	
	'---The Command Button's Click Event
	'---This event procedure determines whether the highlighted item
	'--- in the dirList list box is the same as the dirList.Path.
	'---If the items are different, then dirList.Path is updated.
	'----If the items are the same, then the search is performed.
	
	Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
		'- .
		'- .
		'- .
		' If the dirList.Path is different from the
		' currently selected directory, update it;
		' otherwise perform the search.
		'==If dirList.path <> dirList.List(dirList.ListIndex) Then
		'==     dirList.path = dirList.List(dirList.ListIndex)
		'==     Exit Sub
		'==Else
		If (dirList.DirListIndex >= 0) Then
			mbCurrentPath = dirList.DirList(dirList.DirListIndex)
		Else
			mbCurrentPath = dirList.Path
		End If
		Me.Hide()
		' Continue with search.
		'- .
		'- .
		'- .
	End Sub
	'= = = = = = = =
	'--Note   You can enhance the WinSeek application with additional features.
	'-- For example, you might want to use a file control's attribute properties.
	'---You could use check boxes to allow the user to set different combinations
	'----of file attributes so that the file list box displays files
	'------ that are Hidden, System, and so on.
	'----This would restrict a search to conforming files.
	
	
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		mbCancelled = True
		Me.Hide()
	End Sub
	'= = = = = = = =
	
	'== the end ==
End Class