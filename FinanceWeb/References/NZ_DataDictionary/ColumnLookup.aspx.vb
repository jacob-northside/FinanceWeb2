Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices

Imports FinanceWeb.WebFinGlobal

Public Class ColumnLookup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Search(sender As Object, e As System.EventArgs) Handles SearchColumnName.TextChanged

        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim search_column_sql As String = ""

        Try
            If Columns_Srch_RadioBtn.SelectedValue = "Wildcard" Then
                search_column_sql = "Select * from DWH.DOC.NZ_FDColumnData where columnname like '%" & SearchColumnName.Text & "%'"
            Else
                search_column_sql = "Select * from DWH.DOC.NZ_FDColumnData where columnname = '" & SearchColumnName.Text & "'"
            End If

            Debug.Print(search_column_sql)

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(search_column_sql, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                Debug.Print(search_column_sql)
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                Lookup_Columns.DataSource = ds
                Lookup_Columns.DataMember = "LookUpData"
                Lookup_Columns.DataBind()

                ViewState("Update_Columns") = ds

                conn.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


End Class