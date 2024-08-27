Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security


Imports FinanceWeb.WebFinGlobal

Public Class PFOGroupMapping
    Inherits System.Web.UI.Page
    Public Shared MappedView As New DataView
    Public Shared UnMappedView As New DataView
    Public Shared sortmap As String
    Public Shared sortunmap As String
    Public Shared mapdir As Integer
    Public Shared unmapdir As Integer
    Private Shared Admin As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                Select Case Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")
                    Case "e218173"
                        Admin = 1
                    Case "cw996788"
                        Admin = 1
                    Case "mf995052"
                        Admin = 1

                End Select

                loadgrid(Admin)

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

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
    Private Sub loadgrid(sandwich As Integer)

        Dim gridsql As String = "select *, case when Active = 1 then 'Remove' else 'Add' end as Activation  " & _
        " , convert(varchar, isnull(EffectiveFrom, '1/1/1800'), 107) as EffectiveFromDisplay " & _
        ", convert(varchar, isnull(EffectiveTo, '12/31/9999'), 107) as EffectiveToDisplay " & _
                "        from DWH.UD.MD_PFO_Group_Mapping " & _
        "        where Active = 1  or 1 = " & sandwich.ToString


        Dim da As New SqlDataAdapter
        Dim cmd As SqlCommand
        Dim ds As New DataSet

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand(gridsql, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

        End Using

        UnMappedView = ds.Tables(0).DefaultView
        gvUnMapped.DataSource = UnMappedView
        gvUnMapped.DataBind()
        gvUnMapped.ShowHeaderWhenEmpty = True


    End Sub

    Private Sub gvUnMapped_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvUnMapped.PageIndexChanging
        Try

            gvUnMapped.PageIndex = e.NewPageIndex
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvUnMapped_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvUnMapped.RowCancelingEdit
        Try
            gvUnMapped.EditIndex = -1
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvUnMapped_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUnMapped.RowCommand
        Try
            Dim ID As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If varname = "RemoveRow" Then
                Dim Sql As String = "update DWH.UD.MD_PFO_Group_Mapping set Active = 1 - Active, ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                    "', ModifiedDate = getdate() where ID = '" & Replace(ID, "'", "''") & "'"
                ExecuteSql(Sql)
                loadgrid(Admin)

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub gvUnMapped_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvUnMapped.RowCreated
        Try
            If Admin = 0 Then
                e.Row.Cells(5).CssClass = "hidden"
                e.Row.Cells(6).CssClass = "hidden"
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvUnMapped_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvUnMapped.RowEditing
        Try

            gvUnMapped.EditIndex = e.NewEditIndex
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()

            Dim txtName As TextBox = gvUnMapped.Rows(e.NewEditIndex).FindControl("txtPFO_Group")
            Dim lblName As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblPFO_Group")

            Dim lblEffFrom As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblEffectiveFrom")
            Dim lblEffTo As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblEffectiveTo")
            Dim pnlEffFrom As Panel = gvUnMapped.Rows(e.NewEditIndex).FindControl("pnlEffectiveFrom")
            Dim pnlEffTo As Panel = gvUnMapped.Rows(e.NewEditIndex).FindControl("pnlEffectiveTo")
            txtName.Visible = True
            lblName.Visible = False
            pnlEffFrom.Visible = True
            pnlEffTo.Visible = True
            lblEffFrom.Visible = False
            lblEffTo.Visible = False


            For Each canoe As GridViewRow In gvUnMapped.Rows
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

    'Private Sub gvUnMapped_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnMapped.SelectedIndexChanged
    '    Try
    '        gvUnMapped.EditIndex = gvUnMapped.SelectedIndex
    '        'For Each canoe As GridViewRow In gvUnMapped.Rows
    '        '    If canoe.RowIndex = gvUnMapped.SelectedIndex Then
    '        '        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
    '        '        Dim txtName As TextBox = gvUnMapped.Rows(canoe.RowIndex).FindControl("txtPFO_Group")
    '        '        Dim lblName As Label = gvUnMapped.Rows(canoe.RowIndex).FindControl("lblPFO_Group")

    '        '        Dim pnlEffFrom As Panel = gvUnMapped.Rows(canoe.RowIndex).FindControl("pnlEffectiveFrom")
    '        '        Dim pnlEffTo As Panel = gvUnMapped.Rows(canoe.RowIndex).FindControl("pnlEffectiveTo")
    '        '        txtName.Visible = True
    '        '        lblName.Visible = False
    '        '        pnlEffFrom.Visible = True
    '        '        pnlEffTo.Visible = True
    '        '    Else
    '        '        If canoe.RowIndex Mod 2 = 0 Then
    '        '            canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
    '        '        Else
    '        '            canoe.BackColor = System.Drawing.Color.White
    '        '        End If
    '        '        Dim txtName As TextBox = gvUnMapped.Rows(canoe.RowIndex).FindControl("txtPFO_Group")
    '        '        Dim lblName As Label = gvUnMapped.Rows(canoe.RowIndex).FindControl("lblPFO_Group")

    '        '        Dim pnlEffFrom As Panel = gvUnMapped.Rows(canoe.RowIndex).FindControl("pnlEffectiveFrom")
    '        '        Dim pnlEffTo As Panel = gvUnMapped.Rows(canoe.RowIndex).FindControl("pnlEffectiveTo")
    '        '        txtName.Visible = False
    '        '        lblName.Visible = True
    '        '        pnlEffFrom.Visible = False
    '        '        pnlEffTo.Visible = False
    '        '    End If

    '        'Next

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub btnAddLine_Click(sender As Object, e As EventArgs) Handles btnAddLine.Click

        Try

            Dim CheckSql As String = "select count(*) from DWH.UD.MD_PFO_Group_Mapping " & _
            "where IDGroup = '" & Trim(Replace(txtIDGroup.Text, "'", "''")) & "' and (CostCenter is null and '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & _
            "' = '' or CostCenter = '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & "') " & _
            "and Active = 1 and getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999')"

            If GetScalar(CheckSql) > 0 Then
                If Len(Trim(Replace(txtCostCenter.Text, "'", "''"))) > 0 Then
                    If Admin = 1 Then
                        explantionlabel.Text = "Warning: There is already an active row for this IDGroup and Cost Center <br> Add row anyways? "
                        ConfirmButton.Visible = True
                        OkButton.Visible = False
                        CancelButton.Visible = True
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    explantionlabel.Text = "There is already a row for this IDGroup and Cost Center.<br>If you need an additional row, please contact an Admin."
                    ConfirmButton.Visible = False
                    OkButton.Visible = False
                    CancelButton.Visible = True
                    ModalPopupExtender1.Show()
                    Exit Sub
                Else
                    If Admin = 1 Then
                        explantionlabel.Text = "Warning: There is already a default active row for this IDGroup <br> Add row anyways? "
                        ConfirmButton.Visible = True
                        OkButton.Visible = False
                        CancelButton.Visible = True
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    explantionlabel.Text = "There is already a default row for this IDGroup.<br>If you need an additional row, please contact an Admin."
                    ConfirmButton.Visible = False
                    OkButton.Visible = False
                    CancelButton.Visible = True
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

            End If

            Dim InsertSql As String = "Insert into DWH.UD.MD_PFO_Group_Mapping " & _
            "Select a.*, 1, dateadd(day, 1, EffectiveTo), null, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate() from " & _
            "(Select '" & Trim(Replace(txtIDGroup.Text, "'", "''")) & "' as IDGroup, case when '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & _
            "' = '' then null else '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & "' end as CostCenter, '" & Trim(Replace(txtPFO_Group.Text, "'", "''")) & _
            "' as PFO_Group) a  " & _
            "left join (select *, ROW_NUMBER() over (partition by IDGroup, CostCenter order by EffectiveFrom, EffectiveTo) as RN " & _
            "from DWH.UD.MD_PFO_Group_Mapping " & _
            "where Active = 1 and IDGroup = '" & Trim(Replace(txtIDGroup.Text, "'", "''")) & "' and (CostCenter is null and '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & _
            "' = '' or CostCenter = '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & "') " & _
            ") x on  RN = 1 "

            ExecuteSql(InsertSql)

            txtIDGroup.Text = ""
            txtCostCenter.Text = ""
            txtPFO_Group.Text = ""
            explantionlabel.Text = "Row added successfully."
            ConfirmButton.Visible = False
            OkButton.Visible = True
            CancelButton.Visible = False
            loadgrid(Admin)
            ModalPopupExtender1.Show()

        Catch ex As Exception
            explantionlabel.Text = "Error updating row; Please report to Admin"
            ModalPopupExtender1.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvUnMapped_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvUnMapped.RowUpdating
        Try
            Dim depid As String = gvUnMapped.DataKeys(e.RowIndex).Value.ToString

            Dim txtDept As TextBox = gvUnMapped.Rows(e.RowIndex).FindControl("txtPFO_Group")
            Dim lblDept As Label = gvUnMapped.Rows(e.RowIndex).FindControl("lblStaffName")

            Dim Sql As String = "update DWH.UD.MD_PFO_Group_Mapping " & _
                "set PFO_Group = '" & Replace(txtDept.Text, "'", "''") & "', ModifiedBy = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' "

            If Admin = 1 Then
                Dim txtEffFrom As TextBox = gvUnMapped.Rows(e.RowIndex).FindControl("txtEffectiveFrom")
                Dim txtEffTo As TextBox = gvUnMapped.Rows(e.RowIndex).FindControl("txtEffectiveTo")
                Sql += ", EffectiveFrom = case when '" & Replace(txtEffFrom.Text, "'", "''") & "' = '1/1/1800' then null when '" & Trim(Replace(txtEffFrom.Text, "'", "''")) & "' = '' " & _
                    "then null else '" & Replace(txtEffFrom.Text, "'", "''") & "' end " & _
                    ", EffectiveTo = case when '" & Replace(txtEffTo.Text, "'", "''") & "' = '12/31/9999' then null when '" & Trim(Replace(txtEffTo.Text, "'", "''")) & _
                    "' = '' then null else '" & Replace(txtEffTo.Text, "'", "''") & "' end "
            End If

            Sql += ", ModifiedDate = getdate() " & _
                "where ID = '" & Replace(depid, "'", "''") & "'"

            ExecuteSql(Sql)

            gvUnMapped.EditIndex = -1
            loadgrid(Admin)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    Private Sub gvUnMapped_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvUnMapped.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = UnMappedView

        sorts = e.SortExpression

        If e.SortExpression = sortunmap Then

            If unmapdir = 1 Then
                dv.Sort = sorts + " " + "desc"
                unmapdir = 0
            Else
                dv.Sort = sorts + " " + "asc"
                unmapdir = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            unmapdir = 1
            sortunmap = e.SortExpression
        End If

        gvUnMapped.DataSource = dv
        gvUnMapped.DataBind()
    End Sub


    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click

        ModalPopupExtender1.Hide()

    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click
        Dim InsertSql As String = "Insert into DWH.UD.MD_PFO_Group_Mapping " & _
        "Select '" & Trim(Replace(txtIDGroup.Text, "'", "''")) & "', case when '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & _
        "' = '' then null else '" & Trim(Replace(txtCostCenter.Text, "'", "''")) & "' end, '" & Trim(Replace(txtPFO_Group.Text, "'", "''")) & _
        ", 1, null, null, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate() "

        ExecuteSql(InsertSql)

        explantionlabel.Text = "Row added successfully."
        ConfirmButton.Visible = False
        OkButton.Visible = True
        CancelButton.Visible = False
        loadgrid(Admin)
        ModalPopupExtender1.Show()
    End Sub
End Class