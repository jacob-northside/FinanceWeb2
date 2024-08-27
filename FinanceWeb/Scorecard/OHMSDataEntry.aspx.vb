Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Math

Imports FinanceWeb.WebFinGlobal


Public Class OHMSDataEntry
    Inherits System.Web.UI.Page
    Private Shared RunDate As Date
    Public Shared AdminView As New DataView
    Public Shared Adminmap As String
    Public Shared Admindir As Integer
    Public Shared WebAdminEmail As String = "chelsea.weirich@northside.com"
    Public Shared RowNo As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Try
                Dim wcICausedPostBack As WebControl = CType(GetControlThatCausedPostBack(TryCast(sender, Page)), WebControl)

                Dim indx As Integer = wcICausedPostBack.TabIndex

                Dim ctrl = _
                From control In wcICausedPostBack.Parent.Controls.OfType(Of WebControl)() _
                Where control.TabIndex > indx _
                           Select control
                ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus()
            Catch

            End Try


        Else
            Try


                txtEntryDateStart.Text = DateSerial(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2)
                txtEntryDateEnd.Text = DateSerial(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1)
                'refreshDateDDL()
                refreshFirstDDL()

                PopulateLocations()

                FindValues()
            Catch ex As Exception
                explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
                explanationlabel.DataBind()
                ModalPopupExtender.Show()
                LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If

    End Sub

    'Private Sub refreshDateDDL()

    '    Try
    '        Dim cmd As SqlCommand
    '        Dim da As New SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim loc As String
    '        Dim met As String

    '        If ddlLocationSelect.SelectedValue.ToString = "" Then
    '            loc = "-1"
    '        Else
    '            loc = ddlLocationSelect.SelectedValue.ToString
    '        End If
    '        If ddlMetricSelect.SelectedValue.ToString = "" Then
    '            met = "= -1"
    '        ElseIf ddlMetricSelect.SelectedValue.ToString = "28" Then
    '            met = "in (28, 29)"
    '        Else
    '            met = "= " & ddlMetricSelect.SelectedValue.ToString
    '        End If
    '        Dim adminlist As String = "select distinct dDate, convert(varchar, dDate, 107) as DisplayDate, 0 as ord " & _
    '        "from DWH.KPIS.DEV_OHMS_Data d " & _
    '        "where d.Active = 1 and dDate < getdate() and " & _
    '"	(d.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = d.MID or per.MetricID_Limit is null)" & _
    '"			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    '"			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    '" where per.LocationID_Limit is null and (per.MetricID_Limit = d.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    '"  and (d.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = d.LocID or per.LocationID_Limit is null)  " & _
    '"			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    '"			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    '" where per.MetricID_Limit is null and (per.LocationID_Limit = d.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    '"           and (d.LocID = " & loc & " or -1 = " & loc & ")" & _
    '"           and (d.MID " & met & " or -1 " & met & ")" & _
    '" union " & _
    '"select '12/31/9999', 'Select Date', 1 " & _
    '"order by ord desc, dDate desc "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New SqlCommand(adminlist, conn)
    '            da.SelectCommand = cmd
    '            da.SelectCommand.CommandTimeout = 86400
    '            da.Fill(ds, "OData")

    '        End Using


    '        ddlEntryDateSelect.DataSource = ds
    '        ddlEntryDateSelect.DataValueField = "dDate"
    '        ddlEntryDateSelect.DataTextField = "DisplayDate"
    '        ddlEntryDateSelect.DataBind()
    '    Catch ex As Exception
    '        explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
    '        explanationlabel.DataBind()
    '        ModalPopupExtender.Show()
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub refreshFirstDDL()

        Try
            Dim SelectedDate As Date = txtEntryDateStart.Text
            Dim EndDate As Date = txtEntryDateEnd.Text
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim loc As String
            If ddlLocationSelect.SelectedValue.ToString = "" Then
                loc = "-1"
            Else
                loc = ddlLocationSelect.SelectedValue.ToString
            End If
            Dim adminlist As String = "select distinct case when m.MID in (28, 29) then 28 when m.MID in (195, 196) then 195 else m.MID end MID " & _
    ", case when m.MID in (28, 29) then 'Mammo Backlog' when m.MID in (195, 196) then 'Mammo Backlog (Gwinnett)' else m.Name end Name, 0 ord " & _
    "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
    "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
       "join DWH.KPIS.DEV_OHMS_Data d on d.MID = m.MID and d.Active = 1 and d.dDate between '" & SelectedDate & "' and '" & EndDate & "' " & _
    "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 and d.LocID = m2l.LocID  " & _
    "	and d.dDate between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
    "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
    "	and d.ddate between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
    "where d.ddate between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
    "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "         	and m.Active = 1 " & _
    "           and (L.LocID = " & loc & " or -1 = " & loc & ")" & _
    "           and Name not like '%Repeat Analysis%' and d.dDate < getdate()    " & _
    " union " & _
    "select -1, 'Select Metric', 1 " & _
    "order by ord desc, Name "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(adminlist, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using


            ddlMetricSelect.DataSource = ds
            ddlMetricSelect.DataValueField = "MID"
            ddlMetricSelect.DataTextField = "Name"
            ddlMetricSelect.DataBind()
        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlMetricSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMetricSelect.SelectedIndexChanged
        Try
            Dim x As Integer = ddlLocationSelect.SelectedValue
            'Dim z As String = ddlEntryDateSelect.SelectedValue

            'refreshDateDDL()
            PopulateLocations()

            'Try
            '    ddlEntryDateSelect.SelectedValue = z
            'Catch ex As Exception
            '    ddlEntryDateSelect.SelectedIndex = 0
            'End Try



            Try
                ddlLocationSelect.SelectedValue = x
            Catch ex As Exception
                ddlLocationSelect.SelectedValue = -1
            End Try

            FindValues()

        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PopulateLocations()

        Try
            Dim SelectedDate As Date = txtEntryDateStart.Text
            Dim EndDate As Date = txtEntryDateEnd.Text
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet


            Dim met As String

            If ddlMetricSelect.SelectedValue.ToString = "" Then
                met = "= -1"
            ElseIf ddlMetricSelect.SelectedValue.ToString = "28" Then
                met = "in (28, 29)"
            ElseIf ddlMetricSelect.SelectedValue.ToString = "195" Then
                met = "in (195, 196)"
            Else
                met = "= " & ddlMetricSelect.SelectedValue.ToString
            End If

            Dim locsql As String = "select distinct L.LocID, " & _
    "	isnull(isnull(CONVERT(varchar, L.Location) + ' - ', '') + L.LocationDesc + " & _
    "		case when L.LocID in (85, 86, 87) then '' when L.Entity = 10 then ' (Atlanta)' " & _
    "		when L.Entity = 22 then ' (Cherokee)' " & _
    "		when L.Entity = 6 then ' (Forsyth)' " & _
    "		when L.Entity = 30 then ' (Gwinnett)' " & _
    "		when L.Entity = 40 then ' (Duluth)' " & _
    "		else ' (' + CONVERT(varchar, L.Entity) + ')' end, LocationDesc) as LName, isnull(Location, 999) as Location, isnull(Location, LocationDesc) as ord " & _
    "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
    "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
      "join DWH.KPIS.DEV_OHMS_Data d on  d.MID = m.MID and d.Active = 1 " & _
            "	and (d.dDate between '" & SelectedDate & "' and '" & EndDate & "')" & _
    "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 and d.LocID = m2l.LocID " & _
    "	and d.ddate between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
    "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
    "	and d.ddate between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
    "where d.ddate between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
    "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
    "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
    "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
    "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
    " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
    "			and m.Active = 1 " & _
    "           and (m.MID " & met & " or -1 " & met & ")" & _
    " union " & _
    "select -1, 'Select Location' as LName, '1', '-1' " & _
    "order by ord asc "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(locsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using


            ddlLocationSelect.DataSource = ds
            ddlLocationSelect.DataValueField = "LocID"
            ddlLocationSelect.DataTextField = "LName"
            ddlLocationSelect.DataBind()
        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub FindValues()

        Try
            Dim SelectedDate As Date = txtEntryDateStart.Text
            Dim EndDate As Date = txtEntryDateEnd.Text
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Dim SCMID As String = "0"
            Dim Perc As Integer = 0

            If ddlLocationSelect.SelectedValue = "-1" Then
                If ddlMetricSelect.SelectedValue = "" Then
                    SCMID = "0"
                Else
                    SCMID = ddlMetricSelect.SelectedValue
                End If

            ElseIf ddlLocationSelect.SelectedValue = "" Then
                If ddlMetricSelect.SelectedValue = "" Then
                    SCMID = "0"
                Else
                    SCMID = ddlMetricSelect.SelectedValue
                End If
            Else
                SCMID = ddlLocationSelect.SelectedValue
            End If

            Dim met As String

            If ddlMetricSelect.SelectedValue.ToString = "" Then
                met = "= -1"
            ElseIf ddlMetricSelect.SelectedValue.ToString = "28" Then
                met = "in (28, 29)"
            ElseIf ddlMetricSelect.SelectedValue.ToString = "195" Then
                met = "in (195, 196)"
            Else
                met = "= " & ddlMetricSelect.SelectedValue.ToString
            End If

            Dim FindSCL As String = "select top 1000 dID, Name as [Metric Name], Entity, Location, LocationDesc, convert(decimal(18,2), Numerator) [CurrentNumerator] " & _
                ", case when DataType <> 'percent' then 'N/A' else convert(varchar, convert(decimal(18,2), Denominator)) end [Current Denominator], DataType " & _
                ", convert(decimal(18,2),case when m.DataType = 'percent' and d.Denominator = 0 then d.Numerator when m.DataType = 'percent' then round(d.Numerator/isnull(d.Denominator, 1)*100, 2) else d.Numerator end) as [Current Value] " & _
                ", t.Target " & _
                ", case when case when m.DataType = 'percent' and d.Denominator = 0 then d.Numerator*100 when m.DataType = 'percent' then d.Numerator/isnull(d.Denominator, 1)*100 else d.Numerator end is null then 'purple' " & _
                "       when case when m.DataType = 'percent' and d.Denominator = 0 then d.Numerator*100 when m.DataType = 'percent' then d.Numerator/isnull(d.Denominator, 1)*100 else d.Numerator end > t.RedMax then 'red' " & _
                "	    when case when m.DataType = 'percent' and d.Denominator = 0 then d.Numerator*100 when m.DataType = 'percent' then d.Numerator/isnull(d.Denominator, 1)*100 else d.Numerator end < t.RedMin then 'red' " & _
                "	    when case when m.DataType = 'percent' and d.Denominator = 0 then d.Numerator*100 when m.DataType = 'percent' then d.Numerator/isnull(d.Denominator, 1)*100 else d.Numerator end < t.wMin then 'yellow' " & _
                "	    when case when m.DataType = 'percent' and d.Denominator = 0 then d.Numerator*100 when m.DataType = 'percent' then d.Numerator/isnull(d.Denominator, 1)*100 else d.Numerator end > t.wMax then 'yellow' " & _
                "	    else 'green' end as color, d.dDate,  convert(varchar, dDate, 107) as DisplayDate " & _
                " ,t.RedMax, t.RedMin, t.wMin, t.wMax, m.DataType " & _
            "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
            "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
             "join DWH.KPIS.DEV_OHMS_Data d on d.MID = m.MID and d.Active = 1 " & _
            "	and (d.dDate between '" & SelectedDate & "' and '" & EndDate & "') " & _
            "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 and d.LocID = m2l.LocID " & _
            "	and d.ddate between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
            "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
            "	and d.ddate between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
            "left join DWH.KPIS.DEV_OHMS_Target t on t.LocID = L.LocID and t.MID = m.MID and t.Active = 1 " & _
            "	and d.dDate between isnull(t.TargetEffFromDate, '1/1/1800') and isnull(t.TargetEffToDate, '12/31/9999') " & _
            "where d.ddate between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999') and " & _
            "	(L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
            "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
            "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
            " where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
            "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
            "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
            "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
            " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
            "			and m.Active = 1 " & _
            "           and (m.MID " & met & " or -1 " & met & ")" & _
            "           and (l.LocID = " & ddlLocationSelect.SelectedValue.ToString & " or -1 = " & ddlLocationSelect.SelectedValue.ToString & ")" & _
            " order by Name, Location, LocationDesc, Entity, d.dDate desc"



            ds.Clear()
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(FindSCL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            AdminView = ds.Tables(0).DefaultView
            gvSubmitData.DataSource = AdminView
            gvSubmitData.DataBind()

        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlLocationSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationSelect.SelectedIndexChanged

        Try
            Dim y As Integer = ddlMetricSelect.SelectedValue
            'Dim z As String = ddlEntryDateSelect.SelectedValue

            'refreshDateDDL()
            refreshFirstDDL()

            'Try
            '    ddlEntryDateSelect.SelectedValue = z
            'Catch ex As Exception
            '    ddlEntryDateSelect.SelectedIndex = 0
            'End Try

            Try
                ddlMetricSelect.SelectedValue = y
            Catch ex As Exception
                ddlMetricSelect.SelectedValue = -1
            End Try

            FindValues()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        UpdateRows()
    End Sub

    Private Sub UpdateRows()

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
                        explanationlabel.Text = "Denominators may not be zero. (Check " & MetName & ", Location " & MetLoc & ")"
                        explanationlabel.DataBind()
                        ModalPopupExtender.Show()
                        Exit Sub
                    End If

                    If Numerator <> OldNum Or Denominator <> OldDenom Then
                        If IsNumeric(Numerator) = False And Numerator <> "" Then
                            explanationlabel.Text = "All submitted numerators must be numeric. (Check " & MetName & ", Location " & MetLoc & ")"
                            explanationlabel.DataBind()
                            ModalPopupExtender.Show()
                            Exit Sub
                        ElseIf IsNumeric(Denominator) = False And Denominator <> "" And Denominator <> "N/A" Then
                            explanationlabel.Text = "All submitted Denominators must be numeric. (Check " & MetName & ", Location " & MetLoc & ")"
                            explanationlabel.DataBind()
                            ModalPopupExtender.Show()
                            Exit Sub
                        End If
                        If Numerator = "" Then
                            Numerator = "NULL"
                        End If
                        If Denominator = "" Then
                            Denominator = "NULL"
                        ElseIf Denominator = "N/A" Then
                            Denominator = "NULL"
                        End If
                        UpdateSQL += "UPDATE DWH.KPIS.DEV_OHMS_Data SET Numerator = " & Numerator & ", Denominator = " & Denominator & ", ModifyDate = getdate(), ModifyUser = '" & _
                            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' WHERE dID = " & dID & "; "
                        cnt += 1
                    End If



                End If
            Next

            If Len(UpdateSQL) > 0 Then
                UpdateSQL += "update dd1 " & _
                "set dd1.Denominator = dd2.Denominator, dd1.ModifyDate = GETDATE(), dd1.ModifyUser = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                "' from DWH.KPIS.DEV_OHMS_Data dd1 " & _
                "join DWH.KPIS.DEV_OHMS_Data dd2 on dd1.dDate = dd2.dDate and dd1.LocID = dd2.LocID " & _
                "where dd1.MID = 4 And dd2.MID = 12 " & _
                "and dd1.Active = 1 and dd2.Active = 1 " & _
                "and isnull(convert(varchar, dd1.Denominator), 'null') <> ISNULL(convert(varchar, dd2.Denominator), 'null')"
            End If

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(UpdateSQL, conn)
                cmd.ExecuteNonQuery()
                explanationlabel.Text = "Successfully Updated (" & CStr(cnt) & " rows)"
                explanationlabel.DataBind()
                ModalPopupExtender.Show()
            End Using

        Catch ex As Exception
            explanationlabel.Text = "Error submitting data.  Please re-check values or contact Admin"
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton2.Click
        Try
            FindValues()
            ModalPopupExtender.Hide()
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

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

    Private Sub gvSubmitData_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSubmitData.RowCreated

        Try
            'If e.Row.RowState = DataControlRowState.Edit Then


            'End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Dim lbl1 As Label = e.Row.FindControl("Label1")
                'Dim txt1 As TextBox = e.Row.FindControl("txtCurrNum")
                Dim txt2 As TextBox = e.Row.FindControl("txtCurrDenom")

                If e.Row.DataItem("Metric Name") = "POS Collections" Then
                    txt2.Enabled = False
                End If

                'Dim lbl As Label = e.Row.FindControl("lblCurrVal")

                'txt1.Attributes.Add("TextChanged", "javascript:ChangeValue(this, '" + txt2.ClientID + "', '" + lbl.ClientID + "');")
                'txt2.Attributes.Add("TextChanged", "javascript:ChangeValue(this, '" + txt1.ClientID + "', '" + lbl.ClientID + "');")

                e.Row.Cells(9).CssClass = "hidden"
                'Dim pnl As Panel = e.Row.FindControl("pnlColor")

                'If e.Row.DataItem("color") = "red" Then
                '    'pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                '    e.Row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                'ElseIf e.Row.DataItem("color") = "yellow" Then
                '    'pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                '    e.Row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                'ElseIf e.Row.DataItem("color") = "green" Then
                '    'pnl.BackColor = System.Drawing.ColorTranslator.FromHtml("#b7ffb7")
                '    e.Row.Cells(8).BackColor = System.Drawing.ColorTranslator.FromHtml("#b7ffb7")
                'End If




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
                    If l.Text = "POS Collections" Then
                        RelevantNumber = RelevantNumber + 1
                        If Len(RelevantNumber.ToString) = 1 Then
                            ctrlname = Left(ctrlname, Len(ctrlname) - 13) & "0" & RelevantNumber.ToString & Right(ctrlname, 11)
                        Else
                            ctrlname = Left(ctrlname, Len(ctrlname) - 13) & RelevantNumber.ToString & Right(ctrlname, 11)
                        End If
                    Else
                        ctrlname = Replace(ctrlname, "txtCurrNum", "txtCurrDenom")
                    End If

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

    Public Sub ChangeCurrentValue()

        Try


            'Dim RowNo As Integer = 2

            Dim UpdateSQL As String = ""
            Dim cnt As Integer = 0




            Dim row As GridViewRow = gvSubmitData.Rows(RowNo - 2)
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
                            explanationlabel.Text = "Denominators may not be zero."
                            explanationlabel.DataBind()
                            ModalPopupExtender.Show()
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
                        explanationlabel.Text = "Denominators not used for this metric."
                        explanationlabel.DataBind()
                        ModalPopupExtender.Show()
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
                If rowEnabled.Cells(1).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString = "POS Collections" Then
                    rowEnabled.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = False
                End If
            Next

        Catch ex As Exception
            'explanationlabel.Text = "Error submitting data.  Please re-check values or contact Admin"
            'explanationlabel.DataBind()
            'ModalPopupExtender.Show()
            'LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
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
            explantionlabelPageChange.Text = cnt.ToString & " rows of data have been entered; if you change the page, these changes will be lost."
            explantionlabelPageChange.DataBind()
            ModalPopupExtender1.Show()
            Return 1
        Else
            Return 0
        End If

    End Function


    Private Sub CancelButtonPageChange_Click(sender As Object, e As EventArgs) Handles CancelButtonPageChange.Click
        ModalPopupExtender1.Hide()
    End Sub

    Private Sub SubmitButtonPageChange_Click(sender As Object, e As EventArgs) Handles SubmitButtonPageChange.Click
        ModalPopupExtender1.Hide()
        UpdateRows()
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

    'Private Sub ddlEntryDateSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEntryDateSelect.SelectedIndexChanged
    '    Try
    '        Dim x As Integer = ddlLocationSelect.SelectedValue
    '        Dim y As Integer = ddlMetricSelect.SelectedValue

    '        refreshFirstDDL()
    '        PopulateLocations()

    '        Try
    '            ddlMetricSelect.SelectedValue = y
    '        Catch ex As Exception
    '            ddlMetricSelect.SelectedValue = -1
    '        End Try


    '        Try
    '            ddlLocationSelect.SelectedValue = x
    '        Catch ex As Exception
    '            ddlLocationSelect.SelectedValue = -1
    '        End Try

    '        FindValues()

    '    Catch ex As Exception
    '        explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
    '        explanationlabel.DataBind()
    '        ModalPopupExtender.Show()
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Try
            Dim x As Integer = ddlLocationSelect.SelectedValue
            Dim y As Integer = ddlMetricSelect.SelectedValue

            refreshFirstDDL()

            PopulateLocations()

            Try
                ddlMetricSelect.SelectedValue = y
            Catch ex As Exception
                ddlMetricSelect.SelectedValue = -1
            End Try

            Try
                ddlLocationSelect.SelectedValue = x
            Catch ex As Exception
                ddlLocationSelect.SelectedValue = -1
            End Try

            FindValues()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class