Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Data
Imports System.DirectoryServices



Public Class EncounterLogic
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = True Then

            Else

                Dim da As New SqlDataAdapter
                Dim cmd As SqlCommand
                Dim ds As New DataSet
                Dim fillddl As String = "SELECT Flag, UPPER(Flag) as upflag FROM MPA.dimEncounterFlags WHERE (Flag IS NOT NULL) and Active = 1  UNION " & _
                    "SELECT CategoryA AS Flag, UPPER(CategoryA) FROM MPA.dimEncounterFlags AS dimEncounterFlags_2 WHERE (CategoryA IS NOT NULL) and Active = 1  UNION " & _
                    "SELECT CategoryB AS Flag, UPPER(CategoryB) FROM MPA.dimEncounterFlags AS dimEncounterFlags_1 WHERE (CategoryB IS NOT NULL) and Active = 1  "

                Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If

                    cmd = New SqlCommand(fillddl, conn)
                    da.SelectCommand = cmd
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(ds, "OData")

                End Using

                'ddlUpdateFlag.DataSource = ds
                'ddlUpdateFlag.DataValueField = "upflag"
                'ddlUpdateFlag.DataTextField = "Flag"
                'ddlUpdateFlag.DataBind()

                ddlFlagViewer.DataSource = ds
                ddlFlagViewer.DataValueField = "Flag"
                ddlFlagViewer.DataTextField = "Flag"
                ddlFlagViewer.DataBind()

                pnlSimpleFlags.Visible = False
                pnlComplexFlags.Visible = False

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbViewFlagRules_Click(sender As Object, e As EventArgs) Handles lbViewFlagRules.Click
        Try
            Dim SQL As String
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim da2 As SqlDataAdapter
            Dim ds2 As New DataSet
            Dim cmd As SqlCommand
            Dim dr As SqlDataReader
            Dim i As Integer = 0

            lblAssociatedFlags.Text = ""

            SQL = "select distinct  flag  " & _
            "From DWH.MPA.dimEncounterFlags " & _
            "where (Flag = '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryA =  '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryB ='" & Replace(ddlFlagViewer.Text, "'", "''") & "')  and Active = 1  " & _
            "union " & _
            "select distinct  CategoryA  " & _
            "From DWH.MPA.dimEncounterFlags " & _
            "where (Flag = '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryA =  '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryB ='" & Replace(ddlFlagViewer.Text, "'", "''") & "')  and Active = 1   " & _
            "union " & _
            "select distinct  CategoryB  " & _
            "From DWH.MPA.dimEncounterFlags " & _
            "where (Flag = '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryA =  '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryB ='" & Replace(ddlFlagViewer.Text, "'", "''") & "')  and Active = 1   "

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                cmd = New SqlCommand(SQL, conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                dr = cmd.ExecuteReader

                While dr.Read
                    If IsDBNull(dr.Item(0)) Then
                    Else
                        If dr.Item(0) <> ddlFlagViewer.Text Then
                            lblAssociatedFlags.Text = lblAssociatedFlags.Text + dr.Item(0) + ", "
                        End If
                    End If
                End While
                dr.Read()
            End Using
  
            SQL = " select " & _
            "row_number() over (order by Field) [Rule #] , Flag, " & _
            "Facility as [Facility Specific] , Table_Name as [Table Location], Field, Operator, Value  " & _
            "From DWH.MPA.dimEncounterFlags  " & _
            "where (Flag = '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryA =  '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
            "or CategoryB = '" & Replace(ddlFlagViewer.Text, "'", "''") & "')  " & _
            "and Active = 1 and QueryType = 'Simple' "
  
            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(SQL, conn)
                da.Fill(ds)
            End Using
 
            If ds.Tables(0).Rows.Count = 0 Then
                pnlSimpleFlags.Visible = False
            Else
                pnlSimpleFlags.Visible = True
            End If

            gvFlagRules.DataSource = ds
            gvFlagRules.DataBind()

             

            ds2 = New DataSet

            SQL = " select " & _
               "row_number() over (order by Field) [Rule #], Flag, " & _
               "Field, Operator, Value , Facility , Table_Name " & _
               "From DWH.MPA.dimEncounterFlags  " & _
               "where (Flag = '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
               "or CategoryA =  '" & Replace(ddlFlagViewer.Text, "'", "''") & "' " & _
               "or CategoryB = '" & Replace(ddlFlagViewer.Text, "'", "''") & "')  " & _
               "and Active = 1 and QueryType = 'Complex' "
            Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                da = New SqlDataAdapter(SQL, conn)
                da.Fill(ds2)
            End Using


            If ds2.Tables(0).Rows.Count = 0 Then
                pnlComplexFlags.Visible = False
            Else
                pnlComplexFlags.Visible = True
            End If

            gvComplexRules.DataSource = ds2
            gvComplexRules.DataBind()

            If ds2.Tables(0).Rows.Count = 0 Then
                cFlagRules.Visible = False
            Else
                cFlagRules.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub


#Region "UpdateTab"
    'Private Sub ddlUpdateFlag_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUpdateFlag.SelectedIndexChanged
    '    UpdateFlag()
    'End Sub
    'Private Sub UpdateFlag()
    '    If ddlUpdateFlag.SelectedValue = "--XXNEWFLAGSELECTEDXX--" Then
    '        txtUpdateFlagName.Visible = True
    '    Else
    '        txtUpdateFlagName.Visible = False

    '        Dim sql As String
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet

    '        sql = " select " & _
    '              "row_number() over (order by Field) [Rule #] , " & _
    '              "Facility as [Facility Specific] , Table_Name as [Table Location], Field, Operator, Value  " & _
    '              "From DWH.MPA.dimEncounterFlags  " & _
    '              "where (Flag = '" & Replace(ddlUpdateFlag.SelectedValue, "'", "''") & "' " & _
    '              "or CategoryA =  '" & Replace(ddlUpdateFlag.SelectedValue, "'", "''") & "' " & _
    '              "or CategoryB = '" & Replace(ddlUpdateFlag.SelectedValue, "'", "''") & "')  " & _
    '              "and Active = 1 and QueryType = 'Simple' "

    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            da = New SqlDataAdapter(sql, conn)
    '            da.Fill(ds)
    '        End Using

    '        'If ds.Tables(0).Rows.Count = 0 Then
    '        '    pnlSimpleFlags.Visible = False
    '        'Else
    '        '    pnlSimpleFlags.Visible = True
    '        'End If

    '        'gvFlagRules.DataSource = ds
    '        'gvFlagRules.DataBind()



    '        Dim ds2 As New DataSet

    '        sql = " select " & _
    '           "row_number() over (order by Field) [Rule #], Flag, " & _
    '           "Field, Operator, Value , Facility , Table_Name " & _
    '           "From DWH.MPA.dimEncounterFlags  " & _
    '           "where (Flag = '" & Replace(ddlUpdateFlag.SelectedValue, "'", "''") & "' " & _
    '           "or CategoryA =  '" & Replace(ddlUpdateFlag.SelectedValue, "'", "''") & "' " & _
    '           "or CategoryB = '" & Replace(ddlUpdateFlag.SelectedValue, "'", "''") & "')  " & _
    '           "and Active = 1 and QueryType = 'Complex' "
    '        Using conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '            da = New SqlDataAdapter(sql, conn)
    '            da.Fill(ds2)
    '        End Using


    '        'If ds2.Tables(0).Rows.Count = 0 Then
    '        '    pnlComplexFlags.Visible = False
    '        'Else
    '        '    pnlComplexFlags.Visible = True
    '        'End If

    '        'gvComplexRules.DataSource = ds2
    '        'gvComplexRules.DataBind()

    '        'If ds2.Tables(0).Rows.Count = 0 Then
    '        '    cFlagRules.Visible = False
    '        'Else
    '        '    cFlagRules.Visible = True
    '        'End If

    '    End If
    'End Sub
    'Private Sub txtUpdateFlagName_TextChanged(sender As Object, e As EventArgs) Handles txtUpdateFlagName.TextChanged

    '    Dim CheckFlag As String = "Select count(*) from DWH.MPA.dimEncounterFlags where Flag = '" & Replace(txtUpdateFlagName.Text, "'", "''") & _
    '        "' or CategoryA = '" & Replace(txtUpdateFlagName.Text, "'", "''") & _
    '        "' or CategoryB = '" & Replace(txtUpdateFlagName.Text, "'", "''") & "'"

    '    Dim cnt As Integer
    '    Dim cmd As SqlCommand
    '    Dim subrws As Integer = 0

    '    Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
    '        cmd = New SqlCommand(CheckFlag, conn)
    '        cmd.CommandTimeout = 86400
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        cnt = cmd.ExecuteScalar

    '    End Using

    '    If cnt > 0 Then
    '        explantionlabel.Text = "This Flag already exists; Do you wish to update the existing flag?"
    '        mpeNoButton.Visible = True
    '        mpeYesButton.Visible = True
    '        OkButton.Visible = False
    '        ModalPopupExtender1.Show()
    '    End If

    'End Sub

    'Private Sub mpeNoButton_Click(sender As Object, e As EventArgs) Handles mpeNoButton.Click
    '    ModalPopupExtender1.Hide()
    '    txtUpdateFlagName.Text += "_2"
    '    mpeNoButton.Visible = False
    '    mpeYesButton.Visible = False
    '    OkButton.Visible = True
    'End Sub

    'Private Sub mpeYesButton_Click(sender As Object, e As EventArgs) Handles mpeYesButton.Click
    '    ModalPopupExtender1.Hide()
    '    ddlUpdateFlag.SelectedValue = txtUpdateFlagName.Text.ToUpper
    '    UpdateFlag()
    '    mpeNoButton.Visible = False
    '    mpeYesButton.Visible = False
    '    OkButton.Visible = True
    'End Sub

    'Private Sub btnSimpleValidate_Click(sender As Object, e As EventArgs) Handles btnSimpleValidate.Click
    '    If btnSimpleValidate.Text = "Validate" Then
    '        Dim basicsql As String = "Select top 100 * from " & ddlSimpleTables.SelectedValue & " where " &
    '        btnSimpleValidate.Text = "Add Rule"
    '    Else


    '        btnSimpleValidate.Text = "Validate"
    '    End If
    'End Sub
#End Region
End Class