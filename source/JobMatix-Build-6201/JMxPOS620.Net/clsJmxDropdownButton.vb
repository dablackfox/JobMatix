'-- Buttons..  SEE ALSO-
'--  https://www.codeproject.com/Articles/26622/Custom-Button-Control-with-Gradient-Colors-and-Ext


'-- The file MyButton.Designer.VB
'- https://www.experts-exchange.com/questions/23146751/Creaintg-VB-NET-Buttons-with-Mettalic-or-Gradient-Effect.html
'=======================================================================


'-- AND THIS--
'--   http://forums.codeguru.com/showthread.php?437654-rounded-button-with-gradient-color-in

'Private Sub Button1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Button1.Paint

'    Dim X1 As Integer, Y1 As Integer, Width As Single, Height As Single 'Left, Top, Width, Height
'    Dim RoundRadius As Integer, LineColor As Color 'corner radius, line color

'    Try
'        X1 = Button1.Left 'Left
'        Y1 = Button1.Top 'Top
'        Width = Button1.Width 'Width
'        Height = Button1.Height 'Height
'        RoundRadius = 15 'Radius
'        LineColor = Color.Green 'Line

'        Dim gr As System.Drawing.Graphics 'Graphics object

'        gr = Graphics.FromHwnd(ActiveForm.Handle) 'Where ¿

'        Dim Pen As New System.Drawing.Pen(LineColor) 'Drawing pen

'        'Draw rect
'        Dim myPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath
'        If RoundRadius > Width / 2 OrElse RoundRadius > Height / 2 Then RoundRadius = Height / 2

'        myPath.StartFigure()

'        myPath.AddArc(X1, Y1, RoundRadius * 2, RoundRadius * 2, 180, 90)
'        myPath.AddArc(X1 + Width - RoundRadius * 2, Y1, RoundRadius * 2, RoundRadius * 2, 270, 90)
'        myPath.AddArc(X1 + Width - RoundRadius * 2, Y1 + Height - RoundRadius * 2, RoundRadius * 2, RoundRadius * 2, 0, 90)
'        myPath.AddArc(X1, Y1 + Height - RoundRadius * 2, RoundRadius * 2, RoundRadius * 2, 90, 90)
'        myPath.CloseFigure()

'        'Button background
'        Dim brBackground As New System.Drawing.Drawing2D.LinearGradientBrush(e.ClipRectangle, System.Drawing.Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255), System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
'        gr.DrawPath(Pen, myPath)
'        e.Graphics.FillRectangle(brBackground, e.ClipRectangle)

'        Button1.Region = New Region(myPath) 'Apply
'    Catch ex As Exception
'        MessageBox.Show(ex.Message)
'    End Try
'End Sub

'=======  end intro ==
'=======  end intro ==
'=======  end intro ==



'==  grh = 04-Mar2017=
'--   "MyJmxButton" JobMatix further devel. of MyButton 
'--      to add rounded corners..

'==  grh = 07-Mar2017= OPTIONS..-
'==
'==  grh = 30-Jul-2019=
'==    NOW is clsJmxDropdownButton.-
'==    NOW is clsJmxDropdownButton.-
'==    NOW is clsJmxDropdownButton.-
'==    NOW is clsJmxDropdownButton.-
'==
''= = = = =  = = = = = = = = = = = = = = = = = = = =

Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Windows.Forms

Imports System.Drawing
Imports System.Drawing.Drawing2D

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class clsJmxDropdownButton
    Inherits System.Windows.Forms.Button

    'Control overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Control Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

End Class  '-partial-
'= = = = = = = = = = == = =

'--=====================================================================

'--The MyButton.VB
'--=====================================================================

Public Class clsJmxDropdownButton

    '= Private X1 As Integer, Y1 As Integer, sinWidth As Single, sinHeight As Single 'Left, Top, Width, Height
    Private X1 As Integer, Y1 As Integer, intWidth, intHeight As Integer 'Left, Top, Width, Height
    Private LineColor As Color
    '= Private penBorder As Pen = New Pen(_borderColor, penWidth)
    Private Shared ReadOnly penWidth As Single = 2.0F
    Private _borderColor As Color = Color.White
    Private _roundRadius As Integer = 5  '= 11 '-- corner radius..
    Private _parentBackcolor As Color '= = Me.Parent.BackColor

    Private _dnArrowImage As Image = Nothing
    '= Private _MainImage As Image = Nothing
    Private _bgImage As Image '= = Me.BackgroundImage

    '= = = = =  = = = = = = = = = = =  == = =

    Public Sub New()
        '= MyBase.New()
        Call InitializeComponent()

        '=MyBase.Size = New Size(129, 23)
        '- save original image..

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = =

    'Protected Overrides Sub OnSizeChanged(e As EventArgs)

    '    '- save original image..
    '    If (Me.BackgroundImage IsNot Nothing) AndAlso (_bgImage Is Nothing) Then
    '        _bgImage = Me.BackgroundImage
    '    End If
    '    MyBase.OnSizeChanged(e)
    'End Sub  '-on size changed.-
    '= = = = = = = = = = = === 

    Protected Overrides Sub OnPaint(ByVal ev As System.Windows.Forms.PaintEventArgs)
        Dim penBorder As Pen  '= = New Pen(_borderColor, penWidth)
        Dim s1 As String
        '=Dim bgImage As Image '= = Me.BackgroundImage

        Try  '-paint-
            If (Me.BackgroundImage IsNot Nothing) AndAlso (_bgImage Is Nothing) Then
                _bgImage = Me.BackgroundImage
            End If

            MyBase.OnPaint(ev)
            _parentBackcolor = Me.Parent.BackColor
            ' Calling the base class OnPaint
            MyBase.OnPaint(ev)
            ' Setup the Formatting for the text
            Dim formatText As StringFormat = New StringFormat(StringFormatFlags.NoClip)
            formatText.LineAlignment = StringAlignment.Center
            formatText.Alignment = StringAlignment.Center
            ' Drawing the button yourself. 
            Dim lgBrush As LinearGradientBrush
            ' Draw gradient in different state Clicked and un-clicked
            If IsClicked Then
                ' Mouse clicked state
                lgBrush = New LinearGradientBrush(New Rectangle(0, 0, Me.Width, Me.Height), _
                                                       Color.FromArgb(190, 190, 190), _
                                                        Color.White, LinearGradientMode.Vertical)
                penBorder = New Pen(_borderColor, penWidth)
            ElseIf IsHovering Then  '--dimmer control-
                'lgBrush = New LinearGradientBrush(New Rectangle(0, 0, Me.Width, Me.Height), Color.White, _
                '    Color.FromArgb(150, 150, 150), LinearGradientMode.Vertical)
                '-orange hover-
                lgBrush = New LinearGradientBrush(New Rectangle(0, 0, Me.Width, Me.Height), Color.White, _
                                                     Color.FromArgb(255, 203, 136), LinearGradientMode.Vertical)
                penBorder = New Pen(_borderColor, penWidth)
            Else
                '-- Mouse not clicked state AND NOT hovering.
                '-- Normal colour and gradient.
                'lgBrush = New LinearGradientBrush(New Rectangle(0, 0, Me.Width, Me.Height), Color.White, _
                '    Color.FromArgb(190, 190, 190), LinearGradientMode.Vertical)
                '-- grh=  Use Backcolor property.. 

                '== IN normal mode, Hide everything except IMage and Text.
                lgBrush = New LinearGradientBrush(New Rectangle(0, 0, Me.Width, Me.Height), _parentBackcolor, _
                    _parentBackcolor, LinearGradientMode.Vertical)
                '- hide border-
                penBorder = New Pen(_parentBackcolor, penWidth)
            End If
            '-- shape colour to the middle..
            lgBrush.SetSigmaBellShape(0.7F, 0.5F)
            '=lgBrush.SetSigmaBellShape(0.6F)
            ev.Graphics.FillRectangle(lgBrush, ev.ClipRectangle)

            '-- Draw the line around the button
            '-see below- ev.Graphics.DrawRectangle(New Pen(Color.Black, 1), 0, 0, Me.Width - 1, Me.Height - 1)

            '-- code Guru- Border with round corners-
            '-- code Guru- Border with round corners-
            '-- code Guru- Border with round corners-
            X1 = 0  '= ev.ClipRectangle.Left  '== Me.Left 'Left
            Y1 = 0  '= ev.ClipRectangle.Top   '=Me.Top 'Top
            intWidth = Me.Width  '--Width
            intHeight = Me.Height '--Height
            LineColor = Me.BorderColor
            '= Dim gr As System.Drawing.Graphics 'Graphics object
            '= gr = Graphics.FromHwnd(Me.FindForm.Handle) 'Where ¿
            '= Dim Pen As New System.Drawing.Pen(LineColor) 'Drawing pen
            '---Draw rect
            Dim myPath As System.Drawing.Drawing2D.GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath
            If (RoundRadius > Width / 2) OrElse (RoundRadius > Height / 2) Then
                RoundRadius = CInt(Height / 2)
            End If
            myPath.StartFigure()
            myPath.AddArc(New Rectangle(X1, Y1, RoundRadius * 2, RoundRadius * 2), 180, 90)
            myPath.AddArc(New Rectangle((X1 + intWidth - RoundRadius * 2), Y1, RoundRadius * 2, RoundRadius * 2), 270, 90)
            myPath.AddArc(New Rectangle((X1 + intWidth - RoundRadius * 2), Y1 + intHeight - RoundRadius * 2, _
                                                                                 RoundRadius * 2, RoundRadius * 2), 0, 90)
            myPath.AddArc(New Rectangle(X1, (Y1 + intHeight - RoundRadius * 2), RoundRadius * 2, RoundRadius * 2), 90, 90)
            myPath.CloseFigure()
            ev.Graphics.DrawPath(penBorder, myPath)
            '--END code Guru- Border with round corners-

            '-- Draw focus rectangle
            If HasFocus Then
                Dim focusRect As Rectangle = New Rectangle(7, 4, Me.Width - 13, Me.Height - 11)
                Dim fPen As New Pen(Color.Black, 1)
                fPen.DashStyle = DashStyle.Dot
                ev.Graphics.DrawRectangle(fPen, focusRect)
                fPen.Dispose()  '-grh-
            End If

            '-grh-- Draw Image-
            Dim intYpos As Integer = CInt(Me.Height / 2)  '-default start for test.
            Dim intXpos As Integer
            If (Me.Image IsNot Nothing) Then
                'Dim img2 As New Bitmap(MyBase.Image, MyBase.Width / 2, MyBase.Height / 2)  '-grh-
                'ev.Graphics.DrawImage(img2, _
                '                  New Rectangle(12, 12, img2.Width, img2.Height))
                '-- centre the image.
                '=Dim bgImage As Image = Me.BackgroundImage
                intXpos = CInt((MyBase.Width / 2) - (Me.Image.Width / 2))
                If intXpos < 6 Then
                    intXpos = 6
                End If
                'ev.Graphics.DrawImage(Me.MainImage, _
                '                  New Rectangle(intXpos, 12, Me.MainImage.Width + 6, Me.MainImage.Height + 6))
                ev.Graphics.DrawImage(Me.Image, intXpos, 6)
                intYpos = Me.Image.Height + 12
            End If '- nothing-

            '-- Draw the text in the button
            'ev.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(ForeColor), _
            '                         New RectangleF(0.0F, 0.0F, Me.Width, Me.Height), formatText)
            '=grh-
            '-- parse text for lines..
            Dim asLines() As String = Split(Me.Text, vbCrLf)
            Dim intLineHeight As Integer
            Dim size1 As Size

            If asLines.Length > 0 Then
                '- get line height.
                size1 = TextRenderer.MeasureText(asLines(0), Me.Font)
                intLineHeight = size1.Height
                For Each sLine As String In asLines
                    TextRenderer.DrawText(ev.Graphics, sLine, Me.Font,
                           New Point(6, intYpos), Color.DarkBlue, Me.BackColor)
                    intYpos += intLineHeight + 2
                Next sLine
            End If

            '-grh-  Draw down arrow image..
            If (_dnArrowImage IsNot Nothing) Then
                ev.Graphics.DrawImage(_dnArrowImage, _
                   New Rectangle(CInt(MyBase.Width / 2), MyBase.Height - 10, _dnArrowImage.Width, _dnArrowImage.Height))
            End If  '-nothing.-
            '= = = = = == == ==


            '-- ok..Code Guru- apply modified region.
            Me.Region = New Region(myPath) '-- Apply

        Catch ex As Exception
            MessageBox.Show("Error in Jmx button-paint." & vbCrLf & ex.Message)
        End Try  '-paint-
        penBorder.Dispose()

    End Sub  '-- on Paint-
    '= = = = = = = = = = = = = = 

    '-- grh- from shapedPanel-
    '-- grh- from shapedPanel-
    '-- grh- from shapedPanel-
    <Browsable(True)> _
    Public Property BorderColor() As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal Value As Color)
            _borderColor = Value
            '= penBorder = New Pen(_borderColor, penWidth)
            Invalidate()
        End Set
    End Property
    '= = = = = = = = =  = = = = ==  == = =  =

    '-- AND From shapedPanel and CodeGuru-
    '-- AND From shapedPanel and CodeGuru-
    <Browsable(True)> _
    Public Property RoundRadius() As Integer
        Get
            Return _roundRadius
        End Get
        Set(ByVal Value As Integer)
            _roundRadius = Value
            Invalidate()
        End Set
    End Property
    '= = = = = = = = = = = = = = = =  == = =  = =

    '    [Category("Appearance")]
    '[Description("The image that will be displayed on split part of this control.")]

    Public Property dnArrowImage As Image
        Get
            dnArrowImage = _dnArrowImage
        End Get
        Set(value As Image)
            _dnArrowImage = value
        End Set
    End Property '-SplitImage-
    '= = = = = = = = = = = = = = ==  = =

    '- Main image for top of button.

    'Public Property MainImage As Image
    '    Get
    '        MainImage = _MainImage
    '    End Get
    '    Set(value As Image)
    '        _MainImage = value
    '    End Set
    'End Property '-SplitImage-
    '= = = = = = = = = = = = = = ==  = =

    '-- Mousing.

    Private IsClicked As Boolean = False
    Private HasFocus As Boolean = False
    Private IsHovering As Boolean = False

    ' Is the button being clicked and if so set variable for paint method
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)

        IsClicked = True
        Me.Invalidate()

    End Sub

    ' If button not clicked reset the drawing variable
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)

        IsClicked = False
        Me.Invalidate()

    End Sub
    '= = = = = = = = = = = = = =

    '-- hovering-

    Protected Overrides Sub OnMouseEnter(ev As EventArgs)
        MyBase.OnMouseEnter(ev)
        IsHovering = True
        Me.Invalidate()

    End Sub '--OnMouseEnter-
    '= = = = = = = = = = = = = = 

    Protected Overrides Sub OnMouseLeave(ev As EventArgs)
        MyBase.OnMouseLeave(ev)
        IsHovering = False
        Me.Invalidate()

    End Sub '--OnMouseLeave-
    '= = = = = = = = = = = = = = 


    ' Does the control have the focus if so set vaiable for paint event to 
    ' draw focus rectangle
    Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
        MyBase.OnGotFocus(e)

        HasFocus = True
        Me.Invalidate()

    End Sub
    '= = = = = = = = = = = = =

    ' If control does not have the focus reset variable
    Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)
        MyBase.OnLostFocus(e)

        HasFocus = False
        Me.Invalidate()

    End Sub

    ' Set button focus when button is clicked.
    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        MyBase.OnClick(e)

        Me.Focus()

    End Sub

End Class '-MyJmxButton-
'= = = = = = = =  = = = = =

'-- ========= the end ==============================================================

