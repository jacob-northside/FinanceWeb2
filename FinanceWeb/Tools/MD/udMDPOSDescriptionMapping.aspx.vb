Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security

Imports System.Xml
Imports System.Web
Imports System.Web.UI.HtmlControls
Imports FinanceWeb.WebFinGlobal
Imports DocumentFormat.OpenXml

Imports System.Configuration
Imports System.Collections
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Imports System.Drawing
Imports ClosedXML.Excel


Public Class udMDPOSDescriptionMapping
    Inherits System.Web.UI.Page
    Public Shared MappedView As New DataView
    Public Shared UnMappedView As New DataView
    Public Shared sortmap As String
    Public Shared sortunmap As String
    Public Shared mapdir As Integer
    Public Shared unmapdir As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                Dim cmd As SqlCommand
                Dim da As New SqlDataAdapter
                Dim ds As New DataSet

                Dim IDGroupsql As String = "select ' -- (optional) -- ' as IDGroup, ' -- noneselected -- ' as IDValue, 0 as ord " & _
                      "  union " & _
                      " select distinct IDGroup, IDGroup, 1 FROM DWH.MD.udMD_POSDesc_Mapping with (nolock) " & _
                      " order by ord, IDGroup "

                ds.Clear()
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                        End If

                    cmd = New SqlCommand(IDGroupsql, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds, "OData")

                    End Using


                ddlIDGroupLimit.DataSource = ds
                ddlIDGroupLimit.DataValueField = "IDValue"
                ddlIDGroupLimit.DataTextField = "IDGroup"
                ddlIDGroupLimit.DataBind()

                resetddlDesc()

                loadgrid(0)
                loadgrid(1)

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub loadgrid(sandwich As Integer)

        Dim gridsql As String = "SELECT udMD_POSDesc_MappingID as ID, IDGroup, " & _
            " POSDESC, isnull(NP9_BillDr, LMG_NPI) as NP9_BillDr, " & _
            "m.Entity, CostCenter, VolType , d.DESCRIPTION as DeptDesc " & _
            "FROM DWH.MD.udMD_POSDesc_Mapping m with (nolock) left join DWH.Axiom.DEPT d with (nolock) on m.Entity*100000 + m.CostCenter = d.DEPT "
        If sandwich = 0 Then
            gridsql += "where (m.Entity = '99' or CostCenter = '999' /*or VolType is Null or len(VolType) = 0 */) and isnull(POSDesc, '') <> '' and IDGroup <> 'PMA' and IDGroup not like 'UCASH%'  "
        Else
            gridsql += "where (IDGroup = '" & ddlIDGroupLimit.SelectedValue & "' or ' -- noneselected -- ' = '" & ddlIDGroupLimit.SelectedValue & "') " & _
                " and (POSDesc = '" & ddlDescLimit.SelectedValue & "' or ' -- noneselected -- ' = '" & ddlDescLimit.SelectedValue & "') "
        End If




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

        If sandwich = 0 Then
            UnMappedView = ds.Tables(0).DefaultView
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()
            gvUnMapped.ShowHeaderWhenEmpty = True

            If ds.Tables(0).Rows.Count > 0 Then
                lblAllMapped.Visible = False
            Else
                lblAllMapped.Visible = True
            End If

        ElseIf sandwich = 1 Then
            MappedView = ds.Tables(0).DefaultView
            gvUnMapped.DataSource = MappedView
            gvEditMapped.DataSource = ds
            gvEditMapped.DataBind()
            gvEditMapped.ShowHeaderWhenEmpty = True
        End If


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
    Private Sub gvUnMapped_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvUnMapped.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvUnMapped_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvUnMapped.SelectedIndexChanged
        Try

            lblIDGrp.Text = Replace(gvUnMapped.SelectedRow.Cells(2).Text, "&nbsp;", "")
            lblPOSDesc.Text = Replace(gvUnMapped.SelectedRow.Cells(3).Text, "&nbsp;", "")
            lblBillDrNP9.Text = Replace(gvUnMapped.SelectedRow.Cells(4).Text, "&nbsp;", "")
            txtEntity.Text = Replace(gvUnMapped.SelectedRow.Cells(5).Text, "&nbsp;", "")
            txtCostCenter.Text = Replace(gvUnMapped.SelectedRow.Cells(6).Text, "&nbsp;", "")
            txtVolType.Text = Replace(gvUnMapped.SelectedRow.Cells(7).Text, "&nbsp;", "")

            For Each canoe As GridViewRow In gvUnMapped.Rows
                If canoe.RowIndex = gvUnMapped.SelectedIndex Then
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

    Private Sub btnUpdateUnmapped_Click(sender As Object, e As EventArgs) Handles btnUpdateUnmapped.Click

        Try

            If gvUnMapped.SelectedIndex = -1 Then
                explantionlabel.Text = "Please select a row"
                ModalPopupExtender1.Show()
                Exit Sub
            End If
            Dim updatesql As String = "Update DWH.MD.udMD_POSDesc_Mapping set Entity = '" & Replace(txtEntity.Text, "'", "''") & _
                "', CostCenter = '" & Replace(txtCostCenter.Text, "'", "''") & "', VolType = '" & Replace(txtVolType.Text, "'", "''") & "' " & _
                ", DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                " where udMD_POSDesc_MappingID = '" & gvUnMapped.SelectedRow.Cells(1).Text & "' "

            Dim cmd As SqlCommand

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
                cmd.ExecuteNonQuery()
                loadgrid(0)
                explantionlabel.Text = "Successfully Updated Row"
                explantionlabel.DataBind()
                ModalPopupExtender1.Show()
            End Using

        Catch ex As Exception
            explantionlabel.Text = "Error updating row; Please report to Admin"
            ModalPopupExtender1.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvEditMapped_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvEditMapped.PageIndexChanging
        Try

            gvEditMapped.PageIndex = e.NewPageIndex
            gvEditMapped.DataSource = MappedView
            gvEditMapped.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvEditMapped_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvEditMapped.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvEditMapped_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvEditMapped.SelectedIndexChanged
        Try

            lblEditIDGrp.Text = Replace(gvEditMapped.SelectedRow.Cells(2).Text, "&nbsp;", "")
            lblEditPOSDesc.Text = Replace(gvEditMapped.SelectedRow.Cells(3).Text, "&nbsp;", "")
            lblEditBillDrNP9.Text = Replace(gvEditMapped.SelectedRow.Cells(4).Text, "&nbsp;", "")
            txtEditEntity.Text = Replace(gvEditMapped.SelectedRow.Cells(5).Text, "&nbsp;", "")
            txtEditCostCenter.Text = Replace(gvEditMapped.SelectedRow.Cells(6).Text, "&nbsp;", "")
            txtEditVolType.Text = Replace(gvEditMapped.SelectedRow.Cells(7).Text, "&nbsp;", "")

            For Each canoe As GridViewRow In gvEditMapped.Rows
                If canoe.RowIndex = gvEditMapped.SelectedIndex Then
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

    Private Sub btnUpdateMapped_Click(sender As Object, e As EventArgs) Handles btnUpdateMapped.Click
        Try

            Dim updatesql As String = "Update DWH.MD.udMD_POSDesc_Mapping set Entity = '" & Replace(txtEditEntity.Text, "'", "''") & _
                "', CostCenter = '" & Replace(txtEditCostCenter.Text, "'", "''") & "', VolType = '" & Replace(txtEditVolType.Text, "'", "''") & "' " & _
                ", DateModified = getdate(), ModifiedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                " where udMD_POSDesc_MappingID = '" & gvEditMapped.SelectedRow.Cells(1).Text & "' "

            Dim cmd As SqlCommand

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
                cmd.ExecuteNonQuery()
                loadgrid(1)
                explantionlabel2.Text = "Successfully Updated Row"
                explantionlabel2.DataBind()
                ModalPopupExtender2.Show()
            End Using

        Catch ex As Exception
            explantionlabel2.Text = "Error updating row; Please report to Admin"
            ModalPopupExtender2.Show()
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

    Private Sub gvEditMapped_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvEditMapped.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = MappedView

        sorts = e.SortExpression

        If e.SortExpression = sortmap Then

            If mapdir = 1 Then
                dv.Sort = sorts + " " + "desc"
                mapdir = 0
            Else
                dv.Sort = sorts + " " + "asc"
                mapdir = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            mapdir = 1
            sortmap = e.SortExpression
        End If

        gvEditMapped.DataSource = dv
        gvEditMapped.DataBind()
    End Sub

    Private Sub ddlIDGroupLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIDGroupLimit.SelectedIndexChanged
        resetddlDesc()
    End Sub

    Private Sub resetddlDesc()

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Dim IDGroupsql As String = "select ' -- noneselected -- ' as POSDesc, ' -- (optional) -- ' as POSText, 0 as ord " & _
            " union " & _
            " select POSDesc , POSDESC, 1 FROM DWH.MD.udMD_POSDesc_Mapping with (nolock) " & _
            " where IDGroup = '" & Replace(ddlIDGroupLimit.SelectedValue.ToString, "'", "''") & "' " & _
            " order by ord, POSText "

        ds.Clear()
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
                End If

            cmd = New SqlCommand(IDGroupsql, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

            End Using


        ddlDescLimit.DataSource = ds
        ddlDescLimit.DataValueField = "POSDesc"
        ddlDescLimit.DataTextField = "POSText"
        ddlDescLimit.DataBind()

        ddlDescLimit.SelectedIndex = 0

        If ddlIDGroupLimit.SelectedValue = " -- noneselected -- " Then
            ddlDescLimit.Enabled = False
        Else
            ddlDescLimit.Enabled = True
        End If

        loadgrid(1)

    End Sub

    Private Sub ddlDescLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDescLimit.SelectedIndexChanged
        loadgrid(1)
    End Sub

    Private Sub btnExportMDPOS_Click(sender As Object, e As EventArgs) Handles btnExportMDPOS.Click
        Try

            Dim dt As New DataTable("GridView_Data")

            dt = MappedView.ToTable()

            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dt)
                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment;filename=MD_POS_Mapping.xlsx")
                Using MyMemoryStream As New MemoryStream()
                    wb.SaveAs(MyMemoryStream)
                    MyMemoryStream.WriteTo(Response.OutputStream)
                    Response.Flush()
                    Response.[End]()
                End Using
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class