Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Net.WebUtility

Imports FinanceWeb.WebFinGlobal

Public Class financehelpdesk
    Inherits System.Web.UI.Page
    Private Shared superadmin As Integer = 0
    Private Shared admin As Integer = 0
    Private Shared logged As Integer = 0
    Private Shared AdminView As New DataView
    Private Shared Adminmap As String
    Private Shared Admindir As Integer
    Private Shared SearchView As New DataView
    Private Shared Searchdir As Integer
    Private Shared Searchmap As String
    Private Shared dv As New DataView
    Dim acc1 As Integer = 0
    Dim acc2 As Integer = 0
    Dim acc3 As Integer = 0

    Dim mimic = "hp999853"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        If IsPostBack Then

            'ColorCheck()
            'test

        Else



            lblUsrLogin.Text = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")

            If lblUsrLogin.Text = "" Then
                tpUserProfile.Visible = False
            End If

            If Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") = "cw996788" Then
                superadmin = 0
                admin = 1
                logged = 1
            ElseIf Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") = "mf995052" Then
                superadmin = 0
                admin = 1
                logged = 1
            ElseIf Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") = "e213842" Then
                superadmin = 0
                admin = 1
                logged = 1
            Else

                Dim adminsql As String = "SELECT count(*) FROM [WebFD].[FinanceHelpDesk].[tblUsers] where uid = '" & _
                    Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and IsRep = 1 and Active = 1"

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd = New SqlCommand(adminsql, conn)
                    cmd.CommandTimeout = 86400
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    admin = cmd.ExecuteScalar

                End Using

                Dim loggedsql As String = "SELECT count(*) FROM [WebFD].[FinanceHelpDesk].[tblUsers] where uid = '" & _
                    Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'  and Active = 1 "

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd = New SqlCommand(loggedsql, conn)
                    cmd.CommandTimeout = 86400
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    logged = cmd.ExecuteScalar

                End Using

            End If

            If logged = 0 Then
                tcFinanceHelpDesk.ActiveTabIndex = 4
                tpFinHelpMain.Enabled = False

            End If

            Dim usersUpdatesql As String = "Select isnull([uid], '') + case when fname is null then '' when fname = 'NULL' then '' else  ' (' + isnull([fname], '') + ')' end as usr, " & _
          "uid from WebFD.FinanceHelpDesk.tblUsers where Active = 1 order by fname"


            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(usersUpdatesql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlUpdateUser.DataSource = ds
            ddlUpdateUser.DataValueField = "uid"
            ddlUpdateUser.DataTextField = "usr"
            ddlUpdateUser.DataBind()

            Dim catsql As String

            catsql = "select * from WebFD.FinanceHelpDesk.categories where Active = 1 order by cname"

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(catsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlSelectCategory.DataSource = ds
            ddlSelectCategory.DataValueField = "category_id"
            ddlSelectCategory.DataTextField = "cname"
            ddlSelectCategory.DataBind()




            If admin > 0 Then

                Dim adminlist As String = "select distinct ISNULL(fname, convert(varchar, rep)) as repname, u.uid as rep  from WebFD.FinanceHelpDesk.problems p " & _
                 "left join WebFD.FinanceHelpDesk.tblUsers u on p.rep = u.sid  " & _
                 "where p.status <> 100 " & _
                 "union " & _
                 "select distinct ISNULL(fname, convert(varchar, rep_id)) as repname, u.uid as rep " & _
                 "from WebFD.FinanceHelpDesk.categories " & _
                 "join WebFD.FinanceHelpDesk.tblUsers u on rep_id = u.sid and u.Active = 1 "

                ds.Clear()
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New SqlCommand(adminlist, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds, "OData")

                End Using


                ddlViewOtherCases.DataSource = ds
                ddlViewOtherCases.DataValueField = "rep"
                ddlViewOtherCases.DataTextField = "repname"
                ddlViewOtherCases.DataBind()

                Try
                    ddlViewOtherCases.SelectedValue = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
                Catch ex As Exception
                    ddlViewOtherCases.SelectedIndex = -1
                End Try

                catsql = "select * from WebFD.FinanceHelpDesk.categories where Active = 1 order by cname"

                ds.Clear()
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New SqlCommand(catsql, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds, "OData")

                End Using

                'ddlSelectCategory.DataSource = ds
                'ddlSelectCategory.DataValueField = "category_id"
                'ddlSelectCategory.DataTextField = "cname"
                'ddlSelectCategory.DataBind()

                ddlUpdateCategory.DataSource = ds
                ddlUpdateCategory.DataValueField = "category_id"
                ddlUpdateCategory.DataTextField = "cname"
                ddlUpdateCategory.DataBind()

                tpOpenCases.Visible = True
                tpAdministrative.Visible = True
                populateOCGridview()
                adminrow.Visible = True
                adminrow2.Visible = True
                adminrow3.Visible = True
                ddlSelectPriority.Visible = True
                ddlSelectStatus.Visible = True
                txtTimeSpent.Visible = True
                ddlAssignUser.Visible = True
                txtAssignDeadline.Visible = True
                ddlSelectUser.Visible = True
                rowAssign.Visible = True
                rowDeadline.Visible = True
                rowPriority.Visible = True
                rowStatus.Visible = True
                rowSelectUser.Visible = True
                RowTime.Visible = True

                catsql = "select  sid, uid as userid, fname, 1 as ordering  from WebFD.FinanceHelpDesk.tblUsers " & _
        "where IsRep = 1 and Active = 1 " & _
    " union select - 1, '', 'View All', 0 order by ordering, fname "

                ds.Clear()
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New SqlCommand(catsql, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds, "OData")

                End Using

                ddlCatReps.DataSource = ds
                ddlCatReps.DataValueField = "sid"
                ddlCatReps.DataTextField = "fname"
                ddlCatReps.DataBind()
                populatecats()

            End If


            MakeAccordion(3)
            MakeAccordion(2)
            MakeAccordion(1)
            MakeAccordion(100)

            Accordion1.CssClass = "accordion"
            Accordion2.CssClass = "accordion"
            Accordion3.CssClass = "accordion"

            If acc1 = 0 Then
                Accordion1.CssClass = "hidden"
            End If
            If acc2 = 0 Then
                Accordion2.CssClass = "hidden"
            End If
            If acc3 = 0 Then
                Accordion3.CssClass = "hidden"
            End If

            'If acc1 + acc2 + acc3 = 3 Then
            'ElseIf acc1 = 1 And acc2 = 1 Then
            '    Accordion1.CssClass = "accordion2"
            '    Accordion2.CssClass = "accordion2"
            '    Accordion3.CssClass = "accordion0"
            'ElseIf acc1 = 1 And acc3 = 1 Then
            '    Accordion1.CssClass = "accordion2"
            '    Accordion3.CssClass = "accordion2"
            '    Accordion2.CssClass = "accordion0"
            'ElseIf acc2 = 1 And acc3 = 1 Then
            '    Accordion2.CssClass = "accordion2"
            '    Accordion3.CssClass = "accordion2"
            '    Accordion1.CssClass = "accordion0"
            'ElseIf acc1 = 1 Then
            '    Accordion1.CssClass = "accordion3"
            '    Accordion2.CssClass = "accordion0"
            '    Accordion3.CssClass = "accordion0"
            'ElseIf acc2 = 1 Then
            '    Accordion2.CssClass = "accordion3"
            '    Accordion1.CssClass = "accordion0"
            '    Accordion3.CssClass = "accordion0"
            'ElseIf acc3 = 1 Then
            '    Accordion3.CssClass = "accordion3"
            '    Accordion1.CssClass = "accordion0"
            '    Accordion2.CssClass = "accordion0"
            'End If

            Dim userssql As String = "Select isnull([uid], '') + case when fname is null then '' when fname = 'NULL' then '' else  ' (' + isnull([fname], '') + ')' end as usr, " & _
                "sid from WebFD.FinanceHelpDesk.tblUsers where Active = 1 order by fname"


            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(userssql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using


            ddlSelectUser.DataSource = ds
            ddlSelectUser.DataValueField = "sid"
            ddlSelectUser.DataTextField = "usr"
            ddlSelectUser.DataBind()


            ddlSelectUser.SelectedValue = "null"

            Try
                For Each choice As ListItem In ddlSelectUser.Items
                    If InStr(choice.Text, " (") > 0 Then
                        If Left(choice.Text, InStr(choice.Text, " (") - 1) = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") Then
                            ddlSelectUser.SelectedValue = choice.Value
                        End If
                    Else
                        ddlSelectUser.SelectedValue = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
                    End If

                Next
            Catch ex As Exception
                ddlSelectUser.SelectedValue = "null"
            End Try

            userinfo(1)

            ' moved up 7/25/2018
            'catsql = "select * from WebFD.FinanceHelpDesk.categories where Active = 1 order by cname"

            'ds.Clear()
            'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If

            '    cmd = New SqlCommand(catsql, conn)
            '    da.SelectCommand = cmd
            '    da.SelectCommand.CommandTimeout = 86400
            '    da.Fill(ds, "OData")

            'End Using

            'ddlSelectCategory.DataSource = ds
            'ddlSelectCategory.DataValueField = "category_id"
            'ddlSelectCategory.DataTextField = "cname"
            'ddlSelectCategory.DataBind()

            'ddlUpdateCategory.DataSource = ds
            'ddlUpdateCategory.DataValueField = "category_id"
            'ddlUpdateCategory.DataTextField = "cname"
            'ddlUpdateCategory.DataBind()

            catsql = "select * from WebFD.FinanceHelpDesk.categories order by cname"

            Dim ds3 As New DataSet
            ds3.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(catsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds3, "OData")

            End Using

            ddlAdvSearchCategory.DataSource = ds3
            ddlAdvSearchCategory.DataValueField = "category_id"
            ddlAdvSearchCategory.DataTextField = "cname"
            ddlAdvSearchCategory.DataBind()


            Dim prisql As String = "select * from WebFD.FinanceHelpDesk.priority " & _
                "where priority_id <> 0"


            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(prisql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlSelectPriority.DataSource = ds
            ddlSelectPriority.DataValueField = "priority_id"
            ddlSelectPriority.DataTextField = "pname"
            ddlSelectPriority.DataBind()
            ddlSelectPriority.SelectedValue = 2

            ddlUpdatePriority.DataSource = ds
            ddlUpdatePriority.DataValueField = "priority_id"
            ddlUpdatePriority.DataTextField = "pname"
            ddlUpdatePriority.DataBind()
            ddlUpdatePriority.SelectedValue = 2

            ddlAdvSearchPriority.DataSource = ds
            ddlAdvSearchPriority.DataValueField = "priority_id"
            ddlAdvSearchPriority.DataTextField = "pname"
            ddlAdvSearchPriority.DataBind()

            Dim statsql As String = "select * from WebFD.FinanceHelpDesk.status " & _
             "where status_id <> 0"

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(statsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlSelectStatus.DataSource = ds
            ddlSelectStatus.DataValueField = "status_id"
            ddlSelectStatus.DataTextField = "sname"
            ddlSelectStatus.DataBind()

            ddlUpdateStatus.DataSource = ds
            ddlUpdateStatus.DataValueField = "status_id"
            ddlUpdateStatus.DataTextField = "sname"
            ddlUpdateStatus.DataBind()

            ddlAdvSearchStatus.DataSource = ds
            ddlAdvSearchStatus.DataValueField = "status_id"
            ddlAdvSearchStatus.DataTextField = "sname"
            ddlAdvSearchStatus.DataBind()

            Dim usersql As String = "select * from WebFD.FinanceHelpDesk.tblUsers " & _
            "where IsRep = 1 and Active = 1 order by fname "

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(usersql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlAssignUser.DataSource = ds
            ddlAssignUser.DataValueField = "sid"
            ddlAssignUser.DataTextField = "fname"
            ddlAssignUser.DataBind()

            Dim RepSql As String = "select 'Select Representative' as fname, 'null' as sid, 0 as ord union all select fname, convert(varchar,sid), 1 as ord from WebFD.FinanceHelpDesk.tblUsers " & _
           "where IsRep = 1 and Active = 1 order by ord, fname "

            Dim xtest As String = ddlUpdateRep.SelectedValue
            ddlUpdateRep.SelectedValue = "null"
            ddlUpdateRep.DataSource = GetData(RepSql, "WebFDconn")
            ddlUpdateRep.DataValueField = "sid"
            ddlUpdateRep.DataTextField = "fname"
            ddlUpdateRep.DataBind()
            Try
                ddlUpdateRep.SelectedValue = xtest
            Catch ex As Exception

            End Try



            Dim depsql As String = "select * from WebFD.FinanceHelpDesk.departments " & _
                "where department_id <> 0 and Active = 1 order by dname "

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(depsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlDepartment.DataSource = ds
            ddlDepartment.DataValueField = "department_id"
            ddlDepartment.DataTextField = "dname"
            ddlDepartment.DataBind()

            ddlUpdateDept.DataSource = ds
            ddlUpdateDept.DataValueField = "department_id"
            ddlUpdateDept.DataTextField = "dname"
            ddlUpdateDept.DataBind()

            ddlUsrDepartment.DataSource = ds
            ddlUsrDepartment.DataValueField = "department_id"
            ddlUsrDepartment.DataTextField = "dname"
            ddlUsrDepartment.DataBind()

            Dim fulldepsql As String = "select * from WebFD.FinanceHelpDesk.departments " & _
                "where department_id <> 0 order by dname "

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(fulldepsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlAdvSearchDepartment.DataSource = ds
            ddlAdvSearchDepartment.DataValueField = "department_id"
            ddlAdvSearchDepartment.DataTextField = "dname"
            ddlAdvSearchDepartment.DataBind()


            Dim da2 As New SqlDataAdapter
            Dim ds2 As New DataSet
            Dim assignedto As String = "select distinct isnull(u.fname, 'unknown - Rep ID ' + convert(varchar, p.rep)) repname, p.rep, " & _
                "case when u.sid is null then 0 else 1 end as known, u.fname " & _
                "from WebFD.FinanceHelpDesk.problems p " & _
                "left join WebFD.FinanceHelpDesk.tblUsers u on p.rep = u.sid and u.Active = 1" & _
                "where (1=" & admin.ToString & " or kb = 1) " & _
                "union  " & _
                "select top 1 '(Optional)', 0, 2, '(Optional)' from WebFD.FinanceHelpDesk.problems p " & _
                "union  " & _
                "select top 1 '(Text Search)', -1, 2, '(Text Search)' from WebFD.FinanceHelpDesk.problems p " & _
                "order by known desc, u.fname, p.rep"

            ds2.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(assignedto, conn)
                da2.SelectCommand = cmd
                da2.SelectCommand.CommandTimeout = 86400
                da2.Fill(ds2, "OData")

            End Using

            ddlAdvSearchAssignedTo.DataSource = ds2
            ddlAdvSearchAssignedTo.DataValueField = "rep"
            ddlAdvSearchAssignedTo.DataTextField = "repname"
            ddlAdvSearchAssignedTo.DataBind()

            Dim Cats As String = "select distinct case when isnull(usr.fname, 'NULL') = 'NULL' then p.uid else usr.fname end as username, 1 as known " & _
                "from WebFD.FinanceHelpDesk.problems p " & _
                "left join WebFD.FinanceHelpDesk.tblUsers usr on p.uid = usr.uid and usr.Active = 1 " & _
                "where (1=" & admin.ToString & " or kb = 1) " & _
                "union  " & _
                "select top 1 '(Optional)', 0 from WebFD.FinanceHelpDesk.problems p " & _
                "union  " & _
                "select top 1 '(Text Search)', 0 from WebFD.FinanceHelpDesk.problems p " & _
                "order by known, username asc "


            ds2.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(Cats, conn)
                da2.SelectCommand = cmd
                da2.SelectCommand.CommandTimeout = 86400
                da2.Fill(ds2, "OData")

            End Using

            ddlAdvSearchReportedBy.DataSource = ds2
            ddlAdvSearchReportedBy.DataValueField = "username"
            ddlAdvSearchReportedBy.DataTextField = "username"
            ddlAdvSearchReportedBy.DataBind()


            Dim subs As String = "select distinct case when isnull(u.fname, 'NULL') = 'NULL' then 'unknown - User ID ' + convert(varchar, p.entered_by) else u.fname end repname, " & _
                "p.entered_by as rep, case when u.sid is null then 0 else 1 end as known, u.fname " & _
                "from WebFD.FinanceHelpDesk.problems p " & _
                "left join WebFD.FinanceHelpDesk.tblUsers u on p.entered_by = u.sid and u.Active = 1" & _
                "where (1=" & admin.ToString & " or kb = 1) and entered_by is not null " & _
                "union  " & _
                "select top 1 '(Optional)', 0, 2, '(Optional)' from WebFD.FinanceHelpDesk.problems p " & _
                "union  " & _
                "select top 1 '(Text Search)', -1, 2, '(Text Search)' from WebFD.FinanceHelpDesk.problems p " & _
                "order by known desc, u.fname, rep "


            ds2.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(subs, conn)
                da2.SelectCommand = cmd
                da2.SelectCommand.CommandTimeout = 86400
                da2.Fill(ds2, "OData")

            End Using

            ddlAdvSearchSubmittedBy.DataSource = ds2
            ddlAdvSearchSubmittedBy.DataValueField = "rep"
            ddlAdvSearchSubmittedBy.DataTextField = "repname"
            ddlAdvSearchSubmittedBy.DataBind()

        End If
    End Sub

    Private Sub userinfo(a As Integer)

        If ddlSelectUser.SelectedValue = "null" Then
            txtCompName.Text = ""
            txtIPAdd.Text = ""
            txtPhones.Text = ""
            txtUserEmail.Text = ""
            txtUserName.Text = ""
        Else


            Dim usrsql As String
            If a <> 2 Then
                usrsql = "select * from WebFD.FinanceHelpDesk.tblUsers where  sid = '" & Replace(ddlSelectUser.SelectedValue.ToString, "'", "''") & "' and Active = 1"
            Else
                usrsql = "select * from WebFD.FinanceHelpDesk.tblUsers where uid = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1"
            End If

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(usrsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            If IsDBNull(ds.Tables(0).Rows(0).Item("location2")) Then
            Else
                If a <> 2 Then
                    txtCompName.Text = ds.Tables(0).Rows(0).Item("location2").ToString
                End If

                If a <> 0 Then
                    txtUsrCompName.Text = ds.Tables(0).Rows(0).Item("location2").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("location1")) Then
            Else
                If a <> 2 Then
                    txtIPAdd.Text = ds.Tables(0).Rows(0).Item("location1").ToString
                End If

                If a <> 0 Then
                    txtUsrIPAddress.Text = ds.Tables(0).Rows(0).Item("location1").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("phone")) Then
            Else
                If a <> 2 Then
                    txtPhones.Text = ds.Tables(0).Rows(0).Item("phone").ToString
                End If

                If a <> 0 Then
                    txtUsrPhone.Text = ds.Tables(0).Rows(0).Item("phone").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("email1")) Then
            Else
                If a <> 2 Then
                    txtUserEmail.Text = ds.Tables(0).Rows(0).Item("email1").ToString
                End If

                If a <> 0 Then
                    txtUsrEmail.Text = ds.Tables(0).Rows(0).Item("email1").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("fname")) Then
            Else
                If a <> 2 Then
                    txtUserName.Text = ds.Tables(0).Rows(0).Item("fname").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("department")) Then
            Else
                If a <> 2 Then
                    ddlDepartment.SelectedValue = ds.Tables(0).Rows(0).Item("department").ToString
                End If

                If a <> 0 Then
                    ddlUsrDepartment.SelectedValue = ds.Tables(0).Rows(0).Item("department").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("firstname")) Then
            Else
                If a <> 0 Then
                    txtUsrFirstName.Text = ds.Tables(0).Rows(0).Item("firstname").ToString
                End If

            End If
            If IsDBNull(ds.Tables(0).Rows(0).Item("lastname")) Then
            Else
                If a <> 0 Then
                    txtUsrLastName.Text = ds.Tables(0).Rows(0).Item("lastname").ToString
                End If

            End If
        End If

    End Sub

    Private Sub MakeAccordion(whichPri As Integer)

        Try
            Dim dr As SqlDataReader
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Dim accSql As String = "SELECT "

            If whichPri = 100 Then
                accSql += " top 10 "
            End If

            accSql += "convert(varchar, p.[id]) + '. ' as id, p.title, u.fname, p.[start_date], s.sname" & _
                ", p.[uid], p.uemail, p.uphone, p.ulocation, p.uComputerName, d.dname, c.cname, " & _
                "u.firstname, p.[description], " & _
                "(select convert(varchar(max),note) + ' -- ' + convert(varchar, n.addDate) + ';newline;' from WebFD.FinanceHelpDesk.tblNotes n " & _
                "where n.id = p.id and (n.[private] = 0 or n.[uid] = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "') " & _
                "order by n.addDate asc for XML Path('')) AS note " & _
                ", p.solution , pr.pname, pr.priority_id, s.status_id " & _
                " FROM WebFD.[FinanceHelpDesk].[problems] p " & _
                " left join WebFD.[FinanceHelpDesk].tblUsers u on p.rep = u.[sid] and u.Active = 1" & _
                " left join WebFD.[FinanceHelpDesk].[status] s on p.[status] = s.status_id " & _
                " left join WebFD.[FinanceHelpDesk].departments d on p.department = d.department_id " & _
                " left join WebFD.[FinanceHelpDesk].categories c on p.category = c.category_id " & _
                " left join WebFD.[FinanceHelpDesk].priority pr on p.priority = pr.priority_id " & _
                "/* left join WebFD.[FinanceHelpDesk].tblNotes n on n.[id] = p.[id] and (n.[private] = 0 or n.[uid] = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "') */" & _
                " where (([status] <> 100 and 100 <> " & whichPri & ") or [status] = '" & whichPri & "') " & _
                " and (pr.priority_id = '" & whichPri & "' or 100 = '" & whichPri & "') " & _
                " and (p.[uid] = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                "' or rep = (select isnull([sid],0) FROM WebFD.[FinanceHelpDesk].[tblUsers] where [uid] = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1) or '1' = '" & superadmin & "')"

            If whichPri = 100 Then
                accSql += " order by p.[close_date] desc"
            Else
                accSql += " order by s.status_id asc, p.[start_date] desc"
            End If

            Dim rowcnt As Integer = 0

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                cmd = New SqlCommand(accSql, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader

                'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("FinanceHelpDesk").ConnectionString)
                '    If conn.State = ConnectionState.Closed Then
                '        conn.Open()
                '    End If

                '    cmd = New SqlCommand(accSql, conn)
                '    da.SelectCommand = cmd
                '    da.SelectCommand.CommandTimeout = 86400
                '    da.Fill(ds, "OData")

                'End Using

                'For Each rw As DataRow In ds.Tables(0).Rows
                '    If IsDBNull(rw.Item("note")) Then
                '    Else
                '        rw.Item("note") = Replace(rw.Item("note"), ";nextline;", "<BR />")
                '    End If
                'Next

                'Dim acc As New AjaxControlToolkit.Accordion()
                'acc.ID = "accOpenCases"

                If dr.HasRows() And whichPri = 3 Then
                    acc1 = 1
                ElseIf dr.HasRows() And whichPri = 2 Then
                    acc2 = 1
                ElseIf dr.HasRows() And whichPri = 1 Then
                    acc3 = 1
                End If

                Dim cntr As Integer = 0

                If whichPri = 3 Then
                    Accordion1.DataSource = dr
                    Accordion1.DataBind()
                ElseIf whichPri = 2 Then
                    Accordion2.DataSource = dr
                    Accordion2.DataBind()
                ElseIf whichPri = 1 Then
                    Accordion3.DataSource = dr
                    Accordion3.DataBind()
                ElseIf whichPri = 100 Then
                    Accordion4.DataSource = dr
                    Accordion4.DataBind()
                End If

                'For Each rw As DataRow In dr()
                '    While dr.Read
                '        If IsDBNull(dr.Item("priority_id")) Then
                '        Else
                '            Select Case dr.Item("priority_id")
                '                Case 3
                '                    Accordion1.Panes(cntr).HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                '                Case 2
                '                    Accordion1.Panes(cntr).HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                '                Case 1
                '                    Accordion1.Panes(cntr).HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                '                Case 0
                '                    Accordion1.Panes(cntr).HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#808080")
                '                Case Else
                '                    Accordion1.Panes(cntr).HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                '            End Select
                '        End If
                '    End While


                'While dr.Read

                '    Dim accPnl As New AjaxControlToolkit.AccordionPane()
                '    accPnl.ID = "accPnl" & Str(rowcnt)


                '    accPnl.Width = "400px"
                '    accPnl.Height = "30px"

                '    If Not IsDBNull(dr.Item("priority_id")) Then
                '        If rowcnt = 0 Then
                '            Select Case dr.Item("priority_id")
                '                Case 3
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                '                Case 2
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                '                Case 1
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                '                Case 0
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#808080")
                '                Case Else
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                '            End Select
                '        Else
                '            Select Case dr.Item("priority_id")
                '                Case 3
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                '                Case 2
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                '                Case 1
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                '                Case 0
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3")
                '                Case Else
                '                    accPnl.HeaderContainer.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                '            End Select
                '        End If

                '    End If
                '    Dim lbltitle As Label
                '    lbltitle.Text = ""

                '    If Not IsDBNull(dr.Item("id")) Then
                '        lbltitle.Text += Str(dr.Item("id")) & " -- "
                '    End If

                '    If Not IsDBNull(dr.Item("title")) Then
                '        lbltitle.Text += Str(dr.Item("title"))
                '    End If


                '    accPnl.HeaderContainer.Controls.Add(lbltitle)
                '    acc.Panes.Add(accPnl)
                '    rowcnt += 1

                'End While
                'dr.Read()

                'phOpenCases.Controls.Add(acc)

            End Using
        Catch ex As Exception

        End Try

    End Sub




    Private Sub Accordion1_ItemCreated(sender As Object, e As AjaxControlToolkit.AccordionItemEventArgs) Handles Accordion1.ItemCreated

        'Dim lbl As New Label
        'Dim pnl As New Panel


        ''If e.AccordionItem.ItemType = AjaxControlToolkit.AccordionItemType.Header Then
        'lbl = e.AccordionItem.FindControl("lblHiddenTest")
        'pnl = e.AccordionItem.FindControl("pnlTest")
        ''End If

        'Select Case lbl.Text
        '    Case 3
        '        pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
        '    Case 2
        '        pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
        '    Case 1
        '        pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
        '    Case 0
        '        pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3")
        '    Case Else
        '        pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
        'End Select
    End Sub


    Private Sub ddlSelectUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectUser.SelectedIndexChanged
        userinfo(0)
    End Sub

    Private Sub resetform()

        txtUserName.Text = ""
        txtUserEmail.Text = ""
        ddlDepartment.SelectedValue = 0
        txtIPAdd.Text = ""
        txtCompName.Text = ""
        txtPhones.Text = ""
        ddlSelectUser.SelectedValue = "null"
        ddlSelectCategory.SelectedValue = "null"
        ddlSelectStatus.SelectedValue = 1
        ddlSelectPriority.SelectedValue = 2
        ddlAssignUser.SelectedValue = "null"
        txtAssignDeadline.Text = "12/31/9999"
        txtTimeSpent.Text = "0"
        txtTitle.Text = ""
        txtDescription.Text = ""
        txtSolution.Text = ""
        chbDontSendMail.Checked = False
        chbDontEmailUser.Checked = False

    End Sub
    Private Sub btnResetFrom_Click(sender As Object, e As EventArgs) Handles btnResetFrom.Click
        resetform()
    End Sub

    Private Sub btnSubmitNewCase_Click(sender As Object, e As EventArgs) Handles btnSubmitNewCase.Click

        Debug.Print("Values")
        Debug.Print(ddlAssignUser.SelectedValue)
        Debug.Print(ddlAssignUser.SelectedItem.ToString)

        If ddlSelectCategory.SelectedValue = "null" Then
            explantionlabel.Text = "Please select Category"
            tcOpenAttch.Visible = False
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If ddlDepartment.SelectedValue = "null" Then
            explantionlabel.Text = "Please select Department"
            tcOpenAttch.Visible = False
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        Dim insertsql As String = "insert into WebFD.FinanceHelpDesk.problems ([uid], uemail, ulocation, uphone, rep, [status], time_spent, category, priority, department, title, " & _
            "description, solution, [start_date], entered_by, kb, uComputerName, Deadline, SendEmail ) " & _
            "values ("

        If ddlSelectUser.SelectedValue = "null" Then
            insertsql += "'" & Replace(txtUserName.Text, "'", "''") & "', '"
        Else
            insertsql += "(Select [uid] from WebFD.FinanceHelpDesk.tblUsers where sid = " & Replace(ddlSelectUser.SelectedValue, "'", "''") & " ), '"
        End If

        If admin = 1 Then

            insertsql += Replace(txtUserEmail.Text, "'", "''") & "', '" & Replace(txtIPAdd.Text, "'", "''") & "', '" & _
                Replace(txtPhones.Text, "'", "''") & "', isnull(" & Replace(ddlAssignUser.SelectedValue, "'", "''") & ",  (select rep_id from WebFD.FinanceHelpDesk.categories where category_id = '" & Replace(ddlSelectCategory.SelectedValue, "'", "''") & _
                "')), '" & Replace(ddlSelectStatus.SelectedValue, "'", "''") & "', '" & Replace(txtTimeSpent.Text, "'", "''") & "', '" & Replace(ddlSelectCategory.SelectedValue, "'", "''") & _
                "', '" & Replace(ddlSelectPriority.SelectedValue, "'", "''") & "', '" & Replace(ddlDepartment.SelectedValue, "'", "''") & "', '" & Replace(txtTitle.Text, "'", "''") & _
                "', '" & Replace(txtDescription.Text, "'", "''") & "', '" & Replace(txtSolution.Text, "'", "''") & "', getdate(), " & _
                "(select ISNULL(sid ,0) from WebFD.FinanceHelpDesk.tblUsers where uid = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ), 0, '" & Replace(txtCompName.Text, "'", "''") & "', "
        Else

            insertsql += Replace(txtUserEmail.Text, "'", "''") & "', '" & Replace(txtIPAdd.Text, "'", "''") & "', '" & _
                Replace(txtPhones.Text, "'", "''") & "', (select rep_id from WebFD.FinanceHelpDesk.categories where category_id = '" & Replace(ddlSelectCategory.SelectedValue, "'", "''") & _
                "'), '1', null, '" & Replace(ddlSelectCategory.SelectedValue, "'", "''") & _
                "', '2', '" & Replace(ddlDepartment.SelectedValue, "'", "''") & "', '" & Replace(txtTitle.Text, "'", "''") & _
                "', '" & Replace(txtDescription.Text, "'", "''") & "', null, getdate(), " & _
                "(select ISNULL(sid ,0) from WebFD.FinanceHelpDesk.tblUsers where uid = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ), 0, '" & Replace(txtCompName.Text, "'", "''") & "', "
        End If

        'If txtAssignDeadline.Text = "12/31/9999" Then
        '    insertsql += " null, "
        'Else
        insertsql += " '" & Replace(txtAssignDeadline.Text, "'", "''") & "', "
        'End If

        If chbDontSendMail.Checked = False Then
            insertsql += "'both') ;"
        Else
            insertsql += "'rep') ;"
        End If

        insertsql += " select max(id) from WebFD.FinanceHelpDesk.problems where uid = "

        If ddlSelectUser.SelectedValue = "null" Then
            insertsql += "'" & Replace(txtUserName.Text, "'", "''") & "';"
        Else
            insertsql += "(Select [uid] from WebFD.FinanceHelpDesk.tblUsers where sid = " & Replace(ddlSelectUser.SelectedValue, "'", "''") & " );"
        End If

        insertsql += " update WebFD.FinanceHelpDesk.tblUsers set Active = 1 where sid = '" & Replace(ddlSelectUser.SelectedValue, "'", "''") & "' and Active = 0; "

        Dim cmd As New SqlCommand
        Debug.Print(insertsql)
        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New System.Data.SqlClient.SqlCommand(insertsql, conn)
            'cmd.ExecuteNonQuery()

            'lblhiddenCaseNo.Text = cmd.ExecuteScalar

            btnOpenAttach.Attributes.Add("onclick", "javascript:open_win('" & cmd.ExecuteScalar & "')")


            explantionlabel.Text = "Successfully Submitted Case"
            tcOpenAttch.Visible = True
            explantionlabel.DataBind()
            resetform()
        End Using

        MakeAccordion(3)
        MakeAccordion(2)
        MakeAccordion(1)
        MakeAccordion(100)

        Accordion1.CssClass = "accordion"
        Accordion2.CssClass = "accordion"
        Accordion3.CssClass = "accordion"

        If acc1 = 0 Then
            Accordion1.CssClass = "hidden"
        End If
        If acc2 = 0 Then
            Accordion2.CssClass = "hidden"
        End If
        If acc3 = 0 Then
            Accordion3.CssClass = "hidden"
        End If

        If admin = 1 Then
            populateOCGridview()

            Rowing()
        End If

        ModalPopupExtender1.Show()

    End Sub

    Private Sub populateOCGridview()

        Dim gvsql As String = "select ID, title  as Title, CASE WHEN u.uid is null then 'No User ID' else u.uid end as [UsrSubmit], " & _
            " CASE WHEN u.fname is null THEN 'No User Name' else u.fname END as [User Name], " & _
          " p.[start_date] as dtSubmitted, convert(varchar, p.[start_date], 107) as [Date Submitted]  " & _
          " , Priority, [status], " & _
          " replace(uemail, '''', 'replacedapostrophe') as uemail, p.department, replace(p.ulocation, '''', 'replacedapostrophe') as ulocation, " & _
          " replace(p.uComputerName, '''', 'replacedapostrophe') as uComputerName, replace(p.uphone, '''', 'replacedapostrophe') as uphone, " & _
          " replace(e.fname, '''', 'replacedapostrophe')  as entered_by, " & _
          " replace(p.category, '''', 'replacedapostrophe') as category, rep, time_spent, " & _
          " convert(varchar, p.[start_date], 107) as start_date, convert(varchar,close_date, 107) as close_date," & _
          " replace( (select convert(varchar(max), Title) from WebFD.FinanceHelpDesk.problems dsc where dsc.id = p.id for XML Path('')), '''', 'replacedapostrophe') as title2, " & _
          " description  as  [description], " & _
          " replace( (select convert(varchar(max), Solution) from WebFD.FinanceHelpDesk.problems dsc where dsc.id = p.id for XML Path('')), '''', 'replacedapostrophe')  as solution, " & _
          "  Replace((select convert(varchar(max),note) + ' -- ' + convert(varchar, n.addDate) + ';newline;' " & _
          " from WebFD.FinanceHelpDesk.tblNotes n  " & _
          " where n.id = p.id and (n.[private] = 0 or n.[uid] = '" & Replace(ddlViewOtherCases.SelectedValue, "'", "''") & "')" & _
          " order by n.addDate asc for XML Path('')), '''', 'replacedapostrophe') as notes, isnull(Attachments,0) as Attachments, convert(varchar, isnull(p.[Deadline], '12/31/9999'), 107) as deadline" & _
          " from [WebFD].[FinanceHelpDesk].[problems] p " & _
          " left join WebFD.[FinanceHelpDesk].tblUsers u on p.uid = u.uid  " & _
          " left join WebFD.FinanceHelpDesk.tblUsers e on p.entered_by = e.sid and e.Active = 1" & _
          " left join (select ProblemID, COUNT(*) Attachments from WebFD.FinanceHelpDesk.tblAttachedFiles group by ProblemID) atch " & _
          " on p.id = atch.ProblemID " & _
          " where [status] <> 100 " & _
          " and (isnull(p.rep, 232) = (select [sid] from WebFD.FinanceHelpDesk.tblUsers " & _
          " where [uid] = '" & Replace(ddlViewOtherCases.SelectedValue, "'", "''") & "' ) or 1 = " & superadmin & _
          ")  order by Priority desc, dtSubmitted desc "

        Debug.Print(gvsql)

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        ds.Clear()
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand(gvsql, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

        End Using

        AdminView = ds.Tables(0).DefaultView
        gvOpenCases.DataSource = AdminView
        gvOpenCases.DataBind()


        If AdminView.Count > 0 Then
            gvOpenCases.SelectedIndex = 0
            gvOpenCases_SelectedIndexChanged(Me, EventArgs.Empty)
        End If



    End Sub

    Private Sub gvOpenCases_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvOpenCases.PageIndexChanging
        Try

            gvOpenCases.PageIndex = e.NewPageIndex
            gvOpenCases.DataSource = AdminView
            gvOpenCases.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvOpenCases_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvOpenCases.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
                e.Row.Cells(7).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"

                If e.Row.DataItem("Priority") = "3" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    If e.Row.DataItem("Attachments") > 0 Then
                        Dim img As Image = e.Row.FindControl("imgLR")
                        img.Visible = True
                    End If
                    If e.Row.DataItem("Status") = "1" Then
                        Dim lbl1 As Label = e.Row.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf e.Row.DataItem("Status") = "0" Then
                    ElseIf e.Row.DataItem("Status") = "3" Then
                    Else
                        Dim img2 As Image = e.Row.FindControl("imgLRHR")
                        img2.Visible = True
                    End If
                ElseIf e.Row.DataItem("Priority") = "2" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    If e.Row.DataItem("Attachments") > 0 Then
                        Dim img As Image = e.Row.FindControl("imgLO")
                        img.Visible = True
                    End If
                    If e.Row.DataItem("Status") = "1" Then
                        Dim lbl1 As Label = e.Row.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf e.Row.DataItem("Status") = "0" Then
                    ElseIf e.Row.DataItem("Status") = "3" Then
                    Else
                        Dim img2 As Image = e.Row.FindControl("imgLOHR")
                        img2.Visible = True
                    End If
                ElseIf e.Row.DataItem("Priority") = "1" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    If e.Row.DataItem("Attachments") > 0 Then
                        Dim img As Image = e.Row.FindControl("imgLY")
                        img.Visible = True
                    End If
                    If e.Row.DataItem("Status") = "1" Then
                        Dim lbl1 As Label = e.Row.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf e.Row.DataItem("Status") = "0" Then
                    ElseIf e.Row.DataItem("Status") = "3" Then
                    Else
                        Dim img2 As Image = e.Row.FindControl("imgLYHR")
                        img2.Visible = True
                    End If
                End If

            End If
        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Shared Function GetData(query As String, connectString As String) As DataTable

        Dim strConnString As String = ConfigurationManager.ConnectionStrings(connectString).ConnectionString

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
    Private Sub gvOpenCases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvOpenCases.SelectedIndexChanged
        Try
            Dim cat As String = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(15).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            Dim dep As String = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(10).Text), "&nbsp;", ""), "replacedapostrophe", "'")

            Try
                ddlUpdateCategory.SelectedValue = cat
            Catch ex As Exception


                Dim catsql As String = "select * from WebFD.FinanceHelpDesk.categories where Active = 1 or category_id = '" & cat & "' order by cname"

                ddlUpdateCategory.DataSource = GetData(catsql, "WebFDconn")
                ddlUpdateCategory.DataValueField = "category_id"
                ddlUpdateCategory.DataTextField = "cname"
                ddlUpdateCategory.DataBind()
                ddlUpdateCategory.SelectedValue = cat

            End Try

            Try
                ddlUpdateDept.SelectedValue = dep
            Catch ex As Exception

                Dim depsql As String = "select * from WebFD.FinanceHelpDesk.departments " & _
                    "where (department_id <> 0 and Active = 1) or department_id = '" & dep & "' order by dname "

                ddlUpdateDept.DataSource = GetData(depsql, "WebFDconn")
                ddlUpdateDept.DataValueField = "department_id"
                ddlUpdateDept.DataTextField = "dname"
                ddlUpdateDept.DataBind()
                ddlUpdateDept.SelectedValue = dep

            End Try


            ddlUpdatePriority.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(7).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            ddlUpdateStatus.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(8).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblUpdateUserName.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(5).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdateEmail.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(9).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdateIPAdd.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(11).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdateCompName.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(12).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdatePhone.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(13).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblUpdateEntered.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(14).Text), "&nbsp;", ""), "replacedapostrophe", "'")

            Try
                If Replace(Server.HtmlDecode(Replace(gvOpenCases.SelectedRow.Cells(26).Text, "&nbsp;", "")), "replacedapostrophe", "'") = Nothing Then
                    ddlUpdateUser.SelectedValue = ""
                ElseIf Replace(Server.HtmlDecode(Replace(gvOpenCases.SelectedRow.Cells(26).Text, "&nbsp;", "")), "replacedapostrophe", "'") = "No User ID" Then
                    ddlUpdateUser.SelectedValue = ""
                Else
                    ddlUpdateUser.SelectedValue = Replace(Server.HtmlDecode(Replace(gvOpenCases.SelectedRow.Cells(26).Text, "&nbsp;", "")), "replacedapostrophe", "'")
                End If

                'ddlUpdateUser.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(26).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                Debug.Print(">> " & ddlUpdateUser.SelectedValue)
            Catch ex As Exception
                'ddlUpdateUser.SelectedValue = "null"
                ddlUpdateUser.SelectedValue = ""     ' UID for "No User Selected" user
            End Try

            Try
                ddlUpdateRep.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                Debug.Print(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(16).Text))
            Catch ex As Exception
                ddlUpdateRep.SelectedValue = "null"
            End Try

            txtUpdateTime.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblUpdateStartDate.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblUpdateEndDate.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(19).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdateTitle.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(4).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdateDesc.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(21).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtUpdateSolution.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(22).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblUpdateNotes.Text = Replace(Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(23).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")
            txtUpdateDeadline.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(25).Text), "&nbsp;", ""), "replacedapostrophe", "'")

            btnViewAttach.Attributes.Add("onclick", "javascript:open_win('" & Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(2).Text), "'", "''") & "')")

            Rowing()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub Rowing()
        Dim img As Image
        Dim img2 As Image
        Dim img3 As Image
        Dim img4 As Image
        Dim lbl1 As Label

        For Each canoe As GridViewRow In gvOpenCases.Rows
            If canoe.RowIndex = gvOpenCases.SelectedIndex Then

                Debug.Print(
                "1>> " & canoe.Cells(1).Text & vbCr &
                "2>> " & canoe.Cells(2).Text & vbCr &
                "3>> " & canoe.Cells(3).Text & vbCr &
                "4>> " & canoe.Cells(4).Text & vbCr &
                "5>> " & canoe.Cells(5).Text & vbCr &
                "6>> " & canoe.Cells(6).Text & vbCr &
                "7>> " & canoe.Cells(7).Text
                )




                If canoe.Cells(7).Text = "3" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                    If canoe.Cells(24).Text > 0 Then
                        img = canoe.FindControl("imgLR")
                        img.Visible = False
                        img2 = canoe.FindControl("imgDR")
                        img2.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "2" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                    If canoe.Cells(24).Text > 0 Then
                        img = canoe.FindControl("imgLO")
                        img.Visible = False
                        img2 = canoe.FindControl("imgDO")
                        img2.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                    If canoe.Cells(24).Text > 0 Then
                        img = canoe.FindControl("imgLY")
                        img.Visible = False
                        img2 = canoe.FindControl("imgDY")
                        img2.Visible = True
                    End If
                End If

            Else

                If canoe.Cells(7).Text = "3" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    If canoe.Cells(24).Text > 0 Then
                        img = canoe.FindControl("imgDR")
                        img.Visible = False
                        img2 = canoe.FindControl("imgLR")
                        img2.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "2" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    If canoe.Cells(24).Text > 0 Then
                        img = canoe.FindControl("imgDO")
                        img.Visible = False
                        img2 = canoe.FindControl("imgLO")
                        img2.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    If canoe.Cells(24).Text > 0 Then
                        img = canoe.FindControl("imgDY")
                        img.Visible = False
                        img2 = canoe.FindControl("imgLY")
                        img2.Visible = True
                    End If
                End If
            End If

            If canoe.RowIndex = gvOpenCases.SelectedIndex Then
                If canoe.Cells(7).Text = "3" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                    If canoe.Cells(8).Text = "1" Then
                        lbl1 = canoe.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf canoe.Cells(8).Text = "3" Then
                    ElseIf canoe.Cells(8).Text = "0" Then
                    Else
                        img3 = canoe.FindControl("imgLRHR")
                        img3.Visible = False
                        img4 = canoe.FindControl("imgDRHR")
                        img4.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "2" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                    If canoe.Cells(8).Text = "1" Then
                        lbl1 = canoe.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf canoe.Cells(8).Text = "3" Then
                    ElseIf canoe.Cells(8).Text = "0" Then
                    Else
                        img3 = canoe.FindControl("imgLOHR")
                        img3.Visible = False
                        img4 = canoe.FindControl("imgDOHR")
                        img4.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                    If canoe.Cells(8).Text = "1" Then
                        lbl1 = canoe.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf canoe.Cells(8).Text = "3" Then
                    ElseIf canoe.Cells(8).Text = "0" Then
                    Else
                        img3 = canoe.FindControl("imgLYHR")
                        img3.Visible = False
                        img4 = canoe.FindControl("imgDYHR")
                        img4.Visible = True
                    End If
                End If

            Else

                If canoe.Cells(7).Text = "3" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    If canoe.Cells(8).Text = "1" Then
                        lbl1 = canoe.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf canoe.Cells(8).Text = "3" Then
                    ElseIf canoe.Cells(8).Text = "0" Then
                    Else
                        img3 = canoe.FindControl("imgDRHR")
                        img3.Visible = False
                        img4 = canoe.FindControl("imgLRHR")
                        img4.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "2" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    If canoe.Cells(8).Text = "1" Then
                        lbl1 = canoe.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf canoe.Cells(8).Text = "3" Then
                    ElseIf canoe.Cells(8).Text = "0" Then
                    Else
                        img3 = canoe.FindControl("imgDOHR")
                        img3.Visible = False
                        img4 = canoe.FindControl("imgLOHR")
                        img4.Visible = True
                    End If
                ElseIf canoe.Cells(7).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    If canoe.Cells(8).Text = "1" Then
                        lbl1 = canoe.FindControl("lblAsterisk")
                        lbl1.Visible = True
                    ElseIf canoe.Cells(8).Text = "3" Then
                    ElseIf canoe.Cells(8).Text = "0" Then
                    Else
                        img3 = canoe.FindControl("imgDYHR")
                        img3.Visible = False
                        img4 = canoe.FindControl("imgLYHR")
                        img4.Visible = True
                    End If
                End If
            End If

        Next
    End Sub

    Private Sub gvOpenCases_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvOpenCases.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = AdminView

        sorts = e.SortExpression

        If e.SortExpression = Adminmap Then

            If Admindir = 1 Then
                dv.Sort = sorts + " " + "desc"
                Admindir = 0
            Else
                dv.Sort = sorts + " " + "asc"
                Admindir = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            Admindir = 1
            Adminmap = e.SortExpression
        End If

        gvOpenCases.DataSource = dv
        gvOpenCases.DataBind()
    End Sub

    Private Sub btnUpdateCase_Click(sender As Object, e As EventArgs) Handles btnUpdateCase.Click

        Dim img As Image
        Dim img2 As Image
        Dim img3 As Image
        Dim img4 As Image
        Dim lbl1 As Label


        If gvOpenCases.SelectedIndex = -1 Then
            explanationlabel2.Text = "No Case Selected"
            explanationlabel2.DataBind()
            ModalPopupExtender2.Show()
            Rowing()
            Exit Sub
        ElseIf ddlUpdateUser.SelectedValue = "null" Then
            explanationlabel2.Text = "No Representative Selected"
            explanationlabel2.DataBind()
            ModalPopupExtender2.Show()
            Rowing()
            Exit Sub
        End If

        Dim rep As String = "NULL"
        If ddlUpdateRep.SelectedValue <> "null" Then
            rep = "'" & Replace(ddlUpdateRep.SelectedValue, "'", "''") & "'"
        End If
        Replace(ddlUpdateRep.SelectedValue, "'", "''")

        Dim updatesql As String = "Update [WebFD].[FinanceHelpDesk].[problems] set uid = '" & Replace(ddlUpdateUser.SelectedValue, "'", "''") & "',  uemail = '" & Replace(txtUpdateEmail.Text.Trim, "'", "''") & _
            "', department = '" & Replace(ddlUpdateDept.SelectedValue, "'", "''") & "', ulocation = '" & Replace(txtUpdateIPAdd.Text.Trim, "'", "''") & _
            "', uComputerName = '" & Replace(txtUpdateCompName.Text.Trim, "'", "''") & "', uphone = '" & Replace(txtUpdatePhone.Text.Trim, "'", "''") & _
            "', category = " & Replace(ddlUpdateCategory.SelectedValue, "'", "''") & ", status = '" & Replace(ddlUpdateStatus.SelectedValue, "'", "''") & _
            "', priority = '" & Replace(ddlUpdatePriority.SelectedValue, "'", "''") & "', rep = " & rep & _
            ", time_spent = '" & Replace(txtUpdateTime.Text.Trim, "'", "''") & "', title = '" & Replace(txtUpdateTitle.Text.Trim, "'", "''") & _
            "', description = '" & Replace(txtUpdateDesc.Text.Trim, "'", "''") & "', solution = '" & Replace(txtUpdateSolution.Text.Trim, "'", "''") & _
            "', SendEmail = case when rep = " & rep & " then null else 'nwrep' end " & _
            ", Deadline = '" & Replace(txtUpdateDeadline.Text.Trim, "'", "''") & "',  kb = '"
        If chbKB.Checked Then
            updatesql += "1"
        Else
            updatesql += "0"
        End If
        updatesql += "' where id = '" & Replace(gvOpenCases.SelectedRow.Cells(2).Text, "'", "''") & "'"

        If ddlUpdateStatus.SelectedValue = 100 Then
            updatesql += "; Update [WebFD].[FinanceHelpDesk].[problems] set close_date = getdate() where id = '" & Replace(gvOpenCases.SelectedRow.Cells(2).Text, "'", "''") & "'"
            'gvOpenCases.SelectedIndex = -1
        End If

        Debug.Print(updatesql)

        'Dim tst As Array
        'Dim newnotes As String = ""
        'tst = txtUpdateNewNotes.Text.ToCharArray
        'Dim n As Char = Chr(10)

        'For Each i As Char In tst
        '    If i = n Then
        '        newnotes += ";newline;"
        '    Else
        '        newnotes += i
        '    End If
        'Next
        'newnotes += n

        If Trim(txtUpdateNewNotes.Text) <> "" Then
            updatesql += "; Insert into WebFD.FinanceHelpDesk.tblNotes (id, note, addDate, uid, private) Values ('" & _
                Replace(gvOpenCases.SelectedRow.Cells(2).Text, "'", "''") & "', '" & Replace(Replace(txtUpdateNewNotes.Text.Trim, "'", "''"), Chr(10), ";newline;") & _
                "', getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
            If chbHideNotes.Checked Then
                updatesql += "', 1) "
            Else
                updatesql += "', 0) "
            End If
        End If

        If chbDontEmailUser.Checked = False Then
            updatesql += "; Update [WebFD].[FinanceHelpDesk].[problems] set SendEmail = case when SendEmail = 'nwrep' then 'nwusr' else 'user' end where id = '" & Replace(gvOpenCases.SelectedRow.Cells(2).Text, "'", "''") & "' "
        End If

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
            Debug.Print(updatesql)
            cmd.ExecuteNonQuery()
            explanationlabel2.Text = "Successfully Submitted Case"
            explanationlabel2.DataBind()

        End Using

        If ddlUpdateStatus.SelectedValue = 100 Then
            'updatesql += "; Update [WebFD].[FinanceHelpDesk].[problems] set close_date = getdate() where id = '" & Replace(gvOpenCases.SelectedRow.Cells(2).Text, "'", "''") & "'"
            gvOpenCases.SelectedIndex = -1
        End If

        populateOCGridview()

        Try

            If gvOpenCases.SelectedIndex <> -1 Then
                ddlUpdatePriority.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(7).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                ddlUpdateStatus.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(8).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                lblUpdateUserName.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(5).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdateEmail.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(9).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                ddlUpdateDept.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(10).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdateIPAdd.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(11).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdateCompName.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(12).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdatePhone.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(13).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                lblUpdateEntered.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(14).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                ddlUpdateCategory.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(15).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                Try
                    ddlUpdateRep.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                Catch ex As Exception
                    ddlUpdateRep.SelectedValue = "null"
                End Try

                Try
                    ddlUpdateUser.SelectedValue = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(26).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                Catch ex As Exception
                    'ddlUpdateUser.SelectedValue = "null"
                    ddlUpdateUser.SelectedValue = ""
                End Try

                txtUpdateTime.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                lblUpdateStartDate.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                lblUpdateEndDate.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(19).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdateTitle.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(4).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdateDesc.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(21).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                txtUpdateSolution.Text = Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(22).Text), "&nbsp;", ""), "replacedapostrophe", "'")
                lblUpdateNotes.Text = Replace(Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(23).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")
                txtUpdateDeadline.Text = Replace(Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(25).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")

                btnViewAttach.Attributes.Add("onclick", "javascript:open_win('" & Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(2).Text), "'", "''") & "')")

            End If
        Catch ex As Exception

        End Try

        Rowing()



        chbDontEmailUser.Checked = False
        chbHideNotes.Checked = False
        chbKB.Checked = False
        txtUpdateNewNotes.Text = ""

        MakeAccordion(3)
        MakeAccordion(2)
        MakeAccordion(1)
        MakeAccordion(100)

        Accordion1.CssClass = "accordion"
        Accordion2.CssClass = "accordion"
        Accordion3.CssClass = "accordion"

        If acc1 = 0 Then
            Accordion1.CssClass = "hidden"
        End If
        If acc2 = 0 Then
            Accordion2.CssClass = "hidden"
        End If
        If acc3 = 0 Then
            Accordion3.CssClass = "hidden"
        End If

        ModalPopupExtender2.Show()

    End Sub

#Region "PanelThree"
    Sub EasySearch()

        Dim searchsql As String = "declare @Phrase varchar(max) = '" & Replace(Replace(txtEasySearch.Text, "'", "''"), ",", " ") & "' " & _
            "declare @Mini varchar(max) " & _
            "" & _
            "if OBJECT_ID('tempdb..#tempy') is not null " & _
            "begin " & _
            "   drop table #tempy " & _
            "End " & _
            "" & _
            " Select 0 as titlescore, 0 as descscore, 0 as solscore, 0 as notescore, " & _
            " id, title, description, solution, " & _
            "(select convert(varchar(max),note) + ' -- ' + convert(varchar, n.addDate) + ';newline;' from WebFD.FinanceHelpDesk.tblNotes n " & _
            "where n.id = p.id and (n.[private] = 0 or n.[uid] = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "')" & _
            "    order by n.addDate asc for XML Path('')) AS note " & _
            "into #tempy " & _
            "from WebFD.FinanceHelpDesk.problems p " & _
            "" & _
            "while LEN(@Phrase) > 0 " & _
            "begin " & _
            "if CHARINDEX(' ', @Phrase, 0) > 0 " & _
            "begin " & _
            "set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0)))) " & _
            "set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase)))) " & _
            "End " & _
            "Else " & _
            "begin " & _
            "set @Mini = ltrim(@Phrase) " & _
            "set @Phrase = '' " & _
            "End " & _
            "update #tempy " & _
            "set titlescore = titlescore + 3 " & _
            "where " & _
            "title like '% ' + @Mini + ' %' " & _
            "or title like @Mini + ' %' " & _
            "or title like '% ' +@Mini " & _
            "or title = @Mini " & _
            "update #tempy " & _
            "set titlescore = titlescore + 1 " & _
            "where " & _
            "title like '%' + @Mini + '%' " & _
            "update #tempy " & _
            "set descscore = descscore + 3 " & _
            "where " & _
            "description like '% ' + @Mini + ' %' " & _
            "or description like @Mini + ' %' " & _
            "or description like '% ' +@Mini " & _
            "or description = @Mini " & _
            "update #tempy " & _
            "set descscore = descscore + 1 " & _
            "where " & _
            "description like '%' + @Mini + '%' " & _
            "update #tempy " & _
            "set solscore = solscore + 3 " & _
            "where " & _
            "solution like '% ' + @Mini + ' %' " & _
            "or solution like @Mini + ' %' " & _
            "or solution like '% ' +@Mini " & _
            "or solution = @Mini " & _
            "update #tempy " & _
            "set solscore = solscore + 1 " & _
            "where " & _
            "solution like '%' + @Mini + '%' " & _
            "update #tempy " & _
            "set notescore = notescore + 3 " & _
            "where " & _
            "note like '% ' + @Mini + ' %' " & _
            "or note like @Mini + ' %' " & _
            " or note like '% ' +@Mini " & _
            "or note = @Mini " & _
            "update #tempy " & _
            "set notescore = notescore + 1 " & _
            "where " & _
            "note like '%' + @Mini + '%' " & _
            "End " & _
            "select titlescore*8 + descscore*4 + solscore*3 + notescore AS ord,  " & _
            " p.[start_date] as dtSubmitted, convert(varchar, p.[start_date], 107) as [Date Submitted],  " & _
            " p.[close_date] as dtClosed, convert(varchar, p.[close_date], 107) as [Date Closed],  " & _
            "p.[id], p.title, u.fname, p.[start_date], s.sname, isnull(usr.fname, p.uid) as username " & _
             " , Priority, [status], " & _
            " replace(uemail, '''', 'replacedapostrophe') as uemail, replace(d.dname, '''', 'replacedapostrophe')  as department, replace(p.ulocation, '''', 'replacedapostrophe') as ulocation, " & _
            " replace(p.uComputerName, '''', 'replacedapostrophe') as uComputerName, replace(p.uphone, '''', 'replacedapostrophe') as uphone, " & _
            " replace(e.fname, '''', 'replacedapostrophe')  as entered_by, " & _
             " replace(c.cname, '''', 'replacedapostrophe') as category, rep, time_spent, " & _
            " convert(varchar, p.[start_date], 107) as start_date, convert(varchar,close_date, 107) as close_date, replace(#tempy.[Title], '''', 'replacedapostrophe') as title2, " & _
            " replace(#tempy.[description], '''', 'replacedapostrophe') as  [description], replace(#tempy.[solution], '''', 'replacedapostrophe') as solution, " & _
            "  Replace( #tempy.note, '''', 'replacedapostrophe') as [note] " & _
            "from #tempy " & _
            "join WebFD.FinanceHelpDesk.problems p on #tempy.ID = p.id " & _
            "left join WebFD.FinanceHelpDesk.tblUsers usr on p.uid = usr.uid and usr.Active = 1" & _
            "left join WebFD.[FinanceHelpDesk].tblUsers u on p.rep = u.[sid] and u.Active = 1 " & _
            "left join WebFD.[FinanceHelpDesk].[status] s on p.[status] = s.status_id " & _
            "left join WebFD.[FinanceHelpDesk].departments d on p.department = d.department_id  " & _
            "left join WebFD.[FinanceHelpDesk].categories c on p.category = c.category_id " & _
            "left join WebFD.[FinanceHelpDesk].priority pr on p.priority = pr.priority_id " & _
            " left join WebFD.FinanceHelpDesk.tblUsers e on p.entered_by = e.sid and e.Active = 1 " & _
            "where titlescore + descscore + solscore + notescore > 0 " & _
            "and (1=" & admin.ToString & " or kb = 1) " & _
            "order by titlescore*8 + descscore*4 + solscore*3 + notescore desc, " & _
            "dtSubmitted desc "

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        ds.Clear()
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand(searchsql, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

        End Using

        SearchView = ds.Tables(0).DefaultView
        gvSearchedCases.DataSource = SearchView
        gvSearchedCases.DataBind()

        Searchdir = 5
        Searchmap = ""

        If SearchView.Count > 0 Then
            pnlSearchResults.Visible = True
            gvSearchedCases.SelectedIndex = 0

            lblSearchCaseNumber.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(1).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchPriority.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(6).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchStatus.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(23).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchUserName.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(3).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEmail.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(8).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchDept.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(9).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            'lblSearchIP.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(10).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            'lblsearchCompName.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(11).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchPhone.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(12).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEntered.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(13).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchCategory.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(14).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchRep.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(15).Text), "&nbsp;", ""), "replacedapostrophe", "'")


            lblSearchTime.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchStartDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEndDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchTitle.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(19).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchDesc.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(20).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchSolution.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(21).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchNotes.Text = Replace(Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(22).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")

            If lblSearchStatus.Text = "CLOSED" And admin > 0 Then
                btnSearchReopenCase.Visible = True
            Else
                btnSearchReopenCase.Visible = False
            End If

            Dim phrase As String = Trim(Replace(Replace(txtEasySearch.Text, "'", "''"), ",", " "))
            Dim mini As String = ""
            While Len(phrase) > 0
                If InStr(phrase, " ") > 0 Then
                    mini = Trim(Left(phrase, InStr(phrase, " ")))
                    phrase = Trim(Mid(phrase, InStr(phrase, " ")))
                Else
                    mini = Trim(phrase)
                    phrase = ""
                End If
                lblSearchTitle.Text = Highlight(lblSearchTitle.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchDesc.Text = Highlight(lblSearchDesc.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchSolution.Text = Highlight(lblSearchSolution.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchNotes.Text = Highlight(lblSearchNotes.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
            End While

            If gvSearchedCases.Rows(0).Cells(7).Text = "100" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#4bff4b")
            ElseIf gvSearchedCases.Rows(0).Cells(6).Text = "3" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
            ElseIf gvSearchedCases.Rows(0).Cells(6).Text = "2" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
            ElseIf gvSearchedCases.Rows(0).Cells(6).Text = "1" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
            End If
        Else
            pnlSearchResults.Visible = False
            explanationlabelp3.Text = "No Results Found"
            ModalPopupExtenderp3.Show()
        End If


    End Sub

    Private Sub btnEasySearch_Click(sender As Object, e As EventArgs) Handles btnEasySearch.Click
        EasySearch()
    End Sub

    Private Sub gvSearchedCases_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSearchedCases.PageIndexChanging
        Try

            gvSearchedCases.PageIndex = e.NewPageIndex
            gvSearchedCases.DataSource = SearchView
            gvSearchedCases.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSearchedCases_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSearchedCases.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

                'e.Row.Cells(5).CssClass = "hidden"
                'e.Row.Cells(6).CssClass = "hidden"

                If Not IsDBNull(e.Row.DataItem("Status")) Then
                    If e.Row.DataItem("Status") = "100" Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                    ElseIf e.Row.DataItem("Priority") = "3" Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    ElseIf e.Row.DataItem("Priority") = "2" Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    ElseIf e.Row.DataItem("Priority") = "1" Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    End If

                End If

            End If

        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSearchedCases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSearchedCases.SelectedIndexChanged
        Try

            lblSearchCaseNumber.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(1).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchPriority.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(6).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchStatus.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(23).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchUserName.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(3).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEmail.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(8).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchDept.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(9).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            'lblSearchIP.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(10).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            'lblsearchCompName.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(11).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchPhone.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(12).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEntered.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(13).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchCategory.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(14).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchRep.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(15).Text), "&nbsp;", ""), "replacedapostrophe", "'")


            lblSearchTime.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchStartDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEndDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchTitle.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(2).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            'lblSearchTitle.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(2).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchDesc.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(20).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchSolution.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(21).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchNotes.Text = Replace(Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(22).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")

            If lblSearchStatus.Text = "CLOSED" And admin > 0 Then
                btnSearchReopenCase.Visible = True
            Else
                btnSearchReopenCase.Visible = False
            End If

            Dim phrase As String = Trim(Replace(txtEasySearch.Text, ",", " "))
            Dim mini As String = ""
            While Len(phrase) > 0
                If InStr(phrase, " ") > 0 Then
                    mini = Trim(Left(phrase, InStr(phrase, " ")))
                    phrase = Trim(Mid(phrase, InStr(phrase, " ")))
                Else
                    mini = Trim(phrase)
                    phrase = ""
                End If
                lblSearchTitle.Text = Highlight(lblSearchTitle.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchDesc.Text = Highlight(lblSearchDesc.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchSolution.Text = Highlight(lblSearchSolution.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchNotes.Text = Highlight(lblSearchNotes.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
            End While

            For Each canoe As GridViewRow In gvSearchedCases.Rows
                If canoe.RowIndex = gvSearchedCases.SelectedIndex Then
                    If canoe.Cells(7).Text = "100" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#4bff4b")
                    ElseIf canoe.Cells(6).Text = "3" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                    ElseIf canoe.Cells(6).Text = "2" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                    ElseIf canoe.Cells(6).Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                    End If

                Else
                    If canoe.Cells(7).Text = "100" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                    ElseIf canoe.Cells(6).Text = "3" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    ElseIf canoe.Cells(6).Text = "2" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    ElseIf canoe.Cells(6).Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    End If
                End If
            Next
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSearchedCases_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSearchedCases.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = SearchView

            sorts = e.SortExpression

            If e.SortExpression = Searchmap Then

                If Searchdir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    Searchdir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    Searchdir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                Searchdir = 1
                Searchmap = e.SortExpression
            End If

            gvSearchedCases.DataSource = dv
            gvSearchedCases.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Function Highlight(strText, strFind, strBefore, strAfter)
        Dim nPos
        Dim nLen
        Dim nLenAll

        nLen = Len(strFind)
        nLenAll = nLen + Len(strBefore) + Len(strAfter) + 1

        Highlight = strText

        If nLen > 0 And Len(Highlight) > 0 Then
            nPos = InStr(1, Highlight, strFind, 1)
            Do While nPos > 0
                Highlight = Left(Highlight, nPos - 1) & _
                    strBefore & Mid(Highlight, nPos, nLen) & strAfter & _
                    Mid(Highlight, nPos + nLen)

                nPos = InStr(nPos + nLenAll, Highlight, strFind, 1)
            Loop
        End If
    End Function

#End Region

    Private Sub ddlViewOtherCases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewOtherCases.SelectedIndexChanged

        Try

            populateOCGridview()

            Rowing()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

#Region "Tab Four"

    Sub populatecats()

        Try
            Dim sql As String
            If ChkActiveCategories.Checked Then
                sql = "select c.*, u.fname, u.uid, case when c.Active = 1 then 'Inactivate' else 'Activate' end as ActiveStatus from WebFD.FinanceHelpDesk.categories c " & _
    "left join WebFD.FinanceHelpDesk.tblUsers u on c.rep_id = u.sid and u.Active = 1" & _
    " and (c.rep_id = " & Replace(ddlCatReps.SelectedValue, "'", "''") & " or " & Replace(ddlCatReps.SelectedValue, "'", "''") & " = -1 ) " & _
    "order by cname"
            Else
                sql = "select c.*, u.fname, u.uid, case when c.Active = 1 then 'Inactivate' else 'Activate' end as ActiveStatus from WebFD.FinanceHelpDesk.categories c " & _
    "left join WebFD.FinanceHelpDesk.tblUsers u on c.rep_id = u.sid and u.Active = 1 where c.Active = 1 " & _
        " and (c.rep_id = " & Replace(ddlCatReps.SelectedValue, "'", "''") & " or " & Replace(ddlCatReps.SelectedValue, "'", "''") & " = -1 ) " & _
    "order by cname"
            End If

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(sql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            dv = ds.Tables(0).DefaultView
            gvCategories.DataSource = dv
            gvCategories.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub populatedeps()

        Try

            Dim sql As String
            If chkInactiveDeps.Checked Then
                sql = "select *, case when Active = 1 then 'Inactivate' else 'Activate' end as ActiveStatus from WebFD.FinanceHelpDesk.departments " & _
                        "where department_id <> 0 " & _
                        "order by dname"
            Else
                sql = "select *, case when Active = 1 then 'Inactivate' else 'Activate' end as ActiveStatus from WebFD.FinanceHelpDesk.departments " & _
                        "where department_id <> 0 and Active = 1 " & _
                        "order by dname"
            End If

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(sql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            dv = ds.Tables(0).DefaultView
            gvDepartments.DataSource = dv
            gvDepartments.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvCategories_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvCategories.RowCancelingEdit
        Try
            gvCategories.EditIndex = -1
            gvCategories.DataSource = dv
            gvCategories.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCategories_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvCategories.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvCategories.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If


    End Sub

    Private Sub gvCategories_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvCategories.RowDeleting
        Try
            Dim catid As String = gvCategories.DataKeys(e.RowIndex).Value.ToString
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Dim Sql As String = "update WebFD.FinanceHelpDesk.categories set Active = 1 - Active where category_id = '" & Replace(catid, "'", "''") & "'"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(Sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvCategories.EditIndex = -1
            populatecats()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCategories_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvCategories.RowEditing
        Try

            gvCategories.EditIndex = e.NewEditIndex
            gvCategories.DataSource = dv
            gvCategories.DataBind()

            Dim txtDept As TextBox = gvCategories.Rows(e.NewEditIndex).FindControl("txtCat")
            Dim lblDept As Label = gvCategories.Rows(e.NewEditIndex).FindControl("lblCat")
            Dim ddlRep As DropDownList = gvCategories.Rows(e.NewEditIndex).FindControl("ddlRep")
            Dim lblRep As Label = gvCategories.Rows(e.NewEditIndex).FindControl("lblRep")


            txtDept.Visible = True
            lblDept.Visible = False
            ddlRep.Visible = True
            lblRep.Visible = False


            For Each canoe As GridViewRow In gvCategories.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCategories_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvCategories.RowUpdating
        Try
            Dim catid As String = gvCategories.DataKeys(e.RowIndex).Value.ToString

            Dim txtDept As TextBox = gvCategories.Rows(e.RowIndex).FindControl("txtCat")
            Dim lblDept As Label = gvCategories.Rows(e.RowIndex).FindControl("lblCat")
            Dim ddlRep As DropDownList = gvCategories.Rows(e.RowIndex).FindControl("ddlRep")
            Dim lblRep As Label = gvCategories.Rows(e.RowIndex).FindControl("lblRep")

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim Sql As String = "update WebFD.FinanceHelpDesk.categories " & _
                "set cname = '" & Replace(txtDept.Text, "'", "''") & "', " & _
                "rep_id = '" & Replace(ddlRep.SelectedValue, "'", "''") & "' " & _
                "where category_id = '" & Replace(catid, "'", "''") & "'"



            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(Sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvCategories.EditIndex = -1
            populatecats()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCategories.SelectedIndexChanged
        For Each canoe As GridViewRow In gvCategories.Rows
            If canoe.RowIndex = gvCategories.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub gvCategories_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvCategories.Sorting
        Try

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub ddlManageWhat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageWhat.SelectedIndexChanged

        If ddlManageWhat.SelectedValue = 1 Then
            populatecats()
            pnlCats.Visible = True
            pnlDeps.Visible = False
            pnlUsers.Visible = False
        ElseIf ddlManageWhat.SelectedValue = 2 Then
            populatedeps()
            pnlDeps.Visible = True
            pnlCats.Visible = False
            pnlUsers.Visible = False
        ElseIf ddlManageWhat.SelectedValue = 3 Then  'Users'
            PopulateUsers()
            pnlDeps.Visible = False
            pnlCats.Visible = False
            pnlUsers.Visible = True

            ' Clear out form ' 
            lblUsrLogin0.Text = ""
            txtUsrFirstName0.Text = ""
            txtUsrLastName0.Text = ""
            txtUsrEmail0.Text = ""
            txtUsrPhone0.Text = ""
            txtUsrIPAddress0.Text = ""
            txtUsrDepartment.Text = ""
            txtUsrCompName0.Text = ""
            chkbx_is_rep.Checked = False

        End If



    End Sub


    Private Sub gvDepartments_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDepartments.RowCancelingEdit
        Try
            gvDepartments.EditIndex = -1
            gvDepartments.DataSource = dv
            gvDepartments.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvDepartments_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvDepartments.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvDepartments.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If
    End Sub


    Private Sub gvDepartments_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvDepartments.RowDeleting
        Try
            Dim depid As String = gvDepartments.DataKeys(e.RowIndex).Value.ToString
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Dim Sql As String = "update WebFD.FinanceHelpDesk.departments set Active = 1 - Active where department_id = '" & Replace(depid, "'", "''") & "'"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(Sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvDepartments.EditIndex = -1
            populatedeps()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvDepartments_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvDepartments.RowEditing
        Try

            gvDepartments.EditIndex = e.NewEditIndex
            gvDepartments.DataSource = dv
            gvDepartments.DataBind()

            Dim txtDept As TextBox = gvDepartments.Rows(e.NewEditIndex).FindControl("txtDept")
            Dim lblDept As Label = gvDepartments.Rows(e.NewEditIndex).FindControl("lblDept")

            txtDept.Visible = True
            lblDept.Visible = False


            For Each canoe As GridViewRow In gvDepartments.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvDepartments_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvDepartments.RowUpdating
        Try
            Dim depid As String = gvDepartments.DataKeys(e.RowIndex).Value.ToString

            Dim txtDept As TextBox = gvDepartments.Rows(e.RowIndex).FindControl("txtDept")
            Dim lblDept As Label = gvDepartments.Rows(e.RowIndex).FindControl("lblDept")

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim Sql As String = "update WebFD.FinanceHelpDesk.departments " & _
                "set dname = '" & Replace(txtDept.Text, "'", "''") & "' " & _
                "where department_id = '" & Replace(depid, "'", "''") & "'"



            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(Sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvDepartments.EditIndex = -1
            populatedeps()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvDepartments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDepartments.SelectedIndexChanged
        For Each canoe As GridViewRow In gvDepartments.Rows
            If canoe.RowIndex = gvCategories.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub gvManageUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvManageUsers.SelectedIndexChanged
        ' Use HTML decode to decode characters like ;nbsp -> " ", then you can trim the space
        lblUsrLogin0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(2).Text).Trim()
        txtUsrFirstName0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(3).Text).Trim()
        txtUsrLastName0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(4).Text).Trim()
        txtUsrEmail0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(5).Text).Trim()
        txtUsrPhone0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(6).Text).Trim()
        txtUsrIPAddress0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(7).Text).Trim()
        txtUsrCompName0.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(8).Text).Trim()
        txtUsrDepartment.Text = HtmlDecode(gvManageUsers.SelectedRow.Cells(9).Text).Trim()

        If HtmlDecode(gvManageUsers.SelectedRow.Cells(10).Text).Trim() = 0 Then
            chkbx_is_rep.Checked = False
        Else
            chkbx_is_rep.Checked = True
        End If

        If HtmlDecode(gvManageUsers.SelectedRow.Cells(11).Text).Trim() = 0 Then
            chkbx_is_active.Checked = False
        Else
            chkbx_is_active.Checked = True
        End If

        tblEditUserProfile.Visible = True
        'BtnAddorUpdate.Visible = True

    End Sub

    Private Sub gvManageUsers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvManageUsers.PageIndexChanging
        Try

            gvManageUsers.PageIndex = e.NewPageIndex
            gvManageUsers.DataBind()
            PopulateUsers()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    Private Sub btnAddorUpdateProfile_Click(sender As Object, e As EventArgs) Handles btnAddorUpdateProfile.Click

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        '' Update Options ''
        Dim UpdateOptions(10) As String

        ' Using an array to be able to easily access all elements on the fly '
        UpdateOptions(0) = Replace(lblUsrLogin0.Text, "'", "''")
        UpdateOptions(1) = Replace(txtUsrFirstName0.Text, "'", "''")
        UpdateOptions(2) = Replace(txtUsrLastName0.Text, "'", "''")
        UpdateOptions(3) = Replace(txtUsrEmail0.Text, "'", "''")
        UpdateOptions(4) = Replace(txtUsrPhone0.Text, "'", "''")
        UpdateOptions(5) = Replace(txtUsrIPAddress0.Text, "'", "''")
        UpdateOptions(6) = Replace(txtUsrCompName0.Text, "'", "''")
        UpdateOptions(7) = Replace(txtUsrDepartment.Text, "'", "''")
        If chkbx_is_rep.Checked = True Then
            UpdateOptions(8) = "1"
        Else
            UpdateOptions(8) = "0"
        End If
        If chkbx_is_active.Checked = True Then
            UpdateOptions(9) = "1"
        Else
            UpdateOptions(9) = "0"
        End If

        ' Empty Form '
        lblUsrLogin0.Text = ""
        txtUsrFirstName0.Text = ""
        txtUsrLastName0.Text = ""
        txtUsrEmail0.Text = ""
        txtUsrPhone0.Text = ""
        txtUsrIPAddress0.Text = ""
        txtUsrCompName0.Text = ""
        txtUsrDepartment.Text = ""
        chkbx_is_rep.Checked = False

        For i = 0 To UBound(UpdateOptions)
            If UpdateOptions(i) = "" Then
                UpdateOptions(i) = "NULL"
            ElseIf UpdateOptions(i) <> "NULL" Then
                UpdateOptions(i) = "'" & UpdateOptions(i) & "'"
            End If
        Next i

        Dim MergeSql As String = "Merge into WebFD.FinanceHelpDesk.tblUsers A USING " & _
        "(Select (Select Max(sid)+1 from WebFD.FinanceHelpDesk.tblUsers) as sid, " & UpdateOptions(0) & " as UserID, " & UpdateOptions(1) & " as firstname, " & UpdateOptions(2) & " as lastname, " & UpdateOptions(3) & " as email, " & UpdateOptions(4) & _
        " as phone, " & UpdateOptions(5) & " as location1, " & UpdateOptions(6) & " as location2, " & UpdateOptions(7) & " as department, " & UpdateOptions(8) & " as isrep, " & UpdateOptions(9) & " as Active) B On A.UID = B.UserID WHEN NOT MATCHED THEN " & _
        "INSERT (sid, uid, firstname, lastname, email1, phone, location1, location2, department, isrep, Active) VALUES (b.sid, B.Userid, b.firstname, b.lastname, b.email, b.phone, b.location1, b.location2, b.department, b.isrep, b.active) " & _
        "WHEN MATCHED THEN UPDATE SET A.firstname = b.firstname, a.lastname = b.lastname, a.email1 = b.email, a.phone = b.phone, a.location1 = b.location1, a.location2 = b.location2, a.department = b.department, a.isrep = b.isrep, a.Active = b.Active;"

        Debug.Print(MergeSql)

        lblUsrLogin0.Text = ""
        txtUsrFirstName0.Text = ""
        txtUsrLastName0.Text = ""
        txtUsrEmail0.Text = ""
        txtUsrPhone0.Text = ""
        txtUsrIPAddress0.Text = ""
        txtUsrDepartment.Text = ""
        txtUsrCompName0.Text = ""


        ds.Clear()

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd = New SqlCommand(MergeSql, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")
            conn.Close()
        End Using

        'PopulateUsers()
        Search(Search_Users, e)

    End Sub

    Private Sub PopulateUsers()
        Dim search_Users_sql As String = "select * from WebFD.FinanceHelpDesk.tblUsers order by fname "

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds = New DataSet
            da = New SqlDataAdapter(search_Users_sql, conn)
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")


            gvManageUsers.DataSource = ds
            gvManageUsers.DataMember = "OData"
            gvManageUsers.DataBind()
            conn.Close()
        End Using


    End Sub




    Private Sub Search(sender As Object, e As System.EventArgs) Handles tbl_Search_UserID.TextChanged, tbl_Search_FirstName.TextChanged, tbl_Search_LastName.TextChanged

        Dim search_Users_sql As String = ""

        Try


            search_Users_sql = "select * from WebFD.FinanceHelpDesk.tblUsers Where uid like '%" & tbl_Search_UserID.Text & "%' and firstname like '%" & tbl_Search_FirstName.Text & "%' and lastname like '%" & tbl_Search_LastName.Text & "%';"

            'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    ds = New DataSet                                ' Get blank dataset to store our data
            '    da = New SqlDataAdapter(search_Users_sql, conn)              ' New connection and our select command
            '    da.SelectCommand.CommandTimeout = 86400         ' Set timeout
            '    Debug.Print(search_Users_sql)
            '    da.Fill(ds, "OData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?

            '    gvManageUsers.DataSource = ds
            '    gvManageUsers.DataMember = "OData"

            '    conn.Close()
            'End Using


            Dim ds As New DataSet
            Dim da As New SqlDataAdapter

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet
                da = New SqlDataAdapter(search_Users_sql, conn)
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")


                gvManageUsers.DataSource = ds
                gvManageUsers.DataMember = "OData"
                gvManageUsers.DataBind()
                conn.Close()
            End Using






        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnAddCat_Click(sender As Object, e As EventArgs) Handles btnAddCat.Click

        Dim sql As String = "Insert into WebFD.FinanceHelpDesk.categories (category_id, cname, rep_id, Active) values ( " & _
            "(select MAX(category_id) + 1 from WebFD.FinanceHelpDesk.categories), '" & Replace(txtNewCatName.Text, "'", "''") & _
            "', '" & Replace(ddlNewCatRep.SelectedValue, "'", "''") & "', 1)"

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            cmd = New SqlCommand(sql, conn)
            cmd.CommandTimeout = 86400
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
        End Using

        txtNewCatName.Text = ""
        ddlNewCatRep.SelectedIndex = 0
        gvCategories.EditIndex = -1
        populatecats()

        explanationlabelp4.Text = "New Category Added"
        ModalPopupExtenderp4.Show()

    End Sub

    Private Sub btnNewDep_Click(sender As Object, e As EventArgs) Handles btnNewDep.Click

        Dim sql As String = "Insert into WebFD.FinanceHelpDesk.departments (department_id, dname, Active) values ( " & _
            "(select MAX(department_id) + 1 from WebFD.FinanceHelpDesk.departments), '" & Replace(txtNewDepName.Text, "'", "''") & _
            "', 1)"

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            cmd = New SqlCommand(sql, conn)
            cmd.CommandTimeout = 86400
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
        End Using

        txtNewDepName.Text = ""
        gvDepartments.EditIndex = -1
        populatedeps()

        explanationlabelp4.Text = "New Department Added"
        ModalPopupExtenderp4.Show()

    End Sub


#End Region

    Private Sub btnSpecificID_Click(sender As Object, e As EventArgs) Handles btnSpecificID.Click
        If User.Identity.IsAuthenticated Then
            If IsNumeric(txtSpecificID.Text) Then
                'Dim url As String = "https://financeweb.northside.local/FinanceHelpDesk/FinanceHelpDeskCaseListing/?CaseNo=" & btnSpecificID.Text
                'btnSpecificID.OnClientClick = "javascript:window.open(""" & "../FinanceHelpDesk/FinanceHelpDeskCaseListing/?CaseNo=" & txtSpecificID.Text & """, 'FinanceHelpDeskCaseInfo', 'height=700,width=620, scrollbars, resizable');"
            Else
                explanationlabelp3.Text = "Please enter a valid Case Number"
                ModalPopupExtenderp3.Show()
            End If
        Else
            explanationlabelp3.Text = "Please Log In"
            ModalPopupExtenderp3.Show()
        End If

    End Sub



    'Private Sub BindSubmittingImagesGrid()

    '    Dim cmd As SqlCommand
    '    Dim da As New SqlDataAdapter
    '    Dim ds As New DataSet


    '    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        Dim Str As String = "SELECT * FROM WebFD.[FinanceHelpDesk].[tblAttachedFiles] where UserID = '" & _
    '            Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and ProblemID = -1"
    '        cmd = New SqlCommand(Str, conn)
    '        da.SelectCommand = cmd
    '        da.SelectCommand.CommandTimeout = 86400
    '        da.Fill(ds, "OData")


    '        gvAddingFiles.DataSource = ds

    '        gvAddingFiles.DataBind()

    '    End Using


    'End Sub

    'Protected Sub UploadFileNewCase(sender As Object, e As EventArgs)

    '    Dim x As String = FileUpload1.FileName
    '    Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

    '    Dim contentType As String = FileUpload1.PostedFile.ContentType

    '    Using fs As Stream = FileUpload1.PostedFile.InputStream

    '        Using br As New BinaryReader(fs)

    '            Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
    '            Dim cmd As New SqlCommand

    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                Dim query As String = "INSERT INTO WebFD.[FinanceHelpDesk].[tblAttachedFiles] (ProblemID, FileName, ContentType, Content, UserID, DateAdded) VALUES (" & _
    '                    "-1, @FileName, @ContentType, @Content, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate())"

    '                cmd.Connection = conn
    '                cmd.CommandText = query

    '                cmd.Parameters.AddWithValue("@FileName", filename)
    '                cmd.Parameters.AddWithValue("@ContentType", contentType)
    '                cmd.Parameters.AddWithValue("@Content", bytes)

    '                cmd.ExecuteNonQuery()


    '            End Using



    '        End Using

    '    End Using

    '    BindSubmittingImagesGrid()

    '    'Response.Redirect(Request.Url.AbsoluteUri)


    'End Sub

    'Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
    '    UploadFileNewCase(sender, e)
    'End Sub

    'Private Sub gvAddingFiles_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAddingFiles.RowDataBound

    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        If Left(e.Row.Cells(1).Text, 5) = "image" Then
    '            Try

    '                Dim bytes As Byte() = TryCast(TryCast(e.Row.DataItem, DataRowView)("Content"), Byte())

    '                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)

    '                TryCast(e.Row.FindControl("Image1"), Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String

    '            Catch ex As Exception

    '                Dim img As Image = e.Row.FindControl("Image1")
    '                img.Visible = False
    '                Dim lbl As Label = e.Row.FindControl("lblType")
    '                lbl.Visible = True

    '            End Try
    '        Else
    '            Dim img As Image = e.Row.FindControl("Image1")
    '            img.Visible = False
    '            Dim lbl As Label = e.Row.FindControl("lblType")
    '            lbl.Visible = True
    '        End If

    '    End If
    'End Sub

    'Protected Sub NewCaseDeleteFile(sender As Object, e As EventArgs)

    '    Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

    '    Dim bytes As Byte()

    '    Dim fileName As String, contentType As String

    '    Dim cmd As New SqlCommand

    '    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        cmd.CommandText = "Delete from WebFD.[FinWeb].[UserBlobs] where [FileId]=@Id"

    '        cmd.Parameters.AddWithValue("@Id", id)

    '        cmd.Connection = conn

    '        Using sdr As SqlDataReader = cmd.ExecuteReader()

    '            sdr.Read()

    '            bytes = DirectCast(sdr("Content"), Byte())

    '            contentType = sdr("ContentType").ToString()

    '            fileName = sdr("FileName").ToString()

    '        End Using


    '    End Using
    'End Sub

    Private Sub lbAdvSearch_Click(sender As Object, e As EventArgs) Handles lbAdvSearch.Click
        pnlAdvSearch.Visible = True
    End Sub

    Private Sub ddlAdvSearchAssignedTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvSearchAssignedTo.SelectedIndexChanged
        If ddlAdvSearchAssignedTo.SelectedValue = -1 Then
            txtAdvSearchAssignedTo.Visible = True
        Else
            txtAdvSearchAssignedTo.Visible = False
        End If
    End Sub

    Private Sub ddlAdvSearchReportedBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvSearchReportedBy.SelectedIndexChanged
        If ddlAdvSearchReportedBy.SelectedValue = "(Text Search)" Then
            txtAdvSearchReportedBy.Visible = True
        Else
            txtAdvSearchReportedBy.Visible = False
        End If
    End Sub

    Private Sub ddlAdvSearchSubmittedBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvSearchSubmittedBy.SelectedIndexChanged
        If ddlAdvSearchSubmittedBy.SelectedValue = "-1" Then
            txtAdvSearchSubmittedBy.Visible = True
        Else
            txtAdvSearchSubmittedBy.Visible = False
        End If
    End Sub

    Private Sub lbCloseAdv_Click(sender As Object, e As EventArgs) Handles lbCloseAdv.Click
        pnlAdvSearch.Visible = False
        pnlEasySearch.Visible = True
    End Sub

    Private Sub lbResetAdv_Click(sender As Object, e As EventArgs) Handles lbResetAdv.Click
        ddlAdvSearchAssignedTo.SelectedIndex = 0
        ddlAdvSearchCategory.SelectedIndex = 0
        ddlAdvSearchDateOptions.SelectedIndex = 0
        ddlAdvSearchDepartment.SelectedIndex = 0
        ddlAdvSearchPriority.SelectedIndex = 0
        ddlAdvSearchReportedBy.SelectedIndex = 0
        ddlAdvSearchStatus.SelectedIndex = 0
        txtAdvSearchAssignedTo.Text = ""
        txtAdvSearchEndDate.Text = ""
        txtAdvSearchKeywords.Text = ""
        txtAdvSearchReportedBy.Text = ""
        txtAdvSearchStartDate.Text = ""
        txtAdvSearchSubmittedBy.Text = ""
        txtAdvSearchAssignedTo.Visible = False
        txtAdvSearchReportedBy.Visible = False
        txtAdvSearchSubmittedBy.Visible = False
        trAdvSearchDates.Visible = False
    End Sub

    Sub AdvSearch()

        Dim searchsql As String

        searchsql = "declare @Phrase varchar(max) = '" & Replace(Replace(txtAdvSearchKeywords.Text, "'", "''"), ",", " ") & "' " & _
             "declare @Mini varchar(max) " & _
             "" & _
             "if OBJECT_ID('tempdb..#tempy') is not null " & _
             "begin " & _
             "   drop table #tempy " & _
             "End " & _
             ""

        If Trim(Replace(Replace(txtAdvSearchKeywords.Text, "'", "''"), ",", " ")) = "" Then
            searchsql += " Select 1 as titlescore, 1 as descscore, 1 as solscore, 1 as notescore, 1 as attchscore, "
        Else
            searchsql += " Select 0 as titlescore, 0 as descscore, 0 as solscore, 0 as notescore, 0 as attchscore, "
        End If

        searchsql += " id, title, description, solution, " & _
             "(select convert(varchar(max),note) + ' -- ' + convert(varchar, n.addDate) + ';newline;' from WebFD.FinanceHelpDesk.tblNotes n " & _
             "where n.id = p.id and (n.[private] = 0 or n.[uid] = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "')" & _
             "    order by n.addDate asc for XML Path('')) AS note, " & _
             "(select convert(varchar(max),FileName) + ' -- ' + convert(varchar, n.DateAdded) + ';newline;' from WebFD.FinanceHelpDesk.tblAttachedFiles n " & _
             "where n.ProblemID = p.id  " & _
             " order by n.DateAdded asc for XML Path('')) AS attachs " & _
             "into #tempy " & _
             "from WebFD.FinanceHelpDesk.problems p " & _
             "where 1=1 "


        If ddlAdvSearchAssignedTo.SelectedValue > 0 Then
            searchsql += " and rep = '" & ddlAdvSearchAssignedTo.SelectedValue & "' "
        ElseIf ddlAdvSearchAssignedTo.SelectedValue = -1 Then
            searchsql += " and (rep in (select uid from  WebFD.FinanceHelpDesk.tblUsers where fname like '%" & Replace(txtAdvSearchAssignedTo.Text, "'", "''") & "%') " & _
                " or rep = '" & Replace(txtAdvSearchAssignedTo.Text, "'", "''") & "')"
        End If

        If ddlAdvSearchCategory.SelectedValue <> " -- (none selected) -- " Then
            searchsql += " and category = '" & ddlAdvSearchCategory.SelectedValue & "' "
        End If

        If ddlAdvSearchDepartment.SelectedValue <> " -- (none selected) -- " Then
            searchsql += " and department = '" & ddlAdvSearchDepartment.SelectedValue & "' "
        End If

        If ddlAdvSearchPriority.SelectedValue <> " -- (none selected) -- " Then
            searchsql += " and priority = '" & ddlAdvSearchPriority.SelectedValue & "' "
        End If

        If ddlAdvSearchStatus.SelectedValue <> " -- (none selected) -- " Then
            searchsql += " and status = '" & ddlAdvSearchStatus.SelectedValue & "' "
        End If

        If ddlAdvSearchSubmittedBy.SelectedValue > "0" Then
            If IsNumeric(ddlAdvSearchSubmittedBy.SelectedValue) Then
                searchsql += " and (entered_by in (select sid from  WebFD.FinanceHelpDesk.tblUsers where fname = '" & Replace(ddlAdvSearchSubmittedBy.SelectedValue, "'", "''") & "' and active = 1) " & _
                    " or entered_by = '" & Replace(ddlAdvSearchSubmittedBy.SelectedValue, "'", "''") & "')"
            Else
                searchsql += " and entered_by in (select sid from WebFD.FinanceHelpDesk.tblUsers where fname like '%" & Replace(ddlAdvSearchSubmittedBy.SelectedValue, "'", "''") & "%' and active = 1) "
            End If
        ElseIf ddlAdvSearchSubmittedBy.SelectedValue = "-1" Then
            searchsql += " and entered_by in (select sid from WebFD.FinanceHelpDesk.tblUsers where fname like '%" & Replace(txtAdvSearchSubmittedBy.Text, "'", "''") & "%' and active = 1) "
        End If

        If ddlAdvSearchReportedBy.SelectedValue <> "(Optional)" And ddlAdvSearchReportedBy.SelectedValue <> "(Text Search)" Then
            If IsNumeric(ddlAdvSearchReportedBy.SelectedValue) Then
                searchsql += " and (uid in (select uid from  WebFD.FinanceHelpDesk.tblUsers where fname = '" & Replace(ddlAdvSearchReportedBy.SelectedValue, "'", "''") & "' and active = 1) " & _
                    " or uid = '" & Replace(ddlAdvSearchReportedBy.SelectedValue, "'", "''") & "')"
            Else
                searchsql += " and uid in (select uid from  WebFD.FinanceHelpDesk.tblUsers where fname like '%" & Replace(ddlAdvSearchReportedBy.SelectedValue, "'", "''") & "%' and active = 1)  "
            End If

        ElseIf ddlAdvSearchReportedBy.SelectedValue = "(Text Search)" Then
            searchsql += " and uid in (select uid from  WebFD.FinanceHelpDesk.tblUsers where fname like '%" & Replace(txtAdvSearchReportedBy.Text, "'", "''") & "%' and active = 1) "

        End If

        If ddlAdvSearchDateOptions.SelectedValue = 1 Then
            searchsql += " and (convert(date, start_date) <= '" & Replace(txtAdvSearchEndDate.Text, "'", "''") & "' and " & _
                "isnull(convert(date, close_date), getdate()) >= '" & Replace(txtAdvSearchStartDate.Text, "'", "''") & "')"
        ElseIf ddlAdvSearchDateOptions.SelectedValue = 2 Then
            searchsql += " and convert(date, start_date) between '" & Replace(txtAdvSearchEndDate.Text, "'", "''") & "' and '" & _
                Replace(txtAdvSearchStartDate.Text, "'", "''") & "'"
        ElseIf ddlAdvSearchDateOptions.SelectedValue = 3 Then
            searchsql += " and convert(date, close_date) between '" & Replace(txtAdvSearchEndDate.Text, "'", "''") & "' and '" & _
                Replace(txtAdvSearchStartDate.Text, "'", "''") & "'"
        End If

        If chblAdvSearchAddtnl.Items(0).Selected Then
            searchsql += " and exists (select * from WebFD.FinanceHelpDesk.tblNotes nt where nt.id = p.id and (nt.[private] = 0 or nt.[uid] = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "')) "
        End If
        If chblAdvSearchAddtnl.Items(1).Selected Then
            searchsql += " and exists (select * from WebFD.FinanceHelpDesk.tblAttachedFiles x where x.ProblemID = p.id) "
        End If

        searchsql += "" & _
            "while LEN(@Phrase) > 0 " & _
            "begin " & _
            "if CHARINDEX(' ', @Phrase, 0) > 0 " & _
            "begin " & _
            "set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0)))) " & _
            "set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase)))) " & _
            "End " & _
            "Else " & _
            "begin " & _
            "set @Mini = ltrim(@Phrase) " & _
            "set @Phrase = '' " & _
            "End "

        If cblAdvSearchKeywords.Items(0).Selected Then
            searchsql += "update #tempy " & _
                "set titlescore = titlescore + 3 " & _
                "where " & _
                "title like '% ' + @Mini + ' %' " & _
                "or title like @Mini + ' %' " & _
                "or title like '% ' +@Mini " & _
                "or title = @Mini " & _
                "update #tempy " & _
                "set titlescore = titlescore + 1 " & _
                "where " & _
                "title like '%' + @Mini + '%' "
        End If

        If cblAdvSearchKeywords.Items(1).Selected Then
            searchsql += "update #tempy " & _
            "set descscore = descscore + 3 " & _
            "where " & _
            "description like '% ' + @Mini + ' %' " & _
            "or description like @Mini + ' %' " & _
            "or description like '% ' +@Mini " & _
            "or description = @Mini " & _
            "update #tempy " & _
            "set descscore = descscore + 1 " & _
            "where " & _
            "description like '%' + @Mini + '%' "
        End If

        If cblAdvSearchKeywords.Items(3).Selected Then
            searchsql += "update #tempy " & _
            "set solscore = solscore + 3 " & _
            "where " & _
            "solution like '% ' + @Mini + ' %' " & _
            "or solution like @Mini + ' %' " & _
            "or solution like '% ' +@Mini " & _
            "or solution = @Mini " & _
            "update #tempy " & _
            "set solscore = solscore + 1 " & _
            "where " & _
            "solution like '%' + @Mini + '%' "
        End If

        If cblAdvSearchKeywords.Items(2).Selected Then
            searchsql += "update #tempy " & _
            "set notescore = notescore + 3 " & _
            "where " & _
            "note like '% ' + @Mini + ' %' " & _
            "or note like @Mini + ' %' " & _
            " or note like '% ' +@Mini " & _
            "or note = @Mini " & _
            "update #tempy " & _
            "set notescore = notescore + 1 " & _
            "where " & _
            "note like '%' + @Mini + '%' "
        End If

        If cblAdvSearchKeywords.Items(4).Selected Then
            searchsql += "update #tempy " & _
            "set attchscore = attchscore + 3 " & _
            "where " & _
            "attachs like '% ' + @Mini + ' %' " & _
            "or attachs like @Mini + ' %' " & _
            " or attachs like '% ' +@Mini " & _
            "or attachs = @Mini " & _
            "update #tempy " & _
            "set attchscore = attchscore + 1 " & _
            "where " & _
            "attachs like '%' + @Mini + '%' "
        End If


        searchsql += "End " & _
            "select titlescore*8 + descscore*4 + solscore*3 + notescore + attchscore AS ord,  " & _
            " p.[start_date] as dtSubmitted, convert(varchar, p.[start_date], 107) as [Date Submitted],  " & _
            " p.[close_date] as dtClosed, convert(varchar, p.[close_date], 107) as [Date Closed],  " & _
            "p.[id], p.title, u.fname, p.[start_date], s.sname, isnull(usr.fname, p.uid) as username " & _
             " , Priority, [status], " & _
            " replace(uemail, '''', 'replacedapostrophe') as uemail, replace(d.dname, '''', 'replacedapostrophe')  as department, replace(p.ulocation, '''', 'replacedapostrophe') as ulocation, " & _
            " replace(p.uComputerName, '''', 'replacedapostrophe') as uComputerName, replace(p.uphone, '''', 'replacedapostrophe') as uphone, " & _
            " replace(e.fname, '''', 'replacedapostrophe')  as entered_by, " & _
             " replace(c.cname, '''', 'replacedapostrophe') as category, rep, time_spent, " & _
            " convert(varchar, p.[start_date], 107) as start_date, convert(varchar,close_date, 107) as close_date, replace(#tempy.[Title], '''', 'replacedapostrophe') as title2, " & _
            " replace(#tempy.[description], '''', 'replacedapostrophe') as  [description], replace(#tempy.[solution], '''', 'replacedapostrophe') as solution, " & _
            "  Replace( #tempy.note, '''', 'replacedapostrophe') as [note] " & _
            "from #tempy " & _
            "join WebFD.FinanceHelpDesk.problems p on #tempy.ID = p.id " & _
            "left join WebFD.FinanceHelpDesk.tblUsers usr on p.uid = usr.uid and usr.active = 1 " & _
            "left join WebFD.[FinanceHelpDesk].tblUsers u on p.rep = u.[sid]  and u.active = 1 " & _
            "left join WebFD.[FinanceHelpDesk].[status] s on p.[status] = s.status_id " & _
            "left join WebFD.[FinanceHelpDesk].departments d on p.department = d.department_id  " & _
            "left join WebFD.[FinanceHelpDesk].categories c on p.category = c.category_id " & _
            "left join WebFD.[FinanceHelpDesk].priority pr on p.priority = pr.priority_id " & _
            " left join WebFD.FinanceHelpDesk.tblUsers e on p.entered_by = e.sid and e.active = 1 " & _
            "where titlescore + descscore + solscore + notescore > "

        If Trim(Replace(Replace(txtAdvSearchKeywords.Text, "'", "''"), ",", " ")) = "" Then
            searchsql += " -1 "
        Else
            searchsql += " 0 "
        End If

        searchsql += "and (0<" & admin.ToString & " or kb = 1) " & _
            "order by titlescore*8 + descscore*4 + solscore*3 + notescore desc, " & _
            "dtSubmitted desc "

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        ds.Clear()
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand(searchsql, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

        End Using

        SearchView = ds.Tables(0).DefaultView
        gvSearchedCases.DataSource = SearchView
        gvSearchedCases.DataBind()

        Searchdir = 5
        Searchmap = ""

        If SearchView.Count > 0 Then
            pnlSearchResults.Visible = True
            gvSearchedCases.SelectedIndex = 0

            lblSearchCaseNumber.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(1).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchPriority.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(6).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchStatus.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(23).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchUserName.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(3).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEmail.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(8).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchDept.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(9).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            'lblSearchIP.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(10).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            'lblsearchCompName.Text = Replace(Replace(gvSearchedCases.SelectedRow.Cells(11).Text, "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchPhone.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(12).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEntered.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(13).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchCategory.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(14).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchRep.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(15).Text), "&nbsp;", ""), "replacedapostrophe", "'")


            lblSearchTime.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchStartDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchEndDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchTitle.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(19).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchDesc.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(20).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchSolution.Text = Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(21).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSearchNotes.Text = Replace(Replace(Replace(Server.HtmlDecode(gvSearchedCases.SelectedRow.Cells(22).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")

            If lblSearchStatus.Text = "CLOSED" And admin > 0 Then
                btnSearchReopenCase.Visible = True
            Else
                btnSearchReopenCase.Visible = False
            End If

            Dim phrase As String = Trim(Replace(Replace(txtAdvSearchKeywords.Text, "'", "''"), ",", " "))
            Dim mini As String = ""
            While Len(phrase) > 0
                If InStr(phrase, " ") > 0 Then
                    mini = Trim(Left(phrase, InStr(phrase, " ")))
                    phrase = Trim(Mid(phrase, InStr(phrase, " ")))
                Else
                    mini = Trim(phrase)
                    phrase = ""
                End If
                lblSearchTitle.Text = Highlight(lblSearchTitle.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchDesc.Text = Highlight(lblSearchDesc.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchSolution.Text = Highlight(lblSearchSolution.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblSearchNotes.Text = Highlight(lblSearchNotes.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
            End While

            If gvSearchedCases.Rows(0).Cells(7).Text = "100" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#4bff4b")
            ElseIf gvSearchedCases.Rows(0).Cells(6).Text = "3" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
            ElseIf gvSearchedCases.Rows(0).Cells(6).Text = "2" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
            ElseIf gvSearchedCases.Rows(0).Cells(6).Text = "1" Then
                gvSearchedCases.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
            End If
        Else
            pnlSearchResults.Visible = False
            explanationlabelp3.Text = "No Results Found"
            ModalPopupExtenderp3.Show()
        End If


    End Sub

    Private Sub btnAdvSearch_Click(sender As Object, e As EventArgs) Handles btnAdvSearch.Click
        AdvSearch()

        pnlAdvSearch.Visible = False
    End Sub

    Private Sub ddlAdvSearchDateOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdvSearchDateOptions.SelectedIndexChanged
        If ddlAdvSearchDateOptions.SelectedValue = 0 Then
            trAdvSearchDates.Visible = False
        Else
            trAdvSearchDates.Visible = True
        End If
    End Sub

    Private Sub ColorCheck()
        For Each canoe As GridViewRow In gvSearchedCases.Rows
            If canoe.RowIndex = gvSearchedCases.SelectedIndex Then
                If canoe.Cells(7).Text = "100" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#4bff4b")
                ElseIf canoe.Cells(6).Text = "3" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                ElseIf canoe.Cells(6).Text = "2" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                ElseIf canoe.Cells(6).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                End If

            Else
                If canoe.Cells(7).Text = "100" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                ElseIf canoe.Cells(6).Text = "3" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                ElseIf canoe.Cells(6).Text = "2" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                ElseIf canoe.Cells(6).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                End If
            End If
        Next

        If admin = 1 Then

            Rowing()
        End If

    End Sub

    Private Sub btnUpdateProfile_Click(sender As Object, e As EventArgs) Handles btnUpdateProfile.Click

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        If Trim(Replace(txtUsrFirstName.Text, "'", "''")) = "" Then

        End If

        tpFinHelpMain.Visible = True

        If logged = 0 Then
            Dim insertsql As String = "Insert into WebFD.FinanceHelpDesk.tblUsers (sid, uid, fname, email1, phone, location1, location2, department, IsRep, dtCreated, firstname, lastname, Active) values (" & _
                "(select MAX(sid) + 1 from WebFD.FinanceHelpDesk.tblUsers), '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & Replace(txtUsrFirstName.Text, "'", "''") & " " & Replace(txtUsrLastName.Text, "'", "''") & "', '" & _
                Replace(txtUsrEmail.Text, "'", "''") & "', '" & Replace(txtUsrPhone.Text, "'", "''") & "', '" & Replace(txtUsrIPAddress.Text, "'", "''") & "', '" & Replace(txtUsrCompName.Text, "'", "''") & _
                "', '" & Replace(ddlUsrDepartment.SelectedValue, "'", "''") & "', 0, getdate(), '" & Replace(txtUsrFirstName.Text, "'", "''") & "', '" & Replace(txtUsrLastName.Text, "'", "''") & "', 1)"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(insertsql, conn)
                cmd.ExecuteNonQuery()

            End Using


            Dim userssql As String = "Select isnull([uid], '') + case when fname is null then '' when fname = 'NULL' then '' else  ' (' + isnull([fname], '') + ')' end as usr, " & _
           "sid from WebFD.FinanceHelpDesk.tblUsers where Active = 1 order by fname"


            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(userssql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using


            ddlSelectUser.DataSource = ds
            ddlSelectUser.DataValueField = "sid"
            ddlSelectUser.DataTextField = "usr"
            ddlSelectUser.DataBind()

            ddlSelectUser.SelectedValue = "null"

            Try
                For Each choice As ListItem In ddlSelectUser.Items
                    If InStr(choice.Text, " (") > 0 Then
                        If Left(choice.Text, InStr(choice.Text, " (") - 1) = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") Then
                            ddlSelectUser.SelectedValue = choice.Value
                        End If
                    Else
                        Dim x As String = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
                        If choice.Text = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") Then
                            ddlSelectUser.SelectedValue = choice.Value
                        End If
                    End If

                Next
            Catch ex As Exception
                ddlSelectUser.SelectedValue = "null"
            End Try

            userinfo(1)

            If ddlSelectUser.SelectedValue <> "null" Then
                logged = 1
                tpFinHelpMain.Enabled = True
                tcFinanceHelpDesk.ActiveTabIndex = 0
            End If

        Else
            Dim upsql As String = "Update Usrs set fname = '" & Replace(txtUsrFirstName.Text, "'", "''") & " " & Replace(txtUsrLastName.Text, "'", "''") & "', email1 = '" & _
                Replace(txtUsrEmail.Text, "'", "''") & "', phone = '" & Replace(txtUsrPhone.Text, "'", "''") & "', location1 = '" & Replace(txtUsrIPAddress.Text, "'", "''") & _
                "', location2 = '" & Replace(txtUsrCompName.Text, "'", "''") & "', department = '" & Replace(ddlUsrDepartment.SelectedValue, "'", "''") & "', firstname = '" & _
                Replace(txtUsrFirstName.Text, "'", "''") & "', lastname = '" & Replace(txtUsrLastName.Text, "'", "''") & "' , Active = 1" & _
                "from WebFD.FinanceHelpDesk.tblUsers Usrs where uid = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                cmd = New SqlCommand(upsql, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()

            End Using

            If admin = 1 Then
                userinfo(2)
            Else
                userinfo(1)
            End If


        End If
    End Sub

    Private Sub ddlSelectCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectCategory.SelectedIndexChanged
        Try
            Dim sql As String = "select rep_id from WebFD.FinanceHelpDesk.categories where category_id = '" & Replace(ddlSelectCategory.SelectedValue, "'", "''") & "'"
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ddlAssignUser.SelectedValue = cmd.ExecuteScalar

            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearchReopenCase_Click(sender As Object, e As EventArgs) Handles btnSearchReopenCase.Click
        Try

            Dim sql As String = "Update WebFD.FinanceHelpDesk.problems set status = 1 where id = '" & lblSearchCaseNumber.Text & "';"
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ddlAssignUser.SelectedValue = cmd.ExecuteScalar

            End Using

            If admin > 0 Then
                'btnSearchReopenCase.Visible = False
                lblSearchStatus.Text = "OPEN"
                populateOCGridview()
                Rowing()
            End If

            MakeAccordion(3)
            MakeAccordion(2)
            MakeAccordion(1)
            MakeAccordion(100)

            Accordion1.CssClass = "accordion"
            Accordion2.CssClass = "accordion"
            Accordion3.CssClass = "accordion"

            If acc1 = 0 Then
                Accordion1.CssClass = "hidden"
            End If
            If acc2 = 0 Then
                Accordion2.CssClass = "hidden"
            End If
            If acc3 = 0 Then
                Accordion3.CssClass = "hidden"
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub chkInactiveDeps_CheckedChanged(sender As Object, e As EventArgs) Handles chkInactiveDeps.CheckedChanged
        populatedeps()
    End Sub

    Private Sub ddlCatReps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCatReps.SelectedIndexChanged
        populatecats()
    End Sub

    Private Sub ChkActiveCategories_CheckedChanged(sender As Object, e As EventArgs) Handles ChkActiveCategories.CheckedChanged
        populatecats()
    End Sub
End Class