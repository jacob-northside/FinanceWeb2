Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO

Imports FinanceWeb.WebFinGlobal

Public Class DataDictionaryNew
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
            Dim WhereClause As String = " where ColumnName is not null and titlescore + descscore + tdscore + cavscore > 0 "
            Dim SQLWhere As String = ""

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
                WhereClause = WhereClause & " and DB like '%' "
            End If
            If rdlDataSources.Items.Item(1).Selected = True Then
                WhereClause = WhereClause & " and DB like 'DWH%'  "
            End If

            Dim tbl As System.Data.DataTable = New DataTable()
            Dim col As New DataColumn("ord")
            tbl.Columns.Add(col)
            col = New DataColumn("ColumnName")
            tbl.Columns.Add(col)
            col = New DataColumn("DataType")
            tbl.Columns.Add(col)
            col = New DataColumn("ColumnDesc")
            tbl.Columns.Add(col)
            col = New DataColumn("TechnicalDescription")
            tbl.Columns.Add(col)
            col = New DataColumn("Caveat")
            tbl.Columns.Add(col)
            col = New DataColumn("DB")
            tbl.Columns.Add(col)
            col = New DataColumn("Schema")
            tbl.Columns.Add(col)
            col = New DataColumn("Table")
            tbl.Columns.Add(col)
            col = New DataColumn("Column")
            tbl.Columns.Add(col)




            Dim temp2 As String = Replace(txtBasicSearch.Text, "'", "''")

            Dim initSQL As String = "declare @Phrase varchar(max) = '" & temp2 & "' " & _
                    "declare @Mini varchar(max) " & _
                    " " & _
                    "if OBJECT_ID('tempdb..#tempy') is not null " & _
                    "begin  " & _
                    "   drop table #tempy " & _
                    "end  " & _
                    " " & _
                    "create table #tempy ( " & _
                    "titlescore int null, " & _
                    "descscore int null, " & _
                    "tdscore int null, " & _
                    "cavscore int null, " & _
                    "ColumnName varchar(max) null, " & _
                    "DataType varchar(30) null, " & _
                    "ColumnDesc varchar(max) null, " & _
                    "TechnicalDescription varchar(max) null, " & _
                    "Caveat varchar(max) null, " & _
                    "DB varchar(max) null, " & _
                    "[Schema] varchar(max) null, " & _
                    "[Table] varchar(max) null, " & _
                    "[Column] varchar(max) null " & _
                    ") " & _
                    " " & _
                    "insert into #tempy (titlescore, descscore, tdscore, cavscore, ColumnName, DataType, ColumnDesc, TechnicalDescription, Caveat,  " & _
                    "[DB], [Schema], [Table], [Column]) " & _
                    "select 0, 0, 0, 0, " & _
                    "CD.ColumnName as DataName, 'Column' as DataType, CD.ColumnDesc as [Desc], " & _
                    "TechnicalDescription as TechDesc, columnCaveat as Caveat, " & _
                    "CD.dBName, SchemaName, TableName, ColumnName " & _
                    "from DWH.DOC.FDColumnData CD " & _
                    "join sys.sysdatabases a on (a.name COLLATE DATABASE_DEFAULT = CD.dBName COLLATE DATABASE_DEFAULT ) " & _
                    "WHERE(HAS_DBACCESS(name) = 1) " & _
                    "and (Active = 1 or Active is null )  " & _
                    " " & _
                    "insert into #tempy (titlescore, descscore, tdscore, cavscore, ColumnName, DataType, ColumnDesc, TechnicalDescription, Caveat,  " & _
                    "[DB]) " & _
                    "Select 0, 0, 0, 0, FD.dBName, 'DataBase', dBDesc, TechnicalDescription, dBCaveat, dBName " & _
                    "from DWH.DOC.FDDatabase FD " & _
                    "join sys.sysdatabases a on (a.name COLLATE DATABASE_DEFAULT = FD.dBName COLLATE DATABASE_DEFAULT ) " & _
                    "WHERE(HAS_DBACCESS(name) = 1) " & _
                    "and (Active = 1 or Active is null )  " & _
                    " " & _
                    "insert into #tempy (titlescore, descscore, tdscore, cavscore, ColumnName, DataType, ColumnDesc, TechnicalDescription, Caveat,  " & _
                    "[DB], [Schema]) " & _
                    "Select 0, 0, 0, 0, FS.SchemaName, 'Schema', SchemaDesc, TechnicalDescription, SchemaCaveat, dBName, SchemaName " & _
                    "from DWH.DOC.FDSchema FS " & _
                    "join sys.sysdatabases a on (a.name COLLATE DATABASE_DEFAULT = FS.dBName COLLATE DATABASE_DEFAULT ) " & _
                    "WHERE(HAS_DBACCESS(name) = 1) " & _
                    "and (Active = 1 or Active is null )  " & _
                    " " & _
                    "insert into #tempy (titlescore, descscore, tdscore, cavscore, ColumnName, DataType, ColumnDesc, TechnicalDescription, Caveat,  " & _
                    "[DB], [Schema], [Table]) " & _
                    "Select 0, 0, 0, 0, FT.TableName, 'Table', TableDesc, TechnicalDescription, TableCaveat, dBName, SchemaName, TableName " & _
                    "from DWH.DOC.FDTables FT " & _
                    "join sys.sysdatabases a on (a.name COLLATE DATABASE_DEFAULT = FT.dBName COLLATE DATABASE_DEFAULT ) " & _
                    "WHERE(HAS_DBACCESS(name) = 1) " & _
                    "and (Active = 1 or Active is null )  " & _
                    " " & _
                    " " & _
                    "while LEN(@Phrase) > 0 " & _
                    "begin " & _
                    " " & _
                    "if CHARINDEX(' ', @Phrase, 0) > 0 " & _
                    "begin " & _
                    "	set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0)))) " & _
                    "	set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase)))) " & _
                    "	 " & _
                    "end " & _
                    "else " & _
                    "begin " & _
                    "	set @Mini = ltrim(@Phrase) " & _
                    "	set @Phrase = '' " & _
                    "end " & _
                    " " & _
                    "update #tempy " & _
                    "set titlescore = titlescore + " & _
                    " " & _
                    "case when LTRIM(rtrim(ColumnName)) = '' then 0 " & _
                    "	 when ColumnName = @Mini then 1010 " & _
                    "	 when ColumnName = REPLACE(@Mini, ' ', '') then 1005 " & _
                    "	 when ColumnName = REPLACE(@Mini, '_', '') then 1004 " & _
                    "	 when @Mini = REPLACE(ColumnName, ' ', '') then 1005 " & _
                    "	 when @Mini = REPLACE(ColumnName, '_', '') then 1004 " & _
                    "	 when Replace(REPLACE(ColumnName, '_', ''), ' ', '') = REPLACE(Replace(@Mini, '_', ''), ' ', '') then 1003 " & _
                    "	 when ColumnName like @Mini + '%' then 1003  " & _
                    "	 when ColumnName like '%' + @Mini then 1002  " & _
                    "	 when ColumnName like '%' + @Mini + '%' then 1000  " & _
                    "	 when @Mini like '%' + ColumnName + '%' then 480 " & _
                    "	 when @Mini like '%' + replace(ColumnName, '_', ' ') + '%' then 475 " & _
                    "	 else 0 + " & _
                    "	  " & _
                    "	 case  when LTRIM(rtrim(ColumnName)) = '' then 0 " & _
                    "	 when charindex(' ', @Mini, 0) > 2 and ColumnName = SUBSTRING(@Mini, 0, charindex(' ', @Mini, 0)) then 300 " & _
                    "	when charindex(' ', @Mini, 0) > 2 and ColumnName like '% ' +  SUBSTRING(@Mini, 0, charindex(' ', @Mini, 0)) + ' %' then 640  " & _
                    "	when charindex(' ', @Mini, 0) > 2 and ColumnName like  SUBSTRING(@Mini, 0, charindex(' ', @Mini, 0)) + ' %' then 640  " & _
                    "	when charindex(' ', @Mini, 0) > 2 and ColumnName like '% ' + SUBSTRING(@Mini, 0, charindex(' ', @Mini, 0)) then 640  " & _
                    "	when charindex(' ', @Mini, 0) > 2 and ColumnName like '%' +  SUBSTRING(@Mini, 0, charindex(' ', @Mini, 0)) + '%' then 540  " & _
                    "	when charindex(' ', replace(ColumnName, '_', ' ') , 0) > 2 then " & _
                    "		case when @Mini like '% ' +  SUBSTRING(replace(ColumnName, '_', ' ') , 0, charindex(' ', replace(ColumnName, '_', ' ') , 0)) + ' %' then 640  " & _
                    "			when @Mini like  SUBSTRING(replace(ColumnName, '_', ' ') , 0, charindex(' ', replace(ColumnName, '_', ' ') , 0)) + ' %' then 640  " & _
                    "			when @Mini like '% ' + SUBSTRING(replace(ColumnName, '_', ' ') , 0, charindex(' ', replace(ColumnName, '_', ' ') , 0)) then 640  " & _
                    "			when @Mini like '%' +  SUBSTRING(replace(ColumnName, '_', ' ') , 0, charindex(' ', replace(ColumnName, '_', ' ') , 0)) + '%' then 540  " & _
                    "			else 0 end " & _
                    "			 " & _
                    "	else 0 end  " & _
                    "	+ " & _
                    "case  when LTRIM(rtrim(ColumnName)) = '' then 0 " & _
                    "		 when charindex(' ', Replace(Reverse(@Mini), '_', ' ') ,0) > 2 and ColumnName = right(Replace(@Mini, '_', ' '), charindex(' ', Reverse(Replace(@Mini, '_', ' ')),0) -1) then 300 " & _
                    "		when charindex(' ', Replace(Reverse(@Mini), '_', ' ') ,0) > 2 and ColumnName like '% ' +  right(Replace(@Mini, '_', ' '), charindex(' ', Reverse(Replace(@Mini, '_', ' ')),0) -1) + ' %' then 640  " & _
                    "		when charindex(' ', Replace(Reverse(@Mini), '_', ' ') ,0) > 2 and ColumnName like  right(Replace(@Mini, '_', ' '), charindex(' ', Reverse(Replace(@Mini, '_', ' ')),0) -1) + ' %' then 640 " & _
                    "		when charindex(' ', Replace(Reverse(@Mini), '_', ' ') ,0) > 2 and ColumnName like '% ' +  right(Replace(@Mini, '_', ' '), charindex(' ', Reverse(Replace(@Mini, '_', ' ')),0) -1) then 640 " & _
                    "		when charindex(' ', Replace(Reverse(@Mini), '_', ' ') ,0) > 2 and ColumnName like '%' +  right(Replace(@Mini, '_', ' '), charindex(' ', Reverse(Replace(@Mini, '_', ' ')),0) -1) + '%' then 540 " & _
                    " " & _
                    "	else case when charindex(' ', Reverse(replace(ColumnName, '_', ' ')),0) > 2 then " & _
                    "			case when @Mini like '% ' +  right(replace(ColumnName, '_', ' '), charindex(' ', Reverse(replace(ColumnName, '_', ' ')),0) -1) + ' %' then 640  " & _
                    "			when @Mini like  right(replace(ColumnName, '_', ' '), charindex(' ', Reverse(replace(ColumnName, '_', ' ')),0) -1) + ' %' then 640 " & _
                    "			when @Mini like '% ' +  right(replace(ColumnName, '_', ' '), charindex(' ', Reverse(replace(ColumnName, '_', ' ')),0) -1) then 640 " & _
                    "			when @Mini like '%' +  right(replace(ColumnName, '_', ' '), charindex(' ', Reverse(replace(ColumnName, '_', ' ')),0) -1) + '%' then 540 " & _
                    "			else 0 end " & _
                    "		else 0 end  " & _
                    "	end  " & _
                    "	 end  " & _
                    "	  " & _
                    "	, descscore = descscore + " & _
                    " " & _
                    "case when LEN(LTRIM(ColumnDesc)) = 0 then 0 " & _
                    "else	 " & _
                    "case when ColumnDesc = @Mini then 990 " & _
                    "	 when ColumnDesc = REPLACE(@Mini, ' ', '') then 985 " & _
                    "	 when ColumnDesc = REPLACE(@Mini, '_', '') then 985 " & _
                    "	 when @Mini = REPLACE(ColumnDesc, ' ', '') then 985 " & _
                    "	 when @Mini = REPLACE(ColumnDesc, '_', '') then 985 " & _
                    "	 when Replace(REPLACE(ColumnDesc, '_', ''), ' ', '') = REPLACE(Replace(@Mini, '_', ''), ' ', '') then 985 " & _
                    "	 when ColumnDesc like @Mini + '%' then 695  " & _
                    "	 when ColumnDesc like @Mini + '%' then 698  " & _
                    "	 when ColumnDesc like '%' + @Mini + '%' then 690  " & _
                    "	 when @Mini like '%' + ColumnDesc + '%' then 590 " & _
                    "	 when @Mini like '%' + replace(ColumnDesc, '_', ' ') + '%' then 540 " & _
                    "	 else 0  " & _
                    "	 + " & _
                    "case when charindex(' ', ltrim(@Mini), 0) > 2 and ColumnDesc = SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) then 150 " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and ColumnDesc like '% ' +  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + ' %' then 250  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and ColumnDesc like  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + ' %' then 250  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and ColumnDesc like '% ' + SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) then 250  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and ColumnDesc like '%' +  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + '%' then 200  " & _
                    "	when charindex(' ', ltrim(replace(ColumnDesc, '_', ' ')) , 0) > 2 then " & _
                    "		case when @Mini like '% ' +  SUBSTRING(replace(ColumnDesc, '_', ' ') , 0, charindex(' ', ltrim(replace(ColumnDesc, '_', ' ')) , 0)) + ' %' then 250  " & _
                    "			when @Mini like  SUBSTRING(replace(ColumnDesc, '_', ' ') , 0, charindex(' ', ltrim(replace(ColumnDesc, '_', ' ')) , 0)) + ' %' then 250  " & _
                    "			when @Mini like '% ' + SUBSTRING(replace(ColumnDesc, '_', ' ') , 0, charindex(' ', ltrim(replace(ColumnDesc, '_', ' ')) , 0)) then 250  " & _
                    "			when @Mini like '%' +  SUBSTRING(replace(ColumnDesc, '_', ' ') , 0, charindex(' ', ltrim(replace(ColumnDesc, '_', ' ')) , 0)) + '%' then 200  " & _
                    "			else 0 end " & _
                    "			 " & _
                    "	else 0 end  " & _
                    "	+ " & _
                    "case when charindex(' ', ltrim(Replace(Reverse(@Mini), '_', ' ')) ,0) > 2 then " & _
                    "	case when ColumnDesc = right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1) then 150 " & _
                    "		when ColumnDesc like '% ' +  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + ' %' then 250  " & _
                    "		when ColumnDesc like  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + ' %' then 250 " & _
                    "		when ColumnDesc like '% ' +  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  then 250 " & _
                    "		when ColumnDesc like '%' + right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + '%' then 200 " & _
                    "	else 0 end " & _
                    "	else case when charindex(' ', ltrim(Reverse(replace(ColumnDesc, '_', ' '))),0) > 2 then " & _
                    "			case when @Mini like '% ' +  right(rtrim(replace(ColumnDesc, '_', ' ')), charindex(' ', ltrim(Reverse(replace(ColumnDesc, '_', ' '))),0) -1) + ' %' then 250  " & _
                    "			when @Mini like  right(rtrim(replace(ColumnDesc, '_', ' ')), charindex(' ', ltrim(Reverse(replace(ColumnDesc, '_', ' '))),0) -1)  + ' %' then 250 " & _
                    "			when @Mini like '% ' + right(rtrim(replace(ColumnDesc, '_', ' ')), charindex(' ', ltrim(Reverse(replace(ColumnDesc, '_', ' '))),0) -1)  then 250 " & _
                    "			when @Mini like '%' + right(rtrim(replace(ColumnDesc, '_', ' ')), charindex(' ', ltrim(Reverse(replace(ColumnDesc, '_', ' '))),0) -1)  + '%' then 200 " & _
                    "			else 0 end " & _
                    "		else 0 end  " & _
                    "	end " & _
                    "	  " & _
                    "	 end  " & _
                    "	end " & _
                    "	 " & _
                    "	, tdscore = tdscore + " & _
                    "	 " & _
                    " " & _
                    "case when LEN(LTRIM(TechnicalDescription)) = 0 then 0 " & _
                    "else	 " & _
                    "case when TechnicalDescription = @Mini then 303 " & _
                    "	 when TechnicalDescription = REPLACE(@Mini, ' ', '') then 302 " & _
                    "	 when TechnicalDescription = REPLACE(@Mini, '_', '') then 302 " & _
                    "	 when @Mini = REPLACE(TechnicalDescription, ' ', '') then 302 " & _
                    "	 when @Mini = REPLACE(TechnicalDescription, '_', '') then 302 " & _
                    "	 when Replace(REPLACE(TechnicalDescription, '_', ''), ' ', '') = REPLACE(Replace(@Mini, '_', ''), ' ', '') then 302 " & _
                    "	 when TechnicalDescription like @Mini + '%' then 301  " & _
                    "	 when TechnicalDescription like @Mini + '%' then 301  " & _
                    "	 when TechnicalDescription like '%' + @Mini + '%' then 300  " & _
                    "	 when @Mini like '%' + TechnicalDescription + '%' then 290 " & _
                    "	 when @Mini like '%' + replace(TechnicalDescription, '_', ' ') + '%' then 285 " & _
                    "	 else 0  " & _
                    "	 + " & _
                    "case when charindex(' ', ltrim(@Mini), 0) > 2 and TechnicalDescription = SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) then 100 " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and TechnicalDescription like '% ' +  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + ' %' then 200  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and TechnicalDescription like  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + ' %' then 200  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and TechnicalDescription like '% ' + SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) then 200  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and TechnicalDescription like '%' +  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + '%' then 150  " & _
                    "	when charindex(' ', ltrim(replace(TechnicalDescription, '_', ' ')) , 0) > 2 then " & _
                    "		case when @Mini like '% ' +  SUBSTRING(replace(TechnicalDescription, '_', ' ') , 0, charindex(' ', ltrim(replace(TechnicalDescription, '_', ' ')) , 0)) + ' %' then 200  " & _
                    "			when @Mini like  SUBSTRING(replace(TechnicalDescription, '_', ' ') , 0, charindex(' ', ltrim(replace(TechnicalDescription, '_', ' ')) , 0)) + ' %' then 200  " & _
                    "			when @Mini like '% ' + SUBSTRING(replace(TechnicalDescription, '_', ' ') , 0, charindex(' ', ltrim(replace(TechnicalDescription, '_', ' ')) , 0)) then 200  " & _
                    "			when @Mini like '%' +  SUBSTRING(replace(TechnicalDescription, '_', ' ') , 0, charindex(' ', ltrim(replace(TechnicalDescription, '_', ' ')) , 0)) + '%' then 100  " & _
                    "			else 0 end " & _
                    "			 " & _
                    "	else 0 end  " & _
                    "	+ " & _
                    "case when charindex(' ', ltrim(Replace(Reverse(@Mini), '_', ' ')) ,0) > 2 then " & _
                    "	case when TechnicalDescription = right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1) then 100 " & _
                    "		when TechnicalDescription like '% ' +  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + ' %' then 200  " & _
                    "		when TechnicalDescription like  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + ' %' then 200 " & _
                    "		when TechnicalDescription like '% ' +  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  then 200 " & _
                    "		when TechnicalDescription like '%' + right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + '%' then 150 " & _
                    "	else 0 end " & _
                    "	else case when charindex(' ', ltrim(Reverse(replace(TechnicalDescription, '_', ' '))),0) > 2 then " & _
                    "			case when @Mini like '% ' +  right(rtrim(replace(TechnicalDescription, '_', ' ')), charindex(' ', ltrim(Reverse(replace(TechnicalDescription, '_', ' '))),0) -1) + ' %' then 200  " & _
                    "			when @Mini like  right(rtrim(replace(TechnicalDescription, '_', ' ')), charindex(' ', ltrim(Reverse(replace(TechnicalDescription, '_', ' '))),0) -1)  + ' %' then 200 " & _
                    "			when @Mini like '% ' + right(rtrim(replace(TechnicalDescription, '_', ' ')), charindex(' ', ltrim(Reverse(replace(TechnicalDescription, '_', ' '))),0) -1)  then 200 " & _
                    "			when @Mini like '%' + right(rtrim(replace(TechnicalDescription, '_', ' ')), charindex(' ', ltrim(Reverse(replace(TechnicalDescription, '_', ' '))),0) -1)  + '%' then 150 " & _
                    "			else 0 end " & _
                    "		else 0 end  " & _
                    "	end " & _
                    "	  " & _
                    "	 end  " & _
                    "	end " & _
                    " " & _
                    "	, cavscore = cavscore + " & _
                    "	 " & _
                    " " & _
                    "case when LEN(LTRIM(Caveat)) = 0 then 0 " & _
                    "else	 " & _
                    "case when Caveat = @Mini then 125 " & _
                    "	 when Caveat = REPLACE(@Mini, ' ', '') then 123 " & _
                    "	 when Caveat = REPLACE(@Mini, '_', '') then 123 " & _
                    "	 when @Mini = REPLACE(Caveat, ' ', '') then 123 " & _
                    "	 when @Mini = REPLACE(Caveat, '_', '') then 123 " & _
                    "	 when Replace(REPLACE(Caveat, '_', ''), ' ', '') = REPLACE(Replace(@Mini, '_', ''), ' ', '') then 123 " & _
                    "	 when Caveat like @Mini + '%' then 121  " & _
                    "	 when Caveat like @Mini + '%' then 121  " & _
                    "	 when Caveat like '%' + @Mini + '%' then 120  " & _
                    "	 when @Mini like '%' + Caveat + '%' then 100 " & _
                    "	 when @Mini like '%' + replace(Caveat, '_', ' ') + '%' then 85 " & _
                    "	 else 0  " & _
                    "	 + " & _
                    "case when charindex(' ', ltrim(@Mini), 0) > 2 and Caveat = SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) then 50 " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and Caveat like '% ' +  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + ' %' then 75  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and Caveat like  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + ' %' then 75  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and Caveat like '% ' + SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) then 75  " & _
                    "	when charindex(' ', ltrim(@Mini), 0) > 2 and Caveat like '%' +  SUBSTRING(@Mini, 0, charindex(' ', ltrim(@Mini), 0)) + '%' then 60  " & _
                    "	when charindex(' ', ltrim(replace(Caveat, '_', ' ')) , 0) > 2 then " & _
                    "		case when @Mini like '% ' +  SUBSTRING(replace(Caveat, '_', ' ') , 0, charindex(' ', ltrim(replace(Caveat, '_', ' ')) , 0)) + ' %' then 75  " & _
                    "			when @Mini like  SUBSTRING(replace(Caveat, '_', ' ') , 0, charindex(' ', ltrim(replace(Caveat, '_', ' ')) , 0)) + ' %' then 75  " & _
                    "			when @Mini like '% ' + SUBSTRING(replace(Caveat, '_', ' ') , 0, charindex(' ', ltrim(replace(Caveat, '_', ' ')) , 0)) then 75  " & _
                    "			when @Mini like '%' +  SUBSTRING(replace(Caveat, '_', ' ') , 0, charindex(' ', ltrim(replace(Caveat, '_', ' ')) , 0)) + '%' then 50  " & _
                    "			else 0 end " & _
                    "			 " & _
                    "	else 0 end  " & _
                    "	+ " & _
                    "case when charindex(' ', ltrim(Replace(Reverse(@Mini), '_', ' ')) ,0) > 2 then " & _
                    "	case when Caveat = right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1) then 50 " & _
                    "		when Caveat like '% ' +  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + ' %' then 75  " & _
                    "		when Caveat like  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + ' %' then 75 " & _
                    "		when Caveat like '% ' +  right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  then 75 " & _
                    "		when Caveat like '%' + right(rtrim(Replace(@Mini, '_', ' ')), charindex(' ', ltrim(Reverse(Replace(@Mini, '_', ' '))),0) -1)  + '%' then 60 " & _
                    "	else 0 end " & _
                    "	else case when charindex(' ', ltrim(Reverse(replace(Caveat, '_', ' '))),0) > 2 then " & _
                    "			case when @Mini like '% ' +  right(rtrim(replace(Caveat, '_', ' ')), charindex(' ', ltrim(Reverse(replace(Caveat, '_', ' '))),0) -1) + ' %' then 75  " & _
                    "			when @Mini like  right(rtrim(replace(Caveat, '_', ' ')), charindex(' ', ltrim(Reverse(replace(Caveat, '_', ' '))),0) -1)  + ' %' then 75 " & _
                    "			when @Mini like '% ' + right(rtrim(replace(Caveat, '_', ' ')), charindex(' ', ltrim(Reverse(replace(Caveat, '_', ' '))),0) -1)  then 75 " & _
                    "			when @Mini like '%' + right(rtrim(replace(Caveat, '_', ' ')), charindex(' ', ltrim(Reverse(replace(Caveat, '_', ' '))),0) -1)  + '%' then 50 " & _
                    "			else 0 end " & _
                    "		else 0 end  " & _
                    "	end " & _
                    "	  " & _
                    "	 end  " & _
                    "	end " & _
                    " " & _
                    " " & _
                    "end " & _
                    " " & _
                    "select titlescore + descscore + tdscore + cavscore AS ord,   " & _
                    "* " & _
                    "from #tempy " & _
                    "  " & _
                    WhereClause


            If SortDirection = "" Then
                initSQL += "order by titlescore + descscore + tdscore + cavscore desc "
                Sorts.Text = "ord"
                SortDir.Text = "desc"
            Else
                initSQL += "order by " & SortDirection & ", titlescore + descscore + tdscore + cavscore desc "
            End If

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                cmd = New SqlCommand(initSQL, conn)
                cmd.CommandTimeout = 86400

                Dim drSQL As SqlDataReader
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                drSQL = cmd.ExecuteReader
                Dim dr As DataRow
                While drSQL.Read
                    dr = tbl.NewRow()
                    If IsDBNull(drSQL.Item("ord")) Then
                        dr("ord") = ""
                    Else
                        dr("ord") = drSQL.Item("ord")
                    End If
                    If IsDBNull(drSQL.Item("ColumnName")) Then
                        dr("ColumnName") = ""
                    Else
                        dr("ColumnName") = drSQL.Item("ColumnName")
                    End If

                    If IsDBNull(drSQL.Item("DataType")) Then
                        dr("DataType") = ""
                    Else
                        dr("DataType") = drSQL.Item("DataType")
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
                    If IsDBNull(drSQL.Item("Caveat")) Then
                        dr("Caveat") = ""
                    Else
                        dr("Caveat") = drSQL.Item("Caveat")
                    End If
                    If IsDBNull(drSQL.Item("DB")) Then
                        dr("DB") = ""
                    Else
                        dr("DB") = drSQL.Item("DB")
                    End If
                    If IsDBNull(drSQL.Item("Schema")) Then
                        dr("Schema") = ""
                    Else
                        dr("Schema") = drSQL.Item("Schema")
                    End If
                    If IsDBNull(drSQL.Item("Table")) Then
                        dr("Table") = ""
                    Else
                        dr("Table") = drSQL.Item("Table")
                    End If
                    If IsDBNull(drSQL.Item("Column")) Then
                        dr("Column") = ""
                    Else
                        dr("Column") = drSQL.Item("Column")
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

                tblHeaders.Visible = False
                lblRecommendations.Visible = True
            Else
                tblHeaders.Visible = True
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

    Private Sub gvDataDictionary_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDataDictionary.RowDataBound

        Dim phrase As String = Trim(Replace(Replace(txtBasicSearch.Text, "'", "''"), ",", " "))
        Dim mini As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblData As Label = e.Row.FindControl("lblDataName")
            Dim lblDesc As Label = e.Row.FindControl("lblGVDescription")
            Dim lblTD As Label = e.Row.FindControl("lblGVTechDescription")
            Dim lblCav As Label = e.Row.FindControl("lblGVCaveat")

            While Len(phrase) > 0
                If InStr(phrase, " ") > 0 Then
                    mini = Trim(Left(phrase, InStr(phrase, " ")))
                    phrase = Trim(Mid(phrase, InStr(phrase, " ")))
                Else
                    mini = Trim(phrase)
                    phrase = ""
                End If
                lblData.Text = Highlight(lblData.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblDesc.Text = Highlight(lblDesc.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblTD.Text = Highlight(lblTD.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
                lblCav.Text = Highlight(lblCav.Text, mini, "<b><font color=#2b74bb>", "</font></b>")
            End While

        End If



    End Sub

    Function Highlight(strText, strFind, strBefore, strAfter)
        Dim nPos
        Dim nLen
        Dim nLenAll

        nLen = Len(strFind)
        nLenAll = nLen + Len(strBefore) + Len(strAfter) + 1

        Highlight = strText

        If nLen > 0 And Len(Highlight) > 0 Then
            nPos = InStr(1, Highlight, strFind, 1)
            Do While nPos > 0
                Highlight = Left(Highlight, nPos - 1) & _
                    strBefore & Mid(Highlight, nPos, nLen) & strAfter & _
                    Mid(Highlight, nPos + nLen)

                nPos = InStr(nPos + nLenAll, Highlight, strFind, 1)
            Loop
        End If
    End Function

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
    Sub SortSchema(sortex As String)
        Try
            'Dim dataTable As DataTable = TryCast(gvFDSchema.DataSource, DataTable)
            Dim temp As String = gvDataDictionary.SortDirection

            If gvDataDictionary.PageIndex <> 0 Then
                gvDataDictionary.PageIndex = 0
            End If

            If Sorts.Text = sortex Then
                If SortDir.Text = "asc" Then
                    LoadDataDictionary(sortex & " desc ")
                    SortDir.Text = "desc"
                Else
                    LoadDataDictionary(sortex & " asc ")
                    SortDir.Text = "asc"
                End If
            Else
                Sorts.Text = sortex

                If gvDataDictionary.SortDirection = SortDirection.Ascending Then
                    LoadDataDictionary(sortex & " asc ")
                    SortDir.Text = "asc"
                Else
                    LoadDataDictionary(sortex & " desc ")
                    SortDir.Text = "desc"
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub lnkSch1_Click(sender As Object, e As EventArgs) Handles lnkSch1.Click
        SortSchema("ColumnName")
    End Sub

    Private Sub lnkSch2_Click(sender As Object, e As EventArgs) Handles lnkSch2.Click
        SortSchema("DataType")
    End Sub

    Private Sub lnkSch3_Click(sender As Object, e As EventArgs) Handles lnkSch3.Click
        SortSchema("ColumnDesc")
    End Sub

    Private Sub lnkSch4_Click(sender As Object, e As EventArgs) Handles lnkSch4.Click
        SortSchema("TechnicalDescription")
    End Sub

    Private Sub lnkSch5_Click(sender As Object, e As EventArgs) Handles lnkSch5.Click
        SortSchema("Caveat")
    End Sub

    Private Sub lnkSch6_Click(sender As Object, e As EventArgs) Handles lnkSch6.Click
        SortSchema("DB")
    End Sub

    Private Sub lnkSch7_Click(sender As Object, e As EventArgs) Handles lnkSch7.Click
        SortSchema("[Schema]")
    End Sub

    Private Sub lnkSch8_Click(sender As Object, e As EventArgs) Handles lnkSch8.Click
        SortSchema("[Table]")
    End Sub

    Private Sub lnkSch9_Click(sender As Object, e As EventArgs) Handles lnkSch9.Click
        SortSchema("[Column]")
    End Sub
End Class