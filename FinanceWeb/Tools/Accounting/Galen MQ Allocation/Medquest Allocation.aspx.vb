Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class Medquest_Allocation
    Inherits System.Web.UI.Page

    Private Sub Medquest_Allocation_Init(sender As Object, e As EventArgs) Handles Me.Init

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

                Dim ddate As Date = GetDate("select max(Calendar_Date) from DWH.WF.WFALLACTIVITY wf join DWH.dbo.DimDate dd on wf.AsOfDate = dd.Date_ID ")
                txtStartRange.Text = ddate
                txtEndRange.Text = ddate
                PopulateDDLTranDesc()
                PopulateMainData()


            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub CheckPermissions()
        Try
            If GetScalar("select count(*) from ( " & _
                    "select Claim_ID, [Order] " & _
                    "from DWH.ACCT.Galen_Medquest_Claim_Order_LU  " & _
                    "where getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') and InactivatedDate is null " & _
                    "union  " & _
                    "select -1, 4) clu " & _
                    "join dwh.ACCT.Galen_Medquest_User_Access_2 ua on clu.Claim_ID = isnull(ua.Claim_ID, clu.Claim_ID) and ua.InactivatedDate is null " & _
                    "and AdminRights = 1 and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "'") > 0 Then
                tpUserAdmin.Visible = True

                gvUserAccess.DataSource = LoadAdminData()
                gvUserAccess.DataBind()

            Else
                tpUserAdmin.Visible = False
            End If

            If GetScalar("select count(*) from ( " & _
                    "select Claim_ID, [Order] " & _
                    "from DWH.ACCT.Galen_Medquest_Claim_Order_LU  " & _
                    "where getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') and InactivatedDate is null " & _
                    "union  " & _
                    "select -1, 4) clu " & _
                    "join dwh.ACCT.Galen_Medquest_User_Access_2 ua on clu.Claim_ID = isnull(ua.Claim_ID, clu.Claim_ID) and ua.InactivatedDate is null " & _
                    "and UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "'") > 0 Then
                tpMQAllocation.Visible = True
            Else
                tpMQAllocation.Visible = False
            End If

            HideGalen.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Galen_Medquest_User_Access_2 where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    "' and InactivatedDate is null and isnull(Claim_ID, 1) = 1 ")

            HideMQ.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Galen_Medquest_User_Access_2 where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    "' and InactivatedDate is null and isnull(Claim_ID, 2) = 2 ")

            HideSTAR.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Galen_Medquest_User_Access_2 where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    "' and InactivatedDate is null and isnull(Claim_ID, 3) = 3 ")

            HideOther.Text = GetScalar("select isnull(Vision, 0) from DWH.ACCT.Galen_Medquest_User_Access_2 where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    "' and InactivatedDate is null and isnull(Claim_ID, -1) = -1 ")


            '        HideGalen.Text = GetScalar("select isnull(GalenVision, 0) from DWH.ACCT.Galen_Medquest_User_Access where UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
            '"' and InactivatedDate is null ")
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

        Dim x As String = "select *        , ConComments.Comments as Comments " & _
        ", case when len(ConComments.Comments) > 40 then 'True' else 'False' end as CommentFlag  " & _
        ", case when len(ConComments.Comments) > 40 then left(replace(ConComments.Comments, '&lt;br&gt;&lt;br&gt;', '; '), 40) else replace(ConComments.Comments, '&lt;br&gt;&lt;br&gt;', ';') end as CommentsMini  " & _
        " from (select convert(varchar, UniqueActivityID) as UniqueActivityID, dd.Calendar_Date as AsOfDate, BAITypeCode, Trandesc, wf.Description, NetAmount, CustomerRefNo, BankReference " & _
        ", sum(case when x.Claim_ID = 1 then gma.Claimed else null end) as GalenClaimed " & _
        ", max(case when x.Claim_ID = 1 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GalenVision		 " & _
        ", max(case when x.Claim_ID = 1 then ua.Permission else 0 end) as GalenPermission " & _
        ", sum(case when x.Claim_ID = 2 then gma.Claimed else null end) as MedQuestClaimed " & _
        ", max(case when x.Claim_ID = 2 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as MedQuestVision		 " & _
        ", max(case when x.Claim_ID = 2 then ua.Permission else 0 end) as MedquestPermission " & _
        ", sum(case when x.Claim_ID = 3 then gma.Claimed else null end) as STARClaimed " & _
        ", max(case when x.Claim_ID = 3 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as STARVision		 " & _
        ", max(case when x.Claim_ID = 3 then ua.Permission else 0 end) as STARPermission " & _
        ", sum(case when x.Claim_ID = -1 then gma.Claimed else null end) as OtherClaimed " & _
        ", max(case when x.Claim_ID = -1 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as OtherVision		 " & _
        ", max(case when x.Claim_ID = -1 then ua.Permission else 0 end) as OtherPermission        " & _
        ", NetAmount - sum(isnull(gma.Claimed, 0)) as Unclaimed  " & _
        "from  " & _
        "DWH.WF.WFAllActivity wf  " & _
        "join DWH.dbo.DimDate dd on wf.AsOfDate = dd.Date_ID   " & _
        "left join ( " & _
        "select distinct cl.Claim_ID, cl.Description as OrderDescription, [Order], Dense_Rank() over (partition by cl.Claim_ID order by clu.EffectiveFrom desc) RN_Ord   " & _
        "from DWH.ACCT.Galen_Medquest_Claim_LU cl " & _
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " & _
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " & _
        "join DWH.ACCT.Galen_Medquest_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " & _
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " & _
        "union select -1, 'Other', 999, 1 ) x on RN_Ord = 1  " & _
        "left join ( " & _
        "select distinct cl.Claim_ID,  isnull([Order], 999) as [Order], Dense_Rank() over (partition by cl.Claim_ID order by cl.EffectiveFrom desc) RN_Ord   " & _
        "from DWH.ACCT.Galen_Medquest_Claim_LU cl " & _
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " & _
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " & _
        "left join DWH.ACCT.Galen_Medquest_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " & _
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " & _
         ") x2 on x2.RN_Ord = 1 and x.[Order] = x2.[Order]  " & _
        "left join DWH.ACCT.Galen_Medquest_Allocation_2 gma on wf.UniqueActivityID = gma.WFUniqueActivityID and gma.InactivatedDate is null and x2.Claim_ID = gma.Claim_ID	 " & _
        " " & _
                "left join DWH.ACCT.Galen_Medquest_User_Access_2 ua on ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "' and ua.InactivatedDate is null and isnull(ua.Claim_ID, x.Claim_ID) = x.Claim_ID " & _
                        "where AcctNo = '4129405627'  " & _
                "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' and isnull(TranDesc, 'null') in (" & IncludedTranDescs & " 'null') "

        'Dim x As String = "select convert(varchar, UniqueActivityID) as UniqueActivityID, dd.Calendar_Date as AsOfDate, BAITypeCode, Trandesc, Description, NetAmount, CustomerRefNo, BankReference  " & _
        '", gma.GalenClaimed, case when ua.GalenPermission = 1 then 0 else ua.GalenVision end as GalenVision, ua.GalenPermission " & _
        '", gma.MQClaimed, case when ua.MQPermission = 1 then 0 else ua.MQVision end as MQVision, ua.MQPermission " & _
        '", NetAmount - isnull(gma.galenClaimed, 0) - isnull(gma.MQClaimed, 0) as Unclaimed " & _
        '", case when GalenVision = 1 and MQVision = 1 then 1 else 0 end as Vision " & _
        '"from DWH.WF.WFALLACTIVITY wf " & _
        '"join DWH.dbo.DimDate dd on wf.AsOfDate = dd.Date_ID  " & _
        '"left join DWH.ACCT.Galen_Medquest_Allocation gma on wf.UniqueActivityID = gma.WFUniqueActivityID and gma.InactivatedDate is null " & _
        '"join DWH.ACCT.Galen_Medquest_User_Access ua on ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and ua.InactivatedDate is null " & _
        '"where AcctNo = '4129405627' " & _
        '"and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' "

        If Trim(txtSearchValue.Text) = "" Then
        Else
            Dim moneymoney As Decimal

            If Decimal.TryParse(Replace(Trim(txtSearchValue.Text), "'", "''"), moneymoney) Then
                Dim y As String = Replace(Trim(RememberedText.Text), "'", "''")
                x += "and (TranDesc like '%" & y & "%' or Description like '%" & y & "%' or CustomerRefNo like '%" & y & "%' or BankReference like '%" & y & "%' or NetAmount like '%" & moneymoney & "%')"
            Else
                Dim y As String = Replace(Trim(RememberedText.Text), "'", "''")
                x += "and (TranDesc like '%" & y & "%' or Description like '%" & y & "%' or CustomerRefNo like '%" & y & "%' or BankReference like '%" & y & "%')"
            End If



        End If



        x += "group by convert(varchar, UniqueActivityID) , dd.Calendar_Date , BAITypeCode, Trandesc, wf.Description, NetAmount, CustomerRefNo, BankReference ) x " & _
        "	left join (	Select distinct ST2.UniqueActivityID, " & _
        "    substring( " & _
        "        ( " & _
        "					Select ST1.AddedBy+': '+ST1.Comment + ' ('+ convert(varchar, AddedDate, 101) + ') <br><br>' AS [text()] " & _
        "					From DWH.ACCT.Galen_Medquest_Allocation_Comments ST1 " & _
        "					Where ST1.InactivatedDate is null and ST1.WFUniqueActivityID = ST2.UniqueActivityID " & _
        "					ORDER BY ST1.WFUniqueActivityID " & _
        "					For XML PATH ('') " & _
        "				), 1, 1000) [Comments] " & _
        "		From DWH.WF.WFAllActivity ST2  with (nolock) join DWH.dbo.DimDate dd on ST2.AsOfDate = dd.Date_ID  " & _
        "           where AcctNo = '4129405627'  " & _
                "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' ) ConComments on x.UniqueActivityID = ConComments.UniqueActivityID "

        If RememberedCheck.Text = "False" Then
            x += " where   ( GalenVision + GalenPermission + MedQuestVision + MedQuestPermission + STARVision + STARPermission + OtherVision + OtherPermission = 4 and Unclaimed <> 0)  " & _
             "or (GalenVision + GalenPermission + MedQuestVision + MedQuestPermission + STARVision + STARPermission + OtherVision + OtherPermission < 4 " & _
            "	and (GalenPermission = 0 or GalenClaimed is null) " & _
            "	and (MedquestPermission = 0 or MedquestClaimed is null) " & _
            "	and (STARPermission = 0 or STARClaimed is null) " & _
            "	and (OtherPermission = 0 or OtherClaimed is null)) "

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


        Dim x As String = "select AsOfDate, sum(NetAmount) as NetAmount " & _
    ", sum(GalenClaimed) as GalenClaimed, min(GalenVision) + min(GalenPermission) as GalenVision " & _
    ", sum(MedQuestClaimed) as MedquestClaimed, min(MedquestVision) +  min(MedquestPermission) as MedquestVision " & _
    ", sum(STARClaimed) as STARClaimed, min(STARVision) + min(STARPermission) as STARVision " & _
    ", sum(OtherClaimed) as OtherClaimed, min(OtherVision) + min(OtherPermission) as OtherVision , sum(Unclaimed) as Unclaimed" & _
" " & _
"from ( " & _
   "select convert(varchar, UniqueActivityID) as UniqueActivityID, dd.Calendar_Date as AsOfDate, BAITypeCode, Trandesc, wf.Description, NetAmount, CustomerRefNo, BankReference " & _
        ", sum(case when x.Claim_ID = 1 then gma.Claimed else null end) as GalenClaimed " & _
        ", max(case when x.Claim_ID = 1 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as GalenVision		 " & _
        ", max(case when x.Claim_ID = 1 then ua.Permission else 0 end) as GalenPermission " & _
        ", sum(case when x.Claim_ID = 2 then gma.Claimed else null end) as MedQuestClaimed " & _
        ", max(case when x.Claim_ID = 2 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as MedQuestVision		 " & _
        ", max(case when x.Claim_ID = 2 then ua.Permission else 0 end) as MedquestPermission " & _
        ", sum(case when x.Claim_ID = 3 then gma.Claimed else null end) as STARClaimed " & _
        ", max(case when x.Claim_ID = 3 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as STARVision		 " & _
        ", max(case when x.Claim_ID = 3 then ua.Permission else 0 end) as STARPermission " & _
        ", sum(case when x.Claim_ID = -1 then gma.Claimed else null end) as OtherClaimed " & _
        ", max(case when x.Claim_ID = -1 then case when ua.Permission = 1 then 0 else ua.Vision end else 0 end) as OtherVision		 " & _
        ", max(case when x.Claim_ID = -1 then ua.Permission else 0 end) as OtherPermission        " & _
        ", NetAmount - sum(isnull(gma.Claimed, 0)) as Unclaimed  " & _
        "from  " & _
        "DWH.WF.WFAllActivity wf  " & _
        "join DWH.dbo.DimDate dd on wf.AsOfDate = dd.Date_ID   " & _
        "left join ( " & _
        "select distinct cl.Claim_ID, cl.Description as OrderDescription, [Order], Dense_Rank() over (partition by cl.Claim_ID order by clu.EffectiveFrom desc) RN_Ord   " & _
        "from DWH.ACCT.Galen_Medquest_Claim_LU cl " & _
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " & _
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " & _
        "join DWH.ACCT.Galen_Medquest_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " & _
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " & _
        "union select -1, 'Other', 999, 1 ) x on RN_Ord = 1  " & _
        "left join ( " & _
        "select distinct cl.Claim_ID,  isnull([Order], 999) as [Order], Dense_Rank() over (partition by cl.Claim_ID order by cl.EffectiveFrom desc) RN_Ord   " & _
        "from DWH.ACCT.Galen_Medquest_Claim_LU cl " & _
        "join DWH.dbo.DimDate dd on dd.Calendar_Date between isnull(cl.EffectiveFrom, '1/1/1800') and isnull(cl.EffectiveTo, '12/31/9999') " & _
            "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "'   " & _
        "left join DWH.ACCT.Galen_Medquest_Claim_Order_LU clu on cl.Claim_ID = clu.Claim_ID and " & _
            "dd.Calendar_Date between isnull(clu.EffectiveFrom, '1/1/1800') and isnull(clu.EffectiveTo, '12/31/9999') " & _
         ") x2 on x2.RN_Ord = 1 and x.[Order] = x2.[Order]  " & _
        "left join DWH.ACCT.Galen_Medquest_Allocation_2 gma on wf.UniqueActivityID = gma.WFUniqueActivityID and gma.InactivatedDate is null and x2.Claim_ID = gma.Claim_ID	 " & _
        " " & _
                "left join DWH.ACCT.Galen_Medquest_User_Access_2 ua on ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "' and ua.InactivatedDate is null and isnull(ua.Claim_ID, x.Claim_ID) = x.Claim_ID " & _
                        "where AcctNo = '4129405627'  " & _
                "and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' and isnull(TranDesc, 'null') in (" & IncludedTranDescs & " 'null')" & _
        "group by convert(varchar, UniqueActivityID) , dd.Calendar_Date , BAITypeCode, Trandesc, wf.Description, NetAmount, CustomerRefNo, BankReference " & _
    ") x " & _
    "group by AsOfDate order by AsOfDate "

        'Dim x As String = "select dd.Calendar_Date as AsOfDate, sum(NetAmount) as NetAmount " & _
        '", sum(gma.GalenClaimed) as GalenClaimed, min(ua.GalenVision) as GalenVision" & _
        '", sum(gma.MQClaimed) as MQClaimed, min(ua.MQVision) as MQVision " & _
        '", sum(NetAmount - isnull(gma.galenClaimed, 0) - isnull(gma.MQClaimed, 0)) as Unclaimed " & _
        '", min(case when GalenVision = 1 and MQVision = 1 then 1 else 0 end) as Vision " & _
        '"from DWH.WF.WFALLACTIVITY wf " & _
        '"join DWH.dbo.DimDate dd on wf.AsOfDate = dd.Date_ID  " & _
        '"left join DWH.ACCT.Galen_Medquest_Allocation gma on wf.UniqueActivityID = gma.WFUniqueActivityID and gma.InactivatedDate is null " & _
        '"join DWH.ACCT.Galen_Medquest_User_Access ua on ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and ua.InactivatedDate is null " & _
        '"where AcctNo = '4129405627' " & _
        '"and dd.Calendar_Date between '" & EffFrom & "' and '" & EffTo & "' "

        ''If Trim(txtSearchValue.Text) = "" Then
        ''Else
        ''    Dim y As String = Replace(Trim(RememberedText.Text), "'", "''")
        ''    x += "and (TranDesc like '%" & y & "%' or Description like '%" & y & "%' or CustomerRefNo like '%" & y & "%' or BankReference like '%" & y & "%')"
        ''End If

        'x += "group by dd.Calendar_Date order by dd.Calendar_Date"

        Return GetData(x).DefaultView

    End Function


    Private Function LoadAdminData()

        SynchAccess()

        Dim x As String = "				select u.UserLogin, u2.EmployeeNumber, u2.FirstName, u2.LastName, u2.EmailAddress , u2.Dept " & _
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 1) = 1 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 1) = 1 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 1) = 1 and u.Vision = 1 then 1 else 0 end) as Galen " & _
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 2) = 2 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 2) = 2 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 2) = 2 and u.Vision = 1 then 1 else 0 end) as MQ " & _
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, 3) = 3 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, 3) = 3 and u.Permission = 1 then 2 when isnull(u.Claim_ID, 3) = 3 and u.Vision = 1 then 1 else 0 end) as STAR " & _
"				, max(case when ua.AdminRights = 1 and isnull(u.Claim_ID, -1) = -1 and u.AdminRights = 1 then 3 when isnull(u.Claim_ID, -1) = -1 and u.Permission = 1 then 2 when isnull(u.Claim_ID, -1) = -1 and u.Vision = 1 then 1 else 0 end) as Other " & _
" " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 1) = 1 and ua.AdminRights = 1 then 1 else 0 end) as UpdateGalen " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 2) = 2 and ua.AdminRights = 1 then 1 else 0 end) as UpdateMQ " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 3) = 3 and ua.AdminRights = 1 then 1 else 0 end) as UpdateSTAR " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, -1) = -1 and ua.AdminRights = 1 then 1 else 0 end) as UpdateOther " & _
" " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 1) = 1 and ua.Vision = 1 then 1 else 0 end) as ViewGalen " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 2) = 2 and ua.Vision = 1 then 1 else 0 end) as ViewMQ " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, 3) = 3 and ua.Vision = 1 then 1 else 0 end) as ViewSTAR " & _
"				, max(case when x.UserName is not null then 1 when isnull(ua.Claim_ID, -1) = -1 and ua.Vision = 1 then 1 else 0 end) as ViewOther " & _
" " & _
"				from		DWH.ACCT.Galen_Medquest_User_Access_2 u " & _
"				left join DWH.ACCT.Galen_Medquest_User_Access_2 ua on isnull(isnull(u.Claim_ID, ua.Claim_ID), 999) = isnull(isnull(ua.Claim_ID, u.Claim_ID), 999) and ua.UserLogin = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "' and ua.InactivatedDate is null " & _
"				left join (select u.UserName from WebFD.dbo.aspnet_Users u " & _
"		join WebFD.dbo.vw_aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
"		join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
                 "') x on 1=1 " & _
"				left join DWH.bed.Users u2 on u.UserLogin = u2.UserLogin " & _
                "where u.InactivatedDate is null " & _
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
            If HideGalen.Text = "0" Then
                e.Row.Cells(7).CssClass = "hidden"
                e.Row.Cells(11).CssClass = "hidden"
            End If
            If HideMQ.Text = "0" Then
                e.Row.Cells(8).CssClass = "hidden"
                e.Row.Cells(11).CssClass = "hidden"
            End If
            If HideSTAR.Text = "0" Then
                e.Row.Cells(9).CssClass = "hidden"
                e.Row.Cells(11).CssClass = "hidden"
            End If
            If HideOther.Text = "0" Then
                e.Row.Cells(10).CssClass = "hidden"
                e.Row.Cells(11).CssClass = "hidden"
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

                    Dim GalenAlloc, MQAlloc, OldGalen, OldMQ, OldSTAR, STARAlloc, OtherAlloc, OldOther As String
                    Dim Did As String

                    GalenAlloc = ""
                    MQAlloc = ""
                    OldGalen = ""
                    OldMQ = ""
                    OldSTAR = ""
                    OldOther = ""
                    STARAlloc = ""
                    OtherAlloc = ""

                    Did = gvSearch.DataKeys(boat.RowIndex).Value

                    OldGalen = boat.Cells(7).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    OldMQ = boat.Cells(8).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    GalenAlloc = boat.Cells(7).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    MQAlloc = boat.Cells(8).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString

                    STARAlloc = boat.Cells(9).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    OldSTAR = boat.Cells(9).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    OtherAlloc = boat.Cells(10).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    OldOther = boat.Cells(10).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    Dim commentsubmission = boat.Cells(12).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString

                    If Trim(commentsubmission) <> "" Then
                        UpdateSQL += " Insert into DWH.Acct.Galen_Medquest_Allocation_Comments (WFUniqueActivityID, Comment, AddedBy, AddedDate) " & _
                            "select convert(varbinary, '" & Did & "'), '" & Replace(commentsubmission, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() "

                    End If

                    If HideMQ.Text = "1" Then

                        If MQAlloc <> OldMQ Then
                            If IsNumeric(MQAlloc) = False And MQAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & MQAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation_2 " & _
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            "where WFUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 2 and InactivatedDate is null and (isnull(Claimed, '') <> '" & MQAlloc & "' ) " & _
                            "insert into DWH.ACCT.Galen_Medquest_Allocation_2 (WFUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " & _
                            "select convert(varbinary, '" & Did & "')" & _
                            ", 2 " & _
                            ", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                            ", case when '" & MQAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            "where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation_2 b " & _
                            "where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 2) "


                            'UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation " & _
                            '"set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            '"where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is null and (isnull(MQClaimed, '') <> '" & MQAlloc & "' ) " & _
                            '"insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                            '"select top 1 a.WFUniqueActivityID " & _
                            '", a.GalenClaimed, a.GalenClaimedBy " & _
                            '", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                            '", case when '" & MQAlloc & "' = '' and a.MQClaimed is null then null  " & _
                            '    "when '" & MQAlloc & "' = a.MQClaimed then a.MQClaimedBy else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end	  " & _
                            '", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            '"from DWH.ACCT.Galen_Medquest_Allocation a " & _
                            '"where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is not null " & _
                            '"and not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                            '"where a.WFUniqueActivityID = b.WFUniqueActivityID and b.InactivatedDate is null) " & _
                            '"order by InactivatedDate desc " & _
                            '"insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                            '"select convert(varbinary, '" & Did & "')" & _
                            '", null, null " & _
                            '", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                            '", case when '" & MQAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            '", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            '"where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                            '"where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null) "
                            cnt += 1
                        End If
                    End If

                    If HideGalen.Text = "1" Then

                        If OldGalen <> GalenAlloc Then
                            If IsNumeric(GalenAlloc) = False And GalenAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & GalenAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If

                            UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation_2 " & _
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            "where WFUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 1 and InactivatedDate is null and (isnull(Claimed, '') <> '" & GalenAlloc & "' ) " & _
                            "insert into DWH.ACCT.Galen_Medquest_Allocation_2 (WFUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " & _
                            "select convert(varbinary, '" & Did & "')" & _
                            ", 1 " & _
                            ", case when '" & GalenAlloc & "' = '' then null else '" & GalenAlloc & "' end " & _
                            ", case when '" & GalenAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            "where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation_2 b " & _
                            "where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 1) "



                            'UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation " & _
                            '"set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            '"where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is null and ( isnull(GalenClaimed, '') <> '" & GalenAlloc & "') " & _
                            '"insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                            '"select top 1 a.WFUniqueActivityID " & _
                            '", case when '" & GalenAlloc & "' = '' then null else '" & GalenAlloc & "' end " & _
                            '", case when '" & GalenAlloc & "' = '' and a.GalenClaimed is null then null  " & _
                            '    "when '" & GalenAlloc & "' = a.GalenClaimed then a.GalenClaimedBy else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            '", a.MQClaimed, a.MQClaimedBy, '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            '"from DWH.ACCT.Galen_Medquest_Allocation a " & _
                            '"where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is not null " & _
                            '"and not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                            '"where a.WFUniqueActivityID = b.WFUniqueActivityID and b.InactivatedDate is null) " & _
                            '"order by InactivatedDate desc " & _
                            '"insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                            '"select convert(varbinary, '" & Did & "')" & _
                            '", case when '" & GalenAlloc & "' = '' then null else '" & GalenAlloc & "' end " & _
                            '", case when '" & GalenAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            '", null, null " & _
                            '", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            '"where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                            '"where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null) "
                            cnt += 1
                        End If
                    End If

                    If HideSTAR.Text = "1" Then

                        If STARAlloc <> OldSTAR Then
                            If IsNumeric(STARAlloc) = False And STARAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & STARAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation_2 " & _
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            "where WFUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 3 and InactivatedDate is null and (isnull(Claimed, '') <> '" & STARAlloc & "' ) " & _
                            "insert into DWH.ACCT.Galen_Medquest_Allocation_2 (WFUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " & _
                            "select convert(varbinary, '" & Did & "')" & _
                            ", 3 " & _
                            ", case when '" & STARAlloc & "' = '' then null else '" & STARAlloc & "' end " & _
                            ", case when '" & STARAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            "where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation_2 b " & _
                            "where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 3) "


                            cnt += 1
                        End If
                    End If


                    If HideOther.Text = "1" Then

                        If OtherAlloc <> OldOther Then
                            If IsNumeric(OtherAlloc) = False And OtherAlloc <> "" Then
                                lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & OtherAlloc & ")"
                                lblExplanationLabel.DataBind()
                                mpeStandard.Show()
                                Exit Sub
                            End If


                            UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation_2 " & _
                            "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            "where WFUniqueActivityID = convert(varbinary, '" & Did & "') and Claim_ID = 4 and InactivatedDate is null and (isnull(Claimed, '') <> '" & OtherAlloc & "' ) " & _
                            "insert into DWH.ACCT.Galen_Medquest_Allocation_2 (WFUniqueActivityID, Claim_ID, Claimed, ClaimedBy, AddedBy, AddedDate) " & _
                            "select convert(varbinary, '" & Did & "')" & _
                            ", 4 " & _
                            ", case when '" & OtherAlloc & "' = '' then null else '" & OtherAlloc & "' end " & _
                            ", case when '" & OtherAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            "where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation_2 b " & _
                            "where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null and b.Claim_ID = 4) "


                            'UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation " & _
                            '"set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                            '"where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is null and (isnull(MQClaimed, '') <> '" & MQAlloc & "' ) " & _
                            '"insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                            '"select top 1 a.WFUniqueActivityID " & _
                            '", a.GalenClaimed, a.GalenClaimedBy " & _
                            '", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                            '", case when '" & MQAlloc & "' = '' and a.MQClaimed is null then null  " & _
                            '    "when '" & MQAlloc & "' = a.MQClaimed then a.MQClaimedBy else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end	  " & _
                            '", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            '"from DWH.ACCT.Galen_Medquest_Allocation a " & _
                            '"where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is not null " & _
                            '"and not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                            '"where a.WFUniqueActivityID = b.WFUniqueActivityID and b.InactivatedDate is null) " & _
                            '"order by InactivatedDate desc " & _
                            '"insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                            '"select convert(varbinary, '" & Did & "')" & _
                            '", null, null " & _
                            '", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                            '", case when '" & MQAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                            '", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                            '"where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                            '"where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null) "
                            cnt += 1
                        End If
                    End If


                    'Else

                    '    If OldGalen <> GalenAlloc Or MQAlloc <> OldMQ Then
                    '        If IsNumeric(GalenAlloc) = False And GalenAlloc <> "" Then
                    '            lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & GalenAlloc & ")"
                    '            lblExplanationLabel.DataBind()
                    '            mpeStandard.Show()
                    '            Exit Sub
                    '        ElseIf IsNumeric(MQAlloc) = False And MQAlloc <> "" Then
                    '            lblExplanationLabel.Text = "All submitted allocation amounts must be numeric. (Check " & MQAlloc & ")"
                    '            lblExplanationLabel.DataBind()
                    '            mpeStandard.Show()
                    '            Exit Sub
                    '        End If


                    '        UpdateSQL += "update DWH.ACCT.Galen_Medquest_Allocation " & _
                    '        "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                    '        "where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is null and (isnull(MQClaimed, '') <> '" & MQAlloc & "' or isnull(GalenClaimed, '') <> '" & GalenAlloc & "') " & _
                    '        "insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                    '        "select top 1 a.WFUniqueActivityID " & _
                    '        ", case when '" & GalenAlloc & "' = '' then null else '" & GalenAlloc & "' end " & _
                    '        ", case when '" & GalenAlloc & "' = '' and a.GalenClaimed is null then null  " & _
                    '            "when '" & GalenAlloc & "' = a.GalenClaimed then a.GalenClaimedBy else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                    '        ", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                    '        ", case when '" & MQAlloc & "' = '' and a.MQClaimed is null then null  " & _
                    '            "when '" & MQAlloc & "' = a.MQClaimed then a.MQClaimedBy else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end	  " & _
                    '        ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                    '        "from DWH.ACCT.Galen_Medquest_Allocation a " & _
                    '        "where WFUniqueActivityID = convert(varbinary, '" & Did & "') and InactivatedDate is not null " & _
                    '        "and not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                    '        "where a.WFUniqueActivityID = b.WFUniqueActivityID and b.InactivatedDate is null) " & _
                    '        "order by InactivatedDate desc " & _
                    '        "insert into DWH.ACCT.Galen_Medquest_Allocation (WFUniqueActivityID, GalenClaimed, GalenClaimedBy, MQClaimed, MQClaimedBy, AddedBy, AddedDate) " & _
                    '        "select convert(varbinary, '" & Did & "')" & _
                    '        ", case when '" & GalenAlloc & "' = '' then null else '" & GalenAlloc & "' end " & _
                    '        ", case when '" & GalenAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                    '        ", case when '" & MQAlloc & "' = '' then null else '" & MQAlloc & "' end " & _
                    '        ", case when '" & MQAlloc & "' = '' then null else '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' end " & _
                    '        ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                    '        "where not exists (select * from DWH.ACCT.Galen_Medquest_Allocation b " & _
                    '        "where convert(varbinary, '" & Did & "') = b.WFUniqueActivityID and b.InactivatedDate is null) "
                    '        cnt += 1
                    '    End If
                    'End If




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
            If HideGalen.Text = "0" Then
                e.Row.Cells(2).CssClass = "hidden"
                e.Row.Cells(6).CssClass = "hidden"
            End If
            If HideMQ.Text = "0" Then
                e.Row.Cells(3).CssClass = "hidden"
                e.Row.Cells(6).CssClass = "hidden"
            End If
            If HideSTAR.Text = "0" Then
                e.Row.Cells(4).CssClass = "hidden"
                e.Row.Cells(6).CssClass = "hidden"
            End If
            If HideOther.Text = "0" Then
                e.Row.Cells(5).CssClass = "hidden"
                e.Row.Cells(6).CssClass = "hidden"
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
                        'lblAdminUsrResults.Text = lblAdminUsrResults.Text & "Name: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & "<br/>" & _
                        '  "UserLogin: " & result.GetDirectoryEntry.Properties("samaccountname").Value & "<br/>" & _
                        '  "Email: " & result.GetDirectoryEntry.Properties("mail").Value & "<br/><br/>"
                        'PulledString += ", '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & "' "
                        SQLSrch += " update #TempUsrSearch set Score = Score + 1 where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & _
                            "'  insert into #TempUsrSearch select '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & _
                            "', '" & Replace(result.GetDirectoryEntry.Properties("cn").Value, "'", "''") & "', '" & Replace(result.GetDirectoryEntry.Properties("mail").Value, "'", "''") & _
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
                        'lblAdminUsrResults.Text = lblAdminUsrResults.Text & "Name: " & result.GetDirectoryEntry.Properties("cn").Value & vbCrLf & "<br/>" & _
                        '  "UserLogin: " & result.GetDirectoryEntry.Properties("samaccountname").Value & "<br/>" & _
                        '  "Email: " & result.GetDirectoryEntry.Properties("mail").Value & "<br/><br/>"
                        'PulledString += ", '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & "' "
                        SQLSrch += " update #TempUsrSearch set Score = Score + 1 where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & _
                            "'  insert into #TempUsrSearch select '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & _
                            "', '" & Replace(result.GetDirectoryEntry.Properties("cn").Value, "'", "''") & "', '" & Replace(result.GetDirectoryEntry.Properties("mail").Value, "'", "''") & _
                            "', 1 where not exists (select * from #TempUsrSearch where UserLogin = '" & Replace(result.GetDirectoryEntry.Properties("samaccountname").Value, "'", "''") & "') "
                    End If
                Next

                SubsetSQL += "case when FirstName = '" & Replace(QueryString, "'", "''") & "' then 4 when LastName = '" & Replace(QueryString, "'", "''") & _
                    "' then 4 when EmailAddress = '" & Replace(QueryString, "'", "''") & "' then 4 when FirstName like '" & Replace(QueryString, "'", "''") & "%' then 2 " & _
                    " when LastName like '" & Replace(QueryString, "'", "''") & "%' then 2  when FirstName like '%" & Replace(QueryString, "'", "''") & "%' then 1 " & _
                    " when LastName like '%" & Replace(QueryString, "'", "''") & "%' then 1 when EmailAddress like '%" & Replace(QueryString, "'", "''") & "' then 1 else 0 end + "

            End While

            Dim FurtherSearch As String = SQLSrch & " update u set u.UserLogin = t.UserLogin from DWH.bed.Users u " & _
                "join #TempUsrSearch t on (u.EmailAddress = t.UserMail and t.UserMail <> '') " & _
                "where u.UserLogin is null " & _
                " select isnull(u.UserLogin, t.UserLogin) as UserLogin, " & _
                "case when FirstName + ' ' + LastName = t.UserName then t.UserName " & _
                "	when FirstName + ' ' + LastName <> t.UserName then FirstName + ' ' + LastName  + ' (' + t.UserName + ')' " & _
                "	else isnull(FirstName + ' ' + LastName, t.UserName) end " & _
                "	+ ' - ' + isnull(u.EmailAddress, t.UserMail) + isnull('&#9;' + u.JobTitle, '') + isnull(' at ' + convert(varchar, u.Dept), '') as UserDisplay " & _
                "	, u.Dept, u.JobTitle, Rnk, Score,  isnull(convert(varchar,u.EmployeeNumber), '-999'+'|' + t.UserLogin) + '|' + isnull(u.EmailAddress, t.UserMail)  as UserID " & _
                "from (select *, " & Left(SubsetSQL, Len(SubsetSQL) - 2) & " as Rnk from DWH.Bed.Users where InactivatedDate is null) u " & _
                "full join #TempUsrSearch t on u.UserLogin = t.UserLogin  where Rnk > 0 or Score > 0" & _
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

            ExecuteSql("Insert into DWH.Bed.Users (UserLogin, FirstName, LastName, EmailAddress) values (" & _
                       UserFull & _
                ", '" & Replace(Left(rblTxt, InStr(rblTxt, " ") - 1), "'", "''") & "', '" & _
                Replace(namestring.Substring(InStrRev(namestring, " ")), "'", "''") & _
                "', '" & Replace(rblVal.Substring(GetNthIndex(rblVal, "|", 2) + 1), "'", "''") & "')")
        Else
            UserFull = "'" & Replace(rblNewUserSearchResults.SelectedItem.Value, "'", "''") & "'"
        End If

        If (GetScalar("select isnull(count(*),0) from DWH.ACCT.Galen_Medquest_User_Access_2 where InactivatedDate is null and UserLogin = " & UserFull)) > 0 Then
            lblExplanationLabel.Text = "User Already Exists"
            mpeStandard.Show()
            Exit Sub
        End If

        ExecuteSql("insert into DWH.ACCT.Galen_Medquest_User_Access_2  (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
                   "values (" & UserFull & ", -1, 1, 0, 0, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate())")


        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub

    Protected Sub ddlGalenAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblGalenAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlGalenAcces"), DropDownList)

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

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " & _
            "		from DWH.ACCT.Galen_Medquest_User_Access_2 a " & _
            "		join (select 2 Claim_ID union select 3 union select -1) x on 1 = 1 " & _
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the All-User row */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the Galen Row row if it changed */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID = 1 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " & _
            " " & _
                    "/* Insert new row if user didn't already exist */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 1, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " & _
            "		where not exists (select * from DWH.ACCT.Galen_Medquest_User_Access_2 where InactivatedDate is null and Claim_ID = 1 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub
    Protected Sub ddlMQAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblMQAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlMQAcces"), DropDownList)

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

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " & _
            "		from DWH.ACCT.Galen_Medquest_User_Access_2 a " & _
            "		join (select 1 Claim_ID union select 3 union select -1) x on 1 = 1 " & _
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the All-User row */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the Galen Row row if it changed */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID = 2 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " & _
            " " & _
                    "/* Insert new row if user didn't already exist */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 2, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " & _
            "		where not exists (select * from DWH.ACCT.Galen_Medquest_User_Access_2 where InactivatedDate is null and Claim_ID = 2 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub
    Protected Sub ddlSTARAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblSTARAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlSTARAcces"), DropDownList)

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

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " & _
            "		from DWH.ACCT.Galen_Medquest_User_Access_2 a " & _
            "		join (select 2 Claim_ID union select 1 union select -1) x on 1 = 1 " & _
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the All-User row */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the Galen Row row if it changed */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID = 3 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " & _
            " " & _
                    "/* Insert new row if user didn't already exist */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', 3, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " & _
            "		where not exists (select * from DWH.ACCT.Galen_Medquest_User_Access_2 where InactivatedDate is null and Claim_ID = 3 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()

    End Sub
    Protected Sub ddlOtherAccess_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Dim UpdatesSql As String = ""

        For i As Integer = 0 To gvUserAccess.Rows.Count - 1

            Dim lblAdminServicesRowFacility As Label = CType(gvUserAccess.Rows(i).FindControl("lblOtherAccess"), Label)
            Dim ddlAdminServicesRowFacility As DropDownList = CType(gvUserAccess.Rows(i).FindControl("ddlOtherAcces"), DropDownList)

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

                UpdatesSql += "		/* Insert all others if User used to be an All-User */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select a.UserLogin, x.Claim_ID, Vision, Permission, AdminRights, AddedBy, getdate() " & _
            "		from DWH.ACCT.Galen_Medquest_User_Access_2 a " & _
            "		join (select 2 Claim_ID union select 3 union select 1) x on 1 = 1 " & _
            "		where InactivatedDate is null and a.Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the All-User row */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID is null and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            " " & _
                    "/* De-activate the Galen Row row if it changed */ " & _
            "		update DWH.ACCT.Galen_Medquest_User_Access_2  " & _
            "		set InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', InactivatedDate = getdate() " & _
            "		where InactivatedDate is null and Claim_ID = -1 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "' " & _
            "		and (Vision <> '" & Replace(Vision, "'", "''") & "' or Permission <> '" & Replace(permission, "'", "''") & "' or AdminRights <> '" & Replace(Admin, "'", "''") & "') " & _
            " " & _
                    "/* Insert new row if user didn't already exist */ " & _
            "		insert into DWH.ACCT.Galen_Medquest_User_Access_2 (UserLogin, Claim_ID, Vision, Permission, AdminRights, AddedBy, AddedDate) " & _
            "		select '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "', -1, '" & Replace(Vision, "'", "''") & "', '" & Replace(permission, "'", "''") & "', '" & Replace(Admin, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate()		  " & _
            "		where not exists (select * from DWH.ACCT.Galen_Medquest_User_Access_2 where InactivatedDate is null and Claim_ID = -1 and UserLogin = '" & Replace(gvUserAccess.DataKeys(i).Value.ToString, "'", "''") & "') "


            End If


        Next

        If Len(UpdatesSql) > 0 Then
            ExecuteSql(UpdatesSql)
        End If

        gvUserAccess.DataSource = LoadAdminData()
        gvUserAccess.DataBind()
    End Sub

    Private Sub SynchAccess()

        Dim x As String = "update a " & _
            "set a.InactivatedDate = getdate(), a.InactivatedBy = 'Auto-Removed' " & _
            "from DWH.ACCT.Galen_Medquest_User_Access_2 a " & _
            "where InactivatedDate is null and not exists (select * from DWH.ACCT.Galen_Medquest_User_Access_2 b " & _
            "	where a.UserLogin = b.UserLogin and b.InactivatedDate is null and (b.Vision > 0 or b.Permission > 0 or b.AdminRights > 0)) " & _
            " " & _
        "insert into WebFD.dbo.aspnet_Users " & _
    "select '5A20864E-8700-4FFF-9419-8445308B25DA', HASHBYTES('MD2', dar.UserLogin), UserLogin, UserLogin, null, 0, getdate() " & _
    "from DWH.ACCT.Galen_Medquest_User_Access_2 dar " & _
    "where inactivatedDate is null and not exists (select * from WebFD.dbo.aspnet_Users au  " & _
    "where au.UserName = dar.UserLogin) " & _
    " " & _
    "insert into WebFD.dbo.aspnet_UsersInRoles  " & _
    "select distinct UserId, 'ECFEF86A-E88A-137E-C08F-002350FA0B1F' from " & _
    "DWH.ACCT.Galen_Medquest_User_Access_2 dar " & _
    "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
    "where inactivatedDate is null and not exists (select * from " & _
    "WebFD.dbo.aspnet_UsersInRoles uir  " & _
    "join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId " & _
    "where au.UserId = uir.UserId " & _
    "and RoleName = 'GalenMQAlloc') " & _
    " " & _
    "delete uir from WebFD.dbo.aspnet_UsersInRoles uir  " & _
    "where not exists ( " & _
    "select UserId from " & _
    "DWH.ACCT.Galen_Medquest_User_Access_2 dar " & _
    "join WebFD.dbo.aspnet_Users au on  au.UserName = dar.UserLogin " & _
    "where inactivatedDate is null and uir.UserID = au.UserID) and RoleID = 'ECFEF86A-E88A-137E-C08F-002350FA0B1F'"

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
        If HideGalen.Text = "0" Then
            e.Row.Cells(6).CssClass = "hidden"
        End If
        If HideMQ.Text = "0" Then
            e.Row.Cells(7).CssClass = "hidden"
        End If
        If HideSTAR.Text = "0" Then
            e.Row.Cells(8).CssClass = "hidden"
        End If
        If HideOther.Text = "0" Then
            e.Row.Cells(9).CssClass = "hidden"
        End If
    End Sub

    Private Sub gvUserAccess_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUserAccess.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlGalenAcces As DropDownList = e.Row.FindControl("ddlGalenAcces")
            Dim ddlMQAcces As DropDownList = e.Row.FindControl("ddlMQAcces")
            Dim ddlSTARAcces As DropDownList = e.Row.FindControl("ddlSTARAcces")
            Dim ddlOtherAcces As DropDownList = e.Row.FindControl("ddlOtherAcces")
            Dim x As New DataView
            Dim x2 As New DataView
            Dim chk As Integer = GetScalar("select isnull(count(*), 0) from WebFD.dbo.aspnet_Users u " & _
            "		join WebFD.dbo.vw_aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
            "		join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'")

            Dim s As String = " select 'Update' as ValueDesc, 2 as Value " & _
            " union select 'View' as ValueDesc, 1 as Value " & _
            " union select 'None' as ValueDesc, 0 as Value " & _
            " union select 'Admin' as ValueDesc, 3 as Value  " & _
            " where exists " & _
            " (select u.UserName from WebFD.dbo.aspnet_Users u " & _
            "		join WebFD.dbo.vw_aspnet_UsersInRoles uir on u.UserId = uir.UserId " & _
            "		join WebFD.dbo.aspnet_Roles r on uir.RoleId = r.RoleId where r.RoleName = 'Developer' and u.UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "')  " & _
            "		order by Value "

            Dim s2 As String = " select 'Update' as ValueDesc, 2 as Value " & _
            " union select 'View' as ValueDesc, 1 as Value " & _
            " union select 'None' as ValueDesc, 0 as Value " & _
            " union select 'Admin' as ValueDesc, 3 as Value  " & _
            "		order by Value "

            x = GetData(s).DefaultView
            If chk = 0 Then
                x2 = GetData(s2).DefaultView
            End If


            If e.Row.DataItem("Galen").ToString.ToUpper = "3" And chk = 0 Then
                ddlGalenAcces.DataSource = x2
            Else
                ddlGalenAcces.DataSource = x
            End If

            ddlGalenAcces.DataTextField = "ValueDesc"
            ddlGalenAcces.DataValueField = "Value"
            ddlGalenAcces.DataBind()

            ddlGalenAcces.SelectedValue = e.Row.DataItem("Galen").ToString.ToUpper

            If e.Row.DataItem("Galen").ToString.ToUpper = "3" And chk = 0 Then
                ddlGalenAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateGalen").ToString.ToUpper = "1" Then
                ddlGalenAcces.Enabled = True
            Else
                ddlGalenAcces.Enabled = False
            End If

            If e.Row.DataItem("MQ").ToString.ToUpper = "3" And chk = 0 Then
                ddlMQAcces.DataSource = x2
            Else
                ddlMQAcces.DataSource = x
            End If
            ddlMQAcces.DataTextField = "ValueDesc"
            ddlMQAcces.DataValueField = "Value"
            ddlMQAcces.DataBind()

            ddlMQAcces.SelectedValue = e.Row.DataItem("MQ").ToString.ToUpper
            If e.Row.DataItem("MQ").ToString.ToUpper = "3" And chk = 0 Then
                ddlMQAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateMQ").ToString.ToUpper = "1" Then
                ddlMQAcces.Enabled = True
            Else
                ddlMQAcces.Enabled = False
            End If


            If e.Row.DataItem("STAR").ToString.ToUpper = "3" And chk = 0 Then
                ddlSTARAcces.DataSource = x2
            Else
                ddlSTARAcces.DataSource = x
            End If
            ddlSTARAcces.DataTextField = "ValueDesc"
            ddlSTARAcces.DataValueField = "Value"
            ddlSTARAcces.DataBind()

            ddlSTARAcces.SelectedValue = e.Row.DataItem("STAR").ToString.ToUpper
            If e.Row.DataItem("STAR").ToString.ToUpper = "3" And chk = 0 Then
                ddlSTARAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateSTAR").ToString.ToUpper = "1" Then
                ddlSTARAcces.Enabled = True
            Else
                ddlSTARAcces.Enabled = False
            End If


            If e.Row.DataItem("Other").ToString.ToUpper = "3" And chk = 0 Then
                ddlOtherAcces.DataSource = x2
            Else
                ddlOtherAcces.DataSource = x
            End If
            ddlOtherAcces.DataTextField = "ValueDesc"
            ddlOtherAcces.DataValueField = "Value"
            ddlOtherAcces.DataBind()

            ddlOtherAcces.SelectedValue = e.Row.DataItem("Other").ToString.ToUpper
            If e.Row.DataItem("Other").ToString.ToUpper = "3" And chk = 0 Then
                ddlOtherAcces.Enabled = False
            ElseIf e.Row.DataItem("UpdateOther").ToString.ToUpper = "1" Then
                ddlOtherAcces.Enabled = True
            Else
                ddlOtherAcces.Enabled = False
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

        cblTranDescs.DataSource = GetData("select distinct TranDesc from DWH.WF.WFAllActivity where AcctNo = '4129405627'")
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

    Protected Sub Popup_Click(sender As Object, e As EventArgs)
        Dim btn As LinkButton = sender
        lblExplanationLabel.Text = Server.HtmlDecode(btn.CommandArgument.ToString)
        mpeStandard.Show()
    End Sub

End Class