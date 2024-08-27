Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices

Imports FinanceWeb.WebFinGlobal

Public Class FDLookupTables
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd As SqlCommand
    Dim cmdNS1 As SqlCommand
    Dim dr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then

            Else
                LoadExistingTables()
            End If

            If User.Identity.IsAuthenticated = True Then
                btnLoadData.Visible = True
                If gvDWHLookups.Rows.Count > 0 Then
                    btnExportToExcel.Visible = True
                End If
            Else
                btnLoadData.Visible = False
                btnExportToExcel.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadExistingTables()
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter

            SQL = "Select " & _
                "Table_Catalog, Table_Schema, " & _
                "table_Name,  " & _
                "(Table_Schema +'.' +TABLE_name) GVSearch " & _
                "From DWH.INFORMATION_SCHEMA.TABLES " & _
                "where Table_Name like '%_LU' " & _
                "and Table_Type = 'Base Table' " & _
                "order by Table_Name "
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet
                da = New SqlDataAdapter(SQL, conn)
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "LookUps")
            End Using

            ddlLookUps.DataSource = ds
            ddlLookUps.DataMember = "LookUps"
            ddlLookUps.DataTextField = "table_Name"
            ddlLookUps.DataValueField = "GVSearch"
            ddlLookUps.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnLoadData_Click(sender As Object, e As System.EventArgs) Handles btnLoadData.Click
        Try
            lblGVLookup.Text = ddlLookUps.SelectedValue.ToString
            LoadLookUpGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadLookUpGrid()
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter


            SQL = "Select * from DWH." & lblGVLookup.Text
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet

                da = New SqlDataAdapter(SQL, conn)
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "LookUpData")
                gvDWHLookups.DataSource = ds
                gvDWHLookups.DataMember = "LookUpData"
                gvDWHLookups.DataBind()
            End Using
            lblGVLookup.Text = lblGVLookup.Text & "  - " & gvDWHLookups.Rows.Count.ToString & " rows. "

            If gvDWHLookups.Rows.Count > 0 Then
                btnExportToExcel.Visible = True
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()
            'Dim FileName As String = "FSILookUpFile.xls"

            If gvDWHLookups.Rows.Count <> 0 Then

                'original code  
                Page.Response.AddHeader("content-disposition", "attachment;filename=FSILookUpFile.xls")
                Page.Response.ContentType = "application/vnd.ms-excel"
                '  Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" 
                Page.Response.Charset = ""
                Page.EnableViewState = False
                frm.Attributes("runat") = "server"
                Controls.Add(frm)
                frm.Controls.Add(gvDWHLookups)
                frm.RenderControl(hw)
                Response.Write(sw.ToString())
                'This line works instead of the Response.end () if needed
                ' HttpContext.Current.ApplicationInstance.CompleteRequest()
                Response.End()
            Else
                btnExportToExcel.Visible = False

            End If


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class