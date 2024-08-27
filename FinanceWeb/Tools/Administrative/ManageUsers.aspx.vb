Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

Public Class ManageUsers
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim conString As String = ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            CheckPermissions()
            Try
                LoadAllRoles()
                PopulategvEDSearches(0)
                LoadCheckboxes()
            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

        End If

    End Sub
    Protected Sub CheckPermissions()

        Dim x As String = "select count(*) from WebFD.dbo.aspnet_Roles r " & _
        "join WebFD.dbo.aspnet_UsersInRoles uir on r.RoleId = uir.RoleId " & _
        "join WebFD.dbo.aspnet_Users u on uir.UserId = u.UserId " & _
        "where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

        If GetScalar(x) > 0 Then
        Else
            Response.Redirect("https://bids.northside.local/Reference/UserLogin%20Search%20Tool")
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
    Sub LoadCheckboxes()
        Dim sql As String = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_SCHEMA = 'dbo' and TABLE_NAME = 'email_Distribution'" & _
           "and CHARACTER_MAXIMUM_LENGTH between 0 and 5 order by 1"

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            cmd = New SqlCommand(sql, conn)
            da = New SqlDataAdapter(cmd)
            conn.Open()

            ds = New DataSet()
            da.Fill(ds)

        End Using

        cblUserSearch.DataSource = ds
        cblUserSearch.DataTextField = "COLUMN_NAME"
        cblUserSearch.DataValueField = "COLUMN_NAME"
        cblUserSearch.DataBind()

        cblAddBoxes.DataSource = ds
        cblAddBoxes.DataTextField = "COLUMN_NAME"
        cblAddBoxes.DataValueField = "COLUMN_NAME"
        cblAddBoxes.DataBind()

    End Sub
    Sub LoadADInfo()
        Try


            'Dim searcher As DirectorySearcher = New System.DirectoryServices.DirectorySearcher("(samaccountname=*)")
            'Dim result2 As SearchResult = searcher.FindOne()
            'Dim dentry As DirectoryEntry = result2.GetDirectoryEntry()
            'Dim fullname As String = dentry.Properties("displayName").Value.ToString()

            ''Dim userlist As System.DirectoryServices.SearchResultCollection = searcher.FindAll()
            ''For i As Integer = 0 To userlist.Count - 1
            ''    userlist(i).Properties("displayName").ToString()
            ''    ddlAllUsers.Items.Add(userlist(i).Properties.Item("cn").ToString())
            ''Next

            'Dim propnames As New List(Of String)()
            'Dim colprop As System.DirectoryServices.PropertyCollection
            'For Each elem As System.DirectoryServices.PropertyValueCollection In dentry.Properties
            '    propnames.Add(elem.PropertyName)
            '    ddlAllRoles.Items.Add(elem.PropertyName)
            'Next



            'Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            'Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            'Dim oresult As SearchResultCollection
            'Dim result As SearchResult

            '' osearcher.Filter = "(&(objectCategory=person))" ' search filter
            'osearcher.Filter = "(&(samaccountname=m*))" ' search filter
            'osearcher.PropertiesToLoad.Add("cn") ' username
            'osearcher.PropertiesToLoad.Add("name") ' full name
            'osearcher.PropertiesToLoad.Add("givenname") ' firstname
            'osearcher.PropertiesToLoad.Add("sn") ' lastname
            'osearcher.PropertiesToLoad.Add("mail") ' mail
            'osearcher.PropertiesToLoad.Add("initials") ' initials
            'osearcher.PropertiesToLoad.Add("ou") ' organizational unit
            'osearcher.PropertiesToLoad.Add("userPrincipalName") ' login name
            'osearcher.PropertiesToLoad.Add("distinguishedName") ' distinguised name
            'osearcher.PropertiesToLoad.Add("member")
            'oresult = osearcher.FindAll()


            'For Each result In oresult
            '    If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
            '        ' writes specific values retrieved from above - this is just a sample.
            '        'Response.Write(result.GetDirectoryEntry.Properties("cn").Value & ":" & result.GetDirectoryEntry.Properties("userPrincipalName").Value)
            '        cblAllUsers.Items.Add(result.GetDirectoryEntry.Properties("cn").Value & ":" & result.GetDirectoryEntry.Properties("distinguishedName").Value & _
            '                                 ":" & result.GetDirectoryEntry.Properties("userPrincipalName").Value)
            '    End If
            'Next



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub LoadAllRoles()
        Try


            Using con As New SqlConnection(conString)
                SQL = "select RoleName, RoleID , ApplicationName " & _
                          " FROM [WebFD].[dbo].[aspnet_Roles] a  " & _
                          "join WebFD.dbo.aspnet_Applications b on a.ApplicationId = b.ApplicationId "

                cmd = New SqlCommand(SQL, con)
                da = New SqlDataAdapter(cmd)
                con.Open()

                ds = New DataSet()
                da.Fill(ds)

                If ds.Tables.Count > 0 Then
                    ddlAllRoles.DataTextField = ds.Tables(0).Columns("RoleName").ToString()
                    ddlAllRoles.DataValueField = ds.Tables(0).Columns("RoleID").ToString()

                    ddlAllRoles.DataSource = ds.Tables(0)
                    ddlAllRoles.DataBind()
                End If
            End Using
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnCheckUser_Click(sender As Object, e As EventArgs) Handles btnCheckUser.Click
        Try
            If txtUserID.Text <> "" Then
                LoadUserRoles(Trim(Replace(txtUserID.Text, "'", "''")))
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadUserRoles(ByVal UserID As String)
        Try

            Using con As New SqlConnection(conString)
                SQL = "select UserName, a.UserId, RoleName " & _
                "from  [WebFD].[dbo].[aspnet_Users]a " & _
                "join     [WebFD].[dbo].[aspnet_UsersInRoles] b on a.UserId = b.UserId " & _
                "join [WebFD].[dbo].[aspnet_Roles] c on b.RoleId = c.RoleId " & _
                "where UserName = '" & UserID & "' "
                cmd = New SqlCommand(SQL, con)
                da = New SqlDataAdapter(cmd)
                con.Open()

                ds = New DataSet()
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    gvUserRoles.DataSource = ds
                    gvUserRoles.DataBind()
                End If

            End Using
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub gvUserRoles_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvUserRoles.RowDeleting
        Try
            Dim UserID As String
            Dim RoleName As String
            Dim UserName As String

            ' txtUserID.Text = e.RowIndex.ToString
            UserName = gvUserRoles.Rows(e.RowIndex).Cells(1).Text
            UserID = gvUserRoles.Rows(e.RowIndex).Cells(2).Text
            RoleName = gvUserRoles.Rows(e.RowIndex).Cells(3).Text

            If UserName <> "" Then
                SQL = "delete [WebFD].[dbo].[aspnet_UsersInRoles] " & _
                "where UserId = '" & UserID & "' " & _
                "and RoleId = (select RoleId from WebFD.dbo.aspnet_Roles where RoleName = '" & RoleName & "') "
                Using con As New SqlConnection(conString)
                    cmd = New SqlCommand(SQL, con)
                    con.Open()
                    cmd.ExecuteNonQuery()

                End Using
                LoadUserRoles(UserName)

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnAddNewRole_Click(sender As Object, e As EventArgs) Handles btnAddNewRole.Click
        Try
            If txtUserID.Text <> "" Then

                'Removed MFloyd 9/29/2014
                'SQL = "insert into WebFD.dbo.aspnet_UsersInRoles  " & _
                '"( [UserId],[RoleId]) " & _
                '"values " & _
                '"((select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtUserID.Text & "'),   '" & ddlAllRoles.SelectedValue & "' ) "

                'SQL = "if not exists " & _
                '    "(select * from WebFD.dbo.aspnet_UsersInRoles where UserId = (select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtUserID.Text & "') " & _
                '    "and RoleId = '" & ddlAllRoles.SelectedValue & "')  " & _
                '    "Begin " & _
                '    "insert into WebFD.dbo.aspnet_UsersInRoles   " & _
                '    " ( [UserId],[RoleId])  " & _
                '    " values  " & _
                '    " ((select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtUserID.Text & "'),   '" & ddlAllRoles.SelectedValue & "' ) " & _
                '    "end "



                'SQL = "if not exists " & _
                '"(select * from WebFD.dbo.aspnet_UsersInRoles " & _
                ' "where UserId = (select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtUserID.Text & "') " & _
                ' "and RoleId = 'df785476-7211-4cb3-b372-d177121a16e5') " & _
                '  "Begin " & _
                '  "if not exists (select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtUserID.Text & "') " & _
                '  "begin " & _
                '   "insert into WebFD.dbo.aspnet_Users values " & _
                '"('5A20864E-8700-4FFF-9419-8445308B25DA',   " & _
                '"(select SUBSTRING( " & _
                '  "RIGHT('0'+ cast(datepart(MILLISECOND,getdate()) as varchar),3)  +  " & _
                '  "cast(datepart(year,getdate()) * DATEPART(millisecond,getdate()) as varchar) +   " & _
                '  "right('0' + cast (datepart(month,getdate()) as varchar),2) + " & _
                '  "RIGHT('0'+ cast(datepart(ss,getdate()) as varchar),2)  ,3,8) +    '-' +  " & _
                '  "RIGHT(cast(datepart(HH,getdate()) * datepart(millisecond, getdate()) as varchar),2) + " & _
                '  "RIGHT(cast(datepart(MINUTE ,getdate()) as varchar),2) + '-' + " & _
                '  "right('0' + cast (datepart(month,getdate()) as varchar),2) + " & _
                '  "right('0' + cast (datepart(year,getdate()) as varchar),2) + '-'+ " & _
                '  "RIGHT('0' + cast (datepart(HH, getdate()) as varchar),2) + " & _
                '  "RIGHT('0' + cast (datepart(DAY , getdate()) as varchar),2) + '-' + " & _
                '  "RIGHT('0' + cast(datepart(hh,getdate()) as varchar) ,2 ) +  " & _
                '  "RIGHT('9' + CAST(datepart(millisecond,getdate()) * datepart(yy,getdate()) as varchar), 4) +  " & _
                '  "right('5' + cast (datepart(HH,getdate()) * DATEPART(minute,getdate()) as varchar),2) +  " & _
                '  "right('2' + cast (datepart(YY,getdate()) * DATEPART(MILLISECOND,getdate()) as varchar),4)), " & _
                '   "'" & txtUserID.Text & "', '" & txtUserID.Text & "', 'NULL', '0', GETDATE()) " & _
                '  "end " & _
                '     "insert into WebFD.dbo.aspnet_UsersInRoles   " & _
                '  " ( [UserId],[RoleId])   values   ((select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtUserID.Text & "'),   '" & ddlAllRoles.SelectedValue & "' ) " & _
                '   "end  "




                SQL = "if not exists (select ApplicationID from WebFD.dbo.aspnet_Users where UserName = '" & Trim(Replace(txtUserID.Text, "'", "''")) & "' )" & _
                "begin  " & _
                "  insert into WebFD.dbo.aspnet_Users values ('5A20864E-8700-4FFF-9419-8445308B25DA',   " & _
                   "(select SUBSTRING( " & _
                                  "RIGHT('0'+ cast(datepart(MILLISECOND,getdate()) as varchar),3)  +  " & _
                                  "cast(datepart(year,getdate()) * DATEPART(millisecond,getdate()) as varchar) +   " & _
                                  "right('0' + cast (datepart(month,getdate()) as varchar),2) + " & _
                                  "RIGHT('0'+ cast(datepart(ss,getdate()) as varchar),2)  ,3,8) +    '-' +  " & _
                                  "RIGHT(cast(datepart(HH,getdate()) * datepart(millisecond, getdate()) as varchar),2) + " & _
                                  "RIGHT(cast(datepart(MINUTE ,getdate()) as varchar),2) + '-' + " & _
                                  "right('0' + cast (datepart(month,getdate()) as varchar),2) + " & _
                                  "right('0' + cast (datepart(year,getdate()) as varchar),2) + '-'+ " & _
                                  "RIGHT('0' + cast (datepart(HH, getdate()) as varchar),2) + " & _
                                  "RIGHT('0' + cast (datepart(DAY , getdate()) as varchar),2) + '-' + " & _
                                  "RIGHT('0' + cast(datepart(hh,getdate()) as varchar) ,2 ) +  " & _
                                  "RIGHT('9' + CAST(datepart(millisecond,getdate()) * datepart(yy,getdate()) as varchar), 4) +  " & _
                                  "right('5' + cast (datepart(HH,getdate()) * DATEPART(minute,getdate()) as varchar),2) +  " & _
                                  "right('2' + cast (datepart(YY,getdate()) * DATEPART(MILLISECOND,getdate()) as varchar),4)), " & _
                                   "  '" & Trim(Replace(txtUserID.Text, "'", "''")) & "', '" & Trim(Replace(txtUserID.Text, "'", "''")) & "', 'NULL', '0', GETDATE())  " & _
                                  "end " & _
                                  "   " & _
                                  "if not exists (select * from WebFD.dbo.aspnet_UsersInRoles " & _
                                  "where UserId = (select UserId FROM [WebFD].[dbo].[aspnet_Users] " & _
                                  "where UserName = '" & Trim(Replace(txtUserID.Text, "'", "''")) & "') " & _
                                  " and RoleId = '" & ddlAllRoles.SelectedValue & "' ) " & _
                                  " begin " & _
                                  " insert into WebFD.dbo.aspnet_UsersInRoles   " & _
                                  " ( [UserId],[RoleId]) " & _
                                  "   values   ((select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & Trim(Replace(txtUserID.Text, "'", "''")) & "'),   '" & ddlAllRoles.SelectedValue & "' ) " & _
                                  " end "


                Using con As New SqlConnection(conString)
                    cmd = New SqlCommand(SQL, con)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
                LoadUserRoles(txtUserID.Text)

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnFindUserInfo_Click(sender As Object, e As EventArgs) Handles btnFindUserInfo.Click
        Try
            lblUserInfo.Text = ""
            'Dim searcher As DirectorySearcher = New System.DirectoryServices.DirectorySearcher("(samaccountname=*)")
            'Dim result2 As SearchResult = searcher.FindOne()
            'Dim dentry As DirectoryEntry = result2.GetDirectoryEntry()
            '  Dim fullname As String = dentry.Properties("displayName").Value.ToString()

            'Dim userlist As System.DirectoryServices.SearchResultCollection = searcher.FindAll()
            'For i As Integer = 0 To userlist.Count - 1
            '    userlist(i).Properties("displayName").ToString()
            '    ddlAllUsers.Items.Add(userlist(i).Properties.Item("cn").ToString())
            'Next

            'Dim propnames As New List(Of String)()
            '' Dim colprop As System.DirectoryServices.PropertyCollection
            'For Each elem As System.DirectoryServices.PropertyValueCollection In dentry.Properties
            '    ' propnames.Add(elem.PropertyName)
            '    '  ddlAllRoles.Items.Add(elem.PropertyName)
            'Next

            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            If rblSearchType.Items.Item(0).Selected = True Then  'Email Address
                osearcher.Filter = "(&(mail=" & txtSearchInfo.Text & "*))" ' search filter
            End If
            If rblSearchType.Items.Item(1).Selected = True Then  'UserID 
                osearcher.Filter = "(&(samaccountname=" & txtSearchInfo.Text & "*))" ' search filter
            End If
            If rblSearchType.Items.Item(2).Selected = True Then   'Last Name 
                osearcher.Filter = "(&(cn=*" & txtSearchInfo.Text & "))" ' search filter
            End If
            If rblSearchType.Items.Item(3).Selected = True Then  'Full Name 
                osearcher.Filter = "(&(cn=*" & txtSearchInfo.Text & "*))" ' search filter
            End If

            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()
            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    lblUserInfo.Text = lblUserInfo.Text & "cn: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & "<br/>" & _
                    "name: " & result.GetDirectoryEntry.Properties("name").Value & "<br/>" & _
                    "givenname: " & result.GetDirectoryEntry.Properties("givenname").Value & "<br/>" & _
                    "samaccountname: " & result.GetDirectoryEntry.Properties("samaccountname").Value & "<br/>" & _
                    "sn: " & result.GetDirectoryEntry.Properties("sn").Value & "<br/>" & _
                    "mail: " & result.GetDirectoryEntry.Properties("mail").Value & "<br/>" & _
                    "initials: " & result.GetDirectoryEntry.Properties("initials").Value & "<br/>" & _
                    "ou: " & result.GetDirectoryEntry.Properties("ou").Value & vbCrLf & _
                    "userPrincipalName: " & result.GetDirectoryEntry.Properties("userPrincipalName").Value & "<br/>" & _
                    "distinguishedName: " & result.GetDirectoryEntry.Properties("distinguishedName").Value & "<br/>" & _
                    "member: " & result.GetDirectoryEntry.Properties("member").Value & "<br/>  <br/> "
                    ' "member of:  " & result.GetDirectoryEntry.Properties("memberOf").Value & "<br/>  <br/>"

                End If
            Next

            Exit Sub

            ' osearcher.Filter = "(&(objectCategory=person))" ' search filter

            osearcher.PropertiesToLoad.Add("cn") ' username
            osearcher.PropertiesToLoad.Add("name") ' full name
            osearcher.PropertiesToLoad.Add("givenname") ' firstname
            osearcher.PropertiesToLoad.Add("sn") ' lastname
            osearcher.PropertiesToLoad.Add("mail") ' mail
            osearcher.PropertiesToLoad.Add("initials") ' initials
            osearcher.PropertiesToLoad.Add("ou") ' organizational unit
            osearcher.PropertiesToLoad.Add("userPrincipalName") ' login name
            osearcher.PropertiesToLoad.Add("distinguishedName") ' distinguised name
            osearcher.PropertiesToLoad.Add("member")
            osearcher.PropertiesToLoad.Add("samaccountname")
            oresult = osearcher.FindAll()


            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    ' writes specific values retrieved from above - this is just a sample.
                    'Response.Write(result.GetDirectoryEntry.Properties("cn").Value & ":" & result.GetDirectoryEntry.Properties("userPrincipalName").Value)
                    lblUserInfo.Text = lblUserInfo.Text & "cn: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & _
                           "name: " & result.GetDirectoryEntry.Properties("name").Value & vbCrLf & _
                           "givenname: " & result.GetDirectoryEntry.Properties("givenname").Value & vbCrLf & _
                            "samaccountname: " & result.GetDirectoryEntry.Properties("samaccountname").Value & vbCrLf & _
                           "sn: " & result.GetDirectoryEntry.Properties("sn").Value & vbCrLf & _
                           "mail: " & result.GetDirectoryEntry.Properties("mail").Value & vbCrLf & _
                           "initials: " & result.GetDirectoryEntry.Properties("initials").Value & vbCrLf & _
                           "ou: " & result.GetDirectoryEntry.Properties("ou").Value & vbCrLf & _
                           "userPrincipalName: " & result.GetDirectoryEntry.Properties("userPrincipalName").Value & vbCrLf & _
                           "distinguishedName: " & result.GetDirectoryEntry.Properties("distinguishedName").Value & vbCrLf
                    '& _
                    '                           "member: " & result.GetDirectoryEntry.Properties("member").Value & vbCrLf

                    'cblAllUsers.Items.Add(result.GetDirectoryEntry.Properties("cn").Value & ":" & result.GetDirectoryEntry.Properties("distinguishedName").Value & _
                    '                         ":" & result.GetDirectoryEntry.Properties("userPrincipalName").Value)
                End If
            Next


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub PopulategvEDSearches(e As Integer)

        Try

            Dim sql As String = "SELECT IdUser, [NetworkLogin] " & _
          ",[Email] " & _
          ",[FirstName] " & _
          ",[LastName] " & _
          ",[Title] " & _
          " FROM [DWH].[dbo].[Email_Distribution] "

            If e = 1 Then
                sql = sql & " where NetworkLogin like '%" & Trim(Replace(txtEDSearchlogin.Text, "'", "''")) & "%' " & _
                    " and (FirstName + ' ' + LastName like '%" & Trim(Replace(txtEDSearchName.Text, "'", "''")) & "%' " & _
                    " or LastName + ', ' + FirstName like '%" & Trim(Replace(txtEDSearchName.Text, "'", "''")) & "%') "
            ElseIf e = 2 Then
                sql = sql & " where "
                Select Case ddlAdvEmail.SelectedIndex
                    Case 0
                    Case 1
                        sql = sql & "Email like '%" & Trim(Replace(txtAdvEmail.Text, "'", "''")) & "%' and "
                    Case 2
                        sql = sql & "Email like '" & Trim(Replace(txtAdvEmail.Text, "'", "''")) & "%' and "
                    Case 3
                        sql = sql & "Email like '%" & Trim(Replace(txtAdvEmail.Text, "'", "''")) & "' and "
                    Case 4
                        sql = sql & "Email is null and "
                    Case 5
                        sql = sql & "Email is not null and "
                End Select

                Select Case ddlAdvFirstName.SelectedIndex
                    Case 0
                    Case 1
                        sql = sql & "FirstName like '%" & Trim(Replace(txtAdvFirstName.Text, "'", "''")) & "%' and "
                    Case 2
                        sql = sql & "FirstName like '" & Trim(Replace(txtAdvFirstName.Text, "'", "''")) & "%' and "
                    Case 3
                        sql = sql & "FirstName like '%" & Trim(Replace(txtAdvFirstName.Text, "'", "''")) & "' and "
                    Case 4
                        sql = sql & "FirstName is null and "
                    Case 5
                        sql = sql & "FirstName is not null and "
                End Select

                Select Case ddlAdvLastName.SelectedIndex
                    Case 0
                    Case 1
                        sql = sql & "LastName like '%" & Trim(Replace(txtAdvLastName.Text, "'", "''")) & "%' and "
                    Case 2
                        sql = sql & "LastName like '" & Trim(Replace(txtAdvLastName.Text, "'", "''")) & "%' and "
                    Case 3
                        sql = sql & "LastName like '%" & Trim(Replace(txtAdvLastName.Text, "'", "''")) & "' and "
                    Case 4
                        sql = sql & "LastName is null and "
                    Case 5
                        sql = sql & "LastName is not null and "
                End Select

                Select Case ddlAdvNetworkLogin.SelectedIndex
                    Case 0
                    Case 1
                        sql = sql & "NetworkLogin like '%" & Trim(Replace(txtAdvNetLogin.Text, "'", "''")) & "%' and "
                    Case 2
                        sql = sql & "NetworkLogin like '" & Trim(Replace(txtAdvNetLogin.Text, "'", "''")) & "%' and "
                    Case 3
                        sql = sql & "NetworkLogin like '%" & Trim(Replace(txtAdvNetLogin.Text, "'", "''")) & "' and "
                    Case 4
                        sql = sql & "NetworkLogin is null and "
                    Case 5
                        sql = sql & "NetworkLogin is not null and "
                End Select

                Select Case ddlAdvPhone.SelectedIndex
                    Case 0
                    Case 1
                        sql = sql & "Phone like '%" & Trim(Replace(txtAdvPhone.Text, "'", "''")) & "%' and "
                    Case 2
                        sql = sql & "Phone like '" & Trim(Replace(txtAdvPhone.Text, "'", "''")) & "%' and "
                    Case 3
                        sql = sql & "Phone like '%" & Trim(Replace(txtAdvPhone.Text, "'", "''")) & "' and "
                    Case 4
                        sql = sql & "Phone is null and "
                    Case 5
                        sql = sql & "Phone is not null and "
                End Select

                Select Case ddlAdvTitle.SelectedIndex
                    Case 0
                    Case 1
                        sql = sql & "Title like '%" & Trim(Replace(txtAdvTitle.Text, "'", "''")) & "%' and "
                    Case 2
                        sql = sql & "Title like '" & Trim(Replace(txtAdvTitle.Text, "'", "''")) & "%' and "
                    Case 3
                        sql = sql & "Title like '%" & Trim(Replace(txtAdvTitle.Text, "'", "''")) & "' and "
                    Case 4
                        sql = sql & "Title is null and "
                    Case 5
                        sql = sql & "Title is not null and "
                End Select
                For i As Integer = 0 To cblUserSearch.Items.Count - 1
                    If cblUserSearch.Items(i).Selected = True Then
                        sql = sql & cblUserSearch.Items(i).Value & " > 0 and "
                    End If
                Next

                sql = Left(sql, Len(sql) - 5)
            End If

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDConn").ConnectionString)
                da = New SqlDataAdapter(sql, conn2)
                da.Fill(dt)
                gvEDSearches.DataSource = dt
                gvEDSearches.DataBind()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnEDSearchSimple_Click(sender As Object, e As System.EventArgs) Handles btnEDSearchSimple.Click
        Try
            PopulategvEDSearches(1)
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub lbSearchAdv_Click(sender As Object, e As System.EventArgs) Handles lbSearchAdv.Click

        Try



            If Trim(txtEDSearchlogin.Text) = "" Then
                ddlAdvNetworkLogin.SelectedIndex = 0
                txtAdvNetLogin.Text = ""
            Else
                ddlAdvNetworkLogin.SelectedIndex = 1
                txtAdvNetLogin.Text = Trim(txtEDSearchlogin.Text)
            End If

            If Trim(txtEDSearchName.Text) = "" Then
                ddlAdvFirstName.SelectedIndex = 0
                ddlAdvLastName.SelectedIndex = 0
                txtAdvFirstName.Text = ""
                txtAdvLastName.Text = ""
            Else
                ddlAdvFirstName.SelectedIndex = 1
                If InStr(txtEDSearchName.Text, ",") > 0 Then
                    ddlAdvLastName.SelectedIndex = 1
                    txtAdvLastName.Text = Mid(txtEDSearchName.Text, 1, InStr(txtEDSearchName.Text, ",") - 1)
                    ddlAdvFirstName.SelectedIndex = 2
                    txtAdvFirstName.Text = Mid(txtEDSearchName.Text, InStr(txtEDSearchName.Text, ",") + 2, Len(txtEDSearchName.Text) - InStr(txtEDSearchName.Text, ",") - 2)
                ElseIf InStr(txtEDSearchName.Text, " ") > 0 Then
                    ddlAdvLastName.SelectedIndex = 2
                    txtAdvFirstName.Text = Mid(txtEDSearchName.Text, 1, InStr(txtEDSearchName.Text, " ") - 1)
                    txtAdvLastName.Text = Mid(txtEDSearchName.Text, InStr(txtEDSearchName.Text, " ") + 1, Len(txtEDSearchName.Text) - InStr(txtEDSearchName.Text, " ") - 1)
                Else
                    ddlAdvLastName.SelectedIndex = 0
                    txtAdvFirstName.Text = txtEDSearchName.Text
                End If
            End If

            pnlAdvancedSearch.Visible = True
            pnlAdvancedSearch.CssClass = vbEmpty
            tblBasicSearch.Visible = False
            pnlgvDetails.Visible = False
            If IsNothing(gvEDSearches.SelectedRow) Then
            Else
                gvEDSearches.SelectedRow.BackColor = Drawing.Color.White
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub lbCloseAdv_Click(sender As Object, e As System.EventArgs) Handles lbCloseAdv.Click

        Try
            pnlAdvancedSearch.Visible = False
            tblBasicSearch.Visible = True
            tblBasicSearch.CssClass = vbEmpty
            pnlgvDetails.Visible = False
            If gvEDSearches.SelectedIndex = -1 Then
            Else
                gvEDSearches.SelectedRow.BackColor = Drawing.Color.White
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnAdvancedSearch_Click(sender As Object, e As System.EventArgs) Handles btnAdvancedSearch.Click
        Try
            PopulategvEDSearches(2)
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEDSearches_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvEDSearches.PageIndexChanging
        Try
            If pnlAdvancedSearch.Visible = True Then
                PopulategvEDSearches(2)
            Else
                PopulategvEDSearches(1)
            End If
            gvEDSearches.PageIndex = e.NewPageIndex
            gvEDSearches.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvEDSearches_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvEDSearches.RowCancelingEdit
        Try
            gvEDSearches.EditIndex = -1

            If pnlAdvancedSearch.Visible = True Then
                PopulategvEDSearches(2)
            Else
                PopulategvEDSearches(1)
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEDSearches_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvEDSearches.RowEditing
        Try
            gvEDSearches.EditIndex = e.NewEditIndex
            If pnlAdvancedSearch.Visible = True Then
                PopulategvEDSearches(2)
            Else
                PopulategvEDSearches(1)
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEDSearches_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvEDSearches.RowUpdating
        Try

            Dim selectedRow As GridViewRow = gvEDSearches.Rows(CInt(e.RowIndex))
            Dim txtgvEDNetworkLogin As TextBox = gvEDSearches.Rows(e.RowIndex).FindControl("txtgvEDNetworkLogin")
            Dim txtgvEDEmail As TextBox = gvEDSearches.Rows(e.RowIndex).FindControl("txtgvEDEmail")
            Dim txtgvEDFirstName As TextBox = gvEDSearches.Rows(e.RowIndex).FindControl("txtgvEDFirstName")
            Dim txtgvEDLastName As TextBox = gvEDSearches.Rows(e.RowIndex).FindControl("txtgvEDLastName")
            Dim txtgvEDTitle As TextBox = gvEDSearches.Rows(e.RowIndex).FindControl("txtgvEDTitle")
            Dim lblIdUser As Label = gvEDSearches.Rows(e.RowIndex).FindControl("lblIdUser")

            Dim sql As String = "Update [DWH].[dbo].[Email_Distribution] set NetworkLogin = '" & Replace(txtgvEDNetworkLogin.Text.ToString, "'", "''") & "'," & _
                " Email = '" & Replace(txtgvEDEmail.Text.ToString, "'", "''") & "', " & _
                " FirstName = '" & Replace(txtgvEDFirstName.Text.ToString, "'", "''") & "', " & _
                " LastName = '" & Replace(txtgvEDLastName.Text.ToString, "'", "''") & "', " & _
                " Title = '" & Replace(txtgvEDTitle.Text.ToString, "'", "''") & "' " & _
                " where IdUser = " & lblIdUser.Text

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDConn").ConnectionString)
                cmd = New SqlCommand(sql, con)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using

            gvEDSearches.EditIndex = -1
            If pnlAdvancedSearch.Visible = True Then
                PopulategvEDSearches(2)
            Else
                PopulategvEDSearches(1)
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvEDSearches_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvEDSearches.SelectedIndexChanged

        Try
            pnlAdvancedSearch.CssClass = "shrinkselect"
            tblBasicSearch.CssClass = "shrinkselect"

            For Each colorrow As GridViewRow In gvEDSearches.Rows
                colorrow.BackColor = Drawing.Color.White
            Next

            gvEDSearches.SelectedRow.BackColor = Drawing.Color.LightSteelBlue
            PopulateDetails()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub
    Sub PopulateDetails()

        Try

            Dim lblIdUser As Label = gvEDSearches.SelectedRow.FindControl("lblIdUser")
            Dim columnpull As String = "select COLUMN_NAME, DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_SCHEMA = 'dbo' and TABLE_NAME = 'email_Distribution'" & _
                " and COLUMN_NAME not in ('Email', 'FirstName', 'LastName', 'IdUser', 'Title', 'NetworkLogin') " & _
                " order by COLUMN_NAME asc "

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                cmd = New SqlCommand(columnpull, conn)
                da = New SqlDataAdapter(cmd)
                conn.Open()

                ds = New DataSet()
                da.Fill(ds)

            End Using

            Dim col As DataRow
            Dim fullsql As String = ""

            For Each col In ds.Tables(0).Rows
                fullsql = fullsql & " select '" & col("COLUMN_NAME").ToString() & "' as [Column Name], " & col("COLUMN_NAME").ToString() & _
                    " as [Column Value] from dbo.Email_Distribution where IdUser = " & lblIdUser.Text & " union "
            Next

            fullsql = Left(fullsql, Len(fullsql) - 7)

            Dim da2 As New SqlDataAdapter
            Dim dt2 As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDConn").ConnectionString)
                da2 = New SqlDataAdapter(fullsql, conn2)
                da2.Fill(dt2)
                gvEDUserDetails.DataSource = dt2
                gvEDUserDetails.DataBind()
            End Using

            pnlgvDetails.Visible = True

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEDUserDetails_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvEDUserDetails.RowCancelingEdit

        Try
            gvEDUserDetails.EditIndex = -1
            PopulateDetails()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub gvEDUserDetails_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvEDUserDetails.RowEditing

        Try
            gvEDUserDetails.EditIndex = e.NewEditIndex
            PopulateDetails()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub gvEDUserDetails_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvEDUserDetails.RowUpdating

        Try
            Dim txtgvEDTitle As TextBox = gvEDUserDetails.Rows(e.RowIndex).FindControl("txtgvUDColumnValue")
            Dim lblgvUDColumnName As Label = gvEDUserDetails.Rows(e.RowIndex).FindControl("lblgvUDColumnName")
            Dim lblIdUser As Label = gvEDSearches.SelectedRow.FindControl("lblIdUser")

            Dim sql As String = "Update [DWH].[dbo].[Email_Distribution] set " & Replace(lblgvUDColumnName.Text.ToString, "'", "''") & _
                " = '" & Replace(txtgvEDTitle.Text.ToString, "'", "''") & "' " & _
                " where IdUser = " & lblIdUser.Text

            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDConn").ConnectionString)
                cmd = New SqlCommand(sql, con)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using

            gvEDUserDetails.EditIndex = -1
            PopulateDetails()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Try

            If Trim(txtAddEmail.Text.ToString) = "" Then
                explantionlabel.Text = "Email Address required."
                ModalPopupExtender1.Show()
                Exit Sub

            Else

                Dim ChckSQL As String = "Select count(*) from DWH.dbo.Email_Distribution where [Email] = '" & Replace(txtAddEmail.Text.ToString, "'", "''") & _
                    "' or [NetworkLogin] = '" & Replace(txtAddNetworkLogin.Text.ToString, "'", "''") & "'"

                Dim cmd2 As SqlCommand
                Dim emailcnt As Integer = 1

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd2 = New SqlCommand(ChckSQL, conn)
                    cmd2.CommandTimeout = 86400
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    emailcnt = cmd2.ExecuteScalar

                End Using

                If emailcnt > 0 Then
                    explantionlabel.Text = "Network Login or Email Address already in use."
                    ModalPopupExtender1.Show()
                    Exit Sub

                Else
                    Dim AddSQL As String = "Insert into DWH.dbo.Email_Distribution ([NetworkLogin], [Email], [FirstName], [LastName], [Title], [Phone] "

                    Dim SQLEnd As String = ") values ('" & Replace(txtAddNetworkLogin.Text.ToString, "'", "''") & "', '" & Replace(txtAddEmail.Text.ToString, "'", "''") & "', '" & _
                        Replace(txtAddFirstName.Text.ToString, "'", "''") & "', '" & Replace(txtAddLastName.Text.ToString, "'", "''") & "', '" & _
                        Replace(txtAddTitle.Text.ToString, "'", "''") & "', '" & Replace(txtAddPhone.Text.ToString, "'", "''") & "'"

                    For i As Integer = 0 To cblAddBoxes.Items.Count - 1
                        If cblAddBoxes.Items(i).Selected = True Then
                            AddSQL = AddSQL & ", " & cblAddBoxes.Items(i).Text
                            'SQLEnd = SQLEnd & ", '" & cblAddBoxes.Items(i).Value & "' "
                            SQLEnd = SQLEnd & ", '1' "
                        End If
                    Next

                    AddSQL = AddSQL + SQLEnd + ")"

                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDConn").ConnectionString)
                        cmd = New SqlCommand(AddSQL, con)
                        con.Open()
                        cmd.ExecuteNonQuery()

                        explantionlabel.Text = "User Added"
                        ModalPopupExtender1.Show()

                    End Using
                End If



            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            explantionlabel.Text = "Error -- Please Contact Admin"
            ModalPopupExtender1.Show()
        End Try

    End Sub
End Class