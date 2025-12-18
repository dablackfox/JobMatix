
Option Strict Off
Option Explicit On
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports System.Math
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Windows.Forms.Application

'== Created 21-Jan-2020-  For POS Reports Upgrade..

Public Class clsReportToGrid


    '==
    '==   -- 4220.0121.   21-Jan-2020- -   
    '==       --  clsReportToGrid Created to decode standard Report text... 
    '==            so it can go into a DataGridView..
    '==
    '==    
    '==   New Build- == 4233.0416.  16-April-2020- 
    '==
    '==   --  Made Reports into a Child UserControl..
    '==   -- Show Spme movement while building report grid rows...
    '==
    '==
    '= = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = == 
    '==
    '= = = =  = = = = = = = = = = = = = = == = =  = = = = == = = = = = = = = = = = = = = = = = =

    '= = = = = = = = = = = =  == 
    Private Structure AttribInfo
        Dim Name As String
        Dim Value As String
    End Structure
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    Private mColReportLines As Collection '--receipt lines-
    Private mIntUserItemsPrinted As Integer = 0
    '-- wait form--
    Private mFormWait1 As frmWait


    Public Sub New()

        MyBase.New()
        '== Class_Initialize_Renamed()


     
    End Sub '--initalise..--
    '= = = = = =  = = = ==
    '= = = = = =  = = = = == = = = = = = = ==
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = "POS Reports"  '=msVersionPOS
        mFormWait1.labHdr.Text = "Sales Invoices-"
        mFormWait1.labMsg.Text = sMsg
        '= mFormWait1.Show(mFrmParent1)
        mFormWait1.Show()
        DoEvents()
    End Sub '- mWaitFormOn-
    '-= = = = =  = = = = = =

    '-- kill (hide) wait form--
    Private Sub mWaitFormOff()

        mFormWait1.Hide()
        mFormWait1.Close()
        mFormWait1.Dispose()
        DoEvents()
    End Sub  '--wait--
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '-- check for attribute-
    '-- Return the value if found..-

    Private Function mbHasAttribute(ByVal colAttributes As Collection, _
                                    ByVal strAttribute As String, _
                                    ByRef strvalue As String) As Boolean
        mbHasAttribute = False
        If (Not (colAttributes Is Nothing)) AndAlso (colAttributes.Count > 0) Then
            For Each attrib1 As AttribInfo In colAttributes
                If UCase(attrib1.Name) = UCase(strAttribute) Then
                    strvalue = attrib1.Value
                    mbHasAttribute = True
                    Exit For
                End If
            Next attrib1
        End If  '-nothing-
    End Function  '-check for attr.-
    '= = = = = =  = = = = == = = = = = = = ==
    '-===FF->

    '-mbLoadAttributes-

    Private Function mbLoadAttributes(ByVal strAttributeText As String, _
                                       ByRef colAttributes As Collection) As Boolean
        Dim sRemainder As String = strAttributeText
        Dim intPos As Integer
        Dim sAttribName, sAttribValue As String
        Dim attribInfo1 As AttribInfo

        mbLoadAttributes = False
        Try
            colAttributes = New Collection
            While (Len(sRemainder) > 0)
                intPos = InStr(sRemainder, "=")
                If (intPos = 0) Then
                    sRemainder = ""
                Else
                    sAttribName = Trim(Left(sRemainder, intPos - 1))
                    sRemainder = Trim(Mid(sRemainder, intPos + 1))
                    If (sRemainder <> "") Then
                        '= intPos = FindUnquotedWhitespace(sRemainder, 1)
                        '-- RHS may be quoted (double-quotes only) or not.
                        If Left(sRemainder, 1) = Chr(34) Then '--starts with double quote-
                            intPos = InStr(2, sRemainder, Chr(34))
                            If (intPos > 0) Then
                                sAttribValue = Trim(Mid(sRemainder, 2, intPos - 2))
                                sRemainder = Trim(Mid(sRemainder, intPos + 1))
                            Else  '-no closing quote-
                                sRemainder = ""
                                Exit While
                            End If
                        Else  '-no quote-
                            '-take next token as rhs-
                            intPos = InStr(2, sRemainder, " ")
                            If (intPos > 0) Then
                                sAttribValue = Trim(Left(sRemainder, intPos - 1))
                            Else
                                '- end bit-
                                sAttribValue = Trim(sRemainder)
                                sRemainder = ""
                            End If
                        End If '-quote-
                        attribInfo1.Name = sAttribName
                        attribInfo1.Value = sAttribValue
                        colAttributes.Add(attribInfo1) '=nAttributeCount = nAttributeCount + 1
                    End If  '-remainder-
                End If  '-pos-
            End While  '-remainder-
            mbLoadAttributes = True
            Exit Function

        Catch ex As Exception
            MsgBox("Report-To-Grid- Error in LoadAttributes-" & vbCrLf & ex.Message)
        End Try
    End Function  '-mbLoadAttributes-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-DecodeStandardReport-
    '-- POS Report Printer would normally print this..
    '--  Just extract the data from the Markup.
    '-- and dump into DGV using the TAB attributes to choose a column....

    Public Function DecodeStandardReport(ByRef colReportLines As Collection, _
                                         ByRef colTabStops As Collection, _
                                         ByRef asHeaderTexts() As String, _
                                         ByRef dgvReport As DataGridView) As Boolean
        Dim colTabStopCols As Collection
        '= Dim bHalfLineCounted As Boolean = False
        Dim strDate As String = DateTime.Now.ToLongDateString() & " " & _
                        DateTime.Now.ToShortTimeString()
        Dim s1, sText, sTextline As String
        '= Dim strFormat1 As StringFormat
        '= Dim font1 As userFontDef
        '= Dim fontPrintText As Font
        '-- A Rectangle has Left, Top, Width, height--
        '= Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        '= Dim intPrintHeight As Integer = rectPageBounds.Height - 32
        '= Dim intPrintWidth As Integer = rectPageBounds.Width - 32
        Dim intLineCount As Integer = 0
        Dim intLinesNeeded As Integer
        Dim iPos, iPos2, iPos3, intX, intWidth, ix As Integer
        Dim sFldText, sFldAttributes, sValue As String
        '=Dim asAttributeTokens() As String
        Dim colAttributes As Collection '- of AttribInfo
        Dim colTextFieldItems As Collection

        Dim intTabPos, intColPos As Integer
        Dim intGridRowX As Integer = -1
        Dim gridRow1 As DataGridViewRow

        Dim cellAny As DataGridViewTextBoxCell

        DecodeStandardReport = False

        If (colReportLines Is Nothing) OrElse (colReportLines.Count <= 0) Then
            Exit Function
        End If

        If (colTabStops Is Nothing) OrElse (colTabStops.Count <= 0) Then
            Exit Function
        End If

        dgvReport.Rows.Clear()
        dgvReport.Columns.Clear()

        mColReportLines = colReportLines
        '- (for code re-use).
        mIntUserItemsPrinted = 0

        '-- make list of colNos with TabStop as key..
        '--  WE can translate TabStop to a column index.
        colTabStopCols = New Collection

        '-create cols..
        cellAny = New DataGridViewTextBoxCell
        cellAny.Style.BackColor = Color.White
        Dim column1 As DataGridViewColumn
        Dim intTabStop, intMaxColNo As Integer

        Try
            For ix = 1 To colTabStops.Count  'How many do you want?
                column1 = New DataGridViewColumn
                s1 = asHeaderTexts(ix - 1)
                '= dgvReport.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '--cost
                With column1
                    .HeaderText = s1
                    .Name = s1
                End With
                column1.CellTemplate = cellAny
                If InStr(LCase(s1), "amount") > 0 Then
                    column1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--amount.
                End If
                dgvReport.Columns.Add(column1)
                intTabStop = CInt(colTabStops(ix))
                colTabStopCols.Add(ix - 1, CStr(intTabStop))
                intMaxColNo = ix - 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Cursor.Current = Cursors.WaitCursor

        '---FROM-  Print a page of report lines-
        Try
            Call mWaitFormOn("Wait..  Loading dataGrid..")
            While (mIntUserItemsPrinted < mColReportLines.Count) '= And (intLineCount < intMaxLines)
                '=bLineHasBarcode = False
                '-- NEXT LINE-  PARSE the the string for text items..-
                sText = Trim(mColReportLines.Item(mIntUserItemsPrinted + 1))
                If (VB.Left(UCase(sText), 9) = "<TEXTLINE") Then
                    colTextFieldItems = New Collection
                    '-extract fld list-
                    iPos = InStr(10, sText, ">")
                    If (iPos > 0) Then '-found start tag end-
                        iPos2 = InStr(iPos + 1, UCase(sText), "</TEXTLINE")
                        If (iPos2 > iPos + 1) Then '-found </TEXTLINE-
                            sTextline = Trim(Mid(sText, (iPos + 1), (iPos2 - (iPos + 1)) + 1))
                            gridRow1 = New DataGridViewRow
                            dgvReport.Rows.Add(gridRow1)
                            intGridRowX = dgvReport.Rows.Count - 1  '--last row -
                            With dgvReport.Rows(intGridRowX)
                                '-- .Cells(k_CUSTGRIDCOL_CUSTNO).Value = row1.Item("barcode")
                                .HeaderCell.Value = CStr(mIntUserItemsPrinted + 1)
                            End With
                            intColPos = 0
                            Application.DoEvents()
                            '-  update every 1000.
                            If ((intGridRowX Mod 1000) = 0) Then
                                mFormWait1.labMsg.Text = "Wait..  Loading Grid- " & vbCrLf & " Line " & _
                                        FormatNumber(intGridRowX, 0) & " of " & FormatNumber(mColReportLines.Count, 0) & ".."
                            End If
                            '- Parse and extract/print fld list-
                            '= intXpos = intLeftMargin
                            iPos = InStr(1, UCase(sTextline), "<TXT")
                            While (iPos > 0) And (sTextline <> "")
                                iPos2 = InStr(iPos + 4, sTextline, ">")  '--end of txt tag stuff.
                                If (iPos2 > 0) Then
                                    sFldAttributes = Trim(Mid(sTextline, iPos + 4, (iPos2 - (iPos + 4)) + 1))
                                    '==asAttributeTokens = Split(UCase(Replace(sFldAttributes, ":", " ")), " ")
                                    If Not mbLoadAttributes(sFldAttributes, colAttributes) Then
                                        '= ev.HasMorePages = False  '-error-
                                        Exit Function
                                    End If
                                    '-- TEST- show all attributes-
                                    s1 = "Attributes are:" & vbCrLf
                                    For Each att1 As AttribInfo In colAttributes
                                        s1 &= att1.Name & "=" & att1.Value & vbCrLf
                                    Next '--att1-
                                    '= MsgBox(s1)
                                    '-- get inner data to print-
                                    iPos3 = InStr(iPos2 + 1, UCase(sTextline), "</TXT>")  '-find closing tag-
                                    '== MsgBox("Found fld text at pos: " & iPos)
                                    If (iPos3 > 0) Then
                                        sFldText = Trim(Mid(sTextline, iPos2 + 1, iPos3 - (iPos2 + 1)))
                                        sTextline = Trim(Mid(sTextline, iPos3 + 6)) '- get Remainder-
                                        '= MsgBox("Found fld text:" & vbCrLf & sFldText)
                                        '-- get TAB pos if any..
                                        intColPos = intMaxColNo  '--DEFAULT--
                                        If mbHasAttribute(colAttributes, "TAB", sValue) Then
                                            If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                                                '= intXpos = CInt(sValue) + intLeftMargin
                                                intTabPos = CInt(sValue)
                                                If colTabStopCols.Contains(CStr(intTabPos)) Then
                                                    intColPos = colTabStopCols.Item(CStr(intTabPos))
                                                Else
                                                    intColPos = intMaxColNo
                                                End If
                                            End If  '-numeric-
                                        End If  '-tab-

                                        '--GRID version..
                                        '-- Just assign text-pieces to columns as we go..
                                        With dgvReport.Rows(intGridRowX)
                                            .Cells(intColPos).Value = sFldText
                                        End With
                                        '= intColPos += 1

                                        '= fontPrintText = mFontContent
                                        If mbHasAttribute(colAttributes, "FONTSTYLE", sValue) Then
                                            If InStr(LCase(sValue), "bold") > 0 Then
                                                '= fontPrintText = mFontContentBold
                                            ElseIf InStr(LCase(sValue), "big") > 0 Then
                                                '= fontPrintText = mFontContentBig
                                            ElseIf InStr(LCase(sValue), "barcode") > 0 Then
                                                '= fontPrintText = mFontBarcode
                                                '= bLineHasBarcode = True
                                            End If  '-bold-
                                        End If  '-font-
                                        '=intWidth = mlGetTextWidth(ev, sFldText, fontPrintText)
                                        If mbHasAttribute(colAttributes, "WIDTH", sValue) Then
                                            If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                                                intWidth = CInt(sValue)
                                            End If  '-numeric-
                                        End If  '-width-
                                        Try
                                            '- check if ALIGN= right (needs Rectangle)-
                                            If mbHasAttribute(colAttributes, "ALIGN", sValue) AndAlso _
                                                                        (InStr(LCase(sValue), "right") > 0) Then
                                                '= rectField = New Rectangle(intXpos, intYpos, intWidth, intItemLineHeight)
                                                'ev.Graphics.DrawString(sFldText, _
                                                '               fontPrintText, brushContent, rectField, mStrFormatRight)
                                            Else  '-left align-
                                                'rectField = New Rectangle(intXpos, intYpos, intWidth * 2, intItemLineHeight)
                                                'ev.Graphics.DrawString(sFldText, _
                                                '   fontPrintText, brushContent, rectField, mStrFormat1)
                                            End If  '--align-
                                        Catch ex As Exception
                                            Call mWaitFormOff()
                                            MsgBox("ERROR in graphcs drawstring-" & vbCrLf & _
                                                   ex.Message & vbCrLf & "Text was: " & sFldText, MsgBoxStyle.Exclamation)
                                        End Try
                                        '-- add width to xpos-  (in case next fld has no TAB pos)-
                                        '= intXpos += intWidth '= mlGetTextWidth(ev, sFldText, fontPrintText)
                                        '-- look for next txt fld in this line.
                                        iPos = InStr(1, UCase(sTextline), "<TXT")

                                    End If  '-ipos3-
                                Else  '-invalid txt tag..
                                    Exit While
                                End If
                            End While  '--<txt-
                        Else '-no end tag-
                        End If '-found </TEXTLINE-
                    Else  '-bad textline start tag-
                    End If '-found start tag end-
                ElseIf (VB.Left(UCase(sText), 9) = "<DRAWLINE") Then
                    '-- draw line-
                    'intWidth = mlPrtWidth
                    'intXpos = intLeftMargin  '-default-
                    ''-- get attributes !! --
                    'iPos2 = InStr(10, sText, "/>")  '--end of txt tag stuff.
                    'If (iPos2 > 0) Then
                    '    sFldAttributes = Trim(Mid(sText, 10, (iPos2 - 10) + 1))
                    '    If mbLoadAttributes(sFldAttributes, colAttributes) Then
                    '        If mbHasAttribute(colAttributes, "TAB", sValue) Then
                    '            If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                    '                intXpos = CInt(sValue) + intLeftMargin
                    '                intWidth = mlPrtWidth - CInt(sValue)
                    '            End If  '-numeric-
                    '        End If  '-tab-
                    '        If mbHasAttribute(colAttributes, "WIDTH", sValue) Then
                    '            If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                    '                intWidth = CInt(sValue)
                    '            End If  '-numeric-
                    '        End If  '-width-
                    '    End If '-load-
                    'End If  '-pos-
                    'Call mIntDrawLine(ev, intXpos, intYpos, intWidth)
                    ''ev.Graphics.DrawLine(New Pen(Color.Gray, 1), _
                    ''                         intXpos, intYpos + 3, mlPrtWidth, intYpos + 3)
                ElseIf (VB.Left(UCase(sText), 12) = "<MINPAGEROOM") Then
                    '=3519.0115=
                    '-- "n" lines to be avaliable..
                    'intLinesNeeded = 0
                    'iPos2 = InStr(10, sText, "/>")  '--end of txt tag stuff.
                    'If (iPos2 > 0) Then
                    '    sFldAttributes = Trim(Mid(sText, 13, (iPos2 - 13) + 1))
                    '    If mbLoadAttributes(sFldAttributes, colAttributes) Then
                    '        If mbHasAttribute(colAttributes, "LINES", sValue) Then
                    '            If IsNumeric(sValue) AndAlso (CInt(sValue) < 100) Then
                    '                intLinesNeeded = CInt(sValue)
                    '                '-(intLineCount < intMaxLines)-
                    '                If (intLinesNeeded > (intMaxLines - intLineCount)) Then
                    '                    '-- not enough room left.
                    '                    intLineCount = intMaxLines  '-force new page.
                    '                End If
                    '            End If  '-numeric-
                    '        End If  '-has-
                    '    End If  '-load
                    'End If  '- pos2.
                    'mIntUserItemsPrinted += 1
                    'Continue While
                ElseIf (VB.Left(UCase(sText), 21) = "<VERTICALGAP_HALFLINE") Then
                    'intYpos += intItemLineHeight \ 2 '= 3
                    'mIntUserItemsPrinted += 1
                    ''-- count a line every second half-space..
                    'If bHalfLineCounted Then
                    '    bHalfLineCounted = False
                    'Else
                    '    intLineCount += 1
                    '    bHalfLineCounted = True   '--flip-  
                    'End If
                    'Continue While
                End If  '-textline-etc.
                mIntUserItemsPrinted += 1
                Application.DoEvents()

            End While  '-mIntUserItemsPrinted-
            Call mWaitFormOff()
        Catch ex As Exception
            Call mWaitFormOff()
            MsgBox("Error in PrintReport Content Try." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            '= ev.HasMorePages = False
            Exit Function
        End Try
        Cursor.Current = Cursors.Default


    End Function  '-DecodeStandardReport-
    '= = = = = = === = = = = = = = = = =



End Class  '-clsReportToGrid-
'= = = = = = = = == = = = = =
