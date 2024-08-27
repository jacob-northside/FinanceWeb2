Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration


Imports FinanceWeb.WebFinGlobal

Public Class StarDeptMapping
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

                'test test
                PopulateOutpatientLocationDropDownList()
                PopulateFilterFacilityDropDownList()
                PopulateFilterLocationDropDownList()
                'PopulateFilterRCCDropDownList()
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

    Private Sub PopulateOutpatientLocationDropDownList()

        Try

            Dim x As String = " select outpatientlocation, outpatientlocation as display, 1 as ord from  dwh.ud.star_outpatientlocation opl where 1 = 1 "

            If rblFacility.SelectedIndex < 0 Then
            Else
                x += " and opl.facility = '" & Replace(rblFacility.SelectedValue, "'", "''") & "' "
            End If

            x += "union all select '-1', 'Select a Location',  0 order by ord, outpatientlocation"

            ddlSelectOutpatientLocation.DataSource = GetData(x)
            ddlSelectOutpatientLocation.DataTextField = "display"
            ddlSelectOutpatientLocation.DataValueField = "outpatientlocation"
            ddlSelectOutpatientLocation.DataBind()

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try

    End Sub

    Private Sub PopulateFilterFacilityDropDownList()

        Try

            Dim X As String = "select distinct facility, facility as display, 1 as ord from ( " &
                    "select x.facility from ( " &
                    "select * from  dwh.ud.star_outpatientlocation opl " &
                    "where not exists (select *  from dwh.UD.VW_PAS_TEMP pas where opl.Facility  = pas.Facility  " &
                    "and  opl.OUTPatientLocation  = pas.location  )  " &
                    "union all  " &
                    "select * from  dwh.ud.star_outpatientlocation opl where  exists (select *  from dwh.UD.VW_PAS_TEMP pas2  " &
                    "where opl.Facility  = pas2.Facility  and  opl.OUTPatientLocation  = pas2.location) ) x " &
                    "union all  " &
                    "select facility from dwh.ud.VW_PAS_TEMP) y  " &
                    "union all select '-1', 'Select a Facility', 0 as ord order by ord, facility "

            ddlFilterFacility.DataSource = GetData(X)
            ddlFilterFacility.DataTextField = "display"
            ddlFilterFacility.DataValueField = "facility"
            ddlFilterFacility.DataBind()

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try
    End Sub

    Private Sub PopulateFilterLocationDropDownList()
        Try

            Dim x As String = ""
            'Dim x As String = " select outpatientlocation, outpatientlocation as display, 1 as ord from  dwh.ud.star_outpatientlocation opl where 1 = 1 "

            'If ddlFilterFacility.SelectedIndex = 0 Then
            'Else
            '    x += " and opl.facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
            'End If

            'x += "union all select '-1', 'Select an Outpatient Location',  0 order by ord, outpatientlocation"

            'Dim x As String = ""

            If (cbShowOnlyMapped.Checked = False) Then

                'x += " select outpatientlocation, outpatientlocation as display, 1 as ord from  dwh.ud.star_outpatientlocation opl where 1 = 1 "

                'If ddlFilterFacility.SelectedIndex = 0 Then
                'Else
                '    x += " and opl.facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
                'End If

                'x += "union all select '-1', 'Select an Outpatient Location',  0 order by ord, outpatientlocation"

                x += " select outpatientlocation, outpatientlocation as display, 1 as ord from  dwh.ud.star_outpatientlocation opl where 1 = 1 "

                If ddlFilterFacility.SelectedIndex = 0 Then
                Else
                    x += " and opl.facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
                End If

                x += "union all select '-1', 'Select a Location',  0 order by ord, outpatientlocation"

            Else

                'x += " select outpatientlocation, outpatientlocation as display, 1 as ord from  dwh.ud.star_outpatientlocation opl where 1 = 1 "

                'If ddlFilterFacility.SelectedIndex = 0 Then
                'Else
                '    x += " and opl.facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
                'End If

                'x += "union all select '-1', 'Select an Outpatient Location',  0 order by ord, outpatientlocation"

                x += "select outpatientlocation, outpatientlocation as display, 1 as ord from (select * from  dwh.ud.star_outpatientlocation opl where not exists (select *  " &
                    "from dwh.UD.VW_PAS_TEMP pas where opl.Facility  = pas.Facility  and  opl.OUTPatientLocation  = pas.location  ) " &
                    "union all " &
                    "select * from  dwh.ud.star_outpatientlocation opl where  exists (select *  from dwh.UD.VW_PAS_TEMP pas2 " &
                    "where opl.Facility  = pas2.Facility  and  opl.OUTPatientLocation  = pas2.location) ) x " &
                    "where 1 = 1 "

                If ddlFilterFacility.SelectedIndex = 0 Then
                Else
                    x += " and facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
                End If

                x += " union all select '-1', 'Select a Location', 0 order by ord, outpatientlocation"

            End If

            ddlFilterLoc.DataSource = GetData(x)
            ddlFilterLoc.DataTextField = "display"
            ddlFilterLoc.DataValueField = "outpatientlocation"
            ddlFilterLoc.DataBind()

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try
    End Sub

    'Private Sub PopulateFilterRCCDropDownList()
    '    Try

    '        Dim x As String = "select '-1' as rev_center, 'Select an Admitting Dept' as display, 0 as ord " & _
    '                "union all select distinct Flag_Value, cast(Flag_Value as varchar(100)) as display, 1 as ord from  dwh.ud.Flag_Star_Dept where 1=1 " & _
    '                "and Flag_Name = 'Admitting Dept' "

    '        If ddlFilterFacility.SelectedIndex = 0 Then
    '        Else
    '            x += " and facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
    '        End If

    '        If ddlFilterLoc.SelectedIndex = 0 Then
    '        Else
    '            x += " and location = '" & Replace(ddlFilterLoc.SelectedValue, "'", "''") & "' "
    '        End If

    '        x += " order by ord, rev_center"

    '        ddlFilterRCC.DataSource = GetData(x)
    '        ddlFilterRCC.DataTextField = "display"
    '        ddlFilterRCC.DataValueField = "rev_center"
    '        ddlFilterRCC.DataBind()

    '    Catch ex As Exception
    '        Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
    '        Debug.Print(ex.ToString)
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

    '    End Try
    'End Sub

    Private Sub btnAddMapping_Click(sender As Object, e As EventArgs) Handles btnAddMapping.Click

        Try

            If rblFacility.SelectedIndex < 0 Then
                explantionlabel.Text = "Please select Facility"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If ddlSelectOutpatientLocation.SelectedValue = "-1" Then
                explantionlabel.Text = "Please select Location"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If rblFlagName.SelectedIndex < 0 Then
                explantionlabel.Text = "Please select Flag Name"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If rblFlagValue.SelectedIndex < 0 Then
                explantionlabel.Text = "Please select Flag Value"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If (txtEnterAdmittingRCC.Text = "") Then
                explantionlabel.Text = "Please enter an Admitting Dept"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            If (txtEnterMerchantDesc.Text = "") Then
                explantionlabel.Text = "Please enter a Merchant Description"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If

            Dim x As String = "select count(*) from dwh.stardeptmapping.star_dept_mapping " &
           "where Facility = '" & Replace(rblFacility.SelectedValue, "'", "''") & "' " &
           " and location = '" & Replace(ddlSelectOutpatientLocation.SelectedValue, "'", "''") & "' and active = 1"

            If GetScalar(x) > 0 Then
                explantionlabel.Text = "This Facility / Location combination is already mapped"
                ModalPopupExtender1.Show()
                OkButton.Visible = True
                ConfirmButton.Visible = False
                CancelButton.Visible = False
                Exit Sub
            End If


            explantionlabel.Text = "Please confirm the new location to map"
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

    Private Sub btnUpdateMapping_Click(sender As Object, e As EventArgs) Handles btnUpdateMapping.Click

        Try

            Dim id As UInt32 = 0

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL_Command = New SqlCommand("Select ID from dwh.stardeptmapping.Star_Dept_Mapping where active = 1 and facility = '" &
                                             Replace(rblFacility.SelectedValue, "'", "''") & "' and location = '" &
                                             Replace(ddlSelectOutpatientLocation.SelectedValue, "'", "''") & "'", conn)
                reader = SQL_Command.ExecuteReader
                While reader.Read()
                    id = reader(0).ToString
                End While
                conn.Close()
            End Using

            Dim a As String = "select count(*) from dwh.stardeptmapping.star_dept_mapping " &
           "where Facility = '" & Replace(rblFacility.SelectedValue, "'", "''") & "' " &
           " and location = '" & Replace(ddlSelectOutpatientLocation.SelectedValue, "'", "''") & "' and active = 1"

            If GetScalar(a) = 0 Then
                explantionlabel2.Text = "This facility / location has not been mapped yet. Please first select 'Map New Location'"
                ModalPopupExtender2.Show()
                OkButton2.Visible = True
                ConfirmButton2.Visible = False
                CancelButton2.Visible = False
                Exit Sub
            End If

            Dim x As String = "select count(*) as A from dwh.stardeptmapping.flag_star_dept " &
            "where ID = " & Replace(id, "'", "''") &
            " and flag_name = 'Admitting Dept' and flag_value = '" & Replace(txtEnterAdmittingRCC.Text, "'", "''") & "' and active = 1"

            Dim y As String = "select count(*) as A from dwh.stardeptmapping.flag_star_dept " &
            "where ID = " & Replace(id, "'", "''") &
            " and flag_name = 'PAS Status' and flag_value = '" & Replace(rblFlagValue.SelectedValue, "'", "''") & "' and active = 1 "

            Dim z As String = "select count(*) as A from dwh.stardeptmapping.flag_star_dept " &
            "where ID = " & Replace(id, "'", "''") &
            " and flag_name = 'Merchant Description' and flag_value = '" & Replace(txtEnterMerchantDesc.Text, "'", "''") & "' and active = 1 "

            If GetScalar(x) > 0 And GetScalar(y) > 0 And GetScalar(z) > 0 Then
                explantionlabel2.Text = "This facility / location already has the selected values"
                ModalPopupExtender2.Show()
                OkButton2.Visible = True
                ConfirmButton2.Visible = False
                CancelButton2.Visible = False
                Exit Sub
            End If


            explantionlabel2.Text = "Please confirm the existing location to update"
            ModalPopupExtender2.Show()
            OkButton2.Visible = False
            ConfirmButton2.Visible = True
            CancelButton2.Visible = True

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())

        End Try
    End Sub

    Private Sub rblFacility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblFacility.SelectedIndexChanged

        Try

            PopulateOutpatientLocationDropDownList()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlFilterFacility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterFacility.SelectedIndexChanged
        PopulateFilterLocationDropDownList()
        'PopulateFilterRCCDropDownList()
        PopulateGridView()
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click

        Try

            Dim new_id As UInt32

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL_Command = New SqlCommand("Select max(ID) from dwh.stardeptmapping.Star_Dept_Mapping", conn)
                reader = SQL_Command.ExecuteReader
                While reader.Read()
                    new_id = reader(0).ToString
                End While
                conn.Close()
            End Using

            new_id += 1

            Dim x As String = "insert into dwh.stardeptmapping.star_dept_mapping (ID, Facility, location, Active, Valid_From_Dt, Valid_To_Dt, modifiedby) values " &
                " ( " & Replace(new_id, "'", "''") & ", '" & Replace(rblFacility.SelectedValue, "'", "''") & "','" &
                Replace(ddlSelectOutpatientLocation.SelectedValue, "'", "''") & "',1,getdate(),'9999-12-31','" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

            ExecuteSql(x)

            If txtEnterAdmittingRCC.Text.Length > 0 Then

                Dim y As String = "insert into dwh.stardeptmapping.Flag_Star_Dept (ID, Flag_Name, Flag_Value, Active, Valid_From_Dt, Valid_To_dt, modifiedby) values " &
                    " ( " & Replace(new_id, "'", "''") & ", 'Admitting Dept','" &
                    Replace(txtEnterAdmittingRCC.Text, "'", "''") & "',1, getdate(), '9999-12-31', '" &
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

                ExecuteSql(y)

            End If

            If txtEnterMerchantDesc.Text.Length > 0 Then

                Dim z As String = "insert into dwh.stardeptmapping.Flag_Star_Dept (ID, Flag_Name, Flag_Value, Active, Valid_From_Dt, Valid_To_dt, modifiedby) values " &
                    " ( " & Replace(new_id, "'", "''") & ", 'Merchant Description','" &
                    Replace(txtEnterMerchantDesc.Text, "'", "''") & "',1, getdate(), '9999-12-31', '" &
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

                ExecuteSql(z)

            End If

            If rblFlagValue.SelectedIndex = -1 Then
            Else

                Dim z As String = "insert into dwh.stardeptmapping.Flag_Star_Dept (ID, Flag_Name, Flag_Value, Active, Valid_From_Dt, Valid_To_dt, modifiedby) values " &
                    " ( " & Replace(new_id, "'", "''") & ", 'PAS Status','" &
                    Replace(rblFlagValue.SelectedValue, "'", "''") & "',1, getdate(), '9999-12-31', '" &
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

                ExecuteSql(z)

            End If

            txtEnterAdmittingRCC.Text = ""
            rblFacility.SelectedIndex = -1
            rblFacility.SelectedIndex = -1
            rblFlagValue.SelectedIndex = -1
            ddlSelectOutpatientLocation.SelectedValue = "-1"
            txtEnterMerchantDesc.Text = ""

            PopulateGridView()

            explantionlabel.Text = "Location Mapped"
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

    Private Sub ConfirmButton2_Click(sender As Object, e As EventArgs) Handles ConfirmButton2.Click

        Dim id As UInt32 = 0

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            SQL_Command = New SqlCommand("Select ID from dwh.stardeptmapping.Star_Dept_Mapping where active = 1 and facility = '" &
                                         Replace(rblFacility.SelectedValue, "'", "''") & "' and location = '" &
                                         Replace(ddlSelectOutpatientLocation.SelectedValue, "'", "''") & "'", conn)
            reader = SQL_Command.ExecuteReader
            While reader.Read()
                id = reader(0).ToString
            End While
            conn.Close()
        End Using

        Dim x As String = "select count(*) as A from dwh.stardeptmapping.flag_star_dept " &
        "where ID = " & Replace(id, "'", "''") &
        " and flag_name = 'Admitting Dept' and flag_value = '" & Replace(txtEnterAdmittingRCC.Text, "'", "''") & "' and active = 1"

        Dim y As String = "select count(*) as A from dwh.stardeptmapping.flag_star_dept " &
        "where ID = " & Replace(id, "'", "''") &
        " and flag_name = 'PAS Status' and flag_value = '" & Replace(rblFlagValue.SelectedValue, "'", "''") & "' and active = 1 "

        Dim z As String = "select count(*) as A from dwh.stardeptmapping.flag_star_dept " &
        "where ID = " & Replace(id, "'", "''") &
        " and flag_name = 'Merchant Description' and flag_value = '" & Replace(txtEnterMerchantDesc.Text, "'", "''") & "' and active = 1 "

        If GetScalar(x) = 0 Then
            Dim a As String = "update dwh.stardeptmapping.Flag_Star_Dept set active = 0, Valid_To_Dt = getdate() where id =" & Replace(id, "'", "''") &
            " and flag_name = 'Admitting Dept'"

            ExecuteSql(a)

            Dim b As String = "insert into dwh.stardeptmapping.Flag_Star_Dept values(" & Replace(id, "'", "''") & ", 'Admitting Dept'," & Replace(txtEnterAdmittingRCC.Text, "'", "''") &
                ", 1, GETDATE(), '9999-12-31','" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

            ExecuteSql(b)

        End If

        If GetScalar(y) = 0 Then
            Dim c As String = "update dwh.stardeptmapping.Flag_Star_Dept set active = 0, Valid_To_Dt = getdate() where id =" & Replace(id, "'", "''") &
            " and flag_name = 'PAS Status'"

            ExecuteSql(c)

            Dim d As String = "insert into dwh.stardeptmapping.Flag_Star_Dept values(" & Replace(id, "'", "''") & ", 'PAS Status','" & Replace(rblFlagValue.SelectedValue, "'", "''") &
                "', 1, GETDATE(), '9999-12-31','" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

            ExecuteSql(d)

        End If

        If GetScalar(z) = 0 Then
            Dim f As String = "update dwh.stardeptmapping.Flag_Star_Dept set active = 0, Valid_To_Dt = getdate() where id =" & Replace(id, "'", "''") &
            " and flag_name = 'Merchant Description'"

            ExecuteSql(f)

            Dim g As String = "insert into dwh.stardeptmapping.Flag_Star_Dept values(" & Replace(id, "'", "''") & ", 'Merchant Description','" &
                Replace(txtEnterMerchantDesc.Text, "'", "''") &
                "', 1, GETDATE(), '9999-12-31','" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

            ExecuteSql(g)

        End If

        PopulateGridView()

        explantionlabel.Text = "Location Updated"
        ModalPopupExtender1.Show()
        OkButton.Visible = True
        ConfirmButton.Visible = False
        CancelButton.Visible = False

    End Sub

    Private Function ShowAllEntries()

        Dim X As String = ""

        Try

            If (cbShowOnlyMapped.Checked = False) Then
                X += "select * from (select null as ID, x.facility, x.outpatientlocation as location, null as PAS_STATUS, null as Revenue_Code,null as Merchant_Desc, 1 as active from ( " &
                    "select * from  dwh.ud.star_outpatientlocation opl " &
                    "where not exists (select *  from dwh.UD.VW_Patient_Access pas where opl.Facility  = pas.Facility " &
                    "and  opl.OUTPatientLocation  = pas.location  )) x " &
                    "union all " &
                    "select m.ID, facility, location,  d.flag_value as PAS_STATUS, dd.flag_value as Revenue_Code,ddd.Flag_Value as Merchant_Desc, m.active from dwh.stardeptmapping.Star_Dept_Mapping m " &
                    "join dwh.stardeptmapping.Flag_Star_Dept d on d.ID = m.ID and d.Flag_Name = 'PAS Status' " &
                    "join dwh.stardeptmapping.Flag_Star_Dept dd on dd.ID = m.ID and dd.Flag_Name = 'Admitting Dept' " &
                    "join dwh.StarDeptMapping.Flag_Star_Dept ddd on ddd.ID = m.ID and ddd.Flag_Name = 'Merchant Description'" &
                    "where location is not null and d.Active = 1 and dd.Active = 1 and ddd.active = 1 and m.Active = 1) z where 1 = 1 " &
                    "and active = 1 "



                If (ddlFilterFacility.SelectedValue = "-1") Then
                Else
                    X += " and facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
                End If

                If (ddlFilterLoc.SelectedValue = "-1") Then
                Else
                    X += " and location = '" & Replace(ddlFilterLoc.SelectedValue, "'", "''") & "' "
                End If

                'If (ddlFilterRCC.SelectedValue = "-1") Then
                'Else
                '    X += " and flag_value = '" & Replace(ddlFilterRCC.SelectedValue, "'", "''") & "' "
                'End If


                X += " order by ID desc"
            Else
                X += "select * from ( select null as ID, facility, outpatientlocation as location, null as PAS_Status, null as Revenue_Code, null as Merchant_Desc from  dwh.ud.star_outpatientlocation opl " &
                    "where not exists (select *  from dwh.ud.VW_Patient_Access pas where opl.Facility = pas.Facility " &
                    "and  opl.OUTPatientLocation  = pas.location  ) " &
                    "union all " &
                    "select id, opl.facility, opl.outpatientlocation as location, pas_status, revenuecostcenter as Revenue_Code, p.merchantdesc as Merchant_Description " &
                    "from dwh.ud.star_outpatientlocation opl join dwh.UD.VW_Patient_Access p on p.facility = opl.facility and p.location = opl.outpatientlocation " &
                    "where  exists (select * from dwh.UD.VW_Patient_Access pas2 where opl.Facility = pas2.Facility " &
                    "and  opl.OUTPatientLocation  = pas2.location and (pas2.revenuecostcenter is null or pas2.revenuecostcenter = 0)) " &
                    ") x where 1 = 1 "


                If (ddlFilterFacility.SelectedValue = "-1") Then
                Else
                    X += " and facility = '" & Replace(ddlFilterFacility.SelectedValue, "'", "''") & "' "
                End If

                If (ddlFilterLoc.SelectedValue = "-1") Then
                Else
                    X += " and outpatientlocation = '" & Replace(ddlFilterLoc.SelectedValue, "'", "''") & "' "
                End If

                X += " order by id"
            End If



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

        Return GetData(X).DefaultView


    End Function

    Private Sub PopulateGridView()

        gvShowResults.DataSource = ShowAllEntries()
        gvShowResults.DataBind()

    End Sub

    Private Sub gvShowResults_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShowResults.PageIndexChanging
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

            gvShowResults.PageIndex = e.NewPageIndex
            gvShowResults.DataSource = dv
            gvShowResults.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvShowResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvShowResults.SelectedIndexChanged

        Try

            rblFacility.ClearSelection()
            ddlSelectOutpatientLocation.Items.Clear()
            PopulateOutpatientLocationDropDownList()
            txtEnterAdmittingRCC.Text = ""
            rblFlagValue.ClearSelection()
            txtEnterMerchantDesc.Text = ""


            Dim Admitting_Dept As String = ""
            Dim Pas_Status As String = ""
            Dim Merchant_Desc As String = ""

            rblFacility.SelectedValue = Replace(gvShowResults.SelectedRow.Cells(2).Text, "'", "''")
            ddlSelectOutpatientLocation.SelectedValue = Replace(gvShowResults.SelectedRow.Cells(3).Text, "'", "''")

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL_Command = New SqlCommand("Select flag_value from dwh.stardeptmapping.Flag_Star_Dept where active = 1 and flag_name = 'Admitting Dept' and ID = " & Replace(gvShowResults.SelectedRow.Cells(1).Text, "'", "''"), conn)
                reader = SQL_Command.ExecuteReader
                While reader.Read()
                    Admitting_Dept = reader(0).ToString
                End While
                conn.Close()
            End Using

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL_Command = New SqlCommand("Select flag_value from dwh.stardeptmapping.Flag_Star_Dept where active = 1 and flag_name = 'Merchant Description' and ID = " & Replace(gvShowResults.SelectedRow.Cells(1).Text, "'", "''"), conn)
                reader = SQL_Command.ExecuteReader
                While reader.Read()
                    Merchant_Desc = reader(0).ToString
                End While
                conn.Close()
            End Using

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL_Command = New SqlCommand("Select flag_value from dwh.stardeptmapping.Flag_Star_Dept where active = 1 and flag_name = 'PAS Status' and ID = " & Replace(gvShowResults.SelectedRow.Cells(1).Text, "'", "''"), conn)
                reader = SQL_Command.ExecuteReader
                While reader.Read()
                    Pas_Status = reader(0).ToString
                End While
                conn.Close()
            End Using

            txtEnterAdmittingRCC.Text = Replace(Admitting_Dept, "'", "''")

            If (txtEnterAdmittingRCC.Text.Length = 1) Then
                txtEnterAdmittingRCC.Text = ""
            End If

            txtEnterMerchantDesc.Text = Replace(Merchant_Desc, "'", "''")

            If (txtEnterMerchantDesc.Text.Length = 1) Then
                txtEnterMerchantDesc.Text = ""
            End If

            rblFlagValue.SelectedValue = Replace(Pas_Status, "'", "''")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvShowResults_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvShowResults.Sorting
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
                dv.Sort = "ID asc"
                entersortdir.Text = "1"
                entersortmap.Text = "ID"
            End Try


            gvShowResults.DataSource = dv
            gvShowResults.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub cbShowOnlyMapped_CheckedChanged(sender As Object, e As EventArgs) Handles cbShowOnlyMapped.CheckedChanged

        PopulateFilterLocationDropDownList()
        PopulateGridView()
    End Sub

    Private Sub ddlFilterLoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterLoc.SelectedIndexChanged
        'PopulateFilterRCCDropDownList()
        PopulateGridView()
    End Sub

    'Private Sub ddlFilterRCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterRCC.SelectedIndexChanged
    '    PopulateGridView()
    'End Sub


End Class



