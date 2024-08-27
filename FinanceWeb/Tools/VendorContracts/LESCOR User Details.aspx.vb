
Imports System.DirectoryServices
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class LESCOR_User_Details
    Inherits System.Web.UI.Page
    'Private Shared superadmin As Integer = 0
    'Private Shared admin As Integer = 0
    'Private Shared Cont As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If GetScalar("SELECT count(*) FROM [WebFD].[VendorContracts_Testing].[Users] where UserLogin = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and InactivatedDate is null and (Admin = 1)") > 0 Then
                EmptyPanel.Visible = False
            Else
                lblEmptyExplanation.Text = "You do not have access to view a User's permission details, or you are not properly logged in."
                EmptyPanel.Visible = True
                Exit Sub
            End If

            If Request.QueryString("SearchLogin") IsNot Nothing Then
                lblUserLogin.Text = Request.QueryString("SearchLogin")
            Else
                EmptyPanel.Visible = True
                lblEmptyExplanation.Text = "No UserLogin has been selected."
                Exit Sub
            End If

            UserSearch()

        End If

    End Sub

    Private Sub RefreshDDLGrant(n As Integer)
        Dim x As String = ""
        If n > 1 Then
            x += "select '-1' as DepartmentID, 'Select Cost Center' as DepartmentName, -1 as DepartmentNo, 0 as ord union "
        End If

        x += "select '0' as DepartmentID, 'All Departments' as DepartmentName, 0 as DepartmentNo, 1 as ord union select DepartmentID, " & _
                                        " convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) as DepartmentName, " & _
                                        " d.DepartmentNo, 2 from WebFD.VendorContracts.Department_LU d where Active = 1 order by ord, DepartmentNo, DepartmentName"
        ddlGrantDepartment.DataSource = GetData(x)
        ddlGrantDepartment.DataTextField = "DepartmentName"
        ddlGrantDepartment.DataValueField = "DepartmentID"
        ddlGrantDepartment.DataBind()
    End Sub
    Private Sub UserSearch()

        Dim x As String = GetString("select isnull(UserDisplayName, UserFullName) as UserName from WebFD.VendorContracts_Testing.Users where InactivatedDate is null and UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & "'")
        If IsDBNull(x) Then
            lblEmptyExplanation.Text = "Could not find an active User with the specified UserLogin."
            EmptyPanel.Visible = True
            Exit Sub
        Else
            lblUserName.Text = x
            FullPanel.Visible = True
            EmptyPanel.Visible = False
            ddlGrantPosition.DataSource = GetData("select 'Select Position' as RoleFull, '-1' as RoleID, 0 as ord union select RoleFull, RoleID, 1 from WebFD.VendorContracts_Testing.Role_LU where InactivatedDate is null and RoleID <> 10 order by ord, RoleFull")
            ddlGrantPosition.DataTextField = "RoleFull"
            ddlGrantPosition.DataValueField = "RoleID"
            ddlGrantPosition.DataBind()

            RefreshDDLGrant(2)

        End If

        Dim sql As String = "select isnull(UserDisplayName, UserFullName) as UserName, d2u.RoleID, r.RoleFull, isnull(convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName), 'All Departments') as DepartmentName" & _
            ", d.DepartmentNo, d.DepartmentID as DepartmentIDDisplay, isnull(d.DepartmentID, -1) as DepartmentID, 'Revoke Access' as Clicky, convert(varchar, isnull(d.DepartmentID, -1)) + '|~CRW~|' + convert(varchar, r.RoleID) as Flipper " & _
            ", case when EmailFlag = 0 then 'No' else 'Yes' end as Emails " & _
            "from WebFD.VendorContracts_Testing.Users  u " & _
            "join WebFD.VendorContracts_Testing.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.InactivatedDate is null " & _
            "left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            "left join WebFD.VendorContracts_Testing.Role_LU r on d2u.RoleID = r.RoleID and r.InactivatedDate is null " & _
            "where u.UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & "' and u.InactivatedDate is null and (d2u.DepartmentID is null or d.DepartmentID is not null)"

        Dim ds As DataView = GetData(sql).DefaultView

        'If ds.Count > 0 Then

        'lblUserName.Text = ds(0)(0).ToString
        gvDepartments.DataSource = ds
        gvDepartments.DataBind()


        'Else

        'End If


    End Sub



    Private Shared Function GetData(query As String) As DataTable

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandTimeout = 300
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

                cmd.CommandTimeout = 300
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

                cmd.CommandTimeout = 300
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

                cmd.CommandTimeout = 300
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



    Private Sub btnGrantAccess_Click(sender As Object, e As EventArgs) Handles btnGrantAccess.Click

        If ddlGrantPosition.SelectedIndex < 1 Then
            mpeExplanationLabel.Text = "Please select a position."
            OkayButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            mpeDetails.Show()
            Exit Sub
        End If

        If ddlGrantDepartment.SelectedValue = 1 Then
            mpeExplanationLabel.Text = "Please select a Department."
            OkayButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            mpeDetails.Show()
            Exit Sub
        End If

        If ddlGrantDepartment.SelectedIndex > 1 Then
            Dim s As String = GetString("select RoleID from WebFD.VendorContracts_Testing.Department_2_User where InactivatedDate is null and UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & _
                      "' and DepartmentID = '" & Replace(ddlGrantDepartment.SelectedValue, "'", "''") & "'")

            If IsDBNull(s) Then
            ElseIf s = Nothing Then
            ElseIf s = ddlGrantPosition.SelectedValue Then
                mpeExplanationLabel.Text = "This user already has these permissions"
                OkayButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                mpeDetails.Show()
                Exit Sub
            Else
                mpeExplanationLabel.Text = "This user already has " & s & " permissions as the specified Cost Center; do you wish to overwrite this?"
                mpeDetails.Show()
                OkayButton.Visible = False
                ConfirmButton.Visible = True
                CancelButton.Visible = True
                Exit Sub
            End If

        End If

        AddPermission()
        UserSearch()
        SynchWebFDPermissions()

    End Sub

    Private Sub AddPermission()
        Dim x As String = "Insert into WebFD.VendorContracts_Testing.Department_2_User values ('" & Replace(lblUserLogin.Text, "'", "''") & "',"

        If ddlGrantDepartment.SelectedIndex < 2 Then
            x += " null, "
        Else
            x += "'" & Replace(ddlGrantDepartment.SelectedValue, "'", "''") & "', "
        End If

        If ddlGrantPosition.SelectedIndex < 1 Then
            mpeExplanationLabel.Text = "Please select a position."
            OkayButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            mpeDetails.Show()
            Exit Sub
        Else
            x += "'" & Replace(ddlGrantPosition.SelectedValue, "'", "''") & "', "
        End If

        x += " 1, null, null, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', null, null)"

        ExecuteSql(x)
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click
        ExecuteSql("Update u set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'" & _
      "from WebFD.vendorContracts_Testing.Department_2_User u where InactivatedDate is null and UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & "' " & _
                      "' and DepartmentID = '" & Replace(ddlGrantDepartment.SelectedValue, "'", "''") & "'")
        AddPermission()
        UserSearch()
        SynchWebFDPermissions()
    End Sub

    Private Sub gvDepartments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDepartments.RowCommand

        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            If Commander = "RemoveAccess" Then

                Dim PrepSQL As String = "Update u set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'" & _
      "from WebFD.vendorContracts_Testing.Department_2_User u where InactivatedDate is null and UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & "' " & _
                      " and convert(varchar, isnull(DepartmentID, -1)) + '|~CRW~|' + convert(varchar, RoleID) = '" & Replace(Detail_ID, "'", "''") & "'"

                ExecuteSql(PrepSQL)
                UserSearch()
                SynchWebFDPermissions()
            ElseIf Commander = "Emails" Then

                Dim PrepSQL As String = "Insert into WebFD.vendorContracts_Testing.Department_2_User (UserLogin, DepartmentID, RoleID, EmailFlag, AddedDate, AddedBy ) " & _
         "select UserLogin, DepartmentID, RoleID, -EmailFlag + 1, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
         "from  WebFD.vendorContracts_Testing.Department_2_User where InactivatedDate is null and UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & "' " & _
         " and convert(varchar, isnull(DepartmentID, -1)) + '|~CRW~|' + convert(varchar, RoleID) = '" & Replace(Detail_ID, "'", "''") & "'" & _
         "Update u set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'" & _
         "from WebFD.vendorContracts_Testing.Department_2_User u where InactivatedDate is null and convert(varchar, isnull(DepartmentID, -1)) + '|~CRW~|' + convert(varchar, RoleID) = '" & _
         Replace(Detail_ID, "'", "''") & "' and exists " & _
         "(select * from  WebFD.vendorContracts_Testing.Department_2_User u2 where u2.InactivatedDate is null and u.UserLogin = u2.UserLogin and isnull(u.DepartmentID, -1) = isnull(u2.DepartmentID, -1) " & _
         " and u.RoleID = u2.RoleID and  u2.AddedDate > u.AddedDate) "


                'Dim PrepSQL As String = "with cte as ( " & _
                '    "select top 1 * from WebFD.VendorContracts.Department_2_User d " & _
                '    "where convert(varchar, isnull(d.DepartmentID, -1)) + '|~CRW~|' + Position = '" & Replace(Detail_ID, "'", "''") & "'  and UserLogin = '" & Replace(lblUserLogin.Text, "'", "''") & "'" & _
                '    "order by Active desc,  DateModified desc) " & _
                '    "update cte set EmailFlag = -isnull(EmailFlag, 0) + 1, DateModified = getdate(), ModifiedBy =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                UserSearch()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub


    Private Sub lbSrchUsr_Click(sender As Object, e As EventArgs) Handles lbSrchUsr.Click
        pnlSrchUser.Visible = True
    End Sub

    Private Sub lbCloseUsrSrch_Click(sender As Object, e As EventArgs) Handles lbCloseUsrSrch.Click
        txtAdminUsrSrch.Text = ""
        lblAdminUsrResults.Text = ""
        pnlSrchUser.Visible = False
    End Sub
    Private Sub btnAdminUsrSrch_Click(sender As Object, e As EventArgs) Handles btnAdminUsrSrch.Click
        Try
            lblAdminUsrResults.Text = ""
            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            osearcher.Filter = "(&(cn=*" & txtAdminUsrSrch.Text & "*))" ' search filter

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

    Private Sub btnSearchNewUser_Click(sender As Object, e As EventArgs) Handles btnSearchNewUser.Click
        lblUserLogin.Text = txtnewUser.Text
        txtnewUser.Text = ""
        UserSearch()
        ddlGrantDepartment.SelectedIndex = -1
        ddlGrantPosition.SelectedIndex = -1
    End Sub

    Private Sub SynchWebFDPermissions()

        Dim SQL As String = "insert into WebFD.dbo.aspnet_Users " & _
          "select '5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', dar.UserLogin), UserLogin, UserLogin, null, 0, getdate() " & _
          "from WebFD.vendorContracts_Testing.Users dar " & _
          "where dar.InactivatedDate is null and WebToolAccess = 1 and not exists (select * from WebFD.dbo.aspnet_Users au  " & _
          "where au.UserName = dar.UserLogin) " & _
          " " & _
          "insert into WebFD.dbo.aspnet_UsersInRoles  " & _
          "select UserId, 'A257BE65-B950-8A18-ACE2-F291A684B834' from " & _
          "WebFD.vendorContracts_Testing.Users dar " & _
          "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
          "where dar.InactivatedDate is null and WebToolAccess = 1 and not exists (select * from " & _
          "WebFD.dbo.aspnet_UsersInRoles uir  " & _
          "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
          "where au.UserId = uir.UserId " & _
          "and RoleName = 'LESCOR') " & _
          " " & _
          "delete from WebFD.dbo.aspnet_UsersInRoles  " & _
          "where UserId not in ( " & _
          "select UserId from " & _
          "WebFD.vendorContracts_Testing.Users dar " & _
          "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
          "where dar.InactivatedDate is null and WebToolAccess= 1) and RoleID = 'A257BE65-B950-8A18-ACE2-F291A684B834'  " & _
      "and not exists (select UserId from WebFD.vendorContracts_Testing.Users dar " & _
      "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
      "where dar.InactivatedDate is null and WebToolAccess= 1)"

        ' Added not exists clause 7/12/2018 CRW

        ExecuteSql(SQL)

    End Sub

    Private Sub ddlGrantPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGrantPosition.SelectedIndexChanged
        If CInt(ddlGrantPosition.SelectedValue) <> 1 Then
            RefreshDDLGrant(0)
        Else
            RefreshDDLGrant(2)
        End If
    End Sub
End Class