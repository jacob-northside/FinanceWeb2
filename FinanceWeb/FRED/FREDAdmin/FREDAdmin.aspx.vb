Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

Public Class FREDAdmin
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim conString As String = ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString

    Private Property FREDMQ As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Try

            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        Else
            Try
                LoadDatabases()
                ddlSchema.Visible = False
                lbSelectSchema.Visible = False
                ddlSelectTable.Visible = False
                lbSelectTable.Visible = False
                ddlSelectColumn.Visible = False
                btnAddToFRED.Visible = False
                txtTableAlias.Visible = False
                txtColumnAlias.Visible = False
                'pnlEditTable.Visible = False
                'LoadAvailableTables()
                PopulateGridview()
                PopulateGVCurrentConnections()
                ViewState("SortOrder") = ""
            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try


        End If

    End Sub

#Region "FRED User Administration"
    Private Sub gvFREDUsers_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvFREDUsers.SelectedIndexChanged
        Try
            Dim ActiveStatus As String
            Dim AdminStatus As String
            Dim CostingData As String

            txtFREDUserID.Text = gvFREDUsers.SelectedRow.Cells(1).Text
            ActiveStatus = gvFREDUsers.SelectedRow.Cells(2).Text
            AdminStatus = gvFREDUsers.SelectedRow.Cells(3).Text
            CostingData = gvFREDUsers.SelectedRow.Cells(4).Text

            If ActiveStatus = "True" Then
                rblActiveStatus.Items(0).Selected = True
                rblActiveStatus.Items(1).Selected = False
            Else
                rblActiveStatus.Items(0).Selected = False
                rblActiveStatus.Items(1).Selected = True
            End If
            If AdminStatus = "True" Then
                chbFREDAdmin.Checked = True
            Else
                chbFREDAdmin.Checked = False
            End If
            If CostingData = "True" Then
                chbCosting.Checked = True
            Else
                chbCosting.Checked = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub btnFREDUser_Click(sender As Object, e As System.EventArgs) Handles btnFREDUser.Click
        Try
            Dim IsCosting As String = "False"
            Dim ActiveFred As String = "true"

            If txtFREDUserID.Text <> "" Then
                If rblActiveStatus.Items(0).Selected = True Then
                    ActiveFred = "True"
                Else
                    ActiveFred = "False"
                End If
                If chbCosting.Checked = True Then
                    IsCosting = "True"
                End If

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                    SQL = "If not exists " & _
                    "(select * from WebFD.FRED.USERAccess  " & _
                    "where USERID  = '" & Replace(txtFREDUserID.Text, "'", "''") & "'  )  " & _
                    "begin " & _
                    "insert into WebFD.FRED.USERAccess  " & _
                    "(USERID, CostingAccess )  " & _
                    "values  " & _
                    "('" & Replace(txtFREDUserID.Text, "'", "''") & "' ,  '" & IsCosting & "')  " & _
                    "end  " & _
                    "Update WebFD.FRED.USERAccess set  " & _
                    "CostingAccess =  '" & IsCosting & "'  " & _
                    "where USERID = '" & Replace(txtFREDUserID.Text, "'", "''") & "' "

                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    If chbFREDAdmin.Checked = True Then
                        SQL = "insert into WebFD.dbo.aspnet_UsersInRoles (UserId, RoleId) " & _
                        "(select a.userid, '461CF648-E19A-419C-A2B3-35FA7EC634C8' " & _
                        "from WebFD.dbo.aspnet_Users a " & _
                        "where not exists (select UserId from WebFD.dbo.aspnet_UsersInRoles  " & _
                        "where a.UserId = UserId  " & _
                        "and RoleId = '461CF648-E19A-419C-A2B3-35FA7EC634C8') " & _
                        "and a.UserName = '" & Replace(txtFREDUserID.Text, "'", "''") & "' ) "

                    Else
                        SQL = "Delete WebFD.dbo.aspnet_UsersInRoles " & _
                        "where RoleId = '461CF648-E19A-419C-A2B3-35FA7EC634C8' " & _
                        "and UserID = (select UserId From WebFD.dbo.aspnet_Users where UserName = '" & Replace(txtFREDUserID.Text, "'", "''") & "' ) "
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    If ActiveFred = "True" Then
                        SQL = "insert into WebFD.dbo.aspnet_UsersInRoles (UserId, RoleId) " & _
                        "(select a.userid, 'AD583DB4-34DF-46A6-8498-2B8B16D45D7E' " & _
                        "from WebFD.dbo.aspnet_Users a " & _
                        "where not exists (select UserId from WebFD.dbo.aspnet_UsersInRoles  " & _
                        "where a.UserId = UserId  " & _
                        "and RoleId = 'AD583DB4-34DF-46A6-8498-2B8B16D45D7E') " & _
                        "and a.UserName = '" & Replace(txtFREDUserID.Text, "'", "''") & "' ) "
                    Else
                        SQL = "Delete WebFD.dbo.aspnet_UsersInRoles " & _
                       "where RoleId = 'AD583DB4-34DF-46A6-8498-2B8B16D45D7E' " & _
                       "and UserID = (select UserId From WebFD.dbo.aspnet_Users where UserName = '" & Replace(txtFREDUserID.Text, "'", "''") & "' ) "
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End Using

                gvFREDUsers.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnFindUserInfo_Click(sender As Object, e As EventArgs) Handles btnFindUserInfo.Click
        Try


            lblUserInfo.Text = ""
            lblNEWFRED.Text = ""
            Dim cntFRED As Integer = 0

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
                    "member: " & result.GetDirectoryEntry.Properties("member").Value & "<br/>  <br/>"

                    If cntFRED = 0 Then
                        lblNEWFRED.Text = result.GetDirectoryEntry.Properties("samaccountname").Value
                    Else : lblNEWFRED.Text = ""
                    End If
                    cntFRED += 1
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
                           "distinguishedName: " & result.GetDirectoryEntry.Properties("distinguishedName").Value & vbCrLf & _
                           "member: " & result.GetDirectoryEntry.Properties("member").Value & vbCrLf

                    'cblAllUsers.Items.Add(result.GetDirectoryEntry.Properties("cn").Value & ":" & result.GetDirectoryEntry.Properties("distinguishedName").Value & _
                    '                         ":" & result.GetDirectoryEntry.Properties("userPrincipalName").Value)
                End If
            Next


        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddToFRED_Click(sender As Object, e As System.EventArgs) Handles AddToFRED.Click
        Try
            Dim USERID As String
            If lblNEWFRED.Text <> "" Then

                USERID = Replace(lblNEWFRED.Text, "'", "''")


                SQL = "if not exists " & _
                "(select * from WebFD.dbo.aspnet_Users  " & _
                "where UserName = '" & USERID & "'   ) " & _
                "Begin  " & _
                "insert into WebFD.dbo.aspnet_Users values  " & _
                "('5A20864E-8700-4FFF-9419-8445308B25DA',    " & _
                "(select SUBSTRING(  " & _
                "RIGHT('0'+ cast(datepart(MILLISECOND,getdate()) as varchar),3)  +   " & _
                "cast(datepart(year,getdate()) * DATEPART(millisecond,getdate()) as varchar) +    " & _
                "right('0' + cast (datepart(month,getdate()) as varchar),2) +  " & _
                "RIGHT('0'+ cast(datepart(ss,getdate()) as varchar),2)  ,3,8) +    '-' +   " & _
                "RIGHT(cast(datepart(HH,getdate()) * datepart(millisecond, getdate()) as varchar),2) +  " & _
                "RIGHT(cast(datepart(MINUTE ,getdate()) as varchar),2) + '-' +  " & _
                "right('0' + cast (datepart(month,getdate()) as varchar),2) +  " & _
                "right('0' + cast (datepart(year,getdate()) as varchar),2) + '-'+  " & _
                "RIGHT('0' + cast (datepart(HH, getdate()) as varchar),2) +  " & _
                "RIGHT('0' + cast (datepart(DAY , getdate()) as varchar),2) + '-' +  " & _
                "RIGHT('0' + cast(datepart(hh,getdate()) as varchar) ,2 ) +   " & _
                "RIGHT('9' + CAST(datepart(millisecond,getdate()) * datepart(yy,getdate()) as varchar), 4) +   " & _
                "right('5' + cast (datepart(HH,getdate()) * DATEPART(minute,getdate()) as varchar),2) +   " & _
                "right('2' + cast (datepart(YY,getdate()) * DATEPART(MILLISECOND,getdate()) as varchar),4)),  " & _
                "'" & USERID & "', '" & USERID & "', 'NULL', '0', GETDATE())  " & _
                "end  " & _
                "if not exists (select * from WebFD.dbo.aspnet_UsersInRoles a " & _
                "inner join WebFD.dbo.aspnet_Users b  on a.UserId = b.UserId and a.RoleId = 'AD583DB4-34DF-46A6-8498-2B8B16D45D7E' and b.UserName = '" & USERID & "' ) " & _
                "Begin " & _
                "insert into WebFD.dbo.aspnet_UsersInRoles (UserId, RoleId) " & _
                "(select UserId, 'AD583DB4-34DF-46A6-8498-2B8B16D45D7E' from WebFD.dbo.aspnet_Users where UserName = '" & USERID & "' ) " & _
                "end "

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End Using

                gvFREDUsers.DataBind()
                lblNEWFRED.Text = ""

                'SQL = "if not exists " & _
                '"(select * from WebFD.dbo.aspnet_Users " & _
                '"where UserId = (select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & USERID & "') " & _
                '"and RoleId = 'AD583DB4-34DF-46A6-8498-2B8B16D45D7E') " & _
                '"Begin " & _
                '"if not exists (select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & USERID & "') " & _
                '"begin " & _
                '"insert into WebFD.dbo.aspnet_Users values " & _
                '"('5A20864E-8700-4FFF-9419-8445308B25DA',   " & _
                '"(select SUBSTRING( " & _
                '"RIGHT('0'+ cast(datepart(MILLISECOND,getdate()) as varchar),3)  +  " & _
                '"cast(datepart(year,getdate()) * DATEPART(millisecond,getdate()) as varchar) +   " & _
                '"right('0' + cast (datepart(month,getdate()) as varchar),2) + " & _
                '"RIGHT('0'+ cast(datepart(ss,getdate()) as varchar),2)  ,3,8) +    '-' +  " & _
                '"RIGHT(cast(datepart(HH,getdate()) * datepart(millisecond, getdate()) as varchar),2) + " & _
                '"RIGHT(cast(datepart(MINUTE ,getdate()) as varchar),2) + '-' + " & _
                '"right('0' + cast (datepart(month,getdate()) as varchar),2) + " & _
                '"right('0' + cast (datepart(year,getdate()) as varchar),2) + '-'+ " & _
                '"RIGHT('0' + cast (datepart(HH, getdate()) as varchar),2) + " & _
                '"RIGHT('0' + cast (datepart(DAY , getdate()) as varchar),2) + '-' + " & _
                '"RIGHT('0' + cast(datepart(hh,getdate()) as varchar) ,2 ) +  " & _
                '"RIGHT('9' + CAST(datepart(millisecond,getdate()) * datepart(yy,getdate()) as varchar), 4) +  " & _
                '"right('5' + cast (datepart(HH,getdate()) * DATEPART(minute,getdate()) as varchar),2) +  " & _
                '"right('2' + cast (datepart(YY,getdate()) * DATEPART(MILLISECOND,getdate()) as varchar),4)), " & _
                '"'" & USERID & "', '" & USERID & "', 'NULL', '0', GETDATE()) " & _
                '"end " & _
                '"insert into WebFD.dbo.aspnet_UsersInRoles   " & _
                '" ( [UserId],[RoleId])   values   ((select UserId FROM [WebFD].[dbo].[aspnet_Users] where UserName = '" & txtSearchInfo.Text & "'),   '" & ddlAllRoles.SelectedValue & "' ) " & _
                '"end  "

            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "New Table section"
    Private Sub btnResetFredTableTab_Click(sender As Object, e As System.EventArgs) Handles btnResetFredTableTab.Click
        Try
            rblDataType.Items(0).Selected = False
            rblDataType.Items(1).Selected = False
            ddlAvilableDatabase.Text = ""
            ddlSchema.Text = ""
            ddlSelectTable.Text = ""
            ddlSelectColumn.Text = ""
            txtColumnAlias.Text = ""


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub LoadDatabases()
        Try
            SQL = "Select distinct dbName " & _
                "From DWH.DOC.FDDatabase " & _
                "order by 1  "
            ds = New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "Database")

            End Using

            ddlAvilableDatabase.DataSource = ds
            ddlAvilableDatabase.DataMember = "Database"
            ddlAvilableDatabase.DataTextField = "dbName"
            ddlAvilableDatabase.DataValueField = "dbName"
            ddlAvilableDatabase.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub lbSelectdB_Click(sender As Object, e As System.EventArgs) Handles lbSelectdB.Click
        Try

            LoadSchemas()
            ddlSchema.Visible = True
            lbSelectSchema.Visible = True
            lblschema.Visible = True

            ddlSelectTable.Visible = False
            lbSelectTable.Visible = False
            txtTableAlias.Visible = False
            ddlSelectColumn.Visible = False
            btnAddToFRED.Visible = False
            txtColumnAlias.Visible = False
            lblTable.Visible = False
            lbltable2.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadSchemas()

        Try
            SQL = "select distinct TABLE_CATALOG ,  TABLE_SCHEMA " & _
                "from [" & ddlAvilableDatabase.SelectedValue & "].INFORMATION_SCHEMA.TABLES " & _
                "Order by 2 "

            ds = New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "Schema")

            End Using
            ddlSchema.DataSource = ds
            ddlSchema.DataMember = "Schema"
            ddlSchema.DataTextField = "TABLE_SCHEMA"
            ddlSchema.DataValueField = "TABLE_SCHEMA"
            ddlSchema.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub
    Private Sub lbSelectSchema_Click(sender As Object, e As System.EventArgs) Handles lbSelectSchema.Click
        Try
            LoadTables()
            ddlSelectTable.Visible = True
            lbSelectTable.Visible = True
            txtTableAlias.Visible = True
            ddlSelectColumn.Visible = False
            btnAddToFRED.Visible = False
            txtColumnAlias.Visible = False
            lblTable.Visible = True
            lbltable2.Visible = True
            lblcolumn2.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub LoadTables()
        Try
            Dim TableType As String
            If rblDataType.Items(0).Selected = True Then
                TableType = "0"
            Else
                TableType = "1"
            End If

            SQL = "select distinct TABLE_NAME  " & _
            "from " & ddlAvilableDatabase.SelectedValue & ".INFORMATION_SCHEMA.TABLES  a " & _
            "where not exists (select distinct * from WebFD.FRED.AvailableTables b " & _
            "where b.TABLE_CATALOG = '" & ddlAvilableDatabase.SelectedValue & "' " & _
            "and b.TABLE_SCHEMA = '" & ddlSchema.SelectedValue & "' " & _
            "and b.TABLE_NAME = a.TABLE_NAME " & _
            "and b.FREDType = '" & TableType & "' ) " & _
            "and a.TABLE_SCHEMA = '" & ddlSchema.SelectedValue & "' " & _
            "order by 1 "

            ds = New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "tables")

            End Using
            ddlSelectTable.DataSource = ds
            ddlSelectTable.DataMember = "tables"
            ddlSelectTable.DataTextField = "TABLE_NAME"
            ddlSelectTable.DataValueField = "TABLE_NAME"
            ddlSelectTable.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub lbSelectTable_Click(sender As Object, e As System.EventArgs) Handles lbSelectTable.Click
        Try
            LoadColumns()
            ddlSelectColumn.Visible = True
            btnAddToFRED.Visible = True
            If txtTableAlias.Text = "" Then
                txtTableAlias.Text = ddlSelectTable.SelectedValue
            End If

            txtColumnAlias.Visible = True

            lblcolumn2.Visible = True
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub LoadColumns()
        Try
            SQL = "select distinct COLUMN_NAME " & _
             "from " & ddlAvilableDatabase.SelectedValue & ".INFORMATION_SCHEMA.COLUMNS " & _
             "where TABLE_CATALOG = '" & ddlAvilableDatabase.SelectedValue & "' " & _
             "and TABLE_SCHEMA  = '" & ddlSchema.SelectedValue & "' " & _
             "and TABLE_NAME = '" & ddlSelectTable.SelectedValue & "' " & _
             "order by 1 "

            ds = New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "COLUMNS")

            End Using
            ddlSelectColumn.DataSource = ds
            ddlSelectColumn.DataMember = "COLUMNS"
            ddlSelectColumn.DataTextField = "COLUMN_NAME"
            ddlSelectColumn.DataValueField = "COLUMN_NAME"
            ddlSelectColumn.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub btnAddToFRED_Click(sender As Object, e As System.EventArgs) Handles btnAddToFRED.Click
        Try
            Dim dr As SqlDataReader
            Dim TableID As String = ""
            Dim FREDType As String = "1"
            'Dim FREDHQ As String = "0"

            If ddlAvilableDatabase.SelectedValue <> "" And ddlSchema.SelectedValue <> "" _
                   And ddlSelectTable.SelectedValue <> "" And ddlSelectColumn.SelectedValue <> "" Then
                If rblDataType.Items(0).Selected = True Then
                    FREDType = "0"
                Else
                    FREDType = "1"
                End If
                'If rblFREDType.Items(0).Selected = True Then
                '    'FRED HQ
                '    FREDHQ = "1"
                'End If
                'If rblFREDType.Items(1).Selected = True Then
                '    'FRED MQ 
                '    FREDMQ = "1"
                'End If

                If txtTableAlias.Text = "" Then
                    txtTableAlias.Text = ddlSelectTable.SelectedValue
                End If
                If txtColumnAlias.Text = "" Then
                    txtColumnAlias.Text = ddlSelectColumn.SelectedValue
                End If

                SQL = "Insert into WebFD.FRED.AvailableTables " & _
                "(FREDType, TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, Table_Alias, " & _
                "VisibleOnWeb, Active  ) " & _
                "values " & _
                "(" & FREDType & ",'" & Replace(ddlAvilableDatabase.SelectedValue, "'", "''") & "', '" & Replace(ddlSchema.SelectedValue, "'", "''") & "', " & _
                "'" & Replace(ddlSelectTable.SelectedValue, "'", "''") & "', '" & Replace(txtTableAlias.Text, "'", "''") & "', " & _
                "1, 1 ) "


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    cmd.CommandTimeout = 86400
                    cmd.ExecuteNonQuery()

                    SQL = "select ID from WebFD.FRED.AvailableTables " & _
                    "where TABLE_CATALOG = '" & Replace(ddlAvilableDatabase.SelectedValue, "'", "''") & "' " & _
                    "and TABLE_SCHEMA = '" & Replace(ddlSchema.SelectedValue, "'", "''") & "' " & _
                    "and TABLE_NAME = '" & Replace(ddlSelectTable.SelectedValue, "'", "''") & "'    "
                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("ID")) Then
                            TableID = ""
                        Else : TableID = dr.Item("ID")
                        End If
                    End While
                    dr.Close()

                    SQL = "insert into WebFD.FRED.AvailableColumns " & _
                    "([TABLE_ID],[COLUMN_NAME],[Column_Alias],[VisibleOnWeb],[Active] " & _
                    ",[Costing],[sDefault],[FREDConnect], DataType, PHI, Financial, BO_Financial)  " & _
                    "(select " & TableID & ", COLUMN_NAME, COLUMN_NAME , 1, 1, 0, 0 , 0, DATA_TYPE, 0, 0, 0 " & _
                    "from " & Replace(ddlAvilableDatabase.SelectedValue, "'", "''") & ".INFORMATION_SCHEMA.COLUMNS " & _
                    "where  TABLE_CATALOG = '" & Replace(ddlAvilableDatabase.SelectedValue, "'", "''") & "' " & _
                    "and TABLE_SCHEMA  = '" & Replace(ddlSchema.SelectedValue, "'", "''") & "' " & _
                    "and TABLE_NAME = '" & Replace(ddlSelectTable.SelectedValue, "'", "''") & "' ) "

                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    SQL = " update WebFD.FRED.AvailableColumns set " & _
                        "FREDConnect = 1 " & _
                        "where COLUMN_NAME = '" & Replace(ddlSelectColumn.SelectedValue, "'", "''") & "'  "
                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                End Using

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
#End Region
#Region "Edit Table Section"
    'Private Sub LoadAvailableTables()
    '    Try
    '        'Select Case FREDType
    '        '    Case "HQ"
    '        SQL = " select distinct [TABLE_NAME] + CASE WHEN Active ='1' THEN ' - Active' else ' - Inactive' end as TABLE_NAME, [ID]  " & _
    '        "from WebFD.FRED.AvailableTables " & _
    '        "order by 1  "
    '        '    Case "MQ"
    '        'SQL = " select distinct [TABLE_NAME] + CASE WHEN Active ='1' THEN ' - Active' else ' - Inactive' end as TABLE_NAME, [ID]  " & _
    '        '"from WebFD.FRED.AvailableTables " & _
    '        '"where FREDMQ = 1  " & _
    '        '"order by 1  "
    '        '    Case Else
    '        'SQL = " select distinct [TABLE_NAME] + CASE WHEN Active ='1' THEN ' - Active' else ' - Inactive' end as TABLE_NAME, [ID]  " & _
    '        '"from WebFD.FRED.AvailableTables " & _
    '        '"order by 1  "
    '        'End Select

    '        ds = New DataSet

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
    '            da = New SqlDataAdapter(SQL, conn)
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            da.SelectCommand.CommandTimeout = 86400
    '            da.Fill(ds, "Tables")

    '        End Using

    '        ddlAvailableTables.DataSource = ds
    '        ddlAvailableTables.DataMember = "Tables"
    '        ddlAvailableTables.DataTextField = "TABLE_NAME"
    '        ddlAvailableTables.DataValueField = "ID"
    '        ddlAvailableTables.DataBind()

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub lbEditTable_Click(sender As Object, e As System.EventArgs) Handles lbEditTable.Click
    '    Try
    '        Dim dr As SqlDataReader
    '        If ddlAvailableTables.SelectedValue <> "" Then
    '            pnlEditTable.Visible = True

    '            rblTableType.Items(0).Selected = False
    '            rblTableType.Items(1).Selected = False
    '            rblTableVisibleStatus.Items(0).Selected = False
    '            rblTableVisibleStatus.Items(1).Selected = False
    '            rblEditTableActive.Items(0).Selected = False
    '            rblEditTableActive.Items(1).Selected = False

    '            SQL = " SELECT [ID],[FREDType],[TABLE_CATALOG],[TABLE_SCHEMA]" & _
    '            ",[TABLE_NAME],[Table_Alias],[VisibleOnWeb],[Active],[GridColor] " & _
    '            "from [WebFD].[FRED].[AvailableTables] " & _
    '            "where ID = '" & ddlAvailableTables.SelectedValue & "' "
    '            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
    '                cmd = New SqlCommand(SQL, conn)
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                dr = cmd.ExecuteReader
    '                While dr.Read
    '                    If IsDBNull(dr.Item("TABLE_NAME")) Then
    '                        lblEditTable.Text = ""
    '                    Else : lblEditTable.Text = dr.Item("TABLE_NAME") & "<br/>"
    '                    End If
    '                    If IsDBNull(dr.Item("FREDType")) Then
    '                        rblTableType.Items(0).Selected = False
    '                        rblTableType.Items(1).Selected = False
    '                    Else
    '                        If dr.Item("FREDType") = "0" Then
    '                            rblTableType.Items(0).Selected = True
    '                        Else
    '                            rblTableType.Items(1).Selected = True
    '                        End If
    '                    End If
    '                    If IsDBNull(dr.Item("VisibleOnWeb")) Then
    '                        rblTableVisibleStatus.Items(0).Selected = False
    '                        rblTableVisibleStatus.Items(1).Selected = False
    '                    Else
    '                        If dr.Item("VisibleOnWeb") = True Then
    '                            rblTableVisibleStatus.Items(0).Selected = True
    '                        Else
    '                            rblTableVisibleStatus.Items(1).Selected = True
    '                        End If
    '                    End If
    '                    If IsDBNull(dr.Item("Table_Alias")) Then
    '                        txtEditTableAlias.Text = lblEditTable.Text
    '                    Else : txtEditTableAlias.Text = dr.Item("Table_Alias")
    '                        lblEditTable.Text = lblEditTable.Text & "Alias: " & dr.Item("Table_Alias") & "<br/>" & "Table ID: " & dr.Item("ID")
    '                    End If
    '                    If IsDBNull(dr.Item("Active")) Then
    '                        rblEditTableActive.Items(0).Selected = False
    '                        rblEditTableActive.Items(1).Selected = False
    '                    Else
    '                        If dr.Item("Active") = True Then
    '                            rblEditTableActive.Items(0).Selected = True
    '                        Else
    '                            rblEditTableActive.Items(1).Selected = True
    '                        End If
    '                    End If
    '                    If IsDBNull(dr.Item("GridColor")) Then
    '                        txtEditTableColor.Text = ""
    '                    Else : txtEditTableColor.Text = dr.Item("GridColor")
    '                    End If
    '                End While
    '                dr.Close()

    '            End Using
    '            lblSelectedTableID.Text = ddlAvailableTables.SelectedValue

    '        Else : pnlEditTable.Visible = False
    '        End If
    '        lblUpdateMsg.Text = ""
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub lbSaveTableEdits_Click(sender As Object, e As System.EventArgs) Handles lbSaveTableEdits.Click
    '    Try
    '        Dim VisableStatus, ActiveStatus, FREDType As String
    '        If rblEditTableActive.Text = "Active" Then
    '            ActiveStatus = "True"
    '        Else
    '            ActiveStatus = "False "
    '        End If
    '        If rblTableVisibleStatus.Text = "Visible" Then
    '            VisableStatus = "True"
    '        Else
    '            VisableStatus = "false"
    '        End If
    '        If rblTableType.Text = "Encounter" Then
    '            FREDType = "0"
    '        Else
    '            FREDType = "1"
    '        End If
    '        If txtEditTableAlias.Text = "" Then
    '            txtEditTableAlias.Text = ddlAvailableTables.Text
    '        End If

    '        If lblEditTable.Text <> "" Then
    '            SQL = "Update WebFD.FRED.AvailableTables set " & _
    '                "FREDType = '" & FREDType & "', " & _
    '                "Table_Alias = '" & Replace(txtEditTableAlias.Text, "'", "''") & "',  " & _
    '                "VisibleOnWeb = '" & VisableStatus & "', " & _
    '                "Active = '" & ActiveStatus & "', GridColor = '" & txtEditTableColor.Text & "' " & _
    '                  "where ID = '" & ddlAvailableTables.SelectedValue & "' "
    '            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
    '                cmd = New SqlCommand(SQL, conn)
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteNonQuery()

    '            End Using
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub rblFREDEnvironment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFREDEnvironment.SelectedIndexChanged
    '    Try
    '        lblFredAvailableTables.Visible = True
    '        ddlAvailableTables.Visible = True
    '        LoadAvailableTables()

    '        lbEditTable.Visible = True
    '        lbEditTable.Visible = True

    '        pnlEditTable.Visible = False

    '    Catch ex As Exception

    '    End Try
    'End Sub
#End Region

    Private Sub btnUpdateTable_Click(sender As Object, e As System.EventArgs) Handles btnUpdateTable.Click
        Try
            Dim lbl As Label = gvTableOverview.SelectedRow.FindControl("lblgvTO1")
            Dim da As New SqlDataAdapter
            Dim da2 As New SqlDataAdapter

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                SQL = "Declare @SQLtxt as varchar(max) " & _
                "set @SQLtxt =   " & _
                "( select 'insert into WebFD.FRED.AvailableColumns  " & _
                "([TABLE_ID],[COLUMN_NAME],[Column_Alias],[VisibleOnWeb] " & _
                ",[Active],[Costing],[sDefault],[FREDConnect],[DataType])  " & _
                "(select ' + convert(varchar(max), ID)  + ', COLUMN_NAME, COLUMN_NAME, 1,1,0,0,0, DATA_TYPE  " & _
                "from '+ TABLE_CATALOG + '.INFORMATION_SCHEMA.COLUMNS a where TABLE_NAME = '+CHAR(39)   + TABLE_NAME + CHAR(39)+ ' and TABLE_SCHEMA = '+CHAR(39) + TABLE_SCHEMA + CHAR(39) + '   " & _
                "and  not exists (select COLUMN_NAME  from WebFD.FRED.AvailableColumns b  " & _
                "where a.COLUMN_NAME = b.COLUMN_NAME and TABLE_ID = " & lbl.Text & " and Active = 1)) " & _
                "update WebFD.FRED.AvailableColumns set Active = 0 " & _
                "where ID in (select ID from WebFD.FRED.AvailableColumns a where TABLE_ID = " & lbl.Text & _
                " and not exists (select COLUMN_NAME from ' + TABLE_CATALOG + '.INFORMATION_SCHEMA.COLUMNS b where TABLE_NAME = '" & _
                "+CHAR(39) + TABLE_NAME + CHAR(39) + ' and TABLE_SCHEMA = '+CHAR(39) + TABLE_SCHEMA + CHAR(39) + ' and a.COLUMN_NAME = b.COLUMN_NAME ))'" & _
                "from WebFD.FRED.AvailableTables " & _
                "where ID = " & lbl.Text & ") " & _
                "execute ( @SQLtxt  ) "

                Dim Count1 As String = "Declare @SQLtxt as varchar(max) " & _
                "set @SQLtxt =   " & _
                "( select 'select COLUMN_NAME " & _
                "from '+ TABLE_CATALOG + '.INFORMATION_SCHEMA.COLUMNS a where TABLE_NAME = '+CHAR(39)   + TABLE_NAME + CHAR(39)+ ' and TABLE_SCHEMA = '+CHAR(39) + TABLE_SCHEMA + CHAR(39) + '   " & _
                "and  not exists (select COLUMN_NAME  from WebFD.FRED.AvailableColumns b  " & _
                 "where a.COLUMN_NAME = b.COLUMN_NAME and TABLE_ID = " & lbl.Text & " and Active = 1) '" & _
                "from WebFD.FRED.AvailableTables " & _
                "where ID = " & lbl.Text & ") " & _
                "execute ( @SQLtxt  ) "

                Dim Count2 As String = "Declare @SQLtxt as varchar(max) " & _
                "set @SQLtxt =   " & _
                "( select 'select COLUMN_NAME from WebFD.FRED.AvailableColumns a where TABLE_ID = " & lbl.Text & _
                " and not exists (select COLUMN_NAME from ' + TABLE_CATALOG + '.INFORMATION_SCHEMA.COLUMNS b where TABLE_NAME = '" & _
                "+CHAR(39) + TABLE_NAME + CHAR(39) + ' and TABLE_SCHEMA = '+CHAR(39) + TABLE_SCHEMA + CHAR(39) + ' and a.COLUMN_NAME = b.COLUMN_NAME )'" & _
                "from WebFD.FRED.AvailableTables " & _
                "where ID = " & lbl.Text & ") " & _
                "execute ( @SQLtxt  ) "

                Dim dt As New DataTable
                Dim dt2 As New DataTable
                cmd = New SqlCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da = New SqlDataAdapter(Count1, conn)
                da2 = New SqlDataAdapter(Count2, conn)
                da.Fill(dt)
                gvColumnchange1.DataSource = dt
                gvColumnchange1.DataBind()
                da.Fill(dt2)
                gvColumnChange2.DataSource = dt2

                gvColumnChange2.DataBind()

                cmd.ExecuteNonQuery()

            End Using
            lblUpdateMsg.Text = "Columns Added: "
            ModalPopupExtender1.Show()



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub PopulateGridview()
        Try
            Dim sqlgv As String

            sqlgv = "SELECT t.[ID], c.ID as [columnID], [FREDType], [TABLE_CATALOG], [TABLE_SCHEMA], [TABLE_NAME], [Table_Alias], [COLUMN_NAME], [Column_Alias]" & _
          ",t.[VisibleOnWeb], t.[Active], t.[Costing], t.[PHI], t.[Financial], t.[BO_Financial], [GridColor] " & _
          "  FROM [WebFD].[FRED].[AvailableTables] t " & _
            "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
            "order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME "


            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sqlgv, conn2)
                da.Fill(dt)
                gvTableOverview.DataSource = dt
                'gvTableOverview.DataMember = "DEMO"
                gvTableOverview.DataBind()
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub gvTableOverview_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvTableOverview.RowCancelingEdit
        Try
            gvTableOverview.EditIndex = -1
            PopulateGridview()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvTableOverview_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTableOverview.RowDataBound


        Try

            Dim drv As DataRowView
            drv = e.Row.DataItem

            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowState - DataControlRowState.Edit >= 0 Then

                    Dim dp As DropDownList
                    dp = e.Row.FindControl("ddlgvTOFredConnect")
                    Dim dt As DataTable
                    dt = load_FredConnect(e.Row.RowIndex)

                    dp.DataSource = dt
                    dp.DataTextField = "Column_Name"
                    dp.DataValueField = "ID"
                    dp.DataBind()

                    dp.SelectedValue = drv(3).ToString()

                    Dim cblgvTOWebVis As CheckBox = e.Row.FindControl("cblgvTOWebVis")
                    Dim cblgvTOActive As CheckBox = e.Row.FindControl("cblgvTOActive")
                    Dim cblgvTOCosting As CheckBox = e.Row.FindControl("cblgvTOCosting")
                    Dim cblgvTOPHI As CheckBox = e.Row.FindControl("cblgvTOPHI")
                    Dim cblgvTOFinancial As CheckBox = e.Row.FindControl("cblgvTOFinancial")
                    Dim cblgvTOBO_Financial As CheckBox = e.Row.FindControl("cblgvTOBO_Financial")

                    cblgvTOWebVis.Checked = If(IsDBNull(drv(9)), False, drv(9))
                    cblgvTOActive.Checked = If(IsDBNull(drv(10)), False, drv(10))
                    cblgvTOCosting.Checked = If(IsDBNull(drv(11)), False, drv(11))
                    cblgvTOPHI.Checked = If(IsDBNull(drv(12)), False, drv(12))
                    cblgvTOFinancial.Checked = If(IsDBNull(drv(13)), False, drv(13))
                    cblgvTOBO_Financial.Checked = If(IsDBNull(drv(14)), False, drv(14))



                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Function load_FredConnect(id As Integer)
        Try
            Dim sqlddl As String


            sqlddl = "        select  Column_Name, ID from WebFD.FRED.AvailableColumns where TABLE_ID = (select ID from (" & _
      "SELECT t.[ID], Row_Number() over (order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME ) as rn " & _
            "FROM [WebFD].[FRED].[AvailableTables] t " & _
            "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
            ") x where rn = " & CStr(id.ToString + 1) & ") order by FREDConnect desc, Column_Name asc"


            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sqlddl, conn)
                da.Fill(dt)
                Return dt
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Function

    Private Sub gvTableOverview_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvTableOverview.RowEditing
        Try
            gvTableOverview.EditIndex = e.NewEditIndex
            PopulateGridview()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvTableOverview_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvTableOverview.RowUpdating
        Try
            Dim ddl As DropDownList = gvTableOverview.Rows(e.RowIndex).FindControl("ddlgvTOFredConnect")
            Dim rblgvTOFREDType As RadioButtonList = gvTableOverview.Rows(e.RowIndex).FindControl("rblgvTOFREDType")
            Dim txtgvTOAlias As TextBox = gvTableOverview.Rows(e.RowIndex).FindControl("txtgvTOAlias")
            Dim cblgvTOWebVis As CheckBox = gvTableOverview.Rows(e.RowIndex).FindControl("cblgvTOWebVis")
            Dim cblgvTOActive As CheckBox = gvTableOverview.Rows(e.RowIndex).FindControl("cblgvTOActive")
            Dim cblgvTOCosting As CheckBox = gvTableOverview.Rows(e.RowIndex).FindControl("cblgvTOCosting")
            Dim cblgvTOPHI As CheckBox = gvTableOverview.Rows(e.RowIndex).FindControl("cblgvTOPHI")
            Dim cblgvTOFinancial As CheckBox = gvTableOverview.Rows(e.RowIndex).FindControl("cblgvTOFinancial")
            Dim cblgvTOBO_Financial As CheckBox = gvTableOverview.Rows(e.RowIndex).FindControl("cblgvTOBO_Financial")
            Dim txtgvTOGridColor As TextBox = gvTableOverview.Rows(e.RowIndex).FindControl("txtgvTOGridColor")

            Dim str As String = "Update [WebFD].[FRED].[AvailableColumns] set FREDConnect = 0 where TABLE_ID =  (select ID from (" & _
      "SELECT t.[ID], Row_Number() over (order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME ) as rn " & _
            "FROM [WebFD].[FRED].[AvailableTables] t " & _
            "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
            ") x where rn = " & CStr(e.RowIndex + 1) & ") "

            Dim str2 As String = " Update [WebFD].[FRED].[AvailableColumns] set FREDConnect = 1 where TABLE_ID =  (select ID from (" & _
      "SELECT t.[ID], Row_Number() over (order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME ) as rn " & _
            "FROM [WebFD].[FRED].[AvailableTables] t " & _
            "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
            ") x where rn = " & CStr(e.RowIndex + 1) & ") and ID = " & ddl.SelectedValue.ToString

            Dim checkGridColor As String
            If Trim(txtgvTOGridColor.Text.ToString) = "" Then
                checkGridColor = ", GridColor = Null "
            Else
                checkGridColor = ", GridColor = '" & Replace(txtgvTOGridColor.Text.ToString, "'", "''") & "'"
            End If

            Dim tableupdate As String = "Update [WebFD].[FRED].[AvailableTables] set " & _
                "FREDTYPE = " & rblgvTOFREDType.SelectedValue.ToString & _
                ", Table_Alias = '" & Replace(txtgvTOAlias.Text.ToString, "'", "''") & "'" & _
                ", VisibleOnWeb = " & If(cblgvTOWebVis.Checked = True, "1", "0") & _
                ", Active = " & If(cblgvTOActive.Checked = True, "1", "0") & _
                ", Costing = " & If(cblgvTOCosting.Checked = True, "1", "0") & _
                ", PHI = " & If(cblgvTOPHI.Checked = True, "1", "0") & _
                ", Financial = " & If(cblgvTOFinancial.Checked = True, "1", "0") & _
                ", BO_Financial = " & If(cblgvTOBO_Financial.Checked = True, "1", "0") & _
                checkGridColor & _
                " where ID = (select ID from (" & _
                "SELECT t.[ID], Row_Number() over (order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME ) as rn " & _
                "FROM [WebFD].[FRED].[AvailableTables] t " & _
                "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
                ") x where rn = " & CStr(e.RowIndex + 1) & ")"



            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)

                Dim cmd2 As SqlCommand
                Dim cmd3 As SqlCommand
                cmd = New SqlCommand(str, conn)
                cmd2 = New SqlCommand(str2, conn)
                cmd3 = New SqlCommand(tableupdate, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                cmd2.ExecuteNonQuery()
                cmd3.ExecuteNonQuery()

            End Using

            gvTableOverview.EditIndex = -1
            PopulateGridview()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvTableOverview_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvTableOverview.SelectedIndexChanged
        Try


            PopulategvEditColumns()

            For Each colorrow As GridViewRow In gvTableOverview.Rows
                colorrow.BackColor = Drawing.Color.White
            Next

            gvTableOverview.SelectedRow.BackColor = Drawing.Color.LightSteelBlue
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub PopulategvEditColumns()

        Try
            Dim sqlgv As String

            sqlgv = "SELECT ID, TABLE_ID, COLUMN_NAME, Column_Alias, VisibleOnWeb, Active, Costing, PHI, Financial, BO_Financial, sDefault, FREDConnect FROM FRED.AvailableColumns WHERE TABLE_ID = " & _
            "(select ID from (" & _
                "SELECT t.[ID], Row_Number() over (order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME ) as rn " & _
                "FROM [WebFD].[FRED].[AvailableTables] t " & _
                "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
                ") x where rn = " & CStr(gvTableOverview.SelectedIndex + 1) & ")"


            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sqlgv, conn2)
                da.Fill(dt)
                gvEditColumns.DataSource = dt
                'gvTableOverview.DataMember = "DEMO"
                gvEditColumns.DataBind()
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvEditColumns_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvEditColumns.PageIndexChanging
        Try
            PopulategvEditColumns()
            gvEditColumns.PageIndex = e.NewPageIndex
            gvEditColumns.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvEditColumns_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvEditColumns.RowCancelingEdit
        Try
            gvEditColumns.EditIndex = -1
            PopulategvEditColumns()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEditColumns_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEditColumns.RowDataBound
        Try
            Dim drv As DataRowView
            drv = e.Row.DataItem

            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowState - DataControlRowState.Edit >= 0 Then

                    Dim chbgvECVisible As CheckBox = e.Row.FindControl("cblgvECVisibleOnWeb")
                    Dim cblgvECActive As CheckBox = e.Row.FindControl("cblgvECActive")
                    Dim chbgvECCosting As CheckBox = e.Row.FindControl("cblgvECCosting")
                    Dim chbgvECPHI As CheckBox = e.Row.FindControl("cblgvECPHI")
                    Dim chbgvECFinancial As CheckBox = e.Row.FindControl("cblgvECFinancial")
                    Dim chbgvECBO_Financial As CheckBox = e.Row.FindControl("cblgvECBO_Financial")
                    Dim cblgvECsDefault As CheckBox = e.Row.FindControl("cblgvECsDefault")

                    chbgvECVisible.Checked = If(IsDBNull(drv(4)), False, drv(4))
                    cblgvECActive.Checked = If(IsDBNull(drv(5)), False, drv(5))
                    chbgvECCosting.Checked = If(IsDBNull(drv(6)), False, drv(6))
                    chbgvECPHI.Checked = If(IsDBNull(drv(7)), False, drv(7))
                    chbgvECFinancial.Checked = If(IsDBNull(drv(8)), False, drv(8))
                    chbgvECBO_Financial.Checked = If(IsDBNull(drv(9)), False, drv(9))
                    cblgvECsDefault.Checked = If(IsDBNull(drv(10)), False, drv(10))



                End If
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub gvEditColumns_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvEditColumns.RowEditing
        Try
            gvEditColumns.EditIndex = e.NewEditIndex
            PopulategvEditColumns()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEditColumns_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvEditColumns.RowUpdating

        Try
            Dim selectedRow As GridViewRow = gvEditColumns.Rows(CInt(e.RowIndex))
            Dim txtgvECAlias As TextBox = gvEditColumns.Rows(e.RowIndex).FindControl("txtgvECAlias")
            Dim chbgvECVisible As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECVisibleOnWeb")
            Dim chbgvECCosting As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECCosting")
            Dim chbgvECPHI As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECPHI")
            Dim chbgvECFinancial As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECFinancial")
            Dim chbgvECBO_Financial As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECBO_Financial")
            Dim cblgvECActive As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECActive")
            Dim cblgvECsDefault As CheckBox = gvEditColumns.Rows(e.RowIndex).FindControl("cblgvECsDefault")
            Dim txt1 As String = selectedRow.Cells(0).Text

            Dim columnupdate As String = "Update FRED.AvailableColumns SET Column_Alias = '" & Replace(txtgvECAlias.Text.ToString, "'", "''") & "', " & _
                "VisibleOnWeb = " & If(chbgvECVisible.Checked = True, "1", "0") & ", " & _
                "Active = " & If(cblgvECActive.Checked = True, "1", "0") & ", " & _
                "Costing = " & If(chbgvECCosting.Checked = True, "1", "0") & ", " & _
                "PHI = " & If(chbgvECPHI.Checked = True, "1", "0") & ", " & _
                "Financial = " & If(chbgvECFinancial.Checked = True, "1", "0") & ", " & _
                "BO_Financial = " & If(chbgvECBO_Financial.Checked = True, "1", "0") & ", " & _
                "sDefault = " & If(cblgvECsDefault.Checked = True, "1", "0") & " " & _
                "WHERE ID = " & txt1

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)

                Dim cmd3 As SqlCommand
                cmd3 = New SqlCommand(columnupdate, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd3.ExecuteNonQuery()


            End Using

            gvEditColumns.EditIndex = -1
            PopulategvEditColumns()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvFredFavorites_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvFredFavorites.SelectedIndexChanged

        Try

            Dim sql As String = "declare @ID int = " & gvFredFavorites.SelectedRow.Cells(1).Text & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.TransTableID = trans.ID and ff.IsEncounter = 1 " & _
    "join FRED.AvailableColumns cols on TransTableColumns like '% ' + convert(varchar,cols.ID) + ' %' " & _
    "where ff.ID = @ID " & _
    "        union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Table1ID = trans.ID " & _
    "join FRED.AvailableColumns cols on Table1Columns like '% ' + convert(varchar,cols.ID) + ' %' " & _
    "where ff.ID = @ID " & _
    "union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Table2ID = trans.ID " & _
    "join FRED.AvailableColumns cols on Table2Columns like '% ' + convert(varchar,cols.ID) + ' %' " & _
    "where ff.ID = @ID " & _
    "union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Table3ID = trans.ID " & _
    "join FRED.AvailableColumns cols on Table3Columns like '% ' + convert(varchar,cols.ID) + ' %' " & _
    "where ff.ID = @ID " & _
    "union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Table4ID = trans.ID " & _
    "join FRED.AvailableColumns cols on Table4Columns like '% ' + convert(varchar,cols.ID) + ' %' " & _
    "where ff.ID = @ID "

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sql, conn2)
                da.Fill(dt)
                gvFredFavoritesColumns.DataSource = dt
                'gvTableOverview.DataMember = "DEMO"
                gvFredFavoritesColumns.DataBind()
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

        Try

            Dim sql As String = "declare @ID int = " & gvFredFavorites.SelectedRow.Cells(1).Text & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Filter1TID = trans.ID " & _
    "join FRED.AvailableColumns cols on Filter1DDColumnID = cols.ID " & _
    "where ff.ID = @ID " & _
    "        union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Filter2TID = trans.ID " & _
    "join FRED.AvailableColumns cols on Filter2DDColumnID = cols.ID " & _
    "where ff.ID = @ID " & _
    "union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Filter3TID = trans.ID " & _
    "join FRED.AvailableColumns cols on Filter3DDColumnID = cols.ID " & _
    "where ff.ID = @ID " & _
    "union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Filter4TID = trans.ID " & _
    "join FRED.AvailableColumns cols on Filter4DDColumnID = cols.ID " & _
    "where ff.ID = @ID " & _
    "union " & _
    "select trans.TABLE_SCHEMA, trans.Table_Alias, cols.ID, cols.Column_Alias from WebFD.FRED.FredFavorites ff " & _
    "join FRED.AvailableTables trans on ff.Filter5TID = trans.ID " & _
    "join FRED.AvailableColumns cols on Filter5DDColumnID = cols.ID " & _
    "where ff.ID = @ID "

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sql, conn2)
                da.Fill(dt)
                gvFredFavoritesFilters.DataSource = dt
                gvFredFavoritesFilters.DataBind()
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub OkButton_Click(sender As Object, e As System.EventArgs) Handles OkButton.Click
        Try
            ModalPopupExtender1.Hide()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub


    Private Sub gvEditColumns_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvEditColumns.Sorting
        Try
            Dim s As String

            If ViewState("SortOrder").ToString() = "asc" Then
                s = "desc"
                ViewState("SortOrder") = "desc"
            Else
                s = "asc"
                ViewState("SortOrder") = "asc"
            End If

            BindgvEditColumns(e.SortExpression, s)
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub BindgvEditColumns(e As String, s As String)

        Try
            Dim sqlgv As String

            sqlgv = "SELECT ID, TABLE_ID, COLUMN_NAME, Column_Alias, VisibleOnWeb, Active, Costing, PHI, Financial, BO_Financial, sDefault, FREDConnect FROM FRED.AvailableColumns WHERE TABLE_ID = " & _
            "(select ID from (" & _
                "SELECT t.[ID], Row_Number() over (order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME ) as rn " & _
                "FROM [WebFD].[FRED].[AvailableTables] t " & _
                "left join WebFD.FRED.AvailableColumns c on TABLE_ID = t.ID and c.FREDConnect = 1 " & _
                ") x where rn = " & CStr(gvTableOverview.SelectedIndex + 1) & ")"


            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sqlgv, conn2)
                da.Fill(dt)
            End Using

            Dim mydataview As DataView

            mydataview = dt.DefaultView
            mydataview.Sort = String.Format("{0} {1}", e, s)

            gvEditColumns.DataSource = mydataview
            gvEditColumns.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlFREDColumns_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDColumns.SelectedIndexChanged
        Try
            If ddlFREDLookupColumns.Enabled = True Then
                txtLUJoinString.Text = "LookUpLink.[" & ddlFREDLookupColumns.SelectedItem.Text & "] = FREDLink.[" & ddlFREDColumns.SelectedItem.Text & "]"
                btnAddConnection.Enabled = True
            Else
                txtLUJoinString.Text = ""
                btnAddConnection.Enabled = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlFREDLookupColumns_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDLookupColumns.SelectedIndexChanged

        Try
            If ddlFREDColumns.Enabled = True Then
                txtLUJoinString.Text = "LookUpLink.[" & ddlFREDColumns.SelectedItem.Text & "] = FREDLink.[" & ddlFREDColumns.SelectedItem.Text & "]"
                btnAddConnection.Enabled = True
            Else
                txtLUJoinString.Text = ""
                btnAddConnection.Enabled = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlFREDLookupTables_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDLookupTables.SelectedIndexChanged

        Try
            If ddlFREDLookupTables.SelectedIndex > 0 Then
                ddlFREDLookupColumns.Enabled = True
            Else
                ddlFREDLookupColumns.Enabled = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlFREDTables_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDTables.SelectedIndexChanged
        Try
            If ddlFREDTables.SelectedIndex > 0 Then
                ddlFREDColumns.Enabled = True
            Else
                ddlFREDLookupColumns.Enabled = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnAddConnection_Click(sender As Object, e As System.EventArgs) Handles btnAddConnection.Click

        Try
            Dim s As String = "Insert into [WebFD].[FRED].[LookupConnections] ([FREDTable_ID] " & _
          " ,[LUTable_ID] " & _
          " ,[FREDColumn_ID] " & _
          " ,[LUColumn_ID] " & _
          " ,[JoinString]) Values (" & ddlFREDTables.SelectedValue.ToString & ", " & ddlFREDLookupTables.SelectedValue.ToString & _
          ", " & ddlFREDColumns.SelectedValue.ToString & ", " & ddlFREDLookupColumns.SelectedValue.ToString & _
          ", '" & Replace(txtLUJoinString.Text, "'", "''") & "')"


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(s, conn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try



    End Sub

    Sub PopulateGVCurrentConnections()
        Try
            Dim sqlgv As String

            sqlgv = "SELECT conn.[ID] ,[FREDTable_ID] ,at.Table_NAME as FREDTable_Name ,[LUTable_ID] , lut.TABLE_NAME as LUTable_Name ,[FREDColumn_ID] " & _
                ",ac.COLUMN_NAME as FREDColumn_Name ,[LUColumn_ID] ,luc.COLUMN_NAME as LUColumn_Name ,[JoinString] FROM [WebFD].[FRED].[LookupConnections] conn " & _
                " join WebFD.FRED.LookUpTables lut on conn.LUTable_ID = lut.ID " & _
                " join WebFD.FRED.AvailableTables at on conn.FREDTable_ID = at.ID " & _
                " join WebFD.FRED.AvailableColumns ac on conn.FREDColumn_ID = ac.ID " & _
                " join WebFD.FRED.LookUpColumns luc on conn.LUColumn_ID = luc.ID "


            Dim da As New SqlDataAdapter
            Dim dt As New DataTable

            Using conn2 As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                da = New SqlDataAdapter(sqlgv, conn2)
                da.Fill(dt)
                gvCurrentConnections.DataSource = dt
                'gvTableOverview.DataMember = "DEMO"
                gvCurrentConnections.DataBind()
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub gvCurrentConnections_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvCurrentConnections.RowCancelingEdit
        Try
            gvCurrentConnections.EditIndex = -1
            PopulateGVCurrentConnections()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCurrentConnections_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCurrentConnections.RowDataBound

        Try

            Dim drv As DataRowView
            drv = e.Row.DataItem

            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowState - DataControlRowState.Edit >= 0 Then

                    Dim dp As DropDownList
                    dp = e.Row.FindControl("ddlgvCCFTNAME")
                    Dim dp2 As DropDownList
                    dp2 = e.Row.FindControl("ddlgvCCLTNAME")
                    Dim dp3 As DropDownList
                    dp3 = e.Row.FindControl("ddlgvCCFCNAME")
                    Dim dp4 As DropDownList
                    dp4 = e.Row.FindControl("ddlgvCCLCNAME")

                    Dim dt As New DataTable
                    Dim dt2 As New DataTable
                    Dim dt3 As New DataTable
                    Dim dt4 As New DataTable


                    Try
                        Dim sqlddl1 As String
                        Dim sqlddl2 As String
                        Dim sqlddl3 As String
                        Dim sqlddl4 As String

                        sqlddl1 = "select TABLE_NAME, ID from WebFD.FRED.AvailableTables  order by FREDType, Table_Alias"
                        sqlddl2 = "select TABLE_NAME, ID from WebFD.FRED.LookupTables  order by Table_Alias"
                        sqlddl3 = "select COLUMN_NAME, ID from WebFD.FRED.AvailableColumns where TABLE_ID = " & drv(1).ToString() & " order by Active, VisibleOnWeb, COLUMN_NAME"
                        sqlddl4 = "select COLUMN_NAME, ID from WebFD.FRED.LookUpColumns where TABLE_ID = " & drv(3).ToString() & " order by Active, VisibleOnWeb, COLUMN_NAME"

                        Dim da As New SqlDataAdapter
                        Dim da2 As New SqlDataAdapter
                        Dim da3 As New SqlDataAdapter
                        Dim da4 As New SqlDataAdapter


                        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                            da = New SqlDataAdapter(sqlddl1, conn)
                            da.Fill(dt)
                            da2 = New SqlDataAdapter(sqlddl2, conn)
                            da2.Fill(dt2)
                            da3 = New SqlDataAdapter(sqlddl3, conn)
                            da3.Fill(dt3)
                            da4 = New SqlDataAdapter(sqlddl4, conn)
                            da4.Fill(dt4)
                        End Using


                    Catch ex As Exception
                        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    End Try

                    gvCurrentConnections.SelectedIndex = CInt(e.Row.RowIndex)

                    dp.DataSource = dt
                    dp.DataTextField = "TABLE_NAME"
                    dp.DataValueField = "ID"
                    dp.DataBind()

                    dp2.DataSource = dt2
                    dp2.DataTextField = "TABLE_NAME"
                    dp2.DataValueField = "ID"
                    dp2.DataBind()

                    dp3.DataSource = dt3
                    dp3.DataTextField = "COLUMN_NAME"
                    dp3.DataValueField = "ID"
                    dp3.DataBind()

                    dp4.DataSource = dt4
                    dp4.DataTextField = "COLUMN_NAME"
                    dp4.DataValueField = "ID"
                    dp4.DataBind()

                    dp.SelectedValue = drv(1).ToString()
                    dp2.SelectedValue = drv(3).ToString()
                    dp3.SelectedValue = drv(5).ToString()
                    dp4.SelectedValue = drv(7).ToString()

                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try



    End Sub
    'Sub GetDataforCCFCList(sender As Object, e As System.EventArgs)
    '    Dim dp3 As DropDownList = sender
    '    Dim row2 As GridViewRow = dp3.NamingContainer

    '    Dim dt3 As New DataTable
    '    Dim sqlddl3 As String
    '    sqlddl3 = "select COLUMN_NAME, ID from WebFD.FRED.AvailableColumns where TABLE_ID = " & drv(1).ToString() & " order by Active, VisibleOnWeb, COLUMN_NAME"
    '    Dim da3 As New SqlDataAdapter
    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
    '        da3 = New SqlDataAdapter(sqlddl3, conn)
    '        da3.Fill(dt3)
    '    End Using

    '    dp3.DataSource = dt3
    '    dp3.DataTextField = "COLUMN_NAME"
    '    dp3.DataValueField = "ID"
    '    dp3.DataBind()

    'End Sub
    Sub DropDownListTesting(sender As Object, e As System.EventArgs)

        Try

            Dim dp3 As DropDownList
            dp3 = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCFCNAME")

            Dim dt3 As New DataTable

            Dim ddl As DropDownList = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCFTNAME")
            Dim ddlLU As DropDownList = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCLCNAME")
            Dim txt As TextBox = gvCurrentConnections.SelectedRow.FindControl("txtgvCCJoinString")

            Try

                Dim sqlddl3 As String

                sqlddl3 = "select COLUMN_NAME, ID from WebFD.FRED.AvailableColumns where TABLE_ID = " & ddl.SelectedValue.ToString & " order by Active, VisibleOnWeb, COLUMN_NAME"

                Dim da3 As New SqlDataAdapter


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                    da3 = New SqlDataAdapter(sqlddl3, conn)
                    da3.Fill(dt3)
                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try


            dp3.DataSource = dt3
            dp3.DataTextField = "COLUMN_NAME"
            dp3.DataValueField = "ID"
            dp3.DataBind()

            txt.Text = "LookUpLink.[" & ddlLU.SelectedItem.Text & "] = FREDLink.[" & dp3.SelectedItem.Text & "]"
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub
    Sub ddlgvCCLTNAME_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        Try

            Dim dp3 As DropDownList
            dp3 = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCLCNAME")

            Dim dt3 As New DataTable

            Dim ddl As DropDownList = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCLTNAME")
            Dim ddlLink As DropDownList = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCFCNAME")
            Dim txt As TextBox = gvCurrentConnections.SelectedRow.FindControl("txtgvCCJoinString")

            Try

                Dim sqlddl3 As String

                sqlddl3 = "select COLUMN_NAME, ID from WebFD.FRED.LookUpColumns where TABLE_ID = " & ddl.SelectedValue.ToString & " order by Active, VisibleOnWeb, COLUMN_NAME"

                Dim da3 As New SqlDataAdapter


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)
                    da3 = New SqlDataAdapter(sqlddl3, conn)
                    da3.Fill(dt3)
                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try


            dp3.DataSource = dt3
            dp3.DataTextField = "COLUMN_NAME"
            dp3.DataValueField = "ID"
            dp3.DataBind()

            txt.Text = "LookUpLink.[" & dp3.SelectedItem.Text & "] = FREDLink.[" & ddlLink.SelectedItem.Text & "]"

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub ddlgvCCCol_SelectedIndexChanged()

        Try
            Dim ddl As DropDownList = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCLCNAME")
            Dim ddlLink As DropDownList = gvCurrentConnections.SelectedRow.FindControl("ddlgvCCFCNAME")
            Dim txt As TextBox = gvCurrentConnections.SelectedRow.FindControl("txtgvCCJoinString")

            txt.Text = "LookUpLink.[" & ddl.SelectedItem.Text & "] = FREDLink.[" & ddlLink.SelectedItem.Text & "]"
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvCurrentConnections_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvCurrentConnections.RowEditing
        Try
            gvCurrentConnections.EditIndex = e.NewEditIndex
            PopulateGVCurrentConnections()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCurrentConnections_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvCurrentConnections.RowUpdating
        Try
            Dim ddlgvCCFTNAME As DropDownList = gvCurrentConnections.Rows(e.RowIndex).FindControl("ddlgvCCFTNAME")
            Dim ddlgvCCLTNAME As DropDownList = gvCurrentConnections.Rows(e.RowIndex).FindControl("ddlgvCCLTNAME")
            Dim ddlgvCCFCNAME As DropDownList = gvCurrentConnections.Rows(e.RowIndex).FindControl("ddlgvCCFCNAME")
            Dim ddlgvCCLCNAME As DropDownList = gvCurrentConnections.Rows(e.RowIndex).FindControl("ddlgvCCLCNAME")

            Dim txtgvCCJoinString As TextBox = gvCurrentConnections.Rows(e.RowIndex).FindControl("txtgvCCJoinString")
            Dim lbl As Label = gvCurrentConnections.Rows(e.RowIndex).FindControl("lblgvCCId")

            Dim s As String = "Update [WebFD].[FRED].[LookupConnections] set [FREDTable_ID] = " & ddlgvCCFTNAME.SelectedValue.ToString & ", " & _
            " [LUTable_ID] = " & ddlgvCCLTNAME.SelectedValue.ToString & _
            " ,[FREDColumn_ID] = " & ddlgvCCFCNAME.SelectedValue.ToString & _
            " ,[LUColumn_ID] = " & ddlgvCCLCNAME.SelectedValue.ToString & _
            " ,[JoinString] =  '" & Replace(txtgvCCJoinString.Text, "'", "''") & "' where ID = " & lbl.Text


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)

                Dim cmd As SqlCommand

                cmd = New SqlCommand(s, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End Using

            gvCurrentConnections.EditIndex = -1
            PopulateGVCurrentConnections()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCurrentConnections_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvCurrentConnections.SelectedIndexChanged
        Try

            For Each colorrow As GridViewRow In gvCurrentConnections.Rows
                colorrow.BackColor = Drawing.Color.White
            Next

            gvCurrentConnections.SelectedRow.BackColor = Drawing.Color.LightSteelBlue
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class