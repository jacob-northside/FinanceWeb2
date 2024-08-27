Imports System.Data.SqlClient


Public Class InsertColumnData
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then ' Used when posting with javascript?
                Debug.Print("Postback")
            Else
                If User.Identity.IsAuthenticated = True Then
                    InsertColumns.Visible = True
                Else
                    Insert_Columns_AlertBox.Text = "Please log in."
                End If
            End If
        Catch ex As Exception
            Debug.Print("EXCEPTION CATCHING PageLoad")
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Private Sub InsertNewColumnData() Handles InsertColData_BTN.Click
        Try
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim reader As SqlDataReader
            Dim SQL_Command, Insert_Command As SqlCommand
            Dim new_id As UInt32
            Dim Sql As String = "Insert into DWH.DOC.NZ_FDColumnData (Id, ColumnName, ColumnDesc) VALUES " & _
                "((Select Max(id) from DWH.DOC.NZ_FDColumnData)+1, '" & InsertColumnName.Text & "', '" & InsertColumnDesc.Text & "');"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Insert_Command = New SqlCommand(Sql, conn)
                Insert_Command.ExecuteNonQuery()
                Debug.Print("Insert Complete")
                conn.Close()
            End Using

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                SQL_Command = New SqlCommand("Select max(id) from dwh.doc.nz_fdcolumndata;", conn)
                reader = SQL_Command.ExecuteReader
                While reader.Read()
                    new_id = reader(0).ToString
                End While
                conn.Close()
            End Using

            Insert_Columns_AlertBox.Text = "New Column Inserted with ID of " + new_id.ToString
            Insert_Columns_AlertBox.Visible = True

        Catch ex As Exception
            Debug.Print("EXCEPTION CATCHING InsertColDataBTN.Click")
            Debug.Print(ex.ToString)
            'LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

End Class