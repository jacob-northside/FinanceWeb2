Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Math
Imports System.DirectoryServices
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal

Imports FinanceWeb.WebFinGlobal


Public Class Surgery_TOT_DataEntry
    Inherits System.Web.UI.Page
    Private Shared RunDate As Date
    Public Shared SurgeryTOView As New DataView
    Public Shared SurgeryTOmap As String
    Public Shared SurgeryTOdir As Integer
    Private Shared UserName As String
    Public Shared WebAdminEmail As String = "chelsea.weirich@northside.com"
    Public Shared RowNo As Integer
    Private Shared ClinSupVisible As Integer = 0
    Public Shared Staffdv As New DataView
    Public Shared Codedv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

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

                Dim Backup As String = "update a " & _
                "set DELAY_CODE = isnull(a.Delay_Code, case when TURNOVERTIME <= 28 then 'No Delay' else null end) " & _
                "from DWH.UD.SurgeryTO a " & _
                "left join DWH.UD.SurgeryTO_Staff b on a.CIRCULATOR1_Name = b.Staff_Name and b.Circulator = 1 " & _
                "left join DWH.UD.SurgeryTO_Staff c on a.SURGICALTECH_Name = c.Staff_Name and c.Surgical_Tech = 1 " & _
                                "where Delay_Code Is null And TURNOVERTIME <= 28 "

                ExecuteSql(Backup)

                FixDDLs()
                GetAdminStatus()
                RefreshDates()

                PopulateSurgeryTOGrid()
                PullStaff()

            Catch ex As Exception
                'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
                'explanationlabel.DataBind()
                'ModalPopupExtender.Show()
                LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If

    End Sub

    Private Sub FixDDLs()

        Dim ddlSql As String = "Select distinct Location, 1 as ord from DWH.UD.SurgeryTO union Select '(Select Location)', 0 order by 2, 1"

        ddlSurgeryTOLocation.DataSource = GetData(ddlSql)
        ddlSurgeryTOLocation.DataTextField = "Location"
        ddlSurgeryTOLocation.DataValueField = "Location"
        ddlSurgeryTOLocation.DataBind()

        Dim adminSQL As String = "Select distinct g.Location, 1 as ord from DWH.UD.SurgeryTO g join DWH.UD.SurgeryTO_Access a on g.Location = isnull(a.Location, g.Location) where a.UserLogin = '" & _
            Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' order by 2, 1"

        ddlAdminLocation.DataSource = GetData(adminSQL)
        ddlAdminLocation.DataTextField = "Location"
        ddlAdminLocation.DataValueField = "Location"
        ddlAdminLocation.DataBind()

    End Sub

    Private Sub GetAdminStatus()
        Try
            Dim s As String = "select count(*) from DWH.UD.SurgeryTO_Access " & _
                "where AdminRights = 1 and UserLogin = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            Dim s2 As String
            If ddlSurgeryTOLocation.SelectedValue = "(Select Location)" Then

                s2 = "select count(*) from DWH.UD.SurgeryTO_Access " & _
                "where ClinSupRights = 1 and Location is null and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "
            Else

                s2 = "select count(*) from DWH.UD.SurgeryTO_Access " & _
                "where (Location is null or Location = '" & Replace(ddlSurgeryTOLocation.SelectedValue, "'", "''") & "') and ClinSupRights = 1 and UserLogin = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

            End If


            GetScalar(s)
            If GetScalar(s) > 0 Then
                tpAdministrative.Visible = True
            Else
                tpAdministrative.Visible = False
            End If


            If GetScalar(s2) > 0 Then
                ClinSupVisible = 1
            Else
                ClinSupVisible = 0
            End If
        Catch ex As Exception
            'explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            'explanationlabel.DataBind()
            'ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub RefreshDates()

        Try

            txtSurgeryTO_EndDate.Text = Today()
            txtSurgeryTO_StartDate.Text = Today.AddDays(-7)

        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Sub PopulateSurgeryTOGrid()

        Try
            Dim Loc As String = ""
            If ddlSurgeryTOLocation.SelectedValue <> "(Select Location)" Then
                Loc = "and Location = '" & Replace(ddlSurgeryTOLocation.SelectedValue, "'", "''") & "' "
            End If

            Dim s As String = "SELECT SurgeryTO_ID, BasePatientAccountNumber, ProcedureDate, Location, Room, Surgeon, isnull(CLINICALSUPERVISOR_ID, '') as ClinicalSupervisor_ID " & _
      ",ClinicalSupervisor_Name, isnull(ClinicalSupervisorPresent, '') as Clinical_Supervisor_Present " & _
      ", Reason_For_CS_Absence, isnull(CIRCULATOR1_ID, '') as Circulator1_ID , UPPER(Circulator1_Name) as Circulator1_Name " & _
      ",isnull(SURGICALTECH_ID, '') as SurgicalTech_ID, SurgicalTech_NAME, isnull(EXPEDITOR_ID, '') as Expeditor_ID, Expeditor_Name " & _
      ", TurnOverTime, Pick_Ticket_Posted, Circ_Directly_to_Preop " & _
      ",Surg_Tech_In_OR, Exp_Case_Cart_Checked_Within_45_Mins, Delay_Code, Sub_Delay_Code, Turnover_Delay_Notes, Source_LoadDate " & _
      ",Last_UserModify_Date, Last_UserModify, Active, CONVERT(VARCHAR(10), PROCEDUREDATE, 101) as ProcDate_Display, ActORIn, isnull(ORCalling, '') as ORCalling " & _
      "from  DWH.UD.SurgeryTO " & _
            " where Active = 1 " & _
            "and PROCEDUREDATE between '" & Replace(txtSurgeryTO_StartDate.Text, "'", "''") & "' and '" & Replace(txtSurgeryTO_EndDate.Text, "'", "''") & "' " & Loc & " order by ProcedureDate, Surgeon"

            SurgeryTOView = GetData(s).DefaultView
            gvSurgeryTO_Data.DataSource = SurgeryTOView
            gvSurgeryTO_Data.DataBind()


        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    'Sub PopulateSurgeryTO_DDLs()

    '    Try
    '        Dim chk As DropDownList
    '        Dim obs As DropDownList
    '        Dim ds1 As New DataTable
    '        Dim ds2 As New DataTable
    '        Dim submitsql2 As String
    '        Dim tempholder As String

    '        Dim submitsql1 As String = "select 'Yes' as Result " & _
    '            "union " & _
    '            "select 'No' "

    '        If ddlLocationSelect.SelectedValue.ToString = "2" Then
    '            submitsql2 = "select 'Yes (Nursery)' as Result " & _
    '            "union " & _
    '                "select 'No  (Nursery)' " & _
    '                "union " & _
    '                "select 'N/A' "
    '        ElseIf ddlLocationSelect.SelectedValue.ToString = "16" Then
    '            submitsql2 = "select 'Yes (Nursery)' as Result " & _
    '                "union " & _
    '                "select 'No  (Nursery)' " & _
    '                "union " & _
    '                "select 'N/A' "
    '        ElseIf ddlLocationSelect.SelectedValue.ToString = "26" Then
    '            submitsql2 = "select 'Yes (Nursery)' as Result " & _
    '                "union " & _
    '                "select 'No  (Nursery)' " & _
    '                "union " & _
    '                "select 'N/A' "
    '        ElseIf ddlLocationSelect.SelectedValue.ToString = "38" Then
    '            submitsql2 = "select 'Yes (Nursery)' as Result " & _
    '                "union " & _
    '                "select 'No  (Nursery)' " & _
    '                "union " & _
    '                "select 'N/A' "
    '        Else
    '            submitsql2 = "select 'N/A' as Result "
    '        End If

    '        ds1 = GetData(submitsql1)
    '        ds2 = GetData(submitsql2)

    '        'ds1.TableName = "SomeName1"
    '        'ds2.TableName = "SomeName2"


    '        For i As Integer = 0 To gvXRayImagingViews.Rows.Count - 1
    '            chk = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVExam"), DropDownList)
    '            obs = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVObservation"), DropDownList)
    '            If x = 1 Then
    '                tempholder = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVShielding"), Label).Text
    '            Else
    '                tempholder = obs.SelectedValue
    '            End If

    '            If chk.SelectedValue = "Abdomen" Then
    '                obs.DataSource = ds2
    '            Else
    '                obs.DataSource = ds1
    '            End If
    '            obs.DataValueField = "Result"
    '            obs.DataBind()
    '            obs.SelectedValue = tempholder
    '        Next
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
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

    'Private Sub btnSubmitObservation_Click(sender As Object, e As EventArgs) Handles btnSubmitObservation.Click

    '    Try

    '        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
    '        UserName = Replace(tmblbl.Text, "'", "''")

    '        Dim UpdatesSql As String = ""

    '        If ddlLocationSelect.SelectedValue = -1 Then
    '            explanationlabel.Text = "No X-Ray Imaging Views to Submit; Please select a Valid Location"
    '            ModalPopupExtender.Show()
    '            Exit Sub
    '        End If

    '        For i As Integer = 0 To gvXRayImagingViews.Rows.Count - 1
    '            Dim XIVID As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVID"), Label)
    '            Dim XIVCheckIn As TextBox = CType(gvXRayImagingViews.Rows(i).FindControl("txtXIVCheckInNumber"), TextBox)
    '            Dim XIVExam As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVExam"), DropDownList)
    '            Dim XIVTechnique As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVTechnique"), DropDownList)
    '            Dim XIVPositioning As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVPositioning"), DropDownList)
    '            Dim XIVMarkers As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVMarkers"), DropDownList)
    '            Dim XIVObservation As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVObservation"), DropDownList)
    '            Dim XIVConed As DropDownList = CType(gvXRayImagingViews.Rows(i).FindControl("ddlXIVConed"), DropDownList)
    '            Dim txtComment As TextBox = CType(gvXRayImagingViews.Rows(i).FindControl("txtXIVComment"), TextBox)

    '            Dim OldCheckIn As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVCheckInNumber"), Label)
    '            Dim OldExam As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVExam"), Label)
    '            Dim OldTechnique As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVTechnique"), Label)
    '            Dim OldPositioning As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVPositioning"), Label)
    '            Dim OldMarkers As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVMarkers"), Label)
    '            Dim OldObservation As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVShielding"), Label)
    '            Dim OldConed As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVConed"), Label)
    '            Dim OldComment As Label = CType(gvXRayImagingViews.Rows(i).FindControl("lblXIVComment"), Label)

    '            If CInt(XIVID.Text) < 0 Then
    '                If XIVExam.SelectedValue.ToString <> "Select Exam" Then
    '                    UpdatesSql += "Insert into DWH.KPIS.Radiology_XRay_ImagingViews values (getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    '                        "', '" & UserName & "', '" & Replace(ddlXIVFYQ.SelectedValue.ToString, "'", "''") & "', '" & _
    '                        Replace(ddlLocationSelect.SelectedValue.ToString, "'", "''") & "', '" & Replace(XIVExam.SelectedValue.ToString, "'", "''") & _
    '                        "', '" & Replace(XIVCheckIn.Text, "'", "''") & "', '" & Replace(XIVTechnique.SelectedValue.ToString, "'", "''") & _
    '                        "', '" & Replace(XIVPositioning.SelectedValue.ToString, "'", "''") & _
    '                        "', '" & Replace(XIVMarkers.SelectedValue.ToString, "'", "''") & "', '" & Replace(XIVObservation.SelectedValue.ToString, "'", "''") & _
    '                        "', '" & Replace(XIVConed.SelectedValue.ToString, "'", "''") & "', '" & Replace(txtComment.Text, "'", "''") & "', 1, null, null); "
    '                End If
    '            Else
    '                Dim NewRow As Integer = 0
    '                If XIVCheckIn.Text <> OldCheckIn.Text Then
    '                    NewRow = 1
    '                ElseIf XIVExam.SelectedValue <> OldExam.Text Then
    '                    NewRow = 1
    '                ElseIf XIVTechnique.SelectedValue <> OldTechnique.Text Then
    '                    NewRow = 1
    '                ElseIf XIVPositioning.SelectedValue <> OldPositioning.Text Then
    '                    NewRow = 1
    '                ElseIf XIVMarkers.SelectedValue <> OldMarkers.Text Then
    '                    NewRow = 1
    '                ElseIf XIVObservation.SelectedValue <> OldObservation.Text Then
    '                    NewRow = 1
    '                ElseIf XIVConed.SelectedValue <> OldConed.Text Then
    '                    NewRow = 1
    '                ElseIf txtComment.Text <> OldComment.Text Then
    '                    NewRow = 1
    '                End If
    '                If NewRow > 0 Then
    '                    UpdatesSql += "Update DWH.KPIS.Radiology_XRay_ImagingViews set Exam = '" & Replace(XIVExam.SelectedValue.ToString, "'", "''") & _
    '                        "', CheckInNumber = '" & Replace(XIVCheckIn.Text, "'", "''") & "', Technique = '" & Replace(XIVTechnique.SelectedValue.ToString, "'", "''") & _
    '                        "', Positioning = '" & Replace(XIVPositioning.SelectedValue.ToString, "'", "''") & "', Markers = '" & _
    '                        Replace(XIVMarkers.SelectedValue.ToString, "'", "''") & "', ObservationShielded = '" & Replace(XIVObservation.SelectedValue.ToString, "'", "''") & _
    '                        "', Coned = '" & Replace(XIVConed.SelectedValue.ToString, "'", "''") & "', Comment = '" & Replace(txtComment.Text, "'", "''") & _
    '                        "', ModifyDate = getdate(), ModifyUser = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    '                        "' where ID = " & Replace(XIVID.Text, "'", "''") & "; "
    '                End If
    '            End If

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
    '            explanationlabel.Text = "No Modifications to X-Ray Imaging Views"
    '            ModalPopupExtender.Show()
    '            Exit Sub
    '        End If

    '        ExecuteSql(UpdatesSql)
    '        PopulateXIVGrid()
    '        PopulateObservations(1)

    '        explanationlabel.Text = "X-Ray Imaging Views Successfully Submitted."
    '        ModalPopupExtender.Show()

    '    Catch ex As Exception
    '        explanationlabel.Text = "Error Submitting Data; Please report to admin."
    '        ModalPopupExtender.Show()
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub gvSurgeryTO_Data_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSurgeryTO_Data.PageIndexChanging
        Try

            If CheckBeforePageChange(e.NewPageIndex) = 0 Then
                gvSurgeryTO_Data.PageIndex = e.NewPageIndex
                gvSurgeryTO_Data.DataSource = SurgeryTOView
                gvSurgeryTO_Data.DataBind()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Function CheckBeforePageChange(e As Integer)
        Dim UpdateSQL As String = ""
        Dim cnt As Integer = 0
        For Each row As GridViewRow In gvSurgeryTO_Data.Rows
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

    Private Sub gvSurgeryTO_Data_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSurgeryTO_Data.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Pager Then
            Else
                If ddlSurgeryTOLocation.SelectedValue <> "(Select Location)" Then
                    e.Row.Cells(0).CssClass = "hidden"
                End If
            End If
            If ClinSupVisible = 0 Then
                e.Row.Cells(7).CssClass = "hidden"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvSurgeryTO_Data_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSurgeryTO_Data.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblSurgeryTO_ClinicSup As Label = CType(e.Row.FindControl("lblSurgeryTO_Clinic_Sup"), Label)
            Dim SurgeryTO_ClinicSup As DropDownList = CType(e.Row.FindControl("ddlSurgeryTO_Clinic_Sup"), DropDownList)

            Dim lblSurgeryTO_Location As Label = CType(e.Row.FindControl("lblSurgeryTO_Location"), Label)

            Dim ClinicSupSQL As String = "Select 'Select Supervisor' as ClinicalSupervisor, 0 as ord, 0 as realord " & _
            "union " & _
            "select isnull(Display_Name, Staff_Name) + ' - INACTIVE', SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            "where SurgeryTO_Staff_ID = '" & Replace(lblSurgeryTO_ClinicSup.Text, "'", "''") & "' and isnull(ClinicalSupervisor, 0) = 0 " & _
            "union " & _
            "select isnull(Display_Name, Staff_Name), SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and ClinicalSupervisor = 1 order by realord, ClinicalSupervisor "

            SurgeryTO_ClinicSup.DataSource = GetData(ClinicSupSQL)
            SurgeryTO_ClinicSup.DataTextField = "ClinicalSupervisor"
            SurgeryTO_ClinicSup.DataValueField = "ord"
            SurgeryTO_ClinicSup.DataBind()

            Try
                SurgeryTO_ClinicSup.SelectedValue = lblSurgeryTO_ClinicSup.Text
            Catch ex As Exception
                SurgeryTO_ClinicSup.SelectedValue = 0
            End Try

            Dim lblSurgeryTO_Circ1 As Label = CType(e.Row.FindControl("lblSurgeryTO_Circ1"), Label)
            Dim SurgeryTO_Circ1 As DropDownList = CType(e.Row.FindControl("ddlSurgeryTO_Circ1"), DropDownList)

            Dim Circ1SQL As String = "Select 'Select Circulator' as Circulator, 0 as ord, 0 as realord " & _
            "union " & _
            "select UPPER(isnull(Display_Name, Staff_Name)) + ' - INACTIVE', SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            "where SurgeryTO_Staff_ID = '" & Replace(lblSurgeryTO_ClinicSup.Text, "'", "''") & "' and isnull(Circulator, 0) = 0 " & _
            "union " & _
            "select UPPER(isnull(Display_Name, Staff_Name)), SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and Circulator = 1  order by realord, Circulator"

            SurgeryTO_Circ1.DataSource = GetData(Circ1SQL)
            SurgeryTO_Circ1.DataTextField = "Circulator"
            SurgeryTO_Circ1.DataValueField = "ord"
            SurgeryTO_Circ1.DataBind()

            Try
                SurgeryTO_Circ1.SelectedValue = lblSurgeryTO_Circ1.Text
            Catch ex As Exception
                SurgeryTO_Circ1.SelectedValue = 0
            End Try


            Dim lblSurgeryTO_SurgTech As Label = CType(e.Row.FindControl("lblSurgeryTO_SurgTech"), Label)
            Dim SurgeryTO_SurgTech As DropDownList = CType(e.Row.FindControl("ddlSurgeryTO_SurgTech"), DropDownList)

            Dim SurgTechSQL As String = "Select 'Select SurgTech' as Surgical_Tech, 0 as ord, 0 as realord " & _
            "union " & _
            "select isnull(Display_Name, Staff_Name) + ' - INACTIVE', SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            "where SurgeryTO_Staff_ID = '" & Replace(lblSurgeryTO_SurgTech.Text, "'", "''") & "' and isnull(Surgical_Tech, 0) = 0 " & _
            "union " & _
            "select isnull(Display_Name, Staff_Name), SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and Surgical_Tech = 1 order by realord, Surgical_Tech "

            SurgeryTO_SurgTech.DataSource = GetData(SurgTechSQL)
            SurgeryTO_SurgTech.DataTextField = "Surgical_Tech"
            SurgeryTO_SurgTech.DataValueField = "ord"
            SurgeryTO_SurgTech.DataBind()

            Try
                SurgeryTO_SurgTech.SelectedValue = lblSurgeryTO_SurgTech.Text
            Catch ex As Exception
                SurgeryTO_SurgTech.SelectedValue = 0
            End Try

            'Dim lblSurgeryTO_Expeditor As Label = CType(e.Row.FindControl("lblSurgeryTO_Expeditor"), Label)
            'Dim SurgeryTO_Expeditor As DropDownList = CType(e.Row.FindControl("ddlSurgeryTO_Expeditor"), DropDownList)

            'Dim ExpeditorSQL As String = "Select 'Select Expeditor' as Expeditor, 0 as ord, 0 as realord " & _
            '"union " & _
            '"select isnull(Display_Name, Staff_Name) + ' - INACTIVE', SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            '"where SurgeryTO_Staff_ID = '" & Replace(lblSurgeryTO_Expeditor.Text, "'", "''") & "' and isnull(Expeditor, 0) = 0 " & _
            '"union " & _
            '"select isnull(Display_Name, Staff_Name), SurgeryTO_Staff_ID, 1 from  DWH.UD.SurgeryTO_Staff " & _
            '"where Surgical_Tech = 1  order by realord, Expeditor"

            'SurgeryTO_Expeditor.DataSource = GetData(ExpeditorSQL)
            'SurgeryTO_Expeditor.DataTextField = "Expeditor"
            'SurgeryTO_Expeditor.DataValueField = "ord"
            'SurgeryTO_Expeditor.DataBind()

            'Try
            '    SurgeryTO_Expeditor.SelectedValue = lblSurgeryTO_Expeditor.Text
            'Catch ex As Exception
            '    SurgeryTO_Expeditor.SelectedValue = 0
            'End Try

            Dim lblSurgeryTO_DelayCode As Label = CType(e.Row.FindControl("lblSurgeryTO_DelayCode"), Label)
            Dim SurgeryTO_DelayCode As DropDownList = CType(e.Row.FindControl("ddlSurgeryTO_DelayCode"), DropDownList)

            Dim DelayCodeSQL As String = "Select 'Select Delay Code' as DelayCode, '(No Delay Code Selected)' as ord, 0 as realord " & _
            "union " & _
            "select Code_Name + ' - INACTIVE', Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and Code_Name = '" & Replace(lblSurgeryTO_DelayCode.Text, "'", "''") & "' and Code_Classification <> 'Delay_Code' " & _
            "union " & _
            "select Code_Name, Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and Code_Classification = 'Delay_Code'  order by realord, ord"

            SurgeryTO_DelayCode.DataSource = GetData(DelayCodeSQL)
            SurgeryTO_DelayCode.DataTextField = "DelayCode"
            SurgeryTO_DelayCode.DataValueField = "ord"
            SurgeryTO_DelayCode.DataBind()

            Try
                SurgeryTO_DelayCode.SelectedValue = lblSurgeryTO_DelayCode.Text
            Catch ex As Exception
                SurgeryTO_DelayCode.SelectedValue = "(No Delay Code Selected)"
            End Try



            Dim lblSurgeryTO_SubDelayCode As Label = CType(e.Row.FindControl("lblSurgeryTO_SubDelayCode"), Label)
            Dim SurgeryTO_SubDelayCode As DropDownList = CType(e.Row.FindControl("ddlSurgeryTO_SubDelayCode"), DropDownList)

            Select Case SurgeryTO_DelayCode.SelectedValue
                Case "OR"
                    SurgeryTO_SubDelayCode.Visible = True
                Case "ORNOTREADY"
                    SurgeryTO_SubDelayCode.Visible = True
                Case "No Reason Documented"
                    SurgeryTO_SubDelayCode.Visible = True
                Case Else
                    SurgeryTO_SubDelayCode.Visible = False
            End Select

            Dim SubDelayCodeSQL As String = "Select 'Select Sub Delay Code' as SubDelayCode, '(No Delay Code Selected)' as ord, 0 as realord " & _
            "union " & _
            "select Code_Name + ' - INACTIVE', Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and Code_Name = '" & Replace(lblSurgeryTO_SubDelayCode.Text, "'", "''") & "' and Code_Classification <> 'Sub_Delay_Code' " & _
            "union " & _
            "select Code_Name, Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
            "where Location = '" & Replace(lblSurgeryTO_Location.Text, "'", "''") & "' and Code_Classification = 'Sub_Delay_Code' order by realord, ord "

            SurgeryTO_SubDelayCode.DataSource = GetData(SubDelayCodeSQL)
            SurgeryTO_SubDelayCode.DataTextField = "SubDelayCode"
            SurgeryTO_SubDelayCode.DataValueField = "ord"
            SurgeryTO_SubDelayCode.DataBind()

            Try
                SurgeryTO_SubDelayCode.SelectedValue = lblSurgeryTO_SubDelayCode.Text
            Catch ex As Exception
                SurgeryTO_SubDelayCode.SelectedValue = "(No Delay Code Selected)"
            End Try

        End If

    End Sub
    'Private Sub CancelButtonHHPageChange_Click(sender As Object, e As EventArgs) Handles CancelButtonHHPageChange.Click
    '    ModalPopupExtenderHH2.Hide()
    'End Sub
    'Private Sub SubmitButtonHHPageChange_Click(sender As Object, e As EventArgs) Handles SubmitButtonHHPageChange.Click
    '    ModalPopupExtenderHH2.Hide()
    '    UpdateHHRows()
    '    Dim e2 As Integer
    '    e2 = hiddenLblpass.Text
    '    gvSubmitData.PageIndex = e2
    '    gvSubmitData.DataSource = AdminView
    '    gvSubmitData.DataBind()
    'End Sub
    'Private Sub btnChangePage_Click(sender As Object, e As EventArgs) Handles btnChangePage.Click
    '    Try
    '        Dim e2 As Integer
    '        e2 = hiddenLblpass.Text
    '        gvSubmitData.PageIndex = e2
    '        gvSubmitData.DataSource = AdminView
    '        gvSubmitData.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub gvSurgeryTO_Data_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSurgeryTO_Data.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = SurgeryTOView

            sorts = e.SortExpression

            If e.SortExpression = SurgeryTOmap Then

                If SurgeryTOdir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    SurgeryTOdir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    SurgeryTOdir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                SurgeryTOdir = 1
                SurgeryTOmap = e.SortExpression
            End If

            gvSurgeryTO_Data.DataSource = dv
            gvSurgeryTO_Data.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub txtSurgeryTO_EndDate_TextChanged(sender As Object, e As EventArgs) Handles txtSurgeryTO_EndDate.TextChanged
        PopulateSurgeryTOGrid()
    End Sub

    Private Sub txtSurgeryTO_StartDate_TextChanged(sender As Object, e As EventArgs) Handles txtSurgeryTO_StartDate.TextChanged
        PopulateSurgeryTOGrid()
    End Sub

    Private Sub btnSubmitSurgeryTO_Click(sender As Object, e As EventArgs) Handles btnSubmitSurgeryTO.Click
        SubmitSurgeryTO_Rows()
    End Sub
    Private Sub SubmitSurgeryTO_Rows()
        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim UpdatesSql As String = ""

            For i As Integer = 0 To gvSurgeryTO_Data.Rows.Count - 1
                Dim SurgeryTO_ID As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_ID"), Label)
                Dim SurgeryTO_Clinic_Sup As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_Clinic_Sup"), Label)
                Dim SurgeryTO_Clinic_Sup_Pres As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_Clinic_Sup_Pres"), Label)
                'Dim SurgeryTO_CS_Absence As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_CS_Absence"), Label)
                Dim SurgeryTO_Circ1 As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_Circ1"), Label)
                Dim SurgeryTO_SurgTech As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_SurgTech"), Label)
                'Dim SurgeryTO_Expeditor As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_Expeditor"), Label)
                'Dim SurgeryTO_PickTick As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_PickTick"), Label)
                'Dim SurgeryTO_CircDirect As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_CircDirect"), Label)
                'Dim SurgeryTO_SurgTechOR As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_SurgTechOR"), Label)
                'Dim SurgeryTO_CaseCart As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_CaseCart"), Label)
                Dim SurgeryTO_DelayCode As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_DelayCode"), Label)
                Dim SurgeryTO_SubDelayCode As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_SubDelayCode"), Label)
                Dim SurgeryTO_TurnDelayNotes As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_TurnDelayNotes"), Label)
                Dim SurgeryTO_ORCalling As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_ORCalling"), Label)

                Dim NewSurgeryTO_Clinic_Sup As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Clinic_Sup"), DropDownList)
                Dim NewSurgeryTO_Clinic_Sup_Pres As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Clinic_Sup_Pres"), DropDownList)
                'Dim NewSurgeryTO_CS_Absence As TextBox = CType(gvSurgeryTO_Data.Rows(i).FindControl("txtSurgeryTO_CS_Absence"), TextBox)
                Dim NewSurgeryTO_Circ1 As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Circ1"), DropDownList)
                Dim NewSurgeryTO_SurgTech As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_SurgTech"), DropDownList)
                'Dim NewSurgeryTO_Expeditor As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Expeditor"), DropDownList)
                'Dim NewSurgeryTO_PickTick As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_PickTick"), DropDownList)
                'Dim NewSurgeryTO_CircDirect As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_CircDirect"), DropDownList)
                'Dim NewSurgeryTO_SurgTechOR As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddl_SurgeryTO_SurgTechOR"), DropDownList)
                'Dim NewSurgeryTO_CaseCart As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_CaseCart"), DropDownList)
                Dim NewSurgeryTO_DelayCode As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_DelayCode"), DropDownList)
                Dim NewSurgeryTO_SubDelayCode As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_SubDelayCode"), DropDownList)
                Dim NewSurgeryTO_TurnDelayNotes As TextBox = CType(gvSurgeryTO_Data.Rows(i).FindControl("txtSurgeryTO_TurnDelayNotes"), TextBox)
                Dim NewSurgeryTO_ORCalling As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_ORCalling"), DropDownList)



                Dim NewRow As Integer = 0
                If NewSurgeryTO_Clinic_Sup.SelectedValue <> SurgeryTO_Clinic_Sup.Text Then
                    NewRow = 1
                ElseIf NewSurgeryTO_Clinic_Sup_Pres.SelectedValue <> SurgeryTO_Clinic_Sup_Pres.Text Then
                    NewRow = 1
                    'ElseIf NewSurgeryTO_CS_Absence.Text <> SurgeryTO_CS_Absence.Text Then
                    '    NewRow = 1
                ElseIf NewSurgeryTO_Circ1.SelectedValue <> SurgeryTO_Circ1.Text Then
                    NewRow = 1
                ElseIf NewSurgeryTO_SurgTech.SelectedValue <> SurgeryTO_SurgTech.Text Then
                    NewRow = 1
                    'ElseIf NewSurgeryTO_Expeditor.SelectedValue <> SurgeryTO_Expeditor.Text Then
                    '    NewRow = 1
                    'ElseIf NewSurgeryTO_PickTick.SelectedValue <> SurgeryTO_PickTick.Text Then
                    '    NewRow = 1
                    'ElseIf NewSurgeryTO_CircDirect.SelectedValue <> SurgeryTO_CircDirect.Text Then
                    '    NewRow = 1
                    'ElseIf NewSurgeryTO_SurgTechOR.SelectedValue <> SurgeryTO_SurgTechOR.Text Then
                    '    NewRow = 1
                    'ElseIf NewSurgeryTO_CaseCart.SelectedValue <> SurgeryTO_CaseCart.Text Then
                    '    NewRow = 1
                ElseIf NewSurgeryTO_DelayCode.SelectedValue <> SurgeryTO_DelayCode.Text Then
                    NewRow = 1
                ElseIf NewSurgeryTO_SubDelayCode.SelectedValue <> SurgeryTO_SubDelayCode.Text Then
                    NewRow = 1
                ElseIf NewSurgeryTO_TurnDelayNotes.Text <> SurgeryTO_TurnDelayNotes.Text Then
                    NewRow = 1
                ElseIf NewSurgeryTO_ORCalling.SelectedValue <> SurgeryTO_ORCalling.Text Then
                    NewRow = 1
                End If

                If NewRow > 0 Then

                    If NewSurgeryTO_DelayCode.SelectedValue = "OR" And NewSurgeryTO_SubDelayCode.SelectedValue = "(No Delay Code Selected)" Then
                        explanationlabel.Text = "Sub Delay Code Required Delay Code of 'OR'"
                        ModalPopupExtender.Show()
                        Exit Sub
                    ElseIf NewSurgeryTO_DelayCode.SelectedValue = "ORNOTREADY" And NewSurgeryTO_SubDelayCode.SelectedValue = "(No Delay Code Selected)" Then
                        explanationlabel.Text = "Sub Delay Code Required for Delay Code of 'ORNOTREADY'"
                        ModalPopupExtender.Show()
                        Exit Sub
                    ElseIf NewSurgeryTO_DelayCode.SelectedValue = "No Reason Documented" And NewSurgeryTO_SubDelayCode.SelectedValue = "(No Delay Code Selected)" Then
                        explanationlabel.Text = "Sub Delay Code Required for Delay Code of 'No reason Documented'"
                        ModalPopupExtender.Show()
                        Exit Sub
                    End If

                    Dim sNewSurgeryTO_Clinic_Sup As String
                    Dim sNewSurgeryTO_Clinic_Sup_Pres As String
                    Dim sNewSurgeryTO_CS_Absence As String
                    Dim sNewSurgeryTO_Circ1 As String
                    Dim sNewSurgeryTO_SurgTech As String
                    Dim sNewSurgeryTO_Expeditor As String
                    'Dim sNewSurgeryTO_PickTick As String
                    'Dim sNewSurgeryTO_CircDirect As String
                    'Dim sNewSurgeryTO_SurgTechOR As String
                    'Dim sNewSurgeryTO_CaseCart As String
                    Dim sNewSurgeryTO_DelayCode As String
                    Dim sNewSurgeryTO_SubDelayCode As String
                    Dim sNewSurgeryTO_TurnDelayNotes As String
                    Dim sNewSurgeryTO_ORCalling As String

                    If Trim(Replace(NewSurgeryTO_Clinic_Sup.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_Clinic_Sup = "null"
                    Else
                        sNewSurgeryTO_Clinic_Sup = "'" & Trim(Replace(NewSurgeryTO_Clinic_Sup.SelectedValue.ToString, "'", "''")) & "'"
                    End If
                    If Trim(Replace(NewSurgeryTO_Clinic_Sup_Pres.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_Clinic_Sup_Pres = "null"
                    Else
                        sNewSurgeryTO_Clinic_Sup_Pres = "'" & Trim(Replace(NewSurgeryTO_Clinic_Sup_Pres.SelectedValue.ToString, "'", "''")) & "'"
                    End If
                    'If Trim(Replace(NewSurgeryTO_CS_Absence.Text.ToString, "'", "''")) = "" Then
                    '    sNewSurgeryTO_CS_Absence = "null"
                    'Else
                    '    sNewSurgeryTO_CS_Absence = "'" & Trim(Replace(NewSurgeryTO_CS_Absence.Text.ToString, "'", "''")) & "'"
                    'End If
                    If Trim(Replace(NewSurgeryTO_Circ1.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_Circ1 = "null"
                    Else
                        sNewSurgeryTO_Circ1 = "'" & Trim(Replace(NewSurgeryTO_Circ1.SelectedValue.ToString, "'", "''")) & "'"
                    End If
                    If Trim(Replace(NewSurgeryTO_SurgTech.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_SurgTech = "null"
                    Else
                        sNewSurgeryTO_SurgTech = "'" & Trim(Replace(NewSurgeryTO_SurgTech.SelectedValue.ToString, "'", "''")) & "'"
                    End If
                    'If Trim(Replace(NewSurgeryTO_Expeditor.SelectedValue.ToString, "'", "''")) = "" Then
                    '    sNewSurgeryTO_Expeditor = "null"
                    'Else
                    '    sNewSurgeryTO_Expeditor = "'" & Trim(Replace(NewSurgeryTO_Expeditor.SelectedValue.ToString, "'", "''")) & "'"
                    'End If
                    'If Trim(Replace(NewSurgeryTO_PickTick.SelectedValue.ToString, "'", "''")) = "" Then
                    '    sNewSurgeryTO_PickTick = "null"
                    'Else
                    '    sNewSurgeryTO_PickTick = "'" & Trim(Replace(NewSurgeryTO_PickTick.SelectedValue.ToString, "'", "''")) & "'"
                    'End If
                    'If Trim(Replace(NewSurgeryTO_CircDirect.SelectedValue.ToString, "'", "''")) = "" Then
                    '    sNewSurgeryTO_CircDirect = "null"
                    'Else
                    '    sNewSurgeryTO_CircDirect = "'" & Trim(Replace(NewSurgeryTO_CircDirect.SelectedValue.ToString, "'", "''")) & "'"
                    'End If
                    'If Trim(Replace(NewSurgeryTO_SurgTechOR.SelectedValue.ToString, "'", "''")) = "" Then
                    '    sNewSurgeryTO_SurgTechOR = "null"
                    'Else
                    '    sNewSurgeryTO_SurgTechOR = "'" & Trim(Replace(NewSurgeryTO_SurgTechOR.SelectedValue.ToString, "'", "''")) & "'"
                    'End If
                    'If Trim(Replace(NewSurgeryTO_CaseCart.SelectedValue.ToString, "'", "''")) = "" Then
                    '    sNewSurgeryTO_CaseCart = "null"
                    'Else
                    '    sNewSurgeryTO_CaseCart = "'" & Trim(Replace(NewSurgeryTO_CaseCart.SelectedValue.ToString, "'", "''")) & "'"
                    'End If
                    If Trim(Replace(NewSurgeryTO_DelayCode.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_DelayCode = "null"
                    ElseIf Trim(Replace(NewSurgeryTO_DelayCode.SelectedValue.ToString, "'", "''")) = "(No Delay Code Selected)" Then
                        sNewSurgeryTO_DelayCode = "null"
                    Else
                        sNewSurgeryTO_DelayCode = "'" & Trim(Replace(NewSurgeryTO_DelayCode.SelectedValue.ToString, "'", "''")) & "'"
                    End If
                    If Trim(Replace(NewSurgeryTO_SubDelayCode.SelectedValue.ToString, "'", "''")) = "(No Delay Code Selected)" Then
                        sNewSurgeryTO_SubDelayCode = "null"
                    ElseIf Trim(Replace(NewSurgeryTO_SubDelayCode.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_SubDelayCode = "null"
                    Else
                        sNewSurgeryTO_SubDelayCode = "'" & Trim(Replace(NewSurgeryTO_SubDelayCode.SelectedValue.ToString, "'", "''")) & "'"
                    End If
                    If Trim(Replace(NewSurgeryTO_TurnDelayNotes.Text.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_TurnDelayNotes = "null"
                    Else
                        sNewSurgeryTO_TurnDelayNotes = "'" & Trim(Replace(NewSurgeryTO_TurnDelayNotes.Text.ToString, "'", "''")) & "'"
                    End If
                    If Trim(Replace(NewSurgeryTO_ORCalling.SelectedValue.ToString, "'", "''")) = "" Then
                        sNewSurgeryTO_ORCalling = "null"
                    Else
                        sNewSurgeryTO_ORCalling = "'" & Trim(Replace(NewSurgeryTO_ORCalling.SelectedValue.ToString, "'", "''")) & "'"
                    End If

                    UpdatesSql += "Update a set ClinicalSuperVisor_ID = " & sNewSurgeryTO_Clinic_Sup & _
                        ", ClinicalSupervisor_Name = isnull(b.Display_Name, b.Staff_Name),  ClinicalSupervisorPresent = " & sNewSurgeryTO_Clinic_Sup_Pres & _
                        ", Circulator1_ID = " & sNewSurgeryTO_Circ1 & _
                        ", Circulator1_Name = isnull(c.Display_Name, c.Staff_Name), SurgicalTech_ID = " & sNewSurgeryTO_SurgTech & ", SurgicalTech_Name = isnull(d.Display_Name, d.Staff_Name) " & _
                        ", Delay_Code =" & sNewSurgeryTO_DelayCode & _
                        ", Sub_Delay_Code = " & sNewSurgeryTO_SubDelayCode & ", Turnover_Delay_Notes = " & sNewSurgeryTO_TurnDelayNotes & _
                        ", ORCalling = " & sNewSurgeryTO_ORCalling & _
                        ", Last_UserModify_Date = getdate(), Last_UserModify = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                        "' from DWH.UD.SurgeryTO a " & _
                        " left join DWH.UD.SurgeryTO_Staff b on '" & Replace(NewSurgeryTO_Clinic_Sup.SelectedValue.ToString, "'", "''") & "' = b.SurgeryTO_Staff_ID " & _
                        " left join DWH.UD.SurgeryTO_Staff c on '" & Replace(NewSurgeryTO_Circ1.SelectedValue.ToString, "'", "''") & "' = c.SurgeryTO_Staff_ID " & _
                        " left join DWH.UD.SurgeryTO_Staff d on '" & Replace(NewSurgeryTO_SurgTech.SelectedValue.ToString, "'", "''") & "' = d.SurgeryTO_Staff_ID " & _
                        " where SurgeryTO_ID = " & Replace(SurgeryTO_ID.Text, "'", "''") & "; "
                End If
                ', Reason_For_CS_Absence = " & sNewSurgeryTO_CS_Absence & ", Expeditor_ID = " & sNewSurgeryTO_Expeditor, Expeditor_Name = isnull(e.Display_Name, e.Staff_Name) &
                '                        " left join DWH.UD.SurgeryTO_Staff e on '" & Replace(NewSurgeryTO_Expeditor.SelectedValue.ToString, "'", "''") & "' = e.SurgeryTO_Staff_ID " & _

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
                explanationlabel.Text = "No Modifications to Data"
                ModalPopupExtender.Show()
                Exit Sub
            End If

            ExecuteSql(UpdatesSql)
            PopulateSurgeryTOGrid()

            explanationlabel.Text = "Data Successfully Submitted."
            ModalPopupExtender.Show()

        Catch ex As Exception
            explanationlabel.Text = "Error Submitting Data; Please report to Website Administrator (" & WebAdminEmail & ")."
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PullStaff()
        Try

            Dim StaffSelection As String = "ClinicalSupervisor"
            If ddlManageWhat.SelectedValue = 1 Then
                StaffSelection = "ClinicalSupervisor"
            ElseIf ddlManageWhat.SelectedValue = 2 Then
                StaffSelection = "Circulator"
            ElseIf ddlManageWhat.SelectedValue = 3 Then
                StaffSelection = "Surgical_Tech"
                'ElseIf ddlManageWhat.SelectedValue = 4 Then
                '    StaffSelection = "Expeditor"
            End If

            Dim ExtraClause As String = ""
            If chkInactiveStaff.Checked = True Then
                ExtraClause = " or 2=2 "
            End If

            Dim s As String = "select * " & _
            ", case when ClinicalSupervisor = 1 then 'Inactivate' else 'Activate' end as ClinSupStatus " & _
            ", case when Circulator = 1 then 'Inactivate' else 'Activate' end as CirculatorStatus " & _
            ", case when Surgical_Tech = 1 then 'Inactivate' else 'Activate' end as SurgTechStatus " & _
            ", case when Expeditor = 1 then 'Inactivate' else 'Activate' end as ExpeditorStatus " & _
            ", isnull(Display_Name, Staff_Name) as StaffName " & _
            "from DWH.UD.SurgeryTO_Staff " & _
            "where Location = '" & Replace(ddlAdminLocation.SelectedValue, "'", "''") & "' and (" & StaffSelection & " = 1 " & ExtraClause & ")" & _
            "and Staff_Name like '%" & Replace(txtStaffName_SearchSubmit.Text, "'", "''") & "%' " & _
            "order by Staff_Name "

            Staffdv = GetData(s).DefaultView
            gvSurgeryTOStaff.DataSource = Staffdv
            gvSurgeryTOStaff.DataBind()


        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub PullCodes()
        Try

            Dim StaffSelection As String = "Delay_Code"
            If ddlManageWhat.SelectedValue = 5 Then
                StaffSelection = "Delay_Code"
            ElseIf ddlManageWhat.SelectedValue = 6 Then
                StaffSelection = "Sub_Delay_Code"
            End If


            Dim s As String = "select *, case when Code_Name = 'Other' then 'OR and ORNOTREADY' else Delay_Limiter end as Parent_Delay_Code " & _
            ", case when Code_Classification = '" & StaffSelection & "' then 'Inactivate' else 'Activate' end as ActiveStatus " & _
            "from DWH.UD.SurgeryTO_Codes " & _
            "where Location = '" & Replace(ddlAdminLocation.SelectedValue, "'", "''") & "' and (Code_Classification = '" & StaffSelection & "')" & _
            "and Code_Name like '%" & Replace(txtCodeDesc_SrchSubmit.Text, "'", "''") & "%' " & _
            "order by Code_Name "

            Codedv = GetData(s).DefaultView
            gvGFSOT_Codes.DataSource = Codedv
            gvGFSOT_Codes.DataBind()


        Catch ex As Exception
            explanationlabel.Text = "Error loading data.  Please contact Website Administrator (" & WebAdminEmail & ")."
            explanationlabel.DataBind()
            ModalPopupExtender.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub txtStaffName_SearchSubmit_TextChanged(sender As Object, e As EventArgs) Handles txtStaffName_SearchSubmit.TextChanged
        PullStaff()
    End Sub

    Private Sub chkInactiveStaff_CheckedChanged(sender As Object, e As EventArgs) Handles chkInactiveStaff.CheckedChanged
        PullStaff()
    End Sub

    Private Sub ddlManageWhat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageWhat.SelectedIndexChanged
        If ddlManageWhat.SelectedValue = 1 Then
            btnNewStaff.Text = "Add New Clinical Supervisor"
        ElseIf ddlManageWhat.SelectedValue = 2 Then
            btnNewStaff.Text = "Add New Circulator"
        ElseIf ddlManageWhat.SelectedValue = 3 Then
            btnNewStaff.Text = "Add New Surgical_Tech"
            'ElseIf ddlManageWhat.SelectedValue = 4 Then
            '    btnNewStaff.Text = "Add New Expeditor"
        ElseIf ddlManageWhat.SelectedValue = 5 Then
            btnNewCode.Text = "Add New Delay Code"
            pnlClinSups.Visible = False
            pnlCodes.Visible = True
            PullCodes()
            Exit Sub
        ElseIf ddlManageWhat.SelectedValue = 6 Then
            btnNewCode.Text = "Add New Sub Delay Code"
            pnlClinSups.Visible = False
            pnlCodes.Visible = True
            PullCodes()
            Exit Sub
        End If
        pnlCodes.Visible = False
        pnlClinSups.Visible = True
        PullStaff()
    End Sub

    Private Sub gvSurgeryTOStaff_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvSurgeryTOStaff.RowCancelingEdit
        Try
            gvSurgeryTOStaff.EditIndex = -1
            gvSurgeryTOStaff.DataSource = Staffdv
            gvSurgeryTOStaff.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSurgeryTOStaff_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSurgeryTOStaff.RowCommand
        Try
            Dim SurgeryTO_Staff_ID As String = e.CommandArgument
            Dim varname As String = e.CommandName
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Select Case varname
                Case "Edit"
                Case "Update"
                Case "Cancel"
                Case Else

                    Dim Sql As String = "update DWH.UD.SurgeryTO_Staff set " & varname & " = 1 - " & varname & " where SurgeryTO_Staff_ID = '" & Replace(SurgeryTO_Staff_ID, "'", "''") & "'"

                    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        cmd = New SqlCommand(Sql, conn)
                        cmd.CommandTimeout = 86400
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.ExecuteNonQuery()
                    End Using

                    gvSurgeryTOStaff.EditIndex = -1
                    PullStaff()

            End Select


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSurgeryTOStaff_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSurgeryTOStaff.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvSurgeryTOStaff.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If
    End Sub


    Private Sub gvSurgeryTOStaff_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvSurgeryTOStaff.RowEditing
        Try

            gvSurgeryTOStaff.EditIndex = e.NewEditIndex
            gvSurgeryTOStaff.DataSource = Staffdv
            gvSurgeryTOStaff.DataBind()

            Dim txtName As TextBox = gvSurgeryTOStaff.Rows(e.NewEditIndex).FindControl("txtStaffName")
            Dim lblName As Label = gvSurgeryTOStaff.Rows(e.NewEditIndex).FindControl("lblStaffName")

            txtName.Visible = True
            lblName.Visible = False


            For Each canoe As GridViewRow In gvSurgeryTOStaff.Rows
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

    Private Sub gvSurgeryTOStaff_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvSurgeryTOStaff.RowUpdating
        Try
            Dim depid As String = gvSurgeryTOStaff.DataKeys(e.RowIndex).Value.ToString

            Dim txtDept As TextBox = gvSurgeryTOStaff.Rows(e.RowIndex).FindControl("txtStaffName")
            Dim lblDept As Label = gvSurgeryTOStaff.Rows(e.RowIndex).FindControl("lblStaffName")

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim Sql As String = "update DWH.UD.SurgeryTO_Staff " & _
                "set Display_Name = '" & Replace(txtDept.Text, "'", "''") & "' " & _
                "where SurgeryTO_Staff_ID = '" & Replace(depid, "'", "''") & "'"



            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(Sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvSurgeryTOStaff.EditIndex = -1
            PullStaff()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSurgeryTOStaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSurgeryTOStaff.SelectedIndexChanged
        For Each canoe As GridViewRow In gvSurgeryTOStaff.Rows
            If canoe.RowIndex = gvSurgeryTOStaff.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub btnNewStaff_Click(sender As Object, e As EventArgs) Handles btnNewStaff.Click

        Dim StaffType As String = ""
        If ddlManageWhat.SelectedValue = 1 Then
            StaffType = "ClinicalSupervisor"
        ElseIf ddlManageWhat.SelectedValue = 2 Then
            StaffType = "Circulator"
        ElseIf ddlManageWhat.SelectedValue = 3 Then
            StaffType = "Surgical_Tech"
            'ElseIf ddlManageWhat.SelectedValue = 4 Then
            '    StaffType = "Expeditor"
        End If

        Dim ChkSql As String = "Select count(*) from DWH.UD.SurgeryTO_Staff where Location = '" & Replace(ddlAdminLocation.SelectedValue, "'", "''") & "' and Staff_Name = '" & _
            Replace(txtStaffName_SearchSubmit.Text, "'", "''") & "'"

        If GetScalar(ChkSql) > 0 Then
            explanationlabelp4.Text = "Staff Member with this name already exists; please revise"
            ModalPopupExtenderp4.Show()
            Exit Sub
        End If

        Dim sql As String = "Insert into DWH.UD.SurgeryTO_Staff (Staff_Name, " & StaffType & ", Location) values ( " & _
            "'" & Replace(txtStaffName_SearchSubmit.Text, "'", "''") & _
            "', 1, '" & Replace(ddlAdminLocation.SelectedValue, "'", "''") & "')"

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

        gvSurgeryTOStaff.EditIndex = -1
        PullStaff()

        explanationlabelp4.Text = "New Staff Member Added"
        ModalPopupExtenderp4.Show()

    End Sub

    Private Sub gvGFSOT_Codes_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvGFSOT_Codes.RowCancelingEdit
        Try
            gvGFSOT_Codes.EditIndex = -1
            gvGFSOT_Codes.DataSource = Codedv
            gvGFSOT_Codes.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvGFSOT_Codes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvGFSOT_Codes.RowCommand
        Try
            Dim SurgeryTO_Staff_ID As String = e.CommandArgument
            Dim varname As String = e.CommandName
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Select Case varname
                Case "Edit"
                Case "Cancel"
                Case "Update"
                Case Else


                    Dim Sql As String = "delete from DWH.UD.SurgeryTO_Codes where SurgeryTO_Code_ID = '" & Replace(SurgeryTO_Staff_ID, "'", "''") & "'"

                    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        cmd = New SqlCommand(Sql, conn)
                        cmd.CommandTimeout = 86400
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.ExecuteNonQuery()
                    End Using

                    PullCodes()

            End Select


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    'Private Sub gvGFSOT_Codes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvGFSOT_Codes.RowCommand
    '    Try
    '        Dim SurgeryTO_Staff_ID As String = e.CommandArgument
    '        Dim varname As String = e.CommandName
    '        Dim cmd As SqlCommand
    '        Dim da As New SqlDataAdapter
    '        Dim ds As New DataSet

    '        Dim Sql As String = "Delete from DWH.UD.SurgeryTO_Codes where SurgeryTO_Code_ID = '" & Replace(SurgeryTO_Staff_ID, "'", "''") & "'"

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(Sql, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvGFSOT_Codes.EditIndex = -1
    '        PullCodes()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub gvGFSOT_Codes_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvGFSOT_Codes.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvGFSOT_Codes.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If
    End Sub

    Private Sub gvGFSOT_Codes_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvGFSOT_Codes.RowEditing
        Try

            gvGFSOT_Codes.EditIndex = e.NewEditIndex
            gvGFSOT_Codes.DataSource = Codedv
            gvGFSOT_Codes.DataBind()

            Dim txtName As TextBox = gvGFSOT_Codes.Rows(e.NewEditIndex).FindControl("txtAnythingElse")
            Dim lblName As Label = gvGFSOT_Codes.Rows(e.NewEditIndex).FindControl("lblStaffName")

            txtName.Visible = True
            lblName.Visible = False

            If ddlManageWhat.SelectedValue = 6 Then
                Dim txtParentDelay As TextBox = gvGFSOT_Codes.Rows(e.NewEditIndex).FindControl("txtParent_Delay_Code")
                Dim lblParentDelay As Label = gvGFSOT_Codes.Rows(e.NewEditIndex).FindControl("lblParent_Delay_Code")

                txtParentDelay.Visible = True
                lblParentDelay.Visible = False
            End If

            For Each canoe As GridViewRow In gvGFSOT_Codes.Rows
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

    Private Sub gvGFSOT_Codes_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvGFSOT_Codes.RowUpdating

        Try
            Dim depid As String = gvGFSOT_Codes.DataKeys(e.RowIndex).Value.ToString

            Dim txtDept As TextBox = gvGFSOT_Codes.Rows(e.RowIndex).FindControl("txtAnythingElse")
            Dim lblDept As Label = gvGFSOT_Codes.Rows(e.RowIndex).FindControl("lblStaffName")

            Dim txtParentDelay As TextBox = gvGFSOT_Codes.Rows(e.RowIndex).FindControl("txtParent_Delay_Code")
            Dim lblParentDelay As Label = gvGFSOT_Codes.Rows(e.RowIndex).FindControl("lblParent_Delay_Code")

            Dim s As String = txtParentDelay.Text

            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim Sql As String = "update DWH.UD.SurgeryTO_Codes " & _
                "set Code_Name = '" & Replace(txtDept.Text, "'", "''") & "' " & _
                ", Delay_Limiter = case when Code_Name = 'Other' then Delay_Limiter else '" & Replace(txtParentDelay.Text, "'", "''") & "' end " & _
                "where SurgeryTO_Code_ID = '" & Replace(depid, "'", "''") & "'"



            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(Sql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvGFSOT_Codes.EditIndex = -1
            PullCodes()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvGFSOT_Codes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvGFSOT_Codes.SelectedIndexChanged
        For Each canoe As GridViewRow In gvGFSOT_Codes.Rows
            If canoe.RowIndex = gvGFSOT_Codes.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub btnNewCode_Click(sender As Object, e As EventArgs) Handles btnNewCode.Click

        Dim StaffType As String = ""
        If ddlManageWhat.SelectedValue = 5 Then
            StaffType = "Delay_Code"
        ElseIf ddlManageWhat.SelectedValue = 6 Then
            StaffType = "Sub_Delay_Code"
        End If

        Dim ChkSql As String = "Select count(*) from DWH.UD.SurgeryTO_Codes where  Location = '" & Replace(ddlAdminLocation.SelectedValue, "'", "''") & "' and Code_Name = '" & _
            Replace(txtStaffName_SearchSubmit.Text, "'", "''") & "' and Code_Classification = '" & StaffType & "'"

        If GetScalar(ChkSql) > 0 Then
            explanationlabelp4.Text = StaffType & " with this name already exists"
            ModalPopupExtenderp4.Show()
            Exit Sub
        End If

        Dim sql As String = "Insert into DWH.UD.SurgeryTO_Codes  values ( " & _
            "'" & Replace(txtCodeDesc_SrchSubmit.Text, "'", "''") & _
            "', '" & StaffType & "', null,  '" & Replace(ddlAdminLocation.SelectedValue, "'", "''") & "' )"

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

        gvSurgeryTOStaff.EditIndex = -1
        PullCodes()

        explanationlabelp4.Text = "New Code Added"
        ModalPopupExtenderp4.Show()

    End Sub

    Protected Sub ddlDelayCode_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvSurgeryTO_Data.Rows.Count - 1

            Dim lblDelayCode As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_DelayCode"), Label)
            Dim ddlDelayCode As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_DelayCode"), DropDownList)
            Dim ddlSubDelayCode As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_SubDelayCode"), DropDownList)
            Dim lblSubDelayCode As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_SubDelayCode"), Label)
            Dim lblHiddenDelayCode As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblHiddenDelayCode"), Label)
            Dim SurgeryTO_ClinicSup As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Clinic_Sup"), DropDownList)

            Dim lblLocation As Label = CType(gvSurgeryTO_Data.Rows(i).FindControl("lblSurgeryTO_Location"), Label)

            Dim SubDelay As String = ddlSubDelayCode.SelectedValue

            Dim NewRow As Integer = 0
            Dim SubDelayCodeSQL = ""
            If ddlDelayCode.SelectedValue <> lblHiddenDelayCode.Text Then
                lblHiddenDelayCode.Text = ddlDelayCode.SelectedValue
                Select Case ddlDelayCode.SelectedValue
                    Case "OR"
                        ddlSubDelayCode.Visible = True
                        SubDelayCodeSQL = "Select 'Select Sub Delay Code' as SubDelayCode, '(No Delay Code Selected)' as ord, 0 as realord " & _
                        "union " & _
                        "select Code_Name + ' - INACTIVE', Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        "where Code_Name = '" & Replace(lblSubDelayCode.Text, "'", "''") & "' and (Code_Classification <> 'Sub_Delay_Code' or (Delay_Limiter <>'OR' and Code_Name <> 'Other')) " & _
                        "union " & _
                        "select Code_Name, Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        "where Location = '" & Replace(lblLocation.Text, "'", "''") & "' and Code_Classification = 'Sub_Delay_Code' and (Delay_Limiter = 'OR' or Code_Name = 'Other') order by realord, ord "
                        ddlSubDelayCode.DataSource = GetData(SubDelayCodeSQL)
                        ddlSubDelayCode.DataTextField = "SubDelayCode"
                        ddlSubDelayCode.DataValueField = "ord"
                        ddlSubDelayCode.DataBind()
                    Case "ORNOTREADY"
                        ddlSubDelayCode.Visible = True
                        SubDelayCodeSQL = "Select 'Select Sub Delay Code' as SubDelayCode, '(No Delay Code Selected)' as ord, 0 as realord " & _
                        "union " & _
                        "select Code_Name + ' - INACTIVE', Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        "where Code_Name = '" & Replace(lblSubDelayCode.Text, "'", "''") & "' and (Code_Classification <> 'Sub_Delay_Code' or (Delay_Limiter <>'ORNOTREADY')) " & _
                        "union " & _
                        "select Code_Name, Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        "where Location = '" & Replace(lblLocation.Text, "'", "''") & "' and  Code_Classification = 'Sub_Delay_Code' and (Delay_Limiter = 'ORNOTREADY') order by realord, ord "
                        ddlSubDelayCode.DataSource = GetData(SubDelayCodeSQL)
                        ddlSubDelayCode.DataTextField = "SubDelayCode"
                        ddlSubDelayCode.DataValueField = "ord"
                        ddlSubDelayCode.DataBind()
                    Case "No Reason Documented"
                        ddlSubDelayCode.Visible = True
                        SubDelayCodeSQL = "Select 'Select Sub Delay Code' as SubDelayCode, '(No Delay Code Selected)' as ord, 0 as realord " & _
                        "union " & _
                        "select Code_Name + ' - INACTIVE', Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        "where Code_Name = '" & Replace(lblSubDelayCode.Text, "'", "''") & "' and (Code_Classification <> 'Sub_Delay_Code' or (Delay_Limiter <>'No Reason Documented')) " & _
                        "union " & _
                        "select Code_Name, Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        "where Location = '" & Replace(lblLocation.Text, "'", "''") & "' and  Code_Classification = 'Sub_Delay_Code' and (Delay_Limiter = 'No Reason Documented')  " & _
                        "union select Staff_Name , Staff_Name, 1 " & _
                        "from DWH.UD.SurgeryTO_Staff " & _
                        "where Location = '" & Replace(lblLocation.Text, "'", "''") & "' and  ClinicalSupervisor = 1 and not exists (select * from DWH.UD.SurgeryTO_Codes c where  c.Location = '" & _
                        Replace(lblLocation.Text, "'", "''") & "' and  Code_Name = Staff_Name) order by realord, ord"

                        ddlSubDelayCode.DataSource = GetData(SubDelayCodeSQL)
                        ddlSubDelayCode.DataTextField = "SubDelayCode"
                        ddlSubDelayCode.DataValueField = "ord"
                        ddlSubDelayCode.DataBind()

                    Case Else
                        'SubDelayCodeSQL = "Select 'Select Sub Delay Code' as SubDelayCode, '(No Delay Code Selected)' as ord, 0 as realord " & _
                        '"union " & _
                        '"select Code_Name + ' - INACTIVE', Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        '"where Code_Name = '" & Replace(lblSubDelayCode.Text, "'", "''") & "' and Code_Classification <> 'Sub_Delay_Code' " & _
                        '"union " & _
                        '"select Code_Name, Code_Name, 1 from  DWH.UD.SurgeryTO_Codes " & _
                        '"where Code_Classification = 'Sub_Delay_Code' order by realord, ord "
                        ddlSubDelayCode.Visible = False

                End Select


                Try
                    If SubDelay = "(No Delay Code Selected)" And ddlDelayCode.SelectedValue = "No Reason Documented" Then
                        Try
                            ddlSubDelayCode.SelectedValue = SurgeryTO_ClinicSup.SelectedItem.Text
                        Catch ex As Exception
                            ddlSubDelayCode.SelectedValue = SubDelay
                        End Try
                    Else
                        ddlSubDelayCode.SelectedValue = SubDelay
                    End If

                Catch ex As Exception

                End Try

            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If
    End Sub
    Protected Sub ddlClinicalSup_SelectedIndexChanged1(sender As Object, e As EventArgs)

        For i As Integer = 0 To gvSurgeryTO_Data.Rows.Count - 1

            Dim ddlClinSup As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Clinic_Sup"), DropDownList)

            Dim ddlDelayCode As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_DelayCode"), DropDownList)
            Dim ddlSubDelayCode As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_SubDelayCode"), DropDownList)
            Dim ddlCirculator As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_Circ1"), DropDownList)
            Dim ddlSurgTech As DropDownList = CType(gvSurgeryTO_Data.Rows(i).FindControl("ddlSurgeryTO_SurgTech"), DropDownList)

            Dim SubDelay As String = ddlSubDelayCode.SelectedValue

            Dim NewRow As Integer = 0
            Dim SubDelayCodeSQL = ""
            If ddlClinSup.SelectedValue = "265" Then
                ddlDelayCode.Visible = False
                ddlSubDelayCode.Visible = False
                ddlCirculator.Visible = False
                ddlSurgTech.Visible = False
                ddlSubDelayCode.Visible = False

            Else
                ddlDelayCode.Visible = True
                ddlSubDelayCode.Visible = True
                ddlCirculator.Visible = True
                ddlSurgTech.Visible = True
                Select Case ddlDelayCode.SelectedValue
                    Case "OR"
                        ddlSubDelayCode.Visible = True
                    Case "ORNOTREADY"
                        ddlSubDelayCode.Visible = True
                    Case "No Reason Documented"
                        ddlSubDelayCode.Visible = True
                    Case Else
                        ddlSubDelayCode.Visible = False
                End Select

            End If


        Next


    End Sub

    Private Sub txtCodeDesc_SrchSubmit_TextChanged(sender As Object, e As EventArgs) Handles txtCodeDesc_SrchSubmit.TextChanged
        PullCodes()
    End Sub

    Private Sub ddlSurgeryTOLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSurgeryTOLocation.SelectedIndexChanged
        GetAdminStatus()
        PopulateSurgeryTOGrid()
    End Sub

    Private Sub ddlAdminLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAdminLocation.SelectedIndexChanged
        If ddlManageWhat.SelectedValue = 5 Then
            btnNewCode.Text = "Add New Delay Code"
            pnlClinSups.Visible = False
            pnlCodes.Visible = True
            PullCodes()
            Exit Sub
        ElseIf ddlManageWhat.SelectedValue = 6 Then
            btnNewCode.Text = "Add New Sub Delay Code"
            pnlClinSups.Visible = False
            pnlCodes.Visible = True
            PullCodes()
            Exit Sub
        End If
        pnlCodes.Visible = False
        pnlClinSups.Visible = True
        PullStaff()
    End Sub
End Class