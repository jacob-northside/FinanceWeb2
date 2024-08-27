Imports FinanceWeb.WebFinGlobal
Imports System.Data.SqlClient
Imports System.DirectoryServices

Public Class AccessRequestForm
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then
        Else
            PopulateDeptDDL()
            CheckPermissions()

            ResetPage()


        End If
    End Sub

    Private Shared Function GetData(query As String) As DataTable

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                Using sda As New SqlDataAdapter()

                    cmd.Connection = con

                    sda.SelectCommand = cmd

                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Using ds As New DataSet()

                        Dim dt As New DataTable()

                        sda.Fill(dt)

                        Return dt

                    End Using

                End Using

            End Using

        End Using

    End Function
    Private Sub ExecuteSql(query As String)

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                Using sda As New SqlDataAdapter()

                    cmd.Connection = con

                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        End Using

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


    Private Sub CheckPermissions()
        Try
            If Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") = "" Then
                lblOpenUserLogin.Text = "You are not logged in.  Please enter your credentials at the top to proceed."
            Else
                Dim x As String = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
                
                txtReviewUserID.Text = x
                ProcessUser(x)
                lblOpenUserLogin.Text = "Welcome, " & lblReviewUserName.Text & _
                                       ", to the Finance Division Access Request Portal"

                If GetScalar("select count(*) from WebFD.dbo.aspnet_Users u " & _
                    "join WebFD.dbo.aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
                    "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
                    "where r.RoleName = 'Developer' and UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'") > 0 Then
                    Developer.Text = "1"

                End If

                If GetScalar("select count(*) from WebFD.AccessRequest.Users " & _
                        "where Active = 1 and Admin = 1 and UserLogin =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'") > 0 Then
                    Admin.Text = "1"

                End If

                'tpLegalTab.Visible = True
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub lbSrchUsr_Click(sender As Object, e As EventArgs) Handles lbSrchUsr.Click
        mpeUserSearch.Show()
    End Sub

    Private Sub btnMPECloseSearch_Click(sender As Object, e As EventArgs) Handles btnMPECloseSearch.Click
        txtAdminUsrSrch.Text = ""
        lblAdminUsrResults.Text = ""
    End Sub

    Private Sub btnAdminUsrSrch_Click(sender As Object, e As EventArgs) Handles btnAdminUsrSrch.Click
        Try
            lblAdminUsrResults.Text = ""
            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            osearcher.Filter = "(&(cn=*" & Replace(Trim(txtAdminUsrSrch.Text), "'", "''") & "*))" ' search filter

            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()
            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    lblAdminUsrResults.Text = lblAdminUsrResults.Text & "Name: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & "<br/>" & _
                      "UserLogin: " & result.GetDirectoryEntry.Properties("samaccountname").Value & "<br/>" & _
                      "Email: " & result.GetDirectoryEntry.Properties("mail").Value & "<br/><br/>"
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PopulateAccessTypeDDL()

        Dim x As String = "Select AccessTypeID, AccessTypeShortDescription from WebFD.AccessRequest.AccessType_LU where Active = 1 " & _
            " union select 0, '(Select Request Type)' order by AccessTypeID "

        ddlInitialRequestType.DataSource = GetData(x)
        ddlInitialRequestType.DataTextField = "AccessTypeShortDescription"
        ddlInitialRequestType.DataValueField = "AccessTypeID"
        ddlInitialRequestType.DataBind()

        ddlInitialRequestType.SelectedIndex = -1

    End Sub

    Private Sub AccessTypeFollowUp()

        If ddlInitialRequestType.SelectedIndex < 1 Then
            ddlInitialRequestDetailType.Visible = False
            txtInitialRequestDetail.Visible = False
            lblInitialRequest.Visible = False
        Else
            If GetScalar("Select Count(*) from  WebFD.AccessRequest.AccessDetail_LU where Active = 1  and AccessTypeID = '" & ddlInitialRequestType.SelectedValue & "'") > 0 Then

                Dim x As String = "Select AccessDetailID, AccessDetailShortDescription from WebFD.AccessRequest.AccessDetail_LU where Active = 1 " & _
                    " and AccessTypeID = '" & ddlInitialRequestType.SelectedValue & "' " & _
            " union select 0, '(Select " & ddlInitialRequestType.SelectedItem.Text & ")' order by AccessDetailID "


                ddlInitialRequestDetailType.DataSource = GetData(x)
                ddlInitialRequestDetailType.DataTextField = "AccessDetailShortDescription"
                ddlInitialRequestDetailType.DataValueField = "AccessDetailID"
                ddlInitialRequestDetailType.DataBind()

                ddlInitialRequestDetailType.Visible = True
                txtInitialRequestDetail.Visible = False
                lblInitialRequest.Visible = False
            Else
                ddlInitialRequestDetailType.Visible = False
                txtInitialRequestDetail.Visible = True
                lblInitialRequest.Visible = True
            End If
        End If


    End Sub

    Private Sub ResetPage()
        PopulateAccessTypeDDL()
        AccessTypeFollowUp()
    End Sub

    Private Sub ddlInitialRequestType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInitialRequestType.SelectedIndexChanged
        AccessTypeFollowUp()
    End Sub

    Private Sub PopulateDeptDDL()

        Dim sql2 As String = " select '(Select Department)' as CC, -1 as DepartmentNo, 0 as ord union all " & _
                           " select distinct convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) as CC, d.DepartmentNo, 1  " & _
                           "from WebFD.VendorContracts.Department_LU d " & _
                           "where d.Active = 1 order by ord, CC "

        ddlReviewUserDept.DataSource = GetData(sql2)
        ddlReviewUserDept.DataTextField = "CC"
        ddlReviewUserDept.DataValueField = "DepartmentNo"
        ddlReviewUserDept.DataBind()


    End Sub

    Private Sub ProcessUser(UserLogin As String)

   
        If GetScalar("Select count(*) from WebFD.AccessRequest.Users where UserLogin = '" & UserLogin & "' and Active = 1 ") = 0 Then

            Try
                Dim UserFullName As String = ""
                Dim UserEmail As String = ""
                Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
                Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
                Dim oresult As SearchResultCollection
                Dim result As SearchResult

                osearcher.Filter = "(&(samaccountname=" & UserLogin & "))" ' search filter

                For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                    osearcher.PropertiesToLoad.Add(elem.PropertyName)
                Next
                oresult = osearcher.FindAll()
                For Each result In oresult
                    If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                        UserFullName = Trim(Replace(result.GetDirectoryEntry.Properties("cn").Value, "'", "''"))
                    End If
                    If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                        UserEmail = Trim(Replace(result.GetDirectoryEntry.Properties("mail").Value, "'", "''"))
                    End If
                Next

                If Len(UserFullName) > 0 Then
                    ExecuteSql("Insert into WebFD.AccessRequest.Users (UserLogin, UserFullName, UserEmail, Active, DateAdded, DateModified, ModifiedBy) values ('" & UserLogin & "', '" & _
                               UserFullName & "', '" & UserEmail & "', 1, getdate(), getdate(), 'AutomaticInsert')")
                End If

            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try


        End If

        lblReviewUserName.Text = GetString("select  isnull(UserDisplayName, UserFullName) from WebFD.AccessRequest.Users where Active = 1 and UserLogin = '" & _
                                          UserLogin & "'")
         
        ddlReviewUserDept.SelectedValue = GetString("select  isnull(DefaultDept, -1) from WebFD.AccessRequest.Users where Active = 1 and UserLogin = '" & _
                                          UserLogin & "'")

        txtReviewUserEmail.Text = GetString("select  isnull(UserEmail, '') from WebFD.AccessRequest.Users where Active = 1 and UserLogin = '" & _
                                  UserLogin & "'")
        txtReviewUserPhone.Text = GetString("select  isnull(UserPhone, '') from WebFD.AccessRequest.Users where Active = 1 and UserLogin = '" & _
                                  UserLogin & "'")

    End Sub
End Class