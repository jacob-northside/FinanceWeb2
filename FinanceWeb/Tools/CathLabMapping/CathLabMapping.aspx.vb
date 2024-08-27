Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration


Imports FinanceWeb.WebFinGlobal

Public Class CathLabMapping
    Inherits System.Web.UI.Page
    Public Shared MappedView As New DataView
    Public Shared UnMappedView As New DataView
    Public Shared sortmap As String
    Public Shared sortunmap As String
    Public Shared mapdir As Integer
    Public Shared unmapdir As Integer
    Private Shared Admin As Integer = 0

    Dim reader As SqlDataReader
    Dim SQL_Command, Insert_Command As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                'Select Case Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")
                '    Case "e218173"
                '        Admin = 1
                '    Case "cw996788"
                '        Admin = 1
                '    Case "mf995052"
                '        Admin = 1 

                'End Select

                PopulateFilterGroupNameDropDownList()
                PopulateCathEventView()
                PopulateEmployeeView()


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

    Private Sub btnUpdateEvent_Click(sender As Object, e As EventArgs) Handles btnUpdateEvent.Click

        Try

            If lblEventID.Text = "" Then
                explantionlabel.Text = "Please Enter a Proc Case Event ID"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If (lblProcID.Text = "") Then
                explantionlabel.Text = "Please Enter a Proc ID"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If (txtEnterStartDate.Text = "") Then
                explantionlabel.Text = "Please Enter a Case Start Date"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If (txtEnterEndDate.Text = "") Then
                explantionlabel.Text = "Please Enter a Case End Date"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            Dim x As String = "select count(*) from dwh.cathlab.cath_case_event_corrections " & _
           "where proc_case_event_id = '" & Replace(lblEventID.Text, "'", "''") & "' " & _
           " and procid = '" & Replace(lblProcID.Text, "'", "''") & "'" & _
            " and module = '" & Replace(lblModule.Text, "'", "''") & "' " & _
            " and location = '" & Replace(rblLocation.SelectedValue, "'", "''") & "' " & _
            " and case_start = '" & Replace(txtEnterStartDate.Text, "'", "''") & "' " & _
            " and patient_on_table = '" & Replace(txtEnterTableDate.Text, "'", "''") & "' " & _
            " and case_end = '" & Replace(txtEnterEndDate.Text, "'", "''") & "' "

            If GetScalar(x) > 0 Then
                explantionlabel.Text = "This Proc Case Event ID / Proc ID / Module already has the selected Start/End/Table date"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            explantionlabel.Text = "Please confirm the new Start/End/Table dates to map"
            ModalPopupExtender1.Show()
            OkButton.Visible = False
            ConfirmButton.Visible = True
            CancelButton.Visible = True

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub

    Private Sub btnFilterTable_Click(sender As Object, e As EventArgs) Handles btnFilterTable.Click
        PopulateCathEventView()
    End Sub

    Private Sub btnResetTable_Click(sender As Object, e As EventArgs) Handles btnResetTable.Click
        txtFilterCaseDuration.Text = ""
        txtFilterCaseEventID.Text = ""
        txtFilterTableToStartDuration.Text = ""
        txtFilterFirstName.Text = ""
        txtFilterLastName.Text = ""
        rblLocation.ClearSelection()
        txtProcDate.Text = ""

        PopulateCathEventView()
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click

        Try
  
            Dim y As String = "update dwh.cathlab.cath_case_event_corrections set active = 0 where proc_case_event_id = '" & Replace(lblEventID.Text, "'", "''") & _
                "' and procid = '" & Replace(lblProcID.Text, "'", "''") & "' and module = '" & Replace(lblModule.Text, "'", "''") & "'"

            ExecuteSql(y)

            Dim x As String = "insert into dwh.cathlab.cath_case_event_corrections (proc_case_event_id, procid, module, patient_firstname, patient_lastname, case_start, case_end, patient_on_table, modified_date, location, procedure_date, modified_by, active) values " & _
                " ( '" & Replace(lblEventID.Text, "'", "''") & "', '" & Replace(lblProcID.Text, "'", "''") & "' ," & _
                " '" & Replace(lblModule.Text, "'", "''") & "','" & Replace(lblFirstName.Text, "'", "''") & "', '" & Replace(lblLastName.Text, "'", "''") & _
                "', '" & Replace(txtEnterStartDate.Text, "'", "''") & "' ," & _
                " '" & Replace(txtEnterEndDate.Text, "'", "''") & "', '" & Replace(txtEnterTableDate.Text, "'", "''") & "', getdate(), '" & _
                Replace(lblLocation.Text, "'", "''") & "', '" & Replace(lblProcDate.Text, "'", "''") & "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'" & _
                ",1)"


            ExecuteSql(x)

            lblEventID.Text = "Select a row from the table"
            lblProcID.Text = "Select a row from the table"
            txtEnterStartDate.Text = ""
            txtEnterEndDate.Text = ""
            txtEnterTableDate.Text = ""
            lblModule.Text = "Select a row from the table"
            lblFirstName.Text = "Select a row from the table"
            lblLastName.Text = "Select a row from the table"
            lblLocation.Text = "Select a row from the table"
            lblProcDate.Text = "Select a row from the table"

            PopulateCathEventView()

            explantionlabel.Text = "Start/End Dates Mapped"
            ModalPopupExtender1.Show()
            OkButton.Visible = True
            ConfirmButton.Visible = False
            CancelButton.Visible = False

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub

    Private Function ShowEventEntries()

        Dim X As String = ""

        Try

            X += "select * from (select Proc_case_event_id, procid, module,patient_firstname, patient_lastname, case_start, case_end, patient_on_table, case_duration, " & _
    "on_table_to_case_Start, location, procedure_date from dwh.cathlab.Cath_Case_Event_Sync s " & _
    "where not exists (select Proc_case_event_id, procid, module from dwh.cathlab.cath_case_event_corrections c " & _
    "where c.module = s.module and c.procid = s.procid and c.Proc_case_event_id = s.Proc_case_event_id) " & _
    "union all select Proc_case_event_id, procid, module,patient_firstname, patient_lastname, case_start, case_end, patient_on_table, " & _
    "DATEDIFF(minute, case_start, case_end), " & _
    "abs(DateDiff(Minute, case_start, patient_on_table)), location, procedure_date " & _
    "from (select Proc_case_event_id,  procid, module,patient_firstname, patient_lastname, case_start, location, case_end, patient_on_table, procedure_date, " & _
    "DATEDIFF(minute, case_start, case_end) as A, " & _
    "abs(DateDiff(Minute, case_start, patient_on_table)) as B, " & _
    "ROW_NUMBER() over (partition by proc_case_event_id, procid, module order by modified_date desc) as rn " & _
    "from dwh.cathlab.cath_case_event_corrections) i where rn=1 ) x where 1 = 1 "

            If txtFilterCaseEventID.Text.Length = 0 Then
            Else
                X += " and x.proc_case_event_id = '" & Replace(txtFilterCaseEventID.Text, "'", "''") & "' "
            End If

            If txtFilterCaseDuration.Text.Length = 0 Then
            Else
                X += " and x.case_duration >= '" & Replace(txtFilterCaseDuration.Text, "'", "''") & "' "
            End If

            If txtFilterTableToStartDuration.Text.Length = 0 Then
            Else
                X += " and x.on_table_to_case_Start >= '" & Replace(txtFilterTableToStartDuration.Text, "'", "''") & "' "
            End If

            If txtFilterFirstName.Text.Length = 0 Then
            Else
                X += " and upper(x.patient_firstname) like upper('" & Replace(txtFilterFirstName.Text, "'", "''") & "%') "
            End If

            If txtFilterLastName.Text.Length = 0 Then
            Else
                X += " and upper(x.patient_lastname) like upper('" & Replace(txtFilterLastName.Text, "'", "''") & "%') "
            End If

            If rblLocation.SelectedValue.Length = 0 Then
            Else
                X += " and x.location = '" & Replace(rblLocation.SelectedValue, "'", "''") & "' "
            End If

            If txtProcDate.Text.Length = 0 Then
            Else
                X += " and x.procedure_date >= '" & Replace(txtProcDate.Text, "'", "''") & "' "
            End If

            X += " order by x.proc_case_event_id "


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

        Return GetData(X).DefaultView


    End Function

    Private Sub PopulateCathEventView()

        gvShowCathEvent.DataSource = ShowEventEntries()
        gvShowCathEvent.DataBind()

    End Sub

    Private Sub gvShowCathEvent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvShowCathEvent.SelectedIndexChanged

        Try

            lblEventID.Text = "Select a row from the table"
            lblProcID.Text = "Select a row from the table"
            txtEnterEndDate.Text = ""
            txtEnterStartDate.Text = ""
            txtEnterTableDate.Text = ""
            lblModule.Text = ""
            lblLocation.Text = ""
            lblProcDate.Text = ""

            lblEventID.Text = Replace(gvShowCathEvent.SelectedRow.Cells(1).Text, "'", "''")
            lblProcID.Text = Replace(gvShowCathEvent.SelectedRow.Cells(2).Text, "'", "''")
            txtEnterEndDate.Text = Replace(gvShowCathEvent.SelectedRow.Cells(8).Text, "'", "''")
            txtEnterStartDate.Text = Replace(gvShowCathEvent.SelectedRow.Cells(7).Text, "'", "''")
            txtEnterTableDate.Text = Replace(gvShowCathEvent.SelectedRow.Cells(9).Text, "'", "''")
            lblModule.Text = Replace(gvShowCathEvent.SelectedRow.Cells(3).Text, "'", "''")
            lblFirstName.Text = Replace(gvShowCathEvent.SelectedRow.Cells(5).Text, "'", "''")
            lblLastName.Text = Replace(gvShowCathEvent.SelectedRow.Cells(6).Text, "'", "''")
            lblLocation.Text = Replace(gvShowCathEvent.SelectedRow.Cells(4).Text, "'", "''")
            lblProcDate.Text = Replace(gvShowCathEvent.SelectedRow.Cells(12).Text, "'", "''")

            If txtEnterEndDate.Text = "&nbsp;" Then
                txtEnterEndDate.Text = ""
            End If

            If txtEnterStartDate.Text = "&nbsp;" Then
                txtEnterStartDate.Text = ""
            End If

            If txtEnterTableDate.Text = "&nbsp;" Then
                txtEnterTableDate.Text = ""
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvShowCathEvent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShowCathEvent.PageIndexChanging
        Try

            Dim dv As DataView = ShowEventEntries()

            If entersortdir.Text <> "" Then

                If entersortdir.Text = "1" Then
                    dv.Sort = entersortmap.Text + " " + "asc"
                    entersortdir.Text = "1"

                Else
                    dv.Sort = entersortmap.Text + " " + "desc"
                    entersortdir.Text = 0

                End If
            End If

            gvShowCathEvent.PageIndex = e.NewPageIndex
            gvShowCathEvent.DataSource = dv
            gvShowCathEvent.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvShowCathEvent_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvShowCathEvent.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = ShowEventEntries()

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
                dv.Sort = "ID asc"
                entersortdir.Text = "1"
                entersortmap.Text = "ID"
            End Try


            gvShowCathEvent.DataSource = dv
            gvShowCathEvent.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PopulateFilterGroupNameDropDownList()

        Try

            Dim X As String = ""

            If (cbShowOnlyMapped.Checked = False) Then
                X += "select distinct i.groupname, groupname as display, 1 as ord from dwh.cathlab.PHYSICIAN_GROUP i " & _
                                "union all select '-1', 'Select a Group Name (Optional)', 0 as ord " & _
                                "order by ord, groupname "

                ddlSelectGroupName.DataSource = GetData(X)
                ddlSelectGroupName.DataTextField = "display"
                ddlSelectGroupName.DataValueField = "groupname"
                ddlSelectGroupName.DataBind()
            Else
                X += "select distinct 'Unavailable while below box is checked' as groupname, 'Unavailable while below box is checked' display " & _
                ", 1 as ord from dwh.cathlab.PHYSICIAN_GROUP i "

                ddlSelectGroupName.DataSource = GetData(X)
                ddlSelectGroupName.DataTextField = "display"
                ddlSelectGroupName.DataValueField = "groupname"
                ddlSelectGroupName.DataBind()
            End If


        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub

    Private Sub btnMapGroup_Click(sender As Object, e As EventArgs) Handles btnMapGroup.Click

        Try

            If lblEmployeeID.Text = "Select a row from the table" Then
                explantionlabel3.Text = "Please select a row from the table"
                ModalPopupExtender3.Show()
                OkButton3.Visible = True
                ConfirmButton3.Visible = False
                CancelButton3.Visible = False
                Exit Sub
            End If

            If (txtEnterGroupName.Text = "") Then
                explantionlabel3.Text = "Please enter a Group Name"
                ModalPopupExtender3.Show()
                OkButton3.Visible = True
                ConfirmButton3.Visible = False
                CancelButton3.Visible = False
                Exit Sub
            End If

            Dim x As String = "select count(*) from dwh.cathlab.PHYSICIAN_GROUP " & _
           "where empid = '" & Replace(lblEmployeeID.Text, "'", "''") & "' " & _
           " and groupname = '" & Replace(txtEnterGroupName.Text, "'", "''") & "'"

            If GetScalar(x) > 0 Then
                explantionlabel3.Text = "This Employee ID / Group Name combination is already mapped"
                ModalPopupExtender3.Show()
                OkButton3.Visible = True
                ConfirmButton3.Visible = False
                CancelButton3.Visible = False
                Exit Sub
            End If


            explantionlabel3.Text = "Please confirm the new Employee ID to map"
            ModalPopupExtender3.Show()
            OkButton3.Visible = False
            ConfirmButton3.Visible = True
            CancelButton3.Visible = True

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub

    Private Sub ConfirmButton3_Click(sender As Object, e As EventArgs) Handles ConfirmButton3.Click

        Try

            Dim x As String = "insert into dwh.cathlab.PHYSICIAN_GROUP (empid, groupname, npi, display_name, title, primary_location) values " & _
                " ( '" & Replace(lblEmployeeID.Text, "'", "''") & "', '" & Replace(txtEnterGroupName.Text, "'", "''") & "'" & _
                ", '" & Replace(lblNPI.Text, "'", "''") & "', '" & Replace(lblDisplayName.Text, "'", "''") & "','MD','" & Replace(lblPrimaryLocation.Text, "'", "''") & "'" & _
                ")"

            ExecuteSql(x)

            txtEnterGroupName.Text = ""
            lblEmployeeID.Text = "Select a row from the table"
            lblDisplayName.Text = "Select a row from the table"
            lblNPI.Text = "Select a row from the table"
            lblPrimaryLocation.Text = "Select a row from the table"

            'PopulateEmployeeIDDropDownList()
            PopulateFilterGroupNameDropDownList()
            PopulateEmployeeView()

            explantionlabel3.Text = "Employee ID Mapped"
            ModalPopupExtender3.Show()
            OkButton3.Visible = True
            ConfirmButton3.Visible = False
            CancelButton3.Visible = False

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub

    Private Function ShowEmployeeEntries()

        Dim X As String = ""

        Try

            If (cbShowOnlyMapped.Checked = False) Then
                X += "select * from (select empid, npi, display_name, title, primary_location, null as groupname from ( " & _
                       "select * from  dwh.cathlab.EMPLOYEE_INFORMATION ei " & _
                        "where not exists (select *  from dwh.cathlab.PHYSICIAN_GROUP pg where pg.EMPID = ei.EMPID ) " & _
                        ") x union all select empid, npi, display_name, title, primary_location, groupname from dwh.cathlab.PHYSICIAN_GROUP) z where 1 = 1 and title = 'MD' "



                If (ddlSelectGroupName.SelectedValue = "-1") Then
                Else
                    X += " and groupname = '" & Replace(ddlSelectGroupName.SelectedValue, "'", "''") & "' "
                End If

                X += " order by empid "
            Else
                X += "select empid, npi, display_name, title, primary_location, null as groupname from ( " & _
                    "select * from  dwh.cathlab.EMPLOYEE_INFORMATION ei " & _
                    "where not exists (select *  from dwh.cathlab.PHYSICIAN_GROUP pg where pg.EMPID = ei.EMPID ) ) x where title = 'MD'  "

                X += " order by empid"
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

        Return GetData(X).DefaultView


    End Function

    Private Sub PopulateEmployeeView()

        gvShowEmployee.DataSource = ShowEmployeeEntries()
        gvShowEmployee.DataBind()

    End Sub

    Private Sub gvShowEmployee_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShowEmployee.PageIndexChanging
        Try

            Dim dv As DataView = ShowEmployeeEntries()

            If entersortdir.Text <> "" Then

                If entersortdir.Text = "1" Then
                    dv.Sort = entersortmap.Text + " " + "asc"
                    entersortdir.Text = "1"

                Else
                    dv.Sort = entersortmap.Text + " " + "desc"
                    entersortdir.Text = 0

                End If
            End If

            gvShowEmployee.PageIndex = e.NewPageIndex
            gvShowEmployee.DataSource = dv
            gvShowEmployee.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvShowEmployee_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvShowEmployee.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = ShowEmployeeEntries()

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
                dv.Sort = "ID asc"
                entersortdir.Text = "1"
                entersortmap.Text = "ID"
            End Try


            gvShowEmployee.DataSource = dv
            gvShowEmployee.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvShowEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvShowEmployee.SelectedIndexChanged

        Try

            lblEmployeeID.Text = "Select a row from the table"
            txtEnterGroupName.Text = ""
            lblDisplayName.Text = "Select a row from the table"
            lblNPI.Text = "Select a row from the table"
            lblPrimaryLocation.Text = "Select a row from the table"

            lblEmployeeID.Text = Replace(gvShowEmployee.SelectedRow.Cells(1).Text, "'", "''")
            lblNPI.Text = Replace(gvShowEmployee.SelectedRow.Cells(2).Text, "'", "''")
            lblDisplayName.Text = Replace(gvShowEmployee.SelectedRow.Cells(3).Text, "'", "''")
            lblPrimaryLocation.Text = Replace(gvShowEmployee.SelectedRow.Cells(4).Text, "'", "''")
            txtEnterGroupName.Text = Replace(gvShowEmployee.SelectedRow.Cells(5).Text, "'", "''")
            txtEnterGroupName.Text = Replace(txtEnterGroupName.Text, "&nbsp;", "")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub cbShowOnlyMapped_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowOnlyMapped.CheckedChanged

        PopulateFilterGroupNameDropDownList()
        PopulateEmployeeView()
    End Sub

    Private Sub ddlSelectGroupName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectGroupName.SelectedIndexChanged

        PopulateEmployeeView()
    End Sub

End Class