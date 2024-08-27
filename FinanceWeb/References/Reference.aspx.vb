
Imports FinanceWeb.WebFinGlobal

Public Class Reference
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then
                pnlDataDictionaryEditor.Visible = True
                pnlDataDictionary.Visible = True
            Else
                pnlDataDictionaryEditor.Visible = False
                pnlDataDictionary.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

End Class