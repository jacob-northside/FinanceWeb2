
Imports FinanceWeb.WebFinGlobal

Public Class StarGLBalancingSub
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try


            If IsPostBack Then
                Page.MaintainScrollPositionOnPostBack = True
                If CDate(EndDateTextBox.Text) < CDate(StartDateTextBox.Text) Then
                    EndDateTextBox.Text = StartDateTextBox.Text
                End If
            Else
                If IsDBNull(Session("FARDAPStartDate")) Or Session("FARDAPStartDate") = "" Then
                    StartDateTextBox.Text = Today.Date.AddDays(-3)
                Else
                    StartDateTextBox.Text = Session("FARDAPStartDate")
                End If
                If IsDBNull(Session("FARDAPEndDate")) Or Session("FARDAPEndDate") = "" Then
                    EndDateTextBox.Text = Today.Date
                Else
                    EndDateTextBox.Text = Session("FARDAPEndDate")
                End If
                If IsDBNull(Session("FARDAPFac")) Or Session("FARDAPFac") = "" Then
                    rblFacilityFilter.SelectedValue = "%"
                Else
                    rblFacilityFilter.SelectedValue = Session("FARDAPFac")
                End If
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub gvFARGLD_CFY_DataBound(sender As Object, e As System.EventArgs) Handles gvFARDAP.DataBound
        Try
            If gvFARDAP.Rows.Count = 0 Then
                lblMessage.Text = "No FARDAP activity for this date to the current date."
                lblMessage.Visible = True
            Else
                lblMessage.Text = ""
                lblMessage.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class