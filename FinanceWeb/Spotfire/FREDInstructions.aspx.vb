
Imports FinanceWeb.WebFinGlobal
Public Class FREDInstructions
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If IsPostBack Then

        Else
            If Not Request.Item("t") Is Nothing Then
                If IsNumeric(Request.Item("t")) Then
                    FREDTabs.ActiveTabIndex = CInt(Request.Item("t"))
                End If
            Else
                FREDTabs.ActiveTabIndex = 0
            End If
        End If
        'Try




        'Catch ex As Exception
        '    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        'End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private Sub lbCustomExpressionsTD_Click(sender As Object, e As EventArgs) Handles lbCustomExpressionsTD.Click
        tcCustomExpressionsTD.Visible = True
        tcSpotfireHelpCustomExpressions.Visible = False
    End Sub

    Private Sub lbSpotfireHelpCust_Click(sender As Object, e As EventArgs) Handles lbSpotfireHelpCust.Click
        tcCustomExpressionsTD.Visible = False
        tcSpotfireHelpCustomExpressions.Visible = True
    End Sub
End Class