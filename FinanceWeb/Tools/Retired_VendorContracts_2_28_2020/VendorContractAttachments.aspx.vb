Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal


Public Class VendorContractAttachments
    Inherits System.Web.UI.Page
    Private Shared superadmin As Integer = 0
    Private Shared admin As Integer = 0
    Private Shared Cont As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "cw996788" Then
                superadmin = 0
                admin = 1
            ElseIf Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "mf995052" Then
                superadmin = 0
                admin = 1

            Else

                Dim cmd2 As SqlCommand

                Dim adminsql As String = "SELECT count(*) FROM [WebFD].[VendorContracts].[Users] where UserLogin = '" & _
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' and (Admin = 1 or LegalTeam = 1)"

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    cmd2 = New SqlCommand(adminsql, conn)
                    cmd2.CommandTimeout = 86400
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    admin = cmd2.ExecuteScalar

                End Using
            End If

            If Request.QueryString("ContractStatus") IsNot Nothing Then
                Cont = Request.QueryString("ContractStatus")
            End If

            If Request.QueryString("ContractID") IsNot Nothing Then

                Dim tblname As String
                If Cont = 1 Then
                    'tblname = "WebFD.VendorContracts.PendingContractHeader"
                    FullPanel.Visible = True
                    EmptyPanel.Visible = False
                    CaseUser.Text = Replace(Request.ServerVariables("AUTH_USER"), "NS\", "")
                    CaseNumber.Text = "Not Yet Assigned"
                    CaseTitle.Visible = False
                    CaseDesc.Visible = False

                Else
                    tblname = "WebFD.VendorContracts.ContractHeader"

                    Dim sql As String = "select isnull(isnull(d.DepartmentDisplayName, d.DepartmentNo), p.DepartmentID) as Dept, " & _
                   "VendorName as Vendor, AnnualContractExpense, isnull(u.UserDisplayName, u.UserFullName) as UserName " & _
                   "from " & tblname & " p " & _
                   "left join WebFD.VendorContracts.Department_LU d on p.DepartmentID = d.DepartmentID and d.Active = 1 " & _
                   "left join WebFD.VendorContracts.Users u on p.RequesterUserLogin = u.UserLogin " & _
                   "where p.ContractID = '" & Replace(Request.QueryString("ContractID"), "'", "''") & "' and (p.RequesterUserLogin = '" & _
                   Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "' or 1 = 1 or 1 = " & admin & ")"
                    Dim cmd As SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As DataSet

                    Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        cmd = New SqlCommand(sql, con)
                        da = New SqlDataAdapter(cmd)
                        con.Open()

                        ds = New DataSet()
                        da.Fill(ds)

                        If ds.Tables(0).Rows.Count > 0 Then
                            CaseTitle.Text = ds.Tables(0).Rows(0).Item(1).ToString
                            CaseDesc.Text = ds.Tables(0).Rows(0).Item(3).ToString
                            CaseUser.Text = ds.Tables(0).Rows(0).Item(2).ToString
                            CaseNumber.Text = ds.Tables(0).Rows(0).Item(0).ToString
                            CaseTitle.Visible = True
                            CaseDesc.Visible = True
                            FullPanel.Visible = True
                            EmptyPanel.Visible = False

                        End If
                    End Using

                End If

                BindGrid()

            End If

            End If

    End Sub



    Private Sub BindGrid()

        Dim cmd As SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet


        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim Str As String = "select * from WebFD.VendorContracts.ContractAttachments " & _
                "where ContractID = '" & Replace(Request.QueryString("ContractID"), "'", "''") & "' and Active = 1 and 0 = " & Cont & _
                "union all " & _
                "select * from WebFD.VendorContracts.PendingContractAttachments " & _
                "where ContractID = '" & Replace(Request.QueryString("ContractID"), "'", "''") & "' and Active = 1 and 1 = " & Cont


            cmd = New SqlCommand(Str, conn)
            da.SelectCommand = cmd
            da.SelectCommand.CommandTimeout = 86400
            da.Fill(ds, "OData")

            gvImages.DataSource = ds

            gvImages.DataBind()

        End Using


    End Sub

    Protected Sub UploadFile(sender As Object, e As EventArgs)

        Try


            Dim filename As String = Path.GetFileName(fileInput.PostedFile.FileName)

            Dim x As Integer = fileInput.PostedFile.ContentLength
            If x > 52600000 Then

                Exit Sub
            End If

            Dim contentType As String = fileInput.PostedFile.ContentType

            Using fs As Stream = fileInput.PostedFile.InputStream

                Using br As New BinaryReader(fs)

                    Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                    Dim cmd As New SqlCommand

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If



                        Dim query As String

                        If Cont = 0 Then
                            query = "INSERT INTO WebFD.VendorContracts.ContractAttachments (ContractID, FileName, ContentType, Content, UserID, DateAdded, Active) VALUES " & _
                        "('" & Replace(Request.QueryString("ContractID"), "'", "''") & "', @FileName, @ContentType, @Content, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1)"

                        Else
                            query = "INSERT INTO WebFD.VendorContracts.PendingContractAttachments (ContractID, FileName, ContentType, Content, UserID, DateAdded, Active) VALUES " & _
                            "('" & Replace(Request.QueryString("ContractID"), "'", "''") & "', @FileName, @ContentType, @Content, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1)"

                        End If

                        cmd.Connection = conn
                        cmd.CommandText = query

                        cmd.Parameters.AddWithValue("@FileName", filename)
                        cmd.Parameters.AddWithValue("@ContentType", contentType)
                        cmd.Parameters.AddWithValue("@Content", bytes)

                        cmd.ExecuteNonQuery()


                    End Using



                End Using

            End Using

            BindGrid()
            'Response.Redirect(Request.Url.AbsoluteUri)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        UploadFile(sender, e)
    End Sub

    Protected Sub DownloadFile(sender As Object, e As EventArgs)

        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)

        Dim bytes As Byte()

        Dim fileName As String, contentType As String

        Dim cmd As New SqlCommand

        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.CommandText = "select FileName, Content, ContentType from WebFD.VendorContracts.ContractAttachments where AttachmentID=@Id and 0 = " & Cont & _
                "union all select FileName, Content, ContentType from WebFD.VendorContracts.PendingContractAttachments where AttachmentID=@Id and 1 = " & Cont

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

    Private Sub gvImages_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvImages.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Cells(0).CssClass = "hidden"
                e.Row.Cells(1).CssClass = "hidden"

            End If
        Catch

        End Try

    End Sub


    Private Sub gvImages_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvImages.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            If Left(e.Row.Cells(1).Text, 5) = "image" Then
                Try

                    Dim bytes As Byte() = TryCast(TryCast(e.Row.DataItem, DataRowView)("Content"), Byte())

                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)

                    TryCast(e.Row.FindControl("Image1"), Image).ImageUrl = Convert.ToString("data:image/png;base64,") & base64String

                Catch ex As Exception

                    Dim img As Image = e.Row.FindControl("Image1")
                    img.Visible = False

                End Try
            Else
                Dim img As Image = e.Row.FindControl("Image1")
                img.Visible = False
            End If

        End If

    End Sub

End Class