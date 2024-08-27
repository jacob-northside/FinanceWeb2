Imports System
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls


Imports FinanceWeb.WebFinGlobal


Public Class DelinquencySubmission
    Inherits System.Web.UI.Page

    Private Shared DepositSlipNumber As String
    Private Shared UserName As String
    Public Shared PPSView As New DataView
    Public Shared PPSdir As Integer
    Public Shared PPSmap As String
    Public Shared AllActivityView As New DataView
    Public Shared AllActivitydir As Integer
    Public Shared AllActivitymap As String
    Public Shared InstamedView As New DataView
    Public Shared Instameddir As Integer
    Public Shared Instamedmap As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then

        Else
            Try

                Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
                UserName = tmblbl.Text

                If Request.QueryString("DSN") IsNot Nothing Then
                    If Request.QueryString("FAC") IsNot Nothing Then
                        lblFacility.Text = Request.QueryString("FAC")
                    Else
                        lblFacility.Text = "-1"
                    End If

                    PopulateFacilityDDL()

                    lblDepositSlip.Text = Request.QueryString("DSN")
                    txtDepositSlip.Text = Request.QueryString("DSN")
                    pnlAllActivity1.Visible = True
                    pnlInstamed1.Visible = False
                    pnlAllActivity2.Visible = True
                    pnlInstamed2.Visible = False
                    pnlAA3.Visible = True
                    RefreshAAPage()
                ElseIf Request.QueryString("TranDate") IsNot Nothing And Request.QueryString("MerchDesc") IsNot Nothing Then
                    pnlAllActivity1.Visible = False
                    pnlInstamed1.Visible = True
                    pnlAllActivity2.Visible = False
                    pnlInstamed2.Visible = True
                    pnlAA3.Visible = False
                    lblMerchDesc.Text = Replace(Request.QueryString("MerchDesc"), "perc26", "&")
                    lblTranDate.Text = Request.QueryString("TranDate")
                    RefreshIMPage(lblMerchDesc.Text, lblTranDate.Text)
                Else
                    'TESTING
                    'lblDepositSlip.Text = "0"
                    'pnlAllActivity1.Visible = True
                    'pnlInstamed1.Visible = False
                    'pnlAllActivity2.Visible = True
                    'pnlInstamed2.Visible = False
                    'pnlAA3.Visible = True
                    'RefreshAAPage(lblDepositSlip.Text)
                    pnlAllActivity1.Visible = False
                    pnlInstamed1.Visible = True
                    pnlAllActivity2.Visible = False
                    pnlInstamed2.Visible = True
                    pnlAA3.Visible = False
                    lblMerchDesc.Text = "C.EMERGENCY DEPT.FC"
                    lblTranDate.Text = "6/25/2016"
                    RefreshIMPage(lblMerchDesc.Text, lblTranDate.Text)
                End If


                'If Request.QueryString("USERID") IsNot Nothing Then
                '    lblUserID.Text = Request.QueryString("USERID")
                '    If lblUserID.Text.StartsWith("NS") Then
                '        lblUserID.Text = Mid(lblUserID.Text, 3)
                '    End If
                'End If




            Catch ex As Exception
                LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
            End Try
        End If
    End Sub

    Private Sub PopulateFacilityDDL()

        Dim ddlsql As String = "select 'View All' as ACFDisplay, '-1' as ACF, 0 as ord union all   " & _
            "select distinct f.Fac_Desc " & _
            ", f.fac_ID as ACF, 1 as ord   " & _
            "from DWH.WF.WFAllActivity wf   " & _
            "left join DWH.WF.AcctNo_2_Facility af on wf.AcctNo =af.AcctNo and af.InactivatedDate is null  and WFPPSFlag = 1  " & _
            "	and wf.ValueDate between isnull(af.EffectiveFrom, '1/1/1800') and isnull(af.EffectiveTo, '12/31/9999') " & _
            "left join DWH.wf.Facility_LU f on af.FacilityID = f.Fac_ID and f.InactivatedDate is null  " & _
            "	and wf.ValueDate between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999') " & _
            "where (( (AcctName like '% - WF PPS' or af.FacilityID is not null ) )  " & _
            "and TranDesc in ('COMMERCIAL DEPOSIT', 'COIN AND CURRENCY DEPOSITED', 'DEPOSIT', 'CASH DEPOSIT') or AsOfDate is null)    " & _
            "order by ord, 1   "

        'Dim ddlsql As String = "select 'View All' as ACFDisplay, 'A' as ACF, 0 as ord union all  " & _
        '    "        select distinct case when ACF = 'H' then 'Atlanta' when ACF = 'C' then 'Cherokee' when ACF = 'F' then 'Forsyth' when ACF = 'M' then 'Medquest'  " & _
        '    "         when ACF = 'G-O' then 'GCS - Other' when ACF = 'G-P' then 'GCS - ProFee' when ACF = 'A-MR' then 'Macon Radiation Oncology'  " & _
        '    "		 when ACF = 'D-MC' then 'Duluth Med Ctr' when ACF = 'L-MC' then 'Gwinnett Med Ctr' when ACF = 'CCC' then 'Center for Cancer Care'  " & _
        '    "		 when ACF = 'L-FC' then 'Gwinnett FC' when ACF = 'D-FC' then 'Duluth FC' when ACF = 'GLAN' then 'Glancy Rehab'  " & _
        '    "        when ACF = 'LMED' then 'Gwinnett Medical Education' " & _
        '    "		 when ACF = 'L-EX' then 'Gwinnett Extended Care' when ACF = 'NGDC' then 'NGDC' when ACF = 'LMED' then 'Gwinnett Grad Med ED'  " & _
        '    "		 else 'Other' end as ACFDisplay  " & _
        '    "        , case when ACF in ('H', 'C', 'F', 'M', 'G-O', 'G-P', 'A-MR', 'D-MC', 'L-MC', 'CCC', 'L-FC', 'D-FC', 'GLAN', 'L-EX', 'NGDC', 'LMED') then ACF else 'O' end as ACF, 1 from (  " & _
        '    "        	select distinct	  " & _
        '    "        	case when AcctNo = '4129405627' then 'M'  " & _
        '    "        	when AcctNo = '4120550116' then 'G-O'  " & _
        '    "        	when AcctNo ='4122336415' then 'G-P'  " & _
        '    "        	when AcctNo = '4067966267' then 'A-MR'  " & _
        '    "        	when AcctNo = '2000026169826' then 'H'  " & _
        '    "        	when AcctNo ='2000026169800' then 'C'  " & _
        '    "        	when AcctNo = '2000026169790' then 'F'  " & _
        '    "			when AcctNo = '4943490920' then 'D-MC' " & _
        '    "			when AcctNo = '4943490896' then 'L-MC' " & _
        '    "			when AcctNo = '4789252582' then 'CCC' " & _
        '    "			when AcctNo = '4943490912' then 'L-FC' " & _
        '    "			when AcctNo = '4943496802' then 'D-FC' " & _
        '    "			when AcctNo = '4807564562' then 'GLAN' " & _
        '    "			when AcctNo = '4807564570' then 'L-EX' " & _
        '    "			when AcctNo = '4556146173' then 'NGDC' " & _
        '    "			when AcctNo = '4789252566' then 'LMED' " & _
        '    "         else  " & _
        '    "        substring(AcctName, 1, 3) end as ACF		  " & _
        '    "        from DWH.WF.WFAllActivity wf  " & _
        '    "        	where ( AcctNo in ('2000026169826', '2000026169800', '2000026169790', '4129405627', '4120550116', '4122336415', '4067966267' " & _
        '    ", '4943490920', '4943490896', '4789252582', '4943490912', '4943496802', '4807564562', '4807564570', '4556146173', '4789252566') " & _
        '    "        and TranDesc in ('COMMERCIAL DEPOSIT', 'COIN AND CURRENCY DEPOSITED', 'DEPOSIT', 'CASH DEPOSIT') or AsOfDate is null)   " & _
        '    "        ) x   " & _
        '    "        order by ord, 1  "

        ' updated 9/26/2019 CRW re: new MerchantID_LU and Gwinnett
        'Dim ddlsql As String = "select 'View All' as ACFDisplay, 'A' as ACF, 0 as ord union all " & _
        '"select case when ACF = 'H' then 'Atlanta' when ACF = 'C' then 'Cherokee' when ACF = 'F' then 'Forsyth' when ACF = 'M' then 'Medquest' " & _
        '" when ACF = 'G-O' then 'GCS - Other' when ACF = 'G-P' then 'GCS - ProFee' when ACF = 'A-MR' then 'Macon Radiation Oncology' else 'Other' end as ACFDisplay " & _
        '", case when ACF in ('H', 'C', 'F', 'M', 'G-O', 'G-P', 'A-MR') then ACF else 'O' end as ACF, 1 from ( " & _
        '"	select distinct	 " & _
        '"	case when AcctNo = '4129405627' then 'M' " & _
        '"	when AcctNo = '4120550116' then 'G-O' " & _
        '"	when AcctNo ='4122336415' then 'G-P' " & _
        '"	when AcctNo = '4067966267' then 'A-MR' " & _
        '"	when AcctNo = '2000026169826' then 'H' " & _
        '"	when AcctNo ='2000026169800' then 'C' " & _
        '"	when AcctNo = '2000026169790' then 'F' " & _
        '" else " & _
        '"substring(AcctName, 1, 3) end as ACF		 " & _
        '"from DWH.WF.WFAllActivity wf " & _
        '"	where ( AcctNo in ('4129405627', '4120550116', '4122336415', '4067966267', '2000026169800', '2000026169790', '2000026169826') " & _
        '"and TranDesc in ('COMMERCIAL DEPOSIT', 'COIN AND CURRENCY DEPOSITED', 'DEPOSIT', 'CASH DEPOSIT') or AsOfDate is null)  " & _
        '") x  " & _
        '"order by ord, 1 "

        'Retired 11/14/2018 CRW re: BAI Files and email from Sarah Hammann from 11/13/2018
        '        Dim ddlsql As String = "select 'View All' as ACFDisplay, 'A' as ACF, 0 as ord union all " & _
        '"select case when ACF = 'H' then 'Atlanta' when ACF = 'C' then 'Cherokee' when ACF = 'F' then 'Forsyth' when ACF = 'M' then 'Medquest' " & _
        '" when ACF = 'G-O' then 'GCS - Other' when ACF = 'G-P' then 'GCS - ProFee' when ACF = 'A-MR' then 'Macon Radiation Oncology' else 'Other' end as ACFDisplay " & _
        '", case when ACF in ('H', 'C', 'F', 'M', 'G-O', 'G-P', 'A-MR') then ACF else 'O' end as ACF, 1 from ( " & _
        '"	select distinct	 " & _
        '"	case when AcctNo = '4129405627' then 'M' " & _
        '"	when AcctNo = '4120550116' then 'G-O' " & _
        '"	when AcctNo ='4122336415' then 'G-P' " & _
        '"	when AcctNo = '4067966267' then 'A-MR' " & _
        '" else " & _
        '"right(LEFT(AcctName, 3), 1) end as ACF		 " & _
        '"from DWH.WF.WFAllActivity wf " & _
        '"	where ((AcctName like '% - WF PPS' or  AcctNo in ('4129405627', '4120550116', '4122336415', '4067966267')) " & _
        '"and TranDesc in ('COMMERCIAL DEPOSIT', 'COIN AND CURRENCY DEPOSITED') or AsOfDate is null)  " & _
        '") x  " & _
        '"order by ord, 1 "

        '        Dim ddlsql As String = "select 'View All' as ACFDisplay, 'A' as ACF, 0 as ord union all " & _
        '"select case when ACF = 'H' then 'Atlanta' when ACF = 'C' then 'Cherokee' when ACF = 'F' then 'Forsyth' when ACF = 'M' then 'Medquest' else 'Other' end as ACFDisplay " & _
        '", case when ACF in ('H', 'C', 'F', 'M') then ACF else 'O' end as ACF, 1 from ( " & _
        '"	select distinct	 " & _
        '"	case when AcctNo = '4129405627' then 'M' else " & _
        '"	right(LEFT(AcctName, 3), 1) end as ACF	 " & _
        '"from DWH.WF.WFAllActivity wf " & _
        '"	where ((AcctName like '% - WF PPS' or  AcctNo in ('4129405627', '4120550116', '4122336415', '4067966267')) " & _
        '"and TranDesc in ('COMMERCIAL DEPOSIT', 'COIN AND CURRENCY DEPOSITED') or AsOfDate is null)  " & _
        '") x  " & _
        '"order by ord, 1 "


        ddlFacility.DataSource = GetData(ddlsql)
        ddlFacility.DataValueField = "ACF"
        ddlFacility.DataTextField = "ACFDisplay"
        ddlFacility.DataBind()

        Dim z As String = "-1"
        Try
            z = lblFacility.Text
            ddlFacility.SelectedValue = z
        Catch ex As Exception

        End Try

    End Sub


    Private Sub RefreshAAPage()
        Try

            Dim DSN As String = Trim(txtDepositSlip.Text)
            Dim x As Integer

            x = GetScalar("select * from DWH.WF.DelinquencyLogger where Active = 1 and SourceName = 'Deposit Slip Number' and SourceID = '" & Replace(DSN, "'", "''") & "'")

            If x > 0 Then
                pnlDepositSlip.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                pnlDepositSlip.BorderColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                lblDepSlipActive.Visible = True
                btnIgnoreSlip.Text = "Mark Deposit Slip as Valid"
                lblExplainIgnoreDepositSlip.Text = "*All Bags submitted with this Deposit Slip in the future will show up on Delinquency log"
            Else
                pnlDepositSlip.BackColor = Drawing.Color.White
                pnlDepositSlip.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
                lblDepSlipActive.Visible = False
                btnIgnoreSlip.Text = "Mark Deposit Slip as Ignored"
                lblExplainIgnoreDepositSlip.Text = "*All Bags submitted with this Deposit Slip in the future will not show up on Delinquency log"
            End If

            Dim InitSQL As String = "select DepositBagID, DepositBagNumber, case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end DepositSlipNumber, convert(varchar,DepositDate, 107) DepositDate, DepositDate as DDSort, " & _
            "sum(isnull(Cash, 0)) + SUM(isnull(ManualChecks,0)) as BagTotal, SubmissionFullName, case when dl.Active = 1 then 1 else 0 end as Ignore " & _
            ",dl.Comment, null as RelevantTotal " & _
            "from DWH.WF.PPS_Submissions pps " & _
            "left join DWH.wf.Merchant_ID_LU mil on pps.PPSMerchantID = mil.MerchantID and pps.PPSStoreID = mil.StoreID  " & _
            "	and pps.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null 				 " & _
            "	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') 				 " & _
            "left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null 		 " & _
            "			and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  				 " & _
            "left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null 					 " & _
            "	and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
            "left join DWH.WF.DelinquencyLogger dl on dl.SourceName = 'WF PPS - Bag' " & _
            "and dl.SourceID = convert(varchar, pps.DepositBagID) and dl.Active = 1 " & _
            "where pps.Active = 1 "

            If ddlFacility.SelectedIndex > 0 Then
                InitSQL += " and f.Fac_ID = '" & Replace(ddlFacility.SelectedValue, "'", "''") & "' "
            End If

            InitSQL += " and (case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end) = '" & Replace(DSN, "'", "''") & "' " & _
            "group by DepositBagID, DepositDate, dl.Comment, DepositBagNumber, case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end , convert(varchar,DepositDate, 107), SubmissionFullName, case when dl.Active = 1  then 1 else 0 end "

            PPSView = GetData(InitSQL).DefaultView
            gvPPS_Submissions.DataSource = PPSView

            gvPPS_Submissions.DataBind()
            gvPPS_Submissions.Columns(4).Visible = False

            If PPSView.Count = 0 Then
                lblWFPPSNoRecords.Visible = True
            Else
                lblWFPPSNoRecords.Visible = False
            End If


            Dim WFAllActivitySQL As String = "select convert(varchar, convert(date, convert(varchar, AsOfDate), 112), 107) AsOfDate, BankName, AcctName, " & _
                "round(NetAmount, 2) as  NetAmount, convert(date, convert(varchar, AsOfDate), 112) as AODSort, CustomerRefNo " & _
            ", convert(varchar,ValueDate, 107) ValueDate, ValueDate as VDSort, UniqueActivityID  " & _
            ", case when dl.Active = 1  then 1 when dl2.Active = 1  then 1 else 0 end as Ignored  " & _
            ", case when dl.Comment is not null and dl2.Comment is not null then dl.Comment + '; ' + dl2.Comment else ISNULL(dl.Comment, dl2.Comment) end as Comment  " & _
            "from DWH.WF.WFAllActivity wf  " & _
            "left join DWH.WF.AcctNo_2_Facility af on wf.AcctNo =af.AcctNo and af.InactivatedDate is null and WFPPSFlag = 1   " & _
            "	and wf.ValueDate between isnull(af.EffectiveFrom, '1/1/1800') and isnull(af.EffectiveTo, '12/31/9999') " & _
            "left join DWH.wf.Facility_LU f on af.FacilityID = f.Fac_ID and f.InactivatedDate is null  " & _
            "	and wf.ValueDate between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999') " & _
            "left join DWH.WF.DelinquencyLogger dl on (case when charindex('0', dl.SourceID) > 0 and PATINDEX('%[1-9]%', dl.SourceID) > 0 " & _
            "then SUBSTRING(dl.SourceID, PATINDEX('%[1-9]%', dl.SourceID), LEN(dl.SourceID) - PATINDEX('%[1-9]%', dl.SourceID) + 1) " & _
            "when charindex('0', dl.SourceID) > 0 then '0' " & _
            "else dl.SourceID end) = (case when charindex('0', wf.CustomerRefNo) > 0 and PATINDEX('%[1-9]%', wf.CustomerRefNo) > 0  " & _
            "then SUBSTRING(wf.CustomerRefNo, PATINDEX('%[1-9]%', wf.CustomerRefNo), LEN(wf.CustomerRefNo) - PATINDEX('%[1-9]%', wf.CustomerRefNo) + 1)  " & _
            "when charindex('0', wf.CustomerRefNo) > 0 then '0' " & _
            " else wf.CustomerRefNo end) and dl.SourceName = 'Deposit Slip Number' and dl.Active = 1 " & _
            "left join DWH.WF.DelinquencyLogger dl2 on dl2.SourceID = CONVERT(varchar(64), wf.UniqueActivityID, 1)  " & _
            "and dl2.SourceName = 'WFAllActivity - UAID' and dl2.Active = 1  " & _
            "where (case when charindex('0', wf.CustomerRefNo) > 0 and PATINDEX('%[1-9]%', wf.CustomerRefNo) > 0  " & _
            "then SUBSTRING(wf.CustomerRefNo, PATINDEX('%[1-9]%', wf.CustomerRefNo), LEN(wf.CustomerRefNo) - PATINDEX('%[1-9]%', wf.CustomerRefNo) + 1) " & _
            "when charindex('0', wf.CustomerRefNo) > 0 then '0' " & _
            "else wf.CustomerRefNo end) = '" & DSN & "' "

            If ddlFacility.SelectedIndex = 0 Then
                WFAllActivitySQL += "and  (AcctName like '% - WF PPS' or af.FacilityID is not null )  "
            Else
                WFAllActivitySQL += "and f.Fac_ID = '" & Replace(ddlFacility.SelectedValue, "'", "''") & "' "
            End If

            WFAllActivitySQL += "and TranDesc in ('COMMERCIAL DEPOSIT', 'COIN AND CURRENCY DEPOSITED', 'DEPOSIT', 'CASH DEPOSIT') "

            AllActivityView = GetData(WFAllActivitySQL).DefaultView
            gvWFAllActivity.DataSource = AllActivityView
            gvWFAllActivity.DataBind()

            If AllActivityView.Count = 0 Then
                lblAANoRecords.Visible = True
            Else
                lblAANoRecords.Visible = False
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Private Sub RefreshIMPage(MerchDesc As String, TranDate As String)
        Try

            'Dim x As Integer

            'x = GetScalar("select * from DWH.WF.DelinquencyLogger where Active = 1 and SourceName = 'Deposit Slip Number' and SourceID = '" & Replace(DSN, "'", "''") & "'")

            'If x > 0 Then
            '    pnlDepositSlip.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
            '    pnlDepositSlip.BorderColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
            '    lblDepSlipActive.Visible = True
            '    btnIgnoreSlip.Text = "Mark Deposit Slip as Valid"
            '    lblExplainIgnoreDepositSlip.Text = "*All Bags submitted with this Deposit Slip in the future will show up on Delinquency log"
            'Else
            '    pnlDepositSlip.BackColor = Drawing.Color.White
            '    pnlDepositSlip.BorderColor = System.Drawing.ColorTranslator.FromHtml("#003060")
            '    lblDepSlipActive.Visible = False
            '    btnIgnoreSlip.Text = "Mark Deposit Slip as Ignored"
            '    lblExplainIgnoreDepositSlip.Text = "*All Bags submitted with this Deposit Slip in the future will not show up on Delinquency log"
            'End If

            Dim InitSQL As String = "select DepositBagID, DepositBagNumber, case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end DepositSlipNumber, convert(varchar,DepositDate, 107) DepositDate, DepositDate as DDSort, " & _
            "sum(isnull(Cash, 0)) + SUM(isnull(ManualChecks,0)) as BagTotal, SubmissionFullName, case when dl.Active = 1  then 1 else 0 end as Ignore " & _
            ",dl.Comment, sum(case when  pps.EODCollectionDate = '" & Replace(TranDate, "'", "''") & "' " & _
            "and pps.OutletTA = '" & Replace(MerchDesc, "'", "''") & "' then isnull(Cash, 0) + isnull(ManualChecks,0) else 0 end) as RelevantTotal " & _
            "from DWH.WF.PPS_Submissions pps " & _
            "left join DWH.WF.DelinquencyLogger dl on dl.SourceName = 'WF PPS - Bag' " & _
            "and dl.SourceID = convert(varchar, pps.DepositBagID) and dl.Active = 1 " & _
            "where pps.Active = 1 and Exists (select * from DWH.WF.PPS_Submissions pp2 where pp2.DepositBagID = pps.DepositBagID and  " & _
            "pp2.EODCollectionDate = '" & Replace(TranDate, "'", "''") & "' " & _
            "and pp2.OutletTA = '" & Replace(MerchDesc, "'", "''") & "') " & _
            "group by DepositBagID, DepositDate, dl.Comment, DepositBagNumber, case when charindex('0', pps.DepositSlipNumber) > 0 and PATINDEX('%[1-9]%', pps.DepositSlipNumber) > 0 " & _
            "then SUBSTRING(pps.DepositSlipNumber, PATINDEX('%[1-9]%', pps.DepositSlipNumber), LEN(pps.DepositSlipNumber) - PATINDEX('%[1-9]%', pps.DepositSlipNumber) + 1) " & _
            "when charindex('0', pps.DepositSlipNumber) > 0 then '0' " & _
            "else pps.DepositSlipNumber end , convert(varchar,DepositDate, 107), SubmissionFullName, case when dl.Active = 1  then 1 else 0 end "

            gvPPS_Submissions.Columns(4).Visible = True
            PPSView = GetData(InitSQL).DefaultView
            gvPPS_Submissions.DataSource = PPSView

            gvPPS_Submissions.DataBind()

            If PPSView.Count = 0 Then
                lblWFPPSNoRecords.Visible = True
            Else
                lblWFPPSNoRecords.Visible = False
            End If


            Dim InstamedSQL As String = "select convert(varchar,TransDate, 107) as TransDate, TransDate as TDSort, MerchantDescription, " & _
                "PatientAccountNumber, case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end as Amount, TransactionID, CheckAccountType, case when dl.Active = 1  then 1 else 0 end as Ignored, dl.Comment as Comment, RN " & _
            " from " & _
            "( select  " & _
            "cast(DATEADD(hour, 4, cast(case " & _
            "when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then '' " & _
            "else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) as TransDate " & _
            ",  mt.MerchantDescription, im.* " & _
            ",ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
            "from DWH.WF.InstaMed im " & _
            "left join DWH.WF.Merchant_ID_LU mt on  mt.InactivatedDate is null and GETDATE() between isnull(mt.EffectiveFrom, '1/1/1800') and isnull(mt.EffectiveTo, '12/31/9999') " & _
            "   and im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID " & _
            "   and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
            "where PaymentTransactionType in ('Cash') " & _
            "and mt.MerchantDescription = '" & Replace(MerchDesc, "'", "''") & "' " & _
            ") x " & _
            "left join DWH.WF.DelinquencyLogger dl on convert(varchar, x.RN) = right(SourceName, len(SourceName) - 9) and dl.SourceID = x.TransactionID " & _
            "where TransDate = '" & Replace(TranDate, "'", "''") & "' " & _
            "order by TransDate, PatientAccountNumber, TransactionID, Amount, RN "

            '9/26/2019
            'Dim InstamedSQL As String = "select convert(varchar,TransDate, 107) as TransDate, TransDate as TDSort, MerchantDescription, " & _
            '    "PatientAccountNumber, case when PaymentTransactionAction = 'AuthCapt' then Amount else -Amount end as Amount, TransactionID, CheckAccountType, case when dl.Active = 1  then 1 else 0 end as Ignored, dl.Comment as Comment, RN " & _
            '" from " & _
            '"( select  " & _
            '"cast(DATEADD(hour, 4, cast(case " & _
            '"when right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) = '// ::'  then '' " & _
            '"else right(LEFT(TransactionDate, 6), 2) + '/' + RIGHT(Left(TransactionDate, 8), 2) + '/' + LEFT(TransactionDate, 4) + ' ' + RIGHT(Left(TransactionDate, 10), 2) + ':' + LEFT(RIGHT(TransactionDate, 4), 2) + ':' + RIGHT(TransactionDate, 2) end as DATETIME)) as date) as TransDate " & _
            '",  mt.MerchantDescription, im.* " & _
            '",ROW_NUMBER() over (partition by TransactionID order by PatientAccountNumber, Amount) RN " & _
            '"from DWH.WF.InstaMed im " & _
            '"left join DWH.WF.Merchant_LU mt on im.TakenAtMerchantID = mt.MerchantID and im.TakenAtStoreID = mt.StoreID " & _
            '"and im.TakenAtTerminalID = mt.TerminalID and mt.MerchantID not like '%E%' " & _
            '"where PaymentTransactionType in ('Cash') " & _
            '"and mt.MerchantDescription = '" & Replace(MerchDesc, "'", "''") & "' " & _
            '") x " & _
            '"left join DWH.WF.DelinquencyLogger dl on convert(varchar, x.RN) = right(SourceName, len(SourceName) - 9) and dl.SourceID = x.TransactionID " & _
            '"where TransDate = '" & Replace(TranDate, "'", "''") & "' " & _
            '"order by TransDate, PatientAccountNumber, TransactionID, Amount, RN "

            InstamedView = GetData(InstamedSQL).DefaultView
            gvInstamed.DataSource = InstamedView
            gvInstamed.DataBind()

            If InstamedView.Count = 0 Then
                lblInstamedNoRecords.Visible = True
            Else
                lblInstamedNoRecords.Visible = False
            End If

            Dim RevDate As Date
            RevDate = Replace(TranDate, "'", "''")


            For Each rowboat As GridViewRow In gvPPS_Submissions.Rows
                Dim gvSubmissionRow As GridView = TryCast(rowboat.FindControl("gvSubmissionRow"), GridView)
                For Each canoe As GridViewRow In gvSubmissionRow.Rows
                    If canoe.Cells(2).Text = MonthName(Month(RevDate), True) & " " & RevDate.ToString("dd, yyyy") And canoe.Cells(3).Text.ToUpper = Replace(MerchDesc, "'", "''").ToUpper Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                    End If
                Next
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
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

    Private Sub gvPPS_Submissions_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPPS_Submissions.PageIndexChanging
        Try

            gvPPS_Submissions.PageIndex = e.NewPageIndex
            gvPPS_Submissions.DataSource = PPSView
            gvPPS_Submissions.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvPPS_Submissions_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPPS_Submissions.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim DepositBagID As String = gvPPS_Submissions.DataKeys(e.Row.RowIndex).Value.ToString()

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
    Private Function IgnorePPS(DelinquentID As Integer, updatesql As String) As String

        Dim chk As CheckBox
        Dim chk2 As CheckBox
        Dim gvRowSubmission As GridView
        Dim s As String = ""

        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
        UserName = tmblbl.Text

        For i As Integer = 0 To gvPPS_Submissions.Rows.Count - 1
            chk = CType(gvPPS_Submissions.Rows(i).FindControl("chkBag"), CheckBox)
            If chk.Checked = True Then
                updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 1, DelinquentID = " & DelinquentID & ", " & _
                    "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                    Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                    "SourceName = 'WF PPS - Bag' and Active <> 1 and SourceID = '" & Replace(gvPPS_Submissions.DataKeys(i).Value.ToString(), "'", "''") & "'; " & _
                    "Insert into DWH.WF.DelinquencyLogger select " & DelinquentID & ", '" & Replace(gvPPS_Submissions.DataKeys(i).Value.ToString(), "'", "''") & _
                    "', 'WF PPS - Bag', '" & Replace(UserName, "'", "''") & "', getdate(), '" & Replace(txtComment.Text, "'", "''") & "',  1 " & _
                    "where not exists (select * from DWH.WF.DelinquencyLogger where SourceName = 'WF PPS - Bag' and SourceID = '" & _
                    Replace(gvPPS_Submissions.DataKeys(i).Value.ToString(), "'", "''") & "'); "
            Else
                updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 0, DelinquentID = " & DelinquentID & ", " & _
                    "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                    Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                    "SourceName = 'WF PPS - Bag' and Active <> 0 and SourceID = '" & Replace(gvPPS_Submissions.DataKeys(i).Value.ToString(), "'", "''") & "'; "
            End If
            gvRowSubmission = CType(gvPPS_Submissions.Rows(i).FindControl("gvSubmissionRow"), GridView)
            For j As Integer = 0 To gvRowSubmission.Rows.Count - 1
                s = s + gvRowSubmission.DataKeys(j).Value.ToString() & ","
                chk2 = CType(gvRowSubmission.Rows(j).FindControl("chkRow"), CheckBox)
                If chk2.Checked = True Then
                    updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 1, DelinquentID = " & DelinquentID & ", " & _
                        "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                        Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                        "SourceName = 'WF PPS - Row' and Active <> 1 and SourceID = '" & Replace(gvRowSubmission.DataKeys(j).Value.ToString(), "'", "''") & "'; " & _
                        "Insert into DWH.WF.DelinquencyLogger select " & DelinquentID & ", '" & Replace(gvRowSubmission.DataKeys(j).Value.ToString(), "'", "''") & _
                        "', 'WF PPS - Row', '" & Replace(UserName, "'", "''") & "', getdate(), '" & Replace(txtComment.Text, "'", "''") & "',  1 " & _
                        "where not exists (select * from DWH.WF.DelinquencyLogger where SourceName = 'WF PPS - Row' and SourceID = '" & _
                        Replace(gvRowSubmission.DataKeys(j).Value.ToString(), "'", "''") & "'); "
                Else
                    updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 0, DelinquentID = " & DelinquentID & ", " & _
                        "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                        Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                        "SourceName = 'WF PPS - Row' and Active <> 0 and SourceID = '" & Replace(gvRowSubmission.DataKeys(j).Value.ToString(), "'", "''") & "'; "
                End If
            Next

        Next
        Return updatesql
    End Function
    Private Sub btnIgnoreRows_Click(sender As Object, e As EventArgs) Handles btnIgnoreRows.Click

        Try


            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = tmblbl.Text

            If Len(txtComment.Text.Trim) = 0 Then

                mpe1ExplanationLabel.Text = "Please submit comment explaining these rows should or should not be ignored."
                mpe1.Show()
                Exit Sub

            End If


            Dim chk3 As CheckBox
            Dim counter As Integer = 0
            Dim updatesql As String = ""
            Dim ByteString As String

            Dim DelinquentSQL As String = "select isnull(max(DelinquentID),0) + 1 from DWH.WF.DelinquencyLogger"

            Dim DelinquentID As Integer = Replace(GetScalar(DelinquentSQL), "'", "''")

            updatesql = IgnorePPS(DelinquentID, updatesql)

            For j As Integer = 0 To gvWFAllActivity.Rows.Count - 1
                chk3 = CType(gvWFAllActivity.Rows(j).FindControl("chkWF"), CheckBox)
                If TypeOf gvWFAllActivity.DataKeys(j).Value Is Byte Then
                    ByteString = Bytes_To_String2(gvWFAllActivity.DataKeys(j).Value)
                ElseIf gvWFAllActivity.DataKeys(j).Value.GetType.ToString = "System.Byte[]" Then
                    ByteString = Bytes_To_String2(gvWFAllActivity.DataKeys(j).Value)
                Else
                    ByteString = gvWFAllActivity.DataKeys(j).Value.GetType.ToString
                    ByteString = CStr(gvWFAllActivity.DataKeys(j).Value)
                End If

                If chk3.Checked = True Then
                    counter = counter + 1
                    updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 1, DelinquentID = " & DelinquentID & ", " & _
                        "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                        Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                        "SourceName = 'WFAllActivity - UAID' and Active <> 1 and SourceID = '" & Replace(ByteString, "'", "''") & "'; " & _
                        "Insert into DWH.WF.DelinquencyLogger select " & DelinquentID & ", '" & Replace(ByteString, "'", "''") & _
                        "', 'WFAllActivity - UAID', '" & Replace(UserName, "'", "''") & "', getdate(), '" & Replace(txtComment.Text, "'", "''") & "',  1 " & _
                        "where not exists (select * from DWH.WF.DelinquencyLogger where SourceName = 'WF PPS - Row' and SourceID = '" & _
                        Replace(ByteString, "'", "''") & "'); "
                Else
                    updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 0, DelinquentID = " & DelinquentID & ", " & _
                        "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                        Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                        "SourceName = 'WFAllActivity - UAID' and Active <> 0 and SourceID = '" & Replace(ByteString, "'", "''") & "'; "
                End If
            Next


            ExecuteSql(updatesql)

            RefreshAAPage()
            mpe1ExplanationLabel.Text = "Delinquency Logged"
            mpe1.Show()

        Catch ex As Exception
            RefreshAAPage()
            mpe1ExplanationLabel.Text = "Error Logging Delinquency -- Please report to Admin"
            mpe1.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Function Bytes_To_String2(ByVal bytes_Input As Byte()) As String
        Dim strTemp As String = "0x"
        For Each b As Byte In bytes_Input
            If Len(Conversion.Hex(b)) = 1 Then
                strTemp += "0" & Conversion.Hex(b)
            Else
                strTemp += Conversion.Hex(b)
            End If
        Next
        Return strTemp.ToString()
    End Function
    Private Sub mpe1btnCancel_Click(sender As Object, e As EventArgs) Handles mpe1btnCancel.Click
        mpe1.Hide()
    End Sub

    Private Sub mpe1btnOK_Click(sender As Object, e As EventArgs) Handles mpe1btnOK.Click
        mpe1.Hide()
    End Sub

    Private Sub btnIgnoreSlip_Click(sender As Object, e As EventArgs) Handles btnIgnoreSlip.Click
        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = tmblbl.Text

            If Len(txtComment.Text.Trim) = 0 Then

                mpe1ExplanationLabel.Text = "Please submit comment explaining why this deposit slip number should be ignored."
                mpe1.Show()
                Exit Sub

            End If

            Dim counter As Integer = 0
            Dim updatesql As String = ""
            Dim s As String = ""

            Dim DelinquentSQL As String = "select isnull(max(DelinquentID),0) + 1 from DWH.WF.DelinquencyLogger"

            Dim DelinquentID As Integer = Replace(GetScalar(DelinquentSQL), "'", "''")

            If lblDepSlipActive.Visible = True Then
                updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 0, DelinquentID = " & DelinquentID & ", " & _
                    "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                    Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                    "SourceName = 'Deposit Slip Number' and Active <> 0 and SourceID = '" & Replace(lblDepositSlip.Text, "'", "''") & "'; "

            Else
                updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 1, DelinquentID = " & DelinquentID & ", " & _
                    "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                    Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                    "SourceName = 'Deposit Slip Number' and Active <> 1 and SourceID = '" & Replace(lblDepositSlip.Text, "'", "''") & "'; " & _
                    "Insert into DWH.WF.DelinquencyLogger select " & DelinquentID & ", '" & Replace(lblDepositSlip.Text, "'", "''") & _
                    "', 'Deposit Slip Number', '" & Replace(UserName, "'", "''") & "', getdate(), '" & Replace(txtComment.Text, "'", "''") & "',  1 " & _
                    "where not exists (select * from DWH.WF.DelinquencyLogger where SourceName = 'Deposit Slip Number' and SourceID = '" & _
                    Replace(lblDepositSlip.Text, "'", "''") & "'); "
            End If



            ExecuteSql(updatesql)

            RefreshAAPage()
            txtComment.Text = ""
            mpe1ExplanationLabel.Text = "Delinquency Logged"
            mpe1.Show()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvPPS_Submissions_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvPPS_Submissions.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = PPSView

            sorts = e.SortExpression

            If e.SortExpression = PPSmap Then

                If PPSdir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    PPSdir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    PPSdir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                PPSdir = 1
                PPSmap = e.SortExpression
            End If

            gvPPS_Submissions.DataSource = dv
            gvPPS_Submissions.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvWFAllActivity_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvWFAllActivity.PageIndexChanging
        Try

            gvWFAllActivity.PageIndex = e.NewPageIndex
            gvWFAllActivity.DataSource = AllActivityView
            gvWFAllActivity.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvWFAllActivity_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvWFAllActivity.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = AllActivityView

            sorts = e.SortExpression

            If e.SortExpression = AllActivitymap Then

                If AllActivitydir = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    AllActivitydir = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    AllActivitydir = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                AllActivitydir = 1
                AllActivitymap = e.SortExpression
            End If

            gvWFAllActivity.DataSource = dv
            gvWFAllActivity.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
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

    Private Sub btnIgnoreInstamed_Click(sender As Object, e As EventArgs) Handles btnIgnoreInstamed.Click
        Try

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = tmblbl.Text

            If Len(txtComment.Text.Trim) = 0 Then

                mpe1ExplanationLabel.Text = "Please submit comment explaining these rows should or should not be ignored."
                mpe1.Show()
                Exit Sub

            End If


            Dim chk3 As CheckBox
            Dim counter As Integer = 0
            Dim updatesql As String = ""

            Dim DelinquentSQL As String = "select isnull(max(DelinquentID),0) + 1 from DWH.WF.DelinquencyLogger"

            Dim DelinquentID As Integer = Replace(GetScalar(DelinquentSQL), "'", "''")

            updatesql = IgnorePPS(DelinquentID, updatesql)

            For j As Integer = 0 To gvInstamed.Rows.Count - 1
                chk3 = CType(gvInstamed.Rows(j).FindControl("chkIM"), CheckBox)
                If chk3.Checked = True Then
                    counter = counter + 1

                    updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 1, DelinquentID = " & DelinquentID & ", " & _
                        "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                        Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                        "SourceName = 'Instamed:" & Replace(gvInstamed.DataKeys(j).Values(1).ToString(), "'", "''") & _
                        "' and Active <> 1 and SourceID = '" & Replace(gvInstamed.DataKeys(j).Values(0).ToString, "'", "''") & "'; " & _
                        "Insert into DWH.WF.DelinquencyLogger select " & DelinquentID & ", '" & Replace(gvInstamed.DataKeys(j).Values(0).ToString, "'", "''") & _
                        "', 'Instamed:" & Replace(gvInstamed.DataKeys(j).Values(1).ToString, "'", "''") & "', '" & Replace(UserName, "'", "''") & _
                        "', getdate(), '" & Replace(txtComment.Text, "'", "''") & "',  1 " & _
                        "where not exists (select * from DWH.WF.DelinquencyLogger where SourceName = 'Instamed:" & _
                        Replace(gvInstamed.DataKeys(j).Values(1).ToString, "'", "''") & "' and SourceID = '" & _
                        Replace(gvInstamed.DataKeys(j).Values(0).ToString, "'", "''") & "'); "
                Else
                    updatesql = updatesql & "Update DWH.WF.DelinquencyLogger set Active = 0, DelinquentID = " & DelinquentID & ", " & _
                        "Comment = Comment + '(' + convert(varchar, DelinquentID) + ' -- ' + isnull(convert(varchar,SubmissionDate, 107), '?') + ', ' + isnull(SubmittedBy, 'Unknown') + '); " & _
                        Replace(txtComment.Text, "'", "''") & "',  SubmissionDate = getdate(), SubmittedBy = '" & Replace(UserName, "'", "''") & "' where " & _
                        "SourceName = 'Instamed:" & Replace(gvInstamed.DataKeys(j).Values(1).ToString, "'", "''") & _
                        "' and Active <> 0 and SourceID = '" & Replace(gvInstamed.DataKeys(j).Values(0).ToString, "'", "''") & "'; "
                End If
            Next


            ExecuteSql(updatesql)

            RefreshIMPage(lblMerchDesc.Text, lblTranDate.Text)
            txtComment.Text = ""
            mpe1ExplanationLabel.Text = "Delinquency Logged"
            mpe1.Show()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlFacility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFacility.SelectedIndexChanged
        RefreshAAPage()
    End Sub

    Private Sub txtDepositSlip_TextChanged(sender As Object, e As EventArgs) Handles txtDepositSlip.TextChanged
        RefreshAAPage()
    End Sub
End Class