Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls.WebControl

Imports FinanceWeb.WebFinGlobal

Public Class Rehab_OHMS_Data_Entry

    Inherits System.Web.UI.Page
    Public Shared WebAdminEmail As String = "chelsea.weirich@northside.com"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then
            Else
                CheckPermissions()
                txtOPApptDate.Text = Today.ToShortDateString
                PopulateddlOPAcctLocations()
                PopulateEntryDDL()
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
    Private Sub PopulateddlOPAcctLocations()

        ddlOPApptLocation.DataSource = GetData("select l.LocID, l.LocationDesc, 2 as Ordering " & _
            "from dwh.kpis.DEV_OHMS_Metrics m  " & _
            "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l on m.MID = m2l.MID and m2l.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Location_LU l on m2l.LocID = l.LocID and l.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Update_Permissions up on isnull(up.MetricID_Limit, m.MID) = m.MID	 " & _
            "	and isnull(up.LocationID_Limit, l.LocID) = l.LocID and up.Active = 1 " & _
            "where up.UserLogIn = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and m.MID = 141 and m.Active = 1 and getdate() between isnull(m.EffFromDate, '1/1/1800') and isnull(m.EffToDate, '12/31/9999')" & _
            "union select -1, '(Select Location)', 1 order by Ordering, LocationDesc")
        ddlOPApptLocation.DataTextField = "LocationDesc"
        ddlOPApptLocation.DataValueField = "LocID"
        ddlOPApptLocation.DataBind()

    End Sub

    Private Sub PopulategvOPAppts()

        Dim RecordDate As Date
        If Date.TryParse(txtOPApptDate.Text, RecordDate) Then
        Else
            lblExplanationLabel.Text = "Could not parse '" & txtOPApptDate.Text & "' as date"
            mpeStandard.Show()
            Exit Sub
        End If
        RecordedDate.Text = RecordDate

        gvOPApptResults.DataSource = GetData("select dd.Calendar_Date as RecordDate, l.LocID, s.SpecID, s.SpecialtyDescription " & _
            ", rd.NextAvailableAppt, rd.Comment  " & _
            "from DWH.dbo.DimDate dd " & _
            "join dwh.kpis.DEV_OHMS_Metrics m on m.MID = 141 and m.Active = 1 and dd.Calendar_Date between isnull(m.EffFromDate, '1/1/1800') and isnull(m.EffToDate, '12/31/9999') " & _
            "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l on m.MID = m2l.MID and m2l.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Location_LU l on m2l.LocID = l.LocID and l.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Update_Permissions up on isnull(up.MetricID_Limit, m.MID) = m.MID	 " & _
                "and isnull(up.LocationID_Limit, l.LocID) = l.LocID and up.Active = 1 " & _
            "join DWH.KPIS.Rehab_OP_Appt_Specialty_2_Dept s2d on s2d.LocID = l.LocID and s2d.Active = 1 " & _
                "and dd.Calendar_Date between isnull(s2d.EffectiveFrom, '1/1/1800') and isnull(s2d.EffectiveTo, '12/31/9999') " & _
            "join DWH.KPIS.Rehab_OP_Appt_Specialty_LU s on s2d.SpecID = s.SpecID and s.Active = 1 " & _
            "left join DWH.KPIS.Rehab_OP_Appt_Detail rd on rd.RecordDate = dd.Calendar_Date and rd.Active = 1 and rd.SpecID = s.SpecID and rd.LocID = l.LocID " & _
            "where dd.Calendar_Date = '" & RecordDate & "' and up.UserLogIn = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            "' and l.LocID = '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & "' " & _
            "order by SpecialtyDescription ")

        gvOPApptResults.DataBind()

    End Sub

    Private Sub CheckPermissions()
        If GetScalar("select isnull(count(*), 0) from DWH.KPIS.Rehab_OP_Appt_Admins where Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                     "' and AdminType = 'SpecialtyAdmin'") > 0 Then
            tpSpecAdmin.Visible = True
            PopulateddlAdminAcctLocations()
        Else
            tpSpecAdmin.Visible = False
        End If
        If GetScalar("select isnull(count(*), 0) from DWH.KPIS.Rehab_OP_Appt_Admins where Active = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
             "' and AdminType = 'UserAdmin'") > 0 Then
            tpUserAdmin.Visible = True
        Else
            tpUserAdmin.Visible = False
        End If
    End Sub

    Private Sub PopulateddlAdminAcctLocations()

        ddlLocationChoices.DataSource = GetData("select l.LocID, l.LocationDesc, 2 as Ordering " & _
            "from dwh.kpis.DEV_OHMS_Metrics m  " & _
            "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l on m.MID = m2l.MID and m2l.Active = 1 " & _
            "join DWH.KPIS.DEV_OHMS_Location_LU l on m2l.LocID = l.LocID and l.Active = 1 " & _
            "where m.MID = 141 and m.Active = 1 and getdate() between isnull(m.EffFromDate, '1/1/1800') and isnull(m.EffToDate, '12/31/9999') " & _
            "union select -1, '(Select Location)', 1 order by Ordering, LocationDesc")
        ddlLocationChoices.DataTextField = "LocationDesc"
        ddlLocationChoices.DataValueField = "LocID"
        ddlLocationChoices.DataBind()

    End Sub

    Private Sub CheckgvOPSubmitted()
        ' Remember to add way to tell if a row has been submitted or not

    End Sub

    Private Sub ddlOPApptLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOPApptLocation.SelectedIndexChanged
        If ddlOPApptLocation.SelectedIndex < "1" Then
            btnSubmitRecords.Visible = False
            gvOPApptResults.Visible = False
            lblExplanationLabel.Text = "Please select Location"
            mpeStandard.Show()
            Exit Sub
        Else
            gvOPApptResults.Visible = True
            btnSubmitRecords.Visible = True
            PopulategvOPAppts()
        End If
    End Sub

    Private Sub txtOPApptDate_TextChanged(sender As Object, e As EventArgs) Handles txtOPApptDate.TextChanged
        If ddlOPApptLocation.SelectedIndex < "1" Then
            lblExplanationLabel.Text = "Please select Location"
            mpeStandard.Show()
            Exit Sub
        Else
            PopulategvOPAppts()
        End If
    End Sub

    'Private Sub gvOPApptResults_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvOPApptResults.RowCommand
    '    Try
    '        Dim CommandArg As String = e.CommandArgument
    '        Dim Commander As String = e.CommandName

    '        If Commander = "SubmitLine" Then
    '            Dim txtComment As TextBox = Nothing
    '            Dim ResultComment As String
    '            Dim txtNextAppt As TextBox
    '            Dim NextAppt As Date
    '            For Each canoe As GridViewRow In gvOPApptResults.Rows()
    '                If gvOPApptResults.DataKeys()(canoe.RowIndex).Value = CommandArg Then
    '                    txtComment = canoe.FindControl("txtComment")
    '                    txtNextAppt = canoe.FindControl("txtValues")
    '                    If Date.TryParse(txtNextAppt.Text, NextAppt) Then
    '                        If NextAppt < CDate(RecordedDate.Text) Then
    '                            lblExplanationLabel.Text = "Cannot schedule appointments in the past"
    '                            mpeStandard.Show()
    '                            Exit Sub
    '                        End If

    '                    Else
    '                        lblExplanationLabel.Text = "Could not parse '" & txtNextAppt.Text & "' as date"
    '                        mpeStandard.Show()
    '                        Exit Sub
    '                    End If
    '                Else
    '                End If
    '            Next

    '            If txtComment Is Nothing Then
    '                lblExplanationLabel.Text = "Error -- Please contact Admin"
    '                mpeStandard.Show()
    '                Exit Sub
    '            ElseIf Trim(txtComment.Text) = "" Then
    '                ResultComment = "null"
    '            Else
    '                ResultComment = "'" & Replace(Trim(txtComment.Text), "'", "''") & "'"
    '            End If


    '            Dim PrepSQL As String = "update DWH.KPIS.Rehab_OP_Appt_Detail " & _
    '                "set Active = 0, InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', DateInactivated = getdate() " & _
    '                "where Active = 1 and SpecID = '" & Replace(CommandArg, "'", "''") & "' and LocID = '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & _
    '                "' and RecordDate = '" & Replace(RecordedDate.Text, "'", "''") & "' " & _
    '                "and (NextAvailableAppt <> '" & NextAppt & "' or isnull(Comment, '') <> '" & Replace(Trim(txtComment.Text), "'", "''") & "') " & _
    '                "Insert into DWH.KPIS.Rehab_OP_Appt_Detail (SpecID, LocID, RecordDate, NextAvailableAppt, Comment, Active, AddedBy, DateAdded) " & _
    '                "select '" & Replace(CommandArg, "'", "''") & "', '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & "', '" & Replace(RecordedDate.Text, "'", "''") & _
    '                "', '" & NextAppt & "', " & ResultComment & ", 1, '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
    '                " where not exists (select * from DWH.KPIS.Rehab_OP_Appt_Detail where Active = 1 and SpecID = '" & Replace(CommandArg, "'", "''") & _
    '                "' and LocID = '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & _
    '                "' and RecordDate = '" & Replace(RecordedDate.Text, "'", "''") & "') "


    '            ExecuteSql(PrepSQL)
    '            CheckgvOPSubmitted()
    '            lblExplanationLabel.Text = "Submitted"
    '            mpeStandard.Show()

    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub ddlLocationChoices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocationChoices.SelectedIndexChanged
        If ddlLocationChoices.SelectedIndex >= 1 Then
            gvLocationSpecialties.Visible = True
            PopulateGvSpec()
        Else
            gvLocationSpecialties.Visible = False
        End If
    End Sub

    Private Sub PopulateGvSpec()

        gvLocationSpecialties.DataSource = GetData("select s.SpecID, SpecialtyDescription,  case when s2d.Active = 1 then 'Available' else 'Unavailable' end as Status, " & _
            " case when s2d.Active = 1 then 'Remove Specialty' else 'Add Specialty' end as Button " & _
            "from DWH.KPIS.Rehab_OP_Appt_Specialty_LU s " & _
            "left join DWH.KPIS.Rehab_OP_Appt_Specialty_2_Dept s2d on s2d.SpecID = s.SpecID and s2d.Active = 1 and isnull(s2d.LocID, '" & _
            Replace(ddlLocationChoices.SelectedValue, "'", "''") & "') = '" & Replace(ddlLocationChoices.SelectedValue, "'", "''") & "' " & _
            "	and GETDATE() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') " & _
            "where s.Active = 1 order by SpecialtyDescription")
        gvLocationSpecialties.DataBind()

    End Sub

    Private Sub gvLocationSpecialties_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLocationSpecialties.RowCommand
        Try
            Dim CommandArg As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            If Commander = "AddRemoveSpec" Then

                Dim PrepSQL As String = "if (select isnull(count(*), 0) From DWH.KPIS.Rehab_OP_Appt_Specialty_2_Dept s2d " & _
                "where SpecID = '" & Replace(CommandArg, "'", "''") & "' and LocID = '" & Replace(ddlLocationChoices.SelectedValue, "'", "''") & "' and Active = 1) > 0 " & _
                "begin " & _
                "update DWH.KPIS.Rehab_OP_Appt_Specialty_2_Dept set Active = 0, InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                "', DateInactivated = getdate() " & _
                "where SpecID = '" & Replace(CommandArg, "'", "''") & "' and LocID = '" & Replace(ddlLocationChoices.SelectedValue, "'", "''") & "' and Active = 1 " & _
                "end " & _
                "else " & _
                "begin " & _
                "insert into DWH.KPIS.Rehab_OP_Appt_Specialty_2_Dept (SpecID, LocID, Active, AddedBy, DateAdded) " & _
                "values ('" & Replace(CommandArg, "'", "''") & "', '" & Replace(ddlLocationChoices.SelectedValue, "'", "''") & "', 1, '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()) " & _
                "end "

                ExecuteSql(PrepSQL)
                PopulateGvSpec()
                lblExplanationLabel.Text = "Submitted"
                mpeStandard.Show()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnSubmitRecords_Click(sender As Object, e As EventArgs) Handles btnSubmitRecords.Click
        Try
            Dim PrepSQL As String = ""
            For Each canoe As GridViewRow In gvOPApptResults.Rows()
                Dim txtComment As TextBox = Nothing
                Dim ResultComment As String
                Dim txtNextAppt As TextBox
                Dim NextAppt As Date
                Dim CommandArg As String = gvOPApptResults.DataKeys()(canoe.RowIndex).Value
                txtComment = canoe.FindControl("txtComment")
                txtNextAppt = canoe.FindControl("txtValues")
                If Date.TryParse(txtNextAppt.Text, NextAppt) Then
                    If NextAppt < CDate(RecordedDate.Text) Then
                        lblExplanationLabel.Text = "Cannot schedule appointments in the past"
                        mpeStandard.Show()
                        Exit Sub
                    Else
                        If txtComment Is Nothing Then
                            lblExplanationLabel.Text = "Error -- Please contact Admin"
                            mpeStandard.Show()
                            Exit Sub
                        ElseIf Trim(txtComment.Text) = "" Then
                            ResultComment = "null"
                        Else
                            ResultComment = "'" & Replace(Trim(txtComment.Text), "'", "''") & "'"
                        End If
                        PrepSQL += "update DWH.KPIS.Rehab_OP_Appt_Detail " & _
                            "set Active = 0, InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', DateInactivated = getdate() " & _
                            "where Active = 1 and SpecID = '" & Replace(CommandArg, "'", "''") & "' and LocID = '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & _
                            "' and RecordDate = '" & Replace(RecordedDate.Text, "'", "''") & "' " & _
                            "and (NextAvailableAppt <> '" & NextAppt & "' or isnull(Comment, '') <> '" & Replace(Trim(txtComment.Text), "'", "''") & "') " & _
                            "Insert into DWH.KPIS.Rehab_OP_Appt_Detail (SpecID, LocID, RecordDate, NextAvailableAppt, Comment, Active, AddedBy, DateAdded) " & _
                            "select '" & Replace(CommandArg, "'", "''") & "', '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & "', '" & Replace(RecordedDate.Text, "'", "''") & _
                            "', '" & NextAppt & "', " & ResultComment & ", 1, '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            " where not exists (select * from DWH.KPIS.Rehab_OP_Appt_Detail where Active = 1 and SpecID = '" & Replace(CommandArg, "'", "''") & _
                            "' and LocID = '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & _
                            "' and RecordDate = '" & Replace(RecordedDate.Text, "'", "''") & "') "
                    End If
                ElseIf Trim(txtNextAppt.Text) = "" Then
                    PrepSQL += "update DWH.KPIS.Rehab_OP_Appt_Detail " & _
                    "set Active = 0, InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', DateInactivated = getdate() " & _
                    "where Active = 1 and SpecID = '" & Replace(CommandArg, "'", "''") & "' and LocID = '" & Replace(ddlOPApptLocation.SelectedValue, "'", "''") & _
                    "' and RecordDate = '" & Replace(RecordedDate.Text, "'", "''") & "' "


                Else
                    lblExplanationLabel.Text = "Could not parse '" & txtNextAppt.Text & "' as date"
                    mpeStandard.Show()
                    Exit Sub
                End If






            Next



            ExecuteSql(PrepSQL)
            CheckgvOPSubmitted()
            lblExplanationLabel.Text = "Changes Submitted"
            mpeStandard.Show()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnAddSpec_Click(sender As Object, e As EventArgs) Handles btnAddSpec.Click

        If Trim(txtNewSpecialty.Text) = "" Then
            lblExplanationLabel.Text = "Please enter Specialty Name"
            mpeStandard.Show()
            Exit Sub
        ElseIf GetScalar("select isnull(count(*), 0) from DWH.KPIS.Rehab_OP_Appt_Specialty_LU " & _
            "where SpecialtyDescription = '" & Replace(Trim(txtNewSpecialty.Text), "'", "''") & "' and Active = 1") > 0 Then
            lblExplanationLabel.Text = "A specialty with this description already exists"
            mpeStandard.Show()
            Exit Sub
        Else
            ExecuteSql("insert into DWH.KPIS.Rehab_OP_Appt_Specialty_LU (SpecID, SpecialtyDescription, Active, AddedBy, DateAdded) " & _
                "select isnull(max(SpecID), 0) + 1, '" & Replace(Trim(txtNewSpecialty.Text), "'", "''") & "', 1, '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() from DWH.KPIS.Rehab_OP_Appt_Specialty_LU")
            lblExplanationLabel.Text = "Specialty Added"
            PopulateGvSpec()
            mpeStandard.Show()
            Exit Sub
        End If

    End Sub

    Private Sub PopulateEntryDDL()

        ddlSelectMonthlyMetric.DataSource = GetData("select distinct m.MID, Name, 1 as ord from DWH.KPIS.DEV_OHMS_Metrics m   " & _
            "join DWH.KPIS.DEV_OHMS_Title_2_Metric t2m on m.MID = t2m.MID and t2m.Active = 1   " & _
            "join DWH.KPIS.DEV_OHMS_Update_Permissions up on m.MID = isnull(up.MetricID_Limit, m.MID) and up.Active = 1  " & _
            "where m.Active = 1 and t2m.TitleCode = 14  " & _
            "and m.Owner = 'WebTool' and up.UserLogIn = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'  " & _
            "and GETDATE() between isnull(m.EffFromDate, '1/1/1800') and isnull(m.EffToDate, '12/31/9999') " & _
            "union select -1, '(Select Metric)', 0  " & _
            "order by Ord, Name  ")
        ddlSelectMonthlyMetric.DataValueField = "MID"
        ddlSelectMonthlyMetric.DataTextField = "Name"
        ddlSelectMonthlyMetric.DataBind()

        PopulateLocationDDL()

    End Sub

    Private Sub PopulateLocationDDL()

        ddlSelectMonthlyLocation.DataSource = GetData("select distinct l.LocID, l.LocationDesc, 1 as Ord  " & _
        "from DWH.KPIS.DEV_OHMS_Metrics m  " & _
        "join DWH.KPIS.DEV_OHMS_Title_2_Metric t2m on m.MID = t2m.MID and t2m.Active = 1 	  " & _
        "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l on m.MID = m2l.MID and m2l.Active = 1  " & _
            "and getdate() between isnull(m2l.EffFromDate, '1/1/1800') and isnull(m2l.EffToDate, '12/31/9999')  " & _
        "join DWH.KPIS.DEV_OHMS_Location_LU l on m2l.LocID = l.LocID and l.Active = 1   " & _
            "and getdate() between isnull(l.EffFromDate, '1/1/1800') and isnull(l.EffToDate, '12/31/9999')  " & _
        "join DWH.KPIS.DEV_OHMS_Update_Permissions up on m.MID = isnull(up.MetricID_Limit, m.MID) and up.Active = 1  " & _
            "and l.LocID = isnull(up.LocationID_Limit, l.LocID)  " & _
        "where m.Active = 1 and t2m.TitleCode = 14   " & _
            "and GETDATE() between isnull(m.EffFromDate, '1/1/1800') and isnull(m.EffToDate, '12/31/9999')  " & _
            "and m.Owner = 'WebTool' and up.UserLogIn = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'  " & _
            "and m.MID = '" & Replace(ddlSelectMonthlyMetric.SelectedValue.ToString, "'", "''") & "'  " & _
        "union select -1, '(Select Location)', 0  " & _
        "order by Ord, LocationDesc  ")

        ddlSelectMonthlyLocation.DataValueField = "LocID"
        ddlSelectMonthlyLocation.DataTextField = "LocationDesc"
        ddlSelectMonthlyLocation.DataBind()

    End Sub

    Private Sub ddlSelectMonthlyMetric_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectMonthlyMetric.SelectedIndexChanged

        Dim x As Integer = ddlSelectMonthlyLocation.SelectedValue
        PopulateLocationDDL()
        Try
            ddlSelectMonthlyLocation.SelectedValue = x
        Catch ex As Exception
            ddlSelectMonthlyLocation.SelectedValue = -1
        End Try

        PopulateMetricData()

    End Sub

    Private Sub PopulateMetricData()


        Try
            If ddlSelectMonthlyMetric.SelectedIndex > 0 Then

                Dim Perc As Integer = 0

                'updated to deal with Average or RawSplits 7/11/2019 CRW
                'updated to deal with zero in the denominator for Percentages (and to fix colors for percent%, not just for %, since I saw it) 7/17/2019

                Dim FindSCL As String = "select dID, Name as [Metric Name], Entity, Location, LocationDesc, convert(float, Numerator) [CurrentNumerator] " & _
                    ", case when CHARINDEX('/', m.Calculations) > 0 then convert(varchar, convert(float, Denominator)) else 'N/A' end [Current Denominator], DataType " & _
                    ", convert(float,case when d.Denominator = 0 then Numerator when m.DataType like 'percent%' then round(d.Numerator/d.Denominator*100, 2) when CHARINDEX('/', m.Calculations) > 0 then round(d.Numerator/d.Denominator, 2) else d.Numerator end) as [Current Value] " & _
                    "		, case when CHARINDEX('/', m.Calculations) > 0 then left(m.Calculations, CHARINDEX('/', m.Calculations) - 1) else m.Calculations end as Numerator " & _
                    ", case when CHARINDEX('/', m.Calculations) > 0 then substring(m.Calculations, CHARINDEX('/', m.Calculations) + 1, LEN(Calculations)) else '' end as Denominator " & _
                    ", case when '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' in ('bw996848', 'cw996788') then 1 " & _
                    "       when d.Numerator is null then 1 when dDate > dateadd(month, -2, getdate()) then 1 else 0 end as AllowUpdate " & _
                    ", isnull(m.Objective, '') as Objective, isnull(m.Definition, '') as Definition " & _
                    ", t.Target " & _
                    ", case when case when d.Denominator = 0 then Numerator when m.DataType like 'percent%' then d.Numerator/d.Denominator*100 else d.Numerator end is null then 'purple' " & _
                    "       when case when d.Denominator = 0 then Numerator when m.DataType like 'percent%' then d.Numerator/d.Denominator*100 else d.Numerator end > t.RedMax then 'red' " & _
                    "	    when case when d.Denominator = 0 then Numerator when m.DataType like 'percent%' then d.Numerator/d.Denominator*100 else d.Numerator end < t.RedMin then 'red' " & _
                    "	    when case when d.Denominator = 0 then Numerator when m.DataType like 'percent%' then d.Numerator/d.Denominator*100 else d.Numerator end < t.wMin then 'yellow' " & _
                    "	    when case when d.Denominator = 0 then Numerator when m.DataType like 'percent%' then d.Numerator/d.Denominator*100 else d.Numerator end > t.wMax then 'yellow' " & _
                    "	    else 'green' end as color, d.dDate,  convert(varchar, dDate, 107) as DisplayDate " & _
                    " ,t.RedMax, t.RedMin, t.wMin, t.wMax, m.DataType,  case when CHARINDEX('/', m.Calculations) > 0 then 1 else 0 end EnableDenominator " & _
                "from DWH.KPIS.DEV_OHMS_Metrics m with (nolock) " & _
                "left join DWH.KPIS.DEV_OHMS_Category_LU c with (nolock) on m.Category = c.CatID and c.Active = 1 " & _
                "join DWH.KPIS.DEV_OHMS_Metric_2_Location m2l with (nolock) on m2l.MID = m.MID and m2l.Active = 1 " & _
                "join DWH.KPIS.DEV_OHMS_Location_LU L with (nolock) on m2l.LocID = L.LocID and L.Active = 1 " & _
                "join DWH.KPIS.DEV_OHMS_Data d on d.LocID = L.LocID and d.MID = m.MID and d.Active = 1 " & _
                "left join DWH.KPIS.DEV_OHMS_Target t on t.LocID = L.LocID and t.MID = m.MID and t.Active = 1 " & _
                "	and d.dDate between isnull(t.TargetEffFromDate, '1/1/1800') and isnull(t.TargetEffToDate, '12/31/9999') " & _
                "where d.dDate between ISNULL(m.EffFromDate, '1/1/1800') and ISNULL(m.EffToDate, '12/31/9999')  " & _
                "	and d.dDate between ISNULL(m2l.EffFromDate, '1/1/1800') and ISNULL(m2l.EffToDate, '12/31/9999')  " & _
                "	and d.dDate between ISNULL(L.EffFromDate, '1/1/1800') and ISNULL(L.EffToDate, '12/31/9999')  " & _
                "	and (L.LocID in (select distinct per.LocationID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where ( per.MetricID_Limit = m.MID or per.MetricID_Limit is null)" & _
                "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
                "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
                "            where per.LocationID_Limit is null and (per.MetricID_Limit = m.MID or per.MetricID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0 ) " & _
                "  and (m.MID in (select distinct per.MetricID_Limit from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) where (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null)  " & _
                "			and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) or  " & _
                "			(select COUNT(*) from DWH.KPIS.DEV_OHMS_Update_Permissions per with (nolock) " & _
                " where per.MetricID_Limit is null and (per.LocationID_Limit = L.LocID or per.LocationID_Limit is null) and per.UserLogIn = '" + Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") + "' and per.Active = 1) > 0) " & _
                "			and m.Active = 1 " & _
                "           and (m.MID = '" & Replace(ddlSelectMonthlyMetric.SelectedValue.ToString, "'", "''") & "')" & _
                "           and (l.LocID = '" & Replace(ddlSelectMonthlyLocation.SelectedValue.ToString, "'", "''") & "' or -1 = '" & Replace(ddlSelectMonthlyLocation.SelectedValue.ToString, "'", "''") & "')" & _
                " order by Name, dDate desc, Location, LocationDesc, Entity"

                Dim UpdateView As New DataView
                UpdateView = GetData(FindSCL).DefaultView
                If UpdateView(0)(10) = "" Then
                    HideDenom.Text = "true"
                Else
                    HideDenom.Text = "False"
                End If
                gvSubmitData.DataSource = UpdateView

                gvSubmitData.Columns(4).HeaderText = UpdateView(0)(9)
                gvSubmitData.Columns(5).HeaderText = UpdateView(0)(10)
                lblMetricObjective.Text = UpdateView(0)(12)
                lblMetricDefinition.Text = UpdateView(0)(13)

                If lblMetricDefinition.Text = "" Then
                    trDef.Visible = False
                Else
                    trDef.Visible = True
                End If

                If lblMetricObjective.Text = "" Then
                    trObj.Visible = False
                Else
                    trObj.Visible = True
                End If

                gvSubmitData.Visible = True
                gvSubmitData.DataBind()

                For Each rowEnabled As GridViewRow In gvSubmitData.Rows
                    If rowEnabled.Cells(7).Text = "0" Then
                        rowEnabled.Cells(4).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = False
                        rowEnabled.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Enabled = False
                    End If
                Next

            Else
                gvSubmitData.Visible = False
            End If

        Catch ex As Exception
            lblExplanationLabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            lblExplanationLabel.DataBind()
            mpeStandard.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvSubmitData_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSubmitData.RowCreated
        Try
            If HideDenom.Text = "true" Then
                e.Row.Cells(5).CssClass = "hidden"
                e.Row.Cells(6).CssClass = "hidden"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlSelectMonthlyLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectMonthlyLocation.SelectedIndexChanged
        PopulateMetricData()
    End Sub

    Private Sub Summations()
        For Each rowEnabled As GridViewRow In gvSubmitData.Rows
            Dim x As TextBox = rowEnabled.Cells(4).Controls.OfType(Of TextBox)().FirstOrDefault()
            Dim y As TextBox = rowEnabled.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault()
            If x.Enabled = True And y.Enabled = True Then
                Try
                    If CInt(Trim(y.Text)) = 0 Then
                        rowEnabled.Cells(6).Controls.OfType(Of Label)().FirstOrDefault().Text = "N/A"
                    ElseIf Trim(y.Text) = "" Then
                        rowEnabled.Cells(6).Controls.OfType(Of Label)().FirstOrDefault().Text = ""
                    Else
                        rowEnabled.Cells(6).Controls.OfType(Of Label)().FirstOrDefault().Text = x.Text / y.Text
                    End If

                Catch ex As Exception

                End Try

            End If
        Next
    End Sub

    Private Sub btnMonthlyDataUpdateRows_Click(sender As Object, e As EventArgs) Handles btnMonthlyDataUpdateRows.Click

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

                    dID = gvSubmitData.DataKeys(row.RowIndex).Value
                    OldNum = row.Cells(4).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    OldDenom = row.Cells(5).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    MetName = row.Cells(0).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    MetLoc = row.Cells(2).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    Numerator = row.Cells(4).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    Denominator = row.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString

                    If Denominator = "0" And Numerator <> "0" Then
                        lblExplanationLabel.Text = "Denominators may not be zero. (Check " & MetName & ", Location " & MetLoc & ")"
                        lblExplanationLabel.DataBind()
                        mpeStandard.Show()
                        Exit Sub
                    End If

                    If Numerator <> OldNum Or Denominator <> OldDenom Then
                        If IsNumeric(Numerator) = False And Numerator <> "" Then
                            lblExplanationLabel.Text = "All submitted numerators must be numeric. (Check " & MetName & ", Location " & MetLoc & ")"
                            lblExplanationLabel.DataBind()
                            mpeStandard.Show()
                            Exit Sub
                        ElseIf row.Cells(5).CssClass <> "hidden" And IsNumeric(Denominator) = False And Denominator <> "" Then
                            lblExplanationLabel.Text = "All submitted Denominators must be numeric. (Check " & MetName & ", Location " & MetLoc & ")"
                            lblExplanationLabel.DataBind()
                            mpeStandard.Show()
                            Exit Sub
                        End If
                        If Numerator = "" Then
                            Numerator = "NULL"
                        Else
                            Numerator = "'" & Numerator & "'"
                        End If
                        If Denominator = "" Then
                            Denominator = "NULL"
                        ElseIf Denominator = "N/A" Then
                            Denominator = "NULL"
                        Else
                            Denominator = "'" & Denominator & "'"
                        End If
                        UpdateSQL += "Update  DWH.KPIS.DEV_OHMS_Data SET Active = 0 where DID = '" & dID & "' and (Numerator is not null or Denominator is not null) and Active = 1 " & _
                            "Insert into DWH.KPIS.dev_OHMS_Data select dDate, MID, LocID, " & Numerator & ", " & Denominator & ", 1, getdate(), '" & _
                            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' from DWH.KPIS.dev_OHMS_Data d where dID = '" & dID & "' and Active = 0 " & _
                            " and not exists (select * from DWH.KPIS.dev_OHMS_Data d2 where d.MID = d2.MID and d.LocID = d2.LocID and d.ddate = d2.ddate and d2.Active = 1) " & _
                            "UPDATE DWH.KPIS.DEV_OHMS_Data SET Numerator = " & Numerator & ", Denominator = " & Denominator & ", ModifyDate = getdate(), ModifyUser = '" & _
                            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' WHERE Active = 1 and dID = " & dID & " and Numerator is null and Denominator is null; "
                        cnt += 1
                    End If



                End If
            Next

            If cnt > 0 Then
                ExecuteSql(UpdateSQL)
            End If


            lblExplanationLabel.Text = "Successfully Updated (" & CStr(cnt) & " rows)"
            lblExplanationLabel.DataBind()
            mpeStandard.Show()


        Catch ex As Exception
            lblExplanationLabel.Text = "Error submitting data.  Please re-check values or contact Admin"
            lblExplanationLabel.DataBind()
            mpeStandard.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class