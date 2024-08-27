Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class LESCOR
    Inherits System.Web.UI.Page


    Dim LegalPendingApprovalColor As String = "#d5eaff"
    Dim LegalHighPriorityColor As String = "#ff7d7d"
    Dim LegalMediumPriorityColor As String = "#ffbb77"
    Dim LegalLowPriorityColor As String = "#ffffaa"
    Dim LegalRejectedColor As String = "#ffffff"
    Dim LegalClosedColor As String = "#c6BCe7"
    'Dim LegalLoadedColor As String = "#ffcae4"
    Dim LegalLoadedColor As String = "#eedfd7"

    Dim LegalLegalColor As String = "#ffd7ff"
    Dim LegalPreliminaryColor As String = "#c4ffff"
    Dim LegalNegotiationColor As String = "#e4f7ae"

    Dim PendingApprovalColor As String = "#d5eaff"
    Dim PendingLegalApprovalColor As String = "#ffbb77"
    Dim RejectedColor As String = "#ffffff"
    Dim ClosedColor As String = "#c6BCe7"

    Dim ApprovedColor As String = "#bbffbb"
    Dim NoResponseColor As String = "#cccccc"

    'Dim MimicUserLogin As String = "bg31971"

    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff") 'white
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d") 'red
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5efe2") 'brown
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#d5eaff") 'blue
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1") 'light green
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#eeeeee") 'gray
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77") 'orange
    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb") 'green

    ' Need to find a better way to "share" the dataviews without going across Sessions.  Look into this. 8/30/2017 CRW
    ' Also need to push this change out to other pages

    'Private Shared SubmissionView As New DataView
    'Private Shared SearchView As New DataView
    'Private Shared sortmap As String
    'Private Shared sortunmap As String
    'Private Shared mapdir As Integer
    'Private Shared unmapdir As Integer
    'Private Shared Admin As Integer = 0
    'Private Shared searchmap As String
    'Private Shared searchdir As Integer
    'Private Shared Developer As Integer = 0
    'Private Shared AttachIndex As Integer
    'Private Shared Legal As Integer = 0

    'Private Shared ApprovalView As New DataView
    'Private Shared Approvaldir As Integer
    'Private Shared Approvalmap As String

    'Private Shared LegalView As New DataView
    'Private Shared Legalldir As Integer
    'Private Shared Legalmap As String

    'Private Shared DeptView As New DataView
    'Private Shared Deptdir As Integer
    'Private Shared Deptmap As String

    'Private Shared UserView As New DataView
    'Private Shared Userdir As Integer
    'Private Shared Usermap As String

    'Private Shared CheatCheck As String = "ljackson"
    Private Shared WebAdminEmail As String = "chelsea.weirich@northside.com"

    Protected Sub x() Handles Me.LoadComplete
        If IsPostBack Then
        Else
            If GetScalar("select count(*) from WebFD.VendorContracts.Users " & _
         "where Active = 1 and LegalTeam = 1 and UserLogin =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") > 0 Then
                'Try
                '    ddlLegalAssignedTo.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
                'Catch ex As Exception
                'End Try
            Else
                FlipCBVis(False, False)
                'pnlLegalFields.Visible = False
                'Try
                '    'ddlLegalRequestor.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
                'Catch ex As Exception

                'End Try
            End If
            'Search()


        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnUpload)
                If tpSubmitContract.Visible Then
                    'ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnUpload)
                    AttachmentDownloadRowing(gvSubmissionAttachments)
                End If

                If tpLegalTab.Visible Then
                    LegalRowing()
                    AttachmentDownloadRowing(gvLegalAttachments)
                End If

                If tpPendingContracts.Visible Then
                    AttachmentDownloadRowing(gvApprovalAttachments)
                End If

                If SubmissionView().Count = 0 Then
                    gvSubmissionQuestions.Visible = False
                Else
                    gvSubmissionQuestions.Visible = True
                End If

                'If SearchView.Count = 0 Then
                '    gvSearchedContracts.Visible = False
                'Else
                '    gvSearchedContracts.Visible = True
                '    Rowing()
                'End If

            Else

                CheckPermissions()

                If Legal.Text = "1" Then
                    cbLegalShowPending.Checked = False
                    cbLegalShowRejected.Checked = False
                    cbLegalShowClosed.Checked = False
                    cbLegalShowQueueH.Checked = True
                    cbLegalShowQueueL.Checked = True
                    cbLegalShowQueueM.Checked = True
                    cbLegalLoadedinMeditract.Checked = False
                    cbLegalNoResponse.Checked = False

                    cbLegalLegal.Checked = True
                    cbLegalNegotiation.Checked = True
                    cbLegalPreliminary.Checked = True

                    cbLegalShowClosed.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalClosedColor)
                    cbLegalShowPending.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPendingApprovalColor)
                    cbLegalShowQueueH.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalHighPriorityColor)
                    cbLegalShowQueueM.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalMediumPriorityColor)
                    cbLegalShowQueueL.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLowPriorityColor)
                    cbLegalShowRejected.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor)
                    cbLegalShowApproved.BackColor = System.Drawing.ColorTranslator.FromHtml(ApprovedColor)
                    cbLegalLoadedinMeditract.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLoadedColor)
                    cbLegalNoResponse.BackColor = System.Drawing.ColorTranslator.FromHtml(NoResponseColor)

                    cbLegalLegal.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLegalColor)
                    cbLegalNegotiation.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalNegotiationColor)
                    cbLegalPreliminary.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPreliminaryColor)

                Else
                    cbLegalShowPending.Checked = True
                    cbLegalShowQueue.Checked = True
                    cbLegalShowRejected.Checked = False
                    cbLegalShowClosed.Checked = False
                    cbLegalShowClosed.BackColor = System.Drawing.ColorTranslator.FromHtml(ClosedColor)
                    cbLegalShowPending.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor)
                    cbLegalShowQueue.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                    cbLegalShowRejected.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                    cbLegalShowApproved.BackColor = System.Drawing.ColorTranslator.FromHtml(ApprovedColor)

                End If

                PopulateSignaturesGrid()

                LoadExpenseChoices()
                ResetPage()
                txtSrchSubStart.Text = DateAdd(DateInterval.Month, -1, Today())
                txtSrchSubEnd.Text = Today()
                'SrchGrid()
                Search()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Shared Function GetData(query As String) As DataTable

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                Using sda As New SqlDataAdapter()

                    cmd.Connection = con

                    sda.SelectCommand = cmd

                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Using ds As New DataSet()

                        Dim dt As New DataTable()

                        sda.Fill(dt)

                        Return dt

                    End Using

                End Using

            End Using

        End Using

    End Function
    Private Sub ExecuteSql(query As String)

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                Using sda As New SqlDataAdapter()

                    cmd.Connection = con

                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        End Using

    End Sub
    Private Shared Function GetScalar(query As String) As Integer

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As Integer

                dt = cmd.ExecuteScalar

                Return dt

            End Using

        End Using


    End Function
    Private Shared Function GetString(query As String) As String

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As String

                dt = cmd.ExecuteScalar

                Return dt

            End Using

        End Using


    End Function

    'Private Sub PopulateGrantAccess()

    '    Try
    '        Dim sql As String = " select '(Select Role)' as RoleFull, 0 as ord union all " & _
    '    " select RoleFull, 1 from WebFD.VendorContracts.Roles where Active = 1 order by ord, RoleFull"

    '        ddlGrantPosition.DataSource = GetData(sql)
    '        ddlGrantPosition.DataTextField = "RoleFull"
    '        ddlGrantPosition.DataValueField = "RoleFull"
    '        ddlGrantPosition.DataBind()

    '        Dim sql2 As String = " select '(Select Department)' as Deps, 0 as ord union all  select '(All Departments)' as RoleFull, 1 as ord union all " & _
    '   "select Convert(varchar, DepartmentNo) + ' - ' + isnull(DepartmentDisplayName, DepartmentName) , 2 " & _
    '   "from WebFD.VendorContracts.Department_LU where Active = 1 "

    '        ddlGrantDepartment.DataSource = GetData(sql2)
    '        ddlGrantDepartment.DataTextField = "Deps"
    '        ddlGrantDepartment.DataValueField = "Deps"
    '        ddlGrantDepartment.DataBind()
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub CheckPermissions()
        Try

            Dim x As String = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
            If Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") = "" Then
                lblOpenUserLogin.Text = "You are not logged in.  Please enter your credentials at the top to proceed."
            Else
                lblOpenUserLogin.Text = "Welcome, " & GetString("select  isnull(UserDisplayName, UserFullName) from WebFD.VendorContracts.Users where Active = 1 and UserLogin = '" & _
                                                  Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") & _
                                              ", to the Legal Services Contract Review System ""LESCOR"" "
            End If


            lblEmailHierarchyContact.Text = GetString("Select Value from WebFD.VendorContracts.SpecialValues where Reason = 'Hierarchy Email'")
            lblCostCenterEmail.Text = GetString("Select Value from WebFD.VendorContracts.SpecialValues where Reason = 'Cost Center Email'")
            lblLegalPhoneContact.Text = GetString("Select Value from WebFD.VendorContracts.SpecialValues where Reason = 'Legal Phone'")
            cbAgreements.Text = "I have read and agree to the Contract Management Policy located <a target=""_blank"" href='" & GetString("Select Value from WebFD.VendorContracts.SpecialValues where Active = 1 and Reason = 'Policy Link'") & "'>here</a>."

            If GetScalar("select count(*) from WebFD.dbo.aspnet_Users u " & _
                "join WebFD.dbo.aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
                "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
                "where r.RoleName = 'Developer' and UserName = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") > 0 Then
                Developer.Text = "1"
                tpAdministrative.Visible = True
                PopulateDeptGV()
                'PopulateGrantAccess()
            End If

            If GetScalar("select count(*) from WebFD.VendorContracts.Users " & _
                    "where Active = 1 and Admin = 1 and UserLogin =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") > 0 Then
                Admin.Text = "1"
                tpAdministrative.Visible = True
                PopulateDeptGV()
                'PopulateGrantAccess()
            End If

            'tpLegalTab.Visible = True
            PopulateDDLLegalAssignedTo()

            Dim sql As String = " select '(View All)' as UserName, '-1' as UserLogin, 0 as ord union all " & _
" select distinct isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin), ch.RequesterUserLogin, 1 " & _
"from WebFD.VendorContracts.ContractHeader ch " & _
"left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin and u.Active = 1 " & _
"where ch.Active = 1 " & _
" and ch.DepartmentID in (select case when u.LegalTeam = 1 then ch.DepartmentID else isnull(d2u.DepartmentID, ch.DepartmentID) end " & _
"	from WebFD.VendorContracts.Users u  " & _
"    left join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1  " & _
"    left join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1  " & _
"    where u.Active = 1 and u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "') order by ord, UserName"

            ddlLegalRequestor.DataSource = GetData(sql)
            ddlLegalRequestor.DataTextField = "UserName"
            ddlLegalRequestor.DataValueField = "UserLogin"
            ddlLegalRequestor.DataBind()

            If GetScalar("select count(*) from WebFD.VendorContracts.Users " & _
            "where Active = 1 and LegalTeam = 1 and UserLogin =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") > 0 Then
                Legal.Text = "1"
                'tpPendingContracts.Visible = True
                tpLegalTab.Visible = True
                cbAgreements.Checked = True
                LoadWaitingYourApprovalGrid()
                LoadRecentlyReviewedGrid()
                'pnlLegalFields.Visible = True
                FlipCBVis(True, False)
                'pnlLegalUpdates.Visible = True

                cbLegalShowQueueH.Visible = True
                cbLegalShowQueueM.Visible = True
                cbLegalShowQueueL.Visible = True
                cbLegalShowQueue.Visible = False
                cbLegalLoadedinMeditract.Visible = True
                cbLegalNoResponse.Visible = True
                cbLegalShowRejected.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                cbLegalShowPending.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5efe2")
                cbLegalShowClosed.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1")
                cbLegalLoadedinMeditract.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLoadedColor)
                cbLegalNoResponse.BackColor = System.Drawing.ColorTranslator.FromHtml(NoResponseColor)

                tcLegalOptions.Visible = True
                cbLegalLegal.Visible = True
                cbLegalNegotiation.Visible = True
                cbLegalPreliminary.Visible = True


                cbLegalLegal.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLegalColor)
                cbLegalNegotiation.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalNegotiationColor)
                cbLegalPreliminary.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPreliminaryColor)

                'Try
                '    ddlLegalAssignedTo.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
                'Catch ex As Exception
                'End Try
            Else
                FlipCBVis(False, False)
                'pnlLegalFields.Visible = False

                'Removed Default after first beta test CRW 8/14/2017
                'Try
                '    ddlLegalRequestor.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
                'Catch ex As Exception

                'End Try
            End If
            'tpSubmitContract.Visible = True



            'Search()

            Dim sql2 As String

            If Legal.Text = "1" Then
                sql2 = " select '(View All Available)' as CC, -1 as DepartmentID, 0 as ord union all " & _
                            " select distinct convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) as CC, ch.DepartmentID, 1  " & _
                            "from WebFD.VendorContracts.ContractHeader ch " & _
                            "left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
                            "where ch.Active = 1 order by ord, CC "
            Else
                sql2 = " select '(View All Available)' as CC, -1 as DepartmentID, 0 as ord union all " & _
                 " select  distinct convert(varchar, DepartmentNo) + ' - ' + DepartmentName as DepartmentSelected, d.DepartmentID, 1   " & _
                 "					from WebFD.VendorContracts.Users u " & _
                 "					left join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
                 "					left join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1  " & _
                 "					where u.Active = 1 and u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
                 "'  and (d2u.DepartmentID is null or d.DepartmentID is not null) order by 3, 1"

            End If


            ddlLegalContractCostCenter.DataSource = GetData(sql2)
            ddlLegalContractCostCenter.DataTextField = "CC"
            ddlLegalContractCostCenter.DataValueField = "DepartmentID"
            ddlLegalContractCostCenter.DataBind()

            'Dim sql3 As String = " select '(Select Annual Cost)' as CC, 0 as ord union all " & _
            '" select distinct AnnualContractExpense,  1   " & _
            '"from WebFD.VendorContracts.ContractHeader ch " & _
            '"where ch.Active = 1 order by ord, CC "

            'ddlLegalContractCost.DataSource = GetData(sql3)
            'ddlLegalContractCost.DataTextField = "CC"
            'ddlLegalContractCost.DataValueField = "CC"
            'ddlLegalContractCost.DataBind()

            'Dim sql4 As String = " select '(Select Question)' as Question, -1 as QuestionID, 0 as ord union all " & _
            '" select distinct q.Question, q.QuestionID, 1 from WebFD.VendorContracts.Contract_Answers ca   " & _
            '"join WebFD.VendorContracts.Question_LU q on ca.QuestionID = q.QuestionID " & _
            '"where ca.Active = 1 order by ord, Question "

            'ddlLegalQuestionSearch.DataSource = GetData(sql4)
            'ddlLegalQuestionSearch.DataTextField = "Question"
            'ddlLegalQuestionSearch.DataValueField = "QuestionID"
            'ddlLegalQuestionSearch.DataBind()

            'Dim sql5 As String = " select '(Select Question)' as Question, -1 as QuestionID, 0 as ord  "

            'ddlLegalQuestionResponse.DataSource = GetData(sql5)
            'ddlLegalQuestionResponse.DataTextField = "Question"
            'ddlLegalQuestionResponse.DataValueField = "QuestionID"
            'ddlLegalQuestionResponse.DataBind()




            Dim s As String = "Select isnull('<b>'+Main.Position + '</b><br>' + " & _
                "       Replace(Main.Labelling, ' ~ CRWBREAKLINE ~ ', '<br>'), '') As Labelling " & _
                "From " & _
                "    ( " & _
                "        Select distinct ST2.Position, ST2.UserLogin, ST2.UserDisplayName , " & _
                "            ( " & _
                "                Select ST1.Show AS [text()] " & _
                "                From ( " & _
                "					select  isnull(convert(Varchar, DepartmentNo)  + ' - ' +  DepartmentName, 'All Departments') + ' ~ CRWBREAKLINE ~ ' as Show, u.UserLogin, d2u.Position   " & _
                "					from WebFD.VendorContracts.Users u " & _
                "					left join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
                "					left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
                "					where u.Active = 1  and (d2u.DepartmentID is null or d.DepartmentID is not null) ) ST1 " & _
                "               Where ST1.UserLogin = ST2.UserLogin and ST1.Position = ST2.Position " & _
                "                ORDER BY ST1.Show " & _
                "                For XML PATH ('') " & _
                "            ) Labelling " & _
                "        From ( " & _
                "					select  u.UserLogin, d2u.Position, isnulL(u.UserDisplayName  , u.UserFullName) as UserDisplayName " & _
                "					from WebFD.VendorContracts.Users u " & _
                "					left join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
                "					left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
                "					where u.Active = 1  and (d2u.DepartmentID is null or d.DepartmentID is not null) ) ST2 " & _
                "   ) Main " & _
                "	where UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' "

            'lblOpenSummary.Text = ""
            'For Each row As DataRow In GetData(s).Rows
            '    lblOpenSummary.Text += row(0).ToString + "<br>"
            'Next

            ' If Len(lblOpenSummary.Text) > 0 Then
            If GetData(s).Rows.Count > 0 Then
                'lblOpenSummary.Text = "Your hierarchy level: <br>" + lblOpenSummary.Text
                'tpSubmitContract.Visible = True
                cbAgreements.Visible = True
                PopulateDepartmentDDL()
                PopulateContractPartyDDL()
                PopulateExpenseAcctDDL()
            Else
                lblOpenSummary.Text = "You do not have access to submit contracts at any Cost Centers"
                cbAgreements.Visible = False
                'tpSubmitContract.Visible = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PopulateHierarchiesGrid()
        Dim y As String = "				select RoleMatch, left(Reqs, len(Reqs) - 1) as Users, Hierarchy from ( " & _
            "				Select distinct r.RoleFull as RoleMatch , Hierarchy , " & _
            "                        (  " & _
            "                            Select UserName + ', ' AS [text()]  " & _
            "                            from (select isnull(isnull(u.UserDisplayName, u.UserFullName), d2u.UserLogin) as UserName, r.RoleFull  " & _
            "				from WebFD.VendorContracts.Roles r " & _
            "				join WebFD.VendorContracts.Department_2_User d2u on r.RoleShort = d2u.Position and d2u.Active = 1 " & _
            "				join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            "				join WebFD.VendorContracts.Users u on d2u.UserLogin = u.UserLogin and u.Active = 1 " & _
            "				where d.DepartmentID = '" & Replace(ddlDepartmentNo.SelectedValue, "'", "''") & "') ST1    " & _
            "				where (ST1.RoleFull = r.RoleFull) " & _
             "                            " & _
             "                           For XML PATH ('')  " & _
             "                      ) Reqs  " & _
             "                   From WebFD.VendorContracts.Roles r   " & _
             "           		where r.Active = 1 ) x " & _
            "					where Reqs is not null " & _
            "					order by Hierarchy "


        gvHierarchies.DataSource = GetData(y)
        gvHierarchies.DataBind()
    End Sub

    Private Sub PopulateSignaturesGrid()

        'Dim x As String =
        '    "Select distinct LowerBound, UpperBound,  case when LowerBound is null then 'Less Than $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3) " & _
        '    "            	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)  " & _
        '    "            	else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'   " & _
        '    "            	+ left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)  " & _
        '    "            	end as ImpactRange , " & _
        '    "    substring( " & _
        '    "        ( " & _
        '    "            Select ' and '+X1.ApprovalRequired  AS [text()] " & _
        '    "            From ( " & _
        '    "			 " & _
        '    "select ST2.LowerBound, ST2.UpperBound, ApprovalRequired, r2.Hierarchy " & _
        '    "From WebFD.VendorContracts.ApprovalReq_LU  ST2 " & _
        '    "		left join WebFD.VendorContracts.Roles r on r.Alias like ST2.ApprovalRequired and r.Active = 1 " & _
        '    "       join WebFD.VendorContracts.Roles r2 on r2.RoleFull = isnull(r.RoleFull, ST2.ApprovalRequired) and r2.Active = 1 " & _
        '    "		where r.Alias is null and ST2.Active = 1 " & _
        '    "union  " & _
        '    "Select Main.LowerBound, Main.UpperBound, " & _
        '    "       '(' + Left(Main.Reqs,Len(Main.Reqs)-3) + ')' As Reqs, Main.hierarchy " & _
        '    "From " & _
        '    "    ( " & _
        '    "        Select distinct ST2.LowerBound, ST2.UpperBound,  r.Hierarchy,  " & _
        '    "            ( " & _
        '    "                Select r.RoleFull + ' or ' AS [text()] " & _
        '    "                from WebFD.VendorContracts.ApprovalReq_LU aq " & _
        '    "				join WebFD.VendorContracts.Roles r on r.Alias like aq.ApprovalRequired and r.Active = 1 " & _
        '    "				where aq.Active = 1 " & _
        '    "                and aq.ApprovalRequired = ST2.ApprovalRequired " & _
        '    "				and isnull(aq.LowerBound, -999999999) = isnull(ST2.LowerBound, -999999999) " & _
        '    "				and isnull(aq.UpperBound, -999999999) = isnull(ST2.UpperBound, -999999999) " & _
        '    "                ORDER BY r.RoleFull " & _
        '    "                For XML PATH ('') " & _
        '    "            ) Reqs " & _
        '    "        From WebFD.VendorContracts.ApprovalReq_LU  ST2 " & _
        '    "		join WebFD.VendorContracts.Roles r on r.Alias like ST2.ApprovalRequired and r.Active = 1 " & _
        '    "		where r.Alias is not null and ST2.Active = 1 " & _
        '    "    ) [Main] " & _
        '    " " & _
        '    "	) x1 " & _
        '    "            Where isnull(X1.LowerBound, -999999999) = isnull(X2.LowerBound, -999999999) " & _
        '    "				and isnull(X1.UpperBound, -999999999) = isnull(X2.UpperBound, -999999999) " & _
        '    "            ORDER BY X1.Hierarchy, X1.ApprovalRequired  " & _
        '    "            For XML PATH ('') " & _
        '    "        ), 6, 1000) [RequiredSignatures] " & _
        '    "From ( " & _
        '    " " & _
        '    "select ST2.LowerBound, ST2.UpperBound, ApprovalRequired " & _
        '    "From WebFD.VendorContracts.ApprovalReq_LU  ST2 " & _
        '    "		left join WebFD.VendorContracts.Roles r on r.Alias like ST2.ApprovalRequired and r.Active = 1 " & _
        '    "		where r.Alias is null and ST2.Active = 1 " & _
        '    "union  " & _
        '    "Select Main.LowerBound, Main.UpperBound, " & _
        '    "       '(' + Left(Main.Reqs,Len(Main.Reqs)-3) + ')' As Reqs " & _
        '    "From " & _
        '    "    ( " & _
        '    "        Select distinct ST2.LowerBound, ST2.UpperBound,  " & _
        '    "            ( " & _
        '    "                Select r.RoleFull + ' or ' AS [text()] " & _
        '    "                from WebFD.VendorContracts.ApprovalReq_LU aq " & _
        '    "				join WebFD.VendorContracts.Roles r on r.Alias like aq.ApprovalRequired and r.Active = 1 " & _
        '    "				where aq.Active = 1 " & _
        '    "                and aq.ApprovalRequired = ST2.ApprovalRequired " & _
        '    "				and isnull(aq.LowerBound, -999999999) = isnull(ST2.LowerBound, -999999999) " & _
        '    "				and isnull(aq.UpperBound, -999999999) = isnull(ST2.UpperBound, -999999999) " & _
        '    "                ORDER BY r.RoleFull " & _
        '    "                For XML PATH ('') " & _
        '    "            ) Reqs " & _
        '    "        From WebFD.VendorContracts.ApprovalReq_LU  ST2 " & _
        '    "		join WebFD.VendorContracts.Roles r on r.Alias like ST2.ApprovalRequired and r.Active = 1 " & _
        '    "		where r.Alias is not null and ST2.Active = 1 " & _
        '    "    ) [Main] " & _
        '    " " & _
        '    "	) X2 " & _
        '    " " & _
        '    "	order by LowerBound "


        Dim x As String = "Select distinct LowerBound, UpperBound,  case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)  " & _
         "                       	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)   " & _
         "                       	else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'    " & _
         "                       	+ left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)   " & _
         "                       	end as ImpactRange , ApprovalRequired  " & _
         "                into #TempBounds " & _
         "           From (  " & _
         "             " & _
         "           select ST2.LowerBound, ST2.UpperBound, ApprovalRequired  " & _
         "           From WebFD.VendorContracts.ApprovalReq_LU  ST2  " & _
         "           		left join WebFD.VendorContracts.Roles r on r.Alias like ST2.ApprovalRequired and r.Active = 1  " & _
         "          		where r.Alias is null and ST2.Active = 1  " & _
         "           union   " & _
         "           Select Main.LowerBound, Main.UpperBound,  " & _
         "                  '' + Left(Main.Reqs,Len(Main.Reqs)-3) + '' As Reqs  " & _
         "           From  " & _
         "               (  " & _
         "                   Select distinct ST2.LowerBound, ST2.UpperBound,   " & _
         "                       (  " & _
         "                           Select r.RoleFull + ' or ' AS [text()]  " & _
         "                           from WebFD.VendorContracts.ApprovalReq_LU aq  " & _
         "           				join WebFD.VendorContracts.Roles r on r.Alias like aq.ApprovalRequired and r.Active = 1  " & _
         "           				where aq.Active = 1  " & _
         "                           and aq.ApprovalRequired = ST2.ApprovalRequired  " & _
         "           				and isnull(aq.LowerBound, -999999999) = isnull(ST2.LowerBound, -999999999)  " & _
         "           				and isnull(aq.UpperBound, -999999999) = isnull(ST2.UpperBound, -999999999)  " & _
         "                           ORDER BY r.RoleFull  " & _
         "                           For XML PATH ('')  " & _
         "                       ) Reqs  " & _
        "						 " & _
        "                    From WebFD.VendorContracts.ApprovalReq_LU  ST2  " & _
        "            		join WebFD.VendorContracts.Roles r on r.Alias like ST2.ApprovalRequired and r.Active = 1  " & _
        "            		where r.Alias is not null and ST2.Active = 1  " & _
        "                ) [Main]  " & _
        "              " & _
        "            	) X2  " & _
        "              " & _
        "            	order by LowerBound  " & _
        "Select distinct LowerBound, UpperBound, ST2.ImpactRange, " & _
        "    substring( " & _
        "        ( " & _
        "            Select ' and '+ST1.ApprovalRequired  AS [text()] " & _
        "            From #TempBounds ST1 " & _
        "            Where ST1.ImpactRange = ST2.ImpactRange " & _
        "            ORDER BY ST1.ImpactRange " & _
        "            For XML PATH ('') " & _
        "        ), 6, 1000) ApprovalRequired " & _
        "From #TempBounds ST2 " & _
        "order by LowerBound "



        gvSubmissionRequirements.DataSource = GetData(x)
        gvSubmissionRequirements.DataBind()



    End Sub

    Private Sub PopulateDepartmentDDL()

        Dim sql As String = " select '(Select Cost Center)' as DepartmentSelected, -1 as DepartmentID, 0 as ord union all " & _
            " select  distinct convert(varchar, DepartmentNo) + ' - ' + DepartmentName as DepartmentSelected, d.DepartmentID, 1   " & _
            "					from WebFD.VendorContracts.Users u " & _
            "					left join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
            "					left join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1 " & _
            "					where u.Active = 1 and u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'  and (d2u.DepartmentID is null or d.DepartmentID is not null) order by 3, 1"

        ddlDepartmentNo.DataSource = GetData(sql)
        ddlDepartmentNo.DataTextField = "DepartmentSelected"
        ddlDepartmentNo.DataValueField = "DepartmentID"
        ddlDepartmentNo.DataBind()

    End Sub

    Private Sub PopulateContractPartyDDL()

        Dim sql As String = "select '(Select Contract Party)' as ContractParty, -1 as ContractPartyID, 0 as ord union all " & _
            "select ContractParty, ContractPartyID, case when ContractPartyID = 3 then 2 else 1 end from WebFD.VendorContracts.ContractParty_LU " & _
            "where Active = 1 " & _
            "order by ord, 1 "

        ddlSubmitContractParty.DataSource = GetData(sql)
        ddlSubmitContractParty.DataTextField = "ContractParty"
        ddlSubmitContractParty.DataValueField = "ContractPartyID"
        ddlSubmitContractParty.DataBind()

    End Sub

    Private Sub PopulateExpenseAcctDDL()

        Dim sql As String = "select -9999999 as Acct, '(Select Expense Account)' as Descrip, 0 as ord union " & _
            "select -1, '(Unknown)', 0 union " & _
            "select Acct, convert(varchar, Acct) + ' - ' + Description as Descrip, 1 from DWH.AXIOM.ACCT " & _
            "where ISMAP in ('SUP', 'FEE', 'OTH') " & _
            "order by 3, 1, 2 "


        ddlSubmitContractBudgetAcct.DataSource = GetData(sql)
        ddlSubmitContractBudgetAcct.DataTextField = "Descrip"
        ddlSubmitContractBudgetAcct.DataValueField = "Acct"
        ddlSubmitContractBudgetAcct.DataBind()

    End Sub

    Private Function SubmissionView()
        Return GetData("select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
            "	   from ( " & _
            "select Dense_Rank() over (order by q.QuestionID asc " & _
            "	)   as RN " & _
            "	, Question " & _
            "	,q.QuestionID " & _
            "	, q.Method " & _
            "from WebFD.VendorContracts.Question_LU q where q.Active = 1 " & _
            " and isnull(SummaryQuestion, 0) = 0) N " & _
            "order by RN ").DefaultView
    End Function

    Private Sub loadgrid()

        'Dim sandwich As Integer = lblContractID.Text

        'Dim gridsql As String =

        Dim gridsql2 As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
           "	   from ( " & _
           "select Dense_Rank() over (order by q.QuestionID asc " & _
           "	)   as RN " & _
           "	, Question " & _
           "	,q.QuestionID " & _
           "	, q.Method " & _
           "from WebFD.VendorContracts.Question_LU q where q.Active = 1 " & _
           " and isnull(SummaryQuestion, 0) = 1) N " & _
           "order by RN "

        '   7/10/2017 CRW -- Removing Pending Tables setup
        '    Dim gridsql As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
        '"	   from ( " & _
        '"select Dense_Rank() over (order by q.QuestionID asc " & _
        '"	)   as RN " & _
        '"	, Question " & _
        '"	, ca.Response " & _
        '"	, c.ContractID, q.QuestionID " & _
        '"	, q.Method " & _
        '"from WebFD.VendorContracts.PendingContractHeader c " & _
        '"join WebFD.VendorContracts.ContractType_LU ctl on c.ContractTypeID = ctl.ContractTypeID and ctl.Active = 1 " & _
        '"left join WebFD.VendorContracts.Question_LU q on q.Active = 1 " & _
        '"left join WebFD.VendorContracts.PendingContract_Answers ca on q.QuestionID = ca.QuestionID and ca.ContractID = c.ContractID and ca.Active = 1 " & _
        '"where c.ContractID = " & sandwich & " " & _
        '") N " & _
        '"order by RN "

        ' 7/7/2017 CRW -- Removed Contracts_2_Questions table/requirement; rewrote query accordingly
        ' This Also got rid of Parent Questions -- left initial query here if rewrite is needed.

        '    Dim gridsql As String = "select *, convert(varchar, RN) + case when ParentQuestionID is null then '' else  " & _
        '"	Coalesce((SELECT Char(65 + (N.Num - 475255) / 456976 % 26) WHERE N.Num >= 475255), '') " & _
        '"      + Coalesce((SELECT Char(65 + (N.Num - 18279) / 17576 % 26) WHERE N.Num >= 18279), '') " & _
        '"      + Coalesce((SELECT Char(65 + (N.Num - 703) / 676 % 26) WHERE N.Num >= 703), '') " & _
        '"      + Coalesce((SELECT Char(65 + (N.Num - 27) / 26 % 26) WHERE N.Num >= 27), '') " & _
        '"      + (SELECT Char(65 + (N.Num - 1) % 26)) end + '. ' as DisplayingNo	 " & _
        '"	   from ( " & _
        '"select Dense_Rank() over (order by isnull(ParentQuestionID, ctq.QuestionID) asc " & _
        '"	)   as RN " & _
        '"	,Row_Number() over (partition by isnull(ParentQuestionID, -ctq.QuestionID) order by  " & _
        '"	case when ParentQuestionID is null then 0 else 1 end)    as Num " & _
        '"	, Question " & _
        '"	, ca.Response " & _
        '"	, c.ContractID, q.QuestionID " & _
        '"	, q.Method " & _
        '"	, ParentQuestionID " & _
        '"from WebFD.VendorContracts.PendingContractHeader c " & _
        '"join WebFD.VendorContracts.ContractType_LU ctl on c.ContractTypeID = ctl.ContractTypeID and ctl.Active = 1 " & _
        '"left join WebFD.VendorContracts.Contracts_2_Questions ctq on ctl.ContractTypeID = ctq.ContractTypeID and ctq.Active = 1 " & _
        '"left join WebFD.VendorContracts.Question_LU q on ctq.QuestionID = q.QuestionID and q.Active = 1 " & _
        '"left join WebFD.VendorContracts.PendingContract_Answers ca on ctq.QuestionID = ca.QuestionID and ca.ContractID = c.ContractID and ca.Active = 1 " & _
        '"left join WebFD.VendorContracts.PendingContract_Answers cap on ParentQuestionID = cap.QuestionID and c.ContractID = cap.ContractID " & _
        '"	and ( (  QualificationOperator = '=' and cap.Response = Qualification ) " & _
        '"	or (  QualificationOperator = '>' and cap.Response > Qualification ) " & _
        '"	or (  QualificationOperator = '<' and cap.Response < Qualification ) " & _
        '"	)	and cap.Active = 1 " & _
        '"where c.ContractID = " & sandwich & " and (ParentQuestionID is null or cap.Active = 1) " & _
        '") N " & _
        '"order by RN, Num "



        'SubmissionView = GetData(gridsql).DefaultView
        gvSubmissionQuestions.DataSource = SubmissionView()
        gvSubmissionQuestions.DataBind()
        gvSubmissionQuestions.ShowHeaderWhenEmpty = True

        gvSpecialQuestionSubmission.DataSource = GetData(gridsql2).DefaultView
        gvSpecialQuestionSubmission.DataBind()

        If SubmissionView.Count = 0 Then
            gvSubmissionQuestions.Visible = False
        Else
            gvSubmissionQuestions.Visible = True
            CheckResponses(gvSpecialQuestionSubmission)
            CheckResponses(gvSubmissionQuestions)
        End If


    End Sub

    Private Sub loadprintgrid()

        Dim gridsql As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
"           	   from ( " & _
"           select Dense_Rank() over (order by q.QuestionID asc " & _
"           	)   as RN " & _
"           	, Question " & _
"           	,q.QuestionID " & _
"           	, q.Method " & _
"			, pc.Response, pc.AnswerComment, pc.AnswerDate, case when pc.AnswerComment is null then 'false' else 'true' end ViewComment " & _
"           from WebFD.VendorContracts.Question_LU q " & _
"			left join WebFD.VendorContracts.PendingContract_Answers pc on q.QuestionID = pc.QuestionID and pc.Active = 1 " & _
"			where q.Active = 1 and pc.ModifiedBy = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
            "' and pc.ContractID = '" & Replace(AttachIndex.Text.ToString, "'", "''") & "' and isnull(SummaryQuestion, 0) = 0) N " & _
"          order by RN "

        Dim gridsql2 As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
"           	   from ( " & _
"           select Dense_Rank() over (order by q.QuestionID asc " & _
"           	)   as RN " & _
"           	, Question " & _
"           	,q.QuestionID " & _
"           	, q.Method " & _
"			, pc.Response, pc.AnswerComment, pc.AnswerDate, case when pc.AnswerComment is null and pc.AnswerDate is null then 'false' else 'true' end ViewComment  " & _
"           from WebFD.VendorContracts.Question_LU q " & _
"			left join WebFD.VendorContracts.PendingContract_Answers pc on q.QuestionID = pc.QuestionID and pc.Active = 1 " & _
"			where q.Active = 1 and pc.ModifiedBy = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
            "' and pc.ContractID = '" & Replace(AttachIndex.Text.ToString, "'", "''") & "' and isnull(SummaryQuestion, 0) = 1) N " & _
"          order by RN "



        gvSubmissionQuestionsPrintScreen.DataSource = GetData(gridsql).DefaultView
        gvSubmissionQuestionsPrintScreen.DataBind()

        gvSpecialQuestionsPrintScreen.DataSource = GetData(gridsql2).DefaultView
        gvSpecialQuestionsPrintScreen.DataBind()

    End Sub

    Private Sub loadApprovalgrid(x As Integer)

        Dim gridsql As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
"           	   from ( " & _
"           select Dense_Rank() over (order by q.QuestionID asc " & _
"           	)   as RN " & _
"           	, Question " & _
"           	,q.QuestionID " & _
"           	, q.Method " & _
"			, pc.Response, pc.AnswerComment, pc.AnswerDate, case when pc.AnswerComment is null then 'false' else 'true' end ViewComment " & _
"           from WebFD.VendorContracts.Question_LU q " & _
"			join WebFD.VendorContracts.Contract_Answers pc on q.QuestionID = pc.QuestionID and pc.Active = 1 " & _
"			where pc.ContractID = '" & x.ToString & "' and isnull(SummaryQuestion, 0) = 0) N " & _
"          order by RN "
        ' 10/12/2017 CRW Revised join instead of left join and to remove the q.Active = 1 so that all contracts can be viewed as submitted

        Dim gridsql2 As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
"           	   from ( " & _
"           select Dense_Rank() over (order by q.QuestionID asc " & _
"           	)   as RN " & _
"           	, Question " & _
"           	,q.QuestionID " & _
"           	, q.Method " & _
"			, pc.Response, pc.AnswerComment, pc.AnswerDate, case when pc.AnswerComment is null then 'false' else 'true' end ViewComment  " & _
"           from WebFD.VendorContracts.Question_LU q " & _
"			join WebFD.VendorContracts.Contract_Answers pc on q.QuestionID = pc.QuestionID and pc.Active = 1 " & _
"			where pc.ContractID = '" & x.ToString & "' and isnull(SummaryQuestion, 0) = 1) N " & _
"          order by RN "



        gvApproveQuestions1.DataSource = GetData(gridsql).DefaultView
        gvApproveQuestions1.DataBind()

        gvApproveQuestions2.DataSource = GetData(gridsql2).DefaultView
        gvApproveQuestions2.DataBind()

    End Sub

    Private Sub loadLegalgrid(x As Integer)

        Dim gridsql As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
"           	   from ( " & _
"           select Dense_Rank() over (order by q.QuestionID asc " & _
"           	)   as RN " & _
"           	, Question " & _
"           	,q.QuestionID " & _
"           	, q.Method " & _
"			, pc.Response, pc.AnswerComment, pc.AnswerDate, case when pc.AnswerComment is null then 'false' else 'true' end ViewComment " & _
"           from WebFD.VendorContracts.Question_LU q " & _
"			left join WebFD.VendorContracts.Contract_Answers pc on q.QuestionID = pc.QuestionID and pc.Active = 1 " & _
"			where (q.Active = 1 or pc.Active = 1) and pc.ContractID = '" & x.ToString & "' and isnull(SummaryQuestion, 0) = 0) N " & _
"          order by RN "

        Dim gridsql2 As String = "select *, convert(varchar, RN) + '. ' as DisplayingNo	 " & _
"           	   from ( " & _
"           select Dense_Rank() over (order by q.QuestionID asc " & _
"           	)   as RN " & _
"           	, Question " & _
"           	,q.QuestionID " & _
"           	, q.Method " & _
"			, pc.Response, pc.AnswerComment, pc.AnswerDate, case when pc.AnswerComment is null then 'false' else 'true' end ViewComment  " & _
"           from WebFD.VendorContracts.Question_LU q " & _
"			left join WebFD.VendorContracts.Contract_Answers pc on q.QuestionID = pc.QuestionID and pc.Active = 1 " & _
"			where (q.Active = 1 or pc.Active = 1) and pc.ContractID = '" & x.ToString & "' and isnull(SummaryQuestion, 0) = 1) N " & _
"          order by RN "



        gvLegalResultQuestions1.DataSource = GetData(gridsql).DefaultView
        gvLegalResultQuestions1.DataBind()

        gvLegalResultQuestions2.DataSource = GetData(gridsql2).DefaultView
        gvLegalResultQuestions2.DataBind()

    End Sub

    Private Sub loadprintattachmentgrid()

        Dim Str As String = "select * from WebFD.VendorContracts.PendingContractAttachments " & _
            "where ContractID = '" & AttachIndex.Text & "' and Active = 1 and UserID = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' "

        gvPrintAttachments.DataSource = GetData(Str)
        gvPrintAttachments.DataBind()

    End Sub

    Private Sub ResetPage()
        Try
            'Dim SetupSql As String = "insert into WebFD.VendorContracts.PendingContractHeader (UserLogin, Active) " & _
            '"select '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "', 1 where not exists (select * from WebFD.VendorContracts.PendingContractHeader " & _
            '"	where Active = 1 and UserLogin = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "') " & _
            '"" & _
            '"select a.*, isnull(d.DepartmentNo, a.DepartmentID) as DepartmentNo, v.VendorName, v.VendorTIN From WebFD.VendorContracts.PendingContractHeader a " & _
            '" left join WebFD.VendorContracts.Department_LU d on a.DepartmentID = d.DepartmentID and d.Active = 1" & _
            '" left join WebFD.VendorContracts.Vendor_LU v on a.VendorID = v.VendorID and v.Active = 1" & _
            '"where UserLogin = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' and a.Active = 1 "

            'Dim ds As DataTable = GetData(SetupSql)

            LoadContractType()

            asteriskContractName.Text = ""
            asteriskContractCost.Text = ""
            asteriskVendorName.Text = ""
            asteriskAutoRenew.Text = ""
            asteriskContractType.Text = ""
            asteriskContractingParty.Text = ""
            asteriskPleaseSpecify.Text = ""
            asteriskLengthOfContract.Text = ""
            asteriskDesiredRenewalTerm.Text = ""
            asteriskContractPurpose.Text = ""
            asteriskExpenseAccount.Text = ""

            txtSubmitContractName.Text = ""
            txtSubmitVendorName.Text = ""
            ddlSubmitContractParty.SelectedIndex = -1
            txtSubmitContractParty.Visible = False
            lblSubmitContractPartyPleaseSpecify.Visible = False
            txtSubmitContractParty.Text = ""
            txtSubmitContractLength.Text = ""
            ddlSubmitContractCost.SelectedIndex = -1
            txtSubmitContractPurpose.Text = ""
            ddlSubmitContractBudgetAcct.SelectedIndex = -1
            ddlSubContractType.SelectedIndex = -1
            txtSubmissionComments.Text = ""
            'txtSubEffectiveDate.Text = ""
            'txtSubExpirationDate.Text = ""
            rblSubAutoRenewal.SelectedIndex = -1
            txtSubRenewalTerm.Text = ""
            txtSubRenewalTerm.Enabled = False
            'ddlSubContractExpense.SelectedIndex = 0
            'ddlDepartmentNo.SelectedValue = -1
            'btnFindVendor.Text = "Find"
            'lblSubVendorSrch.Text = ""
            'lblSubVendorSrch.BackColor = Drawing.Color.White


            'This will ensure that in case someone is messing around with two webpages at once that it can differentiate between them.
            'Because somebody is going to do that.
            Dim AttachInt As String = "insert  WebFD.VendorContracts.PendingContractAttachments " & _
                "output Inserted.ContractID " & _
                "select isnull(max(ContractID), 0) + 1, null, null, null, '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', getdate(), 0  " & _
                "from WebFD.VendorContracts.PendingContractAttachments " & _
                "where UserID = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' "

            AttachIndex.Text = GetScalar(AttachInt)
            lblSubmissionContractID.Text = AttachIndex.Text

            'Try
            '    ddlSubContractType.SelectedValue = ds.DefaultView(0)("ContractTypeID")
            'Catch ex As Exception
            'End Try

            'If IsDBNull(ds.DefaultView(0)("EffectiveStartDate")) Then
            'Else
            '    txtSubEffectiveDate.Text = ds.DefaultView(0)("EffectiveStartDate")
            'End If
            'If IsDBNull(ds.DefaultView(0)("EffectiveExpirationDate")) Then
            'Else
            '    txtSubExpirationDate.Text = ds.DefaultView(0)("EffectiveExpirationDate")
            'End If
            'If IsDBNull(ds.DefaultView(0)("AutoRenewal")) Then
            'Else
            '    rblSubAutoRenewal.SelectedValue = ds.DefaultView(0)("AutoRenewal")
            'End If
            'If IsDBNull(ds.DefaultView(0)("RenewalTerm")) Then
            'Else
            '    txtSubRenewalTerm.Text = ds.DefaultView(0)("RenewalTerm")
            'End If
            'If IsDBNull(ds.DefaultView(0)("AnnualContractExpense")) Then
            'Else
            '    txtSubContractExpense.Text = ds.DefaultView(0)("AnnualContractExpense")
            'End If
            'If IsDBNull(ds.DefaultView(0)("DepartmentNo")) Then
            'Else
            '    txtDepNo.Text = ds.DefaultView(0)("DepartmentNo")
            'End If
            ''If IsDBNull(ds.DefaultView(0)("VendorName")) = False And rblSubVendor.SelectedValue = "Name" Then
            ''    txtSubVendorSrch.Text = ds.DefaultView(0)("VendorName")
            ''    txtSubVendorSrch.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
            ''    btnFindVendor.Text = "Change"
            ''End If
            ''If IsDBNull(ds.DefaultView(0)("VendorTIN")) = False And rblSubVendor.SelectedValue = "TIN" Then
            ''    txtSubVendorSrch.Text = ds.DefaultView(0)("VendorTIN")
            ''    txtSubVendorSrch.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
            ''    btnFindVendor.Text = "Change"
            ''End If
            ''If IsDBNull(ds.DefaultView(0)("VendorName")) = False Then
            ''    lblSubVendorSrch.Text = ds.DefaultView(0)("VendorName")
            ''    lblSubVendorSrch.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
            ''    btnFindVendor.Text = "Change"
            ''ElseIf IsDBNull(ds.DefaultView(0)("VendorTIN")) = False Then
            ''    lblSubVendorSrch.Text = ds.DefaultView(0)("VendorTIN")
            ''    lblSubVendorSrch.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
            ''    btnFindVendor.Text = "Change"
            ''End If

            'lblContractID.Text = ds.DefaultView(0)("ContractID")
            loadgrid()

            Dim entry As New DirectoryEntry("LDAP://DC=northside, DC=local")
            Dim search As New DirectorySearcher(entry)
            search.Filter = "(&(objectClass=user)(samaccountname=" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "))"
            'Dim i As Integer = search.Filter.Length

            If search.Filter.ToString = "(&(objectClass=user)(samaccountname=))" Then
                Exit Sub
            End If
            Dim temp As String = ""
            For Each AdObj As SearchResult In search.FindAll()
                temp = temp & AdObj.GetDirectoryEntry.Properties.Item("cn").Value
            Next

            lblSubDate.Text = Today
            lblSubRequestor.Text = temp
            lblSubRequestorID.Text = Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "")

            BindSubmissionAttachmentGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub LoadContractType()

        Dim answerString As String = "Select ContractType, ContractTypeID, 1 as ord from WebFD.VendorContracts.ContractType_LU where Active = 1 " & _
                " union select '(Select Contract Type)', -42, 0 order by ord, ContractType"

        ddlSubContractType.DataSource = GetData(answerString)
        ddlSubContractType.DataTextField = "ContractType"
        ddlSubContractType.DataValueField = "ContractTypeID"
        ddlSubContractType.DataBind()


    End Sub

    Private Function ApprovalView()


        'Return GetData("select * from( " & _
        '            "select ContractID, DateAdded, ContractName, DepartmentNo, isnull(DepartmentDisplayName, DepartmentName) as DepartmentName " & _
        '            ", Requestor, SRCH, UserDisplayName as ApprovalSeeking , UserLogin as ApprovalSeekLogin " & _
        '            ", DENSE_RANK() over (partition by ContractID order by Hierarchy asc) RNK from ( " & _
        '            "select ContractID, DepartmentID, ContractName, RequesterUserLogin, isnull(u.UserDisplayName, u.UserFullName) as Requestor, " & _
        '            " ch.AnnualContractExpense, ch.DateAdded, isnull(r.RoleFull, ApprovalRequired) SRCH , r2.Hierarchy " & _
        '            "from WebFD.VendorContracts.ContractHeader ch " & _
        '            "left join WebFD.VendorContracts.Users u on u.UserLogin = ch.RequesterUserLogin " & _
        '            "left join WebFD.VendorContracts.ApprovalReq_LU arq  " & _
        '            "	on arq.Active = 1 and ch.DateAdded between isnull(arq.EffectiveFrom, '1/1/1800') and isnull(arq.EffectiveTo, '12/31/9999') and ch.AnnualContractExpense =  " & _
        '            "	case when LowerBound is null then '<= $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)  " & _
        '            "      	when UpperBound is null then '> $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)   " & _
        '            "        else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'    " & _
        '            "         + left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)   " & _
        '            "         end  " & _
        '            "left join WebFD.VendorContracts.Roles r on r.Alias like arq.ApprovalRequired and r.Active = 1 " & _
        '            "join WebFD.VendorContracts.Roles r2 on isnull(r.RoleFull, ApprovalRequired) = r2.RoleFull " & _
        '            "where ch.Active = 1 and not exists ( " & _
        '            "	select * from WebFD.VendorContracts.Contract_Approvals ca  " & _
        '            "	join WebFD.VendorContracts.ContractHeader c on ca.ContractID = c.ContractID and c.Active = 1 " & _
        '            "	left join WebFD.VendorContracts.Department_2_User d2u on c.DepartmentID = isnull(d2u.DepartmentID, c.DepartmentID) and ca.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
        '            "	left join WebFD.VendorContracts.Roles r3 on d2u.Position = r3.RoleShort and r3.Active = 1 " & _
        '            "	where ca.Active =1 and ca.Status = 'Approved'" & _
        '            "	and (r2.RoleFull = isnull(ca.RoleFullOnSubmit, r3.RoleFull) or r3.Hierarchy > r2.Hierarchy or arq.ApprovalRequired = r3.Alias) and ca.ContractID = ch.ContractID) " & _
        '            "   and not exists ( " & _
        '            "	select * from WebFD.VendorContracts.Contract_Approvals ca  " & _
        '            "	where ca.ContractID = ch.ContractID and ca.Active =1 and ca.Status = 'Rejected') " & _
        '            "	) a " & _
        '            "left join (select u.UserLogin, isnull(u.UserDisplayName, u.UserFullName) as UserDisplayName  " & _
        '            ", d.DepartmentID, DepartmentNo, DepartmentDisplayName, DepartmentName , d2u.Position, r.RoleFull  " & _
        '            "from WebFD.VendorContracts.Users u " & _
        '            "join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
        '            "join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1 " & _
        '            "join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
        '            "where u.Active = 1 ) assign on a.SRCH = isnull(assign.RoleFull, assign.Position) and a.DepartmentID = assign.DepartmentID " & _
        '            ") x  " & _
        '            "where RNK = 1 and ApprovalSeekLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' ").DefaultView


        ' 4/19/2018 CRW -- Switched <= to Less than or Equal to Per Ryan's request

        Return GetData("select distinct x.ContractID, x.DateAdded, x.ContractName, x.DepartmentID, d.DepartmentNo, d.DepartmentName " & _
            ", Requestor, isnull(r2.RoleFull, SRCH) as SRCH, isnull(ApprovalSeeking,  isnull(u.UserDisplayName, u.UserFullName)) as ApprovalSeeking " & _
            ", isnull(ApprovalSeekLogin, u.UserLogin) as ApprovalSeekLogin, RNK " & _
            "from(  " & _
            "                    select ContractID, DateAdded, ContractName, a.DepartmentID, DepartmentNo, isnull(DepartmentDisplayName, DepartmentName) as DepartmentName  " & _
            "                    , Requestor, SRCH, UserDisplayName as ApprovalSeeking , UserLogin as ApprovalSeekLogin  " & _
            "                    , DENSE_RANK() over (partition by ContractID order by Hierarchy asc) RNK from (  " & _
            "                   select ContractID, DepartmentID, ContractName, RequesterUserLogin, isnull(u.UserDisplayName, u.UserFullName) as Requestor,  " & _
            "                     ch.AnnualContractExpense, ch.DateAdded, isnull(r.RoleFull, ApprovalRequired) SRCH , r2.Hierarchy  " & _
            "                    from WebFD.VendorContracts.ContractHeader ch  " & _
            "                    left join WebFD.VendorContracts.Users u on u.UserLogin = ch.RequesterUserLogin  " & _
            "                    left join WebFD.VendorContracts.ApprovalReq_LU arq   " & _
            "                    	on arq.Active = 1 and ch.DateAdded between isnull(arq.EffectiveFrom, '1/1/1800') and isnull(arq.EffectiveTo, '12/31/9999') and ch.AnnualContractExpense =   " & _
            "                    	case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)   " & _
            "                          	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)    " & _
            "                            else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'     " & _
            "                             + left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)    " & _
            "                             end   " & _
            "                    left join WebFD.VendorContracts.Roles r on r.Alias like arq.ApprovalRequired and r.Active = 1  " & _
            "                    join WebFD.VendorContracts.Roles r2 on isnull(r.RoleFull, ApprovalRequired) = r2.RoleFull  " & _
            "                    where ch.Active = 1 and not exists (  " & _
            "                   	select * from WebFD.VendorContracts.Contract_Approvals ca   " & _
            "                   	join WebFD.VendorContracts.ContractHeader c on ca.ContractID = c.ContractID and c.Active = 1  " & _
            "                    	left join WebFD.VendorContracts.Department_2_User d2u on c.DepartmentID = isnull(d2u.DepartmentID, c.DepartmentID) and ca.UserLogin = d2u.UserLogin and d2u.Active = 1  " & _
            "                    	left join WebFD.VendorContracts.Roles r3 on d2u.Position = r3.RoleShort and r3.Active = 1  " & _
            "                    	where ca.Active =1 and ca.Status = 'Approved' " & _
            "                    	and (r2.RoleFull = isnull(ca.RoleFullOnSubmit, r3.RoleFull) or r3.Hierarchy > r2.Hierarchy or arq.ApprovalRequired = r3.Alias) and ca.ContractID = ch.ContractID)  " & _
            "                       and not exists (  " & _
            "                    	select * from WebFD.VendorContracts.Contract_Approvals ca   " & _
            "                    	where ca.ContractID = ch.ContractID and ca.Active =1 and ca.Status = 'Deleted')  " & _
            "                    	) a  " & _
            "                    left join (select u.UserLogin, isnull(u.UserDisplayName, u.UserFullName) as UserDisplayName   " & _
            "                    , d.DepartmentID, DepartmentNo, DepartmentDisplayName, DepartmentName , d2u.Position, r.RoleFull   " & _
            "                    from WebFD.VendorContracts.Users u  " & _
            "                    join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1  " & _
            "                    join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1  " & _
            "                    join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort  " & _
            "                    where u.Active = 1 ) assign on a.SRCH = isnull(assign.RoleFull, assign.Position) and a.DepartmentID = assign.DepartmentID  " & _
            "                    ) x   " & _
            "					join WebFD.VendorContracts.Department_LU d on x.DepartmentID = d.DepartmentID and d.Active = 1  " & _
            "					left join WebFD.VendorContracts.Roles r on x.SRCH = r.RoleFull and x.ApprovalSeekLogin is null and r.Active = 1 " & _
            "					left join WebFD.VendorContracts.Roles r2 on r2.Active =1 and r2.Hierarchy > r.Hierarchy and not exists " & _
            "						(select * from WebFD.VendorContracts.Roles r3 join WebFD.VendorContracts.Department_2_User d2u on r3.RoleShort = d2u.Position  " & _
            "								and isnull(d2u.DepartmentID, x.DepartmentID) = x.DepartmentID and d2u.Active = 1 " & _
            "							where r3.Hierarchy < r2.Hierarchy and r3.Hierarchy > r.Hierarchy and r3.Active = 1 ) " & _
            "							and exists (select * from WebFD.VendorContracts.Department_2_User d join WebFD.VendorContracts.Users ur on ur.UserLogin = d.UserLogin and ur.Active= 1 " & _
            "							 where isnull(d.DepartmentID, x.DepartmentID) = x.DepartmentID  " & _
            "							and d.Active = 1 and d.Position = r2.RoleShort) " & _
            "					left join WebFD.VendorContracts.Department_2_User dx on isnull(dx.DepartmentID, x.DepartmentID) = x.DepartmentID  " & _
            "							and dx.Active = 1 and dx.Position = r2.RoleShort " & _
            "					left join WebFD.VendorContracts.Users u on u.UserLogin = dx.UserLogin and u.Active = 1 " & _
            "                   where RNK = 1  and isnull(ApprovalSeekLogin, u.UserLogin) = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' ").DefaultView


    End Function

    Private Function ReviewedView()
        Return GetData("select distinct ContractID, DateAdded, ContractName, DepartmentNo, DepartmentFull, Requestor, UserStatus	" & _
            "		from ( 	" & _
            "               	select ch.*, isnull(LegalContractAlias, ContractName) as LegalName 	" & _
            "                	, case when Pnding.ContractID is not null then 'Pending' else isnull(Rejection, 'Approved') end as UserStatus   	" & _
            "					, ReviewDate	" & _
            "                	, isnull(pl.LegalPriorityShortDescription, pl.LegalPriorityLongDescription) as LegalPriority, pl.LegalPriorityID 	" & _
            "					, cstat.LegalStatusLongDescription as LegalFullStatus	" & _
            "                	, isnull(cstat.LegalStatusShortDescription, cstat.LegalStatusLongDescription) as LegalStatus, cstat.LegalStatusID 	" & _
            "                	, d.DepartmentNo as DepartmentNo, convert(varchar, DepartmentNo) + ' - ' + DepartmentName as DepartmentFull 	" & _
            "                	, isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as Requestor 	" & _
            "                	, isnull(isnull(lu.UserDisplayName, lu.UserFullName), ch.LegalUserAssigned) as LegalUserName 	" & _
            "                   , isnull(LegalDeadline, dead.AnswerDate) as Deadline, LegalDeadline, dead.AnswerDate as UserRequestedDeadline 	" & _
            "                   , case when LegalDeadline is null and dead.AnswerDate is not null then 'True' else 'False' end as DeadlineColor 	" & _
            "                   , convert(varchar, DepartmentNo) + ' - ' + DepartmentName as Department, ContractType, cp.ContractParty,  convert(varchar, Acct) + ' - ' + Description as ExpenseDescrip, Comment 	" & _
            "                	from WebFD.VendorContracts.ContractHeader ch 	" & _
            "                	left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin and u.Active = 1 	" & _
            "                	left join WebFD.VendorContracts.Users lu on ch.LegalUserAssigned = lu.UserLogin and lu.Active = 1 	" & _
            "                   left join WebFD.VendorContracts.ContractType_LU cl on cl.ContractTypeID = ch.ContractTypeID and cl.Active = 1 	" & _
            "                   left join WebFD.VendorContracts.ContractParty_LU cp on ch.ContractingParty = cp.ContractPartyID and cp.Active = 1 	" & _
            "                   left join DWH.Axiom.Acct a on a.Acct = ch.ExpenseAccount and ISMAP  in ('SUP', 'FEE', 'OTH')  	" & _
            "                   left join WebFD.VendorContracts.Contract_Comments  cc on cc.ContractID = ch.ContractID and cc.CommentType = 'Submission' and cc.Active = 1 and LegalOnly = 0 	" & _
            "                	left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 	" & _
            "                	left join WebFD.VendorContracts.Contract_Status cs on ch.ContractID = cs.ContractID and cs.Active = 1 	" & _
            "                   left join WebFD.VendorContracts.Contract_Answers dead on dead.QuestionID = 17 and dead.Active = 1 and dead.ContractID = ch.ContractID 	" & _
            "                	left join WebFD.VendorContracts.ContractPriority_LU pl on (cs.LegalPriority = pl.LegalPriorityID  	" & _
            "                		or (cs.LegalPriority is null and pl.DefaultInd = 1)) 	" & _
            "                	left join WebFD.VendorContracts.ContractStatus_LU cstat on (cs.LegalStatus = cstat.LegalStatusID  	" & _
            "                		or (cs.LegalStatus is null and cstat.DefaultInd = 1)) 	" & _
            "                		left join (select ContractID, Max(Status) as Rejection	" & _
            "                				from WebFD.VendorContracts.Contract_Approvals where Active = 1 group by ContractID ) rjct  	" & _
            "                				on ch.ContractID = rjct.ContractID and rjct.Rejection = 'Deleted' 	" & _
            "						left join (select ContractID, Max(ModifyDate) as ReviewDate	" & _
            "                				from WebFD.VendorContracts.Contract_Approvals where Active = 1 group by ContractID ) rvw  	" & _
            "                				on ch.ContractID = rvw.ContractID 	" & _
            "                		left join (  select distinct ContractID from (  	" & _
            "                           select ContractID, DepartmentID, ContractName, RequesterUserLogin, isnull(u.UserDisplayName, u.UserFullName) as Requestor,  	" & _
            "                             ch.AnnualContractExpense, ch.DateAdded, isnull(r.RoleFull, ApprovalRequired) SRCH , r2.Hierarchy  	" & _
            "                           from WebFD.VendorContracts.ContractHeader ch  	" & _
            "                            left join WebFD.VendorContracts.Users u on u.UserLogin = ch.RequesterUserLogin  	" & _
            "                          left join WebFD.VendorContracts.ApprovalReq_LU arq   	" & _
            "                            	on arq.Active = 1 and ch.DateAdded between isnull(arq.EffectiveFrom, '1/1/1800') and isnull(arq.EffectiveTo, '12/31/9999') and ch.AnnualContractExpense =   	" & _
            "                            	case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)   	" & _
            "                                  	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)    	" & _
            "                                    else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'     	" & _
            "                                     + left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)    	" & _
            "                                     end   	" & _
            "                            left join WebFD.VendorContracts.Roles r on r.Alias like arq.ApprovalRequired and r.Active = 1  	" & _
            "                            join WebFD.VendorContracts.Roles r2 on isnull(r.RoleFull, ApprovalRequired) = r2.RoleFull  	" & _
            "                            where ch.Active = 1 and not exists (  	" & _
            "                            	select * from WebFD.VendorContracts.Contract_Approvals ca   	" & _
            "                            	join WebFD.VendorContracts.ContractHeader c on ca.ContractID = c.ContractID and c.Active = 1  	" & _
            "                            	left join WebFD.VendorContracts.Department_2_User d2u on c.DepartmentID = isnull(d2u.DepartmentID, c.DepartmentID) and ca.UserLogin = d2u.UserLogin and d2u.Active = 1  	" & _
            "                            	left join WebFD.VendorContracts.Roles r3 on d2u.Position = r3.RoleShort and r3.Active = 1  	" & _
            "                            	where ca.Active =1 and ca.Status = 'Approved' 	" & _
            "                            	and (r2.RoleFull= isnull(ca.RoleFullOnSubmit, r3.RoleFull) or r2.Hierarchy < r3.Hierarchy  or arq.ApprovalRequired = r3.Alias) and ca.ContractID = ch.ContractID)  	" & _
            "                               and not exists (  	" & _
            "                            	select * from WebFD.VendorContracts.Contract_Approvals ca   	" & _
            "                            	where ca.ContractID = ch.ContractID and ca.Active =1 and ca.Status = 'Deleted')  	" & _
            "                            	) a  	" & _
            "                            left join (select u.UserLogin, isnull(u.UserDisplayName, u.UserFullName) as UserDisplayName   	" & _
            "                            , d.DepartmentID, DepartmentNo, DepartmentDisplayName, DepartmentName , d2u.Position, r.RoleFull   	" & _
            "                            from WebFD.VendorContracts.Users u  	" & _
            "                            join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1  	" & _
            "                            join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1  	" & _
            "                            join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort  	" & _
            "                            where u.Active = 1 ) assign on a.SRCH = isnull(assign.RoleFull, assign.Position) and a.DepartmentID = assign.DepartmentID  	" & _
            "                         ) Pnding on Pnding.ContractID = ch.ContractID where ch.active = 1 	" & _
            "                		 ) x  where 1  = 1 and UserStatus <> 'Pending'	" & _
            "						 and  ReviewDate between dateadd(day, -1, getdate()) and getdate()	" & _
            "						 and x.DepartmentID in (select isnull(d2u.DepartmentID, x.DepartmentID) from WebFD.VendorContracts.Department_2_User d2u where Active = 1 and d2u.UserLogin = '" & _
            Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "')	").DefaultView()

    End Function

    Private Sub LoadWaitingYourApprovalGrid()

        'ApprovalView = GetData(x).DefaultView
        gvWaitingonYou.DataSource = ApprovalView()
        gvWaitingonYou.DataBind()

        If ApprovalView.Count = 0 Then
            pnlNoApprovals.Visible = True
        Else
            pnlNoApprovals.Visible = False
        End If


    End Sub

    Private Sub LoadRecentlyReviewedGrid()

        'ApprovalView = GetData(x).DefaultView
        gvRecentlyReceivedContracts.DataSource = ReviewedView()
        gvRecentlyReceivedContracts.DataBind()

        If ReviewedView.Count = 0 Then
            pnlNoReviewedContracts.Visible = True
        Else
            pnlNoReviewedContracts.Visible = False
        End If

        For Each canoe As GridViewRow In gvRecentlyReceivedContracts.Rows
            If canoe.Cells(6).Text = "Deleted" Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                canoe.BorderStyle = BorderStyle.NotSet
                canoe.BorderWidth = "1"
            Else
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                canoe.BorderStyle = BorderStyle.NotSet
                canoe.BorderWidth = "1"
            End If

        Next

    End Sub

    Private Sub LoadExpenseChoices()

        ' 4/19/2018 CRW -- Switched from <= to Less Than or Equal to per Ryan's request
        Dim ExpenseString As String = "select distinct " & _
            "case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3) " & _
            "	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3) " & _
            "	else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'  " & _
            "	+ left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3) " & _
            "	end as ImpactRange " & _
            "	, Dense_Rank() over (order by LowerBound, UpperBound) ord " & _
            " from WebFD.VendorContracts.ApprovalReq_LU where Active = 1 union select '(Select Annual Expense)', 0 " & _
            " order by ord "

        ddlSubmitContractCost.DataSource = GetData(ExpenseString)
        ddlSubmitContractCost.DataTextField = "ImpactRange"
        ddlSubmitContractCost.DataValueField = "ImpactRange"
        ddlSubmitContractCost.DataBind()

    End Sub

    Private Sub gvSubmissionQuestions_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubmissionQuestions.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlResponse As DropDownList = e.Row.FindControl("ddlResponse")
            Dim txtResponse As TextBox = e.Row.FindControl("txtResponse")
            Dim rblResponse As RadioButtonList = e.Row.FindControl("rblResponse")
            Dim cbResponse As CheckBox = e.Row.FindControl("cbResponse")
            Dim txtResponseDate As TextBox = e.Row.FindControl("txtResponseDate")

            Dim answerString As String = "Select * from WebFD.VendorContracts.Question_Answer_LU where Active = 1 and QuestionID = '" & e.Row.DataItem("QuestionID") & "' "

            If e.Row.DataItem("Method") = "DropDownList" Then

                ddlResponse.Visible = True
                txtResponse.Visible = False
                rblResponse.Visible = False
                cbResponse.Visible = False

                Dim ds As New DataTable
                ds = GetData(answerString)
                ddlResponse.DataSource = ds.DefaultView
                ddlResponse.DataTextField = "AnswerText"
                ddlResponse.DataValueField = "AnswerValue"
                ddlResponse.DataBind()

                For Each row As DataRow In ds.Rows
                    If row("DefaultAnswer") = 1 Then
                        ddlResponse.SelectedValue = row("AnswerValue")
                        Exit For
                    End If
                Next

                ' 7/10/2017 CRW -- Removed Pending tables
                'If IsDBNull(e.Row.DataItem("Response")) Then
                '    For Each row As DataRow In ds.Rows
                '        If row("DefaultAnswer") = 1 Then
                '            ddlResponse.SelectedValue = row("AnswerValue")
                '            Exit For
                '        End If
                '    Next
                'Else
                '    ddlResponse.SelectedValue = e.Row.DataItem("Response")
                'End If


            ElseIf e.Row.DataItem("Method") = "RadioButton" Then

                ddlResponse.Visible = False
                txtResponse.Visible = False
                rblResponse.Visible = True
                cbResponse.Visible = False

                Dim ds As New DataTable
                ds = GetData(answerString)
                rblResponse.DataSource = ds.DefaultView
                rblResponse.DataTextField = "AnswerValue"
                rblResponse.DataBind()

                'If IsDBNull(e.Row.DataItem("Response")) Then
                For Each row As DataRow In ds.Rows
                    If row("DefaultAnswer") = 1 Then
                        rblResponse.SelectedValue = row("AnswerValue")
                        Exit For
                    End If
                Next
                'Else
                '    rblResponse.SelectedValue = e.Row.DataItem("Response")
                'End If
            ElseIf e.Row.DataItem("Method") = "Checkbox" Then

                ddlResponse.Visible = False
                txtResponse.Visible = False
                rblResponse.Visible = False
                cbResponse.Visible = True

                Dim ds As New DataTable
                ds = GetData(answerString)
                cbResponse.Text = ds.Rows(0)("AnswerValue").ToString
                cbResponse.DataBind()

                If ds.Rows(0)("DefaultAnswer") = 1 Then
                    cbResponse.Checked = True
                End If


            Else

                ddlResponse.Visible = False
                txtResponse.Visible = True
                rblResponse.Visible = False
                cbResponse.Visible = False

            End If



        End If


    End Sub

    Private Sub rblSubAutoRenewal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblSubAutoRenewal.SelectedIndexChanged

        If rblSubAutoRenewal.SelectedValue = "Yes" Then
            txtSubRenewalTerm.Enabled = True
        Else
            txtSubRenewalTerm.Enabled = False
        End If

    End Sub

    'Private Sub ddlSubContractType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubContractType.SelectedIndexChanged

    '    If ddlSubContractType.SelectedValue.ToString <> "-42" Then
    '        Dim ConType As String = "update WebFD.VendorContracts.PendingContractHeader set ContractTypeID = '" & Replace(ddlSubContractType.SelectedValue.ToString, "'", "''") & "'" & _
    '            "where UserLogin = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' and Active = 1"

    '        ExecuteSql(ConType)
    '        loadgrid()
    '    End If

    'End Sub

    'Private Sub btnFindVendor_Click(sender As Object, e As EventArgs) Handles btnFindVendor.Click

    '    FindVendor()


    'End Sub
    'Private Sub FindVendor()
    '    Dim VendorSQL As String
    '    'If rblSubVendor.SelectedValue = "Name" Then
    '    '    VendorSQL = "Select * from WebFD.VendorContracts.Vendor_LU where VendorName like '%" & Replace(Trim(txtSubVendorSrch.Text), "'", "''") & "%' and Active = 1 order by VendorName "
    '    'Else
    '    '    VendorSQL = "Select * from WebFD.VendorContracts.Vendor_LU where VendorTIN like '%" & Replace(Trim(txtSubVendorSrch.Text), "'", "''") & "%' and Active = 1 order by VendorTIN"
    '    'End If

    '    VendorSQL = "Select * from WebFD.VendorContracts.Vendor_LU where Active = 1 order by VendorName "

    '    gvVendorHunting.DataSource = GetData(VendorSQL)
    '    gvVendorHunting.DataBind()
    '    mpeVendorHunting.Show()
    'End Sub

    'Private Sub gvVendorHunting_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvVendorHunting.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvVendorHunting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvVendorHunting.SelectedIndexChanged
    '    Try

    '        For Each canoe As GridViewRow In gvVendorHunting.Rows
    '            If canoe.RowIndex = gvVendorHunting.SelectedIndex Then
    '                'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
    '            ElseIf canoe.RowIndex Mod 2 = 0 Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
    '            Else
    '                canoe.BackColor = System.Drawing.Color.White
    '            End If
    '        Next
    '        mpeVendorHunting.Show()
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub btnAddVendor_Click(sender As Object, e As EventArgs) Handles btnAddVendor.Click

    '    Dim vendorexists As String = "select count(*) from WebFD.VendorContracts.Vendor_LU where VendorTin = '" & _
    '        Replace(Trim(txtNewVendorTIN.Text), "'", "''") & "' and Active = 1"

    '    If GetScalar(vendorexists) > 0 Then
    '        lblExists.Visible = True
    '        mpeVendorHunting.Show()
    '        Exit Sub
    '    Else
    '        lblExists.Visible = False
    '    End If

    '    Dim VendorInsert As String = "insert into WebFD.VendorContracts.Vendor_LU (VendorName, VendorTIN, Active, DateAdded, DateModified, ModifiedBy) select '" & _
    '        Replace(Trim(txtNewVendorName.Text), "'", "''") & "', '" & Replace(Trim(txtNewVendorTIN.Text), "'", "''") & "', 1, getdate(), getdate(), '" & _
    '        Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' where not exists (select * from WebFD.VendorContracts.Vendor_LU where VendorTin = '" & _
    '        Replace(Trim(txtNewVendorTIN.Text), "'", "''") & "' and Active = 1)"

    '    Dim vendorSelect As String = "select VendorID from WebFD.VendorContracts.Vendor_LU where VendorTIN = '" & Replace(Trim(txtNewVendorTIN.Text), "'", "''") & "' and Active = 1"

    '    ExecuteSql(VendorInsert)
    '    FindVendor()

    '    Dim s As Integer = GetScalar(vendorSelect)

    '    For i As Integer = 0 To gvVendorHunting.Rows.Count - 1

    '        If gvVendorHunting.DataKeys(i).Value = s Then
    '            gvVendorHunting.SelectedIndex = i
    '            gvVendorHunting.Rows(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
    '            Exit For
    '        End If

    '    Next


    'End Sub

    'Private Sub btnVendorConfirm_Click(sender As Object, e As EventArgs) Handles btnVendorConfirm.Click

    '    If gvVendorHunting.SelectedIndex = -1 Then
    '        explantionlabel.Text = "No Vendor Selected"
    '        ModalPopupExtender1.Show()
    '    Else
    '        Dim UpdateVendor As String = "Update WebFD.VendorContracts.PendingContractHeader set VendorID = '" & Replace(gvVendorHunting.SelectedRow.Cells(1).Text, "'", "''") & "' " & _
    '            "where ContractID = '" & Replace(lblContractID.Text, "'", "''") & "'"

    '        ExecuteSql(UpdateVendor)
    '        lblSubVendorSrch.Text = gvVendorHunting.SelectedRow.Cells(2).Text
    '        lblSubVendorSrch.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
    '        btnFindVendor.Text = "Change"
    '    End If

    'End Sub



    'Protected Sub ddlResponse_SelectedIndexChanged1(sender As Object, e As EventArgs)
    '    Dim UpdatesSql As String = ""

    '    For i As Integer = 0 To gvSubmissionQuestions.Rows.Count - 1

    '        Dim lblResponse As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponse"), Label)
    '        Dim ddlResponse As DropDownList = CType(gvSubmissionQuestions.Rows(i).FindControl("ddlResponse"), DropDownList)
    '        Dim rblResponse As RadioButtonList = CType(gvSubmissionQuestions.Rows(i).FindControl("rblResponse"), RadioButtonList)
    '        Dim txtResponse As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponse"), TextBox)


    '        Dim NewRow As Integer = 0
    '        If ddlResponse.SelectedValue <> lblResponse.Text And Len(Trim(ddlResponse.SelectedValue)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(ddlResponse.SelectedValue, "'", "''") & "', DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(ddlResponse.SelectedValue, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If

    '        If rblResponse.SelectedValue <> lblResponse.Text And Len(Trim(rblResponse.SelectedValue)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' , DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(rblResponse.SelectedValue, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If

    '        If txtResponse.Text <> lblResponse.Text And Len(Trim(txtResponse.Text)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(txtResponse.Text, "'", "''") & "' , DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(txtResponse.Text, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If



    '    Next

    '    If Len(UpdatesSql) > 0 Then
    '        ExecuteSql(UpdatesSql)
    '    End If

    '    loadgrid()
    'End Sub

    'Protected Sub rblResponse_SelectedIndexChanged1(sender As Object, e As EventArgs)
    '    Dim UpdatesSql As String = ""

    '    For i As Integer = 0 To gvSubmissionQuestions.Rows.Count - 1

    '        Dim lblResponse As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponse"), Label)
    '        Dim ddlResponse As DropDownList = CType(gvSubmissionQuestions.Rows(i).FindControl("ddlResponse"), DropDownList)
    '        Dim rblResponse As RadioButtonList = CType(gvSubmissionQuestions.Rows(i).FindControl("rblResponse"), RadioButtonList)
    '        Dim txtResponse As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponse"), TextBox)


    '        Dim NewRow As Integer = 0
    '        If ddlResponse.SelectedValue <> lblResponse.Text And Len(Trim(ddlResponse.SelectedValue)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(ddlResponse.SelectedValue, "'", "''") & "', DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(ddlResponse.SelectedValue, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If

    '        If rblResponse.SelectedValue <> lblResponse.Text And Len(Trim(rblResponse.SelectedValue)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' , DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(rblResponse.SelectedValue, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If

    '        If txtResponse.Text <> lblResponse.Text And Len(Trim(txtResponse.Text)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(txtResponse.Text, "'", "''") & "' , DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(txtResponse.Text, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If



    '    Next

    '    If Len(UpdatesSql) > 0 Then
    '        ExecuteSql(UpdatesSql)
    '    End If
    'End Sub

    'Protected Sub TxtResponseChanged(sender As Object, e As EventArgs)
    '    Dim UpdatesSql As String = ""

    '    For i As Integer = 0 To gvSubmissionQuestions.Rows.Count - 1

    '        Dim lblResponse As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponse"), Label)
    '        Dim ddlResponse As DropDownList = CType(gvSubmissionQuestions.Rows(i).FindControl("ddlResponse"), DropDownList)
    '        Dim rblResponse As RadioButtonList = CType(gvSubmissionQuestions.Rows(i).FindControl("rblResponse"), RadioButtonList)
    '        Dim txtResponse As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponse"), TextBox)


    '        Dim NewRow As Integer = 0
    '        If ddlResponse.SelectedValue <> lblResponse.Text And Len(Trim(ddlResponse.SelectedValue)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(ddlResponse.SelectedValue, "'", "''") & "', DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(ddlResponse.SelectedValue, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If

    '        If rblResponse.SelectedValue <> lblResponse.Text And Len(Trim(rblResponse.SelectedValue)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' , DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(rblResponse.SelectedValue, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If

    '        If txtResponse.Text <> lblResponse.Text And Len(Trim(txtResponse.Text)) > 0 Then
    '            'UpdatesSql += "Update DWH.AR.Activity_Detail set AssignedUser = '" & ddlARRowAssignedUser.SelectedValue & "' where Detail_ID = '" & gv_AR_MainData.DataKeys(i).Value.ToString & "' "
    '            UpdatesSql += "Update WebFD.VendorContracts.PendingContract_Answers " & _
    '                "set Response = '" & Replace(txtResponse.Text, "'", "''") & "' , DateModified = getdate(), ModifiedBy = '" & _
    '                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where ContractID = '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' " & _
    '                " " & _
    '                "insert into WebFD.VendorContracts.PendingContract_Answers " & _
    '                "select '" & Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "', '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '" & Replace(txtResponse.Text, "'", "''") & "', " & _
    '                "1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '                "where not exists (select * from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("ContractID").ToString, "'", "''") & "' and QuestionID = '" & _
    '                Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "' ) "
    '        End If


    '    Next

    '    If Len(UpdatesSql) > 0 Then
    '        ExecuteSql(UpdatesSql)
    '    End If
    'End Sub


    Private Sub btnSubmitContract_Click(sender As Object, e As EventArgs) Handles btnSubmitContract.Click
        Try
            Page.MaintainScrollPositionOnPostBack = False
            Dim issue As Integer = 0

            asteriskContractName.Text = ""
            asteriskContractCost.Text = ""
            asteriskVendorName.Text = ""
            asteriskAutoRenew.Text = ""
            asteriskContractType.Text = ""
            asteriskContractingParty.Text = ""
            asteriskPleaseSpecify.Text = ""
            asteriskLengthOfContract.Text = ""
            asteriskDesiredRenewalTerm.Text = ""
            asteriskContractPurpose.Text = ""
            asteriskExpenseAccount.Text = ""

            If Len(Trim(txtSubmitContractName.Text)) = 0 Then
                txtSubmitContractName.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Contract Name Required"
                End If

                'ModalPopupExtender1.Show()
                'Exit Sub
                issue += 1
                asteriskContractName.Text = "*"
                'tcContractNameBack1.BackColor = Drawing.Color.LightYellow
                'tcContractNameBack2.BackColor = Drawing.Color.LightYellow
            End If
            If ddlDepartmentNo.SelectedValue = -1 Then
                ddlDepartmentNo.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Contract Cost Center Required"
                End If

                issue += 1
                'tcCostCenterBack2.Text = "*"
                'tcCostCenterBack2.ForeColor = Drawing.Color.Red
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If ddlSubmitContractCost.SelectedIndex < 1 Then
                ddlSubmitContractCost.Focus()
                asteriskContractCost.Text = "*"
                If issue = 0 Then
                    explantionlabel.Text = "Annual Contract Cost Required"
                End If

                issue += 1
                'tcAnnualCostBack1.ForeColor = Drawing.Color.Red
                'tcAnnualCostBack2.ForeColor = Drawing.Color.Red
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If Len(Trim(txtSubmitVendorName.Text)) = 0 Then
                txtSubmitVendorName.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Vendor Name Required"
                End If

                issue += 1
                asteriskVendorName.Text = "*"
                'tcVendorNameBack1.CssClass = "RedLeft"
                'tcVendorNameBack2.CssClass = "RedRight"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If rblSubAutoRenewal.SelectedIndex = -1 Then
                rblSubAutoRenewal.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Should the Contract Auto-Renew?"
                End If

                issue += 1
                asteriskAutoRenew.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If ddlSubContractType.SelectedIndex < 1 Then
                ddlSubContractType.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Contract Type Required"
                End If

                issue += 1
                asteriskContractType.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If ddlSubmitContractParty.SelectedValue < 1 Then
                ddlSubContractType.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Nosrthside Contracting Party Required"
                End If

                issue += 1
                asteriskContractingParty.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If txtSubmitContractParty.Visible = True And Len(Trim(txtSubmitContractParty.Text)) = 0 Then
                txtSubmitContractParty.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Contracting Party required when """ & ddlSubmitContractParty.SelectedItem.Text & """ is selected"
                End If

                issue += 1
                asteriskPleaseSpecify.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If Len(Trim(txtSubmitContractLength.Text)) = 0 Then
                txtSubmitContractLength.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Contract Length Required"
                End If

                issue += 1
                asteriskLengthOfContract.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If rblSubAutoRenewal.SelectedValue = "Yes" And Len(Trim(txtSubRenewalTerm.Text)) = 0 Then
                rblSubAutoRenewal.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "If the Contract Auto-Renews, you must provide a desired renewal term"
                End If

                issue += 1
                asteriskDesiredRenewalTerm.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If Len(Trim(txtSubmitContractPurpose.Text)) = 0 Then
                txtSubmitContractPurpose.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Contract Purpose Required"
                End If

                issue += 1
                asteriskContractPurpose.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If
            If ddlSubmitContractBudgetAcct.SelectedIndex < 1 Then
                ddlSubmitContractBudgetAcct.Focus()
                If issue = 0 Then
                    explantionlabel.Text = "Budget Expense Account Required  (If you do not know the correct account, you may select ""(Unknown)"")"
                End If

                issue += 1
                asteriskExpenseAccount.Text = "*"
                'ModalPopupExtender1.Show()
                'Exit Sub
            End If

            If CInt(lblSpecialCountFix.Text) + CInt(lblNormalCountFix.Text) > 0 Then
                'pnlSpecialCountFix.Focus()
                DoubleCheckResponses(gvSpecialQuestionSubmission)
                DoubleCheckResponses(gvSubmissionQuestions)
                If issue = 0 Then
                    explantionlabel.Text = "All questions must be answered. <br>(" & CStr(CInt(lblSpecialCountFix.Text) + CInt(lblNormalCountFix.Text)) & " still unanswered)"
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If
            End If

            If issue > 0 Then
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            Dim AnswerSQL As String = "Delete from WebFD.VendorContracts.PendingContract_Answers where ModifiedBy = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
                "' and ContractID = '" & Replace(AttachIndex.Text.ToString, "'", "''") & "' "

            For i As Integer = 0 To gvSubmissionQuestions.Rows.Count - 1

                Dim QuestionID As String = Replace(gvSubmissionQuestions.DataKeys(i).Value.ToString, "'", "''")

                'Dim lblResponse As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponse"), Label)
                Dim txtResponse As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponse"), TextBox)
                Dim ddlResponse As DropDownList = CType(gvSubmissionQuestions.Rows(i).FindControl("ddlResponse"), DropDownList)
                Dim rblResponse As RadioButtonList = CType(gvSubmissionQuestions.Rows(i).FindControl("rblResponse"), RadioButtonList)
                Dim cbResponse As CheckBox = CType(gvSubmissionQuestions.Rows(i).FindControl("cbResponse"), CheckBox)
                Dim txtResponseComment As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponseComment"), TextBox)
                Dim txtResponseDate As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponseDate"), TextBox)

                Dim NewRow As Integer = 0
                AnswerSQL += "Insert into WebFD.VendorContracts.PendingContract_Answers values ('" & Replace(AttachIndex.Text.ToString, "'", "''") & "', '" & QuestionID & "', '"

                Dim answ As String = ""
                If txtResponse.Visible = True Then
                    If Trim(txtResponse.Text) = "" Then
                        txtResponse.Focus()
                        explantionlabel.Text = "Response Required"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    answ = Trim(Replace(txtResponse.Text, "'", "''"))
                ElseIf ddlResponse.Visible = True Then
                    If ddlResponse.SelectedIndex < 1 Then
                        ddlResponse.Focus()
                        explantionlabel.Text = "Response Required"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    answ = Trim(Replace(ddlResponse.SelectedValue, "'", "''"))
                ElseIf rblResponse.Visible = True Then
                    If rblResponse.SelectedIndex = -1 Then
                        rblResponse.Focus()
                        explantionlabel.Text = "Response Required"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    Else
                        answ = Trim(Replace(rblResponse.SelectedValue, "'", "''"))
                    End If
                ElseIf cbResponse.Visible = True Then
                    If cbResponse.Checked Then
                        answ = Trim(Replace(cbResponse.Text, "'", "''"))
                    Else
                        answ = "Not " & Trim(Replace(cbResponse.Text, "'", "''"))
                    End If
                End If

                AnswerSQL += answ

                AnswerSQL += "', 1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "',"

                If txtResponseComment.Visible = True Then
                    If Trim(txtResponseComment.Text) = "" Then
                        explantionlabel.Text = "Comment required when " & answ & " is entered for select questions."
                        ModalPopupExtender1.Show()
                        txtResponseComment.Focus()
                        Exit Sub
                    End If
                    AnswerSQL += "'" & Replace(Trim(txtResponseComment.Text), "'", "''") & "', "
                Else
                    AnswerSQL += "null, "
                End If
                If txtResponseDate.Visible = True Then
                    If Trim(txtResponseDate.Text) = "" Then
                        explantionlabel.Text = "Date required when " & answ & " is entered for select questions."
                        ModalPopupExtender1.Show()
                        txtResponseDate.Focus()
                        Exit Sub
                    End If
                    AnswerSQL += "'" & Replace(Trim(txtResponseDate.Text), "'", "''") & "') "
                Else
                    AnswerSQL += "null) "
                End If

            Next


            For i As Integer = 0 To gvSpecialQuestionSubmission.Rows.Count - 1

                Dim QuestionID As String = Replace(gvSpecialQuestionSubmission.DataKeys(i).Value.ToString, "'", "''")

                'Dim lblResponse As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponse"), Label)
                Dim txtResponse As TextBox = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("txtResponse"), TextBox)
                Dim ddlResponse As DropDownList = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("ddlResponse"), DropDownList)
                Dim rblResponse As RadioButtonList = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("rblResponse"), RadioButtonList)
                Dim cbResponse As CheckBox = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("cbResponse"), CheckBox)
                Dim txtResponseComment As TextBox = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("txtResponseComment"), TextBox)
                Dim txtResponseDate As TextBox = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("txtResponseDate"), TextBox)

                Dim NewRow As Integer = 0
                AnswerSQL += "Insert into WebFD.VendorContracts.PendingContract_Answers values ('" & Replace(AttachIndex.Text.ToString, "'", "''") & "', '" & QuestionID & "', '"

                Dim answ As String = ""
                If txtResponse.Visible = True Then
                    If Trim(txtResponse.Text) = "" Then
                        explantionlabel.Text = "Response Required"
                        ModalPopupExtender1.Show()
                        txtResponse.Focus()
                        Exit Sub
                    End If
                    answ = Trim(Replace(txtResponse.Text, "'", "''"))
                ElseIf ddlResponse.Visible = True Then
                    If ddlResponse.SelectedIndex < 1 Then
                        ddlResponse.Focus()
                        explantionlabel.Text = "Response Required"
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    answ = Trim(Replace(ddlResponse.SelectedValue, "'", "''"))
                ElseIf rblResponse.Visible = True Then
                    If rblResponse.SelectedIndex = -1 Then
                        explantionlabel.Text = "Response Required"
                        ModalPopupExtender1.Show()
                        rblResponse.Focus()
                        Exit Sub
                    Else
                        answ = Trim(Replace(rblResponse.SelectedValue, "'", "''"))
                    End If
                ElseIf cbResponse.Visible = True Then
                    If cbResponse.Checked Then
                        answ = Trim(Replace(cbResponse.Text, "'", "''"))
                    Else
                        answ = "Not " & Trim(Replace(cbResponse.Text, "'", "''"))
                    End If
                End If

                AnswerSQL += answ

                AnswerSQL += "', 1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "',"

                If txtResponseComment.Visible = True Then
                    If Trim(txtResponseComment.Text) = "" Then
                        explantionlabel.Text = "Comment required when " & answ & " is entered for select questions."
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    AnswerSQL += "'" & Replace(Trim(txtResponseComment.Text), "'", "''") & "', "
                Else
                    AnswerSQL += "null, "
                End If

                If txtResponseDate.Visible = True Then
                    If Trim(txtResponseDate.Text) = "" Then
                        explantionlabel.Text = "Date required when " & answ & " is entered for select questions."
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                    AnswerSQL += "'" & Replace(Trim(txtResponseDate.Text), "'", "''") & "') "
                Else
                    AnswerSQL += "null) "
                End If

            Next

            ExecuteSql(AnswerSQL)

            If GetScalar("select count(*) from WebFD.VendorContracts.PendingContractAttachments " & _
                "where ContractID = '" & AttachIndex.Text & "' and Active = 1 and UserID = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") < 1 Then
                explantionlabel.Text = "No attachments have been submitted for this contract"
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            pnlPrinting.Visible = True
            pnlSubmitting.Visible = False

            lblSubmitContractName.Text = txtSubmitContractName.Text
            lblSubmitContractLength.Text = txtSubmitContractLength.Text
            lblSubDate2.Text = lblSubDate.Text
            lblSubRequestor2.Text = lblSubRequestor.Text
            lblSubAutoRenewal.Text = rblSubAutoRenewal.Text
            lblDepartmentNo.Text = ddlDepartmentNo.SelectedItem.Text.ToString
            lblSubRenewalTerm.Text = txtSubRenewalTerm.Text
            lblContractName.Text = txtSubmitVendorName.Text
            lblSubmitContractCost.Text = ddlSubmitContractCost.SelectedItem.Text.ToString
            lblSubContractType.Text = ddlSubContractType.SelectedItem.Text.ToString
            lblSubmitContractPurpose.Text = txtSubmitContractPurpose.Text.ToString
            lblSubmitContractParty.Text = ddlSubmitContractParty.SelectedItem.Text.ToString
            lblSubmitContractBudgetAcct.Text = ddlSubmitContractBudgetAcct.SelectedItem.Text.ToString
            lblSubmissionComments.Text = txtSubmissionComments.Text

            If txtSubmitContractParty.Visible = True Then
                lblSubmitContractParyExplain.Text = txtSubmitContractParty.Text
            Else
                lblSubmitContractParyExplain.Text = ""
            End If

            loadprintattachmentgrid()
            loadprintgrid()
            'btnPrintContract.Attributes.Add("onclick", "javascript:window.print()")

            testfocus.Focus()

            Exit Sub


            'Dim culture As CultureInfo
            'Dim styles As DateTimeStyles
            'Dim dateResult As DateTime

            '' Parse a date and time with no styles.
            'culture = CultureInfo.CreateSpecificCulture("en-US")
            'styles = DateTimeStyles.None

            'If Trim(ddlDepartmentNo.SelectedValue) = -1 Then
            '    explantionlabel.Text = "Department Number Required"
            '    ModalPopupExtender1.Show()
            '    Exit Sub
            '    'ElseIf lblSubVendorSrch.BackColor <> System.Drawing.ColorTranslator.FromHtml("#ccffcc") Then
            '    '    explantionlabel.Text = "Please confirm Vendor with Find button"
            '    '    ModalPopupExtender1.Show()
            '    '    Exit Sub
            'ElseIf Trim(ddlSubContractType.Text) = "-42" Then
            '    explantionlabel.Text = "Valid Contract Type Required"
            '    ModalPopupExtender1.Show()
            '    Exit Sub
            '    'ElseIf DateTime.TryParse(Trim(txtSubEffectiveDate.Text), culture, styles, dateResult) = False Then
            '    '    explantionlabel.Text = "Invalid Effective Date Format"
            '    '    ModalPopupExtender1.Show()
            '    '    Exit Sub
            '    'ElseIf DateTime.TryParse(Trim(txtSubExpirationDate.Text), culture, styles, dateResult) = False Then
            '    '    explantionlabel.Text = "Invalid Expiration Date Format"
            '    '    ModalPopupExtender1.Show()
            '    '    Exit Sub
            '    'ElseIf CDate(txtSubEffectiveDate.Text) > CDate(txtSubExpirationDate.Text) Then
            '    '    explantionlabel.Text = "Contract cannot expire before it begins"
            '    '    ModalPopupExtender1.Show()
            '    '    Exit Sub
            'ElseIf Trim(rblSubAutoRenewal.Text) = "Yes" And Trim(txtSubRenewalTerm.Text) = "" Then
            '    explantionlabel.Text = "Renewal term required when Auto Renewal is set to Yes"
            '    ModalPopupExtender1.Show()
            '    Exit Sub
            'ElseIf Trim(ddlSubmitContractCost.SelectedValue) = "(Select Annual Expense)" Then
            '    explantionlabel.Text = "Annual Contract Expense Required"
            '    ModalPopupExtender1.Show()
            '    Exit Sub
            '    'ElseIf txtSubContractExpense.BackColor = Drawing.Color.LightGray Then
            '    '    explantionlabel.Text = "Annual Contract Expense should be a decimal number"
            '    '    ModalPopupExtender1.Show()
            '    '    Exit Sub
            'End If

            'Dim SubmitSQL As String = "Insert WebFD.VendorContracts.ContractHeader (UserLogin, DepartmentID, ContractTypeID, EffectiveStartDate, EffectiveExpirationDate, AutoRenewal, RenewalTerm, " & _
            '    " AnnualContractExpense, Active, DateAdded, DateModified, ModifiedBy, Submitted) " & _
            '    "Output Inserted.ContractID select '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
            '    "', '" & Replace(ddlDepartmentNo.SelectedValue, "'", "''") & "', " & _
            '    '"', " & Replace(ddlSubContractType.SelectedValue, "'", "''") & ", '" & Replace(Trim(txtSubEffectiveDate.Text), "'", "''") & "', '" & Replace(Trim(txtSubExpirationDate.Text), "'", "''") & _
            ''"', '" & Replace(rblSubAutoRenewal.SelectedValue, "'", "''") & "', "

            'If rblSubAutoRenewal.SelectedValue = "Yes" Then
            '    SubmitSQL += "'" & Replace(txtSubRenewalTerm.Text, "'", "''") & "', "
            'Else
            '    SubmitSQL += " null, "
            'End If

            'SubmitSQL += Replace(ddlSubContractExpense.SelectedValue, "'", "''") & ", 1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
            '    "', 1 "

            'Dim contractID As Integer = GetScalar(SubmitSQL)

            'Dim AnswerSql As String = ""

            'For i As Integer = 0 To gvSubmissionQuestions.Rows.Count - 1

            '    'Dim lblResponse As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponse"), Label)
            '    Dim txtResponse As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponse"), TextBox)
            '    Dim ddlResponse As DropDownList = CType(gvSubmissionQuestions.Rows(i).FindControl("ddlResponse"), DropDownList)
            '    Dim rblResponse As RadioButtonList = CType(gvSubmissionQuestions.Rows(i).FindControl("rblResponse"), RadioButtonList)

            '    Dim NewRow As Integer = 0
            '    AnswerSql += "Insert into WebFD.VendorContracts.Contract_Answers values (" & contractID & ", '" & _
            '        Replace(gvSubmissionQuestions.DataKeys(i)("QuestionID").ToString, "'", "''") & "', '"

            '    If txtResponse.Visible = True Then
            '        If Trim(txtResponse.Text) = "" Then
            '            explantionlabel.Text = "Response Required"
            '            ModalPopupExtender1.Show()
            '            Exit Sub
            '        End If
            '        AnswerSql += Trim(Replace(txtResponse.Text, "'", "''"))
            '    ElseIf ddlResponse.Visible = True Then
            '        AnswerSql += Trim(Replace(ddlResponse.SelectedValue, "'", "''"))
            '    ElseIf rblResponse.Visible = True Then
            '        AnswerSql += Trim(Replace(rblResponse.SelectedValue, "'", "''"))
            '    End If

            '    AnswerSql += "', 1, getdate(), getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "') "

            'Next


            'AnswerSql += " Insert into WebFD.VendorContracts.ContractAttachments select " & contractID & ", FileName, ContentType, Content, UserID, Dateadded, Active " & _
            '    "from WebFD.VendorContracts.PendingContractAttachments where UserLogin = '" & _
            '    Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and Active = 1 " & _
            '    " Delete from WebFD.VendorContracts.PendingContractAttachments where UserLogin = '" & _
            '    Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and Active = 1 "

            ''"insert into WebFD.VendorContracts.Users " & _
            ''"select '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', '" & Replace(lblSubRequestor.Text, "'", "''") & "', null, 1, " & _
            ''" getdate(), getdate(), 'Automatic', 0, 0, 0, 0 " & _
            ''"where not exists (select * from WebFD.VendorContracts.Users where UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "') "

            ''" Delete from WebFD.VendorContracts.PendingContractHeader where Active = 1 and UserLogin = '" & _
            ''    Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'" & _
            ''    " Delete from WebFD.VendorContracts.PendingContract_Answers where Active = 1 and not exists (select * from WebFD.VendorContracts.PendingContractHeader pc where  " & _
            ''    "pc.ContractID = WebFD.VendorContracts.PendingContract_Answers.ContractID and pc.Active = 1) " & _


            'ExecuteSql(AnswerSql)

            'explantionlabel.Text = "Submitted Contract"
            'ResetPage()
            'SrchGrid()
            'ModalPopupExtender1.Show()



        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    'Private Sub txtSubVendorSrch_TextChanged(sender As Object, e As EventArgs) Handles txtSubVendorSrch.TextChanged
    '    txtNewVendorName.BackColor = Drawing.Color.White
    'End Sub

    'Private Sub txtSubContractExpense_TextChanged(sender As Object, e As EventArgs) Handles txtSubContractExpense.TextChanged

    '    Dim number As Decimal
    '    If Decimal.TryParse(txtSubContractExpense.Text, number) Then
    '        If number > 100000 Then
    '            txtSubContractExpense.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8080")
    '        Else
    '            txtSubContractExpense.BackColor = Drawing.Color.White
    '        End If
    '    Else
    '        txtSubContractExpense.BackColor = Drawing.Color.LightGray
    '    End If

    'End Sub

    Private Sub lbDisplaySrch_Click(sender As Object, e As EventArgs) Handles lbDisplaySrch.Click
        If pnlSrchPanel.Visible = True Then
            pnlSrchPanel.Visible = False
        Else
            pnlSrchPanel.Visible = True
        End If
    End Sub

#Region "OldGrids"
    '    Private Sub LoadWaitingOnMeGrid()

    '        Dim AllMine As Integer = 0
    '        Dim Waiting As Integer = 0


    '        Dim Srch As String = "select * from ( " & _
    '        "select isnull(isnull(u.UserDisplayName, u.UserFullName), cl.UserLogin) as UserSubmitted " & _
    '        ", cl.*  , isnull(isnull(d.DepartmentDisplayName, d.DepartmentName), cl.DepartmentID) as Department " & _
    '        ", v.VendorName " & _
    '        ", v.VendorTIN, ct.ContractType " & _
    '        ",DirectorStatus " & _
    '        "      ,DirectorLogin " & _
    '        "      ,DirectorDate " & _
    '        "      ,DirectorComments " & _
    '        "      ,ComptrollerStatus " & _
    '        "      ,ComptrollerLogin " & _
    '        "      ,ComptrollerDate " & _
    '        "      ,ComptrollerComments " & _
    '        "      ,SupplyStatus " & _
    '        "      ,SupplyLogin " & _
    '        "      ,SupplyDate " & _
    '        "      ,SupplyComments " & _
    '        "      ,ComplianceOfficerStatus " & _
    '        "      ,ComplianceOfficerLogin " & _
    '        "      ,ComplianceOfficerrDate " & _
    '        ", case when DirectorStatus = 1 and ComptrollerStatus = 1 and SupplyStatus = 1 and ComplianceOfficerStatus = 1 " & _
    '        "	then 'Green' " & _
    '        "	when DirectorStatus = 0 then 'Red' " & _
    '        "	when ComptrollerStatus = 0 then 'Red' " & _
    '        "	when SupplyStatus = 0 then 'Red' " & _
    '        "	when ComplianceOfficerStatus = 0 then 'Red' " & _
    '        "	else 'Yellow' end as Status " & _
    '        ", case when d2u.UserLogin = uPulled.UserLogin and DirectorStatus is null then 1 " & _
    '        "	when uPulled.ComplianceOfficer = 1 and ComplianceOfficerStatus is null then 1 " & _
    '        "	when uPulled.Supply = 1 and SupplyStatus is null then 1 " & _
    '        "	when uPulled.Comptroller = 1 and ComptrollerStatus is null then 1 " & _
    '        "	else null end as Waiting	 " & _
    '        ",case when uPulled.UserLogin <> DirectorLogin and rtrim(ltrim(DirectorComments)) <> '' " & _
    '"	then ';newline;' + isnull(isnull(diru.UserDisplayName, diru.UserFullName), cs.DirectorLogin) + ' - '  " & _
    '"	+ DirectorComments + '(' + convert(varchar, DirectorDate, 107) + ') ;newline;' " & _
    '"	else '' " & _
    '"	end  + " & _
    '" case when uPulled.UserLogin <> ComptrollerLogin and rtrim(ltrim(ComptrollerComments)) <> '' " & _
    '"	then ';newline;' +  isnull(isnull(ctiru.UserDisplayName, ctiru.UserFullName), cs.ComptrollerLogin) + ' - '  " & _
    '"	+ ComptrollerComments + '(' + convert(varchar, ComptrollerDate, 107) + ') ;newline;' " & _
    '"	else '' " & _
    '"	end	 + " & _
    '"case when uPulled.UserLogin <> SupplyLogin and rtrim(ltrim(SupplyComments)) <> '' " & _
    '"	then ';newline;' +  isnull(isnull(supu.UserDisplayName, supu.UserFullName), cs.SupplyLogin) + ' - '  " & _
    '"	+ SupplyComments + '(' + convert(varchar, SupplyDate, 107) + ') ;newline;' " & _
    '"	else '' " & _
    '"	end	 	 + " & _
    '"case when uPulled.UserLogin <> ComplianceOfficerLogin and rtrim(ltrim(ComplianceOfficerComments)) <> '' " & _
    '"	then ';newline;' +  isnull(isnull(sysu.UserDisplayName, sysu.UserFullName), cs.ComplianceOfficerLogin) + ' - ' " & _
    '"	+ ComplianceOfficerComments + '(' + convert(varchar, ComplianceOfficerrDate, 107) + ') ;newline;' " & _
    '"	else '' " & _
    '"	end as OtherComments " & _
    '",case when uPulled.UserLogin = DirectorLogin and rtrim(ltrim(DirectorComments)) <> '' " & _
    '"	then ''  " & _
    '"	+ DirectorComments + '(' + convert(varchar, DirectorDate, 107) + ') ' " & _
    '"	else '' " & _
    '"	end  + " & _
    '"case when uPulled.UserLogin = ComptrollerLogin and rtrim(ltrim(ComptrollerComments)) <> '' " & _
    '"	then ''  " & _
    '"	+ ComptrollerComments + '(' + convert(varchar, ComptrollerDate, 107) + ') ' " & _
    '"	else '' " & _
    '"	end	 + " & _
    '"case when uPulled.UserLogin = SupplyLogin and rtrim(ltrim(SupplyComments)) <> '' " & _
    '"	then ''  " & _
    '"	+ SupplyComments + '(' + convert(varchar, SupplyDate, 107) + ') ' " & _
    '"	else '' " & _
    '"	end	 	 + " & _
    '"case when uPulled.UserLogin = ComplianceOfficerLogin and rtrim(ltrim(ComplianceOfficerComments)) <> '' " & _
    '"	then ''  " & _
    '"	+ ComplianceOfficerComments + '(' + convert(varchar, ComplianceOfficerrDate, 107) + ') ' " & _
    '"	else '' " & _
    '"	end as YourComments " & _
    '", case when attch.cnt > 0 then 'View Attachments' else 'Add Attachment' end as Attch " & _
    '        "from WebFD.VendorContracts.ContractHeader cl " & _
    '        "left join WebFD.VendorContracts.Vendor_LU v on cl.VendorID = v.VendorID " & _
    '        "left join WebFD.VendorContracts.Users u on cl.UserLogin = u.UserLogin " & _
    '        "left join WebFD.VendorContracts.Contract_Status cs on cl.ContractID = cs.ContractID and cs.Active = 1 " & _
    '        "left join WebFD.VendorContracts.Users uPulled on uPulled.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '        "left join WebFD.VendorContracts.Department_LU d on cl.DepartmentID = d.DepartmentID and d.Active = 1" & _
    '        "left join WebFD.VendorContracts.ContractType_LU ct on cl.ContractTypeID = ct.ContractTypeID and ct.Active = 1" & _
    '        "left join WebFD.VendorContracts.Department_2_User d2u on cl.DepartmentID = d2u.DepartmentID " & _
    '        "	and d2u.Active = 1 and uPulled.UserLogin = d2u.UserLogin " & _
    '         "left join WebFD.VendorContracts.Users diru on cs.DirectorLogin = diru.UserLogin " & _
    '  "left join WebFD.VendorContracts.Users ctiru on cs.ComptrollerLogin = ctiru.UserLogin " & _
    '  "left join WebFD.VendorContracts.Users supu on cs.SupplyLogin = supu.UserLogin " & _
    '  "left join WebFD.VendorContracts.Users sysu on cs.ComplianceOfficerLogin = sysu.UserLogin " & _
    '  " left join (select count(*) cnt, ContractID from WebFD.VendorContracts.ContractAttachments " & _
    '    "where Active = 1 group by ContractID) attch on attch.ContractID = cl.ContractID " & _
    '        "where ((d2u.UserLogin is not null and DirectorStatus is null) " & _
    '         "or (u.UserLogin = uPulled.UserLogin and 1 = " & AllMine.ToString & " )" & _
    '         "or (DirectorStatus = 1 and ( uPulled.ComplianceOfficer = 1 or uPulled.Supply = 1 or uPulled.Comptroller = 1))) " & _
    '         "and cl.DateAdded between '" & Replace(txtSrchSubStart.Text, "'", "''") & "' and '" & Replace(txtSrchSubEnd.Text, "'", "''") & "' " & _
    '         " ) x where Waiting = " & Waiting.ToString & _
    '          " or (UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and 1 = " & AllMine.ToString & " )"



    '        SearchView = GetData(Srch).DefaultView
    '        gvSearchedContracts.DataSource = SearchView
    '        gvSearchedContracts.DataBind()
    '        gvSearchedContracts.ShowHeaderWhenEmpty = True

    '        If SearchView.Count = 0 Then
    '            gvSearchedContracts.Visible = False
    '        Else
    '            gvSearchedContracts.Visible = True
    '        End If

    '    End Sub




    'Private Sub gvSearchedContracts_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSearchedContracts.PageIndexChanging
    '    Try

    '        gvSearchedContracts.PageIndex = e.NewPageIndex
    '        gvSearchedContracts.DataSource = SearchView
    '        gvSearchedContracts.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try

    'End Sub

    'Private Sub gvSearchedContracts_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSearchedContracts.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
    '            'e.Row.Cells(7).CssClass = "hidden"
    '            'e.Row.Cells(8).CssClass = "hidden"

    '            If e.Row.DataItem("Status") = "Red" Then
    '                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
    '                If e.Row.DataItem("Waiting") = "1" Then
    '                    Dim img2 As Image = e.Row.FindControl("imgLRHR")
    '                    img2.Visible = True
    '                End If
    '            ElseIf e.Row.DataItem("Status") = "Yellow" Then
    '                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
    '                If e.Row.DataItem("Waiting") = "1" Then
    '                    Dim img2 As Image = e.Row.FindControl("imgLYHR")
    '                    img2.Visible = True
    '                End If
    '            ElseIf e.Row.DataItem("Status") = "Green" Then
    '                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
    '            End If

    '        End If
    '    Catch ex As Exception
    '        'LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvSearchedContracts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSearchedContracts.SelectedIndexChanged
    '    Try

    '        lblPendingAutoRenewal.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(13).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingContractExpense.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(6).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingContractID.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(2).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingContractType.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(11).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingDateSubmitted.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(5).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingDepNo.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(10).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingEffDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(7).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingExpirationDate.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(12).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingRenewalTerm.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(14).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingRequestor.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(4).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingVendor.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(3).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblPendingVendorTIN.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(15).Text), "&nbsp;", ""), "replacedapostrophe", "'")
    '        lblOtherComments.Text = Replace(Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(21).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")
    '        txtYourComments.Text = Replace(Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(20).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")
    '        lbvwAttachments.Text = Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(22).Text), "&nbsp;", ""), "replacedapostrophe", "'")

    '        'If Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "1" Then
    '        '    lblDirectorStatus.Text = "Approved"
    '        '    trSysCont.Visible = True
    '        '    trSupply.Visible = True
    '        '    trCompOff.Visible = True
    '        'ElseIf Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(16).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "0" Then
    '        '    lblDirectorStatus.Text = "Rejected"
    '        '    trSysCont.Visible = False
    '        '    trSupply.Visible = False
    '        '    trCompOff.Visible = False
    '        'Else
    '        '    lblDirectorStatus.Text = "Pending"
    '        '    trSysCont.Visible = False
    '        '    trSupply.Visible = False
    '        '    trCompOff.Visible = False
    '        'End If
    '        'If Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "1" Then
    '        '    lblSysContStatus.Text = "Approved"
    '        'ElseIf Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(17).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "0" Then
    '        '    lblSysContStatus.Text = "Rejected"
    '        'Else
    '        '    lblSysContStatus.Text = "Pending"
    '        'End If
    '        'If Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "1" Then
    '        '    lblSupplyStatus.Text = "Approved"
    '        'ElseIf Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(18).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "0" Then
    '        '    lblSupplyStatus.Text = "Rejected"
    '        'Else
    '        '    lblSupplyStatus.Text = "Pending"
    '        'End If
    '        'If Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(19).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "1" Then
    '        '    lblComplianceOfficerStatus.Text = "Approved"
    '        'ElseIf Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(19).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "0" Then
    '        '    lblComplianceOfficerStatus.Text = "Rejected"
    '        'Else
    '        '    lblComplianceOfficerStatus.Text = "Pending"
    '        'End If

    '        If Replace(Replace(Server.HtmlDecode(gvSearchedContracts.SelectedRow.Cells(8).Text), "&nbsp;", ""), "replacedapostrophe", "'") = "1" Then
    '            btnAcceptContract.Visible = True
    '            btnRejectContract.Visible = True
    '            txtYourComments.Visible = True
    '            lblYourComments.Visible = True

    '        Else
    '            btnAcceptContract.Visible = False
    '            btnRejectContract.Visible = False
    '            If Trim(txtYourComments.Text) = "" Then
    '                txtYourComments.Visible = False
    '                lblYourComments.Visible = False
    '            Else
    '                txtYourComments.Visible = True
    '                lblYourComments.Visible = True
    '            End If

    '        End If

    '        If Trim(lblOtherComments.Text) = "" Then
    '            lblOtherCommentsStart.Visible = False
    '        Else
    '            lblOtherCommentsStart.Visible = True
    '        End If

    '        'lblUpdateNotes.Text = Replace(Replace(Replace(Server.HtmlDecode(gvOpenCases.SelectedRow.Cells(23).Text), "&nbsp;", ""), ";newline;", "<BR><BR>"), "replacedapostrophe", "'")

    '        pnlResultPanel.Visible = True
    '        Rowing()
    '        loadQuestiongrid()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gvSearchedContracts_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSearchedContracts.Sorting
    '    Dim dv As DataView
    '    Dim sorts As String
    '    dv = SearchView

    '    sorts = e.SortExpression

    '    If e.SortExpression = searchmap Then

    '        If searchdir = 1 Then
    '            dv.Sort = sorts + " " + "desc"
    '            searchdir = 0
    '        Else
    '            dv.Sort = sorts + " " + "asc"
    '            searchdir = 1
    '        End If

    '    Else
    '        dv.Sort = sorts + " " + "asc"
    '        searchdir = 1
    '        searchmap = e.SortExpression
    '    End If

    '    gvSearchedContracts.DataSource = dv
    '    gvSearchedContracts.DataBind()
    'End Sub

    'Private Sub Rowing()

    '    Dim img3 As Image
    '    Dim img4 As Image


    '    For Each canoe As GridViewRow In gvSearchedContracts.Rows
    '        If canoe.RowIndex = gvSearchedContracts.SelectedIndex Then
    '            If canoe.Cells(9).Text = "Red" Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
    '                If canoe.Cells(8).Text = "1" Then
    '                    img3 = canoe.FindControl("imgLRHR")
    '                    img3.Visible = False
    '                    img4 = canoe.FindControl("imgDRHR")
    '                    img4.Visible = True
    '                End If
    '            ElseIf canoe.Cells(9).Text = "Yellow" Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
    '                If canoe.Cells(8).Text = "1" Then
    '                    img3 = canoe.FindControl("imgLYHR")
    '                    img3.Visible = False
    '                    img4 = canoe.FindControl("imgDYHR")
    '                    img4.Visible = True
    '                End If
    '            ElseIf canoe.Cells(9).Text = "Green" Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc")
    '            End If
    '        Else
    '            If canoe.Cells(9).Text = "Red" Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
    '                If canoe.Cells(8).Text = "1" Then
    '                    img3 = canoe.FindControl("imgDRHR")
    '                    img3.Visible = False
    '                    img4 = canoe.FindControl("imgLRHR")
    '                    img4.Visible = True
    '                End If
    '            ElseIf canoe.Cells(9).Text = "Yellow" Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
    '                If canoe.Cells(8).Text = "1" Then
    '                    img3 = canoe.FindControl("imgDYHR")
    '                    img3.Visible = False
    '                    img4 = canoe.FindControl("imgLYHR")
    '                    img4.Visible = True
    '                End If
    '            ElseIf canoe.Cells(9).Text = "Green" Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#a8ffa8")
    '            End If
    '        End If

    '    Next
    'End Sub


    'Private Sub cbSrchAllMine_CheckedChanged(sender As Object, e As EventArgs) Handles cbSrchAllMine.CheckedChanged
    '    SrchGrid()
    '    pnlResultPanel.Visible = False
    'End Sub

    'Private Sub cbSrchWaiting_CheckedChanged(sender As Object, e As EventArgs) Handles cbSrchWaiting.CheckedChanged
    '    SrchGrid()
    '    pnlResultPanel.Visible = False
    'End Sub

    '    Private Sub loadQuestiongrid()

    '        Dim sandwich As Integer = lblPendingContractID.Text

    '        Dim gridsql As String = "select *, convert(varchar, RN) + case when ParentQuestionID is null then '' else  " & _
    '            "	Coalesce((SELECT Char(65 + (N.Num - 475255) / 456976 % 26) WHERE N.Num >= 475255), '') " & _
    '            "      + Coalesce((SELECT Char(65 + (N.Num - 18279) / 17576 % 26) WHERE N.Num >= 18279), '') " & _
    '            "      + Coalesce((SELECT Char(65 + (N.Num - 703) / 676 % 26) WHERE N.Num >= 703), '') " & _
    '            "      + Coalesce((SELECT Char(65 + (N.Num - 27) / 26 % 26) WHERE N.Num >= 27), '') " & _
    '            "      + (SELECT Char(65 + (N.Num - 1) % 26)) end + '. ' as DisplayingNo	 " & _
    '            "	   from ( " & _
    '            "select Dense_Rank() over (order by isnull(ParentQuestionID, ctq.QuestionID) asc " & _
    '            "	)   as RN " & _
    '            "	,Row_Number() over (partition by isnull(ParentQuestionID, -ctq.QuestionID) order by  " & _
    '            "	case when ParentQuestionID is null then 0 else 1 end)    as Num " & _
    '            "	, Question " & _
    '            "	, ca.Response " & _
    '            "	, c.ContractID, q.QuestionID " & _
    '            "	, q.Method " & _
    '            "	, ParentQuestionID " & _
    '            "from WebFD.VendorContracts.ContractHeader c " & _
    '            "join WebFD.VendorContracts.ContractType_LU ctl on c.ContractTypeID = ctl.ContractTypeID and ctl.Active = 1 " & _
    '            "left join WebFD.VendorContracts.Contracts_2_Questions ctq on ctl.ContractTypeID = ctq.ContractTypeID and ctq.Active = 1 " & _
    '            "left join WebFD.VendorContracts.Question_LU q on ctq.QuestionID = q.QuestionID and q.Active = 1 " & _
    '            "left join WebFD.VendorContracts.Contract_Answers ca on ctq.QuestionID = ca.QuestionID and ca.ContractID = c.ContractID and ca.Active = 1 " & _
    '            "left join WebFD.VendorContracts.Contract_Answers cap on ParentQuestionID = cap.QuestionID and c.ContractID = cap.ContractID " & _
    '            "	and ( (  QualificationOperator = '=' and cap.Response = Qualification ) " & _
    '            "	or (  QualificationOperator = '>' and cap.Response > Qualification ) " & _
    '            "	or (  QualificationOperator = '<' and cap.Response < Qualification ) " & _
    '            "	)	and cap.Active = 1 " & _
    '            "where c.ContractID = " & sandwich & " and (ParentQuestionID is null or cap.Active = 1) " & _
    '            ") N " & _
    '            "order by RN, Num "

    '        gvPendingContractAnswers.DataSource = GetData(gridsql).DefaultView
    '        gvPendingContractAnswers.DataBind()
    '        gvPendingContractAnswers.ShowHeaderWhenEmpty = True

    '        If GetData(gridsql).DefaultView.Count = 0 Then
    '            gvPendingContractAnswers.Visible = False
    '        Else
    '            gvPendingContractAnswers.Visible = True
    '        End If


    '    End Sub

    '    'Private Sub txtSrchSubEnd_TextChanged(sender As Object, e As EventArgs) Handles txtSrchSubEnd.TextChanged
    '    '    SrchGrid()
    '    '    pnlResultPanel.Visible = False
    '    'End Sub

    '    'Private Sub txtSrchSubStart_TextChanged(sender As Object, e As EventArgs) Handles txtSrchSubStart.TextChanged
    '    '    SrchGrid()
    '    '    pnlResultPanel.Visible = False
    '    'End Sub

    '    Private Sub btnAcceptContract_Click(sender As Object, e As EventArgs) Handles btnAcceptContract.Click

    '        Dim AcceptSql As String =
    '"insert into WebFD.VendorContracts.Contract_Status " & _
    '"(ContractID, DirectorStatus, DirectorLogin, DirectorDate, DirectorComments, Active) " & _
    '"select ch.ContractID, 1, d2u.UserLogin, getdate(), '" & Replace(txtYourComments.Text, "'", "''") & "', 1 from WebFD.VendorContracts.ContractHeader ch " & _
    '"join WebFD.VendorContracts.Department_2_User d2u on ch.DepartmentID = d2u.DepartmentID  " & _
    '"and d2u.Active = 1 and d2u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '"where ContractID = " & lblPendingContractID.Text & " " & _
    '"and not exists (select * from WebFD.VendorContracts.Contract_Status cs where cs.ContractID = ch.ContractID) " & _
    '" " & _
    ' "update cs set " & _
    '"cs.ComptrollerComments = '" & Replace(txtYourComments.Text, "'", "''") & "' " & _
    '", cs.ComptrollerLogin = u.UserLogin " & _
    '", cs.ComptrollerDate = getdate() " & _
    '", cs.ComptrollerStatus = 1 " & _
    '"from WebFD.VendorContracts.Contract_Status cs " & _
    '"join WebFD.VendorContracts.Users u on UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and u.Active = 1 " & _
    '"where ContractID = " & lblPendingContractID.Text & " and Comptroller = 1 " & _
    '" " & _
    '"update cs set " & _
    '"cs.ComplianceOfficerComments = '" & Replace(txtYourComments.Text, "'", "''") & "' " & _
    '", cs.ComplianceOfficerLogin = u.UserLogin " & _
    '", cs.ComplianceOfficerrDate = getdate() " & _
    '", cs.ComplianceOfficerStatus = 1 " & _
    '"from WebFD.VendorContracts.Contract_Status cs " & _
    '"join WebFD.VendorContracts.Users u on UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and u.Active = 1 " & _
    '"where ContractID = " & lblPendingContractID.Text & " and ComplianceOfficer = 1 " & _
    '" " & _
    '"update cs set " & _
    '"cs.SupplyComments = '" & Replace(txtYourComments.Text, "'", "''") & "' " & _
    '", cs.SupplyLogin = u.UserLogin " & _
    '", cs.SupplyDate = getdate() " & _
    '", cs.SupplyStatus = 1 " & _
    '"from WebFD.VendorContracts.Contract_Status cs " & _
    '"join WebFD.VendorContracts.Users u on UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and u.Active = 1 " & _
    '"where ContractID = " & lblPendingContractID.Text & " and Supply = 1 "

    '        ExecuteSql(AcceptSql)
    '        'SrchGrid()
    '        pnlResultPanel.Visible = False

    '    End Sub

    '    Private Sub btnRejectContract_Click(sender As Object, e As EventArgs) Handles btnRejectContract.Click

    '        Dim AcceptSql As String =
    '"insert into WebFD.VendorContracts.Contract_Status " & _
    '"(ContractID, DirectorStatus, DirectorLogin, DirectorDate, DirectorComments, Active) " & _
    '"select ch.ContractID, 0, d2u.UserLogin, getdate(), '" & Replace(txtYourComments.Text, "'", "''") & "', 1 from WebFD.VendorContracts.ContractHeader ch " & _
    '"join WebFD.VendorContracts.Department_2_User d2u on ch.DepartmentID = d2u.DepartmentID  " & _
    '"and d2u.Active = 1 and d2u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
    '"where ContractID = " & lblPendingContractID.Text & " " & _
    '"and not exists (select * from WebFD.VendorContracts.Contract_Status  cs where cs.ContractID = ch.ContractID) " & _
    '" " & _
    '"update cs set " & _
    '"cs.ComptrollerComments = '" & Replace(txtYourComments.Text, "'", "''") & "' " & _
    '", cs.ComptrollerLogin = u.UserLogin " & _
    '", cs.ComptrollerDate = getdate() " & _
    '", cs.ComptrollerStatus = 0 " & _
    '"from WebFD.VendorContracts.Contract_Status cs " & _
    '"join WebFD.VendorContracts.Users u on UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and u.Active = 1 " & _
    '"where ContractID = " & lblPendingContractID.Text & " and Comptroller = 1 " & _
    '" " & _
    '"update cs set " & _
    '"cs.ComplianceOfficerComments = '" & Replace(txtYourComments.Text, "'", "''") & "' " & _
    '", cs.ComplianceOfficerLogin = u.UserLogin " & _
    '", cs.ComplianceOfficerrDate = getdate() " & _
    '", cs.ComplianceOfficerStatus = 0 " & _
    '"from WebFD.VendorContracts.Contract_Status cs " & _
    '"join WebFD.VendorContracts.Users u on UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and u.Active = 1 " & _
    '"where ContractID = " & lblPendingContractID.Text & " and ComplianceOfficer = 1 " & _
    '" " & _
    '"update cs set " & _
    '"cs.SupplyComments = '" & Replace(txtYourComments.Text, "'", "''") & "' " & _
    '", cs.SupplyLogin = u.UserLogin " & _
    '", cs.SupplyDate = getdate() " & _
    '", cs.SupplyStatus = 0 " & _
    '"from WebFD.VendorContracts.Contract_Status cs " & _
    '"join WebFD.VendorContracts.Users u on UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and u.Active = 1 " & _
    '"where ContractID = " & lblPendingContractID.Text & " and Supply = 1 "

    '        ExecuteSql(AcceptSql)
    '        'SrchGrid()
    '        pnlResultPanel.Visible = False

    '    End Sub

#End Region

    Private Sub lbFullReset_Click(sender As Object, e As EventArgs) Handles lbFullReset.Click


        'Dim AnswerSql As String = " Delete from WebFD.VendorContracts.PendingContractAttachments where ContractID = " & lblContractID.Text & _
        ' " Delete from WebFD.VendorContracts.PendingContractHeader where Active = 1 and UserLogin = '" & _
        ' Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'" & _
        ' " Delete from WebFD.VendorContracts.PendingContract_Answers where Active = 1 and not exists (select * from WebFD.VendorContracts.PendingContractHeader pc where  " & _
        ' "pc.ContractID = WebFD.VendorContracts.PendingContract_Answers.ContractID and pc.Active = 1) " & _
        ' "insert into WebFD.VendorContracts.Users " & _
        ' "select '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', '" & Replace(lblSubRequestor.Text, "'", "''") & "', null, 1, " & _
        ' " getdate(), getdate(), 'Automatic', 0, 0, 0, 0 " & _
        ' "where not exists (select * from WebFD.VendorContracts.Users where UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "') "

        'ExecuteSql(AnswerSql)

        ResetPage()
        'SrchGrid()

    End Sub

    Private Sub ddlSubmitContractParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubmitContractParty.SelectedIndexChanged
        Try
            If ddlSubmitContractParty.SelectedValue = -1 Then
                txtSubmitContractParty.Visible = False
                lblSubmitContractPartyPleaseSpecify.Visible = False
                Exit Sub
            End If

            Dim sql As String = "select TextBoxRequired from WebFD.VendorContracts.ContractParty_LU " & _
                "where ContractPartyID = '" & ddlSubmitContractParty.SelectedValue & "' and Active = 1"

            If GetScalar(sql) > 0 Then
                txtSubmitContractParty.Visible = True
                lblSubmitContractPartyPleaseSpecify.Visible = True
            Else
                txtSubmitContractParty.Visible = False
                lblSubmitContractPartyPleaseSpecify.Visible = False
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSpecialQuestionSubmission_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSpecialQuestionSubmission.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlResponse As DropDownList = e.Row.FindControl("ddlResponse")
            Dim txtResponse As TextBox = e.Row.FindControl("txtResponse")
            Dim rblResponse As RadioButtonList = e.Row.FindControl("rblResponse")
            Dim cbResponse As CheckBox = e.Row.FindControl("cbResponse")

            Dim answerString As String = "Select * from WebFD.VendorContracts.Question_Answer_LU where Active = 1 and QuestionID = '" & e.Row.DataItem("QuestionID") & "' "

            If e.Row.DataItem("Method") = "DropDownList" Then

                ddlResponse.Visible = True
                txtResponse.Visible = False
                rblResponse.Visible = False
                cbResponse.Visible = False

                Dim ds As New DataTable
                ds = GetData(answerString)
                ddlResponse.DataSource = ds.DefaultView
                ddlResponse.DataTextField = "AnswerText"
                ddlResponse.DataValueField = "AnswerValue"
                ddlResponse.DataBind()

                For Each row As DataRow In ds.Rows
                    If row("DefaultAnswer") = 1 Then
                        ddlResponse.SelectedValue = row("AnswerValue")
                        Exit For
                    End If
                Next

                ' 7/10/2017 CRW -- Removed Pending tables
                'If IsDBNull(e.Row.DataItem("Response")) Then
                '    For Each row As DataRow In ds.Rows
                '        If row("DefaultAnswer") = 1 Then
                '            ddlResponse.SelectedValue = row("AnswerValue")
                '            Exit For
                '        End If
                '    Next
                'Else
                '    ddlResponse.SelectedValue = e.Row.DataItem("Response")
                'End If


            ElseIf e.Row.DataItem("Method") = "RadioButton" Then

                ddlResponse.Visible = False
                txtResponse.Visible = False
                rblResponse.Visible = True
                cbResponse.Visible = False

                Dim ds As New DataTable
                ds = GetData(answerString)
                rblResponse.DataSource = ds.DefaultView
                rblResponse.DataTextField = "AnswerValue"
                rblResponse.DataBind()

                'If IsDBNull(e.Row.DataItem("Response")) Then
                For Each row As DataRow In ds.Rows
                    If row("DefaultAnswer") = 1 Then
                        rblResponse.SelectedValue = row("AnswerValue")
                        Exit For
                    End If
                Next
                'Else
                '    rblResponse.SelectedValue = e.Row.DataItem("Response")
                'End If
            ElseIf e.Row.DataItem("Method") = "Checkbox" Then

                ddlResponse.Visible = False
                txtResponse.Visible = False
                rblResponse.Visible = False
                cbResponse.Visible = True

                Dim ds As New DataTable
                ds = GetData(answerString)
                cbResponse.Text = ds.Rows(0)("AnswerValue").ToString
                cbResponse.DataBind()

                If ds.Rows(0)("DefaultAnswer") = 1 Then
                    cbResponse.Checked = True
                End If

            Else

                ddlResponse.Visible = False
                txtResponse.Visible = True
                rblResponse.Visible = False
                cbResponse.Visible = False

            End If



        End If

    End Sub


    Protected Sub rblResponse_SelectedIndexChanged1()
        CheckResponses(gvSpecialQuestionSubmission)
        'Try
        '    Dim UpdatesSql As String = ""

        '    Dim badcnt As Integer = 0

        '    For i As Integer = 0 To gvSpecialQuestionSubmission.Rows.Count - 1

        '        Dim rblResponse As RadioButtonList = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("rblResponse"), RadioButtonList)
        '        Dim txtResponseComment As TextBox = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("txtResponseComment"), TextBox)
        '        Dim lblResponseAsk As Label = CType(gvSpecialQuestionSubmission.Rows(i).FindControl("lblResponseAsk"), Label)

        '        Dim s As String = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(gvSpecialQuestionSubmission.DataKeys(i).Value.ToString, "'", "''") & _
        '            "' and Active = 1 and CommentRequired = 1 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "

        '        If GetScalar(s) > 0 Then
        '            txtResponseComment.Visible = True
        '            lblResponseAsk.Visible = True
        '        Else
        '            txtResponseComment.Visible = False
        '            lblResponseAsk.Visible = False
        '        End If

        '        Dim x As String = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(gvSpecialQuestionSubmission.DataKeys(i).Value.ToString, "'", "''") & _
        '           "' and Active = 1 and RequiredAnswer = 0 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "

        '        If GetScalar(x) > 0 Then
        '            badcnt += 1
        '        End If

        '    Next

        '    If badcnt > 0 Then
        '        pnlSpecialCountFix.Visible = True
        '        lblSpecialCountFix.Text = "You may not submit this contract with the current answer set.  " & badcnt.ToString & " of the answers below must be changed.  "
        '        gvSpecialQuestionSubmission.BorderColor = Drawing.Color.Red
        '    Else
        '        pnlSpecialCountFix.Visible = False
        '        gvSpecialQuestionSubmission.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
        '    End If
        'Catch ex As Exception
        '    LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        'End Try
    End Sub

    Protected Sub cbResponse_CheckedChanged2()
        CheckResponses(gvSpecialQuestionSubmission)
    End Sub

    Protected Sub cbResponse_CheckedChanged1()
        CheckResponses(gvSubmissionQuestions)
    End Sub

    Protected Sub CheckResponses(q As GridView)
        Try
            Dim UpdatesSql As String = ""

            Dim badcnt As Integer = 0

            For i As Integer = 0 To q.Rows.Count - 1

                Dim rblResponse As RadioButtonList = CType(q.Rows(i).FindControl("rblResponse"), RadioButtonList)
                Dim txtResponseComment As TextBox = CType(q.Rows(i).FindControl("txtResponseComment"), TextBox)
                Dim lblResponseAsk As Label = CType(q.Rows(i).FindControl("lblResponseAsk"), Label)
                Dim cbResponse As CheckBox = CType(q.Rows(i).FindControl("cbResponse"), CheckBox)
                Dim txtResponseDate As TextBox = CType(q.Rows(i).FindControl("txtResponseDate"), TextBox)
                Dim lblResponseDatelbl As Label = CType(q.Rows(i).FindControl("lblResponseDatelbl"), Label)
                Dim trResponses As TableRow = CType(q.Rows(i).FindControl("trResponses"), TableRow)
                'Dim asteriskQuestion As Label = CType(q.Rows(i).FindControl("asteriskQuestion"), Label)

                Dim s As String = ""
                Dim x As String = ""
                Dim y As String = ""



                If cbResponse.Visible And cbResponse.Checked Then
                    s = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                        "' and Active = 1 and CommentRequired = 1 and AnswerValue = '" & Replace(cbResponse.Text, "'", "''") & "'  "
                    y = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                        "' and Active = 1 and DateRequired = 1 and AnswerValue = '" & Replace(cbResponse.Text, "'", "''") & "'  "
                    'ElseIf cbResponse.Visible And cbResponse.Checked = False Then
                    '    x = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                    '  "' and Active = 1 and RequiredAnswer = 1 and AnswerValue = '" & Replace(cbResponse.Text, "'", "''") & "' "
                ElseIf rblResponse.Visible And rblResponse.SelectedIndex = -1 Then
                    badcnt += 1

                ElseIf rblResponse.Visible Then
                    s = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                        "' and Active = 1 and CommentRequired = 1 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "
                    y = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                       "' and Active = 1 and DateRequired = 1 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "'  "
                    ' x = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                    '"' and Active = 1 and RequiredAnswer = 0 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "
                Else

                    badcnt += 1
                End If

                If Len(s) > 0 Then
                    If GetScalar(s) > 0 Then
                        txtResponseComment.Visible = True
                        lblResponseAsk.Visible = True
                        trResponses.Visible = True
                    Else
                        txtResponseComment.Visible = False
                        lblResponseAsk.Visible = False
                        trResponses.Visible = False
                    End If
                Else
                    txtResponseComment.Visible = False
                    lblResponseAsk.Visible = False
                    trResponses.Visible = False
                End If

                If Len(y) > 0 Then
                    If GetScalar(y) > 0 Then
                        txtResponseDate.Visible = True
                        lblResponseDatelbl.Visible = True
                        lblResponseAsk.Visible = True
                        trResponses.Visible = True
                    Else
                        lblResponseDatelbl.Visible = False
                        txtResponseDate.Visible = False
                    End If
                Else
                    lblResponseDatelbl.Visible = False
                    txtResponseDate.Visible = False
                End If

                If Len(x) > 0 Then
                    If GetScalar(x) > 0 Then
                        badcnt += 1
                    End If
                End If


            Next

            If q.UniqueID = gvSubmissionQuestions.UniqueID Then
                lblNormalCountFix.Text = badcnt
            Else
                lblSpecialCountFix.Text = badcnt
            End If


            'If badcnt > 0 Then
            '    If q.UniqueID = gvSubmissionQuestions.UniqueID Then
            '        pnlNormalCountFix.Visible = True
            '        lblNormalCountFix.Text = "Response is still required for " & badcnt.ToString & " questions.  "
            '    Else
            '        pnlSpecialCountFix.Visible = True
            '        lblSpecialCountFix.Text = "Response is still required for " & badcnt.ToString & " questions.  "
            '    End If
            '    q.BorderColor = Drawing.Color.Red
            'Else
            '    If q.UniqueID = gvSubmissionQuestions.UniqueID Then
            '        pnlNormalCountFix.Visible = False
            '    Else
            '        pnlSpecialCountFix.Visible = False
            '    End If
            '    q.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
            'End If
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub DoubleCheckResponses(q As GridView)
        Try
            Dim UpdatesSql As String = ""

            Dim badcnt As Integer = 0

            For i As Integer = 0 To q.Rows.Count - 1

                Dim rblResponse As RadioButtonList = CType(q.Rows(i).FindControl("rblResponse"), RadioButtonList)
                Dim txtResponseComment As TextBox = CType(q.Rows(i).FindControl("txtResponseComment"), TextBox)
                Dim lblResponseAsk As Label = CType(q.Rows(i).FindControl("lblResponseAsk"), Label)
                Dim cbResponse As CheckBox = CType(q.Rows(i).FindControl("cbResponse"), CheckBox)
                Dim txtResponseDate As TextBox = CType(q.Rows(i).FindControl("txtResponseDate"), TextBox)
                Dim lblResponseDatelbl As Label = CType(q.Rows(i).FindControl("lblResponseDatelbl"), Label)
                Dim trResponses As TableRow = CType(q.Rows(i).FindControl("trResponses"), TableRow)
                Dim asteriskQuestion As Label = CType(q.Rows(i).FindControl("asteriskQuestion"), Label)

                Dim s As String = ""
                Dim x As String = ""
                Dim y As String = ""

                asteriskQuestion.Text = ""

                If cbResponse.Visible And cbResponse.Checked Then
                    s = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                        "' and Active = 1 and CommentRequired = 1 and AnswerValue = '" & Replace(cbResponse.Text, "'", "''") & "'  "
                    y = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                        "' and Active = 1 and DateRequired = 1 and AnswerValue = '" & Replace(cbResponse.Text, "'", "''") & "'  "
                    'ElseIf cbResponse.Visible And cbResponse.Checked = False Then
                    '    x = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                    '  "' and Active = 1 and RequiredAnswer = 1 and AnswerValue = '" & Replace(cbResponse.Text, "'", "''") & "' "
                ElseIf rblResponse.Visible And rblResponse.SelectedIndex = -1 Then
                    badcnt += 1
                    asteriskQuestion.Text = "*"
                ElseIf rblResponse.Visible Then
                    s = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                        "' and Active = 1 and CommentRequired = 1 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "
                    y = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                                       "' and Active = 1 and DateRequired = 1 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "'  "
                    ' x = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(q.DataKeys(i).Value.ToString, "'", "''") & _
                    '"' and Active = 1 and RequiredAnswer = 0 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "
                Else
                    asteriskQuestion.Text = "*"
                    badcnt += 1
                End If

                If txtResponseComment.Visible = True Then
                    If Trim(txtResponseComment.Text) = "" Then
                        asteriskQuestion.Text = "*"
                    End If
                End If
                If txtResponseDate.Visible = True Then
                    If Trim(txtResponseDate.Text) = "" Then
                        asteriskQuestion.Text = "*"
                    End If
                End If

                If Len(x) > 0 Then
                    If GetScalar(x) > 0 Then
                        badcnt += 1
                    End If
                End If

            Next

            If q.UniqueID = gvSubmissionQuestions.UniqueID Then
                lblNormalCountFix.Text = badcnt
            Else
                lblSpecialCountFix.Text = badcnt
            End If


            'If badcnt > 0 Then
            '    If q.UniqueID = gvSubmissionQuestions.UniqueID Then
            '        pnlNormalCountFix.Visible = True
            '        lblNormalCountFix.Text = "Response is still required for " & badcnt.ToString & " questions.  "
            '    Else
            '        pnlSpecialCountFix.Visible = True
            '        lblSpecialCountFix.Text = "Response is still required for " & badcnt.ToString & " questions.  "
            '    End If
            '    q.BorderColor = Drawing.Color.Red
            'Else
            '    If q.UniqueID = gvSubmissionQuestions.UniqueID Then
            '        pnlNormalCountFix.Visible = False
            '    Else
            '        pnlSpecialCountFix.Visible = False
            '    End If
            '    q.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
            'End If
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub rblResponse_SelectedIndexChanged2()
        CheckResponses(gvSubmissionQuestions)
        'Try
        '    Dim UpdatesSql As String = ""
        '    Dim badcnt As Integer = 0

        '    For i As Integer = 0 To gvSubmissionQuestions.Rows.Count - 1

        '        Dim rblResponse As RadioButtonList = CType(gvSubmissionQuestions.Rows(i).FindControl("rblResponse"), RadioButtonList)
        '        Dim txtResponseComment As TextBox = CType(gvSubmissionQuestions.Rows(i).FindControl("txtResponseComment"), TextBox)
        '        Dim lblResponseAsk As Label = CType(gvSubmissionQuestions.Rows(i).FindControl("lblResponseAsk"), Label)

        '        Dim s As String = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(gvSubmissionQuestions.DataKeys(i).Value.ToString, "'", "''") & _
        '            "' and Active = 1 and CommentRequired = 1 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "

        '        If GetScalar(s) > 0 Then
        '            txtResponseComment.Visible = True
        '            lblResponseAsk.Visible = True
        '        Else
        '            txtResponseComment.Visible = False
        '            lblResponseAsk.Visible = False
        '        End If

        '        Dim x As String = "Select isnull(count(*), 0) from WebFD.VendorContracts.Question_Answer_LU where QuestionID = '" & Replace(gvSubmissionQuestions.DataKeys(i).Value.ToString, "'", "''") & _
        '           "' and Active = 1 and RequiredAnswer = 0 and AnswerValue = '" & Replace(rblResponse.SelectedValue, "'", "''") & "' "

        '        If GetScalar(x) > 0 Then
        '            badcnt += 1
        '        End If

        '    Next

        '    If badcnt > 0 Then
        '        pnlNormalCountFix.Visible = True
        '        lblNormalCountFix.Text = "You may not submit this contract with the current answer set.  " & badcnt.ToString & " of the answers below must be changed.  "
        '        gvSubmissionQuestions.BorderColor = Drawing.Color.Red
        '    Else
        '        pnlNormalCountFix.Visible = False
        '        gvSubmissionQuestions.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
        '    End If
        'Catch ex As Exception
        '    LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        'End Try
    End Sub


    Private Sub btnCancelPrint_Click(sender As Object, e As EventArgs) Handles btnCancelPrint.Click
        pnlPrinting.Visible = False
        pnlSubmitting.Visible = True
    End Sub

    Private Sub btnConfirmContract_Click(sender As Object, e As EventArgs) Handles btnConfirmContract.Click
        Try

            Dim insertsql As String = "Insert WebFD.VendorContracts.ContractHeader " & _
                " output inserted.ContractID " & _
                " select (select isnull(max(ContractID), 0) + 1 from WebFD.VendorContracts.ContractHeader), '" & _
                 Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & _
                 "', '" & Replace(txtSubmitContractName.Text, "'", "''") & "', null, null, null, '" & Replace(ddlDepartmentNo.SelectedValue.ToString, "'", "''") & _
                "', '" & Replace(txtSubmitVendorName.Text, "'", "''") & "', '" & Replace(ddlSubContractType.SelectedValue.ToString, "'", "''") & _
                "', '" & Replace(txtSubmitContractLength.Text, "'", "''") & "', '" & Replace(ddlSubmitContractCost.SelectedValue, "'", "''") & "', '" & Replace(rblSubAutoRenewal.SelectedValue, "'", "''") & "',"

            If txtSubRenewalTerm.Enabled = True Then
                insertsql += "'" & Replace(txtSubRenewalTerm.Text, "'", "''") & "', "
            Else
                insertsql += "null, "
            End If

            insertsql += "'" & Replace(txtSubmitContractPurpose.Text, "'", "''") & "', '" & Replace(ddlSubmitContractParty.SelectedValue, "'", "''") & "', "

            If txtSubmitContractParty.Enabled = True Then
                insertsql += "'" & Replace(txtSubmitContractParty.Text, "'", "''") & "', "
            Else
                insertsql += "null, "
            End If

            insertsql += " '" & Replace(ddlSubmitContractBudgetAcct.SelectedValue, "'", "''") & "', 1, getdate(), getdate(), '" & _
                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' "

            Dim x As Integer = GetScalar(insertsql)

            Dim Furthersql As String = "insert into WebFD.VendorContracts.ContractAttachments select '" & x & "', FileName, ContentType, Content, UserID, DateAdded, Active " & _
                "from WebFD.VendorContracts.PendingContractAttachments where ContractID = '" & AttachIndex.Text & "' and Active = 1 and UserID = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'  " & _
                " Insert into WebFD.VendorContracts.Contract_Answers select '" & x & "', QuestionID, Response, Active, DateAdded, DateModified, ModifiedBy, AnswerComment, AnswerDate " & _
                "from WebFD.VendorContracts.PendingContract_Answers where ContractID = '" & AttachIndex.Text & "' and ModifiedBy = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and Active = 1 " & _
                "Insert into WebFD.VendorContracts.Contract_Comments values ('" & x & "', 'Submitter', '" & Replace(txtSubmissionComments.Text, "'", "''") & "', 'Submission', 0, getdate(), '" & _
                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 1) " & _
                        "insert into WebFD.VendorContracts.Contract_Approvals " & _
                "select ch.ContractID, 'Default Insert', 'Approved', 'VP Override', getdate(), 'Default Insert', 1, 'Director' /*, ch.AnnualContractExpense */" & _
                "from WebFD.VendorContracts.ContractHeader ch " & _
                "join WebFD.VendorContracts.Department_2_User d2u on ch.DepartmentID = isnull(d2u.DepartmentID, ch.DepartmentID) and d2u.Active =1 " & _
                "join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort and r.Active = 1 " & _
                "join WebFD.VendorContracts.ApprovalReq_LU aq on aq.Active = 1  " & _
                "	and (r.Alias like ApprovalRequired or r.RoleFull = aq.ApprovalRequired) and  " & _
                "	ch.DateAdded between isnull(aq.EffectiveFrom, '1/1/1800') and isnull(aq.EffectiveTo, '12/31/9999') " & _
                "	and ch.AnnualContractExpense =   " & _
                "	case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)   " & _
                "	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)    " & _
                "	 else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'   " & _
                "	 + left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)   end " & _
                "where d2u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & " ' and ch.Active = 1 and d2u.Position = 'VP' and ContractID = '" & x & "' " & _
                "and not exists (select * from WebFD.VendorContracts.Contract_Approvals ca  " & _
                "		where ca.ContractID = ch.ContractID and ca.Active = 1) " & _
                "and exists (select * from WebFD.VendorContracts.ApprovalReq_LU arq  " & _
                "			join WebFD.VendorContracts.Roles r2 on r2.Active = 1 and (r2.Alias like arq.ApprovalRequired or r2.RoleFull = arq.ApprovalRequired)  " & _
                "			where arq.Active = 1 and r2.Hierarchy < r.Hierarchy and ch.DateAdded between isnull(arq.EffectiveFrom, '1/1/1800') and isnull(arq.EffectiveTo, '12/31/9999') " & _
                "			and ch.AnnualContractExpense =   " & _
                "			case when arq.LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(arq.UpperBound, 0) , 1), len(convert(varchar, round(arq.UpperBound, 0) , 1)) - 3)   " & _
                "			when arq.UpperBound is null then 'Greater Than $' + left(convert(varchar, round(arq.LowerBound, 0) , 1), len(convert(varchar, round(arq.LowerBound, 0) , 1)) - 3)    " & _
                "			 else '$' + left(convert(varchar, arq.LowerBound , 1), len(convert(varchar, arq.LowerBound, 1)) - 3) + ' - $'   " & _
                "			 + left(convert(varchar, arq.UpperBound , 1), len(convert(varchar, arq.UpperBound, 1)) - 3)   end ) " & _
                "			  " & _
                "insert into WebFD.VendorContracts.Contract_Approvals " & _
                "select ch.ContractID, RequesterUserLogin, 'Approved', 'Requestor', getdate(), 'Default Insert', 1,  RoleFull " & _
                "from WebFD.VendorContracts.ContractHeader ch " & _
                "join WebFD.VendorContracts.Department_2_User d2u on ch.DepartmentID = isnull(d2u.DepartmentID, ch.DepartmentID) and d2u.Active =1 " & _
                "join WebFD.VendorContracts.Roles r on d2u.Position = RoleShort and r.Active = 1 " & _
                "where d2u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' and ch.Active = 1 and ContractID = '" & x & "' " & _
                "and not exists (select * from WebFD.VendorContracts.Contract_Approvals ca  " & _
                "		where ca.ContractID = ch.ContractID and ca.Active = 1 and ca.UserLogin <> 'Default Insert' ) "



            ExecuteSql(Furthersql)

            Dim BonusSql As String = "Insert into WebFD.VendorContracts.EmailStatus values (" & x & ", 'PendingSubmission', 1, null) "
            ExecuteSql(BonusSql)

            'Dim BonusSql As String = "if OBJECT_ID('tempdb..#MainEmails') is not null " & _
            '    "              begin  " & _
            '    "                 drop table #MainEmails " & _
            '    "              End  " & _
            '    "			   " & _
            '    "declare @ContractID int = " & x & _
            '    "select ch.ContractID, RequesterUserLogin, u.UserEmail as EmailAddress " & _
            '    ", isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as UserName , ch.ContractName " & _
            '    ", ch.ContractPurpose, ch.VendorName, ch.DateAdded " & _
            '    ", isnull(isnull(convert(varchar, d.DepartmentNo) + ' -- ', '') + d.DepartmentName, ch.DepartmentID) as Department " & _
            '    "into #MainEmails " & _
            '    "from WebFD.VendorContracts.ContractHeader ch " & _
            '    "left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin " & _
            '    "left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            '    "where ch.Active = 1 and ch.ContractID = @ContractID and UserEmail is not null " & _
            '    "           " & _
            '    "declare @Body nvarchar(max) = (select UserName from #MainEmails where ContractID = @ContractID) + '<br><br><b>Thank you for submitting your contract to LESCOR.  </b><br> " & _
            '    "<br> You can view the progress of your contract on the ""Browse Contracts"" tab, by searching for Request ID: ' + convert(varchar, @ContractID) + ' <br><br><br> <b> Contract Details: </b>' +  " & _
            '    "'<table border=""1"" width=""80%""> " & _
            '    "<tr> ' + ' <th align=""Left""> Date Submitted </th> <td> ' + isnull((select convert(varchar, DateAdded, 107) from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>' " & _
            '    "+ '<tr><th align=""Left""> Contract Name </th> <td> ' + isnull((select ContractName from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'   " & _
            '    "+ '<tr><th align=""Left""> Department </th> <td> ' + isnull((select Department from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'   " & _
            '    "+ '<tr><th align=""Left""> Contract Purpose </th> <td> ' + isnull((select ContractPurpose from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'  " & _
            '    "+ '<tr><th align=""Left""> VendorName </th> <td> ' + isnull((select VendorName from #MainEmails where ContractID = @ContractID), '') + '</td> </tr> </table> <br><br> <i> Automated Email; Please do not respond </i>'  " & _
            '    " " & _
            '    "declare @OwnerEmail varchar(max) = (select EmailAddress from #MainEmails where ContractID = @ContractID) " & _
            '    " " & _
            '    "  if @OwnerEmail is not null " & _
            '    " begin " & _
            '    "exec msdb.dbo.sp_send_dbmail  " & _
            '    "@from_address = 'financeweb@northside.com',   " & _
            '    "/*@reply_to = 'Chelsea.Weirich@northside.com',   */" & _
            '    "@recipients = @OwnerEmail ,  " & _
            '    "/*--@recipients = 'satanbarbie@gmail.com' ,     When testing change recipients to single person  */  " & _
            '    "@subject = 'LESCOR Contract Submitted',  " & _
            '    "@body =  @Body, " & _
            '    "@body_format = 'HTML'  " & _
            '    " end "

            'ExecuteSql(BonusSql)

            pnlPrinting.Visible = False
            pnlSubmitting.Visible = True
            ResetPage()
            VendorContracts.ActiveTab = tpOpenVendor
            tpSubmitContract.Visible = False
            ddlDepartmentNo.SelectedIndex = -1
            FlipCBVis(True, False)
            Search()
            ExplanationLabelWelcome.Text = "Your contract has been submitted for approval.  <br>  This Contract ID # is " & x.ToString & "<br> Please retain for reference"
            mpeWelcomePage.Show()
        Catch ex As Exception
            ExplanationLabelWelcome.Text = "There was an error submitting your contract.  Please contact the Web Admin (" & WebAdminEmail & ")"
            mpeWelcomePage.Show()
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvPrintAttachments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPrintAttachments.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then

        '    If Left(e.Row.Cells(1).Text, 5) = "image" Then
        '        Try

        '            Dim bytes As Byte() = TryCast(TryCast(e.Row.DataItem, DataRowView)("Content"), Byte())

        '            Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)

        '            TryCast(e.Row.FindControl("Image1"), Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String

        '        Catch ex As Exception

        '            Dim img As Image = e.Row.FindControl("Image1")
        '            img.Visible = False

        '        End Try
        '    Else
        '        Dim img As Image = e.Row.FindControl("Image1")
        '        img.Visible = False
        '    End If

        'End If
    End Sub

    Private Sub FlipCBVis(y As Boolean, x As Boolean)
        trCBVis1.Visible = y
        trCBUnVis1.Visible = Not y
        trCBVis2.Visible = x
        trCBVis3.Visible = x
        trCBVis4.Visible = x
        trCBVis5.Visible = x
        trCBVis6.Visible = x
        trCBVis7.Visible = x
    End Sub

    Private Sub cbAgreements_CheckedChanged(sender As Object, e As EventArgs) Handles cbAgreements.CheckedChanged
        If cbAgreements.Checked Then

            If ddlDepartmentNo.SelectedIndex < 1 Then
                tpSubmitContract.Visible = False
                'tpLegalTab.Visible = False
                FlipCBVis(True, False)
            Else
                FlipCBVis(True, True)
                tpSubmitContract.Visible = True
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnUpload)
                'ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(btnUpload)
                'ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnUpload)
                'loadgrid()
            End If
            LoadWaitingYourApprovalGrid()
            LoadRecentlyReviewedGrid()

            If GetScalar("select count(*) from WebFD.VendorContracts.Users u " & _
                "join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1 " & _
                "join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
                "where u.Active = 1 and u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' " & _
                "and ( r.RoleFull  in (select ApprovalRequired from WebFD.VendorContracts.ApprovalReq_LU where Active = 1) " & _
                "	or r.Alias in (select ApprovalRequired from WebFD.VendorContracts.ApprovalReq_LU where Active = 1)) "
                ) > 0 Then
                tpPendingContracts.Visible = True
            End If
            tpLegalTab.Visible = True
            LegalRowing()
            AttachmentDownloadRowing(gvLegalAttachments)
        Else
            FlipCBVis(False, False)
            tpSubmitContract.Visible = False
            tpLegalTab.Visible = False
            tpPendingContracts.Visible = False
        End If
    End Sub

    Private Sub gvWaitingonYou_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvWaitingonYou.PageIndexChanging
        Try

            gvWaitingonYou.PageIndex = e.NewPageIndex
            gvWaitingonYou.DataSource = ApprovalView()
            gvWaitingonYou.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvWaitingonYou_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvWaitingonYou.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvWaitingonYou_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvWaitingonYou.SelectedIndexChanged

        Dim x As String = gvWaitingonYou.DataKeys(gvWaitingonYou.SelectedIndex).Value.ToString

        For Each canoe As GridViewRow In gvRecentlyReceivedContracts.Rows
            If canoe.Cells(6).Text = "Deleted" Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                canoe.BorderStyle = BorderStyle.NotSet
                canoe.BorderWidth = "1"
            Else
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                canoe.BorderStyle = BorderStyle.NotSet
                canoe.BorderWidth = "1"
            End If

        Next

        For Each canoe As GridViewRow In gvWaitingonYou.Rows
            If canoe.RowIndex = gvWaitingonYou.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor)
                canoe.BorderStyle = BorderStyle.Solid
                canoe.BorderWidth = "3"
            Else
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor)
                canoe.BorderStyle = BorderStyle.NotSet
                canoe.BorderWidth = "1"
            End If
        Next

        AttachmentGrid(gvApprovalAttachments, x)

        gvApprovalCurrent.DataSource = GetData("select isnull(isnull(u.UserDisplayName, u.UserFullName), ca.UserLogin) as UserName " & _
                ", Status, Comment, ca.ModifyDate " & _
                "from WebFD.VendorContracts.Contract_Approvals ca " & _
                "left join WebFD.VendorContracts.Users u on ca.UserLogin = u.UserLogin and u.Active = 1 " & _
                "where ca.ContractID = '" & x & "' and ca.Active = 1 and ca.UserLogin <> 'Default Insert'")

        gvApprovalCurrent.DataBind()

        Dim y As DataTable = GetData("select isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as Requestor , ch.* " & _
             ", convert(varchar, DepartmentNo) + ' - ' + DepartmentName as Department, ContractType, cp.ContractParty,  convert(varchar, Acct) + ' - ' + Description as ExpenseDescrip, Comment " & _
            "from WebFD.VendorContracts.ContractHeader ch " & _
            "left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            "left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin and u.Active = 1 " & _
            "left join WebFD.VendorContracts.ContractType_LU cl on cl.ContractTypeID = ch.ContractTypeID and cl.Active = 1 " & _
            "left join WebFD.VendorContracts.ContractParty_LU cp on ch.ContractingParty = cp.ContractPartyID and cp.Active = 1 " & _
            "left join DWH.Axiom.Acct a on a.Acct = ch.ExpenseAccount and ISMAP  in ('SUP', 'FEE', 'OTH') " & _
            "left join WebFD.VendorContracts.Contract_Comments  cc on cc.ContractID = ch.ContractID and cc.CommentType = 'Submission' and cc.Active = 1 and LegalOnly = 0 " & _
            " where ch.Active = 1 and ch.ContractID = '" & x & "' ")


        If IsDBNull(y(0)("ContractID")) Then
            lblApproveContractID.Text = ""
        Else
            lblApproveContractID.Text = y(0)("ContractID")
        End If

        If IsDBNull(y(0)("Requestor")) Then
            lblApproveRequestor.Text = ""
        Else
            lblApproveRequestor.Text = y(0)("Requestor")
        End If

        If IsDBNull(y(0)("ContractName")) Then
            lblApproveContractName.Text = ""
        Else
            lblApproveContractName.Text = y(0)("ContractName")
        End If

        If IsDBNull(y(0)("Department")) Then
            lblApproveDepartmentNo.Text = ""
        Else
            lblApproveDepartmentNo.Text = (y(0)("Department"))
        End If

        If IsDBNull(y(0)("VendorName")) Then
            lblApproveVendorName.Text = ""
        Else
            lblApproveVendorName.Text = (y(0)("VendorName"))
        End If

        If IsDBNull(y(0)("ContractType")) Then
            lblApproveContractType.Text = ""
        Else
            lblApproveContractType.Text = (y(0)("ContractType"))
        End If

        If IsDBNull(y(0)("ContractLength")) Then
            lblApproveContractLength.Text = ""
        Else
            lblApproveContractLength.Text = (y(0)("ContractLength"))
        End If

        If IsDBNull(y(0)("AnnualContractExpense")) Then
            lblApproveContractCost.Text = ""
        Else
            lblApproveContractCost.Text = (y(0)("AnnualContractExpense"))
        End If

        If IsDBNull(y(0)("AutoRenewal")) Then
            lblApproveAutoRenewal.Text = ""
        Else
            lblApproveAutoRenewal.Text = (y(0)("AutoRenewal"))
        End If

        If IsDBNull(y(0)("RenewalTerm")) Then
            lblApproveRenewalTerm.Text = ""
        Else
            lblApproveRenewalTerm.Text = (y(0)("RenewalTerm"))
        End If

        If IsDBNull(y(0)("ContractPurpose")) Then
            lblApproveContractPurpose.Text = ""
        Else
            lblApproveContractPurpose.Text = (y(0)("ContractPurpose"))
        End If

        If IsDBNull(y(0)("ContractParty")) Then
            lblApproveContractParty.Text = ""
        Else
            lblApproveContractParty.Text = (y(0)("ContractParty"))
        End If

        If IsDBNull(y(0)("ContractingPartyDetail")) Then
            lblApproveContractPartyExplain.Text = ""
        Else
            lblApproveContractPartyExplain.Text = (y(0)("ContractingPartyDetail"))
        End If

        If IsDBNull(y(0)("ExpenseDescrip")) Then
            lblApproveContractBudgetAcct.Text = ""
        Else
            lblApproveContractBudgetAcct.Text = (y(0)("ExpenseDescrip"))
        End If

        If IsDBNull(y(0)("DateAdded")) Then
            lblApproveDate.Text = ""
        Else
            lblApproveDate.Text = (y(0)("DateAdded"))
        End If

        If IsDBNull(y(0)("Comment")) Then
            lblApproveRequestorComments.Text = ""
        Else
            lblApproveRequestorComments.Text = (y(0)("Comment"))
        End If

        loadApprovalgrid(x)
        pnlApproval.Visible = True
        pnlSubApproval.Visible = True

    End Sub

    Private Sub gvWaitingonYou_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvWaitingonYou.Sorting

        Dim dv As DataView
        Dim sorts As String
        dv = ApprovalView()

        sorts = e.SortExpression

        If e.SortExpression = Approvalmap.Text Then

            If Approvaldir.Text = "1" Then
                dv.Sort = sorts + " " + "desc"
                Approvaldir.Text = 0
            Else
                dv.Sort = sorts + " " + "asc"
                Approvaldir.Text = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            Approvaldir.Text = 1
            Approvalmap.Text = e.SortExpression
        End If

        gvWaitingonYou.DataSource = dv
        gvWaitingonYou.DataBind()

    End Sub

    Private Sub btnApproveContract_Click(sender As Object, e As EventArgs) Handles btnApproveContract.Click
        Try
            ExplanationLabelApproval.Text = "You are trying to approve Request ID " & Replace(lblApproveContractID.Text, "'", "''") & ", titled '" & _
                GetString("select ContractName from WebFD.VendorContracts.ContractHeader where Active = 1 and ContractID = '" & Replace(lblApproveContractID.Text, "'", "''") & "'") & "' <br>  Please confirm."
            hiddenRejectorApprove.Text = "Approve"
            mpeApprovalPage.Show()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnRejectContract_Click(sender As Object, e As EventArgs) Handles btnRejectContract.Click
        Try
            ExplanationLabelApproval.Text = "You are trying to reject Request ID " & Replace(lblApproveContractID.Text, "'", "''") & ", titled '" & _
                GetString("select ContractName from WebFD.VendorContracts.ContractHeader where Active = 1 and ContractID = '" & Replace(lblApproveContractID.Text, "'", "''") & "'") & "' <br>  Please confirm."
            hiddenRejectorApprove.Text = "Reject"
            mpeApprovalPage.Show()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Class SqlQuerySet


    End Class

    Private Function ContractSearch()

        'Dim x As String = "	select * from ( " & _
        '        "	select ch.*, isnull(LegalContractAlias, ContractName) as LegalName " & _
        '        "	, case when Pnding.ContractID is not null then 'Pending' else isnull(Rejection, 'Approved') end as UserStatus   " & _
        '        "	, isnull(pl.LegalPriorityShortDescription, pl.LegalPriorityLongDescription) as LegalPriority, pl.LegalPriorityID " & _
        '        "   , cstat.LegalStatusLongDescription as LegalFullStatus " & _
        '        "	, isnull(cstat.LegalStatusShortDescription, cstat.LegalStatusLongDescription) as LegalStatus, cstat.LegalStatusID " & _
        '        "	, d.DepartmentNo as DepartmentNo, convert(varchar, DepartmentNo) + ' - ' + DepartmentName as DepartmentFull " & _
        '        "	, isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as Requestor " & _
        '        "	, isnull(isnull(lu.UserDisplayName, lu.UserFullName), ch.LegalUserAssigned) as LegalUserName " & _
        '        "   , isnull(LegalDeadline, dead.AnswerDate) as Deadline, LegalDeadline, dead.AnswerDate as UserRequestedDeadline " & _
        '        "   , case when LegalDeadline is null and dead.AnswerDate is not null then 'True' else 'False' end as DeadlineColor " & _
        '        "   , convert(varchar, DepartmentNo) + ' - ' + DepartmentName as Department, ContractType, cp.ContractParty,  convert(varchar, Acct) + ' - ' + Description as ExpenseDescrip, Comment " & _
        '        "	from WebFD.VendorContracts.ContractHeader ch " & _
        '        "	left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin and u.Active = 1 " & _
        '        "	left join WebFD.VendorContracts.Users lu on ch.LegalUserAssigned = lu.UserLogin and lu.Active = 1 " & _
        '        "   left join WebFD.VendorContracts.ContractType_LU cl on cl.ContractTypeID = ch.ContractTypeID and cl.Active = 1 " & _
        '        "   left join WebFD.VendorContracts.ContractParty_LU cp on ch.ContractingParty = cp.ContractPartyID and cp.Active = 1 " & _
        '        "   left join DWH.Axiom.Acct a on a.Acct = ch.ExpenseAccount and ISMAP  in ('SUP', 'FEE', 'OTH')  " & _
        '        "   left join WebFD.VendorContracts.Contract_Comments  cc on cc.ContractID = ch.ContractID and cc.CommentType = 'Submission' and cc.Active = 1 and LegalOnly = 0 " & _
        '        "	left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
        '        "	left join WebFD.VendorContracts.Contract_Status cs on ch.ContractID = cs.ContractID and cs.Active = 1 " & _
        '        "   left join WebFD.VendorContracts.Contract_Answers dead on dead.QuestionID = 17 and dead.Active = 1 and dead.ContractID = ch.ContractID " & _
        '        "	left join WebFD.VendorContracts.ContractPriority_LU pl on (cs.LegalPriority = pl.LegalPriorityID  " & _
        '        "		or (cs.LegalPriority is null and pl.DefaultInd = 1)) " & _
        '        "	left join WebFD.VendorContracts.ContractStatus_LU cstat on (cs.LegalStatus = cstat.LegalStatusID  " & _
        '        "		or (cs.LegalStatus is null and cstat.DefaultInd = 1)) " & _
        '        "		left join (select ContractID, Max(Status) as Rejection  " & _
        '        "				from WebFD.VendorContracts.Contract_Approvals where Active = 1 group by ContractID ) rjct  " & _
        '        "				on ch.ContractID = rjct.ContractID and rjct.Rejection = 'Rejected' " & _
        '        "		left join (  select distinct ContractID from (  " & _
        '        "           select ContractID, DepartmentID, ContractName, RequesterUserLogin, isnull(u.UserDisplayName, u.UserFullName) as Requestor,  " & _
        '        "             ch.AnnualContractExpense, ch.DateAdded, isnull(r.RoleFull, ApprovalRequired) SRCH , r2.Hierarchy  " & _
        '        "           from WebFD.VendorContracts.ContractHeader ch  " & _
        '        "            left join WebFD.VendorContracts.Users u on u.UserLogin = ch.RequesterUserLogin  " & _
        '        "           left join WebFD.VendorContracts.ApprovalReq_LU arq   " & _
        '        "            	on arq.Active = 1 and ch.DateAdded between isnull(arq.EffectiveFrom, '1/1/1800') and isnull(arq.EffectiveTo, '12/31/9999') and ch.AnnualContractExpense =   " & _
        '        "            	case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)   " & _
        '        "                  	when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)    " & _
        '        "                    else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'     " & _
        '        "                     + left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)    " & _
        '        "                     end   " & _
        '        "            left join WebFD.VendorContracts.Roles r on r.Alias like arq.ApprovalRequired and r.Active = 1  " & _
        '        "            join WebFD.VendorContracts.Roles r2 on isnull(r.RoleFull, ApprovalRequired) = r2.RoleFull  " & _
        '        "            where ch.Active = 1 and not exists (  " & _
        '        "            	select * from WebFD.VendorContracts.Contract_Approvals ca   " & _
        '        "            	join WebFD.VendorContracts.ContractHeader c on ca.ContractID = c.ContractID and c.Active = 1  " & _
        '        "            	left join WebFD.VendorContracts.Department_2_User d2u on c.DepartmentID = isnull(d2u.DepartmentID, c.DepartmentID) and ca.UserLogin = d2u.UserLogin and d2u.Active = 1  " & _
        '        "            	left join WebFD.VendorContracts.Roles r3 on d2u.Position = r3.RoleShort and r3.Active = 1  " & _
        '        "            	where ca.Active =1 and ca.Status = 'Approved' " & _
        '        "            	and (r2.RoleFull= isnull(ca.RoleFullOnSubmit, r3.RoleFull) or r2.Hierarchy < r3.Hierarchy  or arq.ApprovalRequired = r3.Alias) and ca.ContractID = ch.ContractID)  " & _
        '        "               and not exists (  " & _
        '        "            	select * from WebFD.VendorContracts.Contract_Approvals ca   " & _
        '        "            	where ca.ContractID = ch.ContractID and ca.Active =1 and ca.Status = 'Rejected')  " & _
        '        "            	) a  " & _
        '        "            left join (select u.UserLogin, isnull(u.UserDisplayName, u.UserFullName) as UserDisplayName   " & _
        '        "            , d.DepartmentID, DepartmentNo, DepartmentDisplayName, DepartmentName , d2u.Position, r.RoleFull   " & _
        '        "            from WebFD.VendorContracts.Users u  " & _
        '        "            join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1  " & _
        '        "            join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1  " & _
        '        "            join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort  " & _
        '        "            where u.Active = 1 ) assign on a.SRCH = isnull(assign.RoleFull, assign.Position) and a.DepartmentID = assign.DepartmentID  " & _
        '        "         ) Pnding on Pnding.ContractID = ch.ContractID " & _
        '        "		 ) x  where 1  = 1 "

        ' Revised 5/31/2018 CRW for additional data requested by Ryan

        Dim a As String = "select distinct x.ContractID, x.DateAdded, x.ContractName, x.DepartmentID, d.DepartmentNo, d.DepartmentName  " & _
    "            , Requestor, isnull(r2.RoleFull, SRCH) as SRCH, isnull(ApprovalSeeking,  isnull(u.UserDisplayName, u.UserFullName)) as ApprovalSeeking  " & _
    "            , isnull(ApprovalSeekLogin, u.UserLogin) as ApprovalSeekLogin, RNK  " & _
    "			into #TempContractPull " & _
    "            from ( " & _
    " select ContractID, DateAdded, ContractName, a.DepartmentID, DepartmentNo, isnull(DepartmentDisplayName, DepartmentName) as DepartmentName   " & _
    "            , Requestor, SRCH, UserDisplayName as ApprovalSeeking , UserLogin as ApprovalSeekLogin   " & _
    "            , DENSE_RANK() over (partition by ContractID order by Hierarchy asc) RNK  " & _
                                    " " & _
    "			from (   " & _
    "            select ContractID, DepartmentID, ContractName, RequesterUserLogin, isnull(u.UserDisplayName, u.UserFullName) as Requestor,   " & _
    "                ch.AnnualContractExpense, ch.DateAdded, isnull(r.RoleFull, ApprovalRequired) SRCH , r2.Hierarchy   " & _
    "            from WebFD.VendorContracts.ContractHeader ch   " & _
    "            left join WebFD.VendorContracts.Users u on u.UserLogin = ch.RequesterUserLogin and isnull(u.IgnoreUser, 0) = 0  " & _
    "            left join WebFD.VendorContracts.ApprovalReq_LU arq    " & _
    "                on arq.Active = 1 and ch.DateAdded between isnull(arq.EffectiveFrom, '1/1/1800') and isnull(arq.EffectiveTo, '12/31/9999') and ch.AnnualContractExpense =    " & _
    "                case when LowerBound is null then 'Less Than or Equal to $' + left(convert(varchar, round(UpperBound, 0) , 1), len(convert(varchar, round(UpperBound, 0) , 1)) - 3)    " & _
    "                    when UpperBound is null then 'Greater Than $' + left(convert(varchar, round(LowerBound, 0) , 1), len(convert(varchar, round(LowerBound, 0) , 1)) - 3)     " & _
    "                    else '$' + left(convert(varchar, LowerBound , 1), len(convert(varchar, LowerBound, 1)) - 3) + ' - $'      " & _
    "                        + left(convert(varchar, UpperBound , 1), len(convert(varchar, UpperBound, 1)) - 3)     " & _
    "                        end    " & _
    "            left join WebFD.VendorContracts.Roles r on r.Alias like arq.ApprovalRequired and r.Active = 1   " & _
    "            join WebFD.VendorContracts.Roles r2 on isnull(r.RoleFull, ApprovalRequired) = r2.RoleFull   " & _
    "            where ch.Active = 1 and not exists (   " & _
    "            select * from WebFD.VendorContracts.Contract_Approvals ca    " & _
    "            join WebFD.VendorContracts.ContractHeader c on ca.ContractID = c.ContractID and c.Active = 1   " & _
    "                left join WebFD.VendorContracts.Department_2_User d2u on c.DepartmentID = isnull(d2u.DepartmentID, c.DepartmentID) and ca.UserLogin = d2u.UserLogin and d2u.Active = 1   " & _
    "                left join WebFD.VendorContracts.Roles r3 on d2u.Position = r3.RoleShort and r3.Active = 1   " & _
    "                where ca.Active =1 and ca.Status = 'Approved'  " & _
    "                and (r2.RoleFull = isnull(ca.RoleFullOnSubmit, r3.RoleFull) or r3.Hierarchy > r2.Hierarchy or arq.ApprovalRequired = r3.Alias) and ca.ContractID = ch.ContractID)   " & _
    "                and not exists (   " & _
    "                select * from WebFD.VendorContracts.Contract_Approvals ca    " & _
    "                where ca.ContractID = ch.ContractID and ca.Active =1 and ca.Status = 'Deleted')   " & _
    "                ) a   " & _
    "            left join (select u.UserLogin, isnull(u.UserDisplayName, u.UserFullName) as UserDisplayName    " & _
    "            , d.DepartmentID, DepartmentNo, DepartmentDisplayName, DepartmentName , d2u.Position, r.RoleFull    " & _
    "            from WebFD.VendorContracts.Users u   " & _
    "            join WebFD.VendorContracts.Department_2_User d2u on u.UserLogin = d2u.UserLogin and d2u.Active = 1   " & _
    "            join WebFD.VendorContracts.Department_LU d on isnull(d2u.DepartmentID, d.DepartmentID) = d.DepartmentID and d.Active = 1   " & _
    "            join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort   " & _
    "            where u.Active = 1 and isnull(u.IgnoreUser, 0) = 0 ) assign on a.SRCH = isnull(assign.RoleFull, assign.Position) and a.DepartmentID = assign.DepartmentID   " & _
    "            ) x    " & _
    "            join WebFD.VendorContracts.Department_LU d on x.DepartmentID = d.DepartmentID and d.Active = 1   " & _
    "            left join WebFD.VendorContracts.Roles r on x.SRCH = r.RoleFull and x.ApprovalSeekLogin is null and r.Active = 1  " & _
    "            left join WebFD.VendorContracts.Roles r2 on r2.Active =1 and r2.Hierarchy > r.Hierarchy and not exists  " & _
    "            	(select * from WebFD.VendorContracts.Roles r3 join WebFD.VendorContracts.Department_2_User d2u on r3.RoleShort = d2u.Position   " & _
    "            			and isnull(d2u.DepartmentID, x.DepartmentID) = x.DepartmentID and d2u.Active = 1  " & _
    "            		where r3.Hierarchy < r2.Hierarchy and r3.Hierarchy > r.Hierarchy and r3.Active = 1 )  " & _
    "            		and exists (select * from WebFD.VendorContracts.Department_2_User d join WebFD.VendorContracts.Users ur on ur.UserLogin = d.UserLogin and ur.Active= 1  " & _
    "            			where isnull(d.DepartmentID, x.DepartmentID) = x.DepartmentID   " & _
    "            		and d.Active = 1 and d.Position = r2.RoleShort)  " & _
    "            left join WebFD.VendorContracts.Department_2_User dx on isnull(dx.DepartmentID, x.DepartmentID) = x.DepartmentID   " & _
    "            		and dx.Active = 1 and dx.Position = r2.RoleShort  " & _
    "            left join WebFD.VendorContracts.Users u on u.UserLogin = dx.UserLogin and u.Active = 1 and isnull(u.IgnoreUser, 0) = 0  " & _
    "            where RNK = 1   "

        Dim x As String =
  "	select * from (  " & _
                      "select ch.*, isnull(LegalContractAlias, ContractName) as LegalName  " & _
  "                	, case when Pnding.ContractID is not null then 'Pending' when rjct.Rejection = 'Deleted' then 'Deleted' else 'Approved' end as UserStatus    " & _
  "                	, isnull(pl.LegalPriorityShortDescription, pl.LegalPriorityLongDescription) as LegalPriority, pl.LegalPriorityID  " & _
  "                   , cstat.LegalStatusLongDescription as LegalFullStatus  " & _
  "                	, isnull(cstat.LegalStatusShortDescription, cstat.LegalStatusLongDescription) as LegalStatus, cstat.LegalStatusID  " & _
  "                	, d.DepartmentNo as DepartmentNo, convert(varchar, DepartmentNo) + ' - ' + DepartmentName as DepartmentFull  " & _
  "                	, isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as Requestor  " & _
  "                	, isnull(isnull(lu.UserDisplayName, lu.UserFullName), ch.LegalUserAssigned) as LegalUserName  " & _
  "                   , isnull(LegalDeadline, dead.AnswerDate) as Deadline, LegalDeadline, dead.AnswerDate as UserRequestedDeadline  " & _
  "                   , case when LegalDeadline is null and dead.AnswerDate is not null then 'True' else 'False' end as DeadlineColor  " & _
  "                   , convert(varchar, DepartmentNo) + ' - ' + DepartmentName as Department, ContractType, cp.ContractParty,  convert(varchar, Acct) + ' - ' + Description as ExpenseDescrip, Comment  " & _
  "				   , Pnding.Approvers " & _
  "				   , case when Pnding.ContractID is not null then null when rjct.Rejection = 'Deleted' then null else rjct.ApprovalDate end as DateApproved " & _
  "                	from WebFD.VendorContracts.ContractHeader ch  " & _
  "                	left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin and u.Active = 1  " & _
  "                	left join WebFD.VendorContracts.Users lu on ch.LegalUserAssigned = lu.UserLogin and lu.Active = 1  " & _
  "                   left join WebFD.VendorContracts.ContractType_LU cl on cl.ContractTypeID = ch.ContractTypeID and cl.Active = 1  " & _
  "                   left join WebFD.VendorContracts.ContractParty_LU cp on ch.ContractingParty = cp.ContractPartyID and cp.Active = 1  " & _
  "                   left join DWH.Axiom.Acct a on a.Acct = ch.ExpenseAccount and ISMAP  in ('SUP', 'FEE', 'OTH')   " & _
  "                   left join WebFD.VendorContracts.Contract_Comments  cc on cc.ContractID = ch.ContractID and cc.CommentType = 'Submission' and cc.Active = 1 and LegalOnly = 0  " & _
  "                	left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1  " & _
  "                	left join WebFD.VendorContracts.Contract_Status cs on ch.ContractID = cs.ContractID and cs.Active = 1  " & _
  "                   left join WebFD.VendorContracts.Contract_Answers dead on dead.QuestionID = 17 and dead.Active = 1 and dead.ContractID = ch.ContractID  " & _
  "                	left join WebFD.VendorContracts.ContractPriority_LU pl on (cs.LegalPriority = pl.LegalPriorityID   " & _
  "                		or (cs.LegalPriority is null and pl.DefaultInd = 1))  " & _
  "                	left join WebFD.VendorContracts.ContractStatus_LU cstat on (cs.LegalStatus = cstat.LegalStatusID   " & _
  "                		or (cs.LegalStatus is null and cstat.DefaultInd = 1))  " & _
  "                		left join (select ContractID, Max(Status) as Rejection , max(ModifyDate) as ApprovalDate  " & _
  "                				from WebFD.VendorContracts.Contract_Approvals where Active = 1 group by ContractID ) rjct   " & _
  "                				on ch.ContractID = rjct.ContractID  " & _
  "                		left join (  select distinct ST2.ContractID, substring( ( Select ' or ' + ApprovalSeeking as [text()]   " & _
  "						from #TempContractPull ST1 where ST1.ContractID = ST2.ContractID " & _
  "						Order by ApprovalSeeking " & _
  "						for XML PATH ('') ), 5, 1000) [Approvers] " & _
  "						from #TempContractPull ST2 " & _
  "                         ) Pnding on Pnding.ContractID = ch.ContractID  where ch.Active = 1 " & _
  "                		 ) x  where 1  = 1  "

        If Legal.Text = "1" Then
        Else
            x += " and x.DepartmentID in ( select distinct isnull(DepartmentID, x.DepartmentID) from WebFD.VendorContracts.Department_2_User t2u " & _
                "join WebFD.VendorContracts.Users u on t2u.UserLogin = u.UserLogin and u.Active = 1 " & _
                "where t2u.Active = 1 and u.UserLogin = '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "')"
        End If

        Dim Test As Tuple(Of String, String) = New Tuple(Of String, String)(a, x)
        Return Test

    End Function

    'Not being used any more; stop maintaining 9/19/2017 CRW
    'Private Function BasicSearch()

    '    Dim x As String = ""

    '    If Trim(txtLegalSearchKeywords.Text) <> "" Then
    '        x += "	declare @Phrase varchar(max) = '" & Replace(Trim(txtLegalSearchKeywords.Text), "'", "''") & "' " & _
    '        "	declare @Mini varchar(max) " & _
    '        "	 " & _
    '        "	if OBJECT_ID('tempdb..#VendorTempy') is not null  " & _
    '        "           begin  " & _
    '        "               drop table #VendorTempy  " & _
    '        "            End  " & _
    '        "	Select * " & _
    '        "	, 0 as NameScore, 0 as HeaderScore, 0 as CommentScore, 0 as AttachmentScore, 0 as QuestionScore  " & _
    '        "	into #VendorTempy " & _
    '        "	from ( "
    '    End If

    '    x += ContractSearch()

    '    If ddlLegalAssignedTo.SelectedIndex = 1 Then
    '        x += "and LegalUserAssigned is null "
    '    ElseIf ddlLegalAssignedTo.SelectedIndex <> 0 Then
    '        x += "and LegalUserAssigned = '" & Replace(ddlLegalAssignedTo.SelectedValue, "'", "''") & "' "
    '    End If

    '    If ddlLegalRequestor.SelectedIndex <> 0 Then
    '        x += "and RequesterUserLogin = '" & Replace(ddlLegalRequestor.SelectedValue, "'", "''") & "' "
    '    End If

    '    If cbLegalShowPending.Checked Then
    '    Else
    '        x += "and UserStatus <> 'Pending' "
    '    End If

    '    If cbLegalShowQueue.Checked Then
    '    ElseIf cbLegalShowQueue.Visible = False Then
    '    Else
    '        x += "and (UserStatus <> 'Approved' or LegalStatus = 'Closed') "
    '    End If

    '    If cbLegalShowQueueH.Checked Then
    '    ElseIf cbLegalShowQueueH.Visible = False Then
    '    Else
    '        x += "and (UserStatus <> 'Approved' or LegalStatus = 'Closed' or LegalPriority <> 'High') "
    '    End If

    '    If cbLegalShowQueueM.Checked Then
    '    ElseIf cbLegalShowQueueM.Visible = False Then
    '    Else
    '        x += "and (UserStatus <> 'Approved' or LegalStatus = 'Closed' or LegalPriority <> 'Medium') "
    '    End If

    '    If cbLegalShowQueueL.Checked Then
    '    ElseIf cbLegalShowQueueL.Visible = False Then
    '    Else
    '        x += "and (UserStatus <> 'Approved' or LegalStatus = 'Closed' or LegalPriority <> 'Low') "
    '    End If

    '    If cbLegalShowRejected.Checked Then
    '    Else
    '        x += "and (UserStatus <> 'Rejected' and LegalStatus <> 'Rejected') "
    '    End If

    '    If cbLegalShowClosed.Checked Then
    '    Else
    '        x += "and LegalStatus <> 'Closed' "
    '    End If

    '    If Trim(txtLegalSearchKeywords.Text) <> "" Then
    '        x += "		 ) z " & _
    '        "While len(@Phrase) > 0 " & _
    '        "begin " & _
    '        " " & _
    '        "if CHARINDEX(' ', @Phrase, 0) > 0  " & _
    '        "begin  " & _
    '        "set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0))))  " & _
    '        "set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase))))  " & _
    '        "End  " & _
    '        "Else  " & _
    '        "begin  " & _
    '        "set @Mini = ltrim(@Phrase)  " & _
    '        "set @Phrase = ''  " & _
    '        "End  " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set NameScore = Namescore + 3  " & _
    '        "where LegalName like '% ' + @Mini + ' %' " & _
    '        "	or LegalName like @Mini + ' %' " & _
    '        "	or LegalName like '% ' + @Mini " & _
    '        "	or LegalName = @Mini " & _
    '        "	 " & _
    '        "Update #VendorTempy " & _
    '        "set NameScore = Namescore + 1  " & _
    '        "where LegalName <> ContractName and (ContractName like '% ' + @Mini + ' %' " & _
    '        "	or ContractName like @Mini + ' %' " & _
    '        "	or ContractName like '% ' + @Mini " & _
    '        "	or ContractName = @Mini) " & _
    '        "	 " & _
    '        "Update #VendorTempy " & _
    '        "set NameScore = Namescore + 1  " & _
    '        "where LegalName like '%' + @Mini + '%' " & _
    '        "	 " & _
    '        "Update #VendorTempy " & _
    '        "set NameScore = Namescore + .5  " & _
    '        "where LegalName <> ContractName and ContractName like '%' + @Mini + '%' " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + 3  " & _
    '        "where VendorName like '% ' + @Mini + ' %' " & _
    '        "	or VendorName like @Mini + ' %' " & _
    '        "	or VendorName like '% ' + @Mini " & _
    '        "	or VendorName = @Mini " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + 1  " & _
    '        "where VendorName like '%' + @Mini + '%' " & _
    '        "	 " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + 2  " & _
    '        "where ContractPurpose like '% ' + @Mini + ' %' " & _
    '        "	or ContractPurpose like @Mini + ' %' " & _
    '        "	or ContractPurpose like '% ' + @Mini " & _
    '        "	or ContractPurpose = @Mini " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + 1  " & _
    '        "where ContractPurpose like '%' + @Mini + '%' " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + .5  " & _
    '        "where RenewalTerm like '% ' + @Mini + ' %' " & _
    '        "	or RenewalTerm like @Mini + ' %' " & _
    '        "	or RenewalTerm like '% ' + @Mini " & _
    '        "	or RenewalTerm = @Mini " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + .25  " & _
    '        "where RenewalTerm like '%' + @Mini + '%' " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + .5  " & _
    '        "where ContractLength like '% ' + @Mini + ' %' " & _
    '        "	or ContractLength like @Mini + ' %' " & _
    '        "	or ContractLength like '% ' + @Mini " & _
    '        "	or ContractLength = @Mini " & _
    '        " " & _
    '        "Update #VendorTempy " & _
    '        "set HeaderScore = HeaderScore + .25  " & _
    '        "where ContractLength like '%' + @Mini + '%' " & _
    '        " " & _
    '        "update vt  " & _
    '        "set CommentScore = CommentScore + 3 " & _
    '        "from #VendorTempy vt " & _
    '        "join WebFD.VendorContracts.Contract_Comments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
    '        "where cc.Comment like '% ' + @Mini + ' %' " & _
    '        "	or cc.Comment like @Mini + ' %' " & _
    '        "	or cc.Comment like '% ' + @Mini " & _
    '        "	or cc.Comment = @Mini " & _
    '        " " & _
    '        "update vt  " & _
    '        "set CommentScore = CommentScore + 1 " & _
    '        "from #VendorTempy vt " & _
    '        "join WebFD.VendorContracts.Contract_Comments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
    '        "where cc.Comment like '%' + @Mini + '%' " & _
    '        " " & _
    '        "update vt  " & _
    '        "set AttachmentScore = AttachmentScore + 3 " & _
    '        "from #VendorTempy vt " & _
    '        "join WebFD.VendorContracts.ContractAttachments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
    '        "where cc.FileName like '% ' + @Mini + ' %' " & _
    '        "	or FileName like @Mini + ' %' " & _
    '        "	or FileName like '% ' + @Mini " & _
    '        "	or FileName = @Mini " & _
    '        "	 " & _
    '        "update vt  " & _
    '        "set AttachmentScore = AttachmentScore + 1 " & _
    '        "from #VendorTempy vt " & _
    '        "join WebFD.VendorContracts.ContractAttachments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
    '        "where FileName like '%' + @Mini + '%' " & _
    '        " " & _
    '        "update vt  " & _
    '        "set QuestionScore = QuestionScore + 3 " & _
    '        "from #VendorTempy vt " & _
    '        "join WebFD.VendorContracts.Contract_Answers cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
    '        "where cc.AnswerComment like '% ' + @Mini + ' %' " & _
    '        "	or AnswerComment like @Mini + ' %' " & _
    '        "	or AnswerComment like '% ' + @Mini " & _
    '        "	or AnswerComment = @Mini " & _
    '        " " & _
    '        "update vt  " & _
    '        "set QuestionScore = QuestionScore + 1 " & _
    '        "from #VendorTempy vt " & _
    '        "join WebFD.VendorContracts.Contract_Answers cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
    '        "where AnswerComment like '%' + @Mini + '%' " & _
    '        " " & _
    '        "end " & _
    '        " " & _
    '        " " & _
    '        "Select NameScore * 10 + CommentScore * 6 + AttachmentScore + HeaderScore * 4 + AttachmentScore * 2 + QuestionScore * 6 as ord " & _
    '        ", * " & _
    '        "from #VendorTempy where  NameScore * 10 + CommentScore * 6 + AttachmentScore + HeaderScore * 4 + AttachmentScore * 2 + QuestionScore * 6  > 0" & _
    '        " " & _
    '        "order by 1 desc, DateAdded desc "
    '    Else
    '        x += " order by DateAdded desc, LegalName"
    '    End If

    '    Return x


    '    'LegalView = GetData(x).DefaultView
    '    'gvLegalResults.DataSource = LegalView(x)
    '    'gvLegalResults.DataBind()

    'End Function

    Private Function LegalView(x As String)
        Return GetData(x).DefaultView
    End Function

    Private Function AdvancedSearch()

        Dim x As String = ""

        Dim test As Tuple(Of String, String)
        test = ContractSearch()
        x += test.Item1
        If Trim(txtLegalSearchKeywords.Text) <> "" Then
            x += "	declare @Phrase varchar(max) = '" & Replace(Trim(txtLegalSearchKeywords.Text), "'", "''") & "' " & _
            "	declare @Mini varchar(max) " & _
            "	 " & _
            "	if OBJECT_ID('tempdb..#VendorTempy') is not null  " & _
            "           begin  " & _
            "               drop table #VendorTempy  " & _
            "            End  " & _
            "	Select * " & _
            "	, 0 as NameScore, 0 as HeaderScore, 0 as CommentScore, 0 as AttachmentScore, 0 as QuestionScore  " & _
            "	into #VendorTempy " & _
            "	from ( "
        End If

        x += test.Item2

        If ddlLegalAssignedTo.SelectedIndex = 1 Then
            x += "and LegalUserAssigned is null "
        ElseIf ddlLegalAssignedTo.SelectedIndex <> 0 Then
            x += "and LegalUserAssigned = '" & Replace(ddlLegalAssignedTo.SelectedValue, "'", "''") & "' "
        End If

        If cbLegalShowPending.Checked Then
        Else
            x += "and UserStatus <> 'Pending' "
        End If

        If cbLegalShowQueue.Checked Then
        ElseIf cbLegalShowQueue.Visible = False Then
        Else
            x += "and (UserStatus <> 'Approved' or LegalStatus in ('Closed', 'Approved') ) "
        End If

        If cbLegalShowQueueH.Checked Then
        ElseIf cbLegalShowQueueH.Visible = False Then
        Else
            x += "and (UserStatus <> 'Approved' or LegalStatus in ('Closed', 'Approved', 'Loaded', 'No Response', 'Deleted', 'Preliminary Review', 'Legal Review', 'Negotiation') or LegalPriority <> 'High' ) "
        End If

        If cbLegalShowQueueM.Checked Then
        ElseIf cbLegalShowQueueM.Visible = False Then
        Else
            x += "and (UserStatus <> 'Approved' or LegalStatus in ('Closed', 'Approved', 'Loaded', 'No Response', 'Deleted', 'Preliminary Review', 'Legal Review', 'Negotiation') or LegalPriority <> 'Medium' ) "
        End If

        If cbLegalShowQueueL.Checked Then
        ElseIf cbLegalShowQueueL.Visible = False Then
        Else
            x += "and (UserStatus <> 'Approved' or LegalStatus in ('Closed', 'Approved', 'Loaded', 'No Response', 'Deleted', 'Preliminary Review', 'Legal Review', 'Negotiation') or LegalPriority <> 'Low') "
        End If


        If cbLegalShowApproved.Checked Then
            'ElseIf cbLegalShowApproved.Visible = False Then
        Else
            x += "and (UserStatus <> 'Approved' or LegalStatus <> 'Approved') "
        End If

        If cbLegalShowRejected.Checked Then
        Else
            x += "and (UserStatus <> 'Deleted' and LegalStatus <> 'Deleted') "
        End If

        If cbLegalShowClosed.Checked Then
        Else
            x += "and LegalStatus <> 'Closed' "
        End If

        If cbLegalLoadedinMeditract.Checked Then
        ElseIf cbLegalLoadedinMeditract.Visible = False Then
        Else
            x += "and LegalStatus <> 'Loaded' "
        End If

        If cbLegalNoResponse.Checked Then
        ElseIf cbLegalNoResponse.Visible = False Then
        Else
            x += "and LegalStatus <> 'No Response' "
        End If

        If cbLegalNegotiation.Checked Then
        ElseIf cbLegalNegotiation.Visible = False Then
        Else
            x += "and LegalStatus <> 'Negotiation' "
        End If

        If cbLegalLegal.Checked Then
        ElseIf cbLegalLegal.Visible = False Then
        Else
            x += "and LegalStatus <> 'Legal Review' "
        End If

        If cbLegalPreliminary.Checked Then
        ElseIf cbLegalPreliminary.Visible = False Then
        Else
            x += "and LegalStatus <> 'Preliminary Review' "
        End If

        'If Len(Trim(txtLegalContractName.Text)) > 0 Then
        '    x += "and LegalName like '%" & Replace(Trim(txtLegalContractName.Text), "'", "''") & "%' "
        'End If

        If Len(Trim(txtLegalContractID.Text)) > 0 Then
            x += "and ContractID = '" & Replace(Trim(txtLegalContractID.Text), "'", "''") & "' "
        End If

        If ddlLegalRequestor.SelectedIndex <> 0 Then
            x += "and RequesterUserLogin = '" & Replace(ddlLegalRequestor.SelectedValue, "'", "''") & "' "
        End If

        If ddlLegalContractCostCenter.SelectedIndex <> 0 Then
            x += "and DepartmentID = '" & Replace(ddlLegalContractCostCenter.SelectedValue, "'", "''") & "' "
        End If

        'If Len(Trim(txtLegalVendor.Text)) > 0 Then
        '    x += "and VendorName like '%" & Replace(Trim(txtLegalVendor.Text), "'", "''") & "%' "
        'End If

        'If ddlLegalContractCost.SelectedIndex <> 0 Then
        '    x += "and AnnualContractExpense = '" & Replace(ddlLegalContractCost.SelectedValue, "'", "''") & "' "
        'End If

        'If Len(Trim(txtLegalContractPurpose.Text)) > 0 Then
        '    x += "and ContractPurpose like '%" & Replace(Trim(txtLegalContractPurpose.Text), "'", "''") & "%' "
        'End If

        If Len(Trim(txtLegalSrchSubStart.Text)) > 0 And Len(Trim(txtLegalSrchSubEnd.Text)) > 0 Then
            x += "and DateAdded between '" & Replace(Trim(txtLegalSrchSubStart.Text), "'", "''") & "' and '" & Replace(Trim(txtLegalSrchSubEnd.Text), "'", "''") & "' "
        ElseIf Len(Trim(txtLegalSrchSubStart.Text)) > 0 Then
            x += "and DateAdded >= '" & Replace(Trim(txtLegalSrchSubStart.Text), "'", "''") & "'"
        ElseIf Len(Trim(txtLegalSrchSubEnd.Text)) > 0 Then
            x += "and DateAdded <= '" & Replace(Trim(txtLegalSrchSubEnd.Text), "'", "''") & "'"
        End If

        'If ddlLegalQuestionSearch.SelectedIndex <> 0 Then
        '    x += "and exists (Select * from WebFD.VendorContracts.Contract_Answers ca where ca.ContractID = x.ContractID and ca.QuestionID = '" & Replace(ddlLegalQuestionSearch.SelectedValue, "'", "''") & "' "
        '    If ddlLegalQuestionResponse.SelectedIndex <> 0 Then
        '        x += "and Response = '" & Replace(ddlLegalQuestionResponse.SelectedValue, "'", "''") & "' "
        '    End If
        '    If Len(Trim(txtLegalQuestionComment.Text)) > 0 Then
        '        x += "and AnswerComment like '%" & Replace(Trim(txtLegalQuestionComment.Text), "'", "''") & "%'"
        '    End If
        '    x += ") "
        'End If

        'If Len(Trim(txtLegalContractPurpose.Text)) > 0 Then
        '    x += "and exists (select * from WebFD.VendorContracts.ContractAttachments ca where ca.ContractID = x.ContractID and FileName like '%" & Replace(Trim(txtLegalContractAttachmentName.Text), "'", "''") & "%' ) "
        'End If


        If Trim(txtLegalSearchKeywords.Text) <> "" Then
            x += "		 ) z " & _
            "While len(@Phrase) > 0 " & _
            "begin " & _
            " " & _
            "if CHARINDEX(' ', @Phrase, 0) > 0  " & _
            "begin  " & _
            "set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0))))  " & _
            "set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase))))  " & _
            "End  " & _
            "Else  " & _
            "begin  " & _
            "set @Mini = ltrim(@Phrase)  " & _
            "set @Phrase = ''  " & _
            "End  " & _
            " " & _
            "Update #VendorTempy " & _
            "set NameScore = Namescore + 3  " & _
            "where LegalName like '% ' + @Mini + ' %' " & _
            "	or LegalName like @Mini + ' %' " & _
            "	or LegalName like '% ' + @Mini " & _
            "	or LegalName = @Mini " & _
            "	 " & _
            "Update #VendorTempy " & _
            "set NameScore = Namescore + 1  " & _
            "where LegalName <> ContractName and (ContractName like '% ' + @Mini + ' %' " & _
            "	or ContractName like @Mini + ' %' " & _
            "	or ContractName like '% ' + @Mini " & _
            "	or ContractName = @Mini) " & _
            "	 " & _
            "Update #VendorTempy " & _
            "set NameScore = Namescore + 1  " & _
            "where LegalName like '%' + @Mini + '%' " & _
            "	 " & _
            "Update #VendorTempy " & _
            "set NameScore = Namescore + .5  " & _
            "where LegalName <> ContractName and ContractName like '%' + @Mini + '%' " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + 3  " & _
            "where VendorName like '% ' + @Mini + ' %' " & _
            "	or VendorName like @Mini + ' %' " & _
            "	or VendorName like '% ' + @Mini " & _
            "	or VendorName = @Mini " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + 1  " & _
            "where VendorName like '%' + @Mini + '%' " & _
            "	 " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + 2  " & _
            "where ContractPurpose like '% ' + @Mini + ' %' " & _
            "	or ContractPurpose like @Mini + ' %' " & _
            "	or ContractPurpose like '% ' + @Mini " & _
            "	or ContractPurpose = @Mini " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + 1  " & _
            "where ContractPurpose like '%' + @Mini + '%' " & _
             "	 " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + 2  " & _
            "where ContractingPartyDetail like '% ' + @Mini + ' %' " & _
            "	or ContractingPartyDetail like @Mini + ' %' " & _
            "	or ContractingPartyDetail like '% ' + @Mini " & _
            "	or ContractingPartyDetail = @Mini " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + 1  " & _
            "where ContractingPartyDetail like '%' + @Mini + '%' " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + .5  " & _
            "where RenewalTerm like '% ' + @Mini + ' %' " & _
            "	or RenewalTerm like @Mini + ' %' " & _
            "	or RenewalTerm like '% ' + @Mini " & _
            "	or RenewalTerm = @Mini " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + .25  " & _
            "where RenewalTerm like '%' + @Mini + '%' " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + .5  " & _
            "where ContractLength like '% ' + @Mini + ' %' " & _
            "	or ContractLength like @Mini + ' %' " & _
            "	or ContractLength like '% ' + @Mini " & _
            "	or ContractLength = @Mini " & _
            " " & _
            "Update #VendorTempy " & _
            "set HeaderScore = HeaderScore + .25  " & _
            "where ContractLength like '%' + @Mini + '%' " & _
            " " & _
            "update vt  " & _
            "set CommentScore = CommentScore + 3 " & _
            "from #VendorTempy vt " & _
            "join WebFD.VendorContracts.Contract_Comments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
            "where cc.Comment like '% ' + @Mini + ' %' " & _
            "	or cc.Comment like @Mini + ' %' " & _
            "	or cc.Comment like '% ' + @Mini " & _
            "	or cc.Comment = @Mini " & _
            " " & _
            "update vt  " & _
            "set CommentScore = CommentScore + 1 " & _
            "from #VendorTempy vt " & _
            "join WebFD.VendorContracts.Contract_Comments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
            "where cc.Comment like '%' + @Mini + '%' " & _
            " " & _
            "update vt  " & _
            "set AttachmentScore = AttachmentScore + 3 " & _
            "from #VendorTempy vt " & _
            "join WebFD.VendorContracts.ContractAttachments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
            "where cc.FileName like '% ' + @Mini + ' %' " & _
            "	or FileName like @Mini + ' %' " & _
            "	or FileName like '% ' + @Mini " & _
            "	or FileName = @Mini " & _
            "	 " & _
            "update vt  " & _
            "set AttachmentScore = AttachmentScore + 1 " & _
            "from #VendorTempy vt " & _
            "join WebFD.VendorContracts.ContractAttachments cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
            "where FileName like '%' + @Mini + '%' " & _
            " " & _
            "update vt  " & _
            "set QuestionScore = QuestionScore + 3 " & _
            "from #VendorTempy vt " & _
            "join WebFD.VendorContracts.Contract_Answers cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
            "where cc.AnswerComment like '% ' + @Mini + ' %' " & _
            "	or AnswerComment like @Mini + ' %' " & _
            "	or AnswerComment like '% ' + @Mini " & _
            "	or AnswerComment = @Mini " & _
            " " & _
            "update vt  " & _
            "set QuestionScore = QuestionScore + 1 " & _
            "from #VendorTempy vt " & _
            "join WebFD.VendorContracts.Contract_Answers cc on vt.ContractID = cc.ContractID and cc.Active = 1 " & _
            "where AnswerComment like '%' + @Mini + '%' " & _
            " " & _
            "end " & _
            " " & _
            " " & _
            "Select NameScore * 10 + CommentScore * 6 + AttachmentScore + HeaderScore * 4 + AttachmentScore * 2 + QuestionScore * 6 as ord " & _
            ", * " & _
            "from #VendorTempy where NameScore * 10 + CommentScore * 6 + AttachmentScore + HeaderScore * 4 + AttachmentScore * 2 + QuestionScore * 6 > 0" & _
            " " & _
            "order by 1 desc, DateAdded desc "
        Else
            x += " order by DateAdded desc, LegalName"
        End If

        Return x
        'LegalView = GetData(x).DefaultView
        'gvLegalResults.DataSource = LegalView
        'gvLegalResults.DataBind()

    End Function

    'Private Sub ddlLegalAssignedTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLegalAssignedTo.SelectedIndexChanged

    '    Search()


    'End Sub

    'Private Sub lbLegalDisplaySrch_Click(sender As Object, e As EventArgs) Handles lbLegalDisplaySrch.Click
    '    If pnlLegalSrchPanel.Visible = True Then
    '        pnlLegalSrchPanel.Visible = False
    '    Else
    '        pnlLegalSrchPanel.Visible = True

    '    End If
    'End Sub

    Private Sub gvLegalResults_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvLegalResults.PageIndexChanging
        Try
            Dim x As String
            'If pnlLegalSrchPanel.Visible = True Then
            '    x = AdvancedSearch()
            'Else
            '    x = BasicSearch()
            'End If
            x = AdvancedSearch()

            Dim dv As DataView = LegalView(x)

            If Legaldir.Text = "1" Then
                dv.Sort = Legalmap.Text + " " + "asc"
                Legaldir.Text = "1"

            Else
                dv.Sort = Legalmap.Text + " " + "desc"
                Legaldir.Text = 0

            End If

            gvLegalResults.PageIndex = e.NewPageIndex
            gvLegalResults.DataSource = dv
            gvLegalResults.DataBind()


        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLegalResults_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvLegalResults.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

                'e.Row.Cells(5).CssClass = "hidden"
                'e.Row.Cells(6).CssClass = "hidden"


                If Legal.Text = "1" Then
                    If Not IsDBNull(e.Row.DataItem("UserStatus")) Then
                        If e.Row.DataItem("UserStatus") = "Deleted" Then
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor)
                        ElseIf e.Row.DataItem("UserStatus") = "Pending" Then
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPendingApprovalColor)
                        Else
                            If Not IsDBNull(e.Row.DataItem("LegalStatus")) Then
                                If e.Row.DataItem("LegalStatus") = "Closed" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalClosedColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "Deleted" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "Approved" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(ApprovedColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "Loaded" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLoadedColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "No Response" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(NoResponseColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "Preliminary Review" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPreliminaryColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "Legal Review" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLegalColor)
                                ElseIf e.Row.DataItem("LegalStatus") = "Negotiation" Then
                                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalNegotiationColor)
                                Else
                                    If Not IsDBNull(e.Row.DataItem("LegalPriority")) Then
                                        If e.Row.DataItem("LegalPriority") = "High" Then
                                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalHighPriorityColor)
                                        ElseIf e.Row.DataItem("LegalPriority") = "Medium" Then
                                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalMediumPriorityColor)
                                        ElseIf e.Row.DataItem("LegalPriority") = "Low" Then
                                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLowPriorityColor)
                                        End If
                                    Else
                                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor)
                                    End If

                                End If

                            End If
                        End If

                    End If
                Else
                    If Not IsDBNull(e.Row.DataItem("UserStatus")) Then
                        If e.Row.DataItem("UserStatus") = "Deleted" Then
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                        ElseIf e.Row.DataItem("UserStatus") = "Pending" Then
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor)
                        Else
                            If Not IsDBNull(e.Row.DataItem("LegalStatus")) And e.Row.DataItem("LegalStatus") = "Closed" Then

                                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(ClosedColor)
                            ElseIf e.Row.DataItem("LegalStatus") = "Deleted" Then
                                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                            ElseIf e.Row.DataItem("LegalStatus") = "Approved" Then
                                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(ApprovedColor)
                            Else
                                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                            End If
                        End If

                    End If
                End If

            End If

        Catch ex As Exception
            'LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub AttachmentDownloadRowing(RelevantGrid As GridView)

        For Each canoe As GridViewRow In RelevantGrid.Rows

            Dim lnkDownload As LinkButton = canoe.FindControl("lnkDownload")
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload)

        Next

    End Sub


    Private Sub LegalRowing()


        'Dim LegalPendingApprovalColor As String = "#d5eaff"
        'Dim LegalHighPriorityColor As String = "#ff7d7d"
        'Dim LegalMediumPriorityColor As String = "#ffbb77"
        'Dim LegalLowPriorityColor As String = "#ffffaa"
        'Dim LegalRejectedColor As String = "#ffffff"
        'Dim LegalClosedColor As String = "#eeeeee"

        'Dim PendingApprovalColor As String = "#d5eaff"
        'Dim PendingLegalApprovalColor As String = "#ffbb77"
        'Dim RejectedColor As String = "#ffffff"
        'Dim ClosedColor As String = "#eeeeee"

        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff") 'white
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d") 'red
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5efe2") 'brown
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#d5eaff") 'blue
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1") 'light green
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#eeeeee") 'gray
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77") 'orange
        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb") 'green

        For Each canoe As GridViewRow In gvLegalResults.Rows
            If canoe.RowIndex = gvLegalResults.SelectedIndex Then
                If canoe.Cells(10).Text = "Deleted" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor) 'white
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor) 'white
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d") 'red
                    End If
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#eeeeee")
                ElseIf canoe.Cells(10).Text = "Pending" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5efe2") 'brown
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPendingApprovalColor) 'blue
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor) 'blue
                    End If
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#eee4ce")
                ElseIf canoe.Cells(11).Text = "Closed" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1") 'light green
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalClosedColor) 'gray
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ClosedColor) 'gray
                    End If
                ElseIf canoe.Cells(11).Text = "Deleted" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1") 'light green
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor) 'gray
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor) 'gray
                    End If
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#e5ffe5")
                ElseIf canoe.Cells(11).Text = "Approved" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ApprovedColor) 'gray
                ElseIf canoe.Cells(11).Text = "Loaded" And Legal.Text = "1" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLoadedColor) 'gray
                ElseIf canoe.Cells(11).Text = "No Response" And Legal.Text = "1" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(NoResponseColor) 'gray
                ElseIf canoe.Cells(11).Text = "Legal Review" And Legal.Text = "1" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLegalColor) 'gray
                ElseIf canoe.Cells(11).Text = "Preliminary Review" And Legal.Text = "1" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPreliminaryColor) 'gray
                ElseIf canoe.Cells(11).Text = "Negotiation" And Legal.Text = "1" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalNegotiationColor) 'gray
                ElseIf canoe.Cells(12).Text = "High" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalHighPriorityColor) 'red
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor) 'orange
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb") 'green
                    End If
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                ElseIf canoe.Cells(12).Text = "Medium" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalMediumPriorityColor) 'orange
                    Else
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb") 'green
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor) 'orange
                    End If
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff8000")
                ElseIf canoe.Cells(12).Text = "Low" Then
                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLowPriorityColor) 'yellow
                    Else
                        'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb") 'green
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor) 'orange
                    End If
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                End If

            Else
                If canoe.Cells(10).Text = "Deleted" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                    End If
                ElseIf canoe.Cells(10).Text = "Pending" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5efe2")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalPendingApprovalColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor)
                    End If
                ElseIf canoe.Cells(11).Text = "Closed" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalClosedColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ClosedColor)
                    End If
                ElseIf canoe.Cells(11).Text = "Deleted" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#f1fff1")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalRejectedColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                    End If
                ElseIf canoe.Cells(11).Text = "Approved" Then
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ApprovedColor) 'gray
                ElseIf canoe.Cells(11).Text = "No Response" And Legal.Text = "1" Then
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(NoResponseColor) 'gray
                ElseIf canoe.Cells(12).Text = "High" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalHighPriorityColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                    End If
                ElseIf canoe.Cells(12).Text = "Medium" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalMediumPriorityColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                    End If
                ElseIf canoe.Cells(12).Text = "Low" Then
                    'canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                    If Legal.Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(LegalLowPriorityColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub gvLegalResults_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLegalResults.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(9).Text = "True" Then
                e.Row.Cells(8).Text = "<i>" & e.Row.Cells(8).Text & "</i>"
            End If

        End If

    End Sub

    Private Sub gvLegalResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLegalResults.SelectedIndexChanged

        SelectLegalRow()

    End Sub
    Private Sub SelectLegalRow()
        Try
            LegalRowing()
            txtLegalResultComments.Text = ""
            'cbHideLegalComments.Checked = True


            Dim x As String = gvLegalResults.DataKeys(gvLegalResults.SelectedIndex).Value.ToString

            gvLegalApproval.DataSource = GetData("select isnull(isnull(u.UserDisplayName, u.UserFullName), ca.UserLogin) as UserName " & _
                    ", Status, Comment, ca.ModifyDate, RoleFullOnSubmit as Role " & _
                    "from WebFD.VendorContracts.Contract_Approvals ca " & _
                    "left join WebFD.VendorContracts.Users u on ca.UserLogin = u.UserLogin and u.Active = 1 " & _
                    "where ca.ContractID = '" & x & "' and ca.Active = 1 and ca.UserLogin <> 'Default Insert'")

            gvLegalApproval.DataBind()
            AttachmentGrid(gvLegalAttachments, x)
            Dim test As Tuple(Of String, String)
            test = ContractSearch()
            Dim y As DataTable = GetData(test.Item1 & test.Item2 & " and ContractID = '" & x & "' ")

            ddlLegalResultAssignedLegal.DataSource = GetData("select isnull(UserDisplayName, UserFullName) as UserName, UserLogin, 2 as Ord From WebFD.VendorContracts.Users " & _
            "where LegalTeam = 1 and Active = 1 and isnull(IgnoreUser, 0) = 0 " & _
            "union " & _
            "select isnull(UserDisplayName, UserFullName) + ' (INACTIVE)' as UserName, LegalUserAssigned, 3  " & _
            " from WebFD.VendorContracts.ContractHeader ch " & _
            "left join WebFD.VendorContracts.Users u on ch.LegalUserAssigned = u.UserLogin and u.Active = 1   " & _
            "where ch.ContractID = '" & x & "' and ch.LegalUserAssigned is not null and isnull(LegalTeam, 0) = 0 " & _
            "union Select '(Unassigned)', '--Unassigned crw--', 1 " & _
            "order by Ord, UserName, UserLogin ")
            ddlLegalResultAssignedLegal.DataValueField = "UserLogin"
            ddlLegalResultAssignedLegal.DataTextField = "UserName"
            ddlLegalResultAssignedLegal.DataBind()

            If IsDBNull(y(0)("LegalUserAssigned")) Then
                ddlLegalResultAssignedLegal.SelectedIndex = 0
            Else
                ddlLegalResultAssignedLegal.SelectedValue = y(0)("LegalUserAssigned")
            End If

            If IsDBNull(y(0)("LegalContractAlias")) Then
                txtLegalResultLegalContractName.Text = ""
            Else
                txtLegalResultLegalContractName.Text = y(0)("LegalContractAlias")
            End If

            Dim z As String = ""
            If IsDBNull(y(0)("LegalStatusID")) Then
                z = "-1"
            Else
                z = y(0)("LegalStatusID")
            End If

            ddlLegalResultContractStatus.DataSource = GetData("select LegalStatusID, LegalStatusLongDescription, DefaultInd from WebFD.VendorContracts.ContractStatus_LU where Active = 1 " & _
                "union select LegalStatusID, LegalStatusLongDescription + ' (Retired)', DefaultInd from WebFD.VendorContracts.ContractStatus_LU where Active = 0 and LegalStatusID = " & Replace(z, "'", "''") & _
                " order by LegalStatusLongDescription")
            ddlLegalResultContractStatus.DataValueField = "LegalStatusID"
            ddlLegalResultContractStatus.DataTextField = "LegalStatusLongDescription"
            ddlLegalResultContractStatus.DataBind()

            If IsDBNull(y(0)("LegalStatusID")) Then
                ddlLegalResultContractStatus.SelectedIndex = 0
            Else
                ddlLegalResultContractStatus.SelectedValue = y(0)("LegalStatusID")
            End If

            Dim pri As String = ""
            If IsDBNull(y(0)("LegalPriorityID")) Then
                z = "-1"
            Else
                z = y(0)("LegalPriorityID")
            End If

            ddlLegalResultContractPriority.DataSource = GetData("select LegalPriorityID, LegalPriorityShortDescription, DefaultInd from WebFD.VendorContracts.ContractPriority_LU where Active = 1 " & _
                "union select LegalPriorityID, LegalPriorityShortDescription + ' (Retired)', DefaultInd from WebFD.VendorContracts.ContractPriority_LU where Active = 0 and LegalPriorityID = " & Replace(z, "'", "''") & _
                " order by LegalPriorityShortDescription")
            ddlLegalResultContractPriority.DataValueField = "LegalPriorityID"
            ddlLegalResultContractPriority.DataTextField = "LegalPriorityShortDescription"
            ddlLegalResultContractPriority.DataBind()

            If IsDBNull(y(0)("LegalPriorityID")) Then
                ddlLegalResultContractPriority.SelectedIndex = ""
            Else
                ddlLegalResultContractPriority.SelectedValue = (y(0)("LegalPriorityID"))
            End If

            If IsDBNull(y(0)("ReferralAssignment")) Then
                txtLegalResultReferralAssignment.Text = ""
            Else
                txtLegalResultReferralAssignment.Text = (y(0)("ReferralAssignment"))
            End If

            If IsDBNull(y(0)("ContractID")) Then
                lblLegalContractID.Text = ""
            Else
                lblLegalContractID.Text = y(0)("ContractID")
            End If

            If IsDBNull(y(0)("Requestor")) Then
                lblLegalResultRequestor.Text = ""
            Else
                lblLegalResultRequestor.Text = y(0)("Requestor")
            End If

            If IsDBNull(y(0)("ContractName")) Then
                lblLegalResultContractName.Text = ""
            Else
                lblLegalResultContractName.Text = y(0)("ContractName")
            End If

            If IsDBNull(y(0)("DepartmentFull")) Then
                lblLegalResultDepartmentNo.Text = ""
            Else
                lblLegalResultDepartmentNo.Text = (y(0)("DepartmentFull"))
            End If

            If IsDBNull(y(0)("VendorName")) Then
                lblLegalResultVendorName.Text = ""
            Else
                lblLegalResultVendorName.Text = (y(0)("VendorName"))
            End If

            If IsDBNull(y(0)("ContractType")) Then
                lblLegalResultContractType.Text = ""
            Else
                lblLegalResultContractType.Text = (y(0)("ContractType"))
            End If

            If IsDBNull(y(0)("ContractLength")) Then
                lblLegalResultContractLength.Text = ""
            Else
                lblLegalResultContractLength.Text = (y(0)("ContractLength"))
            End If

            If IsDBNull(y(0)("AnnualContractExpense")) Then
                lblLegalResultContractCost.Text = ""
            Else
                lblLegalResultContractCost.Text = (y(0)("AnnualContractExpense"))
            End If

            If IsDBNull(y(0)("AutoRenewal")) Then
                lblLegalResultAutoRenewal.Text = ""
            Else
                lblLegalResultAutoRenewal.Text = (y(0)("AutoRenewal"))
            End If

            If IsDBNull(y(0)("RenewalTerm")) Then
                lblLegalResultRenewalTerm.Text = ""
            Else
                lblLegalResultRenewalTerm.Text = (y(0)("RenewalTerm"))
            End If

            If IsDBNull(y(0)("ContractPurpose")) Then
                lblLegalResultContractPurpose.Text = ""
            Else
                lblLegalResultContractPurpose.Text = (y(0)("ContractPurpose"))
            End If

            If IsDBNull(y(0)("ContractParty")) Then
                lblLegalResultContractParty.Text = ""
            Else
                lblLegalResultContractParty.Text = (y(0)("ContractParty"))
            End If

            If IsDBNull(y(0)("ContractingPartyDetail")) Then
                lblLegalResultContractPartyExplain.Text = ""
            Else
                lblLegalResultContractPartyExplain.Text = (y(0)("ContractingPartyDetail"))
            End If

            If IsDBNull(y(0)("ExpenseDescrip")) Then
                lblLegalResultContractBudgetAcct.Text = ""
            Else
                lblLegalResultContractBudgetAcct.Text = (y(0)("ExpenseDescrip"))
            End If

            If IsDBNull(y(0)("DateAdded")) Then
                lblLegalResultDate.Text = ""
            Else
                lblLegalResultDate.Text = (y(0)("DateAdded"))
            End If

            If IsDBNull(y(0)("Comment")) Then
                lblLegalResultRequestorComments.Text = ""
            Else
                lblLegalResultRequestorComments.Text = (y(0)("Comment"))
            End If

            If IsDBNull(y(0)("UserRequestedDeadline")) Then
                lblUserRequestedDeadline.Text = ""
            Else
                lblUserRequestedDeadline.Text = (y(0)("UserRequestedDeadline"))
            End If

            If IsDBNull(y(0)("LegalDeadline")) Then
                txtLegalResultDeadline.Text = ""
            Else
                txtLegalResultDeadline.Text = (y(0)("LegalDeadline"))
            End If

            loadLegalgrid(x)
            LoadLegalComments(x)
            pnlLegalUpdates.Visible = True

            If Legal.Text = "1" Then
                pnlLegalComments.Visible = True
                pnlLegalFields.Visible = True
            Else
                pnlLegalComments.Visible = False
                pnlLegalFields.Visible = False
            End If


        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub gvLegalResults_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvLegalResults.Sorting
        Try

            Dim x As String
            'If pnlLegalSrchPanel.Visible = True Then
            '    x = AdvancedSearch()
            'Else
            '    x = BasicSearch()
            'End If
            x = AdvancedSearch()

            Dim dv As DataView
            Dim sorts As String
            dv = LegalView(x)

            sorts = e.SortExpression

            If e.SortExpression = Legalmap.Text Then

                If Legaldir.Text = "1" Then
                    dv.Sort = sorts + " " + "desc"
                    Legaldir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    Legaldir.Text = "1"
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                Legaldir.Text = "1"
                Legalmap.Text = e.SortExpression
            End If

            gvLegalResults.DataSource = dv
            gvLegalResults.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub PopulateDDLLegalAssignedTo()

        ddlLegalAssignedTo.DataSource = GetData("select isnull(UserDisplayName, UserFullName) as UserName, UserLogin, 2 as Ord From WebFD.VendorContracts.Users " & _
"where LegalTeam = 1 and Active = 1 and isnull(IgnoreUser, 0) = 0" & _
"union " & _
"select isnull(UserDisplayName, UserFullName) + ' (INACTIVE)' as UserName, LegalUserAssigned, 3  " & _
" from WebFD.VendorContracts.ContractHeader ch " & _
"left join WebFD.VendorContracts.Users u on ch.LegalUserAssigned = u.UserLogin and u.Active = 1   " & _
"where ch.LegalUserAssigned is not null and isnull(LegalTeam, 0) = 0 " & _
"union Select '(View All)', '-- ALL crw--', 0 " & _
"union Select '(Unassigned)', '--Unassigned crw--', 1 " & _
"order by Ord, UserName, UserLogin ")
        ddlLegalAssignedTo.DataValueField = "UserLogin"
        ddlLegalAssignedTo.DataTextField = "UserName"
        ddlLegalAssignedTo.DataBind()

    End Sub

    Private Sub LoadLegalComments(x As Integer)
        Try
            'Dim z As String = "<Table runat=server><TableRow><TableCell></TableCell></TableRow>"
            Dim z As String = ""

            'Dim y As DataTable = GetData("select " & _
            '    " '<TableRow><TableCell width=2px></TableCell><TableCell ColumnSpan = 3>' + Comment + '</TableCell><TableCell width=2px></TableRow>' " & _
            '    "+ '<TableRow><TableCell></TableCell><TableCell></TableCell><TableCell ColumnSpan = 2> - ' + isnull(isnull(UserDisplayName, UserFullName), cc.ModifyUser) + '</TableCell></TableRow>' " & _
            '    "+ '<TableRow><TableCell></TableCell><TableCell></TableCell><TableCell ColumnSpan = 2>    ' + convert(varchar, ModifyDate) + '</TableCell></TableRow> <TableRow><TableCell></TableCell></TableRow>' " & _
            '    "from WebFD.VendorContracts.Contract_Comments cc " & _
            '    "left join WebFD.VendorContracts.Users u on cc.ModifyUser = u.UserLogin and u.Active = 1 " & _
            '    "where cc.Active = 1 and Relation = 'Legal' and ContractID = " & x & " " & _
            '    "order by ModifyDate ")

            Dim y As DataTable = GetData("select " & _
                " '<br>' + Comment + '<br>' " & _
                "+ '<i> - ' + isnull(isnull(UserDisplayName, UserFullName), cc.ModifyUser) " & _
                "+ '   (' + convert(varchar, ModifyDate) + ')</i><br><br>' " & _
                "from WebFD.VendorContracts.Contract_Comments cc " & _
                "left join WebFD.VendorContracts.Users u on cc.ModifyUser = u.UserLogin and u.Active = 1 " & _
                "where cc.Active = 1 and Relation = 'Legal' and ContractID = " & x & " " & _
                "order by ModifyDate ")

            For Each row In y.Rows

                z += row(0)

            Next

            'z += "</Table>"
            z += "<BR>"

            lblLegalResultComments.Text = Server.HtmlDecode(z)
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnLegalUpdateContract_Click(sender As Object, e As EventArgs) Handles btnLegalUpdateContract.Click
        Try
            Dim x As String = Replace(lblLegalContractID.Text, "'", "''")
            Dim UpdateSQL As String = "Update WebFD.VendorContracts.ContractHeader set "

            If ddlLegalResultAssignedLegal.SelectedIndex <> 0 Then
                UpdateSQL += "LegalUserAssigned = '" & Replace(ddlLegalResultAssignedLegal.SelectedValue, "'", "''") & "', "
            Else
                UpdateSQL += "LegalUserAssigned = null, "
            End If

            If Trim(txtLegalResultLegalContractName.Text) <> "" Then
                UpdateSQL += "LegalContractAlias = '" & Replace(txtLegalResultLegalContractName.Text, "'", "''") & "', "
            Else
                UpdateSQL += "LegalContractAlias = null, "
            End If

            If Trim(txtLegalResultReferralAssignment.Text) <> "" Then
                UpdateSQL += "ReferralAssignment = '" & Replace(txtLegalResultReferralAssignment.Text, "'", "''") & "' "
            Else
                UpdateSQL += "ReferralAssignment = null "
            End If

            UpdateSQL += " where ContractID = '" & x & "'  Update WebFD.VendorContracts.Contract_Status set Active = 0 where ContractID = '" & x & "' " & _
                "Insert into WebFD.VendorContracts.Contract_Status values ('" & x & "', '" & Replace(ddlLegalResultContractStatus.SelectedValue, "'", "''") & "', '" & _
                Replace(ddlLegalResultContractPriority.SelectedValue, "'", "''") & "', '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', getdate(), 1 "

            If Trim(txtLegalResultDeadline.Text) <> "" Then
                UpdateSQL += ", '" & Replace(Trim(txtLegalResultDeadline.Text), "'", "''") & "')"
            Else
                UpdateSQL += ", null)"
            End If

            If Trim(txtLegalResultComments.Text) <> "" Then

                UpdateSQL += "Insert into WebFD.VendorContracts.Contract_Comments values ('" & x & "', 'Legal', '" & Replace(txtLegalResultComments.Text, "'", "''") & "', 'Legal' "

                'If cbHideLegalComments.Checked Then
                UpdateSQL += ", 1 "
                'Else
                '    UpdateSQL += ", 0 "
                'End If

                UpdateSQL += ", getdate(), '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 1 )"

            End If

            ExecuteSql(UpdateSQL)

            Dim BonusSql As String = "Insert into WebFD.VendorContracts.EmailStatus select " & x & ", 'PendingLegalRejection', 1, null where not exists (select * from WebFD.VendorContracts.EmailStatus " & _
                " where ContractID = " & x & " and LatestEmailStatus = 'PendingLegalRejection' and Active = 1) and exists (select * from WebFD.VendorContracts.Contract_Status cs join WebFD.VendorContracts.ContractStatus_LU csl " & _
                " on cs.LegalStatus = csl.LegalStatusID and cs.Active = 1 and csl.LegalStatusShortDescription = 'Deleted' and cs.ContractID = " & x & ") "

            ExecuteSql(BonusSql)

            'Dim BonusSql As String = "if OBJECT_ID('tempdb..#MainEmails') is not null " & _
            '  "              begin  " & _
            '  "                 drop table #MainEmails " & _
            '  "              End  " & _
            '  "			   " & _
            '  "declare @ContractID int = " & x & _
            '  "select ch.ContractID, RequesterUserLogin, u.UserEmail as EmailAddress " & _
            '  ", isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as UserName , ch.ContractName " & _
            '  ", ch.ContractPurpose, ch.VendorName, ch.DateAdded " & _
            '  ", isnull(isnull(convert(varchar, d.DepartmentNo) + ' -- ', '') + d.DepartmentName, ch.DepartmentID) as Department " & _
            '  "into #MainEmails " & _
            '  "from WebFD.VendorContracts.ContractHeader ch " & _
            '  "left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin " & _
            '  "left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            '  "join WebFD.VendorContracts.Contract_Status cs on ch.ContractID = cs.ContractID and cs.Active = 1 " & _
            '  "join WebFD.VendorContracts.ContractStatus_LU cl on cs.LegalStatus = cl.LegalStatusID and LegalStatusShortDescription = 'Rejected' and cl.Active = 1 " & _
            '  "where ch.Active = 1 and ch.ContractID = @ContractID and UserEmail is not null " & _
            '  " and not exists (select * from WebFD.VendorContracts.EmailStatus e where e.LatestEmailStatus = 'Rejected' and e.Active = 1 and e.ContractID = ch.ContractID) " & _
            '  "           " & _
            '  "declare @Body nvarchar(max) = (select UserName from #MainEmails where ContractID = @ContractID) + '<br><br><b>One of your contracts has been rejected in LESCOR.  </b><br> " & _
            '  "<br> Your contract was rejected by Legal Services; for details regarding the reason for rejection, please contact Legal Services directly.  <br> You can view your contract in the LESCOR ""Browse Contracts"" tab, by searching for Request ID: ' + convert(varchar, @ContractID) + ' <br><br><br> <b> Contract Details: </b>' +  " & _
            '  "'<table border=""1"" width=""80%""> " & _
            '  "<tr> ' + ' <th align=""Left""> Date Submitted </th> <td> ' + isnull((select convert(varchar, DateAdded, 107) from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>' " & _
            '  "+ '<tr><th align=""Left""> Contract Name </th> <td> ' + isnull((select ContractName from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'   " & _
            '  "+ '<tr><th align=""Left""> Department </th> <td> ' + isnull((select Department from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'   " & _
            '  "+ '<tr><th align=""Left""> Contract Purpose </th> <td> ' + isnull((select ContractPurpose from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'  " & _
            '  "+ '<tr><th align=""Left""> VendorName </th> <td> ' + isnull((select VendorName from #MainEmails where ContractID = @ContractID), '') + '</td> </tr> </table> <br><br> <i> Automated Email; Please do not respond </i>' '  " & _
            '  " " & _
            '  "declare @OwnerEmail varchar(max) = (select EmailAddress from #MainEmails where ContractID = @ContractID) " & _
            '  " " & _
            '  "  if @OwnerEmail is not null " & _
            '  " begin " & _
            '  "exec msdb.dbo.sp_send_dbmail  " & _
            '  "@from_address = 'financeweb@northside.com',   " & _
            '  "/*@reply_to = 'Chelsea.Weirich@northside.com',   */" & _
            '  "@recipients = @OwnerEmail ,  " & _
            '  "/*--@recipients = 'satanbarbie@gmail.com' ,     When testing change recipients to single person  */  " & _
            '  "@subject = 'LESCOR Contract Rejected',  " & _
            '  "@body =  @Body, " & _
            '  "@body_format = 'HTML'  " & _
            '  "Insert into WebFD.VendorContracts.EmailStatus (ContractID, LatestEmailStatus, Active, DateLastEmailed) values (" & x & ", 'Rejected', 1, getdate()) " & _
            '  " end "


            'ExecuteSql(BonusSql)


            Search()
            pnlLegalUpdates.Visible = False
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlManageWhat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageWhat.SelectedIndexChanged
        Try
            pnlAdminDepartments.Visible = False
            pnlAdminUsers.Visible = False

            Select Case ddlManageWhat.SelectedValue
                Case "Departments"
                    pnlAdminDepartments.Visible = True
                    PopulateDeptGV()
                Case "Users"
                    pnlAdminUsers.Visible = True
                    PopulateAdminUsers()
            End Select
        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Function DeptView()


        Dim SQL As String = ""

        If Trim(txtAdminDepFilter.Text) <> "" Then
            SQL += "	declare @Phrase varchar(max) = '" & Replace(Trim(txtAdminDepFilter.Text), "'", "''") & "' " & _
            "	declare @Mini varchar(max) " & _
            "	 " & _
            "	if OBJECT_ID('tempdb..#DeptUsersTempy') is not null  " & _
            "           begin  " & _
            "               drop table #DeptUsersTempy  " & _
            "            End  " & _
            "	Select * " & _
            "	, 0 as NameScore, 0 as UserLoginScore  " & _
            "	into #DeptUsersTempy " & _
            "	from ( "
        End If

        SQL += "select * " & _
                    ", case when Active = 1 then 'Remove Dept' " & _
                    "	else 'Add Dept' end as Clicky " & _
                    "from WebFD.VendorContracts.Department_LU d where not exists (select * from WebFD.VendorContracts.Department_LU  d2 where " & _
                    " d2.DepartmentNo = D.DepartmentNo and d2.DepartmentID = d.DepartmentID and (d2.Active > d.Active or (d2.Active = d.Active and d2.DateModified > d.DateModified))) "


        If cbAdminDeptsActive.Checked = False Then
            SQL += " and Active = 1 "
        End If

        If Trim(txtAdminDepFilter.Text) <> "" Then
            SQL += "		 ) z " & _
          "While len(@Phrase) > 0 " & _
          "begin " & _
          " " & _
          "if CHARINDEX(' ', @Phrase, 0) > 0  " & _
          "begin  " & _
          "set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0))))  " & _
          "set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase))))  " & _
          "End  " & _
          "Else  " & _
          "begin  " & _
          "set @Mini = ltrim(@Phrase)  " & _
          "set @Phrase = ''  " & _
          "End  " & _
          " " & _
          "Update #DeptUsersTempy " & _
          "set NameScore = Namescore + 3  " & _
          "where DepartmentName like '% ' + @Mini + ' %' " & _
          "	or DepartmentName like @Mini + ' %' " & _
          "	or DepartmentName like '% ' + @Mini " & _
          "	or DepartmentName = @Mini " & _
          "	 " & _
          "Update #DeptUsersTempy " & _
          "set NameScore = Namescore + 1  " & _
          "where NameScore= 0 and (DepartmentDisplayName like '% ' + @Mini + ' %' " & _
          "	or DepartmentDisplayName like @Mini + ' %' " & _
          "	or DepartmentDisplayName like '% ' + @Mini " & _
          "	or DepartmentDisplayName = @Mini) " & _
          "	 " & _
          "Update #DeptUsersTempy " & _
          "set NameScore = Namescore + 1  " & _
          "where DepartmentName like '%' + @Mini + '%' " & _
          "	 " & _
          "Update #DeptUsersTempy " & _
          "set NameScore = Namescore + .5  " & _
          "where DepartmentName <> DepartmentDisplayName and DepartmentDisplayName like '%' + @Mini + '%' " & _
          " " & _
          "Update #DeptUsersTempy " & _
          "set UserLoginScore = UserLoginScore + 3  " & _
          "where convert(varchar, DepartmentNo) like '% ' + @Mini + ' %' " & _
          "	or convert(varchar, DepartmentNo) like @Mini + ' %' " & _
          "	or convert(varchar, DepartmentNo) like '% ' + @Mini " & _
          "	or convert(varchar, DepartmentNo) = @Mini " & _
          " " & _
          "Update #DeptUsersTempy " & _
          "set UserLoginScore = UserLoginScore + 1  " & _
          "where convert(varchar, DepartmentNo) like '%' + @Mini + '%' " & _
          "	 " & _
          "end " & _
          " " & _
          " " & _
          "Select NameScore * 8 + UserLoginScore * 10 as ord " & _
          ", * " & _
          "from #DeptUsersTempy where  NameScore * 8 + UserLoginScore * 10  > 0" & _
          " " & _
          "order by 1 desc, DepartmentID asc, DateAdded desc "
        Else
            SQL += " order by DepartmentNo asc, DepartmentID asc"
        End If

        'SQL += " order by DepartmentNo asc "

        Return GetData(SQL).DefaultView
    End Function

    Private Sub PopulateDeptGV()

        'DeptView = GetData(x).DefaultView
        'DeptView().Sort = "DepartmentNo ASC"
        gvAdminDepartments.DataSource = DeptView()
        gvAdminDepartments.DataBind()


    End Sub

    Private Sub gvAdminDepartments_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAdminDepartments.PageIndexChanging
        gvAdminDepartments.PageIndex = e.NewPageIndex
        gvAdminDepartments.DataSource = DeptView()
        gvAdminDepartments.DataBind()
    End Sub

    Private Sub gvAdminDepartments_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminDepartments.RowCancelingEdit
        Try
            gvAdminDepartments.EditIndex = -1
            gvAdminDepartments.DataSource = DeptView()
            gvAdminDepartments.DataBind()
            gvDeptUserAccess.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminDepartments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminDepartments.RowCommand

        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            If Commander = "RemoveDept" Then

                Dim PrepSQL As String = "with cte as ( " & _
                    "select top 1 * from WebFD.VendorContracts.Department_LU " & _
                    "where DepartmentID = '" & Replace(Detail_ID, "'", "''") & "' " & _
                    "order by Active desc,  DateModified desc) " & _
                    "update cte set Active = -Active + 1, DateModified = getdate(), ModifiedBy =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)
                PopulateDeptGV()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub gvAdminDepartments_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminDepartments.RowEditing
        Try

            gvAdminDepartments.EditIndex = e.NewEditIndex
            gvAdminDepartments.DataSource = DeptView()
            gvAdminDepartments.DataBind()

            Dim x As String = gvAdminDepartments.DataKeys(gvAdminDepartments.EditIndex).Value

            gvDeptUserAccess.DataSource = GetData("select UserLogin, UserName, Position, " & _
                "case when DepartmentID is null then 'Granted All Departments'	else 'Inactivate' end as Clicky  " & _
                "from (select ROW_NUMBER() over (partition by d2u.UserLogin order by Hierarchy desc) as RN , " & _
                "d2u.UserLogin, Position, isnull(isnull(u.UserDisplayName, u.UserFullName), d2u.UserLogin) as UserName, d2u.DepartmentID " & _
                "from WebFD.VendorContracts.Department_2_User d2u " & _
                "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort and r.Active = 1 " & _
                "join WebFD.VendorContracts.Users u on d2u.UserLogin = u.UserLogin and u.Active = 1 " & _
                "where d2u.Active = 1 and (DepartmentID = '" & x & "' or DepartmentID is null) " & _
                ") x where RN = 1 ")
            gvDeptUserAccess.DataBind()
            gvDeptUserAccess.Visible = True

            Dim txtDescription As TextBox = gvAdminDepartments.Rows(e.NewEditIndex).FindControl("txtDescription")
            Dim lblDescription As Label = gvAdminDepartments.Rows(e.NewEditIndex).FindControl("lblDescription")

            Dim lblShortDesc As Label = gvAdminDepartments.Rows(e.NewEditIndex).FindControl("lblShortDesc")
            Dim txtShortDesc As TextBox = gvAdminDepartments.Rows(e.NewEditIndex).FindControl("txtShortDesc")

            txtDescription.Visible = True
            lblDescription.Visible = False

            txtShortDesc.Visible = True
            lblShortDesc.Visible = False

            For Each canoe As GridViewRow In gvAdminDepartments.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminDepartments_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminDepartments.RowUpdating
        Try
            Dim depid As String = gvAdminDepartments.DataKeys(e.RowIndex).Value.ToString

            Dim txtDescription As TextBox = gvAdminDepartments.Rows(e.RowIndex).FindControl("txtDescription")
            Dim txtShortDesc As TextBox = gvAdminDepartments.Rows(e.RowIndex).FindControl("txtShortDesc")

            Dim y As String

            If Len(Trim(txtShortDesc.Text)) = 0 Then
                y = " null, "
            Else
                y = "'" & Replace(txtShortDesc.Text, "'", "''") & "', "
            End If

            ' 3/12/2018 REWRITTEN CRW
            'Dim Sql As String = "update WebFD.VendorContracts.Department_LU " & _
            '    "set DepartmentName = '" & Replace(txtDescription.Text, "'", "''") & "', DepartmentDisplayName = " & y & " DateModified = getdate(), ModifiedBy = '" & _
            '    Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' where DepartmentID = '" & Replace(depid, "'", "''") & "'"

            Dim SQL As String = "update d set Active = 0, DateModified = getdate() " & _
                "from WebFD.VendorContracts.Department_LU d  " & _
                "where DepartmentID = '" & Replace(depid, "'", "''") & "' and not exists (select * from WebFD.VendorContracts.Department_LU d2 where d.DepartmentID = d2.DepartmentID " & _
                "	and d2.Active >= d.Active and d2.DateModified > d.DateModified) " & _
                "	 " & _
                "insert into  WebFD.VendorContracts.Department_LU " & _
                "select DepartmentID, DepartmentNo, Entity, Facility, '" & Replace(txtDescription.Text, "'", "''") & "', " & y & " 1, dateadded, getdate(), '" & _
                Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' " & _
                "from WebFD.VendorContracts.Department_LU d  " & _
                "where DepartmentID = '" & Replace(depid, "'", "''") & "' and not exists (select * from WebFD.VendorContracts.Department_LU d2 where d.DepartmentID = d2.DepartmentID " & _
                "	and d2.Active >= d.Active and d2.DateModified > d.DateModified) "

            ExecuteSql(SQL)

            gvAdminDepartments.EditIndex = -1
            PopulateDeptGV()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminDepartments_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvAdminDepartments.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = DeptView()

        sorts = e.SortExpression

        If e.SortExpression = Deptmap.Text Then

            If Deptdir.Text = "1" Then
                dv.Sort = sorts + " " + "desc"
                Deptdir.Text = 0
            Else
                dv.Sort = sorts + " " + "asc"
                Deptdir.Text = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            Deptdir.Text = 1
            Deptmap.Text = e.SortExpression
        End If

        gvAdminDepartments.DataSource = dv
        gvAdminDepartments.DataBind()

    End Sub

    Private Sub gvDeptUserAccess_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDeptUserAccess.RowCommand

        Try
            Dim Detail_ID As String = e.CommandArgument
            Dim Commander As String = e.CommandName

            If Commander = "RemoveAccess" Then

                Dim PrepSQL As String = "with cte as ( " & _
                    "select top 1 * from WebFD.VendorContracts.Department_2_User " & _
                    "where DepartmentID = '" & Replace(gvAdminDepartments.DataKeys(gvAdminDepartments.EditIndex).Value, "'", "''") & "' and UserLogin = '" & Replace(Detail_ID, "'", "''") & "' " & _
                    "order by Active desc,  DateModified desc) " & _
                    "update cte set Active = -Active + 1, DateModified = getdate(), ModifiedBy =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'"

                ExecuteSql(PrepSQL)

                gvDeptUserAccess.DataSource = GetData("select UserLogin, UserName, Position " & _
               ", case when DepartmentID is null then 'Granted All Departments' else 'Inactivate' end as Clicky  " & _
               "from (select ROW_NUMBER() over (partition by d2u.UserLogin order by Hierarchy desc) as RN , " & _
               "d2u.UserLogin, Position, isnull(isnull(u.UserDisplayName, u.UserFullName), d2u.UserLogin) as UserName, d2u.DepartmentID " & _
               "from WebFD.VendorContracts.Department_2_User d2u " & _
               "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort and r.Active = 1 " & _
               "left join WebFD.VendorContracts.Users u on d2u.UserLogin = u.UserLogin and u.Active = 1 " & _
               "where d2u.Active = 1 and (DepartmentID = '" & Replace(gvAdminDepartments.DataKeys(gvAdminDepartments.EditIndex).Value, "'", "''") & "' or DepartmentID is null) " & _
               ") x where RN = 1 ")
                gvDeptUserAccess.DataBind()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try



    End Sub

    Private Sub Search()
        Try

            Dim datetry As Date

            If Len(Trim(txtLegalSrchSubStart.Text)) > 0 Then
                If Date.TryParse(txtLegalSrchSubStart.Text, datetry) = False Then
                    explanationlabelLegal.Text = "Initial Search Date must be a valid date format"
                    mpeLegalTab.Show()
                    Exit Sub
                End If
            ElseIf Len(Trim(txtLegalSrchSubEnd.Text)) > 0 Then
                If Date.TryParse(txtLegalSrchSubEnd.Text, datetry) = False Then
                    explanationlabelLegal.Text = "Ending Search Date must be a valid date format"
                    mpeLegalTab.Show()
                    Exit Sub
                End If
            End If
            If Len(Trim(txtLegalSrchSubStart.Text)) > 0 And Len(Trim(txtLegalSrchSubEnd.Text)) > 0 Then
                If CDate(txtLegalSrchSubEnd.Text) < CDate(txtLegalSrchSubStart.Text) Then
                    explanationlabelLegal.Text = "Error: Ending Search Date occurs before Initial Search Date"
                    mpeLegalTab.Show()
                    Exit Sub
                End If
            End If


            Dim x As String
            'If pnlLegalSrchPanel.Visible = True Then
            '    x = AdvancedSearch()
            'Else
            '    x = BasicSearch()
            'End If
            x = AdvancedSearch()
            Dim dv As DataView = LegalView(x)

            Try
                If Legaldir.Text = "1" Then
                    dv.Sort = Legalmap.Text + " " + "asc"
                    Legaldir.Text = "1"
                Else
                    dv.Sort = Legalmap.Text + " " + "desc"
                    Legaldir.Text = 0
                End If
            Catch
                dv.Sort = "ContractID asc"
                Legaldir.Text = "1"
                Legalmap.Text = "ContractID"
            End Try


            gvLegalResults.DataSource = dv
            gvLegalResults.DataBind()

            If dv.Count() = 0 Then
                pnlNoResultsFound.Visible = True
            Else
                pnlNoResultsFound.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    'Private Sub cbLegalShowClosed_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowClosed.CheckedChanged
    '    Search()
    'End Sub

    'Private Sub cbLegalShowPending_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowPending.CheckedChanged
    '    Search()
    'End Sub

    'Private Sub cbLegalShowRejected_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowRejected.CheckedChanged
    '    Search()
    'End Sub

    Private Sub btnLegalAdvSearch_Click(sender As Object, e As EventArgs) Handles btnLegalAdvSearch.Click
        Search()
    End Sub

    'Private Sub ddlLegalQuestionSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLegalQuestionSearch.SelectedIndexChanged
    '    Dim sql5 As String
    '    If ddlLegalQuestionSearch.SelectedIndex = 0 Then
    '        sql5 = " select '(Select Question First)' as Response, -1 as ResponseValue, 0 as ord  "
    '    Else
    '        sql5 = "select '(Select Response)' as Response, '-1' as ResponseValue, 0 as DefAns, 0 as ord union " & _
    '                        "select AnswerText, AnswerValue, DefaultAnswer, 1 from WebFD.VendorContracts.Question_Answer_LU " & _
    '                        "where QuestionID = '" & Replace(ddlLegalQuestionSearch.SelectedValue, "'", "''") & "' and Active = 1 " & _
    '                        "union " & _
    '                        "select Response, Response, 0, 2 from WebFD.VendorContracts.Contract_Answers ca " & _
    '                        "where QuestionID = '" & Replace(ddlLegalQuestionSearch.SelectedValue, "'", "''") & "' " & _
    '                        "and not exists (select * from WebFD.VendorContracts.Question_Answer_LU qa  " & _
    '                        "	where qa.QuestionID = ca.QuestionID and qa.Active=  1 and qa.AnswerValue = ca.Response) " & _
    '                        "	order by ord, DefAns desc, Response "
    '    End If

    '    ddlLegalQuestionResponse.DataSource = GetData(sql5)
    '    ddlLegalQuestionResponse.DataTextField = "Response"
    '    ddlLegalQuestionResponse.DataValueField = "ResponseValue"
    '    ddlLegalQuestionResponse.DataBind()
    'End Sub

    Private Function UserView()

        Dim SQL As String = ""

        If Trim(txtFilterUsers.Text) <> "" Then
            SQL += "	declare @Phrase varchar(max) = '" & Replace(Trim(txtFilterUsers.Text), "'", "''") & "' " & _
            "	declare @Mini varchar(max) " & _
            "	 " & _
            "	if OBJECT_ID('tempdb..#VendorUsersTempy') is not null  " & _
            "           begin  " & _
            "               drop table #VendorUsersTempy  " & _
            "            End  " & _
            "	Select * " & _
            "	, 0 as NameScore, 0 as UserLoginScore  " & _
            "	into #VendorUsersTempy " & _
            "	from ( "
        End If

        SQL += "select *, case when Admin = 0 then 'Grant Admin Rights' else 'Revoke Admin Rights' end as AdminClicky " & _
                ", case when LegalTeam = 0 then 'Grant Legal Rights' else 'Revoke Legal Rights' end as Legal  " & _
                ", case when Active = 0 then 'Add User' else 'Remove User' end as UserClicky  " & _
                "from WebFD.VendorContracts.Users u2 where 1 = 1 and not exists (select * from WebFD.VendorContracts.Users u 	" & _
                "where u.UserLogin = u2.UserLogin and  (u.Active > u2.Active or (u.Active = u2.Active and u.DateModified > u2.DateModified))) "

        If cbAdminUserActives.Checked = False Then
            SQL += " and Active = 1 "
        End If

        If Trim(txtFilterUsers.Text) <> "" Then
            SQL += "		 ) z " & _
          "While len(@Phrase) > 0 " & _
          "begin " & _
          " " & _
          "if CHARINDEX(' ', @Phrase, 0) > 0  " & _
          "begin  " & _
          "set @Mini = rtrim(ltrim(SUBSTRING(@Phrase, 0, CHARINDEX(' ', @Phrase, 0))))  " & _
          "set @Phrase = ltrim(rtrim(SUBSTRING(@Phrase, CHARINDEX(' ', @Phrase, 0)+1, len(@Phrase))))  " & _
          "End  " & _
          "Else  " & _
          "begin  " & _
          "set @Mini = ltrim(@Phrase)  " & _
          "set @Phrase = ''  " & _
          "End  " & _
          " " & _
          "Update #VendorUsersTempy " & _
          "set NameScore = Namescore + 3  " & _
          "where UserFullName like '% ' + @Mini + ' %' " & _
          "	or UserFullName like @Mini + ' %' " & _
          "	or UserFullName like '% ' + @Mini " & _
          "	or UserFullName = @Mini " & _
          "	 " & _
          "Update #VendorUsersTempy " & _
          "set NameScore = Namescore + 1  " & _
          "where NameScore= 0 and (UserDisplayName like '% ' + @Mini + ' %' " & _
          "	or UserDisplayName like @Mini + ' %' " & _
          "	or UserDisplayName like '% ' + @Mini " & _
          "	or UserDisplayName = @Mini) " & _
          "	 " & _
          "Update #VendorUsersTempy " & _
          "set NameScore = Namescore + 1  " & _
          "where UserFullName like '%' + @Mini + '%' " & _
          "	 " & _
          "Update #VendorUsersTempy " & _
          "set NameScore = Namescore + .5  " & _
          "where UserFullName <> UserDisplayName and UserDisplayName like '%' + @Mini + '%' " & _
          " " & _
          "Update #VendorUsersTempy " & _
          "set UserLoginScore = UserLoginScore + 3  " & _
          "where UserLogin like '% ' + @Mini + ' %' " & _
          "	or UserLogin like @Mini + ' %' " & _
          "	or UserLogin like '% ' + @Mini " & _
          "	or UserLogin = @Mini " & _
          " " & _
          "Update #VendorUsersTempy " & _
          "set UserLoginScore = UserLoginScore + 1  " & _
          "where UserLogin like '%' + @Mini + '%' " & _
          "	 " & _
          "end " & _
          " " & _
          " " & _
          "Select NameScore * 10 + UserLoginScore * 6 as ord " & _
          ", * " & _
          "from #VendorUsersTempy where  NameScore * 10 + UserLoginScore * 6  > 0" & _
          " " & _
          "order by 1 desc, DateAdded desc "
        Else
            'SQL += " order by DateAdded desc, LegalName"
        End If

        'SQL += " order by UserLogin "

        Return GetData(SQL).DefaultView
    End Function

    Private Sub PopulateAdminUsers()


        'UserView = GetData(SQL).DefaultView
        gvAdminUsers.DataSource = UserView()
        gvAdminUsers.DataBind()

    End Sub


    Private Sub lbSrchUsr_Click(sender As Object, e As EventArgs) Handles lbSrchUsr.Click
        pnlSrchUser.Visible = True
    End Sub

    Private Sub lbCloseUsrSrch_Click(sender As Object, e As EventArgs) Handles lbCloseUsrSrch.Click
        txtAdminUsrSrch.Text = ""
        lblAdminUsrResults.Text = ""
        pnlSrchUser.Visible = False
    End Sub

    Private Sub txtAdminADUserLogin_TextChanged(sender As Object, e As EventArgs) Handles txtAdminADUserLogin.TextChanged
        Try
            lblAdminADUserName.Text = ""
            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            osearcher.Filter = "(&(samaccountname=" & txtAdminADUserLogin.Text & "))" ' search filter

            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()
            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    lblAdminADUserName.Text = lblAdminADUserName.Text & result.GetDirectoryEntry.Properties("cn").Value
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnAdminUsrSrch_Click(sender As Object, e As EventArgs) Handles btnAdminUsrSrch.Click
        Try
            lblAdminUsrResults.Text = ""
            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            osearcher.Filter = "(&(cn=*" & txtAdminUsrSrch.Text & "*))" ' search filter

            For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                osearcher.PropertiesToLoad.Add(elem.PropertyName)
            Next
            oresult = osearcher.FindAll()
            For Each result In oresult
                If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                    lblAdminUsrResults.Text = lblAdminUsrResults.Text & "Name: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & "<br/>" & _
                      "UserLogin: " & result.GetDirectoryEntry.Properties("samaccountname").Value & "<br/>" & _
                      "Email: " & result.GetDirectoryEntry.Properties("mail").Value & "<br/><br/>"
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub btnAdminAddUser_Click(sender As Object, e As EventArgs) Handles btnAdminAddUser.Click
        If Trim(txtAdminADUserLogin.Text) = "" Then
            explanationlabelAdmin.Text = "No User Login entered"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from WebFD.VendorContracts.Users where UserLogin = '" & Trim(Replace(txtAdminADUserLogin.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This User Login already has access"
            mpeAdminPage.Show()
            Exit Sub
        End If
        Dim SQL As String

        Dim x As String

        If Len(Trim(txtAdminADDisplayName.Text)) > 0 Then
            x = "'" & Trim(txtAdminADDisplayName.Text) & "'"
        Else
            x = "null"
        End If

        SQL = "Insert into WebFD.VendorContracts.Users (UserLogin, UserFullName, UserDisplayName, Active, DateAdded, DateModified, ModifiedBy, Admin, UserEmail, LegalTeam) values ('" & _
                Replace(txtAdminADUserLogin.Text, "'", "''") & "', '" & _
                Trim(Replace(lblAdminADUserName.Text, "'", "''")) & "', " & x & ", 1, getdate(), getdate(), '" & _
                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 0, null, 0)"

        ExecuteSql(SQL)
        SynchWebFDPermissions()

        txtFilterUsers.Text = txtAdminADUserLogin.Text
        txtAdminADUserLogin.Text = ""
        txtAdminADDisplayName.Text = ""
        lblAdminADUserName.Text = ""

        Dim dv As DataView = UserView()
        Try
            If Userdir.Text = "1" Then
                dv.Sort = Usermap.Text + " " + "asc"
                Userdir.Text = "1"
            Else
                dv.Sort = Usermap.Text + " " + "desc"
                Userdir.Text = 0
            End If
        Catch
            Userdir.Text = "1"
            Usermap.Text = "UserLogin"
            dv.Sort = "UserLogin asc"
        End Try

        gvAdminUsers.DataSource = dv
        gvAdminUsers.DataBind()
        Dim NewEditIndex As Integer
        For Each canoe As GridViewRow In gvAdminUsers.Rows
            If gvAdminUsers.DataKeys(canoe.RowIndex).Value = txtFilterUsers.Text Then
                gvAdminUsers.EditIndex = canoe.RowIndex
                NewEditIndex = canoe.RowIndex
            End If
        Next
        gvAdminUsers.DataSource = dv
        gvAdminUsers.DataBind()

        Dim txtShortName As TextBox = gvAdminUsers.Rows(NewEditIndex).FindControl("txtShortName")
        Dim lblShortName As Label = gvAdminUsers.Rows(NewEditIndex).FindControl("lblShortName")

        Dim lblEmail As Label = gvAdminUsers.Rows(NewEditIndex).FindControl("lblEmail")
        Dim txtEmail As TextBox = gvAdminUsers.Rows(NewEditIndex).FindControl("txtEmail")

        txtShortName.Visible = True
        lblShortName.Visible = False

        txtEmail.Visible = True
        lblEmail.Visible = False

        'gvUserDepts.DataSource = GetData("select isnull(convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) , 'All Departments') as Department " & _
        '    ", r.RoleFull " & _
        '    ", 'Remove Access' as 'Clicky', d.DepartmentID, isnull(convert(varchar, d2u.DepartmentID), '-1') + '~' + d2u.Position as Flipper  " & _
        '    "from WebFD.VendorContracts.Users u " & _
        '    "left join WebFD.VendorContracts.Department_2_User d2u on d2u.UserLogin = u.UserLogin and d2u.Active = 1 " & _
        '    "left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
        '    "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
        '    "where u.Active = 1 and u.UserLogin = '" & gvAdminUsers.DataKeys(NewEditIndex).Value & "' ")
        'gvUserDepts.DataBind()
        'gvUserDepts.Visible = True
        'pnlGrantAccess.Visible = True

        For Each canoe As GridViewRow In gvAdminUsers.Rows
            If canoe.RowIndex = NewEditIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next


    End Sub

    Private Sub SynchWebFDPermissions()

        Dim SQL As String = "insert into WebFD.dbo.aspnet_Users " & _
            "select '5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', dar.UserLogin), UserLogin, UserLogin, null, 0, getdate() " & _
            "from WebFD.VendorContracts.Users dar " & _
            "where dar.Active = 1 and not exists (select * from WebFD.dbo.aspnet_Users au  " & _
            "where au.UserName = dar.UserLogin) " & _
            " " & _
            "insert into WebFD.dbo.aspnet_UsersInRoles  " & _
            "select UserId, 'CCF6194E-31B0-C8CF-D525-E063CAB7821D' from " & _
            "WebFD.VendorContracts.Users dar " & _
            "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
            "where dar.Active = 1 and not exists (select * from " & _
            "WebFD.dbo.aspnet_UsersInRoles uir  " & _
            "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
            "where au.UserId = uir.UserId " & _
            "and RoleName = 'VendorContracts') " & _
            " " & _
            "delete from WebFD.dbo.aspnet_UsersInRoles  " & _
            "where UserId not in ( " & _
            "select UserId from " & _
            "WebFD.VendorContracts.Users dar " & _
            "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
            "where dar.Active = 1) and RoleID = 'CCF6194E-31B0-C8CF-D525-E063CAB7821D'  " & _
        "and not exists (select UserId from WebFD.VendorContracts.Users dar " & _
        "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
        "where dar.Active = 1)"

        ' Added not exists clause 7/12/2018 CRW

        ExecuteSql(SQL)

    End Sub

    Private Sub gvAdminUsers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAdminUsers.PageIndexChanging
        Try


            Dim dv As DataView = UserView()
            Try
                If Userdir.Text = "1" Then
                    dv.Sort = Usermap.Text + " " + "asc"
                    'Userdir.Text = "0"
                Else
                    dv.Sort = Usermap.Text + " " + "desc"
                    'Userdir.Text = "1"
                End If
            Catch
                Userdir.Text = "1"
                Usermap.Text = "UserLogin"
                dv.Sort = "UserLogin asc"
            End Try


            gvAdminUsers.PageIndex = e.NewPageIndex
            gvAdminUsers.DataSource = dv
            gvAdminUsers.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAdminUsers.RowCancelingEdit
        Try

            Dim dv As DataView = UserView()
            Try
                If Userdir.Text = "1" Then
                    dv.Sort = Usermap.Text + " " + "asc"
                    Userdir.Text = "1"
                Else
                    dv.Sort = Usermap.Text + " " + "desc"
                    Userdir.Text = 0
                End If
            Catch
                Userdir.Text = "1"
                Usermap.Text = "UserLogin"
                dv.Sort = "UserLogin asc"
            End Try

            gvAdminUsers.EditIndex = -1
            gvAdminUsers.DataSource = dv
            gvAdminUsers.DataBind()

            'gvUserDepts.Visible = False
            'pnlGrantAccess.Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAdminUsers.RowCommand
        Try
            Dim UserLogin As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If Developer.Text = 0 And UserLogin = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") Then
                explanationlabelAdmin.Text = "You cannot update your own permissions"
                mpeAdminPage.Show()
                Exit Sub
            End If


            If varname = "FlipAdmin" Then
                Dim Sql As String = "update WebFD.VendorContracts.Users set Admin = 1 - isnull(Admin, 0), ModifiedBy = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & _
                    "', DateModified = getdate() where UserLogin = '" & Replace(UserLogin, "'", "''") & "'"
                ExecuteSql(Sql)
                PopulateAdminUsers()
            ElseIf varname = "FlipLegal" Then
                Dim Sql As String = "update WebFD.VendorContracts.Users set LegalTeam = 1 - isnull(LegalTeam, 0), ModifiedBy = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & _
                    "', DateModified = getdate() where UserLogin = '" & Replace(UserLogin, "'", "''") & "'"
                ExecuteSql(Sql)
                PopulateAdminUsers()
            ElseIf varname = "FlipActive" Then
                Dim Sql As String = "update u2 set Active = 1 - isnull(Active, 0), ModifiedBy = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & _
                    "', DateModified = getdate() from WebFD.VendorContracts.Users u2 where UserLogin = '" & Replace(UserLogin, "'", "''") & "' and not exists (select * from WebFD.VendorContracts.Users u " & _
                    "	where u.UserLogin = u2.UserLogin and (u.Active > u2.Active or (u.Active = u2.Active and u.DateModified > u2.DateModified)))"
                ExecuteSql(Sql)
                'explanationlabelAdmin.Text = Replace(UserLogin, "'", "''") & "'s access to this webpage has been removed; this user can now be found as 'Inactive'"
                'mpeAdminPage.Show()
                SynchWebFDPermissions()
                PopulateAdminUsers()



            End If

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAdminUsers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbManageDepts As LinkButton = e.Row.FindControl("lbManageDepts")
            lbManageDepts.OnClientClick = "javascript:window.open(""" & "LESCOR%20User%20Details/?SearchLogin=" & gvAdminUsers.DataKeys(e.Row.RowIndex).Value & """, 'User Permission Details', 'height=800,width=800, scrollbars, resizable');"

        End If

    End Sub

    Private Sub gvAdminUsers_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAdminUsers.RowEditing
        Try

            Dim dv As DataView = UserView()
            Try
                If Userdir.Text = "1" Then
                    dv.Sort = Usermap.Text + " " + "asc"
                    Userdir.Text = "1"
                Else
                    dv.Sort = Usermap.Text + " " + "desc"
                    Userdir.Text = 0
                End If
            Catch
                Userdir.Text = "1"
                Usermap.Text = "UserLogin"
                dv.Sort = "UserLogin asc"
            End Try

            gvAdminUsers.EditIndex = e.NewEditIndex
            gvAdminUsers.DataSource = dv
            gvAdminUsers.DataBind()

            Dim txtShortName As TextBox = gvAdminUsers.Rows(e.NewEditIndex).FindControl("txtShortName")
            Dim lblShortName As Label = gvAdminUsers.Rows(e.NewEditIndex).FindControl("lblShortName")

            Dim lblEmail As Label = gvAdminUsers.Rows(e.NewEditIndex).FindControl("lblEmail")
            Dim txtEmail As TextBox = gvAdminUsers.Rows(e.NewEditIndex).FindControl("txtEmail")

            txtShortName.Visible = True
            lblShortName.Visible = False

            txtEmail.Visible = True
            lblEmail.Visible = False

            'gvUserDepts.DataSource = GetData("select isnull(convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) , 'All Departments') as Department " & _
            '    ", r.RoleFull " & _
            '    ", 'Remove Access' as 'Clicky', d.DepartmentID, isnull(convert(varchar, d2u.DepartmentID), '-1') + '~' + d2u.Position as Flipper  " & _
            '    "from WebFD.VendorContracts.Users u " & _
            '    "left join WebFD.VendorContracts.Department_2_User d2u on d2u.UserLogin = u.UserLogin and d2u.Active = 1 " & _
            '    "left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            '    "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
            '    "where u.Active = 1 and u.UserLogin = '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & "' ")
            'gvUserDepts.DataBind()
            'gvUserDepts.Visible = True
            'pnlGrantAccess.Visible = True

            For Each canoe As GridViewRow In gvAdminUsers.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAdminUsers.RowUpdating
        Try
            Dim depid As String = gvAdminUsers.DataKeys(e.RowIndex).Value.ToString

            Dim txtShortName As TextBox = gvAdminUsers.Rows(e.RowIndex).FindControl("txtShortName")
            Dim txtEmail As TextBox = gvAdminUsers.Rows(e.RowIndex).FindControl("txtEmail")

            Dim x As String
            Dim y As String
            If Len(Trim(txtEmail.Text)) = 0 Then
                y = " null, "
            Else
                y = "'" & Trim(Replace(txtEmail.Text, "'", "''")) & "', "

            End If

            If Len(Trim(txtShortName.Text)) = 0 Then
                x = " null, "
            Else
                x = "'" & Trim(Replace(txtShortName.Text, "'", "''")) & "', "

            End If

            Dim Sql As String = "update u2 " & _
                "set UserDisplayName = " & x & " UserEmail = " & y & " DateModified = getdate(), ModifiedBy = '" & _
                Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' from WebFD.VendorContracts.Users u2 where UserLogin = '" & Replace(depid, "'", "''") & _
                "' and not exists (select * from WebFD.VendorContracts.Users u " & _
                    "	where u.UserLogin = u2.UserLogin and (u.Active > u2.Active or (u.Active = u2.Active and u.DateModified > u2.DateModified)))"

            ExecuteSql(Sql)

            gvAdminUsers.EditIndex = -1
            PopulateAdminUsers()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvAdminUsers_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvAdminUsers.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = UserView()

        sorts = e.SortExpression

        If e.SortExpression = Usermap.Text Then
            Try
                If Userdir.Text = "1" Then
                    dv.Sort = Usermap.Text + " " + "asc"
                    Userdir.Text = "0"
                Else
                    dv.Sort = Usermap.Text + " " + "desc"
                    Userdir.Text = 1
                End If
            Catch
                dv.Sort = "UserLogin asc"
                Userdir.Text = "1"
                Usermap.Text = "UserLogin"
            End Try


        Else
            dv.Sort = sorts + " " + "asc"
            Userdir.Text = 0
            Usermap.Text = e.SortExpression
        End If



        gvAdminUsers.DataSource = dv
        gvAdminUsers.DataBind()
    End Sub

    Private Sub cbAdminUserActives_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminUserActives.CheckedChanged
        PopulateAdminUsers()
    End Sub

    'Private Sub gvUserDepts_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUserDepts.RowCommand

    '    Try
    '        Dim Detail_ID As String = e.CommandArgument
    '        Dim Commander As String = e.CommandName

    '        If Commander = "RemoveAccess" Then

    '            Dim PrepSQL As String = "with cte as ( " & _
    '                "select top 1 * from WebFD.VendorContracts.Department_2_User " & _
    '                "where UserLogin = '" & Replace(gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value, "'", "''") & "' and isnull(convert(varchar, DepartmentID), '-1') + '~' + Position = '" & Replace(Detail_ID, "'", "''") & "'" & _
    '                "order by Active desc,  DateModified desc) " & _
    '                "update cte set Active = -Active + 1, DateModified = getdate(), ModifiedBy =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'"

    '            ExecuteSql(PrepSQL)

    '            'gvUserDepts.DataSource = GetData("select isnull(convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) , 'All Departments') as Department " & _
    '            '  ", r.RoleFull " & _
    '            '  ", 'Remove Access'  as 'Clicky', d.DepartmentID, isnull(convert(varchar, d2u.DepartmentID), '-1') + '~' + d2u.Position as Flipper  " & _
    '            '  "from WebFD.VendorContracts.Users u " & _
    '            '  "left join WebFD.VendorContracts.Department_2_User d2u on d2u.UserLogin = u.UserLogin and d2u.Active = 1 " & _
    '            '  "left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
    '            '  "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
    '            '  "where u.Active = 1 and u.UserLogin = '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & "' ")
    '            'gvUserDepts.DataBind()
    '            'gvUserDepts.Visible = True

    '        End If

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try

    'End Sub

    'Private Sub btnGrantAccess_Click(sender As Object, e As EventArgs) Handles btnGrantAccess.Click

    '    Dim x As String
    '    If ddlGrantPosition.SelectedIndex = 0 Then
    '        explanationlabelAdmin.Text = "Select a position to grant to this user"
    '        mpeAdminPage.Show()
    '        Exit Sub
    '    End If

    '    If ddlGrantDepartment.SelectedIndex = 0 Then
    '        explanationlabelAdmin.Text = "Either select a single department, or grant the user access to All Departments for this position"
    '        mpeAdminPage.Show()
    '        Exit Sub
    '    End If

    '    If ddlGrantDepartment.SelectedIndex = 1 Then
    '        x = "Insert into WebFD.VendorContracts.Department_2_User select '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & "', null, RoleShort, 1, getdate(), '" & _
    '            Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' from WebFD.VendorContracts.Roles where Active = 1 and RoleFull = '" & Replace(ddlGrantPosition.SelectedValue, "'", "''") & "' " & _
    '            " and not exists (select * from WebFD.VendorContracts.Department_2_User where Active = 1 and UserLogin = '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & _
    '            "' and DepartmentID is null and Position = '" & Replace(ddlGrantPosition.SelectedValue, "'", "''") & "')"
    '    Else
    '        x = "Insert into WebFD.VendorContracts.Department_2_User select '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & "', d.DepartmentID, RoleShort, 1, getdate(), '" & _
    '            Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' from WebFD.VendorContracts.Roles r " & _
    '            "join WebFD.VendorContracts.Department_LU d on d.DepartmentID = '" & Replace(ddlGrantDepartment.SelectedValue, "'", "''") & "' and d.Active = 1" & _
    '            "where r.Active = 1 and RoleFull = '" & Replace(ddlGrantPosition.SelectedValue, "'", "''") & "' " & _
    '            " and not exists (select * from WebFD.VendorContracts.Department_2_User d2u where Active = 1 and UserLogin = '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & _
    '            "' and d2u.DepartmentID = d.DepartmentID and Position = '" & Replace(ddlGrantPosition.SelectedValue, "'", "''") & "')"
    '    End If

    '    ExecuteSql(x)
    '    gvUserDepts.DataSource = GetData("select isnull(convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) , 'All Departments') as Department " & _
    '              ", r.RoleFull " & _
    '              ", 'Remove Access'  as 'Clicky', d.DepartmentID, isnull(convert(varchar, d2u.DepartmentID), '-1') + '~' + d2u.Position as Flipper  " & _
    '              "from WebFD.VendorContracts.Users u " & _
    '              "left join WebFD.VendorContracts.Department_2_User d2u on d2u.UserLogin = u.UserLogin and d2u.Active = 1 " & _
    '              "left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
    '              "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
    '              "where u.Active = 1 and u.UserLogin = '" & gvAdminUsers.DataKeys(gvAdminUsers.EditIndex).Value & "' ")
    '    gvUserDepts.DataBind()
    '    gvUserDepts.Visible = True
    '    ddlGrantDepartment.SelectedIndex = 0
    '    ddlGrantPosition.SelectedIndex = 0

    'End Sub

    Private Sub ddlDepartmentNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartmentNo.SelectedIndexChanged

        If ddlDepartmentNo.SelectedIndex < 1 Then
            tpSubmitContract.Visible = False
            'tpLegalTab.Visible = False
            PopulateHierarchiesGrid()
            FlipCBVis(True, False)
        Else
            FlipCBVis(True, True)

            CheckResponses(gvSubmissionQuestions)
            CheckResponses(gvSpecialQuestionSubmission)
            If cbAgreements.Checked Then
                tpSubmitContract.Visible = True
                tpLegalTab.Visible = True
                LegalRowing()
                AttachmentDownloadRowing(gvLegalAttachments)
                'ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(btnUpload)
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnUpload)
                'loadgrid()
            End If

            lblDepartmentNoNew.Text = ddlDepartmentNo.SelectedItem.Text
            PopulateHierarchiesGrid()
        End If

    End Sub

    Private Sub AttachmentGrid(q As GridView, x As Integer)
        q.DataSource = GetData("select * from WebFD.VendorContracts.ContractAttachments " & _
                "where ContractID = '" & x & "' and Active = 1 ")
        q.DataBind()
    End Sub

    Private Sub BindSubmissionAttachmentGrid()

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet


        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim Str As String = "select *, 'Download' as downloadtext, 'Remove' as removetext from WebFD.VendorContracts.PendingContractAttachments " & _
                "where ContractID = '" & AttachIndex.Text & "' and Active = 1 and UserID = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "' union " & _
            "select null, null, 'None', null, null, null, null, null, null, null where not exists (select * from WebFD.VendorContracts.PendingContractAttachments " & _
                "where ContractID = '" & AttachIndex.Text & "' and Active = 1 and UserID = '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "')"

            cmd = New SqlCommand(Str, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

            gvSubmissionAttachments.DataSource = ds
            gvSubmissionAttachments.DataBind()

            'gvImages.DataSource = ds

            'gvImages.DataBind()

        End Using


    End Sub

    Protected Sub UploadSubmissionAttachmentFile(sender As Object, e As EventArgs)

        Try
            Dim filename As String
            Try
                filename = Path.GetFileName(fileSubmissionAttachmentInput.PostedFile.FileName)
            Catch
                Exit Sub
            End Try

            If Len(filename) = 0 Then
                Exit Sub
            End If

            If filename.Contains("/") Then
                explantionlabel.Text = "Filename has invalid character"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.Contains("\") Then
                explantionlabel.Text = "Filename has invalid character"
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            If filename.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit image files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit image files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit image files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".jpe", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit image files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit image files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".bat", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit bat files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".mp3", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit sound files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not submit executables"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".wav", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not wav files"
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf filename.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase) Then
                explantionlabel.Text = "Please do not dll files"
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            Dim x As Integer = fileSubmissionAttachmentInput.PostedFile.ContentLength
            If x > 52600000 Then

                Exit Sub
            ElseIf x = 0 Then
                Exit Sub
            End If

            Dim contentType As String = fileSubmissionAttachmentInput.PostedFile.ContentType

            Using fs As Stream = fileSubmissionAttachmentInput.PostedFile.InputStream

                Using br As New BinaryReader(fs)

                    Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                    Dim cmd As New SqlCommand

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If



                        Dim query As String

                        query = "INSERT INTO WebFD.VendorContracts.PendingContractAttachments (ContractID, FileName, ContentType, Content, UserID, DateAdded, Active) VALUES " & _
                            "('" & AttachIndex.Text & "', @FileName, @ContentType, @Content, '" & Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", "") & "', getdate(), 1)"


                        cmd.Connection = conn
                        cmd.CommandText = query

                        cmd.Parameters.AddWithValue("@FileName", filename)
                        cmd.Parameters.AddWithValue("@ContentType", contentType)
                        cmd.Parameters.AddWithValue("@Content", bytes)

                        cmd.ExecuteNonQuery()


                    End Using



                End Using

            End Using

            BindSubmissionAttachmentGrid()
            btnUpload.Focus()
            'Response.Redirect(Request.Url.AbsoluteUri)

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    Private Sub Asynch_Upload_File(sender As Object, e As EventArgs)
        UploadSubmissionAttachmentFile(sender, e)
    End Sub

    Protected Sub DownloadSubmissionAttachmentFile(sender As Object, e As EventArgs)

        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

        Dim bytes As Byte()

        Dim fileName As String, contentType As String

        Dim cmd As New SqlCommand

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.CommandText = "select FileName, Content, ContentType from WebFD.VendorContracts.PendingContractAttachments where AttachmentID=@Id"

            cmd.Parameters.AddWithValue("@Id", id)

            cmd.Connection = conn

            Using sdr As SqlDataReader = cmd.ExecuteReader()

                sdr.Read()

                bytes = DirectCast(sdr("Content"), Byte())

                contentType = sdr("ContentType").ToString()

                fileName = sdr("FileName").ToString()

            End Using


        End Using


        Response.Clear()

        Response.Buffer = True

        Response.Charset = ""

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Response.ContentType = contentType

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)

        Response.BinaryWrite(bytes)

        Response.Flush()

        Response.End()

    End Sub
    Protected Sub DownloadApprovalAttachmentFile(sender As Object, e As EventArgs)

        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

        Dim bytes As Byte()

        Dim fileName As String, contentType As String

        Dim cmd As New SqlCommand

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.CommandText = "select FileName, Content, ContentType from WebFD.VendorContracts.ContractAttachments where AttachmentID=@Id"

            cmd.Parameters.AddWithValue("@Id", id)

            cmd.Connection = conn

            Using sdr As SqlDataReader = cmd.ExecuteReader()

                sdr.Read()

                bytes = DirectCast(sdr("Content"), Byte())

                contentType = sdr("ContentType").ToString()

                fileName = sdr("FileName").ToString()

            End Using


        End Using


        Response.Clear()

        Response.Buffer = True

        Response.Charset = ""

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Response.ContentType = contentType

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)

        Response.BinaryWrite(bytes)

        Response.Flush()

        Response.End()

    End Sub
    Protected Sub DownloadLegalAttachmentFile(sender As Object, e As EventArgs)

        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

        Dim bytes As Byte()

        Dim fileName As String, contentType As String

        Dim cmd As New SqlCommand

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.CommandText = "select FileName, Content, ContentType from WebFD.VendorContracts.ContractAttachments where AttachmentID=@Id"

            cmd.Parameters.AddWithValue("@Id", id)

            cmd.Connection = conn

            Using sdr As SqlDataReader = cmd.ExecuteReader()

                sdr.Read()

                bytes = DirectCast(sdr("Content"), Byte())

                contentType = sdr("ContentType").ToString()

                fileName = sdr("FileName").ToString()

            End Using


        End Using


        Response.Clear()

        Response.Buffer = True

        Response.Charset = ""

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Response.ContentType = contentType

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)

        Response.BinaryWrite(bytes)

        Response.Flush()

        Response.End()

    End Sub


    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        UploadSubmissionAttachmentFile(sender, e)
    End Sub


    Private Sub gvSubmissionAttachments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubmissionAttachments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lnkDownload As LinkButton = e.Row.FindControl("lnkDownload")


            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload)


        End If
    End Sub

    Protected Sub RemoveSubmissionAttachmentFile(sender As Object, e As EventArgs)
        Try
            Dim FileID As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

            ExecuteSql("Update WebFD.VendorContracts.PendingContractAttachments set Active = 0 where AttachmentID = '" & Replace(FileID, "'", "''") & "'")
            BindSubmissionAttachmentGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLegalAttachments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLegalAttachments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lnkDownload As LinkButton = e.Row.FindControl("lnkDownload")

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload)


        End If
    End Sub

    Private Sub gvApprovalAttachments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvApprovalAttachments.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDownload As LinkButton = e.Row.FindControl("lnkDownload")
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload)
        End If
    End Sub

    'Private Sub cbLegalShowQueue_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowQueue.CheckedChanged
    '    Search()
    'End Sub

    'Private Sub ddlLegalRequestor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLegalRequestor.SelectedIndexChanged
    '    Search()
    'End Sub

    'Private Sub lbResetSearch_Click(sender As Object, e As EventArgs) Handles lbResetSearch.Click

    '    ddlLegalAssignedTo.SelectedIndex = -1
    '    ddlLegalRequestor.SelectedIndex = -1

    '    If GetScalar("select count(*) from WebFD.VendorContracts.Users " & _
    '        "where Active = 1 and LegalTeam = 1 and UserLogin =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") > 0 Then
    '        Try
    '            ddlLegalAssignedTo.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
    '        Catch ex As Exception
    '        End Try
    '    Else
    '        Try
    '            ddlLegalRequestor.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
    '        Catch ex As Exception

    '        End Try
    '    End If

    '    txtLegalSearchKeywords.Text = ""
    '    cbLegalShowPending.Checked = False
    '    cbLegalShowQueue.Checked = True
    '    cbLegalShowRejected.Checked = False
    '    cbLegalShowClosed.Checked = False
    '    cbLegalShowQueueH.Checked = True
    '    cbLegalShowQueueL.Checked = True
    '    cbLegalShowQueueM.Checked = True

    '    'pnlLegalSrchPanel.Visible = False

    '    txtLegalContractID.Text = ""
    '    ddlLegalContractCostCenter.SelectedIndex = -1
    '    txtLegalSrchSubStart.Text = ""
    '    txtLegalSrchSubEnd.Text = ""
    '    'ddlLegalQuestionSearch.SelectedIndex = -1
    '    'ddlLegalQuestionResponse.SelectedIndex = -1
    '    'txtLegalQuestionComment.Text = ""

    '    Search()


    'End Sub

    'Private Sub cbLegalShowQueueH_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowQueueH.CheckedChanged
    '    Search()
    'End Sub

    'Private Sub cbLegalShowQueueL_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowQueueL.CheckedChanged
    '    Search()
    'End Sub

    'Private Sub cbLegalShowQueueM_CheckedChanged(sender As Object, e As EventArgs) Handles cbLegalShowQueueM.CheckedChanged
    '    Search()
    'End Sub

    Private Sub btnConfirmApprovalRejection_Click(sender As Object, e As EventArgs) Handles btnConfirmApprovalRejection.Click

        If hiddenRejectorApprove.Text = "Approve" Then
            ExecuteSql("Insert into WebFD.VendorContracts.Contract_Approvals Select ch.ContractID, '" & _
                          Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 'Approved', '" & Replace(Trim(txtApproveComments.Text), "'", "''") & "', getdate(), '" & _
                          Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 1, r.RoleFull from WebFD.VendorContracts.ContractHeader ch " & _
                          "join WebFD.VendorContracts.Department_2_User d2u on ch.DepartmentID = isnull(d2u.DepartmentID, ch.DepartmentID) " & _
                          "join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort and r.Active = 1 " & _
                          "where d2u.Active = 1 " & _
                          "and not exists (select * from WebFD.VendorContracts.Department_2_User d2u2  " & _
                          "		join WebFD.VendorContracts.Roles r2 on d2u2.Position = r2.RoleShort and r2.Active = 1 " & _
                          "		where d2u2.Active = 1 and d2u2.UserLogin = d2u.UserLogin and r2.Hierarchy > r.Hierarchy " & _
                          "		and isnull(d2u.DepartmentID, -1) = isnull(isnull(d2u2.DepartmentID, d2u.DepartmentID), -1) " & _
                          "		) " & _
                          "and ch.ContractID = '" & Replace(lblApproveContractID.Text, "'", "''") & "' and UserLogin = '" & _
                          Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' ")

            LoadWaitingYourApprovalGrid()
            LoadRecentlyReviewedGrid()
            pnlApproval.Visible = False

            txtApproveComments.Text = ""
        Else
            ExecuteSql("Insert into WebFD.VendorContracts.Contract_Approvals Select ch.ContractID, '" & _
               Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 'Deleted', '" & Replace(Trim(txtApproveComments.Text), "'", "''") & "', getdate(), '" & _
               Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "', 1, r.RoleFull from WebFD.VendorContracts.ContractHeader ch " & _
               "join WebFD.VendorContracts.Department_2_User d2u on ch.DepartmentID = isnull(d2u.DepartmentID, ch.DepartmentID) " & _
               "join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort and r.Active = 1 " & _
               "where d2u.Active = 1 " & _
               "and not exists (select * from WebFD.VendorContracts.Department_2_User d2u2  " & _
               "		join WebFD.VendorContracts.Roles r2 on d2u2.Position = r2.RoleShort and r2.Active = 1 " & _
               "		where d2u2.Active = 1 and d2u2.UserLogin = d2u.UserLogin and r2.Hierarchy > r.Hierarchy " & _
               "		and isnull(d2u.DepartmentID, -1) = isnull(isnull(d2u2.DepartmentID, d2u.DepartmentID), -1) " & _
               "		) " & _
               "and ch.ContractID = '" & Replace(lblApproveContractID.Text, "'", "''") & "' and UserLogin = '" & _
               Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "' ")


            Dim BonusSql As String = "Insert into WebFD.VendorContracts.EmailStatus select " & Replace(lblApproveContractID.Text, "'", "''") & ", 'PendingDeptRejection', 1, null where not exists ( " & _
                    "select * from  WebFD.VendorContracts.EmailStatus where Active = 1 and ContractID = '" & Replace(lblApproveContractID.Text, "'", "''") & "' and LatestEmailStatus = 'PendingDeptRejection') "
            ExecuteSql(BonusSql)


            'Dim BonusSql As String = "if OBJECT_ID('tempdb..#MainEmails') is not null " & _
            '    "              begin  " & _
            '    "                 drop table #MainEmails " & _
            '    "              End  " & _
            '    "			   " & _
            '    "declare @ContractID int = " & Replace(lblApproveContractID.Text, "'", "''") & _
            '    "select ch.ContractID, RequesterUserLogin, u.UserEmail as EmailAddress " & _
            '    ", isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as UserName , ch.ContractName " & _
            '    ", ch.ContractPurpose, ch.VendorName, ch.DateAdded " & _
            '    ", isnull(isnull(convert(varchar, d.DepartmentNo) + ' -- ', '') + d.DepartmentName, ch.DepartmentID) as Department " & _
            '    "into #MainEmails " & _
            '    "from WebFD.VendorContracts.ContractHeader ch " & _
            '    "left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin " & _
            '    "left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
            '    "where ch.Active = 1 and ch.ContractID = @ContractID and UserEmail is not null " & _
            '    "           " & _
            '    "declare @Body nvarchar(max) = (select UserName from #MainEmails where ContractID = @ContractID) + '<br><br><b>One of your contracts has been rejected in LESCOR.  </b><br> " & _
            '    "<br> Your contract was rejected by your department; you can view comments in the LESCOR ""Browse Contracts"" tab, by searching for Request ID: ' + convert(varchar, @ContractID) + ' <br><br><br> <b> Contract Details: </b>' +  " & _
            '    "'<table border=""1"" width=""80%""> " & _
            '    "<tr> ' + ' <th align=""Left""> Date Submitted </th> <td> ' + isnull((select convert(varchar, DateAdded, 107) from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>' " & _
            '    "+ '<tr><th align=""Left""> Contract Name </th> <td> ' + isnull((select ContractName from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'   " & _
            '    "+ '<tr><th align=""Left""> Department </th> <td> ' + isnull((select Department from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'   " & _
            '    "+ '<tr><th align=""Left""> Contract Purpose </th> <td> ' + isnull((select ContractPurpose from #MainEmails where ContractID = @ContractID), '') + '</td> </tr>'  " & _
            '    "+ '<tr><th align=""Left""> VendorName </th> <td> ' + isnull((select VendorName from #MainEmails where ContractID = @ContractID), '') + '</td> </tr> </table> <br><br> <i> Automated Email; Please do not respond </i>' '  " & _
            '    " " & _
            '    "declare @OwnerEmail varchar(max) = (select EmailAddress from #MainEmails where ContractID = @ContractID) " & _
            '    " " & _
            '    "  if @OwnerEmail is not null " & _
            '    " begin " & _
            '    "exec msdb.dbo.sp_send_dbmail  " & _
            '    "@from_address = 'financeweb@northside.com',   " & _
            '    "/*@reply_to = 'Chelsea.Weirich@northside.com',   */" & _
            '    "@recipients = @OwnerEmail ,  " & _
            '    "/*--@recipients = 'satanbarbie@gmail.com' ,     When testing change recipients to single person  */  " & _
            '    "@subject = 'LESCOR Contract Rejected',  " & _
            '    "@body =  @Body, " & _
            '    "@body_format = 'HTML'  " & _
            '    " end "

            'ExecuteSql(BonusSql)

            LoadWaitingYourApprovalGrid()
            LoadRecentlyReviewedGrid()
            pnlApproval.Visible = False

            txtApproveComments.Text = ""
        End If
        Search()

    End Sub

    Private Sub btnResetSearch_Click(sender As Object, e As EventArgs) Handles btnResetSearch.Click
        ddlLegalAssignedTo.SelectedIndex = -1
        ddlLegalRequestor.SelectedIndex = -1

        If GetScalar("select count(*) from WebFD.VendorContracts.Users " & _
            "where Active = 1 and LegalTeam = 1 and UserLogin =  '" & Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "'") > 0 Then
            'Try
            '    ddlLegalAssignedTo.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
            'Catch ex As Exception
            'End Try
        Else
            'Removed Default after first beta testing 9/14/2017 CRW
            'Try
            '    ddlLegalRequestor.SelectedValue = Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''")
            'Catch ex As Exception

            'End Try
        End If

        txtLegalSearchKeywords.Text = ""

        cbLegalShowApproved.Checked = False
        If Legal.Text = "1" Then
            cbLegalShowPending.Checked = False
            cbLegalShowRejected.Checked = False
            cbLegalShowClosed.Checked = False
            cbLegalShowQueueH.Checked = True
            cbLegalShowQueueL.Checked = True
            cbLegalShowQueueM.Checked = True
            cbLegalLoadedinMeditract.Checked = False
            cbLegalNoResponse.Checked = False
            cbLegalLegal.Checked = True
            cbLegalNegotiation.Checked = True
            cbLegalPreliminary.Checked = True
        Else
            cbLegalShowPending.Checked = True
            cbLegalShowQueue.Checked = True
            cbLegalShowRejected.Checked = False
            cbLegalShowClosed.Checked = False
        End If


        'pnlLegalSrchPanel.Visible = False

        txtLegalContractID.Text = ""
        ddlLegalContractCostCenter.SelectedIndex = -1
        txtLegalSrchSubStart.Text = ""
        txtLegalSrchSubEnd.Text = ""
        'ddlLegalQuestionSearch.SelectedIndex = -1
        'ddlLegalQuestionResponse.SelectedIndex = -1
        'txtLegalQuestionComment.Text = ""

        Search()

    End Sub

    Private Sub btnFilterUserTable_Click(sender As Object, e As EventArgs) Handles btnFilterUserTable.Click
        Dim dv As DataView = UserView()
        Try
            If Userdir.Text = "1" Then
                dv.Sort = Usermap.Text + " " + "asc"
                Userdir.Text = "1"
            Else
                dv.Sort = Usermap.Text + " " + "desc"
                Userdir.Text = 0
            End If
        Catch
            Userdir.Text = "1"
            Usermap.Text = "UserLogin"
            dv.Sort = "UserLogin asc"
        End Try

        gvAdminUsers.DataSource = dv
        gvAdminUsers.DataBind()

    End Sub

    Private Sub btnAdminDepSearch_Click(sender As Object, e As EventArgs) Handles btnAdminDepSearch.Click
        Dim dv As DataView = DeptView()

        gvAdminDepartments.EditIndex = -1
        Try
            If Deptdir.Text = "1" Then
                dv.Sort = Deptmap.Text + " " + "asc"
                Deptdir.Text = "1"
            Else
                dv.Sort = Deptmap.Text + " " + "desc"
                Deptdir.Text = 0
            End If
        Catch
            If Len(txtAdminDepFilter.Text) > 0 Then
                Deptdir.Text = "1"
                Deptmap.Text = "ord"
                dv.Sort = "ord desc"
            Else
                Deptdir.Text = "1"
                Deptmap.Text = "DepartmentNo"
                dv.Sort = "DepartmentNo asc"
            End If


        End Try
        gvDeptUserAccess.Visible = False
        gvAdminDepartments.DataSource = dv
        gvAdminDepartments.DataBind()
    End Sub

    Private Sub AddDepartment()
        Dim SQL As String

        Dim x As String

        If Len(Trim(txtAdminDepartmentShortName.Text)) > 0 Then
            x = "'" & Trim(txtAdminDepartmentShortName.Text) & "'"
        Else
            x = "null"
        End If

        SQL = "Insert into WebFD.VendorContracts.Department_LU  output Inserted.DepartmentID values ((select isnull(max(DepartmentID), 0) + 1 from WebFD.VendorContracts.Department_LU), '" & _
            Replace(txtAdminDepartment.Text, "'", "''") & "', '" & _
            Trim(Replace(ddlAdminFacility.SelectedValue, "'", "''")) & "', '" & Left(Trim(Replace(ddlAdminFacility.SelectedItem.Text, "'", "''")), 1) & "', '" & _
                Trim(Replace(txtAdminDepartmentName.Text, "'", "''")) & "', " & _
                 x & ", 1, getdate(), getdate(), '" & _
                Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''") & "')"
        Dim s As Integer
        s = GetScalar(SQL)

        txtAdminDepFilter.Text = txtAdminDepartment.Text
        txtAdminDepartment.Text = ""
        txtAdminDepartmentName.Text = ""
        txtAdminDepartmentShortName.Text = ""
        ddlAdminFacility.SelectedIndex = -1

        Dim dv As DataView = DeptView()
        Try
            If Deptdir.Text = "1" Then
                dv.Sort = Deptmap.Text + " " + "asc"
                Deptdir.Text = "1"
            Else
                dv.Sort = Deptmap.Text + " " + "desc"
                Deptdir.Text = 0
            End If
        Catch
            If Len(txtAdminDepFilter.Text) > 0 Then
                Deptdir.Text = "1"
                Deptmap.Text = "ord"
                dv.Sort = "ord desc"
            Else
                Deptdir.Text = "1"
                Deptmap.Text = "DepartmentNo"
                dv.Sort = "DepartmentNo asc"
            End If


        End Try

        Dim NewEditIndex As Integer
        gvAdminDepartments.DataSource = dv
        gvAdminDepartments.DataBind()
        For Each canoe As GridViewRow In gvAdminDepartments.Rows
            If gvAdminDepartments.DataKeys(canoe.RowIndex).Value = s Then
                gvAdminDepartments.EditIndex = canoe.RowIndex
                NewEditIndex = canoe.RowIndex
            End If
        Next
        gvAdminDepartments.DataSource = dv
        gvAdminDepartments.DataBind()


        Dim txtShortDesc As TextBox = gvAdminDepartments.Rows(NewEditIndex).FindControl("txtShortDesc")
        Dim lblShortDesc As Label = gvAdminDepartments.Rows(NewEditIndex).FindControl("lblShortDesc")

        'Dim lblEmail As Label = gvAdminDepartments.Rows(NewEditIndex).FindControl("lblEmail")
        'Dim txtEmail As TextBox = gvAdminDepartments.Rows(NewEditIndex).FindControl("txtEmail")

        txtShortDesc.Visible = True
        lblShortDesc.Visible = False

        'txtEmail.Visible = True
        'lblEmail.Visible = False

        'gvUserDepts.DataSource = GetData("select isnull(convert(varchar, d.DepartmentNo) + ' - ' + isnull(d.DepartmentDisplayName, d.DepartmentName) , 'All Departments') as Department " & _
        '    ", r.RoleFull " & _
        '    ", 'Remove Access' as 'Clicky', d.DepartmentID, isnull(convert(varchar, d2u.DepartmentID), '-1') + '~' + d2u.Position as Flipper  " & _
        '    "from WebFD.VendorContracts.Users u " & _
        '    "left join WebFD.VendorContracts.Department_2_User d2u on d2u.UserLogin = u.UserLogin and d2u.Active = 1 " & _
        '    "left join WebFD.VendorContracts.Department_LU d on d2u.DepartmentID = d.DepartmentID and d.Active = 1 " & _
        '    "left join WebFD.VendorContracts.Roles r on d2u.Position = r.RoleShort " & _
        '    "where u.Active = 1 and u.UserLogin = '" & gvAdminUsers.DataKeys(NewEditIndex).Value & "' ")
        'gvUserDepts.DataBind()
        'gvUserDepts.Visible = True
        'pnlGrantAccess.Visible = True

        For Each canoe As GridViewRow In gvAdminDepartments.Rows
            If canoe.RowIndex = NewEditIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
            ElseIf canoe.RowIndex Mod 2 = 0 Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
            Else
                canoe.BackColor = System.Drawing.Color.White
            End If
        Next
    End Sub

    Private Sub btnAddDepartment_Click(sender As Object, e As EventArgs) Handles btnAddDepartment.Click

        Dim y As Integer
        If Trim(txtAdminDepartment.Text) = "" Then
            explanationlabelAdmin.Text = "No Cost Center Number entered"
            mpeAdminPage.Show()
            Exit Sub
        ElseIf Integer.TryParse(txtAdminDepartment.Text, y) = False Then
            explanationlabelAdmin.Text = "Cost Center Number must be integer value"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If Trim(txtAdminDepartmentName.Text) = "" Then
            explanationlabelAdmin.Text = "Please provide Cost Center Description"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from WebFD.VendorContracts.Department_LU where DepartmentNo = '" & Trim(Replace(txtAdminDepartment.Text, "'", "''")) & _
                     "' and DepartmentName = '" & Trim(Replace(txtAdminDepartmentName.Text, "'", "''")) & "' and Active = 1") > 0 Then
            explanationlabelAdmin.Text = "This Cost Center already exists"
            mpeAdminPage.Show()
            Exit Sub
        End If
        If GetScalar("Select count(*) from WebFD.VendorContracts.Department_LU where DepartmentNo = '" & Trim(Replace(txtAdminDepartment.Text, "'", "''")) & "' and Active = 1") > 0 Then
            lblCCGroupins.Text = "This Cost Center already has Departments listed.  Add Department anyways?"
            mpeMultipleCCGroupings.Show()
            Exit Sub
        End If

        AddDepartment()

    End Sub

    Private Sub cbAdminDeptsActive_CheckedChanged(sender As Object, e As EventArgs) Handles cbAdminDeptsActive.CheckedChanged
        PopulateDeptGV()
    End Sub

    Private Sub gvRecentlyReceivedContracts_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRecentlyReceivedContracts.PageIndexChanging
        Try

            gvRecentlyReceivedContracts.PageIndex = e.NewPageIndex
            gvRecentlyReceivedContracts.DataSource = ApprovalView()
            gvRecentlyReceivedContracts.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

        Private Sub gvRecentlyReceivedContracts_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvRecentlyReceivedContracts.RowCreated
            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
                End If

            Catch ex As Exception

            End Try
        End Sub

        Private Sub gvRecentlyReceivedContracts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRecentlyReceivedContracts.SelectedIndexChanged
            Dim x As String = gvRecentlyReceivedContracts.DataKeys(gvRecentlyReceivedContracts.SelectedIndex).Value.ToString

            For Each canoe As GridViewRow In gvWaitingonYou.Rows
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingApprovalColor)
                canoe.BorderStyle = BorderStyle.NotSet
                canoe.BorderWidth = "1"
            Next

            For Each canoe As GridViewRow In gvRecentlyReceivedContracts.Rows
                If canoe.RowIndex = gvRecentlyReceivedContracts.SelectedIndex Then
                If canoe.Cells(6).Text = "Deleted" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                Else
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                End If

                    canoe.BorderStyle = BorderStyle.Solid
                    canoe.BorderWidth = "3"
                Else
                If canoe.Cells(6).Text = "Deleted" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(RejectedColor)
                Else
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(PendingLegalApprovalColor)
                End If
                    canoe.BorderStyle = BorderStyle.NotSet
                    canoe.BorderWidth = "1"
                End If
            Next

            AttachmentGrid(gvApprovalAttachments, x)

            gvApprovalCurrent.DataSource = GetData("select isnull(isnull(u.UserDisplayName, u.UserFullName), ca.UserLogin) as UserName " & _
                    ", Status, Comment, ca.ModifyDate " & _
                    "from WebFD.VendorContracts.Contract_Approvals ca " & _
                    "left join WebFD.VendorContracts.Users u on ca.UserLogin = u.UserLogin and u.Active = 1 " & _
                    "where ca.ContractID = '" & x & "' and ca.Active = 1 and ca.UserLogin <> 'Default Insert'")

            gvApprovalCurrent.DataBind()

            Dim y As DataTable = GetData("select isnull(isnull(u.UserDisplayName, u.UserFullName), ch.RequesterUserLogin) as Requestor , ch.* " & _
                 ", convert(varchar, DepartmentNo) + ' - ' + DepartmentName as Department, ContractType, cp.ContractParty,  convert(varchar, Acct) + ' - ' + Description as ExpenseDescrip, Comment " & _
                "from WebFD.VendorContracts.ContractHeader ch " & _
                "left join WebFD.VendorContracts.Department_LU d on ch.DepartmentID = d.DepartmentID and d.Active = 1 " & _
                "left join WebFD.VendorContracts.Users u on ch.RequesterUserLogin = u.UserLogin and u.Active = 1 " & _
                "left join WebFD.VendorContracts.ContractType_LU cl on cl.ContractTypeID = ch.ContractTypeID and cl.Active = 1 " & _
                "left join WebFD.VendorContracts.ContractParty_LU cp on ch.ContractingParty = cp.ContractPartyID and cp.Active = 1 " & _
                "left join DWH.Axiom.Acct a on a.Acct = ch.ExpenseAccount and ISMAP  in ('SUP', 'FEE', 'OTH') " & _
                "left join WebFD.VendorContracts.Contract_Comments  cc on cc.ContractID = ch.ContractID and cc.CommentType = 'Submission' and cc.Active = 1 and LegalOnly = 0 " & _
                " where ch.Active = 1 and ch.ContractID = '" & x & "' ")


            If IsDBNull(y(0)("ContractID")) Then
                lblApproveContractID.Text = ""
            Else
                lblApproveContractID.Text = y(0)("ContractID")
            End If

            If IsDBNull(y(0)("Requestor")) Then
                lblApproveRequestor.Text = ""
            Else
                lblApproveRequestor.Text = y(0)("Requestor")
            End If

            If IsDBNull(y(0)("ContractName")) Then
                lblApproveContractName.Text = ""
            Else
                lblApproveContractName.Text = y(0)("ContractName")
            End If

            If IsDBNull(y(0)("Department")) Then
                lblApproveDepartmentNo.Text = ""
            Else
                lblApproveDepartmentNo.Text = (y(0)("Department"))
            End If

            If IsDBNull(y(0)("VendorName")) Then
                lblApproveVendorName.Text = ""
            Else
                lblApproveVendorName.Text = (y(0)("VendorName"))
            End If

            If IsDBNull(y(0)("ContractType")) Then
                lblApproveContractType.Text = ""
            Else
                lblApproveContractType.Text = (y(0)("ContractType"))
            End If

            If IsDBNull(y(0)("ContractLength")) Then
                lblApproveContractLength.Text = ""
            Else
                lblApproveContractLength.Text = (y(0)("ContractLength"))
            End If

            If IsDBNull(y(0)("AnnualContractExpense")) Then
                lblApproveContractCost.Text = ""
            Else
                lblApproveContractCost.Text = (y(0)("AnnualContractExpense"))
            End If

            If IsDBNull(y(0)("AutoRenewal")) Then
                lblApproveAutoRenewal.Text = ""
            Else
                lblApproveAutoRenewal.Text = (y(0)("AutoRenewal"))
            End If

            If IsDBNull(y(0)("RenewalTerm")) Then
                lblApproveRenewalTerm.Text = ""
            Else
                lblApproveRenewalTerm.Text = (y(0)("RenewalTerm"))
            End If

            If IsDBNull(y(0)("ContractPurpose")) Then
                lblApproveContractPurpose.Text = ""
            Else
                lblApproveContractPurpose.Text = (y(0)("ContractPurpose"))
            End If

            If IsDBNull(y(0)("ContractParty")) Then
                lblApproveContractParty.Text = ""
            Else
                lblApproveContractParty.Text = (y(0)("ContractParty"))
            End If

            If IsDBNull(y(0)("ContractingPartyDetail")) Then
                lblApproveContractPartyExplain.Text = ""
            Else
                lblApproveContractPartyExplain.Text = (y(0)("ContractingPartyDetail"))
            End If

            If IsDBNull(y(0)("ExpenseDescrip")) Then
                lblApproveContractBudgetAcct.Text = ""
            Else
                lblApproveContractBudgetAcct.Text = (y(0)("ExpenseDescrip"))
            End If

            If IsDBNull(y(0)("DateAdded")) Then
                lblApproveDate.Text = ""
            Else
                lblApproveDate.Text = (y(0)("DateAdded"))
            End If

            If IsDBNull(y(0)("Comment")) Then
                lblApproveRequestorComments.Text = ""
            Else
                lblApproveRequestorComments.Text = (y(0)("Comment"))
            End If

            loadApprovalgrid(x)
            pnlApproval.Visible = True
            pnlSubApproval.Visible = False
        End Sub

        Private Sub gvRecentlyReceivedContracts_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvRecentlyReceivedContracts.Sorting
            Dim dv As DataView
            Dim sorts As String
            dv = ReviewedView()

            sorts = e.SortExpression

            If e.SortExpression = Reviewedmap.Text Then

                If Revieweddir.Text = "1" Then
                    dv.Sort = sorts + " " + "desc"
                    Revieweddir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    Revieweddir.Text = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                Revieweddir.Text = 1
                Reviewedmap.Text = e.SortExpression
            End If

            gvRecentlyReceivedContracts.DataSource = dv
            gvRecentlyReceivedContracts.DataBind()
        End Sub

        Private Sub btnRefreshApprovalPage_Click(sender As Object, e As EventArgs) Handles btnRefreshApprovalPage.Click
            LoadRecentlyReviewedGrid()
            LoadWaitingYourApprovalGrid()
            pnlApproval.Visible = False
        End Sub

        Private Sub btnConfirmCCGroupings_Click(sender As Object, e As EventArgs) Handles btnConfirmCCGroupings.Click
            AddDepartment()
        End Sub
    End Class