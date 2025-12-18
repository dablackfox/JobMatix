
Option Strict Off
Option Explicit On
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports System.Math
Imports VB = Microsoft.VisualBasic


Public Class clsPrintPreviewDialogWithPrinter
    Inherits PrintPreviewDialog

    '==
    '==  JobMatixPOS . 
    '==
    '==    -- 4201.0618/0623.  27-October-2019-   
    '==      NOW PUBLIC. >> Report Printer class.  ADD sub-classed printPreviewDialog to catch Print Button 
    '==                              and show print Dialog to choose printer...
    '==
    '= = = =  = = = = = = = = = = = = = = == = =  = = = = == = = = = = = = = = = = = = = = = = =


    '-  https://social.msdn.microsoft.com/Forums/vstudio/en-US/4e6e60f8-55fe-4a14-848f-c8c1103864ff/printpreviewdialog-how-can-i-add-a-button-to-select-a-printer

    '= Here is what I did to fix the problem.

    '=    I created a class called PrintPreviewDialogSelectPrinter.  In it I have two subs. 
    '== Basically, the class inherits the PrintPreviewDialog, adds a button that functions as I want, 
    '--  and removes the button that doesn't.  I finally found a sample online that I could understand.  
    '==   I'm sure there are better ways of doing this, but it works very well.  Call it just like PrintPreviewDialog.

    '= Very good solution. Thanks a lot.

    '========  Mod to above..==

    '= I allow myself some remarks to the code:
    '= 1. Moving the initialising code to sub New() prevents errors at second call
    '= 2. It is enough, to add a handler to the existing button. One may not exchange it.
    '= 3. It is usefull, to close the dialog, wenn job is done.

    '= So that is what I use:

    Private ts As ToolStrip
    Private printItem, myPrintItem As ToolStripItem

    Public Sub New()
        '=Dim PrintButton As ToolStripItem = ts.Items("printToolStripButton")

        '-- grh-  can't add handler..
        '-- must override-
        '--  https://msdn.microsoft.com/en-us/library/aa290043(v=vs.71).aspx
        '== AddHandler PrintButton.Click, AddressOf MyPrintItemClicked

        '--SO- use J.mohson.88 method to add new button.
        'Get the toolstrip from the base control
        ts = CType(Me.Controls(1), ToolStrip)
        'Get the print button from the toolstrip
        printItem = ts.Items("printToolStripButton")

        'Add a new button 
        With printItem
            '= Dim myPrintItem As ToolStripItem
            myPrintItem = ts.Items.Add(.Text, .Image, New EventHandler(AddressOf MyPrintItemClicked))
            '= myPrintItem.DisplayStyle = ToolStripItemDisplayStyle.Image
            myPrintItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            myPrintItem.Width = 100 '-grh-
            'Relocate the item to the beginning of the toolstrip
            ts.Items.Insert(0, myPrintItem)
        End With

        'Remove the orginal button
        ts.Items.Remove(printItem)

    End Sub  '--new-
    '= = = = = ===== =

    Private Sub myPrintPreview_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'if you like:
        '= Me.Height = FrmMain.Height
        '= Me.Width = FrmMain.Width
    End Sub
    '= = = = = == == =

    Private Sub MyPrintItemClicked(ByVal sender As Object, ByVal e As EventArgs)

        Dim dlgPrint As New PrintDialog
        Try
            With dlgPrint
                .AllowSelection = True
                .ShowNetwork = True
                .Document = Me.Document 'me verweist offenbar schon auf das Doc im Dialog.
            End With
            If dlgPrint.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.Document.Print()
            End If
        Catch ex As Exception
            MsgBox("Print Error: " & ex.Message)
        End Try
        Me.Close()  'to make it more convenient
    End Sub




End Class '- clsPrintPreviewDialogWithPrinter=
'= = = = = = = = = = = = = = = = = = = = = = =
