Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class Suntrust_Allocation
    Inherits System.Web.UI.Page

    Private Sub Suntrust_Allocation_Init(sender As Object, e As EventArgs) Handles Me.Init

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                Dim r As New Random()
                Dim n As Integer = r.Next(10)

                Dim mp As TableCell = upProg_Pacman.FindControl("tcAjaxLoader")
                If Now().Hour < 9 Then
                    If n < 7 Then
                        mp.Attributes.Add("class", "AjaxLoader2")
                    Else
                        mp.Attributes.Add("class", "AjaxLoader1")
                    End If
                Else
                    If n < 3 Then
                        mp.Attributes.Add("class", "AjaxLoader2")
                    Else
                        mp.Attributes.Add("class", "AjaxLoader1")
                    End If
                End If


                CheckPermissions()

                Dim ddate As Date = GetDate("select max(Calendar_Date) from DWH.SUNTRUST.SUNTRUSTAllActivity st join DWH.dbo.DimDate dd on st.AsOfDate = dd.Date_ID ")
                txtStartRange.Text = ddate
                txtEndRange.Text = ddate
                PopulateDDLTranDesc()
                PopulateDDLBaiType()
                PopulateMainData()


            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub CheckPermissions()
        Try
            If GetScalar("select count(*) from ( " &
                    "select Claim_ID, [Order] " &
                    "from DWH.ACCT.Suntrust_Claim_Order_LU  " &
                    "where getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') and InactivatedDate is null " &
                    "union  " &
                    "select -1, 4) clu " &
                    "join dwh.ACCT.Suntrust_User_Access ua on clu.Claim_ID = isnull(ua.Claim_ID, clu.Claim_ID) and ua.InactivatedDate is null " &
                    "and AdminRights = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
                 "'") > 0 Then
                tpUserAdmin.Visible = True

                gvUserAccess.DataSource = LoadAdminData()
                gvUserAccess.DataBind()

            Else
                tpUserAdmin.Visible = False
            End If

            If GetScalar("select count(*) from ( " &
                    "select Claim_ID, [Order] " &
                    "from DWH.ACCT.Suntrust_Claim_Order_LU  " &
                    "where getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') and InactivatedDate is null " &
                    "union  " &
                    "select -1, 4) clu " &
                    "join dwh.ACCT.Suntrust_User_Access ua on clu.Claim_ID = isnull(ua.Claim_ID, clu.Claim_ID) and ua.InactivatedDate is null " &
                    "and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
                 "'") > 0 Then
                tpSuntrustAllocation.Visible = True
            Else
                tpSuntrustAllocation.Visible = False
            End If

            HideENSH.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Suntrust_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
    "' and InactivatedDate is null and isnull(Claim_ID, 1) = 1 ")

            HideGHS.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Suntrust_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
    "' and InactivatedDate is null and isnull(Claim_ID, 2) = 2 ")

            HideNAOS.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Suntrust_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
    "' and InactivatedDate is null and isnull(Claim_ID, 3) = 3 ")

            HideGECC.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Suntrust_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
    "' and InactivatedDate is null and isnull(Claim_ID, 4) = 4 ")

            HideGlancy.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Suntrust_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
    "' and InactivatedDate is null and isnull(Claim_ID, 5) = 5 ")

            HideStrickland.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Suntrust_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
    "' and InactivatedDate is null and isnull(Claim_ID, 6) = 6 ")

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
    Private Shared Function GetDate(query As String) As Date

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandText = query

                cmd.Connection = con

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim dt As Date

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

    Private Function LoadMainData()

        Dim EffFrom As Date
        Dim EffTo As Date

        Date.TryParse(RememberedStartDate.Text, EffFrom)
        Date.TryParse(RememberedEndDate.Text, EffTo)

        Dim IncludedTranDescs As String = ""
        For i As Integer = 0 To cblTranDescs.Items.Count - 1
            If cblTranDescs.Items(i).Selected = True Then
                IncludedTranDescs += "'" & Replace(cblTranDescs.Items(i).Value, "'", "''") & "', "
            End If
        Next

        Dim IncludedBaiTypes As String = ""
        For i As Integer = 0 To cblBaiType.Items.Count - 1
            If cblBaiType.Items(i).Selected = True Then
                IncludedBaiTypes += "'" & Replace(cblBaiType.Items(i).Value, "'", "''") & "', "
            End If
        Next

        Dim x As String = "select *        , ConComments.Comments as Comments " &
        ", case when len(ConComments.Comments) > 40 then 'True' else 'False' end as CommentFlag  " &
        ", case when len(ConComments.Comments) > 40 then left(replace(ConComments.Comments, '&lt;br&gt;&lt;br&gt;', '; '), 40) else replace(ConComments.Comments, '&lt;br&gt;&lt;br&gt;', ';') end as CommentsMini  " &
        " from (select convert(varchar, UniqueActivityID) as UniqueActivityID, dd.Calendar_Date as AsOfDate, BAITypeCode, Trandesc, st.BankReference as Description, NetAmount, AcctNo, '' as BankReference " &
        ", sum(case when x.Claim_ID = 1 then gma.Claimed else null end) as ENSHClaimed " &
        ", max(case when x.Claim_ID = 1 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as ENSHVision		 " &
        ", max(case when x.Claim_ID = 1 then ua.Permission else 0 end) as ENSHPermission " &
        ", sum(case when x.Claim_ID = 2 then gma.Claimed else null end) as GHSClaimed " &
        ", max(case when x.Claim_ID = 2 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GHSVision		 " &
        ", max(case when x.Claim_ID = 2 then ua.Permission else 0 end) as GHSPermission " &
        ", sum(case when x.Claim_ID = 3 then gma.Claimed else null end) as NAOSClaimed " &
        ", max(case when x.Claim_ID = 3 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as NAOSVision		 " &
        ", max(case when x.Claim_ID = 3 then ua.Permission else 0 end) as NAOSPermission " &
        ", sum(case when x.Claim_ID = 4 then gma.Claimed else null end) as GFNAOSlaimed " &
        ", max(case when x.Claim_ID = 4 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GECCVision		 " &
        ", max(case when x.Claim_ID = 4 then ua.Permission else 0 end) as GECCPermission        " &
        ", sum(case when x.Claim_ID = 5 then gma.Claimed else null end) as GlancyClaimed " &
        ", max(case when x.Claim_ID = 5 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GlancyVision		 " &
        ", max(case when x.Claim_ID = 5 then ua.Permission else 0 end) as GlancyPermission        " &
        ", sum(case when x.Claim_ID = 6 then gma.Claimed else null end) as StricklandClaimed " &
        ", max(case when x.Claim_ID = 6 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as StricklandVision		 " &
        ", max(case when x.Claim_ID = 6 then ua.Permission else 0 end) as StricklandPermission        " &
        ", NetAmount - sum(isnull(gma.Claimed, 0)) as Unclaimed  " &
        "from  " &
        "DWH.SUNTRUST.SUNTRUSTAllActivity st  " &
        "join DWH.dbo.DimDate dd on st.AsOfDate = dd.Date_ID   " &
        "left join ( " &
        "select distinct cl.Claim_ID, cl.Description as OrderDescription, [Order], Dense_Rank() over (partition by cl.Claim_ID order by clu.EffectiveFrom desc) RN_Ord   " &
        "from DWH.ACCT.Suntrust_Claim_LU cl " &
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " &
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " &
        "join DWH.ACCT.Suntrust_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " &
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " &
        "union select -1, 'Other', 999, 1 ) x on RN_Ord = 1  " &
        "left join ( " &
        "select distinct cl.Claim_ID,  isnull([Order], 999) as [Order], Dense_Rank() over (partition by cl.Claim_ID order by cl.EffectiveFrom desc) RN_Ord   " &
        "from DWH.ACCT.Suntrust_Claim_LU cl " &
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " &
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " &
        "left join DWH.ACCT.Suntrust_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " &
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " &
         ") x2 on x2.RN_Ord = 1 and x.[Order] = x2.[Order]  " &
        "left join DWH.ACCT.Suntrust_Allocation gma on st.UniqueActivityID = gma.STUniqueActivityID and gma.InactivatedDate is null and x2.Claim_ID = gma.Claim_ID	 " &
        " " &
                "left join DWH.ACCT.Suntrust_User_Access ua on ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
                 "' and ua.InactivatedDate is null and isnull(ua.Claim_ID, x.Claim_ID) = x.Claim_ID " &
                "where 10 = 10 and AcctNo = '1400018279' " &
        "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' and isnull(TranDesc, 'null') in (" & IncludedTranDescs & " 'null')  " &
        " and isnull(BAITypeCode, 'null') in (" & IncludedBaiTypes & " 'null') "

        If Trim(txtSearchValue.Text) = "" Then
        Else
            Dim moneymoney As Decimal

            If Decimal.TryParse(Replace(Trim(txtSearchValue.Text), "'", "''"), moneymoney) Then
                Dim y As String = Replace(Trim(RememberedText.Text), "'", "''")
                x += "and (TranDesc like '%" & y & "%' or Description like '%" & y & "%' or st.BankReference like '%" & y & "%' or AcctNo like '%" & y & "%' or NetAmount like '%" & moneymoney & "%')"
            Else
                Dim y As String = Replace(Trim(RememberedText.Text), "'", "''")
                x += "and (TranDesc like '%" & y & "%' or Description like '%" & y & "%' or st.BankReference like '%" & y & "%' or AcctNo like '%" & y & "%')"
            End If



        End If



        x += "group by convert(varchar, UniqueActivityID) , dd.Calendar_Date , BAITypeCode, Trandesc, st.BankReference, NetAmount, AcctNo ) x " &
        "	left join (	Select distinct ST2.UniqueActivityID, " &
        "    substring( " &
        "        ( " &
        "					Select ST1.AddedBy+': '+ST1.Comment + ' ('+ convert(varchar, AddedDate, 101) + ') <br><br>' AS [text()] " &
        "					From DWH.ACCT.Suntrust_Allocation_Comments ST1 " &
        "					Where ST1.InactivatedDate is null and ST1.STUniqueActivityID = ST2.UniqueActivityID " &
        "					ORDER BY ST1.STUniqueActivityID " &
        "					For XML PATH ('') " &
        "				), 1, 1000) [Comments] " &
        "		From DWH.SUNTRUST.SUNTRUSTAllActivity ST2  with (nolock) join DWH.dbo.DimDate dd on ST2.AsOfDate = dd.Date_ID  " &
        "           where 10 = 10 and AcctNo = '1400018279' " &
                "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' ) ConComments on x.UniqueActivityID = ConComments.UniqueActivityID "

        If RememberedCheck.Text = "False" Then
            x += " where   ( ENSHVision + ENSHPermission + GHSVision + GHSPermission + NAOSVision + NAOSPermission + GECCVision + GECCPermission + GlancyVision + GlancyPermission + StricklandVision + StricklandPermission = 6 and Unclaimed <> 0)  " &
             "or (ENSHVision + ENSHPermission + GHSVision + GHSPermission + NAOSVision + NAOSPermission + GECCVision + GECCPermission + GlancyVision + GlancyPermission + StricklandVision + StricklandPermission < 6 " &
            "	and (ENSHPermission = 0 or ENSHClaimed is null) " &
            "	and (GHSPermission = 0 or GHSClaimed is null) " &
            "	and (NAOSPermission = 0 or NAOSClaimed is null) " &
            "	and (GECCPermission = 0 or GFNAOSlaimed is null) " &
            "   and (GlancyPermission = 0 or GlancyClaimed is null) " &
            "   and (StricklandPermission = 0 or StricklandClaimed is null)) "

        End If

        Return GetData(x).DefaultView

    End Function

    Private Function LoadSummaryData()

        Dim EffFrom As Date
        Dim EffTo As Date

        Date.TryParse(RememberedStartDate.Text, EffFrom)
        Date.TryParse(RememberedEndDate.Text, EffTo)

        Dim IncludedTranDescs As String = ""
        For i As Integer = 0 To cblTranDescs.Items.Count - 1
            If cblTranDescs.Items(i).Selected = True Then
                IncludedTranDescs += "'" & Replace(cblTranDescs.Items(i).Value, "'", "''") & "', "
            End If
        Next

        Dim IncludedBAIType As String = ""
        For i As Integer = 0 To cblBaiType.Items.Count - 1
            If cblBaiType.Items(i).Selected = True Then
                IncludedBAIType += "'" & Replace(cblBaiType.Items(i).Value, "'", "''") & "', "
            End If
        Next


        Dim x As String = "select AsOfDate, sum(NetAmount) as NetAmount " &
    ", sum(ENSHClaimed) as ENSHClaimed, min(ENSHVision) + min(ENSHPermission) as ENSHVision " &
    ", sum(GHSClaimed) as GHSClaimed, min(GHSVision) +  min(GHSPermission) as GHSVision " &
    ", sum(NAOSClaimed) as NAOSClaimed, min(NAOSVision) + min(NAOSPermission) as NAOSVision " &
    ", sum(GFNAOSlaimed) as GFNAOSlaimed, min(GECCVision) + min(GECCPermission) as GECCVision " &
    ", sum(GlancyClaimed) as GlancyClaimed, min(GlancyVision) + min(GlancyPermission) as GlancyVision " &
    ", sum(StricklandClaimed) as StricklandClaimed, min(StricklandVision) + min(StricklandPermission) as StricklandVision, sum(Unclaimed) as Unclaimed" &
" " &
"from ( " &
   "select convert(varchar, UniqueActivityID) as UniqueActivityID, dd.Calendar_Date as AsOfDate, BAITypeCode, Trandesc, st.BankReference as Description, NetAmount, AcctNo, '' as BankReference " &
        ", sum(case when x.Claim_ID = 1 then gma.Claimed else null end) as ENSHClaimed " &
        ", max(case when x.Claim_ID = 1 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as ENSHVision		 " &
        ", max(case when x.Claim_ID = 1 then ua.Permission else 0 end) as ENSHPermission " &
        ", sum(case when x.Claim_ID = 2 then gma.Claimed else null end) as GHSClaimed " &
        ", max(case when x.Claim_ID = 2 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GHSVision		 " &
        ", max(case when x.Claim_ID = 2 then ua.Permission else 0 end) as GHSPermission " &
        ", sum(case when x.Claim_ID = 3 then gma.Claimed else null end) as NAOSClaimed " &
        ", max(case when x.Claim_ID = 3 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as NAOSVision		 " &
        ", max(case when x.Claim_ID = 3 then ua.Permission else 0 end) as NAOSPermission " &
        ", sum(case when x.Claim_ID = 4 then gma.Claimed else null end) as GFNAOSlaimed " &
        ", max(case when x.Claim_ID = 4 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GECCVision		 " &
        ", max(case when x.Claim_ID = 4 then ua.Permission else 0 end) as GECCPermission        " &
        ", sum(case when x.Claim_ID = 5 then gma.Claimed else null end) as GlancyClaimed " &
        ", max(case when x.Claim_ID = 5 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GlancyVision		 " &
        ", max(case when x.Claim_ID = 5 then ua.Permission else 0 end) as GlancyPermission        " &
        ", sum(case when x.Claim_ID = 6 then gma.Claimed else null end) as StricklandClaimed " &
        ", max(case when x.Claim_ID = 6 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as StricklandVision		 " &
        ", max(case when x.Claim_ID = 6 then ua.Permission else 0 end) as StricklandPermission        " &
        ", NetAmount - sum(isnull(gma.Claimed, 0)) as Unclaimed  " &
        "from  " &
        "DWH.SUNTRUST.SUNTRUSTAllActivity st  " &
        "join DWH.dbo.DimDate dd on st.AsOfDate = dd.Date_ID   " &
        "left join ( " &
        "select distinct cl.Claim_ID, cl.Description as OrderDescription, [Order], Dense_Rank() over (partition by cl.Claim_ID order by clu.EffectiveFrom desc) RN_Ord   " &
        "from DWH.ACCT.Suntrust_Claim_LU cl " &
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " &
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " &
        "join DWH.ACCT.Suntrust_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " &
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " &
        "union select -1, 'Other', 999, 1 ) x on RN_Ord = 1  " &
        "left join ( " &
        "select distinct cl.Claim_ID,  isnull([Order], 999) as [Order], Dense_Rank() over (partition by cl.Claim_ID order by cl.EffectiveFrom desc) RN_Ord   " &
        "from DWH.ACCT.Suntrust_Claim_LU cl " &
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " &
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " &
        "left join DWH.ACCT.Suntrust_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " &
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " &
         ") x2 on x2.RN_Ord = 1 and x.[Order] = x2.[Order]  " &
        "left join DWH.ACCT.Suntrust_Allocation gma on st.UniqueActivityID = gma.STUniqueActivityID and gma.InactivatedDate is null and x2.Claim_ID = gma.Claim_ID	 " &
        " " &
                "left join DWH.ACCT.Suntrust_User_Access ua on ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
                 "' and ua.InactivatedDate is null and isnull(ua.Claim_ID, x.Claim_ID) = x.Claim_ID " &
                        "where 10 = 10 and AcctNo = '1400018279'  " &
                "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' and isnull(TranDesc, 'null') in (" & IncludedTranDescs & " 'null')" &
                " and isnull(BAITypeCode, 'null') in (" & IncludedBAIType & " 'null') " &
        "group by convert(varchar, UniqueActivityID) , dd.Calendar_Date , BAITypeCode, Trandesc, st.BankReference, NetAmount, AcctNo " &
    ") x " &
    "group by AsOfDate order by AsOfDate "

        Return GetData(x).DefaultView

    End Function


    Private Function LoadAdminData()

        SynchAccess()

        Dim x As String = "				select u.UserLogin, u2.EmployeeNumber, u2.FirstName, u2.LastName, u2.EmailAddress , u2.Dept " &
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 1) = 1 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 1) = 1 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 1) = 1 and u.Vision = 1 then 1 else 0 end) as ENSH " &
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 2) = 2 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 2) = 2 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 2) = 2 and u.Vision = 1 then 1 else 0 end) as GHS " &
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 3) = 3 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 3) = 3 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 3) = 3 and u.Vision = 1 then 1 else 0 end) as NAOS " &
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 4) = 4 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 4) = 4 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 4) = 4 and u.Vision = 1 then 1 else 0 end) as GECC " &
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 5) = 5 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 5) = 5 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 5) = 5 and u.Vision = 1 then 1 else 0 end) as Glancy " &
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 6) = 6 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 6) = 6 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 6) = 6 and u.Vision = 1 then 1 else 0 end) as Strickland " &
" " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 1) = 1 and ua.AdminRights = 1 then 1 else 0 end) as UpdateENSH " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 2) = 2 and ua.AdminRights = 1 then 1 else 0 end) as UpdateGHS " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 3) = 3 and ua.AdminRights = 1 then 1 else 0 end) as UpdateNAOS " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 4) = 4 and ua.AdminRights = 1 then 1 else 0 end) as UpdateGECC " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 5) = 5 and ua.AdminRights = 1 then 1 else 0 end) as UpdateGlancy " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 6) = 6 and ua.AdminRights = 1 then 1 else 0 end) as UpdateStrickland " &
" " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 1) = 1 and ua.Vision = 1 then 1 else 0 end) as ViewENSH " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 2) = 2 and ua.Vision = 1 then 1 else 0 end) as ViewGHS " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 3) = 3 and ua.Vision = 1 then 1 else 0 end) as ViewNAOS " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 4) = 4 and ua.Vision = 1 then 1 else 0 end) as ViewGECC " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 5) = 5 and ua.Vision = 1 then 1 else 0 end) as ViewGlancy " &
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 6) = 6 and ua.Vision = 1 then 1 else 0 end) as ViewStrickland " &
" " &
"				from		DWH.ACCT.Suntrust_User_Access u " &
"				left join DWH.ACCT.Suntrust_User_Access ua on isnull(isnull(u.Claim_ID, ua.Claim_ID), 999) = isnull(isnull(ua.Claim_ID, u.Claim_ID), 999) and ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
                 "' and ua.InactivatedDate is null " &
"				left join (select u.UserName from WebFD.dbo.aspnet_Users u " &
"		join WebFD.dbo.vw_aspnet_UsersInRoles uir on u.UserId = uir.UserId " &
"		join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") &
                 "') x on 1=1 " &
"				left join DWH.bed.Users u2 on u.UserLogin = u2.UserLogin " &
                "where u.InactivatedDate is null " &
"		group by u.UserLogin, u2.EmployeeNumber, u2.FirstName, u2.LastName, u2.EmailAddress , u2.Dept "

        Return GetData(x).DefaultView

    End Function

    Private Sub PopulateMainData()
        Try
            Dim EffFrom As Date
            Dim EffTo As Date

            If Date.TryParse(txtStartRange.Text, EffFrom) Then
            Else
                lblExplanationLabel.Text = "Could not parse Start Date"
                mpeStandard.Show()
                Exit Sub
            End If

            If Date.TryParse(txtEndRange.Text, EffTo) Then
            Else
                lblExplanationLabel.Text = "Could not parse End Date"
                mpeStandard.Show()
                Exit Sub
            End If

            Dim tdcnt As Integer = 0
            For i As Integer = 0 To cblTranDescs.Items.Count - 1
                If cblTranDescs.Items(i).Selected = True Then
                    tdcnt += 1
                End If
            Next

            If tdcnt = 0 Then
                lblExplanationLabel.Text = "No Tran Descs Selected"
                mpeStandard.Show()
                Exit Sub
            End If

            Dim btcnt As Integer = 0
            For i As Integer = 0 To cblBaiType.Items.Count - 1
                If cblBaiType.Items(i).Selected = True Then
                    btcnt += 1
                End If
            Next

            If btcnt = 0 Then
                lblExplanationLabel.Text = "No BAI Types Selected"
                mpeStandard.Show()
                Exit Sub
            End If

            RememberedStartDate.Text = txtStartRange.Text
            RememberedEndDate.Text = txtEndRange.Text
            RememberedText.Text = txtSearchValue.Text
            RememberedCheck.Text = cbShowBalanced.Checked.ToString

            gvSearch.DataSource = LoadMainData()
            gvSearch.DataBind()

            gvSummary.DataSource = LoadSummaryData()
            gvSummary.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        PopulateMainData()
    End Sub

    Private Sub gvSearch_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSearch.PageIndexChanging
        gvSearch.PageIndex = e.NewPageIndex
        gvSearch.DataSource = LoadMainData()
        gvSearch.DataBind()
    End Sub

    Private Sub gvSearch_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSearch.RowCreated
        Try
            If HideENSH.Text = "0" Then
                e.Row.Cells(7).CssClass = "hidden"
                e.Row.Cells(13).CssClass = "hidden"
            End If
            If HideGHS.Text = "0" Then
                e.Row.Cells(8).CssClass = "hidden"
                e.Row.Cells(13).CssClass = "hidden"
            End If
            If HideNAOS.Text = "0" Then
                e.Row.Cells(9).CssClass = "hidden"
                e.Row.Cells(13).CssClass = "hidden"
            End If
            If HideGECC.Text = "0" Then
                e.Row.Cells(10).CssClass = "hidden"
                e.Row.Cells(13).CssClass = "hidden"
            End If
            If HideGlancy.Text = "0" Then
                e.Row.Cells(11).CssClass = "hidden"
                e.Row.Cells(13).CssClass = "hidden"
            End If
            If HideStrickland.Text = "0" Then
                e.Row.Cells(12).CssClass = "hidden"
                e.Row.Cells(13).CssClass = "hidden"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSubmitRows_Click(sender As Object, e As EventArgs) Handles btnSubmitRows.Click
        Try

            Dim UpdateSQL As String = ""
            Dim cnt As Integer = 0

            For Each boat As GridViewRow In gvSearch.Rows

                If boat.RowType = DataControlRowType.DataRow Then

                    Dim ENSHAlloc, GHSAlloc, OldENSH, OldGHS, OldNAOS, NAOSAlloc, GECCAlloc, OldGECC, GlancyAlloc, OldGlancy, StricklandAlloc, OldStrickland As String
                    Dim Did As String

                    ENSHAlloc = ""
                    GHSAlloc = ""
                    OldENSH = ""
                    OldGHS = ""
                    OldNAOS = ""
                    OldGECC = ""
                    NAOSAlloc = ""
                    GECCAlloc = ""
                    GlancyAlloc = ""
                    OldGlancy = ""
                    StricklandAlloc = ""
                    OldStrickland = ""

                    Did = gvSearch.DataKeys(boat.RowIndex).Value

                    OldENSH = boat.Cells(7).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    OldGHS = boat.Cells(8).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    ENSHAlloc = boat.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    GHSAlloc = boat.Cells(8).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString

                    NAOSAlloc = boat.Cells(9).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    OldNAOS = boat.Cells(9).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    GECCAlloc = boat.Cells(10).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    OldGECC = boat.Cells(10).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    GlancyAlloc = boat.Cells(11).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    OldGlancy = boat.Cells(11).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    StricklandAlloc = boat.Cells(12).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    OldStrickland = boat.Cells(12).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    Dim commentsubmission = boat.Cells(14).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString

                    If Trim(commentsubmission) <> "" Then
                        UpdateSQL += " Insert into DWH.Acct.Suntrust_Allocation_Comments (STUniqueActivityID, Comment, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "'), '" & Replace(commentsubmission, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() "

                    End If

                    If HideGHS.Text = "1" Then

                        If GHSAlloc <> OldGHS Then
                            If IsNumeric(GHSAlloc) = False And GHSAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & GHSAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Suntrust_Allocation " &
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " &
                            "where STUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 2 and InactivatedDate is null and (isnull(Claimed, '') <> '" & GHSAlloc & "' ) " &
                            "insert into DWH.ACCT.Suntrust_Allocation (STUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "')" &
                            ", 2 " &
                            ", case when '" & GHSAlloc & "' = '' then null else '" & GHSAlloc & "' end " &
                            ", case when '" & GHSAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " &
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " &
                            "where not exists (select * from DWH.ACCT.Suntrust_Allocation b " &
                            "where convert(varbinary, '" & Did & "') = b.STUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 2) "

                            cnt += 1
                        End If
                    End If

                    If HideENSH.Text = "1" Then

                        If OldENSH <> ENSHAlloc Then
                            If IsNumeric(ENSHAlloc) = False And ENSHAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & ENSHAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If

                            UpdateSQL += "update DWH.ACCT.Suntrust_Allocation " &
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " &
                            "where STUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 1 and InactivatedDate is null and (isnull(Claimed, '') <> '" & ENSHAlloc & "' ) " &
                            "insert into DWH.ACCT.Suntrust_Allocation (STUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "')" &
                            ", 1 " &
                            ", case when '" & ENSHAlloc & "' = '' then null else '" & ENSHAlloc & "' end " &
                            ", case when '" & ENSHAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " &
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " &
                            "where not exists (select * from DWH.ACCT.Suntrust_Allocation b " &
                            "where convert(varbinary, '" & Did & "') = b.STUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 1) "

                            cnt += 1
                        End If
                    End If

                    If HideNAOS.Text = "1" Then

                        If NAOSAlloc <> OldNAOS Then
                            If IsNumeric(NAOSAlloc) = False And NAOSAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & NAOSAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Suntrust_Allocation " &
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " &
                            "where STUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 3 and InactivatedDate is null and (isnull(Claimed, '') <> '" & NAOSAlloc & "' ) " &
                            "insert into DWH.ACCT.Suntrust_Allocation (STUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "')" &
                            ", 3 " &
                            ", case when '" & NAOSAlloc & "' = '' then null else '" & NAOSAlloc & "' end " &
                            ", case when '" & NAOSAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " &
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " &
                            "where not exists (select * from DWH.ACCT.Suntrust_Allocation b " &
                            "where convert(varbinary, '" & Did & "') = b.STUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 3) "


                            cnt += 1
                        End If
                    End If


                    If HideGECC.Text = "1" Then

                        If GECCAlloc <> OldGECC Then
                            If IsNumeric(GECCAlloc) = False And GECCAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & GECCAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Suntrust_Allocation " &
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " &
                            "where STUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 4 and InactivatedDate is null and (isnull(Claimed, '') <> '" & GECCAlloc & "' ) " &
                            "insert into DWH.ACCT.Suntrust_Allocation (STUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "')" &
                            ", 4 " &
                            ", case when '" & GECCAlloc & "' = '' then null else '" & GECCAlloc & "' end " &
                            ", case when '" & GECCAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " &
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " &
                            "where not exists (select * from DWH.ACCT.Suntrust_Allocation b " &
                            "where convert(varbinary, '" & Did & "') = b.STUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 4) "

                            cnt += 1
                        End If
                    End If

                    If HideGlancy.Text = "1" Then

                        If GlancyAlloc <> OldGlancy Then
                            If IsNumeric(GlancyAlloc) = False And GlancyAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & GlancyAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Suntrust_Allocation " &
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " &
                            "where STUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 5 and InactivatedDate is null and (isnull(Claimed, '') <> '" & GlancyAlloc & "' ) " &
                            "insert into DWH.ACCT.Suntrust_Allocation (STUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "')" &
                            ", 5 " &
                            ", case when '" & GlancyAlloc & "' = '' then null else '" & GlancyAlloc & "' end " &
                            ", case when '" & GlancyAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " &
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " &
                            "where not exists (select * from DWH.ACCT.Suntrust_Allocation b " &
                            "where convert(varbinary, '" & Did & "') = b.STUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 5) "

                            cnt += 1
                        End If
                    End If

                    If HideStrickland.Text = "1" Then

                        If StricklandAlloc <> OldStrickland Then
                            If IsNumeric(StricklandAlloc) = False And StricklandAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & StricklandAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Suntrust_Allocation " &
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " &
                            "where STUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 6 and InactivatedDate is null and (isnull(Claimed, '') <> '" & StricklandAlloc & "' ) " &
                            "insert into DWH.ACCT.Suntrust_Allocation (STUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " &
                            "select convert(varbinary, '" & Did & "')" &
                            ", 6 " &
                            ", case when '" & StricklandAlloc & "' = '' then null else '" & StricklandAlloc & "' end " &
                            ", case when '" & StricklandAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " &
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " &
                            "where not exists (select * from DWH.ACCT.Suntrust_Allocation b " &
                            "where convert(varbinary, '" & Did & "') = b.STUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 6) "

                            cnt += 1
                        End If
                    End If

                End If

            Next

            If UpdateSQL <> "" Then
                ExecuteSql(UpdateSQL)
            End If

            PopulateMainData()

            lblExplanationLabel.Text = "Successfully Updated (" & CStr(cnt) & " rows)"
            lblExplanationLabel.DataBind()
            mpeStandard.Show()


        Catch ex As Exception
            lblExplanationLabel.Text = "Error submitting data.  Please re-check values or contact Admin"
            lblExplanationLabel.DataBind()
            mpeStandard.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSearch_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSearch.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = LoadMainData()

        sorts = e.SortExpression

        If e.SortExpression = MainMap.Text Then

            If MainDir.Text = "1" Then
                dv.Sort = sorts + " " + "desc"
                MainDir.Text = 0
            Else
                dv.Sort = sorts + " " + "asc"
                MainDir.Text = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            MainDir.Text = 1
            MainMap.Text = e.SortExpression
        End If

        gvSearch.DataSource = dv
        gvSearch.DataBind()
    End Sub

    Private Sub gvSummary_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSummary.RowCreated
        Try
            If HideENSH.Text = "0" Then
                e.Row.Cells(2).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"
            End If
            If HideGHS.Text = "0" Then
                e.Row.Cells(3).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"
            End If
            If HideNAOS.Text = "0" Then
                e.Row.Cells(4).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"
            End If
            If HideGECC.Text = "0" Then
                e.Row.Cells(5).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"
            End If
            If HideGlancy.Text = "0" Then
                e.Row.Cells(6).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"
            End If
            If HideStrickland.Text = "0" Then
                e.Row.Cells(7).CssClass = "hidden"
                e.Row.Cells(8).CssClass = "hidden"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gvSummary_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSummary.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = LoadSummaryData()

        sorts = e.SortExpression

        If e.SortExpression = SummaryMap.Text Then

            If SummaryDir.Text = "1" Then
                dv.Sort = sorts + " " + "desc"
                SummaryDir.Text = 0
            Else
                dv.Sort = sorts + " " + "asc"
                SummaryDir.Text = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            SummaryDir.Text = 1
            SummaryMap.Text = e.SortExpression
        End If

        gvSummary.DataSource = dv
        gvSummary.DataBind()
    End Sub


    Private Sub btnNewUserSearch_Click(sender As Object, e As EventArgs) Handles btnNewUserSearch.Click
        Try

            UserSearch(Trim(txtNewUserSearch.Text), rblNewUserSearchResults)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Public Sub UserSearch(s As String, rbl As RadioButtonList)
        Try
            rbl.Items.Clear()

            Dim oroot As DirectoryEntry = New DirectoryEntry("LDAP://northside.local/DC=northside,DC=local")
            Dim osearcher As DirectorySearcher = New DirectorySearcher(oroot)
            osearcher.PageSize = 3000
            Dim oresult As SearchResultCollection
            Dim result As SearchResult

            Dim SQLSrch As String = "create table #TempUsrSearch (UserLogin varchar(50), UserName varchar(max), UserMail varchar(max), Score integer) "
            Dim SubsetSQL As String = ""

            Dim FullQueryString As String = s
            Dim QueryString As String = s
            While Len(FullQueryString) > 0
                If InStr(FullQueryString, " ") > 0 Then
                    QueryString = Left(FullQueryString, InStr(FullQueryString, " "))
                    FullQueryString = Right(FullQueryString, Len(FullQueryString) - InStr(FullQueryString, " "))
                Else
                    QueryString = FullQueryString
                    FullQueryString = ""
                End If

                osearcher.Filter = "(&(cn=*" & QueryString & "*))" ' search filter

                For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                    osearcher.PropertiesToLoad.Add(elem.PropertyName)
                Next
                oresult = osearcher.FindAll()
                For Each result In oresult
                    If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                        SQLSrch += " update #TempUsrSearch set Score = Score + 1 where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") &
                            "'  insert into #TempUsrSearch select '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") &
                            "', '" & Replace(result.GetDirectoryEntry.Properties("cn").Value, "'", "''") & "', '" & Replace(result.GetDirectoryEntry.Properties("mail").Value, "'", "''") &
                            "', 1 where not exists (select * from #TempUsrSearch where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & "') "
                    End If
                Next

                osearcher.Filter = "(&(mail=*" & QueryString & "*))" ' search filter

                For Each elem As System.DirectoryServices.PropertyValueCollection In oroot.Properties
                    osearcher.PropertiesToLoad.Add(elem.PropertyName)
                Next
                oresult = osearcher.FindAll()
                For Each result In oresult
                    If Not result.GetDirectoryEntry.Properties("sn").Value Is Nothing Then
                        SQLSrch += " update #TempUsrSearch set Score = Score + 1 where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") &
                            "'  insert into #TempUsrSearch select '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") &
                            "', '" & Replace(result.GetDirectoryEntry.Properties("cn").Value, "'", "''") & "', '" & Replace(result.GetDirectoryEntry.Properties("mail").Value, "'", "''") &
                            "', 1 where not exists (select * from #TempUsrSearch where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & "') "
                    End If
                Next

                SubsetSQL += "case when FirstName = '" & Replace(QueryString, "'", "''") & "' then 4 when LastName = '" & Replace(QueryString, "'", "''") &
                    "' then 4 when EmailAddress = '" & Replace(QueryString, "'", "''") & "' then 4 when FirstName like '" & Replace(QueryString, "'", "''") & "%' then 2 " &
                    " when LastName like '" & Replace(QueryString, "'", "''") & "%' then 2  when FirstName like '%" & Replace(QueryString, "'", "''") & "%' then 1 " &
                    " when LastName like '%" & Replace(QueryString, "'", "''") & "%' then 1 when EmailAddress like '%" & Replace(QueryString, "'", "''") & "' then 1 else 0 end + "

            End While

            Dim FurtherSearch As String = SQLSrch & " update u set u.UserLogin = t.UserLogin from DWH.bed.Users u " &
                "join #TempUsrSearch t on (u.EmailAddress = t.UserMail and t.UserMail <> '') " &
                "where u.UserLogin is null " &
                " select isnull(u.UserLogin, t.UserLogin) as UserLogin, " &
                "case when FirstName + ' ' + LastName = t.UserName then t.UserName " &
                "	when FirstName + ' ' + LastName <> t.UserName then FirstName + ' ' + LastName  + ' (' + t.UserName + ')' " &
                "	else isnull(FirstName + ' ' + LastName, t.UserName) end " &
                "	+ ' - ' + isnull(u.EmailAddress, t.UserMail) + isnull('&#9;' + u.JobTitle, '') + isnull(' at ' + convert(varchar, u.Dept), '') as UserDisplay " &
                "	, u.Dept, u.JobTitle, Rnk, Score,  isnull(convert(varchar,u.EmployeeNumber), '-999'+'|' + t.UserLogin) + '|' + isnull(u.EmailAddress, t.UserMail)  as UserID " &
                "from (select *, " & Left(SubsetSQL, Len(SubsetSQL) - 2) & " as Rnk from DWH.Bed.Users where InactivatedDate is null) u " &
                "full join #TempUsrSearch t on u.UserLogin = t.UserLogin  where Rnk > 0 or Score > 0" &
                " order by Rnk desc, Score Desc, 2 "

            'lblAdminUsrResults.Text += Server.HtmlDecode(GetString(FurtherSearch))
            rbl.DataSource = GetData(FurtherSearch)
            rbl.DataTextField = "UserDisplay"
            rbl.DataValueField = "UserLogin"
            rbl.DataBind()
            rbl.SelectedIndex = -1

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Public Function GetNthIndex(s As String, t As Char, n As Integer) As Integer
        Dim count As Integer = 0
        For i As Integer = 0 To s.Length - 1
            If s(i) = t Then
                count += 1
                If count = n Then
                    Return i
                End If
            End If
        Next
        Return -1
    End Function
    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Dim UserFull As String = ""
        If rblNewUserSearchResults.SelectedIndex = -1 Then
            lblExplanationLabel.Text = "No User Selected"
            mpeStandard.Show()
            Exit Sub
        End If
        Dim rblVal As String = rblNewUserSearchResults.SelectedValue
        If Left(rblNewUserSearchResults.SelectedValue, 4) = "-999" Then
            Dim rblTxt As String = rblNewUserSearchResults.SelectedItem.Text

            Dim namestring As String = Left(rblTxt, InStr(rblTxt, " - ") - 1)
            UserFull = "'" & Replace(rblVal.Substring(InStr(rblVal, "|"), GetNthIndex(rblVal, "|", 2) - GetNthIndex(rblVal, "|", 1) - 1), "'", "''") & "'"

            ExecuteSql("Insert into DWH.Bed.Users (UserLogin, FirstName, LastName, EmailAddress) values (" &
                       UserFull &
                ", '" & Replace(Left(rblTxt, InStr(rblTxt, " ") - 1), "'", "''") & "', '" &
                Replace(namestring.Substring(InStrRev(namestring, " ")), "'", "''") &
                "', '" & Replace(rblVal.Substring(GetNthIndex(rblVal, "|", 2) + 1), "'", "''") & "')")
        Else
            UserFull = "'" & Replace(rblNewUserSearchResults.SelectedItem.Value, "'", "''") & "'"
        End If

        If (GetScalar("select isnull(count(*),0) from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and UserLogin = " & UserFull)) > 0 Then
            lblExplanationLabel.Text = "User Already Exists"
            mpeStandard.Show()
            Exit Sub
        End If

        ExecuteSql("insert into DWH.ACCT.Suntrust_User_Access  (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
                   "values (" & UserFull & ", -1, 1, 0, 0, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate())")


        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub

    Protected Sub ddlENSHAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblENSHAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlENSHAcces"), DropDownList)

            Dim permission As String = "0"
            Dim Vision As String = "0"
            Dim Admin As String = "0"
            If ddlAdminServicesRowFacility.SelectedValue = "2" Then
                permission = "1"
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "1" Then
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "3" Then
                Admin = "1"
                permission = "1"
                Vision = "1"
            End If

            Dim NewRow As Integer = 0
            If ddlAdminServicesRowFacility.SelectedValue <> lblAdminServicesRowFacility.Text Then

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " &
            "		from DWH.ACCT.Suntrust_User_Access a " &
            "		join (select 2 Claim_ID union select 3 union select -1) x on 1 = 1 " &
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the All-User row */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the ENSH Row row if it changed */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID = 1 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " &
            " " &
                    "/* Insert new row if user didn't already exist */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 1, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " &
            "		where not exists (select * from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and Claim_ID = 1 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub
    Protected Sub ddlGHSAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblGHSAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlGHSAcces"), DropDownList)

            Dim permission As String = "0"
            Dim Vision As String = "0"
            Dim Admin As String = "0"
            If ddlAdminServicesRowFacility.SelectedValue = "2" Then
                permission = "1"
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "1" Then
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "3" Then
                Admin = "1"
                permission = "1"
                Vision = "1"
            End If

            Dim NewRow As Integer = 0
            If ddlAdminServicesRowFacility.SelectedValue <> lblAdminServicesRowFacility.Text Then

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " &
            "		from DWH.ACCT.Suntrust_User_Access a " &
            "		join (select 1 Claim_ID union select 3 union select -1) x on 1 = 1 " &
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the All-User row */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the ENSH Row row if it changed */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID = 2 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " &
            " " &
                    "/* Insert new row if user didn't already exist */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 2, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " &
            "		where not exists (select * from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and Claim_ID = 2 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub
    Protected Sub ddlNAOSAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblNAOSAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlNAOSAcces"), DropDownList)

            Dim permission As String = "0"
            Dim Vision As String = "0"
            Dim Admin As String = "0"
            If ddlAdminServicesRowFacility.SelectedValue = "2" Then
                permission = "1"
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "1" Then
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "3" Then
                Admin = "1"
                permission = "1"
                Vision = "1"
            End If

            Dim NewRow As Integer = 0
            If ddlAdminServicesRowFacility.SelectedValue <> lblAdminServicesRowFacility.Text Then

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " &
            "		from DWH.ACCT.Suntrust_User_Access a " &
            "		join (select 2 Claim_ID union select 1 union select -1) x on 1 = 1 " &
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the All-User row */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the ENSH Row row if it changed */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID = 3 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " &
            " " &
                    "/* Insert new row if user didn't already exist */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 3, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " &
            "		where not exists (select * from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and Claim_ID = 3 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub
    Protected Sub ddlGECCAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblGECCAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlGECCAcces"), DropDownList)

            Dim permission As String = "0"
            Dim Vision As String = "0"
            Dim Admin As String = "0"
            If ddlAdminServicesRowFacility.SelectedValue = "2" Then
                permission = "1"
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "1" Then
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "3" Then
                Admin = "1"
                permission = "1"
                Vision = "1"
            End If

            Dim NewRow As Integer = 0
            If ddlAdminServicesRowFacility.SelectedValue <> lblAdminServicesRowFacility.Text Then

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " &
            "		from DWH.ACCT.Suntrust_User_Access a " &
            "		join (select 2 Claim_ID union select 3 union select -1) x on 1 = 1 " &
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the All-User row */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the ENSH Row row if it changed */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID = 4 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " &
            " " &
                    "/* Insert new row if user didn't already exist */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 4, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " &
            "		where not exists (select * from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and Claim_ID = 4 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()
    End Sub

    Protected Sub ddlGlancyAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblGlancyAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlGlancyAcces"), DropDownList)

            Dim permission As String = "0"
            Dim Vision As String = "0"
            Dim Admin As String = "0"
            If ddlAdminServicesRowFacility.SelectedValue = "2" Then
                permission = "1"
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "1" Then
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "3" Then
                Admin = "1"
                permission = "1"
                Vision = "1"
            End If

            Dim NewRow As Integer = 0
            If ddlAdminServicesRowFacility.SelectedValue <> lblAdminServicesRowFacility.Text Then

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " &
            "		from DWH.ACCT.Suntrust_User_Access a " &
            "		join (select 2 Claim_ID union select 3 union select -1) x on 1 = 1 " &
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the All-User row */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the ENSH Row row if it changed */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID = 5 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " &
            " " &
                    "/* Insert new row if user didn't already exist */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 5, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " &
            "		where not exists (select * from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and Claim_ID = 5 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()
    End Sub

    Protected Sub ddlStricklandAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblStricklandAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlStricklandAcces"), DropDownList)

            Dim permission As String = "0"
            Dim Vision As String = "0"
            Dim Admin As String = "0"
            If ddlAdminServicesRowFacility.SelectedValue = "2" Then
                permission = "1"
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "1" Then
                Vision = "1"
            ElseIf ddlAdminServicesRowFacility.SelectedValue = "3" Then
                Admin = "1"
                permission = "1"
                Vision = "1"
            End If

            Dim NewRow As Integer = 0
            If ddlAdminServicesRowFacility.SelectedValue <> lblAdminServicesRowFacility.Text Then

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " &
            "		from DWH.ACCT.Suntrust_User_Access a " &
            "		join (select 2 Claim_ID union select 3 union select -1) x on 1 = 1 " &
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the All-User row */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            " " &
                    "/* De-activate the ENSH Row row if it changed */ " &
            "		update DWH.ACCT.Suntrust_User_Access  " &
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " &
            "		where InactivatedDate is null and Claim_ID = 6 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " &
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " &
            " " &
                    "/* Insert new row if user didn't already exist */ " &
            "		insert into DWH.ACCT.Suntrust_User_Access (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " &
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 6, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " &
            "		where not exists (select * from DWH.ACCT.Suntrust_User_Access where InactivatedDate is null and Claim_ID = 6 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()
    End Sub

    Private Sub SynchAccess()

        Dim x As String = "update a " &
            "set a.InactivatedDate = getdate(), a.InactivatedBy = 'Auto-Removed' " &
            "from DWH.ACCT.Suntrust_User_Access a " &
            "where InactivatedDate is null and not exists (select * from DWH.ACCT.Suntrust_User_Access b " &
            "	where a.UserLogin = b.UserLogin and b.InactivatedDate is null and (b.Vision > 0 or b.Permission > 0 or b.AdminRights > 0)) " &
            " " &
        "insert into WebFD.dbo.aspnet_Users " &
    "select '5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', dar.UserLogin), UserLogin, UserLogin, null, 0, getdate() " &
    "from DWH.ACCT.Suntrust_User_Access dar " &
    "where inactivatedDate is null and not exists (select * from WebFD.dbo.aspnet_Users au  " &
    "where au.UserName = dar.UserLogin) " &
    " " &
    "insert into WebFD.dbo.aspnet_UsersInRoles  " &
    "select distinct UserId, '816A3849-E9A1-6236-7BBD-0A5380C0E7FC' from " &
    "DWH.ACCT.Suntrust_User_Access dar " &
    "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " &
    "where inactivatedDate is null and not exists (select * from " &
    "WebFD.dbo.aspnet_UsersInRoles uir  " &
    "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " &
    "where au.UserId = uir.UserId " &
    "and RoleName = 'SuntrustAlloc') " &
    " " &
    "delete uir from WebFD.dbo.aspnet_UsersInRoles uir  " &
    "where not exists ( " &
    "select UserId from " &
    "DWH.ACCT.Suntrust_User_Access dar " &
    "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " &
    "where inactivatedDate is null and uir.UserID = au.UserID) and RoleID = '816A3849-E9A1-6236-7BBD-0A5380C0E7FC'"

        ExecuteSql(x)

    End Sub

    Private Sub gvUserAccess_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvUserAccess.PageIndexChanging
        Try

            Dim se As String = gvUserAccess.SortExpression
            Dim x As DataView = LoadAdminData()
            Dim y As String = AdminMap.Text
            Dim z As String = AdminDir.Text

            Try
                If CInt(AdminDir.Text) = 1 Then
                    x.Sort = y + " " + "asc"
                Else
                    x.Sort = y + " " + "desc"
                End If
            Catch ex As Exception
                x.Sort = se
            End Try

            gvUserAccess.PageIndex = e.NewPageIndex
            gvUserAccess.DataSource = x
            gvUserAccess.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvUserAccess_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvUserAccess.RowCreated
        If HideENSH.Text = "0" Then
            e.Row.Cells(6).CssClass = "hidden"
        End If
        If HideGHS.Text = "0" Then
            e.Row.Cells(7).CssClass = "hidden"
        End If
        If HideNAOS.Text = "0" Then
            e.Row.Cells(8).CssClass = "hidden"
        End If
        If HideGECC.Text = "0" Then
            e.Row.Cells(9).CssClass = "hidden"
        End If
        If HideGlancy.Text = "0" Then
            e.Row.Cells(10).CssClass = "hidden"
        End If
        If HideStrickland.Text = "0" Then
            e.Row.Cells(11).CssClass = "hidden"
        End If
    End Sub

    Private Sub gvUserAccess_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUserAccess.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlENSHAcces As DropDownList = e.Row.FindControl("ddlENSHAcces")
            Dim ddlGHSAcces As DropDownList = e.Row.FindControl("ddlGHSAcces")
            Dim ddlNAOSAcces As DropDownList = e.Row.FindControl("ddlNAOSAcces")
            Dim ddlGECCAcces As DropDownList = e.Row.FindControl("ddlGECCAcces")
            Dim ddlGlancyAcces As DropDownList = e.Row.FindControl("ddlGlancyAcces")
            Dim ddlStricklandAcces As DropDownList = e.Row.FindControl("ddlStricklandAcces")
            Dim x As New DataView
            Dim x2 As New DataView
            Dim chk As Integer = GetScalar("select isnull(count(*), 0) from WebFD.dbo.aspnet_Users u " &
            "		join WebFD.dbo.vw_aspnet_UsersInRoles uir on u.UserId = uir.UserId " &
            "		join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'")

            Dim s As String = " select 'Update' as ValueDesc, 2 as Value " &
            " union select 'View' as ValueDesc, 1 as Value " &
            " union select 'None' as ValueDesc, 0 as Value " &
            " union select 'Admin' as ValueDesc, 3 as Value  " &
            " where exists " &
            " (select u.UserName from WebFD.dbo.aspnet_Users u " &
            "		join WebFD.dbo.vw_aspnet_UsersInRoles uir on u.UserId = uir.UserId " &
            "		join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "')  " &
            "		order by Value "

            Dim s2 As String = " select 'Update' as ValueDesc, 2 as Value " &
            " union select 'View' as ValueDesc, 1 as Value " &
            " union select 'None' as ValueDesc, 0 as Value " &
            " union select 'Admin' as ValueDesc, 3 as Value  " &
            "		order by Value "

            x = GetData(s).DefaultView
            If chk = 0 Then
                x2 = GetData(s2).DefaultView
            End If


            If e.Row.DataItem("ENSH").ToString.ToUpper = "3" And chk = 0 Then
                ddlENSHAcces.DataSource = x2
            Else
                ddlENSHAcces.DataSource = x
            End If

            ddlENSHAcces.DataTextField = "ValueDesc"
            ddlENSHAcces.DataValueField = "Value"
            ddlENSHAcces.DataBind()

            ddlENSHAcces.SelectedValue = e.Row.DataItem("ENSH").ToString.ToUpper

            If e.Row.DataItem("ENSH").ToString.ToUpper = "3" And chk = 0 Then
                ddlENSHAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateENSH").ToString.ToUpper = "1" Then
                ddlENSHAcces.Enabled = True
            Else
                ddlENSHAcces.Enabled = False
            End If

            If e.Row.DataItem("GHS").ToString.ToUpper = "3" And chk = 0 Then
                ddlGHSAcces.DataSource = x2
            Else
                ddlGHSAcces.DataSource = x
            End If
            ddlGHSAcces.DataTextField = "ValueDesc"
            ddlGHSAcces.DataValueField = "Value"
            ddlGHSAcces.DataBind()

            ddlGHSAcces.SelectedValue = e.Row.DataItem("GHS").ToString.ToUpper
            If e.Row.DataItem("GHS").ToString.ToUpper = "3" And chk = 0 Then
                ddlGHSAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateGHS").ToString.ToUpper = "1" Then
                ddlGHSAcces.Enabled = True
            Else
                ddlGHSAcces.Enabled = False
            End If


            If e.Row.DataItem("NAOS").ToString.ToUpper = "3" And chk = 0 Then
                ddlNAOSAcces.DataSource = x2
            Else
                ddlNAOSAcces.DataSource = x
            End If
            ddlNAOSAcces.DataTextField = "ValueDesc"
            ddlNAOSAcces.DataValueField = "Value"
            ddlNAOSAcces.DataBind()

            ddlNAOSAcces.SelectedValue = e.Row.DataItem("NAOS").ToString.ToUpper
            If e.Row.DataItem("NAOS").ToString.ToUpper = "3" And chk = 0 Then
                ddlNAOSAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateNAOS").ToString.ToUpper = "1" Then
                ddlNAOSAcces.Enabled = True
            Else
                ddlNAOSAcces.Enabled = False
            End If


            If e.Row.DataItem("GECC").ToString.ToUpper = "3" And chk = 0 Then
                ddlGECCAcces.DataSource = x2
            Else
                ddlGECCAcces.DataSource = x
            End If
            ddlGECCAcces.DataTextField = "ValueDesc"
            ddlGECCAcces.DataValueField = "Value"
            ddlGECCAcces.DataBind()

            ddlGECCAcces.SelectedValue = e.Row.DataItem("GECC").ToString.ToUpper
            If e.Row.DataItem("GECC").ToString.ToUpper = "3" And chk = 0 Then
                ddlGECCAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateGECC").ToString.ToUpper = "1" Then
                ddlGECCAcces.Enabled = True
            Else
                ddlGECCAcces.Enabled = False
            End If

            If e.Row.DataItem("Glancy").ToString.ToUpper = "3" And chk = 0 Then
                ddlGlancyAcces.DataSource = x2
            Else
                ddlGlancyAcces.DataSource = x
            End If
            ddlGlancyAcces.DataTextField = "ValueDesc"
            ddlGlancyAcces.DataValueField = "Value"
            ddlGlancyAcces.DataBind()

            ddlGlancyAcces.SelectedValue = e.Row.DataItem("Glancy").ToString.ToUpper
            If e.Row.DataItem("Glancy").ToString.ToUpper = "3" And chk = 0 Then
                ddlGlancyAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateGlancy").ToString.ToUpper = "1" Then
                ddlGlancyAcces.Enabled = True
            Else
                ddlGlancyAcces.Enabled = False
            End If

            If e.Row.DataItem("Strickland").ToString.ToUpper = "3" And chk = 0 Then
                ddlStricklandAcces.DataSource = x2
            Else
                ddlStricklandAcces.DataSource = x
            End If
            ddlStricklandAcces.DataTextField = "ValueDesc"
            ddlStricklandAcces.DataValueField = "Value"
            ddlStricklandAcces.DataBind()

            ddlStricklandAcces.SelectedValue = e.Row.DataItem("Strickland").ToString.ToUpper
            If e.Row.DataItem("Strickland").ToString.ToUpper = "3" And chk = 0 Then
                ddlStricklandAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateStrickland").ToString.ToUpper = "1" Then
                ddlStricklandAcces.Enabled = True
            Else
                ddlStricklandAcces.Enabled = False
            End If


        End If
    End Sub

    Private Sub gvUserAccess_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvUserAccess.Sorting
        Try

            Dim dv As DataView
            Dim sorts As String
            dv = LoadAdminData()

            sorts = e.SortExpression

            If e.SortExpression = AdminMap.Text Then

                If CInt(AdminDir.Text) = 1 Then
                    dv.Sort = sorts + " " + "desc"
                    AdminDir.Text = 0
                Else
                    dv.Sort = sorts + " " + "asc"
                    AdminDir.Text = 1
                End If

            Else
                dv.Sort = sorts + " " + "asc"
                AdminDir.Text = 1
                AdminMap.Text = e.SortExpression
            End If

            gvUserAccess.DataSource = dv
            gvUserAccess.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub PopulateDDLTranDesc()

        cblTranDescs.DataSource = GetData("select distinct TranDesc from DWH.SUNTRUST.SUNTRUSTAllActivity where AcctNo = '1400018279'")
        cblTranDescs.DataValueField = "TranDesc"
        cblTranDescs.DataTextField = "TranDesc"
        cblTranDescs.DataBind()

        For i As Integer = 0 To cblTranDescs.Items.Count - 1
            cblTranDescs.Items(i).Selected = True
        Next

    End Sub

    Private Sub CountTranDescs()

        Dim x As Integer = 0
        Dim y As Integer = 0
        For i As Integer = 0 To cblTranDescs.Items.Count - 1
            x += 1
            If cblTranDescs.Items(i).Selected = True Then
                y += 1
            End If

        Next
        If x = y Then
            txtTranDescs.Text = "(All Tran Descs Included)"
        ElseIf y = 0 Then
            txtTranDescs.Text = "(No Tran Descs Selected)"
        Else
            txtTranDescs.Text = "(" & (x - y).ToString & " Tran Descs Excluded)"
        End If

    End Sub
    Private Sub cblTranDescs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblTranDescs.SelectedIndexChanged

        CountTranDescs()

    End Sub

    Private Sub cbAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbAll.CheckedChanged
        CountTranDescs()
    End Sub

    Private Sub PopulateDDLBaiType()

        cblBaiType.DataSource = GetData("select distinct BAITypeCode from DWH.SUNTRUST.SUNTRUSTAllActivity order by BAITypeCode asc")
        cblBaiType.DataValueField = "BAITypeCode"
        cblBaiType.DataTextField = "BAITypeCode"
        cblBaiType.DataBind()

        For i As Integer = 0 To cblBaiType.Items.Count - 1
            cblBaiType.Items(i).Selected = True
        Next

    End Sub

    Private Sub CountBAITypes()

        Dim x As Integer = 0
        Dim y As Integer = 0
        For i As Integer = 0 To cblBaiType.Items.Count - 1
            x += 1
            If cblBaiType.Items(i).Selected = True Then
                y += 1
            End If

        Next
        If x = y Then
            txtBaiType.Text = "(All BAI Types Included)"
        ElseIf y = 0 Then
            txtBaiType.Text = "(No BAI Types Selected)"
        Else
            txtBaiType.Text = "(" & (x - y).ToString & " BAI Types Excluded)"
        End If

    End Sub
    Private Sub cblBaiType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblBaiType.SelectedIndexChanged

        CountBAITypes()

    End Sub

    Private Sub cbBAIAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbBaiAll.CheckedChanged
        CountBAITypes()
    End Sub

    Protected Sub Popup_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        lblExplanationLabel.Text = Server.HtmlDecode(btn.CommandArgument.ToString)
        mpeStandard.Show()
    End Sub

End Class