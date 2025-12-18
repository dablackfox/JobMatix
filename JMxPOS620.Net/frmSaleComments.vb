Public Class frmSaleComments

    '--
    '---  Comments Form for POS Sales --
    '==
    '==     3403.728/730- 28-30-July2017-
    '==      --Called from Main SALE Form..
    '--          Comments/Delivery to separate form..  New button to call it.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Private msComments As String = ""
    Private msDelivery As String = ""
    Private mbIsCancelled As Boolean = False

    '--sub new-
    '--sub new-
 
    Public Sub New(ByVal sComments As String, _
                      ByVal sDelivery As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        msComments = sComments
        msDelivery = sDelivery

    End Sub   '--new-
    '= = = = = = = = = = = = = = == = 

    '--results-

    '- result of comments..
    ReadOnly Property commentInfo As String
        Get
            commentInfo = Trim(txtSaleComments.Text)
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    '- result of delivery..
    ReadOnly Property deliveryInfo As String
        Get
            deliveryInfo = Trim(txtSaleDelivery.Text)
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    ReadOnly Property wasCancelled As Boolean
        Get
            wasCancelled = mbIsCancelled
        End Get
    End Property  '-cancelled--
    '= = = = = = = = = = = = = = = = = = = = = = == = = 
    '= = = = = = = = = = = = 
    '-===FF->


    Private Sub frmSaleComments_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call CenterForm(Me)
        txtSaleComments.Text = msComments

        txtSaleDelivery.Text = msDelivery

    End Sub  '-load-
    '= = = = = = = == =  =

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        mbIsCancelled = True
        Me.Hide()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Me.Hide()
    End Sub  '-save-
    '= = = = = = = = = = = = = = 

End Class  '-frmSaleComments-
'= = = = = = == = = = = = = = = 