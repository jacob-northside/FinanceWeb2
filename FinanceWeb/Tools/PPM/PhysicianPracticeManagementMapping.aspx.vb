Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security


Imports FinanceWeb.WebFinGlobal

Public Class PhysicianPracticeManagementMapping
    Inherits System.Web.UI.Page
    Public Shared MappedView As New DataView
    Public Shared UnMappedView As New DataView
    Public Shared sortmap As String
    Public Shared sortunmap As String
    Public Shared mapdir As Integer
    Public Shared unmapdir As Integer
    Private Shared Admin As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                Select Case Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")
                    Case "e218173"
                        Admin = 1
                    Case "cw996788"
                        Admin = 1
                    Case "mf995052"
                        Admin = 1
                    Case "e213842"
                        Admin = 1

                End Select

                PopulateEnterDropDownList()
                PopulateEnterPracticeDropDownList()
                PopulateEnterRoleDropDownList()
                PopulateGridView()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
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
    Private Sub PopulateEnterDropDownList()

        'Dim s As String = "select distinct SourceSystem, SourceSystem as Display, 1 as ord from DWH.UD.ppm_locations where Active = 1 union all select '-1', 'Select Source System (Optional)', 0 order by ord, Display"
        'Add synch 1/20/2020 CRW
        Dim s As String = "insert into  DWH.UD.ppm_locations " & _
            "select (select max(ID) from DWH.UD.ppm_locations) + DENSE_RANK() over (order by SourceSystem) " & _
            ", SourceSystem, Process, 1, getdate() from DWH.Dbo.DWHProcesses_LU a where DataSource = 'Galen' and ACTIVE = 1 " & _
            "and not exists (select * from  DWH.UD.ppm_locations l where l.SourceSyStem = a.SourceSystem and l.Active = 1) "

        s += "select distinct SourceSystem, SourceSystem as Display, 1 as ord from DWH.UD.ppm_locations where Active = 1 union all select '-1', 'Select Source System (Optional)', 0 order by ord, Display"

        ddlEnterSourceSystem.DataSource = GetData(s)
        ddlEnterSourceSystem.DataTextField = "Display"
        ddlEnterSourceSystem.DataValueField = "SourceSystem"
        ddlEnterSourceSystem.DataBind()


    End Sub
    Private Sub PopulateEnterPracticeDropDownList()

        Dim s As String = "select 'Select Practice' as PracticeName, -1 as ID, 0 as ord union all " & _
            "select PracticeName, ID, 1 from DWH.UD.ppm_locations l " & _
            "join dwh.dbo.DWHProcesses_LU p on l.GroupID = p.Process and p.SourceSystem = l.SourceSyStem " & _
            "where l.Active = 1 "
        If ddlEnterSourceSystem.SelectedValue = "-1" Then
        Else
            s += " and l.SourceSystem = '" & Replace(ddlEnterSourceSystem.SelectedValue, "'", "''") & "' "
        End If

        s += " order by ord, PracticeName"

        ddlEnterPractice.DataSource = GetData(s)
        ddlEnterPractice.DataTextField = "PracticeName"
        ddlEnterPractice.DataValueField = "ID"
        ddlEnterPractice.DataBind()

    End Sub
    Private Sub PopulateEnterRoleDropDownList()
        Dim s As String = "select '(Select Role)' as Role, -1 as ID, 0 as ord union all select Role, ID, 1 from DWH.UD.PPM_Role_LU where Active = 1 order by ord, Role"

        ddlEnterRole.DataSource = GetData(s)
        ddlEnterRole.DataTextField = "Role"
        ddlEnterRole.DataValueField = "ID"
        ddlEnterRole.DataBind()

    End Sub

    Private Sub ddlEnterSourceSystem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEnterSourceSystem.SelectedIndexChanged
        PopulateEnterPracticeDropDownList()
        PopulateGridView()
    End Sub

    Private Sub btnEnterNewRole_Click(sender As Object, e As EventArgs) Handles btnEnterNewRole.Click

        If ddlEnterPractice.SelectedValue = "-1" Then
            explantionlabel.Text = "Please select Practice"
            ModalPopupExtender1.Show()
            OkButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            Exit Sub
        End If

        If ddlEnterRole.SelectedValue = "-1" Then
            explantionlabel.Text = "Please select Role"
            ModalPopupExtender1.Show()
            OkButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            Exit Sub
        End If

        Dim x As String = "select count(*) from DWH.UD.PPM_Roles " & _
            "where Person = '" & Replace(txtEnterUserID.Text, "'", "''") & "' and Role = '" & Replace(ddlEnterRole.SelectedValue, "'", "''") & _
            "' and ID = '" & Replace(ddlEnterPractice.SelectedValue, "'", "''") & "' and Active = 1"

        If GetScalar(x) > 0 Then
            explantionlabel.Text = "This user/role/practice combination already exists"
            ModalPopupExtender1.Show()
            OkButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            Exit Sub
        End If

        explantionlabel.Text = "Please confirm selection"
        ModalPopupExtender1.Show()
        OkButton.Visible = False
        ConfirmButton.Visible = True
        CancelButton.Visible = True
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click

        Dim x As String = "Insert into DWH.UD.PPM_Roles (ID, Person, Role, Active, LastUpdated) " & _
            "values('" & Replace(ddlEnterPractice.SelectedValue, "'", "''") & "', '" & Replace(txtEnterUserID.Text, "'", "''") & "', '" & Replace(ddlEnterRole.SelectedValue, "'", "''") & "', 1, getdate() )"

        ExecuteSql(x)

        txtEnterUserID.Text = ""
        ddlEnterPractice.SelectedValue = "-1"
        ddlEnterRole.SelectedValue = "-1"
        ddlEnterSourceSystem.SelectedValue = "-1"

        explantionlabel.Text = "Role Added"
        ModalPopupExtender1.Show()
        OkButton.Visible = True
        ConfirmButton.Visible = False
        CancelButton.Visible = False

        PopulateGridView()

    End Sub


    Private Function ShowAllEntries()

        Dim x As String = "select r.Person, r.Role, l.SourceSyStem, l.GroupID, p.PracticeName, GroupRollup " & _
                    "from DWH.UD.PPM_Roles r " & _
                    "join DWH.UD.ppm_locations l on r.ID = l.ID  " & _
                    "join dwh.dbo.DWHProcesses_LU p on l.GroupID = p.Process and p.SourceSystem = l.SourceSyStem " & _
                    "where r.Active = 1 and l.Active = 1 "

        If ddlEnterSourceSystem.SelectedValue = "-1" Then
        Else
            x += " and l.SourceSystem = '" & Replace(ddlEnterSourceSystem.SelectedValue, "'", "''") & "' "
        End If

        If ddlEnterPractice.SelectedValue = "-1" Then
        Else
            x += " and l.ID = '" & Replace(ddlEnterPractice.SelectedValue, "'", "''") & "' "
        End If

        x += "order by SourceSystem, GroupID "


        Return GetData(x).DefaultView

    End Function

    Private Sub PopulateGridView()

        gvShowAllEntries.DataSource = ShowAllEntries()
        gvShowAllEntries.DataBind()


    End Sub

    Private Sub ddlEnterPractice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEnterPractice.SelectedIndexChanged
        PopulateGridView()
    End Sub

    Private Sub gvShowAllEntries_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShowAllEntries.PageIndexChanging
        Try

            Dim dv As DataView = ShowAllEntries()
            If entersortdir.Text <> "" Then

                If entersortdir.Text = "1" Then
                    dv.Sort = entersortmap.Text + " " + "asc"
                    entersortdir.Text = "1"

                Else
                    dv.Sort = entersortmap.Text + " " + "desc"
                    entersortdir.Text = 0

                End If
            End If

            gvShowAllEntries.PageIndex = e.NewPageIndex
            gvShowAllEntries.DataSource = dv
            gvShowAllEntries.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

 

    Private Sub gvShowAllEntries_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvShowAllEntries.Sorting
        Try


            Dim dv As DataView
            Dim sorts As String
            dv = ShowAllEntries()

            sorts = e.SortExpression
            Try
                If e.SortExpression = entersortmap.Text Then

                    If entersortdir.Text = "1" Then
                        dv.Sort = sorts + " " + "desc"
                        entersortdir.Text = 0
                    Else
                        dv.Sort = sorts + " " + "asc"
                        entersortdir.Text = "1"
                    End If

                Else
                    dv.Sort = sorts + " " + "asc"
                    entersortdir.Text = "1"
                    entersortmap.Text = e.SortExpression
                End If
            Catch ex As Exception
                dv.Sort = "SourceSystem asc"
                entersortdir.Text = "1"
                entersortmap.Text = "SourceSystem"
            End Try


            gvShowAllEntries.DataSource = dv
            gvShowAllEntries.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnClearPractice_Click(sender As Object, e As EventArgs) Handles btnClearPractice.Click

        If ddlEnterPractice.SelectedValue = "-1" Then
            explantionlabel.Text = "Please select Practice"
            ModalPopupExtender1.Show()
            OkButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False
            Exit Sub
        End If

        'Dim x As String = "Delete from DWH.UD.PPM_Roles  " & _
        '            "where ID = '" & Replace(ddlEnterPractice.SelectedValue, "'", "''") & "' "

        Dim x As String = "Update DWH.UD.PPM_Roles set Active = 0, LastUpdated = getdate() " & _
            " where Active = 1 and ID = '" & Replace(ddlEnterPractice.SelectedValue, "'", "''") & "' "

        ExecuteSql(x)

        txtEnterUserID.Text = ""
        ddlEnterPractice.SelectedValue = "-1"
        ddlEnterRole.SelectedValue = "-1"
        ddlEnterSourceSystem.SelectedValue = "-1"

        explantionlabel.Text = "Practice Cleared"
        ModalPopupExtender1.Show()
        OkButton.Visible = True
        ConfirmButton.Visible = False
        CancelButton.Visible = False

        PopulateGridView()



    End Sub
End Class