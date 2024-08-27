Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal
Public Class ProFeeInstructions
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
            CheckUserPermission()
        End If

    End Sub


    Private Shared Function GetScalar(query As String) As Integer

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As Integer

                dt = cmd.ExecuteScalar

                Return dt

            End Using

        End Using


    End Function
    Private Sub CheckUserPermission()

        If GetScalar("select count(*) from WebFD.dbo.aspnet_Users u " & _
               "join WebFD.dbo.aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
               "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
               "where r.RoleName in ('Developer', 'ProFee') and UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'") > 0 Then
            ProFeeMainSchedules.Visible = True
            ProFeeTINSchedules.Visible = True
            GalenInstructions.Visible = True
            lblEdit1.Visible = True
            pnlEdit1.Visible = True
            TabPanel1.HeaderText = "Galen STARINSPLAN Update"
        End If

    End Sub

End Class