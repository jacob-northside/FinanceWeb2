Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Math
Imports System.DirectoryServices
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal

Imports FinanceWeb.WebFinGlobal


Public Class OHMSRADDataEntry
    Inherits System.Web.UI.Page
    Private Shared RunDate As Date
    Public Shared AdminView As New DataView
    Public Shared Adminmap As String
    Public Shared Admindir As Integer
    Public Shared XIVView As New DataView
    Public Shared XIVmap As String
    Public Shared XIVdir As Integer
    Private Shared UserName As String
    Public Shared WebAdminEmail As String = "chelsea.weirich@northside.com"
    Public Shared RowNo As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            If OhmsAdminTabs.ActiveTabIndex = 2 Then
                Dim wcICausedPostBack As WebControl = CType(GetControlThatCausedPostBack(TryCast(sender, Page)), WebControl)

                Dim indx As Integer = wcICausedPostBack.TabIndex

                Dim ctrl = _
                From control In wcICausedPostBack.Parent.Controls.OfType(Of WebControl)() _
                Where control.TabIndex > indx _
                           Select control
                ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus()

            End If


        Else
            Try
                'Dim cmd As SqlCommand
                'Dim RunString As String = "select MAX(dDate) as dDate from DWH.KPIS.DEV_OHMS_Data where Active = 1 and dDate < getdate()"
                'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                '    cmd = New SqlCommand(RunString, conn)
                '    cmd.CommandTimeout = 86400
                '    If conn.State = ConnectionState.Closed Then
                '        conn.Open()
                '    End If
                '    RunDate = cmd.ExecuteScalar

                'End Using

                refreshDateDDL()
                PopulateXRayLocations()



                'removed 2/13/2020 CRW; HH was removed in a radiology meeting yesterday, decision made by Will (because we can't automate it, no reason to make it be entered twice)
                'Bonfire has been gone for a while, wasn't sure if it was coming back; finally removed the tab

                'PopulateHHLocations()
                'PopulateBonfireLocations()

                'If ddlBonfireLocation.Items.Count() > 1 Then
                '    tpRadiologyBonfire.Visible = True
                'Else
                '    tpRadiologyBonfire.Visible = False
                'End If

                ' Removed Hand Hygiene 2/13/2020 CRW re: Radiology Meeting; decided by Will Chilvers
                'If ddlHHLocationSelect.Items.Count() > 1 Then
                '    tpHandHygiene.Visible = True
                'Else
                '    tpHandHygiene.Visible = False

                'End If

                'PopulateObservations()
                XIVFYQ()
                RRFYQ()
                PopulateLocations()
                RRWeek()


                FindValues()

            Catch ex As Exception
                'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
                'explanationlabel.DataBind()
                'ModalPopupExtender.Show()
                LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If

    End Sub

    Private Sub refreshDateDDL()

        Try

            Dim loc As String

            If ddlHHLocationSelect.SelectedValue.ToString = "" Then
                loc = "-1"
            Else
                loc = ddlHHLocationSelect.SelectedValue.ToString
            End If
            Dim adminlist As String = "select distinct dDate, convert(varchar, dDate, 107) as DisplayDate " & _
            "from DWH.KPIS.DEV_OHMS_Data d " & _
            "where d.Active = 1 and dDate < getdate() and d.MID in (2, 96) and" & _
    "	(d.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = d.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.LocationID_Limit is null and (per.MetricID_Limit = d.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (d.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = d.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = d.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "           and (d.LocID = " & loc & " or -1 = " & loc & ")" & _
    "order by dDate desc "

            ddlEntryDateSelect.DataSource = GetData(adminlist)
            ddlEntryDateSelect.DataValueField = "dDate"
            ddlEntryDateSelect.DataTextField = "DisplayDate"
            ddlEntryDateSelect.DataBind()
        Catch ex As Exception
            ExplanationLabelHH.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            ExplanationLabelHH.DataBind()
            ModalPopupExtenderHH.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub XIVFYQ()
        Try
            Dim s As String = "select 'FY ' + convert(varchar, right(FY, 2)) + ', ' + " & _
"    	case when Financial_Quarter = 1 then 'Oct - Dec'  " & _
"    		 when Financial_Quarter = 2 then 'Jan - Mar'  " & _
"    		 when Financial_Quarter = 3 then 'Apr - Jun'  " & _
"    		 when Financial_Quarter = 4 then 'Jul - Sep' end as Display, MIN(Calendar_Date) as Calendar_Date " & _
"    from DWH.dbo.DimDate dd  " & _
"    where Calendar_Date between '10/1/2015' and GETDATE() " & _
"    group by 'FY ' + convert(varchar, right(FY, 2)) + ', ' +  " & _
"    	case when Financial_Quarter = 1 then 'Oct - Dec'  " & _
"    		 when Financial_Quarter = 2 then 'Jan - Mar'  " & _
"    		 when Financial_Quarter = 3 then 'Apr - Jun'  " & _
"    		 when Financial_Quarter = 4 then 'Jul - Sep' end  " & _
" order by Calendar_Date desc  "

            ddlXIVFYQ.DataSource = GetData(s)
            ddlXIVFYQ.DataValueField = "Calendar_Date"
            ddlXIVFYQ.DataTextField = "Display"
            ddlXIVFYQ.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub RRFYQ()
        Try
            Dim s As String = "select distinct 'FY ' + convert(varchar, right(FY, 2)) + ', ' + " & _
    "	case when Financial_Quarter = 1 then 'Oct - Dec' " & _
    "		 when Financial_Quarter = 2 then 'Jan - Mar' " & _
    "		 when Financial_Quarter = 3 then 'Apr - Jun' " & _
    "		 when Financial_Quarter = 4 then 'Jul - Sep' end as Display, Financial_Quarter_Name, FY, Financial_Quarter " & _
    "from DWH.dbo.DimDate dd " & _
    "where Calendar_Date between '10/1/2015' and GETDATE() " & _
    "order by FY desc, Financial_Quarter desc "

            ddlRRFYQ.DataSource = GetData(s)
            ddlRRFYQ.DataValueField = "Financial_Quarter_Name"
            ddlRRFYQ.DataTextField = "Display"
            ddlRRFYQ.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub RRWeek()

        Try

            If ddlModality.SelectedValue = "MRI" Then
                Dim s5 As String = "select MIN(Calendar_Date) from DWH.dbo.DimDate " & _
                "where Financial_Quarter_Name = '" & Replace(ddlRRFYQ.SelectedValue.ToString, "'", "''") & "' "

                Dim ds2 As DataTable = GetData(s5)
                lblRRFakeWeek.Text = ds2.Rows(0)(0).ToString

                Dim s3 As String = "select isnull(COUNT(*), 0) from DWH.KPIS.Radiology_Repeats xiv " & _
"join DWH.dbo.DimDate dd on xiv.WeekStartDate = dd.Calendar_Date " & _
"where Financial_Quarter_Name = '" & Replace(ddlRRFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocation2.SelectedValue, "'", "''") & "' " & _
"and Modality = '" & Replace(ddlModality.SelectedValue.ToString, "'", "''") & "' and Active = 1"

                Dim s4 As String = "select xiv.WeekStartDate,  datediff(day, min(SubmittedDate), getdate()) as SubTime from DWH.KPIS.Radiology_Repeats xiv " & _
        "join DWH.dbo.DimDate dd on xiv.WeekStartDate = dd.Calendar_Date " & _
        "where Financial_Quarter_Name = '" & Replace(ddlRRFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocation2.SelectedValue, "'", "''") & "' " & _
        "and Modality = '" & Replace(ddlModality.SelectedValue.ToString, "'", "''") & "' and Active = 1 " & _
        " group by xiv.WeekStartDate"

                Dim AdminSQL As String = "select count(*) from DWH.KPIS.dev_OHMS_Admins " & _
    "where MetricID_Limit = 15 and UserLogIn = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1"

                Dim IsAdmin As Integer = GetScalar(AdminSQL)

                If GetScalar(s3) > 0 Then
                    Dim ds As DataTable = GetData(s4)
                    If ds.Rows(0)(1) < 8 Then
                        btnRepeatSubmission.Enabled = True
                        lbChangeDate.Visible = True
                        lbchangeDate2.Visible = True
                    ElseIf IsAdmin > 0 Then
                        lbChangeDate.Visible = True
                        lbchangeDate2.Visible = True
                        btnRepeatSubmission.Enabled = True
                    Else
                        lbChangeDate.Visible = False
                        lbchangeDate2.Visible = False
                        btnRepeatSubmission.Enabled = False
                    End If
                Else
                    lbChangeDate.Visible = False
                    lbchangeDate2.Visible = False
                    btnRepeatSubmission.Enabled = True
                End If

            Else

                Dim s2 As String = "select convert(varchar, dd.Calendar_Date, 107) + ' - ' + convert(varchar, dd2.Calendar_Date, 107) as DisplayDates, dd.Calendar_Date " & _
        "from  DWH.dbo.DimDate dd " & _
        "    join DWH.dbo.DimDate dd2 on dd2.Calendar_Date = dateadd(day, 6, dd.Calendar_Date) " & _
        "	and dd.FY = dd2.FY and dd.Financial_Quarter = dd2.Financial_Quarter " & _
        "where dd.Financial_Quarter_Name = '" & Replace(ddlRRFYQ.SelectedValue.ToString, "'", "''") & "' " & _
        "and dd.Calendar_Date <= GETDATE() " & _
        "and dd.Week_Day_Name = 'Sunday' " & _
        " order by dd.Calendar_Date desc "

                Dim s3 As String = "select isnull(COUNT(*), 0) from DWH.KPIS.Radiology_Repeats xiv " & _
                    "join DWH.dbo.DimDate dd on xiv.WeekStartDate = dd.Calendar_Date " & _
                    "where Financial_Quarter_Name = '" & Replace(ddlRRFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocation2.SelectedValue, "'", "''") & "' " & _
                    "and Modality = '" & Replace(ddlModality.SelectedValue.ToString, "'", "''") & "' and Active = 1"

                Dim s4 As String = "select xiv.WeekStartDate,  datediff(day, min(SubmittedDate), getdate()) as SubTime from DWH.KPIS.Radiology_Repeats xiv " & _
        "join DWH.dbo.DimDate dd on xiv.WeekStartDate = dd.Calendar_Date " & _
        "where Financial_Quarter_Name = '" & Replace(ddlRRFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocation2.SelectedValue, "'", "''") & "' " & _
        "and Modality = '" & Replace(ddlModality.SelectedValue.ToString, "'", "''") & "' and Active = 1 " & _
        " group by xiv.WeekStartDate"

                ddlRRWeekSelect.DataSource = GetData(s2)
                ddlRRWeekSelect.DataValueField = "Calendar_Date"
                ddlRRWeekSelect.DataTextField = "DisplayDates"
                ddlRRWeekSelect.DataBind()

                Dim AdminSQL As String = "select count(*) from DWH.KPIS.dev_OHMS_Admins " & _
    "where MetricID_Limit = 15 and UserLogIn = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and Active = 1"

                Dim IsAdmin As Integer = GetScalar(AdminSQL)

                If GetScalar(s3) > 0 Then
                    Dim ds As DataTable = GetData(s4)
                    ddlRRWeekSelect.SelectedValue = ds.Rows(0)(0).ToString
                    ddlRRWeekSelect.Enabled = False
                    If ds.Rows(0)(1) < 8 Then
                        lbChangeDate.Visible = True
                        lbchangeDate2.Visible = True
                        btnRepeatSubmission.Enabled = True
                    ElseIf IsAdmin > 0 Then
                        lbChangeDate.Visible = True
                        lbchangeDate2.Visible = True
                        btnRepeatSubmission.Enabled = True

                    Else
                        lbChangeDate.Visible = False
                        lbchangeDate2.Visible = False
                        btnRepeatSubmission.Enabled = False
                    End If
                Else
                    lbChangeDate.Visible = False
                    lbchangeDate2.Visible = False
                    ddlRRWeekSelect.Enabled = True
                    btnRepeatSubmission.Enabled = True
                End If


            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub PopulateXIVGrid()

        Try

            Dim cnt As String = 10
            If ddlLocationSelect.SelectedValue.ToString = "2" Then
                cnt = "20"
            ElseIf ddlLocationSelect.SelectedValue.ToString = "16" Then
                cnt = "20"
            ElseIf ddlLocationSelect.SelectedValue.ToString = "38" Then
                cnt = "20"
            End If


            Dim s As String = "select ROW_NUMBER() over (order by ID) sort, convert(varchar, ROW_NUMBER() over (order by ID)) + '.' RN, ID, CheckInNumber, Exam, Technique, Positioning, Markers, ObservationShielded, Coned, Comment " & _
    "from DWH.KPIS.Radiology_XRay_ImagingViews xiv " & _
    "where QuarterStartDate = '" & Replace(ddlXIVFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocationSelect.SelectedValue, "'", "''") & "' " & _
    "and Active = 1 " & _
    "union " & _
    "select Day_of_Calendar_Year +  (select isnull(COUNT(*), 0) from DWH.KPIS.Radiology_XRay_ImagingViews xiv " & _
    "where QuarterStartDate = '" & Replace(ddlXIVFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocationSelect.SelectedValue, "'", "''") & "' " & _
    "and Active = 1 ), convert(varchar, Day_of_Calendar_Year +  (select isnull(COUNT(*), 0) from DWH.KPIS.Radiology_XRay_ImagingViews xiv " & _
    "where QuarterStartDate = '" & Replace(ddlXIVFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocationSelect.SelectedValue, "'", "''") & "' " & _
    "and Active = 1 )) + '.' " & _
    ", -999 + Day_of_Calendar_Year, '', 'Select Exam', 'Good Detail', 'Good', 'Lead Markers'   " & _
    ", 'Yes', 'Yes', '' " & _
    "from DWH.dbo.DimDate where -1 <> " & Replace(ddlLocationSelect.SelectedValue, "'", "''") & " and FY = 2016 " & _
    "and Day_of_Calendar_Year  <= " & cnt & " - " & _
    "(select isnull(COUNT(*), 0) from DWH.KPIS.Radiology_XRay_ImagingViews xiv " & _
    "where QuarterStartDate = '" & Replace(ddlXIVFYQ.SelectedValue.ToString, "'", "''") & "' and LocID = '" & Replace(ddlLocationSelect.SelectedValue, "'", "''") & "' " & _
    "and Active = 1 )"

            XIVView = GetData(s).DefaultView
            gvXRayImagingViews.DataSource = XIVView
            gvXRayImagingViews.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub PopulateXRayLocations()

        Try
            Dim ds As New DataTable
            Dim MID As String = 0

     
            Dim locsql As String = "select distinct L.LocID, " & _
    "	CONVERT(varchar, L.Location) + ' - ' + L.LocationDesc + " & _
    "		case when L.Entity = 10 then ' (Atlanta)' " & _
    "		when L.Entity = 22 then ' (Cherokee)' " & _
    "		when L.Entity = 6 then ' (Forsyth)' " & _
    "		when L.Entity = 30 then ' (Gwinnett)' " & _
    "		when L.Entity = 40 then ' (Duluth)' " & _
    "		else ' (' + CONVERT(varchar, L.Entity) + ')' end as LName, Location " & _
    "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
    "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
    "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
    "	and getdate() between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
    "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
    "	and getdate() between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
    "where getdate() between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
    "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "			and m.Active = 1 " & _
    "           and (m.MID in (15, 220))" & _
    " union " & _
    "select -1, 'Select Location' as LName, 1 " & _
    "order by Location asc "

            ds = GetData(locsql)

            ddlLocationSelect.DataSource = ds
            ddlLocationSelect.DataValueField = "LocID"
            ddlLocationSelect.DataTextField = "LName"
            ddlLocationSelect.DataBind()

        Catch ex As Exception
            'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            'explanationlabel.DataBind()
            'ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub PopulateLocations()

        Try
            Dim ds As New DataTable
            Dim MID As String = 0

            If ddlModality.SelectedValue = "X-Ray" Then
                MID = "('15', '220')"
            ElseIf ddlModality.SelectedValue = "CT" Then
                MID = "('16', '221')"
            ElseIf ddlModality.SelectedValue = "MRI" Then
                MID = "('17', '222')"
            End If

            Dim RunDate As String = ddlRRFYQ.SelectedValue

            Dim locsql As String = "declare @RunDate date = (select  max(Calendar_Date) from DWH.DBO.DimDate where Financial_Quarter_Name = '" & RunDate & "') " & _
                " select distinct L.LocID, " & _
    "	CONVERT(varchar, L.Location) + ' - ' + L.LocationDesc + " & _
    "		case when L.Entity = 10 then ' (Atlanta)' " & _
    "		when L.Entity = 22 then ' (Cherokee)' " & _
    "		when L.Entity = 6 then ' (Forsyth)' " & _
    "		when L.Entity = 30 then ' (Gwinnett)' " & _
    "		when L.Entity = 40 then ' (Duluth)' " & _
    "		else ' (' + CONVERT(varchar, L.Entity) + ')' end as LName, Location " & _
    "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
    "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
    "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
    "	and @RunDate between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
    "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
    "	and @RunDate between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
    "where @RunDate between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
    "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "			and m.Active = 1 " & _
    "           and (m.MID in " & MID & ")" & _
    " union " & _
    "select -1, 'Select Location' as LName, 1 " & _
    "order by Location asc "

            ds = GetData(locsql)

            ddlLocation2.DataSource = ds
            ddlLocation2.DataValueField = "LocID"
            ddlLocation2.DataTextField = "LName"
            ddlLocation2.DataBind()

        Catch ex As Exception
            'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            'explanationlabel.DataBind()
            'ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub PopulateHHLocations()

        Try
            Dim ds As New DataTable

            Dim locsql As String = "select distinct L.LocID, " & _
    "	CONVERT(varchar, L.Location) + ' - ' + L.LocationDesc + " & _
    "		case when L.Entity = 10 then ' (Atlanta)' " & _
    "		when L.Entity = 22 then ' (Cherokee)' " & _
    "		when L.Entity = 6 then ' (Forsyth)' " & _
    "		else ' (' + CONVERT(varchar, L.Entity) + ')' end as LName, Location " & _
    "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
    "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
    "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
    "	and getdate() between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
    "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
    "	and getdate() between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
    "where getdate() between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
    "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "			and m.Active = 1 " & _
    "           and (m.MID in (2, 96))" & _
    " union " & _
    "select -1, 'Select Location' as LName, 1 " & _
    "order by Location asc "

            ds = GetData(locsql)

            ddlHHLocationSelect.DataSource = ds
            ddlHHLocationSelect.DataValueField = "LocID"
            ddlHHLocationSelect.DataTextField = "LName"
            ddlHHLocationSelect.DataBind()

        Catch ex As Exception
            'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            'explanationlabel.DataBind()
            'ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub PopulateBonfireLocations()

        Try
            Dim ds As New DataTable
            Dim MID As String = 0

            Dim locsql As String = "select distinct L.LocID, " & _
    "	CONVERT(varchar, L.Location) + ' - ' + L.LocationDesc + " & _
    "		case when L.Entity = 10 then ' (Atlanta)' " & _
    "		when L.Entity = 22 then ' (Cherokee)' " & _
    "		when L.Entity = 6 then ' (Forsyth)' " & _
    "		else ' (' + CONVERT(varchar, L.Entity) + ')' end as LName, Location " & _
    "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
    "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
    "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
    "	and getdate() between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
    "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
    "	and getdate() between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
    "where getdate() between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
    "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "			and m.Active = 1 " & _
    "           and (m.MID between 30 and 38)" & _
    " union " & _
    "select -1, 'Select Location' as LName, 1 " & _
    "order by Location asc "

            ds = GetData(locsql)

            ddlBonfireLocation.DataSource = ds
            ddlBonfireLocation.DataValueField = "LocID"
            ddlBonfireLocation.DataTextField = "LName"
            ddlBonfireLocation.DataBind()

        Catch ex As Exception
            'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            'explanationlabel.DataBind()
            'ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub FindValues()

        Try
            If ddlEntryDateSelect.SelectedValue <> "" Then

                Dim SelectedDate As Date = ddlEntryDateSelect.SelectedValue

                Dim Perc As Integer = 0

                Dim FindSCL As String = "select dID, Name as [Metric Name], Entity, Location, LocationDesc, convert(float, Numerator) [CurrentNumerator] " & _
                    ", case when DataType <> 'percent' then 'N/A' else convert(varchar, convert(float, Denominator)) end [Current Denominator], DataType " & _
                    ", convert(float,case when m.DataType = 'percent' then round(d.Numerator/d.Denominator*100, 2) else d.Numerator end) as [Current Value] " & _
                    ", t.Target " & _
                    ", case when case when m.DataType = 'percent' then d.Numerator/d.Denominator*100 else d.Numerator end is null then 'purple' " & _
                    "       when case when m.DataType = 'percent' then d.Numerator/d.Denominator*100 else d.Numerator end > t.RedMax then 'red' " & _
                    "	    when case when m.DataType = 'percent' then d.Numerator/d.Denominator*100 else d.Numerator end < t.RedMin then 'red' " & _
                    "	    when case when m.DataType = 'percent' then d.Numerator/d.Denominator*100 else d.Numerator end < t.wMin then 'yellow' " & _
                    "	    when case when m.DataType = 'percent' then d.Numerator/d.Denominator*100 else d.Numerator end > t.wMax then 'yellow' " & _
                    "	    else 'green' end as color, d.dDate,  convert(varchar, dDate, 107) as DisplayDate " & _
                    " ,t.RedMax, t.RedMin, t.wMin, t.wMax, m.DataType " & _
                "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
                "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
                "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
                "	and '" + SelectedDate + "' between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
                "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
                "	and '" + SelectedDate + "' between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
                "join DWH.KPIS.DEV_OHMS_Data d on d.LocID = L.LocID and d.MID = m.MID and d.Active = 1 " & _
                "	and d.dDate = '" & SelectedDate & "' " & _
                "left join DWH.KPIS.DEV_OHMS_Target t on t.LocID = L.LocID and t.MID = m.MID and t.Active = 1 " & _
                "	and d.dDate between isnull(t.TargetEffFromDate, '1/1/1800') and isnull(t.TargetEffToDate, '12/31/9999') " & _
                "where '" + SelectedDate + "' between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
                "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
                "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
                "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
                "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
                "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
                "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
                "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
                " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
                "			and m.Active = 1 " & _
                "           and (m.MID in (2, 96) )" & _
                "           and (l.LocID = '" & ddlHHLocationSelect.SelectedValue.ToString & "' or -1 = '" & ddlHHLocationSelect.SelectedValue.ToString & "')" & _
                " order by Name, Location, LocationDesc, Entity"


                AdminView = GetData(FindSCL).DefaultView
                gvSubmitData.DataSource = AdminView
                gvSubmitData.DataBind()

            End If

        Catch ex As Exception
            ExplanationLabelHH.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            ExplanationLabelHH.DataBind()
            ModalPopupExtenderHH.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlModality_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModality.SelectedIndexChanged

        Dim x As String = ddlLocation2.SelectedValue

        PopulateLocations()

        If ddlModality.SelectedValue = "MRI" Then
            HideIfMRI.Visible = False
        Else
            HideIfMRI.Visible = True
        End If

        Try
            ddlLocation2.SelectedValue = x
            RRWeek()
            If ddlLocation2.SelectedValue = "-1" Then
                gvRRTotalExposures.Visible = False
                gvRR.Visible = False
            Else
                If ddlModality.SelectedValue = "X-Ray" Then
                    gvRRTotalExposures.Visible = True
                Else
                    gvRRTotalExposures.Visible = False
                End If
                gvRR.Visible = True
                RRData()
            End If



        Catch ex As Exception
            gvRRTotalExposures.Visible = False
            gvRR.Visible = False
        End Try




    End Sub

    Protected Function GetControlThatCausedPostBack(ByVal page As Page) As Control
        Try

            Dim control As Control = Nothing
            Dim control2 As Control = Nothing



            Dim ctrlname As String = page.Request.Params.Get("__EVENTTARGET")
            Dim ctrlname2 As String
            Dim RelevantNumber As Integer

            If ctrlname IsNot Nothing AndAlso ctrlname <> String.Empty Then

                If InStr(ctrlname, "txtCurrNum") Then
                    RelevantNumber = Left(Right(ctrlname, 13), 2)
                    RowNo = RelevantNumber
                    ctrlname2 = Replace(ctrlname, "txtCurrNum", "Label1")
                    control2 = page.FindControl(ctrlname2)
                    Dim l As Label = CType(control2, Label)
                    ctrlname = Replace(ctrlname, "txtCurrNum", "txtCurrDenom")

                ElseIf InStr(ctrlname, "txtCurrDenom") Then

                    RelevantNumber = Left(Right(ctrlname, 15), 2) + 1
                    RowNo = RelevantNumber - 1
                    If Len(RelevantNumber.ToString) = 1 Then
                        ctrlname = Left(ctrlname, Len(ctrlname) - 15) & "0" & RelevantNumber.ToString & Replace(Right(ctrlname, 13), "txtCurrDenom", "txtCurrNum")
                    Else
                        ctrlname = Left(ctrlname, Len(ctrlname) - 15) & RelevantNumber.ToString & Replace(Right(ctrlname, 13), "txtCurrDenom", "txtCurrNum")
                    End If

                End If



                control = page.FindControl(ctrlname)


            Else

                For Each ctl As String In page.Request.Form

                    Dim c As Control = page.FindControl(ctl)

                    If TypeOf c Is System.Web.UI.WebControls.Button OrElse TypeOf c Is System.Web.UI.WebControls.ImageButton Then

                        control = c

                        Exit For

                    End If

                Next ctl

            End If

            Return control


        Catch ex As Exception


            Dim control As Control = Nothing



            Dim ctrlname As String = page.Request.Params.Get("__EVENTTARGET")
            Dim RelevantNumber As Integer

            If ctrlname IsNot Nothing AndAlso ctrlname <> String.Empty Then

                If InStr(ctrlname, "txtCurrNum") Then
                    RelevantNumber = Left(Right(ctrlname, 13), 2)
                    RowNo = RelevantNumber
                ElseIf InStr(ctrlname, "txtCurrDenom") Then
                    RelevantNumber = Left(Right(ctrlname, 15), 2) + 1
                    RowNo = RelevantNumber - 1

                End If

                control = page.FindControl(ctrlname)

            Else

                For Each ctl As String In page.Request.Form

                    Dim c As Control = page.FindControl(ctl)

                    If TypeOf c Is System.Web.UI.WebControls.Button OrElse TypeOf c Is System.Web.UI.WebControls.ImageButton Then

                        control = c

                        Exit For

                    End If

                Next ctl

            End If

            Return control

            'LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Function
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

    Private Sub ddlHHLocationSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHHLocationSelect.SelectedIndexChanged
        Try
            Dim z As String = ddlEntryDateSelect.SelectedValue

            refreshDateDDL()

            Try
                ddlEntryDateSelect.SelectedValue = z
            Catch ex As Exception
                ddlEntryDateSelect.SelectedIndex = 0
            End Try

            FindValues()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub UpdateHHRows()

        Try

            Dim UpdateSQL As String = ""
            Dim cnt As Integer = 0
            For Each row As GridViewRow In gvSubmitData.Rows
                If row.RowType = DataControlRowType.DataRow Then

                    Dim dID, Numerator, Denominator, OldNum, OldDenom, MetName, MetLoc As String
                    Numerator = ""
                    dID = ""
                    Denominator = ""
                    OldNum = ""
                    OldDenom = ""
                    MetName = ""
                    MetLoc = ""

                    dID = row.Cells(0).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    OldNum = row.Cells(6).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    OldDenom = row.Cells(7).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    MetName = row.Cells(1).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    MetLoc = row.Cells(4).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    Numerator = row.Cells(6).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    Denominator = row.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString

                    If Denominator = "0" Then
                        ExplanationLabelHH.Text = "Denominators may not be zero. (Check " & MetName & ", Location " & MetLoc & ")"
                        ExplanationLabelHH.DataBind()
                        ModalPopupExtenderHH.Show()
                        Exit Sub
                    End If

                    If Numerator <> OldNum Or Denominator <> OldDenom Then
                        If IsNumeric(Numerator) = False And Numerator <> "" Then
                            ExplanationLabelHH.Text = "All submitted numerators must be numeric. (Check " & MetName & ", Location " & MetLoc & ")"
                            ExplanationLabelHH.DataBind()
                            ModalPopupExtenderHH.Show()
                            Exit Sub
                        ElseIf IsNumeric(Denominator) = False And Denominator <> "" Then
                            ExplanationLabelHH.Text = "All submitted Denominators must be numeric. (Check " & MetName & ", Location " & MetLoc & ")"
                            ExplanationLabelHH.DataBind()
                            ModalPopupExtenderHH.Show()
                            Exit Sub
                        End If
                        If Numerator = "" Then
                            Numerator = "NULL"
                        End If
                        If Denominator = "" Then
                            Denominator = "NULL"
                        End If
                        UpdateSQL += "UPDATE DWH.KPIS.DEV_OHMS_Data SET Numerator = " & Numerator & ", Denominator = " & Denominator & ", ModifyDate = getdate(), ModifyUser = '" & _
                            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' WHERE dID = " & dID & "; "
                        cnt += 1
                    End If



                End If
            Next

            If cnt > 0 Then
                ExecuteSql(UpdateSQL)
            End If


            ExplanationLabelHH.Text = "Successfully Updated (" & CStr(cnt) & " rows)"
            ExplanationLabelHH.DataBind()
            ModalPopupExtenderHH.Show()


        Catch ex As Exception
            ExplanationLabelHH.Text = "Error submitting data.  Please re-check values or contact Admin"
            ExplanationLabelHH.DataBind()
            ModalPopupExtenderHH.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    'Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton2.Click
    '    Try
    '        FindValues()
    '        ModalPopupExtender.Hide()
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    Private Sub gvSubmitData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubmitData.PageIndexChanging
        Try

            If CheckBeforePageChange(e.NewPageIndex) = 0 Then
                gvSubmitData.PageIndex = e.NewPageIndex
                gvSubmitData.DataSource = AdminView
                gvSubmitData.DataBind()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Function CheckBeforePageChange(e As Integer)
        Dim UpdateSQL As String = ""
        Dim cnt As Integer = 0
        For Each row As GridViewRow In gvSubmitData.Rows
            If row.RowType = DataControlRowType.DataRow Then

                Dim Numerator, Denominator, OldNum, OldDenom As String
                Numerator = ""
                Denominator = ""
                OldNum = ""
                OldDenom = ""

                OldNum = row.Cells(6).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                OldDenom = row.Cells(7).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                Numerator = row.Cells(6).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                Denominator = row.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString


                If Numerator <> OldNum Or Denominator <> OldDenom Then
                    cnt += 1
                End If

            End If
        Next

        If cnt > 0 Then
            hiddenLblpass.Text = e.ToString
            ExplanationLabelHHPC.Text = cnt.ToString & " rows of data have been entered; if you change the page, these changes will be lost."
            ExplanationLabelHHPC.DataBind()
            ModalPopupExtenderHH2.Show()
            Return 1
        Else
            Return 0
        End If

    End Function

    Private Sub gvSubmitData_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSubmitData.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Cells(9).CssClass = "hidden"

            End If

        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSubmitData_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSubmitData.Sorting

        Try

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

            gvSubmitData.DataSource = dv
            gvSubmitData.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub CancelButtonHHPageChange_Click(sender As Object, e As EventArgs) Handles CancelButtonHHPageChange.Click
        ModalPopupExtenderHH2.Hide()
    End Sub
    Private Sub SubmitButtonHHPageChange_Click(sender As Object, e As EventArgs) Handles SubmitButtonHHPageChange.Click
        ModalPopupExtenderHH2.Hide()
        UpdateHHRows()
        Dim e2 As Integer
        e2 = hiddenLblpass.Text
        gvSubmitData.PageIndex = e2
        gvSubmitData.DataSource = AdminView
        gvSubmitData.DataBind()
    End Sub
    Private Sub btnChangePage_Click(sender As Object, e As EventArgs) Handles btnChangePage.Click
        Try
            Dim e2 As Integer
            e2 = hiddenLblpass.Text
            gvSubmitData.PageIndex = e2
            gvSubmitData.DataSource = AdminView
            gvSubmitData.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnHHUpdateRows_Click(sender As Object, e As EventArgs) Handles btnHHUpdateRows.Click
        UpdateHHRows()
    End Sub
    Public Sub ChangeCurrentValue(sender As Object, e As EventArgs)

        Try


            'Dim RowNo As Integer = 2

            Dim UpdateSQL As String = ""
            Dim cnt As Integer = 0

            Dim tb As TextBox = sender
            Dim row As GridViewRow = tb.Parent.Parent

            'Dim row As GridViewRow = gvSubmitData.Rows(RowNo - 2)
            If row.RowType = DataControlRowType.DataRow Then

                Dim dID, Numerator, Denominator, CurrentVal, Target, RedMax, RedMin, wMax, wMin, DataType As String
                Dim Result, dRedMax, dRedMin, dwMax, dwMin As Decimal
                Numerator = ""
                dID = ""
                Denominator = ""
                CurrentVal = ""
                RedMax = ""
                RedMin = ""
                wMax = ""
                wMin = ""
                Dim CurrentValLabel As Label = row.Cells(8).Controls.OfType(Of Label)().FirstOrDefault()

                'dID = row.Cells(0).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                'OldNum = row.Cells(6).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                'OldDenom = row.Cells(7).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                'MetName = row.Cells(1).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                'MetLoc = row.Cells(4).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                'If row.DataItem("Metric Name") = "Total Collections (POS and Pre-Reg)" Then
                '    row.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = False
                'End If

                Target = row.Cells(5).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                Numerator = row.Cells(6).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                Denominator = row.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                CurrentVal = CurrentValLabel.Text.ToString
                RedMax = Replace(row.Cells(11).Text.ToString, "&nbsp;", "")
                If RedMax <> "" Then
                    dRedMax = Convert.ToDecimal(RedMax)
                Else
                    dRedMax = 0
                End If
                RedMin = Replace(row.Cells(12).Text.ToString, "&nbsp;", "")
                If RedMin <> "" Then
                    dRedMin = Convert.ToDecimal(RedMin)
                Else
                    dRedMin = 0
                End If
                wMax = Replace(row.Cells(13).Text.ToString, "&nbsp;", "")
                If wMax <> "" Then
                    dwMax = Convert.ToDecimal(wMax)
                Else
                    dwMax = 0
                End If
                wMin = Replace(row.Cells(14).Text.ToString, "&nbsp;", "")
                If wMin <> "" Then
                    dwMin = Convert.ToDecimal(wMin)
                Else
                    dwMin = 0
                End If
                DataType = Replace(row.Cells(15).Text.ToString, "&nbsp;", "")

                If DataType = "percent" Then
                    If IsNumeric(Denominator) And IsNumeric(Numerator) Then
                        If Denominator = "0" Then
                            ExplanationLabelHH.Text = "Denominators may not be zero."
                            ExplanationLabelHH.DataBind()
                            ModalPopupExtenderHH.Show()
                            Exit Sub
                        Else
                            Result = Numerator / Denominator * 100

                            If RedMax <> "" And Result > dRedMax Then
                                row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                            ElseIf RedMin <> "" And Result < dRedMin Then
                                row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                            ElseIf wMax <> "" And Result > dwMax Then
                                row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                            ElseIf wMin <> "" And Result < dwMin Then
                                row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                            Else
                                row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#b7ffb7")
                            End If

                            CurrentValLabel.Text = Round(Result, 2)
                        End If
                    Else
                        CurrentValLabel.Text = "Error"
                        row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#c793f2")
                    End If
                Else

                    If Denominator <> "N/A" Then
                        ExplanationLabelHH.Text = "Denominators not used for this metric."
                        ExplanationLabelHH.DataBind()
                        ModalPopupExtenderHH.Show()
                        Exit Sub
                    ElseIf IsNumeric(Numerator) Then
                        Result = Numerator

                        If RedMax <> "" And Result > dRedMax Then
                            row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                        ElseIf RedMin <> "" And Result < dRedMin Then
                            row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                        ElseIf wMax <> "" And Result > dwMax Then
                            row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                        ElseIf wMin <> "" And Result < dwMin Then
                            row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                        Else
                            row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#b7ffb7")
                        End If

                        CurrentValLabel.Text = Round(Result, 2)
                    Else
                        CurrentValLabel.Text = "Error"
                        row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#c793f2")
                    End If
                End If

            End If
            'Next

            Dim enabling As String

            For Each rowEnabled As GridViewRow In gvSubmitData.Rows
                enabling = rowEnabled.Cells(1).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                If rowEnabled.Cells(1).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString = "POS Collection Information" Then
                    rowEnabled.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = False
                End If
            Next

        Catch ex As Exception
            ExplanationLabelHH.Text = "Error submitting data.  Please re-check values or contact Admin"
            ExplanationLabelHH.DataBind()
            ModalPopupExtenderHH.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlEntryDateSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEntryDateSelect.SelectedIndexChanged
        Try

            FindValues()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnRepeatSubmission_Click(sender As Object, e As EventArgs) Handles btnRepeatSubmission.Click

        ExplanationLabelRR.Text = "You are trying to submit " & ddlModality.SelectedValue & " data for <br>Location " & ddlLocation2.SelectedItem.Text & " for <br>Period "
        If ddlModality.SelectedValue = "MRI" Then
            ExplanationLabelRR.Text = ExplanationLabelRR.Text & ddlRRFYQ.SelectedItem.Text
        Else
            ExplanationLabelRR.Text = ExplanationLabelRR.Text & ddlRRWeekSelect.SelectedItem.Text
        End If
        ExplanationLabelRR.Text = ExplanationLabelRR.Text & " <br> -- Please confirm this is the correct data"
        btnCancel.Visible = True
        btnRRConfirm.Text = "Confirm and Submit"
        btnRRConfirm.Visible = True
        btnOKAYRR.Visible = False

        ModalPopupExtenderRR.Show()

    End Sub
    Private Sub SubmitRepeats()

        Try

            Dim wk As String

            If ddlModality.SelectedValue = "MRI" Then
                wk = lblRRFakeWeek.Text.ToString
            Else
                wk = ddlRRWeekSelect.SelectedValue.ToString
            End If

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim UpdatesSql As String = ""

            If ddlLocation2.SelectedValue = -1 Then
                ExplanationLabelRR.Text = "No Repeat Reasons to Submit; Please select a Valid Location"
                ModalPopupExtenderRR.Show()
                Exit Sub
            End If

            For i As Integer = 0 To gvRR.Rows.Count - 1
                Dim RRID As Label = CType(gvRR.Rows(i).FindControl("lblRRID"), Label)
                Dim RRReason As Label = CType(gvRR.Rows(i).FindControl("lblRRReason"), Label)
                Dim RRCount As TextBox = CType(gvRR.Rows(i).FindControl("txtRRCount"), TextBox)
                Dim RRComments As TextBox = CType(gvRR.Rows(i).FindControl("txtRRComment"), TextBox)

                Dim OldCount As Label = CType(gvRR.Rows(i).FindControl("lblRRCount"), Label)
                Dim OldComment As Label = CType(gvRR.Rows(i).FindControl("lblRRComment"), Label)

                If RRReason.Text = "Other" And RRCount.Text <> "0" Then
                    Try
                        If Replace(RRComments.Text.ToString, "'", "''").Trim = "" Then
                            ExplanationLabelRR.Text = "Reason 'Other' Requires an explanation in the Comment Section."
                            ModalPopupExtenderRR.Show()
                            Exit Sub
                        End If
                    Catch ex As Exception
                        ExplanationLabelRR.Text = "Reason 'Other' Requires an explanation in the Comment Section."
                        ModalPopupExtenderRR.Show()
                        Exit Sub
                    End Try

                End If

                If CInt(RRID.Text) < 0 Then
                    UpdatesSql += "Insert into DWH.KPIS.Radiology_Repeats values (getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                            "', '" & UserName & "', '" & Replace(wk, "'", "''") & "', '" & _
                            Replace(ddlLocation2.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlModality.SelectedValue.ToString, "'", "''") & _
                            "', '" & Replace(RRReason.Text, "'", "''") & "', '" & Replace(RRCount.Text.ToString, "'", "''") & _
                            "', '" & Replace(RRComments.Text.ToString, "'", "''") & "', 1, null, null); "
                Else
                    Dim NewRow As Integer = 0
                    If RRCount.Text <> OldCount.Text Then
                        NewRow = 1
                    ElseIf RRComments.Text <> OldComment.Text Then
                        NewRow = 1
                    End If
                    If NewRow > 0 Then
                        UpdatesSql += "Update DWH.KPIS.Radiology_Repeats set RepeatCount = '" & Replace(RRCount.Text.ToString, "'", "''") & _
                            "', Comments = '" & Replace(RRComments.Text, "'", "''") & _
                            "', ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                            "' where ID = " & Replace(RRID.Text, "'", "''") & "; "
                    End If
                End If

            Next

            If ddlModality.SelectedValue = "X-Ray" Then

                For i As Integer = 0 To gvRRTotalExposures.Rows.Count - 1
                    Dim RRID As Label = CType(gvRRTotalExposures.Rows(i).FindControl("lblRRID"), Label)
                    Dim RRReason As Label = CType(gvRRTotalExposures.Rows(i).FindControl("lblRRReason"), Label)
                    Dim RRCount As TextBox = CType(gvRRTotalExposures.Rows(i).FindControl("txtRRCount"), TextBox)
                    Dim RRComments As TextBox = CType(gvRRTotalExposures.Rows(i).FindControl("txtRRComment"), TextBox)

                    Dim OldCount As Label = CType(gvRRTotalExposures.Rows(i).FindControl("lblRRCount"), Label)
                    Dim OldComment As Label = CType(gvRRTotalExposures.Rows(i).FindControl("lblRRComment"), Label)

                    If CInt(RRID.Text) < 0 Then
                        UpdatesSql += "Insert into DWH.KPIS.Radiology_Repeats values (getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                                "', '" & UserName & "', '" & Replace(wk, "'", "''") & "', '" & _
                                Replace(ddlLocation2.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlModality.SelectedValue.ToString, "'", "''") & _
                                "', '" & Replace(RRReason.Text, "'", "''") & "', '" & Replace(RRCount.Text.ToString, "'", "''") & _
                                "', '" & Replace(RRComments.Text.ToString, "'", "''") & "', 1, null, null); "
                    Else
                        Dim NewRow As Integer = 0
                        If RRCount.Text <> OldCount.Text Then
                            NewRow = 1
                        ElseIf RRComments.Text <> OldComment.Text Then
                            NewRow = 1
                        End If
                        If NewRow > 0 Then
                            UpdatesSql += "Update DWH.KPIS.Radiology_Repeats set RepeatCount = '" & Replace(RRCount.Text.ToString, "'", "''") & _
                                "', Comments = '" & Replace(RRComments.Text, "'", "''") & _
                                "', ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                                "' where ID = " & Replace(RRID.Text, "'", "''") & "; "
                        End If
                    End If

                Next
            End If

            If Len(UpdatesSql) = 0 Then
                ExplanationLabelRR.Text = "No Modifications to Repeat Reasons"
                ModalPopupExtenderRR.Show()
                Exit Sub
            End If

            ExecuteSql(UpdatesSql)

            Dim y As String = ddlRRFYQ.SelectedValue
            Dim z As String = ddlRRWeekSelect.SelectedValue

            RRFYQ()
           
            Try
                ddlRRFYQ.SelectedValue = y
            Catch ex As Exception

            End Try

            RRWeek()

            Try
                ddlRRWeekSelect.SelectedValue = z
            Catch ex As Exception

            End Try
            If ddlLocation2.SelectedValue = "-1" Then
                gvRRTotalExposures.Visible = False
                gvRR.Visible = False
            Else
                RRData()
                If ddlModality.SelectedValue = "X-Ray" Then
                    gvRRTotalExposures.Visible = True
                Else
                    gvRRTotalExposures.Visible = False
                End If
                gvRR.Visible = True
            End If


            ExplanationLabelRR.Text = "Repeat Reasons Successfully Submitted."
            ModalPopupExtenderRR.Show()

        Catch ex As Exception
            ExplanationLabelRR.Text = "Error Submitting Data; Please report to admin."
            ModalPopupExtenderRR.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnSubmitObservation_Click(sender As Object, e As EventArgs) Handles btnSubmitObservation.Click

        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim UpdatesSql As String = ""

            If ddlLocationSelect.SelectedValue = -1 Then
                explanationlabel.Text = "No X-Ray Imaging Views to Submit; Please select a Valid Location"
                ModalPopupExtender.Show()
                Exit Sub
            End If

            For i As Integer = 0 To gvXRayImagingViews.Rows.Count - 1
                Dim XIVID As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVID"), Label)
                Dim XIVCheckIn As TextBox = CType(gvXRayImagingViews.Rows(i).FindControl("txtXIVCheckInNumber"), TextBox)
                Dim XIVExam As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVExam"), DropDownList)
                Dim XIVTechnique As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVTechnique"), DropDownList)
                Dim XIVPositioning As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVPositioning"), DropDownList)
                Dim XIVMarkers As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVMarkers"), DropDownList)
                Dim XIVObservation As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVObservation"), DropDownList)
                Dim XIVConed As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVConed"), DropDownList)
                Dim txtComment As TextBox = CType(gvXRayImagingViews.Rows(i).FindControl("txtXIVComment"), TextBox)

                Dim OldCheckIn As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVCheckInNumber"), Label)
                Dim OldExam As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVExam"), Label)
                Dim OldTechnique As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVTechnique"), Label)
                Dim OldPositioning As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVPositioning"), Label)
                Dim OldMarkers As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVMarkers"), Label)
                Dim OldObservation As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVShielding"), Label)
                Dim OldConed As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVConed"), Label)
                Dim OldComment As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVComment"), Label)

                If CInt(XIVID.Text) < 0 Then
                    If XIVExam.SelectedValue.ToString <> "Select Exam" Then
                        UpdatesSql += "Insert into DWH.KPIS.Radiology_XRay_ImagingViews values (getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                            "', '" & UserName & "', '" & Replace(ddlXIVFYQ.SelectedValue.ToString, "'", "''") & "', '" & _
                            Replace(ddlLocationSelect.SelectedValue.ToString, "'", "''") & "', '" & Replace(XIVExam.SelectedValue.ToString, "'", "''") & _
                            "', '" & Replace(XIVCheckIn.Text, "'", "''") & "', '" & Replace(XIVTechnique.SelectedValue.ToString, "'", "''") & _
                            "', '" & Replace(XIVPositioning.SelectedValue.ToString, "'", "''") & _
                            "', '" & Replace(XIVMarkers.SelectedValue.ToString, "'", "''") & "', '" & Replace(XIVObservation.SelectedValue.ToString, "'", "''") & _
                            "', '" & Replace(XIVConed.SelectedValue.ToString, "'", "''") & "', '" & Replace(txtComment.Text, "'", "''") & "', 1, null, null); "
                    End If
                Else
                    Dim NewRow As Integer = 0
                    If XIVCheckIn.Text <> OldCheckIn.Text Then
                        NewRow = 1
                    ElseIf XIVExam.SelectedValue <> OldExam.Text Then
                        NewRow = 1
                    ElseIf XIVTechnique.SelectedValue <> OldTechnique.Text Then
                        NewRow = 1
                    ElseIf XIVPositioning.SelectedValue <> OldPositioning.Text Then
                        NewRow = 1
                    ElseIf XIVMarkers.SelectedValue <> OldMarkers.Text Then
                        NewRow = 1
                    ElseIf XIVObservation.SelectedValue <> OldObservation.Text Then
                        NewRow = 1
                    ElseIf XIVConed.SelectedValue <> OldConed.Text Then
                        NewRow = 1
                    ElseIf txtComment.Text <> OldComment.Text Then
                        NewRow = 1
                    End If
                    If NewRow > 0 Then
                        UpdatesSql += "Update DWH.KPIS.Radiology_XRay_ImagingViews set Exam = '" & Replace(XIVExam.SelectedValue.ToString, "'", "''") & _
                            "', CheckInNumber = '" & Replace(XIVCheckIn.Text, "'", "''") & "', Technique = '" & Replace(XIVTechnique.SelectedValue.ToString, "'", "''") & _
                            "', Positioning = '" & Replace(XIVPositioning.SelectedValue.ToString, "'", "''") & "', Markers = '" & _
                            Replace(XIVMarkers.SelectedValue.ToString, "'", "''") & "', ObservationShielded = '" & Replace(XIVObservation.SelectedValue.ToString, "'", "''") & _
                            "', Coned = '" & Replace(XIVConed.SelectedValue.ToString, "'", "''") & "', Comment = '" & Replace(txtComment.Text, "'", "''") & _
                            "', ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                            "' where ID = " & Replace(XIVID.Text, "'", "''") & "; "
                    End If
                End If

            Next


            'Dim CheckCount As String = "select COUNT(*) from DWH.KPIS.Radiology_MRIObservations m " & _
            '    "join DWH.dbo.DimDate dd on dd.Calendar_Date = m.ExamDate " & _
            '    "join DWH.dbo.DimDate dd2 on dd.Calendar_Date = '" & Replace(txtExamDate.Text, "'", "''") & "' and dd.FY = dd2.FY and dd.Financial_Quarter = dd2.Financial_Quarter " & _
            '    "where Active = 1 " & _
            '    "and LocID = '" & Replace(ddlLocationSelect.SelectedValue.ToString, "'", "''") & "' "

            'If GetScalar(CheckCount) > 9 Then
            '    explanationlabel.Text = "10 X-Ray Imaging Views have already been submitted for this Quarter at this Location."
            '    ModalPopupExtender.Show()

            '    Exit Sub
            'End If

            'Dim SubmitSQL As String = "Insert into DWH.KPIS.Radiology_MRIObservations values (getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            '"', '" & UserName & "', '" & Replace(txtExamDate.Text, "'", "''") & "', '" & _
            'Replace(ddlLocationSelect.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlExamType.SelectedValue.ToString, "'", "''") & "', '" & _
            '"', '" & Replace(txtCheckInNumber.Text, "'", "''") & "', '" & _
            'Replace(ddlTechnique.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlPositioning.SelectedValue.ToString, "'", "''") & "', '" & _
            'Replace(ddlMarkers.SelectedValue.ToString, "'", "''") & "', '" & Replace(ddlObservation.SelectedValue.ToString, "'", "''") & "', '" & _
            'Replace(ddlConed.SelectedValue.ToString, "'", "''") & "', '" & Replace(txtObservComments.Text, "'", "''") & "', 1, null, null)"

            If Len(UpdatesSql) = 0 Then
                explanationlabel.Text = "No Modifications to X-Ray Imaging Views"
                ModalPopupExtender.Show()
                Exit Sub
            End If

            ExecuteSql(UpdatesSql)
            PopulateXIVGrid()
            PopulateObservations(1)

            explanationlabel.Text = "X-Ray Imaging Views Successfully Submitted."
            ModalPopupExtender.Show()

        Catch ex As Exception
            explanationlabel.Text = "Error Submitting Data; Please report to admin."
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub PopO()
        PopulateObservations(2)
    End Sub
    Sub PopulateObservations(Optional x As Integer = 2)

        Try
            Dim chk As DropDownList
            Dim obs As DropDownList
            Dim ds1 As New DataTable
            Dim ds2 As New DataTable
            Dim submitsql2 As String
            Dim tempholder As String

            Dim submitsql1 As String = "select 'Yes' as Result " & _
                "union " & _
                "select 'No' "

            If ddlLocationSelect.SelectedValue.ToString = "2" Then
                submitsql2 = "select 'Yes (Nursery)' as Result " & _
                "union " & _
                    "select 'No  (Nursery)' " & _
                    "union " & _
                    "select 'N/A' "
            ElseIf ddlLocationSelect.SelectedValue.ToString = "16" Then
                submitsql2 = "select 'Yes (Nursery)' as Result " & _
                    "union " & _
                    "select 'No  (Nursery)' " & _
                    "union " & _
                    "select 'N/A' "
            ElseIf ddlLocationSelect.SelectedValue.ToString = "26" Then
                submitsql2 = "select 'Yes (Nursery)' as Result " & _
                    "union " & _
                    "select 'No  (Nursery)' " & _
                    "union " & _
                    "select 'N/A' "
            ElseIf ddlLocationSelect.SelectedValue.ToString = "38" Then
                submitsql2 = "select 'Yes (Nursery)' as Result " & _
                    "union " & _
                    "select 'No  (Nursery)' " & _
                    "union " & _
                    "select 'N/A' "
            Else
                submitsql2 = "select 'N/A' as Result "
            End If

            ds1 = GetData(submitsql1)
            ds2 = GetData(submitsql2)

            'ds1.TableName = "SomeName1"
            'ds2.TableName = "SomeName2"


            For i As Integer = 0 To gvXRayImagingViews.Rows.Count - 1
                chk = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVExam"), DropDownList)
                obs = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVObservation"), DropDownList)
                If x = 1 Then
                    tempholder = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVShielding"), Label).Text
                Else
                    tempholder = obs.SelectedValue
                End If

                If chk.SelectedValue = "Abdomen" Then
                    obs.DataSource = ds2
                Else
                    obs.DataSource = ds1
                End If
                obs.DataValueField = "Result"
                obs.DataTextField = "Result"
                obs.DataBind()
                obs.SelectedValue = tempholder
            Next
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlExamType_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateObservations()
    End Sub

    Private Sub ddlXIVFYQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlXIVFYQ.SelectedIndexChanged

        PopulateXIVGrid()
        PopulateXRayLocations()
        PopulateObservations(1)
    End Sub

    Private Sub ddlLocationSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationSelect.SelectedIndexChanged
        PopulateXIVGrid()
        PopulateXRayLocations()
        PopulateObservations(1)
    End Sub

    Private Sub gvXRayImagingViews_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvXRayImagingViews.PageIndexChanging
        Try

            If CheckBeforePageChange(e.NewPageIndex) = 0 Then
                gvXRayImagingViews.PageIndex = e.NewPageIndex
                gvXRayImagingViews.DataSource = XIVView
                gvXRayImagingViews.DataBind()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvXRayImagingViews_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvXRayImagingViews.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = XIVView

            sorts = e.SortExpression

            If e.SortExpression = XIVmap Then

                If XIVdir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    XIVdir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    XIVdir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                XIVdir = 1
                XIVmap = e.SortExpression
            End If

            gvXRayImagingViews.DataSource = dv
            gvXRayImagingViews.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlRRFYQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRRFYQ.SelectedIndexChanged

        Dim sv As Int16 = ddlLocation2.SelectedValue

        PopulateLocations()

        Try
            ddlLocation2.SelectedValue = sv
        Catch ex As Exception
            Exit Sub
        End Try

        RRWeek()

        If ddlLocation2.SelectedValue = "-1" Then
            gvRRTotalExposures.Visible = False
            gvRR.Visible = False
        Else
            If ddlModality.SelectedValue = "X-Ray" Then
                gvRRTotalExposures.Visible = True
            Else
                gvRRTotalExposures.Visible = False
            End If
            gvRR.Visible = True
            RRData()
        End If

    End Sub

    Sub RRData()

        If ddlLocation2.SelectedValue = -1 Then
            Exit Sub
        End If

        Dim wk As String

        If ddlModality.SelectedValue = "MRI" Then
            wk = lblRRFakeWeek.Text
        Else
            wk = ddlRRWeekSelect.SelectedValue
        End If

        Dim sql As String = "declare @Modality varchar(50) = '" & Replace(ddlModality.SelectedValue, "'", "''") & "' " & _
"select convert(varchar, ROW_NUMBER() over (Order by Ordering, Reason)) + '.' as RN, x.*, RepeatCount, Comments, isnull(ID, -999) as ID, ROW_NUMBER() over (Order by Ordering, Reason) as RealOrder " & _
"from " & _
"( " & _
"select 'Artifact' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray') " & _
"union " & _
"select 'Clipped Anatomy' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray', 'CT', 'MRI') " & _
"union " & _
"select 'Equipment/Injector Failure' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray', 'CT', 'MRI') " & _
"union " & _
"select 'Incorrect Protocol' as Reason, 1 as Ordering " & _
"where @Modality in ('MRI') " & _
"union " & _
"select 'Oral Contrast Problem' as Reason, 1 as Ordering " & _
"where @Modality in ('CT', 'MRI') " & _
"union " & _
"select 'Poor Circulation Time' as Reason, 1 as Ordering " & _
"where @Modality in ('CT') " & _
"union " & _
"select 'Residual Contrast' as Reason, 1 as Ordering " & _
"where @Modality in ('CT') " & _
"union " & _
"select 'Respiratory Gating Problem' as Reason, 1 as Ordering " & _
"where @Modality in ('CT') " & _
"union " & _
"select 'Respiratory Problem' as Reason, 1 as Ordering " & _
"where @Modality in ('MRI') " & _
"union " & _
"select 'Scout' as Reason, 1 as Ordering " & _
"where @Modality in ('CT') " & _
"union " & _
"select 'Motion' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray', 'CT' ) /* MRI removed 4/14/2017 per 3/29/2017 email from Christine Reid */ " & _
"union " & _
"select 'Technique' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray') " & _
"union " & _
"select 'Positioning' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray', 'CT', 'MRI') " & _
"union " & _
"select 'Wrong Side / Wrong Exam' as Reason, 1 as Ordering " & _
"where @Modality in ('X-Ray', 'CT', 'MRI') " & _
"union " & _
"select 'Infiltrate/Extravasation' as Reason, 1 as Ordering " & _
"where @Modality in ('MRI', 'CT') " & _
"union " & _
"select 'Other' as Reason, 2 as Ordering " & _
"where @Modality in ('MRI') " & _
") x " & _
"left join DWH.KPIS.Radiology_Repeats rr on x.Reason = rr.RepeatReason " & _
"	and LocID = '" & Replace(ddlLocation2.SelectedValue, "'", "''") & "' and Modality = @Modality and rr.Active = 1 " & _
"	and WeekStartDate = '" & Replace(wk, "'", "''") & "' " & _
"order by RealOrder, 2 "

        gvRR.DataSource = GetData(sql)
        gvRR.DataBind()

        If ddlModality.SelectedValue = "X-Ray" Then
            Dim xraysql As String = "select convert(varchar, ROW_NUMBER() over (Order by Ordering, Reason)) + '.' as RN, x.*, RepeatCount, Comments, isnull(ID, -999) as ID " & _
            "from " & _
            "( select 'Total # of Weekly Exposures' as Reason, 3 as Ordering  ) x " & _
            "left join DWH.KPIS.Radiology_Repeats rr on x.Reason = rr.RepeatReason " & _
            "	and LocID = '" & Replace(ddlLocation2.SelectedValue, "'", "''") & "' and Modality = 'X-Ray' and rr.Active = 1 " & _
            "	and WeekStartDate = '" & Replace(wk, "'", "''") & "' " & _
            "order by 2, 1 "

            gvRRTotalExposures.DataSource = GetData(xraysql)
            gvRRTotalExposures.DataBind()
            gvRRTotalExposures.Visible = True
        Else
            gvRRTotalExposures.Visible = False
        End If

    End Sub

    Private Sub ddlLocation2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation2.SelectedIndexChanged
        If ddlLocation2.SelectedValue = "-1" Then
            gvRRTotalExposures.Visible = False
            gvRR.Visible = False
        Else
            If ddlModality.SelectedValue = "X-Ray" Then
                gvRRTotalExposures.Visible = True
            Else
                gvRRTotalExposures.Visible = False
            End If

            gvRR.Visible = True
            RRFYQ()
            RRWeek()
            RRData()
        End If

    End Sub

    Private Sub ddlRRWeekSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRRWeekSelect.SelectedIndexChanged
        If ddlLocation2.SelectedValue = "-1" Then
            gvRRTotalExposures.Visible = False
            gvRR.Visible = False
        Else
            If ddlModality.SelectedValue = "X-Ray" Then
                gvRRTotalExposures.Visible = True
            Else
                gvRRTotalExposures.Visible = False
            End If
            gvRR.Visible = True
            RRData()
        End If

    End Sub


    Private Sub btnRRConfirm_Click(sender As Object, e As EventArgs) Handles btnRRConfirm.Click
        ModalPopupExtenderRR.Hide()
        btnRRConfirm.Visible = False
        btnOKAYRR.Visible = True
        btnCancel.Visible = False
        SubmitRepeats()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ModalPopupExtenderRR.Hide()
        btnRRConfirm.Visible = False
        btnOKAYRR.Visible = True
        btnCancel.Visible = False
    End Sub



    Private Sub lbChangeDate_Click(sender As Object, e As EventArgs) Handles lbChangeDate.Click

        ChangeDate()

    End Sub

    Sub ChangeDate()

        Try

            Dim ids As String = "("

            For i As Integer = 0 To gvRR.Rows.Count - 1
                Dim RRID As Label = CType(gvRR.Rows(i).FindControl("lblRRID"), Label)
                ids = ids & RRID.Text & ", "
            Next

            ids = ids.Substring(0, Len(ids) - 2) & ")"

            Dim s As String = "select distinct 'FY ' + convert(varchar, right(FY, 2)) + ', ' + " & _
"	case when Financial_Quarter = 1 then 'Oct - Dec' " & _
"		 when Financial_Quarter = 2 then 'Jan - Mar' " & _
"		 when Financial_Quarter = 3 then 'Apr - Jun' " & _
"		 when Financial_Quarter = 4 then 'Jul - Sep' end as Display, Financial_Quarter_Name, FY, Financial_Quarter " & _
"from DWH.dbo.DimDate dd " & _
"where Calendar_Date between '10/1/2015' and GETDATE() " & _
"and not exists (" & _
"select distinct dd2.fy, dd2.Financial_Quarter from DWH.KPIS.Radiology_Repeats a " & _
"join DWH.KPIS.Radiology_Repeats b " & _
"on a.Modality = b.Modality and a.LocID = b.LocID " & _
"and a.WeekStartDate <> b.WeekStartDate " & _
"join dwh.dbo.DimDate dd2 on a.WeekStartDate = dd2.Calendar_Date " & _
"where a.Active = 1 and b.Active = 1 and dd2.FY = dd.FY and dd2.Financial_Quarter = dd.Financial_Quarter and b.ID in " & ids & ") " & _
"order by FY desc, Financial_Quarter desc "

            ddlCorrectDateFYQ.DataSource = GetData(s)
            ddlCorrectDateFYQ.DataValueField = "Financial_Quarter_Name"
            ddlCorrectDateFYQ.DataTextField = "Display"
            ddlCorrectDateFYQ.DataBind()

            RRCorrectWeek()

            Dim s4 As String = "select distinct xiv.WeekStartDate from DWH.KPIS.Radiology_Repeats xiv " & _
                "where xiv.ID in " & ids & " and Active = 1 "

            Dim ds As DataTable = GetData(s4)
            ddlCorrectDateWeek.SelectedValue = ds.Rows(0)(0).ToString


        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

        ModalPopupWrongDate.Show()

    End Sub
    Sub RRCorrectWeek()

        Try

            If ddlModality.SelectedValue = "MRI" Then
                Dim s5 As String = "select MIN(Calendar_Date) from DWH.dbo.DimDate " & _
                "where Financial_Quarter_Name = '" & Replace(ddlCorrectDateFYQ.SelectedValue.ToString, "'", "''") & "' "

                Dim ds2 As DataTable = GetData(s5)
                lblFakeCorrectWeek.Text = ds2.Rows(0)(0).ToString
                ddlCorrectDateWeek.Visible = False
            Else
                ddlCorrectDateWeek.Visible = True
                Dim s2 As String = "select convert(varchar, dd.Calendar_Date, 107) + ' - ' + convert(varchar, dd2.Calendar_Date, 107) as DisplayDates, dd.Calendar_Date " & _
        "from  DWH.dbo.DimDate dd " & _
        "    join DWH.dbo.DimDate dd2 on dd2.Calendar_Date = dateadd(day, 6, dd.Calendar_Date) " & _
        "	and dd.FY = dd2.FY and dd.Financial_Quarter = dd2.Financial_Quarter " & _
        "where dd.Financial_Quarter_Name = '" & Replace(ddlCorrectDateFYQ.SelectedValue.ToString, "'", "''") & "' " & _
        "and dd.Calendar_Date <= GETDATE() " & _
        "and dd.Week_Day_Name = 'Sunday' " & _
        " order by dd.Calendar_Date desc "

                ddlCorrectDateWeek.DataSource = GetData(s2)
                ddlCorrectDateWeek.DataValueField = "Calendar_Date"
                ddlCorrectDateWeek.DataTextField = "DisplayDates"
                ddlCorrectDateWeek.DataBind()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnCorrectDateConfirm_Click(sender As Object, e As EventArgs) Handles btnCorrectDateConfirm.Click

        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim ids As String = "("

            For i As Integer = 0 To gvRR.Rows.Count - 1
                Dim RRID As Label = CType(gvRR.Rows(i).FindControl("lblRRID"), Label)
                ids = ids & RRID.Text & ", "
            Next

            ids = ids.Substring(0, Len(ids) - 2) & ")"

            Dim wk As String
            If ddlModality.SelectedValue = "MRI" Then
                wk = lblFakeCorrectWeek.Text.ToString
            Else
                wk = ddlCorrectDateWeek.SelectedValue.ToString
            End If

            Dim updatesql As String = "Update DWH.KPIS.Radiology_Repeats set WeekStartDate = '" & Replace(wk, "'", "''") & "', ModifyDate = getdate(), ModifyUser = '" & UserName & "'" & _
                "where ID in " & ids

            ExecuteSql(updatesql)

            ExplanationLabelRR.Text = "Repeat Reason Date Successfully Changed."

            ddlRRFYQ.SelectedValue = ddlCorrectDateFYQ.SelectedValue

            RRWeek()

            ddlRRWeekSelect.SelectedValue = ddlCorrectDateWeek.SelectedValue

            FindValues()

            ModalPopupWrongDate.Hide()
            ModalPopupExtenderRR.Show()

        Catch ex As Exception

            ExplanationLabelRR.Text = "Please report error to Admin"
            ModalPopupWrongDate.Hide()
            ModalPopupExtenderRR.Show()

            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnCorrectDateCancel_Click(sender As Object, e As EventArgs) Handles btnCorrectDateCancel.Click
        ModalPopupWrongDate.Hide()
    End Sub

    Private Sub lbchangeDate2_Click(sender As Object, e As EventArgs) Handles lbchangeDate2.Click

        ChangeDate()

    End Sub

    Private Sub ddlCorrectDateFYQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCorrectDateFYQ.SelectedIndexChanged
        RRCorrectWeek()
        ModalPopupWrongDate.Show()
    End Sub

    Private Sub PopulateBonfireGrid()

        Try

            Dim Perc As Integer = 0

            Dim FindSCL As String = "select m.Name, d.dID, cast(d.Numerator as int) Numerator, cast(d.Denominator as int) Denominator, d.dDate, l.LocID, l.Location " & _
            ", case when dDate between dateadd(month, -1, DATEADD(month, DATEDIFF(month, 0,  getdate() ), 0)) and getdate() then 1 " & _
            " when m.MID = 30 and getdate() < '3/18/2017' then 1 else 0 end as EnableBox " & _
            "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
            "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Data d on d.LocID = L.LocID and d.MID = m.MID and d.Active = 1 " & _
            "	and d.dDate between dateadd(month, -1, DATEADD(year, DATEDIFF(year, 0,  getdate() ), 0)) and dateadd(month, 11, DATEADD(year, DATEDIFF(year, 0,  getdate() ), 0)) " & _
            "	and d.dDate between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
            "	and d.dDate between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
            "left join DWH.KPIS.DEV_OHMS_Target t on t.LocID = L.LocID and t.MID = m.MID and t.Active = 1 " & _
            "	and d.dDate between isnull(t.TargetEffFromDate, '1/1/1800') and isnull(t.TargetEffToDate, '12/31/9999') " & _
            "where d.dDate between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
            "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
            "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
            "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
            "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
            "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
            "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
            "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
            " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
            "			and m.Active = 1 " & _
            "           and (m.MID between 30 and 38 )" & _
            "           and (l.LocID = " & ddlBonfireLocation.SelectedValue.ToString & " )" & _
            " order by dDate"


            gvBonfire.DataSource = GetData(FindSCL).DefaultView
            gvBonfire.DataBind()


            For Each row As GridViewRow In gvBonfire.Rows
                If row.RowType = DataControlRowType.DataRow Then

                    If row.Cells(4).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString = 1 Then
                        row.Cells(2).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = True
                        row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = True
                    End If

                End If
            Next


        Catch ex As Exception
            lblBonfireExceptions.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            lblBonfireExceptions.DataBind()
            mpeBonfirePopups.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub ddlBonfireLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBonfireLocation.SelectedIndexChanged
        PopulateBonfireGrid()
    End Sub

    Private Sub SubmitBonfires()

        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim UpdatesSql As String = ""

            If ddlBonfireLocation.SelectedValue = -1 Then
                lblBonfireExceptions.Text = "No Bonfires to Submit; Please select a Valid Location"
                mpeBonfirePopups.Show()
                Exit Sub
            End If

            For i As Integer = 0 To gvBonfire.Rows.Count - 1
                Dim BonfireID As Label = CType(gvBonfire.Rows(i).FindControl("lblBonfireID"), Label)
                Dim BonfireNum As TextBox = CType(gvBonfire.Rows(i).FindControl("txtBonfireNumerator"), TextBox)
                Dim BonfireDenom As TextBox = CType(gvBonfire.Rows(i).FindControl("txtBonfireDenominator"), TextBox)

                Dim OldNum As Label = CType(gvBonfire.Rows(i).FindControl("lblBonfireNumerator"), Label)
                Dim OldDenom As Label = CType(gvBonfire.Rows(i).FindControl("lblBonfireDenominator"), Label)


                Dim NewRow As Integer = 0
                If BonfireNum.Text <> OldNum.Text Then
                    NewRow = 1
                ElseIf BonfireDenom.Text <> OldDenom.Text Then
                    NewRow = 1
                End If

                If NewRow > 0 Then
                    UpdatesSql += "Update DWH.KPIS.DEV_OHMS_Data set Numerator = '" & Replace(BonfireNum.Text.ToString, "'", "''") & "', Denominator = '" & Replace(BonfireDenom.Text.ToString, "'", "''") & _
                        "', ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                        "' where dID = " & Replace(BonfireID.Text, "'", "''") & "; "
                End If

            Next


            If Len(UpdatesSql) = 0 Then
                lblBonfireExceptions.Text = "No Modifications to Bonfires"
                ModalPopupExtenderRR.Show()
                Exit Sub
            End If

            ExecuteSql(UpdatesSql)

            Dim y As String = ddlBonfireLocation.SelectedValue

            PopulateBonfireLocations()

            Try
                ddlBonfireLocation.SelectedValue = y
            Catch ex As Exception

            End Try


            lblBonfireExceptions.Text = "Bonfires Successfully Submitted."
            mpeBonfirePopups.Show()

        Catch ex As Exception
            lblBonfireExceptions.Text = "Error Submitting Data; Please report to admin."
            mpeBonfirePopups.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnSubmitBonfire_Click(sender As Object, e As EventArgs) Handles btnSubmitBonfire.Click
        SubmitBonfires()
    End Sub
End Class