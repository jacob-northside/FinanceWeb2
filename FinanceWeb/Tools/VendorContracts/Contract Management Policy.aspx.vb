Public Class Contract_Management_Policy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else

            If Request.QueryString("Guide") IsNot Nothing Then
                tcPaymentTransferFAX.ActiveTabIndex = 1
            End If

        End If
    End Sub

End Class