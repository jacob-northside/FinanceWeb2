Public Class _DefaultDev
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        If IsPostBack Then
        Else
            'Dim x As Integer = CInt(Math.Ceiling(Rnd() * 2)) + 1
            'If CInt(Math.Ceiling(Rnd() * 2)) + 1 = 2 Then

            '    pnlBack.CssClass = "Background3"
            '    tblFront.CssClass = "OpacityBox3"
            'End If
        End If


    End Sub

    'Private Sub TimerImages_Tick(sender As Object, e As EventArgs) Handles TimerImages.Tick
    '    Select Case BannerWords.Text
    '        Case "Northside Hospital - Finance Division"
    '            BannerImage1.Visible = False
    '            BannerImage2.Visible = True
    '            BannerWords.Text = "Pulling together Data"
    '        Case Is = "Pulling together Data"
    '            BannerImage2.Visible = False
    '            BannerImage3.Visible = True
    '            BannerWords.Text = "Into a Single Trusted Source"
    '        Case "Into a Single Trusted Source"
    '            BannerImage3.Visible = False
    '            BannerImage4.Visible = True
    '            BannerWords.Text = "See What is Happening"
    '        Case "See What is Happening"
    '            BannerImage4.Visible = False
    '            BannerImage5.Visible = True
    '            BannerWords.Text = "Discover Why it is Happening"
    '        Case "Discover Why it is Happening"
    '            BannerImage5.Visible = False
    '            BannerImage6.Visible = True
    '            BannerWords.Text = "Affect Tomorrow"
    '        Case "Affect Tomorrow"
    '            BannerImage6.Visible = False
    '            BannerImage1.Visible = True
    '            BannerWords.Text = "Northside Hospital - Finance Division"
    '    End Select

    'End Sub
End Class