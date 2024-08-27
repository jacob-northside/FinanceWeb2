Imports FinanceWeb.WebFinGlobal

Public Class Applications
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'This gets the URL so that we can look at the end of the string to find out what should be displayed on the page. 
            'If there is not condition at the end of the string or if it is invalid then all the divs will be shown. 
            Dim URLtemp As String
            URLtemp = Request.Url.AbsoluteUri

            'Programically setting the divs as not visible. 
            divAXIOM.Visible = False
            divGDDS.Visible = False
            divHBI.Visible = False
            divHEFM.Visible = False
            divHEMM.Visible = False
            divSTAR.Visible = False
            divTSTAR.Visible = False

            'If any of the key words appear in the URL string then that div will be shown and then routine will stop.
            If URLtemp.Contains("AXIOM") Then
                divAXIOM.Visible = True
                Exit Sub
            End If

            If URLtemp.Contains("GDDS") Then
                divGDDS.Visible = True
                Exit Sub
            End If

            If URLtemp.Contains("HBI") Then
                divHBI.Visible = True
                Exit Sub
            End If

            'HEFM has a four different paths to get here so it also has a switcing routine.
            If URLtemp.Contains("HEFM") Then
                divHEFM.Visible = True

                liGL.Visible = False
                liAP.Visible = False
                liFA.Visible = False
                liProjects.Visible = False

                If URLtemp.Contains("ID2=") Then
                    If URLtemp.Contains("GL") Then
                        liGL.Visible = True
                    End If
                    If URLtemp.Contains("AP") Then
                        liAP.Visible = True
                    End If
                    If URLtemp.Contains("FA") Then
                        liFA.Visible = True
                    End If
                    If URLtemp.Contains("Projects") Then
                        liProjects.Visible = True
                    End If
                Else
                    liGL.Visible = True
                    liAP.Visible = True
                    liFA.Visible = True
                    liProjects.Visible = True
                End If
                Exit Sub
            End If

            If URLtemp.Contains("HEMM") Then
                divHEMM.Visible = True
                Exit Sub
            End If

            'TSTAR AND STAR are out of order (alphabetically) here because STAR would appear in TSTAR and it would appear but not TSTAR. 
            If URLtemp.Contains("TSTAR") Then
                divTSTAR.Visible = True
                Exit Sub
            End If

            If URLtemp.Contains("STAR") Then
                divSTAR.Visible = True
                Exit Sub
            End If

            'If the URL does not contain any of the div names then all the divs will be visible.
            divAXIOM.Visible = True
            divGDDS.Visible = True
            divHBI.Visible = True
            divHEFM.Visible = True
            divHEMM.Visible = True
            divSTAR.Visible = True
            divTSTAR.Visible = True



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

End Class