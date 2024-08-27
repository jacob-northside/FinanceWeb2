Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

Public Class MD_POS_Desc_Mapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then

            Else
                PopulateIDGroupDDL()
                PopulateVolTypeDDL()
                PopulateVolTypeStatuses()
                PopulategvUnmapped()
                PopulateVolTypesAdmin()
            End If

        Catch ex As Exception

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

                cmd.CommandTimeout = 64000
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

    Protected Sub ddlAssignedVolType_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUnMapped.Rows.Count - 1

            Dim lblAssignedVolType As Label = CType(gvUnMapped.Rows(i).FindControl("lblAssignedVolType"), Label)
            Dim ddlAssignedVolType As DropDownList = CType(gvUnMapped.Rows(i).FindControl("ddlAssignedVolType"), DropDownList)

            Dim DupeKey As String = Replace(gvUnMapped.DataKeys(i).Value.ToString, "'", "''")

            Dim NewRow As Integer = 0
            If ddlAssignedVolType.SelectedValue <> lblAssignedVolType.Text Then
                UpdatesSql += "update DWH.md.udMD_POSDesc_Mapping " & _
                "set VolType = (select VolType from DWH.ud.udMD_POS_VolTypes where InactivatedDate is null and VolID = '" & ddlAssignedVolType.SelectedValue & "') " & _
                ", DateModified = GetDate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                "where IDGroup + '~' + PosDesc = '" & Replace(gvUnMapped.DataKeys(i).Value.ToString, "'", "''") & "' "

            End If

        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If
    End Sub

    Private Function gvVolType_View()
        Dim SearchString As String = ""
        Dim Joinstring As String = ""
        Dim TempTableName As String = "#TempFound_" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "") & "_" & Now().ToString("yyyy_MM_dd_HH_mm_ss")

        If Len(Trim(txtVolTypePOSDesc.Text)) > 0 Then
            SearchString = "declare @SearchWord varchar(max) = '" & Replace(Trim(txtVolTypePOSDesc.Text), "'", "''") & "'  " & _
                   "declare @MinorWord varchar(max) = '' " & _
                   "declare @AreaSearch varchar(max) = '' " & _
                   " " & _
                   "select distinct POSDesc as Searchy , udMD_POSDesc_MappingID, null as Flagged  " & _
                   "into " & TempTableName & " " & _
                   "from DWH.md.udMD_POSDesc_Mapping with (nolock)  " & _
                   " " & _
                   " " & _
                   "while len(@SearchWord) > 0 " & _
                   "begin " & _
                   "if (Charindex(' ', @SearchWord) > Patindex('%:(%)%', @SearchWord) or Charindex(' ', @SearchWord) = 0) and Patindex('%:(%)%', @SearchWord) > 0  " & _
                   "begin " & _
                   "set @MinorWord =  substring(@SearchWord, Charindex(':(', @SearchWord) + 2, Charindex(')', @SearchWord) - Charindex(':(', @SearchWord) - 2) " & _
                   "set @SearchWord = SUBSTRING(@SearchWord,  Charindex(')', @SearchWord) + 1, len(@Searchword)) " & _
                   "end " & _
                   "else " & _
                   "begin " & _
                   "set @MinorWord = substring(@SearchWord, 1, case when charindex(' ', @SearchWord, 0) = 0 then len(@SearchWord) else charindex(' ', @SearchWord, 0) - 1 end)   " & _
                   "set @SearchWord = case when charindex(' ', @SearchWord, 0) = 0 then '' else substring(@SearchWord, charindex(' ', @SearchWord, 0) + 1, len(@SearchWord)) end " & _
                   "end " & _
                   " " & _
                   "update " & TempTableName & " " & _
                   "set Flagged = isnull(Flagged, 0) + 1 " & _
                   "where Searchy like '%'+@MinorWord+'%' " & _
                   " " & _
                   "end "


            Joinstring += "Join (Select distinct udMD_POSDesc_MappingID, '' as SearchResult " & _
                    "From " & TempTableName & " ST2 " & _
                    "where Flagged > 0 ) z on z.udMD_POSDesc_MappingID = ST2.udMD_POSDesc_MappingID "


        End If

        Dim Dupes As String = "''"
        For Each checkb As ListItem In cblVolTypeStatuses.Items
            If checkb.Selected Then
                Dupes += ", '" & Replace(checkb.Value, "'", "''") & "' "
            End If
        Next

        Dim x As String = SearchString & "SELECT distinct ST2.IDGroup + '~' + ST2.PosDesc as poskey, ST2.IDGroup, ST2.POSDesc, isnull(cnt, 0) cnt, case when cnt > 1 then 'Discrepancy' when isnull(cnt, 0) = 0 then 'Unmapped' else 'Mapped' end as Explanation , " & _
        "    SUBSTRING( " & _
        "        ( " & _
        "            SELECT ','+ST1.VolType  AS [text()] " & _
        "            FROM (select distinct IDGroup, POSDesc, VolType from DWH.md.udMD_POSDesc_Mapping) ST1 " & _
        "            WHERE ST1.IDGroup = ST2.IDGroup and ST1.POSDesc = ST2.POSDesc " & _
        "			and st1.VolType <> '' " & _
        "            ORDER BY ST1.VolType " & _
        "            FOR XML PATH ('') " & _
        "        ), 2, 1000) [VolTypes], isnull(Vt.VolID, -1) as VolID " & _
        "FROM DWH.md.udMD_POSDesc_Mapping ST2 " & Joinstring & _
        "left join (Select IDGroup, POSDesc, count(distinct VolType) cnt from DWH.md.udMD_POSDesc_Mapping where VolType <> '' group by IDGroup, POSDesc) x  " & _
            "	on ST2.IDGroup = x.IDGroup and st2.POSDesc = x.POSDesc " & _
            "left join DWH.ud.udMD_POS_VolTypes vt on cnt = 1 and vt.VolTYpe = st2.VolType " & _
            "where isnull(ST2.POSDesc_Column, '') <> 'NullDefault' " & _
            " and case when cnt > 1 then 'Discrepancy' when isnull(cnt, 0) = 0 then 'Unmapped' else 'Mapped' end in (" & Dupes & ") "

        If ddlVoltypeIDGroup.SelectedIndex > 0 Then
            x += " and ST2.IDGroup = '" & Replace(ddlVoltypeIDGroup.SelectedValue, "'", "''") & "' "
        End If

        x += "order by Explanation, poskey "
        Return GetData(x).DefaultView
    End Function

    Private Sub PopulategvUnmapped()

        gvUnMapped.DataSource = gvVolType_View()
        gvUnMapped.DataBind()

    End Sub

    Private Sub PopulateVolTypeDDL()

        Dim x As String = "select VolType, VolID, 1 as ord From  DWH.ud.udMD_POS_VolTypes " & _
            "where InactivatedDate is null " & _
            "union select '(Select VolType)', -1, 0 " & _
            "order by ord, VolType "

        Dim y As DataView = GetData(x).DefaultView

        ddlBulkUpdateVolTypes.DataSource = y
        ddlBulkUpdateVolTypes.DataTextField = "VolType"
        ddlBulkUpdateVolTypes.DataValueField = "VolID"
        ddlBulkUpdateVolTypes.DataBind()


    End Sub

    Private Sub PopulateIDGroupDDL()

        Dim x As String = "select distinct IDGroup, 1 ord from DWH.md.udMD_POSDesc_Mapping union select '(Select All)', -1 order by ord"

        Dim y As DataView = GetData(x).DefaultView

        ddlVoltypeIDGroup.DataSource = y
        ddlVoltypeIDGroup.DataTextField = "IDGroup"
        ddlVoltypeIDGroup.DataValueField = "IDGroup"
        ddlVoltypeIDGroup.DataBind()


    End Sub

    Private Sub PopulateVolTypeStatuses()

        Dim dv As DataView = GetData("select 'Discrepancy' as Explanation, 1 as Display union select 'Unmapped', 1 union select 'Mapped', 0  ").DefaultView

        cblVolTypeStatuses.DataSource = dv
        cblVolTypeStatuses.DataTextField = "Explanation"
        cblVolTypeStatuses.DataValueField = "Explanation"
        cblVolTypeStatuses.DataBind()

        dv.Sort = "Explanation"

        For Each cb As ListItem In cblVolTypeStatuses.Items()

            cb.Selected = dv(dv.Find(cb.Value))(1)

        Next

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        PopulategvUnmapped()
    End Sub

    Private Sub btnBulkUpdate_Click(sender As Object, e As EventArgs) Handles btnBulkUpdate.Click

        Dim x As String = ""
        Dim cnt As Integer = 0
        For Each canoe As GridViewRow In gvUnMapped.Rows()
            Dim cbBulkUpdate As CheckBox = canoe.Cells(1).FindControl("cbBulkUpdate")
            If cbBulkUpdate.Checked Then
                x += "Update  DWH.md.udMD_POSDesc_Mapping set Voltype = '" & Replace(ddlBulkUpdateVolTypes.SelectedItem.Text, "'", "''") & "' " & _
                    ", DateModified =  getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where IDGroup + '~' + PosDesc = '" & _
                    Replace(gvUnMapped.DataKeys(canoe.RowIndex).Value, "'", "''") & "' "
                cnt += 1
            End If
        Next

        If Len(x) > 1 Then
            ExecuteSql(x)
            PopulategvUnmapped()
            explantionlabel.Text = cnt.ToString & " Rows Updated"
            ModalPopupExtender1.Show()
        Else
            explantionlabel.Text = "No Rows Updated"
            ModalPopupExtender1.Show()
        End If

    End Sub

    Private Sub gvUnMapped_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUnMapped.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlARCategory As DropDownList = e.Row.FindControl("ddlAssignedVolType")
            Dim lblAssignedVolType As Label = e.Row.FindControl("lblAssignedVolType")

            Dim x As String = "select VolType, VolID, 1 as ord From  DWH.ud.udMD_POS_VolTypes " & _
                "where InactivatedDate is null " & _
                "union select '(Select VolType)', -1, 0 " & _
                "order by ord, VolType "

            Dim y As DataView = GetData(x).DefaultView

            ddlARCategory.DataSource = y
            ddlARCategory.DataTextField = "VolType"
            ddlARCategory.DataValueField = "VolID"
            ddlARCategory.DataBind()


            Try
                ddlARCategory.SelectedValue = lblAssignedVolType.Text
            Catch ex As Exception
            End Try

        End If



    End Sub

    Private Sub gvUnMapped_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvUnMapped.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = gvVolType_View()

            sorts = e.SortExpression

            If e.SortExpression = VolTypemap.Text Then

                If CInt(VolTypedir.Text) = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    VolTypedir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    VolTypedir.Text = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                VolTypedir.Text = 1
                VolTypemap.Text = e.SortExpression
            End If

            gvUnMapped.DataSource = dv
            gvUnMapped.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PopulateVolTypesAdmin()

        gvAdminVolTypes.DataSource = GetData("select * from DWH.ud.udMD_POS_VolTypes where InactivatedDate is null order by VolType")
        gvAdminVolTypes.DataBind()

    End Sub

    Private Sub gvAdminVolTypes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminVolTypes.RowCommand
        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName


            If Commander = "RemoveType" Then

                Dim PrepSQL As String = "Update DWH.ud.udMD_POS_VolTypes set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                    "' where VolID = '" & Replace(Detail_ID, "'", "''") & "' and InactivatedDate is null"

                ExecuteSql(PrepSQL)
                PopulateVolTypesAdmin()
                PopulateVolTypeDDL()

                Dim cnt As Integer = 0
                For Each canoe As GridViewRow In gvUnMapped.Rows()
                    Dim cbBulkUpdate As CheckBox = canoe.Cells(1).FindControl("cbBulkUpdate")
                    If cbBulkUpdate.Checked Then
                        cnt += 1
                    End If
                Next

                If cnt > 0 Then
                Else
                    PopulategvUnmapped()
                End If

           
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnAddAdminVolType_Click(sender As Object, e As EventArgs) Handles btnAddAdminVolType.Click


        Dim xl As String = "Insert into DWH.ud.udMD_POS_VolTypes (VolID, VolType, AddedDate, AddedBy) select max(VolID) + 1, " & _
                "'" & Replace(Trim(txtAdminVolype.Text), "'", "''") & "', getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' from DWH.ud.udMD_POS_VolTypes " & _
                "where not exists (select * from DWH.ud.udMD_POS_VolTypes where VolType = '" & Replace(Trim(txtAdminVolype.Text), "'", "''") & "' and InactivatedDate is null)"

        ExecuteSql(xl)
        PopulateVolTypesAdmin()
        PopulateVolTypeDDL()

        Dim cnt As Integer = 0
        For Each canoe As GridViewRow In gvUnMapped.Rows()
            Dim cbBulkUpdate As CheckBox = canoe.Cells(1).FindControl("cbBulkUpdate")
            If cbBulkUpdate.Checked Then
                cnt += 1
            End If
        Next

        If cnt > 0 Then
            explantionlabel.Text = "(" & cnt & " rows currently selected on previous tab; refresh search to view new VolType in Update Grid)"
            ModalPopupExtender1.Show()
        Else
            PopulategvUnmapped()
        End If


    End Sub
End Class