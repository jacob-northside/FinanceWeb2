Imports System.Data.SqlClient
Imports System.Data

Imports FinanceWeb.WebFinGlobal

Public Class ErrorTracking
    Inherits System.Web.UI.Page
    Public Shared book As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then

        Else
            book = New DataTable
            PopulateUserDropDown()
            PopulatePageDropDown()
            PopulateCodeBlockDropDown()
            PopulategvNewErrors()
        End If


    End Sub

    Sub PopulateUserDropDown()

        Try
            Dim da As SqlDataAdapter
            Dim dt As New DataTable
            Dim sql As String = "select top 1 'Select User (Optional)' as usr, '  -- noneselected --  ' as userid, 0 as ord from WebFD.FinWeb.ERRORLOG el " & _
            " union " & _
            " Select distinct case when ed.FirstName is null and ed.LastName is null then USERID else ISNULL(ed.FirstName + ' ', '') + ISNULL(ed.LastName + ' ', '') end as usr, USERID, 1 " & _
            " from WebFD.FinWeb.ERRORLOG el " & _
            " left join DWH.dbo.Email_Distribution ed on el.USERID = ed.NetworkLogin " & _
            " where Active is null  " & _
            " order by ord, usr "

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("WebFDConn").ConnectionString)
                da = New SqlDataAdapter(sql, conn2)
                da.Fill(dt)
            End Using

            ddlUserID.DataSource = dt
            ddlUserID.DataValueField = "userid"
            ddlUserID.DataTextField = "usr"
            ddlUserID.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub PopulatePageDropDown()

        Try
            Dim da As SqlDataAdapter
            Dim dt As New DataTable
            Dim sql As String = "select top 1 'Select Page (Optional) ' as page, '  -- noneselected --  ' as pageval, 0 as ord from WebFD.FinWeb.ERRORLOG el " & _
            " union " & _
            " Select distinct Page, Page, 1 " & _
            " from WebFD.FinWeb.ERRORLOG el " & _
            " left join DWH.dbo.Email_Distribution ed on el.USERID = ed.NetworkLogin " & _
            " where Active is null  " & _
            " and (USERID = '" & Replace(ddlUserID.SelectedValue, "'", "''") & "' or '  -- noneselected --  ' = '" & Replace(ddlUserID.SelectedValue, "'", "''") & "') " & _
            " order by ord, Page "

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("WebFDConn").ConnectionString)
                da = New SqlDataAdapter(sql, conn2)
                da.Fill(dt)
            End Using

            ddlPage.DataSource = dt
            ddlPage.DataValueField = "pageval"
            ddlPage.DataTextField = "page"
            ddlPage.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub PopulateCodeBlockDropDown()

        Try
            Dim da As SqlDataAdapter
            Dim dt As New DataTable
            Dim sql As String = "select top 1 'Select Code Block (Optional) ' as CodeBlock, '  -- noneselected --  ' as CodeBlockval, 0 as ord from WebFD.FinWeb.ERRORLOG el " & _
            " union " & _
            " Select distinct CodeBlock, CodeBlock, 1 " & _
            " from WebFD.FinWeb.ERRORLOG el " & _
            " left join DWH.dbo.Email_Distribution ed on el.USERID = ed.NetworkLogin " & _
            " where Active is null  " & _
            " and (USERID = '" & Replace(ddlUserID.SelectedValue, "'", "''") & "' or '  -- noneselected --  ' = '" & Replace(ddlUserID.SelectedValue, "'", "''") & "') " & _
            " and (Page = '" & Replace(ddlPage.SelectedValue, "'", "''") & "' or '  -- noneselected --  ' = '" & Replace(ddlPage.SelectedValue, "'", "''") & "') " & _
            " order by ord, CodeBlock "

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("WebFDConn").ConnectionString)
                da = New SqlDataAdapter(sql, conn2)
                da.Fill(dt)
            End Using

            ddlCodeBlock.DataSource = dt
            ddlCodeBlock.DataValueField = "CodeBlockval"
            ddlCodeBlock.DataTextField = "CodeBlock"
            ddlCodeBlock.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub PopulategvNewErrors()

        Try

            Dim Sql2 As String
            Dim da As SqlDataAdapter

            book.Clear()

            Sql2 = "Select case when ed.FirstName is null and ed.LastName is null then USERID else ISNULL(ed.FirstName + ' ', '') + ISNULL(ed.LastName + ' ', '') end as usr, " & _
                "el.* from WebFD.FinWeb.ERRORLOG el " & _
                "left join DWH.dbo.Email_Distribution ed on el.USERID = ed.NetworkLogin " & _
                "where Active Is null " & _
                " and (USERID = '" & Replace(ddlUserID.SelectedValue, "'", "''") & "' or '  -- noneselected --  ' = '" & Replace(ddlUserID.SelectedValue, "'", "''") & "') " & _
                " and (Page = '" & Replace(ddlPage.SelectedValue, "'", "''") & "' or '  -- noneselected --  ' = '" & Replace(ddlPage.SelectedValue, "'", "''") & "') " & _
                " and (CodeBlock = '" & Replace(ddlCodeBlock.SelectedValue, "'", "''") & "' or '  -- noneselected --  ' = '" & Replace(ddlCodeBlock.SelectedValue, "'", "''") & "') " & _
                " order by DateLogged Desc, Page asc"

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("WebFDConn").ConnectionString)
                da = New SqlDataAdapter(Sql2, conn2)
                da.Fill(book)
                gvNewErrors.DataSource = book
                gvNewErrors.DataBind()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub


    Private Sub gvNewErrors_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNewErrors.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvNewErrors_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvNewErrors.SelectedIndexChanged
        Try

            For Each canoe As GridViewRow In gvNewErrors.Rows
                If canoe.RowIndex = gvNewErrors.SelectedIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.Color.White
                Else
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                End If
            Next

            Dim dr As DataRow

            dr = book(gvNewErrors.SelectedIndex)
            lblExceptionCode.Text = dr("ExceptionCode").ToString

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnUpdateError_Click(sender As Object, e As System.EventArgs) Handles btnUpdateError.Click

    End Sub

    Private Sub ddlCodeBlock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCodeBlock.SelectedIndexChanged
        PopulategvNewErrors()

    End Sub

    Private Sub ddlPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPage.SelectedIndexChanged
        PopulateCodeBlockDropDown()
        PopulategvNewErrors()

    End Sub

    Private Sub ddlUserID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUserID.SelectedIndexChanged
        PopulatePageDropDown()
        PopulateCodeBlockDropDown()
        PopulategvNewErrors()

    End Sub
End Class