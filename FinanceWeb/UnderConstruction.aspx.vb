Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports FinanceWeb.WebFinGlobal

Public Class UnderConstruction
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If User.Identity.IsAuthenticated Then

            If GetScalar("select count(*) from WebFD.dbo.aspnet_Users u " & _
              "join WebFD.dbo.aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
              "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
              "where u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and RoleName = 'VendorContracts' ") > 0 Then
                lblNewsB.Text = "Click <a href='https://financeweb.northside.local/Tools/VendorContracts/LESCOR.aspx'>here for LESCOR</a>."

            Else
                tblLescor.Visible = False

            End If


            'End If
        Else
            lblNewsB.Text = "For Access to BIDS Supported Tools and Analytics including LESCOR, you will need to Login in the top right of this page using your IS Security Login. "
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

    Private Shared Function GetString(query As String) As String

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As String

                dt = cmd.ExecuteScalar

                Return dt

            End Using

        End Using


    End Function

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Response.Redirect(url:="https://FinanceWeb.northside.local/")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class