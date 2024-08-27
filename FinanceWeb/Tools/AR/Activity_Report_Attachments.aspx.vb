Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal


Public Class Activity_Report_Attachments
    Inherits System.Web.UI.Page
    Private Shared superadmin As Integer = 0
    Private Shared admin As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "cw996788" Then
                superadmin = 0
                admin = 1
            ElseIf Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") = "mf995052" Then
                superadmin = 0
                admin = 1

            Else


                'Dim adminsql As String = "SELECT count(*) FROM DWH.AR.Activity_Users where UserLogin = '" & _
                '    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'"
                Dim adminsql As String = "SELECT count(*) FROM DWH.AR.ActivityReport_Users where UserLogin = '" & _
                    Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "'"
                admin = GetScalar(adminsql)

            End If

            If Request.QueryString("Detail_ID") IsNot Nothing Then


                'Dim sql As String = "select ad.Facility, ad.CashCategory, ad.CategoryStatus, ad.DepositDate, ad.WF_Cash_Received " & _
                '    "from DWH.AR.Activity_Detail ad " & _
                '    "where ad.Detail_ID = '" & Replace(Request.QueryString("Detail_ID"), "'", "''") & "' "
                Dim sql As String = "select ad.Facility, ad.CashCategory, ad.DetailStatus, ar.DepositDate, ad.Cash_Received " & _
                    "from DWH.AR.ActivityReport ar join DWH.AR.ActivityReport_Detail ad on ar.ActivityID = ad.ActivityID and ad.active = 1" & _
                    "where ar.Active = 1 and ad.ActivityID = '" & Replace(Request.QueryString("Detail_ID"), "'", "''") & "' and not exists (select * from DWH.AR.ActivityReport_Detail ad2 " & _
                        "where ad2.ActivityID = ad.ActivityID and (ad2.ActivityDate > ad.ActivityDate or (ad2.ActivityDate = ad.ActivityDate and ad2.ModifiedDate > ad.ModifiedDate))) "

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
                        CaseDesc.Text = ds.Tables(0).Rows(0).Item(2).ToString
                        CaseUser.Text = ds.Tables(0).Rows(0).Item(3).ToString
                        CaseNumber.Text = ds.Tables(0).Rows(0).Item(0).ToString
                        CaseCash.Text = ds.Tables(0).Rows(0).Item(4).ToString
                        FullPanel.Visible = True
                        EmptyPanel.Visible = False

                    End If
                End Using

                BindGrid()

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
    Private Sub ExecuteSql(query As String)

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString

        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand()

                cmd.CommandTimeout = 64000
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
    Private Sub BindGrid()

        'Dim Str As String = "select ad.Facility, ad.CashCategory, ad.CategoryStatus, ad.DepositDate, ad.WF_Cash_Received " & _
        '    ", af.* " & _
        '    "from DWH.AR.Activity_Detail ad " & _
        '    "join DWH.AR.Activity_Detail_AttachedFiles af on ad.ActivityID = af.ActivityID " & _
        '    "where ad.Detail_ID = '" & Replace(Request.QueryString("Detail_ID"), "'", "''") & "'"

        Dim Str As String = "select ad.Facility, ad.CashCategory, ad.DetailStatus, ar.DepositDate, ad.Cash_Received, af.* " & _
    "from DWH.AR.ActivityReport ar " & _
    "join DWH.AR.ActivityReport_Detail ad on ar.ActivityID = ad.ActivityID and ad.active = 1" & _
    "join DWH.AR.ActivityReport_AttachedFiles af on ad.ActivityID = af.ActivityID " & _
    "where ar.Active = 1 and ad.ActivityID = '" & Replace(Request.QueryString("Detail_ID"), "'", "''") & "' and not exists (select * from DWH.AR.ActivityReport_Detail ad2 " & _
        "where ad2.ActivityID = ad.ActivityID and (ad2.ActivityDate > ad.ActivityDate or (ad2.ActivityDate = ad.ActivityDate and ad2.ModifiedDate > ad.ModifiedDate))) "


        gvImages.DataSource = GetData(Str)
        gvImages.DataBind()


    End Sub

    Protected Sub UploadFile(sender As Object, e As EventArgs)

        Try


            Dim filename As String = Path.GetFileName(fileInput.PostedFile.FileName)

            Dim contentType As String = fileInput.PostedFile.ContentType

            Using fs As Stream = fileInput.PostedFile.InputStream

                Using br As New BinaryReader(fs)

                    Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                    Dim cmd As New SqlCommand

                    Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)

                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If

                        Dim query As String = "INSERT INTO DWH.AR.ActivityReport_AttachedFiles (ActivityID, FileName, ContentType, Content, UserID, DateAdded, Active) VALUES " & _
                            "('" & Replace(Request.QueryString("Detail_ID"), "'", "''") & "', @FileName, @ContentType, @Content, '" & Replace(Request.ServerVariables("AUTH_USER"), "NS\", "") & "', getdate(), 1)"

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

        'Cleanup 10/16/2017 CRW
        ExecuteSql("update af set af.ActivityDate = ev.Value from DWH.ar.ActivityReport_AttachedFiles af " & _
            "join DWH.ar.ActivityReport_ExtraValues ev on ev.Active = 1 and ev.Description = 'RunDate'  " & _
            "where ActivityDate is null ")


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

            cmd.CommandText = "select FileName, Content, ContentType from DWH.AR.ActivityReport_AttachedFiles where [AttachID]=@Id"

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