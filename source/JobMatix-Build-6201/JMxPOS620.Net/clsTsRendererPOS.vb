Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Threading


Public Class clsTsRendererPOS
    Inherits ToolStripProfessionalRenderer


    '-- Toolstrip professional rendering.
    '-- https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-creating-a-professionally-styled-toolstrip-control 


    Protected Overrides Sub OnRenderButtonBackground(ByVal ev As ToolStripItemRenderEventArgs)
        MyBase.OnRenderButtonBackground(ev)

        Dim g As Graphics = ev.Graphics
        Dim button As ToolStripButton = CType(ev.Item, ToolStripButton)
        Dim bounds As Rectangle = New Rectangle(Point.Empty, ev.Item.Size)

        Dim gradientBegin As Color = Color.FromArgb(203, 225, 252)
        Dim gradientEnd As Color = Color.FromArgb(218, 228, 241)
        Dim bluePen As New Pen(Color.FromArgb(218, 228, 241), 2)

        If (button.Pressed Or button.Checked) Then
            gradientBegin = Color.FromArgb(254, 128, 62)
            gradientEnd = Color.FromArgb(255, 223, 154)
        ElseIf (button.Selected) Then
            gradientBegin = Color.FromArgb(255, 255, 222)
            gradientEnd = Color.FromArgb(255, 203, 136)
        End If

        Using b As Brush = New LinearGradientBrush(bounds, gradientBegin, gradientEnd, _
                                                   LinearGradientMode.Vertical)
            g.FillRectangle(b, bounds)
        End Using

        '-border-
        '=ev.Graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds)
        ev.Graphics.DrawRectangle(bluePen, bounds)

        '--??-
        'g.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, _
        '                  bounds.Width - 1, bounds.Y)
        'g.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, _
        '                                    bounds.X, bounds.Height - 1)

        Dim ToolStrip As ToolStrip = button.Owner
        Dim nextItem As ToolStripButton = CType(button.Owner.GetItemAt(button.Bounds.X, _
                                                       button.Bounds.Bottom + 1), ToolStripButton)

        '=  Don't draw this vert. line between buttons. !!!
        If Not (nextItem Is Nothing) Then
            'g.DrawLine(SystemPens.ControlDarkDark,
            '    bounds.X,
            '    bounds.Height - 1,
            '    bounds.X + bounds.Width - 1,
            '    bounds.Height - 1)
        End If
    End Sub '- OnRenderButtonBackground-
    '= = = = = == = = = = = = = == 


    '= OnRenderDropDownButton--

    Protected Overrides Sub OnRenderDropDownButtonBackground(ev As ToolStripItemRenderEventArgs)
        MyBase.OnRenderDropDownButtonBackground(ev)

        Dim g As Graphics = ev.Graphics
        Dim button As ToolStripDropDownButton = CType(ev.Item, ToolStripDropDownButton)
        Dim bounds As Rectangle = New Rectangle(Point.Empty, ev.Item.Size)
        Dim bluePen As New Pen(Color.FromArgb(218, 228, 241), 2)

        Dim gradientBegin As Color = Color.FromArgb(203, 225, 252)
        '= Dim gradientEnd As Color = Color.FromArgb(125, 165, 224)
        Dim gradientEnd As Color = Color.FromArgb(218, 228, 241)

        If (button.Pressed) Then '= (button.Pressed Or button.Checked) Then
            gradientBegin = Color.FromArgb(254, 128, 62)
            gradientEnd = Color.FromArgb(255, 223, 154)
        ElseIf (button.Selected) Then
            gradientBegin = Color.FromArgb(255, 255, 222)
            gradientEnd = Color.FromArgb(255, 203, 136)
        End If

        Using b As Brush = New LinearGradientBrush(bounds, gradientBegin, gradientEnd, _
                                                                    LinearGradientMode.Vertical)
            g.FillRectangle(b, bounds)
        End Using
        '-border-
        '= ev.Graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds)
        ev.Graphics.DrawRectangle(bluePen, bounds)

        '-- ????-
        'g.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, _
        '                  bounds.Width - 1, bounds.Y)
        'g.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, _
        '                                    bounds.X, bounds.Height - 1)

        Dim ToolStrip As ToolStrip = button.Owner
        Dim nextItem As ToolStripButton = CType(button.Owner.GetItemAt(button.Bounds.X, _
                                                       button.Bounds.Bottom + 1), ToolStripButton)

        '=  Don't draw this vert. line between buttons. !!!
        If Not (nextItem Is Nothing) Then
            'g.DrawLine(SystemPens.ControlDarkDark,
            '     bounds.X,
            '     bounds.Height - 1,
            '     bounds.X + bounds.Width - 1,
            '     bounds.Height - 1)
        End If

    End Sub '-OnRenderDropDownButton-
    '= = = = = = = = == ===== = = = = =

    '- Toolstrip background..

    Protected Overrides Sub OnRenderToolStripBackground(ev As ToolStripRenderEventArgs)
        MyBase.OnRenderToolStripBackground(ev)

        Dim g As Graphics = ev.Graphics
        Dim bounds As Rectangle = New Rectangle(Point.Empty, ev.AffectedBounds.Size)

        '- same as back panel..
        Dim gradientBegin As Color = Color.FromArgb(203, 225, 252)
        '= Dim gradientEnd As Color = Color.FromArgb(230,243,255)
        Dim gradientEnd As Color = Color.FromArgb(218, 228, 241)   '-pale blue-

        Using b As Brush = New LinearGradientBrush(bounds, gradientBegin, gradientEnd, _
                                                                 LinearGradientMode.Vertical)
            g.FillRectangle(b, bounds)
        End Using

        '- draw border ??--

    End Sub '-OnRenderToolStripBackground-
    '= = = = = = = = = = = = = = == = =

    '- Toolstrip border..

    Protected Overrides Sub OnRenderToolStripBorder(ev As ToolStripRenderEventArgs)
        MyBase.OnRenderToolStripBorder(ev)

        Dim g As Graphics = ev.Graphics
        Dim bounds As Rectangle = New Rectangle(Point.Empty, ev.AffectedBounds.Size)
        Dim bluePen As New Pen(Color.FromArgb(230, 243, 255), 3)

        ev.Graphics.DrawRectangle(bluePen, bounds)

    End Sub  '-render border.
    '= = = = = = = = == = = = ==

    '-- Separator-

    '-OnRenderSeparator(ToolStripSeparatorRenderEventArgs)-

    Protected Overrides Sub OnRenderSeparator(ev As ToolStripSeparatorRenderEventArgs)
        MyBase.OnRenderSeparator(ev)

        Dim g As Graphics = ev.Graphics
        Dim bounds As Rectangle = New Rectangle(Point.Empty, ev.Item.Size)
        Dim blueColor As Color = Color.FromArgb(230, 243, 255)

        Dim blueBrush = New SolidBrush(blueColor)

        ev.Graphics.FillRectangle(blueBrush, bounds)

    End Sub  '-Separator-
    '= = = = = = = = = = = = = = =


End Class  '-clsTsRenderPOS-
'= = = = = = = = == =  = = = = = =
