Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices

Imports FinanceWeb.WebFinGlobal

'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Web
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Data.OleDb
'Imports System.Configuration
'Imports System.Web.Script.Services


Imports System.Security.Principal


Public Class StarGLBalancing
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim cmd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If User.Identity.IsAuthenticated And User.IsInRole("StarGL") Then
                hlOpenJECorrections2.Visible = True
                gvFarDAP.Enabled = True

            Else
                '   gvFARGLD_CFY.Columns(0).Visible = False
                hlOpenJECorrections2.Visible = False
                gvFarDAP.Enabled = False
            End If
            If IsPostBack Then
                Page.MaintainScrollPositionOnPostBack = True
                If CDate(EndDateTextBox.Text) < CDate(StartDateTextBox.Text) Then
                    EndDateTextBox.Text = StartDateTextBox.Text
                End If
            Else
                'StartDateTextBox.Text = Today.Date.AddDays(-3)
                'EndDateTextBox.Text = Today.Date
                If IsDBNull(Session("FARDAPStartDate")) Or Session("FARDAPStartDate") = "" Then
                    StartDateTextBox.Text = Today.Date.AddDays(-3)
                Else
                    StartDateTextBox.Text = Session("FARDAPStartDate")
                End If
                If IsDBNull(Session("FARDAPEndDate")) Or Session("FARDAPEndDate") = "" Then
                    EndDateTextBox.Text = Today.Date
                Else
                    EndDateTextBox.Text = Session("FARDAPEndDate")
                End If
                If IsDBNull(Session("FARDAPFac")) Or Session("FARDAPFac") = "" Then
                    rblFacilityFilter.SelectedValue = "%"
                Else
                    rblFacilityFilter.SelectedValue = Session("FARDAPFac")
                End If
                Me.BindGrid()
            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Sub BindGrid()

        Try


            Dim SQL As String = ""
            Dim SQLFac As String = "  "
            SQLFac = rblFacilityFilter.SelectedValue

            If rblFacilityFilter.Items.Item(0).Selected = True Then
                SQLFac = " and FAC like '%' "
            ElseIf rblFacilityFilter.Items.Item(1).Selected = True Then
                SQLFac = " and FAC = 'A' "
            ElseIf rblFacilityFilter.Items.Item(2).Selected = True Then
                SQLFac = " and FAC = 'C' "
            ElseIf rblFacilityFilter.Items.Item(3).Selected = True Then
                SQLFac = " and FAC = 'F' "
            ElseIf rblFacilityFilter.Items.Item(4).Selected = True Then
                SQLFac = " and FAC = 'D' "
            ElseIf rblFacilityFilter.Items.Item(5).Selected = True Then
                SQLFac = " and FAC = 'L' "
            Else
                SQLFac = "  "
            End If

            If cb_UnbalancedAccounts.Checked Then
            Else

                SQLFac += " and a.PAT_ACCT_FAC in " & _
                    "( " & _
                    "select distinct Pat_Acct_Fac from ( " & _
                    "select Dept, SubAcct, Pat_Acct_Fac,  sum(Debit - Credit) bl " & _
                    "from DWH.GL.FARGLD_CORRECTIONS_DS a with (nolock) " & _
                    "where Dept = 9999  " & _
                    "group by Dept, SubAcct, Pat_Acct_Fac " & _
                    "having  sum(Debit - Credit) <> 0 ) x ) "
            End If


            If chbIncludeIgnored.Checked = True Then
                SQL = "SELECT [ID], [FY], [FM], [Fac], [Dept], [SubAcct], convert(varchar, Convert(Date, [TranDate])) Trandate, '$' + convert(varchar(12), [Debit], 1) Debit, '$' + convert(varchar(12), [Credit], 1) Credit, [Qty], [ContrAcct], [Pat_Acct_Fac], comments, IgnoreRecord, HoldRecord, b.TranCode, b.TranCodeDesc, GLEffectiveDate  FROM DWH.GL.FARGLD_CORRECTIONS_DS a left join DWH.GL.FARGLD_TranCode_DS b on a.TranCodeID = b.TranCodeid  WHERE isnull(a.Active, 1) = 1 and Trandate between '" & StartDateTextBox.Text & "' and '" & EndDateTextBox.Text & "' and ISCorrected is null " & SQLFac & " ORDER BY FAC,[TranDate] DESC, [Pat_Acct_Fac] DESC,  (case when Debit = '0.00' then Credit else Debit End ) desc"
            Else
                SQL = "SELECT [ID], [FY], [FM], [Fac], [Dept], [SubAcct], convert(varchar, Convert(Date, [TranDate])) Trandate, '$' + convert(varchar(12), [Debit], 1) Debit, '$' + convert(varchar(12), [Credit], 1) Credit, [Qty], [ContrAcct], [Pat_Acct_Fac], comments, IgnoreRecord, HoldRecord, b.TranCode, b.TranCodeDesc, GLEffectiveDate  " & _
                         "   FROM DWH.GL.FARGLD_CORRECTIONS_DS a " & _
                         "  left join DWH.GL.FARGLD_TranCode_DS b on a.TranCodeID = b.TranCodeid  " & _
                         "  WHERE isnull(a.Active, 1) = 1 and Trandate between '" & StartDateTextBox.Text & "' and '" & EndDateTextBox.Text & "' and ISCorrected is null  " & SQLFac & " and (ignoreRecord is null or ignorerecord = 0 ) ORDER BY FAC,[TranDate] DESC, [Pat_Acct_Fac] DESC,  (case when Debit = '0.00' then Credit else Debit End ) desc"
            End If
            Dim cmd2 As New SqlCommand(SQL)

            'Note:  Fargld_Corrections Active flag added 3/2/2018 CRW

            'Dim cmd2 As New SqlCommand("SELECT [ID], [FY], [FM], [Fac], [Dept], [SubAcct], convert(varchar, Convert(Date, [TranDate])) Trandate, '$' + convert(varchar(12), [Debit], 1) Debit, '$' + convert(varchar(12), [Credit], 1) Credit, [Qty], [ContrAcct], [Pat_Acct_Fac], comments, IgnoreRecord, b.TranCode, b.TranCodeDesc FROM DWH.dbo.FARGLD_CORRECTIONS a left join DWH.dbo.FARGLD_TranCode b on a.TranCodeID = b.TranCodeid  WHERE Trandate between '" & StartDateTextBox.Text & "' and '" & EndDateTextBox.Text & "' and ISCorrected is null and (ignoreRecord is null or ignorerecord = 0 ) ORDER BY FAC,[TranDate] DESC, [Pat_Acct_Fac] DESC,  (case when Debit = '0.00' then Credit else Debit End ) desc")

            gvFarDAP.DataSource = Me.ExecuteQuery2(cmd2, "SELECT")
            gvFarDAP.DataBind()

            Dim temp As String
            Dim i As Integer = 0

            If gvFarDAP.Rows.Count.ToString > 0 Then
                For Each row As GridViewRow In gvFarDAP.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        temp = row.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Text
                        If temp <> "9999" Then
                            i += 1
                        End If
                    End If
                Next
            End If

            lblCount.Text = "Total Record count: " & gvFarDAP.Rows.Count.ToString & "      Records Edited: " & i.ToString

            If gvFarDAP.Rows.Count.ToString = 0 Then
                lblCount.Visible = False
                btnUpdate2.Visible = False
                lblMessage.Text = "There was no suspense activity for the selected dates."
                lblMessage.Visible = True
            Else
                lblCount.Visible = True
                If gvFarDAP.Enabled = True Then
                    btnUpdate2.Visible = True
                End If
                lblMessage.Visible = False
            End If
            Session("FARDAPStartDate") = StartDateTextBox.Text
            Session("FARDAPEndDate") = EndDateTextBox.Text
            Session("FARDAPFac") = rblFacilityFilter.SelectedValue
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Private Function ExecuteQuery2(cmd2 As SqlCommand, action As String) As DataTable

        Try



            Dim conString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString
            Using con As New SqlConnection(conString)
                cmd2.Connection = con
                Select Case action
                    Case "SELECT"
                        Using sda As New SqlDataAdapter()
                            sda.SelectCommand = cmd2
                            Using dt As New DataTable()
                                sda.Fill(dt)
                                Return dt
                            End Using
                        End Using
                    Case "UPDATE"
                        con.Open()
                        cmd2.ExecuteNonQuery()
                        con.Close()
                        Exit Select
                End Select
                Return Nothing
            End Using
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Function
    Protected Sub Update2(sender As Object, e As EventArgs) Handles btnUpdate2.Click

        Try



            For Each row As GridViewRow In gvFarDAP.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    '  Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked

                    Dim IgnoreRecord, FY, HoldRecord, GLEffective As String
                    IgnoreRecord = ""
                    FY = ""
                    HoldRecord = ""
                    GLEffective = ""

                    IgnoreRecord = row.Cells(16).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked.ToString
                    HoldRecord = row.Cells(17).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked.ToString
                    'GLEffective = row.Cells(18).Controls.OfType(Of Calendar)().FirstOrDefault().SelectedDate.ToShortDateString
                    GLEffective = row.Cells(18).Controls.OfType(Of TextBox)().FirstOrDefault().Text.ToString
                    FY = row.Cells(2).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString

                    Select Case (row.Cells(16).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked.ToString)
                        Case "True"
                            IgnoreRecord = "1"
                        Case "False"
                            IgnoreRecord = "0"
                        Case Else
                            IgnoreRecord = ""
                    End Select
                    Select Case (row.Cells(17).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked.ToString)
                        Case "True"
                            HoldRecord = "1"
                        Case "False"
                            HoldRecord = "0"
                        Case Else
                            HoldRecord = ""
                    End Select

                    If GLEffective = "" And row.Cells(6).Controls.OfType(Of TextBox)().FirstOrDefault().Text <> "9999" Then
                        GLEffective = row.Cells(7).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    End If
                    'If GLEffective = "0001-01-01" Or GLEffective = "1/1/0001" Then
                    '    GLEffective = row.Cells(18).Controls.OfType(Of Label)().FirstOrDefault().Text.ToString
                    'Else
                    '    '   GLEffective = "1/1/0001"
                    'End If

                    If GLEffective = "0001-01-01" Or GLEffective = "1/1/0001" Or GLEffective = "" Then
                        If row.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Text <> "9999" And IgnoreRecord = "1" Then
                        Else
                            Dim cmd2 As New SqlCommand("UPDATE DWH.GL.FARGLD_CORRECTIONS_DS SET Dept = @Dept, SubAcct = @SubAcct, comments = @comments, IgnoreRecord = @IgnoreRecord, HoldRecord = @HoldRecord   WHERE Active = 1 and ID = @ID and FY = @FY ")
                            cmd2.Parameters.AddWithValue("@SubAcct", row.Cells(6).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                            cmd2.Parameters.AddWithValue("@Dept", row.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                            cmd2.Parameters.AddWithValue("@comments", row.Cells(13).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                            cmd2.Parameters.AddWithValue("@IgnoreRecord", IgnoreRecord)
                            cmd2.Parameters.AddWithValue("@HoldRecord", HoldRecord)
                            cmd2.Parameters.AddWithValue("@ID", gvFarDAP.DataKeys(row.RowIndex).Value)
                            cmd2.Parameters.AddWithValue("@FY", FY)

                            Me.ExecuteQuery2(cmd2, "UPDATE")
                        End If
                    Else
                        If row.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Text <> "9999" And IgnoreRecord = "1" Then
                        Else
                            Dim cmd2 As New SqlCommand("UPDATE DWH.GL.FARGLD_CORRECTIONS_DS SET Dept = @Dept, SubAcct = @SubAcct, comments = @comments, IgnoreRecord = @IgnoreRecord, HoldRecord = @HoldRecord, GLEffectiveDate = @GLEffectiveDate WHERE Active =1 and ID = @ID and FY = @FY ")
                            cmd2.Parameters.AddWithValue("@SubAcct", row.Cells(6).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                            cmd2.Parameters.AddWithValue("@Dept", row.Cells(5).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                            cmd2.Parameters.AddWithValue("@comments", row.Cells(13).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                            cmd2.Parameters.AddWithValue("@IgnoreRecord", IgnoreRecord)
                            cmd2.Parameters.AddWithValue("@HoldRecord", HoldRecord)
                            cmd2.Parameters.AddWithValue("@GLEffectiveDate", GLEffective)
                            cmd2.Parameters.AddWithValue("@ID", gvFarDAP.DataKeys(row.RowIndex).Value)
                            cmd2.Parameters.AddWithValue("@FY", FY)

                            Me.ExecuteQuery2(cmd2, "UPDATE")
                        End If
                    End If


                End If
            Next
            btnUpdate2.Visible = False
            Me.BindGrid()
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    'Protected Sub OnRowDataBound2(sender As Object, e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim cmd As New SqlCommand("select distinct costcenter from DWH.dbo.CostCenter order by costcenter")
    '        Dim ddlDept As DropDownList = TryCast(e.Row.FindControl("ddlDept"), DropDownList)
    '        ddlDept.DataSource = Me.ExecuteQuery2(cmd, "SELECT")
    '        ddlDept.DataTextField = "costcenter"
    '        ddlDept.DataValueField = "costcenter"
    '        ddlDept.DataBind()
    '        Dim Dept As String = TryCast(e.Row.FindControl("lblDept"), Label).Text
    '        ddlDept.Items.FindByValue(Dept).Selected = True

    '    End If
    'End Sub
    Protected Sub OnCheckedChanged(sender As Object, e As EventArgs)

        Try



            Dim isUpdateVisible As Boolean = False
            Dim NotIsCheck As Boolean = True
            Dim chk As CheckBox = TryCast(sender, CheckBox)
            If chk.ID = "chkAllIR" Then
                For Each row As GridViewRow In gvFarDAP.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        row.Cells(16).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = chk.Checked
                    End If
                Next
            End If
            Dim chkAll As CheckBox = TryCast(gvFarDAP.HeaderRow.FindControl("chkAllIR"), CheckBox)
            chkAll.Checked = True
            For Each row As GridViewRow In gvFarDAP.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim isChecked As Boolean = row.Cells(16).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked
                    If isChecked = True Then
                        NotIsCheck = False
                    Else
                        NotIsCheck = True
                    End If
                    For i As Integer = 1 To row.Cells.Count - 1

                        If Not isChecked Then
                            chkAll.Checked = False
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub OnCheckedChanged2(sender As Object, e As EventArgs)
        Exit Sub

        'Dim isUpdateVisible As Boolean = False
        'Dim NotIsCheck As Boolean = True
        'Dim chk As CheckBox = TryCast(sender, CheckBox)
        'If chk.ID = "chkAll" Then
        '    For Each row As GridViewRow In gvFarDAP.Rows
        '        If row.RowType = DataControlRowType.DataRow Then
        '            row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = chk.Checked
        '        End If
        '    Next
        'End If
        'Dim chkAll As CheckBox = TryCast(gvFarDAP.HeaderRow.FindControl("chkAll"), CheckBox)
        'chkAll.Checked = True
        'For Each row As GridViewRow In gvFarDAP.Rows
        '    If row.RowType = DataControlRowType.DataRow Then
        '        Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked
        '        If isChecked = True Then
        '            NotIsCheck = False
        '        Else
        '            NotIsCheck = True
        '        End If
        '        For i As Integer = 1 To row.Cells.Count - 1
        '            If row.Cells(i).Controls.OfType(Of TextBox)().ToList().Count > 0 Then
        '                row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().ReadOnly = NotIsCheck
        '            End If
        '            If row.Cells(i).Controls.OfType(Of DropDownList)().ToList().Count > 0 Then
        '                row.Cells(i).Controls.OfType(Of DropDownList)().FirstOrDefault().Enabled = isChecked
        '            End If
        '            If row.Cells(i).Controls.OfType(Of CheckBox)().ToList().Count > 0 Then
        '                row.Cells(i).Controls.OfType(Of CheckBox)().FirstOrDefault().Enabled = isChecked
        '            End If

        '            If isChecked AndAlso Not isUpdateVisible Then
        '                isUpdateVisible = True
        '            End If
        '            If Not isChecked Then
        '                chkAll.Checked = False
        '            End If
        '        Next
        '    End If
        'Next
        'btnUpdate2.Visible = isUpdateVisible
    End Sub

    'Private Sub gvFARGLD_CFY_DataBound(sender As Object, e As System.EventArgs) Handles gvFARGLD_CFY.DataBound
    '    Try
    '        If gvFARGLD_CFY.Rows.Count > 0 Then
    '            lblMessage.Text = ""
    '            lblMessage.Visible = False
    '        Else
    '            lblMessage.Text = "There was no suspense activity for the selected dates."
    '            lblMessage.Visible = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub gvFARGLD_CFY_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFARGLD_CFY.RowUpdating
    '    Try
    '        Dim ID As String = ""
    '        Dim FY As String = ""
    '        Dim Dept As String = ""
    '        Dim SubAcct As String = ""
    '        Dim Pat_Acct_Fac As String = ""
    '        Dim Comments As String = ""
    '        Dim IgnoreRecord As String = ""


    '        ID = gvFARGLD_CFY.Rows(e.RowIndex).Cells(1).Text
    '        FY = gvFARGLD_CFY.Rows(e.RowIndex).Cells(2).Text
    '        Dept = e.NewValues.Item("Dept")
    '        SubAcct = e.NewValues.Item("SubAcct")
    '        Pat_Acct_Fac = e.OldValues.Item("Pat_Acct_Fac")
    '        Comments = e.NewValues.Item("comments")
    '        IgnoreRecord = e.NewValues.Item("IgnoreRecord")

    '        If Dept Is Nothing Then
    '            Dept = "NULL"
    '        End If
    '        If SubAcct Is Nothing Then
    '            SubAcct = "NULL"
    '        End If
    '        If IgnoreRecord = "True" Then
    '            IgnoreRecord = "1"
    '        Else
    '            IgnoreRecord = "0"
    '        End If

    '        DWH.UpdateCommand = "update DWH.dbo.FARGLD_Corrections set " & _
    '      "Dept =  " & Replace(Dept, "'", "''") & " , " & _
    '      "SubAcct =  " & Replace(SubAcct, "'", "''") & ",  " & _
    '      "comments = '" & Replace(Comments, "'", "''") & "', " & _
    '      "IgnoreRecord = '" & Replace(IgnoreRecord, "'", "''") & "' " & _
    '      "where ID = '" & Replace(ID, "'", "''") & "' and FY = '" & Replace(FY, "'", "''") & "' "

    '        'DWH.UpdateCommand = "IF not exists(select * from DWH.dbo.FARGLD_Corrections where ID = '" & Replace(ID, "'", "''") & "' and FY = '" & Replace(FY, "'", "''") & "') " & _
    '        '"Insert into DWH.dbo.FARGLD_Corrections " & _
    '        '"(ID, FY, Dept, SubAcct, TranCodeID, Pat_Acct_Fac) " & _
    '        '"values ('" & Replace(ID, "'", "''") & "', '" & Replace(FY, "'", "''") & "', " & Replace(Dept, "'", "''") & ", " & _
    '        '"" & Replace(SubAcct, "'", "''") & ", " & Replace(TranCodeID, "'", "''") & ", '" & Replace(Pat_Acct_Fac, "'", "''") & "') " & _
    '        '"else " & _
    '        '"update DWH.dbo.FARGLD_Corrections set " & _
    '        '"Dept =  " & Replace(Dept, "'", "''") & " , " & _
    '        '"SubAcct =  " & Replace(SubAcct, "'", "''") & " , " & _
    '        '"TranCodeID = " & Replace(TranCodeID, "'", "''") & ", " & _
    '        '"Pat_Acct_Fac = '" & Replace(Pat_Acct_Fac, "'", "''") & "' " & _
    '        '"where ID = '" & Replace(ID, "'", "''") & "' and FY = '" & Replace(FY, "'", "''") & "' "

    '        gvFARGLD_CFY.EditIndex = -1

    '    Catch ex As Exception

    '    End Try
    'End Sub
    ''Protected Sub btnSaveWork_Click(sender As Object, e As EventArgs) Handles btnSaveWork.Click
    '    Try
    '        Dim dsARG As New DataSourceSelectArguments()
    '        Dim view As DataView = CType(DWH.Select(dsARG), DataView)
    '        Dim dt As DataTable = view.ToTable()

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim FARGLDID As String = dt.Rows(i)(0).ToString()
    '            Dim FARGLDyr As String = dt.Rows(i)(1).ToString()
    '            Dim FARGLDdeptUP As String = dt.Rows(i)(13).ToString()
    '            Dim FARGLDAcctUP As String = dt.Rows(i)(14).ToString()

    '            If FARGLDdeptUP <> "" And FARGLDAcctUP <> "" Then
    '                SQL = "update " & _
    '                "b " & _
    '                "set " & _
    '                "b.FY = a.FY, " & _
    '                "b.FM = a.FM, " & _
    '                "b.CY = a.CY, " & _
    '                "b.CM  = a.CM, " & _
    '                "b.PostDate = a.PostDate, " & _
    '                "b.Fac = a.Fac, " & _
    '                "b.Acct = a.Acct, " & _
    '                "b.tranDate = a.Trandate, " & _
    '                "b.TranCodeID = a.TranCodeID,  " & _
    '                "b.Debit = a.Debit, " & _
    '                "b.Credit = a.Credit, " & _
    '                "b.Qty = a.Qty, " & _
    '                "b.ContrAcct = a.ContrAcct, " & _
    '                "b.Pat_Acct_Fac = a.Pat_Acct_Fac " & _
    '                "from DWH.dbo.FARGLD_Corrections b " & _
    '                "Inner Join " & _
    '                "DWH.dbo.FARGLD_CFY a " & _
    '                "on (a.ID = b.ID and a.FY = b.FY and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac) " & _
    '                "where a.ID = " & Replace(FARGLDID, "'", "''") & " and a.FY = " & Replace(FARGLDyr, "'", "''") & " "

    '                cmd = New SqlCommand(SQL, conn)
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteReader()
    '            End If
    '        Next

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub btnGenerateEntries_Click(sender As Object, e As EventArgs) Handles btnGenerateEntries.Click
    '    Try
    '        pnlGLCorrectiveEntries2.Enabled = True
    '        pnlGLCorrectiveEntries2.Visible = True

    '        Dim SQL As String = ""
    '        Dim ds As DataSet
    '        Dim da As SqlDataAdapter

    '        SQL = "select " & _
    '            "case when ID is null then NULL " & _
    '            "else cast(ID as varchar) " & _
    '            "End ID, FY, " & _
    '            "case when Fac = 'A' then '10' " & _
    '            "when Fac = 'C' then '22' " & _
    '            "when Fac = 'F' then '6' " & _
    '            "else Fac " & _
    '            "End Fac, " & _
    '            "Dept, SubAcct, " & _
    '            "case when ID is null then 'Total' else Description end Description, " & _
    '            "Debit, Credit, '' as Spacer     " & _
    '            " from " & _
    '            "(select " & _
    '            "x.ID, x.FY, " & _
    '            "case when Grouping(x.Fac) = 1 then 'Total' " & _
    '            "else Fac " & _
    '            "   end Fac,  " & _
    '            "x.Dept,  " & _
    '            "x.SubAcct,  " & _
    '            " 'PA Daily Journal Entry - Clear Suspense Activity' as  Description , " & _
    '            "  sum(x.Debit) as Debit , " & _
    '            "   sum(x.Credit) as Credit       " & _
    '            "  from  " & _
    '            "( select  " & _
    '            " b.Fac, b.Dept, b.SubAcct,  b.Credit as Debit,b.Debit as Credit, b.ID, b.FY   " & _
    '            "  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
    '            "  join DWH.dbo.FARGLD_CFY b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
    '            " where a.ISCORRECTED is null  " & _
    '            " and a.Dept is not null and a.Dept <> 9999  " & _
    '            " and a.SubAcct  is not null  and a.subAcct <> 9999 " & _
    '            " union  " & _
    '            "  select  " & _
    '            " b.Fac, a.Dept, a.SubAcct, b.Debit, b.Credit, b.ID, b.FY    " & _
    '            "  from DWH.dbo.FARGLD_CORRECTIONS a  " & _
    '            "  join DWH.dbo.FARGLD_CFY b on a.ID = b.ID and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac   " & _
    '            " where a.ISCORRECTED is null  " & _
    '            " and a.Dept is not null  and a.Dept <> 9999 " & _
    '            " and a.SubAcct  is not null   and a.subAcct <> 9999 " & _
    '            " ) x  " & _
    '            "group by Fac, Dept, SubAcct, ID, FY with rollup  ) y  " & _
    '            "where (y.ID is not null and FY is not null)  " & _
    '            "or (y.ID is null and Fac is not null  " & _
    '            "   and Dept is null and SubAcct  is null and Fac <> 'Total' ) " & _
    '            "order by Fac, ID desc  , Dept desc  "

    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If

    '        ds = New DataSet
    '        da = New SqlDataAdapter(SQL, conn)
    '        da.Fill(ds, "FullTable")

    '        Dim drAtl As Array
    '        Dim dtAtl As New DataTable()
    '        Dim dr As DataRow

    '        drAtl = ds.Tables(0).Select("Fac ='10'")
    '        dtAtl = ds.Tables(0).Clone
    '        For Each dr In drAtl
    '            dtAtl.ImportRow(dr)
    '        Next

    '        gvAtlanta.DataSource = dtAtl
    '        gvAtlanta.DataBind()


    '        Dim drChe As Array
    '        Dim dtChe As New DataTable

    '        drChe = ds.Tables(0).Select("Fac = '22'")
    '        dtChe = ds.Tables(0).Clone
    '        For Each dr In drChe
    '            dtChe.ImportRow(dr)
    '        Next

    '        gvCherokee.DataSource = dtChe
    '        gvCherokee.DataBind()

    '        Dim drFor As Array
    '        Dim dtFor As New DataTable

    '        drFor = ds.Tables(0).Select("Fac = '6'")
    '        dtFor = ds.Tables(0).Clone
    '        For Each dr In drFor
    '            dtFor.ImportRow(dr)
    '        Next

    '        gvForsyth.DataSource = dtFor
    '        gvForsyth.DataBind()

    '        If gvAtlanta.Rows.Count > 0 Then
    '            lblAtlMessage.Text = ""
    '            lblAtlMessage.Visible = False
    '        Else
    '            lblAtlMessage.Text = "No data to export"
    '            lblAtlMessage.Visible = True
    '        End If
    '        If gvCherokee.Rows.Count > 0 Then
    '            lblCheMessage.Text = ""
    '            lblCheMessage.Visible = False
    '        Else
    '            lblCheMessage.Text = "No data to export"
    '            lblCheMessage.Visible = True
    '        End If
    '        If gvForsyth.Rows.Count > 0 Then
    '            lblForMessage.Text = ""
    '            lblForMessage.Visible = False
    '        Else
    '            lblForMessage.Text = "No data to export"
    '            lblForMessage.Visible = True
    '        End If



    '    Catch ex As Exception
    '    Finally
    '        StartDateTextBox.Enabled = True
    '        EndDateTextBox.Enabled = True
    '        gvFARGLD_CFY.Enabled = True

    '    End Try
    'End Sub
    'Protected Sub BtnGenerateAtlanta_Click(sender As Object, e As EventArgs) Handles BtnGenerateAtlanta.Click
    '    Try
    '        Dim sw As New StringWriter()
    '        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
    '        Dim frm As HtmlForm = New HtmlForm()
    '        Dim AtlEntry As String

    '        AtlEntry = "AtlantaGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

    '        If gvAtlanta.Rows.Count <> 0 Then
    '            Dim FARGLDID As String = ""
    '            Dim FARGLDYr As String = ""
    '            Dim FARGLDDeptUp As String = ""
    '            Dim FARGLDAcctUp As String = ""
    '            Dim SQLWhere As String = ""

    '            For j As Integer = 0 To gvAtlanta.Rows.Count - 1
    '                FARGLDID = gvAtlanta.Rows(j).Cells(6).Text
    '                FARGLDYr = gvAtlanta.Rows(j).Cells(7).Text

    '                If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
    '                    If SQLWhere.Contains(FARGLDID) = False Then
    '                        SQLWhere = SQLWhere & " ( FAC = 'A' and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
    '                    End If
    '                End If
    '            Next

    '            If SQLWhere <> "" Then
    '                SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

    '                SQL = "update DWH.dbo.FARGLD_CORRECTIONS set " & _
    '                    "iscorrected = sysdatetime() " & _
    '                    SQLWhere


    '                ' I liked this code because it updated based on selects  
    '                'SQL = "update " & _
    '                '   "b " & _
    '                '     "set " & _
    '                '     "b.FY = a.FY, " & _
    '                '     "b.FM = a.FM, " & _
    '                '     "b.CY = a.CY, " & _
    '                '     "b.CM  = a.CM, " & _
    '                '     "b.PostDate = a.PostDate, " & _
    '                '     "b.Fac = a.Fac, " & _
    '                '     "b.Acct = a.Acct, " & _
    '                '     "b.tranDate = a.Trandate, " & _
    '                '     "b.TranCodeID = a.TranCodeID,  " & _
    '                '     "b.Debit = a.Debit, " & _
    '                '     "b.Credit = a.Credit, " & _
    '                '     "b.Qty = a.Qty, " & _
    '                '     "b.ContrAcct = a.ContrAcct, " & _
    '                '     "b.Pat_Acct_Fac = a.Pat_Acct_Fac " & _
    '                '     "from DWH.dbo.FARGLD_Corrections b " & _
    '                '     "Inner Join " & _
    '                '     "DWH.dbo.FARGLD_CFY a " & _
    '                '     "on (a.ID = b.ID and a.FY = b.FY and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac) " &
    '                '     SQLWhere

    '                cmd = New SqlCommand(SQL, conn)
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteReader()
    '            End If

    '            pnlExportHeader.Visible = True
    '            lblHeader.Text = "Atlanta Northside Hospital Journal Entry"
    '            lblEntity.Text = "Entity: 10"
    '            lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

    '            Page.Response.AddHeader("content-disposition", "attachment;filename=" & AtlEntry)
    '            Page.Response.ContentType = "application/vnd.ms-excel"
    '            Page.Response.Charset = ""
    '            Page.EnableViewState = False
    '            frm.Attributes("runat") = "server"
    '            Controls.Add(frm)
    '            frm.Controls.Add(pnlExportHeader)
    '            frm.Controls.Add(gvAtlanta)
    '            frm.RenderControl(hw)
    '            Response.Write(sw.ToString())
    '            pnlExportHeader.Visible = False
    '            Response.End()
    '        Else
    '            lblAtlMessage.Text = "No data to export"
    '            lblAtlMessage.Visible = True
    '        End If




    '        ''''This code worked when the gridview was populated via a datasource  
    '        'Dim dsARG As New DataSourceSelectArguments()
    '        'Dim view As DataView = CType(SqlDataSource1.Select(dsARG), DataView)
    '        'Dim dt As DataTable = view.ToTable()

    '        'For i As Integer = 0 To dt.Rows.Count - 1
    '        '    Dim FARGLDID As String = dt.Rows(i)(0).ToString()
    '        '    Dim FARGLDyr As String = dt.Rows(i)(7).ToString()
    '        '    Dim FARGLDdeptUP As String = dt.Rows(i)(2).ToString()
    '        '    Dim FARGLDAcctUP As String = dt.Rows(i)(3).ToString()

    '        '    If FARGLDdeptUP <> "" And FARGLDAcctUP <> "" Then
    '        '        SQL = "update " & _
    '        '        "b " & _
    '        '        "set " & _
    '        '        "b.FY = a.FY, " & _
    '        '        "b.FM = a.FM, " & _
    '        '        "b.CY = a.CY, " & _
    '        '        "b.CM  = a.CM, " & _
    '        '        "b.PostDate = a.PostDate, " & _
    '        '        "b.Fac = a.Fac, " & _
    '        '        "b.Acct = a.Acct, " & _
    '        '        "b.tranDate = a.Trandate, " & _
    '        '        "b.TranCodeID = a.TranCodeID,  " & _
    '        '        "b.Debit = a.Debit, " & _
    '        '        "b.Credit = a.Credit, " & _
    '        '        "b.Qty = a.Qty, " & _
    '        '        "b.ContrAcct = a.ContrAcct, " & _
    '        '        "b.Pat_Acct_Fac = a.Pat_Acct_Fac " & _
    '        '        "from DWH.dbo.FARGLD_Corrections b " & _
    '        '        "Inner Join " & _
    '        '        "DWH.dbo.FARGLD_CFY a " & _
    '        '        "on (a.ID = b.ID and a.FY = b.FY and a.TranCodeID = b.TranCodeID and a.Pat_Acct_Fac = b.Pat_Acct_Fac) " & _
    '        '        "where a.ID is not null and a.ID = " & Replace(FARGLDID, "'", "''") & " and a.FY = " & Replace(FARGLDyr, "'", "''") & " "

    '        '        cmd = New SqlCommand(SQL, conn)
    '        '        If conn.State = ConnectionState.Closed Then
    '        '            conn.Open()
    '        '        End If
    '        '        cmd.ExecuteReader()
    '        '    End If
    '        'Next

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub btnGenerateCherokee_Click(sender As Object, e As EventArgs) Handles btnGenerateCherokee.Click
    '    Try
    '        Dim sw As New StringWriter()
    '        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
    '        Dim frm As HtmlForm = New HtmlForm()
    '        Dim CheEntry As String

    '        CheEntry = "CherokeeGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

    '        If gvCherokee.Rows.Count <> 0 Then
    '            Dim FARGLDID As String = ""
    '            Dim FARGLDYr As String = ""
    '            Dim FARGLDDeptUp As String = ""
    '            Dim FARGLDAcctUp As String = ""
    '            Dim SQLWhere As String = ""

    '            gvCherokee.Columns(0).Visible = True

    '            For j As Integer = 0 To gvCherokee.Rows.Count - 1
    '                FARGLDID = gvCherokee.Rows(j).Cells(6).Text
    '                FARGLDYr = gvCherokee.Rows(j).Cells(7).Text

    '                If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
    '                    If SQLWhere.Contains(FARGLDID) = False Then
    '                        SQLWhere = SQLWhere & " ( FAC = 'C' and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
    '                    End If
    '                End If
    '            Next

    '            If SQLWhere <> "" Then
    '                SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

    '                SQL = "update DWH.dbo.FARGLD_CORRECTIONS set " & _
    '                    "iscorrected = sysdatetime() " & _
    '                    SQLWhere

    '                cmd = New SqlCommand(SQL, conn)
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteReader()
    '            End If

    '            pnlExportHeader.Visible = True
    '            lblHeader.Text = "Cherokee Northside Hospital Journal Entry"
    '            lblEntity.Text = "Entity: 22"
    '            lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

    '            Page.Response.Clear()
    '            Page.Response.AddHeader("content-disposition", "attachment;filename=" & CheEntry)
    '            Page.Response.ContentType = "application/vnd.ms-excel"
    '            Page.Response.Charset = ""
    '            Page.EnableViewState = False
    '            frm.Attributes("runat") = "server"
    '            Controls.Add(frm)
    '            frm.Controls.Add(pnlExportHeader)
    '            frm.Controls.Add(gvCherokee)
    '            frm.RenderControl(hw)
    '            Response.Write(sw.ToString())
    '            Response.End()

    '            pnlExportHeader.Visible = False
    '        Else
    '            lblCheMessage.Text = "No data to export"
    '            lblCheMessage.Visible = True
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub btnGenerateForsyth_Click(sender As Object, e As EventArgs) Handles btnGenerateForsyth.Click
    '    Try
    '        Dim sw As New StringWriter()
    '        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
    '        Dim frm As HtmlForm = New HtmlForm()
    '        Dim ForEntry As String

    '        ForEntry = "ForsythGLEntry_" & Replace(Replace(Replace(Date.Now.ToString, "/", "_"), ":", "_"), " ", "_") & ".xls"

    '        If gvForsyth.Rows.Count <> 0 Then
    '            Dim FARGLDID As String = ""
    '            Dim FARGLDYr As String = ""
    '            Dim FARGLDDeptUp As String = ""
    '            Dim FARGLDAcctUp As String = ""
    '            Dim SQLWhere As String = ""

    '            gvForsyth.Columns(0).Visible = True

    '            For j As Integer = 0 To gvForsyth.Rows.Count - 1
    '                FARGLDID = gvForsyth.Rows(j).Cells(6).Text
    '                FARGLDYr = gvForsyth.Rows(j).Cells(7).Text

    '                If FARGLDID <> "" And FARGLDID <> "&nbsp;" Then
    '                    If SQLWhere.Contains(FARGLDID) = False Then
    '                        SQLWhere = SQLWhere & " ( FAC = 'F' and ID = " & Replace(FARGLDID, "'", "''") & " and FY = " & Replace(FARGLDYr, "'", "''") & " ) or "
    '                    End If
    '                End If
    '            Next

    '            If SQLWhere <> "" Then
    '                SQLWhere = " where " & Mid(SQLWhere, 1, (SQLWhere.Length - 3))

    '                SQL = "update DWH.dbo.FARGLD_CORRECTIONS set " & _
    '                      "iscorrected = sysdatetime() " & _
    '                      SQLWhere

    '                cmd = New SqlCommand(SQL, conn)
    '                If conn.State = ConnectionState.Closed Then
    '                    conn.Open()
    '                End If
    '                cmd.ExecuteReader()
    '            End If

    '            pnlExportHeader.Visible = True
    '            lblHeader.Text = "Forysth Northside Hospital Journal Entry"
    '            lblEntity.Text = "Entity: 6"
    '            lblDate.Text = "Date: " & Year(Date.Now.ToString) & "-" & Month(Date.Now.ToString) & "-" & Day(Date.Now.ToString)

    '            Response.Clear()
    '            Response.Buffer = True
    '            Response.ContentType = "application/vnd.ms-excel"
    '            Response.AddHeader("content-disposition", "attachment;filename=" & ForEntry)

    '            Response.Charset = ""
    '            EnableViewState = False

    '            frm.Attributes("runat") = "server"
    '            Controls.Add(frm)
    '            frm.Controls.Add(pnlExportHeader)
    '            frm.Controls.Add(gvForsyth)
    '            frm.RenderControl(hw)
    '            Response.Write(sw.ToString())
    '            pnlExportHeader.Visible = False
    '            Response.End()

    '            '             Response.Clear();
    '            'Response.Buffer = true;
    '            'Response.ContentType = "application/vnd.ms-excel";
    '            'Response.AddHeader("content-disposition", "attachment;filename=MyFiles.xls");
    '            'Response.Charset = "";
    '            'this.EnableViewState = false;

    '            'System.IO.StringWriter sw = new System.IO.StringWriter();
    '            'System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

    '            'gvFiles.RenderControl(htw);

    '            'Response.Write(sw.ToString());
    '            'Response.End();
    '        Else
    '            lblForMessage.Text = "No data to export"
    '            lblForMessage.Visible = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub gvFARGLD_CFY_DataBound(sender As Object, e As System.EventArgs) Handles gvFARGLD_CFY.DataBound
    '    Try
    '        If gvFARDAP.Rows.Count = 0 And gvFARGLD_CFY.Rows.Count = 0 Then
    '            lblMessage.Text = "No FARDAP activity for this date to the current date."
    '            lblMessage.Visible = True
    '        Else
    '            lblMessage.Text = ""
    '            lblMessage.Visible = False
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub


    Protected Sub lbLoadNewDates_Click(sender As Object, e As EventArgs) Handles lbLoadNewDates.Click
        Try
            Me.BindGrid()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


End Class