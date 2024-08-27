Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

'lastversion
'Public Class OHMS_NewMetric
'    Inherits System.Web.UI.Page
'    Public datasetmain As New DataSet
'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Dim loadsql As String
'        Dim loadsql2 As String

'        If IsPostBack Then
'            Exit Sub
'        End If


'        loadsql = "SELECT DISTINCT tlu.SCTID, SCTitle FROM DWH.KPIS.Scorecard sc JOIN DWH.KPIS.ScorecardTitle_LU tlu" & _
'            " on sc.SCTID = tlu.SCTID where SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' order by SCTitle asc"

'        loadsql2 = "SELECT ID, SCMName FROM (Select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID " & _
'            "order by SCMEffectiveToDate desc) as rankin FROM DWH.KPIS.ScorecardMetric ) a " & _
'            "where SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' order by SCMName asc"

'        Try

'            Dim ds As DataSet
'            ds = New DataSet
'            Dim da As SqlDataAdapter

'            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
'                da = New SqlDataAdapter(loadsql, conn2)
'                da.Fill(ds)
'                cblnewmetmetriclist.DataSource = ds
'                cblnewmetmetriclist.DataTextField = "SCTitle"
'                cblnewmetmetriclist.DataValueField = "SCTID"
'                cblnewmetmetriclist.DataBind()
'            End Using


'        Catch ex As Exception

'        End Try

'        Try

'            Dim ds As DataSet
'            ds = New DataSet
'            Dim da As SqlDataAdapter

'            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
'                da = New SqlDataAdapter(loadsql2, conn2)
'                da.Fill(ds)
'                ddlMetricUpSelect.DataSource = ds
'                ddlMetricUpSelect.DataTextField = "SCMName"
'                ddlMetricUpSelect.DataValueField = "ID"
'                ddlMetricUpSelect.DataBind()
'            End Using


'        Catch ex As Exception

'        End Try

'        Dim stupidsql As String

'        stupidsql = "Select * FROM DWH.KPIS.ScorecardMetric where ID = '" & ddlMetricUpSelect.SelectedValue & "'"

'        Try

'            Dim da As SqlDataAdapter

'            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
'                da = New SqlDataAdapter(stupidsql, conn2)
'                da.Fill(datasetmain)

'            End Using


'        Catch ex As Exception

'        End Try

'    End Sub
'    Protected Sub btnNewMetricSubmit_Click(sender As Object, e As EventArgs) Handles btnNewMetricSubmit.Click


'        Dim SelectList As String = ""
'        Dim TitleSql As String = ""
'        Dim SQL As String = ""
'        Dim cmd As SqlCommand




'        If txtMetricName.Text = "" Or txtMetricTarget.Text = "" Or txtMetricOwnerlogin.Text = "" Then
'            explantionlabel.Text = "Please Fill in all required fields"
'            ModalPopupExtender1.Show()
'            Exit Sub
'        End If


'        Try

'            TitleSql = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMMeasures],[SCMDefinition]" & _
'      ", [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
'      ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin] ,[SCMUpdateFrequency]" & _
'      ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMUpdated]) Values ((select MAX(SCMID) from KPIS.ScorecardMetric) + 1, '" & _
'           Replace(txtMetricName.Text, "'", "''") & "', '" & Replace(txtMetricObjective.Text, "'", "''") & "', '" & _
'           Replace(txtMetricMeasure.Text, "'", "''") & "', '" & Replace(txtMetricDefinition.Text, "'", "''") & "', '" & Replace(txtMetricCalculated.Text, "'", "''") & "', '" & _
'            Replace(txtMetricSourceSystem.Text, "'", "''") & "', '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', '" & _
'            Replace(txtMetricLTGoalDate.Text, "'", "''") & "', '" & Replace(txtMetricTarget.Text, "'", "''") & "', '" & _
'            ddlMetricTargetType.SelectedValue & "', '" & rbnewmetcum1.Checked & "', '" & Replace(txtMetricMax.Text, "'", "''") & _
'            "', '" & Replace(txtMetricMin.Text, "'", "''") & "', '" & Replace(txtMetricwMax.Text, "'", "''") & "', '" & _
'            Replace(txtMetricwMin.Text, "'", "''") & "', '" & ddlMetricFreq.SelectedValue & "', '" & _
'            Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & ddlnewMetCat.SelectedValue & "', GetDate(), '" & _
'            Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

'            Whatisworng.Text = TitleSql
'            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

'                If conn.State = ConnectionState.Closed Then
'                    conn.Open()
'                End If

'                cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
'                cmd.ExecuteNonQuery()

'            End Using


'        Catch ex As Exception
'            Exit Sub
'        End Try


'        Dim MettoSCSql As String = ""

'        For Each checkeditem As ListItem In cblnewmetmetriclist.Items
'            If (checkeditem.Selected) Then
'                MettoSCSql = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated, SCMCategory) VALUES" & _
'                      " ('" & checkeditem.Value & "', (Select SCMID from DWH.KPIS.ScorecardMetric where ID = (Select Max(ID) from DWH.KPIS.ScorecardMetric)), '" & _
'                      Replace(txtMetricOwner.Text, "'", "''") + "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
'                      "', '" & ddlnewMetCat.SelectedValue & "')"

'                Try

'                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

'                        If conn.State = ConnectionState.Closed Then
'                            conn.Open()
'                        End If

'                        cmd = New System.Data.SqlClient.SqlCommand(MettoSCSql, conn)
'                        cmd.ExecuteNonQuery()

'                    End Using


'                Catch ex As Exception

'                End Try

'            End If
'        Next

'        explantionlabel.Text = "Metric Submitted"
'        ModalPopupExtender1.Show()
'    End Sub
'    Private Sub GridRequestedUpdates_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridRequestedUpdates.RowUpdating



'        Dim UpdateSql As String = ""
'        Dim cmd As SqlCommand
'        Dim ddlmagic As DropDownList
'        Dim magic As String

'        ddlmagic = GridRequestedUpdates.Rows(e.RowIndex).FindControl("ddlDataType")
'        magic = ddlmagic.SelectedValue.ToString


'        Try

'            UpdateSql = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMMeasures]" & _
'      ", SCMDefinition, [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
'      ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin], SCMUpdateMethod, SCMUpdateInput, [SCMUpdateFrequency]" & _
'      ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate], SCMEffectiveToDate, [SCMUpdated]) Values (" & _
'            "(select SCMID FROM DWH.KPIS.ScorecardMetric where ID = " & ddlMetricUpSelect.SelectedValue & "), '" & Replace(e.NewValues.Item(0), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(1), "'", "''") & "', '" & Replace(e.NewValues.Item(2), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(3), "'", "''") & "', '" & Replace(e.NewValues.Item(4), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(5), "'", "''") & "', '" & Replace(e.NewValues.Item(6), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(7), "'", "''") & "', '" & Replace(e.NewValues.Item(8), "'", "''") & "', " & _
'            "case when " & magic & " <> 'none' then " & magic & " else (Select SCMDataType from DWH.KPIS.ScorecardMetric where ID = " & _
'            ddlMetricUpSelect.SelectedValue & ") end, '" & _
'            Replace(e.NewValues.Item(9), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(10), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(11), "'", "''") & "', '" & Replace(e.NewValues.Item(12), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(13), "'", "''") & "', '" & Replace(e.NewValues.Item(14), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(15), "'", "''") & "', '" & Replace(e.NewValues.Item(16), "'", "''") & "', '" & _
'            Replace(e.NewValues.Item(17), "'", "''") & "', '" & Replace(e.NewValues.Item(18), "'", "''") &
'             "', GetDate(), '" & Replace(e.NewValues.Item(19), "'", "''") & "', '" & _
'            Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

'            UpdateSql = Replace(UpdateSql, "''", "NULL")

'            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

'                If conn.State = ConnectionState.Closed Then
'                    conn.Open()
'                End If

'                cmd = New System.Data.SqlClient.SqlCommand(UpdateSql, conn)
'                cmd.ExecuteNonQuery()

'            End Using


'        Catch ex As Exception
'            Exit Sub
'        End Try

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

'            TitleSql = "Insert INTO DWH.KPIS.ScorecardTitle_LU (SCTitle) Values ('" & _
'                Replace(txtnewSCName.Text, "'", "''") & "')"


'            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

'                If conn.State = ConnectionState.Closed Then
'                    conn.Open()
'                End If

'                cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
'                cmd.ExecuteNonQuery()

'            End Using

'            For Each checked2item As ListItem In cblCNSUsersSelect.Items
'                If (checked2item.Selected) Then
'                    SQL2 = "Insert into DWH.KPIS.ScorecardUser_LU (SCTID, SCUName, SCCModifyDate, SCCUpdated) VALUES " & _
'                        "((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checked2item.Value + "', GetDate(), '" & _
'                        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"
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
'                SQL = "Insert into DWH.KPIS.Scorecard (SCTID, SCMID, SCOwner, SCModifyDate, SCUpdated) VALUES" & _
'                      " ((Select Max(SCTID) from DWH.KPIS.ScorecardTitle_LU), '" + checkeditem.Value + "', '" & _
'                      Replace(txtnewSCOwner.Text, "'", "''") + "', GetDate(), '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

'                Try

'                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

'                        If conn.State = ConnectionState.Closed Then
'                            conn.Open()
'                        End If

'                        cmd = New System.Data.SqlClient.SqlCommand(SQL, conn)
'                        cmd.ExecuteNonQuery()

'                    End Using


'                Catch ex As Exception
'                End Try

'            End If
'        Next

'        ExplanationNewSClabel.Text = "Scorecard Submitted"
'        ModalPopupExtender2.Show()

'    End Sub
'    Private Sub OkButton_Click(sender As Object, e As System.EventArgs) Handles OkButton.Click

'        ModalPopupExtender1.Hide()
'    End Sub
'    Private Sub OkButton2_Click(sender As Object, e As System.EventArgs) Handles OkButton.Click

'        ModalPopupExtender2.Hide()
'    End Sub


'End Class 

Public Class OHMS_NewMetric
    Inherits System.Web.UI.Page
    'Public datasetmain As New DataSet
    Public Shared scorecardcontact As String
    Public Shared scorecardcontactemail As String
    Public Shared dr As DataRow


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loadsql As String
        'Dim loadsql2 As String
        'Dim loadsql3 As String
        Dim loadsql4 As String
        Dim ownedmetrics As String
        'Dim publicmetrics As String

        loadsql = "SELECT DISTINCT tlu.SCTID, SCTitle FROM DWH.KPIS.Scorecard sc JOIN DWH.KPIS.ScorecardTitle_LU tlu" & _
                   " on sc.SCTID = tlu.SCTID where tlu.SCOwner = '" & _
                      Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' order by SCTitle asc"

        cblnewmetmetriclist.Style(HtmlTextWriterStyle.Display) = CheckBoxList.DisabledCssClass

        Try

            Dim ds As DataSet
            ds = New DataSet
            Dim da As SqlDataAdapter

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(loadsql, conn2)
                da.Fill(ds)
                cblnewmetmetriclist.DataSource = ds
                cblnewmetmetriclist.DataTextField = "SCTitle"
                cblnewmetmetriclist.DataValueField = "SCTID"
                cblnewmetmetriclist.DataBind()
            End Using


        Catch ex As Exception
            cblnewmetmetriclist.Style(HtmlTextWriterStyle.Display) = "none"
        End Try

        If cblnewmetmetriclist.Items.Count() < 1 Then
            GrayCell.ForeColor = Drawing.Color.LightSlateGray
            cblnewmetmetriclist.Style(HtmlTextWriterStyle.Display) = "none"
            InvGrayBox.Visible = True
        End If

        If IsPostBack Then

            Exit Sub
        End If

        ddlMetricUpSelect.Style(HtmlTextWriterStyle.Display) = "none"
        lblSelectMetric.Style(HtmlTextWriterStyle.Display) = "none"

        scorecardcontact = "Chelsea Weirich"
        scorecardcontactemail = "chelsea.weirich@northside.com"


        contactlabel.Text = " If you have any questions <br /> regarding this form, <br />" & _
            "contact " & scorecardcontact & " at " & scorecardcontactemail
        contactlabel.DataBind()

        'loadsql = "SELECT DISTINCT tlu.SCTID, SCTitle FROM DWH.KPIS.Scorecard sc JOIN DWH.KPIS.ScorecardTitle_LU tlu" & _
        '    " on sc.SCTID = tlu.SCTID where tlu.SCOwner = '" & _
        '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' order by SCTitle asc"

        'loadsql2 = "SELECT ID, SCMName FROM (Select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID " & _
        '    "order by SCMEffectiveToDate desc) as rankin FROM DWH.KPIS.ScorecardMetric ) a " & _
        '    "where SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' order by SCMName asc"

        'loadsql3 = "SELECT ID, SCMName FROM (Select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID " & _
        '    "order by SCMEffectiveToDate desc) as rankin FROM DWH.KPIS.ScorecardMetric ) a " & _
        '    "where SCMOwner <> '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' order by SCMName asc"

        loadsql4 = "Select SCUName, case when FirstName is NULL and LastName is NULL then SCUName else FirstName + ' ' + " & _
            "LastName end as Name FROM DWH.KPIS.ScorecardUser_LU ul left JOIN DWH.dbo.Email_Distribution ed on ul.SCUName = ed.NetworkLogin " & _
            "where SCUName <> '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'  group by SCUName, FirstName, " & _
            "LastName Order by case when FirstName is NULL then 1 else 0 end asc, LastName asc, FirstName asc"

        ownedmetrics = "Select ID, SCMName from " & _
                "(select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID order by SCMActive desc, SCMEffectiveToDate desc," & _
                " SCMEffectiveFromDate desc, ID desc) as rankin from DWH.KPIS.ScorecardMetric) a " & _
                "where SCMOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and rankin = 1 " & _
                "union " & _
                "Select ID, SCMName from (select ID, SCMName, SCMOwner, SCMID, RANK() over " & _
                "(partition by SCMID order by SCMActive desc, SCMEffectiveToDate desc, " & _
                "SCMEffectiveFromDate desc, ID desc) as rankin from " & _
                "(select * from DWH.KPIS.ScorecardMetric where SCMID in (select SCMID from " & _
                "DWH.KPIS.Scorecard sc left join DWH.KPIS.ScorecardTitle_LU st on sc.SCTID = st.SCTID " & _
                "where st.SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') ) a ) b " & _
                "where SCMOwner <> '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and rankin = 1 " & _
                "order by SCMName asc"

        'publicmetrics = " Select ID, SCMName from (select ID, SCMName, SCMOwner, SCMID, RANK() over (partition by SCMID order by SCMActive desc, SCMEffectiveToDate desc, " & _
        '        "SCMEffectiveFromDate desc, ID desc) as rankin from DWH.KPIS.ScorecardMetric) a " & _
        '        "where SCMOwner <> '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and rankin = 1 and SCMID not in (select SCMID from " & _
        '        "DWH.KPIS.Scorecard sc left join DWH.KPIS.ScorecardTitle_LU st on sc.SCTID = st.SCTID " & _
        '        "where st.SCOwner = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')   order by SCMName asc"


        Try

            Dim ds As DataSet
            ds = New DataSet
            Dim da As SqlDataAdapter

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(loadsql4, conn2)
                da.Fill(ds)
                cblCNSUsersSelect.DataSource = ds
                cblCNSUsersSelect.DataTextField = "Name"
                cblCNSUsersSelect.DataValueField = "SCUName"
                cblCNSUsersSelect.DataBind()
                End Using


        Catch ex As Exception
            cblCNSUsersSelect.Style(HtmlTextWriterStyle.Display) = "none"
            End Try


        'Try

        '    Dim ds As DataSet
        '    ds = New DataSet
        '    Dim da As SqlDataAdapter

        '    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
        '        da = New SqlDataAdapter(loadsql, conn2)
        '        da.Fill(ds)
        '        cblnewmetmetriclist.DataSource = ds
        '        cblnewmetmetriclist.DataTextField = "SCTitle"
        '        cblnewmetmetriclist.DataValueField = "SCTID"
        '        cblnewmetmetriclist.DataBind()
        '    End Using


        'Catch ex As Exception
        '    cblnewmetmetriclist.Style(HtmlTextWriterStyle.Display) = "none"
        'End Try

        'If cblnewmetmetriclist.Items.Count() < 1 Then
        '    GrayCell.ForeColor = Drawing.Color.LightSlateGray
        '    cblnewmetmetriclist.Style(HtmlTextWriterStyle.Display) = "none"
        '    InvGrayBox.Visible = True
        'End If

        'Try

        '    Dim ds As DataSet
        '    ds = New DataSet
        '    Dim da As SqlDataAdapter

        '    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
        '        da = New SqlDataAdapter(loadsql2, conn2)
        '        da.Fill(ds)


        '        End Using


        'Catch ex As Exception
        '        '  cblCNSMetricSelect.Style(HtmlTextWriterStyle.Display) = "none"
        '    End Try

        Try

            Dim ds As DataSet
            ds = New DataSet
            Dim da As SqlDataAdapter

            Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(ownedmetrics, conn2)
                da.Fill(ds)
                ddlMetricUpSelect.DataSource = ds
                ddlMetricUpSelect.DataTextField = "SCMName"
                ddlMetricUpSelect.DataValueField = "ID"
                ddlMetricUpSelect.DataBind()
                cblCNSMetricSelect.DataSource = ds
                cblCNSMetricSelect.DataTextField = "SCMName"
                cblCNSMetricSelect.DataValueField = "ID"
                cblCNSMetricSelect.DataBind()
                End Using


        Catch ex As Exception
            cblCNSMetricSelect.Style(HtmlTextWriterStyle.Display) = "none"
            End Try


        'Try

        '    Dim ds As DataSet
        '    ds = New DataSet
        '    Dim da As SqlDataAdapter

        '    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
        '        da = New SqlDataAdapter(publicmetrics, conn2)
        '        da.Fill(ds)
        '        cblpublicMetrics.DataSource = ds
        '        cblpublicMetrics.DataTextField = "SCMName"
        '        cblpublicMetrics.DataValueField = "ID"
        '        cblpublicMetrics.DataBind()
        '        End Using


        'Catch ex As Exception
        '    cblpublicMetrics.Style(HtmlTextWriterStyle.Display) = "none"
        '    End Try


        If ddlMetricUpSelect.Items.Count > 0 Then
            UpdateorNew.Visible = True
        Else
            UpdateorNew.Visible = False
            End If

            'Dim stupidsql As String

            'stupidsql = "Select * FROM DWH.KPIS.ScorecardMetric where ID = '" & ddlMetricUpSelect.SelectedValue & "'"

            'Try

            '    Dim da As SqlDataAdapter

            '    Using conn2 As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            '        da = New SqlDataAdapter(stupidsql, conn2)
            '        da.Fill(datasetmain)

            '    End Using


            'Catch ex As Exception

            'End Try

    End Sub

    Protected Sub btnNewMetricSubmit_Click(sender As Object, e As EventArgs) Handles btnNewMetricSubmit.Click

        Try


            Dim SelectList As String = ""
            Dim TitleSql As String = ""
            Dim SQL As String = ""
            Dim cmd As SqlCommand
            Dim EDsql As String = ""

            'Page.Validate("MetricRequestForm")

            If txtMetricName.Text = "" Or txtMetricTarget.Text = "" Or txtMetricOwnerlogin.Text = "" Or txtMetricOwner.Text = "" Or txtMetricOwneremail.Text = "" Then
                explantionlabel.Text = "Please Fill in all required fields"
                ModalPopupExtender1.Show()

                Exit Sub
            End If

            If InStr(txtMetricOwner.Text, ",") Then

            End If

            Dim checksql As String
            Dim x As String
            x = "2"

            checksql = "Select count(*) from DWH.dbo.Email_Distribution where NetworkLogin = '" & Replace(txtMetricOwnerlogin.Text, "'", "''") & "'"

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
            ElseIf Not IsDBNull(dr("SCMActive")) Then
                If dr("SCMActive") = True Then
                    upId = "(select MAX(SCMID) from KPIS.ScorecardMetric) + 1"
                Else
                    upId = ddlMetricUpSelect.SelectedValue.ToString
                End If
            Else
                upId = ddlMetricUpSelect.SelectedValue.ToString
            End If

            Dim metcat As String

            If ddlnewMetCat.SelectedValue = 0 Then
                metcat = "declare @MainBody varchar(max) " & _
                "declare @Table varchar(max) " & _
                "set @MainBody = '" & _
                "<html><body><H3> New OHMS Category Requested </H3><br><br>' " & _
                "set @Table = '" & _
                "<table border=""1"">" & _
                "<tr><th align=""Right""> User: </th>' + '<td>' + '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' + '</td>' + '" & _
                "</tr><tr><th align=""Right""> New Category: </th>' + '<td>' + '" & CatRequest.Text & "' + '</td>' + '" & _
                "</tr><tr><th alight=""Right""> Metric ID: </th>' + '<td>' + convert(varchar, " & upId & ") + '</td>' + '" & _
                "</tr></table> ' " & _
                "Set @MainBody = @MainBody + @Table + '</body></html>' " & _
                "BEGIN " & _
                "exec msdb.dbo.sp_send_dbmail " & _
                "@recipients = '" & scorecardcontactemail & "' , " & _
                "@subject = 'OHMS -- Category Requested', " & _
                "@body = @MainBody, " & _
                "@body_format = 'HTML' " & _
                "End"


                'End If
                Try
                    '    metcat = "BEGIN " & _
                    '"exec msdb.dbo.sp_send_dbmail " & _
                    '"@recipients = 'Chelsea.Weirich@northside.com' , " & _
                    '"@subject = 'PreMedex File Errors - ATTENTION NEEDED', " & _
                    '"@body = 'test' " & _
                    '"End"

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        cmd = New System.Data.SqlClient.SqlCommand(metcat, conn)
                        cmd.ExecuteNonQuery()

                    End Using


                Catch ex As Exception
                    explantionlabel.Text = "Problems Requesting New Metric"
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    ModalPopupExtender1.Show()
                    Exit Sub
                End Try

            End If

            Dim metfreq As String

            If ddlMetricFreq.SelectedValue = 0 Then
                metfreq = "declare @MainBody varchar(max) " & _
                "declare @Table varchar(max) " & _
                "set @MainBody = '" & _
                "<html><body><H3> New OHMS Frequency Requested </H3><br><br>' " & _
                "set @Table = '" & _
                "<table border=""1"">" & _
                "<tr><th align=""Right""> User: </th>' + '<td>' + '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' + '</td>' + '" & _
                "</tr><tr><th align=""Right""> New Frequency: </th>' + '<td>' + '" & ReqFreq.Text & "' + '</td>' + '" & _
                "</tr><tr><th alight=""Right""> Metric ID: </th>' + '<td>' + convert(varchar, " & upId & ") + '</td>' + '" & _
                "</tr></table> ' " & _
                "Set @MainBody = @MainBody + @Table + '</body></html>' " & _
                "BEGIN " & _
                "exec msdb.dbo.sp_send_dbmail " & _
                "@recipients = '" & scorecardcontactemail & "' , " & _
                "@subject = 'OHMS -- Frequency Requested', " & _
                "@body = @MainBody, " & _
                "@body_format = 'HTML' " & _
                "End"


                Try


                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        cmd = New System.Data.SqlClient.SqlCommand(metfreq, conn)
                        cmd.ExecuteNonQuery()

                    End Using


                Catch ex As Exception
                    explantionlabel.Text = "Problems Requesting New Metric"
                    LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                    ModalPopupExtender1.Show()
                    Exit Sub
                End Try

            End If


            Try
                If UpdateorNew.SelectedValue = 0 Then
                    TitleSql = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMDefinition]" & _
                        ", [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
                        ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin] ,[SCMUpdateFrequency]" & _
                        ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMUpdated]) Values ((select MAX(SCMID) from KPIS.ScorecardMetric) + 1, '" & _
                        Replace(txtMetricName.Text, "'", "''") & "', '" & Replace(txtMetricObjective.Text, "'", "''") & "', '" & _
                        Replace(txtMetricDefinition.Text, "'", "''") & "', '" & Replace(txtMetricCalculated.Text, "'", "''") & "', '" & _
                        Replace(txtMetricSourceSystem.Text, "'", "''") & "', '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', '" & _
                        Replace(txtMetricLTGoalDate.Text, "'", "''") & "', '" & Replace(txtMetricTarget.Text, "'", "''") & "', '" & _
                        ddlMetricTargetType.SelectedValue & "', '" & rbnewmetcum1.Checked & "', '" & Replace(txtMetricMax.Text, "'", "''") & _
                        "', '" & Replace(txtMetricMin.Text, "'", "''") & "', '" & Replace(txtMetricwMax.Text, "'", "''") & "', '" & _
                        Replace(txtMetricwMin.Text, "'", "''") & "', '" & ddlMetricFreq.SelectedValue & "', '" & _
                        Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & ddlnewMetCat.SelectedValue & "', GetDate(), '" & _
                        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

                ElseIf Not IsDBNull(dr("SCMActive")) Then
                    If dr("SCMActive") = True Then
                        TitleSql = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMDefinition]" & _
                            ", [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
                            ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin] ,[SCMUpdateFrequency]" & _
                            ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMUpdated]) Values (" & ddlMetricUpSelect.SelectedValue & ", '" & _
                            Replace(txtMetricName.Text, "'", "''") & "', '" & Replace(txtMetricObjective.Text, "'", "''") & "', '" & _
                            Replace(txtMetricDefinition.Text, "'", "''") & "', '" & Replace(txtMetricCalculated.Text, "'", "''") & "', '" & _
                            Replace(txtMetricSourceSystem.Text, "'", "''") & "', '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', '" & _
                            Replace(txtMetricLTGoalDate.Text, "'", "''") & "', '" & Replace(txtMetricTarget.Text, "'", "''") & "', '" & _
                            ddlMetricTargetType.SelectedValue & "', '" & rbnewmetcum1.Checked & "', '" & Replace(txtMetricMax.Text, "'", "''") & _
                            "', '" & Replace(txtMetricMin.Text, "'", "''") & "', '" & Replace(txtMetricwMax.Text, "'", "''") & "', '" & _
                            Replace(txtMetricwMin.Text, "'", "''") & "', '" & ddlMetricFreq.SelectedValue & "', '" & _
                            Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & ddlnewMetCat.SelectedValue & "', GetDate(), '" & _
                            Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"
                    Else
                        TitleSql = "Declare @cnt int " & _
                           "set @cnt = (Select COUNT(*) from DWH.KPIS.ScorecardData where ID in (select m1.ID  from DWH.KPIS.ScorecardMetric m1 where SCMID = " & _
                           "(select SCMID from DWH.KPIS.ScorecardMetric m2 where m2.ID = " & ddlMetricUpSelect.SelectedValue & "))) " & _
                           "if @cnt > 0 " & _
                           "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMDefinition]" & _
                            ", [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
                            ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin] ,[SCMUpdateFrequency]" & _
                            ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMUpdated]) Values (" & ddlMetricUpSelect.SelectedValue & ", '" & _
                            Replace(txtMetricName.Text, "'", "''") & "', '" & Replace(txtMetricObjective.Text, "'", "''") & "', '" & _
                            Replace(txtMetricDefinition.Text, "'", "''") & "', '" & Replace(txtMetricCalculated.Text, "'", "''") & "', '" & _
                            Replace(txtMetricSourceSystem.Text, "'", "''") & "', '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', '" & _
                            Replace(txtMetricLTGoalDate.Text, "'", "''") & "', '" & Replace(txtMetricTarget.Text, "'", "''") & "', '" & _
                            ddlMetricTargetType.SelectedValue & "', '" & rbnewmetcum1.Checked & "', '" & Replace(txtMetricMax.Text, "'", "''") & _
                            "', '" & Replace(txtMetricMin.Text, "'", "''") & "', '" & Replace(txtMetricwMax.Text, "'", "''") & "', '" & _
                            Replace(txtMetricwMin.Text, "'", "''") & "', '" & ddlMetricFreq.SelectedValue & "', '" & _
                            Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & ddlnewMetCat.SelectedValue & "', GetDate(), '" & _
                            Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') " & _
                            "else " & _
                            "UPDATE DWH.KPIS.ScorecardMetric set [SCMName] = '" & Replace(txtMetricName.Text, "'", "''") & "', [SCMObjective] = '" & _
                            Replace(txtMetricObjective.Text, "'", "''") & "', [SCMDefinition] = '" & Replace(txtMetricDefinition.Text, "'", "''") & "', " & _
                            "[SCMCalculations] = '" & Replace(txtMetricCalculated.Text, "'", "''") & "', [SCMSourceSystem] = '" & Replace(txtMetricSourceSystem.Text, "'", "''") & _
                            "', [SCMLTTarget] = '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', [SCMLTGoalDate] = '" & Replace(txtMetricLTGoalDate.Text, "'", "''") & _
                            "', [SCMTarget] = '" & Replace(txtMetricTarget.Text, "'", "''") & "', [SCMDataType] = '" & ddlMetricTargetType.SelectedValue & _
                            "', [SCMCumulative] = '" & rbnewmetcum1.Checked & "', [SCMMax] = '" & Replace(txtMetricMax.Text, "'", "''") & "', [SCMMin] = '" & Replace(txtMetricMin.Text, "'", "''") & _
                            "', [SCMwMax] = '" & Replace(txtMetricwMax.Text, "'", "''") & "', [SCMwMin] = '" & Replace(txtMetricwMin.Text, "'", "''") & _
                            "', [SCMUpdateFrequency] = '" & ddlMetricFreq.SelectedValue & _
                            "', [SCMOwner] = '" & Replace(txtMetricOwnerlogin.Text, "'", "''") & "',[SCMCategory] = '" & ddlnewMetCat.SelectedValue & _
                            "', [SCMEffectiveFromDate] = GetDate() ,[SCMUpdated] = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where ID =  " & ddlMetricUpSelect.SelectedValue
                    End If
                Else
                    TitleSql = "Declare @cnt int " & _
                       "set @cnt = (Select COUNT(*) from DWH.KPIS.ScorecardData where ID in (select m1.ID  from DWH.KPIS.ScorecardMetric m1 where SCMID = " & _
                       "(select SCMID from DWH.KPIS.ScorecardMetric m2 where m2.ID = " & ddlMetricUpSelect.SelectedValue & "))) " & _
                       "if @cnt > 0 " & _
                       "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMDefinition]" & _
                        ", [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
                        ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin] ,[SCMUpdateFrequency]" & _
                        ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate] ,[SCMUpdated]) Values (" & ddlMetricUpSelect.SelectedValue & ", '" & _
                        Replace(txtMetricName.Text, "'", "''") & "', '" & Replace(txtMetricObjective.Text, "'", "''") & "', '" & _
                        Replace(txtMetricDefinition.Text, "'", "''") & "', '" & Replace(txtMetricCalculated.Text, "'", "''") & "', '" & _
                        Replace(txtMetricSourceSystem.Text, "'", "''") & "', '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', '" & _
                        Replace(txtMetricLTGoalDate.Text, "'", "''") & "', '" & Replace(txtMetricTarget.Text, "'", "''") & "', '" & _
                        ddlMetricTargetType.SelectedValue & "', '" & rbnewmetcum1.Checked & "', '" & Replace(txtMetricMax.Text, "'", "''") & _
                        "', '" & Replace(txtMetricMin.Text, "'", "''") & "', '" & Replace(txtMetricwMax.Text, "'", "''") & "', '" & _
                        Replace(txtMetricwMin.Text, "'", "''") & "', '" & ddlMetricFreq.SelectedValue & "', '" & _
                        Replace(txtMetricOwnerlogin.Text, "'", "''") & "', '" & ddlnewMetCat.SelectedValue & "', GetDate(), '" & _
                        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "') " & _
                        "else " & _
                        "UPDATE DWH.KPIS.ScorecardMetric set [SCMName] = '" & Replace(txtMetricName.Text, "'", "''") & "', [SCMObjective] = '" & _
                        Replace(txtMetricObjective.Text, "'", "''") & "', [SCMDefinition] = '" & Replace(txtMetricDefinition.Text, "'", "''") & "', " & _
                        "[SCMCalculations] = '" & Replace(txtMetricCalculated.Text, "'", "''") & "', [SCMSourceSystem] = '" & Replace(txtMetricSourceSystem.Text, "'", "''") & _
                        "', [SCMLTTarget] = '" & Replace(txtMetricLTGoal.Text, "'", "''") & "', [SCMLTGoalDate] = '" & Replace(txtMetricLTGoalDate.Text, "'", "''") & _
                        "', [SCMTarget] = '" & Replace(txtMetricTarget.Text, "'", "''") & "', [SCMDataType] = '" & ddlMetricTargetType.SelectedValue & _
                        "', [SCMCumulative] = '" & rbnewmetcum1.Checked & "', [SCMMax] = '" & Replace(txtMetricMax.Text, "'", "''") & "', [SCMMin] = '" & Replace(txtMetricMin.Text, "'", "''") & _
                        "', [SCMwMax] = '" & Replace(txtMetricwMax.Text, "'", "''") & "', [SCMwMin] = '" & Replace(txtMetricwMin.Text, "'", "''") & _
                        "', [SCMUpdateFrequency] = '" & ddlMetricFreq.SelectedValue & _
                        "', [SCMOwner] = '" & Replace(txtMetricOwnerlogin.Text, "'", "''") & "',[SCMCategory] = '" & ddlnewMetCat.SelectedValue & _
                        "', [SCMEffectiveFromDate] = GetDate() ,[SCMUpdated] = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' where ID =  " & ddlMetricUpSelect.SelectedValue
                End If

                Whatisworng.Text = TitleSql

                'Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                '    If conn.State = ConnectionState.Closed Then
                '        conn.Open()
                '    End If

                '    cmd = New System.Data.SqlClient.SqlCommand(TitleSql, conn)
                '    cmd.ExecuteNonQuery()

                'End Using


            Catch ex As Exception
                explantionlabel.Text = "Problems creating Metric"
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
                ModalPopupExtender1.Show()
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

            Dim metsub As String
            Dim createdorupdated As String

            If UpdateorNew.SelectedValue = 0 Then
                createdorupdated = "Created"
            Else
                createdorupdated = "Updated"
            End If

            metsub = "declare @MainBody varchar(max) " & _
            "declare @Table varchar(max) " & _
            "set @MainBody = '" & _
            "<html><body><H3> OHMS Metric Update </H3><br><br>' " & _
            "set @Table = '" & _
            "<table border=""1"">" & _
            "<tr><th align=""Right""> User: </th>' + '<td>' + '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' + '</td>' + '" & _
            "</tr><tr><th align=""Right""> This Metric was : </th>' + '<td>' + '" & createdorupdated & "' + '</td>' + '" & _
            "</tr><tr><th alight=""Right""> Metric ID: </th>' + '<td>' + convert(varchar, " & upId & ") + '</td>' + '" & _
            "</tr></table> ' " & _
            "Set @MainBody = @MainBody + @Table + '</body></html>' " & _
            "BEGIN " & _
            "exec msdb.dbo.sp_send_dbmail " & _
            "@recipients = '" & scorecardcontactemail & "' , " & _
            "@subject = 'OHMS -- Metric " & createdorupdated & "', " & _
            "@body = @MainBody, " & _
            "@body_format = 'HTML' " & _
            "End"


            Try


                Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New System.Data.SqlClient.SqlCommand(metsub, conn)
                    cmd.ExecuteNonQuery()

                End Using


            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try


            secret.Text = 1
            explantionlabel.Text = "Metric Submitted"
            ModalPopupExtender1.Show()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    'Private Sub GridRequestedUpdates_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridRequestedUpdates.RowUpdating



    '    Dim UpdateSql As String = ""
    '    Dim cmd As SqlCommand
    '    Dim ddlmagic As DropDownList
    '    Dim magic As String

    '    ddlmagic = GridRequestedUpdates.Rows(e.RowIndex).FindControl("ddlDataType")
    '    magic = ddlmagic.SelectedValue.ToString


    '    Try

    '        UpdateSql = "Insert INTO DWH.KPIS.ScorecardMetric (SCMID, [SCMName] ,[SCMObjective],[SCMMeasures]" & _
    '  ", SCMDefinition, [SCMCalculations] ,[SCMSourceSystem] ,[SCMLTTarget] ,[SCMLTGoalDate] ,[SCMTarget] ,[SCMDataType]" & _
    '  ",[SCMCumulative], [SCMMax] ,[SCMMin] ,[SCMwMax] ,[SCMwMin], SCMUpdateMethod, SCMUpdateInput, [SCMUpdateFrequency]" & _
    '  ",[SCMOwner] ,[SCMCategory] ,[SCMEffectiveFromDate], SCMEffectiveToDate, [SCMUpdated]) Values (" & _
    '        "(select SCMID FROM DWH.KPIS.ScorecardMetric where ID = " & ddlMetricUpSelect.SelectedValue & "), '" & Replace(e.NewValues.Item(0), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(1), "'", "''") & "', '" & Replace(e.NewValues.Item(2), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(3), "'", "''") & "', '" & Replace(e.NewValues.Item(4), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(5), "'", "''") & "', '" & Replace(e.NewValues.Item(6), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(7), "'", "''") & "', '" & Replace(e.NewValues.Item(8), "'", "''") & "', " & _
    '        "case when " & magic & " <> 'none' then " & magic & " else (Select SCMDataType from DWH.KPIS.ScorecardMetric where ID = " & _
    '        ddlMetricUpSelect.SelectedValue & ") end, '" & _
    '        Replace(e.NewValues.Item(9), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(10), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(11), "'", "''") & "', '" & Replace(e.NewValues.Item(12), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(13), "'", "''") & "', '" & Replace(e.NewValues.Item(14), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(15), "'", "''") & "', '" & Replace(e.NewValues.Item(16), "'", "''") & "', '" & _
    '        Replace(e.NewValues.Item(17), "'", "''") & "', '" & Replace(e.NewValues.Item(18), "'", "''") &
    '         "', GetDate(), '" & Replace(e.NewValues.Item(19), "'", "''") & "', '" & _
    '        Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "')"

    '        UpdateSql = Replace(UpdateSql, "''", "NULL")

    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(UpdateSql, conn)
    '            cmd.ExecuteNonQuery()

    '        End Using


    '    Catch ex As Exception
    '        Exit Sub
    '    End Try

    'End Sub

    Protected Sub btnCNSPreview_Click(sender As Object, e As EventArgs) Handles btnCNSPreview.Click

        Dim SelectList As String = ""

        For Each checkeditem As ListItem In cblCNSMetricSelect.Items
            If (checkeditem.Selected) Then
                SelectList += checkeditem.Value + ", "

            End If
        Next

        'For Each checkeditem As ListItem In cblpublicMetrics.Items
        '    If (checkeditem.Selected) Then
        '        SelectList += checkeditem.Value + ", "

        '    End If
        'Next

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



    Private Sub OkButton_Click(sender As Object, e As System.EventArgs) Handles OkButton.Click

        ModalPopupExtender1.Hide()
        'Page.Validate("MetricRequestForm")
    End Sub



    Private Sub OkButton2_Click(sender As Object, e As System.EventArgs) Handles OkButton2.Click
        ModalPopupExtender2.Hide()
        'Page.Validate("CNSValidation")
    End Sub

    Private Sub ddlnewMetCat_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlnewMetCat.SelectedIndexChanged
        Try
            If ddlnewMetCat.SelectedValue = 0 Then
                CatRequest.Text = "New Category Name"
                CatRequest.DataBind()
                CatRequest.Visible = True
            Else
                CatRequest.Visible = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub UpdateorNew_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles UpdateorNew.SelectedIndexChanged
        Try
            If UpdateorNew.SelectedValue = 1 Then
                ddlMetricUpSelect.Style(HtmlTextWriterStyle.Display) = DropDownList.DisabledCssClass
                lblSelectMetric.Style(HtmlTextWriterStyle.Display) = DropDownList.DisabledCssClass
                'lblSelectMetric.Visible = True
            Else
                'lblSelectMetric.Visible = False
                lblSelectMetric.Style(HtmlTextWriterStyle.Display) = "none"
                ddlMetricUpSelect.Style(HtmlTextWriterStyle.Display) = "none"
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try


    End Sub

    Private Sub ddlMetricUpSelect_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMetricUpSelect.SelectedIndexChanged

        If ddlMetricUpSelect.SelectedValue = "novalueselected" Then
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
            ddlMetricTargetType.SelectedIndex = 2
            rbnewmetcum1.Checked = False
            rbnewmetcum2.Checked = False
            rbincreasing.Checked = False
            rbdecreasing.Checked = False
            ddlnewMetCat.SelectedIndex = 0
            Exit Sub
        End If

        Dim stupidsql As String
        Dim datasetmain As New DataSet
        'Dim dr As DataRow

        stupidsql = "Select * FROM DWH.KPIS.ScorecardMetric where ID = '" & ddlMetricUpSelect.SelectedValue & "'"

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


        'txtMetricName.DataBind()
        'txtMetricCalculated.DataBind()
        'txtMetricDefinition.DataBind()
        'txtMetricLTGoal.DataBind()
        'txtMetricLTGoalDate.DataBind()
        'txtMetricMax.DataBind()
        'txtMetricMin.DataBind()
        'txtMetricObjective.DataBind()
        'txtMetricOwnerlogin.DataBind()
        'txtMetricSourceSystem.DataBind()
        'txtMetricTarget.DataBind()
        'txtMetricwMax.DataBind()
        'txtMetricwMin.DataBind()


    End Sub

    Private Sub ddlMetricFreq_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMetricFreq.SelectedIndexChanged
        Try
            If CInt(ddlMetricFreq.SelectedValue) = 0 Then
                If secret.Text = 1 Then
                    reqfreqdesc.Text = "Desired Number of Times <br /> Measured per Year"
                    'ReqFreqDesc.Text = "Desired Number of Times Measured per Year"
                    'ReqFreq.DataBind()
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
End Class
