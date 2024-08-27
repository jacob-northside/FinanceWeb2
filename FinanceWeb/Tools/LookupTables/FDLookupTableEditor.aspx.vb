Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices

Imports FinanceWeb.WebFinGlobal

Public Class FDLookupTableEditor
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd As SqlCommand
    Dim cmdNS1 As SqlCommand
    '   Dim dr As SqlDataReader

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

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds = New DataSet
            da = New SqlDataAdapter(SQL, conn)

            da.Fill(ds, "LookUps")

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
            LoadLookupGrid2()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadLookUpGrid()
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter

            SQL = "Select * from DWH." & lblGVLookup.Text & " order by 1  "

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ds = New DataSet

            da = New SqlDataAdapter(SQL, conn)
            da.Fill(ds, "LookUpData")
            gvDWHLookups.DataSource = ds
            gvDWHLookups.DataMember = "LookUpData"
            gvDWHLookups.DataBind()

            'ViewState.Add("TBL", ds)

            lblGVLookup.Text = lblGVLookup.Text & "  - " & gvDWHLookups.Rows.Count.ToString & " rows. "

            If gvDWHLookups.Rows.Count > 0 Then
                btnExportToExcel.Visible = True
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadLookupGrid2()
        Try
            Dim ColumnArray As ArrayList = New ArrayList
            Dim KeyArray As ArrayList = New ArrayList
            Dim Table_Name As String
            Dim dr As SqlDataReader

            'If lblGVLookup.Text.Contains(".") Then
            '    Table_Name = Mid(lblGVLookup.Text, 1, InStr(lblGVLookup.Text, "."))
            'Else
            Table_Name = ddlLookUps.SelectedItem.ToString
            '  End If

            SQL = "select COLUMN_NAME from DWH.INFORMATION_SCHEMA.COLUMNS   " & _
              "where TABLE_NAME = '" & Table_Name & "' " & _
              "order by ORDINAL_POSITION asc "

            cmd = New SqlCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                ColumnArray.Add(dr.Item("Column_Name"))
            End While
            dr.Close()

            SQL = "SELECT   " & _
            "T.TABLE_NAME,   " & _
            "T.CONSTRAINT_NAME,   " & _
            "K.COLUMN_NAME,   " & _
            "K.ORDINAL_POSITION   " & _
            "FROM   " & _
            "DWH.INFORMATION_SCHEMA.TABLE_CONSTRAINTS T  " & _
            "INNER JOIN  " & _
            "DWH.INFORMATION_SCHEMA.KEY_COLUMN_USAGE K  " & _
            "ON T.CONSTRAINT_NAME = K.CONSTRAINT_NAME   " & _
            "WHERE  " & _
            "T.CONSTRAINT_TYPE = 'PRIMARY KEY'   " & _
            "AND T.TABLE_NAME = '" & Table_Name & "'  " & _
            "ORDER BY  " & _
            "T.TABLE_NAME,  " & _
            "K.ORDINAL_POSITION "

            cmd = New SqlCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            While dr.Read
                KeyArray.Add(dr.Item("Column_Name"))
            End While
            dr.Close()

            Dim temp As String
            temp = ColumnArray.Count.ToString
            Dim temp2 As String
            temp2 = KeyArray.Count.ToString

            Dim tbl As System.Data.DataTable = New DataTable()
            Dim keys(0) As DataColumn
            Dim col As New DataColumn
            Dim j As Integer = 0


            For i As Integer = 0 To ColumnArray.Count.ToString - 1
                col = New DataColumn(ColumnArray.Item(i).ToString)
                If KeyArray.Contains(ColumnArray.Item(i).ToString) Then
                    keys(j) = col
                    j += 1
                End If
                tbl.Columns.Add(col)
            Next

            If j <> 0 Then
                tbl.PrimaryKey = keys
            End If

            If lblGVLookup.Text.Contains("-") Then
                SQL = "Select * from DWH." & Mid(lblGVLookup.Text, 1, InStr(lblGVLookup.Text, "-") - 2) & " order by 1  "
            Else
                SQL = "Select * from DWH." & lblGVLookup.Text & " order by 1  "
            End If



            cmd = New SqlCommand(SQL, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dr = cmd.ExecuteReader
            Dim dRow As DataRow
            While dr.Read
                dRow = tbl.NewRow()
                For i As Integer = 0 To ColumnArray.Count.ToString - 1
                    If IsDBNull(dr.Item(ColumnArray.Item(i).ToString)) Then
                        dRow(ColumnArray.Item(i).ToString) = ""
                    Else
                        dRow(ColumnArray.Item(i).ToString) = dr.Item(ColumnArray.Item(i).ToString)
                    End If
                Next
                tbl.Rows.Add(dRow)
            End While
            dr.Close()

            gvDWHLookups.DataSource = tbl
            gvDWHLookups.DataBind()

            ViewState.Add("TBL", tbl)

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

    Private Sub gvDWHLookups_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvDWHLookups.RowCancelingEdit
        Try

            gvDWHLookups.EditIndex = -1
            gvDWHLookups.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvDWHLookups.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub gvDWHLookups_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDWHLookups.RowEditing
        Try
            gvDWHLookups.EditIndex = e.NewEditIndex
            gvDWHLookups.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvDWHLookups.DataBind()

            ''Dim keyArray As ArrayList = New ArrayList

            ''For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).PrimaryKey.Count - 1
            ''    keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
            ''Next
            ''For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
            ''    If keyArray.Contains(DirectCast(ViewState("TBL"), DataTable).Columns(i).ToString) Then
            ''        DirectCast(ViewState("TBL"), DataTable).Columns(i).ReadOnly = True
            ''    Else
            ''        DirectCast(ViewState("TBL"), DataTable).Columns(i).ReadOnly = False
            ''    End If
            ''Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvDWHLookups_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvDWHLookups.RowUpdating
        Try
            Dim columnArray As ArrayList = New ArrayList
            Dim keyArray As ArrayList = New ArrayList
            Dim SQLUpdate As String = ""
            Dim SQLWhere As String = ""

            gvDWHLookups.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvDWHLookups.DataBind()

            For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                columnArray.Add(DirectCast(ViewState("TBL"), DataTable).Columns.Item(i).ColumnName.ToString())
                keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
            Next

            For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                If keyArray.Contains(DirectCast(ViewState("TBL"), DataTable).Columns(i).ToString) Then

                Else
                    SQLUpdate = SQLUpdate & columnArray(i).ToString & " = '" & e.NewValues.Item(columnArray(i)).ToString & "', "
                End If
            Next

            If SQLUpdate <> "" Then
                SQLUpdate = Mid(SQLUpdate, 1, Len(SQLUpdate) - 2)
            End If

            If DirectCast(ViewState("TBL"), DataTable).Constraints.Count > 0 Then
                For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Constraints.Count - 1
                    SQLWhere = SQLWhere & DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString & " = '" & DirectCast(ViewState("TBL"), DataTable).Rows(e.RowIndex).Item(0).ToString & "' and "

                Next
            End If

            If SQLWhere <> "" Then
                SQLWhere = Mid(SQLWhere, 1, Len(SQLWhere) - 4)
            End If

            SQLUpdate = "Update DWH." & Mid(lblGVLookup.Text, 1, InStr(lblGVLookup.Text, "-") - 2) & "  set  " & SQLUpdate & "  where " & SQLWhere
            cmd = New SqlCommand(SQLUpdate, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteReader()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        Finally
            gvDWHLookups.EditIndex = -1
            gvDWHLookups.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvDWHLookups.DataBind()
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            LoadLookupGrid2()

        End Try
    End Sub
End Class