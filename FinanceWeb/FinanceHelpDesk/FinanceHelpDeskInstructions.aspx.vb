Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class FinanceHelpDeskInstructions
    Inherits System.Web.UI.Page
    Private Shared admin As Integer = 0

    Private Sub FinanceHelpDeskInstructions_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Dim cmd As SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet

            Dim adminsql As String = "SELECT count(*) FROM [WebFD].[FinanceHelpDesk].[tblUsers] where uid = '" & _
    Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''") & "' and IsRep = 1"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(adminsql, conn)
                cmd.CommandTimeout = 86400
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                admin = cmd.ExecuteScalar

            End Using

            If admin > 0 Then
                tpAdministrative.Visible = True
                tpOpenCases.Visible = True
                pnlSearchAdmin.Visible = True
                pnlSearchUser.Visible = False
                MainPanelAdmin.Visible = True
                MainPanelUser.Visible = False
            End If

            If Not Request.Item("t") Is Nothing Then
                If IsNumeric(Request.Item("t")) Then
                    tcFinHelpDeskInstructions.ActiveTabIndex = CInt(Request.Item("t"))
                End If
            Else
                tcFinHelpDeskInstructions.ActiveTabIndex = 0
            End If
        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lbViewMainTab_Click(sender As Object, e As EventArgs) Handles lbViewMainTab.Click
        tcFinHelpDeskInstructions.ActiveTabIndex = tpMainPage.TabIndex
    End Sub
End Class