Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient

Imports FinanceWeb.WebFinGlobal

Public Class RadiologyUpload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private Sub ButtonUploadFile_Click(sender As Object, e As EventArgs) Handles ButtonUploadFile.Click

        Try

            Dim excelConnectionString As String = String.Empty
            'Dim uploadPath As String = "~/Uploads/"
            'Dim filePath As String = Server.MapPath(uploadPath + FileUploadExcel.PostedFile.FileName)

            Dim fileExt As String = Path.GetExtension(FileUploadExcel.PostedFile.FileName)

            Dim strConnection As [String] = ConfigurationManager.ConnectionStrings("WebFDconn").ConnectionString

            If fileExt = ".xls" OrElse fileExt = "XLS" Then
                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" & FileUploadExcel.PostedFile.FileName & "'" & "; Extended Properties ='Excel 8.0;HDR=Yes'"
            ElseIf fileExt = ".xlsx" OrElse fileExt = "XLSX" Then
                'excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FileUploadExcel.PostedFile.FileName & ";Extended Properties=Excel 12.0;Persist Security Info=False"
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FileUploadExcel.PostedFile.FileName & ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'"
            End If
            Dim excelConnection As New OleDbConnection(excelConnectionString)
            Dim cmd As New OleDbCommand("Select * from [HCPCS_List_from_Rad_Stats$]", excelConnection)
            excelConnection.Open()
            Dim dReader As OleDbDataReader
            dReader = cmd.ExecuteReader()
            Dim sqlBulk As New SqlBulkCopy(strConnection)
            sqlBulk.DestinationTableName = "[FinWeb].[RadHCPCSUpload]"
            sqlBulk.WriteToServer(dReader)
            MsgBox("Congratulations! Successfully Imported.")
            excelConnection.Close()

        Catch ex As Exception
            LogWebFinError(Replace(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), "'", "''"), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub


End Class