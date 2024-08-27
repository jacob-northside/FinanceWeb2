Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Math
Imports System.DirectoryServices
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal

Imports FinanceWeb.WebFinGlobal


Public Class Activity_Report
    Inherits System.Web.UI.Page
    'Private Shared ARDetailView As New DataView
    'Private Shared ARDetailmap As String
    'Private Shared ARDetaildir As Integer
    'Private Shared UserName As String
    Private Shared WebAdminEmail As String = "chelsea.weirich@northside.com"
    'Private Shared Admin As Integer = 0
    'Private Shared Assign As Integer = 0
    'Private Shared NettingView As New DataView
    'Private Shared Nettingmap As String
    'Private Shared Nettingdir As Integer
    'Private Shared Research As Integer = 0
    'Private Shared UsersView As New DataView
    'Private Shared usersdir As Integer
    'Private Shared userssort As String
    'Private Shared Developer As Integer = 0
    ' Private Shared Mimic As String = "E252330"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

        Else
            Try

                Dim r As New Random()
                Dim n As Integer = r.Next(10)

                Dim mp As TableCell = upProg_Pacman.FindControl("tcAjaxLoader")
                If Now().Hour < 9 Then
                    If n < 7 Then
                        mp.Attributes.Add("class", "AjaxLoader2")
                    Else
                        mp.Attributes.Add("class", "AjaxLoader1")
                    End If
                Else
                    If n < 3 Then
                        mp.Attributes.Add("class", "AjaxLoader2")
                    Else
                        mp.Attributes.Add("class", "AjaxLoader1")
                    End If
                End If



                ExecuteSql("Update DWH.AR.ActivityReport_Detail set Carry_Forward = null where Carry_Forward = 0")

                'Mimic.Text = "E252330"
                Mimic.Text = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")

                GetAdminStatus()
                RefreshDates()
                UserDropDown()


                Try
                    ddlARAssignedUser.SelectedValue = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
                Catch ex As Exception
                    ddlARAssignedUser.SelectedIndex = 0
                End Try

                InitialPopulateCheckboxes()
                ARDetailView()
                Dim zz As DataView = Session("ARDetailView")
                zz.RowFilter = DSFilter.Text
                gv_AR_MainData.DataSource = zz ' ARDetailView()
                gv_AR_MainData.DataBind()
                BalanceCheck()

            Catch ex As Exception
                explanationlabel.Text = "Error loading page.  Please contact Website Administrator (" & WebAdminEmail & ")."
                explanationlabel.DataBind()
                ModalPopupExtender.Show()
                LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If

    End Sub

    'Private Sub ReloadMainPage()

    '    PopulateCheckboxes()
    '    PopulateGrid()

    'End Sub

    Private Sub ReloadMainPageR()
        Try

   
        Dim se As String = gv_AR_MainData.SortExpression
        Dim pg As Integer = gv_AR_MainData.PageIndex
        ARDetailView()
            Dim x As DataView = Session("ARDetailView") ' ARDetailView()
            x.RowFilter = DSFilter.Text
        Dim y As String = ARDetailmap.Text
        Dim z As String = ARDetaildir.Text

        Try
            If CInt(ARDetaildir.Text) = 1 Then
                x.Sort = y + " " + "asc"
            Else
                x.Sort = y + " " + "desc"
            End If
        Catch ex As Exception
            x.Sort = se
        End Try


            'PopulateCheckboxes()
        BalanceCheck()


        gv_AR_MainData.DataSource = x
        gv_AR_MainData.PageIndex = pg
        gv_AR_MainData.DataBind()

        Catch ex As Exception
            explanationlabel.Text = "Error loading page.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub GetAdminStatus()
        Try
            ' Flipping Tables 5/17/2017 CRW
            'Dim s As String = "select count(*) from DWH.AR.Activity_Users " & _
            '"where Administrator = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            Dim zz As String = "select count(*) from WebFD.dbo.aspnet_Roles r with (nolock) " & _
                "join WebFD.dbo.aspnet_UsersInRoles uir with (nolock)  on r.RoleId = uir.RoleId " & _
                "join WebFD.dbo.aspnet_Users u with (nolock)  on uir.UserId = u.UserId " & _
                "where RoleName = 'Developer' and UserName = '" & Mimic.Text & "' "

            Developer.Text = GetScalar(zz)

            If CInt(Developer.Text) > 0 Then
                btnRunDailyProcessing.Visible = True
                btnResetToday.Visible = True
                btnHoliday.Visible = True
                PopulateAssignmentDropDown()
                PopulateCurrentAssignments()
                tpAdministrative.Visible = True
                PopulateManagementDropDown()
            End If

            Dim s As String = "select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
            "where Administrator = 1 and Active = 1 and UserLogin = '" & Mimic.Text & "' "

            Admin.Text = GetScalar(s)

            If CInt(Admin.Text) > 0 Then
                'btnRunDailyProcessing.Visible = True
                PopulateAssignmentDropDown()
                PopulateCurrentAssignments()
                tpAdministrative.Visible = True
                PopulateManagementDropDown()
            End If

            If GetScalar("select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
            "where RunProcessing = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
                btnRunDailyProcessing.Visible = True
            End If

            If GetScalar("select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
            "where ResetRights = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
                btnResetToday.Visible = True
            End If

            If GetScalar("select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
            "where HolidayRights = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
                btnHoliday.Visible = True
            End If

            Dim s2 As String = "select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
            "where AssignRights = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            Assign.Text = GetScalar(s2)

            Dim r As String = "select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
            "where Research = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            Research.Text = GetScalar(r)

            Dim x As String = "select isnull(max(isnull(MultipleLocks, 2)), 1) from DWH.AR.ActivityReport_Users  with (nolock)  " & _
            "where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1 "

            If GetScalar(x) > 0 Then
                btnSubmitAllRows.Visible = True
            Else
                btnSubmitAllRows.Visible = False
            End If

            Dim f As String = "select isnull(max(isnull(Netting, 2)), 1) from DWH.AR.ActivityReport_Users  with (nolock)  " & _
                "where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1 "

            If GetScalar(f) > 0 Then
                btnNetRows.Visible = True
            Else
                btnNetRows.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub RefreshDates()

        Try
            'txtARDate.Text = Today()
            Dim RunDate As String = "(select Value from DWH.AR.ActivityReport_ExtraValues  with (nolock)  where Description = 'RunDate' and Active = 1)"
            txtARDate.Text = GetDate(RunDate)
            'txtARDate.Text = "7/11/2017"
            'If Admin > 0 Then
            '    txtARDate.Enabled = True
            'Else
            '    txtARDate.Enabled = False
            'End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub UserDropDown()

        'Dim s As String = "select UserLogin, UserDisplayName, 1 as ord from DWH.AR.Activity_Users " & _
        '"where Active = 1 " & _
        '"  union select distinct ' - View All - ' , '- View All -', -1 union select distinct 'Unassigned' , '- Unassigned -', 0 union " & _
        '"select distinct isnull(AssignedUser, 'Unassigned') ,isnull(isnull(au.UserDisplayName, ad.AssignedUser), '- Unassigned -'), isnull(au.Active, 0) " & _
        '"from DWH.AR.Activity_Detail ad " & _
        '"left join DWH.AR.Activity_Users au on ad.AssignedUser = au.UserLogin   " & _
        '"where ad.Active = 1 and ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
        '"order by 3, 2 "

        Dim s As String = "select Upper(UserLogin) as UserLogin, isnull(isnull(UserDropDownListName, left(UserFullName, 10)), UserLogin) as UserDisplayName, 1 as ord, isnull(Assignable, 0) as Assignable  from DWH.AR.ActivityReport_Users  with (nolock)  " & _
        "where Active = 1 " & _
        "  union select distinct ' - View All - ' , '- View All -', -1, 0 as Assignable union select distinct Upper('Unassigned') , '- Unassigned -', 0, 1 as Assignable union " & _
        "select distinct Upper(isnull(UserAssigned, 'Unassigned')) ,isnull(isnull(isnull(UserDropDownListName, left(UserFullName, 10)), UserAssigned), '- Unassigned -'), case when UserAssigned is null then 0 else 1 end, 0 " & _
        "from  DWH.AR.ActivityReport ar   with (nolock)  " & _
        "join DWH.AR.ActivityReport_Assignments aa  with (nolock)  on ar.ActivityID = aa.ActivityID " & _
        "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users  with (nolock) ) au on au.rn = 1 and aa.UserAssigned = au.UserLogin " & _
        "where ar.Active = 1 and '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' between ar.FirstActivityDate and isnull(ar.FinalActivityDate, '12/31/9999') " & _
        "and aa.userAssigned is not null and not exists (select * from DWH.AR.ActivityReport_Users x  with (nolock)  where x.Active = 1 and aa.UserAssigned = x.UserLogin) " & _
        "order by 3, 2 "

        Session("UserDropDown") = GetData(s).DefaultView

        ddlARAssignedUser.DataSource = Session("UserDropDown")
        ddlARAssignedUser.DataTextField = "UserDisplayName"
        ddlARAssignedUser.DataValueField = "UserLogin"
        ddlARAssignedUser.DataBind()

        ddlNetRowUser.DataSource = Session("UserDropDown")
        ddlNetRowUser.DataTextField = "UserDisplayName"
        ddlNetRowUser.DataValueField = "UserLogin"
        ddlNetRowUser.DataBind()

    End Sub

    Sub PopulateManagementDropDown()

        Dim x As String = "select 'Assignments' as Roles, 'User_Responsibility' as Tbls " & _
            "union " & _
            "select 'BAI Type Codes' as Roles, 'BAITypeCodes_LU' as Tbls " & _
            "union " & _
            "select 'Bank Account Numbers' as Roles, 'BankAccountNumber_LU' as Tbls " & _
            "union " & _
            "select 'Cash Journal Types' as Roles, 'CashCategory_Type_LU' as Tbls " & _
            "union " & _
            "select 'Lockbox Codes' as Roles, 'LockboxCodes_LU' as Tbls " & _
            "union " & _
            "select 'Misc GL Codes' as Roles, 'MiscGLCodes_LU' as Tbls " & _
            "union " & _
            "select 'Overrides' as Roles, 'Overrides_LU' as Tbls " & _
            "union " & _
            "select 'Unresolved Reasons' as Roles, 'Unresolved_LU' as Tbls "

        ' As a general note, the Tbls were set up as the table it was pulled from initially in the hope of making it more dynamic; that is probaly more trouble
        ' than it's worth; for now it is for easy reference, but does not particularly need to be set up that way, in case another Cash Category Type dropdown was added, for example

        If GetScalar("select count(*) from DWH.AR.ActivityReport_Users  with (nolock)  " & _
            "where AssignRoles = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
            x += "union " & _
                "select 'Users' as Roles, 'Users' as Tbls "
        ElseIf CInt(Developer.Text) = 1 Then
            x += "union " & _
                "select 'Users' as Roles, 'Users' as Tbls "
        End If

        ddlManageWhat.DataSource = GetData(x)
        ddlManageWhat.DataTextField = "Roles"
        ddlManageWhat.DataValueField = "Tbls"
        ddlManageWhat.DataBind()

    End Sub

    Sub InitialPopulateCheckboxes()

        'Dim s As String = "select distinct CashCategory, 'True' as Checked from DWH.AR.Activity_Detail " & _
        '"where Active = 1 " & _
        '"order by 1 "

        Dim s As String = "select distinct CashCategory, InitiallySelected as Checked from DWH.AR.ActivityReport_CashCategory  with (nolock)  " & _
         "where Active = 1 " & _
         "order by 2 desc, 1 "

        Dim d As New DataTable
        d = GetData(s)
        Dim x As Integer = 0
        cblARCategory.DataSource = d.DefaultView
        cblARCategory.DataTextField = "CashCategory"
        cblARCategory.DataBind()

        For Each item As ListItem In cblARCategory.Items
            If d.Rows(x)(1) = 1 Then
                item.Selected = True
            End If
            x += 1
        Next

        '        Dim s2 As String = "select distinct CategoryStatus, case when CategoryStatus = 'Current' then 'True' else 'False' end as Checked from DWH.AR.Activity_Detail " & _
        '"where Active = 1 union select 'Research', 'False' where " & Research & " > 0 " & _
        '"order by 1 "

        Dim s2 As String = "select distinct DetailStatus from DWH.AR.ActivityReport_Detail  with (nolock)  " & _
        "order by 1 "

        cblARCategoryStatus.DataSource = GetData(s2)
        cblARCategoryStatus.DataTextField = "DetailStatus"
        cblARCategoryStatus.DataBind()
        For Each item As ListItem In cblARCategoryStatus.Items
            If item.Text = "Current" Then
                item.Selected = True
            End If
        Next

        'Dim s3 As String = "select distinct isnull(Facility, 'Unassigned') as Facility, 'True' as Checked from DWH.AR.Activity_Detail " & _
        '"where Active = 1 " & _
        '"order by 1 "

        'Dim s3 As String = "select distinct substring(DisplayDescription, 1 " & _
        '"	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end) as FacDisplay, Facility " & _
        '"from DWH.AR.ActivityReport_BankAccountNumber_LU where Active = 1 " & _
        '"order by 1 "

        Dim s3 As String = "select distinct rtrim(substring(DisplayDescription, 1 " & _
        "	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end)) + ' - ' + Facility as FacDisplay, Facility " & _
        "from DWH.AR.ActivityReport_BankAccountNumber_LU  with (nolock)  where Active = 1" & _
        "order by 1 "

        cblARFacility.DataSource = GetData(s3)
        cblARFacility.DataTextField = "FacDisplay"
        cblARFacility.DataValueField = "Facility"
        cblARFacility.DataBind()
        For Each item As ListItem In cblARFacility.Items
            item.Selected = True
        Next

        Dim s3v2 As String = "select distinct substring(DisplayDescription, 1 " & _
        "	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end) as FacDisplay, Facility, 1 as ord " & _
        "from DWH.AR.ActivityReport_BankAccountNumber_LU  with (nolock)  where Active = 1 union select '(Select Facility)', ' -- NoneSelected -- ', 0 " & _
        "order by ord, 1 "

        ddlNCJFacility.DataSource = GetData(s3v2)
        ddlNCJFacility.DataTextField = "FacDisplay"
        ddlNCJFacility.DataValueField = "Facility"
        ddlNCJFacility.DataBind()

        Dim s4 As String = "select distinct isnull(DropDownListDisplay, isnull(left(FullDisplay, 12), Type)) as TypeDisplay, Type, 1 as ord " & _
"from DWH.AR.ActivityReport_CashCategory_Type_LU  with (nolock)  where Active = 1 and CashCategory = 'Cash Journal' union select '(Select Type)', ' -- NoneSelected -- ', 0 " & _
"order by ord, 1 "

        ddlNCJType.DataSource = GetData(s4)
        ddlNCJType.DataTextField = "TypeDisplay"
        ddlNCJType.DataValueField = "Type"
        ddlNCJType.DataBind()

    End Sub
    Sub PopulateCheckboxes()

        Dim CashCatList As New List(Of String)

        For Each Listy As ListItem In cblARCategory.Items
            If Listy.Selected Then
                CashCatList.Add(Listy.Value)
            End If
        Next

        ' Old strings in Sub InitialPopulateCheckboxes()

        Dim s As String = "select distinct CashCategory, InitiallySelected as Checked from DWH.AR.ActivityReport_CashCategory  with (nolock)  " & _
         "where Active = 1 " & _
         "order by 2 desc, 1 "

        cblARCategory.DataSource = GetData(s)
        cblARCategory.DataTextField = "CashCategory"
        cblARCategory.DataBind()

        For Each item As ListItem In cblARCategory.Items
            If CashCatList.Contains(item.Value) Then
                item.Selected = True
            Else
                item.Selected = False
            End If
        Next


        Dim ARCatStatusList As New List(Of String)

        For Each Listy As ListItem In cblARCategoryStatus.Items
            If Listy.Selected Then
                ARCatStatusList.Add(Listy.Value)
            End If
        Next
        Dim s2 As String = "select distinct DetailStatus from DWH.AR.ActivityReport_Detail  with (nolock)  " & _
         "order by 1 "

        cblARCategoryStatus.DataSource = GetData(s2)
        cblARCategoryStatus.DataTextField = "DetailStatus"
        cblARCategoryStatus.DataBind()

        For Each item As ListItem In cblARCategoryStatus.Items
            If ARCatStatusList.Contains(item.Value) Then
                item.Selected = True
            Else
                item.Selected = False
            End If
        Next

        Dim FACList As New List(Of String)

        For Each Listy As ListItem In cblARFacility.Items
            If Listy.Selected Then
                FACList.Add(Listy.Value)
            End If
        Next

        Dim s3 As String = "select distinct rtrim(substring(DisplayDescription, 1 " & _
        "	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end)) + ' - ' + Facility as FacDisplay, Facility " & _
        "from DWH.AR.ActivityReport_BankAccountNumber_LU  with (nolock)  where Active = 1" & _
        "order by 1 "

        cblARFacility.DataSource = GetData(s3)
        cblARFacility.DataTextField = "FacDisplay"
        cblARFacility.DataValueField = "Facility"
        cblARFacility.DataBind()

        For Each item As ListItem In cblARFacility.Items
            If FACList.Contains(item.Value) Then
                item.Selected = True
            Else
                item.Selected = False
            End If
        Next

    End Sub

    Private Sub ARDetailView()
        Dim Balanced As String = "and ( "
        Dim ResearchQ As String = ""
        Dim SearchString As String = ""
        Dim JoinString As String = "Join (select '' as SearchResult) z on 1 = 1 "

        If Len(Trim(txtSrch.Text)) > 0 Then
            SearchString = "declare @SearchWord varchar(max) = '" & Replace(Trim(txtSrch.Text), "'", "''") & "'  " & _
                    "declare @MinorWord varchar(max) = '' " & _
                    "declare @AreaSearch varchar(max) = '' " & _
                    " " & _
                    " If OBJECT_ID('tempdb.dbo.#TempFound', 'U') IS NOT NULL  " & _
                    "  DROP TABLE #TempFound; " & _
                    "select distinct 'Type' as Area, isnull(Type, '') as Searchy , ard.ActivityID, null as Flagged  " & _
                    "into #TempFound " & _
                    "from DWH.AR.ActivityReport_Detail ard with (nolock)  " & _
"join #AR_Temp arx with (nolock) on arx.ActivityID = ard.ActivityID   " & _
"	 " & _
"where ard.Active = 1 and isnull(Type, '') <> ''  " & _
                    "union all " & _
                    "select distinct 'BankBatchNumber', isnull(BankBatchNumber , '') as Searchy , ard.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Detail ard with (nolock)  " & _
"join #AR_Temp arx with (nolock) on arx.ActivityID = ard.ActivityID   " & _
"where ard.Active = 1 and isnull(BankBatchNumber, '') <> ''  " & _
                    "union all " & _
                    "select distinct 'STARBatchNumber', isnull(STARBatchNumber , '') as Searchy , ard.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Detail ard with (nolock)  " & _
"join #AR_Temp arx with (nolock) on arx.ActivityID = ard.ActivityID  " & _
"where ard.Active = 1 and isnull(STARBatchNumber, '') <> ''  " & _
                    "union all " & _
                    "select distinct 'CashReceived', isnull(convert(varchar(max), Cash_Received) , '') as Searchy , ard.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Detail ard with (nolock)  " & _
"join #AR_Temp arx with (nolock) on arx.ActivityID = ard.ActivityID   " & _
"where ard.Active = 1 and isnull(convert(varchar, Cash_Received), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'ARPosted', isnull(convert(varchar(max), AR_Posted) , '') as Searchy , ard.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Detail ard with (nolock)  " & _
"join #AR_Temp arx with (nolock) on arx.ActivityID = ard.ActivityID   " & _
"where ard.Active = 1 and isnull(convert(varchar, AR_Posted), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'Interest', isnull(convert(varchar(max), Interest) , '') as Searchy , ard.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Detail ard with (nolock)  " & _
"join #AR_Temp arx with (nolock) on arx.ActivityID = ard.ActivityID   " & _
"where ard.Active = 1 and isnull(convert(varchar, Interest), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'FileName', isnull(convert(varchar(max), FileName) , '') as Searchy , arf.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_AttachedFiles arf with (nolock)  " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arf.ActivityID  " & _
                    "where arf.Active = 1 and isnull(convert(varchar, FileName), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'Comment', isnull(convert(varchar(max), Comment) , '') as Searchy , arc.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Comments arc with (nolock)  " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arc.ActivityID  " & _
                    "where arc.Active = 1 and isnull(convert(varchar, Comment), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'CommentType', isnull(convert(varchar(max), CommentType) , '') as Searchy , arc.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Comments arc with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arc.ActivityID   " & _
                    "where arc.Active = 1 and isnull(convert(varchar, CommentType), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'MiscGLComment', isnull(convert(varchar(max), Comment) , '') as Searchy , arm.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_MiscGL arm with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arm.ActivityID   " & _
                    "where arm.Active = 1 and isnull(convert(varchar, Comment), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'MiscAmount', isnull(convert(varchar(max), MiscAmt) , '') as Searchy , arm.ActivityID, null as Flagged   " & _
                    "from DWH.AR.ActivityReport_MiscGL arm with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arm.ActivityID  " & _
                    "where arm.Active = 1 and isnull(convert(varchar, MiscAmt), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'NetComment', isnull(convert(varchar(max), NetComment) , '') as Searchy , InitialID  , null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Netting arn with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arn.InitialID   " & _
                    "where arn.Active = 1 and isnull(convert(varchar, NetComment), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'NetComment', isnull(convert(varchar(max), NetComment) , '') as Searchy , arn.NetID  , null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Netting arn with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arn.NetID  " & _
                    "where arn.Active = 1 and isnull(convert(varchar, NetComment), '') <> '' " & _
                    "union all " & _
                    "select distinct 'NetAmount', isnull(convert(varchar(max), Amount) , '') as Searchy , InitialID  , null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Netting arn with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arn.InitialID " & _
                    "where arn.Active = 1 and isnull(convert(varchar, Amount), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'NetAmount', isnull(convert(varchar(max), Amount) , '') as Searchy , arn.NetID  , null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Netting arn with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = arn.NetID  " & _
                    "where arn.Active = 1 and isnull(convert(varchar, Amount), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'TransferAmount', isnull(convert(varchar(max), Amount) , '') as Searchy , art.TransferFrom  , null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Transfers art with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = art.TransferFrom  " & _
                    "where art.Active = 1 and isnull(convert(varchar, Amount), '') <> ''  " & _
                    "union all " & _
                    "select distinct 'TransferAmount', isnull(convert(varchar(max), Amount) , '') as Searchy , art.TransferTo  , null as Flagged   " & _
                    "from DWH.AR.ActivityReport_Transfers art with (nolock) " & _
                    "join #AR_Temp arx with (nolock) on arx.ActivityID = art.TransferTo   " & _
                    "where art.Active = 1 and isnull(convert(varchar, Amount), '') <> ''  " & _
                    " " & _
                    " " & _
                    "while len(@SearchWord) > 0 " & _
                    "begin " & _
                    "if (Charindex(' ', @SearchWord) > Patindex('%:(%)%', @SearchWord) or Charindex(' ', @SearchWord) = 0) and Patindex('%:(%)%', @SearchWord) > 0  " & _
                    "begin " & _
                    "set @AreaSearch = substring(@SearchWord, 1, Charindex(':(', @SearchWord) - 1) " & _
                    "set @MinorWord =  substring(@SearchWord, Charindex(':(', @SearchWord) + 2, Charindex(')', @SearchWord) - Charindex(':(', @SearchWord) - 2) " & _
                    "set @SearchWord = SUBSTRING(@SearchWord,  Charindex(')', @SearchWord) + 1, len(@Searchword)) " & _
                    "end " & _
                    "else " & _
                    "begin " & _
                    "set @AreaSearch = '' " & _
                    "set @MinorWord = substring(@SearchWord, 1, case when charindex(' ', @SearchWord, 0) = 0 then len(@SearchWord) else charindex(' ', @SearchWord, 0) - 1 end)   " & _
                    "set @SearchWord = case when charindex(' ', @SearchWord, 0) = 0 then '' else substring(@SearchWord, charindex(' ', @SearchWord, 0) + 1, len(@SearchWord)) end " & _
                    "end " & _
                    " " & _
                    " If len(@MinorWord) > 1 begin " & _
                    "update #TempFound " & _
                    "set Flagged = 1 " & _
                    "where Searchy like '%'+@MinorWord+'%' and (Area = @AreaSearch or @AreaSearch = '') " & _
                    " " & _
                    "end end " & _
                    " " & _
                    " delete a from #AR_Temp a  left join (select distinct ActivityID from #TempFound where Flagged is not null) b on a.ActivityID = b.activityID  where b.ActivityID is null "


            JoinString = "Join (Select distinct ActivityID, '' as SearchResult " & _
                    "From #TempFound ST2 " & _
                    "where Flagged = 1 ) z on z.ActivityID = zq.ActivityID"

            ' Optional for change
            'JoinString = "Join (Select distinct ST2.ActivityID,  " & _
            '            "substring( " & _
            '                "( " & _
            '                    "Select '; '+ST1.Area+': '+ST1.Searchy  AS [text()] " & _
            '                    "From #TempFound ST1 " & _
            '                    "Where ST1.ActivityID = ST2.ActivityID " & _
            '                    "and ST1.Flagged = 1 and ST2.Flagged = 1 " & _
            '                    "ORDER BY ST1.Area " & _
            '                    "For XML PATH ('') " & _
            '                "), 2, 1000) [SearchResult] " & _
            '        "From #TempFound ST2 " & _
            '        "where Flagged = 1 ) z on z.ActivityID = zq.ActivityID"

        End If

        Dim b As Integer = 0
        For Each checkb As ListItem In cblARBalancedRows.Items
            If checkb.Selected And checkb.Value = "Bank" Then
                b += 1
                Balanced += "(isnull(BankVariance,0) <> 0) or "
            ElseIf checkb.Selected And checkb.Value = "STAR" Then
                b += 1
                Balanced += "(isnull(STARVariance,0) <> 0) or "
            ElseIf checkb.Selected Then
                b += 1
                Balanced += "(isnull(STARVariance,0) = 0 and isnull(BankVariance,0) = 0) or "
            End If
        Next

        If b = 0 Then
            Balanced = " and 1 = 2"
        Else
            Balanced = Left(Balanced, Len(Balanced) - 4) & ")"
        End If


        'If ddlARAssignedUser.SelectedValue = "Unassigned" Then
        '    Balanced += " and (AssignedUser is null) "
        'Else
        If ddlARAssignedUser.SelectedValue = " - View All - " Then
        Else
            Balanced += " and (AssignedUser = '" & Replace(ddlARAssignedUser.SelectedValue, "'", "''") & "')"
        End If

        'End If

        DSFilter.Text = " CashCategory in ('' "
        For Each checkb As ListItem In cblARCategory.Items
            If checkb.Selected Then
                DSFilter.Text += ", '" & Replace(checkb.Text, "'", "''") & "' "
            End If
        Next


        DSFilter.Text += ") and DetailStatus in ('' "
        For Each checkb As ListItem In cblARCategoryStatus.Items
            If checkb.Selected Then
                DSFilter.Text += ", '" & Replace(checkb.Text, "'", "''") & "' "
            End If
        Next
        DSFilter.Text += ") "

        'Balanced += ") and (case when Research = 1 then 'Research' else CategoryStatus end) in (''"
        'For Each checkb As ListItem In cblARCategoryStatus.Items
        '    'If checkb.Value = "Research" And checkb.Selected Then
        '    '    ResearchQ = " and Research = 1 "
        '    'ElseIf checkb.Value = "Research" Then
        '    '    ResearchQ = " and isnull(Research, 0) = 0 "
        '    If checkb.Selected Then
        '        Balanced += ", '" & Replace(checkb.Text, "'", "''") & "' "
        '    End If
        'Next
        DSFilter.Text += " and isnull(Facility, 'Unassigned') in (''"
        For Each checkb As ListItem In cblARFacility.Items
            If checkb.Selected Then
                DSFilter.Text += ", '" & Replace(checkb.Value, "'", "''") & "' "
            End If
        Next
        DSFilter.Text += ") "

        '8/28/2019 CRW switched from text to value because Gwinnett

        Dim s As String = "declare @SearchDate date = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
            " If OBJECT_ID('tempdb.dbo.#AR_Temp', 'U') IS NOT NULL  " & _
            "  DROP TABLE #AR_Temp; " & _
            "select ActivityID, FirstActivityDate, FinalActivityDate, DepositDate " & _
        "into #AR_Temp from DWH.AR.ActivityReport ar  with (nolock) where @SearchDate between FirstActivityDate and isnull(FinalActivityDate, '12/31/9999') and Active = 1  " & _
   SearchString & _
            " select zq.* " & _
", UnCom.Comment as TextboxComments " & _
", case when len(UnCom.Comment) > 75 then 'True' else 'False' end as TextCommentFlag " & _
", case when len(UnCom.Comment) > 75 then left(UnCom.Comment, 75) else UnCom.Comment end as TextboxCommentsMini " & _
" from ( " & _
"select ar.FirstActivityDate, ar.FinalActivityDate, isnull(ar.DepositDate, ar.FirstActivityDate) as SortDepositDate, " & _
"  convert(varchar, isnull(ar.DepositDate, ar.FirstActivityDate), 101) DepositDate, ad.* " & _
", isnull(isnull(isnull(assu.UserDropDownListName, assu.UserFullName), ass.UserAssigned), 'Unassigned') as AssignedDisplay " & _
", isnull(ass.UserAssigned, 'Unassigned') as AssignedUser   " & _
", case when @SearchDate <> (select Value from DWH.AR.ActivityReport_ExtraValues with (nolock)  where Description = 'RunDate' and Active = 1) then 2 " & _
"   when isnull(al.UserLocked, '--__NULL__--') = '" & Mimic.Text & "' " & _
" then 1 when al.UserLocked is not null then -1 else isnull(Locking, 2) end as EditIndex " & _
" , 'Locked - (' + isnull(isnull(usr.UserDropDownListName, usr.UserFullName), al.UserLocked) + ')' as RowLocked  " & _
", MiscAmt, case when MiscAmt <> 0 then 1 else 0 end as MiscChecked " & _
", case when TransferredIn is null and TransferredOut is null then 'Add Transfer'  " & _
"	else convert(varchar, isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)) end as Transfers " & _
", isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) as Transfer_Sum " & _
", CategorizedComments " & _
", case when len(CategorizedComments) > 75 then 'True' else 'False' end as CatCommentFlag " & _
", case when len(CategorizedComments) > 75 then left(CategorizedComments, 75) else CategorizedComments end as CategorizedCommentsMini " & _
", case when atch.AttachId is null then 'Attach' else 'Exists' end as Attachments " & _
", case when isnull(Carry_Forward, 0) = 0 then 0 else 1 end as Carry_ForwardFlag " & _
", case when isnull(ad.Unresolved, 0) = 0 then 0 else 1 end as UnresolvedFlag " & _
", case when ad.DetailStatus = 'Current' then  " & _
"	isnull(ad.Cash_Received, 0.00) - isnull(ad.AR_Posted, 0.00) - isnull(ad.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ad.Interest, 0.00) " & _
"		- isnull(ad.Carry_Forward, 0.00) - isnull(ad.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) " & _
"	else isnull(ad.AR_Posted, 0.00) + isnull(ad.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(ad.Interest, 0.00) " & _
"		+ isnull(ad.Carry_Forward, 0.00) /*+ isnull(ad.Unresolved, 0.00) rmv CRW*/ - isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00) " & _
"	end as STARVariance " & _
", case when ad.DetailStatus = 'Current' then  " & _
"	null " & _
"	else isnull(ad.Cash_Received, 0.00) - isnull(ad.AR_Posted, 0.00) - isnull(ad.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ad.Interest, 0.00) " & _
"		+ isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) - isnull(ad.Unresolved, 0.00)" & _
"	end as BankVariance " & _
", case when ad.DetailStatus = 'Research' then 1 else 0 end as ResearchFlag " & _
"from #AR_Temp ar  with (nolock)" & _
"left join DWH.AR.ActivityReport_Locking al with (nolock)  on ar.ActivityID = al.ActivityID and al.Active = 1 " & _
"left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users with (nolock)) usr   on usr.RN = 1 and al.UserLocked = usr.UserLogin and usr.Active = 1 " & _
"join (select ard.*, ROW_NUMBER() over (partition by a.ActivityID order by ActivityDate desc, ModifiedDate desc) RN  from  " & _
"		#AR_Temp a with (nolock) join DWH.AR.ActivityReport_Detail ard with (nolock) on a.ActivityID = ard.ActivityID where Active = 1 and convert(date, ActivityDate) <= @SearchDate) ad on ar.ActivityID = ad.ActivityID and ad.RN = 1 " & _
"left join (select m.ActivityID, sum(MiscAmt) as MiscAmt, convert(date, ActivityDate) as ActivityDate from  " & _
"		#AR_Temp a with (nolock) join DWH.AR.ActivityReport_MiscGL m with (nolock) on a.ActivityID = m.ActivityID where Active = 1 and convert(date, ActivityDate) <= @SearchDate group by m.ActivityID, convert(date, ActivityDate)) mgl on ar.ActivityID = mgl.ActivityID " & _
" and mgl.ActivityDate = ad.ActivityDate " & _
"left join (select TransferFrom, sum(Amount) TransferredOut, convert(date, TransferDate) as TFDate from " & _
"       #AR_Temp a with (nolock) join DWH.AR.ActivityReport_Transfers t with (nolock) on t.Transferfrom = a.ActivityID  " & _
"		where Active = 1 and convert(date, TransferDate) <= @SearchDate " & _
"		group by TransferFrom, convert(date, TransferDate) ) tff on tff.TransferFrom = ar.ActivityID and TFDate = ad.ActivityDate  " & _
"left join (select TransferTo, sum(Amount) TransferredIn, convert(date, TransferDate) as TFTDate from DWH.AR.ActivityReport_Transfers with (nolock)  " & _
"		where Active = 1 and convert(date, TransferDate) <= @SearchDate " & _
"		group by TransferTo, convert(date, TransferDate)) tft on tft.TransferTo = ar.ActivityID and TFTDate = ad.ActivityDate " & _
"left join (select max(AttachId) AttachId, af.ActivityId from #AR_Temp a with (nolock) join DWH.AR.ActivityReport_AttachedFiles af with (nolock) on a.ActivityID = af.ActivityID  " & _
"	where Active = 1 and convert(date, ActivityDate) <= @SearchDate group by af.ActivityID) atch on atch.ActivityId = ar.ActivityID " & _
"left join DWH.AR.ActivityReport_Assignments ass with (nolock)  on ass.ActivityID = ar.ActivityID " & _
"left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users with (nolock)) assu   on assu.rn = 1 and ass.UserAssigned = assu.UserLogin " & _
"left join (select case when count(al.ActivityID) >= MultipleLocks then 2 else 0 end as Locking  " & _
"from DWH.AR.ActivityReport_Users usr with (nolock)  " & _
"left join DWH.AR.ActivityReport_Locking al with (nolock)  on al.UserLocked = usr.UserLogin and al.Active = 1  " & _
" /* left join DWH.AR.ActivityReport ar with (nolock)  on ar.ActivityID = al.ActivityID */ " & _
"where usr.UserLogin = '" & Mimic.Text & "' and usr.Active = 1  " & _
"group by MultipleLocks) x on 1=1   " & _
"left join (	Select distinct ST2.ActivityID,  " & _
"    substring( " & _
"        ( " & _
"					Select '; ' + ST1.CommentType+': '+ST1.Comment  AS [text()] " & _
"					From DWH.AR.ActivityReport_Comments ST1 " & _
"					Where ST1.ActivityID = ST2.ActivityID " & _
"					and ST1.CommentType is not null and ST1.Active = 1 " & _
"					and convert(date, ST1.ActivityDate) <= @SearchDate " & _
"					ORDER BY ST1.ActivityID " & _
"					For XML PATH ('') " & _
"				), 3, 1000) [CategorizedComments] " & _
"		From DWH.AR.ActivityReport_Comments ST2  with (nolock)" & _
"       join DWH.AR.ActivityReport ar2 with (nolock) on ST2.ActivityID = ar2.ActivityID " & _
"		where ST2.CommentType is not null and ST2.Active = 1 " & _
"           and @SearchDate between FirstActivityDate and isnull(FinalActivityDate, '12/31/9999') and ar2.Active = 1 " & _
"			and convert(date, ST2.ActivityDate) <= @SearchDate) ConComments on ConComments.ActivityId = ar.ActivityID " & _
        "where not exists (select * from DWH.AR.ActivityReport_Netting net with (nolock)  where net.InitialID = ar.ActivityID and convert(date, net.ActivityDate) <= @SearchDate " & _
"	and net.ActivityDate >= ad.ActivityDate and net.Active = 1) " & _
" " & _
"	) zq " & _
"left join (select c.ActivityId, c.Comment, ROW_NUMBER() over (partition by c.ActivityID order by ActivityDate desc) RN  from " & _
"		#AR_Temp a with (nolock) join DWH.AR.ActivityReport_Comments c with (nolock) on a.ActivityID = c.ActivityID  where Active = 1 and CommentType is null and convert(date, ActivityDate) <= @SearchDate) UnCom on zq.ActivityID = UnCom.ActivityID and UnCom.RN = 1 " & _
 JoinString & " where 1=1  " & ResearchQ & Balanced & " "

        ' Splitting altered 6/19/2017 CRW per Korene; Only allow split AR Posted; everything else is together
        '            "and not exists (select * from DWH.AR.ActivityReport_Splitting split where split.InitialID = ar.ActivityID and convert(date, split.ActivityDate) <= @SearchDate " & _
        '"	and split.ActivityDate >= ad.ActivityDate and split.Active = 1) " & _

        'Dim s As String = "select *,  convert(varchar, isnull(STARVariance,0)  - (case when CategoryStatus = 'Current' then 1 else -1 end)*isnull(NumericCarry_amt,0) " & _
        '        " - (case when CategoryStatus = 'Current' then 1 else -1 end)*isnull(NumericUnresolved_amt,0) ) as DisplaySTARVariance, " & _
        '        " isnull(STARVariance,0)  - (case when CategoryStatus = 'Current' then 1 else -1 end)*isnull(NumericCarry_amt,0) " & _
        '        " - (case when CategoryStatus = 'Current' then 1 else -1 end)*isnull(NumericUnresolved_amt,0)  as SortSTARVariance  from (select ad.Row_ID, ad.Detail_ID, Base_ID " & _
        '    ", Type, convert(varchar, WF_Cash_Received) WF_Cash_Received, WF_Cash_Received as NumericWF_Cash_Received, Batch_Number, STAR_Batch_Number, No_Patients, " & _
        '    "convert(varchar, AR_Posted) AR_Posted, AR_Posted as NumericAR_Posted, " & _
        '    " convert(varchar, Resolve) Resolve, Resolve as NumericResolve, convert(varchar, Misc_Posted) Misc_Posted, Misc_Posted as NumericMisc_Posted, " & _
        '    " convert(varchar, Interest) Interest, Interest as NumericInterest, " & _
        '    " convert(varchar, isnull(Carry_Forward, 0)) as Carry_Forward, Comments, Misc_GL_Acct_Nos, CONVERT(VARCHAR(10), DepositDate, 101) DepositDate, Facility, CashCategory, CategoryStatus " & _
        '    ", case when isnull(u.UserLogin, '--__NULL__--') = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
        '    "' then 1 when u.UserLogin is not null then -1 else isnull(x.Locking, 2) end as EditIndex, 'Locked - (' + u.UserDisplayName + ')' as RowLocked " & _
        '    ", convert(varchar, isnull(TransferFrom, 0) + isnull(TransferTo, 0)) as Transfer_Sum, isnull(TransferFrom, 0) + isnull(TransferTo, 0) as NumericTransfer_Sum " & _
        '    ", case when TransferFrom is null and TransferTo is null then 'Add Transfer'  " & _
        '    "	else convert(varchar, isnull(TransferFrom, 0) + isnull(TransferTo, 0)) end   " & _
        '    "as Transfers " & _
        '    ", isnull(isnull(au.UserDisplayName, ad.AssignedUser), 'Unassigned') as AssignedDisplay " & _
        '    ", isnull(ad.AssignedUser, 'Unassigned') as AssignedUser " & _
        '    ", convert(varchar, case when ad.Carry_Forward = 1 and ad.CategoryStatus = 'Current'  " & _
        '    "	then isnull(WF_Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0)  " & _
        '    "	when ad.Carry_Forward = 1 	 " & _
        '    "		then - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0) 	 " & _
        '    "		else null end) as Carry_amt " & _
        '    ", case when ad.Carry_Forward = 1 and ad.CategoryStatus = 'Current'  " & _
        '    "	then isnull(WF_Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0)  " & _
        '    "	when ad.Carry_Forward = 1 	 " & _
        '    "		then - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0) 	 " & _
        '    "		else null end as NumericCarry_amt " & _
        '    ", convert(varchar, case when ad.Unresolved = 1 and ad.CategoryStatus = 'Current'  " & _
        '    "	then isnull(WF_Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0)  " & _
        '    "	when ad.Unresolved = 1 	 " & _
        '    "		then - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0) 	 " & _
        '    "		else null end) as Unresolved_amt " & _
        '    ", case when ad.Unresolved = 1 and ad.CategoryStatus = 'Current'  " & _
        '    "	then isnull(WF_Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0)  " & _
        '    "	when ad.Unresolved = 1 	 " & _
        '    "		then - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0) 	 " & _
        '    "		else null end as NumericUnresolved_amt " & _
        '    ", case when ad.CategoryStatus = 'Current' then null " & _
        '    "	else  isnull(WF_Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0) end as BankVariance " & _
        '    ", case when ad.CategoryStatus = 'Current' then isnull(WF_Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0)  " & _
        '    "		- isnull(TransferFrom, 0) - isnull(TransferTo, 0)				" & _
        '    "	else  isnull(AR_Posted, 0) + isnull(Misc_Posted, 0) + isnull(Interest, 0) " & _
        '    "		+ isnull(TransferFrom, 0) + isnull(TransferTo, 0) end as STARVariance, isnull(ad.Unresolved,0) as Unresolved " & _
        '    ", case when af.Detail_ID is null then 'Attach' else 'Exists' end as Attachments, isnull(ad.Research, 0) as Research, ad.Misc_GL_Text " & _
        '    "from DWH.AR.Activity_Detail ad " & _
        '    "left join (select max(FileID) FileID, Detail_ID from DWH.AR.Activity_Detail_AttachedFiles group by Detail_ID) af on ad.Detail_ID  = af.Detail_ID " & _
        '    "left join DWH.AR.Activity_Users u on ad.Locked = u.UserLogin  " & _
        '    "left join (select case when count(ad.Locked) >= MultipleLocks then 2 else 0 end as Locking  " & _
        '    "from DWH.AR.Activity_Users au " & _
        '    "left join DWH.AR.Activity_Detail ad on ad.Locked = au.UserLogin " & _
        '    "where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
        '    "group by MultipleLocks) x on 1=1 " & _
        '    "left join DWH.AR.Activity_Users au on ad.AssignedUser = au.UserLogin  " & _
        '    "left join ( " & _
        '    "select tf.Detail_ID as TFID, sum(Amount) as TransferFrom " & _
        '    "from DWH.AR.Activity_Transfers at " & _
        '    "join DWH.AR.Activity_Detail tf on (at.TransferFrom = tf.Detail_ID or (at.IntermediateRow = tf.Detail_ID and isnull(IntermediateActive, 0)= 1)) " & _
        '    "join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID " & _
        '    "where at.Active = 1 and tf.Active = 1 and tt.Active = 1 " & _
        '    "group by tf.Detail_ID ) tf on tf.TFID = ad.Detail_ID " & _
        '    "left join ( " & _
        '    "select tt.Detail_ID as TTID, -sum(Amount) as TransferTo " & _
        '    "from DWH.AR.Activity_Transfers at " & _
        '    "join DWH.AR.Activity_Detail tf on at.TransferFrom = tf.Detail_ID " & _
        '    "join DWH.AR.Activity_Detail tt on (at.TransferTo = tt.Detail_ID or (at.IntermediateRow = tt.Detail_ID and isnull(IntermediateActive, 0) = 1)) " & _
        '    "where at.Active = 1 and tf.Active = 1 and tt.Active = 1 " & _
        '    "group by tt.Detail_ID ) tt on tt.TTID = ad.Detail_ID " & _
        '    "where ad.Active = 1 and not exists (select * from DWH.AR.Activity_Netting ne where ne.InitialID = ad.Row_ID  ) and " & _
        '    "ad.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' ) x where 1 = 1 " & ResearchQ & Balanced & ") "

        Session("ARDetailView") = GetData(s).DefaultView
        'Return GetData(s).DefaultView
    End Sub

    Sub BalanceCheck()

        Try


            'ARDetailView = GetData(s).DefaultView
            'gv_AR_MainData.DataSource = ARDetailView()
            'gv_AR_MainData.DataBind()


            Dim BalCheck As String = "declare @SearchDate date = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
                "   If OBJECT_ID('tempdb.dbo.#AR_Temp', 'U') IS NOT NULL  " & _
                "      DROP TABLE #AR_Temp;  " & _
            " select ActivityID, FirstActivityDate, FinalActivityDate, DepositDate  " & _
            "   into #AR_Temp from DWH.AR.ActivityReport ar where @SearchDate between FirstActivityDate and isnull(FinalActivityDate, '12/31/9999') and Active = 1 " & _
                    "select isnull(count(*),0) from ( " & _
            "select ad.ModifiedBy as Modded, ar.FirstActivityDate, ar.FinalActivityDate, convert(varchar, ar.DepositDate, 101) DepositDate, ad.* " & _
            ", isnull(isnull(isnull(assu.UserDropDownListName, assu.UserFullName), ass.UserAssigned), 'Unassigned') as AssignedDisplay " & _
            ", isnull(ass.UserAssigned, 'Unassigned') as AssignedUser   " & _
            ", case when @SearchDate <> (select Value from DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and Active = 1) then 2 " & _
            "   when isnull(al.UserLocked, '--__NULL__--') = '" & Mimic.Text & "' " & _
            " then 1 when al.UserLocked is not null then -1 else isnull(Locking, 2) end as EditIndex " & _
            " , 'Locked - (' + isnull(isnull(usr.UserDropDownListName, usr.UserFullName), al.UserLocked) + ')' as RowLocked  " & _
            ", MiscAmt, case when MiscAmt <> 0 then 1 else 0 end as MiscChecked " & _
            ", case when TransferredIn is null and TransferredOut is null then 'Add Transfer'  " & _
            "	else convert(varchar, isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)) end as Transfers " & _
            ", isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) as Transfer_Sum " & _
            ", case when atch.AttachId is null then 'Attach' else 'Exists' end as Attachments " & _
            ", case when isnull(Carry_Forward, 0) = 0 then 0 else 1 end as Carry_ForwardFlag " & _
            ", case when isnull(ad.Unresolved, 0) = 0 then 0 else 1 end as UnresolvedFlag " & _
            ", case when ad.DetailStatus = 'Current' then  " & _
            "	isnull(ad.Cash_Received, 0.00) - isnull(ad.AR_Posted, 0.00) - isnull(ad.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ad.Interest, 0.00) " & _
            "		- isnull(ad.Carry_Forward, 0.00) - isnull(ad.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) " & _
            "	else isnull(ad.AR_Posted, 0.00) + isnull(ad.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(ad.Interest, 0.00) " & _
            "		+ isnull(ad.Carry_Forward, 0.00) /*+ isnull(ad.Unresolved, 0.00) rmv CRW*/ - isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00) " & _
            "	end as STARVariance " & _
            ", case when ad.DetailStatus = 'Current' then  " & _
            "	null " & _
            "	else isnull(ad.Cash_Received, 0.00) - isnull(ad.AR_Posted, 0.00) - isnull(ad.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ad.Interest, 0.00) " & _
            "		+ isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) - isnull(ad.Unresolved, 0.00)" & _
            "	end as BankVariance " & _
            ", case when ad.DetailStatus = 'Research' then 1 else 0 end as ResearchFlag " & _
            ", case when isnull(ad.AR_Posted, 0) <> 0 then 1 " & _
            "	   when isnull(ad.Resolve, 0) <> 0 then 1 " & _
            "	   when isnull(MiscAmt, 0) <> 0 then 1 " & _
            "	   when isnull(ad.Interest, 0) <> 0 then 1 " & _
            "	   when isnull(ad.Carry_Forward, 0) <> 0 then 1 " & _
            "	   when isnull(TransferredIn, 0) <> 0 then 1 " & _
            "	   when isnull(TransferredOut, 0) <> 0 then 1 " & _
            "	   when isnull(ad.Unresolved, 0) <> 0 then 1 " & _
            "		else 0 end as Touched " & _
            "from #AR_Temp ar with (nolock) " & _
            "left join DWH.AR.ActivityReport_Locking al with (nolock)  on ar.ActivityID = al.ActivityID and al.Active = 1 " & _
            "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users with (nolock)) usr   on usr.rn = 1 and al.UserLocked = usr.UserLogin and usr.Active = 1 " & _
            "join (select ard.*, ROW_NUMBER() over (partition by a.ActivityID order by ActivityDate desc, ModifiedDate desc) RN  from  " & _
            "		#AR_Temp a " & _
            "		join DWH.AR.ActivityReport_Detail ard with (nolock) on a.ActivityID = ard.ActivityID " & _
            "		 where Active = 1 and convert(date, ActivityDate) <= @SearchDate) ad on ar.ActivityID = ad.ActivityID and ad.RN = 1  " & _
            "left join (select ActivityID, sum(MiscAmt) as MiscAmt, convert(Date, ActivityDate) as MiscDate from  " & _
            "		DWH.AR.ActivityReport_MiscGL with (nolock)  where Active = 1 and convert(date, ActivityDate) <= @SearchDate group by ActivityID, convert(Date, ActivityDate)) mgl on ar.ActivityID = mgl.ActivityID " & _
            " and mgl.MiscDate = ad.ActivityDate " & _
            "left join (select TransferFrom, sum(Amount) TransferredOut, convert(Date, TransferDate) as TFDate from DWH.AR.ActivityReport_Transfers with (nolock)  " & _
            "		where Active = 1 and convert(date, TransferDate) <= @SearchDate " & _
            "		group by TransferFrom, convert(Date, TransferDate) ) tff on tff.TransferFrom = ar.ActivityID and tff.TFDate = ad.ActivityDate " & _
            "left join (select TransferTo, sum(Amount) TransferredIn, convert(date, TransferDate) as TFTDate from DWH.AR.ActivityReport_Transfers with (nolock)  " & _
            "		where Active = 1 and convert(date, TransferDate) <= @SearchDate " & _
            "		group by TransferTo, convert(date, TransferDate)) tft on tft.TransferTo = ar.ActivityID and TFTDate = ad.ActivityDate " & _
            "left join (select max(AttachId) AttachId, ActivityId from DWH.AR.ActivityReport_AttachedFiles  with (nolock)  " & _
            "	where Active = 1 and convert(date, ActivityDate) <= @SearchDate group by ActivityID) atch on atch.ActivityId = ar.ActivityID " & _
            "left join DWH.AR.ActivityReport_Assignments ass with (nolock)  on ass.ActivityID = ar.ActivityID " & _
            "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users with (nolock)) assu   on assu.rn = 1 and ass.UserAssigned = assu.UserLogin " & _
            "left join (select case when count(al.ActivityID) >= MultipleLocks then 2 else 0 end as Locking  " & _
            "from DWH.AR.ActivityReport_Users usr with (nolock)  " & _
            "join DWH.AR.ActivityReport_Locking al with (nolock)  on al.UserLocked = usr.UserLogin and al.Active = 1  " & _
            " /*left join DWH.AR.ActivityReport ar with (nolock)  on ar.ActivityID = al.ActivityID */  " & _
            "where usr.UserLogin = '" & Mimic.Text & "' and usr.Active = 1   " & _
            "group by MultipleLocks) x on 1=1   " & _
            "where not exists (select * from DWH.AR.ActivityReport_Netting net with (nolock)  where net.InitialID = ar.ActivityID and convert(date, net.ActivityDate) <= @SearchDate " & _
            "	and net.ActivityDate >= ad.ActivityDate and net.Active = 1) " & _
            "	) zq where 1=1 and Touched = 1 and CashCategory <> 'Questionable Item'   and Modded = '" & Mimic.Text & _
            "' and ((DetailStatus = 'Current' and isnull(STARVariance, 0) <> 0) or (DetailStatus <> 'Current' and isnull(BankVariance, 0) <> 0  and (isnull(Carry_Forward, 0) <> 0 or isnull(Unresolved, 0) <> 0)) ) "

            Dim x As Integer = GetScalar(BalCheck)

            If x > 0 Then
                lblOutofBalance.Visible = True
                tblOutOfBalance.Visible = True
                If x = 1 Then
                    lblOutofBalance.Text = "&nbsp;" & CStr(x) & " of your rows is out of balance."
                Else
                    lblOutofBalance.Text = "&nbsp;" & CStr(x) & " of your rows are out of balance."
                    End If

            Else
                tblOutOfBalance.Visible = False
                lblOutofBalance.Visible = False
                End If

        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Shared Function GetData(query As String) As DataTable

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandTimeout = 6000
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

                cmd.CommandTimeout = 6000
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
    Private Shared Function GetDate(query As String) As Date

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As Date

                dt = CDate(cmd.ExecuteScalar)

                Return dt

            End Using

        End Using


    End Function
    Private Shared Function GetDecimal(query As String) As Integer

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As Decimal

                dt = CDec(cmd.ExecuteScalar)

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

                dt = CStr(cmd.ExecuteScalar)

                Return dt

            End Using

        End Using


    End Function

#Region "ARData"
    Private Sub gv_AR_MainData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gv_AR_MainData.PageIndexChanging
        Try

            If CheckBeforePageChange(e.NewPageIndex) = 0 Then


                Dim se As String = gv_AR_MainData.SortExpression
                Dim x As DataView = Session("ARDetailView") ' ARDetailView()
                x.RowFilter = DSFilter.Text
                Dim y As String = ARDetailmap.Text
                Dim z As String = ARDetaildir.Text

                Try
                    If CInt(ARDetaildir.Text) = 1 Then
                        x.Sort = y + " " + "asc"
                    Else
                        x.Sort = y + " " + "desc"
                    End If
                Catch ex As Exception
                    x.Sort = se
                End Try

                gv_AR_MainData.PageIndex = e.NewPageIndex
                gv_AR_MainData.DataSource = x
                gv_AR_MainData.DataBind()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub gv_AR_MainData_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gv_AR_MainData.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = Session("ARDetailView") ' ARDetailView()
            dv.RowFilter = DSFilter.Text
            sorts = e.SortExpression

            If e.SortExpression = ARDetailmap.Text Then

                If CInt(ARDetaildir.Text) = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    ARDetaildir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    ARDetaildir.Text = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                ARDetaildir.Text = 1
                ARDetailmap.Text = e.SortExpression
            End If

            gv_AR_MainData.DataSource = dv
            gv_AR_MainData.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Function CheckBeforePageChange(e As Integer)
        Dim UpdateSQL As String = ""
        Dim cnt As Integer = 0
        For Each row As GridViewRow In gv_AR_MainData.Rows
            If row.RowType = DataControlRowType.DataRow Then

                'Dim Numerator, Denominator, OldNum, OldDenom As String

                'If Numerator <> OldNum Or Denominator <> OldDenom Then
                '    cnt += 1
                'End If

            End If
        Next

        If cnt > 0 Then
            explanationlabel.Text = cnt.ToString & " rows of data have been entered; if you change the page, these changes will be lost."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            Return 1
        Else
            Return 0
        End If

    End Function
    Private Sub gv_AR_MainData_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gv_AR_MainData.RowCreated
        Try
            If CInt(Research.Text) = 0 Then
                e.Row.Cells(16).CssClass = "hidden"
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub gv_AR_MainData_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gv_AR_MainData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(1).CssClass = "locked"
            'e.Row.Cells(2).CssClass = "locked"
            '' There isn't a "locked" css class.  I think this can probably be removed
            Try

                Dim attch As Integer = 0
                Dim lbl As Label = e.Row.FindControl("lblARDetail_ID")
                Try
                    Dim HistoryBtn As LinkButton = e.Row.FindControl("lbRowHistory")
                    HistoryBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_History/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail History', 'height=700, scrollbars, resizable');"

                    If e.Row.DataItem("Attachments") = "Attach" Then
                        attch = 1
                    End If
                Catch ex As Exception
                    LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "HistoryBtn" & Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try


                If e.Row.DataItem("EditIndex") = "1" Then
                    Try
                        If attch = 1 Then
                            Dim AttachBtn As LinkButton = e.Row.FindControl("btnRowAttachment")
                            AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                            AttachBtn.Visible = True
                        Else
                            Dim AttachBtn As ImageButton = e.Row.FindControl("imggreen")
                            AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                            AttachBtn.Visible = True
                        End If

                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                        e.Row.FindControl("btnEditDetailRow").Visible = False
                        e.Row.FindControl("lblRowLocked").Visible = False
                        e.Row.FindControl("lbUpdateDetailRow").Visible = True
                        e.Row.FindControl("btnSplitDetailRow").Visible = True
                        e.Row.FindControl("btnCancelDetailRow").Visible = True

                        'e.Row.FindControl("lblARBatchNo").Visible = False
                        'e.Row.FindControl("txtARBatchNo").Visible = True

                        Dim lblSTAR As Label = e.Row.FindControl("lblARSTARBatchNo")

                        If Left(lblSTAR.Text, 8) = "Split - " Then
                        Else
                            e.Row.FindControl("lblARSTARBatchNo").Visible = False
                            e.Row.FindControl("txtARSTARBatchNo").Visible = True
                            e.Row.FindControl("lblARARPosted").Visible = False
                            e.Row.FindControl("txtARARPosted").Visible = True
                        End If


                        'e.Row.FindControl("lblARNoPatients").Visible = False
                        'e.Row.FindControl("txtARNoPatients").Visible = True
                        '9/20/2021 CRW

                        'e.Row.FindControl("lblARType").Visible = False
                        'e.Row.FindControl("txtARType").Visible = True

                    Catch ex As Exception
                        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "Part 1" & Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    End Try

                  

                        Dim lblARCategory As Label = e.Row.FindControl("lblARCategory")
                        Dim ddlARRowType As DropDownList = e.Row.FindControl("ddlARType")

                        If lblARCategory.Text = "Cash Journal" Then
                            Dim lblARType As Label = e.Row.FindControl("lblARType")
                            lblARType.Visible = False
                            ddlARRowType.Visible = True

                            ' Flipping Tables 5/22/2017 CRW 
                            'Dim ARTypes As String = "select isnull(SelectionDisplay, SelectionValue) as DisplaySelection, SelectionValue, 1 as ord from DWH.AR.Activity_Selections " & _
                            '"where SelectionType = 'CashJournalType' and (Active = 1 or SelectionType = '" & Trim(Replace(lblARType.Text, "'", "''")) & "') " & _
                            '"  union select distinct '(Select Type)' , 'Unassigned', 0 " & _
                            '"order by 3, 2 "

                            ' Fixed bug 9/24/2019 CRW
                            'Dim ARTypes As String = "select isnull(isnull(DropDownListDisplay, FullDisplay), Type) as DisplaySelection, Type, 1 as ord from DWH.AR.ActivityReport_CashCategory_Type_LU " & _
                            '"where CashCategory = '" & lblARCategory.Text & "' and (Active = 1 or Type = '" & Trim(Replace(lblARType.Text, "'", "''")) & "') " & _
                            '"  union select distinct '(Select Type)' , 'Unassigned', 0 " & _
                            '" union select '" & Replace(lblARType.Text, "'", "''") & "', '" & Replace(lblARType.Text, "'", "''") & _
                            '    "', 0 where not exists (select * from DWH.AR.ActivityReport_CashCategory_Type_LU where CashCategory = 'Cash Journal' and Type = '" & Trim(Replace(lblARType.Text, "'", "''")) & "') " & _
                            '"order by 3, 2 "

                        Dim ARTypes As String = "select isnull(isnull(DropDownListDisplay, FullDisplay), Type) as DisplaySelection, Type, 1 as ord from DWH.AR.ActivityReport_CashCategory_Type_LU with (nolock) " & _
                                "where CashCategory = '" & lblARCategory.Text & "' and Active = 1  " & _
                                "  union select distinct '(Select Type)' , 'Unassigned', 0 " & _
                                " union select '" & Replace(lblARType.Text, "'", "''") & "', '" & Replace(lblARType.Text, "'", "''") & _
                                    "', 0 where not exists (select * from DWH.AR.ActivityReport_CashCategory_Type_LU with (nolock) where Active = 1 and CashCategory = 'Cash Journal' and Type = '" & _
                                    Trim(Replace(lblARType.Text, "'", "''")) & "') " & _
                                "order by 3, 2 "

                            ddlARRowType.DataSource = GetData(ARTypes)
                            ddlARRowType.DataTextField = "DisplaySelection"
                            ddlARRowType.DataValueField = "Type"
                            ddlARRowType.DataBind()
                            Try
                                ddlARRowType.SelectedValue = lblARType.Text
                            Catch ex As Exception

                            End Try

                        End If

                        'Dim lblGLNos As Label = e.Row.FindControl("lblARMisc_GL_Acct_NosDDL")
                        'Dim ddlARRowMisc_GL_Acct_Nos As DropDownList = e.Row.FindControl("ddlARRowMisc_GL_Acct_Nos")
                        'Dim txtARMisc_GL_Acct_Nos As TextBox = e.Row.FindControl("txtARMisc_GL_Acct_Nos")

                        'Dim GLNos As String = "select isnull(DropDownDisplay, FullDisplay) as DisplaySelection, Misc_Identity, 1 as ord from DWH.AR.ActivityReport_MiscGLCodes_LU " & _
                        '"where (Active = 1 or Misc_Identity = '" & Trim(Replace(lblGLNos.Text, "'", "''")) & "' )" & _
                        '"  union select distinct '(Select GL Nos)' ,-1, 0  " & _
                        '"order by 3, 2 "

                        'ddlARRowMisc_GL_Acct_Nos.DataSource = GetData(GLNos)
                        'ddlARRowMisc_GL_Acct_Nos.DataTextField = "DisplaySelection"
                        'ddlARRowMisc_GL_Acct_Nos.DataValueField = "Misc_Identity"
                        'ddlARRowMisc_GL_Acct_Nos.DataBind()
                        'Try
                        '    ddlARRowType.SelectedValue = lblGLNos.Text
                        'Catch ex As Exception

                        'End Try

                        'If lblGLNos.Text = "11" Then
                        '    txtARMisc_GL_Acct_Nos.Visible = True
                        'End If

                        Dim ddlARCategory As DropDownList = e.Row.FindControl("ddlARCategory")
                    Dim CC As String = "select distinct CashCategory from DWH.AR.ActivityReport_CashCategory with (nolock) " & _
                            "where Active = 1 and 'Questionable Item' = '" & Trim(Replace(lblARCategory.Text, "'", "''")) & "' " & _
                            "  union select 'Questionable Item' " & _
                            "  union select '" & Trim(Replace(lblARCategory.Text, "'", "''")) & "' " & _
                            "order by 1 "

                        ddlARCategory.DataSource = GetData(CC)
                        ddlARCategory.DataTextField = "CashCategory"
                        ddlARCategory.DataValueField = "CashCategory"
                        ddlARCategory.DataBind()

                        Try
                            ddlARCategory.SelectedValue = lblARCategory.Text

                        Catch ex As Exception
                        End Try

                        'Moved to Current only
                        'If Admin > 0 Then
                        '    ddlARCategory.Visible = True
                        '    lblARCategory.Visible = False
                        'End If

                        'e.Row.FindControl("lblARWF_Cash_Received").Visible = False
                        'e.Row.FindControl("txtARWF_Cash_Received").Visible = True

                        'e.Row.FindControl("lblARARPosted").Visible = False
                        'e.Row.FindControl("txtARMiscPosted").Visible = True

                        Dim cbMisc As CheckBox = e.Row.FindControl("cbMiscPosted")
                        cbMisc.Enabled = True

                        e.Row.FindControl("lblARInterest").Visible = False
                        e.Row.FindControl("txtARInterest").Visible = True


                        Dim cbUnresolved As CheckBox = e.Row.FindControl("cbARUnresolved")
                        cbUnresolved.Enabled = True

                        Dim cbARCarryForward As CheckBox = e.Row.FindControl("cbARCarryForward")
                        Dim lblARDetailStatus As Label = e.Row.FindControl("lblARCategoryStatus")
                        If lblARDetailStatus.Text = "Current" Then
                            If CInt(Admin.Text) > 0 Then
                                ddlARCategory.Visible = True
                                lblARCategory.Visible = False
                                cbARCarryForward.Enabled = True
                            Else
                                cbARCarryForward.Enabled = False
                            End If

                        Else
                            cbARCarryForward.Visible = False
                        End If


                        Dim cbResearch As CheckBox = e.Row.FindControl("cbARResearch")
                        cbResearch.Enabled = True

                        e.Row.FindControl("lblARComments").Visible = False
                        e.Row.FindControl("lbARCommentsPopup").Visible = False
                        e.Row.FindControl("txtARComments").Visible = True

                        'e.Row.FindControl("lblARMisc_GL_Acct_Nos").Visible = False
                        'e.Row.FindControl("ddlARRowMisc_GL_Acct_Nos").Visible = True

                        e.Row.FindControl("lblTransfers").Visible = False
                        e.Row.FindControl("btnRowTransfers").Visible = True

                    

                ElseIf e.Row.DataItem("EditIndex") = "0" Then

                    If attch = 0 Then
                        If e.Row.RowIndex Mod 2 = 0 Then
                            Dim AttachBtn As ImageButton = e.Row.FindControl("imgblue")
                            AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                            AttachBtn.Visible = True
                        Else
                            Dim AttachBtn As ImageButton = e.Row.FindControl("imgwhite")
                            AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                            AttachBtn.Visible = True
                        End If
                    End If
                    e.Row.FindControl("btnEditDetailRow").Visible = True
                    e.Row.FindControl("lblRowLocked").Visible = False
                    e.Row.FindControl("lbUpdateDetailRow").Visible = False
                    e.Row.FindControl("btnSplitDetailRow").Visible = False
                    e.Row.FindControl("btnCancelDetailRow").Visible = False


                ElseIf e.Row.DataItem("EditIndex") = "2" Then

                    If attch = 0 Then
                        If e.Row.RowIndex Mod 2 = 0 Then
                            Dim AttachBtn As ImageButton = e.Row.FindControl("imgblue")
                            AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                            AttachBtn.Visible = True
                        Else
                            Dim AttachBtn As ImageButton = e.Row.FindControl("imgwhite")
                            AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                            AttachBtn.Visible = True
                        End If
                    End If
                    e.Row.FindControl("btnEditDetailRow").Visible = False
                    e.Row.FindControl("lblRowLocked").Visible = False
                    e.Row.FindControl("lbUpdateDetailRow").Visible = False
                    e.Row.FindControl("btnSplitDetailRow").Visible = False
                    e.Row.FindControl("btnCancelDetailRow").Visible = False

                Else

                    If attch = 0 Then
                        Dim AttachBtn As ImageButton = e.Row.FindControl("imggray")
                        AttachBtn.OnClientClick = "javascript:window.open(""" & "Activity_Report_Attachments/?Detail_ID=" & lbl.Text.ToString & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                        AttachBtn.Visible = True
                    End If
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3")
                    e.Row.FindControl("btnEditDetailRow").Visible = False
                    e.Row.FindControl("lblRowLocked").Visible = True
                    e.Row.FindControl("lbUpdateDetailRow").Visible = False
                    e.Row.FindControl("btnSplitDetailRow").Visible = False
                    e.Row.FindControl("btnCancelDetailRow").Visible = False

                End If

                Dim lblARAssignedUser As Label = e.Row.FindControl("lblARAssignedUser")
                Dim ddlARRowAssignedUser As DropDownList = e.Row.FindControl("ddlARRowAssignedUser")

                '        Dim s As String = "select UserLogin, isnull(isnull(UserDropDownListName, left(UserFullName, 12)), UserLogin) as UserDisplayName, 1 as ord from DWH.AR.ActivityReport_Users " & _
                '"where Active = 1 and Assignable = 1 or UserLogin = '" & Trim(Replace(lblARAssignedUser.Text, "'", "''")) & "' " & _
                '"  union select distinct 'Unassigned' , 'Unassigned', 0 union " & _
                '"select distinct isnull(AssignedUser, 'Unassigned') ,isnull(isnull(au.UserDisplayName, ad.AssignedUser), 'Unassigned'), isnull(au.Active, 0) " & _
                '"from DWH.AR.Activity_Detail ad " & _
                '"left join DWH.AR.Activity_Users au on ad.AssignedUser = au.UserLogin   " & _
                '"where ad.Active = 1 and ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
                '"order by 3, 2 "

                Dim s As String = "select Upper(UserLogin) as UserLogin, isnull(isnull(UserDropDownListName, left(UserFullName, 12)), UserLogin) as UserDisplayName, 1 as ord " & _
                    "from (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users with (nolock)) z " & _
                    "where z.rn = 1 and (Active = 1 and Assignable = 1 or UserLogin = '" & Trim(Replace(lblARAssignedUser.Text, "'", "''")) & "') " & _
                    "  union select distinct UPPER('Unassigned') , 'Unassigned', 0  " & _
                    "order by 3, 2 "

                'Try
                '    Dim s As New DataView
                '    Try
                '        s = Session("UserDropDown")
                '    Catch ex As Exception
                '        Dim z As String = "select Upper(UserLogin) as UserLogin, isnull(isnull(UserDropDownListName, left(UserFullName, 10)), UserLogin) as UserDisplayName, 1 as ord, Assignable from DWH.AR.ActivityReport_Users " & _
                '      "where Active = 1 " & _
                '      "  union select distinct ' - View All - ' , '- View All -', -1, 0 as Assignable union select distinct Upper('Unassigned') , '- Unassigned -', 0, 1 as Assignable union " & _
                '      "select distinct Upper(isnull(UserAssigned, 'Unassigned')) ,isnull(isnull(isnull(UserDropDownListName, left(UserFullName, 10)), UserAssigned), '- Unassigned -'), case when UserAssigned is null then 0 else 1 end, 0 " & _
                '      "from  DWH.AR.ActivityReport ar " & _
                '      "join DWH.AR.ActivityReport_Assignments aa on ar.ActivityID = aa.ActivityID " & _
                '      "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) au on au.rn = 1 and aa.UserAssigned = au.UserLogin " & _
                '      "where ar.Active = 1 and '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' between ar.FirstActivityDate and isnull(ar.FinalActivityDate, '12/31/9999') " & _
                '      "and aa.userAssigned is not null and not exists (select * from DWH.AR.ActivityReport_Users x where x.Active = 1 and aa.UserAssigned = x.UserLogin) " & _
                '      "order by 3, 2 "

                '        Session("UserDropDown") = GetData(z).DefaultView
                '        s = Session("UserDropDown")
                '    End Try

                '    If s.Count() = 0 Then
                '        Dim z As String = "select Upper(UserLogin) as UserLogin, isnull(isnull(UserDropDownListName, left(UserFullName, 10)), UserLogin) as UserDisplayName, 1 as ord, Assignable from DWH.AR.ActivityReport_Users " & _
                '       "where Active = 1 " & _
                '       "  union select distinct ' - View All - ' , '- View All -', -1, 0 as Assignable union select distinct Upper('Unassigned') , '- Unassigned -', 0, 1 as Assignable union " & _
                '       "select distinct Upper(isnull(UserAssigned, 'Unassigned')) ,isnull(isnull(isnull(UserDropDownListName, left(UserFullName, 10)), UserAssigned), '- Unassigned -'), case when UserAssigned is null then 0 else 1 end, 0 " & _
                '       "from  DWH.AR.ActivityReport ar " & _
                '       "join DWH.AR.ActivityReport_Assignments aa on ar.ActivityID = aa.ActivityID " & _
                '       "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) au on au.rn = 1 and aa.UserAssigned = au.UserLogin " & _
                '       "where ar.Active = 1 and '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' between ar.FirstActivityDate and isnull(ar.FinalActivityDate, '12/31/9999') " & _
                '       "and aa.userAssigned is not null and not exists (select * from DWH.AR.ActivityReport_Users x where x.Active = 1 and aa.UserAssigned = x.UserLogin) " & _
                '       "order by 3, 2 "

                '        Session("UserDropDown") = GetData(z).DefaultView
                '        s = Session("UserDropDown")
                '    End If
                '    Try
                '        s.RowFilter = "Assignable = 1 or UserLogin = '" & e.Row.DataItem("AssignedUser").ToString.ToUpper & "'"
                '    Catch ex As Exception
                '        s.RowFilter = "Assignable = 1 "
                '    End Try


                ddlARRowAssignedUser.DataSource = GetData(s)
                ddlARRowAssignedUser.DataTextField = "UserDisplayName"
                ddlARRowAssignedUser.DataValueField = "UserLogin"
                ddlARRowAssignedUser.DataBind()

                Try
                    ddlARRowAssignedUser.SelectedValue = e.Row.DataItem("AssignedUser").ToString.ToUpper

                Catch ex As Exception

                End Try

                If CInt(Assign.Text) = 1 Then
                    ddlARRowAssignedUser.Enabled = True

                End If


      
            Catch ex As Exception
                LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "Full Segment" & Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

        End If

    End Sub
#End Region

#Region "OtherCode"


    'Private Sub SubmitGFSTO_Rows()
    '    Try

    '        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
    '        UserName = Replace(tmblbl.Text, "'", "''")

    '        Dim UpdatesSql As String = ""

    '        For i As Integer = 0 To gvGFSTO_Data.Rows.Count - 1
    '            Dim GFSTO_ID As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_ID"), Label)
    '            Dim GFSTO_Clinic_Sup As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_Clinic_Sup"), Label)
    '            Dim GFSTO_Clinic_Sup_Pres As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_Clinic_Sup_Pres"), Label)
    '            'Dim GFSTO_CS_Absence As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_CS_Absence"), Label)
    '            Dim GFSTO_Circ1 As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_Circ1"), Label)
    '            Dim GFSTO_SurgTech As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_SurgTech"), Label)
    '            'Dim GFSTO_Expeditor As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_Expeditor"), Label)
    '            'Dim GFSTO_PickTick As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_PickTick"), Label)
    '            'Dim GFSTO_CircDirect As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_CircDirect"), Label)
    '            'Dim GFSTO_SurgTechOR As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_SurgTechOR"), Label)
    '            'Dim GFSTO_CaseCart As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_CaseCart"), Label)
    '            Dim GFSTO_DelayCode As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_DelayCode"), Label)
    '            Dim GFSTO_SubDelayCode As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_SubDelayCode"), Label)
    '            Dim GFSTO_TurnDelayNotes As Label = CType(gvGFSTO_Data.Rows(i).FindControl("lblGFSTO_TurnDelayNotes"), Label)

    '            Dim NewGFSTO_Clinic_Sup As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_Clinic_Sup"), DropDownList)
    '            Dim NewGFSTO_Clinic_Sup_Pres As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_Clinic_Sup_Pres"), DropDownList)
    '            'Dim NewGFSTO_CS_Absence As TextBox = CType(gvGFSTO_Data.Rows(i).FindControl("txtGFSTO_CS_Absence"), TextBox)
    '            Dim NewGFSTO_Circ1 As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_Circ1"), DropDownList)
    '            Dim NewGFSTO_SurgTech As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_SurgTech"), DropDownList)
    '            'Dim NewGFSTO_Expeditor As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_Expeditor"), DropDownList)
    '            'Dim NewGFSTO_PickTick As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_PickTick"), DropDownList)
    '            'Dim NewGFSTO_CircDirect As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_CircDirect"), DropDownList)
    '            'Dim NewGFSTO_SurgTechOR As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddl_GFSTO_SurgTechOR"), DropDownList)
    '            'Dim NewGFSTO_CaseCart As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_CaseCart"), DropDownList)
    '            Dim NewGFSTO_DelayCode As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_DelayCode"), DropDownList)
    '            Dim NewGFSTO_SubDelayCode As DropDownList = CType(gvGFSTO_Data.Rows(i).FindControl("ddlGFSTO_SubDelayCode"), DropDownList)
    '            Dim NewGFSTO_TurnDelayNotes As TextBox = CType(gvGFSTO_Data.Rows(i).FindControl("txtGFSTO_TurnDelayNotes"), TextBox)

    '            Dim NewRow As Integer = 0
    '            If NewGFSTO_Clinic_Sup.SelectedValue <> GFSTO_Clinic_Sup.Text Then
    '                NewRow = 1
    '            ElseIf NewGFSTO_Clinic_Sup_Pres.SelectedValue <> GFSTO_Clinic_Sup_Pres.Text Then
    '                NewRow = 1
    '                'ElseIf NewGFSTO_CS_Absence.Text <> GFSTO_CS_Absence.Text Then
    '                '    NewRow = 1
    '            ElseIf NewGFSTO_Circ1.SelectedValue <> GFSTO_Circ1.Text Then
    '                NewRow = 1
    '            ElseIf NewGFSTO_SurgTech.SelectedValue <> GFSTO_SurgTech.Text Then
    '                NewRow = 1
    '                'ElseIf NewGFSTO_Expeditor.SelectedValue <> GFSTO_Expeditor.Text Then
    '                '    NewRow = 1
    '                'ElseIf NewGFSTO_PickTick.SelectedValue <> GFSTO_PickTick.Text Then
    '                '    NewRow = 1
    '                'ElseIf NewGFSTO_CircDirect.SelectedValue <> GFSTO_CircDirect.Text Then
    '                '    NewRow = 1
    '                'ElseIf NewGFSTO_SurgTechOR.SelectedValue <> GFSTO_SurgTechOR.Text Then
    '                '    NewRow = 1
    '                'ElseIf NewGFSTO_CaseCart.SelectedValue <> GFSTO_CaseCart.Text Then
    '                '    NewRow = 1
    '            ElseIf NewGFSTO_DelayCode.SelectedValue <> GFSTO_DelayCode.Text Then
    '                NewRow = 1
    '            ElseIf NewGFSTO_SubDelayCode.SelectedValue <> GFSTO_SubDelayCode.Text Then
    '                NewRow = 1
    '            ElseIf NewGFSTO_TurnDelayNotes.Text <> GFSTO_TurnDelayNotes.Text Then
    '                NewRow = 1
    '            End If

    '            If NewRow > 0 Then


    '                Dim sNewGFSTO_Clinic_Sup As String
    '                Dim sNewGFSTO_Clinic_Sup_Pres As String
    '                Dim sNewGFSTO_CS_Absence As String
    '                Dim sNewGFSTO_Circ1 As String
    '                Dim sNewGFSTO_SurgTech As String
    '                Dim sNewGFSTO_Expeditor As String
    '                'Dim sNewGFSTO_PickTick As String
    '                'Dim sNewGFSTO_CircDirect As String
    '                'Dim sNewGFSTO_SurgTechOR As String
    '                'Dim sNewGFSTO_CaseCart As String
    '                Dim sNewGFSTO_DelayCode As String
    '                Dim sNewGFSTO_SubDelayCode As String
    '                Dim sNewGFSTO_TurnDelayNotes As String

    '                If Trim(Replace(NewGFSTO_Clinic_Sup.SelectedValue.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_Clinic_Sup = "null"
    '                Else
    '                    sNewGFSTO_Clinic_Sup = "'" & Trim(Replace(NewGFSTO_Clinic_Sup.SelectedValue.ToString, "'", "''")) & "'"
    '                End If
    '                If Trim(Replace(NewGFSTO_Clinic_Sup_Pres.SelectedValue.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_Clinic_Sup_Pres = "null"
    '                Else
    '                    sNewGFSTO_Clinic_Sup_Pres = "'" & Trim(Replace(NewGFSTO_Clinic_Sup_Pres.SelectedValue.ToString, "'", "''")) & "'"
    '                End If
    '                'If Trim(Replace(NewGFSTO_CS_Absence.Text.ToString, "'", "''")) = "" Then
    '                '    sNewGFSTO_CS_Absence = "null"
    '                'Else
    '                '    sNewGFSTO_CS_Absence = "'" & Trim(Replace(NewGFSTO_CS_Absence.Text.ToString, "'", "''")) & "'"
    '                'End If
    '                If Trim(Replace(NewGFSTO_Circ1.SelectedValue.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_Circ1 = "null"
    '                Else
    '                    sNewGFSTO_Circ1 = "'" & Trim(Replace(NewGFSTO_Circ1.SelectedValue.ToString, "'", "''")) & "'"
    '                End If
    '                If Trim(Replace(NewGFSTO_SurgTech.SelectedValue.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_SurgTech = "null"
    '                Else
    '                    sNewGFSTO_SurgTech = "'" & Trim(Replace(NewGFSTO_SurgTech.SelectedValue.ToString, "'", "''")) & "'"
    '                End If
    '                'If Trim(Replace(NewGFSTO_Expeditor.SelectedValue.ToString, "'", "''")) = "" Then
    '                '    sNewGFSTO_Expeditor = "null"
    '                'Else
    '                '    sNewGFSTO_Expeditor = "'" & Trim(Replace(NewGFSTO_Expeditor.SelectedValue.ToString, "'", "''")) & "'"
    '                'End If
    '                'If Trim(Replace(NewGFSTO_PickTick.SelectedValue.ToString, "'", "''")) = "" Then
    '                '    sNewGFSTO_PickTick = "null"
    '                'Else
    '                '    sNewGFSTO_PickTick = "'" & Trim(Replace(NewGFSTO_PickTick.SelectedValue.ToString, "'", "''")) & "'"
    '                'End If
    '                'If Trim(Replace(NewGFSTO_CircDirect.SelectedValue.ToString, "'", "''")) = "" Then
    '                '    sNewGFSTO_CircDirect = "null"
    '                'Else
    '                '    sNewGFSTO_CircDirect = "'" & Trim(Replace(NewGFSTO_CircDirect.SelectedValue.ToString, "'", "''")) & "'"
    '                'End If
    '                'If Trim(Replace(NewGFSTO_SurgTechOR.SelectedValue.ToString, "'", "''")) = "" Then
    '                '    sNewGFSTO_SurgTechOR = "null"
    '                'Else
    '                '    sNewGFSTO_SurgTechOR = "'" & Trim(Replace(NewGFSTO_SurgTechOR.SelectedValue.ToString, "'", "''")) & "'"
    '                'End If
    '                'If Trim(Replace(NewGFSTO_CaseCart.SelectedValue.ToString, "'", "''")) = "" Then
    '                '    sNewGFSTO_CaseCart = "null"
    '                'Else
    '                '    sNewGFSTO_CaseCart = "'" & Trim(Replace(NewGFSTO_CaseCart.SelectedValue.ToString, "'", "''")) & "'"
    '                'End If
    '                If Trim(Replace(NewGFSTO_DelayCode.SelectedValue.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_DelayCode = "null"
    '                ElseIf Trim(Replace(NewGFSTO_DelayCode.SelectedValue.ToString, "'", "''")) = "(No Delay Code Selected)" Then
    '                    sNewGFSTO_DelayCode = "null"
    '                Else
    '                    sNewGFSTO_DelayCode = "'" & Trim(Replace(NewGFSTO_DelayCode.SelectedValue.ToString, "'", "''")) & "'"
    '                End If
    '                If Trim(Replace(NewGFSTO_SubDelayCode.SelectedValue.ToString, "'", "''")) = "(No Delay Code Selected)" Then
    '                    sNewGFSTO_SubDelayCode = "null"
    '                ElseIf Trim(Replace(NewGFSTO_SubDelayCode.SelectedValue.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_SubDelayCode = "null"
    '                Else
    '                    sNewGFSTO_SubDelayCode = "'" & Trim(Replace(NewGFSTO_SubDelayCode.SelectedValue.ToString, "'", "''")) & "'"
    '                End If
    '                If Trim(Replace(NewGFSTO_TurnDelayNotes.Text.ToString, "'", "''")) = "" Then
    '                    sNewGFSTO_TurnDelayNotes = "null"
    '                Else
    '                    sNewGFSTO_TurnDelayNotes = "'" & Trim(Replace(NewGFSTO_TurnDelayNotes.Text.ToString, "'", "''")) & "'"
    '                End If

    '                UpdatesSql += "Update a set ClinicalSuperVisor_ID = " & sNewGFSTO_Clinic_Sup & _
    '                    ", ClinicalSupervisor_Name = isnull(b.Display_Name, b.Staff_Name),  ClinicalSupervisorPresent = " & sNewGFSTO_Clinic_Sup_Pres & _
    '                    ", Circulator1_ID = " & sNewGFSTO_Circ1 & _
    '                    ", Circulator1_Name = isnull(c.Display_Name, c.Staff_Name), SurgicalTech_ID = " & sNewGFSTO_SurgTech & ", SurgicalTech_Name = isnull(d.Display_Name, d.Staff_Name) " & _
    '                    ", Delay_Code =" & sNewGFSTO_DelayCode & _
    '                    ", Sub_Delay_Code = " & sNewGFSTO_SubDelayCode & ", Turnover_Delay_Notes = " & sNewGFSTO_TurnDelayNotes & _
    '                    ", Last_UserModify_Date = getdate(), Last_UserModify = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    '                    "' from DWH.UD.GFSTO a " & _
    '                    " left join DWH.UD.GFSTO_Staff b on '" & Replace(NewGFSTO_Clinic_Sup.SelectedValue.ToString, "'", "''") & "' = b.GFSTO_Staff_ID " & _
    '                    " left join DWH.UD.GFSTO_Staff c on '" & Replace(NewGFSTO_Circ1.SelectedValue.ToString, "'", "''") & "' = c.GFSTO_Staff_ID " & _
    '                    " left join DWH.UD.GFSTO_Staff d on '" & Replace(NewGFSTO_SurgTech.SelectedValue.ToString, "'", "''") & "' = d.GFSTO_Staff_ID " & _
    '                    " where GFSTO_ID = " & Replace(GFSTO_ID.Text, "'", "''") & "; "
    '            End If
    '            ', Reason_For_CS_Absence = " & sNewGFSTO_CS_Absence & ", Expeditor_ID = " & sNewGFSTO_Expeditor, Expeditor_Name = isnull(e.Display_Name, e.Staff_Name) &
    '            '                        " left join DWH.UD.GFSTO_Staff e on '" & Replace(NewGFSTO_Expeditor.SelectedValue.ToString, "'", "''") & "' = e.GFSTO_Staff_ID " & _

    '        Next


    '        'Dim CheckCount As String = "select COUNT(*) from DWH.KPIS.Radiology_MRIObservations m " & _
    '        '    "join DWH.dbo.DimDate dd on dd.Calendar_Date = m.ExamDate " & _
    '        '    "join DWH.dbo.DimDate dd2 on dd.Calendar_Date = '" & Replace(txtExamDate.Text, "'", "''") & "' and dd.FY = dd2.FY and dd.Financial_Quarter = dd2.Financial_Quarter " & _
    '        '    "where Active = 1 " & _
    '        '    "and LocID = '" & Replace(ddlLocationSelect.SelectedValue.ToString, "'", "''") & "' "

    '        'If GetScalar(CheckCount) > 9 Then
    '        '    explanationlabel.Text = "10 X-Ray Imaging Views have already been submitted for this Quarter at this Location."
    '        '    ModalPopupExtender.Show()

    '        '    Exit Sub
    '        'End If

    '        'Dim SubmitSQL As String = "Insert into DWH.KPIS.Radiology_MRIObservations values (getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    '        '"', '" & UserName & "', '" & Replace(txtExamDate.Text, "'", "''") & "', '" & _
    '        'Replace(ddlLocationSelect.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlExamType.SelectedValue.ToString, "'", "''") & "', '" & _
    '        '"', '" & Replace(txtCheckInNumber.Text, "'", "''") & "', '" & _
    '        'Replace(ddlTechnique.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlPositioning.SelectedValue.ToString, "'", "''") & "', '" & _
    '        'Replace(ddlMarkers.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlObservation.SelectedValue.ToString, "'", "''") & "', '" & _
    '        'Replace(ddlConed.SelectedValue.ToString, "'", "''") & "', '" & Replace(txtObservComments.Text, "'", "''") & "', 1, null, null)"

    '        If Len(UpdatesSql) = 0 Then
    '            explanationlabel.Text = "No Modifications to Data"
    '            ModalPopupExtender.Show()
    '            Exit Sub
    '        End If

    '        ExecuteSql(UpdatesSql)
    '        PopulateGFSTOGrid()

    '        explanationlabel.Text = "Data Successfully Submitted."
    '        ModalPopupExtender.Show()

    '    Catch ex As Exception
    '        explanationlabel.Text = "Error Submitting Data; Please report to Website Administrator (" & WebAdminEmail & ")."
    '        ModalPopupExtender.Show()
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub PullStaff()
    '    Try

    '        Dim StaffSelection As String = "ClinicalSupervisor"
    '        If ddlManageWhat.SelectedValue = 1 Then
    '            StaffSelection = "ClinicalSupervisor"
    '        ElseIf ddlManageWhat.SelectedValue = 2 Then
    '            StaffSelection = "Circulator"
    '        ElseIf ddlManageWhat.SelectedValue = 3 Then
    '            StaffSelection = "Surgical_Tech"
    '            'ElseIf ddlManageWhat.SelectedValue = 4 Then
    '            '    StaffSelection = "Expeditor"
    '        End If

    '        Dim ExtraClause As String = ""
    '        If chkInactiveStaff.Checked = True Then
    '            ExtraClause = " or 2=2 "
    '        End If

    '        Dim s As String = "select * " & _
    '        ", case when ClinicalSupervisor = 1 then 'Inactivate' else 'Activate' end as ClinSupStatus " & _
    '        ", case when Circulator = 1 then 'Inactivate' else 'Activate' end as CirculatorStatus " & _
    '        ", case when Surgical_Tech = 1 then 'Inactivate' else 'Activate' end as SurgTechStatus " & _
    '        ", case when Expeditor = 1 then 'Inactivate' else 'Activate' end as ExpeditorStatus " & _
    '        ", isnull(Display_Name, Staff_Name) as StaffName " & _
    '        "from DWH.UD.GFSTO_Staff " & _
    '        "where (" & StaffSelection & " = 1 " & ExtraClause & ")" & _
    '        "and Staff_Name like '%" & Replace(txtStaffName_SearchSubmit.Text, "'", "''") & "%' " & _
    '        "order by Staff_Name "

    '        Staffdv = GetData(s).DefaultView
    '        gvGFSTOStaff.DataSource = Staffdv
    '        gvGFSTOStaff.DataBind()


    '    Catch ex As Exception
    '        explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
    '        explanationlabel.DataBind()
    '        ModalPopupExtender.Show()
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try

    'End Sub
    'Private Sub PullCodes()
    '    Try

    '        Dim StaffSelection As String = "Delay_Code"
    '        If ddlManageWhat.SelectedValue = 5 Then
    '            StaffSelection = "Delay_Code"
    '        ElseIf ddlManageWhat.SelectedValue = 6 Then
    '            StaffSelection = "Sub_Delay_Code"
    '        End If

    '        Dim s As String = "select * " & _
    '        ", case when Code_Classification = '" & StaffSelection & "' then 'Inactivate' else 'Activate' end as ActiveStatus " & _
    '        "from DWH.UD.GFSTO_Codes " & _
    '        "where (Code_Classification = '" & StaffSelection & "')" & _
    '        "and Code_Name like '%" & Replace(txtCodeDesc_SrchSubmit.Text, "'", "''") & "%' " & _
    '        "order by Code_Name "

    '        Codedv = GetData(s).DefaultView
    '        gvGFSOT_Codes.DataSource = Codedv
    '        gvGFSOT_Codes.DataBind()


    '    Catch ex As Exception
    '        explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
    '        explanationlabel.DataBind()
    '        ModalPopupExtender.Show()
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try

    'End Sub
    'Private Sub txtStaffName_SearchSubmit_TextChanged(sender As Object, e As EventArgs) Handles txtStaffName_SearchSubmit.TextChanged
    '    PullStaff()
    'End Sub

    'Private Sub chkInactiveStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkInactiveStaff.CheckedChanged
    '    PullStaff()
    'End Sub

    'Private Sub ddlManageWhat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageWhat.SelectedIndexChanged
    '    If ddlManageWhat.SelectedValue = 1 Then
    '        btnNewStaff.Text = "Add New Clinical Supervisor"
    '    ElseIf ddlManageWhat.SelectedValue = 2 Then
    '        btnNewStaff.Text = "Add New Circulator"
    '    ElseIf ddlManageWhat.SelectedValue = 3 Then
    '        btnNewStaff.Text = "Add New Surgical_Tech"
    '        'ElseIf ddlManageWhat.SelectedValue = 4 Then
    '        '    btnNewStaff.Text = "Add New Expeditor"
    '    ElseIf ddlManageWhat.SelectedValue = 5 Then
    '        btnNewCode.Text = "Add New Delay Code"
    '        pnlClinSups.Visible = False
    '        pnlCodes.Visible = True
    '        PullCodes()
    '        Exit Sub
    '    ElseIf ddlManageWhat.SelectedValue = 6 Then
    '        btnNewCode.Text = "Add New Sub Delay Code"
    '        pnlClinSups.Visible = False
    '        pnlCodes.Visible = True
    '        PullCodes()
    '        Exit Sub
    '    End If
    '    pnlCodes.Visible = False
    '    pnlClinSups.Visible = True
    '    PullStaff()
    'End Sub

    'Private Sub gvGFSTOStaff_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvGFSTOStaff.RowCancelingEdit
    '    Try
    '        gvGFSTOStaff.EditIndex = -1
    '        gvGFSTOStaff.DataSource = Staffdv
    '        gvGFSTOStaff.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvGFSTOStaff_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvGFSTOStaff.RowCommand
    '    Try
    '        Dim GFSTO_Staff_ID As String = e.CommandArgument
    '        Dim varname As String = e.CommandName
    '        Dim cmd As SqlCommand
    '        Dim da As New SqlDataAdapter
    '        Dim ds As New DataSet

    '        Dim Sql As String = "update DWH.UD.GFSTO_Staff set " & varname & " = 1 - " & varname & " where GFSTO_Staff_ID = '" & Replace(GFSTO_Staff_ID, "'", "''") & "'"

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(Sql, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvGFSTOStaff.EditIndex = -1
    '        PullStaff()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvGFSTOStaff_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvGFSTOStaff.RowCreated
    '    If e.Row.RowType = DataControlRowType.DataRow Then

    '        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

    '    End If

    '    If gvGFSTOStaff.EditIndex > -1 Then
    '        e.Row.Attributes.Remove("onclick")
    '    End If
    'End Sub


    'Private Sub gvGFSTOStaff_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvGFSTOStaff.RowEditing
    '    Try

    '        gvGFSTOStaff.EditIndex = e.NewEditIndex
    '        gvGFSTOStaff.DataSource = Staffdv
    '        gvGFSTOStaff.DataBind()

    '        Dim txtName As TextBox = gvGFSTOStaff.Rows(e.NewEditIndex).FindControl("txtStaffName")
    '        Dim lblName As Label = gvGFSTOStaff.Rows(e.NewEditIndex).FindControl("lblStaffName")

    '        txtName.Visible = True
    '        lblName.Visible = False


    '        For Each canoe As GridViewRow In gvGFSTOStaff.Rows
    '            If canoe.RowIndex = e.NewEditIndex Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
    '            ElseIf canoe.RowIndex Mod 2 = 0 Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
    '            Else
    '                canoe.BackColor = System.Drawing.Color.White
    '            End If
    '        Next

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvGFSTOStaff_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvGFSTOStaff.RowUpdating
    '    Try
    '        Dim depid As String = gvGFSTOStaff.DataKeys(e.RowIndex).Value.ToString

    '        Dim txtDept As TextBox = gvGFSTOStaff.Rows(e.RowIndex).FindControl("txtStaffName")
    '        Dim lblDept As Label = gvGFSTOStaff.Rows(e.RowIndex).FindControl("lblStaffName")

    '        Dim cmd As SqlCommand
    '        Dim da As New SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim Sql As String = "update DWH.UD.GFSTO_Staff " & _
    '            "set Display_Name = '" & Replace(txtDept.Text, "'", "''") & "' " & _
    '            "where GFSTO_Staff_ID = '" & Replace(depid, "'", "''") & "'"



    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(Sql, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvGFSTOStaff.EditIndex = -1
    '        PullStaff()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvGFSTOStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvGFSTOStaff.SelectedIndexChanged
    '    For Each canoe As GridViewRow In gvGFSTOStaff.Rows
    '        If canoe.RowIndex = gvGFSTOStaff.SelectedIndex Then
    '            canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
    '        ElseIf canoe.RowIndex Mod 2 = 0 Then
    '            canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
    '        Else
    '            canoe.BackColor = System.Drawing.Color.White
    '        End If
    '    Next
    'End Sub

    'Private Sub btnNewStaff_Click(sender As Object, e As EventArgs) Handles btnNewStaff.Click

    '    Dim StaffType As String = ""
    '    If ddlManageWhat.SelectedValue = 1 Then
    '        StaffType = "ClinicalSupervisor"
    '    ElseIf ddlManageWhat.SelectedValue = 2 Then
    '        StaffType = "Circulator"
    '    ElseIf ddlManageWhat.SelectedValue = 3 Then
    '        StaffType = "Surgical_Tech"
    '        'ElseIf ddlManageWhat.SelectedValue = 4 Then
    '        '    StaffType = "Expeditor"
    '    End If

    '    Dim ChkSql As String = "Select count(*) from DWH.UD.GFSTO_Staff where Staff_Name = '" & Replace(txtStaffName_SearchSubmit.Text, "'", "''") & "'"

    '    If GetScalar(ChkSql) > 0 Then
    '        explanationlabelp4.Text = "Staff Member with this name already exists; please revise"
    '        ModalPopupExtenderp4.Show()
    '        Exit Sub
    '    End If

    '    Dim sql As String = "Insert into DWH.UD.GFSTO_Staff (Staff_Name, " & StaffType & ") values ( " & _
    '        "'" & Replace(txtStaffName_SearchSubmit.Text, "'", "''") & _
    '        "', 1)"

    '    Dim cmd As SqlCommand
    '    Dim da As New SqlDataAdapter
    '    Dim ds As New DataSet

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '        cmd = New SqlCommand(sql, conn)
    '        cmd.CommandTimeout = 86400
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        cmd.ExecuteNonQuery()
    '    End Using

    '    gvGFSTOStaff.EditIndex = -1
    '    PullStaff()

    '    explanationlabelp4.Text = "New Staff Member Added"
    '    ModalPopupExtenderp4.Show()

    'End Sub

#End Region

    'Private Sub cbARBalancedRows_CheckedChanged(sender As Object, e As EventArgs) Handles cblARBalancedRows.SelectedIndexChanged
    '    ReloadMainPageR()
    'End Sub

    'Private Sub cblARCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblARCategory.SelectedIndexChanged
    '    ReloadMainPageR()
    'End Sub

    'Private Sub cblARCategoryStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblARCategoryStatus.SelectedIndexChanged
    '    ReloadMainPageR()
    'End Sub

    Private Sub txtARDate_TextChanged(sender As Object, e As EventArgs) Handles txtARDate.TextChanged
        Dim RunDate As String = "(select Value from DWH.AR.ActivityReport_ExtraValues with (nolock) where Description = 'RunDate' and Active = 1)"
        If txtARDate.Text = GetDate(RunDate) Then
            btnNewCashJournal.Visible = True
            If CInt(Developer.Text) = 1 Then
                btnRunDailyProcessing.Visible = True
                btnResetToday.Visible = True
                btnHoliday.Visible = True
            Else
                If GetScalar("select count(*) from DWH.AR.ActivityReport_Users  with (nolock) " & _
            "where RunProcessing = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
                    btnRunDailyProcessing.Visible = True
                Else
                    btnRunDailyProcessing.Visible = False
                End If

                If GetScalar("select count(*) from DWH.AR.ActivityReport_Users with (nolock)  " & _
                "where ResetRights = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
                    btnResetToday.Visible = True
                Else
                    btnResetToday.Visible = False
                End If

                If GetScalar("select count(*) from DWH.AR.ActivityReport_Users with (nolock) " & _
                "where HolidayRights = 1 and Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' ") > 0 Then
                    btnHoliday.Visible = True
                Else
                    btnHoliday.Visible = False
                End If

            End If
            Dim x As String = "select isnull(max(isnull(MultipleLocks, 2)), 1) from DWH.AR.ActivityReport_Users with (nolock)  " & _
                "where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            If GetScalar(x) > 0 Then
                btnSubmitAllRows.Visible = True
            Else
                btnSubmitAllRows.Visible = False
            End If

            Dim f As String = "select isnull(max(isnull(Netting, 2)), 1) from DWH.AR.ActivityReport_Users with (nolock) " & _
                "where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            If GetScalar(f) > 0 Then
                btnNetRows.Visible = True
            Else
                btnNetRows.Visible = False
            End If
        Else
            btnRunDailyProcessing.Visible = False
            btnHoliday.Visible = False
            btnResetToday.Visible = False
            btnSubmitAllRows.Visible = False
            btnNetRows.Visible = False
            btnNewCashJournal.Visible = False
        End If
            ReloadMainPageR()
    End Sub


    'Private Sub ddlARAssignedUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlARAssignedUser.SelectedIndexChanged
    '    ReloadMainPageR()
    'End Sub

    Private Sub gv_AR_MainData_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gv_AR_MainData.RowCommand

        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            'If sd = 1 Then
            '    se = se + " " + "desc"
            'Else
            '    se = se + " " + "asc"
            'End If

            If Commander = "Transfers" Then

                Dim PrepSQL As String = "delete from DWH.AR.ActivityReport_StagingTransfers " & _
                "where ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
                " " & _
                "insert into DWH.AR.ActivityReport_StagingTransfers  " & _
                "select TFID, TransferDate, TransferFrom, IntermediateRow, TransferTo, Amount, Active, IntermediateActive, getdate(), '" & _
                Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', null, null, CurrentFlag " & _
                "from ( " & _
                "select art.*, Row_Number() over (partition by TransferFrom, TransferTo order by TransferDate desc, ModifyDate desc )  RN " & _
                "from DWH.AR.ActivityReport_Transfers art  with (nolock) " & _
                "left join DWH.AR.ActivityReport_Detail ard  with (nolock) on art.TransferFrom = ard.ActivityID and ard.Active = 1 " & _
                "	and ard.ActivityDate <= '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
                "and not exists (select * from DWH.AR.ActivityReport_Detail ard2  with (nolock)  " & _
                "	where ard2.ActivityDate <= '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' and (ard2.ActivityDate > ard.ActivityDate or (ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate)) " & _
                "	 and ard.ActivityID = ard2.ActivityID and ard2.Active = 1)  " & _
                "where art.Active = 1 and TransferDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & _
                "' and ((" & Detail_ID & " in (IntermediateRow, TransferTo) and convert(date, TransferDate) = ard.ActivityDate) or (" & _
                Detail_ID & " in (TransferFrom) and CurrentFlag = case when ard.DetailStatus = 'Current' then 1 else 0 end)) ) x " & _
                "where RN = 1 and isnull(Amount, 0) <> 0 "

                ' 6/26/2017 Switched so that Transfers correspond to CurrentFlag
                '"insert into DWH.AR.ActivityReport_StagingTransfers " & _
                '"select TFID, TransferDate, TransferFrom, IntermediateRow, TransferTo, Amount, Active, IntermediateActive, getdate(), '" & _
                'Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', null, null " & _
                '"from ( " & _
                '"select *, Row_Number() over (partition by TransferFrom, TransferTo order by TransferDate desc, ModifyDate desc )  RN " & _
                '"from DWH.AR.ActivityReport_Transfers " & _
                '"where Active = 1 and TransferDate <= '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' and " & Detail_ID & " in (TransferFrom, IntermediateRow, TransferTo)) x " & _
                '"where RN = 1 and isnull(Amount, 0) <> 0 "


                ExecuteSql(PrepSQL)

                PopulateTransferGrid(Detail_ID)
                mpeTransfers.Show()
            ElseIf Commander = "EditRow" Then
                Try

                    Dim PrepSQL As String = "delete from DWH.AR.ActivityReport_StagingMiscGL " & _
                         "where ActivityID = '" & Detail_ID & "' " & _
                         "insert into DWH.AR.ActivityReport_StagingMiscGL " & _
                        "select ActivityID, Misc_Identity, ActivityDate, Comment, UserID, ModifiedDate, Active, MiscAmt, CurrentFlag " & _
                        "from DWH.AR.ActivityReport_MiscGL art " & _
                        "where art.Active = 1 and ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & _
                        "' and ActivityID = '" & Detail_ID & "' " &
                        "if convert(date, '" & Trim(Replace(txtARDate.Text, "'", "''")) & "') = (select Value from DWH.ar.ActivityReport_ExtraValues where Active = 1 and Description = 'RunDate') " & _
                        "begin " & _
                        "select 1 " & _
                        "end " & _
                        "else " & _
                        "begin " & _
                        "select 2 " & _
                        "end  "

                If GetScalar(PrepSQL) = "2" Then
                    explanationlabel.Text = "Daily Processing has been run; you are no longer on the current date"
                    ModalPopupExtender.Show()
                    Exit Sub
                End If

                '    Dim sql As String = "update DWH.AR.Activity_Detail " & _
                '"set Locked = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
                '"where Locked is null and Row_ID = '" & Detail_ID & "'"

                Dim sql As String = "Update DWH.AR.ActivityReport_Locking " & _
                    "set UserLocked = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' , LockedDate = getdate(), Active = 1 " & _
                    "where ActivityID = '" & Detail_ID & "' and Active = 0 " & _
                    " " & _
                    "insert into DWH.AR.ActivityReport_Locking " & _
                    "select '" & Detail_ID & "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1 " & _
                    "where not exists (select * from DWH.AR.ActivityReport_Locking where ActivityID = '" & Detail_ID & "' ) "

                ExecuteSql(sql)
                ReloadMainPageR()

                    Exit Sub
                Catch ex As Exception
                    explanationlabel.Text = "Error loading page.  Please contact Website Administrator (" & WebAdminEmail & ")."
                    explanationlabel.DataBind()
                    ModalPopupExtender.Show()
                    LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "ER - " & Detail_ID & " - " & Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try
            ElseIf Commander = "UnlockRow" Then

                '    Dim sql As String = "update DWH.AR.Activity_Detail " & _
                '"set Locked = null " & _
                '"where Locked = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and Row_ID = '" & Detail_ID & "'"

                Dim sql As String = "Update DWH.AR.ActivityReport_Locking " & _
                    "set Active = 0 " & _
                    "where ActivityID = '" & Detail_ID & "' and UserLocked = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' "

                ExecuteSql(sql)
                ReloadMainPageR()

                Exit Sub
            ElseIf Commander = "SplitRow" Then
                'Previous version; revised 1/3/2018 CRW for Transfers initially returning nulls
                'Dim s2 As String = "declare @SearchDate date = ' " & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
                '    "select sum(Cash_Received) from ( " & _
                '    "select *, ROW_NUMBER() over (partition by ActivityID order by ActivityDate desc, ModifiedDate desc) as RN  " & _
                '    "		from DWH.AR.ActivityReport_Detail where Active = 1 and ActivityDate <= @SearchDate and ActivityID = " & Detail_ID & ") x " & _
                '    "		where x.RN = 1 "

                Dim s2 As String = "declare @SearchDate date = ' " & Trim(Replace(txtARDate.Text, "'", "''")) & "'  " & _
                    "select sum(isnull(SplittingAmt, 0)) " & _
                    "from ( select ard.*, ROW_NUMBER() over (partition by ard.ActivityID order by ard.ActivityDate desc, ard.ModifiedDate desc) as RN   " & _
                    ", case when ar.CreatedBy like 'Transfer From%' then ti.TransferAmt else ard.Cash_Received end as SplittingAmt  " & _
                    "from DWH.AR.ActivityReport_Detail ard " & _
                    "join DWH.AR.ActivityReport ar on ard.ActivityID = ar.ActivityID and ar.Active = 1 " & _
                    "left join ( select sum(Amount) TransferAmt from DWH.AR.ActivityReport_Transfers " & _
                    "where TransferTo = '" & Detail_ID & "' and Active = 1 and TransferDate = @SearchDate) ti on 1 = 1 " & _
                    "where ard.Active = 1 and ActivityDate <= @SearchDate and ard.ActivityID = '" & Detail_ID & "') x 		where x.RN = 1 "

                'lblSplitTotal.Text = GetScalar(s2)

                'Dim s2 As String = "select convert(varchar, isnull(WF_Cash_Received, 0.00)) from DWH.AR.Activity_Detail where Row_ID = " & Detail_ID

                Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

                Using con As New SqlConnection(strConnString)

                    Using cmd As New SqlCommand()

                        cmd.CommandText = s2

                        cmd.Connection = con

                        If con.State = ConnectionState.Closed Then
                            con.Open()
                        End If

                        lblSplitTotal.Text = CDec(cmd.ExecuteScalar).ToString("F2")

                    End Using

                End Using
                InitialPopulateSplitGrid(Detail_ID)

                Dim total As Decimal = 0D

                For i As Integer = 0 To gvSplitRows.Rows.Count - 1

                    Dim txtSplitWF_Cash_Received As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitWF_Cash_Received"), TextBox)

                    If IsNumeric(txtSplitWF_Cash_Received.Text) Then
                        total += txtSplitWF_Cash_Received.Text
                    End If

                Next

                lblSplitRemaining.Text = "Remaining: " & (CDec(lblSplitTotal.Text) - total).ToString


                mpeSplit.Show()
            ElseIf Commander = "UpdateRow" Then
                Try

                UpdateRow(Detail_ID)
                ReloadMainPageR()
                If tblOutOfBalance.Visible Then
                    explanationlabel.Text = lblOutofBalance.Text
                    ModalPopupExtender.Show()
                End If

                'ElseIf Commander = "Attachment" Then
                '    Dim s As String = "javascript:window.open(""" & "../Tools/AR/Activity_Report_Attachments/?Detail_ID=" & Detail_ID & """, 'AR Activity Detail Attachments', 'height=700,width=620, scrollbars, resizable');"
                    '    ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
                Catch ex As Exception
                    explanationlabel.Text = "Error loading page.  Please contact Website Administrator (" & WebAdminEmail & ")."
                    explanationlabel.DataBind()
                    ModalPopupExtender.Show()
                    LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "UR - " & Detail_ID & " - " & Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try
            End If


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub UpdateRow(detid As Integer)

        Dim UpdatesSql As String = ""
        Dim PrepSql As String = ""

        For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

            If gv_AR_MainData.DataKeys(i)("ActivityID").ToString = detid.ToString Then

                Dim txtARBatchNo As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARBatchNo"), TextBox)
                Dim txtARSTARBatchNo As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARSTARBatchNo"), TextBox)
                'Dim txtARNoPatients As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARNoPatients"), TextBox)
                '9/20/2021 CRW
                'Dim txtARType As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARType"), TextBox)
                Dim ddlARType As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARType"), DropDownList)
                Dim txtARARPosted As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARARPosted"), TextBox)
                'Dim txtARMiscPosted As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARMiscPosted"), TextBox)
                Dim txtARInterest As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARInterest"), TextBox)
                Dim txtARComments As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARComments"), TextBox)
                'Dim ddlARRowMisc_GL_Acct_Nos As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARRowMisc_GL_Acct_Nos"), DropDownList)
                Dim ddlARCategory As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARCategory"), DropDownList)
                'Dim txtARMisc_GL_Acct_Nos As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARMisc_GL_Acct_Nos"), TextBox)

                Dim cbARCarryForward As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARCarryForward"), CheckBox)
                Dim cbARUnresolved As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARUnresolved"), CheckBox)
                Dim cbResearch As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARResearch"), CheckBox)

                Dim lblCategory As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARCategoryStatus"), Label)
                Dim lblFac As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARFacility"), Label)
                Dim lblReceived As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARWF_Cash_Received"), Label)
                Dim lblTransfers As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblTransfers"), Label)

                Dim lblARType As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARType"), Label)

                Dim CR As String
                Dim UR As String
                Dim RS As String

                Dim BatchNo As String
                Dim STARBatchNo As String
                Dim NoPats As String = "null"
                Dim ARType As String
                Dim ARPosted As String
                Dim MiscPosted As String
                Dim interest As String
                Dim ARComments As String
                Dim MiscGLNos As String
                Dim DDLCategory As String
                Dim MiscGLTxt As String
                Dim CashRec As String

                Dim eBatchNo As String
                Dim eSTARBatchNo As String
                Dim eNoPats As String = " is null"
                Dim eARType As String
                Dim eARPosted As String
                Dim eMiscPosted As String
                Dim einterest As String
                Dim eARComments As String
                Dim eMiscGLNos As String
                Dim eDDLCategory As String
                Dim eMiscGLTxt As String
                Dim eCashRec As String

                If Trim(Replace(txtARBatchNo.Text, "'", "''")) = "" Then
                    BatchNo = "null"
                    eBatchNo = " is null"
                Else
                    BatchNo = "'" & Trim(Replace(txtARBatchNo.Text, "'", "''")) & "'"
                    eBatchNo = " = '" & Trim(Replace(txtARBatchNo.Text, "'", "''")) & "'"
                End If
                If Trim(Replace(txtARSTARBatchNo.Text, "'", "''")) = "" Then
                    STARBatchNo = "null"
                    eSTARBatchNo = " is null"
                ElseIf txtARSTARBatchNo.Visible = True And Len(Trim(txtARSTARBatchNo.Text)) > 3 Then
                    explanationlabel.Text = Trim(Replace(Replace(txtARSTARBatchNo.Text, "'", ""), ",", "")) & " -- STAR Batch No cannot be longer than 3 digits "
                    ModalPopupExtender.Show()
                    Exit Sub
                ElseIf txtARSTARBatchNo.Visible = True And Integer.TryParse(txtARSTARBatchNo.Text, False) = False Then
                    explanationlabel.Text = Trim(Replace(Replace(txtARSTARBatchNo.Text, "'", ""), ",", "")) & " (STAR Batch No) must be an integer "
                    ModalPopupExtender.Show()
                    Exit Sub
                Else
                    STARBatchNo = "'" & Trim(Replace(txtARSTARBatchNo.Text, "'", "''")) & "'"
                    eSTARBatchNo = " = '" & Trim(Replace(txtARSTARBatchNo.Text, "'", "''")) & "'"
                End If

                'If Trim(Replace(txtARNoPatients.Text, "'", "''")) = "" Then
                '    NoPats = "null"
                '    eNoPats = " is null"
                'Else
                '    NoPats = "'" & Trim(Replace(txtARNoPatients.Text, "'", "''")) & "'"
                '    eNoPats = " = '" & Trim(Replace(txtARNoPatients.Text, "'", "''")) & "'"
                'End If
                '9/20/2021 CRW

                If Trim(Replace(ddlARType.SelectedValue, "'", "''")) = "" Then
                    ARType = "'" & Trim(Replace(lblARType.Text, "'", "''")) & "'"
                    eARType = " = '" & Trim(Replace(lblARType.Text, "'", "''")) & "'"
                    'ARType = "null"
                    'eARType = " is null"
                Else
                    ARType = "'" & Trim(Replace(ddlARType.SelectedValue, "'", "''")) & "'"
                    eARType = " = '" & Trim(Replace(ddlARType.SelectedValue, "'", "''")) & "'"
                End If
                If Trim(Replace(txtARARPosted.Text, "'", "''")) = "" Then
                    ARPosted = "null"
                    eARPosted = " is null"
                Else
                    Try
                        ARPosted = Decimal.Parse(Trim(Replace(Replace(txtARARPosted.Text, "'", ""), ",", ""))).ToString
                    Catch ex As Exception
                        explanationlabel.Text = Trim(Replace(Replace(txtARARPosted.Text, "'", ""), ",", "")) & " (AR Posted) cannot be parsed as money "
                        ModalPopupExtender.Show()
                        Exit Sub
                    End Try
                    eARPosted = " = " & Decimal.Parse(Trim(Replace(Replace(txtARARPosted.Text, "'", ""), ",", ""))).ToString
                End If
                If Trim(Replace(lblReceived.Text, "'", "''")) = "" Then
                    CashRec = "null"
                    eCashRec = " is null"
                Else
                    Try
                        CashRec = Decimal.Parse(Trim(Replace(Replace(lblReceived.Text, "'", ""), ",", ""))).ToString
                    Catch ex As Exception
                        explanationlabel.Text = Trim(Replace(Replace(lblReceived.Text, "'", ""), ",", "")) & " (Cash Received) cannot be parsed as money "
                        ModalPopupExtender.Show()
                        Exit Sub
                    End Try
                    eCashRec = " = " & Decimal.Parse(Trim(Replace(Replace(lblReceived.Text, "'", ""), ",", ""))).ToString
                End If
                If Trim(Replace(txtARInterest.Text, "'", "''")) = "" Then
                    interest = "null"
                    einterest = " is null"
                Else
                    Try
                        interest = Decimal.Parse(Trim(Replace(Replace(txtARInterest.Text, "'", ""), ",", ""))).ToString
                    Catch ex As Exception
                        explanationlabel.Text = Trim(Replace(Replace(txtARInterest.Text, "'", ""), ",", "")) & " (Interest) cannot be parsed as money "
                        ModalPopupExtender.Show()
                        Exit Sub
                    End Try
                    einterest = " = " & Decimal.Parse(Trim(Replace(Replace(txtARInterest.Text, "'", ""), ",", ""))).ToString
                End If
     
                'If Trim(Replace(txtARMisc_GL_Acct_Nos.Text, "'", "''")) = "" Then
                '    MiscGLTxt = "null"
                '    eMiscGLTxt = " is null"
                'Else
                '    MiscGLTxt = "'" & Trim(Replace(txtARMisc_GL_Acct_Nos.Text, "'", "''")) & "'"
                '    eMiscGLTxt = " = '" & Trim(Replace(txtARMisc_GL_Acct_Nos.Text, "'", "''")) & "'"
                'End If
                'If Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) = "" Then
                '    MiscGLNos = "null"
                '    eMiscGLNos = " is null"
                'ElseIf Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) = "-1" Then
                '    MiscGLNos = "null"
                '    eMiscGLNos = " is null"
                'ElseIf Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) = "11" Then
                '    If MiscGLTxt = "null" Then
                '        explanationlabel.Text = "Misc GL Text required when (Other) is selected for Misc Acct Nos"
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End If
                '    MiscGLNos = "'" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                '    eMiscGLNos = " = '" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                'Else
                '    MiscGLNos = "'" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                '    eMiscGLNos = " = '" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                'End If
                If Trim(Replace(ddlARCategory.SelectedValue, "'", "''")) = "" Then
                    DDLCategory = "null"
                    eDDLCategory = " is null"
                Else
                    DDLCategory = "'" & Trim(Replace(ddlARCategory.SelectedValue, "'", "''")) & "'"
                    eDDLCategory = " = '" & Trim(Replace(ddlARCategory.SelectedValue, "'", "''")) & "'"
                End If
                'If Trim(Replace(txtARMiscPosted.Text, "'", "''")) = "" Then
                '    MiscPosted = "null"
                '    eMiscPosted = " is null"
                'Else
                '    If MiscGLNos = "null" Then
                '        explanationlabel.Text = "Misc GL No required when Misc is posted"
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End If
                '    Try
                '        MiscPosted = Decimal.Parse(Trim(Replace(Replace(txtARMiscPosted.Text, "'", ""), ",", ""))).ToString
                '    Catch ex As Exception
                '        explanationlabel.Text = Trim(Replace(Replace(txtARMiscPosted.Text, "'", ""), ",", "")) & " (Misc) cannot be parsed as money "
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End Try
                '    eMiscPosted = " = " & Decimal.Parse(Trim(Replace(Replace(txtARMiscPosted.Text, "'", ""), ",", ""))).ToString

                'End If

                'MiscPosted = "(select sum(MiscAmt) from DWH.AR.ActivityReport_MiscGL where Active = 1 and ActivityID = '" & _
                '    gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' and CurrentFlag = case when '" & lblCategory.Text & "' = 'Current' then 1 else 0 end and ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & "')"

                MiscPosted = "(select sum(MiscAmt) from DWH.AR.ActivityReport_MiscGL where Active = 1 and ActivityID = '" & _
                    gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' and  ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "')"

                PrepSql += "update a " & _
                    "set a.Active = 0, ModifiedDate = getdate() " & _
                    "from DWH.AR.ActivityReport_MiscGL a " & _
                    "left join DWH.AR.ActivityReport_StagingMiscGL b  on a.ActivityID = b.ActivityId " & _
                    "	 and a.Misc_Identity = b.Misc_Identity and b.Active = 1 and a.ActivityDate = b.ActivityDate " & _
                    "where a.Active = 1 and a.ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' and a.ActivityId = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' " & _
                    "and (isnull(a.Comment, '') <> isnull(b.Comment, '') or isnull(a.MiscAmt, 0) <> isnull(b.MiscAmt, 0)) " & _
                    "insert into DWH.AR.ActivityReport_MiscGL (ActivityId, Misc_Identity, ActivityDate, Comment, UserID, ModifiedDate, Active, MiscAmt) " & _
                    "select ActivityId, Misc_Identity, ActivityDate, Comment, UserID, ModifiedDate, Active, MiscAmt " & _
                    "from DWH.AR.ActivityReport_StagingMiscGL b " & _
                    "where Active = 1 and not exists (select * from DWH.AR.ActivityReport_MiscGL a " & _
                    "where a.ActivityID = b.ActivityId and a.Misc_Identity = b.Misc_Identity and a.Active = 1 and a.ActivityDate = b.ActivityDate ) " & _
                    "and b.ActivityId = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "'"



                If lblCategory.Text = "Current" Then
                    If cbARCarryForward.Checked Then

                        If cbARUnresolved.Checked Then
                            explanationlabel.Text = "Both Carry Over and Unresolved have been selected"
                            ModalPopupExtender.Show()
                            Exit Sub
                            End If


                        CR = "isnull(" & CashRec & ", 0) - isnull(" & ARPosted & ", 0) - isnull(" & MiscPosted & ", 0) - isnull(" & interest & ", 0)" & _
                            " - isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferFrom = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0) + " & _
                                " isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferTo = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0)  "

                            '" + isnull(" & Decimal.Parse(Replace(lblTransfers.Text, "'", "''")).ToString & ", 0)"

                            'CR = "1"
                    Else
                        CR = "null"
                        End If

                    If cbARUnresolved.Checked Then

                        UR = "isnull(" & CashRec & ", 0) - isnull(" & ARPosted & ", 0) - isnull(" & MiscPosted & ", 0) - isnull(" & interest & ", 0)" & _
                                    " - isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferFrom = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0) +  " & _
                                " isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferTo = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0)  "

                            ' " + isnull(" & Decimal.Parse(Replace(lblTransfers.Text, "'", "''")).ToString & ", 0)"

                        Dim CheckAttachments As String = "Select count(*) from DWH.AR.ActivityReport_AttachedFiles where Active = 1 and ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' and len(Content) > 0 "
                        If GetScalar(CheckAttachments) = 0 Then
                            explanationlabel.Text = "Attachments required when row is unresolved"
                            ModalPopupExtender.Show()
                            Exit Sub
                            End If

                        Dim CheckUR As String = "Select count(*) from DWH.AR.ActivityReport_Comments where Active = 1 and ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "'"
                        If GetScalar(CheckAttachments) = 0 Then
                            explanationlabel.Text = "Comments required when row is unresolved"
                            ModalPopupExtender.Show()
                            Exit Sub
                            End If

                    Else
                        UR = "null"
                        End If


                Else
                    CR = " - isnull(" & ARPosted & ", 0) - isnull(" & MiscPosted & ", 0) - isnull(" & interest & ", 0)" & _
                         " - isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferFrom = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0) +  " & _
                                " isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferTo = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0)  "

                        '" + isnull(" & Decimal.Parse(Replace(lblTransfers.Text, "'", "''")).ToString & ", 0)"

                    If cbARUnresolved.Checked Then

                        UR = "isnull(" & CashRec & ", 0) - isnull(" & ARPosted & ", 0) - isnull(" & MiscPosted & ", 0) - isnull(" & interest & ", 0)" & _
                               " - isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferFrom = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0) +  " & _
                                " isnull((select sum(Amount) from DWH.AR.ActivityReport_Transfers " & _
                                "		where Active = 1 and convert(date, TransferDate) = '" & Replace(txtARDate.Text, "'", "''") & "'" & _
                                "		and TransferTo = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' ), 0)  "

                            ' " + isnull(" & Decimal.Parse(Replace(lblTransfers.Text, "'", "''")).ToString & ", 0)"

                        Dim CheckAttachments As String = "Select count(*) from DWH.AR.ActivityReport_AttachedFiles with (nolock) where Active = 1 and ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "' and len(Content) > 0 "
                        If GetScalar(CheckAttachments) = 0 Then
                            explanationlabel.Text = "Attachments required when row is unresolved"
                            ModalPopupExtender.Show()
                            Exit Sub
                            End If

                        Dim CheckUR As String = "Select count(*) from DWH.AR.ActivityReport_Comments with (nolock) where Active = 1 and ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "'"
                        If GetScalar(CheckAttachments) = 0 Then
                            explanationlabel.Text = "Comments required when row is unresolved"
                            ModalPopupExtender.Show()
                            Exit Sub
                            End If

                    Else
                        UR = "null"
                        End If
                    End If



                If cbResearch.Checked Then
                    RS = "'Research'"
                Else
                    RS = "'" & Replace(lblCategory.Text, "'", "''") & "'"
                    End If

                    'UpdatesSql = "Insert into DWH.AR.Activity_Detail_History SELECT Row_ID, Detail_ID " & _
                    '    "      ,Base_ID " & _
                    '    "      ,ActivityDate " & _
                    '    "     ,Active " & _
                    '    "     ,AssignedUser " & _
                    '    "     ,ModifyDate " & _
                    '    "      ,ModifyUser " & _
                    '    "      ,Facility " & _
                    '    "      ,CashCategory " & _
                    '    "      ,CategoryStatus " & _
                    '    "      ,DepositDate " & _
                    '    "      ,Batch_Number " & _
                    '    "      ,STAR_Batch_Number " & _
                    '    "      ,No_Patients " & _
                    '    "      ,Type " & _
                    '    "      ,WF_Cash_Received " & _
                    '    "      ,AR_Posted " & _
                    '    "      ,Resolve " & _
                    '    "      ,Misc_Posted " & _
                    '    "      ,Interest " & _
                    '    "      ,Carry_Forward " & _
                    '    "      ,Unresolved " & _
                    '    "      ,Comments " & _
                    '    "      ,Misc_GL_Acct_Nos       " & _
                    '    "      ,Research       " & _
                    '    "      ,Misc_GL_Text       " & _
                    '    "  FROM DWH.AR.Activity_Detail " & _
                    '    "  where Row_ID = " & gv_AR_MainData.DataKeys(i)("Row_ID").ToString() & _
                    '    " and NOT (Batch_Number " & eBatchNo & " and STAR_Batch_Number " & eSTARBatchNo & _
                    '    " and No_Patients " & eNoPats & " and Type " & eARType & " and AR_Posted " & eARPosted & " and Misc_Posted " & eMiscPosted & " and Misc_GL_Text " & eMiscGLTxt & " and Interest " & _
                    '    einterest & " and isnull(Carry_Forward, 0) = isnull(" & CR & ",0) and isnull(Unresolved,0) = isnull(" & UR & ",0) and Comments " & eARComments & " and Misc_GL_Acct_Nos " & _
                    '    eMiscGLNos & " and isnull(Research,0) = isnull(" & RS & ",0) and CashCategory " & eDDLCategory & ")"


                    'UpdatesSql += " Update DWH.AR.Activity_Detail set Batch_Number = " & BatchNo & ", STAR_Batch_Number = " & STARBatchNo & _
                    '    ", No_Patients = " & NoPats & ", Type = " & ARType & ", AR_Posted = " & ARPosted & ", Misc_Posted = " & MiscPosted & ", Misc_GL_Text = " & MiscGLTxt & ", Interest = " & _
                    '    interest & ", Research = " & RS & ", Carry_Forward = " & CR & ", Unresolved = " & UR & ", Comments = " & ARComments & ", Misc_GL_Acct_Nos = " & _
                    '    MiscGLNos & ", CashCategory = " & DDLCategory & ", Locked = null, ModifyDate = getdate(), ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
                    '    " where Row_ID = " & gv_AR_MainData.DataKeys(i)("Row_ID").ToString() & _
                    '    " and NOT (Batch_Number " & eBatchNo & " and STAR_Batch_Number " & eSTARBatchNo & _
                    '    " and No_Patients " & eNoPats & " and Type " & eARType & " and AR_Posted " & eARPosted & " and Misc_Posted " & eMiscPosted & " and Misc_GL_Text " & eMiscGLTxt & " and Interest " & _
                    '    einterest & " and isnull(Carry_Forward, 0) = isnull(" & CR & ",0) and isnull(Unresolved,0) = isnull(" & UR & ",0) and Comments " & eARComments & " and Misc_GL_Acct_Nos " & _
                    '    eMiscGLNos & " and isnull(Research,0) = isnull(" & RS & ",0) and CashCategory " & eDDLCategory & ")"

                UpdatesSql += "Insert into DWH.AR.ActivityReport_Detail values ('" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "', '" & _
                    Trim(Replace(txtARDate.Text, "'", "''")) & "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, '" & _
                    Trim(Replace(lblFac.Text, "'", "''")) & "', " & DDLCategory & ",  " & RS & ", " & ARType & ", " & BatchNo & ", " & STARBatchNo & ", " & _
                    NoPats & ", " & CashRec & ", " & ARPosted & ", null, " & MiscPosted & ", " & interest & ", " & CR & ", " & UR & ", null) " & _
                    "Update DWH.AR.ActivityReport_Locking set Active = 0 where ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "' "

                If Trim(Replace(txtARComments.Text, "'", "''")) = "" Then
                    UpdatesSql += "  Update DWH.AR.ActivityReport_Comments set Active = 0, UserID = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                        "', CommentDate = getdate() where ActivityID = '" & _
                    gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "' and Active = 1 and CommentType is null "
                Else
                    UpdatesSql += "Insert into DWH.AR.ActivityReport_Comments select '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "', '" & Trim(Replace(txtARDate.Text, "'", "''")) & _
                    "', '" & Replace(txtARComments.Text, "'", "''") & "', null, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1 " & _
                    "where not exists (select * from DWH.AR.ActivityReport_Comments where ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & _
                    "' and CommentType is null and Active = 1 )  Update DWH.AR.ActivityReport_Comments set Comment = '" & Replace(txtARComments.Text, "'", "''") & _
                    "', UserID = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', CommentDate = getdate() where ActivityID = '" & _
                    gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "' and Active = 1 and CommentType is null "
                    End If



                    'If MiscGLNos <> "null" Then
                    '    UpdatesSql += "Update DWH.AR.ActivityReport_MiscGL set Active = 0 where ActivityID = '" & _
                    '        gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "' and Active = 1 and NOT (Comment " & eMiscGLTxt & " and Misc_Identity " & eMiscGLNos & ") " & _
                    '        "Insert into DWH.AR.ActivityReport_MiscGL select '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & "', " & MiscGLNos & ", " & _
                    '        "'" & Trim(Replace(txtARDate.Text, "'", "''")) & "', " & MiscGLTxt & ", '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1 " & _
                    '        "where not exists (select * from DWH.AR.ActivityReport_MiscGL where ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString() & _
                    '        "' and Active = 1 )   "
                    'End If

            End If
        Next

        If Len(UpdatesSql) > 0 Then
            Try
                ExecuteSql(PrepSql)
                ExecuteSql(UpdatesSql)
                ExecuteSql("Update DWH.AR.ActivityReport_Detail set Carry_Forward = null where Carry_Forward = 0")
            Catch ex As Exception
                explanationlabel.Text = "Error loading page.  Please contact Website Administrator (" & WebAdminEmail & ")."
                explanationlabel.DataBind()
                ModalPopupExtender.Show()
                LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If

        'ReloadMainPage()

    End Sub

    Private Sub PopulateTransferGrid(detid As Integer)

        lblDetailIDTransfers.Text = detid

        'Dim s As String = "select isnull(tf.Facility, 'null') as TFFac, isnull(tt.Facility, 'null') as TTFAC, at.Amount, tf.Detail_ID as TFID,  tt.Detail_ID as TTID " & _
        '"from DWH.AR.Activity_Transfers at " & _
        '"join DWH.AR.Activity_Detail tf on at.TransferFrom = tf.Detail_ID " & _
        '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID " & _
        '"where at.Active = 1 and tf.Active = 1 and tt.Active = 1 and isnull(at.IntermediateActive, 0) = 0 " & _
        '"and tf.Row_ID = " & detid & " " & _
        '"union all " & _
        '"select tf.Facility, tt.Facility, at.Amount, tf.Detail_ID as TFID,  tt.Detail_ID as TTID " & _
        '"from DWH.AR.Activity_Transfers at " & _
        '"join DWH.AR.Activity_Detail tf on at.TransferFrom = tf.Detail_ID " & _
        '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID " & _
        '"where at.Active = 1 and tf.Active = 1 and tt.Active = 1 and isnull(at.IntermediateActive, 0) = 0 " & _
        '"and tt.Row_ID = " & detid & " " & _
        '        "union all " & _
        '"select tf.Facility, tt.Facility, at.Amount, tf.Detail_ID as TFID,  tt.Detail_ID as TTID " & _
        '"from DWH.AR.Activity_Transfers at " & _
        '"join DWH.AR.Activity_Detail tf on at.TransferFrom = tf.Detail_ID " & _
        '"join DWH.AR.Activity_Detail tt on at.IntermediateRow = tt.Detail_ID " & _
        '"where at.Active = 1 and tf.Active = 1 and tt.Active = 1 " & _
        '"and tt.Row_ID = " & detid & " and isnull(at.IntermediateActive, 0) = 1 " & _
        '        "union all " & _
        '"select tf.Facility, tt.Facility, at.Amount, tf.Detail_ID as TFID,  tt.Detail_ID as TTID " & _
        '"from DWH.AR.Activity_Transfers at " & _
        '"join DWH.AR.Activity_Detail tf on at.IntermediateRow = tf.Detail_ID " & _
        '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID " & _
        '"where at.Active = 1 and tf.Active = 1 and tt.Active = 1 and isnull(at.IntermediateActive, 0) = 1 " & _
        '"and tf.Row_ID = " & detid & " "

        '        Dim s As String = "select at.TFID as ID, isnull(adfrom.Facility, TFFac) as TFFac, isnull(isnull(adto.Facility, TTFac), 'null') as TTFac " & _
        '", at.Amount, at.TransferFrom  as TFID " & _
        '"	, at.TransferTo as TTID	 " & _
        '"	, 'Yes' as Editable	 " & _
        '"from DWH.AR.ActivityReport_StagingTransfers at " & _
        '"join DWH.AR.ActivityReport_Detail adfrom on at.TransferFrom = adfrom.ActivityID and adfrom.Active = 1 " & _
        '"	and adfrom.ActivityDate <= at.TransferDate and CurrentFlag = case when adfrom.DetailStatus = 'Current' then 1 else 0 end" & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.Active =1 and ad2.ActivityID = adfrom.ActivityID " & _
        '"		and ((ad2.ActivityDate = adfrom.ActivityDate and ad2.ModifiedDate > adfrom.ModifiedDate) " & _
        '"			or ad2.ActivityDate > adfrom.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        '"left join DWH.AR.ActivityReport_Detail adto on at.TransferTo = adto.ActivityID and adto.Active = 1 " & _
        '"	and adto.ActivityDate = at.TransferDate " & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.Active =1 and ad2.ActivityID = adto.ActivityID " & _
        '"		and ((ad2.ActivityDate = adto.ActivityDate and ad2.ModifiedDate > adto.ModifiedDate) " & _
        '"			or ad2.ActivityDate > adto.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        '"where at.Active = 1 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and TransferFrom = " & detid & " " & _
        '" " & _
        '"union all " & _
        '" " & _
        '"		select at.TFID as ID, isnull(adfrom.Facility, TFFac) as TFFac, isnull(isnull(adto.Facility, TTFac), 'null') as TTFac " & _
        '", at.Amount, at.TransferFrom  as TFID " & _
        '"	, at.TransferTo as TTID	 " & _
        '"	, 'No' as Editable	 " & _
        '"from DWH.AR.ActivityReport_StagingTransfers at " & _
        '"join DWH.AR.ActivityReport_Detail adfrom on at.TransferFrom = adfrom.ActivityID and adfrom.Active = 1 " & _
        '"	and adfrom.ActivityDate <= at.TransferDate and CurrentFlag = case when adfrom.DetailStatus = 'Current' then 1 else 0 end" & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.Active =1 and ad2.ActivityID = adfrom.ActivityID " & _
        '"		and ((ad2.ActivityDate = adfrom.ActivityDate and ad2.ModifiedDate > adfrom.ModifiedDate) " & _
        '"			or ad2.ActivityDate > adfrom.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        '"join DWH.AR.ActivityReport_Detail adto on at.TransferTo = adto.ActivityID and adto.Active = 1 " & _
        '"	and adto.ActivityDate = at.TransferDate " & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.Active =1 and ad2.ActivityID = adto.ActivityID " & _
        '"		and ((ad2.ActivityDate = adto.ActivityDate and ad2.ModifiedDate > adto.ModifiedDate) " & _
        '"			or ad2.ActivityDate > adto.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        '"where at.Active = 1 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and IntermediateRow = " & detid & " and IntermediateActive = 1 " & _
        '" " & _
        '"union all " & _
        '" " & _
        '"		select at.TFID as ID, isnull(adfrom.Facility, TFFac) as TFFac, isnull(isnull(adto.Facility, TTFac), 'null') as TTFac " & _
        '", -at.Amount, at.TransferFrom  as TFID " & _
        '"	, at.TransferTo as TTID	 " & _
        '"	, 'No' as Editable	 " & _
        '"from DWH.AR.ActivityReport_StagingTransfers at " & _
        '"join DWH.AR.ActivityReport_Detail adfrom on at.TransferFrom = adfrom.ActivityID and adfrom.Active = 1 " & _
        '"	and adfrom.ActivityDate <= at.TransferDate and CurrentFlag = case when adfrom.DetailStatus = 'Current' then 1 else 0 end" & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.Active =1 and ad2.ActivityID = adfrom.ActivityID " & _
        '"		and ((ad2.ActivityDate = adfrom.ActivityDate and ad2.ModifiedDate > adfrom.ModifiedDate) " & _
        '"			or ad2.ActivityDate > adfrom.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        '"join DWH.AR.ActivityReport_Detail adto on at.TransferTo = adto.ActivityID and adto.Active = 1 " & _
        '"	and adto.ActivityDate = at.TransferDate " & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.Active =1 and ad2.ActivityID = adto.ActivityID " & _
        '"		and ((ad2.ActivityDate = adto.ActivityDate and ad2.ModifiedDate > adto.ModifiedDate) " & _
        '"			or ad2.ActivityDate > adto.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        '"where at.Active = 1 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and TransferTo = " & detid & " "


        Dim s As String = "select at.TFID as ID, isnull(adfrom.Facility, TFFac) as TFFac, isnull(isnull(adto.Facility, TTFac), 'null') as TTFac " & _
        ", case when  " & detid & " = TransferTo then -at.Amount else at.Amount end as Amount, at.TransferFrom  as TFID " & _
        "	, at.TransferTo as TTID	 " & _
        "	, case when TransferFrom =  " & detid & " then 'Yes' else 'No'		end		as Editable	  " & _
        "from DWH.AR.ActivityReport_StagingTransfers at with (nolock) " & _
        "join DWH.AR.ActivityReport_Detail adfrom with (nolock) on at.TransferFrom = adfrom.ActivityID and adfrom.Active = 1 " & _
        "	and adfrom.ActivityDate <= at.TransferDate and CurrentFlag = case when adfrom.DetailStatus = 'Current' then 1 else 0 end" & _
        "	and not exists (select * from DWH.AR.ActivityReport_Detail ad2  with (nolock) where ad2.Active =1 and ad2.ActivityID = adfrom.ActivityID " & _
        "		and ((ad2.ActivityDate = adfrom.ActivityDate and ad2.ModifiedDate > adfrom.ModifiedDate) " & _
        "			or ad2.ActivityDate > adfrom.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        "left join DWH.AR.ActivityReport_Detail adto  with (nolock) on at.TransferTo = adto.ActivityID and adto.Active = 1 " & _
        "	and adto.ActivityDate = at.TransferDate " & _
        "	and not exists (select * from DWH.AR.ActivityReport_Detail ad2  with (nolock) where ad2.Active =1 and ad2.ActivityID = adto.ActivityID " & _
        "		and ((ad2.ActivityDate = adto.ActivityDate and ad2.ModifiedDate > adto.ModifiedDate) " & _
        "			or ad2.ActivityDate > adto.ActivityDate and ad2.ActivityDate <= at.TransferDate)) " & _
        "where at.Active = 1 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and  " & detid & " in (TransferFrom, TransferTo, IntermediateRow) "
       


        gvTransfers.DataSource = GetData(s).DefaultView
        gvTransfers.DataBind()

    End Sub


    Private Sub btnAddTransfer_Click(sender As Object, e As EventArgs) Handles btnAddTransfer.Click

        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvTransfers.Rows.Count - 1

            Dim lblTFID As Label = CType(gvTransfers.Rows(i).FindControl("lblTFID"), Label)
            Dim lblTTID As Label = CType(gvTransfers.Rows(i).FindControl("lblTTID"), Label)

            'Dim lblTFFAC As Label = CType(gvTransfers.Rows(i).FindControl("lblTFFAC"), Label)
            Dim lblTTFAC As Label = CType(gvTransfers.Rows(i).FindControl("lblTTFAC"), Label)
            Dim lblAmount As Label = CType(gvTransfers.Rows(i).FindControl("lblTAmount"), Label)

            'Dim NewTFFAC As DropDownList = CType(gvTransfers.Rows(i).FindControl("ddlTFFAC"), DropDownList)
            Dim NewTTFAC As DropDownList = CType(gvTransfers.Rows(i).FindControl("ddlTTFAC"), DropDownList)
            Dim NewTAmount As TextBox = CType(gvTransfers.Rows(i).FindControl("txtTAmount"), TextBox)


            Dim NewRow As Integer = 0
            If NewTTFAC.SelectedValue <> lblTTFAC.Text Then

                'UpdatesSql += "Insert into DWH.AR.Activity_Detail_History SELECT Row_ID, Detail_ID " & _
                '    "      ,Base_ID " & _
                '    "      ,ActivityDate " & _
                '    "     ,Active " & _
                '    "     ,AssignedUser " & _
                '    "     ,ModifyDate " & _
                '    "      ,ModifyUser " & _
                '    "      ,Facility " & _
                '    "      ,CashCategory " & _
                '    "      ,CategoryStatus " & _
                '    "      ,DepositDate " & _
                '    "      ,Batch_Number " & _
                '    "      ,STAR_Batch_Number " & _
                '    "      ,No_Patients " & _
                '    "      ,Type " & _
                '    "      ,WF_Cash_Received " & _
                '    "      ,AR_Posted " & _
                '    "      ,Resolve " & _
                '    "      ,Misc_Posted " & _
                '    "      ,Interest " & _
                '    "      ,Carry_Forward " & _
                '    "      ,Unresolved " & _
                '    "      ,Comments " & _
                '    "      ,Misc_GL_Acct_Nos       " & _
                '    "      ,Research       " & _
                '    "      ,Misc_GL_Text       " & _
                '    "  FROM DWH.AR.Activity_Detail " & _
                '    "  where Detail_ID = " & lblTTID.Text & " and ActivityDate = '" & txtARDate.Text & "' "

                'UpdatesSql += " Update DWH.AR.Activity_Detail set Facility = '" & NewTTFAC.SelectedValue & "', ModifyDate = getdate(), ModifyUser = '" & _
                '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where  Detail_ID = " & lblTTID.Text & " and ActivityDate = '" & txtARDate.Text & "' "

                UpdatesSql += " Update DWH.AR.ActivityReport_StagingTransfers set TTFAC = '" & NewTTFAC.SelectedValue & "' " & _
                    " where  TFID = " & gvTransfers.DataKeys(i).Value & "  "

            End If

            If lblAmount.Text <> NewTAmount.Text Then

                'UpdatesSql += "Insert into DWH.AR.Activity_Transfers_History SELECT " & _
                ' "      Base_ID " & _
                ' "      ,TransferFrom " & _
                ' "     ,TransferTo " & _
                ' "     ,Amount " & _
                ' "     ,Active " & _
                ' "     ,ModifyDate " & _
                ' "      ,ModifyUser " & _
                ' "  FROM DWH.AR.Activity_Transfers " & _
                ' "  where TransferFrom = " & lblTFID.Text & " and TransferTo = '" & lblTTID.Text & "' "

                'UpdatesSql += " Update DWH.AR.Activity_Transfers set Amount = '" & Trim(Replace(NewTAmount.Text, "'", "''")) & "', ModifyDate = getdate(), ModifyUser = '" & _
                '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where TransferFrom = '" & lblTFID.Text & "' " & _
                '    " and TransferTo = '" & lblTTID.Text & "' "

                UpdatesSql += " Update DWH.AR.ActivityReport_StagingTransfers set Amount = '" & Trim(Replace(NewTAmount.Text, "'", "''")) & "' " & _
                    " where  TFID = " & gvTransfers.DataKeys(i).Value & "  "

            End If

        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        Dim s As String = "insert into DWH.AR.ActivityReport_StagingTransfers (TFID, TransferDate, TransferFrom, Active, TFFac, CurrentFlag, ModifyUser ) " & _
            "select isnull((select min(TFID) from DWH.AR.ActivityReport_StagingTransfers where TFID < 0), 0) - 1,  '" & Replace(txtARDate.Text, "'", "''") & "', " & _
        " '" & lblDetailIDTransfers.Text & "', 1,  Facility, case when DetailStatus = 'Current' then 1 else 0 end, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' from ( " & _
        "select *, Row_Number() over (partition by ActivityID order by ActivityDate desc, ModifiedDate desc) RN " & _
        "from DWH.AR.ActivityReport_Detail where Active =  1 and ActivityID = " & lblDetailIDTransfers.Text & " and ActivityDate <= '" & Trim(Replace(txtARDate.Text, "'", "''")) & _
        "' ) x where RN = 1 "

            'Dim s As String = "insert into DWH.AR.Activity_Detail " & _
            '"(Detail_ID, Base_ID, ActivityDate, Active, AssignedUser, ModifyDate, ModifyUser, CashCategory, CategoryStatus, Type) " & _
            '"select (select max(Detail_ID) from DWH.AR.Activity_Detail) + 1, Base_ID, ActivityDate, Active, AssignedUser, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
            '"'+ 'TF - ' + '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' " & _
            '", 'Cash Journal', 'Current', CashCategory + ' Transfer from ' + Facility " & _
            '"from DWH.AR.Activity_Detail " & _
            '"where Detail_ID = " & Replace(lblDetailIDTransfers.Text, "'", "''") & " and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
            '" insert into DWH.AR.Activity_Transfers (Base_ID, TransferFrom, TransferTo, Amount, Active, ModifyDate, ModifyUser) " & _
            '"select Base_ID, Detail_ID,  (select max(Detail_ID) from DWH.AR.Activity_Detail where ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
            '"' + 'TF - ' + '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "'), null, 1, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
            '"from DWH.AR.Activity_Detail " & _
            '"where Detail_ID = " & Replace(lblDetailIDTransfers.Text, "'", "''") & " and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' "

            's += "/* TRANSFER SYNCH */ " & _
            '    "update at set at.IntermediateActive = 1 " & _
            '    " from DWH.AR.Activity_Transfers at " & _
            '    "join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '    "join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '    "where ad.Facility <> 'A' and tt.Facility <> 'A' " & _
            '    "and at.IntermediateRow is not null and isnull(IntermediateActive, 0) = 0 " & _
            '    "and at.Active = 1 " & _
            '    " " & _
            '    "update at set at.IntermediateActive = 0 " & _
            '    " from DWH.AR.Activity_Transfers at " & _
            '    "join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '    "join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '    "where (ad.Facility = 'A' or tt.Facility = 'A') " & _
            '    "and at.IntermediateRow is not null and isnull(IntermediateActive, 0) = 1 " & _
            '    "and at.Active = 1 " & _
            '    " " & _
            '    " insert into DWH.AR.Activity_Detail_History " & _
            '    "select  ad.Row_ID, ad.Detail_ID " & _
            '            "      ,ad.Base_ID " & _
            '            "      ,ad.ActivityDate " & _
            '            "     ,ad.Active " & _
            '            "     ,ad.AssignedUser " & _
            '            "     ,ad.ModifyDate " & _
            '            "      ,ad.ModifyUser " & _
            '            "      ,ad.Facility " & _
            '            "      ,ad.CashCategory " & _
            '            "      ,ad.CategoryStatus " & _
            '            "      ,ad.DepositDate " & _
            '            "      ,ad.Batch_Number " & _
            '            "      ,ad.STAR_Batch_Number " & _
            '            "      ,ad.No_Patients " & _
            '            "      ,ad.Type " & _
            '            "      ,ad.WF_Cash_Received " & _
            '            "      ,ad.AR_Posted " & _
            '            "      ,ad.Resolve " & _
            '            "      ,ad.Misc_Posted " & _
            '            "      ,ad.Interest " & _
            '            "      ,ad.Carry_Forward " & _
            '            "      ,ad.Unresolved " & _
            '            "      ,ad.Comments " & _
            '            "      ,ad.Misc_GL_Acct_Nos       " & _
            '             "      ,ad.Research       " & _
            '             "      ,Misc_GL_Text       " & _
            '    "from DWH.AR.Activity_Detail ad " & _
            '    "join DWH.AR.Activity_Transfers at on ad.Detail_ID = at.IntermediateRow " & _
            '    "where ad.Active <> at.IntermediateActive and at.Active = 1 " & _
            '    " " & _
            '    "update ad  " & _
            '    "set ad.Active = at.IntermediateActive, ModifyDate = getdate(), ModifyUser = 'IntermediateTransferChange' " & _
            '    "from DWH.AR.Activity_Detail ad " & _
            '    "join DWH.AR.Activity_Transfers at on ad.Detail_ID = at.IntermediateRow " & _
            '    "where ad.Active <> at.IntermediateActive and at.Active = 1 and ad.Active = 1 " & _
            '    " " & _
            '    "  insert into DWH.AR.Activity_Detail " & _
            '    "select (select max(x.Detail_ID) from DWH.AR.Activity_Detail x) + ROW_NUMBER() over (order by ad.Row_ID)  " & _
            '    ", ad.Base_ID, ad.ActivityDate, 1, ad.AssignedUser, getdate() " & _
            '    ", 'IntermediateTransferInsert-'+ convert(varchar, ad.Detail_ID) + ':'+ convert(varchar, tt.Detail_ID) " & _
            '    ", 'A', 'Cash Journal', 'Current' " & _
            '    ", ad.DepositDate, null, null, null  " & _
            '    ", ' Intermediate Transfer from ' + ad.Facility + ' to ' + tt.Facility " & _
            '    ", null, null, null, null, null, null, null, null, null, null " & _
            '    "from DWH.AR.Activity_Transfers at " & _
            '    "join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '    "join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '    "where ad.Facility <> 'A' and tt.Facility <> 'A' " & _
            '    "and at.IntermediateRow is null and at.Active = 1  " & _
            '    " " & _
            '    "update at  " & _
            '    "set at.IntermediateActive = 1, at.IntermediateRow = it.Detail_ID " & _
            '    "from DWH.AR.Activity_Transfers at " & _
            '    "join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '    "join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '    "join DWH.AR.Activity_Detail it on it.ModifyUser = 'IntermediateTransferInsert-'+ convert(varchar, at.TransferFrom) + ':'+ convert(varchar, at.TransferTo) " & _
            '    "where ad.Facility <> 'A' and tt.Facility <> 'A' " & _
            '    "and at.IntermediateRow is null and at.Active = 1  "



        ExecuteSql(s)

        PopulateTransferGrid(lblDetailIDTransfers.Text)
        mpeTransfers.Show()

    End Sub

    Private Sub btnOkayTransfers_Click(sender As Object, e As EventArgs) Handles btnOkayTransfers.Click


        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvTransfers.Rows.Count - 1

            Dim lblTFID As Label = CType(gvTransfers.Rows(i).FindControl("lblTFID"), Label)
            Dim lblTTID As Label = CType(gvTransfers.Rows(i).FindControl("lblTTID"), Label)

            'Dim lblTFFAC As Label = CType(gvTransfers.Rows(i).FindControl("lblTFFAC"), Label)
            Dim lblTTFAC As Label = CType(gvTransfers.Rows(i).FindControl("lblTTFAC"), Label)
            Dim lblAmount As Label = CType(gvTransfers.Rows(i).FindControl("lblTAmount"), Label)

            Dim NewTFFAC As DropDownList = CType(gvTransfers.Rows(i).FindControl("ddlTFFAC"), DropDownList)
            Dim NewTTFAC As DropDownList = CType(gvTransfers.Rows(i).FindControl("ddlTTFAC"), DropDownList)
            Dim NewTAmount As TextBox = CType(gvTransfers.Rows(i).FindControl("txtTAmount"), TextBox)

            If Len(Trim(lblExplanationTransfer.Text)) = 0 And Len(Trim(lblAmount.Text)) = 0 And NewTTFAC.SelectedValue = "null" And Len(Trim(NewTAmount.Text)) = 0 Then
            Else


                Dim NewRow As Integer = 0
                lblExplanationTransfer.Visible = False
                Dim x As Decimal
                Try
                    x = Decimal.Parse(NewTAmount.Text)
                Catch ex As Exception
                    lblExplanationTransfer.Text = "Could not parse '" & x.ToString & "' as money"
                    lblExplanationTransfer.Visible = True
                    mpeTransfers.Show()
                    Exit Sub
                End Try

                If NewTTFAC.SelectedValue = "null" And x <> 0 Then
                    lblExplanationTransfer.Text = "Please select valid facility for all nonzero transfers"
                    lblExplanationTransfer.Visible = True
                    mpeTransfers.Show()
                    Exit Sub
                ElseIf NewTTFAC.SelectedValue = "null" Then

                Else
                    UpdatesSql += " Update DWH.AR.ActivityReport_StagingTransfers set TTFAC = '" & Replace(NewTTFAC.SelectedValue, "'", "''") & _
           "', TFFAC = '" & Replace(NewTFFAC.SelectedValue, "'", "''") & "' " & _
       " where  TFID = " & gvTransfers.DataKeys(i).Value & "  "


                End If

                'If NewTTFAC.SelectedValue <> lblTTFAC.Text Then

                'UpdatesSql += "Insert into DWH.AR.Activity_Detail_History SELECT Row_ID, Detail_ID " & _
                '    "      ,Base_ID " & _
                '    "      ,ActivityDate " & _
                '    "     ,Active " & _
                '    "     ,AssignedUser " & _
                '    "     ,ModifyDate " & _
                '    "      ,ModifyUser " & _
                '    "      ,Facility " & _
                '    "      ,CashCategory " & _
                '    "      ,CategoryStatus " & _
                '    "      ,DepositDate " & _
                '    "      ,Batch_Number " & _
                '    "      ,STAR_Batch_Number " & _
                '    "      ,No_Patients " & _
                '    "      ,Type " & _
                '    "      ,WF_Cash_Received " & _
                '    "      ,AR_Posted " & _
                '    "      ,Resolve " & _
                '    "      ,Misc_Posted " & _
                '    "      ,Interest " & _
                '    "      ,Carry_Forward " & _
                '    "      ,Unresolved " & _
                '    "      ,Comments " & _
                '    "      ,Misc_GL_Acct_Nos       " & _
                '    "      ,Research       " & _
                '    "      ,Misc_GL_Text       " & _
                '    "  FROM DWH.AR.Activity_Detail " & _
                '    "  where Detail_ID = " & lblTTID.Text & " and ActivityDate = '" & txtARDate.Text & "' "

                'UpdatesSql += " Update DWH.AR.Activity_Detail set Facility = '" & NewTTFAC.SelectedValue & "', ModifyDate = getdate(), ModifyUser = '" & _
                '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where  Detail_ID = " & lblTTID.Text & " and ActivityDate = '" & txtARDate.Text & "' "
         
       
                'End If

                If lblAmount.Text <> NewTAmount.Text Then
                    'UpdatesSql += "Insert into DWH.AR.Activity_Transfers_History SELECT  " & _
                    '     "      Base_ID " & _
                    '     "      ,TransferFrom " & _
                    '     "     ,TransferTo " & _
                    '     "     ,Amount " & _
                    '     "     ,Active " & _
                    '     "     ,ModifyDate " & _
                    '     "      ,ModifyUser " & _
                    '     "  FROM DWH.AR.Activity_Transfers " & _
                    '     "  where TransferFrom = " & lblTFID.Text & " and TransferTo = '" & lblTTID.Text & "' "

                    'UpdatesSql += " Update DWH.AR.Activity_Transfers set Amount = '" & Trim(Replace(NewTAmount.Text, "'", "''")) & "', ModifyDate = getdate(), ModifyUser = '" & _
                    '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where TransferFrom = '" & lblTFID.Text & "' " & _
                    '    " and TransferTo = '" & lblTTID.Text & "' "


                    UpdatesSql += " Update DWH.AR.ActivityReport_StagingTransfers set Amount = '" & Trim(Replace(NewTAmount.Text, "'", "''")) & "' " & _
                        " where  TFID = " & gvTransfers.DataKeys(i).Value & "  "

                End If

            End If


        Next

        If Len(UpdatesSql) > 0 Then

            'UpdatesSql += "/* TRANSFER SYNCH */ " & _
            '"update at set at.IntermediateActive = 1 " & _
            '" from DWH.AR.Activity_Transfers at " & _
            '"join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '"where ad.Facility <> 'A' and tt.Facility <> 'A' " & _
            '"and at.IntermediateRow is not null and isnull(IntermediateActive, 0) = 0 " & _
            '"and at.Active = 1 " & _
            '" " & _
            '"update at set at.IntermediateActive = 0 " & _
            '" from DWH.AR.Activity_Transfers at " & _
            '"join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '"where (ad.Facility = 'A' or tt.Facility = 'A') " & _
            '"and at.IntermediateRow is not null and isnull(IntermediateActive, 0) = 1 " & _
            '"and at.Active = 1 " & _
            '" " & _
            '" insert into DWH.AR.Activity_Detail_History " & _
            '"select  ad.Row_ID, ad.Detail_ID " & _
            '        "      ,ad.Base_ID " & _
            '        "      ,ad.ActivityDate " & _
            '        "     ,ad.Active " & _
            '        "     ,ad.AssignedUser " & _
            '        "     ,ad.ModifyDate " & _
            '        "      ,ad.ModifyUser " & _
            '        "      ,ad.Facility " & _
            '        "      ,ad.CashCategory " & _
            '        "      ,ad.CategoryStatus " & _
            '        "      ,ad.DepositDate " & _
            '        "      ,ad.Batch_Number " & _
            '        "      ,ad.STAR_Batch_Number " & _
            '        "      ,ad.No_Patients " & _
            '        "      ,ad.Type " & _
            '        "      ,ad.WF_Cash_Received " & _
            '        "      ,ad.AR_Posted " & _
            '        "      ,ad.Resolve " & _
            '        "      ,ad.Misc_Posted " & _
            '        "      ,ad.Interest " & _
            '        "      ,ad.Carry_Forward " & _
            '        "      ,ad.Unresolved " & _
            '        "      ,ad.Comments " & _
            '        "      ,ad.Misc_GL_Acct_Nos       " & _
            '        "      ,ad.Research       " & _
            '        "      ,Misc_GL_Text       " & _
            '"from DWH.AR.Activity_Detail ad " & _
            '"join DWH.AR.Activity_Transfers at on ad.Detail_ID = at.IntermediateRow " & _
            '"where ad.Active <> at.IntermediateActive and at.Active = 1 " & _
            '" " & _
            '"update ad  " & _
            '"set ad.Active = at.IntermediateActive, ModifyDate = getdate(), ModifyUser = 'IntermediateTransferChange' " & _
            '"from DWH.AR.Activity_Detail ad " & _
            '"join DWH.AR.Activity_Transfers at on ad.Detail_ID = at.IntermediateRow " & _
            '"where ad.Active <> at.IntermediateActive and at.Active = 1 and ad.Active = 1 " & _
            '" " & _
            '"  insert into DWH.AR.Activity_Detail " & _
            '"select (select max(x.Detail_ID) from DWH.AR.Activity_Detail x) + ROW_NUMBER() over (order by ad.Row_ID)  " & _
            '", ad.Base_ID, ad.ActivityDate, 1, ad.AssignedUser, getdate() " & _
            '", 'IntermediateTransferInsert-'+ convert(varchar, ad.Detail_ID) + ':'+ convert(varchar, tt.Detail_ID) " & _
            '", 'A', 'Cash Journal', 'Current' " & _
            '", ad.DepositDate, null, null, null  " & _
            '", ' Intermediate Transfer from ' + ad.Facility + ' to ' + tt.Facility " & _
            '", null, null, null, null, null, null, null, null, null, null " & _
            '"from DWH.AR.Activity_Transfers at " & _
            '"join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '"where ad.Facility <> 'A' and tt.Facility <> 'A' " & _
            '"and at.IntermediateRow is null and at.Active = 1  " & _
            '" " & _
            '"update at  " & _
            '"set at.IntermediateActive = 1, at.IntermediateRow = it.Detail_ID " & _
            '"from DWH.AR.Activity_Transfers at " & _
            '"join DWH.AR.Activity_Detail ad on at.TransferFrom = ad.Detail_ID and ad.Active = 1 " & _
            '"join DWH.AR.Activity_Detail tt on at.TransferTo = tt.Detail_ID and tt.Active = 1 " & _
            '"join DWH.AR.Activity_Detail it on it.ModifyUser = 'IntermediateTransferInsert-'+ convert(varchar, at.TransferFrom) + ':'+ convert(varchar, at.TransferTo) " & _
            '"where ad.Facility <> 'A' and tt.Facility <> 'A' " & _
            '"and at.IntermediateRow is null and at.Active = 1  "

            ExecuteSql(UpdatesSql)
        End If

        ' 1/28/2020 CRW Potential bug fix; previous version:
        '        "update c set IntermediateRow = d.ActivityID " & _
        '"from DWH.AR.ActivityReport_Transfers c join " & _
        '"(select *, Row_Number() over (order by TFID) RN from DWH.AR.ActivityReport_Transfers  " & _
        '"	where Active = 1 and IntermediateActive = 1 and IntermediateRow is null and TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & _
        '"' and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' ) a on a.TFID = c.TFID " & _
        '"join (select *, ROW_NUMBER() over (order by TFID desc) RN from DWH.AR.ActivityReport_StagingTransfers  " & _
        '"	where TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "'  and 'A' not in (TFFac, TTFac) and Active = 1 and isnull(Amount, 0) <> 0 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and TFID < 0 and isnull(TTFac, 'null') <> 'null' and  'A' not in (TFFac, TTFac)) b on a.RN = b.RN " & _
        '"join (select *, Row_Number() over (order by ActivityID) RN from DWH.AR.ActivityReport ar " & _
        '"	where CreatedBy like 'Intermediate Transfer From %' and CreatedBy like '%(" & Replace(lblDetailIDTransfers.Text, "'", "''") & ")' and ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and Active = 1 " & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Transfers t where t.IntermediateRow = ar.ActivityID and t.Active = 1) " & _
        '"	) d " & _
        '"	 on CreatedBy like 'Intermediate Transfer From ' + b.TFFac + ' To ' + b.TTFac + '%' and a.IntermediateActive = 1 and a.IntermediateRow is null  " & _
        '"	 and a.RN= d.RN " & _

        Dim confirmSQl As String = "Insert DWH.AR.ActivityReport " & _
"output Inserted.FirstActivityDate, substring(Inserted.CreatedBy, 18, len(Inserted.CreatedBy) - 18), null, Inserted.ActivityID, null, 1, null, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', null  " & _
"into DWH.AR.ActivityReport_Transfers " & _
"select TransferDate, null, null, 'Transfer From ' + TFFac + ' (' + convert(varchar, TransferFrom) + ')', getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1  " & _
"from DWH.AR.ActivityReport_StagingTransfers at " & _
"where ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and at.Active = 1 and at.TransferFrom =  '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' " & _
"and at.TFID < 0 and isnull(TTFac, 'null') <> 'null' and isnull(at.Amount, 0) <> 0 " & _
" " & _
"update c " & _
"set Amount = b.Amount " & _
", IntermediateActive = case when  'A' not in (TFFac, TTFac) then 1 else null end " & _
"from DWH.AR.ActivityReport_Transfers c join " & _
"(select *, Row_Number() over (order by TFID) RN from DWH.AR.ActivityReport_Transfers  " & _
"	where TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and Active = 1 and Amount is null) a on a.TFID = c.TFID " & _
"join (select *, ROW_NUMBER() over (order by TFID desc) RN from DWH.AR.ActivityReport_StagingTransfers  " & _
"	where TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and Active = 1 and isnull(Amount, 0) <> 0 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and TFID < 0) b on a.RN = b.RN " & _
"	 " & _
"Insert into DWH.AR.ActivityReport " & _
"select TransferDate, null, null, 'Intermediate Transfer From ' + TFFac + ' To ' + TTFac + ' (' + convert(varchar, TransferFrom) + ')', getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1  " & _
"from DWH.AR.ActivityReport_StagingTransfers at " & _
"where ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and at.Active = 1 and at.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' " & _
"and at.TFID < 0 and isnull(TTFac, 'null') <> 'null' and 'A' not in (TFFac, TTFac) and isnull(at.Amount, 0) <> 0" & _
" " & _
"update c set IntermediateRow = d.ActivityID " & _
"from DWH.AR.ActivityReport_Transfers c join " & _
"(select *, Row_Number() over (order by TFID) RN from DWH.AR.ActivityReport_Transfers  " & _
"	where Active = 1 and IntermediateActive = 1 and IntermediateRow is null and TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & _
"' and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' ) a on a.TFID = c.TFID " & _
"join (select *, ROW_NUMBER() over (order by TFID desc) RN, ROW_NUMBER() over (partition by TTFAC order by TFID desc) as RN2 from DWH.AR.ActivityReport_StagingTransfers  " & _
"	where TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "'  and 'A' not in (TFFac, TTFac) and Active = 1 and isnull(Amount, 0) <> 0 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and TFID < 0 and isnull(TTFac, 'null') <> 'null' and  'A' not in (TFFac, TTFac)) b on a.RN = b.RN " & _
"join (select *, Row_Number() over (partition by CreatedBy order by ActivityID) RN from DWH.AR.ActivityReport ar " & _
"	where CreatedBy like 'Intermediate Transfer From %' and CreatedBy like '%(" & Replace(lblDetailIDTransfers.Text, "'", "''") & ")' and ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and Active = 1 " & _
"	and not exists (select * from DWH.AR.ActivityReport_Transfers t where t.IntermediateRow = ar.ActivityID and t.Active = 1) " & _
"	) d " & _
"	 on CreatedBy = 'Intermediate Transfer From ' + b.TFFac + ' To ' + b.TTFac + ' (' + convert(varchar, c.TransferFrom) + ')'  and a.IntermediateActive = 1 and a.IntermediateRow is null  " & _
"	 and b.RN2= d.RN " & _
"	  " & _
"insert into DWH.AR.ActivityReport_TransfersHistorical " & _
"select t.* " & _
"from DWH.Ar.ActivityReport_StagingTransfers st " & _
"join DWH.AR.ActivityReport_Transfers t on st.TFID = t.TFID  " & _
"where st.TFID > 0 and st.TransferDate = t.TransferDate and  st.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and st.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
"and st.Amount <> t.Amount " & _
"or ((t.IntermediateActive = 1 and  'A' in (TFFac, TTFac)) " & _
"	or (isnull(t.IntermediateActive, 0) = 0 and  'A' not in (TFFac, TTFac))) " & _
" " & _
"update t " & _
"set Amount = st.Amount, ModifyDate = getdate(), ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'  " & _
", IntermediateActive = case when 'A' in (TFFac, TTFac) then 0 when TTFAC is null then null else  1 end " & _
"from DWH.Ar.ActivityReport_StagingTransfers st " & _
"join DWH.AR.ActivityReport_Transfers t on st.TFID = t.TFID  " & _
"where t.TransferDate = st.TransferDate and st.TFID > 0 and st.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and st.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
"and st.Amount <> t.Amount " & _
"or ((t.IntermediateActive = 1 and  'A' in (TFFac, TTFac)) " & _
"	or (isnull(t.IntermediateActive, 0) = 0 and  'A' not in (TFFac, TTFac))) " & _
" " & _
"Insert into DWH.AR.ActivityReport " & _
"select at.TransferDate, null, null, 'Intermediate Transfer From ' + TFFac + ' To ' + TTFac + ' (' + convert(varchar, at.TransferFrom) + ')', getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1  " & _
"from DWH.AR.ActivityReport_StagingTransfers at " & _
"join DWH.AR.ActivityReport_Transfers t on at.TFID = t.TFID  " & _
"where at.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and at.Active = 1 and at.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' " & _
"and t.IntermediateActive = 1 and t.IntermediateRow is null " & _
" " & _
"update t  " & _
"set IntermediateRow = ar.ActivityID " & _
"from (select *, ROW_NUMBER() over (partition by TransferFrom, TFFac, TTFac order by TFID desc) RN  " & _
"	from DWH.AR.ActivityReport_StagingTransfers where isnull(Amount, 0) <> 0 and TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') at " & _
"join DWH.AR.ActivityReport_Transfers t on at.TFID = t.TFID  " & _
"join (select *, ROW_NUMBER() over (partition by CreatedBy order by ActivityID) RN from DWH.AR.ActivityReport ar " & _
"	where not exists (select * from DWH.AR.ActivityReport_Transfers t where t.IntermediateRow = ar.ActivityID and t.Active = 1)) " & _
"	 ar on ar.RN = at.RN and CreatedBy = 'Intermediate Transfer From ' + TFFac + ' To ' + TTFac + ' (' + convert(varchar, at.TransferFrom) + ')' " & _
"where isnull(t.IntermediateRow, 0) = 0 and t.IntermediateActive = 1 " & _
" " & _
"Insert into DWH.AR.ActivityReport_Detail  (ActivityID, ActivityDate, ModifiedBy, ModifiedDate, Active, Facility  " & _
", CashCategory, DetailStatus, Type, BankBatchNumber, STARBatchNumber, NoPatients, Cash_Received, AR_Posted, Resolve, Misc_Posted, Interest " & _
", Carry_Forward, Unresolved, Locked) " & _
"select d.ActivityID, st.TransferDate, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, case when TTFac = 'null' then d.Facility else TTFac end, CashCategory, DetailStatus, Type, " & _
"BankBatchNumber, STARBatchNumber, NoPatients, Cash_Received, AR_Posted, Resolve, Misc_Posted, Interest " & _
", Carry_Forward, Unresolved, 0 " & _
"from DWH.Ar.ActivityReport_StagingTransfers st " & _
"join DWH.AR.ActivityReport_Transfers t on st.TFID = t.TFID  " & _
"join DWH.AR.ActivityReport_Detail d on t.TransferTo = d.ActivityID and d.Active = 1  " & _
"	and d.ActivityDate <= t.TransferDate and not exists (select * from DWH.AR.ActivityReport_Detail d2   " & _
"		where d2.Active = 1 and d2.ActivityID = t.TransferTo and   " & _
"		((d2.ActivityDate = d.ActivityDate and d2.ModifiedDate > d.ModifiedDate)   " & _
"		or (d2.ActivityDate > d.ActivityDate and d2.ActivityDate <= t.TransferDate)))	  " & _
"where st.TFID > 0 and st.TTFAC is not null and st.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and st.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
"and st.TTFac <> d.Facility " & _
" " & _
"Insert into DWH.AR.ActivityReport_Detail  (ActivityID, ActivityDate, ModifiedBy, ModifiedDate, Active, Facility  " & _
", CashCategory, DetailStatus, Type, Locked) " & _
"select st.ActivityID, t.TransferDate, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1 " & _
", case when st.ActivityID = t.IntermediateRow then 'A' " & _
"	else stf.TTFac end " & _
", 'Cash Journal', DetailStatus, Type " & _
", 0 " & _
"from DWH.Ar.ActivityReport st " & _
"join DWH.AR.ActivityReport_Transfers t on st.ActivityID in (t.IntermediateRow, t.TransferTo) " & _
"join DWH.AR.ActivityReport_StagingTransfers stf on t.TFID = stf.TFID " & _
"join DWH.AR.ActivityReport_Detail d on t.TransferFrom = d.ActivityID and d.Active = 1  " & _
"	and d.ActivityDate <= t.TransferDate and not exists (select * from DWH.AR.ActivityReport_Detail d2   " & _
"		where d2.Active = 1 and d2.ActivityID = t.TransferFrom and   " & _
"		((d2.ActivityDate = d.ActivityDate and d2.ModifiedDate > d.ModifiedDate)   " & _
"		or (d2.ActivityDate > d.ActivityDate and d2.ActivityDate <= t.TransferDate)))	  " & _
"where  t.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and t.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
"and not exists (select * from DWH.AR.ActivityReport_Detail ard where ard.ActivityID = st.ActivityID and ard.Active = 1) " & _
"	Insert into DWH.AR.ActivityReport_Detail  (ActivityID, ActivityDate, ModifiedBy, ModifiedDate, Active, Facility " & _
    ", CashCategory, DetailStatus, Type) " & _
    "select a.activityID, st.TransferDate, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, case when a.ActivityID = a.IntermediateRow then 'A' else st.TTFac end, 'Cash Journal', 'Current' /*DetailStatus*/, Type  " & _
    "from  " & _
    "(select ar.*, t.TransferFrom, t.Amount, t.IntermediateActive, t.IntermediateRow, d.DetailStatus, d.Type " & _
    ", Dense_Rank() over (Partition by t.TransferFrom, Amount, IntermediateActive order by t.TFID) as RN   " & _
    "from DWH.AR.ActivityReport_Transfers t " & _
    "join DWH.AR.ActivityReport ar on ar.ActivityID in (t.TransferTo, t.IntermediateRow) and ar.Active = 1 " & _
    "join DWH.AR.ActivityReport_Detail d on t.TransferFrom = d.ActivityID and d.Active = 1   " & _
    "	and d.ActivityDate <= t.TransferDate and not exists (select * from DWH.AR.ActivityReport_Detail d2   " & _
    "	where d2.Active = 1 and d2.ActivityID = t.TransferFrom and " & _
    "	((d2.ActivityDate = d.ActivityDate and d2.ModifiedDate > d.ModifiedDate) " & _
    "	or (d2.ActivityDate > d.ActivityDate and d2.ActivityDate <= t.TransferDate))) " & _
    "where t.TransferFrom = '" & Replace(lblDetailIDTransfers.Text, "'", "''") & "' and t.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
    "' and not exists (select * from DWH.AR.ActivityReport_Detail ard where ard.ActivityID = ar.ActivityID and ard.Active = 1) ) a " & _
    "join (select *, case when 'A' in (TFFAC, TTFAC) then 0 else 1 end as IntAct " & _
    "		, ROW_NUMBER() over (partition by TransferFrom, Amount, case when 'A' in (TFFAC, TTFAC) then 0 else 1 end order by TFID desc) RN  " & _
    "	from DWH.AR.ActivityReport_StagingTransfers where TFID < 0 and isnull(Amount, 0) <> 0  and TTFAC is not null and Active = 1 ) st on st.TransferFrom = a.TransferFrom and st.Amount = a.Amount  " & _
    "	and st.RN = a.RN and isnull(st.IntAct, 0) = isnull(a.IntermediateActive, 0) " & _
        "insert into DWH.AR.ActivityReport_Assignments  " & _
    "select ActivityID, ModifiedBy, getdate(), 'Automatic' from DWH.AR.ActivityReport ar " & _
    "where Active = 1 and (CreatedBy like 'Transfer From %' or CreatedBy like 'Intermediate Transfer From %')" & _
    "and not exists (select * from DWH.AR.ActivityReport_Assignments ass where ass.ActivityID = ar.ActivityID) " & _
    "update art set CurrentFlag = case when DetailStatus = 'Current' then 1 else 0 end " & _
    "from DWH.AR.ActivityReport_Transfers art " & _
    "left join DWH.AR.ActivityReport_Detail ard on art.TransferFrom = ard.ActivityID and ard.Active = 1 " & _
    " and convert(date, art.TransferDate) = ard.ActivityDate " & _
    "and not exists (select * from DWH.AR.ActivityReport_Detail ard2  " & _
    "	where ard2.ActivityDate = convert(date, art.TransferDate) and (ard2.ActivityDate > ard.ActivityDate or (ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate)) " & _
    "	 and ard.ActivityID = ard2.ActivityID and ard2.Active = 1) " & _
    "where art.CurrentFlag is null " & _
"insert into DWH.AR.ActivityReport_Detail " & _
"select ard.ActivityID, case when TFDate > TFTDate then TFDate when TFTDate < TFDate then TFTDate when TFDate is not null then TFDate when TFTDate is not null then TFTDate else ard.ActivityDate end " & _
", 'Automatic Carry Forward Synch', getdate(), ard.Active, ard.Facility, ard.CashCategory, ard.DetailStatus, ard.Type, ard.BankBatchNumber, ard.STARBatchNumber, ard.NoPatients " & _
", ard.Cash_Received, ard.AR_Posted, ard.Resolve, ard.Misc_Posted, ard.Interest " & _
", isnull(Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0)  " & _
", ard.Unresolved " & _
", ard.Locked " & _
"froM DWH.AR.ActivityReport_Detail ard " & _
"left join (select TransferFrom, sum(Amount) TransferredOut, TransferDate as TFDate from DWH.AR.ActivityReport_Transfers " & _
"		where Active = 1  " & _
"		group by TransferFrom, TransferDate) tff on tff.TransferFrom = ard.ActivityID and TFDate >= ard.ActivityDate " & _
"left join (select TransferTo, sum(Amount) TransferredIn, TransferDate as TFTDate from DWH.AR.ActivityReport_Transfers  " & _
"		where Active = 1   " & _
"		group by TransferTo, TransferDate) tft on tft.TransferTo = ard.ActivityID and TFTDate = ard.ActivityDate  " & _
"where ard.Active = 1 and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.Active = 1 and ard2.ActivityID = ard.ActivityID " & _
"	and (ard.ActivityDate < ard2.ActivityDate or (ard.ActivityDate = ard2.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))) " & _
"and DetailStatus = 'Current' and Unresolved is null and Carry_Forward is not null " & _
"and isnull(Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0) <> isnull(Carry_Forward, 0) " & _
"and (AR_Posted is not null or Misc_Posted is not null or Interest is not null or TransferredOut is not null or TransferredIn is not null or Carry_Forward is not null) " & _
" " & _
"insert into DWH.AR.ActivityReport_Detail " & _
"select ard.ActivityID, case when TFDate > TFTDate then TFDate when TFTDate < TFDate then TFTDate when TFDate is not null then TFDate when TFTDate is not null then TFTDate else ard.ActivityDate end " & _
", 'Automatic Unresolved Synch', getdate(), ard.Active, ard.Facility, ard.CashCategory, ard.DetailStatus, ard.Type, ard.BankBatchNumber, ard.STARBatchNumber, ard.NoPatients " & _
", ard.Cash_Received, ard.AR_Posted, ard.Resolve, ard.Misc_Posted, ard.Interest, ard.Carry_Forward " & _
", isnull(Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0)  " & _
", ard.Locked " & _
"froM DWH.AR.ActivityReport_Detail ard " & _
"left join (select TransferFrom, sum(Amount) TransferredOut,  TransferDate as TFDate from DWH.AR.ActivityReport_Transfers " & _
"		where Active = 1  " & _
"		group by TransferFrom, TransferDate) tff on tff.TransferFrom = ard.ActivityID and TFDate >= ard.ActivityDate and not exists " & _
        "(select * from DWH.AR.ActivityReport_Detail ardt where ardt.ActivityID	= ard.ActivityID and ardt.ActivityDate <= TFDate and " & _
 "       ((ardt.ActivityDate > ard.ActivityDate or (ardt.ActivityDate = ard.ActivityDate and ardt.ModifiedDate > ardt.ModifiedDate)) and ardt.Active = 1)) " & _
"left join (select TransferTo, sum(Amount) TransferredIn, max(convert(date, TransferDate)) as TFTDate from DWH.AR.ActivityReport_Transfers  " & _
"		where Active = 1   " & _
"		group by TransferTo) tft on tft.TransferTo = ard.ActivityID and TFTDate = ard.ActivityDate  " & _
"where ard.Active = 1 and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.Active = 1 and ard2.ActivityID = ard.ActivityID " & _
"	and (ard.ActivityDate < ard2.ActivityDate or (ard.ActivityDate = ard2.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))) " & _
"and DetailStatus = 'Current' and Unresolved is not null " & _
"and isnull(Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0) <> isnull(Unresolved, 0) " & _
"and (AR_Posted is not null or Misc_Posted is not null or Interest is not null or TransferredOut is not null or TransferredIn is not null or Unresolved is not null) " & _
" " & _
"insert into DWH.AR.ActivityReport_Detail " & _
"select ard.ActivityID, case when TFDate > TFTDate then TFDate when TFTDate < TFDate then TFTDate when TFDate is not null then TFDate when TFTDate is not null then TFTDate else ard.ActivityDate end " & _
", 'Automatic Carry Forward Synch', getdate(), ard.Active, ard.Facility, ard.CashCategory, ard.DetailStatus, ard.Type, ard.BankBatchNumber, ard.STARBatchNumber, ard.NoPatients " & _
", ard.Cash_Received, ard.AR_Posted, ard.Resolve, ard.Misc_Posted, ard.Interest " & _
", - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0)  " & _
", ard.Unresolved " & _
", ard.Locked " & _
"froM DWH.AR.ActivityReport_Detail ard " & _
"left join (select TransferFrom, sum(Amount) TransferredOut, TransferDate as TFDate from DWH.AR.ActivityReport_Transfers " & _
"		where Active = 1  " & _
"		group by TransferFrom, TransferDate) tff on tff.TransferFrom = ard.ActivityID and TFDate >= ard.ActivityDate 			 " & _
"left join (select TransferTo, sum(Amount) TransferredIn, max(convert(date, TransferDate)) as TFTDate from DWH.AR.ActivityReport_Transfers  " & _
"		where Active = 1   " & _
"		group by TransferTo) tft on tft.TransferTo = ard.ActivityID and TFTDate = ard.ActivityDate  " & _
"where ard.Active = 1 and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.Active = 1 and ard2.ActivityID = ard.ActivityID " & _
"	and (ard.ActivityDate < ard2.ActivityDate or (ard.ActivityDate = ard2.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))) " & _
"and DetailStatus <> 'Current' and Unresolved is null " & _
"and - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0) <> isnull(Carry_Forward, 0) " & _
"and (AR_Posted is not null or Misc_Posted is not null or Interest is not null or TransferredOut is not null or TransferredIn is not null or Carry_Forward is not null) " & _
" " & _
"insert into DWH.AR.ActivityReport_Detail " & _
"select ard.ActivityID, case when TFDate > TFTDate then TFDate when TFTDate < TFDate then TFTDate when TFDate is not null then TFDate when TFTDate is not null then TFTDate else ard.ActivityDate end " & _
", 'Automatic Unresolved Synch', getdate(), ard.Active, ard.Facility, ard.CashCategory, ard.DetailStatus, ard.Type, ard.BankBatchNumber, ard.STARBatchNumber, ard.NoPatients " & _
", ard.Cash_Received, ard.AR_Posted, ard.Resolve, ard.Misc_Posted, ard.Interest " & _
",  - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0)  " & _
", isnull(Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0)  " & _
", ard.Locked " & _
"froM DWH.AR.ActivityReport_Detail ard " & _
"left join (select TransferFrom, sum(Amount) TransferredOut, TransferDate as TFDate from DWH.AR.ActivityReport_Transfers " & _
"		where Active = 1  " & _
"		group by TransferFrom, TransferDate) tff on tff.TransferFrom = ard.ActivityID and tff.TFDate >= ard.ActivityDate 	and not exists " & _
        "(select * from DWH.AR.ActivityReport_Detail ardt where ardt.ActivityID	= ard.ActivityID and ardt.ActivityDate <= TFDate and " & _
 "       ((ardt.ActivityDate > ard.ActivityDate or (ardt.ActivityDate = ard.ActivityDate and ardt.ModifiedDate > ardt.ModifiedDate)) and ardt.Active = 1)) " & _
"left join (select TransferTo, sum(Amount) TransferredIn, TransferDate as TFTDate from DWH.AR.ActivityReport_Transfers  " & _
"		where Active = 1   " & _
"		group by TransferTo, TransferDate) tft on tft.TransferTo = ard.ActivityID and TFTDate = ard.ActivityDate  " & _
"where ard.Active = 1 and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.Active = 1 and ard2.ActivityID = ard.ActivityID " & _
"	and (ard.ActivityDate < ard2.ActivityDate or (ard.ActivityDate = ard2.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))) " & _
"and DetailStatus = 'Current' and Unresolved is not null " & _
"and isnull(Cash_Received, 0) - isnull(AR_Posted, 0) - isnull(Misc_Posted, 0) - isnull(Interest, 0) - isnull(TransferredOut, 0) + isnull(TransferredIn, 0) <> isnull(Unresolved, 0) " & _
"and (AR_Posted is not null or Misc_Posted is not null or Interest is not null or TransferredOut is not null or TransferredIn is not null or Unresolved is not null) " & _
        "/* '6/14/2018 CRW -- Added last two lines to prevent weird duplication possibility */  delete from DWH.AR.ActivityReport_StagingTransfers " & _
                "where ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
        "/* Assingment Logging 6/11/2019 CRW */ " & _
        "insert into DWH.ar.ActivityReport_Assignments_Logging  " & _
        "select *, null, null from DWH.AR.ActivityReport_Assignments ara " & _
        "where not exists (select * from DWH.ar.ActivityReport_Assignments_Logging aral " & _
        "	where ara.ActivityID = aral.ActivityID and ara.ModifyDate = aral.AddedDate) " & _
        " " & _
        "update ar1 " & _
        "set InactivatedDate = ar2.AddedDate, InactivatedBy = ar2.addedby " & _
        "from DWH.ar.ActivityReport_Assignments_Logging ar1 " & _
        "join DWH.AR.ActivityReport_Assignments_Logging ar2 on ar1.ActivityID = ar2.ActivityID " & _
        "	and ar1.InactivatedDate is null and ar2.InactivatedDate is null and ar2.AddedDate > ar1.AddedDate " & _
            "and not exists (select * from DWH.AR.ActivityReport_Assignments_Logging ar3 where ar3.ActivityID = ar2.ActivityID and ar3.InactivatedDate is null " & _
"		and ar3.AddedDate > ar1.AddedDate and ar3.AddedDate < ar2.AddedDate ) "

        '6/14/2018 CRW -- Added last two lines to prevent weird duplication possibility
        '6/20/2018 CRW -- changed line 3101 From 
        '"IntermediateActive = case when 'A' in (TFFac, TTFac) then 0 else  1 end" 
        'to " IntermediateActive = case when 'A' in (TFFac, TTFac) then 0 when TTFAC is null then null else  1 end"
        ' in order to correct intermediate row vanishing

        ExecuteSql(confirmSQl)
        ExecuteSql("Update DWH.AR.ActivityReport_Detail set Carry_Forward = null where Carry_Forward = 0")

        mpeTransfers.Hide()
        'ReloadMainPage()

    End Sub

    'Private Sub cblARFacility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblARFacility.SelectedIndexChanged
    '    ReloadMainPageR()
    'End Sub

    Protected Sub ddlARRowAssignedUser_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

            Dim lblARAssignedUser As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARAssignedUser"), Label)
            Dim ddlARRowAssignedUser As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARRowAssignedUser"), DropDownList)


            Dim NewRow As Integer = 0
            If ddlARRowAssignedUser.SelectedValue <> lblARAssignedUser.Text Then
                'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
                UpdatesSql += "Update DWH.AR.ActivityReport_Assignments " & _
                    "set UserAssigned = case when '" & ddlARRowAssignedUser.SelectedValue & "' = 'Unassigned' then null else '" & ddlARRowAssignedUser.SelectedValue & _
                    "' end, ModifyDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                    "where ActivityID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' and isnull(UserAssigned, 'Unassigned') <> '" & ddlARRowAssignedUser.SelectedValue & "' " & _
                    " " & _
                    "insert into DWH.AR.ActivityReport_Assignments " & _
                    "select '" & gv_AR_MainData.DataKeys(i).Value.ToString & "', '" & ddlARRowAssignedUser.SelectedValue & "', getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                    "where '" & ddlARRowAssignedUser.SelectedValue & "' <> 'Unassigned' and not exists (select * from DWH.AR.ActivityReport_Assignments where ActivityID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' ) "
            End If


        Next

        If Len(UpdatesSql) > 0 Then
            UpdatesSql += "/* Assingment Logging 6/11/2019 CRW */ " & _
        "insert into DWH.ar.ActivityReport_Assignments_Logging  " & _
        "select *, null, null from DWH.AR.ActivityReport_Assignments ara " & _
        "where not exists (select * from DWH.ar.ActivityReport_Assignments_Logging aral " & _
        "	where ara.ActivityID = aral.ActivityID and ara.ModifyDate = aral.AddedDate) " & _
        " " & _
        "update ar1 " & _
        "set InactivatedDate = ar2.AddedDate, InactivatedBy = ar2.addedby " & _
        "from DWH.ar.ActivityReport_Assignments_Logging ar1 " & _
        "join DWH.AR.ActivityReport_Assignments_Logging ar2 on ar1.ActivityID = ar2.ActivityID " & _
        "	and ar1.InactivatedDate is null and ar2.InactivatedDate is null and ar2.AddedDate > ar1.AddedDate " & _
            "and not exists (select * from DWH.AR.ActivityReport_Assignments_Logging ar3 where ar3.ActivityID = ar2.ActivityID and ar3.InactivatedDate is null " & _
"		and ar3.AddedDate > ar1.AddedDate and ar3.AddedDate < ar2.AddedDate ) "
            ExecuteSql(UpdatesSql)
        End If
    End Sub

    Private Sub InitialPopulateSplitGrid(detid As Integer)

        lblSplitDetailID.Text = detid

        'Dim s As String = "select convert(varchar, ROW_NUMBER() over (order by -Detail_ID desc)) + '. ' as RN, isnull(Detail_ID, 0) as Detail_ID, Batch_Number, WF_Cash_Received  " & _
        '    "from ( " & _
        '    "select  Detail_ID, Batch_Number, WF_Cash_Received from DWH.AR.Activity_Detail " & _
        '    "where Detail_ID = " & detid.ToString & " " & _
        '    "union all " & _
        '    "select Detail_ID, Batch_Number, WF_Cash_Received from DWH.AR.Activity_Detail " & _
        '    "where ModifyUser = 'Splitting - " & detid & "' " & _
        '    "union all " & _
        '    "select null, null, null) x " & _
        '    "order by 1"

        Dim s As String = "declare @SearchDate date = ' " & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
        "delete from DWH.AR.ActivityReport_StagingSplitting where InitialID = " & detid & " or ModifyUser = '" & _
        Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
        "insert into DWH.AR.ActivityReport_StagingSplitting (InitialID, SplitID, STARBatchNo, AmountSplit, ActivityDate, Active)" & _
        "select InitialID, SplitID, STARBatchNo, AmountSplit, ActivityDate, 1 from  " & _
        " DWH.AR.ActivityReport_Splitting where Active = 1 and ActivityDate = @SearchDate " & _
        "		and InitialID = " & detid & "  " & _
                "select convert(varchar, ROW_NUMBER() over (order by -SplitID desc)) + '. ' as RN " & _
            ", isnull(SplitID, 0) as SplitID, STARBatchNo , case when AmountSplit = 0 then null else AmountSplit end AmountSplit   " & _
            "            from (  " & _
            "select SplitID, STARBatchNo, AmountSplit from  " & _
            "	(select *, DENSE_RANK() over (partition by InitialID order by ActivityDate desc, ModifyDate desc) as RN  " & _
            "		from DWH.AR.ActivityReport_StagingSplitting where Active = 1 and ActivityDate = @SearchDate) s " & _
            "where s.RN = 1 " & _
            "and s.InitialID = " & detid & " " & _
            "union all  " & _
            "            select null, null, null) x  "


        '        "insert into DWH.AR.ActivityReport_StagingSplitting (InitialID, SplitID, STARBatchNo, AmountSplit, ActivityDate)" & _
        '"select InitialID, SplitID, STARBatchNumber, Cash_Received, x.ActivityDate from  " & _
        '"(select *, DENSE_RANK() over (partition by InitialID order by ActivityDate desc, ModifyDate desc) as RN   " & _
        '"		from DWH.AR.ActivityReport_Splitting where Active = 1 and ActivityDate <= @SearchDate " & _
        '"		and InitialID = " & detid & ") x " & _
        '"join (select *, ROW_NUMBER() over (partition by ActivityID order by ActivityDate desc, ModifiedDate desc) as RN " & _
        '    "		from DWH.AR.ActivityReport_Detail where Active = 1 and ModifiedBy like 'Split - %' and ActivityDate <= @SearchDate) o on o.RN = 1 and x.SplitID = o.ActivityID " & _
        '    " where x.RN = 1 " & _
        '    "select convert(varchar, ROW_NUMBER() over (order by -SplitID desc)) + '. ' as RN " & _
        '    ", isnull(SplitID, 0) as SplitID, STARBatchNo , AmountSplit   " & _
        '    "            from (  " & _
        '    "select SplitID, STARBatchNo, AmountSplit from  " & _
        '    "	(select *, DENSE_RANK() over (partition by InitialID order by ActivityDate desc, ModifyDate desc) as RN  " & _
        '    "		from DWH.AR.ActivityReport_StagingSplitting where Active = 1 and ActivityDate <= @SearchDate) s " & _
        '    "where s.RN = 1 " & _
        '    "and s.InitialID = " & detid & " " & _
        '    "union all  " & _
        '    "            select null, null, null) x  "

        gvSplitRows.DataSource = GetData(s).DefaultView
        gvSplitRows.DataBind()


    End Sub
    Private Sub PopulateSplitGrid(detid As Integer)

        lblSplitDetailID.Text = detid

        'Dim s As String = "select convert(varchar, ROW_NUMBER() over (order by -Detail_ID desc)) + '. ' as RN, isnull(Detail_ID, 0) as Detail_ID, Batch_Number, WF_Cash_Received  " & _
        '    "from ( " & _
        '    "select  Detail_ID, Batch_Number, WF_Cash_Received from DWH.AR.Activity_Detail " & _
        '    "where Detail_ID = " & detid.ToString & " " & _
        '    "union all " & _
        '    "select Detail_ID, Batch_Number, WF_Cash_Received from DWH.AR.Activity_Detail " & _
        '    "where ModifyUser = 'Splitting - " & detid & "' " & _
        '    "union all " & _
        '    "select null, null, null) x " & _
        '    "order by 1"

        Dim s As String = "declare @SearchDate date = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
            "select convert(varchar, ROW_NUMBER() over (order by -SplitID desc)) + '. ' as RN " & _
            ", isnull(SplitID, 0) as SplitID, STARBatchNo , case when AmountSplit = 0 then null else AmountSplit end AmountSplit   " & _
            "            from (  " & _
            "select SplitID, STARBatchNo, AmountSplit from  " & _
            "	(select *, DENSE_RANK() over (partition by InitialID order by ActivityDate desc, ModifyDate desc) as RN  " & _
            "		from DWH.AR.ActivityReport_StagingSplitting where Active = 1 and ActivityDate <= @SearchDate) s " & _
            "where s.RN = 1 " & _
            "and s.InitialID = " & detid & " " & _
            "union all  " & _
            "            select null, null, null) x  "

        gvSplitRows.DataSource = GetData(s).DefaultView
        gvSplitRows.DataBind()


    End Sub

    Protected Sub txtSplit_TextChanged(sender As Object, e As EventArgs)

        Dim focus As Integer

        For i As Integer = 0 To gvSplitRows.Rows.Count - 1

            Dim lblSplitDetail_ID As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitDetail_ID"), Label)
            Dim txtSplitBatch_Number As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitBatch_Number"), TextBox)
            Dim txtSplitWF_Cash_Received As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitWF_Cash_Received"), TextBox)

            Dim lblSplitBatch_Number As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitBatch_Number"), Label)
            Dim lblSplitWF_Cash_Received As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitWF_Cash_Received"), Label)

            If lblSplitDetail_ID.Text = "0" Then
                If Len(Trim(txtSplitBatch_Number.Text)) > 0 Or Len(Trim(txtSplitWF_Cash_Received.Text)) > 0 Then
                    focus = i
                    Exit For
                End If
            Else
                If lblSplitBatch_Number.Text <> txtSplitBatch_Number.Text Or lblSplitWF_Cash_Received.Text <> txtSplitWF_Cash_Received.Text Then
                    focus = i
                    Exit For
                End If
            End If
        Next

        Splitting()
        PopulateSplitGrid(lblSplitDetailID.Text)
        mpeSplit.Show()


        Dim txtSplitWF_Cash_Received2 As TextBox = CType(gvSplitRows.Rows(focus).FindControl("txtSplitWF_Cash_Received"), TextBox)

        txtSplitWF_Cash_Received2.Focus()

    End Sub
    Protected Sub txtSplitTotal_TextChanged(sender As Object, e As EventArgs)

        Dim focus As Integer

        For i As Integer = 0 To gvSplitRows.Rows.Count - 1

            Dim lblSplitDetail_ID As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitDetail_ID"), Label)
            Dim txtSplitBatch_Number As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitBatch_Number"), TextBox)
            Dim txtSplitWF_Cash_Received As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitWF_Cash_Received"), TextBox)

            Dim lblSplitBatch_Number As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitBatch_Number"), Label)
            Dim lblSplitWF_Cash_Received As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitWF_Cash_Received"), Label)

            If lblSplitDetail_ID.Text = "0" Then
                If Len(Trim(txtSplitBatch_Number.Text)) > 0 Or Len(Trim(txtSplitWF_Cash_Received.Text)) > 0 Then
                    focus = i
                    Exit For
                End If
            Else
                If lblSplitBatch_Number.Text <> txtSplitBatch_Number.Text Or lblSplitWF_Cash_Received.Text <> txtSplitWF_Cash_Received.Text Then
                    focus = i
                    Exit For
                End If
            End If
        Next

        Splitting()

        Dim total As Decimal = 0D

        For i As Integer = 0 To gvSplitRows.Rows.Count - 1

            Dim txtSplitWF_Cash_Received As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitWF_Cash_Received"), TextBox)

            If IsNumeric(txtSplitWF_Cash_Received.Text) Then
                total += txtSplitWF_Cash_Received.Text
            End If

        Next

        btnSubmitSplit.Visible = True
        'If lblSplitTotal.Text - total = 0 Then
        '    btnSubmitSplit.Visible = True
        'Else
        '    btnSubmitSplit.Visible = False
        'End If

        lblSplitRemaining.Text = "Remaining: " & (CDec(lblSplitTotal.Text) - total).ToString

        PopulateSplitGrid(lblSplitDetailID.Text)
        mpeSplit.Show()

        'If btnSubmitSplit.Visible = False Then
        Try
            Dim txtSplitBatch_Number2 As TextBox = CType(gvSplitRows.Rows(focus + 1).FindControl("txtSplitBatch_Number"), TextBox)
            txtSplitBatch_Number2.Focus()
        Catch ex As Exception

        End Try

        'End If

    End Sub
    Public Sub Splitting()

        Dim s As String = ""

        For i As Integer = 0 To gvSplitRows.Rows.Count - 1

            Dim lblSplitDetail_ID As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitDetail_ID"), Label)
            Dim txtSplitBatch_Number As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitBatch_Number"), TextBox)
            Dim txtSplitWF_Cash_Received As TextBox = CType(gvSplitRows.Rows(i).FindControl("txtSplitWF_Cash_Received"), TextBox)

            Dim lblSplitBatch_Number As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitBatch_Number"), Label)
            Dim lblSplitWF_Cash_Received As Label = CType(gvSplitRows.Rows(i).FindControl("lblSplitWF_Cash_Received"), Label)

            lblExplanationSplit.Text = ""
            lblExplanationSplit.Visible = False

            If lblSplitDetail_ID.Text = "0" Then

                If Len(Trim(txtSplitBatch_Number.Text)) > 0 Or Len(Trim(txtSplitWF_Cash_Received.Text)) > 0 Then

                    If Integer.TryParse(txtSplitBatch_Number.Text, False) = False Then
                        lblExplanationSplit.Text = "STAR Batch No must be an integer"
                        lblExplanationSplit.Visible = True
                    End If

                    '        s += "insert into DWH.AR.Activity_Detail " & _
                    '"(Base_ID, ActivityDate, Facility, DepositDate, Active, AssignedUser, ModifyDate, ModifyUser, CashCategory, CategoryStatus, Type, Batch_Number, WF_Cash_Received) " & _
                    '"select Base_ID, ActivityDate, Facility, DepositDate, 0, AssignedUser, getdate(), 'Splitting - ' + '" & Replace(lblSplitDetailID.Text, "'", "''") & "' " & _
                    '", CashCategory, CategoryStatus, Type, '" & Trim(Replace(txtSplitBatch_Number.Text, "'", "''")) & "', '" & Trim(Replace(txtSplitWF_Cash_Received.Text, "'", "''")) & "' " & _
                    '"from DWH.AR.Activity_Detail " & _
                    '"where Detail_ID = " & Replace(lblSplitDetailID.Text, "'", "''") & " "

                    s += "insert into DWH.AR.ActivityReport_StagingSplitting (InitialID, SplitID, STARBatchNo, AmountSplit, ActivityDate, Active) values " & _
                        "( " & Replace(lblSplitDetailID.Text, "'", "''") & ", isnull((select max(SplitID) from DWH.AR.ActivityReport_StagingSplitting where InitialID = " & _
                    Replace(lblSplitDetailID.Text, "'", "''") & "), 0) + 1, '" & Trim(Replace(txtSplitBatch_Number.Text, "'", "''")) & "' , '" & _
                    Trim(Replace(txtSplitWF_Cash_Received.Text, "'", "''")) & "', '" & Trim(Replace(txtARDate.Text, "'", "''")) & "', 1) "
                End If
            Else
                If Len(Trim(txtSplitBatch_Number.Text)) = 0 And Len(Trim(txtSplitWF_Cash_Received.Text)) = 0 Then
                    s += "delete from DWH.AR.ActivityReport_StagingSplitting where InitialID = " & Replace(lblSplitDetailID.Text, " '", "''") & _
                     " and SplitID = " & Replace(lblSplitDetail_ID.Text, "'", "''") & " "
                Else
                    If lblSplitBatch_Number.Text <> txtSplitBatch_Number.Text Or lblSplitWF_Cash_Received.Text <> txtSplitWF_Cash_Received.Text Then

                        If Integer.TryParse(txtSplitBatch_Number.Text, False) = False Then
                            lblExplanationSplit.Text = "STAR Batch No must be an integer"
                            lblExplanationSplit.Visible = True
                        End If

                        's += "update DWH.AR.Activity_Detail set Batch_Number = '" & Trim(Replace(txtSplitBatch_Number.Text, "'", "''")) & "', WF_Cash_Received = '" & _
                        'Trim(Replace(txtSplitWF_Cash_Received.Text, "'", "''")) & "' where Detail_ID = " & Replace(lblSplitDetail_ID.Text, "'", "''") & " "

                        s += "update DWH.AR.ActivityReport_StagingSplitting set STARBatchNo = '" & Trim(Replace(txtSplitBatch_Number.Text, "'", "''")) & "', AmountSplit = '" & _
                         Trim(Replace(txtSplitWF_Cash_Received.Text, "'", "''")) & "' where InitialID = " & Replace(lblSplitDetailID.Text, "'", "''") & _
                         " and SplitID = " & Replace(lblSplitDetail_ID.Text, "'", "''") & " "

                    End If



                End If


                End If

        Next

        ExecuteSql(s)


    End Sub

    Private Sub btnSubmitSplit_Click(sender As Object, e As EventArgs) Handles btnSubmitSplit.Click

        'Dim s As String = "Update DWH.AR.Activity_Detail set Active = 1, ModifyDate = getdate(), ModifyUser = '" & _
        '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where ModifyUser = 'Splitting - ' + '" & Replace(lblSplitDetailID.Text, "'", "''") & "'"

        Dim s As String = " Update s set Active = 0 from DWH.AR.ActivityReport_Splitting s left join DWH.AR.ActivityReport_StagingSplitting ss on s.SplitID = ss.SplitID and s.Active = 1 where s.InitialID = '" & _
            Replace(lblSplitDetailID.Text, "'", "''") & "' and (isnull(s.STARBatchNo, '') <> isnull(ss.STARBatchNo, '') or isnull(s.AmountSplit, 0) <> isnull(ss.AmountSplit, 0)) " & _
            "and s.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
            "insert into  DWH.AR.ActivityReport_Splitting " & _
            "select a.InitialID, STARBatchNo, AmountSplit, ActivityDate, 1, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
            "from DWH.AR.ActivityReport_StagingSplitting a " & _
            "where a.InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' and not exists (select * from DWH.AR.ActivityReport_Splitting b where  " & _
            "	a.initialID = b.InitialID and a.SplitID = b.SplitID and b.Active = 1) " & _
             " insert into DWH.AR.ActivityReport_Detail " & _
            "  select ard.ActivityID, UpdDate, 'SplitUpdate', getdate() , 1, ard.Facility, ard.CashCategory, ard.DetailStatus, ard.Type " & _
            "	, ard.BankBatchNumber, 'Split - ' + STARBatchNos, NoPatients, Cash_Received, amt, Resolve, Misc_Posted " & _
            "	, Interest, Carry_Forward, Unresolved, Locked   " & _
             " from (select sum(AmountSplit) amt, InitialID , Max(ActivityDate) as UpdDate " & _
            "		from DWH.AR.ActivityReport_Splitting   " & _
            "		where Active = 1  and InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' " & _
            "		and ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' group by InitialID) x " & _
            "		join (select distinct ST2.InitialID,  " & _
            "   substring( " & _
            "        ( " & _
            "           Select ','+ST1.STARBatchNo  AS [text()] " & _
            "            From DWH.AR.ActivityReport_Splitting ST1 " & _
            "           Where ST1.InitialID = ST2.InitialID " & _
            "			and ST1.Active = 1 and ST2.Active = 1 and ST2.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
            "           and ST1.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
            "           ORDER BY ST1.InitialID " & _
            "            For XML PATH ('') " & _
            "        ), 2, 1000) STARBatchNos " & _
            "From  DWH.AR.ActivityReport_Splitting ST2 where ST2.Active = 1  and ST2.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "') x2 on x2.InitialID = x.InitialID " & _
            "		join DWH.AR.ActivityReport_Detail ard on ard.Active = 1 and x.InitialID = ard.ActivityID and ard.ActivityDate <= UpdDate " & _
            "		and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard.ActivityID = ard2.ActivityID and ard2.ActivityDate <= UpdDate and ard2.Active = 1 " & _
            "			and (ard2.ActivityDate > ard.ActivityDate or (ard.ActivityDate = ard2.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))) " & _
            "delete a  " & _
            "from DWH.AR.ActivityReport_StagingSplitting a " & _
            "where InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' and exists (select * from DWH.AR.ActivityReport_Splitting b where  " & _
            "	a.initialID = b.InitialID and a.SplitID = b.SplitID and b.Active = 1) "


        'and ST2.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' ten lines up from here updated 5/1/2019 to deal with double-inserts on rows split across multiple Activity Dates

        '        "insert DWH.AR.ActivityReport output right(inserted.CreatedBy, len(inserted.CreatedBy) - 10), Inserted.ActivityID  " & _
        '", inserted.FirstActivityDate, 1, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' into DWH.AR.ActivityReport_Splitting " & _
        '"select ActivityDate, null, ar.DepositDate, 'Split from ' + convert(varchar, ActivityID), getdate()  " & _
        '", '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1 " & _
        '"from DWH.AR.ActivityReport_StagingSplitting a " & _
        '"join DWH.AR.ActivityReport ar on a.InitialID = ar.ActivityID and ar.Active = 1 " & _
        '"where a.InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' and not exists (select * from DWH.AR.ActivityReport_Splitting b where  " & _
        '"	a.initialID = b.InitialID and a.SplitID = b.SplitID and b.Active = 1) " & _
        '"	 " & _
        '        "" & _
        '"insert into DWH.AR.ActivityReport_Detail " & _
        '"select d.ActivityID, a.ActivityDate, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, d.Facility, d.CashCategory, d.DetailStatus, d.Type " & _
        '"	, d.BankBatchNumber, a.STARBatchNo, d.NoPatients, d.Cash_Received, d.AR_Posted, d.Resolve, d.Misc_Posted, d.Interest " & _
        '"	, d.Carry_Forward, d.Unresolved, d.Locked " & _
        '"from DWH.AR.ActivityReport_StagingSplitting a  " & _
        '"join DWH.AR.ActivityReport_Detail d on a.SplitID = d.ActivityID and a.AmountSplit <> d.Cash_Received and d.Active = 1 " & _
        '"	and d.ActivityDate <= a.ActivityDate and not exists (select * from DWH.AR.ActivityReport_Detail d2  " & _
        '"		where d2.Active = 1 and d2.ActivityID = a.InitialID and  " & _
        '"		((d2.ActivityDate = d.ActivityDate and d2.ModifiedDate > d.ModifiedDate)  " & _
        '"		or (d2.ActivityDate > d.ActivityDate and d2.ActivityDate <= a.ActivityDate)))	 " & _
        '"where a.InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' and exists (select * from DWH.AR.ActivityReport_Splitting b where " & _
        '"	a.initialID = b.InitialID and a.SplitID = b.SplitID and b.Active = 1) " & _
        '"	 " & _
        '        " " & _
        '"insert into DWH.AR.ActivityReport_Detail (ActivityID, ActivityDate, ModifiedBy, ModifiedDate, Active, Facility " & _
        '", CashCategory, DetailStatus, Type, BankBatchNumber, STARBatchNumber, Cash_Received) " & _
        '"select r.ActivityID, FirstActivityDate, r.CreatedBy + ' (' + r.ModifiedBy + ')', getdate(), 1 " & _
        '", d.Facility, d.CashCategory, d.DetailStatus, d.Type, d.BankBatchNumber, a.STARBatchNo, a.AmountSplit " & _
        '"from (select *, ROW_NUMBER() over (partition by CreatedBy order by ActivityID) rn " & _
        '"	from DWH.AR.ActivityReport where CreatedBy like 'Split from %' and Active = 1) r " & _
        '"join (select *, Row_Number() over (order by SplitID) rn  " & _
        '"	from DWH.AR.ActivityReport_StagingSplitting  where InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' and Active = 1) a  " & _
        '"		on r.CreatedBy =  'Split from ' + convert(varchar, a.InitialID) and  r.rn = a.rn " & _
        '"join DWH.AR.ActivityReport_Detail d on d.ActivityID = a.InitialID and d.Active = 1 and d.ActivityDate <= a.ActivityDate " & _
        '"	and not exists (select * from DWH.AR.ActivityReport_Detail d2 where d2.Active = 1 and d2.ActivityID = a.InitialID and  " & _
        '"		((d2.ActivityDate = d.ActivityDate and d2.ModifiedDate > d.ModifiedDate)  " & _
        '"		or (d2.ActivityDate > d.ActivityDate and d2.ActivityDate <= a.ActivityDate)))	 " & _
        '"where not exists (select * from DWH.AR.ActivityReport_Detail ad where ad.ActivityId = r.ActivityID and ad.Active = 1) " & _
        '        "	 " & _
        '"update DWH.AR.ActivityReport " & _
        '"set FinalActivityDate = ActivityDate, ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', ModifiedDate =getdate() " & _
        '"from DWH.AR.ActivityReport_StagingSplitting a " & _
        '"where a.InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "'  " & _
        '" " & _


        ExecuteSql(s)

        ReloadMainPageR()

        mpeSplit.Hide()

    End Sub

    Private Sub btnCancelSplit_Click(sender As Object, e As EventArgs) Handles btnCancelSplit.Click

        '    Dim s As String = "Update DWH.AR.Activity_Detail set WF_Cash_Received = " & lblSplitTotal.Text & ", ModifyDate = getdate(), ModifyUser = '" & _
        'Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where Detail_ID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "'   " & _
        '    "Update DWH.AR.Activity_Detail set ModifyDate = getdate(), ModifyUser = '" & _
        '        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' + ' Cancelled ' + ModifyUser where ModifyUser = 'Splitting - ' + '" & Replace(lblSplitDetailID.Text, "'", "''") & "'"

        Dim s As String = "delete a  " & _
            "from DWH.AR.ActivityReport_StagingSplitting a " & _
            "where InitialID = '" & Replace(lblSplitDetailID.Text, "'", "''") & "' "

        ExecuteSql(s)

        ReloadMainPageR()
        mpeSplit.Hide()

    End Sub

    Private Sub btnSubmitAllRows_Click(sender As Object, e As EventArgs) Handles btnSubmitAllRows.Click

        Try

    
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

            If gv_AR_MainData.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb") Then


                UpdateRow(gv_AR_MainData.DataKeys(i)("ActivityID").ToString)

                'Dim txtARBatchNo As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARBatchNo"), TextBox)
                'Dim txtARSTARBatchNo As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARSTARBatchNo"), TextBox)
                'Dim txtARNoPatients As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARNoPatients"), TextBox)
                ''Dim txtARType As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARType"), TextBox)
                'Dim ddlARType As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARType"), DropDownList)
                'Dim txtARARPosted As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARARPosted"), TextBox)
                'Dim txtARMiscPosted As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARMiscPosted"), TextBox)
                'Dim txtARInterest As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARInterest"), TextBox)
                'Dim txtARComments As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARComments"), TextBox)
                'Dim ddlARRowMisc_GL_Acct_Nos As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARRowMisc_GL_Acct_Nos"), DropDownList)
                'Dim ddlARCategory As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARCategory"), DropDownList)
                'Dim txtARMisc_GL_Acct_Nos As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARMisc_GL_Acct_Nos"), TextBox)

                'Dim cbARCarryForward As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARCarryForward"), CheckBox)
                'Dim cbARUnresolved As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARUnresolved"), CheckBox)
                'Dim cbResearch As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARResearch"), CheckBox)

                'Dim CR As String
                'Dim UR As String
                'Dim RS
                'If cbARCarryForward.Checked Then
                '    CR = "1"
                'Else
                '    CR = "null"
                'End If
                'If cbARUnresolved.Checked Then
                '    UR = "1"

                '    Dim CheckAttachments As String = "Select count(*) from DWH.AR.ActivityReport_AttachedFiles where ActivityID = '" & gv_AR_MainData.DataKeys(i)("ActivityID").ToString & "'"
                '    If GetScalar(CheckAttachments) = 0 Then
                '        explanationlabel.Text = "Attachments required when row is unresolved"
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End If

                '    If Trim(txtARComments.Text) = "" Then
                '        explanationlabel.Text = "Comments required when row is unresolved"
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End If

                'Else
                '    UR = "null"
                'End If
                'If cbResearch.Checked Then
                '    RS = "1"
                'Else
                '    RS = "null"
                'End If

                'Dim BatchNo As String
                'Dim STARBatchNo As String
                'Dim NoPats As String
                'Dim ARType As String
                'Dim ARPosted As String
                'Dim MiscPosted As String
                'Dim interest As String
                'Dim ARComments As String
                'Dim MiscGLNos As String
                'Dim DDLCategory As String
                'Dim MiscGLTxt As String

                'Dim eBatchNo As String
                'Dim eSTARBatchNo As String
                'Dim eNoPats As String
                'Dim eARType As String
                'Dim eARPosted As String
                'Dim eMiscPosted As String
                'Dim einterest As String
                'Dim eARComments As String
                'Dim eMiscGLNos As String
                'Dim eDDLCategory As String
                'Dim eMiscGLTxt As String

                'If Trim(Replace(txtARBatchNo.Text, "'", "''")) = "" Then
                '    BatchNo = "null"
                '    eBatchNo = " is null"
                'Else
                '    BatchNo = "'" & Trim(Replace(txtARBatchNo.Text, "'", "''")) & "'"
                '    eBatchNo = " = '" & Trim(Replace(txtARBatchNo.Text, "'", "''")) & "'"
                'End If
                'If Trim(Replace(txtARSTARBatchNo.Text, "'", "''")) = "" Then
                '    STARBatchNo = "null"
                '    eSTARBatchNo = " is null"
                'Else
                '    STARBatchNo = "'" & Trim(Replace(txtARSTARBatchNo.Text, "'", "''")) & "'"
                '    eSTARBatchNo = " = '" & Trim(Replace(txtARSTARBatchNo.Text, "'", "''")) & "'"
                'End If
                'If Trim(Replace(txtARNoPatients.Text, "'", "''")) = "" Then
                '    NoPats = "null"
                '    eNoPats = " is null"
                'Else
                '    NoPats = "'" & Trim(Replace(txtARNoPatients.Text, "'", "''")) & "'"
                '    eNoPats = " = '" & Trim(Replace(txtARNoPatients.Text, "'", "''")) & "'"
                'End If
                'If Trim(Replace(ddlARType.SelectedValue, "'", "''")) = "" Then
                '    ARType = "null"
                '    eARType = " is null"
                'Else
                '    ARType = "'" & Trim(Replace(ddlARType.SelectedValue, "'", "''")) & "'"
                '    eARType = " = '" & Trim(Replace(ddlARType.SelectedValue, "'", "''")) & "'"
                'End If
                'If Trim(Replace(txtARARPosted.Text, "'", "''")) = "" Then
                '    ARPosted = "null"
                '    eARPosted = " is null"
                'Else
                '    ARPosted = Trim(Replace(Replace(txtARARPosted.Text, "'", ""), ",", ""))
                '    eARPosted = " = " & Trim(Replace(Replace(txtARARPosted.Text, "'", ""), ",", ""))
                'End If
                'If Trim(Replace(txtARInterest.Text, "'", "''")) = "" Then
                '    interest = "null"
                '    einterest = " is null"
                'Else
                '    interest = Trim(Replace(Replace(txtARInterest.Text, "'", ""), ",", ""))
                '    einterest = " = " & Trim(Replace(Replace(txtARInterest.Text, "'", ""), ",", ""))
                'End If
                'If Trim(Replace(txtARComments.Text, "'", "''")) = "" Then
                '    ARComments = "null"
                '    eARComments = " is null"
                'Else
                '    ARComments = "'" & Trim(Replace(txtARComments.Text, "'", "''")) & "'"
                '    eARComments = " = '" & Trim(Replace(txtARComments.Text, "'", "''")) & "'"
                'End If
                'If Trim(Replace(txtARMisc_GL_Acct_Nos.Text, "'", "''")) = "" Then
                '    MiscGLTxt = "null"
                '    eMiscGLTxt = " is null"
                'Else
                '    MiscGLTxt = "'" & Trim(Replace(txtARMisc_GL_Acct_Nos.Text, "'", "''")) & "'"
                '    eMiscGLTxt = " = '" & Trim(Replace(txtARMisc_GL_Acct_Nos.Text, "'", "''")) & "'"
                'End If
                'If Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) = "" Then
                '    MiscGLNos = "null"
                '    eMiscGLNos = " is null"
                'ElseIf Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) = "-1" Then
                '    MiscGLNos = "null"
                '    eMiscGLNos = " is null"
                'ElseIf Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) = "11" Then
                '    If MiscGLTxt = "null" Then
                '        explanationlabel.Text = "Misc GL Text required when (Other) is selected for Misc Acct Nos"
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End If
                '    MiscGLNos = "'" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                '    eMiscGLNos = " = '" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                'Else
                '    MiscGLNos = "'" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                '    eMiscGLNos = " = '" & Trim(Replace(ddlARRowMisc_GL_Acct_Nos.SelectedValue, "'", "''")) & "'"
                'End If
                'If Trim(Replace(ddlARCategory.SelectedValue, "'", "''")) = "" Then
                '    DDLCategory = "null"
                '    eDDLCategory = " is null"
                'Else
                '    DDLCategory = "'" & Trim(Replace(ddlARCategory.SelectedValue, "'", "''")) & "'"
                '    eDDLCategory = " = '" & Trim(Replace(ddlARCategory.SelectedValue, "'", "''")) & "'"
                'End If
                'If Trim(Replace(txtARMiscPosted.Text, "'", "''")) = "" Then
                '    MiscPosted = "null"
                '    eMiscPosted = " is null"
                'Else
                '    MiscPosted = Trim(Replace(Replace(txtARMiscPosted.Text, "'", ""), ",", ""))
                '    eMiscPosted = " = " & Trim(Replace(Replace(txtARMiscPosted.Text, "'", ""), ",", ""))
                '    If MiscGLNos = "null" Then
                '        explanationlabel.Text = "Misc GL No required when Misc is posted"
                '        ModalPopupExtender.Show()
                '        Exit Sub
                '    End If

                'End If

                'UpdatesSql += "Insert into DWH.AR.Activity_Detail_History SELECT Row_ID, Detail_ID " & _
                '    "      ,Base_ID " & _
                '    "      ,ActivityDate " & _
                '    "     ,Active " & _
                '    "     ,AssignedUser " & _
                '    "     ,ModifyDate " & _
                '    "      ,ModifyUser " & _
                '    "      ,Facility " & _
                '    "      ,CashCategory " & _
                '    "      ,CategoryStatus " & _
                '    "      ,DepositDate " & _
                '    "      ,Batch_Number " & _
                '    "      ,STAR_Batch_Number " & _
                '    "      ,No_Patients " & _
                '    "      ,Type " & _
                '    "      ,WF_Cash_Received " & _
                '    "      ,AR_Posted " & _
                '    "      ,Resolve " & _
                '    "      ,Misc_Posted " & _
                '    "      ,Interest " & _
                '    "      ,Carry_Forward " & _
                '    "      ,Unresolved " & _
                '    "      ,Comments " & _
                '    "      ,Misc_GL_Acct_Nos       " & _
                '    "      ,Research       " & _
                '    "      ,Misc_GL_Text       " & _
                '    "  FROM DWH.AR.Activity_Detail " & _
                '    "  where Row_ID = " & gv_AR_MainData.DataKeys(i)("Row_ID").ToString() & _
                '    " and NOT (Batch_Number " & eBatchNo & " and STAR_Batch_Number " & eSTARBatchNo & _
                '    " and No_Patients " & eNoPats & " and Type " & eARType & " and AR_Posted " & eARPosted & " and Misc_Posted " & eMiscPosted & " and Misc_GL_Text " & eMiscGLTxt & " and Interest " & _
                '    einterest & " and isnull(Carry_Forward, 0) = isnull(" & CR & ",0) and isnull(Unresolved,0) = isnull(" & UR & ",0) and Comments " & eARComments & " and Misc_GL_Acct_Nos " & _
                '    eMiscGLNos & " and isnull(Research,0) = isnull(" & RS & ",0) and CashCategory " & eDDLCategory & ")"


                'UpdatesSql += " Update DWH.AR.Activity_Detail set Batch_Number = " & BatchNo & ", STAR_Batch_Number = " & STARBatchNo & _
                '    ", No_Patients = " & NoPats & ", Type = " & ARType & ", AR_Posted = " & ARPosted & ", Misc_Posted = " & MiscPosted & ", Misc_GL_Text = " & MiscGLTxt & ", Interest = " & _
                '    interest & ", Research = " & RS & ", Carry_Forward = " & CR & ", Unresolved = " & UR & ", Comments = " & ARComments & ", Misc_GL_Acct_Nos = " & _
                '    MiscGLNos & ", CashCategory = " & DDLCategory & ", Locked = null, ModifyDate = getdate(), ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
                '    " where Row_ID = " & gv_AR_MainData.DataKeys(i)("Row_ID").ToString() & _
                '    " and NOT (Batch_Number " & eBatchNo & " and STAR_Batch_Number " & eSTARBatchNo & _
                '    " and No_Patients " & eNoPats & " and Type " & eARType & " and AR_Posted " & eARPosted & " and Misc_Posted " & eMiscPosted & " and Misc_GL_Text " & eMiscGLTxt & " and Interest " & _
                '    einterest & " and isnull(Carry_Forward, 0) = isnull(" & CR & ",0) and isnull(Unresolved,0) = isnull(" & UR & ",0) and Comments " & eARComments & " and Misc_GL_Acct_Nos " & _
                '    eMiscGLNos & " and isnull(Research,0) = isnull(" & RS & ",0) and CashCategory " & eDDLCategory & ")"

            End If
        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        ReloadMainPageR()
        If tblOutOfBalance.Visible Then
            explanationlabel.Text = lblOutofBalance.Text
            ModalPopupExtender.Show()
        End If

        Catch ex As Exception
            explanationlabel.Text = "Error loading page.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnNetRows_Click(sender As Object, e As EventArgs) Handles btnNetRows.Click

        '    Dim s3 As String = "select distinct isnull(Facility, 'Unassigned') as Facility from DWH.AR.Activity_Detail " & _
        '"where Active = 1 and Facility is not null " & _
        '"order by 1 "

        Dim s3 As String = "select distinct substring(DisplayDescription, 1 " & _
"	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end) as FacDisplay, Facility " & _
"from DWH.AR.ActivityReport_BankAccountNumber_LU with (nolock) where Active = 1" & _
"order by 1 "

        ddlNetRowFacility.DataSource = GetData(s3)
        ddlNetRowFacility.DataValueField = "Facility"
        ddlNetRowFacility.DataTextField = "FacDisplay"
        ddlNetRowFacility.DataBind()

        PopulateNetGrid()

        mpeNettingRows.Show()
    End Sub

    Private Sub ddlNetRowFacility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNetRowFacility.SelectedIndexChanged
        PopulateNetGrid()
        mpeNettingRows.Show()
    End Sub

    Private Sub NettingView()

        Dim Balanced As String = ""
        Dim ResearchQ As String = ""

        'If cbARBalancedRows.Checked Then
        'Else
        '    Balanced += "and  (isnull(BankVariance,0) <> 0  or isnull(STARVariance,0) <> 0)  "
        'End If

        Balanced += " and isnull(Facility, 'Unassigned') in ('" & ddlNetRowFacility.SelectedValue & "') "
        Balanced += " and (isnull(UserAssigned, 'Unassigned') in ('" & ddlNetRowUser.SelectedValue & "') or '" & ddlNetRowUser.SelectedValue & "' = ' - View All - ') "
        ResearchQ += " and DetailStatus <> 'Current' "

        Dim s As String = "declare @SearchDate date = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' " & _
            "select *, abs(Cash_Received) as AbsReceived from ( " & _
"select ar.FirstActivityDate, ar.FinalActivityDate, isnull(ar.DepositDate, ar.FirstActivityDate) as SortDepositDate, " & _
" convert(varchar, isnull(ar.DepositDate, ar.FirstActivityDate), 101) as DepositDate, ad.* " & _
", case when TransferredIn is null and TransferredOut is null then 'Add Transfer'  " & _
"	else convert(varchar, isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)) end as Transfers " & _
", isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) as Transfer_Sum " & _
", case when atch.AttachId is null then 'Attach' else 'Exists' end as Attachments " & _
", case when isnull(Carry_Forward, 0) = 0 then 0 else 1 end as Carry_ForwardFlag " & _
", case when isnull(ad.Unresolved, 0) = 0 then 0 else 1 end as UnresolvedFlag " & _
", case when ad.DetailStatus = 'Current' then  " & _
"	isnull(ad.Cash_Received, 0.00) - isnull(ad.AR_Posted, 0.00) - isnull(ad.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ad.Interest, 0.00) " & _
"		- isnull(ad.Carry_Forward, 0.00) - isnull(ad.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) " & _
"	else isnull(ad.AR_Posted, 0.00) + isnull(ad.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(ad.Interest, 0.00) " & _
"		+ isnull(ad.Carry_Forward, 0.00) + isnull(ad.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) " & _
"	end as STARVariance " & _
", case when ad.DetailStatus = 'Current' then  " & _
"	null " & _
"	else isnull(ad.Cash_Received, 0.00) - isnull(ad.AR_Posted, 0.00) - isnull(ad.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ad.Interest, 0.00) " & _
"		+ isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) " & _
"	end as BankVariance " & _
", case when ad.DetailStatus = 'Research' then 1 else 0 end as ResearchFlag " & _
", UnCom.Comment as TextboxComments , isnull(isnull(isnull(assu.UserDropDownListName, assu.UserFullName), ass.UserAssigned), 'Unassigned') as AssignedDisplay , ass.UserAssigned " & _
"from DWH.AR.ActivityReport ar " & _
" left join DWH.AR.ActivityReport_Assignments ass with (nolock)  on ass.ActivityID = ar.ActivityID " & _
" left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users with (nolock)) assu   on assu.rn = 1 and ass.UserAssigned = assu.UserLogin " & _
"join (select *, ROW_NUMBER() over (partition by ActivityID order by ActivityDate desc, ModifiedDate desc) RN  from  " & _
"		DWH.AR.ActivityReport_Detail where Active = 1 and convert(date, ActivityDate) <= @SearchDate) ad on ar.ActivityID = ad.ActivityID and ad.RN = 1 " & _
"		and @SearchDate between FirstActivityDate and isnull(FinalActivityDate, '12/31/9999') " & _
" left join (select ActivityID, sum(MiscAmt) as MiscAmt from DWH.AR.ActivityReport_MiscGL where Active = 1 and ActivityDate = @SearchDate " & _
"           group by ActivityID) misc on misc.ActivityID = ar.ActivityID  " & _
"left join (select TransferFrom, sum(Amount) TransferredOut from DWH.AR.ActivityReport_Transfers " & _
"		where Active = 1 and convert(date, TransferDate) = @SearchDate " & _
"		group by TransferFrom) tff on tff.TransferFrom = ar.ActivityID  " & _
"left join (select TransferTo, sum(Amount) TransferredIn, convert(date, TransferDate) as TFTDate from DWH.AR.ActivityReport_Transfers " & _
"		where Active = 1 and convert(date, TransferDate) <= @SearchDate " & _
"		group by TransferTo, convert(date, TransferDate)) tft on tft.TransferTo = ar.ActivityID and tftDate = ad.ActivityDate " & _
"left join (select *, ROW_NUMBER() over (partition by ActivityID order by ActivityDate desc) RN  from " & _
"		DWH.AR.ActivityReport_Comments where Active = 1 and CommentType is null and convert(date, ActivityDate) <= @SearchDate) UnCom on ar.ActivityID = UnCom.ActivityID and UnCom.RN = 1 " & _
"left join (select max(AttachID) AttachID, ActivityID from DWH.AR.ActivityReport_AttachedFiles where Active = 1 group by ActivityID) atch on ar.ActivityID  = atch.ActivityID " & _
"where not exists (select * from DWH.AR.ActivityReport_Netting net where net.InitialID = ar.ActivityID and convert(date, net.ActivityDate) <= @SearchDate " & _
"	and net.ActivityDate >= ad.ActivityDate and net.Active = 1) " & _
"and not exists (select * from DWH.AR.ActivityReport_Splitting split where split.InitialID = ar.ActivityID and convert(date, split.ActivityDate) <= @SearchDate " & _
"	and split.ActivityDate >= ad.ActivityDate and split.Active = 1) " & _
"	) zq where 1=1  " & ResearchQ & Balanced & " "

        ' 7/30/2019 CRW switched "	and net.ActivityDate > ad.ActivityDate and net.Active = 1) " & _ to >= after there was an accidental double net

        'Dim s As String = "select *  from (select ad.Row_ID, ad.Detail_ID, Base_ID " & _
        '    ", Type, convert(varchar, WF_Cash_Received) WF_Cash_Received, WF_Cash_Received as NumericWF_Cash_Received, Batch_Number, STAR_Batch_Number, No_Patients, " & _
        '    "convert(varchar, AR_Posted) AR_Posted, AR_Posted as NumericAR_Posted, " & _
        '    " convert(varchar, Resolve) Resolve, Resolve as NumericResolve, convert(varchar, Misc_Posted) Misc_Posted, Misc_Posted as NumericMisc_Posted, " & _
        '    " convert(varchar, Interest) Interest, Interest as NumericInterest, " & _
        '    " convert(varchar, isnull(Carry_Forward, 0)) as Carry_Forward, Comments, Misc_GL_Acct_Nos, CONVERT(VARCHAR(10), DepositDate, 101) DepositDate, Facility, CashCategory, CategoryStatus " & _
        '    ", case when af.Detail_ID is null then 'Attach' else 'Exists' end as Attachments " & _
        '    "from DWH.AR.Activity_Detail ad " & _
        '    "left join (select max(FileID) FileID, Detail_ID from DWH.AR.Activity_Detail_AttachedFiles group by Detail_ID) af on ad.Detail_ID  = af.Detail_ID " & _
        '    "where ad.Active = 1 and ad.ActivityDate = '" & Trim(Replace(txtARDate.Text, "'", "''")) & "' ) x where 1 = 1 " & Balanced & " " & _
        '    " and not exists (Select * from DWH.AR.Activity_Netting ne where ne.InitialID = ad.Row_ID)"

        Session("NettingView") = GetData(s).DefaultView

    End Sub

    Sub PopulateNetGrid()

        Try
            NettingView()
            gvNettingRows.DataSource = Session("NettingView")
            gvNettingRows.DataBind()

        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnOKNetRows_Click(sender As Object, e As EventArgs) Handles btnOKNetRows.Click

        If GetScalar("select Count(*) From DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and cast(Value as date) = '" & Replace(txtARDate.Text, "'", "''") & "' and Active =  1") = 0 Then
            explanationlabel.Text = "Daily Processing has run - you may no longer Net Rows for '" & Replace(txtARDate.Text, "'", "''") & "'.  If you believe this is in error, please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            Exit Sub
        End If


        Dim UpdatesSql As String = ""
        Dim NetString As String = ""
        Dim CheckingSql As String = "select count(*) from ("

        For i As Integer = 0 To gvNettingRows.Rows.Count - 1

            Dim cbNetRow As CheckBox = CType(gvNettingRows.Rows(i).FindControl("cbNetRow"), CheckBox)
            If cbNetRow.Checked Then

                gvNettingRows.DataKeys(i)("ActivityID").ToString()

                'UpdatesSql += "Insert into DWH.AR.Activity_Netting SELECT Row_ID, -1 " & _
                '    "     ,Active, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                '    "  FROM DWH.AR.Activity_Detail " & _
                '    "  where Row_ID = " & gvNettingRows.DataKeys(i)("Row_ID").ToString()

                UpdatesSql += "Insert into DWH.AR.ActivityReport_Netting SELECT ActivityID, -1, '" & Trim(Replace(txtARDate.Text, "'", "''")) & "', Cash_Received " & _
                   "     ,Active, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "',  '" & Replace(txtNettingComment.Text, "'", "''") & "'" & _
                   "  FROM DWH.AR.ActivityReport_Detail ad " & _
                   "  where Active = 1 and ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & "' and ActivityID = " & gvNettingRows.DataKeys(i)("ActivityID").ToString() & _
                   " and not exists (select * from DWH.AR.ActivityReport_Detail ad2 " & _
                   " where ad2.Active = 1 and ad.ActivityID = ad2.ActivityID and ad2.ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & _
                   "' and ((ad2.ActivityDate = ad.ActivityDate and ad.ModifiedDate < ad2.ModifiedDate) or ad2.ActivityDate > ad.ActivityDate))"

                If Len(NetString) > 0 Then
                    NetString += ", "
                    CheckingSql += " union "
                End If
                NetString += gvNettingRows.DataKeys(i)("ActivityID").ToString()

                ' Added CheckingSql 2/27/2018 CRW for Safety from people changing things to Questionable Items
                CheckingSql += "select top 1 * froM DWH.AR.ActivityReport_Detail " & _
                    " where ActivityID = '" & gvNettingRows.DataKeys(i)("ActivityID").ToString() & "' order by ActivityDate desc, ModifiedDate desc "


                'UpdatesSql += " Insert into DWH.AR.Activity_Detail_History SELECT Row_ID, Detail_ID " & _
                '    "      ,Base_ID " & _
                '    "      ,ActivityDate " & _
                '    "     ,Active " & _
                '    "     ,AssignedUser " & _
                '    "     ,ModifyDate " & _
                '    "      ,ModifyUser " & _
                '    "      ,Facility " & _
                '    "      ,CashCategory " & _
                '    "      ,CategoryStatus " & _
                '    "      ,DepositDate " & _
                '    "      ,Batch_Number " & _
                '    "      ,STAR_Batch_Number " & _
                '    "      ,No_Patients " & _
                '    "      ,Type " & _
                '    "      ,WF_Cash_Received " & _
                '    "      ,AR_Posted " & _
                '    "      ,Resolve " & _
                '    "      ,Misc_Posted " & _
                '    "      ,Interest " & _
                '    "      ,Carry_Forward " & _
                '    "      ,Unresolved " & _
                '    "      ,Comments " & _
                '    "      ,Misc_GL_Acct_Nos       " & _
                '    "      ,Research       " & _
                '    "      ,Misc_GL_Text       " & _
                '    "  FROM DWH.AR.Activity_Detail " & _
                '    "  where Row_ID = " & gvNettingRows.DataKeys(i)("Row_ID").ToString() & " and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' "

                'UpdatesSql += " Update DWH.AR.ActivitY_Detail set Active = 0, ModifyDate = getdate(), ModifyUser = 'Netting; '+ '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                '    "' where Row_ID = " & gvNettingRows.DataKeys(i)("Row_ID").ToString() & _
                '    " and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' "

            End If
        Next

        CheckingSql += " ) x where ModifiedDate between dateadd(MINUTE, -15, getdate()) and getdate() and ModifiedBy <> '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

        If Len(UpdatesSql) > 0 Then
            If GetScalar(CheckingSql) > 0 Then
                explanationlabel.Text = "Somebody (other than you) updated one of these rows in the last 15 minutes.  Please confirm nothing imperative has changed on the record. "
                explanationlabel.DataBind()
                ModalPopupExtender.Show()
            End If
            ExecuteSql(UpdatesSql)

            UpdatesSql = ""
            'UpdatesSql += " Insert into DWH.AR.Activity_Detail " & _
            '"(Detail_ID, Base_ID, Facility, ActivityDate, Active, AssignedUser, ModifyDate, ModifyUser, CashCategory, CategoryStatus, Type, WF_Cash_Received, AR_Posted, Resolve, Misc_Posted, Interest, Comment) " & _
            '    "select (select max(Detail_ID) from DWH.AR.Activity_Detail) + 1, (select max(Base_ID) from DWH.AR.Activity_Detail) + 1, Facility, ActivityDate, 1, " & _
            '    "'" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
            '    "'+ 'Net - ' + '" & NetString & "' " & _
            '    ", max(CashCategory), max(CategoryStatus), 'Netting from ' + Facility, sum(WF_Cash_Received), sum(AR_Posted), sum(Resolve), sum(Misc_Posted), sum(Interest) " & _
            '    ", '" & Replace(txtNettingComment.Text, "'", "''") & "' " & _
            '    "from DWH.AR.Activity_Detail ad join DWH.AR.Activity_Netting n on ad.Row_ID = InitialID " & _
            '    "where NetID = -1 and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' and n.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'" & _
            '    " group by Facility, ActivityDate " & _
            '    " Update n set NetID = ad.Row_ID from DWH.AR.Activity_Netting n join DWH.AR.Activity_Detail ad on ad.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
            '    "'+ 'Net - ' + '" & NetString & "' " & _
            '    "where NetID = -1 and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' and n.ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'"

            UpdatesSql += "insert into DWH.AR.ActivityReport " & _
            "select distinct ActivityDate, null, null, 'Net Rows', getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1  " & _
            "from DWH.AR.ActivityReport_Netting " & _
            "where NetID = -1 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
            " " & _
            "update DWH.AR.ActivityReport_Netting " & _
            "set NetID = (select ActivityID from DWH.AR.ActivityReport ar where not exists (select *  " & _
            "		from DWH.AR.ActivityReport_Detail rd where rd.ActivityID = ar.ActivityID) " & _
            "		and ar.CreatedBy = 'Net Rows' and ar.ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') " & _
            "where NetID = -1 and ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
            "insert into DWH.AR.ActivityReport_Detail output inserted.ActivityID, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 'NetInsert' " & _
            " into DWH.AR.ActivityReport_Assignments " & _
            "select n.NetID, n.ActivityDate, 'Net Row Creation', getdate(), 1, Facility, max(CashCategory), Max(DetailStatus), NetComment " & _
            ", null, null, sum(NoPatients), sum(Cash_Received), sum(AR_Posted), Sum(Resolve), Sum(Misc_Posted), Sum(Interest), sum(Carry_Forward), Sum(Unresolved) " & _
            ", null   " & _
            "from DWH.AR.ActivityReport_Detail ad " & _
            "join DWH.AR.ActivityReport_Netting n on n.InitialID = ad.ActivityID and n.Active = 1 and ad.ActivityDate <= n.ActivityDate " & _
            "join DWH.AR.ActivityReport ar on n.NetID = ar.ActivityID and ar.Active = 1 " & _
            "where ad.Active = 1 /* added ad.Active = 1 CRW 7/17/2019 re bug from Korene */ and not exists (select * from DWH.AR.ActivityReport_Detail rd where rd.ActivityID = NetID) " & _
            " and not exists (select * from DWH.AR.ActivityReport_Detail ad2 where ad2.ActivityDate <= n.ActivityDate and ad2.ActivityID = ad.ActivityID and ad2.Active = 1 and " & _
                " (ad2.ActivityDate > ad.ActivityDate or (ad.ActivityDate = ad2.ActivityDate and ad2.ModifiedDate > ad.ModifiedDate))) " & _
            "	and ar.CreatedBy = 'Net Rows' and ar.ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
            "	group by  n.NetID, n.ActivityDate, Facility, NetComment " & _
            "insert into DWH.AR.ActivityReport_MiscGL " & _
            "Select distinct n.NetID, ST2.Misc_Identity, n.ActivityDate, " & _
            "    substring( " & _
            "        ( " & _
            "            Select '; '+ST1.Comment + ' (' + UserID +')'  AS [text()] " & _
            "            From DWH.AR.ActivityReport_MiscGL ST1 " & _
            "			join DWH.AR.ActivityReport_Netting na on ST1.ActivityId = na.InitialID and na.Active = 1  " & _
            "            Where ST1.ActivityDate = ST2.ActivityDate and " & _
            "           ST1.Misc_Identity = ST2.Misc_Identity and na.NetID = n.NetID " & _
            "            ORDER BY ST1.ActivityDate, ST1.ModifiedDate " & _
            "            For XML PATH ('') " & _
            "        ), 2, 1000) [Comments], 'Netted', getdate(), 1, null, null " & _
            "From DWH.AR.ActivityReport_MiscGL ST2 " & _
            "join DWH.AR.ActivityReport_Netting n on ST2.ActivityId = n.InitialID and n.Active = 1  " & _
            "  left join DWH.AR.ActivityReport_Detail ard on n.NetID = ard.ActivityID and ard.Active = 1 " & _
            "  and n.ActivityDate = ard.ActivityDate  " & _
            "   and not exists (select * from DWH.AR.ActivityReport_Detail ard2   " & _
            " 	where ard2.ActivityDate = n.ActivityDate and  " & _
            "	((ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))  " & _
            "   and ard.ActivityID = ard2.ActivityID and ard2.Active = 1)  " & _
            "where ST2.ActivityDate = n.ActivityDate and not exists (select * from DWH.AR.ActivityReport_MiscGL mg where mg.ActivityId = n.NetID) " & _
            "update a " & _
            "set MiscAmt = SumMiscAmt " & _
            "from DWH.AR.ActivityReport_MiscGL a " & _
            "join (select sum(MiscAmt) SumMiscAmt, NetID, convert(date, m.ActivityDate) as MiscDate from DWH.AR.ActivityReport_MiscGL m " & _
            "	join DWH.AR.ActivityReport_Netting n on m.ActivityID = n.InitialID and n.Active = 1 and m.Active = 1 " & _
            "	group by n.NetID, convert(date, m.ActivityDate)) snet on a.ActivityId = snet.NetID and convert(date, ActivityDate) = snet.MiscDate " & _
            "where a.miscAmt is null " & _
                    "/* Assingment Logging 6/11/2019 CRW */ " & _
        "insert into DWH.ar.ActivityReport_Assignments_Logging  " & _
        "select *, null, null from DWH.AR.ActivityReport_Assignments ara " & _
        "where not exists (select * from DWH.ar.ActivityReport_Assignments_Logging aral " & _
        "	where ara.ActivityID = aral.ActivityID and ara.ModifyDate = aral.AddedDate) " & _
        " " & _
        "update ar1 " & _
        "set InactivatedDate = ar2.AddedDate, InactivatedBy = ar2.addedby " & _
        "from DWH.ar.ActivityReport_Assignments_Logging ar1 " & _
        "join DWH.AR.ActivityReport_Assignments_Logging ar2 on ar1.ActivityID = ar2.ActivityID " & _
        "	and ar1.InactivatedDate is null and ar2.InactivatedDate is null and ar2.AddedDate > ar1.AddedDate " & _
            "and not exists (select * from DWH.AR.ActivityReport_Assignments_Logging ar3 where ar3.ActivityID = ar2.ActivityID and ar3.InactivatedDate is null " & _
"		and ar3.AddedDate > ar1.AddedDate and ar3.AddedDate < ar2.AddedDate ) "



            ExecuteSql(UpdatesSql)
        Else
            explanationlabel.Text = "No rows selected"
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
        End If

        ReloadMainPageR()


    End Sub

    Private Sub gvNettingRows_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvNettingRows.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = Session("NettingView")

            sorts = e.SortExpression

            If e.SortExpression = Nettingmap.Text Then

                If CInt(Nettingdir.Text) = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    Nettingdir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    Nettingdir.Text = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                Nettingdir.Text = 1
                Nettingmap.Text = e.SortExpression
            End If

            gvNettingRows.DataSource = dv
            gvNettingRows.DataBind()
            mpeNettingRows.Show()
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    'Protected Sub ddlMiscGL_SelectedIndexChanged1(sender As Object, e As EventArgs)

    '    For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

    '        Dim ddlMiscGL As DropDownList = CType(gv_AR_MainData.Rows(i).FindControl("ddlARRowMisc_GL_Acct_Nos"), DropDownList)

    '        Dim txtMiscGL As TextBox = CType(gv_AR_MainData.Rows(i).FindControl("txtARMisc_GL_Acct_Nos"), TextBox)

    '        If ddlMiscGL.SelectedValue = "11" Then
    '            txtMiscGL.Visible = True
    '        Else
    '            txtMiscGL.Visible = False
    '        End If


    '    Next


    'End Sub

    Private Sub btnNewCashJournal_Click(sender As Object, e As EventArgs) Handles btnNewCashJournal.Click

        txtNCJDepositDate.Text = GetDate("select max(Calendar_Date) from DWH.dbo.DimDate where Is_a_Public_Holiday = 0 and Is_a_Weekend_day = 0 and Calendar_Date < '" & _
                                         Replace(txtARDate.Text, "'", "''") & "'")

        'If GetScalar("select isnull(Administrator, 0) from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'") > 0 Then
        '    txtNCJDepositDate.Enabled = True
        'Else
        '    txtNCJDepositDate.Enabled = False
        'End If

        mpeNewCashJournal.Show()


    End Sub

    Private Sub gvTransfers_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransfers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddFAC As DropDownList = e.Row.FindControl("ddlTTFAC")
            Dim txtAmount As TextBox = e.Row.FindControl("txtTAmount")
            If e.Row.Cells(5).Text = "Yes" Then
                ddFAC.Enabled = True
                txtAmount.Enabled = True

            Else
                ddFAC.Enabled = False
                txtAmount.Enabled = False
            End If

            
        End If

    End Sub
    Private Sub PopupError(x As String)
        If x = "Yes" Then
            tclblErrorNCJ.BackColor = Drawing.Color.White
            tclblErrorNCJ.BorderColor = Drawing.Color.Red
            tclblErrorNCJ.BorderStyle = BorderStyle.Dashed
            tclblErrorNCJ.BorderWidth = "1"
            lblErrorNCJ.Visible = True
        Else
            tclblErrorNCJ.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            tclblErrorNCJ.BorderStyle = BorderStyle.None
            lblErrorNCJ.Visible = False
        End If
    End Sub
    Private Sub btnSubmitNewRow_Click(sender As Object, e As EventArgs) Handles btnSubmitNewRow.Click

        If ddlNCJFacility.SelectedIndex < 1 Then
            lblErrorNCJ.Text = "Please select a Facility"
            PopupError("Yes")
            mpeNewCashJournal.Show()
            Exit Sub
        End If

        If ddlNCJType.SelectedIndex < 1 Then
            lblErrorNCJ.Text = "Please select a Type"
            PopupError("Yes")
            mpeNewCashJournal.Show()
            Exit Sub
        End If

        Dim x As Decimal
        Try
            x = Decimal.Parse(txtNCJCashReceived.Text)
        Catch ex As Exception
            lblErrorNCJ.Text = "Could not parse '" & x & "' as decimal"
            PopupError("Yes")
            mpeNewCashJournal.Show()
            Exit Sub
        End Try

        If GetScalar("select count(*) from DWH.AR.ActivityReport_CashCategory_Type_LU with (nolock) " & _
            "where Type = '" & Replace(ddlNCJType.SelectedValue, "'", "''") & "' and Submittable = 0 and Active = 1") > 0 Then

            lblErrorNCJ.Text = "New Cash Journals for " & Replace(ddlNCJType.SelectedValue, "'", "''") & " should not be submitted manually; Please speak with Admin"
            PopupError("Yes")
            mpeNewCashJournal.Show()
            Exit Sub

        End If

        Dim y As String
        Dim z As Date

        If Len(Trim(txtNCJDepositDate.Text)) > 0 Then
            If Date.TryParse(Replace(Trim(txtNCJDepositDate.Text), "'", "''"), z) Then

                'If CDate(txtNCJDepositDate.Text) <= GetDate("select max(Calendar_Date) from DWH.dbo.DimDate where Is_a_Public_Holiday = 0 and Is_a_Weekend_day = 0 and Calendar_Date < '" & _
                '                 Replace(txtARDate.Text, "'", "''") & "'") Then
                '    y = "'" & Replace(Trim(txtNCJDepositDate.Text), "'", "''") & "' "
                'Else
                '    lblErrorNCJ.Text = "This is not a valid deposit date; please speak with Admin"
                '    PopupError("Yes")
                '    mpeNewCashJournal.Show()
                '    Exit Sub
                'End If

                If z > DateAdd(DateInterval.Day, -60, CDate(txtARDate.Text)) And z < CDate(txtARDate.Text) Then
                    y = "'" & Replace(Trim(txtNCJDepositDate.Text), "'", "''") & "' "
                ElseIf Admin.Text = "1" Then
                    y = "'" & Replace(Trim(txtNCJDepositDate.Text), "'", "''") & "' "
                Else
                    lblErrorNCJ.Text = "This is not a valid deposit date; please speak with Admin"
                    PopupError("Yes")
                    mpeNewCashJournal.Show()
                    Exit Sub
                End If

            Else
                lblErrorNCJ.Text = "Could not parse '" & z & "' as date"
                PopupError("Yes")
                mpeNewCashJournal.Show()
                Exit Sub
            End If

        Else
            y = "'" & Replace(txtARDate.Text, "'", "''") & "'"
        End If

        If GetScalar("select Count(*) From DWH.AR.ActivityReport_ExtraValues with (nolock) where Description = 'RunDate' and cast(Value as date) = '" & Replace(txtARDate.Text, "'", "''") & "' and Active =  1") = 0 Then
            explanationlabel.Text = "Daily Processing has run - you may no longer submit Cash Journals for '" & Replace(txtARDate.Text, "'", "''") & "'.  If you believe this is in error, please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            Exit Sub
        End If

        Dim SubmitString As String = "Insert DWH.AR.ActivityReport output Inserted.ActivityID, Inserted.FirstActivityDate, Inserted.ModifiedBy, getdate(), 1, '" & _
            Replace(ddlNCJFacility.SelectedValue, "'", "''") & "', 'Cash Journal', 'Current', '" & Replace(ddlNCJType.SelectedValue, "'", "''") & "', null, null, null, '" & _
            Trim(Replace(x, "'", "''")) & "', null, null, null, null, null, null, null into DWH.AR.ActivityReport_Detail " & _
            "Select '" & Replace(txtARDate.Text, "'", "''") & "', null, " & y & ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), '" & _
            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), 1 " & _
            "insert into DWH.AR.ActivityReport_Assignments " & _
            "select ActivityID, '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), 'AutoAssign' from " & _
            "DWH.AR.ActivityReport r where CreatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
            "and not exists (select * from DWH.AR.ActivityReport_Assignments a where r.ActivityID = a.ActivityID) " & _
                    "/* Assingment Logging 6/11/2019 CRW */ " & _
                "insert into DWH.ar.ActivityReport_Assignments_Logging  " & _
                "select *, null, null from DWH.AR.ActivityReport_Assignments ara " & _
                "where not exists (select * from DWH.ar.ActivityReport_Assignments_Logging aral " & _
                "	where ara.ActivityID = aral.ActivityID and ara.ModifyDate = aral.AddedDate) " & _
                " " & _
                "update ar1 " & _
                "set InactivatedDate = ar2.AddedDate, InactivatedBy = ar2.addedby " & _
                "from DWH.ar.ActivityReport_Assignments_Logging ar1 " & _
                "join DWH.AR.ActivityReport_Assignments_Logging ar2 on ar1.ActivityID = ar2.ActivityID " & _
                "	and ar1.InactivatedDate is null and ar2.InactivatedDate is null and ar2.AddedDate > ar1.AddedDate " & _
                    "and not exists (select * from DWH.AR.ActivityReport_Assignments_Logging ar3 where ar3.ActivityID = ar2.ActivityID and ar3.InactivatedDate is null " & _
"		and ar3.AddedDate > ar1.AddedDate and ar3.AddedDate < ar2.AddedDate ) "

        ExecuteSql(SubmitString)
        Try
            ddlARAssignedUser.SelectedValue = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
        Catch ex As Exception

        End Try
        'cbARBalancedRows.Checked = True

        For Each item As ListItem In cblARCategory.Items
            If item.Value = "Cash Journal" Then
                item.Selected = True
            End If
        Next
        For Each item As ListItem In cblARCategoryStatus.Items
            If item.Value = "Current" Then
                item.Selected = True
            End If
        Next

        PopupError("No")
        ddlNCJFacility.SelectedIndex = -1
        ddlNCJType.SelectedIndex = -1
        txtNCJCashReceived.Text = ""
        txtNCJDepositDate.Text = ""
        lblErrorNCJ.Text = ""
        lblErrorNCJ.Visible = False
        lblMiscRedFlag.Text = ""
        ReloadMainPageR()

    End Sub


    Protected Sub cbUnresolvedClicked(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

            Dim hfARUnresolved As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARUR"), Label)
            Dim cbARUnresolved As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbARUnresolved"), CheckBox)


            Dim NewRow As Integer = 0
            If cbARUnresolved.Checked And hfARUnresolved.Text = "0" Then
                hfARUnresolved.Text = "1"

                lblUnresolvedID.Text = Replace(gv_AR_MainData.DataKeys(i)("ActivityID").ToString, "'", "''")

                Dim gvUnresolvedsql As String = "select isnull(UnresolvedDropDownList, UnresolvedFullName) as Display " & _
                    " , case when UnresolvedId = 15 then 2 else 1 end as ord " & _
                    " , c.* " & _
                    " , case when CommentId is null then 0 else 1 end as Checked " & _
                    "from DWH.AR.ActivityReport_Unresolved_LU ul with (nolock) " & _
                    "left join DWH.AR.ActivityReport_Comments c with (nolock) on c.ActivityId = " & lblUnresolvedID.Text & "  " & _
                    "	and c.CommentType = ul.UnresolvedFullName and c.Active = 1 and ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                    "where ul.Active = 1 " & _
                    "order by 2, 1 "

                gvUnresolved.DataSource = GetData(gvUnresolvedsql)
                gvUnresolved.DataBind()
                mpeUnresolvedComments.Show()
                Exit Sub
            ElseIf cbARUnresolved.Checked = False And hfARUnresolved.Text = "1" Then
                hfARUnresolved.Text = "0"
            End If


        Next

    End Sub

    Protected Sub cbMiscClicked(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""
        Dim PrepSQL As String = ""


        'PrepSQL added 2/21/2018 CRW

        For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

            Dim hfARUnresolved As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARMR"), Label)
            Dim cbMisc As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbMiscPosted"), CheckBox)
            Dim lblStatus As Label = CType(gv_AR_MainData.Rows(i).FindControl("lblARCategoryStatus"), Label)

            Dim NewRow As Integer = 0
            If cbMisc.Checked And hfARUnresolved.Text = "0" Then
                hfARUnresolved.Text = "1"


                lblMiscID.Text = Replace(gv_AR_MainData.DataKeys(i)("ActivityID").ToString, "'", "''")

                'Dim gvUnresolvedsql As String = "select isnull(UnresolvedDropDownList, UnresolvedFullName) as Display " & _
                '    " , case when UnresolvedId = 15 then 2 else 1 end as ord " & _
                '    " , c.* " & _
                '    " , case when CommentId is null then 0 else 1 end as Checked " & _
                '    "from DWH.AR.ActivityReport_Unresolved_LU ul " & _
                '    "left join DWH.AR.ActivityReport_Comments c on c.ActivityId = " & lblUnresolvedID.Text & "  " & _
                '    "	and c.CommentType = ul.UnresolvedFullName and c.Active = 1 and ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                '    "where ul.Active = 1 " & _
                '    "order by 2, 1 "

                Dim gvMiscsql As String = "select isnull(DropDownDisplay, FullDisplay) as display,  " & _
                    "case when ml.Misc_Identity = 11 then 2 else 1 end as ord, " & _
                    "ml.Misc_Identity, m.miscAmt, m.Comment " & _
                    ", case when m.ActivityId is null then 0 else 1 end as Checked " & _
                    "from DWH.AR.ActivityReport_MiscGLCodes_LU ml with (nolock) " & _
                    "left join DWH.AR.ActivityReport_StagingMiscGL m with (nolock) on ml.Misc_Identity = m.Misc_Identity and m.Active = 1 and m.ActivityId = " & lblMiscID.Text & " " & _
                    "	and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                    "   where ml.Active = 1  " & _
                    " order by 2, 1 "


                gvMiscSelections.DataSource = GetData(gvMiscsql)
                gvMiscSelections.DataBind()
                mpeMiscSelections.Show()
                Exit Sub
            ElseIf cbMisc.Checked = False And hfARUnresolved.Text = "1" Then
                hfARUnresolved.Text = "0"
            End If


        Next

    End Sub


    Private Sub btnOkayUnresolvedComments_Click(sender As Object, e As EventArgs) Handles btnOkayUnresolvedComments.Click

        Dim SelectionCnt As Integer = 0
        Dim submitsql As String = ""
        For i As Integer = 0 To gvUnresolved.Rows.Count - 1

            Dim cbARUnresolved As CheckBox = CType(gvUnresolved.Rows(i).FindControl("cbUnresolvedSelect"), CheckBox)
            Dim txtUnresolved As TextBox = CType(gvUnresolved.Rows(i).FindControl("txtResponse"), TextBox)
            Dim lblSubQuestion As Label = CType(gvUnresolved.Rows(i).FindControl("lblSubQuestion"), Label)

            Dim txt As String = ""

            If cbARUnresolved.Checked Then
                SelectionCnt += 1

                If Len(Trim(txtUnresolved.Text)) = 0 Then
                    txt = " null "
                Else
                    txt = "'" & Replace(Trim(txtUnresolved.Text), "'", "''") & "' "
                End If

                If lblSubQuestion.Text = "Other" And Len(Trim(txtUnresolved.Text)) = 0 Then
                    lblOTherRequired.Visible = True
                    lblOTherRequired.Text = "Selection of 'Other' requires comment"
                    mpeUnresolvedComments.Show()
                    Exit Sub
                End If
                submitsql += "Insert into DWH.AR.ActivityReport_Comments Select '" & lblUnresolvedID.Text & "', '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                ", " & txt & ", '" & Replace(lblSubQuestion.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), 1 " & _
                " where not exists (select * from DWH.AR.ActivityReport_Comments where ActivityID = '" & lblUnresolvedID.Text & "' and CommentType = '" & Replace(lblSubQuestion.Text, "'", "''") & "' and Active = 1 ) " & _
                " Update DWH.AR.ActivityReport_Comments set Comment = " & txt & ", UserID = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                "' where ActivityID = '" & lblUnresolvedID.Text & "' and CommentType = '" & Replace(lblSubQuestion.Text, "'", "''") & "' and Active = 1 "
            Else
                submitsql += "Update DWH.AR.ActivityReport_Comments set Active = 0 where ActivityID = '" & lblUnresolvedID.Text & "' and CommentType = '" & Replace(lblSubQuestion.Text, "'", "''") & "' and Active = 1 "

            End If


        Next

        If SelectionCnt = 0 Then
            lblOTherRequired.Text = "You must select at least one reason"
            lblOTherRequired.Visible = True
            mpeUnresolvedComments.Show()
            Exit Sub
        Else
            lblOTherRequired.Visible = False
            ExecuteSql(submitsql)
        End If

    End Sub


    Private Sub gvMiscSelections_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMiscSelections.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim attch As Integer = 0
            Dim txtMiscComment As TextBox = e.Row.FindControl("txtOtherComment")
            Dim MiscIdentity As String = e.Row.Cells(0).Text

            If MiscIdentity = "11" Then
                txtMiscComment.Visible = True
            End If

        End If

    End Sub

    Private Sub btnOKMiscCodes_Click(sender As Object, e As EventArgs) Handles btnOKMiscCodes.Click

        Dim SelectionCnt As Integer = 0
        Dim submitsql As String = ""
        Dim miscAmt As Decimal
        For i As Integer = 0 To gvMiscSelections.Rows.Count - 1

            Dim miscSelection As String = gvMiscSelections.Rows(i).Cells(0).Text
            Dim cbARMisc As CheckBox = CType(gvMiscSelections.Rows(i).FindControl("cbUnresolvedSelect"), CheckBox)
            Dim txtComment As TextBox = CType(gvMiscSelections.Rows(i).FindControl("txtOtherComment"), TextBox)
            Dim txtMiscAmt As TextBox = CType(gvMiscSelections.Rows(i).FindControl("txtMiscAmt"), TextBox)
            Dim lblSubQuestion As Label = CType(gvMiscSelections.Rows(i).FindControl("lblSubQuestion"), Label)

            Dim txt As String = "null"

            If cbARMisc.Checked Then
                SelectionCnt += 1

                If miscSelection = "11" And Len(Trim(txtComment.Text)) = 0 Then
                    lblMiscRedFlag.Visible = True
                    lblMiscRedFlag.Text = "Selection of 'Other' requires comment"
                    mpeMiscSelections.Show()
                    Exit Sub
                ElseIf miscSelection = "11" Then
                    txt = "'" & Replace(txtComment.Text, "'", "''") & "'"
                End If

                Try
                    miscAmt = Decimal.Parse(txtMiscAmt.Text)
                Catch ex As Exception
                    lblMiscRedFlag.Visible = True
                    lblMiscRedFlag.Text = "Could not parse '" & txtMiscAmt.Text & "' as decimal"
                    mpeMiscSelections.Show()
                    Exit Sub
                End Try

                submitsql += " Update DWH.AR.ActivityReport_StagingMiscGL set Active = 0 " & _
                    " where ActivityID = '" & lblMiscID.Text & "' and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 and (isnull(Comment, '') <> isnull(" & txt & ", '') " & _
                    " or MiscAmt <> '" & miscAmt & "') and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                    "Insert into DWH.AR.ActivityReport_StagingMiscGL Select '" & lblMiscID.Text & "', '" & Replace(miscSelection, "'", "''") & "',  '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                    ", " & txt & ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), 1 , '" & Replace(miscAmt.ToString, "'", "''") & "', null " & _
                    " where not exists (select * from DWH.AR.ActivityReport_StagingMiscGL where ActivityID = '" & lblMiscID.Text & "' and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                    " and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 ) "

                ' Flipped to StagingMiscGL table 2/21/2018 CRW for better user interface
                '            submitsql += " Update DWH.AR.ActivityReport_MiscGL set Active = 0 " & _
                '" where ActivityID = '" & lblMiscID.Text & "' and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 and (isnull(Comment, '') <> isnull(" & txt & ", '') " & _
                '" or MiscAmt <> '" & miscAmt & "') and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                '"Insert into DWH.AR.ActivityReport_MiscGL Select '" & lblMiscID.Text & "', '" & Replace(miscSelection, "'", "''") & "',  '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                '", " & txt & ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), 1 , '" & Replace(miscAmt.ToString, "'", "''") & "', null " & _
                '" where not exists (select * from DWH.AR.ActivityReport_MiscGL where ActivityID = '" & lblMiscID.Text & "' and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                '" and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 ) "


                'Altered 2/21/2018 CRW for better tracking
                'submitsql += "Insert into DWH.AR.ActivityReport_MiscGL Select '" & lblMiscID.Text & "', '" & Replace(miscSelection, "'", "''") & "',  '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                '", " & txt & ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate(), 1 , '" & Replace(miscAmt.ToString, "'", "''") & "', null " & _
                '" where not exists (select * from DWH.AR.ActivityReport_MiscGL where ActivityID = '" & lblMiscID.Text & "' and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 ) " & _
                '" Update DWH.AR.ActivityReport_MiscGL set Comment = " & txt & ", MiscAmt = '" & Replace(miscAmt.ToString, "'", "''") & "', UserID = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '"' where ActivityID = '" & lblMiscID.Text & "' and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 "
            Else
                submitsql += "Update DWH.AR.ActivityReport_StagingMiscGL set Active = 0 where ActivityID = '" & lblMiscID.Text & "' and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                    " and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 "

                'Staging Table 2/21/2018 CRW
                '           submitsql += "Update DWH.AR.ActivityReport_MiscGL set Active = 0 where ActivityID = '" & lblMiscID.Text & "' and ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "' " & _
                '" and Misc_Identity = '" & Replace(miscSelection, "'", "''") & "' and Active = 1 "

            End If


        Next

        'If Len(submitsql) > 0 Then
        '    submitsql += " insert into DWH.AR.ActivityReport_Detail " & _
        '    "  select ard.ActivityID, '" & Replace(txtARDate.Text, "'", "''") & "', 'MiscGLUpdate', getdate() , 1, ard.Facility, ard.CashCategory, ard.DetailStatus, ard.Type " & _
        '    "	, ard.BankBatchNumber, STARBatchNumber, NoPatients, Cash_Received, AR_Posted, Resolve, " & _
        '    "(select sum(MiscAmt) from DWH.AR.ActivityReport_MiscGL where Active = 1 and ActivityID = '" & _
        '           lblMiscID.Text & "' and  ActivityDate = '" & Replace(txtARDate.Text, "'", "''") & "') " & _
        '    "	, Interest, Carry_Forward, Unresolved, Locked   " & _
        '     " from  DWH.AR.ActivityReport_Detail ard where ard.Active = 1 and '" & lblMiscID.Text & "' = ard.ActivityID and ard.ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & "' " & _
        '    "		and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard.ActivityID = ard2.ActivityID and ard2.ActivityDate <= '" & Replace(txtARDate.Text, "'", "''") & "' and ard2.Active = 1 " & _
        '    "			and (ard2.ActivityDate > ard.ActivityDate or (ard.ActivityDate = ard2.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate))) "

        '    'Written 2/21/2018 CRW to resolve bug; decided it's not to be used, as switching to Staging table

        '    'Removed 2/21/2018, because it's extraneous. 
        '    'submitsql += "update art set CurrentFlag = case when DetailStatus = 'Current' then 1 else 0 end " & _
        '    '"from DWH.AR.ActivityReport_MiscGL art " & _
        '    '"left join DWH.AR.ActivityReport_Detail ard on art.ActivityID = ard.ActivityID and ard.Active = 1 " & _
        '    '" and art.ActivityDate = ard.ActivityDate " & _
        '    '"and not exists (select * from DWH.AR.ActivityReport_Detail ard2  " & _
        '    '"	where ard2.ActivityDate = art.ActivityDate and (ard2.ActivityDate > ard.ActivityDate or (ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate)) " & _
        '    '"	 and ard.ActivityID = ard2.ActivityID and ard2.Active = 1) " & _
        '    '"where art.CurrentFlag is null "
        'End If

        ExecuteSql(submitsql)
        If SelectionCnt = 0 Then

            For i As Integer = 0 To gv_AR_MainData.Rows.Count - 1

                Dim cbMisc As CheckBox = CType(gv_AR_MainData.Rows(i).FindControl("cbMiscPosted"), CheckBox)
             
                If lblMiscID.Text = Replace(gv_AR_MainData.DataKeys(i)("ActivityID").ToString, "'", "''") Then
                    cbMisc.Checked = False
                End If

            Next


            Exit Sub
        End If
        lblOTherRequired.Visible = False

        'End If
    End Sub

    Private Sub btnRunDailyProcessing_Click(sender As Object, e As EventArgs) Handles btnRunDailyProcessing.Click

        If txtARDate.Text >= Today() Then
            explanationlabel.Text = "You may not run Daily Processing until tomorrow."
            ModalPopupExtender.Show()
            Exit Sub
        End If

        Dim ProcessCheck As String = "declare @RunDate date = (select Value from DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and Active = 1) " & _
            "select isnull(sum(isnull(ARPosted, 0) - isnull(AR, 0)), 0) as ARVariance " & _
            "from ( " & _
            "select isnull(STARBatchNo, STARBatchNumber) as STARBN, sum(NoPatients) as ToolPats, sum(isnull(AmountSplit, AR_Posted)) as AR, Facility " & _
            " from DWH.AR.ActivityReport ar " & _
            "left join DWH.AR.ActivityReport_Splitting sl on ar.ActivityID = sl.InitialID and sl.Active = 1 and sl.ActivityDate = @RunDate " & _
            "join DWH.AR.ActivityReport_Detail ard on ar.ActivityID = ard.ActivityID and ard.Active = 1 " & _
            "	and ard.ActivityDate <= @RunDate " & _
            "	and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.ActivityID = ard.ActivityID and ard2.Active = 1 " & _
            "					and ((ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate) )) " & _
            "where ar.Active = 1 and ard.ActivityID not in (8003, 14269) and ard.ActivityDate = @RunDate and AR_Posted is not null and isnull(AmountSplit, AR_Posted) <> 0 " & _
            "and not exists (select * from DWH.AR.ActivityReport_Netting arn where arn.InitialID = ar.ActivityID and arn.Active = 1 and arn.ActivityDate <= @RunDate) " & _
            "group by isnull(STARBatchNo, STARBatchNumber) , Facility " & _
            ") a full join " & _
            "(select Batch_NO, FAC , count(*) NoPats, sum(Trans_Amt) ARPosted from DWH.STAR.FAR130 where FAR130_Date  = @RunDate " & _
            "group by Batch_NO , FAC) b on a.Facility = b.FAC and a.STARBN = b.BATCH_NO " & _
            "where  isnull(a.AR, 0) <> isnull(b.ARPosted, 0) "

        If GetDecimal(ProcessCheck) <> 0 Then
            explanationlabel.Text = "AR Posted does not match FAR130; please check the FAR 130 Variances"
            ModalPopupExtender.Show()
            Exit Sub
        End If


        Dim ProcessCheck3 As String = "declare @RunDate date = (select Value from DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and Active = 1) " & _
            "select isnull(sum(isnull(ARPosted, 0) - isnull(AR, 0)), 0) as ARVariance " & _
            "from ( " & _
            "select sum(AR_Posted) as AR, Facility" & _
            " from DWH.AR.ActivityReport ar " & _
            "join DWH.AR.ActivityReport_Detail ard on ar.ActivityID = ard.ActivityID and ard.Active = 1 " & _
            "	and ard.ActivityDate <= @RunDate " & _
            "	and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.ActivityID = ard.ActivityID and ard2.Active = 1 " & _
            "					and ((ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate) )) " & _
            "where ar.Active = 1 and ard.ActivityID not in (8003, 14269) and ard.ActivityDate = @RunDate and AR_Posted is not null and AR_Posted <> 0 " & _
            "and not exists (select * from DWH.AR.ActivityReport_Netting arn where arn.InitialID = ar.ActivityID and arn.Active = 1 and arn.ActivityDate <= @RunDate) " & _
            "group by Facility " & _
            ") a full join " & _
            "(select FAC , count(*) NoPats, sum(Trans_Amt) ARPosted from DWH.STAR.FAR130 where FAR130_Date  = @RunDate " & _
            "group by  FAC) b on a.Facility = b.FAC " & _
            "where  isnull(a.AR, 0) <> isnull(b.ARPosted, 0) "

        If GetDecimal(ProcessCheck3) <> 0 Then
            explanationlabel.Text = "Overall AR Posted does not match FAR130; PLEASE CONTACT WEB ADMIN!"
            ModalPopupExtender.Show()
            Exit Sub
        End If

        Dim ProcessCheck2 As String = "declare @RunDate date = (select Value from DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and Active = 1) " & _
            "select count(*) from ( " & _
            "select case when ard.DetailStatus = 'Current' then   " & _
            "	isnull(ard.Cash_Received, 0.00) - isnull(ard.AR_Posted, 0.00) - isnull(ard.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ard.Interest, 0.00)  " & _
            "		- isnull(ard.Carry_Forward, 0.00) - isnull(ard.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)  " & _
            "	else isnull(ard.AR_Posted, 0.00) + isnull(ard.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(ard.Interest, 0.00)  " & _
            "		+ isnull(ard.Carry_Forward, 0.00)  - isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00)  " & _
            "	end as STARVariance ,  " & _
            "	isnull(ard.AR_Posted, 0.00) + isnull(ard.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(ard.Interest, 0.00)  " & _
            "		+ isnull(ard.Carry_Forward, 0.00) + isnull(ard.Unresolved, 0.00) + isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00)  " & _
            "		as CurrentMessedWith, ard.* " & _
            "		, isnull(isnull(isnull(assu.UserDropDownListName, assu.UserFullName), ass.UserAssigned), 'Unassigned') as AssignedDisplay " & _
            "		, isnull(isnull(isnull(modu.UserDropDownListName, modu.UserFullName), ard.ModifiedBy), 'Unassigned') as ModifiedByDisplay " & _
            " from DWH.AR.ActivityReport ar " & _
            "left join DWH.AR.ActivityReport_Splitting sl on ar.ActivityID = sl.InitialID and sl.Active = 1 and sl.ActivityDate <= @RunDate " & _
            "join DWH.AR.ActivityReport_Detail ard on ar.ActivityID = ard.ActivityID and ard.Active = 1 " & _
            "	and ard.ActivityDate <= @RunDate " & _
            "	and not exists (select * from DWH.AR.ActivityReport_Detail ard2 where ard2.ActivityID = ard.ActivityID and ard2.Active = 1 " & _
            "					and ((ard2.ActivityDate = ard.ActivityDate and ard2.ModifiedDate > ard.ModifiedDate) )) " & _
            "left join (select TransferFrom, sum(Amount) TransferredOut from DWH.AR.ActivityReport_Transfers  " & _
            "	where Active = 1 and convert(date, TransferDate) = @RunDate " & _
            "	group by TransferFrom) tff on tff.TransferFrom = ar.ActivityID " & _
            "left join (select TransferTo, sum(Amount) TransferredIn, convert(date, TransferDate) as TFTDate from DWH.AR.ActivityReport_Transfers  " & _
            "	where Active = 1 and convert(date, TransferDate) = @RunDate  " & _
            "	group by TransferTo, convert(date, TransferDate)) tft on tft.TransferTo = ar.ActivityID and TFTDate = ard.ActivityDate  " & _
            "left join (select ActivityID, sum(MiscAmt) as MiscAmt from   " & _
            "	DWH.AR.ActivityReport_MiscGL where Active = 1 and convert(date, ActivityDate) = @RunDate group by ActivityID) mgl on ar.ActivityID = mgl.ActivityID  " & _
            "	  " & _
            "left join DWH.AR.ActivityReport_Assignments ass on ass.ActivityID = ar.ActivityID  " & _
            "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) assu on assu.rn = 1 and ass.UserAssigned = assu.UserLogin  " & _
            "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) modu on modu.rn = 1 and ard.ModifiedBy = modu.UserLogin  " & _
            "where ar.Active = 1 and ard.ActivityDate = @RunDate and ar.ActivityID not in (142690) " & _
            ") x where STARVariance <> 0 and  (DetailStatus <> 'Current' or CurrentMessedWith <> 0) "


        If GetDecimal(ProcessCheck2) <> 0 Then
            explanationlabel.Text = "STAR Variances persist; please check the SSRS Report for identification"
            ModalPopupExtender.Show()
            Exit Sub
        End If

        If GetScalar("select count(*) from DWH.WF.vw_WFAllActivity where LoadDate = (select dateadd(day, 1, Value) from DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and Active = 1)") = 0 Then
            Dim x As String = Trim(GetString("select Week_Day_Name from DWH.dbo.DimDate " & _
                    "where Calendar_Date = (select dateadd(day, 1, Value) from DWH.AR.ActivityReport_ExtraValues where Description = 'RunDate' and Active = 1)"))
            lblExplainNoData.Text = "There is no WF All Activity Data for the next day (" & x & ").  Run Daily Processing anyways?"
            mpeConfirmNoData.Show()
        Else
            srsExplanationLabel.Text = "If you run Daily Processing for " & txtARDate.Text & " you will not be able to change anything that happened on this date."
            mpeSerious.Show()
        End If




    End Sub

    Private Sub btnConfirmSrs_Click(sender As Object, e As EventArgs) Handles btnConfirmSrs.Click
        Try
            If GetScalar("select count(*) from DWH.AR.ActivityReport_ExtraValues where Description = 'KickedOff' and Active = 1") > 0 Then
                explanationlabel.Text = "Daily Processing is already running"
                ModalPopupExtender.Show()
                Exit Sub
            End If
            srsExplanationLabel.Text = "Processing"
            srsExplanationLabel.DataBind()
            ExecuteSql("Insert into  DWH.AR.ActivityReport_ExtraValues (Value, Description, ModifiedBy, ModifiedDate, Active) select '" & _
                       Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 'KickedOff', 'Automated', getdate(), 1")
            ExecuteSql("exec DWH.AR.usp_DailyProcessing ")
            RefreshDates()
            ARDetailView()
            gv_AR_MainData.DataSource = Session("ARDetailView") ' ARDetailView()
            gv_AR_MainData.DataBind()
            BalanceCheck()
            mpeSerious.Hide()

        Catch ex As Exception
            explanationlabel.Text = "Error Conducting Daily Processing.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub PopulateTestTypes()
        If Trim(txtAssignUserKeyword.Text) = "" Then
            lblFYIAssignments.Visible = False
            gvAssignUserConflicts.Visible = False
            Exit Sub
        End If
        Dim testsql As String = "declare @TestType varchar(max) = '" & Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "' " & _
            "select top 10 * " & _
            ", ROW_NUMBER() over (partition by CurrentKeyword order by RNOrder) as RNOrder2 " & _
            " from ( " & _
            "select distinct ard.Type as OriginalType,  ur1.Type as OldKeyword, ur1.UserLogin " & _
            ", case when len(@TestType) > len(ur1.Type) then @TestType  " & _
            "when len(@TestType) < len(ur1.Type) then ur1.Type " & _
            "when CHARINDEX(ur1.Type, ard.Type) > CHARINDEX(@TestType, ard.Type) then @TestType " & _
            "when CHARINDEX(ur1.Type, ard.Type) < CHARINDEX(@TestType, ard.Type) then ur1.Type " & _
            "when @TestType = ur1.Type then 'Type already Exists' " & _
            "else 'Error???' " & _
            "end as CurrentKeyword, isnull(isnull(u.UserDropDownListName, u.UserFullName), ur1.UserLogin) as OldAssignedUser " & _
            ", ROW_NUMBER() over (partition by ur1.Type order by len(ard.Type)) as RNOrder " & _
            "from DWH.AR.ActivityReport_Detail ard " & _
            "join DWH.AR.ActivityReport_User_Responsibility ur1 on ard.Type like '%' + ur1.Type + '%'			 " & _
            "			and not exists (select * from DWH.AR.ActivityReport_User_Responsibility ur4 where ard.Type like '%' + ur4.Type + '%' " & _
            "			and (len(ur4.Type ) > len(ur1.Type) or ( len(ur4.Type ) = len(ur1.Type) and CHARINDEX(ur1.Type, ard.Type) > CHARINDEX(ur4.Type, ard.Type)))) " & _
            " left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) u on u.rn=1 and ur1.UserLogin = u.UserLogin " & _
            "where ard.Type like '%' + @TestType + '%' " & _
            "and  ard.Active = 1  " & _
            ") x " & _
            "order by RNOrder2, RNOrder "

        gvAssignUserConflicts.DataSource = GetData(testsql).DefaultView
        gvAssignUserConflicts.DataBind()

        If gvAssignUserConflicts.Rows.Count() > 0 Then
            lblFYIAssignments.Visible = True
            gvAssignUserConflicts.Visible = True
        Else
            lblFYIAssignments.Visible = False
            gvAssignUserConflicts.Visible = False
        End If

    End Sub

    Private Sub PopulateCurrentAssignments()

        Dim SQL As String = "select ur.Type, isnull(isnull(u.UserDropDownListName, u.UserFullName), ur.UserLogin) as UserAssigned " & _
            ", ur.UserLogin, 'Remove' as Clicky " & _
            "from DWH.AR.ActivityReport_User_Responsibility ur " & _
            "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) u on u.rn = 1 and ur.UserLogin = u.UserLogin " & _
            "where ur.Active = 1 and Type like '%" & Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "%' "

        If cbAdminActiveAssignments.Checked Then
            SQL += "union select Type, UserAssigned, UserLogin, CLicky from ( " & _
                "select ur.Type, isnull(isnull(u.UserDropDownListName, u.UserFullName), ur.UserLogin) as UserAssigned  " & _
                ", ur.UserLogin, 'Reactivate' as Clicky, ur.Active " & _
                ", ROW_NUMBER() over (partition by Type order by ur.ModifyDate desc) RN " & _
                " from DWH.AR.ActivityReport_User_Responsibility ur " & _
                "left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) u on u.rn = 1 and ur.UserLogin = u.UserLogin  " & _
                " ) x where  RN = 1 and  Active = 0 and Type like '%" & Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "%' "
        End If

        SQL += " order by Type"

        gvAssignments.DataSource = GetData(SQL).DefaultView
        gvAssignments.DataBind()

    End Sub

    Private Sub PopulateAssignmentDropDown()

        Dim SQL As String = "select isnull(UserDropDownListName, UserFullName) as DisplayName, UserLogin from DWH.AR.ActivityReport_Users " & _
            "where Assignable = 1 and Active = 1 order by 1 "

        ddlAssignUserSelection.DataSource = GetData(SQL)
        ddlAssignUserSelection.DataTextField = "DisplayName"
        ddlAssignUserSelection.DataValueField = "UserLogin"
        ddlAssignUserSelection.DataBind()


    End Sub

    Private Sub PopulateBAICategoryDropDown()

        Dim SQL As String = "select CashCategory, 1 as ord from DWH.AR.ActivityReport_CashCategory " & _
            "where Active = 1 union select '(Select Category)', 0 order by 2, 1"

        ddlAdminBAICategories.DataSource = GetData(SQL)
        ddlAdminBAICategories.DataTextField = "CashCategory"
        ddlAdminBAICategories.DataValueField = "CashCategory"
        ddlAdminBAICategories.DataBind()


    End Sub


    Private Sub PopulateBAITypeCodes()
        Dim SQL As String = "select *, case when Active = 1 then 'Remove Code' else 'Reactivate' end as Clicky from " & _
            "(select *, Row_Number() over (partition by BAITypeCode order by Active desc, ModifiedDate desc) RN froM DWH.AR.ActivityReport_BAITypeCodes_LU " & _
            ") x where RN = 1 and (Activity_Category = '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & _
            "' or '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & "' = '(Select Category)') "

        If cbAdminBAICodes.Checked = False Then
            SQL += " and Active = 1 "
        End If


        gvBAITypeCodes.DataSource = GetData(SQL).DefaultView
        gvBAITypeCodes.DataBind()

    End Sub

    Private Sub PopulateBankAccountNumbers()
        Dim SQL As String = "select *, case when Active = 1 then 'Remove' else 'Reactivate' end as Clicky from (" & _
            "select *, case when Facility = 'A' then 'Atlanta' when Facility  = 'C' then 'Cherokee' when Facility = 'F' then 'Forsyth' " & _
            "when Facility = 'D' then 'Duluth' when Facility = 'L' then 'Gwinnett' else Facility end as FacFull " & _
            ", Row_Number() over (partition by AccountNumber order by Active desc, ModifiedDate desc) RN  froM DWH.AR.ActivityReport_BankAccountNumber_LU " & _
            ") x where RN = 1 "

        If cbAdminBANs.Checked = False Then
            SQL += " and Active = 1 "
        End If

        gvAdminBankAcctNos.DataSource = GetData(SQL).DefaultView
        gvAdminBankAcctNos.DataBind()

    End Sub

    Private Sub PopulateCJTypes()
        Dim SQL As String = "select *, case when Active = 1 then 'Remove' else 'Reactivate' end as Clicky from (select *, isnull(DropDownListDisplay, isnull(left(FullDisplay, 12), Type)) as ShortDisplay " & _
            ", case when Submittable = 1 then 'Deny Submission' " & _
            "	else 'Allow Submission' end as Submit " & _
            ", Row_Number() over (partition by Type order by Active desc, ModifiedDate desc) RN from DWH.AR.ActivityReport_CashCategory_Type_LU where CashCategory = 'Cash Journal' ) x where RN = 1 "


        If cbAdminCJType.Checked = False Then
            SQL += " and Active = 1 "
        End If

        gvAdminCJTypes.DataSource = GetData(SQL).DefaultView
        gvAdminCJTypes.DataBind()

    End Sub

    Private Sub PopulateLBCodes()
        Dim SQL As String = "select *, case when Active = 1 then 'Remove' else 'Reactivate' end as Clicky, case when LDRollup = 1 then 'Yes' else 'No' end as Rollup " & _
            "from (select *, Row_Number() over (partition by LockboxCode order by Active desc, ModifiedDate desc) RN  from DWH.AR.ActivityReport_LockboxCodes_LU) x where RN = 1 "

        If cbAdminLockboxTypes.Checked = False Then
            SQL += " and Active = 1 "
        End If

        gvAdminLBTypes.DataSource = GetData(SQL).DefaultView
        gvAdminLBTypes.DataBind()

    End Sub

    Private Sub PopulateMiscGLCodes()
        Dim SQL As String = "select *, case when Active = 1 then 'Remove' else 'Reactivate' end as Clicky " & _
            " from DWH.AR.ActivityReport_MiscGLCodes_LU where Misc_Identity <> 11 "
        If cbAdminMiscActive.Checked = False Then
            SQL += " and Active = 1 "
        End If


        gvAdminMiscGL.DataSource = GetData(SQL).DefaultView
        gvAdminMiscGL.DataBind()

    End Sub

    Private Sub PopulateAdminUnresolved()
        Dim SQL As String = "select *, case when Active = 1 then 'Remove' else 'Reactivate' end as Clicky  from DWH.AR.ActivityReport_Unresolved_LU where UnresolvedId <> 15 "

        If cbUnresolvedActive.Checked = False Then
            SQL += " and Active = 1 "
        End If

        gvAdminUnresolvedReasons.DataSource = GetData(SQL).DefaultView
        gvAdminUnresolvedReasons.DataBind()

    End Sub

    Private Function UsersView()

        Dim SQL As String = "select UserLogin, UserFullName, isnull(UserDropDownListName, left(UserFullName, 10)) as DisplayName " & _
    ", case when Administrator = 1 then 'Revoke Admin' " & _
    "	else 'Make Admin' end as Admin " & _
    ", case when MultipleLocks is null then 'No Limit' " & _
    "	else convert(varchar, MultipleLocks) end as LockLimit " & _
    ", case when Netting = 1 then 'Allowed' " & _
    "	else 'Denied' end as Netting " & _
    ", case when Research = 1 then 'Allowed' " & _
    "	else 'Denied' end as Researching " & _
    ", case when AssignRights = 1 then 'Granted' " & _
    "	else 'Denied' end as AssignRights " & _
    ", case when AssignRoles = 1 then 'Granted' " & _
    "	else 'Denied' end as AssignRoles " & _
    ", MultipleLocks " & _
    ", case when Active = 1 then 'Remove' else 'Reactivate' end as Clicky " & _
    ", case when RunProcessing = 1 then 'Granted' " & _
    "	else 'Denied' end as RunProcessing " & _
    ", case when ResetRights = 1 then 'Granted' " & _
    "	else 'Denied' end as ResetRights " & _
     ", case when HolidayRights = 1 then 'Granted' " & _
    "	else 'Denied' end as HolidayRights, EmailAddress " & _
    "from (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) u " & _
    " where rn = 1 "
        If cbAdminUserActives.Checked = False Then
            SQL += " and Active = 1 "
        End If

        Return GetData(SQL).DefaultView

    End Function

    

    Private Sub PopulateAdmiUsers()


        Dim x As DataView = UsersView()
        userssort.Text = "UserLogin ASC"
        x.Sort = "UserLogin ASC"
        gvAdminUsers.DataSource = x
        gvAdminUsers.DataBind()




    End Sub

    Private Sub txtAssignUserKeyword_TextChanged(sender As Object, e As EventArgs) Handles txtAssignUserKeyword.TextChanged
        PopulateTestTypes()
        PopulateCurrentAssignments()
    End Sub

    Private Sub btnAddAssignment_Click(sender As Object, e As EventArgs) Handles btnAddAssignment.Click

        If Trim(txtAssignUserKeyword.Text) = "" Then
            explanationlabelAdmin.Text = "No Keyword entered"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from DWH.AR.ActivityReport_User_Responsibility where Type = '" & Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This keyword is already assigned"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String
        If GetScalar("Select count(*) from DWH.AR.ActivityReport_User_Responsibility where Type = '" & Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "' and Active = 0") > 0 Then
            SQL = "update DWH.AR.ActivityReport_User_Responsibility set Active = 1, ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                "', UserLogin = '" & Replace(ddlAssignUserSelection.SelectedValue, "'", "''") & "' where Type = '" & _
                Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "'"
        Else
            SQL = "Insert into DWH.AR.ActivityReport_User_Responsibility values ('" & Replace(ddlAssignUserSelection.SelectedValue, "'", "''") & "', '" & _
                Trim(Replace(txtAssignUserKeyword.Text, "'", "''")) & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "')"
        End If

        ExecuteSql(SQL)
        txtAssignUserKeyword.Text = ""
        PopulateTestTypes()
        PopulateCurrentAssignments()

    End Sub

    Private Sub gvAssignments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAssignments.RowCommand
 
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            'If sd = 1 Then
            '    se = se + " " + "desc"
            'Else
            '    se = se + " " + "asc"
            'End If

            If Commander = "RemoveAssignation" Then

                'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_User_Responsibility set Active = 0, ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '    "' where Type = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1 "

                Dim PrepSQL As String = "with cte as ( " & _
                    "select top 1 * from DWH.AR.ActivityReport_User_Responsibility " & _
                    "where TYPE = '" & Replace(Detail_ID, "'", "''") & "' " & _
                    "order by Active desc,  ModifyDate desc) " & _
                    "update cte set Active = -Active + 1, ModifyDate = getdate(), ModifyUser =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                PopulateCurrentAssignments()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub gvAssignments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAssignments.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim lblAssignedTo As Label = e.Row.FindControl("lblAssignedTo")
            Dim ddlAssignedTo As DropDownList = e.Row.FindControl("ddlAssignedTo")

            Dim AvailableUsers As String = "select isnull(isnull(u.UserDropDownListName, u.UserFullName), u.UserLogin) as UserDisplay, UserLogin, 1 as ord " & _
                "from DWH.AR.ActivityReport_Users u where Active = 1 and Assignable = 1 " & _
                "union " & _
                "select isnull(isnull(u.UserDropDownListName, u.UserFullName), u.UserLogin) + ' (Inactive)' as UserDisplay, UserLogin, 0  " & _
                "from (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.ar.ActivityReport_Users) u where rn = 1 and Active = 0 and UserLogin = '" & Replace(lblAssignedTo.Text, "'", "''") & "' "

            ddlAssignedTo.DataSource = GetData(AvailableUsers)
            ddlAssignedTo.DataTextField = "UserDisplay"
            ddlAssignedTo.DataValueField = "UserLogin"
            ddlAssignedTo.DataBind()
            Try
                ddlAssignedTo.SelectedValue = lblAssignedTo.Text
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub ddlAssignedUser_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvAssignments.Rows.Count - 1

            Dim lblAssignedTo As Label = CType(gvAssignments.Rows(i).FindControl("lblAssignedTo"), Label)
            Dim ddlAssignedTo As DropDownList = CType(gvAssignments.Rows(i).FindControl("ddlAssignedTo"), DropDownList)
            Dim lblAssignedType As Label = CType(gvAssignments.Rows(i).FindControl("lblAssignedType"), Label)

            Dim NewRow As Integer = 0
            If ddlAssignedTo.SelectedValue <> lblAssignedTo.Text Then
                'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
                UpdatesSql += "Update DWH.AR.ActivityReport_User_Responsibility " & _
                    "set UserLogin = '" & Replace(ddlAssignedTo.SelectedValue, "'", "''") & "' , ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                    "where Type = '" & Replace(lblAssignedType.Text, "'", "''") & "' and Active = 1 "
            End If

        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If
    End Sub

    Private Sub ddlManageWhat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageWhat.SelectedIndexChanged
        '<asp:ListItem Text="Assignments" Value="User_Responsibility"></asp:ListItem>
        '                              <asp:ListItem Text="BAI Type Codes" Value="BAITypeCodes_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Bank Account Numbers" Value="BankAccountNumber_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Cash Journal Types" Value="CashCategory_Type_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Lockbox Codes" Value="LockboxCodes_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Misc GL Codes" Value="MiscGLCodes_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Overrides" Value="Overrides_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Unresolved Reasons" Value="Unresolved_LU"></asp:ListItem>
        '                              <asp:ListItem Text="Users" Value="Users"></asp:ListItem>

        pnlAssignments.Visible = False
        pnlBAITypeCodes.Visible = False
        pnlAdminBankAccountNumbers.Visible = False
        pnlAdminCashJournalTypes.Visible = False
        pnlAdminLockBoxCodes.Visible = False
        pnlAdminMiscGLCodes.Visible = False
        pnlAdminOverrides.Visible = False
        pnlAdminUnresolvedReasons.Visible = False
        pnlAdminUsers.Visible = False

        Select Case ddlManageWhat.SelectedValue
            Case "User_Responsibility"
                pnlAssignments.Visible = True
            Case "BAITypeCodes_LU"
                pnlBAITypeCodes.Visible = True
                PopulateBAICategoryDropDown()
                PopulateBAITypeCodes()
            Case "BankAccountNumber_LU"
                pnlAdminBankAccountNumbers.Visible = True
                PopulateBankAccountNumbers()
            Case "CashCategory_Type_LU"
                pnlAdminCashJournalTypes.Visible = True
                PopulateCJTypes()
            Case "LockboxCodes_LU"
                pnlAdminLockBoxCodes.Visible = True
                PopulateLBCodes()
            Case "MiscGLCodes_LU"
                pnlAdminMiscGLCodes.Visible = True
                PopulateMiscGLCodes()
            Case "Overrides_LU"
                pnlAdminOverrides.Visible = True
            Case "Unresolved_LU"
                pnlAdminUnresolvedReasons.Visible = True
                PopulateAdminUnresolved()
            Case "Users"
                pnlAdminUsers.Visible = True
                PopulateAdmiUsers()
        End Select


    End Sub

    Private Sub btnAdminAddBAICode_Click(sender As Object, e As EventArgs) Handles btnAdminAddBAICode.Click

        Dim n As Integer
        If Trim(txtAdminBAITypeCodes.Text) = "" Then
            explanationlabelAdmin.Text = "No BAI Type Code entered"
            mpeAdminPage.Show()
            Exit Sub
        ElseIf Integer.TryParse(txtAdminBAITypeCodes.Text, n) = False Then
            explanationlabelAdmin.Text = "BAI Type Code must be an Integer value"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If Trim(ddlAdminBAICategories.SelectedValue) = "(Select Category)" Then
            explanationlabelAdmin.Text = "Category Required"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from DWH.AR.ActivityReport_BAITypeCodes_LU where BAITypeCode = '" & Trim(Replace(txtAdminBAITypeCodes.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This BAI Type Code is already assigned"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String
        SQL = "Insert into DWH.AR.ActivityReport_BAITypeCodes_LU values ('" & Trim(Replace(txtAdminBAITypeCodes.Text, "'", "''")) & "', '" & Trim(Replace(txtAdminBAITypeDesc.Text, "'", "''")) & _
            "', '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "', getdate(), 1, '" & Trim(Replace(txtAdminBAIShortDesc.Text, "'", "''")) & "')"

        ExecuteSql(SQL)
        txtAdminBAITypeCodes.Text = ""
        ddlAdminBAICategories.SelectedValue = "(Select Category)"

         PopulateBAITypeCodes()


    End Sub

    Private Sub gvBAITypeCodes_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvBAITypeCodes.RowCancelingEdit
        Try
            gvBAITypeCodes.EditIndex = -1
            PopulateBAITypeCodes()
            ' Dim SQL As String = "select * froM DWH.AR.ActivityReport_BAITypeCodes_LU " & _
            '"where Active = 1 and (Activity_Category = '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & _
            '"' or '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & "' = '(Select Category)')"


            ' gvBAITypeCodes.DataSource = GetData(SQL).DefaultView
            ' gvBAITypeCodes.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvBAITypeCodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvBAITypeCodes.RowCommand

        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            'If sd = 1 Then
            '    se = se + " " + "desc"
            'Else
            '    se = se + " " + "asc"
            'End If

            If Commander = "RemoveCode" Then

                'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_BAITypeCodes_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '    "' where BAITypeCode = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                Dim PrepSQL As String = "with cte as ( " & _
                    "select top 1 * from DWH.AR.ActivityReport_BAITypeCodes_LU " & _
                    "where BAITypeCode = '" & Replace(Detail_ID, "'", "''") & "' " & _
                    "order by Active desc,  ModifiedDate desc) " & _
                    "update cte set Active = -Active + 1, ModifiedDate = getdate(), ModifiedBy =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                PopulateBAITypeCodes()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvBAITypeCodes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvBAITypeCodes.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).CssClass = "locked"
            e.Row.Cells(2).CssClass = "locked"

            Dim attch As Integer = 0
            Dim lbl As Label = e.Row.FindControl("lblCategory")

            Dim ddlCat As DropDownList = e.Row.FindControl("ddlCategory")

        Dim SQL As String = "select CashCategory, 1 as ord from DWH.AR.ActivityReport_CashCategory " & _
            "where Active = 1"

        ddlCat.DataSource = GetData(SQL)
        ddlCat.DataTextField = "CashCategory"
        ddlCat.DataValueField = "CashCategory"
        ddlCat.DataBind()

        Try
                ddlCat.SelectedValue = lbl.Text
        Catch ex As Exception

            End Try


        End If

    End Sub

    Private Sub gvBAITypeCodes_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvBAITypeCodes.RowEditing
        Try


            'Dim SQL As String = "select * froM DWH.AR.ActivityReport_BAITypeCodes_LU " & _
            '"where Active = 1 and (Activity_Category = '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & _
            '"' or '" & Replace(ddlAdminBAICategories.SelectedValue, "'", "''") & "' = '(Select Category)')"

            gvBAITypeCodes.EditIndex = e.NewEditIndex
            PopulateBAITypeCodes()
            'gvBAITypeCodes.DataSource = GetData(SQL).DefaultView
            'gvBAITypeCodes.DataBind()


            'Dim idx As Integer = e.NewEditIndex
            'gvBAITypeCodes.DataBind()

            Dim lblTranDesc As Label = gvBAITypeCodes.Rows(e.NewEditIndex).FindControl("lblTranDescription")
            Dim txtTranDesc As TextBox = gvBAITypeCodes.Rows(e.NewEditIndex).FindControl("txtTranDescription")

            Dim lblTranShortDesc As Label = gvBAITypeCodes.Rows(e.NewEditIndex).FindControl("lblTranShortDesc")
            Dim txtTranShortDesc As TextBox = gvBAITypeCodes.Rows(e.NewEditIndex).FindControl("txtTranShortDesc")

            Dim lblCat As Label = gvBAITypeCodes.Rows(e.NewEditIndex).FindControl("lblCategory")
            Dim ddlCat As DropDownList = gvBAITypeCodes.Rows(e.NewEditIndex).FindControl("ddlCategory")

            'Dim SQL As String = "select CashCategory, 1 as ord from DWH.AR.ActivityReport_CashCategory " & _
            '    "where Active = 1"

            'ddlCat.DataSource = GetData(SQL)
            'ddlCat.DataTextField = "CashCategory"
            'ddlCat.DataValueField = "CashCategory"
            'ddlCat.DataBind()

            'Try
            '    ddlCat.SelectedValue = lblCat.Text
            'Catch ex As Exception

            'End Try

            txtTranDesc.Visible = True
            lblTranDesc.Visible = False
            ddlCat.Visible = True
            lblCat.Visible = False

            For Each canoe As GridViewRow In gvBAITypeCodes.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

            'gvBAITypeCodes.EditIndex = idx

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvBAITypeCodes_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvBAITypeCodes.RowUpdating
        Dim depid As String = gvBAITypeCodes.DataKeys(e.RowIndex).Value.ToString

        Dim txtTranDescription As TextBox = gvBAITypeCodes.Rows(e.RowIndex).FindControl("txtTranDescription")
        Dim txtTranShortDesc As TextBox = gvBAITypeCodes.Rows(e.RowIndex).FindControl("txtTranShortDesc")
        Dim ddlCategory As DropDownList = gvBAITypeCodes.Rows(e.RowIndex).FindControl("ddlCategory")

        Dim PrepSQL As String = "Update DWH.AR.ActivityReport_BAITypeCodes_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where BAITypeCode = '" & Replace(depid, "'", "''") & "' and Active = 1 " & _
                "Insert into DWH.AR.ActivityReport_BAITypeCodes_LU " & _
            "values ('" & Replace(depid, "'", "''") & "', '" & Replace(txtTranDescription.Text, "'", "''") & "', '" & Replace(ddlCategory.SelectedValue, "'", "''") & _
            "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, '" & Replace(txtTranShortDesc.Text, "'", "''") & "') "


        ExecuteSql(PrepSQL)

        gvBAITypeCodes.EditIndex = -1
        PopulateBAITypeCodes()
    End Sub

    Private Sub btnAdminAddBAN_Click(sender As Object, e As EventArgs) Handles btnAdminAddBAN.Click
        Dim n As Int64
        If Trim(txtAdminBankAccountNumber.Text) = "" Then
            explanationlabelAdmin.Text = "No Bank Account Number entered"
            mpeAdminPage.Show()
            Exit Sub
        ElseIf Int64.TryParse(txtAdminBankAccountNumber.Text, n) = False Then
            explanationlabelAdmin.Text = "Bank Account Number must be an Integer value"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If Trim(ddlAdminBANFacilities.SelectedValue) = "(Select Facility)" Then
            explanationlabelAdmin.Text = "Facility Required"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from DWH.AR.ActivityReport_BankAccountNumber_LU where AccountNumber = '" & Trim(Replace(txtAdminBankAccountNumber.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This Bank Account Number is already assigned"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String
        SQL = "Insert into DWH.AR.ActivityReport_BankAccountNumber_LU values ('" & Trim(Replace(txtAdminBankAccountNumber.Text, "'", "''")) & "', '" & Trim(Replace(txtBankDescription.Text, "'", "''")) & _
            "', '" & Replace(ddlAdminBANFacilities.SelectedValue, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "', getdate(), 1)"

        ExecuteSql(SQL)
        txtAdminBankAccountNumber.Text = ""
        txtBankDescription.Text = ""
        ddlAdminBANFacilities.SelectedValue = "(Select Facility)"

        PopulateBankAccountNumbers()
    End Sub

    Private Sub gvAdminBankAcctNos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminBankAcctNos.RowCancelingEdit
        Try
            gvAdminBankAcctNos.EditIndex = -1
            PopulateBankAccountNumbers()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminBankAcctNos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminBankAcctNos.RowCommand
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            'If sd = 1 Then
            '    se = se + " " + "desc"
            'Else
            '    se = se + " " + "asc"
            'End If

            If Commander = "RemoveBAN" Then

                'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_BankAccountNumber_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '    "' where AccountNumber = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                Dim PrepSQL As String = "with cte as ( " & _
                "select top 1 * from DWH.AR.ActivityReport_BankAccountNumber_LU " & _
                "where AccountNumber = '" & Replace(Detail_ID, "'", "''") & "' " & _
                "order by Active desc,  ModifiedDate desc) " & _
                "update cte set Active = -Active + 1, ModifiedDate = getdate(), ModifiedBy =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                PopulateBankAccountNumbers()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvAdminBankAcctNos_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminBankAcctNos.RowEditing
        Try


            ' Dim SQL As String = "select *, case when Facility = 'A' then 'Atlanta' when Facility  = 'C' then 'Cherokee' when Facility = 'F' then 'Forsyth' else Facility end as FacFull " & _
            '" froM DWH.AR.ActivityReport_BankAccountNumber_LU " & _
            '"where Active = 1 "

            gvAdminBankAcctNos.EditIndex = e.NewEditIndex
            'gvAdminBankAcctNos.DataSource = GetData(SQL).DefaultView
            'gvAdminBankAcctNos.DataBind()
            PopulateBankAccountNumbers()


            Dim lblDescription As Label = gvAdminBankAcctNos.Rows(e.NewEditIndex).FindControl("lblDescription")
            Dim txtDescription As TextBox = gvAdminBankAcctNos.Rows(e.NewEditIndex).FindControl("txtDescription")

            Dim lblFacility As Label = gvAdminBankAcctNos.Rows(e.NewEditIndex).FindControl("lblFacility")
            Dim ddlFacility As DropDownList = gvAdminBankAcctNos.Rows(e.NewEditIndex).FindControl("ddlFacility")

            txtDescription.Visible = True
            lblDescription.Visible = False
            ddlFacility.Visible = True
            lblFacility.Visible = False

            For Each canoe As GridViewRow In gvAdminBankAcctNos.Rows
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

    Private Sub gvAdminBankAcctNos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminBankAcctNos.RowUpdating
        Dim depid As String = gvAdminBankAcctNos.DataKeys(e.RowIndex).Value.ToString

        Dim txtDescription As TextBox = gvAdminBankAcctNos.Rows(e.RowIndex).FindControl("txtDescription")
        Dim ddlFacility As DropDownList = gvAdminBankAcctNos.Rows(e.RowIndex).FindControl("ddlFacility")

        Dim PrepSQL As String = "Update DWH.AR.ActivityReport_BankAccountNumber_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where AccountNumber = '" & Replace(depid, "'", "''") & "' and Active = 1 " & _
                "Insert into DWH.AR.ActivityReport_BankAccountNumber_LU " & _
            "values ('" & Replace(depid, "'", "''") & "', '" & Replace(txtDescription.Text, "'", "''") & "', '" & Replace(ddlFacility.SelectedValue, "'", "''") & _
            "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1) "


        ExecuteSql(PrepSQL)

        gvAdminBankAcctNos.EditIndex = -1
        PopulateBankAccountNumbers()
    End Sub

    Private Sub btnAddAdminCJType_Click(sender As Object, e As EventArgs) Handles btnAddAdminCJType.Click

        If Trim(txtAdminCJType.Text) = "" Then
            explanationlabelAdmin.Text = "No Type entered"
            mpeAdminPage.Show()
            Exit Sub
        End If

        If GetScalar("Select count(*) from DWH.AR.ActivityReport_CashCategory_Type_LU where Type = '" & Trim(Replace(txtAdminCJType.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This Typeis already assigned"
            mpeAdminPage.Show()
            Exit Sub
        End If

        Dim x As String
        If Len(Trim(Replace(txtAdminCJDescription.Text, "'", "''"))) > 0 Then
            x = " '" & Trim(Replace(txtAdminCJDescription.Text, "'", "''")) & "', "
        Else
            x = " null, "
        End If

        Dim y As String
        If Len(Trim(Replace(txtAdminCJShort.Text, "'", "''"))) > 0 Then
            y = " '" & Trim(Replace(txtAdminCJShort.Text, "'", "''")) & "', "
        Else
            y = " null, "
        End If

        Dim SQL As String
        SQL = "Insert into DWH.AR.ActivityReport_CashCategory_Type_LU values ('Cash Journal', '" & Trim(Replace(txtAdminCJType.Text, "'", "''")) & "', " & x & y & _
            " '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "', getdate(), 1, 1)"

        ExecuteSql(SQL)
        txtAdminCJType.Text = ""
        txtAdminCJDescription.Text = ""
        txtAdminCJShort.Text = ""

        PopulateCJTypes()

    End Sub

    Private Sub gvAdminCJTypes_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminCJTypes.RowCancelingEdit
        Try
            gvAdminCJTypes.EditIndex = -1
            PopulateCJTypes()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminCJTypes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminCJTypes.RowCommand
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName


            If Commander = "RemoveType" Then

                'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_CashCategory_Type_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '    "' where Type = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                Dim PrepSQL As String = "with cte as ( " & _
                "select top 1 * from DWH.AR.ActivityReport_CashCategory_Type_LU " & _
                "where TYPE = '" & Replace(Detail_ID, "'", "''") & "' " & _
                "order by Active desc,  ModifiedDate desc) " & _
                "update cte set Active = -Active + 1, ModifiedDate = getdate(), ModifiedBy =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                PopulateCJTypes()
            ElseIf Commander = "ReverseSubmittal" Then

                'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_CashCategory_Type_LU set Submittable = -Submittable + 1, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '    "' where Type = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                Dim PrepSQL As String = "with cte as ( " & _
                "select top 1 * from DWH.AR.ActivityReport_CashCategory_Type_LU " & _
                "where TYPE = '" & Replace(Detail_ID, "'", "''") & "' " & _
                "order by Active desc,  ModifiedDate desc) " & _
                "update cte set Submittable = -Submittable + 1, ModifiedDate = getdate(), ModifiedBy =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                PopulateCJTypes()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminCJTypes_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminCJTypes.RowEditing
        Try

            gvAdminCJTypes.EditIndex = e.NewEditIndex
            PopulateCJTypes()

            Dim lblDescription As Label = gvAdminCJTypes.Rows(e.NewEditIndex).FindControl("lblDescription")
            Dim txtDescription As TextBox = gvAdminCJTypes.Rows(e.NewEditIndex).FindControl("txtDescription")

            Dim lblShortDesc As Label = gvAdminCJTypes.Rows(e.NewEditIndex).FindControl("lblShortDesc")
            Dim txtShortDesc As TextBox = gvAdminCJTypes.Rows(e.NewEditIndex).FindControl("txtShortDesc")


            txtDescription.Visible = True
            lblDescription.Visible = False
            txtShortDesc.Visible = True
            lblShortDesc.Visible = False

            For Each canoe As GridViewRow In gvAdminCJTypes.Rows
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

    Private Sub gvAdminCJTypes_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminCJTypes.RowUpdating
        Dim depid As String = gvAdminCJTypes.DataKeys(e.RowIndex).Value.ToString

        Dim txtDescription As TextBox = gvAdminCJTypes.Rows(e.RowIndex).FindControl("txtDescription")
        Dim txtShortDesc As TextBox = gvAdminCJTypes.Rows(e.RowIndex).FindControl("txtShortDesc")
        Dim lblSubmittable As Label = gvAdminCJTypes.Rows(e.RowIndex).FindControl("lblSubmittable")


        Dim PrepSQL As String = "Update DWH.AR.ActivityReport_CashCategory_Type_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where Type = '" & Replace(depid, "'", "''") & "' and Active = 1 " & _
                "Insert into DWH.AR.ActivityReport_CashCategory_Type_LU " & _
            "values ('Cash Journal', '" & Replace(depid, "'", "''") & "', '" & Replace(txtDescription.Text, "'", "''") & "', '" & Replace(txtShortDesc.Text, "'", "''") & _
            "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, '" & Replace(lblSubmittable.Text, "'", "''") & "') "


        ExecuteSql(PrepSQL)

        gvAdminCJTypes.EditIndex = -1
        PopulateCJTypes()
    End Sub

    Private Sub btnAdminAddLB_Click(sender As Object, e As EventArgs) Handles btnAdminAddLB.Click
        Dim n As Integer
        If Trim(txtAdminLockboxCode.Text) = "" Then
            explanationlabelAdmin.Text = "No Lockbox Number entered"
            mpeAdminPage.Show()
            Exit Sub
        ElseIf Integer.TryParse(txtAdminLockboxCode.Text, n) = False Then
            explanationlabelAdmin.Text = "Lockbox Number must be an Integer value"
            mpeAdminPage.Show()
            Exit Sub
        End If

        If GetScalar("Select count(*) from DWH.AR.ActivityReport_LockboxCodes_LU where LockboxCode = '" & Trim(Replace(txtAdminLockboxCode.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This Lockbox Number is already assigned"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String
        SQL = "Insert into DWH.AR.ActivityReport_LockboxCodes_LU values ('" & Trim(Replace(txtAdminLockboxCode.Text, "'", "''")) & "', '" & Trim(Replace(txtAdminLockboxType.Text, "'", "''")) & _
            "', '" & Replace(txtAdminLockboxName.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "', getdate(), 1, null)"

        ExecuteSql(SQL)
        txtAdminLockboxCode.Text = ""
        txtAdminLockboxType.Text = ""
        txtAdminLockboxName.Text = ""


        PopulateLBCodes()

    End Sub

    Private Sub gvAdminLBTypes_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminLBTypes.RowCancelingEdit
        Try
            gvAdminLBTypes.EditIndex = -1
            PopulateLBCodes()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminLBTypes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminLBTypes.RowCommand
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName


            If Commander = "RemoveType" Then

                Dim PrepSQL As String = "Update DWH.AR.ActivityReport_LockboxCodes_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where LockboxCode = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                ExecuteSql(PrepSQL)
                PopulateLBCodes()
            ElseIf Commander = "FlipRollup" Then

                'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_CashCategory_Type_LU set Submittable = -Submittable + 1, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                '    "' where Type = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                Dim PrepSQL As String = "update DWH.AR.ActivityReport_LockboxCodes_LU set LDRollup = -isnull(LDRollup, 0) + 1, ModifiedDate = getdate(), ModifiedBy =  '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                     "' where LockboxCode = '" & Replace(Detail_ID, "'", "''") & "' and Active = 1"

                ExecuteSql(PrepSQL)
                PopulateLBCodes()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminLBTypes_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminLBTypes.RowEditing
        Try

            gvAdminLBTypes.EditIndex = e.NewEditIndex
            PopulateLBCodes()

            Dim lblDescription As Label = gvAdminLBTypes.Rows(e.NewEditIndex).FindControl("lblDescription")
            Dim txtDescription As TextBox = gvAdminLBTypes.Rows(e.NewEditIndex).FindControl("txtDescription")

            Dim lblShortDesc As Label = gvAdminLBTypes.Rows(e.NewEditIndex).FindControl("lblShortDesc")
            Dim txtShortDesc As TextBox = gvAdminLBTypes.Rows(e.NewEditIndex).FindControl("txtShortDesc")


            txtDescription.Visible = True
            lblDescription.Visible = False
            txtShortDesc.Visible = True
            lblShortDesc.Visible = False

            For Each canoe As GridViewRow In gvAdminLBTypes.Rows
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

    Private Sub gvAdminLBTypes_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminLBTypes.RowUpdating
        Dim depid As String = gvAdminLBTypes.DataKeys(e.RowIndex).Value.ToString

        Dim txtDescription As TextBox = gvAdminLBTypes.Rows(e.RowIndex).FindControl("txtDescription")
        Dim txtShortDesc As TextBox = gvAdminLBTypes.Rows(e.RowIndex).FindControl("txtShortDesc")


        Dim PrepSQL As String = "Update DWH.AR.ActivityReport_LockboxCodes_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where LockboxCode = '" & Replace(depid, "'", "''") & "' and Active = 1 " & _
                "Insert into DWH.AR.ActivityReport_LockboxCodes_LU " & _
            "values ('" & Replace(depid, "'", "''") & "', '" & Replace(txtDescription.Text, "'", "''") & "', '" & Replace(txtShortDesc.Text, "'", "''") & _
            "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1, null) "


        ExecuteSql(PrepSQL)

        gvAdminLBTypes.EditIndex = -1
        PopulateLBCodes()
    End Sub

    Private Sub btnAddAdminMiscGLCode_Click(sender As Object, e As EventArgs) Handles btnAddAdminMiscGLCode.Click

        If Trim(txtAdminMiscGL_FullDesc.Text) = "" Then
            explanationlabelAdmin.Text = "No Description entered"
            mpeAdminPage.Show()
            Exit Sub
        End If

        'If GetScalar("Select count(*) from DWH.AR.ActivityReport_LockboxCodes_LU where FullDisplay = '" & Trim(Replace(txtAdminLockboxCode.Text, "'", "''")) & "' and Active = 1") > 0 Then
        '    explanationlabelAdmin.Text = "This Lockbox Number is already assigned"
        '    mpeAdminPage.Show()
        '    Exit Sub
        'End If

        Dim x As String
        If Len(Trim(Replace(txtAdminMiscGL_Department.Text, "'", "''"))) > 0 Then
            x = " '" & Trim(Replace(txtAdminMiscGL_Department.Text, "'", "''")) & "', "
        Else
            x = " null, "
        End If

        Dim y As String
        If Len(Trim(Replace(txtAdminMiscGL_SubAcct.Text, "'", "''"))) > 0 Then
            y = " '" & Trim(Replace(txtAdminMiscGL_SubAcct.Text, "'", "''")) & "', "
        Else
            y = " null, "
        End If

        Dim SQL As String
        SQL = "Insert into DWH.AR.ActivityReport_MiscGLCodes_LU values (" & x & y & "'" & Trim(Replace(txtAdminMiscGL_FullDesc.Text, "'", "''")) & "', '" & Trim(Replace(txtAdminMiscGL_ShortDesc.Text, "'", "''")) & _
            "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "', getdate(), 1)"

        ExecuteSql(SQL)
        txtAdminMiscGL_FullDesc.Text = ""
        txtAdminMiscGL_ShortDesc.Text = ""
        txtAdminMiscGL_Department.Text = ""
        txtAdminMiscGL_SubAcct.Text = ""

        PopulateMiscGLCodes()

    End Sub

    Private Sub gvAdminMiscGL_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminMiscGL.RowCancelingEdit
        Try
            gvAdminMiscGL.EditIndex = -1
            PopulateMiscGLCodes()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminMiscGL_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminMiscGL.RowCommand
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName


            If Commander = "RemoveType" Then

                Dim PrepSQL As String = "Update DWH.AR.ActivityReport_MiscGLCodes_LU set Active = -Active + 1, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where Misc_Identity = '" & Replace(Detail_ID, "'", "''") & "' "

                ExecuteSql(PrepSQL)
                PopulateMiscGLCodes()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminMiscGL_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminMiscGL.RowEditing
        Try

            gvAdminMiscGL.EditIndex = e.NewEditIndex
            PopulateMiscGLCodes()

            Dim lblDescription As Label = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("lblDescription")
            Dim txtDescription As TextBox = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("txtDescription")

            Dim lblShortDesc As Label = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("lblShortDesc")
            Dim txtShortDesc As TextBox = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("txtShortDesc")

            Dim lblDepartment As Label = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("lblDepartment")
            Dim txtDepartment As TextBox = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("txtDepartment")

            Dim lblSubAcct As Label = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("lblSubAcct")
            Dim txtSubAcct As TextBox = gvAdminMiscGL.Rows(e.NewEditIndex).FindControl("txtSubAcct")


            txtDescription.Visible = True
            lblDescription.Visible = False
            txtShortDesc.Visible = True
            lblShortDesc.Visible = False
            txtDepartment.Visible = True
            lblDepartment.Visible = False
            txtSubAcct.Visible = True
            lblSubAcct.Visible = False

            For Each canoe As GridViewRow In gvAdminMiscGL.Rows
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

    Private Sub gvAdminMiscGL_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminMiscGL.RowUpdating
        Dim depid As String = gvAdminMiscGL.DataKeys(e.RowIndex).Value.ToString

        Dim txtDepartment As TextBox = gvAdminMiscGL.Rows(e.RowIndex).FindControl("txtDepartment")
        Dim txtSubAcct As TextBox = gvAdminMiscGL.Rows(e.RowIndex).FindControl("txtSubAcct")
        Dim txtDescription As TextBox = gvAdminMiscGL.Rows(e.RowIndex).FindControl("txtDescription")
        Dim txtShortDesc As TextBox = gvAdminMiscGL.Rows(e.RowIndex).FindControl("txtShortDesc")

        Dim x As String
        If Len(Trim(Replace(txtDepartment.Text, "'", "''"))) > 0 Then
            x = " '" & Trim(Replace(txtDepartment.Text, "'", "''")) & "', "
        Else
            x = " null, "
        End If
        Dim y As String
        If Len(Trim(Replace(txtSubAcct.Text, "'", "''"))) > 0 Then
            y = " '" & Trim(Replace(txtSubAcct.Text, "'", "''")) & "', "
        Else
            y = " null, "
        End If

        'Dim PrepSQL As String = "Update DWH.AR.ActivityReport_MiscGLCodes_LU set Active = 0, ModifiedDate = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
        '            "' where Misc_Identity = '" & Replace(depid, "'", "''") & "' and Active = 1 " & _
        '        "Insert into DWH.AR.ActivityReport_MiscGLCodes_LU " & _
        '    "values (" & x & y & "'" & Replace(txtDescription.Text, "'", "''") & "', '" & Replace(txtShortDesc.Text, "'", "''") & _
        '    "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1) "



        Dim PrepSQL As String = "Insert into DWH.AR.ActivityReport_MiscGLCodes_LU_History select * from DWH.AR.ActivityReport_MiscGLCodes_Lu where Misc_Identity = '" & Replace(depid, "'", "''") & "' " & _
            "Update DWH.AR.ActivityReport_MiscGLCodes_LU set Department = " & x & " SubAcct = " & y & " FullDisplay = '" & Replace(txtDescription.Text, "'", "''") & "', DropDownDisplay = '" & _
            Replace(txtShortDesc.Text, "'", "''") & "', ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            "', ModifiedDate = getdate() where Misc_Identity = '" & Replace(depid, "'", "''") & "' "

        ExecuteSql(PrepSQL)

        gvAdminMiscGL.EditIndex = -1
        PopulateMiscGLCodes()
    End Sub

    Private Sub btnAddUnresolved_Click(sender As Object, e As EventArgs) Handles btnAddUnresolved.Click

        If Trim(txtAdminUnresolvedFull.Text) = "" Then
            explanationlabelAdmin.Text = "No Reason entered"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from DWH.AR.ActivityReport_Unresolved_LU where UnresolvedFullName = '" & Trim(Replace(txtAdminUnresolvedFull.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This reason already exists"
            mpeAdminPage.Show()
            Exit Sub
        ElseIf GetScalar("Select count(*) from DWH.AR.ActivityReport_Unresolved_LU where UnresolvedFullName = '" & Trim(Replace(txtAdminUnresolvedFull.Text, "'", "''")) & "' and Active = 0") > 0 Then
            explanationlabelAdmin.Text = "This reason already exists (It is currently Inactive)"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String

        Dim z As String

        If Trim(Replace(txtAdminUnresolvedShort.Text, "'", "''")) = "" Then
            z = "null, "
        Else
            z = "'" & Trim(Replace(txtAdminUnresolvedShort.Text, "'", "''")) & "', "
        End If

        SQL = "Insert into DWH.AR.ActivityReport_Unresolved_LU values ('" & Replace(txtAdminUnresolvedFull.Text, "'", "''") & "', " & z & " getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 1)"


        ExecuteSql(SQL)
        txtAdminUnresolvedShort.Text = ""
        txtAdminUnresolvedFull.Text = ""

        PopulateAdminUnresolved()

    End Sub

    Private Sub gvAdminUnresolvedReasons_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminUnresolvedReasons.RowCancelingEdit
        Try
            gvAdminUnresolvedReasons.EditIndex = -1
            PopulateAdminUnresolved()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUnresolvedReasons_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminUnresolvedReasons.RowCommand
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName


            If Commander = "RemoveType" Then

                Dim PrepSQL As String = "Update DWH.AR.ActivityReport_Unresolved_LU set Active = -Active + 1, ModifiedDate = getdate(), ModifiedUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where UnresolvedId = '" & Replace(Detail_ID, "'", "''") & "' "

                ExecuteSql(PrepSQL)
                PopulateAdminUnresolved()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUnresolvedReasons_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminUnresolvedReasons.RowEditing
        Try

            gvAdminUnresolvedReasons.EditIndex = e.NewEditIndex
            PopulateAdminUnresolved()

            Dim lblDescription As Label = gvAdminUnresolvedReasons.Rows(e.NewEditIndex).FindControl("lblDescription")
            Dim txtDescription As TextBox = gvAdminUnresolvedReasons.Rows(e.NewEditIndex).FindControl("txtDescription")

            Dim lblShortDesc As Label = gvAdminUnresolvedReasons.Rows(e.NewEditIndex).FindControl("lblShortDesc")
            Dim txtShortDesc As TextBox = gvAdminUnresolvedReasons.Rows(e.NewEditIndex).FindControl("txtShortDesc")

            txtDescription.Visible = True
            lblDescription.Visible = False
            txtShortDesc.Visible = True
            lblShortDesc.Visible = False

            For Each canoe As GridViewRow In gvAdminUnresolvedReasons.Rows
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

    Private Sub gvAdminUnresolvedReasons_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminUnresolvedReasons.RowUpdating
        Dim depid As String = gvAdminUnresolvedReasons.DataKeys(e.RowIndex).Value.ToString

        Dim txtDescription As TextBox = gvAdminUnresolvedReasons.Rows(e.RowIndex).FindControl("txtDescription")
        Dim txtShortDesc As TextBox = gvAdminUnresolvedReasons.Rows(e.RowIndex).FindControl("txtShortDesc")

        'Dim PrepSQL As String = "Insert into DWH.AR.ActivityReport_Unresolved_LU  select UnresolvedFullName, UnresolvedDropDownList, ModifiedDate, ModifiedUser + ' -- ' + convert(varchar, UnresolvedId), 0 " & _
        '        " from DWH.AR.ActivityReport_Unresolved_LU where UnresolvedId = '" & Replace(depid, "'", "''") & "' " & _
        '        "Update DWH.AR.ActivityReport_Unresolved_LU set ModifiedDate = getdate(), ModifiedUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
        '            "', UnresolvedFullName = '" & Replace(txtDescription.Text, "'", "''") & "', UnresolvedDropDownList = '" & Replace(txtShortDesc.Text, "'", "''") & _
        '            "' where UnresolvedId = '" & Replace(depid, "'", "''") & "' and Active = 1 "

        Dim PrepSQL As String = "Insert into DWH.AR.ActivityReport_Unresolved_LU_History select * from DWH.AR.ActivityReport_Unresolved_LU where UnresolvedId = '" & Replace(depid, "'", "''") & "' " & _
            "Update DWH.AR.ActivityReport_Unresolved_LU set ModifiedDate = getdate(), ModifiedUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            "', UnresolvedFullName = '" & Replace(txtDescription.Text, "'", "''") & "', UnresolvedDropDownList = '" & Replace(txtShortDesc.Text, "'", "''") & _
            "' where UnresolvedId = '" & Replace(depid, "'", "''") & "' "

        ExecuteSql(PrepSQL)

        gvAdminUnresolvedReasons.EditIndex = -1
        PopulateAdminUnresolved()

    End Sub

    Private Sub SynchWebFDPermissions()

        Dim SQL As String = "insert into WebFD.dbo.aspnet_Users " & _
            "select '5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', dar.UserLogin), UserLogin, UserLogin, null, 0, getdate() " & _
            "from DWH.AR.ActivityReport_Users dar " & _
            "where dar.Active = 1 and not exists (select * from WebFD.dbo.aspnet_Users au  " & _
            "where au.UserName = dar.UserLogin) " & _
            " " & _
            "insert into WebFD.dbo.aspnet_UsersInRoles  " & _
            "select UserId, '700B2919-5ECE-74E6-10AE-E47524BC9C29' from " & _
            "DWH.AR.ActivityReport_Users dar " & _
            "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
            "where dar.Active = 1 and not exists (select * from " & _
            "WebFD.dbo.aspnet_UsersInRoles uir  " & _
            "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
            "where au.UserId = uir.UserId " & _
            "and RoleName = 'ARActivityReport') " & _
            " " & _
            "delete r from WebFD.dbo.aspnet_UsersInRoles r  " & _
            "where not exists ( " & _
            "select * from " & _
            "DWH.AR.ActivityReport_Users dar " & _
            "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin and au.UserID = r.UserID " & _
            "where dar.Active = 1) and RoleID = '700B2919-5ECE-74E6-10AE-E47524BC9C29'"

        ExecuteSql(SQL)

    End Sub

    Private Sub lbSrchUsr_Click(sender As Object, e As EventArgs) Handles lbSrchUsr.Click
        pnlSrchUser.Visible = True
    End Sub

    Private Sub lbCloseUsrSrch_Click(sender As Object, e As EventArgs) Handles lbCloseUsrSrch.Click
        pnlSrchUser.Visible = False
    End Sub

    Private Sub txtAdminADUserLogin_TextChanged(sender As Object, e As EventArgs) Handles txtAdminADUserLogin.TextChanged

        Try
            lblAdminADUserName.Text = ""
            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            osearcher.Filter = "(&(samaccountname=" & txtAdminADUserLogin.Text & "))" ' search filter

            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()
            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    lblAdminADUserName.Text = lblAdminADUserName.Text & result.GetDirectoryEntry.Properties("cn").Value
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
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

    Private Sub btnAdminAddUser_Click(sender As Object, e As EventArgs) Handles btnAdminAddUser.Click


        If Trim(txtAdminADUserLogin.Text) = "" Then
            explanationlabelAdmin.Text = "No User Login entered"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from DWH.AR.ActivityReport_Users where UserLogin = '" & Trim(Replace(txtAdminADUserLogin.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This User Login already has access"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String

        SQL = "Insert into DWH.AR.ActivityReport_Users values ('" & Replace(txtAdminADUserLogin.Text, "'", "''") & "', '" & _
                Trim(Replace(lblAdminADUserName.Text, "'", "''")) & "', '" & Replace(Trim(txtAdminADDisplayName.Text), "'", "''") & "', 1, getdate(), getdate(), '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, null)"


        ExecuteSql(SQL)
        SynchWebFDPermissions()

        txtAdminADUserLogin.Text = ""
        txtAdminADDisplayName.Text = ""
        lblAdminADUserName.Text = ""

        PopulateAdmiUsers()


    End Sub

    Private Sub gvAdminUsers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAdminUsers.PageIndexChanging
        Try

            Dim dv As DataView

            dv = UsersView()

            dv.Sort = userssort.Text
            gvAdminUsers.DataSource = dv

            gvAdminUsers.PageIndex = e.NewPageIndex

            gvAdminUsers.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminUsers.RowCancelingEdit
        Try
            Dim dv As DataView

            dv = UsersView()


            dv.Sort = userssort.Text
            gvAdminUsers.DataSource = dv
            gvAdminUsers.EditIndex = -1
            gvAdminUsers.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminUsers.RowCommand
        Try
            Dim UserLogin As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If CInt(Developer.Text) = 0 And UserLogin = Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") Then
                explanationlabelAdmin.Text = "You cannot update your own permissions"
                mpeAdminPage.Show()
                Exit Sub
            End If

            If Left(varname, 4) = "Flip" Then
                If GetScalar("select count(*) from DWH.AR.ActivityReport_Users where Active = 1 and AssignRoles = 1 and UserLogin = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'") = 0 And CInt(Developer.Text) = 0 Then
                    explanationlabelAdmin.Text = "You do not have permission to alter role assignments"
                    mpeAdminPage.Show()
                    Exit Sub
                End If
            End If

            If varname = "RevokeAccess" Then
                Dim Sql As String = "update DWH.AR.ActivityReport_Users set Active = 1 - Active, ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate() where UserLogin = '" & Replace(UserLogin, "'", "''") & "'"
                ExecuteSql(Sql)
                PopulateAdmiUsers()
                SynchWebFDPermissions()
            ElseIf varname = "FlipAdmin" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set Administrator = 1 - isnull(Administrator, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate() "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipNetting" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set Netting = 1 - isnull(Netting, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate() "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipResearch" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set Research = 1 - isnull(Research, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate() "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipAssignments" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set AssignRights = 1 - isnull(AssignRights, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate() where UserLogin = '" & Replace(UserLogin, "'", "''") & "'"
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipRoles" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set AssignRoles = 1 - isnull(AssignRoles, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate()  "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipDP" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set RunProcessing = 1 - isnull(RunProcessing, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate()  "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipReset" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set ResetRights = 1 - isnull(ResetRights, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate()  "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            ElseIf varname = "FlipHoliday" Then
                Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(UserLogin, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow set HolidayRights = 1 - isnull(HolidayRights, 0), ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', DateModified = getdate()  "
                ExecuteSql(Sql)
                PopulateAdmiUsers()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvAdminUsers.RowCreated
        Try

            If CInt(Developer.Text) = 0 Then
                Dim adCheck As DataView = UsersView()
                adCheck.Sort = "UserLogin"
                Dim x As Integer = adCheck.Find(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"))

                If adCheck(x)(3) = "Make Admin" Then
                    e.Row.Cells(5).CssClass = "hidden"
                End If
                If adCheck(x)(11) = "Denied" Then
                    e.Row.Cells(6).CssClass = "hidden"
                End If
                If adCheck(x)(4) <> "No Limit" Then
                    e.Row.Cells(8).CssClass = "hidden"
                End If
                If adCheck(x)(5) = "Denied" Then
                    e.Row.Cells(9).CssClass = "hidden"
                End If
                If adCheck(x)(6) = "Denied" Then
                    e.Row.Cells(10).CssClass = "hidden"
                End If
                If adCheck(x)(7) = "Denied" Then
                    e.Row.Cells(11).CssClass = "hidden"
                End If
                If adCheck(x)(8) = "Denied" Then
                    e.Row.Cells(12).CssClass = "hidden"
                End If
                If adCheck(x)(12) = "Denied" Then
                    e.Row.Cells(13).CssClass = "hidden"
                End If
                If adCheck(x)(13) = "Denied" Then
                    e.Row.Cells(14).CssClass = "hidden"
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvAdminUsers_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminUsers.RowEditing
        Try

            Dim dv As DataView

            dv = UsersView()

            dv.Sort = userssort.Text

            gvAdminUsers.EditIndex = e.NewEditIndex
            gvAdminUsers.DataSource = dv
            gvAdminUsers.DataBind()

            Dim txtShortName As TextBox = gvAdminUsers.Rows(e.NewEditIndex).FindControl("txtShortName")
            Dim lblShortName As Label = gvAdminUsers.Rows(e.NewEditIndex).FindControl("lblShortName")

            Dim lblLocking As Label = gvAdminUsers.Rows(e.NewEditIndex).FindControl("lblLocking")
            Dim txtLocking As TextBox = gvAdminUsers.Rows(e.NewEditIndex).FindControl("txtLocking")

            Dim lblEmailAddress As Label = gvAdminUsers.Rows(e.NewEditIndex).FindControl("lblEmailAddress")
            Dim txtEmailAddress As TextBox = gvAdminUsers.Rows(e.NewEditIndex).FindControl("txtEmailAddress")

            txtShortName.Visible = True
            lblShortName.Visible = False

            lblEmailAddress.Visible = False
            txtEmailAddress.Visible = True

            If GetScalar("select count(*) from DWH.AR.ActivityReport_Users where Active = 1 and MultipleLocks is null and UserLogin = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'") > 0 Then
                lblLocking.Visible = False
                txtLocking.Visible = True
            End If

            For Each canoe As GridViewRow In gvAdminUsers.Rows
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

    Private Sub gvAdminUsers_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminUsers.RowUpdating
        Try
            Dim depid As String = gvAdminUsers.DataKeys(e.RowIndex).Value.ToString

            Dim txtShortName As TextBox = gvAdminUsers.Rows(e.RowIndex).FindControl("txtShortName")
            Dim txtLocking As TextBox = gvAdminUsers.Rows(e.RowIndex).FindControl("txtLocking")

            Dim lblEmailAddress As Label = gvAdminUsers.Rows(e.RowIndex).FindControl("lblEmailAddress")
            Dim txtEmailAddress As TextBox = gvAdminUsers.Rows(e.RowIndex).FindControl("txtEmailAddress")

            Dim x As Integer
            Dim y As String

            Dim z As String
            If Trim(txtEmailAddress.Text) = "" Then
                z = " null, "
            Else
                z = "'" & Replace(Trim(txtEmailAddress.Text), "'", "''") & "', "
            End If

            If txtLocking.Visible = False Then
                y = " MultipleLocks, "
            ElseIf Len(Trim(txtLocking.Text)) = 0 Then
                y = " null, "
            Else
                If Integer.TryParse(txtLocking.Text, x) Then
                    y = Trim(Replace(txtLocking.Text, "'", "''")) & ", "
                Else
                    explanationlabelAdmin.Text = "Locking Limit must be an integer or left blank to remove limit"
                    mpeAdminPage.Show()
                    Exit Sub
                End If
            End If

            'If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "e210635" Then
            '    explanationlabelAdmin.Text = "You do not have access to update row limits"
            '    mpeAdminPage.Show()
            '    Exit Sub
            'End If

            Dim Sql As String = "with UpdateRow as (select top 1 * from DWH.AR.ActivityReport_Users where UserLogin = '" & Replace(depid, "'", "''") & "' " & _
                    "order by Active desc, DateModified Desc ) update  UpdateRow  " & _
                "set UserDropDownListName = '" & Replace(txtShortName.Text, "'", "''") & "', MultipleLocks = " & y & " EmailAddress = " & z & _
                " DateModified = getdate(), ModifiedBy = '" & _
                Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' "

            ExecuteSql(Sql)

            gvAdminUsers.EditIndex = -1
            PopulateAdmiUsers()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvAdminUsers.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = UsersView()

        sorts = e.SortExpression

        If e.SortExpression = userssort.Text Then

            If CInt(usersdir.Text) = 1 Then
                dv.Sort = sorts + " " + "desc"
                usersdir.Text = 0
            Else
                dv.Sort = sorts + " " + "asc"
                usersdir.Text = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            usersdir.Text = 1
            userssort.Text = e.SortExpression
        End If

        gvAdminUsers.DataSource = dv
        gvAdminUsers.DataBind()
    End Sub

    Private Sub cbAdminActiveAssignments_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminActiveAssignments.CheckedChanged
        PopulateCurrentAssignments()
    End Sub

    Private Sub cbAdminBAICodes_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminBAICodes.CheckedChanged
        PopulateBAITypeCodes()
    End Sub

    Private Sub cbAdminBANs_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminBANs.CheckedChanged
        PopulateBankAccountNumbers()
    End Sub

    Private Sub cbAdminCJType_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminCJType.CheckedChanged
        PopulateCJTypes()
    End Sub

    Private Sub cbUnresolvedActive_CheckedChanged(sender As Object, e As EventArgs) Handles cbUnresolvedActive.CheckedChanged
        PopulateAdminUnresolved()
    End Sub

    Private Sub cbAdminLockboxTypes_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminLockboxTypes.CheckedChanged
        PopulateLBCodes()
    End Sub

    Private Sub cbAdminMiscActive_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminMiscActive.CheckedChanged
        PopulateMiscGLCodes()
    End Sub

    Private Sub cbAdminUserActives_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminUserActives.CheckedChanged
        PopulateAdmiUsers()
    End Sub

    Private Sub btnResetToday_Click(sender As Object, e As EventArgs) Handles btnResetToday.Click

        Dim x As Date

        If Date.TryParse(txtARDate.Text, x) Then
            srsExplanationLabel2.Text = "If you reset the data for " & txtARDate.Text & " you will permanently lose all user submissions and updates that have been made for this date.  Are you sure?"
            mpeSerious2.Show()
        Else
            explanationlabel.Text = "Please select a valid date"
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
        End If

    End Sub

    Private Sub btnConfirmSrs2_Click(sender As Object, e As EventArgs) Handles btnConfirmSrs2.Click
        Try
            Dim x As String = "update DWH.AR.ActivityReport " & _
                "set Active = 0, ModifiedBy = ModifiedBy + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "where Active = 1 and FirstActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' and CreatedBy not like 'LockBoxAutoGeneration%' and CreatedBy not like 'WFAllActivityAutoGeneration%' " & _
                "	and CreatedBy not like 'OverrideAutoGeneration%' " & _
                "	" & _
                "update DWH.AR.ActivityReport_Comments " & _
                "set Active = 0, UserID = UserID + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "	where Active = 1 and ActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' " & _
                "	" & _
                "update DWH.AR.ActivityReport_Detail " & _
                "set Active = 0, ModifiedBy = ModifiedBy + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "where Active = 1 and ActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' " & _
                "	and ModifiedBy not in ('Flagged Unresolved', 'Automatic CarryOver', 'AutoGeneration', 'Flagged CarryOver') " & _
                "	 " & _
                "update DWH.AR.ActivityReport_MiscGL " & _
                "set Active = 0 , UserID = UserID + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "	where Active = 1 and ActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' " & _
                "	 " & _
                "update DWH.AR.ActivityReport_Netting " & _
                "set Active = 0, ModifyUser = ModifyUser + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "	where Active = 1 and ActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' " & _
                "	 " & _
                "update DWH.AR.ActivityReport_Splitting " & _
                "set Active = 0, ModifyUser = ModifyUser + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "	where Active = 1 and ActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' " & _
                "	 " & _
                "update DWH.AR.ActivityReport_Transfers " & _
                "set Active = 0, ModifyUser = ModifyUser + '-- Reset by " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "	where Active = 1 and TransferDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' "

            ExecuteSql(x)
            ARDetailView()
            gv_AR_MainData.DataSource = Session("ARDetailView") ' ARDetailView()
            gv_AR_MainData.DataBind()
            BalanceCheck()
            mpeSerious2.Hide()

        Catch ex As Exception
            explanationlabel.Text = "Error Resetting Data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnHoliday_Click(sender As Object, e As EventArgs) Handles btnHoliday.Click
        Dim x As Date

        If Date.TryParse(txtARDate.Text, x) Then
            srsExplanationLabel3.Text = "If you mark " & txtARDate.Text & " as a Holiday, all deposits loaded on this day will be moved to the next day.  (You will still need to run daily processing to move on, and ensure everything balances)  Are you sure?"
            mpeSerious3.Show()
        Else
            explanationlabel.Text = "Please select a valid date"
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
        End If
    End Sub

    Private Sub btnConfirmSrs3_Click(sender As Object, e As EventArgs) Handles btnConfirmSrs3.Click
        Try
            Dim x As String = "update DWH.AR.ActivityReport " & _
                "set FirstActivityDate = (select min(Calendar_Date) from DWH.dbo.DimDate where Week_Day_Number between 2 and 6 and Calendar_Date > FirstActivityDate) /* DATEADD(D, 1, FirstActivityDate)*/ , ModifiedDate = getdate(), ModifiedBy = 'Holiday - " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "where Active = 1 and FirstActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' and (CreatedBy like 'LockBoxAutoGeneration%' or CreatedBy like 'WFAllActivityAutoGeneration%' " & _
                "	or CreatedBy like 'OverrideAutoGeneration%') " & _
                "	" & _
                "update DWH.AR.ActivityReport_Detail " & _
                "set ActivityDate = (select min(Calendar_Date) from DWH.dbo.DimDate where Week_Day_Number between 2 and 6 and Calendar_Date > ActivityDate) /* DATEADD(D, 1, ActivityDate)*/ , ModifiedBy = 'Holiday - " & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "where Active = 1 and ActivityDate = '" & Replace(Trim(txtARDate.Text), "'", "''") & "' " & _
                "	and ModifiedBy in ('AutoGeneration') "
              

            ExecuteSql(x)
            ARDetailView()
            gv_AR_MainData.DataSource = Session("ARDetailView") 'ARDetailView()
            gv_AR_MainData.DataBind()
            BalanceCheck()
            mpeSerious3.Hide()

        Catch ex As Exception
            explanationlabel.Text = "Error Resetting Data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnConfirmNoData_Click(sender As Object, e As EventArgs) Handles btnConfirmNoData.Click
        srsExplanationLabel.Text = "If you run Daily Processing for " & txtARDate.Text & " you will not be able to change anything that happened on this date."
        mpeSerious.Show()
    End Sub

    Private Sub btnCancelNoData_Click(sender As Object, e As EventArgs) Handles btnCancelNoData.Click
        mpeConfirmNoData.Hide()
    End Sub
    'Protected Sub txtSrch_TextChanged(sender As Object, e As EventArgs)
    '    ReloadMainPageR()
    'End Sub

    Protected Sub Popup_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        explanationlabel.Text = btn.CommandArgument.ToString
        ModalPopupExtender.Show()
    End Sub

    Private Sub btn_ActivityReportSearch_Click(sender As Object, e As EventArgs) Handles btn_ActivityReportSearch.Click
        ReloadMainPageR()
    End Sub

    Private Sub ddlNetRowUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNetRowUser.SelectedIndexChanged
        PopulateNetGrid()
        mpeNettingRows.Show()
    End Sub
End Class


