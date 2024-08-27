Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

Public Class OHMS
    Inherits System.Web.UI.Page

    Public Shared fiscalmonth As Integer
    Public Shared fiscalyear As Integer
    Public Shared selectedID As String
#Region "OLD CODE "

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub



    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim getSCMID As String
    '    Dim getSCMName As String

    '    If txtboxSCMID.Text = "" Or txtboxSCMName.Text Is Nothing Then
    '        Exit Sub

    '    End If

    '    getSCMID = txtboxSCMID.Text
    '    getSCMName = txtboxSCMName.Text

    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, SCMName) Values ('" & _
    '            Replace(getSCMID, "'", "''") & "', '" & Replace(getSCMName, "'", "''") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception

    '    End Try

    'End Sub
#End Region

#Region "Version 1"


    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub

    'Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    'End Sub

    'Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    'End Sub

    'Protected Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged

    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim getSCMID As String
    '    Dim getSCMName As String

    '    If txtboxSCMID.Text = "" Or txtboxSCMName.Text Is Nothing Then
    '        Exit Sub

    '    End If

    '    getSCMID = txtboxSCMID.Text
    '    getSCMName = txtboxSCMName.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, SCMName, SCMEffectiveFromDate, SCMUpdated) Values ('" & _
    '            Replace(getSCMID, "'", "''") & "', '" & Replace(getSCMName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnSCData_Click(sender As Object, e As EventArgs) Handles btnSCData.Click





    'End Sub
    'Protected Sub dsOhmsMetricSCMName_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles dsOhmsMetricSCMName.Selecting

    'End Sub

    'Protected Sub ScorecardInsert_Click(sender As Object, e As EventArgs) Handles ScorecardInsert.Click
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim scSCTID As String
    '    Dim scSCMID As String
    '    Dim scSCOwner As String

    '    If txtSCSCTID.Text = "" Or txtSCSCMID.Text Is Nothing Or txtSCSCOwner.Text Is Nothing Then
    '        Exit Sub

    '    End If

    '    scSCTID = txtSCSCTID.Text
    '    scSCMID = txtSCSCMID.Text
    '    scSCOwner = txtSCSCOwner.Text

    '    Try

    '        SQL = "Insert INTO DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated) Values ('" & _
    '            Replace(scSCTID, "'", "''") & "', '" & Replace(scSCMID, "'", "''") & "', '" & Replace(scSCOwner, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception

    '    End Try



    'End Sub
    'Protected Sub SDInsertButton_Click(sender As Object, e As EventArgs) Handles SDInsertButton.Click
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim sdFY As String
    '    Dim sdFM As String
    '    Dim sdFD As String
    '    Dim sdSCDActual As String
    '    Dim sdID As String

    '    If txtsdID.Text = "" Then
    '        Exit Sub

    '    End If

    '    sdFY = txtSDSCDFY.Text
    '    sdFM = txtSDSCDFM.Text
    '    sdFD = txtSDSCDFD.Text
    '    sdSCDActual = txtSDSCDActual.Text
    '    sdID = txtsdID.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.Scorecard (SCDFY, SCDFM, SCDFD, ID, SCDActual, SCDModifyDate, SCDUpdated) Values ('" & _
    '            Replace(sdFY, "'", "''") & "', '" & Replace(sdFM, "'", "''") & "', '" & Replace(sdFD, "'", "''") & "', '" & _
    '            Replace(sdID, "'", "''") & "', '" & Replace(sdSCDActual, "'", "''") & ", GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception

    '    End Try




    'End Sub
    'Protected Sub SCTitleInsertButton_Click(sender As Object, e As EventArgs) Handles SCTitleInsertButton.Click
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim sctSCTitle As String

    '    If txtSCTSCTitle.Text = "" Then
    '        Exit Sub

    '    End If

    '    sctSCTitle = txtSCTSCTitle.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle) Values ('" & _
    '            Replace(sctSCTitle, "'", "''") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception

    '    End Try




    'End Sub
    'Protected Sub scCategoryLUInsert_Click(sender As Object, e As EventArgs) Handles scCategoryLUInsert.Click
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim sccSCCat As String

    '    If txtSCCSCCat.Text = "" Then
    '        Exit Sub
    '    End If

    '    sccSCCat = txtSCCSCCat.Text

    '    Try
    '        SQL = "Insert INTO DWH.KPIS.ScorecardCategory_LU (SCCategory) Values ('" & _
    '            Replace(sccSCCat, "'", "''") & "')"

    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()
    '        End Using

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim getSCTID As String
    '    Dim getSCUName As String

    '    If txtboxSCMID0.Text = "" Or txtboxSCMName0.Text Is Nothing Then
    '        Exit Sub

    '    End If

    '    getSCTID = txtboxSCMID0.Text
    '    getSCUName = txtboxSCMName0.Text

    '    Try
    '        SQL = "Insert INTO DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated) Values ('" & _
    '            Replace(getSCTID, "'", "''") & "', '" & Replace(getSCUName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception

    '    End Try

    'End Sub

#End Region


#Region "OHMS version 2"
    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim getSCMID As String
    '    Dim getSCMName As String

    '    If txtboxSCMID.Text = "" Or txtboxSCMName.Text = "" Then
    '        MetricInsertRequirements.Text = "SCMID and SCMName are Required Fields"
    '        MetricModalPopupExtender.Show()
    '        Exit Sub

    '    End If

    '    getSCMID = txtboxSCMID.Text
    '    getSCMName = txtboxSCMName.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, SCMName, SCMEffectiveFromDate, SCMUpdated) Values ('" & _
    '            Replace(getSCMID, "'", "''") & "', '" & Replace(getSCMName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    MetricInsertRequirements.Text = "Metric Inserted"
    '    MetricModalPopupExtender.Show()

    'End Sub

    'Protected Sub btnSCData_Click(sender As Object, e As EventArgs) Handles btnSCData.Click

    'End Sub

    'Protected Sub dsOhmsMetricSCMName_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles dsOhmsMetricSCMName.Selecting

    'End Sub

    'Protected Sub ScorecardInsert_Click(sender As Object, e As EventArgs) Handles ScorecardInsert.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim scSCTID As String
    '    Dim scSCMID As String
    '    Dim scSCOwner As String

    '    If txtSCSCTID.Text = "" Or txtSCSCMID.Text = "" Or txtSCSCOwner.Text = "" Then
    '        SCExplanationLabel.Text = "All Fields are Required"
    '        SCModalPopupExtender.Show()
    '        Exit Sub

    '    End If

    '    scSCTID = txtSCSCTID.Text
    '    scSCMID = txtSCSCMID.Text
    '    scSCOwner = txtSCSCOwner.Text

    '    Try

    '        SQL = "Insert INTO DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated) Values ('" & _
    '            Replace(scSCTID, "'", "''") & "', '" & Replace(scSCMID, "'", "''") & "', '" & Replace(scSCOwner, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    SCExplanationLabel.Text = "Submitted"
    '    SCModalPopupExtender.Show()

    'End Sub

    'Protected Sub SDInsertButton_Click(sender As Object, e As EventArgs) Handles SDInsertButton.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim sdFY As String
    '    Dim sdFM As String
    '    Dim sdFD As String
    '    Dim sdSCDActual As String
    '    Dim sdID As String

    '    If txtsdID.Text = "" Or txtSDSCDFD.Text = "" Or txtSDSCDFM.Text = "" Or txtSDSCDFY.Text = "" Or txtSDSCDActual.Text = "" Then
    '        DataExplanationlabel.Text = "All Fields Required"
    '        DataModalPopupExtender.Show()
    '        Exit Sub

    '    End If

    '    sdFY = txtSDSCDFY.Text
    '    sdFM = txtSDSCDFM.Text
    '    sdFD = txtSDSCDFD.Text
    '    sdSCDActual = txtSDSCDActual.Text
    '    sdID = txtsdID.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardData (SCDFY, SCDFM, SCDFD, ID, SCDActual, SCDModifyDate, SCDUpdated) Values ('" & _
    '            Replace(sdFY, "'", "''") & "', '" & Replace(sdFM, "'", "''") & "', '" & Replace(sdFD, " '", "''") & "', '" & _
    '            Replace(sdID, "'", "''") & "', '" & Replace(sdSCDActual, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    DataExplanationlabel.Text = "Data Submitted"
    '    DataModalPopupExtender.Show()


    'End Sub

    'Protected Sub SCTitleInsertButton_Click(sender As Object, e As EventArgs) Handles SCTitleInsertButton.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim sctSCTitle As String

    '    If txtSCTSCTitle.Text = "" Then
    '        TitleExplanationLabel.Text = "Title Required"
    '        TitleModalPopupExtender.Show()
    '        Exit Sub

    '    End If

    '    sctSCTitle = txtSCTSCTitle.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle) Values ('" & _
    '            Replace(sctSCTitle, "'", "''") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    TitleExplanationLabel.Text = "Submitted"
    '    TitleModalPopupExtender.Show()


    'End Sub

    'Protected Sub scCategoryLUInsert_Click(sender As Object, e As EventArgs) Handles scCategoryLUInsert.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim sccSCCat As String

    '    If txtSCCSCCat.Text = "" Then
    '        CatExplanationLabel.Text = "Category Name Required"
    '        CatModalPopupExtender.Show()
    '        Exit Sub

    '    End If

    '    sccSCCat = txtSCCSCCat.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardCategory_LU (SCCategory) Values ('" & _
    '            Replace(sccSCCat, "'", "''") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    CatExplanationLabel.Text = "Submitted"
    '    CatModalPopupExtender.Show()

    'End Sub

    'Protected Sub OhmsULInsert_Click(sender As Object, e As EventArgs) Handles OhmsULInsert.Click

    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim getSCTID As String
    '    Dim getSCUName As String

    '    If txtboxSCMID0.Text = "" Or txtboxSCMName0.Text = "" Then
    '        ULExplanationlabel.Text = "SCMID and SCMName are required fields"
    '        ULModalPopupExtender.Show()
    '        Exit Sub

    '    End If

    '    getSCTID = txtboxSCMID0.Text
    '    getSCUName = txtboxSCMName0.Text


    '    Try

    '        SQL = "Insert INTO DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated) Values ('" & _
    '            Replace(getSCTID, "'", "''") & "', '" & Replace(getSCUName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try


    '    ULExplanationlabel.Text = "Submitted"
    '    ULModalPopupExtender.Show()

    'End Sub

    'Protected Sub btnCNSPreview_Click(sender As Object, e As EventArgs) Handles btnCNSPreview.Click

    '    Dim SelectList As String = ""

    '    For Each checkeditem As ListItem In cblCNSMetricSelect.Items
    '        If (checkeditem.Selected) Then
    '            SelectList += checkeditem.Value + ", "

    '        End If
    '    Next


    '    Dim Sql2 As String
    '    Dim ds As DataSet
    '    Dim da As SqlDataAdapter


    '    Try

    '        Sql2 = "select SCMName, SCMDefinition, SCMOwner from DWH.KPIS.ScorecardMetric " & _
    '        "where SCMID in (" & SelectList & "0)"


    '        ds = New DataSet

    '        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            da = New SqlDataAdapter(Sql2, conn2)
    '            da.Fill(ds, "DEMO")
    '            gridCNSPreviewMetric.DataSource = ds
    '            gridCNSPreviewMetric.DataMember = "DEMO"
    '            gridCNSPreviewMetric.DataBind()
    '        End Using





    '    Catch ex As Exception

    '    End Try




    'End Sub


    'Protected Sub btnCNSSubmit_Click(sender As Object, e As EventArgs) Handles btnCNSSubmit.Click


    '    Dim SelectList As String = ""
    '    Dim TitleSql As String = ""
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim SQL2 As String = ""



    '    If txtnewSCName.Text = "" Or txtnewSCOwner.Text = "" Then
    '        ExplanationNewSClabel.Text = "Scorecard Name and Owner Required"
    '        ModalPopupExtender2.Show()
    '        Exit Sub
    '    End If
    '    Try

    '        TitleSql = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle) Values ('" & _
    '            Replace(txtnewSCName.Text, "'", "''") & "')"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using

    '        For Each checked2item As ListItem In cblCNSUsersSelect.Items
    '            If (checked2item.Selected) Then
    '                SQL2 = "Insert into DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated) VALUES " & _
    '                    "((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checked2item.Value + "', GetDate(), '" & _
    '                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"
    '                Try
    '                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                        If conn2.State = ConnectionState.Closed Then
    '                            conn2.Open()
    '                        End If

    '                        cmd = New System.Data.SqlClient.SqlCommand(SQL2, conn2)
    '                        cmd.ExecuteNonQuery()
    '                    End Using

    '                Catch ex As Exception

    '                End Try
    '            End If
    '        Next


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    For Each checkeditem As ListItem In cblCNSMetricSelect.Items
    '        If (checkeditem.Selected) Then
    '            SQL = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated) VALUES" & _
    '                  " ((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checkeditem.Value + "', '" & _
    '                  Replace(txtnewSCOwner.Text, "'", "''") + "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

    '            Try

    '                Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                    If conn.State = ConnectionState.Closed Then
    '                        conn.Open()
    '                    End If

    '                    cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                    cmd.ExecuteNonQuery()

    '                End Using


    '            Catch ex As Exception
    '                Exit Sub
    '            End Try

    '        End If
    '    Next

    '    ExplanationNewSClabel.Text = "Scorecard Submitted"
    '    ModalPopupExtender2.Show()

    'End Sub

    'Private Sub OkButton2_Click(sender As Object, e As System.EventArgs) Handles OkButton2.Click

    '    ModalPopupExtender2.Hide()
    'End Sub

#End Region



    '#Region "Version3"
    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    End Sub

    '    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    '    End Sub

    '    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    '    End Sub

    '    Protected Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged

    '    End Sub

    '    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim getSCMID As String
    '        Dim getSCMName As String

    '        If txtboxSCMID.Text = "" Or txtboxSCMName.Text = "" Then
    '            MetricInsertRequirements.Text = "SCMID and SCMName are Required Fields"
    '            MetricModalPopupExtender.Show()
    '            Exit Sub

    '        End If

    '        getSCMID = txtboxSCMID.Text
    '        getSCMName = txtboxSCMName.Text


    '        Try

    '            SQL = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, SCMName, SCMEffectiveFromDate, SCMUpdated, SCMActive) Values ('" & _
    '                Replace(getSCMID, "'", "''") & "', '" & Replace(getSCMName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try

    '        MetricInsertRequirements.Text = "Metric Inserted"
    '        MetricModalPopupExtender.Show()

    '    End Sub

    '    Protected Sub btnSCData_Click(sender As Object, e As EventArgs) Handles btnSCData.Click

    '    End Sub

    '    Protected Sub dsOhmsMetricSCMName_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles dsOhmsMetricSCMName.Selecting

    '    End Sub

    '    Protected Sub ScorecardInsert_Click(sender As Object, e As EventArgs) Handles ScorecardInsert.Click

    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim scSCTID As String
    '        Dim scSCMID As String
    '        Dim scSCOwner As String

    '        If txtSCSCTID.Text = "" Or txtSCSCMID.Text = "" Or txtSCSCOwner.Text = "" Then
    '            SCExplanationLabel.Text = "All Fields are Required"
    '            SCModalPopupExtender.Show()
    '            Exit Sub

    '        End If

    '        scSCTID = txtSCSCTID.Text
    '        scSCMID = txtSCSCMID.Text
    '        scSCOwner = txtSCSCOwner.Text

    '        Try

    '            SQL = "Insert INTO DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated, SCActive) Values ('" & _
    '                Replace(scSCTID, "'", "''") & "', '" & Replace(scSCMID, "'", "''") & "', '" & Replace(scSCOwner, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try

    '        SCExplanationLabel.Text = "Submitted"
    '        SCModalPopupExtender.Show()

    '    End Sub

    '    Protected Sub SDInsertButton_Click(sender As Object, e As EventArgs) Handles SDInsertButton.Click

    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim sdFY As String
    '        Dim sdFM As String
    '        Dim sdFD As String
    '        Dim sdSCDActual As String
    '        Dim sdID As String

    '        If txtsdID.Text = "" Or txtSDSCDFD.Text = "" Or txtSDSCDFM.Text = "" Or txtSDSCDFY.Text = "" Or txtSDSCDActual.Text = "" Then
    '            DataExplanationlabel.Text = "All Fields Required"
    '            DataModalPopupExtender.Show()
    '            Exit Sub

    '        End If

    '        sdFY = txtSDSCDFY.Text
    '        sdFM = txtSDSCDFM.Text
    '        sdFD = txtSDSCDFD.Text
    '        sdSCDActual = txtSDSCDActual.Text
    '        sdID = txtsdID.Text


    '        Try

    '            SQL = "Insert INTO DWH.KPIS.ScorecardData (SCDFY, SCDFM, SCDFD, ID, SCDActual, SCDModifyDate, SCDUpdated, SCDActive) Values ('" & _
    '                Replace(sdFY, "'", "''") & "', '" & Replace(sdFM, "'", "''") & "', '" & Replace(sdFD, " '", "''") & "', '" & _
    '                Replace(sdID, "'", "''") & "', '" & Replace(sdSCDActual, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try

    '        DataExplanationlabel.Text = "Data Submitted"
    '        DataModalPopupExtender.Show()


    '    End Sub

    '    Protected Sub SCTitleInsertButton_Click(sender As Object, e As EventArgs) Handles SCTitleInsertButton.Click

    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim sctSCTitle As String

    '        If txtSCTSCTitle.Text = "" Then
    '            TitleExplanationLabel.Text = "Title Required"
    '            TitleModalPopupExtender.Show()
    '            Exit Sub

    '        End If

    '        sctSCTitle = txtSCTSCTitle.Text


    '        Try

    '            SQL = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle, SCTActive) Values ('" & _
    '                Replace(sctSCTitle, "'", "''") & "', 1)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try

    '        TitleExplanationLabel.Text = "Submitted"
    '        TitleModalPopupExtender.Show()


    '    End Sub

    '    Protected Sub scCategoryLUInsert_Click(sender As Object, e As EventArgs) Handles scCategoryLUInsert.Click

    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim sccSCCat As String

    '        If txtSCCSCCat.Text = "" Then
    '            CatExplanationLabel.Text = "Category Name Required"
    '            CatModalPopupExtender.Show()
    '            Exit Sub

    '        End If

    '        sccSCCat = txtSCCSCCat.Text


    '        Try

    '            SQL = "Insert INTO DWH.KPIS.ScorecardCategory_LU (SCCategory, SCCActive) Values ('" & _
    '                Replace(sccSCCat, "'", "''") & "', 1)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try

    '        CatExplanationLabel.Text = "Submitted"
    '        CatModalPopupExtender.Show()

    '    End Sub

    '    Protected Sub OhmsULInsert_Click(sender As Object, e As EventArgs) Handles OhmsULInsert.Click

    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim getSCTID As String
    '        Dim getSCUName As String

    '        If txtboxSCMID0.Text = "" Or txtboxSCMName0.Text = "" Then
    '            ULExplanationlabel.Text = "SCMID and SCMName are required fields"
    '            ULModalPopupExtender.Show()
    '            Exit Sub

    '        End If

    '        getSCTID = txtboxSCMID0.Text
    '        getSCUName = txtboxSCMName0.Text


    '        Try

    '            SQL = "Insert INTO DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated, SCUActive) Values ('" & _
    '                Replace(getSCTID, "'", "''") & "', '" & Replace(getSCUName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', SCUActive)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try


    '        ULExplanationlabel.Text = "Submitted"
    '        ULModalPopupExtender.Show()

    '    End Sub

    '    Protected Sub btnCNSPreview_Click(sender As Object, e As EventArgs) Handles btnCNSPreview.Click

    '        Dim SelectList As String = ""

    '        For Each checkeditem As ListItem In cblCNSMetricSelect.Items
    '            If (checkeditem.Selected) Then
    '                SelectList += checkeditem.Value + ", "

    '            End If
    '        Next


    '        Dim Sql2 As String
    '        Dim ds As DataSet
    '        Dim da As SqlDataAdapter


    '        Try

    '            Sql2 = "select SCMName, SCMDefinition, SCMOwner from DWH.KPIS.ScorecardMetric " & _
    '            "where SCMID in (" & SelectList & "0)"


    '            ds = New DataSet

    '            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '                da = New SqlDataAdapter(Sql2, conn2)
    '                da.Fill(ds, "DEMO")
    '                gridCNSPreviewMetric.DataSource = ds
    '                gridCNSPreviewMetric.DataMember = "DEMO"
    '                gridCNSPreviewMetric.DataBind()
    '            End Using





    '        Catch ex As Exception

    '        End Try




    '    End Sub


    '    Protected Sub btnCNSSubmit_Click(sender As Object, e As EventArgs) Handles btnCNSSubmit.Click


    '        Dim SelectList As String = ""
    '        Dim TitleSql As String = ""
    '        Dim SQL As String = ""
    '        Dim cmd As SqlCommand
    '        Dim SQL2 As String = ""



    '        If txtnewSCName.Text = "" Or txtnewSCOwner.Text = "" Then
    '            ExplanationNewSClabel.Text = "Scorecard Name and Owner Required"
    '            ModalPopupExtender2.Show()
    '            Exit Sub
    '        End If
    '        Try

    '            TitleSql = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle, SCActive) Values ('" & _
    '                Replace(txtnewSCName.Text, "'", "''") & "', 1)"


    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
    '                cmd.ExecuteNonQuery()

    '            End Using

    '            For Each checked2item As ListItem In cblCNSUsersSelect.Items
    '                If (checked2item.Selected) Then
    '                    SQL2 = "Insert into DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated, SCUActive) VALUES " & _
    '                        "((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checked2item.Value + "', GetDate(), '" & _
    '                        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"
    '                    Try
    '                        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                            If conn2.State = ConnectionState.Closed Then
    '                                conn2.Open()
    '                            End If

    '                            cmd = New System.Data.SqlClient.SqlCommand(SQL2, conn2)
    '                            cmd.ExecuteNonQuery()
    '                        End Using

    '                    Catch ex As Exception

    '                    End Try
    '                End If
    '            Next


    '        Catch ex As Exception
    '            Exit Sub
    '        End Try

    '        For Each checkeditem As ListItem In cblCNSMetricSelect.Items
    '            If (checkeditem.Selected) Then
    '                SQL = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated, SCActive) VALUES" & _
    '                      " ((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checkeditem.Value + "', '" & _
    '                      Replace(txtnewSCOwner.Text, "'", "''") + "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"

    '                Try

    '                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                        If conn.State = ConnectionState.Closed Then
    '                            conn.Open()
    '                        End If

    '                        cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                        cmd.ExecuteNonQuery()

    '                    End Using


    '                Catch ex As Exception
    '                    Exit Sub
    '                End Try

    '            End If
    '        Next

    '        ExplanationNewSClabel.Text = "Scorecard Submitted"
    '        ModalPopupExtender2.Show()

    '    End Sub

    '    Private Sub OkButton2_Click(sender As Object, e As System.EventArgs) Handles OkButton2.Click

    '        ModalPopupExtender2.Hide()
    '    End Sub
    '#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If IsPostBack Then
                Exit Sub
            End If
            Dim getdate As String
            Dim datasetgetdate As New DataSet
            Dim dr As DataRow

            getdate = "select FY, FM from DWH.dbo.DimDate where Calendar_Date = convert(date, GETDATE())"

            Try

                Dim da As SqlDataAdapter

                Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    da = New SqlDataAdapter(getdate, conn2)
                    da.Fill(datasetgetdate)

                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try

            dr = datasetgetdate.Tables(0).Rows(0)
            fiscalmonth = dr("FM").ToString
            fiscalyear = dr("FY").ToString

            ddlSCDFM.SelectedValue = fiscalmonth
            ddlSCDFY.SelectedValue = fiscalyear

            Dim alpha As Int16 = 0
            Dim y As Integer

            y = GridView1.Rows.Count

            While alpha < (y)
                If alpha Mod 2 = 0 Then
                    GridView1.Rows(alpha).BackColor = Drawing.Color.White
                    alpha = alpha + 1
                Else
                    GridView1.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#eee4ce")
                    alpha = alpha + 1
                End If
            End While

            If GridView1.Rows.Count > 0 Then
                GridView1.Rows(0).BackColor = Drawing.Color.LightSteelBlue
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Protected Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Dim SQL As String = ""
        'Dim cmd As SqlCommand
        'Dim getSCMID As String
        'Dim getSCMName As String

        'If txtboxSCMID.Text = "" Or txtboxSCMName.Text = "" Then
        '    MetricInsertRequirements.Text = "SCMID and SCMName are Required Fields"
        '    MetricModalPopupExtender.Show()
        '    Exit Sub

        'End If

        'getSCMID = txtboxSCMID.Text
        'getSCMName = txtboxSCMName.Text


        'Try

        '    SQL = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, SCMName, SCMEffectiveFromDate, SCMUpdated, SCMActive) Values ('" & _
        '        Replace(getSCMID, "'", "''") & "', '" & Replace(getSCMName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


        '    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

        '        If conn.State = ConnectionState.Closed Then
        '            conn.Open()
        '        End If

        '        cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
        '        cmd.ExecuteNonQuery()

        '    End Using


        'Catch ex As Exception
        '    Exit Sub
        'End Try

        Try



            Dim SelectList As String = ""
            Dim TitleSql As String = ""
            Dim SQL As String = ""
            Dim cmd As SqlCommand
            Dim EDsql As String = ""

            'Page.Validate("MetricRequestForm")

            If txtMetricName.Text = "" Or txtMetricTarget.Text = "" Or txtMetricOwnerlogin.Text = "" Then
                explantionlabel.Text = "Please Fill in all required fields"
                ModalPopupExtender1.Show()

                Exit Sub
            End If

            If InStr(txtMetricOwner.Text, ",") Then

            End If

            Dim checksql As String
            Dim x As String
            x = "2"

            checksql = "Select count(*) from DWH.dbo.Email_Distribution e " & _
                "full outer join DWH.KPIS.ContactInfo on NetworkLogin = UserID " & _
                "where NetworkLogin = '" & Replace(txtMetricOwnerlogin.Text, "'", "''") & "' or (UserID = '" & Replace(txtMetricOwnerlogin.Text, "'", "''") &
                "' and UserEmail = '" & Replace(txtMetricOwneremail.Text, "'", "''") & "' and '" & Replace(txtMetricOwner.Text, "'", "''") & "' = UserName )"

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim dc As SqlCommand
                dc = New SqlCommand(checksql, conn2)
                Try
                    conn2.Open()
                    x = Convert.ToString(dc.ExecuteScalar())
                Catch ex As Exception
                    explantionlabel.Text = "Invalid Metric Owner"
                    ModalPopupExtender1.Show()
                End Try

            End Using

            If CInt(x) = 0 Then
                Dim newusersql As String

                newusersql = "Insert into [DWH].[KPIS].[ContactInfo] ([UserID], [UserEmail], [UserName]) Values ('" & _
                    Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & Replace(txtMetricOwneremail.Text, "'", "''") & _
                    "', '" & Replace(txtMetricOwner.Text, "'", "''") & "')"

                Try

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        cmd = New System.Data.SqlClient.SqlCommand(newusersql, conn)
                        cmd.ExecuteNonQuery()

                    End Using


                Catch ex As Exception
                    explantionlabel.Text = "Invalid Metric Owner"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End Try
            End If



            'EDsql = "Insert into DWH.dbo.Email_Distribution (NetworkLogin, Email, FirstName, LastName) Select (" & _
            'Replace(txtMetricOwnerlogin.Text, "'", "''") & ", " & Replace(txtMetricOwneremail.Text, "'", "''") & _
            '", " & Replace(txtMetricOwner.Text, "'", "''") & ") from [DWH].[dbo].[Email_Distribution] where " & _
            'Replace(txtMetricOwnerlogin.Text, "'", "''") & "not in (SELECT distinct NetworkLogin " & _
            '"FROM [DWH].[dbo].[Email_Distribution]) and IdUser = (select MAX(IdUser) from [DWH].[dbo].[Email_Distribution])"

            Dim upId As String
            If UpdateorNew.SelectedValue = 0 Then
                upId = "(select MAX(SCMID) from KPIS.ScorecardMetric) + 1"
            Else
                upId = selectedID
            End If

            Dim freq As String
            If ddlMetricFreq.SelectedValue = 0 Then
                freq = ReqFreq.Text
            Else
                freq = ddlMetricFreq.SelectedValue
            End If

            Dim cat As String
            Dim extracat As String
            If ddlnewMetCat.SelectedValue = 0 Then
                cat = "(select max(SCCID) + 1 FROM [DWH].[KPIS].[ScorecardCategory_LU])"
                extracat = " Insert into [DWH].[KPIS].[ScorecardCategory_LU] (SCCategory, SCCActive) Values ('" & _
                    Replace(CatRequest.Text, "'", "''") & "', 1)"
            Else
                cat = "'" & ddlnewMetCat.SelectedValue & "'"
                extracat = ""
            End If

            Try
                If UpdateorNew.SelectedValue = 0 Then
                    TitleSql = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, SCMActive, [SCMName] ,[SCMObjective],[SCMDefinition]" & _
                        ", [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
                        ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin] ,[SCMUpdateFrequency]" & _
                        ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMUpdated]) Values ((select MAX(SCMID) from KPIS.ScorecardMetric) + 1, 1, '" & _
                        Replace(txtMetricName.Text, "'", "''") & "', '" & Replace(txtMetricObjective.Text, "'", "''") & "', '" & _
                        Replace(txtMetricDefinition.Text, "'", "''") & "', '" & Replace(txtMetricCalculated.Text, "'", "''") & "', '" & _
                        Replace(txtMetricSourceSystem.Text, "'", "''") & "', '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', '" & _
                        Replace(txtMetricLTGoalDate.Text, "'", "''") & "', '" & Replace(txtMetricTarget.Text, "'", "''") & "', '" & _
                        ddlMetricTargetType.SelectedValue & "', '" & rbnewmetcum1.Checked & "', '" & Replace(txtMetricMax.Text, "'", "''") & _
                        "', '" & Replace(txtMetricMin.Text, "'", "''") & "', '" & Replace(txtMetricwMax.Text, "'", "''") & "', '" & _
                        Replace(txtMetricwMin.Text, "'", "''") & "', '" & freq & "', '" & _
                        Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & cat & "', GetDate(), '" & _
                        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')" & extracat

                Else
                    TitleSql =
                        "UPDATE DWH.KPIS.ScorecardMetric set [SCMName] = '" & Replace(txtMetricName.Text, "'", "''") & "', [SCMObjective] = '" & _
                        Replace(txtMetricObjective.Text, "'", "''") & "', [SCMDefinition] = '" & Replace(txtMetricDefinition.Text, "'", "''") & "', " & _
                        "[SCMCalculations] = '" & Replace(txtMetricCalculated.Text, "'", "''") & "', [SCMSourceSystem] = '" & Replace(txtMetricSourceSystem.Text, "'", "''") & _
                        "', [SCMLTTarget] = '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', [SCMLTGoalDate] = '" & Replace(txtMetricLTGoalDate.Text, "'", "''") & _
                        "', [SCMTarget] = '" & Replace(txtMetricTarget.Text, "'", "''") & "', [SCMDataType] = '" & ddlMetricTargetType.SelectedValue & _
                        "', [SCMCumulative] = '" & rbnewmetcum1.Checked & "', [SCMMax] = '" & Replace(txtMetricMax.Text, "'", "''") & "', [SCMMin] = '" & Replace(txtMetricMin.Text, "'", "''") & _
                        "', [SCMwMax] = '" & Replace(txtMetricwMax.Text, "'", "''") & "', [SCMwMin] = '" & Replace(txtMetricwMin.Text, "'", "''") & _
                        "', [SCMUpdateFrequency] = '" & freq & _
                        "', [SCMOwner] = '" & Replace(txtMetricOwnerlogin.Text, "'", "''") & "',[SCMCategory] = " & cat & _
                        ", [SCMEffectiveFromDate] = GetDate(), SCMActive = 1 ,[SCMUpdated] = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where ID =  " & upId & extracat
                End If


                Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
                    cmd.ExecuteNonQuery()

                End Using


            Catch ex As Exception
                explantionlabel.Text = "Problems creating Metric"
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                MetricModalPopupExtender.Show()
                Exit Sub
            End Try


            Dim MettoSCSql As String = ""

            For Each checkeditem As ListItem In cblnewmetmetriclist.Items
                If (checkeditem.Selected) Then
                    MettoSCSql = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated, SCMCategory) VALUES" & _
                          " ('" & checkeditem.Value & "', (Select SCMID from DWH.KPIS.ScorecardMetric where ID = (Select Max(ID) from DWH.KPIS.ScorecardMetric)), '" & _
                          Replace(txtMetricOwner.Text, "'", "''") + "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                          "', '" & ddlnewMetCat.SelectedValue & "')"

                    Try

                        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If

                            cmd = New System.Data.SqlClient.SqlCommand(MettoSCSql, conn)
                            cmd.ExecuteNonQuery()

                        End Using


                    Catch ex As Exception
                        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    End Try

                End If
            Next

            secret.Text = 1


            MetricInsertRequirements.Text = "Metric Inserted"
            MetricModalPopupExtender.Show()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub btnSCData_Click(sender As Object, e As EventArgs) Handles btnSCData.Click

    End Sub

    Protected Sub dsOhmsMetricSCMName_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles dsOhmsMetricSCMName.Selecting

    End Sub

    Protected Sub ScorecardInsert_Click(sender As Object, e As EventArgs) Handles ScorecardInsert.Click

        Dim SQL As String = ""
        Dim cmd As SqlCommand
        Dim scSCTID As String
        Dim scSCMID As String


        If txtSCSCTID.Text = "" Or txtSCSCMID.Text = "" Then
            SCExplanationLabel.Text = "All Fields are Required"
            SCModalPopupExtender.Show()
            Exit Sub

        End If

        scSCTID = txtSCSCTID.Text
        scSCMID = txtSCSCMID.Text


        Try

            SQL = "Insert INTO DWH.KPIS.Scorecard (SCTID, SCMID, SCModifyDate, SCUpdated, SCActive) Values ('" & _
                Replace(scSCTID, "'", "''") & "', '" & Replace(scSCMID, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()

            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try

        SCExplanationLabel.Text = "Submitted"
        SCModalPopupExtender.Show()

    End Sub

    Protected Sub SDInsertButton_Click(sender As Object, e As EventArgs) Handles SDInsertButton.Click

        Dim SQL As String = ""
        Dim cmd As SqlCommand
        Dim sdFY As String
        Dim sdFM As String
        Dim sdFD As String
        Dim sdSCDActual As String
        Dim sdID As String

        If txtsdID.Text = "" Or txtSDSCDFD.Text = "" Or txtSDSCDFM.Text = "" Or txtSDSCDFY.Text = "" Or txtSDSCDActual.Text = "" Then
            DataExplanationlabel.Text = "All Fields Required"
            DataModalPopupExtender.Show()
            Exit Sub

        End If

        sdFY = txtSDSCDFY.Text
        sdFM = txtSDSCDFM.Text
        sdFD = txtSDSCDFD.Text
        sdSCDActual = txtSDSCDActual.Text
        sdID = txtsdID.Text


        Try

            SQL = "Insert INTO DWH.KPIS.ScorecardData (SCDFY, SCDFM, SCDFD, ID, SCDActual, SCDModifyDate, SCDUpdated, SCDActive) Values ('" & _
                Replace(sdFY, "'", "''") & "', '" & Replace(sdFM, "'", "''") & "', '" & Replace(sdFD, " '", "''") & "', '" & _
                Replace(sdID, "'", "''") & "', '" & Replace(sdSCDActual, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()

            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try

        DataExplanationlabel.Text = "Data Submitted"
        DataModalPopupExtender.Show()


    End Sub

    Protected Sub SCTitleInsertButton_Click(sender As Object, e As EventArgs) Handles SCTitleInsertButton.Click

        Dim SQL As String = ""
        Dim cmd As SqlCommand
        Dim sctSCTitle As String

        If txtSCTSCTitle.Text = "" Or txtSCTSCTitle.Text = "" Then
            TitleExplanationLabel.Text = "Title and Owner are Required Fields"
            TitleModalPopupExtender.Show()
            Exit Sub

        End If

        sctSCTitle = txtSCTSCTitle.Text


        Try

            SQL = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle, SCOwner, SCTActive) Values ('" & _
                Replace(sctSCTitle, "'", "''") & "', '" & Replace(txtSCTSCTitle.Text, "'", "''") & "', 1)"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()

            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try

        TitleExplanationLabel.Text = "Submitted"
        TitleModalPopupExtender.Show()


    End Sub

    Protected Sub scCategoryLUInsert_Click(sender As Object, e As EventArgs) Handles scCategoryLUInsert.Click

        Dim SQL As String = ""
        Dim cmd As SqlCommand
        Dim sccSCCat As String

        If txtSCCSCCat.Text = "" Then
            CatExplanationLabel.Text = "Category Name Required"
            CatModalPopupExtender.Show()
            Exit Sub

        End If

        sccSCCat = txtSCCSCCat.Text


        Try

            SQL = "Insert INTO DWH.KPIS.ScorecardCategory_LU (SCCategory, SCCActive) Values ('" & _
                Replace(sccSCCat, "'", "''") & "', 1)"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()

            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try

        CatExplanationLabel.Text = "Submitted"
        CatModalPopupExtender.Show()

    End Sub

    Protected Sub OhmsULInsert_Click(sender As Object, e As EventArgs) Handles OhmsULInsert.Click

        Dim SQL As String = ""
        Dim cmd As SqlCommand
        Dim getSCTID As String
        Dim getSCUName As String

        If txtboxSCMID0.Text = "" Or txtboxSCMName0.Text = "" Then
            ULExplanationlabel.Text = "SCMID and SCMName are required fields"
            ULModalPopupExtender.Show()
            Exit Sub

        End If

        getSCTID = txtboxSCMID0.Text
        getSCUName = txtboxSCMName0.Text


        Try

            SQL = "Insert INTO DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated, SCUActive) Values ('" & _
                Replace(getSCTID, "'", "''") & "', '" & Replace(getSCUName, "'", "''") & "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
                cmd.ExecuteNonQuery()

            End Using


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try


        ULExplanationlabel.Text = "Submitted"
        ULModalPopupExtender.Show()

    End Sub

    'Protected Sub btnCNSPreview_Click(sender As Object, e As EventArgs) Handles btnCNSPreview.Click

    '    Dim SelectList As String = ""

    '    For Each checkeditem As ListItem In cblCNSMetricSelect.Items
    '        If (checkeditem.Selected) Then
    '            SelectList += checkeditem.Value + ", "

    '        End If
    '    Next


    '    Dim Sql2 As String
    '    Dim ds As DataSet
    '    Dim da As SqlDataAdapter


    '    Try

    '        Sql2 = "select SCMName, SCMDefinition, SCMOwner from DWH.KPIS.ScorecardMetric " & _
    '        "where SCMID in (" & SelectList & "0)"


    '        ds = New DataSet

    '        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            da = New SqlDataAdapter(Sql2, conn2)
    '            da.Fill(ds, "DEMO")
    '            gridCNSPreviewMetric.DataSource = ds
    '            gridCNSPreviewMetric.DataMember = "DEMO"
    '            gridCNSPreviewMetric.DataBind()
    '        End Using





    '    Catch ex As Exception

    '    End Try




    'End Sub


    'Protected Sub btnCNSSubmit_Click(sender As Object, e As EventArgs) Handles btnCNSSubmit.Click


    '    Dim SelectList As String = ""
    '    Dim TitleSql As String = ""
    '    Dim SQL As String = ""
    '    Dim cmd As SqlCommand
    '    Dim SQL2 As String = ""



    '    If txtnewSCName.Text = "" Or txtnewSCOwner.Text = "" Then
    '        ExplanationNewSClabel.Text = "Scorecard Name and Owner Required"
    '        ModalPopupExtender2.Show()
    '        Exit Sub
    '    End If
    '    Try

    '        TitleSql = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle, SCActive) Values ('" & _
    '            Replace(txtnewSCName.Text, "'", "''") & "', 1)"


    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using

    '        For Each checked2item As ListItem In cblCNSUsersSelect.Items
    '            If (checked2item.Selected) Then
    '                SQL2 = "Insert into DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated, SCUActive) VALUES " & _
    '                    "(((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU) + 1), '" + checked2item.Value + "', GetDate(), '" & _
    '                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"
    '                Try
    '                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                        If conn2.State = ConnectionState.Closed Then
    '                            conn2.Open()
    '                        End If

    '                        cmd = New System.Data.SqlClient.SqlCommand(SQL2, conn2)
    '                        cmd.ExecuteNonQuery()
    '                    End Using

    '                Catch ex As Exception

    '                End Try
    '            End If
    '        Next


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    '    For Each checkeditem As ListItem In cblCNSMetricSelect.Items
    '        If (checkeditem.Selected) Then
    '            SQL = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated, SCActive) VALUES" & _
    '                  " (((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU)+ 1), '" + checkeditem.Value + "', '" & _
    '                  Replace(txtnewSCOwner.Text, "'", "''") + "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', 1)"

    '            Try

    '                Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                    If conn.State = ConnectionState.Closed Then
    '                        conn.Open()
    '                    End If

    '                    cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
    '                    cmd.ExecuteNonQuery()

    '                End Using


    '            Catch ex As Exception
    '                Exit Sub
    '            End Try

    '        End If
    '    Next

    '    ExplanationNewSClabel.Text = "Scorecard Submitted"
    '    ModalPopupExtender2.Show()

    'End Sub

    Private Sub OkButton2_Click(sender As Object, e As System.EventArgs) Handles OkButton2.Click

        ModalPopupExtender2.Hide()
    End Sub


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        Try



            Dim alpha As Int16 = 0
            Dim y As Integer

            y = GridView1.Rows.Count

            While alpha < (y)
                If alpha Mod 2 = 0 Then
                    GridView1.Rows(alpha).BackColor = Drawing.Color.White
                    alpha = alpha + 1
                Else
                    GridView1.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#eee4ce")
                    alpha = alpha + 1
                End If
            End While

            GridView1.SelectedRow.BackColor = Drawing.Color.LightSteelBlue

            If UpdateorNew.SelectedIndex = 1 Then
                Dim stupidsql As String
                Dim datasetmain As New DataSet
                Dim dr As DataRow


                Dim SCMIDButton As Integer = 0
                Dim SCMOwnerButton As Integer = 0
                Dim SCMCategoryButton As Integer = 0
                Dim SCMActiveButton As Integer = 0
                Dim SCMActive As Integer = 0

                If rbMetricSCMID1.Checked = True Then
                    SCMIDButton = 1
                End If
                If rbMetricSCOwner1.Checked = True Then
                    SCMOwnerButton = 1
                End If
                If rbMetricSCCat1.Checked = True Then
                    SCMCategoryButton = 1
                End If
                If rbMetActive.Checked = True Then
                    SCMActive = 1
                End If
                If rbMetricActive1.Checked = True Then
                    SCMActiveButton = 1
                End If


                stupidsql = " select * from (" & _
                    "select *, RANK() over (order by ID asc) as rankin from ( " & _
                    "SELECT [ID] ,[SCMID] ,[SCMName] ,[SCMObjective]  ,[SCMMeasures]  ,[SCMDefinition]  ,[SCMCalculations] ,[SCMSourceSystem] " & _
                    " ,[SCMLTTarget] ,[SCMLTGoalDate]  ,[SCMTarget] ,[SCMDataType] ,[SCMCumulative]  ,[SCMMax]  ,[SCMMin] ,[SCMwMax] ,[SCMwMin]" & _
                    "   ,[SCMUpdateMethod]  ,[SCMUpdateInput] ,[SCMUpdateFrequency], ImprovementTrend ,[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMEffectiveToDate]" & _
                    " ,[SCMActive]  ,[SCMUpdated]  FROM [DWH].[KPIS].[ScorecardMetric] " & _
                    "WHERE (SCMID = " & ddlMetricSCMID.SelectedValue.ToString & " or " & SCMIDButton.ToString & " = 0) " & _
                    "and (('" & ddlMetricSCMOwner.SelectedValue.ToString & "' = 'NULL' and SCMOwner is NULL or SCMOwner = '" & ddlMetricSCMOwner.SelectedValue.ToString & "') or " & _
                    SCMOwnerButton.ToString & " = 0) " & _
                    "and (SCMCategory = " & ddlMetricSCCat.SelectedValue.ToString & " or " & SCMCategoryButton.ToString & " = 0)" & _
                    "and (SCMActive = " & SCMActive.ToString & " or " & SCMActiveButton.ToString & " = 0) " & _
                    ") a ) b " & _
                    "where rankin = " & GridView1.SelectedIndex.ToString & " + 1"

                Try

                    Dim da As SqlDataAdapter

                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da = New SqlDataAdapter(stupidsql, conn2)
                        da.Fill(datasetmain)

                    End Using


                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

                'txtMetricOwneremail
                'txtMetricOwner

                dr = datasetmain.Tables(0).Rows(0)
                txtMetricName.Text = dr("SCMName").ToString
                txtMetricCalculated.Text = dr("SCMCalculations").ToString
                txtMetricDefinition.Text = dr("SCMDefinition").ToString
                txtMetricLTGoal.Text = dr("SCMLTTarget").ToString
                txtMetricLTGoalDate.Text = dr("SCMLTGoalDate").ToString
                txtMetricMax.Text = dr("SCMMax").ToString
                txtMetricMin.Text = dr("SCMMin").ToString
                txtMetricObjective.Text = dr("SCMObjective").ToString
                txtMetricOwnerlogin.Text = dr("SCMOwner").ToString
                txtMetricSourceSystem.Text = dr("SCMSourceSystem").ToString
                txtMetricTarget.Text = dr("SCMTarget").ToString
                txtMetricwMax.Text = dr("SCMwMax").ToString
                txtMetricwMin.Text = dr("SCMwMin").ToString
                selectedID = dr("ID").ToString

                If dr("SCMUpdateFrequency") = 52 Then
                    ddlMetricFreq.SelectedIndex = 1
                ElseIf dr("SCMUpdateFrequency") = 1 Then
                    ddlMetricFreq.SelectedIndex = 2
                Else
                    ddlMetricFreq.SelectedIndex = 0
                End If

                If dr("SCMDataType") = "money" Then
                    ddlMetricTargetType.SelectedIndex = 0
                ElseIf dr("SCMDataType") = "percent" Then
                    ddlMetricTargetType.SelectedIndex = 1
                Else
                    ddlMetricTargetType.SelectedIndex = 2
                End If

                If IsDBNull(dr("SCMCumulative")) Then
                    rbnewmetcum1.Checked = False
                    rbnewmetcum2.Checked = False
                ElseIf dr("SCMCumulative").ToString = True Then
                    rbnewmetcum2.Checked = False
                    rbnewmetcum1.Checked = True

                Else
                    rbnewmetcum1.Checked = False
                    rbnewmetcum2.Checked = True

                End If

                If IsDBNull(dr("ImprovementTrend")) Then
                    rbincreasing.Checked = False
                    rbdecreasing.Checked = False
                ElseIf dr("ImprovementTrend") = "Increases" Then
                    rbincreasing.Checked = True
                    rbdecreasing.Checked = False
                ElseIf dr("ImprovementTrend") = "Decreases" Then
                    rbincreasing.Checked = False
                    rbdecreasing.Checked = True
                Else
                    rbincreasing.Checked = False
                    rbdecreasing.Checked = False
                End If
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click

        GridView1.SelectedIndex = 0

        'Dim alpha As Int16 = 0
        'Dim y As Integer

        'y = GridView1.Rows.Count

        'While alpha < (y)
        '    If alpha Mod 2 = 0 Then
        '        GridView1.Rows(alpha).BackColor = Drawing.Color.White
        '        alpha = alpha + 1
        '    Else
        '        GridView1.Rows(alpha).BackColor = System.Drawing.ColorTranslator.FromHtml("#eee4ce")
        '        alpha = alpha + 1
        '    End If
        'End While

        'If GridView1.Rows.Count > 0 Then
        '    GridView1.Rows(0).BackColor = Drawing.Color.LightSteelBlue
        'End If
    End Sub

    Private Sub UpdateorNew_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles UpdateorNew.SelectedIndexChanged

        Try


            If GridView1.SelectedIndex = -1 Then
                Exit Sub
            End If
            If UpdateorNew.SelectedValue = 1 Then
                Dim stupidsql As String
                Dim datasetmain As New DataSet
                Dim dr As DataRow

                Dim SCMIDButton As Integer = 0
                Dim SCMOwnerButton As Integer = 0
                Dim SCMCategoryButton As Integer = 0
                Dim SCMActiveButton As Integer = 0
                Dim SCMActive As Integer = 0

                If rbMetricSCMID1.Checked = True Then
                    SCMIDButton = 1
                End If
                If rbMetricSCOwner1.Checked = True Then
                    SCMOwnerButton = 1
                End If
                If rbMetricSCCat1.Checked = True Then
                    SCMCategoryButton = 1
                End If
                If rbMetActive.Checked = True Then
                    SCMActive = 1
                End If
                If rbMetricActive1.Checked = True Then
                    SCMActiveButton = 1
                End If

                stupidsql = " select * from (" & _
                    "select *, RANK() over (order by ID asc) as rankin from ( " & _
                    "SELECT [ID] ,[SCMID] ,[SCMName] ,[SCMObjective]  ,[SCMMeasures]  ,[SCMDefinition]  ,[SCMCalculations] ,[SCMSourceSystem] " & _
                    " ,[SCMLTTarget] ,[SCMLTGoalDate]  ,[SCMTarget] ,[SCMDataType] ,[SCMCumulative]  ,[SCMMax]  ,[SCMMin] ,[SCMwMax] ,[SCMwMin]" & _
                    "   ,[SCMUpdateMethod]  ,[SCMUpdateInput] ,[SCMUpdateFrequency], ImprovementTrend ,[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMEffectiveToDate]" & _
                    " ,[SCMActive]  ,[SCMUpdated]  FROM [DWH].[KPIS].[ScorecardMetric] " & _
                    "WHERE (SCMID = " & ddlMetricSCMID.SelectedValue.ToString & " or " & SCMIDButton.ToString & " = 0) " & _
                    "and (('" & ddlMetricSCMOwner.SelectedValue.ToString & "' = 'NULL' and SCMOwner is NULL or SCMOwner = '" & ddlMetricSCMOwner.SelectedValue.ToString & "') or " & _
                    SCMOwnerButton.ToString & " = 0) " & _
                    "and (SCMCategory = " & ddlMetricSCCat.SelectedValue.ToString & " or " & SCMCategoryButton.ToString & " = 0)" & _
                    "and (SCMActive = " & SCMActive.ToString & " or " & SCMActiveButton.ToString & " = 0) " & _
                    ") a ) b " & _
                    "where rankin = " & GridView1.SelectedIndex.ToString & " + 1"

                '            <asp:ControlParameter ControlID="ddlMetricSCMID" Name="SCMID" 
                '    PropertyName="SelectedValue" />
                '<asp:ControlParameter ControlID="rbMetricSCMID1" Name="SCMIDButton" 
                '    PropertyName="Checked" />
                '<asp:ControlParameter ControlID="ddlMetricSCMOwner" Name="SCMOwner" 
                '    PropertyName="SelectedValue" />
                '<asp:ControlParameter ControlID="rbMetricSCOwner1" Name="SCMOwnerButton" 
                '    PropertyName="Checked" />
                '<asp:ControlParameter ControlID="ddlMetricSCCat" Name="SCMCategory" 
                '    PropertyName="SelectedValue" />
                '<asp:ControlParameter ControlID="rbMetricSCCat1" Name="SCMCategoryButton" 
                '    PropertyName="Checked" />
                '<asp:ControlParameter ControlID="rbMetricActive1" Name="SCMActiveButton" 
                '    PropertyName="Checked" />
                '<asp:ControlParameter ControlID="rbMetActive" Name="SCMActive" 
                '    PropertyName="Checked" />   


                Try

                    Dim da As SqlDataAdapter

                    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                        da = New SqlDataAdapter(stupidsql, conn2)
                        da.Fill(datasetmain)

                    End Using


                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

                'txtMetricOwneremail
                'txtMetricOwner

                dr = datasetmain.Tables(0).Rows(0)
                txtMetricName.Text = dr("SCMName").ToString
                txtMetricCalculated.Text = dr("SCMCalculations").ToString
                txtMetricDefinition.Text = dr("SCMDefinition").ToString
                txtMetricLTGoal.Text = dr("SCMLTTarget").ToString
                txtMetricLTGoalDate.Text = dr("SCMLTGoalDate").ToString
                txtMetricMax.Text = dr("SCMMax").ToString
                txtMetricMin.Text = dr("SCMMin").ToString
                txtMetricObjective.Text = dr("SCMObjective").ToString
                txtMetricOwnerlogin.Text = dr("SCMOwner").ToString
                txtMetricSourceSystem.Text = dr("SCMSourceSystem").ToString
                txtMetricTarget.Text = dr("SCMTarget").ToString
                txtMetricwMax.Text = dr("SCMwMax").ToString
                txtMetricwMin.Text = dr("SCMwMin").ToString
                selectedID = dr("ID").ToString
                ddlnewMetCat.SelectedValue = dr("SCMCategory")

                If dr("SCMUpdateFrequency") = 52 Then
                    ddlMetricFreq.SelectedIndex = 1
                ElseIf dr("SCMUpdateFrequency") = 1 Then
                    ddlMetricFreq.SelectedIndex = 2
                Else
                    ddlMetricFreq.SelectedIndex = 0
                End If

                If dr("SCMDataType") = "money" Then
                    ddlMetricTargetType.SelectedIndex = 0
                ElseIf dr("SCMDataType") = "percent" Then
                    ddlMetricTargetType.SelectedIndex = 1
                Else
                    ddlMetricTargetType.SelectedIndex = 2
                End If

                If IsDBNull(dr("SCMCumulative")) Then
                    rbnewmetcum1.Checked = False
                    rbnewmetcum2.Checked = False
                ElseIf dr("SCMCumulative").ToString = True Then
                    rbnewmetcum2.Checked = False
                    rbnewmetcum1.Checked = True

                Else
                    rbnewmetcum1.Checked = False
                    rbnewmetcum2.Checked = True

                End If

                If IsDBNull(dr("ImprovementTrend")) Then
                    rbincreasing.Checked = False
                    rbdecreasing.Checked = False
                ElseIf dr("ImprovementTrend") = "Increases" Then
                    rbincreasing.Checked = True
                    rbdecreasing.Checked = False
                ElseIf dr("ImprovementTrend") = "Decreases" Then
                    rbincreasing.Checked = False
                    rbdecreasing.Checked = True
                Else
                    rbincreasing.Checked = False
                    rbdecreasing.Checked = False

                End If
            Else
                txtMetricName.Text = ""
                txtMetricCalculated.Text = ""
                txtMetricDefinition.Text = ""
                txtMetricLTGoal.Text = ""
                txtMetricLTGoalDate.Text = ""
                txtMetricMax.Text = ""
                txtMetricMin.Text = ""
                txtMetricObjective.Text = ""
                txtMetricOwnerlogin.Text = ""
                txtMetricSourceSystem.Text = ""
                txtMetricTarget.Text = ""
                txtMetricwMax.Text = ""
                txtMetricwMin.Text = ""
                ddlMetricFreq.SelectedIndex = 0
                ddlnewMetCat.SelectedIndex = 0
                ddlMetricTargetType.SelectedIndex = 2
                rbnewmetcum1.Checked = False
                rbnewmetcum2.Checked = False
                rbincreasing.Checked = False
                rbdecreasing.Checked = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlMetricFreq_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMetricFreq.SelectedIndexChanged

        Try

            If CInt(ddlMetricFreq.SelectedValue) = 0 Then
                If secret.Text = 1 Then
                    reqfreqdesc.Text = "Desired Number of Times <br /> Measured per Year"
                    secret.Text = 2
                End If

                ReqFreq.Visible = True
                reqfreqdesc.Visible = True

            Else
                ReqFreq.Visible = False
                reqfreqdesc.Visible = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub ddlnewMetCat_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlnewMetCat.SelectedIndexChanged
        If CInt(ddlnewMetCat.SelectedValue) = 0 Then
            CatRequest.Visible = True
        Else
            CatRequest.Visible = False
            CatRequest.Text = ""

        End If
    End Sub

    Protected Sub btnCNSPreview_Click(sender As Object, e As EventArgs) Handles btnCNSPreview.Click

        Dim SelectList As String = ""

        For Each checkeditem As ListItem In cblCNSMetricSelect.Items
            If (checkeditem.Selected) Then
                SelectList += checkeditem.Value + ", "

            End If
        Next

        For Each checkeditem As ListItem In cblpublicMetrics.Items
            If (checkeditem.Selected) Then
                SelectList += checkeditem.Value + ", "

            End If
        Next

        Dim Sql2 As String
        Dim ds As DataSet
        Dim da As SqlDataAdapter


        Try

            Sql2 = "select SCMName, SCMDefinition, SCMOwner from DWH.KPIS.ScorecardMetric " & _
            "where ID in (" & SelectList & "0) order by SCMName asc"


            ds = New DataSet

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(Sql2, conn2)
                da.Fill(ds, "DEMO")
                gridCNSPreviewMetric.DataSource = ds
                gridCNSPreviewMetric.DataMember = "DEMO"
                gridCNSPreviewMetric.DataBind()
            End Using





        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try




    End Sub


    Protected Sub btnCNSSubmit_Click(sender As Object, e As EventArgs) Handles btnCNSSubmit.Click


        Dim SelectList As String = ""
        Dim TitleSql As String = ""
        Dim SQL As String = ""
        Dim cmd As SqlCommand
        Dim SQL2 As String = ""

        'Page.Validate("CNSValdiation")

        If txtnewSCName.Text = "" Then
            ExplanationNewSClabel.Text = "Scorecard Name Required"
            ModalPopupExtender2.Show()
            Exit Sub
        End If
        Try

            TitleSql = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle, SCOwner) Values ('" & _
                Replace(txtnewSCName.Text, "'", "''") & "', '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') " & _
                "Insert INTO DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated) VALUES " & _
                "((Select Max(SCTID) from DWH.KPIS.SCorecardTitle_LU), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"


            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
                cmd.ExecuteNonQuery()

            End Using

            For Each checked2item As ListItem In cblCNSUsersSelect.Items
                If (checked2item.Selected) Then
                    SQL2 = "Insert into DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated) VALUES " & _
                        "((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checked2item.Value + "', GetDate(), '" & _
                        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"
                    Try
                        Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                            If conn2.State = ConnectionState.Closed Then
                                conn2.Open()
                            End If

                            cmd = New System.Data.SqlClient.SqlCommand(SQL2, conn2)
                            cmd.ExecuteNonQuery()
                        End Using

                    Catch ex As Exception
                        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    End Try
                End If
            Next


        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            Exit Sub
        End Try

        For Each checkeditem As ListItem In cblCNSMetricSelect.Items
            If (checkeditem.Selected) Then
                SQL = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCModifyDate, SCUpdated) VALUES" & _
                      " ((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checkeditem.Value & _
                      "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

                Try

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
                        cmd.ExecuteNonQuery()

                    End Using


                Catch ex As Exception
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                End Try

            End If
        Next

        'For Each checkeditem As ListItem In cblpublicMetrics.Items
        '    If (checkeditem.Selected) Then
        '        SQL = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCModifyDate, SCUpdated) VALUES" & _
        '              " ((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checkeditem.Value & _
        '              "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

        '        Try

        '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

        '                If conn.State = ConnectionState.Closed Then
        '                    conn.Open()
        '                End If

        '                cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
        '                cmd.ExecuteNonQuery()

        '            End Using


        '        Catch ex As Exception
        '        End Try

        '    End If
        'Next

        ExplanationNewSClabel.Text = "Scorecard Submitted"
        ModalPopupExtender2.Show()

    End Sub


    Private Sub txtnewSCOwner_TextChanged(sender As Object, e As System.EventArgs) Handles txtnewSCOwner.TextChanged

        Dim ownedmetrics As String
        Dim publicmetrics As String

        ownedmetrics = "Select ID, SCMName from " & _
 "(select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID order by SCMActive desc, SCMEffectiveToDate desc," & _
 " SCMEffectiveFromDate desc, ID desc) as rankin from DWH.KPIS.ScorecardMetric) a " & _
 "where SCMOwner = '" & Replace(txtnewSCOwner.Text, "'", "''") & "' and rankin = 1 " & _
 "union " & _
 "Select ID, SCMName from (select ID, SCMName, SCMOwner, SCMID, RANK() over " & _
 "(partition by SCMID order by SCMActive desc, SCMEffectiveToDate desc, " & _
 "SCMEffectiveFromDate desc, ID desc) as rankin from " & _
 "(select * from DWH.KPIS.ScorecardMetric where SCMID in (select SCMID from " & _
 "DWH.KPIS.Scorecard sc left join DWH.KPIS.ScorecardTitle_LU st on sc.SCTID = st.SCTID " & _
 "where st.SCOwner = '" & Replace(txtnewSCOwner.Text, "'", "''") & "') ) a ) b " & _
 "where SCMOwner <> '" & Replace(txtnewSCOwner.Text, "'", "''") & "' and rankin = 1 " & _
 "order by SCMName asc"

        publicmetrics = " Select ID, SCMName from (select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID order by SCMActive desc, SCMEffectiveToDate desc, " & _
               "SCMEffectiveFromDate desc, ID desc) as rankin from DWH.KPIS.ScorecardMetric) a " & _
                "where SCMOwner <> '" & Replace(txtnewSCOwner.Text, "'", "''") & "' and rankin = 1 and SCMID not in (select SCMID from " & _
                "DWH.KPIS.Scorecard sc left join DWH.KPIS.ScorecardTitle_LU st on sc.SCTID = st.SCTID " & _
                "where st.SCOwner = '" & Replace(txtnewSCOwner.Text, "'", "''") & "')   order by SCMName asc"



        Try

            Dim ds As DataSet
            ds = New DataSet
            Dim da As SqlDataAdapter

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(ownedmetrics, conn2)
                da.Fill(ds)
                cblCNSMetricSelect.DataSource = ds
                cblCNSMetricSelect.DataTextField = "SCMName"
                cblCNSMetricSelect.DataValueField = "ID"
                cblCNSMetricSelect.DataBind()
            End Using


        Catch ex As Exception
            cblCNSMetricSelect.Style(HtmlTextWriterStyle.Display) = "none"
        End Try


        Try

            Dim ds As DataSet
            ds = New DataSet
            Dim da As SqlDataAdapter

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(publicmetrics, conn2)
                da.Fill(ds)
                cblpublicMetrics.DataSource = ds
                cblpublicMetrics.DataTextField = "SCMName"
                cblpublicMetrics.DataValueField = "ID"
                cblpublicMetrics.DataBind()
            End Using


        Catch ex As Exception
            cblpublicMetrics.Style(HtmlTextWriterStyle.Display) = "none"
        End Try
    End Sub
End Class