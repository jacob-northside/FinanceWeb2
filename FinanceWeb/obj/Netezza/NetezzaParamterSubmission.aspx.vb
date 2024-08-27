Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.IO
Imports System.DirectoryServices
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal
Imports FinanceWeb.WebFinGlobal
Imports System.Data.OleDb

Public Class NetezzaParamterSubmission
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then

            Else
                LoadSchemas()
                LoadTables()
                LoadColumns()
            End If


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub LoadSchemas()

        Try


            Dim da As New OleDbDataAdapter
            Dim cmd As OleDbCommand
            Dim ds As New DataSet

            Dim ddlfirstsql As String = "select distinct SCHEMA from _v_table"

            Using conn As New OleDbConnection(ConfigurationManager.ConnectionStrings("NetezzaChelseaConnection").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New OleDbCommand(ddlfirstsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlSchemaSelect.Items.Clear()
            ddlSchemaSelect.DataSource = ds
            ddlSchemaSelect.DataValueField = "SCHEMA"
            ddlSchemaSelect.DataTextField = "SCHEMA"
            ddlSchemaSelect.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub LoadTables()

        Try

            Dim da As New OleDbDataAdapter
            Dim cmd As OleDbCommand
            Dim ds As New DataSet


            Dim ddlfirstsql As String = "select distinct TABLENAME from _v_table where Schema = '" & ddlSchemaSelect.SelectedValue & "'"

            Using conn As New OleDbConnection(ConfigurationManager.ConnectionStrings("NetezzaChelseaConnection").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New OleDbCommand(ddlfirstsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlTableSelect.Items.Clear()
            ddlTableSelect.DataSource = ds
            ddlTableSelect.DataValueField = "TABLENAME"
            ddlTableSelect.DataTextField = "TABLENAME"
            ddlTableSelect.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub LoadColumns()

        Try


            Dim da As New OleDbDataAdapter
            Dim cmd As OleDbCommand
            Dim ds As New DataSet

            Dim ddlfirstsql As String = "select distinct ATTNAME from _v_relation_column where schema  = '" & ddlSchemaSelect.SelectedValue & _
                "' and Name = '" & ddlTableSelect.SelectedValue & "'"

            Using conn As New OleDbConnection(ConfigurationManager.ConnectionStrings("NetezzaChelseaConnection").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New OleDbCommand(ddlfirstsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlColumnSelect.Items.Clear()
            ddlColumnSelect.DataSource = ds
            ddlColumnSelect.DataValueField = "ATTNAME"
            ddlColumnSelect.DataTextField = "ATTNAME"
            ddlColumnSelect.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlSchemaSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSchemaSelect.SelectedIndexChanged
        LoadTables()
    End Sub

    Private Sub ddlTableSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTableSelect.SelectedIndexChanged
        LoadColumns()
    End Sub

    Private Sub btnUpdateSelectedColumn_Click(sender As Object, e As EventArgs) Handles btnUpdateSelectedColumn.Click

        Try

            Dim submitsql As String = "DELETE FROM FSI.SpotfireColumnCompare " & _
                "where now() - TimeSubmitted > '00:30:00' or UserID = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'; " & _
                "INSERT INTO  FSI.SpotfireColumnCompare  select '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', " & _
                ddlColumnSelect.SelectedValue & ",  now() from " & ddlSchemaSelect.SelectedValue & "." & ddlTableSelect.SelectedValue

            Dim cmd As New OleDbCommand

            Using conn As New OleDbConnection(ConfigurationManager.ConnectionStrings("NetezzaChelseaConnection").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New OleDbCommand(submitsql, conn)
                cmd.ExecuteNonQuery()

            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
End Class