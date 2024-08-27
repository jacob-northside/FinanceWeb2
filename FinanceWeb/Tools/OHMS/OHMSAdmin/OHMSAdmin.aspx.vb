Imports System.Data.SqlClient
Imports System.Data

Imports FinanceWeb.WebFinGlobal

Public Class OHMSAdmin
    Inherits System.Web.UI.Page
    Public Shared ReferenceData As New DataSet
    Public Shared ReferenceView As New DataView
    Public Shared Admin As Integer
    Public Shared rw As DataRow
    Public Shared sortex As String
    Public Shared sortdir As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Exit Sub
        Else

            If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "cw996788" Then
                Admin = 2
            ElseIf User.Identity.IsAuthenticated And User.IsInRole("OHMS") Then
                Admin = 2
            Else
                Admin = 0
            End If

            Try
                Dim ds As DataSet
                Dim da As New SqlDataAdapter
                Dim Sql As String
                Dim cmd As SqlCommand

                Sql = "select SCMName, sm.SCMID from DWH.KPIS.ScorecardMetric sm " & _
                "left join DWH.KPIS.Scorecard sc on sm.SCMID = sc.SCMID " & _
                "left join DWH.KPIS.ScorecardTitle_LU su on sc.SCTID = su.SCTID and su.SCTActive = 1 " & _
                "left join DWH.KPIS.ScorecardUser_LU sul on sul.SCTID = su.SCTID and sul.SCUActive = 1 " & _
                "where sm.SCMActive = 1 and (SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                "' or " & Admin.ToString & " = 1 or (" & Admin.ToString & " = 2 and sul.SCUName = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')) " & _
                "and SCMName in ( " & _
                "select SCMName from DWH.KPIS.ScorecardMetric sm " & _
                "left join DWH.KPIS.Scorecard sc on sm.SCMID = sc.SCMID " & _
                "left join DWH.KPIS.ScorecardTitle_LU su on sc.SCTID = su.SCTID and su.SCTActive = 1 " & _
                "left join DWH.KPIS.ScorecardUser_LU sul on sul.SCTID = su.SCTID and sul.SCUActive = 1 " & _
                "where SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                "' or " & Admin.ToString & " = 1 or (" & Admin.ToString & " = 2 and sul.SCUName = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') " & _
                "group by SCMName " & _
                "having COUNT(distinct sm.SCMID) = 1 " & _
                ") " & _
                "group by SCMName, sm.SCMID" & _
                " " & _
                "union " & _
                " " & _
                "select SCMName + ' (Metric ID ' + Convert(varchar, sm.SCMID) + ')', sm.SCMID  from " & _
                "DWH.KPIS.ScorecardMetric sm " & _
                "left join DWH.KPIS.Scorecard sc on sm.SCMID = sc.SCMID " & _
                "left join DWH.KPIS.ScorecardTitle_LU su on sc.SCTID = su.SCTID and su.SCTActive = 1 " & _
                "left join DWH.KPIS.ScorecardUser_LU sul on sul.SCTID = su.SCTID and sul.SCUActive = 1 " & _
                "where sm.SCMActive = 1 and SCMName in " & _
                "(select SCMName from DWH.KPIS.ScorecardMetric sm2 " & _
                "left join DWH.KPIS.Scorecard sc2 on sm2.ID = sc2.SCID " & _
                "left join DWH.KPIS.ScorecardTitle_LU su2 on sc2.SCTID = su2.SCTID and su.SCTActive = 1 " & _
                "left join DWH.KPIS.ScorecardUser_LU sul on sul.SCTID = su.SCTID and sul.SCUActive = 1 " & _
                "where SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                "' or " & Admin.ToString & " = 1 or (" & Admin.ToString & " = 2 and sul.SCUName = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') " & _
                "group by SCMName " & _
                "having COUNT(distinct sm2.SCMID) > 1) " & _
                "and (SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                "' or " & Admin.ToString & " = 1 or (" & Admin.ToString & " = 2 and sul.SCUName = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')) " & _
                "group by SCMName + ' (Metric ID ' + Convert(varchar, sm.SCMID) + ')', sm.SCMID "


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    ds = New DataSet
                    cmd = New SqlCommand(Sql, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds, "OData")

                    ddlODSelectedMetric.DataSource = ds
                    ddlODSelectedMetric.DataMember = "OData"
                    ddlODSelectedMetric.DataTextField = "SCMName"
                    ddlODSelectedMetric.DataValueField = "SCMID"
                    ddlODSelectedMetric.DataBind()
                End Using

                filldates()
                ODFillGridView()

            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If

    End Sub
    Private Sub filldates()

        Try

            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Sql As String
            Dim cmd As SqlCommand


            Sql = "select 'Select Date (optional)' as facevalue, 9999 as FY, 12 as FM, '1/1/1800' as starty, '12/31/9999' as ends " & _
                    "union " & _
                      "select Month_Abbreviation + ', '+ ltrim(str(CY)) facevalue , FY, FM, MIN(Calendar_Date) starty, MAX(Calendar_Date) ends " & _
                      "from DWH.KPIS.ScorecardData sd " & _
                      "left join DWH.dbo.DimDate dd on sd.SCDFD = dd.Day_of_Month and sd.SCDFM = dd.FM and sd.SCDFY = dd.FY " & _
                      "left join DWH.KPIS.ScorecardMetric sm on sd.ID = sm.ID " & _
                      "left join DWH.KPIS.Scorecard sc on sm.SCMID = sc.SCMID " & _
                      "left join DWH.KPIS.ScorecardTitle_LU su on sc.SCTID = su.SCTID and su.SCTActive = 1 " & _
                      "left join DWH.KPIS.ScorecardUser_LU sul on sul.SCTID = su.SCTID and sul.SCUActive = 1 " & _
                      "where (SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                      "' or " & Admin.ToString & " = 1 or (" & Admin.ToString & " = 2 and sul.SCUName = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')) " & _
                      "and (sm.SCMID = " & ddlODSelectedMetric.SelectedValue & " or " & ddlODSelectedMetric.SelectedValue & " = 0) " & _
                      "and (sd.SCDActual is null or '" & chbWhereNull.Checked.ToString & "' = 'false' )" & _
                      "and SCDActive = 1 " & _
                      "group by Month_Abbreviation + ', '+ ltrim(str(CY)) , FY, FM " & _
                      "order by FY desc, FM desc "




            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet
                cmd = New SqlCommand(Sql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

                ddlODStartDate.DataSource = ds
                ddlODStartDate.DataMember = "OData"
                ddlODStartDate.DataTextField = "facevalue"
                ddlODStartDate.DataValueField = "starty"
                ddlODStartDate.DataBind()

                ddlODEndDate.DataSource = ds
                ddlODEndDate.DataMember = "OData"
                ddlODEndDate.DataTextField = "facevalue"
                ddlODEndDate.DataValueField = "ends"
                ddlODEndDate.DataBind()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ODFillGridView()
        Try

            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Sql As String
            Dim cmd As SqlCommand

            If Admin = 1 Then

                Sql = "select sd.ID, SCMName, SCMOwner, case when ed.FirstName is NULL and ed.LastName is NULL then isnull(ci.UserName, SCMOwner) else ed.FirstName + ' ' + ed.LastName end as OwnerName, " & _
                    " SCDID, convert(varchar,Calendar_Date, 101) as [Value Date], SCDActual, SCDDenominator, case when SCMDataType = 'percent' then  SCDActual/SCDDenominator*100 else SCDActual end as AdjustedActual, " & _
                    "SCDActive, SCDUpdated, case when ed2.FirstName is NULL and ed2.LastName is NULL then isnull(ci2.UserName, SCDUpdated) else ed2.FirstName + ' ' + ed2.LastName end as UpdName, " & _
                    "convert(varchar,sd.SCDModifyDate, 101) as [Date Updated], sm.SCMDataType, case when SCDActual is null then 0 else 1 end as orders " & _
                            "from DWH.KPIS.ScorecardData sd " & _
                            "left JOIN DWH.dbo.Email_Distribution ed2 on sd.SCDUpdated = ed2.NetworkLogin " & _
                            "left join DWH.KPIS.ContactInfo ci2 on sd.SCDUpdated = ci2.UserID " & _
                            "left join DWH.dbo.DimDate dd on sd.SCDFD = dd.Day_of_Month and sd.SCDFM = dd.FM and sd.SCDFY = dd.FY " & _
                            "left join DWH.KPIS.ScorecardMetric sm on sd.ID = sm.ID " & _
                            "left JOIN DWH.dbo.Email_Distribution ed on sm.SCMOwner = ed.NetworkLogin " & _
                            "left join DWH.KPIS.ContactInfo ci on sm.SCMOwner = ci.UserID " & _
                            "left join DWH.KPIS.Scorecard sc on sm.SCMID = sc.SCMID " & _
                            "left join DWH.KPIS.ScorecardTitle_LU su on sc.SCTID = su.SCTID " & _
                            "where (SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                            "' or " & Admin.ToString & " = 1 ) " & _
                            "and (sm.SCMID = " & ddlODSelectedMetric.SelectedValue & " or " & ddlODSelectedMetric.SelectedValue & " = 0) " & _
                            "and (sd.SCDActual is null or '" & chbWhereNull.Checked.ToString & "' = 'false' )" & _
                            "and Calendar_Date between '" & ddlODStartDate.SelectedValue.ToString & "' and '" & ddlODEndDate.SelectedValue.ToString & "' " & _
                            "order by case when SCDActual is null then 0 else 1 end, Calendar_Date desc, SCMName "



                ReferenceData.Clear()
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    ds = New DataSet
                    cmd = New SqlCommand(Sql, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ReferenceData, "OData")
                    ReferenceView = ReferenceData.Tables(0).DefaultView

                    gvODData.DataSource = ReferenceView
                    gvODData.DataMember = "OData"
                    gvODData.DataBind()

                End Using
            Else

                ReferenceData.Clear()
                Sql = "select distinct SCDID, sm.SCMDataType, SCDActual as RealActual, SCDDenominator, Calendar_Date, SCMName as [Metric Name], convert(varchar,Calendar_Date, 101) as [Date Recorded], " & _
                        "case when SCMDataType = 'percent' then  convert(varchar, left(SCDActual/SCDDenominator*100, 5)) + '%' else convert(varchar, SCDActual) end as [Recorded Value], " & _
                        "case when FirstName is NULL and LastName is NULL then isnull(UserName, SCMOwner) else FirstName + ' ' + LastName end as [Metric Owner], case when SCDActual is null then 0 else 1 end as orders " & _
                          "from DWH.KPIS.ScorecardData sd " & _
                          "left join DWH.dbo.DimDate dd on sd.SCDFD = dd.Day_of_Month and sd.SCDFM = dd.FM and sd.SCDFY = dd.FY " & _
                          "left join DWH.KPIS.ScorecardMetric sm on sd.ID = sm.ID " & _
                          "left JOIN DWH.dbo.Email_Distribution ed on sm.SCMOwner = ed.NetworkLogin " & _
                          "left join DWH.KPIS.ContactInfo ci on sm.SCMOwner = UserID " & _
                          "left join DWH.KPIS.Scorecard sc on sm.SCMID = sc.SCMID " & _
                          "left join DWH.KPIS.ScorecardTitle_LU su on sc.SCTID = su.SCTID and su.SCTActive = 1 " & _
                          "left join DWH.KPIS.ScorecardUser_LU sul on sul.SCTID = su.SCTID and sul.SCUActive = 1 " & _
                          "where (SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                           "' or " & Admin.ToString & " = 1 or (" & Admin.ToString & " = 2 and sul.SCUName = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')) " & _
                          "and (sm.SCMID = " & ddlODSelectedMetric.SelectedValue & " or " & ddlODSelectedMetric.SelectedValue & " = 0) " & _
                          "and (sd.SCDActual is null or '" & chbWhereNull.Checked.ToString & "' = 'false' )" & _
                          "and SCDActive = 1 " & _
                          "and Calendar_Date between '" & ddlODStartDate.SelectedValue.ToString & "' and '" & ddlODEndDate.SelectedValue.ToString & "' " & _
                          "order by case when SCDActual is null then 0 else 1 end, Calendar_Date desc, SCMName  "

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    ds = New DataSet
                    cmd = New SqlCommand(Sql, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ReferenceData, "OData")
                    ReferenceView = ReferenceData.Tables(0).DefaultView

                    gvODData.DataSource = ReferenceView
                    gvODData.DataMember = "OData"
                    gvODData.DataBind()
                    pnlODScrollbars.Width = "60%"


                End Using
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlODSelectedMetric_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlODSelectedMetric.SelectedIndexChanged
        filldates()
        ODFillGridView()
    End Sub

    Private Sub gvODData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvODData.PageIndexChanging
        Try

            gvODData.PageIndex = e.NewPageIndex
            gvODData.DataSource = ReferenceView
            gvODData.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub





    Private Sub gvODData_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvODData.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If

            If Admin = 1 Then
                e.Row.Cells(14).CssClass = "hidden"
            Else
                e.Row.Cells(1).CssClass = "hidden"
                e.Row.Cells(2).CssClass = "hidden"
                e.Row.Cells(3).CssClass = "hidden"
                e.Row.Cells(4).CssClass = "hidden"
                e.Row.Cells(5).CssClass = "hidden"
                e.Row.Cells(10).CssClass = "hidden"
            End If


            If e.Row.RowIndex Mod 2 = 0 Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2EAD9")
            Else
                e.Row.BackColor = System.Drawing.Color.White
            End If
        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvODData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvODData.SelectedIndexChanged
        Try

            tblOD.Visible = True

            For Each canoe As GridViewRow In gvODData.Rows
                If canoe.RowIndex = gvODData.SelectedIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2EAD9")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

            Dim SCDID As String
            Dim inchworm As Integer = 0
            Dim foundit As Integer

            If Admin = 1 Then
                SCDID = gvODData.SelectedRow.Cells(5).Text
                cellActiveCHBX.Visible = True
                For Each testy As DataRow In ReferenceData.Tables(0).Rows
                    If SCDID = testy(4).ToString Then
                        foundit = inchworm
                    Else
                        inchworm += 1
                    End If
                Next
                rw = ReferenceData.Tables(0).Rows(foundit)

                If Not IsDBNull(rw("SCMDataType")) Then
                    If rw("SCMDataType") = "percent" Then
                        ODRecordedDenominator.Visible = True
                        ODRecordedNumerator.Visible = True
                        txtODRecValue.Visible = False
                        lblODRecValue.Visible = True
                        txtODRecDen.Text = rw("SCDDenominator").ToString
                        txtODRecNum.Text = rw("SCDActual").ToString
                        lblODRecValue.Text = rw("AdjustedActual").ToString
                    Else
                        ODRecordedDenominator.Visible = False
                        ODRecordedNumerator.Visible = False
                        txtODRecValue.Visible = True
                        lblODRecValue.Visible = False
                        txtODRecValue.Text = rw("AdjustedActual").ToString

                    End If
                    chbxODActive.Checked = rw("SCDActive")
                End If
                If Not IsDBNull(rw("SCMOwner")) Then
                    lblODMetricOwner.Text = rw("SCMOwner").ToString
                End If
                If Not IsDBNull(rw("SCMName")) Then
                    lblODMetricName.Text = rw("SCMName").ToString
                End If
                If Not IsDBNull(rw("Value Date")) Then
                    lblODDateRecorded.Text = rw("Value Date").ToString
                End If
            Else
                SCDID = gvODData.SelectedRow.Cells(1).Text
                For Each testy As DataRow In ReferenceData.Tables(0).Rows
                    If SCDID = testy(0).ToString Then
                        foundit = inchworm
                    Else
                        inchworm += 1
                    End If
                Next

                rw = ReferenceData.Tables(0).Rows(foundit)

                If Not IsDBNull(rw("SCMDataType")) Then
                    If rw("SCMDataType") = "percent" Then
                        ODRecordedDenominator.Visible = True
                        ODRecordedNumerator.Visible = True
                        txtODRecValue.Visible = False
                        lblODRecValue.Visible = True
                        txtODRecDen.Text = rw("SCDDenominator").ToString
                        txtODRecNum.Text = rw("RealActual").ToString
                        lblODRecValue.Text = rw("Recorded Value").ToString
                    Else
                        ODRecordedDenominator.Visible = False
                        ODRecordedNumerator.Visible = False
                        txtODRecValue.Visible = True
                        lblODRecValue.Visible = False
                        txtODRecValue.Text = rw("Recorded Value").ToString

                    End If
                    chbxODActive.Checked = True
                End If
                If Not IsDBNull(rw("Metric Owner")) Then
                    lblODMetricOwner.Text = rw("Metric Owner").ToString
                End If
                If Not IsDBNull(rw("Metric Name")) Then
                    lblODMetricName.Text = rw("Metric Name").ToString
                End If
                If Not IsDBNull(rw("Date Recorded")) Then
                    lblODDateRecorded.Text = rw("Date Recorded").ToString
                End If

            End If






            'rw = ReferenceData.Tables(0).Rows(gvODData.SelectedIndex + 30 * (gvODData.PageIndex))



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlODStartDate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlODStartDate.SelectedIndexChanged
        ODFillGridView()
    End Sub

    Private Sub ddlODEndDate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlODEndDate.SelectedIndexChanged
        ODFillGridView()
    End Sub

    Private Sub gvODData_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvODData.Sorting

        Dim dv As DataView
        Dim sorts As String
        dv = ReferenceData.Tables(0).DefaultView

        If e.SortExpression = "Date Recorded" Then
            sorts = "Calendar_Date"
        Else
            sorts = e.SortExpression
        End If

        If e.SortExpression = sortex Then

            If sortdir = 1 Then
                dv.Sort = sorts + " " + "desc"
                sortdir = 0
            Else
                dv.Sort = sorts + " " + "asc"
                sortdir = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            sortdir = 1
            sortex = e.SortExpression
        End If

        gvODData.DataSource = dv
        gvODData.DataBind()

    End Sub

    Private Sub btnODUpdateRecord_Click(sender As Object, e As System.EventArgs) Handles btnODUpdateRecord.Click
        Try
            Dim choice As Int16 = 0
            If Admin = 1 Then
                choice = 1
            ElseIf IsDBNull(rw("Recorded Value")) Then
                choice = 1
            End If

            If choice = 1 Then

                Dim updatesql As String
                If ODRecordedNumerator.Visible = True Then
                    updatesql =
                    "Update DWH.KPIS.ScorecardData set " & _
                    "SCDUpdated = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', " & _
                    "SCDModifyDate = getdate(), " & _
                    "SCDActual = '" & Replace(txtODRecNum.Text, "'", "''") & "', " & _
                    "SCDDenominator = '" & Replace(txtODRecDen.Text, "'", "''") & "', " & _
                    "SCDActive = '" & chbxODActive.Checked.ToString & "' " & _
                    "where SCDID = " & rw("SCDID")
                Else
                    updatesql =
                    "Update DWH.KPIS.ScorecardData set " & _
                    "SCDUpdated = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', " & _
                    "SCDModifyDate = getdate(), " & _
                    "SCDActual = '" & Replace(txtODRecValue.Text, "'", "''") & "', " & _
                    "SCDActive = '" & chbxODActive.Checked.ToString & "' " & _
                    "where SCDID = " & rw("SCDID")
                End If

                Dim cmd As SqlCommand

                Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
                    cmd.ExecuteNonQuery()
                    ODModalExplanationLabel.Text = "Successfully Updated"
                    ODFillGridView()
                    ODFeedback.Visible = False
                    ODModalPopupExtender.Show()
                    ODModalOKButton.Visible = False
                    ODModalCancelButton.Text = "Close"
                End Using

            Else
                ODModalExplanationLabel.Text = "You are updating a record which already has data.  Please leave feedback for why it was changed."
                ODFeedback.Text = ""
                ODFeedback.Visible = True
                ODModalOKButton.Visible = True
                ODModalOKButton.Text = "Continue"
                ODModalPopupExtender.Show()
            End If

        Catch ex As Exception
            ODModalExplanationLabel.Text = "Error updating data -- please report to Admin"
            ODFeedback.Visible = False
            ODModalPopupExtender.Show()
            ODModalOKButton.Visible = False
            ODModalCancelButton.Text = "Close"
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ODModalCancelButton_Click(sender As Object, e As System.EventArgs) Handles ODModalCancelButton.Click
        ODModalPopupExtender.Hide()
    End Sub

    Private Sub ODModalOKButton_Click(sender As Object, e As System.EventArgs) Handles ODModalOKButton.Click
        Try
            Dim updatesql As String
            Dim Insertsql As String
            Dim fullsql As String

            updatesql = "Update DWH.KPIS.ScorecardData set " & _
                "SCDUpdated = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', " & _
                "SCDModifyDate = getdate(), " & _
                "SCDActive = 0 " & _
                "where SCDID = " & rw("SCDID")

            If ODRecordedNumerator.Visible = True Then
                Insertsql =
                "Insert into DWH.KPIS.ScorecardData (SCDFY, SCDFM, SCDFD, ID, SCDActual, SCDActive, SCDModifyDate, SCDUpdated, SCDDenominator) " & _
                "Select SCDFY, SCDFM, SCDFD, ID, '" & Replace(txtODRecNum.Text, "'", "''") & "', 1, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', '" & _
                Replace(txtODRecDen.Text, "'", "''") & "' from DWH.KPIS.ScorecardData " & _
                "where SCDID = " & rw("SCDID")
            Else
                Insertsql =
                "Insert into DWH.KPIS.ScorecardData (SCDFY, SCDFM, SCDFD, ID, SCDActual, SCDActive, SCDModifyDate, SCDUpdated) " & _
                "Select SCDFY, SCDFM, SCDFD, ID, '" & Replace(txtODRecValue.Text, "'", "''") & "', 1, getdate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
                " from DWH.KPIS.ScorecardData " & _
                "where SCDID = " & rw("SCDID")
            End If

            Insertsql = Insertsql & " Insert into DWH.KPIS.ScorecardFeedback (SCDID, SCFBDate, SCFeedback, SCFBOwner, SCFBActive) Values (" & _
            "(select MAX(SCDID) from DWH.KPIS.ScorecardData), getdate(), 'Previous value: " & rw("Recorded Value") & " (" & rw("SCDID") & ") -- " & Replace(ODFeedback.Text, "'", "''") & _
            "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"

            fullsql = updatesql & " " & Insertsql

            Dim cmd As SqlCommand

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(fullsql, conn)
                cmd.ExecuteNonQuery()
                ODModalExplanationLabel2.Text = "Successfully Updated"
                ODFillGridView()
                ODModalPopupExtender2.Show()
            End Using

        Catch ex As Exception
            ODModalExplanationLabel2.Text = "Error updating data -- please report to Admin"
            ODModalPopupExtender2.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ODModal2CancelButton_Click(sender As Object, e As System.EventArgs) Handles ODModal2CancelButton.Click
        ODModalPopupExtender2.Hide()
        ODModalPopupExtender.Hide()
    End Sub

    Private Sub chbWhereNull_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbWhereNull.CheckedChanged
        ODFillGridView()
    End Sub
End Class