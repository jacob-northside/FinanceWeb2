Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System
Imports System.IO
Imports System.Data


Imports System.Web.Security.ActiveDirectoryMembershipProvider
Imports System.Security.Principal
Imports System.Data.Sql

Public Class WebFinGlobal

    Public Shared Sub LogWebFinError(ByVal UserID As String, ByVal ForwardedAddr As String, ByVal RemoteAddr As String, ByVal WebPage As String, ByVal CodeBlock As String, ByVal CodeError As String)
        Dim conString As String = ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString
        Dim SQL As String
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter

        Dim saddr As String
        saddr = ForwardedAddr

        If saddr = "" Then
            saddr = RemoteAddr
        End If

        Try



            Using con As New SqlConnection(conString)
                Sql = "insert into WebFD.FinWeb.ERRORLOG " & _
                      "(USERID, Page, CodeBlock, ExceptionCode, IPAddress, DateLogged )  " & _
                      " values " & _
                      "('" & Replace(UserID, "'", "''") & "', '" & Replace(WebPage, "'", "''") & "', " & _
                      "'" & Replace(CodeBlock, "'", "''") & "', '" & Replace(CodeError, "'", "''") & "', '" & saddr & "', convert(date, getdate()) ) "
                cmd = New SqlCommand(Sql, con)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Using con As New SqlConnection(conString)
                SQL = "insert into WebFD.FinWeb.ERRORLOG " & _
                      "(USERID, Page, CodeBlock, ExceptionCode, IPAddress, DateLogged )  " & _
                      " values " & _
                      "('" & Replace(UserID, "'", "''") & "', '" & Replace(WebPage, "'", "''") & "', " & _
                      "'" & Replace(CodeBlock, "'", "''") & "', '" & Replace(CodeError, "'", "''") & "', 'UNKNOWN', convert(date, getdate()) ) "
                cmd = New SqlCommand(Sql, con)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Try
    End Sub

End Class

