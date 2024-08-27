Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class AR_GECC_History
    Inherits System.Web.UI.Page
    Private Shared superadmin As Integer = 0
    Private Shared admin As Integer = 0
    Public Shared ARDetailView As New DataView
    Public Shared ARDetailmap As String
    Public Shared ARDetaildir As Integer

    Private Shared ActivityReportHistorySelectedColor As String = "#bbffbb"
    Private Shared ActivityReportHistoryAlternateColor As String = "#cfefff"
    Private Shared ActivityReportHistoryMainColor As String = "#ffffff"

    Private Shared DifferentColor As String = "#000000"
    Private Shared SameColor As String = "#ffffff"



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "cw996788" Then
                superadmin = 0
                admin = 1
            ElseIf Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "mf995052" Then
                superadmin = 0
                admin = 1

            Else


                Dim adminsql As String = "SELECT count(*) FROM DWH.AR_GECC.ActivityReport_Users where UserLogin = '" & _
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'"
                admin = GetScalar(adminsql)

            End If

            If Request.QueryString("Detail_ID") IsNot Nothing Then
                SelectedDetailID.Text = Request.QueryString("Detail_ID")
                BindHeaderGrid(SelectedDetailID.Text)
                BindFirstGrid(SelectedDetailID.Text)

                'Dim sql As String = "select top 1 ad.Facility, ad.CashCategory, ad.DetailStatus, ar.DepositDate, ad.Cash_Received " & _
                '    "from DWH.AR_GECC.ActivityReport ar join DWH.AR_GECC.ActivityReport_Detail ad on ar.ActivityID = ad.ActivityID " & _
                '    "where ar.ActivityID = '" & Replace(Request.QueryString("Detail_ID"), "'", "''") & "'  order by ad.ActivityDate desc, ad.ModifiedDate desc"
                'Dim cmd As SqlCommand
                'Dim da As SqlDataAdapter
                'Dim ds As DataSet

                'Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                '    cmd = New SqlCommand(sql, con)
                '    da = New SqlDataAdapter(cmd)
                '    con.Open()

                '    ds = New DataSet()
                '    da.Fill(ds)

                '    If ds.Tables(0).Rows.Count > 0 Then
                '        CaseTitle.Text = ds.Tables(0).Rows(0).Item(1).ToString
                '        CaseDesc.Text = ds.Tables(0).Rows(0).Item(2).ToString
                '        CaseUser.Text = ds.Tables(0).Rows(0).Item(3).ToString
                '        CaseNumber.Text = ds.Tables(0).Rows(0).Item(0).ToString
                '        CaseCash.Text = ds.Tables(0).Rows(0).Item(4).ToString
                '        FullPanel.Visible = True
                '        EmptyPanel.Visible = False

                '    End If
                'End Using

                'BindGrid()
            Else
                FullPanel.Visible = False
                EmptyPanel.Visible = True
                'SelectedDetailID.Text = "7725"
                'BindHeaderGrid(SelectedDetailID.Text)
                'BindFirstGrid(SelectedDetailID.Text)
                'BindGrid()
            End If

        End If

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

    Private Sub BindHeaderGrid(detailID As Integer)

        Dim s As String = "declare @SearchID int =  " & detailID & _
"	select * " & _
" , case when FinalActivityDate is not null then 'Closed' else (select top 1 ar.DetailStatus from DWH.AR_GECC.ActivityReport_Detail ar " & _
"           where ar.Active = 1 and ar.ActivityID = N.ActivityID order by ar.ActivityDate desc, ar.ModifiedDate desc) end as Status " & _
",  LetterCodes = " & _
"      Coalesce((SELECT Char(65 + (N.Num - 475255) / 456976 % 26) WHERE N.Num >= 475255), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 18279) / 17576 % 26) WHERE N.Num >= 18279), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 703) / 676 % 26) WHERE N.Num >= 703), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 27) / 26 % 26) WHERE N.Num >= 27), '') " & _
"      + (SELECT Char(65 + (N.Num - 1) % 26)) 	   " & _
"	  from ( " & _
"select ar.ActivityID, FirstActivityDate, FinalActivityDate, ar.DepositDate " & _
" , isnull(s.Source, isnull(UserFullName, ar.CreatedBy)) as RowSource, s.NetAmount, s.ItemDescription, s.UniqueID " & _
", DENSE_RANK() over (order by ar.ActivityID) as num " & _
"from DWH.AR_GECC.ActivityReport ar " & _
"	join (select NetID as ActivityID from DWH.AR_GECC.ActivityReport_Netting  " & _
"	where InitialID = @SearchID and Active = 1  " & _
"	union   " & _
"	select InitialID from DWH.AR_GECC.ActivityReport_Netting  " & _
"	where NetID = @SearchID  and Active = 1  " & _
"	union  " & _
"	select TransferFrom from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) x  " & _
"	where isnull(IntermediateRow, 0) = @SearchID or TransferTo = @SearchID and Active = 1  " & _
"	union   " & _
"	select TransferTo from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) x  " & _
"	where isnull(IntermediateRow, 0) = @SearchID or TransferFrom = @SearchID and Active = 1  " & _
"	union select @SearchID) find on ar.ActivityID = find.ActivityID  " & _
"left join DWH.AR_GECC.ActivityReport_2_Source a2s on ar.ActivityID = a2s.ActivityID and a2s.Active = 1 " & _
"left join DWh.AR_GECC.ActivityReport_Source s on a2s.Base_ID = s.Base_ID and s.Active = 1 " & _
"left join DWH.AR_GECC.ActivityReport_Users u on ar.CreatedBy = u.UserLogin and u.Active = 1 " & _
"where ar.Active = 1  ) N "

        gvHeaderDisplay.DataSource = GetData(s)
        gvHeaderDisplay.DataBind()

        If gvHeaderDisplay.Rows.Count > 0 Then
            FullPanel.Visible = True
            EmptyPanel.Visible = False

        End If

    End Sub


    Private Sub BindFirstGrid(detailID As Integer)

        Dim s As String = "declare @SearchID int =  " & detailID & _
" " & _
"	select isnull(Base_ID, -ActivityID) as Base_ID,  LetterCode = " & _
"      Coalesce((SELECT Char(65 + (N.Num - 475255) / 456976 % 26) WHERE N.Num >= 475255), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 18279) / 17576 % 26) WHERE N.Num >= 18279), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 703) / 676 % 26) WHERE N.Num >= 703), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 27) / 26 % 26) WHERE N.Num >= 27), '') " & _
"      + (SELECT Char(65 + (N.Num - 1) % 26)) 	   " & _
"	  into #TmpBases " & _
"	  from ( " & _
"	select s.*, ar.ActivityID, DENSE_RANK() over (order by ar.ActivityID) as Num   " & _
"	from DWH.AR_GECC.ActivityReport ar " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source two on ar.ActivityID = two.ActivityID and two.Active = 1 " & _
"	join (select NetID as ActivityID from DWH.AR_GECC.ActivityReport_Netting " & _
"	where InitialID = @SearchID and Active = 1 " & _
"	union  " & _
"	select InitialID from DWH.AR_GECC.ActivityReport_Netting " & _
"	where NetID = @SearchID  and Active = 1 " & _
"	union " & _
"	select TransferFrom from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) x " & _
"	where isnull(IntermediateRow, 0) = @SearchID or TransferTo = @SearchID and Active = 1 " & _
"	union  " & _
"	select TransferTo from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) x " & _
"	where isnull(IntermediateRow, 0) = @SearchID or TransferFrom = @SearchID and Active = 1 " & _
"	union select @SearchID) find on ar.ActivityID = find.ActivityID " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on two.Base_ID = s.Base_ID and s.Active = 1 " & _
"	) N " & _
"	 " & _
"	Select distinct ST2.TFID, 'Transfer' as ChangeType, " & _
"	 ST2.TransferDate as ActivityDate, ST2.Amount, ST2.ModifyDate " & _
"	, isnull(u.UserDropDownListName, left(UserFullName, 10))  as UserDisplay, " & _
"   substring( " & _
"        ( " & _
"            Select ','+ST1.LetterCode  AS [text()] " & _
"            From (select art.*, t.*  " & _
"	from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) art " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.IntermediateRow, art.TransferFrom, art.TransferTo) and ar.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 " & _
"	join #TmpBases t on s.Base_ID = t.Base_ID or t.Base_ID = -ar.ActivityID " & _
"	where art.Active = 1) ST1 " & _
"           Where ST1.TFID = ST2.TFID " & _
"            ORDER BY ST1.LetterCode " & _
"            For XML PATH ('') " & _
"        ), 2, 1000) LetterCodes " & _
"From (select art.*, t.*  " & _
"	from (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) art " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.IntermediateRow, art.TransferFrom, art.TransferTo) and ar.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 " & _
"	join #TmpBases t on s.Base_ID = t.Base_ID or t.Base_ID = -ar.ActivityID " & _
"	where art.Active = 1) ST2 " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and ST2.ModifyUser = u.UserLogin " & _
"	 " & _
"	union all " & _
"" & _
"		Select distinct ST2.NetID, 'Net Rows', " & _
"	 ST2.ActivityDate as ActivityDate, ST2.Amount, ST2.ModifyDate " & _
"	, isnull(u.UserDropDownListName, left(UserFullName, 10)), " & _
"   substring( " & _
"        ( " & _
"            Select ','+ST1.LetterCode  AS [text()] " & _
"            From (select art.*, t.*  " & _
"			from DWH.AR_GECC.ActivityReport_Netting art " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.InitialID, art.NetID) and ar.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 " & _
"	join #TmpBases t on s.Base_ID = t.Base_ID or t.Base_ID = -ar.ActivityID " & _
"	where art.Active = 1) ST1 " & _
"           Where ST1.InitialID = ST2.InitialID and ST1.NetID = ST2.NETID " & _
"            ORDER BY ST1.LetterCode " & _
"            For XML PATH ('') " & _
"        ), 2, 1000) LetterCodes " & _
"From (select art.*, t.*  " & _
"	from DWH.AR_GECC.ActivityReport_Netting art " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.InitialID, art.NetID) and ar.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 " & _
"	join #TmpBases t on s.Base_ID = t.Base_ID or t.Base_ID = -ar.ActivityID " & _
"	where art.Active = 1) ST2 " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and ST2.ModifyUser = u.UserLogin " & _
"	 " & _
"	union all " & _
"	 " & _
"			Select distinct ST2.InitialID, 'Split Rows', " & _
"	 ST2.ActivityDate as ActivityDate, ad.Cash_Received, ST2.ModifyDate " & _
"	, isnull(u.UserDropDownListName, left(UserFullName, 10)), " & _
"   ST2.LetterCode as LetterCodes " & _
"From (select art.*, t.*  " & _
"	from DWH.AR_GECC.ActivityReport_Splitting art " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.InitialID) and ar.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 " & _
"	join #TmpBases t on s.Base_ID = t.Base_ID or t.Base_ID = -ar.ActivityID " & _
"	where art.Active = 1) ST2 " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and ST2.ModifyUser = u.UserLogin " & _
"	left join DWH.AR_GECC.ActivityReport_Detail ad on ST2.InitialID = ad.ActivityID and convert(date, ad.ActivityDate) <= ST2.ActivityDate " & _
"		and not exists (select * from DWH.AR_GECC.ActivityReport_Detail adfake where adfake.ActivityID = ad.ActivityID and adfake.ActivityDate > ad.ActivityDate " & _
"							and adfake.ActivityDate <= ST2.ActivityDate) " & _
"" & _
"union all " & _
" " & _
"	select ar.ActivityID, 'Update', ard.ActivityDate, ard.Cash_Received, ard.ModifiedDate " & _
"	, isnull(isnull(u.UserDropDownListName, left(UserFullName, 10)), ard.ModifiedBy) " & _
"	, t.LetterCode " & _
"	from DWH.AR_GECC.ActivityReport ar " & _
"	join DWH.AR_GECC.ActivityReport_Detail ard on ar.ActivityID = ard.ActivityID " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1 " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 " & _
"	join #TmpBases t on s.Base_ID = t.Base_ID or t.Base_ID = -ar.ActivityID " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and ard.ModifiedBy = u.UserLogin " & _
"" & _
"	order by 3, 5 "

        gvInitialDisplay.DataSource = GetData(s).DefaultView
        gvInitialDisplay.DataBind()
        rowingInitialDisplay()

    End Sub

    Private Function BindGrid(x As Integer)

        Dim s As String = "declare @SearchID int =  " & x & _
" " & _
  "select *, ROW_NUMBER() over (order by ModifyDate) as RN " & _
" into #TmpHistory  " & _
" from ( " & _
"	Select TransferDate as ActivityDate " & _
"	, 'Transfer from ' +  tffac.FacDisplay + ' to ' + ttfac.FacDisplay as ChangeType 	 " & _
"	, Amount " & _
"	, ard.Facility " & _
"	, ard.CashCategory " & _
"	, ard.DetailStatus " & _
"	, ard.BankBatchNumber " & _
"	, ard.STARBatchNumber " & _
"	, ard.NoPatients " & _
"	, ard.Type " & _
"	, ard.Cash_Received " & _
"	, ard.AR_Posted " & _
"	, ard.Misc_Posted " & _
"	, ard.Interest " & _
"	, ard.Unresolved " & _
"	, ard.Carry_Forward " & _
"   , case when TransferredIn is null and TransferredOut is null then null else  isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) end as Transfers " & _
"	            , case when ard.DetailStatus = 'Current' then   " & _
"           	isnull(ard.Cash_Received, 0.00) - isnull(ard.AR_Posted, 0.00) - isnull(ard.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ard.Interest, 0.00)  " & _
"            		- isnull(ard.Carry_Forward, 0.00) - isnull(ard.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)  " & _
"            	else isnull(ard.AR_Posted, 0.00) + isnull(ard.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(ard.Interest, 0.00)  " & _
"            		+ isnull(ard.Carry_Forward, 0.00) /*+ isnull(ad.Unresolved, 0.00) rmv CRW*/ - isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00)  " & _
"            	end as STARVariance  " & _
"            , case when ard.DetailStatus = 'Current' then   " & _
"            	null  " & _
"            	else isnull(ard.Cash_Received, 0.00) - isnull(ard.AR_Posted, 0.00) - isnull(ard.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(ard.Interest, 0.00)  " & _
"            		+ isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) - isnull(ard.Unresolved, 0.00) " & _
"            	end as BankVariance  " & _
" " & _
"	,ModifyDate  " & _
"	, isnull(u.UserDropDownListName, left(UserFullName, 10))  as UserDisplay  " & _
"	 " & _
"	 " & _
"	 " & _
"	from (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) art  " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.IntermediateRow, art.TransferFrom, art.TransferTo) and ar.Active = 1  " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and art.ModifyUser = u.UserLogin " & _
"	left join DWH.AR_GECC.ActivityReport_Detail ard on ard.Active = 1 and ar.ActivityID = ard.ActivityID and ard.ActivityDate <= art.TransferDate and ard.ModifiedDate <= art.ModifyDate " & _
"		and not exists (select * from DWH.AR_GECC.ActivityReport_Detail badard where badard.activityID = ar.ActivityID and badard.Active = 1  " & _
"			and badard.ActivityDate <= art.TransferDate and badard.ModifiedDate <= art.ModifyDate  " & _
"			and (badard.ActivityDate > ard.ActivityDate or (badard.ActivityDate = ard.ActivityDate and badard.ModifiedDate > ard.ModifiedDate))) " & _
"	left join DWH.AR_GECC.ActivityReport_Detail ttard on ttard.Active = 1 and art.TransferTo = ttard.ActivityID and ttard.ActivityDate <= art.TransferDate  " & _
"		and not exists (select * from DWH.AR_GECC.ActivityReport_Detail badard where badard.activityID = ttard.ActivityID and badard.Active = 1  " & _
"			and badard.ActivityDate <= art.TransferDate   " & _
"			and (badard.ActivityDate > ttard.ActivityDate or (badard.ActivityDate = ttard.ActivityDate and badard.ModifiedDate > ttard.ModifiedDate))) " & _
"	left join DWH.AR_GECC.ActivityReport_Detail tfard on tfard.Active = 1 and art.TransferFrom = tfard.ActivityID and tfard.ActivityDate <= art.TransferDate and tfard.ModifiedDate <= art.ModifyDate " & _
"		and not exists (select * from DWH.AR_GECC.ActivityReport_Detail badard where badard.activityID = tfard.ActivityID and badard.Active = 1  " & _
"			and badard.ActivityDate <= art.TransferDate and badard.ModifiedDate <= art.ModifyDate  " & _
"			and (badard.ActivityDate > tfard.ActivityDate or (badard.ActivityDate = tfard.ActivityDate and badard.ModifiedDate > tfard.ModifiedDate)))					 " & _
"	left join (select distinct substring(DisplayDescription, 1 " & _
"        	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end) as FacDisplay, Facility  " & _
"        from DWH.AR_GECC.ActivityReport_BankAccountNumber_LU where Active = 1 " & _
"         ) ttfac on  ttfac.Facility = ttard.Facility " & _
"	left join (select distinct substring(DisplayDescription, 1 " & _
"        	, case when charindex(' ', DisplayDescription) > 0 then charindex(' ', DisplayDescription) else len(DisplayDescription) end) as FacDisplay, Facility  " & _
"        from DWH.AR_GECC.ActivityReport_BankAccountNumber_LU where Active = 1 " & _
"        ) tffac on  tffac.Facility = tfard.Facility " & _
"            left join (select ActivityID, sum(MiscAmt) as MiscAmt, convert(Date, ActivityDate) as MiscDate, TFID from    " & _
"            		DWH.AR_GECC.ActivityReport_MiscGL m " & _
"					join (select distinct TFID, TransferDate, ModifyDate from DWH.AR_GECC.ActivityReport_Transfers where Active = 1) t on convert(date, m.ActivityDate) <= t.TransferDate " & _
"						and m.ModifiedDate <= t.ModifyDate  " & _
"					where m.Active = 1   group by t.TFID, ActivityID, convert(Date, ActivityDate)) mgl on ar.ActivityID = mgl.ActivityID  and mgl.TFID = art.TFID " & _
"             and mgl.MiscDate = art.TransferDate " & _
"            left join (select t.TFID, TransferFrom, sum(Amount) TransferredOut, convert(Date, tt.TransferDate) as TFDate from DWH.AR_GECC.ActivityReport_Transfers tt " & _
"				join (select distinct TFID, TransferDate, ModifyDate from DWH.AR_GECC.ActivityReport_Transfers where Active = 1) t  " & _
"					on t.TransferDate <= tt.TransferDate and tt.ModifyDate <= t.ModifyDate " & _
"            		where Active = 1    " & _
"            		group by t.TFID, TransferFrom, convert(Date, tt.TransferDate) ) tff on tff.TFID = art.TFID and tff.TransferFrom = ar.ActivityID and tff.TFDate = art.TransferDate   " & _
"            left join (select t.TFID, TransferTo, sum(Amount) TransferredIn, convert(date, tf.TransferDate) as TFTDate from DWH.AR_GECC.ActivityReport_Transfers  tf " & _
"				join (select distinct TFID, TransferDate, ModifyDate from DWH.AR_GECC.ActivityReport_Transfers where Active = 1) t  " & _
"					on t.TransferDate <= tf.TransferDate and tf.ModifyDate <= t.ModifyDate " & _
"            		where Active = 1 " & _
"            		group by t.TFID, TransferTo, convert(date, tf.TransferDate)) tft on tft.TFID = art.TFID and tft.TransferTo = ar.ActivityID and TFTDate = art.TransferDate   " & _
"					 " & _
"	where art.Active = 1 and ar.ActivityID = @SearchID " & _
"	 " & _
"	union all  " & _
" " & _
"		Select ST2.ActivityDate, ChangeType, Amount " & _
"		, Facility, CashCategory, DetailStatus, BankBatchNumber, STARBatchNumber, NoPatients, Type, Cash_Received, AR_Posted, Misc_Posted, Interest, neward.Unresolved, Carry_Forward " & _
"   , case when TransferredIn is null and TransferredOut is null then null else  isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) end as Transfers " & _
"			            , case when neward.DetailStatus = 'Current' then   " & _
"            	isnull(neward.Cash_Received, 0.00) - isnull(neward.AR_Posted, 0.00) - isnull(neward.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(neward.Interest, 0.00)  " & _
"            		- isnull(neward.Carry_Forward, 0.00) - isnull(neward.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)  " & _
"            	else isnull(neward.AR_Posted, 0.00) + isnull(neward.Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(neward.Interest, 0.00)  " & _
"            		+ isnull(neward.Carry_Forward, 0.00) /*+ isnull(ad.Unresolved, 0.00) rmv CRW*/ - isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00)  " & _
"            	end as STARVariance  " & _
"            , case when neward.DetailStatus = 'Current' then   " & _
"            	null  " & _
"            	else isnull(neward.Cash_Received, 0.00) - isnull(neward.AR_Posted, 0.00) - isnull(neward.Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(neward.Interest, 0.00)  " & _
"            		+ isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) - isnull(neward.Unresolved, 0.00) " & _
"            	end as BankVariance  " & _
"		/*, isnull(ard.Facility, neward.Facility) as Facility " & _
"		, isnull(ard.CashCategory, neward.CashCategory) as CashCategory " & _
"		, isnull(ard.DetailStatus, neward.DetailStatus) as DetailStatus " & _
"		, isnull(ard.BankBatchNumber, neward.BankBatchNumber) as BankBatchNumber " & _
"		, isnull(ard.STARBatchNumber, neward.STARBatchNumber) as STARBatchNumber " & _
"		, isnull(ard.NoPatients, neward.NoPatients) as NoPatients " & _
"		, isnull(ard.Type, neward.Type) as Type " & _
"		, isnull(ard.Cash_Received, neward.Cash_Received) as Cash_Received */ " & _
"			, ModifyDate " & _
"		, isnull(u.UserDropDownListName, left(UserFullName, 10))  as UserDisplay  " & _
"		 " & _
"From (select art.ActivityDate, convert(date, art.ModifyDate) as DateModify, max(art.ModifyDate) as ModifyDate, art.ModifyUser, ar.ActivityID, sum(Amount) as Amount  " & _
"	, case when art.InitialID = ar.ActivityID then 'Row Netted -- to ' + convert(varchar, art.NetID)  else 'Row Created by Netting Others' end as ChangeType " & _
"	from DWH.AR_GECC.ActivityReport_Netting art  " & _
"	join DWH.AR_GECC.ActivityReport ar on ar.ActivityID  in (art.InitialID, art.NetID) and ar.Active = 1  " & _
"	left join DWH.AR_GECC.ActivityReport_2_Source ars on ars.ActivityID = ar.ActivityID and ars.Active = 1  " & _
"	left join DWH.AR_GECC.ActivityReport_Source s on ars.Base_ID = s.Base_ID and s.Active = 1 	 " & _
"	where art.Active = 1 and ar.ActivityID = @SearchID " & _
"	group by art.ActivityDate, convert(date, art.ModifyDate), art.ModifyUser, ar.ActivityID, case when art.InitialID = ar.ActivityID then 'Row Netted -- to ' + convert(varchar, art.NetID)  else 'Row Created by Netting Others' end  " & _
"	) ST2  " & _
"	/*left join DWH.AR_GECC.ActivityReport_Detail ard on ard.Active = 1 and ChangeType <> 'Row Created by Netting Others' and ST2.ActivityID = ard.ActivityID and ard.ActivityDate <= ST2.ActivityDate  " & _
"		and ard.ModifiedDate <= ST2.ModifyDate " & _
"		and not exists (select * from DWH.AR_GECC.ActivityReport_Detail badard where badard.activityID = ST2.ActivityID and badard.Active = 1  " & _
"			and badard.ActivityDate <= ST2.ActivityDate and badard.ModifiedDate <= ST2.ModifyDate  " & _
"			and (badard.ActivityDate > ard.ActivityDate or (badard.ActivityDate = ard.ActivityDate and badard.ModifiedDate > ard.ModifiedDate))) */ " & _
"	left join DWH.AR_GECC.ActivityReport_Detail neward on neward.Active = 1 and ChangeType = 'Row Created by Netting Others' and ST2.ActivityID = neward.ActivityID  " & _
"	and convert(date, neward.ActivityDate) = ST2.ActivityDate  " & _
"		and not exists (select * from DWH.AR_GECC.ActivityReport_Detail badard where badard.activityID = ST2.ActivityID and badard.Active = 1  " & _
"			and badard.ActivityDate = neward.ActivityDate and badard.ModifiedDate < neward.ModifiedDate ) " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and ST2.ModifyUser = u.UserLogin  " & _
"	            left join (select ActivityID, sum(MiscAmt) as MiscAmt, convert(Date, ActivityDate) as MiscDate, TFID from    " & _
"            		DWH.AR_GECC.ActivityReport_MiscGL m " & _
"					join (select distinct TFID, TransferDate, ModifyDate from DWH.AR_GECC.ActivityReport_Transfers where Active = 1) t on convert(date, m.ActivityDate) <= t.TransferDate " & _
"						and m.ModifiedDate <= t.ModifyDate  " & _
"					where m.Active = 1   group by t.TFID, ActivityID, convert(Date, ActivityDate)) mgl on ST2.ActivityID = mgl.ActivityID  and mgl.TFID = ST2.ActivityID " & _
"             and mgl.MiscDate = ST2.ActivityDate " & _
"            left join (select  TransferFrom, sum(Amount) TransferredOut, convert(Date, tt.TransferDate) as TFDate from DWH.AR_GECC.ActivityReport_Transfers tt " & _
"            		where Active = 1    " & _
"            		group by  TransferFrom, convert(Date, tt.TransferDate) ) tff on  tff.TransferFrom = ST2.ActivityID and tff.TFDate = ST2.ActivityDate   " & _
"            left join (select  TransferTo, sum(Amount) TransferredIn, convert(date, tf.TransferDate) as TFTDate from DWH.AR_GECC.ActivityReport_Transfers  tf " & _
"            		where Active = 1 " & _
"            		group by TransferTo, convert(date, tf.TransferDate)) tft on tft.TransferTo = ST2.ActivityID and TFTDate = ST2.ActivityDate   " & _
" 	 " & _
"union all  " & _
"  " & _
"	select  " & _
"	ActivityDate, case when ard.ModifiedBy in ('Flagged Unresolved', 'Flagged CarryOver', 'Automatic CarryOver') then 'Day Change' else 'Update' end as ChangeType, null as Amount " & _
"		, Facility, CashCategory, DetailStatus, BankBatchNumber, STARBatchNumber, NoPatients, Type, Cash_Received, AR_Posted, Misc_Posted, Interest, ard.Unresolved, Carry_Forward " & _
"   , case when TransferredIn is null and TransferredOut is null then null else  isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) end as Transfers " & _
"			            , case when DetailStatus = 'Current' then   " & _
"            	isnull(Cash_Received, 0.00) - isnull(AR_Posted, 0.00) - isnull(Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(Interest, 0.00)  " & _
"            		- isnull(Carry_Forward, 0.00) - isnull(ard.Unresolved, 0.00) + isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00)  " & _
"            	else isnull(AR_Posted, 0.00) + isnull(Resolve, 0.00) + isnull(MiscAmt, 0.00) + isnull(Interest, 0.00)  " & _
"            		+ isnull(Carry_Forward, 0.00) /*+ isnull(ad.Unresolved, 0.00) rmv CRW*/ - isnull(TransferredIn, 0.00) + isnull(TransferredOut, 0.00)  " & _
"            	end as STARVariance  " & _
"            , case when DetailStatus = 'Current' then   " & _
"            	null  " & _
"            	else isnull(Cash_Received, 0.00) - isnull(AR_Posted, 0.00) - isnull(Resolve, 0.00) - isnull(MiscAmt, 0.00) - isnull(Interest, 0.00)  " & _
"            		+ isnull(TransferredIn, 0.00) - isnull(TransferredOut, 0.00) - isnull(ard.Unresolved, 0.00) " & _
"            	end as BankVariance  " & _
"	, ard.ModifiedDate " & _
"	, isnull(isnull(u.UserDropDownListName, left(UserFullName, 10)), ard.ModifiedBy)  " & _
"	 " & _
"	from DWH.AR_GECC.ActivityReport ar  " & _
"	join DWH.AR_GECC.ActivityReport_Detail ard on ar.ActivityID = ard.ActivityID 	 " & _
"	left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on u.rn = 1 and ard.ModifiedBy = u.UserLogin  " & _
"	            left join (select ActivityID, sum(MiscAmt) as MiscAmt, convert(Date, ActivityDate) as MiscDate, TFID from    " & _
"            		DWH.AR_GECC.ActivityReport_MiscGL m " & _
"					join (select distinct TFID, TransferDate, ModifyDate from DWH.AR_GECC.ActivityReport_Transfers where Active = 1) t on convert(date, m.ActivityDate) <= t.TransferDate " & _
"						and m.ModifiedDate <= t.ModifyDate  " & _
"					where m.Active = 1   group by t.TFID, ActivityID, convert(Date, ActivityDate)) mgl on ard.ActivityID = mgl.ActivityID  and mgl.TFID = ard.ActivityID " & _
"             and mgl.MiscDate = ard.ActivityDate " & _
"            left join (select TransferFrom, sum(Amount) TransferredOut, convert(Date, tt.TransferDate) as TFDate from DWH.AR_GECC.ActivityReport_Transfers tt " & _
"            		where Active = 1    " & _
"            		group by  TransferFrom, convert(Date, tt.TransferDate) ) tff on tff.TransferFrom = ard.ActivityID and tff.TFDate = ard.ActivityDate   " & _
"            left join (select  TransferTo, sum(Amount) TransferredIn, convert(date, tf.TransferDate) as TFTDate from DWH.AR_GECC.ActivityReport_Transfers  tf " & _
"            		where Active = 1 " & _
"            		group by  TransferTo, convert(date, tf.TransferDate)) tft on  tft.TransferTo = ard.ActivityID and TFTDate = ard.ActivityDate   " & _
" 		where ar.ActivityID = @SearchID and ar.Active = 1 " & _
") x	 " & _
" " & _
"select a.* " & _
", case when a.ChangeType = 'Update' then isnull(a.Cash_Received, 0) - isnull(b.Cash_Received, 0) - isnull(a.AR_Posted, 0) + isnull(b.AR_Posted, 0)  " & _
"	- isnull(a.Misc_Posted, 0) + isnull(b.Misc_Posted, 0) - isnull(a.Interest, 0) + isnull(b.Interest, 0) - isnull(a.Unresolved, 0) + isnull(b.Unresolved, 0) " & _
"	- isnull(a.Carry_Forward, 0) + isnull(b.Carry_Forward, 0)  " & _
"	else a.Amount end as AmountAffected " & _
"/*, case when isnull(a.ActivityDate, '1/1/1800') <> isnull(b.ActivityDate, '1/1/1800') then 1 else 0 end as ActivityDateChange " & _
", case when isnull(a.ChangeType, '') <> isnull(b.ChangeType, '') then 1 else 0 end as ChangeTypeChange  " & _
", case when isnull(a.Amount, 0) <> isnull(b.Amount, 0) then 1 else 0 end as ChangeTypeChange  " & _
", case when case when a.ChangeType = 'Update' then isnull(a.Cash_Received, 0) - isnull(b.Cash_Received, 0) + isnull(a.AR_Posted, 0) - isnull(b.AR_Posted, 0)  " & _
"	+ isnull(a.Misc_Posted, 0) - isnull(b.Misc_Posted, 0) + isnull(a.Interest, 0) - isnull(b.Interest, 0) + isnull(a.Unresolved, 0) - isnull(b.Unresolved, 0) " & _
"	+ isnull(a.Carry_Forward, 0) - isnull(b.Carry_Forward, 0)  " & _
"	else a.Amount end = 0 then 0 else 1 end as AmountAffetedChagne " & _
", case when isnull(a.Facility, '') <> isnull(b.Facility, '') then 1 else 0 end as FacilityChange  " & _
", case when isnull(a.CashCategory, '') <> isnull(b.CashCategory, '') then 1 else 0 end as CashCategoryChange  " & _
", case when isnull(a.DetailStatus, '') <> isnull(b.DetailStatus, '') then 1 else 0 end as DetailStatusChange  " & _
", case when isnull(a.BankBatchNumber, '') <> isnull(b.BankBatchNumber, '') then 1 else 0 end as BankBatchNumberChange  " & _
", case when isnull(a.STARBatchNumber, '') <> isnull(b.STARBatchNumber, '') then 1 else 0 end as STARBatchNumberChange  " & _
", case when isnull(a.NoPatients, 0) <> isnull(b.NoPatients, 0) then 1 else 0 end as NoPatientsChange  " & _
", case when isnull(a.Type, '') <> isnull(b.Type, '') then 1 else 0 end as TypeChange  " & _
", case when isnull(a.Cash_Received, 0) <> isnull(b.Cash_Received, 0) then 1 else 0 end as Cash_ReceivedChange  " & _
", case when isnull(a.AR_Posted, 0) <> isnull(b.AR_Posted, 0) then 1 else 0 end as AR_PostedChange  " & _
", case when isnull(a.Misc_Posted, 0) <> isnull(b.Misc_Posted, 0) then 1 else 0 end as Misc_PostedChange  " & _
", case when isnull(a.Interest, 0) <> isnull(b.Interest, 0) then 1 else 0 end as InterestChange  " & _
", case when isnull(a.Transfers, 0) <> isnull(b.Transfers, 0) then 1 else 0 end as TransferChange  " & _
", case when isnull(a.Unresolved, 0) <> isnull(b.Unresolved, 0) then 1 else 0 end as UnresolvedChange  " & _
", case when isnull(a.Carry_Forward, 0) <> isnull(b.Carry_Forward, 0) then 1 else 0 end as Carry_ForwardChange  " & _
", case when isnull(a.STARVariance, 0) <> isnull(b.STARVariance, 0) then 1 else 0 end as STARVarianceChange  " & _
", case when isnull(a.BankVariance, 0) <> isnull(b.BankVariance, 0) then 1 else 0 end as BankVarianceChange  " & _
", case when isnull(a.ModifyDate, '1/1/1800') <> isnull(b.ModifyDate, '1/1/1800') then 1 else 0 end as ModifyDateChange " & _
", case when isnull(a.UserDisplay, '') <> isnull(b.UserDisplay, '') then 1 else 0 end as UserDisplayChange */ " & _
"from #TmpHistory a " & _
"left join #TmpHistory b on a.RN = b.RN + 1 " & _
"order by RN "

        Dim z As DataView = GetData(s).DefaultView
        Return z
        'n.DataSource = GetData(s).DefaultView
        'n.DataBind()


    End Function

    'Private Sub gv_AR_MainData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gv_AR_MainData.PageIndexChanging
    '    Try


    '        gv_AR_MainData.PageIndex = e.NewPageIndex
    '        gv_AR_MainData.DataSource = ARDetailView
    '        gv_AR_MainData.DataBind()


    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub gv_AR_MainData_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gv_AR_MainData.Sorting
    '    Try

    '        Dim dv As DataView
    '        Dim sorts As String
    '        dv = ARDetailView

    '        sorts = e.SortExpression

    '        If e.SortExpression = ARDetailmap Then

    '            If ARDetaildir = 1 Then
    '                dv.Sort = sorts + " " + "desc"
    '                ARDetaildir = 0
    '            Else
    '                dv.Sort = sorts + " " + "asc"
    '                ARDetaildir = 1
    '            End If

    '        Else
    '            dv.Sort = sorts + " " + "asc"
    '            ARDetaildir = 1
    '            ARDetailmap = e.SortExpression
    '        End If

    '        gv_AR_MainData.DataSource = dv
    '        gv_AR_MainData.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    Private Sub gvInitialDisplay_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvInitialDisplay.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then


                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

            End If

        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvInitialDisplay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvInitialDisplay.SelectedIndexChanged
        rowingInitialDisplay()
        lbldateforgreen.Text = gvInitialDisplay.SelectedRow.Cells(7).Text
        PopulategvLetterGrid(gvInitialDisplay.SelectedRow.Cells(1).Text)
    End Sub
    Private Sub rowingInitialDisplay()

        For Each canoe As GridViewRow In gvInitialDisplay.Rows
            If canoe.RowIndex = gvInitialDisplay.SelectedIndex Then
                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ActivityReportHistorySelectedColor) 'white
            Else
                If canoe.RowIndex() Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ActivityReportHistoryMainColor)
                Else
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ActivityReportHistoryAlternateColor)
                End If
            End If

        Next

    End Sub
    Private Sub PopulategvLetterGrid(x As String)
        Dim s As String = "declare @SearchID int =  " & SelectedDetailID.Text & _
"	select * from (select * " & _
" , case when FinalActivityDate is not null then 'Closed' else (select top 1 ar.DetailStatus from DWH.AR_GECC.ActivityReport_Detail ar " & _
"           where ar.Active = 1 and ar.ActivityID = N.ActivityID order by ar.ActivityDate desc, ar.ModifiedDate desc) end as Status " & _
",  LetterCodes = " & _
"      Coalesce((SELECT Char(65 + (N.Num - 475255) / 456976 % 26) WHERE N.Num >= 475255), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 18279) / 17576 % 26) WHERE N.Num >= 18279), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 703) / 676 % 26) WHERE N.Num >= 703), '') " & _
"      + Coalesce((SELECT Char(65 + (N.Num - 27) / 26 % 26) WHERE N.Num >= 27), '') " & _
"      + (SELECT Char(65 + (N.Num - 1) % 26)) 	   " & _
"	  from ( " & _
"select ar.ActivityID, FirstActivityDate, FinalActivityDate, ar.DepositDate " & _
" , isnull(s.Source, isnull(UserFullName, ar.CreatedBy)) as RowSource, s.NetAmount, s.ItemDescription, s.UniqueID " & _
", DENSE_RANK() over (order by ar.ActivityID) as num " & _
"from DWH.AR_GECC.ActivityReport ar " & _
"	join (select NetID as ActivityID from DWH.AR_GECC.ActivityReport_Netting  " & _
"	where InitialID = @SearchID and Active = 1  " & _
"	union   " & _
"	select InitialID from DWH.AR_GECC.ActivityReport_Netting  " & _
"	where NetID = @SearchID  and Active = 1  " & _
"	union  " & _
"	select TransferFrom from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) x  " & _
"	where isnull(IntermediateRow, 0) = @SearchID or TransferTo = @SearchID and Active = 1  " & _
"	union   " & _
"	select TransferTo from  (select * from DWH.AR_GECC.ActivityReport_Transfers union all select * from DWH.AR_GECC.ActivityReport_TransfersHistorical) x  " & _
"	where isnull(IntermediateRow, 0) = @SearchID or TransferFrom = @SearchID and Active = 1  " & _
"	union select @SearchID) find on ar.ActivityID = find.ActivityID  " & _
"left join DWH.AR_GECC.ActivityReport_2_Source a2s on ar.ActivityID = a2s.ActivityID and a2s.Active = 1 " & _
"left join DWh.AR_GECC.ActivityReport_Source s on a2s.Base_ID = s.Base_ID and s.Active = 1 " & _
"left join (select *, ROW_NUMBER() over (partition by UserLogin order by Active desc, DateModified desc) rn From DWH.AR_GECC.ActivityReport_Users) u on ar.CreatedBy = u.UserLogin and u.rn = 1 " & _
"where ar.Active = 1  ) N ) n2 where ('" & x & "' = LetterCodes or '" & x & "' like '%,'+ LetterCodes or '" & x & "' like LetterCodes+',%' or '" & x & "' like '%,'+ LetterCodes +',%') "

        gv_Selected_Grids.DataSource = GetData(s)
        gv_Selected_Grids.DataBind()

    End Sub

    Private Sub gv_Selected_Grids_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gv_Selected_Grids.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim gv_AR_MainData As GridView = e.Row.FindControl("gv_AR_MainData")

            Dim v As DataView = BindGrid(e.Row.DataItem("ActivityID"))
            gv_AR_MainData.DataSource = v
            gv_AR_MainData.DataBind()

            Dim n As Integer

            For Each canoe As GridViewRow In gv_AR_MainData.Rows
                If v(n)("ModifyDate").ToString = lbldateforgreen.Text Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ActivityReportHistorySelectedColor) 'white
                Else
                    If n Mod 2 = 0 Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ActivityReportHistoryMainColor)
                    Else
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml(ActivityReportHistoryAlternateColor)
                    End If
                End If
                n += 1

            Next

        End If



    End Sub
End Class