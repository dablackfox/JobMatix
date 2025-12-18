Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Windows.Forms

Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class clsJmxTabControl
    Inherits TabControl

    '- grh 18-April-2019--
    '-- For Access to TabControl unused header space..
    '--  http://www.vbforums.com/showthread.php?615814-Tab-control-Backcolor-Change  

    '- grh 18-October-2019--
    '--  Add property and code to use Close-icon instead of drawing the "X"
    '-----
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Paint background unused tab space for Main TabControl..
    '== = = = = = = =


    Public Sub New()
        MyBase.New()
        Me.DrawMode = TabDrawMode.OwnerDrawFixed
        Me.BackColor = Color.PowderBlue  '-temp-

        Me.ItemSize = New Size(130, 32)  '--height, wieth.

        Me.SizeMode = TabSizeMode.Fixed
    End Sub  '-new-
    '= = = = = = = = =

    Const TAB_MARGIN As Integer = 2 '= 3  ' --check value ???-

    Private _BackColor As Color
    Private m_Xwid As Integer = 16  '= 10  '-- width of X. ??-

    '- grh 18-October-2019--
    Private _CloseIcon As Image

    Private _intMouseX As Integer = -1
    Private _intMouseY As Integer = -1

    Private _HotTabIndex As Integer = -1
    Private _MouseIndex As Integer = -1
    Private _onCloseButton As Boolean = False

    '= = = = = = = = = = = =  =
    '-===FF->

    '- grh 18-October-2019--
    '[Description("The Close image that will be displayed on RHS of Tab Item.")]

    Public Property CloseIcon As Image
        Get
            CloseIcon = _CloseIcon
        End Get
        Set(value As Image)
            _CloseIcon = value
        End Set
    End Property '-close icon.-
    '= = = = = = = = = = = = = = ==  = =


    Public Overrides Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value
        End Set
    End Property  '-BackColor-
    '= = = = = = = = = = =
    '= = = = = = = = = = == =
    '-===FF->

    ''' Helper method for drawing close buttons and responding to mouse activity
    ''' 
    ''' WITH HELP from .Paul..
    ''' 
    '- https://code.msdn.microsoft.com/extended-Tabcontrol-912111ff 

    Private Function GetCloseButtonRect(x As Integer) As Rectangle
        Dim r As Rectangle = MyBase.GetTabRect(x)
        Dim closeRect As New Rectangle(r.Width - 18, If(x <> MyBase.SelectedIndex, 6, 4), 16, 16)
        Return closeRect
    End Function '-GetCloseButtonRect-
    '= = = = = = = = = = = = = == 

    ''' <summary>
    ''' Used to capture 'mouse on close button'
    ''' 
    Private Function PointerOnCloseButton(x As Integer) As Boolean
        If (x > -1) Then
            Dim closeRect As Rectangle = GetCloseButtonRect(x)
            closeRect.X += MyBase.GetTabRect(x).Left
            Dim pt As Point = MyBase.PointToClient(Cursor.Position)
            If closeRect.Contains(pt) Then
                Return True
            End If
        End If
        Return False
    End Function
    '= = = = = == =  = =

    ''' Method used for identifying which tab or button the mouse is over if any

    ''' WITH HELP from .Paul..
    ''' 
    '- https://code.msdn.microsoft.com/extended-Tabcontrol-912111ff 

    Private Function getTabIndex(ByVal p As Point) As Integer
        For x As Integer = 0 To MyBase.TabPages.Count - 1
            If MyBase.GetTabRect(x).Contains(p) Then
                Return x
            End If
        Next
        'Dim gp As New Drawing2D.GraphicsPath
        'gp.AddPolygon(getPolygon())
        'If gp.IsVisible(p) Then
        '    Return MyBase.TabPages.Count
        'End If
        Return -1
    End Function  '-getTabIndex-
    '= = = = = = = = = = = = =

    ''' Overridden OnMouseMove event
    ''' Passes MouseEventArgs to me_mousemove which handles TabControl and parent (Form) mousemove events
    ''' WITH HELP from .Paul..
    ''' 
    '- https://code.msdn.microsoft.com/extended-Tabcontrol-912111ff 

    '-- To capture mouse Hover over close Icon.

    Protected Overrides Sub OnMouseMove(ByVal ev As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(ev)
        '== me_mousemove(Me, e)
        Dim mousePos As Point = Me.MousePosition
        Dim bIsOverCross As Boolean = False

        If MyBase.Bounds.Contains(mousePos) Then '=If MyBase.Bounds.Contains(ev.Location) Then
            _HotTabIndex = getTabIndex(ev.Location)
            If (_HotTabIndex > -1) And (_HotTabIndex < MyBase.TabPages.Count) Then
                '=_onCloseButton = PointerOnCloseButton(_HotTabIndex)
                bIsOverCross = PointerOnCloseButton(_HotTabIndex)
            Else
                '= _onCloseButton = False
            End If
            If (bIsOverCross <> _onCloseButton) Then
                '-- changed-
                _onCloseButton = bIsOverCross
                '-- only invalidate if moved on or off the close Cross.
                Me.Invalidate()
            End If
        End If
    End Sub '--mouse move..
    '= = = = = = = = = = == =
    '-===FF->

    '-- This with help ALSO from-
    '' http://www.vb-helper.com/howto_net_ownerdraw_tab.html 


    Protected Overrides Sub OnDrawItem(ByVal ev As System.Windows.Forms.DrawItemEventArgs)
        MyBase.OnDrawItem(ev)

        Dim txt_brush As Brush
        Dim box_brush As Brush
        Dim box_pen As Pen

        ' Background
        Using bgBrush As New SolidBrush(Me.BackColor)

            '-- Can't do this because it overpaints all the other tabs.
            '-- Can't do this because it overpaints all the other tabs.
            '-- Can't do this because it overpaints all the other tabs.
            '== ev.Graphics.FillRectangle(bgBrush, Me.ClientRectangle)
        End Using

        ' Tab header
        'Dim tab As TabPage = Me.TabPages(ev.Index)
        'Using tabBrush As New SolidBrush(tab.BackColor)
        '    ev.Graphics.FillRectangle(tabBrush, ev.Bounds)
        'End Using
        'Using textBrush As New SolidBrush(tab.ForeColor)
        '    ev.Graphics.DrawString(tab.Text, tab.Font, textBrush, ev.Bounds.Location.X + 5, ev.Bounds.Location.Y + 5)
        'End Using

        '- JobMatix tabs.
        Dim g As Graphics = ev.Graphics
        Dim tp As TabPage = Me.TabPages(ev.Index)
        Dim tabBrush, boxBrush, hoverBrush As Brush
        Dim sf As New StringFormat
        Dim rectText As New RectangleF(ev.Bounds.X, ev.Bounds.Y + 3, _
                                            ev.Bounds.Width - m_Xwid, ev.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Center
        Dim strTitle As String '= = tp.Text
        Dim tab_rect As Rectangle = Me.GetTabRect(ev.Index)


        '-- RE-DRAW all Tab header, as we just blitzed the whole client area..
        '-  NO !!!  tha't doesn't work !!
        '= For Each tp As TabPage In Me.TabPages
        '-  If the current index is the Selected Index, change the color
        strTitle = tp.Text
        If (Me.SelectedIndex = ev.Index) Then
            '- this is the background color of the tabpage
            '- you could make this a stndard color for the selected page
            '= br = New SolidBrush(tp.BackColor)
            '=Using 
            '- POS-4.2-- Differentcolour for different form tyoe.
            If (InStr(LCase(strTitle), "stock") > 0) Then  '-stock admin or stocktake..
                tabBrush = New SolidBrush(Color.NavajoWhite)
            ElseIf (InStr(LCase(strTitle), "customer") > 0) Or (InStr(LCase(strTitle), "statement") > 0) Then  '-cust..
                tabBrush = New SolidBrush(Color.LightBlue)  '-lightblue.
            ElseIf (InStr(LCase(strTitle), "payment") > 0) Then  '-255, 192, 255..
                tabBrush = New SolidBrush(ColorTranslator.FromOle(RGB(255, 192, 255)))  '-light  purple.
            ElseIf (InStr(LCase(strTitle), "goods") > 0) Or (InStr(LCase(strTitle), "purchase") > 0) Then  '-cust..
                tabBrush = New SolidBrush(ColorTranslator.FromOle(RGB(255, 170, 60)))  '-brown 255, 178, 60.
            ElseIf (InStr(LCase(strTitle), "subscription") > 0) Then  '-sub..
                tabBrush = New SolidBrush(ColorTranslator.FromOle(RGB(220, 159, 158)))  '-pinky-brown 220, 159, 158.
            ElseIf (InStr(LCase(strTitle), "layby") > 0) Then  '-..
                tabBrush = New SolidBrush(Color.Thistle)  '-.
            Else
                tabBrush = New SolidBrush(ColorTranslator.FromOle(RGB(255, 255, 170)))  '-light yellow.
            End If
            g.FillRectangle(tabBrush, tab_rect)
            boxBrush = tabBrush
            '=End Using
            Using textBrush As New SolidBrush(tp.ForeColor)
                g.DrawString(strTitle, tp.Font, textBrush, rectText, sf)
            End Using
        Else '-not selected-
            '= these are the standard colors for the unselected tab pages
            '=Using 
            tabBrush = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(tabBrush, tab_rect)
            boxBrush = tabBrush
            '=End Using--
            Using textBrush As New SolidBrush(Color.Black)
                g.DrawString(strTitle, tp.Font, textBrush, rectText, sf)
            End Using
        End If '-selected-

        '-- To draw an X..
        ' Allow room for margins.
        Dim layout_rect As New RectangleF( _
            tab_rect.Left + TAB_MARGIN, _
            tab_rect.Y + TAB_MARGIN, _
            tab_rect.Width - 2 * TAB_MARGIN, _
            tab_rect.Height - 2 * TAB_MARGIN)

        '-- (NO) Draw the tab # in the upper left corner.

        '- Draw an X in the upper right corner.
        Dim rect As Rectangle = Me.GetTabRect(ev.Index)
        box_pen = Pens.DarkBlue

        ev.Graphics.FillRectangle(tabBrush, _
            layout_rect.Right - m_Xwid, _
            layout_rect.Top, _
            m_Xwid, _
            m_Xwid)

        '- grh 18-October-2019--
        hoverBrush = New SolidBrush(Color.Gainsboro)
        '--  If mouse is over a cross, then show hover BG.
        If (ev.Index = _HotTabIndex) And (ev.Index > 0) Then  '--BUT can't close first tab.
            '-- we are painting the Tab where the mouse is..
            If _onCloseButton Then
                ev.Graphics.FillRectangle(hoverBrush, _
                     layout_rect.Right - m_Xwid - 1, _
                     layout_rect.Top - 1, _
                     m_Xwid + 1, _
                     m_Xwid + 1)
            End If
        End If  '-hotindex.

        '- grh 18-October-2019--
        '--  if no icon, draw the cross in a box..
        '-- Icon cross need to be transparent.
        If (_CloseIcon IsNot Nothing) Then
            If (ev.Index > 0) Then  '- can't close first tab.
                ev.Graphics.DrawImage(_CloseIcon, _
                                New Rectangle((CInt(layout_rect.Right) - _CloseIcon.Width - 2), CInt(layout_rect.Top) + 1, _
                                                           _CloseIcon.Width, _CloseIcon.Height))
            End If  '-index-
        Else
            '- no icon-
            ev.Graphics.DrawRectangle(box_pen, _
                layout_rect.Right - m_Xwid, _
                layout_rect.Top, _
                m_Xwid, _
                m_Xwid)
            ev.Graphics.DrawLine(box_pen, _
                layout_rect.Right - m_Xwid, _
                layout_rect.Top, _
                layout_rect.Right, _
                layout_rect.Top + m_Xwid)
            ev.Graphics.DrawLine(box_pen, _
                layout_rect.Right - m_Xwid, _
                layout_rect.Top + m_Xwid, _
                layout_rect.Right, _
                layout_rect.Top)
        End If  '-nothing.

        '== 08-Nov-2019=
        '--  Paint the unused tab BG..
        '=  https://stackoverflow.com/questions/11822748/how-to-change-the-background-color-of-unused-space-tab-in-c-sharp-winforms  

        Using backgroundBrush As New SolidBrush(ColorTranslator.FromOle(RGB(218, 228, 241)))  '-light blue.
            Dim lasttabrect As Rectangle = Me.GetTabRect(Me.TabPages.Count - 1)
            Dim background As Rectangle = New Rectangle()

            background.Location = New Point(lasttabrect.Right, 0)
            '== //pad the rectangle to cover the 1 pixel line between the top of the tabpage and the start of the tabs
            background.Size = New Size(Me.Right - background.Left, lasttabrect.Height + 1)
            ev.Graphics.FillRectangle(backgroundBrush, background)
        End Using
        '== DONE painting BG..

        ' Clean up. (Don't Dispose the stock pens and brushes.)
        sf.Dispose()

        '= Next  '-tp=
    End Sub  '-OnDrawItem-
    '= = = = = = = = = = = ==
    '= = = = = = = = = = == =
    '-===FF->

    '-IsMouseOverCloseX-
    '- intTabIndex is the index of the Tab Page
    '--     where the mouse-down is occurring..

    Public Function IsMouseOverCloseX(intTabIndex As Integer, _
                                      ByVal ev As System.Windows.Forms.MouseEventArgs) As Boolean
        IsMouseOverCloseX = False

        '-- Get the TabRect plus room for margins.
        Dim tab_rect As Rectangle = Me.GetTabRect(intTabIndex)
        Dim rect As New RectangleF( _
            tab_rect.Left + TAB_MARGIN, _
            tab_rect.Y + TAB_MARGIN, _
            tab_rect.Width - 2 * TAB_MARGIN, _
            tab_rect.Height - 2 * TAB_MARGIN)

        If ev.X >= rect.Right - m_Xwid AndAlso _
           ev.X <= rect.Right AndAlso _
           ev.Y >= rect.Top AndAlso _
           ev.Y <= rect.Top + m_Xwid _
        Then
            '= Debug.WriteLine("Tab " & intTabIndex)
            '= Me.TabPages.RemoveAt(intTabIndex)
            IsMouseOverCloseX = True
            Exit Function
        End If

    End Function  '-IsMouseOverCloseX-
    '= = = = = = = = = = = = = = == = 

End Class  '-clsJmxTabControl-
'= = = = = =  = = = = = = = =
