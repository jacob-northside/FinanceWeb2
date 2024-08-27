Imports System
Imports System.IO
Imports System.Data
Imports System.Net
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Data.OleDb
 

Public Class BoardBooks
    Inherits System.Web.UI.Page
    Dim sqlcommand As SqlCommand


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''Code came from this example. http://forums.asp.net/t/1748838.aspx 

            'Original code that pulls info from the server but doesn't have access to run the shortcut. 

            'Dim path As String = Server.MapPath("~/BoardBooks")
            'Dim directory As DirectoryInfo = New DirectoryInfo(path)
            'Dim file As FileInfo = Nothing
            'Dim _Filesdata As New DataTable
            'Dim _NameColumn As New DataColumn("Name")
            'Dim _Link As New DataColumn("Link")
            '_Filesdata.Columns.Add(_NameColumn)
            '_Filesdata.Columns.Add(_Link)

            'For Each file In directory.GetFiles()
            '    Dim i As Int32 = 1
            '    _Filesdata.Rows.Add(Replace(file.Name, ".lnk", ""), "file:///NSHDSFILE:/Shared/FSI/BoardBooks/" + file.Name)
            'Next
            '_Filesdata.AcceptChanges()

            'With FileListRepeater
            '    .DataSource = _Filesdata
            '    .DataBind()
            'End With

            'Exit Sub  

            Dim directory As DirectoryInfo = New DirectoryInfo("\\Nshdsfile\files\Shared\FSI\BoardBooks")
            Dim Files As FileInfo() = directory.GetFiles()
            Dim file As FileInfo
            Dim _Filesdata As New DataTable
            Dim _NameColumn As New DataColumn("Name")
            Dim _Link As New DataColumn("Link")
            _Filesdata.Columns.Add(_NameColumn)
            _Filesdata.Columns.Add(_Link)

            For Each file In directory.GetFiles()
                Dim i As Int32 = 1
                If file.Name.Contains(".lnk") Then
                    '_Filesdata.Rows.Add(Replace(file.Name, ".lnk", ""), "file:///J:/Shared/FSI/BoardBooks/" + file.Name)
                    _Filesdata.Rows.Add(Replace(file.Name, ".lnk", ""), "file://NSHDSFILE/FILES/Shared/FSI/BoardBooks/" + file.Name)
                End If
            Next

            _Filesdata.Select(Nothing, "Name desc")
            _Filesdata.AcceptChanges()

            If _Filesdata.Rows.Count > 0 Then
                Dim dv As DataView = _Filesdata.DefaultView
                dv.Sort = "Name desc"
                _Filesdata = dv.ToTable
            End If

            With FileListRepeater
                .DataSource = _Filesdata
                .DataBind()
            End With

            Exit Sub


            'This code pulls from the shared drive and is modified off of the code above. 
            'Dim directory As DirectoryInfo = New DirectoryInfo("\\Nshdsfile\files\Shared\FSI\BoardBooks")
            'Dim file As FileInfo = Nothing
            'Dim _Filesdata As New DataTable
            'Dim _NameColumn As New DataColumn("Name")
            'Dim _Link As New DataColumn("Link")
            '_Filesdata.Columns.Add(_NameColumn)
            '_Filesdata.Columns.Add(_Link)

            'For Each file In directory.GetFiles()
            '    Dim i As Int32 = 1
            '    _Filesdata.Rows.Add(Replace(file.Name, ".lnk", ""), "file:///J:/Shared/FSI/BoardBooks/" + file.Name)
            'Next
            '_Filesdata.AcceptChanges()

            'With FileListRepeater
            '    .DataSource = _Filesdata
            '    .DataBind()
            'End With

        Catch ex As Exception
            Label1.Text = ex.ToString()
            Label1.Visible = True
        End Try

    End Sub

     
 
  
   
    Private Sub FileListRepeater_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles FileListRepeater.ItemDataBound
        Dim nRow As DataRowView = Nothing
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                With e.Item
                    nRow = DirectCast(e.Item.DataItem, DataRowView)
                    With DirectCast(.FindControl("lnkFile"), HyperLink)
                        If nRow("Name").ToString.Contains("Q4") Then
                            .Text = "<br /> " & nRow("Name")
                        Else
                            .Text = nRow("Name")
                        End If

                        .NavigateUrl = nRow("Link")
                    End With
                End With
        End Select
    End Sub
End Class