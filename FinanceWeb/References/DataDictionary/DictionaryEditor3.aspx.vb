﻿Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Security.Principal

Imports FinanceWeb.WebFinGlobal

Public Class DictionaryEditor3
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim connNS1 As New SqlConnection(ConfigurationManager.ConnectionStrings("NS1conn").ConnectionString)
    Dim cmd As SqlCommand
    Dim cmdNS1 As SqlCommand
    Public Shared dv As New DataView


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                lblUser.Text = Page.User.Identity.Name
                lblUser.Visible = False

                If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then
                    chbSchemaInactive.Visible = True
                    chbTablesInactive.Visible = True
                    chbColumnsInactive.Visible = True
                Else
                End If

                LoadDatabaseGV()

                Try


                    If Request.QueryString("DataBase") IsNot Nothing Then
                        lblDatabaseSelected.Text = Replace(Request.QueryString("DataBase"), "'", "''")


                        dv.Sort = "Name"
                        Dim ind As Integer = dv.Find(lblDatabaseSelected.Text)
                        Dim rw As DataRow = dv.Table(ind)

                        If Trim(rw(1).ToString) = "" Then
                            lblSchemaDatabase.Text = lblDatabaseSelected.Text & ": No description available at this time "
                        Else
                            lblSchemaDatabase.Text = lblDatabaseSelected.Text & ": " & rw(1).ToString
                        End If

                        lblTableDatabase.Text = lblSchemaDatabase.Text
                        lblColumnDatabase.Text = lblSchemaDatabase.Text

                        pnlDBDefinitions.Visible = False
                        pnlSchemaDefinitions.Visible = True

                        pnlDatabase.Visible = False
                        pnlSchema.Visible = True
                        pnlTables.Visible = False
                        pnlColumns.Visible = False
                        gvFDSchema.Visible = True
                        'gvFDSchemaNS1.Visible = False
                        LoadSchemaGV()
                    End If
                Catch ex As Exception
                    explantionlabel.Text = "Not a valid Database"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End Try

                Try

                    If Request.QueryString("Schema") IsNot Nothing Then
                        lblSchemaSelected.Text = Replace(Request.QueryString("Schema"), "'", "''")

                        'LoadSchemaGV()
                        dv.Sort = "TABLE_SCHEMA"
                        Dim ind As Integer = dv.Find(lblSchemaSelected.Text)
                        Dim rw As DataRow = dv.Table(ind)

                        If Trim(rw(4).ToString) = "" Then
                            lblTableSchema.Text = lblSchemaSelected.Text & ": No description available at this time "
                        Else
                            lblTableSchema.Text = lblSchemaSelected.Text & ": " & rw(4).ToString
                        End If
                        lblColumnSchema.Text = lblTableSchema.Text

                        pnlSchemaDefinitions.Visible = False
                        pnlTableDefinitions.Visible = True

                        pnlDatabase.Visible = False
                        pnlSchema.Visible = False
                        pnlTables.Visible = True
                        pnlColumns.Visible = False
                        gvFDTables.Visible = True
                        'gvFDTablesNS1.Visible = False
                        LoadTablesGV()
                    End If
                Catch ex As Exception
                    explantionlabel.Text = "Not a valid Schema"
                    ModalPopupExtender1.Show()
                End Try

                Try


                    If Request.QueryString("Table") IsNot Nothing Then

                        lblTableSelected.Text = Replace(Request.QueryString("Table"), "'", "''")
                        'lblTableSelected.Text = gvFDTables.DataKeys(e.CommandSource).Value.ToString

                        dv.Sort = "Table_name"
                        Dim ind As Integer = dv.Find(lblTableSelected.Text)
                        Dim rw As DataRow = dv.Table(ind)

                        If rw(2).ToString = "&nbsp;" Then
                            lblColumnTable.Text = lblTableSelected.Text & ": No description available at this time "
                        Else
                            lblColumnTable.Text = lblTableSelected.Text & ": " & rw(2).ToString
                        End If

                        pnlTableDefinitions.Visible = False
                        pnlColumnDefinitions.Visible = True

                        pnlDatabase.Visible = False
                        pnlSchema.Visible = False
                        pnlTables.Visible = False
                        pnlColumns.Visible = True

                        'If lblDatabaseSelected.Text = "MPA" Then
                        '    gvFDColumnsMPA.Visible = True
                        'Else
                        gvFDColumns.Visible = True
                        'End If
                        'gvFDColumnsNS1.Visible = False
                        LoadColumnsGV()

                        ColumnDir.Text = "asc"
                        ColumnSort.Text = "COLUMN_NAME"
                    End If
                Catch ex As Exception
                    explantionlabel.Text = "Not a valid Table"
                    ModalPopupExtender1.Show()
                End Try

                Try

                    If Request.QueryString("Column") IsNot Nothing Then
                        Dim filter As String = " COLUMN_NAME = '" & Replace(Request.QueryString("Column"), "'", "''") & "'"
                        dv.RowFilter = filter
                        gvFDColumns.EditIndex = -1
                        gvFDColumns.DataSource = dv
                        gvFDColumns.DataBind()

                    End If

                Catch ex As Exception
                    explantionlabel.Text = "Not a valid Column"
                    ModalPopupExtender1.Show()
                End Try
                'LoadDatabaseGV()
                'LoadDatabaseMPA()

            Else
                Page.MaintainScrollPositionOnPostBack = True
            End If

        Catch ex As Exception
            lblUser.Visible = False
            lblUser.Text = ex.ToString
        End Try
    End Sub
#Region "GVDatabase"
    Protected Sub gvFDdatabase_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDdatabase.RowCommand
        Try
            If e.CommandName = "ViewDatabase" Then
                'This was the original method of getting the correct name of the database and description 
                ' lblDatabaseSelected.Text = gvFDdatabase.Rows(e.CommandArgument).Cells(2).Text
                'If gvFDdatabase.Rows(e.CommandArgument).Cells(3).Text = "&nbsp;" Then
                '    lblSchemaDatabase.Text = gvFDdatabase.Rows(e.CommandArgument).Cells(2).Text & ": No description available at this time "
                'Else
                '    lblSchemaDatabase.Text = gvFDdatabase.Rows(e.CommandArgument).Cells(2).Text & ": " & gvFDdatabase.Rows(e.CommandArgument).Cells(3).Text
                'End If

                lblDatabaseSelected.Text = gvFDdatabase.DataKeys(e.CommandArgument).Value

                dv.Sort = "Name"
                Dim ind As Integer = dv.Find(lblDatabaseSelected.Text)
                Dim rw As DataRow = dv.Table(ind)

                If Trim(rw(1).ToString) = "" Then
                    lblSchemaDatabase.Text = lblDatabaseSelected.Text & ": No description available at this time "
                Else
                    lblSchemaDatabase.Text = lblDatabaseSelected.Text & ": " & rw(1).ToString
                End If

                lblTableDatabase.Text = lblSchemaDatabase.Text
                lblColumnDatabase.Text = lblSchemaDatabase.Text

                pnlDBDefinitions.Visible = False
                pnlSchemaDefinitions.Visible = True

                pnlDatabase.Visible = False
                pnlSchema.Visible = True
                pnlTables.Visible = False
                pnlColumns.Visible = False
                gvFDSchema.Visible = True
                'gvFDSchemaNS1.Visible = False
                LoadSchemaGV()
                '   pnlSchema.ScrollBars = ScrollBars.Auto
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadDatabaseGV(Optional sortDirection As String = "Name asc")
        Try
            If pnlDatabase.Visible = True Then
                Dim tbl As System.Data.DataTable = New DataTable()
                Dim keys(0) As DataColumn
                Dim col As New DataColumn("Name")

                keys(0) = col
                tbl.Columns.Add(col)
                col = New DataColumn("dBDesc")
                tbl.Columns.Add(col)
                col = New DataColumn("dBCaveat")
                tbl.Columns.Add(col)
                col = New DataColumn("dBOwner")
                tbl.Columns.Add(col)
                col = New DataColumn("dBAccess")
                tbl.Columns.Add(col)
                col = New DataColumn("LastUpdated")
                tbl.Columns.Add(col)
                col = New DataColumn("TechnicalDescription")
                tbl.Columns.Add(col)

                tbl.PrimaryKey = keys

                SQL = "SELECT a.Name," & _
                "b.dBDesc, b.dBCaveat, " & _
                "b.dBOwner, b.dBAccess,  convert(varchar(25),b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription " & _
                "FROM sys.sysdatabases a " & _
                "left outer join DWH.Doc.fddatabase b on (a.name COLLATE DATABASE_DEFAULT = b.dBName COLLATE DATABASE_DEFAULT ) " & _
                "WHERE(HAS_DBACCESS(name) = 1) " & _
                "and (Active = 1 or Active is null )  " & _
                                "order by " & sortDirection
                '  "and sid NOT IN (0x01,0x00)  " & _

                'SQL = "SELect * from " & _
                ' "(SELECT a.Name," & _
                ' "b.dBDesc, b.dBCaveat, " & _
                ' "b.dBOwner, b.dBAccess,  convert(varchar(25),b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription " & _
                ' "FROM sys.sysdatabases a " & _
                ' "left outer join DWH.Doc.fddatabase b on (a.name COLLATE DATABASE_DEFAULT = b.dBName COLLATE DATABASE_DEFAULT ) " & _
                ' "WHERE(HAS_DBACCESS(name) = 1) " & _
                ' "and (Active = 1 or Active is null )  " & _
                ' "and sid NOT IN (0x01,0x00)  " & _
                ' "union " & _
                ' "select " & _
                ' "dBName COLLATE DATABASE_DEFAULT as Name, dBDesc, dBCaveat, dBOwner, dBAccess, LastUpdated, TechnicalDescription " & _
                ' " from DWH.DOC.FDDatabase " & _
                ' "where dbName = 'MPA') x  " & _
                ' "order by " & sortDirection


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd = New SqlCommand(SQL, conn)
                    cmd.CommandTimeout = 86400
                    Dim drSQL As SqlDataReader
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    drSQL = cmd.ExecuteReader
                    Dim dr As DataRow
                    While drSQL.Read
                        dr = tbl.NewRow()
                        If IsDBNull(drSQL.Item("Name")) Then
                            dr("Name") = ""
                        Else
                            dr("Name") = drSQL.Item("Name")
                        End If
                        If IsDBNull(drSQL.Item("dBDesc")) Then
                            dr("dBDesc") = ""
                        Else
                            dr("dBDesc") = drSQL.Item("dBDesc")
                        End If
                        If IsDBNull(drSQL.Item("dBCaveat")) Then
                            dr("dBCaveat") = ""
                        Else
                            dr("dBCaveat") = drSQL.Item("dBCaveat")
                        End If
                        If IsDBNull(drSQL.Item("dBOwner")) Then
                            dr("dBOwner") = ""
                        Else
                            dr("dBOwner") = drSQL.Item("dBOwner")
                        End If
                        If IsDBNull(drSQL.Item("dBAccess")) Then
                            dr("dBAccess") = ""
                        Else
                            dr("dBAccess") = drSQL.Item("dBAccess")
                        End If
                        If IsDBNull(drSQL.Item("LastUpdated")) Then
                            dr("LastUpdated") = ""
                        Else
                            dr("LastUpdated") = drSQL.Item("LastUpdated")
                        End If
                        If IsDBNull(drSQL.Item("TechnicalDescription")) Then
                            dr("TechnicalDescription") = ""
                        Else
                            dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
                        End If
                        tbl.Rows.Add(dr)
                    End While
                    drSQL.Close()
                End Using

                gvFDdatabase.Visible = True

                ViewState.Add("TBL", tbl)
                dv = DirectCast(ViewState("TBL"), DataTable).DefaultView

                gvFDdatabase.EditIndex = -1
                gvFDdatabase.DataSource = dv
                gvFDdatabase.DataBind()

                If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

                Else
                    'gvFDdatabase.Columns.Item(0).Visible = False
                    tblDBSelect.Visible = False
                End If

            End If
        Catch ex As Exception
            lblUser.Visible = False
            lblUser.Text = ex.ToString

        End Try
    End Sub
    Protected Sub gvFDDatabase_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDdatabase.RowCancelingEdit
        Try

            gvFDdatabase.EditIndex = -1
            gvFDdatabase.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvFDdatabase.DataBind()
            tblDBSelect.Visible = True
            tblDBEdit.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDDatabase_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDdatabase.RowDeleting
        Try
            Dim DatabaseName As String = ""

            DatabaseName = gvFDdatabase.DataKeys(e.RowIndex).Value

            SQL = "DECLARE @rowcount int " & _
            "UPDATE [DWH].[DOC].[FDDatabase] SET " & _
            "Active = 0, " & _
            "LastUpdated = sysdateTime(), " & _
            "LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'   " & _
            "WHERE (dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "') " & _
            "set @rowcount = @@ROWCOUNT " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "INSERT INTO [DWH].[DOC].[FDDatabase]  " & _
            "([dBName],[dBDesc],[dBCaveat],[Active], [dbOwner], [dbAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
            "VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '', '', 0, '','', sysdatetime(), @UserID, @TechnicalDescription) " & _
            "END " & _
            "Update DWH.Doc.FDColumnData set  " & _
            "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'   " & _
            "WHERE   dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "Update DWH.Doc.FDTables set  " & _
            "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'   " & _
            "WHERE  dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "'  " & _
            "Update DWH.Doc.FDSchema set  " & _
            "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'   " & _
            "WHERE  dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' 	"


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDdatabase.EditIndex = -1
            LoadDatabaseGV()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDDatabase_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDdatabase.RowEditing
        Try
            gvFDdatabase.EditIndex = e.NewEditIndex
            gvFDdatabase.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvFDdatabase.DataBind()
            Dim txtDesc As TextBox = gvFDdatabase.Rows(e.NewEditIndex).FindControl("txtDBGVDescription")
            Dim lblDesc As Label = gvFDdatabase.Rows(e.NewEditIndex).FindControl("lblDBGVDescription")
            Dim txtTechDesc As TextBox = gvFDdatabase.Rows(e.NewEditIndex).FindControl("txtDBGVTechDesc")
            Dim lblTechDesc As Label = gvFDdatabase.Rows(e.NewEditIndex).FindControl("lblDBGVTechDesc")

            txtDesc.Visible = True
            lblDesc.Visible = False
            txtTechDesc.Visible = True
            lblTechDesc.Visible = False
            tblDBSelect.Visible = False
            tblDBEdit.Visible = True
            'gvFDdatabase.Columns(0).ItemStyle.Width = "100px"

            For Each canoe As GridViewRow In gvFDdatabase.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5EAFF")
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
    Protected Sub gvFDDatabase_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDdatabase.RowUpdating
        Try
            Dim DatabaseName As String = ""
            Dim Desc As Object
            Dim Caveat As String = ""
            Dim TechnicalDesc As String = ""
            Dim dBOwner As String = ""
            Dim dBAccess As String = ""

            DatabaseName = gvFDdatabase.DataKeys(e.RowIndex).Value

            Dim txtDesc As TextBox = gvFDdatabase.Rows(e.RowIndex).FindControl("txtDBGVDescription")
            Dim txtTechDesc As TextBox = gvFDdatabase.Rows(e.RowIndex).FindControl("txtDBGVTechDesc")

            Desc = txtDesc.Text
            Caveat = e.NewValues.Item("dBCaveat")
            TechnicalDesc = txtTechDesc.Text
            dBOwner = e.NewValues.Item("dbOwner")
            dBAccess = e.NewValues.Item("dBAccess")

            SQL = "DECLARE @rowcount int " & _
            "UPDATE [DWH].[DOC].[FDDatabase] SET " & _
            "dBDesc = '" & Replace(Desc, "'", "''") & "', dBCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
            "Active = 1, dbOwner = '" & Replace(dBOwner, "'", "''") & "', dbAccess = '" & Replace(dBAccess, "'", "''") & "', " & _
            "LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "', TechnicalDescription = '" & Replace(TechnicalDesc, "'", "''") & "'  " & _
            "WHERE (dBName =  '" & Replace(DatabaseName, "'", "''") & "') " & _
            "set @rowcount = @@ROWCOUNT " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "INSERT INTO [DWH].[DOC].[FDDatabase] " & _
            "([dBName],[dBDesc],[dBCaveat],[Active], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription]) " & _
            "   VALUES " & _
            "('" & Replace(DatabaseName, "'", "''") & "', '" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechnicalDesc, "'", "''") & "')" & _
            "END"

            '"'" & Replace(dBOwner, "'", "''") & "', '" & Replace(dBAccess, "'", "''") & "', " & _

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDdatabase.EditIndex = -1
            tblDBSelect.Visible = True
            tblDBEdit.Visible = False
            LoadDatabaseGV()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDDatabase_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDdatabase.PageIndexChanging
        Try
            gvFDdatabase.PageIndex = e.NewPageIndex
            If DatabaseSort.Text <> "" And DatabaseDir.Text <> "" Then
                LoadDatabaseGV(DatabaseSort.Text & " " & DatabaseDir.Text)
            Else
                LoadDatabaseGV()
            End If

            gvFDdatabase.EditIndex = -1
            gvFDdatabase.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvFDdatabase.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub gvFDdatabase_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvFDdatabase.SelectedIndexChanged

        For Each canoe As GridViewRow In gvFDdatabase.Rows
            If canoe.RowIndex = gvFDdatabase.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5EAFF")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub
    Private Sub gvFDdatabase_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFDdatabase.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvFDdatabase.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If

        If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

        Else
            e.Row.Cells(0).CssClass = "hidden"
        End If
    End Sub
    Protected Sub gvFDDatabase_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDdatabase.Sorting
        Try
            Dim dataTable As DataTable = TryCast(gvFDdatabase.DataSource, DataTable)
            Dim temp As String = e.SortDirection

            gvFDdatabase.EditIndex = -1
            gvFDdatabase.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvFDdatabase.DataBind()

            If gvFDdatabase.PageIndex <> 0 Then
                gvFDdatabase.PageIndex = 0
                LoadDatabaseGV()
                Exit Sub
            End If

            If DatabaseSort.Text = e.SortExpression Then
                If DatabaseDir.Text = "asc" Then
                    LoadDatabaseGV(e.SortExpression & " desc ")
                    DatabaseDir.Text = "desc"
                Else
                    LoadDatabaseGV(e.SortExpression & " asc ")
                    DatabaseDir.Text = "asc"
                End If
            Else
                DatabaseSort.Text = e.SortExpression

                If e.SortDirection = SortDirection.Ascending Then
                    LoadDatabaseGV(e.SortExpression & " asc ")
                    DatabaseDir.Text = "asc"
                Else
                    LoadDatabaseGV(e.SortExpression & " desc ")
                    DatabaseDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
#Region "OldDatabases"
    'Protected Sub gvFDdatabaseNS1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDdatabaseNS1.RowCommand
    '    Try
    '        If e.CommandName = "ViewDatabase" Then
    '            lblDatabaseSelected.Text = gvFDdatabaseNS1.DataKeys(e.CommandArgument).Value

    '            If gvFDdatabaseNS1.Rows(e.CommandArgument).Cells(3).Text = "&nbsp;" Then
    '                lblSchemaDatabase.Text = gvFDdatabaseNS1.DataKeys(e.CommandArgument).Value & ": No description available at this time "
    '            Else
    '                lblSchemaDatabase.Text = gvFDdatabaseNS1.DataKeys(e.CommandArgument).Value & ": " & gvFDdatabaseNS1.Rows(e.CommandArgument).Cells(3).Text
    '            End If
    '            lblTableDatabase.Text = lblSchemaDatabase.Text
    '            lblColumnDatabase.Text = lblSchemaDatabase.Text

    '            pnlDatabase.Visible = False
    '            pnlSchema.Visible = True
    '            pnlTables.Visible = False
    '            pnlColumns.Visible = False
    '            gvFDSchema.Visible = False
    '            gvFDSchemaNS1.Visible = True

    '            LoadSchemaGVNS1()
    '            ' pnlSchema.ScrollBars = ScrollBars.Auto
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub


    '    Sub LoadDatabaseNS1(Optional sortDirectionNS1 As String = "Name asc")
    '        Try
    '            Dim drSQL As SqlDataReader
    '            Dim drSQLNS1 As SqlDataReader
    '            Dim dr As DataRow
    '            Dim temp As String = ""

    '            If pnlDatabase.Visible = True Then
    '                Dim tbl As System.Data.DataTable = New DataTable()
    '                Dim col As New DataColumn("Name")
    '                tbl.Columns.Add(col)
    '                col = New DataColumn("dBDesc")
    '                tbl.Columns.Add(col)
    '                col = New DataColumn("dBCaveat")
    '                tbl.Columns.Add(col)
    '                col = New DataColumn("dBOwner")
    '                tbl.Columns.Add(col)
    '                col = New DataColumn("dBAccess")
    '                tbl.Columns.Add(col)
    '                col = New DataColumn("LastUpdated")
    '                tbl.Columns.Add(col)
    '                col = New DataColumn("TechnicalDescription")
    '                tbl.Columns.Add(col)

    '                SQL = "SELECT a.Name " & _
    '                "FROM sys.sysdatabases a " & _
    '                "WHERE(HAS_DBACCESS(name) = 1) " & _
    '                "and name <> 'master' and name <> 'tempdb' and name <> 'msdb' " & _
    '                "order by " & sortDirectionNS1


    '                Using connNS1 As New SqlConnection(ConfigurationManager.ConnectionStrings("NS1conn").ConnectionString)
    '                    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                        cmdNS1 = New SqlCommand(SQL, connNS1)

    '                        cmdNS1.CommandTimeout = 86400
    '                        If connNS1.State = ConnectionState.Closed Then
    '                            connNS1.Open()
    '                        End If
    '                        drSQLNS1 = cmdNS1.ExecuteReader

    '                        While drSQLNS1.Read
    '                            dr = tbl.NewRow()
    '                            If IsDBNull(drSQLNS1.Item("Name")) Then
    '                                dr("Name") = ""
    '                            Else
    '                                dr("Name") = drSQLNS1.Item("Name")
    '                            End If

    '                            dr("dBDesc") = ""
    '                            dr("dBCaveat") = ""
    '                            dr("dBOwner") = ""
    '                            dr("dBAccess") = ""
    '                            dr("LastUpdated") = ""
    '                            dr("TechnicalDescription") = ""

    '                            SQL = "select b.dBName," & _
    '                            "b.dBDesc, b.dBCaveat, " & _
    '                            "b.dBOwner, b.dBAccess,  convert(varchar(25),b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription  " & _
    '                            "from DWH.DOC.FDDatabase b " & _
    '                            "where dBName = '" & dr("Name") & "' "


    '                            cmd = New SqlCommand(SQL, conn)
    '                            cmd.CommandTimeout = 86400
    '                            If conn.State = ConnectionState.Closed Then
    '                                conn.Open()
    '                            End If
    '                            drSQL = cmd.ExecuteReader
    '                            While drSQL.Read
    '                                If IsDBNull(drSQL.Item("dBDesc")) Then
    '                                    dr("dBDesc") = ""
    '                                Else
    '                                    dr("dBDesc") = drSQL.Item("dBDesc")
    '                                End If
    '                                If IsDBNull(drSQL.Item("dBCaveat")) Then
    '                                    dr("dBCaveat") = ""
    '                                Else
    '                                    dr("dBCaveat") = drSQL.Item("dBCaveat")
    '                                End If
    '                                If IsDBNull(drSQL.Item("dBOwner")) Then
    '                                    dr("dBOwner") = ""
    '                                Else
    '                                    dr("dBOwner") = drSQL.Item("dBOwner")
    '                                End If
    '                                If IsDBNull(drSQL.Item("dBAccess")) Then
    '                                    dr("dBAccess") = ""
    '                                Else
    '                                    dr("dBAccess") = drSQL.Item("dBAccess")
    '                                End If
    '                                If IsDBNull(drSQL.Item("LastUpdated")) Then
    '                                    dr("LastUpdated") = ""
    '                                Else
    '                                    dr("LastUpdated") = drSQL.Item("LastUpdated")
    '                                End If
    '                                If IsDBNull(drSQL.Item("TechnicalDescription")) Then
    '                                    dr("TechnicalDescription") = ""
    '                                Else
    '                                    dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
    '                                End If
    '                            End While
    '                            drSQL.Close()
    '                            tbl.Rows.Add(dr)
    '                        End While
    '                        drSQLNS1.Close()
    '                    End Using
    '                End Using

    '                gvFDdatabaseNS1.DataSource = tbl
    '                gvFDdatabaseNS1.DataBind()
    '                gvFDdatabaseNS1.Visible = True

    '                ViewState.Add("NS1TBL", tbl)


    '            End If


    '        Catch ex As Exception
    '            lblUser.Visible = False
    '            lblUser.Text = ex.ToString

    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseNS1_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDdatabaseNS1.RowCancelingEdit
    '        Try
    '            gvFDdatabaseNS1.EditIndex = -1
    '            gvFDdatabaseNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '            gvFDdatabaseNS1.DataBind()

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseNS1_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDdatabaseNS1.RowEditing
    '        Try
    '            gvFDdatabaseNS1.EditIndex = e.NewEditIndex
    '            gvFDdatabaseNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '            gvFDdatabaseNS1.DataBind()

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseNS1_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDdatabaseNS1.RowUpdating
    '        Try
    '            Dim DatabaseName As String = ""
    '            Dim Desc As Object
    '            Dim Caveat As String = ""
    '            Dim TechnicalDesc As String = ""
    '            Dim dBOwner As String = ""
    '            Dim dBAccess As String = ""

    '            DatabaseName = gvFDdatabaseNS1.DataKeys(e.RowIndex).Value
    '            Desc = e.NewValues.Item("dBDesc")
    '            Caveat = e.NewValues.Item("dBCaveat")
    '            TechnicalDesc = e.NewValues.Item("TechnicalDescription")
    '            dBOwner = e.NewValues.Item("dbOwner")
    '            dBAccess = e.NewValues.Item("dBAccess")

    '            SQL = "DECLARE @rowcount int " & _
    '            "UPDATE [DWH].[DOC].[FDDatabase] SET " & _
    '            "dBDesc = '" & Replace(Desc, "'", "''") & "', dBCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
    '            "Active = 1, dbOwner = '" & Replace(dBOwner, "'", "''") & "', dbAccess = '" & Replace(dBAccess, "'", "''") & "', " & _
    '            "LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "', TechnicalDescription = '" & Replace(TechnicalDesc, "'", "''") & "'  " & _
    '            "WHERE (dBName =  '" & Replace(DatabaseName, "'", "''") & "') " & _
    '            "set @rowcount = @@ROWCOUNT " & _
    '            "IF @rowcount = 0 " & _
    '            "BEGIN " & _
    '            "INSERT INTO [DWH].[DOC].[FDDatabase] " & _
    '            "([dBName],[dBDesc],[dBCaveat],[Active], [dbOwner], [dbAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription]) " & _
    '            "   VALUES " & _
    '            "('" & Replace(DatabaseName, "'", "''") & "', '" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
    '            "'" & Replace(dBOwner, "'", "''") & "', '" & Replace(dBAccess, "'", "''") & "', " & _
    '            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechnicalDesc, "'", "''") & "')" & _
    '            "END"

    '            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                cmd = New SqlCommand(SQL, conn)
    '                cmd.CommandTimeout = 86400
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteNonQuery()
    '            End Using

    '            gvFDdatabaseNS1.EditIndex = -1
    '            LoadDatabaseNS1()

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseNS1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDdatabaseNS1.PageIndexChanging
    '        Try
    '            gvFDdatabaseNS1.PageIndex = e.NewPageIndex
    '            If DatabaseSort.Text <> "" And DatabaseDir.Text <> "" Then
    '                LoadDatabaseNS1(DatabaseSort.Text & " " & DatabaseDir.Text)
    '            Else
    '                LoadDatabaseNS1()
    '            End If

    '            gvFDdatabaseNS1.EditIndex = -1
    '            gvFDdatabaseNS1.DataSource = DirectCast(ViewState("TBL"), DataTable)
    '            gvFDdatabaseNS1.DataBind()
    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseNS1_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDdatabaseNS1.Sorting
    '        Try
    '            Dim dataTable As DataTable = TryCast(gvFDdatabaseNS1.DataSource, DataTable)
    '            Dim temp As String = e.SortDirection

    '            gvFDdatabaseNS1.EditIndex = -1
    '            gvFDdatabaseNS1.DataSource = DirectCast(ViewState("TBL"), DataTable)
    '            gvFDdatabaseNS1.DataBind()

    '            If gvFDdatabaseNS1.PageIndex <> 0 Then
    '                gvFDdatabaseNS1.PageIndex = 0
    '                LoadDatabaseNS1()
    '                Exit Sub
    '            End If
    '            If e.SortExpression <> "name" Then
    '                Exit Sub
    '            End If

    '            If DatabaseSort.Text = e.SortExpression Then
    '                If DatabaseDir.Text = "asc" Then
    '                    LoadDatabaseNS1(e.SortExpression & " desc ")
    '                    DatabaseDir.Text = "desc"
    '                Else
    '                    LoadDatabaseNS1(e.SortExpression & " asc ")
    '                    DatabaseDir.Text = "asc"
    '                End If
    '            Else
    '                DatabaseSort.Text = e.SortExpression

    '                'sort on NS1 server data
    '                If e.SortDirection = SortDirection.Ascending Then
    '                    LoadDatabaseNS1(e.SortExpression & " asc ")
    '                    DatabaseDir.Text = "asc"
    '                Else
    '                    LoadDatabaseNS1(e.SortExpression & " desc ")
    '                    DatabaseDir.Text = "desc"
    '                End If


    '            End If

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub


    '    'gvFDDatabaseMPA'
    '    Sub LoadDatabaseMPA(Optional sortDirectionMPA As String = "Name asc")
    '        Try
    '            Dim drSQL As SqlDataReader
    '            Dim dr As DataRow
    '            Dim keys(0) As DataColumn
    '            Dim temp As String = ""

    '            If pnlDatabase.Visible = True Then
    '                Dim tblMPA As System.Data.DataTable = New DataTable()
    '                Dim col As New DataColumn("Name")
    '                keys(0) = col
    '                tblMPA.Columns.Add(col)
    '                col = New DataColumn("dBDesc")
    '                tblMPA.Columns.Add(col)
    '                col = New DataColumn("dBCaveat")
    '                tblMPA.Columns.Add(col)
    '                col = New DataColumn("dBOwner")
    '                tblMPA.Columns.Add(col)
    '                col = New DataColumn("dBAccess")
    '                tblMPA.Columns.Add(col)
    '                col = New DataColumn("LastUpdated")
    '                tblMPA.Columns.Add(col)
    '                col = New DataColumn("TechnicalDescription")
    '                tblMPA.Columns.Add(col)

    '                tblMPA.PrimaryKey = keys

    '                'SQL = "SELect * from " & _
    '                ' "(select " & _
    '                ' "dBName COLLATE DATABASE_DEFAULT as Name, dBDesc, dBCaveat, dBOwner, dBAccess, LastUpdated, TechnicalDescription " & _
    '                ' " from DWH.DOC.FDDatabase " & _
    '                ' "where dbName = 'MPA') x  " & _
    '                ' "order by " & sortDirectionMPA

    '                SQL = "Select * from  " & _
    '                "(select  " & _
    '                "dBName COLLATE DATABASE_DEFAULT as Name, dBDesc, dBCaveat, " & _
    '                "dBOwner, dBAccess, " & _
    '                "CONVERT(varchar, LastUpdated, 100) 'LastUpdated', " & _
    '                "TechnicalDescription   " & _
    '                "from DWH.DOC.FDDatabase  " & _
    '                "where dbName = 'MPA') x " & _
    '                "order by " & sortDirectionMPA

    '                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                    cmd = New SqlCommand(SQL, conn)
    '                    cmd.CommandTimeout = 86400
    '                    If conn.State = ConnectionState.Closed Then
    '                        conn.Open()
    '                    End If
    '                    drSQL = cmd.ExecuteReader

    '                    While drSQL.Read
    '                        dr = tblMPA.NewRow()
    '                        If IsDBNull(drSQL.Item("Name")) Then
    '                            dr("Name") = ""
    '                        Else
    '                            dr("Name") = drSQL.Item("Name")
    '                        End If
    '                        If IsDBNull(drSQL.Item("dBDesc")) Then
    '                            dr("dBDesc") = ""
    '                        Else
    '                            dr("dBDesc") = drSQL.Item("dBDesc")
    '                        End If
    '                        If IsDBNull(drSQL.Item("dBCaveat")) Then
    '                            dr("dBCaveat") = ""
    '                        Else
    '                            dr("dBCaveat") = drSQL.Item("dBCaveat")
    '                        End If
    '                        If IsDBNull(drSQL.Item("dBOwner")) Then
    '                            dr("dBOwner") = ""
    '                        Else
    '                            dr("dBOwner") = drSQL.Item("dBOwner")
    '                        End If
    '                        If IsDBNull(drSQL.Item("dBAccess")) Then
    '                            dr("dBAccess") = ""
    '                        Else
    '                            dr("dBAccess") = drSQL.Item("dBAccess")
    '                        End If
    '                        If IsDBNull(drSQL.Item("LastUpdated")) Then
    '                            dr("LastUpdated") = ""
    '                        Else
    '                            dr("LastUpdated") = drSQL.Item("LastUpdated")
    '                        End If
    '                        If IsDBNull(drSQL.Item("TechnicalDescription")) Then
    '                            dr("TechnicalDescription") = ""
    '                        Else
    '                            dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
    '                        End If
    '                        tblMPA.Rows.Add(dr)
    '                    End While
    '                    drSQL.Close()
    '                End Using

    '                gvFDDatabaseMPA.DataSource = tblMPA
    '                gvFDDatabaseMPA.DataBind()
    '                gvFDDatabaseMPA.Visible = True

    '                ViewState.Add("TBLMPA", tblMPA)

    '                If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

    '                Else
    '                    gvFDDatabaseMPA.Columns.Item(0).Visible = False
    '                End If

    '            End If
    '        Catch ex As Exception
    '            lblUser.Visible = False
    '            lblUser.Text = ex.ToString

    '        End Try
    '    End Sub

    '    Protected Sub gvFDdatabaseMPA_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDDatabaseMPA.RowCommand
    '        Try
    '            If e.CommandName = "ViewDatabase" Then
    '                lblDatabaseSelected.Text = gvFDDatabaseMPA.DataKeys(e.CommandArgument).Value.ToString

    '                If gvFDDatabaseMPA.Rows(e.CommandArgument).Cells(3).Text = "&nbsp;" Then
    '                    lblSchemaDatabase.Text = gvFDDatabaseMPA.DataKeys(e.CommandArgument).Value.ToString & ": No description available at this time "
    '                Else
    '                    lblSchemaDatabase.Text = gvFDDatabaseMPA.DataKeys(e.CommandArgument).Value.ToString & ": " & gvFDDatabaseMPA.Rows(e.CommandArgument).Cells(3).Text
    '                End If
    '                lblTableDatabase.Text = lblSchemaDatabase.Text
    '                lblColumnDatabase.Text = lblSchemaDatabase.Text

    '                pnlDatabase.Visible = False
    '                pnlSchema.Visible = True
    '                pnlTables.Visible = False
    '                pnlColumns.Visible = False

    '                gvFDSchema.Visible = True
    '                LoadSchemaGV()
    '            End If
    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseMPA_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDDatabaseMPA.RowCancelingEdit
    '        Try
    '            gvFDDatabaseMPA.EditIndex = -1
    '            gvFDDatabaseMPA.DataSource = DirectCast(ViewState("TBLMPA"), DataTable)
    '            gvFDDatabaseMPA.DataBind()

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseMPA_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDDatabaseMPA.RowEditing
    '        Try
    '            gvFDDatabaseMPA.EditIndex = e.NewEditIndex
    '            gvFDDatabaseMPA.DataSource = DirectCast(ViewState("TBLMPA"), DataTable)
    '            gvFDDatabaseMPA.DataBind()

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseMPA_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDDatabaseMPA.RowUpdating
    '        Try
    '            Dim DatabaseName As String = ""
    '            Dim Desc As Object
    '            Dim Caveat As String = ""
    '            Dim TechnicalDesc As String = ""
    '            Dim dBOwner As String = ""
    '            Dim dBAccess As String = ""

    '            DatabaseName = gvFDDatabaseMPA.DataKeys(e.RowIndex).Value.ToString
    '            Desc = e.NewValues.Item("dBDesc")
    '            Caveat = e.NewValues.Item("dBCaveat")
    '            TechnicalDesc = e.NewValues.Item("TechnicalDescription")
    '            dBOwner = e.NewValues.Item("dbOwner")
    '            dBAccess = e.NewValues.Item("dBAccess")

    '            SQL = "DECLARE @rowcount int " & _
    '            "UPDATE [DWH].[DOC].[FDDatabase] SET " & _
    '            "dBDesc = '" & Replace(Desc, "'", "''") & "', dBCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
    '            "Active = 1, dbOwner = '" & Replace(dBOwner, "'", "''") & "', dbAccess = '" & Replace(dBAccess, "'", "''") & "', " & _
    '            "LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "', TechnicalDescription = '" & Replace(TechnicalDesc, "'", "''") & "'  " & _
    '            "WHERE (dBName =  '" & Replace(DatabaseName, "'", "''") & "') " & _
    '            "set @rowcount = @@ROWCOUNT " & _
    '            "IF @rowcount = 0 " & _
    '            "BEGIN " & _
    '            "INSERT INTO [DWH].[DOC].[FDDatabase] " & _
    '            "([dBName],[dBDesc],[dBCaveat],[Active], [dbOwner], [dbAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription]) " & _
    '            "   VALUES " & _
    '            "('" & Replace(DatabaseName, "'", "''") & "', '" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
    '            "'" & Replace(dBOwner, "'", "''") & "', '" & Replace(dBAccess, "'", "''") & "', " & _
    '            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechnicalDesc, "'", "''") & "')" & _
    '            "END"

    '            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                cmd = New SqlCommand(SQL, conn)
    '                cmd.CommandTimeout = 86400
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteNonQuery()
    '            End Using

    '            gvFDDatabaseMPA.EditIndex = -1
    '            LoadDatabaseMPA()

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseMPA_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDDatabaseMPA.PageIndexChanging
    '        Try
    '            gvFDDatabaseMPA.PageIndex = e.NewPageIndex
    '            If DatabaseSort.Text <> "" And DatabaseDir.Text <> "" Then
    '                LoadDatabaseMPA(DatabaseSort.Text & " " & DatabaseDir.Text)
    '            Else
    '                LoadDatabaseMPA()
    '            End If

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
    '    Protected Sub gvFDDatabaseMPA_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDDatabaseMPA.Sorting
    '        Try
    '            Dim dataTable As DataTable = TryCast(gvFDDatabaseMPA.DataSource, DataTable)
    '            Dim temp As String = e.SortDirection

    '            If gvFDDatabaseMPA.PageIndex <> 0 Then
    '                gvFDDatabaseMPA.PageIndex = 0
    '                LoadDatabaseMPA()
    '                Exit Sub
    '            End If
    '            If e.SortExpression <> "name" Then
    '                Exit Sub
    '            End If

    '            If DatabaseSort.Text = e.SortExpression Then
    '                If DatabaseDir.Text = "asc" Then
    '                    LoadDatabaseMPA(e.SortExpression & " desc ")
    '                    DatabaseDir.Text = "desc"
    '                Else
    '                    LoadDatabaseMPA(e.SortExpression & " asc ")
    '                    DatabaseDir.Text = "asc"
    '                End If
    '            Else
    '                DatabaseSort.Text = e.SortExpression

    '                'sort on MPA server data
    '                If e.SortDirection = SortDirection.Ascending Then
    '                    LoadDatabaseMPA(e.SortExpression & " asc ")
    '                    DatabaseDir.Text = "asc"
    '                Else
    '                    LoadDatabaseMPA(e.SortExpression & " desc ")
    '                    DatabaseDir.Text = "desc"
    '                End If
    '            End If

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub
#End Region
#End Region
#Region "GVSchema"
    Sub LoadSchemaGV(Optional sortDirection As String = "Table_Schema asc")
        Try
            If pnlSchema.Visible = True Then
                Dim tbl As System.Data.DataTable = New DataTable()
                Dim keys(0) As DataColumn
                Dim col As New DataColumn("TABLE_SCHEMA")
                keys(0) = col
                tbl.Columns.Add(col)
                col = New DataColumn("cntTable")
                tbl.Columns.Add(col)
                col = New DataColumn("SchemaOwner")
                tbl.Columns.Add(col)
                col = New DataColumn("SchemaAccess")
                tbl.Columns.Add(col)
                col = New DataColumn("SchemaDesc")
                tbl.Columns.Add(col)
                col = New DataColumn("SchemaCaveat")
                tbl.Columns.Add(col)
                col = New DataColumn("LDUpdated")
                tbl.Columns.Add(col)
                col = New DataColumn("LastUpdated")
                tbl.Columns.Add(col)
                col = New DataColumn("TechnicalDescription")
                tbl.Columns.Add(col)
                col = New DataColumn("Active")
                tbl.Columns.Add(col)

                tbl.PrimaryKey = keys

                If lblDatabaseSelected.Text = "MPA" Then
                    SQL = "select " & _
                    "a.SchemaName as Table_Schema, count(b.TableName) as cntTable, " & _
                    "SchemaDesc, SchemaCaveat, SchemaOwner, SchemaAccess, CONVERT(varchar, a.LastUpdated,111) as LDUpdated, " & _
                    "convert(varchar(25), a.LastUpdated , 107) as LastUpdated, a.TechnicalDescription  " & _
                    "from [DWH].[DOC].FDSchema a " & _
                    "left outer join DWH.DOC.FDTables b on (a.dBName = b.dBName and a.SchemaName = b.SchemaName) " & _
                    "where a.dBName = 'MPA' group by  a.SchemaName ,   " & _
                    "SchemaDesc, SchemaCaveat, SchemaOwner, SchemaAccess, " & _
                    "a.LastUpdated, a.TechnicalDescription  " & _
                    "order by " & sortDirection
                Else
                    SQL = "select " & _
                   "a.TABLE_SCHEMA, count(TABLE_NAME) as cntTable, " & _
                   "b.SchemaDesc, b.SchemaCaveat, b.SchemaOwner, b.SchemaAccess, CONVERT(varchar, b.LastUpdated,111) as LDUpdated, " & _
                   "convert(varchar(25), b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription, b.Active " & _
                   "from " & lblDatabaseSelected.Text & ".INFORMATION_SCHEMA.TABLES a " & _
                   "left outer join [DWH].[DOC].fdschema b on (a.TABLE_SCHEMA = b.schemaname and a.TABLE_CATALOG = b.dBName )" & _
                   "where b.Active = 1 or b.Active is null or 1 = "
                    If chbSchemaInactive.Checked = True Then
                        SQL += "1 "
                    Else
                        SQL += "0 "
                    End If
                    SQL += " group by b.Active, a.TABLE_SCHEMA, b.SchemaDesc , b.SchemaCaveat, b.SchemaOwner, b.SchemaAccess, b.LastUpdated, b.TechnicalDescription  " & _
                   "order by " & sortDirection
                End If


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd = New SqlCommand(SQL, conn)
                    cmd.CommandTimeout = 86400
                    Dim drSQL As SqlDataReader
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    drSQL = cmd.ExecuteReader
                    Dim dr As DataRow
                    While drSQL.Read
                        dr = tbl.NewRow()
                        If IsDBNull(drSQL.Item("TABLE_SCHEMA")) Then
                            dr("TABLE_SCHEMA") = ""
                        Else
                            dr("TABLE_SCHEMA") = drSQL.Item("TABLE_SCHEMA")
                        End If
                        If IsDBNull(drSQL.Item("cntTable")) Then
                            dr("cntTable") = ""
                        Else
                            dr("cntTable") = drSQL.Item("cntTable")
                        End If
                        If IsDBNull(drSQL.Item("SchemaOwner")) Then
                            dr("SchemaOwner") = ""
                        Else
                            dr("SchemaOwner") = drSQL.Item("SchemaOwner")
                        End If
                        If IsDBNull(drSQL.Item("SchemaAccess")) Then
                            dr("SchemaAccess") = ""
                        Else
                            dr("SchemaAccess") = drSQL.Item("SchemaAccess")
                        End If
                        If IsDBNull(drSQL.Item("SchemaDesc")) Then
                            dr("SchemaDesc") = ""
                        Else
                            dr("SchemaDesc") = drSQL.Item("SchemaDesc")
                        End If
                        If IsDBNull(drSQL.Item("SchemaCaveat")) Then
                            dr("SchemaCaveat") = ""
                        Else
                            dr("SchemaCaveat") = drSQL.Item("SchemaCaveat")
                        End If
                        If IsDBNull(drSQL.Item("LDUpdated")) Then
                            dr("LDUpdated") = ""
                        Else
                            dr("LDUpdated") = drSQL.Item("LDUpdated")
                        End If
                        If IsDBNull(drSQL.Item("LastUpdated")) Then
                            dr("LastUpdated") = ""
                        Else
                            dr("LastUpdated") = drSQL.Item("LastUpdated")
                        End If
                        If IsDBNull(drSQL.Item("TechnicalDescription")) Then
                            dr("TechnicalDescription") = ""
                        Else                        
                            dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
                        End If
                        If IsDBNull(drSQL.Item("Active")) Then
                            dr("Active") = True
                        ElseIf drSQL.Item("Active") = "1" Then
                            dr("Active") = True
                        Else
                            dr("Active") = False
                        End If
                        tbl.Rows.Add(dr)
                    End While
                    drSQL.Close()
                End Using

                'gvFDSchema.DataSource = tbl
                'gvFDSchema.DataBind()

                ViewState.Add("TBL", tbl)
                dv = DirectCast(ViewState("TBL"), DataTable).DefaultView

                FilterSchema()

                'gvFDSchema.EditIndex = -1
                'gvFDSchema.DataSource = dv
                'gvFDSchema.DataBind()

                If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then
                Else

                    gvFDSchema.Columns.Item(0).Visible = False
                End If

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDSchema_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDSchema.RowCancelingEdit
        Try

            gvFDSchema.EditIndex = -1
            gvFDSchema.DataSource = dv
            gvFDSchema.DataBind()
            tblSSelect.Visible = True
            tblSEdit.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDSchema_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDSchema.RowCommand
        Try
            If e.CommandName = "ViewTables" Then
                lblSchemaSelected.Text = gvFDSchema.DataKeys(e.CommandArgument).Value.ToString

                dv.Sort = "TABLE_SCHEMA"
                Dim ind As Integer = dv.Find(lblSchemaSelected.Text)
                Dim rw As DataRow = dv.Table(ind)

                If Trim(rw(4).ToString) = "" Then
                    lblTableSchema.Text = lblSchemaSelected.Text & ": No description available at this time "
                Else
                    lblTableSchema.Text = lblSchemaSelected.Text & ": " & rw(4).ToString
                End If
                lblColumnSchema.Text = lblTableSchema.Text

                pnlSchemaDefinitions.Visible = False
                pnlTableDefinitions.Visible = True

                pnlDatabase.Visible = False
                pnlSchema.Visible = False
                pnlTables.Visible = True
                pnlColumns.Visible = False
                gvFDTables.Visible = True
                'gvFDTablesNS1.Visible = False
                LoadTablesGV()

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDSchema_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDSchema.RowDeleting
        Try
            Dim SchemaName As String = ""
            ' Dim dBName As String = lblDatabaseSelected.Text

            SchemaName = gvFDSchema.Rows(e.RowIndex).Cells(2).Text

            SQL = "DECLARE @rowcount int " & _
            "UPDATE DWH.Doc.fdschema SET  " & _
             "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
            "WHERE SchemaName = '" & Replace(SchemaName, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "set @rowcount = @@ROWCOUNT  " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
            "  Begin  " & _
            "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  END " & _
            "INSERT INTO DWH.Doc.fdschema  " & _
            "([dBName], [SchemaName],[SchemaDesc],[SchemaCaveat],[Active], [LastUpdated], [LastUpdatedPerson])  " & _
            "   VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(SchemaName, "'", "''") & "', " & _
            "'', '', 0, sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "') " & _
            "END " & _
            "Update DWH.Doc.FDColumnData set " & _
            "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "' " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "Update DWH.Doc.FDTables set " & _
            "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "' " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDSchema.EditIndex = -1
            LoadSchemaGV()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDSchema_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDSchema.RowEditing
        Try

            gvFDSchema.EditIndex = e.NewEditIndex
            gvFDSchema.DataSource = dv
            gvFDSchema.DataBind()

            Dim txtDesc As TextBox = gvFDSchema.Rows(e.NewEditIndex).FindControl("txtSGVDescription")
            Dim lblDesc As Label = gvFDSchema.Rows(e.NewEditIndex).FindControl("lblSGVDescription")
            Dim txtTechDesc As TextBox = gvFDSchema.Rows(e.NewEditIndex).FindControl("txtSGVTechDesc")
            Dim lblTechDesc As Label = gvFDSchema.Rows(e.NewEditIndex).FindControl("lblSGVTechDesc")
            Dim txtCaveat As TextBox  ' = gvFDSchema.Rows(e.NewEditIndex).FindControl("txtSGVCaveat")
            txtCaveat = gvFDSchema.Rows(e.NewEditIndex).FindControl("txtSGVCaveat")

            Dim lblCaveat As Label = gvFDSchema.Rows(e.NewEditIndex).FindControl("lblSGVCaveat")
            'Dim txtOwner As TextBox = gvFDSchema.Rows(e.NewEditIndex).FindControl("txtSGVOwner")
            'Dim lblOwner As Label = gvFDSchema.Rows(e.NewEditIndex).FindControl("lblSGVOwner")
            'Dim txtAccess As TextBox = gvFDSchema.Rows(e.NewEditIndex).FindControl("txtSGVAccess")
            'Dim lblAccess As Label = gvFDSchema.Rows(e.NewEditIndex).FindControl("lblSGVAccess")
            Dim chbActive As CheckBox = gvFDSchema.Rows(e.NewEditIndex).FindControl("chbSGVActive")
            Dim chbPreActive As CheckBox = gvFDSchema.Rows(e.NewEditIndex).FindControl("chbSGVActiveDisabled")





            txtDesc.Visible = True
            lblDesc.Visible = False
            txtTechDesc.Visible = True
            lblTechDesc.Visible = False
            'txtOwner.Visible = True
            'lblOwner.Visible = False
            txtCaveat.Visible = True
            lblCaveat.Visible = False
            tblSSelect.Visible = False
            tblSEdit.Visible = True
            'txtAccess.Visible = True
            'lblAccess.Visible = False
            chbActive.Visible = True
            chbPreActive.Visible = False

            'gvFDSchema.Columns(0).ItemStyle.Width = "100px"

            For Each canoe As GridViewRow In gvFDSchema.Rows
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
    Protected Sub gvFDSchema_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDSchema.RowUpdating
        Try
            Dim SchemaName As String = ""
            Dim Desc As Object
            Dim Caveat As String = ""
            Dim TechnicalDesc As String = ""
            Dim SchemaOwner As String = ""
            Dim SchemaAccess As String = ""
            Dim SchemaActive As String = "1"

            Dim txtDesc As TextBox = gvFDSchema.Rows(e.RowIndex).FindControl("txtSGVDescription")
            Dim txtTechDesc As TextBox = gvFDSchema.Rows(e.RowIndex).FindControl("txtSGVTechDesc")
            Dim txtCaveat As TextBox = gvFDSchema.Rows(e.RowIndex).FindControl("txtSGVCaveat")
            'Dim txtOwner As TextBox = gvFDSchema.Rows(e.RowIndex).FindControl("txtSGVOwner")
            'Dim txtAccess As TextBox = gvFDSchema.Rows(e.RowIndex).FindControl("txtSGVAccess")
            'Dim txtActive As TextBox = gvFDSchema.Rows(e.RowIndex).FindControl("txtSGVActive")
            Dim chbActive As CheckBox = gvFDSchema.Rows(e.RowIndex).FindControl("chbSGVActive")

            SchemaName = gvFDSchema.DataKeys(e.RowIndex).Value.ToString
            Desc = txtDesc.Text
            Caveat = txtCaveat.Text
            TechnicalDesc = txtTechDesc.Text
            'SchemaOwner = txtOwner.Text
            'SchemaAccess = txtAccess.Text
            'SchemaActive = txtActive.Text
            If chbActive.Checked Then
                SchemaActive = "1"
            Else
                SchemaActive = "0"
            End If

            SQL = "DECLARE @rowcount int " & _
            "UPDATE DWH.Doc.fdschema SET  " & _
            "SchemaDesc = '" & Replace(Desc, "'", "''") & "', " & _
            "SchemaCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
            "Active = " & SchemaActive & ", " & _
            "LastUpdated = sysdatetime(), " & _
            "LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "', " & _
            "TechnicalDescription = '" & Replace(TechnicalDesc, "'", "''") & "' " & _
            "WHERE SchemaName = '" & Replace(SchemaName, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "set @rowcount = @@ROWCOUNT  " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
            "  Begin  " & _
            "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  END " & _
            "INSERT INTO DWH.Doc.fdschema  " & _
            "([dBName], [SchemaName],[SchemaDesc],[SchemaCaveat],[Active], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
            "   VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(SchemaName, "'", "''") & "', " & _
            "'" & Replace(Desc, "'", "''") & "', " & _
            "'" & Replace(Caveat, "'", "''") & "', " & SchemaActive & ", " & _
            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechnicalDesc, "'", "''") & "') " & _
            "END "
            '"SchemaOwner = '" & Replace(SchemaOwner, "'", "''") & "', " & _
            '"SchemaAccess ='" & Replace(SchemaAccess, "'", "''") & "', " & _

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDSchema.EditIndex = -1
            LoadSchemaGV()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDSchema_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDSchema.PageIndexChanging
        Try
            gvFDSchema.PageIndex = e.NewPageIndex
            If SchemaSort.Text <> "" And SchemaDir.Text <> "" Then
                LoadSchemaGV(SchemaSort.Text & " " & SchemaDir.Text)
            Else
                LoadSchemaGV()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub gvFDSchema_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvFDSchema.SelectedIndexChanged
        For Each canoe As GridViewRow In gvFDSchema.Rows
            If canoe.RowIndex = gvFDSchema.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub
    Private Sub gvFDSchema_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFDSchema.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvFDSchema.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If

        If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

        Else
            e.Row.Cells(0).CssClass = "hidden"
        End If

        If chbSchemaInactive.Checked = False Then
            e.Row.Cells(8).CssClass = "hidden"
        End If

    End Sub
    Protected Sub gvFDSchema_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDSchema.Sorting
        Try
            'Dim dataTable As DataTable = TryCast(gvFDSchema.DataSource, DataTable)
            Dim temp As String = e.SortDirection

            If gvFDSchema.PageIndex <> 0 Then
                gvFDSchema.PageIndex = 0
                LoadSchemaGV()
                Exit Sub
            End If

            If SchemaSort.Text = e.SortExpression Then
                If SchemaDir.Text = "asc" Then
                    LoadSchemaGV(e.SortExpression & " desc ")
                    SchemaDir.Text = "desc"
                Else
                    LoadSchemaGV(e.SortExpression & " asc ")
                    SchemaDir.Text = "asc"
                End If
            Else
                SchemaSort.Text = e.SortExpression

                If e.SortDirection = SortDirection.Ascending Then
                    LoadSchemaGV(e.SortExpression & " asc ")
                    SchemaDir.Text = "asc"
                Else
                    LoadSchemaGV(e.SortExpression & " desc ")
                    SchemaDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub btnReturnToDatabase_Click(sender As Object, e As System.EventArgs) Handles btnReturnToDatabase.Click
        Try
            pnlDBDefinitions.Visible = True
            pnlSchemaDefinitions.Visible = False

            pnlDatabase.Visible = True
            pnlSchema.Visible = False
            pnlTables.Visible = False
            pnlColumns.Visible = False
            gvFDdatabase.DataBind()
            SchemaDir.Text = ""
            SchemaSort.Text = ""
            LoadDatabaseGV()

            'LoadDatabaseNS1()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub FilterSchema()
        Try

            Dim filter As String = ""

            If Len(txtSGVSearchDesc.Text) > 0 Then
                filter += " SchemaDesc like '%" & txtSGVSearchDesc.Text & "%' and "
            End If

            If Len(txtSGVSearchTechDesc.Text) > 0 Then
                filter += " TechnicalDescription like '%" & txtSGVSearchTechDesc.Text & "%' and "
            End If

            If Len(txtSGVSearchCaveat.Text) > 0 Then
                filter += " SchemaCaveat like '%" & txtSGVSearchCaveat.Text & "%' and "
            End If

            'If Len(txtSGVSearchOwner.Text) > 0 Then
            '    filter += " SchemaOwner like '%" & txtSGVSearchOwner.Text & "%' and "
            'End If

            'If Len(txtSGVSearchAccess.Text) > 0 Then
            '    filter += " SchemaAccess like '%" & txtSGVSearchAccess.Text & "%' and "
            'End If

            If Len(txtSGVSearchUpdate.Text) > 0 Then
                filter += " LDUpdated > '" & Replace(txtSGVSearchUpdate.Text, "-", "/") & "' and "
            End If

            If Len(filter) > 1 Then
                dv.RowFilter = Left(filter, Len(filter) - 5)
            Else
                dv.RowFilter = ""
            End If

            gvFDSchema.EditIndex = -1
            gvFDSchema.DataSource = dv
            gvFDSchema.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub txtSGVSearchDesc_TextChanged(sender As Object, e As System.EventArgs) Handles txtSGVSearchDesc.TextChanged

        FilterSchema()

    End Sub
    Private Sub txtSGVSearchTechDesc_TextChanged(sender As Object, e As System.EventArgs) Handles txtSGVSearchTechDesc.TextChanged

        FilterSchema()

    End Sub
    'Private Sub txtSGVSearchAccess_TextChanged(sender As Object, e As System.EventArgs) Handles txtSGVSearchAccess.TextChanged

    '    FilterSchema()

    'End Sub
    Private Sub txtSGVSearchCaveat_TextChanged(sender As Object, e As System.EventArgs) Handles txtSGVSearchCaveat.TextChanged

        FilterSchema()

    End Sub
    'Private Sub txtSGVSearchOwner_TextChanged(sender As Object, e As System.EventArgs) Handles txtSGVSearchOwner.TextChanged

    '    FilterSchema()

    'End Sub
    Private Sub txtSGVSearchUpdate_TextChanged(sender As Object, e As System.EventArgs) Handles txtSGVSearchUpdate.TextChanged

        FilterSchema()

    End Sub


    Sub SortSchema(sortex As String)
        Try
            'Dim dataTable As DataTable = TryCast(gvFDSchema.DataSource, DataTable)
            Dim temp As String = gvFDSchema.SortDirection

            If gvFDSchema.PageIndex <> 0 Then
                gvFDSchema.PageIndex = 0
                'LoadSchemaGV()
                'Exit Sub
            End If

            If SchemaSort.Text = sortex Then
                If SchemaDir.Text = "asc" Then
                    LoadSchemaGV(sortex & " desc ")
                    SchemaDir.Text = "desc"
                Else
                    LoadSchemaGV(sortex & " asc ")
                    SchemaDir.Text = "asc"
                End If
            Else
                SchemaSort.Text = sortex

                If gvFDSchema.SortDirection = SortDirection.Ascending Then
                    LoadSchemaGV(sortex & " asc ")
                    SchemaDir.Text = "asc"
                Else
                    LoadSchemaGV(sortex & " desc ")
                    SchemaDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub chbSchemaInactive_CheckedChanged(sender As Object, e As EventArgs) Handles chbSchemaInactive.CheckedChanged

        Try

            If gvFDSchema.SortExpression = "" Then
                LoadSchemaGV()
            ElseIf gvFDSchema.SortDirection = SortDirection.Ascending Then
                LoadSchemaGV(gvFDSchema.SortExpression & " asc ")
                SchemaDir.Text = "asc"
            Else
                LoadSchemaGV(gvFDSchema.SortExpression & " desc ")
                SchemaDir.Text = "desc"
            End If

            If chbSchemaInactive.Checked = True Then
                schemaActive.Visible = True
            Else
                schemaActive.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub lnkSch1_Click(sender As Object, e As EventArgs) Handles lnkSch1.Click
        SortSchema("TABLE_SCHEMA")
    End Sub

    Private Sub lnkSch2_Click(sender As Object, e As EventArgs) Handles lnkSch2.Click
        SortSchema("SchemaDesc")
    End Sub

    Private Sub lnkSch3_Click(sender As Object, e As EventArgs) Handles lnkSch3.Click
        SortSchema("TechnicalDescription")
    End Sub

    Private Sub lnkSch4_Click(sender As Object, e As EventArgs) Handles lnkSch4.Click
        SortSchema("SchemaCaveat")
    End Sub

    Private Sub lnkSch5_Click(sender As Object, e As EventArgs) Handles lnkSch5.Click
        SortSchema("cntTable")
    End Sub

    'Private Sub lnkSch6_Click(sender As Object, e As EventArgs) Handles lnkSch6.Click
    '    SortSchema("SchemaOwner")
    'End Sub

    'Private Sub lnkSch7_Click(sender As Object, e As EventArgs) Handles lnkSch7.Click
    '    SortSchema("SchemaAccess")
    'End Sub

    Private Sub lnkSch8_Click(sender As Object, e As EventArgs) Handles lnkSch8.Click
        SortSchema("LDUpdated")
    End Sub
    Private Sub lnkSch9_Click(sender As Object, e As EventArgs) Handles lnkSch9.Click
        SortSchema("Active")
    End Sub

#Region "OldSchemas"
    'Sub LoadSchemaGVNS1(Optional sortDirection As String = "Table_Schema asc")
    '    Try
    '        If pnlSchema.Visible = True Then
    '            Dim drSQL As SqlDataReader
    '            Dim drSQLNS1 As SqlDataReader
    '            Dim dr As DataRow
    '            Dim tbl As System.Data.DataTable = New DataTable()
    '            Dim keys(0) As DataColumn
    '            Dim col As New DataColumn("TABLE_SCHEMA")

    '            keys(0) = col
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("cntTable")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("SchemaOwner")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("SchemaAccess")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("SchemaDesc")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("SchemaCaveat")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("LastUpdated")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TechnicalDescription")
    '            tbl.Columns.Add(col)

    '            tbl.PrimaryKey = keys

    '            SQL = "select " & _
    '            "TABLE_SCHEMA, count(TABLE_NAME) as cntTable " & _
    '            "from [" & lblDatabaseSelected.Text & "].INFORMATION_SCHEMA.TABLES " & _
    '            "group by TABLE_SCHEMA " & _
    '            "order by " & sortDirection

    '            Using connNS1 As New SqlConnection(ConfigurationManager.ConnectionStrings("NS1conn").ConnectionString)
    '                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                    cmdNS1 = New SqlCommand(SQL, connNS1)
    '                    cmdNS1.CommandTimeout = 86400
    '                    If connNS1.State = ConnectionState.Closed Then
    '                        connNS1.Open()
    '                    End If
    '                    drSQLNS1 = cmdNS1.ExecuteReader

    '                    While drSQLNS1.Read
    '                        dr = tbl.NewRow()
    '                        If IsDBNull(drSQLNS1.Item("TABLE_SCHEMA")) Then
    '                            dr("TABLE_SCHEMA") = ""
    '                        Else
    '                            dr("TABLE_SCHEMA") = drSQLNS1.Item("TABLE_SCHEMA")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("cntTable")) Then
    '                            dr("cntTable") = ""
    '                        Else
    '                            dr("cntTable") = drSQLNS1.Item("cntTable")
    '                        End If

    '                        'dr("SchemaOwner") = ""
    '                        'dr("SchemaAccess") = ""
    '                        'dr("SchemaDesc") = ""
    '                        'dr("SchemaCaveat") = ""
    '                        'dr("LastUpdated") = ""
    '                        'dr("TechnicalDescription") = ""

    '                        SQL = "select " & _
    '                        "b.SchemaDesc, b.SchemaCaveat, b.SchemaOwner, b.SchemaAccess, " & _
    '                        "convert(varchar(25), b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription " & _
    '                        "from [DWH].[DOC].fdschema b " & _
    '                        "where dbName = '" & lblDatabaseSelected.Text & "' " & _
    '                        "and SchemaName = '" & dr("Table_Schema") & "' "


    '                        cmd = New SqlCommand(SQL, conn)
    '                        cmd.CommandTimeout = 86400
    '                        If conn.State = ConnectionState.Closed Then
    '                            conn.Open()
    '                        End If
    '                        drSQL = cmd.ExecuteReader
    '                        While drSQL.Read
    '                            If IsDBNull(drSQL.Item("SchemaOwner")) Then
    '                                dr("SchemaOwner") = ""
    '                            Else
    '                                dr("SchemaOwner") = drSQL.Item("SchemaOwner")
    '                            End If
    '                            If IsDBNull(drSQL.Item("SchemaAccess")) Then
    '                                dr("SchemaAccess") = ""
    '                            Else
    '                                dr("SchemaAccess") = drSQL.Item("SchemaAccess")
    '                            End If
    '                            If IsDBNull(drSQL.Item("SchemaDesc")) Then
    '                                dr("SchemaDesc") = ""
    '                            Else
    '                                dr("SchemaDesc") = drSQL.Item("SchemaDesc")
    '                            End If
    '                            If IsDBNull(drSQL.Item("SchemaCaveat")) Then
    '                                dr("SchemaCaveat") = ""
    '                            Else
    '                                dr("SchemaCaveat") = drSQL.Item("SchemaCaveat")
    '                            End If
    '                            If IsDBNull(drSQL.Item("LastUpdated")) Then
    '                                dr("LastUpdated") = ""
    '                            Else
    '                                dr("LastUpdated") = drSQL.Item("LastUpdated")
    '                            End If
    '                            If IsDBNull(drSQL.Item("TechnicalDescription")) Then
    '                                dr("TechnicalDescription") = ""
    '                            Else
    '                                dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
    '                            End If
    '                        End While
    '                        drSQL.Close()
    '                        tbl.Rows.Add(dr)
    '                    End While
    '                    drSQLNS1.Close()
    '                End Using
    '            End Using

    '            gvFDSchemaNS1.DataSource = tbl
    '            gvFDSchemaNS1.DataBind()

    '            ViewState.Add("NS1TBL", tbl)

    '            If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

    '            Else
    '                gvFDSchemaNS1.Columns.Item(0).Visible = False
    '            End If

    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDSchemaNS1.RowCancelingEdit
    '    Try

    '        gvFDSchemaNS1.EditIndex = -1
    '        gvFDSchemaNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '        gvFDSchemaNS1.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDSchemaNS1.RowCommand
    '    Try
    '        If e.CommandName = "ViewTables" Then
    '            lblSchemaSelected.Text = gvFDSchemaNS1.DataKeys(e.CommandArgument).Value.ToString

    '            If gvFDSchemaNS1.Rows(e.CommandArgument).Cells(5).Text = "&nbsp;" Then
    '                lblTableSchema.Text = gvFDSchemaNS1.DataKeys(e.CommandArgument).Value.ToString & ": No description available at this time "
    '            Else
    '                lblTableSchema.Text = gvFDSchemaNS1.DataKeys(e.CommandArgument).Value.ToString & ": " & gvFDSchemaNS1.Rows(e.CommandArgument).Cells(5).Text
    '            End If

    '            lblColumnSchema.Text = lblTableSchema.Text

    '            pnlDatabase.Visible = False
    '            pnlSchema.Visible = False
    '            pnlTables.Visible = True
    '            pnlColumns.Visible = False
    '            gvFDTables.Visible = False
    '            gvFDTablesNS1.Visible = True
    '            LoadTablesGVNS1()

    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDSchemaNS1.RowDeleting
    '    Try
    '        Dim SchemaName As String = ""
    '        ' Dim dBName As String = lblDatabaseSelected.Text

    '        SchemaName = gvFDSchemaNS1.DataKeys(e.RowIndex).Value.ToString

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.fdschema SET  " & _
    '         "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
    '        "WHERE SchemaName = '" & Replace(SchemaName, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
    '        "  Begin  " & _
    '        "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  END " & _
    '        "INSERT INTO DWH.Doc.fdschema  " & _
    '        "([dBName], [SchemaName],[SchemaDesc],[SchemaCaveat],[Active], [LastUpdated], [LastUpdatedPerson])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(SchemaName, "'", "''") & "', " & _
    '        "'', '', 0, sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "') " & _
    '        "END " & _
    '        "Update DWH.Doc.FDColumnData set " & _
    '        "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "Update DWH.Doc.FDTables set " & _
    '        "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' "


    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDSchemaNS1.EditIndex = -1
    '        LoadSchemaGVNS1()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDSchemaNS1.RowEditing
    '    Try

    '        gvFDSchemaNS1.EditIndex = e.NewEditIndex
    '        gvFDSchemaNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '        gvFDSchemaNS1.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDSchemaNS1.RowUpdating
    '    Try
    '        Dim SchemaName As String = ""
    '        Dim Desc As Object
    '        Dim Caveat As String = ""
    '        Dim TechnicalDesc As String = ""
    '        Dim SchemaOwner As String = ""
    '        Dim SchemaAccess As String = ""

    '        SchemaName = gvFDSchemaNS1.DataKeys(e.RowIndex).Value.ToString

    '        Desc = e.NewValues.Item("SchemaDesc")
    '        Caveat = e.NewValues.Item("SchemaCaveat")
    '        TechnicalDesc = e.NewValues.Item("TechnicalDescription")
    '        SchemaOwner = e.NewValues.Item("SchemaOwner")
    '        SchemaAccess = e.NewValues.Item("SchemaAccess")

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.fdschema SET  " & _
    '        "SchemaDesc = '" & Replace(Desc, "'", "''") & "', " & _
    '        "SchemaCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
    '        "Active = 1,  " & _
    '        "LastUpdated = sysdatetime(), " & _
    '        "SchemaOwner = '" & Replace(SchemaOwner, "'", "''") & "', " & _
    '        "SchemaAccess ='" & Replace(SchemaAccess, "'", "''") & "', " & _
    '        "LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "', " & _
    '        "TechnicalDescription = '" & Replace(TechnicalDesc, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(SchemaName, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
    '        "  Begin  " & _
    '        "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  END " & _
    '        "INSERT INTO DWH.Doc.fdschema  " & _
    '        "([dBName], [SchemaName],[SchemaDesc],[SchemaCaveat],[Active], [SchemaOwner], [SchemaAccess],  [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(SchemaName, "'", "''") & "', " & _
    '        "'" & Replace(Desc, "'", "''") & "', " & _
    '        "'" & Replace(Caveat, "'", "''") & "', 1, '" & Replace(SchemaOwner, "'", "''") & "', '" & Replace(SchemaAccess, "'", "''") & "', " & _
    '        "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechnicalDesc, "'", "''") & "') " & _
    '        "END "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDSchemaNS1.EditIndex = -1
    '        LoadSchemaGVNS1()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDSchemaNS1.PageIndexChanging
    '    Try
    '        gvFDSchemaNS1.PageIndex = e.NewPageIndex
    '        If SchemaSort.Text <> "" And SchemaDir.Text <> "" Then
    '            LoadSchemaGVNS1(SchemaSort.Text & " " & SchemaDir.Text)
    '        Else
    '            LoadSchemaGVNS1()
    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDSchemaNS1_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDSchemaNS1.Sorting
    '    Try
    '        Dim dataTable As DataTable = TryCast(gvFDSchemaNS1.DataSource, DataTable)
    '        Dim temp As String = e.SortDirection

    '        If gvFDSchemaNS1.PageIndex <> 0 Then
    '            gvFDSchemaNS1.PageIndex = 0
    '            LoadSchemaGVNS1()
    '            Exit Sub
    '        End If

    '        If e.SortExpression <> "TABLE_SCHEMA" And e.SortExpression <> "cntTable" Then
    '            Exit Sub
    '        End If

    '        If SchemaSort.Text = e.SortExpression Then
    '            If SchemaDir.Text = "asc" Then
    '                LoadSchemaGVNS1(e.SortExpression & " desc ")
    '                SchemaDir.Text = "desc"
    '            Else
    '                LoadSchemaGVNS1(e.SortExpression & " asc ")
    '                SchemaDir.Text = "asc"
    '            End If
    '        Else
    '            SchemaSort.Text = e.SortExpression

    '            If e.SortDirection = SortDirection.Ascending Then
    '                LoadSchemaGVNS1(e.SortExpression & " asc ")
    '                SchemaDir.Text = "asc"
    '            Else
    '                LoadSchemaGVNS1(e.SortExpression & " desc ")
    '                SchemaDir.Text = "desc"
    '            End If
    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
#End Region
#End Region
#Region "GVTables"
    Sub LoadTablesGV(Optional sortDirection As String = "Table_name asc")
        Try
            If pnlTables.Visible = True Then
                Dim tbl As System.Data.DataTable = New DataTable()
                Dim keys(0) As DataColumn
                Dim col As New DataColumn("Table_name")
                keys(0) = col
                tbl.Columns.Add(col)
                col = New DataColumn("cntColumn")
                tbl.Columns.Add(col)
                col = New DataColumn("TableDesc")
                tbl.Columns.Add(col)
                col = New DataColumn("TableOwner")
                tbl.Columns.Add(col)
                col = New DataColumn("TableAccess")
                tbl.Columns.Add(col)
                col = New DataColumn("TableCaveat")
                tbl.Columns.Add(col)
                col = New DataColumn("LastUpdated")
                tbl.Columns.Add(col)
                col = New DataColumn("LDUpdated")
                tbl.Columns.Add(col)
                col = New DataColumn("TechnicalDescription")
                tbl.Columns.Add(col)
                col = New DataColumn("Table_Type")
                tbl.Columns.Add(col)
                col = New DataColumn("Active")
                tbl.Columns.Add(col)

                tbl.PrimaryKey = keys
                ' SQL = "select " & _
                '"a.Table_name, count(COLUMN_NAME) as cntColumn, " & _
                '"b.TableDesc, b.TableCaveat, b.TableOwner, b.TableAccess, " & _
                '"convert(varchar(25),b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription " & _
                '"from " & lblDatabaseSelected.Text & ".INFORMATION_SCHEMA.COLUMNS a " & _
                '"left outer join DWH.Doc.FDTables b on (a.TABLE_SCHEMA = b.schemaname and a.TABLE_CATALOG = b.dBName and a.TABLE_NAME = b.TableName )" & _
                '"and TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
                '"and A.TABLE_NAME = B.TableName	" & _
                '"where b.Active = 1 or b.Active is null " & _
                '" and a.TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
                '"group by a.TABLE_NAME, b.TableDesc , b.TableCaveat, " & _
                '"b.TableOwner, b.TableAccess, b.LastUpdated, b.TechnicalDescription  " & _
                '"order by " & sortDirection

                If lblDatabaseSelected.Text = "MPA" Then
                    SQL = "select * from " & _
                     "(select " & _
                    "a.TableName as Table_name, COUNT(b.ColumnName) as cntColumn,  " & _
                    "TableDesc, TableCaveat, TableOwner, TableAccess,  " & _
                    "convert(varchar(25), a.LastUpdated , 107) as LastUpdated,   " & _
                    "a.TechnicalDescription, '' as Table_Type   " & _
                    "from DWH.DOC.FDTables a " & _
                    "left outer join DWH.DOC.FDColumnData b on (a.dBName = b.dBName and a.SchemaName = b.SchemaName and a.TableName = b.TableName )  " & _
                    "where a.dbname = 'MPA' and a.SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "'  " & _
                    "group by a.TableName, a.TableDesc, a.TableCaveat, a.TableOwner, a.TableAccess, a.LastUpdated,  " & _
                    "a.TechnicalDescription) x  " & _
                    "order by " & sortDirection
                Else
                    SQL = "select " & _
                    "a.Table_name, count(COLUMN_NAME) as cntColumn, " & _
                    "b.TableDesc, b.TableCaveat, b.TableOwner, b.TableAccess, " & _
                    "convert(varchar(25),b.LastUpdated , 107) as LastUpdated, CONVERT(varchar, b.LastUpdated,111) as LDUpdated, b.TechnicalDescription, c.Table_Type, b.Active " & _
                    "from " & lblDatabaseSelected.Text & ".INFORMATION_SCHEMA.COLUMNS a " & _
                    "left outer join DWH.Doc.FDTables b on (a.TABLE_SCHEMA = b.schemaname and a.TABLE_CATALOG = b.dBName and a.TABLE_NAME = b.TableName )" & _
                    "and TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
                    "and A.TABLE_NAME = B.TableName	" & _
                    "left outer join " & lblDatabaseSelected.Text & ".Information_Schema.Tables c " & _
                    "on a.TABLE_CATALOG = c.TABLE_CATALOG and a.TABLE_SCHEMA = c.TABLE_SCHEMA and a.TABLE_NAME = c.TABLE_NAME " & _
                    "where (b.Active = 1 or b.Active is null or 1 = "

                    If chbTablesInactive.Checked = True Then
                        SQL += "1 "
                    Else
                        SQL += "0 "
                    End If

                    SQL += ") and a.TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
                    "group by a.TABLE_NAME, b.TableDesc , b.TableCaveat, " & _
                    "b.TableOwner, b.TableAccess, b.LastUpdated, b.TechnicalDescription, c.Table_Type, b.Active  " & _
                    "order by " & sortDirection
                End If

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd = New SqlCommand(SQL, conn)
                    cmd.CommandTimeout = 86400
                    Dim drSQL As SqlDataReader
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    drSQL = cmd.ExecuteReader
                    Dim dr As DataRow
                    While drSQL.Read
                        dr = tbl.NewRow()
                        If IsDBNull(drSQL.Item("Table_name")) Then
                            dr("Table_name") = ""
                        Else
                            dr("Table_name") = drSQL.Item("Table_name")
                        End If
                        If IsDBNull(drSQL.Item("cntColumn")) Then
                            dr("cntColumn") = ""
                        Else
                            dr("cntColumn") = drSQL.Item("cntColumn")
                        End If
                        If IsDBNull(drSQL.Item("TableDesc")) Then
                            dr("TableDesc") = ""
                        Else
                            dr("TableDesc") = drSQL.Item("TableDesc")
                        End If
                        If IsDBNull(drSQL.Item("TableOwner")) Then
                            dr("TableOwner") = ""
                        Else
                            dr("TableOwner") = drSQL.Item("TableOwner")
                        End If
                        If IsDBNull(drSQL.Item("TableAccess")) Then
                            dr("TableAccess") = ""
                        Else
                            dr("TableAccess") = drSQL.Item("TableAccess")
                        End If
                        If IsDBNull(drSQL.Item("TableCaveat")) Then
                            dr("TableCaveat") = ""
                        Else
                            dr("TableCaveat") = drSQL.Item("TableCaveat")
                        End If
                        If IsDBNull(drSQL.Item("LastUpdated")) Then
                            dr("LastUpdated") = ""
                        Else
                            dr("LastUpdated") = drSQL.Item("LastUpdated")
                        End If
                        If IsDBNull(drSQL.Item("LDUpdated")) Then
                            dr("LDUpdated") = ""
                        Else
                            dr("LDUpdated") = drSQL.Item("LDUpdated")
                        End If
                        If IsDBNull(drSQL.Item("TechnicalDescription")) Then
                            dr("TechnicalDescription") = ""
                        Else
                            dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
                        End If
                        If IsDBNull(drSQL.Item("Table_Type")) Then
                            dr("Table_Type") = ""
                        Else
                            dr("Table_Type") = drSQL.Item("Table_Type")
                        End If
                        If IsDBNull(drSQL.Item("Active")) Then
                            dr("Active") = True
                        ElseIf drSQL.Item("Active") = "1" Then
                            dr("Active") = True
                        Else
                            dr("Active") = False
                        End If

                        tbl.Rows.Add(dr)
                    End While
                    drSQL.Close()
                End Using

                'gvFDTables.DataSource = tbl
                'gvFDTables.DataBind()

                ViewState.Add("TBL", tbl)
                dv = DirectCast(ViewState("TBL"), DataTable).DefaultView

                FilterTable()

                'gvFDTables.EditIndex = -1
                'gvFDTables.DataSource = dv
                'gvFDTables.DataBind()

                If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

                Else
                    gvFDTables.Columns.Item(0).Visible = False
                End If
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDTables_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDTables.RowCancelingEdit
        Try
            gvFDTables.EditIndex = -1
            gvFDTables.DataSource = dv
            gvFDTables.DataBind()
            tblTSelect.Visible = True
            tblTEdit.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDTables_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDTables.RowCommand
        Try
            If e.CommandName = "ViewTableData" Then
                Dim lnks As LinkButton = e.CommandSource
                lblTableSelected.Text = lnks.Text
                'lblTableSelected.Text = gvFDTables.DataKeys(e.CommandSource).Value.ToString

                dv.Sort = "Table_name"
                Dim ind As Integer = dv.Find(lnks.Text)
                Dim rw As DataRow = dv.Table(ind)

                If rw(2).ToString = "&nbsp;" Then
                    lblColumnTable.Text = lblTableSelected.Text & ": No description available at this time "
                Else
                    lblColumnTable.Text = lblTableSelected.Text & ": " & rw(2).ToString
                End If

                pnlTableDefinitions.Visible = False
                pnlColumnDefinitions.Visible = True

                pnlDatabase.Visible = False
                pnlSchema.Visible = False
                pnlTables.Visible = False
                pnlColumns.Visible = True

                'If lblDatabaseSelected.Text = "MPA" Then
                '    gvFDColumnsMPA.Visible = True
                'Else
                gvFDColumns.Visible = True
                'End If
                'gvFDColumnsNS1.Visible = False
                LoadColumnsGV()

                ColumnDir.Text = "asc"
                ColumnSort.Text = "COLUMN_NAME"

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDTables_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDTables.RowDeleting
        Try
            Dim TableName As String = ""

            TableName = gvFDTables.DataKeys(e.RowIndex).Value.ToString

            SQL = "DECLARE @rowcount int " & _
            "UPDATE DWH.Doc.FDTables SET  " & _
            "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "and TableName = '" & Replace(TableName, "'", "''") & "' " & _
            "set @rowcount = @@ROWCOUNT  " & _
            "IF @rowcount = 0 " & _
            "BEGIN  " & _
            "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
            "  Begin  " & _
            "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  END " & _
            "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
            "  Begin " & _
            "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  End " & _
            "INSERT INTO DWH.Doc.FDTables  " & _
            "([dBName], [SchemaName], [TableName], [TableDesc],[TableCaveat],[Active], [LastUpdated], [LastUpdatedPerson])  " & _
            "   VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
            "'" & Replace(TableName, "'", "''") & "', '', '', 0, sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "') " & _
            "END " & _
            "Update DWH.Doc.FDColumnData set " & _
            "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "' " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "and TableName = '" & Replace(TableName, "'", "''") & "' "


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDTables.EditIndex = -1
            LoadTablesGV()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDTables_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDTables.RowEditing
        Try
            gvFDTables.EditIndex = e.NewEditIndex
            gvFDTables.DataSource = dv
            gvFDTables.DataBind()

            Dim txtDesc As TextBox = gvFDTables.Rows(e.NewEditIndex).FindControl("txtTGVDescription")
            Dim lblDesc As Label = gvFDTables.Rows(e.NewEditIndex).FindControl("lblTGVDescription")
            Dim txtTechDesc As TextBox = gvFDTables.Rows(e.NewEditIndex).FindControl("txtTGVTechDesc")
            Dim lblTechDesc As Label = gvFDTables.Rows(e.NewEditIndex).FindControl("lblTGVTechDesc")
            Dim txtCaveat As TextBox = gvFDTables.Rows(e.NewEditIndex).FindControl("txtTGVCaveat")
            Dim lblCaveat As Label = gvFDTables.Rows(e.NewEditIndex).FindControl("lblTGVCaveat")
            'Dim txtOwner As TextBox = gvFDTables.Rows(e.NewEditIndex).FindControl("txtTGVOwner")
            'Dim lblOwner As Label = gvFDTables.Rows(e.NewEditIndex).FindControl("lblTGVOwner")
            'Dim txtAccess As TextBox = gvFDTables.Rows(e.NewEditIndex).FindControl("txtTGVAccess")
            'Dim lblAccess As Label = gvFDTables.Rows(e.NewEditIndex).FindControl("lblTGVAccess")
            Dim chbActive As CheckBox = gvFDTables.Rows(e.NewEditIndex).FindControl("chbTGVActive")
            Dim chbPreActive As CheckBox = gvFDTables.Rows(e.NewEditIndex).FindControl("chbTGVActiveDisabled")

            txtDesc.Visible = True
            lblDesc.Visible = False
            txtTechDesc.Visible = True
            lblTechDesc.Visible = False
            'txtOwner.Visible = True
            'lblOwner.Visible = False
            txtCaveat.Visible = True
            lblCaveat.Visible = False
            tblSSelect.Visible = False
            tblSEdit.Visible = True
            'txtAccess.Visible = True
            'lblAccess.Visible = False
            'txtActive.Visible = True
            'lblActive.Visible = False
            chbActive.Visible = True
            chbPreActive.Visible = False

            For Each canoe As GridViewRow In gvFDTables.Rows
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
    Protected Sub gvFDTables_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDTables.RowUpdating
        Try
            Dim TableName As String = ""
            Dim Desc As Object
            Dim Caveat As String = ""
            Dim TechDesc As String = ""
            Dim TableOwner As String = ""
            Dim TableAccess As String = ""
            Dim TableActive As String = "1"

            Dim txtDesc As TextBox = gvFDTables.Rows(e.RowIndex).FindControl("txtTGVDescription")
            Dim txtTechDesc As TextBox = gvFDTables.Rows(e.RowIndex).FindControl("txtTGVTechDesc")
            Dim txtCaveat As TextBox = gvFDTables.Rows(e.RowIndex).FindControl("txtTGVCaveat")
            'Dim txtOwner As TextBox = gvFDTables.Rows(e.RowIndex).FindControl("txtTGVOwner")
            'Dim txtAccess As TextBox = gvFDTables.Rows(e.RowIndex).FindControl("txtTGVAccess")
            'Dim txtActive As TextBox = gvFDTables.Rows(e.RowIndex).FindControl("txtTGVActive")
            Dim chbActive As CheckBox = gvFDTables.Rows(e.RowIndex).FindControl("chbTGVActive")

            TableName = gvFDTables.DataKeys(e.RowIndex).Value.ToString
            Desc = txtDesc.Text
            Caveat = txtCaveat.Text
            TechDesc = txtTechDesc.Text
            'TableOwner = txtOwner.Text
            'TableAccess = txtAccess.Text
            'SchemaActive = txtActive.Text
            If chbActive.Checked Then
                TableActive = "1"
            Else
                TableActive = "0"
            End If


            'TableName = gvFDTables.DataKeys(e.RowIndex).Value.ToString

            'Desc = e.NewValues.Item("TableDesc")
            'Caveat = e.NewValues.Item("TableCaveat")
            'TechDesc = e.NewValues.Item("TechnicalDescription")
            'TableOwner = e.NewValues.Item("TableOwner")
            'TableAccess = e.NewValues.Item("TableAccess")

            SQL = "DECLARE @rowcount int " & _
            "UPDATE DWH.Doc.FDTables SET  " & _
            "tabledesc = '" & Replace(Desc, "'", "''") & "', " & _
            "tableCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
             "Active = " & TableActive & ", " & _
             "LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "',  " & _
             "TechnicalDescription = '" & Replace(TechDesc, "'", "''") & "' " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "and TableName = '" & Replace(TableName, "'", "''") & "' " & _
            "set @rowcount = @@ROWCOUNT  " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
            "  Begin  " & _
            "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  END " & _
            "  " & _
            "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
            "  Begin " & _
            "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  End " & _
            "INSERT INTO DWH.Doc.FDTables  " & _
            "([dBName], [SchemaName], [TableName], [TableDesc],[TableCaveat],[Active], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
            "   VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
            "'" & Replace(TableName, "'", "''") & "', '" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', " & TableActive & ", " & _
            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechDesc, "'", "''") & "') " & _
            "END "

            '"tableAccess = '" & Replace(TableAccess, "'", "''") & "', " & _
            '"tableowner = '" & Replace(TableOwner, "'", "''") & "', " & _
            '"'" & Replace(TableOwner, "'", "''") & "', '" & Replace(TableAccess, "'", "''") & "', " & _

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDTables.EditIndex = -1
            LoadTablesGV()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDTables_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDTables.PageIndexChanging
        Try
            gvFDTables.PageIndex = e.NewPageIndex
            If TableSort.Text <> "" And TableDir.Text <> "" Then
                LoadTablesGV(TableSort.Text & " " & TableDir.Text)
            Else
                LoadTablesGV()
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvFDTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFDTables.SelectedIndexChanged
        For Each canoe As GridViewRow In gvFDTables.Rows
            If canoe.RowIndex = gvFDTables.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub gvFDTables_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvFDTables.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        End If

        If gvFDTables.EditIndex > -1 Then
            e.Row.Attributes.Remove("onclick")
        End If

        If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

        Else
            e.Row.Cells(0).CssClass = "hidden"
        End If

        If chbTablesInactive.Checked = False Then
            e.Row.Cells(9).CssClass = "hidden"
        End If
    End Sub
    Protected Sub gvFDTables_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDTables.Sorting
        Try
            Dim dataTable As DataTable = TryCast(gvFDTables.DataSource, DataTable)
            Dim temp As String = e.SortDirection

            If gvFDTables.PageIndex <> 0 Then
                gvFDTables.PageIndex = 0
                LoadTablesGV()
                Exit Sub
            End If

            If TableSort.Text = e.SortExpression Then
                If TableDir.Text = "asc" Then
                    LoadTablesGV(e.SortExpression & " desc ")
                    TableDir.Text = "desc"
                Else
                    LoadTablesGV(e.SortExpression & " asc ")
                    TableDir.Text = "asc"
                End If
            Else
                TableSort.Text = e.SortExpression

                If e.SortDirection = SortDirection.Ascending Then
                    LoadTablesGV(e.SortExpression & " asc ")
                    TableDir.Text = "asc"
                Else
                    LoadTablesGV(e.SortExpression & " desc ")
                    TableDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub btnReturnSchema_Click(sender As Object, e As System.EventArgs) Handles btnReturnSchema.Click
        Try
            pnlSchemaDefinitions.Visible = True
            pnlTableDefinitions.Visible = False

            pnlDatabase.Visible = False
            pnlSchema.Visible = True
            pnlTables.Visible = False
            pnlColumns.Visible = False
            LoadSchemaGV()
            'LoadSchemaGVNS1()
            TableDir.Text = ""
            TableSort.Text = ""

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub FilterTable()

        Try

            Dim filter As String = ""

            If Len(txtTblSearchDesc.Text) > 0 Then
                filter += " TableDesc like '%" & txtTblSearchDesc.Text & "%' and "
            End If

            If Len(txtTblSearchTechDesc.Text) > 0 Then
                filter += " TechnicalDescription like '%" & txtTblSearchTechDesc.Text & "%' and "
            End If

            If Len(txtTblSearchCaveat.Text) > 0 Then
                filter += " TableCaveat like '%" & txtTblSearchCaveat.Text & "%' and "
            End If

            'If Len(txtTblSearchOwner.Text) > 0 Then
            '    filter += " TableOwner like '%" & txtTblSearchOwner.Text & "%' and "
            'End If

            'If Len(txtTblSearchAccess.Text) > 0 Then
            '    filter += " TableAccess like '%" & txtTblSearchAccess.Text & "%' and "
            'End If

            If Len(txtTblSearchType.Text) > 0 Then
                filter += " Table_Type like '%" & txtTblSearchType.Text & "%' and "
            End If

            If Len(txtTblSearchUpdate.Text) > 0 Then
                filter += " LDUpdated > '" & Replace(txtTblSearchUpdate.Text, "-", "/") & "' and "
            End If

            If Len(filter) > 1 Then
                dv.RowFilter = Left(filter, Len(filter) - 5)
            Else
                dv.RowFilter = ""
            End If

            gvFDTables.EditIndex = -1
            gvFDTables.DataSource = dv
            gvFDTables.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    'Private Sub txtTblSearchAccess_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchAccess.TextChanged
    '    FilterTable()
    'End Sub


    Private Sub txtTblSearchCaveat_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchCaveat.TextChanged
        FilterTable()
    End Sub

    Private Sub txtTblSearchDesc_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchDesc.TextChanged
        FilterTable()
    End Sub

    'Private Sub txtTblSearchOwner_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchOwner.TextChanged
    '    FilterTable()
    'End Sub

    Private Sub txtTblSearchType_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchType.TextChanged
        FilterTable()
    End Sub

    Private Sub txtTblSearchTechDesc_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchTechDesc.TextChanged
        FilterTable()
    End Sub

    Private Sub txtTblSearchUpdate_TextChanged(sender As Object, e As EventArgs) Handles txtTblSearchUpdate.TextChanged
        FilterTable()
    End Sub


    Sub SortTable(sortex As String)
        Try
            'Dim dataTable As DataTable = TryCast(gvFDSchema.DataSource, DataTable)
            Dim temp As String = gvFDTables.SortDirection

            If gvFDTables.PageIndex <> 0 Then
                gvFDTables.PageIndex = 0
                LoadTablesGV()
                Exit Sub
            End If

            If TableSort.Text = sortex Then
                If TableDir.Text = "asc" Then
                    LoadTablesGV(sortex & " desc ")
                    TableDir.Text = "desc"
                Else
                    LoadTablesGV(sortex & " asc ")
                    TableDir.Text = "asc"
                End If
            Else
                TableSort.Text = sortex

                If gvFDTables.SortDirection = SortDirection.Ascending Then
                    LoadTablesGV(sortex & " asc ")
                    TableDir.Text = "asc"
                Else
                    LoadTablesGV(sortex & " desc ")
                    TableDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub chbTablesInactive_CheckedChanged(sender As Object, e As EventArgs) Handles chbTablesInactive.CheckedChanged

        Try

            If gvFDTables.SortExpression = "" Then
                LoadTablesGV()
            ElseIf gvFDTables.SortDirection = SortDirection.Ascending Then
                LoadTablesGV(gvFDTables.SortExpression & " asc ")
                TableDir.Text = "asc"
            Else
                LoadTablesGV(gvFDTables.SortExpression & " desc ")
                TableDir.Text = "desc"
            End If

            If chbTablesInactive.Checked = True Then
                tableActive.Visible = True
            Else
                tableActive.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub


    Private Sub lnkTbl1_Click(sender As Object, e As EventArgs) Handles lnkTbl1.Click
        SortTable("Table_name")
    End Sub

    Private Sub lnkTbl10_Click(sender As Object, e As EventArgs) Handles lnkTbl10.Click
        SortTable("Active")
    End Sub

    Private Sub lnkTbl2_Click(sender As Object, e As EventArgs) Handles lnkTbl2.Click
        SortTable("TableDesc")
    End Sub

    Private Sub lnkTbl3_Click(sender As Object, e As EventArgs) Handles lnkTbl3.Click
        SortTable("TechnicalDescription")
    End Sub

    Private Sub lnkTbl4_Click(sender As Object, e As EventArgs) Handles lnkTbl4.Click
        SortTable("TableCaveat")
    End Sub

    Private Sub lnkTbl5_Click(sender As Object, e As EventArgs) Handles lnkTbl5.Click
        SortTable("cntColumn")
    End Sub

    'Private Sub lnkTbl6_Click(sender As Object, e As EventArgs) Handles lnkTbl6.Click
    '    SortTable("TableOwner")
    'End Sub

    'Private Sub lnkTbl7_Click(sender As Object, e As EventArgs) Handles lnkTbl7.Click
    '    SortTable("TableAccess")
    'End Sub

    Private Sub lnkTbl8_Click(sender As Object, e As EventArgs) Handles lnkTbl8.Click
        SortTable("Table_Type")
    End Sub

    Private Sub lnkTbl9_Click(sender As Object, e As EventArgs) Handles lnkTbl9.Click
        SortTable("LastUpdated")
    End Sub


#Region "OldTables"
    'Sub LoadTablesGVNS1(Optional sortDirection As String = "Table_name asc")
    '    Try
    '        If pnlTables.Visible = True Then
    '            Dim drSQL As SqlDataReader
    '            Dim drSQLNS1 As SqlDataReader
    '            Dim dr As DataRow
    '            Dim tbl As System.Data.DataTable = New DataTable()
    '            Dim keys(0) As DataColumn
    '            Dim col As New DataColumn("Table_name")

    '            keys(0) = col
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("cntColumn")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TableDesc")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TableOwner")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TableAccess")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TableCaveat")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("LastUpdated")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TechnicalDescription")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("Table_Type")
    '            tbl.Columns.Add(col)

    '            tbl.PrimaryKey = keys
    '            'SQL = "select " & _
    '            '"a.Table_name, count(COLUMN_NAME) as cntColumn " & _
    '            '" from [" & lblDatabaseSelected.Text & "].INFORMATION_SCHEMA.COLUMNS a " & _
    '            '"where  a.TABLE_SCHEMA =  '" & lblSchemaSelected.Text & "' " & _
    '            '"group by a.table_name " & _
    '            '"order by " & sortDirection


    '            SQL = "select " & _
    '            "a.Table_name, count(COLUMN_NAME) as cntColumn, b.Table_Type " & _
    '            " from [" & lblDatabaseSelected.Text & "].INFORMATION_SCHEMA.COLUMNS a " & _
    '            "join (select * from [" & lblDatabaseSelected.Text & "].INFORMATION_SCHEMA.Tables ) b " & _
    '            "on a.Table_Catalog = b.table_Catalog and a.Table_Schema = b.Table_Schema and a.Table_Name = b.Table_Name " & _
    '            "where  a.TABLE_SCHEMA =  '" & lblSchemaSelected.Text & "' " & _
    '            "group by a.table_name, b.Table_Type  " & _
    '            "order by " & sortDirection

    '            Using connNS1 As New SqlConnection(ConfigurationManager.ConnectionStrings("NS1conn").ConnectionString)
    '                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                    cmdNS1 = New SqlCommand(SQL, connNS1)
    '                    cmdNS1.CommandTimeout = 86400

    '                    If connNS1.State = ConnectionState.Closed Then
    '                        connNS1.Open()
    '                    End If
    '                    drSQLNS1 = cmdNS1.ExecuteReader

    '                    While drSQLNS1.Read
    '                        dr = tbl.NewRow()
    '                        If IsDBNull(drSQLNS1.Item("Table_name")) Then
    '                            dr("Table_name") = ""
    '                        Else
    '                            dr("Table_name") = drSQLNS1.Item("Table_name")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("cntColumn")) Then
    '                            dr("cntColumn") = ""
    '                        Else
    '                            dr("cntColumn") = drSQLNS1.Item("cntColumn")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("Table_Type")) Then
    '                            dr("Table_Type") = ""
    '                        Else
    '                            dr("Table_Type") = drSQLNS1.Item("Table_Type")
    '                        End If


    '                        SQL = "select " & _
    '                        "b.TableDesc, b.TableCaveat, b.TableOwner, b.TableAccess, " & _
    '                        "convert(varchar(25),b.LastUpdated , 107) as LastUpdated, b.TechnicalDescription " & _
    '                        "from DWH.Doc.FDTables b " & _
    '                        "where b.dBName = '" & lblDatabaseSelected.Text & "' " & _
    '                        "and b.SchemaName = '" & lblSchemaSelected.Text & "' " & _
    '                        "and b.TableName = '" & dr("Table_name") & "' "


    '                        cmd = New SqlCommand(SQL, conn)
    '                        cmd.CommandTimeout = 86400
    '                        If conn.State = ConnectionState.Closed Then
    '                            conn.Open()
    '                        End If
    '                        drSQL = cmd.ExecuteReader
    '                        While drSQL.Read
    '                            If IsDBNull(drSQL.Item("TableDesc")) Then
    '                                dr("TableDesc") = ""
    '                            Else
    '                                dr("TableDesc") = drSQL.Item("TableDesc")
    '                            End If
    '                            If IsDBNull(drSQL.Item("TableOwner")) Then
    '                                dr("TableOwner") = ""
    '                            Else
    '                                dr("TableOwner") = drSQL.Item("TableOwner")
    '                            End If
    '                            If IsDBNull(drSQL.Item("TableAccess")) Then
    '                                dr("TableAccess") = ""
    '                            Else
    '                                dr("TableAccess") = drSQL.Item("TableAccess")
    '                            End If
    '                            If IsDBNull(drSQL.Item("TableCaveat")) Then
    '                                dr("TableCaveat") = ""
    '                            Else
    '                                dr("TableCaveat") = drSQL.Item("TableCaveat")
    '                            End If
    '                            If IsDBNull(drSQL.Item("LastUpdated")) Then
    '                                dr("LastUpdated") = ""
    '                            Else
    '                                dr("LastUpdated") = drSQL.Item("LastUpdated")
    '                            End If
    '                            If IsDBNull(drSQL.Item("LastUpdated")) Then
    '                                dr("TechnicalDescription") = ""
    '                            Else
    '                                dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
    '                            End If
    '                        End While
    '                        drSQL.Close()
    '                        tbl.Rows.Add(dr)
    '                    End While
    '                    drSQLNS1.Close()
    '                End Using
    '            End Using

    '            gvFDTablesNS1.DataSource = tbl
    '            gvFDTablesNS1.DataBind()

    '            ViewState.Add("NS1TBL", tbl)

    '            If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

    '            Else
    '                gvFDTablesNS1.Columns.Item(0).Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDTablesNS1.RowCancelingEdit
    '    Try
    '        gvFDTablesNS1.EditIndex = -1
    '        gvFDTablesNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '        gvFDTablesNS1.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDTablesNS1.RowCommand
    '    Try
    '        If e.CommandName = "ViewTableData" Then
    '            lblTableSelected.Text = gvFDTablesNS1.DataKeys(e.CommandArgument).Value.ToString

    '            If gvFDTablesNS1.Rows(e.CommandArgument).Cells(4).Text = "&nbsp;" Then
    '                lblColumnTable.Text = gvFDTablesNS1.DataKeys(e.CommandArgument).Value.ToString & ": No description available at this time "
    '            Else
    '                lblColumnTable.Text = gvFDTablesNS1.DataKeys(e.CommandArgument).Value.ToString & ": " & gvFDTablesNS1.Rows(e.CommandArgument).Cells(4).Text
    '            End If
    '            pnlDatabase.Visible = False
    '            pnlSchema.Visible = False
    '            pnlTables.Visible = False
    '            pnlColumns.Visible = True
    '            If lblDatabaseSelected.Text = "MPA" Then
    '                gvFDColumnsMPA.Visible = False
    '            Else
    '                gvFDColumns.Visible = False
    '            End If

    '            gvFDColumnsNS1.Visible = True
    '            LoadColumnsGVNS1()

    '            ColumnDir.Text = "asc"
    '            ColumnSort.Text = "COLUMN_NAME"

    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDTablesNS1.RowDeleting
    '    Try
    '        Dim TableName As String = ""

    '        TableName = gvFDTablesNS1.DataKeys(e.RowIndex).Value.ToString

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.FDTables SET  " & _
    '        "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and TableName = '" & Replace(TableName, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN  " & _
    '        "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
    '        "  Begin  " & _
    '        "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  END " & _
    '        "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
    '        "  Begin " & _
    '        "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  End " & _
    '        "INSERT INTO DWH.Doc.FDTables  " & _
    '        "([dBName], [SchemaName], [TableName], [TableDesc],[TableCaveat],[Active], [LastUpdated], [LastUpdatedPerson])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
    '        "'" & Replace(TableName, "'", "''") & "', '', '', 0, sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "') " & _
    '        "END " & _
    '        "Update DWH.Doc.FDColumnData set " & _
    '        "active = 0, LastUpdated = sysdatetime(), lastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and TableName = '" & Replace(TableName, "'", "''") & "' "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDTablesNS1.EditIndex = -1
    '        LoadTablesGV()
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDTablesNS1.RowEditing
    '    Try
    '        gvFDTablesNS1.EditIndex = e.NewEditIndex
    '        gvFDTablesNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '        gvFDTablesNS1.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDTablesNS1.RowUpdating
    '    Try
    '        Dim TableName As String = ""
    '        Dim Desc As Object
    '        Dim Caveat As String = ""
    '        Dim TechDesc As String = ""
    '        Dim TableOwner As String = ""
    '        Dim TableAccess As String = ""

    '        TableName = gvFDTablesNS1.DataKeys(e.RowIndex).Value.ToString
    '        Desc = e.NewValues.Item("TableDesc")
    '        Caveat = e.NewValues.Item("TableCaveat")
    '        TechDesc = e.NewValues.Item("TechnicalDescription")
    '        TableOwner = e.NewValues.Item("TableOwner")
    '        TableAccess = e.NewValues.Item("TableAccess")

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.FDTables SET  " & _
    '        "tabledesc = '" & Replace(Desc, "'", "''") & "', " & _
    '        "tableCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
    '        "tableAccess = '" & Replace(TableAccess, "'", "''") & "', " & _
    '        "tableowner = '" & Replace(TableOwner, "'", "''") & "', " & _
    '         "Active = 1, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "',  " & _
    '         "TechnicalDescription = '" & Replace(TechDesc, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and TableName = '" & Replace(TableName, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
    '        "  Begin  " & _
    '        "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  END " & _
    '        "  " & _
    '        "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
    '        "  Begin " & _
    '        "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  End " & _
    '        "INSERT INTO DWH.Doc.FDTables  " & _
    '        "([dBName], [SchemaName], [TableName], [TableDesc],[TableCaveat],[Active], [TableOwner], [TableAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
    '        "'" & Replace(TableName, "'", "''") & "', '" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
    '        "'" & Replace(TableOwner, "'", "''") & "', '" & Replace(TableAccess, "'", "''") & "', " & _
    '        "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechDesc, "'", "''") & "') " & _
    '        "END "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDTablesNS1.EditIndex = -1
    '        LoadTablesGV()
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDTablesNS1.PageIndexChanging
    '    Try
    '        gvFDTablesNS1.PageIndex = e.NewPageIndex
    '        If TableSort.Text <> "" And TableDir.Text <> "" Then
    '            LoadTablesGVNS1(TableSort.Text & " " & TableDir.Text)
    '        Else
    '            LoadTablesGVNS1()
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDTablesNS1_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDTablesNS1.Sorting
    '    Try
    '        Dim dataTable As DataTable = TryCast(gvFDTablesNS1.DataSource, DataTable)
    '        Dim temp As String = e.SortDirection

    '        If gvFDTablesNS1.PageIndex <> 0 Then
    '            gvFDTablesNS1.PageIndex = 0
    '            LoadTablesGVNS1()
    '            Exit Sub
    '        End If

    '        If e.SortExpression <> "Table_name" And e.SortExpression <> "cntColumn" Then
    '            Exit Sub
    '        End If

    '        If TableSort.Text = e.SortExpression Then
    '            If TableDir.Text = "asc" Then
    '                LoadTablesGVNS1(e.SortExpression & " desc ")
    '                TableDir.Text = "desc"
    '            Else
    '                LoadTablesGV(e.SortExpression & " asc ")
    '                TableDir.Text = "asc"
    '            End If
    '        Else
    '            TableSort.Text = e.SortExpression

    '            If e.SortDirection = SortDirection.Ascending Then
    '                LoadTablesGVNS1(e.SortExpression & " asc ")
    '                TableDir.Text = "asc"
    '            Else
    '                LoadTablesGVNS1(e.SortExpression & " desc ")
    '                TableDir.Text = "desc"
    '            End If
    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
#End Region
#End Region
#Region "GVTableData"
    Sub LoadColumnsGV(Optional sortDirection As String = "Column_Name asc")
        Try
            Dim tbl As System.Data.DataTable = New DataTable()
            Dim keys(0) As DataColumn
            If pnlColumns.Visible = True Then
                tbl = New DataTable()
                Dim col As New DataColumn("COLUMN_NAME")
                keys(0) = col
                tbl.Columns.Add(col)
                col = New DataColumn("DATA_TYPE")
                tbl.Columns.Add(col)
                col = New DataColumn("Size")
                tbl.Columns.Add(col)
                col = New DataColumn("COLUMN_DEFAULT")
                tbl.Columns.Add(col)
                col = New DataColumn("IS_NULLABLE")
                tbl.Columns.Add(col)
                col = New DataColumn("isKey")
                tbl.Columns.Add(col)
                col = New DataColumn("value")
                tbl.Columns.Add(col)
                col = New DataColumn("ColumnDesc")
                tbl.Columns.Add(col)
                col = New DataColumn("ColumnOwner")
                tbl.Columns.Add(col)
                col = New DataColumn("ColumnAccess")
                tbl.Columns.Add(col)
                col = New DataColumn("columnCaveat")
                tbl.Columns.Add(col)
                col = New DataColumn("ORDINAL_POSITION")
                tbl.Columns.Add(col)
                col = New DataColumn("LastUpdated")
                tbl.Columns.Add(col)
                col = New DataColumn("TechnicalDescription")
                tbl.Columns.Add(col)
                col = New DataColumn("Active")
                tbl.Columns.Add(col)
                col = New DataColumn("LDUpdated")
                tbl.Columns.Add(col)

                tbl.PrimaryKey = keys

                If lblDatabaseSelected.Text = "MPA" Then
                    SQL = "select * from " & _
                    "(select " & _
                    "ColumnName as Column_Name, DataType as Data_Type, Size as Size, '' as Column_Default, " & _
                    "'' as IS_Nullable, Keys as isKey, '' as value, ColumnOwner, ColumnAccess, ColumnDesc , columnCaveat , ColumnOrder as Ordinal_position, " & _
                    "convert(varchar(25),  LastUpdated , 107) as LastUpdated,  TechnicalDescription, Active, CONVERT(varchar, LastUpdated,111) as LDUpdated " & _
                    "from DWH.DOC.FDColumnData " & _
                    "where dBName = 'MPA' and SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
                    "and TableName = '" & Replace(lblTableSelected.Text, "'", "''") & "' ) x " & _
                    "order by " & sortDirection
                Else
                    SQL = "select " & _
                    "a.COLUMN_NAME, a.DATA_TYPE, " & _
                    "a.Size, " & _
                    "a.COLUMN_DEFAULT, a.IS_NULLABLE, " & _
                    "case " & _
                    "when b.TABLE_NAME is not null then 'True' " & _
                    "else Null " & _
                    "end isKey , c.value, d.ColumnOwner, d.ColumnAccess, " & _
                    "d.ColumnDesc , d.columnCaveat, " & _
                    "a.ORDINAL_POSITION, convert(varchar(25), d.LastUpdated , 107) as LastUpdated, CONVERT(varchar, d.LastUpdated,111) as LDUpdated, d.TechnicalDescription, d.Active " & _
                    "from " & _
                    "(select " & _
                    "COLUMN_NAME, DATA_TYPE, " & _
                    "case " & _
                    "when CHARACTER_MAXIMUM_LENGTH = -1 then 'max' " & _
                    "when DATA_TYPE in ('binary', 'char', 'datetime2', 'datetimeoffset', 'nchar', 'nvarchar', 'time', 'varbinary', 'varchar') " & _
                              "then CAST(CHARACTER_MAXIMUM_LENGTH as varchar) " & _
                    "when DATA_TYPE in ('decimarl','numeric') then  cast(NUMERIC_PRECISION as varchar) +','+ cast(NUMERIC_SCALE as varchar)  " & _
                    "end Size ," & _
                    "CHARACTER_MAXIMUM_LENGTH , " & _
                    "NUMERIC_PRECISION, NUMERIC_SCALE, COLUMN_DEFAULT, " & _
                    "IS_NULLABLE, ORDINAL_POSITION   " & _
                    "from " & lblDatabaseSelected.Text & ".INFORMATION_SCHEMA.COLUMNS " & _
                    "where TABLE_CATALOG = '" & lblDatabaseSelected.Text & "' and TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
                    "and TABLE_NAME='" & lblTableSelected.Text & "') a " & _
                    "left outer join (    " & _
                    "SELECT distinct " & _
                    "TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME       " & _
                    "FROM " & lblDatabaseSelected.Text & ".information_schema.key_column_usage " & _
                    "WHERE TABLE_NAME = '" & lblTableSelected.Text & "' " & _
                    "and TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
                    "and TABLE_CATALOG = '" & lblDatabaseSelected.Text & "' ) b on a.COLUMN_NAME = b.COLUMN_NAME " & _
                    "outer apply ( " & _
                    "SELECT objtype, objname, name, value " & _
                    "FROM fn_listextendedproperty (NULL, 'schema', " & _
                    "'" & lblSchemaSelected.Text & "', 'table', '" & lblTableSelected.Text & "', 'column', a.COLUMN_NAME)) c  " & _
                    "left outer join " & _
                    "(select columnName, ColumnDesc, columnOwner, ColumnAccess, " & _
                    "columnCaveat, LastUpdated, TechnicalDescription, Active " & _
                    "from DWH.Doc.FDColumnData " & _
                    "where TableName ='" & lblTableSelected.Text & "' " & _
                    "and SchemaName = '" & lblSchemaSelected.Text & "' " & _
                    "and dBName = '" & lblDatabaseSelected.Text & "' ) d on a.COLUMN_NAME = d.ColumnName " & _
                    " order by " & sortDirection
                End If

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd = New SqlCommand(SQL, conn)
                    cmd.CommandTimeout = 86400
                    Dim drSQL As SqlDataReader
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    drSQL = cmd.ExecuteReader
                    Dim dr As DataRow
                    While drSQL.Read
                        dr = tbl.NewRow()
                        If IsDBNull(drSQL.Item("COLUMN_NAME")) Then
                            dr("COLUMN_NAME") = ""
                        Else
                            dr("COLUMN_NAME") = drSQL.Item("COLUMN_NAME")
                        End If
                        If IsDBNull(drSQL.Item("DATA_TYPE")) Then
                            dr("DATA_TYPE") = ""
                        Else
                            dr("DATA_TYPE") = drSQL.Item("DATA_TYPE")
                        End If
                        If IsDBNull(drSQL.Item("Size")) Then
                            dr("Size") = ""
                        Else
                            dr("Size") = drSQL.Item("Size")
                        End If
                        If IsDBNull(drSQL.Item("COLUMN_DEFAULT")) Then
                            dr("COLUMN_DEFAULT") = ""
                        Else
                            dr("COLUMN_DEFAULT") = Replace(Replace(drSQL.Item("COLUMN_DEFAULT"), "('", ""), "')", "")
                        End If
                        If IsDBNull(drSQL.Item("IS_NULLABLE")) Then
                            dr("IS_NULLABLE") = 0
                        Else
                            dr("IS_NULLABLE") = drSQL.Item("IS_NULLABLE")
                        End If
                        If IsDBNull(drSQL.Item("isKey")) Then
                            dr("isKey") = ""
                        Else
                            dr("isKey") = drSQL.Item("isKey")
                        End If
                        If IsDBNull(drSQL.Item("value")) Then
                            dr("value") = ""
                        Else
                            dr("value") = drSQL.Item("value")
                        End If
                        If IsDBNull(drSQL.Item("ColumnDesc")) Then
                            dr("ColumnDesc") = ""
                        Else
                            dr("ColumnDesc") = drSQL.Item("ColumnDesc")
                        End If

                        If IsDBNull(drSQL.Item("TechnicalDescription")) Then
                            dr("TechnicalDescription") = ""
                        Else
                            dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
                        End If
                        If IsDBNull(drSQL.Item("ColumnOwner")) Then
                            dr("ColumnOwner") = ""
                        Else
                            dr("ColumnOwner") = drSQL.Item("ColumnOwner")
                        End If
                        If IsDBNull(drSQL.Item("ColumnAccess")) Then
                            dr("ColumnAccess") = ""
                        Else
                            dr("ColumnAccess") = drSQL.Item("ColumnAccess")
                        End If

                        If IsDBNull(drSQL.Item("columnCaveat")) Then
                            dr("columnCaveat") = ""
                        Else
                            dr("columnCaveat") = drSQL.Item("columnCaveat")
                        End If
                        If IsDBNull(drSQL.Item("ORDINAL_POSITION")) Then
                            dr("ORDINAL_POSITION") = ""
                        Else
                            dr("ORDINAL_POSITION") = drSQL.Item("ORDINAL_POSITION")
                        End If
                        If IsDBNull(drSQL.Item("LastUpdated")) Then
                            dr("LastUpdated") = ""
                        Else
                            dr("LastUpdated") = drSQL.Item("LastUpdated")
                        End If
                        If IsDBNull(drSQL.Item("Active")) Then
                            dr("Active") = True
                        ElseIf drSQL.Item("Active") = "1" Then
                            dr("Active") = True
                        Else
                            dr("Active") = False
                        End If
                        tbl.Rows.Add(dr)
                    End While
                    drSQL.Close()
                End Using

                ''If lblDatabaseSelected.Text = "MPA" Then
                ''    gvFDColumnsMPA.DataSource = tbl
                ''    gvFDColumnsMPA.DataBind()
                ''Else
                'gvFDColumns.DataSource = tbl
                'gvFDColumns.DataBind()
                ''End If
                ViewState.Add("TBL", tbl)
                dv = DirectCast(ViewState("TBL"), DataTable).DefaultView

                FilterColumn()

                'gvFDColumns.EditIndex = -1
                'gvFDColumns.DataSource = dv
                'gvFDColumns.DataBind()

                If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

                Else
                    gvFDColumns.Columns.Item(0).Visible = False
                    'gvFDColumnsMPA.Columns.Item(0).Visible = False

                End If
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    'DWH Specific Column GridView
    Protected Sub gvFDColumns_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDColumns.RowCancelingEdit
        Try
            gvFDColumns.EditIndex = -1
            gvFDColumns.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvFDColumns.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDColumns_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDColumns.RowCommand
        Try

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDColumns_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDColumns.RowDeleting
        Try
            Dim ColumnName As String = ""

            ColumnName = gvFDColumns.DataKeys(e.RowIndex).Value.ToString

            SQL = "DECLARE @rowcount int " & _
            "UPDATE DWH.Doc.FDColumnData SET  " & _
             "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "and TableName = '" & Replace(lblTableSelected.Text, "'", "''") & "' " & _
            "and ColumnName = '" & Replace(ColumnName, "'", "''") & "' " & _
            "set @rowcount = @@ROWCOUNT  " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "INSERT INTO DWH.Doc.FDColumnData  " & _
            "([dBName], [SchemaName], [TableName], [ColumnName], [ColumnDesc],[ColumnCaveat], [Active], [LastUpdated], [LastUpdatedPerson])  " & _
            "   VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
            "'" & Replace(lblTableSelected.Text, "'", "''") & "', '" & Replace(ColumnName, "'", "''") & "', '', '', 0, " & _
            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "' ) " & _
            "END "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDColumns.EditIndex = -1
            LoadColumnsGV()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDColumns_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDColumns.RowEditing
        Try

            gvFDColumns.EditIndex = e.NewEditIndex
            gvFDColumns.DataSource = dv
            gvFDColumns.DataBind()

            Dim txtDesc As TextBox = gvFDColumns.Rows(e.NewEditIndex).FindControl("txtCGVDescription")
            Dim lblDesc As Label = gvFDColumns.Rows(e.NewEditIndex).FindControl("lblCGVDescription")
            Dim txtTechDesc As TextBox = gvFDColumns.Rows(e.NewEditIndex).FindControl("txtCGVTechDesc")
            Dim lblTechDesc As Label = gvFDColumns.Rows(e.NewEditIndex).FindControl("lblCGVTechDesc")
            Dim txtCaveat As TextBox  ' = gvFDColumns.Rows(e.NewEditIndex).FindControl("txtCGVCaveat")
            txtCaveat = gvFDColumns.Rows(e.NewEditIndex).FindControl("txtCGVCaveat")

            Dim lblCaveat As Label = gvFDColumns.Rows(e.NewEditIndex).FindControl("lblCGVCaveat")
            'Dim txtOwner As TextBox = gvFDColumns.Rows(e.NewEditIndex).FindControl("txtCGVOwner")
            'Dim lblOwner As Label = gvFDColumns.Rows(e.NewEditIndex).FindControl("lblCGVOwner")
            'Dim txtAccess As TextBox = gvFDColumns.Rows(e.NewEditIndex).FindControl("txtCGVAccess")
            'Dim lblAccess As Label = gvFDColumns.Rows(e.NewEditIndex).FindControl("lblCGVAccess")
            Dim chbActive As CheckBox = gvFDColumns.Rows(e.NewEditIndex).FindControl("chbSGVActive")
            Dim chbPreActive As CheckBox = gvFDColumns.Rows(e.NewEditIndex).FindControl("chbSGVActiveDisabled")





            txtDesc.Visible = True
            lblDesc.Visible = False
            txtTechDesc.Visible = True
            lblTechDesc.Visible = False
            'txtOwner.Visible = True
            'lblOwner.Visible = False
            txtCaveat.Visible = True
            lblCaveat.Visible = False
            tblSSelect.Visible = False
            tblSEdit.Visible = True
            'txtAccess.Visible = True
            'lblAccess.Visible = False
            chbActive.Visible = True
            chbPreActive.Visible = False

            'gvFDColumns.Columns(0).ItemStyle.Width = "100px"

            For Each canoe As GridViewRow In gvFDColumns.Rows
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
    Protected Sub gvFDColumns_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDColumns.RowUpdating
        Try
            Dim ColumnName As String = ""
            Dim Desc As Object
            Dim Caveat As String = ""
            Dim TechDesc As String = ""
            'Dim ColumnAccess As String = ""
            'Dim ColumnOwner As String = ""
            Dim ColumnActive As String = "1"

            Dim txtDesc As TextBox = gvFDColumns.Rows(e.RowIndex).FindControl("txtCGVDescription")
            Dim txtTechDesc As TextBox = gvFDColumns.Rows(e.RowIndex).FindControl("txtCGVTechDesc")
            Dim txtCaveat As TextBox = gvFDColumns.Rows(e.RowIndex).FindControl("txtCGVCaveat")
            'Dim txtOwner As TextBox = gvFDColumns.Rows(e.RowIndex).FindControl("txtCGVOwner")
            'Dim txtAccess As TextBox = gvFDColumns.Rows(e.RowIndex).FindControl("txtCGVAccess")
            Dim chbActive As CheckBox = gvFDColumns.Rows(e.RowIndex).FindControl("chbCGVActive")

            ColumnName = gvFDColumns.DataKeys(e.RowIndex).Value.ToString
            Desc = txtDesc.Text
            Caveat = txtCaveat.Text
            TechDesc = txtTechDesc.Text
            'ColumnOwner = txtOwner.Text
            'ColumnAccess = txtAccess.Text
            'SchemaActive = txtActive.Text
            If chbActive.Checked Then
                ColumnActive = "1"
            Else
                ColumnActive = "0"
            End If

            'ColumnName = gvFDColumns.DataKeys(e.RowIndex).Value.ToString
            'Desc = e.NewValues.Item("ColumnDesc")
            'Caveat = e.NewValues.Item("columnCaveat")
            'TechDesc = e.NewValues.Item("TechnicalDescription")
            'ColumnOwner = e.NewValues.Item("ColumnOwner")
            'ColumnAccess = e.NewValues.Item("ColumnAccess")


            SQL = "DECLARE @rowcount int " & _
            "UPDATE DWH.Doc.FDColumnData SET  " & _
            "Columndesc = '" & Replace(Desc, "'", "''") & "', " & _
            "ColumnCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
            "Active = " & ColumnActive & ", LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "',    " & _
            "TechnicalDescription = '" & Replace(TechDesc, "'", "''") & "' " & _
            "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
            "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
            "and tablename = '" & Replace(lblTableSelected.Text, "'", "''") & "' " & _
            "and ColumnName = '" & Replace(ColumnName, "'", "''") & "' " & _
            "set @rowcount = @@ROWCOUNT  " & _
            "IF @rowcount = 0 " & _
            "BEGIN " & _
            "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
            "  Begin  " & _
            "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  END " & _
            "  " & _
            "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
            "  Begin " & _
            "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            "  End " & _
            "If (select count(*) from DWH.Doc.FDTables where dbName = '" & lblDatabaseSelected.Text & "' and Schemaname = '" & lblSchemaSelected.Text & "' and TableName = '" & lblTableSelected.Text & "') = 0  " & _
            "  Begin " & _
            "    Insert into DWH.Doc.FDTables values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '" & lblTableSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
            " End " & _
            "INSERT INTO DWH.Doc.FDColumnData  " & _
            "([dBName], [SchemaName], [TableName], [ColumnName], [ColumnDesc],[ColumnCaveat],[Active], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
            "   VALUES  " & _
            "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
            "'" & Replace(lblTableSelected.Text, "'", "''") & "', '" & Replace(ColumnName, "'", "''") & "', " & _
            "'" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
            "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechDesc, "'", "''") & "' ) " & _
            "END "

            '"ColumnOwner = '" & Replace(ColumnOwner, "'", "''") & "', " & _
            '"ColumnAccess = '" & Replace(ColumnAccess, "'", "''") & "', " & _
            '"'" & Replace(ColumnOwner, "'", "''") & "', '" & Replace(ColumnAccess, "'", "''") & "', " & _

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
            End Using

            gvFDColumns.EditIndex = -1
            LoadColumnsGV()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDColumns_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDColumns.PageIndexChanging
        Try
            gvFDColumns.PageIndex = e.NewPageIndex
            If ColumnSort.Text <> "" And ColumnDir.Text <> "" Then
                LoadColumnsGV(ColumnSort.Text & " " & ColumnDir.Text)
            Else
                LoadColumnsGV()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub gvFDColumns_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvFDColumns.SelectedIndexChanged
        For Each canoe As GridViewRow In gvFDColumns.Rows
            If canoe.RowIndex = gvFDColumns.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub gvFDColumns_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvFDColumns.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If

            If gvFDColumns.EditIndex > -1 Then
                e.Row.Attributes.Remove("onclick")
            End If

            If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

            Else
                e.Row.Cells(0).CssClass = "hidden"
            End If

            If chbColumnsInactive.Checked = False Then
                e.Row.Cells(7).CssClass = "hidden"
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvFDColumns_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDColumns.Sorting
        Try
            Dim dataTable As DataTable = TryCast(gvFDColumns.DataSource, DataTable)
            Dim temp As String = e.SortDirection

            If gvFDColumns.PageIndex <> 0 Then
                gvFDColumns.PageIndex = 0
                LoadColumnsGV()
                Exit Sub
            End If

            If ColumnSort.Text = e.SortExpression Then
                If ColumnDir.Text = "asc" Then
                    LoadColumnsGV(e.SortExpression & " desc ")
                    ColumnDir.Text = "desc"
                Else
                    LoadColumnsGV(e.SortExpression & " asc ")
                    ColumnDir.Text = "asc"
                End If
            Else
                ColumnSort.Text = e.SortExpression

                If e.SortDirection = SortDirection.Ascending Then
                    LoadColumnsGV(e.SortExpression & " asc ")
                    ColumnDir.Text = "asc"
                Else
                    LoadColumnsGV(e.SortExpression & " desc ")
                    ColumnDir.Text = "desc"
                End If
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnReturnTables_Click(sender As Object, e As System.EventArgs) Handles btnReturnTables.Click
        Try

            pnlTableDefinitions.Visible = True
            pnlColumnDefinitions.Visible = False

            pnlDatabase.Visible = False
            pnlSchema.Visible = False
            pnlTables.Visible = True
            pnlColumns.Visible = False
            LoadTablesGV()
            'LoadTablesGVNS1()
            ColumnDir.Text = ""
            ColumnSort.Text = ""
            gvFDColumns.Visible = False
            'gvFDColumnsMPA.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub FilterColumn()

        Try

            Dim filter As String = ""

            If Len(txtColumnSearchDesc.Text) > 0 Then
                filter += " ColumnDesc like '%" & txtColumnSearchDesc.Text & "%' and "
            End If

            If Len(txtColumnSearchTechDesc.Text) > 0 Then
                filter += " TechnicalDescription like '%" & txtColumnSearchTechDesc.Text & "%' and "
            End If

            If Len(txtColumnSearchCaveat.Text) > 0 Then
                filter += " ColumnCaveat like '%" & txtColumnSearchCaveat.Text & "%' and "
            End If

            'If Len(txtColumnSearchOwner.Text) > 0 Then
            '    filter += " SchemaOwner like '%" & txtColumnSearchOwner.Text & "%' and "
            'End If

            'If Len(txtColumnSearchAccess.Text) > 0 Then
            '    filter += " SchemaAccess like '%" & txtColumnSearchAccess.Text & "%' and "
            'End If

            If Len(txtColumnSearchType.Text) > 0 Then
                filter += " DATA_TYPE like '%" & Replace(txtColumnSearchType.Text, "-", "/") & "%' and "
            End If

            If Len(txtColumnSearchUpdate.Text) > 0 Then
                filter += " LDUpdated > '" & Replace(txtColumnSearchUpdate.Text, "-", "/") & "' and "
            End If

            If Len(filter) > 1 Then
                dv.RowFilter = Left(filter, Len(filter) - 5)
            Else
                dv.RowFilter = ""
            End If

            gvFDColumns.EditIndex = -1
            gvFDColumns.DataSource = dv
            gvFDColumns.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub



    Private Sub txtColumnSearchCaveat_TextChanged(sender As Object, e As EventArgs) Handles txtColumnSearchCaveat.TextChanged

        FilterColumn()

    End Sub

    Private Sub txtColumnSearchDesc_TextChanged(sender As Object, e As EventArgs) Handles txtColumnSearchDesc.TextChanged

        FilterColumn()

    End Sub

    Private Sub txtColumnSearchTechDesc_TextChanged(sender As Object, e As EventArgs) Handles txtColumnSearchTechDesc.TextChanged

        FilterColumn()

    End Sub

    Private Sub txtColumnSearchType_TextChanged(sender As Object, e As EventArgs) Handles txtColumnSearchType.TextChanged

        FilterColumn()

    End Sub

    Private Sub txtColumnSearchUpdate_TextChanged(sender As Object, e As EventArgs) Handles txtColumnSearchUpdate.TextChanged

        FilterColumn()

    End Sub

    Sub SortColumns(sortex As String)
        Try
            'Dim dataTable As DataTable = TryCast(gvFDSchema.DataSource, DataTable)
            Dim temp As String = gvFDColumns.SortDirection

            If gvFDColumns.PageIndex <> 0 Then
                gvFDColumns.PageIndex = 0
                LoadColumnsGV()
                Exit Sub
            End If

            If ColumnSort.Text = sortex Then
                If ColumnDir.Text = "asc" Then
                    LoadColumnsGV(sortex & " desc ")
                    ColumnDir.Text = "desc"
                Else
                    LoadColumnsGV(sortex & " asc ")
                    ColumnDir.Text = "asc"
                End If
            Else
                ColumnSort.Text = sortex

                If gvFDColumns.SortDirection = SortDirection.Ascending Then
                    LoadColumnsGV(sortex & " asc ")
                    ColumnDir.Text = "asc"
                Else
                    LoadColumnsGV(sortex & " desc ")
                    ColumnDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub chbColumnsInactive_CheckedChanged(sender As Object, e As EventArgs) Handles chbColumnsInactive.CheckedChanged

        Try

            If gvFDColumns.SortExpression = "" Then
                LoadColumnsGV()
            ElseIf gvFDColumns.SortDirection = SortDirection.Ascending Then
                LoadColumnsGV(gvFDColumns.SortExpression & " asc ")
                ColumnDir.Text = "asc"
            Else
                LoadColumnsGV(gvFDColumns.SortExpression & " desc ")
                ColumnDir.Text = "desc"
            End If

            If chbColumnsInactive.Checked = True Then
                columnActive.Visible = True
            Else
                columnActive.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub lnkCol1_Click(sender As Object, e As EventArgs) Handles lnkCol1.Click
        SortColumns("COLUMN_NAME")
    End Sub

    Private Sub lnkCol10_Click(sender As Object, e As EventArgs) Handles lnkCol10.Click
        SortColumns("Active")
    End Sub

    Private Sub lnkCol2_Click(sender As Object, e As EventArgs) Handles lnkCol2.Click
        SortColumns("ColumnDesc")
    End Sub

    Private Sub lnkCol3_Click(sender As Object, e As EventArgs) Handles lnkCol3.Click
        SortColumns("TechnicalDescription")
    End Sub

    Private Sub lnkCol4_Click(sender As Object, e As EventArgs) Handles lnkCol4.Click
        SortColumns("columnCaveat")
    End Sub

    Private Sub lnkCol8_Click(sender As Object, e As EventArgs) Handles lnkCol8.Click
        SortColumns("DATA_TYPE")
    End Sub

    Private Sub lnkCol9_Click(sender As Object, e As EventArgs) Handles lnkCol9.Click
        SortColumns("LastUpdated")
    End Sub

#End Region




#Region "OldColumns"
    'MPA Specific Column GridView
    'Protected Sub gvFDColumnsMPA_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDColumnsMPA.RowCancelingEdit
    '    Try
    '        gvFDColumnsMPA.EditIndex = -1
    '        gvFDColumnsMPA.DataSource = DirectCast(ViewState("TBL"), DataTable)
    '        gvFDColumnsMPA.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsMPA_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDColumnsMPA.RowDeleting
    '    Try
    '        Dim ColumnName As String = ""

    '        ColumnName = gvFDColumnsMPA.DataKeys(e.RowIndex).Value.ToString

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.FDColumnData SET  " & _
    '         "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and TableName = '" & Replace(lblTableSelected.Text, "'", "''") & "' " & _
    '        "and ColumnName = '" & Replace(ColumnName, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "INSERT INTO DWH.Doc.FDColumnData  " & _
    '        "([dBName], [SchemaName], [TableName], [ColumnName], [ColumnDesc],[ColumnCaveat], [Active], [LastUpdated], [LastUpdatedPerson])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
    '        "'" & Replace(lblTableSelected.Text, "'", "''") & "', '" & Replace(ColumnName, "'", "''") & "', '', '', 0, " & _
    '        "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "' ) " & _
    '        "END "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDColumnsMPA.EditIndex = -1
    '        LoadColumnsGV()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsMPA_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDColumnsMPA.RowEditing
    '    Try
    '        gvFDColumnsMPA.EditIndex = e.NewEditIndex
    '        gvFDColumnsMPA.DataSource = DirectCast(ViewState("TBL"), DataTable)
    '        gvFDColumnsMPA.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsMPA_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDColumnsMPA.RowUpdating
    '    Try
    '        Dim ColumnOrder As String = ""
    '        Dim DataSize As String = ""
    '        Dim DataType As String = ""
    '        Dim IsKey As String = ""
    '        Dim ColumnName As String = ""
    '        Dim Desc As Object
    '        Dim Caveat As String = ""
    '        Dim TechDesc As String = ""
    '        Dim ColumnAccess As String = ""
    '        Dim ColumnOwner As String = ""
    '        Dim ddlDataType As DropDownList
    '        Dim ddlKey As DropDownList

    '        ColumnName = gvFDColumnsMPA.DataKeys(e.RowIndex).Value.ToString

    '        ddlDataType = gvFDColumnsMPA.Rows(e.RowIndex).FindControl("ddlDataType")
    '        DataType = ddlDataType.SelectedValue.ToString

    '        ddlKey = gvFDColumnsMPA.Rows(e.RowIndex).FindControl("ddlIsKey")
    '        IsKey = ddlKey.SelectedValue.ToString

    '        DataSize = e.NewValues.Item("Size")
    '        ColumnOrder = e.NewValues.Item("ORDINAL_POSITION")
    '        Desc = e.NewValues.Item("ColumnDesc")
    '        Caveat = e.NewValues.Item("columnCaveat")
    '        TechDesc = e.NewValues.Item("TechnicalDescription")
    '        ColumnOwner = e.NewValues.Item("ColumnOwner")
    '        ColumnAccess = e.NewValues.Item("ColumnAccess")

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.FDColumnData SET  " & _
    '        "ColumnOrder = '" & Replace(ColumnOrder, "'", "''") & "', " & _
    '        "DataType = '" & Replace(DataType, "'", "''") & "', " & _
    '        "Size = '" & Replace(DataSize, "'", "''") & "', " & _
    '        "Keys = '" & Replace(IsKey, "'", "''") & "', " & _
    '        "Columndesc = '" & Replace(Desc, "'", "''") & "', " & _
    '        "ColumnCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
    '        "ColumnOwner = '" & Replace(ColumnOwner, "'", "''") & "', " & _
    '        "ColumnAccess = '" & Replace(ColumnAccess, "'", "''") & "', " & _
    '        "Active = 1, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "',    " & _
    '        "TechnicalDescription = '" & Replace(TechDesc, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and tablename = '" & Replace(lblTableSelected.Text, "'", "''") & "' " & _
    '        "and ColumnName = '" & Replace(ColumnName, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
    '        "  Begin  " & _
    '        "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  END " & _
    '        "  " & _
    '        "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
    '        "  Begin " & _
    '        "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  End " & _
    '        "If (select count(*) from DWH.Doc.FDTables where dbName = '" & lblDatabaseSelected.Text & "' and Schemaname = '" & lblSchemaSelected.Text & "' and TableName = '" & lblTableSelected.Text & "') = 0  " & _
    '        "  Begin " & _
    '        "    Insert into DWH.Doc.FDTables values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '" & lblTableSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        " End " & _
    '        "INSERT INTO DWH.Doc.FDColumnData  " & _
    '        "([dBName], [SchemaName], [TableName], [ColumnName], [ColumnDesc],[ColumnCaveat],[Active], [ColumnOwner], [ColumnAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
    '        "'" & Replace(lblTableSelected.Text, "'", "''") & "', '" & Replace(ColumnName, "'", "''") & "', " & _
    '        "'" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
    '        "'" & Replace(ColumnOwner, "'", "''") & "', '" & Replace(ColumnAccess, "'", "''") & "', " & _
    '        "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechDesc, "'", "''") & "' ) " & _
    '        "END "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDColumnsMPA.EditIndex = -1
    '        LoadColumnsGV()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsMPA_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDColumnsMPA.PageIndexChanging
    '    Try
    '        gvFDColumnsMPA.PageIndex = e.NewPageIndex
    '        If ColumnSort.Text <> "" And ColumnDir.Text <> "" Then
    '            LoadColumnsGV(ColumnSort.Text & " " & ColumnDir.Text)
    '        Else
    '            LoadColumnsGV()
    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsMPA_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDColumnsMPA.Sorting
    '    Try
    '        Dim dataTable As DataTable = TryCast(gvFDColumnsMPA.DataSource, DataTable)
    '        Dim temp As String = e.SortDirection

    '        If gvFDColumnsMPA.PageIndex <> 0 Then
    '            gvFDColumnsMPA.PageIndex = 0
    '            LoadColumnsGV()
    '            Exit Sub
    '        End If

    '        If ColumnSort.Text = e.SortExpression Then
    '            If ColumnDir.Text = "asc" Then
    '                LoadColumnsGV(e.SortExpression & " desc ")
    '                ColumnDir.Text = "desc"
    '            Else
    '                LoadColumnsGV(e.SortExpression & " asc ")
    '                ColumnDir.Text = "asc"
    '            End If
    '        Else
    '            ColumnSort.Text = e.SortExpression

    '            If e.SortDirection = SortDirection.Ascending Then
    '                LoadColumnsGV(e.SortExpression & " asc ")
    '                ColumnDir.Text = "asc"
    '            Else
    '                LoadColumnsGV(e.SortExpression & " desc ")
    '                ColumnDir.Text = "desc"
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    ''GDDS Specific Column GridView
    'Sub LoadColumnsGVNS1(Optional sortDirection As String = "Column_Name asc")
    '    Try
    '        Dim tbl As System.Data.DataTable = New DataTable()
    '        Dim drSQL As SqlDataReader
    '        Dim drSQLNS1 As SqlDataReader
    '        Dim dr As DataRow
    '        Dim keys(0) As DataColumn

    '        If pnlColumns.Visible = True Then
    '            tbl = New DataTable()
    '            Dim col As New DataColumn("COLUMN_NAME")
    '            keys(0) = col
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("DATA_TYPE")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("Size")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("COLUMN_DEFAULT")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("IS_NULLABLE")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("isKey")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("value")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("ColumnDesc")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("ColumnOwner")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("ColumnAccess")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("columnCaveat")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("ORDINAL_POSITION")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("LastUpdated")
    '            tbl.Columns.Add(col)
    '            col = New DataColumn("TechnicalDescription")
    '            tbl.Columns.Add(col)

    '            tbl.PrimaryKey = keys

    '            SQL = "select " & _
    '            "a.COLUMN_NAME, a.DATA_TYPE,  " & _
    '            "a.Size, " & _
    '            "a.COLUMN_DEFAULT, a.IS_NULLABLE, " & _
    '            "case " & _
    '            "when b.TABLE_NAME is not null then 'True' " & _
    '            "else Null " & _
    '            "end isKey , c.value,  " & _
    '            "a.ORDINAL_POSITION  " & _
    '            "from " & _
    '            "(select " & _
    '            "COLUMN_NAME, DATA_TYPE,  " & _
    '            "case  " & _
    '            "when CHARACTER_MAXIMUM_LENGTH = -1 then 'max'  " & _
    '            "when DATA_TYPE in ('binary', 'char', 'datetime2', 'datetimeoffset', 'nchar', 'nvarchar', 'time', 'varbinary', 'varchar')  " & _
    '            "then CAST(CHARACTER_MAXIMUM_LENGTH as varchar)  " & _
    '            "when DATA_TYPE in ('decimarl','numeric') then  cast(NUMERIC_PRECISION as varchar) +','+ cast(NUMERIC_SCALE as varchar)  " & _
    '            "end Size , " & _
    '            "CHARACTER_MAXIMUM_LENGTH , " & _
    '            "NUMERIC_PRECISION, NUMERIC_SCALE, COLUMN_DEFAULT, " & _
    '            "IS_NULLABLE, ORDINAL_POSITION   " & _
    '            "from [" & lblDatabaseSelected.Text & "].INFORMATION_SCHEMA.COLUMNS " & _
    '            "where TABLE_CATALOG = '" & lblDatabaseSelected.Text & "'  and TABLE_SCHEMA = '" & lblSchemaSelected.Text & "' " & _
    '            "and TABLE_NAME='" & lblTableSelected.Text & "') a " & _
    '            "left outer join (   " & _
    '            "SELECT distinct " & _
    '            "TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME       " & _
    '            "FROM [" & lblDatabaseSelected.Text & "].information_schema.key_column_usage " & _
    '            "WHERE TABLE_NAME = '" & lblTableSelected.Text & "'  " & _
    '            "and TABLE_SCHEMA = '" & lblSchemaSelected.Text & "'  " & _
    '            "and TABLE_CATALOG = '" & lblDatabaseSelected.Text & "' ) b on a.COLUMN_NAME = b.COLUMN_NAME  " & _
    '            "outer apply (  " & _
    '            "SELECT objtype, objname, name, value  " & _
    '            "FROM fn_listextendedproperty (NULL, 'schema',  " & _
    '            "'" & lblSchemaSelected.Text & "', 'table', '" & lblTableSelected.Text & "', 'column', a.COLUMN_NAME)) c  " & _
    '               " order by " & sortDirection


    '            Using connNS1 As New SqlConnection(ConfigurationManager.ConnectionStrings("NS1conn").ConnectionString)
    '                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                    cmdNS1 = New SqlCommand(SQL, connNS1)
    '                    cmdNS1.CommandTimeout = 86400
    '                    If connNS1.State = ConnectionState.Closed Then
    '                        connNS1.Open()
    '                    End If
    '                    drSQLNS1 = cmdNS1.ExecuteReader

    '                    While drSQLNS1.Read
    '                        dr = tbl.NewRow()
    '                        If IsDBNull(drSQLNS1.Item("COLUMN_NAME")) Then
    '                            dr("COLUMN_NAME") = ""
    '                        Else
    '                            dr("COLUMN_NAME") = drSQLNS1.Item("COLUMN_NAME")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("DATA_TYPE")) Then
    '                            dr("DATA_TYPE") = ""
    '                        Else
    '                            dr("DATA_TYPE") = drSQLNS1.Item("DATA_TYPE")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("Size")) Then
    '                            dr("Size") = ""
    '                        Else
    '                            dr("Size") = drSQLNS1.Item("Size")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("COLUMN_DEFAULT")) Then
    '                            dr("COLUMN_DEFAULT") = ""
    '                        Else
    '                            dr("COLUMN_DEFAULT") = Replace(Replace(drSQLNS1.Item("COLUMN_DEFAULT"), "('", ""), "')", "")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("IS_NULLABLE")) Then
    '                            dr("IS_NULLABLE") = ""
    '                        Else
    '                            dr("IS_NULLABLE") = drSQLNS1.Item("IS_NULLABLE")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("isKey")) Then
    '                            dr("isKey") = ""
    '                        Else
    '                            dr("isKey") = drSQLNS1.Item("isKey")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("value")) Then
    '                            dr("value") = ""
    '                        Else
    '                            dr("value") = drSQLNS1.Item("value")
    '                        End If
    '                        If IsDBNull(drSQLNS1.Item("ORDINAL_POSITION")) Then
    '                            dr("ORDINAL_POSITION") = ""
    '                        Else
    '                            dr("ORDINAL_POSITION") = drSQLNS1.Item("ORDINAL_POSITION")
    '                        End If

    '                        SQL = "select columnName, ColumnDesc, columnOwner, ColumnAccess, " & _
    '                        "columnCaveat, LastUpdated, TechnicalDescription " & _
    '                        "from DWH.Doc.FDColumnData " & _
    '                        "where TableName ='" & lblTableSelected.Text & "' " & _
    '                        "and SchemaName = '" & lblSchemaSelected.Text & "' " & _
    '                        "and dBName = '" & lblDatabaseSelected.Text & "'  "


    '                        cmd = New SqlCommand(SQL, conn)
    '                        cmd.CommandTimeout = 86400
    '                        If conn.State = ConnectionState.Closed Then
    '                            conn.Open()
    '                        End If
    '                        drSQL = cmd.ExecuteReader
    '                        While drSQL.Read
    '                            If IsDBNull(drSQL.Item("ColumnDesc")) Then
    '                                dr("ColumnDesc") = ""
    '                            Else
    '                                dr("ColumnDesc") = drSQL.Item("ColumnDesc")
    '                            End If

    '                            If IsDBNull(drSQL.Item("TechnicalDescription")) Then
    '                                dr("TechnicalDescription") = ""
    '                            Else
    '                                dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
    '                            End If
    '                            If IsDBNull(drSQL.Item("ColumnOwner")) Then
    '                                dr("ColumnOwner") = ""
    '                            Else
    '                                dr("ColumnOwner") = drSQL.Item("ColumnOwner")
    '                            End If
    '                            If IsDBNull(drSQL.Item("ColumnAccess")) Then
    '                                dr("ColumnAccess") = ""
    '                            Else
    '                                dr("ColumnAccess") = drSQL.Item("ColumnAccess")
    '                            End If

    '                            If IsDBNull(drSQL.Item("columnCaveat")) Then
    '                                dr("columnCaveat") = ""
    '                            Else
    '                                dr("columnCaveat") = drSQL.Item("columnCaveat")
    '                            End If

    '                            If IsDBNull(drSQL.Item("LastUpdated")) Then
    '                                dr("LastUpdated") = ""
    '                            Else
    '                                dr("LastUpdated") = drSQL.Item("LastUpdated")
    '                            End If
    '                        End While
    '                        drSQL.Close()
    '                        tbl.Rows.Add(dr)

    '                    End While
    '                    drSQLNS1.Close()
    '                End Using
    '            End Using

    '            gvFDColumnsNS1.DataSource = tbl
    '            gvFDColumnsNS1.DataBind()

    '            ViewState.Add("NS1TBL", tbl)

    '            If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

    '            Else
    '                gvFDColumnsNS1.Columns.Item(0).Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFDColumnsNS1.RowCancelingEdit
    '    Try
    '        gvFDColumnsNS1.EditIndex = -1
    '        gvFDColumnsNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '        gvFDColumnsNS1.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFDColumnsNS1.RowCommand
    '    Try


    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFDColumnsNS1.RowDeleting
    '    Try
    '        Dim ColumnName As String = ""

    '        ColumnName = gvFDColumnsNS1.DataKeys(e.RowIndex).Value.ToString

    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.FDColumnData SET  " & _
    '         "Active = 0, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "'  " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and TableName = '" & Replace(lblTableSelected.Text, "'", "''") & "' " & _
    '        "and ColumnName = '" & Replace(ColumnName, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "INSERT INTO DWH.Doc.FDColumnData  " & _
    '        "([dBName], [SchemaName], [TableName], [ColumnName], [ColumnDesc],[ColumnCaveat], [Active], [LastUpdated], [LastUpdatedPerson])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
    '        "'" & Replace(lblTableSelected.Text, "'", "''") & "', '" & Replace(ColumnName, "'", "''") & "', '', '', 0, " & _
    '        "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "' ) " & _
    '        "END "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDColumnsNS1.EditIndex = -1
    '        LoadColumnsGVNS1()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFDColumnsNS1.RowEditing
    '    Try
    '        gvFDColumnsNS1.EditIndex = e.NewEditIndex
    '        gvFDColumnsNS1.DataSource = DirectCast(ViewState("NS1TBL"), DataTable)
    '        gvFDColumnsNS1.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFDColumnsNS1.RowUpdating
    '    Try
    '        Dim ColumnName As String = ""
    '        Dim Desc As Object
    '        Dim Caveat As String = ""
    '        Dim TechDesc As String = ""
    '        Dim ColumnAccess As String = ""
    '        Dim ColumnOwner As String = ""

    '        ColumnName = gvFDColumnsNS1.DataKeys(e.RowIndex).Value.ToString
    '        Desc = e.NewValues.Item("ColumnDesc")
    '        Caveat = e.NewValues.Item("ColumnCaveat")
    '        TechDesc = e.NewValues.Item("TechnicalDescription")
    '        ColumnOwner = e.NewValues.Item("ColumnOwner")
    '        ColumnAccess = e.NewValues.Item("ColumnAccess")


    '        SQL = "DECLARE @rowcount int " & _
    '        "UPDATE DWH.Doc.FDColumnData SET  " & _
    '        "Columndesc = '" & Replace(Desc, "'", "''") & "', " & _
    '        "ColumnCaveat = '" & Replace(Caveat, "'", "''") & "', " & _
    '        "ColumnOwner = '" & Replace(ColumnOwner, "'", "''") & "', " & _
    '        "ColumnAccess = '" & Replace(ColumnAccess, "'", "''") & "', " & _
    '        "Active = 1, LastUpdated = sysdatetime(), LastUpdatedPerson = '" & Replace(lblUser.Text, "'", "''") & "',    " & _
    '        "TechnicalDescription = '" & Replace(TechDesc, "'", "''") & "' " & _
    '        "WHERE SchemaName = '" & Replace(lblSchemaSelected.Text, "'", "''") & "' " & _
    '        "and dBName = '" & Replace(lblDatabaseSelected.Text, "'", "''") & "' " & _
    '        "and tablename = '" & Replace(lblTableSelected.Text, "'", "''") & "' " & _
    '        "and ColumnName = '" & Replace(ColumnName, "'", "''") & "' " & _
    '        "set @rowcount = @@ROWCOUNT  " & _
    '        "IF @rowcount = 0 " & _
    '        "BEGIN " & _
    '        "If (select count(*) from DWH.Doc.FDDatabase where dBName='" & Replace(lblDatabaseSelected.Text, "'", "''") & "') = 0  " & _
    '        "  Begin  " & _
    '        "    Insert into DWH.Doc.FDDatabase values ('" & lblDatabaseSelected.Text & "', '', '', '1', '', '', Sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  END " & _
    '        "  " & _
    '        "If (select count(*) from DWH.Doc.FDSchema where dbName = '" & lblDatabaseSelected.Text & "' and SchemaName = '" & lblSchemaSelected.Text & "') = 0 " & _
    '        "  Begin " & _
    '        "    Insert into DWH.Doc.FDSchema values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        "  End " & _
    '        "If (select count(*) from DWH.Doc.FDTables where dbName = '" & lblDatabaseSelected.Text & "' and Schemaname = '" & lblSchemaSelected.Text & "' and TableName = '" & lblTableSelected.Text & "') = 0  " & _
    '        "  Begin " & _
    '        "    Insert into DWH.Doc.FDTables values ('" & lblDatabaseSelected.Text & "', '" & lblSchemaSelected.Text & "', '" & lblTableSelected.Text & "', '', '', '1', '', '', sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '') " & _
    '        " End " & _
    '        "INSERT INTO DWH.Doc.FDColumnData  " & _
    '        "([dBName], [SchemaName], [TableName], [ColumnName], [ColumnDesc],[ColumnCaveat],[Active], [ColumnOwner], [ColumnAccess], [LastUpdated], [LastUpdatedPerson], [TechnicalDescription])  " & _
    '        "   VALUES  " & _
    '        "('" & Replace(lblDatabaseSelected.Text, "'", "''") & "', '" & Replace(lblSchemaSelected.Text, "'", "''") & "', " & _
    '        "'" & Replace(lblTableSelected.Text, "'", "''") & "', '" & Replace(ColumnName, "'", "''") & "', " & _
    '        "'" & Replace(Desc, "'", "''") & "', '" & Replace(Caveat, "'", "''") & "', 1, " & _
    '        "'" & Replace(ColumnOwner, "'", "''") & "', '" & Replace(ColumnAccess, "'", "''") & "', " & _
    '        "sysdatetime(), '" & Replace(lblUser.Text, "'", "''") & "', '" & Replace(TechDesc, "'", "''") & "' ) " & _
    '        "END "

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            cmd = New SqlCommand(SQL, conn)
    '            cmd.CommandTimeout = 86400
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        gvFDColumnsNS1.EditIndex = -1
    '        LoadColumnsGVNS1()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFDColumnsNS1.PageIndexChanging
    '    Try
    '        gvFDColumnsNS1.PageIndex = e.NewPageIndex
    '        If ColumnSort.Text <> "" And ColumnDir.Text <> "" Then
    '            LoadColumnsGVNS1(ColumnSort.Text & " " & ColumnDir.Text)
    '        Else
    '            LoadColumnsGVNS1()
    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Protected Sub gvFDColumnsNS1_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFDColumnsNS1.Sorting
    '    Try
    '        Dim dataTable As DataTable = TryCast(gvFDColumnsNS1.DataSource, DataTable)
    '        Dim temp As String = e.SortDirection

    '        If gvFDColumnsNS1.PageIndex <> 0 Then
    '            gvFDColumnsNS1.PageIndex = 0
    '            LoadColumnsGVNS1()
    '            Exit Sub
    '        End If

    '        If e.SortExpression = "columnName" Or e.SortExpression = "ColumnDesc" Or e.SortExpression = "columnOwner" Or e.SortExpression = "ColumnAccess" _
    '            Or e.SortExpression = "columnCaveat" Or e.SortExpression = "LastUpdated" Or e.SortExpression = "TechnicalDescription" Then
    '            Exit Sub
    '        End If
    '        If ColumnSort.Text = e.SortExpression Then
    '            If ColumnDir.Text = "asc" Then
    '                LoadColumnsGVNS1(e.SortExpression & " desc ")
    '                ColumnDir.Text = "desc"
    '            Else
    '                LoadColumnsGVNS1(e.SortExpression & " asc ")
    '                ColumnDir.Text = "asc"
    '            End If
    '        Else
    '            ColumnSort.Text = e.SortExpression

    '            If e.SortDirection = SortDirection.Ascending Then
    '                LoadColumnsGVNS1(e.SortExpression & " asc ")
    '                ColumnDir.Text = "asc"
    '            Else
    '                LoadColumnsGVNS1(e.SortExpression & " desc ")
    '                ColumnDir.Text = "desc"
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
#End Region
    'Misc Subs
   
End Class