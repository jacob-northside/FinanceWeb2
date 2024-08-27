Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices

Imports FinanceWeb.WebFinGlobal

Public Class NZ_DataDictionary
    Inherits System.Web.UI.Page
    Dim SQL As String = ""
    Dim SQL_Clause As String = ""
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd As SqlCommand
    Dim cmdNS1 As SqlCommand
    Dim dr As SqlDataReader
    Dim SchemaTableName As String = "DWH.DOC.NZ_FDSCHEMA"
    Dim TablesTableName As String = "DWH.DOC.NZ_FDTABLES"
    Dim ColumnsTableName As String = "DWH.DOC.NZ_FDCOLUMNDATA"
    Dim MappingTableName As String = "DWH.DOC.NZ_FDMAPPING"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then

            Else
                If User.Identity.IsAuthenticated = True Then
                    btnLoadData.Visible = True
                    ddlDD_Tables.Visible = True
                    Label1.Text = "Available Description Tables: "
                    DDExcelLink.Visible = True
                Else
                    btnLoadData.Visible = False
                    ddlDD_Tables.Visible = False
                    Label1.Text = "Log in to see available tables."
                End If

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub btnLoadData_Click(sender As Object, e As System.EventArgs) Handles btnLoadData.Click
        Try

            Update_Mapping.EditIndex = -1
            Update_Schema.EditIndex = -1
            Update_Tables.EditIndex = -1
            Update_Columns.EditIndex = -1
            ' lblGVLookup.Text = ddlDD_Tables.SelectedValue.ToString '' Just the table name label
            LoadLookUpGrid(ddlDD_Tables.SelectedItem.ToString)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadLookUpGrid(TabletoShow As String, Optional SchemaFilter As String = "", Optional TableFilter As String = "")
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter
            Dim SelectedGridView As GridView
            Dim SelectedTable As String = ""
            SQL_Clause = ""

            ' Find out what table we selected and show it, hide the others ''
            If InStr(1, TabletoShow, "Schema", vbTextCompare) Then       '' FILL OUT AFTER ADDING OTHER TABLES ''
                SelectedGridView = Update_Schema
                SelectedTable = SchemaTableName
                Update_Schema.Visible = True
                PanelMapping.Visible = False
                PanelTables.Visible = False
                PanelColumns.Visible = False
            ElseIf InStr(1, TabletoShow, "Tables", vbTextCompare) Then
                SelectedGridView = Update_Tables
                SelectedTable = TablesTableName
                Update_Schema.Visible = False
                PanelColumns.Visible = False
                PanelTables.Visible = True
                PanelMapping.Visible = False
                ReturntoSchemaBtn.Visible = True
                ReturntoTablesBtn.Visible = False
                If SchemaFilter <> "" Then
                    SQL_Clause = " Where schemaname = '" & SchemaFilter & "'"
                End If
            ElseIf InStr(1, TabletoShow, "ColumnData", vbTextCompare) Then
                SelectedGridView = Update_Columns
                SelectedTable = ColumnsTableName
                PanelColumns.Visible = True
                PanelTables.Visible = False
                PanelMapping.Visible = False
                Update_Schema.Visible = False
                ReturntoSchemaBtn.Visible = False
                ReturntoTablesBtn.Visible = True
            ElseIf InStr(1, TabletoShow, "Mapping", vbTextCompare) Then
                SelectedGridView = Update_Mapping
                SelectedTable = MappingTableName
                Update_Schema.Visible = False
                PanelColumns.Visible = False
                PanelTables.Visible = False
                PanelMapping.Visible = True
                ReturntoSchemaBtn.Visible = False
                ReturntoTablesBtn.Visible = True
            End If

            SQL = "Select * from " & SelectedTable

            ' Add in quick sorting of schemas '
            Debug.Print("ARE WE HARE")
            If SelectedTable = "DWH.DOC.NZ_FDTABLES" Then
                'SQL = SQL & " where 1=1 "     ' visible_in_dd = 1"
                If Tables_EmptyFilter.Checked = True Then
                    SQL = SQL & " and tabledesc is NULL"
                End If
            ElseIf SelectedTable = "DWH.DOC.NZ_FDCOLUMNDATA" Then
                'SQL = SQL & " a inner join dwh.doc.NZ_FDMapping b on a.id = b.ColumnDataID inner join dwh.doc.Nz_FDTables c on b.dBName = c.dBName and b.TableName = c.TableName and b.SchemaName = c.SchemaName where visible_in_dd = 1 "
                If Columns_EmptyFilter.Checked = True Then
                    SQL = SQL & " and columndesc is NULL order by upper(a.columnname)"
                Else
                    SQL = SQL & " order by upper(columnname)"
                End If
            ElseIf SelectedTable = "DWH.DOC.NZ_FDMAPPING" Then
                ' Want to include column definitions in the Mapping
                SQL = SQL & " a Inner join DWH.DOC.NZ_FDColumnData b on a.columndataid = b.id inner join dwh.doc.Nz_FDTables c on a.dBName = c.dBName and a.TableName = c.TableName and a.SchemaName = c.SchemaName"
            End If

            If TableFilter <> "" Then
                SQL_Clause = " Where a.tablename = '" & TableFilter & "'"
            End If

            If SQL_Clause <> "" Then
                SQL = SQL & SQL_Clause
            End If
            If SelectedTable = "DWH.DOC.NZ_FDMAPPING" Then
                SQL = SQL + " order by a.SchemaName, a.TableName, a.ColumnName;"
            ElseIf SelectedTable = "DWH.DOC.NZ_FDTABLES" Then
                SQL = SQL & " ORDER BY SCHEMANAME, TABLENAME;"
            End If


                Debug.Print(SQL)
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    ds = New DataSet                                ' Get blank dataset to store our data
                    da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                    da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                    da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                    SelectedGridView.DataSource = ds
                SelectedGridView.DataMember = "LookUpData"
                SelectedGridView.DataBind()

                ' Set up View States ' 
                If SelectedTable = "DWH.DOC.NZ_FDSCHEMA" Then
                    ViewState("Update_Schema") = ds
                ElseIf SelectedTable = "DWH.DOC.NZ_FDTABLES" Then
                    ViewState("Update_Tables") = ds
                ElseIf SelectedTable = "DWH.DOC.NZ_FDCOLUMNDATA" Then
                    ViewState("Update_Columns") = ds
                ElseIf SelectedTable = "DWH.DOC.NZ_FDMAPPING" Then
                    ' ViewState("Update_Mapping") = ds


                End If


            End Using
                'lblGVLookup.Text = lblGVLookup.Text & "  - " & gvDWHLookups.Rows.Count.ToString & " rows. "


        Catch ex As Exception
            Debug.Print("LOAD LOOKUPGRID ERROR")
            Debug.Print(ex.ToString)
            LogWebFinError(ddlDD_Tables.SelectedValue.ToString & Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub Update_Schema_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_Schema.RowEditing
        Try
            Dim Schema_Active As DropDownList = Update_Schema.Rows(e.NewEditIndex).FindControl("Schema_Active")
            Update_Schema.EditIndex = e.NewEditIndex
            Update_Schema.DataSource = ViewState("Update_Schema")
            Update_Schema.DataBind()

            'LoadLookUpGrid("Schemas")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub Update_Schema_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_Schema.RowCancelingEdit
        Try
            Update_Schema.EditIndex = -1
            Update_Schema.DataSource = ViewState("Update_Schema")
            Update_Schema.DataBind()
            'LoadLookUpGrid("Schemas")
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub Update_Schema_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_Schema.RowUpdating

        Try
            Dim Update_Fields(4) As String
            Dim schemaname As LinkButton = Update_Schema.Rows(e.RowIndex).FindControl("Update_Schema_Name")
            Dim UpdateSchemaDesc As TextBox = Update_Schema.Rows(e.RowIndex).FindControl("SchemaDesc_EditBox")
            Dim Schema_Active As DropDownList = Update_Schema.Rows(e.RowIndex).FindControl("Schema_Active")

            Update_Fields(0) = Update_Schema.Rows(e.RowIndex).Cells(1).Text  '' DB Name
            Update_Fields(1) = schemaname.Text
            Update_Fields(2) = UpdateSchemaDesc.Text
            Update_Fields(3) = Schema_Active.Text

            Dim UpdateString As String

            ' Changing to parameterized queries to prevent SQL injection
            UpdateString = "Update DWH.DOC.NZ_FDSchema Set SchemaDesc = @SCHEMADESC, visible_in_dd = @VID" & _
                           " where dbname = @DBNAME and schemaname = @SCHEMANAME;"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)
                UpdateCommand.Parameters.Add("@DBNAME", SqlDbType.VarChar, 100).Value = Update_Fields(0) ' May need to remove single quotes that get added earlier?
                UpdateCommand.Parameters.Add("@SCHEMANAME", SqlDbType.VarChar, 100).Value = Update_Fields(1)
                UpdateCommand.Parameters.Add("@SCHEMADESC", SqlDbType.VarChar, 2000).Value = Update_Fields(2)
                UpdateCommand.Parameters.Add("@VID", SqlDbType.VarChar, 1).Value = Update_Fields(3)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                UpdateCommand.ExecuteNonQuery()
                conn.Close()
            End Using
            Update_Schema.EditIndex = -1
            'Update_Schema.DataBind()

            LoadLookUpGrid("Schemas")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Protected Sub Update_Schema_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Update_Schema.RowCommand
        Try
            If e.CommandName = "ViewFilteredData" Then
                Dim lnks As LinkButton = e.CommandSource
                Update_Mapping.EditIndex = -1
                Update_Schema.EditIndex = -1
                Update_Tables.EditIndex = -1
                Update_Columns.EditIndex = -1

                tbl_SearchSchemas.Text = lnks.Text
                LoadLookUpGrid("Tables", lnks.Text)

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub Update_Tables_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_Tables.RowEditing
        Try
            Update_Tables.EditIndex = e.NewEditIndex
            Update_Tables.DataSource = ViewState("Update_Tables") 'DirectCast(ViewState("TBL"), DataTable)

            Update_Tables.DataBind()

            'LoadLookUpGrid("Tables")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub Update_Tables_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_Tables.RowCancelingEdit
        Try

            Update_Tables.EditIndex = -1
            Update_Tables.DataSource = ViewState("Update_Tables") 'DirectCast(ViewState("TBL"), DataTable)
            Update_Tables.DataBind()

            'LoadLookUpGrid("Tables")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub Update_Tables_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_Tables.RowUpdating

        Try
            Dim Update_Fields(4) As String
            Dim tablename As LinkButton = Update_Tables.Rows(e.RowIndex).FindControl("Update_Table_Name")
            Dim UpdateTableDesc As TextBox = Update_Tables.Rows(e.RowIndex).FindControl("TableDesc_EditBox")
            Dim Table_Active As DropDownList = Update_Tables.Rows(e.RowIndex).FindControl("Tables_Active_DDL")

            Update_Fields(0) = Update_Tables.Rows(e.RowIndex).Cells(1).Text  '' DB Name
            Update_Fields(1) = Update_Tables.Rows(e.RowIndex).Cells(2).Text  '' SchemaName
            Update_Fields(2) = tablename.Text
            Update_Fields(3) = UpdateTableDesc.Text
            Update_Fields(4) = Table_Active.Text

            Dim UpdateString As String
            UpdateString = "Update DWH.DOC.NZ_FDTABLES Set TableDesc = @TABLEDESC, visible_in_dd = @VID where dbname = @DBNAME and schemaname = @SCHEMANAME and tablename = @TABLENAME;"

            ' Take out below and sub in LoadGridView();
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)
                UpdateCommand.Parameters.Add("@TABLEDESC", SqlDbType.VarChar, 2000).Value = Update_Fields(3)
                UpdateCommand.Parameters.Add("@VID", SqlDbType.VarChar, 1).Value = Update_Fields(4)
                UpdateCommand.Parameters.Add("@DBNAME", SqlDbType.VarChar, 100).Value = Update_Fields(0)
                UpdateCommand.Parameters.Add("@TABLENAME", SqlDbType.VarChar, 255).Value = Update_Fields(2)
                UpdateCommand.Parameters.Add("@SCHEMANAME", SqlDbType.VarChar, 100).Value = Update_Fields(1)


                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                UpdateCommand.ExecuteNonQuery()
                conn.Close()
            End Using
            Update_Tables.EditIndex = -1
            Update_Tables.DataSource = ViewState("Update_Tables")
            Update_Tables.DataBind()

            LoadLookUpGrid("Tables")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Protected Sub Update_Tables_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Update_Tables.RowCommand
        Try
            If e.CommandName = "ViewFilteredData" Then
                Dim lnks As LinkButton = e.CommandSource
                Update_Mapping.EditIndex = -1
                Update_Schema.EditIndex = -1
                Update_Tables.EditIndex = -1
                Update_Columns.EditIndex = -1

                ' Before loading the new view enter the table name in the search bar, makes the paging work for filtered daat
                txtTblSearchTableName.Text = lnks.Text


                LoadLookUpGrid("Mapping", , lnks.Text)
                ' lblTableSelected.Text = lnks.Text
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub Update_Columns_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_Columns.RowEditing
        Try
            Update_Columns.EditIndex = e.NewEditIndex
            Debug.Print(Update_Columns.EditIndex & " > " & e.NewEditIndex)
            Update_Columns.DataSource = ViewState("Update_Columns")   'DirectCast(ViewState("Update_Columns"), DataTable)

            Update_Columns.DataBind()

            'LoadLookUpGrid("ColumnData")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub Update_Columns_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_Columns.RowCancelingEdit
        Try

            Update_Columns.EditIndex = -1
            Update_Columns.DataSource = DirectCast(ViewState("TBL"), DataTable)
            Update_Columns.DataBind()

            LoadLookUpGrid("ColumnData")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub Update_Columns_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_Columns.RowUpdating

        Try
            Dim Update_Fields(2) As String
            Dim UpdateColumnDesc As TextBox = Update_Columns.Rows(e.RowIndex).FindControl("ColumnDesc_EditBox")

            Update_Fields(0) = Update_Columns.Rows(e.RowIndex).Cells(1).Text '' ID
            Update_Fields(1) = UpdateColumnDesc.Text

            Dim UpdateString As String
            UpdateString = "Update DWH.DOC.NZ_FDColumnData Set ColumnDesc = @COLMNDESC where ID = @ID;"
            Debug.Print(UpdateString)

            ' Take out below and sub in LoadGridView();
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)
                UpdateCommand.Parameters.Add("@COLMNDESC", SqlDbType.VarChar, 2000).Value = Update_Fields(1)
                UpdateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = Update_Fields(0)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                UpdateCommand.ExecuteNonQuery()
                conn.Close()
            End Using

            Update_Columns.EditIndex = -1
            Update_Columns.DataBind()
            LoadLookUpGrid("ColumnData")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub Update_ColumnData_InsertTemplate() Handles ColumnData_AddRow_Btn.Click




    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Protected Sub Update_Mapping_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_Mapping.RowEditing
        Try
            Update_Mapping.EditIndex = e.NewEditIndex
            Update_Mapping.DataSource = ViewState("Update_Mapping") ' DirectCast(ViewState("TBL"), DataTable)

            Update_Mapping.DataBind()

            'LoadLookUpGrid("Mapping")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub Update_Mapping_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_Mapping.RowCancelingEdit
        Try

            Update_Mapping.EditIndex = -1
            Update_Mapping.DataSource = ViewState("Update_Mapping") ' DirectCast(ViewState("TBL"), DataTable)

            Update_Mapping.DataBind()

            'LoadLookUpGrid("Mapping")

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub Update_Mapping_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_Mapping.RowUpdating

        Try
            Dim UpdateColumnDataID As TextBox = Update_Mapping.Rows(e.RowIndex).Cells(5).Controls(0)
            'Dim UpdateMappingActive As TextBox = Update_Mapping.Rows(e.RowIndex).Cells(6).Controls(0)
            Dim UpdateString As String
            UpdateString = "Update DWH.DOC.NZ_FDMAPPING Set ColumnDataID = @COLID where ID = @ID;"
            Debug.Print(UpdateString)

            ' Take out below and sub in LoadGridView();
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)

                UpdateCommand.Parameters.Add("@COLID", SqlDbType.Int).Value = UpdateColumnDataID.Text
                'UpdateCommand.Parameters.Add("@ACTIVE", SqlDbType.Int).Value = Update_Fields(2)
                UpdateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = Update_Mapping.Rows(e.RowIndex).Cells(1).Text '' ID

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                UpdateCommand.ExecuteNonQuery()
                conn.Close()
            End Using

            Update_Mapping.EditIndex = -1
            Update_Mapping.DataBind()

            LoadLookUpGrid("Mapping")

        Catch ex As Exception
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Protected Sub Schema_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Update_Schema.PageIndexChanging
        Update_Schema.PageIndex = e.NewPageIndex
        Update_Schema.DataBind()
        Search(sender, e)
        LoadLookUpGrid("Schemas")
    End Sub
    Protected Sub Tables_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Update_Tables.PageIndexChanging
        Update_Tables.PageIndex = e.NewPageIndex
        Update_Tables.DataBind()
        Search(sender, e)
        'LoadLookUpGrid("Tables")
    End Sub
    Protected Sub Columns_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Update_Columns.PageIndexChanging
        Update_Columns.PageIndex = e.NewPageIndex
        Update_Columns.DataBind()
        Search(sender, e)
        'LoadLookUpGrid("ColumnData")
    End Sub
    Protected Sub Mapping_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Update_Mapping.PageIndexChanging
        Update_Mapping.PageIndex = e.NewPageIndex
        Update_Mapping.DataBind()
        Search(sender, e)
        'LoadLookUpGrid("Mapping") CHECK IN HERE
    End Sub

    Protected Sub ReturntoSchemaBtn_Click(sender As Object, e As EventArgs) Handles ReturntoSchemaBtn.Click
        ReturntoSchemaBtn.Visible = False
        ReturntoTablesBtn.Visible = False
        LoadLookUpGrid("Schema")
    End Sub
    Protected Sub ReturntoTablesBtn_Click(sender As Object, e As EventArgs) Handles ReturntoTablesBtn.Click
        ReturntoSchemaBtn.Visible = True
        ReturntoTablesBtn.Visible = False
        LoadLookUpGrid("Tables")
    End Sub

    Private Sub Search(sender As Object, e As System.EventArgs) Handles txtTblSearchID.TextChanged, txtTblSearchSchemaName.TextChanged, txtTblSearchTableName.TextChanged, txtTblSearchColumnName.TextChanged, tbl_SearchSchemas.TextChanged, tbl_SearchTables.TextChanged, Cols_SearchColumnName.TextChanged, Cols_SearchID.TextChanged
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim search_mapping_sql As String = ""
        Dim search_schema_sql As String = ""
        Dim search_column_sql As String = ""
        Dim SearchCMD As SqlCommand

        Try
            Dim filter As String = ""

            '''''''''''''''''''''''''''''''MAPPING SEARCH'''''''''''''''''''''''''''''''

            If PanelMapping.Visible = True Then
                If Mapping_Srch_RadioBtn.SelectedValue = "Wildcard" Then
                    search_mapping_sql = "Select * from DWH.DOC.NZ_FDMAPPING WHERE ID like '%' + @TBLSEARCHID +'%' and SchemaName like '%' + @SEARCHSCHEMANAME +'%' and TableName like '%' + @SEARCHTABLENAME + '%' and ColumnName like '%' + @SEARCHCOLNAME + '%'"
                Else
                    search_mapping_sql = "Select * from DWH.DOC.NZ_FDMAPPING WHERE = @TBLSEARCHID and SchemaName = @SEARCHSCHEMANAME and TableName = @SEARCHTABLENAME and ColumnName = @SEARCHCOLNAME"
                End If
                search_mapping_sql += " ORDER BY SCHEMANAME, TABLENAME;"



                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    'SearchCMD = New SqlCommand(search_mapping_sql, conn)
                    'SearchCMD.Parameters.Add("@TBLSEARCHID", SqlDbType.VarChar, 32).Value = txtTblSearchID.Text
                    'SearchCMD.Parameters.Add("@SEARCHSCHEMANAME", SqlDbType.VarChar, 100).Value = txtTblSearchSchemaName.Text
                    'SearchCMD.Parameters.Add("@SEARCHTABLENAME", SqlDbType.VarChar, 255).Value = txtTblSearchTableName.Text
                    'SearchCMD.Parameters.Add("@SEARCHCOLNAME", SqlDbType.VarChar, 255).Value = txtTblSearchColumnName.Text

                    ds = New DataSet                                ' Get blank dataset to store our data
                    da = New SqlDataAdapter(search_mapping_sql, conn)              ' New connection and our select command
                    da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                    da.SelectCommand.Parameters.Add("@TBLSEARCHID", SqlDbType.VarChar, 32).Value = txtTblSearchID.Text
                    da.SelectCommand.Parameters.Add("@SEARCHSCHEMANAME", SqlDbType.VarChar, 100).Value = txtTblSearchSchemaName.Text
                    da.SelectCommand.Parameters.Add("@SEARCHTABLENAME", SqlDbType.VarChar, 255).Value = txtTblSearchTableName.Text
                    da.SelectCommand.Parameters.Add("@SEARCHCOLNAME", SqlDbType.VarChar, 255).Value = txtTblSearchColumnName.Text

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                    conn.Close()

                    Update_Mapping.DataSource = ds
                    Update_Mapping.DataMember = "LookUpData"

                    Update_Mapping.DataBind()
                    ViewState("Update_Mapping") = ds
                    conn.Close()
                    ds.Dispose()
                    da.Dispose()
                End Using

            ElseIf PanelTables.Visible = True Then
                '''''''''''''''''''''''''''''''TABLE SEARCH'''''''''''''''''''''''''''''''
                If Tables_Srch_RadioBtn.SelectedValue = "Wildcard" Then
                    search_schema_sql = "Select * from DWH.DOC.NZ_FDTABLES WHERE SchemaName like '%' + @SCHEMANAME + '%' and TableName like '%' + @TABLENAME + '%'"
                Else
                    search_schema_sql = "Select * from DWH.DOC.NZ_FDTABLES WHERE SchemaName = @SCHEMANAME and TableName = @TABLENAME"
                End If

                If Tables_EmptyFilter.Checked = True Then
                    search_schema_sql += " and tabledesc is NULL"
                End If
                search_schema_sql += " ORDER BY SCHEMANAME, TABLENAME"

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    ds = New DataSet                                ' Get blank dataset to store our data
                    da = New SqlDataAdapter(search_schema_sql, conn)              ' New connection and our select command
                    da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                    da.SelectCommand.Parameters.Add("@TABLENAME", SqlDbType.VarChar, 255).Value = tbl_SearchTables.Text
                    da.SelectCommand.Parameters.Add("@SCHEMANAME", SqlDbType.VarChar, 100).Value = tbl_SearchSchemas.Text

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?

                    Update_Tables.DataSource = ds
                    Update_Tables.DataMember = "LookUpData"

                    Update_Tables.DataBind()
                    ViewState("Update_Tables") = ds
                    conn.Close()
                    ds.Dispose()
                    da.Dispose()
                End Using
            ElseIf PanelColumns.Visible = True Then
                '''''''''''''''''''''''''''''''COLUMNDATA SEARCH'''''''''''''''''''''''''''''''
                If Columns_Srch_RadioBtn.SelectedValue = "Wildcard" Then
                    search_column_sql = "Select * from DWH.DOC.NZ_FDColumnData where ID like '%' + @COLSEARCHID + '%' and columnname like '%' + @COLNAME + '%' "
                Else
                    search_column_sql = "Select * from DWH.DOC.NZ_FDColumnData where ID = @COLSEARCHID and columnname = @COLNAME "
                End If

                If Columns_EmptyFilter.Checked = True Then
                    search_column_sql = search_column_sql & " and columndesc is NULL"
                End If
                search_column_sql += " order by columnname;"

                Debug.Print(search_column_sql)

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    SearchCMD = New SqlCommand(search_mapping_sql, conn)

                    ds = New DataSet                                ' Get blank dataset to store our data
                    da = New SqlDataAdapter(search_column_sql, conn)              ' New connection and our select command
                    da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                    da.SelectCommand.Parameters.Add("@COLSEARCHID", SqlDbType.VarChar, 32).Value = Cols_SearchID.Text
                    da.SelectCommand.Parameters.Add("@COLNAME", SqlDbType.VarChar, 255).Value = Cols_SearchColumnName.Text

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?

                    Update_Columns.DataSource = ds
                    Update_Columns.DataMember = "LookUpData"
                    Update_Columns.DataBind()

                    ViewState("Update_Columns") = ds

                    conn.Close()
                    ds.Dispose()
                    da.Dispose()
                End Using
            End If

        Catch ex As Exception
            Debug.Print("SEARCH ERROR")
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


End Class