Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO

Imports FinanceWeb.WebFinGlobal

Public Class DataDictionary2
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd As SqlCommand
    Dim drSQL As SqlDataReader
    Dim tbl As System.Data.DataTable '= New DataTable()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then

        'Else

        '    LoadDataDictionary()
        'End If

    End Sub
    Protected Sub btnBasicSearch_Click(sender As Object, e As EventArgs) Handles btnBasicSearch.Click
        Try

            LoadDataDictionary()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadDataDictionary(Optional SortDirection As String = "")
        Try
            Dim WhereClause As String = " where DataName is not null "
            Dim SQLWhere As String = ""

            ''Removed 10/6/2014, CRW
            'Dim temp As String = ""

            'If SortDirection = "" Then
            '    SortDirection = "order by Case a.DataType " & _
            '                    "when 'Column' then 1  " & _
            '                    "when 'Table' then 2  " & _
            '                    "when 'Schema' then 3  " & _
            '                    "when 'Database' then 4  " & _
            '                    "End , DataName  "
            'Else
            '    SortDirection = "order by " & SortDirection
            'End If

            If pnlAdvancedOptions.Visible = True Then
                If rdlDataTypes.Items.Item(0).Selected = True Then
                    WhereClause = WhereClause & " and  DataType = 'Column' "
                End If
                If rdlDataTypes.Items.Item(1).Selected = True Then
                    WhereClause = WhereClause & " and  DataType = 'Table' "
                End If
                If rdlDataTypes.Items.Item(2).Selected = True Then
                    WhereClause = WhereClause & " and  DataType = 'Schema' "
                End If
                If rdlDataTypes.Items.Item(3).Selected = True Then
                    WhereClause = WhereClause & " and  DataType = 'Database' "
                End If
            End If

            If rdlDataSources.Items.Item(0).Selected = True Then
                WhereClause = WhereClause & " and DataPath like '%' "
            End If
            If rdlDataSources.Items.Item(1).Selected = True Then
                WhereClause = WhereClause & " and DataPath like 'DWH%'  "
            End If
            If rdlDataSources.Items.Item(2).Selected = True Then
                WhereClause = WhereClause & " and DataPath like 'MPA%'  "
            End If
            If rdlDataSources.Items.Item(3).Selected = True Then
                WhereClause = WhereClause & " and DataPath like 'GDDS%'  "
            End If

            Dim tbl As System.Data.DataTable = New DataTable()
            Dim col As New DataColumn("DataName")
            tbl.Columns.Add(col)
            col = New DataColumn("DataType")
            tbl.Columns.Add(col)
            col = New DataColumn("DataDesc")
            tbl.Columns.Add(col)
            col = New DataColumn("TechnicalDescription")
            tbl.Columns.Add(col)
            col = New DataColumn("DataCaveat")
            tbl.Columns.Add(col)
            col = New DataColumn("DataPath")
            tbl.Columns.Add(col)


            '' Removed 10/6/2014, CRW 
            'If txtBasicSearch.Text.Contains(",") Then
            '    temp = txtBasicSearch.Text

            '    Do While temp <> ""
            '        temp = Trim(temp)
            '        If temp.Contains(",") Then
            '            SQLWhere = Mid(temp, 1, (InStr(temp, ",") - 1))
            '            temp = Replace(temp, (SQLWhere & ","), "")
            '        Else
            '            SQLWhere = temp
            '            temp = ""
            '        End If
            '        SQL = SQL & "select " & _
            '          "ColumnName as DataName, 'Column' as DataType, " & _
            '          "ColumnDesc as DataDesc, TechnicalDescription, columnCaveat as DataCaveat, " & _
            '          "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName + ' --> ' + ColumnName ) as DataPath " & _
            '          "from DWH.DOC.FDColumnData " & _
            '          "where upper(ColumnName) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(ColumnDesc) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(columnCaveat ) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "union  " & _
            '          "select  " & _
            '          "tableName as DataName, 'Table' as DataType,  " & _
            '          "tableDesc as DataDesc, TechnicalDescription, tableCaveat as DataCaveat,  " & _
            '          "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName ) as DataPath  " & _
            '          "from DWH.DOC.FDTables  " & _
            '          "where upper(tableName) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(TableDesc) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(TableCaveat  ) like '%@" & SQLWhere.ToUpper & "%' " & _
            '          "union  " & _
            '          "select  " & _
            '          "SchemaName as DataName, 'Schema' as DataType,  " & _
            '          "SchemaDesc as DataDesc, TechnicalDescription, SchemaCaveat as DataCaveat,  " & _
            '          "(dBName + ' --> ' + SCHEMANAME ) as DataPath  " & _
            '          "from DWH.DOC.FDSchema " & _
            '          "where upper(SchemaName) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(SchemaDesc) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(SchemaCaveat ) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "union  " & _
            '          "select  " & _
            '          "DbName as DataName, 'Database' as DataType,  " & _
            '          "dbDesc as DataDesc, TechnicalDescription, dBCaveat as DataCaveat,  " & _
            '          "(dBName ) as DataPath  " & _
            '          "from DWH.DOC.FDDatabase  " & _
            '          "where upper(DbName) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(dbDesc) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
            '          "or  upper(dbCaveat ) like '%" & SQLWhere.ToUpper & "%'" & _
            '          " union "
            '    Loop
            '    SQL = Mid(SQL, 1, SQL.Length - 7)
            'Else
            '    SQL = "select " & _
            '    "ColumnName as DataName, 'Column' as DataType, " & _
            '    "ColumnDesc as DataDesc, TechnicalDescription, columnCaveat as DataCaveat, " & _
            '    "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName + ' --> ' + ColumnName ) as DataPath " & _
            '    "from DWH.DOC.FDColumnData " & _
            '    "where upper(ColumnName) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(ColumnDesc) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(TechnicalDescription ) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(columnCaveat ) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "union  " & _
            '    "select  " & _
            '    "tableName as DataName, 'Table' as DataType,  " & _
            '    "tableDesc as DataDesc, TechnicalDescription, tableCaveat as DataCaveat,  " & _
            '    "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName ) as DataPath  " & _
            '    "from DWH.DOC.FDTables  " & _
            '    "where upper(tableName) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(TableDesc) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(TechnicalDescription ) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(TableCaveat  ) like '%@" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "union  " & _
            '    "select  " & _
            '    "SchemaName as DataName, 'Schema' as DataType,  " & _
            '    "SchemaDesc as DataDesc, TechnicalDescription, SchemaCaveat as DataCaveat,  " & _
            '    "(dBName + ' --> ' + SCHEMANAME ) as DataPath  " & _
            '    "from DWH.DOC.FDSchema " & _
            '    "where upper(SchemaName) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(SchemaDesc) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(TechnicalDescription ) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(SchemaCaveat ) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "union  " & _
            '    "select  " & _
            '    "DbName as DataName, 'Database' as DataType,  " & _
            '    "dbDesc as DataDesc, TechnicalDescription, dBCaveat as DataCaveat,  " & _
            '    "(dBName ) as DataPath  " & _
            '    "from DWH.DOC.FDDatabase  " & _
            '    "where upper(DbName) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(dbDesc) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(TechnicalDescription ) like '%" & txtBasicSearch.Text.ToUpper & "%' " & _
            '    "or  upper(dbCaveat ) like '%" & txtBasicSearch.Text.ToUpper & "%'"
            'End If

            'SQL = "Select * from " & _
            '"( " & SQL & " ) a  " & _
            'WhereClause & _
            'SortDirection


            Dim tempsql As String = ""
            Dim tempsql2 As String = ""
            Dim sqlfull As String = ""
            Dim temp2 As String
            Dim temp3 As String
            Dim columns As String = ""
            Dim tables As String = ""
            Dim schemas As String = ""
            Dim databases As String = ""
            Dim spacecounter As Integer = 0
            Dim tempfull As String = ""


            temp2 = txtBasicSearch.Text

            Do While temp2 <> ""
                temp2 = Trim(temp2)
                spacecounter = 1
                columns = ""
                tables = ""
                schemas = ""
                databases = ""

                If temp2.Contains(",") Then
                    SQLWhere = Mid(temp2, 1, (InStr(temp2, ",") - 1))
                    temp2 = Replace(temp2, (SQLWhere & ","), "")
                Else
                    SQLWhere = temp2
                    temp2 = ""
                End If

                tempfull = SQLWhere

                If SQLWhere.Contains(" ") Then
                    temp3 = SQLWhere

                    Do While temp3 <> ""
                        If temp3.Contains(" ") Then
                            SQLWhere = Mid(temp3, 1, (InStr(temp3, " ") - 1))
                            temp3 = Trim(Replace(temp3, (SQLWhere & " "), ""))
                        Else
                            SQLWhere = temp3
                            temp3 = ""
                        End If

                        columns = columns & " case when upper(ColumnName) like '%" & SQLWhere & "%' then 1 when upper(ColumnDesc) like '%" & SQLWhere & "%' then 1 " & _
                            "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(columnCaveat) like '%" & SQLWhere & "%' then 1 " & _
                            " when upper(ColumnName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(ColumnDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(columnCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            " else 0 end + "
                        tables = tables & " case when upper(tableName) like '%" & SQLWhere & "%' then 1 when upper(TableDesc) like '%" & SQLWhere & "%' then 1 " & _
                            "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(TableCaveat) like '%" & SQLWhere & "%' then 1 " & _
                            " when upper(tableName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(TableDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(TableCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            " else 0 end + "
                        schemas = schemas & " case when upper(SchemaName) like '%" & SQLWhere & "%' then 1 when upper(SchemaDesc) like '%" & SQLWhere & "%' then 1 " & _
                            "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(SchemaCaveat) like '%" & SQLWhere & "%' then 1 " & _
                            "when upper(SchemaName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(SchemaDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(SchemaCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            " else 0 end + "
                        databases = databases & " case when upper(DbName) like '%" & SQLWhere & "%' then 1 when upper(dbDesc) like '%" & SQLWhere & "%' then 1 " & _
                            "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(dbCaveat) like '%" & SQLWhere & "%' then 1 " & _
                            "when upper(DbName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(dbDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(dbCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                            " else 0 end + "

                        spacecounter = spacecounter + 1
                    Loop
                Else
                    columns = columns & " case when upper(ColumnName) like '%" & SQLWhere & "%' then 1 when upper(ColumnDesc) like '%" & SQLWhere & "%' then 1 " & _
                         "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(columnCaveat) like '%" & SQLWhere & "%' then 1 " & _
                         "when upper(ColumnName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(ColumnDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                         "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(columnCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                         " else 0 end + "
                    tables = tables & " case when upper(tableName) like '%" & SQLWhere & "%' then 1 when upper(TableDesc) like '%" & SQLWhere & "%' then 1 " & _
                        "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(TableCaveat) like '%" & SQLWhere & "%' then 1 " & _
                        "when upper(tableName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(TableDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                        "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(TableCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                        " else 0 end + "
                    schemas = schemas & " case when upper(SchemaName) like '%" & SQLWhere & "%' then 1 when upper(SchemaDesc) like '%" & SQLWhere & "%' then 1 " & _
                        "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(SchemaCaveat) like '%" & SQLWhere & "%' then 1 " & _
                        "when upper(SchemaName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(SchemaDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                        "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(SchemaCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                        " else 0 end + "
                    databases = databases & " case when upper(DbName) like '%" & SQLWhere & "%' then 1 when upper(dbDesc) like '%" & SQLWhere & "%' then 1 " & _
                        "when upper(TechnicalDescription) like '%" & SQLWhere & "%' then 1 when upper(dbCaveat) like '%" & SQLWhere & "%' then 1 " & _
                        "when upper(DbName) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(dbDesc) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                        "when upper(TechnicalDescription) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 when upper(dbCaveat) like '%" & If(Len(SQLWhere) < 3, SQLWhere, Left(SQLWhere, 3)) & "%' then .6 " & _
                        " else 0 end + "


                    spacecounter = spacecounter + 1
                End If

                columns = " select ColumnName as DataName, 'Column' as DataType, ColumnDesc as DataDesc, TechnicalDescription, columnCaveat as DataCaveat, " & _
                "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName + ' --> ' + ColumnName ) as DataPath ," & Left(columns, Len(columns) - 2) & _
                " as [wordcount] from DWH.DOC.FDColumnData "
                tables = "   select  tableName as DataName, 'Table' as DataType,  tableDesc as DataDesc, TechnicalDescription, tableCaveat as DataCaveat,  " & _
                     "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName ) as DataPath," & Left(tables, Len(tables) - 2) & _
                     " as [wordcount] from DWH.DOC.FDTables "
                schemas = "select  SchemaName as DataName, 'Schema' as DataType,  SchemaDesc as DataDesc, TechnicalDescription, SchemaCaveat as DataCaveat,  " & _
                    "(dBName + ' --> ' + SCHEMANAME ) as DataPath," & Left(schemas, Len(schemas) - 2) & " as [wordcount] from DWH.DOC.FDSchema "
                databases = "    select  DbName as DataName, 'Database' as DataType,  dbDesc as DataDesc, TechnicalDescription, dBCaveat as DataCaveat,  " & _
                    " (dBName ) as DataPath, " & Left(databases, Len(databases) - 2) & " as [wordcount] from DWH.DOC.FDDatabase "

                SQLWhere = tempfull

                tempsql2 = tempsql2 & "select *, " & spacecounter.ToString & " as wordcount from ( Select " & _
                      "ColumnName as DataName, 'Column' as DataType, " & _
                      "ColumnDesc as DataDesc, TechnicalDescription, columnCaveat as DataCaveat, " & _
                      "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName + ' --> ' + ColumnName ) as DataPath " & _
                      "from DWH.DOC.FDColumnData " & _
                      "where upper(ColumnName) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(ColumnDesc) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(columnCaveat ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "union  " & _
                      "select  " & _
                      "tableName as DataName, 'Table' as DataType,  " & _
                      "tableDesc as DataDesc, TechnicalDescription, tableCaveat as DataCaveat,  " & _
                      "(dBName + ' --> ' + SCHEMANAME + ' --> ' + TableName ) as DataPath  " & _
                      "from DWH.DOC.FDTables  " & _
                      "where upper(tableName) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(TableDesc) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(TableCaveat  ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "union  " & _
                      "select  " & _
                      "SchemaName as DataName, 'Schema' as DataType,  " & _
                      "SchemaDesc as DataDesc, TechnicalDescription, SchemaCaveat as DataCaveat,  " & _
                      "(dBName + ' --> ' + SCHEMANAME ) as DataPath  " & _
                      "from DWH.DOC.FDSchema " & _
                      "where upper(SchemaName) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(SchemaDesc) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(SchemaCaveat ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "union  " & _
                      "select  " & _
                      "DbName as DataName, 'Database' as DataType,  " & _
                      "dbDesc as DataDesc, TechnicalDescription, dBCaveat as DataCaveat,  " & _
                      "(dBName ) as DataPath  " & _
                      "from DWH.DOC.FDDatabase  " & _
                      "where upper(DbName) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(dbDesc) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(TechnicalDescription ) like '%" & SQLWhere.ToUpper & "%' " & _
                      "or  upper(dbCaveat ) like '%" & SQLWhere.ToUpper & "%' ) x" & _
                      " union  "

                tempsql = tempsql & "select * from (" & columns & " union " & tables & " union " & schemas & _
                      " union " & databases & ") y where DataName is not null and DataPath like '%' " & _
                      "union  "

            Loop
            tempsql = Mid(tempsql, 1, tempsql.Length - 7)
            tempsql2 = Mid(tempsql2, 1, tempsql2.Length - 7)



            tempsql = "select DataName, DataType, DataDesc, TechnicalDescription, DataCaveat, DataPath, sum(wordcount) as wordcount from (" & _
                tempsql & ") q group by DataName, DataType, DataDesc, TechnicalDescription, DataCaveat, DataPath "

            tempsql2 = "select DataName, DataType, DataDesc, TechnicalDescription, DataCaveat, DataPath, sum(wordcount) as wordcount from (" & _
                tempsql2 & ") q group by DataName, DataType, DataDesc, TechnicalDescription, DataCaveat, DataPath "

            sqlfull = "select DataName, DataType, DataDesc, TechnicalDescription, DataCaveat, DataPath from (" & _
                tempsql & " union " & tempsql2 & ") overall " & WhereClause & " group by DataName, DataType, DataDesc, TechnicalDescription, DataCaveat, DataPath " & _
                    "having max(wordcount) > 1 " & _
                "order by " & If(SortDirection = "", "", SortDirection & ", ") & " sum(wordcount) desc, Case DataType when 'Column' then 1  when 'Table' then 2  when 'Schema' then 3  when 'Database' then 4  End , DataName "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                cmd = New SqlCommand(sqlfull, conn)
                cmd.CommandTimeout = 86400

                Dim drSQL As SqlDataReader
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                drSQL = cmd.ExecuteReader
                Dim dr As DataRow
                While drSQL.Read
                    dr = tbl.NewRow()
                    If IsDBNull(drSQL.Item("DataName")) Then
                        dr("DataName") = ""
                    Else
                        dr("DataName") = drSQL.Item("DataName")
                    End If

                    If IsDBNull(drSQL.Item("DataType")) Then
                        dr("DataType") = ""
                    Else
                        dr("DataType") = drSQL.Item("DataType")
                    End If
                    If IsDBNull(drSQL.Item("DataDesc")) Then
                        dr("DataDesc") = ""
                    Else
                        dr("DataDesc") = drSQL.Item("DataDesc")
                    End If
                    If IsDBNull(drSQL.Item("TechnicalDescription")) Then
                        dr("TechnicalDescription") = ""
                    Else
                        dr("TechnicalDescription") = drSQL.Item("TechnicalDescription")
                    End If
                    If IsDBNull(drSQL.Item("DataCaveat")) Then
                        dr("DataCaveat") = ""
                    Else
                        dr("DataCaveat") = drSQL.Item("DataCaveat")
                    End If
                    If IsDBNull(drSQL.Item("DataPath")) Then
                        dr("DataPath") = ""
                    Else
                        dr("DataPath") = drSQL.Item("DataPath")
                    End If
                    tbl.Rows.Add(dr)
                End While
                drSQL.Close()
            End Using

            gvDataDictionary.DataSource = tbl
            gvDataDictionary.DataBind()
            gvDataDictionary.Visible = True


            lblCount.Text = "Count: " & tbl.Rows.Count.ToString & "  rows returned."

            If tbl.Rows.Count = 0 Then





                lblRecommendations.Visible = True
            Else
                lblRecommendations.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Protected Sub gvDataDictionary_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDataDictionary.PageIndexChanging
        Try
            gvDataDictionary.PageIndex = e.NewPageIndex
            If txtField.Text <> "" And txtSortDir.Text <> "" Then
                LoadDataDictionary(txtField.Text & " " & txtSortDir.Text)
            Else
                LoadDataDictionary()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub gvDataDictionary_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvDataDictionary.Sorting
        Try
            Dim dataTable As DataTable = TryCast(gvDataDictionary.DataSource, DataTable)
            Dim temp As String = e.SortDirection

            gvDataDictionary.EditIndex = -1
            gvDataDictionary.DataSource = DirectCast(ViewState("TBL"), DataTable)
            gvDataDictionary.DataBind()

            If gvDataDictionary.PageIndex <> 0 Then
                gvDataDictionary.PageIndex = 0
                LoadDataDictionary()
                Exit Sub
            End If

            If txtField.Text = e.SortExpression Then
                If txtSortDir.Text = "asc" Then
                    LoadDataDictionary(e.SortExpression & " desc ")
                    txtSortDir.Text = "desc"
                Else
                    LoadDataDictionary(e.SortExpression & " asc ")
                    txtSortDir.Text = "asc"
                End If
            Else
                txtField.Text = e.SortExpression

                If e.SortDirection = SortDirection.Ascending Then
                    LoadDataDictionary(e.SortExpression & " asc ")
                    txtSortDir.Text = "asc"
                Else
                    LoadDataDictionary(e.SortExpression & " desc ")
                    txtSortDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Protected Sub btnAdvancedOptions_Click(sender As Object, e As EventArgs) Handles btnAdvancedOptions.Click
        Try
            If pnlAdvancedOptions.Visible = False Then
                pnlAdvancedOptions.Visible = True
                btnAdvancedOptions.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnHideAdvanced_Click(sender As Object, e As EventArgs) Handles btnHideAdvanced.Click
        Try
            If pnlAdvancedOptions.Visible = True Then
                pnlAdvancedOptions.Visible = False
                btnAdvancedOptions.Visible = True
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnResetAdvanced_Click(sender As Object, e As EventArgs) Handles btnResetAdvanced.Click
        Try
            rdlDataSources.Items.Item(0).Selected = True
            rdlDataSources.Items.Item(1).Selected = False
            rdlDataSources.Items.Item(2).Selected = False
            rdlDataSources.Items.Item(3).Selected = False

            rdlDataTypes.Items.Item(0).Selected = False
            rdlDataTypes.Items.Item(1).Selected = False
            rdlDataTypes.Items.Item(2).Selected = False
            rdlDataTypes.Items.Item(3).Selected = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class