Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices

Imports System.Drawing
Imports ClosedXML.Excel

Imports FinanceWeb.WebFinGlobal

Public Class LookupTableEditor
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Public Shared fxed As String
    Public Shared ChelseaRow As Integer
    Public Shared indexfxed As String
    Public Shared Searchdir As Integer
    Public Shared Searchmap As String
    Public Shared SearchView As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If IsPostBack Then

            Else
                'Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim ds As DataSet
                Dim da As New SqlDataAdapter

                Dim LUsql As String = "SELECT [Table_Reference] " & _
                 " ,[Table_Display_Name] " & _
                "  FROM [WebFD].[FinWeb].[Table_Editor] te " & _
                "  join WebFD.FinWeb.Table_Editors ts on te.TableID = ts.TableID " & _
                "  where UserID = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'"

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet
                da = New SqlDataAdapter(LUsql, conn)

                da.Fill(ds, "LookUps")

                ddlSelectedTable.DataSource = ds
                ddlSelectedTable.DataMember = "LookUps"
                ddlSelectedTable.DataTextField = "Table_Display_Name"
                ddlSelectedTable.DataValueField = "Table_Reference"
                ddlSelectedTable.DataBind()

                LoadGrid()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlSelectedTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSelectedTable.SelectedIndexChanged

        LoadGrid()

    End Sub

    Sub LoadGrid()

        Try

            If Left(ddlSelectedTable.SelectedValue, 5) = "[DWH]" Then
                Dim ColumnArray As ArrayList = New ArrayList
                Dim KeyArray As ArrayList = New ArrayList
                Dim Table_Name As String
                Dim Table_Schema As String
                Dim dr As SqlDataReader
                Dim cmd As SqlCommand

                'If lblGVLookup.Text.Contains(".") Then
                '    Table_Name = Mid(lblGVLookup.Text, 1, InStr(lblGVLookup.Text, "."))
                'Else
                Dim Ref As String = ddlSelectedTable.SelectedValue.ToString
                '  End If
                Table_Schema = Left(Right(Ref, Len(Ref) - InStr(Ref, ".")), InStr(Right(Ref, Len(Ref) - InStr(Ref, ".")), "."))
                Table_Name = Right(Right(Ref, Len(Ref) - InStr(Ref, ".")), Len(Right(Ref, Len(Ref) - InStr(Ref, "."))) - InStr(Right(Ref, Len(Ref) - InStr(Ref, ".")), "."))

                Table_Schema = Replace(Replace(Replace(Table_Schema, ".", ""), "[", ""), "]", "")
                Table_Name = Replace(Replace(Replace(Table_Name, ".", ""), "[", ""), "]", "")

                Dim FirstSql As String = "Select 'ChelseaOrderingColumn' as COLUMN_NAME, 0 as ORDINAL_POSITION union select COLUMN_NAME, ORDINAL_POSITION from DWH.INFORMATION_SCHEMA.COLUMNS   " & _
                  "where TABLE_NAME = '" & Table_Name & "' and TABLE_SCHEMA = '" & Table_Schema & "'" & _
                  "order by ORDINAL_POSITION asc "

                cmd = New SqlCommand(FirstSql, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    ColumnArray.Add(dr.Item("Column_Name"))
                End While
                dr.Close()

                Dim SecondSql As String = "SELECT  " & _
                "K.COLUMN_NAME  " & _
                "FROM   " & _
                "DWH.INFORMATION_SCHEMA.TABLE_CONSTRAINTS T  " & _
                "INNER JOIN  " & _
                "DWH.INFORMATION_SCHEMA.KEY_COLUMN_USAGE K  " & _
                "ON T.CONSTRAINT_NAME = K.CONSTRAINT_NAME   " & _
                "WHERE  " & _
                "T.CONSTRAINT_TYPE = 'PRIMARY KEY'   " & _
                "AND T.TABLE_NAME = '" & Table_Name & "'  and T.TABLE_SCHEMA = '" & Table_Schema & "' " & _
                " union " & _
                "select COLUMN_NAME from DWH.INFORMATION_SCHEMA.COLUMNS " & _
                "where TABLE_NAME = '" & Table_Name & "'  and TABLE_SCHEMA = '" & Table_Schema & "' " & _
                "and COLUMNPROPERTY(object_id(table_name), column_name, 'IsIdentity') = 1 "

                cmd = New SqlCommand(SecondSql, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    KeyArray.Add(dr.Item("Column_Name"))
                End While
                dr.Close()

                'Dim temp As String
                'temp = ColumnArray.Count.ToString
                'Dim temp2 As String
                'temp2 = KeyArray.Count.ToString

                Dim tbl As System.Data.DataTable = New DataTable()
                Dim keys(ColumnArray.Count) As DataColumn
                Dim col As New DataColumn
                Dim j As Integer = 0

                fxed = ""
                For i As Integer = 0 To ColumnArray.Count.ToString - 1
                    col = New DataColumn(ColumnArray.Item(i).ToString)
                    If KeyArray.Contains(ColumnArray.Item(i).ToString) Then
                        keys(j) = col
                        j += 1
                        fxed += (i).ToString + ", "
                    End If
                    tbl.Columns.Add(col)
                Next

                If j <> 0 Then
                    tbl.PrimaryKey = keys
                End If

                Dim KeyArray2 As ArrayList = New ArrayList
                Dim SecondPointFiveSql As String = "select COLUMN_NAME from DWH.INFORMATION_SCHEMA.COLUMNS " & _
     "where TABLE_NAME = '" & Table_Name & "'  and TABLE_SCHEMA = '" & Table_Schema & "' " & _
     "and COLUMNPROPERTY(object_id(table_name), column_name, 'IsIdentity') = 1 "

                cmd = New SqlCommand(SecondPointFiveSql, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    KeyArray2.Add(dr.Item("Column_Name"))
                End While
                dr.Close()

                'Dim temp3 As String
                'temp3 = ColumnArray.Count.ToString
                'Dim temp4 As String
                'temp4 = KeyArray2.Count.ToString


                indexfxed = ""
                For i2 As Integer = 0 To ColumnArray.Count.ToString - 1
                    If KeyArray2.Contains(ColumnArray.Item(i2).ToString) Then
                        indexfxed += (i2).ToString + ", "
                    ElseIf ColumnArray.Item(i2).ToString = "Active" Then
                        indexfxed += (i2).ToString + ", "
                    ElseIf ColumnArray.Item(i2).ToString = "LastUpdated" Then
                        indexfxed += (i2).ToString + ", "
                    End If
                Next


                'If lblGVLookup.Text.Contains("-") Then
                '    Sql = "Select * from DWH." & Mid(lblGVLookup.Text, 1, InStr(lblGVLookup.Text, "-") - 2) & " order by 1  "
                'Else
                '    Sql = "Select * from DWH." & lblGVLookup.Text & " order by 1  "
                'End If

                Dim ThridSQL As String = "Select 1 as ChelseaOrderingColumn, * from " & Ref & " order by 2"


                cmd = New SqlCommand(ThridSQL, conn)
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

                Searchdir = 5
                Searchmap = ""

                Dim insertRow As DataRow = tbl.NewRow()
                insertRow("ChelseaOrderingColumn") = 0

                For i As Integer = 1 To tbl.Columns.Count.ToString - 1
                    insertRow(i) = ""
                Next

                tbl.Rows.Add(insertRow)

                SearchView = tbl.DefaultView

                SearchView.Sort = "ChelseaOrderingColumn"

                ChelseaRow = 0
                gvLUTable.DataSource = SearchView
                gvLUTable.DataBind()

                ViewState.Add("TBL", tbl)

                'lblGVLookup.Text = lblGVLookup.Text & "  - " & gvDWHLookups.Rows.Count.ToString & " rows. "

                If gvLUTable.Rows.Count > 0 Then
                    btnExportLUTable.Visible = True
                End If

            Else

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvLUTable_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLUTable.PageIndexChanging
        Try

            ChelseaRow = 0
            gvLUTable.DataSource = SearchView
            gvLUTable.PageIndex = e.NewPageIndex
            'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvLUTable.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLUTable_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvLUTable.RowCancelingEdit
        Try

            ChelseaRow = 0
            gvLUTable.EditIndex = -1
            gvLUTable.DataSource = SearchView
            'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvLUTable.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLUTable_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvLUTable.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).CssClass = "hidden"
            ElseIf e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(1).CssClass = "hidden"
            End If
        Catch

        End Try

    End Sub

    Private Sub gvLUTable_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLUTable.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If ChelseaRow = 0 Then
                    ChelseaRow = 1
                    If e.Row.DataItem("ChelseaOrderingColumn") = "0" Then
                        If e.Row.RowState = DataControlRowState.Alternate Then
                            Dim lbEdit As LinkButton = e.Row.Controls(0).Controls(0)
                            lbEdit.Text = "Add New Row"
                            Dim lbDelete As LinkButton = e.Row.Controls(0).Controls(2)
                            lbDelete.Text = ""
                            lbDelete.Enabled = False
                        ElseIf e.Row.RowState = DataControlRowState.Normal Then
                            Dim lbEdit As LinkButton = e.Row.Controls(0).Controls(0)
                            lbEdit.Text = "Add New Row"
                            Dim lbDelete As LinkButton = e.Row.Controls(0).Controls(2)
                            lbDelete.Text = ""
                            lbDelete.Enabled = False
                        ElseIf e.Row.RowState - DataControlRowState.Edit >= 0 Then
                            Dim lbUpdate As LinkButton = e.Row.Controls(0).Controls(0)
                            lbUpdate.Text = "Insert Row"
                            'Dim lbDelete As LinkButton = e.Row.Controls(0).Controls(1)
                            'lbDelete.Text = ""
                            'lbDelete.Enabled = False
                            Dim lbCancel As LinkButton = e.Row.Controls(0).Controls(2)
                            lbCancel.Text = "Cancel"

                        End If

                        Dim fixit As String = indexfxed

                        While Len(fixit) > 1

                            Dim x As String = Left(fixit, InStr(fixit, ","))
                            Dim y As String = Len(fixit) - Left(fixit, InStr(fixit, ","))
                            e.Row.Cells(Left(fixit, InStr(fixit, ",") - 1) + 1).Enabled = False

                            fixit = Right(fixit, Len(fixit) - InStr(fixit, ","))

                        End While
                    Else
                        Dim fixit As String = fxed

                        While Len(fixit) > 1

                            Dim x As String = Left(fixit, InStr(fixit, ","))
                            Dim y As String = Len(fixit) - Left(fixit, InStr(fixit, ","))
                            e.Row.Cells(Left(fixit, InStr(fixit, ",") - 1) + 1).Enabled = False

                            fixit = Right(fixit, Len(fixit) - InStr(fixit, ","))

                        End While
                    End If
                Else
                    Dim fixit As String = fxed

                    While Len(fixit) > 1

                        Dim x As String = Left(fixit, InStr(fixit, ","))
                        Dim y As String = Len(fixit) - Left(fixit, InStr(fixit, ","))
                        e.Row.Cells(Left(fixit, InStr(fixit, ",") - 1) + 1).Enabled = False

                        fixit = Right(fixit, Len(fixit) - InStr(fixit, ","))

                    End While
                End If
            End If
               

        Catch

        End Try

    End Sub

    Private Sub gvLUTable_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvLUTable.RowDeleting
        Try
            Dim columnArray As ArrayList = New ArrayList
            Dim keyArray As ArrayList = New ArrayList
            Dim SQLUpdate As String = ""
            Dim SQLWhere As String = ""

            ChelseaRow = 0
            gvLUTable.DataSource = SearchView
            'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvLUTable.DataBind()

            For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                columnArray.Add(DirectCast(ViewState("TBL"), DataTable).Columns.Item(i).ColumnName.ToString())
                'keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
            Next
            For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).PrimaryKey.Count.ToString - 1
                keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
            Next


            For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                If keyArray.Contains(DirectCast(ViewState("TBL"), DataTable).Columns(i).ToString) Then
                    SQLWhere = SQLWhere & columnArray(i).ToString & " = '" & e.Values.Item(columnArray(i)).ToString & "' and "
                End If
            Next



            If SQLUpdate <> "" Then
                SQLUpdate = Mid(SQLUpdate, 1, Len(SQLUpdate) - 2)
            End If

            'If DirectCast(ViewState("TBL"), DataTable).Constraints.Count > 0 Then
            '    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Constraints.Count - 1
            '        SQLWhere = SQLWhere & DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString & " = '" & DirectCast(ViewState("TBL"), DataTable).Rows(e.RowIndex).Item(0).ToString & "' and "

            '    Next
            'End If

            If SQLWhere <> "" Then
                SQLWhere = Mid(SQLWhere, 1, Len(SQLWhere) - 4)
            End If

            Dim cmd As SqlCommand
            SQLUpdate = "Update " & ddlSelectedTable.SelectedValue.ToString & " set Active = '0', LastUpdated = getdate() where " & SQLWhere
            cmd = New SqlCommand(SQLUpdate, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteReader()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        Finally
            gvLUTable.EditIndex = -1
            ChelseaRow = 0
            gvLUTable.DataSource = SearchView
            'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvLUTable.DataBind()
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            LoadGrid()

        End Try
    End Sub

    Private Sub gvLUTable_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvLUTable.RowEditing
        Try
            ChelseaRow = 0
            gvLUTable.EditIndex = e.NewEditIndex
            gvLUTable.DataSource = SearchView
            'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvLUTable.DataBind()

            'Dim keyArray As ArrayList = New ArrayList

            'For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).PrimaryKey.Count.ToString - 1
            '    keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
            'Next


            'For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
            '    If keyArray.Contains(DirectCast(ViewState("TBL"), DataTable).Columns(i).ToString) Then

            '        DirectCast(ViewState("TBL"), DataTable).Columns(i).ReadOnly = True
            '    Else
            '        DirectCast(ViewState("TBL"), DataTable).Columns(i).ReadOnly = False
            '    End If
            'Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLUTable_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvLUTable.RowUpdating
        Try

            Dim columnArray As ArrayList = New ArrayList
            Dim keyArray As ArrayList = New ArrayList
            Dim SQLUpdate As String = ""
            Dim SQLWhere As String = ""
            Dim n As String = SearchView.Sort

            Dim temp As String = e.NewValues(0)
            If temp = "1" Or temp = 1 Then
                Try

                    ChelseaRow = 0
                    gvLUTable.DataSource = SearchView
                    'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
                    gvLUTable.DataBind()

                    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                        columnArray.Add(DirectCast(ViewState("TBL"), DataTable).Columns.Item(i).ColumnName.ToString())
                        'keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
                    Next
                    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).PrimaryKey.Count.ToString - 1
                        keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
                    Next


                    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                        If columnArray(i).ToString = "ChelseaOrderingColumn" Then
                        ElseIf columnArray(i).ToString = "LastUpdated" Then
                            SQLUpdate = SQLUpdate & columnArray(i).ToString & " = getdate(), "
                        Else
                            If keyArray.Contains(DirectCast(ViewState("TBL"), DataTable).Columns(i).ToString) Then
                                SQLWhere = SQLWhere & columnArray(i).ToString & " = '" & e.NewValues.Item(columnArray(i)).ToString & "' and "
                            Else
                                SQLUpdate = SQLUpdate & columnArray(i).ToString & " = '" & e.NewValues.Item(columnArray(i)).ToString & "', "
                            End If
                        End If

                    Next



                    If SQLUpdate <> "" Then
                        SQLUpdate = Mid(SQLUpdate, 1, Len(SQLUpdate) - 2)
                    End If

                    'If DirectCast(ViewState("TBL"), DataTable).Constraints.Count > 0 Then
                    '    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Constraints.Count - 1
                    '        SQLWhere = SQLWhere & DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString & " = '" & DirectCast(ViewState("TBL"), DataTable).Rows(e.RowIndex).Item(0).ToString & "' and "

                    '    Next
                    'End If

                    If SQLWhere <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, Len(SQLWhere) - 4)
                    End If

                    Dim cmd As SqlCommand
                    SQLUpdate = "Update " & ddlSelectedTable.SelectedValue.ToString & "  set  " & SQLUpdate & "  where " & SQLWhere
                    cmd = New SqlCommand(SQLUpdate, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()

                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                Finally
                    ChelseaRow = 0
                    gvLUTable.EditIndex = -1
                    gvLUTable.DataSource = SearchView
                    'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
                    gvLUTable.DataBind()
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    LoadGrid()
                    SearchView.Sort = n
                    gvLUTable.DataSource = SearchView
                    gvLUTable.DataBind()


                End Try

            Else
                Try
                    ChelseaRow = 0
                    gvLUTable.DataSource = SearchView
                    'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
                    gvLUTable.DataBind()

                    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                        columnArray.Add(DirectCast(ViewState("TBL"), DataTable).Columns.Item(i).ColumnName.ToString())
                        'keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
                    Next
                    'For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).PrimaryKey.Count.ToString - 1
                    '    keyArray.Add(DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString)
                    'Next

                    Dim fixit As String

                    Dim isit As Integer




                    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Columns.Count.ToString - 1
                        If columnArray(i).ToString = "ChelseaOrderingColumn" Then
                        ElseIf columnArray(i).ToString = "Active" Then
                            SQLWhere = SQLWhere & columnArray(i).ToString & ", "
                            SQLUpdate = SQLUpdate & " '1', "
                        ElseIf columnArray(i).ToString = "LastUpdated" Then
                            SQLWhere = SQLWhere & columnArray(i).ToString & ", "
                            SQLUpdate = SQLUpdate & " getdate(), "
                        Else
                            isit = 0
                            fixit = indexfxed
                            While Len(fixit) > 1

                                Dim x As String = Left(fixit, InStr(fixit, ","))
                                Dim y As String = Len(fixit) - Left(fixit, InStr(fixit, ","))
                                If i = Left(fixit, InStr(fixit, ",") - 1) Then
                                    isit = 1
                                End If

                                fixit = Right(fixit, Len(fixit) - InStr(fixit, ","))

                            End While
                            If isit = 0 Then
                                SQLWhere = SQLWhere & columnArray(i).ToString & ", "
                                SQLUpdate = SQLUpdate & " '"
                                Try
                                    SQLUpdate += e.NewValues.Item(columnArray(i)).ToString() & "', "
                                Catch
                                    SQLUpdate += "', "
                                End Try

                            End If

                        End If

                    Next



                    If SQLUpdate <> "" Then
                        SQLUpdate = Mid(SQLUpdate, 1, Len(SQLUpdate) - 2)
                    End If

                    'If DirectCast(ViewState("TBL"), DataTable).Constraints.Count > 0 Then
                    '    For i As Integer = 0 To DirectCast(ViewState("TBL"), DataTable).Constraints.Count - 1
                    '        SQLWhere = SQLWhere & DirectCast(ViewState("TBL"), DataTable).PrimaryKey(i).ToString & " = '" & DirectCast(ViewState("TBL"), DataTable).Rows(e.RowIndex).Item(0).ToString & "' and "

                    '    Next
                    'End If

                    If SQLWhere <> "" Then
                        SQLWhere = Mid(SQLWhere, 1, Len(SQLWhere) - 2)
                    End If

                    Dim cmd As SqlCommand
                    SQLUpdate = "Insert into " & ddlSelectedTable.SelectedValue.ToString & " (" & SQLWhere & ") Values (" & SQLUpdate & ")"
                    cmd = New SqlCommand(SQLUpdate, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()

                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                Finally
                    ChelseaRow = 0
                    gvLUTable.EditIndex = -1
                    gvLUTable.DataSource = SearchView
                    'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
                    gvLUTable.DataBind()
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    LoadGrid()
                    SearchView.Sort = n
                    gvLUTable.DataSource = SearchView
                    gvLUTable.DataBind()
                End Try

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvLUTable_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvLUTable.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = SearchView

            sorts = e.SortExpression

            If e.SortExpression = Searchmap Then

                If Searchdir = 1 Then
                    dv.Sort = "ChelseaOrderingColumn asc, " + sorts + " " + "desc"
                    Searchdir = 0
                Else
                    dv.Sort = "ChelseaOrderingColumn asc, " + sorts + " " + "asc"
                    Searchdir = 1
                End If

            Else
                dv.Sort = "ChelseaOrderingColumn asc, " + sorts + " " + "asc"
                Searchdir = 1
                Searchmap = e.SortExpression
            End If

            ChelseaRow = 0
            gvLUTable.DataSource = SearchView
            'gvLUTable.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvLUTable.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnExportLUTable_Click(sender As Object, e As EventArgs) Handles btnExportLUTable.Click
        'Create datatable and populate with SQLDatasource 
        Dim dt As New DataTable(ddlSelectedTable.SelectedItem.Text)
        Dim args As New DataSourceSelectArguments()
        Dim view As DataView = SearchView
        dt = view.ToTable()


        Using wb As New XLWorkbook()
            wb.Worksheets.Add(dt)
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=" & ddlSelectedTable.SelectedItem.Text & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.[End]()
            End Using
        End Using
    End Sub
End Class