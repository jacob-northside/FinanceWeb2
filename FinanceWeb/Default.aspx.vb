Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim r As New Random()
        Dim n As Integer = r.Next(2)

        If n Mod 2 = 0 Then
            pnlBack.CssClass = "Background3"
            tblFront.CssClass = "OpacityBox3"
        End If
        lblNewsA.Text = "<b>Legal Services Contract Review System (LESCOR) </b><br>"
        If User.Identity.IsAuthenticated Then

            If GetScalar("select count(*) from WebFD.dbo.aspnet_Users u " & _
              "join WebFD.dbo.aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
              "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
              "where u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and RoleName = 'VendorContracts' ") > 0 Then
                lblNewsB.Text = "<ul style=""list-style-type:circle""><li>Click <a href='https://financeweb.northside.local/Tools/VendorContracts/LESCOR.aspx'>here for LESCOR</a>.</li></ul>"

            Else
                lblNewsB.Text = "<ul style=""list-style-type:circle""> <li>You do not currently have access to the LESCOR tool; if you believe this is in error, please contact " & _
                    GetString("select Value from WebFD.VendorContracts.SpecialValues where Reason = 'Hierarchy Email' and Active = 1") & ".</li></ul>"

            End If


            'End If
        Else
            lblNewsB.Text = "<ul style=""list-style-type:circle""><li>For Access to BIDS Supported Tools and Analytics including LESCOR, you will need to Login in the top right of this page using your IS Security Login. </li></ul>"
        End If

        lblNewsC.Text = "<ul style=""list-style-type:circle""><li>To access your BIDS Supported Tools and Analytics, please select from the Tools dropdown above.<br></li></ul>" &
            "<ul style=""list-style-type:circle""><li>If you do not currently have access to a required web tool, " &
                    "please submit your request <a href='https://bids.northside.local/Data%20Entry/AccessRequest/AccessRequestForm'>here</a>.</li></ul> "

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
End Class