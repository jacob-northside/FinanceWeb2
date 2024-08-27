Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.IO
Imports System.DirectoryServices
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal
Imports FinanceWeb.WebFinGlobal

Public Class WFPPSDelinquency
    Inherits System.Web.UI.Page
    Public Shared ddlset As New DataSet
    Public Shared rwcnt As Integer
    Public Shared admin As Integer = 0
    Public Shared AdminView As New DataView
    Public Shared Adminmap As String
    Public Shared Admindir As Integer
    Private Shared UserName As String
    Public Shared AdminView2 As New DataView
    Public Shared Adminmap2 As String
    Public Shared Admindir2 As Integer
    Public Shared FortView As New DataView
    Public Shared Fortdir As Integer
    Public Shared Fortmap As String
    Public Shared DCorGen As Integer = 0
    Public Shared InstamedView As New DataView
    Public Shared Instameddir As Integer
    Public Shared Instamedmap As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            'CheckRows()

        Else

            admin = 0

            Try
                Dim search As DirectorySearcher
                Dim entry As DirectoryEntry
                Dim temp As String = ""


                entry = New DirectoryEntry("LDAP://DC=northside, DC=local")
                search = New DirectorySearcher(entry)
                search.Filter = "(&(objectClass=user)(samaccountname=" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "))"
                'Dim i As Integer = search.Filter.Length

                If search.Filter.ToString = "(&(objectClass=user)(samaccountname=))" Then
                    Exit Sub
                End If
                Dim AdjOb As SearchResult = search.FindOne()

                temp = AdjOb.GetDirectoryEntry.Properties.Item("cn").Value()
                'For Each AdObj As SearchResult In search.FindAll()
                '    temp = temp & AdObj.GetDirectoryEntry.Properties.Item("cn").Value
                'Next

                UserName = temp

            Catch ex As Exception

            End Try

            If User.Identity.IsAuthenticated And User.IsInRole("WFPPSAdmin") Then
                admin = 1
            End If

            'Select Case Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
            '    Case "cw996788"
            '        admin = 1
            '    Case "mf995052"
            '        admin = 1
            '    Case "sh991605"
            '        admin = 1
            '    Case "dt992475"
            '        admin = 1
            'End Select

            txtDiscCollStartDate.Text = DateAdd(DateInterval.Day, -14, Today)
            txtDiscCollEndDate.Text = Today
            PopulateDCMerchDDL()
            ddlDiscMerch.SelectedValue = " -- noneselected -- "
            FortnightViewer()
            txtIMStartDate.Text = DateAdd(DateInterval.Day, -14, Today)
            txtIMEndDate.Text = Today
            UpdateIMLocationDDL()
            ddlIMOutletTakenAt.SelectedValue = " -- noneselected -- "
            PopulateInstamedGrid()
            RefreshIMMatchPage()

            If admin = 1 Then
                tpAdminReject.Visible = True
                'txtAdminWhichDate.Text = Today.ToShortDateString
                txtDDEndDate.Text = Today.ToShortDateString
                txtDDStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                'FillCBL()
                'PopulateAdminGrid()
                UpdateUserDDL()
                UpdateEntityDDL()
                UpdateLocationDDL()
                PopulatePPSGrid()

            End If




            rwcnt = 0
            'AddPPSRow()
            'AddPPSRow()
        End If
    End Sub


    Sub AddPPSRow()

        Dim MainTable As New Table
        MainTable.ID = "Table1"
        'PlaceHolder1.Controls.Add(MainTable)

        Dim newrow As New TableRow
        Dim datecell As New TableCell
        Dim dateinput As New TextBox
        Dim outletcell As New TableCell
        Dim outlet As New DropDownList
        Dim cash As New TextBox
        Dim cashcell As New TableCell
        Dim manaul As New TextBox
        Dim manualcheckscell As New TableCell
        Dim outletchk As New Label
        Dim outletcheckscell As New TableCell
        Dim doesagree As New DropDownList
        Dim agreecell As New TableCell
        Dim explain As New TextBox
        Dim explaincell As New TableCell

        Dim ys As New ListItem
        Dim ns As New ListItem

        'dateinput.Text = Today.ToShortDateString
        datecell.Controls.Add(dateinput)

        outlet.DataSource = ddlset
        outlet.DataTextField = "Merchant Description"
        outlet.DataValueField = "Merchant Description"
        outlet.DataBind()
        outlet.Font.Size = 9
        outlet.ID = "outlet" & Str(rwcnt)
        outletcell.Controls.Add(outlet)

        cashcell.Controls.Add(cash)
        cash.ID = "cash" & Str(rwcnt)

        manaul.ID = "manual" & Str(rwcnt)
        manualcheckscell.Controls.Add(manaul)

        outletchk.ID = "outlet" & Str(rwcnt)
        outletcheckscell.Controls.Add(outletchk)

        ys.Value = 1
        ys.Text = "Yes"
        ns.Value = 0
        ns.Text = "No"
        doesagree.ID = "doesagree_" & Str(rwcnt)
        doesagree.Items.Add(ys)
        doesagree.Items.Add(ns)
        doesagree.AutoPostBack = True
        agreecell.Controls.Add(doesagree)

        If doesagree.SelectedIndex = 1 Then
            explain.Enabled = True
        Else
            explain.Enabled = False
        End If

        explain.ID = "explain_" & Str(rwcnt)

        explaincell.Controls.Add(explain)

        newrow.Cells.Add(datecell)
        newrow.Cells.Add(outletcell)
        newrow.Cells.Add(cashcell)
        newrow.Cells.Add(manualcheckscell)
        newrow.Cells.Add(outletcheckscell)
        newrow.Cells.Add(agreecell)
        newrow.Cells.Add(explaincell)

        MainTable.Rows.Add(newrow)
        rwcnt += 1
    End Sub
    Sub CheckRows()
        Dim chkcnt As Integer = 0
        Dim x As String
        'While chkcnt < 1

        x = "doesagree" & chkcnt
        Dim MainTable As New Table
        'MainTable = Request.Form(x)
        For Each rw As TableRow In MainTable.Rows
            chkcnt += 1
        Next


        'Dim ddltest As New DropDownList
        'Dim ddlex As New TextBox
        'Dim tst As String
        'tst = Request.Form("ct100$MainContent$doesagree_" & chkcnt)
        'If Request.Form("ct100$MainContent$doesagree_" & chkcnt) = 1 Then
        '    ddlex = MainTable.Rows(chkcnt).Cells(5).FindControl("explain_" & chkcnt)
        '    ddlex.Enabled = True
        'End If

        'chkcnt += 1
        'End While

    End Sub




#Region "Admin2"

    Private Sub gvSubmittedBags_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubmittedBags.PageIndexChanging
        Try

            gvSubmittedBags.PageIndex = e.NewPageIndex
            gvSubmittedBags.DataSource = AdminView2
            gvSubmittedBags.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSubmittedBags_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSubmittedBags.RowCreated
        'Try
        '    If e.Row.RowType = DataControlRowType.DataRow Then

        '        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

        '        'If e.Row.DataItem("Agree") = "0" Then
        '        '    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
        '        'ElseIf e.Row.DataItem("Agree") = "1" Then
        '        '    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
        '        'End If

        '    End If
        'Catch ex As Exception
        '    'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        'End Try
    End Sub

    Private Sub gvSubmittedBags_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubmittedBags.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim DepositBagID As String = gvSubmittedBags.DataKeys(e.Row.RowIndex).Value.ToString()

            Dim gvSubmissionRow As GridView = TryCast(e.Row.FindControl("gvSubmissionRow"), GridView)

            Dim rowSQL As String = "select pps.*, convert(varchar,EODCollectionDate, 107) as EODCollectionShort, " & _
                "case when dl.ID is not null then 1 else 0 end as Ignored, dl.Comment from DWH.WF.PPS_Submissions pps " & _
            "left join DWH.WF.DelinquencyLogger dl on dl.SourceName = 'WF PPS - Row' " & _
            "and dl.SourceID = pps.ID and dl.Active = 1 " & _
            "where pps.Active = 1 " & _
            "and pps.DepositBagID = '{0}'"

            gvSubmissionRow.DataSource = GetData(String.Format(rowSQL, DepositBagID))

            gvSubmissionRow.DataBind()

        End If
    End Sub


    Private Sub gvSubmittedBags_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSubmittedBags.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = AdminView2

        sorts = e.SortExpression

        If e.SortExpression = Adminmap2 Then

            If Admindir2 = 1 Then
                dv.Sort = sorts + " " + "desc"
                Admindir2 = 0
            Else
                dv.Sort = sorts + " " + "asc"
                Admindir2 = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            Admindir2 = 1
            Adminmap2 = e.SortExpression
        End If

        gvSubmittedBags.DataSource = dv
        gvSubmittedBags.DataBind()
    End Sub


    Sub PopulatePPSGridFromDiscrepancy(CollectionDate As Date, MerchantDescription As String)
        Try

            Dim InitSQL As String = "select Entity, DepositBagID, DepositBagNumber, case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end DepositSlipNumber, convert(varchar,DepositDate, 107) DepositDate, DepositDate as dtDeposited, " & _
            "sum(isnull(Cash, 0)) + SUM(isnull(ManualChecks,0)) as Total, SubmissionFullName, case when dl.Active = 1  then 1 else 0 end as Ignore " & _
            ",dl.Comment, sum(case when  pps.EODCollectionDate = '" & Replace(CollectionDate, "'", "''") & "' " & _
            "and pps.OutletTA = '" & Replace(MerchantDescription, "'", "''") & "' then isnull(Cash, 0) + isnull(ManualChecks,0) else 0 end) as RelevantTotal " & _
            ",MIN(convert(int, AgreeToEOD)) as Agree " & _
            "from DWH.WF.PPS_Submissions pps join " & _
             "(select distinct [MerchantDescription], " & _
                    "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
                    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
                    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
                    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
                    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
                    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
                    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
                    "when MerchantID = '184188908992' and StoreID = 440 and TerminalID = 440 then 'Macon Radiation Oncology'  " & _
                    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
                    "from DWH.WF.Merchant_LU " & _
                    "where [MerchantDescription] not like 'ACC.%'  " & _
                    "and [MerchantDescription] not like 'ZZ%'  " & _
                    "and [MerchantID] not like '%E%' ) mp on pps.OutletTA = mp.MerchantDescription      " & _
            "left join DWH.WF.DelinquencyLogger dl on dl.SourceName = 'WF PPS - Bag' " & _
            "and dl.SourceID = convert(varchar, pps.DepositBagID) and dl.Active = 1 " & _
            "where pps.Active = 1 and Exists (select * from DWH.WF.PPS_Submissions pp2 where pp2.DepositBagID = pps.DepositBagID and  " & _
            "pp2.EODCollectionDate = '" & Replace(CollectionDate, "'", "''") & "' " & _
            "and pp2.OutletTA = '" & Replace(MerchantDescription, "'", "''") & "') " & _
            "group by Entity, DepositBagID, DepositDate, dl.Comment, DepositBagNumber, case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end , convert(varchar,DepositDate, 107), SubmissionFullName, case when dl.Active = 1  then 1 else 0 end "

            gvSubmittedBags.Columns(9).Visible = True
            AdminView2 = GetData(InitSQL).DefaultView
            gvSubmittedBags.DataSource = AdminView2

            gvSubmittedBags.DataBind()

            'If AdminView2.Count = 0 Then
            '    lblWFPPSNoRecords.Visible = True
            'Else
            '    lblWFPPSNoRecords.Visible = False
            'End If


            Dim InstamedSQL As String = "select convert(varchar,TransDate, 107) as TransDate, TransDate as TDSort, MerchantDescription, 1 as Checked, " & _
                "PatientAccountNumber, case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end as Amount, TransactionID, CheckAccountType, case when dl.Active = 1  then 1 else 0 end as Ignored, dl.Comment as Comment, RN " & _
            " from " & _
            "( select  " & _
            "cast(DATEADD(hour, 4, cast(case " & _
            "when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then '' " & _
            "else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) as TransDate " & _
            ",  mt.MerchantDescription, im.* " & _
            ",ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
            "from DWH.WF.InstaMed im " & _
            "left join DWH.WF.Merchant_LU mt on im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID " & _
            "and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
            "where PaymentTransactionType in ('Cash') " & _
            "and mt.MerchantDescription = '" & Replace(MerchantDescription, "'", "''") & "' " & _
            ") x " & _
            "left join DWH.WF.DelinquencyLogger dl on convert(varchar, x.RN) = right(SourceName, len(SourceName) - 9) and dl.SourceID = x.TransactionID " & _
            "where TransDate = '" & Replace(CollectionDate, "'", "''") & "' " & _
            "order by TransDate, PatientAccountNumber, TransactionID, Amount, RN "

            InstamedView = GetData(InstamedSQL).DefaultView
            gvInstamed.DataSource = InstamedView
            gvInstamed.DataBind()

            'If InstamedView.Count = 0 Then
            '    lblInstamedNoRecords.Visible = True
            'Else
            '    lblInstamedNoRecords.Visible = False
            'End If

            Dim RevDate As Date
            RevDate = Replace(CollectionDate, "'", "''")


            For Each rowboat As GridViewRow In gvSubmittedBags.Rows
                Dim gvSubmissionRow As GridView = TryCast(rowboat.FindControl("gvSubmissionRow"), GridView)
                For Each canoe As GridViewRow In gvSubmissionRow.Rows
                    If canoe.Cells(2).Text = MonthName(Month(RevDate), True) & " " & RevDate.ToString("dd, yyyy") And canoe.Cells(3).Text.ToUpper = Replace(MerchantDescription, "'", "''").ToUpper Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                        Dim chckBox As CheckBox = canoe.FindControl("chkRow")
                        chckBox.Checked = True
                    End If
                Next
            Next



        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub PopulatePPSGrid()

        Try

            Dim gridsql As String = ""

            Dim DepNumLimiter As String = ""
            If Len(Trim(txtFilterDepBagNum.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtFilterDepBagNum.Text), "'", "''") & "%' "
            End If

            If Len(Trim(txtFilterDepSlipNum.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtFilterDepSlipNum.Text), "'", "''") & "%' "
            End If

            gridsql = "select Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName, " & _
                    "SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, null as RelevantTotal, MIN(convert(int, AgreeToEOD)) as Agree " & _
                    " from DWH.WF.PPS_Submissions wf join " & _
                    "(select distinct [MerchantDescription], " & _
                    "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
                    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
                    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
                    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
                    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
                    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
                    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
                    "when MerchantID = '184188908992' and StoreID = 440 and TerminalID = 440 then 'Macon Radiation Oncology'  " & _
                    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
                    "from DWH.WF.Merchant_LU " & _
                    "where [MerchantDescription] not like 'ACC.%'  " & _
                    "and [MerchantDescription] not like 'ZZ%'  " & _
                    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription      " & _
                    "where Active = 1 and (Entity = '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
                    "and (SubmissionFullName = '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
                    "and DepositDate between '" & Replace(txtDDStartDate.Text, "'", "''") & "' and '" & Replace(txtDDEndDate.Text, "'", "''") & "' " & _
                    "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
                    Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ')) " & _
                    DepNumLimiter &
                    "Group by " & _
                    "Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "


            AdminView2 = GetData(gridsql).DefaultView
            gvSubmittedBags.DataSource = AdminView2
            gvSubmittedBags.DataBind()

            gvSubmittedBags.SelectedIndex = -1

            gvSubmittedBags.Columns(9).Visible = False

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub PopulateInstamedGrid()

        Dim DepNumLimiter As String = ""
        If Len(Trim(txtIMAmountHigh.Text)) > 0 And Len(Trim(txtIMAmountLow.Text)) > 0 Then
            DepNumLimiter = "and Amount between '" & Replace(Trim(txtIMAmountLow.Text), "'", "''") & "' and '" & Replace(Trim(txtIMAmountHigh.Text), "'", "''") & "' "
        ElseIf Len(Trim(txtIMAmountHigh.Text)) > 0 Then
            DepNumLimiter = "and Amount <= '" & Replace(Trim(txtIMAmountHigh.Text), "'", "''") & "' "
        ElseIf Len(Trim(txtIMAmountHigh.Text)) > 0 Then
            DepNumLimiter = "and Amount >= '" & Replace(Trim(txtIMAmountLow.Text), "'", "''") & "' "
        End If

        If Len(Trim(txtIMPAF.Text)) > 0 Then
            DepNumLimiter += "and PatientAccountNumber like '%" & Replace(Trim(txtIMPAF.Text), "'", "''") & "%' "
        End If

        If Len(Trim(txtIMTranID.Text)) > 0 Then
            DepNumLimiter += "and TransactionID like '%" & Replace(Trim(txtIMPAF.Text), "'", "''") & "%' "
        End If

        Dim FilterLocation As String = " -- none selected -- "
        If IsNothing(ddlIMOutletTakenAt.SelectedItem) = False Then
            FilterLocation = Replace(ddlIMOutletTakenAt.SelectedValue.ToString, "'", "''")
        End If

        Dim InstamedSQL As String = "select convert(varchar,TransDate, 107) as TransDate, TransDate as TDSort, MerchantDescription, 0 as Checked, " & _
            "PatientAccountNumber, case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end as Amount, TransactionID, CheckAccountType, case when dl.Active = 1  then 1 else 0 end as Ignored, dl.Comment as Comment, RN " & _
        " from " & _
        "( select  " & _
        "cast(DATEADD(hour, 4, cast(case " & _
        "when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then '' " & _
        "else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) as TransDate " & _
        ",  mt.MerchantDescription, im.* " & _
        ",ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
        "from DWH.WF.InstaMed im " & _
        "left join DWH.WF.Merchant_LU mt on im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID " & _
        "and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
        "where PaymentTransactionType in ('Cash') " & _
        "and (mt.MerchantDescription = '" & FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ') " & _
        ") x " & _
        "left join DWH.WF.DelinquencyLogger dl on convert(varchar, x.RN) = right(SourceName, len(SourceName) - 9) and dl.SourceID = x.TransactionID " & _
        "where TransDate between '" & Replace(Trim(txtIMStartDate.Text), "'", "''") & "' and '" & Replace(Trim(txtIMEndDate.Text), "'", "''") & "' " & _
                DepNumLimiter & _
        "order by TransDate, PatientAccountNumber, TransactionID, Amount, RN "

        InstamedView = GetData(InstamedSQL).DefaultView
        gvInstamed.DataSource = InstamedView
        gvInstamed.DataBind()

    End Sub

    Sub UpdateEntityDDL()

        Try

            Dim DepNumLimiter As String = ""
            If Len(Trim(txtFilterDepBagNum.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtFilterDepBagNum.Text), "'", "''") & "%' "
            End If
            If Len(Trim(txtFilterDepSlipNum.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtFilterDepSlipNum.Text), "'", "''") & "%' "
            End If
            Dim FilterSubmitter As String = " -- none selected -- "
            If IsNothing(ddlFilterSubmitter.SelectedItem) = False Then
                FilterSubmitter = Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''")
            End If

            Dim FilterLocation As String = " -- none selected -- "
            If IsNothing(ddlFilterLocation.SelectedItem) = False Then
                FilterLocation = Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''")
            End If

            Dim sql As String = "select 'Select Entity (optional) ' as lbl, ' -- none selected -- ' as Entity, 0 as ord " & _
                "union " & _
                "select distinct Entity, Entity, 1 " & _
                "from DWH.WF.PPS_Submissions wf join " & _
                "(select distinct [MerchantDescription], " & _
                "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
                "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
                "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
                "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
                "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
                "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
                "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
                "when MerchantID = '184188908992' and StoreID = 440 and TerminalID = 440 then 'Macon Radiation Oncology' " & _
                "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
                "from DWH.WF.Merchant_LU " & _
                "where [MerchantDescription] not like 'ACC.%'  " & _
                "and [MerchantDescription] not like 'ZZ%'  " & _
                "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
                "where Active = 1 " & DepNumLimiter & _
                "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
                "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
                FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
                "order by ord, lbl "


            ddlFilterEntity.DataSource = GetData(sql).DefaultView
            ddlFilterEntity.DataValueField = "Entity"
            ddlFilterEntity.DataTextField = "lbl"
            ddlFilterEntity.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub UpdateUserDDL()

        Try

            Dim DepNumLimiter As String = ""
            If Len(Trim(txtFilterDepBagNum.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtFilterDepBagNum.Text), "'", "''") & "%'"
            End If
            If Len(Trim(txtFilterDepSlipNum.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtFilterDepSlipNum.Text), "'", "''") & "%' "
            End If
            Dim FilterEntity As String = " -- none selected -- "
            If IsNothing(ddlFilterEntity.SelectedItem) = False Then
                FilterEntity = Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''")
            End If
            Dim FilterLocation As String = " -- none selected -- "
            If IsNothing(ddlFilterLocation.SelectedItem) = False Then
                FilterLocation = Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''")
            End If

            Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
                "union " & _
                "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
                "from DWH.WF.PPS_Submissions wf join " & _
                "(select distinct [MerchantDescription], " & _
                "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
                "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
                "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
                "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
                "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
                "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
                "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
                "when MerchantID = '184188908992' and StoreID = 440 and TerminalID = 440 then 'Macon Radiation Oncology'  " & _
                "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
                "from DWH.WF.Merchant_LU " & _
                "where [MerchantDescription] not like 'ACC.%'  " & _
                "and [MerchantDescription] not like 'ZZ%'  " & _
                "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
                "where Active = 1 " & DepNumLimiter & _
                "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
                "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
                FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
                "order by ord, lbl "

            ddlFilterSubmitter.DataSource = GetData(sql).DefaultView
            ddlFilterSubmitter.DataValueField = "SubmissionFullName"
            ddlFilterSubmitter.DataTextField = "lbl"
            ddlFilterSubmitter.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub UpdateLocationDDL()

        Try
            Dim DepNumLimiter As String = ""
            If Len(Trim(txtFilterDepBagNum.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtFilterDepBagNum.Text), "'", "''") & "%'"
            End If
            If Len(Trim(txtFilterDepSlipNum.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtFilterDepSlipNum.Text), "'", "''") & "%' "
            End If
            Dim FilterEntity As String = " -- none selected -- "
            If IsNothing(ddlFilterEntity.SelectedItem) = False Then
                FilterEntity = Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''")
            End If
            Dim FilterSubmitter As String = " -- none selected -- "
            If IsNothing(ddlFilterSubmitter.SelectedItem) = False Then
                FilterSubmitter = Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''")
            End If

            Dim sql As String = "select 'Select Location (optional) ' as lbl, ' -- none selected -- ' as OutletTA, 0 as ord " & _
                "union " & _
                "select distinct OutletTA, OutletTA, 1 " & _
                "from DWH.WF.PPS_Submissions wf join " & _
                "(select distinct [MerchantDescription], " & _
                "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
                "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
                "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
                "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
                "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
                "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
                "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
                "when MerchantID = '184188908992' and StoreID = 440 and TerminalID = 440 then 'Macon Radiation Oncology' " & _
                "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
                "from DWH.WF.Merchant_LU " & _
                "where [MerchantDescription] not like 'ACC.%'  " & _
                "and [MerchantDescription] not like 'ZZ%'  " & _
                "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
                "where Active = 1 " & DepNumLimiter & _
                "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
                "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
                "and DepositDate between '" & Replace(Trim(txtDDStartDate.Text), "'", "''") & "' and '" & Replace(Trim(txtDDEndDate.Text), "'", "''") & "' " & _
                "order by ord, lbl "

            ddlFilterLocation.DataSource = GetData(sql).DefaultView
            ddlFilterLocation.DataValueField = "OutletTA"
            ddlFilterLocation.DataTextField = "lbl"
            ddlFilterLocation.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub UpdateIMLocationDDL()
        Try
            Dim DepNumLimiter As String = ""
            If Len(Trim(txtIMAmountHigh.Text)) > 0 And Len(Trim(txtIMAmountLow.Text)) > 0 Then
                DepNumLimiter = "and Amount between '" & Replace(Trim(txtIMAmountLow.Text), "'", "''") & "' and '" & Replace(Trim(txtIMAmountHigh.Text), "'", "''") & "' "
            ElseIf Len(Trim(txtIMAmountHigh.Text)) > 0 Then
                DepNumLimiter = "and Amount <= '" & Replace(Trim(txtIMAmountHigh.Text), "'", "''") & "' "
            ElseIf Len(Trim(txtIMAmountHigh.Text)) > 0 Then
                DepNumLimiter = "and Amount >= '" & Replace(Trim(txtIMAmountLow.Text), "'", "''") & "' "
            End If

            If Len(Trim(txtIMPAF.Text)) > 0 Then
                DepNumLimiter += "and PatientAccountNumber like '%" & Replace(Trim(txtIMPAF.Text), "'", "''") & "%' "
            End If

            If Len(Trim(txtIMTranID.Text)) > 0 Then
                DepNumLimiter += "and TransactionID like '%" & Replace(Trim(txtIMPAF.Text), "'", "''") & "%' "
            End If

            Dim FilterEntity As String = " -- none selected -- "
            If IsNothing(ddlIMOutletTakenAt.SelectedItem) = False Then
                FilterEntity = Replace(ddlIMOutletTakenAt.SelectedValue.ToString, "'", "''")
            End If

            Dim sql As String = "select 'Select Location (optional) ' as lbl, ' -- none selected -- ' as OutletTA, 0 as ord " & _
                "union " & _
                "select distinct MerchantDescription, MerchantDescription, 1 " & _
                "from DWH.WF.InstaMed im " & _
                "join DWH.WF.Merchant_LU mp on im.TakenAtMerchantID = mp.MerchantID  " & _
                "and im.TakenAtStoreID = mp.StoreID and mp.TerminalID = im.TakenAtTerminalID and mp.MerchantID not like '%E%' " & _
                "where  cast(DATEADD(hour, 4, cast(case  " & _
                "		when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then ''  " & _
                "      else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) " & _
                " between '" & Replace(Trim(txtIMStartDate.Text), "'", "''") & "' and '" & Replace(Trim(txtIMEndDate.Text), "'", "''") & "' " & _
                DepNumLimiter & _
                "order by ord, lbl "


            ddlIMOutletTakenAt.DataSource = GetData(sql).DefaultView
            ddlIMOutletTakenAt.DataValueField = "OutletTA"
            ddlIMOutletTakenAt.DataTextField = "lbl"
            ddlIMOutletTakenAt.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub ddlFilterEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterEntity.SelectedIndexChanged

        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterLocation.SelectedValue.ToString
        UpdateUserDDL()
        UpdateLocationDDL()
        Try
            ddlFilterSubmitter.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = y
        Catch ex As Exception
        End Try

        PopulatePPSGrid()

    End Sub


    Private Sub ddlFilterSubmitter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterSubmitter.SelectedIndexChanged

        Dim x As String = ddlFilterEntity.SelectedValue.ToString
        Dim y As String = ddlFilterLocation.SelectedValue.ToString

        UpdateEntityDDL()
        UpdateLocationDDL()

        Try
            ddlFilterEntity.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = y
        Catch ex As Exception
        End Try

        PopulatePPSGrid()

    End Sub

    Private Sub ddlFilterLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterLocation.SelectedIndexChanged

        Dim x As String = ddlFilterEntity.SelectedValue.ToString
        Dim y As String = ddlFilterSubmitter.SelectedValue.ToString

        UpdateEntityDDL()
        UpdateUserDDL()

        Try
            ddlFilterEntity.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterSubmitter.SelectedValue = y
        Catch ex As Exception
        End Try

        PopulatePPSGrid()

    End Sub

#End Region

    Private Sub txtDDStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtDDStartDate.TextChanged
        PopulatePPSGrid()
    End Sub

    Private Sub txtDDEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtDDEndDate.TextChanged
        PopulatePPSGrid()
    End Sub


    Private Sub btnMPRnvm_Click(sender As Object, e As EventArgs) Handles btnMPRnvm.Click
        For Each canoe As GridViewRow In gvSubmittedBags.Rows
            If canoe.RowIndex = gvSubmittedBags.SelectedIndex Then
                If canoe.Cells(7).Text = "0" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                ElseIf canoe.Cells(7).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                End If
            Else
                If canoe.Cells(7).Text = "0" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                ElseIf canoe.Cells(7).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                End If
            End If
        Next
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

    Private Sub txtFilterDepBagNum_TextChanged(sender As Object, e As EventArgs) Handles txtFilterDepBagNum.TextChanged
        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterEntity.SelectedValue.ToString
        Dim z As String = ddlFilterLocation.SelectedValue.ToString
        UpdateUserDDL()
        UpdateLocationDDL()
        UpdateEntityDDL()
        Try
            ddlFilterSubmitter.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = z
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub txtFilterDepSlipNum_TextChanged(sender As Object, e As EventArgs) Handles txtFilterDepSlipNum.TextChanged
        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterEntity.SelectedValue.ToString
        Dim z As String = ddlFilterLocation.SelectedValue.ToString
        UpdateUserDDL()
        UpdateLocationDDL()
        UpdateEntityDDL()
        Try
            ddlFilterSubmitter.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = z
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub btnGenSrch_Click(sender As Object, e As EventArgs) Handles btnGenSrch.Click
        If btnGenSrch.BackColor = Drawing.Color.Green Then
            GenCell.Visible = False
            btnGenSrch.BackColor = Drawing.Color.Gray
            DCorGen = 0
        Else
            GenCell.Visible = True
            btnGenSrch.BackColor = Drawing.Color.Green
            btnDCSrch.BackColor = Drawing.Color.Gray
            DCCell.Visible = False
            DCorGen = 2
        End If
    End Sub

    Private Sub btnDCSrch_Click(sender As Object, e As EventArgs) Handles btnDCSrch.Click
        If btnDCSrch.BackColor = Drawing.Color.Green Then
            DCCell.Visible = False
            btnDCSrch.BackColor = Drawing.Color.Gray
            DCorGen = 0
        Else
            DCCell.Visible = True
            btnDCSrch.BackColor = Drawing.Color.Green
            btnGenSrch.BackColor = Drawing.Color.Gray
            GenCell.Visible = False
            DCorGen = 1
        End If
    End Sub
    Public Sub FortnightViewer()

        Dim InitSQL As String = "select ISNULL(MerchantID, TakenAtMerchantID) as MerchantID " & _
", ISNULL(StoreID, TakenAtStoreID) as StoreID " & _
", ISNULL(TerminalID, TakenAtTerminalID) as TerminalID " & _
", isnull(EODCollectionDate, TransactionDate) as CollectionDate " & _
", convert(varchar, isnull(EODCollectionDate, TransactionDate), 107) as CollectDisplay " & _
", ISNULL(x.MerchantDescription, im.MerchantDescription) + case when x.MatchID is null then '' else ' - (' + x.MatchName + ')' end  as MerchantDescription " & _
", TotalCollectDaty as Total_PPS_Collections " & _
", TtlForDay as Total_Instamed_Collections " & _
" from ( " & _
"select MerchantID, StoreID, TerminalID, EODCollectionDate, SUM(Cash + ManualChecks) TotalCollectDaty, mt.MerchantDescription, isnull(mlb.MatchID, mlr.MatchID) MatchID, isnull(mlb.MatchName, mlr.MatchName) MatchName  " & _
" from DWH.WF.PPS_Submissions pps " & _
"left join DWH.WF.Merchant_LU mt on pps.OutletTA = mt.MerchantDescription and MerchantID not like '%E%' " & _
"left join DWH.WF.MatchingLogger mlr on mlr.SourceName = 'WF PPS - Row' " & _
"		and mlr.SourceID = pps.ID and mlr.Active = 1 " & _
"left join DWH.WF.MatchingLogger mlb on mlb.SourceName = 'WF PPS - Bag' " & _
"		and mlb.SourceID = pps.DepositBagID and mlb.Active = 1 " & _
"where  " & _
"		not exists ( " & _
"	select * from DWH.WF.DelinquencyLogger dl where dl.SourceName = 'WF PPS - Row' " & _
"		and dl.SourceID = pps.ID and dl.Active = 1) " & _
"		and not exists ( " & _
"	select * from DWH.WF.DelinquencyLogger dl where dl.SourceName = 'WF PPS - Bag' " & _
"		and dl.SourceID = pps.DepositBagID and dl.Active = 1) " & _
"group by MerchantID, StoreID, TerminalID, EODCollectionDate, mt.MerchantDescription, isnull(mlb.MatchID, mlr.MatchID), isnull(mlb.MatchName, mlr.MatchName) " & _
") x  " & _
"full join (select TakenAtMerchantID, TakenAtStoreID, TakenAtTerminalID " & _
", SUM(case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end) TtlForDay " & _
",  cast(DATEADD(hour, 4, cast(case  " & _
"		when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then ''  " & _
"      else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) " & _
"   as TransactionDate " & _
"     ,  mt.MerchantDescription, mli.MatchID, mli.MatchName " & _
"	from  " & _
"	(select *, ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
"	from DWH.WF.InstaMed  " & _
"	) " & _
"	im " & _
"	left join DWH.WF.Merchant_LU mt on im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID  " & _
"		and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
"   left join DWH.WF.MatchingLogger mli on right(SourceName, len(SourceName) - 9) = convert(varchar, RN) and SourceID = TransactionID and mli.Active = 1 " & _
"	where PaymentTransactionType in ('Cash') and PaymentTransactionAction = 'AuthCapt'  " & _
"	and not exists (select * from DWH.WF.DelinquencyLogger where right(SourceName, len(SourceName) - 9) = convert(varchar, RN) and SourceID = TransactionID and Active = 1) " & _
"	group by TakenAtMerchantID, TakenAtStoreID, TakenAtTerminalID, cast(DATEADD(hour, 4, cast(case  " & _
"		when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then ''  " & _
"     else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) " & _
"        , MerchantDescription, mli.MatchID, mli.MatchName " & _
"	) im on im.TakenAtMerchantID = x.MerchantID and im.TakenAtStoreID = x.StoreID and im.TakenAtTerminalID = x.TerminalID " & _
"and x.EODCollectionDate = TransactionDate and isnull(im.MatchID, 0) = isnull(x.MatchID, 0)" & _
"where ISNULL(TransactionDate, EODCollectionDate) between '" & Replace(txtDiscCollStartDate.Text.ToString, "'", "''") & "' and '" & Replace(txtDiscCollEndDate.Text.ToString, "'", "''") & "' " & _
"and (" & Replace(ddlPPSSubmits.SelectedValue.ToString, "'", "''") & " = 1 or x.MerchantID is not null) " & _
"and (" & Replace(ddlShowMatch.SelectedValue.ToString, "'", "''") & " = 1 or ISNULL(x.TotalCollectDaty, 0) <> ISNULL(TtlForDay, 0)) " & _
"and ('" & Replace(ddlDiscMerch.SelectedValue.ToString, "'", "''") & "' = ( case when ISNULL(x.MerchantDescription, im.MerchantDescription) like 'M.%' then 'MedQuest'  " & _
"            when ISNULL(x.MerchantDescription, im.MerchantDescription) like 'MedQuest%'  " & _
"            then 'MedQuest' else ISNULL(MerchantID, TakenAtMerchantID) end " & _
" ) " & _
"or '" & Replace(ddlDiscMerch.SelectedValue.ToString, "'", "''") & "' = ' -- noneselected -- ') " & _
"order by ISNULL(TransactionDate, EODCollectionDate), TakenAtMerchantID, TakenAtStoreID, TakenAtTerminalID "

        FortView = GetData(InitSQL).DefaultView
        gvInstamedvsPPS.DataSource = FortView
        gvInstamedvsPPS.DataBind()

    End Sub

    Private Sub PopulateDCMerchDDL()
        Dim InitSQL As String = "select top 1 ' -- noneselected -- ' as [Merchant ID], 'Select Department Location' as [Merchant Description], 0 as ord from  DWH.WF.Merchant_LU " & _
"            union  " & _
"            select distinct case when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
"            when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID],  " & _
"            case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
"            when [MerchantID] = '184188902995' then 'Cherokee'  " & _
"            when [MerchantID] = '184188901997' then 'Forsyth'  " & _
"            when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
"            when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
"            when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
"            when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
"           when MerchantID = '184188908992' and StoreID = 440 and TerminalID = 440 then 'Macon Radiation Oncology' " & _
"            else 'Other -- (' + [MerchantDescription] + ')' end as [MerchantDescription], 1  " & _
"            from DWH.WF.Merchant_LU  " & _
"             where [MerchantDescription] not like 'ACC.%'  " & _
"            and [MerchantDescription] not like 'ZZ%'  " & _
"            and [MerchantID] not like '%E%'  " & _
"            order by ord, [Merchant Description]  "

        ddlDiscMerch.DataSource = GetData(InitSQL).DefaultView
        ddlDiscMerch.DataTextField = "Merchant Description"
        ddlDiscMerch.DataValueField = "Merchant ID"
        ddlDiscMerch.DataBind()

    End Sub

    Private Sub gvInstamedvsPPS_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvInstamedvsPPS.PageIndexChanging
        Try

            gvInstamedvsPPS.PageIndex = e.NewPageIndex
            gvInstamedvsPPS.DataSource = FortView
            gvInstamedvsPPS.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvInstamedvsPPS_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvInstamedvsPPS.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvInstamedvsPPS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvInstamedvsPPS.SelectedIndexChanged
        Try

            PopulatePPSGridFromDiscrepancy(gvInstamedvsPPS.SelectedDataKey("CollectionDate"), gvInstamedvsPPS.SelectedDataKey("MerchantDescription").ToString)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvInstamedvsPPS_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvInstamedvsPPS.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = FortView

            sorts = e.SortExpression

            If e.SortExpression = Fortmap Then

                If Fortdir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    Fortdir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    Fortdir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                Fortdir = 1
                Fortmap = e.SortExpression
            End If

            gvInstamedvsPPS.DataSource = dv
            gvInstamedvsPPS.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlDiscMerch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDiscMerch.SelectedIndexChanged
        FortnightViewer()
    End Sub

    Private Sub ddlPPSSubmits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPPSSubmits.SelectedIndexChanged
        FortnightViewer()
    End Sub

    Private Sub ddlShowMatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlShowMatch.SelectedIndexChanged
        FortnightViewer()
    End Sub

    Private Sub ddlIMOutletTakenAt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIMOutletTakenAt.SelectedIndexChanged
        PopulateInstamedGrid()
    End Sub

    Private Sub txtIMAmountHigh_TextChanged(sender As Object, e As EventArgs) Handles txtIMAmountHigh.TextChanged
        Dim x As String = ddlIMOutletTakenAt.SelectedValue.ToString

        UpdateIMLocationDDL()
        Try
            ddlIMOutletTakenAt.SelectedValue = x
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub txtIMAmountLow_TextChanged(sender As Object, e As EventArgs) Handles txtIMAmountLow.TextChanged
        Dim x As String = ddlIMOutletTakenAt.SelectedValue.ToString

        UpdateIMLocationDDL()
        Try
            ddlIMOutletTakenAt.SelectedValue = x
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub txtIMEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtIMEndDate.TextChanged
        Dim x As String = ddlIMOutletTakenAt.SelectedValue.ToString

        UpdateIMLocationDDL()
        Try
            ddlIMOutletTakenAt.SelectedValue = x
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub txtIMStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtIMStartDate.TextChanged
        Dim x As String = ddlIMOutletTakenAt.SelectedValue.ToString

        UpdateIMLocationDDL()
        Try
            ddlIMOutletTakenAt.SelectedValue = x
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub txtIMPAF_TextChanged(sender As Object, e As EventArgs) Handles txtIMPAF.TextChanged
        Dim x As String = ddlIMOutletTakenAt.SelectedValue.ToString

        UpdateIMLocationDDL()
        Try
            ddlIMOutletTakenAt.SelectedValue = x
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub txtIMTranID_TextChanged(sender As Object, e As EventArgs) Handles txtIMTranID.TextChanged
        Dim x As String = ddlIMOutletTakenAt.SelectedValue.ToString

        UpdateIMLocationDDL()
        Try
            ddlIMOutletTakenAt.SelectedValue = x
        Catch ex As Exception
        End Try

        PopulatePPSGrid()
    End Sub

    Private Sub gvInstamed_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvInstamed.PageIndexChanging
        Try

            gvInstamed.PageIndex = e.NewPageIndex
            gvInstamed.DataSource = InstamedView
            gvInstamed.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvInstamed_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvInstamed.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = InstamedView

            sorts = e.SortExpression

            If e.SortExpression = Instamedmap Then

                If Instameddir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    Instameddir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    Instameddir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                Instameddir = 1
                Instamedmap = e.SortExpression
            End If

            gvInstamed.DataSource = dv
            gvInstamed.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnMoveIM_Click(sender As Object, e As EventArgs) Handles btnMoveIM.Click
        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = tmblbl.Text

            Dim chk3 As CheckBox
            Dim counter As Integer = 0
            Dim updatesql As String = ""


            For j As Integer = 0 To gvInstamed.Rows.Count - 1
                chk3 = CType(gvInstamed.Rows(j).FindControl("chkIM"), CheckBox)
                If chk3.Checked = True Then
                    counter = counter + 1

                    updatesql = updatesql & "Insert into DWH.WF.TempLogger select '" & Replace(gvInstamed.DataKeys(j).Values(0).ToString, "'", "''") & _
                        "', 'Instamed:" & Replace(gvInstamed.DataKeys(j).Values(1).ToString, "'", "''") & "', '" & Replace(UserName, "'", "''") & _
                        "', getdate() " & _
                        "where not exists (select * from DWH.WF.TempLogger where SourceName = 'Instamed:" & _
                        Replace(gvInstamed.DataKeys(j).Values(1).ToString, "'", "''") & "' and SourceID = '" & _
                        Replace(gvInstamed.DataKeys(j).Values(0).ToString, "'", "''") & "' and SubmittedBy = '" & Replace(UserName, "'", "''") & "'); "

                End If
            Next


            ExecuteSql(updatesql)

            RefreshIMMatchPage()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub btnMovePPS_Click(sender As Object, e As EventArgs) Handles btnMovePPS.Click
        Try

            Dim chk As CheckBox
            Dim chk2 As CheckBox
            Dim gvRowSubmission As GridView
            Dim s As String = ""
            Dim updatesql As String = ""

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = tmblbl.Text

            For i As Integer = 0 To gvSubmittedBags.Rows.Count - 1
                chk = CType(gvSubmittedBags.Rows(i).FindControl("chkBag"), CheckBox)
                If chk.Checked = True Then
                    updatesql = updatesql & "Insert into DWH.WF.TempLogger select  '" & Replace(gvSubmittedBags.DataKeys(i).Value.ToString(), "'", "''") & _
                        "', 'WF PPS - Bag', '" & Replace(UserName, "'", "''") & "', getdate() " & _
                        "where not exists (select * from DWH.WF.TempLogger where SourceName = 'WF PPS - Bag' and SourceID = '" & _
                        Replace(gvSubmittedBags.DataKeys(i).Value.ToString(), "'", "''") & "' and SubmittedBy = '" & Replace(UserName, "'", "''") & "'); "
                Else
                    gvRowSubmission = CType(gvSubmittedBags.Rows(i).FindControl("gvSubmissionRow"), GridView)
                    For j As Integer = 0 To gvRowSubmission.Rows.Count - 1
                        s = s + gvRowSubmission.DataKeys(j).Value.ToString() & ","
                        chk2 = CType(gvRowSubmission.Rows(j).FindControl("chkRow"), CheckBox)
                        If chk2.Checked = True Then
                            updatesql = updatesql & "Insert into DWH.WF.TempLogger select '" & Replace(gvRowSubmission.DataKeys(j).Value.ToString(), "'", "''") & _
                                "', 'WF PPS - Row', '" & Replace(UserName, "'", "''") & "', getdate() " & _
                                "where not exists (select * from DWH.WF.TempLogger where SourceName = 'WF PPS - Row' and SourceID = '" & _
                                Replace(gvRowSubmission.DataKeys(j).Value.ToString(), "'", "''") & "' and SubmittedBy = '" & Replace(UserName, "'", "''") & "'); "
                        End If
                    Next
                End If



            Next

            ExecuteSql(updatesql)

            'RefreshPPSMatchPage()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub RefreshIMMatchPage()

        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
        UserName = tmblbl.Text

        Dim IMTotalSql As String = "select isnull(sum(case when dl.Active = 1  then 0 else case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end end), 0) " & _
            " from " & _
        "( select  " & _
        "cast(DATEADD(hour, 4, cast(case " & _
        "when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then '' " & _
        "else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) as TransDate " & _
        ",  mt.MerchantDescription, im.* " & _
        ",ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
        "from DWH.WF.InstaMed im " & _
        "left join DWH.WF.Merchant_LU mt on im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID " & _
        "and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
        "where PaymentTransactionType in ('Cash') " & _
        ") x " & _
        " join DWH.WF.TempLogger tl on convert(varchar, x.RN) = right(tl.SourceName, len(tl.SourceName) - 9) and tl.SourceID = x.TransactionID and tl.SubmittedBy = '" & Replace(UserName, "'", "''") & "' " & _
        "left join DWH.WF.DelinquencyLogger dl on convert(varchar, x.RN) = right(dl.SourceName, len(dl.SourceName) - 9) and dl.SourceID = x.TransactionID and dl.Active = 1 "

        lblIMWorkBenchTotal.Text = Replace(GetScalar(IMTotalSql), "'", "''")

        Dim InstamedSQL As String = "select convert(varchar,TransDate, 107) as TransDate, TransDate as TDSort, MerchantDescription, case when dl.Active = 1  then 1 else 0 end as Ignored, " & _
            " case when ml.Active = 1  then 1 else 0 end as Matched, case when kl.Active = 1  then 1 else 0 end as Tracking, " & _
            "PatientAccountNumber, case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end as Amount, TransactionID, CheckAccountType, RN " & _
        " from " & _
        "( select  " & _
        "cast(DATEADD(hour, 4, cast(case " & _
        "when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then '' " & _
        "else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) as TransDate " & _
        ",  mt.MerchantDescription, im.* " & _
        ",ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
        "from DWH.WF.InstaMed im " & _
        "left join DWH.WF.Merchant_LU mt on im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID " & _
        "and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
        "where PaymentTransactionType in ('Cash') " & _
        ") x " & _
        " join DWH.WF.TempLogger tl on convert(varchar, x.RN) = right(tl.SourceName, len(tl.SourceName) - 9) and tl.SourceID = x.TransactionID and tl.SubmittedBy = '" & Replace(UserName, "'", "''") & "' " & _
        "left join DWH.WF.DelinquencyLogger dl on convert(varchar, x.RN) = right(dl.SourceName, len(dl.SourceName) - 9) and dl.SourceID = x.TransactionID and dl.Active = 1 " & _
        "left join DWH.WF.MatchingLogger ml on convert(varchar, x.RN) = right(ml.SourceName, len(ml.SourceName) - 9) and ml.SourceID = x.TransactionID and ml.Active = 1 " & _
        "left join DWH.WF.TrackingLogger kl on convert(varchar, x.RN) = right(kl.SourceName, len(kl.SourceName) - 9) and kl.SourceID = x.TransactionID and kl.Active = 1 " & _
        "order by TransDate, PatientAccountNumber, TransactionID, Amount, RN "

        'gvIMWorkBench = GetData(InstamedSQL).DefaultView
        gvIMWorkBench.DataSource = GetData(InstamedSQL).DefaultView
        gvIMWorkBench.DataBind()

    End Sub

  

End Class