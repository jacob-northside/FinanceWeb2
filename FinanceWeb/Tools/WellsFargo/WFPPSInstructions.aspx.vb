
Imports FinanceWeb.WebFinGlobal
Public Class WFPPSInstructions
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try


            If Not Request.Item("t") Is Nothing Then
                If IsNumeric(Request.Item("t")) Then
                    ProFeeToolTabs.ActiveTabIndex = CInt(Request.Item("t"))
                End If
            Else
                ProFeeToolTabs.ActiveTabIndex = 0
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else

            If User.Identity.IsAuthenticated And User.IsInRole("WFPPSAdmin") Then
                tpWFPPSAdmin.Visible = True
            End If
        End If
    End Sub

End Class