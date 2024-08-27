Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Configuration

Imports FinanceWeb.WebFinGlobal

Public Class ARSnapShotsInstructions
    Inherits System.Web.UI.Page

    Private Sub ARSnapShotsInstructions_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not Request.Item("t") Is Nothing Then
            If IsNumeric(Request.Item("t")) Then
                tcARSnapShotInstructions.ActiveTabIndex = CInt(Request.Item("t"))
            End If
        Else
            tcARSnapShotInstructions.ActiveTabIndex = 0
        End If
    End Sub
End Class