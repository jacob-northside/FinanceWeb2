Imports System
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls
Imports DocumentFormat.OpenXml

Imports System.Configuration
Imports System.Collections
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
 

Public Class FRED
    Inherits System.Web.UI.Page
    Dim SQL, SQL2, SQL3 As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd, cmd2 As SqlCommand
    Dim cmdNS1 As SqlCommand
    Dim dr As SqlDataReader
    Public SQLArray() As String
    Public FREDData As SqlDataSource

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then
              
            Else
                CostingCheck()
                rblDataType.SelectedIndex = 0
                FREDReportType(0)
                LoadFREDFreeFilters()

                ResetFREDFilters()
                'acrFREDSelect
                'pnlEncounterChk


                Exit Sub

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub CostingCheck()
        Try
            Dim dr As SqlDataReader

            SQL = "Select CostingAccess " & _
                "From WebFD.FRED.USERAccess " & _
                "where USERID = '" & Page.User.Identity.Name & "'  "
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("CostingAccess")) Then
                        lblCosting.Text = ""
                    Else
                        If dr.Item("CostingAccess") = "1" Then
                            lblCosting.Text = "Costing Access Granted"
                        Else
                            lblCosting.Text = ""
                        End If
                    End If

                End While
                dr.Read()
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rblDataType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblDataType.SelectedIndexChanged
        Try
            FREDReportType(rblDataType.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDReportType(ByRef FredType As Integer)
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            ddlFREDTables.Visible = True

            If FredType = 0 Then
                SQL = "select distinct FREDType, Table_Alias, TABLE_ID from WebFD.FRED.vw_AvailableData where FREDType = 0  order by 1 desc, 2  "
            Else
                SQL = "select distinct FREDType, Table_Alias, TABLE_ID from WebFD.FRED.vw_AvailableData where FREDType <> 0  order by 1 desc, 2   "
            End If

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "FRED")

                ddlFREDTables.DataSource = ds
                ddlFREDTables.DataMember = "FRED"
                ddlFREDTables.DataTextField = "Table_Alias"
                ddlFREDTables.DataValueField = "TABLE_ID"
                ddlFREDTables.DataBind()
            End Using
            If FredType = 0 Then
                SetFREDSelectAccordion(0)
                ddlFREDTables.Visible = False
            Else : SetFREDSelectAccordion(ddlFREDTables.SelectedValue)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetFREDSelectAccordion(ByRef FREDSelectAccordion As Integer)
        Try
            Dim cntFRED As Integer = 1
            Dim ds As New DataSet
            Dim ds2 As DataSet

            Dim da As New SqlDataAdapter
            Dim da2 As New SqlDataAdapter
            Dim dReader As SqlDataReader
            Dim Costing As String = ""

            lblFREDSelect1.Text = ""
            lblFREDSelect2.Text = " "
            lblFREDSelect3.Text = ""
            cblFREDSelect1.DataSource = ds
            cblFREDSelect2.DataSource = ds
            cblFREDSelect3.DataSource = ds

            lblFREDTable1.Text = ""
            lblFREDKey1.Text = ""
            lblFREDTable2.Text = ""
            lblFREDKey2.Text = ""
            lblFREDTable3.Text = ""
            lblFREDKey3.Text = ""

            If lblCosting.Text = "" Then
                Costing = "0"
            Else
                Costing = "0,1"
            End If

            SQL = "select distinct FREDType, Table_Alias, TABLE_ID, " & _
                " (TABLE_CATALOG + '.' +  TABLE_SCHEMA + '.' + TABLE_NAME) as TableID, " & _
                " COLUMN_NAME " & _
                " from WebFD.FRED.vw_AvailableData " & _
                " where (Table_ID =  " & FREDSelectAccordion & " or FREDTYPE = 0) " & _
                " and Costing in (" & Costing & ") " & _
                " and FREDConnect = 1 " & _
                " order by 1 desc, 2 "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "FRED")

                If ds.Tables(0).Rows.Count <> 0 Then
                    Dim lbTitle As Label
                    Dim TblID As String
                    Dim ColList As String
                    Dim cblContent As CheckBoxList
                    Dim dr As DataRow
                    For Each dr In ds.Tables(0).Rows
                        lbTitle = New Label()
                        cblContent = New CheckBoxList()
                        TblID = ""
                        ColList = ""
                        ds2 = New DataSet

                        lbTitle.Text = ">>" & dr("Table_Alias").ToString()
                        TblID = dr("TABLE_ID").ToString()

                        Select Case cntFRED
                            Case 1
                                lblFREDSelect1.Text = lbTitle.Text
                                lblFREDTable1.Text = dr("TableID").ToString()
                                lblFREDKey1.Text = dr("COLUMN_NAME").ToString()
                            Case 2
                                If lblFREDSelect1.Text <> lbTitle.Text Then
                                    lblFREDSelect2.Text = lbTitle.Text
                                    lblFREDTable2.Text = dr("TableID").ToString()
                                    lblFREDKey2.Text = dr("COLUMN_NAME").ToString()
                                End If
                            Case Else
                                If lblFREDSelect1.Text <> lbTitle.Text And lblFREDSelect2.Text <> lbTitle.Text Then
                                    lblFREDSelect3.Text = lbTitle.Text
                                    lblFREDTable3.Text = dr("TableID").ToString()
                                    lblFREDKey3.Text = dr("COLUMN_NAME").ToString()
                                End If
                        End Select

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        SQL = "select  Column_Alias ,  'ID:' + convert(varchar(max),COLUMN_ID ) + ' Desc: ' + isnull(ColumnDesc,'---') as cValue " & _
                            " from WebFD.FRED.vw_AvailableData  where Table_ID =  " & TblID & "  and Costing in (" & Costing & ")  order by 1  "

                        cmd2 = New SqlCommand(SQL, conn)
                        da2.SelectCommand = cmd2
                        da2.SelectCommand.CommandTimeout = 86400
                        da2.Fill(ds2, "FRED")

                        Select Case cntFRED
                            Case 1
                                cblFREDSelect1.DataSource = ds2
                                cblFREDSelect1.DataMember = "FRED"
                                cblFREDSelect1.DataTextField = "Column_Alias"
                                cblFREDSelect1.DataValueField = "cValue"
                                cblFREDSelect1.DataBind()

                                If cblFREDSelect1 IsNot Nothing Then
                                    For Each li As ListItem In cblFREDSelect1.Items
                                        li.Attributes("title") = li.Value
                                    Next
                                End If
                            Case 2
                                cblFREDSelect2.DataSource = ds2
                                cblFREDSelect2.DataMember = "FRED"
                                cblFREDSelect2.DataTextField = "Column_Alias"
                                cblFREDSelect2.DataValueField = "cValue"
                                cblFREDSelect2.DataBind()

                                If cblFREDSelect2 IsNot Nothing Then
                                    For Each li As ListItem In cblFREDSelect2.Items
                                        li.Attributes("title") = li.Value
                                    Next
                                End If
                            Case Else
                                cblFREDSelect3.DataSource = ds2
                                cblFREDSelect3.DataMember = "FRED"
                                cblFREDSelect3.DataTextField = "Column_Alias"
                                cblFREDSelect3.DataValueField = "cValue"
                                cblFREDSelect3.DataBind()

                                If cblFREDSelect3 IsNot Nothing Then
                                    For Each li As ListItem In cblFREDSelect3.Items
                                        li.Attributes("title") = li.Value
                                    Next
                                End If
                        End Select

                        SQL = "DECLARE @List varchar(max)   " & _
                              "SET @List = ''    " & _
                              "SELECT @List = @List +  CHAR(36) + Cast([Column_Alias] As varchar(max)) +  CHAR(36) + ','  " & _
                              "FROM  [WebFD].[FRED].[vw_AvailableData]  " & _
                              "where TABLE_ID = " & TblID & " and sDefault = 1   " & _
                              "if LEN(@List) > 1 " & _
                              "Begin " & _
                              "SET @List = SUBSTRING(@List, 1, Len(@List) - 1)   " & _
                              "end " & _
                              "SELECT @List As 'List'  "

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd2 = New SqlCommand(SQL, conn)
                        dReader = cmd2.ExecuteReader
                        While dReader.Read
                            If IsDBNull(dReader.Item("List")) Then
                                ColList = ""
                            Else
                                ColList = dReader.Item("List")
                            End If
                        End While
                        dReader.Close()
                        Select Case cntFRED
                            Case 1
                                For j As Integer = 0 To cblFREDSelect1.Items.Count - 1
                                    If ColList.Contains("$" & cblFREDSelect1.Items(j).ToString & "$") Then
                                        cblFREDSelect1.Items(j).Selected = True
                                    End If
                                Next
                            Case 2
                                For j As Integer = 0 To cblFREDSelect2.Items.Count - 1
                                    If ColList.Contains("$" & cblFREDSelect2.Items(j).ToString & "$") Then
                                        cblFREDSelect2.Items(j).Selected = True
                                    End If
                                Next
                            Case Else
                                For j As Integer = 0 To cblFREDSelect3.Items.Count - 1
                                    If ColList.Contains("$" & cblFREDSelect3.Items(j).ToString & "$") Then
                                        cblFREDSelect3.Items(j).Selected = True
                                    End If
                                Next
                        End Select
                        cntFRED += 1
                    Next
                End If

                If lblFREDSelect2.Text = " " Then
                    apFREDSelect2.Visible = False
                Else
                    apFREDSelect2.Visible = True
                End If

                If lblFREDSelect3.Text = "" Then
                    apFREDSelect3.Visible = False
                Else
                    apFREDSelect3.Visible = True
                End If

                'If ds.Tables(0).Rows.Count <> 0 Then
                '    Dim lbTitle As Label
                '    Dim TblID As String
                '    Dim ColList As String
                '    Dim cblContent As CheckBoxList
                '    Dim pn As AjaxControlToolkit.AccordionPane
                '    Dim i As Integer = 0  ' This is just to use for assigning pane an id
                '    Dim dr As DataRow
                '    For Each dr In ds.Tables(0).Rows
                '        lbTitle = New Label()
                '        cblContent = New CheckBoxList()
                '        TblID = ""
                '        ColList = ""
                '        ds2 = New DataSet

                '        lbTitle.Text = dr("Table_Alias").ToString()
                '        TblID = dr("TABLE_ID").ToString()

                '        pn = New AjaxControlToolkit.AccordionPane()
                '        pn.ID = i
                '        pn.HeaderContainer.Controls.Add(lbTitle)

                '        If conn.State = ConnectionState.Closed Then
                '            conn.Open()
                '        End If
                '        SQL = "select  Column_Alias ,  'ID:' + convert(varchar(max),COLUMN_ID ) + ' Desc: ' + isnull(ColumnDesc,'---') as cValue " & _
                '                     " from WebFD.FRED.vw_AvailableData  where Table_ID =  " & TblID & "  order by 1  "
                '        cmd2 = New SqlCommand(SQL, conn)
                '        da2.SelectCommand = cmd2
                '        da2.SelectCommand.CommandTimeout = 86400
                '        da2.Fill(ds2, "FRED")

                '        cblContent.DataSource = ds2
                '        cblContent.DataMember = "FRED"
                '        cblContent.DataTextField = "Column_Alias"
                '        cblContent.DataValueField = "cValue"
                '        cblContent.DataBind()

                '        If cblContent IsNot Nothing Then
                '            For Each li As ListItem In cblContent.Items
                '                li.Attributes("title") = li.Value
                '            Next
                '        End If

                '        pn.ContentContainer.Controls.Add(cblContent)
                '        acrFREDSelect.Panes.Add(pn)

                '        SQL = "DECLARE @List varchar(max)   " & _
                '              "SET @List = ''    " & _
                '              "SELECT @List = @List +  CHAR(36) + Cast([Column_Alias] As varchar(max)) +  CHAR(36) + ','  " & _
                '              "FROM  [WebFD].[FRED].[vw_AvailableData]  " & _
                '              "where TABLE_ID = " & TblID & " and sDefault = 1   " & _
                '              "if LEN(@List) > 1 " & _
                '              "Begin " & _
                '              "SET @List = SUBSTRING(@List, 1, Len(@List) - 1)   " & _
                '              "end " & _
                '              "SELECT @List As 'List'  "

                '        If conn.State = ConnectionState.Closed Then
                '            conn.Open()
                '        End If
                '        cmd2 = New SqlCommand(SQL, conn)
                '        dReader = cmd2.ExecuteReader
                '        While dReader.Read
                '            If IsDBNull(dReader.Item("List")) Then
                '                ColList = ""
                '            Else

                '                ColList = dReader.Item("List")
                '            End If
                '        End While
                '        dReader.Close()

                '        For j As Integer = 0 To cblContent.Items.Count - 1
                '            If ColList.Contains("$" & cblContent.Items(j).ToString & "$") Then
                '                cblContent.Items(j).Selected = True
                '            End If
                '        Next
                '        i = i + 1
                '    Next
                'End If

            End Using


        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDTables_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDTables.SelectedIndexChanged

        SetFREDSelectAccordion(ddlFREDTables.SelectedValue)

    End Sub
    Private Sub LoadFREDFreeFilters()
        Try
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter

            ' where FREDType = 1
            SQL = " select distinct Table_Alias, Table_ID  ,FREDType  from WebFD.FRED.vw_AvailableData   " & _
                "where FREDType <> 0 " & _
                "union all " & _
                "select 'N/A', 0, 0 " & _
                "order by FREDType asc  , Table_Alias  "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "FRED")

                rblFREDFilter1.DataSource = ds
                rblFREDFilter1.DataMember = "FRED"
                rblFREDFilter1.DataTextField = "Table_Alias"
                rblFREDFilter1.DataValueField = "Table_ID"
                rblFREDFilter1.DataBind()

                rblFREDFilter2.DataSource = ds
                rblFREDFilter2.DataMember = "FRED"
                rblFREDFilter2.DataTextField = "Table_Alias"
                rblFREDFilter2.DataValueField = "Table_ID"
                rblFREDFilter2.DataBind()

                rblFREDFilter3.DataSource = ds
                rblFREDFilter3.DataMember = "FRED"
                rblFREDFilter3.DataTextField = "Table_Alias"
                rblFREDFilter3.DataValueField = "Table_ID"
                rblFREDFilter3.DataBind()

                rblFREDFilter4.DataSource = ds
                rblFREDFilter4.DataMember = "FRED"
                rblFREDFilter4.DataTextField = "Table_Alias"
                rblFREDFilter4.DataValueField = "Table_ID"
                rblFREDFilter4.DataBind()

                rblFREDFilter5.DataSource = ds
                rblFREDFilter5.DataMember = "FRED"
                rblFREDFilter5.DataTextField = "Table_Alias"
                rblFREDFilter5.DataValueField = "Table_ID"
                rblFREDFilter5.DataBind()
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Protected Function ReadByteStreamFromFileAndOutput(ByRef Buffer As Byte(), filepath As String) As Boolean
        Dim strFileName As String = filepath
        Dim Stream As FileStream
        Try
            If File.Exists(strFileName) Then
                Stream = File.Open(strFileName, FileMode.Open, FileAccess.Read, FileShare.None)
                Buffer = New Byte(Stream.Length - 1) {}
                Stream.Seek(0, SeekOrigin.Begin)
                Stream.Read(Buffer, 0, Buffer.Length)
                Stream.Flush()
                Stream.Close()
            End If
        Catch generatedExceptionName As Exception
            Return False
        End Try
        Return True
    End Function
    Protected Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter
            Dim FileName As String = "FRED_" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "_" & _
            Replace(Replace(Replace(Date.Now.ToString, "/", "_"), " ", ""), ":", "_")

            SQL3 = "select  * from WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")

            If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "" Then
                Exit Sub
            End If

            If chbExcel2007.Checked = True Then
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    ds = New DataSet

                    da = New SqlDataAdapter(SQL3, conn)
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds)
                End Using

                CreateExcelFile.CreateExcelDocument(ds, "C:\TEMP\FREDDUMP\" & FileName & ".xlsx")

                Dim buffer As Byte() = Nothing
                Dim filepath As String = "C:\TEMP\FREDDUMP\" & FileName & ".xlsx"
                FileName = FileName & ".xlsx"
                If Not ReadByteStreamFromFileAndOutput(buffer, filepath) Then
                    Return
                End If
                Response.ClearContent()
                Response.ClearHeaders()
                Response.ContentType = "application/octet-stream"

                Response.AddHeader("Content-disposition", "attachment; filename=" & FileName)
                Response.BinaryWrite(buffer)

                Response.Flush()
                Response.Close()
                Exit Sub
            Else
                Dim dt As New DataTable
                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    ds = New DataSet

                    da = New SqlDataAdapter(SQL3, conn)
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(dt)
                    dt.TableName = "DEMO"
                End Using

                'SAVE THIS CODE UNTIL THIS PART IS COMPLETE, THIS IS A BACK UP OF THE WORKING EXPORT
                Dim attachment As String = "attachment; filename=" & FileName & ".xls"
                Response.ClearContent()
                Response.AddHeader("content-disposition", attachment)
                Response.ContentType = "application/vnd.ms-excel"

                Dim tab As String = ""
                For Each dc As DataColumn In dt.Columns
                    Response.Write(tab + dc.ColumnName)
                    tab = vbTab
                Next
                Response.Write(vbLf)

                Dim i As Integer
                For Each dr As DataRow In dt.Rows
                    tab = ""
                    For i = 0 To dt.Columns.Count - 1
                        Response.Write(tab & dr(i).ToString())
                        tab = vbTab
                    Next
                    Response.Write(vbLf)
                Next
                Response.End()
            End If




            'Dim attachment As String
            'Dim tab As String = ""

            'If chbExcel2007.Checked = True Then
            '    attachment = "attachment; filename=" & FileName & ".xlsx"
            '    Response.ClearContent()
            '    Response.AddHeader("content-disposition", attachment)
            '    Response.ContentType = "application/vnd.openxmlformats"

            '    'Dim ms As MemoryStream = New MemoryStream()

            '    For Each dc As DataColumn In dt.Columns
            '        Response.Write(tab + dc.ColumnName)
            '        tab = vbTab
            '    Next
            '    Response.Write(vbLf)

            '    Dim i As Integer
            '    For Each dr As DataRow In dt.Rows
            '        tab = ""
            '        For i = 0 To dt.Columns.Count - 1
            '            Response.Write(tab & dr(i).ToString())
            '            tab = vbTab
            '        Next
            '        Response.Write(vbLf)
            '    Next
            '    Response.End()

            'Else
            'attachment = "attachment; filename=" & FileName & ".xls"
            'Response.ClearContent()
            'Response.AddHeader("content-disposition", attachment)
            'Response.ContentType = "application/vnd.ms-excel"

            'For Each dc As DataColumn In dt.Columns
            '    Response.Write(tab + dc.ColumnName)
            '    tab = vbTab
            'Next
            'Response.Write(vbLf)

            'Dim i As Integer
            'For Each dr As DataRow In dt.Rows
            '    tab = ""
            '    For i = 0 To dt.Columns.Count - 1
            '        Response.Write(tab & dr(i).ToString())
            '        tab = vbTab
            '    Next
            '    Response.Write(vbLf)
            'Next
            'Response.End()
            'End If








            'THIS WOULD BE USED IF THE EXPORT WAS COMING DIRECTLY FROM THE GRIDVIEW 
            'Dim sw As New StringWriter()
            'Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            'Dim frm As HtmlForm = New HtmlForm()
            ''Dim FileName As String = "FSILookUpFile.xls"

            'If gvDWHLookups.Rows.Count <> 0 Then

            '    'original code  
            '    Page.Response.AddHeader("content-disposition", "attachment;filename=FSILookUpFile.xls")
            '    Page.Response.ContentType = "application/vnd.ms-excel"
            '    '  Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" 
            '    Page.Response.Charset = ""
            '    Page.EnableViewState = False
            '    frm.Attributes("runat") = "server"
            '    Controls.Add(frm)
            '    frm.Controls.Add(gvDWHLookups)
            '    frm.RenderControl(hw)
            '    Response.Write(sw.ToString())
            '    'This line works instead of the Response.end () if needed
            '    ' HttpContext.Current.ApplicationInstance.CompleteRequest()
            '    Response.End()
            'Else
            '    btnExportToExcel.Visible = False

            'End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        Try
            'BuildFRED()
            'Exit Sub
            gvDWHLookups.PageIndex = 1
            BindFred()

            Exit Sub

            Dim ds As DataSet
            Dim da As SqlDataAdapter
            Dim cntResults As Integer

            BuildFRED()
            If SQL = "" Or SQL2 = "" Then
                Exit Sub
            End If

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                ds = New DataSet

                da = New SqlDataAdapter(SQL, conn)
                da.SelectCommand.CommandTimeout = 86400

                da.Fill(ds, "DEMO")
                gvDWHLookups.DataSource = ds
                gvDWHLookups.DataMember = "DEMO"
                gvDWHLookups.DataBind()
            End Using

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL2, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cntResults = cmd.ExecuteScalar
                If cntResults > 1048576 Then
                    lblResults.Text = "Count: " & cntResults.ToString & "  EXCEL LIMIT EXCEEDED "
                Else : lblResults.Text = "Count: " & cntResults.ToString
                End If
            End Using


        Catch ex As Exception
            lblResults.Text = "Count: 0 - Bad data type in Filter(s)."
        
        Finally

        End Try
    End Sub
    Sub BindFred()
        Try
            Dim ds As DataSet
            Dim da As SqlDataAdapter
            Dim cntResults As Integer

            BuildFRED()
            If SQL = "" Or SQL2 = "" Then
                Exit Sub
            End If

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL2, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cntResults = cmd.ExecuteScalar
                If cntResults > 1048576 Then
                    lblResults.Text = "Count: " & cntResults.ToString & "  EXCEL LIMIT EXCEEDED "
                Else : lblResults.Text = "Count: " & cntResults.ToString
                End If
            End Using

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                ds = New DataSet

                da = New SqlDataAdapter(SQL, conn)
                da.SelectCommand.CommandTimeout = 86400

                da.Fill(ds, "DEMO")
                gvDWHLookups.DataSource = ds
                gvDWHLookups.DataMember = "DEMO"
                gvDWHLookups.DataBind()

                'FREDData = New SqlDataSource
                'FREDData.ConnectionString = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString
                'FREDData.SelectCommand = SQL

                'gvDWHLookups.DataSource = FREDData
                'gvDWHLookups.DataBind()


            End Using

            If gvDWHLookups.Visible = False Then
                gvDWHLookups.Visible = True
            End If

        Catch ex As Exception
            lblResults.Text = lblResults.Text & vbCrLf & " OUT OF MEMORY CANNOT DISPLAY RESULTS "
            gvDWHLookups.Visible = False

        End Try
    End Sub
    Sub BuildFRED()
        Try
            Dim temp As String = ""
            Dim SQLSelect As String = ""
            Dim SQLTable As String = ""
            Dim tempFRED As String = ""
            Dim FREDFilter1 As String = ""
            Dim FREDFilter2 As String = ""
            Dim FREDFilter3 As String = ""
            Dim FREDFilter4 As String = ""
            Dim FREDFilter5 As String = ""
            Dim SQLWhere As String = ""
            Dim SQLFRED As String = ""
            Dim SQLDataType As String = ""
            Dim FREDConnect As String = ""
            Dim FREDTemp As String = ""
            Dim FREDchk As String = ""


            Dim ERRORMESSAGE As String = "ERRORMESSAGE GOES HERE"
            FREDTemp = "If Object_ID('WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') is not null " & _
                " begin " & _
                   "Drop table WebFD.FredTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                " end; " & _
                " Create table WebFD.FredTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & " ( " & _
                ""

            For i As Integer = 0 To cblFREDSelect1.Items.Count - 1
                If cblFREDSelect1.Items(i).Selected = True Then
                    SQLSelect = SQLSelect & Mid(cblFREDSelect1.Items(i).Value, 4, InStr(cblFREDSelect1.Items(i).Value, "Desc:") - 5) & ","
                End If
            Next
            For i As Integer = 0 To cblFREDSelect2.Items.Count - 1
                If cblFREDSelect2.Items(i).Selected = True Then
                    SQLSelect = SQLSelect & Mid(cblFREDSelect2.Items(i).Value, 4, InStr(cblFREDSelect2.Items(i).Value, "Desc:") - 5) & ","
                End If
            Next
            For i As Integer = 0 To cblFREDSelect3.Items.Count - 1
                If cblFREDSelect3.Items(i).Selected = True Then
                    SQLSelect = SQLSelect & Mid(cblFREDSelect3.Items(i).Value, 4, InStr(cblFREDSelect3.Items(i).Value, "Desc:") - 5) & ","
                End If
            Next
            If SQLSelect <> "" Then
                SQLSelect = Mid(SQLSelect, 1, Len(SQLSelect) - 1)
            Else
                lblResults.Text = "Count: 0 - NO COLUMNS SELECTED TO THE LEFT. "
                Exit Sub
            End If

            'SQL = "declare @SQLTables as varchar(max) " & _
            '"declare  @SQLSelect as  varchar(max) " & _
            '"  Declare @TableID as varchar(max) " & _
            '"set @SQLSelect = ''  SELECT  @SQLSelect  = @SQLSelect +  (TABLE_CATALOG + '.' + TABLE_SCHEMA + '.' + TABLE_NAME + '.[' + COLUMN_NAME+']'  ) + ',' " & _
            '"FROM [WebFD].[FRED].[vw_AvailableData] " & _
            '"where COLUMN_ID in (" & SQLSelect & ") " & _
            '"if len(@SQLSelect)  > 1  " & _
            '"begin  " & _
            '"set @SQLSelect = SUBSTRING(@SQLSelect, 1, Len(@SQLSelect) - 1) " & _
            '"end  " & _
            '"set @TableID = (select distinct TABLE_ID   " & _
            '"FROM [WebFD].[FRED].[vw_AvailableData]  " & _
            '"where COLUMN_ID in  (" & SQLSelect & ") and FREDType = 1  ) " & _
            '"set @SQLTables = (  SELECT distinct    (TABLE_CATALOG + '.' + TABLE_SCHEMA + '.' + TABLE_NAME  + ' on ' + TABLE_NAME  + '.[' + COLUMN_NAME+']'   )  " & _
            '"FROM [WebFD].[FRED].[vw_AvailableData]  " & _
            '"where Table_ID = @TableID and FREDType = 1  and FREDConnect = 1 ) " & _
            '"select @SQLSelect as 'SQLSelect' , @SQLTables as 'SQLTable' "

            'SQL = "declare @SQLTables as varchar(max) " & _
            '"declare  @SQLSelect as  varchar(max) " & _
            '"Declare @FREDTemp as varchar(max)  " & _
            '"set @SQLSelect = ''  SELECT  @SQLSelect  = @SQLSelect +  (TABLE_CATALOG + '.' + TABLE_SCHEMA + '.' + TABLE_NAME + '.[' + COLUMN_NAME+']'  ) + ',' " & _
            '"FROM [WebFD].[FRED].[vw_AvailableData] " & _
            '"where COLUMN_ID in (" & SQLSelect & ") " & _
            '"set @FREDTemp = ''  SELECT  @FREDTemp  = @FREDTemp +  (  COLUMN_NAME+'['+DataType +']'   ) + ','  " & _
            '"FROM [WebFD].[FRED].[vw_AvailableData]  " & _
            '"where COLUMN_ID in (" & SQLSelect & ") " & _
            '"if len(@SQLSelect)  > 1  " & _
            '"begin  " & _
            '"set @SQLSelect = SUBSTRING(@SQLSelect, 1, Len(@SQLSelect) - 1) " & _
            '"set @FREDTemp = SUBSTRING(@FREDTemp, 1, Len(@FREDTemp) - 1)  " & _
            '"end  " & _
            '"select @SQLSelect as 'SQLSelect' , @FREDTemp as 'FREDTEMP'  "

            SQL = "declare @SQLTables as varchar(max) " & _
          "declare  @SQLSelect as  varchar(max) " & _
          "set @SQLSelect = ''  SELECT  @SQLSelect  = @SQLSelect +  " & _
          "(TABLE_CATALOG + '.' + TABLE_SCHEMA + '.' + TABLE_NAME + '.[' + COLUMN_NAME+']' + ' as ' + TABLE_NAME + '_' +COLUMN_NAME   ) + ',' " & _
          "FROM [WebFD].[FRED].[vw_AvailableData] " & _
          "where COLUMN_ID in (" & SQLSelect & ") " & _
          "if len(@SQLSelect)  > 1  " & _
          "begin  " & _
          "set @SQLSelect = SUBSTRING(@SQLSelect, 1, Len(@SQLSelect) - 1) " & _
          "end  " & _
          "select @SQLSelect as 'SQLSelect'   "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(SQL, conn)
                cmd.CommandTimeout = 86400

                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("SQLSelect")) Then
                        SQLSelect = ""
                    Else : SQLSelect = dr.Item("SQLSelect")
                    End If
                    'If IsDBNull(dr.Item("SQLTable")) Then
                    '    SQLTable = ""
                    'Else : SQLTable = dr.Item("SQLTable")
                    'End If
                    'If IsDBNull(dr.Item("FREDTemp")) Then
                    '    FREDTemp = ""
                    'Else : FREDTemp = FREDTemp & dr.Item("FREDTemp") & ")"
                    'End If
                End While
                dr.Close()
            End Using

            If SQLSelect = "" Then
                lblResults.Text = "Count: 0 " & ERRORMESSAGE
                Exit Sub
            End If
            Dim chk As String = "0"
            If rblDataType.Items(1).Selected = True Then
                For i As Integer = 0 To cblFREDSelect1.Items.Count - 1
                    If cblFREDSelect1.Items(i).Selected = True Then
                        chk = "1"
                    End If
                Next
            End If
            'If SQLTable = "" Or chk <> "1" Then
            '    SQLTable = " from  ODS.STAR.Account  " & _
            '        "left join DWH.TSTAR.Account_Detail   on ODS.STAR.Account.PatientAccountFacility = DWH.TSTAR.Account_Detail.PAT_ACCT_FAC  "
            'Else
            '    SQLTable = " from  ODS.STAR.Account  " & _
            '        "left join DWH.TSTAR.Account_Detail   on ODS.STAR.Account.PatientAccountFacility = DWH.TSTAR.Account_Detail.PAT_ACCT_FAC  " & _
            '        "left join " & SQLTable & " = ODS.STAR.Account.PatientAccountFacility "
            'End If

            If lblFREDTable1.Text <> "" And lblFREDTable2.Text <> "" Then
                SQLTable = " From " & lblFREDTable1.Text & _
               " left join " & lblFREDTable2.Text & " on " & lblFREDTable1.Text & "." & lblFREDKey1.Text & " = " & lblFREDTable2.Text & "." & lblFREDKey2.Text
            End If
            If lblFREDTable3.Text <> "" Then
                SQLTable = SQLTable & "  left join " & lblFREDTable3.Text & " on " & lblFREDTable1.Text & "." & lblFREDKey1.Text & " = " & lblFREDTable3.Text & "." & lblFREDKey3.Text
            End If

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                If rblFREDFilter1.SelectedIndex <> 0 Then
                    SQL = "select " & _
                    "(TABLE_CATALOG + '.'+ TABLE_SCHEMA + '.' + TABLE_NAME) as FREDTable " & _
                    ",COLUMN_NAME as FREDConnect,   " & _
                    "(select DataType  from WebFD.FRED.vw_AvailableData where TABLE_ID = " & rblFREDFilter1.SelectedValue.ToString & _
                    "  and Column_Alias = '" & ddlFREDFilter1.SelectedValue & "' ) as dataType " & _
                    "from WebFD.FRED.vw_AvailableData " & _
                    "where TABLE_ID  = " & rblFREDFilter1.SelectedValue.ToString & " and FREDConnect = 1 "

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("dataType")) Then
                            SQLDataType = "'"
                        Else
                            SQLDataType = dr.Item("dataType")
                            If SQLDataType = "char" Or SQLDataType = "nvarchar" Or SQLDataType = "varchar" Then
                                SQLDataType = "'"
                            Else
                                SQLDataType = ""
                            End If
                        End If
                        'get the full table name of the first filtered option
                        If IsDBNull(dr.Item("FREDTable")) Then
                            FREDFilter1 = ""
                        Else
                            FREDFilter1 = dr.Item("FREDTable")
                        End If
                        If IsDBNull(dr.Item("FREDConnect")) Then
                            FREDConnect = ""
                        Else
                            FREDConnect = dr.Item("FREDConnect")
                        End If
                    End While
                    dr.Close()
                    If lblFREDTable1.Text = FREDFilter1 Or lblFREDTable2.Text = FREDFilter1 Or lblFREDTable3.Text = FREDFilter1 Then
                        FREDchk = "Base"
                    Else
                        FREDchk = "NO"
                    End If

                    Select Case ddlFREDFilterOption1.SelectedIndex
                        Case 1 '<> 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " <> " & SQLDataType & Replace(txtFREDFilter1.Text, " '", "''") & SQLDataType & " "
                            Else
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " = " & SQLDataType & Replace(txtFREDFilter1.Text, " '", "''") & SQLDataType & " "
                                FREDchk = "Flip"
                            End If
                        Case 6 'like 
                            tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " like " & SQLDataType & "%" & Replace(txtFREDFilter1.Text, "'", "''") & "%" & SQLDataType & ""
                        Case 7 'contains AKA in 
                            If txtFREDFilter1.Text.Contains(",") Then
                                temp = txtFREDFilter1.Text
                                txtFREDFilter1.Text = Replace(txtFREDFilter1.Text, "'", "''")
                                Do While txtFREDFilter1.Text.Contains(",")
                                    If InStr(txtFREDFilter1.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter1.Text, 1, InStr(txtFREDFilter1.Text, ",") - 1) & "',"
                                        txtFREDFilter1.Text = Mid(txtFREDFilter1.Text, InStr(txtFREDFilter1.Text, ",") + 1, Len(txtFREDFilter1.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter1.Text & "'"
                                        txtFREDFilter1.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter1.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter1.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter1.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter1.Text & "'"
                            End If
                            tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " in (" & tempFRED & ") "
                            temp = ""
                        Case 8 'not like 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " not like " & SQLDataType & "%" & Replace(txtFREDFilter1.Text, "'", "''") & "%" & SQLDataType & ""
                            Else
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & "  like " & SQLDataType & "%" & Replace(txtFREDFilter1.Text, "'", "''") & "%" & SQLDataType & ""
                                FREDchk = "Flip"
                            End If
                        Case 9 'not contains AKA not in 
                            If txtFREDFilter1.Text.Contains(",") Then
                                temp = txtFREDFilter1.Text
                                txtFREDFilter1.Text = Replace(txtFREDFilter1.Text, "'", "''")
                                Do While txtFREDFilter1.Text.Contains(",")
                                    If InStr(txtFREDFilter1.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter1.Text, 1, InStr(txtFREDFilter1.Text, ",") - 1) & "',"
                                        txtFREDFilter1.Text = Mid(txtFREDFilter1.Text, InStr(txtFREDFilter1.Text, ",") + 1, Len(txtFREDFilter1.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter1.Text & "'"
                                        txtFREDFilter1.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter1.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter1.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter1.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter1.Text & "'"
                            End If

                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " not in (" & tempFRED & ") "
                            Else
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & "  in (" & tempFRED & ") "
                                FREDchk = "Flip"
                            End If
                            temp = ""
                        Case 10 'between 
                            tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " between " & SQLDataType & Replace(numFREDFilter1Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter1End.Text, "'", "''") & SQLDataType & "  "
                        Case 11 'not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " not between  " & SQLDataType & Replace(numFREDFilter1Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter1End.Text, "'", "''") & SQLDataType & "  "
                            Else
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " between  " & SQLDataType & Replace(numFREDFilter1Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter1End.Text, "'", "''") & SQLDataType & "  "
                                FREDchk = "Flip"
                            End If
                        Case 12 'Date between 
                            tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " between " & SQLDataType & Replace(dtpFREDFilter1StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter1EndDate.Text, "'", "''") & SQLDataType & "  "
                        Case 13 'Date not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " not between " & SQLDataType & Replace(dtpFREDFilter1StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter1EndDate.Text, "'", "''") & SQLDataType & "  "
                            Else
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & "  between " & SQLDataType & Replace(dtpFREDFilter1StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter1EndDate.Text, "'", "''") & SQLDataType & "  "
                                FREDchk = "Flip"
                            End If
                        Case 14 'Null 
                            tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " is NULL "
                        Case 15 'Not Null 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " is NOT NULL "
                            Else
                                tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " is NULL "
                                FREDchk = "Flip"
                            End If
                        Case Else  '=,  <, <=, >, >=
                            tempFRED = "[" & ddlFREDFilter1.SelectedValue & "]" & " " & ddlFREDFilterOption1.Text & " " & SQLDataType & Replace(txtFREDFilter1.Text, "'", "''") & SQLDataType & " "
                    End Select

                    If lblFREDTable1.Text = FREDFilter1 Or lblFREDTable2.Text = FREDFilter1 Or lblFREDTable3.Text = FREDFilter1 Then
                        'if Table is used in the select statement then add to the where clause 
                        FREDFilter1 = FREDFilter1 & "." & tempFRED
                    Else
                        If FREDConnect <> "" Then
                            If FREDchk = "Flip" Then
                                FREDFilter1 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter1 & _
                                " where " & FREDFilter1 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                " and " & FREDFilter1 & "." & tempFRED
                            End If
                            FREDFilter1 = " exists (select distinct " & FREDConnect & " from " & FREDFilter1 & _
                            " where " & FREDFilter1 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                            " and " & FREDFilter1 & "." & tempFRED
                        End If
                    End If
                End If
                tempFRED = ""
                FREDConnect = ""
                SQLDataType = ""
                FREDchk = ""

                If rblFREDFilter2.SelectedIndex <> 0 Then
                    SQL = "select " & _
                    "(TABLE_CATALOG + '.'+ TABLE_SCHEMA + '.' + TABLE_NAME) as FREDTable " & _
                    ",COLUMN_NAME as FREDConnect,   " & _
                    "(select DataType  from WebFD.FRED.vw_AvailableData where TABLE_ID = " & rblFREDFilter2.SelectedValue.ToString & _
                    "  and Column_Alias = '" & ddlFREDFilter2.SelectedValue & "' ) as dataType " & _
                    "from WebFD.FRED.vw_AvailableData " & _
                    "where TABLE_ID  = " & rblFREDFilter2.SelectedValue.ToString & " and FREDConnect = 1 "

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("dataType")) Then
                            SQLDataType = "'"
                        Else
                            SQLDataType = dr.Item("dataType")
                            If SQLDataType = "char" Or SQLDataType = "nvarchar" Or SQLDataType = "varchar" Then
                                SQLDataType = "'"
                            Else
                                SQLDataType = ""
                            End If
                        End If
                        'get the full table name of the first filtered option
                        If IsDBNull(dr.Item("FREDTable")) Then
                            FREDFilter2 = ""
                        Else
                            FREDFilter2 = dr.Item("FREDTable")
                        End If
                        If IsDBNull(dr.Item("FREDConnect")) Then
                            FREDConnect = ""
                        Else
                            FREDConnect = dr.Item("FREDConnect")
                        End If
                    End While
                    dr.Close()
                    If lblFREDTable1.Text = FREDFilter2 Or lblFREDTable2.Text = FREDFilter2 Or lblFREDTable3.Text = FREDFilter2 Then
                        FREDchk = "Base"
                    Else
                        FREDchk = "NO"
                    End If

                    Select Case ddlFREDFilterOption2.SelectedIndex
                        Case 1 '<> 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " <> " & SQLDataType & Replace(txtFREDFilter2.Text, " '", "''") & SQLDataType & " "
                            Else
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " = " & SQLDataType & Replace(txtFREDFilter2.Text, " '", "''") & SQLDataType & " "
                                FREDchk = "Flip"
                            End If
                        Case 6 'like 
                            tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " like " & SQLDataType & "%" & Replace(txtFREDFilter2.Text, "'", "''") & "%" & SQLDataType & ""
                        Case 7 'contains AKA in 
                            If txtFREDFilter2.Text.Contains(",") Then
                                temp = txtFREDFilter2.Text
                                txtFREDFilter2.Text = Replace(txtFREDFilter2.Text, "'", "''")
                                Do While txtFREDFilter2.Text.Contains(",")
                                    If InStr(txtFREDFilter2.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter2.Text, 1, InStr(txtFREDFilter2.Text, ",") - 1) & "',"
                                        txtFREDFilter2.Text = Mid(txtFREDFilter2.Text, InStr(txtFREDFilter2.Text, ",") + 1, Len(txtFREDFilter2.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter2.Text & "'"
                                        txtFREDFilter2.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter2.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter2.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter2.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter2.Text & "'"
                            End If
                            tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " in (" & tempFRED & ") "
                            temp = ""
                        Case 8 'not like 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " not like " & SQLDataType & "%" & Replace(txtFREDFilter2.Text, "'", "''") & "%" & SQLDataType & ""
                            Else
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & "  like " & SQLDataType & "%" & Replace(txtFREDFilter2.Text, "'", "''") & "%" & SQLDataType & ""
                                FREDchk = "Flip"
                            End If

                        Case 9 'not contains AKA not in 
                            If txtFREDFilter2.Text.Contains(",") Then
                                temp = txtFREDFilter2.Text
                                txtFREDFilter2.Text = Replace(txtFREDFilter2.Text, "'", "''")
                                Do While txtFREDFilter2.Text.Contains(",")
                                    If InStr(txtFREDFilter2.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter2.Text, 1, InStr(txtFREDFilter2.Text, ",") - 1) & "',"
                                        txtFREDFilter2.Text = Mid(txtFREDFilter2.Text, InStr(txtFREDFilter2.Text, ",") + 1, Len(txtFREDFilter2.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter2.Text & "'"
                                        txtFREDFilter2.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter2.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter2.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter2.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter2.Text & "'"
                            End If
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " not in (" & tempFRED & ") "
                            Else
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & "  in (" & tempFRED & ") "
                                FREDchk = "Flip"
                            End If
                            temp = ""
                        Case 10 'between 
                            tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " between " & SQLDataType & Replace(numFREDFilter2Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter2End.Text, "'", "''") & SQLDataType & "  "
                        Case 11 'not between
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " not between  " & SQLDataType & Replace(numFREDFilter2Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter2End.Text, "'", "''") & SQLDataType & "  "
                            Else
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " not between  " & SQLDataType & Replace(numFREDFilter2Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter2End.Text, "'", "''") & SQLDataType & "  "
                                FREDchk = "Flip"
                            End If
                        Case 12 'Date between 
                            tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " between " & SQLDataType & Replace(dtpFREDFilter2StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter2EndDate.Text, "'", "''") & SQLDataType & "  "
                        Case 13 'Date not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " not between " & SQLDataType & Replace(dtpFREDFilter2StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter2EndDate.Text, "'", "''") & SQLDataType & "  "
                            Else
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " not between " & SQLDataType & Replace(dtpFREDFilter2StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter2EndDate.Text, "'", "''") & SQLDataType & "  "
                                FREDchk = "Flip"
                            End If
                        Case 14 'Null 
                            tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " is NULL "
                        Case 15 'Not Null 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " is NOT NULL "
                            Else
                                tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " is NULL "
                                FREDchk = "Flip"
                            End If
                        Case Else  '=,  <, <=, >, >=
                            tempFRED = "[" & ddlFREDFilter2.SelectedValue & "]" & " " & ddlFREDFilterOption2.Text & " " & SQLDataType & Replace(txtFREDFilter2.Text, "'", "''") & SQLDataType & " "
                    End Select

                    If lblFREDTable1.Text = FREDFilter2 Or lblFREDTable2.Text = FREDFilter2 Or lblFREDTable3.Text = FREDFilter2 Then
                        'if Table is used in the select statement then add to the where clause 
                        FREDFilter2 = FREDFilter2 & "." & tempFRED
                    Else
                        'if an exists statement includes filter option add to that statment
                        If FREDFilter1.Contains(FREDFilter2) Then
                            If FREDchk = "Flip" And FREDFilter1.StartsWith(" exists (") Then
                                FREDFilter2 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter2 & _
                                 " where " & FREDFilter2 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                 " and " & FREDFilter2 & "." & tempFRED
                            Else
                                FREDFilter1 = FREDFilter1 & " and " & FREDFilter2 & "." & tempFRED
                                FREDFilter2 = ""
                            End If
                        Else
                            'if table is not used in the select statment or existing statemnt then start an exists statement 
                            If FREDConnect <> "" Then
                                If FREDchk = "Flip" Then
                                    FREDFilter2 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter2 & _
                                    " where " & FREDFilter2 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                    " and " & FREDFilter2 & "." & tempFRED
                                Else
                                    FREDFilter2 = " exists (select distinct " & FREDConnect & " from " & FREDFilter2 & _
                                      " where " & FREDFilter2 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                      " and " & FREDFilter2 & "." & tempFRED
                                End If
                            End If
                        End If
                    End If
                  
                End If
                tempFRED = ""
                FREDConnect = ""
                SQLDataType = ""
                FREDchk = ""

                If rblFREDFilter3.SelectedIndex <> 0 Then
                    SQL = "select " & _
                  "(TABLE_CATALOG + '.'+ TABLE_SCHEMA + '.' + TABLE_NAME) as FREDTable " & _
                  ",COLUMN_NAME as FREDConnect,   " & _
                  "(select DataType  from WebFD.FRED.vw_AvailableData where TABLE_ID = " & rblFREDFilter3.SelectedValue.ToString & _
                  "  and Column_Alias = '" & ddlFREDFilter3.SelectedValue & "' ) as dataType " & _
                  "from WebFD.FRED.vw_AvailableData " & _
                  "where TABLE_ID  = " & rblFREDFilter3.SelectedValue.ToString & " and FREDConnect = 1 "

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("dataType")) Then
                            SQLDataType = "'"
                        Else
                            SQLDataType = dr.Item("dataType")
                            If SQLDataType = "char" Or SQLDataType = "nvarchar" Or SQLDataType = "varchar" Then
                                SQLDataType = "'"
                            Else
                                SQLDataType = ""
                            End If
                        End If
                        'get the full table name of the first filtered option
                        If IsDBNull(dr.Item("FREDTable")) Then
                            FREDFilter3 = ""
                        Else
                            FREDFilter3 = dr.Item("FREDTable")
                        End If
                        If IsDBNull(dr.Item("FREDConnect")) Then
                            FREDConnect = ""
                        Else
                            FREDConnect = dr.Item("FREDConnect")
                        End If
                    End While
                    dr.Close()
                    If lblFREDTable1.Text = FREDFilter3 Or lblFREDTable2.Text = FREDFilter3 Or lblFREDTable3.Text = FREDFilter3 Then
                        FREDchk = "Base"
                    Else
                        FREDchk = "NO"
                    End If

                    Select Case ddlFREDFilterOption3.SelectedIndex
                        Case 1 '<> 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " <> " & SQLDataType & Replace(txtFREDFilter3.Text, " '", "''") & SQLDataType & " "
                            Else
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " = " & SQLDataType & Replace(txtFREDFilter3.Text, " '", "''") & SQLDataType & " "
                                FREDchk = "Flip"
                            End If
                        Case 6 'like 
                            tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " like " & SQLDataType & "%" & Replace(txtFREDFilter3.Text, "'", "''") & "%" & SQLDataType & ""
                        Case 7 'contains AKA in 
                            If txtFREDFilter3.Text.Contains(",") Then
                                temp = txtFREDFilter3.Text
                                txtFREDFilter3.Text = Replace(txtFREDFilter3.Text, "'", "''")
                                Do While txtFREDFilter3.Text.Contains(",")
                                    If InStr(txtFREDFilter3.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter3.Text, 1, InStr(txtFREDFilter3.Text, ",") - 1) & "',"
                                        txtFREDFilter3.Text = Mid(txtFREDFilter3.Text, InStr(txtFREDFilter3.Text, ",") + 1, Len(txtFREDFilter3.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter3.Text & "'"
                                        txtFREDFilter3.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter3.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter3.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter3.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter3.Text & "'"
                            End If
                            tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " in (" & tempFRED & ") "
                            temp = ""
                        Case 8 'not like 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " not like " & SQLDataType & "%" & Replace(txtFREDFilter3.Text, "'", "''") & "%" & SQLDataType & ""
                            Else
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " like " & SQLDataType & "%" & Replace(txtFREDFilter3.Text, "'", "''") & "%" & SQLDataType & ""
                                FREDchk = "Flip"
                            End If

                        Case 9 'not contains AKA not in 
                            If txtFREDFilter3.Text.Contains(",") Then
                                temp = txtFREDFilter3.Text
                                txtFREDFilter3.Text = Replace(txtFREDFilter3.Text, "'", "''")
                                Do While txtFREDFilter3.Text.Contains(",")
                                    If InStr(txtFREDFilter3.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter3.Text, 1, InStr(txtFREDFilter3.Text, ",") - 1) & "',"
                                        txtFREDFilter3.Text = Mid(txtFREDFilter3.Text, InStr(txtFREDFilter3.Text, ",") + 1, Len(txtFREDFilter3.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter3.Text & "'"
                                        txtFREDFilter3.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter3.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter3.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter3.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter3.Text & "'"
                            End If
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " not in (" & tempFRED & ") "
                            Else
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " in (" & tempFRED & ") "
                                FREDchk = "Flip"
                            End If
                            temp = ""
                        Case 10 'between 
                            tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " between " & SQLDataType & Replace(numFREDFilter3Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter3End.Text, "'", "''") & SQLDataType & "  "
                        Case 11 'not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " not between  " & SQLDataType & Replace(numFREDFilter3Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter3End.Text, "'", "''") & SQLDataType & "  "
                            Else
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " between  " & SQLDataType & Replace(numFREDFilter3Start.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(numFREDFilter3End.Text, "'", "''") & SQLDataType & "  "
                                FREDchk = "Flip"
                            End If
                        Case 12 'Date between 
                            tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " between " & SQLDataType & Replace(dtpFREDFilter3StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter3EndDate.Text, "'", "''") & SQLDataType & "  "
                        Case 13 'Date not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " not between " & SQLDataType & Replace(dtpFREDFilter3StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter3EndDate.Text, "'", "''") & SQLDataType & "  "
                            Else
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & "  between " & SQLDataType & Replace(dtpFREDFilter3StartDate.Text, "'", "''") & SQLDataType & " and " & SQLDataType & Replace(dtpFREDFilter3EndDate.Text, "'", "''") & SQLDataType & "  "
                                FREDchk = "Flip"
                            End If

                        Case 14 'Null 
                            tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " is NULL "
                        Case 15 'Not Null 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " is NOT NULL "
                            Else
                                tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " is  NULL "
                                FREDchk = "Flip"
                            End If

                        Case Else  '=,  <, <=, >, >=
                            tempFRED = "[" & ddlFREDFilter3.SelectedValue & "]" & " " & ddlFREDFilterOption3.Text & " " & SQLDataType & Replace(txtFREDFilter3.Text, "'", "''") & SQLDataType & " "
                    End Select

                    If lblFREDTable1.Text = FREDFilter3 Or lblFREDTable2.Text = FREDFilter3 Or lblFREDTable3.Text = FREDFilter3 Then
                        'if Table is used in the select statement then add to the where clause 
                        FREDFilter3 = FREDFilter3 & "." & tempFRED
                    Else
                        If FREDFilter1.Contains(FREDFilter3) Or FREDFilter2.Contains(FREDFilter3) Then
                            'if an exists statement includes filter option add to that statment
                            If FREDFilter1.Contains(FREDFilter3) Then
                                If FREDchk = "Flip" And FREDFilter1.StartsWith(" exists (") Then
                                    FREDFilter3 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter3 & _
                                     " where " & FREDFilter3 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                     " and " & FREDFilter3 & "." & tempFRED
                                Else
                                    FREDFilter1 = FREDFilter1 & " and " & FREDFilter3 & "." & tempFRED
                                End If
                            End If
                            If FREDFilter2.Contains(FREDFilter3) Then
                                If FREDchk = "Flip" And FREDFilter2.StartsWith(" exists (") Then
                                    FREDFilter3 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter3 & _
                                    " where " & FREDFilter3 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                    " and " & FREDFilter3 & "." & tempFRED
                                Else
                                    FREDFilter2 = FREDFilter2 & " and " & FREDFilter3 & "." & tempFRED
                                End If
                            End If
                            FREDFilter3 = ""
                        Else
                            'if table is not used in the select statment or existing statemnt then start an exists statement 
                            If FREDConnect <> "" Then
                                If FREDchk = "Flip" Then
                                    FREDFilter3 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter3 & _
                                     " where " & FREDFilter3 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                     " and " & FREDFilter3 & "." & tempFRED
                                Else
                                    FREDFilter3 = " exists (select distinct " & FREDConnect & " from " & FREDFilter3 & _
                                     " where " & FREDFilter3 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                     " and " & FREDFilter3 & "." & tempFRED
                                End If
                            End If
                        End If
                    End If
                End If
                tempFRED = ""
                FREDConnect = ""
                SQLDataType = ""
                FREDchk = ""

                If rblFREDFilter4.SelectedIndex <> 0 Then
                    SQL = "select " & _
                    "(TABLE_CATALOG + '.'+ TABLE_SCHEMA + '.' + TABLE_NAME) as FREDTable " & _
                    ",COLUMN_NAME as FREDConnect,   " & _
                    "(select DataType  from WebFD.FRED.vw_AvailableData where TABLE_ID = " & rblFREDFilter4.SelectedValue.ToString & _
                    "  and Column_Alias = '" & ddlFREDFilter4.SelectedValue & "' ) as dataType " & _
                    "from WebFD.FRED.vw_AvailableData " & _
                    "where TABLE_ID  = " & rblFREDFilter4.SelectedValue.ToString & " and FREDConnect = 1 "

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("dataType")) Then
                            SQLDataType = "'"
                        Else
                            SQLDataType = dr.Item("dataType")
                            If SQLDataType = "char" Or SQLDataType = "nvarchar" Or SQLDataType = "varchar" Then
                                SQLDataType = "'"
                            Else
                                SQLDataType = ""
                            End If
                        End If
                        'get the full table name of the first filtered option
                        If IsDBNull(dr.Item("FREDTable")) Then
                            FREDFilter4 = ""
                        Else
                            FREDFilter4 = dr.Item("FREDTable")
                        End If
                        If IsDBNull(dr.Item("FREDConnect")) Then
                            FREDConnect = ""
                        Else
                            FREDConnect = dr.Item("FREDConnect")
                        End If
                    End While
                    dr.Close()
                    If lblFREDTable1.Text = FREDFilter4 Or lblFREDTable2.Text = FREDFilter4 Or lblFREDTable3.Text = FREDFilter4 Then
                        FREDchk = "Base"
                    Else
                        FREDchk = "NO"
                    End If

                    Select Case ddlFREDFilterOption4.SelectedIndex
                        Case 1 '<> 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " <> '" & Replace(txtFREDFilter4.Text, "'", "''") & "' "
                            Else
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " = '" & Replace(txtFREDFilter4.Text, "'", "''") & "' "
                                FREDchk = "Flip"
                            End If
                        Case 6 'like 
                            tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " like '%" & Replace(txtFREDFilter4.Text, "'", "''") & "%' "
                        Case 7 'contains AKA in 
                            If txtFREDFilter4.Text.Contains(",") Then
                                temp = txtFREDFilter4.Text
                                txtFREDFilter4.Text = Replace(txtFREDFilter4.Text, "'", "''")
                                Do While txtFREDFilter4.Text.Contains(",")
                                    If InStr(txtFREDFilter4.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter4.Text, 1, InStr(txtFREDFilter4.Text, ",") - 1) & "',"
                                        txtFREDFilter4.Text = Mid(txtFREDFilter4.Text, InStr(txtFREDFilter4.Text, ",") + 1, Len(txtFREDFilter4.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter4.Text & "'"
                                        txtFREDFilter4.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter4.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter4.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter4.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter4.Text & "'"
                            End If
                            tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " in (" & tempFRED & ") "
                            temp = ""
                        Case 8 'not like 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " not like '%" & Replace(txtFREDFilter4.Text, "'", "''") & "%' "
                            Else
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " like '%" & Replace(txtFREDFilter4.Text, "'", "''") & "%' "
                                FREDchk = "Flip"
                            End If

                        Case 9 'not contains AKA not in 
                            If txtFREDFilter4.Text.Contains(",") Then
                                temp = txtFREDFilter4.Text
                                txtFREDFilter4.Text = Replace(txtFREDFilter4.Text, "'", "''")
                                Do While txtFREDFilter4.Text.Contains(",")
                                    If InStr(txtFREDFilter4.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter4.Text, 1, InStr(txtFREDFilter4.Text, ",") - 1) & "',"
                                        txtFREDFilter4.Text = Mid(txtFREDFilter4.Text, InStr(txtFREDFilter4.Text, ",") + 1, Len(txtFREDFilter4.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter4.Text & "'"
                                        txtFREDFilter4.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter4.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter4.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter4.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter4.Text & "'"
                            End If
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " not in (" & tempFRED & ") "
                            Else
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & "  in (" & tempFRED & ") "
                                FREDchk = "Flip"
                            End If
                            temp = ""
                        Case 10 'between 
                            tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " between '" & Replace(numFREDFilter4Start.Text, "'", "''") & "' and '" & Replace(numFREDFilter4End.Text, "'", "''") & "' "
                        Case 11 'not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " not between '" & Replace(numFREDFilter4Start.Text, "'", "''") & "' and '" & Replace(numFREDFilter4End.Text, "'", "''") & "' "
                            Else
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " between '" & Replace(numFREDFilter4Start.Text, "'", "''") & "' and '" & Replace(numFREDFilter4End.Text, "'", "''") & "' "
                                FREDchk = "Flip"
                            End If

                        Case 12 'Date between 
                            tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " between '" & Replace(dtpFREDFilter4StartDate.Text, "'", "''") & "' and '" & Replace(dtpFREDFilter4EndDate.Text, "'", "''") & "' "
                        Case 13 'Date not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " not between '" & Replace(dtpFREDFilter4StartDate.Text, "'", "''") & "' and '" & Replace(dtpFREDFilter4EndDate.Text, "'", "''") & "' "
                            Else
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " between '" & Replace(dtpFREDFilter4StartDate.Text, "'", "''") & "' and '" & Replace(dtpFREDFilter4EndDate.Text, "'", "''") & "' "
                                FREDchk = "Flip"
                            End If

                        Case 14 'Null 
                            tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " is NULL "
                        Case 15 'Not Null 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " is NOT NULL  "
                            Else
                                tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " is NULL  "
                                FREDchk = "Flip"
                            End If
                        Case Else  '=,  <, <=, >, >=
                            tempFRED = "[" & ddlFREDFilter4.SelectedValue & "]" & " " & ddlFREDFilterOption4.Text & " '" & Replace(txtFREDFilter4.Text, "'", "''") & "' "
                    End Select

                    If lblFREDTable1.Text = FREDFilter4 Or lblFREDTable2.Text = FREDFilter4 Or lblFREDTable3.Text = FREDFilter4 Then
                        'if Table is used in the select statement then add to the where clause 
                        FREDFilter4 = FREDFilter4 & "." & tempFRED
                    Else
                        If FREDFilter1.Contains(FREDFilter4) = True Or FREDFilter2.Contains(FREDFilter4) = True Or FREDFilter3.Contains(FREDFilter4) = True Then
                            'if an exists statement includes filter option add to that statment
                            If FREDFilter1.Contains(FREDFilter4) Then
                                If FREDchk = "Flip" And FREDFilter1.StartsWith(" exists (") Then
                                    FREDFilter4 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter4 & _
                                    " where " & FREDFilter4 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                    " and " & FREDFilter4 & "." & tempFRED
                                Else
                                    FREDFilter1 = FREDFilter1 & " and " & FREDFilter4 & "." & tempFRED
                                End If
                            End If
                            If FREDFilter2.Contains(FREDFilter4) Then
                                If FREDchk = "Flip" And FREDFilter2.StartsWith(" exists (") Then
                                    FREDFilter4 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter4 & _
                                   " where " & FREDFilter4 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                   " and " & FREDFilter4 & "." & tempFRED
                                Else
                                    FREDFilter2 = FREDFilter2 & " and " & FREDFilter4 & "." & tempFRED
                                End If
                            End If
                            If FREDFilter3.Contains(FREDFilter4) Then
                                If FREDchk = "Flip" And FREDFilter3.StartsWith(" exists (") Then
                                    FREDFilter4 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter4 & _
                                   " where " & FREDFilter4 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                   " and " & FREDFilter4 & "." & tempFRED
                                Else
                                    FREDFilter3 = FREDFilter3 & " and " & FREDFilter4 & "." & tempFRED
                                End If
                            End If
                            FREDFilter4 = ""
                        Else
                            'if table is not used in the select statment or existing statemnt then start an exists statement 
                            If FREDConnect <> "" Then
                                If FREDchk = "Flip" Then
                                    FREDFilter4 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter4 & _
                                   " where " & FREDFilter4 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                   " and " & FREDFilter4 & "." & tempFRED
                                Else
                                    FREDFilter4 = " exists (select distinct " & FREDConnect & " from " & FREDFilter4 & _
                                        " where " & FREDFilter4 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                        " and " & FREDFilter4 & "." & tempFRED
                                End If
                            End If
                        End If
                    End If
                    
                End If
                tempFRED = ""
                FREDConnect = ""
                SQLDataType = ""
                FREDchk = ""

                If rblFREDFilter5.SelectedIndex <> 0 Then
                    SQL = "select " & _
                   "(TABLE_CATALOG + '.'+ TABLE_SCHEMA + '.' + TABLE_NAME) as FREDTable " & _
                   ",COLUMN_NAME as FREDConnect,   " & _
                   "(select DataType  from WebFD.FRED.vw_AvailableData where TABLE_ID = " & rblFREDFilter5.SelectedValue.ToString & _
                   "  and Column_Alias = '" & ddlFREDFilter5.SelectedValue & "' ) as dataType " & _
                   "from WebFD.FRED.vw_AvailableData " & _
                   "where TABLE_ID  = " & rblFREDFilter5.SelectedValue.ToString & " and FREDConnect = 1 "

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr.Item("dataType")) Then
                            SQLDataType = "'"
                        Else
                            SQLDataType = dr.Item("dataType")
                            If SQLDataType = "char" Or SQLDataType = "nvarchar" Or SQLDataType = "varchar" Then
                                SQLDataType = "'"
                            Else
                                SQLDataType = ""
                            End If
                        End If
                        'get the full table name of the first filtered option
                        If IsDBNull(dr.Item("FREDTable")) Then
                            FREDFilter5 = ""
                        Else
                            FREDFilter5 = dr.Item("FREDTable")
                        End If
                        If IsDBNull(dr.Item("FREDConnect")) Then
                            FREDConnect = ""
                        Else
                            FREDConnect = dr.Item("FREDConnect")
                        End If
                    End While
                    dr.Close()
                    If lblFREDTable1.Text = FREDFilter5 Or lblFREDTable2.Text = FREDFilter5 Or lblFREDTable3.Text = FREDFilter5 Then
                        FREDchk = "Base"
                    Else
                        FREDchk = "NO"
                    End If

                    Select Case ddlFREDFilterOption5.SelectedIndex
                        Case 1 '<> 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " <> '" & Replace(txtFREDFilter5.Text, "'", "''") & "' "
                            Else
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " = '" & Replace(txtFREDFilter5.Text, "'", "''") & "' "
                                FREDchk = "Flip"
                            End If
                        Case 6 'like 
                            tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " like '%" & Replace(txtFREDFilter5.Text, "'", "''") & "%' "
                        Case 7 'contains AKA in 
                            If txtFREDFilter5.Text.Contains(",") Then
                                temp = txtFREDFilter5.Text
                                txtFREDFilter5.Text = Replace(txtFREDFilter5.Text, "'", "''")
                                Do While txtFREDFilter5.Text.Contains(",")
                                    If InStr(txtFREDFilter5.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter5.Text, 1, InStr(txtFREDFilter5.Text, ",") - 1) & "',"
                                        txtFREDFilter5.Text = Mid(txtFREDFilter5.Text, InStr(txtFREDFilter5.Text, ",") + 1, Len(txtFREDFilter5.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter5.Text & "'"
                                        txtFREDFilter5.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter5.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter5.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter5.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter5.Text & "'"
                            End If
                            tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " in (" & tempFRED & ") "
                            temp = ""
                        Case 8 'not like 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " not like '%" & Replace(txtFREDFilter5.Text, "'", "''") & "%' "
                            Else
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " like '%" & Replace(txtFREDFilter5.Text, "'", "''") & "%' "
                                FREDchk = "Flip"
                            End If
                        Case 9 'not contains AKA not in 
                            If txtFREDFilter5.Text.Contains(",") Then
                                temp = txtFREDFilter5.Text
                                txtFREDFilter5.Text = Replace(txtFREDFilter5.Text, "'", "''")
                                Do While txtFREDFilter5.Text.Contains(",")
                                    If InStr(txtFREDFilter5.Text, ",") > 0 Then
                                        tempFRED = tempFRED & "'" & Mid(txtFREDFilter5.Text, 1, InStr(txtFREDFilter5.Text, ",") - 1) & "',"
                                        txtFREDFilter5.Text = Mid(txtFREDFilter5.Text, InStr(txtFREDFilter5.Text, ",") + 1, Len(txtFREDFilter5.Text))
                                    Else
                                        tempFRED = tempFRED & "'" & txtFREDFilter5.Text & "'"
                                        txtFREDFilter5.Text = ""
                                    End If
                                Loop
                                If Len(txtFREDFilter5.Text) > 0 Then
                                    tempFRED = tempFRED & "'" & txtFREDFilter5.Text & "'"
                                End If
                                If tempFRED.EndsWith(",") Then
                                    tempFRED = Mid(tempFRED, 1, Len(tempFRED) - 1)
                                End If
                                txtFREDFilter5.Text = temp
                            Else
                                tempFRED = "'" & txtFREDFilter5.Text & "'"
                            End If
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " not in (" & tempFRED & ") "
                            Else
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " in (" & tempFRED & ") "
                                FREDchk = "Flip"
                            End If

                            temp = ""
                        Case 10 'between 
                            tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " between '" & Replace(numFREDFilter5Start.Text, "'", "''") & "' and '" & Replace(numFREDFilter5End.Text, "'", "''") & "' "
                        Case 11 'not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " not between '" & Replace(numFREDFilter5Start.Text, "'", "''") & "' and '" & Replace(numFREDFilter5End.Text, "'", "''") & "' "
                            Else
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " between '" & Replace(numFREDFilter5Start.Text, "'", "''") & "' and '" & Replace(numFREDFilter5End.Text, "'", "''") & "' "
                                FREDchk = "Flip"
                            End If
                        Case 12 'Date between 
                            tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " between '" & Replace(dtpFREDFilter5StartDate.Text, "'", "''") & "' and '" & Replace(dtpFREDFilter5EndDate.Text, "'", "''") & "' "
                        Case 13 'Date not between 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " not between '" & Replace(dtpFREDFilter5StartDate.Text, "'", "''") & " and '" & Replace(dtpFREDFilter5EndDate.Text, "'", "''") & "' "
                            Else
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " between '" & Replace(dtpFREDFilter5StartDate.Text, "'", "''") & " and '" & Replace(dtpFREDFilter5EndDate.Text, "'", "''") & "' "
                                FREDchk = "Flip"
                            End If

                        Case 14 'Null 
                            tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " is NULL "
                        Case 15 'Not Null 
                            If FREDchk = "Base" Then
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " is NOT NULL  "
                            Else
                                tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " is NULL  "
                                FREDchk = "Flip"
                            End If
                        Case Else  '=, <, <=, >, >=
                            tempFRED = "[" & ddlFREDFilter5.SelectedValue & "]" & " " & ddlFREDFilterOption5.Text & " '" & Replace(txtFREDFilter5.Text, "'", "''") & "' "
                    End Select

                    If lblFREDTable1.Text = FREDFilter5 Or lblFREDTable2.Text = FREDFilter5 Or lblFREDTable3.Text = FREDFilter5 Then
                        'if Table is used in the select statement then add to the where clause 
                        FREDFilter5 = FREDFilter5 & "." & tempFRED
                    Else
                        If FREDFilter1.Contains(FREDFilter5) = True Or FREDFilter2.Contains(FREDFilter5) = True Or FREDFilter3.Contains(FREDFilter5) = True _
                              Or FREDFilter4.Contains(FREDFilter5) = True Then
                            'if an exists statement includes filter option add to that statment
                            If FREDFilter1.Contains(FREDFilter5) Then
                                If FREDchk = "Flip" And FREDFilter1.StartsWith(" exists (") Then
                                    FREDFilter5 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter5 & _
                                   " where " & FREDFilter5 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                   " and " & FREDFilter5 & "." & tempFRED
                                Else
                                    FREDFilter1 = FREDFilter1 & " and " & FREDFilter5 & "." & tempFRED
                                End If
                            End If
                            If FREDFilter2.Contains(FREDFilter5) Then
                                If FREDchk = "Flip" And FREDFilter2.StartsWith(" exists (") Then
                                    FREDFilter5 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter5 & _
                                   " where " & FREDFilter5 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                   " and " & FREDFilter5 & "." & tempFRED
                                Else
                                    FREDFilter2 = FREDFilter2 & " and " & FREDFilter5 & "." & tempFRED
                                End If
                            End If
                            If FREDFilter3.Contains(FREDFilter5) Then
                                If FREDchk = "Flip" And FREDFilter3.StartsWith(" exists (") Then
                                    FREDFilter5 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter5 & _
                                   " where " & FREDFilter5 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                   " and " & FREDFilter5 & "." & tempFRED
                                Else
                                    FREDFilter3 = FREDFilter3 & " and " & FREDFilter5 & "." & tempFRED
                                End If
                            End If
                            If FREDFilter4.Contains(FREDFilter5) Then
                                If FREDchk = "Flip" And FREDFilter4.StartsWith(" exists (") Then
                                    FREDFilter5 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter5 & _
                                     " where " & FREDFilter5 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                     " and " & FREDFilter5 & "." & tempFRED
                                Else
                                    FREDFilter4 = FREDFilter4 & " and " & FREDFilter5 & "." & tempFRED
                                End If
                            End If
                            FREDFilter5 = ""
                        Else
                            'if table is not used in the select statment or existing statemnt then start an exists statement 
                            If FREDConnect <> "" Then
                                If FREDchk = "Flip" Then
                                    FREDFilter5 = " not exists (select distinct " & FREDConnect & " from " & FREDFilter5 & _
                                     " where " & FREDFilter5 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                     " and " & FREDFilter5 & "." & tempFRED
                                Else
                                    FREDFilter5 = " exists (select distinct " & FREDConnect & " from " & FREDFilter5 & _
                                    " where " & FREDFilter5 & "." & FREDConnect & " = " & lblFREDTable1.Text & "." & lblFREDKey1.Text & _
                                    " and " & FREDFilter5 & "." & tempFRED
                                End If
                            End If
                        End If
                    End If
                  
                End If
            End Using

            SQLWhere = " where 1 = 1 "

            If FREDFilter1 <> "" Then
                SQLWhere = SQLWhere & " and " & FREDFilter1
                If FREDFilter1.Contains("exists (") Then
                    SQLWhere = SQLWhere & ") "
                End If
            End If
            If FREDFilter2 <> "" Then
                SQLWhere = SQLWhere & " and " & FREDFilter2
                If FREDFilter2.Contains("exists (") Then
                    SQLWhere = SQLWhere & ") "
                End If
            End If
            If FREDFilter3 <> "" Then
                SQLWhere = SQLWhere & " and " & FREDFilter3
                If FREDFilter3.Contains("exists (") Then
                    SQLWhere = SQLWhere & ") "
                End If
            End If
            If FREDFilter4 <> "" Then
                SQLWhere = SQLWhere & " and " & FREDFilter4
                If FREDFilter4.Contains("exists (") Then
                    SQLWhere = SQLWhere & ") "
                End If
            End If
            If FREDFilter5 <> "" Then
                SQLWhere = SQLWhere & " and " & FREDFilter5
                If FREDFilter5.Contains("exists (") Then
                    SQLWhere = SQLWhere & ") "
                End If
            End If

            SQLFRED = SQLSelect & SQLTable & SQLWhere

            'SQL = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = ' SET NOCOUNT ON select " & Replace(SQLFRED, "'", "'+CHAR(39)+'") & "' 	 EXECUTE	SP_EXECUTESQL @FinalQuery   "
            'SQL2 = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = 'SET NOCOUNT ON select count(*)  " & Replace(SQLTable, "'", "'+CHAR(39)+'") & _
            '    Replace(SQLWhere, "'", "'+char(39)+'") & "' 	 EXECUTE	SP_EXECUTESQL @FinalQuery   "
            'SQL3 = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = 'SET NOCOUNT ON select " & Replace(SQLFRED, "'", "'+CHAR(39)+'") & "' 	 EXECUTE	SP_EXECUTESQL @FinalQuery   "


            'SQL = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = ' select top 25 " & Replace(SQLFRED, "'", "'+CHAR(39)+'") & "' 	 EXECUTE (@FinalQuery)  "
            'SQL2 = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = ' select count(*)  " & Replace(SQLTable, "'", "'+CHAR(39)+'") & _
            '    Replace(SQLWhere, "'", "'+char(39)+'") & "' 	 EXECUTE  (@FinalQuery)   "
            'SQL3 = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = ' select " & Replace(SQLFRED, "'", "'+CHAR(39)+'") & "' 	 EXECUTE (@FinalQuery)   "

            'FREDTemp = Replace(Replace(Replace(FREDTemp, "[varchar]", "[varchar](max)"), "[nvarchar]", "[nvarchar](max)"), "[char]", "[char](max)")
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                FREDTemp = "If Object_ID('WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') is not null " & _
              " begin " & _
                 "Drop table WebFD.FredTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
              " end; "  

                FREDTemp = FREDTemp & _
                 "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = ' SET NOCOUNT ON select " & SQLSelect & _
                    " into WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                       SQLTable & Replace(SQLWhere, "'", "'+CHAR(39)+'") & " '	 EXECUTE	SP_EXECUTESQL @FinalQuery   "
                cmd = New SqlCommand(FREDTemp, conn)
                cmd.CommandTimeout = 86400
                cmd.ExecuteNonQuery()
            End Using


            SQL = "select top 25 * from WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")
            SQL2 = "Select count(*) from WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")
            SQL3 = "select  * from WebFD.FREDTemp." & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")

            'SQL = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = ' SET NOCOUNT ON select into FREDTemp. " & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
            '            Replace(SQLFRED, "'", "'+CHAR(39)+'") & "' 	 EXECUTE	SP_EXECUTESQL @FinalQuery   "
            'SQL2 = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = 'SET NOCOUNT ON select count(*)  " & Replace(SQLTable, "'", "'+CHAR(39)+'") & _
            '    Replace(SQLWhere, "'", "'+char(39)+'") & "' 	 EXECUTE	SP_EXECUTESQL @FinalQuery   "
            'SQL3 = "DECLARE @FinalQuery NVARCHAR(MAX) SET @FinalQuery = 'SET NOCOUNT ON select " & Replace(SQLFRED, "'", "'+CHAR(39)+'") & "' 	 EXECUTE	SP_EXECUTESQL @FinalQuery   "


            lblSQLStatement.Text = SQL3.ToString
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rblFREDFilter1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFREDFilter1.SelectedIndexChanged
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Costing As String = ""

            If lblCosting.Text = "" Then
                Costing = "0"
            Else
                Costing = "0,1"
            End If

            ddlFREDFilter1.Visible = True
            pnltxtFREDFilter1.Visible = False
            pnlnumFREDFilter1.Visible = False
            pnldtpFREDFilter1.Visible = False


            SQL = "select substring(Column_Alias,1,30) Column_Alias, COLUMN_Name  " & _
            "from WebFD.FRED.vw_AvailableData " & _
            "where TABLE_ID = " & rblFREDFilter1.SelectedValue & _
            "and Costing in (" & Costing & ") " & _
            "order by 1  "

            ds = New DataSet
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "FRED")

                ddlFREDFilter1.DataSource = ds
                ddlFREDFilter1.DataMember = "FRED"
                ddlFREDFilter1.DataTextField = "Column_Alias"
                ddlFREDFilter1.DataValueField = "COLUMN_Name"
                ddlFREDFilter1.DataBind()

            End Using
            If ddlFREDFilter1.Items.Count < 1 Then
                ddlFREDFilter1.Visible = False
                ddlFREDFilterOption1.Visible = False
            Else
                ddlFREDFilterOption1.Visible = True
                FREDFILTEROPTION1()
            End If

            lblFREEFilter1.Text = "Filter #1 - " & rblFREDFilter1.SelectedItem.Text
            upFREEFILTER1.Update()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilterOption1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilterOption1.SelectedIndexChanged
        Try
            FREDFILTEROPTION1()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDFILTEROPTION1()
        Try
            pnltxtFREDFilter1.Visible = False
            pnlnumFREDFilter1.Visible = False
            pnldtpFREDFilter1.Visible = False
            lblFREDFilter1.Visible = False
            lblFREDFilter1b.Visible = False

            Select Case ddlFREDFilterOption1.Text
                Case "between"
                    pnlnumFREDFilter1.Visible = True
                Case "not between"
                    pnlnumFREDFilter1.Visible = True
                Case "DATE between"
                    pnldtpFREDFilter1.Visible = True
                Case "DATE not between"
                    pnldtpFREDFilter1.Visible = True
                Case "contains"
                    lblFREDFilter1.Visible = True
                    lblFREDFilter1b.Visible = True
                    pnltxtFREDFilter1.Visible = True
                Case "does not contain"
                    lblFREDFilter1.Visible = True
                    lblFREDFilter1b.Visible = True
                    pnltxtFREDFilter1.Visible = True
                Case "NULL"

                Case "NOT NULL"

                Case Else
                    pnltxtFREDFilter1.Visible = True
            End Select
 

        Catch ex As Exception

        End Try
    End Sub
    Private Sub rblFREDFilter2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFREDFilter2.SelectedIndexChanged
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Costing As String = ""

            If lblCosting.Text = "" Then
                Costing = "0"
            Else
                Costing = "0,1"
            End If

            ddlFREDFilter2.Visible = True
            pnltxtFREDFilter2.Visible = False
            pnlnumFREDFilter2.Visible = False
            pnldtpFREDFilter2.Visible = False


            SQL = "select substring(Column_Alias,1,30) Column_Alias, COLUMN_Name  " & _
            "from WebFD.FRED.vw_AvailableData " & _
            "where TABLE_ID = " & rblFREDFilter2.SelectedValue & _
           "and Costing in (" & Costing & ") " & _
            "order by 1  "
            ds = New DataSet
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86200
                da.Fill(ds, "FRED")

                ddlFREDFilter2.DataSource = ds
                ddlFREDFilter2.DataMember = "FRED"
                ddlFREDFilter2.DataTextField = "Column_Alias"
                ddlFREDFilter2.DataValueField = "COLUMN_Name"
                ddlFREDFilter2.DataBind()

            End Using
            If ddlFREDFilter2.Items.Count < 1 Then
                ddlFREDFilter2.Visible = False
                ddlFREDFilterOption2.Visible = False
            Else
                ddlFREDFilterOption2.Visible = True
                FREDFILTEROPTION2()

            End If

            lblFREEFilter2.Text = "Filter #2 - " & rblFREDFilter2.SelectedItem.Text
            upFREEFILTER2.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilterOption2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilterOption2.SelectedIndexChanged
        Try
            FREDFILTEROPTION2()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDFILTEROPTION2()
        Try
            pnltxtFREDFilter2.Visible = False
            pnlnumFREDFilter2.Visible = False
            pnldtpFREDFilter2.Visible = False
            lblFREDFilter2.Visible = False
            lblFREDFilter2b.Visible = False

            Select Case ddlFREDFilterOption2.Text
                Case "between"
                    pnlnumFREDFilter2.Visible = True
                Case "not between"
                    pnlnumFREDFilter2.Visible = True
                Case "DATE between"
                    pnldtpFREDFilter2.Visible = True
                Case "DATE not between"
                    pnldtpFREDFilter2.Visible = True
                Case "contains"
                    lblFREDFilter2.Visible = True
                    lblFREDFilter2b.Visible = True
                    pnltxtFREDFilter2.Visible = True
                Case "does not contain"
                    lblFREDFilter2.Visible = True
                    lblFREDFilter2b.Visible = True
                    pnltxtFREDFilter2.Visible = True
                Case "NULL"

                Case "NOT NULL"

                Case Else
                    pnltxtFREDFilter2.Visible = True
            End Select
 

        Catch ex As Exception

        End Try
    End Sub
    Private Sub rblFREDFilter3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFREDFilter3.SelectedIndexChanged
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Costing As String = ""

            If lblCosting.Text = "" Then
                Costing = "0"
            Else
                Costing = "0,1"
            End If

            ddlFREDFilter3.Visible = True
            pnltxtFREDFilter3.Visible = False
            pnlnumFREDFilter3.Visible = False
            pnldtpFREDFilter3.Visible = False


            SQL = "select substring(Column_Alias,1,30) Column_Alias, COLUMN_Name  " & _
            "from WebFD.FRED.vw_AvailableData " & _
            "where TABLE_ID = " & rblFREDFilter3.SelectedValue & _
            "and Costing in (" & Costing & ") " & _
            "order by 1  "
            ds = New DataSet
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86300
                da.Fill(ds, "FRED")

                ddlFREDFilter3.DataSource = ds
                ddlFREDFilter3.DataMember = "FRED"
                ddlFREDFilter3.DataTextField = "Column_Alias"
                ddlFREDFilter3.DataValueField = "COLUMN_Name"
                ddlFREDFilter3.DataBind()

            End Using
            If ddlFREDFilter3.Items.Count < 1 Then
                ddlFREDFilter3.Visible = False
                ddlFREDFilterOption3.Visible = False
            Else
                ddlFREDFilterOption3.Visible = True
                FREDFILTEROPTION3()

            End If

            lblFREEFilter3.Text = "Filter #3 - " & rblFREDFilter3.SelectedItem.Text
            upFREEFILTER3.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilterOption3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilterOption3.SelectedIndexChanged
        Try
            FREDFILTEROPTION3()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDFILTEROPTION3()
        Try
            pnltxtFREDFilter3.Visible = False
            pnlnumFREDFilter3.Visible = False
            pnldtpFREDFilter3.Visible = False
            lblFREDFilter3.Visible = False
            lblFREDFilter3b.Visible = False

            Select Case ddlFREDFilterOption3.Text
                Case "between"
                    pnlnumFREDFilter3.Visible = True
                Case "not between"
                    pnlnumFREDFilter3.Visible = True
                Case "DATE between"
                    pnldtpFREDFilter3.Visible = True
                Case "DATE not between"
                    pnldtpFREDFilter3.Visible = True
                Case "contains"
                    lblFREDFilter3.Visible = True
                    lblFREDFilter3b.Visible = True
                    pnltxtFREDFilter3.Visible = True
                Case "does not contain"
                    lblFREDFilter3.Visible = True
                    lblFREDFilter3b.Visible = True
                    pnltxtFREDFilter3.Visible = True
                Case "NULL"

                Case "NOT NULL"

                Case Else
                    pnltxtFREDFilter3.Visible = True
            End Select
 
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rblFREDFilter4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFREDFilter4.SelectedIndexChanged
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Costing As String = ""

            If lblCosting.Text = "" Then
                Costing = "0"
            Else
                Costing = "0,1"
            End If

            ddlFREDFilter4.Visible = True
            pnltxtFREDFilter4.Visible = False
            pnlnumFREDFilter4.Visible = False
            pnldtpFREDFilter4.Visible = False


            SQL = "select substring(Column_Alias,1,30) Column_Alias, COLUMN_Name  " & _
            "from WebFD.FRED.vw_AvailableData " & _
            "where TABLE_ID = " & rblFREDFilter4.SelectedValue & _
            "and Costing in (" & Costing & ") " & _
            "order by 1  "
            ds = New DataSet
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "FRED")

                ddlFREDFilter4.DataSource = ds
                ddlFREDFilter4.DataMember = "FRED"
                ddlFREDFilter4.DataTextField = "Column_Alias"
                ddlFREDFilter4.DataValueField = "COLUMN_Name"
                ddlFREDFilter4.DataBind()

            End Using
            If ddlFREDFilter4.Items.Count < 1 Then
                ddlFREDFilter4.Visible = False
                ddlFREDFilterOption4.Visible = False
            Else
                ddlFREDFilterOption4.Visible = True
                FREDFILTEROPTION4()

            End If

            lblFREEFilter4.Text = "Filter #4 - " & rblFREDFilter4.SelectedItem.Text
            upFREEFILTER4.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilterOption4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilterOption4.SelectedIndexChanged
        Try
            FREDFILTEROPTION4()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDFILTEROPTION4()
        Try
            pnltxtFREDFilter4.Visible = False
            pnlnumFREDFilter4.Visible = False
            pnldtpFREDFilter4.Visible = False
            lblFREDFilter4.Visible = False
            lblFREDFilter4b.Visible = False

            Select Case ddlFREDFilterOption4.Text
                Case "between"
                    pnlnumFREDFilter4.Visible = True
                Case "not between"
                    pnlnumFREDFilter4.Visible = True
                Case "DATE between"
                    pnldtpFREDFilter4.Visible = True
                Case "DATE not between"
                    pnldtpFREDFilter4.Visible = True
                Case "contains"
                    lblFREDFilter4.Visible = True
                    lblFREDFilter4b.Visible = True
                    pnltxtFREDFilter4.Visible = True
                Case "does not contain"
                    lblFREDFilter4.Visible = True
                    lblFREDFilter4b.Visible = True
                    pnltxtFREDFilter4.Visible = True
                Case "NULL"

                Case "NOT NULL"

                Case Else
                    pnltxtFREDFilter4.Visible = True
            End Select
  
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rblFREDFilter5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblFREDFilter5.SelectedIndexChanged
        Try
            Dim ds As DataSet
            Dim da As New SqlDataAdapter
            Dim Costing As String = ""

            If lblCosting.Text = "" Then
                Costing = "0"
            Else
                Costing = "0,1"
            End If

            ddlFREDFilter5.Visible = True
            pnltxtFREDFilter5.Visible = False
            pnlnumFREDFilter5.Visible = False
            pnldtpFREDFilter5.Visible = False


            SQL = "select substring(Column_Alias,1,30) Column_Alias, COLUMN_Name  " & _
            "from WebFD.FRED.vw_AvailableData " & _
            "where TABLE_ID = " & rblFREDFilter5.SelectedValue & _
            "and Costing in (" & Costing & ") " & _
            "order by 1  "
            ds = New DataSet
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "FRED")

                ddlFREDFilter5.DataSource = ds
                ddlFREDFilter5.DataMember = "FRED"
                ddlFREDFilter5.DataTextField = "Column_Alias"
                ddlFREDFilter5.DataValueField = "COLUMN_Name"
                ddlFREDFilter5.DataBind()

            End Using
            If ddlFREDFilter5.Items.Count < 1 Then
                ddlFREDFilter5.Visible = False
                ddlFREDFilterOption5.Visible = False
            Else
                ddlFREDFilterOption5.Visible = True
                FREDFILTEROPTION5()

            End If
          
            lblFREEFilter5.Text = "Filter #5 - " & rblFREDFilter5.SelectedItem.Text
            upFREEFILTER5.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilterOption5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilterOption5.SelectedIndexChanged
        Try
            FREDFILTEROPTION5()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDFILTEROPTION5()
        Try
            pnltxtFREDFilter5.Visible = False
            pnlnumFREDFilter5.Visible = False
            pnldtpFREDFilter5.Visible = False
            lblFREDFilter5.Visible = False
            lblFREDFilter5b.Visible = False

            Select Case ddlFREDFilterOption5.Text
                Case "between"
                    pnlnumFREDFilter5.Visible = True
                Case "not between"
                    pnlnumFREDFilter5.Visible = True
                Case "DATE between"
                    pnldtpFREDFilter5.Visible = True
                Case "DATE not between"
                    pnldtpFREDFilter5.Visible = True
                Case "contains"
                    lblFREDFilter5.Visible = True
                    lblFREDFilter5b.Visible = True
                    pnltxtFREDFilter5.Visible = True
                Case "does not contain"
                    lblFREDFilter5.Visible = True
                    lblFREDFilter5b.Visible = True
                    pnltxtFREDFilter5.Visible = True
                Case "NULL"

                Case "NOT NULL"

                Case Else
                    pnltxtFREDFilter5.Visible = True
            End Select
 

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnResetFilters_Click(sender As Object, e As System.EventArgs) Handles btnResetFilters.Click
        Try
            ResetFREDFilters()
            acrFREDFilter.SelectedIndex = -1

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ResetFREDFilters()
        Try
            rblFREDFilter1.SelectedIndex = 0
            rblFREDFilter2.SelectedIndex = 0
            rblFREDFilter3.SelectedIndex = 0
            rblFREDFilter4.SelectedIndex = 0
            rblFREDFilter5.SelectedIndex = 0

            ddlFREDFilter1.Visible = False
            ddlFREDFilterOption1.Visible = False
            pnltxtFREDFilter1.Visible = False
            pnlnumFREDFilter1.Visible = False
            pnldtpFREDFilter1.Visible = False
            dtpFREDFilter1StartDate.Text = Date.Today
            dtpFREDFilter1EndDate.Text = Date.Today
            dtpFREDFilter1StartDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            dtpFREDFilter1EndDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            txtFREDFilter1.Text = ""
            numFREDFilter1Start.Text = ""
            numFREDFilter1End.Text = ""
            ddlFREDFilterOption1.SelectedIndex = 0

            ddlFREDFilter2.Visible = False
            ddlFREDFilterOption2.Visible = False
            pnltxtFREDFilter2.Visible = False
            pnlnumFREDFilter2.Visible = False
            pnldtpFREDFilter2.Visible = False
            dtpFREDFilter2StartDate.Text = Date.Today
            dtpFREDFilter2EndDate.Text = Date.Today
            dtpFREDFilter2StartDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            dtpFREDFilter2EndDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            txtFREDFilter2.Text = ""
            numFREDFilter2Start.Text = ""
            numFREDFilter2End.Text = ""
            ddlFREDFilterOption2.SelectedIndex = 0

            ddlFREDFilter3.Visible = False
            ddlFREDFilterOption3.Visible = False
            pnltxtFREDFilter3.Visible = False
            pnlnumFREDFilter3.Visible = False
            pnldtpFREDFilter3.Visible = False
            dtpFREDFilter3StartDate.Text = Date.Today
            dtpFREDFilter3EndDate.Text = Date.Today
            dtpFREDFilter3StartDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            dtpFREDFilter3EndDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            txtFREDFilter3.Text = ""
            numFREDFilter3Start.Text = ""
            numFREDFilter3End.Text = ""
            ddlFREDFilterOption3.SelectedIndex = 0

            ddlFREDFilter4.Visible = False
            ddlFREDFilterOption4.Visible = False
            pnltxtFREDFilter4.Visible = False
            pnlnumFREDFilter4.Visible = False
            pnldtpFREDFilter4.Visible = False
            dtpFREDFilter4StartDate.Text = Date.Today
            dtpFREDFilter4EndDate.Text = Date.Today
            dtpFREDFilter4StartDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            dtpFREDFilter4EndDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            txtFREDFilter4.Text = ""
            numFREDFilter4Start.Text = ""
            numFREDFilter4End.Text = ""
            ddlFREDFilterOption4.SelectedIndex = 0

            ddlFREDFilter5.Visible = False
            ddlFREDFilterOption5.Visible = False
            pnltxtFREDFilter5.Visible = False
            pnlnumFREDFilter5.Visible = False
            pnldtpFREDFilter5.Visible = False
            dtpFREDFilter5StartDate.Text = Date.Today
            dtpFREDFilter5EndDate.Text = Date.Today
            dtpFREDFilter5StartDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            dtpFREDFilter5EndDate.Text = Format(Date.Today(), "yyyy-MM-dd")
            txtFREDFilter5.Text = ""
            numFREDFilter5Start.Text = ""
            numFREDFilter5End.Text = ""
            ddlFREDFilterOption5.SelectedIndex = 0

            lblFREEFilter1.Text = " Filter #1"
            lblFREEFilter2.Text = " Filter #2"
            lblFREEFilter3.Text = " Filter #3"
            lblFREEFilter4.Text = " Filter #4"
            lblFREEFilter5.Text = " Filter #5"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDSelectAll1_Click(sender As Object, e As System.EventArgs) Handles FREDSelectAll1.Click
        Try

            For i As Integer = 0 To cblFREDSelect1.Items.Count - 1
                cblFREDSelect1.Items(i).Selected = True

            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDSelectAll2_Click(sender As Object, e As System.EventArgs) Handles FREDSelectAll2.Click
        Try

            For i As Integer = 0 To cblFREDSelect2.Items.Count - 1
                cblFREDSelect2.Items(i).Selected = True

            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDSelectAll3_Click(sender As Object, e As System.EventArgs) Handles FREDSelectAll3.Click
        Try

            For i As Integer = 0 To cblFREDSelect3.Items.Count - 1
                cblFREDSelect3.Items(i).Selected = True

            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDUnSelectAll1_Click(sender As Object, e As System.EventArgs) Handles FREDUnSelectAll1.Click
        Try

            For i As Integer = 0 To cblFREDSelect1.Items.Count - 1
                cblFREDSelect1.Items(i).Selected = False

            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDUnSelectAll2_Click(sender As Object, e As System.EventArgs) Handles FREDUnSelectAll2.Click
        Try

            For i As Integer = 0 To cblFREDSelect2.Items.Count - 1
                cblFREDSelect2.Items(i).Selected = False

            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FREDUnSelectAll3_Click(sender As Object, e As System.EventArgs) Handles FREDUnSelectAll3.Click
        Try
            For i As Integer = 0 To cblFREDSelect3.Items.Count - 1
                cblFREDSelect3.Items(i).Selected = False
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFREDFilter1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilter1.SelectedIndexChanged
        Try

            lblFREEFilter1.Text = "Filter #1 - " & rblFREDFilter1.SelectedItem.Text & " (" & ddlFREDFilter1.SelectedValue & ")"
            upFREEFILTER1.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilter2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilter2.SelectedIndexChanged
        Try

            lblFREEFilter2.Text = "Filter #2 - " & rblFREDFilter2.SelectedItem.Text & " (" & ddlFREDFilter2.SelectedValue & ")"
            upFREEFILTER2.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilter3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilter3.SelectedIndexChanged
        Try

            lblFREEFilter3.Text = "Filter #3 - " & rblFREDFilter3.SelectedItem.Text & " (" & ddlFREDFilter3.SelectedValue & ")"
            upFREEFILTER3.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilter4_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilter4.SelectedIndexChanged
        Try

            lblFREEFilter4.Text = "Filter #4 - " & rblFREDFilter4.SelectedItem.Text & " (" & ddlFREDFilter4.SelectedValue & ")"
            upFREEFILTER4.Update()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ddlFREDFilter5_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFREDFilter5.SelectedIndexChanged
        Try

            lblFREEFilter5.Text = "Filter #5 - " & rblFREDFilter5.SelectedItem.Text & " (" & ddlFREDFilter5.SelectedValue & ")"
            upFREEFILTER5.Update()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvDWHLookups_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDWHLookups.PageIndexChanging
        Try

            gvDWHLookups.PageIndex = e.NewPageIndex
            BindFred()
           

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvDWHLookups_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDWHLookups.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = DirectCast(e.Row.DataItem, DataRowView)
                For i As Integer = 0 To drv.DataView.Table.Columns.Count - 1
                    If drv.DataView.Table.Columns(i).ColumnName.StartsWith("Account_Detail") Then            'TSTAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.Gold
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.StartsWith("Account") Then               'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.LawnGreen
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionAdjustment") Then   'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.LightSeaGreen
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionBill") Then         'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.LightSeaGreen
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionCash") Then         'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.Green
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionMemo") Then         'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.GreenYellow
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionNote") Then         'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.MediumSeaGreen
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionRefund") Then       'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.SpringGreen
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("TransactionTransfer") Then     'STAR Data 
                        e.Row.Cells(i).BackColor = System.Drawing.Color.PaleGreen
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.StartsWith("FARGLD_ALL_View") Then       'GL Finance (FARGLD) data 
                        e.Row.Cells(i).BackColor = System.Drawing.Color.CornflowerBlue
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("PatientChargeDetail") Then     'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.BlanchedAlmond
                    ElseIf drv.DataView.Table.Columns(i).ColumnName.Contains("MgdCareComponents") Then     'STAR Data
                        e.Row.Cells(i).BackColor = System.Drawing.Color.Yellow
                    Else
                        e.Row.Cells(i).BackColor = System.Drawing.Color.Tomato
                    End If
                Next
            End If

           

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvDWHLookups_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvDWHLookups.Sorting
        Try

            '   BindFred()
        Catch ex As Exception

        End Try
    End Sub
End Class