Imports System
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls


Imports FinanceWeb.WebFinGlobal


Public Class OHMS_FeedbackHistory
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
                LogWebFinError(lblUserID.Text, Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
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

            SQL = "select distinct Name, round(t.Target,2) as Target, convert(varchar, l.Location) + ' - ' + l.LocationDesc as LocationDesc, " & _
                "case when b.DataType = 'percent' then round(isnull(a.Numerator,0)*100/(ISNULL(Denominator,1)), 2) else round(a.Numerator,2) end as Actual,  " & _
                "a.dDate, case when  e.FirstName is null and e.LastName is null then b.Owner else ISNULL(e.FirstName, '') + ISNULL(e.LastName, '') end as Owner " & _
                ", case when DataType = 'raw' then 0 else 2 end  as NbrDecimals " & _
                "from DWH.KPIS.DEV_OHMS_Data  a   " & _
                "inner join DWH.KPIS.DEV_OHMS_Metrics b on b.MID = a.MID  " & _
                "left join DWH.KPIS.DEV_OHMS_Location_LU l on a.LocID = l.LocID and l.Active = 1 " & _
                "left join DWH.dbo.Email_Distribution e on b.Owner = e.NetworkLogin " & _
                "left join DWH.KPIS.DEV_OHMS_Target t on t.LocID = a.LocID and t.MID = a.MID and a.dDate between isnull(t.TargetEffFromDate, '1/1/1800') and isnull(t.TargetEffToDate, '12/31/9999')" & _
               "where a.dID =  '" & lblMetricID.Text & "'  "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(SQL, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr.Item("Name")) Then
                        lblMetric.Text = ""
                    Else
                        lblMetric.Text = dr.Item("Name")
                    End If
                    If IsDBNull(dr.Item("LocationDesc")) Then
                        lblLocation.Text = ""
                    Else
                        lblLocation.Text = dr.Item("LocationDesc")
                    End If
                    If IsDBNull(dr.Item("Target")) Then
                        lblTargetValue.Text = ""
                    Else
                        lblTargetValue.Text = dr.Item("Target")
                    End If
                    If IsDBNull(dr.Item("Actual")) Then
                        lblActualValue.Text = ""
                    Else
                        lblActualValue.Text = CStr(Math.Round(CDec(dr.Item("Actual")), dr.Item("NbrDecimals")))
                    End If
                    If IsDBNull(dr.Item("Owner")) Then
                        lblMetricOwner.Text = "Metric Owner:  "
                    Else
                        lblMetricOwner.Text = "Metric Owner:  " & dr.Item("Owner")
                    End If
                    If IsDBNull(dr.Item("dDate")) Then
                        lblDate.Text = ""
                    Else
                        lblDate.Text = dr.Item("dDate")
                    End If

                End While
                dr.Close()
            End Using

        Catch ex As Exception
            LogWebFinError(lblUserID.Text, Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub ClearForm()
        Try
            txtMetricComment.Text = ""
        Catch ex As Exception
            LogWebFinError(lblUserID.Text, Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnSubmitComment_Click(sender As Object, e As EventArgs) Handles btnSubmitComment.Click
        Try
            If txtMetricComment.Text <> "" Then

                SQL = "insert into DWH.KPIS.DEV_OHMS_Feedback " & _
                "([dID],[Feedback],[FBActive],[FBUser],[FBDate]) " & _
                "values " & _
                "('" & Replace(lblMetricID.Text, "'", "''") & "', '" & Replace(txtMetricComment.Text, "'", "''") & "', '1'," & _
                "'" & Replace(lblUserID.Text, "'", "''") & "', getdate());  "

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
            LogWebFinError(lblUserID.Text, Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



End Class