Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class FDAMammoImageReview
    Inherits System.Web.UI.Page


    'Private Shared CheatCheck As String = "ljackson"
    Private Shared WebAdminEmail As String = "chelsea.weirich@northside.com"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                CheckPermissions()

                If User.Identity.IsAuthenticated = True Then

                    'InitializeEvalForm()
                    InitializeManageDDL()
                    FlushStaging()
                    LoadYourTechRoster()
                    LoadPendingYourTechRoster()
                    LoadTechNames_Gridview()
                    Eval_GetTechList()
                    PopulateGridIssuesMain("")
                Else

                End If

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
    'Private Sub PopulateGrantAccess()

    '    Try
    '        Dim sql As String = " select '(Select Role)' as RoleFull, 0 as ord union all " & _
    '    " select RoleFull, 1 from WebFD.VendorContracts.Roles where Active = 1 order by ord, RoleFull"

    '        ddlGrantPosition.DataSource = GetData(sql)
    '        ddlGrantPosition.DataTextField = "RoleFull"
    '        ddlGrantPosition.DataValueField = "RoleFull"
    '        ddlGrantPosition.DataBind()

    '        Dim sql2 As String = " select '(Select Department)' as Deps, 0 as ord union all  select '(All Departments)' as RoleFull, 1 as ord union all " & _
    '   "select Convert(varchar, DepartmentNo) + ' - ' + isnull(DepartmentDisplayName, DepartmentName) , 2 " & _
    '   "from WebFD.VendorContracts.Department_LU where Active = 1 "

    '        ddlGrantDepartment.DataSource = GetData(sql2)
    '        ddlGrantDepartment.DataTextField = "Deps"
    '        ddlGrantDepartment.DataValueField = "Deps"
    '        ddlGrantDepartment.DataBind()
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub CheckPermissions()
        Try

            Dim x As String = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
            If Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") = "" Then
                lblOpenUserLogin.Text = "You are not logged in.  Please enter your credentials at the top to proceed."
            Else
                lblOpenUserLogin.Text = "Welcome, " & GetString("select  isnull(UserDisplayName, UserFullName) from DWH.RAD_Op.Mammo_IR_Users where Active = 1 and UserLogin = '" & _
                                                  Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'") & _
                                              ",  to the FDA Mammography Image Review Tool "
            End If

            If GetScalar("select  count(*) from DWH.RAD_Op.Mammo_IR_Users where Active = 1 and UserLogin = '" & _
                                                  Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Admin = 1") > 0 Then
                tpTechMonitoring.Visible = True
                cblFacsFilter_Get()
                GetTechMonitoringGrid()


            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Function AllUsers()
        Dim SQL As String = "Select UserLogin, isnull(UserFullName, UserDisplayName) as Name FROM DWH.RAD_Op.Mammo_IR_Users where Active = 1 and Tech = 1 order by 2"

        Return GetData(SQL).DefaultView



    End Function

    Protected Sub LoadTechNames_Gridview()

        Try
            ' Use adapter da to fill dataset ds with 'Lookup Data'?
            TechNames_Gridview.DataSource = AllUsers()
            TechNames_Gridview.DataMember = "LookUpData"
            TechNames_Gridview.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Protected Sub Insert_Eval_Results()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim SQL As String = "Select * FROM DWH.ONCOLOGY.ACRMAMMO_USERS;"


        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                TechNames_Gridview.DataSource = ds
                TechNames_Gridview.DataMember = "LookUpData"
                TechNames_Gridview.DataBind()
                conn.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub InitializeManageDDL()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select ID, Department from DWH.RAD_Op.Mammo_IR_Department_LU where Active = 1 order by Department;"

        Dim SQL As String = "Select d.ID,  d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay,  d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplayB, 1 as ord " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveTo, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "where dim.Active = 1 " & _
            "and dim.UserLogin = @SrchUsr union Select -1, 'Select Department (Optional)', 'Select Department (Required)', 0  order by ord, 2, 1"

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchUsr", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?

                ddlManageDepartments.DataSource = ds
                ddlManageDepartments.DataValueField = "ID"
                ddlManageDepartments.DataTextField = "DepartmentDisplayB"
                ddlManageDepartments.DataBind()


                TechFilterDDL.DataSource = ds
                TechFilterDDL.DataValueField = "ID"
                TechFilterDDL.DataTextField = "DepartmentDisplay"
                TechFilterDDL.DataBind()

                conn.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub InitializeEvalForm(x As String)
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select ID, Department from DWH.RAD_Op.Mammo_IR_Department_LU where Active = 1 order by Department;"

        Dim SQL As String = "Select d.ID,  d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay, 1 as ord " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveTo, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Techs d2t on d2t.Dept_ID= d.ID and d2t.Active = 1 and d2t.UserLogin = @TechUsr " & _
            "where dim.Active = 1 " & _
            "and dim.UserLogin = @SrchUsr union Select -1, 'Select Department', 0  order by ord, 2, 1"

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchUsr", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
                da.SelectCommand.Parameters.AddWithValue("@TechUsr", x)
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                EvalFacility_DDL.DataSource = ds
                EvalFacility_DDL.DataMember = "LookUpData"
                EvalFacility_DDL.DataValueField = "ID"
                EvalFacility_DDL.DataTextField = "DepartmentDisplay"
                EvalFacility_DDL.DataBind()


                conn.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Function YourTechs()

        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        'Dim SQL As String = "Select names.name Name, * from DWH.Rad_OP.Mammo_IR_Users dim " & _
        '                        "left join DWH.Rad_OP.FDAMAMMO_OrgChart_FACT fact " & _
        '                            "on dim.id = fact.supervisor and fact.Active = 1 " & _
        '                        "left join DWH.Rad_OP.FDAMAMMO_Users_DIM names " & _
        '                            "on fact.employee = names.id and names.Active = 1 " & _
        '                        "where dim.Active = 1 and dim.employee_number = @SrchID;"

        Dim SQL As String = "Select distinct usr.UserLogin, isnull(usr.UserDisplayName, usr.UserFullName) as DisplayName " & _
            "  " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveTo, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Techs d2t on d.ID = isnull(d2t.Dept_ID, d.ID) and d2t.Active = 1 and isnull(d2t.SupervisorLogin, dim.UserLogin) = dim.UserLogin " & _
            "	and getdate() between isnull(d2t.EffectiveFrom, '1/1/1800') and isnull(d2t.EffectiveTo, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Users usr on d2t.UserLogin = usr.UserLogin and usr.Active = 1 and usr.Tech = 1 " & _
            "where dim.Active = 1 and dim.UserLogin = @SrchID and (d.ID = @DepID or @DepID = -1) and (isnull(usr.UserDisplayName, usr.UserFullName) like '%' + @Usr + '%' or @Usr = '')  " & _
            "union " & _
        "Select distinct usr.UserLogin, isnull(usr.UserDisplayName, usr.UserFullName) as DisplayName " & _
            "  " & _
            "from DWH.RAD_Op.Mammo_IR_Department_LU d " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Techs d2t on d.ID = isnull(d2t.Dept_ID, d.ID) and d2t.Active = 1 " & _
            "	and getdate() between isnull(d2t.EffectiveFrom, '1/1/1800') and isnull(d2t.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Users usr on d2t.UserLogin = usr.UserLogin and usr.Active = 1 and usr.Tech = 1 " & _
            "join DWH.RAD_Op.Mammo_IR_Users admCheck on admCheck.Active = 1 and admCheck.Admin = 1 and admCheck.UserLogin = @SrchID " & _
            "where d.Active = 1 and (d.ID = @DepID or @DepID = -1) and (isnull(usr.UserDisplayName, usr.UserFullName) like '%' + @Usr + '%' or @Usr = '') order by 2"


        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchID", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
                da.SelectCommand.Parameters.AddWithValue("@DepID", TechFilterDDL.SelectedValue)
                da.SelectCommand.Parameters.AddWithValue("@Usr", Trim(txtFilterYourTechs.Text))
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(dt)                      ' Use adapter da to fill dataset ds with 'Lookup Data'?
                conn.Close()

            End Using

            Return dt.DefaultView

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Return dt.DefaultView
        End Try

    End Function

    Protected Sub Eval_GetTechList()

        YourTechs_Gridview.DataSource = YourTechs()
        YourTechs_Gridview.DataBind()


    End Sub

    Protected Sub FetchAccessionIDs(ByVal EmployeeID)
        Dim ds As DataTable
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select 'ACC1' UserID union all select 'acc2';"
        Dim SQL As String = "SELECT AccessionID, AccessionID + convert(varchar, [DateofImageReview], 109) + isnull(d.Department, img.Facility) as Display " & _
            " FROM [DWH].[RAD_Op].[FDAMammo_ImageEvaluations] Img " & _
            "  join DWH.RAD_Op.FDAMAMMO_Dept_DIM d on img.Dept_ID = d.ID and d.Active = 1 " & _
            "  where img.active = 1 and Reviewee = @Param1"

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataTable                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@Param1", EmployeeID)
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds)                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                conn.Close()
            End Using



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub


    Protected Sub LoadYourTechRoster()
        Dim ds As DataTable
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select 'ACC1' UserID union all select 'acc2';"
        Dim SQL As String = "Select distinct usr.UserLogin, isnull(usr.UserDisplayName, usr.UserFullName) as UserName " & _
            " /*,d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay */ " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveTo, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Techs d2t on d.ID = isnull(d2t.Dept_ID, d.ID) and d2t.Active = 1 and isnull(d2t.SupervisorLogin, dim.UserLogin) = dim.UserLogin " & _
            "	and getdate() between isnull(d2t.EffectiveFrom, '1/1/1800') and isnull(d2t.EffectiveTo, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Users usr on d2t.UserLogin = usr.UserLogin and usr.Active = 1 and usr.Tech = 1 " & _
            "where dim.Active = 1 and dim.UserLogin = @SrchID and (d.ID = @DepID or @DepID = -1) order by 2 "

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataTable                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchID", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
                da.SelectCommand.Parameters.AddWithValue("@DepID", ddlManageDepartments.SelectedValue)
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds)                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                YourTechRoster_Gridview.DataSource = ds
                YourTechRoster_Gridview.DataMember = "LookUpData"
                YourTechRoster_Gridview.DataBind()
                conn.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Protected Sub LoadPendingYourTechRoster()
        Dim ds As DataTable
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select 'ACC1' UserID union all select 'acc2';"
        Dim SQL As String = "Select usr.UserLogin, isnull(usr.UserDisplayName, usr.UserFullName) as UserName, d2t.Active " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim   " & _
            "join DWH.RAD_Op.STAGING_Mammo_IR_Dept_2_Techs d2t on d2t.SupervisorLogin = dim.UserLogin 	 " & _
            "join DWH.RAD_Op.Mammo_IR_Users usr on d2t.UserLogin = usr.UserLogin and usr.Active = 1 and usr.Tech = 1 " & _
            "where dim.Active = 1 and dim.UserLogin =  @SrchID and (isnull(d2t.Dept_ID, @DepID) = @DepID or @DepID = -1)"

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataTable                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchID", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
                da.SelectCommand.Parameters.AddWithValue("@DepID", ddlManageDepartments.SelectedValue)
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds)                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                gvPendingChanges.DataSource = ds
                gvPendingChanges.DataBind()
                conn.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub FlushStaging()

        Dim sql As String = "Delete from DWH.RAD_Op.STAGING_Mammo_IR_Dept_2_Techs where SupervisorLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            "' "

        ExecuteSql(sql)

    End Sub
    Private Sub ddlManageDepartments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageDepartments.SelectedIndexChanged
        FlushStaging()
        LoadYourTechRoster()
        LoadPendingYourTechRoster()
    End Sub
    Private Sub lbSrchUsr_Click(sender As Object, e As EventArgs) Handles lbSrchUsr.Click
        pnlSrchUser.Visible = True
        AddRemoveUser_Modal.Show()
    End Sub
    Private Sub lbCloseUsrSrch_Click(sender As Object, e As EventArgs) Handles lbCloseUsrSrch.Click
        txtAdminUsrSrch.Text = ""
        lblAdminUsrResults.Text = ""
        pnlSrchUser.Visible = False
        AddRemoveUser_Modal.Show()
    End Sub
    Private Sub btnAdminUsrSrch_Click(sender As Object, e As EventArgs) Handles btnAdminUsrSrch.Click
        Try
            lblAdminUsrResults.Text = ""
            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            Dim PulledString As String = "''"

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
                    PulledString += ", '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & "' "
                End If
            Next


            Dim FurtherSearch As String = "Select distinct " & _
                  "    isnull(substring( " & _
                  "        ( " & _
                  "            Select ' <br/><br/>'+ 'EmployeeID: ' + EmployeeID + '<br/>UserLogin: ' + EmployeeName  AS [text()] " & _
                  "            From DWH.STAR.STAR_Username_LU ST1 " & _
                  "            where Active = 1 and EmployeeName like '%" & Replace(txtAdminUsrSrch.Text, "'", "''") & "%' " & _
                  "			and EmployeeID not in (" & PulledString & ") " & _
                  "            ORDER BY ST1.EmployeeID " & _
                  "            For XML PATH ('') " & _
                  "        ), 13, 100000), '') [Stuff] "

            lblAdminUsrResults.Text += Server.HtmlDecode(GetString(FurtherSearch))


            AddRemoveUser_Modal.Show()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub AddRemoveUser_Submit_Click(sender As Object, e As EventArgs) Handles AddRemoveUser_Submit.Click

        lblErrorAddingRemoving.Text = ""
        If Trim(AddRemoveUser_ID.Text) = "" Then
            lblErrorAddingRemoving.Text = "No User Login entered"
            AddRemoveUser_Modal.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from DWH.RAD_Op.Mammo_IR_Users where UserLogin = '" & Trim(Replace(AddRemoveUser_ID.Text, "'", "''")) & "' and Active = 1 and Tech = 1") > 0 Then
            lblErrorAddingRemoving.Text = "This Tech is already listed"
            AddRemoveUser_Modal.Show()
            Exit Sub
        End If
        Dim SQL As String
        Dim UsrName As String = ""

        Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
        Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
        Dim oresult As SearchResultCollection
        Dim result As SearchResult

        osearcher.Filter = "(&(samaccountname=" & Trim(Replace(AddRemoveUser_ID.Text, "'", "''")) & "))" ' search filter

        For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
            osearcher.PropertiesToLoad.Add(elem.PropertyName)
        Next

        oresult = osearcher.FindAll()

        For Each result In oresult
            If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                UsrName += result.GetDirectoryEntry.Properties("cn").Value
            End If
        Next

        'If Len(UsrName) = 0 Then
        '    lblErrorAddingRemoving.Text = "User Not Found"
        '    AddRemoveUser_Modal.Show()
        '    Exit Sub
        'End If

        If Len(UsrName) = 0 Then

            Dim SecondSearch As String = "Select EmployeeName from DWH.STAR.STAR_Username_LU where Active = 1 and EmployeeID = '" & Trim(Replace(AddRemoveUser_ID.Text, "'", "''")) & "'"
            Dim ResultName As String = ""
            ResultName = GetString(SecondSearch)
            If Len(ResultName) > 0 Then
                If GetScalar("Select count(*) from DWH.RAD_Op.Mammo_IR_Users where UserLogin = '" & Trim(Replace(AddRemoveUser_ID.Text, "'", "''")) & "' and Active = 1 ") > 0 Then
                    SQL = "Update DWH.RAD_Op.Mammo_IR_Users set Tech = 1, DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                        " - AddedTech' where UserLogin = '" & Replace(AddRemoveUser_ID.Text, "'", "''") & "' and Active = 1"
                Else
                    SQL = "Insert into DWH.RAD_Op.Mammo_IR_Users (UserLogin, UserFullName, Active, DateModified, ModifiedBy, Tech) values ('" & Replace(AddRemoveUser_ID.Text, "'", "''") & "', '" & _
                            Trim(Replace(ResultName, "'", "''")) & "', 1, getdate(), '" & _
                            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 1)"

                End If
            Else
                lblErrorAddingRemoving.Text = "User Not Found"
                AddRemoveUser_Modal.Show()
                Exit Sub
            End If


        Else
            If GetScalar("Select count(*) from DWH.RAD_Op.Mammo_IR_Users where UserLogin = '" & Trim(Replace(AddRemoveUser_ID.Text, "'", "''")) & "' and Active = 1 ") > 0 Then
                SQL = "Update DWH.RAD_Op.Mammo_IR_Users set Tech = 1, DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    " - AddedTech' where UserLogin = '" & Replace(AddRemoveUser_ID.Text, "'", "''") & "' and Active = 1"
            Else
                SQL = "Insert into DWH.RAD_Op.Mammo_IR_Users (UserLogin, UserFullName, Active, DateModified, ModifiedBy, Tech) values ('" & Replace(AddRemoveUser_ID.Text, "'", "''") & "', '" & _
                        Trim(Replace(UsrName, "'", "''")) & "', 1, getdate(), '" & _
                        Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 1)"

            End If
        End If

        'If GetScalar("Select count(*) from DWH.RAD_Op.Mammo_IR_Users where UserLogin = '" & Trim(Replace(AddRemoveUser_ID.Text, "'", "''")) & "' and Active = 1 ") > 0 Then
        '    SQL = "Update DWH.RAD_Op.Mammo_IR_Users set Tech = 1, DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
        '        " - AddedTech' where UserLogin = '" & Replace(AddRemoveUser_ID.Text, "'", "''") & "' and Active = 1"
        'Else
        '    SQL = "Insert into DWH.RAD_Op.Mammo_IR_Users (UserLogin, UserFullName, Active, DateModified, ModifiedBy, Tech) values ('" & Replace(AddRemoveUser_ID.Text, "'", "''") & "', '" & _
        '            Trim(Replace(UsrName, "'", "''")) & "', 1, getdate(), '" & _
        '            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 1)"

        'End If


        ExecuteSql(SQL)

        'SynchWebFDPermissions()

        LoadYourTechRoster()
        LoadPendingYourTechRoster()
        LoadTechNames_Gridview()
        Eval_GetTechList()
        AddRemoveUser_ID.Text = ""




    End Sub

    Private Sub SynchWebFDPermissions()

        Dim SQL As String = "insert into WebFD.dbo.aspnet_Users " & _
            "select '5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', dar.UserLogin), UserLogin, UserLogin, null, 0, getdate() " & _
            "from DWH.Rad_OP.Mammo_IR_Users dar " & _
            " join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dar.UserLogin = d2s.UserLogin and d2s.Active = 1 " & _
            "where dar.Active = 1 and not exists (select * from WebFD.dbo.aspnet_Users au  " & _
            "where au.UserName = dar.UserLogin) " & _
            " " & _
            "insert into WebFD.dbo.aspnet_UsersInRoles  " & _
            "select UserId, '9929B807-DE4B-6D9A-53FB-167F2C149CEA' from " & _
            "DWH.Rad_OP.Mammo_IR_Users dar " & _
            " join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dar.UserLogin = d2s.UserLogin and d2s.Active = 1 " & _
            "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
            "where dar.Active = 1 and not exists (select * from " & _
            "WebFD.dbo.aspnet_UsersInRoles uir  " & _
            "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
            "where au.UserId = uir.UserId " & _
            "and RoleName = 'FDAMammo') " & _
            " " & _
            "delete uir from WebFD.dbo.aspnet_UsersInRoles uir  " & _
            "where not exists ( " & _
            "select * from " & _
            "DWH.Rad_OP.Mammo_IR_Users dar " & _
             "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dar.UserLogin = d2s.UserLogin and d2s.Active = 1 " & _
            "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
            "where dar.Active = 1  and au.UserId = uir.UserId) and RoleID = '9929B807-DE4B-6D9A-53FB-167F2C149CEA'"

        ExecuteSql(SQL)

    End Sub

    Private Sub TechNames_Gridview_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles TechNames_Gridview.PageIndexChanging
        Try
            Dim x As DataView = AllUsers()
            TechNames_Gridview.DataSource = x
            TechNames_Gridview.PageIndex = e.NewPageIndex
            TechNames_Gridview.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub TechNames_Gridview_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles TechNames_Gridview.RowCommand
        Try
            Dim UserLogin As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If varname = "AddTech" Then

                Dim Sql As String = "Insert into DWH.RAD_Op.Staging_Mammo_IR_Dept_2_Techs (Dept_ID, UserLogin, SupervisorLogin, Active, DateModified, ModifiedBy) select "

                Dim x As String
                Dim y As String
                If ddlManageDepartments.SelectedValue = -1 Then
                    lblMPEFirstTabeExplanation.Text = "No Department Selected"
                    mpeFirstTab.Show()
                    Exit Sub
                Else
                    x = "'" & Replace(ddlManageDepartments.SelectedValue, "'", "''") & "' "
                    y = " = " & x
                End If

                Sql += x & ", '" & Replace(UserLogin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 1, getdate(), '" & _
                    Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from DWH.RAD_Op.Staging_Mammo_IR_Dept_2_Techs " & _
                    "where UserLogin = '" & Replace(UserLogin, "'", "''") & "' and Dept_ID " & y & " and SupervisorLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1 )"

                ExecuteSql(Sql)

                LoadPendingYourTechRoster()

            End If


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvPendingChanges_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPendingChanges.RowCommand
        Try
            Dim UserLogin As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If varname = "RemoveTech" Then
                Dim Sql As String = "Delete from DWH.RAD_Op.Staging_Mammo_IR_Dept_2_Techs "

                Dim x As String
                Dim y As String
                If ddlManageDepartments.SelectedValue = -1 Then
                    x = "null "
                    y = "is null "
                Else
                    x = "'" & Replace(ddlManageDepartments.SelectedValue, "'", "''") & "' "
                    y = " = " & x
                End If

                Sql += "where UserLogin = '" & Replace(UserLogin, "'", "''") & "' and Dept_ID " & y & " and SupervisorLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

                ExecuteSql(Sql)

                LoadPendingYourTechRoster()

            End If


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub gvPendingChanges_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPendingChanges.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim btnTechMinus As ImageButton = e.Row.FindControl("btnTechMinus")
            Dim btnTechPlus As ImageButton = e.Row.FindControl("btnTechPlus")

            If e.Row.DataItem("Active") = "1" Then
                btnTechPlus.Visible = True
            Else
                btnTechMinus.Visible = True
            End If

        End If
    End Sub

    Private Sub YourTechRoster_Gridview_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles YourTechRoster_Gridview.RowCommand
        Try
            Dim UserLogin As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If varname = "RemoveTech" Then
                Dim Sql As String = "Insert into DWH.RAD_Op.Staging_Mammo_IR_Dept_2_Techs (Dept_ID, UserLogin, SupervisorLogin, Active, DateModified, ModifiedBy) select "

                Dim x As String
                Dim y As String
                If ddlManageDepartments.SelectedValue = -1 Then
                    x = "null "
                    y = "is null "
                Else
                    x = "'" & Replace(ddlManageDepartments.SelectedValue, "'", "''") & "' "
                    y = " = " & x
                End If

                Sql += x & ", '" & Replace(UserLogin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 0, getdate(), '" & _
                    Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from DWH.RAD_Op.Staging_Mammo_IR_Dept_2_Techs " & _
                    "where UserLogin = '" & Replace(UserLogin, "'", "''") & "' and Dept_ID " & y & " and SupervisorLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 0 )"

                ExecuteSql(Sql)

                LoadPendingYourTechRoster()

            End If


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnPendingChanges_Click(sender As Object, e As EventArgs) Handles btnPendingChanges.Click

        Try
            Dim sql As String = "update a " & _
                "set DateModified = GETDATE(), EffectiveFrom = a.DateModified, EffectiveTo = DATEADD(day, -1, getdate()), Active = 0 " & _
                "from DWH.RAD_Op.Mammo_IR_Dept_2_Techs a " & _
                "join DWH.RAD_Op.STAGING_Mammo_IR_Dept_2_Techs b on a.UserLogin = b.UserLogin and a.SuperVisorLogin = b.SupervisorLogin and a.Active = 1 " & _
                "	and b.Active = 0 and isnull(a.Dept_ID, -1) = ISNULL(b.Dept_ID, isnull(a.Dept_ID, -1)) " & _
                "where a.SupervisorLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                " " & _
                "insert into DWH.RAD_Op.Mammo_IR_Dept_2_Techs " & _
                "select * from DWH.RAD_Op.STAGING_Mammo_IR_Dept_2_Techs a " & _
                "where a.SupervisorLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1 " & _
                "and not exists (select * from  DWH.RAD_Op.Mammo_IR_Dept_2_Techs b where a.UserLogin = b.UserLogin and a.SuperVisorLogin = b.SupervisorLogin  " & _
                "	and b.Active = 1 and isnull(a.Dept_ID, -1) = ISNULL(b.Dept_ID, isnull(a.Dept_ID, -1))  )"

            ExecuteSql(sql)

            FlushStaging()
            LoadYourTechRoster()
            LoadPendingYourTechRoster()
            Eval_GetTechList()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub YourTechs_Gridview_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles YourTechs_Gridview.PageIndexChanging
        Try
            Dim x As DataView = YourTechs()
            YourTechs_Gridview.DataSource = x
            YourTechs_Gridview.PageIndex = e.NewPageIndex
            YourTechs_Gridview.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub YourTechs_Gridview_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles YourTechs_Gridview.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub YourTechs_Rowing()

        For Each canoe As GridViewRow In YourTechs_Gridview.Rows
            If canoe.RowIndex = YourTechs_Gridview.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            Else
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#d5eaff")
            End If
        Next

    End Sub

    Private Sub YourTechs_Gridview_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles YourTechs_Gridview.SelectedIndexChanging


        For Each canoe As GridViewRow In YourTechs_Gridview.Rows
            If canoe.RowIndex = e.NewSelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                lblPersonReviewed.Text = canoe.Cells(1).Text
                lblUserSelected.Text = canoe.Cells(1).Text
                btnNewAccessionID.Text = "Create new Accession for " & canoe.Cells(1).Text
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#d5eaff")
            Else
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            End If
        Next

        InitializeEvalForm(YourTechs_Gridview.DataKeys(e.NewSelectedIndex).Value.ToString)
        PopulateTechIssues(YourTechs_Gridview.DataKeys(e.NewSelectedIndex).Value.ToString)
        PopulateAccessions(YourTechs_Gridview.DataKeys(e.NewSelectedIndex).Value.ToString)
        pnlShowCreateNewID.Visible = True
        pnlAccessionDetails.Visible = False

    End Sub

    Private Sub PopulateTechIssues(UserLogin As String)

        Dim sql As String = "select FY, Financial_Quarter as FQ, ai.ExamIssue, sum(isnull(count, 0)) as ErrorCount  " & _
 "from DWH.RAD_Op.Mammo_IR_Users u " & _
 "join DWH.RAD_Op.Mammo_IR_AccessionHeader ah on u.UserLogin = ah.TechUserLogin and ah.Active = 1 " & _
 "join DWH.dbo.DimDate dd on ah.ImageReviewDate = dd.Calendar_Date " & _
 "join DWH.RAD_Op.Mammo_IR_Accession_Issues ai on ah.AccID = ai.AccID and ai.Active = 1 " & _
 "where u.UserLogin=  '" & Replace(UserLogin, "'", "''") & "' " & _
 "group by FY, Financial_Quarter, ai.ExamIssue " & _
 "order by FY desc, FQ desc, ExamIssue asc "

        gvTechIssues.DataSource = GetData(sql)
        gvTechIssues.DataBind()
    End Sub


    Private Sub PopulateAccessions(UserLogin As String)


        Dim sql As String = "select a.*, LastUpdated, isnull(d.Department + isnull(' ('+d.DepartmentNo+ ')', ''), a.Dept_ID) as Department,  convert(varchar, ImageReviewDate, 107) as ImageReviewDateDisplay " & _
            " from DWH.RAD_Op.Mammo_IR_AccessionHeader a " & _
            "left join DWH.RAD_Op.Mammo_IR_Department_LU d on Dept_ID=d.ID and d.Active = 1 " & _
                " left join (select AccID, Max(LastUpdated) as LastUpdated from ( " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_AccessionHeader " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_Comments " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_Issues " & _
            "		where Active = 1 " & _
            "		group by AccID ) x " & _
            "		group by AccID) lu on lu.AccID = a.AccID " & _
            "where TechUserLogin='" & Replace(UserLogin, "'", "''") & "' " & _
            " and a.Active=  1 " & _
            "order by AccID desc"

        Dim x As DataView = GetData(sql).DefaultView

        gvAccessions.DataSource = x
        gvAccessions.DataBind()

        If x.Count() > 0 Then
            pnlShowAccessionIDs.Visible = True
        Else
            pnlShowAccessionIDs.Visible = False
        End If


    End Sub

    Private Sub PopulateGridIssuesMain(x As String)

        'Dim SQL As String = "select Description, TableOrder from DWH.RAD_Op.Mammo_IR_ExamArea_LU a " & _
        '"left join DWH.RAD_Op.Mammo_IR_Accession_Issues  b on b.ExamArea = a.Description and b.Active = 1 and a.Active = 0 " & _
        '"where (a.Active = 1 or b.AccID = '" & x & "') union select '', 0 " & _
        '"order by TableOrder "

        Dim SQL As String = "select Description, TableOrder from DWH.RAD_Op.Mammo_IR_ExamIssue_LU a " & _
        "left join DWH.RAD_Op.Mammo_IR_Accession_Issues  b on b.ExamIssue = a.Description and b.Active = 1 and a.Active = 0 " & _
        "where (a.Active = 1 or b.AccID = '" & x & "') union select '', 0 " & _
        "order by TableOrder "

        gvExamIssuesMain.DataSource = GetData(SQL)
        gvExamIssuesMain.DataBind()

    End Sub

    Private Sub PopulateGridIssuesSub(x As String, y As DataList)
        Dim SQL As String = ""
        If x = "" Then
            'SQL = "select a.Description as Data, 'Label' as Show, TableOrder as Show, a.Description from DWH.RAD_Op.Mammo_IR_ExamIssue_LU a " & _
            '                             "left join DWH.RAD_Op.Mammo_IR_Accession_Issues b on b.ExamIssue = a.Description and b.Active = 1 " & _
            '                             "	and b.ExamArea = '" & Replace(x, "'", "''") & "' and b.AccID = '" & Replace(selectedAccID.Text, "'", "''") & "' " & _
            '                             "where a.Active = 1  " & _
            '                             "order by TableOrder "

            SQL = "select a.Description as Data, 'Label' as Show, a.TableOrder as Show, a.Description, b.IssueComments, 'False' as IsOther from DWH.RAD_Op.Mammo_IR_ExamArea_LU a " & _
                                         "left join DWH.RAD_Op.Mammo_IR_Accession_Issues b on b.ExamArea = a.Description and b.Active = 1 " & _
                                         "	and b.ExamIssue = '" & Replace(x, "'", "''") & "' and b.AccID = '" & Replace(selectedAccID.Text, "'", "''") & "' " & _
                                         " left join DWH.RAD_Op.Mammo_IR_ExamIssue_LU i on '" & Replace(x, "'", "''") & "' = i.Description " & _
                                         "where a.Active = 1  or exists (select * from DWH.RAD_Op.Mammo_IR_Accession_Issues c where c.Active = 1 and c.AccID = '" & Replace(selectedAccID.Text, "'", "''") & _
                                         "' and c.ExamArea = a.Description) " & _
                                         "order by a.TableOrder "
        Else
            'SQL = "select distinct isnull(count, 0) as Data, 'Textbox' as Show, TableOrder, a.Description from DWH.RAD_Op.Mammo_IR_ExamIssue_LU a " & _
            '                              "left join DWH.RAD_Op.Mammo_IR_Accession_Issues b on b.ExamIssue = a.Description and b.Active = 1 " & _
            '                              "	and b.ExamArea = '" & Replace(x, "'", "''") & "' and b.AccID = '" & Replace(selectedAccID.Text, "'", "''") & "' " & _
            '                              "where a.Active = 1  " & _
            '                              "order by TableOrder "
            SQL = "select distinct isnull(count, 0) as Data, 'Textbox' as Show, a.TableOrder, a.Description, '' as IssueComments, case when isnull(i.RequireComment, 0) = 1 then 'True' else 'False' end as IsOther from DWH.RAD_Op.Mammo_IR_ExamArea_LU a " & _
                             "left join DWH.RAD_Op.Mammo_IR_Accession_Issues b on b.ExamArea = a.Description and b.Active = 1 " & _
                             "	and b.ExamIssue = '" & Replace(x, "'", "''") & "' and b.AccID = '" & Replace(selectedAccID.Text, "'", "''") & "' " & _
                             " left join DWH.RAD_Op.Mammo_IR_ExamIssue_LU i on '" & Replace(x, "'", "''") & "' = i.Description " & _
                             "where a.Active = 1  or exists (select * from DWH.RAD_Op.Mammo_IR_Accession_Issues c where c.Active = 1 and c.AccID = '" & Replace(selectedAccID.Text, "'", "''") & _
                             "' and c.ExamArea = a.Description) " & _
                             "order by a.TableOrder "
        End If

        y.DataSource = GetData(SQL)
        y.DataBind()

    End Sub

    Private Sub gvExamIssuesMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvExamIssuesMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim gvExamIssuesDetail1 As DataList = e.Row.FindControl("gvExamIssuesDetail1")
            Dim gvExamIssuesDetail2 As DataList = e.Row.FindControl("gvExamIssuesDetail2")
            Dim lblIssueHeader As Label = e.Row.FindControl("lblIssueHeader")

            If e.Row.DataItem("Description") = "" Then
                PopulateGridIssuesSub(e.Row.DataItem("Description"), gvExamIssuesDetail2)
            Else
                PopulateGridIssuesSub(e.Row.DataItem("Description"), gvExamIssuesDetail1)
            End If


        End If
    End Sub

    Private Sub rbAccept_CheckedChanged(sender As Object, e As EventArgs) Handles rbAccept.CheckedChanged

        If rbAccept.Checked Then
            rbReject.Checked = False
            pnlExamIssues.Visible = False
        End If


    End Sub

    Private Sub rbReject_CheckedChanged(sender As Object, e As EventArgs) Handles rbReject.CheckedChanged
        If rbReject.Checked Then
            rbAccept.Checked = False
            pnlExamIssues.Visible = True
        End If

    End Sub

    Private Sub gvAccessions_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAccessions.PageIndexChanging

        Try

            Dim sql As String = "select a.*, LastUpdated, isnull(d.Department + isnull(' ('+d.DepartmentNo+ ')', ''), a.Dept_ID) as Department,  convert(varchar, ImageReviewDate, 107) as ImageReviewDateDisplay " & _
            " from DWH.RAD_Op.Mammo_IR_AccessionHeader a " & _
            "left join DWH.RAD_Op.Mammo_IR_Department_LU d on Dept_ID=d.ID and d.Active = 1 " & _
                " left join (select AccID, Max(LastUpdated) as LastUpdated from ( " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_AccessionHeader " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_Comments " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_Issues " & _
            "		where Active = 1 " & _
            "		group by AccID ) x " & _
            "		group by AccID) lu on lu.AccID = a.AccID " & _
            "where TechUserLogin='" & Replace(YourTechs_Gridview.DataKeys(YourTechs_Gridview.SelectedIndex).Value.ToString, "'", "''") & "' " & _
            " and a.Active=  1 " & _
            "order by AccID desc"

            Dim se As String = gvAccessions.SortExpression
            Dim x As DataView = GetData(sql).DefaultView
            Dim y As String = gvAccessionmap.Text
            Dim z As String = gvAccessiondir.Text

            Try
                If CInt(gvAccessiondir.Text) = 1 Then
                    x.Sort = y + " " + "asc"
                Else
                    x.Sort = y + " " + "desc"
                End If
            Catch ex As Exception
                x.Sort = se
            End Try

            gvAccessions.PageIndex = e.NewPageIndex
            gvAccessions.DataSource = x
            gvAccessions.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvAccessions_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvAccessions.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvAccessions_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvAccessions.SelectedIndexChanging

        For Each canoe As GridViewRow In gvAccessions.Rows
            If canoe.RowIndex = e.NewSelectedIndex Then
                txtAccessionNumber.Text = canoe.Cells(1).Text
                txtImageDate.Text = canoe.Cells(4).Text
                Try
                    EvalFacility_DDL.SelectedValue = canoe.Cells(5).Text
                Catch ex As Exception
                    EvalFacility_DDL.SelectedValue = -1
                End Try

                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#d5eaff")
            Else
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
            End If
        Next

        lblExistingComments.Text = Server.HtmlDecode(GetString("Select distinct " & _
            "    isnull(substring( " & _
             "       ( " & _
            "            Select ST1.Comments + '<br> - (' + ModifiedBy + ', ' + convert(varchar, DateModified, 107) + ') <br><br> '   AS [text()] " & _
            "            From DWH.RAD_Op.Mammo_IR_Accession_Comments ST1 " & _
             "          where Active = 1 and AccID = '" & gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString & "' " & _
             "           ORDER BY ST1.DateModified " & _
             "           For XML PATH ('') " & _
             "       ), 0, 2000), '') [Comments] "))
        pnlAccessionDetails.Visible = True
        selectedAccID.Text = gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString

        Dim s As String = GetString("select CorrectiveAction from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where Active = 1 and AccID = '" & gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString & "'")

        If rbCorr.Text = s Then
            rbCorr.Checked = True
            'rbnoCorr.Checked = False
        Else
            'rbnoCorr.Checked = True
            rbCorr.Checked = False
            'rbCorr2.Checked = False
            'rbCorr3.Checked = False
            'rbCorr4.Checked = False
            'ElseIf rbCorr2.Text = s Then
            '    rbCorr1.Checked = False
            '    rbCorr2.Checked = True
            '    rbCorr3.Checked = False
            '    rbCorr4.Checked = False
            'ElseIf rbCorr3.Text = s Then
            '    rbCorr1.Checked = False
            '    rbCorr2.Checked = False
            '    rbCorr3.Checked = True
            '    rbCorr4.Checked = False
            'ElseIf rbCorr4.Text = s Then
            '    rbCorr1.Checked = False
            '    rbCorr2.Checked = False
            '    rbCorr3.Checked = False
            '    rbCorr4.Checked = True
        End If

        'Dim z As String = GetString("select isnull(ImprovementAfterCorrectiveAction, '') from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where Active = 1 and AccID = '" & gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString & "'")
        'Dim zz As String = GetString("select isnull(ImprovementAfterAdditionalCorrectiveAction, '') from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where Active = 1 and AccID = '" & gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString & "'")

        'If z = rbCorrectiveActionYes.Text Then
        '    rbCorrectiveActionYes.Checked = True
        '    rbCorrectiveActionNo.Checked = False
        'ElseIf z = rbCorrectiveActionNo.Text Then
        '    rbCorrectiveActionYes.Checked = False
        '    rbCorrectiveActionNo.Checked = True
        'End If

        'If zz = rbAddCorrectiveActionYes.Text Then
        '    rbAddCorrectiveActionYes.Checked = True
        '    rbAddCorrectiveActionNo.Checked = False
        'ElseIf zz = rbAddCorrectiveActionNo.Text Then
        '    rbAddCorrectiveActionYes.Checked = False
        '    rbAddCorrectiveActionNo.Checked = True
        'End If

        Dim x As Integer = GetScalar("select isnull(sum(Count), 0) from DWH.RAD_Op.Mammo_IR_Accession_Issues where Active = 1 and AccID = '" & gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString & "'")


        If x > 0 Then
            rbAccept.Checked = False
            rbReject.Checked = True
            pnlExamIssues.Visible = True
        Else
            rbAccept.Checked = True
            rbReject.Checked = False
        End If


        PopulateGridIssuesMain(gvAccessions.DataKeys(e.NewSelectedIndex).Value.ToString)
        pnlAccessionDetails.Focus()



    End Sub

    Private Sub btnNewAccessionID_Click(sender As Object, e As EventArgs) Handles btnNewAccessionID.Click

        txtAccessionNumber.Text = ""
        txtComments.Text = ""
        lblExistingComments.Text = ""
        selectedAccID.Text = ""
        pnlAccessionDetails.Visible = True
        EvalFacility_DDL.SelectedIndex = -1
        txtImageDate.Text = ""
        rbAccept.Checked = False
        rbReject.Checked = False
        'rbAddCorrectiveActionNo.Checked = False
        'rbAddCorrectiveActionYes.Checked = False
        'rbCorrectiveActionNo.Checked = False
        'rbCorrectiveActionYes.Checked = False
        pnlExamIssues.Visible = False
        PopulateGridIssuesMain("")
        rbCorr.Checked = False
        'rbnoCorr.Checked = False
        'rbCorr2.Checked = False
        'rbCorr3.Checked = False
        'rbCorr4.Checked = False

    End Sub

    'Private Sub rbCorr1_CheckedChanged(sender As Object, e As EventArgs) Handles rbCorr1.CheckedChanged

    '    If rbCorr1.Checked Then
    '        'rbnoCorr.Checked = False
    '        'rbCorr2.Checked = False
    '        'rbCorr3.Checked = False
    '        'rbCorr4.Checked = False
    '    End If

    'End Sub

    'Private Sub rbnoCorr_CheckedChanged(sender As Object, e As EventArgs) Handles rbnoCorr.CheckedChanged

    '    If rbnoCorr.Checked Then
    '        rbCorr1.Checked = False
    '        'rbCorr2.Checked = False
    '        'rbCorr3.Checked = False
    '        'rbCorr4.Checked = False
    '    End If

    'End Sub

    'Private Sub rbCorr2_CheckedChanged(sender As Object, e As EventArgs) Handles rbCorr2.CheckedChanged
    '    If rbCorr2.Checked Then
    '        rbCorr1.Checked = False
    '        rbCorr3.Checked = False
    '        rbCorr4.Checked = False
    '    End If
    'End Sub

    'Private Sub rbCorr3_CheckedChanged(sender As Object, e As EventArgs) Handles rbCorr3.CheckedChanged
    '    If rbCorr3.Checked Then
    '        rbCorr1.Checked = False
    '        rbCorr2.Checked = False
    '        rbCorr4.Checked = False
    '    End If
    'End Sub

    'Private Sub rbCorr4_CheckedChanged(sender As Object, e As EventArgs) Handles rbCorr4.CheckedChanged
    '    If rbCorr4.Checked Then
    '        rbCorr1.Checked = False
    '        rbCorr2.Checked = False
    '        rbCorr3.Checked = False
    '    End If
    'End Sub

    Private Sub TechFilterDDL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TechFilterDDL.SelectedIndexChanged
        Eval_GetTechList()
    End Sub

    Private Sub txtFilterYourTechs_TextChanged(sender As Object, e As EventArgs) Handles txtFilterYourTechs.TextChanged
        Eval_GetTechList()
    End Sub

    'Private Sub rbCorrectiveActionNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbCorrectiveActionNo.CheckedChanged
    '    If rbCorrectiveActionNo.Checked Then
    '        rbCorrectiveActionYes.Checked = False
    '    End If
    'End Sub

    'Private Sub rbCorrectiveActionYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbCorrectiveActionYes.CheckedChanged
    '    If rbCorrectiveActionYes.Checked Then
    '        rbCorrectiveActionNo.Checked = False
    '    End If
    'End Sub

    'Private Sub rbAddCorrectiveActionNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbAddCorrectiveActionNo.CheckedChanged
    '    If rbAddCorrectiveActionNo.Checked Then
    '        rbAddCorrectiveActionYes.Checked = False
    '    End If
    'End Sub

    'Private Sub rbAddCorrectiveActionYes_CheckedChanged(sender As Object, e As EventArgs) Handles rbAddCorrectiveActionYes.CheckedChanged
    '    If rbAddCorrectiveActionYes.Checked Then
    '        rbAddCorrectiveActionNo.Checked = False
    '    End If
    'End Sub

    Private Sub btnSubmitAccession_Click(sender As Object, e As EventArgs) Handles btnSubmitAccession.Click

        If Len(Trim(txtAccessionNumber.Text)) = 0 Then
            ExplanationLabelACCErrors.Text = "Accession ID required"
            ExplanationLabelACCGood.Text = ""
            btnMPEAccClose.Visible = True
            btnMPEAccOkay.Visible = False
            'txtAccessionNumber.Focus()
            mpeSubmittingAccession.Show()
            Exit Sub
        End If

        Dim d As Date
        If Len(Trim(txtImageDate.Text)) = 0 Then
            ExplanationLabelACCErrors.Text = "Image Review Date required"
            ExplanationLabelACCGood.Text = ""
            btnMPEAccClose.Visible = True
            btnMPEAccOkay.Visible = False
            'txtImageDate.Focus()
            mpeSubmittingAccession.Show()
            Exit Sub
        ElseIf Date.TryParse(txtImageDate.Text, d) = False Then
            ExplanationLabelACCErrors.Text = "Image Review Date format cannot be interpreted"
            ExplanationLabelACCGood.Text = ""
            btnMPEAccClose.Visible = True
            btnMPEAccOkay.Visible = False
            'txtImageDate.Focus()
            mpeSubmittingAccession.Show()
            Exit Sub
        ElseIf d > Now() Then
            ExplanationLabelACCErrors.Text = "Cannot review dates in the future"
            ExplanationLabelACCGood.Text = ""
            btnMPEAccClose.Visible = True
            btnMPEAccOkay.Visible = False
            'txtImageDate.Focus()
            mpeSubmittingAccession.Show()
            Exit Sub
        End If

        If EvalFacility_DDL.SelectedIndex < 1 Then
            ExplanationLabelACCErrors.Text = "Please select Imaging Department"
            ExplanationLabelACCGood.Text = ""
            btnMPEAccClose.Visible = True
            btnMPEAccOkay.Visible = False
            'EvalFacility_DDL.Focus()
            mpeSubmittingAccession.Show()
            Exit Sub
        End If

        If rbAccept.Checked = False And rbReject.Checked = False Then
            ExplanationLabelACCErrors.Text = "Were the Images Acceptable?"
            ExplanationLabelACCGood.Text = ""
            btnMPEAccClose.Visible = True
            btnMPEAccOkay.Visible = False
            'EvalFacility_DDL.Focus()
            mpeSubmittingAccession.Show()
            Exit Sub
        End If

        If rbReject.Checked Then
            '!! Checks only; may need AccID

            If rbCorr.Checked = False Then
                ExplanationLabelACCErrors.Text = "Corrective Action Required"
                ExplanationLabelACCGood.Text = ""
                btnMPEAccClose.Visible = True
                btnMPEAccOkay.Visible = False
                'EvalFacility_DDL.Focus()
                mpeSubmittingAccession.Show()
                Exit Sub
            End If

            For Each canoe As GridViewRow In gvExamIssuesMain.Rows

                Dim gvExamIssuesDetail1 As DataList = canoe.FindControl("gvExamIssuesDetail1")

                If gvExamIssuesDetail1.Visible Then
                    For Each listrow As DataListItem In gvExamIssuesDetail1.Items
                        'Dim txtIssueCount As HtmlInputGenericControl = CType(listrow.FindControl("txtIssueCount"), HtmlInputGenericControl)
                        Dim lblIssueDesc As Label = CType(listrow.FindControl("lblIssueDesc"), Label)
                        Dim txtIssueOtherComment As TextBox = CType(listrow.FindControl("txtIssueOtherComment"), TextBox)
                        Dim ddlIssueYesNo As DropDownList = CType(listrow.FindControl("ddlIssueYesNo"), DropDownList)

                        If txtIssueOtherComment.Visible = True And ddlIssueYesNo.SelectedValue = 1 And Replace(Trim(txtIssueOtherComment.Text), "'", "''") = "" Then
                            ExplanationLabelACCErrors.Text = "Explanation required when 'Other' is selected as Issue"
                            ExplanationLabelACCGood.Text = ""
                            btnMPEAccClose.Visible = True
                            btnMPEAccOkay.Visible = False
                            'EvalFacility_DDL.Focus()
                            mpeSubmittingAccession.Show()
                            Exit Sub
                        End If

                        'Dim z As Integer
                        'If Integer.TryParse(txtIssueCount.Value, z) = False Then
                        '    ExplanationLabelACCErrors.Text = "Could not parse issue count as integer"
                        '    ExplanationLabelACCGood.Text = ""
                        '    btnMPEAccClose.Visible = True
                        '    btnMPEAccOkay.Visible = False
                        '    'EvalFacility_DDL.Focus()
                        '    mpeSubmittingAccession.Show()
                        '    Exit Sub
                        'ElseIf z < 0 Then
                        '    ExplanationLabelACCErrors.Text = "Negative Counts not Accepted"
                        '    ExplanationLabelACCGood.Text = ""
                        '    btnMPEAccClose.Visible = True
                        '    btnMPEAccOkay.Visible = False
                        '    'EvalFacility_DDL.Focus()
                        '    mpeSubmittingAccession.Show()
                        '    Exit Sub
                        'End If

                    Next

                End If

            Next

        End If

        Dim sql As String = "insert into DWH.RAD_Op.Mammo_IR_AccessionHeader " & _
            "(AccID, SupervisorLogin, TechUserLogin, AccessionNumber, Dept_ID, ImageReviewDate, Active, DateModified, ModifiedBy, DateCreated) " & _
            "select ACCID, SupervisorLogin, TechUserLogin, '" & Replace(Trim(txtAccessionNumber.Text), "'", "''") & "',  '" & Replace(EvalFacility_DDL.SelectedValue, "'", "''") & "',  '" & _
            d & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', DateCreated " & _
            "from DWH.RAD_Op.Mammo_IR_AccessionHeader where Active = 1 and AccID = '" & selectedAccID.Text & "' and ( " & _
            "AccessionNumber <> '" & Replace(Trim(txtAccessionNumber.Text), "'", "''") & "' or Dept_ID <> '" & Replace(EvalFacility_DDL.SelectedValue, "'", "''") & "' or ImageReviewDate <> '" & d & "' ) " & _
            "update DWH.RAD_Op.Mammo_IR_AccessionHeader set Active = 0 where Active = 1 and AccID = '" & selectedAccID.Text & "' and ( " & _
            "AccessionNumber <> '" & Replace(Trim(txtAccessionNumber.Text), "'", "''") & "' or Dept_ID <> '" & Replace(EvalFacility_DDL.SelectedValue, "'", "''") & "' or ImageReviewDate <> '" & d & "' ) " & _
            "insert into DWH.RAD_Op.Mammo_IR_AccessionHeader " & _
            "(AccID, AccessionNumber, TechUserLogin, SupervisorLogin, Dept_ID, ImageReviewDate, Active, DateModified, ModifiedBy, DateCreated) " & _
            "output inserted.AccID " & _
            "select (select isnull(max(AccID), 0) from  DWH.RAD_Op.Mammo_IR_AccessionHeader) + 1,  " & _
            "'" & Replace(Trim(txtAccessionNumber.Text), "'", "''") & "', '" & Replace(YourTechs_Gridview.SelectedDataKey.Value.ToString, "'", "''") & "', '" & _
            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & Replace(EvalFacility_DDL.SelectedValue, "'", "''") & "', '" & d & _
            "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
            "where not exists (select * from DWH.RAD_Op.Mammo_IR_AccessionHeader where Active = 1 and AccID= '" & selectedAccID.Text & "') "

        Dim x As Integer = GetScalar(sql)
        If x = 0 Then
            x = selectedAccID.Text
        End If

        Dim AdditionalSQL As String = ""
        If rbReject.Checked Then

            If rbCorr.Checked Then
                AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions set Active = 0 where Active = 1 and AccID = '" & x & "' and CorrectiveAction <> '" & _
                    Replace(rbCorr.Text, "'", "''") & "' " & _
                    "Insert into DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions (AccID, CorrectiveAction, Active, DateModified, ModifiedBy) select '" & x & "', '" & _
                    Replace(rbCorr.Text, "'", "''") & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from " & _
                    " DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where AccID = '" & x & "' and Active = 1 and CorrectiveAction = '" & Replace(rbCorr.Text, "'", "''") & "' )"
                'ElseIf rbCorr2.Checked Then
                '    AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions set Active = 0 where Active = 1 and AccID = '" & x & "' and CorrectiveAction <> '" & _
                '        Replace(rbCorr2.Text, "'", "''") & "' " & _
                '        "Insert into DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions (AccID, CorrectiveAction, Active, DateModified, ModifiedBy) select '" & x & "', '" & _
                '        Replace(rbCorr2.Text, "'", "''") & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from " & _
                '        " DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where AccID = '" & x & "' and Active = 1 and CorrectiveAction = '" & Replace(rbCorr2.Text, "'", "''") & "' )"
                'ElseIf rbCorr3.Checked Then
                '    AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions set Active = 0 where Active = 1 and AccID = '" & x & "' and CorrectiveAction <> '" & _
                '        Replace(rbCorr3.Text, "'", "''") & "' " & _
                '        "Insert into DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions (AccID, CorrectiveAction, Active, DateModified, ModifiedBy) select '" & x & "', '" & _
                '        Replace(rbCorr3.Text, "'", "''") & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from " & _
                '        " DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where AccID = '" & x & "' and Active = 1 and CorrectiveAction = '" & Replace(rbCorr3.Text, "'", "''") & "' )"
                'ElseIf rbCorr4.Checked Then
                '    AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions set Active = 0 where Active = 1 and AccID = '" & x & "' and CorrectiveAction <> '" & _
                '        Replace(rbCorr4.Text, "'", "''") & "' " & _
                '        "Insert into DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions (AccID, CorrectiveAction, Active, DateModified, ModifiedBy) select '" & x & "', '" & _
                '        Replace(rbCorr4.Text, "'", "''") & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from " & _
                '        " DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions where AccID = '" & x & "' and Active = 1 and CorrectiveAction = '" & Replace(rbCorr4.Text, "'", "''") & "' )"
            Else
                AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions set Active = 0 where Active = 1 and AccID = '" & x & "'  "
            End If

            'If rbCorrectiveActionYes.Checked Then
            '    AdditionalSQL += "Update DWH.Rad_OP.Mammo_IR_Accession_CorrectiveActions set DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            '        "', ImprovementAfterCorrectiveAction = 'Yes' where Active = 1 and AccID = '" & x & "' "
            'ElseIf rbCorrectiveActionNo.Checked Then
            '    AdditionalSQL += "Update DWH.Rad_OP.Mammo_IR_Accession_CorrectiveActions set DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            '        "', ImprovementAfterCorrectiveAction = 'No' where Active = 1 and AccID = '" & x & "' "
            'End If

            'If rbAddCorrectiveActionYes.Checked Then
            '    AdditionalSQL += "Update DWH.Rad_OP.Mammo_IR_Accession_CorrectiveActions set DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            '        "', ImprovementAfterAdditionalCorrectiveAction = 'Yes' where Active = 1 and AccID = '" & x & "' "
            'ElseIf rbAddCorrectiveActionNo.Checked Then
            '    AdditionalSQL += "Update DWH.Rad_OP.Mammo_IR_Accession_CorrectiveActions set DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            '        "', ImprovementAfterAdditionalCorrectiveAction = 'No' where Active = 1 and AccID = '" & x & "' "
            'End If

            For Each canoe As GridViewRow In gvExamIssuesMain.Rows

                Dim gvExamIssuesDetail1 As DataList = canoe.FindControl("gvExamIssuesDetail1")

                If gvExamIssuesDetail1.Visible Then
                    For Each listrow As DataListItem In gvExamIssuesDetail1.Items
                        'Dim txtIssueCount As HtmlInputGenericControl = CType(listrow.FindControl("txtIssueCount"), HtmlInputGenericControl)
                        Dim lblIssueDesc As Label = CType(listrow.FindControl("lblIssueDesc"), Label)
                        Dim ddlIssueYesNo As DropDownList = CType(listrow.FindControl("ddlIssueYesNo"), DropDownList)
                        Dim txtIssueOtherComment As TextBox = CType(listrow.FindControl("txtIssueOtherComment"), TextBox)

                        Dim s As String
                        Dim s2 As String
                        If txtIssueOtherComment.Visible = True Then
                            s = " or IssueComments <> '" & Replace(Trim(txtIssueOtherComment.Text), "'", "''") & "' "
                            s2 = "'" & Replace(Trim(txtIssueOtherComment.Text), "'", "''") & "' "
                        Else
                            s = " or IssueComments is not null "
                            s2 = "null "
                        End If

                        AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_Issues set Active = 0 where Active = 1 and AccID = '" & x & "' and  " & _
                               "ExamIssue = '" & Replace(canoe.Cells(1).Text, "'", "''") & "' and ExamArea = '" & Replace(lblIssueDesc.Text, "'", "''") & "' and (isnull(Count, 0) <> '" & _
                               Replace(ddlIssueYesNo.SelectedValue, "'", "''") & "'" & s & " )"



                        If ddlIssueYesNo.SelectedValue = "1" Then
                            AdditionalSQL += " Insert into DWH.RAD_Op.Mammo_IR_Accession_Issues (AccID, ExamIssue, ExamArea, Count, Active, DateModified, ModifiedBy, IssueComments) " & _
                            "select '" & x & "', '" & Replace(canoe.Cells(1).Text, "'", "''") & "', '" & Replace(lblIssueDesc.Text, "'", "''") & "', '1', 1, getdate(), " & _
                            "'" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', " & s2 & " where not exists (select * from DWH.RAD_Op.Mammo_IR_Accession_Issues " & _
                            " where Active = 1 and AccID = '" & x & "' and ExamArea = '" & Replace(canoe.Cells(1).Text, "'", "''") & "' and ExamIssue = '" & Replace(lblIssueDesc.Text, "'", "''") & _
                            "' and Count = '1') "

                        End If

                        'Dim z As Integer
                        'If Integer.TryParse(txtIssueCount.Value, z) = False Then
                        'ElseIf z < 0 Then
                        'Else
                        '    AdditionalSQL += "Update DWH.RAD_Op.Mammo_IR_Accession_Issues set Active = 0 where Active = 1 and AccID = '" & x & "' and  " & _
                        '        "ExamArea = '" & Replace(canoe.Cells(1).Text, "'", "''") & "' and ExamIssue = '" & Replace(lblIssueDesc.Text, "'", "''") & "' and isnull(Count, 0) <> '" & z & "' " & _
                        '        " Insert into DWH.RAD_Op.Mammo_IR_Accession_Issues (AccID, ExamArea, ExamIssue, Count, Active, DateModified, ModifiedBy) " & _
                        '        "select '" & x & "', '" & Replace(canoe.Cells(1).Text, "'", "''") & "', '" & Replace(lblIssueDesc.Text, "'", "''") & "', '" & z & "', 1, getdate(), " & _
                        '        "'" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' where not exists (select * from DWH.RAD_Op.Mammo_IR_Accession_Issues " & _
                        '        " where Active = 1 and AccID = '" & x & "' and ExamArea = '" & Replace(canoe.Cells(1).Text, "'", "''") & "' and ExamIssue = '" & Replace(lblIssueDesc.Text, "'", "''") & _
                        '        "' and Count = '" & z & "') "

                        'End If

                    Next

                End If

            Next

            ExecuteSql(AdditionalSQL)
        End If

        If Len(Trim(txtComments.Text)) > 0 Then
            Dim CommentSQL As String = "Insert into DWH.RAD_Op.Mammo_IR_Accession_Comments (AccID, Comments, Active, DateModified, ModifiedBy) values ('" & x & "', '" & Replace(Trim(txtComments.Text), "'", "''") & _
                "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "') "
            ExecuteSql(CommentSQL)
        End If

        txtComments.Text = ""
        InitializeEvalForm(Replace(YourTechs_Gridview.SelectedDataKey.Value.ToString, "'", "''"))
        PopulateTechIssues(Replace(YourTechs_Gridview.SelectedDataKey.Value.ToString, "'", "''"))
        PopulateAccessions(Replace(YourTechs_Gridview.SelectedDataKey.Value.ToString, "'", "''"))
        pnlShowCreateNewID.Visible = True
        pnlAccessionDetails.Visible = False

        ExplanationLabelACCErrors.Text = ""
        ExplanationLabelACCGood.Text = "Accession Submitted"
        btnMPEAccClose.Visible = False
        btnMPEAccOkay.Visible = True
        mpeSubmittingAccession.Show()



    End Sub

    Private Sub AddRemoveUsers_Btn_Click(sender As Object, e As EventArgs) Handles AddRemoveUsers_Btn.Click
        AddRemoveUser_ID.Text = ""
        txtAdminUsrSrch.Text = ""
        pnlSrchUser.Visible = False
        lblAdminUsrResults.Text = ""
        AddRemoveUser_Modal.Show()
    End Sub

    Private Sub gvAccessions_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvAccessions.Sorting
        Try


            Dim sql As String = "select a.*, LastUpdated, isnull(d.Department + isnull(' ('+d.DepartmentNo+ ')', ''), a.Dept_ID) as Department,  convert(varchar, ImageReviewDate, 107) as ImageReviewDateDisplay " & _
            " from DWH.RAD_Op.Mammo_IR_AccessionHeader a " & _
            "left join DWH.RAD_Op.Mammo_IR_Department_LU d on Dept_ID=d.ID and d.Active = 1 " & _
                " left join (select AccID, Max(LastUpdated) as LastUpdated from ( " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_AccessionHeader " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_Comments " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions " & _
            "		where Active = 1 " & _
            "		group by AccID " & _
            "		union " & _
            "		select AccID, max(DateModified) as LastUpdated " & _
            "		from DWH.RAD_Op.Mammo_IR_Accession_Issues " & _
            "		where Active = 1 " & _
            "		group by AccID ) x " & _
            "		group by AccID) lu on lu.AccID = a.AccID " & _
            "where TechUserLogin='" & Replace(YourTechs_Gridview.DataKeys(YourTechs_Gridview.SelectedIndex).Value.ToString, "'", "''") & "' " & _
            " and a.Active=  1 " & _
            "order by AccID desc"

            Dim dv As DataView
            Dim sorts As String
            dv = GetData(sql).DefaultView

            sorts = e.SortExpression

            If e.SortExpression = gvAccessionmap.Text Then

                If CInt(gvAccessiondir.Text) = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    gvAccessiondir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    gvAccessiondir.Text = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                gvAccessiondir.Text = 1
                gvAccessionmap.Text = e.SortExpression
            End If

            gvAccessions.DataSource = dv
            gvAccessions.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub GetTechMonitoringGrid()

        Dim x As String = ""
        Dim y As String = ""
        For Each item As ListItem In cblFacsFilter.Items
            If item.Selected = True Then
                If item.Value = "0" Then
                    y = " or (ah.MaxReviewDate is not null and not exists (select * from DWH.RAD_Op.Mammo_IR_Dept_2_Techs a " & _
                " join DWH.Rad_Op.Mammo_IR_Department_LU b on a.Dept_ID = b.ID and b.Active = 1 " & _
                " where a.Active = 1 and a.UserLogin = mu.UserLogin) ) "
                Else
                    x += "'" & Replace(item.Value, "'", "''") & "', "
                End If

            End If
        Next

        If Len(x) > 0 Then
            x = "	or exists (select * from DWH.RAD_Op.Mammo_IR_Dept_2_Techs a " & _
                " join DWH.Rad_Op.Mammo_IR_Department_LU b on a.Dept_ID = b.ID and b.Active = 1 and b.Entity in (" & x & "'' ) " & _
                " where a.Active = 1 and a.UserLogin = mu.UserLogin)  "
        End If

        gvTechAssessment.DataSource = GetData("Select distinct isnull(UserDisplayName, UserFullName) as UserName, mu.UserLogin  , " & _
"    substring( " & _
"        ( " & _
"            Select distinct ', '+d.Department  AS [text()] " & _
"            From DWH.RAD_Op.Mammo_IR_Department_LU d " & _
"			join DWH.RAD_Op.Mammo_IR_Dept_2_Techs dt2 on dt2.Dept_ID = d.id and d.Active = 1 " & _
"            Where dt2.Active = 1 and dt2.UserLogin = mu.UserLogin " & _
"			and getdate() between isnull(dt2.EffectiveFrom, '1/1/1800') and isnull(dt2.EffectiveTo, '12/31/9999') " & _
"            ORDER BY ', '+d.Department " & _
"            For XML PATH ('') " & _
"        ), 3, 1000) [Departments] " & _
", convert(varchar, MaxReviewDate, 107) as MaxReviewDate " & _
"from DWH.RAD_Op.Mammo_IR_Users mu " & _
"left join (select TechUserLogin, max(ImageReviewDate) as MaxReviewDate  " & _
"	from DWH.RAD_Op.Mammo_IR_AccessionHeader where Active =1  " & _
"	group by TechUserLogin) ah  " & _
"	on mu.UserLogin = ah.TechUserLogin where 1 = 0 " & x & y & " order by 1")
        gvTechAssessment.DataBind()
    End Sub

    Private Sub gvTechAssessment_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvTechAssessment.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTechAssessment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTechAssessment.SelectedIndexChanged
        Try


            For Each canoe As GridViewRow In gvTechAssessment.Rows
                If canoe.RowIndex = gvTechAssessment.SelectedIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

            If gvTechAssessment.SelectedIndex >= 0 Then
                If ddlSelectTechDisplay.SelectedValue = "FQ" Then
                    GetTechAssessmentGrid(gvTechAssessment.SelectedDataKey("UserLogin"))
                    gvTechQuarters.Visible = True
                    gvTechMonths.Visible = False
                Else
                    GetTechAssessmentFMGrid(gvTechAssessment.SelectedDataKey("UserLogin"))
                    gvTechQuarters.Visible = False
                    gvTechMonths.Visible = True
                End If
                GetTechCorrectiveActions(gvTechAssessment.SelectedDataKey("UserLogin"))
                lblTechy.Text = "Selected Technologist: " & gvTechAssessment.SelectedDataKey("UserName")
                TRVisible1.Visible = True
                TRVisible2.Visible = True
                ddlSelectTechDisplay.Visible = True
            Else
                lblTechy.Text = "Please Select Technologist to Assess"
                TRVisible1.Visible = False
                TRVisible2.Visible = False
                ddlSelectTechDisplay.Visible = False
                gvTechQuarters.Visible = False
                gvTechMonths.Visible = False
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub GetTechAssessmentGrid(Tech As String)
        gvTechQuarters.DataSource = GetData("select convert(varchar, dd1.FY) + ' - FQ' + convert(varchar, dd1.Financial_Quarter) as QuarterName " & _
", sum(case when IssueCount > 0 then 1 else 0 end) as NotAcceptable  " & _
", sum(case when isnull(IssueCount, 0) > 0 then 0 when ah.AccID is not null then 1 else 0 end) as Acceptable  " & _
", sum(isnull(IssueCount, 0)) as ErrorCount " & _
", FY, Financial_Quarter " & _
"from ( " & _
"select max(ImageReviewDate) mx, min(ImageReviewDate) mn from DWH.RAD_Op.Mammo_IR_AccessionHeader where Active = 1 and TechUserLogin = '" & Replace(Tech, "'", "''") & "') a " & _
"join DWH.dbo.DimDate dd1 on dd1.Calendar_Date between mn and mx or dd1.Calendar_Date between DATEADD(month, -11, getdate()) and getdate() " & _
"left join DWH.RAD_Op.Mammo_IR_AccessionHeader ah on ah.ImageReviewDate = dd1.Calendar_Date and ah.Active = 1 and ah.TechUserLogin = '" & Replace(Tech, "'", "''") & "' " & _
"left join (select AccID, sum(isnull(Count, 0)) as IssueCount from DWH.RAD_Op.Mammo_IR_Accession_Issues where Active = 1 group by AccID) i on ah.AccID = i.AccID " & _
"group by convert(varchar, dd1.FY) + ' - FQ' + convert(varchar, dd1.Financial_Quarter)  " & _
", FY, Financial_Quarter " & _
"order by FY asc, Financial_Quarter asc ")
        gvTechQuarters.DataBind()
    End Sub

    Private Sub GetTechAssessmentFMGrid(Tech As String)
        gvTechMonths.DataSource = GetData("select convert(varchar, dd1.FY) + ' - ' + Month_Abbreviation as MonthName " & _
", sum(case when IssueCount > 0 then 1 else 0 end) as NotAcceptable  " & _
", sum(case when isnull(IssueCount, 0) > 0 then 0 when ah.AccID is not null then 1 else 0 end) as Acceptable  " & _
", sum(isnull(IssueCount, 0)) as ErrorCount " & _
", FY, FM " & _
"from ( " & _
"select max(ImageReviewDate) mx, min(ImageReviewDate) mn from DWH.RAD_Op.Mammo_IR_AccessionHeader where Active = 1 and TechUserLogin = '" & Replace(Tech, "'", "''") & "') a " & _
"join DWH.dbo.DimDate dd1 on dd1.Calendar_Date between mn and mx or dd1.Calendar_Date between DATEADD(month, -6, getdate()) and getdate() " & _
"left join DWH.RAD_Op.Mammo_IR_AccessionHeader ah on ah.ImageReviewDate = dd1.Calendar_Date and ah.Active = 1 and ah.TechUserLogin = '" & Replace(Tech, "'", "''") & "' " & _
"left join (select AccID, sum(isnull(Count, 0)) as IssueCount from DWH.RAD_Op.Mammo_IR_Accession_Issues where Active = 1 group by AccID) i on ah.AccID = i.AccID " & _
"group by convert(varchar, dd1.FY) + ' - ' + Month_Abbreviation  " & _
", FY, FM " & _
"order by FY asc, FM asc ")
        gvTechMonths.DataBind()
    End Sub

    Private Sub ddlSelectTechDisplay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectTechDisplay.SelectedIndexChanged
        If ddlSelectTechDisplay.SelectedValue = "FQ" Then
            GetTechAssessmentGrid(gvTechAssessment.SelectedDataKey("UserLogin"))
            gvTechQuarters.Visible = True
            gvTechMonths.Visible = False
        Else
            GetTechAssessmentFMGrid(gvTechAssessment.SelectedDataKey("UserLogin"))
            gvTechQuarters.Visible = False
            gvTechMonths.Visible = True
        End If

    End Sub

    Private Sub GetTechCorrectiveActions(Tech As String)
        gvTechActionHistory.DataSource = GetData("select 'False' as BtnVisible, 'AccID - '+convert(varchar, ca.AccID) as CorrID, CorrectiveAction, convert(varchar, ca.DateModified, 107) as DateRecorded, isnull(u.UserDisplayName , u.UserFullName) as UserName,  ca.DateModified " & _
            "from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions ca " & _
            "join DWH.RAD_Op.Mammo_IR_AccessionHeader ah on ca.AccID = ah.AccID and ah.Active = 1 " & _
            "left join DWH.RAD_Op.Mammo_IR_Users u on ca.ModifiedBy = u.UserLogin and u.Active = 1 " & _
            "where ah.TechUserLogin = '" & Replace(Tech, "'", "''") & "' " & _
            " " & _
            "union " & _
            " " & _
            "select 'True' as BtnVisible, 'CorrID - '+convert(varchar, ca.CorrectionID) as CorrID, cal.CorrectiveAction, convert(varchar, ca.DateLogged, 107) as DateRecorded, isnull(u.UserDisplayName , u.UserFullName) as UserName , ca.DateLogged  " & _
            "from DWH.RAD_Op.Mammo_IR_User_CorrectiveActions ca " & _
            "left join DWH.Rad_OP.Mammo_IR_Accession_CorrectiveActions_LU cal on ca.CorrID = cal.CorrID and cal.Active = 1 " & _
            "left join DWH.RAD_Op.Mammo_IR_Users u on ca.LoggedBy = u.UserLogin and u.Active = 1 " & _
            "where ca.Active = 1 and ca.UserLogin = '" & Replace(Tech, "'", "''") & "' order by ca.DateModified ")
        gvTechActionHistory.DataBind()

        If gvTechActionHistory.Rows.Count() > 0 Then
            gvTechActionHistory.Visible = True
        Else
            gvTechActionHistory.Visible = False
        End If

    End Sub

    Private Sub btnAddAction_Click(sender As Object, e As EventArgs) Handles btnAddAction.Click
        ddlCorrectiveAction.DataSource = GetData("select CorrectiveAction, CorrID from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions_LU " & _
                "where ActionType = 'Management' and Active = 1 union select '(Select Corrective Action)', 0 " & _
                "order by CorrID ")
        ddlCorrectiveAction.DataBind()
        lblCorrectiveActionError.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)
        mpeCorrectiveActions.Show()
    End Sub

    Private Sub btnRecordImprovement_Click(sender As Object, e As EventArgs) Handles btnRecordImprovement.Click
        ddlCorrectiveAction.DataSource = GetData("select CorrectiveAction, CorrID from DWH.RAD_Op.Mammo_IR_Accession_CorrectiveActions_LU " & _
        "where ActionType = 'Improvement' and Active = 1 union select '(Has there been Improvement?)', 0 " & _
        "order by CorrID ")
        ddlCorrectiveAction.DataBind()
        lblCorrectiveActionError.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)
        mpeCorrectiveActions.Show()
    End Sub

    Private Sub btnAddCorrectiveAction_Click(sender As Object, e As EventArgs) Handles btnAddCorrectiveAction.Click
        Try

            If ddlCorrectiveAction.SelectedValue <> "0" Then
                Dim x As String = "Insert into DWH.RAD_Op.Mammo_IR_User_CorrectiveActions (CorrectionID, UserLogin, CorrID, Active, DateLogged, LoggedBy, AdditionalComment) values (" & _
                 "(select isnull(max(CorrectionID), 0) + 1 from DWH.RAD_Op.Mammo_IR_User_CorrectiveActions), '" & Replace(gvTechAssessment.SelectedDataKey("UserLogin"), "'", "''") & _
                    "', '" & ddlCorrectiveAction.SelectedValue & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', null)"

                ExecuteSql(x)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)
                GetTechCorrectiveActions(gvTechAssessment.SelectedDataKey("UserLogin"))
                mpeCorrectiveActions.Hide()
                lblCorrectiveActionError.Text = ""
            Else
                lblCorrectiveActionError.Text = "No Corrective Action Selected"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)
                GetTechCorrectiveActions(gvTechAssessment.SelectedDataKey("UserLogin"))
                mpeCorrectiveActions.Show()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub cblFacsFilter_Get()

        cblFacsFilter.DataSource = GetData("select distinct Entity  " & _
            ", case when Entity = 10 then 'Atlanta' " & _
            "    when Entity = 22 then 'Cherokee' " & _
            "	 when Entity = 6 then 'Forsyth' " & _
            "	 else 'Entity ' + convert(varchar, Entity) end as EntityDisplay, 1 as sort " & _
            " from DWH.Rad_OP.Mammo_IR_Department_LU where Active = 1 union " & _
            "select distinct  0, 'Unspecified', 2 from DWH.RAD_Op.Mammo_IR_AccessionHeader ah " & _
            " where not exists (select * from DWH.RAD_Op.Mammo_IR_Dept_2_Techs a  " & _
             "    join DWH.Rad_Op.Mammo_IR_Department_LU b on a.Dept_ID = b.ID and b.Active = 1 " & _
             "   where a.Active = 1 and a.UserLogin = ah.TechUserLogin)  order by 3, 2")
        cblFacsFilter.DataTextField = "EntityDisplay"
        cblFacsFilter.DataValueField = "Entity"
        cblFacsFilter.DataBind()

        For Each item As ListItem In cblFacsFilter.Items
            item.Selected = True
        Next

    End Sub

    Private Sub cblFacsFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblFacsFilter.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)
        GetTechMonitoringGrid()
    End Sub

    Private Sub gvTechActionHistory_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTechActionHistory.RowCommand

        Try
            Dim CommandArg As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            If Commander = "RemoveCorrectiveAction" Then

                Dim PrepSQL As String

                If Left(CommandArg, 8) = "AccID - " Then
                Else
                    PrepSQL = "update  DWH.RAD_Op.Mammo_IR_User_CorrectiveActions " & _
                " set Active = 0, DateInactivated = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'" & _
                " where CorrectionID = '" & Replace(Right(CommandArg, Len(CommandArg) - 8), "'", "''") & "' and Active = 1 "
                    ExecuteSql(PrepSQL)
                    GetTechCorrectiveActions(gvTechAssessment.SelectedDataKey("UserLogin"))
                End If


            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class