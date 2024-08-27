Imports System
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web
Imports System.Web.UI.HtmlControls
Imports DocumentFormat.OpenXml

Imports System.Configuration
Imports System.Collections
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

Imports FinanceWeb.WebFinGlobal
 

Public Class FREDCubes
    Inherits System.Web.UI.Page
    Dim SQL, SQL2, SQL3 As String
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    Dim cmd, cmd2 As SqlCommand
    Dim cmdNS1 As SqlCommand
    Dim dr As SqlDataReader
    Public SQLArray() As String
    Public FREDData As SqlDataSource

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then

            Else
               
                iframepdf.Attributes.Add("src", "http://nshdsweb/FinanceWeb/FRED/CubeTest.xlsx")

                Exit Sub

            End If

        Catch ex As Exception
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub




End Class