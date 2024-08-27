Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

Public Class FinanceHelpDeskCaseListing

    Inherits System.Web.UI.Page
    Private Shared superadmin As Integer = 0
    Private Shared admin As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else

            If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "cw996788" Then
                superadmin = 0
                admin = 1
            ElseIf Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "mf995052" Then
                superadmin = 1
                admin = 1

            Else

                Dim cmd2 As SqlCommand

                Dim adminsql As String = "SELECT count(*) FROM [WebFD].[FinanceHelpDesk].[tblUsers] where uid = '" & _
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and IsRep = 1"

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd2 = New SqlCommand(adminsql, conn)
                    cmd2.CommandTimeout = 86400
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    admin = cmd2.ExecuteScalar

                End Using
            End If

            If admin = 1 Then
                trUpdateCase.Visible = True
                trKB.Visible = True
                trAdditionalNotes.Visible = True
                trAdditionalNotes2.Visible = True
                txtUpdateDesc.Visible = True
                txtUpdateSolution.Visible = True
                txtUpdateNewNotes.Visible = True
                txtUpdateTitle.Visible = True
                lblUpdateNotes.Visible = False
                lblDesc.Visible = False
                lblSolution.Visible = False
                lblTitle.Visible = False

            End If

            If Request.QueryString("CaseNo") IsNot Nothing Then
                lblCaseNumber.Text = Request.QueryString("CaseNo")
                lblCaseNumber2.Text = Request.QueryString("CaseNo")

                btnViewAttach.Attributes.Add("onclick", "javascript:open_win('" & Replace(Request.QueryString("CaseNo"), "'", "''") & "')")

                Dim sql As String = "Select p.[id], p.title, u.fname, p.[start_date], p.[close_date], s.sname" & _
                ", p.[uid], p.uemail, p.uphone, p.ulocation, p.uComputerName, d.dname, c.cname, " & _
                "u.firstname, p.[description], " & _
                "(select convert(varchar(max),note) + ' -- ' + convert(varchar, n.addDate) + ';newline;' from WebFD.FinanceHelpDesk.tblNotes n " & _
                "where n.id = p.id and (n.[private] = 0 or n.[uid] = '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "') " & _
                "order by n.addDate asc for XML Path('')) AS note " & _
                ", p.solution , pr.pname, pr.priority_id, s.status_id " & _
                " FROM WebFD.[FinanceHelpDesk].[problems] p " & _
                " left join WebFD.[FinanceHelpDesk].tblUsers u on p.rep = u.[sid] " & _
                " left join WebFD.[FinanceHelpDesk].[status] s on p.[status] = s.status_id " & _
                " left join WebFD.[FinanceHelpDesk].departments d on p.department = d.department_id " & _
                " left join WebFD.[FinanceHelpDesk].categories c on p.category = c.category_id " & _
                " left join WebFD.[FinanceHelpDesk].priority pr on p.priority = pr.priority_id " & _
                "/* left join WebFD.[FinanceHelpDesk].tblNotes n on n.[id] = p.[id] and (n.[private] = 0 or n.[uid] = '" & _
                Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "') */" & _
                " where id = '" & Replace(Request.QueryString("CaseNo"), "'", "''") & "' and " & _
                " (kb = 1 or 1 = " & admin.ToString & ")"

                Dim dr As SqlDataReader
                Dim cmd As SqlCommand
                Dim da As New SqlDataAdapter
                Dim ds As New DataSet

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString)
                    cmd = New SqlCommand(sql, conn)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    dr = cmd.ExecuteReader

                    If dr.HasRows() Then
                        FullPanel.Visible = True
                        EmptyPanel.Visible = False
                    Else
                        FullPanel.Visible = False
                        EmptyPanel.Visible = True
                    End If

                    While dr.Read
                        If IsDBNull(dr.Item("title")) Then
                            txtUpdateTitle.Text = ""
                            lblTitle.Text = ""
                        Else
                            txtUpdateTitle.Text = dr.Item("title").ToString
                            lblTitle.Text = dr.Item("title").ToString
                        End If
                        If IsDBNull(dr.Item("uid")) Then
                            lblUserName.Text = ""
                        Else
                            lblUserName.Text = dr.Item("uid").ToString
                        End If
                        If IsDBNull(dr.Item("uemail")) Then
                            lblEmail.Text = ""
                        Else
                            lblEmail.Text = dr.Item("uemail").ToString
                        End If
                        If IsDBNull(dr.Item("uphone")) Then
                            lblPhone.Text = ""
                        Else
                            lblPhone.Text = dr.Item("uphone").ToString
                        End If
                        If IsDBNull(dr.Item("ulocation")) Then
                            lblIPAdd.Text = ""
                        Else
                            lblIPAdd.Text = dr.Item("ulocation").ToString
                        End If
                        If IsDBNull(dr.Item("uComputerName")) Then
                            lblCompName.Text = ""
                        Else
                            lblCompName.Text = dr.Item("uComputerName").ToString
                        End If
                        If IsDBNull(dr.Item("start_date")) Then
                            lblStartDate.Text = ""
                        Else
                            lblStartDate.Text = dr.Item("start_date").ToString
                        End If
                        If IsDBNull(dr.Item("close_date")) Then
                            lblCloseDate.Text = ""
                        Else
                            lblCloseDate.Text = dr.Item("close_date").ToString
                        End If
                        If IsDBNull(dr.Item("dname")) Then
                            lblDept.Text = ""
                        Else
                            lblDept.Text = dr.Item("dname").ToString
                        End If
                        If IsDBNull(dr.Item("cname")) Then
                            lblCategory.Text = ""
                        Else
                            lblCategory.Text = dr.Item("cname").ToString
                        End If
                        If IsDBNull(dr.Item("fname")) Then
                            lblAssignedTo.Text = ""
                        Else
                            lblAssignedTo.Text = dr.Item("fname").ToString
                        End If
                        If IsDBNull(dr.Item("sname")) Then
                            lblStatus.Text = ""
                        Else
                            lblStatus.Text = dr.Item("sname").ToString
                        End If
                        If IsDBNull(dr.Item("description")) Then
                            lblDesc.Text = ""
                            txtUpdateDesc.Text = ""
                        Else
                            lblDesc.Text = dr.Item("description").ToString
                            txtUpdateDesc.Text = dr.Item("description").ToString
                        End If
                        If IsDBNull(dr.Item("note")) Then
                            lblUpdateNotes.Text = ""
                        Else
                            lblUpdateNotes.Text = dr.Item("note").ToString
                        End If
                        If IsDBNull(dr.Item("solution")) Then
                            txtUpdateSolution.Text = ""
                            lblSolution.Text = ""
                        Else
                            lblSolution.Text = dr.Item("solution").ToString
                            txtUpdateSolution.Text = dr.Item("solution").ToString
                        End If
                    End While

                End Using
            End If
        End If
    End Sub

    Private Sub btnSearchNew_Click(sender As Object, e As EventArgs) Handles btnSearchNew.Click
        If IsNumeric(txtNewCaseNo.Text) Then
            If Request.QueryString("CaseNo") IsNot Nothing Then
                Response.Redirect("../FinanceHelpDeskCaseListing/?CaseNo=" & txtNewCaseNo.Text)
            Else
                Response.Redirect("../FinanceHelpDesk/FinanceHelpDeskCaseListing/?CaseNo=" & txtNewCaseNo.Text)
            End If

        Else
            explanationlabelp4.Text = "Please enter valid Case Number"
            ModalPopupExtenderp4.Show()
        End If

    End Sub

    Private Sub btnUpdateCase_Click(sender As Object, e As EventArgs) Handles btnUpdateCase.Click
        Dim updatesql As String = "Update [WebFD].[FinanceHelpDesk].[problems] set title = '" & Replace(txtUpdateTitle.Text, "'", "''") & _
          "', description = '" & Replace(txtUpdateDesc.Text, "'", "''") & "', solution = '" & Replace(txtUpdateSolution.Text, "'", "''") & _
          "', kb = '"
        If chbKB.Checked Then
            updatesql += "1"
        Else
            updatesql += "0"
        End If
        updatesql += "' where id = '" & Replace(Request.QueryString("CaseNo"), "'", "''") & "'"

        If Trim(txtUpdateNewNotes.Text) <> "" Then
            updatesql += "; Insert into WebFD.FinanceHelpDesk.tblNotes (id, note, addDate, uid, private) Values ('" & _
               Replace(Request.QueryString("CaseNo"), "'", "''") & "', '" & Replace(Trim(txtUpdateNewNotes.Text), "'", "''") & _
                "', getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''")
            If chbHideNotes.Checked Then
                updatesql += "', 1) "
            Else
                updatesql += "', 0) "
            End If
        End If

        'If chbDontEmailUser.Checked = False Then
        '    updatesql += "; exec WebFD.FinanceHelpDesk.UpdateMail @Admin= " & CStr(admin) & ", @ID = " & Replace(gvOpenCases.SelectedRow.Cells(1).Text, "'", "''")
        'End If

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd = New System.Data.SqlClient.SqlCommand(updatesql, conn)
            cmd.ExecuteNonQuery()
            explanationlabelp4.Text = "Successfully Submitted Case"
            explanationlabelp4.DataBind()
            ModalPopupExtenderp4.Show()
        End Using
    End Sub
End Class