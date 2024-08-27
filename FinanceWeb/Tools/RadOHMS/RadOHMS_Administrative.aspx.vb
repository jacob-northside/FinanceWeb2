Imports FinanceWeb.WebFinGlobal
Imports System.Data.SqlClient
Imports System.IO
Imports System.DirectoryServices


'####################### NOTES ###############################'
'If you are getting an error where it is showing "All Locations" when the user shouldn't have all locations then
'there is a location in the location table or permission table that doesn't exist in the other
'####################### NOTES ###############################'

Public Class RadOHMS_Administrative
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim HTTPRequest As String = ""


            Using reader As StreamReader = New StreamReader(HttpContext.Current.Request.InputStream)
                HTTPRequest = reader.ReadToEnd()
            End Using
            Debug.Print("REQUEST >>> " + HTTPRequest.ToString)

            If IsPostBack Then
                FlashMessages.InnerText = ""
                FlashMessages.Visible = False

                Debug.Print("Postback At" + Now)
            Else
                FlashMessages.InnerText = ""
                FlashMessages.Visible = False
                LoadLocationGrid()

                If User.Identity.IsAuthenticated = True Then

                Else

                End If

            End If

        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub LoadLocationGrid()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim SQL As String = "select distinct Case when entity = 6 then Location + ' - ATLANTA' + ' ' + LocationDesc when Entity = 10 then Location + ' - CHEROKEE' + " & _
            "' ' + LocationDesc when Entity = 22 then Location + ' - FORSYTH' + ' ' + LocationDesc END LocationDesc from DWH.KPIS.DEV_OHMS_Location_LU l " & _
        "join dwh.kpis.DEV_OHMS_Metric_2_Location m2l on l.LocID = m2l.LocID and m2l.Active = 1 " & _
        "join DWH.KPIS.DEV_OHMS_Metrics m on m2l.MID = m.MID and m.Active = 1 " & _
        "join DWH.KPIS.DEV_OHMS_Title_2_Metric t2m on m.MID = t2m.MID and t2m.Active = 1 and t2m.TitleCode = 1 " & _
        "where l.Active = 1 order by LocationDesc;"

        Try
            ListPermissionLocations.Visible = True
            Debug.Print(SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                ListPermissionLocations.DataSource = ds
                ListPermissionLocations.DataMember = "LookUpData"
                ListPermissionLocations.DataBind()
                conn.Close()
            End Using

        Catch ex As Exception
            Debug.Print("LOAD GRID ERROR")
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Protected Sub FetchUserPermissions_Btn_Click(sender As Object, e As System.EventArgs) Handles FetchUserPermissions_Btn.Click
        Try
            ' "select * from DWH.KPIS.DEV_OHMS_Update_Permissions where UserLogIn = @UserID;"
            Dim SQL As String = "select Case when entity = 10 then Location + ' - ATLANTA' + ' ' + LocationDesc when Entity = 22 then Location + ' - CHEROKEE' + " & _
            "' ' + LocationDesc when Entity = 6 then Location + ' - FORSYTH' + ' ' + LocationDesc END LocationDesc from DWH.KPIS.DEV_OHMS_Update_Permissions p left join " & _
            "(select distinct l.* from DWH.KPIS.DEV_OHMS_Location_LU l join dwh.kpis.DEV_OHMS_Metric_2_Location m2l " & _
            "on l.LocID = m2l.LocID and m2l.Active = 1 join DWH.KPIS.DEV_OHMS_Metrics m on m2l.MID = m.MID and m.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Title_2_Metric t2m on m.MID = t2m.MID and t2m.Active = 1 and t2m.TitleCode = 1    where l.Active = 1)" & _
            " L ON p.LocationID_Limit = l.locid where UserLogIn = @UserID and MetricID_Limit = 15 and p.active = 1;"
            Dim FullName As String = "User Not Found"

            ''''''''''''''''' Look up full name from User ID '''''''''''''''''
            If Add_UserIDPermissions.Text <> "" Then
                Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
                Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
                Dim oresult As SearchResultCollection
                Dim result As SearchResult

                osearcher.Filter = "(&(samaccountname=" & Add_UserIDPermissions.Text & "))" ' UserID search filter

                For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                    osearcher.PropertiesToLoad.Add(elem.PropertyName)
                Next
                oresult = osearcher.FindAll()

                For Each result In oresult
                    If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                        Debug.Print(result.GetDirectoryEntry.Properties("name").Value)
                        FullName = result.GetDirectoryEntry.Properties("name").Value
                    End If
                Next
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                UserLocations.Controls.Clear()      ' Clear UL of selected locations
                Locations_Field.Value = ""          ' Clear out old locations when loading new user
                Locations_SelectedUser.InnerText = FullName + " - " + Add_UserIDPermissions.Text ' Set selected user textbox

                'Dim ds As DataSet
                'Dim da As SqlDataAdapter
                'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                '    Dim SelectCommand As SqlCommand = New SqlCommand(SQL, conn)
                '    SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 100).Value = Add_UserIDPermissions.Text
                '    ds = New DataSet                                ' Get blank dataset to store our data
                '    da = New SqlDataAdapter(SQL, conn)
                '    If conn.State = ConnectionState.Closed Then
                '        conn.Open()
                '    End If
                '    da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                '    da.Fill(ds, "LookUpData")
                '    conn.Close()
                'End Using

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    Dim RetrieveLocations As SqlCommand = New SqlCommand(SQL, conn)
                    RetrieveLocations.Parameters.Add("@UserID", SqlDbType.VarChar, 100).Value = Add_UserIDPermissions.Text

                    Dim myReader As SqlDataReader
                    myReader = RetrieveLocations.ExecuteReader()

                    'Iterate through the results
                    While myReader.Read()
                        If Convert.IsDBNull(myReader("LocationDesc")) Then
                            AddAllLocations()
                            Exit While
                        Else
                            Debug.Print(myReader("LocationDesc"))
                            Dim li As HtmlGenericControl = New HtmlGenericControl("li")
                            li.InnerHtml = "<img class=""RedMinus"" onclick=""remove();"" src=""/Images/RedMinus.png"" height=""10px"" width=""10px"">" + myReader.GetString(0)
                            'UserLocations.Controls.Add(li)
                            ' Add the loaded locations to the hiddle field so we can change pages
                            If InStr(Locations_Field.Value, myReader.GetString(0)) = 0 Then ' Check that location isn't already in hidden field
                                If Locations_Field.Value = "" Then                      ' Single field doesn't need a delimiter
                                    Locations_Field.Value = myReader.GetString(0)
                                Else
                                    Locations_Field.Value = Locations_Field.Value + "|" + myReader.GetString(0) ' Delimit multiple fields
                                End If
                            End If
                        End If
                    End While

                    Debug.Print(Locations_Field.Value)

                End Using
            Else
                Debug.Print("No user id entered")
            End If
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
    End Sub
    Protected Sub ListPermissionLocations_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles ListPermissionLocations.PageIndexChanging
        ListPermissionLocations.PageIndex = e.NewPageIndex
        ListPermissionLocations.DataBind()
        LoadLocationGrid()
        'Search(sender, e)

    End Sub
    Protected Sub AddAllLocations()
        UserLocations.Controls.Clear()      ' Clear UL of selected locations
        Locations_Field.Value = "All Locations"          ' Clear out old locations when loading new user
    End Sub
    Private Sub lbSrchUsr_Click(sender As Object, e As EventArgs) Handles lbSrchUsr.Click
        pnlSrchUser.Visible = True
    End Sub
    Private Sub lbCloseUsrSrch_Click(sender As Object, e As EventArgs) Handles lbCloseUsrSrch.Click
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
            Debug.Print(txtAdminUsrSrch.Text)
            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()
            Debug.Print(oresult.Count)
            For Each result In oresult
                Debug.Print(result.ToString)
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
    Protected Sub SelectAllLocations_Btn_Click(sender As Object, e As System.EventArgs) Handles SelectAllLocations_Btn.Click
        Try

            AddAllLocations()
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
    End Sub
    Protected Sub SubmitPermissionChanges_Btn_Click(sender As Object, e As System.EventArgs) Handles SubmitPermissionChanges_Btn.Click
        Try
            Dim Metrics = New Integer() {2, 13, 15, 16, 17, 30, 31, 32, 33, 34, 35, 36, 37, 38, 96}
            Dim DeleteString As String = "Update DWH.KPIS.DEV_OHMS_Update_Permissions Set Active = 0 Where UserLogIn = @UserID and metricid_limit in (2, 13, 15, 16, 17, 30, 31, 32, 33, 34, 35, 36, 37, 38, 96);"
            Dim InsertString As String = "Insert into DWH.KPIS.DEV_OHMS_Update_Permissions (TitleCode, UserLogIn, MetricID_Limit, LocationID_Limit, Active, ModifyDate, ModifyUser) " & _
                "Select titlecode, userid, metric, LocID, active, modifydate, modifyuser from " & _
                "(Select 1 titlecode, @UserID userid, @Metric metric, 1 active, GETDATE() modifydate, @Modified_By modifyuser) a " & _
                "left join (select distinct l.locid from DWH.KPIS.DEV_OHMS_Location_LU l join dwh.kpis.DEV_OHMS_Metric_2_Location m2l on l.LocID = m2l.LocID and m2l.Active = 1 " & _
                "join DWH.KPIS.DEV_OHMS_Metrics m on m2l.MID = m.MID and m.Active = 1 join DWH.KPIS.DEV_OHMS_Title_2_Metric t2m on m.MID = t2m.MID and t2m.Active = 1 and t2m.TitleCode = 1 " & _
                " where l.Active = 1 and Case when Entity = 10 then 'Atlanta ' + LocationDesc when Entity = 22 then 'Cherokee ' + LocationDesc when Entity = 6 then 'Forsyth ' + LocationDesc " & _
                "end = @Location) b on 1=1;"
            Dim UsersInRoles As String = "Merge into WebFD.dbo.aspnet_UsersInRoles as target " & _
                                         "using (Values (@UserID, @RoleID)) as Source (UserID, RoleID) " & _
                                         "on Target.UserID = Source.UserID and Target.RoleID = Source.RoleID " & _
                                         "when not matched then insert (UserID, RoleID) values (@UserID, @RoleID);"
            Dim Users As String = "Merge into WebFD.dbo.aspnet_Users as target " & _
            "using (Values ('5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', @UserID), @UserID, @UserID, NULL, 0, CURRENT_TIMESTAMP)) " & _
            "as Source (ApplicationID, UserID, UserName, LoweredUserName, MobileAlias, IsAnonymous, LastActivityDate) " & _
            "on target.ApplicationID = Source.ApplicationID and Target.UserID = Source.UserID and Target.LoweredUserName = Source.LoweredUserName " & _
            "and Target.MobileAlias = Source.MobileAlias and Target.IsAnonymous = Source.IsAnonymous when not matched then " & _
            "insert(ApplicationId, UserID, UserName, LoweredUserName, MobileAlias, IsAnonymous, LastActivityDate) " & _
            "values ('5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', @UserID), @UserID, @UserID, NULL, 0, CURRENT_TIMESTAMP);"

            Dim Users2 As String = "Merge into WebFD.dbo.aspnet_Users as target " & _
            "using (Values ('5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', @UserID), @UserID, @UserID, NULL, 0, CURRENT_TIMESTAMP)) " & _
            "as Source (ApplicationID, UserID, UserName, LoweredUserName, MobileAlias, IsAnonymous, LastActivityDate) " & _
            "on target.ApplicationID = Source.ApplicationID and Target.UserName = Source.UserName and Target.LoweredUserName = Source.LoweredUserName " & _
            "when not matched then insert(ApplicationId, UserID, UserName, LoweredUserName, MobileAlias, IsAnonymous, LastActivityDate) " & _
            "values ('5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', @UserID), @UserID, @UserID, NULL, 0, CURRENT_TIMESTAMP);"

                Dim InsertUsersinRoles As String = "Insert into WebFD.dbo.aspnet_UsersInRoles (userid, RoleId) Select USERID, cast('A071690E-D38E-279E-3FA0-43F0CF34F84B' as uniqueidentifier) ROLEID FROM" & _
                "(Select top 1 calendar_date from dwh.dbo.DimDate) dd left join (Select UserID from WebFD.[dbo].[aspnet_Users] " & _
                " where UserName = @Userid) a on 1=1 where not exists (Select * from WebFD.dbo.aspnet_UsersInRoles where userid = a.UserID " & _
                "and roleid = 'A071690E-D38E-279E-3FA0-43F0CF34F84B');"


                Dim LocationsList() As String = Locations_Field.Value.Split("|")

                Dim rgx As New Regex("^.*- ")
                Dim UserID As String = rgx.Replace(input:=Locations_SelectedUser.InnerText, replacement:="")

                If UserID <> "No User Selected" Then
                    If Locations_Field.Value = "" Then
                        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                            conn.Open()
                            Dim DeleteCommand As SqlCommand = New SqlCommand(DeleteString, conn)
                            DeleteCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 16).Value = UserID
                            DeleteCommand.ExecuteNonQuery()
                        End Using
                        FlashMessages.InnerText = "All Permissions Removed."
                        FlashMessages.Visible = True
                        Locations_Field.Value = ""          ' Clear out old locations when loading new user
                        Locations_SelectedUser.InnerText = "No User Selected"
                    Else

                        ' Set old records to inactive '
                        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                            conn.Open()
                            Dim DeleteCommand As SqlCommand = New SqlCommand(DeleteString, conn)
                            DeleteCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 16).Value = UserID
                            DeleteCommand.ExecuteNonQuery()

                            ' Insert new permissions '
                            Dim InsertCommand As SqlCommand = New SqlCommand(InsertString, conn)
                            InsertCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 16).Value = UserID
                            InsertCommand.Parameters.Add("@Modified_By", SqlDbType.VarChar, 16).Value = Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")


                            For Each Metric As Int16 In Metrics
                                If InsertCommand.Parameters.Contains("@Metric") Then
                                    InsertCommand.Parameters("@Metric").Value = Metric
                                Else
                                    InsertCommand.Parameters.Add("@Metric", SqlDbType.Int).Value = Metric
                                End If


                                If LocationsList.Contains("All Locations") Then
                                    If InsertCommand.Parameters.Contains("@Location") Then
                                        InsertCommand.Parameters("@Location").Value = DBNull.Value
                                    Else
                                        InsertCommand.Parameters.AddWithValue("@Location", DBNull.Value)
                                    End If
                                    InsertCommand.ExecuteNonQuery()
                                    Continue For
                                End If

                                For Each Location In LocationsList
                                    If InsertCommand.Parameters.Contains("@Location") Then
                                        InsertCommand.Parameters("@Location").Value = Location
                                    Else
                                        InsertCommand.Parameters.Add("@Location", SqlDbType.VarChar, 255).Value = Location
                                    End If
                                    InsertCommand.ExecuteNonQuery()

                                Next
                            Next

                        Dim UsersCommand As SqlCommand = New SqlCommand(Users2, conn)
                            UsersCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 16).Value = UserID
                            UsersCommand.ExecuteNonQuery()

                            Dim UsersInRolesCommand As SqlCommand = New SqlCommand(InsertUsersinRoles, conn)
                            UsersInRolesCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 16).Value = UserID
                            UsersInRolesCommand.ExecuteNonQuery()

                            conn.Close()
                        End Using

                        FlashMessages.InnerText = "Permissions successfully edited for user: " & UserID
                        FlashMessages.Visible = True
                        UserLocations.Controls.Clear()      ' Clear UL of selected locations
                        Locations_Field.Value = ""          ' Clear out old locations when loading new user
                        Locations_SelectedUser.InnerText = "No User Selected"
                    End If
                Else
                    FlashMessages.InnerText = "Please select a user."
                    FlashMessages.Visible = True
                End If
        Catch ex As Exception
            FlashMessages.InnerText = "Error Encountered."
            FlashMessages.Visible = True
            Debug.Print(ex.ToString)
            Debug.Print(ex.InnerException.ToString)
        End Try
    End Sub
End Class