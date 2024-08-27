Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.IO
Imports System.DirectoryServices
Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal
Imports FinanceWeb.WebFinGlobal

Public Class WFPPS
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
    Private Shared SuperAdmin As Integer = 0
    Public Shared Rowsdv As New DataView

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

            Select Case Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
                Case "cw996788"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                Case "e223211"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Sarah 8/16/2018
                    'btnSAUpdateBag.Visible = False
                    'btnSARejectReason.Visible = False
                Case "e237162"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Jordan 8/26/2021
                    'btnSAUpdateBag.Visible = False
                    'btnSARejectReason.Visible = False
                Case "e232385"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Teri 1/25/2022
                    'btnSAUpdateBag.Visible = False
                    'btnSARejectReason.Visible = False
                Case "e230760"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted through Access Request Tool 5/3/2022

                Case "e221052"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Sarah 8/16/2018
                    'btnSAUpdateBag.Visible = False
                    'btnSARejectReason.Visible = False
                Case "e229689"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Jordan 6/22/2020

                Case "e209378"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "mf995052"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Jordan 1/8/2019
                    'btnSAUpdateBag.Visible = False
                    'btnSARejectReason.Visible = False
                Case "il990442"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True

                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                    ' permission granted by Jordan 12/30/2020


                Case "sh991605"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "dt992475"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                Case "e220419"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "dt992475"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                Case "kh996062"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "dt992475"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
                Case "dt992475"
                    SuperAdmin = 1
                    admin = 0
                    tpSuperAdmin.Visible = True
                    'Case "dt992475"
                    '    tpAdmin.Visible = True
                    txtSAEndDate.Text = Today.ToShortDateString
                    txtSAStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    SAUpdateUserDDL(SuperAdmin)
                    SAUpdateEntityDDL(SuperAdmin)
                    SAUpdateLocationDDL(SuperAdmin)
                    SAPopulateAdmin2Grid(SuperAdmin)
            End Select

            If admin = 1 Then
                tpAdminReject.Visible = True
                'txtAdminWhichDate.Text = Today.ToShortDateString
                txtDDEndDate.Text = Today.ToShortDateString
                txtDDStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                'FillCBL()
                'PopulateAdminGrid()
                UpdateUserDDL(admin)
                UpdateEntityDDL(admin)
                UpdateLocationDDL(admin)
                PopulateAdmin2Grid(admin)
            Else
                Dim managersql As String = "Select count(*) from DWH.WF.PPS_Management where Active = 1 and Manager = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "'"
                If GetScalar(managersql) > 0 Then
                    tpAdminReject.Visible = True
                    txtRejectReason.Visible = False
                    btnRejectBag.Visible = False
                    'txtAdminWhichDate.Text = Today.ToShortDateString
                    txtDDEndDate.Text = Today.ToShortDateString
                    txtDDStartDate.Text = DateAdd(DateInterval.WeekOfYear, -1, Today).ToShortDateString
                    'FillCBL()
                    'PopulateAdminGrid()
                    UpdateUserDDL(admin)
                    UpdateEntityDDL(admin)
                    UpdateLocationDDL(admin)
                    PopulateAdmin2Grid(admin)
                End If
            End If

            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet

            lblDepositDate.Text = Today.ToShortDateString()
            'txtDepositSlip.Text = " -- NotEnabled -- "


            ' Flipping to WF.Merchant_LU 7/27/2017 CRW
            'Dim ddlfirstsql As String = "select top 1 ' -- noneselected -- ' as [Merchant ID], 'Select Department Location' as [Merchant Description], 0 as ord from  DWH.WF.MappingTable2 " & _
            '"union " & _
            '"select distinct case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as [MerchantDescription], 1 " & _
            '"from DWH.WF.MappingTable2 " & _
            '" where [MerchantDescription] not like 'ACC.%' " & _
            '"and [MerchantDescription] not like 'ZZ%' " & _
            '"and [MerchantID] not like '%E%' " & _
            '"order by ord, [Merchant Description] "

            ' Flipping to WF.Merchant_ID_LU 7/23/2019 CRW
            'Dim ddlfirstsql As String = "select ' -- noneselected -- ' as [Merchant ID], 'Select Department Location' as [Merchant Description], 0 as ord " & _
            '    "union " & _
            '    "select distinct case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as [MerchantDescription], 1 " & _
            '    "from DWH.WF.Merchant_LU " & _
            '    " where Active = 1 and [MerchantDescription] not like 'ACC.%' " & _
            '    "and [MerchantDescription] not like 'ZZ%' " & _
            '    "and [MerchantID] not like '%E%' " & _
            '    "order by ord, [Merchant Description] "

            ' Facility Table 11/22/2019 CRW
            ' Dim ddlfirstsql As String = "select ' -- noneselected -- ' as [Merchant ID], 'Select Department Location' as [Merchant Description], 0 as ord " & _
            '"union " & _
            '"select distinct case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '"when [MerchantID] = '172123612993' then 'Duluth' " & _
            '"when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '"when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '"when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '"when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '"when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '"when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '"when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '"when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '"when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as [MerchantDescription], 1 " & _
            '"from DWH.WF.Merchant_ID_LU mil" & _
            '" where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '" and [MerchantDescription] not like 'ACC.%' " & _
            '"and [MerchantDescription] not like 'ZZ%' " & _
            '"and [MerchantID] not like '%E%' " & _
            '"order by ord, [Merchant Description] "

            Dim ddlfirstsql As String = "select '-1' as [Merchant ID], 'Select Department Location' as [Merchant Description], 0 as ord  " & _
"                union  " & _
"                select distinct isnull(cast(Fac_ID as bigint), mil.MerchantID) as MerchantID             " & _
"				,isnull(f.Fac_Desc, 'Other -- (' + [MerchantDescription] + ')')  " & _
"				, 1  as ord " & _
"                from DWH.WF.Merchant_ID_LU mil " & _
"				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')  " & _
"                 where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999')  " & _
"                 and [MerchantDescription] not like 'ACC.%'  " & _
"                and [MerchantDescription] not like 'ZZ%'  " & _
"                and mil.MerchantID not like '%E%'  " & _
"                order by ord, [Merchant Description]  "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(ddlfirstsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlDepositLoc.Items.Clear()
            ddlDepositLoc.DataSource = ds
            ddlDepositLoc.DataValueField = "Merchant ID"
            ddlDepositLoc.DataTextField = "Merchant Description"
            ddlDepositLoc.DataBind()

            UpdateDDLs()

            'Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord from  DWH.WF.MappingTable2 " & _
            '    "union " & _
            '    "select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.MappingTable2 " & _
            '    "where [MerchantDescription] not like 'ZZ%' and " & _
            '    "[MerchantDescription] not like 'ACC%' " & _
            '    "order by ord, [Merchant Description] "

            'ds.Clear()
            'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If

            '    cmd = New SqlCommand(ddlsetstring, conn)
            '    da.SelectCommand = cmd
            '    da.SelectCommand.CommandTimeout = 86400
            '    da.Fill(ds, "OData")

            'End Using

            'ddlEditOutlet.DataSource = ds
            'ddlEditOutlet.DataValueField = "Merchant Description"
            'ddlEditOutlet.DataTextField = "txt"
            'ddlEditOutlet.DataBind()

            'Dim gvinitstring As String = "select null as CollectionDate, null as Outlet, null as Cash, null as ManualChecks, null as OutletTotal, null as Agree, null as Explain, 0"

            'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If

            '    cmd = New SqlCommand(gvinitstring, conn)
            '    da.SelectCommand = cmd
            '    da.SelectCommand.CommandTimeout = 86400
            '    da.Fill(ds, "OData")

            'End Using

            'gvDeposits.DataSource = ds
            'gvDeposits.DataBind()

            rwcnt = 0
            'AddPPSRow()
            'AddPPSRow()
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
    Private Sub UpdateDDLs()

        Dim da As New SqlDataAdapter
        Dim cmd As SqlCommand
        Dim ds As New DataSet

        ddlset.Clear()

        '    Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord " & _
        '"union " & _
        '"select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.DWH.WF.Merchant_ID_LU mil " & _
        '"where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ZZ%' and " & _
        '"[MerchantDescription] not like 'ACC%' " & _
        '" and ([MerchantID] = '" & ddlDepositLoc.SelectedValue & "' or (([MerchantDescription] like 'M.%' or [MerchantDescription] like 'MedQuest.%') and 'MedQuest' = '" & _
        'ddlDepositLoc.SelectedValue & "')) " & _
        '"order by ord, [Merchant Description] "

        '' Switching to WF.Merchant_ID_LU 7/23/2019 CRW
        'Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord " & _
        '    "union " & _
        '    "select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.Merchant_LU " & _
        '    "where Active = 1 and [MerchantDescription] not like 'ZZ%' and " & _
        '    "[MerchantDescription] not like 'ACC%' " & _
        '    " and ([MerchantID] = '" & ddlDepositLoc.SelectedValue & "' or (([MerchantDescription] like 'M.%' or [MerchantDescription] like 'MedQuest.%') and 'MedQuest' = '" & _
        '    ddlDepositLoc.SelectedValue & "')) " & _
        '    "order by ord, [Merchant Description] "

        ' 11/22/2019
        '    Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord " & _
        '"union " & _
        '"select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.Merchant_ID_LU " & _
        '"where InactivatedDate is null and getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ZZ%' and " & _
        '"[MerchantDescription] not like 'ACC%' " & _
        '" and ([MerchantID] = '" & ddlDepositLoc.SelectedValue & "' or (([MerchantDescription] like 'M.%' or [MerchantDescription] like 'MedQuest.%') and 'MedQuest' = '" & _
        'ddlDepositLoc.SelectedValue & "')) " & _
        '"order by ord, [Merchant Description] "

        Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord " & _
"union " & _
"select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 " & _
"from DWH.wf.Merchant_ID_LU mil  " & _
"				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
"                where mil.InactivatedDate is null and mil.MerchantDescription not like 'ACC%' and mil.MerchantDescription not like 'ZZ%' " & _
"				and getdate() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
" and (isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & ddlDepositLoc.SelectedValue & "') " & _
"order by ord, [Merchant Description] "

        '' Switching to WF.Merchant_LU 7/27/2017 CRW
        '    Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord from  DWH.WF.MappingTable2 " & _
        '"union " & _
        '"select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.MappingTable2 " & _
        '"where [MerchantDescription] not like 'ZZ%' and " & _
        '"[MerchantDescription] not like 'ACC%' " & _
        '" and ([MerchantID] = '" & ddlDepositLoc.SelectedValue & "' or (([MerchantDescription] like 'M.%' or [MerchantDescription] like 'MedQuest.%') and 'MedQuest' = '" & _
        'ddlDepositLoc.SelectedValue & "')) " & _
        '"order by ord, [Merchant Description] "

        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New SqlCommand(ddlsetstring, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ddlset, "OData")

        End Using

        ddlOutlet1.Items.Clear()
        ddlOutlet1.DataSource = ddlset
        ddlOutlet1.DataValueField = "Merchant Description"
        ddlOutlet1.DataTextField = "txt"
        ddlOutlet1.DataBind()

        ddlOutlet2.Items.Clear()
        ddlOutlet2.DataSource = ddlset
        ddlOutlet2.DataValueField = "Merchant Description"
        ddlOutlet2.DataTextField = "txt"
        ddlOutlet2.DataBind()

        ddlOutlet3.Items.Clear()
        ddlOutlet3.DataSource = ddlset
        ddlOutlet3.DataValueField = "Merchant Description"
        ddlOutlet3.DataTextField = "txt"
        ddlOutlet3.DataBind()

        ddlOutlet4.Items.Clear()
        ddlOutlet4.DataSource = ddlset
        ddlOutlet4.DataValueField = "Merchant Description"
        ddlOutlet4.DataTextField = "txt"
        ddlOutlet4.DataBind()

        ddlOutlet5.Items.Clear()
        ddlOutlet5.DataSource = ddlset
        ddlOutlet5.DataValueField = "Merchant Description"
        ddlOutlet5.DataTextField = "txt"
        ddlOutlet5.DataBind()

        ddlOutlet6.Items.Clear()
        ddlOutlet6.DataSource = ddlset
        ddlOutlet6.DataValueField = "Merchant Description"
        ddlOutlet6.DataTextField = "txt"
        ddlOutlet6.DataBind()

        ddlOutlet7.Items.Clear()
        ddlOutlet7.DataSource = ddlset
        ddlOutlet7.DataValueField = "Merchant Description"
        ddlOutlet7.DataTextField = "txt"
        ddlOutlet7.DataBind()

        ddlOutlet8.Items.Clear()
        ddlOutlet8.DataSource = ddlset
        ddlOutlet8.DataValueField = "Merchant Description"
        ddlOutlet8.DataTextField = "txt"
        ddlOutlet8.DataBind()

        ddlOutlet9.Items.Clear()
        ddlOutlet9.DataSource = ddlset
        ddlOutlet9.DataValueField = "Merchant Description"
        ddlOutlet9.DataTextField = "txt"
        ddlOutlet9.DataBind()

        ddlOutlet10.Items.Clear()
        ddlOutlet10.DataSource = ddlset
        ddlOutlet10.DataValueField = "Merchant Description"
        ddlOutlet10.DataTextField = "txt"
        ddlOutlet10.DataBind()

        ddlOutlet11.Items.Clear()
        ddlOutlet11.DataSource = ddlset
        ddlOutlet11.DataValueField = "Merchant Description"
        ddlOutlet11.DataTextField = "txt"
        ddlOutlet11.DataBind()

        ddlOutlet12.Items.Clear()
        ddlOutlet12.DataSource = ddlset
        ddlOutlet12.DataValueField = "Merchant Description"
        ddlOutlet12.DataTextField = "txt"
        ddlOutlet12.DataBind()

        ddlOutlet13.Items.Clear()
        ddlOutlet13.DataSource = ddlset
        ddlOutlet13.DataValueField = "Merchant Description"
        ddlOutlet13.DataTextField = "txt"
        ddlOutlet13.DataBind()

        ddlOutlet14.Items.Clear()
        ddlOutlet14.DataSource = ddlset
        ddlOutlet14.DataValueField = "Merchant Description"
        ddlOutlet14.DataTextField = "txt"
        ddlOutlet14.DataBind()

        ddlOutlet15.Items.Clear()
        ddlOutlet15.DataSource = ddlset
        ddlOutlet15.DataValueField = "Merchant Description"
        ddlOutlet15.DataTextField = "txt"
        ddlOutlet15.DataBind()

        ddlOutlet16.Items.Clear()
        ddlOutlet16.DataSource = ddlset
        ddlOutlet16.DataValueField = "Merchant Description"
        ddlOutlet16.DataTextField = "txt"
        ddlOutlet16.DataBind()

        ddlOutlet17.Items.Clear()
        ddlOutlet17.DataSource = ddlset
        ddlOutlet17.DataValueField = "Merchant Description"
        ddlOutlet17.DataTextField = "txt"
        ddlOutlet17.DataBind()

        ddlOutlet18.Items.Clear()
        ddlOutlet18.DataSource = ddlset
        ddlOutlet18.DataValueField = "Merchant Description"
        ddlOutlet18.DataTextField = "txt"
        ddlOutlet18.DataBind()

        ddlOutlet19.Items.Clear()
        ddlOutlet19.DataSource = ddlset
        ddlOutlet19.DataValueField = "Merchant Description"
        ddlOutlet19.DataTextField = "txt"
        ddlOutlet19.DataBind()

        ddlOutlet20.Items.Clear()
        ddlOutlet20.DataSource = ddlset
        ddlOutlet20.DataValueField = "Merchant Description"
        ddlOutlet20.DataTextField = "txt"
        ddlOutlet20.DataBind()

        ddlOutlet21.Items.Clear()
        ddlOutlet21.DataSource = ddlset
        ddlOutlet21.DataValueField = "Merchant Description"
        ddlOutlet21.DataTextField = "txt"
        ddlOutlet21.DataBind()

        ddlOutlet22.Items.Clear()
        ddlOutlet22.DataSource = ddlset
        ddlOutlet22.DataValueField = "Merchant Description"
        ddlOutlet22.DataTextField = "txt"
        ddlOutlet22.DataBind()

        ddlOutlet23.Items.Clear()
        ddlOutlet23.DataSource = ddlset
        ddlOutlet23.DataValueField = "Merchant Description"
        ddlOutlet23.DataTextField = "txt"
        ddlOutlet23.DataBind()

        ddlOutlet24.Items.Clear()
        ddlOutlet24.DataSource = ddlset
        ddlOutlet24.DataValueField = "Merchant Description"
        ddlOutlet24.DataTextField = "txt"
        ddlOutlet24.DataBind()

        ddlOutlet25.Items.Clear()
        ddlOutlet25.DataSource = ddlset
        ddlOutlet25.DataValueField = "Merchant Description"
        ddlOutlet25.DataTextField = "txt"
        ddlOutlet25.DataBind()

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

#Region "gvDeposits"
    '    Private Sub gvDeposits_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDeposits.RowCancelingEdit
    '        Try

    '            gvDeposits.EditIndex = -1

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try

    '    End Sub

    '    Private Sub gvDeposits_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvDeposits.RowCreated
    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

    '        End If

    '        If gvDeposits.EditIndex > -1 Then
    '            e.Row.Attributes.Remove("onclick")
    '        End If

    '        'If User.Identity.IsAuthenticated And User.IsInRole("FSI User") Then

    '        'Else
    '        '    e.Row.Cells(0).CssClass = "hidden"
    '        'End If
    '    End Sub

    '    Private Sub gvDeposits_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvDeposits.RowEditing
    '        Try
    '            gvDeposits.EditIndex = e.NewEditIndex
    '            'gvDeposits.DataSource = DirectCast(ViewState("TBL"), DataTable)
    '            'gvDeposits.DataBind()
    '            Dim txtDate As TextBox = gvDeposits.Rows(e.NewEditIndex).FindControl("lblDepositDate")
    '            Dim lblDate As Label = gvDeposits.Rows(e.NewEditIndex).FindControl("lblDepositDate")
    '            Dim ddlDepositOutlet As DropDownList = gvDeposits.Rows(e.NewEditIndex).FindControl("ddlDepositOutlet")
    '            Dim lblOutlet As Label = gvDeposits.Rows(e.NewEditIndex).FindControl("lblDepositOutlet")

    '            txtDate.Visible = True
    '            lblDate.Visible = False
    '            ddlDepositOutlet.Visible = True
    '            lblOutlet.Visible = False

    '            ddlDepositOutlet.DataSource = (ddlset)
    '            ddlDepositOutlet.DataValueField = "Merchant Description"
    '            ddlDepositOutlet.DataTextField = "Merchant Description"
    '            ddlDepositOutlet.DataBind()

    '            If lblOutlet.Text = "" Then
    '            Else
    '                ddlDepositOutlet.SelectedValue = lblOutlet.Text
    '            End If

    '            'gvDeposits.Columns(0).ItemStyle.Width = "100px"

    '            For Each canoe As GridViewRow In gvDeposits.Rows
    '                If canoe.RowIndex = e.NewEditIndex Then
    '                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5EAFF")
    '                ElseIf canoe.RowIndex Mod 2 = 0 Then
    '                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2EAD9")
    '                Else
    '                    canoe.BackColor = System.Drawing.Color.White
    '                End If
    '            Next

    '        Catch ex As Exception
    '            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '        End Try
    '    End Sub



    '    Private Sub gvDeposits_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvDeposits.RowUpdating


    '        Dim ddlRow As DropDownList
    '        ddlRow = DirectCast(gvDeposits.Rows(e.RowIndex).Cells(2).FindControl("ddlDepositOutlet"), DropDownList)



    '    End Sub

    '    Private Sub gvDeposits_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDeposits.SelectedIndexChanged

    '        Dim txtDate As TextBox
    '        Dim ddlDepositOutlet As DropDownList
    '        Dim lblOutlet As Label

    '        Dim da As New SqlDataAdapter
    '        Dim cmd As SqlCommand
    '        Dim ds As New DataSet

    '        Dim fullsql As String

    '        fullsql = "create table #tempy ( " & _
    '                "CollectionDate date, " & _
    '                "Outlet varchar(max), " & _
    '                "Cash numeric(18,2), " & _
    '                "ManualChecks numeric (18,2), " & _
    '                "Agree varchar(3), " & _
    '                "Explain varchar(max) " & _
    '                ") "

    '        For Each canoe As GridViewRow In gvDeposits.Rows

    '            txtDate = canoe.FindControl("lblDepositDate")
    '            ddlDepositOutlet = canoe.FindControl("ddlDepositOutlet")
    '            lblOutlet = canoe.FindControl("lblDepositOutlet")

    '            fullsql += " insert into #tempy values ('" & txtDate.Text & "', '" & ddlDepositOutlet.SelectedValue & "', 200.00, 200.00, 'YES', null)"

    '            If canoe.RowIndex = gvDeposits.SelectedIndex Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5EAFF")
    '            ElseIf canoe.RowIndex Mod 2 = 0 Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2EAD9")
    '            Else
    '                canoe.BackColor = System.Drawing.Color.White
    '            End If
    '        Next

    '        fullsql += "select CollectionDate, Outlet, Cash, ManualChecks, Cash + ManualChecks as OutletTotal, Agree, Explain, " & _
    '            "case when CollectionDate is not null then 1 when Outlet is not null then 1 when Cash is not null then 1 " & _
    '            "when ManualChecks is not null then 1 when Agree is not null then 1 when Explain is not null then 1 " & _
    '            "else 0 end as rlcheck " & _
    '            "from #tempy " & _
    '            "union " & _
    '            "select null as CollectionDate, null as Outlet, null as Cash, null as ManualChecks, null as OutletTotal, null as Agree, null as Explain, 0 " & _
    '            "order by case when CollectionDate is not null then 1 when Outlet is not null then 1 when Cash is not null then 1 " & _
    '            "when ManualChecks is not null then 1 when Agree is not null then 1 when Explain is not null then 1 " & _
    '            "else 0 end desc"

    '        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New SqlCommand(fullsql, conn)
    '            da.SelectCommand = cmd
    '            da.SelectCommand.CommandTimeout = 86400
    '            da.Fill(ds, "OData")

    '        End Using

    '        gvDeposits.DataSource = ds
    '        gvDeposits.DataBind()


    '    End Sub

    '    Sub ReDoRows()
    '        For Each canoe As GridViewRow In gvDeposits.Rows
    '            If canoe.RowIndex = gvDeposits.SelectedIndex Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5EAFF")
    '            ElseIf canoe.RowIndex Mod 2 = 0 Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2EAD9")
    '            Else
    '                canoe.BackColor = System.Drawing.Color.White
    '            End If
    '        Next
    '    End Sub

#End Region

#Region "AgreeEnable"
    Private Sub ddlAgree1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree1.SelectedIndexChanged
        If ddlAgree1.SelectedValue = "0" Then
            txtExplain1.Enabled = True
        Else
            txtExplain1.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree2.SelectedIndexChanged
        If ddlAgree2.SelectedValue = "0" Then
            txtExplain2.Enabled = True
        Else
            txtExplain2.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree3.SelectedIndexChanged
        If ddlAgree3.SelectedValue = "0" Then
            txtExplain3.Enabled = True
        Else
            txtExplain3.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree4.SelectedIndexChanged
        If ddlAgree4.SelectedValue = "0" Then
            txtExplain4.Enabled = True
        Else
            txtExplain4.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree5.SelectedIndexChanged
        If ddlAgree5.SelectedValue = "0" Then
            txtExplain5.Enabled = True
        Else
            txtExplain5.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree6.SelectedIndexChanged
        If ddlAgree6.SelectedValue = "0" Then
            txtExplain6.Enabled = True
        Else
            txtExplain6.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree7.SelectedIndexChanged
        If ddlAgree7.SelectedValue = "0" Then
            txtExplain7.Enabled = True
        Else
            txtExplain7.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree8.SelectedIndexChanged
        If ddlAgree8.SelectedValue = "0" Then
            txtExplain8.Enabled = True
        Else
            txtExplain8.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree9.SelectedIndexChanged
        If ddlAgree9.SelectedValue = "0" Then
            txtExplain9.Enabled = True
        Else
            txtExplain9.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree10.SelectedIndexChanged
        If ddlAgree10.SelectedValue = "0" Then
            txtExplain10.Enabled = True
        Else
            txtExplain10.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree11.SelectedIndexChanged
        If ddlAgree11.SelectedValue = "0" Then
            txtExplain11.Enabled = True
        Else
            txtExplain11.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree12.SelectedIndexChanged
        If ddlAgree12.SelectedValue = "0" Then
            txtExplain12.Enabled = True
        Else
            txtExplain12.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree13_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree13.SelectedIndexChanged
        If ddlAgree13.SelectedValue = "0" Then
            txtExplain13.Enabled = True
        Else
            txtExplain13.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree14_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree14.SelectedIndexChanged
        If ddlAgree14.SelectedValue = "0" Then
            txtExplain14.Enabled = True
        Else
            txtExplain14.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree15_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree15.SelectedIndexChanged
        If ddlAgree15.SelectedValue = "0" Then
            txtExplain15.Enabled = True
        Else
            txtExplain15.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree16_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree16.SelectedIndexChanged
        If ddlAgree16.SelectedValue = "0" Then
            txtExplain16.Enabled = True
        Else
            txtExplain16.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree17.SelectedIndexChanged
        If ddlAgree17.SelectedValue = "0" Then
            txtExplain17.Enabled = True
        Else
            txtExplain17.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree18_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree18.SelectedIndexChanged
        If ddlAgree18.SelectedValue = "0" Then
            txtExplain18.Enabled = True
        Else
            txtExplain18.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree19_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree19.SelectedIndexChanged
        If ddlAgree19.SelectedValue = "0" Then
            txtExplain19.Enabled = True
        Else
            txtExplain19.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree20_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree20.SelectedIndexChanged
        If ddlAgree20.SelectedValue = "0" Then
            txtExplain20.Enabled = True
        Else
            txtExplain20.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree21_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree21.SelectedIndexChanged
        If ddlAgree21.SelectedValue = "0" Then
            txtExplain21.Enabled = True
        Else
            txtExplain21.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree22_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree22.SelectedIndexChanged
        If ddlAgree22.SelectedValue = "0" Then
            txtExplain22.Enabled = True
        Else
            txtExplain22.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree23_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree23.SelectedIndexChanged
        If ddlAgree23.SelectedValue = "0" Then
            txtExplain23.Enabled = True
        Else
            txtExplain23.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree24_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree24.SelectedIndexChanged
        If ddlAgree24.SelectedValue = "0" Then
            txtExplain24.Enabled = True
        Else
            txtExplain24.Enabled = False
        End If
    End Sub

    Private Sub ddlAgree25_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAgree25.SelectedIndexChanged
        If ddlAgree25.SelectedValue = "0" Then
            txtExplain25.Enabled = True
        Else
            txtExplain25.Enabled = False
        End If
    End Sub
#End Region
#Region "TotalCash"

    Sub totalbox()

        Dim fulltot As Double = 0

        If Not IsNothing(lblTotal1.Text) And lblTotal1.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal1.Text)
        End If
        If Not IsNothing(lblTotal2.Text) And lblTotal2.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal2.Text)
        End If
        If Not IsNothing(lblTotal3.Text) And lblTotal3.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal3.Text)
        End If
        If Not IsNothing(lblTotal4.Text) And lblTotal4.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal4.Text)
        End If
        If Not IsNothing(lblTotal5.Text) And lblTotal5.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal5.Text)
        End If
        If Not IsNothing(lblTotal6.Text) And lblTotal6.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal6.Text)
        End If
        If Not IsNothing(lblTotal7.Text) And lblTotal7.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal7.Text)
        End If
        If Not IsNothing(lblTotal8.Text) And lblTotal8.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal8.Text)
        End If
        If Not IsNothing(lblTotal9.Text) And lblTotal9.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal9.Text)
        End If
        If Not IsNothing(lblTotal10.Text) And lblTotal10.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal10.Text)
        End If
        If Not IsNothing(lblTotal11.Text) And lblTotal11.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal11.Text)
        End If
        If Not IsNothing(lblTotal12.Text) And lblTotal12.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal12.Text)
        End If
        If Not IsNothing(lblTotal13.Text) And lblTotal13.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal13.Text)
        End If
        If Not IsNothing(lblTotal14.Text) And lblTotal14.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal14.Text)
        End If
        If Not IsNothing(lblTotal15.Text) And lblTotal15.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal15.Text)
        End If
        If Not IsNothing(lblTotal16.Text) And lblTotal16.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal16.Text)
        End If
        If Not IsNothing(lblTotal17.Text) And lblTotal17.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal17.Text)
        End If
        If Not IsNothing(lblTotal18.Text) And lblTotal18.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal18.Text)
        End If
        If Not IsNothing(lblTotal19.Text) And lblTotal19.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal19.Text)
        End If
        If Not IsNothing(lblTotal20.Text) And lblTotal20.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal20.Text)
        End If
        If Not IsNothing(lblTotal21.Text) And lblTotal21.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal21.Text)
        End If
        If Not IsNothing(lblTotal22.Text) And lblTotal22.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal22.Text)
        End If
        If Not IsNothing(lblTotal23.Text) And lblTotal23.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal23.Text)
        End If
        If Not IsNothing(lblTotal24.Text) And lblTotal24.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal24.Text)
        End If
        If Not IsNothing(lblTotal25.Text) And lblTotal25.Text <> "" Then
            fulltot = fulltot + CDbl(lblTotal25.Text)
        End If
        lblDepositTotal.Text = fulltot

    End Sub


    Private Sub txtCash1_TextChanged(sender As Object, e As EventArgs) Handles txtCash1.TextChanged
        If IsNumeric(txtCash1.Text) Then
            If IsNumeric(txtManual1.Text) Then
                lblTotal1.Text = CDbl(txtCash1.Text) + CDbl(txtManual1.Text)
            Else
                lblTotal1.Text = CDbl(txtCash1.Text)
            End If
        ElseIf Trim(txtCash1.Text) = "" Then
            If Trim(txtManual1.Text) = "" Then
                lblTotal1.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual1.Focus()

    End Sub

    Private Sub txtManual1_TextChanged(sender As Object, e As EventArgs) Handles txtManual1.TextChanged
        If IsNumeric(txtManual1.Text) Then
            If IsNumeric(txtCash1.Text) Then
                lblTotal1.Text = CDbl(txtCash1.Text) + CDbl(txtManual1.Text)
            Else
                lblTotal1.Text = CDbl(txtManual1.Text)
            End If
        ElseIf Trim(txtManual1.Text) = "" Then
            If Trim(txtCash1.Text) = "" Then
                lblTotal1.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash2.Focus()
    End Sub

    Private Sub txtCash2_TextChanged(sender As Object, e As EventArgs) Handles txtCash2.TextChanged
        If IsNumeric(txtCash2.Text) Then
            If IsNumeric(txtManual2.Text) Then
                lblTotal2.Text = CDbl(txtCash2.Text) + CDbl(txtManual2.Text)
            Else
                lblTotal2.Text = CDbl(txtCash2.Text)
            End If
        ElseIf Trim(txtCash2.Text) = "" Then
            If Trim(txtManual2.Text) = "" Then
                lblTotal2.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual2.Focus()

    End Sub

    Private Sub txtManual2_TextChanged(sender As Object, e As EventArgs) Handles txtManual2.TextChanged
        If IsNumeric(txtManual2.Text) Then
            If IsNumeric(txtCash2.Text) Then
                lblTotal2.Text = CDbl(txtCash2.Text) + CDbl(txtManual2.Text)
            Else
                lblTotal2.Text = CDbl(txtManual2.Text)
            End If
        ElseIf Trim(txtManual2.Text) = "" Then
            If Trim(txtCash2.Text) = "" Then
                lblTotal2.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash3.Focus()

    End Sub

    Private Sub txtCash3_TextChanged(sender As Object, e As EventArgs) Handles txtCash3.TextChanged
        If IsNumeric(txtCash3.Text) Then
            If IsNumeric(txtManual3.Text) Then
                lblTotal3.Text = CDbl(txtCash3.Text) + CDbl(txtManual3.Text)
            Else
                lblTotal3.Text = CDbl(txtCash3.Text)
            End If
        ElseIf Trim(txtCash3.Text) = "" Then
            If Trim(txtManual3.Text) = "" Then
                lblTotal3.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual3.Focus()

    End Sub

    Private Sub txtManual3_TextChanged(sender As Object, e As EventArgs) Handles txtManual3.TextChanged
        If IsNumeric(txtManual3.Text) Then
            If IsNumeric(txtCash3.Text) Then
                lblTotal3.Text = CDbl(txtCash3.Text) + CDbl(txtManual3.Text)
            Else
                lblTotal3.Text = CDbl(txtManual3.Text)
            End If
        ElseIf Trim(txtManual3.Text) = "" Then
            If Trim(txtCash3.Text) = "" Then
                lblTotal3.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash4.Focus()

    End Sub

    Private Sub txtCash4_TextChanged(sender As Object, e As EventArgs) Handles txtCash4.TextChanged
        If IsNumeric(txtCash4.Text) Then
            If IsNumeric(txtManual4.Text) Then
                lblTotal4.Text = CDbl(txtCash4.Text) + CDbl(txtManual4.Text)
            Else
                lblTotal4.Text = CDbl(txtCash4.Text)
            End If
        ElseIf Trim(txtCash4.Text) = "" Then
            If Trim(txtManual4.Text) = "" Then
                lblTotal4.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual4.Focus()

    End Sub

    Private Sub txtManual4_TextChanged(sender As Object, e As EventArgs) Handles txtManual4.TextChanged
        If IsNumeric(txtManual4.Text) Then
            If IsNumeric(txtCash4.Text) Then
                lblTotal4.Text = CDbl(txtCash4.Text) + CDbl(txtManual4.Text)
            Else
                lblTotal4.Text = CDbl(txtManual4.Text)
            End If
        ElseIf Trim(txtManual4.Text) = "" Then
            If Trim(txtCash4.Text) = "" Then
                lblTotal4.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash5.Focus()

    End Sub

    Private Sub txtCash5_TextChanged(sender As Object, e As EventArgs) Handles txtCash5.TextChanged
        If IsNumeric(txtCash5.Text) Then
            If IsNumeric(txtManual5.Text) Then
                lblTotal5.Text = CDbl(txtCash5.Text) + CDbl(txtManual5.Text)
            Else
                lblTotal5.Text = CDbl(txtCash5.Text)
            End If
        ElseIf Trim(txtCash5.Text) = "" Then
            If Trim(txtManual5.Text) = "" Then
                lblTotal5.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual5.Focus()

    End Sub

    Private Sub txtManual5_TextChanged(sender As Object, e As EventArgs) Handles txtManual5.TextChanged
        If IsNumeric(txtManual5.Text) Then
            If IsNumeric(txtCash5.Text) Then
                lblTotal5.Text = CDbl(txtCash5.Text) + CDbl(txtManual5.Text)
            Else
                lblTotal5.Text = CDbl(txtManual5.Text)
            End If
        ElseIf Trim(txtManual5.Text) = "" Then
            If Trim(txtCash5.Text) = "" Then
                lblTotal5.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash6.Focus()
    End Sub

    Private Sub txtCash6_TextChanged(sender As Object, e As EventArgs) Handles txtCash6.TextChanged
        If IsNumeric(txtCash6.Text) Then
            If IsNumeric(txtManual6.Text) Then
                lblTotal6.Text = CDbl(txtCash6.Text) + CDbl(txtManual6.Text)
            Else
                lblTotal6.Text = CDbl(txtCash6.Text)
            End If
        ElseIf Trim(txtCash6.Text) = "" Then
            If Trim(txtManual6.Text) = "" Then
                lblTotal6.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual6.Focus()

    End Sub

    Private Sub txtManual6_TextChanged(sender As Object, e As EventArgs) Handles txtManual6.TextChanged
        If IsNumeric(txtManual6.Text) Then
            If IsNumeric(txtCash6.Text) Then
                lblTotal6.Text = CDbl(txtCash6.Text) + CDbl(txtManual6.Text)
            Else
                lblTotal6.Text = CDbl(txtManual6.Text)
            End If
        ElseIf Trim(txtManual6.Text) = "" Then
            If Trim(txtCash6.Text) = "" Then
                lblTotal6.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash7.Focus()

    End Sub

    Private Sub txtCash7_TextChanged(sender As Object, e As EventArgs) Handles txtCash7.TextChanged
        If IsNumeric(txtCash7.Text) Then
            If IsNumeric(txtManual7.Text) Then
                lblTotal7.Text = CDbl(txtCash7.Text) + CDbl(txtManual7.Text)
            Else
                lblTotal7.Text = CDbl(txtCash7.Text)
            End If
        ElseIf Trim(txtCash7.Text) = "" Then
            If Trim(txtManual7.Text) = "" Then
                lblTotal7.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual7.Focus()

    End Sub

    Private Sub txtManual7_TextChanged(sender As Object, e As EventArgs) Handles txtManual7.TextChanged
        If IsNumeric(txtManual7.Text) Then
            If IsNumeric(txtCash7.Text) Then
                lblTotal7.Text = CDbl(txtCash7.Text) + CDbl(txtManual7.Text)
            Else
                lblTotal7.Text = CDbl(txtManual7.Text)
            End If
        ElseIf Trim(txtManual7.Text) = "" Then
            If Trim(txtCash7.Text) = "" Then
                lblTotal7.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash8.Focus()

    End Sub

    Private Sub txtCash8_TextChanged(sender As Object, e As EventArgs) Handles txtCash8.TextChanged
        If IsNumeric(txtCash8.Text) Then
            If IsNumeric(txtManual8.Text) Then
                lblTotal8.Text = CDbl(txtCash8.Text) + CDbl(txtManual8.Text)
            Else
                lblTotal8.Text = CDbl(txtCash8.Text)
            End If
        ElseIf Trim(txtCash8.Text) = "" Then
            If Trim(txtManual8.Text) = "" Then
                lblTotal8.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual8.Focus()

    End Sub

    Private Sub txtManual8_TextChanged(sender As Object, e As EventArgs) Handles txtManual8.TextChanged
        If IsNumeric(txtManual8.Text) Then
            If IsNumeric(txtCash8.Text) Then
                lblTotal8.Text = CDbl(txtCash8.Text) + CDbl(txtManual8.Text)
            Else
                lblTotal8.Text = CDbl(txtManual8.Text)
            End If
        ElseIf Trim(txtManual8.Text) = "" Then
            If Trim(txtCash8.Text) = "" Then
                lblTotal8.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash9.Focus()

    End Sub

    Private Sub txtCash9_TextChanged(sender As Object, e As EventArgs) Handles txtCash9.TextChanged
        If IsNumeric(txtCash9.Text) Then
            If IsNumeric(txtManual9.Text) Then
                lblTotal9.Text = CDbl(txtCash9.Text) + CDbl(txtManual9.Text)
            Else
                lblTotal9.Text = CDbl(txtCash9.Text)
            End If
        ElseIf Trim(txtCash9.Text) = "" Then
            If Trim(txtManual9.Text) = "" Then
                lblTotal9.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual9.Focus()

    End Sub

    Private Sub txtManual9_TextChanged(sender As Object, e As EventArgs) Handles txtManual9.TextChanged
        If IsNumeric(txtManual9.Text) Then
            If IsNumeric(txtCash9.Text) Then
                lblTotal9.Text = CDbl(txtCash9.Text) + CDbl(txtManual9.Text)
            Else
                lblTotal9.Text = CDbl(txtManual9.Text)
            End If
        ElseIf Trim(txtManual9.Text) = "" Then
            If Trim(txtCash9.Text) = "" Then
                lblTotal9.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash10.Focus()

    End Sub

    Private Sub txtCash10_TextChanged(sender As Object, e As EventArgs) Handles txtCash10.TextChanged
        If IsNumeric(txtCash10.Text) Then
            If IsNumeric(txtManual10.Text) Then
                lblTotal10.Text = CDbl(txtCash10.Text) + CDbl(txtManual10.Text)
            Else
                lblTotal10.Text = CDbl(txtCash10.Text)
            End If
        ElseIf Trim(txtCash10.Text) = "" Then
            If Trim(txtManual10.Text) = "" Then
                lblTotal10.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual10.Focus()

    End Sub

    Private Sub txtManual10_TextChanged(sender As Object, e As EventArgs) Handles txtManual10.TextChanged
        If IsNumeric(txtManual10.Text) Then
            If IsNumeric(txtCash10.Text) Then
                lblTotal10.Text = CDbl(txtCash10.Text) + CDbl(txtManual10.Text)
            Else
                lblTotal10.Text = CDbl(txtManual10.Text)
            End If
        ElseIf Trim(txtManual10.Text) = "" Then
            If Trim(txtCash10.Text) = "" Then
                lblTotal10.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash11.Focus()


    End Sub

    Private Sub txtCash11_TextChanged(sender As Object, e As EventArgs) Handles txtCash11.TextChanged
        If IsNumeric(txtCash11.Text) Then
            If IsNumeric(txtManual11.Text) Then
                lblTotal11.Text = CDbl(txtCash11.Text) + CDbl(txtManual11.Text)
            Else
                lblTotal11.Text = CDbl(txtCash11.Text)
            End If
        ElseIf Trim(txtCash11.Text) = "" Then
            If Trim(txtManual11.Text) = "" Then
                lblTotal11.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual11.Focus()

    End Sub

    Private Sub txtManual11_TextChanged(sender As Object, e As EventArgs) Handles txtManual11.TextChanged
        If IsNumeric(txtManual11.Text) Then
            If IsNumeric(txtCash11.Text) Then
                lblTotal11.Text = CDbl(txtCash11.Text) + CDbl(txtManual11.Text)
            Else
                lblTotal11.Text = CDbl(txtManual11.Text)
            End If
        ElseIf Trim(txtManual11.Text) = "" Then
            If Trim(txtCash11.Text) = "" Then
                lblTotal11.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash12.Focus()
    End Sub

    Private Sub txtCash12_TextChanged(sender As Object, e As EventArgs) Handles txtCash12.TextChanged
        If IsNumeric(txtCash12.Text) Then
            If IsNumeric(txtManual12.Text) Then
                lblTotal12.Text = CDbl(txtCash12.Text) + CDbl(txtManual12.Text)
            Else
                lblTotal12.Text = CDbl(txtCash12.Text)
            End If
        ElseIf Trim(txtCash12.Text) = "" Then
            If Trim(txtManual12.Text) = "" Then
                lblTotal12.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual12.Focus()

    End Sub

    Private Sub txtManual12_TextChanged(sender As Object, e As EventArgs) Handles txtManual12.TextChanged
        If IsNumeric(txtManual12.Text) Then
            If IsNumeric(txtCash12.Text) Then
                lblTotal12.Text = CDbl(txtCash12.Text) + CDbl(txtManual12.Text)
            Else
                lblTotal12.Text = CDbl(txtManual12.Text)
            End If
        ElseIf Trim(txtManual12.Text) = "" Then
            If Trim(txtCash12.Text) = "" Then
                lblTotal12.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash13.Focus()

    End Sub

    Private Sub txtCash13_TextChanged(sender As Object, e As EventArgs) Handles txtCash13.TextChanged
        If IsNumeric(txtCash13.Text) Then
            If IsNumeric(txtManual13.Text) Then
                lblTotal13.Text = CDbl(txtCash13.Text) + CDbl(txtManual13.Text)
            Else
                lblTotal13.Text = CDbl(txtCash13.Text)
            End If
        ElseIf Trim(txtCash13.Text) = "" Then
            If Trim(txtManual13.Text) = "" Then
                lblTotal13.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual13.Focus()

    End Sub

    Private Sub txtManual13_TextChanged(sender As Object, e As EventArgs) Handles txtManual13.TextChanged
        If IsNumeric(txtManual13.Text) Then
            If IsNumeric(txtCash13.Text) Then
                lblTotal13.Text = CDbl(txtCash13.Text) + CDbl(txtManual13.Text)
            Else
                lblTotal13.Text = CDbl(txtManual13.Text)
            End If
        ElseIf Trim(txtManual13.Text) = "" Then
            If Trim(txtCash13.Text) = "" Then
                lblTotal13.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash14.Focus()

    End Sub

    Private Sub txtCash14_TextChanged(sender As Object, e As EventArgs) Handles txtCash14.TextChanged
        If IsNumeric(txtCash14.Text) Then
            If IsNumeric(txtManual14.Text) Then
                lblTotal14.Text = CDbl(txtCash14.Text) + CDbl(txtManual14.Text)
            Else
                lblTotal14.Text = CDbl(txtCash14.Text)
            End If
        ElseIf Trim(txtCash14.Text) = "" Then
            If Trim(txtManual14.Text) = "" Then
                lblTotal14.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual14.Focus()

    End Sub

    Private Sub txtManual14_TextChanged(sender As Object, e As EventArgs) Handles txtManual14.TextChanged
        If IsNumeric(txtManual14.Text) Then
            If IsNumeric(txtCash14.Text) Then
                lblTotal14.Text = CDbl(txtCash14.Text) + CDbl(txtManual14.Text)
            Else
                lblTotal14.Text = CDbl(txtManual14.Text)
            End If
        ElseIf Trim(txtManual14.Text) = "" Then
            If Trim(txtCash14.Text) = "" Then
                lblTotal14.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash15.Focus()

    End Sub

    Private Sub txtCash15_TextChanged(sender As Object, e As EventArgs) Handles txtCash15.TextChanged
        If IsNumeric(txtCash15.Text) Then
            If IsNumeric(txtManual15.Text) Then
                lblTotal15.Text = CDbl(txtCash15.Text) + CDbl(txtManual15.Text)
            Else
                lblTotal15.Text = CDbl(txtCash15.Text)
            End If
        ElseIf Trim(txtCash15.Text) = "" Then
            If Trim(txtManual15.Text) = "" Then
                lblTotal15.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual15.Focus()

    End Sub

    Private Sub txtManual15_TextChanged(sender As Object, e As EventArgs) Handles txtManual15.TextChanged
        If IsNumeric(txtManual15.Text) Then
            If IsNumeric(txtCash15.Text) Then
                lblTotal15.Text = CDbl(txtCash15.Text) + CDbl(txtManual15.Text)
            Else
                lblTotal15.Text = CDbl(txtManual15.Text)
            End If
        ElseIf Trim(txtManual15.Text) = "" Then
            If Trim(txtCash15.Text) = "" Then
                lblTotal15.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash16.Focus()

    End Sub

    Private Sub txtCash16_TextChanged(sender As Object, e As EventArgs) Handles txtCash16.TextChanged
        If IsNumeric(txtCash16.Text) Then
            If IsNumeric(txtManual16.Text) Then
                lblTotal16.Text = CDbl(txtCash16.Text) + CDbl(txtManual16.Text)
            Else
                lblTotal16.Text = CDbl(txtCash16.Text)
            End If
        ElseIf Trim(txtCash16.Text) = "" Then
            If Trim(txtManual16.Text) = "" Then
                lblTotal16.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual16.Focus()

    End Sub

    Private Sub txtManual16_TextChanged(sender As Object, e As EventArgs) Handles txtManual16.TextChanged
        If IsNumeric(txtManual16.Text) Then
            If IsNumeric(txtCash16.Text) Then
                lblTotal16.Text = CDbl(txtCash16.Text) + CDbl(txtManual16.Text)
            Else
                lblTotal16.Text = CDbl(txtManual16.Text)
            End If
        ElseIf Trim(txtManual16.Text) = "" Then
            If Trim(txtCash16.Text) = "" Then
                lblTotal16.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash17.Focus()

    End Sub

    Private Sub txtCash17_TextChanged(sender As Object, e As EventArgs) Handles txtCash17.TextChanged
        If IsNumeric(txtCash17.Text) Then
            If IsNumeric(txtManual17.Text) Then
                lblTotal17.Text = CDbl(txtCash17.Text) + CDbl(txtManual17.Text)
            Else
                lblTotal17.Text = CDbl(txtCash17.Text)
            End If
        ElseIf Trim(txtCash17.Text) = "" Then
            If Trim(txtManual17.Text) = "" Then
                lblTotal17.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual17.Focus()

    End Sub

    Private Sub txtManual17_TextChanged(sender As Object, e As EventArgs) Handles txtManual17.TextChanged
        If IsNumeric(txtManual17.Text) Then
            If IsNumeric(txtCash17.Text) Then
                lblTotal17.Text = CDbl(txtCash17.Text) + CDbl(txtManual17.Text)
            Else
                lblTotal17.Text = CDbl(txtManual17.Text)
            End If
        ElseIf Trim(txtManual17.Text) = "" Then
            If Trim(txtCash17.Text) = "" Then
                lblTotal17.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash18.Focus()

    End Sub

    Private Sub txtCash18_TextChanged(sender As Object, e As EventArgs) Handles txtCash18.TextChanged
        If IsNumeric(txtCash18.Text) Then
            If IsNumeric(txtManual18.Text) Then
                lblTotal18.Text = CDbl(txtCash18.Text) + CDbl(txtManual18.Text)
            Else
                lblTotal18.Text = CDbl(txtCash18.Text)
            End If
        ElseIf Trim(txtCash18.Text) = "" Then
            If Trim(txtManual18.Text) = "" Then
                lblTotal18.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual18.Focus()

    End Sub

    Private Sub txtManual18_TextChanged(sender As Object, e As EventArgs) Handles txtManual18.TextChanged
        If IsNumeric(txtManual18.Text) Then
            If IsNumeric(txtCash18.Text) Then
                lblTotal18.Text = CDbl(txtCash18.Text) + CDbl(txtManual18.Text)
            Else
                lblTotal18.Text = CDbl(txtManual18.Text)
            End If
        ElseIf Trim(txtManual18.Text) = "" Then
            If Trim(txtCash18.Text) = "" Then
                lblTotal18.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash19.Focus()

    End Sub

    Private Sub txtCash19_TextChanged(sender As Object, e As EventArgs) Handles txtCash19.TextChanged
        If IsNumeric(txtCash19.Text) Then
            If IsNumeric(txtManual19.Text) Then
                lblTotal19.Text = CDbl(txtCash19.Text) + CDbl(txtManual19.Text)
            Else
                lblTotal19.Text = CDbl(txtCash19.Text)
            End If
        ElseIf Trim(txtCash19.Text) = "" Then
            If Trim(txtManual19.Text) = "" Then
                lblTotal19.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual19.Focus()

    End Sub

    Private Sub txtManual19_TextChanged(sender As Object, e As EventArgs) Handles txtManual19.TextChanged
        If IsNumeric(txtManual19.Text) Then
            If IsNumeric(txtCash19.Text) Then
                lblTotal19.Text = CDbl(txtCash19.Text) + CDbl(txtManual19.Text)
            Else
                lblTotal19.Text = CDbl(txtManual19.Text)
            End If
        ElseIf Trim(txtManual19.Text) = "" Then
            If Trim(txtCash19.Text) = "" Then
                lblTotal19.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash20.Focus()

    End Sub

    Private Sub txtCash20_TextChanged(sender As Object, e As EventArgs) Handles txtCash20.TextChanged
        If IsNumeric(txtCash20.Text) Then
            If IsNumeric(txtManual20.Text) Then
                lblTotal20.Text = CDbl(txtCash20.Text) + CDbl(txtManual20.Text)
            Else
                lblTotal20.Text = CDbl(txtCash20.Text)
            End If
        ElseIf Trim(txtCash20.Text) = "" Then
            If Trim(txtManual20.Text) = "" Then
                lblTotal20.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual20.Focus()

    End Sub

    Private Sub txtManual20_TextChanged(sender As Object, e As EventArgs) Handles txtManual20.TextChanged
        If IsNumeric(txtManual20.Text) Then
            If IsNumeric(txtCash20.Text) Then
                lblTotal20.Text = CDbl(txtCash20.Text) + CDbl(txtManual20.Text)
            Else
                lblTotal20.Text = CDbl(txtManual20.Text)
            End If
        ElseIf Trim(txtManual20.Text) = "" Then
            If Trim(txtCash20.Text) = "" Then
                lblTotal20.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash21.Focus()

    End Sub

    Private Sub txtCash21_TextChanged(sender As Object, e As EventArgs) Handles txtCash21.TextChanged
        If IsNumeric(txtCash21.Text) Then
            If IsNumeric(txtManual21.Text) Then
                lblTotal21.Text = CDbl(txtCash21.Text) + CDbl(txtManual21.Text)
            Else
                lblTotal21.Text = CDbl(txtCash21.Text)
            End If
        ElseIf Trim(txtCash21.Text) = "" Then
            If Trim(txtManual21.Text) = "" Then
                lblTotal21.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual21.Focus()

    End Sub

    Private Sub txtManual21_TextChanged(sender As Object, e As EventArgs) Handles txtManual21.TextChanged
        If IsNumeric(txtManual21.Text) Then
            If IsNumeric(txtCash21.Text) Then
                lblTotal21.Text = CDbl(txtCash21.Text) + CDbl(txtManual21.Text)
            Else
                lblTotal21.Text = CDbl(txtManual21.Text)
            End If
        ElseIf Trim(txtManual21.Text) = "" Then
            If Trim(txtCash21.Text) = "" Then
                lblTotal21.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash22.Focus()

    End Sub

    Private Sub txtCash22_TextChanged(sender As Object, e As EventArgs) Handles txtCash22.TextChanged
        If IsNumeric(txtCash22.Text) Then
            If IsNumeric(txtManual22.Text) Then
                lblTotal22.Text = CDbl(txtCash22.Text) + CDbl(txtManual22.Text)
            Else
                lblTotal22.Text = CDbl(txtCash22.Text)
            End If
        ElseIf Trim(txtCash22.Text) = "" Then
            If Trim(txtManual22.Text) = "" Then
                lblTotal22.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual22.Focus()

    End Sub

    Private Sub txtManual22_TextChanged(sender As Object, e As EventArgs) Handles txtManual22.TextChanged
        If IsNumeric(txtManual22.Text) Then
            If IsNumeric(txtCash22.Text) Then
                lblTotal22.Text = CDbl(txtCash22.Text) + CDbl(txtManual22.Text)
            Else
                lblTotal22.Text = CDbl(txtManual22.Text)
            End If
        ElseIf Trim(txtManual22.Text) = "" Then
            If Trim(txtCash22.Text) = "" Then
                lblTotal22.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash23.Focus()

    End Sub

    Private Sub txtCash23_TextChanged(sender As Object, e As EventArgs) Handles txtCash23.TextChanged
        If IsNumeric(txtCash23.Text) Then
            If IsNumeric(txtManual23.Text) Then
                lblTotal23.Text = CDbl(txtCash23.Text) + CDbl(txtManual23.Text)
            Else
                lblTotal23.Text = CDbl(txtCash23.Text)
            End If
        ElseIf Trim(txtCash23.Text) = "" Then
            If Trim(txtManual23.Text) = "" Then
                lblTotal23.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual23.Focus()

    End Sub

    Private Sub txtManual23_TextChanged(sender As Object, e As EventArgs) Handles txtManual23.TextChanged
        If IsNumeric(txtManual23.Text) Then
            If IsNumeric(txtCash23.Text) Then
                lblTotal23.Text = CDbl(txtCash23.Text) + CDbl(txtManual23.Text)
            Else
                lblTotal23.Text = CDbl(txtManual23.Text)
            End If
        ElseIf Trim(txtManual23.Text) = "" Then
            If Trim(txtCash23.Text) = "" Then
                lblTotal23.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash24.Focus()

    End Sub

    Private Sub txtCash24_TextChanged(sender As Object, e As EventArgs) Handles txtCash24.TextChanged
        If IsNumeric(txtCash24.Text) Then
            If IsNumeric(txtManual24.Text) Then
                lblTotal24.Text = CDbl(txtCash24.Text) + CDbl(txtManual24.Text)
            Else
                lblTotal24.Text = CDbl(txtCash24.Text)
            End If
        ElseIf Trim(txtCash24.Text) = "" Then
            If Trim(txtManual24.Text) = "" Then
                lblTotal24.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual24.Focus()

    End Sub

    Private Sub txtManual24_TextChanged(sender As Object, e As EventArgs) Handles txtManual24.TextChanged
        If IsNumeric(txtManual24.Text) Then
            If IsNumeric(txtCash24.Text) Then
                lblTotal24.Text = CDbl(txtCash24.Text) + CDbl(txtManual24.Text)
            Else
                lblTotal24.Text = CDbl(txtManual24.Text)
            End If
        ElseIf Trim(txtManual24.Text) = "" Then
            If Trim(txtCash24.Text) = "" Then
                lblTotal24.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtCash25.Focus()

    End Sub

    Private Sub txtCash25_TextChanged(sender As Object, e As EventArgs) Handles txtCash25.TextChanged
        If IsNumeric(txtCash25.Text) Then
            If IsNumeric(txtManual25.Text) Then
                lblTotal25.Text = CDbl(txtCash25.Text) + CDbl(txtManual25.Text)
            Else
                lblTotal25.Text = CDbl(txtCash25.Text)
            End If
        ElseIf Trim(txtCash25.Text) = "" Then
            If Trim(txtManual25.Text) = "" Then
                lblTotal25.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Cash"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()
        txtManual25.Focus()

    End Sub

    Private Sub txtManual25_TextChanged(sender As Object, e As EventArgs) Handles txtManual25.TextChanged
        If IsNumeric(txtManual25.Text) Then
            If IsNumeric(txtCash25.Text) Then
                lblTotal25.Text = CDbl(txtCash25.Text) + CDbl(txtManual25.Text)
            Else
                lblTotal25.Text = CDbl(txtManual25.Text)
            End If
        ElseIf Trim(txtManual25.Text) = "" Then
            If Trim(txtCash25.Text) = "" Then
                lblTotal25.Text = 0
            End If
        Else
            explantionlabel.Text = "Please enter a numeric value for Manual Checks"
            explantionlabel.DataBind()
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
        End If

        totalbox()

    End Sub


#End Region

    Sub ResetSubmissionPage()

        txtDate1.Text = ""
        txtDate2.Text = ""
        txtDate3.Text = ""
        txtDate4.Text = ""
        txtDate5.Text = ""
        txtDate6.Text = ""
        txtDate7.Text = ""
        txtDate8.Text = ""
        txtDate9.Text = ""
        txtDate10.Text = ""
        txtDate11.Text = ""
        txtDate12.Text = ""
        txtDate13.Text = ""
        txtDate14.Text = ""
        txtDate15.Text = ""
        txtDate16.Text = ""
        txtDate17.Text = ""
        txtDate18.Text = ""
        txtDate19.Text = ""
        txtDate20.Text = ""
        txtDate21.Text = ""
        txtDate22.Text = ""
        txtDate23.Text = ""
        txtDate24.Text = ""
        txtDate25.Text = ""
        ddlOutlet1.SelectedIndex = 0
        ddlOutlet2.SelectedIndex = 0
        ddlOutlet3.SelectedIndex = 0
        ddlOutlet4.SelectedIndex = 0
        ddlOutlet5.SelectedIndex = 0
        ddlOutlet6.SelectedIndex = 0
        ddlOutlet7.SelectedIndex = 0
        ddlOutlet8.SelectedIndex = 0
        ddlOutlet9.SelectedIndex = 0
        ddlOutlet10.SelectedIndex = 0
        ddlOutlet11.SelectedIndex = 0
        ddlOutlet12.SelectedIndex = 0
        ddlOutlet13.SelectedIndex = 0
        ddlOutlet14.SelectedIndex = 0
        ddlOutlet15.SelectedIndex = 0
        ddlOutlet16.SelectedIndex = 0
        ddlOutlet17.SelectedIndex = 0
        ddlOutlet18.SelectedIndex = 0
        ddlOutlet19.SelectedIndex = 0
        ddlOutlet20.SelectedIndex = 0
        ddlOutlet21.SelectedIndex = 0
        ddlOutlet22.SelectedIndex = 0
        ddlOutlet23.SelectedIndex = 0
        ddlOutlet24.SelectedIndex = 0
        ddlOutlet25.SelectedIndex = 0
        txtCash1.Text = ""
        txtCash2.Text = ""
        txtCash3.Text = ""
        txtCash4.Text = ""
        txtCash5.Text = ""
        txtCash6.Text = ""
        txtCash7.Text = ""
        txtCash8.Text = ""
        txtCash9.Text = ""
        txtCash10.Text = ""
        txtCash11.Text = ""
        txtCash12.Text = ""
        txtCash13.Text = ""
        txtCash14.Text = ""
        txtCash15.Text = ""
        txtCash16.Text = ""
        txtCash17.Text = ""
        txtCash18.Text = ""
        txtCash19.Text = ""
        txtCash20.Text = ""
        txtCash21.Text = ""
        txtCash22.Text = ""
        txtCash23.Text = ""
        txtCash24.Text = ""
        txtCash25.Text = ""
        txtManual1.Text = ""
        txtManual2.Text = ""
        txtManual3.Text = ""
        txtManual4.Text = ""
        txtManual5.Text = ""
        txtManual6.Text = ""
        txtManual7.Text = ""
        txtManual8.Text = ""
        txtManual9.Text = ""
        txtManual10.Text = ""
        txtManual11.Text = ""
        txtManual12.Text = ""
        txtManual13.Text = ""
        txtManual14.Text = ""
        txtManual15.Text = ""
        txtManual16.Text = ""
        txtManual17.Text = ""
        txtManual18.Text = ""
        txtManual19.Text = ""
        txtManual20.Text = ""
        txtManual21.Text = ""
        txtManual22.Text = ""
        txtManual23.Text = ""
        txtManual24.Text = ""
        txtManual25.Text = ""
        ddlAgree1.SelectedIndex = 0
        ddlAgree2.SelectedIndex = 0
        ddlAgree3.SelectedIndex = 0
        ddlAgree4.SelectedIndex = 0
        ddlAgree5.SelectedIndex = 0
        ddlAgree6.SelectedIndex = 0
        ddlAgree7.SelectedIndex = 0
        ddlAgree8.SelectedIndex = 0
        ddlAgree9.SelectedIndex = 0
        ddlAgree10.SelectedIndex = 0
        ddlAgree11.SelectedIndex = 0
        ddlAgree12.SelectedIndex = 0
        ddlAgree13.SelectedIndex = 0
        ddlAgree14.SelectedIndex = 0
        ddlAgree15.SelectedIndex = 0
        ddlAgree16.SelectedIndex = 0
        ddlAgree17.SelectedIndex = 0
        ddlAgree18.SelectedIndex = 0
        ddlAgree19.SelectedIndex = 0
        ddlAgree20.SelectedIndex = 0
        ddlAgree21.SelectedIndex = 0
        ddlAgree22.SelectedIndex = 0
        ddlAgree23.SelectedIndex = 0
        ddlAgree24.SelectedIndex = 0
        ddlAgree25.SelectedIndex = 0
        txtExplain1.Text = ""
        txtExplain2.Text = ""
        txtExplain3.Text = ""
        txtExplain4.Text = ""
        txtExplain5.Text = ""
        txtExplain6.Text = ""
        txtExplain7.Text = ""
        txtExplain8.Text = ""
        txtExplain9.Text = ""
        txtExplain10.Text = ""
        txtExplain11.Text = ""
        txtExplain12.Text = ""
        txtExplain13.Text = ""
        txtExplain14.Text = ""
        txtExplain15.Text = ""
        txtExplain16.Text = ""
        txtExplain17.Text = ""
        txtExplain18.Text = ""
        txtExplain19.Text = ""
        txtExplain20.Text = ""
        txtExplain21.Text = ""
        txtExplain22.Text = ""
        txtExplain23.Text = ""
        txtExplain24.Text = ""
        txtExplain25.Text = ""
        txtExplain1.Enabled = False
        txtExplain2.Enabled = False
        txtExplain3.Enabled = False
        txtExplain4.Enabled = False
        txtExplain5.Enabled = False
        txtExplain6.Enabled = False
        txtExplain7.Enabled = False
        txtExplain8.Enabled = False
        txtExplain9.Enabled = False
        txtExplain10.Enabled = False
        txtExplain11.Enabled = False
        txtExplain12.Enabled = False
        txtExplain13.Enabled = False
        txtExplain14.Enabled = False
        txtExplain15.Enabled = False
        txtExplain16.Enabled = False
        txtExplain17.Enabled = False
        txtExplain18.Enabled = False
        txtExplain19.Enabled = False
        txtExplain20.Enabled = False
        txtExplain21.Enabled = False
        txtExplain22.Enabled = False
        txtExplain23.Enabled = False
        txtExplain24.Enabled = False
        txtExplain25.Enabled = False

        txtDepositBag.Text = ""
        txtDepositSlip.Text = ""
        lblDepositTotal.Text = ""
        lblTotal1.Text = ""
        lblTotal2.Text = ""
        lblTotal3.Text = ""
        lblTotal4.Text = ""
        lblTotal5.Text = ""
        lblTotal6.Text = ""
        lblTotal7.Text = ""
        lblTotal8.Text = ""
        lblTotal9.Text = ""
        lblTotal10.Text = ""
        lblTotal11.Text = ""
        lblTotal12.Text = ""
        lblTotal13.Text = ""
        lblTotal14.Text = ""
        lblTotal15.Text = ""
        lblTotal16.Text = ""
        lblTotal17.Text = ""
        lblTotal18.Text = ""
        lblTotal19.Text = ""
        lblTotal20.Text = ""
        lblTotal21.Text = ""
        lblTotal22.Text = ""
        lblTotal23.Text = ""
        lblTotal24.Text = ""
        lblTotal25.Text = ""


    End Sub

    'Private Sub btnSubmitBag_Click(sender As Object, e As EventArgs) Handles btnSubmitBag.Click

    '    Dim getnewDepID As String = "Select isnull(max(DepositBagID),0) from DWH.WF.PPS_Submissions"
    '    Dim ds As DataSet
    '    Dim da As SqlDataAdapter
    '    Dim newDepID As Integer
    '    Dim cmd As SqlCommand
    '    Dim subrws As Integer = 0

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '        cmd = New SqlCommand(getnewDepID, conn)
    '        cmd.CommandTimeout = 86400
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        newDepID = cmd.ExecuteScalar + 1

    '    End Using

    '    Dim insertsql As String = ""

    '    If lblDepositDate.Text <> "" And IsDate(lblDepositDate.Text) Then

    '        If txtDate1.Text <> "" And IsDate(txtDate1.Text) Then
    '            insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
    '            If ddlAgree1.SelectedValue = "0" Then
    '                insertsql += "Explain, "
    '            End If
    '            insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (" & CStr(newDepID) & ", '" & Replace(txtDepositBag.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
    '                Replace(txtDate1.Text, "'", "''") & "', '" & ddlOutlet1.SelectedValue & "', '"
    '            If IsNumeric(txtCash1.Text) Then
    '                insertsql += txtCash1.Text & "', '"
    '            Else
    '                insertsql += "0', '"
    '            End If
    '            If IsNumeric(txtManual1.Text) Then
    '                insertsql += txtManual1.Text & "', '"
    '            Else
    '                insertsql += "0', '"
    '            End If
    '            If ddlAgree1.SelectedValue = "0" Then
    '                insertsql += "0', '" & txtExplain1.Text & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate())"
    '            Else
    '                insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate())"
    '            End If

    '            subrws += 1

    '        End If



    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '            If conn.State = ConnectionState.Closed Then
    '                conn.Open()
    '            End If

    '            cmd = New System.Data.SqlClient.SqlCommand(insertsql, conn)
    '            cmd.ExecuteNonQuery()
    '            explantionlabel.Text = "Successfully Submitted Bag (" & CStr(subrws) & " rows)"
    '            explantionlabel.DataBind()
    '            ModalPopupExtender1.Show()
    '        End Using

    '    Else
    '        explantionlabel.Text = "Deposit Date is not valid; Bag not submitted"
    '        ModalPopupExtender1.Show()
    '    End If

    'End Sub

    Private Sub btnSubmitTheBag_Click(sender As Object, e As EventArgs) Handles btnSubmitTheBag.Click
        Try

            Dim futuredates As Integer = 0
            Dim getnewDepID As String = "Select isnull(max(DepositBagID),0) from DWH.WF.PPS_Submissions"
            Dim subrws As Integer = 0

            ' 10/2/2017 -- CRW; created two bags on one DepositBagID.  
            'Dim newDepID As Integer
            'Dim cmd As SqlCommand
            '

            'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
            '    cmd = New SqlCommand(getnewDepID, conn)
            '    cmd.CommandTimeout = 86400
            '    If conn.State = ConnectionState.Closed Then
            '        conn.Open()
            '    End If
            '    newDepID = cmd.ExecuteScalar + 1

            'End Using

            Dim insertsql As String = "declare @DepID int = (Select isnull(max(DepositBagID),0) + 1 from DWH.WF.PPS_Submissions) "

            Dim rgx As New Regex("[a-zA-Z0-9]")
            Dim rgx2 As New Regex("[0-9]")
            If Len(txtDepositBag.Text) > 25 Then
                explantionlabel.Text = "Deposit Bag Name currently limited to 25 characters."
                explantionlabel.DataBind()
                btnOptInstructions.Visible = False
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf Len(Trim(txtDepositBag.Text)) = 0 Then
                explantionlabel.Text = "Deposit Bag Name Required."
                explantionlabel.DataBind()
                btnOptInstructions.Visible = False
                ModalPopupExtender1.Show()
                Exit Sub
            Else
                For Each ltr As Char In txtDepositBag.Text
                    If (rgx.IsMatch(ltr)) Then
                    Else
                        explantionlabel.Text = "Deposit Bag Name must be alpha-numeric."
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                Next

            End If

            If Len(txtDepositSlip.Text) > 5 Then
                explantionlabel.Text = "Deposit Slip Number currently limited to 5 numeric digits.  <br><br>"
                explantionlabel.DataBind()
                ModalPopupExtender1.Show()
                btnOptInstructions.Visible = True
                Exit Sub
            ElseIf Len(Trim(txtDepositSlip.Text)) = 0 Then
                explantionlabel.Text = "Deposit Slip Number Required.  <br><br>"
                explantionlabel.DataBind()
                ModalPopupExtender1.Show()
                btnOptInstructions.Visible = True
                Exit Sub
            Else
                For Each ltr As Char In txtDepositSlip.Text
                    If (rgx2.IsMatch(ltr)) Then
                    Else
                        explantionlabel.Text = "Deposit Slip Number must be numeric.  "
                        explantionlabel.DataBind()
                        ModalPopupExtender1.Show()
                        btnOptInstructions.Visible = True
                        Exit Sub
                    End If
                Next
            End If

            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim x As Decimal

            If ddlDepositLoc.SelectedValue <> " -- noneselected -- " Then

                If txtDate1.Text <> "" Then
                    If IsDate(txtDate1.Text) Then
                        If CDate(txtDate1.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 1 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate1.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet1.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 1"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree1.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate1.Text, "'", "''") & "', '" & ddlOutlet1.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash1.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual1.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash1.Text) Then
                        '    insertsql += Replace(txtCash1.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual1.Text) Then
                        '    insertsql += Replace(txtManual1.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree1.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain1.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 1"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate2.Text <> "" Then
                    If IsDate(txtDate2.Text) Then
                        If CDate(txtDate2.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 2 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate2.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet2.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 2"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree2.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate2.Text, "'", "''") & "', '" & ddlOutlet2.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash2.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual2.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash2.Text) Then
                        '    insertsql += Replace(txtCash2.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual2.Text) Then
                        '    insertsql += Replace(txtManual2.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree2.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain2.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 2"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If


                If txtDate3.Text <> "" Then
                    If IsDate(txtDate3.Text) Then
                        If CDate(txtDate3.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 3 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate3.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet3.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 3"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree3.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate3.Text, "'", "''") & "', '" & ddlOutlet3.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash3.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual3.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash3.Text) Then
                        '    insertsql += Replace(txtCash3.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual3.Text) Then
                        '    insertsql += Replace(txtManual3.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree3.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain3.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 3"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate4.Text <> "" Then
                    If IsDate(txtDate4.Text) Then
                        If CDate(txtDate4.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 4 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate4.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet4.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 4"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree4.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate4.Text, "'", "''") & "', '" & ddlOutlet4.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash4.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual4.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash4.Text) Then
                        '    insertsql += Replace(txtCash4.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual4.Text) Then
                        '    insertsql += Replace(txtManual4.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree4.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain4.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 4"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate5.Text <> "" Then
                    If IsDate(txtDate5.Text) Then
                        If CDate(txtDate5.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 5 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate5.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet5.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 5"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree5.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate5.Text, "'", "''") & "', '" & ddlOutlet5.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash5.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual5.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash5.Text) Then
                        '    insertsql += Replace(txtCash5.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual5.Text) Then
                        '    insertsql += Replace(txtManual5.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree5.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain5.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 5"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate6.Text <> "" Then
                    If IsDate(txtDate6.Text) Then
                        If CDate(txtDate6.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 6 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate6.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet6.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 6"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree6.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate6.Text, "'", "''") & "', '" & ddlOutlet6.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash6.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual6.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash6.Text) Then
                        '    insertsql += Replace(txtCash6.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual6.Text) Then
                        '    insertsql += Replace(txtManual6.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree6.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain6.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 6"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate7.Text <> "" Then
                    If IsDate(txtDate7.Text) Then
                        If CDate(txtDate7.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 7 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate7.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet7.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 7"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree7.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate7.Text, "'", "''") & "', '" & ddlOutlet7.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash7.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual7.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash7.Text) Then
                        '    insertsql += Replace(txtCash7.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual7.Text) Then
                        '    insertsql += Replace(txtManual7.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree7.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain7.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 7"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate8.Text <> "" Then
                    If IsDate(txtDate8.Text) Then
                        If CDate(txtDate8.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 8 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate8.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet8.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 8"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree8.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate8.Text, "'", "''") & "', '" & ddlOutlet8.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash8.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual8.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash8.Text) Then
                        '    insertsql += Replace(txtCash8.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual8.Text) Then
                        '    insertsql += Replace(txtManual8.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree8.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain8.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 8"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate9.Text <> "" Then
                    If IsDate(txtDate9.Text) Then
                        If CDate(txtDate9.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 9 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate9.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet9.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 9"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree9.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate9.Text, "'", "''") & "', '" & ddlOutlet9.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash9.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual9.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash9.Text) Then
                        '    insertsql += Replace(txtCash9.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual9.Text) Then
                        '    insertsql += Replace(txtManual9.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree9.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain9.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 9"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate10.Text <> "" Then
                    If IsDate(txtDate10.Text) Then
                        If CDate(txtDate10.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 10 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate10.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet10.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 10"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree10.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate10.Text, "'", "''") & "', '" & ddlOutlet10.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash10.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual10.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash10.Text) Then
                        '    insertsql += Replace(txtCash10.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual10.Text) Then
                        '    insertsql += Replace(txtManual10.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree10.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain10.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 10"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate11.Text <> "" Then
                    If IsDate(txtDate11.Text) Then
                        If CDate(txtDate11.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 11 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate11.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet11.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 11"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree11.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate11.Text, "'", "''") & "', '" & ddlOutlet11.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash11.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual11.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash11.Text) Then
                        '    insertsql += Replace(txtCash11.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual11.Text) Then
                        '    insertsql += Replace(txtManual11.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree11.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain11.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 11"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate12.Text <> "" Then
                    If IsDate(txtDate12.Text) Then
                        If CDate(txtDate12.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 12 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate12.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet12.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 12"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree12.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate12.Text, "'", "''") & "', '" & ddlOutlet12.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash12.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual12.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash12.Text) Then
                        '    insertsql += Replace(txtCash12.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual12.Text) Then
                        '    insertsql += Replace(txtManual12.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree12.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain12.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 12"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate13.Text <> "" Then
                    If IsDate(txtDate13.Text) Then
                        If CDate(txtDate13.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 13 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate13.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet13.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 13"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree13.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate13.Text, "'", "''") & "', '" & ddlOutlet13.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash13.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual13.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash13.Text) Then
                        '    insertsql += Replace(txtCash13.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual13.Text) Then
                        '    insertsql += Replace(txtManual13.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree13.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain13.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 13"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate14.Text <> "" Then
                    If IsDate(txtDate14.Text) Then
                        If CDate(txtDate14.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 14 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate14.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet14.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 14"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree14.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate14.Text, "'", "''") & "', '" & ddlOutlet14.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash14.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual14.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash14.Text) Then
                        '    insertsql += Replace(txtCash14.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual14.Text) Then
                        '    insertsql += Replace(txtManual14.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree14.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain14.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 14"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate15.Text <> "" Then
                    If IsDate(txtDate15.Text) Then
                        If CDate(txtDate15.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 15 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate15.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet15.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 15"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree15.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate15.Text, "'", "''") & "', '" & ddlOutlet15.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash15.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual15.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash15.Text) Then
                        '    insertsql += Replace(txtCash15.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual15.Text) Then
                        '    insertsql += Replace(txtManual15.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree15.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain15.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1
                    Else
                        explantionlabel.Text = "Please select a valid date for row 15"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate16.Text <> "" Then
                    If IsDate(txtDate16.Text) Then
                        If CDate(txtDate16.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 16 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate16.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet16.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 16"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree16.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate16.Text, "'", "''") & "', '" & ddlOutlet16.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash16.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual16.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash16.Text) Then
                        '    insertsql += Replace(txtCash16.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual16.Text) Then
                        '    insertsql += Replace(txtManual16.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree16.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain16.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 16"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate17.Text <> "" Then
                    If IsDate(txtDate17.Text) Then
                        If CDate(txtDate17.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 17 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate17.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet17.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 17"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree17.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate17.Text, "'", "''") & "', '" & ddlOutlet17.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash17.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual17.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash17.Text) Then
                        '    insertsql += Replace(txtCash17.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual17.Text) Then
                        '    insertsql += Replace(txtManual17.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree17.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain17.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 17"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate18.Text <> "" Then
                    If IsDate(txtDate18.Text) Then
                        If CDate(txtDate18.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 18 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate18.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If
                        If ddlOutlet18.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 18"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree18.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate18.Text, "'", "''") & "', '" & ddlOutlet18.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash18.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual18.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash18.Text) Then
                        '    insertsql += Replace(txtCash18.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual18.Text) Then
                        '    insertsql += Replace(txtManual18.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree18.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain18.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 18"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate19.Text <> "" Then
                    If IsDate(txtDate19.Text) Then
                        If CDate(txtDate19.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 19 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate19.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet19.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 19"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree19.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate19.Text, "'", "''") & "', '" & ddlOutlet19.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash19.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual19.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash19.Text) Then
                        '    insertsql += Replace(txtCash19.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual19.Text) Then
                        '    insertsql += Replace(txtManual19.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree19.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain19.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 19"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate20.Text <> "" Then
                    If IsDate(txtDate20.Text) Then
                        If CDate(txtDate20.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 20 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate20.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet20.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 20"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree20.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate20.Text, "'", "''") & "', '" & ddlOutlet20.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash20.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual20.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash20.Text) Then
                        '    insertsql += Replace(txtCash20.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual20.Text) Then
                        '    insertsql += Replace(txtManual20.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree20.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain20.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 20"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate21.Text <> "" Then
                    If IsDate(txtDate21.Text) Then
                        If CDate(txtDate21.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 21 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate21.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet21.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 21"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree21.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate21.Text, "'", "''") & "', '" & ddlOutlet21.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash21.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual21.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash21.Text) Then
                        '    insertsql += Replace(txtCash21.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual21.Text) Then
                        '    insertsql += Replace(txtManual21.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree21.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain21.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 21"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate22.Text <> "" Then
                    If IsDate(txtDate22.Text) Then
                        If CDate(txtDate22.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 22 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate22.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet22.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 22"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree22.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate22.Text, "'", "''") & "', '" & ddlOutlet22.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash22.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual22.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash22.Text) Then
                        '    insertsql += Replace(txtCash22.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual22.Text) Then
                        '    insertsql += Replace(txtManual22.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree22.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain22.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 22"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate23.Text <> "" Then
                    If IsDate(txtDate23.Text) Then
                        If CDate(txtDate23.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 23 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate23.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet23.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 23"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree23.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate23.Text, "'", "''") & "', '" & ddlOutlet23.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash23.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual23.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash23.Text) Then
                        '    insertsql += Replace(txtCash23.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual23.Text) Then
                        '    insertsql += Replace(txtManual23.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree23.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain23.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 23"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate24.Text <> "" Then
                    If IsDate(txtDate24.Text) Then
                        If CDate(txtDate24.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 24 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate24.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet24.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 24"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree24.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate24.Text, "'", "''") & "', '" & ddlOutlet24.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash24.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual24.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash24.Text) Then
                        '    insertsql += Replace(txtCash24.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual24.Text) Then
                        '    insertsql += Replace(txtManual24.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree24.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain24.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 24"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                If txtDate25.Text <> "" Then
                    If IsDate(txtDate25.Text) Then
                        If CDate(txtDate25.Text) > Today.Date Then
                            explantionlabel.Text = "The EOD date in row 25 is in the future; please revise"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        ElseIf DateDiff(DateInterval.Month, CDate(txtDate25.Text), Today.Date) > 1 Then
                            futuredates += 1
                        End If

                        If ddlOutlet25.SelectedValue = " -- noneselected -- " Then
                            explantionlabel.Text = "Please select a valid outlet for row 25"
                            explantionlabel.DataBind()
                            btnOptInstructions.Visible = False
                            ModalPopupExtender1.Show()
                            Exit Sub
                        End If
                        insertsql += "Insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, "
                        If ddlAgree25.SelectedValue = "0" Then
                            insertsql += "Explain, "
                        End If
                        insertsql += "SubmissionBy, SubmissionFullName, LastEditedDate) values (@DepID, '" & Replace(txtDepositBag.Text, "'", "''") & _
                            "', '" & Replace(txtDepositSlip.Text, "'", "''") & "', '" & Replace(lblDepositDate.Text, "'", "''") & "', '" & _
                            Replace(txtDate25.Text, "'", "''") & "', '" & ddlOutlet25.SelectedValue & "', '"
                        If Decimal.TryParse(Replace(txtCash25.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        If Decimal.TryParse(Replace(txtManual25.Text, "$", ""), x) Then
                            insertsql += x.ToString & "', '"
                        Else
                            insertsql += "0', '"
                        End If
                        'If IsNumeric(txtCash25.Text) Then
                        '    insertsql += Replace(txtCash25.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        'If IsNumeric(txtManual25.Text) Then
                        '    insertsql += Replace(txtManual25.Text, "$", "") & "', '"
                        'Else
                        '    insertsql += "0', '"
                        'End If
                        If ddlAgree25.SelectedValue = "0" Then
                            insertsql += "0', '" & Replace(txtExplain25.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        Else
                            insertsql += "1', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', '" & _
                                UserName & "', getdate()) "
                        End If

                        subrws += 1

                    Else
                        explantionlabel.Text = "Please select a valid date for row 25"
                        explantionlabel.DataBind()
                        btnOptInstructions.Visible = False
                        ModalPopupExtender1.Show()
                        Exit Sub
                    End If
                End If

                '' 11/22/2019 Added this as quick fix way to get the PPSIDs to line up properly
                insertsql += " update wf  " & _
                    " set PPSMerchantID = mil.MerchantID, PPSStoreID = mil.StoreID, PPSTerminalID = mil.TerminalID " & _
                    " from DWH.WF.PPS_Submissions wf  " & _
                    " join DWH.wf.Merchant_ID_LU mil on mil.MerchantDescription = wf.OutletTA and mil.InactivatedDate is null " & _
                    "	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                    "where wf.Active = 1 and wf.PPSMerchantID is null and MerchantLocID not in (321, 296) "

                If futuredates > 0 Then
                    explantionlabel.Text = "You are trying to submit " & futuredates.ToString & " rows with dates more than one month old.  Is this intentional?"
                    btnOptInstructions.Visible = False
                    lblHoldOverrows.Text = subrws
                    lblHoldOverSQL.Text = insertsql
                    CancelButton.Visible = True
                    SubmitButton.Visible = True
                    SubmitButton.Text = "Submit"
                    OkButton.Visible = False
                    PrintButton.Visible = False
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                If Len(insertsql) > 0 Then

                    explantionlabel.Text = "Do you assert that these " & CStr(subrws) & " rows properly reflect the bag listed?"
                    btnOptInstructions.Visible = False
                    lblHoldOverrows.Text = subrws
                    lblHoldOverSQL.Text = insertsql
                    CancelButton.Visible = True
                    SubmitButton.Text = "Confirm and Submit"
                    SubmitButton.Visible = True
                    OkButton.Visible = False
                    PrintButton.Visible = False
                    ModalPopupExtender1.Show()
                    'Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                    '    If conn.State = ConnectionState.Closed Then
                    '        conn.Open()
                    '    End If

                    '    cmd = New System.Data.SqlClient.SqlCommand(insertsql, conn)
                    '    cmd.ExecuteNonQuery()
                    '    explantionlabel.Text = "Successfully Submitted Bag (" & CStr(subrws) & " rows)"
                    '    explantionlabel.DataBind()
                    '    ResetSubmissionPage()
                    '    ModalPopupExtender1.Show()
                    'End Using
                Else
                    explantionlabel.Text = "No valid rows found; Bag not submitted"
                    btnOptInstructions.Visible = False
                End If


            Else
                explantionlabel.Text = "Please Select a valid Deposit Location; Bag not submitted"
                btnOptInstructions.Visible = False
                ModalPopupExtender1.Show()
            End If
        Catch ex As Exception
            explantionlabel.Text = "Error submitting data; please report to Admin"
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub submitold()

    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        ResetSubmissionPage()
    End Sub

#Region "Original Admin"
    'Sub FillCBL()

    '    Dim dte As String
    '    If rblAdminWhichDate.SelectedIndex = 0 Then
    '        dte = "DepositDate"
    '    Else
    '        dte = "EODCollectionDate"
    '    End If

    '    Dim sql As String

    '    If ddlBagOrSlip.SelectedValue = "Bag" Then
    '        sql = "select distinct DepositBagNumber, DepositBagID from DWH.WF.PPS_Submissions " & _
    '            "where Active = 1 and DepositBagNumber in ( " & _
    '            "select DepositBagNumber from DWH.WF.PPS_Submissions " & _
    '            "where Active = 1 and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' " & _
    '            "group by DepositBagNumber " & _
    '            "having COUNT(distinct DepositBagID) = 1) " & _
    '            "and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' " & _
    '                    "union " & _
    '            "select distinct DepositBagNumber + '(' + convert(varchar, DepositBagID) + ')' as DepositBagNumber, DepositBagID from DWH.WF.PPS_Submissions " & _
    '            "where DepositBagNumber in ( " & _
    '            "select DepositBagNumber from DWH.WF.PPS_Submissions " & _
    '            "where Active = 1 and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' " & _
    '            "group by DepositBagNumber " & _
    '            "having COUNT(distinct DepositBagID) <> 1) " & _
    '            "and Active = 1 and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' "

    '    Else
    '        sql = "select distinct DepositSlipNumber from DWH.WF.PPS_Submissions " & _
    '            "where Active = 1 and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' "

    '    End If



    '    Dim da As New SqlDataAdapter
    '    Dim cmd As SqlCommand
    '    Dim ds As New DataSet

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        cmd = New SqlCommand(sql, conn)
    '        da.SelectCommand = cmd
    '        da.SelectCommand.CommandTimeout = 86400
    '        da.Fill(ds, "OData")

    '    End Using

    '    If ddlBagOrSlip.SelectedValue = "Bag" Then
    '        cblDepositBags.DataSource = ds
    '        cblDepositBags.DataMember = "OData"
    '        cblDepositBags.DataTextField = "DepositBagNumber"
    '        cblDepositBags.DataValueField = "DepositBagID"
    '        cblDepositBags.DataBind()

    '        If cblDepositBags IsNot Nothing Then
    '            For Each li As ListItem In cblDepositBags.Items
    '                li.Attributes("title") = li.Value
    '                li.Selected = True
    '            Next
    '        End If
    '    Else
    '        cblDepositBags.DataSource = ds
    '        cblDepositBags.DataMember = "OData"
    '        cblDepositBags.DataTextField = "DepositSlipNumber"
    '        cblDepositBags.DataValueField = "DepositSlipNumber"
    '        cblDepositBags.DataBind()

    '        If cblDepositBags IsNot Nothing Then
    '            For Each li As ListItem In cblDepositBags.Items
    '                li.Attributes("title") = li.Value
    '                li.Selected = True
    '            Next
    '        End If
    '    End If

    'End Sub

    'Sub PopulateAdminGrid()



    '    Dim gridsql As String = "Select *, convert(varchar, DepositDate, 107) dd, convert(varchar, EODCollectionDate, 107) EODd, convert(varchar, LastEditedDate, 107) LEd, Cash+ManualChecks as Total " & _
    '        "from DWH.WF.PPS_Submissions where Active = 1 and "
    '    Dim ttlsql As String = "select isnull(sum(Cash),0) as CashTotal, isnull(sum(ManualChecks),0) as ManualChecksTotal, isnull(sum(Cash) + sum(ManualChecks),0) as OverallTotal " & _
    '        "from DWH.WF.PPS_Submissions where Active = 1 and "

    '    If ddlBagOrSlip.SelectedValue = "Bag" Then
    '        gridsql += "DepositBagID in (0, '"
    '        ttlsql += "DepositBagID in (0, '"
    '    Else
    '        gridsql += "DepositSlipNumber in (null, '"
    '        ttlsql += "DepositSlipNumber in (null, '"
    '    End If


    '    If cblDepositBags IsNot Nothing Then
    '        For Each li As ListItem In cblDepositBags.Items
    '            If li.Selected = True Then
    '                gridsql += li.Value & "', '"
    '                ttlsql += li.Value & "', '"
    '            End If
    '        Next
    '    End If

    '    gridsql = Left(gridsql, Len(gridsql) - 3) & ") "
    '    ttlsql = Left(ttlsql, Len(ttlsql) - 3) & ") "

    '    If cbSeeFull.Checked = False Then
    '        Dim dte As String
    '        If rblAdminWhichDate.SelectedIndex = 0 Then
    '            dte = "DepositDate"
    '        Else
    '            dte = "EODCollectionDate"
    '        End If
    '        gridsql += " and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' "
    '        ttlsql += " and " & dte & " = '" & Replace(txtAdminWhichDate.Text, "'", "''") & "' "
    '    End If

    '    Dim da As New SqlDataAdapter
    '    Dim cmd As SqlCommand
    '    Dim ds As New DataSet
    '    Dim ds2 As New DataSet

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        cmd = New SqlCommand(gridsql, conn)
    '        da.SelectCommand = cmd
    '        da.SelectCommand.CommandTimeout = 86400
    '        da.Fill(ds, "OData")

    '    End Using

    '    AdminView = ds.Tables(0).DefaultView
    '    gvAdminView.DataSource = AdminView
    '    gvAdminView.DataBind()

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        cmd = New SqlCommand(ttlsql, conn)
    '        da.SelectCommand = cmd
    '        da.SelectCommand.CommandTimeout = 86400
    '        da.Fill(ds2, "OData")

    '    End Using

    '    lblTotalCash.Text = ds2.Tables(0).Rows(0).Item("CashTotal").ToString
    '    lblTotalManual.Text = ds2.Tables(0).Rows(0).Item("ManualChecksTotal").ToString
    '    lblTotal.Text = ds2.Tables(0).Rows(0).Item("OverallTotal").ToString


    'End Sub

    'Private Sub cblDepositBags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblDepositBags.SelectedIndexChanged
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub txtAdminWhichDate_TextChanged(sender As Object, e As EventArgs) Handles txtAdminWhichDate.TextChanged
    '    FillCBL()
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub rblAdminWhichDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblAdminWhichDate.SelectedIndexChanged
    '    FillCBL()
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub lbAdminSelectAll_Click(sender As Object, e As EventArgs) Handles lbAdminSelectAll.Click
    '    If cblDepositBags IsNot Nothing Then
    '        For Each li As ListItem In cblDepositBags.Items
    '            li.Selected = True
    '        Next
    '    End If
    '    PopulateAdminGrid()
    'End Sub
    'Private Sub lbAdminUnSelectAll_Click(sender As Object, e As EventArgs) Handles lbAdminUnSelectAll.Click
    '    If cblDepositBags IsNot Nothing Then
    '        For Each li As ListItem In cblDepositBags.Items
    '            li.Selected = False
    '        Next
    '    End If
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub gvAdminView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAdminView.PageIndexChanging
    '    Try

    '        gvAdminView.PageIndex = e.NewPageIndex
    '        gvAdminView.DataSource = AdminView
    '        gvAdminView.DataBind()

    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Private Sub gvAdminView_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvAdminView.RowCreated
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then

    '            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

    ''e.Row.Cells(2).CssClass = "hidden"
    ''e.Row.Cells(3).CssClass = "hidden"

    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub cbSeeFull_CheckedChanged(sender As Object, e As EventArgs) Handles cbSeeFull.CheckedChanged
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub gvAdminView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAdminView.SelectedIndexChanged
    '    Try


    '        txtEditDepBag.Text = Replace(gvAdminView.SelectedRow.Cells(2).Text, "&nbsp;", "")
    '        txtEditDepSlip.Text = Replace(gvAdminView.SelectedRow.Cells(4).Text, "&nbsp;", "")
    '        txtEditDepDate.Text = gvAdminView.SelectedRow.Cells(5).Text
    '        txtEditEODDate.Text = gvAdminView.SelectedRow.Cells(6).Text
    '        ddlEditOutlet.SelectedValue = gvAdminView.SelectedRow.Cells(7).Text
    '        txtEditCash.Text = gvAdminView.SelectedRow.Cells(8).Text
    '        txtEditManual.Text = gvAdminView.SelectedRow.Cells(9).Text
    '        ddlEditAgree.SelectedValue = gvAdminView.SelectedRow.Cells(11).Text
    '        txtEditExplain.Text = Replace(gvAdminView.SelectedRow.Cells(12).Text, "&nbsp;", "")

    '        For Each canoe As GridViewRow In gvAdminView.Rows
    '            If canoe.RowIndex = gvAdminView.SelectedIndex Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
    '            ElseIf canoe.RowIndex Mod 2 = 0 Then
    '                canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
    '            Else
    '                canoe.BackColor = System.Drawing.Color.White
    '            End If
    '        Next
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub btnAdminUpdate_Click(sender As Object, e As EventArgs) Handles btnAdminUpdate.Click
    '    Try

    '        If txtEditDepDate.Text <> "" And IsDate(txtEditDepDate.Text) Then
    '            If txtEditEODDate.Text <> "" And IsDate(txtEditEODDate.Text) Then
    '                If txtEditDepDate.Text <> gvAdminView.SelectedRow.Cells(5).Text Or txtEditDepBag.Text <> Replace(gvAdminView.SelectedRow.Cells(2).Text, "&nbsp;", "") Or txtEditDepSlip.Text <> Replace(gvAdminView.SelectedRow.Cells(4).Text, "&nbsp;", "") Then
    '                    ExplanationLabel2.Text = "You are trying to change the the Deposit Date, Deposit Bag Number, or Deposit Slip Number for this row; " & _
    '                        "do you want to change these values for all rows in this Deposit Bag, or just the selected row?  "
    '                    btnMPE2OK.Text = "Full Bag"
    '                    btnMPE2Cancel.Text = "Selected Row"
    '                    btnMPE2nvm.Text = "Cancel"
    '                    btnMPE2OK.Visible = True
    '                    btnMPE2Cancel.Visible = True
    '                    btnMPE2nvm.Visible = True
    '                    ModalPopupExtender2.Show()
    '                Else
    '                    updaterows(0)
    '                    btnMPE2OK.Visible = False
    '                    btnMPE2Cancel.Visible = False
    '                    btnMPE2nvm.Text = "OK"
    '                    btnMPE2nvm.Visible = True
    '                    ModalPopupExtender2.Show()
    '                    FillCBL()
    '                    PopulateAdminGrid()
    '                End If

    '            Else
    '                ExplanationLabel2.Text = "Collection Date format incorrect"
    '                btnMPE2OK.Visible = False
    '                btnMPE2Cancel.Visible = False
    '                btnMPE2nvm.Text = "OK"
    '                btnMPE2nvm.Visible = True
    '                ModalPopupExtender2.Show()
    '            End If
    '        Else
    '            ExplanationLabel2.Text = "Deposit Date format incorrect"
    '            btnMPE2OK.Visible = False
    '            btnMPE2Cancel.Visible = False
    '            btnMPE2nvm.Text = "OK"
    '            btnMPE2nvm.Visible = True
    '            ModalPopupExtender2.Show()
    '        End If
    '    Catch ex As Exception
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Sub updaterows(updall As Integer)
    '    Try

    '        Dim updatesql As String = "update DWH.WF.PPS_Submissions " & _
    '            "set Active = 0, " & _
    '            "EditedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', " & _
    '            "LastEditedDate = getdate() " & _
    '            "where ID = " & gvAdminView.SelectedRow.Cells(1).Text & "; "

    '        If updall = 1 Then
    '            updatesql += updateall()
    '        End If

    '        Dim insertsql As String = updatesql & "insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, " & _
    '            "EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, Explain, SubmissionBy, LastEditedDate, Active) " & _
    '            "values (" & gvAdminView.SelectedRow.Cells(3).Text & ", '" & Trim(Replace(txtEditDepBag.Text, "'", "''")) & "', '" & _
    '            Replace(txtEditDepSlip.Text, "'", "''") & "', '" & _
    '            Replace(txtEditDepDate.Text, "'", "''") & "', '" & Replace(txtEditEODDate.Text, "'", "''") & "', '" & _
    '            Replace(ddlEditOutlet.SelectedValue, "'", "''") & "', '" & Replace(txtEditCash.Text, "'", "''") & "', '" & _
    '            Replace(txtEditManual.Text, "'", "''") & "', '" & Replace(ddlEditAgree.SelectedValue, "'", "''") & "', '" & _
    '            Replace(txtEditExplain.Text, "'", "''") & "', '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & _
    '            "', getdate(), 1)"

    '        Dim cmd As SqlCommand
    '        Dim subrws As Integer = 0

    '        If Len(insertsql) > 0 Then
    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(insertsql, conn)
    '                cmd.ExecuteNonQuery()
    '                ExplanationLabel2.Text = "Successfully Updated"
    '                FillCBL()
    '                PopulateAdminGrid()
    '                btnMPE2OK.Visible = False
    '                btnMPE2Cancel.Visible = False
    '                btnMPE2nvm.Text = "OK"
    '                btnMPE2nvm.Visible = True
    '                ExplanationLabel2.DataBind()

    '            End Using
    '        Else
    '            explantionlabel.Text = "Error Submitting Data; Please report to Admin"
    '            btnOptInstructions.Visible = False
    '            btnMPE2OK.Visible = False
    '            btnMPE2Cancel.Visible = False
    '            btnMPE2nvm.Text = "OK"
    '            btnMPE2nvm.Visible = True
    '        End If
    '    Catch ex As Exception
    '        explantionlabel.Text = "Error Submitting Data; Please report to Admin"
    '        btnOptInstructions.Visible = False
    '        btnMPE2OK.Visible = False
    '        btnMPE2Cancel.Visible = False
    '        btnMPE2nvm.Text = "OK"
    '        btnMPE2nvm.Visible = True
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub
    'Function updateall()

    '    Dim updatesql As String = "update DWH.WF.PPS_Submissions " & _
    '        "set Active = 0, " & _
    '        "EditedBy = 'TEMPORARYSYSTEMEDIT', " & _
    '        "LastEditedDate = getdate() " & _
    '        "where Active = 1 and DepositBagID = '" & Replace(gvAdminView.SelectedRow.Cells(3).Text, "'", "''") & "'; "

    '    Dim insertsql As String = "insert into DWH.WF.PPS_Submissions (DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, " & _
    '        "EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, Explain, SubmissionBy, LastEditedDate, Active) " & _
    '        "select DepositBagID, '" & Replace(txtEditDepBag.Text, "'", "''") & "', '" & Replace(txtEditDepSlip.Text, "'", "''") & _
    '        "', '" & Replace(txtEditDepDate.Text, "'", "''") & _
    '        "', EODCollectionDate, OutletTA, Cash, ManualChecks, AgreeToEOD, Explain, '" & _
    '        Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', LastEditedDate, 1 " & _
    '        "from DWH.WF.PPS_Submissions where EditedBy = 'TEMPORARYSYSTEMEDIT' and DepositBagID = '" & _
    '        Replace(gvAdminView.SelectedRow.Cells(3).Text, "'", "''") & "'; "

    '    Dim updatesql2 As String = updatesql & insertsql & "update DWH.WF.PPS_Submissions " & _
    '        "set EditedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
    '        "where DepositBagID = '" & Replace(gvAdminView.SelectedRow.Cells(3).Text, "'", "''") & _
    '        "' and EditedBy = 'TEMPORARYSYSTEMEDIT' ; "

    '    Return updatesql2

    'End Function

    'Private Sub btnMPE2OK_Click(sender As Object, e As EventArgs) Handles btnMPE2OK.Click
    '    updaterows(1)
    '    btnMPE2OK.Visible = False
    '    btnMPE2Cancel.Visible = False
    '    btnMPE2nvm.Text = "OK"
    '    btnMPE2nvm.Visible = True
    '    ModalPopupExtender2.Show()
    '    FillCBL()
    '    PopulateAdminGrid()

    'End Sub

    'Private Sub btnMPE2Cancel_Click(sender As Object, e As EventArgs) Handles btnMPE2Cancel.Click
    '    updaterows(0)
    '    btnMPE2OK.Visible = False
    '    btnMPE2Cancel.Visible = False
    '    btnMPE2nvm.Text = "OK"
    '    btnMPE2nvm.Visible = True
    '    ModalPopupExtender2.Show()
    '    FillCBL()
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub btnMPE2nvm_Click(sender As Object, e As EventArgs) Handles btnMPE2nvm.Click
    '    ModalPopupExtender2.Hide()
    'End Sub

    'Private Sub btnAdminDelete_Click(sender As Object, e As EventArgs) Handles btnAdminDelete.Click
    '    Try

    '        Dim updatesql As String = "update DWH.WF.PPS_Submissions " & _
    '        "set Active = 0, " & _
    '        "EditedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', " & _
    '        "LastEditedDate = getdate() " & _
    '        "where ID = " & gvAdminView.SelectedRow.Cells(1).Text & "; "

    '        Dim cmd As SqlCommand
    '        Dim subrws As Integer = 0

    '        If Len(updatesql) > 0 Then
    '            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If

    '                cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
    '                cmd.ExecuteNonQuery()
    '                ExplanationLabel2.Text = "Row Successfully Removed"
    '                btnMPE2OK.Visible = False
    '                btnMPE2Cancel.Visible = False
    '                btnMPE2nvm.Text = "OK"
    '                btnMPE2nvm.Visible = True
    '                ExplanationLabel2.DataBind()
    '                ModalPopupExtender2.Show()
    '                FillCBL()
    '                PopulateAdminGrid()

    '            End Using
    '        Else
    '            explantionlabel.Text = "Error Submitting Data; Please report to Admin"
    '            btnOptInstructions.Visible = False
    '            btnMPE2OK.Visible = False
    '            btnMPE2Cancel.Visible = False
    '            btnMPE2nvm.Text = "OK"
    '            btnMPE2nvm.Visible = True
    '        End If
    '    Catch ex As Exception
    '        explantionlabel.Text = "Error Submitting Data; Please report to Admin"
    '        btnOptInstructions.Visible = False
    '        btnMPE2OK.Visible = False
    '        btnMPE2Cancel.Visible = False
    '        btnMPE2nvm.Text = "OK"
    '        btnMPE2nvm.Visible = True
    '        LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
    '    End Try
    'End Sub

    'Private Sub ddlBagOrSlip_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBagOrSlip.SelectedIndexChanged
    '    FillCBL()
    '    PopulateAdminGrid()
    'End Sub

    'Private Sub gvAdminView_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvAdminView.Sorting
    '    Dim dv As DataView
    '    Dim sorts As String
    '    dv = AdminView

    '    sorts = e.SortExpression

    '    If e.SortExpression = Adminmap Then

    '        If Admindir = 1 Then
    '            dv.Sort = sorts + " " + "desc"
    '            Admindir = 0
    '        Else
    '            dv.Sort = sorts + " " + "asc"
    '            Admindir = 1
    '        End If

    '    Else
    '        dv.Sort = sorts + " " + "asc"
    '        Admindir = 1
    '        Adminmap = e.SortExpression
    '    End If

    '    gvAdminView.DataSource = dv
    '    gvAdminView.DataBind()
    'End Sub

    '    For Each canoe As GridViewRow In gvAdminView.Rows
    '    If canoe.RowIndex = gvAdminView.SelectedIndex Then
    '        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
    '    ElseIf canoe.RowIndex Mod 2 = 0 Then
    '        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
    '    Else
    '        canoe.BackColor = System.Drawing.Color.White
    '    End If
    'Next
#End Region

    Private Sub ddlDepositLoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepositLoc.SelectedIndexChanged
        UpdateDDLs()
    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        Try


            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet

            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New System.Data.SqlClient.SqlCommand(lblHoldOverSQL.Text, conn)
                cmd.ExecuteNonQuery()
                explantionlabel.Text = "Successfully Submitted Bag (" & CStr(lblHoldOverrows.Text) & " rows)"
                btnOptInstructions.Visible = False
                explantionlabel.DataBind()
                'ResetSubmissionPage()
                ModalPopupExtender1.Show()
            End Using

            CancelButton.Visible = False
            SubmitButton.Visible = False
            OkButton.Visible = True
            PrintButton.Visible = True
            PrintButton.Attributes.Add("onclick", "javascript:window.print()")
            'ModalPopupExtender1.Hide()
        Catch ex As Exception
            explantionlabel.Text = "Error Submitting Bag -- Please Report to Admin"
            btnOptInstructions.Visible = False
            ModalPopupExtender1.Show()
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

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
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))

                'e.Row.Cells(0).CssClass = "hidden"
                'e.Row.Cells(7).CssClass = "hidden"

                If e.Row.DataItem("Agree") = "0" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    'If e.Row.DataItem("Attachments") > 0 Then
                    '    Dim img As Image = e.Row.FindControl("imgLR")
                    '    img.Visible = True
                    'End If
                    'ElseIf e.Row.DataItem("Agree") = "2" Then
                    '    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffbb77")
                    'If e.Row.DataItem("Attachments") > 0 Then
                    '    Dim img As Image = e.Row.FindControl("imgLO")
                    '    img.Visible = True
                    'End If
                ElseIf e.Row.DataItem("Agree") = "1" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    'If e.Row.DataItem("Attachments") > 0 Then
                    '    Dim img As Image = e.Row.FindControl("imgLY")
                    '    img.Visible = True
                    'End If
                End If

            End If
        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSubmittedBags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSubmittedBags.SelectedIndexChanged
        Try

            lblSelectBagDD.Text = Replace(Replace(Server.HtmlDecode(gvSubmittedBags.SelectedRow.Cells(5).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSelectBagDepNo.Text = Replace(Replace(Server.HtmlDecode(gvSubmittedBags.SelectedRow.Cells(3).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSelectBagDepSlip.Text = Replace(Replace(Server.HtmlDecode(gvSubmittedBags.SelectedRow.Cells(4).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSelectTotal.Text = Replace(Replace(Server.HtmlDecode(gvSubmittedBags.SelectedRow.Cells(7).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSelectSubBy.Text = Replace(Replace(Server.HtmlDecode(gvSubmittedBags.SelectedRow.Cells(6).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSelectBagID.Text = Replace(Replace(Server.HtmlDecode(gvSubmittedBags.SelectedRow.Cells(1).Text), "&nbsp;", ""), "replacedapostrophe", "'")

            For Each canoe As GridViewRow In gvSubmittedBags.Rows
                If canoe.RowIndex = gvSubmittedBags.SelectedIndex Then
                    If canoe.Cells(8).Text = "0" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                    ElseIf canoe.Cells(8).Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                    End If
                Else
                    If canoe.Cells(8).Text = "0" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    ElseIf canoe.Cells(8).Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    End If
                End If
            Next

            SelectBag(gvSubmittedBags.SelectedRow.Cells(1).Text)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
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

    Sub SelectBag(x As String)

        Try

            '            Dim selectbagsql As String = " select *, convert(varchar, DepositDate, 107) as DepositDisplay from DWH.WF.PPS_Submissions wf join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_ID_LU mil " & _
            '"where  mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ACC.%' " & _
            '"and [MerchantDescription] not like 'ZZ%' " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '"where Active = 1 And wf.DepositBagID = " & x & _
            '"order by ID "

            'switched 7/23/2019 CRW
            'Dim selectbagsql As String = " select *, convert(varchar, DepositDate, 107) as DepositDisplay from DWH.WF.PPS_Submissions wf join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_LU " & _
            '"where  Active = 1 and [MerchantDescription] not like 'ACC.%' " & _
            '"and [MerchantDescription] not like 'ZZ%' " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '"where Active = 1 And wf.DepositBagID = " & x & _
            '"order by ID "

            ' 11/22/2019 CRW
            '           Dim selectbagsql As String = " select *, convert(varchar, DepositDate, 107) as DepositDisplay from DWH.WF.PPS_Submissions wf join " & _
            '"(select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '             "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '             "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '             "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '             "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '             "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '             "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '             "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '             "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '             "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '             "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '             "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '             "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '             "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '             "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '             "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '             "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '             "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '             "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '             "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '             "from DWH.WF.Merchant_ID_LU mil" & _
            '             " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '             " and [MerchantDescription] not like 'ACC.%' " & _
            '             "and [MerchantDescription] not like 'ZZ%' " & _
            '             "and [MerchantID] not like '%E%'  ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '"where Active = 1 And wf.DepositBagID = " & x & _
            '"order by ID "

            Dim selectbagsql As String = "select  isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')') as Entity " & _
            " , mil.[MerchantDescription] " & _
            " , isnull(cast(Fac_ID as bigint), mil.MerchantID) as MerchantID " & _
            " ,wf.*, convert(varchar, DepositDate, 107) as DepositDisplay  " & _
            " from DWH.WF.PPS_Submissions wf             " & _
            "left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
            "	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            "	left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
            "		and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
            "	left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
            "		and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')  " & _
            " where Active = 1 And wf.DepositBagID = " & x & _
            "order by ID "

         

            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(selectbagsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            gvSelectedBag.DataSource = ds.Tables(0).DefaultView
            gvSelectedBag.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Sub PopulateAdmin2Grid(x As Integer)

        Try
            Dim DepNumLimiter As String = ""
            If Len(Trim(txtFilterDepBagNum.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtFilterDepBagNum.Text), "'", "''") & "%' "
            End If

            If Len(Trim(txtFilterDepSlipNum.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtFilterDepSlipNum.Text), "'", "''") & "%' "
            End If

            '        Dim gridsql As String = "select Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName, " & _
            '"SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, MIN(convert(int, AgreeToEOD)) as Agree " & _
            '" from DWH.WF.PPS_Submissions wf " & _
            '"left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '"join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_ID_LU mil " & _
            '"where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ACC.%'  " & _
            '"and [MerchantDescription] not like 'ZZ%'  " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription      " & _
            '"where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") and (Entity = '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
            '"and (SubmissionFullName = '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
            '"and DepositDate between '" & Replace(txtDDStartDate.Text, "'", "''") & "' and '" & Replace(txtDDEndDate.Text, "'", "''") & "' " & _
            '"and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            'Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ')) " & _
            'DepNumLimiter &
            '"Group by " & _
            '"Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "

            '7/23/2019 CRW
            'Dim gridsql As String = "select Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName, " & _
            '    "SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, MIN(convert(int, AgreeToEOD)) as Agree " & _
            '    " from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '    "from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '    "and [MerchantDescription] not like 'ZZ%'  " & _
            '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription      " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") and (Entity = '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
            '    "and (SubmissionFullName = '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
            '    "and DepositDate between '" & Replace(txtDDStartDate.Text, "'", "''") & "' and '" & Replace(txtDDEndDate.Text, "'", "''") & "' " & _
            '    "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '    Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ')) " & _
            '    DepNumLimiter &
            '    "Group by " & _
            '    "Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "

            '11/22/2019
            'Dim gridsql As String = "select Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName, " & _
            '    "SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, MIN(convert(int, AgreeToEOD)) as Agree " & _
            '    " from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '  "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '  "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '  "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '  "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '  "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '  "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '  "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '  "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '  "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '  "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '  "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '  "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '  "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '  "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '  "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '  "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '  "else 'Other -- (' + [MerchantDescription] + ')' end Entity " & _
            '  "from DWH.WF.Merchant_ID_LU mil" & _
            '  " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '  " and [MerchantDescription] not like 'ACC.%' " & _
            '  "and [MerchantDescription] not like 'ZZ%' " & _
            '  "and [MerchantID] not like '%E%'  ) mp on wf.OutletTA = mp.MerchantDescription      " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") and (Entity = '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
            '    "and (SubmissionFullName = '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
            '    "and DepositDate between '" & Replace(txtDDStartDate.Text, "'", "''") & "' and '" & Replace(txtDDEndDate.Text, "'", "''") & "' " & _
            '    "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '    Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ')) " & _
            '    DepNumLimiter &
            '    "Group by " & _
            '    "Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "

            Dim gridsql As String = "select isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')') as Entity " & _
", DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName,   " & _
"                SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, MIN(convert(int, AgreeToEOD)) as Agree   " & _
"                 from DWH.WF.PPS_Submissions wf   " & _
"                left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'  " & _
"               left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
"	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
"	left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"		and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"	left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"		and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')    " & _
    "where wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") and " & _
    "(isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''") & "' = '-1') " & _
    "and (SubmissionFullName = '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterSubmitter.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
    "and DepositDate between '" & Replace(txtDDStartDate.Text, "'", "''") & "' and '" & Replace(txtDDEndDate.Text, "'", "''") & "' " & _
    "and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
    "           left join DWH.wf.Merchant_ID_LU mil2 on wf2.PPSMerchantID = mil2.MerchantID and wf2.PPSStoreID = mil2.StoreID and wf2.PPSTerminalID = mil2.TerminalID " & _
    "           and mil.InactivatedDate is null and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
    "           where wf.DepositBagID = wf2.DepositBagID and (mil2.MerchantLocID = '" & _
    Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''") & "' = '-1')) " & _
    DepNumLimiter &
    "Group by " & _
    "isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')'), DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "

            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(gridsql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            AdminView2 = ds.Tables(0).DefaultView
            gvSubmittedBags.DataSource = AdminView2
            gvSubmittedBags.DataBind()

            gvSubmittedBags.SelectedIndex = -1

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub UpdateEntityDDL(x As Integer)

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

            Dim FilterLocation As String = "-1"
            If IsNothing(ddlFilterLocation.SelectedItem) = False Then
                FilterLocation = Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''")
            End If

            'Dim sql As String = "select 'Select Entity (optional) ' as lbl, ' -- none selected -- ' as Entity, 0 as ord " & _
            '    "union " & _
            '    "select distinct Entity, Entity, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '    "from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '    "and [MerchantDescription] not like 'ZZ%'  " & _
            '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '    "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '    FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '    "order by ord, lbl "

            ' 11/22/2019
            '          Dim sql As String = "select 'Select Entity (optional) ' as lbl, ' -- none selected -- ' as Entity, 0 as ord " & _
            '"union " & _
            '"select distinct Entity, Entity, 1 " & _
            '"from DWH.WF.PPS_Submissions wf " & _
            '"left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '"join " & _
            '"(select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '          "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '          "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '          "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '          "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '          "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '          "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '          "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '          "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '          "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '          "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '          "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '          "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '          "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '          "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '          "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '          "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '          "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '          "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '          "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '          "from DWH.WF.Merchant_ID_LU mil" & _
            '          " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '          " and [MerchantDescription] not like 'ACC.%' " & _
            '          "and [MerchantDescription] not like 'ZZ%' " & _
            '          "and [MerchantID] not like '%E%'  ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '"where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '"and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '"and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            'FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '"order by ord, lbl "

            Dim sql As String = "select 'Select Entity (optional) ' as lbl, '-1' as Entity, 0 as ord " & _
    "union " & _
    "    select distinct isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')') Entity, isnull(cast(Fac_ID as bigint), mil.MerchantID), 1  " & _
"    from DWH.WF.PPS_Submissions wf  " & _
"    left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
"   left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
"	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
"	left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"		and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"	left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"		and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')    " & _
    "where wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") " & DepNumLimiter & _
    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
    "and exists  (select * from DWH.WF.PPS_Submissions wf2 " & _
    "           left join DWH.wf.Merchant_ID_LU mil2 on wf2.PPSMerchantID = mil2.MerchantID and wf2.PPSStoreID = mil2.StoreID and wf2.PPSTerminalID = mil2.TerminalID " & _
    "           and mil.InactivatedDate is null and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
    "           where wf.DepositBagID = wf2.DepositBagID and (mil2.MerchantLocID = '" & _
    FilterLocation & "' or '" & FilterLocation & "' = '-1')) " & _
    "order by ord, lbl "


            'Dim sql As String = "select 'Select Entity (optional) ' as lbl, ' -- none selected -- ' as Entity, 0 as ord " & _
            '    "union " & _
            '    "select distinct Entity, Entity, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '    "from DWH.WF.Merchant_ID_LU mil " & _
            '    "where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ACC.%'  " & _
            '    "and [MerchantDescription] not like 'ZZ%'  " & _
            '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '    "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '    FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '    "order by ord, lbl "

            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(sql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlFilterEntity.DataSource = ds
            ddlFilterEntity.DataValueField = "Entity"
            ddlFilterEntity.DataTextField = "lbl"
            ddlFilterEntity.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub UpdateUserDDL(x As Integer)

        Try

            Dim DepNumLimiter As String = ""
            If Len(Trim(txtFilterDepBagNum.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtFilterDepBagNum.Text), "'", "''") & "%'"
            End If
            If Len(Trim(txtFilterDepSlipNum.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtFilterDepSlipNum.Text), "'", "''") & "%' "
            End If
            Dim FilterEntity As String = "-1"
            If IsNothing(ddlFilterEntity.SelectedItem) = False Then
                FilterEntity = Replace(ddlFilterEntity.SelectedValue.ToString, "'", "''")
            End If
            Dim FilterLocation As String = "-1"
            If IsNothing(ddlFilterLocation.SelectedItem) = False Then
                FilterLocation = Replace(ddlFilterLocation.SelectedValue.ToString, "'", "''")
            End If

            '   Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
            '"union " & _
            '"select distinct SubmissionFullName, SubmissionFullName, 1 " & _
            '"from DWH.WF.PPS_Submissions wf " & _
            '"left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '"join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_LU " & _
            '"where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '"and [MerchantDescription] not like 'ZZ%'  " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '"where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '"and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '"and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
            '"where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            'FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '"order by ord, lbl "

            ' 11/22/2019
            '          Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
            '  "union " & _
            '  "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
            '  "from DWH.WF.PPS_Submissions wf " & _
            '  "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '  "join " & _
            '  "(select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '"when [MerchantID] = '172123612993' then 'Duluth' " & _
            '"when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '"when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '"when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '"when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '"when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '"when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '"when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '"when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '"when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_ID_LU mil" & _
            '" where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '" and [MerchantDescription] not like 'ACC.%' " & _
            '"and [MerchantDescription] not like 'ZZ%' " & _
            '"and [MerchantID] not like '%E%'  ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '  "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '  "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '  "and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
            '  "where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '  FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '  "order by ord, lbl "

            Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
                "union " & _
                "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
                "from DWH.WF.PPS_Submissions wf " & _
                "left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.Submissionby) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
            "   left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
            "	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            "	left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
            "		and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
            "	left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
            "		and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')    " & _
                "where wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") " & DepNumLimiter & _
                "and (isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & FilterEntity & "' or '" & FilterEntity & "' = '-1') " & _
                "and exists  (select * from DWH.WF.PPS_Submissions wf2 " & _
    "           left join DWH.wf.Merchant_ID_LU mil2 on wf2.PPSMerchantID = mil2.MerchantID and wf2.PPSStoreID = mil2.StoreID and wf2.PPSTerminalID = mil2.TerminalID " & _
    "           and mil.InactivatedDate is null and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
    "           where wf.DepositBagID = wf2.DepositBagID and (mil2.MerchantLocID =  '" & _
    FilterLocation & "' or '" & FilterLocation & "' = '-1')) " & _
                "order by ord, lbl "

            'Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
            '   "union " & _
            '   "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
            '   "from DWH.WF.PPS_Submissions wf " & _
            '   "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '   "join " & _
            '   "(select distinct [MerchantDescription], " & _
            '   "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '   "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '   "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '   "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '   "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '   "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '   "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '   "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '   "from DWH.WF.Merchant_ID_LU mil " & _
            '   "where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ACC.%'  " & _
            '   "and [MerchantDescription] not like 'ZZ%'  " & _
            '   "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '   "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '   "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '   "and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
            '   "where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '   FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '   "order by ord, lbl "

            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(sql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlFilterSubmitter.DataSource = ds
            ddlFilterSubmitter.DataValueField = "SubmissionFullName"
            ddlFilterSubmitter.DataTextField = "lbl"
            ddlFilterSubmitter.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub UpdateLocationDDL(x As Integer)

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

            '        Dim sql As String = "select 'Select Location (optional) ' as lbl, ' -- none selected -- ' as OutletTA, 0 as ord " & _
            '"union " & _
            '"select distinct OutletTA, OutletTA, 1 " & _
            '"from DWH.WF.PPS_Submissions wf " & _
            '"left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '"join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_ID_LU mil " & _
            '"where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '"and [MerchantDescription] not like 'ZZ%'  " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '"where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '"and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '"and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '"order by ord, lbl "

            'Dim sql As String = "select 'Select Location (optional) ' as lbl, ' -- none selected -- ' as OutletTA, 0 as ord " & _
            '    "union " & _
            '    "select distinct OutletTA, OutletTA, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '    "from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '    "and [MerchantDescription] not like 'ZZ%'  " & _
            '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '    "order by ord, lbl "

            Dim sql As String = "select 'Select Location (optional) ' as lbl, '-1' as OutletTA, 0 as ord " & _
    "union " & _
    "select distinct OutletTA, OutletTA, 1 " & _
    "from DWH.WF.PPS_Submissions wf " & _
    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
    "   left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
"	and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
"	left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"		and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"	left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"		and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')    " & _
    "where wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") " & DepNumLimiter & _
    "and (isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & FilterEntity & "' or '" & FilterEntity & "' = '-1') " & _
    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
    "order by ord, lbl "

            Dim da As New SqlDataAdapter
            Dim cmd As SqlCommand
            Dim ds As New DataSet
            Dim ds2 As New DataSet

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                cmd = New SqlCommand(sql, conn)
                da.SelectCommand = cmd
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(ds, "OData")

            End Using

            ddlFilterLocation.DataSource = ds
            ddlFilterLocation.DataValueField = "OutletTA"
            ddlFilterLocation.DataTextField = "lbl"
            ddlFilterLocation.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub ddlFilterEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterEntity.SelectedIndexChanged

        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterLocation.SelectedValue.ToString
        UpdateUserDDL(admin)
        UpdateLocationDDL(admin)
        Try
            ddlFilterSubmitter.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = y
        Catch ex As Exception
        End Try

        PopulateAdmin2Grid(admin)

    End Sub


    Private Sub ddlFilterSubmitter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterSubmitter.SelectedIndexChanged

        Dim x As String = ddlFilterEntity.SelectedValue.ToString
        Dim y As String = ddlFilterLocation.SelectedValue.ToString

        UpdateEntityDDL(admin)
        UpdateLocationDDL(admin)

        Try
            ddlFilterEntity.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = y
        Catch ex As Exception
        End Try

        PopulateAdmin2Grid(admin)

    End Sub

    Private Sub ddlFilterLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterLocation.SelectedIndexChanged

        Dim x As String = ddlFilterEntity.SelectedValue.ToString
        Dim y As String = ddlFilterSubmitter.SelectedValue.ToString

        UpdateEntityDDL(admin)
        UpdateUserDDL(admin)

        Try
            ddlFilterEntity.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlFilterSubmitter.SelectedValue = y
        Catch ex As Exception
        End Try

        PopulateAdmin2Grid(admin)

    End Sub

#End Region

    Private Sub txtDDStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtDDStartDate.TextChanged
        PopulateAdmin2Grid(admin)
    End Sub

    Private Sub txtDDEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtDDEndDate.TextChanged
        PopulateAdmin2Grid(admin)
    End Sub

    Private Sub btnRejectBag_Click(sender As Object, e As EventArgs) Handles btnRejectBag.Click

        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
        UserName = Replace(tmblbl.Text, "'", "''")

        Dim updatesql As String = "Update DWH.WF.PPS_Submissions " & _
            "set Active = 0, Email = 1, " & _
            "EditedBy = '" & Replace(UserName, "'", "''") & "', " & _
            "EditComment = '" & Replace(txtRejectReason.Text, "'", "''") & "', " & _
            "LastEditedDate = getdate() " & _
            "where DepositBagID = '" & Replace(lblSelectBagID.Text, "'", "''") & "'"

        Dim da As New SqlDataAdapter
        Dim cmd As SqlCommand
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
            cmd.ExecuteNonQuery()
            ExplanationLabelReject.Text = "Bag Rejected"
            btnMPROK.Visible = False
            btnMPRCancel.Visible = False
            btnMPRnvm.Text = "OK"
            btnMPRnvm.Visible = True
            ExplanationLabelReject.DataBind()

        End Using

        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterEntity.SelectedValue.ToString
        Dim z As String = ddlFilterLocation.SelectedValue.ToString

        UpdateUserDDL(admin)
        Try
            ddlFilterSubmitter.SelectedValue = x
        Catch ex As Exception
        End Try
        UpdateEntityDDL(admin)
        Try
            ddlFilterEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlFilterLocation.SelectedValue = z
        Catch ex As Exception
        End Try
        PopulateAdmin2Grid(admin)

        txtRejectReason.Text = ""
        lblSelectBagID.Text = ""
        lblSelectBagDepNo.Text = ""
        lblSelectBagDD.Text = ""
        lblSelectSubBy.Text = ""
        lblSelectTotal.Text = ""

        Dim fakeds As New DataTable
        gvSelectedBag.DataSource = fakeds
        gvSelectedBag.DataBind()
        ModalPopupExtenderReject.Show()

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

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        btnOptInstructions.Visible = False
        If PrintButton.Visible = True Then
            ResetSubmissionPage()
        End If

    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        ModalPopupExtender1.Show()
    End Sub


    Private Sub txtFilterDepBagNum_TextChanged(sender As Object, e As EventArgs) Handles txtFilterDepBagNum.TextChanged
        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterEntity.SelectedValue.ToString
        Dim z As String = ddlFilterLocation.SelectedValue.ToString
        UpdateUserDDL(admin)
        UpdateLocationDDL(admin)
        UpdateEntityDDL(admin)
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

        PopulateAdmin2Grid(admin)
    End Sub

    Private Sub txtFilterDepSlipNum_TextChanged(sender As Object, e As EventArgs) Handles txtFilterDepSlipNum.TextChanged
        Dim x As String = ddlFilterSubmitter.SelectedValue.ToString
        Dim y As String = ddlFilterEntity.SelectedValue.ToString
        Dim z As String = ddlFilterLocation.SelectedValue.ToString
        UpdateUserDDL(admin)
        UpdateLocationDDL(admin)
        UpdateEntityDDL(admin)
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

        PopulateAdmin2Grid(admin)
    End Sub

    Private Sub gvSABags_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSABags.PageIndexChanging
        Try

            gvSABags.PageIndex = e.NewPageIndex
            gvSABags.DataSource = AdminView
            gvSABags.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSABags_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSABags.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))
                If e.Row.DataItem("Agree") = "0" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                ElseIf e.Row.DataItem("Agree") = "1" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                End If
            End If
        Catch ex As Exception
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSABags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvSABags.SelectedIndexChanged
        Try
            txtSADepDate.Text = Replace(Replace(Server.HtmlDecode(gvSABags.SelectedRow.Cells(5).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtSADepBagNo.Text = Replace(Replace(Server.HtmlDecode(gvSABags.SelectedRow.Cells(3).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            txtSADepSlipNo.Text = Replace(Replace(Server.HtmlDecode(gvSABags.SelectedRow.Cells(4).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSADepBagTotal.Text = Replace(Replace(Server.HtmlDecode(gvSABags.SelectedRow.Cells(7).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSASubBy.Text = Replace(Replace(Server.HtmlDecode(gvSABags.SelectedRow.Cells(6).Text), "&nbsp;", ""), "replacedapostrophe", "'")
            lblSABagID.Text = Replace(Replace(Server.HtmlDecode(gvSABags.SelectedRow.Cells(1).Text), "&nbsp;", ""), "replacedapostrophe", "'")

            For Each canoe As GridViewRow In gvSABags.Rows
                If canoe.RowIndex = gvSABags.SelectedIndex Then
                    If canoe.Cells(8).Text = "0" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                    ElseIf canoe.Cells(8).Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                    End If
                Else
                    If canoe.Cells(8).Text = "0" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                    ElseIf canoe.Cells(8).Text = "1" Then
                        canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                    End If
                End If
            Next

            SASelectBag(gvSABags.SelectedRow.Cells(1).Text)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSABags_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvSABags.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = AdminView

        sorts = e.SortExpression

        If e.SortExpression = Adminmap Then

            If Admindir = 1 Then
                dv.Sort = sorts + " " + "desc"
                Admindir = 0
            Else
                dv.Sort = sorts + " " + "asc"
                Admindir = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            Admindir = 1
            Adminmap = e.SortExpression
        End If

        gvSABags.DataSource = dv
        gvSABags.DataBind()
    End Sub

    Sub SASelectBag(x As String)

        Try

            ' adjusted when Outlet somehow lost CRW 1/18/2018
            'Dim selectbagsql As String = " select *, convert(varchar, DepositDate, 107) as DepositDisplay from DWH.WF.PPS_Submissions wf left join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_LU " & _
            '"where Active = 1 and [MerchantDescription] not like 'ACC.%' " & _
            '"and [MerchantDescription] not like 'ZZ%' " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '"where Active = 1 And wf.DepositBagID = " & x & _
            '"order by ID "

            '     Dim selectbagsql As String = "  select wf.*, mp.MerchantDescription , convert(varchar, DepositDate, 107) as DepositDisplay, isnull(Entity, BackupEntity) as Entity  " & _
            '         " from DWH.WF.PPS_Submissions wf left join " & _
            '              " (select Max(Entity) as BackupEntity from DWH.WF.PPS_Submissions wf " & _
            ' "	left join  " & _
            '"     (select distinct [MerchantDescription],  " & _
            ' "    case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            ' "    when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            ' "    when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '  "   when [MerchantID] = '184188903993' then 'GCS - Other'    " & _
            '  "   when [MerchantID] = '174243397992' then 'GCS - ProFee'   " & _
            '  "   when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '  "   when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '  "   else 'Other -- (' + [MerchantDescription] + ')' end as Entity  " & _
            '  "   from DWH.WF.Merchant_LU  " & _
            '  "   where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '  "   and [MerchantDescription] not like 'ZZ%'  " & _
            '  "   and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription  " & _
            ' "	where DepositBagID = " & x & ") x on 1 = 1 " & _
            '     "left join (select distinct [MerchantDescription], " & _
            '     "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '     "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '     "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '     "when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '     "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '     "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '     "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '     "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '     "from DWH.WF.Merchant_LU " & _
            '     "where Active = 1 and [MerchantDescription] not like 'ACC.%' " & _
            '     "and [MerchantDescription] not like 'ZZ%' " & _
            '     "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '     "where Active = 1 And wf.DepositBagID = " & x & _
            '     "order by ID "

            ' 11/22/2019 
            '            Dim selectbagsql As String = "  select wf.*, mp.MerchantDescription , convert(varchar, DepositDate, 107) as DepositDisplay, isnull(Entity, BackupEntity) as Entity  " & _
            '         " from DWH.WF.PPS_Submissions wf left join " & _
            '              " (select Max(Entity) as BackupEntity from DWH.WF.PPS_Submissions wf " & _
            ' "	left join  " & _
            '"     (  select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '              "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '              "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '              "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '              "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '              "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '              "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '              "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '              "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '              "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '              "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '              "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '              "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '              "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '              "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '              "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '              "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '              "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '              "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '              "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '              "from DWH.WF.Merchant_ID_LU mil" & _
            '              " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '              " and [MerchantDescription] not like 'ACC.%' " & _
            '              "and [MerchantDescription] not like 'ZZ%' " & _
            '              "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription  " & _
            ' "	where DepositBagID = " & x & ") x on 1 = 1 " & _
            '     "left join (   select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '              "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '              "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '              "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '              "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '              "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '              "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '              "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '              "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '              "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '              "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '              "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '              "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '              "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '              "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '              "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '              "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '              "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '              "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '              "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '              "from DWH.WF.Merchant_ID_LU mil" & _
            '              " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '              " and [MerchantDescription] not like 'ACC.%' " & _
            '              "and [MerchantDescription] not like 'ZZ%' " & _
            '              "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '     "where Active = 1 And wf.DepositBagID = " & x & _
            '     "order by ID "

            Dim selectbagsql As String = "  select wf.*, mil.MerchantDescription , convert(varchar, DepositDate, 107) as DepositDisplay, isnull(isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')'), x.BackupEntity) as Entity   " & _
"          from DWH.WF.PPS_Submissions wf " & _
"		  left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
"				and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
"				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
"       left join (  select DepositBagID, max(isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')')) as BackupEntity   " & _
"          from DWH.WF.PPS_Submissions wf " & _
"		  left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
"				and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
"				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')  group by DepositBagID  ) x on wf.DepositBagID = x.DepositBagID " & _
"     where Active = 1 And wf.DepositBagID = " & x & _
     "order by ID "

            '       Dim selectbagsql As String = "  select wf.*, mp.MerchantDescription , convert(varchar, DepositDate, 107) as DepositDisplay, isnull(Entity, BackupEntity) as Entity  " & _
            '         " from DWH.WF.PPS_Submissions wf left join " & _
            '              " (select Max(Entity) as BackupEntity from DWH.WF.PPS_Submissions wf " & _
            ' "	left join  " & _
            '"     (select distinct [MerchantDescription],  " & _
            ' "    case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            ' "    when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            ' "    when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '  "   when [MerchantID] = '184188903993' then 'GCS - Other'    " & _
            '  "   when [MerchantID] = '174243397992' then 'GCS - ProFee'   " & _
            '  "   when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '  "   when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '  "   else 'Other -- (' + [MerchantDescription] + ')' end as Entity  " & _
            '  "   from DWH.WF.Merchant_ID_LU mil  " & _
            '  "   where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ACC.%'  " & _
            '  "   and [MerchantDescription] not like 'ZZ%'  " & _
            '  "   and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription  " & _
            ' "	where DepositBagID = " & x & ") x on 1 = 1 " & _
            '     "left join (select distinct [MerchantDescription], " & _
            '     "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '     "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '     "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '     "when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '     "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '     "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '     "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '     "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '     "from DWH.WF.Merchant_ID_LU mil " & _
            '     "where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') and [MerchantDescription] not like 'ACC.%' " & _
            '     "and [MerchantDescription] not like 'ZZ%' " & _
            '     "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription            " & _
            '     "where Active = 1 And wf.DepositBagID = " & x & _
            '     "order by ID "


            Rowsdv = GetData(selectbagsql).DefaultView
            gvSARows.DataSource = Rowsdv
            gvSARows.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub
    Sub SAPopulateAdmin2Grid(x As Integer)

        Try
            Dim DepNumLimiter As String = ""
            If Len(Trim(txtSADepBag.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtSADepBag.Text), "'", "''") & "%' "
            End If

            If Len(Trim(txtSADepSlip.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtSADepSlip.Text), "'", "''") & "%' "
            End If



                'Dim gridsql As String = "select Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName, " & _
                '    "SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, MIN(convert(int, AgreeToEOD)) as Agree " & _
                '    " from DWH.WF.PPS_Submissions wf " & _
                '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
                '    "join " & _
                '    "(select distinct [MerchantDescription], " & _
                '    "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
                '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
                '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
                '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
                '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
                '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
                '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
                '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
                '    "from DWH.WF.Merchant_LU " & _
                '    "where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
                '    "and [MerchantDescription] not like 'ZZ%'  " & _
                '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription      " & _
                '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") and " & _
                '    "(Entity = '" & Replace(ddlSAEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSAEntity.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
                '    "and (SubmissionFullName = '" & Replace(ddlSASubBy.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSASubBy.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
                '    "and DepositDate between '" & Replace(txtSAStartDate.Text, "'", "''") & "' and '" & Replace(txtSAEndDate.Text, "'", "''") & "' " & _
                '    "and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
                '    Replace(ddlSALoc.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSALoc.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ')) " & _
                '    DepNumLimiter &
                '    "Group by " & _
                '    "Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "

            Dim gridsql As String = "select isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')') as Entity, DepositBagID, DepositBagNumber, DepositSlipNumber, convert(varchar, DepositDate, 107) as DepositDate, DepositDate as dtDeposited, SubmissionFullName, " & _
                "SUM(ISNULL(cash,0) + isnull(ManualChecks,0)) as Total, MIN(convert(int, AgreeToEOD)) as Agree " & _
                " from DWH.WF.PPS_Submissions wf " & _
                "left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
 "		  left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
"				and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
"				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
"					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
"				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
"					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
                "where wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") and " & _
                "(isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & Replace(ddlSAEntity.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSAEntity.SelectedValue.ToString, "'", "''") & "' = '-1') " & _
                "and (SubmissionFullName = '" & Replace(ddlSASubBy.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSASubBy.SelectedValue.ToString, "'", "''") & "' = ' -- none selected -- ') " & _
                "and DepositDate between '" & Replace(txtSAStartDate.Text, "'", "''") & "' and '" & Replace(txtSAEndDate.Text, "'", "''") & "' " & _
                "and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
                "           left join DWH.wf.Merchant_ID_LU mil2 on wf2.PPSMerchantID = mil2.MerchantID and wf2.PPSStoreID = mil2.StoreID and wf2.PPSTerminalID = mil2.TerminalID " & _
                "           and mil.InactivatedDate is null and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "           where wf.DepositBagID = wf2.DepositBagID and (mil2.MerchantLocID = '" & _
                Replace(ddlSALoc.SelectedValue.ToString, "'", "''") & "' or '" & Replace(ddlSALoc.SelectedValue.ToString, "'", "''") & "' = '-1')) " & _
                DepNumLimiter &
                "Group by " & _
                "isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')'), DepositBagID, DepositBagNumber, DepositSlipNumber, DepositDate, SubmissionFullName "


            AdminView = GetData(gridsql).DefaultView
            gvSABags.DataSource = AdminView
            gvSABags.DataBind()

            gvSABags.SelectedIndex = -1

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub SAUpdateEntityDDL(x As Integer)

        Try

            Dim DepNumLimiter As String = ""
            If Len(Trim(txtSADepBag.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtSADepBag.Text), "'", "''") & "%' "
            End If
            If Len(Trim(txtSADepSlip.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtSADepSlip.Text), "'", "''") & "%' "
            End If
            Dim FilterSubmitter As String = " -- none selected -- "
            If IsNothing(ddlSASubBy.SelectedItem) = False Then
                FilterSubmitter = Replace(ddlSASubBy.SelectedValue.ToString, "'", "''")
            End If

            Dim FilterLocation As String = "-1"
            If IsNothing(ddlSALoc.SelectedItem) = False Then
                FilterLocation = Replace(ddlSALoc.SelectedValue.ToString, "'", "''")
            End If

            '        Dim sql As String = "select 'Select Entity (optional) ' as lbl, ' -- none selected -- ' as Entity, 0 as ord " & _
            '"union " & _
            '"select distinct Entity, Entity, 1 " & _
            '"from DWH.WF.PPS_Submissions wf " & _
            '"left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '"join " & _
            '"(select distinct [MerchantDescription], " & _
            '"case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '"from DWH.WF.Merchant_LU " & _
            '"where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '"and [MerchantDescription] not like 'ZZ%'  " & _
            '"and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '"where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '"and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '"and exists (select * from DWH.WF.PPS_Submissions wf2 where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            'FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '"order by ord, lbl "

            Dim sql As String = "select 'Select Entity (optional) ' as lbl, '-1' as Entity, 0 as ord " & _
                "union " & _
                "select distinct isnull(f.Fac_Desc, 'Other -- (' + mil.MerchantDescription + ')') as Entity, isnull(cast(Fac_ID as bigint), mil.MerchantID) Entity, 1 " & _
                "from DWH.WF.PPS_Submissions wf " & _
                "left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
                "		   join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
                "				and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
                "					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
                "				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
                "					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
                "where wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") " & DepNumLimiter & _
                "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
                "and exists  (select * from DWH.WF.PPS_Submissions wf2 " & _
                "           left join DWH.wf.Merchant_ID_LU mil2 on wf2.PPSMerchantID = mil2.MerchantID and wf2.PPSStoreID = mil2.StoreID and wf2.PPSTerminalID = mil2.TerminalID " & _
                "           and mil.InactivatedDate is null and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "           where wf.DepositBagID = wf2.DepositBagID and (mil2.MerchantLocID = '" & _
                FilterLocation & "' or '" & FilterLocation & "' = '-1')) " & _
                "order by ord, lbl "

            ddlSAEntity.DataSource = GetData(sql)
            ddlSAEntity.DataValueField = "Entity"
            ddlSAEntity.DataTextField = "lbl"
            ddlSAEntity.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Sub SAUpdateUserDDL(x As Integer)

        Try

            Dim DepNumLimiter As String = ""
            If Len(Trim(txtSADepBag.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtSADepBag.Text), "'", "''") & "%'"
            End If
            If Len(Trim(txtSADepSlip.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtSADepSlip.Text), "'", "''") & "%' "
            End If
            Dim FilterEntity As String = "-1"
            If IsNothing(ddlSAEntity.SelectedItem) = False Then
                FilterEntity = Replace(ddlSAEntity.SelectedValue.ToString, "'", "''")
            End If
            Dim FilterLocation As String = "-1"
            If IsNothing(ddlSALoc.SelectedItem) = False Then
                FilterLocation = Replace(ddlSALoc.SelectedValue.ToString, "'", "''")
            End If

            'Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
            '    "union " & _
            '    "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '    "from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '    "and [MerchantDescription] not like 'ZZ%'  " & _
            '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '    "and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
            '    "where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '    FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '    "order by ord, lbl "

            ' 11/22/2019
            'Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
            '    "union " & _
            '    "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '  "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '  "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '  "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '  "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '  "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '  "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '  "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '  "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '  "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '  "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '  "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '  "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '  "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '  "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '  "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '  "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '  "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '  "from DWH.WF.Merchant_ID_LU mil" & _
            '  " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '  " and [MerchantDescription] not like 'ACC.%' " & _
            '  "and [MerchantDescription] not like 'ZZ%' " & _
            '  "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '    "and exists (select * from DWH.WF.PPS_Submissions wf2 " & _
            '    "where wf.DepositBagID = wf2.DepositBagID and (wf2.OutletTA = '" & _
            '    FilterLocation & "' or '" & FilterLocation & "' = ' -- none selected -- ')) " & _
            '    "order by ord, lbl "


            Dim sql As String = "select 'Select Submitter (optional) ' as lbl, ' -- none selected -- ' as SubmissionFullName, 0 as ord " & _
                "union " & _
                "select distinct SubmissionFullName, SubmissionFullName, 1 " & _
                "from DWH.WF.PPS_Submissions wf " & _
                "left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
               "		  left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
                "				and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
                "					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
                "				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
                "					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
              " where  [MerchantDescription] not like 'ACC.%' " & _
              "and [MerchantDescription] not like 'ZZ%' " & _
                "and wf.Active = 1 and (m.Manager is not null or 1 = " & x & ") " & DepNumLimiter & _
                "and (isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & FilterEntity & "' or '" & FilterEntity & "' = '-1') " & _
                "and exists  (select * from DWH.WF.PPS_Submissions wf2 " & _
                "           left join DWH.wf.Merchant_ID_LU mil2 on wf2.PPSMerchantID = mil2.MerchantID and wf2.PPSStoreID = mil2.StoreID and wf2.PPSTerminalID = mil2.TerminalID " & _
                "           and mil.InactivatedDate is null and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "           where wf.DepositBagID = wf2.DepositBagID and (mil2.MerchantLocID = '" & _
                FilterLocation & "' or '" & FilterLocation & "' = '-1')) " & _
                "order by ord, lbl "

            ddlSASubBy.DataSource = GetData(sql)
            ddlSASubBy.DataValueField = "SubmissionFullName"
            ddlSASubBy.DataTextField = "lbl"
            ddlSASubBy.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Sub SAUpdateLocationDDL(x As Integer)

        Try
            Dim DepNumLimiter As String = ""
            If Len(Trim(txtSADepBag.Text)) > 0 Then
                DepNumLimiter = "and DepositBagNumber like '%" & Replace(Trim(txtSADepBag.Text), "'", "''") & "%'"
            End If
            If Len(Trim(txtSADepSlip.Text)) > 0 Then
                DepNumLimiter += "and DepositSlipNumber like '%" & Replace(Trim(txtSADepSlip.Text), "'", "''") & "%' "
            End If
            Dim FilterEntity As String = "-1"
            If IsNothing(ddlSAEntity.SelectedItem) = False Then
                FilterEntity = Replace(ddlSAEntity.SelectedValue.ToString, "'", "''")
            End If
            Dim FilterSubmitter As String = " -- none selected -- "
            If IsNothing(ddlSASubBy.SelectedItem) = False Then
                FilterSubmitter = Replace(ddlSASubBy.SelectedValue.ToString, "'", "''")
            End If

            'Dim sql As String = "select 'Select Location (optional) ' as lbl, ' -- none selected -- ' as OutletTA, 0 as ord " & _
            '    "union " & _
            '    "select distinct OutletTA, OutletTA, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], " & _
            '    "case when [MerchantID] = '184188900999' then 'Atlanta'  " & _
            '    "when [MerchantID] = '184188902995' then 'Cherokee'  " & _
            '    "when [MerchantID] = '184188901997' then 'Forsyth'  " & _
            '    "when [MerchantID] = '184188903993' then 'GCS - Other'  " & _
            '    "when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '    "when [MerchantDescription] like 'M.%' then 'MedQuest'  " & _
            '    "when [MerchantDescription] like 'MedQuest%' then 'MedQuest'  " & _
            '    "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '    "from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ACC.%'  " & _
            '    "and [MerchantDescription] not like 'ZZ%'  " & _
            '    "and [MerchantID] not like '%E%' ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '    "order by ord, lbl "

            ' 11/22/2019
            'Dim sql As String = "select 'Select Location (optional) ' as lbl, ' -- none selected -- ' as OutletTA, 0 as ord " & _
            '    "union " & _
            '    "select distinct OutletTA, OutletTA, 1 " & _
            '    "from DWH.WF.PPS_Submissions wf " & _
            '    "left join DWH.WF.PPS_Management m on wf.SubmissionBy = m.UserLogin and m.Active = 1 " & _
            '    "join " & _
            '    "(select distinct [MerchantDescription], case when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '  "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' else [MerchantID] end as [MerchantID], " & _
            '  "case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '  "when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '  "when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '  "when [MerchantID] = '184188903993' then 'GCS - Other' " & _
            '  "when [MerchantID] = '174243397992' then 'GCS - ProFee' " & _
            '  "when [MerchantID] = '172123612993' then 'Duluth' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '  "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '  "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '  "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '  "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '  "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '  "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '  "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '  "when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '  "when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '  "else 'Other -- (' + [MerchantDescription] + ')' end as Entity " & _
            '  "from DWH.WF.Merchant_ID_LU mil" & _
            '  " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            '  " and [MerchantDescription] not like 'ACC.%' " & _
            '  "and [MerchantDescription] not like 'ZZ%' " & _
            '  "and [MerchantID] not like '%E%'  ) mp on wf.OutletTA = mp.MerchantDescription          " & _
            '    "where wf.Active = 1 and (m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = " & x & ") " & DepNumLimiter & _
            '    "and (Entity = '" & FilterEntity & "' or '" & FilterEntity & "' = ' -- none selected -- ') " & _
            '    "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
            '    "order by ord, lbl "

            Dim sql As String = "select 'Select Location (optional) ' as lbl, '-1' as OutletTA, 0 as ord " & _
                "union " & _
                "select distinct OutletTA, isnull(mil.MerchantLocID, -1), 1 " & _
                "from DWH.WF.PPS_Submissions wf " & _
                "left join DWH.WF.PPS_Management m on wf.SubmissionBy = isnull(m.UserLogin, wf.SubmissionBy) and m.Active = 1 and m.Manager = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' " & _
               "		  left join DWH.wf.Merchant_ID_LU mil on wf.PPSMerchantID = mil.MerchantID and wf.PPSStoreID = mil.StoreID and wf.PPSTerminalID = mil.TerminalID and mil.InactivatedDate is null " & _
                "				and DepositDate between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
                "					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
                "				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
                "					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
              " where mil.InactivatedDate is null and GETDATE() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
              " and [MerchantDescription] not like 'ACC.%' " & _
              "and [MerchantDescription] not like 'ZZ%' " & _
                "and wf.Active = 1 and (m.Manager is null or 1 = " & x & ") " & DepNumLimiter & _
                "and (isnull(cast(Fac_ID as bigint), mil.MerchantID) = '" & FilterEntity & "' or '" & FilterEntity & "' = '-1') " & _
                "and (SubmissionFullName = '" & FilterSubmitter & "' or '" & FilterSubmitter & "' = ' -- none selected -- ') " & _
                "order by ord, lbl "

            ddlSALoc.DataSource = GetData(sql)
            ddlSALoc.DataValueField = "OutletTA"
            ddlSALoc.DataTextField = "lbl"
            ddlSALoc.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub ddlSAEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSAEntity.SelectedIndexChanged
        Dim x As String = ddlSASubBy.SelectedValue.ToString
        Dim y As String = ddlSALoc.SelectedValue.ToString
        SAUpdateUserDDL(SuperAdmin)
        SAUpdateLocationDDL(SuperAdmin)
        Try
            ddlSASubBy.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlSALoc.SelectedValue = y
        Catch ex As Exception
        End Try

        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub ddlSASubBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSASubBy.SelectedIndexChanged

        Dim x As String = ddlSAEntity.SelectedValue.ToString
        Dim y As String = ddlSALoc.SelectedValue.ToString

        SAUpdateEntityDDL(SuperAdmin)
        SAUpdateLocationDDL(SuperAdmin)

        Try
            ddlSAEntity.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlSALoc.SelectedValue = y
        Catch ex As Exception
        End Try

        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub ddlSALoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSALoc.SelectedIndexChanged
        Dim x As String = ddlSAEntity.SelectedValue.ToString
        Dim y As String = ddlSASubBy.SelectedValue.ToString

        SAUpdateEntityDDL(SuperAdmin)
        SAUpdateUserDDL(SuperAdmin)

        Try
            ddlSAEntity.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlSASubBy.SelectedValue = y
        Catch ex As Exception
        End Try

        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub btnSARejectBag_Click(sender As Object, e As EventArgs) Handles btnSARejectReason.Click

        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
        UserName = Replace(tmblbl.Text, "'", "''")

        Dim updatesql As String = "Update DWH.WF.PPS_Submissions " & _
            "set Active = 0, Email = 1, " & _
            "EditedBy = '" & Replace(UserName, "'", "''") & "', " & _
            "EditComment = '" & Replace(txtSARejectReason.Text, "'", "''") & "', " & _
            "LastEditedDate = getdate() " & _
            "where DepositBagID = '" & Replace(lblSABagID.Text, "'", "''") & "'"

        Dim da As New SqlDataAdapter
        Dim cmd As SqlCommand
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
            cmd.ExecuteNonQuery()
            mpeSAexplanationLabel.Text = "Bag Rejected"
            mpeSAOKButton.Visible = False
            mpeSACancelButton.Text = "OK"
            mpeSACancelButton.Visible = True
            'ExplanationLabelReject.DataBind()

        End Using

        Dim x As String = ddlSASubBy.SelectedValue.ToString
        Dim y As String = ddlSAEntity.SelectedValue.ToString
        Dim z As String = ddlSALoc.SelectedValue.ToString

        SAUpdateUserDDL(SuperAdmin)
        Try
            ddlSASubBy.SelectedValue = x
        Catch ex As Exception
        End Try
        SAUpdateEntityDDL(SuperAdmin)
        Try
            ddlSAEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlSALoc.SelectedValue = z
        Catch ex As Exception
        End Try
        SAPopulateAdmin2Grid(SuperAdmin)

        txtSARejectReason.Text = ""
        lblSABagID.Text = ""
        txtSADepBagNo.Text = ""
        txtSADepDate.Text = ""
        lblSASubBy.Text = ""
        lblSADepBagTotal.Text = ""

        Dim fakeds As New DataTable
        gvSARows.DataSource = fakeds
        gvSARows.DataBind()
        mpeSA.Show()

    End Sub

    Private Sub txtSAStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtSAStartDate.TextChanged
        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub txtSAEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtSAEndDate.TextChanged
        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub txtSADepBag_TextChanged(sender As Object, e As EventArgs) Handles txtSADepBag.TextChanged
        Dim x As String = ddlSASubBy.SelectedValue.ToString
        Dim y As String = ddlSAEntity.SelectedValue.ToString
        Dim z As String = ddlSALoc.SelectedValue.ToString
        SAUpdateUserDDL(SuperAdmin)
        SAUpdateLocationDDL(SuperAdmin)
        SAUpdateEntityDDL(SuperAdmin)
        Try
            ddlSASubBy.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlSAEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlSALoc.SelectedValue = z
        Catch ex As Exception
        End Try

        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub txtSADepSlip_TextChanged(sender As Object, e As EventArgs) Handles txtSADepSlip.TextChanged
        Dim x As String = ddlSASubBy.SelectedValue.ToString
        Dim y As String = ddlSAEntity.SelectedValue.ToString
        Dim z As String = ddlSALoc.SelectedValue.ToString
        SAUpdateUserDDL(SuperAdmin)
        SAUpdateLocationDDL(SuperAdmin)
        SAUpdateEntityDDL(SuperAdmin)
        Try
            ddlSASubBy.SelectedValue = x
        Catch ex As Exception
        End Try
        Try
            ddlSAEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlSALoc.SelectedValue = z
        Catch ex As Exception
        End Try

        SAPopulateAdmin2Grid(SuperAdmin)
    End Sub

    Private Sub gvSARows_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvSARows.RowCancelingEdit
        Try
            gvSARows.EditIndex = -1
            gvSARows.DataSource = Rowsdv
            gvSARows.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSARows_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvSARows.RowEditing
        Try

            gvSARows.EditIndex = e.NewEditIndex
            gvSARows.DataSource = Rowsdv
            gvSARows.DataBind()

            Dim txtEODDate As TextBox = gvSARows.Rows(e.NewEditIndex).FindControl("txtSAEODCollectionDate")
            Dim lblEODDate As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSAEODCollectionDate")

            Dim txtEntity As TextBox = gvSARows.Rows(e.NewEditIndex).FindControl("txtSAEntity")
            Dim lblEntity As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSAEntity")

            Dim ddlOTA As DropDownList = gvSARows.Rows(e.NewEditIndex).FindControl("ddlSAOutletTA")
            Dim lblOTA As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSAOutletTA")

            Dim txtCash As TextBox = gvSARows.Rows(e.NewEditIndex).FindControl("txtSACash")
            Dim lblCash As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSACash")

            Dim txtManCheck As TextBox = gvSARows.Rows(e.NewEditIndex).FindControl("txtSAManualChecks")
            Dim lblManCheck As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSAManualChecks")

            Dim txtEOD As TextBox = gvSARows.Rows(e.NewEditIndex).FindControl("txtSAAgreeToEOD")
            Dim lblEOD As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSAAgreeToEOD")

            Dim txtExplain As TextBox = gvSARows.Rows(e.NewEditIndex).FindControl("txtSAExplain")
            Dim lblExplain As Label = gvSARows.Rows(e.NewEditIndex).FindControl("lblSAExplain")

            '            Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord  " & _
            '    "union " & _
            '    "select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ZZ%' and " & _
            '    "[MerchantDescription] not like 'ACC%' " & _
            '    " and (case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end ) = '" & lblEntity.Text & "' " & _
            '    "order by ord, [Merchant Description] "

            ' Clearly problematic 11/22/2019
            '            Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord  " & _
            '    "union " & _
            '    "select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 from DWH.WF.Merchant_LU " & _
            '    "where Active = 1 and [MerchantDescription] not like 'ZZ%' and " & _
            '    "[MerchantDescription] not like 'ACC%' " & _
            '    " and (case when [MerchantID] = '184188900999' then 'Atlanta' " & _
            '"when [MerchantID] = '184188902995' then 'Cherokee' " & _
            '"when [MerchantID] = '184188901997' then 'Forsyth' " & _
            '"when [MerchantID] = '184188903993' then 'GCS - Other'   " & _
            '"when [MerchantID] = '174243397992' then 'GCS - ProFee'  " & _
            '"when [MerchantID] = '172123612993' then 'Duluth' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett' " & _
            '  "when [MerchantID] = '172123639996' then 'Cancer Center' " & _
            '  "when [MerchantID] = '172123642990' then 'Gwinnett - FC' " & _
            '  "when [MerchantID] = '172123613991' then 'Gwinnett - ?' " & _
            '  "when [MerchantID] = '172123643998' then 'Duluth - FC' " & _
            '  "when [MerchantID] = '172123641992' then 'Glancy Rehab' " & _
            '  "when [MerchantID] = '184191774993' then 'North GA Diagnostic Profee' " & _
            '  "when [MerchantID] = '172123656990' then 'Gwinnett Medical Education' " & _
            '  "when [MerchantID] = '184188908992' then 'Radiation Oncology' " & _
            '"when [MerchantDescription] like 'M.%' then 'MedQuest' " & _
            '"when [MerchantDescription] like 'MedQuest%' then 'MedQuest' " & _
            '"else 'Other -- (' + [MerchantDescription] + ')' end ) = '" & lblEntity.Text & "' " & _
            '    "order by ord, [Merchant Description] "

            Dim ddlsetstring As String = "select top 1 'Select Outlet' as txt, ' -- noneselected -- ' as [Merchant Description], 0 as ord  " & _
                "union " & _
                "select distinct [MerchantDescription] as txt, [MerchantDescription] as ord, 1 " & _
                "from DWH.wf.Merchant_ID_LU mil  " & _
            "				left join  DWH.[WF].[Merchant_ID_2_Facility] m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
            "					and GETDATE() between isnull(m2f.EffectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999')  " & _
            "				left join  DWH.wf.Facility_LU f on f.Fac_ID = m2f.FacilityID and f.InactivatedDate is null " & _
            "					and GETDATE() between isnull(f.EffectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999')     " & _
            "                where mil.InactivatedDate is null and mil.MerchantDescription not like 'ZZ%' and mil.MerchantDescription not like 'ACC%' " & _
            "				and getdate() between isnull(mil.EffectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
            "and isnull(cast(Fac_ID as bigint), mil.MerchantID)= '" & lblEntity.Text & "' " & _
                "order by ord, [Merchant Description] "


            ddlOTA.Items.Clear()
            ddlOTA.DataSource = GetData(ddlsetstring)
            ddlOTA.DataValueField = "Merchant Description"
            ddlOTA.DataTextField = "txt"
            ddlOTA.DataBind()
            ddlOTA.SelectedValue = lblOTA.Text

            txtEODDate.Visible = True
            lblEODDate.Visible = False
            txtEntity.Visible = True
            lblEntity.Visible = False
            ddlOTA.Visible = True
            lblOTA.Visible = False
            txtCash.Visible = True
            lblCash.Visible = False
            txtManCheck.Visible = True
            lblManCheck.Visible = False
            txtEOD.Visible = True
            lblEOD.Visible = False
            txtExplain.Visible = True
            lblExplain.Visible = False

            For Each canoe As GridViewRow In gvSARows.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#6da9e3")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvSARows_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvSARows.RowUpdating
        Try
            Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
            UserName = Replace(tmblbl.Text, "'", "''")

            Dim depid As String = gvSARows.DataKeys(e.RowIndex).Value.ToString

            Dim txtEODDate As TextBox = gvSARows.Rows(e.RowIndex).FindControl("txtSAEODCollectionDate")
            Dim lblEODDate As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSAEODCollectionDate")

            Dim txtEntity As TextBox = gvSARows.Rows(e.RowIndex).FindControl("txtSAEntity")
            Dim lblEntity As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSAEntity")

            Dim lblOTA As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSAOutletTA")
            Dim ddlSAOutletTA As DropDownList = gvSARows.Rows(e.RowIndex).FindControl("ddlSAOutletTA")

            Dim txtCash As TextBox = gvSARows.Rows(e.RowIndex).FindControl("txtSACash")
            Dim lblCash As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSACash")

            Dim txtManCheck As TextBox = gvSARows.Rows(e.RowIndex).FindControl("txtSAManualChecks")
            Dim lblManCheck As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSAManualChecks")

            Dim txtEOD As TextBox = gvSARows.Rows(e.RowIndex).FindControl("txtSAAgreeToEOD")
            Dim lblEOD As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSAAgreeToEOD")

            Dim txtExplain As TextBox = gvSARows.Rows(e.RowIndex).FindControl("txtSAExplain")
            Dim lblExplain As Label = gvSARows.Rows(e.RowIndex).FindControl("lblSAExplain")

            Dim Sql As String = "update DWH.WF.PPS_Submissions set "


            If Trim(txtEODDate.Text) <> Trim(lblEODDate.Text) Then
                Sql = Sql & "EODCollectionDate = '" & Trim(Replace(txtEODDate.Text, "'", "''")) & "', "
            End If

            If Trim(txtEntity.Text) <> Trim(lblEntity.Text) Then
                Sql = Sql & "Entity = '" & Trim(Replace(txtEntity.Text, "'", "''")) & "', "
            End If

            If Trim(lblOTA.Text) <> Trim(ddlSAOutletTA.SelectedValue) Then
                Sql = Sql & "OutletTA = '" & Trim(Replace(ddlSAOutletTA.SelectedValue, "'", "''")) & "', "
            End If

            If Trim(txtCash.Text) <> Trim(lblCash.Text) Then
                Sql = Sql & "Cash = '" & Trim(Replace(txtCash.Text, "'", "''")) & "', "
            End If

            If Trim(txtManCheck.Text) <> Trim(lblManCheck.Text) Then
                Sql = Sql & "ManualChecks = '" & Trim(Replace(txtManCheck.Text, "'", "''")) & "', "
            End If

            If Trim(txtEOD.Text) <> Trim(lblEOD.Text) Then
                Sql = Sql & "AgreeToEOD = '" & Trim(Replace(txtEOD.Text, "'", "''")) & "', "
            End If

            If Trim(txtExplain.Text) <> Trim(lblExplain.Text) Then
                Sql = Sql & "Explain = '" & Trim(Replace(txtExplain.Text, "'", "''")) & "', "
            End If

            Sql = Sql & " EditComment = '" & Trim(Replace(txtSARejectReason.Text, "'", "''")) & "', EditedBy = '" & _
                UserName & "', LastEditedDate = getdate() where ID = '" & Replace(depid, "'", "''") & "'"

            ExecuteSql(Sql)

            gvSARows.EditIndex = -1
            SASelectBag(gvSABags.SelectedRow.Cells(1).Text)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

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

    Private Sub btnSAUpdateBag_Click(sender As Object, e As EventArgs) Handles btnSAUpdateBag.Click
        Dim tmblbl As Label = DirectCast(Master.FindControl("lblWelcome"), Label)
        UserName = Replace(tmblbl.Text, "'", "''")

        Dim updatesql As String = "Update DWH.WF.PPS_Submissions " & _
            "set DepositBagNumber = '" & Trim(Replace(txtSADepBagNo.Text, "'", "''")) & "', DepositSlipNumber = '" & Trim(Replace(txtSADepSlipNo.Text, "'", "''")) & "' " & _
            ", DepositDate = '" & Replace(txtSADepDate.Text, "'", "''") & "' " & _
            ", EditedBy = '" & Replace(UserName, "'", "''") & "', " & _
            "EditComment = '" & Replace(txtSARejectReason.Text, "'", "''") & "', " & _
            "LastEditedDate = getdate() " & _
            "where DepositBagID = '" & Replace(lblSABagID.Text, "'", "''") & "'"

        Dim da As New SqlDataAdapter
        Dim cmd As SqlCommand
        Dim ds As New DataSet
        Dim ds2 As New DataSet

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
            cmd.ExecuteNonQuery()
            mpeSAexplanationLabel.Text = "Bag Updated"
            mpeSAOKButton.Visible = False
            mpeSACancelButton.Text = "OK"
            mpeSACancelButton.Visible = True
            'ExplanationLabelReject.DataBind()

        End Using

        Dim x As String = ddlSASubBy.SelectedValue.ToString
        Dim y As String = ddlSAEntity.SelectedValue.ToString
        Dim z As String = ddlSALoc.SelectedValue.ToString

        SAUpdateUserDDL(SuperAdmin)
        Try
            ddlSASubBy.SelectedValue = x
        Catch ex As Exception
        End Try
        SAUpdateEntityDDL(SuperAdmin)
        Try
            ddlSAEntity.SelectedValue = y
        Catch ex As Exception
        End Try
        Try
            ddlSALoc.SelectedValue = z
        Catch ex As Exception
        End Try
        SAPopulateAdmin2Grid(SuperAdmin)

        txtSARejectReason.Text = ""
        lblSABagID.Text = ""
        txtSADepBagNo.Text = ""
        txtSADepDate.Text = ""
        lblSASubBy.Text = ""
        lblSADepBagTotal.Text = ""

        Dim fakeds As New DataTable
        gvSARows.DataSource = fakeds
        gvSARows.DataBind()
        mpeSA.Show()
    End Sub

    Private Sub mpeSAOKButton_Click(sender As Object, e As EventArgs) Handles mpeSAOKButton.Click
        For Each canoe As GridViewRow In gvSABags.Rows
            If canoe.RowIndex = gvSABags.SelectedIndex Then
                If canoe.Cells(8).Text = "0" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000")
                ElseIf canoe.Cells(8).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff2f")
                End If
            Else
                If canoe.Cells(8).Text = "0" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7d7d")
                ElseIf canoe.Cells(8).Text = "1" Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffaa")
                End If
            End If
        Next
    End Sub
End Class