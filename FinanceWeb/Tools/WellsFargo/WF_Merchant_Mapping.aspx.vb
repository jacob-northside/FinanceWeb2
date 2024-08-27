Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security


Imports FinanceWeb.WebFinGlobal

Public Class WF_Merchant_Mapping
    Inherits System.Web.UI.Page
    Public Shared MappedView As New DataView
    Public Shared UnMappedView As New DataView
    Public Shared sortmap As String
    Public Shared sortunmap As String
    Public Shared mapdir As Integer
    Public Shared unmapdir As Integer
    Private Shared Admin As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else

                Dim zz As String = "select count(*) from WebFD.dbo.aspnet_Roles r " & _
                "join WebFD.dbo.aspnet_UsersInRoles uir on r.RoleId = uir.RoleId " & _
                "join WebFD.dbo.aspnet_Users u on uir.UserId = u.UserId " & _
                "where RoleName = 'Developer' and UserName = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "

                Admin = GetScalar(zz)
                DDLPopulate()
                loadgrid()

            End If

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

    Private Sub DDLPopulate()

        Dim s As String = "select '-1' as MerchantID, '(Filter MerchantID)' as MerchantDescription, 0 as ord  " & _
                "union " & _
               "select distinct isnull(f.Fac_ID, -1) as MerchantID,  isnull(Fac_Desc, mil.merchantDescription), case when Fac_ID is null then 2 else 1 end as Ord " & _
                "from DWH.WF.Merchant_ID_LU mil " & _
                "				left join DWH.wf.Merchant_ID_2_Facility m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
                "    and getdate() between isnull(m2f.effectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999') " & _
                "left join DWH.wf.Facility_LU f on m2f.FacilityID = f.Fac_ID and f.InactivatedDate is null " & _
                    "and getdate() between isnull(f.effectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999') " & _
                "where getdate() between isnull(mil.effectiveFrom, '1/1/1800') and isnull(mil.EffectiveTo, '12/31/9999') " & _
                "and mil.InactivatedDate is null " & _
                " order by ord, MerchantDescription  "


        ' /* switching out to full CDC 5/28/2019; EffectiveFrom/To not actually implemented yet, but the new table's built out appropriately if necessary */
        'Dim s As String = "select ' -- noneselected -- ' as MerchantID, '(Filter MerchantID)' as MerchantDescription, 0 as ord  " & _
        '"union " & _
        '"select case when MerchantDescription like 'M.%' then 'MedQuest'  " & _
        '"            when MerchantDescription like 'MedQuest%' then 'MedQuest' else MerchantID end as MerchantID,  " & _
        '"            case when MerchantID = '184188900999' then 'Atlanta'  " & _
        '"            when MerchantID = '184188902995' then 'Cherokee'  " & _
        '"            when MerchantID = '184188901997' then 'Forsyth'  " & _
        '"            when MerchantID = '184188903993' then 'GCS - Other'  " & _
        '"            when MerchantID = '174243397992' then 'GCS - ProFee'  " & _
        '"            when MerchantDescription like 'M.%' then 'MedQuest'  " & _
        '"            when MerchantDescription like 'MedQuest%' then 'MedQuest'  " & _
        '"            else 'Other -- (' + MerchantID + ')' end as MerchantDescription  " & _
        '"			, case when MerchantID = '184188900999' then 1 " & _
        '"            when MerchantID = '184188902995' then 1 " & _
        '"            when MerchantID = '184188901997' then 1 " & _
        '"            when MerchantID = '184188903993' then 1 " & _
        '"            when MerchantID = '174243397992' then 1 " & _
        '"            when MerchantDescription like 'M.%' then 1 " & _
        '"            when MerchantDescription like 'MedQuest%' then 1 " & _
        '"            else 2 end " & _
        '"			from  DWH.WF.Merchant_LU " & _
        '"where Active = 1 " & _
        '" order by ord, MerchantDescription  "



        ddlMerchantSelect.DataSource = GetData(s)
        ddlMerchantSelect.DataValueField = "MerchantID"
        ddlMerchantSelect.DataTextField = "MerchantDescription"
        ddlMerchantSelect.DataBind()

    End Sub

    Private Sub loadgrid()

        Dim se As String = UnMappedView.Sort
        Dim pg As Integer = gvUnMapped.PageIndex

        Dim gridsql As String = "select * from ( " & _
            "select f.Fac_Desc, f.Fac_ID as FacID, mil.*, case when mil.InactivatedDate is null then 'Remove' else 'Add' end as Activation " & _
                ", case when mil.MerchantID like '%E%' then null else convert(bigint, mil.MerchantID) end as MerchIDSort " & _
                        ", convert(integer, StoreID) as StoreSort, isnull(OutletTypeID, -1) as OutletTypeID_Null  " & _
                        ", convert(integer, TerminalID) as TermSort, isnull(BusinessDayCount, -1) as BusinessDayCount_Null  " & _
                        ", isnull(mpf.POSFacilityID, -1) as POSFacility_Null " & _
                        ", ROW_NUMBER() over (partition by mil.MerchantLocID order by case when mil.InactivatedDate is null then 1 else 0 end desc, mil.InactivatedDate desc) RN " & _
            "from DWH.WF.Merchant_ID_LU mil " & _
            "left join DWH.wf.Merchant_ID_2_Facility m2f on mil.MerchantID = m2f.MerchantID and m2f.InactivatedDate is null " & _
            "	    and getdate() between isnull(m2f.effectiveFrom, '1/1/1800') and isnull(m2f.EffectiveTo, '12/31/9999') " & _
            "left join DWH.wf.Facility_LU f on m2f.FacilityID = f.Fac_ID and f.InactivatedDate is null " & _
                "and getdate() between isnull(f.effectiveFrom, '1/1/1800') and isnull(f.EffectiveTo, '12/31/9999') " & _
            "left join DWH.wf.Merchant_ID_2_POSFacility mpf on mpf.MerchantLocID = mil.MerchantLocID and mpf.InactivatedDate is null " & _
            "where getdate() between isnull(mpf.effectiveFrom, '1/1/1800') and isnull(mpf.EffectiveTo, '12/31/9999') " & _
            ") x where RN = 1 and (isnull(FacID, -1) = '" & Replace(ddlMerchantSelect.SelectedValue, "'", "''") & "' or '" & Replace(ddlMerchantSelect.SelectedValue, "'", "''") & "' = '-1') "

        If cbMerchShowActives.Checked Then
        Else
            gridsql += " and InactivatedDate is null "
        End If

        ' /* switching out to full CDC 5/28/2019; EffectiveFrom/To not actually implemented yet, but the new table's built out appropriately if necessary */
        'Dim gridsql As String = "select m.*, case when Active = 1 then 'Remove' else 'Add' end as Activation " & _
        '    ", case when m.MerchantID like '%E%' then null else convert(bigint, m.MerchantID) end as MerchIDSort " & _
        '    ", convert(integer, m.StoreID) as StoreSort " & _
        '    ", convert(integer, m.TerminalID) as TermSort " & _
        '    "from DWH.WF.Merchant_LU m " & _ 
        '    "where not exists (select * from DWH.WF.Merchant_LU m2 where m.MerchantID = m2.MerchantID and m.StoreID = m2.StoreID and m.TerminalID = m2.TerminalID " & _
        '    "			and m2.Active >= m.Active and (m2.ModifyDate > m.ModifyDate or (m2.ModifyDate = m.ModifyDate and m2.InternalID > m.InternalID))) "

        'If ddlMerchantSelect.SelectedValue = "MedQuest" Then
        '    gridsql += " and (MerchantDescription like 'M.%' or MerchantDescription like 'MedQuest%') "
        'Else
        '    gridsql += " and (m.MerchantID = '" & Replace(ddlMerchantSelect.SelectedValue, "'", "''") & "' or '" & Replace(ddlMerchantSelect.SelectedValue, "'", "''") & "' = ' -- noneselected -- ') "
        'End If

        'If cbMerchShowActives.Checked Then
        'Else
        '    gridsql += " and Active = 1 "
        'End If

        UnMappedView = GetData(gridsql).DefaultView
        UnMappedView.Sort = se
        gvUnMapped.DataSource = UnMappedView
        gvUnMapped.DataBind()
        gvUnMapped.ShowHeaderWhenEmpty = True
        gvUnMapped.PageIndex = pg


    End Sub

    Private Sub gvUnMapped_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvUnMapped.PageIndexChanging
        Try

            gvUnMapped.PageIndex = e.NewPageIndex
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvUnMapped_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvUnMapped.RowCancelingEdit
        Try
            gvUnMapped.EditIndex = -1
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvUnMapped_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUnMapped.RowCommand
        Try
            Dim ID As String = e.CommandArgument
            Dim varname As String = e.CommandName

            If varname = "AugmentActive" Then

                Dim Sql As String = "declare @Status int = (select isnull(count(*), 0) from DWH.wf.Merchant_ID_LU where InactivatedDate is null and MerchantLocID = '" & Replace(ID, "'", "''") & "') " & _
                    "/* Get count to figure out which way the flip is going */" & _
                    " insert into DWH.wf.Merchant_ID_LU (MerchantLocID, FacilityID, MerchantID, StoreID, TerminalID, MerchantDescription, OutletTypeID, BusinessDayCount, EffectiveFrom, EffectiveTo, AddedBy, AddedDate) " & _
                   "select top 1 MerchantLocID, FacilityID, MerchantID, StoreID, TerminalID, MerchantDescription, OutletTypeID, BusinessDayCount, EffectiveFrom, EffectiveTo " & _
                   ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                   "from  DWH.wf.Merchant_ID_LU a " & _
                   "where not exists (select * from  DWH.wf.Merchant_ID_LU b where a.MerchantLocID = b.MerchantLocID and b.InactivatedDate is null) " & _
                   "and a.MerchantLocID = '" & Replace(ID, "'", "''") & "'  " & _
                   "order by InactivatedDate desc " & _
                   " " & _
                   "update DWH.wf.Merchant_ID_LU " & _
                   "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                   "where InactivatedDate is null and MerchantLocID = '" & Replace(ID, "'", "''") & "' and @Status > 0 "

                ' /* switching out to full CDC 5/28/2019; EffectiveFrom/To not actually implemented yet, but the new table's built out appropriately if necessary */

                'Dim Sql As String = "update w   set Active = 1 - Active, ModifyUser = '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & _
                '    "', ModifyDate = getdate() from DWH.WF.Merchant_LU w where InternalID = '" & Replace(ID, "'", "''") & "' and not exists " & _
                '    " (select * from DWH.WF.Merchant_LU w2  where w.MerchantID = w2.MerchantID and w.StoreID = w2.StoreID and w.TerminalID = w2.TerminalID and w2.Active = 1 and w2.InternalID <> w.InternalID) "


                ExecuteSql(Sql)
                loadgrid()

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub gvUnMapped_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvUnMapped.RowCreated
        Try
            'If Admin = 0 Then
            '    e.Row.Cells(5).CssClass = "hidden"
            '    e.Row.Cells(6).CssClass = "hidden"
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvUnMapped_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUnMapped.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblOutletType As Label = e.Row.FindControl("lblOutletType")
            Dim ddlOutletType As DropDownList = e.Row.FindControl("ddlOutletType")
            Dim CC As String = "select '(Select Outlet Type)' as OutletTypeDescription, -1 as OutletTypeID, 0 as ord union " & _
                " select distinct OutletTypeDescription, OutletTypeID, 1 from DWH.WF.Merchant_OutletType_LU " & _
                "where InactivatedDate is null and getdate() between isnull(EffectiveFrom, '1/1/1800') and isnull(EffectiveTo, '12/31/9999') " & _
                "order by  ord, 1 "

            ddlOutletType.DataSource = GetData(CC)
            ddlOutletType.DataTextField = "OutletTypeDescription"
            ddlOutletType.DataValueField = "OutletTypeID"
            ddlOutletType.DataBind()

            Try
                ddlOutletType.SelectedValue = lblOutletType.Text

            Catch ex As Exception
            End Try

            Dim lblBusinessDayCount As Label = e.Row.FindControl("lblBusinessDayCount")
            Dim ddlBusinessDayCount As DropDownList = e.Row.FindControl("ddlBusinessDayCount")

            Try
                ddlBusinessDayCount.SelectedValue = lblBusinessDayCount.Text

            Catch ex As Exception
            End Try

            'Dim lblPOSCategory As Label = e.Row.FindControl("lblPOSCategory")
            'Dim ddlPOSCategory As DropDownList = e.Row.FindControl("ddlPOSCategory")
            'Dim cat As String = "select '(Select Category)' as CategoryDescription, -1 as CategoryID, 0 as ord union " & _
            '    " select distinct Description, POSCategoryID, 1 from DWH.wf.POSCategory_LU " & _
            '    "where InactivatedDate is null " & _
            '    "order by  ord, 1 "

            'ddlPOSCategory.DataSource = GetData(cat)
            'ddlPOSCategory.DataTextField = "CategoryDescription"
            'ddlPOSCategory.DataValueField = "CategoryID"
            'ddlPOSCategory.DataBind()

            'Try
            '    ddlPOSCategory.SelectedValue = lblPOSCategory.Text

            'Catch ex As Exception
            'End Try

            Dim lblPOSFacility As Label = e.Row.FindControl("lblPOSFacility")
            Dim ddlPOSFacility As DropDownList = e.Row.FindControl("ddlPOSFacility")
            Dim fac As String = "select '(Select Facility)' as FacDescription, -1 as FacID, 0 as ord union " & _
                " select distinct Description, POSFacilityID, 1 from DWH.WF.POSFacility_LU " & _
                "where InactivatedDate is null  " & _
                "order by  ord, 1 "

            ddlPOSFacility.DataSource = GetData(fac)
            ddlPOSFacility.DataTextField = "FacDescription"
            ddlPOSFacility.DataValueField = "FacID"
            ddlPOSFacility.DataBind()

            Try
                ddlPOSFacility.SelectedValue = lblPOSFacility.Text

            Catch ex As Exception
            End Try


        End If
    End Sub


    Private Sub gvUnMapped_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvUnMapped.RowEditing
        Try

            gvUnMapped.EditIndex = e.NewEditIndex
            gvUnMapped.DataSource = UnMappedView
            gvUnMapped.DataBind()

            Dim txtMerchantDescription As TextBox = gvUnMapped.Rows(e.NewEditIndex).FindControl("txtMerchantDescription")
            Dim lblMerchantDescription As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblMerchantDescription")

            Dim ddlOutletType As DropDownList = gvUnMapped.Rows(e.NewEditIndex).FindControl("ddlOutletType")
            Dim lblOutletType As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblOutletType")


            Dim ddlBusinessDayCount As DropDownList = gvUnMapped.Rows(e.NewEditIndex).FindControl("ddlBusinessDayCount")
            Dim lblBusinessDayCount As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblBusinessDayCount")

            'Dim ddlPOSCategory As DropDownList = gvUnMapped.Rows(e.NewEditIndex).FindControl("ddlPOSCategory")
            'Dim lblPOSCategory As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblPOSCategory")

            Dim ddlPOSFacility As DropDownList = gvUnMapped.Rows(e.NewEditIndex).FindControl("ddlPOSFacility")
            Dim lblPOSFacility As Label = gvUnMapped.Rows(e.NewEditIndex).FindControl("lblPOSFacility")


            txtMerchantDescription.Visible = True
            lblMerchantDescription.Visible = False

            ddlOutletType.Enabled = True

            ddlBusinessDayCount.Enabled = True
            'ddlPOSCategory.Enabled = True
            ddlPOSFacility.Enabled = True


            For Each canoe As GridViewRow In gvUnMapped.Rows
                If canoe.RowIndex = e.NewEditIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
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
    Private Sub btnAddLine_Click(sender As Object, e As EventArgs) Handles btnAddLine.Click

        Try


            If Len(Trim(Replace(txtMerchID.Text, "'", "''"))) = 0 Then
                explantionlabel.Text = "Merchant ID is required for new mapping submissions."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf Len(Trim(Replace(txtStoreID.Text, "'", "''"))) = 0 Then
                explantionlabel.Text = "Store ID is required for new mapping submissions."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf Len(Trim(Replace(txtTerminalID.Text, "'", "''"))) = 0 Then
                explantionlabel.Text = "Terminal ID is required for new mapping submissions."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            Dim n As Int64
            Dim m As Int64
            Dim o As Int64

            If Int64.TryParse(txtMerchID.Text, n) = False Then
                explantionlabel.Text = "Merchant ID must be an integer value."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf Int64.TryParse(txtStoreID.Text, m) = False Then
                explantionlabel.Text = "Store ID must be an integer value."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            ElseIf Int64.TryParse(txtTerminalID.Text, o) = False Then
                explantionlabel.Text = "Terminal ID must be an integer value."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            ' Swapped out m with txtMerchID Re: Richard TerminalID 0311 <> 00311 CRW 12/20/2019
            Dim CheckSql As String = "select count(*) from DWH.WF.Merchant_ID_LU " & _
            "where MerchantID = '" & Trim(Replace(txtMerchID.Text, "'", "''")) & "' and StoreID = '" & Trim(Replace(txtStoreID.Text, "'", "''")) & _
            "' and TerminalID = '" & Trim(Replace(txtTerminalID.Text, "'", "''")) & "' " & _
            "and InactivatedDate is null "


            ' /* switching out to full CDC 5/28/2019 */
            'Dim CheckSql As String = "select count(*) from DWH.WF.Merchant_LU " & _
            '"where convert(bigint, MerchantID) = '" & Trim(Replace(n, "'", "''")) & "' and convert(int, StoreID) = '" & Trim(Replace(m, "'", "''")) & _
            '"' and convert(int, TerminalID) = '" & Trim(Replace(o, "'", "''")) & "' " & _
            '"and Active = 1 "

            If GetScalar(CheckSql) > 0 Then
                explantionlabel.Text = "There is already a row with this Merchant/Store/Terminal combination."
                ConfirmButton.Visible = False
                OkButton.Visible = False
                CancelButton.Visible = True
                ModalPopupExtender1.Show()
                Exit Sub
            End If


            Dim InsertSql As String = "/* Insert new row if it never existed */" & _
            " Insert into DWH.WF.Merchant_ID_LU  (MerchantLocID, MerchantID, MerchantDescription,  StoreID, TerminalID, AddedDate, AddedBy)  " & _
            "Select (select isnull(max(MerchantLocID), 0) from DWH.WF.Merchant_ID_LU) + 1,  '" & Trim(Replace(txtMerchID.Text, "'", "''")) & "', '" & Trim(Replace(txtMerchDesc.Text, "'", "''")) & "', '" & Trim(Replace(txtStoreID.Text, "'", "''")) & "', '" & _
            Trim(Replace(txtTerminalID.Text, "'", "''")) & "',  getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
            " where not exists (select * from  DWH.wf.Merchant_ID_LU b where MerchantID = '" & Trim(Replace(txtMerchID.Text, "'", "''")) & "' and StoreID = '" & Trim(Replace(txtStoreID.Text, "'", "''")) & _
            "' and TerminalID = '" & Trim(Replace(txtTerminalID.Text, "'", "''")) & "' ) " & _
            "/* Insert new row if it already existed previously */ " & _
            " insert into DWH.wf.Merchant_ID_LU (MerchantLocID, FacilityID, MerchantID, StoreID, TerminalID, MerchantDescription, OutletTypeID, BusinessDayCount, EffectiveFrom, EffectiveTo, AddedBy, AddedDate) " & _
               "select top 1 MerchantLocID, FacilityID, MerchantID, StoreID, TerminalID, '" & Trim(Replace(txtMerchDesc.Text, "'", "''")) & "', OutletTypeID, BusinessDayCount, EffectiveFrom, EffectiveTo " & _
               ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
               "from  DWH.wf.Merchant_ID_LU a " & _
               "where MerchantID = '" & Trim(Replace(txtMerchID.Text, "'", "''")) & "' and StoreID = '" & Trim(Replace(txtStoreID.Text, "'", "''")) & _
            "' and TerminalID = '" & Trim(Replace(txtTerminalID.Text, "'", "''")) & "' and " & _
            " not exists (select * from  DWH.wf.Merchant_ID_LU b where a.MerchantLocID = b.MerchantLocID and b.InactivatedDate is null) " & _
               "order by InactivatedDate desc "

            ExecuteSql(InsertSql)

            ' /* switching out to full CDC 5/28/2019 */
            'Dim InsertSql As String = "Insert into DWH.WF.Merchant_LU  " & _
            '"Select '" & Trim(Replace(txtMerchID.Text, "'", "''")) & "', '" & Trim(Replace(txtMerchDesc.Text, "'", "''")) & "', '" & Trim(Replace(txtStoreID.Text, "'", "''")) & "', '" & _
            'Trim(Replace(txtTerminalID.Text, "'", "''")) & "', 1, getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' "
            'ExecuteSql(InsertSql)

            txtMerchID.Text = ""
            txtStoreID.Text = ""
            txtTerminalID.Text = ""
            txtMerchDesc.Text = ""
            explantionlabel.Text = "Row added successfully."
            ConfirmButton.Visible = False
            OkButton.Visible = True
            CancelButton.Visible = False
            loadgrid()
            ModalPopupExtender1.Show()

        Catch ex As Exception
            explantionlabel.Text = "Error updating row; Please report to Admin"
            ModalPopupExtender1.Show()
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Private Sub gvUnMapped_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvUnMapped.RowUpdating
        Try
            Dim depid As String = gvUnMapped.DataKeys(e.RowIndex).Value.ToString

            Dim txtMerchantDescription As TextBox = gvUnMapped.Rows(e.RowIndex).FindControl("txtMerchantDescription")
            Dim lblMerchantDescription As Label = gvUnMapped.Rows(e.RowIndex).FindControl("lblMerchantDescription")

            Dim ddlOutletType As DropDownList = gvUnMapped.Rows(e.RowIndex).FindControl("ddlOutletType")
            Dim lblOutletType As Label = gvUnMapped.Rows(e.RowIndex).FindControl("lblOutletType")

            Dim ddlBusinessDayCount As DropDownList = gvUnMapped.Rows(e.RowIndex).FindControl("ddlBusinessDayCount")
            Dim lblBusinessDayCount As Label = gvUnMapped.Rows(e.RowIndex).FindControl("lblBusinessDayCount")

            'Dim ddlPOSCategory As DropDownList = gvUnMapped.Rows(e.RowIndex).FindControl("ddlPOSCategory")
            'Dim lblPOSCategory As Label = gvUnMapped.Rows(e.RowIndex).FindControl("lblPOSCategory")

            Dim ddlPOSFacility As DropDownList = gvUnMapped.Rows(e.RowIndex).FindControl("ddlPOSFacility")
            Dim lblPOSFacility As Label = gvUnMapped.Rows(e.RowIndex).FindControl("lblPOSFacility")

            Dim chg As Int16 = 0

            If txtMerchantDescription.Text <> lblMerchantDescription.Text Then
                chg += 1
            ElseIf ddlOutletType.SelectedValue <> lblOutletType.Text Then
                chg += 1
            ElseIf ddlBusinessDayCount.SelectedValue <> lblBusinessDayCount.Text Then
                chg += 1
                'ElseIf ddlPOSCategory.SelectedValue <> lblPOSCategory.Text Then
                '    Dim pos As String = "Update DWH.wf.Merchant_ID_2_POSCategory set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                '    " where MerchantLocID = '" & Replace(depid, "'", "''") & "' and InactivatedDate is null " & _
                '    "Insert into DWH.wf.Merchant_ID_2_POSCategory (POSCategoryID, MerchantLocID, AddedBy, AddedDate) " & _
                '    "values ('" & Replace(ddlPOSCategory.SelectedValue, "'", "''") & "', '" & Replace(depid, "'", "''") & "', '" & _
                '    Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() ) "
                '    ExecuteSql(pos)

            End If

            If ddlPOSFacility.SelectedValue <> lblPOSFacility.Text Then
                Dim fac As String = "Update DWH.wf.Merchant_ID_2_POSFacility set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                " where MerchantLocID = '" & Replace(depid, "'", "''") & "' and InactivatedDate is null " & _
                "Insert into DWH.wf.Merchant_ID_2_POSFacility (POSFacilityID, MerchantLocID, AddedBy, AddedDate) " & _
                "values ('" & Replace(ddlPOSFacility.SelectedValue, "'", "''") & "', '" & Replace(depid, "'", "''") & "', '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() ) "
                ExecuteSql(fac)
            End If


            If chg > 0 Then
                Dim Sql As String = "update DWH.WF.Merchant_ID_LU  " & _
                "set InactivatedDate = getdate(), InactivatedBy = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' " & _
                " where MerchantLocID = '" & Replace(depid, "'", "''") & "' and InactivatedDate is null " & _
                " insert into DWH.wf.Merchant_ID_LU (MerchantLocID, FacilityID, MerchantID, StoreID, TerminalID, MerchantDescription, OutletTypeID, BusinessDayCount, EffectiveFrom, EffectiveTo, AddedBy, AddedDate) " & _
                   "select top 1 MerchantLocID, FacilityID, MerchantID, StoreID, TerminalID, '" & Replace(Trim(txtMerchantDescription.Text), "'", "''") & "', " & _
                   "'" & Replace(ddlOutletType.SelectedValue, "'", "''") & "', '" & Replace(ddlBusinessDayCount.SelectedValue, "'", "''") & "' , EffectiveFrom, EffectiveTo " & _
                   ", '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', getdate() " & _
                   "from  DWH.wf.Merchant_ID_LU a " & _
                   "where MerchantLocID = '" & Replace(depid, "'", "''") & "' and " & _
                " not exists (select * from  DWH.wf.Merchant_ID_LU b where a.MerchantLocID = b.MerchantLocID and b.InactivatedDate is null) " & _
                   "order by InactivatedDate desc "

                'Dim Sql As String = "update DWH.WF.Merchant_LU  " & _
                '    "set Active = 0 where InternalID = '" & Replace(depid, "'", "''") & "' " & _
                '    " Insert into DWH.WF.Merchant_LU select MerchantID, '" & Replace(Trim(txtMerchantDescription.Text), "'", "''") & "', StoreID, TerminalID, 1, getdate(), '" & _
                '     Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' from DWH.WF.Merchant_LU where InternalID = '" & Replace(depid, "'", "''") & "' "

                ExecuteSql(Sql)
            End If


            gvUnMapped.EditIndex = -1
            loadgrid()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    Private Sub gvUnMapped_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvUnMapped.Sorting
        Dim dv As DataView
        Dim sorts As String
        dv = UnMappedView

        sorts = e.SortExpression

        If e.SortExpression = sortunmap Then

            If unmapdir = 1 Then
                dv.Sort = sorts + " " + "desc"
                unmapdir = 0
            Else
                dv.Sort = sorts + " " + "asc"
                unmapdir = 1
            End If

        Else
            dv.Sort = sorts + " " + "asc"
            unmapdir = 1
            sortunmap = e.SortExpression
        End If

        gvUnMapped.DataSource = dv
        gvUnMapped.DataBind()
    End Sub


    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click

        ModalPopupExtender1.Hide()

    End Sub


    Private Sub cbMerchShowActives_CheckedChanged(sender As Object, e As EventArgs) Handles cbMerchShowActives.CheckedChanged
        loadgrid()
    End Sub

    Private Sub ddlMerchantSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMerchantSelect.SelectedIndexChanged
        loadgrid()
    End Sub
End Class