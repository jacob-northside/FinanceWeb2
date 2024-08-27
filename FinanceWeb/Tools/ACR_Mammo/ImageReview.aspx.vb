Imports FinanceWeb.WebFinGlobal
Imports System.IO
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web.Services.WebService
Imports System.Web.Script.Serialization

Public Class ImageReview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim HTTPRequest As String = ""


            'Using reader As StreamReader = New StreamReader(HttpContext.Current.Request.InputStream)
            '    HTTPRequest = reader.ReadToEnd()
            'End Using
            'Debug.Print("REQUEST >>> " + HTTPRequest.ToString)

            'If HttpContext.Current.Request.ContentType = "application/json; charset=UTF-8" Then
            '    Dim js As JavaScriptSerializer
            '    js = New JavaScriptSerializer

            '    Dim JsonRequest As Object
            '    JsonRequest = js.Deserialize(Of Object)(HTTPRequest)

            '    If JsonRequest("requestsource") = "AccessionIDs" Then
            '        Dim jsonresponse As String
            '        FetchAccessionIDs(JsonRequest("UserID"))
            '        Exit Sub
            '    End If
            'End If

            If IsPostBack Then
                Debug.Print("Postback At" + Now)
            Else
                If User.Identity.IsAuthenticated = True Then
                    GetUserName()

                    InitializeEvalForm()
                    InitializeManageDDL()
                    LoadYourTechRoster()
                    LoadTechNames_Gridview()
                    Eval_GetTechList()
                Else

                End If

            End If

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Protected Sub LoadTechNames_Gridview()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim SQL As String = "Select UserLogin, isnull(UserFullName, UserDisplayName) as Name FROM DWH.RAD_Op.Mammo_IR_Users where Active = 1 and Tech = 1"

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
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
    End Sub


    Protected Sub GetUserName()
        ' For obtaining username '
        Dim search As DirectorySearcher
        Dim entry As DirectoryEntry
        Dim temp As String = ""
        ' For obtaining username '
        Try
            entry = New DirectoryEntry("LDAP://DC=northside, DC=local")
            search = New DirectorySearcher(entry)
            search.Filter = "(&(objectClass=user)(samaccountname=" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "))"
            'Dim i As Integer = search.Filter.Length

            If search.Filter.ToString = "(&(objectClass=user)(samaccountname=))" Then
                Exit Sub
            End If
            For Each AdObj As SearchResult In search.FindAll()
                temp = temp & AdObj.GetDirectoryEntry.Properties.Item("cn").Value
            Next




        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
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
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
    End Sub
    Protected Sub InitializeManageDDL()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select ID, Department from DWH.RAD_Op.Mammo_IR_Department_LU where Active = 1 order by Department;"

        Dim SQL As String = "Select d.ID,  d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay, 1 as ord " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "where dim.Active = 1 " & _
            "and dim.UserLogin = @SrchUsr union Select -1, 'Select Department (Optional)', 0  order by ord, 2, 1"

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
                ddlManageDepartments.DataTextField = "DepartmentDisplay"
                ddlManageDepartments.DataBind()

                conn.Close()
            End Using

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Protected Sub InitializeEvalForm()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select ID, Department from DWH.RAD_Op.Mammo_IR_Department_LU where Active = 1 order by Department;"

        Dim SQL As String = "Select d.ID,  d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay, 1 as ord " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "where dim.Active = 1 " & _
            "and dim.UserLogin = @SrchUsr union Select -1, 'Select Department', 0  order by ord, 2, 1"

        Try
            Debug.Print("Executing SQL => " & Sql)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchUsr", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
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
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
    End Sub

    Protected Sub Eval_GetTechList()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select names.name Name, * from DWH.Rad_OP.Mammo_IR_Users dim " & _
        '                        "left join DWH.Rad_OP.FDAMAMMO_OrgChart_FACT fact " & _
        '                            "on dim.id = fact.supervisor and fact.Active = 1 " & _
        '                        "left join DWH.Rad_OP.FDAMAMMO_Users_DIM names " & _
        '                            "on fact.employee = names.id and names.Active = 1 " & _
        '                        "where dim.Active = 1 and dim.employee_number = @SrchID;"

        Dim SQL As String = "Select usr.UserLogin, isnull(usr.UserDisplayName, usr.UserFullName) as UserName " & _
            " ,d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Techs d2t on d.ID = d2t.Dept_ID and d2t.Active = 1 and isnull(d2t.SupervisorLogin, dim.UserLogin) = dim.UserLogin " & _
            "	and getdate() between isnull(d2t.EffectiveFrom, '1/1/1800') and isnull(d2t.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Users usr on d2t.UserLogin = usr.UserLogin and usr.Active = 1 and usr.Tech = 1 " & _
            "where dim.Active = 1 and dim.UserLogin = @SrchID "

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.Parameters.AddWithValue("@SrchID", Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""))
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                YourTechs_Gridview.DataSource = ds
                YourTechs_Gridview.DataMember = "LookUpData"
                YourTechs_Gridview.DataBind()
                conn.Close()
            End Using

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
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

            Dim js As JavaScriptSerializer
            js = New JavaScriptSerializer

            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In ds.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In ds.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next

            Dim jsonresponse As String
            jsonresponse = js.Serialize(rows)
            Debug.Print("Accession IDs returned: " & jsonresponse)

            Response.Clear()
            Response.ContentType = "application/json"
            Response.Write(jsonresponse)

            HttpContext.Current.Response.Flush() 'Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = True  'Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest()  'Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            'HttpContext.Current.Response.End() ' 


            'Response.End()

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try


    End Sub


    Protected Sub LoadYourTechRoster()
        Dim ds As DataTable
        Dim da As SqlDataAdapter
        'Dim SQL As String = "Select 'ACC1' UserID union all select 'acc2';"
        Dim SQL As String = "Select usr.UserLogin, isnull(usr.UserDisplayName, usr.UserFullName) as UserName " & _
            " ,d.Department + isnull(' (' + d.DepartmentNo + ')', '') as DepartmentDisplay " & _
            "from DWH.Rad_OP.Mammo_IR_Users dim  " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Supervisor d2s on dim.UserLogin = d2s.UserLogin and d2s.Active = 1  " & _
            "	and getdate() between isnull(d2s.EffectiveFrom, '1/1/1800') and isnull(d2s.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Department_LU d on isnull(d2s.Dept_ID, d.ID) = d.ID and d.Active =1 " & _
            "join DWH.RAD_Op.Mammo_IR_Dept_2_Techs d2t on d.ID = d2t.Dept_ID and d2t.Active = 1 and isnull(d2t.SupervisorLogin, dim.UserLogin) = dim.UserLogin " & _
            "	and getdate() between isnull(d2t.EffectiveFrom, '1/1/1800') and isnull(d2t.EffectiveFrom, '12/31/9999') " & _
            "join DWH.RAD_Op.Mammo_IR_Users usr on d2t.UserLogin = usr.UserLogin and usr.Active = 1 and usr.Tech = 1 " & _
            "where dim.Active = 1 and dim.UserLogin = @SrchID and (d.ID = @DepID or @DepID = -1)"

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
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
        End Try
    End Sub


    Private Sub ddlManageDepartments_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageDepartments.SelectedIndexChanged
        LoadTechNames_Gridview()
    End Sub
End Class