Public Class MediaCenter
    Inherits System.Web.UI.Page
    Public Shared FileName As String
    Public Shared Remembertime As Double
    Public Shared BoolFullScreen As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Exit Sub

        End If
        FileName = ""
        BoolFullScreen = False
    End Sub

    'Private Sub BtnPlay_Click(sender As Object, e As System.EventArgs) Handles BtnPlay.Click
    '    If BtnPause.Enabled = False Then
    '        StartStop = Remembertime
    '        FileName = ddlSelectedVideo.SelectedValue
    '        BtnPause.Enabled = True
    '        Exit Sub
    '    End If
    '    StartStop = 0
    '    FileName = ddlSelectedVideo.SelectedValue
    'End Sub



    Private Sub BtnFullScreen_Click(sender As Object, e As System.EventArgs) Handles BtnFullScreen.Click
        BoolFullScreen = True
    End Sub

    Private Sub ddlSelectedVideo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedVideo.SelectedIndexChanged
        BoolFullScreen = False
        FileName = ddlSelectedVideo.SelectedValue
    End Sub
End Class