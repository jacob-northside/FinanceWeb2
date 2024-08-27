Imports FinanceWeb.WebFinGlobal
Imports System.IO
Imports System.Data.SqlClient
Imports System.DirectoryServices
Imports System.Web.Services.WebService
Imports System.Globalization

Public Class ENTDataEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack Then
                Debug.Print("Postback At" + Now)
            Else
                If User.Identity.IsAuthenticated = True Then
                    LoadENTCodes_Gridview()
                    LoadGACodes_Gridview()
                Else

                End If

            End If

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub LoadENTCodes_Gridview()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim SQL As String = "SELECT ID, GROUPID " & _
                               ", POD_ID " & _
                               ", CPT " & _
                               ", POS " & _
                               ", CATEGORY " & _
                               ", ALT_CATEGORY " & _
                               ", RATE " & _
                               ", ALT_RATE " & _
                               ", EFFECTIVE_FROM " & _
                               ", EFFECTIVE_TO " & _
                               "FROM dwh.ud.ENT_CPTCODES_RATES WHERE ACTIVE = 1;"
        ' "FROM DWH.UD.ENT_CPTCODES_RATES;"


        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                Update_CPTCODESRATES.DataSource = ds
                Update_CPTCODESRATES.DataMember = "LookUpData"
                Update_CPTCODESRATES.DataBind()
                ViewState("Update_CPTCODESRATES") = ds
                conn.Close()
            End Using



        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_CPTCODESRATES_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_CPTCODESRATES.RowEditing
        Try
            Update_CPTCODESRATES.EditIndex = e.NewEditIndex
            Update_CPTCODESRATES.DataSource = ViewState("Update_CPTCODESRATES") 'DirectCast(ViewState("TBL"), DataTable)
            Update_CPTCODESRATES.DataBind()
            'LoadLookUpGrid("Tables")
            Search(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
    Protected Sub Update_CPTCODESRATES_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_CPTCODESRATES.RowCancelingEdit
        Try
            Update_CPTCODESRATES.EditIndex = -1
            Update_CPTCODESRATES.DataSource = ViewState("Update_CPTCODESRATES") 'DirectCast(ViewState("TBL"), DataTable)
            Update_CPTCODESRATES.DataBind()
            'LoadLookUpGrid("Tables")
            Search(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_CPTCODESRATES_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Update_CPTCODESRATES.PageIndexChanging
        Try
            Update_CPTCODESRATES.PageIndex = e.NewPageIndex
            Update_CPTCODESRATES.DataSource = ViewState("Update_CPTCODESRATES")
            Update_CPTCODESRATES.DataBind()
            Search(sender, e)
            'LoadLookUpGrid("Schemas")

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_CPTCODESRATES_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_CPTCODESRATES.RowUpdating

        Try
            ' Get the values from the boundfields first '
            Dim GroupID As String = Update_CPTCODESRATES.Rows(e.RowIndex).Cells(2).Text ' Group ID Cell '
            Dim CPT As String = Update_CPTCODESRATES.Rows(e.RowIndex).Cells(4).Text ' 
            ' Now get templated fields ' 
            Dim POD_ID As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("POD_ID_EditBox")
            Dim POS As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("POS_EditBox")
            Dim Category As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("Category_EditBox")
            Dim Alt_Category As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("AltCategory_EditBox")
            Dim Rate As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("Rate_EditBox")
            Dim Alt_Rate As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("ALT_RATE_EditBox")
            Dim Effective_From As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("EFFECTIVE_FROM_EditBox")
            Dim Effective_To As TextBox = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("EFFECTIVE_TO_EditBox")
            Dim UpdateString As String

            ' Update array with 0 - Name, 1 - Value, 2 - SQL data type, 3 - SQL param options '
            Dim UpdateFields(9, 3) As String
            UpdateFields(0, 0) = "POD_ID"
            UpdateFields(0, 1) = POD_ID.Text
            UpdateFields(0, 2) = "Int"
            UpdateFields(0, 3) = 0

            UpdateFields(1, 0) = "POS"
            UpdateFields(1, 1) = POS.Text
            UpdateFields(1, 2) = "VarChar"
            UpdateFields(1, 3) = 0

            UpdateFields(2, 0) = "CATEGORY"
            UpdateFields(2, 1) = Category.Text
            UpdateFields(2, 2) = "VarChar"
            UpdateFields(2, 3) = 255

            UpdateFields(3, 0) = "ALT_CATEGORY"
            UpdateFields(3, 1) = Alt_Category.Text
            UpdateFields(3, 2) = "VarChar"
            UpdateFields(3, 3) = 255

            UpdateFields(4, 0) = "RATE"
            UpdateFields(4, 1) = Rate.Text
            UpdateFields(4, 2) = "Float"
            UpdateFields(4, 3) = 0

            UpdateFields(5, 0) = "ALT_RATE"
            UpdateFields(5, 1) = Alt_Rate.Text
            UpdateFields(5, 2) = "Float"
            UpdateFields(5, 3) = 0

            UpdateFields(6, 0) = "EFFECTIVE_FROM"
            UpdateFields(6, 1) = Effective_From.Text
            UpdateFields(6, 2) = "Date"
            UpdateFields(6, 3) = 0

            UpdateFields(7, 0) = "EFFECTIVE_TO"
            UpdateFields(7, 1) = Effective_To.Text
            UpdateFields(7, 2) = "Date"
            UpdateFields(7, 3) = 0

            UpdateFields(8, 0) = "GroupID"
            UpdateFields(8, 1) = GroupID
            UpdateFields(8, 2) = "VarChar"
            UpdateFields(8, 3) = 32

            UpdateFields(9, 0) = "CPT"
            UpdateFields(9, 1) = CPT
            UpdateFields(9, 2) = "VarChar"
            UpdateFields(9, 3) = 32


            ' Changing to parameterized queries to prevent SQL injection
            UpdateString = "Update dwh.ud.ENT_CPTCODES_RATES Set " & _
                                "POD_ID = @POD_ID, " & _
                                "POS = @POS, " & _
                                "Category = @Category, " & _
                                "Alt_Category = @Alt_Category, " & _
                                "Rate = @Rate, " & _
                                "Alt_Rate = @Alt_Rate, " & _
                                "Effective_From = @Effective_From, " & _
                                "Effective_To = @Effective_To " & _
                                " Where GroupID = @GroupID and CPT = @CPT AND ACTIVE = 1;"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)

                For i = 0 To UpdateFields.GetUpperBound(0)
                    If UpdateFields(i, 1) = "" Then
                        UpdateCommand.Parameters.AddWithValue("@" + UpdateFields(i, 0), DBNull.Value)
                        UpdateFields(i, 1) = "NULL"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "VarChar" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.VarChar, UpdateFields(i, 3)).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = "'" + UpdateFields(i, 1) + "'"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "Int" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.Int).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = "'" + UpdateFields(i, 1) + "'"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "Date" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.Date).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = "'" + UpdateFields(i, 1) + "'"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "Float" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.Float).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = UpdateFields(i, 1) ' For printing out query later and debugging '
                    End If

                Next

                Debug.Print("Executing SQL:" + vbCr + "Update DWH.UD.ENT_CPTCODES_RATES Set " + UpdateFields(0, 0) + " = " + UpdateFields(0, 1) + ", " + UpdateFields(1, 0) + " = " + UpdateFields(1, 1) + ", " + UpdateFields(2, 0) + " = " + UpdateFields(2, 1) + ", " + UpdateFields(3, 0) + " = " + UpdateFields(3, 1) + ", " + UpdateFields(4, 0) + " = " + UpdateFields(4, 1) + ", " + UpdateFields(5, 0) + " = " + UpdateFields(5, 1) + ", " + UpdateFields(6, 0) + " = " + UpdateFields(6, 1) + ", " + UpdateFields(7, 0) + " = " + UpdateFields(7, 1) + " where groupid = '" + GroupID + "' and cpt = '" + CPT + "';")
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                UpdateCommand.ExecuteNonQuery()
                conn.Close()
            End Using
            Update_CPTCODESRATES.EditIndex = -1
            Update_CPTCODESRATES.DataBind()
            ' LoadENTCodes_Gridview()
            Search(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try

    End Sub

    Protected Sub CPTCODESRATES_InsertRow(sender As Object, e As System.EventArgs) Handles CPTCODESRATES_Insert.Click
        Dim Insert_rates_sql As String = ""

        Try
            Insert_rates_sql = "Insert into DWH.UD.ENT_CPTCODES_RATES (ID, GROUPID, POD_ID, CPT, POS, CATEGORY, ALT_CATEGORY, RATE, ALT_RATE, EFFECTIVE_FROM, EFFECTIVE_TO, ACTIVE, Last_Modified) " & _
                                "Select max(ID) + 1, @GROUPID, @POD_ID, @CPT, @POS, @CATEGORY, @ALT_CATEGORY, @RATE, @ALT_RATE, @EFFECTIVE_FROM, @EFFECTIVE_TO, 1, CURRENT_TIMESTAMP FROM DWH.UD.ENT_CPTCODES_RATES"
            '"Values (@GROUPID, @POD_ID, @CPT, @POS, @CATEGORY, @ALT_CATEGORY, @RATE, @ALT_RATE, @EFFECTIVE_FROM, @EFFECTIVE_TO, 1, CURRENT_TIMESTAMP);"

            'Debug.Print("Executing SQL => Insert into DWH.UD.ENT_CPTCODES_RATES (GROUPID, POD_ID, CPT, POS, CATEGORY, ALT_CATEGORY, RATE, ALT_RATE, EFFECTIVE_FROM, EFFECTIVE_TO, ACTIVE, Last_Modified) " & _
            '            "Values (" + Group_Insert.Text + ", " + POD_Insert.Text + ", " + CPT_Insert.Text + ", " + POS_Insert.Text + ", " + Category_Insert.Text + ", " + AltCat_Insert.Text + ", " + Rate_Insert.Text & _
            '            +", " + AltRate_Insert.Text + ", " + EffFrom_Insert.Text + ", " + EffTo_Insert.Text + ", 1, CURRENT_TIMESTAMP);")
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim InsertCommand As SqlCommand = New SqlCommand(Insert_rates_sql, conn)
                InsertCommand.CommandTimeout = 86400         ' Set timeout


                If Group_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@GROUPID", SqlDbType.VarChar, 32).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@GROUPID", SqlDbType.VarChar, 32).Value = Group_Insert.Text
                End If


                If POD_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@POD_ID", SqlDbType.Int).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@POD_ID", SqlDbType.Int).Value = POD_Insert.Text
                End If


                If POS_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@POS", SqlDbType.Int).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@POS", SqlDbType.Int).Value = POS_Insert.Text
                End If


                If CPT_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@CPT", SqlDbType.VarChar, 32).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@CPT", SqlDbType.VarChar, 32).Value = CPT_Insert.Text
                End If

                If Category_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = Category_Insert.Text
                End If

                If AltCat_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@ALT_CATEGORY", SqlDbType.VarChar, 255).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@ALT_CATEGORY", SqlDbType.VarChar, 255).Value = AltCat_Insert.Text
                End If

                If Rate_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@RATE", SqlDbType.Float).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@RATE", SqlDbType.Float).Value = Rate_Insert.Text
                End If

                If AltRate_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@ALT_RATE", SqlDbType.Float).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@ALT_RATE", SqlDbType.Float).Value = AltRate_Insert.Text
                End If

                If EffFrom_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@EFFECTIVE_FROM", SqlDbType.Date).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@EFFECTIVE_FROM", SqlDbType.Date).Value = EffFrom_Insert.Text
                End If

                If EffTo_Insert.Text = "" Then
                    InsertCommand.Parameters.Add("@EFFECTIVE_TO", SqlDbType.Date).Value = EffTo_Insert.Text
                Else
                    InsertCommand.Parameters.Add("@EFFECTIVE_TO", SqlDbType.Date).Value = EffTo_Insert.Text
                End If


                'InsertCommand.Parameters.Add("@GROUPID", SqlDbType.VarChar, 32).Value = Group_Insert.Text
                'InsertCommand.Parameters.Add("@POD_ID", SqlDbType.Int).Value = POD_Insert.Text
                'InsertCommand.Parameters.Add("@CPT", SqlDbType.VarChar, 32).Value = CPT_Insert.Text
                'InsertCommand.Parameters.Add("@POS", SqlDbType.Int).Value = POS_Insert.Text
                'InsertCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = Category_Insert.Text
                'InsertCommand.Parameters.Add("@ALT_CATEGORY", SqlDbType.VarChar, 255).Value = AltCat_Insert.Text
                'InsertCommand.Parameters.Add("@RATE", SqlDbType.Float).Value = Rate_Insert.Text
                'InsertCommand.Parameters.Add("@ALT_RATE", SqlDbType.Float).Value = AltRate_Insert.Text
                'InsertCommand.Parameters.Add("@EFFECTIVE_FROM", SqlDbType.Date).Value = EffFrom_Insert.Text
                'InsertCommand.Parameters.Add("@EFFECTIVE_TO", SqlDbType.Date).Value = EffTo_Insert.Text


                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                    InsertCommand.ExecuteNonQuery()
                End If
                conn.Close()
            End Using
            ResponseMsg_CPT.Text = "Row Inserted Successfully"
            ResponseMsg_CPT.ForeColor = System.Drawing.Color.Green

        Catch ex As Exception
            ResponseMsg_CPT.Text = "Insert Error, please check values and formatting"
            ResponseMsg_CPT.ForeColor = System.Drawing.Color.Red
            Debug.Print("Insert Rates ERROR")
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_CPTCODESRATES_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Update_CPTCODESRATES.RowDeleting
        Try
            Dim DeleteString As String
            'DeleteString = "Delete from DWH.UD.ENT_CPTCODES_RATES WHERE ID = @ID;"
            DeleteString = "Update DWH.UD.ENT_CPTCODES_RATES SET ACTIVE = 0 WHERE ID = @ID AND ACTIVE = 1;"
            Dim ID As Label = Update_CPTCODESRATES.Rows(e.RowIndex).FindControl("ID")

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim DeleteCommand As SqlCommand = New SqlCommand(DeleteString, conn)

                DeleteCommand.Parameters.AddWithValue("@ID", ID.Text)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                DeleteCommand.ExecuteNonQuery()
                conn.Close()
            End Using
            Update_CPTCODESRATES.EditIndex = -1
            Update_CPTCODESRATES.DataBind()

            Search(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub


    Protected Sub Search(sender As Object, e As System.EventArgs) Handles tbl_SearchCPT.TextChanged, tbl_SearchCategories.TextChanged
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim search_rates_sql As String = ""

        Try
            search_rates_sql = "Select * from DWH.UD.ENT_CPTCODES_RATES WHERE ACTIVE = 1 AND CPT like '%' + @CPT + '%' AND (CATEGORY like '%' + @CATEGORY + '%' OR ALT_CATEGORY like '%' + @CATEGORY + '%');"
            Debug.Print("Executing SQL => Select * from DWH.UD.ENT_CPTCODES_RATES WHERE CPT like '%" + tbl_SearchCPT.Text + "%' and (CATEGORY like '%" + tbl_SearchCategories.Text + "%' OR ALT_CATEGORY like '%" + tbl_SearchCategories.Text + "%');")
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(search_rates_sql, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.SelectCommand.Parameters.Add("@CPT", SqlDbType.VarChar, 32).Value = tbl_SearchCPT.Text
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = tbl_SearchCategories.Text

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                conn.Close()

                Update_CPTCODESRATES.DataSource = ds
                Update_CPTCODESRATES.DataMember = "LookUpData"
                Update_CPTCODESRATES.DataBind()
                ViewState("Update_CPTCODESRATES") = ds
                conn.Close()
                ds.Dispose()
                da.Dispose()
            End Using

        Catch ex As Exception
            Debug.Print("SEARCH ERROR")
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub LoadGACodes_Gridview()
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim SQL As String = "SELECT ID, POD_ID " & _
                               ", PROVIDER_TYPE " & _
                               ", MD_NAME " & _
                               ", NPI " & _
                               ", POS " & _
                               ", CATEGORY " & _
                               ", RATE " & _
                               ", ALT_CATEGORY " & _
                               ", ALT_RATE " & _
                               ", EFFECTIVE_FROM " & _
                               ", EFFECTIVE_TO " & _
                               "FROM dwh.ud.ENT_GA_PROVIDERS WHERE ACTIVE = 1;"

        Try
            Debug.Print("Executing SQL => " & SQL)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(SQL, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                Update_GAProviders.DataSource = ds
                Update_GAProviders.DataMember = "LookUpData"
                Update_GAProviders.DataBind()
                ViewState("Update_GAProviders") = ds
                conn.Close()
            End Using

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_GAProviders_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Update_GAProviders.RowEditing
        Try
            Update_GAProviders.EditIndex = e.NewEditIndex
            Update_GAProviders.DataSource = ViewState("Update_GAProviders") 'DirectCast(ViewState("TBL"), DataTable)
            Update_GAProviders.DataBind()
            'LoadLookUpGrid("Tables")
            Search_GA(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_GAProviders_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles Update_GAProviders.RowCancelingEdit
        Try
            Update_GAProviders.EditIndex = -1
            Update_GAProviders.DataSource = ViewState("Update_GAProviders") 'DirectCast(ViewState("TBL"), DataTable)
            Update_GAProviders.DataBind()
            'LoadLookUpGrid("Tables")
            Search_GA(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_GAProviders_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Update_GAProviders.PageIndexChanging
        Try
            Update_GAProviders.PageIndex = e.NewPageIndex
            Update_GAProviders.DataSource = ViewState("Update_GAProviders")
            Update_GAProviders.DataBind()
            Search_GA(sender, e)
            'LoadLookUpGrid("Schemas")

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_GAProviders_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles Update_GAProviders.RowUpdating

        Try
            ' Get the values from the boundfields first '
            Dim ProviderType As String = Update_GAProviders.Rows(e.RowIndex).Cells(3).Text
            Dim MD_Name As String = Update_GAProviders.Rows(e.RowIndex).Cells(4).Text
            Dim NPI As String = Update_GAProviders.Rows(e.RowIndex).Cells(5).Text
            Dim Category As String = Update_GAProviders.Rows(e.RowIndex).Cells(7).Text
            ' Now get templated fields ' 
            Dim POD_ID As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("POD_ID_EditBox")
            Dim POS As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("POS_EditBox")
            Dim Alt_Category As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("ALT_CATEGORY_EditBox")
            Dim Rate As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("Rate_EditBox")
            Dim Alt_Rate As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("ALT_RATE_EditBox")
            Dim Effective_From As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("EFFECTIVE_FROM_EditBox")
            Dim Effective_To As TextBox = Update_GAProviders.Rows(e.RowIndex).FindControl("EFFECTIVE_TO_EditBox")

            ' Update array with 0 - Name, 1 - Value, 2 - SQL data type, 3 - SQL param options '
            Dim UpdateString As String
            Dim UpdateFields(10, 3) As String

            UpdateFields(0, 0) = "ProviderType"
            UpdateFields(0, 1) = ProviderType
            UpdateFields(0, 2) = "VarChar"
            UpdateFields(0, 3) = 255

            UpdateFields(1, 0) = "MD_Name"
            UpdateFields(1, 1) = MD_Name
            UpdateFields(1, 2) = "VarChar"
            UpdateFields(1, 3) = 255

            UpdateFields(2, 0) = "CATEGORY"
            UpdateFields(2, 1) = Category
            UpdateFields(2, 2) = "VarChar"
            UpdateFields(2, 3) = 255

            UpdateFields(3, 0) = "POD_ID"
            UpdateFields(3, 1) = POD_ID.Text
            UpdateFields(3, 2) = "Int"
            UpdateFields(3, 3) = 0

            UpdateFields(4, 0) = "POS"
            UpdateFields(4, 1) = POS.Text
            UpdateFields(4, 2) = "Int"
            UpdateFields(4, 3) = 0

            UpdateFields(5, 0) = "ALT_CATEGORY"
            UpdateFields(5, 1) = Alt_Category.Text
            UpdateFields(5, 2) = "VarChar"
            UpdateFields(5, 3) = 255

            UpdateFields(6, 0) = "RATE"
            UpdateFields(6, 1) = Rate.Text
            UpdateFields(6, 2) = "Float"
            UpdateFields(6, 3) = 0

            UpdateFields(7, 0) = "ALT_RATE"
            UpdateFields(7, 1) = Alt_Rate.Text
            UpdateFields(7, 2) = "Float"
            UpdateFields(7, 3) = 0

            UpdateFields(8, 0) = "EFFECTIVE_FROM"
            UpdateFields(8, 1) = Effective_From.Text
            UpdateFields(8, 2) = "Date"
            UpdateFields(8, 3) = 0

            UpdateFields(9, 0) = "EFFECTIVE_TO"
            UpdateFields(9, 1) = Effective_To.Text
            UpdateFields(9, 2) = "Date"
            UpdateFields(9, 3) = 0

            UpdateFields(10, 0) = "NPI"
            UpdateFields(10, 1) = NPI
            UpdateFields(10, 2) = "VarChar"
            UpdateFields(10, 3) = 32

            ' Changing to parameterized queries to prevent SQL injection
            UpdateString = "Update dwh.ud.ENT_GA_PROVIDERS Set " & _
                                "POD_ID = @POD_ID, " & _
                                "POS = @POS, " & _
                                "Alt_Category = @Alt_Category, " & _
                                "Rate = @Rate, " & _
                                "Alt_Rate = @Alt_Rate, " & _
                                "Effective_From = @Effective_From, " & _
                                "Effective_To = @Effective_To " & _
                                " Where Category = @Category and NPI = @NPI AND ACTIVE = 1;"

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim UpdateCommand As SqlCommand = New SqlCommand(UpdateString, conn)

                For i = 0 To UpdateFields.GetUpperBound(0)
                    If UpdateFields(i, 1) = "" Then
                        UpdateCommand.Parameters.AddWithValue("@" + UpdateFields(i, 0), DBNull.Value)
                        UpdateFields(i, 1) = "NULL"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "VarChar" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.VarChar, UpdateFields(i, 3)).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = "'" + UpdateFields(i, 1) + "'"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "Int" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.Int).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = "'" + UpdateFields(i, 1) + "'"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "Date" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.Date).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = "'" + UpdateFields(i, 1) + "'"  ' For printing out query later and debugging '
                    ElseIf UpdateFields(i, 2) = "Float" Then
                        UpdateCommand.Parameters.Add("@" + UpdateFields(i, 0), SqlDbType.Float).Value = UpdateFields(i, 1)
                        UpdateFields(i, 1) = UpdateFields(i, 1) ' For printing out query later and debugging '
                    End If

                Next

                Debug.Print("Executing SQL:" + vbCr + "Update DWH.UD.ENTGA_CPTCODES_RATES Set " + UpdateFields(3, 0) + " = " + UpdateFields(3, 1) + ", " + UpdateFields(4, 0) + " = " + UpdateFields(4, 1) + ", " + UpdateFields(5, 0) + " = " + UpdateFields(5, 1) + ", " + UpdateFields(6, 0) + " = " + UpdateFields(6, 1) + ", " + UpdateFields(7, 0) + " = " + UpdateFields(7, 1) + ", " + UpdateFields(8, 0) + " = " + UpdateFields(8, 1) + ", " + UpdateFields(9, 0) + " = " + UpdateFields(9, 1) + " where Category = '" + Category + "' and NPI = '" + NPI + "';")

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                UpdateCommand.ExecuteNonQuery()
                conn.Close()
            End Using
            Update_GAProviders.EditIndex = -1
            Update_GAProviders.DataBind()
            ' LoadENTCodes_Gridview()
            Search_GA(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub GAProviders_InsertRow(sender As Object, e As System.EventArgs) Handles GAProviders_Insert.Click
        Dim Insert_rates_sql As String = ""

        Try
            'Insert_rates_sql = "Insert into dwh.ud.ENT_GA_PROVIDERS (POD_ID, PROVIDER_TYPE, MD_NAME, NPI, POS, CATEGORY, RATE, ALT_CATEGORY, ALT_RATE, EFFECTIVE_FROM, EFFECTIVE_TO, ACTIVE, LAST_MODIFIED) " & _
            '                     "Values (@POD_ID, @PROVIDER_TYPE, @MD_NAME, @NPI, @POS, @CATEGORY, @RATE, @ALT_CATEGORY, @ALT_RATE, @EFFECTIVE_FROM, @EFFECTIVE_TO, 1, CURRENT_TIMESTAMP);"
            Insert_rates_sql = "Insert into dwh.ud.ENT_GA_PROVIDERS (ID, POD_ID, PROVIDER_TYPE, MD_NAME, NPI, POS, CATEGORY, RATE, ALT_CATEGORY, ALT_RATE, EFFECTIVE_FROM, EFFECTIVE_TO, ACTIVE, LAST_MODIFIED) " & _
                                " Select max(id) +1, @POD_ID, @PROVIDER_TYPE, @MD_NAME, @NPI, @POS, @CATEGORY, @RATE, @ALT_CATEGORY, @ALT_RATE, @EFFECTIVE_FROM, @EFFECTIVE_TO, 1, CURRENT_TIMESTAMP from DWH.UD.ENT_GA_PROVIDERS"


            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim InsertCommand As SqlCommand = New SqlCommand(Insert_rates_sql, conn)
                InsertCommand.CommandTimeout = 86400         ' Set timeout
                'InsertCommand.Parameters.Add("@POD_ID", SqlDbType.Int).Value = POD_Insert_GA.Text
                'InsertCommand.Parameters.Add("@PROVIDER_TYPE", SqlDbType.VarChar, 255).Value = Provider_Insert_GA.Text
                'InsertCommand.Parameters.Add("@MD_NAME", SqlDbType.VarChar, 255).Value = MD_Insert_GA.Text
                'InsertCommand.Parameters.Add("@NPI", SqlDbType.VarChar, 32).Value = NPI_Insert_GA.Text
                'InsertCommand.Parameters.Add("@POS", SqlDbType.Int).Value = POS_Insert_GA.Text
                'InsertCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = Category_Insert_GA.Text
                'InsertCommand.Parameters.Add("@RATE", SqlDbType.Float).Value = Rate_Insert_GA.Text
                'InsertCommand.Parameters.Add("@ALT_CATEGORY", SqlDbType.VarChar, 255).Value = AltCategory_Insert_GA.Text
                'InsertCommand.Parameters.Add("@ALT_RATE", SqlDbType.Float).Value = AltRate_Insert_GA.Text
                'InsertCommand.Parameters.Add("@EFFECTIVE_FROM", SqlDbType.Date).Value = EffFrom_Insert_GA.Text
                'InsertCommand.Parameters.Add("@EFFECTIVE_TO", SqlDbType.Date).Value = EffTo_Insert_GA.Text


                If POD_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@POD_ID", SqlDbType.Int).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@POD_ID", SqlDbType.Int).Value = POD_Insert_GA.Text
                End If



                If Provider_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@PROVIDER_TYPE", SqlDbType.VarChar, 255).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@PROVIDER_TYPE", SqlDbType.VarChar, 255).Value = Provider_Insert_GA.Text
                End If


                If MD_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@MD_NAME", SqlDbType.VarChar, 255).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@MD_NAME", SqlDbType.VarChar, 255).Value = MD_Insert_GA.Text
                End If


                If NPI_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@NPI", SqlDbType.VarChar, 32).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@NPI", SqlDbType.VarChar, 32).Value = NPI_Insert_GA.Text
                End If


                If POS_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@POS", SqlDbType.Int).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@POS", SqlDbType.Int).Value = POS_Insert_GA.Text
                End If

                If Category_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = Category_Insert_GA.Text
                End If

                If Rate_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@RATE", SqlDbType.Float).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@RATE", SqlDbType.Float).Value = Rate_Insert_GA.Text
                End If


                If AltCategory_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@ALT_CATEGORY", SqlDbType.VarChar, 255).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@ALT_CATEGORY", SqlDbType.VarChar, 255).Value = AltCategory_Insert_GA.Text
                End If


                If AltRate_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@ALT_RATE", SqlDbType.Float).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@ALT_RATE", SqlDbType.Float).Value = AltRate_Insert_GA.Text
                End If

                If EffFrom_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@EFFECTIVE_FROM", SqlDbType.Date).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@EFFECTIVE_FROM", SqlDbType.Date).Value = EffFrom_Insert_GA.Text
                End If

                If EffTo_Insert_GA.Text = "" Then
                    InsertCommand.Parameters.Add("@EFFECTIVE_TO", SqlDbType.Date).Value = DBNull.Value
                Else
                    InsertCommand.Parameters.Add("@EFFECTIVE_TO", SqlDbType.Date).Value = EffTo_Insert_GA.Text
                End If



                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                    InsertCommand.ExecuteNonQuery()
                End If
                conn.Close()
            End Using
            ResponseMsg_GA.Text = "Row Inserted Successfully"
            ResponseMsg_GA.ForeColor = System.Drawing.Color.Green

        Catch ex As Exception
            ResponseMsg_GA.Text = "Insert Error, please check values and formatting"
            ResponseMsg_GA.ForeColor = System.Drawing.Color.Red
            Debug.Print("Insert Rates ERROR")
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub

    Protected Sub Update_GAProviders_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Update_GAProviders.RowDeleting
        Try
            Dim DeleteString As String
            'DeleteString = "Delete from DWH.UD.ENT_GA_PROVIDERS WHERE ID = @ID;"
            DeleteString = "Update DWH.UD.ENT_GA_PROVIDERS SET ACTIVE=0 WHERE ID = @ID AND ACTIVE = 1;"
            Dim ID As Label = Update_GAProviders.Rows(e.RowIndex).FindControl("ID")

            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                Dim DeleteCommand As SqlCommand = New SqlCommand(DeleteString, conn)

                DeleteCommand.Parameters.AddWithValue("@ID", ID.Text)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                DeleteCommand.ExecuteNonQuery()
                conn.Close()
            End Using
            Update_GAProviders.EditIndex = -1
            Update_GAProviders.DataBind()

            Search_GA(sender, e)

        Catch ex As Exception
            Debug.Print("Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString)
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub



    Protected Sub Search_GA(sender As Object, e As System.EventArgs) Handles tbl_SearchNPI.TextChanged, tbl_SearchGACategories.TextChanged
        Dim ds As DataSet
        Dim da As SqlDataAdapter
        Dim search_rates_sql As String = ""

        Try
            search_rates_sql = "Select * from DWH.UD.ENT_GA_PROVIDERS WHERE ACTIVE=1 AND NPI like '%' + @NPI + '%' AND (CATEGORY like '%' + @CATEGORY + '%' OR ALT_CATEGORY like '%' + @CATEGORY + '%');"
            Debug.Print("Executing SQL => Select * from DWH.UD.ENT_CPTCODES_RATES WHERE CPT like '%" + tbl_SearchCPT.Text + "%' and (CATEGORY like '%" + tbl_SearchCategories.Text + "%' OR ALT_CATEGORY like '%" + tbl_SearchCategories.Text + "%');")
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("PRDconn").ConnectionString)
                ds = New DataSet                                ' Get blank dataset to store our data
                da = New SqlDataAdapter(search_rates_sql, conn)              ' New connection and our select command
                da.SelectCommand.CommandTimeout = 86400         ' Set timeout
                da.SelectCommand.Parameters.Add("@NPI", SqlDbType.VarChar, 32).Value = tbl_SearchNPI.Text
                da.SelectCommand.Parameters.Add("@CATEGORY", SqlDbType.VarChar, 255).Value = tbl_SearchGACategories.Text

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                da.Fill(ds, "LookUpData")                       ' Use adapter da to fill dataset ds with 'Lookup Data'?
                conn.Close()

                Update_GAProviders.DataSource = ds
                Update_GAProviders.DataMember = "LookUpData"
                Update_GAProviders.DataBind()
                ViewState("Update_GAProviders") = ds
                conn.Close()
                ds.Dispose()
                da.Dispose()
            End Using

        Catch ex As Exception
            Debug.Print("SEARCH ERROR")
            Debug.Print(ex.ToString)
            LogWebFinError(Replace(Request.ServerVariables("AUTH_USER"), "NS\", ""), Request.ServerVariables("HTTP_X_FORWARDED_FOR"), Request.ServerVariables("REMOTE_ADDR"), System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name(), ex.ToString())
        End Try
    End Sub
End Class