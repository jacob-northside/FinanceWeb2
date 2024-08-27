Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Data.OleDb

Imports System.Drawing
Imports ClosedXML.Excel

Imports FinanceWeb.WebFinGlobal

Public Class StarGLJEEntries
    Inherits System.Web.UI.Page

    Dim SQL As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd As SqlCommand
    'Public Shared tmpDT As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then
                Page.MaintainScrollPositionOnPostBack = True

            Else
                btnGenerateFiles.Visible = False
                ddlFiles.Visible = False
                lblFiles.Visible = False
            End If

            If Not IsPostBack Then
                LoadExistingFiles()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)

        Try

            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
            Response.WriteFile(filePath)
            Response.End()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Sub LoadExistingFiles()
        Try
            'Dim filePaths() As String = Directory.GetFiles(Server.MapPath("//nsmvwebsvr02/Finance Web/DownloadFiles/StarGL/Atlanta/"))
            'Dim filePaths() As String = Directory.GetFiles("//nsmvwebsvr02/Finance Web/DownloadFiles/StarGL/Atlanta/")
            'Dim filePaths() As String = Directory.GetFiles("//NSHDSWEB/StarGL/Atlanta/") 
            ' Flipped to FinanceWeb 4/16/2018 CRW
            Dim filePaths() As String = Directory.GetFiles("//nshdsfile/files/Shared/BIDS_Transfer/FinanceWeb/StarGL/Atlanta/")
            Dim files As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In filePaths
                files.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next

            GridView1.DataSource = files
            GridView1.DataBind()
            'GridView1.Sort(GridView1.SortExpression, WebControls.SortDirection.Descending)

            'Dim ChefilePaths() As String = Directory.GetFiles("//NSHDSWEB/StarGL/Cherokee/")
            ' Flipped to FinanceWeb 4/16/2018 CRW
            Dim ChefilePaths() As String = Directory.GetFiles("//nshdsfile/files/Shared/BIDS_Transfer/FinanceWeb/StarGL/Cherokee/")
            Dim Chefiles As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In ChefilePaths
                Chefiles.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
            GVCherokeeDownloads.DataSource = Chefiles
            GVCherokeeDownloads.DataBind()
            'GVCherokeeDownloads.Sort(GVCherokeeDownloads.SortExpression, WebControls.SortDirection.Descending)

            'Dim ForsythfilePaths() As String = Directory.GetFiles("//NSHDSWEB/StarGL/Forsyth/")
            ' Flipped to FinanceWeb 4/16/2018 CRW
            Dim ForsythfilePaths() As String = Directory.GetFiles("//nshdsfile/files/Shared/BIDS_Transfer/FinanceWeb/StarGL/Forsyth/")
            Dim Forsythfiles As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In ForsythfilePaths
                Forsythfiles.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
            GVForsythDownloads.DataSource = Forsythfiles
            GVForsythDownloads.DataBind()
            'GVForsythDownloads.Sort(GVForsythDownloads.SortExpression, WebControls.SortDirection.Descending)

            Dim DuluthfilePaths() As String = Directory.GetFiles("//nshdsfile/files/Shared/BIDS_Transfer/FinanceWeb/StarGL/Duluth/")
            Dim Duluthfiles As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In DuluthfilePaths
                Duluthfiles.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
            GVDuluthDownloads.DataSource = Duluthfiles
            GVDuluthDownloads.DataBind()
            'GVForsythDownloads.Sort(GVForsythDownloads.SortExpression, WebControls.SortDirection.Descending)


            Dim GwinnettfilePaths() As String = Directory.GetFiles("//nshdsfile/files/Shared/BIDS_Transfer/FinanceWeb/StarGL/Gwinnett/")
            Dim Gwinnettfiles As List(Of ListItem) = New List(Of ListItem)
            For Each filePath As String In GwinnettfilePaths
                Gwinnettfiles.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
            gvGwinnettDownloads.DataSource = Gwinnettfiles
            gvGwinnettDownloads.DataBind()
            'GVForsythDownloads.Sort(GVForsythDownloads.SortExpression, WebControls.SortDirection.Descending)


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub btnGenerateFiles_Click(sender As Object, e As System.EventArgs) Handles btnGenerateFiles.Click
        Try
            Dim SQL As String = ""
            Dim ds As DataSet
            Dim da As SqlDataAdapter

            Dim FAC As String = ""
            Dim GLEffectiveDate As String = ""

            FAC = ddlFiles.SelectedItem.Text.Substring(0, 1)
            GLEffectiveDate = ddlFiles.SelectedItem.Text.Substring(2)

            If FAC = "A" Then
                'If chbAtlanta.Checked = True Then
                'SQL = "select " & _
                '        "case when ID is null then NULL " & _
                '        "else cast(ID as varchar) " & _
                '        "End ID, FY, " & _
                '        "case when Fac = 'A' then '10' " & _
                '        "when Fac = 'C' then '22' " & _
                '        "when Fac = 'F' then '6' " & _
                '        "else Fac " & _
                '        "End Fac, " & _
                '        "Dept, SubAcct, " & _
                '        "case when ID is null then 'Total' else Description end Description, " & _
                '        "Debit, Credit, '' as Spacer     " & _
                '        " from " & _
                '        "(select " & _
                '        "x.ID, x.FY, " & _
                '        "case when Grouping(x.Fac) = 1 then 'Total' " & _
                '        "else Fac " & _
                '        "   end Fac,  " & _
                '        "x.Dept,  " & _
                '        "x.SubAcct,  " & _
                '        " 'PA Daily Journal Entry - Clear Suspense Activity' as  Description , " & _
                '        "  sum(x.Debit) as Debit , " & _
                '        "   sum(x.Credit) as Credit       " & _
                '        "  from  " & _
                '        "( select  " & _
                '        " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY   " & _
                '        "  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                '        "  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                '        " where a.ISCORRECTED is null  " & _
                '        " and a.Dept is not null and a.Dept <> 9999  " & _
                '        " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                '        "and a.Fac = 'A' " & _
                '        " union  " & _
                '        "  select  " & _
                '        " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY    " & _
                '        "  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                '        "  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                '        " where a.ISCORRECTED is null  " & _
                '        " and a.Dept is not null  and a.Dept <> 9999 " & _
                '        " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                '        "and a.Fac = 'A' " & _
                '        " ) x  " & _
                '        "group by Fac, Dept, SubAcct, ID, FY with rollup  ) y  " & _
                '        "where (y.ID is not null and FY is not null)  " & _
                '        "or (y.ID is null and Fac is not null  " & _
                '        "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                '        "order by Fac, ID desc  , Dept desc  "

                SQL = "select " & _
                      "case when ID is null then NULL " & _
                      "else cast(ID as varchar) " & _
                      "End ID, FY, " & _
                      "case when Fac = 'A' then '10' " & _
                      "when Fac = 'C' then '22' " & _
                      "when Fac = 'F' then '06' " & _
                      "when Fac = 'D' then '40' " & _
                      "when Fac = 'L' then '30' " & _
                      "else Fac " & _
                      "End Fac, " & _
                      "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                      "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                      "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                      "else convert(varchar(5),Dept)  " & _
                      "End  Dept, " & _
                      "case  " & _
                      "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                      "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                      "else convert(varchar(5), SubAcct  ) " & _
                      "end   SubAcct,  " & _
                      "case when ID is null then 'Total' else Description end Description, " & _
                      "Debit, Credit, '' as Spacer     " & _
                      " from " & _
                      "(select " & _
                      "x.ID, x.FY, " & _
                      "case when Grouping(x.Fac) = 1 then 'Total' " & _
                      "else Fac " & _
                      "   end Fac,  " & _
                      "x.Dept,  " & _
                      "x.SubAcct,  " & _
                      "  'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description,  " & _
                      "  sum(x.Debit) as Debit , " & _
                      "   sum(x.Credit) as Credit       " & _
                      "  from  " & _
                      "( select  " & _
                      " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate    " & _
                      "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                      "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                      " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                      " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                      " and a.Dept is not null and a.Dept <> 9999  " & _
                      " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                      "and a.Fac = 'A' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                      " union  " & _
                      "  select  " & _
                      " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate      " & _
                      "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                      "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                      " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                      " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                      " and a.Dept is not null  and a.Dept <> 9999 " & _
                      " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                      "and a.Fac = 'A' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                      " ) x  " & _
                      "group by Fac, Dept, SubAcct, ID, FY, Pat_Acct_Fac, TranDate  with rollup  ) y  " & _
                      "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                      "or (y.ID is null and Fac is not null  " & _
                      "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                      "order by Fac, ID desc  , Dept desc  "


                Using cmd As New SqlCommand(SQL, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 300

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    da = New SqlDataAdapter
                    da.SelectCommand = cmd

                    ds = New DataSet
                    da.Fill(ds, "FullTable")
                End Using

                Dim drAtl As Array
                Dim dtAtl As New DataTable()
                Dim dr As DataRow

                drAtl = ds.Tables(0).Select("Fac ='10'")
                dtAtl = ds.Tables(0).Clone

                For Each dr In drAtl
                    dtAtl.ImportRow(dr)
                Next

                'tmpDT = dtAtl
                gvAtlanta.DataSource = dtAtl
                gvAtlanta.DataBind()

                If gvAtlanta.Rows.Count > 0 Then
                    lblAtlMessage.Text = (gvAtlanta.Rows.Count.ToString - 1) / 2 & "  Entries"
                    lblAtlMessage.Visible = True
                    btnExportAtlanta.Visible = True
                    'Label1.Visible = True
                    'AtlGLEffectiveDate.Visible = True
                Else
                    lblAtlMessage.Text = "No data to export"
                    lblAtlMessage.Visible = True
                    btnExportAtlanta.Visible = False
                    'Label1.Visible = False
                    'AtlGLEffectiveDate.Visible = False
                End If
                'rblAtlanta.Visible = True
                ATL.Visible = True
            Else
                ATL.Visible = False
            End If

            If FAC = "C" Then
                'If chbCherokee.Checked = True Then
                SQL = "select " & _
                    "case when ID is null then NULL " & _
                    "else cast(ID as varchar) " & _
                    "End ID, FY, " & _
                    "case when Fac = 'A' then '10' " & _
                    "when Fac = 'C' then '22' " & _
                    "when Fac = 'F' then '06' " & _
                    "when Fac = 'D' then '40' " & _
                    "when Fac = 'L' then '30' " & _
                    "else Fac " & _
                    "End Fac, " & _
                       "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                      "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                      "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                      "else convert(varchar(5),Dept)  " & _
                      "End  Dept, " & _
                      "case " & _
                      "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                      "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                      "else convert(varchar(5), SubAcct  ) " & _
                      "end   SubAcct,  " & _
                    "case when ID is null then 'Total' else Description end Description, " & _
                    "Debit, Credit, '' as Spacer     " & _
                    " from " & _
                    "(select " & _
                    "x.ID, x.FY, " & _
                    "case when Grouping(x.Fac) = 1 then 'Total' " & _
                    "else Fac " & _
                    "   end Fac,  " & _
                    "x.Dept,  " & _
                    "x.SubAcct,  " & _
                    "  'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description,  " & _
                    "  sum(x.Debit) as Debit , " & _
                    "   sum(x.Credit) as Credit       " & _
                    "  from  " & _
                    "( select  " & _
                    " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate    " & _
                    "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                    "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                    " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                    "and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                    " and a.Dept is not null and a.Dept <> 9999  " & _
                    " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                    "and a.Fac = 'C' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                    " union  " & _
                    "  select  " & _
                    " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate      " & _
                    "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                    "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                    " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                    "and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                    " and a.Dept is not null  and a.Dept <> 9999 " & _
                    " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                    "and a.Fac = 'C' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                    " ) x  " & _
                    "group by Fac, Dept, SubAcct, ID, FY, Pat_Acct_Fac, TranDate  with rollup  ) y  " & _
                    "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                    "or (y.ID is null and Fac is not null  " & _
                    "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                    "order by Fac, ID desc  , Dept desc  "

                Using cmd As New SqlCommand(SQL, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 300

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    da = New SqlDataAdapter
                    da.SelectCommand = cmd

                    ds = New DataSet
                    da.Fill(ds, "FullTable")
                End Using

                Dim drChe As Array
                Dim dtChe As New DataTable

                drChe = ds.Tables(0).Select("Fac = '22'")
                dtChe = ds.Tables(0).Clone
                For Each dr In drChe
                    dtChe.ImportRow(dr)
                Next

                gvCherokee.DataSource = dtChe
                gvCherokee.DataBind()

                If gvCherokee.Rows.Count > 0 Then
                    lblCheMessage.Text = (gvCherokee.Rows.Count.ToString - 1) / 2 & "  Entries"
                    lblCheMessage.Visible = True
                    btnExportCherokee.Visible = True
                    'Label2.Visible = True
                    'CHEGLEffectiveDate.Visible = True
                Else
                    lblCheMessage.Text = "No data to export"
                    lblCheMessage.Visible = True
                    btnExportCherokee.Visible = False
                    'Label2.Visible = True
                    'CHEGLEffectiveDate.Visible = True
                End If
                'rblCherokee.Visible = True
                Cherokee.Visible = True
            Else
                Cherokee.Visible = False
            End If

            If FAC = "F" Then
                'If chbForsyth.Checked = True Then
                SQL = "select " & _
                     "case when ID is null then NULL " & _
                     "else cast(ID as varchar) " & _
                     "End ID, FY, " & _
                     "case when Fac = 'A' then '10' " & _
                     "when Fac = 'C' then '22' " & _
                     "when Fac = 'F' then '06' " & _
                     "when Fac = 'D' then '40' " & _
                     "when Fac = 'L' then '30' " & _
                     "else Fac " & _
                     "End Fac, " & _
                    "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                      "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                      "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                      "else convert(varchar(5),Dept)  " & _
                      "End  Dept, " & _
                      "case " & _
                      "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                      "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                      "else convert(varchar(5), SubAcct  ) " & _
                      "end   SubAcct,  " & _
                     "case when ID is null then 'Total' else Description end Description, " & _
                     "Debit, Credit, '' as Spacer     " & _
                     " from " & _
                     "(select " & _
                     "x.ID, x.FY, " & _
                     "case when Grouping(x.Fac) = 1 then 'Total' " & _
                     "else Fac " & _
                     "   end Fac,  " & _
                     "x.Dept,  " & _
                     "x.SubAcct,  " & _
                     "  'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description,  " & _
                     "  sum(x.Debit) as Debit , " & _
                     "   sum(x.Credit) as Credit       " & _
                     "  from  " & _
                     "( select  " & _
                     " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate    " & _
                     "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                     "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                     " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                     " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                     " and a.Dept is not null and a.Dept <> 9999  " & _
                     " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                     "and a.Fac = 'F' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                     " union  " & _
                     "  select  " & _
                     " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate      " & _
                     "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                     "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                     " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                     " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                     " and a.Dept is not null  and a.Dept <> 9999 " & _
                     " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                     "and a.Fac = 'F' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                     " ) x  " & _
                     "group by Fac, Dept, SubAcct, ID, FY, Pat_Acct_Fac, TranDate  with rollup  ) y  " & _
                     "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                     "or (y.ID is null and Fac is not null  " & _
                     "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                     "order by Fac, ID desc  , Dept desc  "

                Using cmd As New SqlCommand(SQL, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 300

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    da = New SqlDataAdapter
                    da.SelectCommand = cmd

                    ds = New DataSet
                    da.Fill(ds, "FullTable")
                End Using

                Dim drFor As Array
                Dim dtFor As New DataTable

                drFor = ds.Tables(0).Select("Fac = '06'")
                dtFor = ds.Tables(0).Clone
                For Each dr In drFor
                    dtFor.ImportRow(dr)
                Next

                gvForsyth.DataSource = dtFor
                gvForsyth.DataBind()

                If gvForsyth.Rows.Count > 0 Then
                    lblForMessage.Text = (gvForsyth.Rows.Count - 1) / 2 & "  Entries"
                    lblForMessage.Visible = True
                    btnExportForsyth.Visible = True
                    'Label5.Visible = True
                    'FORGLEffectiveDate.Visible = True
                Else
                    lblForMessage.Text = "No data to export"
                    lblForMessage.Visible = True
                    btnExportForsyth.Visible = False
                    'Label5.Visible = True
                    'FORGLEffectiveDate.Visible = True
                End If
                'rdlForsyth.Visible = True
                FORSYTH.Visible = True
            Else
                FORSYTH.Visible = False
            End If

            If FAC = "D" Then
                'If chbForsyth.Checked = True Then
                SQL = "select " & _
                     "case when ID is null then NULL " & _
                     "else cast(ID as varchar) " & _
                     "End ID, FY, " & _
                     "case when Fac = 'A' then '10' " & _
                     "when Fac = 'C' then '22' " & _
                     "when Fac = 'F' then '06' " & _
                     "when Fac = 'D' then '40' " & _
                     "when Fac = 'L' then '30' " & _
                     "else Fac " & _
                     "End Fac, " & _
                    "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                      "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                      "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                      "else convert(varchar(5),Dept)  " & _
                      "End  Dept, " & _
                      "case " & _
                      "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                      "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                      "else convert(varchar(5), SubAcct  ) " & _
                      "end   SubAcct,  " & _
                     "case when ID is null then 'Total' else Description end Description, " & _
                     "Debit, Credit, '' as Spacer     " & _
                     " from " & _
                     "(select " & _
                     "x.ID, x.FY, " & _
                     "case when Grouping(x.Fac) = 1 then 'Total' " & _
                     "else Fac " & _
                     "   end Fac,  " & _
                     "x.Dept,  " & _
                     "x.SubAcct,  " & _
                     "  'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description,  " & _
                     "  sum(x.Debit) as Debit , " & _
                     "   sum(x.Credit) as Credit       " & _
                     "  from  " & _
                     "( select  " & _
                     " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate    " & _
                     "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                     "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                     " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                     " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                     " and a.Dept is not null and a.Dept <> 9999  " & _
                     " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                     "and a.Fac = 'D' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                     " union  " & _
                     "  select  " & _
                     " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate      " & _
                     "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                     "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                     " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                     " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                     " and a.Dept is not null  and a.Dept <> 9999 " & _
                     " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                     "and a.Fac = 'D' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                     " ) x  " & _
                     "group by Fac, Dept, SubAcct, ID, FY, Pat_Acct_Fac, TranDate  with rollup  ) y  " & _
                     "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                     "or (y.ID is null and Fac is not null  " & _
                     "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                     "order by Fac, ID desc  , Dept desc  "

                Using cmd As New SqlCommand(SQL, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 300

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    da = New SqlDataAdapter
                    da.SelectCommand = cmd

                    ds = New DataSet
                    da.Fill(ds, "FullTable")
                End Using

                Dim drDuluth As Array
                Dim dtDuluth As New DataTable

                drDuluth = ds.Tables(0).Select("Fac = '40'")
                dtDuluth = ds.Tables(0).Clone
                For Each dr In drDuluth
                    dtDuluth.ImportRow(dr)
                Next

                gvDuluth.DataSource = dtDuluth
                gvDuluth.DataBind()

                If gvDuluth.Rows.Count > 0 Then
                    lblDuluthMessage.Text = (gvDuluth.Rows.Count - 1) / 2 & "  Entries"
                    lblDuluthMessage.Visible = True
                    btnExportDuluth.Visible = True
                    'Label5.Visible = True
                    'FORGLEffectiveDate.Visible = True
                Else
                    lblDuluthMessage.Text = "No data to export"
                    lblDuluthMessage.Visible = True
                    btnExportDuluth.Visible = False
                    'Label5.Visible = True
                    'FORGLEffectiveDate.Visible = True
                End If
                'rdlForsyth.Visible = True
                Duluth.Visible = True
            Else
                Duluth.Visible = False
            End If


            If FAC = "L" Then
                'If chbForsyth.Checked = True Then
                SQL = "select " & _
                     "case when ID is null then NULL " & _
                     "else cast(ID as varchar) " & _
                     "End ID, FY, " & _
                     "case when Fac = 'A' then '10' " & _
                     "when Fac = 'C' then '22' " & _
                     "when Fac = 'F' then '06' " & _
                     "when Fac = 'D' then '40' " & _
                     "when Fac = 'L' then '30' " & _
                     "else Fac " & _
                     "End Fac, " & _
                    "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                      "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                      "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                      "else convert(varchar(5),Dept)  " & _
                      "End  Dept, " & _
                      "case " & _
                      "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                      "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                      "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                      "else convert(varchar(5), SubAcct  ) " & _
                      "end   SubAcct,  " & _
                     "case when ID is null then 'Total' else Description end Description, " & _
                     "Debit, Credit, '' as Spacer     " & _
                     " from " & _
                     "(select " & _
                     "x.ID, x.FY, " & _
                     "case when Grouping(x.Fac) = 1 then 'Total' " & _
                     "else Fac " & _
                     "   end Fac,  " & _
                     "x.Dept,  " & _
                     "x.SubAcct,  " & _
                     "  'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description,  " & _
                     "  sum(x.Debit) as Debit , " & _
                     "   sum(x.Credit) as Credit       " & _
                     "  from  " & _
                     "( select  " & _
                     " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate    " & _
                     "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                     "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                     " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                     " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                     " and a.Dept is not null and a.Dept <> 9999  " & _
                     " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                     "and a.Fac = 'L' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                     " union  " & _
                     "  select  " & _
                     " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate      " & _
                     "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                     "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                     " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                     " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                     " and a.Dept is not null  and a.Dept <> 9999 " & _
                     " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                     "and a.Fac = 'L' and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                     " ) x  " & _
                     "group by Fac, Dept, SubAcct, ID, FY, Pat_Acct_Fac, TranDate  with rollup  ) y  " & _
                     "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                     "or (y.ID is null and Fac is not null  " & _
                     "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                     "order by Fac, ID desc  , Dept desc  "

                Using cmd As New SqlCommand(SQL, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 300

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    da = New SqlDataAdapter
                    da.SelectCommand = cmd

                    ds = New DataSet
                    da.Fill(ds, "FullTable")
                End Using

                Dim drGwinnett As Array
                Dim dtGwinnett As New DataTable

                drGwinnett = ds.Tables(0).Select("Fac = '30'")
                dtGwinnett = ds.Tables(0).Clone
                For Each dr In drGwinnett
                    dtGwinnett.ImportRow(dr)
                Next

                gvGwinnett.DataSource = dtGwinnett
                gvGwinnett.DataBind()

                If gvGwinnett.Rows.Count > 0 Then
                    lblGwinnettMessage.Text = (gvGwinnett.Rows.Count - 1) / 2 & "  Entries"
                    lblGwinnettMessage.Visible = True
                    btnExportGwinnett.Visible = True
                    'Label5.Visible = True
                    'FORGLEffectiveDate.Visible = True
                Else
                    lblGwinnettMessage.Text = "No data to export"
                    lblGwinnettMessage.Visible = True
                    btnExportGwinnett.Visible = False
                    'Label5.Visible = True
                    'FORGLEffectiveDate.Visible = True
                End If
                'rdlForsyth.Visible = True
                Gwinnett.Visible = True
            Else
                Gwinnett.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub BtnGenerateAtlanta_Click(sender As Object, e As EventArgs) Handles BtnGenerateAtlanta.Click
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()
            Dim AtlEntry As String


            AtlEntry = "AtlantaGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

            If gvAtlanta.Rows.Count <> 0 And hlDownload.Visible = False Then
                Dim FARGLDID As String = ""
                Dim FARGLDYr As String = ""
                Dim FARGLDDeptUp As String = ""
                Dim FARGLDAcctUp As String = ""
                Dim SQLWhere As String = ""

                For j As Integer = 0 To gvAtlanta.Rows.Count - 1
                    FARGLDID = gvAtlanta.Rows(j).Cells(10).Text
                    FARGLDYr = gvAtlanta.Rows(j).Cells(11).Text

                    If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                        If SQLWhere.Contains(FARGLDID) = False Then
                            SQLWhere = SQLWhere & " ( FAC = 'A' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                        End If
                    End If
                Next

                If SQLWhere <> "" Then
                    SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                    SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                        "iscorrected = sysdatetime() " & _
                         SQLWhere
                    ' I liked this code because it updated based on selects  
                    'SQL = "update " & _
                    '   "b " & _
                    '     "set " & _
                    '     "b.FY = a.FY, " & _
                    '     "b.FM = a.FM, " & _
                    '     "b.CY = a.CY, " & _
                    '     "b.CM  = a.CM, " & _
                    '     "b.PostDate = a.PostDate, " & _
                    '     "b.Fac = a.Fac, " & _
                    '     "b.Acct = a.Acct, " & _
                    '     "b.tranDate = a.Trandate, " & _
                    '     "b.TranCodeID = a.TranCodeID,  " & _
                    '     "b.Debit = a.Debit, " & _
                    '     "b.Credit = a.Credit, " & _
                    '     "b.Qty = a.Qty, " & _
                    '     "b.ContrAcct = a.ContrAcct, " & _
                    '     "b.Pat_Acct_Fac = a.Pat_Acct_Fac " & _
                    '     "from DWH.dbo.FARGLD_Corrections b " & _
                    '     "Inner Join " & _
                    '     "DWH.dbo.FARGLD a " & _
                    '     "on (a.ID = b.ID and a.FY = b.FY and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac) " &
                    '     SQLWhere


                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                pnlExportHeader.Visible = True
                lblHeader.Text = "Atlanta Northside Hospital Journal Entry"
                lblEntity.Text = "Entity: 10"
                lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

                Page.Response.AddHeader("content-disposition", "attachment;filename=" & AtlEntry)
                Page.Response.ContentType = "application/vnd.ms-excel"
                Page.Response.Charset = ""
                Page.EnableViewState = False
                frm.Attributes("runat") = "server"
                Controls.Add(frm)
                frm.Controls.Add(pnlExportHeader)
                frm.Controls.Add(gvAtlanta)
                frm.RenderControl(hw)
                Response.Write(sw.ToString())
                pnlExportHeader.Visible = False
                Response.End()
            Else
                lblAtlMessage.Text = "No data to export"
                lblAtlMessage.Visible = True
            End If




            ''''This code worked when the gridview was populated via a datasource  
            'Dim dsARG As New DataSourceSelectArguments()
            'Dim view As DataView = CType(SqlDataSource1.Select(dsARG), DataView)
            'Dim dt As DataTable = view.ToTable()

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    Dim FARGLDID As String = dt.Rows(i)(0).ToString()
            '    Dim FARGLDyr As String = dt.Rows(i)(7).ToString()
            '    Dim FARGLDdeptUP As String = dt.Rows(i)(2).ToString()
            '    Dim FARGLDAcctUP As String = dt.Rows(i)(3).ToString()

            '    If FARGLDdeptUP <> "" And FARGLDAcctUP <> "" Then
            '        SQL = "update " & _
            '        "b " & _
            '        "set " & _
            '        "b.FY = a.FY, " & _
            '        "b.FM = a.FM, " & _
            '        "b.CY = a.CY, " & _
            '        "b.CM  = a.CM, " & _
            '        "b.PostDate = a.PostDate, " & _
            '        "b.Fac = a.Fac, " & _
            '        "b.Acct = a.Acct, " & _
            '        "b.tranDate = a.Trandate, " & _
            '        "b.TranCodeID = a.TranCodeID,  " & _
            '        "b.Debit = a.Debit, " & _
            '        "b.Credit = a.Credit, " & _
            '        "b.Qty = a.Qty, " & _
            '        "b.ContrAcct = a.ContrAcct, " & _
            '        "b.Pat_Acct_Fac = a.Pat_Acct_Fac " & _
            '        "from DWH.dbo.FARGLD_Corrections b " & _
            '        "Inner Join " & _
            '        "DWH.dbo.FARGLD a " & _
            '        "on (a.ID = b.ID and a.FY = b.FY and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac) " & _
            '        "where a.ID is not null and a.ID = " & Replace(FARGLDID, "'", "''") & " and a.FY = " & Replace(FARGLDyr, "'", "''") & " "

            '        cmd = New SqlCommand(SQL, conn)
            '        If conn.State = ConnectionState.Closed Then
            '            conn.Open()
            '        End If
            '        cmd.ExecuteReader()
            '    End If
            'Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub btnGenerateCherokee_Click(sender As Object, e As EventArgs) Handles btnGenerateCherokee.Click
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()
            Dim CheEntry As String

            CheEntry = "CherokeeGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

            If gvCherokee.Rows.Count <> 0 Then
                Dim FARGLDID As String = ""
                Dim FARGLDYr As String = ""
                Dim FARGLDDeptUp As String = ""
                Dim FARGLDAcctUp As String = ""
                Dim SQLWhere As String = ""

                gvCherokee.Columns(0).Visible = True

                For j As Integer = 0 To gvCherokee.Rows.Count - 1
                    FARGLDID = gvCherokee.Rows(j).Cells(10).Text
                    FARGLDYr = gvCherokee.Rows(j).Cells(11).Text

                    If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                        If SQLWhere.Contains(FARGLDID) = False Then
                            SQLWhere = SQLWhere & " ( FAC = 'C' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                        End If
                    End If
                Next

                If SQLWhere <> "" Then
                    SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                    SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                        "iscorrected = sysdatetime() " & _
                        SQLWhere

                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                pnlExportHeader.Visible = True
                lblHeader.Text = "Cherokee Northside Hospital Journal Entry"
                lblEntity.Text = "Entity: 22"
                lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

                Page.Response.Clear()
                Page.Response.AddHeader("content-disposition", "attachment;filename=" & CheEntry)
                Page.Response.ContentType = "application/vnd.ms-excel"
                Page.Response.Charset = ""
                Page.EnableViewState = False
                frm.Attributes("runat") = "server"
                Controls.Add(frm)
                frm.Controls.Add(pnlExportHeader)
                frm.Controls.Add(gvCherokee)
                frm.RenderControl(hw)
                Response.Write(sw.ToString())
                Response.End()

                pnlExportHeader.Visible = False
            Else
                lblCheMessage.Text = "No data to export"
                lblCheMessage.Visible = True
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub btnGenerateForsyth_Click(sender As Object, e As EventArgs) Handles btnGenerateForsyth.Click
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()
            Dim ForEntry As String

            ForEntry = "ForsythGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

            If gvForsyth.Rows.Count <> 0 Then
                Dim FARGLDID As String = ""
                Dim FARGLDYr As String = ""
                Dim FARGLDDeptUp As String = ""
                Dim FARGLDAcctUp As String = ""
                Dim SQLWhere As String = ""

                gvForsyth.Columns(0).Visible = True

                For j As Integer = 0 To gvForsyth.Rows.Count - 1
                    FARGLDID = gvForsyth.Rows(j).Cells(10).Text
                    FARGLDYr = gvForsyth.Rows(j).Cells(11).Text

                    If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                        If SQLWhere.Contains(FARGLDID) = False Then
                            SQLWhere = SQLWhere & " ( FAC = 'F' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                        End If
                    End If
                Next

                If SQLWhere <> "" Then
                    SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                    SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                          "iscorrected = sysdatetime() " & _
                          SQLWhere

                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                pnlExportHeader.Visible = True
                lblHeader.Text = "Forysth Northside Hospital Journal Entry"
                lblEntity.Text = "Entity: 6"
                lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/vnd.ms-excel"
                ' Response.ContentType = "application/vnd.openxmlformats"

                Response.AddHeader("content-disposition", "attachment;filename=" & ForEntry)

                Response.Charset = ""
                EnableViewState = False

                frm.Attributes("runat") = "server"
                Controls.Add(frm)
                frm.Controls.Add(pnlExportHeader)
                frm.Controls.Add(gvForsyth)
                frm.RenderControl(hw)
                Response.Write(sw.ToString())
                pnlExportHeader.Visible = False
                Response.End()

                '             Response.Clear();
                'Response.Buffer = true;
                'Response.ContentType = "application/vnd.ms-excel";
                'Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls");
                'Response.Charset = "";
                'this.EnableViewState = false;

                'System.IO.StringWriter sw = new System.IO.StringWriter();
                'System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                'gvFiles.RenderControl(htw);

                'Response.Write(sw.ToString());
                'Response.End();
            Else
                lblForMessage.Text = "No data to export"
                lblForMessage.Visible = True
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnGenerateDuluth_Click(sender As Object, e As EventArgs) Handles btnGenerateDuluth.Click
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()
            Dim DuluthEntry As String

            DuluthEntry = "DuluthGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

            If gvDuluth.Rows.Count <> 0 Then
                Dim FARGLDID As String = ""
                Dim FARGLDYr As String = ""
                Dim FARGLDDeptUp As String = ""
                Dim FARGLDAcctUp As String = ""
                Dim SQLWhere As String = ""

                gvDuluth.Columns(0).Visible = True

                For j As Integer = 0 To gvDuluth.Rows.Count - 1
                    FARGLDID = gvDuluth.Rows(j).Cells(10).Text
                    FARGLDYr = gvDuluth.Rows(j).Cells(11).Text

                    If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                        If SQLWhere.Contains(FARGLDID) = False Then
                            SQLWhere = SQLWhere & " ( FAC = 'D' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                        End If
                    End If
                Next

                If SQLWhere <> "" Then
                    SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                    SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                          "iscorrected = sysdatetime() " & _
                          SQLWhere

                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                pnlExportHeader.Visible = True
                lblHeader.Text = "Duluth Northside Hospital Journal Entry"
                lblEntity.Text = "Entity: 40"
                lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/vnd.ms-excel"
                ' Response.ContentType = "application/vnd.openxmlformats"

                Response.AddHeader("content-disposition", "attachment;filename=" & DuluthEntry)

                Response.Charset = ""
                EnableViewState = False

                frm.Attributes("runat") = "server"
                Controls.Add(frm)
                frm.Controls.Add(pnlExportHeader)
                frm.Controls.Add(gvDuluth)
                frm.RenderControl(hw)
                Response.Write(sw.ToString())
                pnlExportHeader.Visible = False
                Response.End()

                '             Response.Clear();
                'Response.Buffer = true;
                'Response.ContentType = "application/vnd.ms-excel";
                'Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls");
                'Response.Charset = "";
                'this.EnableViewState = false;

                'System.IO.StringWriter sw = new System.IO.StringWriter();
                'System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                'gvFiles.RenderControl(htw);

                'Response.Write(sw.ToString());
                'Response.End();
            Else
                lblDuluthMessage.Text = "No data to export"
                lblDuluthMessage.Visible = True
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnGenerateGwinnett_Click(sender As Object, e As EventArgs) Handles btnGenerateGwinnett.Click
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()
            Dim GwinnettEntry As String

            GwinnettEntry = "GwinnettGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

            If gvGwinnett.Rows.Count <> 0 Then
                Dim FARGLDID As String = ""
                Dim FARGLDYr As String = ""
                Dim FARGLDDeptUp As String = ""
                Dim FARGLDAcctUp As String = ""
                Dim SQLWhere As String = ""

                gvGwinnett.Columns(0).Visible = True

                For j As Integer = 0 To gvDuluth.Rows.Count - 1
                    FARGLDID = gvDuluth.Rows(j).Cells(10).Text
                    FARGLDYr = gvDuluth.Rows(j).Cells(11).Text

                    If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                        If SQLWhere.Contains(FARGLDID) = False Then
                            SQLWhere = SQLWhere & " ( FAC = 'L' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                        End If
                    End If
                Next

                If SQLWhere <> "" Then
                    SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                    SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                          "iscorrected = sysdatetime() " & _
                          SQLWhere

                    cmd = New SqlCommand(SQL, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.ExecuteReader()
                End If

                pnlExportHeader.Visible = True
                lblHeader.Text = "Gwinnett Northside Hospital Journal Entry"
                lblEntity.Text = "Entity: 30"
                lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/vnd.ms-excel"
                ' Response.ContentType = "application/vnd.openxmlformats"

                Response.AddHeader("content-disposition", "attachment;filename=" & GwinnettEntry)

                Response.Charset = ""
                EnableViewState = False

                frm.Attributes("runat") = "server"
                Controls.Add(frm)
                frm.Controls.Add(pnlExportHeader)
                frm.Controls.Add(gvGwinnett)
                frm.RenderControl(hw)
                Response.Write(sw.ToString())
                pnlExportHeader.Visible = False
                Response.End()

                '             Response.Clear();
                'Response.Buffer = true;
                'Response.ContentType = "application/vnd.ms-excel";
                'Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls");
                'Response.Charset = "";
                'this.EnableViewState = false;

                'System.IO.StringWriter sw = new System.IO.StringWriter();
                'System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                'gvFiles.RenderControl(htw);

                'Response.Write(sw.ToString());
                'Response.End();
            Else
                lblGwinnettMessage.Text = "No data to export"
                lblGwinnettMessage.Visible = True
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnExportAtlanta_Click(sender As Object, e As EventArgs) Handles btnExportAtlanta.Click
        Try
            Dim strDownloadFileName As String = ""
            Dim strExcelConn As String = ""

            If hlDownload.Visible = True Then
                Exit Sub
            End If

            Dim dtSQL As DataTable = RetrieveData("Atl")
            dtSQL.TableName = "STAR GL Entry"
            'Dim dtSQL As DataTable = tmpDT



            'strDownloadFileName = "//nsmvwebsvr02/Finance Web/DownloadFiles/StarGL/Atlanta/AtlantaGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"

            'strDownloadFileName = "\\NSHDSWEB\StarGL\Atlanta\AtlantaGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            ' Flipped to FinanceWeb 4/16/2018 CRW
            strDownloadFileName = "\\nshdsfile\files\Shared\BIDS_Transfer\FinanceWeb\StarGL\Atlanta\AtlantaGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"

            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strDownloadFileName & ".xlsx" & ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=2\'"


            'Using ClosedXML Library (exports .xlsx)
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dtSQL)
                wb.SaveAs(strDownloadFileName)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.Clear()
                'Response.Buffer = False
                'Response.Charset = ""
                'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                'Response.AddHeader("content-disposition", "attachment;filename=Galen_STARINSPLAN.xlsx")
                'Using MyMemoryStream As New MemoryStream()
                'wb.SaveAs("\\northside.local\users\c500726\Test.xlsx")
                'wb.SaveAs(MyMemoryStream)
                '    MyMemoryStream.WriteTo(Response.OutputStream)
                '    Response.Flush()
                '    Response.[End]()
                'End Using
            End Using

            'If rblDownload.SelectedValue = "Yes" Then
            hlDownload.Visible = True

            ' Display the download link.
            hlDownload.Text = "Click here to download Atlanta file."
            hlDownload.NavigateUrl = "file:" & strDownloadFileName
            'Else
            '    ' Hide the download link.
            '    hlDownload.Visible = False
            'End If
            LoadExistingFiles()
            LoadAvailableList()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            lblAtlMessage.Text = ex.ToString
        End Try
    End Sub
    Protected Sub btnExportCherokee_Click(sender As Object, e As EventArgs) Handles btnExportCherokee.Click
        Try
            Dim strDownloadFileName As String = ""
            Dim strExcelConn As String = ""

            If hlDownloadCherokee.Visible = True Then
                Exit Sub
            End If

            Dim dtSQL As DataTable = RetrieveData("Che")
            dtSQL.TableName = "STAR GL Entry"
            'If rblCherokee.SelectedValue = "xls" Then
            '    ' Excel 97-2003 

            'strDownloadFileName = "\\NSHDSWEB\StarGL\Cherokee\CherokeeGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            ' Flipped to FinanceWeb 4/16/2018 CRW
            strDownloadFileName = "\\nshdsfile\files\Shared\BIDS_Transfer\FinanceWeb\StarGL\Cherokee\CherokeeGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strDownloadFileName & ".xlsx" & ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=2\'"


            'Using ClosedXML Library (exports .xlsx)
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dtSQL)
                wb.SaveAs(strDownloadFileName)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.Clear()
                'Response.Buffer = False
                'Response.Charset = ""
                'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                'Response.AddHeader("content-disposition", "attachment;filename=Galen_STARINSPLAN.xlsx")
                'Using MyMemoryStream As New MemoryStream()
                'wb.SaveAs("\\northside.local\users\c500726\Test.xlsx")
                'wb.SaveAs(MyMemoryStream)
                '    MyMemoryStream.WriteTo(Response.OutputStream)
                '    Response.Flush()
                '    Response.[End]()
                'End Using
            End Using


            ' Export data to an Excel spreadsheet.
            'ExportToExcel(strExcelConn, dtSQL)

            'If rblDownload.SelectedValue = "Yes" Then
            hlDownloadCherokee.Visible = True

            ' Display the download link.
            hlDownloadCherokee.Text = "Click here to download Cherokee file."
            hlDownloadCherokee.NavigateUrl = "file:" & strDownloadFileName
            'Else
            '    ' Hide the download link.
            '    hlDownload.Visible = False
            'End If
            LoadExistingFiles()
            LoadAvailableList()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            lblAtlMessage.Text = ex.ToString
        End Try
    End Sub
    Protected Sub btnExportForsyth_Click(sender As Object, e As EventArgs) Handles btnExportForsyth.Click
        Try
            Dim strDownloadFileName As String = ""
            Dim strExcelConn As String = ""

            If hlDownloadForsyth.Visible = True Then
                Exit Sub
            End If

            Dim dtSQL As DataTable = RetrieveData("For")
            dtSQL.TableName = "STAR GL Entry"

            'If rdlForsyth.SelectedValue = "xls" Then
            '    ' Excel 97-2003 

            'strDownloadFileName = "\\NSHDSWEB\StarGL\Forsyth\ForsythGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            ' Flipped to FinanceWeb 4/16/2018 CRW
            strDownloadFileName = "\\nshdsfile\files\Shared\BIDS_Transfer\FinanceWeb\StarGL\Forsyth\ForsythGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strDownloadFileName & ".xlsx" & ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=2\'"


            'Using ClosedXML Library (exports .xlsx)
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dtSQL)
                wb.SaveAs(strDownloadFileName)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.Clear()
                'Response.Buffer = False
                'Response.Charset = ""
                'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                'Response.AddHeader("content-disposition", "attachment;filename=Galen_STARINSPLAN.xlsx")
                'Using MyMemoryStream As New MemoryStream()
                'wb.SaveAs("\\northside.local\users\c500726\Test.xlsx")
                'wb.SaveAs(MyMemoryStream)
                '    MyMemoryStream.WriteTo(Response.OutputStream)
                '    Response.Flush()
                '    Response.[End]()
                'End Using
            End Using

            ' Retrieve data from SQL Server table.




            ' Export data to an Excel spreadsheet.
            'ExportToExcel(strExcelConn, dtSQL)

            'If rblDownload.SelectedValue = "Yes" Then
            hlDownloadForsyth.Visible = True

            ' Display the download link.
            hlDownloadForsyth.Text = "Click here to download Forsyth file."
            hlDownloadForsyth.NavigateUrl = "file:" & strDownloadFileName
            'Else
            '    ' Hide the download link.
            '    hlDownload.Visible = False
            'End If
            LoadExistingFiles()
            LoadAvailableList()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            lblAtlMessage.Text = ex.ToString
        End Try
    End Sub

    Protected Sub btnExportDuluth_Click(sender As Object, e As EventArgs) Handles btnExportDuluth.Click
        Try
            Dim strDownloadFileName As String = ""
            Dim strExcelConn As String = ""

            If hlDownloadDuluth.Visible = True Then
                Exit Sub
            End If

            Dim dtSQL As DataTable = RetrieveData("Duluth")
            dtSQL.TableName = "STAR GL Entry"

            'If rdlForsyth.SelectedValue = "xls" Then
            '    ' Excel 97-2003 

            'strDownloadFileName = "\\NSHDSWEB\StarGL\Forsyth\ForsythGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            ' Flipped to FinanceWeb 4/16/2018 CRW
            strDownloadFileName = "\\nshdsfile\files\Shared\BIDS_Transfer\FinanceWeb\StarGL\Duluth\DuluthGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strDownloadFileName & ".xlsx" & ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=2\'"


            'Using ClosedXML Library (exports .xlsx)
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dtSQL)
                wb.SaveAs(strDownloadFileName)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.Clear()
                'Response.Buffer = False
                'Response.Charset = ""
                'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                'Response.AddHeader("content-disposition", "attachment;filename=Galen_STARINSPLAN.xlsx")
                'Using MyMemoryStream As New MemoryStream()
                'wb.SaveAs("\\northside.local\users\c500726\Test.xlsx")
                'wb.SaveAs(MyMemoryStream)
                '    MyMemoryStream.WriteTo(Response.OutputStream)
                '    Response.Flush()
                '    Response.[End]()
                'End Using
            End Using

            ' Retrieve data from SQL Server table.




            ' Export data to an Excel spreadsheet.
            'ExportToExcel(strExcelConn, dtSQL)

            'If rblDownload.SelectedValue = "Yes" Then
            hlDownloadDuluth.Visible = True

            ' Display the download link.
            hlDownloadDuluth.Text = "Click here to download Duluth file."
            hlDownloadDuluth.NavigateUrl = "file:" & strDownloadFileName
            'Else
            '    ' Hide the download link.
            '    hlDownload.Visible = False
            'End If
            LoadExistingFiles()
            LoadAvailableList()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            lblAtlMessage.Text = ex.ToString
        End Try
    End Sub

    Protected Sub btnExportGwinnett_Click(sender As Object, e As EventArgs) Handles btnExportGwinnett.Click
        Try
            Dim strDownloadFileName As String = ""
            Dim strExcelConn As String = ""

            If hlDownloadGwinnett.Visible = True Then
                Exit Sub
            End If

            Dim dtSQL As DataTable = RetrieveData("Gwinnett")
            dtSQL.TableName = "STAR GL Entry"

            'If rdlForsyth.SelectedValue = "xls" Then
            '    ' Excel 97-2003 

            'strDownloadFileName = "\\NSHDSWEB\StarGL\Forsyth\ForsythGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            ' Flipped to FinanceWeb 4/16/2018 CRW
            strDownloadFileName = "\\nshdsfile\files\Shared\BIDS_Transfer\FinanceWeb\StarGL\Gwinnett\GwinnettGLEntry_" & DateTime.Now.ToString("yyyy_MM_dd_HHmmss") & ".xlsx"
            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strDownloadFileName & ".xlsx" & ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=2\'"


            'Using ClosedXML Library (exports .xlsx)
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dtSQL)
                wb.SaveAs(strDownloadFileName)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.Clear()
                'Response.Buffer = False
                'Response.Charset = ""
                'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                'Response.AddHeader("content-disposition", "attachment;filename=Galen_STARINSPLAN.xlsx")
                'Using MyMemoryStream As New MemoryStream()
                'wb.SaveAs("\\northside.local\users\c500726\Test.xlsx")
                'wb.SaveAs(MyMemoryStream)
                '    MyMemoryStream.WriteTo(Response.OutputStream)
                '    Response.Flush()
                '    Response.[End]()
                'End Using
            End Using

            ' Retrieve data from SQL Server table.




            ' Export data to an Excel spreadsheet.
            'ExportToExcel(strExcelConn, dtSQL)

            'If rblDownload.SelectedValue = "Yes" Then
            hlDownloadGwinnett.Visible = True

            ' Display the download link.
            hlDownloadGwinnett.Text = "Click here to download Gwinnett file."
            hlDownloadGwinnett.NavigateUrl = "file:" & strDownloadFileName
            'Else
            '    ' Hide the download link.
            '    hlDownload.Visible = False
            'End If
            LoadExistingFiles()
            LoadAvailableList()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            lblAtlMessage.Text = ex.ToString
        End Try
    End Sub

    Protected Function RetrieveData(ByVal Location As String) As DataTable

        Try

            Dim dt As New DataTable()
            Dim FARGLDID As String = ""
            Dim FARGLDYr As String = ""
            Dim FARGLDDeptUp As String = ""
            Dim FARGLDAcctUp As String = ""
            Dim SQLWhere As String = ""

            '     Dim FAC As String = ""
            Dim GLEffectiveDate As String = ""

            '   FAC = ddlFiles.SelectedItem.Text.Substring(0, 1)
            GLEffectiveDate = ddlFiles.SelectedItem.Text.Substring(2)

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDConn").ToString())
                ' Initialize a SqlDataAdapter object.

                Select Case Location
                    Case "Atl"

                        'Spacers removed 11/17/2021 re KIM due to HM upgrade CRW
                        Dim da As New SqlDataAdapter("select 1 as lblID, " & _
                        "'Atlanta Northside Hospital Journal Entry' as lblDesc ,  cast('' as varchar) as Entity,  cast('' as varchar) as Dept,  " & _
                        "cast('' as varchar) as SubAcct,  /* cast('' as varchar) as Spacer1,  cast('' as varchar) as Spacer2, */  cast('' as varchar) as Description,  " & _
                        "/* cast('' as varchar) as Spacer3,  cast('' as varchar) as Spacer4, */  cast('' as varchar) as Debit,  cast('' as varchar) as Credit  into #temptable   " & _
                        "insert into #temptable values (2, ' ', '', '', '', '', '', '' /*, '', '', '', '' */ )  " & _
                        "insert into #temptable values (3, 'Entity: 10', '', '', '', '', '', '' /*, '', '', '', '' */ )  " & _
                        "insert into #temptable values (4, 'Date: ' + cast( CONVERT(date,getdate()) as varchar), '', '', '', '', '', '' /*, '', '', '', '' */ )  " & _
                        "insert into #temptable values (5, 'Period: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                        "insert into #temptable values (6, 'Year: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (7, 'Batch: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (8, 'JE Ref#: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                        "insert into #temptable values (9, 'JE(J) or ACCR(A): ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                        "insert into #temptable values (10, 'Reversing: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                        "insert into #temptable values (11, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (12, 'Description: Clear suspense activity', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (13, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (14, ' ', 'Entity', 'Dept', 'SubAcct', /* '', '', */ 'Description', /* '', '', */ 'Debit', 'Credit' )  " & _
                        "select * from #temptable order by lblID " & _
                        "drop table #temptable ", conn)
                        ' Fill the DataTable with data from SQL Server table.
                        da.Fill(dt)

                        'Dim da2 As New SqlDataAdapter("select " & _
                        '"15 as lblID, '' as lblDesc, " & _
                        '"case when Fac = 'A' then '10' " & _
                        '"when Fac = 'C' then '22' " & _
                        '"when Fac = 'F' then '6' " & _
                        '"else Fac " & _
                        '"End Entity, " & _
                        '"case when Dept is null then '' else Dept end Dept  ," & _
                        '"case when SubAcct is Null then '' else SubAcct end SubAcct, " & _
                        '"'' as Spacer1, '' as Spacer2, " & _
                        '"case when ID is null then 'Total' else Description end Description, " & _
                        '"'' as Spacer3, '' as Spacer4, " & _
                        '"Debit, Credit  " & _
                        '" from " & _
                        '"(select " & _
                        '"x.ID, x.FY, " & _
                        '"case when Grouping(x.Fac) = 1 then 'Total' " & _
                        '"else Fac " & _
                        '"   end Fac,  " & _
                        '"x.Dept,  " & _
                        '"x.SubAcct,  " & _
                        '" 'PA Daily Journal Entry - Clear Suspense Activity' as  Description , " & _
                        '"  sum(x.Debit) as Debit , " & _
                        '"   sum(x.Credit) as Credit       " & _
                        '"  from  " & _
                        '"( select  " & _
                        '" b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY   " & _
                        '"  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                        '"  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                        '" where a.ISCORRECTED is null  " & _
                        '" and a.Dept is not null and a.Dept <> 9999  " & _
                        '" and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                        '"and a.Fac = 'A' " & _
                        '" union  " & _
                        '"  select  " & _
                        '" b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY    " & _
                        '"  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                        '"  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                        '" where a.ISCORRECTED is null  " & _
                        '" and a.Dept is not null  and a.Dept <> 9999 " & _
                        '" and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                        '"and a.Fac = 'A' " & _
                        '" ) x  " & _
                        '"group by Fac, Dept, SubAcct, ID, FY with rollup  ) y  " & _
                        '"where (y.ID is not null and FY is not null)  " & _
                        '"or (y.ID is null and Fac is not null  " & _
                        '"   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                        '"order by Fac, ID desc  , Dept desc  ", conn)

                        Dim da2 As New SqlDataAdapter("select " & _
                   "15 as lblID, '' as lblDesc, " & _
                   "case when Fac = 'A' then '10' " & _
                   "when Fac = 'C' then '22' " & _
                   "when Fac = 'F' then '06' " & _
                   "when Fac = 'D' then '40' " & _
                   "when Fac = 'L' then '30' " & _
                   "else Fac " & _
                   "End Entity, " & _
                   "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                   "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                   "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                   "else convert(varchar(5),Dept)  " & _
                   "End  Dept, " & _
                   "case  " & _
                   "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                   "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                   "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                   "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                   "else convert(varchar(5), SubAcct  ) " & _
                   "end   SubAcct,  " & _
                   " /* '' as Spacer1, '' as Spacer2, */ " & _
                   "case when ID is null then 'Total' else Description end Description, " & _
                   "/* '' as Spacer3, '' as Spacer4, */ " & _
                   "Debit, Credit  " & _
                   " from " & _
                   "(select " & _
                   "x.ID, x.FY, " & _
                   "case when Grouping(x.Fac) = 1 then 'Total' " & _
                   "else Fac " & _
                   "   end Fac,  " & _
                   "x.Dept,  " & _
                   "x.SubAcct,  " & _
                   " 'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description, " & _
                   "  sum(x.Debit) as Debit , " & _
                   "   sum(x.Credit) as Credit       " & _
                   "  from  " & _
                   "( select  " & _
                   " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate        " & _
                   "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                   "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                   " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                   " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                   " and a.Dept is not null and a.Dept <> 9999  " & _
                   " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                   "and a.Fac = 'A'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                   " union  " & _
                   "  select  " & _
                   " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate         " & _
                   "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                   "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                   " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                   " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                   " and a.Dept is not null  and a.Dept <> 9999 " & _
                   " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                   "and a.Fac = 'A'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                   " ) x  " & _
                   "group by Fac, Dept, SubAcct, ID, FY,  Pat_Acct_Fac,   TranDate  with rollup  ) y  " & _
                   "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                   "or (y.ID is null and Fac is not null  " & _
                   "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                   "order by Fac, ID desc  , Dept desc  ", conn)

                        ' Fill the DataTable with data from SQL Server table.
                        da2.Fill(dt)


                        'update records in Corrections table 
                        For j As Integer = 0 To gvAtlanta.Rows.Count - 1
                            FARGLDID = gvAtlanta.Rows(j).Cells(10).Text
                            FARGLDYr = gvAtlanta.Rows(j).Cells(11).Text

                            If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                                If SQLWhere.Contains(FARGLDID) = False Then
                                    SQLWhere = SQLWhere & " ( FAC = 'A' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                                End If
                            End If
                        Next

                        If SQLWhere <> "" Then
                            SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                            SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                                "iscorrected = sysdatetime() " & _
                                 SQLWhere


                            cmd = New SqlCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.ExecuteReader()
                        End If

                    Case "Che"
                        Dim da As New SqlDataAdapter("select 1 as lblID, " & _
                      "'Cherokee Northside Hospital Journal Entry' as lblDesc ,  cast('' as varchar) as Entity,  cast('' as varchar) as Dept,  " & _
                      "cast('' as varchar) as SubAcct, /* cast('' as varchar) as Spacer1,  cast('' as varchar) as Spacer2, */ cast('' as varchar) as Description,  " & _
                      " /* cast('' as varchar) as Spacer3,  cast('' as varchar) as Spacer4, */ cast('' as varchar) as Debit,  cast('' as varchar) as Credit  into #temptable   " & _
                      "insert into #temptable values (2, ' ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (3, 'Entity: 22', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (4, 'Date: ' + cast( CONVERT(date,getdate()) as varchar), '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                      "insert into #temptable values (5, 'Period: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                      "insert into #temptable values (6, 'Year: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (7, 'Batch: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                      "insert into #temptable values (8, 'JE Ref#: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (9, 'JE(J) or ACCR(A): ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                      "insert into #temptable values (10, 'Reversing: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (11, ' ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (12, 'Description: Clear suspense activity', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (13, ' ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                      "insert into #temptable values (14, ' ', 'Entity', 'Dept', 'SubAcct', /* '', '', */ 'Description', /* '', '', */ 'Debit', 'Credit' )  " & _
                      "select * from #temptable order by lblID " & _
                      "drop table #temptable ", conn)
                        ' Fill the DataTable with data from SQL Server table.
                        da.Fill(dt)

                        Dim da2 As New SqlDataAdapter("select " & _
               "15 as lblID, '' as lblDesc, " & _
               "case when Fac = 'A' then '10' " & _
               "when Fac = 'C' then '22' " & _
               "when Fac = 'F' then '06' " & _
               "when Fac = 'D' then '40' " & _
               "when Fac = 'L' then '30' " & _
               "else Fac " & _
               "End Entity, " & _
                  "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                          "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                          "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                          "else convert(varchar(5),Dept)  " & _
                          "End  Dept, " & _
                          "case  " & _
                          "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                          "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                          "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                          "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                          "else convert(varchar(5), SubAcct  ) " & _
                          "end   SubAcct,  " & _
               "/* '' as Spacer1, '' as Spacer2,  */ " & _
               "case when ID is null then 'Total' else Description end Description, " & _
               "/* '' as Spacer3, '' as Spacer4, */ " & _
               "Debit, Credit  " & _
               " from " & _
               "(select " & _
               "x.ID, x.FY, " & _
               "case when Grouping(x.Fac) = 1 then 'Total' " & _
               "else Fac " & _
               "   end Fac,  " & _
               "x.Dept,  " & _
               "x.SubAcct,  " & _
               " 'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description, " & _
               "  sum(x.Debit) as Debit , " & _
               "   sum(x.Credit) as Credit       " & _
               "  from  " & _
               "( select  " & _
               " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate        " & _
               "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
               "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
               " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null  and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
               "  and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
               " and a.Dept is not null and a.Dept <> 9999  " & _
               " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
               "and a.Fac = 'C'  and GLEffectiveDate = '" & GLEffectiveDate & "'  " & _
               " union  " & _
               "  select  " & _
               " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate         " & _
               "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
               "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
               " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null  and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
               "  and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
               " and a.Dept is not null  and a.Dept <> 9999 " & _
               " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
               "and a.Fac = 'C'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
               " ) x  " & _
               "group by Fac, Dept, SubAcct, ID, FY,  Pat_Acct_Fac,   TranDate  with rollup  ) y  " & _
               "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
               "or (y.ID is null and Fac is not null  " & _
               "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
               "order by Fac, ID desc  , Dept desc  ", conn)

                        ' Fill the DataTable with data from SQL Server table.
                        da2.Fill(dt)


                        'update records in Corrections table 
                        For j As Integer = 0 To gvCherokee.Rows.Count - 1
                            FARGLDID = gvCherokee.Rows(j).Cells(10).Text
                            FARGLDYr = gvCherokee.Rows(j).Cells(11).Text

                            If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                                If SQLWhere.Contains(FARGLDID) = False Then
                                    SQLWhere = SQLWhere & " ( FAC = 'C' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                                End If
                            End If
                        Next

                        If SQLWhere <> "" Then
                            SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                            SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                                  "iscorrected = sysdatetime() " & _
                                  SQLWhere

                            cmd = New SqlCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.ExecuteReader()
                        End If

                    Case "For"
                        Dim da As New SqlDataAdapter("select 1 as lblID, " & _
                          "'Forsyth Northside Hospital Journal Entry' as lblDesc ,  cast('' as varchar) as Entity,  cast('' as varchar) as Dept,  " & _
                          "cast('' as varchar) as SubAcct, /* cast('' as varchar) as Spacer1,  cast('' as varchar) as Spacer2, */ cast('' as varchar) as Description,  " & _
                          "/* cast('' as varchar) as Spacer3,  cast('' as varchar) as Spacer4, */  cast('' as varchar) as Debit,  cast('' as varchar) as Credit  into #temptable   " & _
                          "insert into #temptable values (2, ' ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                          "insert into #temptable values (3, 'Entity: 6', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (4, 'Date: ' + cast( CONVERT(date,getdate()) as varchar), '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                          "insert into #temptable values (5, 'Period: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (6, 'Year: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                          "insert into #temptable values (7, 'Batch: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (8, 'JE Ref#: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (9, 'JE(J) or ACCR(A): ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (10, 'Reversing: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (11, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (12, 'Description: Clear suspense activity', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                          "insert into #temptable values (13, ' ', '', '', '', '', '', '' /*, '', '', '', '' */ )  " & _
                          "insert into #temptable values (14, ' ', 'Entity', 'Dept', 'SubAcct', /*'', '', */ 'Description', /*'', '', */ 'Debit', 'Credit' )  " & _
                          "select * from #temptable order by lblID " & _
                          "drop table #temptable ", conn)
                        ' Fill the DataTable with data from SQL Server table.
                        da.Fill(dt)

                        Dim da2 As New SqlDataAdapter("select " & _
               "15 as lblID, '' as lblDesc, " & _
               "case when Fac = 'A' then '10' " & _
               "when Fac = 'C' then '22' " & _
               "when Fac = 'F' then '06' " & _
               "when Fac = 'D' then '40' " & _
               "when Fac = 'L' then '30' " & _
               "else Fac " & _
               "End Entity, " & _
                "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                          "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                          "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                          "else convert(varchar(5),Dept)  " & _
                          "End  Dept, " & _
                          "case  " & _
                          "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                          "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                          "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                          "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                          "else convert(varchar(5), SubAcct  ) " & _
                          "end   SubAcct,  " & _
               "/* '' as Spacer1, '' as Spacer2, */ " & _
               "case when ID is null then 'Total' else Description end Description, " & _
               "/* '' as Spacer3, '' as Spacer4, */" & _
               "Debit, Credit  " & _
               " from " & _
               "(select " & _
               "x.ID, x.FY, " & _
               "case when Grouping(x.Fac) = 1 then 'Total' " & _
               "else Fac " & _
               "   end Fac,  " & _
               "x.Dept,  " & _
               "x.SubAcct,  " & _
               " 'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description, " & _
               "  sum(x.Debit) as Debit , " & _
               "   sum(x.Credit) as Credit       " & _
               "  from  " & _
               "( select  " & _
               " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate        " & _
               "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
               "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
               " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
               "  and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
               " and a.Dept is not null and a.Dept <> 9999  " & _
               " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
               "and a.Fac = 'F'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
               " union  " & _
               "  select  " & _
               " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate         " & _
               "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
               "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
               " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
               "  and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
               " and a.Dept is not null  and a.Dept <> 9999 " & _
               " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
               "and a.Fac = 'F'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
               " ) x  " & _
               "group by Fac, Dept, SubAcct, ID, FY,  Pat_Acct_Fac,   TranDate  with rollup  ) y  " & _
               "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
               "or (y.ID is null and Fac is not null  " & _
               "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
               "order by Fac, ID desc  , Dept desc  ", conn)

                        ' Fill the DataTable with data from SQL Server table.
                        da2.Fill(dt)

                        'update records in Corrections table 
                        For j As Integer = 0 To gvForsyth.Rows.Count - 1
                            FARGLDID = gvForsyth.Rows(j).Cells(10).Text
                            FARGLDYr = gvForsyth.Rows(j).Cells(11).Text

                            If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                                If SQLWhere.Contains(FARGLDID) = False Then
                                    SQLWhere = SQLWhere & " ( FAC = 'F' and active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                                End If
                            End If
                        Next

                        If SQLWhere <> "" Then
                            SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                            SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                                  "iscorrected = sysdatetime() " & _
                                   SQLWhere
                            cmd = New SqlCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.ExecuteReader()
                        End If

                    Case "Duluth"
                        Dim da As New SqlDataAdapter("select 1 as lblID, " & _
                        "'Duluth Northside Hospital Journal Entry' as lblDesc ,  cast('' as varchar) as Entity,  cast('' as varchar) as Dept,  " & _
                        "cast('' as varchar) as SubAcct, /* cast('' as varchar) as Spacer1,  cast('' as varchar) as Spacer2, */  cast('' as varchar) as Description,  " & _
                        "/* cast('' as varchar) as Spacer3,  cast('' as varchar) as Spacer4, */  cast('' as varchar) as Debit,  cast('' as varchar) as Credit  into #temptable   " & _
                        "insert into #temptable values (2, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (3, 'Entity: 40', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (4, 'Date: ' + cast( CONVERT(date,getdate()) as varchar), '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (5, 'Period: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (6, 'Year: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (7, 'Batch: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (8, 'JE Ref#: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (9, 'JE(J) or ACCR(A): ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (10, 'Reversing: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (11, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (12, 'Description: Clear suspense activity', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (13, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (14, ' ', 'Entity', 'Dept', 'SubAcct', /*'', '', */ 'Description', /* '', '', */ 'Debit', 'Credit' )  " & _
                        "select * from #temptable order by lblID " & _
                        "drop table #temptable ", conn)
                        ' Fill the DataTable with data from SQL Server table.
                        da.Fill(dt)

                        'Dim da2 As New SqlDataAdapter("select " & _
                        '"15 as lblID, '' as lblDesc, " & _
                        '"case when Fac = 'A' then '10' " & _
                        '"when Fac = 'C' then '22' " & _
                        '"when Fac = 'F' then '6' " & _
                        '"else Fac " & _
                        '"End Entity, " & _
                        '"case when Dept is null then '' else Dept end Dept  ," & _
                        '"case when SubAcct is Null then '' else SubAcct end SubAcct, " & _
                        '"'' as Spacer1, '' as Spacer2, " & _
                        '"case when ID is null then 'Total' else Description end Description, " & _
                        '"'' as Spacer3, '' as Spacer4, " & _
                        '"Debit, Credit  " & _
                        '" from " & _
                        '"(select " & _
                        '"x.ID, x.FY, " & _
                        '"case when Grouping(x.Fac) = 1 then 'Total' " & _
                        '"else Fac " & _
                        '"   end Fac,  " & _
                        '"x.Dept,  " & _
                        '"x.SubAcct,  " & _
                        '" 'PA Daily Journal Entry - Clear Suspense Activity' as  Description , " & _
                        '"  sum(x.Debit) as Debit , " & _
                        '"   sum(x.Credit) as Credit       " & _
                        '"  from  " & _
                        '"( select  " & _
                        '" b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY   " & _
                        '"  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                        '"  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                        '" where a.ISCORRECTED is null  " & _
                        '" and a.Dept is not null and a.Dept <> 9999  " & _
                        '" and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                        '"and a.Fac = 'A' " & _
                        '" union  " & _
                        '"  select  " & _
                        '" b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY    " & _
                        '"  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                        '"  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                        '" where a.ISCORRECTED is null  " & _
                        '" and a.Dept is not null  and a.Dept <> 9999 " & _
                        '" and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                        '"and a.Fac = 'A' " & _
                        '" ) x  " & _
                        '"group by Fac, Dept, SubAcct, ID, FY with rollup  ) y  " & _
                        '"where (y.ID is not null and FY is not null)  " & _
                        '"or (y.ID is null and Fac is not null  " & _
                        '"   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                        '"order by Fac, ID desc  , Dept desc  ", conn)

                        Dim da2 As New SqlDataAdapter("select " & _
                   "15 as lblID, '' as lblDesc, " & _
                   "case when Fac = 'A' then '10' " & _
                   "when Fac = 'C' then '22' " & _
                   "when Fac = 'F' then '06' " & _
                   "when Fac = 'D' then '40' " & _
                   "when Fac = 'L' then '30' " & _
                   "else Fac " & _
                   "End Entity, " & _
                   "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                   "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                   "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                   "else convert(varchar(5),Dept)  " & _
                   "End  Dept, " & _
                   "case  " & _
                   "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                   "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                   "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                   "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                   "else convert(varchar(5), SubAcct  ) " & _
                   "end   SubAcct,  " & _
                   "/* '' as Spacer1, '' as Spacer2, */ " & _
                   "case when ID is null then 'Total' else Description end Description, " & _
                   "/* '' as Spacer3, '' as Spacer4, */ " & _
                   "Debit, Credit  " & _
                   " from " & _
                   "(select " & _
                   "x.ID, x.FY, " & _
                   "case when Grouping(x.Fac) = 1 then 'Total' " & _
                   "else Fac " & _
                   "   end Fac,  " & _
                   "x.Dept,  " & _
                   "x.SubAcct,  " & _
                   " 'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description, " & _
                   "  sum(x.Debit) as Debit , " & _
                   "   sum(x.Credit) as Credit       " & _
                   "  from  " & _
                   "( select  " & _
                   " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate        " & _
                   "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                   "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                   " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                   " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                   " and a.Dept is not null and a.Dept <> 9999  " & _
                   " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                   "and a.Fac = 'D'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                   " union  " & _
                   "  select  " & _
                   " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate         " & _
                   "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                   "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                   " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                   " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                   " and a.Dept is not null  and a.Dept <> 9999 " & _
                   " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                   "and a.Fac = 'D'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                   " ) x  " & _
                   "group by Fac, Dept, SubAcct, ID, FY,  Pat_Acct_Fac,   TranDate  with rollup  ) y  " & _
                   "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                   "or (y.ID is null and Fac is not null  " & _
                   "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                   "order by Fac, ID desc  , Dept desc  ", conn)

                        ' Fill the DataTable with data from SQL Server table.
                        da2.Fill(dt)


                        'update records in Corrections table 
                        For j As Integer = 0 To gvDuluth.Rows.Count - 1
                            FARGLDID = gvDuluth.Rows(j).Cells(10).Text
                            FARGLDYr = gvDuluth.Rows(j).Cells(11).Text

                            If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                                If SQLWhere.Contains(FARGLDID) = False Then
                                    SQLWhere = SQLWhere & " ( FAC = 'D' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                                End If
                            End If
                        Next

                        If SQLWhere <> "" Then
                            SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                            SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                                "iscorrected = sysdatetime() " & _
                                 SQLWhere


                            cmd = New SqlCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.ExecuteReader()
                        End If

                    Case "Gwinnett"
                        Dim da As New SqlDataAdapter("select 1 as lblID, " & _
                        "'Gwinnett Northside Hospital Journal Entry' as lblDesc ,  cast('' as varchar) as Entity,  cast('' as varchar) as Dept,  " & _
                        "cast('' as varchar) as SubAcct, /* cast('' as varchar) as Spacer1,  cast('' as varchar) as Spacer2, */ cast('' as varchar) as Description,  " & _
                        " /* cast('' as varchar) as Spacer3,  cast('' as varchar) as Spacer4, */  cast('' as varchar) as Debit,  cast('' as varchar) as Credit  into #temptable   " & _
                        "insert into #temptable values (2, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (3, 'Entity: 30', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (4, 'Date: ' + cast( CONVERT(date,getdate()) as varchar), '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (5, 'Period: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (6, 'Year: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (7, 'Batch: ', '', '', '', '', '', '' /*, '', '', '', ''*/ )  " & _
                        "insert into #temptable values (8, 'JE Ref#: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (9, 'JE(J) or ACCR(A): ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (10, 'Reversing: ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (11, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (12, 'Description: Clear suspense activity', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (13, ' ', '', '', '', '', '', '' /*, '', '', '', '' */)  " & _
                        "insert into #temptable values (14, ' ', 'Entity', 'Dept', 'SubAcct', /*'', '', */ 'Description', /*'', '', */ 'Debit', 'Credit' )  " & _
                        "select * from #temptable order by lblID " & _
                        "drop table #temptable ", conn)
                        ' Fill the DataTable with data from SQL Server table.
                        da.Fill(dt)

                        'Dim da2 As New SqlDataAdapter("select " & _
                        '"15 as lblID, '' as lblDesc, " & _
                        '"case when Fac = 'A' then '10' " & _
                        '"when Fac = 'C' then '22' " & _
                        '"when Fac = 'F' then '6' " & _
                        '"else Fac " & _
                        '"End Entity, " & _
                        '"case when Dept is null then '' else Dept end Dept  ," & _
                        '"case when SubAcct is Null then '' else SubAcct end SubAcct, " & _
                        '"'' as Spacer1, '' as Spacer2, " & _
                        '"case when ID is null then 'Total' else Description end Description, " & _
                        '"'' as Spacer3, '' as Spacer4, " & _
                        '"Debit, Credit  " & _
                        '" from " & _
                        '"(select " & _
                        '"x.ID, x.FY, " & _
                        '"case when Grouping(x.Fac) = 1 then 'Total' " & _
                        '"else Fac " & _
                        '"   end Fac,  " & _
                        '"x.Dept,  " & _
                        '"x.SubAcct,  " & _
                        '" 'PA Daily Journal Entry - Clear Suspense Activity' as  Description , " & _
                        '"  sum(x.Debit) as Debit , " & _
                        '"   sum(x.Credit) as Credit       " & _
                        '"  from  " & _
                        '"( select  " & _
                        '" b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY   " & _
                        '"  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                        '"  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                        '" where a.ISCORRECTED is null  " & _
                        '" and a.Dept is not null and a.Dept <> 9999  " & _
                        '" and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                        '"and a.Fac = 'A' " & _
                        '" union  " & _
                        '"  select  " & _
                        '" b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY    " & _
                        '"  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
                        '"  join DWH.dbo.FARGLD b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                        '" where a.ISCORRECTED is null  " & _
                        '" and a.Dept is not null  and a.Dept <> 9999 " & _
                        '" and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                        '"and a.Fac = 'A' " & _
                        '" ) x  " & _
                        '"group by Fac, Dept, SubAcct, ID, FY with rollup  ) y  " & _
                        '"where (y.ID is not null and FY is not null)  " & _
                        '"or (y.ID is null and Fac is not null  " & _
                        '"   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                        '"order by Fac, ID desc  , Dept desc  ", conn)

                        Dim da2 As New SqlDataAdapter("select " & _
                   "15 as lblID, '' as lblDesc, " & _
                   "case when Fac = 'A' then '10' " & _
                   "when Fac = 'C' then '22' " & _
                   "when Fac = 'F' then '06' " & _
                   "when Fac = 'D' then '40' " & _
                   "when Fac = 'L' then '30' " & _
                   "else Fac " & _
                   "End Entity, " & _
                   "case when Dept = '9999' then convert(varchar(5), '09999')  " & _
                   "when LEN(Dept) = 3 then '00' + CONVERT(varchar(5), Dept)  " & _
                   "when LEN(Dept) = 4 then '0' + CONVERT(varchar(5), Dept)  " & _
                   "else convert(varchar(5),Dept)  " & _
                   "End  Dept, " & _
                   "case  " & _
                   "when LEN(SubAcct) = 1 then '0000' + convert(varchar(5), SubAcct) " & _
                   "when LEN(SubAcct) = 2 then '000' + convert(varchar(5),SubAcct) " & _
                   "when LEN(SubAcct) = 3 then '00' + convert(varchar(5),SubAcct) " & _
                   "when LEN(SubAcct) = 4 then '0' + convert(varchar(5),SubAcct  ) " & _
                   "else convert(varchar(5), SubAcct  ) " & _
                   "end   SubAcct,  " & _
                   "/* '' as Spacer1, '' as Spacer2, */ " & _
                   "case when ID is null then 'Total' else Description end Description, " & _
                   "/* '' as Spacer3, '' as Spacer4, */ " & _
                   "Debit, Credit  " & _
                   " from " & _
                   "(select " & _
                   "x.ID, x.FY, " & _
                   "case when Grouping(x.Fac) = 1 then 'Total' " & _
                   "else Fac " & _
                   "   end Fac,  " & _
                   "x.Dept,  " & _
                   "x.SubAcct,  " & _
                   " 'Suspense Reclass - ' + x.Pat_Acct_Fac + ' - ' + cast(cast(x.TranDate  as date) as varchar(max)) as  Description, " & _
                   "  sum(x.Debit) as Debit , " & _
                   "   sum(x.Credit) as Credit       " & _
                   "  from  " & _
                   "( select  " & _
                   " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate        " & _
                   "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                   "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                   " where isnull(a.Active, 1) = 1 and  a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                   " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                   " and a.Dept is not null and a.Dept <> 9999  " & _
                   " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
                   "and a.Fac = 'L'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                   " union  " & _
                   "  select  " & _
                   " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY,   a.Pat_Acct_Fac  ,  a.TranDate         " & _
                   "  from DWH.GL.FARGLD_CORRECTIONS_DS a  " & _
                   "  join DWH.GL.FARGLD_DS b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
                   " where isnull(a.Active, 1) = 1 and a.ISCORRECTED is null and (a.IgnoreRecord is null or a.IgnoreRecord = '0' ) " & _
                   " and (a.HoldRecord is null or a.HoldRecord = '0' ) " & _
                   " and a.Dept is not null  and a.Dept <> 9999 " & _
                   " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
                   "and a.Fac = 'L'  and GLEffectiveDate = '" & GLEffectiveDate & "' " & _
                   " ) x  " & _
                   "group by Fac, Dept, SubAcct, ID, FY,  Pat_Acct_Fac,   TranDate  with rollup  ) y  " & _
                   "where (y.ID is not null and FY is not null and y.Description is not null)  " & _
                   "or (y.ID is null and Fac is not null  " & _
                   "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
                   "order by Fac, ID desc  , Dept desc  ", conn)

                        ' Fill the DataTable with data from SQL Server table.
                        da2.Fill(dt)


                        'update records in Corrections table 
                        For j As Integer = 0 To gvGwinnett.Rows.Count - 1
                            FARGLDID = gvGwinnett.Rows(j).Cells(10).Text
                            FARGLDYr = gvGwinnett.Rows(j).Cells(11).Text

                            If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
                                If SQLWhere.Contains(FARGLDID) = False Then
                                    SQLWhere = SQLWhere & " ( FAC = 'L' and Active = 1 and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
                                End If
                            End If
                        Next

                        If SQLWhere <> "" Then
                            SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

                            SQL = "update DWH.GL.FARGLD_CORRECTIONS_DS set " & _
                                "iscorrected = sysdatetime() " & _
                                 SQLWhere


                            cmd = New SqlCommand(SQL, conn)
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            cmd.ExecuteReader()
                        End If
                End Select

            End Using

            Return dt

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Function
    Protected Sub ExportToExcel(strConn As String, dtSQL As DataTable)

        Try


            Using connOLEDB As New OleDbConnection(strConn)
                ' Create a new sheet in the Excel spreadsheet.
                Dim cmd As New OleDbCommand("create table JEEntry(lblDesc varchar(255), Entity varchar(50), Department varchar(50),SubAccount varchar(255), /* Spacer1 varchar(1), Spacer2 varchar(1), */ Description varchar(255), /* Spacer3 varchar(1), Spacer4 varchar(1), */ Debit varchar(255), Credit varchar(255))", connOLEDB)

                ' Open the connection.
                If connOLEDB.State = ConnectionState.Closed Then
                    connOLEDB.Open()
                End If

                ' Execute the OleDbCommand.
                cmd.ExecuteNonQuery()

                'cmd.CommandText = "INSERT INTO JEEntry (lblDesc, EntitY, Department, SubAccount, Spacer1, Spacer2, Description, Spacer3, Spacer4, Debit, Credit) values (?,?,?,?,?,?,?, ?,?,?,? )"
                cmd.CommandText = "INSERT INTO JEEntry (lblDesc, EntitY, Department, SubAccount,  Description, Debit, Credit) values (?,?,?,?,?,?,?)"

                ' Add the parameters.
                '  cmd.Parameters.Add("lblID", OleDbType.Integer, 255, "lblID")
                cmd.Parameters.Add("lblDesc", OleDbType.VarChar, 255, "lblDesc")
                cmd.Parameters.Add("Entity", OleDbType.VarChar, 50, "Entity")
                cmd.Parameters.Add("Department", OleDbType.VarChar, 50, "Dept")
                cmd.Parameters.Add("SubAccount", OleDbType.VarChar, 255, "SubAcct")
                'cmd.Parameters.Add("Spacer", OleDbType.VarChar, 1, "Spacer1")
                'cmd.Parameters.Add("Spacer2", OleDbType.VarChar, 1, "Spacer2")
                cmd.Parameters.Add("Description", OleDbType.VarChar, 255, "Description")
                'cmd.Parameters.Add("Spacer3", OleDbType.VarChar, 1, "Spacer3")
                'cmd.Parameters.Add("Spacer4", OleDbType.VarChar, 1, "Spacer4")
                cmd.Parameters.Add("Debit", OleDbType.VarChar, 255, "Debit")
                'cmd.Parameters.Add("Credit", OleDbType.VarChar, 255, "Credit")
                cmd.Parameters.Add("Credit", OleDbType.VarChar, 255, "Credit")

                ' Initialize an OleDBDataAdapter object.
                Dim da As New OleDbDataAdapter("select * from JEEntry  ", connOLEDB)

                ' Set the InsertCommand of OleDbDataAdapter, 
                ' which is used to insert data.
                da.InsertCommand = cmd

                ' Changes the Rowstate()of each DataRow to Added,
                ' so that OleDbDataAdapter will insert the rows.
                For Each dr As DataRow In dtSQL.Rows
                    dr.SetAdded()
                Next

                ' Insert the data into the Excel spreadsheet.
                da.Update(dtSQL)

            End Using



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub LoadAvailableList()
        Try
            ddlFiles.Items.Clear()
            Dim dr As SqlDataReader
            Dim SQLFac As String = ""

            If chbAtlanta.Checked = True Then
                SQLFac = "'A',"
            End If
            If chbCherokee.Checked = True Then
                SQLFac = SQLFac & "'C',"
            End If
            If chbForsyth.Checked = True Then
                SQLFac = SQLFac & "'F',"
            End If
            If chbDuluth.Checked = True Then
                SQLFac = SQLFac & "'D',"
            End If
            If chbGwinnett.Checked = True Then
                SQLFac = SQLFac & "'L',"
            End If
            If SQLFac <> "" Then
                SQLFac = "and FAC in (" & SQLFac.Substring(0, SQLFac.Length - 1) & ") "


                SQL = "Select " & _
                "GLEffectiveDate, Fac " & _
                ",(FAC + '-' + convert(varchar(10),GLEFFEctiveDate)) as ddlItem " & _
                "from DWH.GL.FARGLD_CORRECTIONS_DS  " & _
                "where isnull(Active, 1) = 1 and GLEffectiveDate is not null  " & _
                "and IgnoreRecord = 0  " & _
                "and HoldRecord = 0  " & _
                "and ISCORRECTED is null " & _
                "/*and GLEffectiveDate >= dateadd(month, -3, getdate()) */ " & _
                 SQLFac & _
                "group by GLEffectiveDate, Fac order by GLEffectiveDate desc  "

                cmd = New SqlCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("ddlItem")) Then
                    Else
                        ddlFiles.Items.Add(dr.Item("ddlItem"))
                    End If

                End While
                dr.Close()

                If ddlFiles.Items.Count > 0 Then
                    lblFiles.Visible = True
                    btnGenerateFiles.Visible = True
                    ddlFiles.Visible = True
                    lblFiles.Text = "List of un-finailized entries. (Fac-Date)"
                Else
                    lblFiles.Text = "No data ready to export"
                    lblFiles.Visible = True
                    btnGenerateFiles.Visible = False
                    ddlFiles.Visible = False
                End If

            Else
                lblFiles.Visible = False
                btnGenerateFiles.Visible = False
                ddlFiles.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnGenerateList_Click(sender As Object, e As System.EventArgs) Handles btnGenerateList.Click
        Try
            LoadAvailableList()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


End Class