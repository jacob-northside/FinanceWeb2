Imports System
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls


Imports FinanceWeb.WebFinGlobal


Public Class FeedbackHistory
    Inherits System.Web.UI.Page

    Dim SQL, SQL2, SQL3 As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd, cmd2 As SqlCommand
    Dim cmdNS1 As SqlCommand
    Dim dr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

        Else
            Try

                If Request.QueryString("ModID") IsNot Nothing Then
                    lblMetricID.Text = Request.QueryString("ModID")
                End If
                If Request.QueryString("USERID") IsNot Nothing Then
                    lblUserID.Text = Request.QueryString("USERID")
                    If lblUserID.Text.StartsWith("NS") Then
                        lblUserID.Text = Mid(lblUserID.Text, 3)
                    End If
                End If

                'lblMetricID.Text = "2"
                'lblFiscalYear.Text = "2014"
                'lblFiscalMonth.Text = "6"
                'lblUserID.Text = "mf995052"

                If lblMetricID.Text <> "" And lblUserID.Text <> "" Then
                    LoadMetricData()
                    ' FeedBack.DataBind()
                    '   gvMetricHistory.DataSource = FeedBack
                    gvMetricHistory.DataBind()

                Else
                    ClearForm()
                    txtMetricComment.Text = "ERROR LOADING METRIC HISTORY COMMENT TOOL"
                End If
            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If
    End Sub
    Private Sub LoadMetricData()
        Try
            'Dim FiscalYear As String
            'Dim FiscalMonth As String

            'If lblFiscalYear.Text <> "" Then
            '    FiscalYear = lblFiscalYear.Text
            'Else
            '    FiscalYear = ""
            'End If
            'If lblFiscalMonth.Text <> "" Then
            '    FiscalMonth = lblFiscalMonth.Text
            'Else
            '    FiscalMonth = ""
            'End If

            'If FiscalYear <> "" And FiscalMonth <> "" Then
            '    SQL = "select distinct CurrentSCMName, " & _
            '    "CurrentSCMTarget, SCDActual, " & _
            '    "SCDFY, SCDFM, SCDFD   " & _
            '    "FROM [DWH].[KPIS].[vw_ScorecardData] " & _
            '    "where SCDID = '" & lblModuleID.Text & "' " & _
            '    "and  SCDFY = '" & FiscalYear & "' " & _
            '    "and SCDFM = '" & FiscalMonth & "'   "
            'Else
            '    SQL = "select distinct CurrentSCMName, CurrentSCMTarget, SCDActual, " & _
            '    "a.SCDFY, a.SCDFM, a.SCDFD " & _
            '    "from DWH.KPIS.vw_ScorecardData  a " & _
            '    "inner join (select SCMID,    " & _
            '    "max(SCDFullDate) FullDate " & _
            '    "FROM DWH.KPIS.vw_ScorecardData " & _
            '    "where SCDID = '" & lblModuleID.Text & "' and SCDActual is not null " & _
            '    "group by SCMID ) b on a.SCMID = b.SCMID and a.SCDFullDate = b.FullDate  "
            'End If

            SQL = "select distinct CurrentSCMName, round(CurrentSCMTarget,2) as CurrentSCMTarget," & _
                " case when SCMDataType = 'percent' then round(isnull(SCDActual,0)*100/(ISNULL(SCDDenominator,1)), 2) else round(SCDActual,2) end as SCDActual, " & _
            "a.SCDFY, a.SCDFM, a.SCDFD  , b.SCMOwner " & _
            "from DWH.KPIS.vw_ScorecardData  a  " & _
            "inner join DWH.KPIS.ScorecardMetric b on b.SCMID = a.SCMID  " & _
            "where a.SCDID =  '" & lblMetricID.Text & "'  "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("CurrentSCMName")) Then
                        lblMetric.Text = ""
                    Else
                        lblMetric.Text = dr.Item("CurrentSCMName")
                    End If
                    If IsDBNull(dr.Item("CurrentSCMTarget")) Then
                        lblTargetValue.Text = ""
                    Else
                        lblTargetValue.Text = dr.Item("CurrentSCMTarget")
                    End If
                    If IsDBNull(dr.Item("SCDActual")) Then
                        lblActualValue.Text = ""
                    Else
                        lblActualValue.Text = dr.Item("SCDActual")
                    End If
                    If IsDBNull(dr.Item("SCMOwner")) Then
                        lblMetricOwner.Text = "Metric Owner:  "
                    Else
                        lblMetricOwner.Text = "Metric Owner:  " & dr.Item("SCMOwner")
                    End If
                    If IsDBNull(dr.Item("SCDFY")) Then
                        lblFiscalYear.Text = ""
                    Else
                        lblFiscalYear.Text = dr.Item("SCDFY")
                    End If
                    If IsDBNull(dr.Item("SCDFM")) Then
                        lblFiscalMonth.Text = ""
                    Else
                        lblFiscalMonth.Text = dr.Item("SCDFM")
                    End If
                End While
                dr.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub ClearForm()
        Try
            txtMetricComment.Text = ""
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnSubmitComment_Click(sender As Object, e As EventArgs) Handles btnSubmitComment.Click
        Try
            If txtMetricComment.Text <> "" Then

                SQL = "insert into DWH.KPIS.ScorecardFeedback " & _
                "([SCDID],[SCFBDate],[SCFeedback],[SCFBOwner],[SCFBActive]) " & _
                "values " & _
                "('" & Replace(lblMetricID.Text, "'", "''") & "', getdate(), '" & Replace(txtMetricComment.Text, "'", "''") & "', " & _
                "'" & Replace(lblUserID.Text, "'", "''") & "', '1');  " & _
                "insert into DWH.KPIS.ScorecardFeedback " & _
                "select  d2.SCDID, getdate(), '" & Replace(txtMetricComment.Text, "'", "''") & "', " & _
                "'" & Replace(lblUserID.Text, "'", "''") & "', '1' " & _
                "From DWH.KPIS.ScorecardData d " & _
                "join DWH.KPIS.ScorecardMetric m on m.ID = d.ID and d.SCDActive = 1 " & _
                "join DWH.KPIS.ScorecardMetric m2 on m2.SCMCalculations = 'Same as ID ' + convert(varchar, m.ID) " & _
                "join DWH.KPIS.ScorecardData d2 on m2.ID = d2.ID and d.SCDFY = d2.SCDFY and d.SCDFM = d2.SCDFM and d.SCDFD = d2.SCDFD and d2.SCDActive = 1 " & _
                "where d.SCDID = '" & Replace(lblMetricID.Text, "'", "''") & "'"

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                        End If
                    cmd = New SqlCommand(SQL, conn)
                    cmd.ExecuteNonQuery()

                    End Using
                ClearForm()
                    'FeedBack.DataBind()
                gvMetricHistory.DataBind()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



End Class