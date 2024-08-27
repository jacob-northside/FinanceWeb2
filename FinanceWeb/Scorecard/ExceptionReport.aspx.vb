Imports System
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls


Public Class ExceptionReport
    Inherits System.Web.UI.Page

    Dim SQL, SQL2, SQL3 As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd, cmd2 As SqlCommand
    Dim cmdNS1 As SqlCommand
    Dim dr As SqlDataReader


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

        Else
            If Request.QueryString("ModID") IsNot Nothing Then
                lblMetricID.Text = Request.QueryString("ModID")
            End If
            'If Request.QueryString("FY") IsNot Nothing Then
            '    lblFiscalYear.Text = Request.QueryString("FY")
            'End If
            'If Request.QueryString("FM") IsNot Nothing Then
            '    lblFiscalMonth.Text = Request.QueryString("FM")
            'End If
            If Request.QueryString("USERID") IsNot Nothing Then
                lblUserID.Text = Request.QueryString("USERID")
                If lblUserID.Text.StartsWith("NS") Then
                    lblUserID.Text = Mid(lblUserID.Text, 3)
                End If
            End If

            'lblModuleID.Text = "2"
            'lblFiscalYear.Text = "2014"
            'lblFiscalMonth.Text = "6"
            'lblUserID.Text = "mf995052"

            If lblMetricID.Text <> "" And lblUserID.Text <> "" Then
                LoadMetricData()
                ' FeedBack.DataBind()
                '   gvExceptionHistory.DataSource = FeedBack
                gvExceptionHistory.DataBind()

            Else
                ClearForm()
                txtExceptionComment.Text = "ERROR LOADING EXCEPTION COMMENT TOOL"
            End If
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

            SQL = "select distinct CurrentSCMName, CurrentSCMTarget, SCDActual,  " & _
            "a.SCDFY, a.SCDFM, a.SCDFD  , b.SCMOwner " & _
            "from DWH.KPIS.vw_ScorecardData  a  " & _
            "inner join DWH.KPIS.ScorecardModule b on b.SCMID = a.SCMID  " & _
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

        End Try
    End Sub
    Private Sub ClearForm()
        Try
            txtExceptionComment.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmitComment_Click(sender As Object, e As EventArgs) Handles btnSubmitComment.Click
        Try
            If txtExceptionComment.Text <> "" Then

                SQL = "insert into DWH.KPIS.ScorecardFeedback " & _
                "([SCDID],[SCFBDate],[SCFeedback],[SCFBOwner],[SCFBActive]) " & _
                "values " & _
                "('" & Replace(lblMetricID.Text, "'", "''") & "', getdate(), '" & Replace(txtExceptionComment.Text, "'", "''") & "', " & _
                "'" & Replace(lblUserID.Text, "'", "''") & "', '1')  "

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd = New SqlCommand(SQL, conn)
                    cmd.ExecuteNonQuery()

                End Using
                ClearForm()
                'FeedBack.DataBind()
                gvExceptionHistory.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class