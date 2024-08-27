Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Globalization
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls.WebControl

Imports FinanceWeb.WebFinGlobal


Public Class Armored_Car_Tracking
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack Then

            Else
                txtFilter.Text = ""
                PopulateLocationGrid()
                CalEx.StartDate = DateAdd(DateInterval.Day, -6, Today)
                CalEx.EndDate = Today
            End If

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
    Private Sub PopulateLocationGrid()
        Try
            gvLocationOptions.DataSource = GetData("select * from (  " & _
                "select convert(varchar, LOCATION_NUMBER) as VendorReferenceNo, 'D~'+convert(varchar, LOCATION_NUMBER) as VendRefNo " & _
                ", LOCATION_NAME " & _
                ", LOCATION_ADDRESS1 " & _
                ", case when LOCATION_ADDRESS2 like '#'+ convert(varchar, LOCATION_NUMBER) + ' - %'  " & _
                    "then SUBSTRING(LOCATION_ADDRESS2, len(LOCATION_NUMBER) + 5, len(LOCATION_ADDRESS2)) " & _
                    "when LOCATION_ADDRESS2 like '#'+ convert(varchar, LOCATION_NUMBER) + '-%'  " & _
                    "then SUBSTRING(LOCATION_ADDRESS2, len(LOCATION_NUMBER) + 3, len(LOCATION_ADDRESS2)) " & _
                    "when LOCATION_ADDRESS2 like '#'+ convert(varchar, LOCATION_NUMBER) + '%'  " & _
                    "then SUBSTRING(LOCATION_ADDRESS2, len(LOCATION_NUMBER) + 2, len(LOCATION_ADDRESS2)) " & _
                    "when LOCATION_ADDRESS2 = '#' + convert(varchar, LOCATION_NUMBER) then null " & _
                    "else LOCATION_ADDRESS2 end " & _
                 "as LOCATION_ADDRESS2   " & _
                ", LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP  " & _
                 ", ROW_NUMBER() over (partition by 'Dunbar', Location_Number order by Invoice_Date desc) as RN  " & _
                 ", 'Dunbar' as Vendor " & _
                "from DWH.ACCT.DunbarInvoices   " & _
                "union all   " & _
                "select SITE_CUSTOMER_NUMBER, 'L~'+SITE_CUSTOMER_NUMBER, /*LOCATION, */ PROD_SITE_CUSTOMER_NAME, ADDRESS_LINE, null,   " & _
                "CITY, STATE, ZIP, ROW_NUMBER() over (partition by 'Loomis', SITE_CUSTOMER_NUMBER order by Trans_Service_Date desc) as RN  " & _
                ", 'Loomis' as Vendor " & _
                "from DWH.ACCT.LOOMISINVOICES) x where RN = 1 ")
            gvLocationOptions.DataBind()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLocationOptions_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvLocationOptions.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" & e.Row.RowIndex.ToString))


            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub gvLocationOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvLocationOptions.SelectedIndexChanged
        Try

            lblLocationName.Text = Replace(gvLocationOptions.SelectedRow.Cells(3).Text, "&nbsp;", "")
            lblAddress.Text = Replace(gvLocationOptions.SelectedRow.Cells(4).Text, "&nbsp;", "")
            If Len(Replace(gvLocationOptions.SelectedRow.Cells(6).Text, "&nbsp;", "")) > 0 Then
                lblAddress2.Text = Replace(gvLocationOptions.SelectedRow.Cells(6).Text, "&nbsp;", "") & ", " & Replace(gvLocationOptions.SelectedRow.Cells(7).Text, "&nbsp;", "")
            Else
                Replace(gvLocationOptions.SelectedRow.Cells(7).Text, "&nbsp;", "")
            End If

            For Each canoe As GridViewRow In gvLocationOptions.Rows
                If canoe.RowIndex = gvLocationOptions.SelectedIndex Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#bbffbb")
                ElseIf canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next

            If gvLocationOptions.SelectedIndex >= 0 Then
                pnlSubmittal.Visible = True
            Else
                pnlSubmittal.Visible = False
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", "filter();", True)

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub btnSubmitMissedDate_Click(sender As Object, e As EventArgs) Handles btnSubmitMissedDate.Click
        Try
            If Len(Trim(txtContactName.Text)) > 1 Then
            Else
                lblExplanationLabel.Text = "Please enter the Contact's Name"
                mpeStandard.Show()
                Exit Sub
            End If

            If Len(Trim(txtContactEmail.Text)) < 7 Then
                lblExplanationLabel.Text = "Please enter the Contact's Full Email Address"
                mpeStandard.Show()
                Exit Sub
            ElseIf InStr(txtContactEmail.Text, "@", CompareMethod.Text) < 1 Then
                lblExplanationLabel.Text = "Please enter the Contact's Full Email Address"
                mpeStandard.Show()
                Exit Sub
            End If

            Dim x As Int64
            If Len(Trim(txtContactPhone.Text)) < 10 Then
                lblExplanationLabel.Text = "Please enter the Contact's Full Phone Number"
                mpeStandard.Show()
                Exit Sub
            ElseIf Len(Trim(txtContactPhone.Text)) > 11 Then
                lblExplanationLabel.Text = "Listed Phone Number is too long"
                mpeStandard.Show()
                Exit Sub
            ElseIf Int64.TryParse(Replace(Replace(Replace(txtContactPhone.Text, "-", ""), "(", ""), ")", ""), x) = False Then
                lblExplanationLabel.Text = "Cannot parse Contact's Phone Number"
                mpeStandard.Show()
                Exit Sub
            End If

            Dim y As Date

            If Len(Trim(txtMissedDate.Text)) < 8 Then
                lblExplanationLabel.Text = "Please select Missed Date"
                mpeStandard.Show()
                Exit Sub
            ElseIf Date.TryParse(txtMissedDate.Text, y) = False Then
                lblExplanationLabel.Text = "Cannot parse " & txtMissedDate.Text & " as Date"
                mpeStandard.Show()
                Exit Sub
            ElseIf Date.Parse(txtMissedDate.Text) < DateAdd(DateInterval.Day, -6, Today) Then
                lblExplanationLabel.Text = "Cannot submit missed pickups more than a week after incident"
                mpeStandard.Show()
                Exit Sub
            ElseIf Date.Parse(txtMissedDate.Text) > Today Then
                lblExplanationLabel.Text = "Cannot submit missed pickups in the future"
                mpeStandard.Show()
                Exit Sub
            End If

            Dim address As String
            address = Replace(gvLocationOptions.SelectedRow.Cells(4).Text, "&nbsp;", "") & " " & Replace(gvLocationOptions.SelectedRow.Cells(5).Text, "&nbsp;", "") & " " & _
                Replace(gvLocationOptions.SelectedRow.Cells(6).Text, "&nbsp;", "") & ", " & Replace(gvLocationOptions.SelectedRow.Cells(7).Text, "&nbsp;", "") & " " & _
                Replace(gvLocationOptions.SelectedRow.Cells(8).Text, "&nbsp;", "")

            Dim zz As String = "Insert into DWH.ACCT.ArmoredCarTracking " & _
                " (TrackID, LocationID, LocationNameAtSubmittal, LocationAddressAtSubmittal, ContactName, ContactEmail, ContactPhone, DateMissed, DateSubmitted, SubmittedBy, Active) " & _
                "output Inserted.TrackID select isnull(max(TrackID), 0) + 1, '" & gvLocationOptions.SelectedDataKey().Value.ToString & "', '" & Replace(gvLocationOptions.SelectedRow.Cells(3).Text, "'", "''") & "', '" & address & "', '" & _
                Replace(Trim(txtContactName.Text), "'", "''") & "', '" & Replace(Trim(txtContactEmail.Text), "'", "''") & "', '" & Replace(x, "'", "''") & "', '" & Replace(y, "'", "''") & _
                "', getdate(), '" & Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "', 1 from DWH.ACCT.ArmoredCarTracking "

            Dim ResultID As Integer
            Try
                ResultID = GetScalar(zz)
            Catch ex As Exception
                lblExplanationLabel.Text = "Error Submitting Missed Pickup; please contact Web Admin"
                mpeStandard.Show()
                Exit Sub
            End Try

            'Try
            '    ResultID = GetScalar("exec DWH.ACCT.ArmoredCarTracking_Email")
            'Catch ex As Exception
            '    lblExplanationLabel.Text = "Emails not allowed.  Boo."
            '    mpeStandard.Show()
            '    Exit Sub
            'End Try


            txtFilter.Text = ""
            txtContactEmail.Text = ""
            txtContactName.Text = ""
            txtContactPhone.Text = ""
            txtMissedDate.Text = ""
            gvLocationOptions.SelectedIndex = -1
            pnlSubmittal.Visible = False

            For Each canoe As GridViewRow In gvLocationOptions.Rows
                If canoe.RowIndex Mod 2 = 0 Then
                    canoe.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBE3FB")
                Else
                    canoe.BackColor = System.Drawing.Color.White
                End If
            Next


            lblExplanationLabel.Text = "Missed Pickup Submitted.  You should receive an email shortly."
            mpeStandard.Show()

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class

