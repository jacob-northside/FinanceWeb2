
Imports FinanceWeb.WebFinGlobal

Public Class udMDPOSDescMappingInstructions
    Inherits System.Web.UI.Page

    Private Sub udMDPOSDescMappingInstructions_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try


            If Not Request.Item("t") Is Nothing Then
                If IsNumeric(Request.Item("t")) Then
                    MDPOSMappingTabs.ActiveTabIndex = CInt(Request.Item("t"))
                End If
            Else
                MDPOSMappingTabs.ActiveTabIndex = 0
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class