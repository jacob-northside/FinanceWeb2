
Imports System.Xml
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Data
Imports FinanceWeb.WebFinGlobal
Imports System.IO
Imports DocumentFormat.OpenXml

Imports System.Configuration
Imports System.Collections
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System

Imports System.Drawing
Imports ClosedXML.Excel

Public Class Test_Page_Samurai

    Inherits System.Web.UI.Page
    Public Shared gridname As String
    Public Shared galenselectedrow As String
    Public Shared galenchosenrow As String
    Public Shared y As Integer
    Public Shared postFS As String
    Public Shared tennis As DataSet
    Public Shared tennis2 As DataSet
    Public Shared Galenset As DataSet





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Exit Sub
        End If

        Try

            gridname = "0"
            tennis = New DataSet
            tennis2 = New DataSet
            Galenset = New DataSet
            ' ddlSelectedFS.Style(HtmlTextWriterStyle.Display) = "none"

            Dim Sql2 As String
            Dim ds As New DataSet
            Dim da As SqlDataAdapter

            Try
                Sql2 = "SELECT TOP 100 [FAC]" & _
          ",[INSPLAN]" & _
          ",[IDGroup]" & _
          ",[INSPLAN_NAME]" & _
          ",[STARINSPLANCODE]" & _
          ",[CONTRACTID]" & _
          ",[CONTRACT_NAME]" & _
          ",[CONTRACTID_SUM]" & _
          ",[CONTRACTID_SUM_NAME]" & _
               " FROM [DWH].[dbo].[ContractID_Galen_LU] " & _
               " where(STARINSPLANCODE Is null)" & _
             "order by 2"


                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da = New SqlDataAdapter(Sql2, conn2)
                    da.Fill(ds, "DEMO")
                    gvGalenBlanks.DataSource = ds
                    gvGalenBlanks.DataMember = "DEMO"
                    gvGalenBlanks.DataBind()

                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

            'Dim ds As DataSet
            Dim alpha As Integer
            Dim stupidsql As String
            Dim x As String

            x = "notfilled"
            stupidsql = "SELECT case when COUNT(*) > 100 then 100 else COUNT(*) end as cnt FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null" & _
                " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')"



            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim dc As SqlCommand
                dc = New SqlCommand(stupidsql, conn2)
                Try
                    conn2.Open()
                    x = Convert.ToInt16(dc.ExecuteScalar())
                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

            End Using
            alpha = 0
            galenselectedrow = 0
            While alpha < CInt(x)
                If alpha = 0 Then
                    gvGalenBlanks.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                End If
                If alpha = CInt(galenselectedrow) Then
                    alpha = alpha + 1
                ElseIf alpha Mod 2 = 0 Then
                    gvGalenBlanks.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                    alpha = alpha + 1
                Else
                    gvGalenBlanks.Rows(alpha).BackColor = System.Drawing.Color.White
                    alpha = alpha + 1
                End If
            End While
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub


    'Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs)

    '    If (e.Row.RowType = DataControlRowType.DataRow) Then
    '        For Each row As GridViewRow In GridView1.Rows
    '            row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, String.Format("rowSelect"))
    '        Next

    '    End If


    'End Sub

    Protected Sub ddlSelectedPayor_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedPayor.SelectedIndexChanged

        'window.open("http://nshdsweb/FinanceWeb/Tools/ManagedCare/Profee/ProFeeInstructions.aspx", "ProFeeInstructions", "height=768,width=800, scrollbars, resizable");

        Try

            Dim Sql2 As String
            'Dim ds As DataSet
            Dim da As SqlDataAdapter
            Dim dr As DataRow

            If ddlSelectedPayor.SelectedValue.ToString = "novalueselected" Then
                ddlSelectedFS.Enabled = False
                ddlSelectedTINs.Enabled = False
                ' ddlSelectedFS.Style(HtmlTextWriterStyle.Display) = "none"
                Exit Sub
            Else
                ddlSelectedFS.Enabled = True
                ddlSelectedTINs.Enabled = True
                ' ddlSelectedFS.Style(HtmlTextWriterStyle.Display) = DropDownList.DisabledCssClass

                'Sql2 = "select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                '        "where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "'  and (FeeSchedName = '" & _
                '        ddlSelectedFS.SelectedValue.ToString & "' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')"
                'testinglabel1.Text = Sql2
                'testinglabel1.DataBind()
                ddlSelectedFS.SelectedIndex = 0
                ddlSelectedTINs.SelectedIndex = 0
                Try
                    Sql2 = "select PayorName, FeeSchedName, convert(varchar, EffectiveDate, 107) as [Effective Date], convert(varchar, EndDate, 107) as [End Date], [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                            "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                            Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                            " group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] order by FeeSchedName, PayorName, EffectiveDate desc, EndDate desc, LoadDate desc," & _
                            " [Default], [ManagersNotes]"

                    tennis.Clear()
                    'ds = New DataSet


                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da = New SqlDataAdapter(Sql2, conn2)
                        da.Fill(tennis, "DEMO")
                        GridView1.DataSource = tennis
                        GridView1.DataMember = "DEMO"
                        GridView1.DataBind()
                        ScrollPanel.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
                        ScrollPanel.BorderWidth = "1"
                        dr = tennis.Tables(0).Rows(0)
                        txtCurrentED.Text = dr("Effective Date").ToString
                        txtCurrentED.DataBind()
                        txtCurrentDefault.Text = dr("Default").ToString
                        txtCurrentDefault.DataBind()
                        txtCurrentEndD.Text = dr("End Date").ToString
                        txtCurrentEndD.DataBind()
                        txtCurrentMN.Text = dr("ManagersNotes").ToString
                        txtCurrentMN.DataBind()
                        txtPayorName.Text = dr("PayorName").ToString
                        txtPayorName.DataBind()
                        txtFSName.Text = dr("FeeSchedName").ToString
                        txtFSName.DataBind()


                        FakelblStartDate.Text = dr("Effective Date").ToString
                        FakelblStartDate.DataBind()
                        FakelblDefault.Text = dr("Default").ToString
                        FakelblDefault.DataBind()
                        FakelblEndDate.Text = dr("End Date").ToString
                        FakelblEndDate.DataBind()
                        FakelblManagersNotes.Text = dr("ManagersNotes").ToString
                        FakelblManagersNotes.DataBind()
                        FakelblPayorName.Text = dr("PayorName").ToString
                        FakelblPayorName.DataBind()
                        FakelblFSName.Text = dr("FeeSchedName").ToString
                        FakelblFSName.DataBind()

                    End Using


                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try
            End If

            Dim x As String
            Dim stupidsql As String
            'Dim dt As DataTable
            'Dim ds As DataSet
            x = "0"
            stupidsql = "Select count(*) from (select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                        "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                        Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                        " group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes]) a"



            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim dc As SqlCommand
                dc = New SqlCommand(stupidsql, conn2)
                Try
                    conn2.Open()
                    x = Convert.ToString(dc.ExecuteScalar())
                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

            End Using

            If CInt(x) > 10 Then
                ScrollPanel.Height = "350"
            ElseIf CInt(x) > 5 Then
                ScrollPanel.Height = "200"
            ElseIf CInt(x) > 3 Then
                ScrollPanel.Height = "150"
            Else
                ScrollPanel.Height = "100"
            End If
            ScrollPanel.DataBind()



            Dim dq As SqlCommand
            Dim countsql As String
            'Dim y As Integer

            countsql = "Select COUNT(distinct FeeSchedID) from DWH.ProFee.FeeSchedule fs where " & _
            "exists(select * from (" & _
            "Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
            ",[LoadDate] desc, [Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] " & _
            ",[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule where PayorName = '" & _
            Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
            "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
            " Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName" & _
            ") a where rankin = " & 1 & " and a.[Default] = fs.[Default] and a.EffectiveDate = fs.EffectiveDate " & _
            "and a.EndDate = fs.EndDate and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName )"

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                dq = New SqlCommand(countsql, conn2)
                Try
                    conn2.Open()
                    y = Convert.ToInt16(dq.ExecuteScalar())
                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

            End Using
            CountLabel.Text = ""
            If y > 1 Then
                CountLabel.Text = "There are " & y.ToString & " fee schedules which meet the above criteria.  Which " & _
                    "one would you like to update?"



                Dim grid2sql As String

                'Version(one)
                grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, case when CPTRank = 1 then Replace(Replace(TIN, '||', ' '), '|', '') else NULL end as TINs," & _
                        "CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                        "from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                        "left join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID  " & _
                        "where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
                        " ,[LoadDate] desc,[Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                        "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
                        "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                        "Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                        "where(rankin = " & 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                        "and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null or fsd.FeeSchedID is null)" & _
                        "and TIN = (select MAX(TIN) from DWH.ProFee.FeeSchedule where FeeSchedID = fs.FeeSchedID)) b where(CPTRank <= 10) " & _
                        "order by FSRank asc, CPT asc, Modifier asc"

                '    'Version two:
                '    grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, TIN," & _
                '"CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                '"from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                '"join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
                '",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                '"where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
                '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')" & _
                '"Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                '"where(rankin = " & 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                '"and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null)) b where(CPTRank <= 10) " & _
                '"order by FSRank asc, CPT asc, Modifier asc"

                Try

                    Dim da2 As New SqlDataAdapter
                    Dim dss As New DataSet

                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da2 = New SqlDataAdapter(grid2sql, conn2)
                        da2.Fill(dss, "DEMO")
                        GridView2.DataSource = dss
                        GridView2.DataMember = "DEMO"
                        GridView2.DataBind()
                        GridView2.Visible = True
                        redpanel.Visible = True
                        UpdateButton.Enabled = False
                        PanelRed.Visible = True
                        PanelRed.BorderColor = System.Drawing.Color.Red



                        Dim alpha As Int16 = 0
                        While alpha < (y) * 10
                            If Fix(alpha / 10) Mod 2 = 0 Then
                                GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                                alpha = alpha + 1
                            Else
                                GridView2.Rows(alpha).BackColor = System.Drawing.Color.White

                                alpha = alpha + 1
                            End If
                        End While
                    End Using


                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try
            Else
                'GridView2.Columns.Clear()
                'GridView2.DataBind()
                GridView2.Visible = False
                redpanel.Visible = False
                UpdateButton.Enabled = True
                PanelRed.Visible = False
            End If
            CountLabel.DataBind()
            If x > 0 Then
                GridView1.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            End If

            CurrentFSPanel.Visible = True

            Dim currentfssql As String

            'currentfssql = "select FeeSchedID, TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
            'ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
            '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ') and '" & _
            'txtCurrentED.Text.ToString & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & txtCurrentEndD.Text.ToString & _
            '"' and [Default] = '" & txtCurrentDefault.Text.ToString & "' and ManagersNotes = '" & _
            'txtCurrentMN.Text.ToString & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"

            currentfssql = "        DECLARE @concatenated NVARCHAR(1000) " & _
            "SET @concatenated = '' " & _
            "SELECT @concatenated = @concatenated + '|' + TIN + '|' FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, Dense_Rank() over (order by LoadDate desc) as den from DWH.ProFee.FeeSchedule FeeSchedID  where PayorName = '" & _
            Replace(txtPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(txtFSName.Text.ToString, "'", "''") & _
            "') and '" & _
            Replace(txtCurrentED.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(txtCurrentEndD.Text.ToString, "'", "''") & _
            "' and [Default] = '" & Replace(txtCurrentDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
            Replace(txtCurrentMN.Text.ToString, "'", "''") & "' ) x where den = 1 ) order by TIN, FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]" & _
            "SELECT top 1 FeeSchedID, LoadDate, left(@concatenated, LEN(@concatenated)) as TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
            Replace(txtPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(txtFSName.Text.ToString, "'", "''") & "') and '" & _
            Replace(txtCurrentED.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(txtCurrentEndD.Text.ToString, "'", "''") & _
             "' and [Default] = '" & Replace(txtCurrentDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
            Replace(txtCurrentMN.Text.ToString, "'", "''") & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"

            Dim dfs As DataSet
            Dim dfa As New SqlDataAdapter
            Dim cmd As New SqlCommand
            Dim dr2 As DataRow

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dfs = New DataSet
                cmd = New SqlCommand(currentfssql, conn)
                dfa.SelectCommand = cmd
                dfa.SelectCommand.CommandTimeout = 86400
                dfa.Fill(dfs, "TIns")
                dr2 = dfs.Tables(0).Rows(0)
                fakelabelTIN.Text = dr2("TIN").ToString
                lblPreviousTIN.Text = Replace(Replace(dr2("TIN").ToString, "||", ", "), "|", "")
                lblPreviousTIN.DataBind()
                postFS = dr2("FeeSchedID").ToString
                fakelableLoadDate.Text = dr2("LoadDate").ToString


            End Using
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Protected Sub ddlSelectedFS_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedFS.SelectedIndexChanged
        ddlSelectedFSChangedFunction()
    End Sub

    Sub ddlSelectedFSChangedFunction()

        Dim Sql2 As String
        'Dim ds As DataSet
        Dim da As SqlDataAdapter

        ddlSelectedTINs.SelectedIndex = 0

        'Sql2 = "select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
        '        "where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "'  and (FeeSchedName = '" & _
        '        ddlSelectedFS.SelectedValue.ToString & "' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')"
        'testinglabel1.Text = Sql2
        'testinglabel1.DataBind()

        Try
            Sql2 = "select PayorName, FeeSchedName, convert(varchar, EffectiveDate, 107) as [Effective Date], convert(varchar, EndDate, 107) as [End Date], [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                    "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                    Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    " group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes]  order by FeeSchedName, PayorName, EffectiveDate desc, EndDate desc, LoadDate desc," & _
                    " [Default], [ManagersNotes]"


            'ds = New DataSet
            Dim rowboat As DataRow
            tennis.Clear()

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(Sql2, conn2)
                da.Fill(tennis, "DEMO")
                GridView1.DataSource = tennis
                GridView1.DataMember = "DEMO"
                GridView1.DataBind()
                rowboat = tennis.Tables(0).Rows(0)
                txtCurrentED.Text = rowboat("Effective Date").ToString
                txtCurrentED.DataBind()
                txtCurrentDefault.Text = rowboat("Default").ToString
                txtCurrentDefault.DataBind()
                txtCurrentEndD.Text = rowboat("End Date").ToString
                txtCurrentEndD.DataBind()
                txtCurrentMN.Text = rowboat("ManagersNotes").ToString
                txtCurrentMN.DataBind()
                txtPayorName.Text = rowboat("PayorName").ToString
                txtPayorName.DataBind()
                txtFSName.Text = rowboat("FeeSchedName").ToString
                txtFSName.DataBind()

                FakelblStartDate.Text = rowboat("Effective Date").ToString
                FakelblStartDate.DataBind()
                FakelblDefault.Text = rowboat("Default").ToString
                FakelblDefault.DataBind()
                FakelblEndDate.Text = rowboat("End Date").ToString
                FakelblEndDate.DataBind()
                FakelblManagersNotes.Text = rowboat("ManagersNotes").ToString
                FakelblManagersNotes.DataBind()
                FakelblPayorName.Text = rowboat("PayorName").ToString
                FakelblPayorName.DataBind()
                FakelblFSName.Text = rowboat("FeeSchedName").ToString
                FakelblFSName.DataBind()
            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


        Dim x As String
        Dim stupidsql As String
        'Dim dt As DataTable
        'Dim ds As DataSet
        x = "0"
        stupidsql = "Select count(*) from (select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                    "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                    Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    " group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes]) a"



        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            Dim dc As SqlCommand
            dc = New SqlCommand(stupidsql, conn2)
            Try
                conn2.Open()
                x = Convert.ToString(dc.ExecuteScalar())
            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

        End Using

        If CInt(x) > 10 Then
            ScrollPanel.Height = "350"
        ElseIf CInt(x) > 5 Then
            ScrollPanel.Height = "200"
        ElseIf CInt(x) > 3 Then
            ScrollPanel.Height = "150"
        Else
            ScrollPanel.Height = "100"
        End If
        ScrollPanel.DataBind()


        Dim dq As SqlCommand
        Dim countsql As String
        'Dim y As Integer

        countsql = "Select COUNT(distinct FeeSchedID) from DWH.ProFee.FeeSchedule fs where " & _
        "exists(select * from (" & _
        "Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
        ",[LoadDate],[Default]  ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] " & _
        ",[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule where PayorName = '" & _
        Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
        "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
        " Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName" & _
        ") a where rankin = " & 1 & " and a.[Default] = fs.[Default] and a.EffectiveDate = fs.EffectiveDate " & _
        "and a.EndDate = fs.EndDate and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName )"


        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            dq = New SqlCommand(countsql, conn2)
            Try
                conn2.Open()
                y = Convert.ToInt16(dq.ExecuteScalar())
            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

        End Using
        CountLabel.Text = ""
        If y > 1 Then
            CountLabel.Text = "There are " & y.ToString & " fee schedules which meet the above criteria.  Which " & _
                "one would you like to update?"



            Dim grid2sql As String

            grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, case when CPTRank = 1 then Replace(Replace(TIN, '||', ' '), '|', '') else NULL end as TINs," & _
                    "CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                    "from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                    "left join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID " & _
                    "where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
                    " ,[LoadDate] desc,[Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                    "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
                    "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    "Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                    "where(rankin = " & 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                    "and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null or fsd.FeeSchedID is null)" & _
                    "and TIN = (select MAX(TIN) from DWH.ProFee.FeeSchedule where FeeSchedID = fs.FeeSchedID)) b where(CPTRank <= 10) " & _
                    "order by FSRank asc, CPT asc, Modifier asc"

            'Version2
            '    grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, TIN," & _
            '"CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
            '"from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
            '"join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
            '",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
            '"where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
            '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')" & _
            '"Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
            '"where(rankin = " & 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
            '"and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null)) b where(CPTRank <= 10) " & _
            '"order by FSRank asc, CPT asc, Modifier asc"

            Try

                Dim da2 As New SqlDataAdapter
                Dim dss As New DataSet

                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da2 = New SqlDataAdapter(grid2sql, conn2)
                    da2.Fill(dss, "DEMO")
                    GridView2.DataSource = dss
                    GridView2.DataMember = "DEMO"
                    GridView2.DataBind()
                    GridView2.Visible = True
                    redpanel.Visible = True
                    UpdateButton.Enabled = False
                    PanelRed.Visible = True
                    PanelRed.BorderColor = System.Drawing.Color.Red

                    Dim alpha As Int16 = 0
                    While alpha < (y) * 10
                        If Fix(alpha / 10) Mod 2 = 0 Then
                            GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                            alpha = alpha + 1
                        Else
                            GridView2.Rows(alpha).BackColor = System.Drawing.Color.White
                            alpha = alpha + 1
                        End If
                    End While
                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        Else
            'GridView2.Columns.Clear()
            'GridView2.DataBind()
            GridView2.Visible = False
            redpanel.Visible = False
            UpdateButton.Enabled = True
            PanelRed.Visible = False
        End If
        CountLabel.DataBind()
        If x > 0 Then
            GridView2.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
        End If

        CurrentFSPanel.Visible = True

        Dim currentfssql As String

        'currentfssql = "select FeeSchedID, TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
        'ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
        '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ') and '" & _
        'txtCurrentED.Text.ToString & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & txtCurrentEndD.Text.ToString & _
        '"' and [Default] = '" & txtCurrentDefault.Text.ToString & "' and ManagersNotes = '" & _
        'txtCurrentMN.Text.ToString & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"


        currentfssql = "        DECLARE @concatenated NVARCHAR(1000) " & _
        "SET @concatenated = '' " & _
        "SELECT @concatenated = @concatenated + '|' + TIN + '|' FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, Dense_Rank() over (order by LoadDate desc) as den from DWH.ProFee.FeeSchedule FeeSchedID  where PayorName = '" & _
        Replace(txtPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(txtFSName.Text.ToString, "'", "''") & _
        "') and '" & _
        Replace(txtCurrentED.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(txtCurrentEndD.Text.ToString, "'", "''") & _
        "' and [Default] = '" & Replace(txtCurrentDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
        Replace(txtCurrentMN.Text.ToString, "'", "''") & "' ) x where den = 1 ) order by TIN, FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]" & _
        "SELECT top 1 FeeSchedID, LoadDate, left(@concatenated, LEN(@concatenated) ) as TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
        Replace(txtPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(txtFSName.Text.ToString, "'", "''") & "') and '" & _
        Replace(txtCurrentED.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(txtCurrentEndD.Text.ToString, "'", "''") & _
         "' and [Default] = '" & Replace(txtCurrentDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
        Replace(txtCurrentMN.Text.ToString, "'", "''") & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"


        Dim dfs As DataSet
        Dim dfa As New SqlDataAdapter
        Dim cmd As New SqlCommand
        Dim dr2 As DataRow


        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dfs = New DataSet
            cmd = New SqlCommand(currentfssql, conn)
            dfa.SelectCommand = cmd
            dfa.SelectCommand.CommandTimeout = 86400
            dfa.Fill(dfs, "TIns")
            dr2 = dfs.Tables(0).Rows(0)
            fakelabelTIN.Text = dr2("TIN").ToString
            lblPreviousTIN.Text = Replace(Replace(dr2("TIN").ToString, "||", ", "), "|", "")
            lblPreviousTIN.DataBind()
            postFS = dr2("FeeSchedID").ToString
            fakelableLoadDate.Text = dr2("LoadDate").ToString


        End Using

    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged




        'If gridname = "nothingyet" Then
        'labeltesting2.Text = ""
        'gridname = GridView1.SelectedIndex.ToString 'GridView1.SelectedRow.Attributes.ToString()

        'labeltesting2.Text = GridView1.SelectedIndex.ToString
        'Dim keyColl As  _
        'Dictionary(Of String, String).KeyCollection = _
        'GridView1.SelectedDataKey

        'GridView1.SelectedRow.BackColor = Drawing.Color.LightSteelBlue
        'For Each s As String In GridView1.Attributes.Keys
        'Label2.Text = Label2.Text & s.ToString
        'Next s
        'labeltesting2.DataBind()
        'For Each kvp As KeyValuePair(Of String, String) In keyColl
        'Label2.Text = Label2.Text & kvp.Key & kvp.Value
        'Next kvp



        'labeltesting2.Text = GridView1.SelectedRow.Attributes.Keys.ToString

        Try

            If gridname = GridView1.SelectedIndex.ToString Then
                Exit Sub
            Else

                txtCurrentED.Text = ""
                txtCurrentED.DataBind()
                txtCurrentDefault.Text = ""
                txtCurrentDefault.DataBind()
                txtCurrentEndD.Text = ""
                txtCurrentEndD.DataBind()
                txtCurrentMN.Text = ""
                txtCurrentMN.DataBind()
                txtPayorName.Text = ""
                txtPayorName.DataBind()
                txtFSName.Text = ""
                txtFSName.DataBind()

                fakelabelTIN.Text = "||"
                lblPreviousTIN.Text = ""
                lblPreviousTIN.DataBind()
                postFS = ""
                fakelableLoadDate.Text = ""
                UpdateButton.Enabled = False

                Dim x As String
                Dim stupidsql As String
                'Dim dt As DataTable
                'Dim ds As DataSet
                'Dim alpha As Integer
                'x = "notfilled"
                'stupidsql = "Select count(*) from (select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                '            "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                '            Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                '            " group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes]) a"



                'Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                '    Dim dc As SqlCommand
                '    dc = New SqlCommand(stupidsql, conn2)
                '    Try
                '        conn2.Open()
                '        x = Convert.ToInt16(dc.ExecuteScalar())
                '    Catch ex As Exception
                '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                '    End Try

                'End Using
                'alpha = 0
                gridname = GridView1.SelectedIndex.ToString
                'While alpha < CInt(x)
                '    If alpha = CInt(gridname) Then
                '        alpha = alpha + 1
                '    ElseIf alpha Mod 2 = 0 Then
                '        GridView1.Rows(alpha).BackColor = Drawing.Color.White
                '        alpha = alpha + 1
                '    Else
                '        GridView1.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#eee4ce")
                '        alpha = alpha + 1
                '    End If
                'End While
                Dim counter As Integer = 0
                For Each row As GridViewRow In GridView1.Rows
                    If counter Mod 2 = 0 Then
                        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                    Else
                        row.BackColor = System.Drawing.Color.White
                    End If
                    counter = counter + 1

                Next

                'labeltesting2.Text = labeltesting2.Text & x.ToString & stupidsql

                'If GridView1.BackColor = Drawing.Color.LightSteelBlue Then
                'GridView1.BackColor = Drawing.Color.Wheat
                'End If
                'For Each s As Integer In GridView1.Rows
                'If s = gridname Then
                'GridView1.BackColor = Drawing.Color.White
                'End If
                'Next

                GridView1.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")

                'labeltesting2.Text = labeltesting2.Text & gridname
                'labeltesting2.Text = GridView1.SelectedRow.Attributes.ToString()
            End If

            If ddlSelectedTINs.SelectedIndex <> 0 Then

                Dim dq As SqlCommand
                Dim countsql2 As String
                Dim Postimestart As Integer = 2
                Dim rankin As String

                If GridView1.SelectedIndex > 0 Then
                    For i = 1 To GridView1.SelectedIndex
                        Postimestart = InStr(Postimestart + 1, ddlSelectedTINs.SelectedValue, ",")
                    Next
                    rankin = Mid(ddlSelectedTINs.SelectedValue, Postimestart + 1, If(ddlSelectedTINs.SelectedValue.IndexOf(" ") > 0, InStr(Postimestart, ddlSelectedTINs.SelectedValue, " "), InStr(Postimestart, ddlSelectedTINs.SelectedValue, ")") + 1) - Postimestart + 1)
                Else
                    rankin = Mid(ddlSelectedTINs.SelectedValue, Postimestart, If(ddlSelectedTINs.SelectedValue.IndexOf(",") > 0, InStr(Postimestart, ddlSelectedTINs.SelectedValue, ","), InStr(Postimestart, ddlSelectedTINs.SelectedValue, ")") + 1) - Postimestart)
                End If


                countsql2 = "Select COUNT(distinct FeeSchedID) from DWH.ProFee.FeeSchedule fs where " & _
                "exists(select * from (" & _
                "Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
                " ,[LoadDate] desc,[Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] " & _
                " ,[LoadDate] ,[Default] ,[ManagersNotes] from DWH.ProFee.FeeSchedule where PayorName = '" & _
                Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
                "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                " Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName" & _
                ") a where rankin = " & rankin & _
                " and a.[Default] = fs.[Default] and a.EffectiveDate = fs.EffectiveDate " & _
                "and a.EndDate = fs.EndDate and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName )"

                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    dq = New SqlCommand(countsql2, conn2)
                    'Try
                    conn2.Open()
                    y = Convert.ToInt16(dq.ExecuteScalar())
                    'Catch ex As Exception
                    '    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    'End Try

                End Using
                CountLabel.Text = ""
                If y > 1 Then
                    CountLabel.Text = "There are " & y.ToString & " fee schedules which meet the above criteria.  Which " & _
                        "one would you like to update?"



                    Dim grid2sql As String

                    grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, case when CPTRank = 1 then Replace(Replace(TIN, '||', ' '), '|', '') else NULL end as TINs," & _
                            "CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                            "from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                            "left join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID " & _
                            "where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
                            " ,[LoadDate] desc,[Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                            "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
                            "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                            "Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                            " where rankin = " & rankin & _
                            " And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                            "and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null or fsd.FeeSchedID is null)" & _
                            "and TIN = (select MAX(TIN) from DWH.ProFee.FeeSchedule where FeeSchedID = fs.FeeSchedID)) b where(CPTRank <= 10) " & _
                            "order by FSRank asc, CPT asc, Modifier asc"

                    'Version2
                    '    grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, TIN," & _
                    '"CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                    '"from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                    '"join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
                    '",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                    '"where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
                    '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')" & _
                    '"Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                    '"where(rankin = " & 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                    '"and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null)) b where(CPTRank <= 10) " & _
                    '"order by FSRank asc, CPT asc, Modifier asc"

                    'Try

                    Dim da2 As New SqlDataAdapter
                    Dim dss As New DataSet

                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da2 = New SqlDataAdapter(grid2sql, conn2)
                        da2.Fill(dss, "DEMO")
                        GridView2.DataSource = dss
                        GridView2.DataMember = "DEMO"
                        GridView2.DataBind()
                        GridView2.Visible = True
                        redpanel.Visible = True
                        UpdateButton.Enabled = False
                        PanelRed.Visible = True
                        PanelRed.BorderColor = System.Drawing.Color.Red

                        Dim alpha As Int16 = 0
                        While alpha < (y) * 10
                            If Fix(alpha / 10) Mod 2 = 0 Then
                                GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                                alpha = alpha + 1
                            Else
                                GridView2.Rows(alpha).BackColor = System.Drawing.Color.White
                                alpha = alpha + 1
                            End If
                        End While
                    End Using


                    'Catch ex As Exception
                    '    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    'End Try
                Else
                    'GridView2.Columns.Clear()
                    'GridView2.DataBind()
                    GridView2.Visible = False
                    redpanel.Visible = False
                    UpdateButton.Enabled = True
                    PanelRed.Visible = False
                End If
                CountLabel.DataBind()
                If PanelRed.Visible = True Then
                    GridView1.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                End If

                CurrentFSPanel.Visible = True


                Dim dr3 As DataRow

                dr3 = tennis.Tables(0).Rows(CInt(gridname))

                txtCurrentED.Text = dr3("Effective Date").ToString
                txtCurrentED.DataBind()
                txtCurrentDefault.Text = dr3("Default").ToString
                txtCurrentDefault.DataBind()
                txtCurrentEndD.Text = dr3("End Date").ToString
                txtCurrentEndD.DataBind()
                txtCurrentMN.Text = dr3("ManagersNotes").ToString
                txtCurrentMN.DataBind()
                txtPayorName.Text = dr3("PayorName").ToString
                txtPayorName.DataBind()
                txtFSName.Text = dr3("FeeSchedName").ToString
                txtFSName.DataBind()
                UpdateButton.Enabled = True

                FakelblStartDate.Text = dr3("Effective Date").ToString
                FakelblStartDate.DataBind()
                FakelblDefault.Text = dr3("Default").ToString
                FakelblDefault.DataBind()
                FakelblEndDate.Text = dr3("End Date").ToString
                FakelblEndDate.DataBind()
                FakelblManagersNotes.Text = dr3("ManagersNotes").ToString
                FakelblManagersNotes.DataBind()
                FakelblPayorName.Text = dr3("PayorName").ToString
                FakelblPayorName.DataBind()
                FakelblFSName.Text = dr3("FeeSchedName").ToString
                FakelblFSName.DataBind()

                Dim currentfssql2 As String

                'currentfssql = "select FeeSchedID, TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
                'ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
                '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ') and '" & _
                'txtCurrentED.Text.ToString & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & txtCurrentEndD.Text.ToString & _
                '"' and [Default] = '" & txtCurrentDefault.Text.ToString & "' and ManagersNotes = '" & _
                'txtCurrentMN.Text.ToString & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"


                currentfssql2 = "        DECLARE @concatenated NVARCHAR(1000) " & _
                "SET @concatenated = '' " & _
                "SELECT @concatenated = @concatenated + '|' + TIN + '|' FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc, [EndDate] desc, [Default], " & _
                    "[LoadDate] desc, [ManagersNotes]) as rankin from DWH.ProFee.FeeSchedule where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                    Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    ") x where rankin in (" & rankin & _
                    ")) order by TIN, FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]" & _
                "SELECT top 1 FeeSchedID, LoadDate, left(@concatenated, LEN(@concatenated) ) as TIN from DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc, [EndDate] desc, [Default], " & _
                    "[LoadDate] desc, [ManagersNotes]) as rankin from DWH.ProFee.FeeSchedule where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                    Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    ") x where rankin in (" & rankin & _
                    ")) order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"


                Dim dfs2 As DataSet
                Dim dfa2 As New SqlDataAdapter
                Dim cmd2 As New SqlCommand
                Dim dr22 As DataRow


                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dfs2 = New DataSet
                    cmd2 = New SqlCommand(currentfssql2, conn)
                    dfa2.SelectCommand = cmd2
                    dfa2.SelectCommand.CommandTimeout = 86400
                    dfa2.Fill(dfs2, "TIns")
                    dr22 = dfs2.Tables(0).Rows(0)
                    fakelabelTIN.Text = dr22("TIN").ToString
                    lblPreviousTIN.Text = Replace(Replace(dr22("TIN").ToString, "||", ", "), "|", "")
                    lblPreviousTIN.DataBind()
                    postFS = dr22("FeeSchedID").ToString
                    fakelableLoadDate.Text = dr22("LoadDate").ToString


                End Using

                Exit Sub

            End If

            Dim da As SqlCommand
            Dim countsql As String
            'Dim y As Integer

            countsql = "Select COUNT(distinct FeeSchedID) from DWH.ProFee.FeeSchedule fs where " & _
            "exists(select * from (" & _
            "Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
            " ,[LoadDate] desc, [Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] " & _
            ",[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule where PayorName = '" & _
            Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
            "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
            " Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName" & _
            ") a where rankin = " & gridname.ToString + 1 & " and a.[Default] = fs.[Default] and a.EffectiveDate = fs.EffectiveDate " & _
            "and a.EndDate = fs.EndDate and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName )"

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlCommand(countsql, conn2)
                'Try
                conn2.Open()
                y = Convert.ToInt16(da.ExecuteScalar())
                'Catch ex As Exception
                '    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                'End Try

            End Using
            CountLabel.Text = ""
            If y > 1 Then
                CountLabel.Text = "There are " & y.ToString & " fee schedules which meet the above criteria.  Which " & _
                    "one would you like to update?"



                Dim grid2sql As String

                'grid2sql = "Select COUNT(*) from DWH.ProFee.FeeSchedule fs where " & _
                '"exists(select * from (" & _
                '"Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
                '",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] " & _
                '",[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule where PayorName = '" & _
                'ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
                '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = ' -- None -- ')" & _
                '" Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName" & _
                '") a where rankin = " & gridname.ToString + 1 & " and a.[Default] = fs.[Default] and a.EffectiveDate = fs.EffectiveDate " & _
                '"and a.EndDate = fs.EndDate and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName )"

                'old
                'grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                '        "from (Select fsd.*, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                '        "join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
                '        ",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                '        "where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
                '        "' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')" & _
                '        "Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                '        "where(rankin = " & gridname.ToString + 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                '        "and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null)) b where(CPTRank <= 10) " & _
                '        "order by FSRank asc, CPT asc, Modifier asc"

                'version1
                grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, case when CPTRank = 1 then Replace(Replace(TIN, '||', ' '), '|', '') else NULL end as TINs," & _
            "CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
            "from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
            "left join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID " & _
            "where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
            ",[LoadDate] desc,[Default]  ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
            "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
            "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
            "Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
            "where(rankin = " & gridname.ToString + 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
            "and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null or fsd.FeeSchedID is null) " & _
            "and TIN = (select MAX(TIN) from DWH.ProFee.FeeSchedule where FeeSchedID = fs.FeeSchedID)) b where(CPTRank <= 10) " & _
            "order by FSRank asc, CPT asc, Modifier asc"

                'version2
                '            grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, TIN," & _
                '"CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                '"from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                '"join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
                '",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                '"where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
                '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')" & _
                '"Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                '"where(rankin = " & gridname.ToString + 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
                '"and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null)) b where(CPTRank <= 10) " & _
                '"order by FSRank asc, CPT asc, Modifier asc"

                'Try

                Dim da2 As New SqlDataAdapter
                Dim dss As New DataSet

                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da2 = New SqlDataAdapter(grid2sql, conn2)
                    da2.Fill(dss, "DEMO")
                    GridView2.DataSource = dss
                    GridView2.DataMember = "DEMO"
                    GridView2.DataBind()
                    GridView2.Visible = True
                    redpanel.Visible = True
                    UpdateButton.Enabled = False
                    PanelRed.Visible = True
                    PanelRed.BorderColor = System.Drawing.Color.Red

                    Dim alpha As Int16 = 0
                    While alpha < (y) * 10
                        If Fix(alpha / 10) Mod 2 = 0 Then
                            GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                            alpha = alpha + 1
                        Else
                            GridView2.Rows(alpha).BackColor = System.Drawing.Color.White
                            alpha = alpha + 1
                        End If
                    End While

                End Using


                'Catch ex As Exception
                '    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                'End Try
            Else
                'GridView2.Columns.Clear()
                'GridView2.DataBind()
                GridView2.Visible = False
                redpanel.Visible = False
                UpdateButton.Enabled = True
                PanelRed.Visible = False

            End If
            CountLabel.DataBind()

            Dim dr As DataRow

            dr = tennis.Tables(0).Rows(CInt(gridname))

            txtCurrentED.Text = dr("Effective Date").ToString
            txtCurrentED.DataBind()
            txtCurrentDefault.Text = dr("Default").ToString
            txtCurrentDefault.DataBind()
            txtCurrentEndD.Text = dr("End Date").ToString
            txtCurrentEndD.DataBind()
            txtCurrentMN.Text = dr("ManagersNotes").ToString
            txtCurrentMN.DataBind()
            txtPayorName.Text = dr("PayorName").ToString
            txtPayorName.DataBind()
            txtFSName.Text = dr("FeeSchedName").ToString
            txtFSName.DataBind()
            UpdateButton.Enabled = True

            FakelblStartDate.Text = dr("Effective Date").ToString
            FakelblStartDate.DataBind()
            FakelblDefault.Text = dr("Default").ToString
            FakelblDefault.DataBind()
            FakelblEndDate.Text = dr("End Date").ToString
            FakelblEndDate.DataBind()
            FakelblManagersNotes.Text = dr("ManagersNotes").ToString
            FakelblManagersNotes.DataBind()
            FakelblPayorName.Text = dr("PayorName").ToString
            FakelblPayorName.DataBind()
            FakelblFSName.Text = dr("FeeSchedName").ToString
            FakelblFSName.DataBind()

            Dim currentfssql As String

            'currentfssql = "select FeeSchedID, TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
            'ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
            '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ') and '" & _
            'txtCurrentED.Text.ToString & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & txtCurrentEndD.Text.ToString & _
            '"' and [Default] = '" & txtCurrentDefault.Text.ToString & "' and ManagersNotes = '" & _
            'txtCurrentMN.Text.ToString & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"


            currentfssql = "        DECLARE @concatenated NVARCHAR(1000) " & _
        "SET @concatenated = '' " & _
        "SELECT @concatenated = @concatenated + '|' + TIN + '|' FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, " & _
        "DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate] desc, [Default] ,[ManagersNotes]) as rankin  from DWH.ProFee.FeeSchedule " & _
        "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
            "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & " ) x where rankin = " & gridname.ToString + 1 & " ) order by TIN, FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]" & _
        "SELECT top 1 FeeSchedID, LoadDate, left(@concatenated, LEN(@concatenated)) as TIN FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, " & _
        "DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc,[LoadDate] desc, [Default]  ,[ManagersNotes]) as rankin  from DWH.ProFee.FeeSchedule " & _
        "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
            "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & " ) x where rankin = " & gridname.ToString + 1 & " ) order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"




            Dim dfs As DataSet
            Dim dfa As New SqlDataAdapter
            Dim cmd As New SqlCommand
            Dim dr2 As DataRow


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dfs = New DataSet
                cmd = New SqlCommand(currentfssql, conn)
                dfa.SelectCommand = cmd
                dfa.SelectCommand.CommandTimeout = 86400
                dfa.Fill(dfs, "Tins")
                dr2 = dfs.Tables(0).Rows(0)
                fakelabelTIN.Text = dr2("TIN").ToString
                lblPreviousTIN.Text = Replace(Replace(dr2("TIN").ToString, "||", ", "), "|", "")
                lblPreviousTIN.DataBind()
                postFS = dr2("FeeSchedID").ToString
                fakelableLoadDate.Text = dr2("LoadDate").ToString

            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub GridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView2.SelectedIndexChanged

        Try

            Dim gridnew As String
            Dim alpha As Int16 = 0
            gridnew = GridView2.SelectedIndex.ToString
            For Each bow As GridViewRow In GridView2.Rows
                If alpha >= Fix(CInt(gridnew) / 10) * 10 And alpha < Fix(CInt(gridnew) / 10) * 10 + 10 Then
                    GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                    alpha = alpha + 1
                ElseIf Fix(alpha / 10) Mod 2 = 0 Then
                    GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                    alpha = alpha + 1
                Else
                    GridView2.Rows(alpha).BackColor = System.Drawing.Color.White
                    alpha = alpha + 1
                End If
            Next


            Dim currentfssql As String

            currentfssql = "        DECLARE @concatenated NVARCHAR(1000) " & _
            "SET @concatenated = '' " & _
            "SELECT @concatenated = @concatenated + '|' + TIN + '|' FROM (select FeeSchedID, TIN from (select FeeSchedID, TIN, DENSE_rank() over(partition by FeeSchedName " & _
                "order by FeeSchedID, EffectiveDate desc, EndDate desc,  [LoadDate] desc,[Default], [ManagersNotes]) as rankin from DWH.ProFee.FeeSchedule " & _
                "where PayorName = '" & _
            Replace(FakelblPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(FakelblFSName.Text.ToString, "'", "''") & "') and '" & _
            Replace(FakelblStartDate.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(FakelblEndDate.Text.ToString, "'", "''") & _
            "' and [LoadDate] = '" & Replace(fakelableLoadDate.Text.ToString, "'", "''") & _
            "' and [Default] = '" & Replace(FakelblDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
            Replace(FakelblManagersNotes.Text.ToString, "'", "''") & "') b where rankin = '" & Fix(CInt(gridnew) / 10) + 1 & "') z " & _
            "SELECT top 1 FeeSchedID, LoadDate, left(@concatenated, LEN(@concatenated)) as TIN from (select FeeSchedID, LoadDate, TIN from (select FeeSchedID, LoadDate, TIN, DENSE_rank() over(partition by FeeSchedName " & _
                "order by FeeSchedID, EffectiveDate desc, EndDate desc, [Default], [LoadDate], [ManagersNotes]) as rankin from DWH.ProFee.FeeSchedule " & _
                "where PayorName = '" & _
            Replace(FakelblPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(FakelblFSName.Text.ToString, "'", "''") & "') and '" & _
            Replace(FakelblStartDate.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(FakelblEndDate.Text.ToString, "'", "''") & _
            "' and [LoadDate] = '" & Replace(fakelableLoadDate.Text.ToString, "'", "''") & _
            "' and [Default] = '" & Replace(FakelblDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
            Replace(FakelblManagersNotes.Text.ToString, "'", "''") & "') b where rankin = '" & Fix(CInt(gridnew) / 10) + 1 & "') z"

            'currentfssql = "(select FeeSchedID, TIN from (select FeeSchedID, TIN, rank() over(partition by FeeSchedName " & _
            '    "order by FeeSchedID, EffectiveDate desc, EndDate desc, [Default], [LoadDate], [ManagersNotes]) as rankin from DWH.ProFee.FeeSchedule " & _
            '    "where PayorName = '" & _
            'lblPayorName.Text.ToString & "' and (FeeSchedName = '" & lblFSName.Text.ToString & "') and '" & _
            'txtCurrentED.Text.ToString & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & txtCurrentEndD.Text.ToString & _
            '"' and [Default] = '" & txtCurrentDefault.Text.ToString & "' and ManagersNotes = '" & _
            'txtCurrentMN.Text.ToString & "') b where rankin = '" & Fix(CInt(gridnew) / 10) + 1 & "') z"

            Dim dfs As DataSet
            Dim dfa As New SqlDataAdapter
            Dim cmd As New SqlCommand
            Dim dr2 As DataRow


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dfs = New DataSet
                cmd = New SqlCommand(currentfssql, conn)
                dfa.SelectCommand = cmd
                dfa.SelectCommand.CommandTimeout = 86400
                dfa.Fill(dfs, "TIns")
                dr2 = dfs.Tables(0).Rows(0)
                fakelabelTIN.Text = dr2("TIN").ToString
                lblPreviousTIN.Text = Replace(Replace(dr2("TIN").ToString, "||", ", "), "|", "")
                lblPreviousTIN.DataBind()
                postFS = dr2("FeeSchedID").ToString
                fakelableLoadDate.Text = dr2("LoadDate").ToString

            End Using

            PanelRed.Visible = False
            redpanel.BorderColor = System.Drawing.Color.White
            UpdateButton.Enabled = True
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub lblPreviousTIN_DataBinding(sender As Object, e As System.EventArgs) Handles lblPreviousTIN.DataBinding

        Try

            Dim leftovers As String = fakelabelTIN.Text

            If InStr(Right(Left(leftovers, Len(leftovers) - 1), Len(leftovers) - 2), "|", CompareMethod.Text) > 0 Then

                'If InStr(leftovers, "9 Month Travis Jones", CompareMethod.Text) > 0 Then
                '    cbTravisJones.Checked = True
                '    leftovers = leftovers.Replace("9 Month Travis Jones", "")
                'Else
                '    cbTravisJones.Checked = False
                'End If

                If InStr(leftovers, "|NSPS (gyn onc only)|", CompareMethod.Text) > 0 Then
                    cbNSPSonly.Checked = True
                    leftovers = leftovers.Replace("|NSPS (gyn onc only)|", "")
                Else
                    cbNSPSonly.Checked = False
                End If

                If InStr(leftovers, "|NSPS (incl gyn onc)|", CompareMethod.Text) > 0 Then
                    cbNSPSinc.Checked = True
                    leftovers = leftovers.Replace("|NSPS (incl gyn onc)|", "")
                Else
                    cbNSPSinc.Checked = False
                End If

                If InStr(leftovers, "|NSPS (excl gyn onc)|", CompareMethod.Text) > 0 Then
                    cbNSPSex.Checked = True
                    leftovers = leftovers.Replace("|NSPS (excl gyn onc)|", "")
                Else
                    cbNSPSex.Checked = False
                End If

                If InStr(leftovers, "|MANG (SPC Only)|", CompareMethod.Text) > 0 Then
                    cbMANGSPC.Checked = True
                    leftovers = leftovers.Replace("|MANG (SPC Only)|", "")
                Else
                    cbMANGSPC.Checked = False
                End If

                If InStr(leftovers, "|MANG (PCP Only)|", CompareMethod.Text) > 0 Then
                    cbMANGPCP.Checked = True
                    leftovers = leftovers.Replace("|MANG (PCP Only)|", "")
                Else
                    cbMANGPCP.Checked = False
                End If

                If InStr(leftovers, "|PNFM|", CompareMethod.Text) > 0 Then
                    cbPNFM.Checked = True
                    leftovers = leftovers.Replace("|PNFM|", "")
                Else
                    cbPNFM.Checked = False
                End If

                If InStr(leftovers, "|GU|", CompareMethod.Text) > 0 Then
                    cbGU.Checked = True
                    leftovers = leftovers.Replace("|GU|", "")
                Else
                    cbGU.Checked = False
                End If


                If InStr(leftovers, "|NPCPS|", CompareMethod.Text) > 0 Then
                    cbNPCPS.Checked = True
                    leftovers = leftovers.Replace("|NPCPS|", "")
                Else
                    cbNPCPS.Checked = False
                End If

                'If InStr(leftovers, "|MAPS|", CompareMethod.Text) > 0 Then
                '    cbMAPS.Checked = True
                '    leftovers = leftovers.Replace("|MAPS|", " ")
                'Else
                '    cbMAPS.Checked = False
                'End If

                If InStr(leftovers, "|MANG (ALL)|", CompareMethod.Text) > 0 Then
                    cbMANG.Checked = True
                    leftovers = leftovers.Replace("MANG (ALL)", " ")
                Else
                    cbMANG.Checked = False
                End If

                If InStr(leftovers, "|GSPS|", CompareMethod.Text) > 0 Then
                    cbGSPS.Checked = True
                    leftovers = leftovers.Replace("|GSPS|", " ")
                Else
                    cbGSPS.Checked = False
                End If

                If InStr(leftovers, "|NCPS|", CompareMethod.Text) > 0 Then
                    cbNCPS.Checked = True
                    leftovers = leftovers.Replace("|NCPS|", " ")
                Else
                    cbNCPS.Checked = False
                End If

                'If InStr(leftovers, "|NSPS|", CompareMethod.Text) > 0 Then
                '    cbNSPS.Checked = True
                '    leftovers = leftovers.Replace("|NSPS|", " ")
                'Else
                '    cbNSPS.Checked = False
                'End If

                If InStr(leftovers, "|NAPS|", CompareMethod.Text) > 0 Then
                    cbNAPS.Checked = True
                    leftovers = leftovers.Replace("|NAPS|", " ")
                Else
                    cbNAPS.Checked = False
                End If

                If InStr(leftovers, "|AGA|", CompareMethod.Text) > 0 Then
                    cbAGA.Checked = True
                    leftovers = leftovers.Replace("|AGA|", " ")
                Else
                    cbAGA.Checked = False
                End If

                If InStr(leftovers, "|GCS|", CompareMethod.Text) > 0 Then
                    cbGCS.Checked = True
                    leftovers = leftovers.Replace("|GCS|", " ")
                Else
                    cbGCS.Checked = False
                End If

                If InStr(leftovers, "|ACC|", CompareMethod.Text) > 0 Then
                    cbACC.Checked = True
                    leftovers = leftovers.Replace("|ACC|", " ")
                Else
                    cbACC.Checked = False
                End If

                If InStr(leftovers, "|USA|", CompareMethod.Text) > 0 Then
                    cbUSA.Checked = True
                    leftovers = leftovers.Replace("|USA|", " ")
                Else
                    cbUSA.Checked = False
                End If

                If InStr(leftovers, "|ALL|", CompareMethod.Text) > 0 Then
                    cbALL.Checked = True
                    leftovers = leftovers.Replace("|ALL|", " ")
                Else
                    cbALL.Checked = False
                End If

                While InStr(leftovers, "  ", CompareMethod.Text) > 0
                    leftovers = leftovers.Replace("  ", " ")
                End While
            Else



                'If InStr(leftovers, "9 Month Travis Jones", CompareMethod.Text) > 0 Then
                '    cbTravisJones.Checked = True
                '    leftovers = leftovers.Replace("9 Month Travis Jones", "")
                'Else
                '    cbTravisJones.Checked = False
                'End If

                If InStr(leftovers, "NSPS(gyn onc only)", CompareMethod.Text) > 0 Then
                    cbNSPSonly.Checked = True
                    leftovers = leftovers.Replace("NSPS(gyn onc only)", "")
                ElseIf InStr(leftovers, "NSPS (gyn onc only)", CompareMethod.Text) > 0 Then
                    cbNSPSonly.Checked = True
                    leftovers = leftovers.Replace("NSPS (gyn onc only)", "")
                Else
                    cbNSPSonly.Checked = False
                End If

                If InStr(leftovers, "NSPS(incl gyn onc)", CompareMethod.Text) > 0 Then
                    cbNSPSinc.Checked = True
                    leftovers = leftovers.Replace("NSPS(incl gyn onc)", "")
                ElseIf InStr(leftovers, "NSPS (incl gyn onc)", CompareMethod.Text) > 0 Then
                    cbNSPSinc.Checked = True
                    leftovers = leftovers.Replace("NSPS (incl gyn onc)", "")
                Else
                    cbNSPSinc.Checked = False
                End If

                If InStr(leftovers, "NSPS(excl gyn onc)", CompareMethod.Text) > 0 Then
                    cbNSPSex.Checked = True
                    leftovers = leftovers.Replace("NSPS(excl gyn onc)", "")
                ElseIf InStr(leftovers, "NSPS (excl gyn onc)", CompareMethod.Text) > 0 Then
                    cbNSPSex.Checked = True
                    leftovers = leftovers.Replace("NSPS (excl gyn onc)", "")
                Else
                    cbNSPSex.Checked = False
                End If

                If InStr(leftovers, "NPCPS", CompareMethod.Text) > 0 Then
                    cbNPCPS.Checked = True
                    leftovers = leftovers.Replace("NPCPS", "")
                Else
                    cbNPCPS.Checked = False
                End If

                If InStr(leftovers, "MANG (SPC Only)", CompareMethod.Text) > 0 Then
                    cbMANGSPC.Checked = True
                    leftovers = leftovers.Replace("MANG (SPC Only)", "")
                Else
                    cbMANGSPC.Checked = False
                End If

                If InStr(leftovers, "MANG (PCP Only)", CompareMethod.Text) > 0 Then
                    cbMANGPCP.Checked = True
                    leftovers = leftovers.Replace("MANG (PCP Only)", "")
                Else
                    cbMANGPCP.Checked = False
                End If

                If InStr(leftovers, "PNFM", CompareMethod.Text) > 0 Then
                    cbPNFM.Checked = True
                    leftovers = leftovers.Replace("PNFM", "")
                Else
                    cbPNFM.Checked = False
                End If

                If InStr(leftovers, "GU", CompareMethod.Text) > 0 Then
                    cbGU.Checked = True
                    leftovers = leftovers.Replace("GU", "")
                Else
                    cbGU.Checked = False
                End If


                'If InStr(leftovers, "MAPS", CompareMethod.Text) > 0 Then
                '    cbMAPS.Checked = True
                '    leftovers = leftovers.Replace("MAPS", " ")
                'Else
                '    cbMAPS.Checked = False
                'End If

                If InStr(leftovers, "MANG (ALL)", CompareMethod.Text) > 0 Then
                    cbMANG.Checked = True
                    leftovers = leftovers.Replace("MANG (ALL)", " ")
                Else
                    cbMANG.Checked = False
                End If

                If InStr(leftovers, "GSPS", CompareMethod.Text) > 0 Then
                    cbGSPS.Checked = True
                    leftovers = leftovers.Replace("GSPS", " ")
                Else
                    cbGSPS.Checked = False
                End If

                If InStr(leftovers, "NCPS", CompareMethod.Text) > 0 Then
                    cbNCPS.Checked = True
                    leftovers = leftovers.Replace("NCPS", " ")
                Else
                    cbNCPS.Checked = False
                End If

                'If InStr(leftovers, "NSPS", CompareMethod.Text) > 0 Then
                '    cbNSPS.Checked = True
                '    leftovers = leftovers.Replace("NSPS", " ")
                'Else
                '    cbNSPS.Checked = False
                'End If

                If InStr(leftovers, "NAPS", CompareMethod.Text) > 0 Then
                    cbNAPS.Checked = True
                    leftovers = leftovers.Replace("NAPS", " ")
                Else
                    cbNAPS.Checked = False
                End If

                If InStr(leftovers, "AGA", CompareMethod.Text) > 0 Then
                    cbAGA.Checked = True
                    leftovers = leftovers.Replace("AGA", " ")
                Else
                    cbAGA.Checked = False
                End If

                If InStr(leftovers, "GCS", CompareMethod.Text) > 0 Then
                    cbGCS.Checked = True
                    leftovers = leftovers.Replace("GCS", " ")
                Else
                    cbGCS.Checked = False
                End If

                If InStr(leftovers, "ACC", CompareMethod.Text) > 0 Then
                    cbACC.Checked = True
                    leftovers = leftovers.Replace("ACC", " ")
                Else
                    cbACC.Checked = False
                End If

                If InStr(leftovers, "USA", CompareMethod.Text) > 0 Then
                    cbUSA.Checked = True
                    leftovers = leftovers.Replace("USA", " ")
                Else
                    cbUSA.Checked = False
                End If

                If InStr(leftovers, "ALL", CompareMethod.Text) > 0 Then
                    cbALL.Checked = True
                    leftovers = leftovers.Replace("ALL", " ")
                Else
                    cbALL.Checked = False
                End If

                While InStr(leftovers, "  ", CompareMethod.Text) > 0
                    leftovers = leftovers.Replace("  ", " ")
                End While

            End If

            If InStr(leftovers, "|", CompareMethod.Text) > 0 Then
                leftovers = leftovers.Replace("|", "")
            End If
            While InStr(leftovers, ", ,", CompareMethod.Text) > 0
                leftovers = leftovers.Replace(", ,", ",")
            End While
            While InStr(leftovers, ",,", CompareMethod.Text) > 0
                leftovers = leftovers.Replace(",,", ",")
            End While
            txtFree.Text = leftovers
            txtFree.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As System.EventArgs) Handles UpdateButton.Click

        Try

            Dim Tinlist As String = ""
            Dim insertsql As String = ""

            'If cbTravisJones.Checked Then
            '    Tinlist = Tinlist + "'9 Month Travis Jones', "
            '    insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
            '    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & "', '" & lblFSName.Text.ToString & ", '" & lblPayorName.Text.ToString & _
            '    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
            '    "', '" & fakelableLoadDate.Text.ToString & "', '9 Month Travis Jones', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
            '    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = '9 Month Travis Jones') "
            'End If
            If cbACC.Checked Then
                Tinlist = Tinlist + "'ACC', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
                ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
                "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
                "', '" & fakelableLoadDate.Text.ToString & "', 'ACC', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
                "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'ACC') "
            End If

            If cbAGA.Checked Then
                Tinlist = Tinlist + "'AGA', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'AGA', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'AGA') "
            End If

            If cbALL.Checked Then
                Tinlist = Tinlist + "'ALL', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'ALL', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'ALL') "
            End If

            If cbGCS.Checked Then
                Tinlist = Tinlist + "'GCS', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'GCS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'GCS') "
            End If

            If cbGSPS.Checked Then
                Tinlist = Tinlist + "'GSPS', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'GSPS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'GSPS') "
            End If

            If cbMANG.Checked Then
                Tinlist = Tinlist + "'MANG (ALL)', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'MANG (ALL)', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'MANG (ALL)') "
            End If

            If cbMANGPCP.Checked Then
                Tinlist = Tinlist + "'MANG (PCP Only)', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'MANG (PCP Only)', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'MANG (PCP Only)') "
            End If

            If cbMANGSPC.Checked Then
                Tinlist = Tinlist + "'MANG (SPC Only)', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'MANG (SPC Only)', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'MANG (SPC Only)') "
            End If

            If cbGU.Checked Then
                Tinlist = Tinlist + "'GU', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'GU', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'GU') "
            End If

            If cbPNFM.Checked Then
                Tinlist = Tinlist + "'PNFM', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'PNFM', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'PNFM') "
            End If

            If cbUSA.Checked Then
                Tinlist = Tinlist + "'USA', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'USA', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'USA') "
            End If

            '        If cbMAPS.Checked Then
            '            Tinlist = Tinlist + "'MAPS', "
            '            insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
            '",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & lblFSName.Text.ToString & "', '" & lblPayorName.Text.ToString & _
            '"', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
            '"', '" & fakelableLoadDate.Text.ToString & "', 'MAPS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
            '"(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'MAPS') "
            '        End If

            If cbNAPS.Checked Then
                Tinlist = Tinlist + "'NAPS', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'NAPS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NAPS') "
            End If

            If cbNCPS.Checked Then
                Tinlist = Tinlist + "'NCPS', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'NCPS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NCPS') "
            End If

            If cbNPCPS.Checked Then
                Tinlist = Tinlist + "'NPCPS', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'NPCPS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NPCPS') "
            End If

            '        If cbNSPS.Checked Then
            '            Tinlist = Tinlist + "'NSPS', "
            '            insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
            '",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & lblFSName.Text.ToString & "', '" & lblPayorName.Text.ToString & _
            '"', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
            '"', '" & fakelableLoadDate.Text.ToString & "', 'NSPS', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
            '"(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NSPS') "
            '        End If

            If cbNSPSex.Checked Then
                Tinlist = Tinlist + "'NSPS (excl gyn onc)', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'NSPS (excl gyn onc)', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NSPS (excl gyn onc)') "
            End If

            If cbNSPSinc.Checked Then
                Tinlist = Tinlist + "'NSPS (incl gyn onc)', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'NSPS (incl gyn onc)', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NSPS (incl gyn onc)') "
            End If

            If cbNSPSonly.Checked Then
                Tinlist = Tinlist + "'NSPS (gyn onc only)', "
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
    ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
    "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtPayorName.Text, "'", "''") & _
    "', '" & fakelableLoadDate.Text.ToString & "', 'NSPS (gyn onc only)', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
    "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = 'NSPS (gyn onc only)') "
            End If

            If Trim(txtFree.Text) <> "" And Trim(txtFree.Text) <> " " Then
                Tinlist = Tinlist + "'" + Trim(Replace(txtFree.Text, "'", "''")) + "'"
                insertsql = insertsql + "insert into [DWH].[ProFee].[FeeSchedule] ([FeeSchedID], [FeeSchedName],[PayorName],[EffectiveDate],[EndDate]" & _
        ",[Default],[LoadDate],[TIN],[ManagersNotes]) Select " & postFS.ToString & ", '" & Replace(txtFSName.Text.ToString, "'", "''") & "', '" & Replace(txtPayorName.Text.ToString, "'", "''") & _
        "', '" & Replace(txtCurrentED.Text, "'", "''") & "', '" & Replace(txtCurrentEndD.Text, "'", "''") & "', '" & Replace(txtCurrentDefault.Text, "'", "''") & _
        "', '" & fakelableLoadDate.Text.ToString & "', '" & Replace(txtFree.Text, "'", "''") & "', '" & Replace(txtCurrentMN.Text, "'", "''") & "' where not exists " & _
        "(select * from DWH.ProFee.FeeSchedule where FeeSchedID = " & postFS.ToString & " and TIN = '" & Trim(Replace(txtFree.Text, "'", "''")) & "') "
            Else
                Tinlist = Left(Tinlist, Len(Tinlist) - 2)
            End If


            Dim updatesql As String
            Dim deletesql As String
            Dim allsql As String
            Dim cmd As SqlCommand

            updatesql = "Update [DWH].[ProFee].[FeeSchedule] set EffectiveDate = '" & Replace(txtCurrentED.Text, "'", "''") & "', EndDate = '" & _
                Replace(txtCurrentEndD.Text, "'", "''") & "', [Default] = '" & Replace(txtCurrentDefault.Text, "'", "''") & "', ManagersNotes = '" & _
                Replace(txtCurrentMN.Text, "'", "''") & "', [PayorName] = '" & Replace(txtPayorName.Text, "'", "''") & "', FeeSchedName = '" & _
                Replace(txtFSName.Text, "'", "''") & "' where FeeSchedID = " & postFS.ToString & " and TIN in (" & Tinlist.ToString & ")"

            deletesql = "Delete from [DWH].[ProFee].[FeeSchedule] where FeeSchedID = " & postFS.ToString & " and TIN not in (" & Tinlist.ToString & ")"

            allsql = updatesql & "; " & insertsql & "; " & deletesql
            Try

                Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New System.Data.SqlClient.SqlCommand(allsql, conn)
                    cmd.ExecuteNonQuery()
                    explantionlabel.Text = "Successfully Updated"
                    explantionlabel.DataBind()
                    ModalPopupExtender1.Show()
                End Using


            Catch ex As Exception
                explantionlabel.Text = "Could not Submit at this time"
                explantionlabel.DataBind()
                ModalPopupExtender1.Show()
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                Exit Sub
            End Try
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub OkButton_Click(sender As Object, e As System.EventArgs) Handles OkButton.Click
        ModalPopupExtender1.Hide()
    End Sub

    Private Sub ddlSelectedTINp2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedTINp2.SelectedIndexChanged

        Try

            Dim Sql2 As String
            Dim da As SqlDataAdapter
            Dim dr As DataRow

            p2lblPreviousTIN.Text = ddlSelectedTINp2.SelectedValue.ToString

            If ddlSelectedTINp2.SelectedValue.ToString = "novalueselected" Then
                ddlSelectedPayorp2.Enabled = False
                p2PanelCurrentFS.Visible = False
                Exit Sub
            Else
                p2PanelCurrentFS.Visible = True
                ddlSelectedPayorp2.Enabled = True

                ddlSelectedPayorp2.SelectedIndex = 0
                Try
                    Sql2 = "select PayorName, FeeSchedName, convert(varchar, EffectiveDate, 107) as [Effective Date], convert(varchar, EndDate, 107) as [End Date], [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                            "where TIN = '" & Replace(ddlSelectedTINp2.SelectedValue.ToString, "'", "''") & "'" & _
                            " order by PayorName, FeeSchedName, [Effective Date] desc, [Default], ManagersNotes"


                    tennis2.Clear()
                    'ds = New DataSet


                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da = New SqlDataAdapter(Sql2, conn2)
                        da.Fill(tennis2, "DEMO")
                        GridView1p2.DataSource = tennis2
                        GridView1p2.DataMember = "DEMO"
                        GridView1p2.DataBind()
                        ScrollPanelp2.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
                        ScrollPanelp2.BorderWidth = "1"
                        dr = tennis2.Tables(0).Rows(0)
                        p2txtCurrentED.Text = dr("Effective Date").ToString
                        p2txtCurrentED.DataBind()
                        p2txtCurrentDefault.Text = dr("Default").ToString
                        p2txtCurrentDefault.DataBind()
                        p2txtCurrentEndD.Text = dr("End Date").ToString
                        p2txtCurrentEndD.DataBind()
                        p2txtCurrentMN.Text = dr("ManagersNotes").ToString
                        p2txtCurrentMN.DataBind()
                        p2lblPayorName.Text = dr("PayorName").ToString
                        p2lblPayorName.DataBind()
                        p2lblFSName.Text = dr("FeeSchedName").ToString
                        p2lblFSName.DataBind()



                        GridView1p2.SelectedIndex = 0
                        GridView1p2.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                    End Using


                Catch ex As Exception
                    p2PanelCurrentFS.Visible = False
                End Try
            End If

            Dim stupidsql As String
            Dim x As String = "0"
            stupidsql = "Select count(*) from DWH.ProFee.FeeSchedule where TIN = '" & ddlSelectedTINp2.SelectedValue.ToString & "'"


            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim dc As SqlCommand
                dc = New SqlCommand(stupidsql, conn2)
                Try
                    conn2.Open()
                    x = Convert.ToString(dc.ExecuteScalar())
                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

            End Using

            If CInt(x) > 10 Then
                ScrollPanelp2.Height = "350"
            ElseIf CInt(x) > 5 Then
                ScrollPanelp2.Height = "200"
            ElseIf CInt(x) > 3 Then
                ScrollPanelp2.Height = "150"
            Else
                ScrollPanelp2.Height = "100"
            End If
            ScrollPanelp2.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlSelectedPayorp2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedPayorp2.SelectedIndexChanged

        Try

            Dim Sql2 As String
            Dim da As SqlDataAdapter
            Dim dr As DataRow


            Try
                tennis2.Clear()
                Sql2 = "select PayorName, FeeSchedName, convert(varchar, EffectiveDate, 107) as [Effective Date], convert(varchar, EndDate, 107) as [End Date], [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                        "where TIN = '" & Replace(ddlSelectedTINp2.SelectedValue.ToString, "'", "''") & "' and (PayorName = '" & Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' or '" & _
                        Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ') " & _
                        " order by PayorName, FeeSchedName, [Effective Date] desc, [Default], ManagersNotes"

                p2PanelCurrentFS.Visible = True

                'ds = New DataSet


                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da = New SqlDataAdapter(Sql2, conn2)
                    da.Fill(tennis2, "DEMO")
                    GridView1p2.DataSource = tennis2
                    GridView1p2.DataMember = "DEMO"
                    GridView1p2.DataBind()
                    dr = tennis2.Tables(0).Rows(0)
                    p2txtCurrentED.Text = dr("Effective Date").ToString
                    p2txtCurrentED.DataBind()
                    p2txtCurrentDefault.Text = dr("Default").ToString
                    p2txtCurrentDefault.DataBind()
                    p2txtCurrentEndD.Text = dr("End Date").ToString
                    p2txtCurrentEndD.DataBind()
                    p2txtCurrentMN.Text = dr("ManagersNotes").ToString
                    p2txtCurrentMN.DataBind()
                    p2lblPayorName.Text = dr("PayorName").ToString
                    p2lblPayorName.DataBind()
                    p2lblFSName.Text = dr("FeeSchedName").ToString
                    p2lblFSName.DataBind()

                    GridView1p2.SelectedIndex = 0
                    GridView1p2.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                End Using


            Catch ex As Exception
                p2PanelCurrentFS.Visible = False
            End Try

            Dim stupidsql As String
            Dim x As String = "0"
            stupidsql = "Select count(*) from DWH.ProFee.FeeSchedule where TIN = '" & Replace(ddlSelectedTINp2.SelectedValue.ToString, "'", "''") & "' and (PayorName = '" & Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' or '" & _
                        Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ') "



            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim dc As SqlCommand
                dc = New SqlCommand(stupidsql, conn2)
                Try
                    conn2.Open()
                    x = Convert.ToString(dc.ExecuteScalar())
                Catch ex As Exception
                    'gridname = "error1"
                End Try

            End Using

            If CInt(x) > 10 Then
                ScrollPanelp2.Height = "350"
            ElseIf CInt(x) > 5 Then
                ScrollPanel.Height = "200"
            ElseIf CInt(x) > 3 Then
                ScrollPanelp2.Height = "150"
            Else
                ScrollPanelp2.Height = "100"
            End If
            ScrollPanelp2.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub GridView1p2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1p2.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub GridView1p2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1p2.SelectedIndexChanged

        Try

            If gridname = GridView1p2.SelectedIndex.ToString Then
                Exit Sub
            Else
                Dim x As String
                Dim stupidsql As String
                'Dim dt As DataTable
                'Dim ds As DataSet
                Dim alpha As Integer
                x = "notfilled"
                stupidsql = "Select count(*) from (select PayorName, FeeSchedName, convert(varchar, EffectiveDate, 107) as [Effective Date], [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                        "where TIN = '" & Replace(ddlSelectedTINp2.SelectedValue.ToString, "'", "''") & "' and (PayorName = '" & Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' or '" & _
                        Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ' )) a"



                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    Dim dc As SqlCommand
                    dc = New SqlCommand(stupidsql, conn2)
                    Try
                        conn2.Open()
                        x = Convert.ToInt16(dc.ExecuteScalar())
                    Catch ex As Exception
                        'gridname = "error1"
                    End Try

                End Using
                alpha = 0
                gridname = GridView1p2.SelectedIndex.ToString
                While alpha < CInt(x)
                    If alpha = CInt(gridname) Then
                        alpha = alpha + 1
                    ElseIf alpha Mod 2 = 0 Then
                        GridView1p2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                        alpha = alpha + 1
                    Else
                        GridView1p2.Rows(alpha).BackColor = System.Drawing.Color.White
                        alpha = alpha + 1
                    End If
                End While

                GridView1p2.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")

            End If

            Dim dr As DataRow

            dr = tennis2.Tables(0).Rows(CInt(GridView1p2.SelectedIndex.ToString))

            p2txtCurrentED.Text = dr("Effective Date").ToString
            p2txtCurrentED.DataBind()
            p2txtCurrentDefault.Text = dr("Default").ToString
            p2txtCurrentDefault.DataBind()
            p2txtCurrentEndD.Text = dr("End Date").ToString
            p2txtCurrentEndD.DataBind()
            p2txtCurrentMN.Text = dr("ManagersNotes").ToString
            p2txtCurrentMN.DataBind()
            p2lblPayorName.Text = dr("PayorName").ToString
            p2lblPayorName.DataBind()
            p2lblFSName.Text = dr("FeeSchedName").ToString
            p2lblFSName.DataBind()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub p2UpdateButton_Click(sender As Object, e As System.EventArgs) Handles p2UpdateButton.Click

        Dim cmd As SqlCommand
        Dim updatesql As String
        updatesql = "Update [DWH].[ProFee].[FeeSchedule] set EffectiveDate = '" & Replace(p2txtCurrentED.Text, "'", "''") & "', EndDate = '" & _
         Replace(p2txtCurrentEndD.Text, "'", "''") & "', [Default] = '" & Replace(p2txtCurrentDefault.Text, "'", "''") & "', ManagersNotes = '" & _
         Replace(p2txtCurrentMN.Text, "'", "''") & "' where FeeSchedID = (Select FeeSchedID from (select FeeSchedID, Row_Number() over (partition by TIN order by " & _
         "PayorName, FeeSchedName, [EffectiveDate] desc, [Default], ManagersNotes) as rn from DWH.ProFee.FeeSchedule " & _
                    "where TIN = '" & Replace(ddlSelectedTINp2.SelectedValue.ToString, "'", "''") & "' and (PayorName = '" & Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' or '" & _
                    Replace(ddlSelectedPayorp2.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')) x where rn = " & GridView1p2.SelectedIndex.ToString & _
                    " + 1 )  and TIN = '" & Replace(ddlSelectedTINp2.SelectedValue.ToString, "'", "''") & "'"

        Try

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
                cmd.ExecuteNonQuery()
                explantionlabel2.Text = "Successfully Updated"
                explantionlabel2.DataBind()
                ModalPopupExtender2.Show()
            End Using


        Catch ex As Exception
            explantionlabel2.Text = "Could not Submit at this time"
            explantionlabel2.DataBind()
            ModalPopupExtender2.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try
    End Sub

    Private Sub OkButton2_Click(sender As Object, e As System.EventArgs) Handles OkButton2.Click
        ModalPopupExtender2.Hide()
    End Sub

    Private Sub gvGalenBlanks_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvGalenBlanks.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvGalenBlanks_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvGalenBlanks.SelectedIndexChanged

        Try

            If galenselectedrow = gvGalenBlanks.SelectedIndex.ToString Then
                Exit Sub
            Else
                Dim x As String
                Dim stupidsql As String
                'Dim dt As DataTable
                'Dim ds As DataSet
                Dim alpha As Integer
                x = "notfilled"
                stupidsql = "SELECT case when COUNT(*) > 100 then 100 else COUNT(*) end as cnt FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null" & _
                    " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')"



                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    Dim dc As SqlCommand
                    dc = New SqlCommand(stupidsql, conn2)
                    Try
                        conn2.Open()
                        x = Convert.ToInt16(dc.ExecuteScalar())
                    Catch ex As Exception
                        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    End Try

                End Using
                alpha = 0
                galenselectedrow = gvGalenBlanks.SelectedIndex.ToString
                While alpha < CInt(x)
                    If alpha = CInt(galenselectedrow) Then
                        alpha = alpha + 1
                    ElseIf alpha Mod 2 = 0 Then
                        gvGalenBlanks.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                        alpha = alpha + 1
                    Else
                        gvGalenBlanks.Rows(alpha).BackColor = System.Drawing.Color.White
                        alpha = alpha + 1
                    End If
                End While

                gvGalenBlanks.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")

            End If

            Dim Sql2 As String
            Dim da As SqlDataAdapter
            Dim dr As DataRow

            'Fuzzy logic
            Try
                Sql2 = "declare @num int = " & CInt(galenselectedrow) + 1 & _
            " declare @FAC varchar = (select fac from ( " & _
            "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
            " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
            "order by 2 ) z where rn = @num) " & _
            "declare @INSPLAN varchar(5) = (select INSPLAN from ( " & _
            "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
            " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
            "order by 2 ) z where rn = @num) " & _
            "declare @IDGroup varchar(max) = (select IDGroup from ( " & _
            "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
            " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
            "order by 2 ) z where rn = @num) " & _
            "Declare @INSPLANNAME varchar(max) = (select INSPLAN_NAME from ( " & _
            "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
            " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
            "order by 2 ) z where rn = @num) " & _
            "select top 25 " & _
            "[FAC]  " & _
            "      ,[INSPLAN] " & _
            "      ,[IDGroup] " & _
            "      ,[INSPLAN_NAME] " & _
            "      ,[STARINSPLANCODE] " & _
            "      ,[CONTRACTID] " & _
            "      ,[CONTRACT_NAME] " & _
            "      ,[CONTRACTID_SUM] " & _
            "      ,[CONTRACTID_SUM_NAME] " & _
            "      from ( " & _
            "select *, ROW_NUMBER() over (partition by FAC, INSPLAN, INSPLAN_NAME order by ordering desc) as rn from (       " & _
            "select *,  " & _
            "case when FAC = @FAC then .6 else 0 end + case when IDGroup = @IDGroup then .3 else 0 end + " & _
            "case when INSPLAN = @INSPLAN then 8 when LEFT(INSPLAN, 3) = LEFT(@INSPLAN, 3) then 1.2 " & _
            "when RIGHT(INSPLAN, 3) = RIGHT(@INSPLAN, 3) then 1 else 0 end + " & _
            "case when @INSPLANNAME = INSPLAN_NAME then 300 when left(INSPLAN_NAME, 8) = LEFT(@INSPLANNAME, 8) then 200 " & _
            "when RIGHT(INSPLAN_NAME, 8) = RIGHT(@INSPLANNAME, 8) then 180 " & _
            "when INSPLAN_NAME like ('%' + left(@INSPLANNAME, CHARINDEX(' ', INSPLAN_NAME)) + '%') and CHARINDEX(' ', INSPLAN_NAME) > 0 then 150 " & _
            "when @INSPLANNAME like ('%' + left(INSPLAN_NAME,CHARINDEX(' ', INSPLAN_NAME)) + '%') and CHARINDEX(' ', INSPLAN_NAME) > 0 then 150 else 0 end " & _
            " as ordering " & _
            " from  " & _
            "[DWH].[dbo].[ContractID_Galen_LU] " & _
            "where STARINSPLANCODE is not null " & _
            ") x ) z " & _
            "where rn < 3 " & _
            "order by ordering desc"

                'tennis.Clear()
                Galenset.Clear()


                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da = New SqlDataAdapter(Sql2, conn2)
                    da.Fill(Galenset, "DEMO")
                    gvGalenChosen.DataSource = Galenset
                    gvGalenChosen.DataMember = "DEMO"
                    gvGalenChosen.DataBind()
                    ScrollPanel2p3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
                    ScrollPanel2p3.BorderWidth = "1"
                    dr = Galenset.Tables(0).Rows(0)
                    txtgalenchosenStarInsplanCode.Text = dr("STARINSPLANCODE").ToString
                    txtgalenchosenStarInsplanCode.DataBind()

                    Dim beta As Integer = 1
                    While beta < 25
                        If beta Mod 2 = 0 Then
                            gvGalenChosen.Rows(beta).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                            beta = beta + 1
                        Else
                            gvGalenChosen.Rows(beta).BackColor = System.Drawing.Color.White
                            beta = beta + 1
                        End If
                    End While
                    gvGalenChosen.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvGalenChosen_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvGalenChosen.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvGalenChosen_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvGalenChosen.SelectedIndexChanged
        Try

            If galenchosenrow = gvGalenChosen.SelectedIndex.ToString Then
                Exit Sub
            Else

                galenchosenrow = gvGalenChosen.SelectedIndex.ToString
                Dim dr As DataRow
                dr = Galenset.Tables(0).Rows(galenchosenrow)
                txtgalenchosenStarInsplanCode.Text = dr("STARINSPLANCODE").ToString
                txtgalenchosenStarInsplanCode.DataBind()

                Dim alpha As Integer = 0

                While alpha < 25
                    If alpha Mod 2 = 0 Then
                        gvGalenChosen.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                        alpha = alpha + 1
                    Else
                        gvGalenChosen.Rows(alpha).BackColor = System.Drawing.Color.White
                        alpha = alpha + 1
                    End If
                End While

                gvGalenChosen.Rows(CInt(galenchosenrow)).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnGalenUpdate_Click(sender As Object, e As System.EventArgs) Handles btnGalenUpdate.Click


        If Trim(txtgalenchosenStarInsplanCode.Text) = "" Then
            Galenexplantionlabel.Text = "Please enter a STARINSPLANCODE value"
            Galenexplantionlabel.DataBind()
            Exit Sub
        End If

        Dim chk As CheckBox
        Dim galenupdatesql As String = ""
        Dim cmd As SqlCommand
        Dim counter As Integer = 0

        For i As Integer = 0 To gvGalenBlanks.Rows.Count - 1
            chk = CType(gvGalenBlanks.Rows(i).FindControl("chk"), CheckBox)
            If chk.Checked = True Then
                counter = counter + 1
                galenupdatesql = galenupdatesql & "Update [DWH].[dbo].[ContractID_Galen_LU] set STARINSPLANCODE = '" & Replace(txtgalenchosenStarInsplanCode.Text, "'", "''") & _
                    "' where FAC = '" & Replace(gvGalenBlanks.Rows(i).Cells(2).Text, "'", "''") & "' and INSPLAN = '" & Replace(gvGalenBlanks.Rows(i).Cells(3).Text, "'", "''") & "' and IDGroup = '" & _
                    Replace(gvGalenBlanks.Rows(i).Cells(4).Text, "'", "''") & "' and INSPLAN_NAME = '" & Replace(gvGalenBlanks.Rows(i).Cells(5).Text, "'", "''") & "' "
            End If
        Next

        Try

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(galenupdatesql, conn)
                cmd.ExecuteNonQuery()
                Galenexplantionlabel.Text = counter.ToString & " Rows Successfully Updated"
                Galenexplantionlabel.DataBind()
                ModalPopupExtenderGalen.Show()
            End Using


        Catch ex As Exception
            Galenexplantionlabel.Text = "Could not Submit at this time"
            Galenexplantionlabel.DataBind()
            ModalPopupExtenderGalen.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try

    End Sub

    Private Sub GalenOkButton_Click(sender As Object, e As System.EventArgs) Handles GalenOkButton.Click
        ModalPopupExtenderGalen.Hide()
    End Sub

    Private Sub btnGalenUpdateAll_Click(sender As Object, e As System.EventArgs) Handles btnGalenUpdateAll.Click

        Try

            If Trim(txtgalenchosenStarInsplanCode.Text) = "" Then
                Galenexplantionlabel.Text = "Please enter a STARINSPLANCODE value"
                Galenexplantionlabel.DataBind()
                Exit Sub
            End If

            If ddlSelectedINSPLAN.SelectedValue = "novalueselected" Then
                Galenexplantionlabel.Text = "No INSPLAN_NAME was selected."
                ModalPopupExtenderGalen.Show()
                Exit Sub
            Else
                Dim galenupdatesql As String
                Dim chk As CheckBox
                Dim cmd As SqlCommand
                galenupdatesql = "Update [DWH].[dbo].[ContractID_Galen_LU] set STARINSPLANCODE = '" & Replace(txtgalenchosenStarInsplanCode.Text, "'", "''") & _
            "' where INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue, "'", "''") & "' and STARINSPLANCODE is null "

                Try

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        cmd = New System.Data.SqlClient.SqlCommand(galenupdatesql, conn)
                        cmd.ExecuteNonQuery()
                        Galenexplantionlabel.Text = "Rows Successfully Updated"
                        Galenexplantionlabel.DataBind()
                        ModalPopupExtenderGalen.Show()
                    End Using


                Catch ex As Exception
                    Galenexplantionlabel.Text = "Could not Submit at this time"
                    Galenexplantionlabel.DataBind()
                    ModalPopupExtenderGalen.Show()
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    Exit Sub
                End Try
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlSelectedINSPLAN_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedINSPLAN.SelectedIndexChanged

        Try


            Dim Sql21 As String
            Dim ds1 As New DataSet
            Dim da1 As SqlDataAdapter

            Try
                Sql21 = "SELECT TOP 100 [FAC]" & _
          ",[INSPLAN]" & _
          ",[IDGroup]" & _
          ",[INSPLAN_NAME]" & _
          ",[STARINSPLANCODE]" & _
          ",[CONTRACTID]" & _
          ",[CONTRACT_NAME]" & _
          ",[CONTRACTID_SUM]" & _
          ",[CONTRACTID_SUM_NAME]" & _
               " FROM [DWH].[dbo].[ContractID_Galen_LU] " & _
               " where(STARINSPLANCODE Is null)" & _
            "and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
             "order by 2"


                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da1 = New SqlDataAdapter(Sql21, conn2)
                    da1.Fill(ds1, "DEMO")
                    gvGalenBlanks.DataSource = ds1
                    gvGalenBlanks.DataMember = "DEMO"
                    gvGalenBlanks.DataBind()

                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try


            Dim x As String
            Dim stupidsql As String
            'Dim dt As DataTable
            'Dim ds As DataSet
            Dim alpha As Integer
            x = "notfilled"
            stupidsql = "SELECT case when COUNT(*) > 100 then 100 else COUNT(*) end as cnt FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null" & _
                " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')"



            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim dc As SqlCommand
                dc = New SqlCommand(stupidsql, conn2)
                Try
                    conn2.Open()
                    x = Convert.ToInt16(dc.ExecuteScalar())
                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

            End Using
            alpha = 0
            galenselectedrow = 0
            While alpha < CInt(x)
                If alpha = CInt(galenselectedrow) Then
                    alpha = alpha + 1
                ElseIf alpha Mod 2 = 0 Then
                    gvGalenBlanks.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                    alpha = alpha + 1
                Else
                    gvGalenBlanks.Rows(alpha).BackColor = System.Drawing.Color.White
                    alpha = alpha + 1
                End If
            End While

            gvGalenBlanks.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")

            If ddlSelectedINSPLAN.SelectedValue = "novalueselected" Then
                Exit Sub
            Else

                Dim Sql2 As String
                Dim da As SqlDataAdapter
                Dim dr As DataRow

                'Fuzzy logic
                Try
                    Sql2 = "declare @num int = " & CInt(galenselectedrow) + 1 & _
                " declare @FAC varchar = (select fac from ( " & _
                "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
                " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
                "order by 2 ) z where rn = @num) " & _
                "declare @INSPLAN varchar(5) = (select INSPLAN from ( " & _
                "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
                " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
                "order by 2 ) z where rn = @num) " & _
                "declare @IDGroup varchar(max) = (select IDGroup from ( " & _
                "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
                " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
                "order by 2 ) z where rn = @num) " & _
                "Declare @INSPLANNAME varchar(max) = (select INSPLAN_NAME from ( " & _
                "SELECT  top 100 *, ROW_NUMBER() over (order by INSPLAN) as rn FROM [DWH].[dbo].[ContractID_Galen_LU]  where STARINSPLANCODE is null " & _
                " and (INSPLAN_NAME = '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedINSPLAN.SelectedValue.ToString, "'", "''") & "' = 'novalueselected')" & _
                "order by 2 ) z where rn = @num) " & _
                "select top 25 " & _
                "[FAC]  " & _
                "      ,[INSPLAN] " & _
                "      ,[IDGroup] " & _
                "      ,[INSPLAN_NAME] " & _
                "      ,[STARINSPLANCODE] " & _
                "      ,[CONTRACTID] " & _
                "      ,[CONTRACT_NAME] " & _
                "      ,[CONTRACTID_SUM] " & _
                "      ,[CONTRACTID_SUM_NAME] " & _
                "      from ( " & _
                "select *, ROW_NUMBER() over (partition by FAC, INSPLAN, INSPLAN_NAME order by ordering desc) as rn from (       " & _
                "select *,  " & _
                "case when FAC = @FAC then .6 else 0 end + case when IDGroup = @IDGroup then .3 else 0 end + " & _
                "case when INSPLAN = @INSPLAN then 8 when LEFT(INSPLAN, 3) = LEFT(@INSPLAN, 3) then 1.2 " & _
                "when RIGHT(INSPLAN, 3) = RIGHT(@INSPLAN, 3) then 1 else 0 end + " & _
                "case when @INSPLANNAME = INSPLAN_NAME then 300 when left(INSPLAN_NAME, 8) = LEFT(@INSPLANNAME, 8) then 200 " & _
                "when RIGHT(INSPLAN_NAME, 8) = RIGHT(@INSPLANNAME, 8) then 180 " & _
                "when INSPLAN_NAME like ('%' + left(@INSPLANNAME, CHARINDEX(' ', INSPLAN_NAME)) + '%') and CHARINDEX(' ', INSPLAN_NAME) > 0 then 150 " & _
                "when @INSPLANNAME like ('%' + left(INSPLAN_NAME,CHARINDEX(' ', INSPLAN_NAME)) + '%') and CHARINDEX(' ', INSPLAN_NAME) > 0 then 150 else 0 end " & _
                " as ordering " & _
                " from  " & _
                "[DWH].[dbo].[ContractID_Galen_LU] " & _
                "where STARINSPLANCODE is not null " & _
                ") x ) z " & _
                "where rn < 3 " & _
                "order by ordering desc"

                    'tennis.Clear()
                    Galenset.Clear()


                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da = New SqlDataAdapter(Sql2, conn2)
                        da.Fill(Galenset, "DEMO")
                        gvGalenChosen.DataSource = Galenset
                        gvGalenChosen.DataMember = "DEMO"
                        gvGalenChosen.DataBind()
                        ScrollPanel2p3.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
                        ScrollPanel2p3.BorderWidth = "1"
                        dr = Galenset.Tables(0).Rows(0)
                        txtgalenchosenStarInsplanCode.Text = dr("STARINSPLANCODE").ToString
                        txtgalenchosenStarInsplanCode.DataBind()

                        Dim beta As Integer = 1
                        While beta < 25
                            If beta Mod 2 = 0 Then
                                gvGalenChosen.Rows(beta).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                                beta = beta + 1
                            Else
                                gvGalenChosen.Rows(beta).BackColor = System.Drawing.Color.White
                                beta = beta + 1
                            End If
                        End While
                        gvGalenChosen.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                    End Using


                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    'Private Sub btnDeleteAetna_Click(sender As Object, e As System.EventArgs) Handles btnDeleteAetna.Click

    '    Dim sql1 As String = "Delete FROM DWH.ProFee.FeeScheduleDetail " & _
    '        " where FeeSchedID in (SELECT FeeSchedID " & _
    '        "  FROM [DWH].[ProFee].[FeeSchedule] " & _
    '        "  where PayorName like '%Aetna%') "

    '    Dim sql2 As String = "delete from [DWH].[ProFee].[FeeSchedule] " & _
    '        " where PayorName like '%Aetna%' "

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WEBFDconn").ConnectionString)

    '        Dim cmd As New SqlCommand(sql1, conn)
    '        Dim cmd2 As New SqlCommand(sql2, conn)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        cmd.ExecuteNonQuery()
    '        cmd2.ExecuteNonQuery()

    '    End Using

    'End Sub

    Sub PopulateddlSelectedTINs()


    End Sub

    Private Sub ddlSelectedTINs_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSelectedTINs.SelectedIndexChanged

        Dim Sql2 As String
        Dim da As SqlDataAdapter
        Dim dr As DataRow

        If ddlSelectedTINs.SelectedIndex = 0 Then
            ddlSelectedFSChangedFunction()
            Exit Sub

        End If
        Try
            Sql2 = "select PayorName, FeeSchedName, convert(varchar, EffectiveDate, 107) as [Effective Date], convert(varchar, EndDate, 107) as [End Date], [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
                    "where FeeSchedID in (select FeeSchedID from (select FeeSchedID, DENSE_RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc, [EndDate] desc,  " & _
                    "[LoadDate] desc,[Default], [ManagersNotes]) as rankin from DWH.ProFee.FeeSchedule where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
                    Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    ") x where rankin in " & ddlSelectedTINs.SelectedValue.ToString & _
                    ") group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] order by EffectiveDate desc, EndDate desc,  LoadDate desc"


            'ds = New DataSet
            Dim rowboat As DataRow
            tennis.Clear()

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(Sql2, conn2)
                da.Fill(tennis, "DEMO")
                GridView1.DataSource = tennis
                GridView1.DataMember = "DEMO"
                GridView1.DataBind()
                rowboat = tennis.Tables(0).Rows(0)
                txtCurrentED.Text = rowboat("Effective Date").ToString
                txtCurrentED.DataBind()
                txtCurrentDefault.Text = rowboat("Default").ToString
                txtCurrentDefault.DataBind()
                txtCurrentEndD.Text = rowboat("End Date").ToString
                txtCurrentEndD.DataBind()
                txtCurrentMN.Text = rowboat("ManagersNotes").ToString
                txtCurrentMN.DataBind()
                txtPayorName.Text = rowboat("PayorName").ToString
                txtPayorName.DataBind()
                txtFSName.Text = rowboat("FeeSchedName").ToString
                txtFSName.DataBind()

                FakelblStartDate.Text = rowboat("Effective Date").ToString
                FakelblStartDate.DataBind()
                FakelblDefault.Text = rowboat("Default").ToString
                FakelblDefault.DataBind()
                FakelblEndDate.Text = rowboat("End Date").ToString
                FakelblEndDate.DataBind()
                FakelblManagersNotes.Text = rowboat("ManagersNotes").ToString
                FakelblManagersNotes.DataBind()
                FakelblPayorName.Text = rowboat("PayorName").ToString
                FakelblPayorName.DataBind()
                FakelblFSName.Text = rowboat("FeeSchedName").ToString
                FakelblFSName.DataBind()

            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


        Dim x As String
        'Dim stupidsql As String
        'Dim dt As DataTable
        'Dim ds As DataSet
        x = "0"
        'stupidsql = "Select count(*) from (select PayorName, FeeSchedName, EffectiveDate, EndDate, [Default], ManagersNotes from DWH.ProFee.FeeSchedule " & _
        '            "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "'  and (FeeSchedName = '" & _
        '            Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
        '            " group by [FeeSchedName] ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes]) a"

        x = tennis.Tables(0).Rows.Count

        'Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
        '    Dim dc As SqlCommand
        '    dc = New SqlCommand(stupidsql, conn2)
        '    Try
        '        conn2.Open()
        '        x = Convert.ToString(dc.ExecuteScalar())
        '    Catch ex As Exception
        '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        '    End Try

        'End Using

        If CInt(x) > 10 Then
            ScrollPanel.Height = "350"
        ElseIf CInt(x) > 5 Then
            ScrollPanel.Height = "200"
        ElseIf CInt(x) > 3 Then
            ScrollPanel.Height = "150"
        Else
            ScrollPanel.Height = "100"
        End If
        ScrollPanel.DataBind()


        Dim dq As SqlCommand
        Dim countsql As String
        'Dim y As Integer

        countsql = "Select COUNT(distinct FeeSchedID) from DWH.ProFee.FeeSchedule fs where " & _
        "exists(select * from (" & _
        "Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
        " ,[LoadDate] desc,[Default] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] " & _
        ",[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule where PayorName = '" & _
        Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
        "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
        " Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName" & _
        ") a where rankin = " & Mid(ddlSelectedTINs.SelectedValue, 2, If(ddlSelectedTINs.SelectedValue.IndexOf(" ") > 0, ddlSelectedTINs.SelectedValue.IndexOf(" ") - 1, ddlSelectedTINs.SelectedValue.IndexOf(")")) - 1) & _
        " and a.[Default] = fs.[Default] and a.EffectiveDate = fs.EffectiveDate " & _
        "and a.EndDate = fs.EndDate and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName )"

        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            dq = New SqlCommand(countsql, conn2)
            Try
                conn2.Open()
                y = Convert.ToInt16(dq.ExecuteScalar())
            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

        End Using
        CountLabel.Text = ""
        If y > 1 Then
            CountLabel.Text = "There are " & y.ToString & " fee schedules which meet the above criteria.  Which " & _
                "one would you like to update?"



            Dim grid2sql As String

            grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, case when CPTRank = 1 then Replace(Replace(TIN, '||', ' '), '|', '') else NULL end as TINs," & _
                    "CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
                    "from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
                    "left join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID " & _
                    "where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc " & _
                    ",[LoadDate] desc,[Default]  ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
                    "where PayorName = '" & Replace(ddlSelectedPayor.SelectedValue.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & _
                    "' or '" & Replace(ddlSelectedFS.SelectedValue.ToString, "'", "''") & "' = '  -- None --  ')" & _
                    "Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
                    "where rankin = " & Mid(ddlSelectedTINs.SelectedValue, 2, If(ddlSelectedTINs.SelectedValue.IndexOf(" ") > 0, ddlSelectedTINs.SelectedValue.IndexOf(" ") - 1, ddlSelectedTINs.SelectedValue.IndexOf(")")) - 1) & _
                    " And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate " & _
                    "and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null or fsd.FeeSchedId is null)" & _
                    "and TIN = (select MAX(TIN) from DWH.ProFee.FeeSchedule where FeeSchedID = fs.FeeSchedID)) b where(CPTRank <= 10) " & _
                    "order by FSRank asc, CPT asc, Modifier asc"

            'Version2
            '    grid2sql = "Select case when CPTRank = 1 then FSRank else NULL end as FeeSchedule, TIN," & _
            '"CPT, Modifier, Fac_Allow, NonFac_Allow, Fac_Criteria, NonFac_Criteria, [Default] " & _
            '"from (Select fsd.*, TIN, RANK() over (partition by fs.FeeSchedID order by CPT, Modifier) as CPTRank, DENSE_RANK() over (order by fs.FeeSchedID) as FSRank from DWH.ProFee.FeeSchedule fs " & _
            '"join DWH.ProFee.FeeScheduleDetail fsd on fs.FeeSchedID = fsd.FeeSchedID where exists (select * from (Select RANK() over (partition by PayorName order by FeeSchedName, [EffectiveDate] ,[EndDate] " & _
            '",[Default] ,[LoadDate] ,[ManagersNotes]) as rankin ,[PayorName] ,[EffectiveDate] ,[EndDate] ,[Default] ,[LoadDate] ,[ManagersNotes] from DWH.ProFee.FeeSchedule " & _
            '"where PayorName = '" & ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
            '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ')" & _
            '"Group by EffectiveDate, EndDate, [Default], LoadDate, ManagersNotes, FeeSchedName, PayorName) a " & _
            '"where(rankin = " & 1 & "And a.[Default] = fs.[Default] And a.EffectiveDate = fs.EffectiveDate And a.EndDate = fs.EndDate) " & _
            '"and a.LoadDate = fs.LoadDate and a.ManagersNotes = fs.ManagersNotes and a.PayorName = fs.PayorName ) and (Fac_Allow is not null or Modifier is not null)) b where(CPTRank <= 10) " & _
            '"order by FSRank asc, CPT asc, Modifier asc"

            Try

                Dim da2 As New SqlDataAdapter
                Dim dss As New DataSet

                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da2 = New SqlDataAdapter(grid2sql, conn2)
                    da2.Fill(dss, "DEMO")
                    GridView2.DataSource = dss
                    GridView2.DataMember = "DEMO"
                    GridView2.DataBind()
                    GridView2.Visible = True
                    redpanel.Visible = True
                    PanelRed.Visible = True
                    UpdateButton.Enabled = False
                    PanelRed.BorderColor = System.Drawing.Color.Red

                    Dim alpha As Int16 = 0
                    While alpha < (y) * 10
                        If Fix(alpha / 10) Mod 2 = 0 Then
                            GridView2.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                            alpha = alpha + 1
                        Else
                            GridView2.Rows(alpha).BackColor = System.Drawing.Color.White
                            alpha = alpha + 1
                        End If
                    End While
                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        Else
            'GridView2.Columns.Clear()
            'GridView2.DataBind()
            GridView2.Visible = False
            redpanel.Visible = False
            PanelRed.Visible = False
            UpdateButton.Enabled = True
        End If
        CountLabel.DataBind()
        If x > 0 Then
            GridView1.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
        End If

        CurrentFSPanel.Visible = True

        Dim currentfssql As String

        'currentfssql = "select FeeSchedID, TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
        'ddlSelectedPayor.SelectedValue.ToString & "' and (FeeSchedName = '" & ddlSelectedFS.SelectedValue.ToString & _
        '"' or '" & ddlSelectedFS.SelectedValue.ToString & "' = '  -- None --  ') and '" & _
        'txtCurrentED.Text.ToString & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & txtCurrentEndD.Text.ToString & _
        '"' and [Default] = '" & txtCurrentDefault.Text.ToString & "' and ManagersNotes = '" & _
        'txtCurrentMN.Text.ToString & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc, [Default] ,[LoadDate] ,[ManagersNotes]"


        currentfssql = "        DECLARE @concatenated NVARCHAR(1000) " & _
        "SET @concatenated = '' " & _
        "SELECT @concatenated = @concatenated + '|' + TIN + '|' FROM DWH.ProFee.FeeSchedule where FeeSchedID in (select FeeSchedID from (select FeeSchedID, Dense_Rank() over (order by LoadDate desc) as den from DWH.ProFee.FeeSchedule FeeSchedID  where PayorName = '" & _
        Replace(FakelblPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(FakelblFSName.Text.ToString, "'", "''") & _
        "') and '" & _
        Replace(txtCurrentED.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(txtCurrentEndD.Text.ToString, "'", "''") & _
        "' and [Default] = '" & Replace(txtCurrentDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
        Replace(txtCurrentMN.Text.ToString, "'", "''") & "' ) x where den = 1 ) order by TIN, FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate] desc, [Default] ,[ManagersNotes]" & _
        "SELECT top 1 FeeSchedID, LoadDate, left(@concatenated, LEN(@concatenated) ) as TIN from DWH.ProFee.FeeSchedule where PayorName = '" & _
        Replace(FakelblPayorName.Text.ToString, "'", "''") & "' and (FeeSchedName = '" & Replace(FakelblFSName.Text.ToString, "'", "''") & "') and '" & _
        Replace(txtCurrentED.Text.ToString, "'", "''") & "' = convert(varchar, EffectiveDate, 107) and convert(varchar, EndDate, 107) = '" & Replace(txtCurrentEndD.Text.ToString, "'", "''") & _
         "' and [Default] = '" & Replace(txtCurrentDefault.Text.ToString, "'", "''") & "' and ManagersNotes = '" & _
        Replace(txtCurrentMN.Text.ToString, "'", "''") & "' order by FeeSchedName, [EffectiveDate] desc ,[EndDate] desc ,[LoadDate] desc, [Default] ,[ManagersNotes]"


        Dim dfs As DataSet
        Dim dfa As New SqlDataAdapter
        Dim cmd As New SqlCommand
        Dim dr2 As DataRow


        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            dfs = New DataSet
            cmd = New SqlCommand(currentfssql, conn)
            dfa.SelectCommand = cmd
            dfa.SelectCommand.CommandTimeout = 86400
            dfa.Fill(dfs, "TIns")
            dr2 = dfs.Tables(0).Rows(0)
            fakelabelTIN.Text = dr2("TIN").ToString
            lblPreviousTIN.Text = Replace(Replace(dr2("TIN").ToString, "||", ", "), "|", "")
            lblPreviousTIN.DataBind()
            postFS = dr2("FeeSchedID").ToString
            fakelableLoadDate.Text = dr2("LoadDate").ToString


        End Using




    End Sub

    Private Sub gvGalenUpdates_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvGalenUpdates.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    'Private Sub gvGalenUpdates_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvGalenUpdates.RowEditing
    '    Dim counter As Integer = 0
    '    For Each row As GridViewRow In gvGalenUpdates.Rows
    '        If counter = gvGalenUpdates.EditIndex Then
    '            row.BackColor = Drawing.Color.LightSteelBlue
    '        ElseIf counter Mod 2 = 0 Then
    '            row.BackColor = Drawing.Color.White
    '        Else
    '            row.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee4ce")
    '        End If
    '        counter = counter + 1

    '    Next

    'End Sub

    Private Sub gvGalenUpdates_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles gvGalenUpdates.SelectedIndexChanged
        Dim counter As Integer = 0
        For Each row As GridViewRow In gvGalenUpdates.Rows
            If counter Mod 2 = 0 Then
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                row.BackColor = System.Drawing.Color.White
            End If
            counter = counter + 1

        Next

        gvGalenUpdates.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")

        lblGalenUpdatesContractID.Text = gvGalenUpdates.SelectedRow.Cells(5).Text
        lblGalenUpdatesContractIDSUM.Text = gvGalenUpdates.SelectedRow.Cells(7).Text
        lblGalenUpdatesContractIDSUMNAME.Text = gvGalenUpdates.SelectedRow.Cells(8).Text
        lblGalenUpdatesContractName.Text = gvGalenUpdates.SelectedRow.Cells(6).Text
        lblGalenUpdatesIDGroup.Text = gvGalenUpdates.SelectedRow.Cells(2).Text
        lblGalenUpdatesINSPLAN.Text = gvGalenUpdates.SelectedRow.Cells(1).Text
        lblGalenUpdatesINSPLAN_NAME.Text = gvGalenUpdates.SelectedRow.Cells(3).Text
        txtGalenUpdatesSTARINSPLANCODE.Text = gvGalenUpdates.SelectedRow.Cells(4).Text
        lblGalenUpdatesDC_FROM.Text = gvGalenUpdates.DataKeys(gvGalenUpdates.SelectedRow.RowIndex).Item("DC_FROM").ToString
        lblGalenUpdatesToDate.Text = gvGalenUpdates.SelectedRow.Cells(10).Text
        lblGalenUpdatesFromDate.Text = gvGalenUpdates.SelectedRow.Cells(9).Text


    End Sub

    Private Sub btnGUUpdateCode_Click(sender As Object, e As System.EventArgs) Handles btnGUUpdateCode.Click


        Try

            Dim cmd As New SqlCommand
            Dim sqlupdate As String = "Update DWH.dbo.ContractID_Galen_LU set STARINSPLANCODE = '" & txtGalenUpdatesSTARINSPLANCODE.Text & "' " & _
                " where INSPLAN = '" & lblGalenUpdatesINSPLAN.Text & "' and IDGroup = '" & lblGalenUpdatesIDGroup.Text & "' and " & _
                " INSPLAN_NAME = '" & lblGalenUpdatesINSPLAN_NAME.Text & "' and DC_FROM = '" & lblGalenUpdatesDC_FROM.Text & "'"



            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(sqlupdate, conn)
                cmd.ExecuteNonQuery()
                GalenUpdatesexplantionlabel.Text = "Successfully Updated"
                GalenUpdatesexplantionlabel.DataBind()
                ModalPopupExtenderGalenUpdates.Show()
            End Using


        Catch ex As Exception
            GalenUpdatesexplantionlabel.Text = "Could not Submit at this time"
            GalenUpdatesexplantionlabel.DataBind()
            ModalPopupExtenderGalenUpdates.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try



    End Sub

    Private Sub cbEditInsplans_CheckedChanged(sender As Object, e As EventArgs) Handles cbEditInsplans.CheckedChanged
        If cbEditInsplans.Checked = True Then
            tblGalenEdits.Visible = True
            btnGUUpdateCode.Visible = True
            pnlScrollPanelGalenUpdates.Height = 400
        Else
            tblGalenEdits.Visible = False
            btnGUUpdateCode.Visible = False
            pnlScrollPanelGalenUpdates.Height = 800
        End If
    End Sub

    Private Sub ddlPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPageSize.SelectedIndexChanged
        gvGalenUpdates.PageSize = ddlPageSize.SelectedValue
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
                'File.Delete(strFileName)
            End If
        Catch generatedExceptionName As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), generatedExceptionName.ToString())
            Return False
        End Try
        Return True
    End Function

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        'required to avoid the runtime error "  
        'Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    End Sub

    Private Sub btnExportGalen_Click(sender As Object, e As EventArgs) Handles btnExportGalen.Click
        Me.ExportGridToExcel()
    End Sub

    Private Sub ExportGridToExcel()
        Try
            'Create datatable and populate with SQLDatasource 
            Dim dt As New DataTable("GridView_Data")
            Dim args As New DataSourceSelectArguments()
            Dim view As DataView = DirectCast(dsGVGalenUpdates.[Select](args), DataView)
            dt = view.ToTable()

            'Using ClosedXML Library (exports .xlsx)
            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dt)
                wb.SaveAs("\\northside.local\users\cw996788\Desktop\Test.pmt")
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

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnExportGalen_Click1(sender As Object, e As EventArgs) Handles btnExportGalen.Click
        'Dim dv2 As DataView = dsGVGalenUpdates.Select(DataSourceSelectArguments.Empty)
        'Dim ds2 As DataTable = dv2.ToTable()
        'ds2.TableName = "Galen STARINSPLAN"

        'Dim FI As New FileInfo(Path)
        'Dim stringWriter As New StringWriter()
        'Dim htmlWrite As New HtmlTextWriter(stringWriter)
        'Dim DataGrd As New DataGrid()
        'DataGrd.DataSource = dsGVGalenUpdates
        'DataGrd.DataBind()


        'DataGrd.RenderControl(htmlWrite)
        'Dim directory__1 As String = Path.Substring(0, Path.LastIndexOf("\"))
        '' GetDirectory(Path);
        'If Not Directory.Exists(directory__1) Then
        '    Directory.CreateDirectory(directory__1)
        'End If

        'Dim vw As New System.IO.StreamWriter(Path, True)
        'stringWriter.ToString().Normalize()
        'vw.Write(stringWriter.ToString())
        'vw.Flush()
        'vw.Close()
        'WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString())


        '        FileInfo FI = new FileInfo(Path);
        'StringWriter stringWriter = new StringWriter();
        'HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
        'DataGrid DataGrd = new DataGrid();
        'DataGrd.DataSource = dt1;
        'DataGrd.DataBind();

        'DataGrd.RenderControl(htmlWrite);
        'string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
        '        If (!Directory.Exists(Directory)) Then
        '{
        '    Directory.CreateDirectory(directory);
        '}

        'System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
        'stringWriter.ToString().Normalize();
        'vw.Write(stringWriter.ToString());
        'vw.Flush();
        'vw.Close();
        'WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());


        Exit Sub

        'Try
        '    Dim dv As DataView = dsGVGalenUpdates.Select(DataSourceSelectArguments.Empty)
        '    Dim ds As DataTable = dv.ToTable()
        '    ds.TableName = "Galen STARINSPLAN"

        '    Dim FileName As String

        '    If ddlGalenUpdatesIDGroup.SelectedValue = " -- (optional) -- " Then
        '        FileName = "Galen_STARINSPLAN"
        '    Else
        '        FileName = "Galen_" & ddlGalenUpdatesIDGroup.SelectedValue.ToString & "_STARINSPLAN"
        '    End If

        '    Dim rbltesting As Integer = 1


        '    If rbltesting = 0 Then

        '        Response.Clear()
        '        Response.ContentType = "text/csv"
        '        Response.AddHeader("content-disposition", "attachment;filename=" & FileName & ".csv")

        '        Dim sb As New StringBuilder()
        '        For i As Integer = 0 To ds.Columns.Count - 1
        '            sb.Append(Replace(ds.Columns(i).ColumnName, "CarriageReturn", " ") + ",")
        '        Next
        '        sb.Append(Environment.NewLine)

        '        For j As Integer = 0 To ds.Rows.Count - 1
        '            For k As Integer = 0 To ds.Columns.Count - 1
        '                sb.Append(ds.Rows(j)(k).ToString() + ",")
        '            Next
        '            sb.Append(Environment.NewLine)
        '        Next



        '        Response.Write(sb.ToString())
        '        Response.End()

        '    ElseIf rbltesting = 1 Then

        '        Dim filepath As String = "C:\Users\" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "\Downloads\" & FileName & ".xlsx"
        '        'filepath = "C:\temp\exportTEST\" & FileName & ".xlsx"
        '        If File.Exists(filepath) Then
        '            File.Delete(filepath)
        '        End If

        '        'CreateExcelFile.CreateExcelDocument(ds, "C:\TEMP\FREDDUMP\" & FileName & ".xlsx")
        '        ' CreateExcelFile.CreateExcelDocument(ds, "C:\Users\" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "\Downloads\" & FileName & ".xlsx")
        '        CreateExcelFile.CreateExcelDocument(ds, filepath.ToString)
        '        '  CreateExcelFile.CreateExcelDocument(ds, "C:\temp\exportTEST\" & FileName & ".xlsx")

        '        Dim buffer As Byte() = Nothing
        '        'Dim filepath As String = "C:\TEMP\FREDDUMP\" & FileName & ".xlsx"

        '        FileName = FileName & ".xlsx"
        '        If Not ReadByteStreamFromFileAndOutput(buffer, filepath) Then
        '            Return
        '        End If
        '        Response.ClearContent()
        '        Response.ClearHeaders()
        '        Response.ContentType = "application/octet-stream"

        '        Response.AddHeader("Content-disposition", "attachment; filename=" & FileName)
        '        Response.BinaryWrite(buffer)

        '        Response.Flush()
        '        Response.Close()

        '        Exit Sub
        '    Else


        '        'SAVE THIS CODE UNTIL THIS PART IS COMPLETE, THIS IS A BACK UP OF THE WORKING EXPORT
        '        Dim attachment As String = "attachment; filename=" & FileName & ".xls"
        '        Response.ClearContent()
        '        Response.AddHeader("content-disposition", attachment)
        '        Response.ContentType = "application/vnd.ms-excel"

        '        Dim tab As String = ""
        '        For Each dc As DataColumn In ds.Columns
        '            Response.Write(tab + dc.ColumnName)
        '            tab = vbTab
        '        Next
        '        Response.Write(vbLf)

        '        Dim i As Integer
        '        For Each dr As DataRow In ds.Rows
        '            tab = ""
        '            For i = 0 To ds.Columns.Count - 1
        '                Response.Write(tab & dr(i).ToString())
        '                tab = vbTab
        '            Next
        '            Response.Write(vbLf)
        '        Next
        '        Response.End()
        '    End If


        '    '    'SAVE THIS CODE UNTIL THIS PART IS COMPLETE, THIS IS A BACK UP OF THE WORKING EXPORT
        '    '    Dim attachment As String = "attachment; filename=" & FileName & ".xls"
        '    '    Response.ClearContent()
        '    '    Response.AddHeader("content-disposition", attachment)
        '    '    Response.ContentType = "application/vnd.ms-excel"

        '    '    Dim tab As String = ""
        '    '    For Each dc As DataColumn In ds.Columns
        '    '        Response.Write(tab + Replace(dc.ColumnName, "CarriageReturn", " "))
        '    '        tab = vbTab
        '    '    Next
        '    '    Response.Write(vbLf)

        '    '    Dim i As Integer
        '    '    For Each dr As DataRow In ds.Rows
        '    '        tab = ""
        '    '        For i = 0 To ds.Columns.Count - 1
        '    '            Response.Write(tab & dr(i).ToString())
        '    '            tab = vbTab
        '    '        Next
        '    '        Response.Write(vbLf)
        '    '    Next
        '    '    'Response.Flush()
        '    '    'Response.Close()
        '    '    Response.End()
        '    'End If


        '    'Dim attachment As String = "attachment; filename=" & "C:\temp\" & "Galen_STARINSPLAN.xls"
        '    'Response.ClearContent()
        '    'Response.AddHeader("content-disposition", attachment)
        '    'Response.ContentType = "application/vnd.ms-excel"

        '    'Dim tab As String = ""
        '    'Response.Write(vbLf)

        '    'Dim i As Integer
        '    'For Each dr As DataRow In ds.Rows
        '    '    tab = ""
        '    '    For i = 0 To ds.Columns.Count - 1
        '    '        Response.Write(tab & dr(i).ToString())
        '    '        tab = vbTab
        '    '    Next
        '    '    Response.Write(vbLf)
        '    'Next
        '    'Response.Flush()
        '    'Response.Close()
        '    'Response.End()

        '    'CreateExcelFile.CreateExcelDocument(ds, FileName & ".xlsx")

        '    'Dim buffer As Byte() = Nothing
        '    'Dim fp As String = FileName & ".xlsx"

        '    'If Not ReadByteStreamFromFileAndOutput(buffer, fp) Then
        '    '    Return
        '    'End If


        '    'Response.ClearContent()
        '    'Response.ClearHeaders()
        '    'Response.ContentType = "application/octet-stream"

        '    'Response.AddHeader("Content-disposition", "attachment; filename=" & FileName & ".xlsx")
        '    'Response.BinaryWrite(buffer)

        '    'Response.Flush()
        '    'Response.End()




        'Catch ex As Exception

        'End Try
    End Sub

End Class
